<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FR_MENU
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        MenuStrip1 = New MenuStrip()
        FileToolStripMenuItem = New ToolStripMenuItem()
        mnLogout = New ToolStripMenuItem()
        mnExit = New ToolStripMenuItem()
        DatabaseToolStripMenuItem = New ToolStripMenuItem()
        mnKaryawan = New ToolStripMenuItem()
        mnBarang = New ToolStripMenuItem()
        TransaksiToolStripMenuItem = New ToolStripMenuItem()
        mnBarangMasuk = New ToolStripMenuItem()
        mnBarangKeluar = New ToolStripMenuItem()
        mnLaporan = New ToolStripMenuItem()
        mnTentang = New ToolStripMenuItem()
        ToolStrip1 = New ToolStrip()
        txtTanggal = New ToolStripLabel()
        ToolStripSeparator2 = New ToolStripSeparator()
        txtWaktu = New ToolStripLabel()
        ToolStripSeparator1 = New ToolStripSeparator()
        ToolStripLabelUser = New ToolStripLabel()
        WAKTU = New Timer(components)
        PengaturanToolStripMenuItem = New ToolStripMenuItem()
        MenuStrip1.SuspendLayout()
        ToolStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.ImageScalingSize = New Size(32, 32)
        MenuStrip1.Items.AddRange(New ToolStripItem() {FileToolStripMenuItem, DatabaseToolStripMenuItem, TransaksiToolStripMenuItem, mnLaporan, PengaturanToolStripMenuItem, mnTentang})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(830, 42)
        MenuStrip1.TabIndex = 1
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' FileToolStripMenuItem
        ' 
        FileToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {mnLogout, mnExit})
        FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        FileToolStripMenuItem.Size = New Size(110, 38)
        FileToolStripMenuItem.Text = "System"
        ' 
        ' mnLogout
        ' 
        mnLogout.Name = "mnLogout"
        mnLogout.Size = New Size(222, 44)
        mnLogout.Text = "Logout"
        ' 
        ' mnExit
        ' 
        mnExit.Name = "mnExit"
        mnExit.Size = New Size(222, 44)
        mnExit.Text = "Exit"
        ' 
        ' DatabaseToolStripMenuItem
        ' 
        DatabaseToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {mnKaryawan, mnBarang})
        DatabaseToolStripMenuItem.Name = "DatabaseToolStripMenuItem"
        DatabaseToolStripMenuItem.Size = New Size(132, 38)
        DatabaseToolStripMenuItem.Text = "Database"
        ' 
        ' mnKaryawan
        ' 
        mnKaryawan.Name = "mnKaryawan"
        mnKaryawan.Size = New Size(304, 44)
        mnKaryawan.Text = "Data Karyawan"
        ' 
        ' mnBarang
        ' 
        mnBarang.Name = "mnBarang"
        mnBarang.Size = New Size(304, 44)
        mnBarang.Text = "Data Barang"
        ' 
        ' TransaksiToolStripMenuItem
        ' 
        TransaksiToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {mnBarangMasuk, mnBarangKeluar})
        TransaksiToolStripMenuItem.Name = "TransaksiToolStripMenuItem"
        TransaksiToolStripMenuItem.Size = New Size(129, 38)
        TransaksiToolStripMenuItem.Text = "Transaksi"
        ' 
        ' mnBarangMasuk
        ' 
        mnBarangMasuk.Name = "mnBarangMasuk"
        mnBarangMasuk.Size = New Size(298, 44)
        mnBarangMasuk.Text = "Barang Masuk"
        ' 
        ' mnBarangKeluar
        ' 
        mnBarangKeluar.Name = "mnBarangKeluar"
        mnBarangKeluar.Size = New Size(298, 44)
        mnBarangKeluar.Text = "Barang Keluar"
        ' 
        ' mnLaporan
        ' 
        mnLaporan.Name = "mnLaporan"
        mnLaporan.Size = New Size(119, 38)
        mnLaporan.Text = "Laporan"
        ' 
        ' mnTentang
        ' 
        mnTentang.Name = "mnTentang"
        mnTentang.Size = New Size(120, 38)
        mnTentang.Text = "Tentang"
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(32, 32)
        ToolStrip1.Items.AddRange(New ToolStripItem() {txtTanggal, ToolStripSeparator2, txtWaktu, ToolStripSeparator1, ToolStripLabelUser})
        ToolStrip1.Location = New Point(0, 42)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(830, 38)
        ToolStrip1.TabIndex = 3
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' txtTanggal
        ' 
        txtTanggal.Name = "txtTanggal"
        txtTanggal.Size = New Size(116, 32)
        txtTanggal.Text = "TANGGAL"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 38)
        ' 
        ' txtWaktu
        ' 
        txtWaktu.Name = "txtWaktu"
        txtWaktu.Size = New Size(59, 32)
        txtWaktu.Text = "JAM"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 38)
        ' 
        ' ToolStripLabelUser
        ' 
        ToolStripLabelUser.Name = "ToolStripLabelUser"
        ToolStripLabelUser.Size = New Size(69, 32)
        ToolStripLabelUser.Text = "USER"
        ' 
        ' WAKTU
        ' 
        ' 
        ' PengaturanToolStripMenuItem
        ' 
        PengaturanToolStripMenuItem.Name = "PengaturanToolStripMenuItem"
        PengaturanToolStripMenuItem.Size = New Size(155, 38)
        PengaturanToolStripMenuItem.Text = "Pengaturan"
        ' 
        ' FR_MENU
        ' 
        AutoScaleDimensions = New SizeF(13F, 32F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(830, 475)
        Controls.Add(ToolStrip1)
        Controls.Add(MenuStrip1)
        FormBorderStyle = FormBorderStyle.None
        IsMdiContainer = True
        MainMenuStrip = MenuStrip1
        Name = "FR_MENU"
        Text = "Menu"
        WindowState = FormWindowState.Maximized
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents mnLogout As ToolStripMenuItem
    Friend WithEvents mnExit As ToolStripMenuItem
    Friend WithEvents DatabaseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents mnKaryawan As ToolStripMenuItem
    Friend WithEvents mnBarang As ToolStripMenuItem
    Friend WithEvents TransaksiToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents mnBarangMasuk As ToolStripMenuItem
    Friend WithEvents mnBarangKeluar As ToolStripMenuItem
    Friend WithEvents mnLaporan As ToolStripMenuItem
    Friend WithEvents mnTentang As ToolStripMenuItem
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents txtTanggal As ToolStripLabel
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents txtWaktu As ToolStripLabel
    Friend WithEvents WAKTU As Timer
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripLabelUser As ToolStripLabel
    Friend WithEvents PengaturanToolStripMenuItem As ToolStripMenuItem

End Class
