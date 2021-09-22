<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSplitLedgerOpening
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
        Me.BtnOk = New System.Windows.Forms.Button()
        Me.TxtEntryNo = New AgControls.AgTextBox()
        Me.SuspendLayout()
        '
        'BtnOk
        '
        Me.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOk.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOk.Location = New System.Drawing.Point(83, 226)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(75, 23)
        Me.BtnOk.TabIndex = 697
        Me.BtnOk.Text = "OK"
        Me.BtnOk.UseVisualStyleBackColor = True
        '
        'TxtEntryNo
        '
        Me.TxtEntryNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtEntryNo.AgLastValueTag = Nothing
        Me.TxtEntryNo.AgLastValueText = Nothing
        Me.TxtEntryNo.AgMandatory = False
        Me.TxtEntryNo.AgMasterHelp = False
        Me.TxtEntryNo.AgNumberLeftPlaces = 8
        Me.TxtEntryNo.AgNumberNegetiveAllow = False
        Me.TxtEntryNo.AgNumberRightPlaces = 2
        Me.TxtEntryNo.AgPickFromLastValue = False
        Me.TxtEntryNo.AgRowFilter = ""
        Me.TxtEntryNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtEntryNo.AgSelectedValue = Nothing
        Me.TxtEntryNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtEntryNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtEntryNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtEntryNo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEntryNo.Location = New System.Drawing.Point(9, 27)
        Me.TxtEntryNo.MaxLength = 0
        Me.TxtEntryNo.Name = "TxtEntryNo"
        Me.TxtEntryNo.Size = New System.Drawing.Size(271, 23)
        Me.TxtEntryNo.TabIndex = 698
        '
        'FrmSplitLedgerOpening
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.TxtEntryNo)
        Me.Controls.Add(Me.BtnOk)
        Me.Name = "FrmSplitLedgerOpening"
        Me.Text = "FrmSplitLedgerOpening"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnOk As Button
    Public WithEvents TxtEntryNo As AgControls.AgTextBox
End Class
