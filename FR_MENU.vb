Public Class FR_MENU
    Private Sub mnLogout_Click(sender As Object, e As EventArgs) Handles mnLogout.Click
        Dim result As DialogResult = MessageBox.Show("Yakin mau logout?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            LoggedInUser = String.Empty
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

    Sub BukaForm(ByVal FR As Form)
        For Each F As Form In Me.MdiChildren
            If F.Name = FR.Name Then
                MsgBox("Form sudah terbuka", MsgBoxStyle.Information, "Informasi")
                Exit Sub
            End If
        Next
        FR.MdiParent = Me
        FR.Show()
    End Sub

    Private Sub mnKaryawan_Click(sender As Object, e As EventArgs) Handles mnKaryawan.Click
        BukaForm(FR_KARYAWAN)
    End Sub

    Private Sub mnBarang_Click(sender As Object, e As EventArgs) Handles mnBarang.Click
        BukaForm(FR_BARANG)
    End Sub

    Private Sub mnBarangMasuk_Click(sender As Object, e As EventArgs) Handles mnBarangMasuk.Click
        BukaForm(FR_MASUK)
    End Sub

    Private Sub mnBarangKeluar_Click(sender As Object, e As EventArgs) Handles mnBarangKeluar.Click
        Dim fr As New FR_KELUAR
        fr.Show()
        Me.Hide()
    End Sub

    Private Sub mnLaporan_Click(sender As Object, e As EventArgs) Handles mnLaporan.Click
        BukaForm(FR_LAPORAN)
    End Sub

    Private Sub mnTentang_Click(sender As Object, e As EventArgs) Handles mnTentang.Click
        BukaForm(FR_TENTANG)
    End Sub

    Private Sub FR_MENU_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtTanggal.Text = FormatDateTime(Date.Now, DateFormat.LongDate)
        txtWaktu.Text = TimeOfDay.ToString("HH:mm:ss")
        WAKTU.Enabled = True
        ToolStripLabelUser.Text = "Login sebagai: " & LoggedInUser
    End Sub

    Private Sub WAKTU_Tick(sender As Object, e As EventArgs) Handles WAKTU.Tick
        txtTanggal.Text = FormatDateTime(Date.Now, DateFormat.LongDate)
        txtWaktu.Text = TimeOfDay.ToString("HH:mm:ss")
    End Sub

End Class