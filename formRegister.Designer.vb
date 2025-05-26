<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formRegister
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
        btnKembali = New Button()
        btnRegister = New Button()
        txtPassword = New TextBox()
        txtUsername = New TextBox()
        Label3 = New Label()
        Label2 = New Label()
        Label1 = New Label()
        txtKonfirmasi = New TextBox()
        Label5 = New Label()
        SuspendLayout()
        ' 
        ' btnKembali
        ' 
        btnKembali.Location = New Point(365, 582)
        btnKembali.Name = "btnKembali"
        btnKembali.Size = New Size(210, 65)
        btnKembali.TabIndex = 16
        btnKembali.Text = "Kembali"
        btnKembali.UseVisualStyleBackColor = True
        ' 
        ' btnRegister
        ' 
        btnRegister.Location = New Point(113, 582)
        btnRegister.Name = "btnRegister"
        btnRegister.Size = New Size(210, 65)
        btnRegister.TabIndex = 17
        btnRegister.Text = "Register"
        btnRegister.UseVisualStyleBackColor = True
        ' 
        ' txtPassword
        ' 
        txtPassword.Location = New Point(113, 330)
        txtPassword.Name = "txtPassword"
        txtPassword.Size = New Size(461, 39)
        txtPassword.TabIndex = 15
        txtPassword.UseSystemPasswordChar = True
        ' 
        ' txtUsername
        ' 
        txtUsername.Location = New Point(113, 206)
        txtUsername.Name = "txtUsername"
        txtUsername.Size = New Size(461, 39)
        txtUsername.TabIndex = 14
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.ForeColor = Color.White
        Label3.Location = New Point(113, 281)
        Label3.Name = "Label3"
        Label3.Size = New Size(111, 32)
        Label3.TabIndex = 12
        Label3.Text = "Password"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.ForeColor = Color.White
        Label2.Location = New Point(113, 151)
        Label2.Name = "Label2"
        Label2.Size = New Size(121, 32)
        Label2.TabIndex = 11
        Label2.Text = "Username"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 16.125F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.White
        Label1.Location = New Point(236, 43)
        Label1.Name = "Label1"
        Label1.Size = New Size(221, 59)
        Label1.TabIndex = 9
        Label1.Text = "REGISTER"
        ' 
        ' txtKonfirmasi
        ' 
        txtKonfirmasi.Location = New Point(113, 455)
        txtKonfirmasi.Name = "txtKonfirmasi"
        txtKonfirmasi.Size = New Size(461, 39)
        txtKonfirmasi.TabIndex = 19
        txtKonfirmasi.UseSystemPasswordChar = True
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.ForeColor = Color.White
        Label5.Location = New Point(113, 406)
        Label5.Name = "Label5"
        Label5.Size = New Size(230, 32)
        Label5.TabIndex = 18
        Label5.Text = "Konfirmasi Password"
        ' 
        ' formRegister
        ' 
        AutoScaleDimensions = New SizeF(13F, 32F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(30), CByte(30), CByte(47))
        ClientSize = New Size(680, 750)
        Controls.Add(txtKonfirmasi)
        Controls.Add(Label5)
        Controls.Add(btnKembali)
        Controls.Add(btnRegister)
        Controls.Add(txtPassword)
        Controls.Add(txtUsername)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.None
        Name = "formRegister"
        StartPosition = FormStartPosition.CenterScreen
        Text = "formRegister"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnKembali As Button
    Friend WithEvents btnRegister As Button
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtKonfirmasi As TextBox
    Friend WithEvents Label5 As Label
End Class
