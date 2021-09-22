<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAttachmentViewer
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
        Me.components = New System.ComponentModel.Container()
        Me.BtnNewAttachment = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LblDocNo = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.BtnPickFrom = New System.Windows.Forms.Button()
        Me.LblPath = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnNewAttachment
        '
        Me.BtnNewAttachment.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnNewAttachment.Cursor = System.Windows.Forms.Cursors.Default
        Me.BtnNewAttachment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnNewAttachment.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnNewAttachment.Location = New System.Drawing.Point(729, 432)
        Me.BtnNewAttachment.Name = "BtnNewAttachment"
        Me.BtnNewAttachment.Size = New System.Drawing.Size(124, 26)
        Me.BtnNewAttachment.TabIndex = 0
        Me.BtnNewAttachment.Text = "New Attachment"
        Me.BtnNewAttachment.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(4, 421)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(850, 4)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.LblDocNo)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(856, 36)
        Me.Panel1.TabIndex = 672
        '
        'LblDocNo
        '
        Me.LblDocNo.AutoSize = True
        Me.LblDocNo.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDocNo.Location = New System.Drawing.Point(16, 10)
        Me.LblDocNo.Name = "LblDocNo"
        Me.LblDocNo.Size = New System.Drawing.Size(57, 16)
        Me.LblDocNo.TabIndex = 11
        Me.LblDocNo.Text = "Caption"
        '
        'BtnPickFrom
        '
        Me.BtnPickFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnPickFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.BtnPickFrom.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPickFrom.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPickFrom.Location = New System.Drawing.Point(4, 432)
        Me.BtnPickFrom.Name = "BtnPickFrom"
        Me.BtnPickFrom.Size = New System.Drawing.Size(146, 26)
        Me.BtnPickFrom.TabIndex = 673
        Me.BtnPickFrom.Text = "Move All Files From"
        Me.BtnPickFrom.UseVisualStyleBackColor = True
        '
        'LblPath
        '
        Me.LblPath.AutoSize = True
        Me.LblPath.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPath.Location = New System.Drawing.Point(156, 437)
        Me.LblPath.Name = "LblPath"
        Me.LblPath.Size = New System.Drawing.Size(41, 16)
        Me.LblPath.TabIndex = 674
        Me.LblPath.Text = "Path"
        '
        'FrmAttachmentViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(856, 461)
        Me.Controls.Add(Me.LblPath)
        Me.Controls.Add(Me.BtnPickFrom)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.BtnNewAttachment)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmAttachmentViewer"
        Me.Text = "Attachment Viewer"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnNewAttachment As System.Windows.Forms.Button
    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents Panel1 As Panel
    Public WithEvents LblDocNo As Label
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents BtnPickFrom As Button
    Friend WithEvents LblPath As Label
End Class
