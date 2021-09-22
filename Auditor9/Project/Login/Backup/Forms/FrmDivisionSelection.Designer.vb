<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDivisionSelection
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDivisionSelection))
        Me.LblRight = New System.Windows.Forms.Label
        Me.LblLeft = New System.Windows.Forms.Label
        Me.BtnCancel = New System.Windows.Forms.Button
        Me.BtnOk = New System.Windows.Forms.Button
        Me.PnlMain = New System.Windows.Forms.Panel
        Me.PcbTop = New System.Windows.Forms.PictureBox
        Me.LblBottom = New System.Windows.Forms.Label
        CType(Me.PcbTop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblRight
        '
        Me.LblRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.LblRight.Location = New System.Drawing.Point(717, 70)
        Me.LblRight.Name = "LblRight"
        Me.LblRight.Size = New System.Drawing.Size(21, 307)
        Me.LblRight.TabIndex = 22
        '
        'LblLeft
        '
        Me.LblLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.LblLeft.Location = New System.Drawing.Point(0, 70)
        Me.LblLeft.Name = "LblLeft"
        Me.LblLeft.Size = New System.Drawing.Size(21, 307)
        Me.LblLeft.TabIndex = 21
        '
        'BtnCancel
        '
        Me.BtnCancel.AutoEllipsis = True
        Me.BtnCancel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtnCancel.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnCancel.Location = New System.Drawing.Point(648, 383)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(84, 24)
        Me.BtnCancel.TabIndex = 18
        Me.BtnCancel.Text = "&Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = False
        '
        'BtnOk
        '
        Me.BtnOk.AutoEllipsis = True
        Me.BtnOk.BackColor = System.Drawing.Color.WhiteSmoke
        Me.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtnOk.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnOk.Location = New System.Drawing.Point(559, 383)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(84, 24)
        Me.BtnOk.TabIndex = 17
        Me.BtnOk.Text = "&Ok"
        Me.BtnOk.UseVisualStyleBackColor = False
        '
        'PnlMain
        '
        Me.PnlMain.Location = New System.Drawing.Point(23, 70)
        Me.PnlMain.Name = "PnlMain"
        Me.PnlMain.Size = New System.Drawing.Size(692, 307)
        Me.PnlMain.TabIndex = 16
        '
        'PcbTop
        '
        Me.PcbTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PcbTop.Image = Global.Trade.My.Resources.Resources.My7
        Me.PcbTop.Location = New System.Drawing.Point(0, 0)
        Me.PcbTop.Name = "PcbTop"
        Me.PcbTop.Size = New System.Drawing.Size(738, 70)
        Me.PcbTop.TabIndex = 19
        Me.PcbTop.TabStop = False
        '
        'LblBottom
        '
        Me.LblBottom.BackColor = System.Drawing.Color.Transparent
        Me.LblBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.LblBottom.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBottom.ForeColor = System.Drawing.Color.White
        Me.LblBottom.Image = CType(resources.GetObject("LblBottom.Image"), System.Drawing.Image)
        Me.LblBottom.Location = New System.Drawing.Point(0, 377)
        Me.LblBottom.Name = "LblBottom"
        Me.LblBottom.Size = New System.Drawing.Size(738, 38)
        Me.LblBottom.TabIndex = 20
        Me.LblBottom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FrmDivisionSelection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(738, 415)
        Me.Controls.Add(Me.LblRight)
        Me.Controls.Add(Me.LblLeft)
        Me.Controls.Add(Me.PcbTop)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOk)
        Me.Controls.Add(Me.PnlMain)
        Me.Controls.Add(Me.LblBottom)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "FrmDivisionSelection"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Select Division"
        CType(Me.PcbTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LblRight As System.Windows.Forms.Label
    Friend WithEvents LblLeft As System.Windows.Forms.Label
    Friend WithEvents PcbTop As System.Windows.Forms.PictureBox
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents BtnOk As System.Windows.Forms.Button
    Friend WithEvents PnlMain As System.Windows.Forms.Panel
    Friend WithEvents LblBottom As System.Windows.Forms.Label
End Class
