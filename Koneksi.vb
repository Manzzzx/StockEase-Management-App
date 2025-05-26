Imports MySql.Data.MySqlClient

Module Koneksi
    Public conn As MySqlConnection

    Public Sub BukaKoneksi()
        conn = New MySqlConnection("server=localhost;user id=root;password=;database=db_login")
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Catch ex As Exception
            MsgBox("Gagal koneksi: " & ex.Message)
        End Try
    End Sub
End Module
