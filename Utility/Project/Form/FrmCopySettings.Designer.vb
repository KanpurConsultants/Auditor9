<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCopySettings
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
        Me.TxtSiteName = New AgControls.AgTextBox
        Me.BtnOk = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.BtnCancel = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'TxtSiteName
        '
        Me.TxtSiteName.AgAllowUserToEnableMasterHelp = False
        Me.TxtSiteName.AgLastValueTag = Nothing
        Me.TxtSiteName.AgLastValueText = Nothing
        Me.TxtSiteName.AgMandatory = False
        Me.TxtSiteName.AgMasterHelp = False
        Me.TxtSiteName.AgNumberLeftPlaces = 0
        Me.TxtSiteName.AgNumberNegetiveAllow = False
        Me.TxtSiteName.AgNumberRightPlaces = 0
        Me.TxtSiteName.AgPickFromLastValue = False
        Me.TxtSiteName.AgRowFilter = ""
        Me.TxtSiteName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSiteName.AgSelectedValue = Nothing
        Me.TxtSiteName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSiteName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSiteName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSiteName.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TxtSiteName.Location = New System.Drawing.Point(87, 23)
        Me.TxtSiteName.Name = "TxtSiteName"
        Me.TxtSiteName.Size = New System.Drawing.Size(242, 18)
        Me.TxtSiteName.TabIndex = 0
        '
        'BtnOk
        '
        Me.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOk.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOk.Location = New System.Drawing.Point(106, 80)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(52, 24)
        Me.BtnOk.TabIndex = 1
        Me.BtnOk.Text = "Ok"
        Me.BtnOk.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label1.Location = New System.Drawing.Point(12, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 16)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Site Name"
        '
        'BtnCancel
        '
        Me.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancel.Location = New System.Drawing.Point(168, 80)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(66, 24)
        Me.BtnCancel.TabIndex = 2
        Me.BtnCancel.Text = "Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'FrmCopySettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(341, 122)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOk)
        Me.Controls.Add(Me.TxtSiteName)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "FrmCopySettings"
        Me.Text = "Copy Settings"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnOk As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Public WithEvents TxtSiteName As AgControls.AgTextBox
End Class
