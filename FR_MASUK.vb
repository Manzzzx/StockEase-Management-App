Imports MySql.Data.MySqlClient

Public Class FR_MASUK
    Private Const DEFAULT_PAGE_SIZE As Integer = 5
    Private Const DATE_FORMAT As String = "yyyy-MM-dd HH:mm:ss"

    Private currentPage As Integer = 1
    Private totalRows As Integer = 0
    Private ReadOnly Property TotalPages As Integer
        Get
            Return Math.Ceiling(totalRows / DEFAULT_PAGE_SIZE)
        End Get
    End Property

#Region "Form Events"
    Private Sub FR_MASUK_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeDataGridView()
        LoadTransactionData()
    End Sub

    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) Handles btnKeluar.Click
        Me.Close()
        FR_MENU.Show()
    End Sub
#End Region

#Region "Data Display Methods"
    Private Sub InitializeDataGridView()
        If dgvTampil.Columns.Count > 0 Then Return

        With dgvTampil
            .Columns.Add("no", "No")
            .Columns("no").Width = 40

            .Columns.Add("id", "ID")
            .Columns("id").Visible = False

            .Columns.Add("kode_barang", "Kode")
            .Columns.Add("nama_barang", "Nama")
            .Columns.Add("satuan", "Satuan")

            .Columns.Add("jumlah", "Jumlah")
            .Columns("jumlah").DefaultCellStyle.Format = "N0"

            .Columns.Add("suplier", "Suplier")

            .Columns.Add("harga_partai", "Harga Partai")
            .Columns("harga_partai").DefaultCellStyle.Format = "N0"

            .Columns.Add("tanggal_masuk", "Tanggal Masuk")
        End With
    End Sub

    Private Sub LoadTransactionData()
        Try
            BukaKoneksi()
            LoadTotalRows()
            LoadPagedData()
            UpdatePagingInfo()
        Catch ex As Exception
            ShowErrorMessage("Gagal memuat data", ex.Message)
        End Try
    End Sub

    Private Sub LoadTotalRows()
        Using cmd As New MySqlCommand("SELECT COUNT(*) FROM transaksi_masuk", conn)
            totalRows = Convert.ToInt32(cmd.ExecuteScalar())
        End Using
    End Sub

    Private Sub LoadPagedData()
        Dim offset As Integer = (currentPage - 1) * DEFAULT_PAGE_SIZE
        Dim sql As String = "SELECT id, kode_barang, nama_barang, satuan, jumlah, suplier, harga_partai, tanggal_masuk " &
                           "FROM transaksi_masuk ORDER BY id ASC LIMIT @limit OFFSET @offset"

        Using da As New MySqlDataAdapter(sql, conn)
            da.SelectCommand.Parameters.AddWithValue("@limit", DEFAULT_PAGE_SIZE)
            da.SelectCommand.Parameters.AddWithValue("@offset", offset)

            Dim dt As New DataTable
            da.Fill(dt)
            PopulateGrid(dt)
        End Using
    End Sub

    Private Sub PopulateGrid(dt As DataTable)
        dgvTampil.Rows.Clear()
        Dim nomorUrut As Integer = (currentPage - 1) * DEFAULT_PAGE_SIZE + 1

        For Each row As DataRow In dt.Rows
            dgvTampil.Rows.Add(nomorUrut, row("id"), row("kode_barang"), row("nama_barang"),
                             row("satuan"), row("jumlah"), row("suplier"), row("harga_partai"), row("tanggal_masuk"))
            nomorUrut += 1
        Next
    End Sub

    Private Sub UpdatePagingInfo()
        lblPagingInfo.Text = currentPage & " dari " & TotalPages
    End Sub
#End Region

#Region "Item Search Methods"
    Private Sub LoadAllItems()
        Try
            BukaKoneksi()
            Using da As New MySqlDataAdapter("SELECT kode_barang, nama_barang, satuan FROM barang", conn)
                Dim dt As New DataTable
                da.Fill(dt)
                SetupSearchGrid(dt)
            End Using
        Catch ex As Exception
            ShowErrorMessage("Error memuat data barang", ex.Message)
        End Try
    End Sub

    Private Sub SearchItems(searchTerm As String)
        If String.IsNullOrWhiteSpace(searchTerm) Then
            dgvCari.DataSource = Nothing
            Return
        End If

        Try
            BukaKoneksi()
            Dim sql As String = "SELECT kode_barang, nama_barang, satuan FROM barang " &
                               "WHERE nama_barang LIKE @cari OR kode_barang LIKE @cari"

            Using da As New MySqlDataAdapter(sql, conn)
                da.SelectCommand.Parameters.AddWithValue("@cari", $"%{searchTerm.Trim()}%")
                Dim dt As New DataTable
                da.Fill(dt)
                SetupSearchGrid(dt)
            End Using
        Catch ex As Exception
            ShowErrorMessage("Error pencarian", ex.Message)
        End Try
    End Sub

    Private Sub SetupSearchGrid(dt As DataTable)
        dgvCari.DataSource = dt
        If dt.Columns.Contains("kode_barang") Then dgvCari.Columns("kode_barang").HeaderText = "Kode"
        If dt.Columns.Contains("nama_barang") Then dgvCari.Columns("nama_barang").HeaderText = "Nama Barang"
        If dt.Columns.Contains("satuan") Then dgvCari.Columns("satuan").HeaderText = "Satuan"
    End Sub
