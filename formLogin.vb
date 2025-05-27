Imports MySql.Data.MySqlClient

Public Class formLogin
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If txtUsername.Text = "" Or txtPassword.Text = "" Then
            MsgBox("Username dan Password tidak boleh kosong!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        BukaKoneksi()
        Try
            Dim cmd As New MySqlCommand("SELECT * FROM users WHERE username=@username AND password=@password", conn)
            cmd.Parameters.AddWithValue("@username", txtUsername.Text)
            cmd.Parameters.AddWithValue("@password", txtPassword.Text)

            Dim rd As MySqlDataReader = cmd.ExecuteReader()
            Dim Menu As New FR_MENU

            If rd.HasRows Then
                MsgBox("Login berhasil!", MsgBoxStyle.Information)
                rd.Close()
                conn.Close()
                LoggedInUser = txtUsername.Text
                IsLoggedIn = True
                Me.Hide()
                Menu.Show()
            Else
                MsgBox("Username atau password salah!", MsgBoxStyle.Critical)
                rd.Close()
                conn.Close()
            End If
        Catch ex As Exception
            MsgBox("Terjadi kesalahan: " & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub btnKeluar_Click(sender As Object, e As EventArgs) Handles btnKeluar.Click
        End
    End Sub

    Private Sub linkRegister_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles linkRegister.LinkClicked
        Dim Register As New formRegister
        Me.Hide()
        Register.Show()
    End Sub
End Class
