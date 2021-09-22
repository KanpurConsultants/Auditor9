Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain.ConfigurableFields

Public Class FrmVoucher_Type

    Inherits AgTemplate.TempMaster

    Dim mQry$
    Public Const ColSNo As String = "SNo"


    Public WithEvents DglMain As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1LastValue As String = "Last Value"
    Public Const Col1HeadOriginal As String = "Head Original"


    Dim rowNCat As Integer = 0
    Dim rowCategory As Integer = 1
    Dim rowV_Type As Integer = 2
    Dim rowDescription As Integer = 3
    Dim rowPrintingDescription As Integer = 4
    Dim rowShort_Name As Integer = 5
    Dim rowMnuAttachedInModule As Integer = 6
    Dim rowMnuName As Integer = 7
    Dim rowMnuText As Integer = 8
    Dim rowNature As Integer = 9
    Dim rowManualRefType As Integer = 10
    Dim rowVoucherTypeTags As Integer = 11
    Dim rowIsFutureDateTransactionAllowed As Integer = 12
    Dim rowStructure As Integer = 13
    Dim rowCustomUI As Integer = 14
    Dim rowSiteList As Integer = 15
    Dim rowDivisionList As Integer = 16
    Dim rowIsPostInLedger As Integer = 17


    Public Const hcNCat As String = "NCat"
    Public Const hcCategory As String = "Category"
    Public Const hcV_Type As String = "V_Type"
    Public Const hcDescription As String = "Description"
    Public Const hcPrintingDescription As String = "Printing Description"
    Public Const hcShort_Name As String = "Short_Name"
    Public Const hcMnuAttachedInModule As String = "Menu Attached In Module"
    Public Const hcMnuName As String = "Menu Name"
    Public Const hcMnuText As String = "Menu Text"
    Public Const hcNature As String = "Nature"
    Public Const hcManualRefType As String = "Manual Ref Type"
    Public Const hcVoucherTypeTags As String = "Voucher Type Tags"
    Public Const hcIsFutureDateTransactionAllowed As String = "Is Future Date Transaction Allowed"
    Public Const hcStructure As String = "Structure"
    Public Const hcCustomUI As String = "Custom UI"
    Public Const hcSiteList As String = "Site List"
    Public Const hcDivisionList As String = "Division List"
    Public Const hcIsPostInLedger As String = "Is Post In Ledger"


    Friend WithEvents Pnl1 As Panel
