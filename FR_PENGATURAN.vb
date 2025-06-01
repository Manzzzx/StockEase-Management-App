Imports System.Drawing.Printing

Public Class FR_PENGATURAN
    Sub isiPrinter()
        Dim printers As String = ""
        For Each printer_toko As String In PrinterSettings.InstalledPrinters
            cbPrinterToko.Items.Add(printer_toko)
        Next
    End Sub
    Private Sub FR_PENGATURAN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        isiPrinter()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
        FR_MENU.Show()
    End Sub

End Class