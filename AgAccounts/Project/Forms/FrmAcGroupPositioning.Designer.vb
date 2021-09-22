<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAcGroupPositioning
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAcGroupPositioning))
        Me.BtnClose = New System.Windows.Forms.Button
        Me.BtnSave = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.BtnDown = New System.Windows.Forms.Button
        Me.BtnUp = New System.Windows.Forms.Button
        Me.TrvMain = New System.Windows.Forms.TreeView
        Me.LblBG = New System.Windows.Forms.Label
        Me.BtnDefault = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnClose.Location = New System.Drawing.Point(604, 608)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(84, 26)
        Me.BtnClose.TabIndex = 4
        Me.BtnClose.Text = "Clos&e"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'BtnSave
        '
        Me.BtnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSave.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnSave.Location = New System.Drawing.Point(514, 608)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(84, 26)
        Me.BtnSave.TabIndex = 3
        Me.BtnSave.Text = "&Save"
        Me.BtnSave.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Location = New System.Drawing.Point(5, 594)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(689, 9)
        Me.GroupBox2.TabIndex = 32
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Tag = ""
        '
        'BtnDown
        '
        Me.BtnDown.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.BtnDown.BackColor = System.Drawing.Color.Transparent
        Me.BtnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnDown.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnDown.ForeColor = System.Drawing.Color.Black
        Me.BtnDown.Image = CType(resources.GetObject("BtnDown.Image"), System.Drawing.Image)
        Me.BtnDown.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnDown.Location = New System.Drawing.Point(232, 562)
        Me.BtnDown.Name = "BtnDown"
        Me.BtnDown.Size = New System.Drawing.Size(111, 26)
        Me.BtnDown.TabIndex = 1
        Me.BtnDown.Text = "Move Down"
        Me.BtnDown.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnDown.UseVisualStyleBackColor = False
        '
        'BtnUp
        '
        Me.BtnUp.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.BtnUp.BackColor = System.Drawing.Color.Transparent
        Me.BtnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnUp.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnUp.ForeColor = System.Drawing.Color.Black
        Me.BtnUp.Image = CType(resources.GetObject("BtnUp.Image"), System.Drawing.Image)
        Me.BtnUp.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnUp.Location = New System.Drawing.Point(349, 562)
        Me.BtnUp.Name = "BtnUp"
        Me.BtnUp.Size = New System.Drawing.Size(111, 26)
        Me.BtnUp.TabIndex = 2
        Me.BtnUp.Text = "Move Up"
        Me.BtnUp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnUp.UseVisualStyleBackColor = False
        '
        'TrvMain
        '
        Me.TrvMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TrvMain.BackColor = System.Drawing.Color.White
        Me.TrvMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TrvMain.CheckBoxes = True
        Me.TrvMain.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TrvMain.FullRowSelect = True
        Me.TrvMain.HideSelection = False
        Me.TrvMain.Indent = 30
        Me.TrvMain.Location = New System.Drawing.Point(12, 12)
        Me.TrvMain.Name = "TrvMain"
        Me.TrvMain.ShowLines = False
        Me.TrvMain.Size = New System.Drawing.Size(673, 547)
        Me.TrvMain.TabIndex = 0
        '
        'LblBG
        '
        Me.LblBG.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblBG.BackColor = System.Drawing.Color.LemonChiffon
        Me.LblBG.Location = New System.Drawing.Point(12, 561)
        Me.LblBG.Name = "LblBG"
        Me.LblBG.Size = New System.Drawing.Size(673, 28)
        Me.LblBG.TabIndex = 38
        '
        'BtnDefault
        '
        Me.BtnDefault.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnDefault.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnDefault.Location = New System.Drawing.Point(12, 608)
        Me.BtnDefault.Name = "BtnDefault"
        Me.BtnDefault.Size = New System.Drawing.Size(84, 26)
        Me.BtnDefault.TabIndex = 6
        Me.BtnDefault.Text = "&Default"
        Me.BtnDefault.UseVisualStyleBackColor = True
        '
        'FrmAcGroupPositioning
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(697, 638)
        Me.Controls.Add(Me.BtnDefault)
        Me.Controls.Add(Me.TrvMain)
        Me.Controls.Add(Me.BtnUp)
        Me.Controls.Add(Me.BtnDown)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.BtnSave)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.LblBG)
        Me.Name = "FrmAcGroupPositioning"
        Me.ShowIcon = False
        Me.Text = "Account Groups Positioning"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents BtnSave As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnDown As System.Windows.Forms.Button
    Friend WithEvents BtnUp As System.Windows.Forms.Button
    Friend WithEvents TrvMain As System.Windows.Forms.TreeView
    Friend WithEvents LblBG As System.Windows.Forms.Label
    Friend WithEvents BtnDefault As System.Windows.Forms.Button
End Class
