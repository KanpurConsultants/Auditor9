Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain
Public Class FrmItemCategory_Grid
    Inherits AgTemplate.TempMaster

    Dim mQry$

    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1LastValue As String = "Last Value"
    Public Const Col1HeadOriginal As String = "Head Original"

    Public Const ColSNo As String = "SNo"
    Public WithEvents DGL2 As New AgControls.AgDataGrid
    Public Const Col1WEF As String = "WEF"
    Public Const Col1RateGreaterThan As String = "Rate Greater Than"
    Public Const Col1SalesTaxGroup As String = "Sales Tax Group"


    Dim rowItemType As Integer = 0
    Dim rowDescription As Integer = 1
    Dim rowUnit As Integer = 2
    Dim rowStockUnit As Integer = 3
    Dim rowDealUnit As Integer = 4
    Dim rowSalesTaxGroup As Integer = 5
    Dim rowHSN As Integer = 6
    Dim rowDepartment As Integer = 7
    Dim rowParent As Integer = 8
    Dim rowSalesRepresentativeCommissionPer As Integer = 9
    Dim rowBarcodeType As Integer = 10
    Dim rowBarcodePattern As Integer = 11
    Dim rowAddition As Integer = 12
    Dim rowLossQty As Integer = 13
    Dim rowLossQtyPer As Integer = 14
    Dim rowLossDealQty As Integer = 15
    Dim rowLossDealQtyPer As Integer = 16
    Dim rowIsShowDimensionDetailInPurchase As Integer = 17
    Dim rowIsShowDimensionDetailInSale As Integer = 18
    Dim rowIsLotApplicable As Integer = 19
    Dim rowIsStockInPcsApplicable As Integer = 20
    Dim rowIsNewItemAllowedPurch As Integer = 21
    Dim rowIsNewDimension1AllowedPurch As Integer = 22
    Dim rowIsNewDimension2AllowedPurch As Integer = 23
    Dim rowIsNewDimension3AllowedPurch As Integer = 24
    Dim rowIsNewDimension4AllowedPurch As Integer = 25


    Public Const hcItemType As String = "Item Type"
    Public Const hcDescription As String = "Description"
    Public Const hcUnit As String = "Unit"
    Public Const hcStockUnit As String = "Stock Unit"
    Public Const hcDealUnit As String = "Deal Unit"
    Public Const hcSalesTaxGroup As String = "Sales Tax Group"
    Public Const hcHSN As String = "HSN"
    Public Const hcDepartment As String = "Department"
    Public Const hcParent As String = "Parent"
    Public Const hcBarcodeType As String = "Barcode Type"
    Public Const hcSalesRepresentativeCommissionPer As String = "Sales Representative Commision %"
    Public Const hcBarcodePattern As String = "Barcode Pattern"
    Public Const hcAddition As String = "Addition"
    Public Const hcLossQty As String = "Loss Qty"
    Public Const hcLossQtyPer As String = "Loss Qty @"
    Public Const hcLossDealQty As String = "Loss Deal Qty"
    Public Const hcLossDealQtyPer As String = "Loss Deal Qty @"
    Public Const hcIsShowDimensionDetailInPurchase As String = "Is Show Dimension Detail In Purchase"
    Public Const hcIsShowDimensionDetailInSale As String = "Is Show Dimension Detail In Sale"
    Public Const hcIsLotApplicable As String = "Is Lot Applicable"
    Public Const hcIsStockInPcsApplicable As String = "Is Stock In Pcs Applicable"
    Public Const hcIsNewItemAllowedPurch As String = "Is New Item Allowed Purch"
    Public Const hcIsNewDimension1AllowedPurch As String = "Is New Dimension1 Allowed Purch"
    Public Const hcIsNewDimension2AllowedPurch As String = "Is New Dimension2 Allowed Purch"
    Public Const hcIsNewDimension3AllowedPurch As String = "Is New Dimension3 Allowed Purch"
    Public Const hcIsNewDimension4AllowedPurch As String = "Is New Dimension4 Allowed Purch"

    Dim DtItemTypeSetting As DataTable
    Public WithEvents Pnl1 As Panel
    Dim mItemTypeLastValue As String

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.LblIsSystemDefine = New System.Windows.Forms.Label()
        Me.ChkIsSystemDefine = New System.Windows.Forms.CheckBox()
        Me.Pnl2 = New System.Windows.Forms.Panel()
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
        Me.Topctrl1.Size = New System.Drawing.Size(897, 41)
        Me.Topctrl1.TabIndex = 12
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 458)
        Me.GroupBox1.Size = New System.Drawing.Size(939, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(14, 462)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(148, 527)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(231, 462)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(401, 462)
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
        Me.GroupBox2.Location = New System.Drawing.Point(704, 462)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(470, 462)
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
        'LblIsSystemDefine
        '
        Me.LblIsSystemDefine.AutoSize = True
        Me.LblIsSystemDefine.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIsSystemDefine.ForeColor = System.Drawing.Color.Red
        Me.LblIsSystemDefine.Location = New System.Drawing.Point(148, 483)
        Me.LblIsSystemDefine.Name = "LblIsSystemDefine"
        Me.LblIsSystemDefine.Size = New System.Drawing.Size(96, 15)
        Me.LblIsSystemDefine.TabIndex = 1063
        Me.LblIsSystemDefine.Text = "IsSystemDefine"
        '
        'ChkIsSystemDefine
        '
        Me.ChkIsSystemDefine.AutoSize = True
        Me.ChkIsSystemDefine.BackColor = System.Drawing.Color.Transparent
        Me.ChkIsSystemDefine.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkIsSystemDefine.ForeColor = System.Drawing.Color.Red
        Me.ChkIsSystemDefine.Location = New System.Drawing.Point(127, 484)
        Me.ChkIsSystemDefine.Name = "ChkIsSystemDefine"
        Me.ChkIsSystemDefine.Size = New System.Drawing.Size(15, 14)
        Me.ChkIsSystemDefine.TabIndex = 1062
        Me.ChkIsSystemDefine.UseVisualStyleBackColor = False
        '
        'Pnl2
        '
        Me.Pnl2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl2.Location = New System.Drawing.Point(7, 310)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(886, 139)
        Me.Pnl2.TabIndex = 7
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(7, 45)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(886, 260)
        Me.Pnl1.TabIndex = 1064
        '
        'FrmItemCategory_Grid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(897, 506)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.Pnl2)
        Me.Controls.Add(Me.LblIsSystemDefine)
        Me.Controls.Add(Me.ChkIsSystemDefine)
        Me.MaximizeBox = True
        Me.Name = "FrmItemCategory_Grid"
        Me.Text = "Item Category Master"
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.ChkIsSystemDefine, 0)
        Me.Controls.SetChildIndex(Me.LblIsSystemDefine, 0)
        Me.Controls.SetChildIndex(Me.Pnl2, 0)
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
        Me.PerformLayout()

    End Sub
    Public WithEvents LblIsSystemDefine As System.Windows.Forms.Label
    Friend WithEvents ChkIsSystemDefine As System.Windows.Forms.CheckBox
    Public WithEvents Pnl2 As Panel
