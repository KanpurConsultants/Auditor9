Imports System.Data.SQLite
Public Class FrmUserVoucherTypePermission
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable
    Dim mQry As String = "", mSearchCode As String = "", mModuleName As String = ""
    Dim DtVType As DataTable

    Public WithEvents DGL1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1Code As String = "Code"
    Protected Const Col1VType As String = "Voucher Type"
    Protected Const Col1Description As String = "Description"
    Protected Const Col1Permission As String = "P"
    Protected Const Col1Site As String = ".."
    Protected Const Col1SiteList As String = "Site List"
    Protected Const Col1Division As String = "..."
    Protected Const Col1DivisionList As String = "Division List"

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal AglibVar As AgLibrary.ClsMain)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
        AgL = AglibVar
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub Form_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        DTMaster = Nothing
    End Sub

    Private Sub IniGrid()
        DGL1.Height = Pnl1.Height
        DGL1.Width = Pnl1.Width
        DGL1.Top = Pnl1.Top
        DGL1.Left = Pnl1.Left
        Pnl1.Visible = False
        Controls.Add(DGL1)
        DGL1.Visible = True
        DGL1.BringToFront()
        With AgCL
            .AddAgTextColumn(DGL1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(DGL1, Col1Code, 300, 5, Col1Code, False, False, False, True)
            .AddAgTextColumn(DGL1, Col1VType, 90, 10, Col1VType, True, True, False, True)
            .AddAgTextColumn(DGL1, Col1Description, 250, 50, Col1Description, True, True, False, True)
            .AddAgCheckColumn(DGL1, Col1Permission, 35, Col1Permission, True)
            .AddAgButtonColumn(DGL1, Col1Site, 25, Col1Site, True, False)
            .AddAgTextColumn(DGL1, Col1SiteList, 120, 500, Col1SiteList, True, True, False, True)
            .AddAgButtonColumn(DGL1, Col1Division, 25, Col1Division, True, False)
            .AddAgTextColumn(DGL1, Col1DivisionList, 120, 500, Col1DivisionList, True, True, False, True)
        End With
        DGL1.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        DGL1.TabIndex = Pnl1.TabIndex
        DGL1.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Arial"), 9)
        DGL1.DefaultCellStyle.Font = New Font(New FontFamily("Arial"), 8)
    End Sub

    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Or e.KeyCode = Keys.F3 Or e.KeyCode = Keys.F4 Or e.KeyCode = (Keys.F And e.Control) Or e.KeyCode = (Keys.P And e.Control) _
        Or e.KeyCode = (Keys.S And e.Control) Or e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F5 Or e.KeyCode = Keys.F10 _
        Or e.KeyCode = Keys.Home Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Or e.KeyCode = Keys.End Then
            Topctrl1.TopKey_Down(e)
        End If

        If Me.ActiveControl IsNot Nothing Then
            If Me.ActiveControl.Name <> Topctrl1.Name And _
                Not (TypeOf (Me.ActiveControl) Is AgControls.AgDataGrid) Then
                If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
            End If
        End If
    End Sub

    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then Exit Sub
        If Me.ActiveControl Is Nothing Then Exit Sub
        AgL.CheckQuote(e)
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.WinSetting(Me, 600, 880, 0, 0)
            AgL.GridDesign(DGL1)
            IniGrid()
            FIniMaster()
            Ini_List()
            DispText()
            MoveRec()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FIniMaster(Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 1)
        Dim CondStr As String = " Where 1 = 1 "
        If Not (AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or _
            AgL.StrCmp(AgL.PubUserName, "SA") Or AgL.PubIsUserAdmin) Then
            CondStr += " And User_Name = '" & AgL.PubUserName & "' "
        End If

        mQry = "Select User_Name As SearchCode " & _
            " From UserMast " & CondStr
        Topctrl1.FIniForm(DTMaster, AgL.GcnMain, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Sub Ini_List()
        Dim CondStr As String = ""
        mQry = "Select U.User_Name As Code, U.User_Name as Name, U.Description, " &
                " Case IfNull(U.Admin,'N') When 'Y' Then 'Yes' Else 'No' End As Administrator " &
                " From UserMast U " &
                " Where 1=1 " &
                " Order By U.User_Name "
        TxtUserName.AgHelpDataSet() = AgL.FillData(mQry, AgL.GcnMain)

        If Not (AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or
            AgL.StrCmp(AgL.PubUserName, "SA") Or AgL.PubIsUserAdmin) Then

            CondStr = " And User_Name = '" & AgL.PubUserName & "' "
        Else
            CondStr = ""
        End If
        'mQry = "Select User_Name As Code, User_Name As Name From UserMast " & _
        '        "  Where 1 = 1 " & CondStr & " Order By User_Name "
        'AgCL.IniAgHelpList(AgL.GcnMain, CboUserName, mQry, "Name", "Code")
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        BlankText()
        DispText()
        TxtUserName.Focus()
    End Sub

    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New SQLiteCommand
        Dim MastPos As Long
        Dim mTrans As Boolean = False

        Try
            MastPos = BMBMaster.Position

            If DTMaster.Rows.Count > 0 Then
                If Not AgL.StrCmp(AgL.PubUserName, "SA") Then Err.Raise(1, , "Permission Denied!..." & vbCrLf & "Login User Is Not System Administrator!")
                If AgL.StrCmp(mSearchCode, "SA") Then Err.Raise(1, , "Permission Denied!..." & vbCrLf & "User Is System Administrator!")

                If MsgBox("Are You Sure To Delete This Record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, AgLibrary.ClsMain.PubMsgTitleInfo) = vbYes Then
                    AgL.ECmd = AgL.GcnMain.CreateCommand
                    AgL.ETrans = AgL.GcnMain.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = True

                    mQry = "Delete From User_VoucherType_Permission Where UserName='" & mSearchCode & "'  "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain, AgL.ECmd)

                    AgL.ETrans.Commit()
                    mTrans = False

                    Call AgL.LogTableEntry(mSearchCode, Me.Text, "D", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn)

                    FIniMaster(1)
                    Topctrl1_tbRef()
                    MoveRec()
                End If
            End If
        Catch Ex As Exception
            If mTrans = True Then AgL.ETrans.Rollback()
            MsgBox(Ex.Message, MsgBoxStyle.Information, AgLibrary.ClsMain.PubMsgTitleInfo)
        End Try
    End Sub

    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        FIniMaster(0, 0)
        Topctrl1.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        DispText()
        DGL1.Focus()
    End Sub

    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox("No Records To Search.", vbInformation, AgLibrary.ClsMain.PubMsgTitleInfo) : Exit Sub
        Try
            Dim CondStr As String = " Where 1 = 1 "

            If Not (AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or
                AgL.StrCmp(AgL.PubUserName, "SA") Or AgL.PubIsUserAdmin) Then

                CondStr += " And U.User_Name = '" & AgL.PubUserName & "' "
            End If

            AgL.PubFindQry = "Select U.User_Name As SearchCode, U.User_Name as [User Name], U.Description, " &
                                " Case IfNull(U.Admin,'N') When 'Y' Then 'Yes' Else 'No' End As Administrator " &
                                " From UserMast U " & CondStr

            AgL.PubFindQryOrdBy = "SearchCode"

            '*************** common code start *****************
            AgL.PubObjFrmFind = New AgLibrary.frmFind(AgL)
            AgL.PubObjFrmFind.ShowDialog()
            AgL.PubObjFrmFind = Nothing
            If AgL.PubSearchRow <> "" Then
                AgL.PubDRFound = DTMaster.Rows.Find(AgL.PubSearchRow)
                BMBMaster.Position = DTMaster.Rows.IndexOf(AgL.PubDRFound)
                MoveRec()
            End If
            '*************** common code end  *****************
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Private Sub Topctrl1_tbRef() Handles Topctrl1.tbRef
        Ini_List()
    End Sub

    Private Sub Topctrl1_tbSave() Handles Topctrl1.tbSave
        Dim MastPos As Long
        Dim I As Integer
        Dim mTrans As Boolean = False

        Try
            MastPos = BMBMaster.Position
            DtVType.DefaultView.RowFilter = Nothing

            If Not Data_Validation() Then Exit Sub
            AgL.ECmd = AgL.GcnMain.CreateCommand
            AgL.ETrans = AgL.GcnMain.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True

            mQry = "Delete From User_VoucherType_Permission " &
                    " Where UserName='" & mSearchCode & "'  "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain, AgL.ECmd)

            With DGL1
                For I = 0 To .Rows.Count - 1
                    If AgL.XNull(.Item(Col1Code, I).Value).ToString.Trim <> "" And AgL.StrCmp(.Item(Col1Permission, I).Value, AgLibrary.ClsConstant.StrCheckedValue) Then
                        mQry = " INSERT INTO User_VoucherType_Permission (UserName, V_Type,	SNo, Active, SiteList, DivisionList ) " &
                                " Values( '" & mSearchCode & "', " & AgL.Chk_Text(.Item(Col1Code, I).Value) & "," & Val(.Item(ColSNo, I).Value) & ", 'Y', " &
                                " " & AgL.Chk_Text(.Item(Col1SiteList, I).Value) & " , " & AgL.Chk_Text(.Item(Col1DivisionList, I).Value) & " ) "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain, AgL.ECmd)
                    End If
                Next I
            End With

            AgL.ETrans.Commit()
            mTrans = False

            Call AgL.LogTableEntry(mSearchCode, Me.Text, "E", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn)

            FIniMaster(0, 1)
            Topctrl1_tbRef()
            Topctrl1.SetDisp(True)
            MoveRec()
        Catch ex As Exception
            If mTrans = True Then AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        Finally
            'Finally Code
        End Try
    End Sub

    Public Sub MoveRec()
        Dim DsTemp As DataSet = Nothing
        Dim MastPos As Long
        Try
            FClear()
            BlankText()
            If DTMaster.Rows.Count > 0 Then
                MastPos = BMBMaster.Position
                mSearchCode = DTMaster.Rows(MastPos)("SearchCode")
                TxtUserName.AgSelectedValue = mSearchCode
                CboUserName.Enabled = IIf(mSearchCode.Trim.ToUpper = "SA", False, True)
                CboUserName.SelectedValue = mSearchCode
                Fill_Grid(TxtUserName.Text)
            Else
                BlankText()
            End If
            Topctrl1.FSetDispRec(BMBMaster)

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            DsTemp = Nothing
            Topctrl1.tAdd = False : Topctrl1.tEdit = False : Topctrl1.tDel = False : Topctrl1.tPrn = False
        End Try
    End Sub
    Private Sub BlankText()
        If Topctrl1.Mode <> "Add" Then Topctrl1.BlankTextBoxes(Me)
        mSearchCode = ""
        DGL1.DataSource = Nothing
    End Sub
    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
    End Sub

    Private Sub DGL1_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGL1.CellContentClick
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim FrmObj As Utility.FrmSiteList
        Dim FrmObjDivision As FrmDivisionList
        Dim mSiteList$ = "", mDivisionList$ = ""
        Try
            mRowIndex = DGL1.CurrentCell.RowIndex
            mColumnIndex = DGL1.CurrentCell.ColumnIndex
            Select Case sender.Columns(sender.CurrentCell.ColumnIndex).Name
                Case Col1Site
                    If DGL1.Item(Col1SiteList, mRowIndex).Value Is Nothing Then DGL1.Item(Col1SiteList, mRowIndex).Value = ""
                    mSiteList = DGL1.Item(Col1SiteList, mRowIndex).Value
                    FrmObj = New Utility.FrmSiteList : FrmObj.SiteListStr = mSiteList : FrmObj.ShowDialog()
                    mSiteList = FrmObj.SiteListStr : FrmObj = Nothing
                    DGL1.Item(Col1SiteList, mRowIndex).Value = mSiteList

                Case Col1Division
                    If DGL1.Item(Col1DivisionList, mRowIndex).Value Is Nothing Then DGL1.Item(Col1DivisionList, mRowIndex).Value = ""
                    mDivisionList = DGL1.Item(Col1DivisionList, mRowIndex).Value
                    FrmObjDivision = New FrmDivisionList : FrmObjDivision.DivisionListStr = mDivisionList : FrmObjDivision.ShowDialog()
                    mDivisionList = FrmObjDivision.DivisionListStr : FrmObjDivision = Nothing
                    DGL1.Item(Col1DivisionList, mRowIndex).Value = mDivisionList

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DGL1.CellMouseUp
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Try
            If DGL1.Rows.Count = 0 Then Exit Sub

            mRowIndex = sender.CurrentCell.RowIndex
            mColumnIndex = sender.CurrentCell.ColumnIndex

            If sender.Item(mColumnIndex, mRowIndex).Value Is Nothing Then sender.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case sender.Columns(sender.CurrentCell.ColumnIndex).Name
                Case Col1Permission
                    Try
                        Call AgL.ProcSetCheckColumnCellValue(sender, sender.Columns(Col1Permission).Index)
                    Catch ex As Exception
                    End Try
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGL1.KeyDown
        If DGL1.Rows.Count = 0 Then Exit Sub
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
        Try
            Select Case sender.Columns(sender.CurrentCell.ColumnIndex).Name
                Case Col1Permission
                    If e.KeyCode = Keys.Space Then
                        Try
                            AgL.ProcSetCheckColumnCellValue(sender, sender.Columns(Col1Permission).Index)
                        Catch ex As Exception
                        End Try
                    End If
            End Select
        Catch ex As Exception
        End Try
    End Sub

    'Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGL1.EditingControl_KeyDown
    '    Dim FrmObj As Object = Nothing
    '    Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
    '    Dim DrTemp As DataRow() = Nothing
    '    Try
    '        bRowIndex = DGL1.CurrentCell.RowIndex
    '        bColumnIndex = DGL1.CurrentCell.ColumnIndex

    '        If e.KeyCode = Keys.Enter Then Exit Sub

    '        Select Case DGL1.Columns(DGL1.CurrentCell.ColumnIndex).Name
    '            Case Col1SiteList
    '                If DGL1.AgHelpDataSet(DGL1.CurrentCell.ColumnIndex) Is Nothing Then
    '                    'FHPGD_TxtFilter("SELECT 'o' AS Tick, Code, Name  FROM SiteMast  Order By Name", Col1SiteList, bRowIndex)
    '                    FHPGD_TxtFilter("SELECT 'o' AS Tick, User_Name, USER_NAME  FROM UserMast", Col1SiteList, bRowIndex)
    '                End If

    '                'Case Col1Item
    '                '    If e.KeyCode <> Keys.Enter Then
    '                '        If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
    '                '            FCreateHelpItem(Col1Item)
    '                '        End If
    '                '    End If


    '        End Select
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    'Private Sub FHPGD_TxtFilter(ByVal mQry As String, ByVal ColoumnName As String, ByVal RowIndex As Integer)
    '    Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
    '    FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 400, 370, , , False)
    '    FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
    '    FRH_Multiple.FFormatColumn(1, , 0, , False)
    '    FRH_Multiple.FFormatColumn(2, "Name", 250, DataGridViewContentAlignment.MiddleLeft)

    '    FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
    '    FRH_Multiple.ShowDialog()

    '    If FRH_Multiple.BytBtnValue = 0 Then
    '        DGL1.Item(ColoumnName, RowIndex).Tag = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
    '        DGL1.Item(ColoumnName, RowIndex).Value = FRH_Multiple.FFetchData(2, "", "", ",", True)
    '    End If
    '    FRH_Multiple = Nothing
    'End Sub

    Private Sub FClear()
        DTStruct.Clear()
    End Sub
    Private Sub FAddRowStructure()
        Dim DRStruct As DataRow
        Try
            DRStruct = DTStruct.NewRow
            DTStruct.Rows.Add(DRStruct)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function Data_Validation() As Boolean
        Try
            If AgL.RequiredField(TxtUserName) Then Exit Function
            If Not (AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or
                AgL.StrCmp(AgL.PubUserName, "SA") Or AgL.PubIsUserAdmin) Then
                Err.Raise(1, , "Permission Denied!..." & vbCrLf & "Login User Is Not System Administrator!")
            End If

            If AgL.StrCmp(mSearchCode, "SA") Then Err.Raise(1, , "Permission Denied!..." & vbCrLf & "User Is System Administrator!")
            Data_Validation = True
        Catch ex As Exception
            Data_Validation = False
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub Fill_Grid(ByVal UserName As String)
        Dim I As Integer
        Try
            mQry = " SELECT V.V_Type AS Code, Vt.Short_Name , Vt.Description, " &
                    " convert(BIT, CASE IfNull(U.Active,'N') WHEN 'Y' THEN 1 ELSE 0 END ) AS Permission, U.DivisionList, U.SiteList " &
                    " FROM " &
                    " ( " &
                    " SELECT V_Type, Active, SNo  " &
                    " FROM User_VoucherType_Permission " &
                    " WHERE UserName ='SA' And IfNull(Active,'Y') = 'Y' " &
                    " ) AS V  " &
                    " LEFT JOIN User_VoucherType_Permission U ON U.V_Type = V.V_Type AND U.UserName ='" & UserName & "' " &
                    " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = V.V_Type  " &
                    " WHERE IfNull(U.Active,'Y') = 'Y'  " &
                    " ORDER BY Vt.Short_Name "
            DtVType = AgL.FillData(mQry, AgL.GcnMain).tables(0)

            With DtVType
                DGL1.RowCount = 1
                DGL1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To DtVType.Rows.Count - 1
                        DGL1.Rows.Add()
                        DGL1.Item(ColSNo, I).Value = DGL1.Rows.Count - 1
                        DGL1.Item(Col1Code, I).Value = AgL.XNull(.Rows(I)("Code"))
                        DGL1.Item(Col1VType, I).Value = AgL.XNull(.Rows(I)("Short_Name"))
                        DGL1.Item(Col1Description, I).Value = AgL.XNull(.Rows(I)("Description"))
                        DGL1.Item(Col1DivisionList, I).Value = AgL.XNull(.Rows(I)("DivisionList"))
                        DGL1.Item(Col1SiteList, I).Value = AgL.XNull(.Rows(I)("SiteList"))
                        DGL1.Item(Col1Permission, I).Value = IIf(AgL.VNull(.Rows(I)("Permission")) = True, AgLibrary.ClsConstant.StrCheckedValue, AgLibrary.ClsConstant.StrUnCheckedValue)

                    Next
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CmdCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdCopy.Click
        Dim mTrans As Boolean = False
        Try
            DGL1.DataSource = Nothing
            If DTMaster.Rows.Count > 0 Then
                If AgL.RequiredField(CboUserName) Then Exit Sub
                If Not (AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or _
                    AgL.StrCmp(AgL.PubUserName, "SA") Or AgL.PubIsUserAdmin) Then

                    Err.Raise(1, , "Permission Denied!..." & vbCrLf & "Login User Is Not System Administrator!")
                End If

                If AgL.StrCmp(mSearchCode, "SA") Then Err.Raise(1, , "Permission Denied!..." & vbCrLf & "User Is System Administrator!")
                If AgL.StrCmp(mSearchCode, CboUserName.Text) Then Err.Raise(1, , "Copy From User is Same as Current User!...")

                If MsgBox("Are You Sure to copy User Voucher Type Permission From """ & CboUserName.Text & """?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

                AgL.ECmd = AgL.GcnMain.CreateCommand
                AgL.ETrans = AgL.GcnMain.BeginTransaction(IsolationLevel.ReadCommitted)
                AgL.ECmd.Transaction = AgL.ETrans
                mTrans = True

                mQry = "Delete From User_VoucherType_Permission Where UserName='" & mSearchCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain, AgL.ECmd)

                mQry = "Insert Into User_VoucherType_Permission (UserName, V_Type,	Active,SiteList, DivisionList) " & _
                        " (Select '" & mSearchCode & "', V_Type, Active, SiteList, DivisionList " & _
                        " From User_VoucherType_Permission Where UserName='" & CboUserName.Text & "')"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain, AgL.ECmd)
                AgL.ETrans.Commit()

                mTrans = False

                Call AgL.LogTableEntry(mSearchCode, Me.Text, "A", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn)

                FIniMaster(0, 1)
                Topctrl1_tbRef()
                Topctrl1.SetDisp(True)
                MoveRec()
            End If

        Catch ex As Exception
            If mTrans = True Then AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CmdRevoke_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdRevokeAll.Click, CmdPermissionAll.Click
        Try
            Select Case sender.name
                Case CmdRevokeAll.Name
                    Assign_Permission(False)
                Case CmdPermissionAll.Name
                    Assign_Permission(True)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Assign_Permission(ByVal mPermission As Boolean)
        Dim I As Integer
        Try
            If AgL.StrCmp(mSearchCode, "SA") Then Exit Sub
            With DGL1
                For I = 0 To .Rows.Count - 1
                    If AgL.XNull(.Item(Col1Code, I).Value).ToString.Trim <> "" Then
                        .Item(Col1Permission, I).Value = mPermission
                        DGL1.Item(Col1Permission, I).Value = IIf(mPermission = True, AgLibrary.ClsConstant.StrCheckedValue, AgLibrary.ClsConstant.StrUnCheckedValue)
                    End If
                Next
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdSave.Click
        Topctrl1_tbSave()
    End Sub
End Class
