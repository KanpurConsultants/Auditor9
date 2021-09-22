Imports System.Data.SQLite
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class FrmSettings_Menus
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

    Protected Const Col1Parent As String = "Parent"
    Protected Const Col1MnuName As String = "MnuName"
    Protected Const Col1MenuText As String = "Menu"
    Protected Const Col1Value As String = "Value"
    Protected Const Col1IsVisible As String = "Is Visible"

    Dim mQry As String = ""

    Dim DTFind As New DataTable
    Dim fld As String
    Public HlpSt As String

    Dim DtSettingsData As New DataTable

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub
    Public Sub InitSettingData()
        DtSettingsData.Columns.Add(Col1Parent)
        DtSettingsData.Columns.Add(Col1MnuName)
        DtSettingsData.Columns.Add(Col1MenuText)
        DtSettingsData.Columns.Add(Col1Value)
        DtSettingsData.Columns.Add(Col1IsVisible)
    End Sub
    Private Sub Ini_Grid()
        Dgl1.ColumnHeadersHeight = 40

        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AgAllowFind = False
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.AgAllowFind = False


        Dgl1.AllowUserToAddRows = False
        Dgl1.EnableHeadersVisualStyles = True
        AgL.GridDesign(Dgl1)


        Dgl1.Columns(Col1Parent).Width = 300
        Dgl1.Columns(Col1MenuText).Width = 300
        Dgl1.Columns(Col1Value).Width = 120
        Dgl1.Columns(Col1IsVisible).Width = 75

        Dgl1.Columns(Col1Parent).ReadOnly = True
        Dgl1.Columns(Col1MenuText).ReadOnly = True
        Dgl1.Columns(Col1Value).ReadOnly = True
        Dgl1.Columns(Col1IsVisible).ReadOnly = True

        Dgl1.Columns(Col1MnuName).Visible = False

        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub
    Private Sub FrmBarcode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''AgL.WinSetting(Me, 654, 990, 0, 0)
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        InitSettingData()
        MovRec()
        Me.WindowState = FormWindowState.Maximized
    End Sub
    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click, BtnAdd.Click, BtnMakeAllOptionYes.Click, BtnMakeAllOptionNo.Click
        Dim MyCommand As OleDb.OleDbDataAdapter = Nothing
        Select Case sender.name
            Case BtnClose.Name
                Me.Close()
                ClsMain.FCreateSettingDataTable()

            Case BtnAdd.Name
                Dim FrmObj As New FrmSettings_Add()
                FrmObj.StartPosition = FormStartPosition.CenterScreen
                FrmObj.ShowDialog()
                If Not AgL.StrCmp(FrmObj.UserAction, "OK") Then Exit Sub
                MovRec()

            Case BtnMakeAllOptionYes.Name
                For I As Integer = 0 To Dgl1.Rows.Count - 1
                    Dgl1.Item(Col1IsVisible, I).Tag = 1
                    Dgl1.Item(Col1IsVisible, I).Value = "Yes"
                    ProcSave(Dgl1.Item(Col1MnuName, I).Value, "IsVisible", Dgl1.Item(Col1IsVisible, I).Tag)
                Next

            Case BtnMakeAllOptionNo.Name
                For I As Integer = 0 To Dgl1.Rows.Count - 1
                    Dgl1.Item(Col1IsVisible, I).Tag = 0
                    Dgl1.Item(Col1IsVisible, I).Value = "No"
                    ProcSave(Dgl1.Item(Col1MnuName, I).Value, "IsVisible", Dgl1.Item(Col1IsVisible, I).Tag)
                Next
        End Select
    End Sub
    Private Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
    End Sub
    Private Sub ProcSave(Code As String, FieldName As String, Value As Object)
        Dim mCode As Integer = 0
        Dim mPrimaryCode As Integer = 0
        Dim mTrans As String = ""
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            mQry = "UPDATE Menus Set " & FieldName & " = " + "'" + Value.ToString + "'" + " Where MnuName = " + "'" + Code + "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            If FieldName = "IsVisible" Then
                If Value.ToString <> "0" Then
                    If AgL.VNull(AgL.Dman_Execute(" Select Count(*) From User_Permission Where MnuName = " + "'" + Code + "' And UserName = 'Sa'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) = 0 Then
                        mQry = " Delete From User_Permission Where MnuName = " + "'" + Code + "' And UserName = 'Sa'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                Else
                    If AgL.VNull(AgL.Dman_Execute(" Select Count(*) From User_Permission Where MnuName = " + "'" + Code + "' And UserName = 'Sa'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()) = 0 Then
                        mQry = "INSERT INTO User_Permission (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                                SELECT 'Sa' AS UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent  FROM User_Permission WHERE MnuText  = 'Re-Check Bills' "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                End If
            End If

            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim I As Integer = 0, Cnt = 0
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Value
                    ProcSave(Dgl1.Item(Col1MnuName, mRowIndex).Value, "ShortCutKey", Dgl1.Item(Col1Value, mRowIndex).Value)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub FillData()
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0

        mQry = " SELECT M.Parent, M.MnuModule, M.MnuName, M.MnuText, M.ShortCutKey,
                    CASE WHEN M.IsVisible <> 0 THEN 'Yes' ELSE 'No' END AS IsVisible
                    FROM Menus M
                    Order By M.Parent, M.MnuText "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        For I = 0 To DtTemp.Rows.Count - 1
            DtSettingsData.Rows.Add()

            DtSettingsData.Rows(I)(Col1Parent) = GetFormattedString(AgL.XNull(DtTemp.Rows(I)("Parent")).ToString.Replace("Mnu", ""))
            DtSettingsData.Rows(I)(Col1MnuName) = AgL.XNull(DtTemp.Rows(I)("MnuName"))
            DtSettingsData.Rows(I)(Col1MenuText) = AgL.XNull(DtTemp.Rows(I)("MnuText"))
            DtSettingsData.Rows(I)(Col1Value) = AgL.XNull(DtTemp.Rows(I)("ShortCutKey"))
            DtSettingsData.Rows(I)(Col1IsVisible) = AgL.XNull(DtTemp.Rows(I)("IsVisible"))
        Next
        Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
    End Sub
    Public Sub MovRec()
        Try
            Dgl1.Rows.Clear()
            FillData()
            Dgl1.DataSource = DtSettingsData
            Ini_Grid()

            'For I As Integer = 0 To Dgl1.Columns.Count - 1
            '    Dim BlankValueColumn As DataRow() = DtSettingsData.Select("[" + Dgl1.Columns(I).Name + "] <> '' ")
            '    If BlankValueColumn.Length = 0 Then
            '        Dgl1.Columns(I).Visible = False
            '    End If
            'Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            If Dgl1.CurrentCell Is Nothing Then Exit Sub

            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Left Or e.KeyCode = Keys.Right Or e.KeyCode = Keys.Down Or
                e.KeyCode = Keys.Up Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Then
                Exit Sub
            End If

            If bColumnIndex <> Dgl1.Columns(Col1Value).Index And
                    bColumnIndex <> Dgl1.Columns(Col1IsVisible).Index Then
                Exit Sub
            End If

            If e.Control Or e.Shift Or e.Alt Then Exit Sub

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1IsVisible
                    FProcessYesNoColumns(e.KeyCode, Col1IsVisible, bRowIndex, "IsVisible")
                Case Col1Value
                    mQry = " Select 'o' As Tick, 'Alt' As Code 
                            UNION ALL 
                            Select 'o' As Tick, 'Ctrl' As Code 
                            UNION ALL 
                            Select 'o' As Tick, 'A' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'B' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'C' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'D' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'E' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'F' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'G' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'H' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'I' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'J' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'K' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'L' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'M' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'N' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'O' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'P' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'Q' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'R' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'S' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'T' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'U' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'V' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'W' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'X' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'Y' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'Z' As Code 
                            UNION ALL 
                            Select 'o' As Tick, 'F1' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'F2' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'F3' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'F4' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'F5' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'F6' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'F7' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'F8' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'F9' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'F10' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'F11' As Code
                            UNION ALL 
                            Select 'o' As Tick, 'F12' As Code "
                    Dgl1.Columns(Col1Value).Tag = AgL.FillData(mQry, AgL.GCn)

                    Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
                    FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(CType(Dgl1.Columns(Col1Value).Tag, DataSet).Tables(0)), "", 400, 400, , , False)
                    FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
                    FRH_Multiple.FFormatColumn(1, "Description", 250, DataGridViewContentAlignment.MiddleLeft)
                    FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
                    FRH_Multiple.ShowDialog()

                    If FRH_Multiple.BytBtnValue = 0 Then
                        If FRH_Multiple.FFetchData(1, "'", "'", "+", True) <> "" Then
                            Dgl1.Item(Col1Value, bRowIndex).Value = "+" + FRH_Multiple.FFetchData(1, "", "", "+", True)
                        Else
                            Dgl1.Item(Col1Value, bRowIndex).Value = ""
                        End If
                    End If
                    ProcSave(Dgl1.Item(Col1MnuName, Dgl1.CurrentCell.RowIndex).Value, "ShortCutKey", Dgl1.Item(Col1Value, Dgl1.CurrentCell.RowIndex).Value)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmMenus_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
            ClsMain.FCreateSettingDataTable()
        End If
    End Sub
    Private Sub Dgl1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Dgl1.KeyPress
        Try
            If Dgl1.CurrentCell IsNot Nothing Then
                If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1Value).Index Or
                        Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1IsVisible).Index Then
                    Exit Sub
                End If
            End If

            If e.KeyChar = vbCr Or e.KeyChar = vbCrLf Or e.KeyChar = vbTab Or e.KeyChar = ChrW(27) Then Exit Sub

            If Dgl1.CurrentCell IsNot Nothing Then
                If Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name = "Tick" Then Exit Sub
                fld = Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            End If

            If Dgl1.CurrentCell Is Nothing Then
                DtSettingsData.DefaultView.RowFilter = Nothing
            End If

            If Asc(e.KeyChar) = Keys.Back Then
                If TxtFind.Text <> "" Then TxtFind.Text = Microsoft.VisualBasic.Left(TxtFind.Text, Len(TxtFind.Text) - 1)
            End If

            FManageFindTextboxVisibility()

            TxtFind_KeyPress(TxtFind, e)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TxtFind_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtFind.KeyPress
        RowsFilter(HlpSt, Dgl1, sender, e, fld, DtSettingsData)
    End Sub

    Private Function RowsFilter(ByVal selStr As String, ByVal CtrlObj As Object, ByVal TXT As TextBox, ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal FndFldName As String, ByVal DTable As DataTable) As Integer
        Try
            Dim strExpr As String, findStr As String, bSelStr As String = ""
            Dim sa As String
            Dim IntRow As Integer
            Dim i As Integer
            sa = TXT.Text
            bSelStr = selStr

            If sa.Length = 0 And Asc(e.KeyChar) = 8 Then IntRow = 0 : CtrlObj.CurrentCell = CtrlObj(FndFldName, IntRow) : DtSettingsData.DefaultView.RowFilter = Nothing : Dgl1.CurrentCell = Dgl1(FndFldName, 0) : Exit Function
            If TXT.Text = "(null)" Then
                findStr = e.KeyChar
            Else
                findStr = IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19, TXT.Text, TXT.Text + e.KeyChar)
            End If
            strExpr = "ltrim([" & FndFldName & "])  like '" & findStr & "%' "
            i = InStr(selStr, "where", CompareMethod.Text)
            If i = 0 Then
                selStr = selStr + " where " + strExpr + "order by [" & FndFldName & "]"
            Else
                selStr = selStr + " and " + strExpr + "order by [" & FndFldName & "]"
            End If

            ''==================================< Filter DTFind For Searching >====================================================
            DtSettingsData.DefaultView.RowFilter = Nothing
            'DtSettingsData.DefaultView.RowFilter = " [" & FndFldName & "] like '%" & findStr & "%' "
            If DtSettingsData.DefaultView.RowFilter <> "" And DtSettingsData.DefaultView.RowFilter <> Nothing Then
                DtSettingsData.DefaultView.RowFilter += " And " + " [" & FndFldName & "] like '" & findStr & "%' "
            Else
                DtSettingsData.DefaultView.RowFilter += " [" & FndFldName & "] like '" & findStr & "%' "
            End If
            Try
                Dgl1.CurrentCell = Dgl1(FndFldName, 0)
            Catch ex As Exception
            End Try
            TXT.Text = TXT.Text + IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19, "", e.KeyChar)

            FManageFindTextboxVisibility()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub DGL1_Click(sender As Object, e As EventArgs) Handles Dgl1.Click
        TxtFind.Text = ""
        FManageFindTextboxVisibility()
    End Sub
    Private Sub DGL1_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles Dgl1.PreviewKeyDown
        If Dgl1.CurrentCell Is Nothing Then Exit Sub


        If Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name = Col1Value Then
            If e.KeyCode = Keys.Delete Then
                Dgl1.Item(Col1Value, Dgl1.CurrentCell.RowIndex).Value = ""
                ProcSave(Dgl1.Item(Col1MnuName, Dgl1.CurrentCell.RowIndex).Value, "ShortCutKey", Dgl1.Item(Col1Value, Dgl1.CurrentCell.RowIndex).Value)
            End If
        Else
            If e.KeyCode = Keys.Delete Then
                TxtFind.Text = ""
                FManageFindTextboxVisibility()
                DtSettingsData.DefaultView.RowFilter = Nothing
                Dgl1.CurrentCell = Dgl1(fld, 0)
                DtSettingsData.DefaultView.RowFilter = Nothing
            End If
        End If
    End Sub
    Private Sub FManageFindTextboxVisibility()
        If TxtFind.Text = "" Then TxtFind.Visible = False : TxtFind.Visible = True
    End Sub
    Private Sub Dgl1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles Dgl1.DataBindingComplete

        Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)

        'FAddButtonColumn()
    End Sub
    Private Sub FProcessYesNoColumns(bKeyCode As Keys, bColumnName As String, bRowIndex As Integer, FieldToSave As String)
        If AgL.StrCmp(ChrW(bKeyCode), "Y") Then
            Dgl1.Item(bColumnName, bRowIndex).Tag = 1
            Dgl1.Item(bColumnName, bRowIndex).Value = "Yes"
        ElseIf AgL.StrCmp(ChrW(bKeyCode), "N") Then
            Dgl1.Item(bColumnName, bRowIndex).Tag = 0
            Dgl1.Item(bColumnName, bRowIndex).Value = "No"
        End If

        If AgL.StrCmp(ChrW(bKeyCode), "Y") Or AgL.StrCmp(ChrW(bKeyCode), "N") Then
            If Dgl1.Item(bColumnName, bRowIndex).Tag = -1 Then
                Dgl1.Item(bColumnName, bRowIndex).Tag = 1
            End If

            ProcSave(Dgl1.Item(Col1MnuName, bRowIndex).Value, "IsVisible", Dgl1.Item(bColumnName, bRowIndex).Tag)
        End If
    End Sub
    Private Function GetFormattedString(FieldName As String)
        Dim FieldNameArr As MatchCollection = Regex.Matches(FieldName.Trim(), "[A-Z][a-z]+")
        Dim strFieldName As String = ""
        For J As Integer = 0 To FieldNameArr.Count - 1
            If strFieldName = "" Then
                strFieldName = FieldNameArr(J).ToString
            Else
                strFieldName += " " + FieldNameArr(J).ToString
            End If
        Next
        If strFieldName <> "" Then
            If strFieldName.ToUpper().Trim().Replace(" ", "").Replace("_", "") <> FieldName.ToUpper().Trim().Replace(" ", "").Replace("_", "") Then
                Return FieldName
            Else
                Return strFieldName
            End If
        Else
            Return FieldName
        End If
    End Function
End Class