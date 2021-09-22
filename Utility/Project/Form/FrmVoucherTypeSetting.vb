Imports System.Data.SQLite
Public Class FrmVoucherTypeSettings
    Inherits AgTemplate.TempMaster
    Public Const Yes As String = "Yes"
    Public Const No As String = "No"
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents TxtSiteCode As AgControls.AgTextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents TxtIsEditable_Rate As AgControls.AgTextBox
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_Amount As AgControls.AgTextBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents TxtIsEditable_Amount As AgControls.AgTextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents TxtIsMandatory_Rate As AgControls.AgTextBox
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents TC1 As System.Windows.Forms.TabControl
    Friend WithEvents TP1 As System.Windows.Forms.TabPage
    Friend WithEvents TxtDEFAULT_Godown As AgControls.AgTextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents TxtFilterExclude_SubGroupDivision As AgControls.AgTextBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents TxtFilterExclude_SubGroupSite As AgControls.AgTextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents TxtFilterInclude_ContraV_Type As AgControls.AgTextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents TxtFilterInclude_Item As AgControls.AgTextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents TxtFilterExclude_Item As AgControls.AgTextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents TxtFilterInclude_SubgroupSite As AgControls.AgTextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents TxtFilterInclude_SubgroupDivision As AgControls.AgTextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents TxtFilterInclude_ItemSite As AgControls.AgTextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents TxtFilterInclude_Process As AgControls.AgTextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents TxtFilterInclude_AcGroup As AgControls.AgTextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents TxtFilterExclude_AcGroup As AgControls.AgTextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents TxtFilterInclude_ItemType As AgControls.AgTextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents TxtFilterInclude_ItemGroup As AgControls.AgTextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents TxtFilterExclude_ItemGroup As AgControls.AgTextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents TxtFilterInclude_ItemDivision As AgControls.AgTextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_Rate As AgControls.AgTextBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_ProcessLine As AgControls.AgTextBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents TxtIsEditable_ProcessLine As AgControls.AgTextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_ItemCode As AgControls.AgTextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents TxtIsEditable_ItemCode As AgControls.AgTextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents TxtIsEditable_ItemName As AgControls.AgTextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_ItemUID As AgControls.AgTextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents TxtIsPostedInStockProcess As AgControls.AgTextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents TxtIsPostedInStock As AgControls.AgTextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_BaleNo As AgControls.AgTextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_LotNo As AgControls.AgTextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_Process As AgControls.AgTextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_ProdOrder As AgControls.AgTextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TxtIsEditable_MeasureUnit As AgControls.AgTextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_MeasureUnit As AgControls.AgTextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TxtIsEditable_Measure As AgControls.AgTextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_Measure As AgControls.AgTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TxtIsEditable_MeasurePerPcs As AgControls.AgTextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_MeasurePerPcs As AgControls.AgTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtIsMandatory_SubCode As AgControls.AgTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TxtIsEditableSubCode As AgControls.AgTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents LblEntryTypeReq As System.Windows.Forms.Label
    Friend WithEvents TxtVoucherType As AgControls.AgTextBox
    Friend WithEvents LblEntryType As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_Specification As AgControls.AgTextBox
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_Supplier As AgControls.AgTextBox
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_RateType As AgControls.AgTextBox
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_BillingType As AgControls.AgTextBox
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents TxtDefault_SubCode As AgControls.AgTextBox
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents TxtFilterInclude_SubGroup As AgControls.AgTextBox
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_Unit As AgControls.AgTextBox
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_PartySpecification As AgControls.AgTextBox
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_RejMeasure As AgControls.AgTextBox
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_RejQty As AgControls.AgTextBox
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_Qty As AgControls.AgTextBox
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents TP2 As System.Windows.Forms.TabPage
    Friend WithEvents TxtIsVisible_PartyUPC As AgControls.AgTextBox
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_ShippingDetail As AgControls.AgTextBox
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_DeliveryDetail As AgControls.AgTextBox
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_Deal As AgControls.AgTextBox
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_MRP As AgControls.AgTextBox
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_FreeQty As AgControls.AgTextBox
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_PurchIndent As AgControls.AgTextBox
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_PurchQuotation As AgControls.AgTextBox
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents TxtShowRecordCount As AgControls.AgTextBox
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents TxtShowLastPoRates As AgControls.AgTextBox
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents TxtIsPostConsumption As AgControls.AgTextBox
    Friend WithEvents Label80 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_CostCenter As AgControls.AgTextBox
    Friend WithEvents Label79 As System.Windows.Forms.Label
    Friend WithEvents TxtIsPostedInStockVirtual As AgControls.AgTextBox
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents TXTIsVisible_ExpiryDate As AgControls.AgTextBox
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_SaleRate As AgControls.AgTextBox
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents TxtIsEditable_ProfitMarginPer As AgControls.AgTextBox
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_ProfitMarginPer As AgControls.AgTextBox
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_PartySKU As AgControls.AgTextBox
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_FreeMeasure As AgControls.AgTextBox
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Protected WithEvents BtnCopyToAllDiv As System.Windows.Forms.Button
    Protected WithEvents BtnCopyToAllSite As System.Windows.Forms.Button
    Protected WithEvents BtnFillDefaultValue As System.Windows.Forms.Button
    Friend WithEvents TxtIsPostInSaleInvoice As AgControls.AgTextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents TxtIsEditable_Qty As AgControls.AgTextBox
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents TxtIsMandatory_Approval As AgControls.AgTextBox
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents TxtFilterExclude_Subgroup As AgControls.AgTextBox
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents TxtIndustryType As AgControls.AgTextBox
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Protected WithEvents BtnInsertV_Type As System.Windows.Forms.Button
    Friend WithEvents TP3 As System.Windows.Forms.TabPage
    Friend WithEvents TxtTransactionDelete_AllowedDays As AgControls.AgTextBox
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents TxtTransactionEdit_AllowedDays As AgControls.AgTextBox
    Friend WithEvents Label82 As System.Windows.Forms.Label
    Friend WithEvents TxtTransactionEdit_AllowedDaysAdmin As AgControls.AgTextBox
    Friend WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents TxtTransactionDelete_AllowedDaysAdmin As AgControls.AgTextBox
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents TxtIsVisible_TransactionHistory As AgControls.AgTextBox
    Friend WithEvents Label83 As System.Windows.Forms.Label
    Friend WithEvents TP4 As System.Windows.Forms.TabPage
    Friend WithEvents TxtTransactionHistory_ColumnWidthCsv As AgControls.AgTextBox
    Friend WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents TxtTransactionHistory_SqlQuery As AgControls.AgTextBox
    Friend WithEvents Label85 As System.Windows.Forms.Label
    Dim mQry$

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If AgL.RequiredField(TxtVoucherType, LblEntryType.Text) Then passed = False : Exit Sub

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From Voucher_Type_Settings Where V_Type='" & TxtVoucherType.AgSelectedValue & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code ='" & AgL.PubSiteCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then passed = False : MsgBox("Entry Type Already Exists")
        Else
            mQry = "Select count(*) From Voucher_Type_Settings Where V_Type='" & TxtVoucherType.AgSelectedValue & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code ='" & AgL.PubSiteCode & "' And Code <> '" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then passed = False : MsgBox("Entry Type Already Exists")
        End If
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        mConStr = " WHERE 1=1 AND H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code = '" & AgL.PubSiteCode & "' AND IfNull(H.IsDeleted,0) = 0"
        AgL.PubFindQry = " SELECT H.Code, H.V_Type, Vt.Description AS [V Type], SM.Name AS SiteName, D.Div_Name " &
                        " FROM Voucher_Type_Settings H  " &
                        " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type  " &
                        " LEFT JOIN SiteMast SM ON SM.Code = H.Site_Code  " &
                        " LEFT JOIN Division D ON D.Div_Code = H.Div_Code " &
                        " " & mConStr & " "
        AgL.PubFindQryOrdBy = "[V Type]"
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Voucher_Type_Settings"
        LogTableName = "Voucher_Type_Settings_Log"
    End Sub

    Private Function RetBitValue(ByVal ObjTextBox As TextBox) As Integer
        RetBitValue = IIf(AgL.StrCmp(ObjTextBox.Text, "Yes"), 1, 0)
    End Function

    Private Function RetStringValue(ByVal ObjTextBox As TextBox) As String
        RetStringValue = ""
        If ObjTextBox.Tag <> "" Then
            RetStringValue = Replace(ObjTextBox.Tag, "'", "|")
        End If
    End Function

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        mQry = " UPDATE Voucher_Type_Settings " &
                " SET V_Type = " & AgL.Chk_Text(TxtVoucherType.Tag) & " , " &
                " IsEditable_SubCode = " & RetBitValue(TxtIsEditableSubCode) & ", " &
                " IsMandatory_SubCode = " & RetBitValue(TxtIsMandatory_SubCode) & ", " &
                " IsVisible_MeasurePerPcs = " & RetBitValue(TxtIsVisible_MeasurePerPcs) & ", " &
                " IsEditable_MeasurePerPcs = " & RetBitValue(TxtIsEditable_MeasurePerPcs) & ", " &
                " IsVisible_Measure = " & RetBitValue(TxtIsVisible_Measure) & ", " &
                " IsEditable_Measure = " & RetBitValue(TxtIsEditable_Measure) & ", " &
                " IsVisible_MeasureUnit = " & RetBitValue(TxtIsVisible_MeasureUnit) & ", " &
                " IsEditable_MeasureUnit = " & RetBitValue(TxtIsEditable_MeasureUnit) & ", " &
                " IsVisible_ProdOrder = " & RetBitValue(TxtIsVisible_ProdOrder) & ", " &
                " IsVisible_Process = " & RetBitValue(TxtIsVisible_Process) & ", " &
                " IsVisible_LotNo = " & RetBitValue(TxtIsVisible_LotNo) & ", " &
                " IsVisible_BaleNo = " & RetBitValue(TxtIsVisible_BaleNo) & ", " &
                " IsPostedInStock = " & RetBitValue(TxtIsPostedInStock) & ", " &
                " IsPostedInStockProcess = " & RetBitValue(TxtIsPostedInStockProcess) & ", " &
                " IsVisible_ItemUID = " & RetBitValue(TxtIsVisible_ItemUID) & ", " &
                " IsVisible_ItemCode = " & RetBitValue(TxtIsVisible_ItemCode) & ", " &
                " IsEditable_ItemCode = " & RetBitValue(TxtIsEditable_ItemCode) & ", " &
                " IsEditable_ItemName = " & RetBitValue(TxtIsEditable_ItemName) & ", " &
                " FilterInclude_Process = '" & RetStringValue(TxtFilterInclude_Process) & "', " &
                " FilterInclude_AcGroup = '" & RetStringValue(TxtFilterInclude_AcGroup) & "', " &
                " FilterExclude_AcGroup = '" & RetStringValue(TxtFilterExclude_AcGroup) & "', " &
                " FilterInclude_ItemType = '" & RetStringValue(TxtFilterInclude_ItemType) & "', " &
                " FilterInclude_ItemGroup = '" & RetStringValue(TxtFilterInclude_ItemGroup) & "', " &
                " FilterExclude_ItemGroup = '" & RetStringValue(TxtFilterExclude_ItemGroup) & "', " &
                " EntryBy = '" & AgL.PubUserName & "', " &
                " EntryDate = '" & AgL.PubLoginDate & "', " &
                " ApproveBy =  '" & AgL.PubUserName & "', " &
                " ApproveDate = '" & AgL.PubLoginDate & "', " &
                " Site_Code = '" & TxtSiteCode.Tag & "', " &
                " Div_Code = '" & TxtDivision.Tag & "', " &
                " FilterInclude_ItemDivision = '" & RetStringValue(TxtFilterInclude_ItemDivision) & "', " &
                " FilterInclude_ItemSite = '" & RetStringValue(TxtFilterInclude_ItemSite) & "', " &
                " FilterInclude_SubgroupDivision = '" & RetStringValue(TxtFilterInclude_SubgroupDivision) & "', " &
                " FilterInclude_SubgroupSite = '" & RetStringValue(TxtFilterInclude_SubgroupSite) & "', " &
                " FilterExclude_Item = '" & RetStringValue(TxtFilterExclude_Item) & "', " &
                " FilterInclude_Item = '" & RetStringValue(TxtFilterInclude_Item) & "', " &
                " IsVisible_ProcessLine =  " & RetBitValue(TxtIsVisible_ProcessLine) & ", " &
                " IsEditable_ProcessLine =  " & RetBitValue(TxtIsEditable_ProcessLine) & ", " &
                " DEFAULT_Godown = '" & TxtDEFAULT_Godown.Tag & "', " &
                " IsVisible_Rate =  " & RetBitValue(TxtIsVisible_Rate) & ", " &
                " IsEditable_Rate =  " & RetBitValue(TxtIsEditable_Rate) & ", " &
                " IsVisible_Amount =  " & RetBitValue(TxtIsVisible_Amount) & ", " &
                " IsEditable_Amount =  " & RetBitValue(TxtIsEditable_Amount) & ", " &
                " IsMandatory_Rate =  " & RetBitValue(TxtIsMandatory_Rate) & ", " &
                " FilterExclude_SubGroupDivision = '" & RetStringValue(TxtFilterExclude_SubGroupDivision) & "', " &
                " FilterExclude_SubGroupSite = '" & RetStringValue(TxtFilterExclude_SubGroupSite) & "', " &
                " IsVisible_Specification = " & RetBitValue(TxtIsVisible_Specification) & ", " &
                " IsVisible_BillingType = " & RetBitValue(TxtIsVisible_BillingType) & ", " &
                " IsVisible_RateType = " & RetBitValue(TxtIsVisible_RateType) & ", " &
                " FilterInclude_ContraV_Type = '" & RetStringValue(TxtFilterInclude_ContraV_Type) & "', " &
                " Default_SubCode = '" & TxtDefault_SubCode.Tag & "', " &
                " FilterInclude_SubGroup = '" & RetStringValue(TxtFilterInclude_SubGroup) & "', " &
                " IsVisible_Supplier = " & RetBitValue(TxtIsVisible_Supplier) & ", " &
                " ShowLastPoRates = " & RetBitValue(TxtShowLastPoRates) & ", " &
                " ShowRecordCount = " & Val(TxtShowRecordCount.Text) & ", " &
                " IsVisible_PurchQuotation = " & RetBitValue(TxtIsVisible_PurchQuotation) & ", " &
                " IsVisible_PurchIndent = " & RetBitValue(TxtIsVisible_PurchIndent) & ", " &
                " IsVisible_FreeQty = " & RetBitValue(TxtIsVisible_FreeQty) & ", " &
                " IsVisible_MRP = " & RetBitValue(TxtIsVisible_MRP) & ", " &
                " IsVisible_Deal = " & RetBitValue(TxtIsVisible_Deal) & ", " &
                " IsVisible_DeliveryDetail = " & RetBitValue(TxtIsVisible_DeliveryDetail) & ", " &
                " IsVisible_ShippingDetail = " & RetBitValue(TxtIsVisible_ShippingDetail) & ", " &
                " IsVisible_Qty = " & RetBitValue(TxtIsVisible_Qty) & ", " &
                " IsVisible_RejQty = " & RetBitValue(TxtIsVisible_RejQty) & ", " &
                " IsVisible_RejMeasure = " & RetBitValue(TxtIsVisible_RejMeasure) & ", " &
                " IsVisible_FreeMeasure = " & RetBitValue(TxtIsVisible_FreeMeasure) & ", " &
                " IsVisible_PartyUPC = " & RetBitValue(TxtIsVisible_PartyUPC) & ", " &
                " IsVisible_PartySpecification = " & RetBitValue(TxtIsVisible_PartySpecification) & ", " &
                " IsVisible_Unit = " & RetBitValue(TxtIsVisible_Unit) & ", " &
                " IsVisible_ProfitMarginPer = " & RetBitValue(TxtIsVisible_ProfitMarginPer) & ", " &
                " IsVisible_ExpiryDate = " & RetBitValue(TXTIsVisible_ExpiryDate) & ", " &
                " IsEditable_ProfitMarginPer = " & RetBitValue(TxtIsEditable_ProfitMarginPer) & ", " &
                " IsVisible_SaleRate = " & RetBitValue(TxtIsVisible_SaleRate) & ", " &
                " IsPostedInStockVirtual = " & RetBitValue(TxtIsPostedInStockVirtual) & ", " &
                " IsVisible_PartySKU = " & RetBitValue(TxtIsVisible_PartySKU) & ", " &
                " IsVisible_CostCenter = " & RetBitValue(TxtIsVisible_CostCenter) & ", " &
                " IsPostInSaleInvoice = " & RetBitValue(TxtIsPostInSaleInvoice) & ", " &
                " IsEditable_Qty = " & RetBitValue(TxtIsEditable_Qty) & ", " &
                " IsMandatory_Approval = " & RetBitValue(TxtIsMandatory_Approval) & ", " &
                " IndustryType = '" & TxtIndustryType.Text & "', " &
                " TransactionDelete_AllowedDays = " & Val(TxtTransactionDelete_AllowedDays.Text) & ", " &
                " TransactionDelete_AllowedDaysAdmin = " & Val(TxtTransactionDelete_AllowedDaysAdmin.Text) & ", " &
                " TransactionEdit_AllowedDaysAdmin = " & Val(TxtTransactionEdit_AllowedDaysAdmin.Text) & ", " &
                " TransactionEdit_AllowedDays = " & Val(TxtTransactionEdit_AllowedDays.Text) & ", " &
                " IsVisible_TransactionHistory = " & RetBitValue(TxtIsVisible_TransactionHistory) & ", " &
                " TransactionHistory_ColumnWidthCsv = '" & TxtTransactionHistory_ColumnWidthCsv.Text & "', " &
                " TransactionHistory_SqlQuery = '" & TxtTransactionHistory_SqlQuery.Text & "', " &
                " FilterExclude_SubGroup = '" & RetStringValue(TxtFilterExclude_Subgroup) & "', " &
                " IsPostConsumption = " & RetBitValue(TxtIsPostConsumption) & " " &
                " Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        If AgL.StrCmp(Topctrl1.Mode, "Edit") Then
            TxtVoucherType.Enabled = False
        End If
        TxtSiteCode.Enabled = False
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1 And Div_Code = '" & AgL.PubDivCode & "' And Site_Code = '" & AgL.PubSiteCode & "'  AND IfNull(IsDeleted,0) = 0 "
        mQry = "Select Code As SearchCode " &
            " From Voucher_Type_Settings " & mConStr &
            " Order By V_Type "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseFunction_FIniMastLog(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMastLog
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1 " & AgL.RetDivisionCondition(AgL, "Div_Code") & ""
        mQry = "Select UID As SearchCode " &
               " From Voucher_Type_Settings_log " & mConStr &
               " And EntryStatus='" & LogStatus.LogOpen & "' Order By Description"

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = " SELECT H.* , Vt.Description AS VtDesc, G.Description AS GodownDesc, SG.DispName , SM.Name AS SiteName  " &
            " FROM Voucher_Type_Settings H " &
            " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type  " &
            " LEFT JOIN Godown G ON H.DEFAULT_Godown = G.Code  " &
            " LEFT JOIN SiteMast SM ON SM.Code = H.Site_Code " &
            " LEFT JOIN SubGroup SG ON SG.SubCode = H.Default_SubCode  " &
            " Where H.Code='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtVoucherType.Tag = AgL.XNull(.Rows(0)("V_Type"))
                TxtVoucherType.Text = AgL.XNull(.Rows(0)("VtDesc"))
                TxtSiteCode.Tag = AgL.XNull(.Rows(0)("Site_Code"))
                TxtSiteCode.Text = AgL.XNull(.Rows(0)("SiteName"))
                TxtIsEditableSubCode.Text = IIf(AgL.XNull(.Rows(0)("IsEditable_SubCode")) = "True", "Yes", "No")
                TxtIsMandatory_SubCode.Text = IIf(AgL.XNull(.Rows(0)("IsMandatory_SubCode")) = "True", "Yes", "No")
                TxtIsVisible_MeasurePerPcs.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_MeasurePerPcs")) = "True", "Yes", "No")
                TxtIsEditable_MeasurePerPcs.Text = IIf(AgL.XNull(.Rows(0)("IsEditable_MeasurePerPcs")) = "True", "Yes", "No")
                TxtIsVisible_Measure.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_Measure")) = "True", "Yes", "No")
                TxtIsEditable_Measure.Text = IIf(AgL.XNull(.Rows(0)("IsEditable_Measure")) = "True", "Yes", "No")
                TxtIsVisible_MeasureUnit.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_MeasureUnit")) = "True", "Yes", "No")
                TxtIsEditable_MeasureUnit.Text = IIf(AgL.XNull(.Rows(0)("IsEditable_MeasureUnit")) = "True", "Yes", "No")
                TxtIsVisible_ProdOrder.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_ProdOrder")) = "True", "Yes", "No")
                TxtIsVisible_Process.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_Process")) = "True", "Yes", "No")
                TxtIsVisible_LotNo.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_LotNo")) = "True", "Yes", "No")
                TxtIsVisible_BaleNo.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_BaleNo")) = "True", "Yes", "No")
                TxtIsPostedInStock.Text = IIf(AgL.XNull(.Rows(0)("IsPostedInStock")) = "True", "Yes", "No")
                TxtIsPostedInStockProcess.Text = IIf(AgL.XNull(.Rows(0)("IsPostedInStockProcess")) = "True", "Yes", "No")
                TxtIsVisible_ItemUID.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_ItemUID")) = "True", "Yes", "No")
                TxtIsVisible_ItemCode.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_ItemCode")) = "True", "Yes", "No")
                TxtIsEditable_ItemName.Text = IIf(AgL.XNull(.Rows(0)("IsEditable_ItemName")) = "True", "Yes", "No")
                TxtIsEditable_ItemCode.Text = IIf(AgL.XNull(.Rows(0)("IsEditable_ItemCode")) = "True", "Yes", "No")
                TxtIsVisible_ProcessLine.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_ProcessLine")) = "True", "Yes", "No")
                TxtIsEditable_ProcessLine.Text = IIf(AgL.XNull(.Rows(0)("IsEditable_ProcessLine")) = "True", "Yes", "No")
                TxtIsVisible_Rate.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_Rate")) = "True", "Yes", "No")
                TxtIsEditable_Rate.Text = IIf(AgL.XNull(.Rows(0)("IsEditable_Rate")) = "True", "Yes", "No")
                TxtIsVisible_Amount.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_Amount")) = "True", "Yes", "No")
                TxtIsEditable_Amount.Text = IIf(AgL.XNull(.Rows(0)("IsEditable_Amount")) = "True", "Yes", "No")
                TxtIsMandatory_Rate.Text = IIf(AgL.XNull(.Rows(0)("IsMandatory_Rate")) = "True", "Yes", "No")
                TxtIsVisible_Specification.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_Specification")) = "True", "Yes", "No")
                TxtIsVisible_BillingType.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_BillingType")) = "True", "Yes", "No")
                TxtIsVisible_RateType.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_RateType")) = "True", "Yes", "No")
                TxtIsVisible_Supplier.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_Supplier")) = "True", "Yes", "No")
                TxtShowLastPoRates.Text = IIf(AgL.XNull(.Rows(0)("ShowLastPoRates")) = "True", "Yes", "No")
                TxtShowRecordCount.Text = AgL.VNull(.Rows(0)("ShowRecordCount"))
                TxtIsVisible_PurchQuotation.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_PurchQuotation")) = "True", "Yes", "No")
                TxtIsVisible_PurchIndent.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_PurchIndent")) = "True", "Yes", "No")
                TxtIsVisible_FreeQty.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_FreeQty")) = "True", "Yes", "No")
                TxtIsVisible_MRP.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_MRP")) = "True", "Yes", "No")
                TxtIsVisible_Deal.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_Deal")) = "True", "Yes", "No")
                TxtIsVisible_DeliveryDetail.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_DeliveryDetail")) = "True", "Yes", "No")
                TxtIsVisible_ShippingDetail.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_ShippingDetail")) = "True", "Yes", "No")
                TxtIsVisible_Qty.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_Qty")) = "True", "Yes", "No")
                TxtIsVisible_RejQty.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_RejQty")) = "True", "Yes", "No")
                TxtIsVisible_RejMeasure.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_RejMeasure")) = "True", "Yes", "No")
                TxtIsVisible_FreeMeasure.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_FreeMeasure")) = "True", "Yes", "No")
                TxtIsVisible_PartyUPC.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_PartyUPC")) = "True", "Yes", "No")
                TxtIsVisible_PartySpecification.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_PartySpecification")) = "True", "Yes", "No")
                TxtIsVisible_Unit.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_Unit")) = "True", "Yes", "No")
                TxtIsVisible_ProfitMarginPer.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_ProfitMarginPer")) = "True", "Yes", "No")
                TXTIsVisible_ExpiryDate.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_ExpiryDate")) = "True", "Yes", "No")
                TxtIsEditable_ProfitMarginPer.Text = IIf(AgL.XNull(.Rows(0)("IsEditable_ProfitMarginPer")) = "True", "Yes", "No")
                TxtIsVisible_SaleRate.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_SaleRate")) = "True", "Yes", "No")
                TxtIsPostedInStockVirtual.Text = IIf(AgL.XNull(.Rows(0)("IsPostedInStockVirtual")) = "True", "Yes", "No")
                TxtIsVisible_PartySKU.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_PartySKU")) = "True", "Yes", "No")
                TxtIsVisible_CostCenter.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_CostCenter")) = "True", "Yes", "No")
                TxtIsPostConsumption.Text = IIf(AgL.XNull(.Rows(0)("IsPostConsumption")) = "True", "Yes", "No")
                TxtIsPostInSaleInvoice.Text = IIf(AgL.XNull(.Rows(0)("IsPostInSaleInvoice")) = "True", "Yes", "No")
                TxtIsEditable_Qty.Text = IIf(AgL.XNull(.Rows(0)("IsEditable_Qty")) = "True", "Yes", "No")
                TxtIsMandatory_Approval.Text = IIf(AgL.XNull(.Rows(0)("IsMandatory_Approval")) = "True", "Yes", "No")
                TxtIsVisible_TransactionHistory.Text = IIf(AgL.XNull(.Rows(0)("IsVisible_TransactionHistory")) = "True", "Yes", "No")

                TxtIndustryType.Text = AgL.XNull(.Rows(0)("IndustryType"))
                TxtTransactionHistory_ColumnWidthCsv.Text = AgL.XNull(.Rows(0)("TransactionHistory_ColumnWidthCsv"))
                TxtTransactionHistory_SqlQuery.Text = AgL.XNull(.Rows(0)("TransactionHistory_SqlQuery"))

                TxtTransactionDelete_AllowedDays.Text = AgL.VNull(.Rows(0)("TransactionDelete_AllowedDays"))
                TxtTransactionDelete_AllowedDaysAdmin.Text = AgL.VNull(.Rows(0)("TransactionDelete_AllowedDaysAdmin"))
                TxtTransactionEdit_AllowedDaysAdmin.Text = AgL.VNull(.Rows(0)("TransactionEdit_AllowedDaysAdmin"))
                TxtTransactionEdit_AllowedDays.Text = AgL.VNull(.Rows(0)("TransactionEdit_AllowedDays"))


                TxtFilterInclude_Process.Tag = AgL.XNull(.Rows(0)("FilterInclude_Process"))
                TxtFilterInclude_Process.Text = RetFilterValue(TxtFilterInclude_Process.Tag, "Select Description FROM Process", "NCat")
                TxtFilterInclude_AcGroup.Tag = AgL.XNull(.Rows(0)("FilterInclude_AcGroup"))
                TxtFilterInclude_AcGroup.Text = RetFilterValue(TxtFilterInclude_AcGroup.Tag, "SELECT GroupName FROM AcGroup", "GroupCode")
                TxtFilterExclude_AcGroup.Tag = AgL.XNull(.Rows(0)("FilterExclude_AcGroup"))
                TxtFilterExclude_AcGroup.Text = RetFilterValue(TxtFilterExclude_AcGroup.Tag, "SELECT GroupName FROM AcGroup", "GroupCode")
                TxtFilterInclude_ItemType.Tag = AgL.XNull(.Rows(0)("FilterInclude_ItemType"))
                TxtFilterInclude_ItemType.Text = RetFilterValue(TxtFilterInclude_ItemType.Tag, "SELECT Name FROM ItemType", "Code")
                TxtFilterInclude_ItemGroup.Tag = AgL.XNull(.Rows(0)("FilterInclude_ItemGroup"))
                TxtFilterInclude_ItemGroup.Text = RetFilterValue(TxtFilterInclude_ItemGroup.Tag, "SELECT Description FROM ItemGroup", "Code")
                TxtFilterExclude_ItemGroup.Tag = AgL.XNull(.Rows(0)("FilterExclude_ItemGroup"))
                TxtFilterExclude_ItemGroup.Text = RetFilterValue(TxtFilterExclude_ItemGroup.Tag, "SELECT Description FROM ItemGroup", "Code")
                TxtFilterInclude_ItemDivision.Tag = AgL.XNull(.Rows(0)("FilterInclude_ItemDivision"))
                TxtFilterInclude_ItemDivision.Text = RetFilterValue(TxtFilterInclude_ItemDivision.Tag, "SELECT Div_Name FROM Division", "Div_Code")
                TxtFilterInclude_ItemSite.Tag = AgL.XNull(.Rows(0)("FilterInclude_ItemSite"))
                TxtFilterInclude_ItemSite.Text = RetFilterValue(TxtFilterInclude_ItemSite.Tag, "SELECT Name  FROM SiteMast", "Code")
                TxtFilterInclude_SubgroupDivision.Tag = AgL.XNull(.Rows(0)("FilterInclude_SubgroupDivision"))
                TxtFilterInclude_SubgroupDivision.Text = RetFilterValue(TxtFilterInclude_SubgroupDivision.Tag, "SELECT Div_Name FROM Division", "Div_Code")
                TxtFilterInclude_SubgroupSite.Tag = AgL.XNull(.Rows(0)("FilterInclude_SubgroupSite"))
                TxtFilterInclude_SubgroupSite.Text = RetFilterValue(TxtFilterInclude_SubgroupSite.Tag, "SELECT Name  FROM SiteMast", "Code")
                TxtFilterExclude_Item.Tag = AgL.XNull(.Rows(0)("FilterExclude_Item"))
                TxtFilterExclude_Item.Text = RetFilterValue(TxtFilterExclude_Item.Tag, "SELECT Description FROM Item", "Code")
                TxtFilterInclude_Item.Tag = AgL.XNull(.Rows(0)("FilterInclude_Item"))
                TxtFilterInclude_Item.Text = RetFilterValue(TxtFilterInclude_Item.Tag, "SELECT Description FROM Item", "Code")
                TxtFilterExclude_SubGroupDivision.Tag = AgL.XNull(.Rows(0)("FilterExclude_SubGroupDivision"))
                TxtFilterExclude_SubGroupDivision.Text = RetFilterValue(TxtFilterExclude_SubGroupDivision.Tag, "SELECT Div_Name FROM Division", "Div_Code")
                TxtFilterExclude_SubGroupSite.Tag = AgL.XNull(.Rows(0)("FilterExclude_SubGroupSite"))
                TxtFilterExclude_SubGroupSite.Text = RetFilterValue(TxtFilterExclude_SubGroupSite.Tag, "SELECT Name  FROM SiteMast", "Code")
                TxtFilterInclude_ContraV_Type.Tag = AgL.XNull(.Rows(0)("FilterInclude_ContraV_Type"))
                TxtFilterInclude_ContraV_Type.Text = RetFilterValue(TxtFilterInclude_ContraV_Type.Tag, "Select Description FROM Voucher_Type", "V_Type")
                TxtFilterInclude_SubGroup.Tag = AgL.XNull(.Rows(0)("FilterInclude_SubGroup"))
                TxtFilterInclude_SubGroup.Text = RetFilterValue(TxtFilterInclude_SubGroup.Tag, "Select DispName FROM SubGroup", "SubCode")
                TxtFilterExclude_Subgroup.Tag = AgL.XNull(.Rows(0)("FilterExclude_SubGroup"))
                TxtFilterExclude_Subgroup.Text = RetFilterValue(TxtFilterExclude_Subgroup.Tag, "Select DispName FROM SubGroup", "SubCode")
                TxtDEFAULT_Godown.Tag = AgL.XNull(.Rows(0)("DEFAULT_Godown"))
                TxtDEFAULT_Godown.Text = AgL.XNull(.Rows(0)("GodownDesc"))
                TxtDefault_SubCode.Tag = AgL.XNull(.Rows(0)("Default_SubCode"))
                TxtDefault_SubCode.Text = AgL.XNull(.Rows(0)("DispName"))

            End If
        End With
        Topctrl1.tPrn = False
    End Sub
    Private Function RetFilterValue(ByVal bStrFilterValue As String, ByVal bStrQry As String, ByVal bCond As String) As String
        RetFilterValue = ""
        Dim mItemStr As String = ""
        If bStrFilterValue <> "" Then
            mQry = Replace(bStrQry, " FROM", " + ', ' FROM") & " Where " & bCond & " IN ( " & Replace(bStrFilterValue, "|", "'") & " ) " &
                    " FOR XML Path ('') "
            mItemStr = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
            mItemStr = mItemStr.Substring(0, Len(mItemStr) - 2)
        End If
        RetFilterValue = mItemStr
    End Function

    Private Sub FUpdateNullWithDefaultValues()
        mQry = "Update Voucher_Type_Settings Set IsEditable_SubCode = 1 Where IsEditable_SubCode  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsMandatory_SubCode = 1 Where IsMandatory_SubCode  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_MeasurePerPcs = 0 Where IsVisible_MeasurePerPcs  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsEditable_MeasurePerPcs = 0 Where IsEditable_MeasurePerPcs  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_Measure = 0 Where IsVisible_Measure  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsEditable_Measure = 0 Where IsEditable_Measure  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_MeasureUnit = 0 Where IsVisible_MeasureUnit  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsEditable_MeasureUnit = 0 Where IsEditable_MeasureUnit  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_ProdOrder = 0 Where IsVisible_ProdOrder  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_Process = 0 Where IsVisible_Process  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_LotNo = 0 Where IsVisible_LotNo  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_BaleNo = 0 Where IsVisible_BaleNo  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsPostedInStock = 1 Where IsPostedInStock  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsPostedInStockProcess = 0 Where IsPostedInStockProcess  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_ItemUID = 0 Where IsVisible_ItemUID  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_ItemCode = 0 Where IsVisible_ItemCode  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsEditable_ItemCode = 0 Where IsEditable_ItemCode  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsEditable_ItemName = 1 Where IsEditable_ItemName  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_ProcessLine = 0 Where IsVisible_ProcessLine  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsEditable_ProcessLine = 0 Where IsEditable_ProcessLine  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_Rate = 1 Where IsVisible_Rate  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsEditable_Rate = 1 Where IsEditable_Rate  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_Amount = 1 Where IsVisible_Amount  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsEditable_Amount = 0 Where IsEditable_Amount  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsMandatory_Rate = 0 Where IsMandatory_Rate  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_Specification = 1 Where IsVisible_Specification  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_BillingType = 0 Where IsVisible_BillingType  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_RateType = 0 Where IsVisible_RateType  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_Supplier = 0 Where IsVisible_Supplier  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set ShowLastPoRates = 5 Where ShowLastPoRates  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set ShowRecordCount = 5 Where ShowRecordCount  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_PurchQuotation = 1 Where IsVisible_PurchQuotation  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_PurchIndent = 1 Where IsVisible_PurchIndent  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_FreeQty = 0 Where IsVisible_FreeQty  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_MRP = 0 Where IsVisible_MRP  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_Deal = 0 Where IsVisible_Deal  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_DeliveryDetail = 0 Where IsVisible_DeliveryDetail  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_ShippingDetail = 1 Where IsVisible_ShippingDetail  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_Qty = 0 Where IsVisible_Qty  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_RejQty = 0 Where IsVisible_RejQty  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_RejMeasure = 0 Where IsVisible_RejMeasure  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_FreeMeasure = 0 Where IsVisible_FreeMeasure  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_PartyUPC = 0 Where IsVisible_PartyUPC  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_PartySpecification = 0 Where IsVisible_PartySpecification  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_Unit = 1 Where IsVisible_Unit  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_ProfitMarginPer = 0 Where IsVisible_ProfitMarginPer  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_ExpiryDate = 0 Where IsVisible_ExpiryDate  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsEditable_ProfitMarginPer = 0 Where IsEditable_ProfitMarginPer  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_SaleRate = 0 Where IsVisible_SaleRate  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsPostedInStockVirtual = 1 Where IsPostedInStockVirtual  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_PartySKU = 0 Where IsVisible_PartySKU  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsVisible_CostCenter = 0 Where IsVisible_CostCenter  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsPostConsumption = 0 Where IsPostConsumption  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsPostInSaleInvoice = 1 Where IsPostInSaleInvoice  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsMandatory_Approval = 0 Where IsMandatory_Approval  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        mQry = "Update Voucher_Type_Settings Set IsEditable_Qty = 1 Where IsEditable_Qty  is Null "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
    End Sub

    Private Sub ProcToFillDetaultValue()
        TxtIsEditableSubCode.Text = Yes
        TxtIsMandatory_SubCode.Text = Yes
        TxtIsVisible_MeasurePerPcs.Text = No
        TxtIsEditable_MeasurePerPcs.Text = No
        TxtIsVisible_Measure.Text = No
        TxtIsEditable_Measure.Text = No
        TxtIsVisible_MeasureUnit.Text = No
        TxtIsEditable_MeasureUnit.Text = No
        TxtIsVisible_ProdOrder.Text = No
        TxtIsVisible_Process.Text = No
        TxtIsVisible_LotNo.Text = No
        TxtIsVisible_BaleNo.Text = No
        TxtIsPostedInStock.Text = Yes
        TxtIsPostedInStockProcess.Text = No
        TxtIsVisible_ItemUID.Text = No
        TxtIsVisible_ItemCode.Text = No
        TxtIsEditable_ItemName.Text = Yes
        TxtIsEditable_ItemCode.Text = Yes
        TxtIsVisible_ProcessLine.Text = No
        TxtIsEditable_ProcessLine.Text = No
        TxtIsVisible_Rate.Text = Yes
        TxtIsEditable_Rate.Text = Yes
        TxtIsVisible_Amount.Text = Yes
        TxtIsEditable_Amount.Text = No
        TxtIsMandatory_Rate.Text = No
        TxtIsVisible_Specification.Text = No
        TxtIsVisible_BillingType.Text = No
        TxtIsVisible_RateType.Text = No
        TxtIsVisible_Supplier.Text = No
        TxtShowLastPoRates.Text = No
        TxtShowRecordCount.Text = 5
        TxtIsVisible_PurchQuotation.Text = No
        TxtIsVisible_PurchIndent.Text = No
        TxtIsVisible_FreeQty.Text = No
        TxtIsVisible_MRP.Text = No
        TxtIsVisible_Deal.Text = No
        TxtIsVisible_DeliveryDetail.Text = No
        TxtIsVisible_ShippingDetail.Text = No
        TxtIsVisible_Qty.Text = Yes
        TxtIsVisible_RejQty.Text = No
        TxtIsVisible_RejMeasure.Text = No
        TxtIsVisible_FreeMeasure.Text = No
        TxtIsVisible_PartyUPC.Text = No
        TxtIsVisible_PartySpecification.Text = No
        TxtIsVisible_Unit.Text = Yes
        TxtIsVisible_ProfitMarginPer.Text = No
        TXTIsVisible_ExpiryDate.Text = No
        TxtIsEditable_ProfitMarginPer.Text = No
        TxtIsVisible_SaleRate.Text = No
        TxtIsPostedInStockVirtual.Text = No
        TxtIsVisible_PartySKU.Text = No
        TxtIsVisible_CostCenter.Text = No
        TxtIsPostConsumption.Text = No
        TxtIsPostInSaleInvoice.Text = No
        TxtIsEditable_Qty.Text = Yes
        TxtIsMandatory_Approval.Text = No
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AgL.WinSetting(Me, 660, 992, 0, 0)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtVoucherType.Focus()
        TxtSiteCode.Tag = AgL.PubSiteCode
        TxtSiteCode.Text = AgL.Dman_Execute("SELECT Name FROM SiteMast Where Code = '" & TxtSiteCode.Tag & "'", AgL.GcnRead).ExecuteScalar
        ProcToFillDetaultValue()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtIsMandatory_SubCode.Focus()
    End Sub

    Private Sub InitializeComponent()
        Me.Label26 = New System.Windows.Forms.Label
        Me.TxtSiteCode = New AgControls.AgTextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.TxtIsEditable_Rate = New AgControls.AgTextBox
        Me.Label41 = New System.Windows.Forms.Label
        Me.TxtIsVisible_Amount = New AgControls.AgTextBox
        Me.Label42 = New System.Windows.Forms.Label
        Me.TxtIsEditable_Amount = New AgControls.AgTextBox
        Me.Label43 = New System.Windows.Forms.Label
        Me.TxtIsMandatory_Rate = New AgControls.AgTextBox
        Me.Label44 = New System.Windows.Forms.Label
        Me.TC1 = New System.Windows.Forms.TabControl
        Me.TP1 = New System.Windows.Forms.TabPage
        Me.TxtIndustryType = New AgControls.AgTextBox
        Me.Label59 = New System.Windows.Forms.Label
        Me.TxtFilterExclude_Subgroup = New AgControls.AgTextBox
        Me.Label58 = New System.Windows.Forms.Label
        Me.TxtIsMandatory_Approval = New AgControls.AgTextBox
        Me.Label57 = New System.Windows.Forms.Label
        Me.TxtIsEditable_Qty = New AgControls.AgTextBox
        Me.Label56 = New System.Windows.Forms.Label
        Me.TxtIsPostConsumption = New AgControls.AgTextBox
        Me.Label80 = New System.Windows.Forms.Label
        Me.TxtIsVisible_CostCenter = New AgControls.AgTextBox
        Me.Label79 = New System.Windows.Forms.Label
        Me.TxtIsPostedInStockVirtual = New AgControls.AgTextBox
        Me.Label75 = New System.Windows.Forms.Label
        Me.TxtIsVisible_Unit = New AgControls.AgTextBox
        Me.Label71 = New System.Windows.Forms.Label
        Me.TxtIsVisible_PartySpecification = New AgControls.AgTextBox
        Me.Label70 = New System.Windows.Forms.Label
        Me.TxtIsVisible_RejMeasure = New AgControls.AgTextBox
        Me.Label67 = New System.Windows.Forms.Label
        Me.TxtIsVisible_RejQty = New AgControls.AgTextBox
        Me.Label66 = New System.Windows.Forms.Label
        Me.TxtIsVisible_Qty = New AgControls.AgTextBox
        Me.Label65 = New System.Windows.Forms.Label
        Me.TxtFilterInclude_SubGroup = New AgControls.AgTextBox
        Me.Label55 = New System.Windows.Forms.Label
        Me.TxtDefault_SubCode = New AgControls.AgTextBox
        Me.Label54 = New System.Windows.Forms.Label
        Me.TxtIsVisible_Specification = New AgControls.AgTextBox
        Me.Label53 = New System.Windows.Forms.Label
        Me.TxtIsVisible_Supplier = New AgControls.AgTextBox
        Me.Label47 = New System.Windows.Forms.Label
        Me.TxtIsVisible_RateType = New AgControls.AgTextBox
        Me.Label46 = New System.Windows.Forms.Label
        Me.TxtIsVisible_BillingType = New AgControls.AgTextBox
        Me.Label45 = New System.Windows.Forms.Label
        Me.TxtIsVisible_Rate = New AgControls.AgTextBox
        Me.Label40 = New System.Windows.Forms.Label
        Me.TxtIsVisible_ProcessLine = New AgControls.AgTextBox
        Me.Label39 = New System.Windows.Forms.Label
        Me.TxtIsEditable_ProcessLine = New AgControls.AgTextBox
        Me.Label38 = New System.Windows.Forms.Label
        Me.TxtIsVisible_ItemCode = New AgControls.AgTextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.TxtIsEditable_ItemCode = New AgControls.AgTextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.TxtIsEditable_ItemName = New AgControls.AgTextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.TxtIsVisible_ItemUID = New AgControls.AgTextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.TxtIsPostedInStockProcess = New AgControls.AgTextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.TxtIsPostedInStock = New AgControls.AgTextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.TxtIsVisible_BaleNo = New AgControls.AgTextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.TxtIsVisible_LotNo = New AgControls.AgTextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.TxtIsVisible_Process = New AgControls.AgTextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.TxtIsVisible_ProdOrder = New AgControls.AgTextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.TxtIsEditable_MeasureUnit = New AgControls.AgTextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.TxtIsVisible_MeasureUnit = New AgControls.AgTextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.TxtIsEditable_Measure = New AgControls.AgTextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.TxtIsVisible_Measure = New AgControls.AgTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TxtIsEditable_MeasurePerPcs = New AgControls.AgTextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtIsVisible_MeasurePerPcs = New AgControls.AgTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtIsMandatory_SubCode = New AgControls.AgTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtIsEditableSubCode = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtDEFAULT_Godown = New AgControls.AgTextBox
        Me.Label37 = New System.Windows.Forms.Label
        Me.TxtFilterExclude_SubGroupDivision = New AgControls.AgTextBox
        Me.Label36 = New System.Windows.Forms.Label
        Me.TxtFilterExclude_SubGroupSite = New AgControls.AgTextBox
        Me.Label35 = New System.Windows.Forms.Label
        Me.TxtFilterInclude_ContraV_Type = New AgControls.AgTextBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.TxtFilterInclude_Item = New AgControls.AgTextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.TxtFilterExclude_Item = New AgControls.AgTextBox
        Me.Label31 = New System.Windows.Forms.Label
        Me.TxtFilterInclude_SubgroupSite = New AgControls.AgTextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtFilterInclude_SubgroupDivision = New AgControls.AgTextBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.TxtFilterInclude_ItemSite = New AgControls.AgTextBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.TxtFilterInclude_Process = New AgControls.AgTextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.TxtFilterInclude_AcGroup = New AgControls.AgTextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.TxtFilterExclude_AcGroup = New AgControls.AgTextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.TxtFilterInclude_ItemType = New AgControls.AgTextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.TxtFilterInclude_ItemGroup = New AgControls.AgTextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.TxtFilterExclude_ItemGroup = New AgControls.AgTextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.TxtFilterInclude_ItemDivision = New AgControls.AgTextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.TP2 = New System.Windows.Forms.TabPage
        Me.TxtIsPostInSaleInvoice = New AgControls.AgTextBox
        Me.Label34 = New System.Windows.Forms.Label
        Me.TxtIsVisible_FreeMeasure = New AgControls.AgTextBox
        Me.Label68 = New System.Windows.Forms.Label
        Me.TxtIsVisible_PartySKU = New AgControls.AgTextBox
        Me.Label78 = New System.Windows.Forms.Label
        Me.TXTIsVisible_ExpiryDate = New AgControls.AgTextBox
        Me.Label77 = New System.Windows.Forms.Label
        Me.TxtIsVisible_SaleRate = New AgControls.AgTextBox
        Me.Label74 = New System.Windows.Forms.Label
        Me.TxtIsEditable_ProfitMarginPer = New AgControls.AgTextBox
        Me.Label73 = New System.Windows.Forms.Label
        Me.TxtIsVisible_ProfitMarginPer = New AgControls.AgTextBox
        Me.Label72 = New System.Windows.Forms.Label
        Me.TxtIsVisible_PartyUPC = New AgControls.AgTextBox
        Me.Label69 = New System.Windows.Forms.Label
        Me.TxtIsVisible_ShippingDetail = New AgControls.AgTextBox
        Me.Label64 = New System.Windows.Forms.Label
        Me.TxtIsVisible_DeliveryDetail = New AgControls.AgTextBox
        Me.Label63 = New System.Windows.Forms.Label
        Me.TxtIsVisible_Deal = New AgControls.AgTextBox
        Me.Label62 = New System.Windows.Forms.Label
        Me.TxtIsVisible_MRP = New AgControls.AgTextBox
        Me.Label61 = New System.Windows.Forms.Label
        Me.TxtIsVisible_FreeQty = New AgControls.AgTextBox
        Me.Label52 = New System.Windows.Forms.Label
        Me.TxtIsVisible_PurchIndent = New AgControls.AgTextBox
        Me.Label51 = New System.Windows.Forms.Label
        Me.TxtIsVisible_PurchQuotation = New AgControls.AgTextBox
        Me.Label50 = New System.Windows.Forms.Label
        Me.TxtShowRecordCount = New AgControls.AgTextBox
        Me.Label49 = New System.Windows.Forms.Label
        Me.TxtShowLastPoRates = New AgControls.AgTextBox
        Me.Label48 = New System.Windows.Forms.Label
        Me.LblEntryTypeReq = New System.Windows.Forms.Label
        Me.TxtVoucherType = New AgControls.AgTextBox
        Me.LblEntryType = New System.Windows.Forms.Label
        Me.BtnCopyToAllDiv = New System.Windows.Forms.Button
        Me.BtnCopyToAllSite = New System.Windows.Forms.Button
        Me.BtnFillDefaultValue = New System.Windows.Forms.Button
        Me.BtnInsertV_Type = New System.Windows.Forms.Button
        Me.TP3 = New System.Windows.Forms.TabPage
        Me.TxtTransactionDelete_AllowedDays = New AgControls.AgTextBox
        Me.Label60 = New System.Windows.Forms.Label
        Me.TxtTransactionDelete_AllowedDaysAdmin = New AgControls.AgTextBox
        Me.Label76 = New System.Windows.Forms.Label
        Me.TxtTransactionEdit_AllowedDaysAdmin = New AgControls.AgTextBox
        Me.Label81 = New System.Windows.Forms.Label
        Me.TxtTransactionEdit_AllowedDays = New AgControls.AgTextBox
        Me.Label82 = New System.Windows.Forms.Label
        Me.TxtIsVisible_TransactionHistory = New AgControls.AgTextBox
        Me.Label83 = New System.Windows.Forms.Label
        Me.TP4 = New System.Windows.Forms.TabPage
        Me.TxtTransactionHistory_ColumnWidthCsv = New AgControls.AgTextBox
        Me.Label84 = New System.Windows.Forms.Label
        Me.TxtTransactionHistory_SqlQuery = New AgControls.AgTextBox
        Me.Label85 = New System.Windows.Forms.Label
        Me.GrpUP.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TC1.SuspendLayout()
        Me.TP1.SuspendLayout()
        Me.TP2.SuspendLayout()
        Me.TP3.SuspendLayout()
        Me.TP4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(986, 41)
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 573)
        Me.GroupBox1.Size = New System.Drawing.Size(988, 10)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(20, 583)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(287, 583)
        Me.GBoxEntryType.Size = New System.Drawing.Size(121, 44)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Size = New System.Drawing.Size(115, 18)
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(580, 588)
        Me.GBoxMoveToLog.Size = New System.Drawing.Size(121, 44)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Size = New System.Drawing.Size(89, 18)
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(436, 588)
        Me.GBoxApprove.Size = New System.Drawing.Size(121, 44)
        Me.GBoxApprove.Text = "Approved By"
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Location = New System.Drawing.Point(3, 23)
        Me.TxtApproveBy.Size = New System.Drawing.Size(115, 18)
        Me.TxtApproveBy.Tag = ""
        '
        'CmdDiscard
        '
        Me.CmdDiscard.Location = New System.Drawing.Point(92, 18)
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(822, 583)
        Me.GroupBox2.Size = New System.Drawing.Size(121, 44)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(557, 583)
        Me.GBoxDivision.Size = New System.Drawing.Size(121, 44)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Size = New System.Drawing.Size(115, 18)
        Me.TxtDivision.Tag = ""
        '
        'TxtStatus
        '
        Me.TxtStatus.AgSelectedValue = ""
        Me.TxtStatus.Size = New System.Drawing.Size(89, 18)
        Me.TxtStatus.Tag = ""
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(176, 77)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(10, 7)
        Me.Label26.TabIndex = 730
        Me.Label26.Text = "Ä"
        '
        'TxtSiteCode
        '
        Me.TxtSiteCode.AgAllowUserToEnableMasterHelp = False
        Me.TxtSiteCode.AgLastValueTag = Nothing
        Me.TxtSiteCode.AgLastValueText = Nothing
        Me.TxtSiteCode.AgMandatory = True
        Me.TxtSiteCode.AgMasterHelp = False
        Me.TxtSiteCode.AgNumberLeftPlaces = 0
        Me.TxtSiteCode.AgNumberNegetiveAllow = False
        Me.TxtSiteCode.AgNumberRightPlaces = 0
        Me.TxtSiteCode.AgPickFromLastValue = False
        Me.TxtSiteCode.AgRowFilter = ""
        Me.TxtSiteCode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSiteCode.AgSelectedValue = Nothing
        Me.TxtSiteCode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSiteCode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSiteCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSiteCode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSiteCode.Location = New System.Drawing.Point(192, 72)
        Me.TxtSiteCode.MaxLength = 0
        Me.TxtSiteCode.Name = "TxtSiteCode"
        Me.TxtSiteCode.Size = New System.Drawing.Size(241, 18)
        Me.TxtSiteCode.TabIndex = 2
        Me.TxtSiteCode.Text = "TxtSiteCode"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(78, 72)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(76, 16)
        Me.Label27.TabIndex = 729
        Me.Label27.Text = "Site/Branch"
        '
        'TxtIsEditable_Rate
        '
        Me.TxtIsEditable_Rate.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsEditable_Rate.AgLastValueTag = Nothing
        Me.TxtIsEditable_Rate.AgLastValueText = Nothing
        Me.TxtIsEditable_Rate.AgMandatory = False
        Me.TxtIsEditable_Rate.AgMasterHelp = False
        Me.TxtIsEditable_Rate.AgNumberLeftPlaces = 0
        Me.TxtIsEditable_Rate.AgNumberNegetiveAllow = False
        Me.TxtIsEditable_Rate.AgNumberRightPlaces = 0
        Me.TxtIsEditable_Rate.AgPickFromLastValue = False
        Me.TxtIsEditable_Rate.AgRowFilter = ""
        Me.TxtIsEditable_Rate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsEditable_Rate.AgSelectedValue = Nothing
        Me.TxtIsEditable_Rate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsEditable_Rate.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsEditable_Rate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsEditable_Rate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsEditable_Rate.Location = New System.Drawing.Point(187, 337)
        Me.TxtIsEditable_Rate.MaxLength = 3
        Me.TxtIsEditable_Rate.Name = "TxtIsEditable_Rate"
        Me.TxtIsEditable_Rate.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsEditable_Rate.TabIndex = 17
        Me.TxtIsEditable_Rate.Text = "TxtIsEditable_Rate"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(9, 337)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(100, 16)
        Me.Label41.TabIndex = 756
        Me.Label41.Text = "Is Editable Rate"
        '
        'TxtIsVisible_Amount
        '
        Me.TxtIsVisible_Amount.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_Amount.AgLastValueTag = Nothing
        Me.TxtIsVisible_Amount.AgLastValueText = Nothing
        Me.TxtIsVisible_Amount.AgMandatory = False
        Me.TxtIsVisible_Amount.AgMasterHelp = False
        Me.TxtIsVisible_Amount.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_Amount.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_Amount.AgNumberRightPlaces = 0
        Me.TxtIsVisible_Amount.AgPickFromLastValue = False
        Me.TxtIsVisible_Amount.AgRowFilter = ""
        Me.TxtIsVisible_Amount.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_Amount.AgSelectedValue = Nothing
        Me.TxtIsVisible_Amount.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_Amount.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_Amount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_Amount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_Amount.Location = New System.Drawing.Point(187, 356)
        Me.TxtIsVisible_Amount.MaxLength = 3
        Me.TxtIsVisible_Amount.Name = "TxtIsVisible_Amount"
        Me.TxtIsVisible_Amount.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_Amount.TabIndex = 18
        Me.TxtIsVisible_Amount.Text = "TxtIsVisible_Amount"
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.Location = New System.Drawing.Point(9, 356)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(109, 16)
        Me.Label42.TabIndex = 762
        Me.Label42.Text = "Is Visible Amount"
        '
        'TxtIsEditable_Amount
        '
        Me.TxtIsEditable_Amount.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsEditable_Amount.AgLastValueTag = Nothing
        Me.TxtIsEditable_Amount.AgLastValueText = Nothing
        Me.TxtIsEditable_Amount.AgMandatory = False
        Me.TxtIsEditable_Amount.AgMasterHelp = False
        Me.TxtIsEditable_Amount.AgNumberLeftPlaces = 0
        Me.TxtIsEditable_Amount.AgNumberNegetiveAllow = False
        Me.TxtIsEditable_Amount.AgNumberRightPlaces = 0
        Me.TxtIsEditable_Amount.AgPickFromLastValue = False
        Me.TxtIsEditable_Amount.AgRowFilter = ""
        Me.TxtIsEditable_Amount.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsEditable_Amount.AgSelectedValue = Nothing
        Me.TxtIsEditable_Amount.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsEditable_Amount.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsEditable_Amount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsEditable_Amount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsEditable_Amount.Location = New System.Drawing.Point(187, 375)
        Me.TxtIsEditable_Amount.MaxLength = 3
        Me.TxtIsEditable_Amount.Name = "TxtIsEditable_Amount"
        Me.TxtIsEditable_Amount.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsEditable_Amount.TabIndex = 19
        Me.TxtIsEditable_Amount.Text = "TxtIsEditable_Amount"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.Location = New System.Drawing.Point(9, 375)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(117, 16)
        Me.Label43.TabIndex = 760
        Me.Label43.Text = "Is Editable Amount"
        '
        'TxtIsMandatory_Rate
        '
        Me.TxtIsMandatory_Rate.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsMandatory_Rate.AgLastValueTag = Nothing
        Me.TxtIsMandatory_Rate.AgLastValueText = Nothing
        Me.TxtIsMandatory_Rate.AgMandatory = False
        Me.TxtIsMandatory_Rate.AgMasterHelp = False
        Me.TxtIsMandatory_Rate.AgNumberLeftPlaces = 0
        Me.TxtIsMandatory_Rate.AgNumberNegetiveAllow = False
        Me.TxtIsMandatory_Rate.AgNumberRightPlaces = 0
        Me.TxtIsMandatory_Rate.AgPickFromLastValue = False
        Me.TxtIsMandatory_Rate.AgRowFilter = ""
        Me.TxtIsMandatory_Rate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsMandatory_Rate.AgSelectedValue = Nothing
        Me.TxtIsMandatory_Rate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsMandatory_Rate.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsMandatory_Rate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsMandatory_Rate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsMandatory_Rate.Location = New System.Drawing.Point(187, 318)
        Me.TxtIsMandatory_Rate.MaxLength = 3
        Me.TxtIsMandatory_Rate.Name = "TxtIsMandatory_Rate"
        Me.TxtIsMandatory_Rate.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsMandatory_Rate.TabIndex = 16
        Me.TxtIsMandatory_Rate.Text = "TxtIsMandatory_Rate"
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.Location = New System.Drawing.Point(9, 318)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(114, 16)
        Me.Label44.TabIndex = 764
        Me.Label44.Text = "Is Mandatory Rate"
        '
        'TC1
        '
        Me.TC1.Controls.Add(Me.TP1)
        Me.TC1.Controls.Add(Me.TP2)
        Me.TC1.Controls.Add(Me.TP3)
        Me.TC1.Controls.Add(Me.TP4)
        Me.TC1.Location = New System.Drawing.Point(0, 102)
        Me.TC1.Name = "TC1"
        Me.TC1.SelectedIndex = 0
        Me.TC1.Size = New System.Drawing.Size(986, 481)
        Me.TC1.TabIndex = 3
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.Gainsboro
        Me.TP1.Controls.Add(Me.TxtIsVisible_TransactionHistory)
        Me.TP1.Controls.Add(Me.Label83)
        Me.TP1.Controls.Add(Me.TxtIndustryType)
        Me.TP1.Controls.Add(Me.Label59)
        Me.TP1.Controls.Add(Me.TxtFilterExclude_Subgroup)
        Me.TP1.Controls.Add(Me.Label58)
        Me.TP1.Controls.Add(Me.TxtIsMandatory_Approval)
        Me.TP1.Controls.Add(Me.Label57)
        Me.TP1.Controls.Add(Me.TxtIsEditable_Qty)
        Me.TP1.Controls.Add(Me.Label56)
        Me.TP1.Controls.Add(Me.TxtIsPostConsumption)
        Me.TP1.Controls.Add(Me.Label80)
        Me.TP1.Controls.Add(Me.TxtIsVisible_CostCenter)
        Me.TP1.Controls.Add(Me.Label79)
        Me.TP1.Controls.Add(Me.TxtIsPostedInStockVirtual)
        Me.TP1.Controls.Add(Me.Label75)
        Me.TP1.Controls.Add(Me.TxtIsVisible_Unit)
        Me.TP1.Controls.Add(Me.Label71)
        Me.TP1.Controls.Add(Me.TxtIsVisible_PartySpecification)
        Me.TP1.Controls.Add(Me.Label70)
        Me.TP1.Controls.Add(Me.TxtIsVisible_RejMeasure)
        Me.TP1.Controls.Add(Me.Label67)
        Me.TP1.Controls.Add(Me.TxtIsVisible_RejQty)
        Me.TP1.Controls.Add(Me.Label66)
        Me.TP1.Controls.Add(Me.TxtIsVisible_Qty)
        Me.TP1.Controls.Add(Me.Label65)
        Me.TP1.Controls.Add(Me.TxtFilterInclude_SubGroup)
        Me.TP1.Controls.Add(Me.Label55)
        Me.TP1.Controls.Add(Me.TxtDefault_SubCode)
        Me.TP1.Controls.Add(Me.Label54)
        Me.TP1.Controls.Add(Me.TxtIsVisible_Specification)
        Me.TP1.Controls.Add(Me.Label53)
        Me.TP1.Controls.Add(Me.TxtIsVisible_Supplier)
        Me.TP1.Controls.Add(Me.Label47)
        Me.TP1.Controls.Add(Me.TxtIsVisible_RateType)
        Me.TP1.Controls.Add(Me.Label46)
        Me.TP1.Controls.Add(Me.TxtIsVisible_BillingType)
        Me.TP1.Controls.Add(Me.Label45)
        Me.TP1.Controls.Add(Me.TxtIsMandatory_Rate)
        Me.TP1.Controls.Add(Me.Label44)
        Me.TP1.Controls.Add(Me.TxtIsVisible_Rate)
        Me.TP1.Controls.Add(Me.TxtIsVisible_Amount)
        Me.TP1.Controls.Add(Me.Label40)
        Me.TP1.Controls.Add(Me.Label42)
        Me.TP1.Controls.Add(Me.TxtIsVisible_ProcessLine)
        Me.TP1.Controls.Add(Me.TxtIsEditable_Amount)
        Me.TP1.Controls.Add(Me.Label39)
        Me.TP1.Controls.Add(Me.Label43)
        Me.TP1.Controls.Add(Me.TxtIsEditable_ProcessLine)
        Me.TP1.Controls.Add(Me.TxtIsEditable_Rate)
        Me.TP1.Controls.Add(Me.Label38)
        Me.TP1.Controls.Add(Me.Label41)
        Me.TP1.Controls.Add(Me.TxtIsVisible_ItemCode)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Controls.Add(Me.TxtIsEditable_ItemCode)
        Me.TP1.Controls.Add(Me.Label24)
        Me.TP1.Controls.Add(Me.TxtIsEditable_ItemName)
        Me.TP1.Controls.Add(Me.Label23)
        Me.TP1.Controls.Add(Me.TxtIsVisible_ItemUID)
        Me.TP1.Controls.Add(Me.Label15)
        Me.TP1.Controls.Add(Me.TxtIsPostedInStockProcess)
        Me.TP1.Controls.Add(Me.Label14)
        Me.TP1.Controls.Add(Me.TxtIsPostedInStock)
        Me.TP1.Controls.Add(Me.Label13)
        Me.TP1.Controls.Add(Me.TxtIsVisible_BaleNo)
        Me.TP1.Controls.Add(Me.Label12)
        Me.TP1.Controls.Add(Me.TxtIsVisible_LotNo)
        Me.TP1.Controls.Add(Me.Label11)
        Me.TP1.Controls.Add(Me.TxtIsVisible_Process)
        Me.TP1.Controls.Add(Me.Label10)
        Me.TP1.Controls.Add(Me.TxtIsVisible_ProdOrder)
        Me.TP1.Controls.Add(Me.Label9)
        Me.TP1.Controls.Add(Me.TxtIsEditable_MeasureUnit)
        Me.TP1.Controls.Add(Me.Label8)
        Me.TP1.Controls.Add(Me.TxtIsVisible_MeasureUnit)
        Me.TP1.Controls.Add(Me.Label7)
        Me.TP1.Controls.Add(Me.TxtIsEditable_Measure)
        Me.TP1.Controls.Add(Me.Label6)
        Me.TP1.Controls.Add(Me.TxtIsVisible_Measure)
        Me.TP1.Controls.Add(Me.Label5)
        Me.TP1.Controls.Add(Me.TxtIsEditable_MeasurePerPcs)
        Me.TP1.Controls.Add(Me.Label4)
        Me.TP1.Controls.Add(Me.TxtIsVisible_MeasurePerPcs)
        Me.TP1.Controls.Add(Me.Label2)
        Me.TP1.Controls.Add(Me.TxtIsMandatory_SubCode)
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.TxtIsEditableSubCode)
        Me.TP1.Controls.Add(Me.Label3)
        Me.TP1.Controls.Add(Me.TxtDEFAULT_Godown)
        Me.TP1.Controls.Add(Me.Label37)
        Me.TP1.Controls.Add(Me.TxtFilterExclude_SubGroupDivision)
        Me.TP1.Controls.Add(Me.Label36)
        Me.TP1.Controls.Add(Me.TxtFilterExclude_SubGroupSite)
        Me.TP1.Controls.Add(Me.Label35)
        Me.TP1.Controls.Add(Me.TxtFilterInclude_ContraV_Type)
        Me.TP1.Controls.Add(Me.Label33)
        Me.TP1.Controls.Add(Me.TxtFilterInclude_Item)
        Me.TP1.Controls.Add(Me.Label32)
        Me.TP1.Controls.Add(Me.TxtFilterExclude_Item)
        Me.TP1.Controls.Add(Me.Label31)
        Me.TP1.Controls.Add(Me.TxtFilterInclude_SubgroupSite)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.TxtFilterInclude_SubgroupDivision)
        Me.TP1.Controls.Add(Me.Label29)
        Me.TP1.Controls.Add(Me.TxtFilterInclude_ItemSite)
        Me.TP1.Controls.Add(Me.Label28)
        Me.TP1.Controls.Add(Me.TxtFilterInclude_Process)
        Me.TP1.Controls.Add(Me.Label22)
        Me.TP1.Controls.Add(Me.TxtFilterInclude_AcGroup)
        Me.TP1.Controls.Add(Me.Label21)
        Me.TP1.Controls.Add(Me.TxtFilterExclude_AcGroup)
        Me.TP1.Controls.Add(Me.Label20)
        Me.TP1.Controls.Add(Me.TxtFilterInclude_ItemType)
        Me.TP1.Controls.Add(Me.Label19)
        Me.TP1.Controls.Add(Me.TxtFilterInclude_ItemGroup)
        Me.TP1.Controls.Add(Me.Label18)
        Me.TP1.Controls.Add(Me.TxtFilterExclude_ItemGroup)
        Me.TP1.Controls.Add(Me.Label17)
        Me.TP1.Controls.Add(Me.TxtFilterInclude_ItemDivision)
        Me.TP1.Controls.Add(Me.Label16)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Name = "TP1"
        Me.TP1.Padding = New System.Windows.Forms.Padding(3)
        Me.TP1.Size = New System.Drawing.Size(978, 455)
        Me.TP1.TabIndex = 0
        Me.TP1.Text = "Common"
        '
        'TxtIndustryType
        '
        Me.TxtIndustryType.AgAllowUserToEnableMasterHelp = False
        Me.TxtIndustryType.AgLastValueTag = Nothing
        Me.TxtIndustryType.AgLastValueText = Nothing
        Me.TxtIndustryType.AgMandatory = False
        Me.TxtIndustryType.AgMasterHelp = False
        Me.TxtIndustryType.AgNumberLeftPlaces = 0
        Me.TxtIndustryType.AgNumberNegetiveAllow = False
        Me.TxtIndustryType.AgNumberRightPlaces = 0
        Me.TxtIndustryType.AgPickFromLastValue = False
        Me.TxtIndustryType.AgRowFilter = ""
        Me.TxtIndustryType.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIndustryType.AgSelectedValue = Nothing
        Me.TxtIndustryType.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIndustryType.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtIndustryType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIndustryType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIndustryType.Location = New System.Drawing.Point(780, 392)
        Me.TxtIndustryType.MaxLength = 100
        Me.TxtIndustryType.Name = "TxtIndustryType"
        Me.TxtIndustryType.Size = New System.Drawing.Size(188, 18)
        Me.TxtIndustryType.TabIndex = 58
        Me.TxtIndustryType.Text = "TxtIndustryType"
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.Location = New System.Drawing.Point(581, 392)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(85, 16)
        Me.Label59.TabIndex = 921
        Me.Label59.Text = "Industry Type"
        '
        'TxtFilterExclude_Subgroup
        '
        Me.TxtFilterExclude_Subgroup.AgAllowUserToEnableMasterHelp = False
        Me.TxtFilterExclude_Subgroup.AgLastValueTag = Nothing
        Me.TxtFilterExclude_Subgroup.AgLastValueText = Nothing
        Me.TxtFilterExclude_Subgroup.AgMandatory = False
        Me.TxtFilterExclude_Subgroup.AgMasterHelp = False
        Me.TxtFilterExclude_Subgroup.AgNumberLeftPlaces = 0
        Me.TxtFilterExclude_Subgroup.AgNumberNegetiveAllow = False
        Me.TxtFilterExclude_Subgroup.AgNumberRightPlaces = 0
        Me.TxtFilterExclude_Subgroup.AgPickFromLastValue = False
        Me.TxtFilterExclude_Subgroup.AgRowFilter = ""
        Me.TxtFilterExclude_Subgroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFilterExclude_Subgroup.AgSelectedValue = Nothing
        Me.TxtFilterExclude_Subgroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFilterExclude_Subgroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFilterExclude_Subgroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFilterExclude_Subgroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFilterExclude_Subgroup.Location = New System.Drawing.Point(780, 163)
        Me.TxtFilterExclude_Subgroup.MaxLength = 100
        Me.TxtFilterExclude_Subgroup.Name = "TxtFilterExclude_Subgroup"
        Me.TxtFilterExclude_Subgroup.Size = New System.Drawing.Size(188, 18)
        Me.TxtFilterExclude_Subgroup.TabIndex = 47
        Me.TxtFilterExclude_Subgroup.Text = "TxtFilterExclude_Subgroup"
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label58.Location = New System.Drawing.Point(581, 163)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(150, 16)
        Me.Label58.TabIndex = 919
        Me.Label58.Text = "Filter Exclude SubGroup"
        '
        'TxtIsMandatory_Approval
        '
        Me.TxtIsMandatory_Approval.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsMandatory_Approval.AgLastValueTag = Nothing
        Me.TxtIsMandatory_Approval.AgLastValueText = Nothing
        Me.TxtIsMandatory_Approval.AgMandatory = False
        Me.TxtIsMandatory_Approval.AgMasterHelp = False
        Me.TxtIsMandatory_Approval.AgNumberLeftPlaces = 0
        Me.TxtIsMandatory_Approval.AgNumberNegetiveAllow = False
        Me.TxtIsMandatory_Approval.AgNumberRightPlaces = 0
        Me.TxtIsMandatory_Approval.AgPickFromLastValue = False
        Me.TxtIsMandatory_Approval.AgRowFilter = ""
        Me.TxtIsMandatory_Approval.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsMandatory_Approval.AgSelectedValue = Nothing
        Me.TxtIsMandatory_Approval.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsMandatory_Approval.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsMandatory_Approval.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsMandatory_Approval.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsMandatory_Approval.Location = New System.Drawing.Point(479, 328)
        Me.TxtIsMandatory_Approval.MaxLength = 3
        Me.TxtIsMandatory_Approval.Name = "TxtIsMandatory_Approval"
        Me.TxtIsMandatory_Approval.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsMandatory_Approval.TabIndex = 38
        Me.TxtIsMandatory_Approval.Text = "TxtIsMandatory_Approval"
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.Location = New System.Drawing.Point(300, 328)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(135, 16)
        Me.Label57.TabIndex = 917
        Me.Label57.Text = "Is Mandatory Approval"
        '
        'TxtIsEditable_Qty
        '
        Me.TxtIsEditable_Qty.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsEditable_Qty.AgLastValueTag = Nothing
        Me.TxtIsEditable_Qty.AgLastValueText = Nothing
        Me.TxtIsEditable_Qty.AgMandatory = False
        Me.TxtIsEditable_Qty.AgMasterHelp = False
        Me.TxtIsEditable_Qty.AgNumberLeftPlaces = 0
        Me.TxtIsEditable_Qty.AgNumberNegetiveAllow = False
        Me.TxtIsEditable_Qty.AgNumberRightPlaces = 0
        Me.TxtIsEditable_Qty.AgPickFromLastValue = False
        Me.TxtIsEditable_Qty.AgRowFilter = ""
        Me.TxtIsEditable_Qty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsEditable_Qty.AgSelectedValue = Nothing
        Me.TxtIsEditable_Qty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsEditable_Qty.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsEditable_Qty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsEditable_Qty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsEditable_Qty.Location = New System.Drawing.Point(187, 261)
        Me.TxtIsEditable_Qty.MaxLength = 3
        Me.TxtIsEditable_Qty.Name = "TxtIsEditable_Qty"
        Me.TxtIsEditable_Qty.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsEditable_Qty.TabIndex = 13
        Me.TxtIsEditable_Qty.Text = "TxtIsEditable_Qty"
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.Location = New System.Drawing.Point(9, 261)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(94, 16)
        Me.Label56.TabIndex = 915
        Me.Label56.Text = "Is Editable Qty"
        '
        'TxtIsPostConsumption
        '
        Me.TxtIsPostConsumption.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsPostConsumption.AgLastValueTag = Nothing
        Me.TxtIsPostConsumption.AgLastValueText = Nothing
        Me.TxtIsPostConsumption.AgMandatory = False
        Me.TxtIsPostConsumption.AgMasterHelp = False
        Me.TxtIsPostConsumption.AgNumberLeftPlaces = 0
        Me.TxtIsPostConsumption.AgNumberNegetiveAllow = False
        Me.TxtIsPostConsumption.AgNumberRightPlaces = 0
        Me.TxtIsPostConsumption.AgPickFromLastValue = False
        Me.TxtIsPostConsumption.AgRowFilter = ""
        Me.TxtIsPostConsumption.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsPostConsumption.AgSelectedValue = Nothing
        Me.TxtIsPostConsumption.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsPostConsumption.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsPostConsumption.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsPostConsumption.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsPostConsumption.Location = New System.Drawing.Point(479, 103)
        Me.TxtIsPostConsumption.MaxLength = 3
        Me.TxtIsPostConsumption.Name = "TxtIsPostConsumption"
        Me.TxtIsPostConsumption.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsPostConsumption.TabIndex = 27
        Me.TxtIsPostConsumption.Text = "TxtIsPostConsumption"
        '
        'Label80
        '
        Me.Label80.AutoSize = True
        Me.Label80.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label80.Location = New System.Drawing.Point(300, 103)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(129, 16)
        Me.Label80.TabIndex = 913
        Me.Label80.Text = "Is Post Consumption"
        '
        'TxtIsVisible_CostCenter
        '
        Me.TxtIsVisible_CostCenter.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_CostCenter.AgLastValueTag = Nothing
        Me.TxtIsVisible_CostCenter.AgLastValueText = Nothing
        Me.TxtIsVisible_CostCenter.AgMandatory = False
        Me.TxtIsVisible_CostCenter.AgMasterHelp = False
        Me.TxtIsVisible_CostCenter.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_CostCenter.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_CostCenter.AgNumberRightPlaces = 0
        Me.TxtIsVisible_CostCenter.AgPickFromLastValue = False
        Me.TxtIsVisible_CostCenter.AgRowFilter = ""
        Me.TxtIsVisible_CostCenter.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_CostCenter.AgSelectedValue = Nothing
        Me.TxtIsVisible_CostCenter.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_CostCenter.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_CostCenter.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_CostCenter.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_CostCenter.Location = New System.Drawing.Point(479, 84)
        Me.TxtIsVisible_CostCenter.MaxLength = 3
        Me.TxtIsVisible_CostCenter.Name = "TxtIsVisible_CostCenter"
        Me.TxtIsVisible_CostCenter.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_CostCenter.TabIndex = 26
        Me.TxtIsVisible_CostCenter.Text = "TxtIsVisible_CostCenter"
        '
        'Label79
        '
        Me.Label79.AutoSize = True
        Me.Label79.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label79.Location = New System.Drawing.Point(300, 84)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(134, 16)
        Me.Label79.TabIndex = 911
        Me.Label79.Text = "Is Visible Cost Center"
        '
        'TxtIsPostedInStockVirtual
        '
        Me.TxtIsPostedInStockVirtual.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsPostedInStockVirtual.AgLastValueTag = Nothing
        Me.TxtIsPostedInStockVirtual.AgLastValueText = Nothing
        Me.TxtIsPostedInStockVirtual.AgMandatory = False
        Me.TxtIsPostedInStockVirtual.AgMasterHelp = False
        Me.TxtIsPostedInStockVirtual.AgNumberLeftPlaces = 0
        Me.TxtIsPostedInStockVirtual.AgNumberNegetiveAllow = False
        Me.TxtIsPostedInStockVirtual.AgNumberRightPlaces = 0
        Me.TxtIsPostedInStockVirtual.AgPickFromLastValue = False
        Me.TxtIsPostedInStockVirtual.AgRowFilter = ""
        Me.TxtIsPostedInStockVirtual.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsPostedInStockVirtual.AgSelectedValue = Nothing
        Me.TxtIsPostedInStockVirtual.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsPostedInStockVirtual.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsPostedInStockVirtual.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsPostedInStockVirtual.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsPostedInStockVirtual.Location = New System.Drawing.Point(479, 164)
        Me.TxtIsPostedInStockVirtual.MaxLength = 3
        Me.TxtIsPostedInStockVirtual.Name = "TxtIsPostedInStockVirtual"
        Me.TxtIsPostedInStockVirtual.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsPostedInStockVirtual.TabIndex = 30
        Me.TxtIsPostedInStockVirtual.Text = "TxtIsPostedInStockVirtual"
        '
        'Label75
        '
        Me.Label75.AutoSize = True
        Me.Label75.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label75.Location = New System.Drawing.Point(300, 164)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(152, 16)
        Me.Label75.TabIndex = 903
        Me.Label75.Text = "IsPosted In Stock Virtual"
        '
        'TxtIsVisible_Unit
        '
        Me.TxtIsVisible_Unit.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_Unit.AgLastValueTag = Nothing
        Me.TxtIsVisible_Unit.AgLastValueText = Nothing
        Me.TxtIsVisible_Unit.AgMandatory = False
        Me.TxtIsVisible_Unit.AgMasterHelp = False
        Me.TxtIsVisible_Unit.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_Unit.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_Unit.AgNumberRightPlaces = 0
        Me.TxtIsVisible_Unit.AgPickFromLastValue = False
        Me.TxtIsVisible_Unit.AgRowFilter = ""
        Me.TxtIsVisible_Unit.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_Unit.AgSelectedValue = Nothing
        Me.TxtIsVisible_Unit.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_Unit.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_Unit.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_Unit.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_Unit.Location = New System.Drawing.Point(187, 280)
        Me.TxtIsVisible_Unit.MaxLength = 3
        Me.TxtIsVisible_Unit.Name = "TxtIsVisible_Unit"
        Me.TxtIsVisible_Unit.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_Unit.TabIndex = 14
        Me.TxtIsVisible_Unit.Text = "TxtIsVisible_Unit"
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label71.Location = New System.Drawing.Point(9, 280)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(88, 16)
        Me.Label71.TabIndex = 883
        Me.Label71.Text = "Is Visible Unit"
        '
        'TxtIsVisible_PartySpecification
        '
        Me.TxtIsVisible_PartySpecification.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_PartySpecification.AgLastValueTag = Nothing
        Me.TxtIsVisible_PartySpecification.AgLastValueText = Nothing
        Me.TxtIsVisible_PartySpecification.AgMandatory = False
        Me.TxtIsVisible_PartySpecification.AgMasterHelp = False
        Me.TxtIsVisible_PartySpecification.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_PartySpecification.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_PartySpecification.AgNumberRightPlaces = 0
        Me.TxtIsVisible_PartySpecification.AgPickFromLastValue = False
        Me.TxtIsVisible_PartySpecification.AgRowFilter = ""
        Me.TxtIsVisible_PartySpecification.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_PartySpecification.AgSelectedValue = Nothing
        Me.TxtIsVisible_PartySpecification.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_PartySpecification.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_PartySpecification.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_PartySpecification.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_PartySpecification.Location = New System.Drawing.Point(479, 65)
        Me.TxtIsVisible_PartySpecification.MaxLength = 3
        Me.TxtIsVisible_PartySpecification.Name = "TxtIsVisible_PartySpecification"
        Me.TxtIsVisible_PartySpecification.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_PartySpecification.TabIndex = 25
        Me.TxtIsVisible_PartySpecification.Text = "TxtIsVisible_PartySpecification"
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label70.Location = New System.Drawing.Point(300, 65)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(174, 16)
        Me.Label70.TabIndex = 881
        Me.Label70.Text = "Is Visible Party Specification"
        '
        'TxtIsVisible_RejMeasure
        '
        Me.TxtIsVisible_RejMeasure.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_RejMeasure.AgLastValueTag = Nothing
        Me.TxtIsVisible_RejMeasure.AgLastValueText = Nothing
        Me.TxtIsVisible_RejMeasure.AgMandatory = False
        Me.TxtIsVisible_RejMeasure.AgMasterHelp = False
        Me.TxtIsVisible_RejMeasure.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_RejMeasure.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_RejMeasure.AgNumberRightPlaces = 0
        Me.TxtIsVisible_RejMeasure.AgPickFromLastValue = False
        Me.TxtIsVisible_RejMeasure.AgRowFilter = ""
        Me.TxtIsVisible_RejMeasure.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_RejMeasure.AgSelectedValue = Nothing
        Me.TxtIsVisible_RejMeasure.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_RejMeasure.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_RejMeasure.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_RejMeasure.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_RejMeasure.Location = New System.Drawing.Point(187, 413)
        Me.TxtIsVisible_RejMeasure.MaxLength = 3
        Me.TxtIsVisible_RejMeasure.Name = "TxtIsVisible_RejMeasure"
        Me.TxtIsVisible_RejMeasure.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_RejMeasure.TabIndex = 21
        Me.TxtIsVisible_RejMeasure.Text = "TxtIsVisible_RejMeasure"
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.Location = New System.Drawing.Point(9, 413)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(142, 16)
        Me.Label67.TabIndex = 877
        Me.Label67.Text = "Is Visible Rej. Measure"
        '
        'TxtIsVisible_RejQty
        '
        Me.TxtIsVisible_RejQty.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_RejQty.AgLastValueTag = Nothing
        Me.TxtIsVisible_RejQty.AgLastValueText = Nothing
        Me.TxtIsVisible_RejQty.AgMandatory = False
        Me.TxtIsVisible_RejQty.AgMasterHelp = False
        Me.TxtIsVisible_RejQty.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_RejQty.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_RejQty.AgNumberRightPlaces = 0
        Me.TxtIsVisible_RejQty.AgPickFromLastValue = False
        Me.TxtIsVisible_RejQty.AgRowFilter = ""
        Me.TxtIsVisible_RejQty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_RejQty.AgSelectedValue = Nothing
        Me.TxtIsVisible_RejQty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_RejQty.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_RejQty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_RejQty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_RejQty.Location = New System.Drawing.Point(187, 394)
        Me.TxtIsVisible_RejQty.MaxLength = 3
        Me.TxtIsVisible_RejQty.Name = "TxtIsVisible_RejQty"
        Me.TxtIsVisible_RejQty.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_RejQty.TabIndex = 20
        Me.TxtIsVisible_RejQty.Text = "TxtIsVisible_RejQty"
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.Location = New System.Drawing.Point(9, 394)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(113, 16)
        Me.Label66.TabIndex = 875
        Me.Label66.Text = "Is Visible Rej. Qty"
        '
        'TxtIsVisible_Qty
        '
        Me.TxtIsVisible_Qty.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_Qty.AgLastValueTag = Nothing
        Me.TxtIsVisible_Qty.AgLastValueText = Nothing
        Me.TxtIsVisible_Qty.AgMandatory = False
        Me.TxtIsVisible_Qty.AgMasterHelp = False
        Me.TxtIsVisible_Qty.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_Qty.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_Qty.AgNumberRightPlaces = 0
        Me.TxtIsVisible_Qty.AgPickFromLastValue = False
        Me.TxtIsVisible_Qty.AgRowFilter = ""
        Me.TxtIsVisible_Qty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_Qty.AgSelectedValue = Nothing
        Me.TxtIsVisible_Qty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_Qty.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_Qty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_Qty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_Qty.Location = New System.Drawing.Point(187, 242)
        Me.TxtIsVisible_Qty.MaxLength = 3
        Me.TxtIsVisible_Qty.Name = "TxtIsVisible_Qty"
        Me.TxtIsVisible_Qty.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_Qty.TabIndex = 12
        Me.TxtIsVisible_Qty.Text = "TxtIsVisible_Qty"
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label65.Location = New System.Drawing.Point(9, 242)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(86, 16)
        Me.Label65.TabIndex = 873
        Me.Label65.Text = "Is Visible Qty"
        '
        'TxtFilterInclude_SubGroup
        '
        Me.TxtFilterInclude_SubGroup.AgAllowUserToEnableMasterHelp = False
        Me.TxtFilterInclude_SubGroup.AgLastValueTag = Nothing
        Me.TxtFilterInclude_SubGroup.AgLastValueText = Nothing
        Me.TxtFilterInclude_SubGroup.AgMandatory = False
        Me.TxtFilterInclude_SubGroup.AgMasterHelp = False
        Me.TxtFilterInclude_SubGroup.AgNumberLeftPlaces = 0
        Me.TxtFilterInclude_SubGroup.AgNumberNegetiveAllow = False
        Me.TxtFilterInclude_SubGroup.AgNumberRightPlaces = 0
        Me.TxtFilterInclude_SubGroup.AgPickFromLastValue = False
        Me.TxtFilterInclude_SubGroup.AgRowFilter = ""
        Me.TxtFilterInclude_SubGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFilterInclude_SubGroup.AgSelectedValue = Nothing
        Me.TxtFilterInclude_SubGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFilterInclude_SubGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFilterInclude_SubGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFilterInclude_SubGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFilterInclude_SubGroup.Location = New System.Drawing.Point(780, 144)
        Me.TxtFilterInclude_SubGroup.MaxLength = 100
        Me.TxtFilterInclude_SubGroup.Name = "TxtFilterInclude_SubGroup"
        Me.TxtFilterInclude_SubGroup.Size = New System.Drawing.Size(188, 18)
        Me.TxtFilterInclude_SubGroup.TabIndex = 46
        Me.TxtFilterInclude_SubGroup.Text = "TxtFilterInclude_SubGroup"
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.Location = New System.Drawing.Point(581, 144)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(144, 16)
        Me.Label55.TabIndex = 851
        Me.Label55.Text = "Filter Include SubGroup"
        '
        'TxtDefault_SubCode
        '
        Me.TxtDefault_SubCode.AgAllowUserToEnableMasterHelp = False
        Me.TxtDefault_SubCode.AgLastValueTag = Nothing
        Me.TxtDefault_SubCode.AgLastValueText = Nothing
        Me.TxtDefault_SubCode.AgMandatory = False
        Me.TxtDefault_SubCode.AgMasterHelp = False
        Me.TxtDefault_SubCode.AgNumberLeftPlaces = 0
        Me.TxtDefault_SubCode.AgNumberNegetiveAllow = False
        Me.TxtDefault_SubCode.AgNumberRightPlaces = 0
        Me.TxtDefault_SubCode.AgPickFromLastValue = False
        Me.TxtDefault_SubCode.AgRowFilter = ""
        Me.TxtDefault_SubCode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDefault_SubCode.AgSelectedValue = Nothing
        Me.TxtDefault_SubCode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDefault_SubCode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDefault_SubCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDefault_SubCode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDefault_SubCode.Location = New System.Drawing.Point(780, 182)
        Me.TxtDefault_SubCode.MaxLength = 100
        Me.TxtDefault_SubCode.Name = "TxtDefault_SubCode"
        Me.TxtDefault_SubCode.Size = New System.Drawing.Size(188, 18)
        Me.TxtDefault_SubCode.TabIndex = 48
        Me.TxtDefault_SubCode.Text = "TxtDefault_SubCode"
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.Location = New System.Drawing.Point(581, 182)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(105, 16)
        Me.Label54.TabIndex = 849
        Me.Label54.Text = "Default SubCode"
        '
        'TxtIsVisible_Specification
        '
        Me.TxtIsVisible_Specification.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_Specification.AgLastValueTag = Nothing
        Me.TxtIsVisible_Specification.AgLastValueText = Nothing
        Me.TxtIsVisible_Specification.AgMandatory = False
        Me.TxtIsVisible_Specification.AgMasterHelp = False
        Me.TxtIsVisible_Specification.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_Specification.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_Specification.AgNumberRightPlaces = 0
        Me.TxtIsVisible_Specification.AgPickFromLastValue = False
        Me.TxtIsVisible_Specification.AgRowFilter = ""
        Me.TxtIsVisible_Specification.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_Specification.AgSelectedValue = Nothing
        Me.TxtIsVisible_Specification.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_Specification.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_Specification.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_Specification.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_Specification.Location = New System.Drawing.Point(479, 249)
        Me.TxtIsVisible_Specification.MaxLength = 3
        Me.TxtIsVisible_Specification.Name = "TxtIsVisible_Specification"
        Me.TxtIsVisible_Specification.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_Specification.TabIndex = 34
        Me.TxtIsVisible_Specification.Text = "TxtIsVisible_Specification"
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.Location = New System.Drawing.Point(300, 249)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(139, 16)
        Me.Label53.TabIndex = 847
        Me.Label53.Text = "Is Visible Specification"
        '
        'TxtIsVisible_Supplier
        '
        Me.TxtIsVisible_Supplier.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_Supplier.AgLastValueTag = Nothing
        Me.TxtIsVisible_Supplier.AgLastValueText = Nothing
        Me.TxtIsVisible_Supplier.AgMandatory = False
        Me.TxtIsVisible_Supplier.AgMasterHelp = False
        Me.TxtIsVisible_Supplier.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_Supplier.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_Supplier.AgNumberRightPlaces = 0
        Me.TxtIsVisible_Supplier.AgPickFromLastValue = False
        Me.TxtIsVisible_Supplier.AgRowFilter = ""
        Me.TxtIsVisible_Supplier.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_Supplier.AgSelectedValue = Nothing
        Me.TxtIsVisible_Supplier.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_Supplier.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_Supplier.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_Supplier.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_Supplier.Location = New System.Drawing.Point(479, 306)
        Me.TxtIsVisible_Supplier.MaxLength = 3
        Me.TxtIsVisible_Supplier.Name = "TxtIsVisible_Supplier"
        Me.TxtIsVisible_Supplier.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_Supplier.TabIndex = 37
        Me.TxtIsVisible_Supplier.Text = "TxtIsVisible_Supplier"
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.Location = New System.Drawing.Point(300, 306)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(112, 16)
        Me.Label47.TabIndex = 835
        Me.Label47.Text = "Is Visible Supplier"
        '
        'TxtIsVisible_RateType
        '
        Me.TxtIsVisible_RateType.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_RateType.AgLastValueTag = Nothing
        Me.TxtIsVisible_RateType.AgLastValueText = Nothing
        Me.TxtIsVisible_RateType.AgMandatory = False
        Me.TxtIsVisible_RateType.AgMasterHelp = False
        Me.TxtIsVisible_RateType.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_RateType.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_RateType.AgNumberRightPlaces = 0
        Me.TxtIsVisible_RateType.AgPickFromLastValue = False
        Me.TxtIsVisible_RateType.AgRowFilter = ""
        Me.TxtIsVisible_RateType.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_RateType.AgSelectedValue = Nothing
        Me.TxtIsVisible_RateType.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_RateType.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_RateType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_RateType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_RateType.Location = New System.Drawing.Point(479, 287)
        Me.TxtIsVisible_RateType.MaxLength = 3
        Me.TxtIsVisible_RateType.Name = "TxtIsVisible_RateType"
        Me.TxtIsVisible_RateType.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_RateType.TabIndex = 36
        Me.TxtIsVisible_RateType.Text = "TxtIsVisible_RateType"
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(300, 287)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(123, 16)
        Me.Label46.TabIndex = 833
        Me.Label46.Text = "Is Visible Rate Type"
        '
        'TxtIsVisible_BillingType
        '
        Me.TxtIsVisible_BillingType.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_BillingType.AgLastValueTag = Nothing
        Me.TxtIsVisible_BillingType.AgLastValueText = Nothing
        Me.TxtIsVisible_BillingType.AgMandatory = False
        Me.TxtIsVisible_BillingType.AgMasterHelp = False
        Me.TxtIsVisible_BillingType.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_BillingType.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_BillingType.AgNumberRightPlaces = 0
        Me.TxtIsVisible_BillingType.AgPickFromLastValue = False
        Me.TxtIsVisible_BillingType.AgRowFilter = ""
        Me.TxtIsVisible_BillingType.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_BillingType.AgSelectedValue = Nothing
        Me.TxtIsVisible_BillingType.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_BillingType.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_BillingType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_BillingType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_BillingType.Location = New System.Drawing.Point(479, 268)
        Me.TxtIsVisible_BillingType.MaxLength = 3
        Me.TxtIsVisible_BillingType.Name = "TxtIsVisible_BillingType"
        Me.TxtIsVisible_BillingType.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_BillingType.TabIndex = 35
        Me.TxtIsVisible_BillingType.Text = "TxtIsVisible_BillingType"
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.Location = New System.Drawing.Point(300, 268)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(131, 16)
        Me.Label45.TabIndex = 831
        Me.Label45.Text = "Is Visible Billing Type"
        '
        'TxtIsVisible_Rate
        '
        Me.TxtIsVisible_Rate.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_Rate.AgLastValueTag = Nothing
        Me.TxtIsVisible_Rate.AgLastValueText = Nothing
        Me.TxtIsVisible_Rate.AgMandatory = False
        Me.TxtIsVisible_Rate.AgMasterHelp = False
        Me.TxtIsVisible_Rate.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_Rate.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_Rate.AgNumberRightPlaces = 0
        Me.TxtIsVisible_Rate.AgPickFromLastValue = False
        Me.TxtIsVisible_Rate.AgRowFilter = ""
        Me.TxtIsVisible_Rate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_Rate.AgSelectedValue = Nothing
        Me.TxtIsVisible_Rate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_Rate.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_Rate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_Rate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_Rate.Location = New System.Drawing.Point(187, 299)
        Me.TxtIsVisible_Rate.MaxLength = 3
        Me.TxtIsVisible_Rate.Name = "TxtIsVisible_Rate"
        Me.TxtIsVisible_Rate.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_Rate.TabIndex = 15
        Me.TxtIsVisible_Rate.Text = "TxtIsVisible_Rate"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(9, 299)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(92, 16)
        Me.Label40.TabIndex = 829
        Me.Label40.Text = "Is Visible Rate"
        '
        'TxtIsVisible_ProcessLine
        '
        Me.TxtIsVisible_ProcessLine.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_ProcessLine.AgLastValueTag = Nothing
        Me.TxtIsVisible_ProcessLine.AgLastValueText = Nothing
        Me.TxtIsVisible_ProcessLine.AgMandatory = False
        Me.TxtIsVisible_ProcessLine.AgMasterHelp = False
        Me.TxtIsVisible_ProcessLine.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_ProcessLine.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_ProcessLine.AgNumberRightPlaces = 0
        Me.TxtIsVisible_ProcessLine.AgPickFromLastValue = False
        Me.TxtIsVisible_ProcessLine.AgRowFilter = ""
        Me.TxtIsVisible_ProcessLine.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_ProcessLine.AgSelectedValue = Nothing
        Me.TxtIsVisible_ProcessLine.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_ProcessLine.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_ProcessLine.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_ProcessLine.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_ProcessLine.Location = New System.Drawing.Point(479, 207)
        Me.TxtIsVisible_ProcessLine.MaxLength = 3
        Me.TxtIsVisible_ProcessLine.Name = "TxtIsVisible_ProcessLine"
        Me.TxtIsVisible_ProcessLine.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_ProcessLine.TabIndex = 32
        Me.TxtIsVisible_ProcessLine.Text = "TxtIsVisible_ProcessLine"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(300, 207)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(141, 16)
        Me.Label39.TabIndex = 827
        Me.Label39.Text = "Is Visible Process Line"
        '
        'TxtIsEditable_ProcessLine
        '
        Me.TxtIsEditable_ProcessLine.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsEditable_ProcessLine.AgLastValueTag = Nothing
        Me.TxtIsEditable_ProcessLine.AgLastValueText = Nothing
        Me.TxtIsEditable_ProcessLine.AgMandatory = False
        Me.TxtIsEditable_ProcessLine.AgMasterHelp = False
        Me.TxtIsEditable_ProcessLine.AgNumberLeftPlaces = 0
        Me.TxtIsEditable_ProcessLine.AgNumberNegetiveAllow = False
        Me.TxtIsEditable_ProcessLine.AgNumberRightPlaces = 0
        Me.TxtIsEditable_ProcessLine.AgPickFromLastValue = False
        Me.TxtIsEditable_ProcessLine.AgRowFilter = ""
        Me.TxtIsEditable_ProcessLine.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsEditable_ProcessLine.AgSelectedValue = Nothing
        Me.TxtIsEditable_ProcessLine.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsEditable_ProcessLine.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsEditable_ProcessLine.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsEditable_ProcessLine.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsEditable_ProcessLine.Location = New System.Drawing.Point(479, 226)
        Me.TxtIsEditable_ProcessLine.MaxLength = 3
        Me.TxtIsEditable_ProcessLine.Name = "TxtIsEditable_ProcessLine"
        Me.TxtIsEditable_ProcessLine.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsEditable_ProcessLine.TabIndex = 33
        Me.TxtIsEditable_ProcessLine.Text = "TxtIsEditable_ProcessLine"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(300, 226)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(149, 16)
        Me.Label38.TabIndex = 825
        Me.Label38.Text = "Is Editable Process Line"
        '
        'TxtIsVisible_ItemCode
        '
        Me.TxtIsVisible_ItemCode.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_ItemCode.AgLastValueTag = Nothing
        Me.TxtIsVisible_ItemCode.AgLastValueText = Nothing
        Me.TxtIsVisible_ItemCode.AgMandatory = False
        Me.TxtIsVisible_ItemCode.AgMasterHelp = False
        Me.TxtIsVisible_ItemCode.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_ItemCode.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_ItemCode.AgNumberRightPlaces = 0
        Me.TxtIsVisible_ItemCode.AgPickFromLastValue = False
        Me.TxtIsVisible_ItemCode.AgRowFilter = ""
        Me.TxtIsVisible_ItemCode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_ItemCode.AgSelectedValue = Nothing
        Me.TxtIsVisible_ItemCode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_ItemCode.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_ItemCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_ItemCode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_ItemCode.Location = New System.Drawing.Point(187, 65)
        Me.TxtIsVisible_ItemCode.MaxLength = 3
        Me.TxtIsVisible_ItemCode.Name = "TxtIsVisible_ItemCode"
        Me.TxtIsVisible_ItemCode.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_ItemCode.TabIndex = 3
        Me.TxtIsVisible_ItemCode.Text = "TxtIsVisible_ItemCode"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(9, 65)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(124, 16)
        Me.Label25.TabIndex = 823
        Me.Label25.Text = "Is Visible Item Code"
        '
        'TxtIsEditable_ItemCode
        '
        Me.TxtIsEditable_ItemCode.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsEditable_ItemCode.AgLastValueTag = Nothing
        Me.TxtIsEditable_ItemCode.AgLastValueText = Nothing
        Me.TxtIsEditable_ItemCode.AgMandatory = False
        Me.TxtIsEditable_ItemCode.AgMasterHelp = False
        Me.TxtIsEditable_ItemCode.AgNumberLeftPlaces = 0
        Me.TxtIsEditable_ItemCode.AgNumberNegetiveAllow = False
        Me.TxtIsEditable_ItemCode.AgNumberRightPlaces = 0
        Me.TxtIsEditable_ItemCode.AgPickFromLastValue = False
        Me.TxtIsEditable_ItemCode.AgRowFilter = ""
        Me.TxtIsEditable_ItemCode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsEditable_ItemCode.AgSelectedValue = Nothing
        Me.TxtIsEditable_ItemCode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsEditable_ItemCode.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsEditable_ItemCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsEditable_ItemCode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsEditable_ItemCode.Location = New System.Drawing.Point(187, 84)
        Me.TxtIsEditable_ItemCode.MaxLength = 3
        Me.TxtIsEditable_ItemCode.Name = "TxtIsEditable_ItemCode"
        Me.TxtIsEditable_ItemCode.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsEditable_ItemCode.TabIndex = 4
        Me.TxtIsEditable_ItemCode.Text = "TxtIsEditable_ItemCode"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(9, 84)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(132, 16)
        Me.Label24.TabIndex = 821
        Me.Label24.Text = "Is Editable Item Code"
        '
        'TxtIsEditable_ItemName
        '
        Me.TxtIsEditable_ItemName.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsEditable_ItemName.AgLastValueTag = Nothing
        Me.TxtIsEditable_ItemName.AgLastValueText = Nothing
        Me.TxtIsEditable_ItemName.AgMandatory = False
        Me.TxtIsEditable_ItemName.AgMasterHelp = False
        Me.TxtIsEditable_ItemName.AgNumberLeftPlaces = 0
        Me.TxtIsEditable_ItemName.AgNumberNegetiveAllow = False
        Me.TxtIsEditable_ItemName.AgNumberRightPlaces = 0
        Me.TxtIsEditable_ItemName.AgPickFromLastValue = False
        Me.TxtIsEditable_ItemName.AgRowFilter = ""
        Me.TxtIsEditable_ItemName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsEditable_ItemName.AgSelectedValue = Nothing
        Me.TxtIsEditable_ItemName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsEditable_ItemName.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsEditable_ItemName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsEditable_ItemName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsEditable_ItemName.Location = New System.Drawing.Point(187, 103)
        Me.TxtIsEditable_ItemName.MaxLength = 3
        Me.TxtIsEditable_ItemName.Name = "TxtIsEditable_ItemName"
        Me.TxtIsEditable_ItemName.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsEditable_ItemName.TabIndex = 5
        Me.TxtIsEditable_ItemName.Text = "TxtIsEditable_ItemName"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(9, 103)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(136, 16)
        Me.Label23.TabIndex = 819
        Me.Label23.Text = "Is Editable Item Name"
        '
        'TxtIsVisible_ItemUID
        '
        Me.TxtIsVisible_ItemUID.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_ItemUID.AgLastValueTag = Nothing
        Me.TxtIsVisible_ItemUID.AgLastValueText = Nothing
        Me.TxtIsVisible_ItemUID.AgMandatory = False
        Me.TxtIsVisible_ItemUID.AgMasterHelp = False
        Me.TxtIsVisible_ItemUID.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_ItemUID.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_ItemUID.AgNumberRightPlaces = 0
        Me.TxtIsVisible_ItemUID.AgPickFromLastValue = False
        Me.TxtIsVisible_ItemUID.AgRowFilter = ""
        Me.TxtIsVisible_ItemUID.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_ItemUID.AgSelectedValue = Nothing
        Me.TxtIsVisible_ItemUID.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_ItemUID.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_ItemUID.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_ItemUID.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_ItemUID.Location = New System.Drawing.Point(187, 46)
        Me.TxtIsVisible_ItemUID.MaxLength = 3
        Me.TxtIsVisible_ItemUID.Name = "TxtIsVisible_ItemUID"
        Me.TxtIsVisible_ItemUID.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_ItemUID.TabIndex = 2
        Me.TxtIsVisible_ItemUID.Text = "TxtIsVisible_ItemUID"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(9, 46)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(115, 16)
        Me.Label15.TabIndex = 817
        Me.Label15.Text = "Is Visible Item UID"
        '
        'TxtIsPostedInStockProcess
        '
        Me.TxtIsPostedInStockProcess.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsPostedInStockProcess.AgLastValueTag = Nothing
        Me.TxtIsPostedInStockProcess.AgLastValueText = Nothing
        Me.TxtIsPostedInStockProcess.AgMandatory = False
        Me.TxtIsPostedInStockProcess.AgMasterHelp = False
        Me.TxtIsPostedInStockProcess.AgNumberLeftPlaces = 0
        Me.TxtIsPostedInStockProcess.AgNumberNegetiveAllow = False
        Me.TxtIsPostedInStockProcess.AgNumberRightPlaces = 0
        Me.TxtIsPostedInStockProcess.AgPickFromLastValue = False
        Me.TxtIsPostedInStockProcess.AgRowFilter = ""
        Me.TxtIsPostedInStockProcess.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsPostedInStockProcess.AgSelectedValue = Nothing
        Me.TxtIsPostedInStockProcess.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsPostedInStockProcess.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsPostedInStockProcess.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsPostedInStockProcess.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsPostedInStockProcess.Location = New System.Drawing.Point(479, 145)
        Me.TxtIsPostedInStockProcess.MaxLength = 3
        Me.TxtIsPostedInStockProcess.Name = "TxtIsPostedInStockProcess"
        Me.TxtIsPostedInStockProcess.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsPostedInStockProcess.TabIndex = 29
        Me.TxtIsPostedInStockProcess.Text = "TxtIsPostedInStockProcess"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(300, 145)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(167, 16)
        Me.Label14.TabIndex = 815
        Me.Label14.Text = "Is Posted In Stock Process"
        '
        'TxtIsPostedInStock
        '
        Me.TxtIsPostedInStock.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsPostedInStock.AgLastValueTag = Nothing
        Me.TxtIsPostedInStock.AgLastValueText = Nothing
        Me.TxtIsPostedInStock.AgMandatory = False
        Me.TxtIsPostedInStock.AgMasterHelp = False
        Me.TxtIsPostedInStock.AgNumberLeftPlaces = 0
        Me.TxtIsPostedInStock.AgNumberNegetiveAllow = False
        Me.TxtIsPostedInStock.AgNumberRightPlaces = 0
        Me.TxtIsPostedInStock.AgPickFromLastValue = False
        Me.TxtIsPostedInStock.AgRowFilter = ""
        Me.TxtIsPostedInStock.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsPostedInStock.AgSelectedValue = Nothing
        Me.TxtIsPostedInStock.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsPostedInStock.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsPostedInStock.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsPostedInStock.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsPostedInStock.Location = New System.Drawing.Point(479, 126)
        Me.TxtIsPostedInStock.MaxLength = 3
        Me.TxtIsPostedInStock.Name = "TxtIsPostedInStock"
        Me.TxtIsPostedInStock.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsPostedInStock.TabIndex = 28
        Me.TxtIsPostedInStock.Text = "TxtIsPostedInStock"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(300, 126)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(115, 16)
        Me.Label13.TabIndex = 813
        Me.Label13.Text = "Is Posted In Stock"
        '
        'TxtIsVisible_BaleNo
        '
        Me.TxtIsVisible_BaleNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_BaleNo.AgLastValueTag = Nothing
        Me.TxtIsVisible_BaleNo.AgLastValueText = Nothing
        Me.TxtIsVisible_BaleNo.AgMandatory = False
        Me.TxtIsVisible_BaleNo.AgMasterHelp = False
        Me.TxtIsVisible_BaleNo.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_BaleNo.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_BaleNo.AgNumberRightPlaces = 0
        Me.TxtIsVisible_BaleNo.AgPickFromLastValue = False
        Me.TxtIsVisible_BaleNo.AgRowFilter = ""
        Me.TxtIsVisible_BaleNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_BaleNo.AgSelectedValue = Nothing
        Me.TxtIsVisible_BaleNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_BaleNo.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_BaleNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_BaleNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_BaleNo.Location = New System.Drawing.Point(479, 24)
        Me.TxtIsVisible_BaleNo.MaxLength = 3
        Me.TxtIsVisible_BaleNo.Name = "TxtIsVisible_BaleNo"
        Me.TxtIsVisible_BaleNo.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_BaleNo.TabIndex = 23
        Me.TxtIsVisible_BaleNo.Text = "TxtIsVisible_BaleNo"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(300, 24)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(111, 16)
        Me.Label12.TabIndex = 811
        Me.Label12.Text = "Is Visible Bale No"
        '
        'TxtIsVisible_LotNo
        '
        Me.TxtIsVisible_LotNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_LotNo.AgLastValueTag = Nothing
        Me.TxtIsVisible_LotNo.AgLastValueText = Nothing
        Me.TxtIsVisible_LotNo.AgMandatory = False
        Me.TxtIsVisible_LotNo.AgMasterHelp = False
        Me.TxtIsVisible_LotNo.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_LotNo.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_LotNo.AgNumberRightPlaces = 0
        Me.TxtIsVisible_LotNo.AgPickFromLastValue = False
        Me.TxtIsVisible_LotNo.AgRowFilter = ""
        Me.TxtIsVisible_LotNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_LotNo.AgSelectedValue = Nothing
        Me.TxtIsVisible_LotNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_LotNo.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_LotNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_LotNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_LotNo.Location = New System.Drawing.Point(479, 5)
        Me.TxtIsVisible_LotNo.MaxLength = 3
        Me.TxtIsVisible_LotNo.Name = "TxtIsVisible_LotNo"
        Me.TxtIsVisible_LotNo.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_LotNo.TabIndex = 22
        Me.TxtIsVisible_LotNo.Text = "TxtIsVisible_LotNo"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(300, 5)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(103, 16)
        Me.Label11.TabIndex = 809
        Me.Label11.Text = "Is Visible Lot No"
        '
        'TxtIsVisible_Process
        '
        Me.TxtIsVisible_Process.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_Process.AgLastValueTag = Nothing
        Me.TxtIsVisible_Process.AgLastValueText = Nothing
        Me.TxtIsVisible_Process.AgMandatory = False
        Me.TxtIsVisible_Process.AgMasterHelp = False
        Me.TxtIsVisible_Process.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_Process.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_Process.AgNumberRightPlaces = 0
        Me.TxtIsVisible_Process.AgPickFromLastValue = False
        Me.TxtIsVisible_Process.AgRowFilter = ""
        Me.TxtIsVisible_Process.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_Process.AgSelectedValue = Nothing
        Me.TxtIsVisible_Process.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_Process.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_Process.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_Process.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_Process.Location = New System.Drawing.Point(479, 188)
        Me.TxtIsVisible_Process.MaxLength = 3
        Me.TxtIsVisible_Process.Name = "TxtIsVisible_Process"
        Me.TxtIsVisible_Process.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_Process.TabIndex = 31
        Me.TxtIsVisible_Process.Text = "TxtIsVisible_Process"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(300, 188)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(113, 16)
        Me.Label10.TabIndex = 807
        Me.Label10.Text = "Is Visible Process"
        '
        'TxtIsVisible_ProdOrder
        '
        Me.TxtIsVisible_ProdOrder.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_ProdOrder.AgLastValueTag = Nothing
        Me.TxtIsVisible_ProdOrder.AgLastValueText = Nothing
        Me.TxtIsVisible_ProdOrder.AgMandatory = False
        Me.TxtIsVisible_ProdOrder.AgMasterHelp = False
        Me.TxtIsVisible_ProdOrder.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_ProdOrder.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_ProdOrder.AgNumberRightPlaces = 0
        Me.TxtIsVisible_ProdOrder.AgPickFromLastValue = False
        Me.TxtIsVisible_ProdOrder.AgRowFilter = ""
        Me.TxtIsVisible_ProdOrder.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_ProdOrder.AgSelectedValue = Nothing
        Me.TxtIsVisible_ProdOrder.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_ProdOrder.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_ProdOrder.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_ProdOrder.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_ProdOrder.Location = New System.Drawing.Point(479, 46)
        Me.TxtIsVisible_ProdOrder.MaxLength = 3
        Me.TxtIsVisible_ProdOrder.Name = "TxtIsVisible_ProdOrder"
        Me.TxtIsVisible_ProdOrder.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_ProdOrder.TabIndex = 24
        Me.TxtIsVisible_ProdOrder.Text = "TxtIsVisible_ProdOrder"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(300, 46)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(132, 16)
        Me.Label9.TabIndex = 805
        Me.Label9.Text = "Is Visible Prod. Order"
        '
        'TxtIsEditable_MeasureUnit
        '
        Me.TxtIsEditable_MeasureUnit.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsEditable_MeasureUnit.AgLastValueTag = Nothing
        Me.TxtIsEditable_MeasureUnit.AgLastValueText = Nothing
        Me.TxtIsEditable_MeasureUnit.AgMandatory = False
        Me.TxtIsEditable_MeasureUnit.AgMasterHelp = False
        Me.TxtIsEditable_MeasureUnit.AgNumberLeftPlaces = 0
        Me.TxtIsEditable_MeasureUnit.AgNumberNegetiveAllow = False
        Me.TxtIsEditable_MeasureUnit.AgNumberRightPlaces = 0
        Me.TxtIsEditable_MeasureUnit.AgPickFromLastValue = False
        Me.TxtIsEditable_MeasureUnit.AgRowFilter = ""
        Me.TxtIsEditable_MeasureUnit.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsEditable_MeasureUnit.AgSelectedValue = Nothing
        Me.TxtIsEditable_MeasureUnit.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsEditable_MeasureUnit.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsEditable_MeasureUnit.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsEditable_MeasureUnit.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsEditable_MeasureUnit.Location = New System.Drawing.Point(187, 220)
        Me.TxtIsEditable_MeasureUnit.MaxLength = 3
        Me.TxtIsEditable_MeasureUnit.Name = "TxtIsEditable_MeasureUnit"
        Me.TxtIsEditable_MeasureUnit.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsEditable_MeasureUnit.TabIndex = 11
        Me.TxtIsEditable_MeasureUnit.Text = "TxIsEditable_MeasureUnit"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(9, 220)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(150, 16)
        Me.Label8.TabIndex = 803
        Me.Label8.Text = "Is Editable Measure Unit"
        '
        'TxtIsVisible_MeasureUnit
        '
        Me.TxtIsVisible_MeasureUnit.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_MeasureUnit.AgLastValueTag = Nothing
        Me.TxtIsVisible_MeasureUnit.AgLastValueText = Nothing
        Me.TxtIsVisible_MeasureUnit.AgMandatory = False
        Me.TxtIsVisible_MeasureUnit.AgMasterHelp = False
        Me.TxtIsVisible_MeasureUnit.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_MeasureUnit.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_MeasureUnit.AgNumberRightPlaces = 0
        Me.TxtIsVisible_MeasureUnit.AgPickFromLastValue = False
        Me.TxtIsVisible_MeasureUnit.AgRowFilter = ""
        Me.TxtIsVisible_MeasureUnit.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_MeasureUnit.AgSelectedValue = Nothing
        Me.TxtIsVisible_MeasureUnit.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_MeasureUnit.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_MeasureUnit.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_MeasureUnit.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_MeasureUnit.Location = New System.Drawing.Point(187, 201)
        Me.TxtIsVisible_MeasureUnit.MaxLength = 3
        Me.TxtIsVisible_MeasureUnit.Name = "TxtIsVisible_MeasureUnit"
        Me.TxtIsVisible_MeasureUnit.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_MeasureUnit.TabIndex = 10
        Me.TxtIsVisible_MeasureUnit.Text = "TxtIsVisible_MeasureUnit"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(9, 201)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(142, 16)
        Me.Label7.TabIndex = 801
        Me.Label7.Text = "Is Visible Measure Unit"
        '
        'TxtIsEditable_Measure
        '
        Me.TxtIsEditable_Measure.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsEditable_Measure.AgLastValueTag = Nothing
        Me.TxtIsEditable_Measure.AgLastValueText = Nothing
        Me.TxtIsEditable_Measure.AgMandatory = False
        Me.TxtIsEditable_Measure.AgMasterHelp = False
        Me.TxtIsEditable_Measure.AgNumberLeftPlaces = 0
        Me.TxtIsEditable_Measure.AgNumberNegetiveAllow = False
        Me.TxtIsEditable_Measure.AgNumberRightPlaces = 0
        Me.TxtIsEditable_Measure.AgPickFromLastValue = False
        Me.TxtIsEditable_Measure.AgRowFilter = ""
        Me.TxtIsEditable_Measure.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsEditable_Measure.AgSelectedValue = Nothing
        Me.TxtIsEditable_Measure.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsEditable_Measure.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsEditable_Measure.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsEditable_Measure.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsEditable_Measure.Location = New System.Drawing.Point(187, 182)
        Me.TxtIsEditable_Measure.MaxLength = 3
        Me.TxtIsEditable_Measure.Name = "TxtIsEditable_Measure"
        Me.TxtIsEditable_Measure.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsEditable_Measure.TabIndex = 9
        Me.TxtIsEditable_Measure.Text = "TxtIsEditable_Measure"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(9, 182)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(123, 16)
        Me.Label6.TabIndex = 799
        Me.Label6.Text = "Is Editable Measure"
        '
        'TxtIsVisible_Measure
        '
        Me.TxtIsVisible_Measure.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_Measure.AgLastValueTag = Nothing
        Me.TxtIsVisible_Measure.AgLastValueText = Nothing
        Me.TxtIsVisible_Measure.AgMandatory = False
        Me.TxtIsVisible_Measure.AgMasterHelp = False
        Me.TxtIsVisible_Measure.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_Measure.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_Measure.AgNumberRightPlaces = 0
        Me.TxtIsVisible_Measure.AgPickFromLastValue = False
        Me.TxtIsVisible_Measure.AgRowFilter = ""
        Me.TxtIsVisible_Measure.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_Measure.AgSelectedValue = Nothing
        Me.TxtIsVisible_Measure.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_Measure.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_Measure.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_Measure.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_Measure.Location = New System.Drawing.Point(187, 163)
        Me.TxtIsVisible_Measure.MaxLength = 3
        Me.TxtIsVisible_Measure.Name = "TxtIsVisible_Measure"
        Me.TxtIsVisible_Measure.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_Measure.TabIndex = 8
        Me.TxtIsVisible_Measure.Text = "TxtIsVisible_Measure"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(9, 163)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(115, 16)
        Me.Label5.TabIndex = 797
        Me.Label5.Text = "Is Visible Measure"
        '
        'TxtIsEditable_MeasurePerPcs
        '
        Me.TxtIsEditable_MeasurePerPcs.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsEditable_MeasurePerPcs.AgLastValueTag = Nothing
        Me.TxtIsEditable_MeasurePerPcs.AgLastValueText = Nothing
        Me.TxtIsEditable_MeasurePerPcs.AgMandatory = False
        Me.TxtIsEditable_MeasurePerPcs.AgMasterHelp = False
        Me.TxtIsEditable_MeasurePerPcs.AgNumberLeftPlaces = 0
        Me.TxtIsEditable_MeasurePerPcs.AgNumberNegetiveAllow = False
        Me.TxtIsEditable_MeasurePerPcs.AgNumberRightPlaces = 0
        Me.TxtIsEditable_MeasurePerPcs.AgPickFromLastValue = False
        Me.TxtIsEditable_MeasurePerPcs.AgRowFilter = ""
        Me.TxtIsEditable_MeasurePerPcs.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsEditable_MeasurePerPcs.AgSelectedValue = Nothing
        Me.TxtIsEditable_MeasurePerPcs.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsEditable_MeasurePerPcs.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsEditable_MeasurePerPcs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsEditable_MeasurePerPcs.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsEditable_MeasurePerPcs.Location = New System.Drawing.Point(187, 144)
        Me.TxtIsEditable_MeasurePerPcs.MaxLength = 3
        Me.TxtIsEditable_MeasurePerPcs.Name = "TxtIsEditable_MeasurePerPcs"
        Me.TxtIsEditable_MeasurePerPcs.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsEditable_MeasurePerPcs.TabIndex = 7
        Me.TxtIsEditable_MeasurePerPcs.Text = "TxtIsEditable_MeasurePerPcs"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(9, 144)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(174, 16)
        Me.Label4.TabIndex = 795
        Me.Label4.Text = "Is Editable Measure Per Pcs"
        '
        'TxtIsVisible_MeasurePerPcs
        '
        Me.TxtIsVisible_MeasurePerPcs.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_MeasurePerPcs.AgLastValueTag = Nothing
        Me.TxtIsVisible_MeasurePerPcs.AgLastValueText = Nothing
        Me.TxtIsVisible_MeasurePerPcs.AgMandatory = False
        Me.TxtIsVisible_MeasurePerPcs.AgMasterHelp = False
        Me.TxtIsVisible_MeasurePerPcs.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_MeasurePerPcs.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_MeasurePerPcs.AgNumberRightPlaces = 0
        Me.TxtIsVisible_MeasurePerPcs.AgPickFromLastValue = False
        Me.TxtIsVisible_MeasurePerPcs.AgRowFilter = ""
        Me.TxtIsVisible_MeasurePerPcs.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_MeasurePerPcs.AgSelectedValue = Nothing
        Me.TxtIsVisible_MeasurePerPcs.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_MeasurePerPcs.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_MeasurePerPcs.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_MeasurePerPcs.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_MeasurePerPcs.Location = New System.Drawing.Point(187, 125)
        Me.TxtIsVisible_MeasurePerPcs.MaxLength = 3
        Me.TxtIsVisible_MeasurePerPcs.Name = "TxtIsVisible_MeasurePerPcs"
        Me.TxtIsVisible_MeasurePerPcs.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_MeasurePerPcs.TabIndex = 6
        Me.TxtIsVisible_MeasurePerPcs.Text = "TxtIsVisible_MeasurePerPcs"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(9, 125)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(166, 16)
        Me.Label2.TabIndex = 793
        Me.Label2.Text = "Is Visible Measure Per Pcs"
        '
        'TxtIsMandatory_SubCode
        '
        Me.TxtIsMandatory_SubCode.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsMandatory_SubCode.AgLastValueTag = Nothing
        Me.TxtIsMandatory_SubCode.AgLastValueText = "TxtIsMandatory_SubCode"
        Me.TxtIsMandatory_SubCode.AgMandatory = False
        Me.TxtIsMandatory_SubCode.AgMasterHelp = False
        Me.TxtIsMandatory_SubCode.AgNumberLeftPlaces = 0
        Me.TxtIsMandatory_SubCode.AgNumberNegetiveAllow = False
        Me.TxtIsMandatory_SubCode.AgNumberRightPlaces = 0
        Me.TxtIsMandatory_SubCode.AgPickFromLastValue = False
        Me.TxtIsMandatory_SubCode.AgRowFilter = ""
        Me.TxtIsMandatory_SubCode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsMandatory_SubCode.AgSelectedValue = Nothing
        Me.TxtIsMandatory_SubCode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsMandatory_SubCode.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsMandatory_SubCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsMandatory_SubCode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsMandatory_SubCode.Location = New System.Drawing.Point(187, 5)
        Me.TxtIsMandatory_SubCode.MaxLength = 3
        Me.TxtIsMandatory_SubCode.Name = "TxtIsMandatory_SubCode"
        Me.TxtIsMandatory_SubCode.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsMandatory_SubCode.TabIndex = 0
        Me.TxtIsMandatory_SubCode.Text = "TxtIsMandatory_SubCode"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(140, 16)
        Me.Label1.TabIndex = 791
        Me.Label1.Text = "Is Mandatory SubCode"
        '
        'TxtIsEditableSubCode
        '
        Me.TxtIsEditableSubCode.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsEditableSubCode.AgLastValueTag = Nothing
        Me.TxtIsEditableSubCode.AgLastValueText = Nothing
        Me.TxtIsEditableSubCode.AgMandatory = False
        Me.TxtIsEditableSubCode.AgMasterHelp = False
        Me.TxtIsEditableSubCode.AgNumberLeftPlaces = 0
        Me.TxtIsEditableSubCode.AgNumberNegetiveAllow = False
        Me.TxtIsEditableSubCode.AgNumberRightPlaces = 0
        Me.TxtIsEditableSubCode.AgPickFromLastValue = False
        Me.TxtIsEditableSubCode.AgRowFilter = ""
        Me.TxtIsEditableSubCode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsEditableSubCode.AgSelectedValue = Nothing
        Me.TxtIsEditableSubCode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsEditableSubCode.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsEditableSubCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsEditableSubCode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsEditableSubCode.Location = New System.Drawing.Point(187, 24)
        Me.TxtIsEditableSubCode.MaxLength = 3
        Me.TxtIsEditableSubCode.Name = "TxtIsEditableSubCode"
        Me.TxtIsEditableSubCode.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsEditableSubCode.TabIndex = 1
        Me.TxtIsEditableSubCode.Text = "TxtIsEditable_SubCode"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(9, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(126, 16)
        Me.Label3.TabIndex = 789
        Me.Label3.Text = "Is Editable SubCode"
        '
        'TxtDEFAULT_Godown
        '
        Me.TxtDEFAULT_Godown.AgAllowUserToEnableMasterHelp = False
        Me.TxtDEFAULT_Godown.AgLastValueTag = Nothing
        Me.TxtDEFAULT_Godown.AgLastValueText = Nothing
        Me.TxtDEFAULT_Godown.AgMandatory = False
        Me.TxtDEFAULT_Godown.AgMasterHelp = False
        Me.TxtDEFAULT_Godown.AgNumberLeftPlaces = 0
        Me.TxtDEFAULT_Godown.AgNumberNegetiveAllow = False
        Me.TxtDEFAULT_Godown.AgNumberRightPlaces = 0
        Me.TxtDEFAULT_Godown.AgPickFromLastValue = False
        Me.TxtDEFAULT_Godown.AgRowFilter = ""
        Me.TxtDEFAULT_Godown.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDEFAULT_Godown.AgSelectedValue = Nothing
        Me.TxtDEFAULT_Godown.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDEFAULT_Godown.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDEFAULT_Godown.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDEFAULT_Godown.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDEFAULT_Godown.Location = New System.Drawing.Point(780, 354)
        Me.TxtDEFAULT_Godown.MaxLength = 100
        Me.TxtDEFAULT_Godown.Name = "TxtDEFAULT_Godown"
        Me.TxtDEFAULT_Godown.Size = New System.Drawing.Size(188, 18)
        Me.TxtDEFAULT_Godown.TabIndex = 56
        Me.TxtDEFAULT_Godown.Text = "TxtDEFAULT_Godown"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(581, 354)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(99, 16)
        Me.Label37.TabIndex = 784
        Me.Label37.Text = "Default Godown"
        '
        'TxtFilterExclude_SubGroupDivision
        '
        Me.TxtFilterExclude_SubGroupDivision.AgAllowUserToEnableMasterHelp = False
        Me.TxtFilterExclude_SubGroupDivision.AgLastValueTag = Nothing
        Me.TxtFilterExclude_SubGroupDivision.AgLastValueText = Nothing
        Me.TxtFilterExclude_SubGroupDivision.AgMandatory = False
        Me.TxtFilterExclude_SubGroupDivision.AgMasterHelp = False
        Me.TxtFilterExclude_SubGroupDivision.AgNumberLeftPlaces = 0
        Me.TxtFilterExclude_SubGroupDivision.AgNumberNegetiveAllow = False
        Me.TxtFilterExclude_SubGroupDivision.AgNumberRightPlaces = 0
        Me.TxtFilterExclude_SubGroupDivision.AgPickFromLastValue = False
        Me.TxtFilterExclude_SubGroupDivision.AgRowFilter = ""
        Me.TxtFilterExclude_SubGroupDivision.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFilterExclude_SubGroupDivision.AgSelectedValue = Nothing
        Me.TxtFilterExclude_SubGroupDivision.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFilterExclude_SubGroupDivision.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFilterExclude_SubGroupDivision.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFilterExclude_SubGroupDivision.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFilterExclude_SubGroupDivision.Location = New System.Drawing.Point(780, 87)
        Me.TxtFilterExclude_SubGroupDivision.MaxLength = 100
        Me.TxtFilterExclude_SubGroupDivision.Name = "TxtFilterExclude_SubGroupDivision"
        Me.TxtFilterExclude_SubGroupDivision.Size = New System.Drawing.Size(188, 18)
        Me.TxtFilterExclude_SubGroupDivision.TabIndex = 43
        Me.TxtFilterExclude_SubGroupDivision.Text = "TxtFilterExclude_SubGroupDivision"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.Location = New System.Drawing.Point(581, 87)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(198, 16)
        Me.Label36.TabIndex = 782
        Me.Label36.Text = "Filter Exclude SubGroup Division"
        '
        'TxtFilterExclude_SubGroupSite
        '
        Me.TxtFilterExclude_SubGroupSite.AgAllowUserToEnableMasterHelp = False
        Me.TxtFilterExclude_SubGroupSite.AgLastValueTag = Nothing
        Me.TxtFilterExclude_SubGroupSite.AgLastValueText = Nothing
        Me.TxtFilterExclude_SubGroupSite.AgMandatory = False
        Me.TxtFilterExclude_SubGroupSite.AgMasterHelp = False
        Me.TxtFilterExclude_SubGroupSite.AgNumberLeftPlaces = 0
        Me.TxtFilterExclude_SubGroupSite.AgNumberNegetiveAllow = False
        Me.TxtFilterExclude_SubGroupSite.AgNumberRightPlaces = 0
        Me.TxtFilterExclude_SubGroupSite.AgPickFromLastValue = False
        Me.TxtFilterExclude_SubGroupSite.AgRowFilter = ""
        Me.TxtFilterExclude_SubGroupSite.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFilterExclude_SubGroupSite.AgSelectedValue = Nothing
        Me.TxtFilterExclude_SubGroupSite.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFilterExclude_SubGroupSite.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFilterExclude_SubGroupSite.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFilterExclude_SubGroupSite.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFilterExclude_SubGroupSite.Location = New System.Drawing.Point(780, 125)
        Me.TxtFilterExclude_SubGroupSite.MaxLength = 100
        Me.TxtFilterExclude_SubGroupSite.Name = "TxtFilterExclude_SubGroupSite"
        Me.TxtFilterExclude_SubGroupSite.Size = New System.Drawing.Size(188, 18)
        Me.TxtFilterExclude_SubGroupSite.TabIndex = 45
        Me.TxtFilterExclude_SubGroupSite.Text = "TxtFilterExclude_SubGroupSite"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(581, 125)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(177, 16)
        Me.Label35.TabIndex = 780
        Me.Label35.Text = "Filter Exclude SubGroup Site"
        '
        'TxtFilterInclude_ContraV_Type
        '
        Me.TxtFilterInclude_ContraV_Type.AgAllowUserToEnableMasterHelp = False
        Me.TxtFilterInclude_ContraV_Type.AgLastValueTag = Nothing
        Me.TxtFilterInclude_ContraV_Type.AgLastValueText = Nothing
        Me.TxtFilterInclude_ContraV_Type.AgMandatory = False
        Me.TxtFilterInclude_ContraV_Type.AgMasterHelp = False
        Me.TxtFilterInclude_ContraV_Type.AgNumberLeftPlaces = 0
        Me.TxtFilterInclude_ContraV_Type.AgNumberNegetiveAllow = False
        Me.TxtFilterInclude_ContraV_Type.AgNumberRightPlaces = 0
        Me.TxtFilterInclude_ContraV_Type.AgPickFromLastValue = False
        Me.TxtFilterInclude_ContraV_Type.AgRowFilter = ""
        Me.TxtFilterInclude_ContraV_Type.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFilterInclude_ContraV_Type.AgSelectedValue = Nothing
        Me.TxtFilterInclude_ContraV_Type.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFilterInclude_ContraV_Type.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFilterInclude_ContraV_Type.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFilterInclude_ContraV_Type.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFilterInclude_ContraV_Type.Location = New System.Drawing.Point(780, 373)
        Me.TxtFilterInclude_ContraV_Type.MaxLength = 100
        Me.TxtFilterInclude_ContraV_Type.Name = "TxtFilterInclude_ContraV_Type"
        Me.TxtFilterInclude_ContraV_Type.Size = New System.Drawing.Size(188, 18)
        Me.TxtFilterInclude_ContraV_Type.TabIndex = 57
        Me.TxtFilterInclude_ContraV_Type.Text = "TxtFilterInclude_ContraV_Type"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(581, 373)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(171, 16)
        Me.Label33.TabIndex = 776
        Me.Label33.Text = "Filter Include Contra V_Type"
        '
        'TxtFilterInclude_Item
        '
        Me.TxtFilterInclude_Item.AgAllowUserToEnableMasterHelp = False
        Me.TxtFilterInclude_Item.AgLastValueTag = Nothing
        Me.TxtFilterInclude_Item.AgLastValueText = Nothing
        Me.TxtFilterInclude_Item.AgMandatory = False
        Me.TxtFilterInclude_Item.AgMasterHelp = False
        Me.TxtFilterInclude_Item.AgNumberLeftPlaces = 0
        Me.TxtFilterInclude_Item.AgNumberNegetiveAllow = False
        Me.TxtFilterInclude_Item.AgNumberRightPlaces = 0
        Me.TxtFilterInclude_Item.AgPickFromLastValue = False
        Me.TxtFilterInclude_Item.AgRowFilter = ""
        Me.TxtFilterInclude_Item.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFilterInclude_Item.AgSelectedValue = Nothing
        Me.TxtFilterInclude_Item.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFilterInclude_Item.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFilterInclude_Item.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFilterInclude_Item.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFilterInclude_Item.Location = New System.Drawing.Point(780, 306)
        Me.TxtFilterInclude_Item.MaxLength = 100
        Me.TxtFilterInclude_Item.Name = "TxtFilterInclude_Item"
        Me.TxtFilterInclude_Item.Size = New System.Drawing.Size(188, 18)
        Me.TxtFilterInclude_Item.TabIndex = 54
        Me.TxtFilterInclude_Item.Text = "TxtFilterInclude_Item"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(581, 306)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(111, 16)
        Me.Label32.TabIndex = 774
        Me.Label32.Text = "Filter Include Item"
        '
        'TxtFilterExclude_Item
        '
        Me.TxtFilterExclude_Item.AgAllowUserToEnableMasterHelp = False
        Me.TxtFilterExclude_Item.AgLastValueTag = Nothing
        Me.TxtFilterExclude_Item.AgLastValueText = Nothing
        Me.TxtFilterExclude_Item.AgMandatory = False
        Me.TxtFilterExclude_Item.AgMasterHelp = False
        Me.TxtFilterExclude_Item.AgNumberLeftPlaces = 0
        Me.TxtFilterExclude_Item.AgNumberNegetiveAllow = False
        Me.TxtFilterExclude_Item.AgNumberRightPlaces = 0
        Me.TxtFilterExclude_Item.AgPickFromLastValue = False
        Me.TxtFilterExclude_Item.AgRowFilter = ""
        Me.TxtFilterExclude_Item.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFilterExclude_Item.AgSelectedValue = Nothing
        Me.TxtFilterExclude_Item.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFilterExclude_Item.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFilterExclude_Item.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFilterExclude_Item.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFilterExclude_Item.Location = New System.Drawing.Point(780, 325)
        Me.TxtFilterExclude_Item.MaxLength = 100
        Me.TxtFilterExclude_Item.Name = "TxtFilterExclude_Item"
        Me.TxtFilterExclude_Item.Size = New System.Drawing.Size(188, 18)
        Me.TxtFilterExclude_Item.TabIndex = 55
        Me.TxtFilterExclude_Item.Text = "TxtFilterExclude_Item"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(581, 325)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(117, 16)
        Me.Label31.TabIndex = 772
        Me.Label31.Text = "Filter Exclude Item"
        '
        'TxtFilterInclude_SubgroupSite
        '
        Me.TxtFilterInclude_SubgroupSite.AgAllowUserToEnableMasterHelp = False
        Me.TxtFilterInclude_SubgroupSite.AgLastValueTag = Nothing
        Me.TxtFilterInclude_SubgroupSite.AgLastValueText = Nothing
        Me.TxtFilterInclude_SubgroupSite.AgMandatory = False
        Me.TxtFilterInclude_SubgroupSite.AgMasterHelp = False
        Me.TxtFilterInclude_SubgroupSite.AgNumberLeftPlaces = 0
        Me.TxtFilterInclude_SubgroupSite.AgNumberNegetiveAllow = False
        Me.TxtFilterInclude_SubgroupSite.AgNumberRightPlaces = 0
        Me.TxtFilterInclude_SubgroupSite.AgPickFromLastValue = False
        Me.TxtFilterInclude_SubgroupSite.AgRowFilter = ""
        Me.TxtFilterInclude_SubgroupSite.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFilterInclude_SubgroupSite.AgSelectedValue = Nothing
        Me.TxtFilterInclude_SubgroupSite.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFilterInclude_SubgroupSite.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFilterInclude_SubgroupSite.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFilterInclude_SubgroupSite.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFilterInclude_SubgroupSite.Location = New System.Drawing.Point(780, 106)
        Me.TxtFilterInclude_SubgroupSite.MaxLength = 100
        Me.TxtFilterInclude_SubgroupSite.Name = "TxtFilterInclude_SubgroupSite"
        Me.TxtFilterInclude_SubgroupSite.Size = New System.Drawing.Size(188, 18)
        Me.TxtFilterInclude_SubgroupSite.TabIndex = 44
        Me.TxtFilterInclude_SubgroupSite.Text = "TxtFilterInclude_SubgroupSite"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(581, 106)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(168, 16)
        Me.Label30.TabIndex = 770
        Me.Label30.Text = "Filter Include Subgroup Site"
        '
        'TxtFilterInclude_SubgroupDivision
        '
        Me.TxtFilterInclude_SubgroupDivision.AgAllowUserToEnableMasterHelp = False
        Me.TxtFilterInclude_SubgroupDivision.AgLastValueTag = Nothing
        Me.TxtFilterInclude_SubgroupDivision.AgLastValueText = Nothing
        Me.TxtFilterInclude_SubgroupDivision.AgMandatory = False
        Me.TxtFilterInclude_SubgroupDivision.AgMasterHelp = False
        Me.TxtFilterInclude_SubgroupDivision.AgNumberLeftPlaces = 0
        Me.TxtFilterInclude_SubgroupDivision.AgNumberNegetiveAllow = False
        Me.TxtFilterInclude_SubgroupDivision.AgNumberRightPlaces = 0
        Me.TxtFilterInclude_SubgroupDivision.AgPickFromLastValue = False
        Me.TxtFilterInclude_SubgroupDivision.AgRowFilter = ""
        Me.TxtFilterInclude_SubgroupDivision.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFilterInclude_SubgroupDivision.AgSelectedValue = Nothing
        Me.TxtFilterInclude_SubgroupDivision.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFilterInclude_SubgroupDivision.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFilterInclude_SubgroupDivision.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFilterInclude_SubgroupDivision.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFilterInclude_SubgroupDivision.Location = New System.Drawing.Point(780, 68)
        Me.TxtFilterInclude_SubgroupDivision.MaxLength = 100
        Me.TxtFilterInclude_SubgroupDivision.Name = "TxtFilterInclude_SubgroupDivision"
        Me.TxtFilterInclude_SubgroupDivision.Size = New System.Drawing.Size(188, 18)
        Me.TxtFilterInclude_SubgroupDivision.TabIndex = 42
        Me.TxtFilterInclude_SubgroupDivision.Text = "TxtFilterInclude_SubgroupDivision"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(581, 68)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(189, 16)
        Me.Label29.TabIndex = 768
        Me.Label29.Text = "Filter Include Subgroup Division"
        '
        'TxtFilterInclude_ItemSite
        '
        Me.TxtFilterInclude_ItemSite.AgAllowUserToEnableMasterHelp = False
        Me.TxtFilterInclude_ItemSite.AgLastValueTag = Nothing
        Me.TxtFilterInclude_ItemSite.AgLastValueText = Nothing
        Me.TxtFilterInclude_ItemSite.AgMandatory = False
        Me.TxtFilterInclude_ItemSite.AgMasterHelp = False
        Me.TxtFilterInclude_ItemSite.AgNumberLeftPlaces = 0
        Me.TxtFilterInclude_ItemSite.AgNumberNegetiveAllow = False
        Me.TxtFilterInclude_ItemSite.AgNumberRightPlaces = 0
        Me.TxtFilterInclude_ItemSite.AgPickFromLastValue = False
        Me.TxtFilterInclude_ItemSite.AgRowFilter = ""
        Me.TxtFilterInclude_ItemSite.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFilterInclude_ItemSite.AgSelectedValue = Nothing
        Me.TxtFilterInclude_ItemSite.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFilterInclude_ItemSite.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFilterInclude_ItemSite.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFilterInclude_ItemSite.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFilterInclude_ItemSite.Location = New System.Drawing.Point(780, 287)
        Me.TxtFilterInclude_ItemSite.MaxLength = 100
        Me.TxtFilterInclude_ItemSite.Name = "TxtFilterInclude_ItemSite"
        Me.TxtFilterInclude_ItemSite.Size = New System.Drawing.Size(188, 18)
        Me.TxtFilterInclude_ItemSite.TabIndex = 53
        Me.TxtFilterInclude_ItemSite.Text = "TxtFilterInclude_ItemSite"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(581, 287)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(138, 16)
        Me.Label28.TabIndex = 766
        Me.Label28.Text = "Filter Include Item Site"
        '
        'TxtFilterInclude_Process
        '
        Me.TxtFilterInclude_Process.AgAllowUserToEnableMasterHelp = False
        Me.TxtFilterInclude_Process.AgLastValueTag = Nothing
        Me.TxtFilterInclude_Process.AgLastValueText = Nothing
        Me.TxtFilterInclude_Process.AgMandatory = False
        Me.TxtFilterInclude_Process.AgMasterHelp = False
        Me.TxtFilterInclude_Process.AgNumberLeftPlaces = 0
        Me.TxtFilterInclude_Process.AgNumberNegetiveAllow = False
        Me.TxtFilterInclude_Process.AgNumberRightPlaces = 0
        Me.TxtFilterInclude_Process.AgPickFromLastValue = False
        Me.TxtFilterInclude_Process.AgRowFilter = ""
        Me.TxtFilterInclude_Process.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFilterInclude_Process.AgSelectedValue = Nothing
        Me.TxtFilterInclude_Process.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFilterInclude_Process.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFilterInclude_Process.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFilterInclude_Process.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFilterInclude_Process.Location = New System.Drawing.Point(780, 5)
        Me.TxtFilterInclude_Process.MaxLength = 100
        Me.TxtFilterInclude_Process.Name = "TxtFilterInclude_Process"
        Me.TxtFilterInclude_Process.Size = New System.Drawing.Size(188, 18)
        Me.TxtFilterInclude_Process.TabIndex = 39
        Me.TxtFilterInclude_Process.Text = "TxtFilterInclude_Process"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(581, 5)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(134, 16)
        Me.Label22.TabIndex = 764
        Me.Label22.Text = "Filter Include Process"
        '
        'TxtFilterInclude_AcGroup
        '
        Me.TxtFilterInclude_AcGroup.AgAllowUserToEnableMasterHelp = False
        Me.TxtFilterInclude_AcGroup.AgLastValueTag = Nothing
        Me.TxtFilterInclude_AcGroup.AgLastValueText = Nothing
        Me.TxtFilterInclude_AcGroup.AgMandatory = False
        Me.TxtFilterInclude_AcGroup.AgMasterHelp = False
        Me.TxtFilterInclude_AcGroup.AgNumberLeftPlaces = 0
        Me.TxtFilterInclude_AcGroup.AgNumberNegetiveAllow = False
        Me.TxtFilterInclude_AcGroup.AgNumberRightPlaces = 0
        Me.TxtFilterInclude_AcGroup.AgPickFromLastValue = False
        Me.TxtFilterInclude_AcGroup.AgRowFilter = ""
        Me.TxtFilterInclude_AcGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFilterInclude_AcGroup.AgSelectedValue = Nothing
        Me.TxtFilterInclude_AcGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFilterInclude_AcGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFilterInclude_AcGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFilterInclude_AcGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFilterInclude_AcGroup.Location = New System.Drawing.Point(780, 30)
        Me.TxtFilterInclude_AcGroup.MaxLength = 100
        Me.TxtFilterInclude_AcGroup.Name = "TxtFilterInclude_AcGroup"
        Me.TxtFilterInclude_AcGroup.Size = New System.Drawing.Size(188, 18)
        Me.TxtFilterInclude_AcGroup.TabIndex = 40
        Me.TxtFilterInclude_AcGroup.Text = "TxtFilterInclude_AcGroup"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(581, 30)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(146, 16)
        Me.Label21.TabIndex = 762
        Me.Label21.Text = "Filter Include A/C Group"
        '
        'TxtFilterExclude_AcGroup
        '
        Me.TxtFilterExclude_AcGroup.AgAllowUserToEnableMasterHelp = False
        Me.TxtFilterExclude_AcGroup.AgLastValueTag = Nothing
        Me.TxtFilterExclude_AcGroup.AgLastValueText = Nothing
        Me.TxtFilterExclude_AcGroup.AgMandatory = False
        Me.TxtFilterExclude_AcGroup.AgMasterHelp = False
        Me.TxtFilterExclude_AcGroup.AgNumberLeftPlaces = 0
        Me.TxtFilterExclude_AcGroup.AgNumberNegetiveAllow = False
        Me.TxtFilterExclude_AcGroup.AgNumberRightPlaces = 0
        Me.TxtFilterExclude_AcGroup.AgPickFromLastValue = False
        Me.TxtFilterExclude_AcGroup.AgRowFilter = ""
        Me.TxtFilterExclude_AcGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFilterExclude_AcGroup.AgSelectedValue = Nothing
        Me.TxtFilterExclude_AcGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFilterExclude_AcGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFilterExclude_AcGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFilterExclude_AcGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFilterExclude_AcGroup.Location = New System.Drawing.Point(780, 49)
        Me.TxtFilterExclude_AcGroup.MaxLength = 100
        Me.TxtFilterExclude_AcGroup.Name = "TxtFilterExclude_AcGroup"
        Me.TxtFilterExclude_AcGroup.Size = New System.Drawing.Size(188, 18)
        Me.TxtFilterExclude_AcGroup.TabIndex = 41
        Me.TxtFilterExclude_AcGroup.Text = "TxtFilterExclude_AcGroup"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(581, 49)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(152, 16)
        Me.Label20.TabIndex = 760
        Me.Label20.Text = "Filter Exclude A/C Group"
        '
        'TxtFilterInclude_ItemType
        '
        Me.TxtFilterInclude_ItemType.AgAllowUserToEnableMasterHelp = False
        Me.TxtFilterInclude_ItemType.AgLastValueTag = Nothing
        Me.TxtFilterInclude_ItemType.AgLastValueText = Nothing
        Me.TxtFilterInclude_ItemType.AgMandatory = False
        Me.TxtFilterInclude_ItemType.AgMasterHelp = False
        Me.TxtFilterInclude_ItemType.AgNumberLeftPlaces = 0
        Me.TxtFilterInclude_ItemType.AgNumberNegetiveAllow = False
        Me.TxtFilterInclude_ItemType.AgNumberRightPlaces = 0
        Me.TxtFilterInclude_ItemType.AgPickFromLastValue = False
        Me.TxtFilterInclude_ItemType.AgRowFilter = ""
        Me.TxtFilterInclude_ItemType.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFilterInclude_ItemType.AgSelectedValue = Nothing
        Me.TxtFilterInclude_ItemType.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFilterInclude_ItemType.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFilterInclude_ItemType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFilterInclude_ItemType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFilterInclude_ItemType.Location = New System.Drawing.Point(780, 211)
        Me.TxtFilterInclude_ItemType.MaxLength = 100
        Me.TxtFilterInclude_ItemType.Name = "TxtFilterInclude_ItemType"
        Me.TxtFilterInclude_ItemType.Size = New System.Drawing.Size(188, 18)
        Me.TxtFilterInclude_ItemType.TabIndex = 49
        Me.TxtFilterInclude_ItemType.Text = "TxtFilterInclude_ItemType"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(581, 211)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(142, 16)
        Me.Label19.TabIndex = 758
        Me.Label19.Text = "Filter Include Item Type"
        '
        'TxtFilterInclude_ItemGroup
        '
        Me.TxtFilterInclude_ItemGroup.AgAllowUserToEnableMasterHelp = False
        Me.TxtFilterInclude_ItemGroup.AgLastValueTag = Nothing
        Me.TxtFilterInclude_ItemGroup.AgLastValueText = Nothing
        Me.TxtFilterInclude_ItemGroup.AgMandatory = False
        Me.TxtFilterInclude_ItemGroup.AgMasterHelp = False
        Me.TxtFilterInclude_ItemGroup.AgNumberLeftPlaces = 0
        Me.TxtFilterInclude_ItemGroup.AgNumberNegetiveAllow = False
        Me.TxtFilterInclude_ItemGroup.AgNumberRightPlaces = 0
        Me.TxtFilterInclude_ItemGroup.AgPickFromLastValue = False
        Me.TxtFilterInclude_ItemGroup.AgRowFilter = ""
        Me.TxtFilterInclude_ItemGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFilterInclude_ItemGroup.AgSelectedValue = Nothing
        Me.TxtFilterInclude_ItemGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFilterInclude_ItemGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFilterInclude_ItemGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFilterInclude_ItemGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFilterInclude_ItemGroup.Location = New System.Drawing.Point(780, 230)
        Me.TxtFilterInclude_ItemGroup.MaxLength = 100
        Me.TxtFilterInclude_ItemGroup.Name = "TxtFilterInclude_ItemGroup"
        Me.TxtFilterInclude_ItemGroup.Size = New System.Drawing.Size(188, 18)
        Me.TxtFilterInclude_ItemGroup.TabIndex = 50
        Me.TxtFilterInclude_ItemGroup.Text = "TxtFilterInclude_ItemGroup"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(581, 230)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(150, 16)
        Me.Label18.TabIndex = 756
        Me.Label18.Text = "Filter Include Item Group"
        '
        'TxtFilterExclude_ItemGroup
        '
        Me.TxtFilterExclude_ItemGroup.AgAllowUserToEnableMasterHelp = False
        Me.TxtFilterExclude_ItemGroup.AgLastValueTag = Nothing
        Me.TxtFilterExclude_ItemGroup.AgLastValueText = Nothing
        Me.TxtFilterExclude_ItemGroup.AgMandatory = False
        Me.TxtFilterExclude_ItemGroup.AgMasterHelp = False
        Me.TxtFilterExclude_ItemGroup.AgNumberLeftPlaces = 0
        Me.TxtFilterExclude_ItemGroup.AgNumberNegetiveAllow = False
        Me.TxtFilterExclude_ItemGroup.AgNumberRightPlaces = 0
        Me.TxtFilterExclude_ItemGroup.AgPickFromLastValue = False
        Me.TxtFilterExclude_ItemGroup.AgRowFilter = ""
        Me.TxtFilterExclude_ItemGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFilterExclude_ItemGroup.AgSelectedValue = Nothing
        Me.TxtFilterExclude_ItemGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFilterExclude_ItemGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFilterExclude_ItemGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFilterExclude_ItemGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFilterExclude_ItemGroup.Location = New System.Drawing.Point(780, 249)
        Me.TxtFilterExclude_ItemGroup.MaxLength = 100
        Me.TxtFilterExclude_ItemGroup.Name = "TxtFilterExclude_ItemGroup"
        Me.TxtFilterExclude_ItemGroup.Size = New System.Drawing.Size(188, 18)
        Me.TxtFilterExclude_ItemGroup.TabIndex = 51
        Me.TxtFilterExclude_ItemGroup.Text = "TxtFilterExclude_ItemGroup"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(581, 249)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(156, 16)
        Me.Label17.TabIndex = 754
        Me.Label17.Text = "Filter Exclude Item Group"
        '
        'TxtFilterInclude_ItemDivision
        '
        Me.TxtFilterInclude_ItemDivision.AgAllowUserToEnableMasterHelp = False
        Me.TxtFilterInclude_ItemDivision.AgLastValueTag = Nothing
        Me.TxtFilterInclude_ItemDivision.AgLastValueText = Nothing
        Me.TxtFilterInclude_ItemDivision.AgMandatory = False
        Me.TxtFilterInclude_ItemDivision.AgMasterHelp = False
        Me.TxtFilterInclude_ItemDivision.AgNumberLeftPlaces = 0
        Me.TxtFilterInclude_ItemDivision.AgNumberNegetiveAllow = False
        Me.TxtFilterInclude_ItemDivision.AgNumberRightPlaces = 0
        Me.TxtFilterInclude_ItemDivision.AgPickFromLastValue = False
        Me.TxtFilterInclude_ItemDivision.AgRowFilter = ""
        Me.TxtFilterInclude_ItemDivision.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFilterInclude_ItemDivision.AgSelectedValue = Nothing
        Me.TxtFilterInclude_ItemDivision.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFilterInclude_ItemDivision.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFilterInclude_ItemDivision.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFilterInclude_ItemDivision.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFilterInclude_ItemDivision.Location = New System.Drawing.Point(780, 268)
        Me.TxtFilterInclude_ItemDivision.MaxLength = 100
        Me.TxtFilterInclude_ItemDivision.Name = "TxtFilterInclude_ItemDivision"
        Me.TxtFilterInclude_ItemDivision.Size = New System.Drawing.Size(188, 18)
        Me.TxtFilterInclude_ItemDivision.TabIndex = 52
        Me.TxtFilterInclude_ItemDivision.Text = "TxtFilterInclude_ItemDivision"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(581, 268)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(159, 16)
        Me.Label16.TabIndex = 752
        Me.Label16.Text = "Filter Include Item Division"
        '
        'TP2
        '
        Me.TP2.BackColor = System.Drawing.Color.Gainsboro
        Me.TP2.Controls.Add(Me.TxtIsPostInSaleInvoice)
        Me.TP2.Controls.Add(Me.Label34)
        Me.TP2.Controls.Add(Me.TxtIsVisible_FreeMeasure)
        Me.TP2.Controls.Add(Me.Label68)
        Me.TP2.Controls.Add(Me.TxtIsVisible_PartySKU)
        Me.TP2.Controls.Add(Me.Label78)
        Me.TP2.Controls.Add(Me.TXTIsVisible_ExpiryDate)
        Me.TP2.Controls.Add(Me.Label77)
        Me.TP2.Controls.Add(Me.TxtIsVisible_SaleRate)
        Me.TP2.Controls.Add(Me.Label74)
        Me.TP2.Controls.Add(Me.TxtIsEditable_ProfitMarginPer)
        Me.TP2.Controls.Add(Me.Label73)
        Me.TP2.Controls.Add(Me.TxtIsVisible_ProfitMarginPer)
        Me.TP2.Controls.Add(Me.Label72)
        Me.TP2.Controls.Add(Me.TxtIsVisible_PartyUPC)
        Me.TP2.Controls.Add(Me.Label69)
        Me.TP2.Controls.Add(Me.TxtIsVisible_ShippingDetail)
        Me.TP2.Controls.Add(Me.Label64)
        Me.TP2.Controls.Add(Me.TxtIsVisible_DeliveryDetail)
        Me.TP2.Controls.Add(Me.Label63)
        Me.TP2.Controls.Add(Me.TxtIsVisible_Deal)
        Me.TP2.Controls.Add(Me.Label62)
        Me.TP2.Controls.Add(Me.TxtIsVisible_MRP)
        Me.TP2.Controls.Add(Me.Label61)
        Me.TP2.Controls.Add(Me.TxtIsVisible_FreeQty)
        Me.TP2.Controls.Add(Me.Label52)
        Me.TP2.Controls.Add(Me.TxtIsVisible_PurchIndent)
        Me.TP2.Controls.Add(Me.Label51)
        Me.TP2.Controls.Add(Me.TxtIsVisible_PurchQuotation)
        Me.TP2.Controls.Add(Me.Label50)
        Me.TP2.Controls.Add(Me.TxtShowRecordCount)
        Me.TP2.Controls.Add(Me.Label49)
        Me.TP2.Controls.Add(Me.TxtShowLastPoRates)
        Me.TP2.Controls.Add(Me.Label48)
        Me.TP2.Location = New System.Drawing.Point(4, 22)
        Me.TP2.Name = "TP2"
        Me.TP2.Padding = New System.Windows.Forms.Padding(3)
        Me.TP2.Size = New System.Drawing.Size(978, 455)
        Me.TP2.TabIndex = 1
        Me.TP2.Text = "Sale/Purchase"
        '
        'TxtIsPostInSaleInvoice
        '
        Me.TxtIsPostInSaleInvoice.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsPostInSaleInvoice.AgLastValueTag = Nothing
        Me.TxtIsPostInSaleInvoice.AgLastValueText = Nothing
        Me.TxtIsPostInSaleInvoice.AgMandatory = False
        Me.TxtIsPostInSaleInvoice.AgMasterHelp = False
        Me.TxtIsPostInSaleInvoice.AgNumberLeftPlaces = 0
        Me.TxtIsPostInSaleInvoice.AgNumberNegetiveAllow = False
        Me.TxtIsPostInSaleInvoice.AgNumberRightPlaces = 0
        Me.TxtIsPostInSaleInvoice.AgPickFromLastValue = False
        Me.TxtIsPostInSaleInvoice.AgRowFilter = ""
        Me.TxtIsPostInSaleInvoice.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsPostInSaleInvoice.AgSelectedValue = Nothing
        Me.TxtIsPostInSaleInvoice.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsPostInSaleInvoice.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsPostInSaleInvoice.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsPostInSaleInvoice.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsPostInSaleInvoice.Location = New System.Drawing.Point(232, 353)
        Me.TxtIsPostInSaleInvoice.MaxLength = 3
        Me.TxtIsPostInSaleInvoice.Name = "TxtIsPostInSaleInvoice"
        Me.TxtIsPostInSaleInvoice.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsPostInSaleInvoice.TabIndex = 17
        Me.TxtIsPostInSaleInvoice.Text = "TxtIsPostInSaleInvoice"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(37, 350)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(136, 16)
        Me.Label34.TabIndex = 921
        Me.Label34.Text = "Is Post In Sale Invoice"
        '
        'TxtIsVisible_FreeMeasure
        '
        Me.TxtIsVisible_FreeMeasure.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_FreeMeasure.AgLastValueTag = Nothing
        Me.TxtIsVisible_FreeMeasure.AgLastValueText = Nothing
        Me.TxtIsVisible_FreeMeasure.AgMandatory = False
        Me.TxtIsVisible_FreeMeasure.AgMasterHelp = False
        Me.TxtIsVisible_FreeMeasure.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_FreeMeasure.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_FreeMeasure.AgNumberRightPlaces = 0
        Me.TxtIsVisible_FreeMeasure.AgPickFromLastValue = False
        Me.TxtIsVisible_FreeMeasure.AgRowFilter = ""
        Me.TxtIsVisible_FreeMeasure.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_FreeMeasure.AgSelectedValue = Nothing
        Me.TxtIsVisible_FreeMeasure.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_FreeMeasure.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_FreeMeasure.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_FreeMeasure.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_FreeMeasure.Location = New System.Drawing.Point(232, 144)
        Me.TxtIsVisible_FreeMeasure.MaxLength = 3
        Me.TxtIsVisible_FreeMeasure.Name = "TxtIsVisible_FreeMeasure"
        Me.TxtIsVisible_FreeMeasure.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_FreeMeasure.TabIndex = 6
        Me.TxtIsVisible_FreeMeasure.Text = "TxtIsVisible_FreeMeasure"
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label68.Location = New System.Drawing.Point(37, 144)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(183, 16)
        Me.Label68.TabIndex = 919
        Me.Label68.Text = "Is Visible Free/Waist Measure"
        '
        'TxtIsVisible_PartySKU
        '
        Me.TxtIsVisible_PartySKU.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_PartySKU.AgLastValueTag = Nothing
        Me.TxtIsVisible_PartySKU.AgLastValueText = Nothing
        Me.TxtIsVisible_PartySKU.AgMandatory = False
        Me.TxtIsVisible_PartySKU.AgMasterHelp = False
        Me.TxtIsVisible_PartySKU.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_PartySKU.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_PartySKU.AgNumberRightPlaces = 0
        Me.TxtIsVisible_PartySKU.AgPickFromLastValue = False
        Me.TxtIsVisible_PartySKU.AgRowFilter = ""
        Me.TxtIsVisible_PartySKU.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_PartySKU.AgSelectedValue = Nothing
        Me.TxtIsVisible_PartySKU.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_PartySKU.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_PartySKU.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_PartySKU.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_PartySKU.Location = New System.Drawing.Point(232, 239)
        Me.TxtIsVisible_PartySKU.MaxLength = 3
        Me.TxtIsVisible_PartySKU.Name = "TxtIsVisible_PartySKU"
        Me.TxtIsVisible_PartySKU.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_PartySKU.TabIndex = 11
        Me.TxtIsVisible_PartySKU.Text = "TxtIsVisible_PartySKU"
        '
        'Label78
        '
        Me.Label78.AutoSize = True
        Me.Label78.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label78.Location = New System.Drawing.Point(37, 239)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(127, 16)
        Me.Label78.TabIndex = 917
        Me.Label78.Text = "Is Visible Party SKU"
        '
        'TXTIsVisible_ExpiryDate
        '
        Me.TXTIsVisible_ExpiryDate.AgAllowUserToEnableMasterHelp = False
        Me.TXTIsVisible_ExpiryDate.AgLastValueTag = Nothing
        Me.TXTIsVisible_ExpiryDate.AgLastValueText = Nothing
        Me.TXTIsVisible_ExpiryDate.AgMandatory = False
        Me.TXTIsVisible_ExpiryDate.AgMasterHelp = False
        Me.TXTIsVisible_ExpiryDate.AgNumberLeftPlaces = 0
        Me.TXTIsVisible_ExpiryDate.AgNumberNegetiveAllow = False
        Me.TXTIsVisible_ExpiryDate.AgNumberRightPlaces = 0
        Me.TXTIsVisible_ExpiryDate.AgPickFromLastValue = False
        Me.TXTIsVisible_ExpiryDate.AgRowFilter = ""
        Me.TXTIsVisible_ExpiryDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TXTIsVisible_ExpiryDate.AgSelectedValue = Nothing
        Me.TXTIsVisible_ExpiryDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TXTIsVisible_ExpiryDate.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TXTIsVisible_ExpiryDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TXTIsVisible_ExpiryDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TXTIsVisible_ExpiryDate.Location = New System.Drawing.Point(232, 315)
        Me.TXTIsVisible_ExpiryDate.MaxLength = 3
        Me.TXTIsVisible_ExpiryDate.Name = "TXTIsVisible_ExpiryDate"
        Me.TXTIsVisible_ExpiryDate.Size = New System.Drawing.Size(60, 18)
        Me.TXTIsVisible_ExpiryDate.TabIndex = 15
        Me.TXTIsVisible_ExpiryDate.Text = "TxtIsVisible_ExpiryDate"
        '
        'Label77
        '
        Me.Label77.AutoSize = True
        Me.Label77.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label77.Location = New System.Drawing.Point(37, 315)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(133, 16)
        Me.Label77.TabIndex = 915
        Me.Label77.Text = "Is Visible Expiry Date"
        '
        'TxtIsVisible_SaleRate
        '
        Me.TxtIsVisible_SaleRate.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_SaleRate.AgLastValueTag = Nothing
        Me.TxtIsVisible_SaleRate.AgLastValueText = Nothing
        Me.TxtIsVisible_SaleRate.AgMandatory = False
        Me.TxtIsVisible_SaleRate.AgMasterHelp = False
        Me.TxtIsVisible_SaleRate.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_SaleRate.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_SaleRate.AgNumberRightPlaces = 0
        Me.TxtIsVisible_SaleRate.AgPickFromLastValue = False
        Me.TxtIsVisible_SaleRate.AgRowFilter = ""
        Me.TxtIsVisible_SaleRate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_SaleRate.AgSelectedValue = Nothing
        Me.TxtIsVisible_SaleRate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_SaleRate.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_SaleRate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_SaleRate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_SaleRate.Location = New System.Drawing.Point(232, 334)
        Me.TxtIsVisible_SaleRate.MaxLength = 3
        Me.TxtIsVisible_SaleRate.Name = "TxtIsVisible_SaleRate"
        Me.TxtIsVisible_SaleRate.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_SaleRate.TabIndex = 16
        Me.TxtIsVisible_SaleRate.Text = "TxtIsVisible_SaleRate"
        '
        'Label74
        '
        Me.Label74.AutoSize = True
        Me.Label74.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label74.Location = New System.Drawing.Point(37, 334)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(122, 16)
        Me.Label74.TabIndex = 913
        Me.Label74.Text = "Is Visible Sale Rate"
        '
        'TxtIsEditable_ProfitMarginPer
        '
        Me.TxtIsEditable_ProfitMarginPer.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsEditable_ProfitMarginPer.AgLastValueTag = Nothing
        Me.TxtIsEditable_ProfitMarginPer.AgLastValueText = Nothing
        Me.TxtIsEditable_ProfitMarginPer.AgMandatory = False
        Me.TxtIsEditable_ProfitMarginPer.AgMasterHelp = False
        Me.TxtIsEditable_ProfitMarginPer.AgNumberLeftPlaces = 0
        Me.TxtIsEditable_ProfitMarginPer.AgNumberNegetiveAllow = False
        Me.TxtIsEditable_ProfitMarginPer.AgNumberRightPlaces = 0
        Me.TxtIsEditable_ProfitMarginPer.AgPickFromLastValue = False
        Me.TxtIsEditable_ProfitMarginPer.AgRowFilter = ""
        Me.TxtIsEditable_ProfitMarginPer.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsEditable_ProfitMarginPer.AgSelectedValue = Nothing
        Me.TxtIsEditable_ProfitMarginPer.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsEditable_ProfitMarginPer.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsEditable_ProfitMarginPer.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsEditable_ProfitMarginPer.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsEditable_ProfitMarginPer.Location = New System.Drawing.Point(232, 296)
        Me.TxtIsEditable_ProfitMarginPer.MaxLength = 3
        Me.TxtIsEditable_ProfitMarginPer.Name = "TxtIsEditable_ProfitMarginPer"
        Me.TxtIsEditable_ProfitMarginPer.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsEditable_ProfitMarginPer.TabIndex = 14
        Me.TxtIsEditable_ProfitMarginPer.Text = "TxtIsEditable_ProfitMarginPer"
        '
        'Label73
        '
        Me.Label73.AutoSize = True
        Me.Label73.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label73.Location = New System.Drawing.Point(37, 296)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(170, 16)
        Me.Label73.TabIndex = 911
        Me.Label73.Text = "Is Editable Profit Margin Per"
        '
        'TxtIsVisible_ProfitMarginPer
        '
        Me.TxtIsVisible_ProfitMarginPer.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_ProfitMarginPer.AgLastValueTag = Nothing
        Me.TxtIsVisible_ProfitMarginPer.AgLastValueText = Nothing
        Me.TxtIsVisible_ProfitMarginPer.AgMandatory = False
        Me.TxtIsVisible_ProfitMarginPer.AgMasterHelp = False
        Me.TxtIsVisible_ProfitMarginPer.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_ProfitMarginPer.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_ProfitMarginPer.AgNumberRightPlaces = 0
        Me.TxtIsVisible_ProfitMarginPer.AgPickFromLastValue = False
        Me.TxtIsVisible_ProfitMarginPer.AgRowFilter = ""
        Me.TxtIsVisible_ProfitMarginPer.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_ProfitMarginPer.AgSelectedValue = Nothing
        Me.TxtIsVisible_ProfitMarginPer.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_ProfitMarginPer.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_ProfitMarginPer.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_ProfitMarginPer.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_ProfitMarginPer.Location = New System.Drawing.Point(232, 277)
        Me.TxtIsVisible_ProfitMarginPer.MaxLength = 3
        Me.TxtIsVisible_ProfitMarginPer.Name = "TxtIsVisible_ProfitMarginPer"
        Me.TxtIsVisible_ProfitMarginPer.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_ProfitMarginPer.TabIndex = 13
        Me.TxtIsVisible_ProfitMarginPer.Text = "TxtIsVisible_ProfitMarginPer"
        '
        'Label72
        '
        Me.Label72.AutoSize = True
        Me.Label72.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label72.Location = New System.Drawing.Point(37, 277)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(162, 16)
        Me.Label72.TabIndex = 909
        Me.Label72.Text = "Is Visible Profit Margin Per"
        '
        'TxtIsVisible_PartyUPC
        '
        Me.TxtIsVisible_PartyUPC.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_PartyUPC.AgLastValueTag = Nothing
        Me.TxtIsVisible_PartyUPC.AgLastValueText = Nothing
        Me.TxtIsVisible_PartyUPC.AgMandatory = False
        Me.TxtIsVisible_PartyUPC.AgMasterHelp = False
        Me.TxtIsVisible_PartyUPC.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_PartyUPC.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_PartyUPC.AgNumberRightPlaces = 0
        Me.TxtIsVisible_PartyUPC.AgPickFromLastValue = False
        Me.TxtIsVisible_PartyUPC.AgRowFilter = ""
        Me.TxtIsVisible_PartyUPC.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_PartyUPC.AgSelectedValue = Nothing
        Me.TxtIsVisible_PartyUPC.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_PartyUPC.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_PartyUPC.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_PartyUPC.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_PartyUPC.Location = New System.Drawing.Point(232, 258)
        Me.TxtIsVisible_PartyUPC.MaxLength = 3
        Me.TxtIsVisible_PartyUPC.Name = "TxtIsVisible_PartyUPC"
        Me.TxtIsVisible_PartyUPC.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_PartyUPC.TabIndex = 12
        Me.TxtIsVisible_PartyUPC.Text = "TxtIsVisible_PartyUPC"
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label69.Location = New System.Drawing.Point(37, 258)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(127, 16)
        Me.Label69.TabIndex = 873
        Me.Label69.Text = "Is Visible Party UPC"
        '
        'TxtIsVisible_ShippingDetail
        '
        Me.TxtIsVisible_ShippingDetail.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_ShippingDetail.AgLastValueTag = Nothing
        Me.TxtIsVisible_ShippingDetail.AgLastValueText = Nothing
        Me.TxtIsVisible_ShippingDetail.AgMandatory = False
        Me.TxtIsVisible_ShippingDetail.AgMasterHelp = False
        Me.TxtIsVisible_ShippingDetail.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_ShippingDetail.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_ShippingDetail.AgNumberRightPlaces = 0
        Me.TxtIsVisible_ShippingDetail.AgPickFromLastValue = False
        Me.TxtIsVisible_ShippingDetail.AgRowFilter = ""
        Me.TxtIsVisible_ShippingDetail.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_ShippingDetail.AgSelectedValue = Nothing
        Me.TxtIsVisible_ShippingDetail.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_ShippingDetail.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_ShippingDetail.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_ShippingDetail.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_ShippingDetail.Location = New System.Drawing.Point(232, 220)
        Me.TxtIsVisible_ShippingDetail.MaxLength = 3
        Me.TxtIsVisible_ShippingDetail.Name = "TxtIsVisible_ShippingDetail"
        Me.TxtIsVisible_ShippingDetail.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_ShippingDetail.TabIndex = 10
        Me.TxtIsVisible_ShippingDetail.Text = "TxtIsVisible_ShippingDetail"
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.Location = New System.Drawing.Point(37, 220)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(152, 16)
        Me.Label64.TabIndex = 863
        Me.Label64.Text = "Is Visible Shipping Detail"
        '
        'TxtIsVisible_DeliveryDetail
        '
        Me.TxtIsVisible_DeliveryDetail.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_DeliveryDetail.AgLastValueTag = Nothing
        Me.TxtIsVisible_DeliveryDetail.AgLastValueText = Nothing
        Me.TxtIsVisible_DeliveryDetail.AgMandatory = False
        Me.TxtIsVisible_DeliveryDetail.AgMasterHelp = False
        Me.TxtIsVisible_DeliveryDetail.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_DeliveryDetail.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_DeliveryDetail.AgNumberRightPlaces = 0
        Me.TxtIsVisible_DeliveryDetail.AgPickFromLastValue = False
        Me.TxtIsVisible_DeliveryDetail.AgRowFilter = ""
        Me.TxtIsVisible_DeliveryDetail.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_DeliveryDetail.AgSelectedValue = Nothing
        Me.TxtIsVisible_DeliveryDetail.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_DeliveryDetail.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_DeliveryDetail.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_DeliveryDetail.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_DeliveryDetail.Location = New System.Drawing.Point(232, 201)
        Me.TxtIsVisible_DeliveryDetail.MaxLength = 3
        Me.TxtIsVisible_DeliveryDetail.Name = "TxtIsVisible_DeliveryDetail"
        Me.TxtIsVisible_DeliveryDetail.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_DeliveryDetail.TabIndex = 9
        Me.TxtIsVisible_DeliveryDetail.Text = "TxtIsVisible_DeliveryDetail"
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.Location = New System.Drawing.Point(37, 201)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(147, 16)
        Me.Label63.TabIndex = 861
        Me.Label63.Text = "Is Visible Delivery Detail"
        '
        'TxtIsVisible_Deal
        '
        Me.TxtIsVisible_Deal.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_Deal.AgLastValueTag = Nothing
        Me.TxtIsVisible_Deal.AgLastValueText = Nothing
        Me.TxtIsVisible_Deal.AgMandatory = False
        Me.TxtIsVisible_Deal.AgMasterHelp = False
        Me.TxtIsVisible_Deal.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_Deal.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_Deal.AgNumberRightPlaces = 0
        Me.TxtIsVisible_Deal.AgPickFromLastValue = False
        Me.TxtIsVisible_Deal.AgRowFilter = ""
        Me.TxtIsVisible_Deal.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_Deal.AgSelectedValue = Nothing
        Me.TxtIsVisible_Deal.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_Deal.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_Deal.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_Deal.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_Deal.Location = New System.Drawing.Point(232, 182)
        Me.TxtIsVisible_Deal.MaxLength = 3
        Me.TxtIsVisible_Deal.Name = "TxtIsVisible_Deal"
        Me.TxtIsVisible_Deal.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_Deal.TabIndex = 8
        Me.TxtIsVisible_Deal.Text = "TxtIsVisible_Deal"
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label62.Location = New System.Drawing.Point(37, 182)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(91, 16)
        Me.Label62.TabIndex = 859
        Me.Label62.Text = "Is Visible Deal"
        '
        'TxtIsVisible_MRP
        '
        Me.TxtIsVisible_MRP.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_MRP.AgLastValueTag = Nothing
        Me.TxtIsVisible_MRP.AgLastValueText = Nothing
        Me.TxtIsVisible_MRP.AgMandatory = False
        Me.TxtIsVisible_MRP.AgMasterHelp = False
        Me.TxtIsVisible_MRP.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_MRP.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_MRP.AgNumberRightPlaces = 0
        Me.TxtIsVisible_MRP.AgPickFromLastValue = False
        Me.TxtIsVisible_MRP.AgRowFilter = ""
        Me.TxtIsVisible_MRP.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_MRP.AgSelectedValue = Nothing
        Me.TxtIsVisible_MRP.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_MRP.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_MRP.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_MRP.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_MRP.Location = New System.Drawing.Point(232, 163)
        Me.TxtIsVisible_MRP.MaxLength = 3
        Me.TxtIsVisible_MRP.Name = "TxtIsVisible_MRP"
        Me.TxtIsVisible_MRP.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_MRP.TabIndex = 7
        Me.TxtIsVisible_MRP.Text = "TxtIsVisible_MRP"
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label61.Location = New System.Drawing.Point(37, 163)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(94, 16)
        Me.Label61.TabIndex = 857
        Me.Label61.Text = "Is Visible MRP"
        '
        'TxtIsVisible_FreeQty
        '
        Me.TxtIsVisible_FreeQty.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_FreeQty.AgLastValueTag = Nothing
        Me.TxtIsVisible_FreeQty.AgLastValueText = Nothing
        Me.TxtIsVisible_FreeQty.AgMandatory = False
        Me.TxtIsVisible_FreeQty.AgMasterHelp = False
        Me.TxtIsVisible_FreeQty.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_FreeQty.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_FreeQty.AgNumberRightPlaces = 0
        Me.TxtIsVisible_FreeQty.AgPickFromLastValue = False
        Me.TxtIsVisible_FreeQty.AgRowFilter = ""
        Me.TxtIsVisible_FreeQty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_FreeQty.AgSelectedValue = Nothing
        Me.TxtIsVisible_FreeQty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_FreeQty.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_FreeQty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_FreeQty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_FreeQty.Location = New System.Drawing.Point(232, 125)
        Me.TxtIsVisible_FreeQty.MaxLength = 3
        Me.TxtIsVisible_FreeQty.Name = "TxtIsVisible_FreeQty"
        Me.TxtIsVisible_FreeQty.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_FreeQty.TabIndex = 5
        Me.TxtIsVisible_FreeQty.Text = "TxtIsVisible_FreeQty"
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label52.Location = New System.Drawing.Point(37, 125)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(154, 16)
        Me.Label52.TabIndex = 855
        Me.Label52.Text = "Is Visible Free/Waist Qty"
        '
        'TxtIsVisible_PurchIndent
        '
        Me.TxtIsVisible_PurchIndent.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_PurchIndent.AgLastValueTag = Nothing
        Me.TxtIsVisible_PurchIndent.AgLastValueText = Nothing
        Me.TxtIsVisible_PurchIndent.AgMandatory = False
        Me.TxtIsVisible_PurchIndent.AgMasterHelp = False
        Me.TxtIsVisible_PurchIndent.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_PurchIndent.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_PurchIndent.AgNumberRightPlaces = 0
        Me.TxtIsVisible_PurchIndent.AgPickFromLastValue = False
        Me.TxtIsVisible_PurchIndent.AgRowFilter = ""
        Me.TxtIsVisible_PurchIndent.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_PurchIndent.AgSelectedValue = Nothing
        Me.TxtIsVisible_PurchIndent.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_PurchIndent.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_PurchIndent.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_PurchIndent.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_PurchIndent.Location = New System.Drawing.Point(232, 106)
        Me.TxtIsVisible_PurchIndent.MaxLength = 3
        Me.TxtIsVisible_PurchIndent.Name = "TxtIsVisible_PurchIndent"
        Me.TxtIsVisible_PurchIndent.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_PurchIndent.TabIndex = 4
        Me.TxtIsVisible_PurchIndent.Text = "TxtIsVisible_PurchIndent"
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.Location = New System.Drawing.Point(37, 106)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(138, 16)
        Me.Label51.TabIndex = 853
        Me.Label51.Text = "Is Visible Purch Indent"
        '
        'TxtIsVisible_PurchQuotation
        '
        Me.TxtIsVisible_PurchQuotation.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_PurchQuotation.AgLastValueTag = Nothing
        Me.TxtIsVisible_PurchQuotation.AgLastValueText = Nothing
        Me.TxtIsVisible_PurchQuotation.AgMandatory = False
        Me.TxtIsVisible_PurchQuotation.AgMasterHelp = False
        Me.TxtIsVisible_PurchQuotation.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_PurchQuotation.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_PurchQuotation.AgNumberRightPlaces = 0
        Me.TxtIsVisible_PurchQuotation.AgPickFromLastValue = False
        Me.TxtIsVisible_PurchQuotation.AgRowFilter = ""
        Me.TxtIsVisible_PurchQuotation.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_PurchQuotation.AgSelectedValue = Nothing
        Me.TxtIsVisible_PurchQuotation.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_PurchQuotation.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_PurchQuotation.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_PurchQuotation.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_PurchQuotation.Location = New System.Drawing.Point(232, 87)
        Me.TxtIsVisible_PurchQuotation.MaxLength = 3
        Me.TxtIsVisible_PurchQuotation.Name = "TxtIsVisible_PurchQuotation"
        Me.TxtIsVisible_PurchQuotation.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_PurchQuotation.TabIndex = 3
        Me.TxtIsVisible_PurchQuotation.Text = "TxtIsVisible_PurchQuotation"
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.Location = New System.Drawing.Point(37, 87)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(159, 16)
        Me.Label50.TabIndex = 851
        Me.Label50.Text = "Is Visible Purch Quotation"
        '
        'TxtShowRecordCount
        '
        Me.TxtShowRecordCount.AgAllowUserToEnableMasterHelp = False
        Me.TxtShowRecordCount.AgLastValueTag = Nothing
        Me.TxtShowRecordCount.AgLastValueText = Nothing
        Me.TxtShowRecordCount.AgMandatory = False
        Me.TxtShowRecordCount.AgMasterHelp = False
        Me.TxtShowRecordCount.AgNumberLeftPlaces = 2
        Me.TxtShowRecordCount.AgNumberNegetiveAllow = False
        Me.TxtShowRecordCount.AgNumberRightPlaces = 0
        Me.TxtShowRecordCount.AgPickFromLastValue = False
        Me.TxtShowRecordCount.AgRowFilter = ""
        Me.TxtShowRecordCount.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtShowRecordCount.AgSelectedValue = Nothing
        Me.TxtShowRecordCount.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtShowRecordCount.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtShowRecordCount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtShowRecordCount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtShowRecordCount.Location = New System.Drawing.Point(232, 68)
        Me.TxtShowRecordCount.MaxLength = 3
        Me.TxtShowRecordCount.Name = "TxtShowRecordCount"
        Me.TxtShowRecordCount.Size = New System.Drawing.Size(60, 18)
        Me.TxtShowRecordCount.TabIndex = 1
        Me.TxtShowRecordCount.Text = "TxtShowRecordCount"
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.Location = New System.Drawing.Point(37, 68)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(123, 16)
        Me.Label49.TabIndex = 849
        Me.Label49.Text = "Show Record Count"
        '
        'TxtShowLastPoRates
        '
        Me.TxtShowLastPoRates.AgAllowUserToEnableMasterHelp = False
        Me.TxtShowLastPoRates.AgLastValueTag = Nothing
        Me.TxtShowLastPoRates.AgLastValueText = "TxtShowLastPoRates"
        Me.TxtShowLastPoRates.AgMandatory = False
        Me.TxtShowLastPoRates.AgMasterHelp = False
        Me.TxtShowLastPoRates.AgNumberLeftPlaces = 0
        Me.TxtShowLastPoRates.AgNumberNegetiveAllow = False
        Me.TxtShowLastPoRates.AgNumberRightPlaces = 0
        Me.TxtShowLastPoRates.AgPickFromLastValue = False
        Me.TxtShowLastPoRates.AgRowFilter = ""
        Me.TxtShowLastPoRates.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtShowLastPoRates.AgSelectedValue = Nothing
        Me.TxtShowLastPoRates.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtShowLastPoRates.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtShowLastPoRates.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtShowLastPoRates.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtShowLastPoRates.Location = New System.Drawing.Point(232, 49)
        Me.TxtShowLastPoRates.MaxLength = 3
        Me.TxtShowLastPoRates.Name = "TxtShowLastPoRates"
        Me.TxtShowLastPoRates.Size = New System.Drawing.Size(60, 18)
        Me.TxtShowLastPoRates.TabIndex = 0
        Me.TxtShowLastPoRates.Text = "TxtShowLastPoRates"
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.Location = New System.Drawing.Point(37, 49)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(127, 16)
        Me.Label48.TabIndex = 847
        Me.Label48.Text = "Show Last Po Rates"
        '
        'LblEntryTypeReq
        '
        Me.LblEntryTypeReq.AutoSize = True
        Me.LblEntryTypeReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblEntryTypeReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblEntryTypeReq.Location = New System.Drawing.Point(176, 55)
        Me.LblEntryTypeReq.Name = "LblEntryTypeReq"
        Me.LblEntryTypeReq.Size = New System.Drawing.Size(10, 7)
        Me.LblEntryTypeReq.TabIndex = 788
        Me.LblEntryTypeReq.Text = "Ä"
        '
        'TxtVoucherType
        '
        Me.TxtVoucherType.AgAllowUserToEnableMasterHelp = False
        Me.TxtVoucherType.AgLastValueTag = Nothing
        Me.TxtVoucherType.AgLastValueText = Nothing
        Me.TxtVoucherType.AgMandatory = True
        Me.TxtVoucherType.AgMasterHelp = False
        Me.TxtVoucherType.AgNumberLeftPlaces = 0
        Me.TxtVoucherType.AgNumberNegetiveAllow = False
        Me.TxtVoucherType.AgNumberRightPlaces = 0
        Me.TxtVoucherType.AgPickFromLastValue = False
        Me.TxtVoucherType.AgRowFilter = ""
        Me.TxtVoucherType.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVoucherType.AgSelectedValue = Nothing
        Me.TxtVoucherType.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVoucherType.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVoucherType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVoucherType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVoucherType.Location = New System.Drawing.Point(192, 52)
        Me.TxtVoucherType.MaxLength = 0
        Me.TxtVoucherType.Name = "TxtVoucherType"
        Me.TxtVoucherType.Size = New System.Drawing.Size(241, 18)
        Me.TxtVoucherType.TabIndex = 1
        Me.TxtVoucherType.Text = "TxtVoucherType"
        '
        'LblEntryType
        '
        Me.LblEntryType.AutoSize = True
        Me.LblEntryType.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEntryType.Location = New System.Drawing.Point(78, 54)
        Me.LblEntryType.Name = "LblEntryType"
        Me.LblEntryType.Size = New System.Drawing.Size(70, 16)
        Me.LblEntryType.TabIndex = 787
        Me.LblEntryType.Text = "Entry Type"
        '
        'BtnCopyToAllDiv
        '
        Me.BtnCopyToAllDiv.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCopyToAllDiv.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCopyToAllDiv.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnCopyToAllDiv.Location = New System.Drawing.Point(784, 72)
        Me.BtnCopyToAllDiv.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnCopyToAllDiv.Name = "BtnCopyToAllDiv"
        Me.BtnCopyToAllDiv.Size = New System.Drawing.Size(148, 25)
        Me.BtnCopyToAllDiv.TabIndex = 789
        Me.BtnCopyToAllDiv.TabStop = False
        Me.BtnCopyToAllDiv.Text = "Copy To All Division"
        Me.BtnCopyToAllDiv.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnCopyToAllDiv.UseVisualStyleBackColor = True
        '
        'BtnCopyToAllSite
        '
        Me.BtnCopyToAllSite.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCopyToAllSite.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCopyToAllSite.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnCopyToAllSite.Location = New System.Drawing.Point(784, 46)
        Me.BtnCopyToAllSite.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnCopyToAllSite.Name = "BtnCopyToAllSite"
        Me.BtnCopyToAllSite.Size = New System.Drawing.Size(148, 25)
        Me.BtnCopyToAllSite.TabIndex = 790
        Me.BtnCopyToAllSite.TabStop = False
        Me.BtnCopyToAllSite.Text = "Copy To All Site"
        Me.BtnCopyToAllSite.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnCopyToAllSite.UseVisualStyleBackColor = True
        '
        'BtnFillDefaultValue
        '
        Me.BtnFillDefaultValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillDefaultValue.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillDefaultValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFillDefaultValue.Location = New System.Drawing.Point(635, 46)
        Me.BtnFillDefaultValue.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillDefaultValue.Name = "BtnFillDefaultValue"
        Me.BtnFillDefaultValue.Size = New System.Drawing.Size(148, 25)
        Me.BtnFillDefaultValue.TabIndex = 791
        Me.BtnFillDefaultValue.TabStop = False
        Me.BtnFillDefaultValue.Text = "Fill Default Values"
        Me.BtnFillDefaultValue.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillDefaultValue.UseVisualStyleBackColor = True
        '
        'BtnInsertV_Type
        '
        Me.BtnInsertV_Type.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnInsertV_Type.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnInsertV_Type.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnInsertV_Type.Location = New System.Drawing.Point(635, 72)
        Me.BtnInsertV_Type.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnInsertV_Type.Name = "BtnInsertV_Type"
        Me.BtnInsertV_Type.Size = New System.Drawing.Size(148, 25)
        Me.BtnInsertV_Type.TabIndex = 792
        Me.BtnInsertV_Type.TabStop = False
        Me.BtnInsertV_Type.Text = "Insert V_Type"
        Me.BtnInsertV_Type.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnInsertV_Type.UseVisualStyleBackColor = True
        '
        'TP3
        '
        Me.TP3.BackColor = System.Drawing.Color.Gainsboro
        Me.TP3.Controls.Add(Me.TxtTransactionEdit_AllowedDays)
        Me.TP3.Controls.Add(Me.Label82)
        Me.TP3.Controls.Add(Me.TxtTransactionEdit_AllowedDaysAdmin)
        Me.TP3.Controls.Add(Me.Label81)
        Me.TP3.Controls.Add(Me.TxtTransactionDelete_AllowedDaysAdmin)
        Me.TP3.Controls.Add(Me.Label76)
        Me.TP3.Controls.Add(Me.TxtTransactionDelete_AllowedDays)
        Me.TP3.Controls.Add(Me.Label60)
        Me.TP3.Location = New System.Drawing.Point(4, 22)
        Me.TP3.Name = "TP3"
        Me.TP3.Size = New System.Drawing.Size(978, 455)
        Me.TP3.TabIndex = 2
        Me.TP3.Text = "Administration"
        '
        'TxtTransactionDelete_AllowedDays
        '
        Me.TxtTransactionDelete_AllowedDays.AgAllowUserToEnableMasterHelp = False
        Me.TxtTransactionDelete_AllowedDays.AgLastValueTag = Nothing
        Me.TxtTransactionDelete_AllowedDays.AgLastValueText = "0"
        Me.TxtTransactionDelete_AllowedDays.AgMandatory = False
        Me.TxtTransactionDelete_AllowedDays.AgMasterHelp = False
        Me.TxtTransactionDelete_AllowedDays.AgNumberLeftPlaces = 4
        Me.TxtTransactionDelete_AllowedDays.AgNumberNegetiveAllow = False
        Me.TxtTransactionDelete_AllowedDays.AgNumberRightPlaces = 0
        Me.TxtTransactionDelete_AllowedDays.AgPickFromLastValue = False
        Me.TxtTransactionDelete_AllowedDays.AgRowFilter = ""
        Me.TxtTransactionDelete_AllowedDays.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTransactionDelete_AllowedDays.AgSelectedValue = Nothing
        Me.TxtTransactionDelete_AllowedDays.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTransactionDelete_AllowedDays.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtTransactionDelete_AllowedDays.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTransactionDelete_AllowedDays.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTransactionDelete_AllowedDays.Location = New System.Drawing.Point(322, 48)
        Me.TxtTransactionDelete_AllowedDays.MaxLength = 4
        Me.TxtTransactionDelete_AllowedDays.Name = "TxtTransactionDelete_AllowedDays"
        Me.TxtTransactionDelete_AllowedDays.Size = New System.Drawing.Size(60, 18)
        Me.TxtTransactionDelete_AllowedDays.TabIndex = 1
        Me.TxtTransactionDelete_AllowedDays.Text = "TxtTransactionDelete_AllowedDays"
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label60.Location = New System.Drawing.Point(64, 48)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(202, 16)
        Me.Label60.TabIndex = 851
        Me.Label60.Text = "Transaction Delete  Allowed Days"
        '
        'TxtTransactionDelete_AllowedDaysAdmin
        '
        Me.TxtTransactionDelete_AllowedDaysAdmin.AgAllowUserToEnableMasterHelp = False
        Me.TxtTransactionDelete_AllowedDaysAdmin.AgLastValueTag = Nothing
        Me.TxtTransactionDelete_AllowedDaysAdmin.AgLastValueText = Nothing
        Me.TxtTransactionDelete_AllowedDaysAdmin.AgMandatory = False
        Me.TxtTransactionDelete_AllowedDaysAdmin.AgMasterHelp = False
        Me.TxtTransactionDelete_AllowedDaysAdmin.AgNumberLeftPlaces = 4
        Me.TxtTransactionDelete_AllowedDaysAdmin.AgNumberNegetiveAllow = False
        Me.TxtTransactionDelete_AllowedDaysAdmin.AgNumberRightPlaces = 0
        Me.TxtTransactionDelete_AllowedDaysAdmin.AgPickFromLastValue = False
        Me.TxtTransactionDelete_AllowedDaysAdmin.AgRowFilter = ""
        Me.TxtTransactionDelete_AllowedDaysAdmin.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTransactionDelete_AllowedDaysAdmin.AgSelectedValue = Nothing
        Me.TxtTransactionDelete_AllowedDaysAdmin.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTransactionDelete_AllowedDaysAdmin.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtTransactionDelete_AllowedDaysAdmin.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTransactionDelete_AllowedDaysAdmin.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTransactionDelete_AllowedDaysAdmin.Location = New System.Drawing.Point(322, 67)
        Me.TxtTransactionDelete_AllowedDaysAdmin.MaxLength = 4
        Me.TxtTransactionDelete_AllowedDaysAdmin.Name = "TxtTransactionDelete_AllowedDaysAdmin"
        Me.TxtTransactionDelete_AllowedDaysAdmin.Size = New System.Drawing.Size(60, 18)
        Me.TxtTransactionDelete_AllowedDaysAdmin.TabIndex = 2
        Me.TxtTransactionDelete_AllowedDaysAdmin.Text = "TxtTransactionDelete_AllowedDaysAdmin"
        '
        'Label76
        '
        Me.Label76.AutoSize = True
        Me.Label76.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label76.Location = New System.Drawing.Point(64, 67)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(238, 16)
        Me.Label76.TabIndex = 853
        Me.Label76.Text = "Transaction Delete Allowed Days Admin"
        '
        'TxtTransactionEdit_AllowedDaysAdmin
        '
        Me.TxtTransactionEdit_AllowedDaysAdmin.AgAllowUserToEnableMasterHelp = False
        Me.TxtTransactionEdit_AllowedDaysAdmin.AgLastValueTag = Nothing
        Me.TxtTransactionEdit_AllowedDaysAdmin.AgLastValueText = Nothing
        Me.TxtTransactionEdit_AllowedDaysAdmin.AgMandatory = False
        Me.TxtTransactionEdit_AllowedDaysAdmin.AgMasterHelp = False
        Me.TxtTransactionEdit_AllowedDaysAdmin.AgNumberLeftPlaces = 4
        Me.TxtTransactionEdit_AllowedDaysAdmin.AgNumberNegetiveAllow = False
        Me.TxtTransactionEdit_AllowedDaysAdmin.AgNumberRightPlaces = 0
        Me.TxtTransactionEdit_AllowedDaysAdmin.AgPickFromLastValue = False
        Me.TxtTransactionEdit_AllowedDaysAdmin.AgRowFilter = ""
        Me.TxtTransactionEdit_AllowedDaysAdmin.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTransactionEdit_AllowedDaysAdmin.AgSelectedValue = Nothing
        Me.TxtTransactionEdit_AllowedDaysAdmin.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTransactionEdit_AllowedDaysAdmin.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtTransactionEdit_AllowedDaysAdmin.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTransactionEdit_AllowedDaysAdmin.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTransactionEdit_AllowedDaysAdmin.Location = New System.Drawing.Point(322, 105)
        Me.TxtTransactionEdit_AllowedDaysAdmin.MaxLength = 4
        Me.TxtTransactionEdit_AllowedDaysAdmin.Name = "TxtTransactionEdit_AllowedDaysAdmin"
        Me.TxtTransactionEdit_AllowedDaysAdmin.Size = New System.Drawing.Size(60, 18)
        Me.TxtTransactionEdit_AllowedDaysAdmin.TabIndex = 4
        Me.TxtTransactionEdit_AllowedDaysAdmin.Text = "TxtTransactionEdit_AllowedDaysAdmin"
        '
        'Label81
        '
        Me.Label81.AutoSize = True
        Me.Label81.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label81.Location = New System.Drawing.Point(64, 105)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(224, 16)
        Me.Label81.TabIndex = 855
        Me.Label81.Text = "Transaction Edit Allowed Days Admin"
        '
        'TxtTransactionEdit_AllowedDays
        '
        Me.TxtTransactionEdit_AllowedDays.AgAllowUserToEnableMasterHelp = False
        Me.TxtTransactionEdit_AllowedDays.AgLastValueTag = Nothing
        Me.TxtTransactionEdit_AllowedDays.AgLastValueText = Nothing
        Me.TxtTransactionEdit_AllowedDays.AgMandatory = False
        Me.TxtTransactionEdit_AllowedDays.AgMasterHelp = False
        Me.TxtTransactionEdit_AllowedDays.AgNumberLeftPlaces = 4
        Me.TxtTransactionEdit_AllowedDays.AgNumberNegetiveAllow = False
        Me.TxtTransactionEdit_AllowedDays.AgNumberRightPlaces = 0
        Me.TxtTransactionEdit_AllowedDays.AgPickFromLastValue = False
        Me.TxtTransactionEdit_AllowedDays.AgRowFilter = ""
        Me.TxtTransactionEdit_AllowedDays.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTransactionEdit_AllowedDays.AgSelectedValue = Nothing
        Me.TxtTransactionEdit_AllowedDays.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTransactionEdit_AllowedDays.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtTransactionEdit_AllowedDays.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTransactionEdit_AllowedDays.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTransactionEdit_AllowedDays.Location = New System.Drawing.Point(322, 86)
        Me.TxtTransactionEdit_AllowedDays.MaxLength = 4
        Me.TxtTransactionEdit_AllowedDays.Name = "TxtTransactionEdit_AllowedDays"
        Me.TxtTransactionEdit_AllowedDays.Size = New System.Drawing.Size(60, 18)
        Me.TxtTransactionEdit_AllowedDays.TabIndex = 3
        Me.TxtTransactionEdit_AllowedDays.Text = "TxtTransactionEdit_AllowedDays"
        '
        'Label82
        '
        Me.Label82.AutoSize = True
        Me.Label82.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label82.Location = New System.Drawing.Point(64, 86)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(184, 16)
        Me.Label82.TabIndex = 857
        Me.Label82.Text = "Transaction Edit Allowed Days"
        '
        'TxtIsVisible_TransactionHistory
        '
        Me.TxtIsVisible_TransactionHistory.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsVisible_TransactionHistory.AgLastValueTag = Nothing
        Me.TxtIsVisible_TransactionHistory.AgLastValueText = Nothing
        Me.TxtIsVisible_TransactionHistory.AgMandatory = False
        Me.TxtIsVisible_TransactionHistory.AgMasterHelp = False
        Me.TxtIsVisible_TransactionHistory.AgNumberLeftPlaces = 0
        Me.TxtIsVisible_TransactionHistory.AgNumberNegetiveAllow = False
        Me.TxtIsVisible_TransactionHistory.AgNumberRightPlaces = 0
        Me.TxtIsVisible_TransactionHistory.AgPickFromLastValue = False
        Me.TxtIsVisible_TransactionHistory.AgRowFilter = ""
        Me.TxtIsVisible_TransactionHistory.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsVisible_TransactionHistory.AgSelectedValue = Nothing
        Me.TxtIsVisible_TransactionHistory.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsVisible_TransactionHistory.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsVisible_TransactionHistory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIsVisible_TransactionHistory.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsVisible_TransactionHistory.Location = New System.Drawing.Point(479, 349)
        Me.TxtIsVisible_TransactionHistory.MaxLength = 3
        Me.TxtIsVisible_TransactionHistory.Name = "TxtIsVisible_TransactionHistory"
        Me.TxtIsVisible_TransactionHistory.Size = New System.Drawing.Size(60, 18)
        Me.TxtIsVisible_TransactionHistory.TabIndex = 922
        Me.TxtIsVisible_TransactionHistory.Text = "TxtIsVisible_TransactionHistory"
        '
        'Label83
        '
        Me.Label83.AutoSize = True
        Me.Label83.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label83.Location = New System.Drawing.Point(300, 349)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(177, 16)
        Me.Label83.TabIndex = 923
        Me.Label83.Text = "Is Visible Transaction History"
        '
        'TP4
        '
        Me.TP4.BackColor = System.Drawing.Color.Gainsboro
        Me.TP4.Controls.Add(Me.TxtTransactionHistory_SqlQuery)
        Me.TP4.Controls.Add(Me.Label85)
        Me.TP4.Controls.Add(Me.TxtTransactionHistory_ColumnWidthCsv)
        Me.TP4.Controls.Add(Me.Label84)
        Me.TP4.Location = New System.Drawing.Point(4, 22)
        Me.TP4.Name = "TP4"
        Me.TP4.Size = New System.Drawing.Size(978, 455)
        Me.TP4.TabIndex = 3
        Me.TP4.Text = "SQL Add-On"
        '
        'TxtTransactionHistory_ColumnWidthCsv
        '
        Me.TxtTransactionHistory_ColumnWidthCsv.AgAllowUserToEnableMasterHelp = False
        Me.TxtTransactionHistory_ColumnWidthCsv.AgLastValueTag = Nothing
        Me.TxtTransactionHistory_ColumnWidthCsv.AgLastValueText = "TxtTransactionHistory_ColumnWidthCsv"
        Me.TxtTransactionHistory_ColumnWidthCsv.AgMandatory = False
        Me.TxtTransactionHistory_ColumnWidthCsv.AgMasterHelp = False
        Me.TxtTransactionHistory_ColumnWidthCsv.AgNumberLeftPlaces = 0
        Me.TxtTransactionHistory_ColumnWidthCsv.AgNumberNegetiveAllow = False
        Me.TxtTransactionHistory_ColumnWidthCsv.AgNumberRightPlaces = 0
        Me.TxtTransactionHistory_ColumnWidthCsv.AgPickFromLastValue = False
        Me.TxtTransactionHistory_ColumnWidthCsv.AgRowFilter = ""
        Me.TxtTransactionHistory_ColumnWidthCsv.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTransactionHistory_ColumnWidthCsv.AgSelectedValue = Nothing
        Me.TxtTransactionHistory_ColumnWidthCsv.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTransactionHistory_ColumnWidthCsv.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtTransactionHistory_ColumnWidthCsv.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTransactionHistory_ColumnWidthCsv.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTransactionHistory_ColumnWidthCsv.Location = New System.Drawing.Point(284, 18)
        Me.TxtTransactionHistory_ColumnWidthCsv.MaxLength = 0
        Me.TxtTransactionHistory_ColumnWidthCsv.Name = "TxtTransactionHistory_ColumnWidthCsv"
        Me.TxtTransactionHistory_ColumnWidthCsv.Size = New System.Drawing.Size(623, 18)
        Me.TxtTransactionHistory_ColumnWidthCsv.TabIndex = 1
        Me.TxtTransactionHistory_ColumnWidthCsv.Text = "TxtTransactionHistory_ColumnWidthCsv"
        '
        'Label84
        '
        Me.Label84.AutoSize = True
        Me.Label84.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label84.Location = New System.Drawing.Point(40, 18)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(231, 16)
        Me.Label84.TabIndex = 923
        Me.Label84.Text = "Transaction History Column Width Csv"
        '
        'TxtTransactionHistory_SqlQuery
        '
        Me.TxtTransactionHistory_SqlQuery.AgAllowUserToEnableMasterHelp = False
        Me.TxtTransactionHistory_SqlQuery.AgLastValueTag = Nothing
        Me.TxtTransactionHistory_SqlQuery.AgLastValueText = Nothing
        Me.TxtTransactionHistory_SqlQuery.AgMandatory = False
        Me.TxtTransactionHistory_SqlQuery.AgMasterHelp = False
        Me.TxtTransactionHistory_SqlQuery.AgNumberLeftPlaces = 0
        Me.TxtTransactionHistory_SqlQuery.AgNumberNegetiveAllow = False
        Me.TxtTransactionHistory_SqlQuery.AgNumberRightPlaces = 0
        Me.TxtTransactionHistory_SqlQuery.AgPickFromLastValue = False
        Me.TxtTransactionHistory_SqlQuery.AgRowFilter = ""
        Me.TxtTransactionHistory_SqlQuery.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTransactionHistory_SqlQuery.AgSelectedValue = Nothing
        Me.TxtTransactionHistory_SqlQuery.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTransactionHistory_SqlQuery.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtTransactionHistory_SqlQuery.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTransactionHistory_SqlQuery.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTransactionHistory_SqlQuery.Location = New System.Drawing.Point(284, 38)
        Me.TxtTransactionHistory_SqlQuery.MaxLength = 0
        Me.TxtTransactionHistory_SqlQuery.Multiline = True
        Me.TxtTransactionHistory_SqlQuery.Name = "TxtTransactionHistory_SqlQuery"
        Me.TxtTransactionHistory_SqlQuery.Size = New System.Drawing.Size(623, 142)
        Me.TxtTransactionHistory_SqlQuery.TabIndex = 2
        Me.TxtTransactionHistory_SqlQuery.Text = "TxtTransactionHistory_SqlQuery"
        '
        'Label85
        '
        Me.Label85.AutoSize = True
        Me.Label85.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label85.Location = New System.Drawing.Point(40, 38)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(182, 16)
        Me.Label85.TabIndex = 925
        Me.Label85.Text = "Transaction History Sql Query"
        '
        'FrmVoucherTypeSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(986, 632)
        Me.Controls.Add(Me.BtnInsertV_Type)
        Me.Controls.Add(Me.BtnFillDefaultValue)
        Me.Controls.Add(Me.BtnCopyToAllSite)
        Me.Controls.Add(Me.BtnCopyToAllDiv)
        Me.Controls.Add(Me.TC1)
        Me.Controls.Add(Me.TxtSiteCode)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.LblEntryType)
        Me.Controls.Add(Me.TxtVoucherType)
        Me.Controls.Add(Me.LblEntryTypeReq)
        Me.Name = "FrmVoucherTypeSettings"
        Me.Text = "Voucher Type Settings"
        Me.Controls.SetChildIndex(Me.LblEntryTypeReq, 0)
        Me.Controls.SetChildIndex(Me.TxtVoucherType, 0)
        Me.Controls.SetChildIndex(Me.LblEntryType, 0)
        Me.Controls.SetChildIndex(Me.Label26, 0)
        Me.Controls.SetChildIndex(Me.Label27, 0)
        Me.Controls.SetChildIndex(Me.TxtSiteCode, 0)
        Me.Controls.SetChildIndex(Me.TC1, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.BtnCopyToAllDiv, 0)
        Me.Controls.SetChildIndex(Me.BtnCopyToAllSite, 0)
        Me.Controls.SetChildIndex(Me.BtnFillDefaultValue, 0)
        Me.Controls.SetChildIndex(Me.BtnInsertV_Type, 0)
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
        Me.TC1.ResumeLayout(False)
        Me.TP1.ResumeLayout(False)
        Me.TP1.PerformLayout()
        Me.TP2.ResumeLayout(False)
        Me.TP2.PerformLayout()
        Me.TP3.ResumeLayout(False)
        Me.TP3.PerformLayout()
        Me.TP4.ResumeLayout(False)
        Me.TP4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private Sub Txt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtVoucherType.KeyDown, TxtSiteCode.KeyDown, TxtFilterExclude_AcGroup.KeyDown, TxtFilterExclude_Item.KeyDown, TxtFilterExclude_ItemGroup.KeyDown, TxtFilterExclude_SubGroupDivision.KeyDown, TxtFilterExclude_SubGroupSite.KeyDown, TxtFilterInclude_AcGroup.KeyDown, TxtFilterInclude_ContraV_Type.KeyDown, TxtFilterInclude_Item.KeyDown, TxtFilterInclude_ItemGroup.KeyDown, TxtFilterInclude_ItemSite.KeyDown, TxtFilterInclude_ItemType.KeyDown, TxtFilterInclude_Process.KeyDown, TxtFilterInclude_SubGroup.KeyDown, TxtFilterInclude_SubgroupDivision.KeyDown, TxtFilterInclude_SubgroupSite.KeyDown, TxtDEFAULT_Godown.KeyDown, TxtDefault_SubCode.KeyDown, TxtFilterInclude_ItemDivision.KeyDown, TxtFilterInclude_SubGroup.KeyDown, TxtFilterExclude_Subgroup.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then Exit Sub
            If e.KeyCode = Keys.Delete Then sender.Tag = "" : sender.Text = "" : Exit Sub

            Select Case sender.Name
                Case TxtVoucherType.Name
                    If TxtVoucherType.AgHelpDataSet Is Nothing Then
                        mQry = " SELECT V_Type AS Code,Description AS [Entry Type], V_Type AS [Voucher Type] " &
                                " FROM Voucher_Type   " &
                                " Where IfNull(Description,'') <> '' " &
                                " Order By Description "
                        TxtVoucherType.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case TxtDEFAULT_Godown.Name
                    If TxtDEFAULT_Godown.AgHelpDataSet Is Nothing Then
                        mQry = " Select Code, Description FROM Godown " &
                                " Order By Description "
                        TxtDEFAULT_Godown.AgHelpDataSet(, TC1.Top, TC1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case TxtDefault_SubCode.Name
                    If TxtDefault_SubCode.AgHelpDataSet Is Nothing Then
                        mQry = "Select  SubCode, DispName FROM SubGroup  Order By DispName "
                        TxtDefault_SubCode.AgHelpDataSet(, TC1.Top, TC1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case TxtSiteCode.Name
                    If TxtSiteCode.AgHelpDataSet Is Nothing Then
                        mQry = " SELECT H.Code, H.Name FROM SiteMast H " &
                                " Order By H.Name "
                        TxtSiteCode.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case TxtFilterExclude_AcGroup.Name
                    If TxtFilterExclude_AcGroup.AgHelpDataSet Is Nothing Then
                        FHPGD_TxtFilter(TxtFilterExclude_AcGroup, "SELECT 'o' AS Tick, GroupCode, GroupName FROM AcGroup Order By GroupName")
                    End If

                Case TxtFilterInclude_AcGroup.Name
                    If TxtFilterInclude_AcGroup.AgHelpDataSet Is Nothing Then
                        FHPGD_TxtFilter(TxtFilterInclude_AcGroup, "SELECT 'o' AS Tick, GroupCode, GroupName FROM AcGroup Order By GroupName")
                    End If

                Case TxtFilterInclude_Process.Name
                    If TxtFilterInclude_Process.AgHelpDataSet Is Nothing Then
                        FHPGD_TxtFilter(TxtFilterInclude_Process, "SELECT 'o' AS Tick, NCat, Description FROM Process Order By Description")
                    End If

                Case TxtFilterInclude_ItemType.Name
                    If TxtFilterInclude_ItemType.AgHelpDataSet Is Nothing Then
                        FHPGD_TxtFilter(TxtFilterInclude_ItemType, "SELECT 'o' AS Tick,  Code, Name FROM ItemType Order By Name")
                    End If

                Case TxtFilterInclude_ItemGroup.Name
                    If TxtFilterInclude_ItemGroup.AgHelpDataSet Is Nothing Then
                        FHPGD_TxtFilter(TxtFilterInclude_ItemGroup, "SELECT 'o' AS Tick, Code, Description  FROM ItemGroup Order By Description")
                    End If

                Case TxtFilterExclude_ItemGroup.Name
                    If TxtFilterExclude_ItemGroup.AgHelpDataSet Is Nothing Then
                        FHPGD_TxtFilter(TxtFilterExclude_ItemGroup, "SELECT 'o' AS Tick, Code, Description  FROM ItemGroup Order By Description")
                    End If

                Case TxtFilterInclude_ItemDivision.Name
                    If TxtFilterInclude_ItemDivision.AgHelpDataSet Is Nothing Then
                        FHPGD_TxtFilter(TxtFilterInclude_ItemDivision, "SELECT 'o' AS Tick, Div_Code, Div_Name FROM Division Order By Div_Name")
                    End If

                Case TxtFilterInclude_ItemSite.Name
                    If TxtFilterInclude_ItemSite.AgHelpDataSet Is Nothing Then
                        FHPGD_TxtFilter(TxtFilterInclude_ItemSite, "SELECT 'o' AS Tick,  Code, Name FROM SiteMast Order By Name")
                    End If

                Case TxtFilterInclude_SubgroupDivision.Name
                    If TxtFilterInclude_SubgroupDivision.AgHelpDataSet Is Nothing Then
                        FHPGD_TxtFilter(TxtFilterInclude_SubgroupDivision, "SELECT 'o' AS Tick, Div_Code, Div_Name FROM Division Order By Div_Name")
                    End If

                Case TxtFilterInclude_SubgroupSite.Name
                    If TxtFilterInclude_SubgroupSite.AgHelpDataSet Is Nothing Then
                        FHPGD_TxtFilter(TxtFilterInclude_SubgroupSite, "SELECT 'o' AS Tick,  Code, Name FROM SiteMast Order By Name")
                    End If

                Case TxtFilterInclude_Item.Name
                    If TxtFilterInclude_Item.AgHelpDataSet Is Nothing Then
                        FHPGD_TxtFilter(TxtFilterInclude_Item, "SELECT 'o' AS Tick, Code, Description  FROM Item ORDER BY Description")
                    End If

                Case TxtFilterExclude_Item.Name
                    If TxtFilterExclude_Item.AgHelpDataSet Is Nothing Then
                        FHPGD_TxtFilter(TxtFilterExclude_Item, "SELECT 'o' AS Tick, Code, Description  FROM Item ORDER BY Description")
                    End If

                Case TxtFilterExclude_SubGroupDivision.Name
                    If TxtFilterExclude_SubGroupDivision.AgHelpDataSet Is Nothing Then
                        FHPGD_TxtFilter(TxtFilterExclude_SubGroupDivision, "SELECT 'o' AS Tick, Div_Code, Div_Name FROM Division Order By Div_Name")
                    End If

                Case TxtFilterExclude_SubGroupSite.Name
                    If TxtFilterExclude_SubGroupSite.AgHelpDataSet Is Nothing Then
                        FHPGD_TxtFilter(TxtFilterExclude_SubGroupSite, "SELECT 'o' AS Tick,  Code, Name FROM SiteMast Order By Name")
                    End If

                Case TxtFilterInclude_ContraV_Type.Name
                    If TxtFilterInclude_ContraV_Type.AgHelpDataSet Is Nothing Then
                        FHPGD_TxtFilter(TxtFilterInclude_ContraV_Type, "SELECT 'o' AS Tick,  V_Type , Description  FROM Voucher_Type     Order By Description")
                    End If

                Case TxtFilterInclude_SubGroup.Name
                    If TxtFilterInclude_SubGroup.AgHelpDataSet Is Nothing Then
                        FHPGD_TxtFilter(TxtFilterInclude_SubGroup, "SELECT 'o' AS Tick,  SubCode, DispName FROM SubGroup Order By DispName")
                    End If

                Case TxtFilterExclude_Subgroup.Name
                    If TxtFilterExclude_Subgroup.AgHelpDataSet Is Nothing Then
                        FHPGD_TxtFilter(TxtFilterExclude_Subgroup, "SELECT 'o' AS Tick,  SubCode, DispName FROM SubGroup Order By DispName")
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FHPGD_TxtFilter(ByVal Text As TextBox, ByVal mQry As String)
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 400, 370, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Name", 250, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            Text.Tag = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
            Text.Text = FRH_Multiple.FFetchData(2, "", "", ",", True)
        End If

        FRH_Multiple = Nothing
    End Sub

    Private Sub BtnCopyToAllSite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCopyToAllSite.Click
        If Topctrl1.Mode <> "Browse" Then Exit Sub
        If MsgBox("Are You Sure To Copy this for All Sites?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, AgLibrary.ClsMain.PubMsgTitleInfo) = vbYes Then
            ProcCopyToAllSite()
            MsgBox("Process is completed !")
        End If
    End Sub

    Private Sub ProcCopyToAllSite()
        Dim DsTemp As DataSet
        Dim mTrans As String = ""
        Dim I As Integer
        mQry = "SELECT Code FROM SiteMast WHERE Code <> '" & AgL.PubSiteCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        AgL.ECmd = AgL.GCn.CreateCommand
        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
        AgL.ECmd.Transaction = AgL.ETrans
        mTrans = "Begin"

        Try
            With DsTemp.Tables(0)
                If .Rows.Count > 0 Then
                    For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                        mQry = " INSERT INTO Voucher_Type_Settings_Log " &
                                " SELECT * FROM Voucher_Type_Settings WHERE V_Type = '" & TxtVoucherType.Tag & "' AND Site_Code = '" & AgL.XNull(.Rows(I)("Code")) & "' AND Div_Code = '" & AgL.PubDivCode & "' "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = "Select count(*) From Voucher_Type_Settings  Where V_Type='" & TxtVoucherType.AgSelectedValue & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code = '" & AgL.XNull(.Rows(I)("Code")) & "' "
                        If AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar <= 0 Then
                            mQry = " INSERT INTO Voucher_Type_Settings (Code, V_Type , EntryBy , EntryDate, Site_Code, Div_Code ) " &
                                    " Values (" & AgL.Chk_Text(GetCode(AgL.XNull(.Rows(I)("Code")), AgL.PubDivCode)) & ", " & AgL.Chk_Text(TxtVoucherType.Tag) & ", " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", '" & AgL.XNull(.Rows(I)("Code")) & "', " & AgL.Chk_Text(AgL.PubDivCode) & " ) "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                        End If

                        mQry = "  Update Voucher_Type_Settings  " &
                                " Set  " & RetTableColStr("Voucher_Type_Settings") &
                                " FROM " &
                                " ( " &
                                " SELECT *    " &
                                " From Voucher_Type_Settings   " &
                                " Where V_TYpe = " & AgL.Chk_Text(TxtVoucherType.Tag) & " AND Div_Code = " & AgL.Chk_Text(AgL.PubDivCode) & " AND Site_Code =" & AgL.Chk_Text(AgL.PubSiteCode) & "  " &
                                " ) V1 WHERE V1.V_TYpe = Voucher_Type_Settings.V_Type  " &
                                " AND V1.Div_Code = Voucher_Type_Settings.Div_Code  " &
                                " AND voucher_type_settings.Site_Code =  '" & AgL.XNull(.Rows(I)("Code")) & "' "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    Next
                End If
            End With

            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            If mTrans = "Begin" Then
                AgL.ETrans.Rollback()
            ElseIf mTrans = "Commit" Then
                Topctrl1.FButtonClick(14, True)
            End If
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProcCopyToAllDivision()
        Dim DsTemp As DataSet
        Dim mTrans As String = ""
        Dim I As Integer
        mQry = "SELECT Div_Code AS Code FROM Division WHERE Div_Code <> '" & AgL.PubDivCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        AgL.ECmd = AgL.GCn.CreateCommand
        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
        AgL.ECmd.Transaction = AgL.ETrans
        mTrans = "Begin"

        Try
            With DsTemp.Tables(0)
                If .Rows.Count > 0 Then
                    For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                        mQry = " INSERT INTO Voucher_Type_Settings_Log " &
                                " SELECT * FROM Voucher_Type_Settings WHERE V_Type = '" & TxtVoucherType.Tag & "' AND Div_Code = '" & AgL.XNull(.Rows(I)("Code")) & "' AND Site_Code = '" & AgL.PubSiteCode & "' "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = "Select count(*) From Voucher_Type_Settings  Where V_Type='" & TxtVoucherType.AgSelectedValue & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code = '" & AgL.XNull(.Rows(I)("Code")) & "' "
                        If AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar <= 0 Then
                            mQry = " INSERT INTO Voucher_Type_Settings (Code, V_Type , EntryBy , EntryDate, ApproveBy ,ApproveDate, Div_Code, Site_Code ) " &
                                    " Values (" & AgL.Chk_Text(GetCode(AgL.PubSiteCode, AgL.XNull(.Rows(I)("Code")))) & ", " & AgL.Chk_Text(TxtVoucherType.Tag) & ", " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", '" & AgL.XNull(.Rows(I)("Code")) & "', " & AgL.Chk_Text(AgL.PubSiteCode) & " ) "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                        End If

                        mQry = "  Update Voucher_Type_Settings  " &
                                " Set  " & RetTableColStr("Voucher_Type_Settings") &
                                " FROM " &
                                " ( " &
                                " SELECT *    " &
                                " From Voucher_Type_Settings   " &
                                " Where V_TYpe = " & AgL.Chk_Text(TxtVoucherType.Tag) & " AND Site_Code = " & AgL.Chk_Text(AgL.PubSiteCode) & " AND Div_Code = " & AgL.Chk_Text(AgL.PubDivCode) & "  " &
                                " ) V1 WHERE V1.V_TYpe = Voucher_Type_Settings.V_Type  " &
                                " AND V1.Site_Code = Voucher_Type_Settings.Site_Code  " &
                                " AND voucher_type_settings.Div_Code =  '" & AgL.XNull(.Rows(I)("Code")) & "' "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    Next
                End If
            End With

            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            If mTrans = "Begin" Then
                AgL.ETrans.Rollback()
            ElseIf mTrans = "Commit" Then
                Topctrl1.FButtonClick(14, True)
            End If
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function GetCode(ByVal bSiteCode As String, ByVal bDivCode As String) As String
        GetCode = AgL.GetMaxId("Voucher_Type_Settings", "Code", AgL.GCn, bDivCode, bSiteCode, 4, True, True, , AgL.Gcn_ConnectionString)
    End Function

    Function RetTableColStr(ByVal TableName As String) As String
        Dim mQry$
        mQry = "DECLARE @ColStr VARCHAR(Max) " &
                "SET @ColStr='' " &
                "SELECT @ColStr=@ColStr + ' " & TableName & ".' + C.COLUMN_NAME + ' = V1.' + C.COLUMN_NAME  + ',' " &
                "FROM INFORMATION_SCHEMA.COLUMNS C  " &
                "WHERE C.TABLE_NAME ='" & TableName & "' " &
                "AND C.COLUMN_NAME NOT IN ('UID', 'EntryBy', 'EntryDate', 'ApproveBy', 'ApproveDate', 'RowID', 'Site_Code','Div_Code','V_Type','Code', " &
                " 'Query', 'Report_Name','Report_Heading', 'Report_Format', 'SubReport_QueryList', 'SubReport_NameList') " &
                "IF LEN(@ColStr)>0 SET @ColStr=substring (@ColStr,1,len(@ColStr)-1) " &
                " SELECT @ColStr "
        RetTableColStr = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
    End Function

    Private Sub BtnCopyToAllDiv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCopyToAllDiv.Click
        If Topctrl1.Mode <> "Browse" Then Exit Sub
        If MsgBox("Are You Sure To Copy this for All Division?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, AgLibrary.ClsMain.PubMsgTitleInfo) = vbYes Then
            ProcCopyToAllDivision()
            MsgBox("Process is completed !")
        End If
    End Sub

    Private Sub BtnFillDefaultValue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFillDefaultValue.Click
        If Topctrl1.Mode = "Browse" Then Exit Sub
        If MsgBox("Are You Sure To Fill Default Values ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, AgLibrary.ClsMain.PubMsgTitleInfo) = vbYes Then
            ProcToFillDetaultValue()
        End If
    End Sub

    Private Sub BtnInsertV_Type_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnInsertV_Type.Click
        FUpdateNullWithDefaultValues()
    End Sub
End Class
