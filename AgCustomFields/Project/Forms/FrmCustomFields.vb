Imports System.Data.SQLite
Public Class FrmCustomFields
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable
    Dim mQry As String = "", mSearchCode As String = ""

    Private Const Col_SNo As String = "S.No."
    Public WithEvents DGL1 As New AgControls.AgDataGrid
    Private Const Col1_Head As String = "Head"
    Private Const Col1_HeadManualCode As String = "Head Manual Code"
    Private Const Col1_Value_Type As String = "Value Type"
    Private Const Col1_FLength As String = "Length"
    Private Const Col1_value As String = "Value"
    Private Const Col1_Defaultvalue As String = "Default Value"
    Private Const Col1_Active As String = "Active"
    Private Const Col1_MandatoryYN As String = "Is Manadatory Y/N"
    Private Const Col1_UpdateField As String = "Update Field"
    Private Const Col1_UpdateFieldType As String = "Update Field Type"
    Private Const Col1_HeaderField As String = "Header Field"
    Private Const Col1_HeaderFieldDataType As String = "Header Field Data Type"
    Private Const Col1_HeaderFieldLength As String = "Header Field Length"

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub Form_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        DTMaster = Nothing
    End Sub

    Private Sub IniGrid()
        AgL.AddAgDataGrid(DGL1, Pnl1)
        With AgCL
            .AddAgNumberColumn(DGL1, Col_SNo, 40, 5, 0, False, Col_SNo, True, False, True)
            .AddAgTextColumn(DGL1, Col1_Head, 170, 0, Col1_Head, True, False, False)
            .AddAgTextColumn(DGL1, Col1_HeadManualCode, 100, 0, Col1_HeadManualCode, True, True, False)
            .AddAgListColumn(DGL1, "Text,Number,Date,List,Help", Col1_Value_Type, 100, "Text,Number,Date,List,Help", Col1_Value_Type, True, False, False)
            .AddAgTextColumn(DGL1, Col1_FLength, 80, 20, Col1_FLength, True, False, False)
            .AddAgTextColumn(DGL1, Col1_value, 100, 0, Col1_value, True, False, False)
            .AddAgTextColumn(DGL1, Col1_Defaultvalue, 100, 0, Col1_Defaultvalue, True, False, False)
            .AddAgListColumn(DGL1, "Yes,No", Col1_Active, 80, "1,0", Col1_Active, True, False, False)
            .AddAgListColumn(DGL1, "Yes,No", Col1_MandatoryYN, 80, "1,0", Col1_MandatoryYN, True, False, False)
            .AddAgTextColumn(DGL1, Col1_UpdateField, 170, 0, Col1_UpdateField, True, False, False)
            .AddAgListColumn(DGL1, "Text,Number,Date", Col1_UpdateFieldType, 100, "Text,Number,Date", Col1_UpdateFieldType, True, False, False)
            .AddAgTextColumn(DGL1, Col1_HeaderField, 170, 0, Col1_HeaderField, True, False, False)
            .AddAgTextColumn(DGL1, Col1_HeaderFieldDataType, 170, 0, Col1_HeaderFieldDataType, True, False, False)
            .AddAgNumberColumn(DGL1, Col1_HeaderFieldLength, 40, 5, 0, False, Col1_HeaderFieldLength, True, False, True)
        End With
        DGL1.EnableHeadersVisualStyles = False
        DGL1.ColumnHeadersHeight = 50
    End Sub

    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Or e.KeyCode = Keys.F3 Or e.KeyCode = Keys.F4 Or e.KeyCode = (Keys.F And e.Control) Or e.KeyCode = (Keys.P And e.Control) _
        Or e.KeyCode = (Keys.S And e.Control) Or e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F5 Or e.KeyCode = Keys.F10 _
        Or e.KeyCode = Keys.Home Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Or e.KeyCode = Keys.End Then
            Topctrl1.TopKey_Down(e)
        End If
        If Me.ActiveControl IsNot Nothing Then
            If Me.ActiveControl.Name <> Topctrl1.Name And
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
            AgL.WinSetting(Me, 660, 990, 0, 0)
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
        mQry = "Select Code As SearchCode " &
                " From CustomFields  "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Sub Ini_List()
        Try
            mQry = "Select Code As Code, Code As Description  " &
                    " From CustomFields  " &
                    " Order By Description"
            TxtCode.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

            mQry = "Select Code As Code, Description  " &
                    " From CustomFields  " &
                    " Order By Description "
            TxtDescription.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
            TxtCopyFrom.AgHelpDataSet(0, GroupBox1.Top, GroupBox1.Left) = TxtDescription.AgHelpDataSet.Copy

            mQry = "Select Code, Description, ManualCode From CustomFieldsHead Order By Description"
            DGL1.AgHelpDataSet(Col1_Head) = AgL.FillData(mQry, AgL.GCn)

            mQry = " Select " & AgLibrary.ClsMain.SQLDataType.BigInt & " As Code, 'BigInt' As Name  " &
                    " UNION ALL " &
                    " Select " & AgLibrary.ClsMain.SQLDataType.Bit & " As Code, 'Bit' As Name  " &
                    " UNION ALL " &
                    " Select " & AgLibrary.ClsMain.SQLDataType.Float & " As Code, 'Float' As Name  " &
                    " UNION ALL " &
                    " Select " & AgLibrary.ClsMain.SQLDataType.IDENTITY & " As Code, 'IDENTITY' As Name  " &
                    " UNION ALL " &
                    " Select " & AgLibrary.ClsMain.SQLDataType.image & " As Code, 'image' As Name  " &
                    " UNION ALL " &
                    " Select " & AgLibrary.ClsMain.SQLDataType.Int & " As Code, 'Int' As Name  " &
                    " UNION ALL " &
                    " Select " & AgLibrary.ClsMain.SQLDataType.nVarChar & " As Code, 'nVarChar' As Name  " &
                    " UNION ALL " &
                    " Select " & AgLibrary.ClsMain.SQLDataType.SmallDateTime & " As Code, 'SmallDateTime' As Name  " &
                    " UNION ALL " &
                    " Select " & AgLibrary.ClsMain.SQLDataType.SmallInt & " As Code, 'SmallInt' As Name  " &
                    " UNION ALL " &
                    " Select " & AgLibrary.ClsMain.SQLDataType.TinyInt & " As Code, 'TinyInt' As Name  " &
                    " UNION ALL " &
                    " Select " & AgLibrary.ClsMain.SQLDataType.uniqueidentifier & " As Code, 'uniqueidentifier' As Name  " &
                    " UNION ALL " &
                    " Select " & AgLibrary.ClsMain.SQLDataType.VarChar & " As Code, 'VarChar' As Name  " &
                    " UNION ALL " &
                    " Select " & AgLibrary.ClsMain.SQLDataType.VarCharMax & " As Code, 'VarCharMax' As Name  "
            DGL1.AgHelpDataSet(Col1_HeaderFieldDataType) = AgL.FillData(mQry, AgL.GCn)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        BlankText()
        DispText()
        TxtCode.Focus()
    End Sub

    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False

        Dim MastPos As Long
        Dim mTrans As Boolean = False
        Try
            MastPos = BMBMaster.Position

            If DTMaster.Rows.Count > 0 Then
                If MsgBox("Are You Sure To Delete This Record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, AgLibrary.ClsMain.PubMsgTitleInfo) = vbYes Then


                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = True

                    AgL.LedgerUnPost(AgL.GCn, AgL.ECmd, mSearchCode)

                    mQry = "Delete From CustomFieldsDetail Where Code = '" & mSearchCode & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mQry = "Delete From CustomFields Where Code = '" & mSearchCode & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    Call AgL.LogTableEntry(mSearchCode, Me.Text, "D", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)

                    AgL.ETrans.Commit()
                    mTrans = False

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
    End Sub

    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox("No Records To Search.", vbInformation, AgLibrary.ClsMain.PubMsgTitleInfo) : Exit Sub
        Try
            AgL.PubFindQry = "Select  Code As SearchCode,  Description As [Item Name],  " &
                                " ManualCode As [Manual Code]  " &
                                " From  CustomFields   "

            AgL.PubFindQryOrdBy = "[SearchCode]"

            '*************** common code start *****************
            AgL.PubObjFrmFind = New AgLibrary.FrmFind(AgL.PubFindQry, "Find", AgL)
            AgL.PubObjFrmFind.ShowDialog()
            AgL.PubObjFrmFind = Nothing
            If AgL.PubSearchRow <> " Then" Then
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
        Dim I As Integer, mSr As Integer = 0
        Dim mTrans As Boolean = False
        Try
            MastPos = BMBMaster.Position

            If Not Data_Validation() Then Exit Sub

            If Topctrl1.Mode = "Add" Then
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From CustomFields Where Code='" & TxtCode.Text & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Code Already Exist!") : TxtCode.Focus() : Exit Sub
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From CustomFields Where Description='" & TxtDescription.Text & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Description Already Exist!") : TxtDescription.Focus() : Exit Sub

                mSearchCode = TxtCode.Text
            Else
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From CustomFields Where Code='" & TxtCode.Text & "' And Code<>'" & mSearchCode & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Code Already Exist!") : TxtCode.Focus() : Exit Sub
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From CustomFields Where Description='" & TxtDescription.Text & "' And Code<>'" & mSearchCode & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Description Already Exist!") : TxtDescription.Focus() : Exit Sub
            End If

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True


            If Topctrl1.Mode = "Add" Then
                mQry = "INSERT INTO CustomFields " &
                        " (Code,Div_Code, Site_Code," &
                        " Description, TableName, PrimaryField, PreparedBy,U_EntDt,U_AE) " &
                        " VALUES ( " &
                        " " & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(AgL.PubDivCode) & ", " & AgL.Chk_Text(AgL.PubSiteCode) & ",   " &
                        " " & AgL.Chk_Text(TxtDescription.Text) & ", " &
                        " " & AgL.Chk_Text(TxtTableName.Text) & ", " & AgL.Chk_Text(TxtTableName.Text) & ", " &
                        " " & AgL.Chk_Text(AgL.PubUserName) & "," & AgL.ConvertDate(AgL.PubLoginDate) & ",'" & AgL.MidStr(Topctrl1.Mode, 0, 1) & "')"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                mQry = "UPDATE CustomFields " &
                       " SET Description =" & AgL.Chk_Text(TxtDescription.Text) & ", " &
                       " TableName = " & AgL.Chk_Text(TxtTableName.Text) & ", " &
                       " PrimaryField = " & AgL.Chk_Text(TxtPrimaryField.Text) & ", " &
                       " U_AE = 'E', " &
                       " Edit_Date = " & AgL.ConvertDate(AgL.PubLoginDate) & ",	" &
                       " ModifiedBy = " & AgL.Chk_Text(AgL.PubUserName) & "  " &
                       " Where Code = '" & mSearchCode & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            mQry = "Delete From CustomFieldsDetail Where Code = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            With DGL1
                mSr = 0
                For I = 0 To .Rows.Count - 1
                    If .Item(Col1_Head, I).Value <> "" Then
                        mSr = mSr + 1
                        mQry = "INSERT INTO CustomFieldsDetail " &
                                " (Code, Sr, Head, FLength, Value_Type, Value, Default_Value, Active, " &
                                " IsMandatory, UpdateField, UpdateFieldType, HeaderField, HeaderFieldDataType, HeaderFieldLength)" &
                                " VALUES (" &
                                " " & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(DGL1.Item(Col_SNo, I).Value) & ", " &
                                " " & AgL.Chk_Text(DGL1.AgSelectedValue(Col1_Head, I)) & " , " &
                                " " & AgL.Chk_Text(DGL1.Item(Col1_FLength, I).Value) & " , " &
                                " " & AgL.Chk_Text(DGL1.AgSelectedValue(Col1_Value_Type, I)) & " , " &
                                " " & AgL.Chk_Text(DGL1.Item(Col1_value, I).Value) & " , " &
                                " " & AgL.Chk_Text(DGL1.Item(Col1_Defaultvalue, I).Value) & " , " &
                                " " & AgL.Chk_Text(DGL1.AgSelectedValue(Col1_Active, I)) & " , " &
                                " " & AgL.Chk_Text(DGL1.AgSelectedValue(Col1_MandatoryYN, I)) & ", " &
                                " " & AgL.Chk_Text(DGL1.Item(Col1_UpdateField, I).Value) & ", " &
                                " " & AgL.Chk_Text(DGL1.Item(Col1_UpdateFieldType, I).Value) & ", " &
                                " " & AgL.Chk_Text(DGL1.Item(Col1_HeaderField, I).Value) & ", " &
                                " " & AgL.Chk_Text(DGL1.AgSelectedValue(Col1_HeaderFieldDataType, I)) & ", " &
                                " " & Val(DGL1.Item(Col1_HeaderFieldLength, I).Value) & " " &
                                " ) "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                Next I
            End With

            Call AgL.LogTableEntry(mSearchCode, Me.Text, AgL.MidStr(Topctrl1.Mode, 0, 1), AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)

            AgL.ETrans.Commit()
            mTrans = False
            FIniMaster(0, 1)
            Topctrl1_tbRef()
            If Topctrl1.Mode = "Add" Then
                Topctrl1.LblDocId.Text = mSearchCode
                Topctrl1.FButtonClick(0)
                Exit Sub
            Else
                Topctrl1.SetDisp(True)
                MoveRec()
            End If
        Catch ex As Exception
            If mTrans = True Then AgL.ETrans.Rollback()
            MsgBox(ex.Message)
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
                mQry = "Select SI.* " &
                        " From CustomFields Si " &
                        " Where SI.Code='" & mSearchCode & "'"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    If .Rows.Count > 0 Then
                        TxtCode.Text = AgL.XNull(.Rows(0)("Code"))
                        TxtDescription.Text = AgL.XNull(.Rows(0)("Description"))
                        TxtTableName.Text = AgL.XNull(.Rows(0)("TableName"))
                        TxtPrimaryField.Text = AgL.XNull(.Rows(0)("PrimaryField"))
                        TxtPrepared.Text = AgL.XNull(.Rows(0)("PreparedBy"))
                        TxtModified.Text = AgL.XNull(.Rows(0)("ModifiedBy"))
                        GroupBox4.Visible = IIf(TxtModified.Text.Trim <> "", True, False)
                    End If
                End With
                MoveRecLine(mSearchCode)
            Else
                BlankText()
            End If
            Topctrl1.FSetDispRec(BMBMaster)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            DsTemp = Nothing
        End Try
    End Sub

    Private Sub MoveRecLine(ByVal mCode As String)
        Dim DsTemp As DataSet
        Dim I As Integer

        mQry = "Select SN.*, C.ManualCode " &
                " From CustomFieldsDetail SN " &
                " Left Join CustomFieldsHead C On SN.Head = C.Code " &
                " Where Sn.Code='" & mCode & "' Order By Sr"
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            DGL1.RowCount = 1
            DGL1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    DGL1.Rows.Add()
                    DGL1.Item(Col_SNo, I).Value = AgL.VNull(.Rows(I)("Sr"))
                    DGL1.AgSelectedValue(Col1_Head, I) = AgL.XNull(.Rows(I)("Head"))
                    DGL1.Item(Col1_HeadManualCode, I).Value = AgL.XNull(.Rows(I)("ManualCode"))
                    DGL1.AgSelectedValue(Col1_Value_Type, I) = AgL.XNull(.Rows(I)("Value_Type"))
                    DGL1.Item(Col1_value, I).Value = AgL.XNull(.Rows(I)("Value"))
                    DGL1.Item(Col1_Defaultvalue, I).Value = AgL.XNull(.Rows(I)("Default_Value"))
                    DGL1.Item(Col1_FLength, I).Value = AgL.XNull(.Rows(I)("FLength"))
                    DGL1.AgSelectedValue(Col1_Active, I) = Math.Abs(AgL.VNull(.Rows(I)("Active")))
                    DGL1.AgSelectedValue(Col1_MandatoryYN, I) = Math.Abs(AgL.VNull(.Rows(I)("IsMandatory")))
                    DGL1.Item(Col1_UpdateField, I).Value = AgL.XNull(.Rows(I)("UpdateField"))
                    DGL1.Item(Col1_UpdateFieldType, I).Value = AgL.XNull(.Rows(I)("UpdateFieldType"))
                    DGL1.Item(Col1_HeaderField, I).Value = AgL.XNull(.Rows(I)("HeaderField"))
                    DGL1.AgSelectedValue(Col1_HeaderFieldDataType, I) = AgL.XNull(.Rows(I)("HeaderFieldDataType"))
                    DGL1.Item(Col1_HeaderFieldLength, I).Value = AgL.VNull(.Rows(I)("HeaderFieldLength"))
                Next I
            End If
        End With
    End Sub

    Private Sub BlankText()
        If Topctrl1.Mode <> "Add" Then Topctrl1.BlankTextBoxes()
        mSearchCode = ""
        DGL1.RowCount = 1 : DGL1.Rows.Clear()
    End Sub

    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
        If Topctrl1.Mode <> "Add" Then
            TxtCode.Enabled = False
        End If
    End Sub

    Private Sub DGL1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGL1.CellEnter
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer

        Try
            mRowIndex = DGL1.CurrentCell.RowIndex
            mColumnIndex = DGL1.CurrentCell.ColumnIndex

            If DGL1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then DGL1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case DGL1.CurrentCell.ColumnIndex
                Case Col1_Head

            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DGL1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DGL1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing

        Try
            mRowIndex = DGL1.CurrentCell.RowIndex
            mColumnIndex = DGL1.CurrentCell.ColumnIndex
            If DGL1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then DGL1.Item(mColumnIndex, mRowIndex).Value = ""

            Select Case DGL1.Columns(DGL1.CurrentCell.ColumnIndex).Name
                Case Col1_Head
                    Validating_Head(DGL1.AgSelectedValue(Col1_Head, mRowIndex), mRowIndex)

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Validating_Head(ByVal Code As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            If DGL1.Item(Col1_Head, mRow).Value.ToString.Trim = "" Or DGL1.AgSelectedValue(Col1_Head, mRow).ToString.Trim = "" Then
                DGL1.Item(Col1_HeadManualCode, mRow).Value = ""
            Else
                If DGL1.AgHelpDataSet(Col1_Head) IsNot Nothing Then
                    DrTemp = DGL1.AgHelpDataSet(Col1_Head).Tables(0).Select("Code = '" & Code & "'")
                    DGL1.Item(Col1_HeadManualCode, mRow).Value = AgL.XNull(DrTemp(0)("ManualCode"))
                    DGL1.AgSelectedValue(Col1_Active, mRow) = 1
                    DGL1.AgSelectedValue(Col1_MandatoryYN, mRow) = 0
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Private Sub DGL1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles DGL1.EditingControlShowing
        If Topctrl1.Mode = "Browse" Then Exit Sub
        If TypeOf e.Control Is ComboBox Then
            e.Control.Text = ""
        End If
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGL1.KeyDown
        If Topctrl1.Mode <> "Browse" Then
            If e.Control And e.KeyCode = Keys.D Then
                sender.CurrentRow.Selected = True
            End If
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles DGL1.RowsAdded
        'sender(Col_SNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub DGL1_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles DGL1.RowsRemoved
        Try
            DTStruct.Rows.Remove(DTStruct.Rows.Item(e.RowIndex))
        Catch ex As Exception
        End Try
        'AgL.FSetSNo(sender, Col_SNo)
    End Sub

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
        Dim I As Integer = 0
        Try
            'If AgCL.AgCheckMandatory(Me) = False Then Exit Function
            If AgCL.AgIsDuplicate(DGL1, "" & DGL1.Columns(Col1_Head).Index & "") Then Exit Function

            With DGL1
                For I = 0 To .Rows.Count - 1
                    If .Item(Col1_Head, I).Value Is Nothing Then .Item(Col1_Head, I).Value = ""
                    If .Item(Col1_Head, I).Value <> "" Then
                        If Val(.Item(Col_SNo, I).Value) = 0 Then
                            MsgBox("SNo Is Required At Row No " & I & "...!", MsgBoxStyle.Information)
                            DGL1.CurrentCell = .Item(Col_SNo, I) : DGL1.Focus() : Exit Function
                        End If

                        If .Item(Col1_UpdateField, I).Value <> "" Then
                            If .Item(Col1_UpdateFieldType, I).Value = "" Then
                                MsgBox("Update Field Type Is Blank At Row No " & I + 1 & "")
                                DGL1.CurrentCell = .Item(Col1_UpdateFieldType, I) : DGL1.Focus() : Exit Function
                            End If
                        End If
                    End If
                Next
            End With
            Data_Validation = True
        Catch ex As Exception
            MsgBox(ex.Message)
            Data_Validation = False
        End Try
    End Function

    Private Sub BtnCopyFrom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCopyFrom.Click
        If TxtCopyFrom.Text <> "" Then
            If MsgBox("Sure to Copy From Selected Value?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                MoveRecLine(TxtCopyFrom.AgSelectedValue)
            End If
        End If
    End Sub
End Class