#End Region

#Region "Form Input Methods"
    Private Sub LoadItemByCode(kodeBarang As String)
        If String.IsNullOrWhiteSpace(kodeBarang) Then
            ClearItemInfo()
            Return
        End If

        Try
            BukaKoneksi()
            Using cmd As New MySqlCommand("SELECT nama_barang, satuan FROM barang WHERE kode_barang=@kode", conn)
                cmd.Parameters.AddWithValue("@kode", kodeBarang.Trim())
                Using rd = cmd.ExecuteReader()
                    If rd.Read() Then
                        lblBarang.Text = rd("nama_barang").ToString()
                        lblSatuan.Text = rd("satuan").ToString()
                    Else
                        ClearItemInfo()
                    End If
                End Using
            End Using
        Catch ex As Exception
            ClearItemInfo()
        End Try
    End Sub

    Private Sub ClearItemInfo()
        lblBarang.Text = "-"
        lblSatuan.Text = "-"
    End Sub

    Private Sub ResetForm()
        txtKode.Clear()
        txtJumlah.Clear()
        txtSuplier.Clear()
        txtHargaPartai.Clear()
        ClearItemInfo()
        txtKode.Focus()
    End Sub

    Private Function ValidateInput() As ValidationResult
        ' Validasi field kosong
        If String.IsNullOrWhiteSpace(txtKode.Text) OrElse lblBarang.Text = "-" OrElse
           String.IsNullOrWhiteSpace(txtJumlah.Text) OrElse String.IsNullOrWhiteSpace(txtSuplier.Text) OrElse
           String.IsNullOrWhiteSpace(txtHargaPartai.Text) Then
            Return New ValidationResult(False, "Semua field harus diisi!")
        End If

        ' Validasi jumlah
        Dim jumlah As Integer
        If Not Integer.TryParse(txtJumlah.Text.Trim, jumlah) OrElse jumlah <= 0 Then
            txtJumlah.Focus()
            Return New ValidationResult(False, "Jumlah harus berupa angka lebih dari 0!")
        End If

        ' Validasi harga
        Dim harga As Integer
        If Not Integer.TryParse(txtHargaPartai.Text.Trim, harga) OrElse harga <= 0 Then
            txtHargaPartai.Focus()
            Return New ValidationResult(False, "Harga partai harus berupa angka lebih dari 0!")
        End If

        Return New ValidationResult(True, "", jumlah, harga)
    End Function
#End Region

#Region "Database Operations"
    Private Function SaveTransaction(jumlah As Integer, harga As Integer) As Boolean
        Try
            BukaKoneksi()
            Dim sql As String = "INSERT INTO transaksi_masuk (kode_barang, nama_barang, satuan, jumlah, suplier, harga_partai, tanggal_masuk) " &
                               "VALUES (@kode, @nama, @satuan, @jumlah, @suplier, @harga, @tanggal)"

            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@kode", txtKode.Text.Trim)
                cmd.Parameters.AddWithValue("@nama", lblBarang.Text.Trim)
                cmd.Parameters.AddWithValue("@satuan", lblSatuan.Text.Trim)
                cmd.Parameters.AddWithValue("@jumlah", jumlah)
                cmd.Parameters.AddWithValue("@suplier", txtSuplier.Text.Trim)
                cmd.Parameters.AddWithValue("@harga", harga)
                cmd.Parameters.AddWithValue("@tanggal", DateTime.Now)

                cmd.ExecuteNonQuery()
                Return True
            End Using
        Catch ex As Exception
            ShowErrorMessage("Gagal menyimpan data", ex.Message)
            Return False
        End Try
    End Function

    Private Function DeleteTransaction(id As Integer) As Boolean
        Try
            BukaKoneksi()
            Using cmd As New MySqlCommand("DELETE FROM transaksi_masuk WHERE id=@id", conn)
                cmd.Parameters.AddWithValue("@id", id)
                cmd.ExecuteNonQuery()
                Return True
            End Using
        Catch ex As Exception
            ShowErrorMessage("Gagal menghapus data", ex.Message)
            Return False
        End Try
    End Function
#End Region

