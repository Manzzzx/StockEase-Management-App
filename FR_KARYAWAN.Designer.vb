<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FR_KARYAWAN
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
        Label6 = New Label()
        Label7 = New Label()
        txtNIK = New TextBox()
        txtNoHP = New TextBox()
        txtNama = New TextBox()
        txtAsal = New TextBox()
        dtpTanggal = New DateTimePicker()
        txtAlamat = New TextBox()
        cbJenis = New ComboBox()
        btnSave = New Button()
        btnUpdate = New Button()
        txtCari = New TextBox()
        DataGridView1 = New DataGridView()
        btnDelete = New Button()
        btnReset = New Button()
        btnExit = New Button()
        Label8 = New Label()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(34, 32)
        Label1.Name = "Label1"
        Label1.Size = New Size(52, 32)
        Label1.TabIndex = 0
        Label1.Text = "NIK"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(34, 103)
        Label2.Name = "Label2"
        Label2.Size = New Size(84, 32)
        Label2.TabIndex = 1
        Label2.Text = "NAMA"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(34, 249)
        Label3.Name = "Label3"
        Label3.Size = New Size(68, 32)
        Label3.TabIndex = 2
        Label3.Text = "ASAL"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(34, 177)
        Label4.Name = "Label4"
        Label4.Size = New Size(187, 32)
        Label4.TabIndex = 3
        Label4.Text = "TANGGAL LAHIR"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(686, 36)
        Label5.Name = "Label5"
        Label5.Size = New Size(178, 32)
        Label5.TabIndex = 4
        Label5.Text = "JENIS KELAMIN"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(686, 106)
        Label6.Name = "Label6"
        Label6.Size = New Size(104, 32)
        Label6.TabIndex = 5
        Label6.Text = "ALAMAT"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(686, 177)
        Label7.Name = "Label7"
        Label7.Size = New Size(87, 32)
        Label7.TabIndex = 6
        Label7.Text = "NO HP"
        ' 
        ' txtNIK
        ' 
        txtNIK.Location = New Point(235, 35)
        txtNIK.Name = "txtNIK"
        txtNIK.Size = New Size(399, 39)
        txtNIK.TabIndex = 7
        ' 
        ' txtNoHP
        ' 
        txtNoHP.Location = New Point(887, 180)
        txtNoHP.Name = "txtNoHP"
        txtNoHP.Size = New Size(399, 39)
        txtNoHP.TabIndex = 10
        ' 
        ' txtNama
        ' 
        txtNama.Location = New Point(235, 106)
        txtNama.Name = "txtNama"
        txtNama.Size = New Size(399, 39)
        txtNama.TabIndex = 12
        ' 
        ' txtAsal
        ' 
        txtAsal.Location = New Point(235, 252)
        txtAsal.Name = "txtAsal"
        txtAsal.Size = New Size(399, 39)
        txtAsal.TabIndex = 13
        ' 
        ' dtpTanggal
        ' 
        dtpTanggal.Location = New Point(235, 180)
        dtpTanggal.Name = "dtpTanggal"
        dtpTanggal.Size = New Size(399, 39)
        dtpTanggal.TabIndex = 14
        ' 
        ' txtAlamat
        ' 
        txtAlamat.Location = New Point(887, 109)
        txtAlamat.Name = "txtAlamat"
        txtAlamat.Size = New Size(399, 39)
        txtAlamat.TabIndex = 11
        ' 
        ' cbJenis
        ' 
        cbJenis.FormattingEnabled = True
        cbJenis.Items.AddRange(New Object() {"Laki-laki", "Perempuan"})
        cbJenis.Location = New Point(887, 38)
        cbJenis.Name = "cbJenis"
        cbJenis.Size = New Size(399, 40)
        cbJenis.TabIndex = 15
        ' 
        ' btnSave
        ' 
        btnSave.Location = New Point(38, 374)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(229, 55)
        btnSave.TabIndex = 16
        btnSave.Text = "Save"
        btnSave.UseVisualStyleBackColor = True
        ' 
        ' btnUpdate
        ' 
        btnUpdate.Location = New Point(293, 374)
        btnUpdate.Name = "btnUpdate"
        btnUpdate.Size = New Size(229, 55)
        btnUpdate.TabIndex = 17
        btnUpdate.Text = "Update"
        btnUpdate.UseVisualStyleBackColor = True
        ' 
        ' txtCari
        ' 
        txtCari.Location = New Point(166, 446)
        txtCari.Name = "txtCari"
        txtCari.Size = New Size(1121, 39)
        txtCari.TabIndex = 18
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(34, 501)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersWidth = 82
        DataGridView1.Size = New Size(1253, 586)
        DataGridView1.TabIndex = 19
        ' 
        ' btnDelete
        ' 
        btnDelete.Location = New Point(548, 374)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(229, 55)
        btnDelete.TabIndex = 20
        btnDelete.Text = "Delete"
        btnDelete.UseVisualStyleBackColor = True
        ' 
        ' btnReset
        ' 
        btnReset.Location = New Point(803, 374)
        btnReset.Name = "btnReset"
        btnReset.Size = New Size(229, 55)
        btnReset.TabIndex = 21
        btnReset.Text = "Reset"
        btnReset.UseVisualStyleBackColor = True
        ' 
        ' btnExit
        ' 
        btnExit.Location = New Point(1058, 374)
        btnExit.Name = "btnExit"
        btnExit.Size = New Size(229, 55)
        btnExit.TabIndex = 22
        btnExit.Text = "Exit"
        btnExit.UseVisualStyleBackColor = True
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(34, 449)
        Label8.Name = "Label8"
        Label8.Size = New Size(126, 32)
        Label8.TabIndex = 23
        Label8.Text = "CARI DATA"
        ' 
        ' FR_KARYAWAN
        ' 
        AutoScaleDimensions = New SizeF(13F, 32F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1321, 1114)
        Controls.Add(Label8)
        Controls.Add(btnExit)
        Controls.Add(btnReset)
        Controls.Add(btnDelete)
        Controls.Add(DataGridView1)
        Controls.Add(txtCari)
        Controls.Add(btnUpdate)
        Controls.Add(btnSave)
        Controls.Add(cbJenis)
        Controls.Add(dtpTanggal)
        Controls.Add(txtAsal)
        Controls.Add(txtNama)
        Controls.Add(txtAlamat)
        Controls.Add(txtNoHP)
        Controls.Add(txtNIK)
        Controls.Add(Label7)
        Controls.Add(Label6)
        Controls.Add(Label5)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Name = "FR_KARYAWAN"
        StartPosition = FormStartPosition.CenterScreen
        Text = "DATA KARYAWAN"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents txtNIK As TextBox
    Friend WithEvents txtNoHP As TextBox
    Friend WithEvents txtNama As TextBox
    Friend WithEvents txtAsal As TextBox
    Friend WithEvents dtpTanggal As DateTimePicker
    Friend WithEvents txtAlamat As TextBox
    Friend WithEvents cbJenis As ComboBox
    Friend WithEvents btnSave As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents txtCari As TextBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnReset As Button
    Friend WithEvents btnExit As Button
    Friend WithEvents Label8 As Label
End Class
