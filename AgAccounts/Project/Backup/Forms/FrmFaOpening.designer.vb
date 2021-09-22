<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFaOpening
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
        Me.Topctrl1 = New Topctrl.Topctrl
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.TxtV_Date = New AgControls.AgTextBox
        Me.TxtDocId = New AgControls.AgTextBox
        Me.LblDocId = New System.Windows.Forms.Label
        Me.TxtSite_Code = New AgControls.AgTextBox
        Me.LblSite_Code = New System.Windows.Forms.Label
        Me.LblSite_CodeReq = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.LblV_Date = New System.Windows.Forms.Label
        Me.TxtV_No = New AgControls.AgTextBox
        Me.LblV_No = New System.Windows.Forms.Label
        Me.LblPrefix = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtV_Type = New AgControls.AgTextBox
        Me.LblV_Type = New System.Windows.Forms.Label
        Me.TxtNarration = New AgControls.AgTextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Topctrl1
        '
        Me.Topctrl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Me.Topctrl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Topctrl1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Topctrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Topctrl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Topctrl1.Location = New System.Drawing.Point(0, 0)
        Me.Topctrl1.Mode = "Browse"
        Me.Topctrl1.Name = "Topctrl1"
        Me.Topctrl1.Size = New System.Drawing.Size(872, 41)
        Me.Topctrl1.TabIndex = 8
        Me.Topctrl1.tAdd = True
        Me.Topctrl1.tCancel = True
        Me.Topctrl1.tDel = True
        Me.Topctrl1.tDiscard = False
        Me.Topctrl1.tEdit = True
        Me.Topctrl1.tExit = True
        Me.Topctrl1.tFind = True
        Me.Topctrl1.tFirst = True
        Me.Topctrl1.tLast = True
        Me.Topctrl1.tNext = True
        Me.Topctrl1.tPrev = True
        Me.Topctrl1.tPrn = True
        Me.Topctrl1.tRef = True
        Me.Topctrl1.tSave = False
        Me.Topctrl1.tSite = True
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(84, 171)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(705, 386)
        Me.Pnl1.TabIndex = 5
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgMandatory = False
        Me.TxtV_Date.AgMasterHelp = False
        Me.TxtV_Date.AgNumberLeftPlaces = 0
        Me.TxtV_Date.AgNumberNegetiveAllow = False
        Me.TxtV_Date.AgNumberRightPlaces = 0
        Me.TxtV_Date.AgPickFromLastValue = False
        Me.TxtV_Date.AgRowFilter = ""
        Me.TxtV_Date.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtV_Date.AgSelectedValue = Nothing
        Me.TxtV_Date.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtV_Date.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtV_Date.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtV_Date.Location = New System.Drawing.Point(344, 129)
        Me.TxtV_Date.Name = "TxtV_Date"
        Me.TxtV_Date.Size = New System.Drawing.Size(100, 21)
        Me.TxtV_Date.TabIndex = 3
        Me.TxtV_Date.Text = "TxtV_Date"
        '
        'TxtDocId
        '
        Me.TxtDocId.AgMandatory = False
        Me.TxtDocId.AgMasterHelp = False
        Me.TxtDocId.AgNumberLeftPlaces = 0
        Me.TxtDocId.AgNumberNegetiveAllow = False
        Me.TxtDocId.AgNumberRightPlaces = 0
        Me.TxtDocId.AgPickFromLastValue = False
        Me.TxtDocId.AgRowFilter = ""
        Me.TxtDocId.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDocId.AgSelectedValue = Nothing
        Me.TxtDocId.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDocId.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDocId.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDocId.ForeColor = System.Drawing.Color.Blue
        Me.TxtDocId.Location = New System.Drawing.Point(344, 63)
        Me.TxtDocId.MaxLength = 21
        Me.TxtDocId.Name = "TxtDocId"
        Me.TxtDocId.ReadOnly = True
        Me.TxtDocId.Size = New System.Drawing.Size(325, 21)
        Me.TxtDocId.TabIndex = 0
        Me.TxtDocId.Text = "TxtDocId"
        '
        'LblDocId
        '
        Me.LblDocId.AutoSize = True
        Me.LblDocId.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDocId.ForeColor = System.Drawing.Color.Blue
        Me.LblDocId.Location = New System.Drawing.Point(203, 67)
        Me.LblDocId.Name = "LblDocId"
        Me.LblDocId.Size = New System.Drawing.Size(76, 13)
        Me.LblDocId.TabIndex = 266
        Me.LblDocId.Text = "Documet ID"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgMandatory = False
        Me.TxtSite_Code.AgMasterHelp = False
        Me.TxtSite_Code.AgNumberLeftPlaces = 0
        Me.TxtSite_Code.AgNumberNegetiveAllow = False
        Me.TxtSite_Code.AgNumberRightPlaces = 0
        Me.TxtSite_Code.AgPickFromLastValue = False
        Me.TxtSite_Code.AgRowFilter = ""
        Me.TxtSite_Code.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSite_Code.AgSelectedValue = Nothing
        Me.TxtSite_Code.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSite_Code.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSite_Code.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSite_Code.Location = New System.Drawing.Point(344, 85)
        Me.TxtSite_Code.MaxLength = 2
        Me.TxtSite_Code.Name = "TxtSite_Code"
        Me.TxtSite_Code.Size = New System.Drawing.Size(325, 21)
        Me.TxtSite_Code.TabIndex = 1
        Me.TxtSite_Code.Text = "TxtSite_Code"
        '
        'LblSite_Code
        '
        Me.LblSite_Code.AutoSize = True
        Me.LblSite_Code.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSite_Code.Location = New System.Drawing.Point(203, 89)
        Me.LblSite_Code.Name = "LblSite_Code"
        Me.LblSite_Code.Size = New System.Drawing.Size(111, 13)
        Me.LblSite_Code.TabIndex = 265
        Me.LblSite_Code.Text = "Site/Branch Name"
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.AutoSize = True
        Me.LblSite_CodeReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblSite_CodeReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(331, 92)
        Me.LblSite_CodeReq.Name = "LblSite_CodeReq"
        Me.LblSite_CodeReq.Size = New System.Drawing.Size(10, 7)
        Me.LblSite_CodeReq.TabIndex = 264
        Me.LblSite_CodeReq.Text = "Ä"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(331, 136)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 7)
        Me.Label5.TabIndex = 271
        Me.Label5.Text = "Ä"
        '
        'LblV_Date
        '
        Me.LblV_Date.AutoSize = True
        Me.LblV_Date.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblV_Date.Location = New System.Drawing.Point(203, 133)
        Me.LblV_Date.Name = "LblV_Date"
        Me.LblV_Date.Size = New System.Drawing.Size(85, 13)
        Me.LblV_Date.TabIndex = 268
        Me.LblV_Date.Text = "Voucher Date"
        '
        'TxtV_No
        '
        Me.TxtV_No.AgMandatory = False
        Me.TxtV_No.AgMasterHelp = False
        Me.TxtV_No.AgNumberLeftPlaces = 8
        Me.TxtV_No.AgNumberNegetiveAllow = False
        Me.TxtV_No.AgNumberRightPlaces = 0
        Me.TxtV_No.AgPickFromLastValue = False
        Me.TxtV_No.AgRowFilter = ""
        Me.TxtV_No.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtV_No.AgSelectedValue = Nothing
        Me.TxtV_No.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtV_No.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtV_No.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtV_No.Location = New System.Drawing.Point(569, 129)
        Me.TxtV_No.Name = "TxtV_No"
        Me.TxtV_No.Size = New System.Drawing.Size(100, 21)
        Me.TxtV_No.TabIndex = 4
        Me.TxtV_No.Text = "TxtV_No"
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblV_No
        '
        Me.LblV_No.AutoSize = True
        Me.LblV_No.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblV_No.Location = New System.Drawing.Point(458, 133)
        Me.LblV_No.Name = "LblV_No"
        Me.LblV_No.Size = New System.Drawing.Size(77, 13)
        Me.LblV_No.TabIndex = 269
        Me.LblV_No.Text = "Voucher No."
        '
        'LblPrefix
        '
        Me.LblPrefix.AutoSize = True
        Me.LblPrefix.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPrefix.ForeColor = System.Drawing.Color.Blue
        Me.LblPrefix.Location = New System.Drawing.Point(531, 133)
        Me.LblPrefix.Name = "LblPrefix"
        Me.LblPrefix.Size = New System.Drawing.Size(56, 13)
        Me.LblPrefix.TabIndex = 270
        Me.LblPrefix.Text = "LblPrefix"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(331, 114)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 7)
        Me.Label4.TabIndex = 274
        Me.Label4.Text = "Ä"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgMandatory = False
        Me.TxtV_Type.AgMasterHelp = False
        Me.TxtV_Type.AgNumberLeftPlaces = 8
        Me.TxtV_Type.AgNumberNegetiveAllow = False
        Me.TxtV_Type.AgNumberRightPlaces = 0
        Me.TxtV_Type.AgPickFromLastValue = False
        Me.TxtV_Type.AgRowFilter = ""
        Me.TxtV_Type.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtV_Type.AgSelectedValue = Nothing
        Me.TxtV_Type.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtV_Type.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtV_Type.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtV_Type.Location = New System.Drawing.Point(344, 107)
        Me.TxtV_Type.Name = "TxtV_Type"
        Me.TxtV_Type.Size = New System.Drawing.Size(325, 21)
        Me.TxtV_Type.TabIndex = 2
        Me.TxtV_Type.Text = "TxtVtype"
        '
        'LblV_Type
        '
        Me.LblV_Type.AutoSize = True
        Me.LblV_Type.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblV_Type.Location = New System.Drawing.Point(203, 111)
        Me.LblV_Type.Name = "LblV_Type"
        Me.LblV_Type.Size = New System.Drawing.Size(86, 13)
        Me.LblV_Type.TabIndex = 273
        Me.LblV_Type.Text = "Voucher Type"
        '
        'TxtNarration
        '
        Me.TxtNarration.AgMandatory = False
        Me.TxtNarration.AgMasterHelp = False
        Me.TxtNarration.AgNumberLeftPlaces = 0
        Me.TxtNarration.AgNumberNegetiveAllow = False
        Me.TxtNarration.AgNumberRightPlaces = 0
        Me.TxtNarration.AgPickFromLastValue = False
        Me.TxtNarration.AgRowFilter = ""
        Me.TxtNarration.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtNarration.AgSelectedValue = Nothing
        Me.TxtNarration.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtNarration.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtNarration.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNarration.Location = New System.Drawing.Point(84, 572)
        Me.TxtNarration.MaxLength = 255
        Me.TxtNarration.Name = "TxtNarration"
        Me.TxtNarration.Size = New System.Drawing.Size(489, 21)
        Me.TxtNarration.TabIndex = 7
        Me.TxtNarration.Text = "AgTextBox4"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(12, 576)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(60, 13)
        Me.Label12.TabIndex = 6
        Me.Label12.Text = "&Narration"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(81, 155)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(131, 13)
        Me.Label1.TabIndex = 277
        Me.Label1.Text = "A/c Opening Balance:"
        '
        'FrmFaOpening
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(872, 616)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtNarration)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtV_Type)
        Me.Controls.Add(Me.LblV_Type)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.LblV_Date)
        Me.Controls.Add(Me.TxtV_No)
        Me.Controls.Add(Me.LblV_No)
        Me.Controls.Add(Me.LblPrefix)
        Me.Controls.Add(Me.TxtDocId)
        Me.Controls.Add(Me.LblDocId)
        Me.Controls.Add(Me.TxtSite_Code)
        Me.Controls.Add(Me.LblSite_Code)
        Me.Controls.Add(Me.LblSite_CodeReq)
        Me.Controls.Add(Me.TxtV_Date)
        Me.Controls.Add(Me.Topctrl1)
        Me.Controls.Add(Me.Pnl1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmFaOpening"
        Me.Text = "Financial Account Opening"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Topctrl1 As Topctrl.Topctrl
    Friend WithEvents Pnl1 As System.Windows.Forms.Panel
    Friend WithEvents TxtV_Date As AgControls.AgTextBox
    Friend WithEvents TxtDocId As AgControls.AgTextBox
    Friend WithEvents LblDocId As System.Windows.Forms.Label
    Friend WithEvents TxtSite_Code As AgControls.AgTextBox
    Friend WithEvents LblSite_Code As System.Windows.Forms.Label
    Friend WithEvents LblSite_CodeReq As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents LblV_Date As System.Windows.Forms.Label
    Friend WithEvents TxtV_No As AgControls.AgTextBox
    Friend WithEvents LblV_No As System.Windows.Forms.Label
    Friend WithEvents LblPrefix As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtV_Type As AgControls.AgTextBox
    Friend WithEvents LblV_Type As System.Windows.Forms.Label
    Friend WithEvents TxtNarration As AgControls.AgTextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
