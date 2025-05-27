Imports MySql.Data.MySqlClient

Public Class FR_KARYAWAN

    Sub TampilData()
        BukaKoneksi()
        Dim da As New MySqlDataAdapter("SELECT * FROM karyawan", conn)
        Dim dt As New DataTable
        da.Fill(dt)
        DataGridView1.DataSource = dt
        conn.Close()

        With DataGridView1
            .Columns("nik").HeaderText = "NIK"
            .Columns("nama").HeaderText = "Nama"
            .Columns("tanggal_lahir").HeaderText = "Tanggal Lahir"
            .Columns("asal").HeaderText = "Asal"
            .Columns("jenis_kelamin").HeaderText = "Jenis Kelamin"
            .Columns("alamat").HeaderText = "Alamat"
            .Columns("no_hp").HeaderText = "No HP"
        End With
    End Sub

    Sub AutoNIK()
        BukaKoneksi()
        Dim cmd As New MySqlCommand("SELECT MAX(RIGHT(nik, 4)) FROM karyawan", conn)
        Dim urutan As String
        Dim hitung As Integer

        Dim rd = cmd.ExecuteReader()
        If rd.Read() And Not IsDBNull(rd(0)) Then
            hitung = CInt(rd(0)) + 1
            urutan = "202025" & hitung.ToString("D4")
        Else
            urutan = "2020250001"
        End If
        rd.Close()
        txtNIK.Text = urutan
        conn.Close()
    End Sub

    Sub KosongkanForm()
        txtNIK.Clear()
        txtNama.Clear()
        txtAsal.Clear()
        cbJenis.SelectedIndex = -1
        txtAlamat.Clear()
        txtNoHP.Clear()
        dtpTanggal.Value = Today
        AutoNIK()
    End Sub

    Private Sub FR_KARYAWAN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TampilData()
        KosongkanForm()
        AutoNIK()
    End Sub

    Private Sub txtNoHP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNoHP.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        BukaKoneksi()
        Dim cmd As New MySqlCommand("INSERT INTO karyawan VALUES (@nik, @nama, @tgl, @asal, @jk, @alamat, @nohp)", conn)
        cmd.Parameters.AddWithValue("@nik", txtNIK.Text)
        cmd.Parameters.AddWithValue("@nama", txtNama.Text)
        cmd.Parameters.AddWithValue("@tgl", dtpTanggal.Value)
        cmd.Parameters.AddWithValue("@asal", txtAsal.Text)
        cmd.Parameters.AddWithValue("@jk", cbJenis.Text)
        cmd.Parameters.AddWithValue("@alamat", txtAlamat.Text)
        cmd.Parameters.AddWithValue("@nohp", txtNoHP.Text)

        Try
            cmd.ExecuteNonQuery()
            MsgBox("Data berhasil disimpan", MsgBoxStyle.Information)
            TampilData()
            KosongkanForm()
        Catch ex As Exception
            MsgBox("Gagal simpan: " & ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        BukaKoneksi()
        Dim cmd As New MySqlCommand("UPDATE karyawan SET nama=@nama, tanggal_lahir=@tgl, asal=@asal, jenis_kelamin=@jk, alamat=@alamat, no_hp=@nohp WHERE nik=@nik", conn)
        cmd.Parameters.AddWithValue("@nik", txtNIK.Text)
        cmd.Parameters.AddWithValue("@nama", txtNama.Text)
        cmd.Parameters.AddWithValue("@tgl", dtpTanggal.Value)
        cmd.Parameters.AddWithValue("@asal", txtAsal.Text)
        cmd.Parameters.AddWithValue("@jk", cbJenis.Text)
        cmd.Parameters.AddWithValue("@alamat", txtAlamat.Text)
        cmd.Parameters.AddWithValue("@nohp", txtNoHP.Text)

        Try
            cmd.ExecuteNonQuery()
            MsgBox("Data berhasil diupdate", MsgBoxStyle.Information)
            TampilData()
            KosongkanForm()
        Catch ex As Exception
            MsgBox("Gagal update: " & ex.Message)
        End Try
        conn.Close()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If MsgBox("Yakin ingin menghapus data?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            BukaKoneksi()
            Dim cmd As New MySqlCommand("DELETE FROM karyawan WHERE nik=@nik", conn)
            cmd.Parameters.AddWithValue("@nik", txtNIK.Text)
            Try
                cmd.ExecuteNonQuery()
                MsgBox("Data berhasil dihapus", MsgBoxStyle.Information)
                TampilData()
                KosongkanForm()
            Catch ex As Exception
                MsgBox("Gagal hapus: " & ex.Message)
            End Try
            conn.Close()
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        KosongkanForm()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
        FR_MENU.Show()
    End Sub

    Private Sub txtCari_TextChanged(sender As Object, e As EventArgs) Handles txtCari.TextChanged
        BukaKoneksi()
        Dim cmd As New MySqlCommand("SELECT * FROM karyawan WHERE nik LIKE @cari OR nama LIKE @cari OR asal LIKE @cari OR alamat LIKE @cari", conn)
        cmd.Parameters.AddWithValue("@cari", "%" & txtCari.Text & "%")
        Dim da As New MySqlDataAdapter(cmd)
        Dim dt As New DataTable
        da.Fill(dt)
        DataGridView1.DataSource = dt
        conn.Close()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            txtNIK.Text = row.Cells("nik").Value.ToString()
            txtNama.Text = row.Cells("nama").Value.ToString()
            dtpTanggal.Value = Convert.ToDateTime(row.Cells("tanggal_lahir").Value)
            txtAsal.Text = row.Cells("asal").Value.ToString()
            cbJenis.Text = row.Cells("jenis_kelamin").Value.ToString()
            txtAlamat.Text = row.Cells("alamat").Value.ToString()
            txtNoHP.Text = row.Cells("no_hp").Value.ToString()
        End If
    End Sub
End Class
