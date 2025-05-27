Imports System.Drawing.Printing
Imports MySql.Data.MySqlClient

Public Class FR_MASUK
    Dim currentPage As Integer = 1
    Dim pageSize As Integer = 5
    Dim totalRows As Integer = 0
    Dim totalPages As Integer = 0

    Sub TampilSemuaBarang()
        Try
            BukaKoneksi()
            Dim da As New MySqlDataAdapter("SELECT kode_barang, nama_barang, satuan FROM barang", conn)
            Dim dt As New DataTable
            da.Fill(dt)
            dgvCari.DataSource = dt
            dgvCari.Columns("kode_barang").HeaderText = "Kode"
            dgvCari.Columns("nama_barang").HeaderText = "Nama Barang"
            dgvCari.Columns("satuan").HeaderText = "Satuan"
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        End Try
    End Sub

    Sub ResetForm()
        txtKode.Clear()
        lblBarang.Text = "-"
        lblSatuan.Text = "-"
        txtJumlah.Clear()
        txtSuplier.Clear()
        txtHargaPartai.Clear()
        txtKode.Focus()
    End Sub

    Sub TambahKeGrid(Optional ByVal tanggalMasuk As String = "", Optional ByVal id As Integer = 0)
        If txtKode.Text = "" Or lblBarang.Text = "" Or lblSatuan.Text = "" Or txtJumlah.Text = "" Or txtSuplier.Text = "" Or txtHargaPartai.Text = "" Then
            Exit Sub
        End If

        Dim idx As Integer = dgvTampil.Rows.Add()
        dgvTampil.Rows(idx).Cells("id").Value = id
        dgvTampil.Rows(idx).Cells("kode_barang").Value = txtKode.Text
        dgvTampil.Rows(idx).Cells("nama_barang").Value = lblBarang.Text
        dgvTampil.Rows(idx).Cells("satuan").Value = lblSatuan.Text
        dgvTampil.Rows(idx).Cells("jumlah").Value = txtJumlah.Text
        dgvTampil.Rows(idx).Cells("suplier").Value = txtSuplier.Text
        dgvTampil.Rows(idx).Cells("harga_partai").Value = txtHargaPartai.Text
        dgvTampil.Rows(idx).Cells("tanggal_masuk").Value = If(tanggalMasuk = "", Now.ToString("yyyy-MM-dd HH:mm:ss"), tanggalMasuk)
    End Sub

    Sub TampilTransaksiMasuk()
        Try
            BukaKoneksi()
            ' Hitung total data dulu
            Dim cmdCount As New MySqlCommand("SELECT COUNT(*) FROM transaksi_masuk", conn)
            totalRows = Convert.ToInt32(cmdCount.ExecuteScalar())
            totalPages = Math.Ceiling(totalRows / pageSize)

            ' Ambil data dengan LIMIT dan OFFSET
            Dim offset As Integer = (currentPage - 1) * pageSize
            Dim da As New MySqlDataAdapter("SELECT id, kode_barang, nama_barang, satuan, jumlah, suplier, harga_partai, tanggal_masuk FROM transaksi_masuk ORDER BY id ASC LIMIT @limit OFFSET @offset", conn)
            da.SelectCommand.Parameters.AddWithValue("@limit", pageSize)
            da.SelectCommand.Parameters.AddWithValue("@offset", offset)

            Dim dt As New DataTable
            da.Fill(dt)
            dgvTampil.Rows.Clear()
            For Each row As DataRow In dt.Rows
                dgvTampil.Rows.Add(row("id"), row("kode_barang"), row("nama_barang"), row("satuan"), row("jumlah"), row("suplier"), row("harga_partai"), row("tanggal_masuk"))
            Next
        Catch ex As Exception
            MsgBox("Gagal load data: " & ex.Message, MsgBoxStyle.Critical)
        End Try
        lblPagingInfo.Text = currentPage & " dari " & totalPages
    End Sub

    Private Sub FR_MASUK_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If dgvTampil.Columns.Count = 0 Then
            dgvTampil.Columns.Add("id", "ID")
            dgvTampil.Columns.Add("kode_barang", "Kode")
            dgvTampil.Columns.Add("nama_barang", "Nama")
            dgvTampil.Columns.Add("satuan", "Satuan")
            dgvTampil.Columns.Add("jumlah", "Jumlah")
            dgvTampil.Columns.Add("suplier", "Suplier")
            dgvTampil.Columns.Add("harga_partai", "Harga Partai")
            dgvTampil.Columns.Add("tanggal_masuk", "Tanggal Masuk")
        End If
        TampilTransaksiMasuk()
    End Sub

    Private Sub txtKode_TextChanged(sender As Object, e As EventArgs) Handles txtKode.TextChanged
        If txtKode.Text.Trim = "" Then
            lblBarang.Text = ""
            lblSatuan.Text = ""
            Return
        End If

        Try
            BukaKoneksi()
            Dim cmd As New MySqlCommand("SELECT nama_barang, satuan FROM barang WHERE kode_barang=@kode", conn)
            cmd.Parameters.AddWithValue("@kode", txtKode.Text.Trim)
            Dim rd = cmd.ExecuteReader
            If rd.Read Then
                lblBarang.Text = rd("nama_barang").ToString
                lblSatuan.Text = rd("satuan").ToString
            Else
                lblBarang.Text = "-"
                lblSatuan.Text = "-"
            End If
            rd.Close()
        Catch ex As Exception
            lblBarang.Text = "-"
            lblSatuan.Text = "-"
        End Try
    End Sub

    Private Sub txtJumlah_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtJumlah.KeyPress
        If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtHargaPartai_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHargaPartai.KeyPress
        If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        pnCari.Visible = True
        txtPanelCari.Text = ""
        TampilSemuaBarang()
        txtPanelCari.Focus()
    End Sub

    Private Sub btnTutup_Click(sender As Object, e As EventArgs) Handles btnTutup.Click
        pnCari.Visible = False
    End Sub

    Private Sub txtPanelCari_TextChanged(sender As Object, e As EventArgs) Handles txtPanelCari.TextChanged
        If txtPanelCari.Text.Trim() = "" Then
            dgvCari.DataSource = Nothing
            Return
        End If

        Try
            BukaKoneksi()
            Dim da As New MySqlDataAdapter("SELECT kode_barang, nama_barang, satuan FROM barang WHERE nama_barang LIKE @cari OR kode_barang LIKE @cari", conn)
            da.SelectCommand.Parameters.AddWithValue("@cari", "%" & txtPanelCari.Text.Trim() & "%")
            Dim dt As New DataTable
            da.Fill(dt)
            dgvCari.DataSource = dt
            dgvCari.Columns("kode_barang").HeaderText = "Kode"
            dgvCari.Columns("nama_barang").HeaderText = "Nama Barang"
            dgvCari.Columns("satuan").HeaderText = "Satuan"
        Catch ex As Exception
            MsgBox("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub dgvCari_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCari.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvCari.Rows(e.RowIndex)
            txtKode.Text = row.Cells("kode_barang").Value.ToString()
            lblBarang.Text = row.Cells("nama_barang").Value.ToString()
            lblSatuan.Text = row.Cells("satuan").Value.ToString()
            txtJumlah.Focus()
            pnCari.Visible = False
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        ResetForm()
    End Sub

    Private Sub btnTambahBarang_Click(sender As Object, e As EventArgs) Handles btnTambahBarang.Click
        Dim fr As New FR_BARANG
        fr.ShowDialog()
    End Sub

    Private Sub btnTambahStock_Click(sender As Object, e As EventArgs) Handles btnTambahStock.Click
        ' Validasi field wajib
        If txtKode.Text.Trim = "" Or lblBarang.Text = "-" Or lblSatuan.Text = "-" Or
       txtJumlah.Text.Trim = "" Or txtSuplier.Text.Trim = "" Or txtHargaPartai.Text.Trim = "" Then
            MsgBox("Semua field harus diisi!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        ' Validasi jumlah
        Dim jumlah As Integer
        If Not Integer.TryParse(txtJumlah.Text.Trim, jumlah) OrElse jumlah <= 0 Then
            MsgBox("Jumlah harus berupa angka lebih dari 0!", MsgBoxStyle.Exclamation)
            txtJumlah.Focus()
            Exit Sub
        End If

        ' Validasi harga partai
        Dim harga As Integer
        If Not Integer.TryParse(txtHargaPartai.Text.Trim, harga) OrElse harga <= 0 Then
            MsgBox("Harga partai harus berupa angka lebih dari 0!", MsgBoxStyle.Exclamation)
            txtHargaPartai.Focus()
            Exit Sub
        End If

        ' Validasi suplier
        If txtSuplier.Text.Trim = "" Then
            MsgBox("Nama suplier harus diisi!", MsgBoxStyle.Exclamation)
            txtSuplier.Focus()
            Exit Sub
        End If
        ' Insert ke database barang_masuk
        Dim tanggalMasuk As String = Now.ToString("yyyy-MM-dd HH:mm:ss")
        Try
            BukaKoneksi()
            Dim cmd As New MySqlCommand("INSERT INTO transaksi_masuk (kode_barang, nama_barang, satuan, jumlah, suplier, harga_partai, tanggal_masuk) VALUES (@kode, @nama, @satuan, @jumlah, @suplier, @harga, @tanggal)", conn)
            cmd.Parameters.AddWithValue("@kode", txtKode.Text.Trim)
            cmd.Parameters.AddWithValue("@nama", lblBarang.Text.Trim)
            cmd.Parameters.AddWithValue("@satuan", lblSatuan.Text.Trim)
            cmd.Parameters.AddWithValue("@jumlah", jumlah)
            cmd.Parameters.AddWithValue("@suplier", txtSuplier.Text.Trim)
            cmd.Parameters.AddWithValue("@harga", harga)
            cmd.Parameters.AddWithValue("@tanggal", Now)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Gagal menyimpan data " & ex.Message, MsgBoxStyle.Critical)
            Exit Sub
        End Try

        ' Jika sukses, tambahkan ke grid dan reset form
        TambahKeGrid()
        ResetForm()
        MsgBox("Data barang masuk berhasil disimpan.", MsgBoxStyle.Information)
        TampilTransaksiMasuk()
    End Sub

    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) Handles btnKeluar.Click
        Me.Close()
        FR_MENU.Show()
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If currentPage < totalPages Then
            currentPage += 1
            TampilTransaksiMasuk()
        End If
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        If currentPage > 1 Then
            currentPage -= 1
            TampilTransaksiMasuk()
        End If
    End Sub

End Class