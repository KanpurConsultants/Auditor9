Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain.ConfigurableFields

Public Class FrmItemBOM
    Inherits AgTemplate.TempMaster

    Dim mQry$
    Dim mBOMItemType As String = "BOM"

    Public Const ColSNo As String = "SNo"
    Public WithEvents Dgl2 As New AgControls.AgDataGrid
    Public Const Col1Product As String = FrmItemBOMDetail.Product
    Public Const Col1Unit As String = FrmItemBOMDetail.Unit
    Public Const Col1Dimension1 As String = FrmItemBOMDetail.Dimension1
    Public Const Col1Dimension2 As String = FrmItemBOMDetail.Dimension2
    Public Const Col1Dimension3 As String = FrmItemBOMDetail.Dimension3
    Public Const Col1Qty As String = FrmItemBOMDetail.Qty
    Public Const Col1ConsumptionPer As String = FrmItemBOMDetail.ConsumptionPer
    Public Const Col1ConsumptionPer1 As String = FrmItemBOMDetail.ConsumptionPer1

    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1HeadOriginal As String = "Head Original"





    Dim rowProcess As Integer = 0
    Dim rowItemCategory As Integer = 1
    Dim rowItemGroup As Integer = 2
    Dim rowDimension1 As Integer = 3
    Dim rowDimension2 As Integer = 4
    Dim rowBaseItem As Integer = 5
    Dim rowBatchQty As Integer = 6
    Dim rowBatchUnit As Integer = 7
    Dim rowWastagePer As Integer = 8
    Dim rowStandardWeight As Integer = 9
    'Dim rowDefaultAdditionalDiscountPerPurchase As Integer = 8
    'Dim rowDefaultMarginPer As Integer = 9
    'Dim rowShowInOtherDivision As Integer = 10
    'Dim rowBarcodeType As Integer = 11
    'Dim rowBarcodePattern As Integer = 12
    'Dim rowDefaultSupplier As Integer = 13
    'Dim rowDepartment As Integer = 14

    Dim DtItemTypeSetting As DataTable
    Friend WithEvents Pnl1 As Panel
    Public WithEvents PnlTotals As Panel
    Public WithEvents LblDealQty As Label
    Public WithEvents LblDealQtyText As Label
    Public WithEvents LblTotalQty As Label
    Public WithEvents LblTotalQtyText As Label
    Public WithEvents LinkLabel1 As LinkLabel
    Dim mItemTypeLastValue As String

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.LblIsSystemDefine = New System.Windows.Forms.Label()
        Me.ChkIsSystemDefine = New System.Windows.Forms.CheckBox()
        Me.PnlRateType = New System.Windows.Forms.Panel()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.PnlTotals = New System.Windows.Forms.Panel()
        Me.LblDealQty = New System.Windows.Forms.Label()
        Me.LblDealQtyText = New System.Windows.Forms.Label()
        Me.LblTotalQty = New System.Windows.Forms.Label()
        Me.LblTotalQtyText = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.GrpUP.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlTotals.SuspendLayout()
        Me.SuspendLayout()
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(961, 41)
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 558)
        Me.GroupBox1.Size = New System.Drawing.Size(1003, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(14, 562)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(200, 623)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(228, 562)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(401, 562)
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
        Me.GroupBox2.Location = New System.Drawing.Point(704, 562)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(465, 562)
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
        'LblIsSystemDefine
        '
        Me.LblIsSystemDefine.AutoSize = True
        Me.LblIsSystemDefine.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIsSystemDefine.ForeColor = System.Drawing.Color.Red
        Me.LblIsSystemDefine.Location = New System.Drawing.Point(827, 536)
        Me.LblIsSystemDefine.Name = "LblIsSystemDefine"
        Me.LblIsSystemDefine.Size = New System.Drawing.Size(96, 15)
        Me.LblIsSystemDefine.TabIndex = 1061
        Me.LblIsSystemDefine.Text = "IsSystemDefine"
        '
        'ChkIsSystemDefine
        '
        Me.ChkIsSystemDefine.AutoSize = True
        Me.ChkIsSystemDefine.BackColor = System.Drawing.Color.Transparent
        Me.ChkIsSystemDefine.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkIsSystemDefine.ForeColor = System.Drawing.Color.Red
        Me.ChkIsSystemDefine.Location = New System.Drawing.Point(748, 535)
        Me.ChkIsSystemDefine.Name = "ChkIsSystemDefine"
        Me.ChkIsSystemDefine.Size = New System.Drawing.Size(15, 14)
        Me.ChkIsSystemDefine.TabIndex = 1060
        Me.ChkIsSystemDefine.UseVisualStyleBackColor = False
        '
        'PnlRateType
        '
        Me.PnlRateType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlRateType.Location = New System.Drawing.Point(21, 301)
        Me.PnlRateType.Name = "PnlRateType"
        Me.PnlRateType.Size = New System.Drawing.Size(924, 225)
        Me.PnlRateType.TabIndex = 2
        '
        'Pnl1
        '
        Me.Pnl1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl1.Location = New System.Drawing.Point(21, 50)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(924, 226)
        Me.Pnl1.TabIndex = 1
        '
        'PnlTotals
        '
        Me.PnlTotals.BackColor = System.Drawing.Color.Cornsilk
        Me.PnlTotals.Controls.Add(Me.LblDealQty)
        Me.PnlTotals.Controls.Add(Me.LblDealQtyText)
        Me.PnlTotals.Controls.Add(Me.LblTotalQty)
        Me.PnlTotals.Controls.Add(Me.LblTotalQtyText)
        Me.PnlTotals.Location = New System.Drawing.Point(21, 532)
        Me.PnlTotals.Name = "PnlTotals"
        Me.PnlTotals.Size = New System.Drawing.Size(902, 23)
        Me.PnlTotals.TabIndex = 1062
        '
        'LblDealQty
        '
        Me.LblDealQty.AutoSize = True
        Me.LblDealQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDealQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblDealQty.Location = New System.Drawing.Point(551, 3)
        Me.LblDealQty.Name = "LblDealQty"
        Me.LblDealQty.Size = New System.Drawing.Size(12, 16)
        Me.LblDealQty.TabIndex = 666
        Me.LblDealQty.Text = "."
        Me.LblDealQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblDealQtyText
        '
        Me.LblDealQtyText.AutoSize = True
        Me.LblDealQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDealQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblDealQtyText.Location = New System.Drawing.Point(444, 3)
        Me.LblDealQtyText.Name = "LblDealQtyText"
        Me.LblDealQtyText.Size = New System.Drawing.Size(61, 16)
        Me.LblDealQtyText.TabIndex = 665
        Me.LblDealQtyText.Text = "Total % :"
        '
        'LblTotalQty
        '
        Me.LblTotalQty.AutoSize = True
        Me.LblTotalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalQty.Location = New System.Drawing.Point(332, 3)
        Me.LblTotalQty.Name = "LblTotalQty"
        Me.LblTotalQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalQty.TabIndex = 660
        Me.LblTotalQty.Text = "."
        Me.LblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalQtyText
        '
        Me.LblTotalQtyText.AutoSize = True
        Me.LblTotalQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalQtyText.Location = New System.Drawing.Point(247, 3)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(72, 16)
        Me.LblTotalQtyText.TabIndex = 659
        Me.LblTotalQtyText.Text = "Total Qty :"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(23, 279)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(147, 19)
        Me.LinkLabel1.TabIndex = 1063
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Consumption Detail"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FrmItemBOM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(961, 606)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.PnlTotals)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.PnlRateType)
        Me.Controls.Add(Me.LblIsSystemDefine)
        Me.Controls.Add(Me.ChkIsSystemDefine)
        Me.Name = "FrmItemBOM"
        Me.Text = "Quality Master"
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
        Me.Controls.SetChildIndex(Me.PnlRateType, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.PnlTotals, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
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
        Me.PnlTotals.ResumeLayout(False)
        Me.PnlTotals.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents LblIsSystemDefine As System.Windows.Forms.Label
    Friend WithEvents ChkIsSystemDefine As System.Windows.Forms.CheckBox
    Public WithEvents PnlRateType As Panel
#End Region

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer

        If Dgl1.Item(Col1Value, rowProcess).Value = "" Then Err.Raise(1, , "Process Is Required!")
        If Dgl1.Item(Col1Value, rowItemGroup).Value = "" Then Err.Raise(1, , "ItemGroup Is Required!")
        If Dgl1.Item(Col1Value, rowDimension1).Value = "" Then Err.Raise(1, , "Dimension1 Is Required!")
        If Dgl1.Item(Col1Value, rowDimension2).Value = "" Then Err.Raise(1, , "Dimension2 Is Required!")

        If Val(LblDealQty.Text) <> 100 Then Err.Raise(1, , "Consumption should be 100% ")

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From Item Where Description='" & Dgl1.Item(Col1Value, rowBaseItem).Value & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        Else
            mQry = "Select count(*) From Item Where Description='" & Dgl1.Item(Col1Value, rowBaseItem).Value & "' And Code<>'" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If


        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Value, I).Value = Nothing Then Dgl1.Item(Col1Value, I).Value = ""
            If Dgl1.Item(Col1Value, I).Tag = Nothing Then Dgl1.Item(Col1Value, I).Tag = ""
        Next
    End Sub

    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        AgL.PubFindQry = "SELECT H.Code, Max(H.Description) AS Description, Max(P.NCat)  AS Process,  Max(D1.Description)  Dimension1, Max(D2.Description)  Dimension2, Max(BD.BatchQty) BatchQty, Max(BD.BatchUnit) BatchUnit, Max(BD.WastagePer) WastagePer  
                            FROM Item H
                            LEFT JOIN BOMDetail BD ON BD.BaseItem = H.Code 
                            LEFT JOIN Process P ON P.NCat = BD.Process 
                            LEFT JOIN Dimension1 D1 ON D1.Code = BD.Dimension1  
                            LEFT JOIN Dimension2 D2 ON D2.Code = BD.Dimension2
                            WHERE H.ItemType ='" & mBOMItemType & "'
                            GROUP BY H.Code "
        AgL.PubFindQryOrdBy = "[Description]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Item"
        MainLineTableCsv = "BOMDetail"
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer

        mQry = "UPDATE Item 
                Set 
                Description = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowBaseItem).Value) & ",
                ItemGroup = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowItemGroup).Tag) & ",
                Dimension1 = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDimension1).Tag) & ",
                Dimension2 = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDimension2).Tag) & ",
                ItemType = '" & mBOMItemType & "'                 
                Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "DELETE FROM BOMDetail WHERE BaseItem  = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        For I = 0 To Dgl2.Rows.Count - 1
            If Dgl2.Item(Col1Product, I).Value <> "" And Val(Dgl2.Item(Col1Qty, I).Value) > 0 Then
                mQry = "INSERT INTO BOMDetail (Code, Sr, Process, Item, Qty, ConsumptionPer,  Unit, WastagePer, BaseItem, BatchQty, BatchUnit, Dimension1, Dimension2, Dimension3)
                        VALUES ('" & SearchCode & "', " & I + 1 & ", " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowProcess).Tag) & ", " & AgL.Chk_Text(Dgl2.Item(Col1Product, I).Tag) & ", 
                        " & Val(Dgl2.Item(Col1Qty, I).Value) & ", " & Val(Dgl2.Item(Col1ConsumptionPer, I).Value) & ", " & AgL.Chk_Text(Dgl2.Item(Col1Unit, I).Tag) & ", 
                        " & Val(Dgl1.Item(Col1Value, rowWastagePer).Value) & ", '" & SearchCode & "' , " & Val(Dgl1.Item(Col1Value, rowBatchQty).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowBatchUnit).Tag) & ",  " & AgL.Chk_Text(Dgl2.Item(Col1Dimension1, I).Tag) & ", " & AgL.Chk_Text(Dgl2.Item(Col1Dimension2, I).Tag) & " , " & AgL.Chk_Text(Dgl2.Item(Col1Dimension3, I).Tag) & " ) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
        Next
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList


    End Sub
    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet
        mQry = "SELECT H.Code, Max(H.Description) AS Description,  Max(BD.Process) Process, Max(P.NCat)  AS ProcessName,  Max(IG.ItemCategory)  ItemCategory, Max(IC.Description)  ItemCategoryName, Max(H.ItemGroup)  ItemGroup, Max(IG.Description)  ItemGroupName,  Max(H.Dimension1)  Dimension1, Max(D1.Description)  Dimension1Name, Max(H.Dimension2)  Dimension2, Max(D2.Description)  Dimension2Name, 
                Max(BD.BatchQty) BatchQty, Max(BD.BatchUnit) BatchUnit, Max(BD.WastagePer) WastagePer , Sum(BD.Qty) TotalQty 
                FROM Item H
                LEFT JOIN BOMDetail BD ON BD.BaseItem = H.Code 
                LEFT JOIN Process P ON P.NCat = BD.Process 
                LEFT JOIN ItemGroup IG ON IG.Code = H.ItemGroup 
                LEFT JOIN ItemCategory IC ON IC.Code = IG.ItemCategory
                LEFT JOIN Dimension1 D1 ON D1.Code = H.Dimension1  
                LEFT JOIN Dimension2 D2 ON D2.Code = H.Dimension2
                WHERE H.Code ='" & SearchCode & "'
                GROUP BY H.Code"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))


                Dgl1.Item(Col1Value, rowProcess).Tag = AgL.XNull(.Rows(0)("Process"))
                Dgl1.Item(Col1Value, rowProcess).Value = AgL.XNull(.Rows(0)("ProcessName"))
                Dgl1.Item(Col1Value, rowBaseItem).Value = AgL.XNull(.Rows(0)("Description"))
                Dgl1.Item(Col1Value, rowBaseItem).Tag = AgL.XNull(.Rows(0)("Code"))
                Dgl1.Item(Col1Value, rowItemGroup).Tag = AgL.XNull(.Rows(0)("ItemGroup"))
                Dgl1.Item(Col1Value, rowItemGroup).Value = AgL.XNull(.Rows(0)("ItemCategoryName"))
                Dgl1.Item(Col1Value, rowItemCategory).Tag = AgL.XNull(.Rows(0)("ItemCategory"))
                Dgl1.Item(Col1Value, rowItemCategory).Value = AgL.XNull(.Rows(0)("ItemGroupName"))
                Dgl1.Item(Col1Value, rowDimension1).Tag = AgL.XNull(.Rows(0)("Dimension1"))
                Dgl1.Item(Col1Value, rowDimension1).Value = AgL.XNull(.Rows(0)("Dimension1Name"))
                Dgl1.Item(Col1Value, rowDimension2).Tag = AgL.XNull(.Rows(0)("Dimension2"))
                Dgl1.Item(Col1Value, rowDimension2).Value = AgL.XNull(.Rows(0)("Dimension2Name"))
                Dgl1.Item(Col1Value, rowBatchQty).Value = AgL.VNull(.Rows(0)("BatchQty"))
                Dgl1.Item(Col1Value, rowBatchUnit).Value = AgL.XNull(.Rows(0)("BatchUnit"))
                Dgl1.Item(Col1Value, rowWastagePer).Value = AgL.VNull(.Rows(0)("WastagePer"))
                Dgl1.Item(Col1Value, rowStandardWeight).Value = AgL.VNull(.Rows(0)("TotalQty"))

                LblTotalQty.Text = Dgl1.Item(Col1Value, rowStandardWeight).Value
                LblDealQty.Text = 100

                FGetItemTypeSetting()


                'ChkIsSystemDefine.Checked = AgL.VNull(.Rows(0)("IsSystemDefine"))
                'LblIsSystemDefine.Text = IIf(AgL.VNull(.Rows(0)("IsSystemDefine")) = 0, "User Define", "System Define")
                ChkIsSystemDefine.Enabled = False
            End If
        End With


        Dim I As Integer
        mQry = "SELECT H.Code, H.Sr, H.Item, I.Description AS ItemName,  H.Dimension1, D1.Description AS Dimension1Name, H.Dimension2, D2.Description AS Dimension2Name, H.Dimension3, D3.Description AS Dimension3Name, H.Unit, H.Qty, H.ConsumptionPer  
                FROM BOMDetail H
                LEFT JOIN Item I ON I.Code = H.Item 
                LEFT JOIN Dimension1 D1 ON D1.Code = H.Dimension1  
                LEFT JOIN Dimension2 D2 ON D2.Code = H.Dimension2
                LEFT JOIN Dimension3 D3 ON D3.Code = H.Dimension3
                WHERE BaseItem ='" & SearchCode & "'
                ORDER BY H.Sr "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            Dgl2.RowCount = 1
            Dgl2.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl2.Rows.Add()
                    Dgl2.Item(ColSNo, I).Value = Dgl2.Rows.Count - 1
                    Dgl2.Item(Col1Product, I).Tag = AgL.XNull(.Rows(I)("Item"))
                    Dgl2.Item(Col1Product, I).Value = AgL.XNull(.Rows(I)("ItemName"))
                    Dgl2.Item(Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                    Dgl2.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Name"))
                    Dgl2.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                    Dgl2.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Name"))
                    Dgl2.Item(Col1Dimension3, I).Tag = AgL.XNull(.Rows(I)("Dimension3"))
                    Dgl2.Item(Col1Dimension3, I).Value = AgL.XNull(.Rows(I)("Dimension3Name"))
                    Dgl2.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                    Dgl2.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.00")
                    Dgl2.Item(Col1ConsumptionPer, I).Value = Format(AgL.VNull(.Rows(I)("ConsumptionPer")), "0.00")
                Next I
                Dgl2.Visible = True
            Else
                Dgl2.Visible = False
            End If
        End With
        FrmItemBOM_BaseFunction_DispText()
    End Sub

    'Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
    '    TxtDescription.Focus()
    'End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        Dgl1.CurrentCell = Dgl1(Col1Value, rowDimension2)
        Dgl1.Focus()
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
        mQry = "SELECT I.Code AS SearchCode FROM Item I  WHERE I.ItemType =  '" & mBOMItemType & "'" &
                " Order By I.Code "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmItemBOM_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 325, 885)
        FManageSystemDefine()
    End Sub

    Private Sub TxtItemCategory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
        End If
    End Sub

    Private Sub Dgl2_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl2.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl2.CurrentCell.RowIndex
            mColumnIndex = Dgl2.CurrentCell.ColumnIndex
            If Dgl2.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl2.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl2.Columns(Dgl2.CurrentCell.ColumnIndex).Name

                Case Col1Product
                    Validating_ItemCode(Dgl2.Item(mColumnIndex, mRowIndex).Tag, mColumnIndex, mRowIndex)


            End Select
            Call Calculation()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Validating_ItemCode(ItemCode As String, ByVal mColumn As Integer, ByVal mRow As Integer)
        Dim DtItem As DataTable = Nothing
        Try

            mQry = "Select I.Code, I.Description, I.ManualCode, I.Unit 
                                From Item I  With (NoLock)
                                Where I.Code ='" & ItemCode & "'"
            DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtItem.Rows.Count > 0 Then
                Dgl2.Item(Col1Unit, mRow).Value = AgL.XNull(DtItem.Rows(0)("Unit"))
                Dgl2.Item(Col1Unit, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Unit"))
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        Passed = FRestrictSystemDefine()


        FGetItemTypeSetting()
    End Sub

    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        Passed = FRestrictSystemDefine()
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

    Private Sub FrmItemBOM_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        Dim DsTemp As DataSet
        ChkIsSystemDefine.Checked = False
        FManageSystemDefine()


        FGetItemTypeSetting()


    End Sub

    Private Sub FrmItemBOM_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dim I As Integer
        Dgl2.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl2, ColSNo, 40, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl2, Col1Product, 300, 0, Col1Product, True, False, False)
            .AddAgTextColumn(Dgl2, Col1Unit, 50, 0, Col1Unit, True, False, False)
            .AddAgTextColumn(Dgl2, Col1Dimension1, 100, 0, Col1Dimension1, True, False, False)
            .AddAgTextColumn(Dgl2, Col1Dimension2, 100, 0, Col1Dimension2, True, False, False)
            .AddAgTextColumn(Dgl2, Col1Dimension3, 100, 0, Col1Dimension3, True, False, False)
            .AddAgNumberColumn(Dgl2, Col1Qty, 70, 3, 3, False, Col1Qty, True, False, True)
            .AddAgNumberColumn(Dgl2, Col1ConsumptionPer, 80, 2, 3, False, "%", True, False, True)
            .AddAgNumberColumn(Dgl2, Col1ConsumptionPer1, 80, 2, 3, False, Col1ConsumptionPer1, False, False, True)
        End With
        AgL.AddAgDataGrid(Dgl2, PnlRateType)
        Dgl2.EnableHeadersVisualStyles = False
        Dgl2.AgSkipReadOnlyColumns = True
        Dgl2.RowHeadersVisible = False
        'Dgl2.AllowUserToAddRows = False
        AgL.GridDesign(Dgl2)



        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Head, 200, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl1, Col1HeadOriginal, 180, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(Dgl1, Col1Mandatory, 12, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(Dgl1, Col1Value, 580, 255, Col1Value, True, False)
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
        AgL.GridDesign(Dgl1)


        Dgl1.Rows.Add(15)
        For I = 0 To Dgl1.Rows.Count - 1
            Dgl1.Rows(I).Visible = False
        Next

        Dgl1.Item(Col1Head, rowProcess).Value = FrmItemBOMHeaderDgl1.Process
        Dgl1.Item(Col1Head, rowItemCategory).Value = FrmItemBOMHeaderDgl1.ItemCategory
        Dgl1.Item(Col1Head, rowItemGroup).Value = FrmItemBOMHeaderDgl1.ItemGroup
        Dgl1.Item(Col1Head, rowDimension1).Value = FrmItemBOMHeaderDgl1.Dimension1
        Dgl1.Item(Col1Head, rowDimension2).Value = FrmItemBOMHeaderDgl1.Dimension2
        Dgl1.Item(Col1Head, rowBaseItem).Value = FrmItemBOMHeaderDgl1.BaseItem
        Dgl1.Item(Col1Head, rowBatchQty).Value = FrmItemBOMHeaderDgl1.BatchQty
        Dgl1.Item(Col1Head, rowBatchUnit).Value = FrmItemBOMHeaderDgl1.BatchUnit
        Dgl1.Item(Col1Head, rowWastagePer).Value = FrmItemBOMHeaderDgl1.WastagePer
        Dgl1.Item(Col1Head, rowStandardWeight).Value = FrmItemBOMHeaderDgl1.StandardWeight


    End Sub

    Private Sub Calculation()
        Dim I As Integer
        If Topctrl1.Mode = "Browse" Then Exit Sub




        LblTotalQty.Text = 0
        LblDealQty.Text = 0



        For I = 0 To Dgl2.RowCount - 1
            If Dgl2.Item(Col1Product, I).Value <> "" And Dgl2.Rows(I).Visible Then



                'Footer Calculation
                Dim bQty As Double = 0
                Dim bPer As Double = 0


                If Dgl2.Item(Col1ConsumptionPer, I).Value = 0 Then
                    Dgl2.Item(Col1ConsumptionPer1, I).Value = Math.Round(Val(Dgl2.Item(Col1Qty, I).Value) / Val(Dgl1.Item(Col1Value, rowStandardWeight).Value) * 100, 2)
                Else
                    Dgl2.Item(Col1ConsumptionPer1, I).Value = Dgl2.Item(Col1ConsumptionPer, I).Value
                    Dgl2.Item(Col1Qty, I).Value = Dgl2.Item(Col1ConsumptionPer, I).Value * Val(Dgl1.Item(Col1Value, rowStandardWeight).Value) / 100
                End If

                bQty = Val(Dgl2.Item(Col1Qty, I).Value)
                bPer = Val(Dgl2.Item(Col1ConsumptionPer1, I).Value)

                LblTotalQty.Text = Val(LblTotalQty.Text) + bQty
                LblDealQty.Text = Val(LblDealQty.Text) + bPer

            End If
        Next
    End Sub


    Private Sub DglRateType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl2.KeyDown
        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub

        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If

        If e.Control Or e.Shift Or e.Alt Then Exit Sub

        If e.KeyCode = Keys.Enter Then
            'If Dgl2.CurrentCell.ColumnIndex = Dgl2.Columns(Col1Margin).Index Then
            '    If Dgl2.Item(Dgl2.CurrentCell.ColumnIndex, Dgl2.CurrentCell.RowIndex).Value Is Nothing Then Dgl2.Item(Dgl2.CurrentCell.ColumnIndex, Dgl2.CurrentCell.RowIndex).Value = ""
            '    If Dgl2.Item(Dgl2.CurrentCell.ColumnIndex, Dgl2.CurrentCell.RowIndex).Value = "" Then
            '        If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, "Save") = MsgBoxResult.Yes Then
            '            Topctrl1.FButtonClick(13)
            '        End If
            '    End If
            'End If
        End If
    End Sub

    Private Sub FGetItemTypeSetting()


        ApplyItemTypeSetting(Dgl1(Col1Value, rowProcess).Tag)
    End Sub


    Private Sub ApplyItemTypeSetting(ItemType As String)
        Dim mQry As String
        Dim DtTemp As DataTable
        Dim I As Integer, J As Integer
        Dim mDgl1RowCount As Integer
        Dim mDglRateTypeColumnCount As Integer
        Try

            mQry = "Select H.*
                    from EntryHeaderUISetting H                   
                    Where EntryName='FrmItemBOM' And GridName ='Dgl1' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 0 To Dgl1.Rows.Count - 1
                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = Dgl1.Item(Col1Head, J).Value Then
                            Dgl1.Rows(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
                            If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDgl1RowCount += 1
                            Dgl1.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
                            If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
                                Dgl1.Item(Col1Head, J).Value = AgL.XNull(DtTemp.Rows(I)("Caption"))
                            End If
                            If AgL.VNull(DtTemp.Rows(I)("IsEditable")) = 0 Then Dgl1.Rows(J).ReadOnly = True
                            'MsgBox(NameOf(rowAdditionalDiscountPatternPurchase))
                        End If
                    Next
                Next
            End If
            If mDgl1RowCount = 0 Then Dgl1.Visible = False Else Dgl1.Visible = True



            mQry = "Select H.*
                    from EntryLineUISetting H                    
                    Where EntryName='FrmItemBOM' And GridName ='Dgl2' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 0 To Dgl2.Columns.Count - 1
                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = Dgl2.Columns(J).Name Then
                            Dgl2.Columns(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
                            If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDglRateTypeColumnCount += 1
                            If Not IsDBNull(DtTemp.Rows(I)("DisplayIndex")) Then
                                Dgl2.Columns(J).DisplayIndex = AgL.VNull(DtTemp.Rows(I)("DisplayIndex"))
                            End If
                            'Dgl1.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
                        End If
                    Next
                Next
            End If
            If mDglRateTypeColumnCount = 0 Then Dgl2.Visible = False Else Dgl2.Visible = True

        Catch ex As Exception
            MsgBox(ex.Message & " [ApplySubgroupTypeSetting]")
        End Try
    End Sub


    Private Sub FrmItemBOM_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        If DtItemTypeSetting Is Nothing Then Exit Sub
        ChkIsSystemDefine.Enabled = False
        'Dgl2.Visible = False
        If DtItemTypeSetting IsNot Nothing Then
            If DtItemTypeSetting.Rows(0)("IsItemBOMLinkedWithItemCategory") Then
                Dgl1(Col1Value, rowDimension1).ReadOnly = IIf(Topctrl1.Mode <> "Browse", True, False)
            Else
                Dgl1(Col1Value, rowDimension1).ReadOnly = False
            End If
        Else
            Dgl1(Col1Value, rowDimension1).ReadOnly = False
        End If





    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try

            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            If Topctrl1.Mode = "BROWSE" Then
                Dgl1.CurrentCell.ReadOnly = True
            End If

            If Dgl1.CurrentCell.ColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub


            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0
            CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = False

            Select Case Dgl1.CurrentCell.RowIndex
                Case rowBatchQty, rowStandardWeight
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberLeftPlaces = 2
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberRightPlaces = 2

                Case rowWastagePer
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberLeftPlaces = 3
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberRightPlaces = 3
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
            If bColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub

            Select Case Dgl1.CurrentCell.RowIndex
                Case rowProcess
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " Select Code, NCat From Process "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowItemGroup
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        If Dgl1.Item(Col1Head, rowItemCategory).Tag Is Nothing Then
                            mQry = " Select Code, Description From ItemGroup "
                        Else
                            mQry = " Select Code, Description From ItemGroup Where "
                        End If

                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowDimension1
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " Select Code, Description From Dimension1 "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If
                Case rowDimension2
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " Select Code, Description From Dimension2 "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowBatchUnit
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Code AS Description FROM Unit "
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

    Private Sub DGLRateType_EditingControl_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl2.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = Dgl2.CurrentCell.RowIndex
            bColumnIndex = Dgl2.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If Topctrl1.Mode = "Browse" Then Exit Sub


            Select Case Dgl2.Columns(Dgl2.CurrentCell.ColumnIndex).Name
                'Case Col1DiscountPattern
                '    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                '        If Dgl2.AgHelpDataSet(bColumnIndex) Is Nothing Then
                '            mQry = ClsMain.GetStringsFromClassConstants(GetType(DiscountCalculationPattern))
                '            Dgl2.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                '        End If
                '    End If
                Case Col1Product
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl2.AgHelpDataSet(Col1Product) Is Nothing Then
                            mQry = "SELECT Code, Description FROM Item"
                            Dgl2.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
                Case Col1Dimension1
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl2.AgHelpDataSet(Col1Dimension1) Is Nothing Then
                            mQry = "SELECT Code, Description FROM Dimension1"
                            Dgl2.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
                Case Col1Dimension2
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl2.AgHelpDataSet(Col1Dimension2) Is Nothing Then
                            mQry = "SELECT Code, Description FROM Dimension2"
                            Dgl2.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
                Case Col1Dimension3
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl2.AgHelpDataSet(Col1Dimension3) Is Nothing Then
                            mQry = "SELECT Code, Description FROM Dimension3"
                            Dgl2.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
                Case Col1Unit
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl2.AgHelpDataSet(Col1Unit) Is Nothing Then
                            mQry = "SELECT Code, Code as Description FROM Unit "
                            Dgl2.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
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
                Case rowProcess
                    'FGetItemTypeSetting()

                    'If DtItemTypeSetting.Rows(0)("IsItemBOMLinkedWithItemCategory") Then

                    '    Dgl1(Col1Value, rowDimension1).ReadOnly = False
                    'Else
                    '    Dgl1(Col1Value, rowDimension1).ReadOnly = True
                    '    Dgl1(Col1Value, rowDimension1).Value = ""
                    '    Dgl1(Col1Value, rowDimension1).Tag = ""
                    '    Dgl1(Col1Head, rowDimension1).Tag = Nothing
                    'End If

                Case rowProcess, rowItemGroup, rowDimension1, rowDimension2
                    Dgl1(Col1Value, rowBaseItem).Value = Dgl1(Col1Value, rowDimension1).Value & "-" & Dgl1(Col1Value, rowItemGroup).Value & "-" & Dgl1(Col1Value, rowDimension2).Value & "-" & Dgl1(Col1Value, rowProcess).Value & "-BOM"

            End Select
        End If
    End Sub

    Private Sub FrmItemBOM_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dim i As Integer

        For i = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1Value, i).Value = ""
            Dgl1(Col1Value, i).Tag = ""
        Next

        Dgl1.Item(Col1Value, rowBatchQty).Value = 1
        Dgl1.Item(Col1Value, rowBatchUnit).Value = ClsMain.UnitConstants.SqYard
        Dgl1.Item(Col1Value, rowBatchUnit).Tag = ClsMain.UnitConstants.SqYard

        Dgl2.Rows.Clear()
        Dgl2.RowCount = 1

        LblTotalQty.Text = "."
        LblDealQty.Text = "."

    End Sub
End Class
