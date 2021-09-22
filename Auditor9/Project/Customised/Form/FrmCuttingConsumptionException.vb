Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain
Imports Customised.ClsMain.ConfigurableFields

Public Class FrmCuttingConsumptionException
    Inherits AgTemplate.TempMaster

    Dim mQry$


    Public Const ColSNo As String = "SNo"
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1SKU As String = "SKU"
    Public Const Col1Process As String = "Process"
    Public Const Col1ItemCategory As String = "Item Category"
    Public Const Col1ItemGroup As String = "Item Group"
    Public Const Col1Item As String = "Item"
    Public Const Col1Dimension1 As String = "Dimension 1"
    Public Const Col1Dimension2 As String = "Dimension 2"
    Public Const Col1Dimension3 As String = "Dimension 3"
    Public Const Col1Dimension4 As String = "Dimension 4"
    Public Const Col1Size As String = "Size"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1ConsumptionPer As String = "Consumption %"
    Public Const Col1FaceConsumptionPer As String = "Face Consumption %"


    Public Const Col1MItemCategory As String = "M Item Category"
    Public Const Col1MItemGroup As String = "M Item Group"
    Public Const Col1MItemSpecification As String = "M Item Specification"
    Public Const Col1MDimension1 As String = "M Dimension 1"
    Public Const Col1MDimension2 As String = "M Dimension 2"
    Public Const Col1MDimension3 As String = "M Dimension 3"
    Public Const Col1MDimension4 As String = "M Dimension 4"
    Public Const Col1MSize As String = "M Size"






    Public WithEvents DglMain As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1HeadOriginal As String = "Head Original"



    Dim rowSKU As Integer = 0
    Dim rowProcess As Integer = 1
    Dim rowItemType As Integer = 2
    Dim rowItemCategory As Integer = 3
    Dim rowItemGroup As Integer = 4
    Dim rowDimension1 As Integer = 5
    Dim rowDimension2 As Integer = 6
    Dim rowDimension3 As Integer = 7
    Dim rowDimension4 As Integer = 8
    Dim rowSize As Integer = 9
    Dim rowSpecification As Integer = 10
    Dim rowItem As Integer = 11
    Dim rowBatchQty As Integer = 12
    Dim rowBatchUnit As Integer = 13
    Dim rowWastagePer As Integer = 14
    Dim rowWeightForPer As Integer = 15


    Public Const hcSKU As String = "SKU"
    Public Const hcProcess As String = "Process"
    Public Const hcItemType As String = "Item Type"
    Public Const hcItemCategory As String = "Item Category"
    Public Const hcItemGroup As String = "Item Group"
    Public Const hcDimension1 As String = "Dimension 1"
    Public Const hcDimension2 As String = "Dimension 2"
    Public Const hcDimension3 As String = "Dimension 3"
    Public Const hcDimension4 As String = "Dimension 4"
    Public Const hcSize As String = "Size"
    Public Const hcItem As String = "Item"
    Public Const hcBatchQty As String = "Batch Qty"
    Public Const hcBatchUnit As String = "Batch Unit"
    Public Const hcWastagePer As String = "Wastage Per"
    Public Const hcWeightForPer As String = "Weight For Per"

    Dim ExceptionTag As String = "Exception"

    Dim DtItemTypeSetting As DataTable
    Friend WithEvents PnlMain As Panel
    Public WithEvents PnlTotals As Panel
    Public WithEvents LblPercentage As Label
    Public WithEvents LblPercentageText As Label
    Public WithEvents LblTotalQty As Label
    Public WithEvents LblTotalQtyText As Label
    Public WithEvents LinkLabel1 As LinkLabel
    Dim mItemTypeLastValue As String

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.LblIsSystemDefine = New System.Windows.Forms.Label()
        Me.ChkIsSystemDefine = New System.Windows.Forms.CheckBox()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.PnlMain = New System.Windows.Forms.Panel()
        Me.PnlTotals = New System.Windows.Forms.Panel()
        Me.LblPercentage = New System.Windows.Forms.Label()
        Me.LblPercentageText = New System.Windows.Forms.Label()
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
        'Pnl1
        '
        Me.Pnl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl1.Location = New System.Drawing.Point(2, 301)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(959, 233)
        Me.Pnl1.TabIndex = 2
        '
        'PnlMain
        '
        Me.PnlMain.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PnlMain.Location = New System.Drawing.Point(2, 43)
        Me.PnlMain.Name = "PnlMain"
        Me.PnlMain.Size = New System.Drawing.Size(959, 233)
        Me.PnlMain.TabIndex = 1
        '
        'PnlTotals
        '
        Me.PnlTotals.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlTotals.BackColor = System.Drawing.Color.Cornsilk
        Me.PnlTotals.Controls.Add(Me.LblPercentage)
        Me.PnlTotals.Controls.Add(Me.LblPercentageText)
        Me.PnlTotals.Controls.Add(Me.LblTotalQty)
        Me.PnlTotals.Controls.Add(Me.LblTotalQtyText)
        Me.PnlTotals.Location = New System.Drawing.Point(0, 534)
        Me.PnlTotals.Name = "PnlTotals"
        Me.PnlTotals.Size = New System.Drawing.Size(961, 23)
        Me.PnlTotals.TabIndex = 1062
        '
        'LblPercentage
        '
        Me.LblPercentage.AutoSize = True
        Me.LblPercentage.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPercentage.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblPercentage.Location = New System.Drawing.Point(551, 3)
        Me.LblPercentage.Name = "LblPercentage"
        Me.LblPercentage.Size = New System.Drawing.Size(12, 16)
        Me.LblPercentage.TabIndex = 666
        Me.LblPercentage.Text = "."
        Me.LblPercentage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblPercentage.Visible = False
        '
        'LblPercentageText
        '
        Me.LblPercentageText.AutoSize = True
        Me.LblPercentageText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPercentageText.ForeColor = System.Drawing.Color.Maroon
        Me.LblPercentageText.Location = New System.Drawing.Point(444, 3)
        Me.LblPercentageText.Name = "LblPercentageText"
        Me.LblPercentageText.Size = New System.Drawing.Size(61, 16)
        Me.LblPercentageText.TabIndex = 665
        Me.LblPercentageText.Text = "Total % :"
        Me.LblPercentageText.Visible = False
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
        Me.LinkLabel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(-1, 279)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(147, 21)
        Me.LinkLabel1.TabIndex = 1063
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Consumption Detail"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FrmCuttingConsumptionException
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(961, 606)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.PnlTotals)
        Me.Controls.Add(Me.PnlMain)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.LblIsSystemDefine)
        Me.Controls.Add(Me.ChkIsSystemDefine)
        Me.MaximizeBox = True
        Me.Name = "FrmCuttingConsumptionException"
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
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.PnlMain, 0)
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
    Public WithEvents Pnl1 As Panel