#Region "Event Handlers"
    Private Sub txtKode_TextChanged(sender As Object, e As EventArgs) Handles txtKode.TextChanged
        LoadItemByCode(txtKode.Text)
    End Sub

    Private Sub txtJumlah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtJumlah.KeyPress
        ValidateNumericInput(e)
    End Sub

    Private Sub txtHargaPartai_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHargaPartai.KeyPress
        ValidateNumericInput(e)
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        ShowSearchPanel()
    End Sub

    Private Sub btnTutup_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        HideSearchPanel()
    End Sub

    Private Sub txtPanelCari_TextChanged(sender As Object, e As EventArgs) Handles txtPanelCari.TextChanged
        SearchItems(txtPanelCari.Text)
    End Sub

    Private Sub dgvCari_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCari.CellClick
        SelectItemFromSearch(e.RowIndex)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        ResetForm()
    End Sub

    Private Sub btnTambahBarang_Click(sender As Object, e As EventArgs) Handles btnTambahBarang.Click
        Dim fr As New FR_BARANG()
        fr.ShowDialog()
    End Sub

    Private Sub btnTambahStock_Click(sender As Object, e As EventArgs) Handles btnTambahStock.Click
        ProcessAddStock()
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        NavigateToNextPage()
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        NavigateToPreviousPage()
    End Sub

    Private Sub HapusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HapusToolStripMenuItem.Click
        ProcessDeleteTransaction()
    End Sub

    Private Sub dgvTampil_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvTampil.CellMouseDown
        HandleGridRightClick(e)
    End Sub
#End Region

#Region "Helper Methods"
    Private Sub ValidateNumericInput(e As KeyPressEventArgs)
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub ShowSearchPanel()
        pnCari.Visible = True
        txtPanelCari.Clear()
        LoadAllItems()
        txtPanelCari.Focus()
    End Sub

    Private Sub HideSearchPanel()
        pnCari.Visible = False
    End Sub

    Private Sub SelectItemFromSearch(rowIndex As Integer)
        If rowIndex < 0 Then Return

        Dim row As DataGridViewRow = dgvCari.Rows(rowIndex)
        txtKode.Text = If(row.Cells("kode_barang").Value IsNot Nothing, row.Cells("kode_barang").Value.ToString(), "")
        lblBarang.Text = If(row.Cells("nama_barang").Value IsNot Nothing, row.Cells("nama_barang").Value.ToString(), "")
        lblSatuan.Text = If(row.Cells("satuan").Value IsNot Nothing, row.Cells("satuan").Value.ToString(), "")
        txtJumlah.Focus()
        HideSearchPanel()
    End Sub

    Private Sub ProcessAddStock()
        Dim validation = ValidateInput()
        If Not validation.IsValid Then
            MsgBox(validation.Message, MsgBoxStyle.Exclamation)
            Return
        End If

        If SaveTransaction(validation.Jumlah, validation.Harga) Then
            ResetForm()
            LoadTransactionData()
            MsgBox("Data barang masuk berhasil disimpan.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub NavigateToNextPage()
        If currentPage < TotalPages Then
            currentPage += 1
            LoadTransactionData()
        End If
    End Sub

    Private Sub NavigateToPreviousPage()
        If currentPage > 1 Then
            currentPage -= 1
            LoadTransactionData()
        End If
    End Sub

    Private Sub ProcessDeleteTransaction()
        Dim selectedId = GetSelectedTransactionId()
        If selectedId Is Nothing Then
            MsgBox("Pilih data yang ingin dihapus terlebih dahulu.", MsgBoxStyle.Exclamation)
            Return
        End If

        If MsgBox("Yakin ingin menghapus data ini?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Return
        End If

        If DeleteTransaction(selectedId.Value) Then
            LoadTransactionData()
            MsgBox("Data berhasil dihapus.", MsgBoxStyle.Information)
        End If
    End Sub

    Private Function GetSelectedTransactionId() As Integer?
        If dgvTampil.SelectedRows.Count = 0 OrElse dgvTampil.SelectedRows(0).IsNewRow Then
            Return Nothing
        End If

        Dim idCell = dgvTampil.SelectedRows(0).Cells("id").Value
        If idCell Is Nothing OrElse Not Integer.TryParse(idCell.ToString, Nothing) Then
            Return Nothing
        End If

        Return Convert.ToInt32(idCell)
    End Function

    Private Sub HandleGridRightClick(e As DataGridViewCellMouseEventArgs)
        If e.Button = MouseButtons.Right AndAlso e.RowIndex >= 0 Then
            dgvTampil.ClearSelection()
            dgvTampil.Rows(e.RowIndex).Selected = True
        End If
    End Sub

    Private Sub ShowErrorMessage(title As String, message As String)
        MsgBox(title & ": " & message, MsgBoxStyle.Critical)
    End Sub
#End Region
End Class

' Helper class untuk validasi
Public Class ValidationResult
    Public Property IsValid As Boolean
    Public Property Message As String
    Public Property Jumlah As Integer
    Public Property Harga As Integer

    Public Sub New(isValid As Boolean, message As String, Optional jumlah As Integer = 0, Optional harga As Integer = 0)
        Me.IsValid = isValid
        Me.Message = message
        Me.Jumlah = jumlah
        Me.Harga = harga
    End Sub
End Class