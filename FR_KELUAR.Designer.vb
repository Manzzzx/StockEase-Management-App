<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FR_KELUAR
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Panel1 = New Panel()
        GroupBox3 = New GroupBox()
        GroupBox2 = New GroupBox()
        lblHarga = New Label()
        GroupBox1 = New GroupBox()
        txtJumlah = New TextBox()
        Label5 = New Label()
        btnCari = New Button()
        txtHarga = New TextBox()
        txtSatuan = New TextBox()
        txtBarang = New TextBox()
        txtKode = New TextBox()
        Label4 = New Label()
        Label3 = New Label()
        Label2 = New Label()
        Label1 = New Label()
        Panel2 = New Panel()
        btnMinimize = New Button()
        btnMenu = New Button()
        btnBayar = New Button()
        dgvTampil = New DataGridView()
        Kode = New DataGridViewTextBoxColumn()
        Barang = New DataGridViewTextBoxColumn()
        Satuan = New DataGridViewTextBoxColumn()
        Harga = New DataGridViewTextBoxColumn()
        Qty = New DataGridViewTextBoxColumn()
        Total = New DataGridViewTextBoxColumn()
        ContextMenuStrip1 = New ContextMenuStrip(components)
        HapusToolStripMenuItem = New ToolStripMenuItem()
        Panel1.SuspendLayout()
        GroupBox2.SuspendLayout()
        GroupBox1.SuspendLayout()
        Panel2.SuspendLayout()
        CType(dgvTampil, ComponentModel.ISupportInitialize).BeginInit()
        ContextMenuStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(GroupBox3)
        Panel1.Controls.Add(GroupBox2)
        Panel1.Controls.Add(GroupBox1)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(2686, 397)
        Panel1.TabIndex = 0
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Dock = DockStyle.Fill
        GroupBox3.Font = New Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        GroupBox3.Location = New Point(604, 0)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Size = New Size(849, 397)
        GroupBox3.TabIndex = 2
        GroupBox3.TabStop = False
        GroupBox3.Text = "Pembayaran"
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(lblHarga)
        GroupBox2.Dock = DockStyle.Right
        GroupBox2.Font = New Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        GroupBox2.Location = New Point(1453, 0)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(1233, 397)
        GroupBox2.TabIndex = 1
        GroupBox2.TabStop = False
        GroupBox2.Text = "Total Harga"
        ' 
        ' lblHarga
        ' 
        lblHarga.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        lblHarga.Font = New Font("Arial", 48F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblHarga.Location = New Point(49, 40)
        lblHarga.Name = "lblHarga"
        lblHarga.Size = New Size(1129, 293)
        lblHarga.TabIndex = 0
        lblHarga.Text = "0"
        lblHarga.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(txtJumlah)
        GroupBox1.Controls.Add(Label5)
        GroupBox1.Controls.Add(btnCari)
        GroupBox1.Controls.Add(txtHarga)
        GroupBox1.Controls.Add(txtSatuan)
        GroupBox1.Controls.Add(txtBarang)
        GroupBox1.Controls.Add(txtKode)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Dock = DockStyle.Left
        GroupBox1.Location = New Point(0, 0)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(604, 397)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        GroupBox1.Text = "Data Barang"
        ' 
        ' txtJumlah
        ' 
        txtJumlah.Location = New Point(170, 321)
        txtJumlah.Name = "txtJumlah"
        txtJumlah.Size = New Size(384, 39)
        txtJumlah.TabIndex = 10
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(30, 320)
        Label5.Name = "Label5"
        Label5.Size = New Size(90, 32)
        Label5.TabIndex = 9
        Label5.Text = "Jumlah"
        ' 
        ' btnCari
        ' 
        btnCari.Location = New Point(404, 42)
        btnCari.Name = "btnCari"
        btnCari.Size = New Size(150, 39)
        btnCari.TabIndex = 8
        btnCari.Text = "Cari (F1)"
        btnCari.UseVisualStyleBackColor = True
        ' 
        ' txtHarga
        ' 
        txtHarga.Location = New Point(170, 251)
        txtHarga.Name = "txtHarga"
        txtHarga.ReadOnly = True
        txtHarga.Size = New Size(384, 39)
        txtHarga.TabIndex = 7
        ' 
        ' txtSatuan
        ' 
        txtSatuan.Location = New Point(170, 181)
        txtSatuan.Name = "txtSatuan"
        txtSatuan.ReadOnly = True
        txtSatuan.Size = New Size(384, 39)
        txtSatuan.TabIndex = 6
        ' 
        ' txtBarang
        ' 
        txtBarang.Location = New Point(170, 111)
        txtBarang.Name = "txtBarang"
        txtBarang.ReadOnly = True
        txtBarang.Size = New Size(384, 39)
        txtBarang.TabIndex = 5
        ' 
        ' txtKode
        ' 
        txtKode.Location = New Point(170, 42)
        txtKode.Name = "txtKode"
        txtKode.Size = New Size(220, 39)
        txtKode.TabIndex = 4
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(30, 246)
        Label4.Name = "Label4"
        Label4.Size = New Size(77, 32)
        Label4.TabIndex = 3
        Label4.Text = "Harga"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(31, 180)
        Label3.Name = "Label3"
        Label3.Size = New Size(87, 32)
        Label3.TabIndex = 2
        Label3.Text = "Satuan"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(30, 114)
        Label2.Name = "Label2"
        Label2.Size = New Size(88, 32)
        Label2.TabIndex = 1
        Label2.Text = "Barang"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(30, 45)
        Label1.Name = "Label1"
        Label1.Size = New Size(68, 32)
        Label1.TabIndex = 0
        Label1.Text = "Kode"
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(btnMinimize)
        Panel2.Controls.Add(btnMenu)
        Panel2.Controls.Add(btnBayar)
        Panel2.Dock = DockStyle.Bottom
        Panel2.Location = New Point(0, 1147)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(2686, 128)
        Panel2.TabIndex = 1
        ' 
        ' btnMinimize
        ' 
        btnMinimize.Location = New Point(499, 32)
        btnMinimize.Name = "btnMinimize"
        btnMinimize.Size = New Size(176, 70)
        btnMinimize.TabIndex = 2
        btnMinimize.Text = "Minimize"
        btnMinimize.UseVisualStyleBackColor = True
        ' 
        ' btnMenu
        ' 
        btnMenu.Location = New Point(238, 32)
        btnMenu.Name = "btnMenu"
        btnMenu.Size = New Size(228, 70)
        btnMenu.TabIndex = 1
        btnMenu.Text = "Kembali Ke Menu"
        btnMenu.UseVisualStyleBackColor = True
        ' 
        ' btnBayar
        ' 
        btnBayar.Location = New Point(30, 32)
        btnBayar.Name = "btnBayar"
        btnBayar.Size = New Size(176, 70)
        btnBayar.TabIndex = 0
        btnBayar.Text = "Bayar (F2)"
        btnBayar.UseVisualStyleBackColor = True
        ' 
        ' dgvTampil
        ' 
        dgvTampil.AllowUserToAddRows = False
        dgvTampil.AllowUserToResizeColumns = False
        dgvTampil.AllowUserToResizeRows = False
        dgvTampil.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvTampil.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvTampil.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvTampil.Columns.AddRange(New DataGridViewColumn() {Kode, Barang, Satuan, Harga, Qty, Total})
        dgvTampil.ContextMenuStrip = ContextMenuStrip1
        dgvTampil.Dock = DockStyle.Fill
        dgvTampil.Location = New Point(0, 397)
        dgvTampil.Name = "dgvTampil"
        dgvTampil.RowHeadersVisible = False
        dgvTampil.RowHeadersWidth = 82
        dgvTampil.Size = New Size(2686, 750)
        dgvTampil.TabIndex = 2
        ' 
        ' Kode
        ' 
        Kode.HeaderText = "KODE"
        Kode.MinimumWidth = 10
        Kode.Name = "Kode"
        Kode.ReadOnly = True
        ' 
        ' Barang
        ' 
        Barang.HeaderText = "BARANG"
        Barang.MinimumWidth = 10
        Barang.Name = "Barang"
        Barang.ReadOnly = True
        ' 
        ' Satuan
        ' 
        Satuan.HeaderText = "SATUAN"
        Satuan.MinimumWidth = 10
        Satuan.Name = "Satuan"
        Satuan.ReadOnly = True
        ' 
        ' Harga
        ' 
        Harga.HeaderText = "HARGA"
        Harga.MinimumWidth = 10
        Harga.Name = "Harga"
        Harga.ReadOnly = True
        ' 
        ' Qty
        ' 
        Qty.HeaderText = "JUMLAH"
        Qty.MinimumWidth = 10
        Qty.Name = "Qty"
        ' 
        ' Total
        ' 
        Total.HeaderText = "TOTAL"
        Total.MinimumWidth = 10
        Total.Name = "Total"
        Total.ReadOnly = True
        ' 
        ' ContextMenuStrip1
        ' 
        ContextMenuStrip1.ImageScalingSize = New Size(32, 32)
        ContextMenuStrip1.Items.AddRange(New ToolStripItem() {HapusToolStripMenuItem})
        ContextMenuStrip1.Name = "ContextMenuStrip1"
        ContextMenuStrip1.Size = New Size(156, 42)
        ' 
        ' HapusToolStripMenuItem
        ' 
        HapusToolStripMenuItem.Name = "HapusToolStripMenuItem"
        HapusToolStripMenuItem.Size = New Size(155, 38)
        HapusToolStripMenuItem.Text = "Hapus"
        ' 
        ' FR_KELUAR
        ' 
        AutoScaleDimensions = New SizeF(13F, 32F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(2686, 1275)
        Controls.Add(dgvTampil)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        FormBorderStyle = FormBorderStyle.None
        Name = "FR_KELUAR"
        Text = "BARANG KELUAR"
        WindowState = FormWindowState.Maximized
        Panel1.ResumeLayout(False)
        GroupBox2.ResumeLayout(False)
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        Panel2.ResumeLayout(False)
        CType(dgvTampil, ComponentModel.ISupportInitialize).EndInit()
        ContextMenuStrip1.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents dgvTampil As DataGridView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btnCari As Button
    Friend WithEvents txtHarga As TextBox
    Friend WithEvents txtSatuan As TextBox
    Friend WithEvents txtBarang As TextBox
    Friend WithEvents txtKode As TextBox
    Friend WithEvents btnMinimize As Button
    Friend WithEvents btnMenu As Button
    Friend WithEvents btnBayar As Button
    Friend WithEvents lblHarga As Label
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents HapusToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Kode As DataGridViewTextBoxColumn
    Friend WithEvents Barang As DataGridViewTextBoxColumn
    Friend WithEvents Satuan As DataGridViewTextBoxColumn
    Friend WithEvents Harga As DataGridViewTextBoxColumn
    Friend WithEvents Qty As DataGridViewTextBoxColumn
    Friend WithEvents Total As DataGridViewTextBoxColumn
    Friend WithEvents txtJumlah As TextBox
End Class
