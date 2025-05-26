<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formLogin
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
        linkRegister = New LinkLabel()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        txtUsername = New TextBox()
        txtPassword = New TextBox()
        btnLogin = New Button()
        btnKeluar = New Button()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 16.125F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.White
        Label1.Location = New Point(257, 32)
        Label1.Name = "Label1"
        Label1.Size = New Size(158, 59)
        Label1.TabIndex = 0
        Label1.Text = "LOGIN"
        ' 
        ' linkRegister
        ' 
        linkRegister.AutoSize = True
        linkRegister.LinkColor = Color.LightBlue
        linkRegister.Location = New Point(392, 639)
        linkRegister.Name = "linkRegister"
        linkRegister.Size = New Size(112, 37)
        linkRegister.TabIndex = 1
        linkRegister.TabStop = True
        linkRegister.Text = "Register"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.ForeColor = Color.White
        Label2.Location = New Point(109, 190)
        Label2.Name = "Label2"
        Label2.Size = New Size(136, 37)
        Label2.TabIndex = 2
        Label2.Text = "Username"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.ForeColor = Color.White
        Label3.Location = New Point(109, 353)
        Label3.Name = "Label3"
        Label3.Size = New Size(128, 37)
        Label3.TabIndex = 3
        Label3.Text = "Password"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.ForeColor = Color.White
        Label4.Location = New Point(148, 639)
        Label4.Name = "Label4"
        Label4.Size = New Size(247, 37)
        Label4.TabIndex = 4
        Label4.Text = "Belum punya akun?"
        ' 
        ' txtUsername
        ' 
        txtUsername.Location = New Point(109, 255)
        txtUsername.Name = "txtUsername"
        txtUsername.Size = New Size(461, 43)
        txtUsername.TabIndex = 5
        ' 
        ' txtPassword
        ' 
        txtPassword.Location = New Point(109, 412)
        txtPassword.Name = "txtPassword"
        txtPassword.Size = New Size(461, 43)
        txtPassword.TabIndex = 6
        txtPassword.UseSystemPasswordChar = True
        ' 
        ' btnLogin
        ' 
        btnLogin.Location = New Point(109, 545)
        btnLogin.Name = "btnLogin"
        btnLogin.Size = New Size(210, 65)
        btnLogin.TabIndex = 8
        btnLogin.Text = "Login"
        btnLogin.UseVisualStyleBackColor = True
        ' 
        ' btnKeluar
        ' 
        btnKeluar.Location = New Point(361, 545)
        btnKeluar.Name = "btnKeluar"
        btnKeluar.Size = New Size(210, 65)
        btnKeluar.TabIndex = 8
        btnKeluar.Text = "Keluar"
        btnKeluar.UseVisualStyleBackColor = True
        ' 
        ' formLogin
        ' 
        AutoScaleDimensions = New SizeF(15F, 37F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(30), CByte(30), CByte(47))
        ClientSize = New Size(680, 750)
        Controls.Add(btnKeluar)
        Controls.Add(btnLogin)
        Controls.Add(txtPassword)
        Controls.Add(txtUsername)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(linkRegister)
        Controls.Add(Label1)
        Font = New Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        FormBorderStyle = FormBorderStyle.None
        MaximizeBox = False
        Name = "formLogin"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Login"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents linkRegister As LinkLabel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents btnLogin As Button
    Friend WithEvents btnKeluar As Button
End Class
