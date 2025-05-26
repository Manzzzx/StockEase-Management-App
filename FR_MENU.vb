Public Class FR_MENU
    Private Sub mnLogout_Click(sender As Object, e As EventArgs) Handles mnLogout.Click
        Dim result As DialogResult = MessageBox.Show("Yakin mau logout?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Me.Hide()
            formLogin.Show()
        End If
    End Sub

    Private Sub mnExit_Click(sender As Object, e As EventArgs) Handles mnExit.Click
        Dim result As DialogResult = MessageBox.Show("Keluar dari aplikasi?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If result = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub
End Class
