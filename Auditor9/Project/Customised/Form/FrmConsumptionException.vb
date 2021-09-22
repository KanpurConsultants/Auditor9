Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants

Public Class FrmConsumptionException
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1SKU As String = "SKU"
    Public Const Col1Process As String = "Process"
    Public Const Col1Party As String = "Party"
    Public Const Col1RateType As String = "Rate Type"
    Public Const Col1ItemCategory As String = "Item Category"
    Public Const Col1ItemGroup As String = "Item Group"
    Public Const Col1Item As String = "Item"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1Dimension3 As String = "Dimension3"
    Public Const Col1Dimension4 As String = "Dimension4"
    Public Const Col1Size As String = "Size"
    Public Const Col1Qty As String = "Qty"

    Dim mEntryMode$ = ""
    Dim mItemCategory$ = ""
    Dim mItemCategoryName$ = ""
    Dim mDimension3$ = ""
    Dim mDimension3Name$ = ""
    Dim bGeneratedMainItemCode$ = ""
    Dim mDglRow As DataGridViewRow

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        bGeneratedMainItemCode = AgL.XNull(AgL.Dman_Execute("Select Code 
                        From Item Where ItemCategory = '" & mItemCategory & "'
                        And Dimension3 = '" & mDimension3 & "'
                        And V_Type = '" & ItemV_Type.BOM & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())
    End Sub
    Public Property EntryMode() As String
        Get
            EntryMode = mEntryMode
        End Get
        Set(ByVal value As String)
            mEntryMode = value
        End Set
    End Property
    Public Property ItemCategory() As String
        Get
            ItemCategory = mItemCategory
        End Get
        Set(ByVal value As String)
            mItemCategory = value
        End Set
    End Property
    Public Property ItemCategoryName() As String
        Get
            ItemCategoryName = mItemCategoryName
        End Get
        Set(ByVal value As String)
            mItemCategoryName = value
        End Set
    End Property
    Public Property Dimension3() As String
        Get
            Dimension3 = mDimension3
        End Get
        Set(ByVal value As String)
            mDimension3 = value
        End Set
    End Property
    Public Property Dimension3Name() As String
        Get
            Dimension3Name = mDimension3Name
        End Get
        Set(ByVal value As String)
            mDimension3Name = value
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
    Public Sub IniGrid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Process, 220, 0, Col1Process, True, False)
            .AddAgTextColumn(Dgl1, Col1Party, 100, 0, Col1Party, True, False)
            .AddAgTextColumn(Dgl1, Col1RateType, 100, 0, Col1RateType, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemCategory, 150, 0, Col1ItemCategory, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 200, 0, Col1ItemGroup, True, False)
            .AddAgTextColumn(Dgl1, Col1Item, 400, 0, Col1Item, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, Col1Dimension1, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, Col1Dimension2, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension3, 100, 0, Col1Dimension3, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension4, 100, 0, Col1Dimension4, True, False)
            .AddAgTextColumn(Dgl1, Col1Size, 100, 0, Col1Size, True, False)
            .AddAgTextColumn(Dgl1, Col1SKU, 300, 0, Col1SKU, True, False, False)
            .AddAgNumberColumn(Dgl1, Col1Qty, 70, 3, 3, False, Col1Qty, True, False, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True

        AgL.FSetDimensionCaptionForHorizontalGrid(Dgl1, AgL)
        ApplyUISetting()

        FMoverec()

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

            Me.Top = 400
            Me.Left = 400
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
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Process
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Process) Is Nothing Then
                            mQry = " Select H.SubCode, H.Name From SubGroup H Where H.SubGroupType = '" & SubgroupType.Process & "' Order By H.Name "
                            Dgl1.AgHelpDataSet(Col1Process) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
                Case Col1ItemCategory
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            mQry = "SELECT Code, Description FROM Item Where V_Type='" & ItemV_Type.ItemCategory & "'"
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
                Case Col1Dimension1
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            mQry = "SELECT I.Code, I.Description FROM Item I Where I.V_Type = '" & ItemV_Type.Dimension1 & "' Order By I.Description"
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
                Case Col1Dimension2
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            mQry = "SELECT I.Code, I.Description FROM Item I Where I.V_Type = '" & ItemV_Type.Dimension2 & "' Order By I.Description"
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
                Case Col1Dimension4
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            mQry = "SELECT I.Code, I.Description FROM Item I Where I.V_Type = '" & ItemV_Type.Dimension4 & "' Order By I.Description"
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
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
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        Dim I As Integer = 0
        Select Case sender.Name
            Case BtnOk.Name
                mOkButtonPressed = True
                Me.Close()
        End Select
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub
    Public Sub FPostConsumptionException(ByVal Conn As Object, ByVal Cmd As Object)
        Dim bRateListCode$ = "", bGeneratedMainItemDesc$ = ""
        Dim I As Integer, mSr As Integer

        FDataValidation()

        bGeneratedMainItemDesc = mItemCategoryName + "-" + mDimension3Name
        bGeneratedMainItemCode = AgL.XNull(AgL.Dman_Execute("Select Code 
                        From Item Where ItemCategory = '" & mItemCategory & "'
                        And Dimension3 = '" & mDimension3 & "'
                        And V_Type = '" & ItemV_Type.BOM & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())
        If bGeneratedMainItemCode = "" Then
            bGeneratedMainItemCode = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

            mQry = " INSERT INTO Item (Code, ManualCode, Description, Unit, EntryBy, EntryDate, Status, 
                Div_Code, Specification, ItemCategory, DealQty, Dimension3, StockYN, V_Type)
                Select " & AgL.Chk_Text(bGeneratedMainItemCode) & " As Code, Null As ManualCode, 
                " & AgL.Chk_Text(bGeneratedMainItemDesc) & " As Description, 'Nos' As Unit, 
                " & AgL.Chk_Text(AgL.PubUserName) & " As EntryBy, " & AgL.Chk_Date(AgL.PubLoginDate) & " As EntryDate, 
                " & AgL.Chk_Text(AgTemplate.ClsMain.EntryStatus.Active) & " As Status,  
                " & AgL.Chk_Text(AgL.PubDivCode) & " As Div_Code, 
                " & AgL.Chk_Text(bGeneratedMainItemDesc) & " As Specification, 
                " & AgL.Chk_Text(mItemCategory) & " As ItemCategory, 
                1 As BatchQty, 
                " & AgL.Chk_Text(mDimension3) & " As Dimension3, 
                0 As StockYN, " & AgL.Chk_Text(ItemV_Type.BOM) & " As V_Type "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Else

        End If

        mQry = "DELETE FROM BOMDetail WHERE Code  = '" & bGeneratedMainItemCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        For I = 0 To Dgl1.Rows.Count - 1
            If Val(Dgl1.Item(Col1Qty, I).Value) > 0 Then
                mQry = "INSERT INTO BOMDetail (Code, Sr, Process, Item, Qty)
                        VALUES ('" & bGeneratedMainItemCode & "', " & I + 1 & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1Process, I).Tag) & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1SKU, I).Tag) & "                                                 
                        ," & Val(Dgl1.Item(Col1Qty, I).Value) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
        Next

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        End If
    End Sub

    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.KeyDown
        Select Case e.KeyCode
            Case Keys.Right, Keys.Up, Keys.Left, Keys.Down, Keys.Enter
            Case Else
                e.Handled = True
        End Select

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
    Private Sub FDataValidation()
        Dim I As Integer = 0
        For I = 0 To Dgl1.Rows.Count - 1
            If Val(Dgl1.Item(Col1Qty, I).Value) <> 0 Then
                Dgl1.Item(Col1Process, I).Tag = ClsGarmentProduction.Process_Cutting
                Dgl1.Item(Col1Dimension3, I).Tag = mDimension3
                Dgl1.Item(Col1Dimension3, I).Value = mDimension3Name

                If AgL.XNull(Dgl1.Item(Col1ItemCategory, I).Value) <> "" _
                    Or AgL.XNull(Dgl1.Item(Col1ItemGroup, I).Value) <> "" _
                    Or AgL.XNull(Dgl1.Item(Col1Item, I).Value) <> "" _
                    Or AgL.XNull(Dgl1.Item(Col1Dimension1, I).Value) <> "" _
                    Or AgL.XNull(Dgl1.Item(Col1Dimension2, I).Value) <> "" _
                    Or AgL.XNull(Dgl1.Item(Col1Dimension3, I).Value) <> "" _
                    Or AgL.XNull(Dgl1.Item(Col1Dimension4, I).Value) <> "" _
                    Or AgL.XNull(Dgl1.Item(Col1Size, I).Value) <> "" _
                   Then
                    Dgl1.Item(Col1SKU, I).Tag = ClsMain.FGetSKUCode(Dgl1.Item(ColSNo, I).Value, ItemTypeCode.InternalProduct, Dgl1.Item(Col1ItemCategory, I).Tag, Dgl1.Item(Col1ItemCategory, I).Value _
                               , Dgl1.Item(Col1ItemGroup, I).Tag, Dgl1.Item(Col1ItemGroup, I).Value _
                               , Dgl1.Item(Col1Item, I).Tag, Dgl1.Item(Col1Item, I).Value _
                               , Dgl1.Item(Col1Dimension1, I).Tag, Dgl1.Item(Col1Dimension1, I).Value _
                               , Dgl1.Item(Col1Dimension2, I).Tag, Dgl1.Item(Col1Dimension2, I).Value _
                               , Dgl1.Item(Col1Dimension3, I).Tag, Dgl1.Item(Col1Dimension3, I).Value _
                               , Dgl1.Item(Col1Dimension4, I).Tag, Dgl1.Item(Col1Dimension4, I).Value _
                               , Dgl1.Item(Col1Size, I).Tag, Dgl1.Item(Col1Size, I).Value _
                               , "", "", "", "", "", "", "", "")
                    If Dgl1.Item(Col1SKU, I).Tag = "" Then
                        Err.Raise(1,, "Problem in Generating Line Sku.")
                    End If
                End If
            End If
        Next
    End Sub
    Private Sub ApplyUISetting()
        For I As Integer = 0 To Dgl1.Columns.Count - 1
            Dgl1.Columns(I).Visible = False
        Next

        Dgl1.Columns(ColSNo).Visible = True
        Dgl1.Columns(Col1ItemCategory).Visible = True
        Dgl1.Columns(Col1Dimension1).Visible = True
        Dgl1.Columns(Col1Dimension2).Visible = True
        Dgl1.Columns(Col1Dimension4).Visible = True
        Dgl1.Columns(Col1Qty).Visible = True
    End Sub
    Public Sub FMoverec()
        Dim DsTemp As DataSet
        Dim I As Integer = 0

        bGeneratedMainItemCode = AgL.XNull(AgL.Dman_Execute("Select Code 
                        From Item Where ItemCategory = '" & mItemCategory & "'
                        And Dimension3 = '" & mDimension3 & "'
                        And V_Type = '" & ItemV_Type.BOM & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())

        mQry = "SELECT H.*, 
                Sku.BaseItem, Sku.Description, Sku.ItemCategory, Sku.ItemGroup, SKU.Dimension1, SKU.Dimension2, Sku.Dimension3, Sku.Dimension4, Sku.Size, Sku.RawMaterial, Sku.Unit,
                IC.Description as ItemCategoryName, IG.Description as ItemGroupName,
                D1.Description as Dimension1Name,D2.Description as Dimension2Name,
                D3.Description as Dimension3Name,D4.Description as Dimension4Name,
                Size.Description as SizeName, RawMaterial.Description as RawMaterialName, P.Name as ProcessName, I.Code as ItemCode, I.Description as ItemName,                
                I.ItemCategory as MItemCategory, I.ItemGroup as MItemGroup, I.Specification as MItemSpecification, 
                I.Dimension1 as MDimension1,  I.Dimension2 as MDimension2,  I.Dimension3 as MDimension3,  I.Dimension4 as MDimension4,  I.Size as MSize
                FROM BOMDetail H
                LEFT JOIN Item Sku ON Sku.Code = H.Item 
                LEFT JOIN Item I ON I.Code = IfNull(Sku.BaseItem,Sku.Code) 
                Left Join Item IC On Sku.ItemCategory = IC.Code
                Left Join Item IG On Sku.ItemGroup = IG.Code
                LEFT JOIN Item D1 ON D1.Code = Sku.Dimension1  
                LEFT JOIN Item D2 ON D2.Code = Sku.Dimension2
                LEFT JOIN Item D3 ON D3.Code = Sku.Dimension3
                LEFT JOIN Item D4 ON D4.Code = Sku.Dimension4
                LEFT JOIN Item Size ON Size.Code = Sku.Size
                LEFT JOIN Item RawMaterial ON RawMaterial.Code = Sku.RawMaterial
                Left Join Subgroup P On H.Process = P.Subcode
                WHERE H.Code ='" & bGeneratedMainItemCode & "'
                ORDER BY H.Sr "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                    Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("ItemCode"))
                    Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemName"))
                    Dgl1.Item(Col1ItemCategory, I).Tag = AgL.XNull(.Rows(I)("ItemCategory"))
                    Dgl1.Item(Col1ItemCategory, I).Value = AgL.XNull(.Rows(I)("ItemCategoryName"))
                    Dgl1.Item(Col1ItemGroup, I).Tag = AgL.XNull(.Rows(I)("ItemGroup"))
                    Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupName"))
                    Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                    Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Name"))
                    Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                    Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Name"))
                    Dgl1.Item(Col1Dimension3, I).Tag = AgL.XNull(.Rows(I)("Dimension3"))
                    Dgl1.Item(Col1Dimension3, I).Value = AgL.XNull(.Rows(I)("Dimension3Name"))
                    Dgl1.Item(Col1Dimension4, I).Tag = AgL.XNull(.Rows(I)("Dimension4"))
                    Dgl1.Item(Col1Dimension4, I).Value = AgL.XNull(.Rows(I)("Dimension4Name"))
                    Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.00")
                Next I
            End If
        End With
    End Sub
End Class