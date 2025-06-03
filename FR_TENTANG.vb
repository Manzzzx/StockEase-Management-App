Public Class FR_TENTANG

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim kata_awal As String = Microsoft.VisualBasic.Left(lbJalan.Text, 1)
        Dim kata_akhir As String = Microsoft.VisualBasic.Right(lbJalan.Text, lbJalan.Text.Length - 1)

        lbJalan.Text = kata_akhir & " " & kata_awal
    End Sub

    Private Sub FR_TENTANG_Load(sender As Object, e As EventArgs) Handles Me.Load
        Timer1.Enabled = True
    End Sub
End Class