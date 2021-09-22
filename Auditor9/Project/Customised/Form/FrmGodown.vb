Imports System.IO
Imports AgLibrary.ClsMain.agConstants

Public Class FrmGodown
    Inherits AgTemplate.TempMaster
    Dim mQry$ = ""
    Protected mGroupNature As String = "", mNature As String = ""

    Dim mSubgroupType$ = ""

    Dim mSubGroupNature As ESubgroupNature
    Dim mIsReturnValue As Boolean = False


    Public Const rowContactPerson As Integer = 0
    Public Const rowSalesTaxNo As Integer = 1
    Public Const rowPanNo As Integer = 2
    Public Const rowAadharNo As Integer = 3
    Public Const rowParent As Integer = 4
    Public Const rowArea As Integer = 5
    Friend WithEvents MnuOptions As ContextMenuStrip
    Private components As System.ComponentModel.IContainer
    Friend WithEvents MnuImport As ToolStripMenuItem
    Dim gStateCode As String

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal SubgroupType As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
        mSubgroupType = SubgroupType

    End Sub

    Public Property IsReturnValue() As Boolean
        Get
            IsReturnValue = mIsReturnValue
        End Get
        Set(ByVal value As Boolean)
            mIsReturnValue = value
        End Set
    End Property

    Public Enum ESubgroupNature
        Customer = 0
        Supplier = 1
    End Enum

    Public Class SubGroupConst
        Public Const GroupNature_Debtors As String = "A"
        Public Const Nature_Debtors As String = "Customer"
        Public Const GroupCode_Debtors As String = "0020"
        Public Const GroupNature_Creditors As String = "L"
        Public Const Nature_Creditors As String = "Supplier"
        Public Const GroupCode_Creditors As String = "0016"
    End Class

    Public Property SubGroupNature() As ESubgroupNature
        Get
            SubGroupNature = mSubGroupNature
        End Get
        Set(ByVal value As ESubgroupNature)
            mSubGroupNature = value
        End Set
    End Property

    Public Property SubgroupType() As String
        Get
            Return mSubgroupType
        End Get
        Set(ByVal value As String)
            mSubgroupType = value
        End Set
    End Property

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.LblNameReq = New System.Windows.Forms.Label()
        Me.LblManualCodeReq = New System.Windows.Forms.Label()
        Me.TxtManualCode = New AgControls.AgTextBox()
        Me.LblManualCode = New System.Windows.Forms.Label()
        Me.TxtDispName = New AgControls.AgTextBox()
        Me.LblName = New System.Windows.Forms.Label()
        Me.TxtAcGroup = New AgControls.AgTextBox()
        Me.LblAcGroup = New System.Windows.Forms.Label()
        Me.LblAcGroupReq = New System.Windows.Forms.Label()
        Me.MnuOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuImport = New System.Windows.Forms.ToolStripMenuItem()
        Me.GrpUP.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MnuOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(1023, 41)
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 338)
        Me.GroupBox1.Size = New System.Drawing.Size(1065, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(6, 342)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(142, 415)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(231, 342)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(400, 342)
        Me.GBoxApprove.Size = New System.Drawing.Size(147, 44)
        Me.GBoxApprove.Text = "Approved By"
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Location = New System.Drawing.Point(3, 23)
        Me.TxtApproveBy.Size = New System.Drawing.Size(141, 18)
        Me.TxtApproveBy.Tag = ""
        '
        'CmdDiscard
        '
        Me.CmdDiscard.Location = New System.Drawing.Point(118, 18)
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(702, 342)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(462, 342)
        Me.GBoxDivision.Size = New System.Drawing.Size(132, 44)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Size = New System.Drawing.Size(126, 18)
        Me.TxtDivision.Tag = ""
        '
        'TxtStatus
        '
        Me.TxtStatus.AgSelectedValue = ""
        Me.TxtStatus.Tag = ""
        '
        'LblNameReq
        '
        Me.LblNameReq.AutoSize = True
        Me.LblNameReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblNameReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblNameReq.Location = New System.Drawing.Point(391, 143)
        Me.LblNameReq.Name = "LblNameReq"
        Me.LblNameReq.Size = New System.Drawing.Size(10, 7)
        Me.LblNameReq.TabIndex = 781
        Me.LblNameReq.Text = "Ä"
        '
        'LblManualCodeReq
        '
        Me.LblManualCodeReq.AutoSize = True
        Me.LblManualCodeReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblManualCodeReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblManualCodeReq.Location = New System.Drawing.Point(392, 123)
        Me.LblManualCodeReq.Name = "LblManualCodeReq"
        Me.LblManualCodeReq.Size = New System.Drawing.Size(10, 7)
        Me.LblManualCodeReq.TabIndex = 778
        Me.LblManualCodeReq.Text = "Ä"
        '
        'TxtManualCode
        '
        Me.TxtManualCode.AgAllowUserToEnableMasterHelp = False
        Me.TxtManualCode.AgLastValueTag = Nothing
        Me.TxtManualCode.AgLastValueText = Nothing
        Me.TxtManualCode.AgMandatory = True
        Me.TxtManualCode.AgMasterHelp = True
        Me.TxtManualCode.AgNumberLeftPlaces = 0
        Me.TxtManualCode.AgNumberNegetiveAllow = False
        Me.TxtManualCode.AgNumberRightPlaces = 0
        Me.TxtManualCode.AgPickFromLastValue = False
        Me.TxtManualCode.AgRowFilter = ""
        Me.TxtManualCode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtManualCode.AgSelectedValue = Nothing
        Me.TxtManualCode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtManualCode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtManualCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtManualCode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtManualCode.Location = New System.Drawing.Point(406, 116)
        Me.TxtManualCode.MaxLength = 20
        Me.TxtManualCode.Name = "TxtManualCode"
        Me.TxtManualCode.Size = New System.Drawing.Size(292, 18)
        Me.TxtManualCode.TabIndex = 2
        '
        'LblManualCode
        '
        Me.LblManualCode.AutoSize = True
        Me.LblManualCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualCode.Location = New System.Drawing.Point(310, 117)
        Me.LblManualCode.Name = "LblManualCode"
        Me.LblManualCode.Size = New System.Drawing.Size(38, 16)
        Me.LblManualCode.TabIndex = 775
        Me.LblManualCode.Text = "Code"
        '
        'TxtDispName
        '
        Me.TxtDispName.AgAllowUserToEnableMasterHelp = False
        Me.TxtDispName.AgLastValueTag = Nothing
        Me.TxtDispName.AgLastValueText = Nothing
        Me.TxtDispName.AgMandatory = True
        Me.TxtDispName.AgMasterHelp = True
        Me.TxtDispName.AgNumberLeftPlaces = 0
        Me.TxtDispName.AgNumberNegetiveAllow = False
        Me.TxtDispName.AgNumberRightPlaces = 0
        Me.TxtDispName.AgPickFromLastValue = False
        Me.TxtDispName.AgRowFilter = ""
        Me.TxtDispName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDispName.AgSelectedValue = Nothing
        Me.TxtDispName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDispName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDispName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDispName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDispName.Location = New System.Drawing.Point(406, 136)
        Me.TxtDispName.MaxLength = 100
        Me.TxtDispName.Name = "TxtDispName"
        Me.TxtDispName.Size = New System.Drawing.Size(292, 18)
        Me.TxtDispName.TabIndex = 3
        '
        'LblName
        '
        Me.LblName.AutoSize = True
        Me.LblName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblName.Location = New System.Drawing.Point(309, 136)
        Me.LblName.Name = "LblName"
        Me.LblName.Size = New System.Drawing.Size(42, 16)
        Me.LblName.TabIndex = 777
        Me.LblName.Text = "Name"
        '
        'TxtAcGroup
        '
        Me.TxtAcGroup.AgAllowUserToEnableMasterHelp = False
        Me.TxtAcGroup.AgLastValueTag = Nothing
        Me.TxtAcGroup.AgLastValueText = Nothing
        Me.TxtAcGroup.AgMandatory = True
        Me.TxtAcGroup.AgMasterHelp = False
        Me.TxtAcGroup.AgNumberLeftPlaces = 0
        Me.TxtAcGroup.AgNumberNegetiveAllow = False
        Me.TxtAcGroup.AgNumberRightPlaces = 0
        Me.TxtAcGroup.AgPickFromLastValue = False
        Me.TxtAcGroup.AgRowFilter = ""
        Me.TxtAcGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAcGroup.AgSelectedValue = Nothing
        Me.TxtAcGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAcGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAcGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAcGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAcGroup.Location = New System.Drawing.Point(406, 156)
        Me.TxtAcGroup.MaxLength = 100
        Me.TxtAcGroup.Name = "TxtAcGroup"
        Me.TxtAcGroup.Size = New System.Drawing.Size(292, 18)
        Me.TxtAcGroup.TabIndex = 11
        Me.TxtAcGroup.Visible = False
        '
        'LblAcGroup
        '
        Me.LblAcGroup.AutoSize = True
        Me.LblAcGroup.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAcGroup.Location = New System.Drawing.Point(309, 156)
        Me.LblAcGroup.Name = "LblAcGroup"
        Me.LblAcGroup.Size = New System.Drawing.Size(67, 16)
        Me.LblAcGroup.TabIndex = 860
        Me.LblAcGroup.Text = "A/c Group"
        Me.LblAcGroup.Visible = False
        '
        'LblAcGroupReq
        '
        Me.LblAcGroupReq.AutoSize = True
        Me.LblAcGroupReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblAcGroupReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblAcGroupReq.Location = New System.Drawing.Point(391, 162)
        Me.LblAcGroupReq.Name = "LblAcGroupReq"
        Me.LblAcGroupReq.Size = New System.Drawing.Size(10, 7)
        Me.LblAcGroupReq.TabIndex = 861
        Me.LblAcGroupReq.Text = "Ä"
        Me.LblAcGroupReq.Visible = False
        '
        'MnuOptions
        '
        Me.MnuOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuImport})
        Me.MnuOptions.Name = "MnuOptions"
        Me.MnuOptions.Size = New System.Drawing.Size(111, 26)
        '
        'MnuImport
        '
        Me.MnuImport.Name = "MnuImport"
        Me.MnuImport.Size = New System.Drawing.Size(110, 22)
        Me.MnuImport.Text = "Import"
        '
        'FrmGodown
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(1023, 386)
        Me.ContextMenuStrip = Me.MnuOptions
        Me.Controls.Add(Me.LblAcGroupReq)
        Me.Controls.Add(Me.TxtAcGroup)
        Me.Controls.Add(Me.LblAcGroup)
        Me.Controls.Add(Me.LblNameReq)
        Me.Controls.Add(Me.LblManualCodeReq)
        Me.Controls.Add(Me.TxtManualCode)
        Me.Controls.Add(Me.LblManualCode)
        Me.Controls.Add(Me.TxtDispName)
        Me.Controls.Add(Me.LblName)
        Me.Name = "FrmGodown"
        Me.Text = "Buyer Master"
        Me.Controls.SetChildIndex(Me.LblName, 0)
        Me.Controls.SetChildIndex(Me.TxtDispName, 0)
        Me.Controls.SetChildIndex(Me.LblManualCode, 0)
        Me.Controls.SetChildIndex(Me.TxtManualCode, 0)
        Me.Controls.SetChildIndex(Me.LblManualCodeReq, 0)
        Me.Controls.SetChildIndex(Me.LblNameReq, 0)
        Me.Controls.SetChildIndex(Me.LblAcGroup, 0)
        Me.Controls.SetChildIndex(Me.TxtAcGroup, 0)
        Me.Controls.SetChildIndex(Me.LblAcGroupReq, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.GrpUP.ResumeLayout(False)
        Me.GrpUP.PerformLayout()
        Me.GBoxEntryType.ResumeLayout(False)
        Me.GBoxEntryType.PerformLayout()
        Me.GBoxMoveToLog.ResumeLayout(False)
        Me.GBoxMoveToLog.PerformLayout()
        Me.GBoxApprove.ResumeLayout(False)
        Me.GBoxApprove.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GBoxDivision.ResumeLayout(False)
        Me.GBoxDivision.PerformLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MnuOptions.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Protected WithEvents LblName As System.Windows.Forms.Label
    Public WithEvents TxtDispName As AgControls.AgTextBox
    Protected WithEvents LblManualCode As System.Windows.Forms.Label
    Protected WithEvents TxtManualCode As AgControls.AgTextBox
    Protected WithEvents LblManualCodeReq As System.Windows.Forms.Label
    Protected WithEvents LblNameReq As System.Windows.Forms.Label
    Protected WithEvents TxtAcGroup As AgControls.AgTextBox
    Protected WithEvents LblAcGroup As System.Windows.Forms.Label
    Protected WithEvents LblAcGroupReq As System.Windows.Forms.Label
#End Region

    Private Sub FrmShade_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        AgL.PubFindQry = " SELECT H.SubCode AS SearchCode,  H.DispName AS [Display Name], 
                         H.ManualCode As [Manual Code], H.Address, C.CityName As [City Name], 
                         H.Mobile, H.EMail, 
                         H.EntryBy As [Entry By], H.EntryDate As [Entry Date], H.EntryType As [Entry Type], 
                         H.Status, AG.GroupName As [GROUP No], D.Div_Name As Division,SM.Name As [Site Name] 
                         FROM SubGroup H 
                         LEFT JOIN Division D On D.Div_Code=H.Div_Code  
                         LEFT JOIN SiteMast SM On SM.Code=H.Site_Code 
                         LEFT JOIN AcGroup AG On AG.GroupCode = H.GroupCode 
                         LEFT JOIN City C On C.CityCode = H.CityCode  
                         Left Join SubgroupType ST On H.SubgroupType = ST.SubgroupType
                        Where 1=1 
                        "

        AgL.PubFindQry += " And   H.SubgroupType = '" & AgLibrary.ClsMain.agConstants.SubgroupType.Godown & "' "

        AgL.PubFindQryOrdBy = "[Name]"
    End Sub

    Private Sub FrmShade_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "SubGroup"

        PrimaryField = "SubCode"
    End Sub


    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = "Select S.SubCode as Code, S.ManualCode, S.DispName as Name " &
                " From SubGroup S  " &
                " Where S.Site_Code =" & AgL.Chk_Text(AgL.PubSiteCode) & "" &
                " Order By S.ManualCode "
        TxtManualCode.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select S.SubCode as Code, S.DispName As Name " &
                " From SubGroup S " &
                " Where S.Site_Code =" & AgL.Chk_Text(AgL.PubSiteCode) & "" &
                " Order By S.DispName "
        TxtDispName.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)


        mQry = "SELECT A.GroupCode AS Code, A.GroupName AS Name, A.GroupNature , A.Nature  " &
                  " FROM AcGroup A "
        TxtAcGroup.AgHelpDataSet(2) = AgL.FillData(mQry, AgL.GCn)


    End Sub

    Private Sub FrmShade_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        mQry = "
                Select S.SubCode As SearchCode 
                From SubGroup S 
                Left Join SubgroupType ST On S.SubgroupType = ST.SubgroupType
                Where 1=1 

                "
        mQry += " And   S.SubgroupType = '" & AgLibrary.ClsMain.agConstants.SubgroupType.Godown & "' "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet
        Dim DrTemp As DataRow() = Nothing
        Dim I As Integer

        mQry = "Select S.*, P.Name as ParentName, State.ManualCode as StateCode, A.Description as AreaName 
                    From SubGroup S 
                    Left Join viewHelpSubgroup P on S.Parent = P.Code
                    Left Join City C On S.CityCode = C.CityCode   
                    Left Join State On C.State = State.Code
                    Left Join Area A On S.Area = A.Code
                    Where S.SubCode='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("SubCode"))
                TxtManualCode.Text = AgL.XNull(.Rows(0)("ManualCode"))
                TxtDispName.Text = AgL.XNull(.Rows(0)("DispName"))
                TxtAcGroup.AgSelectedValue = AgL.XNull(.Rows(0)("GroupCode"))
                gStateCode = AgL.XNull(.Rows(0)("StateCode"))
                mNature = AgL.XNull(.Rows(0)("Nature"))
                mGroupNature = AgL.XNull(.Rows(0)("GroupNature"))


            End If
        End With


        Topctrl1.tPrn = False
    End Sub

    Private Sub Control_Enter(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Select Case sender.name
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub ProcSave()
        Dim MastPos As Long
        Dim mTrans As Boolean = False
        Dim ChildDataPassed As Boolean = True
        Dim bName$ = "", mUpLineStr$ = ""
        Try
            If AgL.PubMoveRecApplicable Then MastPos = BMBMaster.Position

            'For Data Validation
            If AgCL.AgCheckMandatory(Me) = False Then Exit Sub
            If AgL.RequiredField(TxtDispName, LblName.Text) Then Exit Sub

            If Not ChildDataPassed Then
                Exit Sub
            End If

            If Topctrl1.Mode = "Add" Then
                mSearchCode = AgL.GetMaxId("SubGroup", "SubCode", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
                mInternalCode = mSearchCode
            End If

            If TxtAcGroup.Visible = False Then
                If mSubGroupNature = ESubgroupNature.Customer Then
                    TxtAcGroup.AgSelectedValue = SubGroupConst.GroupCode_Debtors
                    mGroupNature = SubGroupConst.GroupNature_Debtors
                    mNature = SubGroupConst.Nature_Debtors
                Else
                    TxtAcGroup.AgSelectedValue = SubGroupConst.GroupCode_Creditors
                    mGroupNature = SubGroupConst.GroupNature_Creditors
                    mNature = SubGroupConst.Nature_Creditors
                End If
            End If

            If AgL.RequiredField(TxtManualCode, LblManualCode.Text) Then Exit Sub


            If Topctrl1.Mode = "Add" Then
                mQry = "Select count(*) From SubGroup Where ManualCode='" & TxtManualCode.Text & "' AND Site_Code =" & AgL.Chk_Text(AgL.PubSiteCode) & ""
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Code Already Exists")
            Else
                mQry = "Select count(*) From SubGroup Where ManualCode ='" & TxtManualCode.Text & "' And SubCode<>'" & mInternalCode & "'  AND Site_Code =" & AgL.Chk_Text(AgL.PubSiteCode) & ""
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Code Already Exists")
            End If

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True

            bName = TxtDispName.Text + " {" + TxtManualCode.Text + "}"


            If Topctrl1.Mode = "Add" Then
                mQry = "INSERT INTO SubGroup(SubCode, Site_Code, Name, DispName, " &
                        " GroupCode, GroupNature, ManualCode,	Nature, SubgroupType, " &
                        " EntryBy, EntryDate,  EntryType, EntryStatus, Div_Code, Status) " &
                        " VALUES(" & AgL.Chk_Text(mSearchCode) & ", " &
                        " '" & AgL.PubSiteCode & "', " & AgL.Chk_Text(bName) & ",	" &
                        " " & AgL.Chk_Text(TxtDispName.Text) & ", " & AgL.Chk_Text(TxtAcGroup.AgSelectedValue) & ", " &
                        " " & AgL.Chk_Text(mGroupNature) & ", " & AgL.Chk_Text(TxtManualCode.Text) & ", " &
                        " " & AgL.Chk_Text(mNature) & ",  " &
                        " " & AgL.Chk_Text(AgLibrary.ClsMain.agConstants.SubgroupType.Godown) & ", " &
                        " " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", " &
                        " " & AgL.Chk_Text(Topctrl1.Mode) & ", " & AgL.Chk_Text(LogStatus.LogOpen) & ", " &
                        " " & AgL.Chk_Text(TxtDivision.AgSelectedValue) & ", " & AgL.Chk_Text(TxtStatus.Text) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                mQry = "UPDATE SubGroup " &
                        " SET " &
                        " Name = " & AgL.Chk_Text(bName) & ", " &
                        " DispName = " & AgL.Chk_Text(TxtDispName.Text) & ", " &
                        " GroupCode = " & AgL.Chk_Text(TxtAcGroup.AgSelectedValue) & ", " &
                        " GroupNature = " & AgL.Chk_Text(mGroupNature) & ", " &
                        " ManualCode = " & AgL.Chk_Text(TxtManualCode.Text) & ", " &
                        " Nature = " & AgL.Chk_Text(mNature) & ", " &
                        " EntryType = " & AgL.Chk_Text(Topctrl1.Mode) & ", " &
                        " EntryStatus = " & AgL.Chk_Text(LogStatus.LogOpen) & ", " &
                        " Div_Code = " & AgL.Chk_Text(TxtDivision.AgSelectedValue) & ", " &
                        " MoveToLogDate = " & AgL.Chk_Date(CDate(AgL.PubLoginDate).ToString("u")) & ", " &
                        " MoveToLog = '" & AgL.PubUserName & "' " &
                        " Where Subcode = " & AgL.Chk_Text(mSearchCode) & "  "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If


            Dim mRegSr As Integer = 0

            mQry = "Delete From SubgroupRegistration Where Subcode = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)



            Call AgL.LogTableEntry(mSearchCode, Me.Text, AgL.MidStr(Topctrl1.Mode, 0, 1), AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)

            AgL.ETrans.Commit()
            mTrans = False


            If AgL.PubMoveRecApplicable Then
                FIniMaster(0, 1)
                Topctrl1_tbRef()
            End If

            If Topctrl1.Mode = "Add" Then
                If mIsReturnValue = True Then Me.Close() : Exit Sub
                Topctrl1.LblDocId.Text = mSearchCode
                Topctrl1.FButtonClick(0)
                Exit Sub
            Else
                Topctrl1.SetDisp(True)
                If AgL.PubMoveRecApplicable Then MoveRec()
            End If

        Catch ex As Exception
            If mTrans = True Then AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtAcGroup.Validating
        Dim DtTemp As DataTable = Nothing
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.NAME
                Case TxtAcGroup.Name
                    If sender.text.ToString.Trim = "" Or sender.AgSelectedValue.Trim = "" Then
                        mGroupNature = ""
                        mNature = ""
                    Else
                        If sender.AgHelpDataSet IsNot Nothing Then
                            DrTemp = TxtAcGroup.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(TxtAcGroup.AgSelectedValue) & "")
                            mGroupNature = AgL.XNull(DrTemp(0)("GroupNature"))
                            mNature = AgL.XNull(DrTemp(0)("Nature"))
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmSteward_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsReturnValue = False Then
            ''AgL.WinSetting(Me, 350, 913, 0, 0)
        Else
            Topctrl1.FButtonClick(0)
        End If
        If Not AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
            Me.ContextMenuStrip = Nothing
        End If
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub TxtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                Topctrl1.FButtonClick(13)
            End If
        End If
    End Sub

    Private Sub FrmParty_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        If AgL.PubServerName = "" Then
            TxtManualCode.Text = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(ManualCode AS INTEGER)),0) +1 FROM Subgroup  WHERE ABS(ManualCode)>0", AgL.GcnRead).ExecuteScalar)
        Else
            TxtManualCode.Text = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(ManualCode AS INTEGER)),0) +1 FROM Subgroup  WHERE IsNumeric(ManualCode)>0", AgL.GcnRead).ExecuteScalar)
        End If
        TxtAcGroup.Tag = "0017"
        TxtAcGroup.Text = "Opening Stock"
        TxtDispName.Focus()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub FrmParty_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        TxtManualCode.Focus()
    End Sub


    Private Sub FrmPerson_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid

    End Sub

    Private Sub FrmPerson_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dim I As Integer

        gStateCode = ""
    End Sub

    Private Sub FrmPerson_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation

    End Sub

    Public Function ValidateGstNoForParty(Subcode As String) As Boolean
        Try
            Dim mQry As String
            Dim DtSubcode As DataTable
            Dim mReason As String

            mQry = "Select Sg.*, S.ManualCode as StateCode 
                    From SubGroup Sg 
                    Left Join City C On Sg.CityCode = C.CityCode 
                    Left Join State S On C.State = S.Code
                    Where Sg.Subcode='" & Subcode & "'"
            DtSubcode = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtSubcode.Rows.Count > 0 Then
                If AgL.XNull(DtSubcode.Rows(0)("SalesTaxPostingGroup")) = "REGISTERED" Then
                    If AgL.XNull(DtSubcode.Rows(0)("STRegNo")) = "" Then
                        mReason = "Gst No. Can not be blank"
                    ElseIf Len(AgL.XNull(DtSubcode.Rows(0)("STRegNo"))) <> 15 Then
                        mReason = "Gst No. should be of 15 characters"
                    ElseIf AgL.XNull(DtSubcode.Rows(0)("STRegNo")).ToString.Substring(0, 2) <> AgL.XNull(DtSubcode.Rows(0)("StateCode")) Then
                        mReason = "First two characteres of gst no are not matching with state code"
                    Else
                        ValidateGstNoForParty = True
                    End If
                Else
                    MsgBox("Gst No. is only for registered dealers")
                End If
            Else
                MsgBox("Record not found for passed subcode")
            End If


        Catch ex As Exception

        End Try
    End Function

End Class
