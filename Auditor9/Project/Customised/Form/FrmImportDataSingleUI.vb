Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports Customised.ClsMain

Public Class FrmImportDataSingleUI
    Public WithEvents DglItem As New AgControls.AgDataGrid
    Public WithEvents DglItemRateList As New AgControls.AgDataGrid
    Public WithEvents DglParty As New AgControls.AgDataGrid
    Public WithEvents DglSale1 As New AgControls.AgDataGrid
    Public WithEvents DglSale2 As New AgControls.AgDataGrid
    Public WithEvents DglSale3 As New AgControls.AgDataGrid
    Public WithEvents DglPurch1 As New AgControls.AgDataGrid
    Public WithEvents DglPurch2 As New AgControls.AgDataGrid
    Public WithEvents DglPurch3 As New AgControls.AgDataGrid

    Dim DsExcelData_Item As New DataSet
    Dim DsExcelData_ItemRateList As New DataSet
    Dim DsExcelData_Party As New DataSet

    Dim DsExcelData_SaleInvoice As New DataSet
    Dim DsExcelData_SaleInvoiceDetail As New DataSet
    Dim DsExcelData_SaleInvoiceDimensionDetail As New DataSet

    Dim DsExcelData_PurchaseInvoice As New DataSet
    Dim DsExcelData_PurchaseInvoiceDetail As New DataSet
    Dim DsExcelData_PurchaseInvoiceDimensionDetail As New DataSet

    Dim MyConnection_Item As System.Data.OleDb.OleDbConnection
    Dim MyConnection_ItemRateList As System.Data.OleDb.OleDbConnection

    Dim MyConnection_Party As System.Data.OleDb.OleDbConnection

    Dim MyConnection_SaleInvoice As System.Data.OleDb.OleDbConnection
    Dim MyConnection_SaleInvoiceDetail As System.Data.OleDb.OleDbConnection
    Dim MyConnection_SaleInvoiceDimensionDetail As System.Data.OleDb.OleDbConnection

    Dim MyConnection_PurchaseInvoice As System.Data.OleDb.OleDbConnection
    Dim MyConnection_PurchaseInvoiceDetail As System.Data.OleDb.OleDbConnection
    Dim MyConnection_PurchaseInvoiceDimensionDetail As System.Data.OleDb.OleDbConnection

    Dim DtItemDataFields As DataTable
    Dim DtItemCategory As DataTable
    Dim DtItemGroup As DataTable

    Dim mItemTableStructure As New DataTable

    Dim mRateListTableStructure As New DataTable
    Dim mRateListDetailTableStructure As New DataTable


    Dim mItemTable As New DataTable
    Dim mItemGroupTable As New DataTable
    Dim mItemCategoryTable As New DataTable
    Dim mRateTypeTable As New DataTable


    Dim DtRateListDataFields As DataTable

    Dim DtPartyDataFields As DataTable

    Dim DtSaleInvoice_DataFields As DataTable
    Dim DtSaleInvoiceDetail_DataFields As DataTable
    Dim DtSaleInvoiceDimensionDetail_DataFields As DataTable

    Dim DtPurchInvoice_DataFields As DataTable
    Dim DtPurchInvoiceDetail_DataFields As DataTable
    Dim DtPurchInvoiceDimensionDetail_DataFields As DataTable

    Dim DtRows_RateList As DataRow()
    Dim DtRows_RateListDetail As DataRow()

    Dim DtItemCategoryRow As DataRow()
    Dim DtItemGroupRow As DataRow()
    Dim DtItemRow As DataRow()

    Dim mQry As String = ""

    Private Sub Ini_Grid()
        mQry = "Select '' as Srl, 'Item Code' as [Field Name], 'Text' as [Data Type], 10 as [Length], 'Mandatory, Should be unique.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Item Name' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory, Should be unique.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Item Display Name' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Item Group' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Item Category' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Specification' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Unit' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Purchase Rate' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sale Rate' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sales Tax Group' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, GST 0% / GST 5% / GST 12% / GST 18% / GST 28%' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'HSN Code' as [Field Name], 'Text' as [Data Type], 20 as [Length], '' as Remark "
        DtItemDataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)
        DglItem.DataSource = DtItemDataFields
        AgL.AddAgDataGrid(DglItem, PnlItem)
        FormatFieldGrid(DglItem)


        '''''''''''''''''''''''''''''''''''''''End For Item'''''''''''''''''''''''''''''''''''''''

        '''''''''''''''''''''''''''''''''''''''For Item Rate List'''''''''''''''''''''''''''''''''''''''
        mQry = "Select '' as Srl, 'Item Name' as [Field Name], 'Text' as [Data Type], 10 as [Length], 'Mandatory' as Remark "
        Dim DtRateTypes As DataTable = AgL.FillData("Select Description From RateType ", AgL.GCn).Tables(0)
        For I As Integer = 0 To DtRateTypes.Rows.Count - 1
            mQry = mQry + "Union All Select  '' as Srl,'" & DtRateTypes.Rows(I)("Description") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        Next
        DtRateListDataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)
        DglItemRateList.DataSource = DtRateListDataFields
        AgL.AddAgDataGrid(DglItemRateList, PnlItemRateList)
        FormatFieldGrid(DglItemRateList)



        '''''''''''''''''''''''''''''''''''''''End For Item Rate List'''''''''''''''''''''''''''''''''''''''


        '''''''''''''''''''''''''''''''''''''''For Party'''''''''''''''''''''''''''''''''''''''
        mQry = "Select '' as Srl, 'Party Type' as [Field Name], 'Text' as [Data Type], 10 as [Length], 'Customer / Supplier / Transporter / Sales Agent / Purchase Agent. If Party is a simple ledger account like expenses then this field can be blank.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Code' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, Should be unique.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Display Name' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Name' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory, Should be unique.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Address' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'City' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'State' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Pin No' as [Field Name], 'Text' as [Data Type], 6 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Contact No' as [Field Name], 'Text' as [Data Type], 35 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Mobile' as [Field Name], 'Text' as [Data Type], 10 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'EMail' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Account Group' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory, Sundry Debtors / Sundry Creditors' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Group Nature' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory, L / A' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Nature' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sales Tax Group' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, Registered / Unregistered / Composition' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Credit Days' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Credit Limit' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Contact Person' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'GST No' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'PAN No' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Aadhar No' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Master Party' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Area' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Agent' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Transporter' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Distance' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        DtPartyDataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)
        DglParty.DataSource = DtPartyDataFields
        AgL.AddAgDataGrid(DglParty, PnlParty)
        FormatFieldGrid(DglParty)


        '''''''''''''''''''''''''''''''''''''''End For Party'''''''''''''''''''''''''''''''''''''''


        '''''''''''''''''''''''''''''''''''''''For Sale Invoice'''''''''''''''''''''''''''''''''''''''
        mQry = "Select '' as Srl, 'V_TYPE' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'V_NO' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'V_Date' as [Field Name], 'Date' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Invoice No' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, Should be unique.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sale To Party' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sale To Party Address' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sale To Party City' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sale To Party Pincode' as [Field Name], 'Text' as [Data Type], 6 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sale To Party Sales Tax No' as [Field Name], 'Text' as [Data Type], 20 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Bill To Party' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Agent' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Rate Type' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sales Tax Group Party' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, Registered / Unregistered / Composition' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Place Of Supply' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, Outside State / Within State' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sale To Party Doc No' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sale To Party Doc Date' as [Field Name], 'Date' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Remark' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Terms And Conditions' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Credit Limit' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Credit Days' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'SubTotal1' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Deduction_Per' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Deduction' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Other_Charge_Per' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Other_Charge' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Round_Off' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Net_Amount' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "

        DtSaleInvoice_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)
        DglSale1.DataSource = DtSaleInvoice_DataFields
        AgL.AddAgDataGrid(DglSale1, PnlSale1)
        FormatFieldGrid(DglSale1)


        mQry = "Select '' as Srl, 'V_TYPE' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Invoice No' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'TSr' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Sr Of Second Table' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Item Name' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Specification' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sales Tax Group Item' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, GST 0% / GST 5% / GST 12% / GST 18% / GST 28%' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Qty' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Unit' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Pcs' as [Field Name], 'Number' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Unit Multiplier' as [Field Name], 'Text' as [Data Type], 10 as [Length], 'Conversion from unit to deal unit.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Deal Unit' as [Field Name], 'Text' as [Data Type], 10 as [Length], 'If billing unit is different from unit then that billing unit will be save in deal unit other wise unit will be save here.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Deal Qty' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Rate' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Discount Per' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Discount Amount' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Additional Discount Per' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Additional Discount Amount' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Amount' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Remark' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Bale No' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Lot No' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Gross_Amount' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Taxable_Amount' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax1_Per' as [Field Name], 'Number' as [Data Type], Null as [Length], 'IGST Per' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax1' as [Field Name], 'Number' as [Data Type], Null as [Length], 'IGST Amount' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax2_Per' as [Field Name], 'Number' as [Data Type], Null as [Length], 'CGST Per' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax2' as [Field Name], 'Number' as [Data Type], Null as [Length], 'CGST Amount' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax3_Per' as [Field Name], 'Number' as [Data Type], Null as [Length], 'SGST Per' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax3' as [Field Name], 'Number' as [Data Type], Null as [Length], 'SGST Amount' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax4_Per' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax4' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax5_Per' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax5' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'SubTotal1' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        DtSaleInvoiceDetail_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)
        DglSale2.DataSource = DtSaleInvoiceDetail_DataFields
        AgL.AddAgDataGrid(DglSale2, PnlSale2)
        FormatFieldGrid(DglSale2)

        mQry = "Select '' as Srl, 'V_TYPE' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Invoice No' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select '' as Srl, 'TSr' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Sr Of Second Table' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sr' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Specification' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Pcs' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Qty' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'TotalQty' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        DtSaleInvoiceDimensionDetail_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)
        DglSale3.DataSource = DtSaleInvoiceDimensionDetail_DataFields
        AgL.AddAgDataGrid(DglSale3, PnlSale3)
        FormatFieldGrid(DglSale3)

        '''''''''''''''''''''''''''''''''''''''End For Sale Invoice'''''''''''''''''''''''''''''''''''''''



        '''''''''''''''''''''''''''''''''''''''For Purchase Invoice'''''''''''''''''''''''''''''''''''''''
        mQry = "Select '' as Srl, 'V_TYPE' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'V_NO' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'V_Date' as [Field Name], 'Date' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Invoice No' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, Should be unique.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Vendor' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Vendor Address' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Vendor City' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Vendor Pincode' as [Field Name], 'Text' as [Data Type], 6 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Vendor Mobile' as [Field Name], 'Text' as [Data Type], 20 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Vendor Sales Tax No' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Vendor GST No.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Vendor Doc No' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Vendor Doc Date' as [Field Name], 'Date' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Bill To Party' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Agent' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sales Tax Group Party' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, Registered / Unregistered / Composition' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Place Of Supply' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory, Outside State / Within State' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Ship To Address' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Remark' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'SubTotal1' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Deduction_Per' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Deduction' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Other_Charge_Per' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Other_Charge' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Round_Off' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Net_Amount' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        DtPurchInvoice_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)
        DglPurch1.DataSource = DtPurchInvoice_DataFields
        AgL.AddAgDataGrid(DglPurch1, PnlPurch1)
        FormatFieldGrid(DglPurch1)

        mQry = "Select '' as Srl, 'V_TYPE' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Invoice No' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select '' as Srl, 'TSr' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Sr Of Second Table' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Item Name' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Specification' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Bale No' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sales Tax Group Item' as [Field Name], 'Text' as [Data Type],  20 as [Length], 'Mandatory, GST 0% / GST 5% / GST 12% / GST 18% / GST 28%' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Profit Margin Per' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Qty' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Unit' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Pcs' as [Field Name], 'Number' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Deal Unit' as [Field Name], 'Text' as [Data Type], 50 as [Length], 'If billing unit is different from unit then that billing unit will be save in deal unit other wise unit will be save here.' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Deal Qty' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Rate' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Discount Per' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Discount Amount' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Additional Discount Per' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Additional Discount Amount' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Amount' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sale Rate' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'MRP' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Remark' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'LR No' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'LR Date' as [Field Name], 'Date' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Lot No' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Gross_Amount' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Taxable_Amount' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax1_Per' as [Field Name], 'Number' as [Data Type], Null as [Length], 'IGST Per' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax1' as [Field Name], 'Number' as [Data Type], Null as [Length], 'IGST Amount' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax2_Per' as [Field Name], 'Number' as [Data Type], Null as [Length], 'CGST Per' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax2' as [Field Name], 'Number' as [Data Type], Null as [Length], 'CGST Amount' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax3_Per' as [Field Name], 'Number' as [Data Type], Null as [Length], 'SGST Per' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax3' as [Field Name], 'Number' as [Data Type], Null as [Length], 'SGST Amount' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax4_Per' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax4' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax5_Per' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Tax5' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'SubTotal1' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        DtPurchInvoiceDetail_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)
        DglPurch2.DataSource = DtPurchInvoiceDetail_DataFields
        AgL.AddAgDataGrid(DglPurch2, PnlPurch2)
        FormatFieldGrid(DglPurch2)


        mQry = "Select '' as Srl, 'V_TYPE' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Invoice No' as [Field Name], 'Text' as [Data Type], 20 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select '' as Srl, 'TSr' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Sr Of Second Table' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Sr' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Specification' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Pcs' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Qty' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'TotalQty' as [Field Name], 'Number' as [Data Type], Null as [Length], '' as Remark "
        DtPurchInvoiceDimensionDetail_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)
        DglPurch3.DataSource = DtPurchInvoiceDimensionDetail_DataFields
        AgL.AddAgDataGrid(DglPurch3, PnlPurch3)
        FormatFieldGrid(DglPurch3)

        '''''''''''''''''''''''''''''''''''''''End For Purchase Invoice'''''''''''''''''''''''''''''''''''''''
    End Sub

    Private Sub FormatFieldGrid(DGL As AgControls.AgDataGrid)
        DGL.ColumnHeadersHeight = 30
        DGL.EnableHeadersVisualStyles = False
        AgL.GridDesign(DGL)
        DGL.Columns(0).Width = 40
        DGL.Columns(1).Width = 180
        DGL.Columns(2).Width = 90
        DGL.Columns(3).Width = 70
        DGL.Columns(4).Width = 550
        DGL.ReadOnly = True
        DGL.AllowUserToAddRows = False
        DGL.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        DGL.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
        DGL.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGL.Columns(3).HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
    End Sub

    Private Sub FCreateDataTables_ForTableDataStructure()
        mQry = "Select * From Item Where 1=2"
        mItemTableStructure = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = "Select * From RateList Where 1=2"
        mRateListTableStructure = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = "Select * From RateListDetail Where 1=2"
        mRateListDetailTableStructure = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mItemTableStructure.BeginLoadData()
        mRateListTableStructure.BeginLoadData()
        mRateListDetailTableStructure.BeginLoadData()
    End Sub

    Private Sub FCreateDataTables_ForFetchingData()
        mQry = "Select * From Item"
        mItemTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = "Select * From ItemGroup"
        mItemGroupTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = "Select * From ItemCategory"
        mItemCategoryTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = "Select * From RateType"
        mRateTypeTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub


    Private Sub FrmImportData_Test_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 654, 990, 0, 0)
        Ini_Grid()
        FCreateDataTables_ForTableDataStructure()
        FCreateDataTables_ForFetchingData()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSelectExcelFile_Item.Click,
            BtnSelectExcelFile_ItemRateList.Click, BtnSelectExcelFile_Party.Click,
            BtnSelectExcelFile_SaleInvoice.Click, BtnSelectExcelFile_SaleInvoiceDetail.Click, BtnSelectExcelFile_SaleInvoiceDimensionDetail.Click,
            BtnSelectExcelFile_PurchInvoice.Click, BtnSelectExcelFile_PurchInvoiceDetail.Click, BtnSelectExcelFile_PurchInvoiceDimensionDetail.Click
        Dim MyCommand As System.Data.OleDb.OleDbDataAdapter = Nothing
        Opn.ShowDialog()

        Select Case sender.Name
            Case BtnSelectExcelFile_Item.Name
                TxtExcelPath_Item.Text = Opn.FileName
                'MyConnection_Item = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;data source='" & TxtExcelPath_Item.Text & " '; " & "Extended Properties=Excel 8.0;")
                MyConnection_Item = New System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + TxtExcelPath_Item.Text + ";Extended Properties=Excel 12.0;")
                MyConnection_Item.Open()

            Case BtnSelectExcelFile_ItemRateList.Name
                TxtExcelPath_ItemRateList.Text = Opn.FileName
                MyConnection_ItemRateList = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;data source='" & TxtExcelPath_ItemRateList.Text & " '; " & "Extended Properties=Excel 8.0;")
                MyConnection_ItemRateList.Open()

            Case BtnSelectExcelFile_Party.Name
                TxtExcelPath_Party.Text = Opn.FileName
                MyConnection_Party = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;data source='" & TxtExcelPath_Party.Text & " '; " & "Extended Properties=Excel 8.0;")
                MyConnection_Party.Open()

            Case BtnSelectExcelFile_SaleInvoice.Name
                TxtExcelPath_SaleInvoice.Text = Opn.FileName
                MyConnection_SaleInvoice = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;data source='" & TxtExcelPath_SaleInvoice.Text & " '; " & "Extended Properties=Excel 8.0;")
                MyConnection_SaleInvoice.Open()

            Case BtnSelectExcelFile_SaleInvoiceDetail.Name
                TxtExcelPath_SaleInvoiceDetail.Text = Opn.FileName
                MyConnection_SaleInvoiceDetail = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;data source='" & TxtExcelPath_SaleInvoiceDetail.Text & " '; " & "Extended Properties=Excel 8.0;")
                MyConnection_SaleInvoiceDetail.Open()

            Case BtnSelectExcelFile_SaleInvoiceDimensionDetail.Name
                TxtExcelPath_SaleInvoiceDimensionDetail.Text = Opn.FileName
                MyConnection_SaleInvoiceDimensionDetail = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;data source='" & TxtExcelPath_SaleInvoiceDimensionDetail.Text & " '; " & "Extended Properties=Excel 8.0;")
                MyConnection_SaleInvoiceDimensionDetail.Open()

            Case BtnSelectExcelFile_PurchInvoice.Name
                TxtExcelPath_PurchInvoice.Text = Opn.FileName
                MyConnection_PurchaseInvoice = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;data source='" & TxtExcelPath_PurchInvoice.Text & " '; " & "Extended Properties=Excel 8.0;")
                MyConnection_PurchaseInvoice.Open()

            Case BtnSelectExcelFile_PurchInvoiceDetail.Name
                TxtExcelPath_PurchInvoiceDetail.Text = Opn.FileName
                MyConnection_PurchaseInvoiceDetail = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;data source='" & TxtExcelPath_PurchInvoiceDetail.Text & " '; " & "Extended Properties=Excel 8.0;")
                MyConnection_PurchaseInvoiceDetail.Open()

            Case BtnSelectExcelFile_PurchInvoiceDimensionDetail.Name
                TxtExcelPath_PurchInvoiceDimensionDetail.Text = Opn.FileName
                MyConnection_PurchaseInvoiceDimensionDetail = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;data source='" & TxtExcelPath_PurchInvoiceDimensionDetail.Text & " '; " & "Extended Properties=Excel 8.0;")
                MyConnection_PurchaseInvoiceDimensionDetail.Open()
        End Select
    End Sub
    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click, BtnCancel.Click
        DsExcelData_Item = New DataSet
        DsExcelData_ItemRateList = New DataSet
        DsExcelData_Party = New DataSet

        DsExcelData_SaleInvoice = New DataSet
        DsExcelData_SaleInvoiceDetail = New DataSet
        DsExcelData_SaleInvoiceDimensionDetail = New DataSet

        DsExcelData_PurchaseInvoice = New DataSet
        DsExcelData_PurchaseInvoiceDetail = New DataSet
        DsExcelData_PurchaseInvoiceDimensionDetail = New DataSet



        Dim DtSheetNames As DataTable = Nothing
        Dim MyCommand As OleDb.OleDbDataAdapter = Nothing
        Select Case sender.name
            Case BtnOK.Name
                If TxtExcelPath_Item.Text <> "" Then
                    FCheckSheetValidity(MyConnection_Item.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing), "Item")
                    MyCommand = New System.Data.OleDb.OleDbDataAdapter("select *  from [sheet1$] ", MyConnection_Item)
                    MyCommand.Fill(DsExcelData_Item)
                End If

                If TxtExcelPath_ItemRateList.Text <> "" Then
                    FCheckSheetValidity(MyConnection_ItemRateList.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing), "Item Rate List")
                    MyCommand = New System.Data.OleDb.OleDbDataAdapter("select *  from [sheet1$] ", MyConnection_ItemRateList)
                    MyCommand.Fill(DsExcelData_ItemRateList)
                End If

                If TxtExcelPath_Party.Text <> "" Then
                    FCheckSheetValidity(MyConnection_Party.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing), "Party")
                    MyCommand = New System.Data.OleDb.OleDbDataAdapter("select *  from [sheet1$] ", MyConnection_Party)
                    MyCommand.Fill(DsExcelData_Party)
                End If

                If TxtExcelPath_SaleInvoice.Text <> "" Then
                    FCheckSheetValidity(MyConnection_SaleInvoice.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing), "Sale Invoice")
                    MyCommand = New System.Data.OleDb.OleDbDataAdapter("select *  from [sheet1$] ", MyConnection_SaleInvoice)
                    MyCommand.Fill(DsExcelData_SaleInvoice)
                End If

                If TxtExcelPath_SaleInvoiceDetail.Text <> "" Then
                    FCheckSheetValidity(MyConnection_SaleInvoiceDetail.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing), "Sale Invoice Detail")
                    MyCommand = New System.Data.OleDb.OleDbDataAdapter("select *  from [sheet1$] ", MyConnection_SaleInvoiceDetail)
                    MyCommand.Fill(DsExcelData_SaleInvoiceDetail)
                End If

                If TxtExcelPath_SaleInvoiceDimensionDetail.Text <> "" Then
                    FCheckSheetValidity(MyConnection_SaleInvoiceDimensionDetail.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing), "Sale Invoice Dimension Detail")
                    MyCommand = New System.Data.OleDb.OleDbDataAdapter("select *  from [sheet1$] ", MyConnection_SaleInvoiceDimensionDetail)
                    MyCommand.Fill(DsExcelData_SaleInvoiceDimensionDetail)
                End If

                If TxtExcelPath_PurchInvoice.Text <> "" Then
                    FCheckSheetValidity(MyConnection_PurchaseInvoice.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing), "Purchase Invoice")
                    MyCommand = New System.Data.OleDb.OleDbDataAdapter("select *  from [sheet1$] ", MyConnection_PurchaseInvoice)
                    MyCommand.Fill(DsExcelData_PurchaseInvoice)
                End If

                If TxtExcelPath_PurchInvoiceDetail.Text <> "" Then
                    FCheckSheetValidity(MyConnection_PurchaseInvoiceDetail.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing), "Purchase Invoice Detail")
                    MyCommand = New System.Data.OleDb.OleDbDataAdapter("select *  from [sheet1$] ", MyConnection_PurchaseInvoiceDetail)
                    MyCommand.Fill(DsExcelData_PurchaseInvoiceDetail)
                End If

                If TxtExcelPath_PurchInvoiceDimensionDetail.Text <> "" Then
                    FCheckSheetValidity(MyConnection_PurchaseInvoiceDimensionDetail.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing), "Purchase Invoice Dimension Detail")
                    MyCommand = New System.Data.OleDb.OleDbDataAdapter("select *  from [sheet1$] ", MyConnection_PurchaseInvoiceDimensionDetail)
                    MyCommand.Fill(DsExcelData_PurchaseInvoiceDimensionDetail)
                End If


                FImportItemFromExcel()
                'FImportRateListFromExcel()

            Case BtnCancel.Name
                Me.Dispose()
        End Select
    End Sub

    Private Function FCheckSheetValidity(DtSheetNames As DataTable, FileName As String) As String
        Dim bValidationString As String = ""
        Dim IsShee1Exist As Boolean = False
        For I As Integer = 0 To DtSheetNames.Rows.Count - 1
            If AgL.StrCmp(DtSheetNames.Rows(I)("Table_Name"), "sheet1$") Then
                IsShee1Exist = True
                Exit For
            End If
        Next

        If IsShee1Exist = False Then
            bValidationString = "Sheet1 does not exist in " + FileName + "."
        End If

        Return bValidationString
    End Function

    Private Sub FrmImportPurchaseFromExcel_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Public Sub FImportItemFromExcel()
        Dim mTrans As String = ""
        Dim DtTemp As DataTable
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer


        DtTemp = DsExcelData_Item.Tables(0)

        For I = 0 To DtItemDataFields.Rows.Count - 1
            If AgL.XNull(DtItemDataFields.Rows(I)("Remark")).ToString().Contains("Mandatory") Then
                If Not DtTemp.Columns.Contains(AgL.XNull(DtItemDataFields.Rows(I)("Field Name")).ToString()) Then
                    If ErrorLog.Contains("These fields are not present is excel file") = False Then
                        ErrorLog += vbCrLf & "These fields are not present is excel file" & vbCrLf
                        ErrorLog += AgL.XNull(DtItemDataFields.Rows(I)("Field Name")).ToString() & ", "
                    Else
                        ErrorLog += AgL.XNull(DtItemDataFields.Rows(I)("Field Name")).ToString() & ", "
                    End If
                End If
            End If
        Next


        If DtTemp.Columns.Contains("Sales Tax Group") Then
            Dim DtSalesTaxGroup = DtTemp.DefaultView.ToTable(True, "Sales Tax Group")
            For I = 0 To DtSalesTaxGroup.Rows.Count - 1
                If AgL.XNull(DtSalesTaxGroup.Rows(I)("Sales Tax Group")) <> "" Then
                    If AgL.Dman_Execute("SELECT Count(*) From PostingGroupSalesTaxItem where Description = '" & AgL.XNull(DtSalesTaxGroup.Rows(I)("Sales Tax Group")) & "' ", AgL.GCn).ExecuteScalar = 0 Then
                        If ErrorLog.Contains("These Sales Tax Groups Are Not Present In Master") = False Then
                            ErrorLog += vbCrLf & "These Sales Tax Groups Are Not Present In Master" & vbCrLf
                            ErrorLog += AgL.XNull(DtSalesTaxGroup.Rows(I)("Sales Tax Group")) & ", "
                        Else
                            ErrorLog += AgL.XNull(DtSalesTaxGroup.Rows(I)("Sales Tax Group")) & ", "
                        End If
                    End If
                End If
            Next
        End If

        If DtTemp.Columns.Contains("Unit") Then
            Dim DtUnit = DtTemp.DefaultView.ToTable(True, "Unit")
            For I = 0 To DtUnit.Rows.Count - 1
                If AgL.XNull(DtUnit.Rows(I)("Unit")) <> "" Then
                    If AgL.Dman_Execute("SELECT Count(*) From Unit where Code = '" & AgL.XNull(DtUnit.Rows(I)("Unit")) & "' ", AgL.GCn).ExecuteScalar = 0 Then
                        If ErrorLog.Contains("These units are not present in master") = False Then
                            ErrorLog += vbCrLf & "These Unit Are Not Present In Master" & vbCrLf
                            ErrorLog += AgL.XNull(DtUnit.Rows(I)("Unit")) & ", "
                        Else
                            ErrorLog += AgL.XNull(DtUnit.Rows(I)("Unit")) & ", "
                        End If
                    End If
                End If
            Next
        End If

        For I = 0 To DtTemp.Rows.Count - 1
            For J As Integer = 0 To DtItemDataFields.Rows.Count - 1
                If DtTemp.Columns.Contains(DtItemDataFields.Rows(J)("Field Name")) Then
                    If DtItemDataFields.Rows(J)("Remark").ToString().Contains("Mandatory") Then
                        If AgL.XNull(DtTemp.Rows(I)(DtItemDataFields.Rows(J)("Field Name"))) = "" Then
                            ErrorLog += DtItemDataFields.Rows(J)("Field Name") + " is blank at row no." + (I + 2).ToString() & vbCrLf
                        End If
                    End If
                End If
            Next
        Next

        If ErrorLog <> "" Then
            If File.Exists(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt") Then
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt", ErrorLog, False)
            Else
                File.Create(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt")
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt", ErrorLog, False)
            End If
            System.Diagnostics.Process.Start("notepad.exe", My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt")
            Exit Sub
        End If

        Dim mSqlConn As New SqlClient.SqlConnection
        Dim mSqlCmd As New SqlClient.SqlCommand
        Dim mSqlTrans As SqlClient.SqlTransaction

        mSqlConn.ConnectionString = AgL.GCn.ConnectionString
        mSqlConn.Open()
        mSqlCmd.Connection = mSqlConn
        mSqlTrans = mSqlConn.BeginTransaction()
        mSqlCmd.Transaction = mSqlTrans

        Try
            Dim bLastItemCategoryCode = AgL.GetMaxId("Item", "Code", mSqlConn, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, mSqlCmd, AgL.Gcn_ConnectionString)

            Dim DtItemCategory = DtTemp.DefaultView.ToTable(True, "Item Category", "Sales Tax Group")
            For I = 0 To DtItemCategory.Rows.Count - 1
                If AgL.XNull(DtItemCategory.Rows(I)("Item Category")) <> "" Then
                    Dim ItemCategoryTable As New StructItem
                    Dim bItemCategoryCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemCategoryCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                    ItemCategoryTable.Code = bItemCategoryCode
                    ItemCategoryTable.Description = AgL.XNull(DtItemCategory.Rows(I)("Item Category")).ToString.Trim
                    ItemCategoryTable.ItemType = "TP"
                    ItemCategoryTable.SalesTaxPostingGroup = AgL.XNull(DtItemCategory.Rows(I)("Sales Tax Group"))
                    ItemCategoryTable.V_Type = "IC"
                    ItemCategoryTable.Unit = "Nos"
                    ItemCategoryTable.PurchaseRate = 0
                    ItemCategoryTable.Rate = 0
                    ItemCategoryTable.Mrp = 0
                    ItemCategoryTable.EntryBy = AgL.PubUserName
                    ItemCategoryTable.EntryDate = AgL.PubLoginDate
                    ItemCategoryTable.EntryType = "Add"
                    ItemCategoryTable.EntryStatus = LogStatus.LogOpen
                    ItemCategoryTable.Div_Code = AgL.PubDivCode
                    ItemCategoryTable.Status = "Active"

                    FCreateItemDataTable(ItemCategoryTable)
                End If
            Next
            FInsertDataTableToDatabase("Item", "H.Code = H_Temp.Code", mSqlConn, mSqlCmd, mItemTableStructure, "[#Temp_ItemCategory]")

            mItemCategoryTable = AgL.FillData("Select * From ItemCategory With (NoLock) ", AgL.GcnRead).Tables(0)


            Dim bLastItemGroupCode = AgL.GetMaxId("Item", "Code", mSqlConn, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, mSqlCmd, AgL.Gcn_ConnectionString)

            Dim DtItemGroup = DtTemp.DefaultView.ToTable(True, "Item Group", "Item Category", "Sales Tax Group")
            For I = 0 To DtItemGroup.Rows.Count - 1
                If AgL.XNull(DtItemGroup.Rows(I)("Item Group")) <> "" Then
                    Dim ItemGroupTable As New StructItem
                    Dim bItemGroupCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemGroupCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                    ItemGroupTable.Code = bItemGroupCode
                    ItemGroupTable.Description = AgL.XNull(DtItemGroup.Rows(I)("Item Group")).ToString.Trim
                    ItemGroupTable.ItemCategory = AgL.XNull(DtItemGroup.Rows(I)("Item Category")).ToString.Trim
                    ItemGroupTable.ItemType = "TP"
                    ItemGroupTable.SalesTaxPostingGroup = AgL.XNull(DtItemGroup.Rows(I)("Sales Tax Group"))
                    ItemGroupTable.V_Type = "IG"
                    ItemGroupTable.Unit = "Pcs"
                    ItemGroupTable.PurchaseRate = 0
                    ItemGroupTable.Rate = 0
                    ItemGroupTable.MRP = 0
                    ItemGroupTable.EntryBy = AgL.PubUserName
                    ItemGroupTable.EntryDate = AgL.PubLoginDate
                    ItemGroupTable.EntryType = "Add"
                    ItemGroupTable.EntryStatus = LogStatus.LogOpen
                    ItemGroupTable.Div_Code = AgL.PubDivCode
                    ItemGroupTable.Status = "Active"

                    FCreateItemDataTable(ItemGroupTable)
                End If
            Next
            FInsertDataTableToDatabase("Item", "H.Code = H_Temp.Code", mSqlConn, mSqlCmd, mItemTableStructure, "[#Temp_ItemGroup]")

            mItemGroupTable = AgL.FillData("Select * From ItemGroup With (NoLock) ", AgL.GcnRead).Tables(0)

            Dim bLastItemCode As String = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, mSqlCmd, AgL.Gcn_ConnectionString)

            For I = 0 To DtTemp.Rows.Count - 1
                If AgL.XNull(DtTemp.Rows(I)("Item Name")) <> "" Then

                    Dim ItemTable As New StructItem
                    Dim bItemCode As String = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(bLastItemCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")

                    ItemTable.Code = bItemCode
                    ItemTable.ManualCode = AgL.XNull(DtTemp.Rows(I)("Item Code"))
                    ItemTable.Description = AgL.XNull(DtTemp.Rows(I)("Item Name"))
                    ItemTable.DisplayName = AgL.XNull(DtTemp.Rows(I)("Item Display Name"))
                    ItemTable.Specification = AgL.XNull(DtTemp.Rows(I)("Specification"))
                    ItemTable.ItemGroup = AgL.XNull(DtTemp.Rows(I)("Item Group")).ToString.Trim
                    ItemTable.ItemCategory = AgL.XNull(DtTemp.Rows(I)("Item Category")).ToString.Trim
                    ItemTable.ItemType = "TP"
                    ItemTable.V_Type = "ITEM"
                    ItemTable.Unit = AgL.XNull(DtTemp.Rows(I)("Unit"))
                    ItemTable.PurchaseRate = AgL.XNull(DtTemp.Rows(I)("Purchase Rate"))
                    ItemTable.Rate = AgL.XNull(DtTemp.Rows(I)("Sale Rate"))
                    ItemTable.MRP = 0
                    ItemTable.SalesTaxPostingGroup = AgL.XNull(DtTemp.Rows(I)("Sales Tax Group"))
                    ItemTable.HSN = AgL.XNull(DtTemp.Rows(I)("HSN Code"))
                    ItemTable.EntryBy = AgL.PubUserName
                    ItemTable.EntryDate = AgL.PubLoginDate
                    ItemTable.EntryType = "Add"
                    ItemTable.EntryStatus = LogStatus.LogOpen
                    ItemTable.Div_Code = AgL.PubDivCode
                    ItemTable.Status = "Active"
                    ItemTable.StockYN = 1
                    ItemTable.IsSystemDefine = 0

                    FCreateItemDataTable(ItemTable)
                End If
            Next
            FInsertDataTableToDatabase("Item", "H.Code = H_Temp.Code", mSqlConn, mSqlCmd, mItemTableStructure)

            mItemTable = AgL.FillData("Select * From Item With (NoLock) ", AgL.GcnRead).Tables(0)

            'mQry = " INSERT INTO RateList(Code, WEF, RateType, EntryBy, EntryDate, EntryType, 
            '            EntryStatus, Status, Div_Code) 
            '            Select I.Code, '01/Apr/2000' As WEF,Null As RateType ,
            '            I.EntryBy, I.EntryDate, I.EntryType, 
            '            I.EntryStatus, I.Status, I.Div_Code
            '            From Item I With (NoLock) "
            'AgL.Dman_ExecuteNonQry(mQry, mSqlConn, mSqlCmd)

            'mQry = "INSERT INTO RateListDetail(Code, Sr, WEF, Item, RateType, Rate) 
            '            Select I.Code, 1 As Sr, '01/Apr/2000' As WEF, I.Code As Item, Null As RateType, I.Rate From Item I  With (NoLock) "
            'AgL.Dman_ExecuteNonQry(mQry, mSqlConn, mSqlCmd)

            mQry = "UPDATE Item SET ShowItemInOtherDivisions = 1 WHERE ShowItemInOtherDivisions IS NULL
                    UPDATE Item SET ShowItemInOtherSites = 1 WHERE ShowItemInOtherSites IS NULL
                    UPDATE Item SET MaintainStockYn = 1 WHERE MaintainStockYn IS NULL"
            AgL.Dman_ExecuteNonQry(mQry, mSqlConn, mSqlCmd)

            mSqlTrans.Commit()
            mSqlConn.Close()
            MsgBox("Process Completed Succesfully.", MsgBoxStyle.Information)
        Catch ex As Exception
            mSqlTrans.Rollback()
            mSqlConn.Close()
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub FImportRateListFromExcel()
        Dim mTrans As String = ""
        Dim DtTemp As DataTable
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer


        DtTemp = DsExcelData_ItemRateList.Tables(0)

        Dim DtRateTypes As DataTable = AgL.FillData("Select Description From RateType ", AgL.GCn).Tables(0)

        For I = 0 To DtRateListDataFields.Rows.Count - 1
            If AgL.XNull(DtRateListDataFields.Rows(I)("Remark")).ToString().Contains("Mandatory") Then
                If Not DtTemp.Columns.Contains(AgL.XNull(DtRateListDataFields.Rows(I)("Field Name")).ToString()) Then
                    If ErrorLog.Contains("These fields are not present is excel file") = False Then
                        ErrorLog += vbCrLf & "These fields are not present is excel file" & vbCrLf
                        ErrorLog += AgL.XNull(DtRateListDataFields.Rows(I)("Field Name")).ToString() & ", "
                    Else
                        ErrorLog += AgL.XNull(DtRateListDataFields.Rows(I)("Field Name")).ToString() & ", "
                    End If
                End If
            End If
        Next

        For I = 0 To DtTemp.Rows.Count - 1
            For J As Integer = 0 To DtRateListDataFields.Rows.Count - 1
                If DtTemp.Columns.Contains(DtRateListDataFields.Rows(J)("Field Name")) Then
                    If DtRateListDataFields.Rows(J)("Remark").ToString().Contains("Mandatory") Then
                        If AgL.XNull(DtTemp.Rows(I)(DtRateListDataFields.Rows(J)("Field Name"))) = "" Then
                            ErrorLog += DtRateListDataFields.Rows(J)("Field Name") + " is blank at row no." + (I + 2).ToString() & vbCrLf
                        End If
                    End If
                End If
            Next

            If DtTemp.Columns.Contains("Item Name") Then
                If AgL.XNull(DtTemp.Rows(I)("Item Name")) <> "" Then
                    DtItemRow = mItemTable.Select(" Description = " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Item Name"))) & "")
                    If DtItemRow.Length = 0 Then
                        If ErrorLog.Contains("These Items Are Not Present In Master") = False Then
                            ErrorLog += vbCrLf & "These Items Are Not Present In Master" & vbCrLf
                            ErrorLog += AgL.XNull(DtTemp.Rows(I)("Item Name")) & ", "
                        Else
                            ErrorLog += AgL.XNull(DtTemp.Rows(I)("Item Name")) & ", "
                        End If
                    End If
                End If
            End If
        Next



        If ErrorLog <> "" Then
            If File.Exists(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt") Then
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt", ErrorLog, False)
            Else
                File.Create(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt")
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt", ErrorLog, False)
            End If
            System.Diagnostics.Process.Start("notepad.exe", My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt")
            Exit Sub
        End If

        Dim mSqlConn As New SqlClient.SqlConnection
        Dim mSqlCmd As New SqlClient.SqlCommand
        Dim mSqlTrans As SqlClient.SqlTransaction

        mSqlConn.ConnectionString = AgL.GCn.ConnectionString
        mSqlConn.Open()
        mSqlCmd.Connection = mSqlConn
        mSqlTrans = mSqlConn.BeginTransaction()
        mSqlCmd.Transaction = mSqlTrans

        Try
            Dim bItemCode As String = ""

            For I = 0 To DtTemp.Rows.Count - 1
                For J As Integer = 0 To DtTemp.Columns.Count - 1
                    For K As Integer = 0 To DtRateTypes.Rows.Count - 1
                        If DtTemp.Columns(J).ColumnName.ToUpper() = DtRateTypes.Rows(K)("Description").ToUpper() Then
                            If AgL.VNull(DtTemp.Rows(I)(J)) > 0 Then
                                Dim RateListTable As New StructRateList
                                'bItemCode = AgL.XNull(AgL.Dman_Execute("Select Code From Item With (NoLock) Where Description = " & AgL.Chk_Text(DtTemp.Rows(I)("Item Name").ToString.Trim) & "", AgL.GcnRead).ExecuteScalar())

                                DtItemRow = mItemTable.Select(" Description = " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Item Name"))) & "")
                                If DtItemRow.Length > 0 Then
                                    bItemCode = DtItemRow(0)("Code")
                                End If

                                RateListTable.Code = bItemCode
                                RateListTable.WEF = AgL.PubLoginDate
                                RateListTable.RateType = ""
                                RateListTable.EntryBy = AgL.PubUserName
                                RateListTable.EntryDate = AgL.PubLoginDate
                                RateListTable.EntryType = "Add"
                                RateListTable.EntryStatus = LogStatus.LogOpen
                                RateListTable.Status = "Active"
                                RateListTable.Div_Code = AgL.PubDivCode
                                RateListTable.Line_Sr = 0
                                RateListTable.Line_WEF = AgL.PubStartDate
                                RateListTable.Line_Item = bItemCode
                                RateListTable.Line_RateType = DtRateTypes.Rows(K)("Description")
                                RateListTable.Line_Rate = AgL.VNull(DtTemp.Rows(I)(J))

                                FCreateRateListDataTable(RateListTable)
                            End If
                        End If
                    Next
                Next
            Next
            FInsertDataTableToDatabase("RateList", "H.Code = H_Temp.Code", mSqlConn, mSqlCmd, mRateListTableStructure)
            FInsertDataTableToDatabase("RateListDetail", "H.Code = H_Temp.Code And H.RateType = H_Temp.RateType", mSqlConn, mSqlCmd, mRateListDetailTableStructure)

            mSqlTrans.Commit()
            mSqlConn.Close()
            MsgBox("Process Completed Succesfully.", MsgBoxStyle.Information)
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FInsertDataTableToDatabase(bTableName As String, bJoinCondStr As String,
                                           mSqlConn As SqlConnection,
                                           mSqlCmd As SqlCommand, DtData As DataTable,
                                           Optional bMyTempTableName As String = "")
        Dim mTrans As String = ""
        Dim DtFields As DataTable = Nothing
        Dim DtSqliteTableData As DataTable = Nothing
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim K As Integer = 0
        Dim StrColumnList As String = ""
        Dim bTempTableName As String = ""
        If bMyTempTableName <> "" Then
            bTempTableName = bMyTempTableName
        Else
            bTempTableName = "[#Temp_" + bTableName + "]"
        End If

        mQry = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.Columns With (NoLock) WHERE TABLE_NAME = '" & bTableName & "'  
                ORDER BY ORDINAL_POSITION "
        DtFields = AgL.FillData(mQry, AgL.GCn).Tables(0)
        StrColumnList = ""
        For J = 0 To DtFields.Rows.Count - 1
            If StrColumnList = "" Then
                StrColumnList = DtFields.Rows(J)("COLUMN_NAME")
            Else
                StrColumnList += ", " & DtFields.Rows(J)("COLUMN_NAME")
            End If
        Next

        mQry = "SELECT * INTO " & bTempTableName & " FROM " & bTableName & " WHERE 1 = 2 "
        AgL.Dman_ExecuteNonQry(mQry, mSqlConn, mSqlCmd)


        Using bulkCopy As SqlBulkCopy = New SqlBulkCopy(mSqlConn, SqlBulkCopyOptions.Default, mSqlCmd.Transaction)
            bulkCopy.DestinationTableName = bTempTableName
            bulkCopy.BulkCopyTimeout = 500
            bulkCopy.WriteToServer(DtData)
        End Using


        StrColumnList = StrColumnList.Replace("SmallDateTime", "DateTime")

        mQry = "INSERT INTO " & bTableName & "(" & StrColumnList & ")
                Select H_Temp." & Replace(StrColumnList, ",", ",H_Temp.") & "
                From " & bTempTableName & " H_Temp 
                LEFT JOIN " & bTableName & " H On " & bJoinCondStr &
                " Where H.Code Is Null "
        AgL.Dman_ExecuteNonQry(mQry, mSqlConn, mSqlCmd)
    End Sub
    Private Sub FCreateItemDataTable(ItemTable As StructItem)
        mItemTableStructure.Rows.Add()
        mItemTableStructure.Rows(mItemTableStructure.Rows.Count - 1)("Code") = ItemTable.Code
        mItemTableStructure.Rows(mItemTableStructure.Rows.Count - 1)("ManualCode") = ItemTable.ManualCode

        If ItemTable.Description IsNot Nothing Then
            If ItemTable.Description.Length > 100 Then
                mItemTableStructure.Rows(mItemTableStructure.Rows.Count - 1)("Description") = ItemTable.Description.Substring(1, 99)
            Else
                mItemTableStructure.Rows(mItemTableStructure.Rows.Count - 1)("Description") = ItemTable.Description
            End If
        End If

        If ItemTable.DisplayName IsNot Nothing Then
            If ItemTable.DisplayName.Length > 100 Then
                mItemTableStructure.Rows(mItemTableStructure.Rows.Count - 1)("DisplayName") = ItemTable.DisplayName.Substring(1, 99)
            Else
                mItemTableStructure.Rows(mItemTableStructure.Rows.Count - 1)("DisplayName") = ItemTable.DisplayName
            End If
        End If

        mItemTableStructure.Rows(mItemTableStructure.Rows.Count - 1)("Specification") = ItemTable.Specification

        DtItemGroupRow = mItemGroupTable.Select(" Description = '" & ItemTable.ItemGroup & "'")
        If DtItemGroupRow.Length > 0 Then
            mItemTableStructure.Rows(mItemTableStructure.Rows.Count - 1)("ItemGroup") = DtItemGroupRow(0)("Code")
        End If

        DtItemCategoryRow = mItemCategoryTable.Select(" Description = '" & ItemTable.ItemCategory & "'")
        If DtItemCategoryRow.Length > 0 Then
            mItemTableStructure.Rows(mItemTableStructure.Rows.Count - 1)("ItemCategory") = DtItemCategoryRow(0)("Code")
        End If

        mItemTableStructure.Rows(mItemTableStructure.Rows.Count - 1)("ItemType") = ItemTable.ItemType
        mItemTableStructure.Rows(mItemTableStructure.Rows.Count - 1)("Unit") = ItemTable.Unit
        mItemTableStructure.Rows(mItemTableStructure.Rows.Count - 1)("PurchaseRate") = ItemTable.PurchaseRate
        mItemTableStructure.Rows(mItemTableStructure.Rows.Count - 1)("Rate") = ItemTable.Rate
        mItemTableStructure.Rows(mItemTableStructure.Rows.Count - 1)("MRP") = ItemTable.MRP

        mItemTableStructure.Rows(mItemTableStructure.Rows.Count - 1)("SalesTaxPostingGroup") = ItemTable.SalesTaxPostingGroup
        mItemTableStructure.Rows(mItemTableStructure.Rows.Count - 1)("HSN") = ItemTable.HSN
        mItemTableStructure.Rows(mItemTableStructure.Rows.Count - 1)("EntryBy") = ItemTable.EntryBy
        mItemTableStructure.Rows(mItemTableStructure.Rows.Count - 1)("EntryDate") = ItemTable.EntryDate
        mItemTableStructure.Rows(mItemTableStructure.Rows.Count - 1)("EntryType") = ItemTable.EntryType
        mItemTableStructure.Rows(mItemTableStructure.Rows.Count - 1)("EntryStatus") = ItemTable.EntryStatus
        mItemTableStructure.Rows(mItemTableStructure.Rows.Count - 1)("Status") = ItemTable.Status
        mItemTableStructure.Rows(mItemTableStructure.Rows.Count - 1)("Div_Code") = ItemTable.Div_Code
        mItemTableStructure.Rows(mItemTableStructure.Rows.Count - 1)("V_Type") = ItemTable.V_Type
        'mItemDataTable.Rows(mItemDataTable.Rows.Count - 1)("StockYN") = ItemTable.StockYN
        'mItemDataTable.Rows(mItemDataTable.Rows.Count - 1)("IsSystemDefine") = ItemTable.IsSystemDefine
    End Sub


    Private Sub FCreateRateListDataTable(ItemRateListTable As StructRateList)
        DtRows_RateList = mRateListTableStructure.Select("[Code] = '" & ItemRateListTable.Code & "'")

        If DtRows_RateList.Length = 0 Then
            mRateListTableStructure.Rows.Add()
            mRateListTableStructure.Rows(mRateListTableStructure.Rows.Count - 1)("Code") = ItemRateListTable.Code
            mRateListTableStructure.Rows(mRateListTableStructure.Rows.Count - 1)("WEF") = ItemRateListTable.WEF
            mRateListTableStructure.Rows(mRateListTableStructure.Rows.Count - 1)("RateType") = ItemRateListTable.RateType
            mRateListTableStructure.Rows(mRateListTableStructure.Rows.Count - 1)("EntryBy") = ItemRateListTable.EntryBy
            mRateListTableStructure.Rows(mRateListTableStructure.Rows.Count - 1)("EntryDate") = ItemRateListTable.EntryDate
            mRateListTableStructure.Rows(mRateListTableStructure.Rows.Count - 1)("EntryType") = ItemRateListTable.EntryType
            mRateListTableStructure.Rows(mRateListTableStructure.Rows.Count - 1)("EntryStatus") = ItemRateListTable.EntryStatus
            mRateListTableStructure.Rows(mRateListTableStructure.Rows.Count - 1)("Status") = ItemRateListTable.Status
            mRateListTableStructure.Rows(mRateListTableStructure.Rows.Count - 1)("Div_Code") = ItemRateListTable.Div_Code
        End If


        DtRows_RateListDetail = mRateListDetailTableStructure.Select("[Code] = '" & ItemRateListTable.Code & "'
            And [RateType] = '" & ItemRateListTable.Line_RateType & "'")

        If DtRows_RateListDetail.Length = 0 Then
            mRateListDetailTableStructure.Rows.Add()
            mRateListDetailTableStructure.Rows(mRateListDetailTableStructure.Rows.Count - 1)("Code") = ItemRateListTable.Code
            mRateListDetailTableStructure.Rows(mRateListDetailTableStructure.Rows.Count - 1)("Sr") = mRateListDetailTableStructure.Rows.Count
            mRateListDetailTableStructure.Rows(mRateListDetailTableStructure.Rows.Count - 1)("WEF") = AgL.PubStartDate
            mRateListDetailTableStructure.Rows(mRateListDetailTableStructure.Rows.Count - 1)("Item") = ItemRateListTable.Code
            mRateListDetailTableStructure.Rows(mRateListDetailTableStructure.Rows.Count - 1)("RateType") = ItemRateListTable.Line_RateType
            mRateListDetailTableStructure.Rows(mRateListDetailTableStructure.Rows.Count - 1)("Rate") = ItemRateListTable.Line_Rate
        End If
    End Sub
    Public Structure StructItem
        Dim Code As String
        Dim ManualCode As String
        Dim Description As String
        Dim DisplayName As String
        Dim Specification As String
        Dim ItemGroup As String
        Dim ItemCategory As String
        Dim ItemType As String
        Dim V_Type As String
        Dim PurchaseRate As String
        Dim Rate As String
        Dim MRP As String
        Dim SalesTaxPostingGroup As String
        Dim HSN As String
        Dim Unit As String
        Dim EntryBy As String
        Dim EntryDate As String
        Dim EntryType As String
        Dim EntryStatus As String
        Dim Status As String
        Dim Div_Code As String
        Dim StockYN As String
        Dim IsSystemDefine As String
    End Structure
    Public Structure StructRateList
        Dim Code As String
        Dim WEF As String
        Dim RateType As String
        Dim EntryBy As String
        Dim EntryDate As String
        Dim EntryType As String
        Dim EntryStatus As String
        Dim Status As String
        Dim Div_Code As String

        Dim Line_Sr As String
        Dim Line_WEF As String
        Dim Line_Item As String
        Dim Line_RateType As String
        Dim Line_Rate As String
    End Structure
End Class