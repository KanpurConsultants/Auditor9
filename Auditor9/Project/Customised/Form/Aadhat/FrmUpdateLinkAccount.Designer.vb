<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUpdateLinkAccount
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
        Me.TxtParty = New AgControls.AgTextBox()
        Me.LblBuyer = New System.Windows.Forms.Label()
        Me.txtLinkedParty = New AgControls.AgTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TxtParty
        '
        Me.TxtParty.AgAllowUserToEnableMasterHelp = False
        Me.TxtParty.AgLastValueTag = Nothing
        Me.TxtParty.AgLastValueText = Nothing
        Me.TxtParty.AgMandatory = True
        Me.TxtParty.AgMasterHelp = False
        Me.TxtParty.AgNumberLeftPlaces = 8
        Me.TxtParty.AgNumberNegetiveAllow = False
        Me.TxtParty.AgNumberRightPlaces = 2
        Me.TxtParty.AgPickFromLastValue = False
        Me.TxtParty.AgRowFilter = ""
        Me.TxtParty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtParty.AgSelectedValue = Nothing
        Me.TxtParty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtParty.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtParty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtParty.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtParty.Location = New System.Drawing.Point(156, 58)
        Me.TxtParty.MaxLength = 0
        Me.TxtParty.Name = "TxtParty"
        Me.TxtParty.Size = New System.Drawing.Size(358, 17)
        Me.TxtParty.TabIndex = 694
        '
        'LblBuyer
        '
        Me.LblBuyer.AutoSize = True
        Me.LblBuyer.BackColor = System.Drawing.Color.Transparent
        Me.LblBuyer.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBuyer.Location = New System.Drawing.Point(103, 58)
        Me.LblBuyer.Name = "LblBuyer"
        Me.LblBuyer.Size = New System.Drawing.Size(47, 16)
        Me.LblBuyer.TabIndex = 695
        Me.LblBuyer.Text = "Party"
        '
        'txtLinkedParty
        '
        Me.txtLinkedParty.AgAllowUserToEnableMasterHelp = False
        Me.txtLinkedParty.AgLastValueTag = Nothing
        Me.txtLinkedParty.AgLastValueText = Nothing
        Me.txtLinkedParty.AgMandatory = True
        Me.txtLinkedParty.AgMasterHelp = False
        Me.txtLinkedParty.AgNumberLeftPlaces = 8
        Me.txtLinkedParty.AgNumberNegetiveAllow = False
        Me.txtLinkedParty.AgNumberRightPlaces = 2
        Me.txtLinkedParty.AgPickFromLastValue = False
        Me.txtLinkedParty.AgRowFilter = ""
        Me.txtLinkedParty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.txtLinkedParty.AgSelectedValue = Nothing
        Me.txtLinkedParty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.txtLinkedParty.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.txtLinkedParty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLinkedParty.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLinkedParty.Location = New System.Drawing.Point(156, 87)
        Me.txtLinkedParty.MaxLength = 0
        Me.txtLinkedParty.Name = "txtLinkedParty"
        Me.txtLinkedParty.Size = New System.Drawing.Size(358, 17)
        Me.txtLinkedParty.TabIndex = 696
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(34, 87)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(118, 16)
        Me.Label1.TabIndex = 697
        Me.Label1.Text = "Linked Account"
        '
        'btnUpdate
        '
        Me.btnUpdate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUpdate.Location = New System.Drawing.Point(231, 154)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(75, 23)
        Me.btnUpdate.TabIndex = 698
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'FrmUpdateLinkAccount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(550, 217)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.txtLinkedParty)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtParty)
        Me.Controls.Add(Me.LblBuyer)
        Me.Name = "FrmUpdateLinkAccount"
        Me.Text = "FrmUpdateLinkAccount"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents TxtParty As AgControls.AgTextBox
    Public WithEvents LblBuyer As Label
    Public WithEvents txtLinkedParty As AgControls.AgTextBox
    Public WithEvents Label1 As Label
    Friend WithEvents btnUpdate As Button
End Class
