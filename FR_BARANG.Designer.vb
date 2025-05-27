<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FR_BARANG
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
        DataGridView1 = New DataGridView()
        txtKodeBarang = New TextBox()
        txtHarga = New TextBox()
        txtNamaBarang = New TextBox()
        txtCari = New TextBox()
        cbSatuan = New ComboBox()
        btnSave = New Button()
        btnUpdate = New Button()
        btnDelete = New Button()
        btnReset = New Button()
        btnExit = New Button()
        Label5 = New Label()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(52, 69)
        Label1.Name = "Label1"
        Label1.Size = New Size(173, 32)
        Label1.TabIndex = 0
        Label1.Text = "KODE BARANG"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(52, 128)
        Label2.Name = "Label2"
        Label2.Size = New Size(183, 32)
        Label2.TabIndex = 1
        Label2.Text = "NAMA BARANG"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(632, 69)
        Label3.Name = "Label3"
        Label3.Size = New Size(101, 32)
        Label3.TabIndex = 2
        Label3.Text = "SATUAN"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(632, 134)
        Label4.Name = "Label4"
        Label4.Size = New Size(185, 32)
        Label4.TabIndex = 3
        Label4.Text = "HARGA SATUAN"
        ' 
        ' DataGridView1
        ' 
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(52, 359)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.ReadOnly = True
        DataGridView1.RowHeadersWidth = 82
        DataGridView1.Size = New Size(1074, 549)
        DataGridView1.TabIndex = 4
        ' 
        ' txtKodeBarang
        ' 
        txtKodeBarang.Location = New Point(266, 69)
        txtKodeBarang.Name = "txtKodeBarang"
        txtKodeBarang.Size = New Size(280, 39)
        txtKodeBarang.TabIndex = 5
        ' 
        ' txtHarga
        ' 
        txtHarga.Location = New Point(846, 127)
        txtHarga.Name = "txtHarga"
        txtHarga.Size = New Size(280, 39)
        txtHarga.TabIndex = 6
        ' 
        ' txtNamaBarang
        ' 
        txtNamaBarang.Location = New Point(266, 125)
        txtNamaBarang.Name = "txtNamaBarang"
        txtNamaBarang.Size = New Size(280, 39)
        txtNamaBarang.TabIndex = 7
        ' 
        ' txtCari
        ' 
        txtCari.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        txtCari.Location = New Point(253, 300)
        txtCari.Name = "txtCari"
        txtCari.Size = New Size(873, 39)
        txtCari.TabIndex = 8
        ' 
        ' cbSatuan
        ' 
        cbSatuan.FormattingEnabled = True
        cbSatuan.Location = New Point(846, 66)
        cbSatuan.Name = "cbSatuan"
        cbSatuan.Size = New Size(280, 40)
        cbSatuan.TabIndex = 9
        ' 
        ' btnSave
        ' 
        btnSave.Location = New Point(52, 211)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(196, 63)
        btnSave.TabIndex = 10
        btnSave.Text = "Save"
        btnSave.UseVisualStyleBackColor = True
        ' 
        ' btnUpdate
        ' 
        btnUpdate.Location = New Point(266, 211)
        btnUpdate.Name = "btnUpdate"
        btnUpdate.Size = New Size(196, 63)
        btnUpdate.TabIndex = 11
        btnUpdate.Text = "Update"
        btnUpdate.UseVisualStyleBackColor = True
        ' 
        ' btnDelete
        ' 
        btnDelete.Location = New Point(486, 211)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(196, 63)
        btnDelete.TabIndex = 12
        btnDelete.Text = "Delete"
        btnDelete.UseVisualStyleBackColor = True
        ' 
        ' btnReset
        ' 
        btnReset.Location = New Point(707, 211)
        btnReset.Name = "btnReset"
        btnReset.Size = New Size(196, 63)
        btnReset.TabIndex = 13
        btnReset.Text = "Reset"
        btnReset.UseVisualStyleBackColor = True
        ' 
        ' btnExit
        ' 
        btnExit.Location = New Point(930, 211)
        btnExit.Name = "btnExit"
        btnExit.Size = New Size(196, 63)
        btnExit.TabIndex = 14
        btnExit.Text = "Exit"
        btnExit.UseVisualStyleBackColor = True
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(52, 303)
        Label5.Name = "Label5"
        Label5.Size = New Size(163, 32)
        Label5.TabIndex = 15
        Label5.Text = "CARI BARANG"
        ' 
        ' FR_BARANG
        ' 
        AutoScaleDimensions = New SizeF(13F, 32F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1176, 944)
        Controls.Add(Label5)
        Controls.Add(btnExit)
        Controls.Add(btnReset)
        Controls.Add(btnDelete)
        Controls.Add(btnUpdate)
        Controls.Add(btnSave)
        Controls.Add(cbSatuan)
        Controls.Add(txtCari)
        Controls.Add(txtNamaBarang)
        Controls.Add(txtHarga)
        Controls.Add(txtKodeBarang)
        Controls.Add(DataGridView1)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.None
        Name = "FR_BARANG"
        StartPosition = FormStartPosition.CenterScreen
        Text = "DATA BARANG"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents txtKodeBarang As TextBox
    Friend WithEvents txtHarga As TextBox
    Friend WithEvents txtNamaBarang As TextBox
    Friend WithEvents txtCari As TextBox
    Friend WithEvents cbSatuan As ComboBox
    Friend WithEvents btnSave As Button
    Friend WithEvents btnUpdate As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnReset As Button
    Friend WithEvents btnExit As Button
    Friend WithEvents Label5 As Label
End Class
