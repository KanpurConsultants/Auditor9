Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain
Imports Customised.ClsMain.ConfigurableFields
Public Class FrmItemGroup
    Inherits AgTemplate.TempMaster

    Dim mQry$
    Public Const ColSNo As String = "SNo"
    Public WithEvents DGLRateType As New AgControls.AgDataGrid
    Public Const Col1RateType As String = FrmItemGroupLineRateType.RateType
    Public Const Col1Margin As String = FrmItemGroupLineRateType.MarginPer
    Public Const Col1DiscountPer As String = FrmItemGroupLineRateType.DiscountPer
    Public Const Col1AdditionalDiscountPer As String = FrmItemGroupLineRateType.AdditionalDiscountPer
    Public Const Col1ExtraDiscountPer As String = FrmItemGroupLineRateType.ExtraDiscountPer
    Public Const Col1AdditionPer As String = FrmItemGroupLineRateType.AdditionPer


    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1LastValue As String = "Last Value"
    Public Const Col1HeadOriginal As String = "Head Original"





    Dim rowItemType As Integer = 0
    Dim rowItemCategory As Integer = 1
    Dim rowDescription As Integer = 2
    Dim rowPrintingDescription As Integer = 3
    Dim rowDefaultDiscountPerSale As Integer = 4
    Dim rowDefaultAdditionalDiscountPerSale As Integer = 5
    Dim rowDefaultAdditionPerSale As Integer = 6
    Dim rowDefaultDiscountPerPurchase As Integer = 7
    Dim rowDefaultAdditionalDiscountPerPurchase As Integer = 8
    Dim rowDefaultMarginPer As Integer = 9
    Dim rowCalcCode As Integer = 10
    Dim rowShowItemGroupInOtherDivision As Integer = 11
    Dim rowShowItemGroupInOtherSite As Integer = 12
    Dim rowSalesRepresentativeCommissionPer As Integer = 13
    Dim rowBarcodeType As Integer = 14
    Dim rowBarcodePattern As Integer = 15
    Dim rowDefaultSupplier As Integer = 16
    Dim rowDepartment As Integer = 17
    Dim rowSite As Integer = 18
    Dim rowItemInvoiceGroup As Integer = 19
    Dim rowParent As Integer = 20
    Dim rowRemark As Integer = 21

    Dim DtItemTypeSetting As DataTable
    Friend WithEvents Pnl1 As Panel
    Dim mItemTypeLastValue As String

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.LblIsSystemDefine = New System.Windows.Forms.Label()
        Me.ChkIsSystemDefine = New System.Windows.Forms.CheckBox()
        Me.PnlRateType = New System.Windows.Forms.Panel()
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
        Me.Topctrl1.Size = New System.Drawing.Size(865, 41)
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 445)
        Me.GroupBox1.Size = New System.Drawing.Size(907, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(14, 449)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(200, 510)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(228, 449)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(401, 449)
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
        Me.GroupBox2.Location = New System.Drawing.Point(704, 449)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(465, 449)
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
        Me.LblIsSystemDefine.Location = New System.Drawing.Point(357, 467)
        Me.LblIsSystemDefine.Name = "LblIsSystemDefine"
        Me.LblIsSystemDefine.Size = New System.Drawing.Size(96, 15)
        Me.LblIsSystemDefine.TabIndex = 1061
        Me.LblIsSystemDefine.Text = "IsSystemDefine"
        Me.LblIsSystemDefine.Visible = False
        '
        'ChkIsSystemDefine
        '
        Me.ChkIsSystemDefine.AutoSize = True
        Me.ChkIsSystemDefine.BackColor = System.Drawing.Color.Transparent
        Me.ChkIsSystemDefine.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkIsSystemDefine.ForeColor = System.Drawing.Color.Red
        Me.ChkIsSystemDefine.Location = New System.Drawing.Point(702, 429)
        Me.ChkIsSystemDefine.Name = "ChkIsSystemDefine"
        Me.ChkIsSystemDefine.Size = New System.Drawing.Size(15, 14)
        Me.ChkIsSystemDefine.TabIndex = 1060
        Me.ChkIsSystemDefine.UseVisualStyleBackColor = False
        '
        'PnlRateType
        '
        Me.PnlRateType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlRateType.Location = New System.Drawing.Point(10, 318)
        Me.PnlRateType.Name = "PnlRateType"
        Me.PnlRateType.Size = New System.Drawing.Size(846, 119)
        Me.PnlRateType.TabIndex = 2
        '
        'Pnl1
        '
        Me.Pnl1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl1.Location = New System.Drawing.Point(10, 49)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(846, 261)
        Me.Pnl1.TabIndex = 1
        '
        'FrmItemGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(865, 493)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.PnlRateType)
        Me.Controls.Add(Me.LblIsSystemDefine)
        Me.Controls.Add(Me.ChkIsSystemDefine)
        Me.MaximizeBox = True
        Me.Name = "FrmItemGroup"
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
    Public WithEvents PnlRateType As Panel
#End Region

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1(Col1Mandatory, I).Value <> "" And Dgl1.Rows(I).Visible Then
                If Dgl1(Col1Value, I).Value.ToString = "" Then
                    MsgBox(Dgl1(Col1Head, I).Value & " can not be blank.")
                    Dgl1.CurrentCell = Dgl1(Col1Value, I)
                    Dgl1.Focus()
                    passed = False
                    Exit Sub
                End If
            End If
        Next

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From Item Where Description='" & Dgl1.Item(Col1Value, rowDescription).Value & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        Else
            mQry = "Select count(*) From Item Where Description='" & Dgl1.Item(Col1Value, rowDescription).Value & "' And Code<>'" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If


        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Value, I).Value = Nothing Then Dgl1.Item(Col1Value, I).Value = ""
            If Dgl1.Item(Col1Value, I).Tag = Nothing Then Dgl1.Item(Col1Value, I).Tag = ""
        Next

        SetLastValues()
    End Sub

    Private Sub SetLastValues()
        Dim I As Integer
        For I = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1LastValue, I).Value = Dgl1(Col1Value, I).Value
            Dgl1(Col1LastValue, I).Tag = Dgl1(Col1Value, I).Tag
        Next
    End Sub
    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        AgL.PubFindQry = "SELECT I.Code As SearchCode, I.Description as Item_Group, IC.Description as ItemCategory, T.Name AS ItemType, Sm.Name As SiteName, I.CalcCode  
                        FROM ItemGroup I  
                        Left Join ItemCategory IC On I.ItemCategory = IC.Code
                        Left Join ItemType T On I.ItemType = T.Code 
                        LEFT JOIN SiteMast Sm On I.Site_Code = Sm.Code "
        AgL.PubFindQryOrdBy = "[Description]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Item"
        MainLineTableCsv = "ItemGroupRateType"
    End Sub

    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, AgL.PubSiteCode, Dgl1(Col1Value, rowItemType).Tag, Dgl1(Col1Value, rowItemCategory).Tag, ItemV_Type.ItemGroup, "", "")
        FGetSettings = mValue
    End Function
    Private Sub Dgl1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles Dgl1.EditingControlShowing, DGLRateType.EditingControlShowing
        If FGetSettings(SettingFields.DefaultTextCaseInMasters, SettingType.General) = TextCase.Upper Then

            DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Upper
        ElseIf FGetSettings(SettingFields.DefaultTextCaseInMasters, SettingType.General) = TextCase.Lower Then
            DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Lower
        End If
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim DsTemp As DataSet
        Dim I As Integer

        mQry = "UPDATE Item 
                Set 
                Description = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDescription).Value) & ", 
                PrintingDescription = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowPrintingDescription).Value) & ", 
                IsSystemDefine = " & Val(IIf(ChkIsSystemDefine.Checked, 1, 0)) & ", 
                ItemCategory = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowItemCategory).Tag) & ", 
                ItemType = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowItemType).Tag) & ", 
                V_Type = 'IG', 
                BarcodeType = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowBarcodeType).Tag) & ", 
                BarcodePattern = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowBarcodePattern).Tag) & ", 
                ShowItemInOtherDivisions = " & IIf(Dgl1.Item(Col1Value, rowShowItemGroupInOtherDivision).Value.ToString.ToUpper = "YES", 1, 0) & ",                 
                ShowItemInOtherSites = " & IIf(Dgl1.Item(Col1Value, rowShowItemGroupInOtherSite).Value.ToString.ToUpper = "YES", 1, 0) & ",                 
                Default_DiscountPerSale = " & Val(Dgl1.Item(Col1Value, rowDefaultDiscountPerSale).Value) & ",                
                Default_AdditionalDiscountPerSale = " & Val(Dgl1.Item(Col1Value, rowDefaultAdditionalDiscountPerSale).Value) & ",                
                Default_AdditionPerSale = " & Val(Dgl1.Item(Col1Value, rowDefaultAdditionPerSale).Value) & ",                
                Default_DiscountPerPurchase = " & Val(Dgl1.Item(Col1Value, rowDefaultDiscountPerPurchase).Value) & ",                
                Default_AdditionalDiscountPerPurchase = " & Val(Dgl1.Item(Col1Value, rowDefaultAdditionalDiscountPerPurchase).Value) & ",                
                Default_MarginPer = " & Val(Dgl1.Item(Col1Value, rowDefaultMarginPer).Value) & ",
                CalcCode = " & Val(Dgl1.Item(Col1Value, rowCalcCode).Value) & ",
                DefaultSupplier = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDefaultSupplier).Tag) & ",                 
                Department = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDepartment).Tag) & ",
                Parent = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowParent).Tag) & ", 
                Remark = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowRemark).Value) & ", 
                SalesRepresentativeCommissionPer = " & Val(Dgl1.Item(Col1Value, rowSalesRepresentativeCommissionPer).Value) & ",
                ItemInvoiceGroup = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowItemInvoiceGroup).Tag) & ",
                OMSId = Null, 
                Site_Code = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowSite).Tag) & "                                  
                Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "Delete from ItemGroupRateType where Code = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        For I = 0 To DGLRateType.Rows.Count - 1
            If DGLRateType.Item(Col1RateType, I).Value <> "" And (Val(DGLRateType.Item(Col1Margin, I).Value) > 0 Or Val(DGLRateType.Item(Col1DiscountPer, I).Value) > 0 Or Val(DGLRateType.Item(Col1AdditionalDiscountPer, I).Value) > 0 Or Val(DGLRateType.Item(Col1ExtraDiscountPer, I).Value) > 0 Or Val(DGLRateType.Item(Col1AdditionPer, I).Value) > 0) Then
                mQry = " Insert Into ItemGroupRateType (
                                                 Code,RateType, 
                                                 Margin, 
                                                 DiscountPer,
                                                 AdditionalDiscountPer,
                                                 ExtraDiscountPer,
                                                 AdditionPer
                                                ) 
                         Values ('" & SearchCode & "', " & AgL.Chk_Text(DGLRateType.Item(Col1RateType, I).Tag) & ", 
                         " & Val(DGLRateType.Item(Col1Margin, I).Value) & ", 
                         " & Val(DGLRateType.Item(Col1DiscountPer, I).Value) & ", 
                         " & Val(DGLRateType.Item(Col1AdditionalDiscountPer, I).Value) & ", 
                         " & Val(DGLRateType.Item(Col1ExtraDiscountPer, I).Value) & ", 
                         " & Val(DGLRateType.Item(Col1AdditionPer, I).Value) & "
                         )"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
        Next
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList


    End Sub
    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = "Select H.*, C.Description as ItemCategoryDesc, Supplier.Name as SupplierName, " &
            " T.Name as ItemTypeName, Department.Description as DepartmentName, IIg.Description As ItemInvoiceGroupDesc, Sm.Name As SiteName, Parent.Description as ParentName  " &
            " From Item H " &
            " Left Join ItemCategory C On H.ItemCategory = C.Code " &
            " Left Join viewHelpSubgroup Supplier On H.DefaultSupplier = Supplier.Code " &
            " Left Join Department On H.Department = Department.Code " &
            " Left Join ItemType T On H.ItemType = T.Code " &
            " Left Join Item IIg On H.ItemInvoiceGroup = IIg.Code " &
            " LEFT JOIN Item Parent With (NoLock) ON Parent.Code = H.Parent " &
            " Left Join SiteMast Sm On H.Site_Code = Sm.Code " &
            " Where H.Code='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))


                Dgl1.Item(Col1Value, rowDescription).Value = AgL.XNull(.Rows(0)("Description"))
                Dgl1.Item(Col1Value, rowPrintingDescription).Value = AgL.XNull(.Rows(0)("PrintingDescription"))
                Dgl1.Item(Col1Value, rowItemCategory).Tag = AgL.XNull(.Rows(0)("ItemCategory"))
                Dgl1.Item(Col1Value, rowItemCategory).Value = AgL.XNull(.Rows(0)("ItemCategoryDesc"))
                Dgl1.Item(Col1Value, rowItemType).Tag = AgL.XNull(.Rows(0)("ItemType"))
                Dgl1.Item(Col1Value, rowItemType).Value = AgL.XNull(.Rows(0)("ItemTypeName"))

                FGetItemTypeSetting()
                Dgl1.Item(Col1Value, rowBarcodeType).Tag = AgL.XNull(.Rows(0)("BarcodeType"))
                Dgl1.Item(Col1Value, rowBarcodeType).Value = AgL.XNull(.Rows(0)("BarcodeType"))
                Dgl1.Item(Col1Value, rowBarcodePattern).Tag = AgL.XNull(.Rows(0)("BarcodePattern"))
                Dgl1.Item(Col1Value, rowBarcodePattern).Value = AgL.XNull(.Rows(0)("BarcodePattern"))
                Dgl1.Item(Col1Value, rowDefaultDiscountPerSale).Value = AgL.VNull(.Rows(0)("Default_DiscountPerSale"))
                Dgl1.Item(Col1Value, rowDefaultAdditionalDiscountPerSale).Value = AgL.VNull(.Rows(0)("Default_AdditionalDiscountPerSale"))
                Dgl1.Item(Col1Value, rowDefaultAdditionPerSale).Value = AgL.VNull(.Rows(0)("Default_AdditionPerSale"))
                Dgl1.Item(Col1Value, rowDefaultDiscountPerPurchase).Value = AgL.VNull(.Rows(0)("Default_DiscountPerPurchase"))
                Dgl1.Item(Col1Value, rowDefaultAdditionalDiscountPerPurchase).Value = AgL.VNull(.Rows(0)("Default_AdditionalDiscountPerPurchase"))
                Dgl1.Item(Col1Value, rowDefaultMarginPer).Value = AgL.VNull(.Rows(0)("Default_MarginPer"))
                Dgl1.Item(Col1Value, rowCalcCode).Value = AgL.VNull(.Rows(0)("CalcCode"))
                Dgl1.Item(Col1Value, rowShowItemGroupInOtherDivision).Value = IIf((AgL.VNull(.Rows(0)("ShowItemInOtherDivisions"))), "Yes", "No")
                Dgl1.Item(Col1Value, rowShowItemGroupInOtherSite).Value = IIf((AgL.VNull(.Rows(0)("ShowItemInOtherSites"))), "Yes", "No")
                Dgl1.Item(Col1Value, rowDefaultSupplier).Tag = AgL.XNull(.Rows(0)("DefaultSupplier"))
                Dgl1.Item(Col1Value, rowDefaultSupplier).Value = AgL.XNull(.Rows(0)("SupplierName"))
                Dgl1.Item(Col1Value, rowDepartment).Tag = AgL.XNull(.Rows(0)("Department"))
                Dgl1.Item(Col1Value, rowDepartment).Value = AgL.XNull(.Rows(0)("DepartmentName"))
                Dgl1.Item(Col1Value, rowItemInvoiceGroup).Tag = AgL.XNull(.Rows(0)("ItemInvoiceGroup"))
                Dgl1.Item(Col1Value, rowItemInvoiceGroup).Value = AgL.XNull(.Rows(0)("ItemInvoiceGroupDesc"))
                Dgl1.Item(Col1Value, rowParent).Tag = AgL.XNull(.Rows(0)("Parent"))
                Dgl1.Item(Col1Value, rowParent).Value = AgL.XNull(.Rows(0)("ParentName"))
                Dgl1.Item(Col1Value, rowRemark).Value = AgL.XNull(.Rows(0)("Remark"))
                Dgl1.Item(Col1Value, rowSite).Tag = AgL.XNull(.Rows(0)("Site_Code"))
                Dgl1.Item(Col1Value, rowSite).Value = AgL.XNull(.Rows(0)("SiteName"))
                Dgl1.Item(Col1Value, rowSalesRepresentativeCommissionPer).Value = AgL.VNull(DsTemp.Tables(0).Rows(0)("SalesRepresentativeCommissionPer"))

                ChkIsSystemDefine.Checked = AgL.VNull(.Rows(0)("IsSystemDefine"))
                LblIsSystemDefine.Text = IIf(AgL.VNull(.Rows(0)("IsSystemDefine")) = 0, "User Define", "System Define")
                ChkIsSystemDefine.Enabled = False
            End If
        End With


        Dim I As Integer
        mQry = " Select  H.Code, H.Description, L.*
                        From RateType H 
                        Left join ItemGroupRateType L on L.RateType = H.Code And L.Code='" & SearchCode & "' 
                        Order By H.Sr "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            DGLRateType.RowCount = 1
            DGLRateType.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    DGLRateType.Rows.Add()
                    DGLRateType.Item(ColSNo, I).Value = DGLRateType.Rows.Count - 1
                    DGLRateType.Item(Col1RateType, I).Tag = AgL.XNull(.Rows(I)("Code"))
                    DGLRateType.Item(Col1RateType, I).Value = AgL.XNull(.Rows(I)("Description"))
                    DGLRateType.Item(Col1Margin, I).Value = Format(AgL.VNull(.Rows(I)("Margin")), "0.00")
                    DGLRateType.Item(Col1DiscountPer, I).Value = Format(AgL.VNull(.Rows(I)("DiscountPer")), "0.00")
                    DGLRateType.Item(Col1AdditionalDiscountPer, I).Value = Format(AgL.VNull(.Rows(I)("AdditionalDiscountPer")), "0.00")
                    DGLRateType.Item(Col1ExtraDiscountPer, I).Value = Format(AgL.VNull(.Rows(I)("ExtraDiscountPer")), "0.00")
                    DGLRateType.Item(Col1AdditionPer, I).Value = Format(AgL.VNull(.Rows(I)("AdditionPer")), "0.00")
                Next I
                DGLRateType.Visible = True
            Else
                DGLRateType.Visible = False
            End If
        End With


        SetLastValues()
        FrmItemGroup_BaseFunction_DispText()
    End Sub

    'Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
    '    TxtDescription.Focus()
    'End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        Dgl1.CurrentCell = Dgl1(Col1Value, rowDescription)
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
        mQry = "Select I.Code As SearchCode " &
                " From ItemGroup I " &
                " Order By I.Description "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmItemGroup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 325, 885)
        FManageSystemDefine()
    End Sub

    Private Sub TxtItemCategory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
        End If
    End Sub

    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim DtTemp As DataTable = Nothing
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.NAME
                'Case TxtItemCategory.Name
                'If TxtItemCategory.Visible = True Then
                '    If TxtItemCategory.AgSelectedValue <> "" Then
                '        TxtItemType.AgSelectedValue = AgL.FillData("Select ItemType From ItemCategory Where Code = '" & TxtItemCategory.AgSelectedValue & "' ", AgL.GCn).tables(0).rows(0)(0)
                '        'If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, "Save") = MsgBoxResult.Yes Then
                '        '    Topctrl1.FButtonClick(13)
                '        'End If
                '    End If
                'End If

                'Case TxtItemType.Name
                '    FGetItemTypeSetting()

                'mQry = "Select * From ItemTypeSetting Where ItemType = '" & TxtItemType.Tag & "' And Div_Code = '" & TxtDivision.Tag & "' "
                'DtItemTypeSetting = AgL.FillData(mQry, AgL.GCn).tables(0)
                'If DtItemTypeSetting.Rows.Count = 0 Then
                '    mQry = "Select * From ItemTypeSetting Where ItemType = '" & TxtItemType.Tag & "' And Div_Code Is Null "
                '    DtItemTypeSetting = AgL.FillData(mQry, AgL.GCn).tables(0)
                '    If DtItemTypeSetting.Rows.Count = 0 Then
                '        mQry = "Select * From ItemTypeSetting Where ItemType Is Null And Div_Code Is Null "
                '        DtItemTypeSetting = AgL.FillData(mQry, AgL.GCn).tables(0)
                '        If DtItemTypeSetting.Rows.Count = 0 Then
                '            MsgBox("Settings not found for selected Item Type.")
                '            sender.text = ""
                '            sender.tag = ""
                '        End If
                '    End If
                'End If



                'If TxtItemType.Visible = True Then
                '    If TxtItemType.AgSelectedValue <> "" Then
                '        If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, "Save") = MsgBoxResult.Yes Then
                '            Topctrl1.FButtonClick(13)
                '        End If
                '    End If
                'End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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
        Passed = Not FGetRelationalData()

        If ClsMain.IsEntryLockedWithLockText("Item", "Code", mSearchCode) = True Then
            Passed = False
            Exit Sub
        End If
    End Sub


    Private Function FGetRelationalData() As Boolean
        Try
            mQry = " Select Count(*) From Item Where ItemGroup = '" & mSearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                MsgBox(" Data Exists For ItemGroup " & Dgl1(Col1Value, rowDescription).Value & " In Item Master. Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData")
            FGetRelationalData = True
        End Try
    End Function

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

    Private Sub FrmItemGroup_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        Dim DsTemp As DataSet
        ChkIsSystemDefine.Checked = False
        FManageSystemDefine()

        Dgl1.Item(Col1Value, rowDefaultMarginPer).Value = AgL.VNull(AgL.PubDtEnviro.Rows(0)("Default_ProfitPer"))

        Dim I As Integer
        mQry = " Select  H.Code, H.Description, H.Margin, H.Discount from RateType H Order By H.Sr "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            DGLRateType.RowCount = 1
            DGLRateType.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    DGLRateType.Rows.Add()
                    DGLRateType.Item(ColSNo, I).Value = DGLRateType.Rows.Count - 1
                    DGLRateType.Item(Col1RateType, I).Tag = AgL.XNull(.Rows(I)("Code"))
                    DGLRateType.Item(Col1RateType, I).Value = AgL.XNull(.Rows(I)("Description"))
                    DGLRateType.Item(Col1Margin, I).Value = Format(AgL.VNull(.Rows(I)("Margin")), "0.00")
                    DGLRateType.Item(Col1DiscountPer, I).Value = Format(AgL.VNull(.Rows(I)("Discount")), "0.00")
                Next I
                DGLRateType.Visible = True
            Else
                DGLRateType.Visible = False
            End If
        End With

        If Dgl1(Col1LastValue, rowItemType).Value = "" Then
            Dgl1.Item(Col1Value, rowItemType).Tag = ItemTypeCode.TradingProduct
            Dgl1.Item(Col1Value, rowItemType).Value = "Trading Product"
        Else
            Dgl1.Item(Col1Value, rowItemType).Value = Dgl1.Item(Col1LastValue, rowItemType).Value
            Dgl1.Item(Col1Value, rowItemType).Tag = Dgl1.Item(Col1LastValue, rowItemType).Tag
        End If



        FGetItemTypeSetting()
        If Dgl1.Visible = True Then
            If AgL.VNull(DtItemTypeSetting.Rows(0)("IsItemGroupLinkedWithItemCategory")) Then
                Dgl1.CurrentCell = Dgl1(Col1Value, rowItemCategory) 'Dgl1.FirstDisplayedCell
                Dgl1.Focus()
            Else
                Dgl1.CurrentCell = Dgl1(Col1Value, rowDescription) 'Dgl1.FirstDisplayedCell
                Dgl1.Focus()
            End If
        End If
    End Sub

    Private Sub FrmItemGroup_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dim I As Integer
        DGLRateType.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DGLRateType, ColSNo, 40, 5, ColSNo, False, True, False)
            .AddAgTextColumn(DGLRateType, Col1RateType, 220, 0, Col1RateType, True, True, False)
            .AddAgNumberColumn(DGLRateType, Col1Margin, 130, 3, 2, False, Col1Margin, True, False, True)
            .AddAgNumberColumn(DGLRateType, Col1DiscountPer, 130, 2, 2, False, Col1DiscountPer, False, False, True)
            .AddAgNumberColumn(DGLRateType, Col1AdditionalDiscountPer, 130, 2, 2, False, Col1AdditionalDiscountPer, False, False, True)
            .AddAgNumberColumn(DGLRateType, Col1ExtraDiscountPer, 130, 2, 2, False, Col1ExtraDiscountPer, False, False, True)
            .AddAgNumberColumn(DGLRateType, Col1AdditionPer, 130, 2, 2, False, Col1AdditionPer, False, False, True)

        End With
        AgL.AddAgDataGrid(DGLRateType, PnlRateType)
        DGLRateType.EnableHeadersVisualStyles = False
        DGLRateType.AgSkipReadOnlyColumns = True
        DGLRateType.RowHeadersVisible = False
        DGLRateType.AllowUserToAddRows = False
        DGLRateType.BackgroundColor = Me.BackColor
        AgL.GridDesign(DGLRateType)
        DGLRateType.Anchor = AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom


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

        Dgl1.Rows.Add(22)

        Dgl1.Item(Col1Head, rowItemType).Value = FrmItemGroupHeaderDgl1.ItemType
        Dgl1.Item(Col1Head, rowItemCategory).Value = FrmItemGroupHeaderDgl1.ItemCategory
        Dgl1.Item(Col1Head, rowDescription).Value = FrmItemGroupHeaderDgl1.ItemGroup
        Dgl1.Item(Col1Head, rowPrintingDescription).Value = FrmItemGroupHeaderDgl1.PrintingDescription
        Dgl1.Item(Col1Head, rowDefaultDiscountPerSale).Value = FrmItemGroupHeaderDgl1.DefaultDiscountPerSale
        Dgl1.Item(Col1Head, rowDefaultAdditionalDiscountPerSale).Value = FrmItemGroupHeaderDgl1.DefaultAdditionalDiscountPerSale
        Dgl1.Item(Col1Head, rowDefaultAdditionPerSale).Value = FrmItemGroupHeaderDgl1.DefaultAdditionPerSale
        Dgl1.Item(Col1Head, rowDefaultDiscountPerPurchase).Value = FrmItemGroupHeaderDgl1.DefaultDiscountPerPurchase
        Dgl1.Item(Col1Head, rowDefaultAdditionalDiscountPerPurchase).Value = FrmItemGroupHeaderDgl1.DefaultAdditionalDiscountPerPurchase
        Dgl1.Item(Col1Head, rowDefaultMarginPer).Value = FrmItemGroupHeaderDgl1.DefaultMarginPer
        Dgl1.Item(Col1Head, rowCalcCode).Value = FrmItemGroupHeaderDgl1.CalcCode
        Dgl1.Item(Col1Head, rowBarcodeType).Value = FrmItemGroupHeaderDgl1.BarcodeType
        Dgl1.Item(Col1Head, rowBarcodePattern).Value = FrmItemGroupHeaderDgl1.BarcodePattern
        Dgl1.Item(Col1Head, rowShowItemGroupInOtherDivision).Value = FrmItemGroupHeaderDgl1.ShowItemGroupInOtherDivisions
        Dgl1.Item(Col1Head, rowShowItemGroupInOtherSite).Value = FrmItemGroupHeaderDgl1.ShowItemGroupInOtherSites
        Dgl1.Item(Col1Head, rowDefaultSupplier).Value = FrmItemGroupHeaderDgl1.DefaultSupplier
        Dgl1.Item(Col1Head, rowDepartment).Value = FrmItemGroupHeaderDgl1.Department
        Dgl1.Item(Col1Head, rowItemInvoiceGroup).Value = FrmItemGroupHeaderDgl1.ItemInvoiceGroup
        Dgl1.Item(Col1Head, rowParent).Value = FrmItemGroupHeaderDgl1.Parent
        Dgl1.Item(Col1Head, rowRemark).Value = FrmItemGroupHeaderDgl1.Remark
        Dgl1.Item(Col1Head, rowSalesRepresentativeCommissionPer).Value = FrmItemGroupHeaderDgl1.SalesRepresentativeCommissionPer
        Dgl1.Item(Col1Head, rowSite).Value = FrmItemGroupHeaderDgl1.Site


        For I = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1HeadOriginal, I).Value = Dgl1(Col1Head, I).Value
        Next
    End Sub



    Private Sub DglRateType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGLRateType.KeyDown
        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub

        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If

        If e.Control Or e.Shift Or e.Alt Then Exit Sub

        If e.KeyCode = Keys.Enter Then
            If DGLRateType.CurrentCell.ColumnIndex = DGLRateType.Columns(Col1Margin).Index Then
                If DGLRateType.Item(DGLRateType.CurrentCell.ColumnIndex, DGLRateType.CurrentCell.RowIndex).Value Is Nothing Then DGLRateType.Item(DGLRateType.CurrentCell.ColumnIndex, DGLRateType.CurrentCell.RowIndex).Value = ""
                If DGLRateType.Item(DGLRateType.CurrentCell.ColumnIndex, DGLRateType.CurrentCell.RowIndex).Value = "" Then
                    If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, "Save") = MsgBoxResult.Yes Then
                        Topctrl1.FButtonClick(13)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub FGetItemTypeSetting()
        If mItemTypeLastValue <> Dgl1(Col1Value, rowItemType).Tag And Dgl1(Col1Value, rowItemType).Tag <> "" Then
            mItemTypeLastValue = Dgl1(Col1Value, rowItemType).Tag
            mQry = "Select * From ItemTypeSetting Where ItemType = '" & Dgl1(Col1Value, rowItemType).Tag & "' And Div_Code = '" & TxtDivision.Tag & "' "
            DtItemTypeSetting = AgL.FillData(mQry, AgL.GCn).tables(0)
            If DtItemTypeSetting.Rows.Count = 0 Then
                mQry = "Select * From ItemTypeSetting Where ItemType = '" & Dgl1(Col1Value, rowItemType).Tag & "' And Div_Code Is Null "
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


    Private Sub ApplyItemTypeSetting(ItemType As String)
        Dim mQry As String
        Dim DtTemp As DataTable
        Dim I As Integer, J As Integer
        Dim mDgl1RowCount As Integer
        Dim mDglRateTypeColumnCount As Integer
        Try


            For I = 0 To Dgl1.Rows.Count - 1
                Dgl1.Rows(I).Visible = False
            Next


            mQry = "Select H.*
                    from EntryHeaderUISetting H                   
                    Where EntryName='FrmItemGroup' And NCat = '" & ItemType & "' And GridName ='Dgl1' "
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



            For I = 0 To DGLRateType.Columns.Count - 1
                DGLRateType.Columns(I).Visible = False
            Next


            mQry = "Select H.*
                    from EntryLineUISetting H                    
                    Where EntryName='FrmItemGroup' And NCat = '" & ItemType & "' And GridName ='DglRateType' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 0 To DGLRateType.Columns.Count - 1
                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = DGLRateType.Columns(J).Name Then
                            DGLRateType.Columns(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
                            If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDglRateTypeColumnCount += 1
                            If Not IsDBNull(DtTemp.Rows(I)("DisplayIndex")) Then
                                DGLRateType.Columns(J).DisplayIndex = AgL.VNull(DtTemp.Rows(I)("DisplayIndex"))
                            End If
                            If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
                                DGLRateType.Columns(J).HeaderText = AgL.XNull(DtTemp.Rows(I)("Caption"))
                            End If
                            'Dgl1.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
                        End If
                    Next
                Next
            End If
            If mDglRateTypeColumnCount = 0 Then DGLRateType.Visible = False Else DGLRateType.Visible = True


            If AgL.VNull(DtItemTypeSetting.Rows(0)("IsItemGroupLinkedWithItemCategory")) Then
                Dgl1.Rows(rowItemCategory).Visible = True
                Dgl1(Col1Mandatory, rowItemCategory).Value = "Ä"
                Dgl1(Col1Value, rowItemCategory).ReadOnly = False
            Else
                Dgl1.Rows(rowItemCategory).Visible = False
                Dgl1(Col1Mandatory, rowItemCategory).Value = ""
                Dgl1(Col1Value, rowItemCategory).ReadOnly = True
                Dgl1.Item(Col1Value, rowItemCategory).Value = ""
                Dgl1.Item(Col1Value, rowItemCategory).Tag = ""
                Dgl1.Item(Col1Head, rowItemCategory).Tag = Nothing
            End If
            'Dgl1.Rows(rowBarcodeType).Visible = AgL.VNull(DtItemTypeSetting.Rows(0)("IsApplicable_Barcode"))
            'Dgl1.Rows(rowBarcodePattern).Visible = AgL.VNull(DtItemTypeSetting.Rows(0)("IsApplicable_Barcode"))

        Catch ex As Exception
            MsgBox(ex.Message & " [ApplySubgroupTypeSetting]")
        End Try
    End Sub


    Private Sub FrmItemGroup_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        If DtItemTypeSetting Is Nothing Then Exit Sub
        ChkIsSystemDefine.Enabled = False
        'DGLRateType.Visible = False
        'If DtItemTypeSetting IsNot Nothing Then
        '    If DtItemTypeSetting.Rows(0)("IsItemGroupLinkedWithItemCategory") Then
        '        Dgl1(Col1Value, rowItemCategory).ReadOnly = IIf(Topctrl1.Mode <> "Browse", True, False)
        '    Else
        '        Dgl1(Col1Value, rowItemCategory).ReadOnly = False
        '    End If
        'Else
        '    Dgl1(Col1Value, rowItemCategory).ReadOnly = False
        'End If




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
                Case rowDefaultDiscountPerSale, rowDefaultAdditionalDiscountPerSale, rowDefaultAdditionPerSale, rowDefaultDiscountPerPurchase, rowDefaultAdditionalDiscountPerPurchase
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberLeftPlaces = 2
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberRightPlaces = 2
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
                'Case rowDiscountPatternSale
                '    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                '        mQry = ClsMain.GetStringsFromClassConstants(GetType(DiscountCalculationPattern))
                '        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                '    End If
                '    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                '        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                '    End If

                Case rowItemType
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " Select Code, Name From ItemType "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowItemCategory
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " Select Code, Description From ItemCategory "
                        mQry = "
                                    SELECT Code, Description 
                                    FROM ItemCategory IC 
                                    Where IfNull(IC.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' 
                                    And ItemType='" & Dgl1(Col1Value, rowItemType).Tag & "'

                                    "

                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If
                Case rowDescription
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "Select Code, Description As Name " &
                                " From ItemGroup " &
                                " Order By Description"
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If
                    CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True




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

                Case rowShowItemGroupInOtherDivision, rowShowItemGroupInOtherSite
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT 'Yes' as Code, 'Yes' as Name Union All Select 'No' as Code, 'No' as Name "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowDefaultSupplier
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT H.Code, H.Name From viewHelpSubgroup H 
                                LEFT JOIN SubGroupType Sgt On H.SubgroupType = Sgt.SubgroupType
                                Where IfNull(Sgt.Parent,Sgt.SubgroupType) = '" & SubgroupType.Supplier & "' "
                        If AgL.XNull(DtItemTypeSetting.Rows(0)("FilterInclude_SupplierTreeNodeType")).ToString.ToUpper = "+ROOT" Then
                            mQry += " And H.Parent Is Null "
                        End If
                        mQry += " Order By Name"

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

                Case rowItemInvoiceGroup
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Description as Name  FROM Item where V_Type = '" & ItemV_Type.ItemInvoiceGroup & "' And Status='Active' Order By Description"
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowParent
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Description as Name  FROM ItemGroup Where Status='Active' Order By Description"
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If

                Case rowSite
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Name  FROM SiteMast "
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

    Private Sub DGLRateType_EditingControl_KeyDown(sender As Object, e As KeyEventArgs) Handles DGLRateType.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = DGLRateType.CurrentCell.RowIndex
            bColumnIndex = DGLRateType.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If Topctrl1.Mode = "Browse" Then Exit Sub


            Select Case DGLRateType.Columns(DGLRateType.CurrentCell.ColumnIndex).Name
                'Case Col1DiscountPattern
                '    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                '        If DGLRateType.AgHelpDataSet(bColumnIndex) Is Nothing Then
                '            mQry = ClsMain.GetStringsFromClassConstants(GetType(DiscountCalculationPattern))
                '            DGLRateType.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                '        End If
                '    End If
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
                    If Me.MdiParent.ActiveMdiChild.Name = Me.Name Then
                        MsgBox(Dgl1(Col1Head, mRow).Value & " can not be blank.")
                        e.Cancel = True
                    End If
                    Exit Sub
                End If
            End If


            Select Case mRow
                Case rowItemType
                    FGetItemTypeSetting()

                    If AgL.VNull(DtItemTypeSetting.Rows(0)("IsItemGroupLinkedWithItemCategory")) Then
                        Dgl1.CurrentCell = Dgl1(Col1Value, rowItemCategory) 'Dgl1.FirstDisplayedCell
                        Dgl1.Focus()
                    Else
                        Dgl1.CurrentCell = Dgl1(Col1Value, rowDescription) 'Dgl1.FirstDisplayedCell
                        Dgl1.Focus()

                    End If

                    Dgl1(Col1Head, rowItemCategory).Tag = Nothing
            End Select
        End If
    End Sub

    Private Sub FrmItemGroup_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dim i As Integer

        For i = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1Value, i).Value = ""
            Dgl1(Col1Value, i).Tag = ""
        Next

        Dgl1(Col1Head, rowItemCategory).Tag = Nothing
    End Sub

    Private Sub FrmItemGroup_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Dim i As Integer

        For i = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1Head, i).Tag = Nothing
        Next
    End Sub

    Private Sub FrmItemGroup_BaseEvent_ApproveDeletion_InTrans(SearchCode As String, Conn As Object, Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = "Delete from ItemGroupPerson Where ItemGroup = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
End Class
