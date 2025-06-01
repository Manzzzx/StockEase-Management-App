Imports Google.Protobuf.WellKnownTypes

Module Module1
    Public LoggedInUser As String = ""
    Public IsLoggedIn As Boolean = False
    Public nama_toko As String
    Public alamat_toko As String
    Public no_telp As String
    Public printer_toko As String

    Public Sub masuk_data(ByVal toko As String, ByVal alamat As String, ByVal no As String, ByVal prints As String)
        Call SaveSetting("toko", "setting", "nama_toko", toko)
        Call SaveSetting("toko", "setting", "alamat_toko", alamat)
        Call SaveSetting("toko", "setting", "no_telp", no)
        Call SaveSetting("toko", "setting", "printer_toko", prints)
    End Sub

    Public Sub ambil_data()
        nama_toko = GetSetting("toko", "setting", "nama_toko", "")
        alamat_toko = GetSetting("toko", "setting", "alamat_toko", "")
        no_telp = GetSetting("toko", "setting", "no_telp", "")
        printer_toko = GetSetting("toko", "setting", "printer_toko", "")
    End Sub
End Module
