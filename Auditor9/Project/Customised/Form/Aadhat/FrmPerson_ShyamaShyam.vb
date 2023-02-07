Imports System.IO
Imports AgLibrary.ClsMain.agConstants
Imports System.Xml
Imports Customised.ClsMain
Imports System.ComponentModel
Imports System.Linq

Public Class FrmPerson_ShyamaShyam

    Inherits AgTemplate.TempMaster
    Dim mQry$ = ""
    Protected mGroupNature As String = "", mNature As String = ""

    Dim mSubgroupType$ = ""

    Dim mSubGroupNature As ESubgroupNature
    Friend WithEvents Pnl1 As Panel
    Dim mIsReturnValue As Boolean = False

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1BtnDetail As String = "Detail"
    Public Const Col1HeadOriginal As String = "Head Original"
    Public Const Col1LastValue As String = "Last Value"


    Public Const rowSubgroupType As Integer = 0
    Public Const rowCode As Integer = 1
    Public Const rowName As Integer = 2
    Public Const rowPrintingName As Integer = 3
    Public Const rowAddress As Integer = 4
    Public Const rowCity As Integer = 5
    Public Const rowPin As Integer = 6
    Public Const rowContactNo As Integer = 7
    Public Const rowMobile As Integer = 8
    Public Const rowEmail As Integer = 9
    Public Const rowDesignation As Integer = 10
    Public Const rowSite As Integer = 11
    Public Const rowAcGroup As Integer = 12
    Public Const rowContactPerson As Integer = 13
    Public Const rowSalesTaxGroup As Integer = 14
    Public Const rowSalesTaxGroupRegType As Integer = 15
    Public Const rowSalesTaxNo As Integer = 16
    Public Const rowHSN As Integer = 17
    Public Const rowPanNo As Integer = 18
    Public Const rowAadharNo As Integer = 19
    Public Const rowLicenseNo As Integer = 20
    Public Const rowParent As Integer = 21
    Public Const rowArea As Integer = 22
    Public Const rowAgent As Integer = 23
    Public Const rowTransporter As Integer = 24
    Public Const rowRelationshipExecutive As Integer = 25
    Public Const rowSalesRepresentative As Integer = 26
    Public Const rowSalesRepresentativeCommissionPer As Integer = 27
    Public Const rowInterestSlab As Integer = 28
    Public Const rowRateType As Integer = 29
    Public Const rowDistance As Integer = 30
    Public Const rowDiscount As Integer = 31
    Public Const rowExtraDiscount As Integer = 32
    Public Const rowAddition As Integer = 33
    Public Const rowCreditDays As Integer = 34
    Public Const rowCreditLimit As Integer = 35
    Public Const rowBankName As Integer = 36
    Public Const rowBankAccount As Integer = 37
    Public Const rowBankIFSC As Integer = 38
    Public Const rowShowAccountInOtherDivisions As Integer = 39
    Public Const rowShowAccountInOtherSites As Integer = 40
    Public Const rowWeekOffDays As Integer = 41
    Public Const rowRemarks As Integer = 42
    Public Const rowProcesses As Integer = 43
    Public Const rowChequeFormat As Integer = 44
    Public Const rowBlockedTransactions As Integer = 45
    Public Const rowLockText As Integer = 46
    Public Const rowGrade As Integer = 47
    Public Const rowTdsGroup As Integer = 48
    Public Const rowTdsCategory As Integer = 49
    Public Const rowReconciliationUpToDate As Integer = 50
    Public Const rowDivisionScopeOfWork As Integer = 51
    Public Const rowFairDiscountPer As Integer = 52
    Public Const rowPrevProcess As Integer = 53
    Public Const rowProcessScopeOfWork As Integer = 54
    Public Const rowCombinationOfProcesses As Integer = 55
    Public Const rowFirstProcessOfCombination As Integer = 56
    Public Const rowLastProcessOfCombination As Integer = 57
    Public Const rowStatus As Integer = 58


    'Public Const rowContactPerson As Integer = 15
    'Public Const rowSalesTaxNo As Integer = 16
    'Public Const rowHSN As Integer = 17
    'Public Const rowPanNo As Integer = 18
    'Public Const rowAadharNo As Integer = 19
    'Public Const rowParent As Integer = 20
    'Public Const rowArea As Integer = 21
    'Public Const rowAgent As Integer = 22
    'Public Const rowTransporter As Integer = 23
    'Public Const rowRelationshipExecutive As Integer = 24
    'Public Const rowSalesRepresentative As Integer = 25
    'Public Const rowSalesRepresentativeCommissionPer As Integer = 26
    'Public Const rowInterestSlab As Integer = 27
    'Public Const rowRateType As Integer = 28
    'Public Const rowDistance As Integer = 29
    'Public Const rowDiscount As Integer = 30
    'Public Const rowCreditDays As Integer = 31
    'Public Const rowCreditLimit As Integer = 32
    'Public Const rowBankName As Integer = 33
    'Public Const rowBankAccount As Integer = 34
    'Public Const rowBankIFSC As Integer = 35
    'Public Const rowShowAccountInOtherDivisions As Integer = 36
    'Public Const rowShowAccountInOtherSites As Integer = 37
    'Public Const rowWeekOffDays As Integer = 38
    'Public Const rowRemarks As Integer = 39
    'Public Const rowProcesses As Integer = 40
    'Public Const rowChequeFormat As Integer = 41
    'Public Const rowBlockedTransactions As Integer = 42
    'Public Const rowLockText As Integer = 43
    'Public Const rowGrade As Integer = 44
    'Public Const rowScopeOfWork As Integer = 45



    Public Const hcLicenseNo As String = "License No."
    Public Const hcHsn As String = "HSN"
    Public Const hcBankName As String = "Bank Name"
    Public Const hcBankAccount As String = "Bank Account No."
    Public Const hcBankIFSC As String = "Bank IFSC"
    Public Const hcShowAccountInOtherDivisions As String = "Show A/c In Other Divisions"
    Public Const hcShowAccountInOtherSites As String = "Show A/c In Other Sites"
    Public Const hcWeekOffDays As String = "Week Off Days"
    Public Const hcRelationshipExecutive As String = "Relationship Executive"
    Public Const hcProcesses As String = "Processes"
    Public Const hcChequeFormat As String = "Cheque Format"
    Public Const hcSalesRepresentative As String = "Sales Representative"
    Public Const hcSalesRepresentativeCommissionPer As String = "Sales Representative Commision %"
    Public Const hcBlockedTransactions As String = "Blocked Transactions"
    Public Const hcLockText As String = "Lock Text"
    Public Const hcGrade As String = "Grade"
    Public Const hcTdsGroup As String = "Tds Group"
    Public Const hcTdsCategory As String = "Tds Category"
    Public Const hcReconciliationUpToDate As String = "Reconciliation Upto Date"
    Public Const hcDivisionScopeOfWork As String = "Scope Of Work"
    Public Const hcFairDiscountPer As String = "Fair Discount %"
    Public Const hcPrevProcess As String = "Prev Process"
    Public Const hcProcessScopeOfWork As String = "Process Scope Of Work"
    Public Const hcCombinationOfProcesses As String = "Combination Of Processes"
    Public Const hcFirstProcessOfCombination As String = "First Process Of Combination"
    Public Const hcLastProcessOfCombination As String = "Last Process Of Combination"
    Public Const hcStatus As String = "Status"




    Friend WithEvents MnuOptions As ContextMenuStrip
    Private components As System.ComponentModel.IContainer
    Friend WithEvents MnuImportFromExcel As ToolStripMenuItem
    Friend WithEvents MnuImportFromTally As ToolStripMenuItem
    Public WithEvents OFDMain As OpenFileDialog
    Friend WithEvents MnuBulkEdit As ToolStripMenuItem
    Friend WithEvents MnuImportFromDos As ToolStripMenuItem
    Dim gStateCode As String
    Protected WithEvents BtnAttachments As Button
    Dim DtSubgroupTypeSettings As DataTable

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
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.MnuOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuImportFromExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportFromDos = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportFromTally = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuBulkEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.OFDMain = New System.Windows.Forms.OpenFileDialog()
        Me.BtnAttachments = New System.Windows.Forms.Button()
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
        'Pnl1
        '
        Me.Pnl1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl1.Location = New System.Drawing.Point(14, 47)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(948, 511)
        Me.Pnl1.TabIndex = 15
        '
        'MnuOptions
        '
        Me.MnuOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuImportFromExcel, Me.MnuImportFromDos, Me.MnuImportFromTally, Me.MnuBulkEdit})
        Me.MnuOptions.Name = "MnuOptions"
        Me.MnuOptions.Size = New System.Drawing.Size(171, 114)
        '
        'MnuImportFromExcel
        '
        Me.MnuImportFromExcel.Name = "MnuImportFromExcel"
        Me.MnuImportFromExcel.Size = New System.Drawing.Size(170, 22)
        Me.MnuImportFromExcel.Text = "Import From Excel"
        '
        'MnuImportFromDos
        '
        Me.MnuImportFromDos.Name = "MnuImportFromDos"
        Me.MnuImportFromDos.Size = New System.Drawing.Size(170, 22)
        Me.MnuImportFromDos.Text = "Import From Dos"
        '
        'MnuImportFromTally
        '
        Me.MnuImportFromTally.Name = "MnuImportFromTally"
        Me.MnuImportFromTally.Size = New System.Drawing.Size(170, 22)
        Me.MnuImportFromTally.Text = "Import From Tally"
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
        'FrmPerson
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(974, 612)
        Me.ContextMenuStrip = Me.MnuOptions
        Me.Controls.Add(Me.BtnAttachments)
        Me.Controls.Add(Me.Pnl1)
        Me.MaximizeBox = True
        Me.Name = "FrmPerson"
        Me.Text = "Buyer Master"
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.BtnAttachments, 0)
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
#End Region

    Private Sub FrmShade_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        AgL.PubFindQry = " SELECT H.SubCode AS SearchCode,  H.Name AS [Name], 
                         H.ManualCode As [Code], H.SubgroupType as [Subgroup Type], H.Address, C.CityName As [City Name], 
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
        If mSubgroupType <> "" Then
            AgL.PubFindQry += " And   H.SubgroupType = '" & mSubgroupType & "' "
        Else
            AgL.PubFindQry += " And   ST.IsCustomUI=0 "
        End If
        'AgL.PubFindQry += " Order By H.Name "


        AgL.PubFindQryOrdBy = "[Name]"
    End Sub

    Private Sub FrmShade_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "SubGroup"
        MainLineTableCsv = "SubgroupSiteDivisionDetail"

        PrimaryField = "SubCode"

        If AgL.PubDtEnviro.Columns.Contains("AskSubGroupTypeInPersonMaster") Then
            If AgL.VNull(AgL.PubDtEnviro.Rows(0)("AskSubGroupTypeInPersonMaster")) = True Then
                FShowSubGroupTypeHelp()
            End If
        End If
    End Sub

    Private Sub ApplySubgroupTypeSetting(SubgroupType As String)
        Dim mQry As String
        Dim DsTemp As DataSet
        Dim DtTemp As DataTable
        Dim I As Integer, J As Integer
        Dim mDgl1RowCount As Integer




        Try

            For I = 0 To Dgl1.Rows.Count - 1
                Dgl1.Rows(I).Visible = False
            Next


            mQry = "Select H.*
                    from EntryHeaderUISetting H                   
                    Where EntryName='" & Me.Name & "' And NCat = '" & SubgroupType & "' And GridName ='" & Dgl1.Name & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 0 To Dgl1.Rows.Count - 1
                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = Dgl1.Item(Col1HeadOriginal, J).Value Then
                            Dgl1.Rows(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
                            If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDgl1RowCount += 1
                            Dgl1.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
                            If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
                                Dgl1.Item(Col1Head, J).Value = AgL.XNull(DtTemp.Rows(I)("Caption"))
                            End If
                        End If
                    Next
                Next
            End If
            If mDgl1RowCount = 0 Then Dgl1.Visible = False Else Dgl1.Visible = True

            If AgL.StrCmp(Dgl1(Col1Value, rowSubgroupType).Tag, AgLibrary.ClsMain.agConstants.SubgroupType.Employee) Then
                mQry = "Select Count(*) from SiteMast"
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 1 Then
                    Dgl1.Rows(rowSite).Visible = True
                Else
                    Dgl1.Rows(rowSite).Visible = False
                End If
            End If



            mQry = "Select S.*, A.GroupName As AcGroupName, A.GroupNature, A.Nature 
                    from subgroupTypeSetting S
                    Left Join AcGroup A On S.AcGroupCode = A.GroupCode
                   Where SubgroupType = '" & SubgroupType & "' "
            DsTemp = AgL.FillData(mQry, AgL.GCn)
            DtSubgroupTypeSettings = DsTemp.Tables(0)
            With DsTemp.Tables(0)
                If DsTemp.Tables(0).Rows.Count > 0 Then
                    'Dgl1.Rows(rowContactPerson).Visible = CBool(AgL.XNull(.Rows(0)("IsVisible_ContactPerson")))
                    'Dgl1.Rows(rowPanNo).Visible = CBool(AgL.XNull(.Rows(0)("IsVisible_PanNo")))
                    'Dgl1.Rows(rowAadharNo).Visible = CBool(AgL.XNull(.Rows(0)("IsVisible_AadharNo")))
                    'Dgl1.Rows(rowSalesTaxNo).Visible = CBool(AgL.XNull(.Rows(0)("IsVisible_SalesTaxNo")))
                    'Dgl1.Rows(rowParent).Visible = CBool(AgL.XNull(.Rows(0)("IsVisible_Parent")))
                    'Dgl1.Rows(rowTransporter).Visible = CBool(AgL.XNull(.Rows(0)("IsVisible_Transporter")))
                    'Dgl1.Rows(rowAgent).Visible = CBool(AgL.XNull(.Rows(0)("IsVisible_Agent")))
                    'Dgl1.Rows(rowDiscount).Visible = CBool(AgL.XNull(.Rows(0)("IsVisible_Discount")))
                    'Dgl1.Rows(rowInterestSlab).Visible = CBool(AgL.XNull(.Rows(0)("IsVisible_InterestSlab")))
                    'Dgl1.Rows(rowCreditDays).Visible = CBool(AgL.XNull(.Rows(0)("IsVisible_CreditLimit")))
                    'Dgl1.Rows(rowCreditLimit).Visible = CBool(AgL.XNull(.Rows(0)("IsVisible_CreditLimit")))
                    'If SubgroupType = AgLibrary.ClsMain.agConstants.SubgroupType.Customer Then
                    '    Dgl1.Rows(rowRateType).Visible = AgL.IsFeatureApplicable_RateType
                    '    Dgl1.Rows(rowArea).Visible = AgL.IsFeatureApplicable_Area
                    'Else
                    '    Dgl1.Rows(rowRateType).Visible = False
                    '    Dgl1.Rows(rowArea).Visible = False
                    'End If

                    'If AgL.XNull(.Rows(0)("Caption_Parent")) <> "" Then
                    '    Dgl1.Item(Col1Head, rowParent).Value = AgL.XNull(.Rows(0)("Caption_Parent"))
                    'End If


                    If AgL.VNull(DtSubgroupTypeSettings.Rows(0)("PersonCanHaveDivisionWiseAgentYn")) = True Or AgL.VNull(DtSubgroupTypeSettings.Rows(0)("PersonCanHaveSiteWiseAgentYn")) = True Then
                        Dgl1.Item(Col1BtnDetail, rowAgent) = New DataGridViewButtonCell
                        Dgl1(Col1BtnDetail, rowAgent).ReadOnly = False
                    Else
                        Dgl1.Item(Col1BtnDetail, rowAgent) = New DataGridViewTextBoxCell
                        Dgl1(Col1BtnDetail, rowAgent).ReadOnly = True
                    End If


                    If AgL.VNull(DtSubgroupTypeSettings.Rows(0)("PersonCanHaveDivisionWiseTransporterYn")) = True Or AgL.VNull(DtSubgroupTypeSettings.Rows(0)("PersonCanHaveSiteWiseTransporterYn")) = True Then
                        Dgl1.Item(Col1BtnDetail, rowTransporter) = New DataGridViewButtonCell
                        Dgl1(Col1BtnDetail, rowTransporter).ReadOnly = False
                    Else
                        Dgl1.Item(Col1BtnDetail, rowTransporter) = New DataGridViewTextBoxCell
                        Dgl1(Col1BtnDetail, rowTransporter).ReadOnly = True
                    End If

                    If AgL.VNull(DtSubgroupTypeSettings.Rows(0)("PersonCanHaveDivisionWiseRateTypeYn")) = True Or AgL.VNull(DtSubgroupTypeSettings.Rows(0)("PersonCanHaveSiteWiseRateTypeYn")) = True Then
                        Dgl1.Item(Col1BtnDetail, rowRateType) = New DataGridViewButtonCell
                        Dgl1(Col1BtnDetail, rowRateType).ReadOnly = False
                    Else
                        Dgl1.Item(Col1BtnDetail, rowRateType) = New DataGridViewTextBoxCell
                        Dgl1(Col1BtnDetail, rowRateType).ReadOnly = True
                    End If

                    If AgL.VNull(DtSubgroupTypeSettings.Rows(0)("PersonCanHaveItemGroupWiseInterestSlabYn")) = True Or AgL.VNull(DtSubgroupTypeSettings.Rows(0)("PersonCanHaveItemCategoryWiseInterestSlabYn")) = True Then
                        Dgl1.Item(Col1BtnDetail, rowInterestSlab) = New DataGridViewButtonCell
                        Dgl1(Col1BtnDetail, rowInterestSlab).ReadOnly = False
                    Else
                        Dgl1.Item(Col1BtnDetail, rowInterestSlab) = New DataGridViewTextBoxCell
                        Dgl1(Col1BtnDetail, rowInterestSlab).ReadOnly = True
                    End If

                    If AgL.VNull(DtSubgroupTypeSettings.Rows(0)("PersonCanHaveItemGroupWiseDiscountYn")) = True Or AgL.VNull(DtSubgroupTypeSettings.Rows(0)("PersonCanHaveItemCategoryWiseDiscountYn")) = True Then
                        Dgl1.Item(Col1BtnDetail, rowDiscount) = New DataGridViewButtonCell
                        Dgl1(Col1BtnDetail, rowDiscount).ReadOnly = False
                    Else
                        Dgl1.Item(Col1BtnDetail, rowDiscount) = New DataGridViewTextBoxCell
                        Dgl1(Col1BtnDetail, rowDiscount).ReadOnly = True
                    End If

                    Dgl1.Item(Col1BtnDetail, rowExtraDiscount) = New DataGridViewButtonCell
                    Dgl1(Col1BtnDetail, rowExtraDiscount).ReadOnly = False



                    Dgl1(Col1Value, rowSalesTaxGroup).Tag = AgL.XNull(DtSubgroupTypeSettings.Rows(0)("Default_SalesTaxGroupPerson"))
                    Dgl1(Col1Value, rowSalesTaxGroup).Value = AgL.XNull(DtSubgroupTypeSettings.Rows(0)("Default_SalesTaxGroupPerson"))
                    Dgl1(Col1Value, rowAcGroup).Tag = AgL.XNull(DtSubgroupTypeSettings.Rows(0)("AcGroupCode"))
                    Dgl1(Col1Value, rowAcGroup).Value = AgL.XNull(DtSubgroupTypeSettings.Rows(0)("AcGroupName"))
                    mGroupNature = AgL.XNull(.Rows(0)("GroupNature"))
                    mNature = AgL.XNull(.Rows(0)("Nature"))

                End If

            End With
        Catch ex As Exception
            MsgBox(ex.Message & " [ApplySubgroupTypeSetting]")
        End Try
    End Sub
    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, AgL.PubSiteCode, "", Dgl1(Col1Value, rowSubgroupType).Tag, "", "", "")
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
        If mSubgroupType <> "" Then
            mQry += " And   S.SubgroupType = '" & mSubgroupType & "' "
        Else
            mQry += " And   ST.IsCustomUI=0 "
        End If

        mQry += " Order by S.Name "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet
        Dim DtTemp As DataTable
        Dim DrTemp As DataRow() = Nothing
        Dim DtSiteDivisionCount As DataTable
        Dim I As Integer

        mQry = "Select S.*, P.Name as ParentName, C.CityName, State.ManualCode as StateCode, 
                    A.Description as AreaName, Ins.Description as InterestSlabName , AcGroup.GroupName, Site.Name as SiteName,
                    Designation.Code as DesignationCode, Designation.Description  as DesignationName,
                    CF.Description as ChequeFormatName, Tg.Description As TdsGroupDesc, Tc.Description As TdsCategoryDesc
                    From SubGroup S 
                    Left Join viewHelpSubgroup P on S.Parent = P.Code
                    Left Join City C On S.CityCode = C.CityCode   
                    Left Join State On C.State = State.Code
                    Left Join AcGroup On S.GroupCode = AcGroup.GroupCode
                    Left Join Area A On S.Area = A.Code
                    Left Join InterestSlab InS on S.InterestSlab = Ins.Code
                    LEFT JOIN TdsGroup Tg On S.TdsGroup = Tg.Code
                    LEFT JOIN TdsCategory Tc On S.TdsCategory = Tc.Code
                    Left Join SiteMast Site On S.Site_Code = Site.Code
                    Left Join HRM_Employee Emp On S.Subcode = Emp.Subcode
                    Left Join HRM_Designation Designation On Emp.Designation = Designation.Code
                    Left Join ChequeFormat CF On S.ChequeFormat = CF.Code
                    Where S.SubCode='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                Dgl1(Col1Value, rowSubgroupType).Tag = AgL.XNull(.Rows(0)("SubgroupType"))
                Dgl1(Col1Value, rowSubgroupType).Value = AgL.XNull(.Rows(0)("SubgroupType"))
                ApplySubgroupTypeSetting(Dgl1(Col1Value, rowSubgroupType).Value)
                mInternalCode = AgL.XNull(.Rows(0)("SubCode"))
                Dgl1(Col1Value, rowCode).Value = AgL.XNull(.Rows(0)("ManualCode"))
                Dgl1(Col1Value, rowName).Value = AgL.XNull(.Rows(0)("Name"))
                Dgl1(Col1Value, rowPrintingName).Value = IIf(AgL.XNull(.Rows(0)("DispName")) = AgL.XNull(.Rows(0)("Name")), "", AgL.XNull(.Rows(0)("DispName")))
                Dgl1(Col1Value, rowAcGroup).Tag = AgL.XNull(.Rows(0)("GroupCode"))
                Dgl1(Col1Value, rowAcGroup).Value = AgL.XNull(.Rows(0)("GroupName"))
                Dgl1(Col1Value, rowAddress).Value = AgL.XNull(.Rows(0)("Address"))
                Dgl1(Col1Value, rowCity).Tag = AgL.XNull(.Rows(0)("CityCode"))
                Dgl1(Col1Value, rowCity).Value = AgL.XNull(.Rows(0)("CityName"))
                gStateCode = AgL.XNull(.Rows(0)("StateCode"))
                Dgl1(Col1Value, rowPin).Value = AgL.XNull(.Rows(0)("PIN"))
                Dgl1(Col1Value, rowHSN).Value = AgL.XNull(.Rows(0)("HSN"))
                Dgl1(Col1Value, rowSite).Tag = AgL.XNull(.Rows(0)("Site_Code"))
                Dgl1(Col1Value, rowSite).Value = AgL.XNull(.Rows(0)("SiteName"))
                Dgl1(Col1Value, rowDesignation).Tag = AgL.XNull(.Rows(0)("DesignationCode"))
                Dgl1(Col1Value, rowDesignation).Value = AgL.XNull(.Rows(0)("DesignationName"))
                Dgl1(Col1Value, rowMobile).Value = AgL.XNull(.Rows(0)("Mobile"))
                Dgl1(Col1Value, rowContactNo).Value = AgL.XNull(.Rows(0)("Phone"))
                Dgl1(Col1Value, rowCreditDays).Value = AgL.XNull(.Rows(0)("CreditDays"))
                Dgl1(Col1Value, rowCreditLimit).Value = AgL.XNull(.Rows(0)("CreditLimit"))
                Dgl1(Col1Value, rowEmail).Value = AgL.XNull(.Rows(0)("EMail"))
                Dgl1(Col1Value, rowSalesTaxGroup).Tag = AgL.XNull(.Rows(0)("SalesTaxPostingGroup"))
                Dgl1(Col1Value, rowSalesTaxGroup).Value = AgL.XNull(.Rows(0)("SalesTaxPostingGroup"))
                Dgl1(Col1Value, rowSalesTaxGroupRegType).Tag = AgL.XNull(.Rows(0)("SalesTaxGroupRegType"))
                Dgl1(Col1Value, rowSalesTaxGroupRegType).Value = AgL.XNull(.Rows(0)("SalesTaxGroupRegType"))
                Dgl1(Col1Value, rowWeekOffDays).Value = AgL.XNull(.Rows(0)("WeekOffDays"))
                Dgl1.Item(Col1Value, rowShowAccountInOtherDivisions).Value = IIf((.Rows(0)("ShowAccountInOtherDivisions")), "Yes", "No")
                Dgl1.Item(Col1Value, rowShowAccountInOtherSites).Value = IIf((.Rows(0)("ShowAccountInOtherSites")), "Yes", "No")
                Dgl1(Col1Value, rowRemarks).Value = AgL.XNull(.Rows(0)("Remarks"))
                Dgl1(Col1Value, rowLockText).Value = AgL.XNull(.Rows(0)("LockText"))
                Dgl1(Col1Value, rowGrade).Value = AgL.XNull(.Rows(0)("Grade"))
                Dgl1(Col1Value, rowTdsGroup).Tag = AgL.XNull(.Rows(0)("TdsGroup"))
                Dgl1(Col1Value, rowTdsGroup).Value = AgL.XNull(.Rows(0)("TdsGroupDesc"))
                Dgl1(Col1Value, rowTdsCategory).Tag = AgL.XNull(.Rows(0)("TdsCategory"))
                Dgl1(Col1Value, rowTdsCategory).Value = AgL.XNull(.Rows(0)("TdsCategoryDesc"))
                Dgl1(Col1Value, rowStatus).Value = AgL.XNull(.Rows(0)("WStatus"))
                Dgl1(Col1Value, rowStatus).Tag = AgL.XNull(.Rows(0)("WStatus"))


                Dgl1(Col1Value, rowReconciliationUpToDate).Value = ClsMain.FormatDate(AgL.XNull(.Rows(0)("ReconciliationUpToDate")))
                Dgl1(Col1Value, rowFairDiscountPer).Value = AgL.XNull(.Rows(0)("FairDiscountPer"))
                mNature = AgL.XNull(.Rows(0)("Nature"))
                DisplayFieldsBasedOnNature(mNature)
                mGroupNature = AgL.XNull(.Rows(0)("GroupNature"))

                Dgl1.Item(Col1Value, rowChequeFormat).Tag = AgL.XNull(.Rows(0)("ChequeFormat"))
                Dgl1.Item(Col1Value, rowChequeFormat).Value = AgL.XNull(.Rows(0)("ChequeFormatName"))

                Dgl1.Item(Col1Value, rowContactPerson).Value = AgL.XNull(.Rows(0)("ContactPerson"))
                Dgl1.Item(Col1Value, rowParent).Tag = AgL.XNull(.Rows(0)("Parent"))
                Dgl1.Item(Col1Value, rowParent).Value = AgL.XNull(.Rows(0)("ParentName"))
                Dgl1.Item(Col1Value, rowArea).Tag = AgL.XNull(.Rows(0)("Area"))
                Dgl1.Item(Col1Value, rowArea).Value = AgL.XNull(.Rows(0)("AreaName"))
                Dgl1.Item(Col1Value, rowInterestSlab).Tag = AgL.XNull(.Rows(0)("InterestSlab"))
                Dgl1.Item(Col1Value, rowInterestSlab).Value = AgL.XNull(.Rows(0)("InterestSlabName"))
            End If
        End With

        mQry = " Select *, PSg.Name As PrevProcessName, FSg.Name As FirstProcessOfCombinationName, 
                LSg.Name As LastProcessOfCombinationName  
                From ProcessDetail L
                LEFT JOIN SubGroup PSg On L.PrevProcess = PSg.SubCode
                LEFT JOIN SubGroup FSg On L.FirstProcessOfCombination = FSg.SubCode
                LEFT JOIN SubGroup LSg On L.LastProcessOfCombination = LSg.SubCode
                Where L.SubCode = '" & mSearchCode & "'"
        Dim DtProcessDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtProcessDetail.Rows.Count > 0 Then
            Dgl1(Col1Value, rowPrevProcess).Tag = AgL.XNull(DtProcessDetail.Rows(0)("PrevProcess"))
            Dgl1(Col1Value, rowPrevProcess).Value = AgL.XNull(DtProcessDetail.Rows(0)("PrevProcessName"))
            Dgl1(Col1Value, rowFirstProcessOfCombination).Tag = AgL.XNull(DtProcessDetail.Rows(0)("FirstProcessOfCombination"))
            Dgl1(Col1Value, rowFirstProcessOfCombination).Value = AgL.XNull(DtProcessDetail.Rows(0)("FirstProcessOfCombinationName"))
            Dgl1(Col1Value, rowLastProcessOfCombination).Tag = AgL.XNull(DtProcessDetail.Rows(0)("LastProcessOfCombination"))
            Dgl1(Col1Value, rowLastProcessOfCombination).Value = AgL.XNull(DtProcessDetail.Rows(0)("LastProcessOfCombinationName"))

            If AgL.XNull(DtProcessDetail.Rows(0)("ScopeOfWork")) <> "" Then
                Dim bScopeOfWork As String = DtProcessDetail.Rows(0)("ScopeOfWork")
                If DtProcessDetail.Rows(0)("ScopeOfWork").ToString.Contains(Ncat.JobOrder) Then
                    bScopeOfWork = bScopeOfWork.Replace(Ncat.JobOrder, "Job Order")
                End If
                If DtProcessDetail.Rows(0)("ScopeOfWork").ToString.Contains(Ncat.JobReceive) Then
                    bScopeOfWork = bScopeOfWork.Replace(Ncat.JobReceive, "Job Receive")
                End If
                If DtProcessDetail.Rows(0)("ScopeOfWork").ToString.Contains(Ncat.JobInvoice) Then
                    bScopeOfWork = bScopeOfWork.Replace(Ncat.JobInvoice, "Job Invoice")
                End If
                Dgl1.Item(Col1Value, rowProcessScopeOfWork).Tag = DtProcessDetail.Rows(0)("ScopeOfWork")
                Dgl1.Item(Col1Value, rowProcessScopeOfWork).Value = bScopeOfWork
            End If
            If AgL.XNull(DtProcessDetail.Rows(0)("CombinationOfProcesses")) <> "" Then
                mQry = "Select * From SubGroup Where SubCode In ('" & AgL.XNull(DtProcessDetail.Rows(0)("CombinationOfProcesses")).ToString.Replace(",", "','") & "') "
                Dim DtCombinationOfProcesses As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                For K As Integer = 0 To DtCombinationOfProcesses.Rows.Count - 1
                    If Dgl1.Item(Col1Value, rowCombinationOfProcesses).Tag <> "" Then Dgl1.Item(Col1Value, rowCombinationOfProcesses).Tag += ","
                    Dgl1.Item(Col1Value, rowCombinationOfProcesses).Tag += DtCombinationOfProcesses.Rows(K)("SubCode")

                    If Dgl1.Item(Col1Value, rowCombinationOfProcesses).Value <> "" Then Dgl1.Item(Col1Value, rowCombinationOfProcesses).Value += ","
                    Dgl1.Item(Col1Value, rowCombinationOfProcesses).Value += DtCombinationOfProcesses.Rows(K)("Name")
                Next
            End If
        End If



        mQry = "Select * From SubgroupRegistration where Subcode = '" & mSearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        If DsTemp.Tables(0).Rows.Count > 0 Then
            For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                If UCase(AgL.XNull(DsTemp.Tables(0).Rows(I)("RegistrationType"))) = SubgroupRegistrationType.SalesTaxNo.ToUpper Then
                    Dgl1.Item(Col1Value, rowSalesTaxNo).Value = AgL.XNull(DsTemp.Tables(0).Rows(I)("RegistrationNo"))
                ElseIf UCase(AgL.XNull(DsTemp.Tables(0).Rows(I)("RegistrationType"))) = SubgroupRegistrationType.PanNo.ToUpper Then
                    Dgl1.Item(Col1Value, rowPanNo).Value = AgL.XNull(DsTemp.Tables(0).Rows(I)("RegistrationNo"))
                ElseIf UCase(AgL.XNull(DsTemp.Tables(0).Rows(I)("RegistrationType"))) = SubgroupRegistrationType.AadharNo.ToUpper Then
                    Dgl1.Item(Col1Value, rowAadharNo).Value = AgL.XNull(DsTemp.Tables(0).Rows(I)("RegistrationNo"))
                ElseIf UCase(AgL.XNull(DsTemp.Tables(0).Rows(I)("RegistrationType"))) = SubgroupRegistrationType.LicenseNo.ToUpper Then
                    Dgl1.Item(Col1Value, rowLicenseNo).Value = AgL.XNull(DsTemp.Tables(0).Rows(I)("RegistrationNo"))
                End If
            Next
        End If


        mQry = "Select * From SubgroupBankAccount where Subcode = '" & mSearchCode & "' And Sr=0 "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        If DsTemp.Tables(0).Rows.Count > 0 Then
            Dgl1.Item(Col1Value, rowBankAccount).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("BankAccount"))
            Dgl1.Item(Col1Value, rowBankName).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("BankName"))
            Dgl1.Item(Col1Value, rowBankIFSC).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("BankIFSC"))
        End If


        mQry = "Select DiscountPer from PersonDiscount With (NoLock) Where Person = '" & mSearchCode & "' And ItemCategory Is Null And ItemGroup Is Null "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        If DsTemp.Tables(0).Rows.Count > 0 Then
            Dgl1.Item(Col1Value, rowDiscount).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("DiscountPer"))
        End If

        mQry = "Select AdditionPer from PersonAddition With (NoLock) Where Person = '" & mSearchCode & "' And ItemCategory Is Null And ItemGroup Is Null And Process Is Null "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        If DsTemp.Tables(0).Rows.Count > 0 Then
            Dgl1.Item(Col1Value, rowAddition).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("AdditionPer"))
        End If

        mQry = "Select ExtraDiscountPer from PersonExtraDiscount With (NoLock) Where Person = '" & mSearchCode & "' And ItemCategory Is Null And ItemGroup Is Null And Process Is Null "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        If DsTemp.Tables(0).Rows.Count > 0 Then
            Dgl1.Item(Col1Value, rowExtraDiscount).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("ExtraDiscountPer"))
        End If


        mQry = "Select H.Agent, H.Transporter, A.Name as AgentName, T.Name as TransporterName, 
                H.RelationShipExecutive, R.Name as RelationShipExecutiveName, 
                H.SalesRepresentative, S.Name as SalesRepresentativeName, 
                H.SalesRepresentativeCommissionPer,
                H.RateType, RT.Description as RateTypeName, H.Distance 
                From SubgroupSiteDivisionDetail H
                Left Join viewHelpSubgroup A On H.Agent = A.Code 
                Left Join viewHelpSubgroup T On H.Transporter = T.Code 
                Left Join viewHelpSubgroup R On H.RelationshipExecutive = R.Code 
                Left Join viewHelpSubgroup S On H.SalesRepresentative = S.Code 
                Left Join RateType RT On H.RateType = RT.Code
                Where H.Subcode = '" & mSearchCode & "' And H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code ='" & AgL.PubSiteCode & "' "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        If DsTemp.Tables(0).Rows.Count > 0 Then
            Dgl1.Item(Col1Value, rowAgent).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("AgentName"))
            Dgl1.Item(Col1Value, rowAgent).Tag = AgL.XNull(DsTemp.Tables(0).Rows(0)("Agent"))
            Dgl1.Item(Col1Value, rowTransporter).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("TransporterName"))
            Dgl1.Item(Col1Value, rowTransporter).Tag = AgL.XNull(DsTemp.Tables(0).Rows(0)("Transporter"))
            Dgl1.Item(Col1Value, rowRelationshipExecutive).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("RelationShipExecutiveName"))
            Dgl1.Item(Col1Value, rowRelationshipExecutive).Tag = AgL.XNull(DsTemp.Tables(0).Rows(0)("RelationShipExecutive"))
            Dgl1.Item(Col1Value, rowSalesRepresentative).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("SalesRepresentativeName"))
            Dgl1.Item(Col1Value, rowSalesRepresentative).Tag = AgL.XNull(DsTemp.Tables(0).Rows(0)("SalesRepresentative"))
            Dgl1.Item(Col1Value, rowSalesRepresentativeCommissionPer).Value = AgL.VNull(DsTemp.Tables(0).Rows(0)("SalesRepresentativeCommissionPer"))


            Dgl1.Item(Col1Value, rowRateType).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("RateTypeName"))
            Dgl1.Item(Col1Value, rowRateType).Tag = AgL.XNull(DsTemp.Tables(0).Rows(0)("RateType"))
            Dgl1.Item(Col1Value, rowDistance).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("Distance"))


            mQry = "SELECT Count(DISTINCT IfNull(RateType,'')) AS RateType, Count(DISTINCT IfNull(Agent,'')) AS Agent, Count(Distinct IfNull(Transporter,'')) as Transporter  
                    From SubgroupSiteDivisionDetail L With (NoLock)
                    Where L.Subcode = '" & mSearchCode & "'"
            DtSiteDivisionCount = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If AgL.VNull(DtSiteDivisionCount.Rows(0)("Agent")) > 1 Then
                Dgl1.Item(Col1Value, rowAgent).Value = ""
                Dgl1.Item(Col1Value, rowAgent).Tag = ""
                Dgl1.Item(Col1BtnDetail, rowAgent).Value = AgL.VNull(DtSiteDivisionCount.Rows(0)("Agent"))

            End If

            If AgL.VNull(DtSiteDivisionCount.Rows(0)("Transporter")) > 1 Then
                Dgl1.Item(Col1Value, rowTransporter).Value = ""
                Dgl1.Item(Col1Value, rowTransporter).Tag = ""
                Dgl1.Item(Col1BtnDetail, rowTransporter).Value = AgL.VNull(DtSiteDivisionCount.Rows(0)("Transporter"))

            End If

            If AgL.VNull(DtSiteDivisionCount.Rows(0)("RateType")) > 1 Then
                Dgl1.Item(Col1Value, rowRateType).Value = ""
                Dgl1.Item(Col1Value, rowRateType).Tag = ""
                Dgl1.Item(Col1BtnDetail, rowRateType).Value = AgL.VNull(DtSiteDivisionCount.Rows(0)("RateType"))

            End If
        End If



        mQry = "Select L.Process, P.Name As ProcessName
                From SubgroupProcess L 
                LEFT JOIN Subgroup P ON L.Process = P.SubCode
                Where L.SubCode = '" & mSearchCode & "' "
        DtTemp = AgL.FillData(mQry, AgL.GCn).tABLES(0)
        For I = 0 To DtTemp.Rows.Count - 1
            If Dgl1.Item(Col1Value, rowProcesses).Tag <> "" Then Dgl1.Item(Col1Value, rowProcesses).Tag += ","
            If Dgl1.Item(Col1Value, rowProcesses).Value <> "" Then Dgl1.Item(Col1Value, rowProcesses).Value += ","
            Dgl1.Item(Col1Value, rowProcesses).Tag += AgL.XNull(DtTemp.Rows(I)("Process"))
            Dgl1.Item(Col1Value, rowProcesses).Value += AgL.XNull(DtTemp.Rows(I)("ProcessName"))
        Next


        mQry = "Select Vt.NCat, Max(Vt.Description) As NCatName
                From SubgroupBlockedTransactions L 
                LEFT JOIN Voucher_Type Vt ON L.NCat = Vt.NCat
                Where L.SubCode = '" & mSearchCode & "' Group By Vt.NCat "
        DtTemp = AgL.FillData(mQry, AgL.GCn).tABLES(0)
        For I = 0 To DtTemp.Rows.Count - 1
            If Dgl1.Item(Col1Value, rowBlockedTransactions).Tag <> "" Then Dgl1.Item(Col1Value, rowBlockedTransactions).Tag += ","
            If Dgl1.Item(Col1Value, rowBlockedTransactions).Value <> "" Then Dgl1.Item(Col1Value, rowBlockedTransactions).Value += ","
            Dgl1.Item(Col1Value, rowBlockedTransactions).Tag += AgL.XNull(DtTemp.Rows(I)("NCat"))
            Dgl1.Item(Col1Value, rowBlockedTransactions).Value += AgL.XNull(DtTemp.Rows(I)("NCatName"))
        Next

        mQry = "Select ScopeOfWork From Division D Where D.SubCode = '" & mSearchCode & "'"
        DtTemp = AgL.FillData(mQry, AgL.GCn).TABLES(0)
        If DtTemp.Rows.Count > 0 Then
            Dgl1.Item(Col1Value, rowDivisionScopeOfWork).Value += AgL.XNull(DtTemp.Rows(I)("ScopeOfWork"))
            Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
            Dgl1.AutoResizeRow(rowDivisionScopeOfWork, DataGridViewAutoSizeRowMode.AllCells)
        End If

        SetLastValues()

        SetAttachmentCaption()

        Topctrl1.tPrn = False
        If AgL.StrCmp(Dgl1.Item(Col1Value, rowSubgroupType).Value, AgLibrary.ClsMain.agConstants.SubgroupType.Process) And
                Not AgL.StrCmp(AgL.PubUserName, "Super") Then
            Topctrl1.tAdd = False
            Topctrl1.tDel = False
            Topctrl1.tEdit = False
        End If
    End Sub

    Private Sub SetLastValues()
        Dim I As Integer
        For I = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1LastValue, I).Value = Dgl1(Col1Value, I).Value
            Dgl1(Col1LastValue, I).Tag = Dgl1(Col1Value, I).Tag
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
        Dim mUpLineStr$ = ""
        Try
            If EntryPointIniMode <> AgTemplate.ClsMain.EntryPointIniMode.Insertion Then
                MastPos = BMBMaster.Position
            End If

            'For Data Validation


            If Data_Validation() = False Then Exit Sub

            If Topctrl1.Mode = "Add" Then
                If AgL.StrCmp(Dgl1(Col1Value, rowSubgroupType).Tag, AgLibrary.ClsMain.agConstants.SubgroupType.Division) Then
                    Dim MaxDiv_Code As String = AgL.Dman_Execute("Select Max(Div_Code) As Div_Cde From Division ", AgL.GCn).ExecuteScalar()
                    mSearchCode = Chr(Asc(MaxDiv_Code) + 1)
                ElseIf AgL.StrCmp(Dgl1(Col1Value, rowSubgroupType).Tag, AgLibrary.ClsMain.agConstants.SubgroupType.Site) Then
                    Dim MaxSite_Code As String = AgL.Dman_Execute("Select Cast(Max(Code) As BIGINT) As Site_Code From SiteMast ", AgL.GCn).ExecuteScalar()
                    mSearchCode = MaxSite_Code + 1
                Else
                    mSearchCode = AgL.GetMaxId("SubGroup", "SubCode", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
                End If
                mInternalCode = mSearchCode
            End If


            mQry = "Select * from AcGroup Where GroupCode = '" & Dgl1.Item(Col1Value, rowAcGroup).Tag & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).tables(0)
            If DtTemp.Rows.Count > 0 Then
                mGroupNature = AgL.XNull(DtTemp.Rows(0)("GroupNature"))
                mNature = AgL.XNull(DtTemp.Rows(0)("Nature"))
            Else
                If mSubGroupNature = ESubgroupNature.Supplier Then
                    Dgl1(Col1Value, rowAcGroup).Tag = SubGroupConst.GroupCode_Creditors
                    Dgl1(Col1Value, rowAcGroup).Value = SubGroupConst.GroupCode_Creditors
                    mGroupNature = SubGroupConst.GroupNature_Creditors
                    mNature = SubGroupConst.Nature_Creditors

                Else
                    Dgl1(Col1Value, rowAcGroup).Tag = SubGroupConst.GroupCode_Debtors
                    Dgl1(Col1Value, rowAcGroup).Value = SubGroupConst.GroupCode_Debtors
                    mGroupNature = SubGroupConst.GroupNature_Debtors
                    mNature = SubGroupConst.Nature_Debtors
                End If
            End If



            If Topctrl1.Mode = "Add" Then
                If AgL.PubServerName = "" Then
                    Dgl1(Col1Value, rowCode).Value = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(ManualCode AS INTEGER)),0) +1 FROM Subgroup  WHERE ABS(ManualCode)>0", AgL.GcnRead).ExecuteScalar)
                Else
                    Dgl1(Col1Value, rowCode).Value = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(ManualCode AS INTEGER)),0) +1 FROM Subgroup  WHERE IsNumeric(ManualCode)>0", AgL.GcnRead).ExecuteScalar)
                End If

                If Dgl1.Rows(rowCode).Visible = True Then
                    mQry = "Select count(*) From SubGroup Where ManualCode='" & Dgl1(Col1Value, rowCode).Value & "'"
                    If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Code Already Exists")
                End If


                mQry = "Select count(*) From SubGroup Where Replace(Replace(Replace(Replace(Name,' ',''),'.',''),'-',''),'*','')='" & Replace(Replace(Replace(Replace(Dgl1(Col1Value, rowName).Value, " ", ""), ".", ""), "-", ""), "*", "") & "' And CityCode = '" & Dgl1(Col1Value, rowCity).Tag & "' "
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Name Already Exists")


                If Dgl1(Col1Value, rowSalesTaxNo).Value <> "" Then
                    mQry = "Select Sg.Name, Sg.Code, IfNull(Sg.Parent,Sg.Code) As Parent 
                            From SubgroupRegistration SR With (NoLock) 
                            Left Join viewHelpSubgroup Sg With (NoLock) On SR.Subcode = Sg.Code 
                            Where SR.RegistrationNo='" & Dgl1(Col1Value, rowSalesTaxNo).Value & "' 
                            And SR.RegistrationType = '" & SubgroupRegistrationType.SalesTaxNo & "' 
                            And Sg.SubgroupType = '" & Dgl1(Col1Value, rowSubgroupType).Tag & "' "
                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        If AgL.XNull(DtTemp.Rows(0)("Code")) = Dgl1(Col1Value, rowParent).Tag Or
                           AgL.XNull(DtTemp.Rows(0)("Parent")) = Dgl1(Col1Value, rowParent).Tag Then
                        Else
                            MsgBox("GST No. Already Exists for " & AgL.XNull(DtTemp.Rows(0)("Name")))
                            Dgl1.CurrentCell = Dgl1(Col1Value, rowSalesTaxNo)
                            Dgl1.Focus()
                            Exit Sub
                        End If


                        'MsgBox("GST No. Already Exists for " & AgL.XNull(DtTemp.Rows(0)("Name")))
                        'Dgl1.CurrentCell = Dgl1(Col1Value, rowSalesTaxNo)
                        'Dgl1.Focus()
                        'Exit Sub
                    End If
                End If

                If Dgl1(Col1Value, rowAadharNo).Value <> "" Then
                    mQry = "Select Sg.Name, Sg.Code, IfNull(Sg.Parent,Sg.Code) As Parent 
                            From SubgroupRegistration Sr With (NoLock)
                            Left Join viewHelpSubgroup Sg With (NoLock) On Sr.SubCode = Sg.Code
                            Where SR.RegistrationNo='" & Dgl1(Col1Value, rowAadharNo).Value & "' 
                            And SR.RegistrationType = '" & SubgroupRegistrationType.AadharNo & "' 
                            And Sg.SubgroupType  = '" & Dgl1(Col1Value, rowSubgroupType).Tag & "' "

                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        If AgL.XNull(DtTemp.Rows(0)("Code")) = Dgl1(Col1Value, rowParent).Tag Or
                            AgL.XNull(DtTemp.Rows(0)("Parent")) = Dgl1(Col1Value, rowParent).Tag Then
                        Else
                            MsgBox("Aadhar No. Already Exists for " & AgL.XNull(DtTemp.Rows(0)("Name")))
                            Dgl1.CurrentCell = Dgl1(Col1Value, rowAadharNo)
                            Dgl1.Focus()
                            Exit Sub
                        End If



                        'MsgBox("Aadhar No. Already Exists for " & AgL.XNull(DtTemp.Rows(0)("Name")))
                        'Dgl1.CurrentCell = Dgl1(Col1Value, rowAadharNo)
                        'Dgl1.Focus()
                        'Exit Sub
                    End If
                End If

                If Dgl1(Col1Value, rowPanNo).Value <> "" Then
                    mQry = "Select Sg.Name, Sg.Code, IfNull(Sg.Parent,Sg.Code) As Parent 
                            From SubgroupRegistration Sr With (NoLock)
                            Left Join viewHelpSubgroup Sg With (NoLock) On Sr.SubCode = Sg.Code                            
                            Where RegistrationNo='" & Dgl1(Col1Value, rowPanNo).Value & "' 
                            And RegistrationType = '" & SubgroupRegistrationType.PanNo & "' 
                            And Sg.SubgroupType  = '" & Dgl1(Col1Value, rowSubgroupType).Tag & "' "
                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        If AgL.XNull(DtTemp.Rows(0)("Code")) = Dgl1(Col1Value, rowParent).Tag Or
                            AgL.XNull(DtTemp.Rows(0)("Parent")) = Dgl1(Col1Value, rowParent).Tag Then
                        Else
                            MsgBox("PAN No. Already Exists for " & AgL.XNull(DtTemp.Rows(0)("Name")))
                            Dgl1.CurrentCell = Dgl1(Col1Value, rowPanNo)
                            Dgl1.Focus()
                            Exit Sub
                        End If


                        'MsgBox("PAN No. Already Exists for " & AgL.XNull(DtTemp.Rows(0)("Name")))
                        'Dgl1.CurrentCell = Dgl1(Col1Value, rowPanNo)
                        'Dgl1.Focus()
                        'Exit Sub
                    End If
                End If
            Else
                If Dgl1.Rows(rowCode).Visible = True Then
                    mQry = "Select count(*) From SubGroup Where ManualCode ='" & Dgl1(Col1Value, rowCode).Value & "' And SubCode<>'" & mInternalCode & "'"
                    If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Code Already Exists")
                End If

                mQry = "Select count(*) From SubGroup Where Replace(Replace(Replace(Replace(Name,' ',''),'.',''),'-',''),'*','')='" & Replace(Replace(Replace(Replace(Dgl1(Col1Value, rowName).Value, " ", ""), ".", ""), "-", ""), "*", "") & "' And CityCode = '" & Dgl1(Col1Value, rowCity).Tag & "' And SubCode<>'" & mInternalCode & "'"
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Name Already Exists")


                If Dgl1(Col1Value, rowSalesTaxNo).Value <> "" Then
                    mQry = "Select Sg.Name, Sg.Code, IfNull(Sg.Parent,Sg.Code) As Parent  
                            From SubgroupRegistration SR With (NoLock) 
                            Left Join viewHelpSubgroup Sg With (NoLock) On SR.Subcode = Sg.Code 
                            Where SR.RegistrationNo='" & Dgl1(Col1Value, rowSalesTaxNo).Value & "' 
                            And SR.RegistrationType = '" & SubgroupRegistrationType.SalesTaxNo & "' 
                            And Sg.SubgroupType = '" & Dgl1(Col1Value, rowSubgroupType).Tag & "' 
                            And SubCode<>'" & mInternalCode & "' "
                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        If AgL.XNull(DtTemp.Rows(0)("Code")) = Dgl1(Col1Value, rowParent).Tag Or
                            AgL.XNull(DtTemp.Rows(0)("Parent")) = Dgl1(Col1Value, rowParent).Tag Then
                        Else
                            MsgBox("GST No. Already Exists for " & AgL.XNull(DtTemp.Rows(0)("Name")))
                            Dgl1.CurrentCell = Dgl1(Col1Value, rowSalesTaxNo)
                            Dgl1.Focus()
                            Exit Sub
                        End If



                        'MsgBox("GST No. Already Exists for " & AgL.XNull(DtTemp.Rows(0)("Name")))
                        'Dgl1.CurrentCell = Dgl1(Col1Value, rowSalesTaxNo)
                        'Dgl1.Focus()
                        'Exit Sub
                    End If
                End If

                If Dgl1(Col1Value, rowAadharNo).Value <> "" Then
                    mQry = "Select Sg.Name, Sg.Code, IfNull(Sg.Parent,Sg.Code) As Parent 
                            From SubgroupRegistration Sr With (NoLock)
                            Left Join viewHelpSubgroup Sg With (NoLock) On Sr.SubCode = Sg.Code
                            Where SR.RegistrationNo='" & Dgl1(Col1Value, rowAadharNo).Value & "' 
                            And SR.RegistrationType = '" & SubgroupRegistrationType.AadharNo & "' 
                            And Sg.SubgroupType  = '" & Dgl1(Col1Value, rowSubgroupType).Tag & "' 
                            And SubCode<>'" & mInternalCode & "'"

                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        If AgL.XNull(DtTemp.Rows(0)("Code")) = Dgl1(Col1Value, rowParent).Tag Or
                            AgL.XNull(DtTemp.Rows(0)("Parent")) = Dgl1(Col1Value, rowParent).Tag Then
                        Else
                            MsgBox("Aadhar No. Already Exists for " & AgL.XNull(DtTemp.Rows(0)("Name")))
                            Dgl1.CurrentCell = Dgl1(Col1Value, rowAadharNo)
                            Dgl1.Focus()
                            Exit Sub
                        End If

                        'MsgBox("Aadhar No. Already Exists for " & AgL.XNull(DtTemp.Rows(0)("Name")))
                        'Dgl1.CurrentCell = Dgl1(Col1Value, rowAadharNo)
                        'Dgl1.Focus()
                        'Exit Sub
                    End If
                End If

                If Dgl1(Col1Value, rowPanNo).Value <> "" Then
                    mQry = "Select Sg.Name, Sg.Code, IfNull(Sg.Parent,Sg.Code) As Parent 
                            From SubgroupRegistration Sr With (NoLock)
                            Left Join viewHelpSubgroup Sg With (NoLock) On Sr.SubCode = Sg.Code                            
                            Where RegistrationNo='" & Dgl1(Col1Value, rowPanNo).Value & "' 
                            And RegistrationType = '" & SubgroupRegistrationType.PanNo & "' 
                            And Sg.SubgroupType  = '" & Dgl1(Col1Value, rowSubgroupType).Tag & "' 
                            And SubCode<>'" & mInternalCode & "' "
                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        If AgL.XNull(DtTemp.Rows(0)("Code")) = Dgl1(Col1Value, rowParent).Tag Or
                            AgL.XNull(DtTemp.Rows(0)("Parent")) = Dgl1(Col1Value, rowParent).Tag Then
                        Else
                            MsgBox("PAN No. Already Exists for " & AgL.XNull(DtTemp.Rows(0)("Name")))
                            Dgl1.CurrentCell = Dgl1(Col1Value, rowPanNo)
                            Dgl1.Focus()
                            Exit Sub
                        End If

                        'MsgBox("PAN No. Already Exists for " & AgL.XNull(DtTemp.Rows(0)("Name")))
                        'Dgl1.CurrentCell = Dgl1(Col1Value, rowPanNo)
                        'Dgl1.Focus()
                        'Exit Sub
                    End If
                End If
            End If

            If Dgl1(Col1Value, rowParent).Value <> "" And Dgl1(Col1Value, rowParent).Value IsNot Nothing Then
                Dim DtParent As DataTable = AgL.FillData(" Select Sg.Parent, Sg1.Name As ParentName
                        From SubGroup Sg With (NoLock)
                        LEFT JOIN SubGroup Sg1 With (NoLock) On Sg.Parent = Sg1.SubCode
                        Where Sg.SubCode = '" & Dgl1(Col1Value, rowParent).Tag & "' 
                        AND Sg.Parent IS NOT NULL", AgL.GCn).Tables(0)
                If DtParent.Rows.Count > 0 Then
                    Dgl1(Col1Value, rowParent).Tag = AgL.XNull(DtParent.Rows(0)("Parent"))
                    Dgl1(Col1Value, rowParent).Value = AgL.XNull(DtParent.Rows(0)("ParentName"))
                End If
            End If

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True




            If Topctrl1.Mode = "Add" Then
                mQry = "INSERT INTO SubGroup(SubCode, Site_Code, Name, DispName, " &
                        " GroupCode, GroupNature, ManualCode, 	Nature,	Address, CityCode,  " &
                        " PIN, Phone,  ContactPerson, SubgroupType, ShowAccountInOtherDivisions, ShowAccountInOtherSites, WeekOffDays, Grade, TdsGroup, TdsCategory, ReconciliationUpToDate, Remarks, " &
                        " Mobile, CreditDays, CreditLimit, FairDiscountPer, EMail, Parent, ChequeFormat, Area, InterestSlab, SalesTaxPostingGroup, SalesTaxGroupRegType, HSN, WStatus, " &
                        " EntryBy, EntryDate,  EntryType, EntryStatus, Div_Code, Status) " &
                        " VALUES(" & AgL.Chk_Text(mSearchCode) & ", " &
                        " " & AgL.Chk_Text(Dgl1(Col1Value, rowSite).Tag) & ", " & AgL.Chk_Text(Dgl1(Col1Value, rowName).Value) & ",	" &
                        " " & AgL.Chk_Text(IIf(Dgl1(Col1Value, rowPrintingName).Value = "", Dgl1(Col1Value, rowName).Value, Dgl1(Col1Value, rowPrintingName).Value)) & ", " & AgL.Chk_Text(Dgl1(Col1Value, rowAcGroup).Tag) & ", " &
                        " " & AgL.Chk_Text(mGroupNature) & ", " & AgL.Chk_Text(Dgl1(Col1Value, rowCode).Value) & ", " &
                        " " & AgL.Chk_Text(mNature) & ", " & AgL.Chk_Text(Dgl1(Col1Value, rowAddress).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1(Col1Value, rowCity).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1(Col1Value, rowPin).Value) & ", " & AgL.Chk_Text(Dgl1(Col1Value, rowContactNo).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowContactPerson).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1(Col1Value, rowSubgroupType).Tag) & ", " &
                        " " & IIf(Dgl1.Item(Col1Value, rowShowAccountInOtherDivisions).Value.ToUpper = "NO", 0, 1) & ", " &
                        " " & IIf(Dgl1.Item(Col1Value, rowShowAccountInOtherSites).Value.ToUpper = "NO", 0, 1) & ", " &
                        " " & AgL.Chk_Text(Dgl1(Col1Value, rowWeekOffDays).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1(Col1Value, rowGrade).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1(Col1Value, rowTdsGroup).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1(Col1Value, rowTdsCategory).Tag) & ", " &
                        " " & AgL.Chk_Date(Dgl1(Col1Value, rowReconciliationUpToDate).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1(Col1Value, rowRemarks).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1(Col1Value, rowMobile).Value) & ", " &
                        " " & Val(Dgl1(Col1Value, rowCreditDays).Value) & ", " &
                        " " & Val(Dgl1(Col1Value, rowCreditLimit).Value) & ", " &
                        " " & Val(Dgl1(Col1Value, rowFairDiscountPer).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1(Col1Value, rowEmail).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowParent).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowChequeFormat).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowArea).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowInterestSlab).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1(Col1Value, rowSalesTaxGroup).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1(Col1Value, rowSalesTaxGroupRegType).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1(Col1Value, rowHSN).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1(Col1Value, rowStatus).Value) & ", " &
                        " " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Date(CDate(AgL.GetDateTime(AgL.GcnRead)).ToString("u")) & ", " &
                        " " & AgL.Chk_Text(Topctrl1.Mode) & ", " & AgL.Chk_Text(LogStatus.LogOpen) & ", " &
                        " " & AgL.Chk_Text(TxtDivision.AgSelectedValue) & ", " & AgL.Chk_Text(TxtStatus.Text) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                mQry = "UPDATE SubGroup " &
                        " SET " &
                        " Name = " & AgL.Chk_Text(Dgl1(Col1Value, rowName).Value) & ", " &
                        " DispName = " & AgL.Chk_Text(IIf(Dgl1(Col1Value, rowPrintingName).Value = "", Dgl1(Col1Value, rowName).Value, Dgl1(Col1Value, rowPrintingName).Value)) & ", " &
                        " GroupCode = " & AgL.Chk_Text(Dgl1(Col1Value, rowAcGroup).Tag) & ", " &
                        " GroupNature = " & AgL.Chk_Text(mGroupNature) & ", " &
                        " ManualCode = " & AgL.Chk_Text(Dgl1(Col1Value, rowCode).Value) & ", " &
                        " Nature = " & AgL.Chk_Text(mNature) & ", " &
                        " Address = " & AgL.Chk_Text(Dgl1(Col1Value, rowAddress).Value) & ", " &
                        " CityCode = " & AgL.Chk_Text(Dgl1(Col1Value, rowCity).Tag) & ", " &
                        " Mobile = " & AgL.Chk_Text(Dgl1(Col1Value, rowMobile).Value) & ", " &
                        " CreditDays = " & Val(Dgl1(Col1Value, rowCreditDays).Value) & ", " &
                        " CreditLimit = " & Val(Dgl1(Col1Value, rowCreditLimit).Value) & ", " &
                        " EMail = " & AgL.Chk_Text(Dgl1(Col1Value, rowEmail).Value) & ", " &
                        " PIN = " & AgL.Chk_Text(Dgl1(Col1Value, rowPin).Value) & ", " &
                        " Phone = " & AgL.Chk_Text(Dgl1(Col1Value, rowContactNo).Value) & ", " &
                        " ContactPerson = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowContactPerson).Value) & ", " &
                        " Parent = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowParent).Tag) & ", " &
                        " ChequeFormat = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowChequeFormat).Tag) & ", " &
                        " Area = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowArea).Tag) & ", " &
                        " InterestSlab = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowInterestSlab).Tag) & ", " &
                        " SalesTaxPostingGroup = " & AgL.Chk_Text(Dgl1(Col1Value, rowSalesTaxGroup).Tag) & ", " &
                        " SalesTaxGroupRegType = " & AgL.Chk_Text(Dgl1(Col1Value, rowSalesTaxGroupRegType).Tag) & ", " &
                        " HSN = " & AgL.Chk_Text(Dgl1(Col1Value, rowHSN).Value) & ", " &
                        " SubgroupType = " & AgL.Chk_Text(Dgl1(Col1Value, rowSubgroupType).Tag) & ", " &
                        " WeekOffDays = " & AgL.Chk_Text(Dgl1(Col1Value, rowWeekOffDays).Value) & ", " &
                        " ShowAccountInOtherDivisions = " & IIf(Dgl1.Item(Col1Value, rowShowAccountInOtherDivisions).Value.ToUpper = "NO", 0, 1) & ", " &
                        " ShowAccountInOtherSites = " & IIf(Dgl1.Item(Col1Value, rowShowAccountInOtherSites).Value.ToUpper = "NO", 0, 1) & ", " &
                        " Grade = " & AgL.Chk_Text(Dgl1(Col1Value, rowGrade).Value) & ", " &
                        " TdsGroup = " & AgL.Chk_Text(Dgl1(Col1Value, rowTdsGroup).Tag) & ", " &
                        " TdsCategory = " & AgL.Chk_Text(Dgl1(Col1Value, rowTdsCategory).Tag) & ", " &
                        " ReconciliationUpToDate = " & AgL.Chk_Date(Dgl1(Col1Value, rowReconciliationUpToDate).Value) & ", " &
                        " FairDiscountPer = " & Val(Dgl1(Col1Value, rowFairDiscountPer).Value) & ", " &
                        " Remarks = " & AgL.Chk_Text(Dgl1(Col1Value, rowRemarks).Value) & ", " &
                        " WStatus = " & AgL.Chk_Text(Dgl1(Col1Value, rowStatus).Value) & ", " &
                        " EntryType = " & AgL.Chk_Text(Topctrl1.Mode) & ", " &
                        " EntryStatus = " & AgL.Chk_Text(LogStatus.LogOpen) & ", " &
                        " Div_Code = " & AgL.Chk_Text(TxtDivision.AgSelectedValue) & ", " &
                        " Site_Code = " & AgL.Chk_Text(Dgl1(Col1Value, rowSite).Tag) & ", " &
                        " UploadDate = Null, " &
                        " MoveToLogDate = " & AgL.Chk_Date(CDate(AgL.PubLoginDate).ToString("u")) & ", " &
                        " MoveToLog = '" & AgL.PubUserName & "' " &
                        " Where Subcode = " & AgL.Chk_Text(mSearchCode) & "  "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            If Dgl1.Item(Col1Value, rowSubgroupType).Value = AgLibrary.ClsMain.agConstants.SubgroupType.Process Then
                If AgL.VNull(AgL.Dman_Execute(" Select Count(*) From ProcessDetail Where SubCode = '" & mSearchCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) = 0 Then
                    mQry = "INSERT INTO ProcessDetail (Subcode, ScopeOfWork, PrevProcess, CombinationOfProcesses, FirstProcessOfCombination, LastProcessOfCombination)
                            Select '" & mSearchCode & "' As Subcode, 
                            " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowProcessScopeOfWork).Tag) & " As ScopeOfWork, 
                            " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowPrevProcess).Tag) & " As PrevProcess, 
                            " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowCombinationOfProcesses).Tag) & " As CombinationOfProcesses, 
                            " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowFirstProcessOfCombination).Tag) & " As FirstProcessOfCombination, 
                            " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowLastProcessOfCombination).Tag) & " As LastProcessOfCombination "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                Else
                    mQry = "UPDATE ProcessDetail
                            SET ScopeOfWork = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowProcessScopeOfWork).Tag) & ",
	                            PrevProcess = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowPrevProcess).Tag) & ",
	                            CombinationOfProcesses = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowCombinationOfProcesses).Tag) & ",
	                            FirstProcessOfCombination = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowFirstProcessOfCombination).Tag) & ",
	                            LastProcessOfCombination = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowLastProcessOfCombination).Tag) & "
                            Where SubCode = '" & mSearchCode & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            End If


            mQry = "Delete from HRM_Employee Where Subcode = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            If AgL.StrCmp(Dgl1(Col1Value, rowSubgroupType).Tag, AgLibrary.ClsMain.agConstants.SubgroupType.Employee) Then
                mQry = "Insert Into HRM_Employee (Code, Subcode, Designation) 
                   Values (" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(Dgl1(Col1Value, rowDesignation).Tag) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If


            mQry = "Select Count(*) from PersonDiscount With (NoLock) Where Person = '" & mSearchCode & "' And ItemCategory Is Null And ItemGroup Is Null "
            If AgL.Dman_Execute(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar() > 0 Then
                mQry = "Update PersonDiscount Set DiscountPer=" & Val(Dgl1.Item(Col1Value, rowDiscount).Value) & "  Where Person = '" & mSearchCode & "' And ItemCategory Is Null And ItemGroup Is Null  "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                If Val(Dgl1.Item(Col1Value, rowDiscount).Value) > 0 Then
                    mQry = "Insert Into PersonDiscount (Person, DiscountPer) Values (" & AgL.Chk_Text(mSearchCode) & ", " & Val(Dgl1.Item(Col1Value, rowDiscount).Value) & ") "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            End If



            mQry = "Delete from PersonAddition Where Person = '" & mSearchCode & "' And ItemGroup Is Null And ItemCategory Is Null And Process Is Null"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            If Val(Dgl1.Item(Col1Value, rowAddition).Value) > 0 Then
                mQry = "Insert Into PersonAddition (Person, AdditionPer) Values (" & AgL.Chk_Text(mSearchCode) & ", " & Val(Dgl1.Item(Col1Value, rowAddition).Value) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            mQry = "Delete from PersonExtraDiscount Where Person = '" & mSearchCode & "' And ItemGroup Is Null And ItemCategory Is Null And Process Is Null "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            If Val(Dgl1.Item(Col1Value, rowExtraDiscount).Value) > 0 Then
                mQry = "Insert Into PersonExtraDiscount (Person, ExtraDiscountPer) Values (" & AgL.Chk_Text(mSearchCode) & ", " & Val(Dgl1.Item(Col1Value, rowExtraDiscount).Value) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If




            SaveDataInPersonLastTransactionValues(mSearchCode, AgL.GCn, AgL.ECmd)


            If Dgl1.Item(Col1BtnDetail, rowDiscount).Tag IsNot Nothing And TypeOf (Dgl1(Col1BtnDetail, rowDiscount)) Is DataGridViewButtonCell Then
                CType(Dgl1.Item(Col1BtnDetail, rowDiscount).Tag, FrmPersonWiseDiscount).FSave(mSearchCode, AgL.GCn, AgL.ECmd)
            End If

            If Dgl1.Item(Col1BtnDetail, rowExtraDiscount).Tag IsNot Nothing And TypeOf (Dgl1(Col1BtnDetail, rowExtraDiscount)) Is DataGridViewButtonCell Then
                CType(Dgl1.Item(Col1BtnDetail, rowExtraDiscount).Tag, FrmPersonWiseExtraDiscount).FSave(mSearchCode, AgL.GCn, AgL.ECmd)
            End If

            If Dgl1.Item(Col1BtnDetail, rowInterestSlab).Tag IsNot Nothing And TypeOf (Dgl1(Col1BtnDetail, rowInterestSlab)) Is DataGridViewButtonCell Then
                CType(Dgl1.Item(Col1BtnDetail, rowInterestSlab).Tag, FrmPersonItemGroupInterest).FSave(mSearchCode, AgL.GCn, AgL.ECmd)
            End If

            If Dgl1.Item(Col1BtnDetail, rowRateType).Tag IsNot Nothing And TypeOf (Dgl1(Col1BtnDetail, rowRateType)) Is DataGridViewButtonCell Then
                CType(Dgl1.Item(Col1BtnDetail, rowRateType).Tag, FrmPersonSiteRateType).FSave(mSearchCode, AgL.GCn, AgL.ECmd)
            End If

            If Dgl1.Item(Col1BtnDetail, rowAgent).Tag IsNot Nothing And TypeOf (Dgl1(Col1BtnDetail, rowAgent)) Is DataGridViewButtonCell Then
                CType(Dgl1.Item(Col1BtnDetail, rowAgent).Tag, FrmPersonSiteAgent).FSave(mSearchCode, AgL.GCn, AgL.ECmd)
            End If

            If Dgl1.Item(Col1BtnDetail, rowTransporter).Tag IsNot Nothing And TypeOf (Dgl1(Col1BtnDetail, rowTransporter)) Is DataGridViewButtonCell Then
                CType(Dgl1.Item(Col1BtnDetail, rowTransporter).Tag, FrmPersonSiteTransporter).FSave(mSearchCode, AgL.GCn, AgL.ECmd)
            End If


            If AgL.StrCmp(Dgl1(Col1Value, rowSubgroupType).Tag, AgLibrary.ClsMain.agConstants.SubgroupType.Division) Then
                If AgL.Dman_Execute("Select Count(*) From Division D Where D.SubCode = " & AgL.Chk_Text(mSearchCode) & "", AgL.GcnRead).ExecuteScalar = 0 Then
                    mQry = " INSERT INTO Division (Div_Code, Div_Name, Subcode, ScopeOfWork)
                        VALUES (" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(Dgl1(Col1Value, rowCode).Value) & ", " & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(Dgl1(Col1Value, rowDivisionScopeOfWork).Value) & ") "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                Else
                    mQry = "UPDATE Division " &
                            " SET " &
                            " Div_Name = " & AgL.Chk_Text(Dgl1(Col1Value, rowCode).Value) & ", " &
                            " ScopeOfWork = " & AgL.Chk_Text(Dgl1(Col1Value, rowDivisionScopeOfWork).Value) & " " &
                            " Where Subcode = " & AgL.Chk_Text(mSearchCode) & "  "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            End If

            If AgL.StrCmp(Dgl1(Col1Value, rowSubgroupType).Tag, AgLibrary.ClsMain.agConstants.SubgroupType.Site) Then
                If AgL.Dman_Execute("Select Count(*) From SiteMast S Where S.Code = " & AgL.Chk_Text(mSearchCode) & "", AgL.GcnRead).ExecuteScalar = 0 Then
                    mQry = "INSERT INTO SiteMast (Code, ManualCode, Name, HO_YN, Add1, City_Code, Phone, Mobile, 
                        PinNo, U_Name, U_EntDt, U_AE, RowId)
                        VALUES (" & AgL.Chk_Text(mSearchCode) & ", 
                        " & AgL.Chk_Text(Dgl1(Col1Value, rowCode).Value) & ", 
                        " & AgL.Chk_Text(Dgl1(Col1Value, rowName).Value) & ", 
                        'N', " & AgL.Chk_Text(Dgl1(Col1Value, rowAddress).Value) & ", 
                        " & AgL.Chk_Text(Dgl1(Col1Value, rowCity).Tag) & ", 
                        " & AgL.Chk_Text(Dgl1(Col1Value, rowContactNo).Value) & ", 
                        " & AgL.Chk_Text(Dgl1(Col1Value, rowMobile).Value) & ", 
                        " & AgL.Chk_Text(Dgl1(Col1Value, rowPin).Value) & ", 
                        '" & AgL.PubUserName & "', 
                        " & AgL.Chk_Date(AgL.PubLoginDate) & ", 'A', 1) "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                Else
                    mQry = "UPDATE SiteMast
                            SET 
                            ManualCode = " & AgL.Chk_Text(Dgl1(Col1Value, rowCode).Value) & ",
                            Name = " & AgL.Chk_Text(Dgl1(Col1Value, rowName).Value) & ", 
                            HO_YN = 'N', 
                            Add1 = " & AgL.Chk_Text(Dgl1(Col1Value, rowAddress).Value) & ", 
                            City_Code = " & AgL.Chk_Text(Dgl1(Col1Value, rowCity).Tag) & ", 
                            Phone = " & AgL.Chk_Text(Dgl1(Col1Value, rowContactNo).Value) & ", 
                            Mobile = " & AgL.Chk_Text(Dgl1(Col1Value, rowMobile).Value) & ", 
                            PinNo = " & AgL.Chk_Text(Dgl1(Col1Value, rowPin).Value) & ", 
                            U_AE = 'E',
                            Edit_Date = " & AgL.Chk_Date(AgL.PubLoginDate) & ", 
                            ModifiedBy = '" & AgL.PubUserName & "'
                            Where Code = " & AgL.Chk_Text(mSearchCode) & ""
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            End If

            Dim mRegSr As Integer = 0

            mQry = "Delete From SubgroupRegistration Where Subcode = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            If Dgl1.Item(Col1Value, rowSalesTaxNo).Value <> "" Then
                mRegSr += 1
                mQry = "Insert Into SubgroupRegistration(Subcode, Sr, RegistrationType, RegistrationNo)
                        Values ('" & mSearchCode & "', " & mRegSr & ", '" & SubgroupRegistrationType.SalesTaxNo & "', " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowSalesTaxNo).Value) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If


            If Dgl1.Item(Col1Value, rowPanNo).Value <> "" Then
                mRegSr += 1
                mQry = "Insert Into SubgroupRegistration(Subcode, Sr, RegistrationType, RegistrationNo)
                       Values ('" & mSearchCode & "', " & mRegSr & ", '" & SubgroupRegistrationType.PanNo & "', " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowPanNo).Value) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            If Dgl1.Item(Col1Value, rowAadharNo).Value <> "" Then
                mRegSr += 1
                mQry = "Insert Into SubgroupRegistration(Subcode, Sr, RegistrationType, RegistrationNo)
                       Values ('" & mSearchCode & "', " & mRegSr & ", '" & SubgroupRegistrationType.AadharNo.ToUpper & "', " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowAadharNo).Value) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If


            If Dgl1.Item(Col1Value, rowLicenseNo).Value <> "" Then
                mRegSr += 1
                mQry = "Insert Into SubgroupRegistration(Subcode, Sr, RegistrationType, RegistrationNo)
                        Values ('" & mSearchCode & "', " & mRegSr & ", '" & SubgroupRegistrationType.LicenseNo & "', " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowLicenseNo).Value) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If



            mQry = "Delete From SubgroupBankAccount Where Subcode = '" & mSearchCode & "' And Sr=0"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            If Dgl1.Item(Col1Value, rowBankName).Value <> "" Or Dgl1.Item(Col1Value, rowBankAccount).Value <> "" Or Dgl1.Item(Col1Value, rowBankIFSC).Value <> "" Then
                mQry = "Insert Into SubgroupBankAccount(Subcode, Sr, BankName, BankAccount, BankIFSC)
                        Values ('" & mSearchCode & "', 0, " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowBankName).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowBankAccount).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowBankIFSC).Value) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If


            mQry = "DELETE FROM SubgroupProcess WHERE SubCode = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            FInsertSubgroupProcess(AgL.GCn, AgL.ECmd)

            mQry = "DELETE FROM SubgroupBlockedTransactions WHERE SubCode = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            FInsertSubgroupBlockedTransactions(AgL.GCn, AgL.ECmd)


            If Val(Dgl1.Item(Col1BtnDetail, rowTransporter).Value) = 0 Then
                mQry = "Update SubgroupSiteDivisionDetail Set 
                        Transporter = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowTransporter).Tag) & "
                        Where SubCode = '" & mSearchCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            If Val(Dgl1.Item(Col1BtnDetail, rowRelationshipExecutive).Value) = 0 Then
                mQry = "Update SubgroupSiteDivisionDetail Set 
                        RelationshipExecutive = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowRelationshipExecutive).Tag) & "
                        Where SubCode = '" & mSearchCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            If Val(Dgl1.Item(Col1BtnDetail, rowSalesRepresentative).Value) = 0 Then
                mQry = "Update SubgroupSiteDivisionDetail Set 
                        SalesRepresentative = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowSalesRepresentative).Tag) & "
                        Where SubCode = '" & mSearchCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            If Val(Dgl1.Item(Col1BtnDetail, rowSalesRepresentativeCommissionPer).Value) = 0 Then
                mQry = "Update SubgroupSiteDivisionDetail Set 
                        SalesRepresentativeCommissionPer = " & Val(Dgl1.Item(Col1Value, rowSalesRepresentativeCommissionPer).Value) & "
                        Where SubCode = '" & mSearchCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If


            If Val(Dgl1.Item(Col1BtnDetail, rowAgent).Value) = 0 Then
                mQry = "Update SubgroupSiteDivisionDetail Set 
                        Agent = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowAgent).Tag) & " 
                        Where SubCode = '" & mSearchCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                If Dgl1.Item(Col1Value, rowSubgroupType).Value.ToString.ToUpper = "MASTER CUSTOMER" Then
                    mQry = "Update SubgroupSiteDivisionDetail Set 
                        Agent = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowAgent).Tag) & " 
                        Where SubCode In (Select Subcode From Subgroup Where Parent = '" & mSearchCode & "')"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = "Update Subgroup set UploadDate=Null where Subcode  In (Select Subcode From Subgroup Where Parent = '" & mSearchCode & "')"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                Else
                    If ClsMain.FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
                        mQry = "UPDATE SubgroupSiteDivisionDetail SET Agent =(SELECT sl.Agent FROM Subgroup ss  LEFT JOIN subgroup sp ON ss.Parent = sp.Subcode LEFT JOIN  SubgroupSiteDivisionDetail sL ON sp.Subcode = sl.SubCode AND sl.Site_Code = SubgroupSiteDivisionDetail.Site_Code Where SubgroupSiteDivisionDetail.SubCode = ss.Subcode)
                            WHERE Subcode ='" & mSearchCode & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                End If
            End If

            If Val(Dgl1.Item(Col1BtnDetail, rowRateType).Value) = 0 Then
                mQry = "Update SubgroupSiteDivisionDetail Set 
                        RateType = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowRateType).Tag) & " 
                        Where SubCode = '" & mSearchCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            mQry = " Select Code From SiteMast With (NoLock) "
            DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
            For I = 0 To DtTemp.Rows.Count - 1
                mQry = "Update SubGroupSiteDivisionDetail Set Distance=(Select Distance From CitySiteDivisionDetail Where CityCode = '" & Dgl1(Col1Value, rowCity).Tag & "' And Site_Code = '" & DtTemp.Rows(I)("Code") & "') 
                        Where subcode = '" & mSearchCode & "'  And Site_Code = '" & DtTemp.Rows(I)("Code") & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Next


            If Dgl1.Item(Col1Value, rowSubgroupType).Value.ToString.ToUpper = "MASTER CUSTOMER" Then
                mQry = "Update Subgroup Set 
                        InterestSlab = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowInterestSlab).Tag) & " 
                        Where SubCode In (Select Subcode From Subgroup Where Parent = '" & mSearchCode & "')"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If



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

    Sub DisplayFieldsBasedOnNature(Nature As String)
        If Nature.ToString.ToUpper = "BANK" Then
            Dgl1.Rows(rowChequeFormat).Visible = True
        Else
            Dgl1.Rows(rowChequeFormat).Visible = False
        End If
    End Sub

    Private Sub FInsertSubgroupProcess(ByVal Conn As Object, ByVal Cmd As Object)
        Dim bRateListCode$ = ""
        Dim I As Integer, mSr As Integer

        Dim bValueArr As String() = Dgl1.Item(Col1Value, rowProcesses).Tag.ToString.Split(",")

        mSr = AgL.Dman_Execute("Select IfNull(Max(Sr),0) From SubgroupProcess With (NoLock) Where SubCode = '" & mSearchCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()

        For I = 0 To bValueArr.Length - 1
            If bValueArr(I) <> "" Then
                mSr += 1
                mQry = "INSERT INTO SubgroupProcess(SubCode, Sr, Process) 
                        VALUES(" & AgL.Chk_Text(mSearchCode) & ", 
                        " & mSr & ", 
                        " & AgL.Chk_Text(bValueArr(I)) & ") "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next
    End Sub

    Private Sub FInsertSubgroupBlockedTransactions(ByVal Conn As Object, ByVal Cmd As Object)
        Dim bRateListCode$ = ""
        Dim I As Integer, mSr As Integer

        Dim bValueArr As String() = Dgl1.Item(Col1Value, rowBlockedTransactions).Tag.ToString.Split(",")

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
            MnuImportFromTally.Visible = False
            MnuImportFromDos.Visible = False
        End If

    End Sub

    Private Sub FShowSubGroupTypeHelp()
        Dim mQry As String = " SELECT '' As Code, 'All' AS Type
                UNION ALL 
                SELECT SubgroupType As Code, SubgroupType As Type   FROM SubGroupType WHERE IsCustomUI = 0 "
        Dim FRH_Single As DMHelpGrid.FrmHelpGrid
        FRH_Single = New DMHelpGrid.FrmHelpGrid(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 350, 300, 150, 520, False)
        FRH_Single.FFormatColumn(0, , 0, , False)
        FRH_Single.FFormatColumn(1, "Type", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH_Single.StartPosition = FormStartPosition.Manual
        FRH_Single.ShowDialog()

        Dim bCode As String = ""
        If FRH_Single.BytBtnValue = 0 Then
            mSubgroupType = FRH_Single.DRReturn("Code")
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



        Dgl1.Item(Col1Head, rowCity).Tag = Nothing
        Dgl1(Col1Value, rowCity).Tag = bCityCode
        Dgl1(Col1Value, rowCity).Value = AgL.XNull(AgL.Dman_Execute("Select CityName From City Where CityCode = '" & bCityCode & "'", AgL.GCn).ExecuteScalar)
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

        Dim frmObj As FrmPerson_ShyamaShyam

        frmObj = New FrmPerson_ShyamaShyam(StrUserPermission, DTUP)
        frmObj.EntryPointIniMode = AgTemplate.ClsMain.EntryPointIniMode.Insertion
        frmObj.StartPosition = FormStartPosition.CenterParent
        frmObj.Dgl1(Col1Value, rowSubgroupType).Value = SubgroupType
        frmObj.Dgl1(Col1Value, rowSubgroupType).Tag = SubgroupType
        frmObj.ApplySubgroupTypeSetting(SubgroupType)
        frmObj.IniGrid()
        frmObj.ShowDialog()
        bSubCode = frmObj.mSearchCode
        frmObj = Nothing
    End Function

    Private Sub FrmParty_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        If AgL.PubServerName = "" Then
            Dgl1(Col1Value, rowCode).Value = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(ManualCode AS INTEGER)),0) +1 FROM Subgroup  WHERE ABS(ManualCode)>0", AgL.GcnRead).ExecuteScalar)
        Else
            Dgl1(Col1Value, rowCode).Value = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(ManualCode AS INTEGER)),0) +1 FROM Subgroup  WHERE IsNumeric(ManualCode)>0", AgL.GcnRead).ExecuteScalar)
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


        If Dgl1(Col1LastValue, rowSubgroupType).Value = "" Then
            If mSubgroupType = "" Then
                Dgl1(Col1Value, rowSubgroupType).Tag = "Customer"
                Dgl1(Col1Value, rowSubgroupType).Value = "Customer"
            Else
                Dgl1(Col1Value, rowSubgroupType).Tag = mSubgroupType
                Dgl1(Col1Value, rowSubgroupType).Value = mSubgroupType
            End If
        Else
            Dgl1(Col1Value, rowSubgroupType).Value = Dgl1(Col1LastValue, rowSubgroupType).Value
            Dgl1(Col1Value, rowSubgroupType).Tag = Dgl1(Col1LastValue, rowSubgroupType).Tag
        End If
        ApplySubgroupTypeSetting(Dgl1(Col1Value, rowSubgroupType).Tag)
        SetAttachmentCaption()

        If Dgl1.Visible = True Then
            If Dgl1(Col1Value, rowSubgroupType).Visible = False Then
                Dgl1.CurrentCell = Dgl1(Col1Value, rowName)
            Else
                Dgl1.CurrentCell = Dgl1(Col1Value, rowSubgroupType)
            End If
            Dgl1.Focus()
        End If
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub FrmParty_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        If AgL.XNull(Dgl1.Item(Col1Value, rowLockText).Value) <> "" Then
            MsgBox(AgL.XNull(Dgl1.Item(Col1Value, rowLockText).Value) & ", Can not modify")
            Passed = False
            Exit Sub
        End If

        If ClsMain.IsEntryLockedWithLockText("SubGroup", "SubCode", mSearchCode) = True Then
            Passed = False
            Exit Sub
        End If

        Dgl1.CurrentCell = Dgl1(Col1Value, rowName)
        Dgl1.Focus()
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

    Private Sub FrmPerson_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dim I As Integer

        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Head, 250, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl1, Col1Mandatory, 12, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(Dgl1, Col1Value, 630, 255, Col1Value, True, False)
            .AddAgButtonColumn(Dgl1, Col1BtnDetail, 35, Col1BtnDetail, True, True)
            .AddAgTextColumn(Dgl1, Col1HeadOriginal, 200, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(Dgl1, Col1LastValue, 200, 255, Col1LastValue, False, True)

        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.Columns(Col1Mandatory).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings 2", 5.25, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Dgl1.Columns(Col1Mandatory).DefaultCellStyle.ForeColor = Color.Red
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToAddRows = False
        Dgl1.RowHeadersVisible = False
        Dgl1.ColumnHeadersVisible = False
        Dgl1.BackgroundColor = Me.BackColor
        Dgl1.BorderStyle = BorderStyle.None
        Dgl1.Name = "Dgl1"
        AgL.GridDesign(Dgl1)
        Dgl1.Anchor = AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top + AnchorStyles.Bottom



        Dgl1.Rows.Add(59)

        Dgl1.Item(Col1Head, rowSubgroupType).Value = ConfigurableFields.FrmPersonHeaderDgl1.SubgroupType
        Dgl1.Item(Col1Head, rowCode).Value = ConfigurableFields.FrmPersonHeaderDgl1.Code
        Dgl1.Item(Col1Head, rowName).Value = ConfigurableFields.FrmPersonHeaderDgl1.Name
        Dgl1.Item(Col1Head, rowPrintingName).Value = ConfigurableFields.FrmPersonHeaderDgl1.PrintingDescription
        Dgl1.Item(Col1Head, rowAddress).Value = ConfigurableFields.FrmPersonHeaderDgl1.Address
        Dgl1.Item(Col1Head, rowCity).Value = ConfigurableFields.FrmPersonHeaderDgl1.City
        Dgl1.Item(Col1Head, rowPin).Value = ConfigurableFields.FrmPersonHeaderDgl1.Pincode
        Dgl1.Item(Col1Head, rowContactNo).Value = ConfigurableFields.FrmPersonHeaderDgl1.ContactNo
        Dgl1.Item(Col1Head, rowMobile).Value = ConfigurableFields.FrmPersonHeaderDgl1.Mobile
        Dgl1.Item(Col1Head, rowEmail).Value = ConfigurableFields.FrmPersonHeaderDgl1.Email
        Dgl1.Item(Col1Head, rowDesignation).Value = ConfigurableFields.FrmPersonHeaderDgl1.Designation
        Dgl1.Item(Col1Head, rowSite).Value = ConfigurableFields.FrmPersonHeaderDgl1.Site
        Dgl1.Item(Col1Head, rowAcGroup).Value = ConfigurableFields.FrmPersonHeaderDgl1.AcGroup
        Dgl1.Item(Col1Head, rowSalesTaxGroup).Value = ConfigurableFields.FrmPersonHeaderDgl1.SalesTaxGroup
        Dgl1.Item(Col1Head, rowSalesTaxGroupRegType).Value = ConfigurableFields.FrmPersonHeaderDgl1.SalesTaxGroupRegType
        Dgl1.Item(Col1Head, rowHSN).Value = hcHsn
        Dgl1.Item(Col1Head, rowContactPerson).Value = ConfigurableFields.FrmPersonHeaderDgl1.ContactPerson
        Dgl1.Item(Col1Head, rowPanNo).Value = ConfigurableFields.FrmPersonHeaderDgl1.PanNo
        Dgl1.Item(Col1Head, rowSalesTaxNo).Value = ConfigurableFields.FrmPersonHeaderDgl1.SalesTaxNo
        Dgl1.Item(Col1Head, rowAadharNo).Value = ConfigurableFields.FrmPersonHeaderDgl1.AadharNo
        Dgl1.Item(Col1Head, rowLicenseNo).Value = hcLicenseNo
        Dgl1.Item(Col1Head, rowParent).Value = ConfigurableFields.FrmPersonHeaderDgl1.Parent
        Dgl1.Item(Col1Head, rowArea).Value = ConfigurableFields.FrmPersonHeaderDgl1.Area
        Dgl1.Item(Col1Head, rowInterestSlab).Value = ConfigurableFields.FrmPersonHeaderDgl1.InterestSlab
        Dgl1.Item(Col1Head, rowAgent).Value = ConfigurableFields.FrmPersonHeaderDgl1.Agent
        Dgl1.Item(Col1Head, rowTransporter).Value = ConfigurableFields.FrmPersonHeaderDgl1.Transporter
        Dgl1.Item(Col1Head, rowRateType).Value = ConfigurableFields.FrmPersonHeaderDgl1.RateType
        Dgl1.Item(Col1Head, rowDistance).Value = ConfigurableFields.FrmPersonHeaderDgl1.Distance
        Dgl1.Item(Col1Head, rowDiscount).Value = ConfigurableFields.FrmPersonHeaderDgl1.Discount
        Dgl1.Item(Col1Head, rowAddition).Value = ConfigurableFields.FrmPersonHeaderDgl1.Addition
        Dgl1.Item(Col1Head, rowExtraDiscount).Value = ConfigurableFields.FrmPersonHeaderDgl1.ExtraDiscount
        Dgl1.Item(Col1Head, rowCreditDays).Value = ConfigurableFields.FrmPersonHeaderDgl1.CreditDays
        Dgl1.Item(Col1Head, rowCreditLimit).Value = ConfigurableFields.FrmPersonHeaderDgl1.CreditLimit
        Dgl1.Item(Col1Head, rowBankName).Value = hcBankName
        Dgl1.Item(Col1Head, rowBankAccount).Value = hcBankAccount
        Dgl1.Item(Col1Head, rowBankIFSC).Value = hcBankIFSC
        Dgl1.Item(Col1Head, rowShowAccountInOtherDivisions).Value = hcShowAccountInOtherDivisions
        Dgl1.Item(Col1Head, rowShowAccountInOtherSites).Value = hcShowAccountInOtherSites
        Dgl1.Item(Col1Head, rowRelationshipExecutive).Value = hcRelationshipExecutive
        Dgl1.Item(Col1Head, rowSalesRepresentative).Value = hcSalesRepresentative
        Dgl1.Item(Col1Head, rowSalesRepresentativeCommissionPer).Value = hcSalesRepresentativeCommissionPer
        Dgl1.Item(Col1Head, rowWeekOffDays).Value = hcWeekOffDays
        Dgl1.Item(Col1Head, rowProcesses).Value = hcProcesses
        Dgl1.Item(Col1Head, rowChequeFormat).Value = hcChequeFormat
        Dgl1.Item(Col1Head, rowBlockedTransactions).Value = hcBlockedTransactions
        Dgl1.Item(Col1Head, rowLockText).Value = hcLockText
        Dgl1.Item(Col1Head, rowGrade).Value = hcGrade
        Dgl1.Item(Col1Head, rowTdsGroup).Value = hcTdsGroup
        Dgl1.Item(Col1Head, rowTdsCategory).Value = hcTdsCategory
        Dgl1.Item(Col1Head, rowReconciliationUpToDate).Value = hcReconciliationUpToDate
        Dgl1.Item(Col1Head, rowDivisionScopeOfWork).Value = hcDivisionScopeOfWork
        Dgl1.Item(Col1Head, rowFairDiscountPer).Value = hcFairDiscountPer
        Dgl1.Item(Col1Head, rowPrevProcess).Value = hcPrevProcess
        Dgl1.Item(Col1Head, rowProcessScopeOfWork).Value = hcProcessScopeOfWork
        Dgl1.Item(Col1Head, rowCombinationOfProcesses).Value = hcCombinationOfProcesses
        Dgl1.Item(Col1Head, rowFirstProcessOfCombination).Value = hcFirstProcessOfCombination
        Dgl1.Item(Col1Head, rowLastProcessOfCombination).Value = hcLastProcessOfCombination
        Dgl1.Item(Col1Head, rowStatus).Value = hcStatus

        Dgl1.Item(Col1Head, rowRemarks).Value = ConfigurableFields.FrmPersonHeaderDgl1.Remarks
        Dgl1.Rows(rowAddress).Height = 50
        Dgl1(Col1Value, rowAddress).Style.WrapMode = DataGridViewTriState.True
        Dgl1.Rows(rowRemarks).Height = 50
        Dgl1(Col1Value, rowRemarks).Style.WrapMode = DataGridViewTriState.True

        For I = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1HeadOriginal, I).Value = Dgl1(Col1Head, I).Value
        Next

        Dgl1.Item(Col1Head, rowDiscount).Value = AgL.PubCaptionLineDiscount
        Dgl1.Item(Col1Head, rowAddition).Value = AgL.PubCaptionLineAddition
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try

            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            If Topctrl1.Mode = "BROWSE" Then
                Dgl1.CurrentCell.ReadOnly = True
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

            'Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            '    Case Col1BtnDetail
            '        If Dgl1(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).ReadOnly = True And  Then
            '            SendKeys.Send("{Enter}")
            '        End If
            'End Select

            If Dgl1.CurrentCell.ColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub


            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0
            'Dgl1.Columns(Col1Value).DefaultCellStyle.WrapMode = DataGridViewTriState.True            


            Select Case Dgl1.CurrentCell.RowIndex
                Case rowContactPerson
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 100
                Case rowPanNo
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 10
                Case rowAadharNo
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 12
                Case rowSalesTaxNo
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 15
                Case rowDiscount
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberLeftPlaces = 2
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberRightPlaces = 2
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberNegetiveAllow = False
                Case rowAddition
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberLeftPlaces = 2
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberRightPlaces = 2
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberNegetiveAllow = False
                Case rowExtraDiscount
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberLeftPlaces = 2
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberRightPlaces = 2
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberNegetiveAllow = False
                Case rowPin
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberLeftPlaces = 6
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberRightPlaces = 0
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberNegetiveAllow = False
                Case rowBankName, rowBankAccount, rowBankIFSC
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 50
                Case rowHSN
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberLeftPlaces = 8
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberRightPlaces = 0
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberNegetiveAllow = False
                Case rowReconciliationUpToDate
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
                Case rowDivisionScopeOfWork, rowProcessScopeOfWork, rowCombinationOfProcesses
                    Dgl1.Item(Col1Value, Dgl1.CurrentCell.RowIndex).ReadOnly = True
                Case rowFairDiscountPer
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberLeftPlaces = 2
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberRightPlaces = 2
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberNegetiveAllow = False
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim bNewMasterCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub

            Select Case Dgl1.CurrentCell.RowIndex
                Case rowSubgroupType
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = " Select H.SubgroupType As Code, SubgroupType As Name FROM SubGroupType H Where IfNull(IsActive,1)=1  "
                            If mSubgroupType <> "" Then
                                mQry += " And SubGroupType = '" & mSubgroupType & "' "
                            Else
                                mQry += " And IfNull(IsCustomUI,0)=0  "
                            End If
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowCity
                    If e.KeyCode = Keys.Insert Then
                        FOpenCityMaster()

                    ElseIf e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "Select CityCode, CityName From City Order By CityName"
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowRelationshipExecutive
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Sg.Code, Sg.Name From viewHelpSubgroup Sg  With (NoLock) Left Join HRM_Employee Emp On Sg.Code = Emp.Subcode Where sg.SubgroupType ='" & AgLibrary.ClsMain.agConstants.SubgroupType.Employee & "' And Emp.RelievingDate Is Null And Site_Code = '" & AgL.PubSiteCode & "' Order By sg.Name "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowSalesRepresentative
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Sg.Code, Sg.Name From viewHelpSubgroup Sg  With (NoLock) Left Join HRM_Employee Emp On Sg.Code = Emp.Subcode Where sg.SubgroupType ='" & AgLibrary.ClsMain.agConstants.SubgroupType.Employee & "' And Emp.RelievingDate Is Null And Site_Code = '" & AgL.PubSiteCode & "' Order By sg.Name "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If


                Case rowCode
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "Select S.SubCode as Code, S.ManualCode, S.Name as [Name], C.CityName " &
                                    " From SubGroup S  " &
                                    " Left Join City C On S.CityCode = C.CityCode " &
                                    " Order By S.ManualCode "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                            CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True
                        End If
                    End If

                Case rowName
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "Select S.SubCode as Code, S.Name As [Name], C.CityName, S.SubgroupType as [A/c Type] 
                                    From SubGroup S 
                                    Left Join City C On S.CityCode = C.CityCode
                                    Order By S.Name "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                            CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True
                        End If
                    End If

                Case rowParent
                    If e.KeyCode = Keys.Insert Then
                        bNewMasterCode = FOpenPersonMaster(Dgl1(Col1Value, rowSubgroupType).Value)
                        Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Tag = bNewMasterCode
                        Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Value = AgL.XNull(AgL.Dman_Execute("Select Name From viewHelpSubgroup Where Code = '" & bNewMasterCode & "'", AgL.GCn).ExecuteScalar)

                        SendKeys.Send("{Enter}")
                    End If
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "select Sg.Code, Sg.Name From viewHelpSubgroup Sg Where Sg.Code <>'" & mSearchCode & "'"
                        If AgL.XNull(DtSubgroupTypeSettings.Rows(0)("FilterInclude_SubgroupTypeForMasterParty")) <> "" Then
                            mQry += " And CharIndex('+' || Sg.SubgroupType,'" & AgL.XNull(DtSubgroupTypeSettings.Rows(0)("FilterInclude_SubgroupTypeForMasterParty")) & "') > 0 "
                            mQry += " And CharIndex('-' || Sg.SubgroupType,'" & AgL.XNull(DtSubgroupTypeSettings.Rows(0)("FilterInclude_SubgroupTypeForMasterParty")) & "') <= 0 "
                        End If
                        mQry += " Order By Sg.Name"

                        ''Patch
                        'If mSubgroupType = "Customer" Then
                        '    mQry += " And Sg.SubGroupType = 'Master Customer'"
                        'ElseIf mSubgroupType = "Supplier" Then
                        '    mQry += " And Sg.SubGroupType = 'Master Supplier'"
                        'Else
                        '    mQry += " And Sg.SubGroupType = ''"
                        'End If
                        'mQry += " Order By Sg.Name"
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowArea
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "Select Code, Description From area Order By Description"
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowChequeFormat
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "Select Code, Description From ChequeFormat Order By Description"
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowInterestSlab
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "Select Code, Description From InterestSlab Order By Description"
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowAgent
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        If Dgl1(Col1Value, rowSubgroupType).Value = AgLibrary.ClsMain.agConstants.SubgroupType.Supplier Then
                            mQry = "Select Code, Name From viewHelpSubgroup Where subgroupType = '" & AgLibrary.ClsMain.agConstants.SubgroupType.PurchaseAgent & "' Order By Name"
                        Else
                            mQry = "select Code, Name From viewHelpSubgroup Where subgroupType = '" & AgLibrary.ClsMain.agConstants.SubgroupType.SalesAgent & "' Order By Name"
                        End If

                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowTransporter
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "select Code, Name From viewHelpSubgroup Where subgroupType = '" & AgLibrary.ClsMain.agConstants.SubgroupType.Transporter & "' Order By Name"
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowDesignation
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "select Code, Description From HRM_Designation Order By Description"
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowRateType
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "select Code, Description From RateType Order By Description"
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If
                Case rowAcGroup
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "Select A.GroupCode As Code, A.GroupName As Name, A.GroupNature , A.Nature   FROM AcGroup A "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value, 2) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowSalesTaxGroup
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " Select Description As Code, Description  FROM PostingGroupSalesTaxParty "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If
                Case rowSalesTaxGroupRegType
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " Select Name As Code, Name  FROM PostingGroupSalesTaxRegType "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowFirstProcessOfCombination, rowLastProcessOfCombination
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " Select SubCode As Code, Name  FROM SubGroup Where SubGroupType = '" & AgLibrary.ClsMain.agConstants.SubgroupType.Process & "' "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowStatus
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " Select 'Active' As Code, 'Active' as Name Union All Select 'Inactive' as Code, 'Inactive' as Name  "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If


                Case rowSite
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " Select Code, Name  FROM SiteMast Order By Name "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowGrade
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " Select Distinct Grade As Code, Grade As Name FROM SubGroup Order By Grade "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If
                    CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True

                Case rowTdsCategory
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " SELECT Code, Description  FROM TdsCategory Order By Description "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowTdsGroup
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " SELECT Code, Description  FROM TdsGroup Order By Description "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowPrevProcess
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " SELECT SubCode, Name FROM SubGroup Where SubGroupType = '" & mSubgroupType & "' Order By Name "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmPerson_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dim I As Integer
        For I = 0 To Dgl1.Rows.Count - 1
            Dgl1.Item(Col1Value, I).Value = ""
            Dgl1.Item(Col1Value, I).Tag = ""
            Dgl1.Item(Col1BtnDetail, I).Tag = Nothing
            Dgl1.Item(Col1BtnDetail, I) = New DataGridViewTextBoxCell
            Dgl1(Col1BtnDetail, I).ReadOnly = True
        Next

        gStateCode = ""
    End Sub

    Private Sub FrmPerson_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer
        Dim DtTemp As DataTable

        Dgl1.EndEdit()

        passed = AgCL.AgCheckMandatory(Me)

        For I = 0 To Dgl1.RowCount - 1
            If AgL.XNull(Dgl1(Col1Mandatory, I).Value) <> "" And Dgl1.Rows(I).Visible Then
                If AgL.XNull(Dgl1(Col1Value, I).Value) = "" And AgL.XNull(Dgl1(Col1BtnDetail, I).Value) = "" Then
                    MsgBox(Dgl1(Col1Head, I).Value & " can not be blank.")
                    Dgl1.CurrentCell = Dgl1(Col1Value, I)
                    Dgl1.Focus()
                    passed = False
                    Exit Sub
                End If
            End If
        Next


        If Dgl1(Col1Value, rowSalesTaxGroup).Value.ToUpper = "REGISTERED" Then
            If Dgl1.Item(Col1Value, rowSalesTaxNo).Value = "" Then
                MsgBox("GST No. is mandatory for registered dealers.")
                passed = False
                Exit Sub
            End If
        End If


        mQry = " Select Count(*) From SubGroup 
                Where Replace(Replace(Replace(Replace(Name,' ',''),'.',''),'-',''),',','') = '" & AgL.XNull(Dgl1.Item(Col1Value, rowName).Value).ToString.Replace(" ", "").Replace(".", "").Replace("-", "").Replace(",", "") & "' 
                And SubCode <> '" & mSearchCode & "'"
        If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar()) > 0 Then
            MsgBox("Party already exists.", MsgBoxStyle.Information)
            Dgl1.CurrentCell = Dgl1.Item(Col1Value, rowName)
            Dgl1.Focus()
            passed = False
            Exit Sub
        End If



        If Dgl1(Col1Value, rowSalesTaxGroup).Value.ToUpper = "UNREGISTERED" Then
            If Dgl1.Item(Col1Value, rowSalesTaxNo).Value <> "" Then
                'If AgL.StrCmp(Dgl1(Col1Value, rowSubgroupType).Tag, AgLibrary.ClsMain.agConstants.SubgroupType.Transporter) Then
                '    If MsgBox("GST No. defined for unregistered Transporter. Do you want to continue? ", MsgBoxStyle.YesNo) = vbNo Then
                '        Dgl1.CurrentCell = Dgl1(Col1Value, rowSalesTaxGroup)
                '        Dgl1.Focus()
                '        passed = False
                '        Exit Sub
                '    End If
                'Else
                '    MsgBox("GST No. can not be defined for unregistered parties.")
                '    Dgl1.CurrentCell = Dgl1(Col1Value, rowSalesTaxGroup)
                '    Dgl1.Focus()
                '    passed = False
                '    Exit Sub
                'End If

                If Not AgL.StrCmp(Dgl1(Col1Value, rowSubgroupType).Tag, AgLibrary.ClsMain.agConstants.SubgroupType.Transporter) Then
                    MsgBox("GST No. can not be defined for unregistered parties.")
                    Dgl1.CurrentCell = Dgl1(Col1Value, rowSalesTaxGroup)
                    Dgl1.Focus()
                    passed = False
                    Exit Sub
                End If

            End If
        End If

        If ClsFunction.ValidateGstNo(Dgl1.Item(Col1Value, rowSalesTaxNo).Value, Dgl1(Col1Value, rowSalesTaxGroup).Tag, gStateCode, AgL.StrCmp(Dgl1(Col1Value, rowSubgroupType).Tag, AgLibrary.ClsMain.agConstants.SubgroupType.Transporter)) = False Then
            Dgl1.CurrentCell = Dgl1(Col1Value, rowSalesTaxNo)
            Dgl1.Focus()
            passed = False
            Exit Sub
        End If

        If Dgl1.Item(Col1Value, rowSalesTaxNo).Value <> "" And Dgl1.Item(Col1Value, rowCity).Value <> "ALL INDIA" Then
            If GSTINValidator.IsValid(Dgl1.Item(Col1Value, rowSalesTaxNo).Value) = False Then
                MsgBox("GST No is not valid...!", MsgBoxStyle.Information)
                Dgl1.CurrentCell = Dgl1(Col1Value, rowSalesTaxNo)
                Dgl1.Focus()
                passed = False
                Exit Sub
            End If
        End If




        If ValidatePanNo(Dgl1.Item(Col1Value, rowPanNo).Value) = False Then
            Dgl1.CurrentCell = Dgl1(Col1Value, rowPanNo)
            Dgl1.Focus()
            passed = False
            Exit Sub
        End If

        If ValidateAadharNo(Dgl1.Item(Col1Value, rowAadharNo).Value) = False Then
            Dgl1.CurrentCell = Dgl1(Col1Value, rowAadharNo)
            Dgl1.Focus()
            passed = False
            Exit Sub
        End If


        If ValidateEMailId(Dgl1(Col1Value, rowEmail).Value) = False Then
            Dgl1.CurrentCell = Dgl1(Col1Value, rowEmail)
            Dgl1.Focus()
            passed = False
            Exit Sub
        End If






        If AgL.XNull(Dgl1.Item(Col1Value, rowSite).Tag) = "" Then Dgl1(Col1Value, rowSite).Tag = AgL.PubSiteCode
        If AgL.XNull(Dgl1.Item(Col1Value, rowShowAccountInOtherDivisions).Value) = "" Then Dgl1.Item(Col1Value, rowShowAccountInOtherDivisions).Value = "YES"
        If AgL.XNull(Dgl1.Item(Col1Value, rowShowAccountInOtherSites).Value) = "" Then Dgl1.Item(Col1Value, rowShowAccountInOtherSites).Value = "YES"

        SetLastValues()
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
                    ElseIf AgL.XNull(DtSubcode.Rows(0)("STRegNo")).ToString.Substring(0, 2) <> AgL.XNull(DtSubcode.Rows(0)("StateCode")) And Not Not AgL.StrCmp(Dgl1(Col1Value, rowSubgroupType).Tag, AgLibrary.ClsMain.agConstants.SubgroupType.Transporter) Then
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

            If Dgl1(Col1Value, rowSalesTaxGroup).Value.ToUpper = "REGISTERED" Then
                If Len(Dgl1.Item(Col1Value, rowSalesTaxNo).Value) <> 15 Then

                End If
            End If

        Catch ex As Exception

        End Try
    End Function


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
    Public Sub FImportFromExcel_Old()
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtTemp As DataTable
        Dim DtMain As DataTable = Nothing
        Dim I As Integer
        'Dim FW As System.IO.StreamWriter = New System.IO.StreamWriter("C:\ImportLog.Txt", False, System.Text.Encoding.Default)
        Dim StrErrLog As String = ""
        mQry = "Select '' as Srl, 'Party Type' as [Field Name], 'Text' as [Data Type], 10 as [Length], 'Mandatory, Customer / Supplier / Transporter / Sales Agent / Purchase Agent' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Code' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, Should be unique.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Display Name' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Name' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory, Should be unique.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Address' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'City' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'State' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Pin No' as [Field Name], 'Text' as [Data Type], 6 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Contact No' as [Field Name], 'Text' as [Data Type], 35 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Mobile' as [Field Name], 'Text' as [Data Type], 10 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'EMail' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Account Group' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory, Sundry Debtors / Sundry Creditors' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sales Tax Group' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, Registered / Unregistered / Composition' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Credit Days' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Credit Limit' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Contact Person' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'GST No' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'PAN No' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Aadhar No' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Master Party' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Area' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Agent' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Transporter' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Distance' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "

        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim ObjFrmImport As New FrmImportFromExcel
        ObjFrmImport.Text = "Person Master Import"
        ObjFrmImport.Dgl1.DataSource = DtTemp
        ObjFrmImport.StartPosition = FormStartPosition.CenterScreen
        ObjFrmImport.ShowDialog()

        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

        DtTemp = ObjFrmImport.P_DsExcelData.Tables(0)


        Dim DtAccountGroup = DtTemp.DefaultView.ToTable(True, "Account Group")
        For I = 0 To DtAccountGroup.Rows.Count - 1
            If AgL.XNull(DtAccountGroup.Rows(I)("Account Group")) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From AcGroup where GroupName = '" & AgL.XNull(DtAccountGroup.Rows(I)("Account Group")) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Account Groups Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Account Groups Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtAccountGroup.Rows(I)("Account Group")) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtAccountGroup.Rows(I)("Account Group")) & ", "
                    End If
                End If
            End If
        Next

        'Dim DtCity = DtTemp.DefaultView.ToTable(True, "City")
        'For I = 0 To DtCity.Rows.Count - 1
        '    If AgL.XNull(DtCity.Rows(I)("City")) <> "" Then
        '        If AgL.Dman_Execute("SELECT Count(*) From City where CityName = '" & AgL.XNull(DtCity.Rows(I)("City")) & "' ", AgL.GCn).ExecuteScalar = 0 Then
        '            If ErrorLog.Contains("These Cities Are Not Present In Master") = False Then
        '                ErrorLog += vbCrLf & "These Cities Are Not Present In Master" & vbCrLf
        '                ErrorLog += AgL.XNull(DtCity.Rows(I)("City")) & ", "
        '            Else
        '                ErrorLog += AgL.XNull(DtCity.Rows(I)("City")) & ", "
        '            End If
        '        End If
        '    End If
        'Next

        Dim DtState = DtTemp.DefaultView.ToTable(True, "State")
        For I = 0 To DtState.Rows.Count - 1
            If AgL.XNull(DtState.Rows(I)("State")) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From State where Description = '" & AgL.XNull(DtState.Rows(I)("State")) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These States Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These States Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtState.Rows(I)("State")) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtState.Rows(I)("State")) & ", "
                    End If
                End If
            End If
        Next

        Dim DtSalesTaxGroup = DtTemp.DefaultView.ToTable(True, "Sales Tax Group")
        For I = 0 To DtSalesTaxGroup.Rows.Count - 1
            If AgL.XNull(DtSalesTaxGroup.Rows(I)("Sales Tax Group")) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From PostingGroupSalesTaxParty where Description = '" & AgL.XNull(DtSalesTaxGroup.Rows(I)("Sales Tax Group")) & "' ", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Sales Tax Groups Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Sales Tax Groups Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtSalesTaxGroup.Rows(I)("Sales Tax Group")) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtSalesTaxGroup.Rows(I)("Sales Tax Group")) & ", "
                    End If
                End If
            End If
        Next

        For I = 0 To DtTemp.Rows.Count - 1
            If AgL.XNull(DtTemp.Rows(I)("Account Group")) = "" Then
                ErrorLog += "Account Group is blank at row no." + (I + 2).ToString() & vbCrLf
            End If

            If AgL.XNull(DtTemp.Rows(I)("City")) = "" Then
                ErrorLog += "City is blank at row no." + (I + 2).ToString() & vbCrLf
            End If

            If AgL.XNull(DtTemp.Rows(I)("State")) = "" Then
                ErrorLog += "State is blank at row no." + (I + 2).ToString() & vbCrLf
            End If

            If AgL.XNull(DtTemp.Rows(I)("Sales Tax Group")) = "" Then
                ErrorLog += "Sales Tax Group is blank at row no." + (I + 2).ToString() & vbCrLf
            End If
        Next

        If ErrorLog <> "" Then
            If File.Exists(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt") Then
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt", ErrorLog, False)
            Else
                File.Create(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt")
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt", ErrorLog, False)
            End If
            System.Diagnostics.Process.Start("notepad.exe", My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt")
            Exit Sub
        End If

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            Dim mCityCode = AgL.GetMaxId("City", "CityCode", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

            Dim DtCity = DtTemp.DefaultView.ToTable(True, "City")
            For I = 0 To DtCity.Rows.Count - 1
                If AgL.XNull(DtCity.Rows(I)("City")) <> "" Then
                    If AgL.Dman_Execute("SELECT Count(*) From City where CityName = '" & AgL.XNull(DtCity.Rows(I)("City")) & "'", AgL.GCn).ExecuteScalar = 0 Then
                        Dim mCityCode_New = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(mCityCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")
                        mQry = " INSERT INTO City(CityCode, CityName, State, EntryBy, EntryDate, EntryType, EntryStatus)
                                    Select '" & mCityCode_New & "' As CityCode, " & AgL.Chk_Text(AgL.XNull(DtCity.Rows(I)("City"))) & " As City, 
                                    (SELECT Code From State where Description = '" & AgL.XNull(DtTemp.Rows(I)("State")) & "') As State, 
                                    '" & AgL.PubUserName & "' As EntryBy, " & AgL.Chk_Date(AgL.PubLoginDate) & " As EntryDate, 
                                    'Add' As EntryType, 'Open' As EntryStatus "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                End If
            Next


            Dim mSubCode As String = AgL.GetMaxId("SubGroup", "SubCode", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

            For I = 0 To DtTemp.Rows.Count - 1
                Dim mRegSr As Integer = 0
                Dim mSubCode_New = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(mSubCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(8, "0")


                Dim mGroupCode As String = ""
                Dim mGroupNature As String = ""
                Dim mNature As String = ""

                mQry = "SELECT GroupCode, GroupNature, Nature  From AcGroup WHERE GroupName =  '" & AgL.XNull(DtTemp.Rows(I)("Account Group")) & "'"
                Dim DtAcGroup As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If (DtAcGroup.Rows.Count > 0) Then
                    mGroupCode = DtAcGroup.Rows(0)("GroupCode")
                    mGroupNature = DtAcGroup.Rows(0)("GroupNature")
                    mNature = DtAcGroup.Rows(0)("Nature")
                End If

                If AgL.Dman_Execute("SELECT Count(*) From Subgroup where ManualCode = '" & AgL.XNull(DtTemp.Rows(I)("Code")) & "' ", AgL.GCn).ExecuteScalar = 0 Then
                    mQry = "INSERT INTO SubGroup(SubCode, Site_Code, Name, DispName, " &
                        " GroupCode, GroupNature, ManualCode,	Nature,	Address, CityCode,  " &
                        " PIN, Phone,  ContactPerson, SubgroupType, " &
                        " Mobile, CreditDays, CreditLimit, EMail, Parent, SalesTaxPostingGroup, " &
                        " EntryBy, EntryDate,  EntryType, EntryStatus, Div_Code, Status) " &
                        " Select " & AgL.Chk_Text(mSubCode_New) & ", " &
                        " '" & AgL.PubSiteCode & "', " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Name"))) & ",	" &
                        " " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Name"))) & ", " & AgL.Chk_Text(mGroupCode) & ", " &
                        " " & AgL.Chk_Text(mGroupNature) & ", " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Code"))) & ", " &
                        " " & AgL.Chk_Text(mNature) & ", " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Address"))) & ", " &
                        " (SELECT CityCode  From City WHERE CityName = '" & AgL.XNull(DtTemp.Rows(I)("City")) & "') As CityCode, " &
                        " " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Pin No"))) & ", " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Contact No"))) & ", " &
                        " " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Contact Person"))) & ", " &
                        " " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Party Type"))) & ", " &
                        " " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Mobile"))) & ", " &
                        " " & Val(AgL.XNull(DtTemp.Rows(I)("Credit Days"))) & ", " &
                        " " & Val(AgL.XNull(DtTemp.Rows(I)("Credit Limit"))) & ", " &
                        " " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("EMail"))) & ", " &
                        " Null, " &
                        " (SELECT Description from PostingGroupSalesTaxParty WHERE Description = '" & AgL.XNull(DtTemp.Rows(I)("Sales Tax Group")) & "') As SalesTaxPostingGroup, " &
                        " " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", " &
                        " 'Add', " & AgL.Chk_Text(LogStatus.LogOpen) & ", " &
                        " " & AgL.Chk_Text(AgL.PubDivCode) & ", 'Active' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    If AgL.XNull(DtTemp.Rows(I)("Sales Tax No")) <> "" Then
                        mRegSr += 1
                        mQry = "Insert Into SubgroupRegistration(Subcode, Sr, RegistrationType, RegistrationNo)
                        Values ('" & mSubCode_New & "', " & mRegSr & ", '" & SubgroupRegistrationType.SalesTaxNo & "', " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("SalesTaxNo"))) & ") "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If


                    If AgL.XNull(DtTemp.Rows(I)("PAN No")) <> "" Then
                        mRegSr += 1
                        mQry = "Insert Into SubgroupRegistration(Subcode, Sr, RegistrationType, RegistrationNo)
                       Values ('" & mSubCode_New & "', " & mRegSr & ", '" & SubgroupRegistrationType.PanNo & "', " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("PAN No"))) & ") "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If

                    If AgL.XNull(DtTemp.Rows(I)("Aadhar No")) <> "" Then
                        mRegSr += 1
                        mQry = "Insert Into SubgroupRegistration(Subcode, Sr, RegistrationType, RegistrationNo)
                       Values ('" & mSubCode_New & "', " & mRegSr & ", '" & SubgroupRegistrationType.AadharNo.ToUpper & "', " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Aadhar No"))) & ") "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                End If
            Next

            AgL.ETrans.Commit()
            mTrans = "Commit"

        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
        If StrErrLog <> "" Then MsgBox(StrErrLog)
    End Sub

    Public Sub FImportFromExcel(bImportFor As ImportFor)
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtTemp As DataTable
        Dim DtDataFields As DataTable
        Dim DtMain As DataTable = Nothing
        Dim I As Integer
        'Dim FW As System.IO.StreamWriter = New System.IO.StreamWriter("C:\ImportLog.Txt", False, System.Text.Encoding.Default)
        Dim StrErrLog As String = ""
        mQry = "Select '' as Srl, '" & GetFieldAliasName(bImportFor, "Party Type") & "' as [Field Name], 'Text' as [Data Type], 10 as [Length], 'Customer / Supplier / Transporter / Sales Agent / Purchase Agent. If Party is a simple ledger account like expenses then this field can be blank.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Code") & "' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, Should be unique.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Display Name") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Name") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory, Should be unique.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Address") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "City") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "State") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Pin No") & "' as [Field Name], 'Text' as [Data Type], 6 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Contact No") & "' as [Field Name], 'Text' as [Data Type], 35 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Mobile") & "' as [Field Name], 'Text' as [Data Type], 10 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "EMail") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Account Group") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory, Sundry Debtors / Sundry Creditors' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Sales Tax Group") & "' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, Registered / Unregistered / Composition' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Credit Days") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Credit Limit") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Contact Person") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "GST No") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "PAN No") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Aadhar No") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Master Party") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Area") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Agent") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Transporter") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Distance") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "OMSId") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "

        DtDataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim ObjFrmImport As New FrmImportFromExcel
        ObjFrmImport.Text = "Person Master Import"
        ObjFrmImport.Dgl1.DataSource = DtDataFields
        ObjFrmImport.StartPosition = FormStartPosition.CenterScreen
        ObjFrmImport.ShowDialog()

        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

        DtTemp = ObjFrmImport.P_DsExcelData.Tables(0)



        If bImportFor = ImportFor.Dos Then

        End If

        For I = 0 To DtDataFields.Rows.Count - 1
            If AgL.XNull(DtDataFields.Rows(I)("Remark")).ToString().Contains("Mandatory") Then
                If Not DtTemp.Columns.Contains(GetFieldAliasName(bImportFor, AgL.XNull(DtDataFields.Rows(I)("Field Name"))).ToString()) Then
                    If ErrorLog.Contains("These fields are not present is excel file") = False Then
                        ErrorLog += vbCrLf & "These fields are not present is excel file" & vbCrLf
                        ErrorLog += GetFieldAliasName(bImportFor, AgL.XNull(DtDataFields.Rows(I)("Field Name")).ToString()) & ", "
                    Else
                        ErrorLog += GetFieldAliasName(bImportFor, AgL.XNull(DtDataFields.Rows(I)("Field Name")).ToString()) & ", "
                    End If
                End If
            End If
        Next

        If DtTemp.Columns.Contains(GetFieldAliasName(bImportFor, "State")) Then
            Dim DtState = DtTemp.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "State"))
            For I = 0 To DtState.Rows.Count - 1
                If AgL.XNull(DtState.Rows(I)(GetFieldAliasName(bImportFor, "State"))).ToString().Trim() <> "" Then
                    If AgL.Dman_Execute("SELECT Count(*) From State where Upper(RTrim(LTrim(Description))) = '" & AgL.XNull(DtState.Rows(I)(GetFieldAliasName(bImportFor, "State"))).ToString().ToUpper.Trim() & "'", AgL.GCn).ExecuteScalar = 0 Then
                        If ErrorLog.Contains("These States Are Not Present In Master") = False Then
                            ErrorLog += vbCrLf & "These States Are Not Present In Master" & vbCrLf
                            ErrorLog += AgL.XNull(DtState.Rows(I)(GetFieldAliasName(bImportFor, "State"))).ToString.Trim & ", "
                        Else
                            ErrorLog += AgL.XNull(DtState.Rows(I)(GetFieldAliasName(bImportFor, "State"))).ToString.Trim & ", "
                        End If
                    End If
                End If
            Next
        End If

        If DtTemp.Columns.Contains(GetFieldAliasName(bImportFor, "Sales Tax Group")) Then
            Dim DtSalesTaxGroup = DtTemp.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Sales Tax Group"))
            For I = 0 To DtSalesTaxGroup.Rows.Count - 1
                If AgL.XNull(DtSalesTaxGroup.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group"))).ToString().Trim() <> "" Then
                    If AgL.Dman_Execute("SELECT Count(*) From PostingGroupSalesTaxParty where Upper(RTrim(LTrim(Description)))  = '" & AgL.XNull(DtSalesTaxGroup.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group"))).ToString().ToUpper.Trim() & "' ", AgL.GCn).ExecuteScalar = 0 Then
                        If ErrorLog.Contains("These Sales Tax Groups Are Not Present In Master") = False Then
                            ErrorLog += vbCrLf & "These Sales Tax Groups Are Not Present In Master" & vbCrLf
                            ErrorLog += AgL.XNull(DtSalesTaxGroup.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group"))) & ", "
                        Else
                            ErrorLog += AgL.XNull(DtSalesTaxGroup.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group"))) & ", "
                        End If
                    End If
                End If
            Next
        End If

        For I = 0 To DtTemp.Rows.Count - 1
            If AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "City"))).ToString.Trim <> "" And AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "State"))).ToString.Trim = "" Then
                ErrorLog += " State is blank at row no." + (I + 2).ToString() & vbCrLf
            End If

            For J As Integer = 0 To DtDataFields.Rows.Count - 1
                If DtTemp.Columns.Contains(DtDataFields.Rows(J)("Field Name")) Then
                    If DtDataFields.Rows(J)("Remark").ToString().Contains("Mandatory") Then
                        If AgL.XNull(DtTemp.Rows(I)(DtDataFields.Rows(J)("Field Name"))) = "" Then
                            ErrorLog += DtDataFields.Rows(J)("Field Name") + " is blank at row no." + (I + 2).ToString() & vbCrLf
                        End If
                    End If
                End If
            Next
        Next

        If ErrorLog <> "" Then
            If File.Exists(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt") Then
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt", ErrorLog, False)
            Else
                File.Create(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt")
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt", ErrorLog, False)
            End If
            System.Diagnostics.Process.Start("notepad.exe", My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt")
            Exit Sub
        End If

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            Dim bLastAcGroupCode As Integer = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(GroupCode AS INTEGER)),0) FROM AcGroup WHERE ABS(GroupCode)>0", AgL.GcnRead).ExecuteScalar)
            Dim DtAccountGroup = DtTemp.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Account Group"))
            For I = 0 To DtAccountGroup.Rows.Count - 1
                Dim AcGroupTable As New StructAcGroup
                Dim bAcGroupCode As String = (bLastAcGroupCode + (I + 1)).ToString.PadLeft(4).Replace(" ", "0")

                AcGroupTable.GroupCode = bAcGroupCode
                AcGroupTable.SNo = ""
                AcGroupTable.GroupName = AgL.XNull(DtAccountGroup.Rows(I)(GetFieldAliasName(bImportFor, "Account Group"))).ToString.Trim
                AcGroupTable.ContraGroupName = AgL.XNull(DtAccountGroup.Rows(I)(GetFieldAliasName(bImportFor, "Account Group"))).ToString.Trim
                AcGroupTable.GroupUnder = ""


                If AgL.XNull(DtAccountGroup.Rows(I)(GetFieldAliasName(bImportFor, "Account Group"))).ToString.Trim.Contains("Bank") Then
                    AcGroupTable.GroupNature = "A"
                    AcGroupTable.Nature = "Bank"
                ElseIf AgL.XNull(DtAccountGroup.Rows(I)(GetFieldAliasName(bImportFor, "Account Group"))).ToString.Trim.Contains("Cash") Then
                    AcGroupTable.GroupNature = "A"
                    AcGroupTable.Nature = "Cash"
                Else
                    AcGroupTable.GroupNature = "A"
                    AcGroupTable.Nature = "Others"
                End If


                AcGroupTable.SysGroup = "N"
                AcGroupTable.U_Name = AgL.PubUserName
                AcGroupTable.U_EntDt = AgL.GetDateTime(AgL.GcnRead)
                AcGroupTable.U_AE = "A"

                ImportAcGroupTable(AcGroupTable)
            Next


            Dim bLastSubCode As String = AgL.GetMaxId("SubGroup", "SubCode", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

            For I = 0 To DtTemp.Rows.Count - 1
                Dim SubGroupTable As New StructSubGroupTable
                Dim bSubCode = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastSubCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(8, "0")

                SubGroupTable.SubCode = bSubCode
                SubGroupTable.Site_Code = AgL.PubSiteCode
                SubGroupTable.Name = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Name"))).ToString.Trim
                SubGroupTable.DispName = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Display Name"))).ToString.Trim
                SubGroupTable.ManualCode = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Code"))).ToString.Trim
                SubGroupTable.AccountGroup = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Account Group"))).ToString.Trim
                SubGroupTable.StateName = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "State"))).ToString.Trim
                SubGroupTable.AgentName = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Agent"))).ToString.Trim
                SubGroupTable.TransporterName = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Transporter"))).ToString.Trim
                SubGroupTable.AreaName = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Area"))).ToString.Trim
                SubGroupTable.CityName = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "City"))).ToString.Trim
                SubGroupTable.GroupCode = ""
                SubGroupTable.GroupNature = ""
                SubGroupTable.Nature = ""
                SubGroupTable.Address = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Address"))).ToString.Trim
                SubGroupTable.CityCode = ""
                SubGroupTable.PIN = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Pin No"))).ToString.Trim
                SubGroupTable.Phone = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Contact No"))).ToString.Trim
                'If AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Contact No"))).ToString.Trim <> "" Then
                '    MsgBox("")
                'End If
                SubGroupTable.ContactPerson = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Contact Person"))).ToString.Trim
                SubGroupTable.SubgroupType = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Party Type"))).ToString().Trim()
                SubGroupTable.Mobile = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Mobile"))).ToString.Trim
                SubGroupTable.CreditDays = AgL.VNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Credit Days")))
                SubGroupTable.CreditLimit = AgL.VNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Credit Limit")))
                SubGroupTable.EMail = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "EMail"))).ToString.Trim
                SubGroupTable.Parent = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Master Party"))).ToString.Trim
                SubGroupTable.SalesTaxPostingGroup = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax Group"))).ToString().Trim()
                SubGroupTable.EntryBy = AgL.PubUserName
                SubGroupTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                SubGroupTable.EntryType = "Add"
                SubGroupTable.EntryStatus = LogStatus.LogOpen
                SubGroupTable.Div_Code = AgL.PubDivCode
                SubGroupTable.Status = "Active"
                SubGroupTable.SalesTaxNo = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Sales Tax No"))).ToString.Trim
                SubGroupTable.PANNo = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "PAN No"))).ToString.Trim
                SubGroupTable.AadharNo = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "Aadhar No"))).ToString.Trim
                SubGroupTable.OMSId = AgL.XNull(DtTemp.Rows(I)(GetFieldAliasName(bImportFor, "OMSId"))).ToString.Trim
                SubGroupTable.Cnt = I
                ImportSubgroupTable(SubGroupTable)

                If I Mod 1000 = 0 Then
                    'MsgBox(I.ToString + " Records Imported.")
                End If
            Next

            AgL.ETrans.Commit()
            mTrans = "Commit"

        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message + " at Record " + I.ToString)
        End Try
        If StrErrLog <> "" Then MsgBox(StrErrLog)
    End Sub
    Private Function GetFieldAliasName(bImportFor As ImportFor, bFieldName As String)
        Dim bAliasName As String = bFieldName
        If bImportFor = ImportFor.Dos Then


            Select Case bFieldName
                Case "Party Type"
                    bAliasName = "PARTY_TYPE"
                Case "Code"
                    bAliasName = "Code"
                Case "Display Name"
                    bAliasName = "DISPLAY"
                Case "Name"
                    bAliasName = "NAME"
                Case "Address"
                    bAliasName = "ADDRESS"
                Case "City"
                    bAliasName = "CITY"
                Case "State"
                    bAliasName = "STATE"
                Case "Pin No"
                    bAliasName = "PIN_NO"
                Case "Contact No"
                    bAliasName = "CONTACT_NO"
                Case "Mobile"
                    bAliasName = "MOBILE"
                Case "EMail"
                    bAliasName = "EMAIL"
                Case "Account Group"
                    bAliasName = "ACC_GROUP"
                Case "Sales Tax Group"
                    bAliasName = "TAX_GROUP"
                Case "Credit Days"
                    bAliasName = "CREDITDAYS"
                Case "Credit Limit"
                    bAliasName = "LIMIT"
                Case "Contact Person"
                    bAliasName = "CONTACT"
                Case "Sales Tax No"
                    bAliasName = "GST_NO"
                Case "PAN No"
                    bAliasName = "PAN_NO"
                Case "Aadhar No"
                    bAliasName = "AADHAR_NO"
                Case "Master Party"
                    bAliasName = "MASTER"
                Case "Area"
                    bAliasName = "AREA"
                Case "Agent"
                    bAliasName = "AGENT"
                Case "Transporter"
                    bAliasName = "TRANSPORT"
                Case "Distance"
                    bAliasName = "DISTANCE"
            End Select
            Return bAliasName
        Else
            Return bFieldName
        End If
    End Function

    Private Sub MnuImport_Click(sender As Object, e As EventArgs) Handles MnuImportFromExcel.Click, MnuImportFromDos.Click, MnuImportFromTally.Click, MnuBulkEdit.Click
        Select Case sender.name
            Case MnuImportFromExcel.Name
                FImportFromExcel(ImportFor.Excel)

            Case MnuImportFromDos.Name
                FImportFromExcel(ImportFor.Dos)

            Case MnuImportFromTally.Name
                FImportFromTally()

            Case MnuBulkEdit.Name
                Dim FrmObj As New FrmPersonBulk()
                FrmObj.MdiParent = Me.MdiParent
                FrmObj.Show()
        End Select
    End Sub
    Public Sub FImportFromTally()
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtTemp As New DataTable
        Dim I As Integer = 0, J As Integer = 0
        Dim FileNameWithPath As String = ""
        'Dim FileNameWithPath As String = My.Application.Info.DirectoryPath & "\TallyXML\LedgerMaster.xml"

        OFDMain.Filter = "*.xml|*.XML"
        If OFDMain.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        FileNameWithPath = OFDMain.FileName



        Dim doc As New XmlDocument()
        doc.Load(FileNameWithPath)

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            Dim bLastAcGroupCode As Integer = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(GroupCode AS INTEGER)),0) FROM AcGroup WHERE ABS(GroupCode)>0", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar)

            Dim LedgerGroupElementList As XmlNodeList = doc.GetElementsByTagName("GROUP")
            For I = 0 To LedgerGroupElementList.Count - 1
                Dim AcGroupTable As New StructAcGroup
                Dim bAcGroupCode As String = (bLastAcGroupCode + I).ToString.PadLeft(4).Replace(" ", "0")

                AcGroupTable.GroupCode = bAcGroupCode
                AcGroupTable.SNo = ""
                AcGroupTable.GroupName = LedgerGroupElementList(I).Attributes("NAME").Value
                AcGroupTable.ContraGroupName = LedgerGroupElementList(I).Attributes("NAME").Value

                If LedgerGroupElementList(I).SelectSingleNode("PARENT") IsNot Nothing Then
                    If LedgerGroupElementList(I).SelectSingleNode("PARENT").ChildNodes.Count > 0 Then
                        AcGroupTable.GroupUnder = LedgerGroupElementList(I).SelectSingleNode("PARENT").ChildNodes(0).Value
                    End If
                End If
                AcGroupTable.GroupNature = ""
                AcGroupTable.Nature = ""
                AcGroupTable.SysGroup = "N"
                AcGroupTable.U_Name = AgL.PubUserName
                AcGroupTable.U_EntDt = AgL.GetDateTime(AgL.GcnRead)
                AcGroupTable.U_AE = "A"

                ImportAcGroupTable(AcGroupTable)
            Next


            'Dim bLastManualCode As String = AgL.XNull(AgL.Dman_Execute("SELECT  IfNull(Max(CAST(ManualCode AS INTEGER)),0) FROM Subgroup  WHERE ABS(ManualCode)>0", AgL.GcnRead).ExecuteScalar)
            Dim bLastManualCode As String = "0"
            Dim bLastSubCode As String = AgL.GetMaxId("SubGroup", "SubCode", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)




            Dim LedgerElementList As XmlNodeList = doc.GetElementsByTagName("LEDGER")
            For I = 0 To LedgerElementList.Count - 1
                Dim SubGroupTable As New StructSubGroupTable
                Dim bManualCode = bLastManualCode + I
                Dim bSubCode = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastSubCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(8, "0")

                SubGroupTable.SubCode = bSubCode
                SubGroupTable.Site_Code = AgL.PubSiteCode
                SubGroupTable.Name = LedgerElementList(I).Attributes("NAME").Value
                SubGroupTable.DispName = LedgerElementList(I).Attributes("NAME").Value
                SubGroupTable.ManualCode = bManualCode

                If LedgerElementList(I).SelectSingleNode("PARENT") IsNot Nothing Then
                    If LedgerElementList(I).SelectSingleNode("PARENT").ChildNodes.Count > 0 Then
                        SubGroupTable.AccountGroup = LedgerElementList(I).SelectSingleNode("PARENT").ChildNodes(0).Value
                    End If
                End If

                If LedgerElementList(I).SelectSingleNode("LEDSTATENAME") IsNot Nothing Then
                    If LedgerElementList(I).SelectSingleNode("LEDSTATENAME").ChildNodes.Count > 0 Then
                        SubGroupTable.StateName = LedgerElementList(I).SelectSingleNode("LEDSTATENAME").ChildNodes(0).Value
                    End If
                End If

                If LedgerElementList(I).SelectSingleNode("UDF_788529287.LIST") IsNot Nothing Then
                    If LedgerElementList(I).SelectSingleNode("UDF_788529287.LIST").SelectSingleNode("UDF_788529287") IsNot Nothing Then
                        SubGroupTable.AgentName = LedgerElementList(I).SelectSingleNode("UDF_788529287.LIST").SelectSingleNode("UDF_788529287").ChildNodes(0).Value
                    End If
                End If

                If LedgerElementList(I).SelectSingleNode("UDF_788529285.LIST") IsNot Nothing Then
                    If LedgerElementList(I).SelectSingleNode("UDF_788529285.LIST").SelectSingleNode("UDF_788529285") IsNot Nothing Then
                        SubGroupTable.TransporterName = LedgerElementList(I).SelectSingleNode("UDF_788529285.LIST").SelectSingleNode("UDF_788529285").ChildNodes(0).Value
                    End If
                End If

                If LedgerElementList(I).SelectSingleNode("UDF_788529311.LIST") IsNot Nothing Then
                    If LedgerElementList(I).SelectSingleNode("UDF_788529311.LIST").SelectSingleNode("UDF_788529311") IsNot Nothing Then
                        SubGroupTable.AreaName = LedgerElementList(I).SelectSingleNode("UDF_788529311.LIST").SelectSingleNode("UDF_788529311").ChildNodes(0).Value
                    End If
                End If

                If LedgerElementList(I).SelectSingleNode("UDF_788529317.LIST") IsNot Nothing Then
                    If LedgerElementList(I).SelectSingleNode("UDF_788529317.LIST").SelectSingleNode("UDF_788529317") IsNot Nothing Then
                        SubGroupTable.CityName = LedgerElementList(I).SelectSingleNode("UDF_788529317.LIST").SelectSingleNode("UDF_788529317").ChildNodes(0).Value
                    End If
                End If

                If AgL.XNull(SubGroupTable.CityName) = "" Then
                    SubGroupTable.CityName = SubGroupTable.StateName
                End If

                SubGroupTable.GroupCode = ""
                SubGroupTable.GroupNature = ""
                SubGroupTable.Nature = ""

                If LedgerElementList(I).SelectSingleNode("ADDRESS.LIST") IsNot Nothing Then
                    For J = 0 To LedgerElementList(I).SelectSingleNode("ADDRESS.LIST").ChildNodes.Count - 1
                        If LedgerElementList(I).SelectSingleNode("ADDRESS.LIST").ChildNodes(J).ChildNodes.Count > 0 Then
                            If SubGroupTable.Address = "" Then
                                SubGroupTable.Address = LedgerElementList(I).SelectSingleNode("ADDRESS.LIST").ChildNodes(J).ChildNodes(0).Value
                            Else
                                SubGroupTable.Address += " " + LedgerElementList(I).SelectSingleNode("ADDRESS.LIST").ChildNodes(J).ChildNodes(0).Value
                            End If
                        End If
                    Next J
                End If

                SubGroupTable.CityCode = ""
                If LedgerElementList(I).SelectSingleNode("PINCODE") IsNot Nothing Then
                    If LedgerElementList(I).SelectSingleNode("PINCODE").ChildNodes.Count > 0 Then
                        SubGroupTable.PIN = LedgerElementList(I).SelectSingleNode("PINCODE").ChildNodes(0).Value
                    End If
                End If
                If LedgerElementList(I).SelectSingleNode("LEDGERPHONE") IsNot Nothing Then
                    If LedgerElementList(I).SelectSingleNode("LEDGERPHONE").ChildNodes.Count > 0 Then
                        SubGroupTable.Phone = LedgerElementList(I).SelectSingleNode("LEDGERPHONE").ChildNodes(0).Value
                    End If
                End If
                If LedgerElementList(I).SelectSingleNode("LEDGERCONTACT") IsNot Nothing Then
                    If LedgerElementList(I).SelectSingleNode("LEDGERCONTACT").ChildNodes.Count > 0 Then
                        SubGroupTable.ContactPerson = LedgerElementList(I).SelectSingleNode("LEDGERCONTACT").ChildNodes(0).Value
                    End If
                End If


                If SubGroupTable.AccountGroup = "Sundry Debtors" Then
                    SubGroupTable.SubgroupType = "Customer"
                ElseIf SubGroupTable.AccountGroup = "Sundry Creditors" Then
                    SubGroupTable.SubgroupType = "Supplier"
                Else
                    SubGroupTable.SubgroupType = "Ledger Account"
                End If


                If LedgerElementList(I).SelectSingleNode("LEDGERMOBILE") IsNot Nothing Then
                    If LedgerElementList(I).SelectSingleNode("LEDGERMOBILE").ChildNodes.Count > 0 Then
                        SubGroupTable.Mobile = LedgerElementList(I).SelectSingleNode("LEDGERMOBILE").ChildNodes(0).Value
                    End If
                End If
                SubGroupTable.CreditDays = 0
                SubGroupTable.CreditLimit = 0
                If LedgerElementList(I).SelectSingleNode("EMAIL") IsNot Nothing Then
                    If LedgerElementList(I).SelectSingleNode("EMAIL").ChildNodes.Count > 0 Then
                        SubGroupTable.EMail = LedgerElementList(I).SelectSingleNode("EMAIL").ChildNodes(0).Value
                    End If
                End If
                SubGroupTable.Parent = ""
                If LedgerElementList(I).SelectSingleNode("GSTREGISTRATIONTYPE") IsNot Nothing Then
                    If LedgerElementList(I).SelectSingleNode("GSTREGISTRATIONTYPE").ChildNodes.Count > 0 Then
                        SubGroupTable.SalesTaxPostingGroup = LedgerElementList(I).SelectSingleNode("GSTREGISTRATIONTYPE").ChildNodes(0).Value
                    End If
                End If


                SubGroupTable.EntryBy = AgL.PubUserName
                SubGroupTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                SubGroupTable.EntryType = "Add"
                SubGroupTable.EntryStatus = LogStatus.LogOpen
                SubGroupTable.Div_Code = AgL.PubDivCode
                SubGroupTable.Status = "Active"

                If LedgerElementList(I).SelectSingleNode("PARTYGSTIN") IsNot Nothing Then
                    If LedgerElementList(I).SelectSingleNode("PARTYGSTIN").ChildNodes.Count > 0 Then
                        SubGroupTable.SalesTaxNo = LedgerElementList(I).SelectSingleNode("PARTYGSTIN").ChildNodes(0).Value
                    End If
                End If

                SubGroupTable.PANNo = ""
                SubGroupTable.AadharNo = ""
                SubGroupTable.Cnt = I

                ImportSubgroupTable(SubGroupTable)
            Next I
            AgL.ETrans.Commit()
            mTrans = "Commit"

        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message + " at row number " + I.ToString)
        End Try
    End Sub
    Public Shared Function ImportSubgroupTable(SubGroupTable As StructSubGroupTable, Optional UpdateIfExists As Boolean = False) As String
        Dim mQry As String = ""
        Dim mRegSr As Integer = 0

        'If AgL.Dman_Execute("SELECT Count(*) From Subgroup With (NoLock) where ManualCode = '" & SubGroupTable.ManualCode & "' ", AgL.GcnRead).ExecuteScalar = 0 Then
        If AgL.Dman_Execute("SELECT Count(*) From Subgroup With (NoLock) where Name = " & AgL.Chk_Text(SubGroupTable.Name) & " ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar = 0 Then
            If SubGroupTable.CityCode <> "" Then
                If SubGroupTable.CityName <> "" Then
                    If AgL.Dman_Execute("SELECT Count(*) From City With (NoLock) where CityName = '" & SubGroupTable.CityName & "' ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar = 0 Then
                        Dim mLastCityCode = AgL.GetMaxId("City", "CityCode", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.GCn.ConnectionString)

                        Dim mCityCode = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(mLastCityCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + 1).ToString().PadLeft(4, "0")

                        Dim mStateCode As String = AgL.XNull(AgL.Dman_Execute("Select Code From State With (NoLock) 
                        Where Description = '" & SubGroupTable.StateName & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())

                        If mStateCode = "" Then
                            Err.Raise(1, "", "State is blank for " & SubGroupTable.CityName)
                        End If


                        mQry = "INSERT INTO City (CityCode, CityName, State, IsDeleted,
                             Country, EntryBy, EntryDate, EntryType,
                             EntryStatus, Status, Div_Code, U_Name, U_AE)
                             Select '" & mCityCode & "' As CityCode, '" & SubGroupTable.CityName & "' CityName, 
                             (Select Code From State Where Description = '" & SubGroupTable.StateName & "') State, 
                             0 As IsDeleted,
                             'India' As Country, '" & SubGroupTable.EntryBy & "' EntryBy, 
                             " & AgL.Chk_Date(SubGroupTable.EntryDate) & " As EntryDate, 
                             " & AgL.Chk_Text(SubGroupTable.EntryType) & " As EntryType,
                             " & AgL.Chk_Text(SubGroupTable.EntryStatus) & " As EntryStatus, 
                             " & AgL.Chk_Text(SubGroupTable.Status) & " As Status, 
                             " & AgL.Chk_Text(SubGroupTable.Div_Code) & " As Div_Code, 
                             '" & SubGroupTable.EntryBy & "'  As U_Name, 'A' As U_AE "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                End If

                SubGroupTable.CityCode = AgL.Dman_Execute("SELECT CityCode From City With (NoLock) where CityName = '" & SubGroupTable.CityName & "' ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar
            End If
            'If AgL.XNull(AgL.Dman_Execute("Select State From City With (NoLock) Where CityCode = '" & SubGroupTable.CityCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead))) = "" Then

            'End If

            If AgL.Dman_Execute("SELECT Count(*) From Area With (NoLock) where Description = '" & SubGroupTable.AreaName & "' ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar = 0 Then
                Dim mLastAreaCode = AgL.GetMaxId("Area", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.GCn.ConnectionString)

                Dim mAreaCode = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(mLastAreaCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + SubGroupTable.Cnt).ToString().PadLeft(4, "0")

                mQry = "INSERT INTO Area (Code, Description, IsDeleted,
                             EntryBy, EntryDate, EntryType,
                             EntryStatus, Status, Div_Code)
                             Select '" & mAreaCode & "' As AreaCode, '" & SubGroupTable.AreaName & "' Description, 
                             0 As IsDeleted,
                             '" & SubGroupTable.EntryBy & "' EntryBy, 
                             " & AgL.Chk_Date(SubGroupTable.EntryDate) & " As EntryDate, 
                             " & AgL.Chk_Text(SubGroupTable.EntryType) & " As EntryType,
                             " & AgL.Chk_Text(SubGroupTable.EntryStatus) & " As EntryStatus, 
                             " & AgL.Chk_Text(SubGroupTable.Status) & " As Status, 
                             " & AgL.Chk_Text(SubGroupTable.Div_Code) & " As Div_Code "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            End If

            SubGroupTable.AreaCode = AgL.Dman_Execute("SELECT Code From Area With (NoLock) where Description = '" & SubGroupTable.AreaName & "' ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar

            SubGroupTable.TransporterCode = AgL.Dman_Execute("Select SubCode From SubGroup With (NoLock) Where Name = '" & SubGroupTable.TransporterName & "' ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar
            SubGroupTable.AgentCode = AgL.Dman_Execute("Select SubCode From SubGroup With (NoLock) Where Name = '" & SubGroupTable.AgentName & "' ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar
            SubGroupTable.Parent = AgL.Dman_Execute("Select SubCode From SubGroup With (NoLock) Where Name = '" & SubGroupTable.Parent & "' ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar


            mQry = "SELECT GroupCode, GroupNature, Nature  From AcGroup With (NoLock) WHERE GroupName =  '" & SubGroupTable.AccountGroup & "'"
            Dim DtAcGroup As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).Tables(0)
            If (DtAcGroup.Rows.Count > 0) Then
                If SubGroupTable.GroupCode = "" Then SubGroupTable.GroupCode = AgL.XNull(DtAcGroup.Rows(0)("GroupCode"))
                If SubGroupTable.GroupNature = "" Then SubGroupTable.GroupNature = AgL.XNull(DtAcGroup.Rows(0)("GroupNature"))
                If SubGroupTable.Nature = "" Then SubGroupTable.Nature = AgL.XNull(DtAcGroup.Rows(0)("Nature"))
            End If

            If SubGroupTable.SubgroupType = "" Then
                If SubGroupTable.Nature = "Customer" Or SubGroupTable.AccountGroup.Contains("Debtor") Then
                    SubGroupTable.SubgroupType = AgLibrary.ClsMain.agConstants.SubgroupType.Customer
                ElseIf SubGroupTable.Nature = "Supplier" Or SubGroupTable.AccountGroup.Contains("Creditor") Then
                    SubGroupTable.SubgroupType = AgLibrary.ClsMain.agConstants.SubgroupType.Supplier
                ElseIf SubGroupTable.Nature = "TRANSPORT" Or SubGroupTable.AccountGroup.Contains("TRANSPORT") Then
                    SubGroupTable.SubgroupType = AgLibrary.ClsMain.agConstants.SubgroupType.Transporter
                ElseIf SubGroupTable.Nature = "Broker" Or SubGroupTable.AccountGroup.Contains("Broker") Then
                    SubGroupTable.SubgroupType = AgLibrary.ClsMain.agConstants.SubgroupType.SalesAgent
                End If
            End If

            If SubGroupTable.SubgroupType = "" Then
                SubGroupTable.SubgroupType = AgLibrary.ClsMain.agConstants.SubgroupType.LedgerAccount
            End If

            If SubGroupTable.SalesTaxPostingGroup = "Regular" Then
                SubGroupTable.SalesTaxPostingGroup = AgLibrary.ClsMain.agConstants.PostingGroupSalesTaxParty.Registered
            End If

            If SubGroupTable.PIN IsNot Nothing Then
                If SubGroupTable.PIN.Length > 6 Then
                    SubGroupTable.PIN = SubGroupTable.PIN.Substring(1, 6)
                End If
            End If


            'If SubGroupTable.Mobile.Length > 10 Then
            '    SubGroupTable.Mobile = SubGroupTable.Mobile.Substring(0, 9)
            'End If

            mQry = "INSERT INTO SubGroup(SubCode, Site_Code, Name, DispName, " &
                    " GroupCode, GroupNature, ManualCode,	Nature,	Address, CityCode,  " &
                    " PIN, Phone,  ContactPerson, SubgroupType, " &
                    " Mobile, CreditDays, CreditLimit, EMail, Parent, SalesTaxPostingGroup, InterestSlab, " &
                    " EntryBy, EntryDate,  EntryType, EntryStatus, Div_Code, Status, LockText, OMSId) " &
                    " Select " & AgL.Chk_Text(SubGroupTable.SubCode) & ", " &
                    " '" & SubGroupTable.Site_Code & "', " & AgL.Chk_Text(SubGroupTable.Name) & ",	" &
                    " " & AgL.Chk_Text(SubGroupTable.Name) & ", " & AgL.Chk_Text(SubGroupTable.GroupCode) & ", " &
                    " " & AgL.Chk_Text(SubGroupTable.GroupNature) & ", " & AgL.Chk_Text(SubGroupTable.ManualCode) & ", " &
                    " " & AgL.Chk_Text(SubGroupTable.Nature) & ", " & AgL.Chk_Text(SubGroupTable.Address) & ", " &
                    " " & AgL.Chk_Text(SubGroupTable.CityCode) & ", " &
                    " " & AgL.Chk_Text(SubGroupTable.PIN) & ", " & AgL.Chk_Text(SubGroupTable.Phone) & ", " &
                    " " & AgL.Chk_Text(SubGroupTable.ContactPerson) & ", " &
                    " " & AgL.Chk_Text(SubGroupTable.SubgroupType) & ", " &
                    " " & AgL.Chk_Text(SubGroupTable.Mobile) & ", " &
                    " " & Val(SubGroupTable.CreditDays) & ", " &
                    " " & Val(SubGroupTable.CreditLimit) & ", " &
                    " " & AgL.Chk_Text(SubGroupTable.EMail) & ", " &
                    " " & AgL.Chk_Text(SubGroupTable.Parent) & ", " & AgL.Chk_Text(SubGroupTable.SalesTaxPostingGroup) & ", " & AgL.Chk_Text(SubGroupTable.InterestSlab) & "," &
                    " " & AgL.Chk_Text(SubGroupTable.EntryBy) & ", " & AgL.Chk_Date(SubGroupTable.EntryDate) & ",   " &
                    " " & AgL.Chk_Text(SubGroupTable.EntryType) & ", " & AgL.Chk_Text(SubGroupTable.EntryStatus) & ",  " &
                    " " & AgL.Chk_Text(SubGroupTable.Div_Code) & ", " & AgL.Chk_Text(SubGroupTable.Status) & ", " &
                    " " & AgL.Chk_Text(SubGroupTable.LockText) & ", " &
                    " " & AgL.Chk_Text(SubGroupTable.OMSId) & ""
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


            mQry = "INSERT INTO SubgroupSiteDivisionDetail (SubCode, V_Type, Div_Code, Site_Code,
                        V_Date, V_No, RateType, Transporter, TermsAndConditions, Agent)
                        Select '" & SubGroupTable.SubCode & "' As SubCode,  'SI' As V_Type, '" & SubGroupTable.Div_Code & "' As Div_Code, 
                        '" & SubGroupTable.Site_Code & "' As Site_Code,
                        Null As V_Date, Null As V_No, Null As RateType, " & AgL.Chk_Text(SubGroupTable.TransporterCode) & " As Transporter, 
                        Null As TermsAndConditions, " & AgL.Chk_Text(SubGroupTable.AgentCode) & " As Agent "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            If SubGroupTable.SalesTaxNo <> "" Then
                mRegSr += 1
                mQry = "Insert Into SubgroupRegistration(Subcode, SR, RegistrationType, RegistrationNo)
                        Values ('" & SubGroupTable.SubCode & "', " & mRegSr & ", '" & AgLibrary.ClsMain.agConstants.SubgroupRegistrationType.SalesTaxNo & "', " & AgL.Chk_Text(SubGroupTable.SalesTaxNo) & ") "
                Try
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If


            If SubGroupTable.PANNo <> "" Then
                mRegSr += 1
                mQry = "Insert Into SubgroupRegistration(Subcode, Sr, RegistrationType, RegistrationNo)
                       Values ('" & SubGroupTable.SubCode & "', " & mRegSr & ", '" & AgLibrary.ClsMain.agConstants.SubgroupRegistrationType.PanNo & "', " & AgL.Chk_Text(SubGroupTable.PANNo) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            If SubGroupTable.AadharNo <> "" Then
                mRegSr += 1
                mQry = "Insert Into SubgroupRegistration(Subcode, Sr, RegistrationType, RegistrationNo)
                       Values ('" & SubGroupTable.SubCode & "', " & mRegSr & ", '" & SubgroupRegistrationType.AadharNo.ToUpper & "', " & AgL.Chk_Text(SubGroupTable.AadharNo) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
        Else
            If UpdateIfExists = True Then
                SubGroupTable.SubCode = AgL.Dman_Execute("SELECT SubCode From Subgroup With (NoLock) where Name = " & AgL.Chk_Text(SubGroupTable.Name) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar
                SubGroupTable.AgentCode = AgL.Dman_Execute("SELECT SubCode From Subgroup With (NoLock) where Name = " & AgL.Chk_Text(SubGroupTable.AgentName) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar
                SubGroupTable.TransporterCode = AgL.Dman_Execute("SELECT SubCode From Subgroup With (NoLock) where Name = " & AgL.Chk_Text(SubGroupTable.TransporterName) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar

                mQry = " UPDATE Subgroup
                SET Address = " & AgL.Chk_Text(SubGroupTable.Address) & ",
	                PIN = " & AgL.Chk_Text(SubGroupTable.PIN) & ",
	                Phone = " & AgL.Chk_Text(SubGroupTable.Phone) & ",
	                Mobile = " & AgL.Chk_Text(SubGroupTable.Mobile) & ",
	                Email = " & AgL.Chk_Text(SubGroupTable.EMail) & ",
	                ContactPerson = " & AgL.Chk_Text(SubGroupTable.ContactPerson) & "
                WHERE Subcode = '" & SubGroupTable.SubCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = "UPDATE SubgroupSiteDivisionDetail 
                    Set Transporter = " & AgL.Chk_Text(SubGroupTable.TransporterCode) & ", 
                    Agent = " & AgL.Chk_Text(SubGroupTable.AgentCode) & "
                    WHERE Subcode = '" & SubGroupTable.SubCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mRegSr = AgL.Dman_Execute("Select IfNull(Max(Sr),0) from SubgroupRegistration With (NoLock) Where SubCode = '" & SubGroupTable.SubCode & "' ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()

                If SubGroupTable.SalesTaxNo <> "" Then
                    If AgL.Dman_Execute("Select Count(*) From SubgroupRegistration With (NoLock)
                            Where SubCode = '" & SubGroupTable.SubCode & "'
                            And Upper(RegistrationType) = '" & AgLibrary.ClsMain.agConstants.SubgroupRegistrationType.SalesTaxNo.ToString.ToUpper & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar() = 0 Then
                        mRegSr += 1
                        mQry = "Insert Into SubgroupRegistration(Subcode, SR, RegistrationType, RegistrationNo)
                            Values ('" & SubGroupTable.SubCode & "', " & mRegSr & ", '" & AgLibrary.ClsMain.agConstants.SubgroupRegistrationType.SalesTaxNo & "', " & AgL.Chk_Text(SubGroupTable.SalesTaxNo) & ") "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                End If


                If SubGroupTable.PANNo <> "" Then
                    If AgL.Dman_Execute("Select Count(*) From SubgroupRegistration With (NoLock)
                            Where SubCode = '" & SubGroupTable.SubCode & "'
                            And Upper(RegistrationType) = '" & AgLibrary.ClsMain.agConstants.SubgroupRegistrationType.PanNo.ToString.ToUpper & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar() = 0 Then

                        mRegSr += 1
                        mQry = "Insert Into SubgroupRegistration(Subcode, Sr, RegistrationType, RegistrationNo)
                       Values ('" & SubGroupTable.SubCode & "', " & mRegSr & ", '" & AgLibrary.ClsMain.agConstants.SubgroupRegistrationType.PanNo & "', " & AgL.Chk_Text(SubGroupTable.PANNo) & ") "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                End If

                If SubGroupTable.AadharNo <> "" Then
                    If AgL.Dman_Execute("Select Count(*) From SubgroupRegistration With (NoLock)
                            Where SubCode = '" & SubGroupTable.SubCode & "'
                            And Upper(RegistrationType) = '" & AgLibrary.ClsMain.agConstants.SubgroupRegistrationType.AadharNo.ToString.ToUpper & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar() = 0 Then

                        mRegSr += 1
                        mQry = "Insert Into SubgroupRegistration(Subcode, Sr, RegistrationType, RegistrationNo)
                       Values ('" & SubGroupTable.SubCode & "', " & mRegSr & ", '" & SubgroupRegistrationType.AadharNo.ToUpper & "', " & AgL.Chk_Text(SubGroupTable.AadharNo) & ") "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                End If
            Else
                mQry = " UPDATE Subgroup
                        SET OMSId = " & AgL.Chk_Text(SubGroupTable.OMSId) & "
                        WHERE Name = '" & SubGroupTable.Name & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
        End If
        'End If
        Return SubGroupTable.SubCode
    End Function
    Public Shared Sub ImportAcGroupTable(AcGroupTable As StructAcGroup)
        Dim mQry As String = ""
        If AgL.Dman_Execute("SELECT Count(*) From AcGroup With (NoLock) Where Upper(Replace(GroupName,' ','')) = Upper(Replace('" & AcGroupTable.GroupName & "',' ',''))", AgL.GcnRead).ExecuteScalar = 0 Then
            mQry = "SELECT GroupCode, GroupNature, Nature  From AcGroup With (NoLock) WHERE GroupName =  '" & AcGroupTable.GroupUnder & "'"
            Dim DtAcGroup As DataTable = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

            If (DtAcGroup.Rows.Count > 0) Then
                AcGroupTable.GroupUnder = DtAcGroup.Rows(0)("GroupCode")
                If AcGroupTable.GroupNature = "" Then AcGroupTable.GroupNature = DtAcGroup.Rows(0)("GroupNature")
                If AcGroupTable.Nature = "" Then AcGroupTable.Nature = DtAcGroup.Rows(0)("Nature")
            End If

            mQry = " INSERT INTO AcGroup(GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature,
                    SysGroup, OMSId, LockText, U_Name, U_EntDt, U_AE)
                    Select " & AgL.Chk_Text(AcGroupTable.GroupCode) & ", '" & AcGroupTable.SNo & "' As SNo, 
                    " & AgL.Chk_Text(AcGroupTable.GroupName) & " As GroupName, 
                    " & AgL.Chk_Text(AcGroupTable.ContraGroupName) & " As ContraGroupName, 
                    " & AgL.Chk_Text(AcGroupTable.GroupUnder) & " As GroupUnder, 
                    " & AgL.Chk_Text(AcGroupTable.GroupNature) & " As GroupNature, 
                    " & AgL.Chk_Text(AcGroupTable.Nature) & " As Nature,
                    " & AgL.Chk_Text(AcGroupTable.SysGroup) & " As SysGroup, 
                    " & AgL.Chk_Text(AcGroupTable.OMSId) & " As OMSId, 
                    " & AgL.Chk_Text(AcGroupTable.LockText) & " As LockText, 
                    " & AgL.Chk_Text(AcGroupTable.U_Name) & " As U_Name, 
                    " & AgL.Chk_Date(AcGroupTable.U_EntDt) & " As U_EntDt, 
                    " & AgL.Chk_Text(AcGroupTable.U_AE) & " As U_AE "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Else
            mQry = " UPDATE AcGroup Set OMSId = '" & AcGroupTable.OMSId & "' 
                    Where GroupName = '" & AcGroupTable.GroupName & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If
    End Sub
    Public Shared Sub ImportCityTable(CityTable As StructCity)
        Dim mQry As String = ""
        If AgL.Dman_Execute("SELECT Count(*) From City With (NoLock) Where CityName = '" & CityTable.CityName & "'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = " INSERT INTO City(CityCode, CityName, State, EntryBy, EntryDate, EntryType, EntryStatus, OMSId)
                    Select '" & CityTable.CityCode & "' As CityCode, " & AgL.Chk_Text(CityTable.CityName) & " As City, 
                    " & AgL.Chk_Text(CityTable.State) & " As State, 
                    '" & CityTable.EntryBy & "' As EntryBy, " & AgL.Chk_Date(CityTable.EntryDate) & " As EntryDate, 
                    '" & CityTable.EntryType & "' As EntryType, '" & CityTable.EntryStatus & "' As EntryStatus, 
                    '" & CityTable.OMSId & "' As OMSId "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Else
            mQry = " UPDATE City Set OMSId = '" & CityTable.OMSId & "' 
                    Where CityName = '" & CityTable.CityName & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If
    End Sub

    Private Sub FrmPerson_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        If AgL.XNull(Dgl1.Item(Col1Value, rowLockText).Value) <> "" Then
            MsgBox(AgL.XNull(Dgl1.Item(Col1Value, rowLockText).Value) & ", Can not modify")
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
                MsgBox(" Data Exists For Person " & Dgl1(Col1Value, rowName).Value & " In Sale Invoice . Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If

            mQry = " Select Count(*) From PurchInvoice Where Vendor = '" & mSearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                MsgBox(" Data Exists For Person " & Dgl1(Col1Value, rowName).Value & " In Purchase Invoice . Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If

            mQry = " Select Count(*) From Stock Where Subcode = '" & mSearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                MsgBox(" Data Exists For Person " & Dgl1(Col1Value, rowName).Value & " In Stock. Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If

            mQry = " Select Count(*) From Ledger Where Subcode = '" & mSearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                MsgBox(" Data Exists For Person " & Dgl1(Col1Value, rowName).Value & " In Ledger. Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If


            mQry = " Select Count(*) From LedgerHead Where Subcode = '" & mSearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                MsgBox(" Data Exists For Person " & Dgl1(Col1Value, rowName).Value & " In LedgerHead. Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If


            mQry = " Select Count(*) From LedgerHeadDetail Where Subcode = '" & mSearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                MsgBox(" Data Exists For Person " & Dgl1(Col1Value, rowName).Value & " In LedgerHeadDetail. Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If

            mQry = " Select Count(*) From StockHead Where Subcode = '" & mSearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                MsgBox(" Data Exists For Person " & Dgl1(Col1Value, rowName).Value & " In StockHead. Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If


        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData")
            FGetRelationalData = True
        End Try
    End Function

    Private Sub Dgl1_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Dim mQry As String
        Dim DtTemp As DataTable

        If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1Value).Index Then
            If Dgl1.Item(Col1Mandatory, Dgl1.CurrentCell.RowIndex).Value <> "" Then
                If Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Value = "" Then
                    MsgBox(Dgl1(Col1Head, Dgl1.CurrentCell.RowIndex).Value & " can not be blank.")
                    e.Cancel = True
                    Exit Sub
                End If
            End If

            Select Case Dgl1.CurrentCell.RowIndex
                Case rowSalesTaxNo
                    If ClsFunction.ValidateGstNo(Dgl1.Item(Col1Value, rowSalesTaxNo).Value, Dgl1(Col1Value, rowSalesTaxGroup).Tag, gStateCode, AgL.StrCmp(Dgl1(Col1Value, rowSubgroupType).Tag, AgLibrary.ClsMain.agConstants.SubgroupType.Transporter)) Then
                        If Dgl1.Item(Col1Value, rowSalesTaxNo).Value <> "" Then
                            Dgl1.Item(Col1Value, rowPanNo).Value = Dgl1.Item(Col1Value, rowSalesTaxNo).Value.ToString.Substring(2, 10)
                        End If
                    End If
                Case rowSalesTaxGroup
                    If Dgl1.Item(Col1Value, rowSalesTaxGroup).Value.ToString.ToUpper <> "REGISTERED" Then
                        Dgl1.Item(Col1Value, rowSalesTaxGroupRegType).Value = ""
                        Dgl1.Item(Col1Value, rowSalesTaxGroupRegType).Tag = ""
                    End If
                Case rowEmail
                    ValidateEMailId(Dgl1.Item(Col1Value, rowEmail).Value)
                Case rowPanNo
                    ValidatePanNo(Dgl1.Item(Col1Value, rowPanNo).Value)
                Case rowAadharNo
                    ValidateAadharNo(Dgl1.Item(Col1Value, rowAadharNo).Value)
                Case rowSubgroupType
                    ApplySubgroupTypeSetting(Dgl1(Col1Value, rowSubgroupType).Value)
                    Dgl1.Item(Col1Head, rowAgent).Tag = Nothing
                    Dgl1.CurrentCell = Dgl1(Col1Value, rowName)
                    Dgl1.Focus()
                Case rowAcGroup
                    If Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Value.ToString.Trim = "" Or Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Tag.Trim = "" Then
                        mGroupNature = ""
                        mNature = ""
                    Else
                        mQry = "Select GroupNature, Nature From AcGroup With (NoLock) Where GroupCode = '" & Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Tag & "'"
                        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtTemp.Rows.Count > 0 Then
                            mGroupNature = AgL.XNull(DtTemp.Rows(0)("GroupNature"))
                            mNature = AgL.XNull(DtTemp.Rows(0)("Nature"))
                        End If
                    End If
                    DisplayFieldsBasedOnNature(mNature)


                Case rowCity
                    Validate_City()
            End Select
        End If
    End Sub
    Private Sub Validate_City()
        Dim DtTemp As DataTable

        If Dgl1(Col1Value, rowCity).Value <> "" Then
            mQry = "Select ManualCode From State Where Code = (Select State From City Where CityCode = '" & Dgl1(Col1Value, rowCity).Tag & "')"
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTemp.Rows.Count > 0 Then
                gStateCode = AgL.XNull(DtTemp.Rows(0)("ManualCode"))
            Else
                MsgBox("State Code is not defined for selected city.")
            End If
        End If
    End Sub


    Private Sub FrmPerson_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Dim I As Integer
        For I = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1Head, I).Tag = Nothing
        Next
    End Sub

    Private Sub ShowDiscountDetail()
        Dim FrmObj As FrmPersonWiseDiscount

        If Dgl1.Item(Col1BtnDetail, rowDiscount).Tag IsNot Nothing Then
            FrmObj = Dgl1.Item(Col1BtnDetail, rowDiscount).Tag
            FrmObj.EntryMode = Topctrl1.Mode
            FrmObj.DtSubgroupTypeSettings = DtSubgroupTypeSettings
            FrmObj.StartPosition = FormStartPosition.CenterParent
            FrmObj.ShowDialog()
            Dgl1.Item(Col1BtnDetail, rowDiscount).Tag = FrmObj
        Else
            FrmObj = New FrmPersonWiseDiscount
            FrmObj.EntryMode = Topctrl1.Mode
            FrmObj.DtSubgroupTypeSettings = DtSubgroupTypeSettings
            FrmObj.IniGrid(mSearchCode)
            FrmObj.StartPosition = FormStartPosition.CenterParent
            FrmObj.ShowDialog()
            Dgl1.Item(Col1BtnDetail, rowDiscount).Tag = FrmObj
        End If
    End Sub


    Private Sub ShowExtraDiscountDetail()
        Dim FrmObj As FrmPersonWiseExtraDiscount

        If Dgl1.Item(Col1BtnDetail, rowExtraDiscount).Tag IsNot Nothing Then
            FrmObj = Dgl1.Item(Col1BtnDetail, rowExtraDiscount).Tag
            FrmObj.EntryMode = Topctrl1.Mode
            FrmObj.DtSubgroupTypeSettings = DtSubgroupTypeSettings
            FrmObj.StartPosition = FormStartPosition.CenterParent
            FrmObj.ShowDialog()
            Dgl1.Item(Col1BtnDetail, rowExtraDiscount).Tag = FrmObj
        Else
            FrmObj = New FrmPersonWiseExtraDiscount
            FrmObj.EntryMode = Topctrl1.Mode
            FrmObj.DtSubgroupTypeSettings = DtSubgroupTypeSettings
            FrmObj.IniGrid(mSearchCode)
            FrmObj.StartPosition = FormStartPosition.CenterParent
            FrmObj.ShowDialog()
            Dgl1.Item(Col1BtnDetail, rowExtraDiscount).Tag = FrmObj
        End If
    End Sub


    Private Sub ShowRateTypeDetail()
        Dim FrmObj As FrmPersonSiteRateType
        If Dgl1.Item(Col1BtnDetail, rowRateType).Tag IsNot Nothing Then
            FrmObj = Dgl1.Item(Col1BtnDetail, rowRateType).Tag
            FrmObj.EntryMode = Topctrl1.Mode
            FrmObj.DataValidation = False
            FrmObj.DtSubgroupTypeSettings = DtSubgroupTypeSettings
            FrmObj.StartPosition = FormStartPosition.CenterParent
            FrmObj.ShowDialog()
            If FrmObj.DataValidation = True Then
                Dgl1.Item(Col1BtnDetail, rowRateType).Tag = FrmObj
                Dgl1.Item(Col1BtnDetail, rowRateType).Value = 2
                Dgl1.Item(Col1Value, rowRateType).Value = ""
                Dgl1.Item(Col1Value, rowRateType).Tag = ""
            Else
                Dgl1.Item(Col1BtnDetail, rowRateType).Tag = Nothing
                Dgl1.Item(Col1BtnDetail, rowRateType).Value = ""
            End If
        Else
            FrmObj = New FrmPersonSiteRateType
            FrmObj.EntryMode = Topctrl1.Mode
            FrmObj.DataValidation = False
            FrmObj.DtSubgroupTypeSettings = DtSubgroupTypeSettings
            FrmObj.IniGrid(mSearchCode)
            FrmObj.StartPosition = FormStartPosition.CenterParent
            FrmObj.ShowDialog()
            If FrmObj.DataValidation = True Then
                Dgl1.Item(Col1BtnDetail, rowRateType).Tag = FrmObj
                Dgl1.Item(Col1BtnDetail, rowRateType).Value = 2
                Dgl1.Item(Col1Value, rowRateType).Value = ""
                Dgl1.Item(Col1Value, rowRateType).Tag = ""
            Else
                Dgl1.Item(Col1BtnDetail, rowRateType).Tag = Nothing
                Dgl1.Item(Col1BtnDetail, rowRateType).Value = ""
            End If
        End If
    End Sub



    Private Sub ShowTransporterDetail()
        Dim FrmObj As FrmPersonSiteTransporter
        If Dgl1.Item(Col1BtnDetail, rowTransporter).Tag IsNot Nothing Then
            FrmObj = Dgl1.Item(Col1BtnDetail, rowTransporter).Tag
            FrmObj.EntryMode = Topctrl1.Mode
            FrmObj.DataValidation = False
            FrmObj.DtSubgroupTypeSettings = DtSubgroupTypeSettings
            FrmObj.StartPosition = FormStartPosition.CenterParent
            FrmObj.ShowDialog()
            If FrmObj.DataValidation = True Then
                Dgl1.Item(Col1BtnDetail, rowTransporter).Tag = FrmObj
                Dgl1.Item(Col1BtnDetail, rowTransporter).Value = 2
                Dgl1.Item(Col1Value, rowTransporter).Value = ""
                Dgl1.Item(Col1Value, rowTransporter).Tag = ""
            Else
                Dgl1.Item(Col1BtnDetail, rowTransporter).Tag = Nothing
                Dgl1.Item(Col1BtnDetail, rowTransporter).Value = ""
            End If
        Else
            FrmObj = New FrmPersonSiteTransporter
            FrmObj.EntryMode = Topctrl1.Mode
            FrmObj.DataValidation = False
            FrmObj.DtSubgroupTypeSettings = DtSubgroupTypeSettings
            FrmObj.IniGrid(mSearchCode)
            FrmObj.StartPosition = FormStartPosition.CenterParent
            FrmObj.ShowDialog()
            If FrmObj.DataValidation = True Then
                Dgl1.Item(Col1BtnDetail, rowTransporter).Tag = FrmObj
                Dgl1.Item(Col1BtnDetail, rowTransporter).Value = 2
                Dgl1.Item(Col1Value, rowTransporter).Value = ""
                Dgl1.Item(Col1Value, rowTransporter).Tag = ""
            Else
                Dgl1.Item(Col1BtnDetail, rowTransporter).Tag = Nothing
                Dgl1.Item(Col1BtnDetail, rowTransporter).Value = ""
            End If
        End If
    End Sub

    Private Sub ShowAgentDetail()
        Dim FrmObj As FrmPersonSiteAgent
        If Dgl1.Item(Col1BtnDetail, rowAgent).Tag IsNot Nothing Then
            FrmObj = Dgl1.Item(Col1BtnDetail, rowAgent).Tag
            FrmObj.EntryMode = Topctrl1.Mode
            FrmObj.SubgroupType = Dgl1.Item(Col1BtnDetail, rowSubgroupType).Tag
            FrmObj.DataValidation = False
            FrmObj.DtSubgroupTypeSettings = DtSubgroupTypeSettings
            FrmObj.StartPosition = FormStartPosition.CenterParent
            FrmObj.ShowDialog()
            If FrmObj.DataValidation = True Then
                Dgl1.Item(Col1BtnDetail, rowAgent).Tag = FrmObj
                Dgl1.Item(Col1BtnDetail, rowAgent).Value = 2
                Dgl1.Item(Col1Value, rowAgent).Value = ""
                Dgl1.Item(Col1Value, rowAgent).Tag = ""
            Else
                Dgl1.Item(Col1BtnDetail, rowAgent).Tag = Nothing
                Dgl1.Item(Col1BtnDetail, rowAgent).Value = ""
            End If
        Else
            FrmObj = New FrmPersonSiteAgent
            FrmObj.EntryMode = Topctrl1.Mode
            FrmObj.DataValidation = False
            FrmObj.DtSubgroupTypeSettings = DtSubgroupTypeSettings
            FrmObj.SubgroupType = Dgl1.Item(Col1BtnDetail, rowSubgroupType).Tag
            FrmObj.IniGrid(mSearchCode)
            FrmObj.StartPosition = FormStartPosition.CenterParent
            FrmObj.ShowDialog()
            If FrmObj.DataValidation = True Then
                Dgl1.Item(Col1BtnDetail, rowAgent).Tag = FrmObj
                Dgl1.Item(Col1BtnDetail, rowAgent).Value = 2
                Dgl1.Item(Col1Value, rowAgent).Value = ""
                Dgl1.Item(Col1Value, rowAgent).Tag = ""
            Else
                Dgl1.Item(Col1BtnDetail, rowAgent).Tag = Nothing
                Dgl1.Item(Col1BtnDetail, rowAgent).Value = ""
            End If
        End If
    End Sub
    Private Sub ShowInterestSlabDetail()
        Dim FrmObj As FrmPersonItemGroupInterest

        If Dgl1.Item(Col1BtnDetail, rowInterestSlab).Tag IsNot Nothing Then
            FrmObj = Dgl1.Item(Col1BtnDetail, rowInterestSlab).Tag
            FrmObj.EntryMode = Topctrl1.Mode
            FrmObj.DtSubgroupTypeSettings = DtSubgroupTypeSettings
            FrmObj.StartPosition = FormStartPosition.CenterParent
            FrmObj.ShowDialog()
            Dgl1.Item(Col1BtnDetail, rowInterestSlab).Tag = FrmObj
        Else
            FrmObj = New FrmPersonItemGroupInterest
            FrmObj.EntryMode = Topctrl1.Mode
            FrmObj.DtSubgroupTypeSettings = DtSubgroupTypeSettings
            FrmObj.IniGrid(mSearchCode)
            FrmObj.StartPosition = FormStartPosition.CenterParent
            FrmObj.ShowDialog()
            Dgl1.Item(Col1BtnDetail, rowInterestSlab).Tag = FrmObj
        End If
    End Sub

    Private Sub FrmPerson_BaseEvent_ApproveDeletion_InTrans(SearchCode As String, Conn As Object, Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = "Delete from SubgroupRegistration Where Subcode = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        mQry = "Delete from PersonDiscount Where Person = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete from PersonAddition Where Person = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete from PersonExtraDiscount Where Person = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub Dgl1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        If e.ColumnIndex = Dgl1.Columns(Col1BtnDetail).Index And TypeOf (Dgl1(Col1BtnDetail, e.RowIndex)) Is DataGridViewButtonCell Then
            Select Case e.RowIndex
                Case rowInterestSlab
                    ShowInterestSlabDetail()
                Case rowDiscount
                    ShowDiscountDetail()
                Case rowExtraDiscount
                    ShowExtraDiscountDetail()
                Case rowRateType
                    ShowRateTypeDetail()
                Case rowTransporter
                    ShowTransporterDetail()
                Case rowAgent
                    ShowAgentDetail()

            End Select
        End If
    End Sub

    Private Sub Dgl1_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles Dgl1.CellBeginEdit
        Dim mRow As Integer
        mRow = Dgl1.CurrentCell.RowIndex
        If Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name = Col1Value Then
            Select Case mRow
                Case rowAgent, rowTransporter, rowRateType
                    If Val(Dgl1.Item(Col1BtnDetail, mRow).Value) > 1 Then
                        e.Cancel = True
                    End If
                Case rowShowAccountInOtherDivisions, rowShowAccountInOtherSites, rowWeekOffDays, rowProcesses, rowBlockedTransactions
                    e.Cancel = True
                Case rowSalesTaxGroupRegType
                    If Dgl1.Item(Col1Value, rowSalesTaxGroup).Value.ToString.ToUpper <> "REGISTERED" Then
                        e.Cancel = True
                    End If

            End Select
        End If
    End Sub

    Public Structure StructSubGroupTable
        Dim SubCode As String
        Dim Site_Code As String
        Dim Name As String
        Dim DispName As String
        Dim AccountGroup As String
        Dim GroupCode As String
        Dim GroupNature As String
        Dim ManualCode As String
        Dim Nature As String
        Dim Address As String
        Dim CityCode As String
        Dim CityName As String
        Dim AreaCode As String
        Dim AreaName As String
        Dim StateName As String
        Dim PIN As String
        Dim Phone As String
        Dim ContactPerson As String
        Dim SubgroupType As String
        Dim Mobile As String
        Dim CreditDays As String
        Dim CreditLimit As String
        Dim EMail As String
        Dim Parent As String
        Dim SalesTaxPostingGroup As String
        Dim EntryBy As String
        Dim EntryDate As String
        Dim EntryType As String
        Dim EntryStatus As String
        Dim Div_Code As String
        Dim Status As String
        Dim SalesTaxNo As String
        Dim PANNo As String
        Dim AadharNo As String
        Dim TransporterCode As String
        Dim TransporterName As String
        Dim AgentCode As String
        Dim AgentName As String
        Dim InterestSlab As String
        Dim OMSId As String
        Dim LockText As String
        Dim Remark As String
        Dim Cnt As Integer
    End Structure

    Public Structure StructCity
        Dim CityCode As String
        Dim CityName As String
        Dim State As String
        Dim EntryBy As String
        Dim EntryDate As String
        Dim EntryType As String
        Dim EntryStatus As String
        Dim OMSId As String
    End Structure

    Public Structure StructAcGroup
        Dim GroupCode As String
        Dim SNo As String
        Dim GroupName As String
        Dim ContraGroupName As String
        Dim GroupUnder As String
        Dim GroupNature As String
        Dim Nature As String
        Dim SysGroup As String
        Dim LockText As String
        Dim OMSId As String
        Dim U_Name As String
        Dim U_EntDt As String
        Dim U_AE As String
    End Structure

    Private Sub BtnAttachments_Click(sender As Object, e As EventArgs) Handles BtnAttachments.Click
        Dim FrmObj As New AgLibrary.FrmAttachmentViewer(AgL)
        FrmObj.LblDocNo.Text = "Party Name : " + Dgl1(Col1Value, rowName).Value
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
    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.KeyDown
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        If ClsMain.IsSpecialKeyPressed(e) Then Exit Sub

        If Topctrl1.Mode.ToUpper <> "BROWSE" Then
            If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1Value).Index Then
                If e.KeyCode = Keys.Delete Then
                    Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Value = ""
                    Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Tag = ""
                End If

                Select Case Dgl1.CurrentCell.RowIndex
                    Case rowCity
                        If e.KeyCode = Keys.Insert Then
                            FOpenCityMaster()
                        End If
                    Case rowShowAccountInOtherDivisions
                        If Not IsSpecialKeyPressed(e) Then
                            If e.KeyCode = Keys.N Then
                                Dgl1.Item(Col1Value, rowShowAccountInOtherDivisions).Value = "NO"
                            Else
                                Dgl1.Item(Col1Value, rowShowAccountInOtherDivisions).Value = "YES"
                            End If
                        End If
                    Case rowShowAccountInOtherSites
                        If Not IsSpecialKeyPressed(e) Then
                            If e.KeyCode = Keys.N Then
                                Dgl1.Item(Col1Value, rowShowAccountInOtherSites).Value = "NO"
                            Else
                                Dgl1.Item(Col1Value, rowShowAccountInOtherSites).Value = "YES"
                            End If
                        End If
                    Case rowWeekOffDays
                        If e.KeyCode <> Keys.Enter And e.Control = False And e.Alt = False Then
                            Dgl1.Item(Col1Value, Dgl1.CurrentCell.RowIndex).Value = FHPGD_WeekOffDays()
                        End If
                    Case rowProcesses
                        FHPGD_Process(Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Tag, Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Value)
                    Case rowProcessScopeOfWork
                        FHPGD_ProcessScopeOfWork(Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Tag, Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Value)
                    Case rowCombinationOfProcesses
                        FHPGD_Process(Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Tag, Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Value)
                    Case rowBlockedTransactions
                        FHPGD_BlockedTransactions(Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Tag, Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Value)
                    Case rowDivisionScopeOfWork
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = ClsMain.GetStringsFromClassConstants(GetType(IndustryType)).ToUpper.Replace("SELECT ", "Select 'o' As Tick, ")
                            mQry += " UNION ALL "
                            mQry += ClsMain.GetStringsFromClassConstants(GetType(IndustryType.SubIndustryType)).ToUpper.Replace("SELECT ", "Select 'o' As Tick, ")
                            mQry += " UNION ALL "
                            mQry += ClsMain.GetStringsFromClassConstants(GetType(IndustryType.CommonModules)).ToUpper.Replace("SELECT ", "Select 'o' As Tick, ")


                            If AgL.XNull(Dgl1.Item(Col1Value, rowDivisionScopeOfWork).Value) <> "" Then
                                Dim ScopeOfWorkArr As String() = Dgl1.Item(Col1Value, rowDivisionScopeOfWork).Value.ToString.Split("+")
                                For I As Integer = 0 To ScopeOfWorkArr.Length - 1
                                    If mQry.ToLower.Contains("select 'o' as tick, '+" & ScopeOfWorkArr(I).ToLower & "' as code") Then
                                        mQry = mQry.ToLower.Replace("select 'o' as tick, '+" & ScopeOfWorkArr(I).ToLower & "' as code", "Select 'þ' As Tick, '+" & ScopeOfWorkArr(I).ToLower & "' As Code ")
                                    End If
                                Next
                            End If
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
                        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CType(Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag, DataSet).Tables(0)), "", 400, 400, , , False)
                        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
                        FRH_Multiple.FFormatColumn(1, , 0, , False)
                        FRH_Multiple.FFormatColumn(2, "Description", 250, DataGridViewContentAlignment.MiddleLeft)
                        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
                        FRH_Multiple.ShowDialog()

                        If FRH_Multiple.BytBtnValue = 0 Then
                            If FRH_Multiple.FFetchData(1, "'", "'", "", True) <> "" Then
                                Dgl1.Item(Col1Value, Dgl1.CurrentCell.RowIndex).Tag = FRH_Multiple.FFetchData(1, "", "", "", True)
                                Dgl1.Item(Col1Value, Dgl1.CurrentCell.RowIndex).Value = FRH_Multiple.FFetchData(2, "", "", "", True)
                            Else
                                Dgl1.Item(Col1Value, Dgl1.CurrentCell.RowIndex).Tag = ""
                                Dgl1.Item(Col1Value, Dgl1.CurrentCell.RowIndex).Value = ""
                            End If
                            Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
                            Dgl1.AutoResizeRow(Dgl1.CurrentCell.RowIndex, DataGridViewAutoSizeRowMode.AllCells)
                        End If

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


    Private Function FHPGD_WeekOffDays() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtTemp As DataTable

        mQry = " Select 'o' As Tick, 'Sunday' as Code, 'Sunday' As Days 
                Union All  Select 'o' As Tick, 'Saturday' as Code, 'Saturday' As Days 
                Union All  Select 'o' As Tick, 'Friday' as Code, 'Friday' As Days 
                Union All  Select 'o' As Tick, 'Thursday' as Code, 'Thursday' As Days 
                Union All  Select 'o' As Tick, 'Wednesday' as Code, 'Wednesday' As Days 
                Union All  Select 'o' As Tick, 'Tuesday' as Code, 'Tuesday' As Days 
                Union All  Select 'o' As Tick, 'Monday' as Code, 'Monday' As Days                                                                          
                "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 300, 330, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Days", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            FHPGD_WeekOffDays = FRH_Multiple.FFetchData(2, "", "", ",")
        Else
            FHPGD_WeekOffDays = ""
        End If
        If FHPGD_WeekOffDays = "All" Then FHPGD_WeekOffDays = ""

        FRH_Multiple = Nothing
    End Function

    Private Sub FHPGD_Process(ByRef bTag As String, ByRef bValue As String)
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtTemp As DataTable

        mQry = " SELECT 'o' As Tick, Sg.SubCode AS Code, Sg.Name, Parent.Name as ParentName 
                FROM Subgroup Sg With (NoLock)
                Left Join Subgroup Parent On Parent.Subcode = Sg.Parent
                Where Sg.SubgroupType = '" & AgLibrary.ClsMain.agConstants.SubgroupType.Process & "' 
                And IfNull(Sg.Status,'Active') = 'Active' And Sg.Subcode Not In ('" & Process.Purchase & "', '" & Process.Sales & "')"
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 400, 530, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Parent Name", 200, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            bTag = FRH_Multiple.FFetchData(1, "", "", ",", True)
            bValue = FRH_Multiple.FFetchData(2, "", "", ",", True)
        End If
        FRH_Multiple = Nothing
    End Sub

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
    Private Sub Dgl1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles Dgl1.EditingControlShowing
        If FGetSettings(SettingFields.DefaultTextCaseInMasters, SettingType.General) = TextCase.Upper Then

            DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Upper
        ElseIf FGetSettings(SettingFields.DefaultTextCaseInMasters, SettingType.General) = TextCase.Lower Then
            DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Lower
        End If
    End Sub
    Public Shared Sub ImportPersonExtraDiscount(PersonExtraDiscountTable As StructPersonExtraDiscount)
        Dim mQry As String = ""
        If AgL.VNull(AgL.Dman_Execute(" Select Count(*) From PersonExtraDiscount Igp 
                Where IfNull(Igp.ItemCategory,'') = '" & PersonExtraDiscountTable.ItemCategory & "'
                And IfNull(Igp.ItemGroup,'') = '" & PersonExtraDiscountTable.ItemGroup & "'
                And IfNull(Igp.Person,'') = '" & PersonExtraDiscountTable.Person & "'", AgL.GCn).ExecuteScalar()) = 0 Then
            mQry = "INSERT INTO PersonExtraDiscount (ItemCategory, ItemGroup, Person, 
                    ExtraDiscountCalculationPattern, ExtraDiscountPer)
                    Select " & AgL.Chk_Text(PersonExtraDiscountTable.ItemCategory) & " As ItemCategory, 
                    " & AgL.Chk_Text(PersonExtraDiscountTable.ItemGroup) & " As ItemGroup, 
                    " & AgL.Chk_Text(PersonExtraDiscountTable.Person) & " As Person, 
                    " & AgL.Chk_Text(PersonExtraDiscountTable.ExtraDiscountCalculationPattern) & " As DiscountCalculationPattern, 
                    " & Val(PersonExtraDiscountTable.ExtraDiscountPer) & " As DiscountPer "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Else
            mQry = " UPDATE PersonExtraDiscount
                    Set ExtraDiscountCalculationPattern = " & AgL.Chk_Text(PersonExtraDiscountTable.ExtraDiscountCalculationPattern) & ", 
                    ExtraDiscountPer = " & Val(PersonExtraDiscountTable.ExtraDiscountPer) & " 
                    Where IfNull(ItemCategory,'') = '" & PersonExtraDiscountTable.ItemCategory & "'
                    And IfNull(ItemGroup,'') = '" & PersonExtraDiscountTable.ItemGroup & "'
                    And IfNull(Person,'') = '" & PersonExtraDiscountTable.Person & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If
    End Sub


    Public Structure StructPersonExtraDiscount
        Dim ItemCategory As String
        Dim ItemGroup As String
        Dim Person As String
        Dim ExtraDiscountCalculationPattern As String
        Dim ExtraDiscountPer As String
        Dim OMSId As String
    End Structure
End Class
