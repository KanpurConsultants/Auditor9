<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSaleInvoiceOrderSummary
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
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.LblSaleToParty = New System.Windows.Forms.Label()
        Me.LblOrderNo = New System.Windows.Forms.Label()
        Me.TxtOrderNo = New AgControls.AgTextBox()
        Me.TxtPartyName = New AgControls.AgTextBox()
        Me.BtnOk = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl1.Location = New System.Drawing.Point(10, 42)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(758, 303)
        Me.Pnl1.TabIndex = 2
        '
        'LblSaleToParty
        '
        Me.LblSaleToParty.AutoSize = True
        Me.LblSaleToParty.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSaleToParty.Location = New System.Drawing.Point(256, 12)
        Me.LblSaleToParty.Name = "LblSaleToParty"
        Me.LblSaleToParty.Size = New System.Drawing.Size(83, 16)
        Me.LblSaleToParty.TabIndex = 13
        Me.LblSaleToParty.Text = "Party Name"
        '
        'LblOrderNo
        '
        Me.LblOrderNo.AutoSize = True
        Me.LblOrderNo.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblOrderNo.Location = New System.Drawing.Point(4, 12)
        Me.LblOrderNo.Name = "LblOrderNo"
        Me.LblOrderNo.Size = New System.Drawing.Size(65, 16)
        Me.LblOrderNo.TabIndex = 11
        Me.LblOrderNo.Text = "Order No"
        '
        'TxtOrderNo
        '
        Me.TxtOrderNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtOrderNo.AgLastValueTag = Nothing
        Me.TxtOrderNo.AgLastValueText = Nothing
        Me.TxtOrderNo.AgMandatory = False
        Me.TxtOrderNo.AgMasterHelp = False
        Me.TxtOrderNo.AgNumberLeftPlaces = 8
        Me.TxtOrderNo.AgNumberNegetiveAllow = False
        Me.TxtOrderNo.AgNumberRightPlaces = 2
        Me.TxtOrderNo.AgPickFromLastValue = False
        Me.TxtOrderNo.AgRowFilter = ""
        Me.TxtOrderNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtOrderNo.AgSelectedValue = Nothing
        Me.TxtOrderNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtOrderNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtOrderNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtOrderNo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtOrderNo.Location = New System.Drawing.Point(75, 12)
        Me.TxtOrderNo.MaxLength = 20
        Me.TxtOrderNo.Name = "TxtOrderNo"
        Me.TxtOrderNo.Size = New System.Drawing.Size(141, 16)
        Me.TxtOrderNo.TabIndex = 0
        '
        'TxtPartyName
        '
        Me.TxtPartyName.AgAllowUserToEnableMasterHelp = False
        Me.TxtPartyName.AgLastValueTag = Nothing
        Me.TxtPartyName.AgLastValueText = Nothing
        Me.TxtPartyName.AgMandatory = False
        Me.TxtPartyName.AgMasterHelp = False
        Me.TxtPartyName.AgNumberLeftPlaces = 8
        Me.TxtPartyName.AgNumberNegetiveAllow = False
        Me.TxtPartyName.AgNumberRightPlaces = 2
        Me.TxtPartyName.AgPickFromLastValue = False
        Me.TxtPartyName.AgRowFilter = ""
        Me.TxtPartyName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPartyName.AgSelectedValue = Nothing
        Me.TxtPartyName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPartyName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPartyName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPartyName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPartyName.Location = New System.Drawing.Point(373, 12)
        Me.TxtPartyName.MaxLength = 20
        Me.TxtPartyName.Name = "TxtPartyName"
        Me.TxtPartyName.Size = New System.Drawing.Size(400, 16)
        Me.TxtPartyName.TabIndex = 1
        '
        'BtnOk
        '
        Me.BtnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOk.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOk.Location = New System.Drawing.Point(693, 350)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(75, 23)
        Me.BtnOk.TabIndex = 14
        Me.BtnOk.Text = "OK"
        Me.BtnOk.UseVisualStyleBackColor = True
        '
        'FrmSaleInvoiceOrderSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(780, 380)
        Me.Controls.Add(Me.BtnOk)
        Me.Controls.Add(Me.TxtPartyName)
        Me.Controls.Add(Me.LblSaleToParty)
        Me.Controls.Add(Me.TxtOrderNo)
        Me.Controls.Add(Me.LblOrderNo)
        Me.Controls.Add(Me.Pnl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmSaleInvoiceOrderSummary"
        Me.Text = "Sale Invoice Order Summary"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents Pnl1 As System.Windows.Forms.Panel
    Public WithEvents LblOrderNo As Label
    Public WithEvents LblSaleToParty As Label
    Public WithEvents TxtOrderNo As AgControls.AgTextBox
    Public WithEvents TxtPartyName As AgControls.AgTextBox
    Friend WithEvents BtnOk As Button
End Class
