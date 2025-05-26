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
        MenuStrip1 = New MenuStrip()
        FileToolStripMenuItem = New ToolStripMenuItem()
        mnLogout = New ToolStripMenuItem()
        mnExit = New ToolStripMenuItem()
        DatabaseToolStripMenuItem = New ToolStripMenuItem()
        TransaksiToolStripMenuItem = New ToolStripMenuItem()
        LaporanToolStripMenuItem = New ToolStripMenuItem()
        TentangToolStripMenuItem = New ToolStripMenuItem()
        DataKaryawanToolStripMenuItem = New ToolStripMenuItem()
        DataBarangToolStripMenuItem = New ToolStripMenuItem()
        BarangMasukToolStripMenuItem = New ToolStripMenuItem()
        BarangKeluarToolStripMenuItem = New ToolStripMenuItem()
        MenuStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.ImageScalingSize = New Size(32, 32)
        MenuStrip1.Items.AddRange(New ToolStripItem() {FileToolStripMenuItem, DatabaseToolStripMenuItem, TransaksiToolStripMenuItem, LaporanToolStripMenuItem, TentangToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(800, 40)
        MenuStrip1.TabIndex = 1
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' FileToolStripMenuItem
        ' 
        FileToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {mnLogout, mnExit})
        FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        FileToolStripMenuItem.Size = New Size(110, 36)
        FileToolStripMenuItem.Text = "System"
        ' 
        ' mnLogout
        ' 
        mnLogout.Name = "mnLogout"
        mnLogout.Size = New Size(359, 44)
        mnLogout.Text = "Logout"
        ' 
        ' mnExit
        ' 
        mnExit.Name = "mnExit"
        mnExit.Size = New Size(359, 44)
        mnExit.Text = "Exit"
        ' 
        ' DatabaseToolStripMenuItem
        ' 
        DatabaseToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {DataKaryawanToolStripMenuItem, DataBarangToolStripMenuItem})
        DatabaseToolStripMenuItem.Name = "DatabaseToolStripMenuItem"
        DatabaseToolStripMenuItem.Size = New Size(132, 38)
        DatabaseToolStripMenuItem.Text = "Database"
        ' 
        ' TransaksiToolStripMenuItem
        ' 
        TransaksiToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {BarangMasukToolStripMenuItem, BarangKeluarToolStripMenuItem})
        TransaksiToolStripMenuItem.Name = "TransaksiToolStripMenuItem"
        TransaksiToolStripMenuItem.Size = New Size(129, 38)
        TransaksiToolStripMenuItem.Text = "Transaksi"
        ' 
        ' LaporanToolStripMenuItem
        ' 
        LaporanToolStripMenuItem.Name = "LaporanToolStripMenuItem"
        LaporanToolStripMenuItem.Size = New Size(119, 38)
        LaporanToolStripMenuItem.Text = "Laporan"
        ' 
        ' TentangToolStripMenuItem
        ' 
        TentangToolStripMenuItem.Name = "TentangToolStripMenuItem"
        TentangToolStripMenuItem.Size = New Size(120, 38)
        TentangToolStripMenuItem.Text = "Tentang"
        ' 
        ' DataKaryawanToolStripMenuItem
        ' 
        DataKaryawanToolStripMenuItem.Name = "DataKaryawanToolStripMenuItem"
        DataKaryawanToolStripMenuItem.Size = New Size(359, 44)
        DataKaryawanToolStripMenuItem.Text = "Data Karyawan"
        ' 
        ' DataBarangToolStripMenuItem
        ' 
        DataBarangToolStripMenuItem.Name = "DataBarangToolStripMenuItem"
        DataBarangToolStripMenuItem.Size = New Size(359, 44)
        DataBarangToolStripMenuItem.Text = "Data Barang"
        ' 
        ' BarangMasukToolStripMenuItem
        ' 
        BarangMasukToolStripMenuItem.Name = "BarangMasukToolStripMenuItem"
        BarangMasukToolStripMenuItem.Size = New Size(359, 44)
        BarangMasukToolStripMenuItem.Text = "Barang Masuk"
        ' 
        ' BarangKeluarToolStripMenuItem
        ' 
        BarangKeluarToolStripMenuItem.Name = "BarangKeluarToolStripMenuItem"
        BarangKeluarToolStripMenuItem.Size = New Size(359, 44)
        BarangKeluarToolStripMenuItem.Text = "Barang Keluar"
        ' 
        ' formMenu
        ' 
        AutoScaleDimensions = New SizeF(13F, 32F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(MenuStrip1)
        IsMdiContainer = True
        MainMenuStrip = MenuStrip1
        Name = "formMenu"
        Text = "Menu"
        WindowState = FormWindowState.Maximized
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents mnLogout As ToolStripMenuItem
    Friend WithEvents mnExit As ToolStripMenuItem
    Friend WithEvents DatabaseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DataKaryawanToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DataBarangToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TransaksiToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BarangMasukToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BarangKeluarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LaporanToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TentangToolStripMenuItem As ToolStripMenuItem

End Class
