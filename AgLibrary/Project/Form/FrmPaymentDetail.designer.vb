<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPaymentDetail
    Inherits System.Windows.Forms.Form
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If Disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.          [Ag]
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TxtCashAmount = New AgControls.AgTextBox
        Me.LblCashAmount = New System.Windows.Forms.Label
        Me.LblCashAc = New System.Windows.Forms.Label
        Me.TxtCashAc = New AgControls.AgTextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.TxtChq_No = New AgControls.AgTextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.TxtChq_Date = New AgControls.AgTextBox
        Me.LblBank_Code = New System.Windows.Forms.Label
        Me.TxtBank_Code = New AgControls.AgTextBox
        Me.TxtDocId = New AgControls.AgTextBox
        Me.LblDocId = New System.Windows.Forms.Label
        Me.TxtPartyAc = New AgControls.AgTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtBankAc = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtBankAmount = New AgControls.AgTextBox
        Me.TxtCardAc = New AgControls.AgTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.TxtCardAmount = New AgControls.AgTextBox
        Me.TxtCardBank_Code = New AgControls.AgTextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.TxtCard_No = New AgControls.AgTextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.TxtClg_Date = New AgControls.AgTextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.TxtAcTransferBankAc = New AgControls.AgTextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.TxtAcTransferAmount = New AgControls.AgTextBox
        Me.TxtAcTransferBank_Code = New AgControls.AgTextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.TxtAcTransferAcNo = New AgControls.AgTextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.TxtTotalPayment = New AgControls.AgTextBox
        Me.BtnExit = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label27 = New System.Windows.Forms.Label
        Me.TxtClg_Date3 = New AgControls.AgTextBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.TxtBankAc3 = New AgControls.AgTextBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtBankAmount3 = New AgControls.AgTextBox
        Me.TxtBank_Code3 = New AgControls.AgTextBox
        Me.Label31 = New System.Windows.Forms.Label
        Me.TxtChq_Date3 = New AgControls.AgTextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.TxtChq_No3 = New AgControls.AgTextBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.TxtClg_Date2 = New AgControls.AgTextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.TxtBankAc2 = New AgControls.AgTextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.TxtBankAmount2 = New AgControls.AgTextBox
        Me.TxtBank_Code2 = New AgControls.AgTextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.TxtChq_Date2 = New AgControls.AgTextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.TxtChq_No2 = New AgControls.AgTextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.TxtAdjustmentAc = New AgControls.AgTextBox
        Me.Label35 = New System.Windows.Forms.Label
        Me.Label36 = New System.Windows.Forms.Label
        Me.TxtAdjustmentAmount = New AgControls.AgTextBox
        Me.TxtAdjustmentRemark = New AgControls.AgTextBox
        Me.Label37 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'TxtCashAmount
        '
        Me.TxtCashAmount.AgMandatory = False
        Me.TxtCashAmount.AgMasterHelp = False
        Me.TxtCashAmount.AgNumberLeftPlaces = 8
        Me.TxtCashAmount.AgNumberNegetiveAllow = False
        Me.TxtCashAmount.AgNumberRightPlaces = 2
        Me.TxtCashAmount.AgPickFromLastValue = False
        Me.TxtCashAmount.AgRowFilter = ""
        Me.TxtCashAmount.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCashAmount.AgSelectedValue = Nothing
        Me.TxtCashAmount.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCashAmount.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtCashAmount.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCashAmount.Location = New System.Drawing.Point(136, 100)
        Me.TxtCashAmount.Name = "TxtCashAmount"
        Me.TxtCashAmount.Size = New System.Drawing.Size(100, 21)
        Me.TxtCashAmount.TabIndex = 3
        Me.TxtCashAmount.Text = "0.00"
        Me.TxtCashAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblCashAmount
        '
        Me.LblCashAmount.AutoSize = True
        Me.LblCashAmount.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCashAmount.Location = New System.Drawing.Point(11, 104)
        Me.LblCashAmount.Name = "LblCashAmount"
        Me.LblCashAmount.Size = New System.Drawing.Size(84, 13)
        Me.LblCashAmount.TabIndex = 0
        Me.LblCashAmount.Text = "Cash Amount"
        '
        'LblCashAc
        '
        Me.LblCashAc.AutoSize = True
        Me.LblCashAc.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCashAc.Location = New System.Drawing.Point(11, 82)
        Me.LblCashAc.Name = "LblCashAc"
        Me.LblCashAc.Size = New System.Drawing.Size(59, 13)
        Me.LblCashAc.TabIndex = 137
        Me.LblCashAc.Text = "Cash A/c"
        '
        'TxtCashAc
        '
        Me.TxtCashAc.AgMandatory = False
        Me.TxtCashAc.AgMasterHelp = False
        Me.TxtCashAc.AgNumberLeftPlaces = 8
        Me.TxtCashAc.AgNumberNegetiveAllow = False
        Me.TxtCashAc.AgNumberRightPlaces = 0
        Me.TxtCashAc.AgPickFromLastValue = False
        Me.TxtCashAc.AgRowFilter = ""
        Me.TxtCashAc.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Me.TxtCashAc.AgSelectedValue = Nothing
        Me.TxtCashAc.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCashAc.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCashAc.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCashAc.Location = New System.Drawing.Point(136, 78)
        Me.TxtCashAc.Name = "TxtCashAc"
        Me.TxtCashAc.Size = New System.Drawing.Size(300, 21)
        Me.TxtCashAc.TabIndex = 2
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(10, 200)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(74, 13)
        Me.Label24.TabIndex = 170
        Me.Label24.Text = "Cheque No."
        '
        'TxtChq_No
        '
        Me.TxtChq_No.AgMandatory = False
        Me.TxtChq_No.AgMasterHelp = False
        Me.TxtChq_No.AgNumberLeftPlaces = 6
        Me.TxtChq_No.AgNumberNegetiveAllow = False
        Me.TxtChq_No.AgNumberRightPlaces = 0
        Me.TxtChq_No.AgPickFromLastValue = False
        Me.TxtChq_No.AgRowFilter = ""
        Me.TxtChq_No.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtChq_No.AgSelectedValue = Nothing
        Me.TxtChq_No.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtChq_No.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtChq_No.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtChq_No.Location = New System.Drawing.Point(135, 196)
        Me.TxtChq_No.MaxLength = 10
        Me.TxtChq_No.Name = "TxtChq_No"
        Me.TxtChq_No.Size = New System.Drawing.Size(100, 21)
        Me.TxtChq_No.TabIndex = 6
        Me.TxtChq_No.Text = "Cheque No."
        Me.TxtChq_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(244, 200)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(72, 13)
        Me.Label23.TabIndex = 169
        Me.Label23.Text = "Cheque Dt."
        '
        'TxtChq_Date
        '
        Me.TxtChq_Date.AgMandatory = False
        Me.TxtChq_Date.AgMasterHelp = False
        Me.TxtChq_Date.AgNumberLeftPlaces = 0
        Me.TxtChq_Date.AgNumberNegetiveAllow = False
        Me.TxtChq_Date.AgNumberRightPlaces = 0
        Me.TxtChq_Date.AgPickFromLastValue = False
        Me.TxtChq_Date.AgRowFilter = ""
        Me.TxtChq_Date.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtChq_Date.AgSelectedValue = Nothing
        Me.TxtChq_Date.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtChq_Date.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtChq_Date.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtChq_Date.Location = New System.Drawing.Point(335, 196)
        Me.TxtChq_Date.MaxLength = 10
        Me.TxtChq_Date.Name = "TxtChq_Date"
        Me.TxtChq_Date.Size = New System.Drawing.Size(100, 21)
        Me.TxtChq_Date.TabIndex = 7
        Me.TxtChq_Date.Text = "Cheque Dt"
        '
        'LblBank_Code
        '
        Me.LblBank_Code.AutoSize = True
        Me.LblBank_Code.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBank_Code.Location = New System.Drawing.Point(10, 178)
        Me.LblBank_Code.Name = "LblBank_Code"
        Me.LblBank_Code.Size = New System.Drawing.Size(73, 13)
        Me.LblBank_Code.TabIndex = 172
        Me.LblBank_Code.Text = "Bank Name"
        '
        'TxtBank_Code
        '
        Me.TxtBank_Code.AgMandatory = False
        Me.TxtBank_Code.AgMasterHelp = True
        Me.TxtBank_Code.AgNumberLeftPlaces = 8
        Me.TxtBank_Code.AgNumberNegetiveAllow = False
        Me.TxtBank_Code.AgNumberRightPlaces = 0
        Me.TxtBank_Code.AgPickFromLastValue = False
        Me.TxtBank_Code.AgRowFilter = ""
        Me.TxtBank_Code.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Me.TxtBank_Code.AgSelectedValue = Nothing
        Me.TxtBank_Code.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBank_Code.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBank_Code.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBank_Code.Location = New System.Drawing.Point(135, 174)
        Me.TxtBank_Code.MaxLength = 100
        Me.TxtBank_Code.Name = "TxtBank_Code"
        Me.TxtBank_Code.Size = New System.Drawing.Size(300, 21)
        Me.TxtBank_Code.TabIndex = 5
        Me.TxtBank_Code.Text = "Bank Name"
        '
        'TxtDocId
        '
        Me.TxtDocId.AgMandatory = True
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
        Me.TxtDocId.BackColor = System.Drawing.Color.White
        Me.TxtDocId.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDocId.Location = New System.Drawing.Point(136, 12)
        Me.TxtDocId.MaxLength = 21
        Me.TxtDocId.Name = "TxtDocId"
        Me.TxtDocId.ReadOnly = True
        Me.TxtDocId.Size = New System.Drawing.Size(300, 21)
        Me.TxtDocId.TabIndex = 0
        Me.TxtDocId.TabStop = False
        Me.TxtDocId.Text = "AgTextBox1"
        '
        'LblDocId
        '
        Me.LblDocId.AutoSize = True
        Me.LblDocId.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDocId.Location = New System.Drawing.Point(11, 16)
        Me.LblDocId.Name = "LblDocId"
        Me.LblDocId.Size = New System.Drawing.Size(83, 13)
        Me.LblDocId.TabIndex = 240
        Me.LblDocId.Text = "Document ID"
        '
        'TxtPartyAc
        '
        Me.TxtPartyAc.AgMandatory = False
        Me.TxtPartyAc.AgMasterHelp = False
        Me.TxtPartyAc.AgNumberLeftPlaces = 8
        Me.TxtPartyAc.AgNumberNegetiveAllow = False
        Me.TxtPartyAc.AgNumberRightPlaces = 0
        Me.TxtPartyAc.AgPickFromLastValue = False
        Me.TxtPartyAc.AgRowFilter = ""
        Me.TxtPartyAc.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Me.TxtPartyAc.AgSelectedValue = Nothing
        Me.TxtPartyAc.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPartyAc.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPartyAc.BackColor = System.Drawing.Color.White
        Me.TxtPartyAc.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPartyAc.Location = New System.Drawing.Point(136, 34)
        Me.TxtPartyAc.Name = "TxtPartyAc"
        Me.TxtPartyAc.ReadOnly = True
        Me.TxtPartyAc.Size = New System.Drawing.Size(300, 21)
        Me.TxtPartyAc.TabIndex = 1
        Me.TxtPartyAc.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 243
        Me.Label2.Text = "Party Name"
        '
        'TxtBankAc
        '
        Me.TxtBankAc.AgMandatory = False
        Me.TxtBankAc.AgMasterHelp = False
        Me.TxtBankAc.AgNumberLeftPlaces = 8
        Me.TxtBankAc.AgNumberNegetiveAllow = False
        Me.TxtBankAc.AgNumberRightPlaces = 0
        Me.TxtBankAc.AgPickFromLastValue = False
        Me.TxtBankAc.AgRowFilter = ""
        Me.TxtBankAc.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Me.TxtBankAc.AgSelectedValue = Nothing
        Me.TxtBankAc.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBankAc.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBankAc.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBankAc.Location = New System.Drawing.Point(135, 152)
        Me.TxtBankAc.Name = "TxtBankAc"
        Me.TxtBankAc.Size = New System.Drawing.Size(300, 21)
        Me.TxtBankAc.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(10, 156)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 13)
        Me.Label3.TabIndex = 248
        Me.Label3.Text = "Bank A/c"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(10, 222)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(84, 13)
        Me.Label4.TabIndex = 245
        Me.Label4.Text = "Bank Amount"
        '
        'TxtBankAmount
        '
        Me.TxtBankAmount.AgMandatory = False
        Me.TxtBankAmount.AgMasterHelp = False
        Me.TxtBankAmount.AgNumberLeftPlaces = 8
        Me.TxtBankAmount.AgNumberNegetiveAllow = False
        Me.TxtBankAmount.AgNumberRightPlaces = 2
        Me.TxtBankAmount.AgPickFromLastValue = False
        Me.TxtBankAmount.AgRowFilter = ""
        Me.TxtBankAmount.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBankAmount.AgSelectedValue = Nothing
        Me.TxtBankAmount.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBankAmount.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtBankAmount.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBankAmount.Location = New System.Drawing.Point(135, 218)
        Me.TxtBankAmount.Name = "TxtBankAmount"
        Me.TxtBankAmount.Size = New System.Drawing.Size(100, 21)
        Me.TxtBankAmount.TabIndex = 8
        Me.TxtBankAmount.Text = "0.00"
        Me.TxtBankAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtCardAc
        '
        Me.TxtCardAc.AgMandatory = False
        Me.TxtCardAc.AgMasterHelp = False
        Me.TxtCardAc.AgNumberLeftPlaces = 8
        Me.TxtCardAc.AgNumberNegetiveAllow = False
        Me.TxtCardAc.AgNumberRightPlaces = 0
        Me.TxtCardAc.AgPickFromLastValue = False
        Me.TxtCardAc.AgRowFilter = ""
        Me.TxtCardAc.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Me.TxtCardAc.AgSelectedValue = Nothing
        Me.TxtCardAc.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCardAc.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCardAc.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCardAc.Location = New System.Drawing.Point(579, 220)
        Me.TxtCardAc.Name = "TxtCardAc"
        Me.TxtCardAc.Size = New System.Drawing.Size(300, 21)
        Me.TxtCardAc.TabIndex = 26
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(454, 224)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 13)
        Me.Label5.TabIndex = 258
        Me.Label5.Text = "Card A/c"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(688, 268)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(83, 13)
        Me.Label6.TabIndex = 255
        Me.Label6.Text = "Card Amount"
        '
        'TxtCardAmount
        '
        Me.TxtCardAmount.AgMandatory = False
        Me.TxtCardAmount.AgMasterHelp = False
        Me.TxtCardAmount.AgNumberLeftPlaces = 8
        Me.TxtCardAmount.AgNumberNegetiveAllow = False
        Me.TxtCardAmount.AgNumberRightPlaces = 2
        Me.TxtCardAmount.AgPickFromLastValue = False
        Me.TxtCardAmount.AgRowFilter = ""
        Me.TxtCardAmount.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCardAmount.AgSelectedValue = Nothing
        Me.TxtCardAmount.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCardAmount.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtCardAmount.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCardAmount.Location = New System.Drawing.Point(779, 264)
        Me.TxtCardAmount.Name = "TxtCardAmount"
        Me.TxtCardAmount.Size = New System.Drawing.Size(100, 21)
        Me.TxtCardAmount.TabIndex = 29
        Me.TxtCardAmount.Text = "0.00"
        Me.TxtCardAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtCardBank_Code
        '
        Me.TxtCardBank_Code.AgMandatory = False
        Me.TxtCardBank_Code.AgMasterHelp = True
        Me.TxtCardBank_Code.AgNumberLeftPlaces = 8
        Me.TxtCardBank_Code.AgNumberNegetiveAllow = False
        Me.TxtCardBank_Code.AgNumberRightPlaces = 0
        Me.TxtCardBank_Code.AgPickFromLastValue = False
        Me.TxtCardBank_Code.AgRowFilter = ""
        Me.TxtCardBank_Code.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Me.TxtCardBank_Code.AgSelectedValue = Nothing
        Me.TxtCardBank_Code.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCardBank_Code.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCardBank_Code.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCardBank_Code.Location = New System.Drawing.Point(579, 242)
        Me.TxtCardBank_Code.MaxLength = 100
        Me.TxtCardBank_Code.Name = "TxtCardBank_Code"
        Me.TxtCardBank_Code.Size = New System.Drawing.Size(300, 21)
        Me.TxtCardBank_Code.TabIndex = 27
        Me.TxtCardBank_Code.Text = "Bank Name"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(454, 246)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(68, 13)
        Me.Label7.TabIndex = 254
        Me.Label7.Text = "Card Bank"
        '
        'TxtCard_No
        '
        Me.TxtCard_No.AgMandatory = False
        Me.TxtCard_No.AgMasterHelp = False
        Me.TxtCard_No.AgNumberLeftPlaces = 6
        Me.TxtCard_No.AgNumberNegetiveAllow = False
        Me.TxtCard_No.AgNumberRightPlaces = 0
        Me.TxtCard_No.AgPickFromLastValue = False
        Me.TxtCard_No.AgRowFilter = ""
        Me.TxtCard_No.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCard_No.AgSelectedValue = Nothing
        Me.TxtCard_No.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCard_No.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCard_No.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCard_No.Location = New System.Drawing.Point(579, 264)
        Me.TxtCard_No.MaxLength = 20
        Me.TxtCard_No.Name = "TxtCard_No"
        Me.TxtCard_No.Size = New System.Drawing.Size(100, 21)
        Me.TxtCard_No.TabIndex = 28
        Me.TxtCard_No.Text = "Card No."
        Me.TxtCard_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(454, 268)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(58, 13)
        Me.Label9.TabIndex = 253
        Me.Label9.Text = "Card No."
        '
        'TxtClg_Date
        '
        Me.TxtClg_Date.AgMandatory = False
        Me.TxtClg_Date.AgMasterHelp = False
        Me.TxtClg_Date.AgNumberLeftPlaces = 0
        Me.TxtClg_Date.AgNumberNegetiveAllow = False
        Me.TxtClg_Date.AgNumberRightPlaces = 0
        Me.TxtClg_Date.AgPickFromLastValue = False
        Me.TxtClg_Date.AgRowFilter = ""
        Me.TxtClg_Date.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtClg_Date.AgSelectedValue = Nothing
        Me.TxtClg_Date.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtClg_Date.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtClg_Date.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtClg_Date.Location = New System.Drawing.Point(335, 218)
        Me.TxtClg_Date.MaxLength = 10
        Me.TxtClg_Date.Name = "TxtClg_Date"
        Me.TxtClg_Date.Size = New System.Drawing.Size(100, 21)
        Me.TxtClg_Date.TabIndex = 9
        Me.TxtClg_Date.Text = "Cheque Dt"
        Me.TxtClg_Date.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(244, 222)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(76, 13)
        Me.Label8.TabIndex = 260
        Me.Label8.Text = "Clearing Dt."
        Me.Label8.Visible = False
        '
        'TxtAcTransferBankAc
        '
        Me.TxtAcTransferBankAc.AgMandatory = False
        Me.TxtAcTransferBankAc.AgMasterHelp = False
        Me.TxtAcTransferBankAc.AgNumberLeftPlaces = 8
        Me.TxtAcTransferBankAc.AgNumberNegetiveAllow = False
        Me.TxtAcTransferBankAc.AgNumberRightPlaces = 0
        Me.TxtAcTransferBankAc.AgPickFromLastValue = False
        Me.TxtAcTransferBankAc.AgRowFilter = ""
        Me.TxtAcTransferBankAc.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Me.TxtAcTransferBankAc.AgSelectedValue = Nothing
        Me.TxtAcTransferBankAc.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAcTransferBankAc.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAcTransferBankAc.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAcTransferBankAc.Location = New System.Drawing.Point(580, 140)
        Me.TxtAcTransferBankAc.Name = "TxtAcTransferBankAc"
        Me.TxtAcTransferBankAc.Size = New System.Drawing.Size(300, 21)
        Me.TxtAcTransferBankAc.TabIndex = 22
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(455, 144)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(101, 13)
        Me.Label10.TabIndex = 268
        Me.Label10.Text = "A/c Transfer A/c"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(689, 188)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(51, 13)
        Me.Label11.TabIndex = 265
        Me.Label11.Text = "Amount"
        '
        'TxtAcTransferAmount
        '
        Me.TxtAcTransferAmount.AgMandatory = False
        Me.TxtAcTransferAmount.AgMasterHelp = False
        Me.TxtAcTransferAmount.AgNumberLeftPlaces = 8
        Me.TxtAcTransferAmount.AgNumberNegetiveAllow = False
        Me.TxtAcTransferAmount.AgNumberRightPlaces = 2
        Me.TxtAcTransferAmount.AgPickFromLastValue = False
        Me.TxtAcTransferAmount.AgRowFilter = ""
        Me.TxtAcTransferAmount.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAcTransferAmount.AgSelectedValue = Nothing
        Me.TxtAcTransferAmount.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAcTransferAmount.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtAcTransferAmount.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAcTransferAmount.Location = New System.Drawing.Point(780, 184)
        Me.TxtAcTransferAmount.Name = "TxtAcTransferAmount"
        Me.TxtAcTransferAmount.Size = New System.Drawing.Size(100, 21)
        Me.TxtAcTransferAmount.TabIndex = 25
        Me.TxtAcTransferAmount.Text = "0.00"
        Me.TxtAcTransferAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtAcTransferBank_Code
        '
        Me.TxtAcTransferBank_Code.AgMandatory = False
        Me.TxtAcTransferBank_Code.AgMasterHelp = True
        Me.TxtAcTransferBank_Code.AgNumberLeftPlaces = 8
        Me.TxtAcTransferBank_Code.AgNumberNegetiveAllow = False
        Me.TxtAcTransferBank_Code.AgNumberRightPlaces = 0
        Me.TxtAcTransferBank_Code.AgPickFromLastValue = False
        Me.TxtAcTransferBank_Code.AgRowFilter = ""
        Me.TxtAcTransferBank_Code.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Me.TxtAcTransferBank_Code.AgSelectedValue = Nothing
        Me.TxtAcTransferBank_Code.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAcTransferBank_Code.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAcTransferBank_Code.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAcTransferBank_Code.Location = New System.Drawing.Point(580, 162)
        Me.TxtAcTransferBank_Code.MaxLength = 100
        Me.TxtAcTransferBank_Code.Name = "TxtAcTransferBank_Code"
        Me.TxtAcTransferBank_Code.Size = New System.Drawing.Size(300, 21)
        Me.TxtAcTransferBank_Code.TabIndex = 23
        Me.TxtAcTransferBank_Code.Text = "Bank Name"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(455, 166)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(113, 13)
        Me.Label12.TabIndex = 264
        Me.Label12.Text = "A/c Transser Bank"
        '
        'TxtAcTransferAcNo
        '
        Me.TxtAcTransferAcNo.AgMandatory = False
        Me.TxtAcTransferAcNo.AgMasterHelp = False
        Me.TxtAcTransferAcNo.AgNumberLeftPlaces = 6
        Me.TxtAcTransferAcNo.AgNumberNegetiveAllow = False
        Me.TxtAcTransferAcNo.AgNumberRightPlaces = 0
        Me.TxtAcTransferAcNo.AgPickFromLastValue = False
        Me.TxtAcTransferAcNo.AgRowFilter = ""
        Me.TxtAcTransferAcNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAcTransferAcNo.AgSelectedValue = Nothing
        Me.TxtAcTransferAcNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAcTransferAcNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAcTransferAcNo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAcTransferAcNo.Location = New System.Drawing.Point(580, 184)
        Me.TxtAcTransferAcNo.MaxLength = 20
        Me.TxtAcTransferAcNo.Name = "TxtAcTransferAcNo"
        Me.TxtAcTransferAcNo.Size = New System.Drawing.Size(100, 21)
        Me.TxtAcTransferAcNo.TabIndex = 24
        Me.TxtAcTransferAcNo.Text = "A/c No."
        Me.TxtAcTransferAcNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(455, 188)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(124, 13)
        Me.Label13.TabIndex = 263
        Me.Label13.Text = "A/c Transfer A/c No."
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Blue
        Me.Label15.Location = New System.Drawing.Point(689, 356)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(89, 13)
        Me.Label15.TabIndex = 269
        Me.Label15.Text = "Total Payment"
        '
        'TxtTotalPayment
        '
        Me.TxtTotalPayment.AgMandatory = False
        Me.TxtTotalPayment.AgMasterHelp = False
        Me.TxtTotalPayment.AgNumberLeftPlaces = 8
        Me.TxtTotalPayment.AgNumberNegetiveAllow = False
        Me.TxtTotalPayment.AgNumberRightPlaces = 2
        Me.TxtTotalPayment.AgPickFromLastValue = False
        Me.TxtTotalPayment.AgRowFilter = ""
        Me.TxtTotalPayment.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTotalPayment.AgSelectedValue = Nothing
        Me.TxtTotalPayment.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTotalPayment.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtTotalPayment.BackColor = System.Drawing.Color.White
        Me.TxtTotalPayment.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTotalPayment.ForeColor = System.Drawing.Color.Blue
        Me.TxtTotalPayment.Location = New System.Drawing.Point(780, 352)
        Me.TxtTotalPayment.Name = "TxtTotalPayment"
        Me.TxtTotalPayment.ReadOnly = True
        Me.TxtTotalPayment.Size = New System.Drawing.Size(100, 21)
        Me.TxtTotalPayment.TabIndex = 33
        Me.TxtTotalPayment.TabStop = False
        Me.TxtTotalPayment.Text = "0.00"
        Me.TxtTotalPayment.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'BtnExit
        '
        Me.BtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.BtnExit.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnExit.Location = New System.Drawing.Point(780, 381)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(100, 23)
        Me.BtnExit.TabIndex = 34
        Me.BtnExit.Text = "E&xit"
        Me.BtnExit.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(11, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(103, 13)
        Me.Label1.TabIndex = 270
        Me.Label1.Text = "Cash Payment:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Blue
        Me.Label14.Location = New System.Drawing.Point(10, 130)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(174, 13)
        Me.Label14.TabIndex = 271
        Me.Label14.Text = "Cheque/DD Payment (1) :"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Blue
        Me.Label16.Location = New System.Drawing.Point(454, 207)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(102, 13)
        Me.Label16.TabIndex = 272
        Me.Label16.Text = "Card Payment:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Blue
        Me.Label17.Location = New System.Drawing.Point(455, 124)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(156, 13)
        Me.Label17.TabIndex = 273
        Me.Label17.Text = "A/c Transfer Payment:"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.Blue
        Me.Label27.Location = New System.Drawing.Point(455, 15)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(174, 13)
        Me.Label27.TabIndex = 297
        Me.Label27.Text = "Cheque/DD Payment (3) :"
        '
        'TxtClg_Date3
        '
        Me.TxtClg_Date3.AgMandatory = False
        Me.TxtClg_Date3.AgMasterHelp = False
        Me.TxtClg_Date3.AgNumberLeftPlaces = 0
        Me.TxtClg_Date3.AgNumberNegetiveAllow = False
        Me.TxtClg_Date3.AgNumberRightPlaces = 0
        Me.TxtClg_Date3.AgPickFromLastValue = False
        Me.TxtClg_Date3.AgRowFilter = ""
        Me.TxtClg_Date3.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtClg_Date3.AgSelectedValue = Nothing
        Me.TxtClg_Date3.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtClg_Date3.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtClg_Date3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtClg_Date3.Location = New System.Drawing.Point(779, 100)
        Me.TxtClg_Date3.MaxLength = 10
        Me.TxtClg_Date3.Name = "TxtClg_Date3"
        Me.TxtClg_Date3.Size = New System.Drawing.Size(100, 21)
        Me.TxtClg_Date3.TabIndex = 21
        Me.TxtClg_Date3.Text = "Cheque Dt"
        Me.TxtClg_Date3.Visible = False
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(689, 104)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(76, 13)
        Me.Label28.TabIndex = 296
        Me.Label28.Text = "Clearing Dt."
        Me.Label28.Visible = False
        '
        'TxtBankAc3
        '
        Me.TxtBankAc3.AgMandatory = False
        Me.TxtBankAc3.AgMasterHelp = False
        Me.TxtBankAc3.AgNumberLeftPlaces = 8
        Me.TxtBankAc3.AgNumberNegetiveAllow = False
        Me.TxtBankAc3.AgNumberRightPlaces = 0
        Me.TxtBankAc3.AgPickFromLastValue = False
        Me.TxtBankAc3.AgRowFilter = ""
        Me.TxtBankAc3.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Me.TxtBankAc3.AgSelectedValue = Nothing
        Me.TxtBankAc3.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBankAc3.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBankAc3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBankAc3.Location = New System.Drawing.Point(579, 34)
        Me.TxtBankAc3.Name = "TxtBankAc3"
        Me.TxtBankAc3.Size = New System.Drawing.Size(300, 21)
        Me.TxtBankAc3.TabIndex = 16
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(455, 38)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(59, 13)
        Me.Label29.TabIndex = 295
        Me.Label29.Text = "Bank A/c"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(455, 104)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(84, 13)
        Me.Label30.TabIndex = 294
        Me.Label30.Text = "Bank Amount"
        '
        'TxtBankAmount3
        '
        Me.TxtBankAmount3.AgMandatory = False
        Me.TxtBankAmount3.AgMasterHelp = False
        Me.TxtBankAmount3.AgNumberLeftPlaces = 8
        Me.TxtBankAmount3.AgNumberNegetiveAllow = False
        Me.TxtBankAmount3.AgNumberRightPlaces = 2
        Me.TxtBankAmount3.AgPickFromLastValue = False
        Me.TxtBankAmount3.AgRowFilter = ""
        Me.TxtBankAmount3.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBankAmount3.AgSelectedValue = Nothing
        Me.TxtBankAmount3.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBankAmount3.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtBankAmount3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBankAmount3.Location = New System.Drawing.Point(579, 100)
        Me.TxtBankAmount3.Name = "TxtBankAmount3"
        Me.TxtBankAmount3.Size = New System.Drawing.Size(100, 21)
        Me.TxtBankAmount3.TabIndex = 20
        Me.TxtBankAmount3.Text = "0.00"
        Me.TxtBankAmount3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtBank_Code3
        '
        Me.TxtBank_Code3.AgMandatory = False
        Me.TxtBank_Code3.AgMasterHelp = True
        Me.TxtBank_Code3.AgNumberLeftPlaces = 8
        Me.TxtBank_Code3.AgNumberNegetiveAllow = False
        Me.TxtBank_Code3.AgNumberRightPlaces = 0
        Me.TxtBank_Code3.AgPickFromLastValue = False
        Me.TxtBank_Code3.AgRowFilter = ""
        Me.TxtBank_Code3.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Me.TxtBank_Code3.AgSelectedValue = Nothing
        Me.TxtBank_Code3.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBank_Code3.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBank_Code3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBank_Code3.Location = New System.Drawing.Point(579, 56)
        Me.TxtBank_Code3.MaxLength = 100
        Me.TxtBank_Code3.Name = "TxtBank_Code3"
        Me.TxtBank_Code3.Size = New System.Drawing.Size(300, 21)
        Me.TxtBank_Code3.TabIndex = 17
        Me.TxtBank_Code3.Text = "Bank Name"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(455, 60)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(73, 13)
        Me.Label31.TabIndex = 293
        Me.Label31.Text = "Bank Name"
        '
        'TxtChq_Date3
        '
        Me.TxtChq_Date3.AgMandatory = False
        Me.TxtChq_Date3.AgMasterHelp = False
        Me.TxtChq_Date3.AgNumberLeftPlaces = 0
        Me.TxtChq_Date3.AgNumberNegetiveAllow = False
        Me.TxtChq_Date3.AgNumberRightPlaces = 0
        Me.TxtChq_Date3.AgPickFromLastValue = False
        Me.TxtChq_Date3.AgRowFilter = ""
        Me.TxtChq_Date3.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtChq_Date3.AgSelectedValue = Nothing
        Me.TxtChq_Date3.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtChq_Date3.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtChq_Date3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtChq_Date3.Location = New System.Drawing.Point(779, 78)
        Me.TxtChq_Date3.MaxLength = 10
        Me.TxtChq_Date3.Name = "TxtChq_Date3"
        Me.TxtChq_Date3.Size = New System.Drawing.Size(100, 21)
        Me.TxtChq_Date3.TabIndex = 19
        Me.TxtChq_Date3.Text = "Cheque Dt"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(689, 82)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(72, 13)
        Me.Label32.TabIndex = 291
        Me.Label32.Text = "Cheque Dt."
        '
        'TxtChq_No3
        '
        Me.TxtChq_No3.AgMandatory = False
        Me.TxtChq_No3.AgMasterHelp = False
        Me.TxtChq_No3.AgNumberLeftPlaces = 6
        Me.TxtChq_No3.AgNumberNegetiveAllow = False
        Me.TxtChq_No3.AgNumberRightPlaces = 0
        Me.TxtChq_No3.AgPickFromLastValue = False
        Me.TxtChq_No3.AgRowFilter = ""
        Me.TxtChq_No3.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtChq_No3.AgSelectedValue = Nothing
        Me.TxtChq_No3.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtChq_No3.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtChq_No3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtChq_No3.Location = New System.Drawing.Point(579, 78)
        Me.TxtChq_No3.MaxLength = 10
        Me.TxtChq_No3.Name = "TxtChq_No3"
        Me.TxtChq_No3.Size = New System.Drawing.Size(100, 21)
        Me.TxtChq_No3.TabIndex = 18
        Me.TxtChq_No3.Text = "Cheque No."
        Me.TxtChq_No3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(455, 82)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(74, 13)
        Me.Label33.TabIndex = 292
        Me.Label33.Text = "Cheque No."
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.Blue
        Me.Label18.Location = New System.Drawing.Point(11, 255)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(174, 13)
        Me.Label18.TabIndex = 284
        Me.Label18.Text = "Cheque/DD Payment (2) :"
        '
        'TxtClg_Date2
        '
        Me.TxtClg_Date2.AgMandatory = False
        Me.TxtClg_Date2.AgMasterHelp = False
        Me.TxtClg_Date2.AgNumberLeftPlaces = 0
        Me.TxtClg_Date2.AgNumberNegetiveAllow = False
        Me.TxtClg_Date2.AgNumberRightPlaces = 0
        Me.TxtClg_Date2.AgPickFromLastValue = False
        Me.TxtClg_Date2.AgRowFilter = ""
        Me.TxtClg_Date2.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtClg_Date2.AgSelectedValue = Nothing
        Me.TxtClg_Date2.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtClg_Date2.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtClg_Date2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtClg_Date2.Location = New System.Drawing.Point(335, 352)
        Me.TxtClg_Date2.MaxLength = 10
        Me.TxtClg_Date2.Name = "TxtClg_Date2"
        Me.TxtClg_Date2.Size = New System.Drawing.Size(100, 21)
        Me.TxtClg_Date2.TabIndex = 15
        Me.TxtClg_Date2.Text = "Cheque Dt"
        Me.TxtClg_Date2.Visible = False
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(245, 356)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(76, 13)
        Me.Label19.TabIndex = 283
        Me.Label19.Text = "Clearing Dt."
        Me.Label19.Visible = False
        '
        'TxtBankAc2
        '
        Me.TxtBankAc2.AgMandatory = False
        Me.TxtBankAc2.AgMasterHelp = False
        Me.TxtBankAc2.AgNumberLeftPlaces = 8
        Me.TxtBankAc2.AgNumberNegetiveAllow = False
        Me.TxtBankAc2.AgNumberRightPlaces = 0
        Me.TxtBankAc2.AgPickFromLastValue = False
        Me.TxtBankAc2.AgRowFilter = ""
        Me.TxtBankAc2.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Me.TxtBankAc2.AgSelectedValue = Nothing
        Me.TxtBankAc2.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBankAc2.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBankAc2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBankAc2.Location = New System.Drawing.Point(135, 286)
        Me.TxtBankAc2.Name = "TxtBankAc2"
        Me.TxtBankAc2.Size = New System.Drawing.Size(300, 21)
        Me.TxtBankAc2.TabIndex = 10
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(11, 290)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(59, 13)
        Me.Label20.TabIndex = 282
        Me.Label20.Text = "Bank A/c"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(11, 356)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(84, 13)
        Me.Label21.TabIndex = 281
        Me.Label21.Text = "Bank Amount"
        '
        'TxtBankAmount2
        '
        Me.TxtBankAmount2.AgMandatory = False
        Me.TxtBankAmount2.AgMasterHelp = False
        Me.TxtBankAmount2.AgNumberLeftPlaces = 8
        Me.TxtBankAmount2.AgNumberNegetiveAllow = False
        Me.TxtBankAmount2.AgNumberRightPlaces = 2
        Me.TxtBankAmount2.AgPickFromLastValue = False
        Me.TxtBankAmount2.AgRowFilter = ""
        Me.TxtBankAmount2.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBankAmount2.AgSelectedValue = Nothing
        Me.TxtBankAmount2.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBankAmount2.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtBankAmount2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBankAmount2.Location = New System.Drawing.Point(135, 352)
        Me.TxtBankAmount2.Name = "TxtBankAmount2"
        Me.TxtBankAmount2.Size = New System.Drawing.Size(100, 21)
        Me.TxtBankAmount2.TabIndex = 14
        Me.TxtBankAmount2.Text = "0.00"
        Me.TxtBankAmount2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtBank_Code2
        '
        Me.TxtBank_Code2.AgMandatory = False
        Me.TxtBank_Code2.AgMasterHelp = True
        Me.TxtBank_Code2.AgNumberLeftPlaces = 8
        Me.TxtBank_Code2.AgNumberNegetiveAllow = False
        Me.TxtBank_Code2.AgNumberRightPlaces = 0
        Me.TxtBank_Code2.AgPickFromLastValue = False
        Me.TxtBank_Code2.AgRowFilter = ""
        Me.TxtBank_Code2.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Me.TxtBank_Code2.AgSelectedValue = Nothing
        Me.TxtBank_Code2.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBank_Code2.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBank_Code2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBank_Code2.Location = New System.Drawing.Point(135, 308)
        Me.TxtBank_Code2.MaxLength = 100
        Me.TxtBank_Code2.Name = "TxtBank_Code2"
        Me.TxtBank_Code2.Size = New System.Drawing.Size(300, 21)
        Me.TxtBank_Code2.TabIndex = 11
        Me.TxtBank_Code2.Text = "Bank Name"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(11, 312)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(73, 13)
        Me.Label22.TabIndex = 280
        Me.Label22.Text = "Bank Name"
        '
        'TxtChq_Date2
        '
        Me.TxtChq_Date2.AgMandatory = False
        Me.TxtChq_Date2.AgMasterHelp = False
        Me.TxtChq_Date2.AgNumberLeftPlaces = 0
        Me.TxtChq_Date2.AgNumberNegetiveAllow = False
        Me.TxtChq_Date2.AgNumberRightPlaces = 0
        Me.TxtChq_Date2.AgPickFromLastValue = False
        Me.TxtChq_Date2.AgRowFilter = ""
        Me.TxtChq_Date2.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtChq_Date2.AgSelectedValue = Nothing
        Me.TxtChq_Date2.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtChq_Date2.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtChq_Date2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtChq_Date2.Location = New System.Drawing.Point(335, 330)
        Me.TxtChq_Date2.MaxLength = 10
        Me.TxtChq_Date2.Name = "TxtChq_Date2"
        Me.TxtChq_Date2.Size = New System.Drawing.Size(100, 21)
        Me.TxtChq_Date2.TabIndex = 13
        Me.TxtChq_Date2.Text = "Cheque Dt"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(245, 334)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(72, 13)
        Me.Label25.TabIndex = 278
        Me.Label25.Text = "Cheque Dt."
        '
        'TxtChq_No2
        '
        Me.TxtChq_No2.AgMandatory = False
        Me.TxtChq_No2.AgMasterHelp = False
        Me.TxtChq_No2.AgNumberLeftPlaces = 6
        Me.TxtChq_No2.AgNumberNegetiveAllow = False
        Me.TxtChq_No2.AgNumberRightPlaces = 0
        Me.TxtChq_No2.AgPickFromLastValue = False
        Me.TxtChq_No2.AgRowFilter = ""
        Me.TxtChq_No2.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtChq_No2.AgSelectedValue = Nothing
        Me.TxtChq_No2.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtChq_No2.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtChq_No2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtChq_No2.Location = New System.Drawing.Point(135, 330)
        Me.TxtChq_No2.MaxLength = 10
        Me.TxtChq_No2.Name = "TxtChq_No2"
        Me.TxtChq_No2.Size = New System.Drawing.Size(100, 21)
        Me.TxtChq_No2.TabIndex = 12
        Me.TxtChq_No2.Text = "Cheque No."
        Me.TxtChq_No2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(11, 334)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(74, 13)
        Me.Label26.TabIndex = 279
        Me.Label26.Text = "Cheque No."
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.Blue
        Me.Label34.Location = New System.Drawing.Point(455, 290)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(86, 13)
        Me.Label34.TabIndex = 302
        Me.Label34.Text = "Adjustment:"
        '
        'TxtAdjustmentAc
        '
        Me.TxtAdjustmentAc.AgMandatory = False
        Me.TxtAdjustmentAc.AgMasterHelp = False
        Me.TxtAdjustmentAc.AgNumberLeftPlaces = 8
        Me.TxtAdjustmentAc.AgNumberNegetiveAllow = False
        Me.TxtAdjustmentAc.AgNumberRightPlaces = 0
        Me.TxtAdjustmentAc.AgPickFromLastValue = False
        Me.TxtAdjustmentAc.AgRowFilter = ""
        Me.TxtAdjustmentAc.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Me.TxtAdjustmentAc.AgSelectedValue = Nothing
        Me.TxtAdjustmentAc.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAdjustmentAc.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAdjustmentAc.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAdjustmentAc.Location = New System.Drawing.Point(580, 308)
        Me.TxtAdjustmentAc.Name = "TxtAdjustmentAc"
        Me.TxtAdjustmentAc.Size = New System.Drawing.Size(300, 21)
        Me.TxtAdjustmentAc.TabIndex = 30
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(455, 312)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(95, 13)
        Me.Label35.TabIndex = 301
        Me.Label35.Text = "Adjustment A/c"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(455, 356)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(120, 13)
        Me.Label36.TabIndex = 298
        Me.Label36.Text = "Adjustment Amount"
        '
        'TxtAdjustmentAmount
        '
        Me.TxtAdjustmentAmount.AgMandatory = False
        Me.TxtAdjustmentAmount.AgMasterHelp = False
        Me.TxtAdjustmentAmount.AgNumberLeftPlaces = 8
        Me.TxtAdjustmentAmount.AgNumberNegetiveAllow = False
        Me.TxtAdjustmentAmount.AgNumberRightPlaces = 2
        Me.TxtAdjustmentAmount.AgPickFromLastValue = False
        Me.TxtAdjustmentAmount.AgRowFilter = ""
        Me.TxtAdjustmentAmount.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAdjustmentAmount.AgSelectedValue = Nothing
        Me.TxtAdjustmentAmount.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAdjustmentAmount.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtAdjustmentAmount.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAdjustmentAmount.Location = New System.Drawing.Point(580, 352)
        Me.TxtAdjustmentAmount.Name = "TxtAdjustmentAmount"
        Me.TxtAdjustmentAmount.Size = New System.Drawing.Size(100, 21)
        Me.TxtAdjustmentAmount.TabIndex = 32
        Me.TxtAdjustmentAmount.Text = "0.00"
        Me.TxtAdjustmentAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TxtAdjustmentRemark
        '
        Me.TxtAdjustmentRemark.AgMandatory = False
        Me.TxtAdjustmentRemark.AgMasterHelp = False
        Me.TxtAdjustmentRemark.AgNumberLeftPlaces = 8
        Me.TxtAdjustmentRemark.AgNumberNegetiveAllow = False
        Me.TxtAdjustmentRemark.AgNumberRightPlaces = 0
        Me.TxtAdjustmentRemark.AgPickFromLastValue = False
        Me.TxtAdjustmentRemark.AgRowFilter = ""
        Me.TxtAdjustmentRemark.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Me.TxtAdjustmentRemark.AgSelectedValue = Nothing
        Me.TxtAdjustmentRemark.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAdjustmentRemark.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAdjustmentRemark.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAdjustmentRemark.Location = New System.Drawing.Point(580, 330)
        Me.TxtAdjustmentRemark.MaxLength = 255
        Me.TxtAdjustmentRemark.Name = "TxtAdjustmentRemark"
        Me.TxtAdjustmentRemark.Size = New System.Drawing.Size(300, 21)
        Me.TxtAdjustmentRemark.TabIndex = 31
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(454, 334)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(121, 13)
        Me.Label37.TabIndex = 304
        Me.Label37.Text = "Adjustment Remark"
        '
        'FrmPaymentDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(892, 416)
        Me.Controls.Add(Me.TxtAdjustmentRemark)
        Me.Controls.Add(Me.Label37)
        Me.Controls.Add(Me.Label34)
        Me.Controls.Add(Me.TxtAdjustmentAc)
        Me.Controls.Add(Me.Label35)
        Me.Controls.Add(Me.Label36)
        Me.Controls.Add(Me.TxtAdjustmentAmount)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.TxtClg_Date3)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.TxtBankAc3)
        Me.Controls.Add(Me.Label29)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label30)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtBankAmount3)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.TxtBank_Code3)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label31)
        Me.Controls.Add(Me.TxtTotalPayment)
        Me.Controls.Add(Me.TxtChq_Date3)
        Me.Controls.Add(Me.TxtAcTransferBankAc)
        Me.Controls.Add(Me.Label32)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.TxtChq_No3)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label33)
        Me.Controls.Add(Me.TxtAcTransferAmount)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.TxtAcTransferBank_Code)
        Me.Controls.Add(Me.TxtClg_Date2)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.TxtAcTransferAcNo)
        Me.Controls.Add(Me.TxtBankAc2)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.TxtClg_Date)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TxtBankAmount2)
        Me.Controls.Add(Me.TxtCardAc)
        Me.Controls.Add(Me.TxtBank_Code2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtChq_Date2)
        Me.Controls.Add(Me.TxtCardAmount)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.TxtCardBank_Code)
        Me.Controls.Add(Me.TxtChq_No2)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.TxtCard_No)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TxtBankAc)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtBankAmount)
        Me.Controls.Add(Me.TxtPartyAc)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtDocId)
        Me.Controls.Add(Me.LblDocId)
        Me.Controls.Add(Me.TxtBank_Code)
        Me.Controls.Add(Me.LblBank_Code)
        Me.Controls.Add(Me.TxtChq_Date)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.TxtChq_No)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.TxtCashAc)
        Me.Controls.Add(Me.LblCashAc)
        Me.Controls.Add(Me.LblCashAmount)
        Me.Controls.Add(Me.TxtCashAmount)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmPaymentDetail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Payment Detail"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TxtCashAmount As AgControls.AgTextBox
    Friend WithEvents LblCashAmount As System.Windows.Forms.Label
    Friend WithEvents LblCashAc As System.Windows.Forms.Label
    Friend WithEvents TxtCashAc As AgControls.AgTextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents TxtChq_No As AgControls.AgTextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents TxtChq_Date As AgControls.AgTextBox
    Friend WithEvents LblBank_Code As System.Windows.Forms.Label
    Friend WithEvents TxtBank_Code As AgControls.AgTextBox
    Friend WithEvents TxtDocId As AgControls.AgTextBox
    Friend WithEvents LblDocId As System.Windows.Forms.Label
    Friend WithEvents TxtPartyAc As AgControls.AgTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtBankAc As AgControls.AgTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtBankAmount As AgControls.AgTextBox
    Friend WithEvents TxtCardAc As AgControls.AgTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtCardAmount As AgControls.AgTextBox
    Friend WithEvents TxtCardBank_Code As AgControls.AgTextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TxtCard_No As AgControls.AgTextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TxtClg_Date As AgControls.AgTextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TxtAcTransferBankAc As AgControls.AgTextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TxtAcTransferAmount As AgControls.AgTextBox
    Friend WithEvents TxtAcTransferBank_Code As AgControls.AgTextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TxtAcTransferAcNo As AgControls.AgTextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents TxtTotalPayment As AgControls.AgTextBox
    Friend WithEvents BtnExit As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents TxtClg_Date3 As AgControls.AgTextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents TxtBankAc3 As AgControls.AgTextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents TxtBankAmount3 As AgControls.AgTextBox
    Friend WithEvents TxtBank_Code3 As AgControls.AgTextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents TxtChq_Date3 As AgControls.AgTextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents TxtChq_No3 As AgControls.AgTextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents TxtClg_Date2 As AgControls.AgTextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents TxtBankAc2 As AgControls.AgTextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents TxtBankAmount2 As AgControls.AgTextBox
    Friend WithEvents TxtBank_Code2 As AgControls.AgTextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents TxtChq_Date2 As AgControls.AgTextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents TxtChq_No2 As AgControls.AgTextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents TxtAdjustmentAc As AgControls.AgTextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents TxtAdjustmentAmount As AgControls.AgTextBox
    Friend WithEvents TxtAdjustmentRemark As AgControls.AgTextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
End Class
