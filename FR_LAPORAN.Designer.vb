<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FR_LAPORAN
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
        Panel1 = New Panel()
        btnCetak = New Button()
        btnTampil = New Button()
        tglSampai = New DateTimePicker()
        tglMulai = New DateTimePicker()
        Label3 = New Label()
        Label2 = New Label()
        Label1 = New Label()
        cbLaporan = New ComboBox()
        dgvtampil = New DataGridView()
        laporan = New Printing.PrintDocument()
        Panel1.SuspendLayout()
        CType(dgvtampil, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(btnCetak)
        Panel1.Controls.Add(btnTampil)
        Panel1.Controls.Add(tglSampai)
        Panel1.Controls.Add(tglMulai)
        Panel1.Controls.Add(Label3)
        Panel1.Controls.Add(Label2)
        Panel1.Controls.Add(Label1)
        Panel1.Controls.Add(cbLaporan)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(1909, 92)
        Panel1.TabIndex = 0
        ' 
        ' btnCetak
        ' 
        btnCetak.Location = New Point(1703, 26)
        btnCetak.Name = "btnCetak"
        btnCetak.Size = New Size(150, 46)
        btnCetak.TabIndex = 7
        btnCetak.Text = "Cetak"
        btnCetak.UseVisualStyleBackColor = True
        ' 
        ' btnTampil
        ' 
        btnTampil.Location = New Point(1514, 27)
        btnTampil.Name = "btnTampil"
        btnTampil.Size = New Size(150, 46)
        btnTampil.TabIndex = 6
        btnTampil.Text = "Tampil"
        btnTampil.UseVisualStyleBackColor = True
        ' 
        ' tglSampai
        ' 
        tglSampai.Format = DateTimePickerFormat.Short
        tglSampai.Location = New Point(1208, 27)
        tglSampai.Name = "tglSampai"
        tglSampai.Size = New Size(226, 39)
        tglSampai.TabIndex = 5
        ' 
        ' tglMulai
        ' 
        tglMulai.Format = DateTimePickerFormat.Short
        tglMulai.Location = New Point(752, 27)
        tglMulai.Name = "tglMulai"
        tglMulai.Size = New Size(208, 39)
        tglMulai.TabIndex = 4
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(1021, 30)
        Label3.Name = "Label3"
        Label3.Size = New Size(181, 32)
        Label3.TabIndex = 3
        Label3.Text = "Sampai Tanggal"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(583, 30)
        Label2.Name = "Label2"
        Label2.Size = New Size(163, 32)
        Label2.TabIndex = 2
        Label2.Text = "Mulai Tanggal"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(25, 30)
        Label1.Name = "Label1"
        Label1.Size = New Size(158, 32)
        Label1.TabIndex = 1
        Label1.Text = "Jenis Laporan"
        ' 
        ' cbLaporan
        ' 
        cbLaporan.DropDownStyle = ComboBoxStyle.DropDownList
        cbLaporan.FormattingEnabled = True
        cbLaporan.Items.AddRange(New Object() {"Laporan Barang Masuk", "Laporan Barang Keluar"})
        cbLaporan.Location = New Point(210, 26)
        cbLaporan.Name = "cbLaporan"
        cbLaporan.Size = New Size(285, 40)
        cbLaporan.TabIndex = 0
        ' 
        ' dgvtampil
        ' 
        dgvtampil.AllowUserToAddRows = False
        dgvtampil.AllowUserToDeleteRows = False
        dgvtampil.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvtampil.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgvtampil.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvtampil.Dock = DockStyle.Fill
        dgvtampil.Location = New Point(0, 92)
        dgvtampil.Name = "dgvtampil"
        dgvtampil.RowHeadersVisible = False
        dgvtampil.RowHeadersWidth = 82
        dgvtampil.Size = New Size(1909, 1152)
        dgvtampil.TabIndex = 1
        ' 
        ' FR_LAPORAN
        ' 
        AutoScaleDimensions = New SizeF(13F, 32F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1909, 1244)
        Controls.Add(dgvtampil)
        Controls.Add(Panel1)
        Name = "FR_LAPORAN"
        Text = "LAPORAN"
        Panel1.ResumeLayout(False)
        Panel1.PerformLayout()
        CType(dgvtampil, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents cbLaporan As ComboBox
    Friend WithEvents dgvtampil As DataGridView
    Friend WithEvents btnTampil As Button
    Friend WithEvents tglSampai As DateTimePicker
    Friend WithEvents tglMulai As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btnCetak As Button
    Friend WithEvents laporan As Printing.PrintDocument
End Class
