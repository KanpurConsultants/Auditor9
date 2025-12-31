<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmImportDataFromWeb
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.Opn = New System.Windows.Forms.OpenFileDialog()
        Me.LblChildProgress = New System.Windows.Forms.Label()
        Me.PrgBarChild = New System.Windows.Forms.ProgressBar()
        Me.PrgBarParent = New System.Windows.Forms.ProgressBar()
        Me.LblParentProgress = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.PnlMain = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(0, 509)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(627, 4)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(0, 37)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(595, 466)
        Me.Pnl1.TabIndex = 10
        '
        'BtnOK
        '
        Me.BtnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOK.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOK.Location = New System.Drawing.Point(455, 623)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(64, 23)
        Me.BtnOK.TabIndex = 668
        Me.BtnOK.Text = "OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancel.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancel.Location = New System.Drawing.Point(525, 623)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(64, 23)
        Me.BtnCancel.TabIndex = 669
        Me.BtnCancel.Text = "Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'Opn
        '
        Me.Opn.FileName = "OpenFileDialog1"
        '
        'LblChildProgress
        '
        Me.LblChildProgress.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LblChildProgress.AutoSize = True
        Me.LblChildProgress.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblChildProgress.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.LblChildProgress.Location = New System.Drawing.Point(2, 522)
        Me.LblChildProgress.Name = "LblChildProgress"
        Me.LblChildProgress.Size = New System.Drawing.Size(19, 14)
        Me.LblChildProgress.TabIndex = 674
        Me.LblChildProgress.Text = "..."
        '
        'PrgBarChild
        '
        Me.PrgBarChild.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PrgBarChild.Location = New System.Drawing.Point(4, 541)
        Me.PrgBarChild.Maximum = 1960
        Me.PrgBarChild.Name = "PrgBarChild"
        Me.PrgBarChild.Size = New System.Drawing.Size(586, 22)
        Me.PrgBarChild.Step = 2
        Me.PrgBarChild.TabIndex = 675
        '
        'PrgBarParent
        '
        Me.PrgBarParent.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PrgBarParent.Location = New System.Drawing.Point(4, 582)
        Me.PrgBarParent.Maximum = 12
        Me.PrgBarParent.Name = "PrgBarParent"
        Me.PrgBarParent.Size = New System.Drawing.Size(586, 22)
        Me.PrgBarParent.TabIndex = 676
        '
        'LblParentProgress
        '
        Me.LblParentProgress.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LblParentProgress.AutoSize = True
        Me.LblParentProgress.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblParentProgress.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.LblParentProgress.Location = New System.Drawing.Point(2, 566)
        Me.LblParentProgress.Name = "LblParentProgress"
        Me.LblParentProgress.Size = New System.Drawing.Size(19, 14)
        Me.LblParentProgress.TabIndex = 677
        Me.LblParentProgress.Text = "..."
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox3.Location = New System.Drawing.Point(-15, 611)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(627, 4)
        Me.GroupBox3.TabIndex = 678
        Me.GroupBox3.TabStop = False
        '
        'PnlMain
        '
        Me.PnlMain.Location = New System.Drawing.Point(0, 2)
        Me.PnlMain.Name = "PnlMain"
        Me.PnlMain.Size = New System.Drawing.Size(595, 32)
        Me.PnlMain.TabIndex = 680
        '
        'FrmImportDataFromWeb
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(596, 646)
        Me.Controls.Add(Me.PnlMain)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.LblParentProgress)
        Me.Controls.Add(Me.LblChildProgress)
        Me.Controls.Add(Me.PrgBarParent)
        Me.Controls.Add(Me.PrgBarChild)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.Name = "FrmImportDataFromWeb"
        Me.Text = "Import From Excel"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents Pnl1 As System.Windows.Forms.Panel
    Public WithEvents BtnOK As System.Windows.Forms.Button
    Public WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents Opn As System.Windows.Forms.OpenFileDialog
    Friend WithEvents LblChildProgress As Label
    Friend WithEvents PrgBarChild As ProgressBar
    Friend WithEvents PrgBarParent As ProgressBar
    Friend WithEvents LblParentProgress As Label
    Public WithEvents GroupBox3 As GroupBox
    Public WithEvents PnlMain As Panel
End Class
