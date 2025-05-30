Imports System.Drawing.Printing
Imports MySql.Data.MySqlClient

Public Class FR_KELUAR
    Private Const DEFAULT_KODE_PREFIX As String = "BRG0"
    Private Const CURRENCY_FORMAT As String = "Rp {0:N0}"

    Private Sub FR_KELUAR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeEventHandlers()
        ResetProductInput()
    End Sub

    Private Sub InitializeEventHandlers()
        AddHandler txtJumlah.KeyDown, AddressOf txtJumlah_KeyDown
        AddHandler dgvTampil.CellEndEdit, AddressOf dgvTampil_CellEndEdit
        AddHandler dgvTampil.EditingControlShowing, AddressOf dgvTampil_EditingControlShowing
    End Sub

    Private Sub ClearProductInputs()
        txtBarang.Clear()
        txtSatuan.Clear()
        txtHarga.Clear()
    End Sub

    Private Sub ResetProductInput()
        txtKode.Text = DEFAULT_KODE_PREFIX
        txtKode.SelectionStart = txtKode.Text.Length
        ClearProductInputs()
        txtJumlah.Clear()
        txtKode.Focus()
    End Sub

    Private Sub UpdateTotalAmount()
        Dim totalSum As Decimal = CalculateGridTotal()
        lblHarga.Text = String.Format(CURRENCY_FORMAT, totalSum)
        txtTotalHarga.Text = lblHarga.Text
    End Sub

    Private Function CalculateGridTotal() As Decimal
        Return dgvTampil.Rows.Cast(Of DataGridViewRow)() _
            .Where(Function(r) r.Cells("Total").Value IsNot Nothing) _
            .Sum(Function(r) Convert.ToDecimal(r.Cells("Total").Value))
    End Function

    Private Function GetAvailableStock(productCode As String) As Integer
        Dim stockIn As Integer = GetStockFromDatabase("transaksi_masuk", "jumlah", productCode)
        Dim stockOut As Integer = GetStockFromDatabase("transaksi_keluar", "qty", productCode)

        Return stockIn - stockOut
    End Function

    Private Function GetStockFromDatabase(tableName As String, columnName As String, productCode As String) As Integer
        Try
            BukaKoneksi()
            Dim query As String = $"SELECT COALESCE(SUM({columnName}),0) FROM {tableName} WHERE kode_barang = @kode"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@kode", productCode)
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        Catch ex As Exception
            ShowErrorMessage("Gagal mengambil stok: " & ex.Message)
            Return 0
        Finally
            conn.Close()
        End Try
    End Function

    Private Function ReserveStock(productCode As String, quantity As Integer) As Boolean
        Try
            BukaKoneksi()
            Dim query As String = "SELECT id, jumlah FROM transaksi_masuk WHERE kode_barang = @kode AND jumlah > 0 ORDER BY tanggal_masuk ASC, id ASC"
            Dim stokList As New List(Of Tuple(Of Integer, Integer))
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@kode", productCode)
                Using rd = cmd.ExecuteReader()
                    While rd.Read()
                        stokList.Add(Tuple.Create(Convert.ToInt32(rd("id")), Convert.ToInt32(rd("jumlah"))))
                    End While
                End Using
            End Using

            Dim sisaQty As Integer = quantity
            For Each stok In stokList
                If sisaQty <= 0 Then Exit For
                Dim ambil As Integer = Math.Min(stok.Item2, sisaQty)
                Dim updateQuery As String = "UPDATE transaksi_masuk SET jumlah = jumlah - @qty WHERE id = @id"
                Using updateCmd As New MySqlCommand(updateQuery, conn)
                    updateCmd.Parameters.AddWithValue("@qty", ambil)
                    updateCmd.Parameters.AddWithValue("@id", stok.Item1)
                    updateCmd.ExecuteNonQuery()
                End Using
                sisaQty -= ambil
            Next

            If sisaQty > 0 Then
                ShowWarningMessage("Stok tidak cukup untuk diambil.")
                Return False
            End If

            Return True
        Catch ex As Exception
            ShowErrorMessage("Gagal mereservasi stok: " & ex.Message)
            Return False
        Finally
            conn.Close()
        End Try
    End Function

    Private Function ReleaseStock(productCode As String, quantity As Integer) As Boolean
        Try
            BukaKoneksi()
            Dim query As String = "UPDATE transaksi_masuk SET jumlah = jumlah + @qty WHERE kode_barang = @kode LIMIT 1"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@qty", quantity)
                cmd.Parameters.AddWithValue("@kode", productCode)
                cmd.ExecuteNonQuery()
                Return True
            End Using
        Catch ex As Exception
            ShowErrorMessage("Gagal mengembalikan stok: " & ex.Message)
            Return False
        Finally
            conn.Close()
        End Try
    End Function

    Private Sub LoadProductData(productCode As String)
        If String.IsNullOrWhiteSpace(productCode) Then
            ClearProductInputs()
            Return
        End If

        Try
            BukaKoneksi()
            Dim query As String = "SELECT nama_barang, satuan, harga_satuan FROM barang WHERE kode_barang = @kode"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@kode", productCode.Trim)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    If reader.Read() Then
                        SetProductData(reader)
                    Else
                        ClearProductInputs()
                    End If
                End Using
            End Using
        Catch ex As Exception
            ShowErrorMessage("Terjadi kesalahan saat mengambil data barang: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub SetProductData(reader As MySqlDataReader)
        txtBarang.Text = reader("nama_barang").ToString()
        txtSatuan.Text = reader("satuan").ToString()
        txtHarga.Text = reader("harga_satuan").ToString()
    End Sub

    Private Function AddOrUpdateProductInGrid(quantity As Integer, price As Decimal) As Boolean
        Dim productCode As String = txtKode.Text.Trim
        Dim existingRow As DataGridViewRow = FindProductInGrid(productCode)

        If Not ReserveStock(productCode, quantity) Then
            ShowWarningMessage("Gagal mengabil stok untuk produk ini!")
            Return False
        End If

        If existingRow IsNot Nothing Then
            UpdateExistingProductRow(existingRow, quantity, price)
        Else
            AddNewProductRow(quantity, price)
        End If

        Return True
    End Function

    Private Function FindProductInGrid(productCode As String) As DataGridViewRow
        Return dgvTampil.Rows.Cast(Of DataGridViewRow)() _
            .FirstOrDefault(Function(r) r.Cells("Kode").Value?.ToString() = productCode)
    End Function

    Private Sub UpdateExistingProductRow(row As DataGridViewRow, additionalQuantity As Integer, price As Decimal)
        Dim newQuantity As Integer = Convert.ToInt32(row.Cells("Qty").Value) + additionalQuantity
        row.Cells("Qty").Value = newQuantity
        row.Cells("Total").Value = price * newQuantity
    End Sub

    Private Sub AddNewProductRow(quantity As Integer, price As Decimal)
        Dim total As Decimal = price * quantity
        dgvTampil.Rows.Add(txtKode.Text, txtBarang.Text, txtSatuan.Text, price, quantity, total)
    End Sub

    Private Function RemoveProductFromGrid(row As DataGridViewRow) As Boolean
        Dim productCode As String = row.Cells("Kode").Value.ToString()
        Dim quantity As Integer = Convert.ToInt32(row.Cells("Qty").Value)

        If ReleaseStock(productCode, quantity) Then
            dgvTampil.Rows.Remove(row)
            Return True
        Else
            ShowWarningMessage("Gagal mengembalikan stok ke database!")
            Return False
        End If
    End Function

    Private Function IsValidProductInput() As Boolean
        If String.IsNullOrWhiteSpace(txtKode.Text) OrElse String.IsNullOrWhiteSpace(txtBarang.Text) Then
            ShowWarningMessage("Lengkapi data terlebih dahulu.")
            Return False
        End If
        Return True
    End Function

    Private Function GetValidQuantity() As Integer
        Dim quantity As Integer
        If Integer.TryParse(txtJumlah.Text, quantity) AndAlso quantity > 0 Then
            Return quantity
        End If
        Return 1
    End Function

    Private Function IsStockSufficient(requestedQuantity As Integer, productCode As String) As Boolean
        Dim availableStock As Integer = GetAvailableStock(productCode)
        If requestedQuantity > availableStock Then
            ShowWarningMessage($"Melebihi stok! Stok tersedia: {availableStock}")
            txtJumlah.Focus()
            txtJumlah.SelectAll()
            Return False
        End If
        Return True
    End Function

    Private Sub txtKode_TextChanged(sender As Object, e As EventArgs) Handles txtKode.TextChanged
        LoadProductData(txtKode.Text)
    End Sub

    Private Sub txtJumlah_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode <> Keys.Enter Then Return
        ProcessProductEntry()
    End Sub

    Private Sub ProcessProductEntry()
        If Not IsValidProductInput() Then Return

        Dim quantity As Integer = GetValidQuantity()
        If Not IsStockSufficient(quantity, txtKode.Text.Trim) Then Return

        Dim price As Decimal = Decimal.Parse(txtHarga.Text)

        If AddOrUpdateProductInGrid(quantity, price) Then
            UpdateTotalAmount()
            ResetProductInput()
        End If
    End Sub

    Private Sub dgvTampil_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs)
        If dgvTampil.Columns(e.ColumnIndex).Name <> "Qty" Then Return
        ValidateAndUpdateGridRow(e.RowIndex)
    End Sub

    Private Sub ValidateAndUpdateGridRow(rowIndex As Integer)
        Dim row As DataGridViewRow = dgvTampil.Rows(rowIndex)
        Dim price As Decimal = Convert.ToDecimal(row.Cells("Harga").Value)
        Dim newQuantity As Integer = Convert.ToInt32(row.Cells("Qty").Value)
        Dim productCode As String = row.Cells("Kode").Value.ToString()

        Dim oldQuantity As Integer = GetOldQuantityFromGrid(productCode, rowIndex)
        Dim quantityDifference As Integer = newQuantity - oldQuantity

        If quantityDifference > 0 Then
            Dim availableStock As Integer = GetAvailableStock(productCode)
            If quantityDifference > availableStock Then
                ShowWarningMessage($"Melebihi stok! Stok tersedia: {availableStock}")
                row.Cells("Qty").Value = oldQuantity + availableStock
                newQuantity = oldQuantity + availableStock
                quantityDifference = availableStock
            End If

            If quantityDifference > 0 Then
                ReserveStock(productCode, quantityDifference)
            End If

        ElseIf quantityDifference < 0 Then
            ReleaseStock(productCode, Math.Abs(quantityDifference))
        End If

        row.Cells("Total").Value = price * newQuantity
        UpdateTotalAmount()
    End Sub

    Private Function GetOldQuantityFromGrid(productCode As String, currentRowIndex As Integer) As Integer
        Static originalQuantities As New Dictionary(Of String, Integer)

        If originalQuantities.ContainsKey($"{productCode}_{currentRowIndex}") Then
            Return originalQuantities($"{productCode}_{currentRowIndex}")
        End If

        Dim currentQty As Integer = Convert.ToInt32(dgvTampil.Rows(currentRowIndex).Cells("Qty").Value)
        originalQuantities($"{productCode}_{currentRowIndex}") = currentQty
        Return currentQty
    End Function

    Private Sub dgvTampil_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgvTampil.EditingControlShowing
        If dgvTampil.CurrentCell.ColumnIndex = dgvTampil.Columns("Qty").Index Then
            AttachNumericValidationToTextBox(e.Control)
        End If
    End Sub

    Private Sub AttachNumericValidationToTextBox(control As Control)
        Dim textBox As TextBox = TryCast(control, TextBox)
        If textBox IsNot Nothing Then
            RemoveHandler textBox.KeyPress, AddressOf QtyColumn_KeyPress
            AddHandler textBox.KeyPress, AddressOf QtyColumn_KeyPress
        End If
    End Sub

    Private Sub QtyColumn_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub HapusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HapusToolStripMenuItem.Click
        If Not HasSelectedRow() Then
            ShowInfoMessage("Pilih baris yang ingin dihapus terlebih dahulu.")
            Return
        End If

        If ConfirmDeletion() Then
            Dim selectedRow As DataGridViewRow = dgvTampil.SelectedRows(0)
            If RemoveProductFromGrid(selectedRow) Then
                UpdateTotalAmount()
                ClearPaymentIfNoItems()
            End If
        End If
    End Sub

    Private Function HasSelectedRow() As Boolean
        Return dgvTampil.SelectedRows.Count > 0
    End Function

    Private Function ConfirmDeletion() As Boolean
        Return MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi",
                              MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes
    End Function

    Private Sub ClearPaymentIfNoItems()
        If dgvTampil.Rows.Count = 0 Then
            txtTunai.Clear()
            txtKembalian.Clear()
        End If
    End Sub

    Private Sub dgvTampil_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvTampil.CellMouseDown
        If e.Button = MouseButtons.Right AndAlso e.RowIndex >= 0 Then
            SelectRowOnRightClick(e.RowIndex)
        End If
    End Sub

    Private Sub SelectRowOnRightClick(rowIndex As Integer)
        dgvTampil.ClearSelection()
        dgvTampil.Rows(rowIndex).Selected = True
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        ShowSearchPanel()
    End Sub

    Private Sub ShowSearchPanel()
        pnCari.Visible = True
        txtCari.Clear()
        txtCari.Focus()
        LoadAllProducts()
    End Sub

    Private Sub btnTutup_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        pnCari.Visible = False
    End Sub

    Private Sub txtCari_TextChanged(sender As Object, e As EventArgs) Handles txtCari.TextChanged
        If String.IsNullOrWhiteSpace(txtCari.Text) Then
            dgvCari.DataSource = Nothing
            Return
        End If
        SearchProducts(txtCari.Text.Trim())
    End Sub

    Private Sub SearchProducts(searchTerm As String)
        Try
            BukaKoneksi()
            Dim query As String = "SELECT kode_barang, nama_barang, satuan FROM barang WHERE nama_barang LIKE @cari OR kode_barang LIKE @cari"
            Using adapter As New MySqlDataAdapter(query, conn)
                adapter.SelectCommand.Parameters.AddWithValue("@cari", $"%{searchTerm}%")
                Dim dataTable As New DataTable
                adapter.Fill(dataTable)

                If Not dataTable.Columns.Contains("jumlah") Then
                    dataTable.Columns.Add("jumlah", GetType(Integer))
                End If

                For Each row As DataRow In dataTable.Rows
                    row("jumlah") = GetAvailableStock(row("kode_barang").ToString())
                Next

                dgvCari.DataSource = dataTable
                If dgvCari.Columns.Contains("kode_barang") Then dgvCari.Columns("kode_barang").HeaderText = "Kode Barang"
                If dgvCari.Columns.Contains("nama_barang") Then dgvCari.Columns("nama_barang").HeaderText = "Nama Barang"
                If dgvCari.Columns.Contains("satuan") Then dgvCari.Columns("satuan").HeaderText = "Satuan"
                If dgvCari.Columns.Contains("jumlah") Then dgvCari.Columns("jumlah").HeaderText = "Jumlah"
            End Using
        Catch ex As Exception
            ShowErrorMessage("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub dgvCari_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCari.CellClick
        If e.RowIndex >= 0 Then
            SelectProductFromSearch(e.RowIndex)
        End If
    End Sub

    Private Sub SelectProductFromSearch(rowIndex As Integer)
        With dgvCari.Rows(rowIndex)
            txtKode.Text = .Cells("kode_barang").Value.ToString()
            txtBarang.Text = .Cells("nama_barang").Value.ToString()
            txtSatuan.Text = .Cells("satuan").Value.ToString()
        End With
        txtJumlah.Focus()
        pnCari.Visible = False
    End Sub

    Private Sub LoadAllProducts()
        Try
            BukaKoneksi()
            Dim query As String = "SELECT kode_barang, nama_barang, satuan FROM barang"
            Using adapter As New MySqlDataAdapter(query, conn)
                Dim dataTable As New DataTable
                adapter.Fill(dataTable)

                If Not dataTable.Columns.Contains("jumlah") Then
                    dataTable.Columns.Add("jumlah", GetType(Integer))
                End If

                For Each row As DataRow In dataTable.Rows
                    row("jumlah") = GetAvailableStock(row("kode_barang").ToString())
                Next

                dgvCari.DataSource = dataTable

                If dgvCari.Columns.Contains("kode_barang") Then dgvCari.Columns("kode_barang").HeaderText = "Kode Barang"
                If dgvCari.Columns.Contains("nama_barang") Then dgvCari.Columns("nama_barang").HeaderText = "Nama Barang"
                If dgvCari.Columns.Contains("satuan") Then dgvCari.Columns("satuan").HeaderText = "Satuan"
                If dgvCari.Columns.Contains("jumlah") Then dgvCari.Columns("jumlah").HeaderText = "Jumlah"
            End Using
        Catch ex As Exception
            ShowErrorMessage("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub txtTunai_TextChanged(sender As Object, e As EventArgs) Handles txtTunai.TextChanged
        CalculateChange()
    End Sub

    Private Sub CalculateChange()
        Dim totalAmount As Decimal = GetTotalAmount()
        Dim cashAmount As Decimal = GetCashAmount()

        If IsValidPayment(cashAmount, totalAmount) Then
            txtKembalian.Text = String.Format(CURRENCY_FORMAT, cashAmount - totalAmount)
        Else
            txtKembalian.Text = ""
        End If
    End Sub

    Private Function GetTotalAmount() As Decimal
        Dim amount As Decimal
        Decimal.TryParse(txtTotalHarga.Text.Replace("Rp", "").Replace(".", "").Trim(), amount)
        Return amount
    End Function

    Private Function GetCashAmount() As Decimal
        Dim amount As Decimal
        Decimal.TryParse(txtTunai.Text, amount)
        Return amount
    End Function

    Private Function IsValidPayment(cashAmount As Decimal, totalAmount As Decimal) As Boolean
        Return cashAmount >= totalAmount AndAlso totalAmount > 0
    End Function

    Private Sub txtTunai_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTunai.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnBayar_Click(sender As Object, e As EventArgs) Handles btnBayar.Click
        If String.IsNullOrWhiteSpace(txtTunai.Text) Then
            MessageBox.Show("Silakan masukkan jumlah uang tunai terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtTunai.Focus()
            Return
        End If

        Dim tunai As Decimal
        If Not Decimal.TryParse(txtTunai.Text, tunai) Then
            MessageBox.Show("Jumlah uang tunai harus berupa angka yang valid!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtTunai.Focus()
            txtTunai.SelectAll()
            Return
        End If

        Dim totalBayar As Decimal = Convert.ToDecimal(lblHarga.Text.Replace("Rp", "").Replace(",", "").Trim())
        If tunai < totalBayar Then
            MessageBox.Show(
                $"Jumlah uang tunai tidak mencukupi!{Environment.NewLine}Total: Rp {totalBayar:N0}{Environment.NewLine}Tunai: Rp {tunai:N0}",
                "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtTunai.Focus()
            txtTunai.SelectAll()
            Return
        End If

        SimpanTransaksiKeluar()
        PrintNota()
        dgvTampil.Rows.Clear()
        UpdateTotalAmount()
        ClearPaymentIfNoItems()
    End Sub

    Sub PrintNota()
        If PrinterSettings.InstalledPrinters.Cast(Of String)().Contains("Printer") Then
            nota.PrinterSettings.PrinterName = "Printer"
            nota.Print()
        Else
            Dim previewDialog As New PrintPreviewDialog()
            previewDialog.Document = nota
            previewDialog.WindowState = FormWindowState.Maximized
            previewDialog.ShowDialog()
        End If
    End Sub

    Private Sub nota_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles nota.PrintPage
        Dim fontHeader As New Font("Arial", 16, FontStyle.Bold)
        Dim fontSubHeader As New Font("Arial", 10, FontStyle.Regular)
        Dim fontBold As New Font("Arial", 10, FontStyle.Bold)
        Dim fontNormal As New Font("Arial", 9, FontStyle.Regular)
        Dim fontSmall As New Font("Arial", 8, FontStyle.Regular)

        Dim blackBrush As New SolidBrush(Color.Black)
        Dim blueBrush As New SolidBrush(Color.FromArgb(52, 73, 94))
        Dim grayBrush As New SolidBrush(Color.FromArgb(127, 140, 141))
        Dim blackPen As New Pen(Color.Black, 1)
        Dim bluePen As New Pen(Color.FromArgb(52, 152, 219), 2)

        Dim x As Integer = 50
        Dim y As Integer = 50
        Dim lineHeight As Integer = 20
        Dim pageWidth As Integer = e.PageBounds.Width - 100
        Dim leftMargin As Integer = 50
        Dim rightMargin As Integer = e.PageBounds.Width - 50

        Dim headerRect As New Rectangle(x - 10, y - 10, pageWidth + 20, 80)
        Dim headerBrush As New SolidBrush(Color.FromArgb(240, 243, 244))
        e.Graphics.FillRectangle(headerBrush, headerRect)
        e.Graphics.DrawRectangle(bluePen, headerRect)

        Dim companyName As String = "Nur Firmansyah Zamani"
        Dim companySize As SizeF = e.Graphics.MeasureString(companyName, fontHeader)
        Dim centerX As Single = CSng((pageWidth / 2) - (companySize.Width / 2) + x)
        e.Graphics.DrawString(companyName, fontHeader, blueBrush, centerX, CSng(y))
        y += 25

        Dim phoneInfo As String = "No. Tlp: 123"
        Dim addressInfo As String = "Jl. Jalan"
        Dim phoneSize As SizeF = e.Graphics.MeasureString(phoneInfo, fontSubHeader)
        Dim addressSize As SizeF = e.Graphics.MeasureString(addressInfo, fontSubHeader)

        e.Graphics.DrawString(phoneInfo, fontSubHeader, grayBrush, CSng((pageWidth / 2) - (phoneSize.Width / 2) + x), CSng(y))
        y += 15
        e.Graphics.DrawString(addressInfo, fontSubHeader, grayBrush, CSng((pageWidth / 2) - (addressSize.Width / 2) + x), CSng(y))
        y += 35

        e.Graphics.DrawString("DETAIL PEMBELIAN", fontBold, blueBrush, CSng(x), CSng(y))
        e.Graphics.DrawLine(bluePen, CSng(x), CSng(y + 15), CSng(x + 150), CSng(y + 15))
        y += 30

        Dim tableHeaderRect As New Rectangle(x, y, pageWidth, 25)
        Dim tableHeaderBrush As New SolidBrush(Color.FromArgb(236, 240, 241))
        e.Graphics.FillRectangle(tableHeaderBrush, tableHeaderRect)
        e.Graphics.DrawRectangle(blackPen, tableHeaderRect)

        e.Graphics.DrawString("BARANG", fontBold, blackBrush, CSng(x + 5), CSng(y + 5))
        e.Graphics.DrawString("QTY", fontBold, blackBrush, CSng(x + 200), CSng(y + 5))
        e.Graphics.DrawString("TOTAL", fontBold, blackBrush, CSng(x + 270), CSng(y + 5))

        e.Graphics.DrawLine(blackPen, CSng(x + 190), CSng(y), CSng(x + 190), CSng(y + 25))
        e.Graphics.DrawLine(blackPen, CSng(x + 260), CSng(y), CSng(x + 260), CSng(y + 25))

        y += 25

        Dim rowCount As Integer = 0
        For Each row As DataGridViewRow In dgvTampil.Rows
            If Not row.IsNewRow Then
                rowCount += 1

                Dim rowRect As New Rectangle(x, y, pageWidth, 20)
                If rowCount Mod 2 = 0 Then
                    Dim altRowBrush As New SolidBrush(Color.FromArgb(248, 249, 250))
                    e.Graphics.FillRectangle(altRowBrush, rowRect)
                    altRowBrush.Dispose()
                End If

                e.Graphics.DrawRectangle(blackPen, rowRect)

                Dim namaBarang As String = row.Cells(1).Value.ToString()
                Dim qty As String = row.Cells("Qty").Value.ToString()
                Dim totalItem As String = FormatCurrency(row.Cells("Total").Value)

                e.Graphics.DrawString(namaBarang, fontNormal, blackBrush, CSng(x + 5), CSng(y + 3))
                e.Graphics.DrawString(qty, fontNormal, blackBrush, CSng(x + 195), CSng(y + 3))
                e.Graphics.DrawString(totalItem, fontNormal, blackBrush, CSng(x + 265), CSng(y + 3))

                e.Graphics.DrawLine(blackPen, CSng(x + 190), CSng(y), CSng(x + 190), CSng(y + 20))
                e.Graphics.DrawLine(blackPen, CSng(x + 260), CSng(y), CSng(x + 260), CSng(y + 20))

                y += 20
            End If
        Next

        y += 20

        Dim summaryRect As New Rectangle(x + 150, y, pageWidth - 150, 80)
        Dim summaryBrush As New SolidBrush(Color.FromArgb(248, 249, 250))
        e.Graphics.FillRectangle(summaryBrush, summaryRect)
        e.Graphics.DrawRectangle(bluePen, summaryRect)

        y += 10
        e.Graphics.DrawString("RINGKASAN", fontBold, blueBrush, CSng(x + 160), CSng(y))
        y += 20

        e.Graphics.DrawString("Total:", fontBold, blackBrush, CSng(x + 160), CSng(y))
        e.Graphics.DrawString(FormatCurrency(txtTotalHarga.Text), fontBold, blackBrush, CSng(x + 250), CSng(y))
        y += 15

        e.Graphics.DrawString("Tunai:", fontNormal, blackBrush, CSng(x + 160), CSng(y))
        Dim tunaiValue As Decimal
        If Decimal.TryParse(txtTunai.Text, tunaiValue) Then
            e.Graphics.DrawString(FormatCurrency(tunaiValue), fontNormal, blackBrush, CSng(x + 250), CSng(y))
        Else
            e.Graphics.DrawString(txtTunai.Text, fontNormal, blackBrush, CSng(x + 250), CSng(y))
        End If
        y += 15

        e.Graphics.DrawString("Kembali:", fontNormal, blackBrush, CSng(x + 160), CSng(y))
        e.Graphics.DrawString(FormatCurrency(txtKembalian.Text), fontNormal, blackBrush, CSng(x + 250), CSng(y))
        y += 30

        Dim dashPen As New Pen(Color.Gray, 1)
        dashPen.DashStyle = Drawing2D.DashStyle.Dash
        e.Graphics.DrawLine(dashPen, CSng(x), CSng(y), CSng(rightMargin - 50), CSng(y))
        y += 20

        Dim thankYouMsg As String = "Terimakasih...!!!"
        Dim thankYouSize As SizeF = e.Graphics.MeasureString(thankYouMsg, fontBold)
        Dim greenBrush As New SolidBrush(Color.FromArgb(39, 174, 96))
        e.Graphics.DrawString(thankYouMsg, fontBold, greenBrush, CSng((pageWidth / 2) - (thankYouSize.Width / 2) + x), CSng(y))
        y += 20

        Dim footerNote As String = "Semoga harimu menyenangkan!"
        Dim footerSize As SizeF = e.Graphics.MeasureString(footerNote, fontSmall)
        e.Graphics.DrawString(footerNote, fontSmall, grayBrush, CSng((pageWidth / 2) - (footerSize.Width / 2) + x), CSng(y))

        fontHeader.Dispose()
        fontSubHeader.Dispose()
        fontBold.Dispose()
        fontNormal.Dispose()
        fontSmall.Dispose()
        blackBrush.Dispose()
        blueBrush.Dispose()
        grayBrush.Dispose()
        greenBrush.Dispose()
        blackPen.Dispose()
        bluePen.Dispose()
        dashPen.Dispose()
        headerBrush.Dispose()
        tableHeaderBrush.Dispose()
        summaryBrush.Dispose()
    End Sub

    Private Function FormatCurrency(value As Object) As String
        Try
            Dim numValue As Decimal = Convert.ToDecimal(value)
            Return "Rp " & numValue.ToString("N0")
        Catch
            Return value.ToString()
        End Try
    End Function

    Private Sub SimpanTransaksiKeluar()
        Try
            BukaKoneksi()
            For Each row As DataGridViewRow In dgvTampil.Rows
                If row.IsNewRow Then Continue For

                Dim kodeBarang As String = row.Cells("Kode").Value.ToString()
                Dim namaBarang As String = row.Cells("Barang").Value.ToString()
                Dim satuan As String = row.Cells("Satuan").Value.ToString()
                Dim hargaSatuan As Integer = Convert.ToInt32(row.Cells("Harga").Value)
                Dim qty As Integer = Convert.ToInt32(row.Cells("Qty").Value)
                Dim total As Integer = Convert.ToInt32(row.Cells("Total").Value)
                Dim tanggal As Date = Date.Now

                Dim query As String = "INSERT INTO transaksi_keluar (kode_barang, nama_barang, satuan, harga_satuan, qty, total, tanggal) " &
                                  "VALUES (@kode, @nama, @satuan, @harga, @qty, @total, @tanggal)"
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@kode", kodeBarang)
                    cmd.Parameters.AddWithValue("@nama", namaBarang)
                    cmd.Parameters.AddWithValue("@satuan", satuan)
                    cmd.Parameters.AddWithValue("@harga", hargaSatuan)
                    cmd.Parameters.AddWithValue("@qty", qty)
                    cmd.Parameters.AddWithValue("@total", total)
                    cmd.Parameters.AddWithValue("@tanggal", tanggal)
                    cmd.ExecuteNonQuery()
                End Using
            Next
        Catch ex As Exception
            ShowErrorMessage("Gagal menyimpan transaksi keluar: " & ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Private Sub btnMenu_Click(sender As Object, e As EventArgs) Handles btnMenu.Click
        NavigateToMenu()
    End Sub

    Private Sub NavigateToMenu()
        Dim menuForm As New FR_MENU
        menuForm.Show()
        Me.Close()
    End Sub

    Private Sub btnMinimize_Click(sender As Object, e As EventArgs) Handles btnMinimize.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub ShowErrorMessage(message As String)
        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Sub ShowWarningMessage(message As String)
        MessageBox.Show(message, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    Private Sub ShowInfoMessage(message As String)
        MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

End Class