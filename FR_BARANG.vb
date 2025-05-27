Imports MySql.Data.MySqlClient
Public Class FR_BARANG

    Sub TampilBarang()
        BukaKoneksi()
        da = New MySqlDataAdapter("SELECT * FROM barang", conn)
        dt = New DataTable
        da.Fill(dt)
        DataGridView1.DataSource = dt

        With DataGridView1
            .Columns("kode_barang").HeaderText = "Kode Barang"
            .Columns("nama_barang").HeaderText = "Nama Barang"
            .Columns("satuan").HeaderText = "Satuan"
            .Columns("harga_satuan").HeaderText = "Harga Satuan"
            .Columns("harga_satuan").DefaultCellStyle.Format = "N0"
        End With
    End Sub

    Sub Kosongkan()
        txtKodeBarang.Text = AutoKode()
        txtNamaBarang.Clear()
        cbSatuan.SelectedIndex = -1
        txtHarga.Clear()
        txtCari.Clear()
        txtNamaBarang.Focus()
    End Sub

    Function AutoKode() As String
        BukaKoneksi()
        cmd = New MySqlCommand("SELECT kode_barang FROM barang ORDER BY kode_barang DESC LIMIT 1", conn)
        rd = cmd.ExecuteReader()
        Dim kode As String = ""
        If rd.Read() Then
            Dim lastNum As Integer = Val(Microsoft.VisualBasic.Right(rd.Item(0), 3)) + 1
            kode = "BRG" & lastNum.ToString("D3")
        Else
            kode = "BRG001"
        End If
        rd.Close()
        Return kode
    End Function

    Private Sub FR_BARANG_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtKodeBarang.Text = AutoKode()
        cbSatuan.Items.AddRange(New String() {"Pcs", "Lusin", "Kg", "Box", "Set", "Unit"})
        TampilBarang()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtNamaBarang.Text = "" Or cbSatuan.Text = "" Or txtHarga.Text = "" Then
            MsgBox("Semua field harus diisi!")
            Exit Sub
        End If

        BukaKoneksi()
        cmd = New MySqlCommand("INSERT INTO barang VALUES (@kode, @nama, @satuan, @harga)", conn)
        cmd.Parameters.AddWithValue("@kode", txtKodeBarang.Text)
        cmd.Parameters.AddWithValue("@nama", txtNamaBarang.Text)
        cmd.Parameters.AddWithValue("@satuan", cbSatuan.Text)
        cmd.Parameters.AddWithValue("@harga", txtHarga.Text)
        cmd.ExecuteNonQuery()

        MsgBox("Data berhasil disimpan.")
        TampilBarang()
        Kosongkan()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        BukaKoneksi()
        cmd = New MySqlCommand("UPDATE barang SET nama_barang=@nama, satuan=@satuan, harga_satuan=@harga WHERE kode_barang=@kode", conn)
        cmd.Parameters.AddWithValue("@kode", txtKodeBarang.Text)
        cmd.Parameters.AddWithValue("@nama", txtNamaBarang.Text)
        cmd.Parameters.AddWithValue("@satuan", cbSatuan.Text)
        cmd.Parameters.AddWithValue("@harga", txtHarga.Text)
        cmd.ExecuteNonQuery()

        MsgBox("Data berhasil diperbarui.")
        TampilBarang()
        Kosongkan()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If MsgBox("Yakin ingin menghapus data ini?", vbYesNo + vbQuestion) = vbYes Then
            BukaKoneksi()
            cmd = New MySqlCommand("DELETE FROM barang WHERE kode_barang=@kode", conn)
            cmd.Parameters.AddWithValue("@kode", txtKodeBarang.Text)
            cmd.ExecuteNonQuery()

            MsgBox("Data berhasil dihapus.")
            TampilBarang()
            Kosongkan()
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Kosongkan()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
        FR_MENU.Show()
    End Sub

    Private Sub txtCari_TextChanged(sender As Object, e As EventArgs) Handles txtCari.TextChanged
        BukaKoneksi()
        da = New MySqlDataAdapter("SELECT * FROM barang WHERE 
            kode_barang LIKE '%" & txtCari.Text & "%' OR 
            nama_barang LIKE '%" & txtCari.Text & "%' OR 
            satuan LIKE '%" & txtCari.Text & "%'", conn)
        dt = New DataTable
        da.Fill(dt)
        DataGridView1.DataSource = dt
    End Sub

    Private Sub txtHarga_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHarga.KeyPress
        If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim i As Integer = DataGridView1.CurrentRow.Index
        txtKodeBarang.Text = DataGridView1.Item(0, i).Value
        txtNamaBarang.Text = DataGridView1.Item(1, i).Value
        cbSatuan.Text = DataGridView1.Item(2, i).Value
        txtHarga.Text = DataGridView1.Item(3, i).Value
    End Sub
End Class
