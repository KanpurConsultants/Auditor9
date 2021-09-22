Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants

Public Class FrmPersonSiteTransporter


    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Division As String = "Division"
    Public Const Col1Site As String = "Site"
    Public Const Col1Transporter As String = "Transporter"

    Dim mSearchCode As String
    Dim mDtSubgroupTypeSettings As DataTable
    Dim mDataValidation As Boolean

    Dim mEntryMode$ = ""

    Public Property EntryMode() As String
        Get
            EntryMode = mEntryMode
        End Get
        Set(ByVal value As String)
            mEntryMode = value
        End Set
    End Property

    Public Property DataValidation() As Boolean
        Get
            DataValidation = mDataValidation
        End Get
        Set(ByVal value As Boolean)
            mDataValidation = value
        End Set
    End Property


    Public Property DtSubgroupTypeSettings() As DataTable
        Get
            DtSubgroupTypeSettings = mDtSubgroupTypeSettings
        End Get
        Set(ByVal value As DataTable)
            mDtSubgroupTypeSettings = value
        End Set
    End Property


    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    'Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
    '    AgL.FPaintForm(Me, e, 0)
    'End Sub

    Public Sub IniGrid(DocID As String)

        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Division, 160, 255, Col1Division, False, True)
            .AddAgTextColumn(Dgl1, Col1Site, 200, 255, Col1Site, True, True)
            .AddAgTextColumn(Dgl1, Col1Transporter, 400, 255, Col1Transporter, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.AllowUserToAddRows = False

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)

        FMoverec(DocID)
    End Sub
    Public Sub FMoverec(DocID As String)
        Dim DsTemp As DataSet = Nothing
        Dim I As Integer

        If DocID = "" Then
            If AgL.VNull(DtSubgroupTypeSettings.Rows(0)("PersonCanHaveDivisionWiseTransporterYn")) = True And AgL.VNull(DtSubgroupTypeSettings.Rows(0)("PersonCanHaveSiteWiseTransporterYn")) = True Then
                mQry = "Select D.Div_Code, D.Div_Name, S.Code as Site_Code, S.Name as Site_Name, Null as Transporter, Null as TransporterName
                From Division D, SiteMast S Order By D.Div_Name, S.Site_Name"
            ElseIf AgL.VNull(DtSubgroupTypeSettings.Rows(0)("PersonCanHaveDivisionWiseTransporterYn")) = True Then
                mQry = "Select D.Div_Code, D.Div_Name, Null as Site_Code, Null as Site_Name, Null as Transporter, Null as TransporterName
                From Division D "
            ElseIf AgL.VNull(DtSubgroupTypeSettings.Rows(0)("PersonCanHaveSiteWiseTransporterYn")) = True Then
                mQry = "Select Null as Div_Code, Null as Div_Name, S.Code as Site_Code, S.Name as Site_Name, Null as Transporter, Null as TransporterName
                From SiteMast S "
            End If
        Else
            If AgL.VNull(DtSubgroupTypeSettings.Rows(0)("PersonCanHaveDivisionWiseTransporterYn")) = True And AgL.VNull(DtSubgroupTypeSettings.Rows(0)("PersonCanHaveSiteWiseTransporterYn")) = True Then
                mQry = "Select D.Div_Code, D.Div_Name, S.Code as Site_Code, S.Name as Site_Name, L.Transporter, Rt.Name as TransporterName
                From Division D  With (NoLock)
                Left Join Site S With (NoLock) On 1 = 1               
                Left Join SubgroupSiteDivisionDetail L With (NoLock) On D.Div_Code = L.Div_Code And L.Subcode = '" & DocID & "'  
                Left Join viewHelpSubgroup Rt On L.Transporter = Rt.Code"
            ElseIf AgL.VNull(DtSubgroupTypeSettings.Rows(0)("PersonCanHaveDivisionWiseTransporterYn")) = True Then
                mQry = "Select Null as Div_Code, Null as Div_Name, S.Code as Site_Code, S.Name as Site_Name, L.Transporter, Rt.Name as TransporterName
                From Division D  With (NoLock)                
                Left Join SubgroupSiteDivisionDetail L With (NoLock) On D.Div_Code = L.Div_Code And L.Subcode = '" & DocID & "'  
                Left Join viewHelpSubgroup Rt On L.Transporter = Rt.Code"
            ElseIf AgL.VNull(DtSubgroupTypeSettings.Rows(0)("PersonCanHaveSiteWiseTransporterYn")) = True Then
                mQry = "Select Null as Div_Code, Null as Div_Name, Site.Code as Site_Code, Site.Name as Site_Name, L.Transporter, Rt.Name as TransporterName
                From SiteMast Site  With (NoLock)                
                Left Join SubgroupSiteDivisionDetail L With (NoLock) On Site.Code = L.Site_Code And L.Subcode = '" & DocID & "'  
                Left Join viewHelpSubgroup Rt On L.Transporter = Rt.Code"
            End If
        End If

        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                    'Dgl1.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))
                    Dgl1.Item(Col1Site, I).Tag = AgL.XNull(.Rows(I)("Site_Code"))
                    Dgl1.Item(Col1Site, I).Value = AgL.XNull(.Rows(I)("Site_Name"))
                    Dgl1.Item(Col1Division, I).Tag = AgL.XNull(.Rows(I)("Div_Code"))
                    Dgl1.Item(Col1Division, I).Value = AgL.XNull(.Rows(I)("Div_Name"))
                    Dgl1.Item(Col1Transporter, I).Tag = AgL.XNull(.Rows(I)("Transporter"))
                    Dgl1.Item(Col1Transporter, I).Value = AgL.XNull(.Rows(I)("TransporterName"))
                Next I
            End If
        End With
        Calculation()
    End Sub

    Function FData_Validation() As String
        Dim I As Integer
        Dim mMessage As String = ""

        'For I = 0 To Dgl1.Rows.Count - 1
        '    If Dgl1.Item(Col1Transporter, I).Value = "" Then
        '        If Dgl1.Item(Col1Division, I).Value <> "" And Dgl1.Item(Col1Site, I).Value <> "" Then

        '            mMessage = "Data is blank for Division : " & Dgl1.Item(Col1Division, I).Value & " And Site : " & Dgl1.Item(Col1Site, I).Value & ", S/w can not allowed blank data."
        '                Dgl1.CurrentCell = Dgl1.Item(Col1Transporter, I) : Dgl1.Focus()
        '                FData_Validation = mMessage
        '                Exit Function

        '            ElseIf Dgl1.Item(Col1Division, I).Value <> "" Then

        '            mMessage = "Data is blank for Division : " & Dgl1.Item(Col1Division, I).Value & ", S/w can not allowed blank data."
        '                Dgl1.CurrentCell = Dgl1.Item(Col1Transporter, I) : Dgl1.Focus()
        '                FData_Validation = mMessage
        '                Exit Function

        '            ElseIf Dgl1.Item(Col1Site, I).Value <> "" Then

        '            mMessage = "Data is blank for Site : " & Dgl1.Item(Col1Site, I).Value & ", S/w can not allowed blank data."
        '                Dgl1.CurrentCell = Dgl1.Item(Col1Transporter, I) : Dgl1.Focus()
        '                FData_Validation = mMessage
        '                Exit Function

        '        End If
        '    End If
        'Next





        FData_Validation = mMessage
    End Function

    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            Me.Close()
        End If
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(Dgl1)

            Dgl1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try

            If Dgl1.CurrentCell Is Nothing Then Exit Sub




            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing

            Select Case Dgl1.CurrentCell.RowIndex

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
    '    If e.Control And e.KeyCode = Keys.D Then
    '        sender.CurrentRow.Selected = True
    '    End If
    '    If e.Control Or e.Shift Or e.Alt Then Exit Sub
    'End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If mEntryMode = "Browse" Then Exit Sub


            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Transporter
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If Dgl1.AgHelpDataSet(Col1Transporter) Is Nothing Then
                            mQry = "SELECT Code, Name  FROM viewHelpSubgroup Where SubgroupType = '" & SubgroupType.Transporter & "' ORDER BY Name "
                            Dgl1.AgHelpDataSet(Col1Transporter) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If EntryMode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                'Case Col1FromUnit
                '    Dgl1.Item(Col1Equal, mRowIndex).Value = "="
                '    Dgl1.Item(Col1ToUnit, mRowIndex).Value = mUnit
                '    Dgl1.Item(Col1ToQtyDecimalPlaces, mRowIndex).Value = mToQtyDecimalPlace
                '    If Val(Dgl1.Item(Col1FromQty, mRowIndex).Value) = 0 Then
                '        Dgl1.Item(Col1FromQty, mRowIndex).Value = "1"
                '    End If

                '    If Dgl1.AgSelectedValue(Col1FromUnit, mRowIndex) Is Nothing Then Dgl1.AgSelectedValue(Col1FromUnit, mRowIndex) = ""

                '    If Dgl1.Item(Col1FromUnit, mRowIndex).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1FromUnit, mRowIndex).ToString.Trim = "" Then
                '        Dgl1.Item(Col1FromQtyDecimalPlaces, mRowIndex).Value = ""
                '    Else
                '        If Dgl1.AgDataRow IsNot Nothing Then
                '            Dgl1.Item(Col1FromQtyDecimalPlaces, mRowIndex).Value = AgL.XNull(Dgl1.AgDataRow.Cells("DecimalPlaces").Value)
                '        End If
                '    End If


            End Select
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim I As Integer = 0
        Select Case sender.Name
            Case BtnOk.Name
                If AgL.StrCmp(EntryMode, "Browse") Then Me.Close() : Exit Sub
                mOkButtonPressed = True
                Me.Close()
        End Select
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub

    Public Sub Calculation()

    End Sub

    Public Sub FSave(DocId As String, ByVal Conn As Object, ByVal Cmd As Object)
        Dim I As Integer, J As Integer
        Dim DtDivision As DataTable
        Dim DtSite As DataTable

        mQry = "Select Div_Code, Div_Name From Division Order By Div_Name"
        DtDivision = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

        mQry = "Select Code, Name From SiteMast Order By Name"
        DtSite = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

        If AgL.VNull(DtSubgroupTypeSettings.Rows(0)("PersonCanHaveDivisionWiseTransporterYn")) = True And AgL.VNull(DtSubgroupTypeSettings.Rows(0)("PersonCanHaveSiteWiseTransporterYn")) = True Then
            For I = 0 To Dgl1.RowCount - 1
                If Dgl1.Rows(I).Visible Then
                    mQry = " Update SubgroupSiteDivisionDetail Set Transporter =" & AgL.Chk_Text(Dgl1.Item(Col1Transporter, I).Tag) & "  
                             Where Div_Code = '" & Dgl1.Item(Col1Division, I).Tag & "' And Site_Code = '" & Dgl1.Item(Col1Site, I).Tag & "' And Subcode = '" & DocId & "' "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next

        ElseIf AgL.VNull(DtSubgroupTypeSettings.Rows(0)("PersonCanHaveDivisionWiseTransporterYn")) = True Then
            For J = 0 To DtSite.Rows.Count - 1
                For I = 0 To Dgl1.RowCount - 1
                    If Dgl1.Rows(I).Visible Then

                        mQry = " Update SubgroupSiteDivisionDetail Set Transporter =" & AgL.Chk_Text(Dgl1.Item(Col1Transporter, I).Tag) & "  
                                     Where Div_Code = '" & Dgl1.Item(Col1Division, I).Tag & "' And Site_Code = '" & DtSite.Rows(J)("Code") & "' And Subcode = '" & DocId & "' "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    End If
                Next
            Next

        ElseIf AgL.VNull(DtSubgroupTypeSettings.Rows(0)("PersonCanHaveSiteWiseTransporterYn")) = True Then
            For J = 0 To DtDivision.Rows.Count - 1
                For I = 0 To Dgl1.RowCount - 1
                    If Dgl1.Rows(I).Visible Then
                        mQry = " Update SubgroupSiteDivisionDetail Set Transporter =" & AgL.Chk_Text(Dgl1.Item(Col1Transporter, I).Tag) & "  
                                    Where Div_Code = '" & DtDivision.Rows(J)("Div_Code") & "' And Site_Code = '" & Dgl1.Item(Col1Site, I).Tag & "' And Subcode = '" & DocId & "' "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                Next
            Next
        End If

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        End If
    End Sub

    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.KeyDown
        If EntryMode = "Browse" Then
            Select Case e.KeyCode
                Case Keys.Right, Keys.Up, Keys.Left, Keys.Down, Keys.Enter
                Case Else
                    e.Handled = True
            End Select
            Exit Sub
        End If

        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If

        If e.KeyCode = Keys.Delete Then
            If sender.currentrow.selected Then
                sender.Rows(sender.currentcell.rowindex).Visible = False
                Calculation()
                e.Handled = True
            End If
        End If

        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub Dgl1_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles Dgl1.CellBeginEdit
        If mEntryMode = "BROWSE" Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Private Sub BtnOk_Click(sender As Object, e As EventArgs) Handles BtnOk.Click
        Me.Close()
    End Sub



    Private Sub FrmPersonSiteTransporter_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Dim I As Integer, mCountData As Integer, mDistinctData As Integer

        Dim mValidationMessage As String
        mValidationMessage = FData_Validation()

        If mValidationMessage <> "" Then
            If MsgBox(mValidationMessage & vbCrLf & "Do you want to exit without saving data? ", vbYesNo) = vbNo Then
                e.Cancel = True
                Exit Sub
            Else
                mDataValidation = False
                Exit Sub
            End If
        End If


        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Transporter, I).Value <> "" Then
                mCountData += 1
            End If
        Next

        If mCountData = 0 Then
            mDataValidation = False
            Exit Sub
        End If


        For I = 1 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Transporter, I).Value <> Dgl1.Item(Col1Transporter, I - 1).Value Then
                mDistinctData += 1
            End If
        Next

        If mDistinctData = 0 Then
            mDataValidation = False
            Exit Sub
        End If


        mDataValidation = True
    End Sub
End Class