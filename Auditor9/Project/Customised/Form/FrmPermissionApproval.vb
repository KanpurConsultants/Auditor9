Imports System.Data.SQLite
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports AgLibrary.ClsMain.agConstants

Public Class FrmPermissionApproval
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

    Protected Const Col1Code As String = "Code"
    Protected Const Col1UserName As String = "User"
    Protected Const Col1FromDate As String = "From Date"
    Protected Const Col1ToDate As String = "To Date"
    Protected Const Col1V_Type As String = "Voucher Type"
    Protected Const Col1Div_Code As String = "Division"
    Protected Const Col1Site_Code As String = "Site"
    Protected Const Col1EntryNo As String = "Entry No"
    Protected Const Col1V_Date As String = "Entry Date"
    Protected Const Col1Action As String = "Action"
    Protected Const Col1Reason As String = "Reason"
    Protected Const Col1ExpiryDate As String = "Expiry Date"
    Protected Const Col1BtnApprove As String = "Approve"
    Protected Const Col1BtnReject As String = "Reject"

    Dim mQry As String = ""
    Dim mGridRowNumber As Integer = 0

    Dim DTFind As New DataTable
    Dim fld As String
    Public HlpSt As String

    Dim DtSettingsData As New DataTable

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Public Sub InitSettingData()
        DtSettingsData.Columns.Add(Col1Code)
        DtSettingsData.Columns.Add(Col1UserName)
        DtSettingsData.Columns.Add(Col1FromDate)
        DtSettingsData.Columns.Add(Col1ToDate)
        DtSettingsData.Columns.Add(Col1V_Type)
        DtSettingsData.Columns.Add(Col1Div_Code)
        DtSettingsData.Columns.Add(Col1Site_Code)
        DtSettingsData.Columns.Add(Col1EntryNo)
        DtSettingsData.Columns.Add(Col1V_Date)
        DtSettingsData.Columns.Add(Col1Action)
        DtSettingsData.Columns.Add(Col1Reason)
        DtSettingsData.Columns.Add(Col1ExpiryDate)
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

        Dgl1.Columns(Col1UserName).Width = 70
        Dgl1.Columns(Col1FromDate).Width = 120
        Dgl1.Columns(Col1ToDate).Width = 120
        Dgl1.Columns(Col1V_Type).Width = 160
        Dgl1.Columns(Col1Div_Code).Width = 150
        Dgl1.Columns(Col1Site_Code).Width = 150
        Dgl1.Columns(Col1EntryNo).Width = 100
        Dgl1.Columns(Col1V_Date).Width = 120
        Dgl1.Columns(Col1Action).Width = 70
        Dgl1.Columns(Col1Reason).Width = 200
        Dgl1.Columns(Col1ExpiryDate).Width = 120

        Dgl1.Columns(Col1UserName).ReadOnly = True
        Dgl1.Columns(Col1FromDate).ReadOnly = True
        Dgl1.Columns(Col1ToDate).ReadOnly = True
        Dgl1.Columns(Col1V_Type).ReadOnly = True
        Dgl1.Columns(Col1Div_Code).ReadOnly = True
        Dgl1.Columns(Col1Site_Code).ReadOnly = True
        Dgl1.Columns(Col1EntryNo).ReadOnly = True
        Dgl1.Columns(Col1V_Date).ReadOnly = True
        Dgl1.Columns(Col1Action).ReadOnly = True
        Dgl1.Columns(Col1Reason).ReadOnly = True

        Dgl1.Columns(Col1Code).Visible = False


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
    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Dim MyCommand As OleDb.OleDbDataAdapter = Nothing
        Select Case sender.name
            Case BtnClose.Name
                Me.Close()
        End Select
    End Sub
    Private Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
    End Sub
    Public Sub MovRec()
        mQry = "SELECT H.Code, H.EntryBy, H.FromDate, H.ToDate, Vt.Description AS VoucherType, D.Div_Name AS Division, 
                S.Name AS Site, H.EntryNo, H.V_Date, H.Action, H.Reason
                FROM PermissionRequest H 
                LEFT JOIN Division D ON H.Div_Code = D.Div_Code
                LEFT JOIN SiteMast S ON H.Site_Code = S.Code
                LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type 
                WHERE H.ApproveBy IS NULL And H.RejectedBy Is Null "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            For I As Integer = 0 To DtTemp.Rows.Count - 1
                DtSettingsData.Rows.Add()
                DtSettingsData.Rows(mGridRowNumber)(Col1Code) = AgL.XNull(DtTemp.Rows(I)("Code"))
                DtSettingsData.Rows(mGridRowNumber)(Col1UserName) = AgL.XNull(DtTemp.Rows(I)("EntryBy"))
                DtSettingsData.Rows(mGridRowNumber)(Col1FromDate) = AgL.XNull(DtTemp.Rows(I)("FromDate"))
                DtSettingsData.Rows(mGridRowNumber)(Col1ToDate) = AgL.XNull(DtTemp.Rows(I)("ToDate"))
                DtSettingsData.Rows(mGridRowNumber)(Col1V_Type) = AgL.XNull(DtTemp.Rows(I)("VoucherType"))
                DtSettingsData.Rows(mGridRowNumber)(Col1Div_Code) = AgL.XNull(DtTemp.Rows(I)("Division"))
                DtSettingsData.Rows(mGridRowNumber)(Col1Site_Code) = AgL.XNull(DtTemp.Rows(I)("Site"))
                DtSettingsData.Rows(mGridRowNumber)(Col1EntryNo) = AgL.XNull(DtTemp.Rows(I)("EntryNo"))
                DtSettingsData.Rows(mGridRowNumber)(Col1V_Date) = AgL.XNull(DtTemp.Rows(I)("V_Date"))
                DtSettingsData.Rows(mGridRowNumber)(Col1Action) = AgL.XNull(DtTemp.Rows(I)("Action"))
                DtSettingsData.Rows(mGridRowNumber)(Col1Reason) = AgL.XNull(DtTemp.Rows(I)("Reason"))
                DtSettingsData.Rows(mGridRowNumber)(Col1ExpiryDate) = AgL.PubLoginDate
                mGridRowNumber += 1
            Next
        End If

        Dgl1.DataSource = DtSettingsData
        Ini_Grid()
    End Sub
    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            If Dgl1.CurrentCell Is Nothing Then Exit Sub

            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub



            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
    End Sub
    Private Sub FrmPermissionApproval_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
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
    Private Sub Dgl1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Dgl1.KeyPress
        Try
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
        If e.KeyCode = Keys.Delete Then TxtFind.Text = "" : FManageFindTextboxVisibility() : DtSettingsData.DefaultView.RowFilter = Nothing : Dgl1.CurrentCell = Dgl1(fld, 0) : DtSettingsData.DefaultView.RowFilter = Nothing
    End Sub
    Private Sub FManageFindTextboxVisibility()
        If TxtFind.Text = "" Then TxtFind.Visible = False : TxtFind.Visible = True
    End Sub
    Private Sub Dgl1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles Dgl1.DataBindingComplete
        Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
        FAddButtonColumn()
    End Sub
    Sub FAddButtonColumn()
        If Dgl1.Columns.Contains(Col1BtnApprove) = False Then
            Dim mButtongColumns As New DataGridViewButtonColumn
            mButtongColumns.Name = Col1BtnApprove
            Dgl1.Columns.Add(mButtongColumns)
            Dgl1.Columns(Col1BtnApprove).Width = 70
            Dgl1.Columns(Col1BtnApprove).HeaderText = " "
        End If

        If Dgl1.Columns.Contains(Col1BtnReject) = False Then
            Dim mButtongColumns As New DataGridViewButtonColumn
            mButtongColumns.Name = Col1BtnReject
            Dgl1.Columns.Add(mButtongColumns)
            Dgl1.Columns(Col1BtnReject).Width = 60
            Dgl1.Columns(Col1BtnReject).HeaderText = " "
        End If


        Dim bShowFromDateColumns As Boolean = False
        Dim bShowToDateColumns As Boolean = False
        For I As Integer = 0 To Dgl1.Rows.Count - 1
            Dgl1.Item(Col1BtnApprove, I).Value = "Approve"
            Dgl1.Item(Col1BtnReject, I).Value = "Reject"

            If Dgl1.Item(Col1FromDate, I).Value <> "" Then bShowFromDateColumns = True
            If Dgl1.Item(Col1ToDate, I).Value <> "" Then bShowToDateColumns = True
        Next
        Dgl1.Columns(Col1FromDate).Visible = bShowFromDateColumns
        Dgl1.Columns(Col1ToDate).Visible = bShowToDateColumns
    End Sub
    Private Sub Dgl1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Dim mRowIndex As Integer = 0, mColumnIndex As Integer = 0

        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1BtnApprove
                    If Dgl1.Item(Col1ExpiryDate, mRowIndex).Value = "" Then Dgl1.Item(Col1ExpiryDate, mRowIndex).Value = AgL.PubLoginDate
                    mQry = " UPDATE PermissionRequest Set ApproveBy = '" & AgL.PubUserName & "', 
                                ApproveDate = " & AgL.Chk_Date(AgL.PubLoginDate) & ",
                                ExpiryDate = " & AgL.Chk_Date(Dgl1.Item(Col1ExpiryDate, mRowIndex).Value) & "  
                                Where Code = '" & Dgl1.Item(Col1Code, mRowIndex).Value & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    Dgl1.Rows.Remove(Dgl1.Rows(mRowIndex))

                Case Col1BtnReject
                    mQry = " UPDATE PermissionRequest Set RejectedBy = '" & AgL.PubUserName & "', 
                                RejectedDate = " & AgL.Chk_Date(AgL.PubLoginDate) & " 
                                Where Code = '" & Dgl1.Item(Col1Code, mRowIndex).Value & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    Dgl1.Rows.Remove(Dgl1.Rows(mRowIndex))
            End Select
        Catch ex As Exception
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
                Case Col1ExpiryDate
                    If AgL.XNull(Dgl1.Item(Col1ExpiryDate, mRowIndex).Value) <> "" Then
                        Dgl1.Item(mColumnIndex, mRowIndex).Value = AgL.RetDate(Dgl1.Item(mColumnIndex, mRowIndex).Value)
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class