#End Region


    Private Sub FGetItemTypeSetting()
        If mItemTypeLastValue <> Dgl1.Item(Col1Value, rowItemType).Tag And Dgl1.Item(Col1Value, rowItemType).Tag <> "" Then
            mItemTypeLastValue = Dgl1.Item(Col1Value, rowItemType).Tag
            mQry = "Select * From ItemTypeSetting Where ItemType = '" & Dgl1.Item(Col1Value, rowItemType).Tag & "' And Div_Code = '" & TxtDivision.Tag & "' "
            DtItemTypeSetting = AgL.FillData(mQry, AgL.GCn).tables(0)
            If DtItemTypeSetting.Rows.Count = 0 Then
                mQry = "Select * From ItemTypeSetting Where ItemType = '" & Dgl1.Item(Col1Value, rowItemType).Tag & "' And Div_Code Is Null "
                DtItemTypeSetting = AgL.FillData(mQry, AgL.GCn).tables(0)
                If DtItemTypeSetting.Rows.Count = 0 Then
                    mQry = "Select * From ItemTypeSetting Where ItemType Is Null And Div_Code Is Null "
                    DtItemTypeSetting = AgL.FillData(mQry, AgL.GCn).tables(0)
                    If DtItemTypeSetting.Rows.Count = 0 Then
                        MsgBox("Item Type Setting Not Found")
                    End If
                End If
            End If
        End If

        ApplyItemTypeSetting(Dgl1(Col1Value, rowItemType).Tag)
    End Sub

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If Dgl1.Item(Col1Value, rowDescription).Value.Trim = "" Then Err.Raise(1, , "Description Is Required!")

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From ItemCategory Where Description='" & Dgl1.Item(Col1Value, rowDescription).Value & "' And " & AgTemplate.ClsMain.RetDivFilterStr & "  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        Else
            mQry = "Select count(*) From ItemCategory Where Description='" & Dgl1.Item(Col1Value, rowDescription).Value & "' And Code<>'" & mInternalCode & "' And " & AgTemplate.ClsMain.RetDivFilterStr & "  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If

        If AgL.XNull(Dgl1.Item(Col1Value, rowHSN).Value) <> "" Then
            If Len(Dgl1.Item(Col1Value, rowHSN).Value) < 2 Then
                MsgBox("HSN Code and not be less than 2 characters.")
                passed = False
                Dgl1.CurrentCell = Dgl1.Item(Col1Value, rowHSN)
                Dgl1.Focus()
            End If
        End If
    End Sub

    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        AgL.PubFindQry = "SELECT I.Code, I.Description, T.Name AS ItemType  " &
                        " FROM ItemCategory I " &
                        " Left Join ItemType T On I.ItemType = T.Code "
        AgL.PubFindQryOrdBy = "[Description]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Item"
        'LogTableName = "ItemCategory_Log"
        'MainLineTableCsv = "ItemBuyer"
        'LogLineTableCsv = "ItemBuyer_Log"
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer


        mQry = "UPDATE Item
                Set 
                Description = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDescription).Value) & ", 
                V_Type = " & AgL.Chk_Text("IC") & ", 
                IsSystemDefine = " & Val(IIf(ChkIsSystemDefine.Checked, 1, 0)) & ", 
                ItemType = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowItemType).Tag) & ", 
                SalesTaxPostingGroup = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowSalesTaxGroup).Tag) & ", 
                HSN = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowHSN).Value) & ", 
                Department = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDepartment).Tag) & ", 
                Parent = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowParent).Tag) & ", 
                SalesRepresentativeCommissionPer = " & Val(Dgl1.Item(Col1Value, rowSalesRepresentativeCommissionPer).Value) & ",
                BarcodeType = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowBarcodeType).Tag) & ", 
                BarcodePattern = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowBarcodePattern).Tag) & ", 
                Addition = " & Val(Dgl1.Item(Col1Value, rowAddition).Value) & ", 
                LossQty = " & Val(Dgl1.Item(Col1Value, rowLossQty).Value) & ", 
                LossQtyPer = " & Val(Dgl1.Item(Col1Value, rowLossQtyPer).Value) & ", 
                LossDealQty = " & Val(Dgl1.Item(Col1Value, rowLossDealQty).Value) & ", 
                LossDealQtyPer = " & Val(Dgl1.Item(Col1Value, rowLossDealQtyPer).Value) & ", 
                ShowDimensionDetailInPurchase = " & IIf(Dgl1.Item(Col1Value, rowIsShowDimensionDetailInPurchase).Value = "Yes", 1, 0) & ", 
                ShowDimensionDetailInSales = " & IIf(Dgl1.Item(Col1Value, rowIsShowDimensionDetailInSale).Value = "Yes", 1, 0) & ", 
                IsLotApplicable = " & IIf(Dgl1.Item(Col1Value, rowIsLotApplicable).Value = "Yes", 1, 0) & ", 
                IsStockInPcsApplicable = " & IIf(Dgl1.Item(Col1Value, rowIsStockInPcsApplicable).Value = "Yes", 1, 0) & ", 
                IsNewItemAllowedPurch = " & IIf(Dgl1.Item(Col1Value, rowIsNewItemAllowedPurch).Value = "Yes", 1, 0) & ", 
                IsNewDimension1AllowedPurch = " & IIf(Dgl1.Item(Col1Value, rowIsNewDimension1AllowedPurch).Value = "Yes", 1, 0) & ", 
                IsNewDimension2AllowedPurch = " & IIf(Dgl1.Item(Col1Value, rowIsNewDimension2AllowedPurch).Value = "Yes", 1, 0) & ", 
                IsNewDimension3AllowedPurch = " & IIf(Dgl1.Item(Col1Value, rowIsNewDimension3AllowedPurch).Value = "Yes", 1, 0) & ", 
                IsNewDimension4AllowedPurch = " & IIf(Dgl1.Item(Col1Value, rowIsNewDimension4AllowedPurch).Value = "Yes", 1, 0) & ", 
                Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowUnit).Tag) & ", 
                StockUnit = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowStockUnit).Tag) & ",
                DealUnit = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDealUnit).Tag) & ",
                OMSId = Null 
                Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


        mQry = " UPDATE Item Set ItemType = '" & Dgl1.Item(Col1Value, rowItemType).Tag & "' Where ItemCategory = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "Delete from ItemCategorySalesTax where Code = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        For I = 0 To DGL2.Rows.Count - 1
            If DGL2.Item(Col1SalesTaxGroup, I).Value <> "" And Val(DGL2.Item(Col1RateGreaterThan, I).Value) > 0 Then
                mQry = " Insert Into ItemCategorySalesTax (Code,WEF, RateGreaterThan, SalesTaxGroupItem) " &
                       " Values ('" & SearchCode & "', " & AgL.Chk_Date(DGL2.Item(Col1WEF, I).Value) & ", " & Val(DGL2.Item(Col1RateGreaterThan, I).Value) & ", " & AgL.Chk_Text(DGL2.Item(Col1SalesTaxGroup, I).Value) & " )"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
        Next

        If PubDtSaleInvoiceItemHelp IsNot Nothing Then PubDtSaleInvoiceItemHelp = Nothing
    End Sub
    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = "Select H.*, D.Description as DepartmentName, P.Description As ParentName, It.Name As ItemTypeName
                 From ItemCategory H 
                 Left Join Department D On H.Department = D.Code
                 LEFT JOIN ItemCategory P On H.Parent = P.Code
                LEFT JOIN ItemType It ON H.ItemType = It.Code
                 Where H.Code='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(DsTemp.Tables(0).Rows(0)("Code"))
                Dgl1.Item(Col1Value, rowDescription).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("Description"))
                Dgl1.Item(Col1Value, rowItemType).Tag = AgL.XNull(DsTemp.Tables(0).Rows(0)("ItemType"))
                Dgl1.Item(Col1Value, rowItemType).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("ItemTypeName"))
                FGetItemTypeSetting()
                Dgl1.Item(Col1Value, rowSalesTaxGroup).Tag = AgL.XNull(DsTemp.Tables(0).Rows(0)("SalesTaxGroup"))
                Dgl1.Item(Col1Value, rowSalesTaxGroup).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("SalesTaxGroup"))
                Dgl1.Item(Col1Value, rowUnit).Tag = AgL.XNull(DsTemp.Tables(0).Rows(0)("Unit"))
                Dgl1.Item(Col1Value, rowUnit).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("Unit"))
                Dgl1.Item(Col1Value, rowStockUnit).Tag = AgL.XNull(DsTemp.Tables(0).Rows(0)("StockUnit"))
                Dgl1.Item(Col1Value, rowStockUnit).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("StockUnit"))
                Dgl1.Item(Col1Value, rowDealUnit).Tag = AgL.XNull(DsTemp.Tables(0).Rows(0)("DealUnit"))
                Dgl1.Item(Col1Value, rowDealUnit).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("DealUnit"))
                Dgl1.Item(Col1Value, rowDepartment).Tag = AgL.XNull(DsTemp.Tables(0).Rows(0)("Department"))
                Dgl1.Item(Col1Value, rowDepartment).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("DepartmentName"))
                Dgl1.Item(Col1Value, rowParent).Tag = AgL.XNull(DsTemp.Tables(0).Rows(0)("Parent"))
                Dgl1.Item(Col1Value, rowParent).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("ParentName"))
                Dgl1.Item(Col1Value, rowSalesRepresentativeCommissionPer).Value = AgL.VNull(DsTemp.Tables(0).Rows(0)("SalesRepresentativeCommissionPer"))

                Dgl1.Item(Col1Value, rowBarcodeType).Tag = AgL.XNull(.Rows(0)("BarcodeType"))
                Dgl1.Item(Col1Value, rowBarcodeType).Value = AgL.XNull(.Rows(0)("BarcodeType"))
                Dgl1.Item(Col1Value, rowBarcodePattern).Tag = AgL.XNull(.Rows(0)("BarcodePattern"))
                Dgl1.Item(Col1Value, rowBarcodePattern).Value = AgL.XNull(.Rows(0)("BarcodePattern"))


                Dgl1.Item(Col1Value, rowHSN).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("HSN"))

                Dgl1.Item(Col1Value, rowAddition).Value = AgL.VNull(DsTemp.Tables(0).Rows(0)("Addition"))
                Dgl1.Item(Col1Value, rowLossQty).Value = AgL.VNull(DsTemp.Tables(0).Rows(0)("LossQty"))
                Dgl1.Item(Col1Value, rowLossQtyPer).Value = AgL.VNull(DsTemp.Tables(0).Rows(0)("LossQtyPer"))
                Dgl1.Item(Col1Value, rowLossDealQty).Value = AgL.VNull(DsTemp.Tables(0).Rows(0)("LossDealQty"))
                Dgl1.Item(Col1Value, rowLossDealQtyPer).Value = AgL.VNull(DsTemp.Tables(0).Rows(0)("LossDealQtyPer"))
                Dgl1.Item(Col1Value, rowIsShowDimensionDetailInPurchase).Value = IIf(AgL.VNull(DsTemp.Tables(0).Rows(0)("ShowDimensionDetailInPurchase")) <> 0, "Yes", "No")
                Dgl1.Item(Col1Value, rowIsShowDimensionDetailInSale).Value = IIf(AgL.VNull(DsTemp.Tables(0).Rows(0)("ShowDimensionDetailInSales")) <> 0, "Yes", "No")
                Dgl1.Item(Col1Value, rowIsLotApplicable).Value = IIf(AgL.VNull(DsTemp.Tables(0).Rows(0)("IsLotApplicable")) <> 0, "Yes", "No")
                Dgl1.Item(Col1Value, rowIsStockInPcsApplicable).Value = IIf(AgL.VNull(DsTemp.Tables(0).Rows(0)("IsStockInPcsApplicable")) <> 0, "Yes", "No")


                Dgl1.Item(Col1Value, rowIsNewItemAllowedPurch).Value = IIf(AgL.VNull(DsTemp.Tables(0).Rows(0)("IsNewItemAllowedPurch")) <> 0, "Yes", "No")
                Dgl1.Item(Col1Value, rowIsNewDimension1AllowedPurch).Value = IIf(AgL.VNull(DsTemp.Tables(0).Rows(0)("IsNewDimension1AllowedPurch")) <> 0, "Yes", "No")
                Dgl1.Item(Col1Value, rowIsNewDimension2AllowedPurch).Value = IIf(AgL.VNull(DsTemp.Tables(0).Rows(0)("IsNewDimension2AllowedPurch")) <> 0, "Yes", "No")
                Dgl1.Item(Col1Value, rowIsNewDimension3AllowedPurch).Value = IIf(AgL.VNull(DsTemp.Tables(0).Rows(0)("IsNewDimension3AllowedPurch")) <> 0, "Yes", "No")
                Dgl1.Item(Col1Value, rowIsNewDimension4AllowedPurch).Value = IIf(AgL.VNull(DsTemp.Tables(0).Rows(0)("IsNewDimension4AllowedPurch")) <> 0, "Yes", "No")

                ChkIsSystemDefine.Checked = AgL.VNull(DsTemp.Tables(0).Rows(0)("IsSystemDefine"))
                LblIsSystemDefine.Text = IIf(AgL.VNull(DsTemp.Tables(0).Rows(0)("IsSystemDefine")) = 0, "User Define", "System Define")
                ChkIsSystemDefine.Enabled = False
            End If
        End With


        Dim I As Integer
        mQry = " Select  H.Code, H.WEF, H.RateGreaterThan, H.SalesTaxGroupItem 
                        From ItemCategorySalesTax H 
                        Where H.Code='" & SearchCode & "' 
                        Order By H.WEF, H.RateGreaterThan "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            DGL2.RowCount = 1
            DGL2.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    DGL2.Rows.Add()
                    DGL2.Item(ColSNo, I).Value = DGL2.Rows.Count - 1
                    DGL2.Item(Col1WEF, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("WEF")))
                    DGL2.Item(Col1RateGreaterThan, I).Value = Format(AgL.VNull(.Rows(I)("RateGreaterThan")), "0.00")
                    DGL2.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                    DGL2.Item(Col1SalesTaxGroup, I).Value = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                Next I
                DGL2.Visible = True
            Else
                DGL2.Visible = False
            End If
        End With

        FrmItemCategory_Grid_BaseFunction_DispText()
    End Sub

    Private Function FGetRelationalData() As Boolean
        Try
            mQry = " Select Count(*) From Item Where ItemCategory = '" & mSearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                MsgBox(" Data Exists For ItemCategory " & Dgl1.Item(Col1Value, rowDescription).Value & " In Item Master . Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If

        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData")
            FGetRelationalData = True
        End Try
    End Function


    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        Dgl1.CurrentCell = Dgl1.Item(Col1Value, rowDescription)
        Dgl1.Focus()
    End Sub

    Private Sub TxtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                Topctrl1.FButtonClick(13)
            End If
        End If
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
        mQry = "Select I.Code As SearchCode " &
            " From ItemCategory I " &
            " Order By I.Description "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmItemCategory_Grid_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 360, 885)
        FManageSystemDefine()
    End Sub

    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        Passed = FRestrictSystemDefine()

        If ClsMain.IsEntryLockedWithLockText("Item", "Code", mSearchCode) = True Then
            Passed = False
            Exit Sub
        End If

        FGetItemTypeSetting()
    End Sub

    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        Passed = FRestrictSystemDefine()
        If Passed = False Then Exit Sub
        Passed = Not FGetRelationalData()

        If ClsMain.IsEntryLockedWithLockText("Item", "Code", mSearchCode) = True Then
            Passed = False
            Exit Sub
        End If
    End Sub
    Private Sub ChkIsSystemDefine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkIsSystemDefine.Click
        FManageSystemDefine()
    End Sub

    Private Sub FManageSystemDefine()
        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
            ChkIsSystemDefine.Visible = True
            ChkIsSystemDefine.Enabled = True
        Else
            ChkIsSystemDefine.Visible = False
            ChkIsSystemDefine.Enabled = False
        End If

        If ChkIsSystemDefine.Checked Then
            LblIsSystemDefine.Text = "System Define"
        Else
            LblIsSystemDefine.Text = "User Define"
        End If
    End Sub

    Private Function FRestrictSystemDefine() As Boolean
        If ChkIsSystemDefine.Checked = True Then
            If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
                If MsgBox("This is a System Define Item.Do You Want To Proceed...?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Topctrl1.FButtonClick(14, True)
                    FRestrictSystemDefine = False
                    Exit Function
                End If
            Else
                MsgBox("Can't Edit System Define Items...!", MsgBoxStyle.Information) : Topctrl1.FButtonClick(14, True)
                FRestrictSystemDefine = False
                Exit Function
            End If
        End If
        FManageSystemDefine()
        FRestrictSystemDefine = True
    End Function

    Private Sub FrmItemCategory_Grid_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        Dim DtTemp As DataTable
        Try

            ChkIsSystemDefine.Checked = False
            FManageSystemDefine()

            Dgl1.Item(Col1Value, rowItemType).Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_ItemType"))
            If Dgl1.Item(Col1Value, rowItemType).Tag <> "" Then
                DtTemp = AgL.FillData("Select Name From ItemType Where Code = '" & Dgl1.Item(Col1Value, rowItemType).Tag & "'", AgL.GCn).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    Dgl1.Item(Col1Value, rowItemType).Value = AgL.XNull(DtTemp.Rows(0)("Name"))
                    FGetItemTypeSetting()
                Else
                    MsgBox("Invalid data in Default_ItemType of Enviromentment Settings")
                End If
            End If


            Dgl1.Item(Col1Value, rowUnit).Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_Unit"))
            If Dgl1.Item(Col1Value, rowUnit).Tag <> "" Then
                DtTemp = AgL.FillData("Select Code From Unit Where Code = '" & Dgl1.Item(Col1Value, rowUnit).Tag & "'", AgL.GCn).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    Dgl1.Item(Col1Value, rowUnit).Value = AgL.XNull(DtTemp.Rows(0)("Code"))
                Else
                    MsgBox("Invalid data in Default_Unit of Enviromentment Settings")
                End If
            End If

            Dgl1.Item(Col1Value, rowSalesTaxGroup).Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_SalesTaxGroupItem"))
            If Dgl1.Item(Col1Value, rowSalesTaxGroup).Tag <> "" Then
                DtTemp = AgL.FillData("Select Description From PostingGroupSalesTaxItem Where Description = '" & Dgl1.Item(Col1Value, rowSalesTaxGroup).Tag & "'", AgL.GCn).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    Dgl1.Item(Col1Value, rowSalesTaxGroup).Value = AgL.XNull(DtTemp.Rows(0)("Description"))
                Else
                    MsgBox("Invalid data in Default_SalesTaxGroupItem of Enviromentment Settings")
                End If
            End If


            Dgl1.Item(Col1Value, rowHSN).Value = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_HSN"))

            Dgl1.CurrentCell = Dgl1.FirstDisplayedCell ' Dgl1.Item(Col1Value, rowItemType)
            Dgl1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message & " [FrmItemCategory_Grid_BaseEvent_Topctrl_tbAdd]")
        End Try

    End Sub

    Private Sub FrmItemCategory_Grid_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        ChkIsSystemDefine.Enabled = False

        If DtItemTypeSetting IsNot Nothing Then
            If AgL.VNull(DtItemTypeSetting.Rows(0)("IsSalesTaxBasedOnRate")) Then
                DGL2.Visible = True
            Else
                DGL2.Visible = False
            End If
        End If
    End Sub

    Private Sub FrmItemCategory_Grid_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        DGL2.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DGL2, ColSNo, 40, 5, ColSNo, False, True, False)
            .AddAgDateColumn(DGL2, Col1WEF, 90, Col1WEF, True, False)
            .AddAgNumberColumn(DGL2, Col1RateGreaterThan, 80, 8, 2, False, Col1RateGreaterThan, True, False, True)
            .AddAgTextColumn(DGL2, Col1SalesTaxGroup, 100, 0, Col1SalesTaxGroup, True, False, False)
        End With
        AgL.AddAgDataGrid(DGL2, Pnl2)
        DGL2.EnableHeadersVisualStyles = False
        DGL2.AgSkipReadOnlyColumns = True
        DGL2.RowHeadersVisible = False
        DGL2.ColumnHeadersHeight = 48
        DGL2.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right
        AgL.GridDesign(DGL2)


        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Head, 300, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl1, Col1HeadOriginal, 180, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(Dgl1, Col1Mandatory, 12, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(Dgl1, Col1Value, 500, 255, Col1Value, True, False)
            .AddAgTextColumn(Dgl1, Col1LastValue, 300, 255, Col1LastValue, False, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.Columns(Col1Mandatory).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings 2", 5.25, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Dgl1.Columns(Col1Mandatory).DefaultCellStyle.ForeColor = Color.Red
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToAddRows = False
        Dgl1.RowHeadersVisible = False
        Dgl1.ColumnHeadersVisible = False
        Dgl1.BackgroundColor = Me.BackColor
        AgL.GridDesign(Dgl1)
        Dgl1.Anchor = AnchorStyles.Top + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom

        Dgl1.Rows.Add(26)

        Dgl1.Item(Col1Head, rowItemType).Value = hcItemType
        Dgl1.Item(Col1Head, rowDescription).Value = hcDescription
        Dgl1.Item(Col1Head, rowUnit).Value = hcUnit
        Dgl1.Item(Col1Head, rowStockUnit).Value = hcStockUnit
        Dgl1.Item(Col1Head, rowDealUnit).Value = hcDealUnit
        Dgl1.Item(Col1Head, rowSalesTaxGroup).Value = hcSalesTaxGroup
        Dgl1.Item(Col1Head, rowHSN).Value = hcHSN
        Dgl1.Item(Col1Head, rowDepartment).Value = hcDepartment
        Dgl1.Item(Col1Head, rowParent).Value = hcParent
        Dgl1.Item(Col1Head, rowSalesRepresentativeCommissionPer).Value = hcSalesRepresentativeCommissionPer
        Dgl1.Item(Col1Head, rowBarcodeType).Value = hcBarcodeType
        Dgl1.Item(Col1Head, rowBarcodePattern).Value = hcBarcodePattern
        Dgl1.Item(Col1Head, rowAddition).Value = hcAddition
        Dgl1.Item(Col1Head, rowLossQty).Value = hcLossQty
        Dgl1.Item(Col1Head, rowLossQtyPer).Value = hcLossQtyPer
        Dgl1.Item(Col1Head, rowLossDealQty).Value = hcLossDealQty
        Dgl1.Item(Col1Head, rowLossDealQtyPer).Value = hcLossDealQtyPer
        Dgl1.Item(Col1Head, rowIsLotApplicable).Value = hcIsLotApplicable
        Dgl1.Item(Col1Head, rowIsShowDimensionDetailInPurchase).Value = hcIsShowDimensionDetailInPurchase
        Dgl1.Item(Col1Head, rowIsShowDimensionDetailInSale).Value = hcIsShowDimensionDetailInSale
        Dgl1.Item(Col1Head, rowIsStockInPcsApplicable).Value = hcIsStockInPcsApplicable
        Dgl1.Item(Col1Head, rowIsNewItemAllowedPurch).Value = hcIsNewItemAllowedPurch
        Dgl1.Item(Col1Head, rowIsNewDimension1AllowedPurch).Value = hcIsNewDimension1AllowedPurch
        Dgl1.Item(Col1Head, rowIsNewDimension2AllowedPurch).Value = hcIsNewDimension2AllowedPurch
        Dgl1.Item(Col1Head, rowIsNewDimension3AllowedPurch).Value = hcIsNewDimension3AllowedPurch
        Dgl1.Item(Col1Head, rowIsNewDimension4AllowedPurch).Value = hcIsNewDimension4AllowedPurch

        For I As Integer = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1HeadOriginal, I).Value = Dgl1(Col1Head, I).Value
        Next

        Dgl1.Item(Col1Head, rowLossQty).Value = IIf(AgL.PubCaptionLossQty <> "", AgL.PubCaptionLossQty, hcLossQty)
        Dgl1.Item(Col1Head, rowLossQtyPer).Value = IIf(AgL.PubCaptionLossQty <> "", AgL.PubCaptionLossQty & " @", hcLossQty)
        Dgl1.Item(Col1Head, rowLossDealQty).Value = IIf(AgL.PubCaptionLossDealQty <> "", AgL.PubCaptionLossDealQty, hcLossDealQty)
        Dgl1.Item(Col1Head, rowLossDealQtyPer).Value = IIf(AgL.PubCaptionLossDealQty <> "", AgL.PubCaptionLossDealQty & " @", hcLossDealQty)

        AgL.FSetDimensionCaptionForVerticalGrid(Dgl1, AgL)
    End Sub
    Private Sub DGL2_EditingControl_KeyDown(sender As Object, e As KeyEventArgs) Handles DGL2.EditingControl_KeyDown
        Dim mQry As String
        Select Case DGL2.Columns(DGL2.CurrentCell.ColumnIndex).Name
            Case Col1SalesTaxGroup
                If e.KeyCode <> Keys.Enter Then
                    If DGL2.AgHelpDataSet(Col1SalesTaxGroup) Is Nothing Then
                        mQry = "select Description as Code, Description  from postinggroupsalesTaxitem Where IfNull(Active,1)=1 Order By Description"
                        DGL2.AgHelpDataSet(Col1SalesTaxGroup) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If
        End Select
    End Sub
    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, AgL.PubSiteCode, Dgl1.Item(Col1Value, rowItemType).Tag, "", "", "", "")
        FGetSettings = mValue
    End Function
    Private Sub FrmItemCategory_Grid_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dim i As Integer

        For i = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1Value, i).Value = ""
            Dgl1(Col1Value, i).Tag = ""
        Next

        Dim obj As Object
        For Each obj In Me.Controls
            If TypeOf obj Is TextBox Then
                If FGetSettings(SettingFields.DefaultTextCaseInMasters, SettingType.General) = TextCase.Upper Then
                    DirectCast(obj, TextBox).CharacterCasing = CharacterCasing.Upper
                ElseIf FGetSettings(SettingFields.DefaultTextCaseInMasters, SettingType.General) = TextCase.Lower Then
                    DirectCast(obj, TextBox).CharacterCasing = CharacterCasing.Lower
                End If
            End If
        Next
    End Sub
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub

            Select Case Dgl1.CurrentCell.RowIndex
                Case rowDescription
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "Select Code, Description As Name " &
                            " From ItemCategory " &
                            " Order By Description "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If
                    CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True

                Case rowItemType
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " Select Code, Name From ItemType "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowSalesTaxGroup
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " SELECT Description as  Code, Description as Name  FROM PostingGroupSalesTaxItem where Active=1 Order By Description"
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowUnit, rowStockUnit, rowDealUnit
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " SELECT Code, Code as Name  FROM Unit where IfNull(IsActive,1) = 1 Order By Code "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowDepartment
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Description as Name  FROM Department where Status='Active' Order By Code"
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If


                Case rowParent
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Description as Name  FROM ItemCategory where Status='Active' Order By Code"
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowIsNewItemAllowedPurch, rowIsNewDimension1AllowedPurch, rowIsNewDimension2AllowedPurch, rowIsNewDimension3AllowedPurch, rowIsNewDimension4AllowedPurch,
                     rowIsShowDimensionDetailInPurchase, rowIsShowDimensionDetailInSale, rowIsLotApplicable, rowIsStockInPcsApplicable
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "Select 'Yes' As Code, 'Yes' As Name 
                            UNION ALL 
                            Select 'No' As Code, 'No' As Name "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowBarcodeType
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " Select '" & AgLibrary.ClsMain.agConstants.BarcodeType.NA & "' As Code, '" & AgLibrary.ClsMain.agConstants.BarcodeType.NA & "' As Description
                                UNION ALL 
                                Select '" & AgLibrary.ClsMain.agConstants.BarcodeType.UniquePerPcs & "' As Code, '" & AgLibrary.ClsMain.agConstants.BarcodeType.UniquePerPcs & "' As Description 
                                UNION ALL 
                                Select '" & AgLibrary.ClsMain.agConstants.BarcodeType.Fixed & "' As Code, '" & AgLibrary.ClsMain.agConstants.BarcodeType.Fixed & "' As Description 
                                UNION ALL 
                                Select '" & AgLibrary.ClsMain.agConstants.BarcodeType.LotWise & "' As Code, '" & AgLibrary.ClsMain.agConstants.BarcodeType.LotWise & "' As Description "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowBarcodePattern
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " Select '" & AgLibrary.ClsMain.agConstants.BarcodePattern.Auto & "' As Code, '" & AgLibrary.ClsMain.agConstants.BarcodePattern.Auto & "' As Description
                                UNION ALL 
                                Select '" & AgLibrary.ClsMain.agConstants.BarcodePattern.Manual & "' As Code, '" & AgLibrary.ClsMain.agConstants.BarcodePattern.Manual & "' As Description "
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
    Private Sub Dgl1_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Dim mRow As Integer
        Dim mColumn As Integer
        mRow = Dgl1.CurrentCell.RowIndex
        mColumn = Dgl1.CurrentCell.ColumnIndex
        If mColumn = Dgl1.Columns(Col1Value).Index Then
            If Dgl1.Item(Col1Mandatory, mRow).Value <> "" Then
                If Dgl1(Col1Value, mRow).Value = "" Then
                    MsgBox(Dgl1(Col1Head, mRow).Value & " can not be blank.")
                    e.Cancel = True
                    Exit Sub
                End If
            End If

            Select Case mRow
                Case rowHSN
                    If AgL.XNull(Dgl1.Item(Col1Value, rowHSN).Value) <> "" Then
                        If Len(Dgl1.Item(Col1Value, rowHSN).Value) < 2 Then
                            MsgBox("HSN Code can not be less than 2 characters.")
                            e.Cancel = True
                        End If
                    End If

                Case rowItemType
                    FGetItemTypeSetting()
                    FrmItemCategory_Grid_BaseFunction_DispText()
            End Select
        End If
    End Sub
    Private Sub ApplyItemTypeSetting(ItemType As String)
        Me.Name = "FrmItemCategory"
        Dim mQry As String
        Dim DtTemp As DataTable
        Dim I As Integer, J As Integer
        Dim mDgl1RowCount As Integer
        Try
            For I = 0 To Dgl1.Rows.Count - 1
                Dgl1.Rows(I).Visible = False
            Next


            mQry = "Select H.*
                    from EntryHeaderUISetting H                   
                    Where EntryName='FrmItemCategory' And NCat = '" & ItemType & "' And GridName ='Dgl1' "
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
                            'MsgBox(NameOf(rowAdditionalDiscountPatternPurchase))
                        End If
                    Next
                Next
            End If
            If mDgl1RowCount = 0 Then Dgl1.Visible = False Else Dgl1.Visible = True



        Catch ex As Exception
            MsgBox(ex.Message & " [ApplySubgroupTypeSetting]")
        End Try
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
            End If


            If Dgl1.CurrentCell.ColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub


            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0
            CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = False

            Select Case Dgl1.CurrentCell.RowIndex
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmItemCategory_Grid_BaseEvent_Save_PostTrans(SearchCode As String) Handles Me.BaseEvent_Save_PostTrans
        ClsMain.FCreateItemDataTable()
    End Sub
    Private Sub LblIsSystemDefine_Click(sender As Object, e As EventArgs) Handles LblIsSystemDefine.Click
    End Sub
    Private Sub FrmItemCategory_Grid_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Dim I As Integer
        For I = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1Head, I).Tag = Nothing
        Next
    End Sub
End Class
