Imports MySql.Data.MySqlClient

Module Koneksi
    Public conn As MySqlConnection
    Public cmd As MySqlCommand
    Public da As MySqlDataAdapter
    Public rd As MySqlDataReader
    Public ds As DataSet
    Public dt As DataTable

    Public Sub BukaKoneksi()
        conn = New MySqlConnection("server=localhost;user id=root;password=;database=vbnet_projectuas")
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
        Catch ex As Exception
            MsgBox("Gagal koneksi: " & ex.Message)
        End Try
    End Sub
End Module