#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.GrpUP.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.GroupBox1.Location = New System.Drawing.Point(0, 471)
        Me.GroupBox1.Size = New System.Drawing.Size(1016, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(14, 475)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(200, 536)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(228, 475)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(401, 475)
        Me.GBoxApprove.Text = "Approved By"
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Location = New System.Drawing.Point(3, 23)
        Me.TxtApproveBy.Size = New System.Drawing.Size(136, 18)
        Me.TxtApproveBy.Tag = ""
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(704, 475)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(465, 475)
        Me.GBoxDivision.Size = New System.Drawing.Size(136, 44)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Size = New System.Drawing.Size(130, 18)
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
        Me.Pnl1.Location = New System.Drawing.Point(0, 43)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(972, 426)
        Me.Pnl1.TabIndex = 1
        '
        'FrmVoucher_Type
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(974, 519)
        Me.Controls.Add(Me.Pnl1)
        Me.MaximizeBox = True
        Me.Name = "FrmVoucher_Type"
        Me.Text = "Cheque Format"
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
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
        Me.ResumeLayout(False)

    End Sub
#End Region

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer
        DglMain.EndEdit()

        For I = 0 To DglMain.RowCount - 1
            If DglMain(Col1Mandatory, I).Value <> "" And DglMain.Rows(I).Visible Then
                If DglMain(Col1Value, I).Value.ToString = "" Then
                    MsgBox(DglMain(Col1Head, I).Value & " can not be blank.")
                    DglMain.CurrentCell = DglMain(Col1Value, I)
                    DglMain.Focus()
                    passed = False
                    Exit Sub
                End If
            End If
        Next

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From Voucher_Type Where V_Type='" & DglMain.Item(Col1Value, rowV_Type).Value & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")

            mQry = "Select count(*) From Voucher_Type Where Description='" & DglMain.Item(Col1Value, rowDescription).Value & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        Else
            mQry = "Select count(*) From Voucher_Type Where V_Type='" & DglMain.Item(Col1Value, rowV_Type).Value & "' And V_Type<>'" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")

            mQry = "Select count(*) From Voucher_Type Where Description='" & DglMain.Item(Col1Value, rowDescription).Value & "' And V_Type<>'" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If


        For I = 0 To DglMain.Rows.Count - 1
            If DglMain.Item(Col1Value, I).Value = Nothing Then DglMain.Item(Col1Value, I).Value = ""
            If DglMain.Item(Col1Value, I).Tag = Nothing Then DglMain.Item(Col1Value, I).Tag = ""
        Next

        mSearchCode = DglMain.Item(Col1Value, rowV_Type).Value
        mInternalCode = mSearchCode
    End Sub
    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        AgL.PubFindQry = "SELECT I.V_Type As SearchCode, I.Description As Description  
                        FROM Voucher_Type I  "
        AgL.PubFindQryOrdBy = "[Description]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Voucher_Type"
        PrimaryField = "V_Type"
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        mQry = "UPDATE Voucher_Type 
                Set 
                NCat = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowNCat).Value) & ", 
                Category = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowCategory).Value) & ", 
                V_Type = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowV_Type).Value) & ", 
                Description = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowDescription).Value) & ", 
                PrintingDescription = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowPrintingDescription).Value) & ", 
                Short_Name = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowShort_Name).Value) & ",
                MnuAttachedInModule = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowMnuAttachedInModule).Value) & ",
                MnuName = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowMnuName).Value) & ",
                MnuText = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowMnuText).Value) & ",
                Nature = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowNature).Value) & ",
                ManualRefType = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowManualRefType).Value) & ",
                Structure = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowStructure).Tag) & ",
                CustomUI = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowCustomUI).Value) & ",
                VoucherTypeTags = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowVoucherTypeTags).Value) & ", 
                IsFutureDateTransactionAllowed = " & IIf(DglMain.Item(Col1Value, rowIsFutureDateTransactionAllowed).Value = "Yes", 1, 0) & ", 
                IsPostInLedger = " & IIf(DglMain.Item(Col1Value, rowIsPostInLedger).Value = "Yes", 1, 0) & ", 
                SiteList = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSiteList).Tag) & " ,
                DivisionList = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowDivisionList).Tag) & " 
                Where V_Type = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        If AgL.VNull(AgL.Dman_Execute("SELECT Count(*) AS Cnt FROM Voucher_Prefix With (NoLock) WHERE V_Type = '" & DglMain.Item(Col1Value, rowV_Type).Value & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) = 0 Then
            Dim bExistingV_Type As String = AgL.XNull(AgL.Dman_Execute("SELECT DISTINCT Vt.V_Type 
                    FROM Voucher_Type Vt With (NoLock) 
                    LEFT JOIN Voucher_Prefix Vp With (NoLock) ON Vt.V_Type = Vp.V_Type 
                    WHERE Vt.NCat = '" & DglMain.Item(Col1Value, rowNCat).Value & "'
                    AND Vp.V_Type IS NOT NULL", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())

            If bExistingV_Type <> "" Then
                mQry = "INSERT INTO Voucher_Prefix (V_Type, Date_From, Prefix, Start_Srl_No, Date_To, Comp_Code, Site_Code, Div_Code, UpLoadDate, Status_Add, Status_Edit, Status_Delete, Status_Print, Ref_Prefix, Ref_PadLength)
                        SELECT '" & DglMain.Item(Col1Value, rowV_Type).Value & "' As V_Type, Date_From, Prefix, 0 AS Start_Srl_No, Date_To, Comp_Code, Site_Code, Div_Code, UpLoadDate, Status_Add, Status_Edit, Status_Delete, Status_Print, Ref_Prefix, Ref_PadLength
                        FROM Voucher_Prefix With (NoLock) 
                        WHERE V_Type = '" & bExistingV_Type & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
        End If
    End Sub
    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = "Select H.*, S.Description As StructureDesc  " &
            " From Voucher_Type H " &
            " LEFT JOIN Structure S On H.Structure = S.Code " &
            " Where H.V_Type='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("V_Type"))
                DglMain.Item(Col1Value, rowNCat).Value = AgL.XNull(.Rows(0)("NCat"))
                DglMain.Item(Col1Value, rowCategory).Value = AgL.XNull(.Rows(0)("Category"))
                DglMain.Item(Col1Value, rowV_Type).Value = AgL.XNull(.Rows(0)("V_Type"))
                DglMain.Item(Col1Value, rowDescription).Value = AgL.XNull(.Rows(0)("Description"))
                DglMain.Item(Col1Value, rowPrintingDescription).Value = AgL.XNull(.Rows(0)("PrintingDescription"))
                DglMain.Item(Col1Value, rowShort_Name).Value = AgL.XNull(.Rows(0)("Short_Name"))
                DglMain.Item(Col1Value, rowMnuAttachedInModule).Value = AgL.XNull(.Rows(0)("MnuAttachedInModule"))
                DglMain.Item(Col1Value, rowMnuName).Value = AgL.XNull(.Rows(0)("MnuName"))
                DglMain.Item(Col1Value, rowMnuText).Value = AgL.XNull(.Rows(0)("MnuText"))
                DglMain.Item(Col1Value, rowNature).Value = AgL.XNull(.Rows(0)("Nature"))
                DglMain.Item(Col1Value, rowManualRefType).Value = AgL.XNull(.Rows(0)("ManualRefType"))
                DglMain.Item(Col1Value, rowStructure).Tag = AgL.XNull(.Rows(0)("Structure"))
                DglMain.Item(Col1Value, rowStructure).Value = AgL.XNull(.Rows(0)("StructureDesc"))
                DglMain.Item(Col1Value, rowCustomUI).Value = AgL.XNull(.Rows(0)("CustomUI"))
                DglMain.Item(Col1Value, rowVoucherTypeTags).Value = AgL.XNull(.Rows(0)("VoucherTypeTags"))
                DglMain.Item(Col1Value, rowIsFutureDateTransactionAllowed).Value = IIf(AgL.VNull(.Rows(0)("IsFutureDateTransactionAllowed")) = 0, "No", "Yes")
                DglMain.Item(Col1Value, rowIsPostInLedger).Value = IIf(AgL.VNull(.Rows(0)("IsPostInLedger")) = 0, "No", "Yes")
                DglMain.Item(Col1Value, rowSiteList).Tag = AgL.XNull(.Rows(0)("SiteList"))
                If AgL.XNull(DglMain.Item(Col1Value, rowSiteList).Tag) <> "" Then
                    mQry = " Select Name From SiteMast Where Code In ('" & AgL.XNull(DglMain.Item(Col1Value, rowSiteList).Tag).ToString.Replace("+", "','") & "')"
                    Dim DtSites As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    For I As Integer = 0 To DtSites.Rows.Count - 1
                        DglMain.Item(Col1Value, rowSiteList).Value += "+" + AgL.XNull(DtSites.Rows(I)("Name"))
                    Next
                End If

                DglMain.Item(Col1Value, rowDivisionList).Tag = AgL.XNull(.Rows(0)("DivisionList"))
                If AgL.XNull(DglMain.Item(Col1Value, rowDivisionList).Tag) <> "" Then
                    mQry = " Select Div_Name From Division Where Div_Code In ('" & AgL.XNull(DglMain.Item(Col1Value, rowDivisionList).Tag).ToString.Replace("+", "','") & "')"
                    Dim DtDivisions As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    For I As Integer = 0 To DtDivisions.Rows.Count - 1
                        DglMain.Item(Col1Value, rowDivisionList).Value += "+" + AgL.XNull(DtDivisions.Rows(I)("Div_Name"))
                    Next
                End If
            End If
        End With
    End Sub
    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        DglMain.CurrentCell = DglMain(Col1Value, rowNCat)
        DglMain.Focus()
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub

    Private Sub FrmYarn_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mConStr$ = ""
        mQry = "Select I.V_Type As SearchCode " &
                " From Voucher_Type I " &
                " Order By I.V_Type "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub
    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub
    Private Sub Frm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 325, 885)
    End Sub
    Private Sub TxtItemCategory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
        End If
    End Sub
    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim DtTemp As DataTable = Nothing
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.NAME


            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        'Passed = FRestrictSystemDefine()
    End Sub

    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        Passed = Not FGetRelationalData()
    End Sub

    Private Function FGetRelationalData() As Boolean
        Try
            'mQry = " Select Count(*) From Voucher_Type Where V_Type = '" & mSearchCode & "'"
            'If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
            '    MsgBox(" Data Exists For ItemGroup " & Dgl1(Col1Value, rowDescription).Value & " In Item Master. Can't Delete Entry", MsgBoxStyle.Information)
            '    FGetRelationalData = True
            '    Exit Function
            'End If
        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData")
            FGetRelationalData = True
        End Try
    End Function
    Private Sub Frm_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        ApplyUISetting()
        DglMain.CurrentCell = DglMain(Col1Value, rowNCat) 'Dgl1.FirstDisplayedCell
        DglMain.Focus()
    End Sub
    Private Sub Frm_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dim I As Integer
        DglMain.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DglMain, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(DglMain, Col1Head, 300, 255, Col1Head, True, True)
            .AddAgTextColumn(DglMain, Col1HeadOriginal, 180, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(DglMain, Col1Mandatory, 12, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(DglMain, Col1Value, 640, 255, Col1Value, True, False)
            .AddAgTextColumn(DglMain, Col1LastValue, 250, 255, Col1LastValue, False, False)
        End With
        AgL.AddAgDataGrid(DglMain, Pnl1)
        DglMain.Columns(Col1Mandatory).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings 2", 5.25, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        DglMain.Columns(Col1Mandatory).DefaultCellStyle.ForeColor = Color.Red
        DglMain.EnableHeadersVisualStyles = False
        DglMain.ColumnHeadersHeight = 35
        DglMain.AgSkipReadOnlyColumns = True
        DglMain.AllowUserToAddRows = False
        DglMain.RowHeadersVisible = False
        DglMain.ColumnHeadersVisible = False
        DglMain.BackgroundColor = Me.BackColor
        AgL.GridDesign(DglMain)
        DglMain.Name = "DglMain"
        DglMain.Anchor = AnchorStyles.Top + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom



        DglMain.Rows.Add(18)

        DglMain.Item(Col1Head, rowNCat).Value = hcNCat
        DglMain.Item(Col1Head, rowCategory).Value = hcCategory
        DglMain.Item(Col1Head, rowV_Type).Value = hcV_Type
        DglMain.Item(Col1Head, rowDescription).Value = hcDescription
        DglMain.Item(Col1Head, rowPrintingDescription).Value = hcPrintingDescription
        DglMain.Item(Col1Head, rowShort_Name).Value = hcShort_Name
        DglMain.Item(Col1Head, rowMnuAttachedInModule).Value = hcMnuAttachedInModule
        DglMain.Item(Col1Head, rowMnuName).Value = hcMnuName
        DglMain.Item(Col1Head, rowMnuText).Value = hcMnuText
        DglMain.Item(Col1Head, rowNature).Value = hcNature
        DglMain.Item(Col1Head, rowManualRefType).Value = hcManualRefType
        DglMain.Item(Col1Head, rowVoucherTypeTags).Value = hcVoucherTypeTags
        DglMain.Item(Col1Head, rowIsFutureDateTransactionAllowed).Value = hcIsFutureDateTransactionAllowed
        DglMain.Item(Col1Head, rowIsPostInLedger).Value = hcIsPostInLedger
        DglMain.Item(Col1Head, rowStructure).Value = hcStructure
        DglMain.Item(Col1Head, rowCustomUI).Value = hcCustomUI
        DglMain.Item(Col1Head, rowSiteList).Value = hcSiteList
        DglMain.Item(Col1Head, rowDivisionList).Value = hcDivisionList

        DglMain(Col1Value, rowShort_Name).Style.Alignment = DataGridViewContentAlignment.TopLeft
        DglMain(Col1Value, rowShort_Name).Style.WrapMode = DataGridViewTriState.True

        For I = 0 To DglMain.Rows.Count - 1
            DglMain(Col1HeadOriginal, I).Value = DglMain(Col1Head, I).Value
        Next

        ApplyUISetting()
    End Sub
    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DglMain.CellEnter
        Try
            If DglMain.CurrentCell Is Nothing Then Exit Sub
            If Topctrl1.Mode = "BROWSE" Then
                DglMain.CurrentCell.ReadOnly = True
            End If

            If DglMain.CurrentCell.ColumnIndex <> DglMain.Columns(Col1Value).Index Then Exit Sub

            DglMain.AgHelpDataSet(DglMain.CurrentCell.ColumnIndex) = Nothing
            CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0
            CType(DglMain.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = False

            Select Case DglMain.CurrentCell.RowIndex
                Case rowVoucherTypeTags, rowSiteList, rowDivisionList, rowIsFutureDateTransactionAllowed, rowIsPostInLedger
                    DglMain.Item(Col1Value, DglMain.CurrentCell.RowIndex).ReadOnly = True
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DglMain.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = DglMain.CurrentCell.RowIndex
            bColumnIndex = DglMain.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> DglMain.Columns(Col1Value).Index Then Exit Sub

            Select Case DglMain.CurrentCell.RowIndex
                Case rowV_Type
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT V_Type AS Code, V_Type AS Name FROM Voucher_Type ORDER BY V_Type "
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If
                    CType(DglMain.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True

                Case rowDescription
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Description  AS Code, Description AS Name FROM Voucher_Type ORDER BY Description "
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If
                    CType(DglMain.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True

                Case rowNCat
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "Select Distinct NCat, NCat As Name " &
                                " From Voucher_Type " &
                                " Order By NCat "
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If

                Case rowCategory
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "Select Distinct Category, Category As Name " &
                                " From Voucher_Type " &
                                " Order By Category "
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If

                Case rowMnuAttachedInModule
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT DISTINCT MnuModule AS Code, MnuModule AS Name FROM User_Permission WHERE UserName = 'SA' ORDER BY MnuModule "
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If

                Case rowMnuName
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT DISTINCT MnuName AS Code, MnuName AS Name FROM User_Permission WHERE UserName = 'SA' ORDER BY MnuName "
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If

                Case rowMnuText
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT DISTINCT MnuText AS Code, MnuText AS Name FROM User_Permission WHERE UserName = 'SA' ORDER BY MnuText "
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If

                Case rowNature
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT DISTINCT Nature  AS Code, Nature AS Name FROM Voucher_Type WHERE Nature IS NOT NULL ORDER BY Nature  "
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If

                Case rowManualRefType
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT DISTINCT Nature  AS Code, Nature AS Name FROM Voucher_Type WHERE Nature IS NOT NULL ORDER BY Nature  "
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If

                Case rowStructure
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Description FROM Structure ORDER BY Description "
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If

                Case rowCustomUI
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "Select '" & mCustomUI_Retail & "' As Code, '" & mCustomUI_Retail & "' As Description 
                                UNION ALL 
                                Select '" & mCustomUI_Order & "' As Code, '" & mCustomUI_Order & "' As Description 
                                UNION ALL 
                                Select '" & mCustomUI_Quotation & "' As Code, '" & mCustomUI_Quotation & "' As Description 
                                UNION ALL 
                                Select '" & mCustomUI_Estimate & "' As Code, '" & mCustomUI_Estimate & "' As Description "
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
    Private Sub Dgl1_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles DglMain.EditingControl_Validating
        Dim mRow As Integer
        Dim mColumn As Integer
        mRow = DglMain.CurrentCell.RowIndex
        mColumn = DglMain.CurrentCell.ColumnIndex
        If mColumn = DglMain.Columns(Col1Value).Index Then
            If DglMain.Item(Col1Mandatory, mRow).Value <> "" Then
                If DglMain(Col1Value, mRow).Value = "" Then
                    MsgBox(DglMain(Col1Head, mRow).Value & " can not be blank.")
                    e.Cancel = True
                    Exit Sub
                End If
            End If

            Select Case mRow
            End Select
        End If
    End Sub
    Private Sub Frm_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dim i As Integer
        For i = 0 To DglMain.Rows.Count - 1
            DglMain(Col1Value, i).Value = ""
            DglMain(Col1Value, i).Tag = ""
        Next
    End Sub
    Private Sub Frm_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Dim i As Integer

        For i = 0 To DglMain.Rows.Count - 1
            DglMain(Col1Head, i).Tag = Nothing
        Next
    End Sub
    Private Sub Frm_BaseEvent_ApproveDeletion_InTrans(SearchCode As String, Conn As Object, Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
    End Sub
    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles DglMain.KeyDown
        Dim mRow As Integer
        If DglMain.CurrentCell Is Nothing Then Exit Sub
        If ClsMain.IsSpecialKeyPressed(e) Then Exit Sub
        mRow = DglMain.CurrentCell.RowIndex

        If Topctrl1.Mode.ToUpper <> "BROWSE" Then
            If DglMain.CurrentCell.ColumnIndex = DglMain.Columns(Col1Value).Index Then
                If e.KeyCode = Keys.Delete Then
                    DglMain(Col1Value, DglMain.CurrentCell.RowIndex).Value = ""
                    DglMain(Col1Value, DglMain.CurrentCell.RowIndex).Tag = ""
                End If

                Select Case DglMain.CurrentCell.RowIndex
                    Case rowVoucherTypeTags
                        FHPGD_VoucherTypeTags(DglMain(Col1Value, DglMain.CurrentCell.RowIndex).Tag, DglMain(Col1Value, DglMain.CurrentCell.RowIndex).Value)
                    Case rowSiteList
                        FHPGD_Sites(DglMain(Col1Value, DglMain.CurrentCell.RowIndex).Tag, DglMain(Col1Value, DglMain.CurrentCell.RowIndex).Value)
                    Case rowDivisionList
                        FHPGD_Divisions(DglMain(Col1Value, DglMain.CurrentCell.RowIndex).Tag, DglMain(Col1Value, DglMain.CurrentCell.RowIndex).Value)
                    Case rowIsFutureDateTransactionAllowed, rowIsPostInLedger
                        If e.KeyCode <> Keys.Enter Then
                            If AgL.StrCmp(ChrW(e.KeyCode), "Y") Then
                                DglMain.Item(Col1Value, mRow).Value = "Yes"
                            ElseIf AgL.StrCmp(ChrW(e.KeyCode), "N") Then
                                DglMain.Item(Col1Value, mRow).Value = "No"
                            End If
                        End If
                End Select
            End If
        End If
    End Sub
    Private Sub FHPGD_VoucherTypeTags(ByRef bTag As String, ByRef bValue As String)
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtTemp As DataTable

        mQry = "SELECT 'o' As Tick, '" & ClsMain.VoucherTypeTags.ExcludeInSalesTaxReturns & "' AS Code, '" & ClsMain.VoucherTypeTags.ExcludeInSalesTaxReturns & "' AS Name "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 500, 520, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Name", 400, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            bTag = "+" + FRH_Multiple.FFetchData(1, "", "", "+", True)
            bValue = "+" + FRH_Multiple.FFetchData(2, "", "", "+", True)
        End If

        If bTag = "+" Then bTag = ""
        If bValue = "+" Then bValue = ""

        FRH_Multiple = Nothing
    End Sub

    Private Sub FHPGD_Sites(ByRef bTag As String, ByRef bValue As String)
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtTemp As DataTable

        mQry = "SELECT 'o' As Tick, Code, Name From SiteMast "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 500, 520, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Name", 400, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            bTag = "+" + FRH_Multiple.FFetchData(1, "", "", "+", True)
            bValue = "+" + FRH_Multiple.FFetchData(2, "", "", "+", True)
        End If

        If bTag = "+" Then bTag = ""
        If bValue = "+" Then bValue = ""

        FRH_Multiple = Nothing
    End Sub
    Private Sub FHPGD_Divisions(ByRef bTag As String, ByRef bValue As String)
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtTemp As DataTable

        mQry = "SELECT 'o' As Tick, Div_Code As Code, Div_Name As Name From Division "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 500, 520, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Name", 400, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            bTag = "+" + FRH_Multiple.FFetchData(1, "", "", "+", True)
            bValue = "+" + FRH_Multiple.FFetchData(2, "", "", "+", True)
        End If

        If bTag = "+" Then bTag = ""
        If bValue = "+" Then bValue = ""

        FRH_Multiple = Nothing
    End Sub
    Private Sub ApplyUISetting()
        ClsMain.GetUISetting(DglMain, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", ClsMain.GridTypeConstants.VerticalGrid)
    End Sub
End Class

