Imports System.Data.SQLite
Public Class TempVisitorsGateInOut
    Inherits AgTemplate.TempTransaction
    Public mQry$

    'Dim blnFlagOutdetail As Boolean
    Public Event BaseFunction_MoveRecLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer)
    Public Event BaseEvent_Save_InTransLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand)

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TempVisitorsGateInOut))
        Me.TxtVisitorName = New AgControls.AgTextBox
        Me.LblVisitorName = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtAddress = New AgControls.AgTextBox
        Me.TxtVehicleNo = New AgControls.AgTextBox
        Me.LblVehicleNo = New System.Windows.Forms.Label
        Me.LblVisitorNameReq = New System.Windows.Forms.Label
        Me.TxtPhone = New AgControls.AgTextBox
        Me.LblPhone = New System.Windows.Forms.Label
        Me.TxtPersonToMeet = New AgControls.AgTextBox
        Me.TxtPerpose = New AgControls.AgTextBox
        Me.LblPurpose = New System.Windows.Forms.Label
        Me.LblPersonToMeetReq = New System.Windows.Forms.Label
        Me.TxtEntryTime = New AgControls.AgTextBox
        Me.LblOutEntryBy = New System.Windows.Forms.Label
        Me.TxtOutEntryBy = New AgControls.AgTextBox
        Me.TxtOutDate = New AgControls.AgTextBox
        Me.LblOutDate = New System.Windows.Forms.Label
        Me.TxtOutTime = New AgControls.AgTextBox
        Me.GrpGateOutEntry = New System.Windows.Forms.GroupBox
        Me.BtnSaveGateOutEntry = New System.Windows.Forms.Button
        Me.LinkLabel5 = New System.Windows.Forms.LinkLabel
        Me.LblPersonToMeet = New System.Windows.Forms.Label
        Me.LblAddressReq = New System.Windows.Forms.Label
        Me.LblManualNo = New System.Windows.Forms.Label
        Me.TxtManualNo = New AgControls.AgTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtDateValue = New AgControls.AgTextBox
        Me.LblVisitorID = New System.Windows.Forms.Label
        Me.TxtVisitorID = New AgControls.AgTextBox
        Me.LblPassNo = New System.Windows.Forms.Label
        Me.TxtPassNo = New AgControls.AgTextBox
        Me.GroupBox2.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GrpUP.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TP1.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpGateOutEntry.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(702, 482)
        Me.GroupBox2.Size = New System.Drawing.Size(148, 40)
        '
        'TxtStatus
        '
        Me.TxtStatus.AgSelectedValue = ""
        Me.TxtStatus.Location = New System.Drawing.Point(29, 19)
        Me.TxtStatus.Tag = ""
        '
        'CmdStatus
        '
        Me.CmdStatus.Size = New System.Drawing.Size(26, 19)
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(625, 482)
        Me.GBoxMoveToLog.Size = New System.Drawing.Size(148, 40)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Location = New System.Drawing.Point(3, 19)
        Me.TxtMoveToLog.Size = New System.Drawing.Size(142, 18)
        Me.TxtMoveToLog.Tag = ""
        '
        'CmdMoveToLog
        '
        Me.CmdMoveToLog.Size = New System.Drawing.Size(137, 19)
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(452, 482)
        Me.GBoxApprove.Size = New System.Drawing.Size(148, 40)
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Location = New System.Drawing.Point(29, 19)
        Me.TxtApproveBy.Tag = ""
        '
        'CmdDiscard
        '
        Me.CmdDiscard.Size = New System.Drawing.Size(26, 19)
        '
        'CmdApprove
        '
        Me.CmdApprove.Size = New System.Drawing.Size(26, 19)
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(169, 482)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(25, 482)
        Me.GrpUP.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GroupBox1
        '
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 478)
        Me.GroupBox1.Size = New System.Drawing.Size(877, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(313, 482)
        Me.GBoxDivision.Size = New System.Drawing.Size(114, 40)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Location = New System.Drawing.Point(3, 19)
        Me.TxtDivision.Tag = ""
        '
        'TxtDocId
        '
        Me.TxtDocId.AgSelectedValue = ""
        Me.TxtDocId.BackColor = System.Drawing.Color.White
        Me.TxtDocId.Location = New System.Drawing.Point(59, 13)
        Me.TxtDocId.Tag = ""
        Me.TxtDocId.Text = ""
        '
        'LblV_No
        '
        Me.LblV_No.Location = New System.Drawing.Point(14, 142)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Visible = False
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(109, 141)
        Me.TxtV_No.Size = New System.Drawing.Size(51, 18)
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtV_No.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(529, 36)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(453, 29)
        Me.LblV_Date.Size = New System.Drawing.Size(80, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Date && Time"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(94, 127)
        Me.LblV_TypeReq.Tag = ""
        Me.LblV_TypeReq.Visible = False
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(545, 28)
        Me.TxtV_Date.Size = New System.Drawing.Size(79, 18)
        Me.TxtV_Date.TabIndex = 1
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(14, 122)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Visible = False
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(109, 121)
        Me.TxtV_Type.Size = New System.Drawing.Size(51, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        Me.TxtV_Type.Visible = False
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(74, 166)
        Me.LblSite_CodeReq.Tag = ""
        Me.LblSite_CodeReq.Visible = False
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(-4, 162)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        Me.LblSite_Code.Visible = False
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(89, 160)
        Me.TxtSite_Code.Size = New System.Drawing.Size(71, 18)
        Me.TxtSite_Code.TabIndex = 0
        Me.TxtSite_Code.Tag = ""
        Me.TxtSite_Code.Visible = False
        '
        'LblDocId
        '
        Me.LblDocId.Location = New System.Drawing.Point(12, 15)
        Me.LblDocId.Tag = ""
        '
        'LblPrefix
        '
        Me.LblPrefix.Location = New System.Drawing.Point(129, 14)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-3, 12)
        Me.TabControl1.Size = New System.Drawing.Size(871, 470)
        Me.TabControl1.TabIndex = 2
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.TxtPassNo)
        Me.TP1.Controls.Add(Me.LblVisitorID)
        Me.TP1.Controls.Add(Me.TxtVisitorID)
        Me.TP1.Controls.Add(Me.TxtDateValue)
        Me.TP1.Controls.Add(Me.Label3)
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.LblManualNo)
        Me.TP1.Controls.Add(Me.TxtManualNo)
        Me.TP1.Controls.Add(Me.LblAddressReq)
        Me.TP1.Controls.Add(Me.LblPersonToMeet)
        Me.TP1.Controls.Add(Me.GrpGateOutEntry)
        Me.TP1.Controls.Add(Me.TxtPersonToMeet)
        Me.TP1.Controls.Add(Me.TxtEntryTime)
        Me.TP1.Controls.Add(Me.LblVisitorName)
        Me.TP1.Controls.Add(Me.TxtVisitorName)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.TxtAddress)
        Me.TP1.Controls.Add(Me.LblVehicleNo)
        Me.TP1.Controls.Add(Me.TxtVehicleNo)
        Me.TP1.Controls.Add(Me.LblVisitorNameReq)
        Me.TP1.Controls.Add(Me.LblPhone)
        Me.TP1.Controls.Add(Me.TxtPhone)
        Me.TP1.Controls.Add(Me.LblPersonToMeetReq)
        Me.TP1.Controls.Add(Me.TxtPerpose)
        Me.TP1.Controls.Add(Me.LblPurpose)
        Me.TP1.Controls.Add(Me.LblPassNo)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(863, 444)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblPassNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPurpose, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtPerpose, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPersonToMeetReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtPhone, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPhone, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblVisitorNameReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVehicleNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblVehicleNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtAddress, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVisitorName, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblVisitorName, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtEntryTime, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtPersonToMeet, 0)
        Me.TP1.Controls.SetChildIndex(Me.GrpGateOutEntry, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPersonToMeet, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblAddressReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtManualNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblManualNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label3, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDateValue, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVisitorID, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblVisitorID, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtPassNo, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(859, 41)
        Me.Topctrl1.TabIndex = 0
        '
        'TxtVisitorName
        '
        Me.TxtVisitorName.AgMandatory = True
        Me.TxtVisitorName.AgMasterHelp = False
        Me.TxtVisitorName.AgNumberLeftPlaces = 8
        Me.TxtVisitorName.AgNumberNegetiveAllow = False
        Me.TxtVisitorName.AgNumberRightPlaces = 2
        Me.TxtVisitorName.AgPickFromLastValue = False
        Me.TxtVisitorName.AgRowFilter = ""
        Me.TxtVisitorName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVisitorName.AgSelectedValue = Nothing
        Me.TxtVisitorName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVisitorName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVisitorName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVisitorName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVisitorName.Location = New System.Drawing.Point(301, 48)
        Me.TxtVisitorName.MaxLength = 100
        Me.TxtVisitorName.Name = "TxtVisitorName"
        Me.TxtVisitorName.Size = New System.Drawing.Size(381, 18)
        Me.TxtVisitorName.TabIndex = 3
        '
        'LblVisitorName
        '
        Me.LblVisitorName.AutoSize = True
        Me.LblVisitorName.BackColor = System.Drawing.Color.Transparent
        Me.LblVisitorName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVisitorName.Location = New System.Drawing.Point(180, 49)
        Me.LblVisitorName.Name = "LblVisitorName"
        Me.LblVisitorName.Size = New System.Drawing.Size(83, 16)
        Me.LblVisitorName.TabIndex = 706
        Me.LblVisitorName.Text = "Visitor Name"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(180, 69)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(56, 16)
        Me.Label30.TabIndex = 723
        Me.Label30.Text = "Address"
        '
        'TxtAddress
        '
        Me.TxtAddress.AgMandatory = True
        Me.TxtAddress.AgMasterHelp = False
        Me.TxtAddress.AgNumberLeftPlaces = 0
        Me.TxtAddress.AgNumberNegetiveAllow = False
        Me.TxtAddress.AgNumberRightPlaces = 0
        Me.TxtAddress.AgPickFromLastValue = False
        Me.TxtAddress.AgRowFilter = ""
        Me.TxtAddress.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAddress.AgSelectedValue = Nothing
        Me.TxtAddress.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAddress.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAddress.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAddress.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAddress.Location = New System.Drawing.Point(301, 68)
        Me.TxtAddress.MaxLength = 255
        Me.TxtAddress.Multiline = True
        Me.TxtAddress.Name = "TxtAddress"
        Me.TxtAddress.Size = New System.Drawing.Size(381, 58)
        Me.TxtAddress.TabIndex = 4
        '
        'TxtVehicleNo
        '
        Me.TxtVehicleNo.AgMandatory = False
        Me.TxtVehicleNo.AgMasterHelp = True
        Me.TxtVehicleNo.AgNumberLeftPlaces = 8
        Me.TxtVehicleNo.AgNumberNegetiveAllow = False
        Me.TxtVehicleNo.AgNumberRightPlaces = 2
        Me.TxtVehicleNo.AgPickFromLastValue = False
        Me.TxtVehicleNo.AgRowFilter = ""
        Me.TxtVehicleNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVehicleNo.AgSelectedValue = Nothing
        Me.TxtVehicleNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVehicleNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVehicleNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVehicleNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVehicleNo.Location = New System.Drawing.Point(559, 128)
        Me.TxtVehicleNo.MaxLength = 30
        Me.TxtVehicleNo.Name = "TxtVehicleNo"
        Me.TxtVehicleNo.Size = New System.Drawing.Size(123, 18)
        Me.TxtVehicleNo.TabIndex = 6
        '
        'LblVehicleNo
        '
        Me.LblVehicleNo.AutoSize = True
        Me.LblVehicleNo.BackColor = System.Drawing.Color.Transparent
        Me.LblVehicleNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVehicleNo.Location = New System.Drawing.Point(459, 129)
        Me.LblVehicleNo.Name = "LblVehicleNo"
        Me.LblVehicleNo.Size = New System.Drawing.Size(75, 16)
        Me.LblVehicleNo.TabIndex = 731
        Me.LblVehicleNo.Text = "Vehicle No."
        '
        'LblVisitorNameReq
        '
        Me.LblVisitorNameReq.AutoSize = True
        Me.LblVisitorNameReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblVisitorNameReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblVisitorNameReq.Location = New System.Drawing.Point(286, 54)
        Me.LblVisitorNameReq.Name = "LblVisitorNameReq"
        Me.LblVisitorNameReq.Size = New System.Drawing.Size(10, 7)
        Me.LblVisitorNameReq.TabIndex = 733
        Me.LblVisitorNameReq.Text = "Ä"
        '
        'TxtPhone
        '
        Me.TxtPhone.AgMandatory = False
        Me.TxtPhone.AgMasterHelp = True
        Me.TxtPhone.AgNumberLeftPlaces = 8
        Me.TxtPhone.AgNumberNegetiveAllow = False
        Me.TxtPhone.AgNumberRightPlaces = 2
        Me.TxtPhone.AgPickFromLastValue = False
        Me.TxtPhone.AgRowFilter = ""
        Me.TxtPhone.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPhone.AgSelectedValue = Nothing
        Me.TxtPhone.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPhone.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPhone.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPhone.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPhone.Location = New System.Drawing.Point(301, 128)
        Me.TxtPhone.MaxLength = 30
        Me.TxtPhone.Name = "TxtPhone"
        Me.TxtPhone.Size = New System.Drawing.Size(152, 18)
        Me.TxtPhone.TabIndex = 5
        '
        'LblPhone
        '
        Me.LblPhone.AutoSize = True
        Me.LblPhone.BackColor = System.Drawing.Color.Transparent
        Me.LblPhone.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPhone.Location = New System.Drawing.Point(180, 130)
        Me.LblPhone.Name = "LblPhone"
        Me.LblPhone.Size = New System.Drawing.Size(69, 16)
        Me.LblPhone.TabIndex = 735
        Me.LblPhone.Text = "Phone No."
        '
        'TxtPersonToMeet
        '
        Me.TxtPersonToMeet.AgMandatory = True
        Me.TxtPersonToMeet.AgMasterHelp = False
        Me.TxtPersonToMeet.AgNumberLeftPlaces = 0
        Me.TxtPersonToMeet.AgNumberNegetiveAllow = False
        Me.TxtPersonToMeet.AgNumberRightPlaces = 0
        Me.TxtPersonToMeet.AgPickFromLastValue = False
        Me.TxtPersonToMeet.AgRowFilter = ""
        Me.TxtPersonToMeet.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPersonToMeet.AgSelectedValue = Nothing
        Me.TxtPersonToMeet.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPersonToMeet.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPersonToMeet.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPersonToMeet.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPersonToMeet.Location = New System.Drawing.Point(301, 168)
        Me.TxtPersonToMeet.MaxLength = 50
        Me.TxtPersonToMeet.Name = "TxtPersonToMeet"
        Me.TxtPersonToMeet.Size = New System.Drawing.Size(381, 18)
        Me.TxtPersonToMeet.TabIndex = 9
        '
        'TxtPerpose
        '
        Me.TxtPerpose.AgMandatory = True
        Me.TxtPerpose.AgMasterHelp = False
        Me.TxtPerpose.AgNumberLeftPlaces = 0
        Me.TxtPerpose.AgNumberNegetiveAllow = False
        Me.TxtPerpose.AgNumberRightPlaces = 0
        Me.TxtPerpose.AgPickFromLastValue = False
        Me.TxtPerpose.AgRowFilter = ""
        Me.TxtPerpose.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPerpose.AgSelectedValue = Nothing
        Me.TxtPerpose.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPerpose.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPerpose.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPerpose.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPerpose.Location = New System.Drawing.Point(301, 188)
        Me.TxtPerpose.MaxLength = 255
        Me.TxtPerpose.Multiline = True
        Me.TxtPerpose.Name = "TxtPerpose"
        Me.TxtPerpose.Size = New System.Drawing.Size(381, 84)
        Me.TxtPerpose.TabIndex = 10
        '
        'LblPurpose
        '
        Me.LblPurpose.AutoSize = True
        Me.LblPurpose.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPurpose.Location = New System.Drawing.Point(180, 189)
        Me.LblPurpose.Name = "LblPurpose"
        Me.LblPurpose.Size = New System.Drawing.Size(56, 16)
        Me.LblPurpose.TabIndex = 747
        Me.LblPurpose.Text = "Purpose"
        '
        'LblPersonToMeetReq
        '
        Me.LblPersonToMeetReq.AutoSize = True
        Me.LblPersonToMeetReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblPersonToMeetReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblPersonToMeetReq.Location = New System.Drawing.Point(285, 176)
        Me.LblPersonToMeetReq.Name = "LblPersonToMeetReq"
        Me.LblPersonToMeetReq.Size = New System.Drawing.Size(10, 7)
        Me.LblPersonToMeetReq.TabIndex = 748
        Me.LblPersonToMeetReq.Text = "Ä"
        '
        'TxtEntryTime
        '
        Me.TxtEntryTime.AgMandatory = False
        Me.TxtEntryTime.AgMasterHelp = True
        Me.TxtEntryTime.AgNumberLeftPlaces = 8
        Me.TxtEntryTime.AgNumberNegetiveAllow = False
        Me.TxtEntryTime.AgNumberRightPlaces = 2
        Me.TxtEntryTime.AgPickFromLastValue = False
        Me.TxtEntryTime.AgRowFilter = ""
        Me.TxtEntryTime.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtEntryTime.AgSelectedValue = Nothing
        Me.TxtEntryTime.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtEntryTime.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtEntryTime.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtEntryTime.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEntryTime.Location = New System.Drawing.Point(630, 28)
        Me.TxtEntryTime.MaxLength = 20
        Me.TxtEntryTime.Name = "TxtEntryTime"
        Me.TxtEntryTime.Size = New System.Drawing.Size(52, 18)
        Me.TxtEntryTime.TabIndex = 2
        '
        'LblOutEntryBy
        '
        Me.LblOutEntryBy.AutoSize = True
        Me.LblOutEntryBy.BackColor = System.Drawing.Color.Transparent
        Me.LblOutEntryBy.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblOutEntryBy.Location = New System.Drawing.Point(42, 31)
        Me.LblOutEntryBy.Name = "LblOutEntryBy"
        Me.LblOutEntryBy.Size = New System.Drawing.Size(59, 16)
        Me.LblOutEntryBy.TabIndex = 746
        Me.LblOutEntryBy.Text = "Entry By"
        '
        'TxtOutEntryBy
        '
        Me.TxtOutEntryBy.AgMandatory = False
        Me.TxtOutEntryBy.AgMasterHelp = True
        Me.TxtOutEntryBy.AgNumberLeftPlaces = 8
        Me.TxtOutEntryBy.AgNumberNegetiveAllow = False
        Me.TxtOutEntryBy.AgNumberRightPlaces = 2
        Me.TxtOutEntryBy.AgPickFromLastValue = False
        Me.TxtOutEntryBy.AgRowFilter = ""
        Me.TxtOutEntryBy.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtOutEntryBy.AgSelectedValue = Nothing
        Me.TxtOutEntryBy.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtOutEntryBy.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtOutEntryBy.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtOutEntryBy.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtOutEntryBy.Location = New System.Drawing.Point(184, 30)
        Me.TxtOutEntryBy.MaxLength = 20
        Me.TxtOutEntryBy.Name = "TxtOutEntryBy"
        Me.TxtOutEntryBy.Size = New System.Drawing.Size(157, 18)
        Me.TxtOutEntryBy.TabIndex = 0
        '
        'TxtOutDate
        '
        Me.TxtOutDate.AgMandatory = False
        Me.TxtOutDate.AgMasterHelp = True
        Me.TxtOutDate.AgNumberLeftPlaces = 8
        Me.TxtOutDate.AgNumberNegetiveAllow = False
        Me.TxtOutDate.AgNumberRightPlaces = 2
        Me.TxtOutDate.AgPickFromLastValue = False
        Me.TxtOutDate.AgRowFilter = ""
        Me.TxtOutDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtOutDate.AgSelectedValue = Nothing
        Me.TxtOutDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtOutDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtOutDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtOutDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtOutDate.Location = New System.Drawing.Point(184, 51)
        Me.TxtOutDate.MaxLength = 20
        Me.TxtOutDate.Name = "TxtOutDate"
        Me.TxtOutDate.Size = New System.Drawing.Size(95, 18)
        Me.TxtOutDate.TabIndex = 1
        '
        'LblOutDate
        '
        Me.LblOutDate.AutoSize = True
        Me.LblOutDate.BackColor = System.Drawing.Color.Transparent
        Me.LblOutDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblOutDate.Location = New System.Drawing.Point(42, 53)
        Me.LblOutDate.Name = "LblOutDate"
        Me.LblOutDate.Size = New System.Drawing.Size(105, 16)
        Me.LblOutDate.TabIndex = 741
        Me.LblOutDate.Text = "Out Date && Time"
        '
        'TxtOutTime
        '
        Me.TxtOutTime.AgMandatory = False
        Me.TxtOutTime.AgMasterHelp = True
        Me.TxtOutTime.AgNumberLeftPlaces = 8
        Me.TxtOutTime.AgNumberNegetiveAllow = False
        Me.TxtOutTime.AgNumberRightPlaces = 2
        Me.TxtOutTime.AgPickFromLastValue = False
        Me.TxtOutTime.AgRowFilter = ""
        Me.TxtOutTime.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtOutTime.AgSelectedValue = Nothing
        Me.TxtOutTime.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtOutTime.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtOutTime.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtOutTime.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtOutTime.Location = New System.Drawing.Point(282, 51)
        Me.TxtOutTime.MaxLength = 20
        Me.TxtOutTime.Name = "TxtOutTime"
        Me.TxtOutTime.Size = New System.Drawing.Size(59, 18)
        Me.TxtOutTime.TabIndex = 2
        '
        'GrpGateOutEntry
        '
        Me.GrpGateOutEntry.Controls.Add(Me.BtnSaveGateOutEntry)
        Me.GrpGateOutEntry.Controls.Add(Me.LblOutEntryBy)
        Me.GrpGateOutEntry.Controls.Add(Me.TxtOutDate)
        Me.GrpGateOutEntry.Controls.Add(Me.LblOutDate)
        Me.GrpGateOutEntry.Controls.Add(Me.TxtOutEntryBy)
        Me.GrpGateOutEntry.Controls.Add(Me.LinkLabel5)
        Me.GrpGateOutEntry.Controls.Add(Me.TxtOutTime)
        Me.GrpGateOutEntry.Location = New System.Drawing.Point(240, 278)
        Me.GrpGateOutEntry.Name = "GrpGateOutEntry"
        Me.GrpGateOutEntry.Size = New System.Drawing.Size(383, 131)
        Me.GrpGateOutEntry.TabIndex = 807
        Me.GrpGateOutEntry.TabStop = False
        Me.GrpGateOutEntry.Tag = ""
        Me.GrpGateOutEntry.Text = "Out Detail"
        '
        'BtnSaveGateOutEntry
        '
        Me.BtnSaveGateOutEntry.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSaveGateOutEntry.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSaveGateOutEntry.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSaveGateOutEntry.Image = CType(resources.GetObject("BtnSaveGateOutEntry.Image"), System.Drawing.Image)
        Me.BtnSaveGateOutEntry.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnSaveGateOutEntry.Location = New System.Drawing.Point(145, 86)
        Me.BtnSaveGateOutEntry.Name = "BtnSaveGateOutEntry"
        Me.BtnSaveGateOutEntry.Size = New System.Drawing.Size(68, 25)
        Me.BtnSaveGateOutEntry.TabIndex = 3
        Me.BtnSaveGateOutEntry.Text = "Save"
        Me.BtnSaveGateOutEntry.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnSaveGateOutEntry.UseVisualStyleBackColor = True
        '
        'LinkLabel5
        '
        Me.LinkLabel5.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel5.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel5.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel5.LinkColor = System.Drawing.Color.White
        Me.LinkLabel5.Location = New System.Drawing.Point(6, 0)
        Me.LinkLabel5.Name = "LinkLabel5"
        Me.LinkLabel5.Size = New System.Drawing.Size(112, 21)
        Me.LinkLabel5.TabIndex = 808
        Me.LinkLabel5.TabStop = True
        Me.LinkLabel5.Text = "Out Detail"
        Me.LinkLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblPersonToMeet
        '
        Me.LblPersonToMeet.AutoSize = True
        Me.LblPersonToMeet.BackColor = System.Drawing.Color.Transparent
        Me.LblPersonToMeet.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPersonToMeet.Location = New System.Drawing.Point(180, 169)
        Me.LblPersonToMeet.Name = "LblPersonToMeet"
        Me.LblPersonToMeet.Size = New System.Drawing.Size(100, 16)
        Me.LblPersonToMeet.TabIndex = 812
        Me.LblPersonToMeet.Text = "Person To Meet"
        '
        'LblAddressReq
        '
        Me.LblAddressReq.AutoSize = True
        Me.LblAddressReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblAddressReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblAddressReq.Location = New System.Drawing.Point(285, 76)
        Me.LblAddressReq.Name = "LblAddressReq"
        Me.LblAddressReq.Size = New System.Drawing.Size(10, 7)
        Me.LblAddressReq.TabIndex = 813
        Me.LblAddressReq.Text = "Ä"
        '
        'LblManualNo
        '
        Me.LblManualNo.AutoSize = True
        Me.LblManualNo.BackColor = System.Drawing.Color.Transparent
        Me.LblManualNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualNo.Location = New System.Drawing.Point(180, 29)
        Me.LblManualNo.Name = "LblManualNo"
        Me.LblManualNo.Size = New System.Drawing.Size(70, 16)
        Me.LblManualNo.TabIndex = 815
        Me.LblManualNo.Text = "Manual No"
        '
        'TxtManualNo
        '
        Me.TxtManualNo.AgMandatory = True
        Me.TxtManualNo.AgMasterHelp = False
        Me.TxtManualNo.AgNumberLeftPlaces = 8
        Me.TxtManualNo.AgNumberNegetiveAllow = False
        Me.TxtManualNo.AgNumberRightPlaces = 2
        Me.TxtManualNo.AgPickFromLastValue = False
        Me.TxtManualNo.AgRowFilter = ""
        Me.TxtManualNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtManualNo.AgSelectedValue = Nothing
        Me.TxtManualNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtManualNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtManualNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtManualNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtManualNo.Location = New System.Drawing.Point(301, 28)
        Me.TxtManualNo.MaxLength = 20
        Me.TxtManualNo.Name = "TxtManualNo"
        Me.TxtManualNo.Size = New System.Drawing.Size(152, 18)
        Me.TxtManualNo.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(285, 196)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 816
        Me.Label1.Text = "Ä"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(286, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 7)
        Me.Label3.TabIndex = 817
        Me.Label3.Text = "Ä"
        '
        'TxtDateValue
        '
        Me.TxtDateValue.AgMandatory = True
        Me.TxtDateValue.AgMasterHelp = False
        Me.TxtDateValue.AgNumberLeftPlaces = 8
        Me.TxtDateValue.AgNumberNegetiveAllow = False
        Me.TxtDateValue.AgNumberRightPlaces = 2
        Me.TxtDateValue.AgPickFromLastValue = False
        Me.TxtDateValue.AgRowFilter = ""
        Me.TxtDateValue.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDateValue.AgSelectedValue = Nothing
        Me.TxtDateValue.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDateValue.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDateValue.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDateValue.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateValue.Location = New System.Drawing.Point(40, 76)
        Me.TxtDateValue.MaxLength = 20
        Me.TxtDateValue.Name = "TxtDateValue"
        Me.TxtDateValue.Size = New System.Drawing.Size(103, 18)
        Me.TxtDateValue.TabIndex = 820
        Me.TxtDateValue.Visible = False
        '
        'LblVisitorID
        '
        Me.LblVisitorID.AutoSize = True
        Me.LblVisitorID.BackColor = System.Drawing.Color.Transparent
        Me.LblVisitorID.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVisitorID.Location = New System.Drawing.Point(180, 149)
        Me.LblVisitorID.Name = "LblVisitorID"
        Me.LblVisitorID.Size = New System.Drawing.Size(61, 16)
        Me.LblVisitorID.TabIndex = 822
        Me.LblVisitorID.Text = "Visitor ID"
        '
        'TxtVisitorID
        '
        Me.TxtVisitorID.AgMandatory = False
        Me.TxtVisitorID.AgMasterHelp = True
        Me.TxtVisitorID.AgNumberLeftPlaces = 8
        Me.TxtVisitorID.AgNumberNegetiveAllow = False
        Me.TxtVisitorID.AgNumberRightPlaces = 2
        Me.TxtVisitorID.AgPickFromLastValue = False
        Me.TxtVisitorID.AgRowFilter = ""
        Me.TxtVisitorID.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVisitorID.AgSelectedValue = Nothing
        Me.TxtVisitorID.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVisitorID.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVisitorID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVisitorID.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVisitorID.Location = New System.Drawing.Point(301, 148)
        Me.TxtVisitorID.MaxLength = 30
        Me.TxtVisitorID.Name = "TxtVisitorID"
        Me.TxtVisitorID.Size = New System.Drawing.Size(152, 18)
        Me.TxtVisitorID.TabIndex = 7
        '
        'LblPassNo
        '
        Me.LblPassNo.AutoSize = True
        Me.LblPassNo.BackColor = System.Drawing.Color.Transparent
        Me.LblPassNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPassNo.Location = New System.Drawing.Point(459, 149)
        Me.LblPassNo.Name = "LblPassNo"
        Me.LblPassNo.Size = New System.Drawing.Size(103, 16)
        Me.LblPassNo.TabIndex = 824
        Me.LblPassNo.Text = "Visitor Pass No."
        '
        'TxtPassNo
        '
        Me.TxtPassNo.AgMandatory = False
        Me.TxtPassNo.AgMasterHelp = True
        Me.TxtPassNo.AgNumberLeftPlaces = 8
        Me.TxtPassNo.AgNumberNegetiveAllow = False
        Me.TxtPassNo.AgNumberRightPlaces = 2
        Me.TxtPassNo.AgPickFromLastValue = False
        Me.TxtPassNo.AgRowFilter = ""
        Me.TxtPassNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPassNo.AgSelectedValue = Nothing
        Me.TxtPassNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPassNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPassNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPassNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPassNo.Location = New System.Drawing.Point(559, 148)
        Me.TxtPassNo.MaxLength = 30
        Me.TxtPassNo.Name = "TxtPassNo"
        Me.TxtPassNo.Size = New System.Drawing.Size(123, 18)
        Me.TxtPassNo.TabIndex = 8
        '
        'TempVisitorsGateInOut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(859, 523)
        Me.Name = "TempVisitorsGateInOut"
        Me.Text = "Temp Visitors Gate In Out"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GBoxMoveToLog.ResumeLayout(False)
        Me.GBoxMoveToLog.PerformLayout()
        Me.GBoxApprove.ResumeLayout(False)
        Me.GBoxApprove.PerformLayout()
        Me.GBoxEntryType.ResumeLayout(False)
        Me.GBoxEntryType.PerformLayout()
        Me.GrpUP.ResumeLayout(False)
        Me.GrpUP.PerformLayout()
        Me.GBoxDivision.ResumeLayout(False)
        Me.GBoxDivision.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TP1.ResumeLayout(False)
        Me.TP1.PerformLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpGateOutEntry.ResumeLayout(False)
        Me.GrpGateOutEntry.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents TxtVisitorName As AgControls.AgTextBox
    Protected WithEvents LblVisitorName As System.Windows.Forms.Label
    Protected WithEvents TxtAddress As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents TxtVehicleNo As AgControls.AgTextBox
    Protected WithEvents LblVehicleNo As System.Windows.Forms.Label
    Protected WithEvents LblVisitorNameReq As System.Windows.Forms.Label
    Protected WithEvents TxtPhone As AgControls.AgTextBox
    Protected WithEvents LblPhone As System.Windows.Forms.Label
    Protected WithEvents TxtPersonToMeet As AgControls.AgTextBox
    Protected WithEvents TxtPerpose As AgControls.AgTextBox
    Protected WithEvents LblPurpose As System.Windows.Forms.Label
    Protected WithEvents LblPersonToMeetReq As System.Windows.Forms.Label
    Protected WithEvents TxtOutDate As AgControls.AgTextBox
    Protected WithEvents LblOutDate As System.Windows.Forms.Label
    Protected WithEvents TxtOutTime As AgControls.AgTextBox
    Protected WithEvents LblOutEntryBy As System.Windows.Forms.Label
    Protected WithEvents TxtOutEntryBy As AgControls.AgTextBox
    Protected WithEvents GrpGateOutEntry As System.Windows.Forms.GroupBox
    Protected WithEvents LinkLabel5 As System.Windows.Forms.LinkLabel
    Protected WithEvents LblPersonToMeet As System.Windows.Forms.Label
    Protected WithEvents LblAddressReq As System.Windows.Forms.Label
    Protected WithEvents LblManualNo As System.Windows.Forms.Label
    Protected WithEvents TxtManualNo As AgControls.AgTextBox
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Protected WithEvents TxtDateValue As AgControls.AgTextBox
    Protected WithEvents BtnSaveGateOutEntry As System.Windows.Forms.Button
    Protected WithEvents TxtPassNo As AgControls.AgTextBox
    Protected WithEvents LblVisitorID As System.Windows.Forms.Label
    Protected WithEvents TxtVisitorID As AgControls.AgTextBox
    Protected WithEvents LblPassNo As System.Windows.Forms.Label
    Protected WithEvents TxtEntryTime As AgControls.AgTextBox
#End Region

    Private Sub TempPurchIndent_BaseEvent_Approve_InTrans(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand) Handles Me.BaseEvent_Approve_InTrans

    End Sub

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Visitor_GateInOut"
        LogTableName = "Visitor_GateInOut_Log"
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("P.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                       " And " & AgL.PubSiteCondition("P.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "P.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = " Select P.DocID As SearchCode " &
            " From Visitor_GateInOut P " &
            " Left Join Voucher_Type Vt On P.V_Type = Vt.V_Type  " &
            " Where IfNull(IsDeleted,0) = 0  " & mCondStr & "  Order By P.V_Date Desc "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMastLog(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMastLog
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("P.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("P.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "P.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = "Select P.UID As SearchCode " &
               " From Visitor_GateInOut_Log P " &
               " Left Join Voucher_Type Vt On P.V_Type = Vt.V_Type  " &
               " Where P.EntryStatus='" & LogStatus.LogOpen & "' " & mCondStr & " Order By P.EntryDate "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        If MsgBox("Do you want All Entry ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, AgLibrary.ClsMain.PubMsgTitleInfo) = vbNo Then
            mCondStr = mCondStr & " And H.Out_EntryBy IS NULL "
        End If

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, H.V_Date AS [In_DATE], H.V_Time AS [In_TIME], H.V_No AS [Entry_No], " &
                        " H.Manual_RefNo AS [Manual_No], H.VisitorName AS [Visitor_Name], H.Address, H.Phone, H.VehicleNo AS [Vehicle_No], H.Person_To_Meet AS [Person_To_Meet],  " &
                        " H.Perpose, H.Out_EntryBy AS [OUT_Entry_By], H.Out_Date AS [OUT_Date], H.Out_Time AS [OUT_Time], H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date],  " &
                        " H.EntryType AS [Entry_Type], H.EntryStatus AS [Entry_Status], H.ApproveBy AS [Approve_By], H.ApproveDate AS [Approve_Date], H.MoveToLog AS [Move_To_Log],  " &
                        " H.MoveToLogDate AS [Move_To_Log_Date], H.Status, " &
                        " D.Div_Name AS Division,SM.Name AS [Site_Name] " &
                        " FROM  Visitor_GateInOut H  " &
                        " LEFT JOIN Division D ON D.Div_Code=H.Div_Code  " &
                        " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code  " &
                        " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type  " &
                        " Where 1=1 " & mCondStr
        AgL.PubFindQryOrdBy = "[Entry_Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindLog() Handles Me.BaseEvent_FindLog
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        If MsgBox("Do you want All Entry ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, AgLibrary.ClsMain.PubMsgTitleInfo) = vbNo Then
            mCondStr = mCondStr & " And H.Out_EntryBy IS NULL "
        End If

        AgL.PubFindQry = " SELECT H.UID AS SearchCode, H.V_Date AS [In_DATE], H.V_Time AS [In_TIME], H.V_No AS [Entry_No], " &
                " H.Manual_RefNo AS [Manual_No], H.VisitorName AS [Visitor_Name], H.Address, H.Phone, " &
                " H.VehicleNo AS [Vehicle_No], H.Person_To_Meet AS [Person_To_Meet],  " &
                " H.Perpose, H.Out_EntryBy AS [OUT_Entry_By], H.Out_Date AS [OUT_Date], H.Out_Time AS [OUT_Time], " &
                " H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date],  " &
                " H.EntryType AS [Entry_Type], H.EntryStatus AS [Entry_Status], H.ApproveBy AS [Approve_By], " &
                " H.ApproveDate AS [Approve_Date], H.MoveToLog AS [Move_To_Log],  " &
                " H.MoveToLogDate AS [Move_To_Log_Date], H.Status, " &
                " D.Div_Name AS Division,SM.Name AS [Site_Name] " &
                " FROM  Visitor_GateInOut_Log H  " &
                " LEFT JOIN Division D ON D.Div_Code=H.Div_Code  " &
                " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code  " &
                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type  " &
                " Where 1=1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry_Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Topctrl1.BringToFront()
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand) Handles Me.BaseEvent_Save_InTrans
        mQry = " UPDATE Visitor_GateInOut_Log " &
                " SET Manual_RefNo = " & AgL.Chk_Text(TxtManualNo.Text) & ", " &
                " VisitorName = " & AgL.Chk_Text(TxtVisitorName.Text) & ", " &
                " V_Time = " & AgL.Chk_Text(TxtEntryTime.Text) & ", " &
                " Address = " & AgL.Chk_Text(TxtAddress.Text) & ", " &
                " Phone = " & AgL.Chk_Text(TxtPhone.Text) & ", " &
                " VehicleNo = " & AgL.Chk_Text(TxtVehicleNo.Text) & ", " &
                " VisitorID = " & AgL.Chk_Text(TxtVisitorID.Text) & ", " &
                " PassNo = " & AgL.Chk_Text(TxtPassNo.Text) & ", " &
                " Employee = " & AgL.Chk_Text(TxtPersonToMeet.AgSelectedValue) & ", " &
                " Person_To_Meet = " & AgL.Chk_Text(TxtPersonToMeet.Text) & ", " &
                " Perpose = " & AgL.Chk_Text(TxtPerpose.Text) & " " &
                " Where UID = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DrTemp As DataRow() = Nothing
        Dim DsTemp As DataSet

        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = "Select P.* " &
                " From Visitor_GateInOut P " &
                " Where P.DocID = '" & SearchCode & "'"
        Else
            mQry = "Select P.* " &
                " From Visitor_GateInOut_Log P " &
                " Where P.UID='" & SearchCode & "'"
        End If
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then

                TxtVisitorName.Text = AgL.XNull(.Rows(0)("VisitorName"))
                TxtEntryTime.Text = AgL.XNull(.Rows(0)("V_Time"))
                TxtVehicleNo.Text = AgL.XNull(.Rows(0)("VehicleNo"))
                TxtManualNo.Text = AgL.XNull(.Rows(0)("Manual_RefNo"))
                TxtAddress.Text = AgL.XNull(.Rows(0)("Address"))
                TxtPhone.Text = AgL.XNull(.Rows(0)("Phone"))
                TxtVisitorID.Text = AgL.XNull(.Rows(0)("VisitorID"))
                TxtPassNo.Text = AgL.XNull(.Rows(0)("PassNo"))
                TxtPersonToMeet.Text = AgL.XNull(.Rows(0)("Person_To_Meet"))
                TxtPerpose.Text = AgL.XNull(.Rows(0)("Perpose"))
                TxtOutDate.Text = AgL.XNull(.Rows(0)("Out_Date"))
                TxtOutTime.Text = AgL.XNull(.Rows(0)("Out_Time"))
                TxtOutEntryBy.Text = AgL.XNull(.Rows(0)("Out_EntryBy"))

                Calculation()
            End If
        End With
        GrpGateOutEntry.Visible = True
        LinkLabel5.Visible = True
        BtnSaveGateOutEntry.Visible = True

        If TxtOutEntryBy.Text.Trim = "" Then
            BtnSaveGateOutEntry.Enabled = True
        Else
            BtnSaveGateOutEntry.Enabled = False
        End If

    End Sub

    Private Sub FrmProductionOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Topctrl1.ChangeAgGridState(Dgl1, False)
        'GrpGateOutEntry.Tag = dtup
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = " SELECT E.SubCode AS Code, SG.DispName AS Employee, IfNull(SG.IsDeleted,0) AS Isdeleted " &
                " FROM Employee E " &
                " LEFT JOIN SubGroup SG ON SG.SubCode = E.SubCode "
        TxtPersonToMeet.AgHelpDataSet(1, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation

    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If AgL.RequiredField(TxtManualNo, LblManualNo.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtVisitorName, LblVisitorName.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtAddress, "Address") Then passed = False : Exit Sub
        If AgL.RequiredField(TxtPersonToMeet, LblPersonToMeet.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtPerpose, LblPurpose.Text) Then passed = False : Exit Sub
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        ' TxtWeight.Text = 0 : TxtQty.Text = 0
        GrpGateOutEntry.Visible = False
        BtnSaveGateOutEntry.Visible = False
        LinkLabel5.Visible = False
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtVisitorName.Validating
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.name
                'Case TxtOutUser.Name
                '    Topctrl1.Focus()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Txt_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtVisitorName.Enter
        Try
            Select Case sender.name
                Case TxtVisitorName.Name

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TempGateInOut_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        mQry = " SELECT getdate ()"
        TxtEntryTime.Text = CDate(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar().ToString).TimeOfDay.ToString.Substring(0, 5)
        TxtDateValue.Text = CDate(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar().ToString).Year.ToString.Substring(2, 2) + CDate(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar().ToString).Month.ToString + CDate(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar().ToString).Date.ToString.Substring(0, 2)
        mQry = " SELECT IfNull(Max(substring(Manual_RefNo,7,4)),0) + 1  FROM Visitor_GateInOut_Log " &
                " WHERE substring(Manual_RefNo,1,6)='" & TxtDateValue.Text & "' "
        TxtManualNo.Text = TxtDateValue.Text + AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar().ToString.PadLeft(4, "0")
        TxtManualNo.Focus()
    End Sub

    Private Sub TempGateInOut_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        TxtV_Date.Enabled = False : TxtEntryTime.Enabled = False
        TxtOutDate.Enabled = False : TxtOutTime.Enabled = False : TxtOutEntryBy.Enabled = False

    End Sub

    Private Sub BtnGateOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
    End Sub

    Sub ProcFillGateInDetail(ByVal bGateInDocId As String)
        Dim DrTemp As DataRow() = Nothing
        Dim DsTemp As DataSet
        mQry = "Select P.* " &
            " From Visitor_GateInOut_Log P " &
            " Where P.UID='" & bGateInDocId & "'"

        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then

                TxtVisitorName.Text = AgL.XNull(.Rows(0)("VisitorName"))
                TxtEntryTime.Text = AgL.XNull(.Rows(0)("V_Time"))
                TxtVehicleNo.Text = AgL.XNull(.Rows(0)("VehicleNo"))
                TxtManualNo.Text = AgL.XNull(.Rows(0)("Manual_RefNo"))
                TxtAddress.Text = AgL.XNull(.Rows(0)("Address"))
                TxtPhone.Text = AgL.XNull(.Rows(0)("Phone"))
                TxtPersonToMeet.Text = AgL.XNull(.Rows(0)("Person_To_Meet"))
                TxtPerpose.Text = AgL.XNull(.Rows(0)("Perpose"))
            End If
        End With
    End Sub

    Private Sub BtnSaveGateOutEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSaveGateOutEntry.Click
        mQry = " SELECT getdate ()"
        TxtOutDate.Text = CDate(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar().ToString).Date.ToString
        TxtOutTime.Text = CDate(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar().ToString).TimeOfDay.ToString.Substring(0, 5)
        TxtOutEntryBy.Text = AgL.PubUserName
        Call ProcSaveOutDetail(mSearchCode)
        Call MoveRec()
    End Sub

    Private Sub TempGateInOut_BaseEvent_Topctrl_tbEdit() Handles Me.BaseEvent_Topctrl_tbEdit
        If TxtOutDate.Text.Trim <> "" Then
            MsgBox(" Entry can not be Change !") : Topctrl1.FButtonClick(14, True)
        Else
            ' BtnSaveGateOutEntry.PerformClick()
        End If
    End Sub

    Private Sub TempGateInOut_BaseEvent_Topctrl_tbDel() Handles Me.BaseEvent_Topctrl_tbDel
        If TxtOutDate.Text.Trim <> "" Then
            MsgBox(" Entry can not be Change !") : Topctrl1.FButtonClick(14, True)
        End If
    End Sub

    Private Sub ProcSaveOutDetail(ByVal SearchCode As String)
        mQry = " UPDATE Visitor_GateInOut_Log " &
                " SET " &
                " Out_Date = " & AgL.Chk_Text(TxtOutDate.Text) & ", " &
                " Out_Time = " & AgL.Chk_Text(TxtOutTime.Text) & ", " &
                " Out_EntryBy = " & AgL.Chk_Text(TxtOutEntryBy.Text) & " " &
                " Where UID = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " UPDATE Visitor_GateInOut " &
                " SET " &
                " Out_Date = " & AgL.Chk_Text(TxtOutDate.Text) & ", " &
                " Out_Time = " & AgL.Chk_Text(TxtOutTime.Text) & ", " &
                " Out_EntryBy = " & AgL.Chk_Text(TxtOutEntryBy.Text) & " " &
                " Where UID = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

    End Sub

End Class
