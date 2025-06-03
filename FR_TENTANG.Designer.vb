<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FR_TENTANG
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FR_TENTANG))
        StatusStrip1 = New StatusStrip()
        lbJalan = New ToolStripStatusLabel()
        Timer1 = New Timer(components)
        GroupBox1 = New GroupBox()
        PictureBox1 = New PictureBox()
        Label1 = New Label()
        StatusStrip1.SuspendLayout()
        GroupBox1.SuspendLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.ImageScalingSize = New Size(32, 32)
        StatusStrip1.Items.AddRange(New ToolStripItem() {lbJalan})
        StatusStrip1.Location = New Point(0, 882)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Size = New Size(1305, 42)
        StatusStrip1.TabIndex = 0
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' lbJalan
        ' 
        lbJalan.Name = "lbJalan"
        lbJalan.Size = New Size(318, 32)
        lbJalan.Text = "StockEase Management App"
        ' 
        ' Timer1
        ' 
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Controls.Add(PictureBox1)
        GroupBox1.Location = New Point(12, 12)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(1281, 867)
        GroupBox1.TabIndex = 1
        GroupBox1.TabStop = False
        GroupBox1.Text = "Biodata"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Dock = DockStyle.Top
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(3, 35)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(1275, 393)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 0
        PictureBox1.TabStop = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(6, 431)
        Label1.Name = "Label1"
        Label1.Size = New Size(403, 32)
        Label1.TabIndex = 1
        Label1.Text = "Created By : Nur Firmansyah Zamani"
        ' 
        ' FR_TENTANG
        ' 
        AutoScaleDimensions = New SizeF(13F, 32F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1305, 924)
        Controls.Add(GroupBox1)
        Controls.Add(StatusStrip1)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Name = "FR_TENTANG"
        StartPosition = FormStartPosition.CenterScreen
        Text = "TENTANG"
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents lbJalan As ToolStripStatusLabel
    Friend WithEvents Timer1 As Timer
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
End Class
