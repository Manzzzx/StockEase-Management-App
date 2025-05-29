Imports MySql.Data.MySqlClient

Public Class FR_KELUAR
    Private Const DEFAULT_KODE_PREFIX As String = "BRG0"
    Private Const CURRENCY_FORMAT As String = "Rp {0:N0}"

    '===============[ Form Events ]===============
    Private Sub FR_KELUAR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeEventHandlers()
        ResetProductInput()
    End Sub

    Private Sub InitializeEventHandlers()
        AddHandler txtJumlah.KeyDown, AddressOf txtJumlah_KeyDown
        AddHandler dgvTampil.CellEndEdit, AddressOf dgvTampil_CellEndEdit
        AddHandler dgvTampil.EditingControlShowing, AddressOf dgvTampil_EditingControlShowing
    End Sub

    '===============[ Helper Methods - UI Management ]===============
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

    '===============[ Stock Management ]===============
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
            Dim query As String = "UPDATE transaksi_masuk SET jumlah = jumlah - @qty WHERE kode_barang = @kode AND jumlah >= @qty LIMIT 1"
            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@qty", quantity)
                cmd.Parameters.AddWithValue("@kode", productCode)
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                Return rowsAffected > 0
            End Using
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

    '===============[ Product Data Management ]===============
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

    '===============[ Grid Management ]===============
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

        ' Release stock back to database
        If ReleaseStock(productCode, quantity) Then
            dgvTampil.Rows.Remove(row)
            Return True
        Else
            ShowWarningMessage("Gagal mengembalikan stok ke database!")
            Return False
        End If
    End Function

    '===============[ Validation ]===============
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

    '===============[ Event Handlers - Product Input ]===============
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

    '===============[ Event Handlers - Grid Editing ]===============
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

    '===============[ Event Handlers - Context Menu ]===============
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

    '===============[ Event Handlers - Search Panel ]===============
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
                dgvCari.DataSource = dataTable
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
                dgvCari.DataSource = dataTable
            End Using
        Catch ex As Exception
            ShowErrorMessage("Error: " & ex.Message)
        End Try
    End Sub

    '===============[ Event Handlers - Payment ]===============
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

    '===============[ Event Handlers - Navigation ]===============
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

    '===============[ Message Helpers ]===============
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