#End Region

    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, AgL.PubSiteCode, DglMain(Col1Value, rowItemType).Tag, DglMain(Col1Value, rowItemCategory).Tag, ItemV_Type.BOM, "", "")
        FGetSettings = mValue
    End Function

    Private Sub SetBomSkuName()
        Dim mBomSkuName As String
        mBomSkuName = ""

        If DglMain.Item(Col1Value, rowItem).Value <> "" Then
            If mBomSkuName <> "" Then mBomSkuName += "-"
            mBomSkuName += DglMain.Item(Col1Value, rowSpecification).Value
        End If
        If DglMain.Item(Col1Value, rowItemCategory).Value <> "" And DglMain.Item(Col1Value, rowItem).Value = "" Then
            If mBomSkuName <> "" Then mBomSkuName += "-"
            mBomSkuName += DglMain.Item(Col1Value, rowItemCategory).Value
        End If
        If DglMain.Item(Col1Value, rowItemGroup).Value <> "" Then
            If mBomSkuName <> "" Then mBomSkuName += "-"
            mBomSkuName += DglMain.Item(Col1Value, rowItemGroup).Value
        End If
        If DglMain.Item(Col1Value, rowDimension1).Value <> "" Then
            If mBomSkuName <> "" Then mBomSkuName += "-"
            mBomSkuName += DglMain.Item(Col1Value, rowDimension1).Value
        End If
        If DglMain.Item(Col1Value, rowDimension2).Value <> "" Then
            If mBomSkuName <> "" Then mBomSkuName += "-"
            mBomSkuName += DglMain.Item(Col1Value, rowDimension2).Value
        End If
        If DglMain.Item(Col1Value, rowDimension3).Value <> "" Then
            If mBomSkuName <> "" Then mBomSkuName += "-"
            mBomSkuName += DglMain.Item(Col1Value, rowDimension3).Value
        End If
        If DglMain.Item(Col1Value, rowDimension4).Value <> "" Then
            If mBomSkuName <> "" Then mBomSkuName += "-"
            mBomSkuName += DglMain.Item(Col1Value, rowDimension4).Value
        End If
        If DglMain.Item(Col1Value, rowSize).Value <> "" Then
            If mBomSkuName <> "" Then mBomSkuName += "-"
            mBomSkuName += DglMain.Item(Col1Value, rowSize).Value
        End If


        DglMain(Col1Value, rowSKU).Value = mBomSkuName & "-BOM"
    End Sub


    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer


        For I = 0 To DglMain.RowCount - 1
            If DglMain(Col1Mandatory, I).Value <> "" And DglMain.Rows(I).Visible Then
                If DglMain(Col1Value, I).Value.ToString = "" Then
                    MsgBox(DglMain(Col1Head, I).Value.ToString & " can not be blank.")
                    DglMain.CurrentCell = DglMain(Col1Value, I)
                    DglMain.Focus()
                    passed = False : Exit Sub
                End If
            End If
        Next


        SetBomSkuName()
        'If Val(LblDealQty.Text) <> 100 Then Err.Raise(1, , "Consumption should be 100% ")

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From Item Where Description='" & DglMain.Item(Col1Value, rowSKU).Value & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        Else
            mQry = "Select count(*) From Item Where Description='" & DglMain.Item(Col1Value, rowSKU).Value & "' And Code<>'" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If


        For I = 0 To DglMain.Rows.Count - 1
            If DglMain.Item(Col1Value, I).Value = Nothing Then DglMain.Item(Col1Value, I).Value = ""
            If DglMain.Item(Col1Value, I).Tag = Nothing Then DglMain.Item(Col1Value, I).Tag = ""
        Next


        For I = 0 To Dgl1.Rows.Count - 1
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
                                   , Dgl1.Item(Col1MItemCategory, I).Value _
                                   , Dgl1.Item(Col1MItemGroup, I).Value _
                                   , Dgl1.Item(Col1MItemSpecification, I).Value _
                                   , Dgl1.Item(Col1MDimension1, I).Value _
                                   , Dgl1.Item(Col1MDimension2, I).Value _
                                   , Dgl1.Item(Col1MDimension3, I).Value _
                                   , Dgl1.Item(Col1MDimension4, I).Value _
                                   , Dgl1.Item(Col1MSize, I).Value
                                   )
                If Dgl1.Item(Col1SKU, I).Tag = "" Then
                    passed = False
                    Exit Sub
                End If
            End If
            Dgl1.Item(Col1Process, I).Tag = ClsGarmentProduction.Process_Cutting
        Next
    End Sub
    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        AgL.PubFindQry = "SELECT H.Code, H.Description as Name
                            , I.Description as BaseItem
                            , D1.Description as [" & AgL.PubCaptionDimension1 & "]
                            , D2.Description as [" & AgL.PubCaptionDimension2 & "]
                            , D3.Description as [" & AgL.PubCaptionDimension3 & "]
                            , D4.Description as [" & AgL.PubCaptionDimension4 & "]
                            , Size.Description as Size
                            , H.DealQty as BatchQty, H.DealUnit BatchUnit, H.WastagePer, H.WeightForPer
                            FROM Item H
                            Left Join Item I On I.Code =   H.BaseItem
                            LEFT JOIN Item IC ON IC.Code = H.ItemCategory
                            LEFT JOIN Item IG ON IG.Code = H.ItemGroup
                            LEFT JOIN Item D1 ON D1.Code = H.Dimension1  
                            LEFT JOIN Item D2 ON D2.Code = H.Dimension2
                            LEFT JOIN Item D3 ON D3.Code = H.Dimension3
                            LEFT JOIN Item D4 ON D4.Code = H.Dimension4
                            LEFT JOIN Item Size ON Size.Code = H.Size
                            WHERE H.V_Type =" & AgL.Chk_Text(ItemV_Type.BOM) & " 
                            "
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
                Description=" & AgL.Chk_Text(DglMain.Item(Col1Value, rowSKU).Value) & ",
                BaseItem=" & AgL.Chk_Text(DglMain.Item(Col1Value, rowItem).Tag) & ",
                Specification = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSpecification).Value) & ",                
                ItemCategory = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowItemCategory).Tag) & ",
                ItemGroup = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowItemGroup).Tag) & ",
                Dimension1 = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowDimension1).Tag) & ",
                Dimension2 = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowDimension2).Tag) & ",
                Dimension3 = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowDimension3).Tag) & ",
                Dimension4 = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowDimension4).Tag) & ",
                Size = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSize).Tag) & ",
                ItemType = " & AgL.Chk_Text(ItemTypeCode.InternalProduct) & ",                 
                V_Type = " & AgL.Chk_Text(ItemV_Type.BOM) & ",
                Tags = " & AgL.Chk_Text(ExceptionTag) & ",
                DealQty = " & Val(DglMain.Item(Col1Value, rowBatchQty).Value) & ",                 
                DealUnit = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowBatchUnit).Value) & ",                 
                WastagePer = " & Val(DglMain.Item(Col1Value, rowWastagePer).Value) & ",                 
                WeightForPer = " & Val(DglMain.Item(Col1Value, rowWeightForPer).Value) & "                 
                Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "DELETE FROM BOMDetail WHERE Code  = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Item, I).Value <> "" And Val(Dgl1.Item(Col1Qty, I).Value) > 0 Then
                mQry = "INSERT INTO BOMDetail (Code, Sr, Process, Item, Qty, ConsumptionPer, FaceConsumptionPer)
                        VALUES ('" & SearchCode & "', " & I + 1 & ", " & AgL.Chk_Text(Dgl1.Item(Col1Process, I).Tag) & ", " & AgL.Chk_Text(Dgl1.Item(Col1SKU, I).Tag) & "                                                 
                        ," & Val(Dgl1.Item(Col1Qty, I).Value) & ", " & Val(Dgl1.Item(Col1ConsumptionPer, I).Value) & "  
                        , " & Val(Dgl1.Item(Col1FaceConsumptionPer, I).Value) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
        Next
    End Sub
    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet
        mQry = "SELECT Sku.*, 
                IC.Description as ItemCategoryName, IG.Description as ItemGroupName,
                D1.Description as Dimension1Name,D2.Description as Dimension2Name,
                D3.Description as Dimension3Name,D4.Description as Dimension4Name,
                Size.Description as SizeName, I.Description as ItemName
                FROM Item Sku
                Left Join Item I On Sku.BaseItem = I.Code
                Left Join Item IC On Sku.ItemCategory = IC.Code
                Left Join Item IG On Sku.ItemGroup = IG.Code
                LEFT JOIN Item D1 ON D1.Code = Sku.Dimension1  
                LEFT JOIN Item D2 ON D2.Code = Sku.Dimension2
                LEFT JOIN Item D3 ON D3.Code = Sku.Dimension3
                LEFT JOIN Item D4 ON D4.Code = Sku.Dimension4
                LEFT JOIN Item Size ON Size.Code = Sku.Size                
                WHERE Sku.Code ='" & SearchCode & "'
                "
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))

                DglMain.Item(Col1Value, rowSKU).Value = AgL.XNull(.Rows(0)("Description"))
                DglMain.Item(Col1Value, rowSKU).Tag = AgL.XNull(.Rows(0)("Code"))
                DglMain.Item(Col1Value, rowItem).Value = AgL.XNull(.Rows(0)("ItemName"))
                DglMain.Item(Col1Value, rowItem).Tag = AgL.XNull(.Rows(0)("BaseItem"))
                DglMain.Item(Col1Value, rowItemGroup).Tag = AgL.XNull(.Rows(0)("ItemGroup"))
                DglMain.Item(Col1Value, rowItemGroup).Value = AgL.XNull(.Rows(0)("ItemGroupName"))
                DglMain.Item(Col1Value, rowItemCategory).Tag = AgL.XNull(.Rows(0)("ItemCategory"))
                DglMain.Item(Col1Value, rowItemCategory).Value = AgL.XNull(.Rows(0)("ItemCategoryName"))
                DglMain.Item(Col1Value, rowDimension1).Tag = AgL.XNull(.Rows(0)("Dimension1"))
                DglMain.Item(Col1Value, rowDimension1).Value = AgL.XNull(.Rows(0)("Dimension1Name"))
                DglMain.Item(Col1Value, rowDimension2).Tag = AgL.XNull(.Rows(0)("Dimension2"))
                DglMain.Item(Col1Value, rowDimension2).Value = AgL.XNull(.Rows(0)("Dimension2Name"))
                DglMain.Item(Col1Value, rowDimension3).Tag = AgL.XNull(.Rows(0)("Dimension3"))
                DglMain.Item(Col1Value, rowDimension3).Value = AgL.XNull(.Rows(0)("Dimension3Name"))
                DglMain.Item(Col1Value, rowDimension4).Tag = AgL.XNull(.Rows(0)("Dimension4"))
                DglMain.Item(Col1Value, rowDimension4).Value = AgL.XNull(.Rows(0)("Dimension4Name"))
                DglMain.Item(Col1Value, rowSize).Tag = AgL.XNull(.Rows(0)("Size"))
                DglMain.Item(Col1Value, rowSize).Value = AgL.XNull(.Rows(0)("SizeName"))
                DglMain.Item(Col1Value, rowBatchQty).Value = AgL.VNull(.Rows(0)("DealQty"))
                DglMain.Item(Col1Value, rowBatchUnit).Value = AgL.XNull(.Rows(0)("DealUnit"))
                DglMain.Item(Col1Value, rowWastagePer).Value = AgL.VNull(.Rows(0)("WastagePer"))
                DglMain.Item(Col1Value, rowWeightForPer).Value = AgL.VNull(.Rows(0)("WeightForPer"))

                LblTotalQty.Text = DglMain.Item(Col1Value, rowWeightForPer).Value
                LblPercentage.Text = 100

                ApplyUISetting()


                'ChkIsSystemDefine.Checked = AgL.VNull(.Rows(0)("IsSystemDefine"))
                'LblIsSystemDefine.Text = IIf(AgL.VNull(.Rows(0)("IsSystemDefine")) = 0, "User Define", "System Define")
                ChkIsSystemDefine.Enabled = False
            End If
        End With


        Dim I As Integer
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
                WHERE H.Code ='" & SearchCode & "'
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
                    Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                    Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.00")
                    Dgl1.Item(Col1ConsumptionPer, I).Value = Format(AgL.VNull(.Rows(I)("ConsumptionPer")), "0.00")
                    Dgl1.Item(Col1MItemCategory, I).Tag = AgL.XNull(.Rows(I)("MItemCategory"))
                    Dgl1.Item(Col1MItemGroup, I).Tag = AgL.XNull(.Rows(I)("MItemGroup"))
                    Dgl1.Item(Col1MItemSpecification, I).Value = AgL.XNull(.Rows(I)("MItemSpecification"))
                    Dgl1.Item(Col1MDimension1, I).Tag = AgL.XNull(.Rows(I)("MDimension1"))
                    Dgl1.Item(Col1MDimension2, I).Tag = AgL.XNull(.Rows(I)("MDimension2"))
                    Dgl1.Item(Col1MDimension3, I).Tag = AgL.XNull(.Rows(I)("MDimension3"))
                    Dgl1.Item(Col1MDimension4, I).Tag = AgL.XNull(.Rows(I)("MDimension4"))
                    Dgl1.Item(Col1MSize, I).Tag = AgL.XNull(.Rows(I)("MSize"))

                    LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                Next I
                Dgl1.Visible = True
            Else
                Dgl1.Visible = False
            End If
        End With
        FrmItemBOM_BaseFunction_DispText()
    End Sub

    'Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
    '    TxtDescription.Focus()
    'End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        'Dgl1.CurrentCell = Dgl1(Col1Value, rowDimension2)
        DglMain.CurrentCell = DglMain.FirstDisplayedCell
        DglMain.Focus()
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
        mQry = "SELECT I.Code AS SearchCode FROM Item I  WHERE I.V_Type =  '" & ItemV_Type.BOM & "'" &
                " Order By I.Code "

        If FDivisionNameForCustomization(14) = "PRATHAM APPARE" Then
            mQry = "SELECT I.Code AS SearchCode 
                    FROM Item I  
                    LEFT JOIN BOMDetail L ON I.Code = L.Code
                    WHERE I.V_Type = '" & ItemV_Type.BOM & "'
                    AND IsNull(L.Process,'') = '" & ClsGarmentProduction.Process_Cutting & "'
                    And IsNull(I.Tags,'') = '" & ExceptionTag & "'
                    GROUP BY I.Code
                    ORDER By I.Code "
        End If
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

    Private Sub Dgl2_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name

                Case Col1Item
                    Validating_ItemCode(Dgl1.Item(mColumnIndex, mRowIndex).Tag, mColumnIndex, mRowIndex)


            End Select
            Call Calculation()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Validating_ItemCode(ItemCode As String, ByVal mColumn As Integer, ByVal mRow As Integer)
        Dim DtItem As DataTable = Nothing
        Try

            mQry = "Select I.Code, I.Description, I.ManualCode, I.Unit, I.Specification, I.ItemType
                    , I.ItemCategory, IC.Description as ItemCategoryName
                    , I.ItemGroup, IG.Description as ItemGroupName
                    , I.Dimension1, D1.Description as Dimension1Name
                    , I.Dimension2, D2.Description as Dimension2Name
                    , I.Dimension3, D3.Description as Dimension3Name
                    , I.Dimension4, D4.Description as Dimension4Name
                    , I.Size, Size.Description as SizeName 
                    From Item I  With (NoLock)
                    Left Join Item IC With (NoLock) On I.ItemCategory = IC.Code
                    Left Join Item IG With (NoLock) On I.ItemGroup = IG.Code
                    Left Join Item D1 With (NoLock) On I.Dimension1 = D1.Code
                    Left Join Item D2 With (NoLock) On I.Dimension2 = D2.Code
                    Left Join Item D3 With (NoLock) On I.Dimension3 = D3.Code
                    Left Join Item D4 With (NoLock) On I.Dimension4 = D1.Code
                    Left Join Item Size With (NoLock) On I.Size = Size.Code
                    Where I.Code ='" & ItemCode & "'"
            DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtItem.Rows.Count > 0 Then
                Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DtItem.Rows(0)("Unit"))
                Dgl1.Item(Col1Unit, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Unit"))

                Dgl1.Item(Col1ItemCategory, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemCategory"))
                Dgl1.Item(Col1ItemCategory, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemCategoryName"))

                If AgL.XNull(DtItem.Rows(0)("ItemGroup")) <> "" Then
                    Dgl1.Item(Col1ItemGroup, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemGroup"))
                    Dgl1.Item(Col1ItemGroup, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemGroupName"))
                End If

                Dgl1.Item(Col1MItemSpecification, mRow).Value = AgL.XNull(DtItem.Rows(0)("Specification"))
                If AgL.XNull(DtItem.Rows(0)("Dimension1")) <> "" Then
                    Dgl1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension1"))
                    Dgl1.Item(Col1Dimension1, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension1Name"))
                End If
                If AgL.XNull(DtItem.Rows(0)("Dimension2")) <> "" Then
                    Dgl1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension2"))
                    Dgl1.Item(Col1Dimension2, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension2Name"))
                End If
                If AgL.XNull(DtItem.Rows(0)("Dimension3")) <> "" Then
                    Dgl1.Item(Col1Dimension3, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension3"))
                    Dgl1.Item(Col1Dimension3, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension3Name"))
                End If
                If AgL.XNull(DtItem.Rows(0)("Dimension4")) <> "" Then
                    Dgl1.Item(Col1Dimension4, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension4"))
                    Dgl1.Item(Col1Dimension4, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension4Name"))
                End If
                If AgL.XNull(DtItem.Rows(0)("Size")) <> "" Then
                    Dgl1.Item(Col1Size, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Size"))
                    Dgl1.Item(Col1Size, mRow).Value = AgL.XNull(DtItem.Rows(0)("SizeName"))
                End If




                Dgl1.Item(Col1MItemCategory, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemCategory"))
                Dgl1.Item(Col1MItemCategory, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemCategoryName"))
                Dgl1.Item(Col1MItemGroup, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemGroup"))
                Dgl1.Item(Col1MItemGroup, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemGroupName"))
                Dgl1.Item(Col1MItemSpecification, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Specification"))
                Dgl1.Item(Col1MDimension1, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension1"))
                Dgl1.Item(Col1MDimension1, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension1Name"))
                Dgl1.Item(Col1MDimension2, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension2"))
                Dgl1.Item(Col1MDimension2, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension2Name"))
                Dgl1.Item(Col1MDimension3, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension3"))
                Dgl1.Item(Col1MDimension3, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension3Name"))
                Dgl1.Item(Col1MDimension4, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension4"))
                Dgl1.Item(Col1MDimension4, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension4Name"))
                Dgl1.Item(Col1MSize, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Size"))
                Dgl1.Item(Col1MSize, mRow).Value = AgL.XNull(DtItem.Rows(0)("SizeName"))
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub


    Private Sub Validating_ItemHeader(ItemCode As String)
        Dim DtItem As DataTable = Nothing
        Try

            mQry = "Select I.Code, I.Description, I.ManualCode, I.Unit, I.Specification, I.ItemType
                    , I.ItemCategory, IC.Description as ItemCategoryName
                    , I.ItemGroup, IG.Description as ItemGroupName
                    , I.Dimension1, D1.Description as Dimension1Name
                    , I.Dimension2, D2.Description as Dimension2Name
                    , I.Dimension3, D3.Description as Dimension3Name
                    , I.Dimension4, D4.Description as Dimension4Name
                    , I.Size, Size.Description as SizeName 
                    From Item I  With (NoLock)
                    Left Join Item IC With (NoLock) On I.ItemCategory = IC.Code
                    Left Join Item IG With (NoLock) On I.ItemGroup = IG.Code
                    Left Join Item D1 With (NoLock) On I.Dimension1 = D1.Code
                    Left Join Item D2 With (NoLock) On I.Dimension2 = D2.Code
                    Left Join Item D3 With (NoLock) On I.Dimension3 = D3.Code
                    Left Join Item D4 With (NoLock) On I.Dimension4 = D1.Code
                    Left Join Item Size With (NoLock) On I.Size = Size.Code
                    Where I.Code ='" & ItemCode & "'"
            DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtItem.Rows.Count > 0 Then
                DglMain.Item(Col1Value, rowBatchUnit).Value = AgL.XNull(DtItem.Rows(0)("Unit"))
                DglMain.Item(Col1Value, rowBatchUnit).Tag = AgL.XNull(DtItem.Rows(0)("Unit"))


                DglMain.Item(Col1Value, rowItemCategory).Tag = AgL.XNull(DtItem.Rows(0)("ItemCategory"))
                DglMain.Item(Col1Value, rowItemCategory).Value = AgL.XNull(DtItem.Rows(0)("ItemCategoryName"))

                If AgL.XNull(DtItem.Rows(0)("ItemGroup")) <> "" Then
                    DglMain.Item(Col1Value, rowItemGroup).Tag = AgL.XNull(DtItem.Rows(0)("ItemGroup"))
                    DglMain.Item(Col1Value, rowItemGroup).Value = AgL.XNull(DtItem.Rows(0)("ItemGroupName"))
                End If

                DglMain.Item(Col1Value, rowSpecification).Tag = AgL.XNull(DtItem.Rows(0)("Specification"))
                If AgL.XNull(DtItem.Rows(0)("Dimension1")) <> "" Then
                    DglMain.Item(Col1Value, rowDimension1).Tag = AgL.XNull(DtItem.Rows(0)("Dimension1"))
                    DglMain.Item(Col1Value, rowDimension1).Value = AgL.XNull(DtItem.Rows(0)("Dimension1Name"))
                End If
                If AgL.XNull(DtItem.Rows(0)("Dimension2")) <> "" Then
                    DglMain.Item(Col1Value, rowDimension2).Tag = AgL.XNull(DtItem.Rows(0)("Dimension2"))
                    DglMain.Item(Col1Value, rowDimension2).Value = AgL.XNull(DtItem.Rows(0)("Dimension2Name"))
                End If
                If AgL.XNull(DtItem.Rows(0)("Dimension3")) <> "" Then
                    DglMain.Item(Col1Value, rowDimension3).Tag = AgL.XNull(DtItem.Rows(0)("Dimension3"))
                    DglMain.Item(Col1Value, rowDimension3).Value = AgL.XNull(DtItem.Rows(0)("Dimension3Name"))
                End If
                If AgL.XNull(DtItem.Rows(0)("Dimension4")) <> "" Then
                    DglMain.Item(Col1Value, rowDimension4).Tag = AgL.XNull(DtItem.Rows(0)("Dimension4"))
                    DglMain.Item(Col1Value, rowDimension4).Value = AgL.XNull(DtItem.Rows(0)("Dimension4Name"))
                End If
                If AgL.XNull(DtItem.Rows(0)("Size")) <> "" Then
                    DglMain.Item(Col1Value, rowDimension4).Tag = AgL.XNull(DtItem.Rows(0)("Size"))
                    DglMain.Item(Col1Value, rowDimension4).Value = AgL.XNull(DtItem.Rows(0)("SizeName"))
                End If




                'Dgl2.Item(Col1MItemCategory, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemCategory"))
                'Dgl2.Item(Col1MItemCategory, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemCategoryName"))
                'Dgl2.Item(Col1MItemGroup, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemGroup"))
                'Dgl2.Item(Col1MItemGroup, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemGroupName"))
                'Dgl2.Item(Col1MItemSpecification, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Specification"))
                'Dgl2.Item(Col1MDimension1, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension1"))
                'Dgl2.Item(Col1MDimension1, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension1Name"))
                'Dgl2.Item(Col1MDimension2, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension2"))
                'Dgl2.Item(Col1MDimension2, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension2Name"))
                'Dgl2.Item(Col1MDimension3, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension3"))
                'Dgl2.Item(Col1MDimension3, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension3Name"))
                'Dgl2.Item(Col1MDimension4, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension4"))
                'Dgl2.Item(Col1MDimension4, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension4Name"))
                'Dgl2.Item(Col1MSize, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Size"))
                'Dgl2.Item(Col1MSize, mRow).Value = AgL.XNull(DtItem.Rows(0)("SizeName"))
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub


    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        Passed = FRestrictSystemDefine()


        ApplyUISetting()
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


        If DglMain.Rows(rowItem).Visible Then DglMain.CurrentCell = DglMain(Col1Value, rowItem)
        If DglMain.Rows(rowItemGroup).Visible Then DglMain.CurrentCell = DglMain(Col1Value, rowItemGroup)
        If DglMain.Rows(rowItemCategory).Visible Then DglMain.CurrentCell = DglMain(Col1Value, rowItemCategory)
        DglMain.Focus()
        ApplyUISetting()
    End Sub

    Private Sub FrmItemBOM_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dim I As Integer
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Process, 120, 0, Col1Process, True, False, False)
            .AddAgTextColumn(Dgl1, Col1ItemCategory, 180, 0, Col1ItemCategory, True, False, False)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 180, 0, Col1ItemGroup, True, False, False)
            .AddAgTextColumn(Dgl1, Col1Item, 300, 0, Col1Item, True, False, False)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 150, 0, AgL.PubCaptionDimension1, True, False, False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 150, 0, AgL.PubCaptionDimension2, True, False, False)
            .AddAgTextColumn(Dgl1, Col1Dimension3, 150, 0, AgL.PubCaptionDimension3, True, False, False)
            .AddAgTextColumn(Dgl1, Col1Dimension4, 150, 0, AgL.PubCaptionDimension4, True, False, False)
            .AddAgTextColumn(Dgl1, Col1Size, 120, 0, Col1Size, True, False, False)
            .AddAgTextColumn(Dgl1, Col1SKU, 300, 0, Col1SKU, True, False, False)
            .AddAgNumberColumn(Dgl1, Col1Qty, 70, 3, 3, False, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, False, False)
            .AddAgNumberColumn(Dgl1, Col1ConsumptionPer, 80, 2, 3, False, "%", True, False, True)
            .AddAgNumberColumn(Dgl1, Col1FaceConsumptionPer, 80, 2, 3, False, Col1FaceConsumptionPer, False, False, True)


            .AddAgTextColumn(Dgl1, Col1MItemCategory, 100, 0, Col1MItemCategory, True, False, False)
            .AddAgTextColumn(Dgl1, Col1MItemGroup, 100, 0, Col1MItemGroup, True, False, False)
            .AddAgTextColumn(Dgl1, Col1MItemSpecification, 100, 0, Col1MItemSpecification, True, False, False)
            .AddAgTextColumn(Dgl1, Col1MDimension1, 100, 0, "M " & AgL.PubCaptionDimension1, True, False, False)
            .AddAgTextColumn(Dgl1, Col1MDimension2, 100, 0, "M " & AgL.PubCaptionDimension2, True, False, False)
            .AddAgTextColumn(Dgl1, Col1MDimension3, 100, 0, "M " & AgL.PubCaptionDimension3, True, False, False)
            .AddAgTextColumn(Dgl1, Col1MDimension4, 100, 0, "M " & AgL.PubCaptionDimension4, True, False, False)
            .AddAgTextColumn(Dgl1, Col1MSize, 100, 0, Col1MSize, True, False, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.RowHeadersVisible = False
        Dgl1.BackgroundColor = Me.BackColor
        'Dgl2.AllowUserToAddRows = False
        AgL.GridDesign(Dgl1)
        Dgl1.Anchor = AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom


        DglMain.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DglMain, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(DglMain, Col1Head, 200, 255, Col1Head, True, True)
            .AddAgTextColumn(DglMain, Col1HeadOriginal, 180, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(DglMain, Col1Mandatory, 12, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(DglMain, Col1Value, 580, 255, Col1Value, True, False)
        End With
        AgL.AddAgDataGrid(DglMain, PnlMain)
        DglMain.Columns(Col1Mandatory).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings 2", 5.25, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        DglMain.Columns(Col1Mandatory).DefaultCellStyle.ForeColor = Color.Red
        DglMain.EnableHeadersVisualStyles = False
        DglMain.ColumnHeadersHeight = 35
        DglMain.AgSkipReadOnlyColumns = True
        DglMain.AllowUserToAddRows = False
        DglMain.RowHeadersVisible = False
        DglMain.ColumnHeadersVisible = False
        AgL.GridDesign(DglMain)
        DglMain.BackgroundColor = Me.BackColor
        DglMain.Anchor = AnchorStyles.Top + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom

        DglMain.Rows.Add(16)
        'For I = 0 To Dgl1.Rows.Count - 1
        '    Dgl1.Rows(I).Visible = False
        'Next

        DglMain.Item(Col1Head, rowSKU).Value = hcSKU
        DglMain.Item(Col1Head, rowProcess).Value = hcProcess
        DglMain.Item(Col1Head, rowItemType).Value = hcItemType
        DglMain.Item(Col1Head, rowItemCategory).Value = hcItemCategory
        DglMain.Item(Col1Head, rowItemGroup).Value = hcItemGroup
        DglMain.Item(Col1Head, rowDimension1).Value = hcDimension1
        DglMain.Item(Col1Head, rowDimension2).Value = hcDimension2
        DglMain.Item(Col1Head, rowDimension3).Value = hcDimension3
        DglMain.Item(Col1Head, rowDimension4).Value = hcDimension4
        DglMain.Item(Col1Head, rowSize).Value = hcSize
        DglMain.Item(Col1Head, rowItem).Value = hcItem
        DglMain.Item(Col1Head, rowBatchQty).Value = hcBatchQty
        DglMain.Item(Col1Head, rowBatchUnit).Value = hcBatchUnit
        DglMain.Item(Col1Head, rowWastagePer).Value = hcWastagePer
        DglMain.Item(Col1Head, rowWeightForPer).Value = hcWeightForPer


        For I = 0 To DglMain.Rows.Count - 1
            DglMain(Col1HeadOriginal, I).Value = DglMain(Col1Head, I).Value
        Next

    End Sub
    Sub SetProductName()
        If DglMain.Item(Col1Value, rowSpecification).Value = "" Then Exit Sub

        Dim mName As String = FGetSettings(SettingFields.ItemNamePattern, SettingType.General)
        If mName = "" Then mName = "<SPECIFICATION>"
        mName = mName.ToString.ToUpper.Replace("+", "||").Replace("'%*S'", "'%*s'").
            Replace("<SPECIFICATION>", DglMain.Item(Col1Value, rowSpecification).Value).
                          Replace("<ITEMGROUP>", DglMain.Item(Col1Value, rowItemGroup).Value).
                          Replace("<ITEMCATEGORY>", DglMain.Item(Col1Value, rowItemCategory).Value).
                          Replace("<ITEMTYPE>", DglMain.Item(Col1Value, rowItemType).Value).
                          Replace("<DIMENSION1>", DglMain.Item(Col1Value, rowDimension1).Value).
                          Replace("<DIMENSION2>", DglMain.Item(Col1Value, rowDimension2).Value).
                          Replace("<DIMENSION3>", DglMain.Item(Col1Value, rowDimension3).Value).
                          Replace("<DIMENSION4>", DglMain.Item(Col1Value, rowDimension4).Value).
                          Replace("<SIZE>", DglMain.Item(Col1Value, rowSize).Value)
        mName = "SELECT " & "'" & mName & "'"
        mName = AgL.GetBackendBasedQuery(mName)
        mName = AgL.Dman_Execute(mName, AgL.GCn).ExecuteScalar
        'Dgl1(Col1Value, rowItemName).Value = Dgl1(Col1Value, rowSpecification).Value + Space(10) + "[" + Dgl1(Col1Value, rowItemGroup).Value + " | " + Dgl1(Col1Value, rowItemCategory).Value + "]"
        DglMain(Col1Value, rowItem).Value = mName
    End Sub
    Private Sub Calculation()
        Dim I As Integer
        If Topctrl1.Mode = "Browse" Then Exit Sub


        LblTotalQty.Text = 0
        LblPercentage.Text = 0



        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" And Dgl1.Rows(I).Visible Then
                'Footer Calculation
                Dim bQty As Double = 0
                Dim bPer As Double = 0

                If Val(DglMain.Item(Col1Value, rowWeightForPer).Value) > 0 Then


                    If Dgl1.Item(Col1ConsumptionPer, I).Value = 0 Then
                        Dgl1.Item(Col1FaceConsumptionPer, I).Value = Math.Round(Val(Dgl1.Item(Col1Qty, I).Value) / Val(DglMain.Item(Col1Value, rowWeightForPer).Value) * 100, 2)
                    Else
                        Dgl1.Item(Col1FaceConsumptionPer, I).Value = Dgl1.Item(Col1ConsumptionPer, I).Value
                        Dgl1.Item(Col1Qty, I).Value = Dgl1.Item(Col1ConsumptionPer, I).Value * Val(DglMain.Item(Col1Value, rowWeightForPer).Value) / 100
                    End If
                End If
                bQty = Val(Dgl1.Item(Col1Qty, I).Value)
                bPer = Val(Dgl1.Item(Col1FaceConsumptionPer, I).Value)

                LblTotalQty.Text = Val(LblTotalQty.Text) + bQty
                LblPercentage.Text = Val(LblPercentage.Text) + bPer

            End If
        Next
    End Sub


    Private Sub DglRateType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
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

    Private Sub ApplyUISetting()
        Dim mQry As String
        Dim DtTemp As DataTable
        Dim I As Integer, J As Integer
        Dim mDgl1RowCount As Integer
        Dim mDglRateTypeColumnCount As Integer
        Try

            For I = 0 To DglMain.Rows.Count - 1
                DglMain.Rows(I).Visible = False
            Next


            mQry = "Select H.*
                    from EntryHeaderUISetting H                   
                    Where EntryName='FrmCuttingConsumptionException' And GridName ='DglMain' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 0 To DglMain.Rows.Count - 1
                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = DglMain.Item(Col1HeadOriginal, J).Value Then
                            DglMain.Rows(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
                            If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDgl1RowCount += 1
                            DglMain.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
                            If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
                                DglMain.Item(Col1Head, J).Value = AgL.XNull(DtTemp.Rows(I)("Caption"))
                            End If
                            If AgL.VNull(DtTemp.Rows(I)("IsEditable")) = 0 Then DglMain.Rows(J).ReadOnly = True
                        End If
                    Next
                Next
            End If
            If mDgl1RowCount = 0 Then DglMain.Visible = False Else DglMain.Visible = True



            For I = 0 To Dgl1.Columns.Count - 1
                Dgl1.Columns(I).Visible = False
            Next



            mQry = "Select H.*
                    from EntryLineUISetting H                    
                    Where EntryName='FrmCuttingConsumptionException' And GridName ='Dgl1' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 0 To Dgl1.Columns.Count - 1
                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = Dgl1.Columns(J).Name Then
                            Dgl1.Columns(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
                            If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDglRateTypeColumnCount += 1
                            If Not IsDBNull(DtTemp.Rows(I)("DisplayIndex")) Then
                                Dgl1.Columns(J).DisplayIndex = AgL.VNull(DtTemp.Rows(I)("DisplayIndex"))
                            End If
                        End If
                    Next
                Next
            End If
            If mDglRateTypeColumnCount = 0 Then Dgl1.Visible = False Else Dgl1.Visible = True


            DglMain.Item(Col1Head, rowDimension1).Value = AgL.PubCaptionDimension1
            DglMain.Item(Col1Head, rowDimension2).Value = AgL.PubCaptionDimension2
            DglMain.Item(Col1Head, rowDimension3).Value = AgL.PubCaptionDimension3
            DglMain.Item(Col1Head, rowDimension4).Value = AgL.PubCaptionDimension4
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
                DglMain(Col1Value, rowDimension1).ReadOnly = IIf(Topctrl1.Mode <> "Browse", True, False)
            Else
                DglMain(Col1Value, rowDimension1).ReadOnly = False
            End If
        Else
            DglMain(Col1Value, rowDimension1).ReadOnly = False
        End If





    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DglMain.CellEnter
        Try

            If DglMain.CurrentCell Is Nothing Then Exit Sub
            If Topctrl1.Mode = "BROWSE" Then
                DglMain.CurrentCell.ReadOnly = True
            End If

            If DglMain.CurrentCell.ColumnIndex <> DglMain.Columns(Col1Value).Index Then Exit Sub


            DglMain.AgHelpDataSet(DglMain.CurrentCell.ColumnIndex) = Nothing
            CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0
            CType(DglMain.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = False

            Select Case DglMain.CurrentCell.RowIndex
                Case rowBatchQty, rowWeightForPer
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgNumberLeftPlaces = 2
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgNumberRightPlaces = 2

                Case rowWastagePer
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgNumberLeftPlaces = 3
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgNumberRightPlaces = 3
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DglMain.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = DglMain.CurrentCell.RowIndex
            bColumnIndex = DglMain.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> DglMain.Columns(Col1Value).Index Then Exit Sub

            Select Case DglMain.CurrentCell.RowIndex
                Case rowProcess
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " SELECT Sg.SubCode AS Code, Sg.Name, Parent.Name as ParentName 
                            FROM Subgroup Sg With (NoLock)
                            Left Join Subgroup Parent On Parent.Subcode = Sg.Parent
                            Where Sg.SubgroupType = '" & AgLibrary.ClsMain.agConstants.SubgroupType.Process & "' 
                            And IfNull(Sg.Status,'Active') = 'Active' And Sg.Subcode Not In ('" & Process.Purchase & "', '" & Process.Sales & "')"
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If

                Case rowItem
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " Select I.Code, I.Description, IT.Name as ItemType 
                                     From Item I With (Nolock)
                                     Left Join ItemType IT With (Nolock) On I.ItemType = IT.Code
                                     Where IfNull(I.Status,'Active') = 'Active' And I.V_Type = '" & ItemV_Type.Item & "'
                                     Order By I.Description"
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If

                Case rowItemCategory
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            FCreateHelpItemCategoryHead()
                        End If
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If


                Case rowItemGroup
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = " Select I.Code, I.Description, IT.Name as ItemType 
                                     From Item I With (Nolock)
                                     Left Join ItemType IT With (Nolock) On I.ItemType = IT.Code
                                     Where IfNull(I.Status,'Active') = 'Active' And I.V_Type = '" & ItemV_Type.ItemGroup & "'
                                     Order By I.Description"
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If

                Case rowDimension1
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        FCreateHelpDimension1Head()
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If
                Case rowDimension2
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        FCreateHelpDimension2Head()
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If
                Case rowDimension3
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        FCreateHelpDimension3Head()
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If
                Case rowDimension4
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        FCreateHelpDimension4Head()
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If
                Case rowSize
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        FCreateHelpSizeHead()
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If

                Case rowBatchUnit
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Code AS Description FROM Unit "
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FCreateHelpItemHead()
        Dim strCond As String = ""


        If FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General) & "') > 0 OR I.ItemType Is Null) "
            ElseIf FGetSettings(SettingFields.FilterInclude_Process, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General) & "') <= 0 OR I.ItemType Is Null) "
            End If
        End If


        If FGetSettings(SettingFields.FilterInclude_ItemV_TypeHeader, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_ItemV_TypeHeader, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || I.V_Type,'" & FGetSettings(SettingFields.FilterInclude_ItemV_TypeHeader, SettingType.General) & "') > 0 OR I.V_Type Is Null) "
            ElseIf FGetSettings(SettingFields.FilterInclude_Process, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || I.V_Type,'" & FGetSettings(SettingFields.FilterInclude_ItemV_TypeHeader, SettingType.General) & "') <= 0 OR I.V_Type Is Null) "
            End If
        Else
            strCond += " And I.V_Type = '" & ItemV_Type.Item & "' "
        End If


        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherDivisions")) Then
            strCond += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(I.ShowItemInOtherDivisions,0) =1 Or I.Div_Code Is Null) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherSites")) Then
            strCond += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(I.ShowItemInOtherSites,0) =1 Or I.Site_Code Is Null) "
        End If


        If DglMain.Item(Col1Value, rowItemType).Value <> "" And DglMain.Rows(rowItemType).Visible Then
            strCond += " And (I.ItemType = '" & DglMain.Item(Col1Value, rowItemType).Tag & "' or I.ItemType Is Null) "
        End If

        If DglMain.Item(Col1Value, rowItemCategory).Value <> "" And DglMain.Rows(rowItemCategory).Visible Then
            strCond += " And (I.ItemCategory = '" & DglMain.Item(Col1Value, rowItemCategory).Tag & "' or I.ItemCategory Is Null) "
        End If

        If DglMain.Item(Col1Value, rowItemGroup).Value <> "" And DglMain.Rows(rowItemGroup).Visible Then
            strCond += " And (I.ItemGroup = '" & DglMain.Item(Col1Value, rowItemGroup).Tag & "' or I.ItemGroup Is Null) "
        End If



        mQry = " Select I.Code, I.Description, IT.Name as ItemType 
                                     From Item I With (Nolock)
                                     Left Join ItemType IT With (Nolock) On I.ItemType = IT.Code
                                     Where IfNull(I.Status,'Active') = 'Active' " & strCond & "
                                     Order By I.Description"
        DglMain.Item(Col1Head, rowItemCategory).Tag = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FCreateHelpItemCategoryHead()
        Dim strCond As String = ""


        If FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General) & "') > 0 OR I.ItemType Is Null) "
            ElseIf FGetSettings(SettingFields.FilterInclude_Process, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General) & "') <= 0 OR I.ItemType Is Null) "
            End If
        End If


        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherDivisions")) Then
            strCond += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(I.ShowItemInOtherDivisions,0) =1 Or I.Div_Code Is Null) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherSites")) Then
            strCond += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(I.ShowItemInOtherSites,0) =1 Or I.Site_Code Is Null) "
        End If


        If DglMain.Item(Col1Value, rowItemType).Value <> "" And DglMain.Rows(rowItemType).Visible Then
            strCond += " And (I.ItemType = '" & DglMain.Item(Col1Value, rowItemType).Tag & "' or I.ItemType Is Null) "
        End If


        mQry = " Select I.Code, I.Description, IT.Name as ItemType 
                                     From Item I With (Nolock)
                                     Left Join ItemType IT With (Nolock) On I.ItemType = IT.Code
                                     Where IfNull(I.Status,'Active') = 'Active' And I.V_Type = '" & ItemV_Type.ItemCategory & "' " & strCond & "
                                     Order By I.Description"
        DglMain.Item(Col1Head, rowItemCategory).Tag = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FCreateHelpDimension1Head()
        Dim strCond As String = ""

        If FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General) & "') > 0 OR I.ItemType Is Null) "
            ElseIf FGetSettings(SettingFields.FilterInclude_Process, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General) & "') <= 0 OR I.ItemType Is Null) "
            End If
        End If


        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherDivisions")) Then
            strCond += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(I.ShowItemInOtherDivisions,0) =1 Or I.Div_Code Is Null) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherSites")) Then
            strCond += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(I.ShowItemInOtherSites,0) =1 Or I.Site_Code Is Null) "
        End If


        If DglMain.Item(Col1Value, rowItemType).Value <> "" And DglMain.Rows(rowItemType).Visible Then
            strCond += " And (I.ItemType = '" & DglMain.Item(Col1Value, rowItemType).Tag & "' or I.ItemType Is Null) "
        End If

        If DglMain.Item(Col1Value, rowItemCategory).Value <> "" And DglMain.Rows(rowItemCategory).Visible Then
            strCond += " And (I.ItemCategory = '" & DglMain.Item(Col1Value, rowItemCategory).Tag & "' or I.ItemCategory Is Null) "
        End If

        If DglMain.Item(Col1Value, rowItemGroup).Value <> "" And DglMain.Rows(rowItemGroup).Visible Then
            strCond += " And (I.ItemGroup = '" & DglMain.Item(Col1Value, rowItemGroup).Tag & "' or I.ItemGroup Is Null) "
        End If



        mQry = " Select I.Code, I.Description, IT.Name as ItemType 
                                     From Item I With (Nolock)
                                     Left Join ItemType IT With (Nolock) On I.ItemType = IT.Code
                                     Where IfNull(I.Status,'Active') = 'Active' And I.V_Type = '" & ItemV_Type.Dimension1 & "' " & strCond & "
                                     Order By I.Description"
        DglMain.Item(Col1Head, rowDimension1).Tag = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FCreateHelpDimension2Head()
        Dim strCond As String = ""

        If FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General) & "') > 0 OR I.ItemType Is Null) "
            ElseIf FGetSettings(SettingFields.FilterInclude_Process, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General) & "') <= 0 OR I.ItemType Is Null) "
            End If
        End If


        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherDivisions")) Then
            strCond += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(I.ShowItemInOtherDivisions,0) =1 Or I.Div_Code Is Null) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherSites")) Then
            strCond += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(I.ShowItemInOtherSites,0) =1 Or I.Site_Code Is Null) "
        End If


        If DglMain.Item(Col1Value, rowItemType).Value <> "" And DglMain.Rows(rowItemType).Visible Then
            strCond += " And (I.ItemType = '" & DglMain.Item(Col1Value, rowItemType).Tag & "' or I.ItemType Is Null) "
        End If

        If DglMain.Item(Col1Value, rowItemCategory).Value <> "" And DglMain.Rows(rowItemCategory).Visible Then
            strCond += " And (I.ItemCategory = '" & DglMain.Item(Col1Value, rowItemCategory).Tag & "' or I.ItemCategory Is Null) "
        End If

        If DglMain.Item(Col1Value, rowItemGroup).Value <> "" And DglMain.Rows(rowItemGroup).Visible Then
            strCond += " And (I.ItemGroup = '" & DglMain.Item(Col1Value, rowItemGroup).Tag & "' or I.ItemGroup Is Null) "
        End If



        mQry = " Select I.Code, I.Description, IT.Name as ItemType 
                                     From Item I With (Nolock)
                                     Left Join ItemType IT With (Nolock) On I.ItemType = IT.Code
                                     Where IfNull(I.Status,'Active') = 'Active' And I.V_Type = '" & ItemV_Type.Dimension2 & "' " & strCond & "
                                     Order By I.Description"
        DglMain.Item(Col1Head, rowDimension2).Tag = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FCreateHelpDimension3Head()
        Dim strCond As String = ""

        If FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General) & "') > 0 OR I.ItemType Is Null) "
            ElseIf FGetSettings(SettingFields.FilterInclude_Process, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General) & "') <= 0 OR I.ItemType Is Null) "
            End If
        End If


        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherDivisions")) Then
            strCond += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(I.ShowItemInOtherDivisions,0) =1 Or I.Div_Code Is Null) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherSites")) Then
            strCond += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(I.ShowItemInOtherSites,0) =1 Or I.Site_Code Is Null) "
        End If


        If DglMain.Item(Col1Value, rowItemType).Value <> "" And DglMain.Rows(rowItemType).Visible Then
            strCond += " And (I.ItemType = '" & DglMain.Item(Col1Value, rowItemType).Tag & "' or I.ItemType Is Null) "
        End If

        If DglMain.Item(Col1Value, rowItemCategory).Value <> "" And DglMain.Rows(rowItemCategory).Visible Then
            strCond += " And (I.ItemCategory = '" & DglMain.Item(Col1Value, rowItemCategory).Tag & "' or I.ItemCategory Is Null) "
        End If

        If DglMain.Item(Col1Value, rowItemGroup).Value <> "" And DglMain.Rows(rowItemGroup).Visible Then
            strCond += " And (I.ItemGroup = '" & DglMain.Item(Col1Value, rowItemGroup).Tag & "' or I.ItemGroup Is Null) "
        End If



        mQry = " Select I.Code, I.Description, IT.Name as ItemType 
                                     From Item I With (Nolock)
                                     Left Join ItemType IT With (Nolock) On I.ItemType = IT.Code
                                     Where IfNull(I.Status,'Active') = 'Active' And I.V_Type = '" & ItemV_Type.Dimension3 & "' " & strCond & "
                                     Order By I.Description"
        DglMain.Item(Col1Head, rowDimension3).Tag = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FCreateHelpDimension4Head()
        Dim strCond As String = ""

        If FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General) & "') > 0 OR I.ItemType Is Null) "
            ElseIf FGetSettings(SettingFields.FilterInclude_Process, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General) & "') <= 0 OR I.ItemType Is Null) "
            End If
        End If


        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherDivisions")) Then
            strCond += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(I.ShowItemInOtherDivisions,0) =1 Or I.Div_Code Is Null) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherSites")) Then
            strCond += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(I.ShowItemInOtherSites,0) =1 Or I.Site_Code Is Null) "
        End If


        If DglMain.Item(Col1Value, rowItemType).Value <> "" And DglMain.Rows(rowItemType).Visible Then
            strCond += " And (I.ItemType = '" & DglMain.Item(Col1Value, rowItemType).Tag & "' or I.ItemType Is Null) "
        End If

        If DglMain.Item(Col1Value, rowItemCategory).Value <> "" And DglMain.Rows(rowItemCategory).Visible Then
            strCond += " And (I.ItemCategory = '" & DglMain.Item(Col1Value, rowItemCategory).Tag & "' or I.ItemCategory Is Null) "
        End If

        If DglMain.Item(Col1Value, rowItemGroup).Value <> "" And DglMain.Rows(rowItemGroup).Visible Then
            strCond += " And (I.ItemGroup = '" & DglMain.Item(Col1Value, rowItemGroup).Tag & "' or I.ItemGroup Is Null) "
        End If



        mQry = " Select I.Code, I.Description, IT.Name as ItemType 
                                     From Item I With (Nolock)
                                     Left Join ItemType IT With (Nolock) On I.ItemType = IT.Code
                                     Where IfNull(I.Status,'Active') = 'Active' And I.V_Type = '" & ItemV_Type.Dimension4 & "' " & strCond & "
                                     Order By I.Description"
        DglMain.Item(Col1Head, rowDimension4).Tag = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FCreateHelpSizeHead()
        Dim strCond As String = ""

        If FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General) & "') > 0 OR I.ItemType Is Null) "
            ElseIf FGetSettings(SettingFields.FilterInclude_Process, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemTypeHeader, SettingType.General) & "') <= 0 OR I.ItemType Is Null) "
            End If
        End If


        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherDivisions")) Then
            strCond += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(I.ShowItemInOtherDivisions,0) =1 Or I.Div_Code Is Null) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherSites")) Then
            strCond += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(I.ShowItemInOtherSites,0) =1 Or I.Site_Code Is Null) "
        End If

        If DglMain.Item(Col1Value, rowItemType).Value <> "" And DglMain.Rows(rowItemType).Visible Then
            strCond += " And (I.ItemType = '" & DglMain.Item(Col1Value, rowItemType).Tag & "' or I.ItemType Is Null) "
        End If

        If DglMain.Item(Col1Value, rowItemCategory).Value <> "" And DglMain.Rows(rowItemCategory).Visible Then
            strCond += " And (I.ItemCategory = '" & DglMain.Item(Col1Value, rowItemCategory).Tag & "' or I.ItemCategory Is Null) "
        End If

        If DglMain.Item(Col1Value, rowItemGroup).Value <> "" And DglMain.Rows(rowItemGroup).Visible Then
            strCond += " And (I.ItemGroup = '" & DglMain.Item(Col1Value, rowItemGroup).Tag & "' or I.ItemGroup Is Null) "
        End If


        mQry = " Select I.Code, I.Description, IT.Name as ItemType 
                                     From Item I With (Nolock)
                                     Left Join ItemType IT With (Nolock) On I.ItemType = IT.Code
                                     Where IfNull(I.Status,'Active') = 'Active' And I.V_Type = '" & ItemV_Type.SIZE & "' " & strCond & "
                                     Order By I.Description"
        DglMain.Item(Col1Head, rowSize).Tag = AgL.FillData(mQry, AgL.GCn)
    End Sub


    Private Sub FCreateHelpItem(mRowIndex As Integer)
        Dim strCond As String = ""

        If FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) & "') > 0 OR I.ItemType Is Null) "
            ElseIf FGetSettings(SettingFields.FilterInclude_Process, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) & "') <= 0 OR I.ItemType Is Null) "
            End If
        End If

        If FGetSettings(SettingFields.FilterInclude_ItemV_Type, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_ItemV_Type, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || I.V_Type,'" & FGetSettings(SettingFields.FilterInclude_ItemV_Type, SettingType.General) & "') > 0 OR I.V_Type Is Null) "
            ElseIf FGetSettings(SettingFields.FilterInclude_Process, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || I.V_Type,'" & FGetSettings(SettingFields.FilterInclude_ItemV_Type, SettingType.General) & "') <= 0 OR I.V_Type Is Null) "
            End If
        Else
            strCond += " And I.V_Type = '" & ItemV_Type.Item & "' "
        End If


        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherDivisions")) Then
            strCond += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(I.ShowItemInOtherDivisions,0) =1 Or I.Div_Code Is Null) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherSites")) Then
            strCond += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(I.ShowItemInOtherSites,0) =1 Or I.Site_Code Is Null) "
        End If


        'If Dgl2.Item(Col1Value, rowItemType).Value <> "" And Dgl2.Columns(col1ItemType).Visible Then
        '    strCond += " And (I.ItemType = '" & Dgl1.Item(Col1Value, rowItemType).Tag & "' or I.ItemType Is Null) "
        'End If

        If Dgl1.Item(Col1ItemCategory, mRowIndex).Value <> "" And Dgl1.Columns(Col1ItemCategory).Visible Then
            strCond += " And (I.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, mRowIndex).Tag & "' or I.ItemCategory Is Null) "
        End If

        If Dgl1.Item(Col1ItemGroup, mRowIndex).Value <> "" And Dgl1.Columns(Col1ItemGroup).Visible Then
            strCond += " And (I.ItemGroup = '" & Dgl1.Item(Col1ItemGroup, mRowIndex).Tag & "' or I.ItemGroup Is Null) "
        End If


        mQry = " Select I.Code, I.Description, IT.Name as ItemType 
                                     From Item I With (Nolock)
                                     Left Join ItemType IT With (Nolock) On I.ItemType = IT.Code
                                     Where IfNull(I.Status,'Active') = 'Active' " & strCond & "
                                     Order By I.Description"
        Dgl1.AgHelpDataSet(Col1Item) = AgL.FillData(mQry, AgL.GCn)
    End Sub


    Private Sub FCreateHelpDimension4(mRowIndex As Integer)
        Dim strCond As String = ""

        If FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) & "') > 0 OR I.ItemType Is Null) "
            ElseIf FGetSettings(SettingFields.FilterInclude_Process, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) & "') <= 0 OR I.ItemType Is Null) "
            End If
        End If



        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherDivisions")) Then
            strCond += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(I.ShowItemInOtherDivisions,0) =1 Or I.Div_Code Is Null) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherSites")) Then
            strCond += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(I.ShowItemInOtherSites,0) =1 Or I.Site_Code Is Null) "
        End If


        'If Dgl2.Item(Col1ItemType, mRowIndex).Value <> "" And Dgl2.Columns(Col1ItemType).Visible Then
        '    strCond += " And (I.ItemType = '" & Dgl2.Item(Col1ItemType, mRowIndex).Tag & "' or I.ItemType Is Null) "
        'End If

        If Dgl1.Item(Col1ItemCategory, mRowIndex).Value <> "" And Dgl1.Columns(Col1ItemCategory).Visible Then
            strCond += " And (I.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, mRowIndex).Tag & "' or I.ItemCategory Is Null) "
        End If

        If Dgl1.Item(Col1ItemGroup, mRowIndex).Value <> "" And Dgl1.Columns(Col1ItemGroup).Visible Then
            strCond += " And (I.ItemGroup = '" & Dgl1.Item(Col1ItemGroup, mRowIndex).Tag & "' or I.ItemGroup Is Null) "
        End If


        mQry = " Select I.Code, I.Description, IT.Name as ItemType 
                                     From Item I With (Nolock)
                                     Left Join ItemType IT With (Nolock) On I.ItemType = IT.Code
                                     Where IfNull(I.Status,'Active') = 'Active' And I.V_Type = '" & ItemV_Type.Dimension4 & "' " & strCond & "
                                     Order By I.Description"
        Dgl1.AgHelpDataSet(Col1Dimension4) = AgL.FillData(mQry, AgL.GCn)
    End Sub


    Private Sub DGLRateType_EditingControl_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If Topctrl1.Mode = "Browse" Then Exit Sub


            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name

                Case Col1ItemCategory
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            mQry = "SELECT Code, Description FROM Item Where V_Type='" & ItemV_Type.ItemCategory & "'"
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1ItemGroup
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            mQry = "SELECT Code, Description FROM Item Where V_Type='" & ItemV_Type.ItemGroup & "'"
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1Item
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            FCreateHelpItem(bRowIndex)
                        End If
                    End If
                Case Col1Dimension1
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            FCreateHelpDimension4(bRowIndex)
                        End If
                    End If
                Case Col1Dimension2
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            mQry = "SELECT I.Code, I.Description FROM Item I Where I.V_Type = '" & ItemV_Type.Dimension2 & "' Order By I.Description"
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
                Case Col1Dimension3
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            mQry = "SELECT I.Code, I.Description FROM Item I Where I.V_Type = '" & ItemV_Type.Dimension3 & "' Order By I.Description"
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
                Case Col1Size
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            mQry = "SELECT I.Code, I.Description FROM Item I Where I.V_Type = '" & ItemV_Type.SIZE & "' Order By I.Description"
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1Unit
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            mQry = "SELECT Code, Code as Description FROM Unit "
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Dgl1_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles DglMain.EditingControl_Validating
        Dim DtTemp As DataTable
        Dim mRow As Integer
        Dim mColumn As Integer
        mRow = DglMain.CurrentCell.RowIndex
        mColumn = DglMain.CurrentCell.ColumnIndex
        If mColumn = DglMain.Columns(Col1Value).Index Then
            If DglMain.Item(Col1Mandatory, mRow).Value <> "" Then
                If DglMain(Col1Value, mRow).Value = "" Then
                    MsgBox(DglMain(Col1Head, mRow).Value & " can not be blank.")
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

                Case rowItemCategory
                    mQry = "Select Code, Name From ItemType With (Nolock) Where Code = (Select ItemType From Item Where Code = '" & DglMain.Item(Col1Value, rowItemCategory).Tag & "')"
                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        DglMain.Item(Col1Value, rowItemType).Tag = AgL.XNull(DtTemp.Rows(0)("Code"))
                        DglMain.Item(Col1Value, rowItemType).Value = AgL.XNull(DtTemp.Rows(0)("Name"))
                    End If
                    DglMain.Item(Col1Head, rowItemGroup).Tag = Nothing
                    DglMain.Item(Col1Value, rowItemGroup).Value = ""
                    DglMain.Item(Col1Value, rowItemGroup).Tag = ""
                    DglMain.Item(Col1Head, rowDimension1).Tag = Nothing
                    DglMain.Item(Col1Value, rowDimension1).Value = ""
                    DglMain.Item(Col1Value, rowDimension1).Tag = ""
                    DglMain.Item(Col1Head, rowDimension2).Tag = Nothing
                    DglMain.Item(Col1Value, rowDimension2).Value = ""
                    DglMain.Item(Col1Value, rowDimension2).Tag = ""
                    DglMain.Item(Col1Head, rowDimension3).Tag = Nothing
                    DglMain.Item(Col1Value, rowDimension3).Value = ""
                    DglMain.Item(Col1Value, rowDimension3).Tag = ""
                    DglMain.Item(Col1Head, rowDimension4).Tag = Nothing
                    DglMain.Item(Col1Value, rowDimension4).Value = ""
                    DglMain.Item(Col1Value, rowDimension4).Tag = ""
                    DglMain.Item(Col1Head, rowSize).Tag = Nothing
                    DglMain.Item(Col1Value, rowSize).Value = ""
                    DglMain.Item(Col1Value, rowSize).Tag = ""
            End Select
        End If
        Calculation()
    End Sub

    Private Sub FrmItemBOM_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dim i As Integer

        For i = 0 To DglMain.Rows.Count - 1
            DglMain(Col1Value, i).Value = ""
            DglMain(Col1Value, i).Tag = ""
        Next


        Dgl1.Rows.Clear()
        Dgl1.RowCount = 1

        LblTotalQty.Text = "."
        LblPercentage.Text = "."

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

    Private Sub Dgl1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DglMain.EditingControlShowing, Dgl1.EditingControlShowing
        If FGetSettings(SettingFields.DefaultTextCaseInMasters, SettingType.General) = TextCase.Upper Then
            DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Upper
        ElseIf FGetSettings(SettingFields.DefaultTextCaseInMasters, SettingType.General) = TextCase.Lower Then
            DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Lower
        End If
    End Sub
    Private Sub FrmSaleOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer
        If Topctrl1.Mode = "Browse" Then Exit Sub
        LblTotalQty.Text = 0
        LblPercentage.Text = 0

        For I = 0 To DglMain.RowCount - 1
            If DglMain.Item(Col1SKU, I).Value <> "" And DglMain.Rows(I).Visible Then
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(DglMain.Item(Col1Qty, I).Value)
                LblPercentage.Text = Val(LblPercentage.Text) + Val(DglMain.Item(Col1ConsumptionPer, I).Value)
            End If
        Next
        LblTotalQty.Text = Val(LblTotalQty.Text)
        LblPercentage.Text = Val(LblPercentage.Text)
    End Sub
    Private Sub FrmCuttingConsumptionException_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        If Dgl1.AgHelpDataSet(Col1ItemCategory) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1ItemCategory).Dispose() : Dgl1.AgHelpDataSet(Col1ItemCategory) = Nothing
        If Dgl1.AgHelpDataSet(Col1ItemGroup) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1ItemGroup).Dispose() : Dgl1.AgHelpDataSet(Col1ItemGroup) = Nothing
        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
        If Dgl1.AgHelpDataSet(Col1Dimension1) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Dimension1).Dispose() : Dgl1.AgHelpDataSet(Col1Dimension1) = Nothing
        If Dgl1.AgHelpDataSet(Col1Dimension2) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Dimension2).Dispose() : Dgl1.AgHelpDataSet(Col1Dimension2) = Nothing
        If Dgl1.AgHelpDataSet(Col1Dimension3) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Dimension3).Dispose() : Dgl1.AgHelpDataSet(Col1Dimension3) = Nothing
        If Dgl1.AgHelpDataSet(Col1Dimension4) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Dimension4).Dispose() : Dgl1.AgHelpDataSet(Col1Dimension4) = Nothing
        If Dgl1.AgHelpDataSet(Col1Size) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Size).Dispose() : Dgl1.AgHelpDataSet(Col1Size) = Nothing

        For I As Integer = 0 To DglMain.Rows.Count - 1
            DglMain(Col1Head, I).Tag = Nothing
        Next
    End Sub
    Private Sub DGL2_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub
End Class
