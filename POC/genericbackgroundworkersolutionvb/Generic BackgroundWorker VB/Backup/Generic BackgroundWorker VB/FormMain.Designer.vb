<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMain
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
        Me.listBox = New System.Windows.Forms.ListBox
        Me.labelProgress = New System.Windows.Forms.Label
        Me.progressBar = New System.Windows.Forms.ProgressBar
        Me.buttonStart = New System.Windows.Forms.Button
        Me.buttonCancel = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'listBox
        '
        Me.listBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.listBox.Enabled = False
        Me.listBox.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.listBox.FormattingEnabled = True
        Me.listBox.ItemHeight = 14
        Me.listBox.Location = New System.Drawing.Point(12, 12)
        Me.listBox.Name = "listBox"
        Me.listBox.Size = New System.Drawing.Size(321, 256)
        Me.listBox.TabIndex = 1
        '
        'labelProgress
        '
        Me.labelProgress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.labelProgress.Location = New System.Drawing.Point(12, 278)
        Me.labelProgress.Name = "labelProgress"
        Me.labelProgress.Size = New System.Drawing.Size(321, 13)
        Me.labelProgress.TabIndex = 2
        '
        'progressBar
        '
        Me.progressBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.progressBar.Enabled = False
        Me.progressBar.Location = New System.Drawing.Point(12, 294)
        Me.progressBar.Name = "progressBar"
        Me.progressBar.Size = New System.Drawing.Size(321, 23)
        Me.progressBar.TabIndex = 3
        '
        'buttonStart
        '
        Me.buttonStart.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.buttonStart.Location = New System.Drawing.Point(94, 323)
        Me.buttonStart.Name = "buttonStart"
        Me.buttonStart.Size = New System.Drawing.Size(75, 23)
        Me.buttonStart.TabIndex = 4
        Me.buttonStart.Text = "&Start"
        Me.buttonStart.UseVisualStyleBackColor = True
        '
        'buttonCancel
        '
        Me.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.buttonCancel.Enabled = False
        Me.buttonCancel.Location = New System.Drawing.Point(175, 323)
        Me.buttonCancel.Name = "buttonCancel"
        Me.buttonCancel.Size = New System.Drawing.Size(75, 23)
        Me.buttonCancel.TabIndex = 5
        Me.buttonCancel.Text = "&Cancel"
        Me.buttonCancel.UseVisualStyleBackColor = True
        '
        'FormMain
        '
        Me.AcceptButton = Me.buttonStart
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(345, 358)
        Me.Controls.Add(Me.buttonCancel)
        Me.Controls.Add(Me.buttonStart)
        Me.Controls.Add(Me.progressBar)
        Me.Controls.Add(Me.labelProgress)
        Me.Controls.Add(Me.listBox)
        Me.Name = "FormMain"
        Me.Text = "Generic BackgroundWorker"
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents listBox As System.Windows.Forms.ListBox
    Private WithEvents labelProgress As System.Windows.Forms.Label
    Private WithEvents progressBar As System.Windows.Forms.ProgressBar
    Private WithEvents buttonStart As System.Windows.Forms.Button
    Private WithEvents buttonCancel As System.Windows.Forms.Button

End Class
