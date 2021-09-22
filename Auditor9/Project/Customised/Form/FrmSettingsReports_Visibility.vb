Imports System.Data.SQLite
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports AgLibrary.ClsMain.agConstants

Public Class FrmSettingsReports_Visibility
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

    Protected Const Col1TableName As String = "Table Name"
    Protected Const Col1TableDispName As String = "Table Disp Name"
    Protected Const Col1Code As String = "Code"
    Protected Const Col1ReportName As String = "Report Name"
    Protected Const Col1ReportFormatName As String = "NCat"
    Protected Const Col1GridName As String = "Grid Name"
    Protected Const Col1SiteName As String = "Site"
    Protected Const Col1DivisionName As String = "Division"
    Protected Const Col1FieldName As String = "Field Name"
    Protected Const Col1IsVisible As String = "Is Visible"
    Protected Const Col1IsMandatory As String = "Is Mandatory"
    Protected Const Col1IsEditable As String = "Is Editable"
    Protected Const Col1IsSystemDefined As String = "Is System Defined"
    Protected Const Col1DisplayIndex As String = "Display Index"
    Protected Const Col1Caption As String = "Caption"


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
        DtSettingsData.Columns.Add(Col1TableName)
        DtSettingsData.Columns.Add(Col1TableDispName)
        DtSettingsData.Columns.Add(Col1Code)
        DtSettingsData.Columns.Add(Col1ReportName)
        DtSettingsData.Columns.Add(Col1ReportFormatName)
        DtSettingsData.Columns.Add(Col1GridName)
        DtSettingsData.Columns.Add(Col1SiteName)
        DtSettingsData.Columns.Add(Col1DivisionName)
        DtSettingsData.Columns.Add(Col1FieldName)
        DtSettingsData.Columns.Add(Col1IsVisible)
        DtSettingsData.Columns.Add(Col1IsMandatory)
        DtSettingsData.Columns.Add(Col1IsEditable)
        DtSettingsData.Columns.Add(Col1IsSystemDefined)
        DtSettingsData.Columns.Add(Col1DisplayIndex)
        DtSettingsData.Columns.Add(Col1Caption)
    End Sub

    Private Sub Ini_Grid()
        Dgl1.ColumnHeadersHeight = 55

        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AgAllowFind = False
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.AgAllowFind = False


        Dgl1.AllowUserToAddRows = False
        Dgl1.EnableHeadersVisualStyles = True
        AgL.GridDesign(Dgl1)


        Dgl1.Columns(Col1GridName).Width = 100
        Dgl1.Columns(Col1SiteName).Width = 100
        Dgl1.Columns(Col1DivisionName).Width = 100
        Dgl1.Columns(Col1ReportName).Width = 300
        Dgl1.Columns(Col1ReportFormatName).Width = 140
        Dgl1.Columns(Col1FieldName).Width = 230
        Dgl1.Columns(Col1IsVisible).Width = 75
        Dgl1.Columns(Col1IsMandatory).Width = 85
        Dgl1.Columns(Col1IsEditable).Width = 80
        Dgl1.Columns(Col1IsSystemDefined).Width = 80
        Dgl1.Columns(Col1DisplayIndex).Width = 80
        Dgl1.Columns(Col1Caption).Width = 135


        Dgl1.Columns(Col1GridName).ReadOnly = True
        Dgl1.Columns(Col1SiteName).ReadOnly = True
        Dgl1.Columns(Col1DivisionName).ReadOnly = True
        Dgl1.Columns(Col1ReportName).ReadOnly = True
        Dgl1.Columns(Col1ReportFormatName).ReadOnly = True
        Dgl1.Columns(Col1FieldName).ReadOnly = True
        Dgl1.Columns(Col1IsVisible).ReadOnly = True
        Dgl1.Columns(Col1IsMandatory).ReadOnly = True
        Dgl1.Columns(Col1IsEditable).ReadOnly = True

        Dgl1.Columns(Col1TableName).Visible = False
        Dgl1.Columns(Col1TableDispName).Visible = False
        Dgl1.Columns(Col1Code).Visible = False
        Dgl1.Columns(Col1IsSystemDefined).Visible = False
        Dgl1.Columns(Col1DisplayIndex).Visible = False

        Dgl1.Columns(Col1IsEditable).Visible = False
        Dgl1.Columns(Col1IsMandatory).Visible = False

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
    Private Sub ProcSave(TableName As String, Code As String, FieldName As String, Value As Object)
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

            mQry = "UPDATE " + TableName + " Set " + FieldName + " = " + "'" + Value.ToString + "'" + " Where Code = " + "'" + Code + "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub MovRec()
        Dim I As Integer = 0
        mGridRowNumber = 0
        Dgl1.Rows.Clear()

        GetSettingDataForTable("ReportHeaderUISetting", "Header")
        GetSettingDataForTable("ReportLineUISetting", "Line")

        Dgl1.DataSource = DtSettingsData
        Ini_Grid()

        For I = 0 To Dgl1.Columns.Count - 1
            Dim BlankValueColumn As DataRow() = DtSettingsData.Select("[" + Dgl1.Columns(I).Name + "] <> '' ")
            If BlankValueColumn.Length = 0 Then
                Dgl1.Columns(I).Visible = False
            End If
        Next
    End Sub

    Public Sub GetSettingDataForTable(mTableName As String, mTableDispName As String)
        Dim I As Integer = 0

        mQry = "SELECT H.Code As [Code], 
                H.ReportName As [Report Name], H.GridName As [Grid Name], S.Name AS [Site], D.Div_Name AS [Division], H.ReportFormatName As [Report Format Name], 
                H.FieldName As [Field Name],
                CASE WHEN H.IsVisible <> 0 THEN 'Yes' ELSE 'No' END AS [Is Visible],
                CASE WHEN H.IsMandatory <> 0 THEN 'Yes' ELSE 'No' END AS [Is Mandatory],
                CASE WHEN H.IsEditable <> 0 THEN 'Yes' ELSE 'No' END AS [Is Editable],
                CASE WHEN H.IsSystemDefined <> 0 THEN 'Yes' ELSE 'No' END AS [Is System Defined],
                H.DisplayIndex As [Display Index], 
                H.Caption As [Caption]
                FROM " & mTableName & " H  
                LEFT JOIN SiteMast S ON H.Site_Code = S.Code
                LEFT JOIN Division D ON H.Div_Code = D.Div_Code "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                DtSettingsData.Rows.Add()
                DtSettingsData.Rows(mGridRowNumber)(Col1TableName) = mTableName
                DtSettingsData.Rows(mGridRowNumber)(Col1TableDispName) = mTableDispName
                DtSettingsData.Rows(mGridRowNumber)(Col1Code) = AgL.XNull(DtTemp.Rows(I)("Code"))
                DtSettingsData.Rows(mGridRowNumber)(Col1ReportName) = AgL.XNull(DtTemp.Rows(I)("Report Name"))
                DtSettingsData.Rows(mGridRowNumber)(Col1GridName) = AgL.XNull(DtTemp.Rows(I)("Grid Name"))
                DtSettingsData.Rows(mGridRowNumber)(Col1SiteName) = AgL.XNull(DtTemp.Rows(I)("Site"))
                DtSettingsData.Rows(mGridRowNumber)(Col1DivisionName) = AgL.XNull(DtTemp.Rows(I)("Division"))
                DtSettingsData.Rows(mGridRowNumber)(Col1ReportFormatName) = AgL.XNull(DtTemp.Rows(I)("Report Format Name"))
                'DtSettingsData.Rows(mGridRowNumber)(Col1FieldName) = AgL.XNull(DtTemp.Rows(I)("Field Name"))
                DtSettingsData.Rows(mGridRowNumber)(Col1FieldName) = FGetFieldCaptionName(AgL.XNull(DtTemp.Rows(I)("Field Name")))
                DtSettingsData.Rows(mGridRowNumber)(Col1IsVisible) = AgL.XNull(DtTemp.Rows(I)("Is Visible"))
                DtSettingsData.Rows(mGridRowNumber)(Col1IsMandatory) = AgL.XNull(DtTemp.Rows(I)("Is Mandatory"))
                DtSettingsData.Rows(mGridRowNumber)(Col1IsEditable) = AgL.XNull(DtTemp.Rows(I)("Is Editable"))
                DtSettingsData.Rows(mGridRowNumber)(Col1IsSystemDefined) = AgL.XNull(DtTemp.Rows(I)("Is System Defined"))
                DtSettingsData.Rows(mGridRowNumber)(Col1DisplayIndex) = AgL.XNull(DtTemp.Rows(I)("Display Index"))
                DtSettingsData.Rows(mGridRowNumber)(Col1Caption) = AgL.XNull(DtTemp.Rows(I)("Caption"))
                mGridRowNumber += 1
            Next
        End If
    End Sub
    Private Function FGetFieldCaptionName(FieldName As String)
        If FieldName.Contains("Dimension1") And AgL.PubCaptionDimension1 <> "" Then
            FGetFieldCaptionName = FieldName.Replace("Dimension1", AgL.PubCaptionDimension1)
        ElseIf FieldName.Contains("Dimension2") And AgL.PubCaptionDimension2 <> "" Then
            FGetFieldCaptionName = FieldName.Replace("Dimension2", AgL.PubCaptionDimension2)
        ElseIf FieldName.Contains("Dimension3") And AgL.PubCaptionDimension3 <> "" Then
            FGetFieldCaptionName = FieldName.Replace("Dimension3", AgL.PubCaptionDimension3)
        ElseIf FieldName.Contains("Dimension4") And AgL.PubCaptionDimension4 <> "" Then
            FGetFieldCaptionName = FieldName.Replace("Dimension4", AgL.PubCaptionDimension4)
        Else
            FGetFieldCaptionName = FieldName
        End If
    End Function
    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            If Dgl1.CurrentCell Is Nothing Then Exit Sub

            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> Dgl1.Columns(Col1IsVisible).Index And
                bColumnIndex <> Dgl1.Columns(Col1IsMandatory).Index And
                bColumnIndex <> Dgl1.Columns(Col1IsEditable).Index Then
                Exit Sub
            End If

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1IsVisible
                    FProcessYesNoColumns(e.KeyCode, Col1IsVisible, bRowIndex, "IsVisible")
                Case Col1IsMandatory
                    FProcessYesNoColumns(e.KeyCode, Col1IsMandatory, bRowIndex, "IsMandatory")
                Case Col1IsEditable
                    FProcessYesNoColumns(e.KeyCode, Col1IsEditable, bRowIndex, "IsEditable")
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FProcessYesNoColumns(bKeyCode As Keys, bColumnName As String, bRowIndex As Integer, FieldToSave As String)
        If AgL.StrCmp(ChrW(bKeyCode), "Y") Then
            If FDataValidation(bRowIndex) = False Then Exit Sub
            Dgl1.Item(bColumnName, bRowIndex).Tag = 1
            Dgl1.Item(bColumnName, bRowIndex).Value = "Yes"
        ElseIf AgL.StrCmp(ChrW(bKeyCode), "N") Then
            If FDataValidation(bRowIndex) = False Then Exit Sub
            Dgl1.Item(bColumnName, bRowIndex).Tag = 0
            Dgl1.Item(bColumnName, bRowIndex).Value = "No"
        End If

        If AgL.StrCmp(ChrW(bKeyCode), "Y") Or AgL.StrCmp(ChrW(bKeyCode), "N") Then
            If Dgl1.Item(bColumnName, bRowIndex).Tag = -1 Then
                Dgl1.Item(bColumnName, bRowIndex).Tag = 1
            End If

            ProcSave(Dgl1.Item(Col1TableName, bRowIndex).Value, Dgl1.Item(Col1Code, bRowIndex).Value,
                            FieldToSave, Dgl1.Item(bColumnName, bRowIndex).Tag)
        End If
    End Sub

    Private Sub Dgl1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
    End Sub
    Private Sub FrmSettingsReports_Visibility_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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
            If Dgl1.CurrentCell IsNot Nothing Then
                If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1IsVisible).Index Or
                        Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1IsMandatory).Index Or
                        Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1IsEditable).Index Then
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
            'DtSettingsData.DefaultView.RowFilter = Nothing
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
    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim I As Integer = 0, Cnt = 0
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""

            If FDataValidation(mRowIndex) = False Then Exit Sub

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Caption
                    If Dgl1.Item(Col1Caption, mRowIndex).Value <> "" Then
                        ProcSave(Dgl1.Item(Col1TableName, mRowIndex).Value,
                             Dgl1.Item(Col1Code, mRowIndex).Value,
                             "Caption", Dgl1.Item(Col1Caption, mRowIndex).Value)
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function FDataValidation(bRowIndex As Integer)
        FDataValidation = True
        If Dgl1.Item(Col1IsSystemDefined, bRowIndex).Value = "Yes" Then
            MsgBox("Value is system defined.Can't change...!", MsgBoxStyle.Information)
            FDataValidation = False
        End If
    End Function
    Private Sub Dgl1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles Dgl1.DataBindingComplete
        Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
    End Sub
End Class