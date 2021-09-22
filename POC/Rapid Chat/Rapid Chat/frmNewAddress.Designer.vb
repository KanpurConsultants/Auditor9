<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNewAddress
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtAddress = New System.Windows.Forms.TextBox
        Me.btnImBrowse = New Glass.GlassButton
        Me.GlassButton1 = New Glass.GlassButton
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GlassButton1)
        Me.GroupBox1.Controls.Add(Me.btnImBrowse)
        Me.GroupBox1.Controls.Add(Me.txtAddress)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(217, 73)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Enter Client IP"
        '
        'txtAddress
        '
        Me.txtAddress.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtAddress.Location = New System.Drawing.Point(3, 16)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(211, 20)
        Me.txtAddress.TabIndex = 0
        '
        'btnImBrowse
        '
        Me.btnImBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnImBrowse.BackColor = System.Drawing.Color.LightGray
        Me.btnImBrowse.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnImBrowse.ForeColor = System.Drawing.Color.Black
        Me.btnImBrowse.GlowColor = System.Drawing.Color.Gold
        Me.btnImBrowse.Location = New System.Drawing.Point(154, 42)
        Me.btnImBrowse.Name = "btnImBrowse"
        Me.btnImBrowse.Size = New System.Drawing.Size(60, 23)
        Me.btnImBrowse.TabIndex = 6
        Me.btnImBrowse.Text = "Add"
        '
        'GlassButton1
        '
        Me.GlassButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GlassButton1.BackColor = System.Drawing.Color.LightGray
        Me.GlassButton1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.GlassButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.GlassButton1.ForeColor = System.Drawing.Color.Black
        Me.GlassButton1.GlowColor = System.Drawing.Color.Red
        Me.GlassButton1.Location = New System.Drawing.Point(88, 42)
        Me.GlassButton1.Name = "GlassButton1"
        Me.GlassButton1.Size = New System.Drawing.Size(60, 23)
        Me.GlassButton1.TabIndex = 7
        Me.GlassButton1.Text = "Cancel"
        '
        'frmNewAddress
        '
        Me.AcceptButton = Me.btnImBrowse
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.GlassButton1
        Me.ClientSize = New System.Drawing.Size(217, 73)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmNewAddress"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "New Address"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents btnImBrowse As Glass.GlassButton
    Friend WithEvents GlassButton1 As Glass.GlassButton
End Class
