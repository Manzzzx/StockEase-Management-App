Imports MySql.Data.MySqlClient

Public Class formRegister
    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        If txtUsername.Text = "" Or txtPassword.Text = "" Or txtKonfirmasi.Text = "" Then
            MsgBox("Semua field wajib diisi!", MsgBoxStyle.Exclamation)
        ElseIf txtPassword.Text <> txtKonfirmasi.Text Then
            MsgBox("Password dan konfirmasi tidak sama!", MsgBoxStyle.Critical)
        Else
            BukaKoneksi()

            Dim cekUser As New MySqlCommand("SELECT * FROM users WHERE username=@username", conn)
            cekUser.Parameters.AddWithValue("@username", txtUsername.Text)
            Dim rd As MySqlDataReader = cekUser.ExecuteReader()
            Dim Login As New formLogin

            If rd.HasRows Then
                MsgBox("Username sudah terdaftar!", MsgBoxStyle.Critical)
                rd.Close()
                conn.Close()
                Exit Sub
            End If
            rd.Close()

            ' Insert user baru
            Dim cmd As New MySqlCommand("INSERT INTO users(username, password) VALUES (@username, @password)", conn)
            cmd.Parameters.AddWithValue("@username", txtUsername.Text)
            cmd.Parameters.AddWithValue("@password", txtPassword.Text)

            cmd.ExecuteNonQuery()
            MsgBox("Registrasi berhasil! Silakan login.", MsgBoxStyle.Information)

            conn.Close()
            Me.Hide()
            Login.Show()
        End If
    End Sub

    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Dim Login As New formLogin
        Me.Hide()
        formLogin.Show()
    End Sub

    Private Sub txtUsername_TextChanged(sender As Object, e As EventArgs) Handles txtUsername.TextChanged

    End Sub
End Class
