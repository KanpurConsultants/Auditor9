<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FRenewSoftware
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
        Me.TxtKeyNo = New AgControls.AgTextBox()
        Me.LblKeyNo = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'TxtKeyNo
        '
        Me.TxtKeyNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtKeyNo.AgLastValueTag = Nothing
        Me.TxtKeyNo.AgLastValueText = Nothing
        Me.TxtKeyNo.AgMandatory = True
        Me.TxtKeyNo.AgMasterHelp = False
        Me.TxtKeyNo.AgNumberLeftPlaces = 8
        Me.TxtKeyNo.AgNumberNegetiveAllow = False
        Me.TxtKeyNo.AgNumberRightPlaces = 2
        Me.TxtKeyNo.AgPickFromLastValue = False
        Me.TxtKeyNo.AgRowFilter = ""
        Me.TxtKeyNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtKeyNo.AgSelectedValue = Nothing
        Me.TxtKeyNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtKeyNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtKeyNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtKeyNo.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtKeyNo.Location = New System.Drawing.Point(163, 23)
        Me.TxtKeyNo.MaxLength = 0
        Me.TxtKeyNo.Name = "TxtKeyNo"
        Me.TxtKeyNo.Size = New System.Drawing.Size(268, 17)
        Me.TxtKeyNo.TabIndex = 3003
        '
        'LblKeyNo
        '
        Me.LblKeyNo.AutoSize = True
        Me.LblKeyNo.BackColor = System.Drawing.Color.Transparent
        Me.LblKeyNo.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblKeyNo.Location = New System.Drawing.Point(12, 23)
        Me.LblKeyNo.Name = "LblKeyNo"
        Me.LblKeyNo.Size = New System.Drawing.Size(144, 16)
        Me.LblKeyNo.TabIndex = 3004
        Me.LblKeyNo.Text = "Enter Your Key No."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 71)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 16)
        Me.Label1.TabIndex = 3005
        Me.Label1.Text = "Message"
        '
        'FRenewSoftware
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(443, 261)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtKeyNo)
        Me.Controls.Add(Me.LblKeyNo)
        Me.Name = "FRenewSoftware"
        Me.Text = "Re-New Software"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents TxtKeyNo As AgControls.AgTextBox
    Public WithEvents LblKeyNo As Label
    Public WithEvents Label1 As Label
End Class
