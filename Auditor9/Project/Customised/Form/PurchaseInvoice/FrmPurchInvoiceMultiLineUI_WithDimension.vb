Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants

Public Class FrmPurchInvoiceMultiLineUI_WithDimension
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Size As String = "Size"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Rate As String = "Rate"


    Dim mSearchCode As String
    Dim mSearchCodeSr As Integer

    Dim DtItemRelation As DataTable

    Dim mEntryMode$ = ""
    Dim mUnit$ = ""
    Dim mItemName As String
    Dim mUnitDecimalPlace As Integer
    Dim mDglRow As DataGridViewRow
    Dim mDtV_TypeSettings As DataTable
    Dim mProcessCode$ = ""
    Dim mRateType$ = ""
    Dim mVType As String
    Dim mDivCode As String
    Dim mSiteCode As String
    Dim mSettingGroup As String
    Dim mPartyCode As String

    Public Property PartyCode() As String
        Get
            PartyCode = mPartyCode
        End Get
        Set(ByVal value As String)
            mPartyCode = value
        End Set
    End Property
    Public Property ProcessCode() As String
        Get
            ProcessCode = mProcessCode
        End Get
        Set(ByVal value As String)
            mProcessCode = value
        End Set
    End Property
    Public Property EntryMode() As String
        Get
            EntryMode = mEntryMode
        End Get
        Set(ByVal value As String)
            mEntryMode = value
        End Set
    End Property
    Public Property DglRow() As DataGridViewRow
        Get
            DglRow = mDglRow
        End Get
        Set(ByVal value As DataGridViewRow)
            mDglRow = value
        End Set
    End Property
    Public Property DtV_TypeSettings() As DataTable
        Get
            DtV_TypeSettings = mDtV_TypeSettings
        End Get
        Set(ByVal value As DataTable)
            mDtV_TypeSettings = value
        End Set
    End Property

    Public Property DivCode() As String
        Get
            DivCode = mDivCode
        End Get
        Set(ByVal value As String)
            mDivCode = value
        End Set
    End Property

    Public Property SiteCode() As String
        Get
            SiteCode = mSiteCode
        End Get
        Set(ByVal value As String)
            mSiteCode = value
        End Set
    End Property

    Public Property VType() As String
        Get
            VType = mVType
        End Get
        Set(ByVal value As String)
            mVType = value
        End Set
    End Property

    Public Property RateType() As String
        Get
            RateType = mRateType
        End Get
        Set(ByVal value As String)
            mRateType = value
        End Set
    End Property

    Public Property SettingGroup() As String
        Get
            SettingGroup = mSettingGroup
        End Get
        Set(ByVal value As String)
            mSettingGroup = value
        End Set
    End Property

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Public Sub IniGrid(DocID As String)
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Size, 180, 255, Col1Size, True, False)
            .AddAgNumberColumn(Dgl1, Col1Qty, 100, 8, 2, False, Col1Qty, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Rate, 100, 8, 2, False, Col1Rate, True, False, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub
    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            Me.Close()
        End If
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(Dgl1)
            FIniList()
            Me.Top = 400
            Me.Left = 400
            Dgl1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If AgL.StrCmp(EntryMode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing

            Select Case Dgl1.CurrentCell.RowIndex

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
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
                Case Col1Size
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Size) Is Nothing Then
                            FCreateHelpSize(bRowIndex)
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
                Case Col1Size
                    FCheckDuplicate(mRowIndex)

                    Dgl1.Item(Col1Rate, mRowIndex).Value = ClsMain.FGetRateWithRatePattern("", mPartyCode, mSettingGroup, mDivCode, mSiteCode, mProcessCode, mVType, mRateType,
                        AgL.XNull(DglRow.Cells(FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory).Tag),
                        AgL.XNull(DglRow.Cells(FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup).Tag),
                        AgL.XNull(DglRow.Cells(FrmPurchInvoiceDirect_WithDimension.Col1Item).Tag),
                        AgL.XNull(DglRow.Cells(FrmPurchInvoiceDirect_WithDimension.Col1Dimension1).Tag),
                        AgL.XNull(DglRow.Cells(FrmPurchInvoiceDirect_WithDimension.Col1Dimension2).Tag),
                        AgL.XNull(DglRow.Cells(FrmPurchInvoiceDirect_WithDimension.Col1Dimension3).Tag),
                        AgL.XNull(DglRow.Cells(FrmPurchInvoiceDirect_WithDimension.Col1Dimension4).Tag),
                        AgL.XNull(Dgl1.Item(Col1Size, mRowIndex).Tag))

                    If Val(Dgl1.Item(Col1Rate, mRowIndex).Value) > 0 Then
                        Dgl1.Item(Col1Rate, mRowIndex).Value = Val(Dgl1.Item(Col1Rate, mRowIndex).Value) +
                            ClsMain.FGetRateWithRatePattern(RateCategory.RateAddition, mPartyCode, mSettingGroup, mDivCode, mSiteCode, mProcessCode, mVType, mRateType,
                                AgL.XNull(DglRow.Cells(FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory).Tag),
                                AgL.XNull(DglRow.Cells(FrmPurchInvoiceDirect_WithDimension.Col1ItemGroup).Tag),
                                AgL.XNull(DglRow.Cells(FrmPurchInvoiceDirect_WithDimension.Col1Item).Tag),
                                AgL.XNull(DglRow.Cells(FrmPurchInvoiceDirect_WithDimension.Col1Dimension1).Tag),
                                AgL.XNull(DglRow.Cells(FrmPurchInvoiceDirect_WithDimension.Col1Dimension2).Tag),
                                AgL.XNull(DglRow.Cells(FrmPurchInvoiceDirect_WithDimension.Col1Dimension3).Tag),
                                AgL.XNull(DglRow.Cells(FrmPurchInvoiceDirect_WithDimension.Col1Dimension4).Tag),
                                AgL.XNull(Dgl1.Item(Col1Size, mRowIndex).Tag))
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        Dim I As Integer = 0
        Select Case sender.Name
            Case BtnOk.Name
                If AgL.StrCmp(EntryMode, "Browse") Then Me.Close() : Exit Sub
                'If FDataValidation() = False Then Exit Select
                mOkButtonPressed = True
                Me.Close()
        End Select
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
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
                e.Handled = True
            End If
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub FCreateHelpSize(RowIndex As Integer)
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")).ToString.Substring(0, 1) = "+" Then
                    strCond += " And CharIndex('+' || I.ItemType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
                ElseIf AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")).ToString.Substring(0, 1) = "-" Then
                    strCond += " And CharIndex('-' || I.ItemType,'" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') <= 0 "
                End If
            End If
        End If

        If AgL.XNull(DglRow.Cells(FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory).Tag) <> "" Then
            strCond += " And (I.ItemCategory = '" & AgL.XNull(DglRow.Cells(FrmPurchInvoiceDirect_WithDimension.Col1ItemCategory).Tag) & "' 
                            Or I.ItemCategory Is Null ) "
        End If

        strCond += " And I.V_Type = '" & ItemV_Type.SIZE & "' "

        If DtItemRelation.Rows.Count > 0 Then
            If AgL.XNull(DglRow.Cells(FrmPurchInvoiceDirect_WithDimension.Col1Dimension3).Tag) <> "" Then
                If DtItemRelation.Select("ItemV_Type = '" & ItemV_Type.Dimension3 & "'
                                And RelatedItemV_Type = '" & ItemV_Type.SIZE & "'").Length > 0 Then
                    Dim DrItemRelation As DataRow() = DtItemRelation.Select("Item = '" & AgL.XNull(DglRow.Cells(FrmPurchInvoiceDirect_WithDimension.Col1Dimension3).Tag) & "'")
                    Dim bFilterItems As String = ""
                    For I As Integer = 0 To DrItemRelation.Length - 1
                        If bFilterItems <> "" Then bFilterItems += ","
                        bFilterItems += AgL.Chk_Text(AgL.XNull(DrItemRelation(I)("RelatedItem")))
                    Next
                    strCond += " And I.Code In (" & bFilterItems & ") "
                End If
            End If
        End If

        mQry = "SELECT I.Code, I.Description
                        FROM Item I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        Dgl1.AgHelpDataSet(Col1Size) = AgL.FillData(mQry, AgL.GCn)
    End Sub
    Private Sub FIniList()
        mQry = "SELECT Ir.*, I.V_Type As ItemV_Type, RI.V_Type As RelatedItemV_Type 
                FROM ItemRelation Ir 
                LEFT JOIN Item I On Ir.Item = I.Code 
                LEFT JOIN Item RI On Ir.RelatedItem = Ri.Code "
        DtItemRelation = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub
    Private Function FDataValidation() As Boolean
        For I As Integer = 0 To Dgl1.Rows.Count - 1
            FCheckDuplicate(I)
        Next
    End Function
    Private Sub FCheckDuplicate(ByVal mRow As Integer)
        Dim I As Integer = 0
        Dim Str1 As String = ""
        Dim Str2 As String = ""
        Try
            With Dgl1
                For I = 0 To .Rows.Count - 1
                    If .Item(Col1Size, I).Value <> "" Then
                        If mRow <> I Then
                            Str1 = Dgl1.Item(Col1Size, I).Value
                            Str2 = Dgl1.Item(Col1Size, mRow).Value
                            If AgL.StrCmp(Str1, Str2) Then
                                If MsgBox("Size " & .Item(Col1Size, I).Value & " Is Already Feeded At Row No " & .Item(ColSNo, I).Value & ".Do You Want To Continue ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                                    Dgl1.Item(Col1Size, mRow).Tag = "" : Dgl1.Item(Col1Size, mRow).Value = ""
                                Else
                                    Dim mFirstRowIndex As Integer
                                    mFirstRowIndex = Val(Dgl1.Item(ColSNo, I).Value) - 1
                                    Dgl1.CurrentCell = Dgl1.Item(Col1Qty, mFirstRowIndex)
                                    Dgl1.Item(Col1Size, mRow).Tag = "" : Dgl1.Item(Col1Size, mRow).Value = ""
                                End If
                            End If
                        End If
                    End If
                Next
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class