<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FR_MASUK
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
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Label5 = New Label()
        txtKode = New TextBox()
        txtJumlah = New TextBox()
        txtSuplier = New TextBox()
        txtHargaPartai = New TextBox()
        lblBarang = New Label()
        dgvTampil = New DataGridView()
        Panel1 = New Panel()
        pnCari = New Panel()
        dgvCari = New DataGridView()
        btnTutup = New Button()
        txtPanelCari = New TextBox()
        btnKeluar = New Button()
        btnReset = New Button()
        Label6 = New Label()
        lblSatuan = New Label()
        btnTambahStock = New Button()
        btnTambahBarang = New Button()
        btnCari = New Button()
        Panel2 = New Panel()
        txtCari = New TextBox()
        btnNext = New Button()
        btnPrev = New Button()
        CType(dgvTampil, ComponentModel.ISupportInitialize).BeginInit()
        Panel1.SuspendLayout()
        pnCari.SuspendLayout()
        CType(dgvCari, ComponentModel.ISupportInitialize).BeginInit()
        Panel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(22, 26)
        Label1.Name = "Label1"
        Label1.Size = New Size(173, 32)
        Label1.TabIndex = 0
        Label1.Text = "KODE BARANG"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(22, 99)
        Label2.Name = "Label2"
        Label2.Size = New Size(183, 32)
        Label2.TabIndex = 1
        Label2.Text = "NAMA BARANG"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(22, 245)
        Label3.Name = "Label3"
        Label3.Size = New Size(105, 32)
        Label3.TabIndex = 2
        Label3.Text = "JUMLAH"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(22, 318)
        Label4.Name = "Label4"
        Label4.Size = New Size(99, 32)
        Label4.TabIndex = 3
        Label4.Text = "SUPLIER"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(22, 391)
        Label5.Name = "Label5"
        Label5.Size = New Size(169, 32)
        Label5.TabIndex = 4
        Label5.Text = "HARGA PARTAI"
        ' 
        ' txtKode
        ' 
        txtKode.Location = New Point(222, 19)
        txtKode.Name = "txtKode"
        txtKode.Size = New Size(210, 39)
        txtKode.TabIndex = 5
        ' 
        ' txtJumlah
        ' 
        txtJumlah.Location = New Point(222, 242)
        txtJumlah.Name = "txtJumlah"
        txtJumlah.Size = New Size(210, 39)
        txtJumlah.TabIndex = 6
        ' 
        ' txtSuplier
        ' 
        txtSuplier.Location = New Point(222, 315)
        txtSuplier.Name = "txtSuplier"
        txtSuplier.Size = New Size(465, 39)
        txtSuplier.TabIndex = 7
        ' 
        ' txtHargaPartai
        ' 
        txtHargaPartai.Location = New Point(222, 388)
        txtHargaPartai.Name = "txtHargaPartai"
        txtHargaPartai.Size = New Size(465, 39)
        txtHargaPartai.TabIndex = 8
        ' 
        ' lblBarang
        ' 
        lblBarang.AutoSize = True
        lblBarang.Location = New Point(222, 99)
        lblBarang.Name = "lblBarang"
        lblBarang.Size = New Size(24, 32)
        lblBarang.TabIndex = 9
        lblBarang.Text = "-"
        ' 
        ' dgvTampil
        ' 
        dgvTampil.AllowUserToAddRows = False
        dgvTampil.AllowUserToDeleteRows = False
        dgvTampil.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvTampil.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvTampil.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvTampil.Dock = DockStyle.Fill
        dgvTampil.Location = New Point(723, 77)
        dgvTampil.Name = "dgvTampil"
        dgvTampil.ReadOnly = True
        dgvTampil.RowHeadersVisible = False
        dgvTampil.RowHeadersWidth = 82
        dgvTampil.Size = New Size(1616, 843)
        dgvTampil.TabIndex = 10
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(pnCari)
        Panel1.Controls.Add(btnKeluar)
        Panel1.Controls.Add(btnReset)
        Panel1.Controls.Add(Label6)
        Panel1.Controls.Add(lblSatuan)
        Panel1.Controls.Add(btnTambahStock)
        Panel1.Controls.Add(btnTambahBarang)
        Panel1.Controls.Add(btnCari)
        Panel1.Controls.Add(Label1)
        Panel1.Controls.Add(Label2)
        Panel1.Controls.Add(lblBarang)
        Panel1.Controls.Add(Label3)
        Panel1.Controls.Add(txtHargaPartai)
        Panel1.Controls.Add(Label4)
        Panel1.Controls.Add(txtSuplier)
        Panel1.Controls.Add(Label5)
        Panel1.Controls.Add(txtJumlah)
        Panel1.Controls.Add(txtKode)
        Panel1.Dock = DockStyle.Left
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(723, 920)
        Panel1.TabIndex = 11
        ' 
        ' pnCari
        ' 
        pnCari.Controls.Add(dgvCari)
        pnCari.Controls.Add(btnTutup)
        pnCari.Controls.Add(txtPanelCari)
        pnCari.Location = New Point(0, 551)
        pnCari.Name = "pnCari"
        pnCari.Size = New Size(723, 386)
        pnCari.TabIndex = 15
        pnCari.Visible = False
        ' 
        ' dgvCari
        ' 
        dgvCari.AllowUserToAddRows = False
        dgvCari.AllowUserToDeleteRows = False
        dgvCari.AllowUserToResizeColumns = False
        dgvCari.AllowUserToResizeRows = False
        dgvCari.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvCari.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvCari.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvCari.Location = New Point(0, 77)
        dgvCari.Name = "dgvCari"
        dgvCari.RowHeadersVisible = False
        dgvCari.RowHeadersWidth = 82
        dgvCari.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvCari.Size = New Size(720, 857)
        dgvCari.TabIndex = 17
        ' 
        ' btnTutup
        ' 
        btnTutup.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnTutup.Location = New Point(562, 19)
        btnTutup.Name = "btnTutup"
        btnTutup.Size = New Size(140, 39)
        btnTutup.TabIndex = 16
        btnTutup.Text = "TUTUP"
        btnTutup.UseVisualStyleBackColor = True
        ' 
        ' txtPanelCari
        ' 
        txtPanelCari.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtPanelCari.Location = New Point(12, 19)
        txtPanelCari.Name = "txtPanelCari"
        txtPanelCari.Size = New Size(532, 39)
        txtPanelCari.TabIndex = 16
        ' 
        ' btnKeluar
        ' 
        btnKeluar.Location = New Point(472, 461)
        btnKeluar.Name = "btnKeluar"
        btnKeluar.Size = New Size(215, 60)
        btnKeluar.TabIndex = 17
        btnKeluar.Text = "KELUAR"
        btnKeluar.UseVisualStyleBackColor = True
        ' 
        ' btnReset
        ' 
        btnReset.Location = New Point(470, 122)
        btnReset.Name = "btnReset"
        btnReset.Size = New Size(217, 58)
        btnReset.TabIndex = 16
        btnReset.Text = "RESET"
        btnReset.UseVisualStyleBackColor = True
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(22, 172)
        Label6.Name = "Label6"
        Label6.Size = New Size(101, 32)
        Label6.TabIndex = 13
        Label6.Text = "SATUAN"
        ' 
        ' lblSatuan
        ' 
        lblSatuan.AutoSize = True
        lblSatuan.Location = New Point(222, 172)
        lblSatuan.Name = "lblSatuan"
        lblSatuan.Size = New Size(24, 32)
        lblSatuan.TabIndex = 14
        lblSatuan.Text = "-"
        ' 
        ' btnTambahStock
        ' 
        btnTambahStock.Location = New Point(222, 461)
        btnTambahStock.Name = "btnTambahStock"
        btnTambahStock.Size = New Size(215, 60)
        btnTambahStock.TabIndex = 12
        btnTambahStock.Text = "TAMBAH STOCK"
        btnTambahStock.UseVisualStyleBackColor = True
        ' 
        ' btnTambahBarang
        ' 
        btnTambahBarang.Location = New Point(470, 232)
        btnTambahBarang.Name = "btnTambahBarang"
        btnTambahBarang.Size = New Size(217, 58)
        btnTambahBarang.TabIndex = 11
        btnTambahBarang.Text = "TAMBAH BARANG"
        btnTambahBarang.UseVisualStyleBackColor = True
        ' 
        ' btnCari
        ' 
        btnCari.Location = New Point(470, 17)
        btnCari.Name = "btnCari"
        btnCari.Size = New Size(217, 51)
        btnCari.TabIndex = 10
        btnCari.Text = "CARI"
        btnCari.UseVisualStyleBackColor = True
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(btnPrev)
        Panel2.Controls.Add(btnNext)
        Panel2.Controls.Add(txtCari)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(723, 0)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(1616, 77)
        Panel2.TabIndex = 12
        ' 
        ' txtCari
        ' 
        txtCari.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtCari.Location = New Point(6, 19)
        txtCari.Name = "txtCari"
        txtCari.Size = New Size(1311, 39)
        txtCari.TabIndex = 10
        ' 
        ' btnNext
        ' 
        btnNext.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnNext.Location = New Point(1489, 17)
        btnNext.Name = "btnNext"
        btnNext.Size = New Size(115, 41)
        btnNext.TabIndex = 18
        btnNext.Text = ">"
        btnNext.UseVisualStyleBackColor = True
        ' 
        ' btnPrev
        ' 
        btnPrev.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnPrev.Location = New Point(1356, 18)
        btnPrev.Name = "btnPrev"
        btnPrev.Size = New Size(115, 41)
        btnPrev.TabIndex = 19
        btnPrev.Text = "<"
        btnPrev.UseVisualStyleBackColor = True
        ' 
        ' FR_MASUK
        ' 
        AutoScaleDimensions = New SizeF(13F, 32F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(2339, 920)
        Controls.Add(dgvTampil)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        Name = "FR_MASUK"
        Text = "BARANG MASUK"
        CType(dgvTampil, ComponentModel.ISupportInitialize).EndInit()
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        pnCari.ResumeLayout(False)
        pnCari.PerformLayout()
        CType(dgvCari, ComponentModel.ISupportInitialize).EndInit()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txtKode As TextBox
    Friend WithEvents txtJumlah As TextBox
    Friend WithEvents txtSuplier As TextBox
    Friend WithEvents txtHargaPartai As TextBox
    Friend WithEvents lblBarang As Label
    Friend WithEvents dgvTampil As DataGridView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents txtCari As TextBox
    Friend WithEvents btnTambahStock As Button
    Friend WithEvents btnTambahBarang As Button
    Friend WithEvents btnCari As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents lblSatuan As Label
    Friend WithEvents pnCari As Panel
    Friend WithEvents dgvCari As DataGridView
    Friend WithEvents btnTutup As Button
    Friend WithEvents txtPanelCari As TextBox
    Friend WithEvents btnKeluar As Button
    Friend WithEvents btnReset As Button
    Friend WithEvents btnPrev As Button
    Friend WithEvents btnNext As Button
End Class
