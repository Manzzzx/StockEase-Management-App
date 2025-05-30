Imports System.Drawing.Printing
Imports MySql.Data.MySqlClient

Public Class FR_LAPORAN
    Dim conn As MySqlConnection
    Dim da As MySqlDataAdapter
    Dim ds As DataSet
    Dim cmd As MySqlCommand
    Dim dr As MySqlDataReader
    Dim strConn As String = "server=localhost;user id=root;password=;database=vbnet_projectuas;"

    Private Sub FR_LAPORAN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conn = New MySqlConnection(strConn)
        IsiComboBoxLaporan()
        AturRangeTanggal()
        AddHandler dgvtampil.CellFormatting, AddressOf dgvtampil_CellFormatting
    End Sub

    Private Sub IsiComboBoxLaporan()
        cbLaporan.Items.Clear()
        cbLaporan.Items.Add("Barang Masuk")
        cbLaporan.Items.Add("Barang Keluar")
        cbLaporan.Items.Add("Semua Transaksi")
        cbLaporan.SelectedIndex = 0
    End Sub

    Private Sub TambahKolomNoUrut()
        If dgvtampil.Columns.Contains("No") Then
            dgvtampil.Columns.Remove("No")
        End If

        Dim colNo As New DataGridViewTextBoxColumn()
        colNo.Name = "No"
        colNo.HeaderText = "No"
        colNo.Width = 50
        colNo.ReadOnly = True
        dgvtampil.Columns.Insert(0, colNo)

        For i As Integer = 0 To dgvtampil.Rows.Count - 1
            If Not dgvtampil.Rows(i).IsNewRow Then
                dgvtampil.Rows(i).Cells("No").Value = i + 1
            End If
        Next
    End Sub

    Private Sub dgvtampil_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If dgvtampil.Columns(e.ColumnIndex).HeaderText.ToLower().Contains("jumlah") OrElse
       dgvtampil.Columns(e.ColumnIndex).HeaderText.ToLower().Contains("qty") OrElse
       dgvtampil.Columns(e.ColumnIndex).HeaderText.ToLower().Contains("harga") OrElse
       dgvtampil.Columns(e.ColumnIndex).HeaderText.ToLower().Contains("total") Then

            If e.Value IsNot Nothing AndAlso IsNumeric(e.Value) Then
                e.Value = FormatNumber(Convert.ToDouble(e.Value), 0, TriState.False, TriState.False, TriState.True)
                e.FormattingApplied = True
            End If
        End If
    End Sub

    Private Sub btnTampil_Click(sender As Object, e As EventArgs) Handles btnTampil.Click
        If cbLaporan.SelectedIndex = -1 Then
            MessageBox.Show("Pilih jenis laporan terlebih dahulu!")
            Return
        End If

        If tglMulai.Value > tglSampai.Value Then
            MessageBox.Show("Tanggal mulai tidak boleh lebih besar dari tanggal sampai!")
            Return
        End If

        Select Case cbLaporan.SelectedItem.ToString()
            Case "Barang Masuk"
                TampilkanBarangMasuk()
            Case "Barang Keluar"
                TampilkanBarangKeluar()
            Case "Semua Transaksi"
                TampilkanSemuaTransaksi()
        End Select
    End Sub

    Private Sub TampilkanBarangMasuk()
        Try
            If conn.State = ConnectionState.Open Then conn.Close()
            conn.Open()

            Dim query As String = "SELECT kode_barang, nama_barang, satuan, jumlah, suplier, harga_partai, tanggal_masuk " &
                                "FROM transaksi_masuk " &
                                "WHERE tanggal_masuk BETWEEN @tgl1 AND @tgl2 " &
                                "ORDER BY tanggal_masuk ASC"

            da = New MySqlDataAdapter(query, conn)
            da.SelectCommand.Parameters.AddWithValue("@tgl1", tglMulai.Value.Date)
            da.SelectCommand.Parameters.AddWithValue("@tgl2", tglSampai.Value.Date.AddDays(1).AddSeconds(-1))

            ds = New DataSet()
            da.Fill(ds, "data")

            If ds.Tables("data").Rows.Count > 0 Then
                dgvtampil.DataSource = ds.Tables("data")
                AturHeaderBarangMasuk()
                TambahKolomNoUrut()
            Else
                dgvtampil.DataSource = Nothing
                MessageBox.Show("Tidak ada data barang masuk pada rentang tanggal tersebut.")
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal menampilkan data barang masuk: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub TampilkanBarangKeluar()
        Try
            If conn.State = ConnectionState.Open Then conn.Close()
            conn.Open()

            Dim query As String = "SELECT kode_barang, nama_barang, satuan, harga_satuan, qty, total, tanggal " &
                                "FROM transaksi_keluar " &
                                "WHERE tanggal BETWEEN @tgl1 AND @tgl2 " &
                                "ORDER BY tanggal ASC"

            da = New MySqlDataAdapter(query, conn)
            da.SelectCommand.Parameters.AddWithValue("@tgl1", tglMulai.Value.Date)
            da.SelectCommand.Parameters.AddWithValue("@tgl2", tglSampai.Value.Date.AddDays(1).AddSeconds(-1))

            ds = New DataSet()
            da.Fill(ds, "data")

            If ds.Tables("data").Rows.Count > 0 Then
                dgvtampil.DataSource = ds.Tables("data")
                AturHeaderBarangKeluar()
                TambahKolomNoUrut()
            Else
                dgvtampil.DataSource = Nothing
                MessageBox.Show("Tidak ada data barang keluar pada rentang tanggal tersebut.")
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal menampilkan data barang keluar: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub TampilkanSemuaTransaksi()
        Try
            If conn.State = ConnectionState.Open Then conn.Close()
            conn.Open()

            Dim query As String = "SELECT kode_barang, nama_barang, satuan, jumlah AS qty, suplier AS keterangan, harga_partai AS harga, tanggal_masuk AS tanggal, 'MASUK' AS jenis " &
                                 "FROM transaksi_masuk WHERE tanggal_masuk BETWEEN @tgl1 AND @tgl2 " &
                                 "UNION ALL " &
                                 "SELECT kode_barang, nama_barang, satuan, qty, CONCAT('Customer') AS keterangan, total AS harga, tanggal, 'KELUAR' AS jenis " &
                                 "FROM transaksi_keluar WHERE tanggal BETWEEN @tgl1 AND @tgl2 " &
                                 "ORDER BY tanggal ASC"

            da = New MySqlDataAdapter(query, conn)
            da.SelectCommand.Parameters.AddWithValue("@tgl1", tglMulai.Value.Date)
            da.SelectCommand.Parameters.AddWithValue("@tgl2", tglSampai.Value.Date.AddDays(1).AddSeconds(-1))

            ds = New DataSet()
            da.Fill(ds, "data")

            If ds.Tables("data").Rows.Count > 0 Then
                dgvtampil.DataSource = ds.Tables("data")
                AturHeaderSemuaTransaksi()
                TambahKolomNoUrut()
            Else
                dgvtampil.DataSource = Nothing
                MessageBox.Show("Tidak ada data transaksi pada rentang tanggal tersebut.")
            End If

        Catch ex As Exception
            MessageBox.Show("Gagal menampilkan semua transaksi: " & ex.Message)
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private Sub AturHeaderBarangMasuk()
        If dgvtampil.Columns.Count >= 8 Then
            dgvtampil.Columns(1).HeaderText = "Kode Barang"
            dgvtampil.Columns(2).HeaderText = "Nama Barang"
            dgvtampil.Columns(3).HeaderText = "Satuan"
            dgvtampil.Columns(4).HeaderText = "Jumlah"
            dgvtampil.Columns(5).HeaderText = "Supplier"
            dgvtampil.Columns(6).HeaderText = "Harga Partai"
            dgvtampil.Columns(7).HeaderText = "Tanggal Masuk"

            dgvtampil.Columns(1).Width = 40
            dgvtampil.Columns(2).Width = 150
            dgvtampil.Columns(3).Width = 60
            dgvtampil.Columns(4).Width = 80
            dgvtampil.Columns(5).Width = 120
            dgvtampil.Columns(6).Width = 100
            dgvtampil.Columns(7).Width = 120
        End If
    End Sub

    Private Sub AturHeaderBarangKeluar()
        If dgvtampil.Columns.Count >= 8 Then
            dgvtampil.Columns(1).HeaderText = "Kode Barang"
            dgvtampil.Columns(2).HeaderText = "Nama Barang"
            dgvtampil.Columns(3).HeaderText = "Satuan"
            dgvtampil.Columns(4).HeaderText = "Harga Satuan"
            dgvtampil.Columns(5).HeaderText = "Qty"
            dgvtampil.Columns(6).HeaderText = "Total"
            dgvtampil.Columns(7).HeaderText = "Tanggal Keluar"

            dgvtampil.Columns(1).Width = 40
            dgvtampil.Columns(2).Width = 150
            dgvtampil.Columns(3).Width = 60
            dgvtampil.Columns(4).Width = 100
            dgvtampil.Columns(5).Width = 40
            dgvtampil.Columns(6).Width = 100
            dgvtampil.Columns(7).Width = 120
        End If
    End Sub

    Private Sub AturHeaderSemuaTransaksi()
        If dgvtampil.Columns.Count >= 9 Then
            dgvtampil.Columns(1).HeaderText = "Kode Barang"
            dgvtampil.Columns(2).HeaderText = "Nama Barang"
            dgvtampil.Columns(3).HeaderText = "Satuan"
            dgvtampil.Columns(4).HeaderText = "Qty"
            dgvtampil.Columns(5).HeaderText = "Keterangan"
            dgvtampil.Columns(6).HeaderText = "Harga"
            dgvtampil.Columns(7).HeaderText = "Tanggal"
            dgvtampil.Columns(8).HeaderText = "Jenis"

            dgvtampil.Columns(1).Width = 40
            dgvtampil.Columns(2).Width = 150
            dgvtampil.Columns(3).Width = 60
            dgvtampil.Columns(4).Width = 40
            dgvtampil.Columns(5).Width = 120
            dgvtampil.Columns(6).Width = 100
            dgvtampil.Columns(7).Width = 120
            dgvtampil.Columns(8).Width = 80
        End If
    End Sub

    Sub AturRangeTanggal()
        Try
            If conn.State = ConnectionState.Open Then conn.Close()
            conn.Open()

            Dim minDate As Date = Date.Today.AddYears(-1)
            Dim maxDate As Date = Date.Today

            cmd = New MySqlCommand("SELECT MIN(tanggal_masuk), MAX(tanggal_masuk) FROM transaksi_masuk", conn)
            dr = cmd.ExecuteReader()
            If dr.Read() Then
                If Not IsDBNull(dr(0)) Then minDate = dr.GetDateTime(0)
                If Not IsDBNull(dr(1)) Then maxDate = dr.GetDateTime(1)
            End If
            dr.Close()

            cmd = New MySqlCommand("SELECT MIN(tanggal), MAX(tanggal) FROM transaksi_keluar", conn)
            dr = cmd.ExecuteReader()
            If dr.Read() Then
                If Not IsDBNull(dr(0)) AndAlso dr.GetDateTime(0) < minDate Then
                    minDate = dr.GetDateTime(0)
                End If
                If Not IsDBNull(dr(1)) AndAlso dr.GetDateTime(1) > maxDate Then
                    maxDate = dr.GetDateTime(1)
                End If
            End If
            dr.Close()

            tglMulai.MinDate = minDate
            tglMulai.MaxDate = maxDate
            tglSampai.MinDate = minDate
            tglSampai.MaxDate = maxDate

            tglMulai.Value = New Date(Date.Today.Year, Date.Today.Month, 1)
            tglSampai.Value = Date.Today

        Catch ex As Exception
            MessageBox.Show("Gagal mengatur range tanggal: " & ex.Message)
            tglMulai.Value = Date.Today.AddMonths(-1)
            tglSampai.Value = Date.Today
        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
        End Try
    End Sub

    Private WithEvents printDocument As New PrintDocument()
    Private WithEvents PrintPreviewDialog As New PrintPreviewDialog()
    Private currentPrintRow As Integer = 0
    Private reportTitle As String = ""
    Private reportSubTitle As String = ""

    Private Sub SetReportTitle(title As String, Optional subTitle As String = "")
        reportTitle = title
        reportSubTitle = subTitle
    End Sub

    Private Sub btnCetak_Click(sender As Object, e As EventArgs) Handles btnCetak.Click

        If dgvtampil.DataSource Is Nothing OrElse dgvtampil.Rows.Count = 0 Then
            MessageBox.Show("Tidak ada data untuk dicetak!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            currentPrintRow = 0

            SetReportTitle(
                "LAPORAN " & cbLaporan.SelectedItem.ToString().ToUpper(),
                "Periode: " & tglMulai.Value.ToString("dd/MM/yyyy") & " s/d " & tglSampai.Value.ToString("dd/MM/yyyy")
            )

            With printDocument
                .DocumentName = "Laporan Data " & DateTime.Now.ToString("dd-MM-yyyy")
                .DefaultPageSettings.Landscape = False
                .DefaultPageSettings.Margins = New Margins(50, 50, 80, 50)
            End With

            With PrintPreviewDialog
                .Document = printDocument
                .WindowState = FormWindowState.Maximized
                .Text = "Preview Cetak - " & reportTitle
                .UseAntiAlias = True
                .ShowDialog()
            End With

        Catch ex As Exception
            MessageBox.Show("Error saat menyiapkan pencetakan: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub printDocument_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles printDocument.PrintPage
        Try
            Dim fontTitle As New Font("Arial", 14, FontStyle.Bold)
            Dim fontHeader As New Font("Arial", 10, FontStyle.Bold)
            Dim fontCell As New Font("Arial", 9)
            Dim fontFooter As New Font("Arial", 8)
            Dim brush As New SolidBrush(Color.Black)
            Dim grayBrush As New SolidBrush(Color.Gray)

            Dim leftMargin As Integer = e.MarginBounds.Left
            Dim topMargin As Integer = e.MarginBounds.Top
            Dim rightMargin As Integer = e.MarginBounds.Right
            Dim bottomMargin As Integer = e.MarginBounds.Bottom
            Dim pageWidth As Integer = e.MarginBounds.Width

            Dim titleHeight As Integer = 30
            Dim headerHeight As Integer = 25
            Dim rowHeight As Integer = 22
            Dim footerHeight As Integer = 20

            Dim x As Integer = leftMargin
            Dim y As Integer = topMargin

            If Not String.IsNullOrEmpty(reportTitle) Then
                Dim titleRect As New Rectangle(leftMargin, y, pageWidth, titleHeight)
                Dim titleFormat As New StringFormat()
                titleFormat.Alignment = StringAlignment.Center
                titleFormat.LineAlignment = StringAlignment.Center

                e.Graphics.DrawString(reportTitle.ToUpper(), fontTitle, brush, titleRect, titleFormat)
                y += titleHeight
            End If

            If Not String.IsNullOrEmpty(reportSubTitle) Then
                Dim subTitleRect As New Rectangle(leftMargin, y, pageWidth, 20)
                Dim subTitleFormat As New StringFormat()
                subTitleFormat.Alignment = StringAlignment.Center
                subTitleFormat.LineAlignment = StringAlignment.Center

                e.Graphics.DrawString(reportSubTitle, fontHeader, brush, subTitleRect, subTitleFormat)
                y += 25
            End If

            e.Graphics.DrawString("Tanggal Cetak: " & DateTime.Now.ToString("dd MMMM yyyy HH:mm"), fontFooter, grayBrush, leftMargin, y)
            e.Graphics.DrawLine(Pens.Black, leftMargin, y + 15, rightMargin, y + 15)
            y += titleHeight + 30

            Dim colWidths(dgvtampil.Columns.Count - 1) As Integer
            Dim totalWidth As Integer = 0

            For i As Integer = 0 To dgvtampil.Columns.Count - 1
                If dgvtampil.Columns(i).Visible Then
                    Dim maxWidth As Integer = CInt(e.Graphics.MeasureString(dgvtampil.Columns(i).HeaderText, fontHeader).Width) + 16

                    For j As Integer = 0 To dgvtampil.Rows.Count - 1
                        Dim cellValue As String = ""
                        If dgvtampil.Rows(j).Cells(i).Value IsNot Nothing Then
                            cellValue = dgvtampil.Rows(j).Cells(i).Value.ToString()
                        End If
                        Dim cellWidth As Integer = CInt(e.Graphics.MeasureString(cellValue, fontCell).Width) + 16
                        If cellWidth > maxWidth Then maxWidth = cellWidth
                    Next

                    colWidths(i) = maxWidth
                    totalWidth += maxWidth
                Else
                    colWidths(i) = 0
                End If
            Next

            If totalWidth > pageWidth Then
                Dim scale As Double = pageWidth / totalWidth
                For i As Integer = 0 To colWidths.Length - 1
                    colWidths(i) = CInt(colWidths(i) * scale)
                Next
            End If

            x = leftMargin
            For i As Integer = 0 To dgvtampil.Columns.Count - 1
                If dgvtampil.Columns(i).Visible AndAlso colWidths(i) > 0 Then
                    e.Graphics.FillRectangle(New SolidBrush(Color.LightGray), x, y, colWidths(i), headerHeight)
                    e.Graphics.DrawRectangle(Pens.Black, x, y, colWidths(i), headerHeight)

                    Dim headerRect As New Rectangle(x + 2, y + 2, colWidths(i) - 4, headerHeight - 4)
                    Dim headerFormat As New StringFormat()
                    headerFormat.Alignment = StringAlignment.Center
                    headerFormat.LineAlignment = StringAlignment.Center
                    headerFormat.Trimming = StringTrimming.EllipsisCharacter

                    e.Graphics.DrawString(dgvtampil.Columns(i).HeaderText, fontHeader, brush, headerRect, headerFormat)
                    x += colWidths(i)
                End If
            Next

            y += headerHeight

            Dim rowCount As Integer = 0
            While currentPrintRow < dgvtampil.Rows.Count AndAlso y + rowHeight < bottomMargin - footerHeight
                Dim row = dgvtampil.Rows(currentPrintRow)

                If Not row.IsNewRow Then
                    x = leftMargin
                    Dim rowBrush As Brush = If(rowCount Mod 2 = 0, Brushes.White, New SolidBrush(Color.FromArgb(245, 245, 245)))
                    e.Graphics.FillRectangle(rowBrush, leftMargin, y, pageWidth, rowHeight)

                    For i As Integer = 0 To dgvtampil.Columns.Count - 1
                        If dgvtampil.Columns(i).Visible AndAlso colWidths(i) > 0 Then
                            e.Graphics.DrawRectangle(Pens.Black, x, y, colWidths(i), rowHeight)
                            Dim cellValue As String = If(row.Cells(i).Value IsNot Nothing, row.Cells(i).Value.ToString().Trim(), "")

                            Dim headerText As String = dgvtampil.Columns(i).HeaderText.ToLower()
                            If (headerText.Contains("tanggal")) AndAlso Not String.IsNullOrEmpty(cellValue) Then
                                Dim dt As DateTime
                                If DateTime.TryParse(cellValue, dt) Then
                                    cellValue = dt.ToString("dd/MM/yyyy")
                                End If
                            End If

                            If Not String.IsNullOrEmpty(cellValue) Then
                                Dim cellRect As New Rectangle(x + 3, y + 2, colWidths(i) - 6, rowHeight - 4)
                                Dim cellFormat As New StringFormat()
                                cellFormat.LineAlignment = StringAlignment.Center
                                cellFormat.Trimming = StringTrimming.EllipsisCharacter
                                If IsNumeric(cellValue) AndAlso Not headerText.Contains("kode") Then
                                    cellFormat.Alignment = StringAlignment.Far
                                Else
                                    cellFormat.Alignment = StringAlignment.Near
                                End If
                                e.Graphics.DrawString(cellValue, fontCell, brush, cellRect, cellFormat)
                            End If
                            x += colWidths(i)
                        End If
                    Next

                    y += rowHeight
                    rowCount += 1
                End If

                currentPrintRow += 1
            End While

            Dim pageInfo As String = $"Halaman {(currentPrintRow \ 30) + 1} - Total Data: {dgvtampil.Rows.Count - 1}"
            e.Graphics.DrawString(pageInfo, fontFooter, grayBrush, leftMargin, bottomMargin - footerHeight)

            Dim printTime As String = "Dicetak pada: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
            Dim printTimeSize = e.Graphics.MeasureString(printTime, fontFooter)
            e.Graphics.DrawString(printTime, fontFooter, grayBrush, rightMargin - printTimeSize.Width, bottomMargin - footerHeight)

            If currentPrintRow < dgvtampil.Rows.Count Then
                e.HasMorePages = True
            Else
                e.HasMorePages = False
                currentPrintRow = 0
            End If

            fontTitle.Dispose()
            fontHeader.Dispose()
            fontCell.Dispose()
            fontFooter.Dispose()
            brush.Dispose()
            grayBrush.Dispose()

        Catch ex As Exception
            MessageBox.Show("Error saat mencetak: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            e.HasMorePages = False
        End Try
    End Sub

    Private Sub btnCetakLangsung_Click(sender As Object, e As EventArgs) Handles btnCetak.Click
        If dgvtampil.DataSource Is Nothing OrElse dgvtampil.Rows.Count = 0 Then
            MessageBox.Show("Tidak ada data untuk dicetak!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            currentPrintRow = 0
            printDocument.Print()
            MessageBox.Show("Dokumen berhasil dicetak!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Error saat mencetak: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class