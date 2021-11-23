Imports System.IO
Imports AgLibrary.ClsMain.agConstants
Imports System.Xml
Imports Customised.ClsMain
Imports System.ComponentModel
Imports System.Linq

Public Class FrmStudent
    Inherits AgTemplate.TempMaster
    Dim mQry$ = ""
    Protected mGroupNature As String = "", mNature As String = ""

    Dim mSubGroupNature As ESubgroupNature
    Friend WithEvents PnlMain As Panel
    Dim mIsReturnValue As Boolean = False

    Public Const ColSNo As String = "S.No."
    Public WithEvents DglMain As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1BtnDetail As String = "Detail"
    Public Const Col1HeadOriginal As String = "Head Original"
    Public Const Col1LastValue As String = "Last Value"

    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Facility As String = "Facility"
    Public Const Col1FacilitySubHead As String = "Sub Head"
    Public Const Col1StartDate As String = "Start Date"
    Public Const Col1EndDate As String = "End Date"
    Public Const Col1ChargeableFrom As String = "Chargeable From"
    Public Const Col1ChargeableUpTo As String = "Chargeable UpTo"
    Public Const Col1Remark As String = "Remark"

    Public Const rowSubgroupType As Integer = 0
    Public Const rowCode As Integer = 1
    Public Const rowName As Integer = 2
    Public Const rowPrintingName As Integer = 3
    Public Const rowFatherName As Integer = 4
    Public Const rowMotherName As Integer = 5
    Public Const rowAddress As Integer = 6
    Public Const rowCity As Integer = 7
    Public Const rowPin As Integer = 8
    Public Const rowContactNo As Integer = 9
    Public Const rowMobile As Integer = 10
    Public Const rowEmail As Integer = 11
    Public Const rowSite As Integer = 12
    Public Const rowAcGroup As Integer = 13
    Public Const rowContactPerson As Integer = 14
    Public Const rowPanNo As Integer = 15
    Public Const rowAadharNo As Integer = 16
    Public Const rowParent As Integer = 17
    Public Const rowArea As Integer = 18
    Public Const rowBankName As Integer = 19
    Public Const rowBankAccount As Integer = 20
    Public Const rowBankIFSC As Integer = 21
    Public Const rowShowAccountInOtherDivisions As Integer = 22
    Public Const rowShowAccountInOtherSites As Integer = 23
    Public Const rowBlockedTransactions As Integer = 24
    Public Const rowLockText As Integer = 25
    Public Const rowReligion As Integer = 26
    Public Const rowCaste As Integer = 27
    Public Const rowGender As Integer = 28
    Public Const rowAdmissionDate As Integer = 29
    Public Const rowLeftDate As Integer = 30
    Public Const rowDOB As Integer = 31
    Public Const rowFeeHead As Integer = 32
    Public Const rowClass As Integer = 33
    Public Const rowSection As Integer = 34
    Public Const rowRollNo As Integer = 35
    Public Const rowHouse As Integer = 36
    Public Const rowDiscount As Integer = 37
    Public Const rowRemarks As Integer = 38


    Public Const hcSubgroupType As String = "A/c Type"
    Public Const hcCode As String = "Code"
    Public Const hcName As String = "Name"
    Public Const hcPrintingDescription As String = "Printing Description"
    Public Const hcFatherName As String = "Father Name"
    Public Const hcMotherName As String = "Mother Name"
    Public Const hcAddress As String = "Address"
    Public Const hcCity As String = "City"
    Public Const hcPincode As String = "Pincode"
    Public Const hcContactNo As String = "Contact No."
    Public Const hcMobile As String = "Mobile"
    Public Const hcEmail As String = "Email"
    Public Const hcSite As String = "Site"
    Public Const hcAcGroup As String = "A/c Group"
    Public Const hcContactPerson As String = "Contact Person"
    Public Const hcPanNo As String = "PAN No."
    Public Const hcAadharNo As String = "Aadhar No."
    Public Const hcParent As String = "Parent"
    Public Const hcArea As String = "Area"
    Public Const hcBankName As String = "Bank Name"
    Public Const hcBankAccount As String = "Bank Account No."
    Public Const hcBankIFSC As String = "Bank IFSC"
    Public Const hcShowAccountInOtherDivisions As String = "Show A/c In Other Divisions"
    Public Const hcShowAccountInOtherSites As String = "Show A/c In Other Sites"
    Public Const hcBlockedTransactions As String = "Blocked Transactions"
    Public Const hcLockText As String = "Lock Text"
    Public Const hcReligion As String = "Religion"
    Public Const hcCaste As String = "Caste"
    Public Const hcGender As String = "Gender"
    Public Const hcAdmissionDate As String = "Admission Date"
    Public Const hcDOB As String = "DOB"
    Public Const hcLeftDate As String = "Left Date"
    Public Const hcFeeHead As String = "Fee Head"
    Public Const hcClass As String = "Class"
    Public Const hcSection As String = "Section"
    Public Const hcRollNo As String = "Roll No"
    Public Const hcHouse As String = "House"
    Public Const hcDiscount As String = "Discount"
    Public Const hcRemarks As String = "Remarks"

    Dim gStateCode As String
    Public WithEvents Pnl1 As Panel
    Dim DtSubgroupTypeSettings As DataTable

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
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

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.PnlMain = New System.Windows.Forms.Panel()
        Me.MnuOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuImportFromExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuBulkEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.OFDMain = New System.Windows.Forms.OpenFileDialog()
        Me.BtnAttachments = New System.Windows.Forms.Button()
        Me.Pnl1 = New System.Windows.Forms.Panel()
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
        Me.Topctrl1.Size = New System.Drawing.Size(974, 41)
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 564)
        Me.GroupBox1.Size = New System.Drawing.Size(1016, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(6, 568)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(142, 638)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(215, 568)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(400, 568)
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
        Me.GroupBox2.Location = New System.Drawing.Point(702, 568)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(459, 568)
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
        'PnlMain
        '
        Me.PnlMain.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PnlMain.Location = New System.Drawing.Point(1, 47)
        Me.PnlMain.Name = "PnlMain"
        Me.PnlMain.Size = New System.Drawing.Size(968, 366)
        Me.PnlMain.TabIndex = 15
        '
        'MnuOptions
        '
        Me.MnuOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuImportFromExcel, Me.MnuBulkEdit})
        Me.MnuOptions.Name = "MnuOptions"
        Me.MnuOptions.Size = New System.Drawing.Size(171, 48)
        '
        'MnuImportFromExcel
        '
        Me.MnuImportFromExcel.Name = "MnuImportFromExcel"
        Me.MnuImportFromExcel.Size = New System.Drawing.Size(170, 22)
        Me.MnuImportFromExcel.Text = "Import From Excel"
        '
        'MnuBulkEdit
        '
        Me.MnuBulkEdit.Name = "MnuBulkEdit"
        Me.MnuBulkEdit.Size = New System.Drawing.Size(170, 22)
        Me.MnuBulkEdit.Text = "Bulk Edit"
        '
        'OFDMain
        '
        Me.OFDMain.FileName = "price.xls"
        Me.OFDMain.Filter = "*.xls|*.Xls"
        Me.OFDMain.InitialDirectory = "D:\"
        Me.OFDMain.ShowHelp = True
        Me.OFDMain.Title = "Select Excel File"
        '
        'BtnAttachments
        '
        Me.BtnAttachments.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnAttachments.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAttachments.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAttachments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnAttachments.Location = New System.Drawing.Point(833, 589)
        Me.BtnAttachments.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnAttachments.Name = "BtnAttachments"
        Me.BtnAttachments.Size = New System.Drawing.Size(136, 23)
        Me.BtnAttachments.TabIndex = 1019
        Me.BtnAttachments.TabStop = False
        Me.BtnAttachments.Text = "Add Attachments"
        Me.BtnAttachments.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnAttachments.UseVisualStyleBackColor = True
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl1.Location = New System.Drawing.Point(1, 416)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(973, 149)
        Me.Pnl1.TabIndex = 1020
        '
        'FrmStudent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(974, 612)
        Me.ContextMenuStrip = Me.MnuOptions
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.BtnAttachments)
        Me.Controls.Add(Me.PnlMain)
        Me.MaximizeBox = True
        Me.Name = "FrmStudent"
        Me.Text = "Buyer Master"
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.PnlMain, 0)
        Me.Controls.SetChildIndex(Me.BtnAttachments, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
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

    End Sub
    Friend WithEvents MnuOptions As ContextMenuStrip
    Private components As System.ComponentModel.IContainer
    Friend WithEvents MnuImportFromExcel As ToolStripMenuItem
    Public WithEvents OFDMain As OpenFileDialog
    Friend WithEvents MnuBulkEdit As ToolStripMenuItem
    Protected WithEvents BtnAttachments As Button
#End Region

    Private Sub FrmShade_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        AgL.PubFindQry = " SELECT H.SubCode AS SearchCode,  H.Name AS [Name], 
                         H.ManualCode As [Code], IfNull(ST.Description,H.SubgroupType) as [Subgroup Type], H.Address, C.CityName As [City Name], 
                         H.Mobile, H.Phone, H.EMail, H.SalesTaxPostingGroup,
                         H.EntryBy As [Entry By], H.EntryDate As [Entry Date], H.EntryType As [Entry Type], 
                         H.Status, AG.GroupName As [GROUP Name], D.Div_Name As Division,SM.Name As [Site Name],
                        (SELECT Max(RegistrationNo) FROM SubgroupRegistration WHERE RegistrationType ='Sales Tax No' AND Subcode =H.Subcode) as [Gst No]
                         FROM SubGroup H 
                         LEFT JOIN Division D On D.Div_Code=H.Div_Code  
                         LEFT JOIN SiteMast SM On SM.Code=H.Site_Code 
                         LEFT JOIN AcGroup AG On AG.GroupCode = H.GroupCode 
                         LEFT JOIN City C On C.CityCode = H.CityCode  
                         Left Join SubgroupType ST On H.SubgroupType = ST.SubgroupType
                        Where 1=1 
                        "
        AgL.PubFindQry += " And   H.SubgroupType = '" & ClsSchool.SubGroupType_Student & "' "

        'AgL.PubFindQry += " Order By H.Name "


        AgL.PubFindQryOrdBy = "[Name]"
    End Sub

    Private Sub FrmShade_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "SubGroup"
        MainLineTableCsv = "SubgroupSiteDivisionDetail"

        PrimaryField = "SubCode"
    End Sub

    'Private Sub ApplySubgroupTypeSetting(SubgroupType As String)
    '    Dim mQry As String
    '    Dim DsTemp As DataSet
    '    Dim DtTemp As DataTable
    '    Dim I As Integer, J As Integer
    '    Dim mDgl1RowCount As Integer

    '    Try
    '        For I = 0 To DglMain.Rows.Count - 1
    '            DglMain.Rows(I).Visible = False
    '        Next

    '        mQry = "Select H.*
    '                from EntryHeaderUISetting H                   
    '                Where EntryName='" & Me.Name & "' And NCat = '" & SubgroupType & "' And GridName ='" & DglMain.Name & "' "
    '        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

    '        If DtTemp.Rows.Count > 0 Then
    '            For I = 0 To DtTemp.Rows.Count - 1
    '                For J = 0 To DglMain.Rows.Count - 1
    '                    If AgL.XNull(DtTemp.Rows(I)("FieldName")) = DglMain.Item(Col1HeadOriginal, J).Value Then
    '                        DglMain.Rows(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
    '                        If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDgl1RowCount += 1
    '                        DglMain.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
    '                        If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
    '                            DglMain.Item(Col1Head, J).Value = AgL.XNull(DtTemp.Rows(I)("Caption"))
    '                        End If
    '                    End If
    '                Next
    '            Next
    '        End If
    '        If mDgl1RowCount = 0 Then DglMain.Visible = False Else DglMain.Visible = True

    '        If AgL.StrCmp(DglMain(Col1Value, rowSubgroupType).Tag, AgLibrary.ClsMain.agConstants.SubgroupType.Employee) Then
    '            mQry = "Select Count(*) from SiteMast"
    '            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 1 Then
    '                DglMain.Rows(rowSite).Visible = True
    '            Else
    '                DglMain.Rows(rowSite).Visible = False
    '            End If
    '        End If

    '        mQry = "Select S.*, A.GroupName As AcGroupName, A.GroupNature, A.Nature 
    '                from subgroupTypeSetting S
    '                Left Join AcGroup A On S.AcGroupCode = A.GroupCode
    '               Where SubgroupType = '" & SubgroupType & "' "
    '        DsTemp = AgL.FillData(mQry, AgL.GCn)
    '        DtSubgroupTypeSettings = DsTemp.Tables(0)
    '        With DsTemp.Tables(0)
    '            If DsTemp.Tables(0).Rows.Count > 0 Then
    '                DglMain(Col1Value, rowAcGroup).Tag = AgL.XNull(DtSubgroupTypeSettings.Rows(0)("AcGroupCode"))
    '                DglMain(Col1Value, rowAcGroup).Value = AgL.XNull(DtSubgroupTypeSettings.Rows(0)("AcGroupName"))
    '                mGroupNature = AgL.XNull(.Rows(0)("GroupNature"))
    '                mNature = AgL.XNull(.Rows(0)("Nature"))
    '            End If
    '        End With
    '    Catch ex As Exception
    '        MsgBox(ex.Message & " [ApplySubgroupTypeSetting]")
    '    End Try
    'End Sub
    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, AgL.PubSiteCode, "", DglMain(Col1Value, rowSubgroupType).Tag, "", "", "")
        FGetSettings = mValue
    End Function
    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
    End Sub
    Private Sub FrmShade_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        mQry = "Select S.SubCode As SearchCode 
            From SubGroup S 
            Left Join SubgroupType ST On S.SubgroupType = ST.SubgroupType
            Where 1=1 
            "
        mQry += " And   S.SubgroupType = '" & ClsSchool.SubGroupType_Student & "' "


        mQry += " Order by S.Name "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub
    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet
        Dim DtTemp As DataTable
        Dim DrTemp As DataRow() = Nothing
        Dim DtSiteDivisionCount As DataTable
        Dim I As Integer

        mQry = "Select S.*, Sgt.Description As SubGroupTypeDesc, P.Name as ParentName, C.CityName, State.ManualCode as StateCode, 
                    A.Description as AreaName, Ins.Description as InterestSlabName , AcGroup.GroupName, Site.Name as SiteName,
                    Designation.Code as DesignationCode, Designation.Description  as DesignationName,
                    CF.Description as ChequeFormatName, Tg.Description As TdsGroupDesc, Tc.Description As TdsCategoryDesc,
                    Rel.Name As ReligionName, Cst.Name As CasteName
                    From SubGroup S 
                    Left Join viewHelpSubgroup P on S.Parent = P.Code
                    Left Join City C On S.CityCode = C.CityCode   
                    Left Join State On C.State = State.Code
                    Left Join AcGroup On S.GroupCode = AcGroup.GroupCode
                    Left Join Area A On S.Area = A.Code
                    Left Join InterestSlab InS on S.InterestSlab = Ins.Code
                    LEFT JOIN TdsGroup Tg On S.TdsGroup = Tg.Code
                    LEFT JOIN TdsCategory Tc On S.TdsCategory = Tc.Code
                    LEFT JOIN SubGroup Rel ON S.Religion = Rel.SubCode
                    LEFT JOIN SubGroup Cst ON S.Caste = Cst.SubCode
                    Left Join SiteMast Site On S.Site_Code = Site.Code
                    Left Join HRM_Employee Emp On S.Subcode = Emp.Subcode
                    Left Join HRM_Designation Designation On Emp.Designation = Designation.Code
                    Left Join ChequeFormat CF On S.ChequeFormat = CF.Code
                    LEFT JOIN SubGroupType Sgt On S.SubGroupType = Sgt.SubGroupType
                    Where S.SubCode='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                DglMain(Col1Value, rowSubgroupType).Tag = AgL.XNull(.Rows(0)("SubgroupType"))
                If AgL.XNull(.Rows(0)("SubgroupTypeDesc")) <> "" Then
                    DglMain(Col1Value, rowSubgroupType).Value = AgL.XNull(.Rows(0)("SubgroupTypeDesc"))
                Else
                    DglMain(Col1Value, rowSubgroupType).Value = AgL.XNull(.Rows(0)("SubgroupType"))
                End If
                mInternalCode = AgL.XNull(.Rows(0)("SubCode"))
                DglMain(Col1Value, rowCode).Value = AgL.XNull(.Rows(0)("ManualCode"))
                DglMain(Col1Value, rowName).Value = AgL.XNull(.Rows(0)("Name"))
                DglMain(Col1Value, rowPrintingName).Value = IIf(AgL.XNull(.Rows(0)("DispName")) = AgL.XNull(.Rows(0)("Name")), "", AgL.XNull(.Rows(0)("DispName")))
                DglMain(Col1Value, rowFatherName).Value = AgL.XNull(.Rows(0)("FatherName"))
                DglMain(Col1Value, rowMotherName).Value = AgL.XNull(.Rows(0)("MotherName"))
                DglMain(Col1Value, rowAcGroup).Tag = AgL.XNull(.Rows(0)("GroupCode"))
                DglMain(Col1Value, rowAcGroup).Value = AgL.XNull(.Rows(0)("GroupName"))
                DglMain(Col1Value, rowAddress).Value = AgL.XNull(.Rows(0)("Address"))
                DglMain(Col1Value, rowCity).Tag = AgL.XNull(.Rows(0)("CityCode"))
                DglMain(Col1Value, rowCity).Value = AgL.XNull(.Rows(0)("CityName"))
                gStateCode = AgL.XNull(.Rows(0)("StateCode"))
                DglMain(Col1Value, rowPin).Value = AgL.XNull(.Rows(0)("PIN"))
                DglMain(Col1Value, rowSite).Tag = AgL.XNull(.Rows(0)("Site_Code"))
                DglMain(Col1Value, rowSite).Value = AgL.XNull(.Rows(0)("SiteName"))
                DglMain(Col1Value, rowMobile).Value = AgL.XNull(.Rows(0)("Mobile"))
                DglMain(Col1Value, rowContactNo).Value = AgL.XNull(.Rows(0)("Phone"))
                DglMain(Col1Value, rowEmail).Value = AgL.XNull(.Rows(0)("EMail"))
                DglMain.Item(Col1Value, rowShowAccountInOtherDivisions).Value = IIf((.Rows(0)("ShowAccountInOtherDivisions")), "Yes", "No")
                DglMain.Item(Col1Value, rowShowAccountInOtherSites).Value = IIf((.Rows(0)("ShowAccountInOtherSites")), "Yes", "No")
                DglMain(Col1Value, rowDiscount).Value = AgL.XNull(.Rows(0)("Discount"))
                DglMain(Col1Value, rowRemarks).Value = AgL.XNull(.Rows(0)("Remarks"))
                DglMain(Col1Value, rowLockText).Value = AgL.XNull(.Rows(0)("LockText"))
                DglMain(Col1Value, rowReligion).Tag = AgL.XNull(.Rows(0)("Religion"))
                DglMain(Col1Value, rowReligion).Value = AgL.XNull(.Rows(0)("ReligionName"))
                DglMain(Col1Value, rowCaste).Tag = AgL.XNull(.Rows(0)("Caste"))
                DglMain(Col1Value, rowCaste).Value = AgL.XNull(.Rows(0)("CasteName"))
                DglMain(Col1Value, rowGender).Value = AgL.XNull(.Rows(0)("Gender"))
                DglMain(Col1Value, rowLeftDate).Value = ClsMain.FormatDate(AgL.XNull(DsTemp.Tables(0).Rows(0)("LeftDate")))
                DglMain(Col1Value, rowDOB).Value = ClsMain.FormatDate(AgL.XNull(DsTemp.Tables(0).Rows(0)("DOB")))
                mNature = AgL.XNull(.Rows(0)("Nature"))
                mGroupNature = AgL.XNull(.Rows(0)("GroupNature"))

                DglMain.Item(Col1Value, rowContactPerson).Value = AgL.XNull(.Rows(0)("ContactPerson"))
                DglMain.Item(Col1Value, rowArea).Tag = AgL.XNull(.Rows(0)("Area"))
                DglMain.Item(Col1Value, rowArea).Value = AgL.XNull(.Rows(0)("AreaName"))
            End If
        End With

        mQry = "Select * From SubgroupRegistration where Subcode = '" & mSearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        If DsTemp.Tables(0).Rows.Count > 0 Then
            For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                If UCase(AgL.XNull(DsTemp.Tables(0).Rows(I)("RegistrationType"))) = SubgroupRegistrationType.PanNo.ToUpper Then
                    DglMain.Item(Col1Value, rowPanNo).Value = AgL.XNull(DsTemp.Tables(0).Rows(I)("RegistrationNo"))
                ElseIf UCase(AgL.XNull(DsTemp.Tables(0).Rows(I)("RegistrationType"))) = SubgroupRegistrationType.AadharNo.ToUpper Then
                    DglMain.Item(Col1Value, rowAadharNo).Value = AgL.XNull(DsTemp.Tables(0).Rows(I)("RegistrationNo"))
                End If
            Next
        End If


        mQry = "Select * From SubgroupBankAccount where Subcode = '" & mSearchCode & "' And Sr=0 "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        If DsTemp.Tables(0).Rows.Count > 0 Then
            DglMain.Item(Col1Value, rowBankAccount).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("BankAccount"))
            DglMain.Item(Col1Value, rowBankName).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("BankName"))
            DglMain.Item(Col1Value, rowBankIFSC).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("BankIFSC"))
        End If


        mQry = "Select Vt.NCat, Max(Vt.Description) As NCatName
                From SubgroupBlockedTransactions L 
                LEFT JOIN Voucher_Type Vt ON L.NCat = Vt.NCat
                Where L.SubCode = '" & mSearchCode & "' Group By Vt.NCat "
        DtTemp = AgL.FillData(mQry, AgL.GCn).tABLES(0)
        For I = 0 To DtTemp.Rows.Count - 1
            If DglMain.Item(Col1Value, rowBlockedTransactions).Tag <> "" Then DglMain.Item(Col1Value, rowBlockedTransactions).Tag += ","
            If DglMain.Item(Col1Value, rowBlockedTransactions).Value <> "" Then DglMain.Item(Col1Value, rowBlockedTransactions).Value += ","
            DglMain.Item(Col1Value, rowBlockedTransactions).Tag += AgL.XNull(DtTemp.Rows(I)("NCat"))
            DglMain.Item(Col1Value, rowBlockedTransactions).Value += AgL.XNull(DtTemp.Rows(I)("NCatName"))
        Next


        mQry = "SELECT H.*, Facility.Name As FacilityName, FacilitySubHead.Name As FacilitySubHeadName
                FROM SubgroupFacility H
                LEFT JOIN SubGroup Facility ON H.Facility = Facility.SubCode
                LEFT JOIN SubGroup FacilitySubHead ON H.FacilitySubHead = FacilitySubHead.SubCode
                WHERE H.SubCode ='" & SearchCode & "'
                ORDER BY H.Sr "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                    Dgl1.Item(Col1Facility, I).Tag = AgL.XNull(.Rows(I)("Facility"))
                    Dgl1.Item(Col1Facility, I).Value = AgL.XNull(.Rows(I)("FacilityName"))
                    Dgl1.Item(Col1FacilitySubHead, I).Tag = AgL.XNull(.Rows(I)("FacilitySubHead"))
                    Dgl1.Item(Col1FacilitySubHead, I).Value = AgL.XNull(.Rows(I)("FacilitySubHeadName"))
                    Dgl1.Item(Col1StartDate, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("StartDate")))
                    Dgl1.Item(Col1EndDate, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("EndDate")))
                    Dgl1.Item(Col1ChargeableFrom, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("ChargeableFrom")))
                    Dgl1.Item(Col1ChargeableUpTo, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("ChargeableUpTo")))
                    Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))
                Next I
            End If
        End With

        mQry = "Select H.*, Class.Name As ClassName, Section.Name As SectionName, 
                House.Name As HouseName, FeeHead.Name As FeeHeadName 
                From SubgroupAdmission H
                LEFT JOIN SubGroup Class On H.Class = Class.SubCode
                LEFT JOIN SubGroup Section On H.Section = Section.SubCode
                LEFT JOIN SubGroup House On H.House = House.SubCode
                LEFT JOIN SubGroup FeeHead On H.FeeHead = FeeHead.SubCode
                Where H.Subcode = '" & mSearchCode & "'
                And PromotionDate Is Null "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        If DsTemp.Tables(0).Rows.Count > 0 Then
            DglMain.Item(Col1Value, rowAdmissionDate).Value = ClsMain.FormatDate(AgL.XNull(DsTemp.Tables(0).Rows(0)("AdmissionDate")))
            DglMain.Item(Col1Value, rowFeeHead).Tag = AgL.XNull(DsTemp.Tables(0).Rows(0)("FeeHead"))
            DglMain.Item(Col1Value, rowFeeHead).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("FeeHeadName"))
            DglMain.Item(Col1Value, rowClass).Tag = AgL.XNull(DsTemp.Tables(0).Rows(0)("Class"))
            DglMain.Item(Col1Value, rowClass).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("ClassName"))
            DglMain.Item(Col1Value, rowSection).Tag = AgL.XNull(DsTemp.Tables(0).Rows(0)("Section"))
            DglMain.Item(Col1Value, rowSection).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("SectionName"))
            DglMain.Item(Col1Value, rowRollNo).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("RollNo"))
            DglMain.Item(Col1Value, rowHouse).Tag = AgL.XNull(DsTemp.Tables(0).Rows(0)("House"))
            DglMain.Item(Col1Value, rowHouse).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("HouseName"))
        End If

        'mQry = "SELECT H.*, Facility.Name As FacilityName
        '        FROM SubgroupFacility H
        '        LEFT JOIN SubGroup Facility On H.Facility = Facility.SubCode
        '        WHERE H.SubCode ='" & SearchCode & "'
        '        ORDER BY H.Sr "
        'DsTemp = AgL.FillData(mQry, AgL.GCn)
        'With DsTemp.Tables(0)
        '    Dgl1.RowCount = 1
        '    Dgl1.Rows.Clear()
        '    If .Rows.Count > 0 Then
        '        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
        '            Dgl1.Rows.Add()
        '            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
        '            Dgl1.Item(Col1Facility, I).Tag = AgL.XNull(.Rows(I)("Facility"))
        '            Dgl1.Item(Col1Facility, I).Value = AgL.XNull(.Rows(I)("FacilityName"))
        '            Dgl1.Item(Col1StartDate, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("StartDate")))
        '            Dgl1.Item(Col1EndDate, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("EndDate")))
        '            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))
        '        Next I
        '    End If
        'End With

        SetLastValues()

        SetAttachmentCaption()

        Topctrl1.tPrn = False
        If AgL.StrCmp(DglMain.Item(Col1Value, rowSubgroupType).Tag, AgLibrary.ClsMain.agConstants.SubgroupType.Process) And
                Not AgL.StrCmp(AgL.PubUserName, "Super") Then
            Topctrl1.tAdd = False
            Topctrl1.tDel = False
            Topctrl1.tEdit = False
        End If
    End Sub
    Private Sub SetLastValues()
        Dim I As Integer
        For I = 0 To DglMain.Rows.Count - 1
            DglMain(Col1LastValue, I).Value = DglMain(Col1Value, I).Value
            DglMain(Col1LastValue, I).Tag = DglMain(Col1Value, I).Tag
        Next
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
        Dim DtTemp As DataTable
        Dim I As Integer
        Dim mSr As Integer = 0
        Dim mUpLineStr$ = ""
        Try
            If EntryPointIniMode <> AgTemplate.ClsMain.EntryPointIniMode.Insertion Then
                MastPos = BMBMaster.Position
            End If

            'For Data Validation
            If Data_Validation() = False Then Exit Sub

            If Topctrl1.Mode = "Add" Then
                If AgL.StrCmp(DglMain(Col1Value, rowSubgroupType).Tag, AgLibrary.ClsMain.agConstants.SubgroupType.Division) Then
                    Dim MaxDiv_Code As String = AgL.Dman_Execute("Select Max(Div_Code) As Div_Cde From Division ", AgL.GCn).ExecuteScalar()
                    mSearchCode = Chr(Asc(MaxDiv_Code) + 1)
                ElseIf AgL.StrCmp(DglMain(Col1Value, rowSubgroupType).Tag, AgLibrary.ClsMain.agConstants.SubgroupType.Site) Then
                    Dim MaxSite_Code As String = AgL.Dman_Execute("Select Cast(Max(Code) As BIGINT) As Site_Code From SiteMast ", AgL.GCn).ExecuteScalar()
                    mSearchCode = MaxSite_Code + 1
                Else
                    mSearchCode = AgL.GetMaxId("SubGroup", "SubCode", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
                End If
                mInternalCode = mSearchCode
            End If


            mQry = "Select * from AcGroup Where GroupCode = '" & DglMain.Item(Col1Value, rowAcGroup).Tag & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).tables(0)
            If DtTemp.Rows.Count > 0 Then
                mGroupNature = AgL.XNull(DtTemp.Rows(0)("GroupNature"))
                mNature = AgL.XNull(DtTemp.Rows(0)("Nature"))
            Else
                If mSubGroupNature = ESubgroupNature.Supplier Then
                    DglMain(Col1Value, rowAcGroup).Tag = SubGroupConst.GroupCode_Creditors
                    DglMain(Col1Value, rowAcGroup).Value = SubGroupConst.GroupCode_Creditors
                    mGroupNature = SubGroupConst.GroupNature_Creditors
                    mNature = SubGroupConst.Nature_Creditors
                Else
                    DglMain(Col1Value, rowAcGroup).Tag = SubGroupConst.GroupCode_Debtors
                    DglMain(Col1Value, rowAcGroup).Value = SubGroupConst.GroupCode_Debtors
                    mGroupNature = SubGroupConst.GroupNature_Debtors
                    mNature = SubGroupConst.Nature_Debtors
                End If
            End If

            If Topctrl1.Mode = "Add" Then
                If AgL.PubServerName = "" Then
                    DglMain(Col1Value, rowCode).Value = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(ManualCode AS INTEGER)),0) +1 FROM Subgroup  WHERE SubGroupType = '" & ClsSchool.SubGroupType_Student & "' And ABS(ManualCode)>0", AgL.GcnRead).ExecuteScalar)
                Else
                    DglMain(Col1Value, rowCode).Value = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(ManualCode AS INTEGER)),0) +1 FROM Subgroup  WHERE SubGroupType = '" & ClsSchool.SubGroupType_Student & "' And IsNumeric(ManualCode)>0", AgL.GcnRead).ExecuteScalar)
                End If

                If DglMain.Rows(rowCode).Visible = True Then
                    mQry = "Select count(*) From SubGroup Where SubGroupType = '" & ClsSchool.SubGroupType_Student & "' And ManualCode='" & DglMain(Col1Value, rowCode).Value & "'"
                    If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Code Already Exists")
                End If


                mQry = "Select count(*) From SubGroup Where Replace(Replace(Replace(Replace(Name,' ',''),'.',''),'-',''),'*','')='" & Replace(Replace(Replace(Replace(DglMain(Col1Value, rowName).Value, " ", ""), ".", ""), "-", ""), "*", "") & "' And CityCode = '" & DglMain(Col1Value, rowCity).Tag & "' "
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Name Already Exists")

                If DglMain(Col1Value, rowAadharNo).Value <> "" Then
                    mQry = "Select Sg.Name, Sg.Code, IfNull(Sg.Parent,Sg.Code) As Parent 
                            From SubgroupRegistration Sr With (NoLock)
                            Left Join viewHelpSubgroup Sg With (NoLock) On Sr.SubCode = Sg.Code
                            Where SR.RegistrationNo='" & DglMain(Col1Value, rowAadharNo).Value & "' 
                            And SR.RegistrationType = '" & SubgroupRegistrationType.AadharNo & "' 
                            And Sg.SubgroupType  = '" & DglMain(Col1Value, rowSubgroupType).Tag & "' "

                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        If AgL.XNull(DtTemp.Rows(0)("Code")) = DglMain(Col1Value, rowParent).Tag Or
                            AgL.XNull(DtTemp.Rows(0)("Parent")) = DglMain(Col1Value, rowParent).Tag Then
                        Else
                            MsgBox("Aadhar No. Already Exists for " & AgL.XNull(DtTemp.Rows(0)("Name")))
                            DglMain.CurrentCell = DglMain(Col1Value, rowAadharNo)
                            DglMain.Focus()
                            Exit Sub
                        End If
                    End If
                End If

                If DglMain(Col1Value, rowPanNo).Value <> "" Then
                    mQry = "Select Sg.Name, Sg.Code, IfNull(Sg.Parent,Sg.Code) As Parent 
                            From SubgroupRegistration Sr With (NoLock)
                            Left Join viewHelpSubgroup Sg With (NoLock) On Sr.SubCode = Sg.Code                            
                            Where RegistrationNo='" & DglMain(Col1Value, rowPanNo).Value & "' 
                            And RegistrationType = '" & SubgroupRegistrationType.PanNo & "' 
                            And Sg.SubgroupType  = '" & DglMain(Col1Value, rowSubgroupType).Tag & "' "
                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        If AgL.XNull(DtTemp.Rows(0)("Code")) = DglMain(Col1Value, rowParent).Tag Or
                            AgL.XNull(DtTemp.Rows(0)("Parent")) = DglMain(Col1Value, rowParent).Tag Then
                        Else
                            MsgBox("PAN No. Already Exists for " & AgL.XNull(DtTemp.Rows(0)("Name")))
                            DglMain.CurrentCell = DglMain(Col1Value, rowPanNo)
                            DglMain.Focus()
                            Exit Sub
                        End If
                    End If
                End If
            Else
                If DglMain.Rows(rowCode).Visible = True Then
                    mQry = "Select count(*) From SubGroup Where SubGroupType = '" & ClsSchool.SubGroupType_Student & "' And ManualCode ='" & DglMain(Col1Value, rowCode).Value & "' And SubCode<>'" & mInternalCode & "'"
                    If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Code Already Exists")
                End If

                mQry = "Select count(*) From SubGroup Where Replace(Replace(Replace(Replace(Name,' ',''),'.',''),'-',''),'*','')='" & Replace(Replace(Replace(Replace(DglMain(Col1Value, rowName).Value, " ", ""), ".", ""), "-", ""), "*", "") & "' And CityCode = '" & DglMain(Col1Value, rowCity).Tag & "' And SubCode<>'" & mInternalCode & "'"
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Name Already Exists")


                If DglMain(Col1Value, rowAadharNo).Value <> "" Then
                    mQry = "Select Sg.Name, Sg.Code, IfNull(Sg.Parent,Sg.Code) As Parent 
                            From SubgroupRegistration Sr With (NoLock)
                            Left Join viewHelpSubgroup Sg With (NoLock) On Sr.SubCode = Sg.Code
                            Where SR.RegistrationNo='" & DglMain(Col1Value, rowAadharNo).Value & "' 
                            And SR.RegistrationType = '" & SubgroupRegistrationType.AadharNo & "' 
                            And Sg.SubgroupType  = '" & DglMain(Col1Value, rowSubgroupType).Tag & "' 
                            And SubCode<>'" & mInternalCode & "'"

                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        If AgL.XNull(DtTemp.Rows(0)("Code")) = DglMain(Col1Value, rowParent).Tag Or
                            AgL.XNull(DtTemp.Rows(0)("Parent")) = DglMain(Col1Value, rowParent).Tag Then
                        Else
                            MsgBox("Aadhar No. Already Exists for " & AgL.XNull(DtTemp.Rows(0)("Name")))
                            DglMain.CurrentCell = DglMain(Col1Value, rowAadharNo)
                            DglMain.Focus()
                            Exit Sub
                        End If
                    End If
                End If

                If DglMain(Col1Value, rowPanNo).Value <> "" Then
                    mQry = "Select Sg.Name, Sg.Code, IfNull(Sg.Parent,Sg.Code) As Parent 
                            From SubgroupRegistration Sr With (NoLock)
                            Left Join viewHelpSubgroup Sg With (NoLock) On Sr.SubCode = Sg.Code                            
                            Where RegistrationNo='" & DglMain(Col1Value, rowPanNo).Value & "' 
                            And RegistrationType = '" & SubgroupRegistrationType.PanNo & "' 
                            And Sg.SubgroupType  = '" & DglMain(Col1Value, rowSubgroupType).Tag & "' 
                            And SubCode<>'" & mInternalCode & "' "
                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        If AgL.XNull(DtTemp.Rows(0)("Code")) = DglMain(Col1Value, rowParent).Tag Or
                            AgL.XNull(DtTemp.Rows(0)("Parent")) = DglMain(Col1Value, rowParent).Tag Then
                        Else
                            MsgBox("PAN No. Already Exists for " & AgL.XNull(DtTemp.Rows(0)("Name")))
                            DglMain.CurrentCell = DglMain(Col1Value, rowPanNo)
                            DglMain.Focus()
                            Exit Sub
                        End If
                    End If
                End If
            End If

            If DglMain(Col1Value, rowParent).Value <> "" And DglMain(Col1Value, rowParent).Value IsNot Nothing Then
                Dim DtParent As DataTable = AgL.FillData(" Select Sg.Parent, Sg1.Name As ParentName
                        From SubGroup Sg With (NoLock)
                        LEFT JOIN SubGroup Sg1 With (NoLock) On Sg.Parent = Sg1.SubCode
                        Where Sg.SubCode = '" & DglMain(Col1Value, rowParent).Tag & "' 
                        AND Sg.Parent IS NOT NULL", AgL.GCn).Tables(0)
                If DtParent.Rows.Count > 0 Then
                    DglMain(Col1Value, rowParent).Tag = AgL.XNull(DtParent.Rows(0)("Parent"))
                    DglMain(Col1Value, rowParent).Value = AgL.XNull(DtParent.Rows(0)("ParentName"))
                End If
            End If

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True

            If Topctrl1.Mode = "Add" Then
                mQry = "INSERT INTO SubGroup(SubCode, Site_Code, Name, DispName, FatherName, MotherName, " &
                        " GroupCode, GroupNature, ManualCode, Nature,	Address, CityCode,  " &
                        " PIN, Phone,  ContactPerson, SubgroupType, ShowAccountInOtherDivisions, ShowAccountInOtherSites, " &
                        " Religion, Caste, Gender, Discount, Remarks, " &
                        " Mobile, EMail, Parent, Area, DOB, LeftDate, " &
                        " EntryBy, EntryDate,  EntryType, EntryStatus, Div_Code, Status) " &
                        " VALUES(" & AgL.Chk_Text(mSearchCode) & ", " &
                        " " & AgL.Chk_Text(DglMain(Col1Value, rowSite).Tag) & ", " & AgL.Chk_Text(DglMain(Col1Value, rowName).Value) & ",	" &
                        " " & AgL.Chk_Text(IIf(DglMain(Col1Value, rowPrintingName).Value = "", DglMain(Col1Value, rowName).Value, DglMain(Col1Value, rowPrintingName).Value)) & ", " &
                        " " & AgL.Chk_Text(DglMain(Col1Value, rowFatherName).Value) & ", " &
                        " " & AgL.Chk_Text(DglMain(Col1Value, rowMotherName).Value) & ", " &
                        " " & AgL.Chk_Text(DglMain(Col1Value, rowAcGroup).Tag) & ", " &
                        " " & AgL.Chk_Text(mGroupNature) & ", " & AgL.Chk_Text(DglMain(Col1Value, rowCode).Value) & ", " &
                        " " & AgL.Chk_Text(mNature) & ", " & AgL.Chk_Text(DglMain(Col1Value, rowAddress).Value) & ", " &
                        " " & AgL.Chk_Text(DglMain(Col1Value, rowCity).Tag) & ", " &
                        " " & AgL.Chk_Text(DglMain(Col1Value, rowPin).Value) & ", " & AgL.Chk_Text(DglMain(Col1Value, rowContactNo).Value) & ", " &
                        " " & AgL.Chk_Text(DglMain.Item(Col1Value, rowContactPerson).Value) & ", " &
                        " " & AgL.Chk_Text(DglMain(Col1Value, rowSubgroupType).Tag) & ", " &
                        " " & IIf(DglMain.Item(Col1Value, rowShowAccountInOtherDivisions).Value.ToUpper = "NO", 0, 1) & ", " &
                        " " & IIf(DglMain.Item(Col1Value, rowShowAccountInOtherSites).Value.ToUpper = "NO", 0, 1) & ", " &
                        " " & AgL.Chk_Text(DglMain(Col1Value, rowReligion).Tag) & ", " &
                        " " & AgL.Chk_Text(DglMain(Col1Value, rowCaste).Tag) & ", " &
                        " " & AgL.Chk_Text(DglMain(Col1Value, rowGender).Value) & ", " &
                        " " & Val(DglMain(Col1Value, rowDiscount).Value) & ", " &
                        " " & AgL.Chk_Text(DglMain(Col1Value, rowRemarks).Value) & ", " &
                        " " & AgL.Chk_Text(DglMain(Col1Value, rowMobile).Value) & ", " &
                        " " & AgL.Chk_Text(DglMain(Col1Value, rowEmail).Value) & ", " &
                        " " & AgL.Chk_Text(DglMain.Item(Col1Value, rowParent).Tag) & ", " &
                        " " & AgL.Chk_Text(DglMain.Item(Col1Value, rowArea).Tag) & ", " &
                        " " & AgL.Chk_Date(DglMain.Item(Col1Value, rowDOB).Value) & ", " &
                        " " & AgL.Chk_Date(DglMain.Item(Col1Value, rowLeftDate).Value) & ", " &
                        " " & AgL.Chk_Text(AgL.PubUserName) & ", " &
                        " " & AgL.Chk_Date(CDate(AgL.GetDateTime(AgL.GcnRead)).ToString("u")) & ", " &
                        " " & AgL.Chk_Text(Topctrl1.Mode) & ", " & AgL.Chk_Text(LogStatus.LogOpen) & ", " &
                        " " & AgL.Chk_Text(TxtDivision.AgSelectedValue) & ", " & AgL.Chk_Text(TxtStatus.Text) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                mQry = "UPDATE SubGroup " &
                        " SET " &
                        " Name = " & AgL.Chk_Text(DglMain(Col1Value, rowName).Value) & ", " &
                        " DispName = " & AgL.Chk_Text(IIf(DglMain(Col1Value, rowPrintingName).Value = "", DglMain(Col1Value, rowName).Value, DglMain(Col1Value, rowPrintingName).Value)) & ", " &
                        " FatherName = " & AgL.Chk_Text(DglMain(Col1Value, rowFatherName).Value) & ", " &
                        " MotherName = " & AgL.Chk_Text(DglMain(Col1Value, rowMotherName).Value) & ", " &
                        " GroupCode = " & AgL.Chk_Text(DglMain(Col1Value, rowAcGroup).Tag) & ", " &
                        " GroupNature = " & AgL.Chk_Text(mGroupNature) & ", " &
                        " ManualCode = " & AgL.Chk_Text(DglMain(Col1Value, rowCode).Value) & ", " &
                        " Nature = " & AgL.Chk_Text(mNature) & ", " &
                        " Address = " & AgL.Chk_Text(DglMain(Col1Value, rowAddress).Value) & ", " &
                        " CityCode = " & AgL.Chk_Text(DglMain(Col1Value, rowCity).Tag) & ", " &
                        " Mobile = " & AgL.Chk_Text(DglMain(Col1Value, rowMobile).Value) & ", " &
                        " EMail = " & AgL.Chk_Text(DglMain(Col1Value, rowEmail).Value) & ", " &
                        " PIN = " & AgL.Chk_Text(DglMain(Col1Value, rowPin).Value) & ", " &
                        " Phone = " & AgL.Chk_Text(DglMain(Col1Value, rowContactNo).Value) & ", " &
                        " ContactPerson = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowContactPerson).Value) & ", " &
                        " Parent = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowParent).Tag) & ", " &
                        " Area = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowArea).Tag) & ", " &
                        " SubgroupType = " & AgL.Chk_Text(DglMain(Col1Value, rowSubgroupType).Tag) & ", " &
                        " ShowAccountInOtherDivisions = " & IIf(DglMain.Item(Col1Value, rowShowAccountInOtherDivisions).Value.ToUpper = "NO", 0, 1) & ", " &
                        " ShowAccountInOtherSites = " & IIf(DglMain.Item(Col1Value, rowShowAccountInOtherSites).Value.ToUpper = "NO", 0, 1) & ", " &
                        " Religion = " & AgL.Chk_Text(DglMain(Col1Value, rowReligion).Tag) & ", " &
                        " Caste = " & AgL.Chk_Text(DglMain(Col1Value, rowCaste).Tag) & ", " &
                        " Gender = " & AgL.Chk_Text(DglMain(Col1Value, rowGender).Value) & ", " &
                        " DOB = " & AgL.Chk_Date(DglMain(Col1Value, rowDOB).Value) & ", " &
                        " LeftDate = " & AgL.Chk_Date(DglMain(Col1Value, rowLeftDate).Value) & ", " &
                        " Discount = " & Val(DglMain(Col1Value, rowDiscount).Value) & ", " &
                        " Remarks = " & AgL.Chk_Text(DglMain(Col1Value, rowRemarks).Value) & ", " &
                        " EntryType = " & AgL.Chk_Text(Topctrl1.Mode) & ", " &
                        " EntryStatus = " & AgL.Chk_Text(LogStatus.LogOpen) & ", " &
                        " Div_Code = " & AgL.Chk_Text(TxtDivision.AgSelectedValue) & ", " &
                        " Site_Code = " & AgL.Chk_Text(DglMain(Col1Value, rowSite).Tag) & ", " &
                        " UploadDate = Null, " &
                        " MoveToLogDate = " & AgL.Chk_Date(CDate(AgL.PubLoginDate).ToString("u")) & ", " &
                        " MoveToLog = '" & AgL.PubUserName & "' " &
                        " Where Subcode = " & AgL.Chk_Text(mSearchCode) & "  "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            SaveDataInPersonLastTransactionValues(mSearchCode, AgL.GCn, AgL.ECmd)

            Dim mRegSr As Integer = 0

            mQry = "Delete From SubgroupRegistration Where Subcode = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            If DglMain.Item(Col1Value, rowPanNo).Value <> "" Then
                mRegSr += 1
                mQry = "Insert Into SubgroupRegistration(Subcode, Sr, RegistrationType, RegistrationNo)
                       Values ('" & mSearchCode & "', " & mRegSr & ", '" & SubgroupRegistrationType.PanNo & "', " & AgL.Chk_Text(DglMain.Item(Col1Value, rowPanNo).Value) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            If DglMain.Item(Col1Value, rowAadharNo).Value <> "" Then
                mRegSr += 1
                mQry = "Insert Into SubgroupRegistration(Subcode, Sr, RegistrationType, RegistrationNo)
                       Values ('" & mSearchCode & "', " & mRegSr & ", '" & SubgroupRegistrationType.AadharNo.ToUpper & "', " & AgL.Chk_Text(DglMain.Item(Col1Value, rowAadharNo).Value) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            mQry = "Delete From SubgroupBankAccount Where Subcode = '" & mSearchCode & "' And Sr=0"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            If DglMain.Item(Col1Value, rowBankName).Value <> "" Or DglMain.Item(Col1Value, rowBankAccount).Value <> "" Or DglMain.Item(Col1Value, rowBankIFSC).Value <> "" Then
                mQry = "Insert Into SubgroupBankAccount(Subcode, Sr, BankName, BankAccount, BankIFSC)
                        Values ('" & mSearchCode & "', 0, " & AgL.Chk_Text(DglMain.Item(Col1Value, rowBankName).Value) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowBankAccount).Value) & ", " & AgL.Chk_Text(DglMain.Item(Col1Value, rowBankIFSC).Value) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If


            mQry = "DELETE FROM SubgroupBlockedTransactions WHERE SubCode = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            FInsertSubgroupBlockedTransactions(AgL.GCn, AgL.ECmd)



            mQry = " Select Code From SiteMast With (NoLock) "
            DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
            For I = 0 To DtTemp.Rows.Count - 1
                mQry = "Update SubGroupSiteDivisionDetail Set Distance=(Select Distance From CitySiteDivisionDetail Where CityCode = '" & DglMain(Col1Value, rowCity).Tag & "' And Site_Code = '" & DtTemp.Rows(I)("Code") & "') 
                        Where subcode = '" & mSearchCode & "'  And Site_Code = '" & DtTemp.Rows(I)("Code") & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Next

            If AgL.VNull(AgL.Dman_Execute("Select Count(*) From SubgroupAdmission 
                    Where SubCode = '" & mSearchCode & "' And PromotionDate Is Null ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) = 0 Then
                mSr = AgL.VNull(AgL.Dman_Execute(" Select Max(Sr) From SubgroupAdmission 
                        Where SubCode = '" & mSearchCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())
                mQry = "Insert Into SubgroupAdmission(Subcode, Sr, Comp_Code, Div_Code, Site_Code, 
                        AdmissionDate, FeeHead, Class, Section, RollNo, House)
                        Values ('" & mSearchCode & "', " & mSr & ", " & AgL.Chk_Text(AgL.PubCompCode) & ", 
                        " & AgL.Chk_Text(AgL.PubDivCode) & ", " & AgL.Chk_Text(AgL.PubSiteCode) & ", 
                        " & AgL.Chk_Date(DglMain.Item(Col1Value, rowAdmissionDate).Value) & ", 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowFeeHead).Tag) & ", 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowClass).Tag) & ", 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSection).Tag) & ",
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowRollNo).Value) & ",
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowHouse).Tag) & "
                        ) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                mSr = AgL.VNull(AgL.Dman_Execute(" Select Sr From SubgroupAdmission 
                        Where SubCode = '" & mSearchCode & "' And PromotionDate Is NUll", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())
                mQry = " UPDATE SubgroupAdmission Set 
                        AdmissionDate = " & AgL.Chk_Date(DglMain.Item(Col1Value, rowAdmissionDate).Value) & ", 
                        FeeHead = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowFeeHead).Tag) & ", 
                        Class = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowClass).Tag) & ", 
                        Section = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSection).Tag) & ", 
                        RollNo = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowRollNo).Value) & ",
                        House = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowHouse).Tag) & "
                        Where SubCode = '" & mSearchCode & "'
                        And Sr = " & mSr & ""
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            mQry = "DELETE FROM SubgroupFacility WHERE SubCode  = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            For I = 0 To Dgl1.Rows.Count - 1
                If Dgl1.Item(Col1Facility, I).Value <> "" Then
                    mSr += 1
                    mQry = "INSERT INTO SubgroupFacility (SubCode, Sr, Div_Code, Site_Code, Facility, FacilitySubHead,
                            StartDate, EndDate, ChargeableFrom, ChargeableUpTo, Remark)
                            Select '" & mSearchCode & "', " & mSr & ", 
                            " & AgL.Chk_Text(AgL.PubDivCode) & ",
                            " & AgL.Chk_Text(AgL.PubSiteCode) & ", 
                            " & AgL.Chk_Text(Dgl1.Item(Col1Facility, I).Tag) & ", 
                            " & AgL.Chk_Text(Dgl1.Item(Col1FacilitySubHead, I).Tag) & ", 
                            " & AgL.Chk_Date(Dgl1.Item(Col1StartDate, I).Value) & ", 
                            " & AgL.Chk_Date(Dgl1.Item(Col1EndDate, I).Value) & ", 
                            " & AgL.Chk_Date(Dgl1.Item(Col1ChargeableFrom, I).Value) & ", 
                            " & AgL.Chk_Date(Dgl1.Item(Col1ChargeableUpTo, I).Value) & ", 
                            " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & "
                            "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            Next


            Call AgL.LogTableEntry(mSearchCode, Me.Text, AgL.MidStr(Topctrl1.Mode, 0, 1), AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)

            AgL.ETrans.Commit()
            mTrans = False


            If EntryPointIniMode = AgTemplate.ClsMain.EntryPointIniMode.Insertion Then
                Me.Close()
                Exit Sub
            End If


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
    Private Sub FInsertSubgroupBlockedTransactions(ByVal Conn As Object, ByVal Cmd As Object)
        Dim bRateListCode$ = ""
        Dim I As Integer, mSr As Integer

        Dim bValueArr As String() = DglMain.Item(Col1Value, rowBlockedTransactions).Tag.ToString.Split(",")

        mSr = AgL.Dman_Execute("Select IfNull(Max(Sr),0) From SubgroupBlockedTransactions With (NoLock) Where SubCode = '" & mSearchCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()

        For I = 0 To bValueArr.Length - 1
            If bValueArr(I) <> "" Then
                mSr += 1
                mQry = "INSERT INTO SubgroupBlockedTransactions(SubCode, Sr, NCat) 
                        VALUES(" & AgL.Chk_Text(mSearchCode) & ", 
                        " & mSr & ", 
                        " & AgL.Chk_Text(bValueArr(I)) & ") "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next
    End Sub
    Public Sub SaveDataInPersonLastTransactionValues(DocId As String, ByVal Conn As Object, ByVal Cmd As Object)
        Dim I As Integer, J As Integer
        Dim DtDivision As DataTable
        Dim DtSite As DataTable

        'mQry = "Delete from SubgroupSiteDivisionDetail Where Subcode='" & DocId & "'"
        'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Select Div_Code, Div_Name From Division Order By Div_Name"
        DtDivision = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

        mQry = "Select Code, Name From SiteMast Order By Name"
        DtSite = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

        For J = 0 To DtDivision.Rows.Count - 1
            For I = 0 To DtSite.Rows.Count - 1
                If Topctrl1.Mode = "Add" Then
                    mQry = " INSERT INTO SubgroupSiteDivisionDetail (Subcode, Div_Code, Site_Code) 
                                    VALUES (" & AgL.Chk_Text(DocId) & ", 
                                    " & AgL.Chk_Text(DtDivision.Rows(J)("Div_Code")) & ", 
                                    " & AgL.Chk_Text(DtSite.Rows(I)("Code")) & "                                     
                                    )"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                Else
                    mQry = "Select Count(*) from SubgroupSiteDivisionDetail Where Div_Code = '" & DtDivision.Rows(J)("Div_Code") & "' And  Site_Code = '" & DtSite.Rows(I)("Code") & "' And Subcode = '" & DocId & "' "
                    If AgL.Dman_Execute(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar() = 0 Then
                        mQry = " INSERT INTO SubgroupSiteDivisionDetail (Subcode, Div_Code, Site_Code) 
                                    VALUES (" & AgL.Chk_Text(DocId) & ", 
                                    " & AgL.Chk_Text(DtDivision.Rows(J)("Div_Code")) & ", 
                                    " & AgL.Chk_Text(DtSite.Rows(I)("Code")) & "                                     
                                    )"
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                End If
            Next
        Next
    End Sub
    Private Sub FrmSteward_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsReturnValue = False Then
            ''AgL.WinSetting(Me, 418, 913, 0, 0)
        Else
            Topctrl1.FButtonClick(0)
        End If

        If Not AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
            MnuImportFromExcel.Visible = False
        End If
    End Sub
    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub
    Private Sub FOpenCityMaster()
        Dim DrTemp As DataRow() = Nothing
        Dim bCityCode$ = ""
        Dim objMdi As New MDIMain
        Dim StrUserPermission As String
        Dim DTUP As DataTable

        StrUserPermission = AgIniVar.FunGetUserPermission(ClsMain.ModuleName, objMdi.MnuCityMaster.Name, objMdi.MnuCityMaster.Text, DTUP)

        Dim frmObj As FrmCity

        frmObj = New FrmCity(StrUserPermission, DTUP)
        frmObj.EntryPointIniMode = AgTemplate.ClsMain.EntryPointIniMode.Insertion
        frmObj.StartPosition = FormStartPosition.CenterParent
        frmObj.IniGrid()
        frmObj.ShowDialog()
        bCityCode = frmObj.mSearchCode
        frmObj = Nothing



        DglMain.Item(Col1Head, rowCity).Tag = Nothing
        DglMain(Col1Value, rowCity).Tag = bCityCode
        DglMain(Col1Value, rowCity).Value = AgL.XNull(AgL.Dman_Execute("Select CityName From City Where CityCode = '" & bCityCode & "'", AgL.GCn).ExecuteScalar)
        Validate_City()
        SendKeys.Send("{Enter}")
    End Sub


    Public Function FOpenPersonMaster(SubgroupType As String) As String
        Dim DrTemp As DataRow() = Nothing
        Dim bSubCode$ = ""
        Dim objMdi As New MDIMain
        Dim StrUserPermission As String
        Dim DTUP As DataTable

        StrUserPermission = AgIniVar.FunGetUserPermission(ClsMain.ModuleName, objMdi.MnuCustomerMaster.Name, objMdi.MnuCustomerMaster.Text, DTUP)

        Dim frmObj As FrmStudent

        frmObj = New FrmStudent(StrUserPermission, DTUP)
        frmObj.EntryPointIniMode = AgTemplate.ClsMain.EntryPointIniMode.Insertion
        frmObj.StartPosition = FormStartPosition.CenterParent
        frmObj.DglMain(Col1Value, rowSubgroupType).Tag = SubgroupType
        frmObj.DglMain(Col1Value, rowSubgroupType).Value = AgL.XNull(AgL.Dman_Execute(" Select IfNull(Description,SubGroupType) 
                                                From SubGroupType Where SubGroupType ='" & frmObj.DglMain(Col1Value, rowSubgroupType).Tag & "'", AgL.GCn).ExecuteScalar())
        frmObj.IniGrid()
        frmObj.ShowDialog()
        bSubCode = frmObj.mSearchCode
        frmObj = Nothing
    End Function
    Private Sub FrmParty_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        If AgL.PubServerName = "" Then
            DglMain(Col1Value, rowCode).Value = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(ManualCode AS INTEGER)),0) +1 FROM Subgroup  WHERE SubGroupType = '" & ClsSchool.SubGroupType_Student & "' And ABS(ManualCode)>0", AgL.GcnRead).ExecuteScalar)
        Else
            DglMain(Col1Value, rowCode).Value = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(ManualCode AS INTEGER)),0) +1 FROM Subgroup  WHERE SubGroupType = '" & ClsSchool.SubGroupType_Student & "' And IsNumeric(ManualCode)>0", AgL.GcnRead).ExecuteScalar)
        End If

        'If mSubGroupNature = ESubgroupNature.Customer Then
        '    TxtAcGroup.AgSelectedValue = SubGroupConst.GroupCode_Debtors
        '    mNature = SubGroupConst.Nature_Debtors
        '    mGroupNature = SubGroupConst.GroupNature_Debtors
        'Else
        '    TxtAcGroup.AgSelectedValue = SubGroupConst.GroupCode_Creditors
        '    mNature = SubGroupConst.Nature_Creditors
        '    mGroupNature = SubGroupConst.GroupNature_Creditors
        'End If


        If DglMain(Col1LastValue, rowSubgroupType).Value = "" Then
            DglMain(Col1Value, rowSubgroupType).Tag = ClsSchool.SubGroupType_Student
            DglMain(Col1Value, rowSubgroupType).Value = AgL.XNull(AgL.Dman_Execute(" Select IfNull(Description,SubGroupType) 
                                                From SubGroupType Where SubGroupType ='" & DglMain(Col1Value, rowSubgroupType).Tag & "'", AgL.GCn).ExecuteScalar())
        Else
            DglMain(Col1Value, rowSubgroupType).Value = DglMain(Col1LastValue, rowSubgroupType).Value
            DglMain(Col1Value, rowSubgroupType).Tag = DglMain(Col1LastValue, rowSubgroupType).Tag
        End If
        SetAttachmentCaption()

        If DglMain.Visible = True Then
            If DglMain(Col1Value, rowSubgroupType).Visible = False Then
                DglMain.CurrentCell = DglMain(Col1Value, rowName)
            Else
                DglMain.CurrentCell = DglMain(Col1Value, rowSubgroupType)
            End If
            DglMain.Focus()
        End If
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub FrmParty_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        If AgL.XNull(DglMain.Item(Col1Value, rowLockText).Value) <> "" Then
            MsgBox(AgL.XNull(DglMain.Item(Col1Value, rowLockText).Value) & ", Can not modify")
            Passed = False
            Exit Sub
        End If

        If ClsMain.IsEntryLockedWithLockText("SubGroup", "SubCode", mSearchCode) = True Then
            Passed = False
            Exit Sub
        End If

        DglMain.CurrentCell = DglMain(Col1Value, rowName)
        DglMain.Focus()
    End Sub

    Private Function FRetUpline(ByVal SubCode As String, ByRef mUpLineStr As String, Optional ByVal Parent As String = "") As String
        Dim mParent As String = ""
        If Parent = "" Then
            mQry = " SELECT Sg.Parent FROM SubGroup Sg  WHERE Sg.SubCode = '" & SubCode & "'"
            mParent = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)
        Else
            mParent = Parent
        End If

        If InStr(mUpLineStr, mSearchCode) > 0 Then
            Err.Raise(1, , "Parent Name Is Invalid.It is creating a cycle.")
        End If

        If mParent <> SubCode And mParent <> "" Then
            mUpLineStr += IIf(mUpLineStr = "", "|" + mParent + "|", "," + "|" + mParent + "|")
            FRetUpline(mParent, mUpLineStr)
        End If
        FRetUpline = mUpLineStr
    End Function
    Private Sub FrmParty_BaseFunction_DispText() Handles Me.BaseFunction_DispText

    End Sub

    Private Sub FrmStudent_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dim I As Integer

        DglMain.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DglMain, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(DglMain, Col1Head, 250, 255, Col1Head, True, True)
            .AddAgTextColumn(DglMain, Col1Mandatory, 12, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(DglMain, Col1Value, 630, 255, Col1Value, True, False)
            .AddAgButtonColumn(DglMain, Col1BtnDetail, 35, Col1BtnDetail, True, True)
            .AddAgTextColumn(DglMain, Col1HeadOriginal, 200, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(DglMain, Col1LastValue, 200, 255, Col1LastValue, False, True)
        End With
        AgL.AddAgDataGrid(DglMain, PnlMain)
        DglMain.EnableHeadersVisualStyles = False
        DglMain.Columns(Col1Mandatory).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings 2", 5.25, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        DglMain.Columns(Col1Mandatory).DefaultCellStyle.ForeColor = Color.Red
        DglMain.ColumnHeadersHeight = 35
        DglMain.AgSkipReadOnlyColumns = True
        DglMain.AllowUserToAddRows = False
        DglMain.RowHeadersVisible = False
        DglMain.ColumnHeadersVisible = False
        DglMain.BackgroundColor = Me.BackColor
        DglMain.BorderStyle = BorderStyle.None
        DglMain.Name = "DglMain"
        AgL.GridDesign(DglMain)
        DglMain.Anchor = AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top + AnchorStyles.Bottom



        DglMain.Rows.Add(39)

        DglMain.Item(Col1Head, rowSubgroupType).Value = hcSubgroupType
        DglMain.Item(Col1Head, rowCode).Value = hcCode
        DglMain.Item(Col1Head, rowName).Value = hcName
        DglMain.Item(Col1Head, rowPrintingName).Value = hcPrintingDescription
        DglMain.Item(Col1Head, rowFatherName).Value = hcFatherName
        DglMain.Item(Col1Head, rowMotherName).Value = hcMotherName
        DglMain.Item(Col1Head, rowAddress).Value = hcAddress
        DglMain.Item(Col1Head, rowCity).Value = hcCity
        DglMain.Item(Col1Head, rowPin).Value = hcPincode
        DglMain.Item(Col1Head, rowContactNo).Value = hcContactNo
        DglMain.Item(Col1Head, rowMobile).Value = hcMobile
        DglMain.Item(Col1Head, rowEmail).Value = hcEmail
        DglMain.Item(Col1Head, rowSite).Value = hcSite
        DglMain.Item(Col1Head, rowAcGroup).Value = hcAcGroup
        DglMain.Item(Col1Head, rowContactPerson).Value = hcContactPerson
        DglMain.Item(Col1Head, rowPanNo).Value = hcPanNo
        DglMain.Item(Col1Head, rowAadharNo).Value = hcAadharNo
        DglMain.Item(Col1Head, rowParent).Value = hcParent
        DglMain.Item(Col1Head, rowArea).Value = hcArea
        DglMain.Item(Col1Head, rowBankName).Value = hcBankName
        DglMain.Item(Col1Head, rowBankAccount).Value = hcBankAccount
        DglMain.Item(Col1Head, rowBankIFSC).Value = hcBankIFSC
        DglMain.Item(Col1Head, rowShowAccountInOtherDivisions).Value = hcShowAccountInOtherDivisions
        DglMain.Item(Col1Head, rowShowAccountInOtherSites).Value = hcShowAccountInOtherSites
        DglMain.Item(Col1Head, rowBlockedTransactions).Value = hcBlockedTransactions
        DglMain.Item(Col1Head, rowLockText).Value = hcLockText
        DglMain.Item(Col1Head, rowReligion).Value = hcReligion
        DglMain.Item(Col1Head, rowCaste).Value = hcCaste
        DglMain.Item(Col1Head, rowGender).Value = hcGender
        DglMain.Item(Col1Head, rowAdmissionDate).Value = hcAdmissionDate
        DglMain.Item(Col1Head, rowLeftDate).Value = hcLeftDate
        DglMain.Item(Col1Head, rowDOB).Value = hcDOB
        DglMain.Item(Col1Head, rowFeeHead).Value = hcFeeHead
        DglMain.Item(Col1Head, rowClass).Value = hcClass
        DglMain.Item(Col1Head, rowSection).Value = hcSection
        DglMain.Item(Col1Head, rowRollNo).Value = hcRollNo
        DglMain.Item(Col1Head, rowHouse).Value = hcHouse
        DglMain.Item(Col1Head, rowDiscount).Value = hcDiscount
        DglMain.Item(Col1Head, rowRemarks).Value = hcRemarks

        DglMain.Rows(rowAddress).Height = 50
        DglMain(Col1Value, rowAddress).Style.WrapMode = DataGridViewTriState.True
        DglMain.Rows(rowRemarks).Height = 50
        DglMain(Col1Value, rowRemarks).Style.WrapMode = DataGridViewTriState.True

        For I = 0 To DglMain.Rows.Count - 1
            DglMain(Col1HeadOriginal, I).Value = DglMain(Col1Head, I).Value
        Next

        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Facility, 120, 0, Col1Facility, True, False, False)
            .AddAgTextColumn(Dgl1, Col1FacilitySubHead, 120, 0, Col1FacilitySubHead, True, False, False)
            .AddAgDateColumn(Dgl1, Col1StartDate, 120, Col1StartDate, True, False)
            .AddAgDateColumn(Dgl1, Col1EndDate, 120, Col1EndDate, True, False)
            .AddAgDateColumn(Dgl1, Col1ChargeableFrom, 120, Col1ChargeableFrom, True, False)
            .AddAgDateColumn(Dgl1, Col1ChargeableUpTo, 120, Col1ChargeableUpTo, True, False)
            .AddAgTextColumn(Dgl1, Col1Remark, 180, 0, Col1Remark, True, False, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.RowHeadersVisible = False
        Dgl1.ColumnHeadersHeight = 38
        Dgl1.BackgroundColor = Me.BackColor
        Dgl1.Name = "Dgl1"
        AgL.GridDesign(Dgl1)
        Dgl1.Anchor = AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom

        ApplyUISetting()
    End Sub
    Private Sub DglMain_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DglMain.CellEnter
        Try
            If DglMain.CurrentCell Is Nothing Then Exit Sub
            If Topctrl1.Mode = "BROWSE" Then
                DglMain.CurrentCell.ReadOnly = True
            End If

            If Me.Visible And sender.ReadOnly = False Then
                If sender.CurrentCell.ColumnIndex = sender.Columns(Col1Head).Index Or
                    sender.CurrentCell.ColumnIndex = sender.Columns(Col1Mandatory).Index Then
                    SendKeys.Send("{Tab}")
                End If

                If sender.CurrentCell.ColumnIndex = sender.Columns(Col1BtnDetail).Index Then
                    If TypeOf (sender.currentcell) IsNot DataGridViewButtonCell Then
                        SendKeys.Send("{Tab}")
                    End If
                End If
            End If

            If DglMain.CurrentCell.ColumnIndex <> DglMain.Columns(Col1Value).Index Then Exit Sub


            DglMain.AgHelpDataSet(DglMain.CurrentCell.ColumnIndex) = Nothing
            CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0
            'Dgl1.Columns(Col1Value).DefaultCellStyle.WrapMode = DataGridViewTriState.True            


            Select Case DglMain.CurrentCell.RowIndex
                Case rowContactPerson
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 100
                Case rowPanNo
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 10
                Case rowAadharNo
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 12
                Case rowPin
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgNumberLeftPlaces = 6
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgNumberRightPlaces = 0
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgNumberNegetiveAllow = False
                Case rowBankName, rowBankAccount, rowBankIFSC
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 50
                Case rowAdmissionDate, rowDOB, rowLeftDate
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub DglMain_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DglMain.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim bNewMasterCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = DglMain.CurrentCell.RowIndex
            bColumnIndex = DglMain.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> DglMain.Columns(Col1Value).Index Then Exit Sub

            Select Case DglMain.CurrentCell.RowIndex
                Case rowSubgroupType
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = " Select H.SubgroupType As Code, IfNull(Description, SubgroupType) As Name FROM SubGroupType H Where IfNull(IsActive,1)=1  "
                            mQry += " And SubGroupType = '" & ClsSchool.SubGroupType_Student & "' "
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowCity
                    If e.KeyCode = Keys.Insert Then
                        FOpenCityMaster()

                    ElseIf e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "Select CityCode, CityName From City Order By CityName"
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowCode
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "Select S.SubCode as Code, S.ManualCode, S.Name as [Name], C.CityName " &
                                    " From SubGroup S  " &
                                    " Left Join City C On S.CityCode = C.CityCode " &
                                    " Order By S.ManualCode "
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                            CType(DglMain.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True
                        End If
                    End If

                Case rowName
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "Select S.SubCode as Code, S.Name As [Name], C.CityName, S.SubgroupType as [A/c Type] 
                                    From SubGroup S 
                                    Left Join City C On S.CityCode = C.CityCode
                                    Order By S.Name "
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                            CType(DglMain.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True
                        End If
                    End If

                Case rowParent
                    If e.KeyCode = Keys.Insert Then
                        bNewMasterCode = FOpenPersonMaster(DglMain(Col1Value, rowSubgroupType).Tag)
                        DglMain(Col1Value, DglMain.CurrentCell.RowIndex).Tag = bNewMasterCode
                        DglMain(Col1Value, DglMain.CurrentCell.RowIndex).Value = AgL.XNull(AgL.Dman_Execute("Select Name From viewHelpSubgroup Where Code = '" & bNewMasterCode & "'", AgL.GCn).ExecuteScalar)

                        SendKeys.Send("{Enter}")
                    End If
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "select Sg.Code, Sg.Name From viewHelpSubgroup Sg Where Sg.Code <>'" & mSearchCode & "'"
                        If AgL.XNull(DtSubgroupTypeSettings.Rows(0)("FilterInclude_SubgroupTypeForMasterParty")) <> "" Then
                            mQry += " And CharIndex('+' || Sg.SubgroupType,'" & AgL.XNull(DtSubgroupTypeSettings.Rows(0)("FilterInclude_SubgroupTypeForMasterParty")) & "') > 0 "
                            mQry += " And CharIndex('-' || Sg.SubgroupType,'" & AgL.XNull(DtSubgroupTypeSettings.Rows(0)("FilterInclude_SubgroupTypeForMasterParty")) & "') <= 0 "
                        End If
                        mQry += " Order By Sg.Name"
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If

                Case rowArea
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "Select Code, Description From area Order By Description"
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If

                Case rowAcGroup
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "Select A.GroupCode As Code, A.GroupName As Name, A.GroupNature , A.Nature   FROM AcGroup A "
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value, 2) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If

                Case rowSite
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " Select Code, Name  FROM SiteMast Order By Name "
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If

                Case rowReligion
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Sg.SubCode As Code, Sg.Name From Subgroup Sg  With (NoLock) Where SubGroupType = '" & AgLibrary.ClsMain.agConstants.SubgroupType.Religion & "' Order By sg.Name "
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If

                Case rowCaste
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Sg.SubCode As Code, Sg.Name From Subgroup Sg  With (NoLock) Where SubGroupType = '" & AgLibrary.ClsMain.agConstants.SubgroupType.Caste & "' Order By sg.Name "
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If

                Case rowGender
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT 'Male' As Code, 'Male' As Name 
                                UNION ALL 
                                SELECT 'Female' As Code, 'Female' As Name 
                                UNION ALL 
                                SELECT 'Other' As Code, 'Other' As Name "
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If

                Case rowFeeHead
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " SELECT Sg.SubCode AS Code, Sg.Name
                                FROM Subgroup Sg With (NoLock)
                                Where Sg.SubgroupType = '" & ClsSchool.SubGroupType_FeeHead & "' 
                                And IfNull(Sg.Status,'Active') = 'Active'"
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If

                Case rowClass
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " SELECT Sg.SubCode AS Code, Sg.Name
                                FROM Subgroup Sg With (NoLock)
                                Where Sg.SubgroupType = '" & ClsSchool.SubGroupType_Class & "' 
                                And IfNull(Sg.Status,'Active') = 'Active'"
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If

                Case rowSection
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " SELECT Sg.SubCode AS Code, Sg.Name
                                FROM Subgroup Sg With (NoLock)
                                Where Sg.SubgroupType = '" & ClsSchool.SubGroupType_Section & "' 
                                And IfNull(Sg.Status,'Active') = 'Active'"
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If

                Case rowHouse
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " SELECT Sg.SubCode AS Code, Sg.Name
                                FROM Subgroup Sg With (NoLock)
                                Where Sg.SubgroupType = '" & ClsSchool.SubGroupType_House & "' 
                                And IfNull(Sg.Status,'Active') = 'Active'"
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmStudent_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dim I As Integer
        For I = 0 To DglMain.Rows.Count - 1
            DglMain.Item(Col1Value, I).Value = ""
            DglMain.Item(Col1Value, I).Tag = ""
            DglMain.Item(Col1BtnDetail, I).Tag = Nothing
            DglMain.Item(Col1BtnDetail, I) = New DataGridViewTextBoxCell
            DglMain(Col1BtnDetail, I).ReadOnly = True
        Next

        gStateCode = ""
        Dgl1.Rows.Clear()
        Dgl1.RowCount = 1
    End Sub

    Private Sub FrmStudent_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer
        Dim DtTemp As DataTable

        DglMain.EndEdit()

        passed = AgCL.AgCheckMandatory(Me)

        For I = 0 To DglMain.RowCount - 1
            If AgL.XNull(DglMain(Col1Mandatory, I).Value) <> "" And DglMain.Rows(I).Visible Then
                If AgL.XNull(DglMain(Col1Value, I).Value) = "" And AgL.XNull(DglMain(Col1BtnDetail, I).Value) = "" Then
                    MsgBox(DglMain(Col1Head, I).Value & " can not be blank.")
                    DglMain.CurrentCell = DglMain(Col1Value, I)
                    DglMain.Focus()
                    passed = False
                    Exit Sub
                End If
            End If
        Next

        mQry = " Select Count(*) From SubGroup 
                Where Replace(Replace(Replace(Replace(Name,' ',''),'.',''),'-',''),',','') = '" & AgL.XNull(DglMain.Item(Col1Value, rowName).Value).ToString.Replace(" ", "").Replace(".", "").Replace("-", "").Replace(",", "") & "' 
                And SubCode <> '" & mSearchCode & "'"
        If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar()) > 0 Then
            MsgBox("Party already exists.", MsgBoxStyle.Information)
            DglMain.CurrentCell = DglMain.Item(Col1Value, rowName)
            DglMain.Focus()
            passed = False
            Exit Sub
        End If

        If ValidatePanNo(DglMain.Item(Col1Value, rowPanNo).Value) = False Then
            DglMain.CurrentCell = DglMain(Col1Value, rowPanNo)
            DglMain.Focus()
            passed = False
            Exit Sub
        End If

        If ValidateAadharNo(DglMain.Item(Col1Value, rowAadharNo).Value) = False Then
            DglMain.CurrentCell = DglMain(Col1Value, rowAadharNo)
            DglMain.Focus()
            passed = False
            Exit Sub
        End If


        If ValidateEMailId(DglMain(Col1Value, rowEmail).Value) = False Then
            DglMain.CurrentCell = DglMain(Col1Value, rowEmail)
            DglMain.Focus()
            passed = False
            Exit Sub
        End If

        For I = 0 To Dgl1.Rows.Count - 1
            If AgL.XNull(Dgl1.Item(Col1Facility, I).Value) <> "" Then
                If AgL.XNull(Dgl1.Item(Col1StartDate, I).Value) = "" Then
                    Dgl1.Item(Col1StartDate, I).Value = AgL.PubStartDate
                    FGetChargeableDates(I)
                End If

                If AgL.XNull(Dgl1.Item(Col1EndDate, I).Value) = "" Then
                    Dgl1.Item(Col1EndDate, I).Value = AgL.PubEndDate
                    FGetChargeableDates(I)
                End If
            End If
        Next


        If AgL.XNull(DglMain.Item(Col1Value, rowSite).Tag) = "" Then DglMain(Col1Value, rowSite).Tag = AgL.PubSiteCode
        If AgL.XNull(DglMain.Item(Col1Value, rowShowAccountInOtherDivisions).Value) = "" Then DglMain.Item(Col1Value, rowShowAccountInOtherDivisions).Value = "YES"
        If AgL.XNull(DglMain.Item(Col1Value, rowShowAccountInOtherSites).Value) = "" Then DglMain.Item(Col1Value, rowShowAccountInOtherSites).Value = "YES"

        SetLastValues()
    End Sub
    Public Function ValidatePanNo(PANNo As String) As Boolean
        Dim mReason As String = ""
        ValidatePanNo = True

        If PANNo = "" Then Exit Function

        If Len(PANNo) <> 10 Then
            mReason = "PAN No. should be of 10 characters. Currently It is " & Len(PANNo).ToString
        Else
            If Not System.Text.RegularExpressions.Regex.IsMatch(PANNo, "[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}") Then
                mReason = "Some thing wrong in the given PAN No."
            End If
        End If

        If mReason <> "" Then
            MsgBox(mReason)
            ValidatePanNo = False
        End If
    End Function


    Public Function ValidateEMailId(ByVal EmailId As String) As Boolean
        If EmailId <> "" Then
            If InStr(EmailId, "@") > 1 And InStr(EmailId, "@") < EmailId.ToString.Length And InStr(EmailId, ".") > 1 And InStr(EmailId, ".") < EmailId.ToString.Length Then
                ValidateEMailId = True
            Else
                ValidateEMailId = False
                MsgBox(EmailId & " is not valid Email Id.")
            End If
        Else
            ValidateEMailId = True
        End If
    End Function

    Public Function ValidateAadharNo(AadharNo As String) As Boolean
        Dim mReason As String = ""

        ValidateAadharNo = True

        If AadharNo = "" Then Exit Function

        If Len(AadharNo) <> 12 Then
            mReason = "Aadhar No. should be of 12 characters. Currently It is " & Len(AadharNo).ToString
        Else
            If Not System.Text.RegularExpressions.Regex.IsMatch(AadharNo, "[0-9]{12}") Then
                mReason = "Some thing wrong in the given Aadhar No."
            End If
        End If

        If mReason <> "" Then
            MsgBox(mReason)
            ValidateAadharNo = False
        End If
    End Function
    Private Sub MnuImport_Click(sender As Object, e As EventArgs) Handles MnuImportFromExcel.Click, MnuBulkEdit.Click
        Select Case sender.name
            'Case MnuImportFromExcel.Name
            '    FImportFromExcel(ImportFor.Excel)

            'Case MnuBulkEdit.Name
            '    Dim FrmObj As New FrmStudentBulk()
            '    FrmObj.MdiParent = Me.MdiParent
            '    FrmObj.Show()
        End Select
    End Sub
    Private Sub FrmStudent_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        If AgL.XNull(DglMain.Item(Col1Value, rowLockText).Value) <> "" Then
            MsgBox(AgL.XNull(DglMain.Item(Col1Value, rowLockText).Value) & ", Can not modify")
            Passed = False
            Exit Sub
        End If

        If ClsMain.IsEntryLockedWithLockText("SubGroup", "SubCode", mSearchCode) = True Then
            Passed = False
            Exit Sub
        End If

        Passed = Not FGetRelationalData()
    End Sub

    Private Function FGetRelationalData() As Boolean
        Try
            mQry = " Select Count(*) From SaleInvoice Where SaleToParty = '" & mSearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                MsgBox(" Data Exists For Person " & DglMain(Col1Value, rowName).Value & " In Sale Invoice . Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If

            mQry = " Select Count(*) From PurchInvoice Where Vendor = '" & mSearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                MsgBox(" Data Exists For Person " & DglMain(Col1Value, rowName).Value & " In Purchase Invoice . Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If

            mQry = " Select Count(*) From Stock Where Subcode = '" & mSearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                MsgBox(" Data Exists For Person " & DglMain(Col1Value, rowName).Value & " In Stock. Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If

            mQry = " Select Count(*) From Ledger Where Subcode = '" & mSearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                MsgBox(" Data Exists For Person " & DglMain(Col1Value, rowName).Value & " In Ledger. Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If


            mQry = " Select Count(*) From LedgerHead Where Subcode = '" & mSearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                MsgBox(" Data Exists For Person " & DglMain(Col1Value, rowName).Value & " In LedgerHead. Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If


            mQry = " Select Count(*) From LedgerHeadDetail Where Subcode = '" & mSearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                MsgBox(" Data Exists For Person " & DglMain(Col1Value, rowName).Value & " In LedgerHeadDetail. Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If

            mQry = " Select Count(*) From StockHead Where Subcode = '" & mSearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                MsgBox(" Data Exists For Person " & DglMain(Col1Value, rowName).Value & " In StockHead. Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If


        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData")
            FGetRelationalData = True
        End Try
    End Function

    Private Sub DglMain_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles DglMain.EditingControl_Validating
        Dim mQry As String
        Dim DtTemp As DataTable

        If DglMain.CurrentCell.ColumnIndex = DglMain.Columns(Col1Value).Index Then
            If DglMain.Item(Col1Mandatory, DglMain.CurrentCell.RowIndex).Value <> "" Then
                If DglMain(Col1Value, DglMain.CurrentCell.RowIndex).Value = "" Then
                    MsgBox(DglMain(Col1Head, DglMain.CurrentCell.RowIndex).Value & " can not be blank.")
                    e.Cancel = True
                    Exit Sub
                End If
            End If

            Select Case DglMain.CurrentCell.RowIndex
                Case rowEmail
                    ValidateEMailId(DglMain.Item(Col1Value, rowEmail).Value)
                Case rowPanNo
                    ValidatePanNo(DglMain.Item(Col1Value, rowPanNo).Value)
                Case rowAadharNo
                    ValidateAadharNo(DglMain.Item(Col1Value, rowAadharNo).Value)
                Case rowSubgroupType
                    ApplyUISetting()
                    DglMain.CurrentCell = DglMain(Col1Value, rowName)
                    DglMain.Focus()
                Case rowAcGroup
                    If DglMain(Col1Value, DglMain.CurrentCell.RowIndex).Value.ToString.Trim = "" Or DglMain(Col1Value, DglMain.CurrentCell.RowIndex).Tag.Trim = "" Then
                        mGroupNature = ""
                        mNature = ""
                    Else
                        mQry = "Select GroupNature, Nature From AcGroup With (NoLock) Where GroupCode = '" & DglMain(Col1Value, DglMain.CurrentCell.RowIndex).Tag & "'"
                        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtTemp.Rows.Count > 0 Then
                            mGroupNature = AgL.XNull(DtTemp.Rows(0)("GroupNature"))
                            mNature = AgL.XNull(DtTemp.Rows(0)("Nature"))
                        End If
                    End If
                Case rowCity
                    Validate_City()
            End Select
        End If
    End Sub
    Private Sub Validate_City()
        Dim DtTemp As DataTable

        If DglMain(Col1Value, rowCity).Value <> "" Then
            mQry = "Select ManualCode From State Where Code = (Select State From City Where CityCode = '" & DglMain(Col1Value, rowCity).Tag & "')"
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTemp.Rows.Count > 0 Then
                gStateCode = AgL.XNull(DtTemp.Rows(0)("ManualCode"))
            Else
                MsgBox("State Code is not defined for selected city.")
            End If
        End If
    End Sub


    Private Sub FrmStudent_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Dim I As Integer
        For I = 0 To DglMain.Rows.Count - 1
            DglMain(Col1Head, I).Tag = Nothing
        Next
    End Sub
    Private Sub FrmStudent_BaseEvent_ApproveDeletion_InTrans(SearchCode As String, Conn As Object, Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = "Delete from SubgroupRegistration Where Subcode = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete from PersonDiscount Where Person = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete from PersonAddition Where Person = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete from PersonExtraDiscount Where Person = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete from SubgroupFacility Where Subcode = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Sub Dgl1_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles DglMain.CellBeginEdit
        Dim mRow As Integer
        mRow = DglMain.CurrentCell.RowIndex
        If DglMain.Columns(DglMain.CurrentCell.ColumnIndex).Name = Col1Value Then
            Select Case mRow
                Case rowShowAccountInOtherDivisions, rowShowAccountInOtherSites, rowBlockedTransactions
                    e.Cancel = True
            End Select
        End If
    End Sub
    Private Sub BtnAttachments_Click(sender As Object, e As EventArgs) Handles BtnAttachments.Click
        Dim FrmObj As New AgLibrary.FrmAttachmentViewer(AgL)
        FrmObj.LblDocNo.Text = "Party Name : " + DglMain(Col1Value, rowName).Value
        FrmObj.SearchCode = "SubGroup-" + mSearchCode
        FrmObj.TableName = "SubGroupAttachments"
        FrmObj.StartPosition = FormStartPosition.CenterParent
        FrmObj.ShowDialog()
        FrmObj.Dispose()
        FrmObj = Nothing
        SetAttachmentCaption()
    End Sub
    Private Sub SetAttachmentCaption()
        Dim AttachmentPath As String = PubAttachmentPath + "SubGroup-" + mSearchCode + "\"
        If Directory.Exists(AttachmentPath) Then
            Dim FileCount As Integer = Directory.GetFiles(AttachmentPath).Count
            If FileCount > 0 Then BtnAttachments.Text = FileCount.ToString + IIf(FileCount = 1, " Attachment", " Attachments") Else BtnAttachments.Text = "Attachments"
        Else
            BtnAttachments.Text = "Attachments"
        End If
    End Sub
    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles DglMain.KeyDown
        If DglMain.CurrentCell Is Nothing Then Exit Sub
        If ClsMain.IsSpecialKeyPressed(e) Then Exit Sub

        If Topctrl1.Mode.ToUpper <> "BROWSE" Then
            If DglMain.CurrentCell.ColumnIndex = DglMain.Columns(Col1Value).Index Then
                If e.KeyCode = Keys.Delete Then
                    DglMain(Col1Value, DglMain.CurrentCell.RowIndex).Value = ""
                    DglMain(Col1Value, DglMain.CurrentCell.RowIndex).Tag = ""
                End If

                Select Case DglMain.CurrentCell.RowIndex
                    Case rowCity
                        If e.KeyCode = Keys.Insert Then
                            FOpenCityMaster()
                        End If
                    Case rowShowAccountInOtherDivisions
                        If Not IsSpecialKeyPressed(e) Then
                            If e.KeyCode = Keys.N Then
                                DglMain.Item(Col1Value, rowShowAccountInOtherDivisions).Value = "NO"
                            Else
                                DglMain.Item(Col1Value, rowShowAccountInOtherDivisions).Value = "YES"
                            End If
                        End If
                    Case rowShowAccountInOtherSites
                        If Not IsSpecialKeyPressed(e) Then
                            If e.KeyCode = Keys.N Then
                                DglMain.Item(Col1Value, rowShowAccountInOtherSites).Value = "NO"
                            Else
                                DglMain.Item(Col1Value, rowShowAccountInOtherSites).Value = "YES"
                            End If
                        End If

                    Case rowBlockedTransactions
                        FHPGD_BlockedTransactions(DglMain(Col1Value, DglMain.CurrentCell.RowIndex).Tag, DglMain(Col1Value, DglMain.CurrentCell.RowIndex).Value)
                End Select
            End If
        End If
    End Sub
    Private Function FHPGD_ProcessScopeOfWork(ByRef bTag As String, ByRef bValue As String) As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtTemp As DataTable

        mQry = " Select 'o' As Tick, '" & Ncat.JobOrder & "' as Code, 'Job Order' As Name
                Union All  Select 'o' As Tick, '" & Ncat.JobReceive & "' as Code, 'Job Receive' As Name
                Union All  Select 'o' As Tick, '" & Ncat.JobInvoice & "' as Code, 'Job Invoice' As Name "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 300, 330, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            bTag = FRH_Multiple.FFetchData(1, "", "", ",", True)
            bValue = FRH_Multiple.FFetchData(2, "", "", ",", True)
        End If

        FRH_Multiple = Nothing
    End Function




    Private Sub FHPGD_BlockedTransactions(ByRef bTag As String, ByRef bValue As String)
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtTemp As DataTable

        mQry = " SELECT 'o' As Tick, Vt.NCat AS Code, Min(Vt.Description) as Name
                FROM Voucher_Type Vt With (NoLock)                
                Group By Vt.NCat "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 400, 450, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)


        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            bTag = FRH_Multiple.FFetchData(1, "", "", ",", True)
            bValue = FRH_Multiple.FFetchData(2, "", "", ",", True)
        End If
        FRH_Multiple = Nothing
    End Sub
    Private Sub Dgl1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DglMain.EditingControlShowing
        If FGetSettings(SettingFields.DefaultTextCaseInMasters, SettingType.General) = TextCase.Upper Then

            DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Upper
        ElseIf FGetSettings(SettingFields.DefaultTextCaseInMasters, SettingType.General) = TextCase.Lower Then
            DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Lower
        End If
    End Sub
    Private Sub ApplyUISetting()
        GetUISetting_WithDataTables(DglMain, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, ClsSchool.SubGroupType_Student, "", "", "", ClsMain.GridTypeConstants.VerticalGrid)
        GetUISetting_WithDataTables(Dgl1, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, ClsSchool.SubGroupType_Student, "", "", "", ClsMain.GridTypeConstants.HorizontalGrid)
    End Sub
    Private Sub DGL1_EditingControl_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If Topctrl1.Mode = "Browse" Then Exit Sub


            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Facility
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            mQry = " SELECT Sg.SubCode AS Code, Sg.Name
                                    FROM Subgroup Sg With (NoLock)
                                    Where Sg.SubgroupType = '" & ClsSchool.SubGroupType_Facility & "' 
                                    And IfNull(Sg.Status,'Active') = 'Active'"
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1FacilitySubHead
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            mQry = " SELECT Sg.SubCode AS Code, Sg.Name
                                    FROM Subgroup Sg With (NoLock)
                                    Where Sg.SubgroupType = '" & ClsSchool.SubGroupType_FacilityHead & "' 
                                    And IfNull(Sg.Status,'Active') = 'Active'"
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1ChargeableFrom, Col1ChargeableUpTo
                    Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).ReadOnly = True
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Facility, Col1FacilitySubHead, Col1StartDate, Col1EndDate
                    FGetChargeableDates(mRowIndex)
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FGetChargeableDates(RowIndex As Integer)
        Dim DtTemp As DataTable
        If AgL.XNull(Dgl1.Item(Col1StartDate, RowIndex).Value) <> "" Then
            mQry = " Select Max(DueDate) As ChargeableFrom
                            From FeeStructure 
                            Where DueDate <= " & AgL.Chk_Date(CDate(Dgl1.Item(Col1StartDate, RowIndex).Value)) & " 
                            And Fee = '" & Dgl1.Item(Col1Facility, RowIndex).Tag & "'
                            And IfNull(SubHead,'') = '" & Dgl1.Item(Col1FacilitySubHead, RowIndex).Tag & "'"
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            Dgl1.Item(Col1ChargeableFrom, RowIndex).Value = ClsMain.FormatDate(AgL.XNull(DtTemp.Rows(0)("ChargeableFrom")))
        End If

        If AgL.XNull(Dgl1.Item(Col1EndDate, RowIndex).Value) <> "" Then
            mQry = " Select Min(DueDate) As ChargeableUpto
                            From FeeStructure 
                            Where DueDate > " & AgL.Chk_Date(CDate(Dgl1.Item(Col1EndDate, RowIndex).Value)) & "
                            And Fee = '" & Dgl1.Item(Col1Facility, RowIndex).Tag & "'
                            And IfNull(SubHead,'') = '" & Dgl1.Item(Col1FacilitySubHead, RowIndex).Tag & "'"
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If AgL.XNull(DtTemp.Rows(0)("ChargeableUpto")) <> "" Then
                Dgl1.Item(Col1ChargeableUpTo, RowIndex).Value = ClsMain.FormatDate(DateAdd(DateInterval.Day, -1, CDate(AgL.XNull(DtTemp.Rows(0)("ChargeableUpto")))))
            Else
                Dgl1.Item(Col1ChargeableUpTo, RowIndex).Value = AgL.PubEndDate
            End If
        End If
    End Sub
    Private Sub DGL2_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub
End Class
