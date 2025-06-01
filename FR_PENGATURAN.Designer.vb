<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FR_PENGATURAN
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
        GroupBox1 = New GroupBox()
        Button2 = New Button()
        Button1 = New Button()
        cbPrinterToko = New ComboBox()
        txtTelpToko = New TextBox()
        txtAlamatToko = New TextBox()
        txtNamaToko = New TextBox()
        Label4 = New Label()
        Label3 = New Label()
        Label2 = New Label()
        Label1 = New Label()
        GroupBox1.SuspendLayout()
        SuspendLayout()
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(Button2)
        GroupBox1.Controls.Add(Button1)
        GroupBox1.Controls.Add(cbPrinterToko)
        GroupBox1.Controls.Add(txtTelpToko)
        GroupBox1.Controls.Add(txtAlamatToko)
        GroupBox1.Controls.Add(txtNamaToko)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Location = New Point(0, 0)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(749, 365)
        GroupBox1.TabIndex = 0
        GroupBox1.TabStop = False
        GroupBox1.Text = "GroupBox1"
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(484, 289)
        Button2.Name = "Button2"
        Button2.Size = New Size(150, 46)
        Button2.TabIndex = 9
        Button2.Text = "Kembali"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(247, 289)
        Button1.Name = "Button1"
        Button1.Size = New Size(150, 46)
        Button1.TabIndex = 8
        Button1.Text = "Simpan"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' cbPrinterToko
        ' 
        cbPrinterToko.FormattingEnabled = True
        cbPrinterToko.Location = New Point(247, 221)
        cbPrinterToko.Name = "cbPrinterToko"
        cbPrinterToko.Size = New Size(387, 40)
        cbPrinterToko.TabIndex = 7
        ' 
        ' txtTelpToko
        ' 
        txtTelpToko.Location = New Point(247, 165)
        txtTelpToko.Name = "txtTelpToko"
        txtTelpToko.Size = New Size(387, 39)
        txtTelpToko.TabIndex = 6
        ' 
        ' txtAlamatToko
        ' 
        txtAlamatToko.Location = New Point(247, 109)
        txtAlamatToko.Name = "txtAlamatToko"
        txtAlamatToko.Size = New Size(387, 39)
        txtAlamatToko.TabIndex = 5
        ' 
        ' txtNamaToko
        ' 
        txtNamaToko.Location = New Point(247, 52)
        txtNamaToko.Name = "txtNamaToko"
        txtNamaToko.Size = New Size(387, 39)
        txtNamaToko.TabIndex = 4
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(54, 225)
        Label4.Name = "Label4"
        Label4.Size = New Size(143, 32)
        Label4.TabIndex = 3
        Label4.Text = "Printer Nota"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(54, 168)
        Label3.Name = "Label3"
        Label3.Size = New Size(116, 32)
        Label3.TabIndex = 2
        Label3.Text = "Telp Toko"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(54, 112)
        Label2.Name = "Label2"
        Label2.Size = New Size(146, 32)
        Label2.TabIndex = 1
        Label2.Text = "Alamat Toko"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(54, 55)
        Label1.Name = "Label1"
        Label1.Size = New Size(135, 32)
        Label1.TabIndex = 0
        Label1.Text = "Nama Toko"
        ' 
        ' FR_PENGATURAN
        ' 
        AutoScaleDimensions = New SizeF(13F, 32F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(749, 388)
        Controls.Add(GroupBox1)
        FormBorderStyle = FormBorderStyle.None
        Name = "FR_PENGATURAN"
        StartPosition = FormStartPosition.CenterScreen
        Text = "FR_PENGATURAN"
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents cbPrinterToko As ComboBox
    Friend WithEvents txtTelpToko As TextBox
    Friend WithEvents txtAlamatToko As TextBox
    Friend WithEvents txtNamaToko As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
End Class
