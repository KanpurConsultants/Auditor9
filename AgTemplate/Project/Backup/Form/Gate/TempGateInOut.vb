Public Class TempGateInOut
    Inherits AgTemplate.TempTransaction
    Public mQry$

    'Dim blnFlagOutdetail As Boolean
    Public Event BaseFunction_MoveRecLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer)
    Public Event BaseEvent_Save_InTransLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer, ByVal Conn As SqlClient.SqlConnection, ByVal Cmd As SqlClient.SqlCommand)

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TempGateInOut))
        Me.TxtParty = New AgControls.AgTextBox
        Me.LblParty = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.TxtVehicleNo = New AgControls.AgTextBox
        Me.LblVehicleNo = New System.Windows.Forms.Label
        Me.LblPartyReq = New System.Windows.Forms.Label
        Me.TxtVehicleType = New AgControls.AgTextBox
        Me.LblVehicleType = New System.Windows.Forms.Label
        Me.TxtItem = New AgControls.AgTextBox
        Me.LblItem = New System.Windows.Forms.Label
        Me.TxtWeight = New AgControls.AgTextBox
        Me.LblWeight = New System.Windows.Forms.Label
        Me.TxtQty = New AgControls.AgTextBox
        Me.LblQty = New System.Windows.Forms.Label
        Me.TxtTransporter = New AgControls.AgTextBox
        Me.LblTransporter = New System.Windows.Forms.Label
        Me.TxtDriverName = New AgControls.AgTextBox
        Me.LblDriverName = New System.Windows.Forms.Label
        Me.TxtEntryTime = New AgControls.AgTextBox
        Me.LblOutEntryBy = New System.Windows.Forms.Label
        Me.TxtOutEntryBy = New AgControls.AgTextBox
        Me.LblOutRemark = New System.Windows.Forms.Label
        Me.TxtOutRemark = New AgControls.AgTextBox
        Me.TxtOutDate = New AgControls.AgTextBox
        Me.LblOutDate = New System.Windows.Forms.Label
        Me.TxtOutTime = New AgControls.AgTextBox
        Me.BtnSaveGateOutEntry = New System.Windows.Forms.Button
        Me.GrpGateOutEntry = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtGatePassNo = New AgControls.AgTextBox
        Me.LblOutItem = New System.Windows.Forms.Label
        Me.TxtOutItem = New AgControls.AgTextBox
        Me.LblOutQty = New System.Windows.Forms.Label
        Me.TxtOutWeight = New AgControls.AgTextBox
        Me.LbiOutWeight = New System.Windows.Forms.Label
        Me.TxtOutQty = New AgControls.AgTextBox
        Me.LinkLabel5 = New System.Windows.Forms.LinkLabel
        Me.LblLrDate = New System.Windows.Forms.Label
        Me.TxtLrDate = New AgControls.AgTextBox
        Me.LblLrNo = New System.Windows.Forms.Label
        Me.TxtLrNo = New AgControls.AgTextBox
        Me.LblManualNo = New System.Windows.Forms.Label
        Me.TxtManualNo = New AgControls.AgTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtDateValue = New AgControls.AgTextBox
        Me.LblPartyAddress = New System.Windows.Forms.Label
        Me.TxtPartyAddress = New AgControls.AgTextBox
        Me.LblPartyMobile = New System.Windows.Forms.Label
        Me.TxtPartyMobileNo = New AgControls.AgTextBox
        Me.LblPartyCity = New System.Windows.Forms.Label
        Me.TxtPartyCity = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
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
        Me.GroupBox2.Location = New System.Drawing.Point(661, 528)
        Me.GroupBox2.Size = New System.Drawing.Size(119, 40)
        '
        'TxtStatus
        '
        Me.TxtStatus.AgSelectedValue = ""
        Me.TxtStatus.Location = New System.Drawing.Point(29, 19)
        Me.TxtStatus.Size = New System.Drawing.Size(87, 18)
        Me.TxtStatus.Tag = ""
        '
        'CmdStatus
        '
        Me.CmdStatus.Size = New System.Drawing.Size(26, 19)
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(625, 527)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(452, 527)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(237, 527)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(25, 527)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 523)
        Me.GroupBox1.Size = New System.Drawing.Size(877, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(449, 527)
        Me.GBoxDivision.Size = New System.Drawing.Size(119, 40)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Location = New System.Drawing.Point(3, 19)
        Me.TxtDivision.Size = New System.Drawing.Size(113, 18)
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
        Me.LblV_No.Location = New System.Drawing.Point(6, 112)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Visible = False
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(101, 111)
        Me.TxtV_No.Size = New System.Drawing.Size(42, 18)
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtV_No.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(525, 21)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(449, 14)
        Me.LblV_Date.Size = New System.Drawing.Size(80, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Date && Time"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(86, 96)
        Me.LblV_TypeReq.Tag = ""
        Me.LblV_TypeReq.Visible = False
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(535, 13)
        Me.TxtV_Date.Size = New System.Drawing.Size(96, 18)
        Me.TxtV_Date.TabIndex = 1
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(6, 91)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Visible = False
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(101, 90)
        Me.TxtV_Type.Size = New System.Drawing.Size(39, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        Me.TxtV_Type.Visible = False
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(82, 74)
        Me.LblSite_CodeReq.Tag = ""
        Me.LblSite_CodeReq.Visible = False
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(4, 67)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        Me.LblSite_Code.Visible = False
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(97, 68)
        Me.TxtSite_Code.Size = New System.Drawing.Size(43, 18)
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
        Me.TabControl1.Size = New System.Drawing.Size(871, 511)
        Me.TabControl1.TabIndex = 2
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.Label3)
        Me.TP1.Controls.Add(Me.TxtPassNo)
        Me.TP1.Controls.Add(Me.LblPartyMobile)
        Me.TP1.Controls.Add(Me.TxtPartyMobileNo)
        Me.TP1.Controls.Add(Me.LblPartyCity)
        Me.TP1.Controls.Add(Me.TxtPartyCity)
        Me.TP1.Controls.Add(Me.LblPartyAddress)
        Me.TP1.Controls.Add(Me.TxtPartyAddress)
        Me.TP1.Controls.Add(Me.TxtDateValue)
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.LblManualNo)
        Me.TP1.Controls.Add(Me.TxtManualNo)
        Me.TP1.Controls.Add(Me.LblLrDate)
        Me.TP1.Controls.Add(Me.TxtLrDate)
        Me.TP1.Controls.Add(Me.LblLrNo)
        Me.TP1.Controls.Add(Me.TxtLrNo)
        Me.TP1.Controls.Add(Me.LblItem)
        Me.TP1.Controls.Add(Me.GrpGateOutEntry)
        Me.TP1.Controls.Add(Me.TxtItem)
        Me.TP1.Controls.Add(Me.LblQty)
        Me.TP1.Controls.Add(Me.TxtEntryTime)
        Me.TP1.Controls.Add(Me.LblParty)
        Me.TP1.Controls.Add(Me.TxtParty)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.LblVehicleNo)
        Me.TP1.Controls.Add(Me.TxtVehicleNo)
        Me.TP1.Controls.Add(Me.LblPartyReq)
        Me.TP1.Controls.Add(Me.LblVehicleType)
        Me.TP1.Controls.Add(Me.TxtVehicleType)
        Me.TP1.Controls.Add(Me.TxtDriverName)
        Me.TP1.Controls.Add(Me.LblDriverName)
        Me.TP1.Controls.Add(Me.TxtTransporter)
        Me.TP1.Controls.Add(Me.LblTransporter)
        Me.TP1.Controls.Add(Me.TxtWeight)
        Me.TP1.Controls.Add(Me.LblWeight)
        Me.TP1.Controls.Add(Me.TxtQty)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(863, 485)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.TxtQty, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblWeight, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtWeight, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblTransporter, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtTransporter, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDriverName, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDriverName, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVehicleType, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblVehicleType, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPartyReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVehicleNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblVehicleNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtEntryTime, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblQty, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtItem, 0)
        Me.TP1.Controls.SetChildIndex(Me.GrpGateOutEntry, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblItem, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtLrNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblLrNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtLrDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblLrDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtManualNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblManualNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDateValue, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtPartyAddress, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPartyAddress, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtPartyCity, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPartyCity, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtPartyMobileNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPartyMobile, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtPassNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label3, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(859, 41)
        Me.Topctrl1.TabIndex = 1
        '
        'TxtParty
        '
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
        Me.TxtParty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtParty.Location = New System.Drawing.Point(297, 53)
        Me.TxtParty.MaxLength = 50
        Me.TxtParty.Name = "TxtParty"
        Me.TxtParty.Size = New System.Drawing.Size(381, 18)
        Me.TxtParty.TabIndex = 4
        '
        'LblParty
        '
        Me.LblParty.AutoSize = True
        Me.LblParty.BackColor = System.Drawing.Color.Transparent
        Me.LblParty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblParty.Location = New System.Drawing.Point(184, 54)
        Me.LblParty.Name = "LblParty"
        Me.LblParty.Size = New System.Drawing.Size(39, 16)
        Me.LblParty.TabIndex = 706
        Me.LblParty.Text = "Party"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(184, 235)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(60, 16)
        Me.Label30.TabIndex = 723
        Me.Label30.Text = "Remarks"
        '
        'TxtRemarks
        '
        Me.TxtRemarks.AgMandatory = False
        Me.TxtRemarks.AgMasterHelp = False
        Me.TxtRemarks.AgNumberLeftPlaces = 0
        Me.TxtRemarks.AgNumberNegetiveAllow = False
        Me.TxtRemarks.AgNumberRightPlaces = 0
        Me.TxtRemarks.AgPickFromLastValue = False
        Me.TxtRemarks.AgRowFilter = ""
        Me.TxtRemarks.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtRemarks.AgSelectedValue = Nothing
        Me.TxtRemarks.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtRemarks.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtRemarks.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemarks.Location = New System.Drawing.Point(297, 233)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(381, 68)
        Me.TxtRemarks.TabIndex = 17
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
        Me.TxtVehicleNo.Location = New System.Drawing.Point(535, 113)
        Me.TxtVehicleNo.MaxLength = 20
        Me.TxtVehicleNo.Name = "TxtVehicleNo"
        Me.TxtVehicleNo.Size = New System.Drawing.Size(143, 18)
        Me.TxtVehicleNo.TabIndex = 9
        '
        'LblVehicleNo
        '
        Me.LblVehicleNo.AutoSize = True
        Me.LblVehicleNo.BackColor = System.Drawing.Color.Transparent
        Me.LblVehicleNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVehicleNo.Location = New System.Drawing.Point(449, 114)
        Me.LblVehicleNo.Name = "LblVehicleNo"
        Me.LblVehicleNo.Size = New System.Drawing.Size(75, 16)
        Me.LblVehicleNo.TabIndex = 731
        Me.LblVehicleNo.Text = "Vehicle No."
        '
        'LblPartyReq
        '
        Me.LblPartyReq.AutoSize = True
        Me.LblPartyReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblPartyReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblPartyReq.Location = New System.Drawing.Point(281, 61)
        Me.LblPartyReq.Name = "LblPartyReq"
        Me.LblPartyReq.Size = New System.Drawing.Size(10, 7)
        Me.LblPartyReq.TabIndex = 733
        Me.LblPartyReq.Text = "Ä"
        '
        'TxtVehicleType
        '
        Me.TxtVehicleType.AgMandatory = False
        Me.TxtVehicleType.AgMasterHelp = True
        Me.TxtVehicleType.AgNumberLeftPlaces = 8
        Me.TxtVehicleType.AgNumberNegetiveAllow = False
        Me.TxtVehicleType.AgNumberRightPlaces = 2
        Me.TxtVehicleType.AgPickFromLastValue = False
        Me.TxtVehicleType.AgRowFilter = ""
        Me.TxtVehicleType.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVehicleType.AgSelectedValue = Nothing
        Me.TxtVehicleType.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVehicleType.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVehicleType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVehicleType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVehicleType.Location = New System.Drawing.Point(297, 113)
        Me.TxtVehicleType.MaxLength = 20
        Me.TxtVehicleType.Name = "TxtVehicleType"
        Me.TxtVehicleType.Size = New System.Drawing.Size(148, 18)
        Me.TxtVehicleType.TabIndex = 8
        '
        'LblVehicleType
        '
        Me.LblVehicleType.AutoSize = True
        Me.LblVehicleType.BackColor = System.Drawing.Color.Transparent
        Me.LblVehicleType.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVehicleType.Location = New System.Drawing.Point(184, 114)
        Me.LblVehicleType.Name = "LblVehicleType"
        Me.LblVehicleType.Size = New System.Drawing.Size(83, 16)
        Me.LblVehicleType.TabIndex = 735
        Me.LblVehicleType.Text = "Vehicle Type"
        '
        'TxtItem
        '
        Me.TxtItem.AgMandatory = False
        Me.TxtItem.AgMasterHelp = True
        Me.TxtItem.AgNumberLeftPlaces = 0
        Me.TxtItem.AgNumberNegetiveAllow = False
        Me.TxtItem.AgNumberRightPlaces = 0
        Me.TxtItem.AgPickFromLastValue = False
        Me.TxtItem.AgRowFilter = ""
        Me.TxtItem.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtItem.AgSelectedValue = Nothing
        Me.TxtItem.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtItem.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtItem.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtItem.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItem.Location = New System.Drawing.Point(297, 153)
        Me.TxtItem.MaxLength = 50
        Me.TxtItem.Name = "TxtItem"
        Me.TxtItem.Size = New System.Drawing.Size(381, 18)
        Me.TxtItem.TabIndex = 12
        '
        'LblItem
        '
        Me.LblItem.AutoSize = True
        Me.LblItem.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblItem.Location = New System.Drawing.Point(184, 154)
        Me.LblItem.Name = "LblItem"
        Me.LblItem.Size = New System.Drawing.Size(33, 16)
        Me.LblItem.TabIndex = 739
        Me.LblItem.Text = "Item"
        '
        'TxtWeight
        '
        Me.TxtWeight.AgMandatory = False
        Me.TxtWeight.AgMasterHelp = True
        Me.TxtWeight.AgNumberLeftPlaces = 8
        Me.TxtWeight.AgNumberNegetiveAllow = False
        Me.TxtWeight.AgNumberRightPlaces = 3
        Me.TxtWeight.AgPickFromLastValue = False
        Me.TxtWeight.AgRowFilter = ""
        Me.TxtWeight.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtWeight.AgSelectedValue = Nothing
        Me.TxtWeight.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtWeight.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtWeight.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtWeight.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtWeight.Location = New System.Drawing.Point(297, 173)
        Me.TxtWeight.MaxLength = 20
        Me.TxtWeight.Name = "TxtWeight"
        Me.TxtWeight.Size = New System.Drawing.Size(148, 18)
        Me.TxtWeight.TabIndex = 13
        Me.TxtWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblWeight
        '
        Me.LblWeight.AutoSize = True
        Me.LblWeight.BackColor = System.Drawing.Color.Transparent
        Me.LblWeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblWeight.Location = New System.Drawing.Point(184, 174)
        Me.LblWeight.Name = "LblWeight"
        Me.LblWeight.Size = New System.Drawing.Size(49, 16)
        Me.LblWeight.TabIndex = 743
        Me.LblWeight.Text = "Weight"
        '
        'TxtQty
        '
        Me.TxtQty.AgMandatory = False
        Me.TxtQty.AgMasterHelp = True
        Me.TxtQty.AgNumberLeftPlaces = 8
        Me.TxtQty.AgNumberNegetiveAllow = False
        Me.TxtQty.AgNumberRightPlaces = 2
        Me.TxtQty.AgPickFromLastValue = False
        Me.TxtQty.AgRowFilter = ""
        Me.TxtQty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtQty.AgSelectedValue = Nothing
        Me.TxtQty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtQty.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtQty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtQty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtQty.Location = New System.Drawing.Point(535, 173)
        Me.TxtQty.MaxLength = 20
        Me.TxtQty.Name = "TxtQty"
        Me.TxtQty.Size = New System.Drawing.Size(143, 18)
        Me.TxtQty.TabIndex = 14
        Me.TxtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblQty
        '
        Me.LblQty.AutoSize = True
        Me.LblQty.BackColor = System.Drawing.Color.Transparent
        Me.LblQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblQty.Location = New System.Drawing.Point(449, 173)
        Me.LblQty.Name = "LblQty"
        Me.LblQty.Size = New System.Drawing.Size(33, 16)
        Me.LblQty.TabIndex = 741
        Me.LblQty.Text = "Qty."
        '
        'TxtTransporter
        '
        Me.TxtTransporter.AgMandatory = False
        Me.TxtTransporter.AgMasterHelp = False
        Me.TxtTransporter.AgNumberLeftPlaces = 0
        Me.TxtTransporter.AgNumberNegetiveAllow = False
        Me.TxtTransporter.AgNumberRightPlaces = 0
        Me.TxtTransporter.AgPickFromLastValue = False
        Me.TxtTransporter.AgRowFilter = ""
        Me.TxtTransporter.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTransporter.AgSelectedValue = Nothing
        Me.TxtTransporter.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTransporter.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtTransporter.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTransporter.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTransporter.Location = New System.Drawing.Point(297, 193)
        Me.TxtTransporter.MaxLength = 10
        Me.TxtTransporter.Name = "TxtTransporter"
        Me.TxtTransporter.Size = New System.Drawing.Size(381, 18)
        Me.TxtTransporter.TabIndex = 15
        '
        'LblTransporter
        '
        Me.LblTransporter.AutoSize = True
        Me.LblTransporter.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTransporter.Location = New System.Drawing.Point(184, 194)
        Me.LblTransporter.Name = "LblTransporter"
        Me.LblTransporter.Size = New System.Drawing.Size(73, 16)
        Me.LblTransporter.TabIndex = 745
        Me.LblTransporter.Text = "Transporter"
        '
        'TxtDriverName
        '
        Me.TxtDriverName.AgMandatory = False
        Me.TxtDriverName.AgMasterHelp = False
        Me.TxtDriverName.AgNumberLeftPlaces = 0
        Me.TxtDriverName.AgNumberNegetiveAllow = False
        Me.TxtDriverName.AgNumberRightPlaces = 0
        Me.TxtDriverName.AgPickFromLastValue = False
        Me.TxtDriverName.AgRowFilter = ""
        Me.TxtDriverName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDriverName.AgSelectedValue = Nothing
        Me.TxtDriverName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDriverName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDriverName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDriverName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDriverName.Location = New System.Drawing.Point(297, 213)
        Me.TxtDriverName.MaxLength = 50
        Me.TxtDriverName.Name = "TxtDriverName"
        Me.TxtDriverName.Size = New System.Drawing.Size(381, 18)
        Me.TxtDriverName.TabIndex = 16
        '
        'LblDriverName
        '
        Me.LblDriverName.AutoSize = True
        Me.LblDriverName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDriverName.Location = New System.Drawing.Point(184, 214)
        Me.LblDriverName.Name = "LblDriverName"
        Me.LblDriverName.Size = New System.Drawing.Size(78, 16)
        Me.LblDriverName.TabIndex = 747
        Me.LblDriverName.Text = "Driver Name"
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
        Me.TxtEntryTime.Location = New System.Drawing.Point(634, 13)
        Me.TxtEntryTime.MaxLength = 20
        Me.TxtEntryTime.Name = "TxtEntryTime"
        Me.TxtEntryTime.Size = New System.Drawing.Size(44, 18)
        Me.TxtEntryTime.TabIndex = 2
        '
        'LblOutEntryBy
        '
        Me.LblOutEntryBy.AutoSize = True
        Me.LblOutEntryBy.BackColor = System.Drawing.Color.Transparent
        Me.LblOutEntryBy.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblOutEntryBy.Location = New System.Drawing.Point(81, 107)
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
        Me.TxtOutEntryBy.Location = New System.Drawing.Point(212, 106)
        Me.TxtOutEntryBy.MaxLength = 20
        Me.TxtOutEntryBy.Name = "TxtOutEntryBy"
        Me.TxtOutEntryBy.Size = New System.Drawing.Size(207, 18)
        Me.TxtOutEntryBy.TabIndex = 2
        '
        'LblOutRemark
        '
        Me.LblOutRemark.AutoSize = True
        Me.LblOutRemark.BackColor = System.Drawing.Color.Transparent
        Me.LblOutRemark.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblOutRemark.Location = New System.Drawing.Point(27, 137)
        Me.LblOutRemark.Name = "LblOutRemark"
        Me.LblOutRemark.Size = New System.Drawing.Size(78, 16)
        Me.LblOutRemark.TabIndex = 744
        Me.LblOutRemark.Text = "Out Remark"
        Me.LblOutRemark.Visible = False
        '
        'TxtOutRemark
        '
        Me.TxtOutRemark.AgMandatory = False
        Me.TxtOutRemark.AgMasterHelp = True
        Me.TxtOutRemark.AgNumberLeftPlaces = 8
        Me.TxtOutRemark.AgNumberNegetiveAllow = False
        Me.TxtOutRemark.AgNumberRightPlaces = 2
        Me.TxtOutRemark.AgPickFromLastValue = False
        Me.TxtOutRemark.AgRowFilter = ""
        Me.TxtOutRemark.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtOutRemark.AgSelectedValue = Nothing
        Me.TxtOutRemark.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtOutRemark.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtOutRemark.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtOutRemark.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtOutRemark.Location = New System.Drawing.Point(111, 136)
        Me.TxtOutRemark.MaxLength = 255
        Me.TxtOutRemark.Multiline = True
        Me.TxtOutRemark.Name = "TxtOutRemark"
        Me.TxtOutRemark.Size = New System.Drawing.Size(32, 17)
        Me.TxtOutRemark.TabIndex = 743
        Me.TxtOutRemark.Visible = False
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
        Me.TxtOutDate.Location = New System.Drawing.Point(212, 86)
        Me.TxtOutDate.MaxLength = 20
        Me.TxtOutDate.Name = "TxtOutDate"
        Me.TxtOutDate.Size = New System.Drawing.Size(95, 18)
        Me.TxtOutDate.TabIndex = 0
        '
        'LblOutDate
        '
        Me.LblOutDate.AutoSize = True
        Me.LblOutDate.BackColor = System.Drawing.Color.Transparent
        Me.LblOutDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblOutDate.Location = New System.Drawing.Point(81, 89)
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
        Me.TxtOutTime.Location = New System.Drawing.Point(310, 86)
        Me.TxtOutTime.MaxLength = 20
        Me.TxtOutTime.Name = "TxtOutTime"
        Me.TxtOutTime.Size = New System.Drawing.Size(109, 18)
        Me.TxtOutTime.TabIndex = 1
        '
        'BtnSaveGateOutEntry
        '
        Me.BtnSaveGateOutEntry.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSaveGateOutEntry.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSaveGateOutEntry.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSaveGateOutEntry.Image = CType(resources.GetObject("BtnSaveGateOutEntry.Image"), System.Drawing.Image)
        Me.BtnSaveGateOutEntry.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnSaveGateOutEntry.Location = New System.Drawing.Point(189, 136)
        Me.BtnSaveGateOutEntry.Name = "BtnSaveGateOutEntry"
        Me.BtnSaveGateOutEntry.Size = New System.Drawing.Size(68, 25)
        Me.BtnSaveGateOutEntry.TabIndex = 806
        Me.BtnSaveGateOutEntry.Text = "Save"
        Me.BtnSaveGateOutEntry.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnSaveGateOutEntry.UseVisualStyleBackColor = True
        '
        'GrpGateOutEntry
        '
        Me.GrpGateOutEntry.Controls.Add(Me.Label4)
        Me.GrpGateOutEntry.Controls.Add(Me.TxtGatePassNo)
        Me.GrpGateOutEntry.Controls.Add(Me.LblOutItem)
        Me.GrpGateOutEntry.Controls.Add(Me.TxtOutItem)
        Me.GrpGateOutEntry.Controls.Add(Me.LblOutQty)
        Me.GrpGateOutEntry.Controls.Add(Me.TxtOutWeight)
        Me.GrpGateOutEntry.Controls.Add(Me.LbiOutWeight)
        Me.GrpGateOutEntry.Controls.Add(Me.TxtOutQty)
        Me.GrpGateOutEntry.Controls.Add(Me.LblOutRemark)
        Me.GrpGateOutEntry.Controls.Add(Me.BtnSaveGateOutEntry)
        Me.GrpGateOutEntry.Controls.Add(Me.LblOutEntryBy)
        Me.GrpGateOutEntry.Controls.Add(Me.TxtOutDate)
        Me.GrpGateOutEntry.Controls.Add(Me.LblOutDate)
        Me.GrpGateOutEntry.Controls.Add(Me.TxtOutRemark)
        Me.GrpGateOutEntry.Controls.Add(Me.TxtOutEntryBy)
        Me.GrpGateOutEntry.Controls.Add(Me.LinkLabel5)
        Me.GrpGateOutEntry.Controls.Add(Me.TxtOutTime)
        Me.GrpGateOutEntry.Location = New System.Drawing.Point(208, 304)
        Me.GrpGateOutEntry.Name = "GrpGateOutEntry"
        Me.GrpGateOutEntry.Size = New System.Drawing.Size(450, 171)
        Me.GrpGateOutEntry.TabIndex = 807
        Me.GrpGateOutEntry.TabStop = False
        Me.GrpGateOutEntry.Tag = "UP"
        Me.GrpGateOutEntry.Text = "Out Detail"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(81, 47)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 16)
        Me.Label4.TabIndex = 816
        Me.Label4.Text = "Gate Pass No"
        '
        'TxtGatePassNo
        '
        Me.TxtGatePassNo.AgMandatory = False
        Me.TxtGatePassNo.AgMasterHelp = True
        Me.TxtGatePassNo.AgNumberLeftPlaces = 0
        Me.TxtGatePassNo.AgNumberNegetiveAllow = False
        Me.TxtGatePassNo.AgNumberRightPlaces = 0
        Me.TxtGatePassNo.AgPickFromLastValue = False
        Me.TxtGatePassNo.AgRowFilter = ""
        Me.TxtGatePassNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtGatePassNo.AgSelectedValue = Nothing
        Me.TxtGatePassNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtGatePassNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtGatePassNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtGatePassNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGatePassNo.Location = New System.Drawing.Point(212, 46)
        Me.TxtGatePassNo.MaxLength = 20
        Me.TxtGatePassNo.Name = "TxtGatePassNo"
        Me.TxtGatePassNo.Size = New System.Drawing.Size(207, 18)
        Me.TxtGatePassNo.TabIndex = 815
        '
        'LblOutItem
        '
        Me.LblOutItem.AutoSize = True
        Me.LblOutItem.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblOutItem.Location = New System.Drawing.Point(81, 27)
        Me.LblOutItem.Name = "LblOutItem"
        Me.LblOutItem.Size = New System.Drawing.Size(33, 16)
        Me.LblOutItem.TabIndex = 812
        Me.LblOutItem.Text = "Item"
        '
        'TxtOutItem
        '
        Me.TxtOutItem.AgMandatory = False
        Me.TxtOutItem.AgMasterHelp = True
        Me.TxtOutItem.AgNumberLeftPlaces = 0
        Me.TxtOutItem.AgNumberNegetiveAllow = False
        Me.TxtOutItem.AgNumberRightPlaces = 0
        Me.TxtOutItem.AgPickFromLastValue = False
        Me.TxtOutItem.AgRowFilter = ""
        Me.TxtOutItem.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtOutItem.AgSelectedValue = Nothing
        Me.TxtOutItem.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtOutItem.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtOutItem.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtOutItem.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtOutItem.Location = New System.Drawing.Point(212, 26)
        Me.TxtOutItem.MaxLength = 50
        Me.TxtOutItem.Name = "TxtOutItem"
        Me.TxtOutItem.Size = New System.Drawing.Size(207, 18)
        Me.TxtOutItem.TabIndex = 3
        '
        'LblOutQty
        '
        Me.LblOutQty.AutoSize = True
        Me.LblOutQty.BackColor = System.Drawing.Color.Transparent
        Me.LblOutQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblOutQty.Location = New System.Drawing.Point(296, 67)
        Me.LblOutQty.Name = "LblOutQty"
        Me.LblOutQty.Size = New System.Drawing.Size(33, 16)
        Me.LblOutQty.TabIndex = 5
        Me.LblOutQty.Text = "Qty."
        '
        'TxtOutWeight
        '
        Me.TxtOutWeight.AgMandatory = False
        Me.TxtOutWeight.AgMasterHelp = True
        Me.TxtOutWeight.AgNumberLeftPlaces = 8
        Me.TxtOutWeight.AgNumberNegetiveAllow = False
        Me.TxtOutWeight.AgNumberRightPlaces = 3
        Me.TxtOutWeight.AgPickFromLastValue = False
        Me.TxtOutWeight.AgRowFilter = ""
        Me.TxtOutWeight.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtOutWeight.AgSelectedValue = Nothing
        Me.TxtOutWeight.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtOutWeight.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtOutWeight.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtOutWeight.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtOutWeight.Location = New System.Drawing.Point(212, 66)
        Me.TxtOutWeight.MaxLength = 20
        Me.TxtOutWeight.Name = "TxtOutWeight"
        Me.TxtOutWeight.Size = New System.Drawing.Size(78, 18)
        Me.TxtOutWeight.TabIndex = 4
        Me.TxtOutWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LbiOutWeight
        '
        Me.LbiOutWeight.AutoSize = True
        Me.LbiOutWeight.BackColor = System.Drawing.Color.Transparent
        Me.LbiOutWeight.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LbiOutWeight.Location = New System.Drawing.Point(81, 67)
        Me.LbiOutWeight.Name = "LbiOutWeight"
        Me.LbiOutWeight.Size = New System.Drawing.Size(49, 16)
        Me.LbiOutWeight.TabIndex = 814
        Me.LbiOutWeight.Text = "Weight"
        '
        'TxtOutQty
        '
        Me.TxtOutQty.AgMandatory = False
        Me.TxtOutQty.AgMasterHelp = True
        Me.TxtOutQty.AgNumberLeftPlaces = 8
        Me.TxtOutQty.AgNumberNegetiveAllow = False
        Me.TxtOutQty.AgNumberRightPlaces = 2
        Me.TxtOutQty.AgPickFromLastValue = False
        Me.TxtOutQty.AgRowFilter = ""
        Me.TxtOutQty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtOutQty.AgSelectedValue = Nothing
        Me.TxtOutQty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtOutQty.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtOutQty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtOutQty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtOutQty.Location = New System.Drawing.Point(335, 66)
        Me.TxtOutQty.MaxLength = 20
        Me.TxtOutQty.Name = "TxtOutQty"
        Me.TxtOutQty.Size = New System.Drawing.Size(84, 18)
        Me.TxtOutQty.TabIndex = 811
        Me.TxtOutQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LinkLabel5
        '
        Me.LinkLabel5.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel5.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel5.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel5.LinkColor = System.Drawing.Color.White
        Me.LinkLabel5.Location = New System.Drawing.Point(6, 1)
        Me.LinkLabel5.Name = "LinkLabel5"
        Me.LinkLabel5.Size = New System.Drawing.Size(112, 21)
        Me.LinkLabel5.TabIndex = 808
        Me.LinkLabel5.TabStop = True
        Me.LinkLabel5.Text = "Out Detail"
        Me.LinkLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblLrDate
        '
        Me.LblLrDate.AutoSize = True
        Me.LblLrDate.BackColor = System.Drawing.Color.Transparent
        Me.LblLrDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblLrDate.Location = New System.Drawing.Point(449, 134)
        Me.LblLrDate.Name = "LblLrDate"
        Me.LblLrDate.Size = New System.Drawing.Size(67, 16)
        Me.LblLrDate.TabIndex = 811
        Me.LblLrDate.Text = "L. R. Date"
        '
        'TxtLrDate
        '
        Me.TxtLrDate.AgMandatory = False
        Me.TxtLrDate.AgMasterHelp = True
        Me.TxtLrDate.AgNumberLeftPlaces = 8
        Me.TxtLrDate.AgNumberNegetiveAllow = False
        Me.TxtLrDate.AgNumberRightPlaces = 2
        Me.TxtLrDate.AgPickFromLastValue = False
        Me.TxtLrDate.AgRowFilter = ""
        Me.TxtLrDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtLrDate.AgSelectedValue = Nothing
        Me.TxtLrDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtLrDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtLrDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtLrDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLrDate.Location = New System.Drawing.Point(535, 133)
        Me.TxtLrDate.MaxLength = 20
        Me.TxtLrDate.Name = "TxtLrDate"
        Me.TxtLrDate.Size = New System.Drawing.Size(143, 18)
        Me.TxtLrDate.TabIndex = 11
        '
        'LblLrNo
        '
        Me.LblLrNo.AutoSize = True
        Me.LblLrNo.BackColor = System.Drawing.Color.Transparent
        Me.LblLrNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblLrNo.Location = New System.Drawing.Point(184, 134)
        Me.LblLrNo.Name = "LblLrNo"
        Me.LblLrNo.Size = New System.Drawing.Size(60, 16)
        Me.LblLrNo.TabIndex = 812
        Me.LblLrNo.Text = "L. R. No."
        '
        'TxtLrNo
        '
        Me.TxtLrNo.AgMandatory = False
        Me.TxtLrNo.AgMasterHelp = True
        Me.TxtLrNo.AgNumberLeftPlaces = 8
        Me.TxtLrNo.AgNumberNegetiveAllow = False
        Me.TxtLrNo.AgNumberRightPlaces = 2
        Me.TxtLrNo.AgPickFromLastValue = False
        Me.TxtLrNo.AgRowFilter = ""
        Me.TxtLrNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtLrNo.AgSelectedValue = Nothing
        Me.TxtLrNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtLrNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtLrNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtLrNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLrNo.Location = New System.Drawing.Point(297, 133)
        Me.TxtLrNo.MaxLength = 20
        Me.TxtLrNo.Name = "TxtLrNo"
        Me.TxtLrNo.Size = New System.Drawing.Size(148, 18)
        Me.TxtLrNo.TabIndex = 10
        '
        'LblManualNo
        '
        Me.LblManualNo.AutoSize = True
        Me.LblManualNo.BackColor = System.Drawing.Color.Transparent
        Me.LblManualNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualNo.Location = New System.Drawing.Point(184, 14)
        Me.LblManualNo.Name = "LblManualNo"
        Me.LblManualNo.Size = New System.Drawing.Size(63, 16)
        Me.LblManualNo.TabIndex = 817
        Me.LblManualNo.Text = "Entry No."
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
        Me.TxtManualNo.Location = New System.Drawing.Point(297, 13)
        Me.TxtManualNo.MaxLength = 20
        Me.TxtManualNo.Name = "TxtManualNo"
        Me.TxtManualNo.Size = New System.Drawing.Size(148, 18)
        Me.TxtManualNo.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(281, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 818
        Me.Label1.Text = "Ä"
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
        Me.TxtDateValue.Location = New System.Drawing.Point(10, 131)
        Me.TxtDateValue.MaxLength = 20
        Me.TxtDateValue.Name = "TxtDateValue"
        Me.TxtDateValue.Size = New System.Drawing.Size(103, 18)
        Me.TxtDateValue.TabIndex = 819
        Me.TxtDateValue.Visible = False
        '
        'LblPartyAddress
        '
        Me.LblPartyAddress.AutoSize = True
        Me.LblPartyAddress.BackColor = System.Drawing.Color.Transparent
        Me.LblPartyAddress.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPartyAddress.Location = New System.Drawing.Point(184, 74)
        Me.LblPartyAddress.Name = "LblPartyAddress"
        Me.LblPartyAddress.Size = New System.Drawing.Size(56, 16)
        Me.LblPartyAddress.TabIndex = 821
        Me.LblPartyAddress.Text = "Address"
        '
        'TxtPartyAddress
        '
        Me.TxtPartyAddress.AgMandatory = True
        Me.TxtPartyAddress.AgMasterHelp = False
        Me.TxtPartyAddress.AgNumberLeftPlaces = 8
        Me.TxtPartyAddress.AgNumberNegetiveAllow = False
        Me.TxtPartyAddress.AgNumberRightPlaces = 2
        Me.TxtPartyAddress.AgPickFromLastValue = False
        Me.TxtPartyAddress.AgRowFilter = ""
        Me.TxtPartyAddress.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPartyAddress.AgSelectedValue = Nothing
        Me.TxtPartyAddress.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPartyAddress.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPartyAddress.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPartyAddress.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPartyAddress.Location = New System.Drawing.Point(297, 73)
        Me.TxtPartyAddress.MaxLength = 50
        Me.TxtPartyAddress.Name = "TxtPartyAddress"
        Me.TxtPartyAddress.Size = New System.Drawing.Size(381, 18)
        Me.TxtPartyAddress.TabIndex = 5
        '
        'LblPartyMobile
        '
        Me.LblPartyMobile.AutoSize = True
        Me.LblPartyMobile.BackColor = System.Drawing.Color.Transparent
        Me.LblPartyMobile.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPartyMobile.Location = New System.Drawing.Point(449, 94)
        Me.LblPartyMobile.Name = "LblPartyMobile"
        Me.LblPartyMobile.Size = New System.Drawing.Size(70, 16)
        Me.LblPartyMobile.TabIndex = 825
        Me.LblPartyMobile.Text = "Mobile No."
        '
        'TxtPartyMobileNo
        '
        Me.TxtPartyMobileNo.AgMandatory = False
        Me.TxtPartyMobileNo.AgMasterHelp = True
        Me.TxtPartyMobileNo.AgNumberLeftPlaces = 8
        Me.TxtPartyMobileNo.AgNumberNegetiveAllow = False
        Me.TxtPartyMobileNo.AgNumberRightPlaces = 2
        Me.TxtPartyMobileNo.AgPickFromLastValue = False
        Me.TxtPartyMobileNo.AgRowFilter = ""
        Me.TxtPartyMobileNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPartyMobileNo.AgSelectedValue = Nothing
        Me.TxtPartyMobileNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPartyMobileNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPartyMobileNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPartyMobileNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPartyMobileNo.Location = New System.Drawing.Point(535, 93)
        Me.TxtPartyMobileNo.MaxLength = 20
        Me.TxtPartyMobileNo.Name = "TxtPartyMobileNo"
        Me.TxtPartyMobileNo.Size = New System.Drawing.Size(143, 18)
        Me.TxtPartyMobileNo.TabIndex = 7
        '
        'LblPartyCity
        '
        Me.LblPartyCity.AutoSize = True
        Me.LblPartyCity.BackColor = System.Drawing.Color.Transparent
        Me.LblPartyCity.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPartyCity.Location = New System.Drawing.Point(184, 94)
        Me.LblPartyCity.Name = "LblPartyCity"
        Me.LblPartyCity.Size = New System.Drawing.Size(31, 16)
        Me.LblPartyCity.TabIndex = 826
        Me.LblPartyCity.Text = "City"
        '
        'TxtPartyCity
        '
        Me.TxtPartyCity.AgMandatory = False
        Me.TxtPartyCity.AgMasterHelp = True
        Me.TxtPartyCity.AgNumberLeftPlaces = 8
        Me.TxtPartyCity.AgNumberNegetiveAllow = False
        Me.TxtPartyCity.AgNumberRightPlaces = 2
        Me.TxtPartyCity.AgPickFromLastValue = False
        Me.TxtPartyCity.AgRowFilter = ""
        Me.TxtPartyCity.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPartyCity.AgSelectedValue = Nothing
        Me.TxtPartyCity.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPartyCity.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPartyCity.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPartyCity.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPartyCity.Location = New System.Drawing.Point(297, 93)
        Me.TxtPartyCity.MaxLength = 20
        Me.TxtPartyCity.Name = "TxtPartyCity"
        Me.TxtPartyCity.Size = New System.Drawing.Size(148, 18)
        Me.TxtPartyCity.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(184, 34)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(103, 16)
        Me.Label3.TabIndex = 828
        Me.Label3.Text = "Visitor Pass No."
        '
        'TxtPassNo
        '
        Me.TxtPassNo.AgMandatory = True
        Me.TxtPassNo.AgMasterHelp = False
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
        Me.TxtPassNo.Location = New System.Drawing.Point(297, 33)
        Me.TxtPassNo.MaxLength = 20
        Me.TxtPassNo.Name = "TxtPassNo"
        Me.TxtPassNo.Size = New System.Drawing.Size(148, 18)
        Me.TxtPassNo.TabIndex = 3
        '
        'TempGateInOut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(859, 568)
        Me.Name = "TempGateInOut"
        Me.Text = "Temp Gate In Out"
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
    Protected WithEvents TxtParty As AgControls.AgTextBox
    Protected WithEvents LblParty As System.Windows.Forms.Label
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents TxtVehicleNo As AgControls.AgTextBox
    Protected WithEvents LblVehicleNo As System.Windows.Forms.Label
    Protected WithEvents LblPartyReq As System.Windows.Forms.Label
    Protected WithEvents TxtVehicleType As AgControls.AgTextBox
    Protected WithEvents LblVehicleType As System.Windows.Forms.Label
    Protected WithEvents TxtItem As AgControls.AgTextBox
    Protected WithEvents LblItem As System.Windows.Forms.Label
    Protected WithEvents TxtWeight As AgControls.AgTextBox
    Protected WithEvents LblWeight As System.Windows.Forms.Label
    Protected WithEvents TxtQty As AgControls.AgTextBox
    Protected WithEvents LblQty As System.Windows.Forms.Label
    Protected WithEvents TxtDriverName As AgControls.AgTextBox
    Protected WithEvents LblDriverName As System.Windows.Forms.Label
    Protected WithEvents TxtTransporter As AgControls.AgTextBox
    Protected WithEvents LblTransporter As System.Windows.Forms.Label
    Protected WithEvents TxtOutDate As AgControls.AgTextBox
    Protected WithEvents LblOutDate As System.Windows.Forms.Label
    Protected WithEvents TxtOutTime As AgControls.AgTextBox
    Protected WithEvents LblOutRemark As System.Windows.Forms.Label
    Protected WithEvents TxtOutRemark As AgControls.AgTextBox
    Protected WithEvents LblOutEntryBy As System.Windows.Forms.Label
    Protected WithEvents TxtOutEntryBy As AgControls.AgTextBox
    Protected WithEvents BtnSaveGateOutEntry As System.Windows.Forms.Button
    Protected WithEvents GrpGateOutEntry As System.Windows.Forms.GroupBox
    Protected WithEvents LinkLabel5 As System.Windows.Forms.LinkLabel
    Protected WithEvents LblLrDate As System.Windows.Forms.Label
    Protected WithEvents TxtLrDate As AgControls.AgTextBox
    Protected WithEvents LblLrNo As System.Windows.Forms.Label
    Protected WithEvents TxtLrNo As AgControls.AgTextBox
    Protected WithEvents LblManualNo As System.Windows.Forms.Label
    Protected WithEvents TxtManualNo As AgControls.AgTextBox
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents TxtDateValue As AgControls.AgTextBox
    Protected WithEvents LblPartyMobile As System.Windows.Forms.Label
    Protected WithEvents TxtPartyMobileNo As AgControls.AgTextBox
    Protected WithEvents LblPartyCity As System.Windows.Forms.Label
    Protected WithEvents TxtPartyCity As AgControls.AgTextBox
    Protected WithEvents LblPartyAddress As System.Windows.Forms.Label
    Protected WithEvents TxtPartyAddress As AgControls.AgTextBox
    Protected WithEvents LblOutItem As System.Windows.Forms.Label
    Protected WithEvents TxtOutItem As AgControls.AgTextBox
    Protected WithEvents LblOutQty As System.Windows.Forms.Label
    Protected WithEvents TxtOutWeight As AgControls.AgTextBox
    Protected WithEvents LbiOutWeight As System.Windows.Forms.Label
    Protected WithEvents TxtOutQty As AgControls.AgTextBox
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Protected WithEvents TxtPassNo As AgControls.AgTextBox
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents TxtGatePassNo As AgControls.AgTextBox
    Protected WithEvents TxtEntryTime As AgControls.AgTextBox
#End Region

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "GateInOut"
        LogTableName = "GateInOut_Log"
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("P.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                       " And " & AgL.PubSiteCondition("P.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "P.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = " Select P.DocID As SearchCode " & _
            " From GateInOut P " & _
            " Left Join Voucher_Type Vt On P.V_Type = Vt.V_Type  " & _
            " Where IsNull(IsDeleted,0) = 0  " & mCondStr & "  Order By P.V_Date Desc "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMastLog(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMastLog
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("P.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("P.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "P.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = "Select P.UID As SearchCode " & _
               " From GateInOut_Log P " & _
               " Left Join Voucher_Type Vt On P.V_Type = Vt.V_Type  " & _
               " Where P.EntryStatus='" & LogStatus.LogOpen & "' " & mCondStr & " Order By P.EntryDate"

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        If MsgBox("Do you want All Entry ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, AgLibrary.ClsMain.PubMsgTitleInfo) = vbNo Then
            mCondStr = mCondStr & " And H.Out_EntryBy IS NULL "
        End If

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, H.V_Date AS [IN date], H.V_Time AS [IN_Time], H.V_No AS [Entry_No], " & _
                        " SG.DispName AS [Party_Name],H.PassNo , H.VehicleType AS [Vehicle_Type], H.VehicleNo AS [Vehicle_No], H.Item, H.Weight, H.Qty, H.Driver, H.Remarks,  " & _
                        " H.Close_Date AS [OUT_Date], H.Close_Time AS [OUT_Time], H.Close_Remarks AS [OUT_Remarks], " & _
                        " H.Close_EntryBy AS [OUT_EntryBy],  " & _
                        " H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date], H.EntryType AS [Entry_Type], " & _
                        " H.EntryStatus AS [Entry_Status],  " & _
                        " H.ApproveBy AS [Approve_By], H.ApproveDate AS [Approve_Date], H.MoveToLog AS [Move_To_Log], " & _
                        " H.MoveToLogDate AS [Move_To_Log_Date],  " & _
                        " H.Status, H.LrNo AS [LR_No], H.LrDate AS [LR_Date], H.Manual_RefNo AS [Manual_No], " & _
                        " D.Div_Name AS Division, SM.Name AS [Site_Name] " & _
                        " FROM  GateInOut H " & _
                        " LEFT JOIN Division D ON D.Div_Code=H.Div_Code  " & _
                        " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code  " & _
                        " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                        " LEFT JOIN SubGroup SG ON SG.SubCode = H.SubCode  " & _
                        " Where 1=1 " & mCondStr
        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindLog() Handles Me.BaseEvent_FindLog
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        If MsgBox("Do you want All Entry ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, AgLibrary.ClsMain.PubMsgTitleInfo) = vbNo Then
            mCondStr = mCondStr & " And H.Close_EntryBy IS NULL "
        End If

        AgL.PubFindQry = " SELECT H.UID AS SearchCode, H.V_Date AS [IN date], H.V_Time AS [IN_Time], H.V_No AS [Entry_No], " & _
                        " SG.DispName AS [Party_Name],H.PassNo , H.VehicleType AS [Vehicle_Type], H.VehicleNo AS [Vehicle_No], H.Item, H.Weight, H.Qty, H.Driver, H.Remarks,  " & _
                        " H.Close_Date AS [OUT_Date], H.Close_Time AS [OUT_Time], H.Close_Remarks AS [OUT_Remarks], " & _
                        " H.Close_EntryBy AS [OUT_EntryBy],  " & _
                        " H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date], H.EntryType AS [Entry_Type], " & _
                        " H.EntryStatus AS [Entry_Status],  " & _
                        " H.ApproveBy AS [Approve_By], H.ApproveDate AS [Approve_Date], H.MoveToLog AS [Move_To_Log], " & _
                        " H.MoveToLogDate AS [Move_To_Log_Date],  " & _
                        " H.Status, H.LrNo AS [LR_No], H.LrDate AS [LR_Date], H.Manual_RefNo AS [Manual_No], " & _
                        " D.Div_Name AS Division, SM.Name AS [Site_Name] " & _
                        " FROM  GateInOut_Log H " & _
                        " LEFT JOIN Division D ON D.Div_Code=H.Div_Code  " & _
                        " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code  " & _
                        " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                        " LEFT JOIN SubGroup SG ON SG.SubCode = H.SubCode  " & _
                        " Where H.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'" & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Topctrl1.BringToFront()
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        mQry = " UPDATE GateInOut_Log " & _
                " SET	SubCode = " & AgL.Chk_Text(TxtParty.AgSelectedValue) & ", " & _
                " PassNo = " & AgL.Chk_Text(TxtPassNo.Text) & ", " & _
                " Manual_RefNo = " & AgL.Chk_Text(TxtManualNo.Text) & ", " & _
                " VehicleType = " & AgL.Chk_Text(TxtVehicleType.Text) & ", " & _
                " V_Time = " & AgL.Chk_Text(TxtEntryTime.Text) & ", " & _
                " VehicleNo = " & AgL.Chk_Text(TxtVehicleNo.Text) & ", " & _
                " LrNo = " & AgL.Chk_Text(TxtLrNo.Text) & ", " & _
                " LrDate = " & AgL.Chk_Text(TxtLrDate.Text) & ", " & _
                " Item = " & AgL.Chk_Text(TxtItem.Text) & ", " & _
                " Weight =" & Val(TxtWeight.Text) & ", " & _
                " Qty = " & Val(TxtQty.Text) & ", " & _
                " Transporter = " & AgL.Chk_Text(TxtTransporter.AgSelectedValue) & ", " & _
                " Driver = " & AgL.Chk_Text(TxtDriverName.Text) & ", " & _
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & " " & _
                " Where UID = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DrTemp As DataRow() = Nothing
        Dim DsTemp As DataSet

        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = "Select P.* , ISNULL(SG.Add1,'')+ISNULL(SG.Add2,'') AS Address ," & _
                " SG.CityCode, SG.Phone" & _
                " From GateInOut P " & _
                " LEFT JOIN SubGroup SG ON SG.SubCode = P.SubCode" & _
                " Where P.DocID = '" & SearchCode & "'"
        Else
            mQry = "Select P.* , ISNULL(SG.Add1,'')+ISNULL(SG.Add2,'') AS Address ," & _
                " SG.CityCode, SG.Phone" & _
                " From GateInOut_Log P " & _
                " LEFT JOIN SubGroup SG ON SG.SubCode = P.SubCode" & _
                " Where P.UID='" & SearchCode & "'"
        End If
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtParty.AgSelectedValue = AgL.XNull(.Rows(0)("SubCode"))
                TxtPartyAddress.Text = AgL.XNull(.Rows(0)("Address"))
                TxtPartyMobileNo.Text = AgL.XNull(.Rows(0)("Phone"))
                TxtPartyCity.AgSelectedValue = AgL.XNull(.Rows(0)("CityCode"))
                TxtPassNo.Text = AgL.XNull(.Rows(0)("PassNo"))
                TxtVehicleType.Text = AgL.XNull(.Rows(0)("VehicleType"))
                TxtEntryTime.Text = AgL.XNull(.Rows(0)("V_Time"))
                TxtVehicleNo.Text = AgL.XNull(.Rows(0)("VehicleNo"))
                TxtManualNo.Text = AgL.XNull(.Rows(0)("Manual_RefNo"))
                TxtLrDate.Text = AgL.XNull(.Rows(0)("LrDate"))
                TxtLrNo.Text = AgL.XNull(.Rows(0)("LrNo"))
                TxtItem.Text = AgL.XNull(.Rows(0)("Item"))
                TxtWeight.Text = Format(AgL.VNull(.Rows(0)("Weight")), "0.000")
                TxtQty.Text = Format(AgL.VNull(.Rows(0)("Qty")), "0")
                TxtTransporter.AgSelectedValue = AgL.XNull(.Rows(0)("Transporter"))
                TxtDriverName.Text = AgL.XNull(.Rows(0)("Driver"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))

                TxtOutDate.Text = AgL.XNull(.Rows(0)("Close_Date"))
                TxtOutTime.Text = AgL.XNull(.Rows(0)("Close_Time"))
                TxtOutRemark.Text = AgL.XNull(.Rows(0)("Close_Remarks"))
                TxtOutEntryBy.Text = AgL.XNull(.Rows(0)("Close_EntryBy"))
                TxtGatePassNo.Text = AgL.XNull(.Rows(0)("Close_GatePassNo"))
                TxtOutItem.Text = AgL.XNull(.Rows(0)("Close_Item"))
                TxtOutWeight.Text = Format(AgL.VNull(.Rows(0)("Close_Weight")), "0.000")
                TxtOutQty.Text = Format(AgL.VNull(.Rows(0)("Close_Qty")), "0")
                Calculation()
            End If
        End With
        GrpGateOutEntry.Visible = True
        LinkLabel5.Visible = True
        BtnSaveGateOutEntry.Visible = True
        GrpGateOutEntry.Enabled = True

        If TxtOutEntryBy.Text.Trim = "" Then
            TxtOutRemark.Enabled = True
            BtnSaveGateOutEntry.Enabled = True
            TxtOutItem.Enabled = True
            TxtGatePassNo.Enabled = True
            TxtOutWeight.Enabled = True
            TxtOutQty.Enabled = True
        Else
            TxtOutRemark.Enabled = False
            BtnSaveGateOutEntry.Enabled = False
            TxtOutItem.Enabled = False
            TxtOutWeight.Enabled = False
            TxtOutQty.Enabled = False
            TxtGatePassNo.Enabled = False
        End If
    End Sub

    Private Sub FrmProductionOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Topctrl1.ChangeAgGridState(Dgl1, False)
        'GrpGateOutEntry.Tag = dtup
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = " SELECT SG.SubCode AS Code,SG.DispName AS Name,SG.Nature, " & _
                " IsNull(SG.IsDeleted ,0) AS IsDeleted, SG.Div_Code, " & _
                " ISNULL(SG.Add1,'')+ISNULL(SG.Add2,'') AS Address, SG.CityCode , SG.Phone ," & _
                " IsNull(SG.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status " & _
                " FROM SubGroup SG " & _
                " Where Sg.Nature In ('Customer','Supplier') "
        TxtParty.AgHelpDataSet(7, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT DISTINCT H.VehicleType AS Code ,H.VehicleType  " & _
                " FROM GateInOut H " & _
                " WHERE isnull(H.VehicleType,'') != ''  "
        TxtVehicleType.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT DISTINCT H.VehicleNo AS Code ,H.VehicleNo, SubCode  " & _
                " FROM GateInOut H " & _
                " WHERE isnull(H.VehicleNo,'') != ''  "
        TxtVehicleNo.AgHelpDataSet(1, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT DISTINCT H.Item AS Code ,H.Item  " & _
                " FROM GateInOut H " & _
                " WHERE isnull(H.Item,'') != ''  "
        TxtItem.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT H.SubCode AS Code,SG.DispName AS Transporter, " & _
                " IsNull(SG.IsDeleted ,0) AS IsDeleted, SG.Div_Code, " & _
                " IsNull(SG.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status " & _
                " FROM Transporter H " & _
                " LEFT JOIN SubGroup SG ON SG.SubCode=H.SubCode "
        TxtTransporter.AgHelpDataSet(3, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT C.CityCode AS Code, C.CityName AS City FROM City C"
        TxtPartyCity.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)

    End Sub

    Private Sub FrmProductionOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation

    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If AgL.RequiredField(TxtManualNo, LblManualNo.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtParty, LblParty.Text) Then passed = False : Exit Sub
        'If AgL.RequiredField(TxtItem, LblItem.Text) Then passed = False : Exit Sub

        'If Val(TxtWeight.Text) = 0 And Val(TxtQty.Text) = 0 Then
        '    MsgBox(" Fill Weight or Qty. !") : TxtWeight.Focus() : passed = False : Exit Sub
        'End If
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        TxtWeight.Text = 0 : TxtQty.Text = 0
        GrpGateOutEntry.Visible = False
        BtnSaveGateOutEntry.Visible = False
        LinkLabel5.Visible = False
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtParty.Validating
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.name
                Case TxtParty.Name
                    If sender.text.ToString.Trim <> "" Then
                        If sender.AgHelpDataSet IsNot Nothing Then
                            DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                            TxtPartyAddress.Text = AgL.XNull(DrTemp(0)("Address"))
                            TxtPartyCity.AgSelectedValue = AgL.XNull(DrTemp(0)("CityCode"))
                            TxtPartyMobileNo.Text = AgL.XNull(DrTemp(0)("Phone"))
                        End If
                    Else
                        TxtPartyAddress.Text = ""
                        TxtPartyMobileNo.Text = ""
                        TxtPartyCity.AgSelectedValue = ""
                    End If

                Case TxtOutRemark.Name
                    Topctrl1.Focus()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Txt_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtParty.Enter
        Try
            Select Case sender.name
                Case TxtParty.Name

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TempGateInOut_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        mQry = " SELECT getdate ()"
        TxtEntryTime.Text = CDate(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar().ToString).TimeOfDay.ToString.Substring(0, 5)
        TxtDateValue.Text = CDate(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar().ToString).Year.ToString.Substring(2, 2) + CDate(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar().ToString).Month.ToString + CDate(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar().ToString).Date.ToString.Substring(0, 2)
        mQry = " SELECT ISNULL(Max(substring(Manual_RefNo,7,4)),0) + 1  FROM GateInOut_Log " & _
                " WHERE substring(Manual_RefNo,1,6)='" & TxtDateValue.Text & "' "
        TxtManualNo.Text = TxtDateValue.Text + AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar().ToString.PadLeft(4, "0")
        TxtManualNo.Focus()
    End Sub

    Private Sub TempGateInOut_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        TxtV_Date.Enabled = False : TxtEntryTime.Enabled = False
        TxtOutDate.Enabled = False : TxtOutTime.Enabled = False : TxtOutEntryBy.Enabled = False
        TxtPartyAddress.Enabled = False : TxtPartyCity.Enabled = False : TxtPartyMobileNo.Enabled = False
    End Sub

    Private Sub BtnGateOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
    End Sub

    Sub ProcFillGateInDetail(ByVal bGateInDocId As String)
        Dim DrTemp As DataRow() = Nothing
        Dim DsTemp As DataSet
        mQry = "Select P.* " & _
            " From GateInOut_Log P " & _
            " Where P.UID='" & bGateInDocId & "'"

        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then

                TxtParty.AgSelectedValue = AgL.XNull(.Rows(0)("SubCode"))
                TxtVehicleType.Text = AgL.XNull(.Rows(0)("VehicleType"))
                TxtEntryTime.Text = AgL.XNull(.Rows(0)("V_Time"))
                TxtVehicleNo.Text = AgL.XNull(.Rows(0)("VehicleNo"))
                TxtManualNo.Text = AgL.XNull(.Rows(0)("Manual_RefNo"))
                TxtItem.Text = AgL.XNull(.Rows(0)("Item"))
                TxtWeight.Text = Format(AgL.VNull(.Rows(0)("Weight")), "0.000")
                TxtQty.Text = Format(AgL.VNull(.Rows(0)("Qty")), "0")
                TxtTransporter.AgSelectedValue = AgL.XNull(.Rows(0)("Transporter"))
                TxtDriverName.Text = AgL.XNull(.Rows(0)("Driver"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))

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
            'BtnSaveGateOutEntry.PerformClick()
        End If
    End Sub

    Private Sub TempGateInOut_BaseEvent_Topctrl_tbDel() Handles Me.BaseEvent_Topctrl_tbDel
        If TxtOutDate.Text.Trim <> "" Then
            MsgBox(" Entry can not be Change !") : Topctrl1.FButtonClick(14, True)
        End If
    End Sub

    Private Sub ProcSaveOutDetail(ByVal SearchCode As String)
        mQry = " UPDATE GateInOut_Log " & _
                " SET " & _
                " Close_Date = " & AgL.Chk_Text(TxtOutDate.Text) & ", " & _
                " Close_Time = " & AgL.Chk_Text(TxtOutTime.Text) & ", " & _
                " Close_EntryBy = " & AgL.Chk_Text(TxtOutEntryBy.Text) & ", " & _
                " Close_Item = " & AgL.Chk_Text(TxtOutItem.Text) & ", " & _
                " Close_Weight =" & Val(TxtOutWeight.Text) & ", " & _
                " Close_Qty = " & Val(TxtOutQty.Text) & ", " & _
                " Close_GatePassNo = " & AgL.Chk_Text(TxtGatePassNo.Text) & ", " & _
                " Close_Remarks = " & AgL.Chk_Text(TxtOutRemark.Text) & " " & _
                " Where UID = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " UPDATE GateInOut " & _
                " SET " & _
                " Close_Date = " & AgL.Chk_Text(TxtOutDate.Text) & ", " & _
                " Close_Time = " & AgL.Chk_Text(TxtOutTime.Text) & ", " & _
                " Close_EntryBy = " & AgL.Chk_Text(TxtOutEntryBy.Text) & ", " & _
                " Close_Item = " & AgL.Chk_Text(TxtOutItem.Text) & ", " & _
                " Close_Weight =" & Val(TxtOutWeight.Text) & ", " & _
                " Close_Qty = " & Val(TxtOutQty.Text) & ", " & _
                " Close_GatePassNo = " & AgL.Chk_Text(TxtGatePassNo.Text) & ", " & _
                " Close_Remarks = " & AgL.Chk_Text(TxtOutRemark.Text) & " " & _
                " Where UID = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

    End Sub

End Class
