Imports System.ComponentModel
Imports System.IO
Imports System.Linq
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain
Public Class FrmSaleInvoiceW_OnlyW
    Dim mSearchCode$ = ""

    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const ColSNo As String = "S.No."
    Public Const Col1SaleInvoiceDocId As String = "SaleInvoiceDocId"
    Public Const Col1PurchInvoiceDocId As String = "PurchInvoiceDocId"
    Public Const Col1Supplier As String = "Supplier"
    Public Const Col1InvoiceNo As String = "Invoice No"
    Public Const Col1InvoiceDate As String = "Invoice Date"
    Public Const Col1ItemGroup As String = "Brand"
    Public Const Col1InvoiceDiscountPer As String = "Invoice Discount @"
    Public Const Col1InvoiceAdditionalDiscountPer As String = "Invoice Additional Discount @"
    Public Const Col1Tax As String = "Tax"
    Public Const Col1DiscountPer As String = "Pcs Less"
    Public Const Col1AdditionalDiscountPer As String = "Additional Discount @"
    Public Const Col1AdditionPer As String = "Addition @"
    Public Const Col1Amount As String = "Purch Amount"
    Public Const Col1AmountWithoutDiscountAndTax As String = "Actual Goods Value Without Discount And Tax"
    Public Const Col1AddedManuallySr As String = "Added Manually Sr"
    Public Const Col1IsThirdPartyBilling As String = "Is Third Party Billing"
    Public Const Col1CommissionAmount As String = "Comm.Amt"
    Public Const Col1AdditionalCommissionAmount As String = "Add.Comm.Amt"
    Public Const Col1GeneratedDocId As String = "GeneratedDocId"
    Public Const Col1GeneratedManualRefNo As String = "GeneratedManualRefNo"


    Public Const Col1MasterSupplier As String = "Master Supplier"
    Public Const Col1WInvoiceNo As String = "W Invoice No"
    Public Const Col1WInvoiceDate As String = "W Invoice Date"
    Public Const Col1WQty As String = "W Qty"
    Public Const Col1WFreight As String = "W Freight"
    Public Const Col1WPacking As String = "W Packing"
    Public Const Col1WAmount As String = "W Amount"
    Public Const Col1WPurchInvoiceDocId As String = "W Purch Invoice DocId"


    Public WithEvents Dgl2 As New AgControls.AgDataGrid
    Public Const Col2SaleInvoiceDocId As String = "SaleInvoiceDocId"
    Public Const Col2Party As String = "Party"
    Public Const Col2InvoiceNo As String = "Invoice No"
    Public Const Col2InvoiceDate As String = "Invoice Date"
    Public Const Col2ItemGroup As String = "Brand"
    Public Const Col2DiscountPer As String = "Pcs Less"
    Public Const Col2AdditionalDiscountPer As String = "Additional Discount @"
    Public Const Col2ExtraDiscountPer As String = "Extra Discount @"
    Public Const Col2AdditionPer As String = "Addition @"
    Public Const Col2Amount As String = "Sale Amount"
    Public Const Col2AmountWithoutTax As String = "Actual Goods Value Without Discount"
    Public Const Col2Tax As String = "Tax"
    Public Const Col2Discount As String = "Discount"
    Public Const Col2ShipToParty As String = "Ship To Party"
    Public Const Col2AddedManuallySr As String = "Added Manually Sr"
    Public Const Col2IsThirdPartyBilling As String = "Is Third Party Billing"
    Public Const Col2GeneratedDocId As String = "GeneratedDocId"

    Public Const Col2MasterParty As String = "Master Party"
    Public Const Col2WSaleOrderDocId As String = "W SaleOrderDocId"
    Public Const Col2WInvoiceNo As String = "W Invoice No"
    Public Const Col2WInvoiceDate As String = "W Invoice Date"
    Public Const Col2WQty As String = "W Qty"
    Public Const Col2WFreight As String = "W Freight"
    Public Const Col2WPacking As String = "W Packing"
    Public Const Col2WDiscount As String = "W Discount"
    Public Const Col2WSaleInvoiceAmount As String = "W Sale Invoice Amount"
    Public Const Col2WSaleInvoiceDocId As String = "W Sale Invoice DocId"

    Public WithEvents Dgl3 As New AgControls.AgDataGrid
    Public Const Col3DrCr As String = "Debit/Credit Note"
    Public Const Col3V_Date As String = "Date"
    Public Const Col3Party As String = "Party Name"
    Public Const Col3LinkedParty As String = "Linked Party Name"
    Public Const Col3ReasonAc As String = "Reason Ac"
    Public Const Col3Amount As String = "Amount"
    Public Const Col3Remark As String = "Remark"

    Dim mFromTransDate As String = "01/Jan/2020"


    Dim mQry As String = ""
    Dim mOrderNCat As String = "SO"
    Public mDbPath As String = ""
    Public mDbEncryption As String = ""
    Dim Connection_Pakka As New SQLite.SQLiteConnection

    Dim DtItem As DataTable
    Dim DtSubGroup As DataTable
    Dim mMode As String = "A"
    Public Sub Ini_Grid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1SaleInvoiceDocId, 100, 0, Col1SaleInvoiceDocId, False, True)
            .AddAgTextColumn(Dgl1, Col1PurchInvoiceDocId, 100, 0, Col1PurchInvoiceDocId, False, True)
            .AddAgTextColumn(Dgl1, Col1Supplier, 100, 0, Col1Supplier, True, False)
            .AddAgTextColumn(Dgl1, Col1InvoiceNo, 80, 0, Col1InvoiceNo, True, True)
            .AddAgDateColumn(Dgl1, Col1InvoiceDate, 80, Col1InvoiceDate, True, True)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 80, 0, Col1ItemGroup, True, False)
            .AddAgNumberColumn(Dgl1, Col1InvoiceDiscountPer, 80, 0, 0, False, Col1InvoiceDiscountPer, False, True)
            .AddAgNumberColumn(Dgl1, Col1InvoiceAdditionalDiscountPer, 80, 0, 0, False, Col1InvoiceAdditionalDiscountPer, False, True)
            .AddAgNumberColumn(Dgl1, Col1Tax, 80, 8, 2, False, Col1Tax, True, True)
            .AddAgNumberColumn(Dgl1, Col1DiscountPer, 80, 2, 2, False, Col1DiscountPer,, False)
            .AddAgNumberColumn(Dgl1, Col1AdditionalDiscountPer, 70, 2, 2, False, Col1AdditionalDiscountPer,, False)
            .AddAgNumberColumn(Dgl1, Col1AdditionPer, 80, 0, 0, False, Col1AdditionPer, False, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 90, 0, 0, False, Col1Amount,, True)
            .AddAgNumberColumn(Dgl1, Col1AmountWithoutDiscountAndTax, 90, 0, 0, False, Col1AmountWithoutDiscountAndTax, True, True)
            .AddAgNumberColumn(Dgl1, Col1AddedManuallySr, 90, 0, 0, False, Col1AddedManuallySr, False, True)
            .AddAgNumberColumn(Dgl1, Col1IsThirdPartyBilling, 90, 0, 0, False, Col1IsThirdPartyBilling, False, True)
            .AddAgNumberColumn(Dgl1, Col1CommissionAmount, 90, 0, 0, False, Col1CommissionAmount,, True)
            .AddAgNumberColumn(Dgl1, Col1AdditionalCommissionAmount, 90, 0, 0, False, Col1AdditionalCommissionAmount,, True)


            .AddAgTextColumn(Dgl1, Col1MasterSupplier, 100, 0, Col1MasterSupplier, False, True)
            .AddAgTextColumn(Dgl1, Col1WInvoiceNo, 90, 0, Col1WInvoiceNo, True, False)
            .AddAgDateColumn(Dgl1, Col1WInvoiceDate, 90, Col1WInvoiceDate, True, False)
            .AddAgNumberColumn(Dgl1, Col1WQty, 90, 0, 0, False, Col1WQty)
            .AddAgNumberColumn(Dgl1, Col1WFreight, 80, 0, 0, False, Col1WFreight)
            .AddAgNumberColumn(Dgl1, Col1WPacking, 80, 0, 0, False, Col1WPacking)
            .AddAgNumberColumn(Dgl1, Col1WAmount, 90, 0, 0, False, Col1WAmount)
            .AddAgTextColumn(Dgl1, Col1WPurchInvoiceDocId, 90, 0, Col1WPurchInvoiceDocId, False, True)
            .AddAgTextColumn(Dgl1, Col1GeneratedDocId, 90, 0, Col1GeneratedDocId, False, True)
            .AddAgTextColumn(Dgl1, Col1GeneratedManualRefNo, 90, 0, Col1GeneratedManualRefNo, False, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 50
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgL.GridDesign(Dgl1)
        Dgl1.AllowUserToAddRows = False
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
        Dgl1.Anchor = AnchorStyles.Top + AnchorStyles.Left + AnchorStyles.Right


        Dgl2.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl2, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl2, Col2SaleInvoiceDocId, 100, 0, Col2SaleInvoiceDocId, False, True)
            .AddAgTextColumn(Dgl2, Col2Party, 100, 0, Col2Party, True, True)
            .AddAgTextColumn(Dgl2, Col2InvoiceNo, 80, 0, Col2InvoiceNo, True, True)
            .AddAgDateColumn(Dgl2, Col2InvoiceDate, 80, Col2InvoiceDate, True, True)
            .AddAgTextColumn(Dgl2, Col2ItemGroup, 90, 0, Col2ItemGroup, True, True)
            .AddAgNumberColumn(Dgl2, Col2DiscountPer, 90, 2, 2, False, Col2DiscountPer,, False)
            .AddAgNumberColumn(Dgl2, Col2AdditionalDiscountPer, 90, 2, 2, False, Col2AdditionalDiscountPer, , False)
            .AddAgNumberColumn(Dgl2, Col2ExtraDiscountPer, 90, 2, 2, False, Col2ExtraDiscountPer,, False)
            .AddAgNumberColumn(Dgl2, Col2AdditionPer, 90, 2, 2, False, Col2AdditionPer,, False)
            .AddAgNumberColumn(Dgl2, Col2Amount, 90, 0, 0, False, Col2Amount,, True)
            .AddAgNumberColumn(Dgl2, Col2AmountWithoutTax, 90, 0, 0, False, Col2AmountWithoutTax,, True)
            .AddAgNumberColumn(Dgl2, Col2Tax, 90, 0, 0, False, Col2Tax,, True)
            .AddAgNumberColumn(Dgl2, Col2Discount, 90, 0, 0, False, Col2Discount, True, True)
            .AddAgTextColumn(Dgl2, Col2ShipToParty, 90, 0, Col2ShipToParty, True, False)
            .AddAgNumberColumn(Dgl2, Col2AddedManuallySr, 90, 0, 0, False, Col2AddedManuallySr, False, True)
            .AddAgNumberColumn(Dgl2, Col2IsThirdPartyBilling, 90, 0, 0, False, Col2IsThirdPartyBilling, False, True)

            .AddAgTextColumn(Dgl2, Col2MasterParty, 100, 0, Col2MasterParty, False, True)
            .AddAgTextColumn(Dgl2, Col2WSaleOrderDocId, 100, 0, Col2WSaleOrderDocId, False, True)
            .AddAgTextColumn(Dgl2, Col2WInvoiceNo, 90, 0, Col2WInvoiceNo, True, False)
            .AddAgDateColumn(Dgl2, Col2WInvoiceDate, 90, Col2WInvoiceDate, True, False)
            .AddAgNumberColumn(Dgl2, Col2WQty, 90, 0, 0, False, Col2WQty, False)
            .AddAgNumberColumn(Dgl2, Col2WFreight, 90, 0, 0, False, Col2WFreight)
            .AddAgNumberColumn(Dgl2, Col2WPacking, 90, 0, 0, False, Col2WPacking)
            .AddAgNumberColumn(Dgl2, Col2WDiscount, 90, 0, 0, False, Col2WDiscount, False, True)
            .AddAgNumberColumn(Dgl2, Col2WSaleInvoiceAmount, 100, 0, 0, False, Col2WSaleInvoiceAmount,, True)
            .AddAgTextColumn(Dgl2, Col2WSaleInvoiceDocId, 90, 0, Col2WSaleInvoiceDocId, False, True)
            .AddAgTextColumn(Dgl2, Col2GeneratedDocId, 100, 0, Col2GeneratedDocId, False, True)
        End With
        AgL.AddAgDataGrid(Dgl2, Pnl2)
        Dgl2.EnableHeadersVisualStyles = False
        Dgl2.ColumnHeadersHeight = 55
        Dgl2.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgL.GridDesign(Dgl2)
        Dgl2.AllowUserToAddRows = False
        Dgl2.AgSkipReadOnlyColumns = True
        Dgl2.AllowUserToOrderColumns = True
        Dgl2.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right
        Dgl2.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgCL.GridSetiingShowXml(Me.Text & Dgl2.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl2, False)
        Dgl2.Anchor = AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom



        Dgl3.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl3, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl3, Col3DrCr, 100, 0, Col3DrCr, True, True)
            .AddAgDateColumn(Dgl3, Col3V_Date, 80, Col3V_Date, True, True)
            .AddAgTextColumn(Dgl3, Col3Party, 300, 0, Col3Party, True, True)
            .AddAgTextColumn(Dgl3, Col3LinkedParty, 300, 0, Col3LinkedParty, False, True)
            .AddAgTextColumn(Dgl3, Col3ReasonAc, 300, 0, Col3ReasonAc, False, True)
            .AddAgNumberColumn(Dgl3, Col3Amount, 200, 0, 0, False, Col3Amount,, True)
            .AddAgTextColumn(Dgl3, Col3Remark, 300, 0, Col3Remark, True, False)
        End With
        AgL.AddAgDataGrid(Dgl3, Pnl3)
        Dgl3.EnableHeadersVisualStyles = False
        Dgl3.ColumnHeadersHeight = 40
        Dgl3.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgL.GridDesign(Dgl3)
        Dgl3.AllowUserToAddRows = False
        Dgl3.AgSkipReadOnlyColumns = True
        Dgl3.AllowUserToOrderColumns = True
        Dgl3.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right
        Dgl3.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgCL.GridSetiingShowXml(Me.Text & Dgl3.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl3, False)
        Dgl3.Anchor = AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom
        Dgl3.Visible = False
    End Sub
    Private Sub FrmReconcile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mDbPath = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "ActualDBPath", "")
        mDbEncryption = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "Encryption", "")
        If mDbEncryption = "N" Then
            Connection_Pakka.ConnectionString = "DataSource=" & mDbPath & ";Version=3;"
        Else
            Connection_Pakka.ConnectionString = "DataSource=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
        End If
        Connection_Pakka.Open()

        FIniList()

        Ini_Grid()
        TxtOrderNo.Focus()
        TxtPartyName.Enabled = False
        BtnApprove.Visible = False
        Me.WindowState = FormWindowState.Maximized
    End Sub
    Private Sub FIniList()
        mQry = " Select * From SubGroup "
        DtSubGroup = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Select Code, OMSId From Item "
        DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub
    Private Sub FSyncSaleOrderDocuments()
        mQry = "Select DocId, OMSId From SaleInvoice Where V_Type = 'SO' And OMSId Is Not Null"
        Dim DtSaleOrder As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        For I As Integer = 0 To DtSaleOrder.Rows.Count - 1
            CopyAttachments(AgL.XNull(DtSaleOrder.Rows(I)("OMSId")), AgL.XNull(DtSaleOrder.Rows(I)("DocId")))
        Next
    End Sub
    Private Sub FSyncPurchInvoiceDocuments()
        mQry = "Select DocId, OMSId From PurchInvoice Where OMSId Is Not Null "
        Dim DtPurchInvoice As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        For I As Integer = 0 To DtPurchInvoice.Rows.Count - 1
            CopyAttachments(AgL.XNull(DtPurchInvoice.Rows(I)("OMSId")), AgL.XNull(DtPurchInvoice.Rows(I)("DocId")))
        Next
    End Sub
    Private Sub Dgl1_ColumnDisplayIndexChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles Dgl1.ColumnDisplayIndexChanged
        Try
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Dgl1_ColumnWidthChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles Dgl1.ColumnWidthChanged
        Try
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Dgl2_ColumnDisplayIndexChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles Dgl2.ColumnDisplayIndexChanged
        Try
            AgCL.GridSetiingWriteXml(Me.Text & Dgl2.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl2)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Dgl2_ColumnWidthChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles Dgl2.ColumnWidthChanged
        Try
            AgCL.GridSetiingWriteXml(Me.Text & Dgl2.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl2)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub
    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtOrderNo.Validating
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Dim mDiscountQry As String = ""

        Try
            Select Case sender.NAME
                Case TxtOrderNo.Name
                    Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                    Dgl2.RowCount = 1 : Dgl2.Rows.Clear()
                    Dgl3.RowCount = 1 : Dgl3.Rows.Clear()

                    mQry = "Select H.SaleToParty, Sg.Name As SaleToPartyName, H.UploadDate
                            From SaleInvoice H  With (NoLock)
                            LEFT JOIN SubGroup Sg On H.SaleToParty = Sg.SubCode
                            Where H.DocId = '" & TxtOrderNo.Tag & "'"
                    DtTemp = AgL.FillData(mQry, Connection_Pakka).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        If AgL.XNull(DtTemp.Rows(0)("UploadDate")) = "" Then
                            MsgBox("Sale Order is not synced in Kachha.", MsgBoxStyle.Information)
                            Exit Sub
                        Else
                            TxtPartyName.Tag = AgL.XNull(DtTemp.Rows(0)("SaleToParty"))
                            TxtPartyName.Text = AgL.XNull(DtTemp.Rows(0)("SaleToPartyName"))

                            TxtSaleOrderDocId_W.Text = AgL.XNull(AgL.Dman_Execute("Select DocId 
                                    From SaleInvoice Where OMSId = '" & TxtOrderNo.Tag & "'", AgL.GCn).ExecuteScalar())

                            Dim mExistedInvoiceNo As String = AgL.XNull(AgL.Dman_Execute(" Select H.ManualRefNo As InvoiceNo
                                    From SaleInvoice H 
                                    LEFT JOIN SaleInvoiceDetail L On H.DocId = L.DocId
                                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                                    Where L.SaleInvoice = '" & TxtSaleOrderDocId_W.Text & "' 
                                    And Vt.NCat = '" & Ncat.SaleInvoice & "'", AgL.GCn).ExecuteScalar())

                            If mExistedInvoiceNo <> "" Then
                                If MsgBox("Invoice No. " & mExistedInvoiceNo & " already exist for " & TxtOrderNo.Text & ". Do you want to continue ? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If

                    Dim bSaleInvoiceDocIdStr As String = ""
                    Dim WSaleInvoice As String = ""
                    Dim WSaleInvoiceNo As Int32 = 0

                    mQry = "Select Si.DocId As SaleInvoiceDocId, Sg.Name As SaleToPartyName, Max(Sg1.Name) As BillToPartyName, Max(Sg2.Name) As ShipToPartyName,
                            Si.DocId As InvoiceDocId, Si.ManualRefNo As invoiceNo, Si.V_Type As InvoiceV_Type, Si.V_Date As InvoiceDate, 
                            Ig.Code As ItemGroup, Ig.Description As ItemGroupDesc,
                            Max(Si.SaleToParty) As SaleToParty, Max(Si.BillToParty) As BillToParty, 
                            Max(H.ShipToParty) As ShipToParty, Max(Si.Site_Code) As Site_Code, 
                            Max(Si.Div_Code) As Div_Code, Max(Si.Net_Amount) As Amount,
                            Max(H.V_Type) As OrderV_Type, Max(H.ManualRefNo) As OrderManualRefNo,
                            Sum(Sil.DiscountAmount+Sil.AdditionalDiscountAmount-Sil.AdditionAmount) As TotalDiscount,
                            Sum(Sil.Taxable_Amount) As AmountWithoutTax,
                            IfNull(Sum(Sil.Tax1),0) + IfNull(Sum(Sil.Tax2),0) + IfNull(Sum(Sil.Tax3),0) + IfNull(Sum(Sil.Tax4),0) + IfNull(Sum(Sil.Tax5),0) As Tax,
                            L.DocId As SaleOrder, L.Sr As SaleOrderSr, Max(Si.UploadDate) As UploadDate
                            From (Select * From SaleInvoice Where IfNull(ReferenceDocId,DocId) = '" & TxtOrderNo.Tag & "') H 
                            LEFT JOIN SaleInvoiceDetail L ON H.DocId = L.DocId
                            LEFT JOIN SaleInvoiceDetail Sil On L.Docid = Sil.SaleInvoice And L.Sr = Sil.SaleInvoiceSr
                            LEFT JOIN SaleInvoice Si ON Sil.DocID = Si.DocId
                            LEFT JOIN SubGroup Sg ON Si.SaleToParty = Sg.SubCode 
                            LEFT JOIN SubGroup Sg1 ON Si.BillToParty = Sg1.SubCode
                            LEFT JOIN SubGroup Sg2 On H.ShipToParty = Sg2.SubCode
                            LEFT JOIN Item I On Sil.Item = I.Code
                            LEFT JOIN ItemGroup Ig On I.ItemGroup = Ig.Code
                            Where Sil.DocId Is Not Null
                            And Si.V_Type = 'SI'
                            And I.ItemType Not In ('" & ItemTypeCode.ServiceProduct & "')
                            Group By Si.ManualRefNo, Si.V_Date, Ig.Code, Ig.Description "

                    'And I.Description Not In ('" & ItemCode.Packing & "','" & ItemCode.Freight & "')
                    DtTemp = AgL.FillData(mQry, Connection_Pakka).Tables(0)
                    Dgl2.RowCount = 1 : Dgl2.Rows.Clear()
                    If DtTemp.Rows.Count > 0 Then
                        For I = 0 To DtTemp.Rows.Count - 1
                            If AgL.VNull(AgL.Dman_Execute("SELECT Count(*) As Cnt FROM SaleInvoice 
                                    WHERE IfNull(AmsDocId,'') = '" & AgL.XNull(DtTemp.Rows(I)("SaleInvoiceDocId")) & "'", AgL.GCn).ExecuteScalar()) = 0 Then

                                'If AgL.XNull(DtTemp.Rows(I)("UploadDate")) = "" Then
                                '    MsgBox("Sale Invoice is not synced in Kachha.", MsgBoxStyle.Information)
                                '    BtnOk.Enabled = False
                                '    Exit Sub
                                'Else
                                '    BtnOk.Enabled = True
                                'End If


                                Dgl2.Rows.Add()
                                Dgl2.Item(ColSNo, Dgl2.Rows.Count - 1).Value = Dgl2.Rows.Count
                                Dgl2.Item(Col2AddedManuallySr, Dgl2.Rows.Count - 1).Value = 0
                                Dgl2.Item(Col2SaleInvoiceDocId, Dgl2.Rows.Count - 1).Value = AgL.XNull(DtTemp.Rows(I)("SaleInvoiceDocId"))
                                Dgl2.Item(Col2Party, Dgl2.Rows.Count - 1).Tag = FGetSubCodeFromOMSId(AgL.XNull(DtTemp.Rows(I)("SaleToParty")))
                                Dgl2.Item(Col2Party, Dgl2.Rows.Count - 1).Value = AgL.XNull(DtTemp.Rows(I)("SaleToPartyName"))
                                Dgl2.Item(Col2MasterParty, Dgl2.Rows.Count - 1).Tag = FGetSubCodeFromOMSId(AgL.XNull(DtTemp.Rows(I)("BillToParty")))
                                Dgl2.Item(Col2MasterParty, Dgl2.Rows.Count - 1).Value = AgL.XNull(DtTemp.Rows(I)("BillToPartyName"))
                                Dgl2.Item(Col2InvoiceNo, Dgl2.Rows.Count - 1).Tag = AgL.XNull(DtTemp.Rows(I)("InvoiceDocId"))
                                Dgl2.Item(Col2InvoiceNo, Dgl2.Rows.Count - 1).Value = AgL.XNull(DtTemp.Rows(I)("InvoiceNo"))
                                Dgl2.Item(Col2InvoiceDate, Dgl2.Rows.Count - 1).Value = AgL.XNull(DtTemp.Rows(I)("InvoiceDate"))

                                'Dgl2.Item(Col2WInvoiceDate, Dgl2.Rows.Count - 1).Value = AgL.PubLoginDate
                                WSaleInvoice = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "SaleInvoice", "WSI", AgL.PubLoginDate, AgL.PubDivCode, AgL.PubSiteCode, AgTemplate.ClsMain.ManualRefType.Max)
                                WSaleInvoiceNo = Convert.ToInt32(WSaleInvoice) + Dgl2.Rows.Count - 1
                                Dgl2.Item(Col2WInvoiceNo, Dgl2.Rows.Count - 1).Value = WSaleInvoiceNo.ToString()


                                Dgl2.Item(Col2ShipToParty, Dgl2.Rows.Count - 1).Tag = FGetSubCodeFromOMSId(AgL.XNull(DtTemp.Rows(I)("ShipToParty")))
                                Dgl2.Item(Col2ShipToParty, Dgl2.Rows.Count - 1).Value = AgL.XNull(DtTemp.Rows(I)("ShipToPartyName"))

                                Dgl2.Item(Col2ItemGroup, Dgl2.Rows.Count - 1).Tag = FGetItemCodeFromOMSId(AgL.XNull(DtTemp.Rows(I)("ItemGroup")))
                                Dgl2.Item(Col2ItemGroup, Dgl2.Rows.Count - 1).Value = AgL.XNull(DtTemp.Rows(I)("ItemGroupDesc"))
                                Dgl2.Item(Col2Amount, Dgl2.Rows.Count - 1).Value = AgL.XNull(DtTemp.Rows(I)("Amount"))
                                Dgl2.Item(Col2Discount, Dgl2.Rows.Count - 1).Value = AgL.XNull(DtTemp.Rows(I)("TotalDiscount"))
                                Dgl2.Item(Col2AmountWithoutTax, Dgl2.Rows.Count - 1).Value = AgL.XNull(DtTemp.Rows(I)("AmountWithoutTax"))
                                Dgl2.Item(Col2Tax, Dgl2.Rows.Count - 1).Value = AgL.XNull(DtTemp.Rows(I)("Tax"))


                                Dim mServiceProductTaxSale As Double = 0
                                mQry = " Select IfNull(Sum(L.Tax1),0) + IfNull(Sum(L.Tax2),0) + IfNull(Sum(L.Tax3),0) + IfNull(Sum(L.Tax4),0) + IfNull(Sum(L.Tax5),0) As ServiceProductTax
                                    From SaleInvoiceDetail L
                                    LEFT JOIN Item I On L.Item = I.Code
                                    Where L.DocId = '" & Dgl2.Item(Col2SaleInvoiceDocId, Dgl2.Rows.Count - 1).Value & "'
                                    And I.ItemType In ('" & ItemTypeCode.ServiceProduct & "') "
                                If Dgl2.Rows.Count - 1 = 0 Then
                                    mServiceProductTaxSale = AgL.Dman_Execute(mQry, Connection_Pakka).ExecuteScalar()
                                ElseIf Dgl2.Item(Col2SaleInvoiceDocId, Dgl2.Rows.Count - 1).Value <> Dgl2.Item(Col2SaleInvoiceDocId, Dgl2.Rows.Count - 2).Value Then
                                    mServiceProductTaxSale = AgL.Dman_Execute(mQry, Connection_Pakka).ExecuteScalar()
                                End If
                                Dgl2.Item(Col2Tax, Dgl2.Rows.Count - 1).Value = AgL.VNull(DtTemp.Rows(I)("Tax")) + mServiceProductTaxSale


                                If bSaleInvoiceDocIdStr <> "" Then bSaleInvoiceDocIdStr = bSaleInvoiceDocIdStr + ","
                                bSaleInvoiceDocIdStr = bSaleInvoiceDocIdStr + Dgl2.Item(Col2InvoiceNo, Dgl2.Rows.Count - 1).Tag

                                'FCopyTransportDetail(Dgl2.Item(Col2InvoiceNo, Dgl2.Rows.Count - 1).Tag)

                                Dim DTDiscounts As DataTable = FGetDiscountRates(AgL.XNull(DtTemp.Rows(I)("BillToParty")),
                                                AgL.XNull(DtTemp.Rows(I)("Site_Code")),
                                                AgL.XNull(DtTemp.Rows(I)("Div_Code")),
                                                AgL.XNull(DtTemp.Rows(I)("ItemGroup")))
                                If DTDiscounts.Rows.Count > 0 Then
                                    Dgl2.Item(Col2DiscountPer, Dgl2.Rows.Count - 1).Value = AgL.VNull(DTDiscounts.Rows(0)("Default_DiscountPerSale"))
                                    Dgl2.Item(Col2AdditionalDiscountPer, Dgl2.Rows.Count - 1).Value = AgL.VNull(DTDiscounts.Rows(0)("Default_AdditionalDiscountPerSale"))
                                    Dgl2.Item(Col2AdditionPer, Dgl2.Rows.Count - 1).Value = AgL.VNull(DTDiscounts.Rows(0)("Default_AdditionPerSale"))
                                End If

                                'If Discount found in Item Group Person Table means for this 
                                'specific brand and person combination have a different Discount Setting.
                                mQry = "Select * 
                                        from ItemGroupPerson With (NoLock) 
                                        Where ItemGroup  = '" & AgL.XNull(DtTemp.Rows(I)("ItemGroup")) & "'
                                        And Person  = '" & AgL.XNull(DtTemp.Rows(I)("BillToParty")) & "'
                                       "
                                DTDiscounts = AgL.FillData(mQry, Connection_Pakka).tables(0)
                                If DTDiscounts.Rows.Count > 0 Then
                                    Dgl2.Item(Col2DiscountPer, Dgl2.Rows.Count - 1).Value = AgL.VNull(DTDiscounts.Rows(0)("DiscountPer"))
                                    Dgl2.Item(Col2AdditionalDiscountPer, Dgl2.Rows.Count - 1).Value = AgL.VNull(DTDiscounts.Rows(0)("AdditionalDiscountPer"))
                                    Dgl2.Item(Col2AdditionPer, Dgl2.Rows.Count - 1).Value = AgL.VNull(DTDiscounts.Rows(0)("AdditionPer"))
                                End If

                                mQry = "Select * 
                                    from PersonExtraDiscount With (NoLock) 
                                    Where ItemGroup  = '" & AgL.XNull(DtTemp.Rows(I)("ItemGroup")) & "'
                                    And Person  = '" & AgL.XNull(DtTemp.Rows(I)("BillToParty")) & "' "
                                Dim DTExtraDiscounts As DataTable = AgL.FillData(mQry, Connection_Pakka).tables(0)
                                If DTExtraDiscounts.Rows.Count > 0 Then
                                    Dgl2.Item(Col2ExtraDiscountPer, Dgl2.Rows.Count - 1).Value = AgL.VNull(DTExtraDiscounts.Rows(0)("ExtraDiscountPer"))
                                End If


                                If AgL.XNull(DtTemp.Rows(I)("OrderV_Type")) <> "" And
                                AgL.XNull(DtTemp.Rows(I)("OrderManualRefNo")) <> "" Then
                                    mQry = " Select H.DocId
                                            From SaleInvoice H
                                            Where OMSId = '" & AgL.XNull(DtTemp.Rows(I)("SaleOrder")) & "'"
                                    Dgl2.Item(Col2WSaleOrderDocId, Dgl2.Rows.Count - 1).Value = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
                                End If
                            End If
                        Next I

                        mQry = "Select H.GenDocId As SaleInvoiceDocId, H.DocId As PurchInvoiceDocId, Max(H.Vendor) As Supplier, Supp.Name As SupplierName, 
                            Max(H.VendorDocNo) As InvoiceNo, H.V_Date As InvoiceDate, 
                            Ig.Code as ItemGroup, Ig.Description As ItemGroupDesc,
                            Max(H.Net_Amount) As Amount, 
                            Max(H.BillToParty) As BillToParty, Max(H.Site_Code) As Site_Code, 
                            Max(H.Div_Code) As Div_Code, Min(L.DiscountPer) As InvoiceDiscountPer,
                            Min(L.AdditionalDiscountPer) As InvoiceAdditionalDiscountPer,
                            IfNull(Sum(L.Tax1),0) + IfNull(Sum(L.Tax2),0) + IfNull(Sum(L.Tax3),0) + IfNull(Sum(L.Tax4),0) + IfNull(Sum(L.Tax5),0) As Tax,
                            Sum(L.Rate * L.Qty) As AmountWithoutTaxAndDiscount,
                            Max(Sg1.Name) As BillToPartyName, Max(H.UploadDate) As UploadDate
                            From PurchInvoice H  With (NoLock)
                            LEFT JOIN PurchInvoiceDetail L On H.DocId = L.DocId
                            LEFT JOIN Item I On L.Item = I.Code
                            LEFT JOIN ItemGroup Ig ON I.ItemGroup = Ig.Code
                            LEFT JOIN SubGroup Supp On H.Vendor = Supp.SubCode
                            LEFT JOIN SubGroup Sg1 ON H.BillToParty = Sg1.SubCode
                            Where H.GenDocId In ('" & bSaleInvoiceDocIdStr.Replace(",", "','") & "')
                            And I.ItemType Not In ('" & ItemTypeCode.ServiceProduct & "')
                            Group By H.GenDocId, H.DocId, Supp.Name, H.ManualRefNo, H.V_Date, Ig.Code, Ig.Description "

                        'And I.Description Not In ('" & ItemCode.Packing & "','" & ItemCode.Freight & "')
                        DtTemp = AgL.FillData(mQry, Connection_Pakka).Tables(0)
                        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                        If DtTemp.Rows.Count > 0 Then
                            For I = 0 To DtTemp.Rows.Count - 1
                                'If AgL.XNull(DtTemp.Rows(I)("UploadDate")) = "" Then
                                '    MsgBox("Purchase Invoice is not synced in Kachha.", MsgBoxStyle.Information)
                                '    BtnOk.Enabled = False
                                '    Exit Sub
                                'Else
                                '    BtnOk.Enabled = True
                                'End If

                                Dgl1.Rows.Add()
                                Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                                Dgl1.Item(Col1AddedManuallySr, Dgl1.Rows.Count - 1).Value = 0
                                Dgl1.Item(Col1SaleInvoiceDocId, I).Value = AgL.XNull(DtTemp.Rows(I)("SaleInvoiceDocId"))
                                Dgl1.Item(Col1PurchInvoiceDocId, I).Value = AgL.XNull(DtTemp.Rows(I)("PurchInvoiceDocId"))
                                Dgl1.Item(Col1Supplier, I).Tag = FGetSubCodeFromOMSId(AgL.XNull(DtTemp.Rows(I)("Supplier")))
                                Dgl1.Item(Col1Supplier, I).Value = AgL.XNull(DtTemp.Rows(I)("SupplierName"))
                                Dgl1.Item(Col1MasterSupplier, I).Tag = FGetSubCodeFromOMSId(AgL.XNull(DtTemp.Rows(I)("BillToParty")))
                                Dgl1.Item(Col1MasterSupplier, I).Value = AgL.XNull(DtTemp.Rows(I)("BillToPartyName"))
                                Dgl1.Item(Col1InvoiceNo, I).Value = AgL.XNull(DtTemp.Rows(I)("InvoiceNo"))
                                Dgl1.Item(Col1InvoiceDate, I).Value = AgL.XNull(DtTemp.Rows(I)("InvoiceDate"))
                                Dgl1.Item(Col1ItemGroup, I).Tag = FGetItemCodeFromOMSId(AgL.XNull(DtTemp.Rows(I)("ItemGroup")))
                                Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(DtTemp.Rows(I)("ItemGroupDesc"))
                                Dgl1.Item(Col1InvoiceDiscountPer, I).Value = AgL.VNull(DtTemp.Rows(I)("InvoiceDiscountPer"))
                                Dgl1.Item(Col1InvoiceAdditionalDiscountPer, I).Value = AgL.VNull(DtTemp.Rows(I)("InvoiceAdditionalDiscountPer"))
                                Dgl1.Item(Col1Tax, I).Value = AgL.VNull(DtTemp.Rows(I)("Tax"))
                                Dgl1.Item(Col1Amount, I).Value = AgL.VNull(DtTemp.Rows(I)("Amount"))
                                Dgl1.Item(Col1AmountWithoutDiscountAndTax, I).Value = AgL.VNull(DtTemp.Rows(I)("AmountWithoutTaxAndDiscount"))


                                Dim mServiceProductTaxPurch As Double = 0
                                mQry = " Select IfNull(Sum(L.Tax1),0) + IfNull(Sum(L.Tax2),0) + IfNull(Sum(L.Tax3),0) + IfNull(Sum(L.Tax4),0) + IfNull(Sum(L.Tax5),0) As ServiceProductTax
                                    From PurchInvoiceDetail L
                                    LEFT JOIN Item I On L.Item = I.Code
                                    Where L.DocId = '" & Dgl1.Item(Col1PurchInvoiceDocId, Dgl1.Rows.Count - 1).Value & "'
                                    And I.ItemType In ('" & ItemTypeCode.ServiceProduct & "') "
                                If Dgl1.Rows.Count - 1 = 0 Then
                                    mServiceProductTaxPurch = AgL.Dman_Execute(mQry, Connection_Pakka).ExecuteScalar()
                                ElseIf Dgl1.Item(Col1PurchInvoiceDocId, Dgl1.Rows.Count - 1).Value <> Dgl1.Item(Col1PurchInvoiceDocId, Dgl1.Rows.Count - 2).Value Then
                                    mServiceProductTaxPurch = AgL.Dman_Execute(mQry, Connection_Pakka).ExecuteScalar()
                                End If
                                Dgl1.Item(Col1Tax, Dgl1.Rows.Count - 1).Value = AgL.VNull(DtTemp.Rows(I)("Tax")) + mServiceProductTaxPurch



                                mQry = "Select IG.Default_DiscountPerPurchase, IG.Default_AdditionalDiscountPerPurchase,
                                    0 As Default_AdditionPerPurchase
                                    From ItemGroup IG  
                                    Where IG.Code ='" & AgL.XNull(DtTemp.Rows(I)("ItemGroup")) & "'"
                                Dim DTDiscounts As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)
                                If DTDiscounts.Rows.Count > 0 Then
                                    Dgl1.Item(Col1DiscountPer, I).Value = AgL.VNull(DTDiscounts.Rows(0)("Default_DiscountPerPurchase"))
                                    Dgl1.Item(Col1AdditionalDiscountPer, I).Value = AgL.VNull(DTDiscounts.Rows(0)("Default_AdditionalDiscountPerPurchase"))
                                    Dgl1.Item(Col1AdditionPer, I).Value = AgL.VNull(DTDiscounts.Rows(0)("Default_AdditionPerPurchase"))
                                End If
                            Next I
                        End If
                    Else
                        mQry = " Select H.DocId As SaleOrderDocId, H.V_Type As OrderV_Type, H.ManualRefNo As OrderManualRefNo, 
                                H.SaleToParty, H.BillToParty, H.Site_Code, H.Div_Code, L.Item As ItemGroup,
                                I.Description As ItemGroupDesc, Sg.Name As SaleToPartyName,
                                Supp.SubCode As MasterSupplier, Supp.Name As MasterSupplierName, CSupp.SubCode As Supplier, CSupp.Name As SupplierName, Sg1.Name As BillToPartyName
                                From SaleInvoice H 
                                LEFT JOIN SaleInvoiceDetail L ON H.DocId = L.DocId
                                LEFT JOIN Item I ON L.Item = I.Code
                                LEFT JOIN SubGroup Sg On H.SaleToParty = Sg.SubCode
                                LEFT JOIN SubGroup Supp On I.DefaultSupplier = Supp.SubCode
                                LEFT JOIN SubGroup Sg1 ON H.BillToParty = Sg1.SubCode
                                LEFT JOIN SubGroup Sg2 On Supp.Parent = Sg2.SubCode
                                Left Join 
                                (
                                 select Parent, Max(Code) AS ChildSupp  from ViewHelpSubGroup CS Group By Parent
                                ) CS On CS.Parent =I.DefaultSupplier
                                LEFT JOIN SubGroup CSupp On CS.ChildSupp = CSupp.SubCode
                                Where IfNull(H.ReferenceDocId,H.DocId)  = '" & TxtOrderNo.Tag & "'"
                        Dim DtSaleOrderDetail As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)
                        Dgl2.RowCount = 1 : Dgl2.Rows.Clear()
                        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                        If DtSaleOrderDetail.Rows.Count = 0 Then
                            mQry = " Select Rh.ManualRefNo As ParentOrderNo
                                    From SaleOrder H
                                    LEFT JOIN SaleOrder Rh On H.ReferenceDocId = Rh.DocId
                                    Where H.DocId = '" & TxtOrderNo.Tag & "'
                                    And H.ReferenceDocId Is Not Null "
                            Dim DtParentOrder As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)
                            If DtParentOrder.Rows.Count > 0 Then
                                If AgL.XNull(DtParentOrder.Rows(0)("ParentOrderNo")) <> "" Then
                                    MsgBox("There is a reference order exist with number " & AgL.XNull(DtParentOrder.Rows(0)("ParentOrderNo")), MsgBoxStyle.Information)
                                    Exit Sub
                                End If
                            End If
                        End If


                        If DtSaleOrderDetail.Rows.Count > 0 Then
                            For I = 0 To DtSaleOrderDetail.Rows.Count - 1
                                Dgl2.Rows.Add()
                                Dgl2.Item(ColSNo, I).Value = Dgl2.Rows.Count
                                Dgl2.Item(Col2AddedManuallySr, I).Value = 0
                                Dgl2.Item(Col2SaleInvoiceDocId, I).Value = ""
                                Dgl2.Item(Col2Party, I).Tag = FGetSubCodeFromOMSId(AgL.XNull(DtSaleOrderDetail.Rows(I)("SaleToParty")))
                                Dgl2.Item(Col2Party, I).Value = AgL.XNull(DtSaleOrderDetail.Rows(I)("SaleToPartyName"))
                                Dgl2.Item(Col2MasterParty, I).Tag = FGetSubCodeFromOMSId(AgL.XNull(DtSaleOrderDetail.Rows(I)("BillToParty")))
                                Dgl2.Item(Col2MasterParty, I).Value = AgL.XNull(DtSaleOrderDetail.Rows(I)("BillToPartyName"))
                                Dgl2.Item(Col2InvoiceNo, I).Tag = ""
                                Dgl2.Item(Col2InvoiceNo, I).Value = ""
                                Dgl2.Item(Col2InvoiceDate, I).Value = ""

                                'Dgl2.Item(Col2WInvoiceDate, I).Value = AgL.PubLoginDate
                                WSaleInvoice = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "SaleInvoice", "WSI", AgL.PubLoginDate, AgL.PubDivCode, AgL.PubSiteCode, AgTemplate.ClsMain.ManualRefType.Max)
                                WSaleInvoiceNo = Convert.ToInt32(WSaleInvoice) + Dgl2.Rows.Count - 1
                                Dgl2.Item(Col2WInvoiceNo, I).Value = WSaleInvoiceNo.ToString()

                                Dgl2.Item(Col2ItemGroup, I).Tag = FGetItemCodeFromOMSId(AgL.XNull(DtSaleOrderDetail.Rows(I)("ItemGroup")))
                                Dgl2.Item(Col2ItemGroup, I).Value = AgL.XNull(DtSaleOrderDetail.Rows(I)("ItemGroupDesc"))
                                Dgl2.Item(Col2Amount, I).Value = 0
                                Dgl2.Item(Col2IsThirdPartyBilling, I).Value = 1
                                'FCopyTransportDetail(TxtOrderNo.Tag)

                                Dim DTSaleDiscounts As DataTable = FGetDiscountRates(AgL.XNull(DtSaleOrderDetail.Rows(I)("BillToParty")),
                                    AgL.XNull(DtSaleOrderDetail.Rows(I)("Site_Code")),
                                    AgL.XNull(DtSaleOrderDetail.Rows(I)("Div_Code")),
                                    AgL.XNull(DtSaleOrderDetail.Rows(I)("ItemGroup")))
                                If DTSaleDiscounts.Rows.Count > 0 Then
                                    Dgl2.Item(Col2DiscountPer, I).Value = AgL.VNull(DTSaleDiscounts.Rows(0)("Default_DiscountPerSale"))
                                    Dgl2.Item(Col2AdditionalDiscountPer, I).Value = AgL.VNull(DTSaleDiscounts.Rows(0)("Default_AdditionalDiscountPerSale"))
                                    Dgl2.Item(Col2AdditionPer, I).Value = AgL.VNull(DTSaleDiscounts.Rows(0)("Default_AdditionPerSale"))
                                End If

                                mQry = "Select * 
                                    from PersonExtraDiscount With (NoLock) 
                                    Where ItemGroup  = '" & AgL.XNull(DtSaleOrderDetail.Rows(I)("ItemGroup")) & "'
                                    And Person  = '" & AgL.XNull(DtSaleOrderDetail.Rows(I)("BillToParty")) & "' "
                                Dim DTExtraDiscounts As DataTable = AgL.FillData(mQry, Connection_Pakka).tables(0)
                                If DTExtraDiscounts.Rows.Count > 0 Then
                                    Dgl2.Item(Col2ExtraDiscountPer, Dgl2.Rows.Count - 1).Value = AgL.VNull(DTExtraDiscounts.Rows(0)("ExtraDiscountPer"))
                                End If

                                If AgL.XNull(DtSaleOrderDetail.Rows(I)("OrderV_Type")) <> "" And
                                        AgL.XNull(DtSaleOrderDetail.Rows(I)("OrderManualRefNo")) <> "" Then
                                    'mQry = " Select DocId 
                                    '        From SaleInvoice 
                                    '        Where V_Type = '" & AgL.XNull(DtSaleOrderDetail.Rows(I)("OrderV_Type")) & "'
                                    '        And ManualRefNo = '" & AgL.XNull(DtSaleOrderDetail.Rows(I)("OrderManualRefNo")) & "'"
                                    mQry = " Select H.DocId
                                        From SaleInvoice H
                                        Where OMSId = '" & AgL.XNull(DtSaleOrderDetail.Rows(I)("SaleOrderDocId")) & "'"
                                    Dgl2.Item(Col2WSaleOrderDocId, I).Value = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
                                End If

                                'For Purchase Data


                                Dgl1.Rows.Add()
                                Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                                Dgl1.Item(Col1AddedManuallySr, I).Value = 0
                                Dgl1.Item(Col1SaleInvoiceDocId, I).Value = ""
                                Dgl1.Item(Col1PurchInvoiceDocId, I).Value = ""
                                Dgl1.Item(Col1Supplier, I).Tag = FGetSubCodeFromOMSId(AgL.XNull(DtSaleOrderDetail.Rows(I)("Supplier")))
                                Dgl1.Item(Col1Supplier, I).Value = AgL.XNull(DtSaleOrderDetail.Rows(I)("SupplierName"))
                                Dgl1.Item(Col1MasterSupplier, I).Tag = FGetSubCodeFromOMSId(AgL.XNull(DtSaleOrderDetail.Rows(I)("MasterSupplier")))
                                Dgl1.Item(Col1MasterSupplier, I).Value = AgL.XNull(DtSaleOrderDetail.Rows(I)("MasterSupplierName"))
                                Dgl1.Item(Col1InvoiceNo, I).Value = ""
                                Dgl1.Item(Col1InvoiceDate, I).Value = ""
                                Dgl1.Item(Col1ItemGroup, I).Tag = FGetItemCodeFromOMSId(AgL.XNull(DtSaleOrderDetail.Rows(I)("ItemGroup")))
                                Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(DtSaleOrderDetail.Rows(I)("ItemGroupDesc"))
                                Dgl1.Item(Col1InvoiceDiscountPer, I).Value = 0
                                Dgl1.Item(Col1InvoiceAdditionalDiscountPer, I).Value = 0
                                Dgl1.Item(Col1Amount, I).Value = 0
                                Dgl1.Item(Col1IsThirdPartyBilling, I).Value = 1

                                mQry = "Select IG.Default_DiscountPerPurchase, IG.Default_AdditionalDiscountPerPurchase,
                                    0 As Default_AdditionPerPurchase
                                    From ItemGroup IG  
                                    Where IG.Code ='" & AgL.XNull(DtSaleOrderDetail.Rows(I)("ItemGroup")) & "'"
                                Dim DTPurchaseDiscounts As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)
                                If DTPurchaseDiscounts.Rows.Count > 0 Then
                                    Dgl1.Item(Col1DiscountPer, I).Value = AgL.VNull(DTPurchaseDiscounts.Rows(0)("Default_DiscountPerPurchase"))
                                    Dgl1.Item(Col1AdditionalDiscountPer, I).Value = AgL.VNull(DTPurchaseDiscounts.Rows(0)("Default_AdditionalDiscountPerPurchase"))
                                    Dgl1.Item(Col1AdditionPer, I).Value = AgL.VNull(DTPurchaseDiscounts.Rows(0)("Default_AdditionPerPurchase"))
                                End If
                            Next
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function FGetDiscountRates(SubCode As String, Site_Code As String, Div_Code As String, ItemGroup As String) As DataTable
        mQry = "Select IfNull(Max(DiscountPer),0) As Default_DiscountPerSale,
                IfNull(Max(AdditionalDiscountPer),0) As Default_AdditionalDiscountPerSale,
                IfNull(Max(AdditionPer),0) As Default_AdditionPerSale
                From (
                    Select * From SubgroupSiteDivisionDetail 
                    Where SubCode = '" & SubCode & "'
                    And Site_Code = '" & Site_Code & "'
                    And Div_Code = '" & Div_Code & "') As VSubGroup
                LEFT JOIN ItemGroupRateType H  With (NoLock) On VSubGroup.RateType = H.RateType
                Where Code = '" & ItemGroup & "' "
        Dim DTDiscounts As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)
        Return DTDiscounts
    End Function
    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If Me.ActiveControl IsNot Nothing Then
            If TxtOrderNo.Focused = True And e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")

            If e.KeyCode = (Keys.S And e.Control) Then
                FProcSave()
            End If
        End If
    End Sub
    Private Sub TxtBuyer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtOrderNo.KeyDown, TxtTag.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then Exit Sub
            Select Case sender.name
                Case TxtTag.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataset Is Nothing Then
                            mQry = "Select Code, Description From Tag Where V_Type ='WSI' Order By Description"
                            TxtTag.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtOrderNo.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataset Is Nothing Then

                            Dim Connection_Pakka_Temp As New SQLite.SQLiteConnection
                            If mDbEncryption = "N" Then
                                Connection_Pakka_Temp.ConnectionString = "DataSource=" & mDbPath & ";Version=3;"
                            Else
                                Connection_Pakka_Temp.ConnectionString = "DataSource=" & mDbPath & ";Version=3;Password=" & AgLibrary.ClsConstant.PubDbPassword & ";"
                            End If
                            Connection_Pakka_Temp.Open()

                            mQry = " Select H.DocId, H.ManualRefNo As OrderNo, H.V_Date As OrderDate
                                    From SaleOrder H 
                                    Where H.Site_Code = '" & AgL.PubSiteCode & "'
                                    And H.Div_Code = '" & AgL.PubDivCode & "'
                                    And IfNull(H.Status,'" & AgTemplate.ClsMain.SaleOrderStatus.Active & "') <> '" & AgTemplate.ClsMain.SaleOrderStatus.Closed & "' "
                            TxtOrderNo.AgHelpDataSet() = AgL.FillData(mQry, Connection_Pakka_Temp)

                            Connection_Pakka_Temp.Close()
                            Connection_Pakka_Temp.Dispose()
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            Dim bRowIndex As Integer = Dgl1.CurrentCell.RowIndex
            Dim bColumnIndex As Integer = Dgl1.CurrentCell.ColumnIndex

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Supplier
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Supplier) Is Nothing Then
                            mQry = " Select Code, Name From ViewHelpSubGroup
                                Where Parent = '" & (Dgl1.Item(Col1MasterSupplier, Dgl1.CurrentCell.RowIndex).Tag) & "'"
                            Dgl1.AgHelpDataSet(Col1Supplier) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1ItemGroup
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1ItemGroup) Is Nothing Then
                            mQry = "SELECT Ig.Code, Ig.Description FROM ItemGroup Ig 
                                    Where DefaultSupplier = '" & Dgl1.Item(Col1MasterSupplier, 0).Tag & "' "
                            Dgl1.AgHelpDataSet(Col1ItemGroup) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl2_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl2.EditingControl_KeyDown
        Try
            Dim bRowIndex As Integer = Dgl2.CurrentCell.RowIndex
            Dim bColumnIndex As Integer = Dgl2.CurrentCell.ColumnIndex

            Select Case Dgl2.Columns(Dgl2.CurrentCell.ColumnIndex).Name
                Case Col2ShipToParty
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl2.AgHelpDataSet(Col2ShipToParty) Is Nothing Then
                            mQry = "SELECT Sg.Subcode, Sg.Name FROM Subgroup Sg "
                            Dgl2.AgHelpDataSet(Col2ShipToParty) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If Dgl1.CurrentCell Is Nothing Then Exit Sub

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Supplier, Col1ItemGroup, Col1InvoiceNo, Col1InvoiceDate, Col1Tax, Col1Amount
                    If Val(Dgl1.Item(Col1AddedManuallySr, Dgl1.CurrentCell.RowIndex).Value) = 0 And
                        Val(Dgl1.Item(Col1IsThirdPartyBilling, Dgl1.CurrentCell.RowIndex).Value) = 0 Then
                        Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).ReadOnly = True
                    Else
                        Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).ReadOnly = False
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function FDataValidation() As Boolean
        Dim bNoInvoicesFeed As Boolean = True
        For I As Integer = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1WInvoiceNo, I).Value <> "" Then
                bNoInvoicesFeed = False
            End If
        Next






        If bNoInvoicesFeed = True Then
            MsgBox("No Invoice Detail Entered...!", MsgBoxStyle.Information)
            FDataValidation = False
            Exit Function
        End If

        For I As Integer = 0 To Dgl1.Rows.Count - 1
            If AgL.XNull(Dgl1.Item(Col1WInvoiceNo, I).Value) <> "" Then
                If Dgl1.Item(Col1WInvoiceDate, I).Value = "" Then
                    MsgBox("W Invoice Date is blank for line no " & Dgl1.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                    Dgl1.CurrentCell = Dgl1.Item(Col1WInvoiceDate, I)
                    Dgl1.Focus()
                    FDataValidation = False
                    Exit Function
                End If

                If CDate(Dgl1.Item(Col1WInvoiceDate, I).Value) > CDate(AgL.PubLoginDate) Then
                    MsgBox("Future date transaction is not allowed.", MsgBoxStyle.Information)
                    Dgl1.CurrentCell = Dgl1.Item(Col1WInvoiceDate, I)
                    Dgl1.Focus()
                    FDataValidation = False
                    Exit Function
                End If

                If CDate(Dgl1.Item(Col1WInvoiceDate, I).Value) < CDate(mFromTransDate) Then
                    MsgBox("Date can not be older than " & mFromTransDate, MsgBoxStyle.Information)
                    Dgl1.CurrentCell = Dgl1.Item(Col1WInvoiceDate, I)
                    Dgl1.Focus()
                    FDataValidation = False
                    Exit Function
                End If

                If Val(Dgl1.Item(Col1WQty, I).Value) = 0 Then
                    MsgBox("W Qty is blank for line no " & Dgl1.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                    Dgl1.CurrentCell = Dgl1.Item(Col1WQty, I)
                    Dgl1.Focus()
                    FDataValidation = False
                    Exit Function
                End If

                If AgL.XNull(Dgl1.Item(Col1Supplier, I).Value) = "" Then
                    MsgBox("Purchase Invoice No is not blank But Party is blank at line no " & Dgl1.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                    Dgl1.CurrentCell = Dgl1.Item(Col1Supplier, I)
                    Dgl1.Focus()
                    FDataValidation = False
                    Exit Function
                End If

                For J As Integer = 0 To Dgl2.Rows.Count - 1
                    If (Dgl1.Item(Col1SaleInvoiceDocId, I).Value = Dgl2.Item(Col2SaleInvoiceDocId, J).Value And
                        Dgl1.Item(Col1SaleInvoiceDocId, I).Value <> "" And
                        Dgl2.Item(Col2SaleInvoiceDocId, J).Value <> "") Or
                        (Dgl1.Item(Col1AddedManuallySr, I).Value = Dgl2.Item(Col2AddedManuallySr, J).Value And
                        Val(Dgl1.Item(Col1AddedManuallySr, I).Value) <> 0 And
                        Val(Dgl2.Item(Col2AddedManuallySr, J).Value) <> 0) Or
                        (Dgl1.Item(Col1IsThirdPartyBilling, I).Value = Dgl2.Item(Col2IsThirdPartyBilling, J).Value And
                        Val(Dgl1.Item(Col1IsThirdPartyBilling, I).Value) <> 0 And
                        Val(Dgl2.Item(Col2IsThirdPartyBilling, J).Value) <> 0) Then
                        If Dgl2.Item(Col2WInvoiceNo, J).Value = "" Then
                            MsgBox("W Sale Invoice No is blank for line no " & Dgl2.Item(ColSNo, J).Value & "", MsgBoxStyle.Information)
                            Dgl2.CurrentCell = Dgl2.Item(Col2WInvoiceNo, J)
                            Dgl2.Focus()
                            FDataValidation = False
                            Exit Function
                        End If

                        If Dgl2.Item(Col2InvoiceDate, J).Value = "" Then
                            If Dgl2.Item(Col2WInvoiceDate, J).Value = "" Then
                                MsgBox("W Invoice Date is blank for line no " & Dgl2.Item(ColSNo, J).Value & "", MsgBoxStyle.Information)
                                Dgl2.CurrentCell = Dgl2.Item(Col2WInvoiceDate, J)
                                Dgl2.Focus()
                                FDataValidation = False
                                Exit Function
                            End If
                        End If

                        If AgL.XNull(Dgl2.Item(Col2WInvoiceDate, J).Value) <> "" Then
                            If CDate(Dgl2.Item(Col2WInvoiceDate, J).Value) > CDate(AgL.PubLoginDate) Then
                                MsgBox("Future date transaction is not allowed.", MsgBoxStyle.Information)
                                Dgl2.CurrentCell = Dgl2.Item(Col2WInvoiceDate, J)
                                Dgl2.Focus()
                                FDataValidation = False
                                Exit Function
                            End If

                            If CDate(Dgl2.Item(Col2WInvoiceDate, J).Value) < CDate(mFromTransDate) Then
                                MsgBox("Date can not be older then " & mFromTransDate, MsgBoxStyle.Information)
                                Dgl2.CurrentCell = Dgl2.Item(Col2WInvoiceDate, J)
                                Dgl2.Focus()
                                FDataValidation = False
                                Exit Function
                            End If
                        End If

                        If Val(Dgl2.Item(Col2WQty, J).Value) = 0 Then
                            MsgBox("W Qty is blank for line no " & Dgl2.Item(ColSNo, J).Value & "", MsgBoxStyle.Information)
                            Dgl2.CurrentCell = Dgl2.Item(Col2WQty, J)
                            Dgl2.Focus()
                            FDataValidation = False
                            Exit Function
                        End If

                        If Val(Dgl2.Item(Col2WSaleInvoiceAmount, J).Value) < 0 Then
                            MsgBox("W Sale Invoice Amount is negative for line no " & Dgl2.Item(ColSNo, J).Value & "", MsgBoxStyle.Information)
                            Dgl2.CurrentCell = Dgl2.Item(Col2WSaleInvoiceAmount, J)
                            Dgl2.Focus()
                            FDataValidation = False
                            Exit Function
                        End If
                    End If
                Next
            End If
        Next

        For J As Integer = 0 To Dgl2.Rows.Count - 1
            If Dgl2.Item(Col2WSaleOrderDocId, J).Value = "" Then
                MsgBox("Sale Order is not Synced...!", MsgBoxStyle.Information)
                Dgl2.CurrentCell = Dgl2.Item(Col2WInvoiceNo, J)
                Dgl2.Focus()
                FDataValidation = False
                Exit Function
            End If

            For K As Integer = 0 To Dgl2.Rows.Count - 1
                If J <> K Then
                    If Dgl2.Item(Col2WInvoiceNo, J).Value <> "" And Dgl2.Item(Col2WInvoiceNo, K).Value <> "" Then
                        If Dgl2.Item(Col2WInvoiceNo, J).Value <> Dgl2.Item(Col2WInvoiceNo, K).Value Then
                            MsgBox("Multiple Kachha Sale Invoices are not allowed in single entry.", MsgBoxStyle.Information)
                            Dgl2.CurrentCell = Dgl2.Item(Col2WInvoiceNo, J)
                            Dgl2.Focus()
                            FDataValidation = False
                            Exit Function
                        End If
                    End If

                    If Dgl2.Item(Col2WInvoiceNo, J).Value <> "" And Dgl2.Item(Col2WInvoiceNo, K).Value <> "" Then
                        If Dgl2.Item(Col2InvoiceNo, J).Value <> Dgl2.Item(Col2InvoiceNo, K).Value Then
                            If Dgl2.Item(Col2WInvoiceNo, J).Value = Dgl2.Item(Col2WInvoiceNo, K).Value Then
                                MsgBox("Pakka Sale Invoices are not same but Kachha Sale Invoices Nos are Same.Can't allow.", MsgBoxStyle.Information)
                                Dgl2.CurrentCell = Dgl2.Item(Col2WInvoiceNo, J)
                                Dgl2.Focus()
                                FDataValidation = False
                                Exit Function
                            End If
                        End If
                    End If

                    If Dgl2.Item(Col2WInvoiceNo, J).Value <> "" And Dgl2.Item(Col2WInvoiceNo, K).Value <> "" Then
                        If Dgl2.Item(Col2InvoiceNo, J).Value = Dgl2.Item(Col2InvoiceNo, K).Value Then
                            If Dgl2.Item(Col2WInvoiceNo, J).Value <> Dgl2.Item(Col2WInvoiceNo, K).Value Then
                                MsgBox("Pakka Sale Invoices are same but Kachha Sale Invoices Nos are difference.Can't allow.", MsgBoxStyle.Information)
                                Dgl2.CurrentCell = Dgl2.Item(Col2WInvoiceNo, J)
                                Dgl2.Focus()
                                FDataValidation = False
                                Exit Function
                            End If
                        End If
                    End If
                End If
            Next



            'If J > 0 Then
            '    If Dgl2.Item(Col2WInvoiceNo, J).Value <> "" Then
            '        If Dgl2.Item(Col2WInvoiceNo, J).Value <> "" And Dgl2.Item(Col2WInvoiceNo, J - 1).Value <> "" Then
            '            If Dgl2.Item(Col2WInvoiceNo, J).Value <> Dgl2.Item(Col2WInvoiceNo, J - 1).Value Then
            '                MsgBox("Multiple Kachha Sale Invoices are not allowed in single entry.", MsgBoxStyle.Information)
            '                Dgl2.CurrentCell = Dgl2.Item(Col2WInvoiceNo, J)
            '                Dgl2.Focus()
            '                FDataValidation = False
            '                Exit Function
            '            End If
            '        End If
            '    End If
            'End If

            'If J > 0 Then
            '    If Dgl2.Item(Col2InvoiceNo, J).Value <> "" Then
            '        If Dgl2.Item(Col2InvoiceNo, J).Value = Dgl2.Item(Col2InvoiceNo, J - 1).Value Then
            '            If Dgl2.Item(Col2WInvoiceNo, J).Value <> Dgl2.Item(Col2WInvoiceNo, J - 1).Value Then
            '                MsgBox("Pakka Sale Invoices are same but Kachha Sale Invoices Nos are different.Can't allow.", MsgBoxStyle.Information)
            '                Dgl2.CurrentCell = Dgl2.Item(Col2WInvoiceNo, J)
            '                Dgl2.Focus()
            '                FDataValidation = False
            '                Exit Function
            '            End If
            '        End If
            '    End If
            'End If


            If AgL.XNull(Dgl2.Item(Col2InvoiceNo, J).Value) <> "" And
                        AgL.XNull(Dgl2.Item(Col2Party, J).Value) = "" Then
                MsgBox("Purchase Invoice No is not blank But Party is blank at line no " & Dgl2.Item(ColSNo, J).Value & "", MsgBoxStyle.Information)
                Dgl2.CurrentCell = Dgl2.Item(Col2Party, J)
                Dgl2.Focus()
                FDataValidation = False
                Exit Function
            End If


            For I As Integer = 0 To Dgl1.Rows.Count - 1
                If (Dgl1.Item(Col1SaleInvoiceDocId, I).Value = Dgl2.Item(Col2SaleInvoiceDocId, J).Value And
                        Dgl1.Item(Col1SaleInvoiceDocId, I).Value <> "" And
                        Dgl2.Item(Col2SaleInvoiceDocId, J).Value <> "") Or
                        (Dgl1.Item(Col1AddedManuallySr, I).Value = Dgl2.Item(Col2AddedManuallySr, J).Value And
                        Val(Dgl1.Item(Col1AddedManuallySr, I).Value) <> 0 And
                        Val(Dgl2.Item(Col2AddedManuallySr, J).Value) <> 0) Or
                        (Dgl1.Item(Col1IsThirdPartyBilling, I).Value = Dgl2.Item(Col2IsThirdPartyBilling, J).Value And
                        Val(Dgl1.Item(Col1IsThirdPartyBilling, I).Value) <> 0 And
                        Val(Dgl2.Item(Col2IsThirdPartyBilling, J).Value) <> 0) Then
                    If Dgl2.Item(Col2WInvoiceNo, J).Value <> "" Then
                        If Dgl1.Item(Col1WInvoiceNo, I).Value = "" Then
                            MsgBox("W Purchase Invoice No is blank for line no " & Dgl1.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                            Dgl1.CurrentCell = Dgl1.Item(Col1WInvoiceNo, I)
                            Dgl1.Focus()
                            FDataValidation = False
                            Exit Function
                        End If
                    End If
                End If
            Next
        Next

        'If BtnTransportDetail.Tag Is Nothing Then
        FetchLr()
        'Else
        'If AgL.XNull(CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowLrNo).Value) = "" Then
        '        FetchLr()
        '    End If
        'End If

        If BtnTransportDetail.Tag Is Nothing Then
            ShowSaleInvoiceHeader("", False)
        End If

        If AgL.XNull(CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowTransporter).Tag) = "" Then
            MsgBox("Transporter is blank.", MsgBoxStyle.Information)
            ShowSaleInvoiceHeader() : Exit Function
        End If

        If AgL.XNull(CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowLrNo).Value) = "" Then
            MsgBox("Lr No. is blank.", MsgBoxStyle.Information)
            ShowSaleInvoiceHeader() : Exit Function
        End If


        FDataValidation = True
    End Function
    Public Sub FPostPurchaseData_ForDifference(Conn As Object, Cmd As Object)
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim I As Integer
        Dim J As Integer
        Dim StrErrLog As String = ""


        Dim Tot_Commission_Amount As Double = 0
        Dim Tot_Additional_Commission_Amount As Double = 0
        Dim Tot_Gross_Amount As Double = 0
        Dim Tot_Taxable_Amount As Double = 0
        Dim Tot_Tax1 As Double = 0
        Dim Tot_Tax2 As Double = 0
        Dim Tot_Tax3 As Double = 0
        Dim Tot_Tax4 As Double = 0
        Dim Tot_Tax5 As Double = 0
        Dim Tot_SubTotal1 As Double = 0
        Dim Tot_Other_Charges As Double = 0
        Dim Tot_Other_Charges1 As Double = 0


        For I = 0 To Dgl1.Rows.Count - 1
            If AgL.XNull(Dgl1.Item(Col1WInvoiceNo, I).Value) <> "" Then
                'If AgL.VNull(Dgl1.Item(Col1WPurchInvoiceAmount, I).Value) <> 0 Then
                Tot_Commission_Amount = 0
                Tot_Additional_Commission_Amount = 0
                Tot_Gross_Amount = 0
                Tot_Taxable_Amount = 0
                Tot_Tax1 = 0
                Tot_Tax2 = 0
                Tot_Tax3 = 0
                Tot_Tax4 = 0
                Tot_Tax5 = 0
                Tot_SubTotal1 = 0
                Tot_Other_Charges = 0
                Tot_Other_Charges1 = 0

                Dim PurchInvoiceTableList(0) As FrmPurchInvoiceDirect_WithDimension.StructPurchInvoice
                Dim PurchInvoiceTable As New FrmPurchInvoiceDirect_WithDimension.StructPurchInvoice

                PurchInvoiceTable.DocID = ""
                PurchInvoiceTable.V_Type = "WPI"
                PurchInvoiceTable.V_Prefix = ""
                PurchInvoiceTable.Site_Code = AgL.PubSiteCode
                PurchInvoiceTable.Div_Code = AgL.PubDivCode
                PurchInvoiceTable.V_No = 0
                PurchInvoiceTable.V_Date = Dgl1.Item(Col1WInvoiceDate, I).Value
                If Dgl1.Item(Col1GeneratedManualRefNo, I).Value <> "" Then
                    PurchInvoiceTable.ManualRefNo = Dgl1.Item(Col1GeneratedManualRefNo, I).Value
                Else
                    PurchInvoiceTable.ManualRefNo = ""
                End If
                PurchInvoiceTable.Vendor = Dgl1.Item(Col1Supplier, I).Tag
                PurchInvoiceTable.VendorName = Dgl1.Item(Col1Supplier, I).Value
                PurchInvoiceTable.AgentCode = ""
                PurchInvoiceTable.AgentName = ""
                PurchInvoiceTable.BillToPartyCode = Dgl1.Item(Col1MasterSupplier, I).Tag
                PurchInvoiceTable.BillToPartyName = Dgl1.Item(Col1MasterSupplier, I).Value
                PurchInvoiceTable.VendorAddress = ""
                PurchInvoiceTable.VendorCity = ""
                PurchInvoiceTable.VendorMobile = ""
                PurchInvoiceTable.VendorSalesTaxNo = ""
                PurchInvoiceTable.SalesTaxGroupParty = ""
                PurchInvoiceTable.PlaceOfSupply = ""
                PurchInvoiceTable.StructureCode = ""
                PurchInvoiceTable.CustomFields = ""
                PurchInvoiceTable.VendorDocNo = Dgl1.Item(Col1WInvoiceNo, I).Value
                PurchInvoiceTable.VendorDocDate = Dgl1.Item(Col1WInvoiceDate, I).Value
                PurchInvoiceTable.ReferenceDocId = ""
                PurchInvoiceTable.GenDocId = ""
                PurchInvoiceTable.GenDocIdSr = ""
                PurchInvoiceTable.Tags = "+" & TxtTag.Text
                PurchInvoiceTable.Remarks = "Kachha Invoice Amount : " + Dgl1.Item(Col1WAmount, I).Value.ToString
                PurchInvoiceTable.Status = "Active"
                PurchInvoiceTable.EntryBy = AgL.PubUserName
                PurchInvoiceTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                PurchInvoiceTable.ApproveBy = ""
                PurchInvoiceTable.ApproveDate = ""
                PurchInvoiceTable.MoveToLog = ""
                PurchInvoiceTable.MoveToLogDate = ""
                PurchInvoiceTable.UploadDate = ""
                PurchInvoiceTable.LockText = "Genereded From Sale Invoice W Entry.Can't Edit."

                PurchInvoiceTable.Deduction_Per = 0
                PurchInvoiceTable.Deduction = 0
                PurchInvoiceTable.Other_Charge_Per = 0
                PurchInvoiceTable.Other_Charge = 0
                PurchInvoiceTable.Round_Off = 0
                PurchInvoiceTable.Net_Amount = 0


                'For Line Detail
                PurchInvoiceTable.Line_Sr = 1
                PurchInvoiceTable.Line_ItemCode = Dgl1.Item(Col1ItemGroup, I).Tag
                PurchInvoiceTable.Line_ItemName = Dgl1.Item(Col1ItemGroup, I).Value
                PurchInvoiceTable.Line_Specification = ""
                PurchInvoiceTable.Line_SalesTaxGroupItem = "GST 0%"
                PurchInvoiceTable.Line_ReferenceNo = ""
                PurchInvoiceTable.Line_DocQty = Val(Dgl1.Item(Col1WQty, I).Value)
                PurchInvoiceTable.Line_FreeQty = 0
                PurchInvoiceTable.Line_Qty = Val(Dgl1.Item(Col1WQty, I).Value)
                PurchInvoiceTable.Line_Unit = "Nos"
                PurchInvoiceTable.Line_Pcs = 0
                PurchInvoiceTable.Line_UnitMultiplier = 0
                PurchInvoiceTable.Line_DealUnit = ""
                PurchInvoiceTable.Line_DocDealQty = ""
                PurchInvoiceTable.Line_DiscountPer = 0
                PurchInvoiceTable.Line_DiscountAmount = 0
                PurchInvoiceTable.Line_AdditionalDiscountPer = 0
                PurchInvoiceTable.Line_AdditionalDiscountAmount = 0
                PurchInvoiceTable.Line_Amount = Val(Dgl1.Item(Col1WAmount, I).Value)
                PurchInvoiceTable.Line_Rate = Math.Round(Val(PurchInvoiceTable.Line_Amount) / Val(PurchInvoiceTable.Line_Qty), 2)
                PurchInvoiceTable.Line_Remark = TxtRemark.Text
                PurchInvoiceTable.Line_BaleNo = ""
                PurchInvoiceTable.Line_LotNo = ""
                PurchInvoiceTable.Line_ReferenceDocId = ""
                PurchInvoiceTable.Line_ReferenceSr = ""
                PurchInvoiceTable.Line_PurchInvoice = ""
                PurchInvoiceTable.Line_PurchInvoiceSr = ""
                PurchInvoiceTable.Line_GrossWeight = 0
                PurchInvoiceTable.Line_NetWeight = 0

                PurchInvoiceTable.Line_CommissionPer = Val(Dgl1.Item(Col1DiscountPer, I).Value)
                PurchInvoiceTable.Line_CommissionAmount = Val(Dgl1.Item(Col1CommissionAmount, I).Value)
                PurchInvoiceTable.Line_AdditionalCommissionPer = Val(Dgl1.Item(Col1AdditionalDiscountPer, I).Value)
                PurchInvoiceTable.Line_AdditionalCommissionAmount = Val(Dgl1.Item(Col1AdditionalCommissionAmount, I).Value)


                PurchInvoiceTable.Line_Gross_Amount = PurchInvoiceTable.Line_Amount
                PurchInvoiceTable.Line_Taxable_Amount = PurchInvoiceTable.Line_Amount
                PurchInvoiceTable.Line_Tax1_Per = 0
                PurchInvoiceTable.Line_Tax1 = Val(Dgl1.Item(Col1Tax, I).Value)
                'PurchInvoiceTable.Line_Tax1 = PurchInvoiceTable.Line_Amount * PurchInvoiceTable.Line_Tax1_Per / 100
                PurchInvoiceTable.Line_Tax2_Per = 0
                PurchInvoiceTable.Line_Tax2 = PurchInvoiceTable.Line_Amount * PurchInvoiceTable.Line_Tax2_Per / 100
                PurchInvoiceTable.Line_Tax3_Per = 0
                PurchInvoiceTable.Line_Tax3 = PurchInvoiceTable.Line_Amount * PurchInvoiceTable.Line_Tax3_Per / 100
                PurchInvoiceTable.Line_Tax4_Per = 0
                PurchInvoiceTable.Line_Tax4 = PurchInvoiceTable.Line_Amount * PurchInvoiceTable.Line_Tax4_Per / 100
                PurchInvoiceTable.Line_Tax5_Per = 0
                PurchInvoiceTable.Line_Tax5 = PurchInvoiceTable.Line_Amount * PurchInvoiceTable.Line_Tax5_Per / 100
                PurchInvoiceTable.Line_SubTotal1 = PurchInvoiceTable.Line_Amount + PurchInvoiceTable.Line_Tax1 + PurchInvoiceTable.Line_Tax2 +
                                                            PurchInvoiceTable.Line_Tax3 + PurchInvoiceTable.Line_Tax4 + PurchInvoiceTable.Line_Tax5
                PurchInvoiceTable.Line_Other_Charge = Val(Dgl1.Item(Col1WFreight, I).Value)
                PurchInvoiceTable.Line_Other_Charge1 = Val(Dgl1.Item(Col1WPacking, I).Value)

                'For Header Values
                Tot_Commission_Amount += PurchInvoiceTable.Line_CommissionAmount
                Tot_Additional_Commission_Amount += PurchInvoiceTable.Line_AdditionalCommissionAmount
                Tot_Gross_Amount += PurchInvoiceTable.Line_Gross_Amount
                Tot_Taxable_Amount += PurchInvoiceTable.Line_Taxable_Amount
                Tot_Tax1 += PurchInvoiceTable.Line_Tax1
                Tot_Tax2 += PurchInvoiceTable.Line_Tax2
                Tot_Tax3 += PurchInvoiceTable.Line_Tax3
                Tot_Tax4 += PurchInvoiceTable.Line_Tax4
                Tot_Tax5 += PurchInvoiceTable.Line_Tax5
                Tot_SubTotal1 += PurchInvoiceTable.Line_SubTotal1
                Tot_Other_Charges += PurchInvoiceTable.Line_Other_Charge
                Tot_Other_Charges1 += PurchInvoiceTable.Line_Other_Charge1

                PurchInvoiceTableList(UBound(PurchInvoiceTableList)) = PurchInvoiceTable
                ReDim Preserve PurchInvoiceTableList(UBound(PurchInvoiceTableList) + 1)

                '#Region "Packing Charge"
                '                If Val(Dgl1.Item(Col1WPacking, I).Value) > 0 Then
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Sr = 2
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_ItemCode = ""
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_ItemName = ItemCode.Packing
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Specification = ""
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_SalesTaxGroupItem = "GST 0%"
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_ReferenceNo = ""
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_DocQty = 1
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_FreeQty = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Qty = 1
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Unit = "Nos"
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Pcs = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_UnitMultiplier = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_DealUnit = ""
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_DocDealQty = ""
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_DiscountPer = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_DiscountAmount = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_AdditionalDiscountPer = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_AdditionalDiscountAmount = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Amount = Val(Dgl1.Item(Col1WPacking, I).Value)
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Rate = Val(Dgl1.Item(Col1WPacking, I).Value)
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Remark = ""
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_BaleNo = ""
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_LotNo = ""
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_ReferenceDocId = ""
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_ReferenceSr = ""
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_PurchInvoice = ""
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_PurchInvoiceSr = ""
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_GrossWeight = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_NetWeight = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Gross_Amount = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Amount
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Amount
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax1_Per = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax1 = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax1_Per / 100
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax2_Per = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax2 = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax2_Per / 100
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax3_Per = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax3 = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax3_Per / 100
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax4_Per = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax4 = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax4_Per / 100
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax5_Per = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax5 = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax5_Per / 100
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_SubTotal1 = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Amount + PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax1 + PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax2 +
                '                                                                PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax3 + PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax4 + PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax5
                '                    'For Header Values
                '                    Tot_Gross_Amount += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Gross_Amount
                '                    Tot_Taxable_Amount += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount
                '                    Tot_Tax1 += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax1
                '                    Tot_Tax2 += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax2
                '                    Tot_Tax3 += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax3
                '                    Tot_Tax4 += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax4
                '                    Tot_Tax5 += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax5
                '                    Tot_SubTotal1 += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_SubTotal1

                '                    'PurchInvoiceTableList(UBound(PurchInvoiceTableList)) = PurchInvoiceTable
                '                    ReDim Preserve PurchInvoiceTableList(UBound(PurchInvoiceTableList) + 1)
                '                End If
                '#End Region

                '#Region "Freight Charge"
                '                If Val(Dgl1.Item(Col1WFreight, I).Value) > 0 Then
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Sr = 3
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_ItemCode = ""
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_ItemName = ItemCode.Freight
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Specification = ""
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_SalesTaxGroupItem = "GST 0%"
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_ReferenceNo = ""
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_DocQty = 1
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_FreeQty = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Qty = 1
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Unit = "Nos"
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Pcs = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_UnitMultiplier = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_DealUnit = ""
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_DocDealQty = ""
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_DiscountPer = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_DiscountAmount = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_AdditionalDiscountPer = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_AdditionalDiscountAmount = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Amount = Val(Dgl1.Item(Col1WFreight, I).Value)
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Rate = Val(Dgl1.Item(Col1WFreight, I).Value)
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Remark = ""
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_BaleNo = ""
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_LotNo = ""
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_ReferenceDocId = ""
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_ReferenceSr = ""
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_PurchInvoice = ""
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_PurchInvoiceSr = ""
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_GrossWeight = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_NetWeight = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Gross_Amount = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Amount
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Amount
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax1_Per = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax1 = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax1_Per / 100
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax2_Per = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax2 = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax2_Per / 100
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax3_Per = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax3 = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax3_Per / 100
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax4_Per = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax4 = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax4_Per / 100
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax5_Per = 0
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax5 = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount * PurchInvoiceTable.Line_Tax5_Per / 100
                '                    PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_SubTotal1 = PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount + PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax1 + PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax2 +
                '                                                                PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax3 + PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax4 + PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax5
                '                    'For Header Values
                '                    Tot_Gross_Amount += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Gross_Amount
                '                    Tot_Taxable_Amount += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Taxable_Amount
                '                    Tot_Tax1 += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax1
                '                    Tot_Tax2 += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax2
                '                    Tot_Tax3 += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax3
                '                    Tot_Tax4 += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax4
                '                    Tot_Tax5 += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_Tax5
                '                    Tot_SubTotal1 += PurchInvoiceTableList(UBound(PurchInvoiceTableList)).Line_SubTotal1

                '                    'PurchInvoiceTableList(UBound(PurchInvoiceTableList)) = PurchInvoiceTable
                '                    ReDim Preserve PurchInvoiceTableList(UBound(PurchInvoiceTableList) + 1)
                '                End If
                '#End Region


                PurchInvoiceTableList(0).CommissionAmount = Tot_Commission_Amount
                PurchInvoiceTableList(0).AdditionalCommissionAmount = Tot_Additional_Commission_Amount
                PurchInvoiceTableList(0).Gross_Amount = Tot_Gross_Amount
                PurchInvoiceTableList(0).Taxable_Amount = Tot_Taxable_Amount
                PurchInvoiceTableList(0).Tax1 = Tot_Tax1
                PurchInvoiceTableList(0).Tax2 = Tot_Tax2
                PurchInvoiceTableList(0).Tax3 = Tot_Tax3
                PurchInvoiceTableList(0).Tax4 = Tot_Tax4
                PurchInvoiceTableList(0).Tax5 = Tot_Tax5
                PurchInvoiceTableList(0).SubTotal1 = Tot_SubTotal1
                PurchInvoiceTableList(0).Other_Charge = Tot_Other_Charges
                PurchInvoiceTableList(0).Other_Charge1 = Tot_Other_Charges1
                PurchInvoiceTableList(0).Deduction = 0
                PurchInvoiceTableList(0).Round_Off = Math.Round(Math.Round(PurchInvoiceTableList(0).SubTotal1 + PurchInvoiceTableList(0).Other_Charge + PurchInvoiceTableList(0).Other_Charge1) - (PurchInvoiceTableList(0).SubTotal1 + PurchInvoiceTableList(0).Other_Charge + PurchInvoiceTableList(0).Other_Charge1), 2)
                PurchInvoiceTableList(0).Net_Amount = Math.Round(PurchInvoiceTableList(0).SubTotal1 + PurchInvoiceTableList(0).Other_Charge + PurchInvoiceTableList(0).Other_Charge1)

                Dim Tot_RoundOff As Double = 0
                Dim Tot_NetAmount As Double = 0
                For J = 0 To PurchInvoiceTableList.Length - 1
                    If Val(PurchInvoiceTableList(0).Gross_Amount) > 0 Then
                        PurchInvoiceTableList(J).Line_Round_Off = Math.Round(PurchInvoiceTableList(0).Round_Off * PurchInvoiceTableList(J).Line_Gross_Amount / PurchInvoiceTableList(0).Gross_Amount, 2)
                        PurchInvoiceTableList(J).Line_Net_Amount = Math.Round(PurchInvoiceTableList(0).Net_Amount * PurchInvoiceTableList(J).Line_Gross_Amount / PurchInvoiceTableList(0).Gross_Amount, 2)
                    End If
                    Tot_RoundOff += Val(PurchInvoiceTableList(J).Line_Round_Off)
                    Tot_NetAmount += Val(PurchInvoiceTableList(J).Line_Net_Amount)
                Next

                If Tot_RoundOff <> PurchInvoiceTableList(0).Round_Off Then
                    PurchInvoiceTableList(0).Line_Round_Off = PurchInvoiceTableList(0).Line_Round_Off + (PurchInvoiceTableList(0).Round_Off - Tot_RoundOff)
                End If

                If Tot_NetAmount <> PurchInvoiceTableList(0).Net_Amount Then
                    PurchInvoiceTableList(0).Line_Net_Amount = PurchInvoiceTableList(0).Line_Net_Amount + (PurchInvoiceTableList(0).Net_Amount - Tot_NetAmount)
                End If
                'If PurchInvoiceTableList(0).Net_Amount > 0 Then
                Dim bDocId As String = FrmPurchInvoiceDirect_WithDimension.InsertPurchInvoice(PurchInvoiceTableList)
                If AgL.XNull(bDocId) <> "" Then
                    Dgl1.Item(Col1WPurchInvoiceDocId, I).Value = bDocId

                    mQry = " UPDATE PurchInvoice Set 
                            AmsDocId = " & AgL.Chk_Text(Dgl1.Item(Col1PurchInvoiceDocId, I).Value) & ",
                            AmsDocNo = " & AgL.Chk_Text(Dgl1.Item(Col1InvoiceNo, I).Value) & ",
                            AmsDocDate = " & AgL.Chk_Date(Dgl1.Item(Col1InvoiceDate, I).Value) & ",
                            AmsDocTaxAmount = " & Val(Dgl1.Item(Col1Tax, I).Value) & ", 
                            AmsDocNetAmount = " & Val(Dgl1.Item(Col1Amount, I).Value) & "  
                            Where DocId = '" & bDocId & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mQry = " INSERT INTO SaleInvoiceGeneratedEntries(Code, Type, DocId, SaleOrderNo, SaleOrderDocId, Site_Code, Div_Code, V_Type, Remarks) 
                            Select '" & mSearchCode & "' As Code, 'Purchase Invoice', '" & bDocId & "', '" & TxtOrderNo.Text & "', 
                            '" & TxtSaleOrderDocId_W.Text & "', '" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "', '" & PurchInvoiceTableList(0).V_Type & "', '" & TxtRemark.Text & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mQry = " UPDATE Ledger Set EffectiveDate = " & AgL.Chk_Date(Dgl1.Item(Col1WInvoiceDate, I).Value) & "
                                Where DocId = '" & Dgl1.Item(Col1PurchInvoiceDocId, I).Value & "'"
                    AgL.Dman_ExecuteNonQry(mQry, Connection_Pakka)
                Else
                    mQry = " UPDATE Ledger Set EffectiveDate = " & AgL.Chk_Date(Dgl1.Item(Col1WInvoiceDate, I).Value) & "
                                Where DocId = '" & Dgl1.Item(Col1PurchInvoiceDocId, I).Value & "'"
                    AgL.Dman_ExecuteNonQry(mQry, Connection_Pakka)
                End If
                If AgL.XNull(Dgl1.Item(Col1GeneratedDocId, I).Value) <> "" Then
                    'Dim SourceDatabasePath As String = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "ActualDBPath", "")
                    'Dim SourcePath As String = System.IO.Path.GetDirectoryName(SourceDatabasePath) + "\Images\" + AgL.XNull(Dgl1.Item(Col1GeneratedDocId, I).Value)
                    Dim SourcePath As String = PubAttachmentPath + AgL.XNull(Dgl1.Item(Col1GeneratedDocId, I).Value)
                    If (Directory.Exists(SourcePath)) Then
                        My.Computer.FileSystem.RenameDirectory(SourcePath, bDocId)
                    End If
                End If
                Dgl1.Item(Col1GeneratedDocId, I).Value = bDocId
            End If
        Next
    End Sub
    Public Sub FPostSaleData_ForDifference(Conn As Object, Cmd As Object)
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim dtTemp As DataTable = Nothing
        Dim I As Integer
        Dim StrErrLog As String = ""
        Dim mRow As Integer = 0


        Dim Tot_Gross_Amount As Double = 0
        Dim Tot_Taxable_Amount As Double = 0
        Dim Tot_Tax1 As Double = 0
        Dim Tot_Tax2 As Double = 0
        Dim Tot_Tax3 As Double = 0
        Dim Tot_Tax4 As Double = 0
        Dim Tot_Tax5 As Double = 0
        Dim Tot_SubTotal1 As Double = 0
        Dim Tot_Other_Charges As Double = 0
        Dim Tot_Other_Charges1 As Double = 0


        Tot_Gross_Amount = 0
        Tot_Taxable_Amount = 0
        Tot_Tax1 = 0
        Tot_Tax2 = 0
        Tot_Tax3 = 0
        Tot_Tax4 = 0
        Tot_Tax5 = 0
        Tot_SubTotal1 = 0
        Tot_Other_Charges = 0
        Tot_Other_Charges1 = 0


        For M As Integer = 0 To Dgl2.Rows.Count - 1
            If AgL.XNull(Dgl2.Item(Col2WInvoiceNo, M).Value) <> "" Then
                mRow = M
            End If
        Next

        Dim SaleInvoiceTableList(0) As FrmSaleInvoiceDirect_WithDimension_ShyamaShyam.StructSaleInvoice

        'In One Transaction only one Sale Invoice Can be Generated.The First Sale Invoice No will be 
        'Considered As Sale Invoice No
        If AgL.XNull(Dgl2.Item(Col2WInvoiceNo, mRow).Value) <> "" Then
            'If AgL.VNull(Dgl2.Item(Col2WSaleInvoiceAmount, mRow).Value) <> 0 Then
            SaleInvoiceTableList(0).DocID = ""
            SaleInvoiceTableList(0).V_Type = "WSI"
            SaleInvoiceTableList(0).V_Prefix = ""
            SaleInvoiceTableList(0).Site_Code = AgL.PubSiteCode
            SaleInvoiceTableList(0).Div_Code = AgL.PubDivCode
            SaleInvoiceTableList(0).V_No = 0
            SaleInvoiceTableList(0).V_Date = Dgl2.Item(Col2WInvoiceDate, mRow).Value
            'SaleInvoiceTableList(0).V_Date = Dgl2.Item(Col2InvoiceDate, mRow).Value
            'SaleInvoiceTableList(0).ManualRefNo = Dgl2.Item(Col2WInvoiceNo, mRow).Value

            Dim WSaleInvoice As String = ""
            WSaleInvoice = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "SaleInvoice", "WSI", AgL.PubLoginDate, AgL.PubDivCode, AgL.PubSiteCode, AgTemplate.ClsMain.ManualRefType.Max)

            SaleInvoiceTableList(0).ManualRefNo = WSaleInvoice

            SaleInvoiceTableList(0).SaleToParty = Dgl2.Item(Col2Party, mRow).Tag
            SaleInvoiceTableList(0).SaleToPartyName = Dgl2.Item(Col2Party, mRow).Value
            SaleInvoiceTableList(0).AgentCode = ""
            SaleInvoiceTableList(0).AgentName = ""
            SaleInvoiceTableList(0).BillToPartyCode = Dgl2.Item(Col2MasterParty, mRow).Tag
            SaleInvoiceTableList(0).BillToPartyName = Dgl2.Item(Col2MasterParty, mRow).Value
            SaleInvoiceTableList(0).SaleToPartyAddress = ""
            SaleInvoiceTableList(0).SaleToPartyCityCode = ""
            SaleInvoiceTableList(0).SaleToPartyMobile = ""
            SaleInvoiceTableList(0).SaleToPartySalesTaxNo = ""
            SaleInvoiceTableList(0).ShipToPartyCode = Dgl2.Item(Col2ShipToParty, mRow).Tag
            SaleInvoiceTableList(0).ShipToAddress = ""
            SaleInvoiceTableList(0).SalesTaxGroupParty = ""
            SaleInvoiceTableList(0).PlaceOfSupply = PlaceOfSupplay.WithinState
            SaleInvoiceTableList(0).StructureCode = ""
            SaleInvoiceTableList(0).CustomFields = ""
            SaleInvoiceTableList(0).ReferenceDocId = ""
            SaleInvoiceTableList(0).Tags = "+" & TxtTag.Text
            SaleInvoiceTableList(0).Remarks = "Pakka Invoice No : " + Dgl2.Item(Col2InvoiceNo, mRow).Value.ToString +
                                                        " And Invoice Amount : " + Dgl2.Item(Col2Amount, mRow).Value.ToString +
                                                        IIf(AgL.XNull(Dgl2.Item(Col2ShipToParty, mRow).Value) <> "", " And Ship To Party : " + AgL.XNull(Dgl2.Item(Col2ShipToParty, mRow).Value).ToString, "")
            SaleInvoiceTableList(0).Status = "Active"
            SaleInvoiceTableList(0).EntryBy = AgL.PubUserName
            SaleInvoiceTableList(0).EntryDate = AgL.GetDateTime(AgL.GcnRead)
            SaleInvoiceTableList(0).ApproveBy = ""
            SaleInvoiceTableList(0).ApproveDate = ""
            SaleInvoiceTableList(0).MoveToLog = ""
            SaleInvoiceTableList(0).MoveToLogDate = ""
            SaleInvoiceTableList(0).UploadDate = ""
            SaleInvoiceTableList(0).LockText = "Genereded From Sale Invoice W Entry.Can't Edit."

            SaleInvoiceTableList(0).Deduction_Per = 0
            SaleInvoiceTableList(0).Deduction = 0
            SaleInvoiceTableList(0).Other_Charge_Per = 0
            SaleInvoiceTableList(0).Other_Charge = 0
            SaleInvoiceTableList(0).Round_Off = 0
            SaleInvoiceTableList(0).Net_Amount = 0

            For I = 0 To Dgl2.Rows.Count - 1
                If Val(Dgl2.Item(Col2WQty, I).Value) > 0 Then
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Sr = UBound(SaleInvoiceTableList) + 1
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_ItemCode = Dgl2.Item(Col2ItemGroup, I).Tag
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_ItemName = Dgl2.Item(Col2ItemGroup, I).Value
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Specification = ""
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_SalesTaxGroupItem = "GST 0%"
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_ReferenceNo = ""
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_DocQty = Dgl2.Item(Col2WQty, I).Value
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_FreeQty = 0
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Qty = Val(Dgl2.Item(Col2WQty, I).Value)
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Unit = "Nos"
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Pcs = 0
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_UnitMultiplier = 0
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_DealUnit = ""
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_DocDealQty = ""
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_DiscountPer = Val(Dgl2.Item(Col2DiscountPer, I).Value)
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_DiscountAmount = Val(Dgl2.Item(Col2WDiscount, I).Value)
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_AdditionalDiscountPer = Val(Dgl2.Item(Col2AdditionalDiscountPer, I).Value)
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_AdditionalDiscountAmount = 0
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_ExtraDiscountPer = Val(Dgl2.Item(Col2ExtraDiscountPer, I).Value)
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_ExtraDiscountAmount = 0
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_AdditionPer = Val(Dgl2.Item(Col2AdditionPer, I).Value)
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_AdditionAmount = 0
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Amount = Val(Dgl2.Item(Col2WSaleInvoiceAmount, I).Value)
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Rate = (Val(Dgl2.Item(Col2WSaleInvoiceAmount, I).Value) +
                                                                                    Val(Dgl2.Item(Col2WDiscount, I).Value)) / Val(Dgl2.Item(Col2WQty, I).Value)
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Remark = TxtRemark.Text
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_BaleNo = ""
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_LotNo = ""
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_ReferenceDocId = ""
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_SaleInvoice = Dgl2.Item(Col2WSaleOrderDocId, I).Value
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_SaleInvoiceSr = 1
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_GrossWeight = 0
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_NetWeight = 0
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Gross_Amount = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Amount
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Taxable_Amount = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Amount
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax1_Per = 0
                    'SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax1 = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Amount * SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax1_Per / 100
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax1 = Val(Dgl2.Item(Col2Tax, I).Value)
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax2_Per = 0
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax2 = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Amount * SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax2_Per / 100
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax3_Per = 0
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax3 = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Amount * SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax3_Per / 100
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax4_Per = 0
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax4 = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Amount * SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax4_Per / 100
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax5_Per = 0
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax5 = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Amount * SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax5_Per / 100
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_SubTotal1 = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Amount +
                                                                SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax1 +
                                                                SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax2 +
                                                                SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax3 +
                                                                SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax4 +
                                                                SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax5

                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Other_Charge = Val(Dgl2.Item(Col2WFreight, I).Value)
                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Other_Charge1 = Val(Dgl2.Item(Col2WPacking, I).Value)



                    'For Header Values
                    Tot_Gross_Amount += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Gross_Amount
                    Tot_Taxable_Amount += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Taxable_Amount
                    Tot_Tax1 += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax1
                    Tot_Tax2 += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax2
                    Tot_Tax3 += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax3
                    Tot_Tax4 += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax4
                    Tot_Tax5 += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax5
                    Tot_SubTotal1 += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_SubTotal1
                    Tot_Other_Charges += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Other_Charge
                    Tot_Other_Charges1 += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Other_Charge1


                    'SaleInvoiceTableList(UBound(SaleInvoiceTableList)) = SaleInvoiceTable
                    ReDim Preserve SaleInvoiceTableList(UBound(SaleInvoiceTableList) + 1)

                    '#Region "Packing Charge"
                    '                    If Val(Dgl2.Item(Col2WPacking, I).Value) > 0 Then
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Sr = UBound(SaleInvoiceTableList) + 1
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_ItemCode = ""
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_ItemName = ItemCode.Packing
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Specification = ""
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_SalesTaxGroupItem = "GST 0%"
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_ReferenceNo = ""
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_DocQty = 1
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_FreeQty = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Qty = 1
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Unit = "Nos"
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Pcs = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_UnitMultiplier = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_DealUnit = ""
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_DocDealQty = ""
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_DiscountPer = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_DiscountAmount = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_AdditionalDiscountPer = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_AdditionalDiscountAmount = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Amount = Val(Dgl2.Item(Col2WPacking, I).Value)
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Rate = Val(Dgl2.Item(Col2WPacking, I).Value)
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Remark = ""
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_BaleNo = ""
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_LotNo = ""
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_ReferenceDocId = ""
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_SaleInvoice = ""
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_SaleInvoiceSr = ""
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_GrossWeight = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_NetWeight = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Gross_Amount = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Amount
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Taxable_Amount = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Amount
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax1_Per = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax1 = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Taxable_Amount * SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax1_Per / 100
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax2_Per = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax2 = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Taxable_Amount * SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax2_Per / 100
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax3_Per = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax3 = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Taxable_Amount * SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax3_Per / 100
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax4_Per = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax4 = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Taxable_Amount * SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax4_Per / 100
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax5_Per = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax5 = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Taxable_Amount * SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax5_Per / 100
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_SubTotal1 = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Amount + SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax1 + SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax2 +
                    '                                                                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax3 + SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax4 + SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax5
                    '                        'For Header Values
                    '                        Tot_Gross_Amount += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Gross_Amount
                    '                        Tot_Taxable_Amount += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Taxable_Amount
                    '                        Tot_Tax1 += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax1
                    '                        Tot_Tax2 += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax2
                    '                        Tot_Tax3 += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax3
                    '                        Tot_Tax4 += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax4
                    '                        Tot_Tax5 += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax5
                    '                        Tot_SubTotal1 += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_SubTotal1

                    '                        'SaleInvoiceTableList(UBound(SaleInvoiceTableList)) = SaleInvoiceTable
                    '                        ReDim Preserve SaleInvoiceTableList(UBound(SaleInvoiceTableList) + 1)
                    '                    End If
                    '#End Region

                    '#Region "Freight Charge"
                    '                    If Val(Dgl2.Item(Col2WFreight, I).Value) > 0 Then
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Sr = UBound(SaleInvoiceTableList) + 1
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_ItemCode = ""
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_ItemName = ItemCode.Freight
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Specification = ""
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_SalesTaxGroupItem = "GST 0%"
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_ReferenceNo = ""
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_DocQty = 1
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_FreeQty = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Qty = 1
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Unit = "Nos"
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Pcs = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_UnitMultiplier = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_DealUnit = ""
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_DocDealQty = ""
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_DiscountPer = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_DiscountAmount = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_AdditionalDiscountPer = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_AdditionalDiscountAmount = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Amount = Val(Dgl2.Item(Col2WFreight, I).Value)
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Rate = Val(Dgl2.Item(Col2WFreight, I).Value)
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Remark = ""
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_BaleNo = ""
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_LotNo = ""
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_ReferenceDocId = ""
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_SaleInvoice = ""
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_SaleInvoiceSr = ""
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_GrossWeight = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_NetWeight = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Gross_Amount = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Amount
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Taxable_Amount = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Amount
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax1_Per = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax1 = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Taxable_Amount * SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax1_Per / 100
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax2_Per = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax2 = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Taxable_Amount * SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax2_Per / 100
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax3_Per = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax3 = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Taxable_Amount * SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax3_Per / 100
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax4_Per = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax4 = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Taxable_Amount * SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax4_Per / 100
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax5_Per = 0
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax5 = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Taxable_Amount * SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax5_Per / 100
                    '                        SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_SubTotal1 = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Taxable_Amount + SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax1 + SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax2 +
                    '                                                                    SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax3 + SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax4 + SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax5
                    '                        'For Header Values
                    '                        Tot_Gross_Amount += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Gross_Amount
                    '                        Tot_Taxable_Amount += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Taxable_Amount
                    '                        Tot_Tax1 += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax1
                    '                        Tot_Tax2 += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax2
                    '                        Tot_Tax3 += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax3
                    '                        Tot_Tax4 += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax4
                    '                        Tot_Tax5 += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax5
                    '                        Tot_SubTotal1 += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_SubTotal1

                    '                        'SaleInvoiceTableList(UBound(SaleInvoiceTableList)) = SaleInvoiceTable
                    '                        ReDim Preserve SaleInvoiceTableList(UBound(SaleInvoiceTableList) + 1)
                    '                    End If
                    '#End Region
                End If
            Next

            SaleInvoiceTableList(0).Gross_Amount = Tot_Gross_Amount
            SaleInvoiceTableList(0).Taxable_Amount = Tot_Taxable_Amount
            SaleInvoiceTableList(0).Tax1 = Tot_Tax1
            SaleInvoiceTableList(0).Tax2 = Tot_Tax2
            SaleInvoiceTableList(0).Tax3 = Tot_Tax3
            SaleInvoiceTableList(0).Tax4 = Tot_Tax4
            SaleInvoiceTableList(0).Tax5 = Tot_Tax5
            SaleInvoiceTableList(0).SubTotal1 = Tot_SubTotal1
            SaleInvoiceTableList(0).Other_Charge = Tot_Other_Charges
            SaleInvoiceTableList(0).Other_Charge1 = Tot_Other_Charges1
            SaleInvoiceTableList(0).Deduction = 0
            SaleInvoiceTableList(0).Round_Off = Math.Round(Math.Round(SaleInvoiceTableList(0).SubTotal1 + SaleInvoiceTableList(0).Other_Charge + SaleInvoiceTableList(0).Other_Charge1) - (SaleInvoiceTableList(0).SubTotal1 + SaleInvoiceTableList(0).Other_Charge + SaleInvoiceTableList(0).Other_Charge1), 2)
            SaleInvoiceTableList(0).Net_Amount = Math.Round(SaleInvoiceTableList(0).SubTotal1 + SaleInvoiceTableList(0).Other_Charge + SaleInvoiceTableList(0).Other_Charge1)



            Dim Tot_RoundOff As Double = 0
            Dim Tot_NetAmount As Double = 0
            For J As Integer = 0 To SaleInvoiceTableList.Length - 1
                If Val(SaleInvoiceTableList(0).Gross_Amount) > 0 Then
                    SaleInvoiceTableList(J).Line_Round_Off = Math.Round(SaleInvoiceTableList(0).Round_Off * SaleInvoiceTableList(J).Line_Gross_Amount / SaleInvoiceTableList(0).Gross_Amount, 2)
                    SaleInvoiceTableList(J).Line_Net_Amount = Math.Round(SaleInvoiceTableList(0).Net_Amount * SaleInvoiceTableList(J).Line_Gross_Amount / SaleInvoiceTableList(0).Gross_Amount, 2)
                End If
                Tot_RoundOff += SaleInvoiceTableList(J).Line_Round_Off
                Tot_NetAmount += SaleInvoiceTableList(J).Line_Net_Amount
            Next

            If Tot_RoundOff <> SaleInvoiceTableList(0).Round_Off Then
                SaleInvoiceTableList(0).Line_Round_Off = SaleInvoiceTableList(0).Line_Round_Off + (SaleInvoiceTableList(0).Round_Off - Tot_RoundOff)
            End If

            If Tot_NetAmount <> SaleInvoiceTableList(0).Net_Amount Then
                SaleInvoiceTableList(0).Line_Net_Amount = SaleInvoiceTableList(0).Line_Net_Amount + (SaleInvoiceTableList(0).Net_Amount - Tot_NetAmount)
            End If

            'If SaleInvoiceTableList(0).Net_Amount > 0 Then
            Dim bDocId As String = FrmSaleInvoiceDirect_WithDimension_ShyamaShyam.InsertSaleInvoice(SaleInvoiceTableList)
            If AgL.XNull(bDocId) <> "" And (AgL.XNull(SaleInvoiceTableList(0).V_Type) = "SI" Or AgL.XNull(SaleInvoiceTableList(0).V_Type) = "WSI") Then

                For M As Integer = 0 To Dgl2.Rows.Count - 1
                    If AgL.XNull(Dgl2.Item(Col2WInvoiceNo, M).Value) <> "" Then
                        Dgl2.Item(Col2WSaleInvoiceDocId, M).Value = bDocId
                    End If
                Next


                mQry = " UPDATE SaleInvoice Set 
                            AmsDocId = " & AgL.Chk_Text(Dgl2.Item(Col2SaleInvoiceDocId, mRow).Value) & ",
                            AmsDocNo = " & AgL.Chk_Text(Dgl2.Item(Col2InvoiceNo, mRow).Value) & ",
                            AmsDocDate = " & AgL.Chk_Date(Dgl2.Item(Col2InvoiceDate, mRow).Value) & ",
                            AmsDocTaxAmount = " & Val(Dgl2.Item(Col2Tax, mRow).Value) & ", 
                            AmsDocNetAmount = " & Val(Dgl2.Item(Col2Amount, mRow).Value) & "  
                            Where DocId = '" & bDocId & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                mQry = " INSERT INTO SaleInvoiceGeneratedEntries(Code, Type, DocId, SaleOrderNo, SaleOrderDocId, Site_Code, Div_Code, V_Type, Remarks) 
                            Select '" & mSearchCode & "' As Code, 'Sale Invoice', '" & bDocId & "', '" & TxtOrderNo.Text & "', 
                            '" & TxtSaleOrderDocId_W.Text & "', '" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "', '" & SaleInvoiceTableList(0).V_Type & "', '" & TxtRemark.Text & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = "Update PurchInvoice Set GenDocId = '" & bDocId & "' Where DocID = (Select DocID From SaleInvoiceGeneratedEntries Where Code = '" & mSearchCode & "' And Type = 'Purchase Invoice')"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                If BtnTransportDetail.Tag IsNot Nothing Then
                    CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).FSave(bDocId, Conn, Cmd)
                End If


                'mQry = "Insert Into TransactionReferences (DocID, ReferenceDocID, IsEditingAllowed, IsDeletingAllowed) 
                '        Values (" & AgL.Chk_Text(bDocId) & ", '" & bDocId & "', 1, 0) "
                'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            Else
            End If

            If AgL.XNull(Dgl2.Item(Col2GeneratedDocId, mRow).Value) <> "" Then
                'Dim SourceDatabasePath As String = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "ActualDBPath", "")
                'Dim SourcePath As String = System.IO.Path.GetDirectoryName(SourceDatabasePath) + "\Images\" + AgL.XNull(Dgl2.Item(Col2GeneratedDocId, mRow).Value)
                Dim SourcePath As String = PubAttachmentPath + AgL.XNull(Dgl2.Item(Col2GeneratedDocId, mRow).Value)
                If (Directory.Exists(SourcePath)) Then
                    My.Computer.FileSystem.RenameDirectory(SourcePath, bDocId)
                End If
            End If

            Dgl2.Item(Col2GeneratedDocId, mRow).Value = bDocId
        End If
    End Sub
    Private Sub BtnOk_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        FProcSave()
    End Sub
    Private Sub FProcSave()
        Dim mTrans As String = ""
        If FDataValidation() = False Then Exit Sub
        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            If mMode = "E" Then
                FCreateLog(mSearchCode)
            End If

            FDelete(mSearchCode, AgL.GCn, AgL.ECmd)

            mSearchCode = AgL.GetMaxId("SaleInvoiceGeneratedEntries", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

            FPostPurchaseData_ForDifference(AgL.GCn, AgL.ECmd)
            FPostSaleData_ForDifference(AgL.GCn, AgL.ECmd)
            'FPostDebitCreditNoteData_ForDifference(AgL.GCn, AgL.ECmd, "WDNS")

            FPostUIValues(mSearchCode, AgL.GCn, AgL.ECmd)

            FPostTransactionReferences(mSearchCode, AgL.GCn, AgL.ECmd)
            FLockPakkaEntries(mSearchCode, AgL.GCn, AgL.ECmd, "Invoice Created In Demo.Cant Modify!")

            If mMode = "A" Then
                FCreateLog(mSearchCode)
            End If

            AgL.ETrans.Commit()
            mTrans = "Commit"

            BtnTransportDetail.Tag = Nothing

            If MsgBox("Do you want to print?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1) = MsgBoxResult.Yes Then
                Dim dtTemp As DataTable
                mQry = "Select DocID From SaleInvoiceGeneratedEntries Where Code = '" & mSearchCode & "' And V_Type = 'SI' "
                dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                FGetPrintCrystal1(PrintFor.DocumentPrint, False, "")
            Else
                MsgBox("Entry Saved Successfully...", MsgBoxStyle.Information)
            End If
            BlankText()
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub BlankText()
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        Dgl2.RowCount = 1 : Dgl2.Rows.Clear()
        Dgl3.RowCount = 1 : Dgl3.Rows.Clear()
        TxtOrderNo.Tag = "" : TxtOrderNo.Text = ""
        TxtSaleOrderDocId_W.Tag = "" : TxtSaleOrderDocId_W.Text = ""
        TxtPartyName.Tag = "" : TxtPartyName.Text = ""
        TxtRemark.Tag = "" : TxtRemark.Text = ""
        mSearchCode = ""
    End Sub
    Private Sub Calculation()
        Dgl3.Rows.Clear()

        For J As Integer = 0 To Dgl2.Rows.Count - 1
            Dgl2.Item(Col2WSaleInvoiceAmount, J).Tag = "0"
            Dgl2.Item(Col2WDiscount, J).Tag = "0"
        Next

        For I As Integer = 0 To Dgl1.Rows.Count - 1
            Dgl1.Item(Col1CommissionAmount, I).Value = Math.Round(Val(Dgl1.Item(Col1WQty, I).Value) * Val(Dgl1.Item(Col1DiscountPer, I).Value), 2)
            Dgl1.Item(Col1AdditionalCommissionAmount, I).Value = Math.Round((Dgl1.Item(Col1WAmount, I).Value - Dgl1.Item(Col1CommissionAmount, I).Value) * Dgl1.Item(Col1AdditionalDiscountPer, I).Value / 100, 2)


            For J As Integer = 0 To Dgl2.Rows.Count - 1
                If Val(Dgl1.Item(Col1AddedManuallySr, I).Value) > 0 And Val(Dgl2.Item(Col2AddedManuallySr, J).Value) > 0 Then
                    If Dgl1.Item(Col1AddedManuallySr, I).Value = Dgl2.Item(Col2AddedManuallySr, J).Value Then
                        Dgl2.Item(Col2ItemGroup, J).Tag = Dgl1.Item(Col1ItemGroup, I).Tag
                        Dgl2.Item(Col2ItemGroup, J).Value = Dgl1.Item(Col1ItemGroup, I).Value
                    End If
                End If
            Next
        Next

        For I As Integer = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1WInvoiceDate, I).Value <> "" Then
                For J As Integer = 0 To Dgl2.Rows.Count - 1
                    If ((Dgl1.Item(Col1SaleInvoiceDocId, I).Value = Dgl2.Item(Col2SaleInvoiceDocId, J).Value And
                            Dgl1.Item(Col1SaleInvoiceDocId, I).Value <> "" And
                            Dgl2.Item(Col2SaleInvoiceDocId, J).Value <> "") Or
                            (Dgl1.Item(Col1AddedManuallySr, I).Value = Dgl2.Item(Col2AddedManuallySr, J).Value And
                            Val(Dgl1.Item(Col1AddedManuallySr, I).Value) <> 0 And
                            Val(Dgl2.Item(Col2AddedManuallySr, J).Value) <> 0) Or
                            (Dgl1.Item(Col1IsThirdPartyBilling, I).Value = Dgl2.Item(Col2IsThirdPartyBilling, J).Value And
                            Val(Dgl1.Item(Col1IsThirdPartyBilling, I).Value) <> 0 And
                            Val(Dgl2.Item(Col2IsThirdPartyBilling, J).Value) <> 0)) And
                            Dgl1.Item(Col1ItemGroup, I).Value = Dgl2.Item(Col2ItemGroup, J).Value Then

                        Dgl2.Item(Col2WQty, J).Value = Dgl1.Item(Col1WQty, I).Value

                        'Dgl2.Item(Col2WSaleInvoiceAmount, J).Value = Val(Dgl2.Item(Col2WSaleInvoiceAmount, J).Tag) + Val(Dgl1.Item(Col1WAmount, I).Value) +
                        '        (Val(Dgl1.Item(Col1WAmount, I).Value) * Val(Dgl2.Item(Col2AdditionPer, J).Value) / 100) -
                        '        (Val(Dgl1.Item(Col1WAmount, I).Value) * Val(Dgl2.Item(Col2AdditionalDiscountPer, J).Value) / 100) -
                        '        (Val(Dgl1.Item(Col1WQty, I).Value) * Val(Dgl2.Item(Col2DiscountPer, J).Value)) -
                        '        (Val(Dgl2.Item(Col2AmountWithoutTax, J).Value))

                        'Above Statement is commented because it was calculating
                        'difference amount and now it will calculate Complete Sale Amount
                        Dgl2.Item(Col2WSaleInvoiceAmount, J).Value = Val(Dgl1.Item(Col1WAmount, I).Value) +
                                (Val(Dgl1.Item(Col1WAmount, I).Value) * Val(Dgl2.Item(Col2AdditionPer, J).Value) / 100) -
                                (Val(Dgl1.Item(Col1WAmount, I).Value) * Val(Dgl2.Item(Col2AdditionalDiscountPer, J).Value) / 100) -
                                (Val(Dgl1.Item(Col1WAmount, I).Value) * Val(Dgl2.Item(Col2ExtraDiscountPer, J).Value) / 100) -
                                (Val(Dgl2.Item(Col2WQty, J).Value) * Val(Dgl2.Item(Col2DiscountPer, J).Value))

                        Dgl2.Item(Col2WDiscount, J).Value = (Val(Dgl1.Item(Col1WAmount, I).Value) * Val(Dgl2.Item(Col2AdditionalDiscountPer, J).Value) / 100) +
                                (Val(Dgl1.Item(Col1WAmount, I).Value) * Val(Dgl2.Item(Col2ExtraDiscountPer, J).Value) / 100) +
                                (Val(Dgl2.Item(Col2WQty, J).Value) * Val(Dgl2.Item(Col2DiscountPer, J).Value) -
                                (Val(Dgl1.Item(Col1WAmount, I).Value) * Val(Dgl2.Item(Col2AdditionPer, J).Value) / 100))

                        'Dgl2.Item(Col2WDiscount, J).Value = Val(Dgl2.Item(Col2WDiscount, J).Tag) +
                        '        -(Val(Dgl1.Item(Col1WAmount, I).Value) * Val(Dgl2.Item(Col2AdditionPer, J).Value) / 100) +
                        '        (Val(Dgl1.Item(Col1WAmount, I).Value) * Val(Dgl2.Item(Col2AdditionalDiscountPer, J).Value) / 100) -
                        '        (Val(Dgl1.Item(Col1WQty, I).Value) * Val(Dgl2.Item(Col2DiscountPer, J).Value)) -
                        '        (Val(Dgl2.Item(Col2Discount, J).Value))

                        'If AgL.XNull(Dgl2.Item(Col2SyncedSaleInvoiceDocId, J).Value) <> "" Then
                        '    If AgL.XNull(AgL.Dman_Execute("Select Structure  From SaleInvoice Where DocId = '" & Dgl2.Item(Col2SyncedSaleInvoiceDocId, J).Value & "'", AgL.GCn).ExecuteScalar()) = "GstSaleMrp" Then
                        '        Dgl2.Item(Col2WSaleInvoiceAmount, J).Value = 0
                        '    End If
                        'End If
                    End If
                Next

                Dim bAmountDiffDebitNote As Double = 0
                bAmountDiffDebitNote = Math.Round(Dgl1.Item(Col1WAmount, I).Value *
                        Val(Dgl1.Item(Col1AdditionalDiscountPer, I).Value) / 100, 2)

                bAmountDiffDebitNote = bAmountDiffDebitNote + (Math.Round(Val(Dgl1.Item(Col1WQty, I).Value) *
                        Val(Dgl1.Item(Col1DiscountPer, I).Value)))

                If bAmountDiffDebitNote > 0 Then
                    Dgl3.Rows.Add()
                    Dgl3.Item(ColSNo, Dgl3.Rows.Count - 1).Value = Dgl3.Rows.Count
                    Dgl3.Item(Col3DrCr, Dgl3.Rows.Count - 1).Value = "Debit Note"
                    Dgl3.Item(Col3V_Date, Dgl3.Rows.Count - 1).Value = Dgl1.Item(Col1WInvoiceDate, I).Value
                    Dgl3.Item(Col3Party, Dgl3.Rows.Count - 1).Tag = Dgl1.Item(Col1MasterSupplier, I).Tag
                    Dgl3.Item(Col3Party, Dgl3.Rows.Count - 1).Value = Dgl1.Item(Col1MasterSupplier, I).Value
                    Dgl3.Item(Col3LinkedParty, Dgl3.Rows.Count - 1).Tag = Dgl1.Item(Col1Supplier, I).Tag
                    Dgl3.Item(Col3LinkedParty, Dgl3.Rows.Count - 1).Value = Dgl1.Item(Col1Supplier, I).Value
                    Dgl3.Item(Col3ReasonAc, Dgl3.Rows.Count - 1).Value = "Rate Diff A/c"
                    'Dgl3.Item(Col3SyncedPurchInvoiceDocId, Dgl3.Rows.Count - 1).Value = Dgl1.Item(Col1SyncedPurchInvoiceDocId, I).Value
                    Dgl3.Item(Col3Amount, Dgl3.Rows.Count - 1).Value = bAmountDiffDebitNote
                    Dgl3.Item(Col3Remark, Dgl3.Rows.Count - 1).Value = "Debit Note due to Amount Differnece In Kachha and Pakka Invoice. Pakka Purchase Invoice No " & Dgl1.Item(Col1InvoiceNo, I).Value & " And Kachha Purchase Invoice No." & Dgl1.Item(Col1WInvoiceNo, I).Value & "."
                End If


                'Dim bDiscountDiffDebitNote As Double = 0

                'If Dgl1.Item(Col1InvoiceAdditionalDiscountPer, I).Value < Dgl1.Item(Col1AdditionalDiscountPer, I).Value Then
                '    bDiscountDiffDebitNote = Math.Round(Val(Dgl1.Item(Col1Amount, I).Value) *
                '    (Val(Dgl1.Item(Col1AdditionalDiscountPer, I).Value) - Val(Dgl1.Item(Col1InvoiceAdditionalDiscountPer, I).Value)) / 100, 2)
                'End If

                'If Dgl1.Item(Col1InvoiceDiscountPer, I).Value < Dgl1.Item(Col1DiscountPer, I).Value Then
                '    bDiscountDiffDebitNote = bDiscountDiffDebitNote + (Math.Round(Val(Dgl1.Item(Col1Amount, I).Value) *
                '    (Val(Dgl1.Item(Col1DiscountPer, I).Value) - Val(Dgl1.Item(Col1InvoiceDiscountPer, I).Value)) / 100, 2))
                'End If



                'If bDiscountDiffDebitNote > 0 Then
                '    Dgl3.Rows.Add()
                '    Dgl3.Item(ColSNo, Dgl3.Rows.Count - 1).Value = Dgl3.Rows.Count
                '    Dgl3.Item(Col3DrCr, Dgl3.Rows.Count - 1).Value = "Debit Note"
                '    Dgl3.Item(Col3V_Date, Dgl3.Rows.Count - 1).Value = Dgl1.Item(Col1WInvoiceDate, I).Value
                '    Dgl3.Item(Col3Party, Dgl3.Rows.Count - 1).Value = Dgl1.Item(Col1MasterSupplier, I).Value
                '    Dgl3.Item(Col3ReasonAc, Dgl3.Rows.Count - 1).Value = "Discount Diff A/c"
                '    'Dgl3.Item(Col3SyncedPurchInvoiceDocId, Dgl3.Rows.Count - 1).Value = Dgl1.Item(Col1SyncedPurchInvoiceDocId, I).Value
                '    Dgl3.Item(Col3Amount, Dgl3.Rows.Count - 1).Value = bDiscountDiffDebitNote
                '    Dgl3.Item(Col3Remark, Dgl3.Rows.Count - 1).Value = "Debit Note due to Discount Differnece In Kachha and Pakka Invoice."
                'End If
            End If

            'If AgL.XNull(Dgl1.Item(Col1SyncedPurchInvoiceDocId, I).Value) <> "" Then
            '    If AgL.XNull(AgL.Dman_Execute("Select Structure  From PurchInvoice Where DocId = '" & Dgl1.Item(Col1SyncedPurchInvoiceDocId, I).Value & "'", AgL.GCn).ExecuteScalar()) = "GstPurMrp" Then
            '        Dgl1.Item(Col1WPurchInvoiceAmount, I).Value = 0
            '    End If
            'End If
        Next
    End Sub
    Private Sub Dgl1_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Try
            If Dgl1.CurrentCell Is Nothing Then Exit Sub

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1WInvoiceNo
                    If Dgl1.Item(Col1InvoiceDate, Dgl1.CurrentCell.RowIndex).Value <> "" And
                            Dgl1.Item(Col1WInvoiceNo, Dgl1.CurrentCell.RowIndex).Value <> "" Then
                        Dgl1.Item(Col1WInvoiceDate, Dgl1.CurrentCell.RowIndex).Value = Dgl1.Item(Col1InvoiceDate, Dgl1.CurrentCell.RowIndex).Value
                    End If

                Case Col1ItemGroup
                    mQry = "Select IG.Default_DiscountPerPurchase, IG.Default_AdditionalDiscountPerPurchase,
                            0 As Default_AdditionPerPurchase, Ig.DefaultSupplier, 
                            Sg.Name As DefaultSupplierName
                            From ItemGroup IG  
                            LEFT JOIN SubGroup Sg On Ig.DefaultSupplier = Sg.SubCode
                            Where IG.Code ='" & FGetOMSIdFromItemCode(Dgl1.Item(Col1ItemGroup, Dgl1.CurrentCell.RowIndex).Tag) & "'"

                    Dim DTDiscounts As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)
                    If DTDiscounts.Rows.Count > 0 Then
                        Dgl1.Item(Col1DiscountPer, Dgl1.CurrentCell.RowIndex).Value = AgL.VNull(DTDiscounts.Rows(0)("Default_DiscountPerPurchase"))
                        Dgl1.Item(Col1AdditionalDiscountPer, Dgl1.CurrentCell.RowIndex).Value = AgL.VNull(DTDiscounts.Rows(0)("Default_AdditionalDiscountPerPurchase"))
                        Dgl1.Item(Col1AdditionPer, Dgl1.CurrentCell.RowIndex).Value = AgL.VNull(DTDiscounts.Rows(0)("Default_AdditionPerPurchase"))

                        Dgl1.Item(Col1MasterSupplier, Dgl1.CurrentCell.RowIndex).Tag = FGetSubCodeFromOMSId(AgL.XNull(DTDiscounts.Rows(0)("DefaultSupplier")))
                        Dgl1.Item(Col1MasterSupplier, Dgl1.CurrentCell.RowIndex).Value = AgL.XNull(DTDiscounts.Rows(0)("DefaultSupplierName"))

                        mQry = " Select Code, Name From ViewHelpSubGroup
                                Where Parent = '" & (Dgl1.Item(Col1MasterSupplier, Dgl1.CurrentCell.RowIndex).Tag) & "'"
                        Dgl1.AgHelpDataSet(Col1Supplier) = AgL.FillData(mQry, AgL.GCn)

                        If Dgl1.AgHelpDataSet(Col1Supplier).Tables(0).Rows.Count = 1 Then
                            Dgl1.Item(Col1Supplier, Dgl1.CurrentCell.RowIndex).Tag = AgL.XNull(Dgl1.AgHelpDataSet(Col1Supplier).Tables(0).Rows(0)("Code"))
                            Dgl1.Item(Col1Supplier, Dgl1.CurrentCell.RowIndex).Value = AgL.XNull(Dgl1.AgHelpDataSet(Col1Supplier).Tables(0).Rows(0)("Name"))
                        End If
                    End If
            End Select
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub FPostDebitCreditNoteData_ForDifference(Conn As Object, Cmd As Object,
                                                      V_Type As String)
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim bMultiplier As Integer = 1
        Dim I As Integer
        Dim J As Integer
        Dim StrErrLog As String = ""


        Dim Tot_Gross_Amount As Double = 0
        Dim Tot_Taxable_Amount As Double = 0
        Dim Tot_Tax1 As Double = 0
        Dim Tot_Tax2 As Double = 0
        Dim Tot_Tax3 As Double = 0
        Dim Tot_Tax4 As Double = 0
        Dim Tot_Tax5 As Double = 0
        Dim Tot_SubTotal1 As Double = 0

        If V_Type = "DNS" Then
            bMultiplier = -1
        End If


        For I = 0 To Dgl3.Rows.Count - 1
            If Val(Dgl3.Item(Col3Amount, I).Value) > 0 Then
                Tot_Gross_Amount = 0
                Tot_Taxable_Amount = 0
                Tot_Tax1 = 0
                Tot_Tax2 = 0
                Tot_Tax3 = 0
                Tot_Tax4 = 0
                Tot_Tax5 = 0
                Tot_SubTotal1 = 0

                Dim VoucherEntryTableList(0) As FrmVoucherEntry.StructLedgerHead
                Dim VoucherEntryTable As New FrmVoucherEntry.StructLedgerHead

                VoucherEntryTable.DocID = ""
                VoucherEntryTable.V_Type = V_Type
                VoucherEntryTable.V_Prefix = ""
                VoucherEntryTable.Site_Code = AgL.PubSiteCode
                VoucherEntryTable.Div_Code = AgL.PubDivCode
                VoucherEntryTable.V_No = 0
                VoucherEntryTable.V_Date = Dgl3.Item(Col3V_Date, I).Value
                VoucherEntryTable.ManualRefNo = ""
                VoucherEntryTable.Subcode = Dgl3.Item(Col3Party, I).Tag
                VoucherEntryTable.SubcodeName = Dgl3.Item(Col3Party, I).Value
                VoucherEntryTable.LinkedSubcode = Dgl3.Item(Col3LinkedParty, I).Tag
                VoucherEntryTable.LinkedSubcodeName = Dgl3.Item(Col3LinkedParty, I).Value

                If VoucherEntryTable.V_Type = "DNS" Or VoucherEntryTable.V_Type = "DNC" Then
                    VoucherEntryTable.DrCr = "Dr"
                ElseIf VoucherEntryTable.V_Type = "CNS" Or VoucherEntryTable.V_Type = "CNC" Then
                    VoucherEntryTable.DrCr = "Cr"
                End If

                VoucherEntryTable.SalesTaxGroupParty = ""
                VoucherEntryTable.PlaceOfSupply = PlaceOfSupplay.WithinState
                VoucherEntryTable.StructureCode = ""
                VoucherEntryTable.CustomFields = ""
                VoucherEntryTable.Remarks = Dgl3.Item(Col3Remark, I).Value
                VoucherEntryTable.Status = "Active"
                VoucherEntryTable.EntryBy = AgL.PubUserName
                VoucherEntryTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
                VoucherEntryTable.ApproveBy = ""
                VoucherEntryTable.ApproveDate = ""
                VoucherEntryTable.MoveToLog = ""
                VoucherEntryTable.MoveToLogDate = ""
                VoucherEntryTable.UploadDate = ""
                VoucherEntryTable.LockText = "Genereded From Sale Invoice W Entry.Can't Edit."

                VoucherEntryTable.Deduction_Per = 0
                VoucherEntryTable.Deduction = 0
                VoucherEntryTable.Other_Charge_Per = 0
                VoucherEntryTable.Other_Charge = 0
                VoucherEntryTable.Round_Off = 0
                VoucherEntryTable.Net_Amount = 0

                VoucherEntryTable.Line_Sr = J + 1
                VoucherEntryTable.Line_SubCode = ""
                VoucherEntryTable.Line_SubCodeName = Dgl3.Item(Col3ReasonAc, I).Value
                VoucherEntryTable.Line_Specification = ""
                VoucherEntryTable.Line_SalesTaxGroupItem = "GST 0%"
                VoucherEntryTable.Line_Qty = 0
                VoucherEntryTable.Line_Unit = ""
                VoucherEntryTable.Line_Rate = 0
                VoucherEntryTable.Line_Amount = Val(Dgl3.Item(Col3Amount, I).Value)
                VoucherEntryTable.Line_Amount = VoucherEntryTable.Line_Amount * bMultiplier
                VoucherEntryTable.Line_ChqRefNo = ""
                VoucherEntryTable.Line_ChqRefDate = ""
                VoucherEntryTable.Line_Remarks = ""


                VoucherEntryTable.Line_Gross_Amount = VoucherEntryTable.Line_Amount
                VoucherEntryTable.Line_Taxable_Amount = VoucherEntryTable.Line_Amount
                VoucherEntryTable.Line_Tax1_Per = 0
                VoucherEntryTable.Line_Tax1 = (VoucherEntryTable.Line_Amount * VoucherEntryTable.Line_Tax1_Per / 100)
                VoucherEntryTable.Line_Tax2_Per = 0
                VoucherEntryTable.Line_Tax2 = (VoucherEntryTable.Line_Amount * VoucherEntryTable.Line_Tax2_Per / 100)
                VoucherEntryTable.Line_Tax3_Per = 0
                VoucherEntryTable.Line_Tax3 = (VoucherEntryTable.Line_Amount * VoucherEntryTable.Line_Tax3_Per / 100)
                VoucherEntryTable.Line_Tax4_Per = 0
                VoucherEntryTable.Line_Tax4 = (VoucherEntryTable.Line_Amount * VoucherEntryTable.Line_Tax4_Per / 100)
                VoucherEntryTable.Line_Tax5_Per = 0
                VoucherEntryTable.Line_Tax5 = (VoucherEntryTable.Line_Amount * VoucherEntryTable.Line_Tax5_Per / 100)
                VoucherEntryTable.Line_SubTotal1 = (VoucherEntryTable.Line_Amount +
                                                        VoucherEntryTable.Line_Tax1 +
                                                        VoucherEntryTable.Line_Tax2 +
                                                        VoucherEntryTable.Line_Tax3 +
                                                        VoucherEntryTable.Line_Tax4 +
                                                        VoucherEntryTable.Line_Tax5)


                'For Header Values
                Tot_Gross_Amount += VoucherEntryTable.Line_Gross_Amount
                Tot_Taxable_Amount += VoucherEntryTable.Line_Taxable_Amount
                Tot_Tax1 += VoucherEntryTable.Line_Tax1
                Tot_Tax2 += VoucherEntryTable.Line_Tax2
                Tot_Tax3 += VoucherEntryTable.Line_Tax3
                Tot_Tax4 += VoucherEntryTable.Line_Tax4
                Tot_Tax5 += VoucherEntryTable.Line_Tax5
                Tot_SubTotal1 += VoucherEntryTable.Line_SubTotal1


                VoucherEntryTableList(UBound(VoucherEntryTableList)) = VoucherEntryTable
                ReDim Preserve VoucherEntryTableList(UBound(VoucherEntryTableList) + 1)


                VoucherEntryTableList(0).Gross_Amount = Tot_Gross_Amount
                VoucherEntryTableList(0).Taxable_Amount = Tot_Taxable_Amount
                VoucherEntryTableList(0).Tax1 = Tot_Tax1
                VoucherEntryTableList(0).Tax2 = Tot_Tax2
                VoucherEntryTableList(0).Tax3 = Tot_Tax3
                VoucherEntryTableList(0).Tax4 = Tot_Tax4
                VoucherEntryTableList(0).Tax5 = Tot_Tax5
                VoucherEntryTableList(0).SubTotal1 = Tot_SubTotal1
                VoucherEntryTableList(0).Other_Charge = 0
                VoucherEntryTableList(0).Deduction = 0
                VoucherEntryTableList(0).Round_Off = Math.Round(Math.Round(VoucherEntryTableList(0).SubTotal1) - VoucherEntryTableList(0).SubTotal1, 2)
                VoucherEntryTableList(0).Net_Amount = Math.Round(VoucherEntryTableList(0).SubTotal1)

                Dim Tot_RoundOff As Double = 0
                Dim Tot_NetAmount As Double = 0
                For J = 0 To VoucherEntryTableList.Length - 1
                    VoucherEntryTableList(J).Line_Round_Off = Math.Round(VoucherEntryTableList(0).Round_Off * VoucherEntryTableList(J).Line_Gross_Amount / VoucherEntryTableList(0).Gross_Amount, 2)
                    VoucherEntryTableList(J).Line_Net_Amount = Math.Round(VoucherEntryTableList(0).Net_Amount * VoucherEntryTableList(J).Line_Gross_Amount / VoucherEntryTableList(0).Gross_Amount, 2)
                    Tot_RoundOff += VoucherEntryTableList(J).Line_Round_Off
                    Tot_NetAmount += VoucherEntryTableList(J).Line_Net_Amount
                Next

                If Tot_RoundOff <> VoucherEntryTableList(0).Round_Off Then
                    VoucherEntryTableList(0).Line_Round_Off = VoucherEntryTableList(0).Line_Round_Off + (VoucherEntryTableList(0).Round_Off - Tot_RoundOff)
                End If

                If Tot_NetAmount <> VoucherEntryTableList(0).Net_Amount Then
                    VoucherEntryTableList(0).Line_Net_Amount = VoucherEntryTableList(0).Line_Net_Amount + (VoucherEntryTableList(0).Net_Amount - Tot_NetAmount)
                End If
                Dim bDocId As String = FrmVoucherEntry.InsertLedgerHead(VoucherEntryTableList)
                If AgL.XNull(bDocId) <> "" Then
                    mQry = " INSERT INTO SaleInvoiceGeneratedEntries(Code, Type, DocId, SaleOrderNo, SaleOrderDocId, Site_Code, Div_Code, V_Type, Remarks) 
                            Select '" & mSearchCode & "' As Code, 'Debit Note', '" & bDocId & "', '" & TxtOrderNo.Text & "', 
                            '" & TxtSaleOrderDocId_W.Text & "', '" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "','" & VoucherEntryTableList(0).V_Type & "', '" & TxtRemark.Text & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    'If AgL.XNull(Dgl3.Item(Col3SyncedPurchInvoiceDocId, I).Value) <> "" Then
                    '    mQry = "Insert Into TransactionReferences (DocID, ReferenceDocID, IsEditingAllowed, IsDeletingAllowed) 
                    '        Values (" & AgL.Chk_Text(Dgl3.Item(Col3SyncedPurchInvoiceDocId, I).Value) & ", '" & bDocId & "', 1, 0) "
                    '    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    'Else
                    '    mQry = "Insert Into TransactionReferences (DocID, ReferenceDocID, IsEditingAllowed, IsDeletingAllowed) 
                    '        Values (" & AgL.Chk_Text(bDocId) & ", '" & bDocId & "', 1, 0) "
                    '    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    'End If
                End If
            End If
        Next
    End Sub
    Private Sub Find()
        mQry = " SELECT Ge.Code As SearchCode, Max(So.ManualRefNo) AS SaleOrderNo,  
                    Max(CASE WHEN Si.V_Type = 'SI' THEN Si.ManualRefNo ELSE NULL END) AS PakkaSaleInvoiceNo,
                    Max(CASE WHEN Pi.V_Type = 'PI' THEN Pi.VendorDocNo ELSE NULL END) AS PakkaPurchaseInvoiceNo,
                    Max(CASE WHEN Si.V_Type = 'WSI' THEN Si.ManualRefNo ELSE NULL END) AS KachhaSaleInvoiceNo,
                    Max(CASE WHEN Pi.V_Type = 'WPI' THEN Pi.VendorDocNo ELSE NULL END) AS KachhaPurchaseInvoiceNo,
                    Max(CASE WHEN Lh.V_Type = 'DNS' THEN Lh.ManualRefNo ELSE NULL END) AS KachhaPurchaseDebitNoteNo
                    FROM SaleInvoiceGeneratedEntries Ge 
                    LEFT JOIN SaleInvoice Si ON Ge.DocId = Si.DocID
                    LEFT JOIN PurchInvoice Pi ON Ge.DocId = Pi.DocID
                    LEFT JOIN LedgerHead Lh ON Ge.DocId = Lh.DocID
                    LEFT JOIN SaleOrder So On Ge.SaleOrderDocId = So.DocId
                    Where Ge.Site_Code = '" & AgL.PubSiteCode & "' 
                    And Ge.Div_Code = '" & AgL.PubDivCode & "'
                    GROUP BY Ge.Code "

        Dim Frmbj As AgLibrary.FrmFind = New AgLibrary.FrmFind(mQry, Me.Text & " Find", AgL)
        Frmbj.ShowDialog()
        AgL.PubSearchRow = AgL.XNull(Frmbj.DGL1.Item(0, Frmbj.DGL1.CurrentRow.Index).Value)
        If AgL.PubSearchRow <> "" Then
            mSearchCode = AgL.PubSearchRow
            MoveRec()
        End If
    End Sub
    Private Sub MoveRec()
        Dgl1.Rows.Clear()
        Dgl2.Rows.Clear()
        Dgl3.Rows.Clear()

        mQry = " Select So.DocId As SaleOrderDocId, So.ManualRefNo As SaleOrderManualRefNo, Sg.Name As SaleToPartyName, L.* 
                From SaleInvoiceGeneratedEntries L
                LEFT JOIN SaleOrder So On L.SaleOrderDocId = So.DocId
                LEFT JOIN SubGroup Sg On So.SaleToParty = Sg.SubCode
                Where Code = '" & mSearchCode & "' "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        TxtOrderNo.Text = AgL.XNull(DtTemp.Rows(0)("SaleOrderManualRefNo"))
        TxtSaleOrderDocId_W.Text = AgL.XNull(DtTemp.Rows(0)("SaleOrderDocId"))
        TxtRemark.Text = AgL.XNull(DtTemp.Rows(0)("Remarks"))

        TxtPartyName.Text = AgL.XNull(DtTemp.Rows(0)("SaleToPartyName"))
        LblApproveBy.Text = AgL.XNull(DtTemp.Rows(0)("ApproveBy")) & IIf(AgL.XNull(DtTemp.Rows(0)("ApproveBy")) <> "", "  " & AgL.XNull(DtTemp.Rows(0)("ApproveDate")), "")
        If LblApproveBy.Text <> "" Then
            BtnApprove.Visible = False
        Else
            BtnApprove.Visible = True
        End If

        mQry = "SELECT SG.Name AS SupplierName, MSg.Name AS MasterSupplierName, Ig.Description AS ItemGroupDesc, L.* 
                FROM WPurchInvoiceDetail L 
                LEFT JOIN Subgroup SG ON L.Supplier = Sg.Subcode
                LEFT JOIN Subgroup MSg ON L.MasterSupplier = Msg.Subcode
                LEFT JOIN ItemGroup Ig ON L.ItemGroup = Ig.Code
                Where L.Code = '" & mSearchCode & "'"
        Dim DtPurchInvoiceDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        For I As Integer = 0 To DtPurchInvoiceDetail.Rows.Count - 1
            Dgl1.Rows.Add()
            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
            Dgl1.Item(Col1SaleInvoiceDocId, I).Value = AgL.XNull(DtPurchInvoiceDetail.Rows(I)("PakkaSaleInvoiceDocId"))
            Dgl1.Item(Col1AddedManuallySr, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("AddedManuallySr"))
            Dgl1.Item(Col1IsThirdPartyBilling, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("IsThirdPartyBilling"))
            Dgl1.Item(Col1Supplier, I).Tag = AgL.XNull(DtPurchInvoiceDetail.Rows(I)("Supplier"))
            Dgl1.Item(Col1Supplier, I).Value = AgL.XNull(DtPurchInvoiceDetail.Rows(I)("SupplierName"))
            Dgl1.Item(Col1MasterSupplier, I).Tag = AgL.XNull(DtPurchInvoiceDetail.Rows(I)("MasterSupplier"))
            Dgl1.Item(Col1MasterSupplier, I).Value = AgL.XNull(DtPurchInvoiceDetail.Rows(I)("MasterSupplierName"))
            Dgl1.Item(Col1InvoiceNo, I).Value = AgL.XNull(DtPurchInvoiceDetail.Rows(I)("InvoiceNo"))
            Dgl1.Item(Col1InvoiceDate, I).Value = AgL.XNull(DtPurchInvoiceDetail.Rows(I)("InvoiceDate"))
            Dgl1.Item(Col1ItemGroup, I).Tag = AgL.XNull(DtPurchInvoiceDetail.Rows(I)("ItemGroup"))
            Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(DtPurchInvoiceDetail.Rows(I)("ItemGroupDesc"))
            Dgl1.Item(Col1InvoiceDiscountPer, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("InvoiceDiscountPer"))
            Dgl1.Item(Col1InvoiceAdditionalDiscountPer, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("InvoiceAdditionalDiscountPer"))
            Dgl1.Item(Col1DiscountPer, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("DiscountPer"))
            Dgl1.Item(Col1AdditionalDiscountPer, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("AdditionalDiscountPer"))
            Dgl1.Item(Col1AdditionPer, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("AdditionPer"))
            Dgl1.Item(Col1Amount, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("Amount"))
            Dgl1.Item(Col1AmountWithoutDiscountAndTax, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("AmountWithoutDiscountAndTax"))
            Dgl1.Item(Col1Tax, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("Tax"))
            Dgl1.Item(Col1CommissionAmount, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("Commission"))
            Dgl1.Item(Col1AdditionalCommissionAmount, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("AdditionalCommission"))
            Dgl1.Item(Col1WInvoiceNo, I).Value = AgL.XNull(DtPurchInvoiceDetail.Rows(I)("WInvoiceNo"))
            Dgl1.Item(Col1WInvoiceDate, I).Value = AgL.XNull(DtPurchInvoiceDetail.Rows(I)("WInvoiceDate"))
            Dgl1.Item(Col1WQty, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("WQty"))
            Dgl1.Item(Col1WFreight, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("WFreight"))
            Dgl1.Item(Col1WPacking, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("WPacking"))
            Dgl1.Item(Col1WAmount, I).Value = AgL.VNull(DtPurchInvoiceDetail.Rows(I)("WAmount"))
            Dgl1.Item(Col1WPurchInvoiceDocId, I).Value = AgL.XNull(DtPurchInvoiceDetail.Rows(I)("WPurchInvoiceDocId"))
            Dgl1.Item(Col1GeneratedDocId, I).Value = AgL.XNull(DtPurchInvoiceDetail.Rows(I)("GeneratedDocId"))

            mQry = " Select ManualRefNo From Purchinvoice Where DocId = '" & Dgl1.Item(Col1GeneratedDocId, I).Value & "'"
            Dim DtPrevDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtPrevDetail.Rows.Count > 0 Then
                Dgl1.Item(Col1GeneratedManualRefNo, I).Value = AgL.XNull(DtPrevDetail.Rows(0)("ManualRefNo"))
            End If
        Next

        mQry = "SELECT SG.Name AS PartyName, MSg.Name AS MasterPartyName, SSg.Name As ShipToPartyName, 
                Ig.Description AS ItemGroupDesc, L.* 
                FROM WSaleInvoiceDetail L 
                LEFT JOIN Subgroup SG ON L.Party = Sg.Subcode
                LEFT JOIN Subgroup MSg ON L.MasterParty = Msg.Subcode
                LEFT JOIN SubGroup SSg On L.ShipToParty = SSg.SubCode
                LEFT JOIN ItemGroup Ig ON L.ItemGroup = Ig.Code
                Where L.Code = '" & mSearchCode & "'"
        Dim DtSaleInvoiceDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dgl2.RowCount = 1 : Dgl2.Rows.Clear()
        For I As Integer = 0 To DtSaleInvoiceDetail.Rows.Count - 1
            Dgl2.Rows.Add()
            Dgl2.Item(ColSNo, I).Value = Dgl2.Rows.Count
            Dgl2.Item(Col2SaleInvoiceDocId, I).Value = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("PakkaSaleInvoiceDocId"))
            Dgl2.Item(Col2AddedManuallySr, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("AddedManuallySr"))
            Dgl2.Item(Col2IsThirdPartyBilling, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("IsThirdPartyBilling"))
            Dgl2.Item(Col2Party, I).Tag = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("Party"))
            Dgl2.Item(Col2Party, I).Value = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("PartyName"))
            Dgl2.Item(Col2MasterParty, I).Tag = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("MasterParty"))
            Dgl2.Item(Col2MasterParty, I).Value = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("MasterPartyName"))
            Dgl2.Item(Col2InvoiceNo, I).Value = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("InvoiceNo"))
            Dgl2.Item(Col2InvoiceDate, I).Value = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("InvoiceDate"))
            Dgl2.Item(Col2ItemGroup, I).Tag = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("ItemGroup"))
            Dgl2.Item(Col2ItemGroup, I).Value = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("ItemGroupDesc"))
            Dgl2.Item(Col2DiscountPer, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("DiscountPer"))
            Dgl2.Item(Col2AdditionalDiscountPer, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("AdditionalDiscountPer"))
            Dgl2.Item(Col2ExtraDiscountPer, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("ExtraDiscountPer"))
            Dgl2.Item(Col2AdditionPer, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("AdditionPer"))
            Dgl2.Item(Col2Amount, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("Amount"))
            Dgl2.Item(Col2AmountWithoutTax, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("AmountWithoutTax"))
            Dgl2.Item(Col2Tax, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("Tax"))
            Dgl2.Item(Col2Discount, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("Discount"))
            Dgl2.Item(Col2ShipToParty, I).Tag = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("ShipToParty"))
            Dgl2.Item(Col2ShipToParty, I).Value = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("ShipToPartyName"))
            Dgl2.Item(Col2WSaleOrderDocId, I).Value = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("WSaleOrderDocId"))
            Dgl2.Item(Col2WInvoiceNo, I).Value = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("WInvoiceNo"))
            Dgl2.Item(Col2WInvoiceDate, I).Value = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("WInvoiceDate"))
            Dgl2.Item(Col2WQty, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("WQty"))
            Dgl2.Item(Col2WFreight, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("WFreight"))
            Dgl2.Item(Col2WPacking, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("WPacking"))
            Dgl2.Item(Col2WDiscount, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("WDiscount"))
            Dgl2.Item(Col2WSaleInvoiceAmount, I).Value = AgL.VNull(DtSaleInvoiceDetail.Rows(I)("WSaleInvoiceAmount"))
            Dgl2.Item(Col2WSaleInvoiceDocId, I).Value = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("WSaleInvoiceDocId"))
            Dgl2.Item(Col2GeneratedDocId, I).Value = AgL.XNull(DtSaleInvoiceDetail.Rows(I)("GeneratedDocId"))
        Next

        mQry = " SELECT SG.Name AS PartyName, LSG.Name As LinkedPartyName, L.* 
                FROM WLedgerHeadDetail L 
                LEFT JOIN Subgroup SG ON L.Party = Sg.Subcode
                LEFT JOIN Subgroup LSG ON L.LinkedParty = LSG.Subcode
                Where L.Code = '" & mSearchCode & "'"
        Dim DtLedgerHeadDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dgl3.RowCount = 1 : Dgl3.Rows.Clear()
        For I As Integer = 0 To DtLedgerHeadDetail.Rows.Count - 1
            Dgl3.Rows.Add()
            Dgl3.Item(ColSNo, I).Value = Dgl3.Rows.Count
            Dgl3.Item(Col3DrCr, I).Value = AgL.XNull(DtLedgerHeadDetail.Rows(I)("DrCr"))
            Dgl3.Item(Col3V_Date, I).Value = AgL.XNull(DtLedgerHeadDetail.Rows(I)("V_Date"))
            Dgl3.Item(Col3Party, I).Tag = AgL.XNull(DtLedgerHeadDetail.Rows(I)("Party"))
            Dgl3.Item(Col3Party, I).Value = AgL.XNull(DtLedgerHeadDetail.Rows(I)("PartyName"))
            Dgl3.Item(Col3LinkedParty, I).Tag = AgL.XNull(DtLedgerHeadDetail.Rows(I)("LinkedParty"))
            Dgl3.Item(Col3LinkedParty, I).Value = AgL.XNull(DtLedgerHeadDetail.Rows(I)("LinkedPartyName"))
            Dgl3.Item(Col3ReasonAc, I).Value = AgL.XNull(DtLedgerHeadDetail.Rows(I)("ReasonAc"))
            Dgl3.Item(Col3Amount, I).Value = AgL.VNull(DtLedgerHeadDetail.Rows(I)("Amount"))
            'Dgl3.Item(Col3SyncedPurchInvoiceDocId, I).Value = AgL.XNull(DtLedgerHeadDetail.Rows(I)("SyncedPurchInvoiceDocId"))
            Dgl3.Item(Col3Remark, I).Value = AgL.XNull(DtLedgerHeadDetail.Rows(I)("Remark"))
        Next

        BtnTransportDetail.Tag = Nothing
        ShowSaleInvoiceHeader(Dgl2.Item(Col2WSaleInvoiceDocId, 0).Value, False)

        Dgl1.ReadOnly = True
        Dgl2.ReadOnly = True
        Dgl3.ReadOnly = True
        TxtOrderNo.Enabled = False
        BtnSave.Enabled = False
        BtnEdit.Enabled = True
    End Sub
    Private Sub BtnFind_Click(sender As Object, e As EventArgs) Handles BtnFind.Click
        Find()
    End Sub
    Private Sub FDelete(SearchCode As String, Conn As Object, Cmd As Object)
        mQry = " Select * From SaleInvoiceGeneratedEntries Where Code = '" & mSearchCode & "' "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = " Delete From WPurchInvoiceDetail Where Code = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = " Delete From WSaleInvoiceDetail Where Code = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        mQry = " Delete From WLedgerHeadDetail Where Code = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        FLockPakkaEntries(SearchCode, Conn, Cmd, "")

        For I As Integer = 0 To DtTemp.Rows.Count - 1
            If AgL.XNull(DtTemp.Rows(I)("Type")) = "Sale Invoice" And AgL.XNull(DtTemp.Rows(I)("V_Type")) = "WSI" Then
                FDeleteSaleInvoice(AgL.XNull(DtTemp.Rows(I)("DocId")), AgL.GCn, AgL.ECmd)
            End If

            If AgL.XNull(DtTemp.Rows(I)("Type")) = "Purchase Invoice" And AgL.XNull(DtTemp.Rows(I)("V_Type")) = "WPI" Then
                FDeletePurchaseInvoice(AgL.XNull(DtTemp.Rows(I)("DocId")), AgL.GCn, AgL.ECmd)
            End If

            If AgL.XNull(DtTemp.Rows(I)("Type")) = "Debit Note" Then
                FDeleteLedgerHeads(AgL.XNull(DtTemp.Rows(I)("DocId")), AgL.GCn, AgL.ECmd)
            End If
        Next

        mQry = "Delete From SaleInvoiceGeneratedEntries Where Code = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub
    Private Sub FDeleteSaleInvoice(bDocId As String, Conn As Object, Cmd As Object)
        mQry = " Delete From SaleInvoiceTrnSetting Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From Stock Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From Ledger Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From SaleInvoiceDimensionDetail Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From SaleInvoiceDetailHelpValues Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From SaleInvoiceTransport Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From SaleInvoiceDetailSku Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From SaleInvoiceDetail Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From TransactionReferences Where ReferenceDocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From SaleInvoice Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Sub FDeletePurchaseInvoice(bDocId As String, Conn As Object, Cmd As Object)
        mQry = " Delete From Stock Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From Ledger Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From PurchInvoiceTransport Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From PurchInvoiceDimensionDetail Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From PurchInvoiceDetailSku Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From PurchInvoiceDetail Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From TransactionReferences Where ReferenceDocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From PurchInvoice Where DocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Sub FDeleteLedgerHeads(bDocId As String, Conn As Object, Cmd As Object)
        mQry = "Delete From Ledger Where DocID = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From LedgerHeadDetail Where DocID = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From TransactionReferences Where ReferenceDocId = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From LedgerHead Where DocID = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From LedgerM Where DocID = '" & bDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        If mSearchCode = "" Then
            MsgBox("No Record Selected...!", MsgBoxStyle.Information)
            Exit Sub
        End If

        mMode = "D"

        Dim dtTemp As DataTable
        mQry = "Select * From Cloth_SupplierSettlementInvoices where PurchaseInvoiceDocId In (Select DocID From SaleInvoiceGeneratedEntries Where Code = '" & mSearchCode & "')"
        dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If dtTemp.Rows.Count > 0 Then
            MsgBox("Settlement Entry for purchase or sales is done. Can't modify Entry")
            Exit Sub
        End If


        mQry = "Select * From Ledger where Clg_Date Is Not Null And DocId In (Select DocID From SaleInvoiceGeneratedEntries Where Code = '" & mSearchCode & "')"
        dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If dtTemp.Rows.Count > 0 Then
            MsgBox("Reconciliation for purchase or sales is done. Can't modify Entry")
            Exit Sub
        End If


        If MsgBox("Are tou sure to delete ? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            Dim mTrans As String = ""
            Try
                AgL.ECmd = AgL.GCn.CreateCommand
                AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                AgL.ECmd.Transaction = AgL.ETrans
                mTrans = "Begin"

                FCreateLog(mSearchCode)
                FDelete(mSearchCode, AgL.GCn, AgL.ECmd)

                AgL.ETrans.Commit()
                mTrans = "Commit"

                BtnTransportDetail.Tag = Nothing
                MsgBox("Record Deleted Successfull...!", MsgBoxStyle.Information)
                BlankText()
            Catch ex As Exception
                AgL.ETrans.Rollback()
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        mMode = "A"
        BlankText()
        BtnSave.Enabled = True
        BtnDelete.Enabled = False
        BtnEdit.Enabled = False
        BtnAdd.Enabled = False

        TxtOrderNo.Enabled = True
        Dgl1.ReadOnly = False
        Dgl2.ReadOnly = False
        Dgl3.ReadOnly = False
        BtnTransportDetail.Tag = Nothing
    End Sub
    Private Sub CopyAttachments(SourceDocId As String, DestinationDocId As String)
        Dim SourceDatabasePath As String = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "ActualDBPath", "")
        Dim SourcePath As String = System.IO.Path.GetDirectoryName(SourceDatabasePath) + "\Images\" + SourceDocId
        'Dim DestinationPath As String = "D:\DesktopApp\trunk\Auditor9\Data\Images\" + DestinationDocId
        Dim DestinationPath As String = PubAttachmentPath + DestinationDocId

        If (Directory.Exists(SourcePath)) Then
            Dim bDirectoryInfo As New DirectoryInfo(SourcePath)
            Dim mFileArr As FileInfo() = bDirectoryInfo.GetFiles()

            If mFileArr.Count = 0 Then Exit Sub

            Dim mFile As FileInfo
            For Each mFile In mFileArr
                'My.Computer.FileSystem.MoveFile(SourcePath + "\" + mFile.Name, DestinationPath + "\" + mFile.Name)
                Dim destinationFileName As String = System.IO.Path.Combine(DestinationPath, mFile.Name)
                My.Computer.FileSystem.CopyFile(SourcePath + "\" + mFile.Name, destinationFileName, True)
                My.Computer.FileSystem.DeleteFile(SourcePath + "\" + mFile.Name)
            Next mFile
            My.Computer.FileSystem.DeleteDirectory(SourcePath, FileIO.DeleteDirectoryOption.DeleteAllContents)
        End If
    End Sub
    Private Function FGetSubCodeFromOMSId(SubCode As String) As String
        Dim DtSubGroupRow As DataRow() = DtSubGroup.Select("OMSId = '" & SubCode & "'")
        If DtSubGroupRow.Length > 0 Then
            FGetSubCodeFromOMSId = DtSubGroupRow(0)("SubCode")
        Else
            FGetSubCodeFromOMSId = ""
        End If
    End Function
    Private Function FGetItemCodeFromOMSId(Code As String) As String
        Dim DtItemRow As DataRow() = DtItem.Select("OMSId = '" & Code & "'")
        If DtItemRow.Length > 0 Then
            FGetItemCodeFromOMSId = DtItemRow(0)("Code")
        Else
            FGetItemCodeFromOMSId = ""
        End If
    End Function
    Private Function FGetOMSIdFromItemCode(Code As String) As String
        Dim DtItemRow As DataRow() = DtItem.Select("Code = '" & Code & "'")
        If DtItemRow.Length > 0 Then
            FGetOMSIdFromItemCode = DtItemRow(0)("OMSId")
        Else
            FGetOMSIdFromItemCode = ""
        End If
    End Function

    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Debug.Print("Before FGetSettings " & AgL.PubStopWatch.ElapsedMilliseconds.ToString)
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, AgL.PubDivCode, AgL.PubSiteCode, VoucherCategory.Sales, Ncat.SaleInvoice, "WSI", "", "")
        FGetSettings = mValue
        Debug.Print("After FGetSettings " & AgL.PubStopWatch.ElapsedMilliseconds.ToString)
    End Function
    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Dim dtTemp As DataTable
        mQry = "Select DocID From SaleInvoiceGeneratedEntries Where Code = '" & mSearchCode & "' And V_Type = 'SI' "
        dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub
    Private Sub BtnPrintW_Click(sender As Object, e As EventArgs) Handles BtnPrintW.Click
        'FGetPrintCrystal(mSearchCode, PrintFor.DocumentPrint, False, "")
        FGetPrintCrystal1(PrintFor.DocumentPrint, False, "")
    End Sub
    Sub FGetPrintCrystal(ByVal SearchCode As String, mPrintFor As ClsMain.PrintFor, Optional ByVal IsPrintToPrinter As Boolean = False, Optional BulkCondStr As String = "")
        Dim mPrintTitle As String
        Dim PrintingCopies() As String
        Dim I As Integer, J As Integer
        Dim sQryPayment As String = ""
        Dim DtDoc As DataTable
        Dim mDocReportFileName As String = ""
        Dim dtTemp As DataTable
        Dim mSaleToParty As String
        Dim mSalesTaxGroupParty As String
        Dim mDocDate As Date




        AgL.PubTempStr = AgL.PubTempStr & "Start Feching basic header detail of document : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
        mQry = "Select H.DocID, H.V_Type, H.Div_Code, H.Site_Code, H.V_date, IfNull(SalesTaxGroupParty,'') as SalesTaxGroupParty, IfNull(SaleToParty,'') as SaleToParty 
                From SaleInvoice H With (NoLock) 
                Left Join SaleInvoiceGeneratedEntries LE ON H.DocID = LE.DocId  
                Where LE.Code = '" & SearchCode & "'"
        DtDoc = AgL.FillData(mQry, AgL.GCn).Tables(0)
        AgL.PubTempStr = AgL.PubTempStr & "End Feching basic header detail of document : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf

        AgL.PubTempStr = AgL.PubTempStr & "Start Feching Printing Desc of voucher Type : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
        mPrintTitle = AgL.Dman_Execute("Select IfNull(PrintingDescription, Description) From Voucher_Type Where V_Type = '" & AgL.XNull(DtDoc.Rows(0)("V_Type")) & "' ", AgL.GCn).ExecuteScalar()
        AgL.PubTempStr = AgL.PubTempStr & "End Feching Printing Desc of voucher Type : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf

        AgL.PubTempStr = AgL.PubTempStr & "Start Feching Doc No Caption Setting : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
        Dim mDocNoCaption As String = FGetSettings(SettingFields.DocumentPrintEntryNoCaption, SettingType.General)
        AgL.PubTempStr = AgL.PubTempStr & "End Feching Doc No Caption Setting : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
        AgL.PubTempStr = AgL.PubTempStr & "Start Feching Doc Date Caption Setting : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
        Dim mDocDateCaption As String = FGetSettings(SettingFields.DocumentPrintEntryDateCaption, SettingType.General)
        AgL.PubTempStr = AgL.PubTempStr & "End Feching Doc Date Caption Setting : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
        AgL.PubTempStr = AgL.PubTempStr & "Start Feching Terms & Cond Setting : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
        Dim mTermsAndConditions As String = FGetSettings(SettingFields.TermsAndConditions, SettingType.General)
        AgL.PubTempStr = AgL.PubTempStr & "End Feching Terms & Cond Setting : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf


        AgL.PubTempStr = AgL.PubTempStr & "Start Feching Party Detail from document Header Table : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf


        If DtDoc.Rows.Count > 0 Then
            mSaleToParty = AgL.XNull(DtDoc.Rows(0)("SaleToParty"))
            mSalesTaxGroupParty = AgL.XNull(DtDoc.Rows(0)("SalesTaxGroupParty"))
        Else
            MsgBox("Party detail can not be fetched for selected invoice. Can't generate print.")
            Exit Sub
        End If
        AgL.PubTempStr = AgL.PubTempStr & "End Feching Party Detail from document Header Table : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf




        If mSalesTaxGroupParty <> AgLibrary.ClsMain.agConstants.PostingGroupSalesTaxParty.Registered Then
            AgL.PubTempStr = AgL.PubTempStr & "Start Feching Unreg party report file name from setting : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            mDocReportFileName = FGetSettings(SettingFields.DocumentPrintReportFileNameUnregisteredParty, SettingType.General)
            AgL.PubTempStr = AgL.PubTempStr & "End Feching Unreg party report file name from setting : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
        End If
        If mDocReportFileName = "" Then
            AgL.PubTempStr = AgL.PubTempStr & "Start Feching report file name from setting : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            mDocReportFileName = FGetSettings(SettingFields.DocumentPrintReportFileName, SettingType.General)
            AgL.PubTempStr = AgL.PubTempStr & "End Feching report file name from setting : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
        End If


        AgL.PubTempStr = AgL.PubTempStr & "Start Feching Show Party Balance In Report from setting : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
        Dim DocumentPrintShowPartyBalance As String = FGetSettings(SettingFields.DocumentPrintShowPartyBalance, SettingType.General)
        AgL.PubTempStr = AgL.PubTempStr & "End Feching Show Party Balance In Report from setting : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf

        Dim mOpeningBalance As Double = 0, mTodaysDr As Double = 0, mTodaysCr As Double = 0, mClosingBalance As Double = 0
        If DocumentPrintShowPartyBalance <> DocumentPrintFieldsVisibilityOptions.Hide Then
            AgL.PubTempStr = AgL.PubTempStr & "Start Feching Party Balance To Print From Ledger Table : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
            mQry = "Select IfNull(Sum(Case When LG.V_Date < " & AgL.Chk_Date(AgL.XNull(DtDoc.Rows(0)("V_Date"))) & " THEN LG.AmtDr-LG.AmtCr ELSE 0 END),0) AS OpeningBalance,
	               IfNull(Sum(CASE WHEN LG.V_Date = " & AgL.Chk_Date(AgL.XNull(DtDoc.Rows(0)("V_Date"))) & " THEN LG.AmtDr ELSE 0 END),0) AS TodaysDr,
	               IfNull(Sum(CASE WHEN LG.V_Date = " & AgL.Chk_Date(AgL.XNull(DtDoc.Rows(0)("V_Date"))) & " THEN LG.AmtCr ELSE 0 END),0) AS TodaysCr,
	               IfNull(Sum(CASE WHEN LG.V_Date <= " & AgL.Chk_Date(AgL.XNull(DtDoc.Rows(0)("V_Date"))) & " THEN LG.AmtDr-LG.AmtCr ELSE 0 END),0) AS ClosingBalance     
                   FROM Ledger LG
                   WHERE LG.SubCode ='" & mSaleToParty & "'"
            dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If dtTemp.Rows.Count > 0 Then
                mOpeningBalance = AgL.VNull(dtTemp.Rows(0)("OpeningBalance"))
                mTodaysDr = AgL.VNull(dtTemp.Rows(0)("TodaysDr"))
                mTodaysCr = AgL.VNull(dtTemp.Rows(0)("TodaysCr"))
                mClosingBalance = AgL.VNull(dtTemp.Rows(0)("ClosingBalance"))
            End If
            AgL.PubTempStr = AgL.PubTempStr & "End Feching Party Balance To Print From Ledger Table : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
        End If


        'If LblV_Type.Tag = Ncat.SaleInvoice Then
        '    AgL.PubTempStr = AgL.PubTempStr & "Start Feching Is Sales Tax Applicable from setting : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
        '    If Not AgL.PubDtDivisionSiteSetting.Rows(0)("IsSalesTaxApplicable") Then
        '        mPrintTitle = "CHALLAN"
        '    Else
        '        mDocNoCaption = "Invoice No."
        '        mDocDateCaption = "Invoice Date"
        '    End If
        '    AgL.PubTempStr = AgL.PubTempStr & "Start Feching Is Sales Tax Applicable from setting : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
        'End If


        AgL.PubTempStr = AgL.PubTempStr & "Start Feching Is Copy Captions from setting : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
        Dim bPrimaryQry As String = ""
        If BulkCondStr <> "" Then
            bPrimaryQry = " Select * From SaleInvoice  With (NoLock) Where DocID In (" & BulkCondStr & ")"
            PrintingCopies = FGetSettings(SettingFields.PrintingBulkCopyCaptions, SettingType.General).ToString.Split(",")
        Else
            bPrimaryQry = " Select * From SaleInvoice  With (NoLock) Where DocID = '" & DtDoc.Rows(0)("DocID") & "'"
            PrintingCopies = FGetSettings(SettingFields.PrintingCopyCaptions, SettingType.General).ToString.Split(",")
        End If
        AgL.PubTempStr = AgL.PubTempStr & "End Feching Is Copy Captions from setting : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf


        'PrintingCopies = AgL.XNull(DtV_TypeSettings.Rows(0)("PrintingCopyCaptions")).ToString.Split(",")

        mQry = ""
        For I = 1 To PrintingCopies.Length
            If mQry <> "" Then mQry = mQry + " Union All "
            '(Case When DP.Prefix Is Not Null Then DP.Prefix || H.ManualRefNo Else H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo End) as InvoiceNo, 
            mQry = mQry + "
                Select '" & I & "' as Copies, '" & AgL.XNull(PrintingCopies(I - 1)) & "' as CopyPrintingCaption, '" & mDocNoCaption & "' as DocNoCaption, '" & mDocDateCaption & "' as DocDateCaption, SiteState.ManualCode as SiteStateCode, SiteState.Description as SiteStateName, H.DocID, L.Sr, H.V_Date, H.DeliveryDate, VT.Description as Voucher_Type, VT.NCat,                                 
                '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo  as InvoiceNo, 
                Gen.ManualRefNo as GenDocNo, H.AmsDocNo, H.AmsDocDate, H.AmsDocNetAmount, IfNull(RT.Description,'Nett') as RateType, 
                '" & FGetSettings(SettingFields.DocumentPrintShowRateType, SettingType.General) & "' as DocumentPrintShowRateType,
                IfNull(Agent.DispName,'') as AgentName, IfNull(SRep.Name,'') as SalesRepName, IfNull(SRep.ManualCode,'') as SalesRepCode, '" & AgL.PubDtEnviro.Rows(0)("Caption_SalesAgent") & "' as AgentCaption,
                (Case When BP.Nature = 'Cash' Then BP.DispName || ' - ' || IsNull(H.SaleToPartyName,'') Else H.SaletoPartyName End) as SaleToPartyName, 
                IfNull(H.SaleToPartyAddress,'') as SaleToPartyAddress, IfNull(C.CityName,'') as CityName, IfNull(H.SaleToPartyPincode,'') as SaleToPartyPincode, 
                IfNull(State.ManualCode,'') as StateCode, IfNull(State.Description,'')  as StateName, 
                IfNull(H.SaleToPartyMobile,'') as SaleToPartyMobile, Sg.ContactPerson, IfNull(H.SaleToPartySalesTaxNo,'') as SaleToPartySalesTaxNo, 
                IfNull(H.SaleToPartyAadharNo,'') as SaleToPartyAadharNo, IfNull(H.SaleToPartyPanNo,'') as SaleToPartyPanNo,
                IfNull((Select RegistrationNo From SubgroupRegistration Where Subcode=H.SaleToParty And RegistrationType = '" & SubgroupRegistrationType.LicenseNo & "'),'') as SaleToPartyLicenseNo,
                (Case When BP.Nature = 'Cash' Then IfNull(SP.DispName, BP.DispName || ' - ' || IsNull(H.SaleToPartyName,'')) Else IfNull(SP.DispName,H.SaletoPartyName) End) as ShipToPartyName,
                (Case When SP.DispName Is Null Then IfNull(H.SaleToPartyAddress,'') Else IfNull(Sp.Address,'') End) as ShipToPartyAddress, 
                (Case When SP.DispName Is Null Then IfNull(C.CityName,'') Else IfNull(SC.CityName,'') End) as ShipToPartyCity, 
                (Case When SP.DispName Is Null Then IfNull(H.SaleToPartyPinCode,'') Else IfNull(Sp.Pin,'') End) as ShipToPartyPincode, 
                (Case When SP.DispName Is Null Then IfNull(State.ManualCode,'') Else IfNull(SS.ManualCode,'') End) as ShipToPartyStateCode, 
                (Case When SP.DispName Is Null Then IfNull(State.Description,'') Else IfNull(SS.Description,'') End) as ShipToPartyStateName, 
                (Case When SP.DispName Is Null Then IfNull(H.SaleToPartyMobile,'') Else IfNull(Sp.Mobile,'') End) as ShipToPartyMobile, 
                (Case When SP.DispName Is Null Then IfNull(H.SaleToPartySalesTaxNo,'') Else IfNull((Select RegistrationNo From SubgroupRegistration Where Subcode=H.ShipToParty And RegistrationType = '" & SubgroupRegistrationType.SalesTaxNo & "'),'') End) as ShipToPartySalesTaxNo, 
                (Case When SP.DispName Is Null Then IfNull(H.SaleToPartyAadharNo,'') Else IfNull((Select RegistrationNo From SubgroupRegistration Where Subcode=H.ShipToParty And RegistrationType = '" & SubgroupRegistrationType.AadharNo & "'),'') End) as ShipToPartyAadharNo, 
                (Case When SP.DispName Is Null Then IfNull(H.SaleToPartyPanNo,'') Else IfNull((Select RegistrationNo From SubgroupRegistration Where Subcode=H.ShipToParty And RegistrationType = '" & SubgroupRegistrationType.PanNo & "'),'') End) as ShipToPartyPanNo, 
                (Case When SP.DispName Is Null Then IfNull((Select RegistrationNo From SubgroupRegistration Where Subcode=H.SaleToParty And RegistrationType = '" & SubgroupRegistrationType.LicenseNo & "'),'') Else IfNull((Select RegistrationNo From SubgroupRegistration Where Subcode=H.ShipToParty And RegistrationType = '" & SubgroupRegistrationType.LicenseNo & "'),'') End) as ShipToPartyLicenseNo, 
                H.ShipToAddress, '" & mTermsAndConditions & "'  TermsAndConditions, IfNull(Transporter.Name,'') as TransporterName, IfNull(TD.LrNo,'') as LrNo, TD.LrDate, TD.NoOfBales, IfNull(TD.PrivateMark,'') PrivateMark, TD.Weight, TD.Freight, TD.ChargedWeight, IfNull(TD.PaymentType,'') as FreightType, 
                IfNull(TD.RoadPermitNo,'') as RoadPermitNo, TD.RoadPermitDate, IfNull(TD.VehicleNo,'') as VehicleNo, IfNull(TD.ShipMethod,'') as ShipMethod, IfNull(TD.PreCarriageBy,'') PreCarriageBy, IfNull(TD.PreCarriagePlace,'') as PreCarriagePlace, IfNull(TD.BookedFrom,'') as BookedFrom, IfNull(TD.BookedTo,'') as BookedTo, IfNull(TD.Destination,'') as Destination, IfNull(TD.DescriptionOfGoods,'') as DescriptionOfGoods, IfNull(TD.DescriptionOfPacking,'') as DescriptionOfPacking, 
                IfNull(L.ReferenceNo,'') as ReferenceNo, Barcode.Description as BarcodeName,
                I.Description as ItemName, " & IIf(mPrintFor = ClsMain.PrintFor.QA, "IG.Description", "IfNull(IG.PrintingDescription,IG.Description)") & " as ItemGroupName, 
                IC.Description as ItemCatName, I.Specification as ItemSpecification, L.Specification as InvoiceLineSpecification, IfNull(I.HSN,IC.HSN) as HSN, I.MaintainStockYn,
                D1.Specification as D1Spec, D2.Specification as D2Spec, D3.Specification as D3Spec, D4.Specification as D4Spec, Size.Specification as SizeSpec,
                IIG.ManualCode as ItemInvoiceGroupCode, IIG.Description as ItemInvoiceGroupDesc,
                '" & AgL.PubCaptionItemType & "' as ItemTypeCaption,'" & AgL.PubCaptionItemCategory & "' as ItemCategoryCaption,
                '" & AgL.PubCaptionItemGroup & "' as ItemGroupCaption,'" & AgL.PubCaptionItem & "' as ItemCaption,'" & AgL.PubCaptionBarcode & "' as BarcodeCaption,
                '" & AgL.PubCaptionDimension1 & "' as D1Caption, '" & AgL.PubCaptionDimension2 & "' as D2Caption, '" & AgL.PubCaptionDimension3 & "' as D3Caption, '" & AgL.PubCaptionDimension4 & "' as D4Caption, 
                L.SalesTaxGroupItem, STGI.GrossTaxRate,  L.MRP, L.LotNo, L.ExpiryDate, I.Remark1 as ItemRemark1, 
                (Case when abs(IfNull(I.MaintainStockYn,1)) =1 AND IfNull(I.ItemType,Ic.ItemType) <> '" & ItemTypeCode.ServiceProduct & "' Then L.Pcs Else 0 End) as Pcs, 
                (Case when abs(IfNull(I.MaintainStockYn,1)) =1 AND IfNull(I.ItemType,Ic.ItemType) <> '" & ItemTypeCode.ServiceProduct & "' Then (Case When Vt.Ncat = '" & Ncat.SaleReturn & "' Then  abs(L.Qty) Else L.Qty End) Else 0 End) as Qty, 
                (Case when abs(IfNull(I.MaintainStockYn,1)) =1 AND IfNull(I.ItemType,Ic.ItemType) <> '" & ItemTypeCode.ServiceProduct & "' Then L.Rate Else 0 End) as Rate, 
                ISS.Description as ItemState, L.Unit, U.DecimalPlaces as UnitDecimalPlaces, 
                L.DiscountPer, L.DiscountAmount, L.AdditionalDiscountPer, L.AdditionalDiscountAmount, L.AdditionPer, L.AdditionAmount, 
                L.DiscountAmount+L.AdditionalDiscountAmount-L.AdditionAmount as TotalDiscount, L.Deal,
                ((Case When Vt.Ncat = '" & Ncat.SaleReturn & "' Then -1.0 else 1.0 end) * L.Amount)+(L.DiscountAmount+L.AdditionalDiscountAmount-L.AdditionAmount) as AmountBeforeDiscount,
                (Case When Vt.Ncat = '" & Ncat.SaleReturn & "' Then -1.0 else 1.0 end) * (L.Amount) as Amount,
                (Case When Vt.Ncat = '" & Ncat.SaleReturn & "' Then -1.0 else 1.0 end) * (L.Taxable_Amount) as Taxable_Amount,
                Abs(L.Tax1_Per) as Tax1_Per, (Case When Vt.Ncat = '" & Ncat.SaleReturn & "' Then -1.0 else 1.0 end) * (L.Tax1) as Tax1, 
                abs(L.Tax2_Per) as Tax2_Per, (Case When Vt.Ncat = '" & Ncat.SaleReturn & "' Then -1.0 else 1.0 end) * (L.Tax2) as Tax2, 
                abs(L.Tax3_Per) as Tax3_Per, (Case When Vt.Ncat = '" & Ncat.SaleReturn & "' Then -1.0 else 1.0 end) * (L.Tax3) as Tax3, 
                abs(L.Tax4_Per) as Tax4_Per, (Case When Vt.Ncat = '" & Ncat.SaleReturn & "' Then -1.0 else 1.0 end) * (L.Tax4) as Tax4, 
                abs(L.Tax5_Per) as Tax5_Per, (Case When Vt.Ncat = '" & Ncat.SaleReturn & "' Then -1.0 else 1.0 end) * (L.Tax5) as Tax5, 
                (Case When Vt.Ncat = '" & Ncat.SaleReturn & "' Then -1.0 else 1.0 end) * (L.Net_Amount) as Net_Amount, 
                L.Remark as LRemarks, IfNull(H.Remarks,'') as HRemarks, H.SalesTaxSummaryStr,
                (Select Sum(L1.DiscountAmount) From SaleInvoiceDetail L1 Where L1.DocID = H.DocID) as H_Discount, 
                (Select Sum(L1.AdditionalDiscountAmount) From SaleInvoiceDetail L1 Where L1.DocID = H.DocID) as H_AdditionalDiscount, 
                (Select Sum(L1.AdditionAmount) From SaleInvoiceDetail L1 Where L1.DocID = H.DocID) as H_Additional, 
                (Select Sum(L1.DiscountAmount+L1.AdditionalDiscountAmount-L1.AdditionAmount) From SaleInvoiceDetail L1 Where L1.DocID = H.DocID) as H_TotalDiscount, 
                (Select Sum(abs(L1.Amount)+L1.DiscountAmount+L1.AdditionalDiscountAmount-L1.AdditionAmount) From SaleinvoiceDetail L1 Where L1.DocID = H.DocId) as H_AmountBeforeDiscount,
                abs(H.Gross_Amount) as H_Gross_Amount, 
                H.SpecialDiscount_Per as H_SpecialDiscount_Per, H.SpecialDiscount as H_SpecialDiscount, Abs(H.Taxable_Amount) as H_Taxable_Amount,
                Abs(H.Tax1_Per) as H_Tax1_Per, Abs(H.Tax1) as H_Tax1, H.Tax2_Per as H_Tax2_Per, abs(H.Tax2) as H_Tax2, 
                H.Tax3_Per as H_Tax3_Per, abs(H.Tax3) as H_Tax3, H.Tax4_Per as H_Tax4_Per, abs(H.Tax4) as H_Tax4, 
                H.Tax5_Per as H_Tax5_Per, abs(H.Tax5) as H_Tax5, H.Deduction_Per as H_Deduction_Per, H.Deduction as H_Deduction, 
                H.Other_Charge_Per as H_Other_Charge_Per, H.Other_Charge as H_Other_Charge, 
                H.Other_Charge1_Per as H_Other_Charge1_Per, H.Other_Charge1 as H_Other_Charge1, 
                H.Other_Charge2_Per as H_Other_Charge2_Per, H.Other_Charge2 as H_Other_Charge2, 
                H.Round_Off, abs(H.Net_Amount) as H_Net_Amount, 
                '" & AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_BankAccountDetail")) & "' as Default_BankAccountDetail,
                '" & FGetSettings(SettingFields.DocumentPrintHeaderPattern, SettingType.General) & "' as DocumentPrintHeaderPattern, IfNull(L.DimensionDetail,'') as DimDetail,
                '" & AgL.PubUserName & "' as PrintedByUser, H.EntryBy as EntryByUser, '" & mPrintTitle & "' as PrintTitle,
                '" & FGetSettings(SettingFields.DocumentPrintShowPrintDateTimeYn, SettingType.General) & "' as DocumentPrintShowPrintDateTimeYn,
                '" & DocumentPrintShowPartyBalance & "' as DocumentPrintShowPartyBalance, 
                " & mOpeningBalance & " as TodaysOpeningBalance, " & mTodaysDr & " as TodaysDr, " & mTodaysCr & " as TodaysCr, " & mClosingBalance & " as TodaysClosingBalance
                from (" & bPrimaryQry & ") as H
                Left Join SaleInvoiceTrnSetting TS  With (NoLock) On H.DocId = TS.DocID
                Left Join SaleInvoiceDetail L  With (NoLock) On H.DocID = L.DocID
                Left Join SaleInvoiceDetailSku LS  With (NoLock) On LS.DocID = L.DocID And LS.Sr = L.Sr
                Left Join Item I  With (NoLock) On LS.Item = I.Code
                Left Join Item D1  With (NoLock) On LS.Dimension1 = D1.Code
                Left Join Item D2  With (NoLock) On LS.Dimension2 = D2.Code
                Left Join Item D3  With (NoLock) On LS.Dimension3 = D3.Code
                Left Join Item D4  With (NoLock) On LS.Dimension4 = D4.Code   
                Left Join Item size  With (NoLock) On LS.Size = Size.Code
                Left Join Unit U  With (NoLock) On I.Unit = U.Code
                Left Join Item IG  With (NoLock) On LS.ItemGroup = IG.Code
                Left Join Item IC  With (NoLock) On LS.ItemCategory = IC.Code
                Left Join Item ISS On L.ItemState = ISS.Code
                Left Join Item IIG On LS.ItemInvoiceGroup = IIG.Code
                Left Join City C  With (NoLock) On H.SaleToPartyCity = C.CityCode
                Left Join State  With (NoLock) On C.State = State.Code
                Left Join SaleInvoiceTransport TD  With (NoLock) On H.DocID = TD.DocID
                Left Join ViewHelpSubgroup Transporter  With (NoLock) On TD.Transporter= Transporter.Code
                Left Join PostingGroupSalesTaxItem STGI  With (NoLock) On L.SalesTaxGroupItem = STGI.Description
                Left Join Subgroup Sg  With (NoLock) On H.SaleToParty = Sg.Subcode
                Left Join Subgroup BP With (NoLock) On H.BillToParty = BP.Subcode
                Left Join Subgroup SP With (NoLock) On H.ShipToParty = SP.Subcode
                Left Join Subgroup SRep With (NoLock) on L.SalesRepresentative  = SRep.Subcode
                Left Join City SC With (NoLock) On SP.CityCode = SC.CityCode
                Left Join State SS with (NoLock) On SC.State = SS.Code
                Left Join RateType RT  With (NoLock) on H.RateType = Rt.Code
                Left Join Subgroup Agent  With (NoLock) On H.Agent = Agent.Subcode
                Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type
                Left Join DocumentPrefix DP On VT.Category = DP.Category And H.Div_Code = DP.Div_Code                
                Left Join SiteMast Site On H.Site_Code = Site.Code
                Left Join City SiteCity On Site.City_Code = SiteCity.CityCode
                Left Join State SiteState On SiteCity.State = SiteState.Code
                Left Join Barcode With (NoLock) On Barcode.Code = L.Barcode
                Left Join SaleInvoice Gen With (NoLock) On H.GenDocID = Gen.DocId
                "


            'If ClsMain.IsScopeOfWorkContains(IndustryType.SubIndustryType.RetailModule) Then
            If sQryPayment <> "" Then sQryPayment = sQryPayment + " Union All "

            sQryPayment = sQryPayment + "Select '" & I & "' as Copies, H.DocID,
                                    H.Sr, PM.Description AS PaymentModeName, H.Amount, H.ReferenceNo  
                                    FROM SaleInvoicePayment H
                                    LEFT JOIN PaymentMode PM ON H.PaymentMode = PM.Code 
                                    WHERE H.DocID ='" & mSearchCode & "'                                   
                                  "
            'End If

        Next
        mQry = mQry + " Order By Copies, H.DocID, L.Sr "


        Dim objRepPrint As Object
        If mPrintFor = ClsMain.PrintFor.EMail Then
            objRepPrint = New AgLibrary.FrmMailComposeWithCrystal(AgL)
            'objRepPrint.TxtToEmail.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Email
            '        From SaleInvoice H  With (NoLock)
            '        LEFT JOIN SubGroup Sg  With (NoLock) On H.SaleToParty = Sg.SubCode
            '        Where H.DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
            'objRepPrint.TxtCcEmail.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Email
            '        From SaleInvoice H  With (NoLock)
            '        LEFT JOIN SubGroup Sg  With (NoLock) On H.Agent = Sg.SubCode
            '        Where H.DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
            'FGetMailConfiguration(objRepPrint, SearchCode)
            'objRepPrint.AttachmentName = "Invoice"
        Else
            objRepPrint = New AgLibrary.RepView(AgL)
        End If


        Dim sQry As String = ""
        Dim sQryRepName As String = ""

        If sQryPayment <> "" Then
            If sQry <> "" Then sQry = sQry & "^"
            If sQryRepName <> "" Then sQryRepName = sQryRepName & "^"
            sQry += sQryPayment
            sQryRepName += "PaymentDetail"
        End If


        AgL.PubTempStr = AgL.PubTempStr & "Start FPrintThisDocument Function : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf
        If mDocReportFileName = "" Then
            ClsMain.FPrintThisDocument(Me, objRepPrint, "WSI", mQry, "SaleInvoice_Print.rpt", mPrintTitle, , sQry, sQryRepName, "", "", IsPrintToPrinter, AgL.XNull(DtDoc.Rows(0)("Div_Code")), AgL.XNull(DtDoc.Rows(0)("Site_Code")))
        Else
            ClsMain.FPrintThisDocument(Me, objRepPrint, "WSI", mQry, mDocReportFileName, mPrintTitle, , sQry, sQryRepName, "", "", IsPrintToPrinter, AgL.XNull(DtDoc.Rows(0)("Div_Code")), AgL.XNull(DtDoc.Rows(0)("Site_Code")))
        End If
        AgL.PubTempStr = AgL.PubTempStr & "End FPrintThisDocument Function : " & AgL.PubStopWatch.ElapsedMilliseconds.ToString & vbCrLf

    End Sub
    Sub FGetPrintCrystal1(mPrintFor As ClsMain.PrintFor, Optional ByVal IsPrintToPrinter As Boolean = False, Optional BulkCondStr As String = "")
        Try
            mQry = "Select LE.Code, 'W' AS ATYPE, SM.Name as SiteName,  H.DocID, L.Sr, Null as InvoiceNo, Max(H.ManualRefNo) AS InvoiceNoW, 
                    H.V_Date, Max(Sp.Name) as SaleToPartyName, Max(Sg.DispName) AS PartyName, Max(Sg.ManualCode) AS PartyCode, Max(Sg.Address) Address, 
                    Max(c.CityName) AS CityName, Max(spp.Name) as ShipToPartyName,
                    Max(I.Description) As Brand, 
                    (select sPI.VendorDocNo from purchInvoice sPI 
                    Left Join PurchInvoiceDetail sPIL On sPI.DocID = sPIL.DocId                    
                    where sPI.DocId In (Select DocID from SaleInvoiceGeneratedEntries Where Code='" & mSearchCode & "' And V_Type='WPI' )
                    And sPIL.Item = L.Item) as PInvNo, 
                    Sum(Case When I.ItemType <> '" & ItemTypeCode.ServiceProduct & "' Then L.Qty else 0 End) as Qty, sUM(L.Amount)*0.01 As Amount,
                    Sum(L.Amount + (L.DiscountAmount + L.AdditionalDiscountAmount - L.AdditionAmount ))*0.01 As GoodsValue, 
                    (Case When Max(L.DiscountPer)>0 Then Cast(PrintF('%.2f',Max(L.DiscountPer)) as VarChar) || ' Per Pcs ' Else '' End) || 
                    (Case When Max(L.AdditionalDiscountPer)>0 Then Cast(PrintF('%.2f',Max(L.AdditionalDiscountPer)) as Varchar) || ' % ' Else '' End) || 
                    (Case When Max(L.ExtraDiscountPer)>0 Then Cast(PrintF('%.2f',Max(L.ExtraDiscountPer)) as Varchar) || ' Ex% ' Else '' End) || 
                    (Case When Max(L.AdditionPer)>0 Then Cast(PrintF('%.2f',Max(L.AdditionPer)) as Varchar) || ' Aadhat%' Else '' End) As DiscountPer,
                    Sum(L.DiscountAmount + L.AdditionalDiscountAmount + L.ExtraDiscountAmount - L.AdditionAmount)*0.01 As TotalDiscount, 
                    Sum(L.Other_Charge)*0.01 as OtherCharge, Sum(L.Other_Charge1)*0.01 as OtherCharge1,
                    Sum(L.Tax1 + L.Tax2 + L.Tax3 + L.Tax4_Per + L.Tax5)*0.01 As Tax,                    
                    Sum(L.Net_Amount)*0.01 As NetAmount, IfNull(Max(L.Remark),'') as LRemarks, Max(Tr.DispName) As TransportName, Max(SIT.LrNo) As LRNO, strftime('%d-%m-%Y',Max(SIT.LrDate)) As LRDate, 
                    Max(SIT.NoOfBales) As NOOfBales, Max(SIT.PrivateMark) As PrivateMark , Max(SIT.BookedFrom) BookedFrom, Max(SIT.Destination) As Destination,
                    '" & AgL.PubUserName & "' as PrintedByUser, H.EntryBy as EntryByUser, H.EntryDate as UserEntryDate     
                    From SaleInvoice H 
                    Left Join SaleInvoiceDetail L ON H.DocID = L.DocID 
                    Left Join Item I ON L.Item = I.Code 
                    Left Join Item IG ON I.ItemGroup = IG.Code 
                    Left Join viewHelpSubgroup Sp ON H.SaleToParty = Sp.Code  
                    Left Join Subgroup Sg ON H.BillToParty = Sg.Subcode 
                    Left Join viewHelpSubgroup Spp ON H.ShipToParty = Spp.Code  
                    Left Join City C ON Sg.CityCode = C.CityCode 
                    Left Join SaleInvoiceTransport SIT ON H.DocID = SIT.DocID 
                    Left Join Subgroup Tr ON SIT.Transporter = Tr.Subcode 
                    Left Join SaleInvoiceGeneratedEntries LE ON H.DocID = LE.DocId 
                    Left Join SiteMast SM On H.Site_Code= Sm.Code
                    WHERE H.V_Type ='WSI' And H.Net_Amount>0 And LE.Code ='" & mSearchCode & "'
                    GROUP BY H.DocID, IG.Description, L.Sr                     
                    ORDER BY LE.Code, H.DocID, L.Sr
                   "

            Dim objRepPrint As Object
            If mPrintFor = ClsMain.PrintFor.EMail Then
                objRepPrint = New AgLibrary.FrmMailComposeWithCrystal(AgL)
            Else
                objRepPrint = New AgLibrary.RepView()
            End If


            ClsMain.FPrintThisDocument(objRepPrint, objRepPrint, "", mQry, "SaleInvoice_Print_AadhatW2.rpt", "CHALLAN", , , , "", AgL.PubLoginDate, IsPrintToPrinter)
        Catch ex As Exception
            MsgBox(ex.Message & "  In FGetPrintCrysal Procedure of ClsMasterPartyLedgerAadhat")
        End Try
    End Sub

    Private Sub BtnTransportDetail_Click(sender As Object, e As EventArgs) Handles BtnTransportDetail.Click
        If Dgl2.Rows.Count > 0 Then
            ShowSaleInvoiceHeader(Dgl2.Item(Col2WSaleInvoiceDocId, 0).Value)
        Else
            ShowSaleInvoiceHeader()
        End If
    End Sub

    Private Sub ShowSaleInvoiceHeader(Optional DocId As String = "", Optional ShowDialog As Boolean = True)
        If BtnTransportDetail.Tag IsNot Nothing Then
            CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).EntryMode = "Add"
            CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Ncat = Ncat.SaleInvoice
            If ShowDialog = True Then BtnTransportDetail.Tag.ShowDialog()
        Else
            Dim FrmObj As FrmSaleInvoiceTransport
            FrmObj = New FrmSaleInvoiceTransport
            FrmObj.Ncat = Ncat.SaleInvoice
            FrmObj.IniGrid(DocId)
            FrmObj.EntryMode = "Add"

            BtnTransportDetail.Tag = FrmObj
            If ShowDialog = True Then BtnTransportDetail.Tag.ShowDialog()
        End If
    End Sub

    Private Sub FPostUIValues(SearchCode As String, Conn As Object, Cmd As Object)
        Dim mSr As Integer = 0
        For I As Integer = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1WPurchInvoiceDocId, I).Value <> "" Then
                mSr += 1
                mQry = "INSERT INTO WPurchInvoiceDetail (Code, Sr, PakkaSaleInvoiceDocId, AddedManuallySr, IsThirdPartyBilling, Supplier, InvoiceNo, InvoiceDate, ItemGroup, InvoiceDiscountPer, InvoiceAdditionalDiscountPer, Tax, DiscountPer, AdditionalDiscountPer, AdditionPer, Amount, AmountWithoutDiscountAndTax, Commission, AdditionalCommission, MasterSupplier, WInvoiceNo, WInvoiceDate, WQty, WFreight, WPacking, WAmount, WPurchInvoiceDocId, GeneratedDocId)
                Select " & AgL.Chk_Text(SearchCode) & " As Code,  " & mSr & " As Sr, 
                " & AgL.Chk_Text(Dgl1.Item(Col1SaleInvoiceDocId, I).Value) & " As PakkaSaleInvoiceDocId, 
                " & Val(Dgl1.Item(Col1AddedManuallySr, I).Value) & " As AddedManuallySr, 
                " & Val(Dgl1.Item(Col1IsThirdPartyBilling, I).Value) & " As IsThirdPartyBilling, 
                " & AgL.Chk_Text(Dgl1.Item(Col1Supplier, I).Tag) & " As Supplier, 
                " & AgL.Chk_Text(Dgl1.Item(Col1InvoiceNo, I).Value) & " As InvoiceNo, 
                " & AgL.Chk_Date(Dgl1.Item(Col1InvoiceDate, I).Value) & " As InvoiceDate, 
                " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, I).Tag) & " As ItemGroup, 
                " & Val(Dgl1.Item(Col1InvoiceDiscountPer, I).Value) & " As InvoiceDiscountPer, 
                " & Val(Dgl1.Item(Col1InvoiceAdditionalDiscountPer, I).Value) & " As InvoiceAdditionalDiscountPer, 
                " & Val(Dgl1.Item(Col1Tax, I).Value) & " As Tax, 
                " & Val(Dgl1.Item(Col1DiscountPer, I).Value) & " As DiscountPer, 
                " & Val(Dgl1.Item(Col1AdditionalDiscountPer, I).Value) & " As AdditionalDiscountPer, 
                " & Val(Dgl1.Item(Col1AdditionPer, I).Value) & " As AdditionPer, 
                " & Val(Dgl1.Item(Col1Amount, I).Value) & " As Amount, 
                " & Val(Dgl1.Item(Col1AmountWithoutDiscountAndTax, I).Value) & " As AmountWithoutDiscountAndTax, 
                " & Val(Dgl1.Item(Col1CommissionAmount, I).Value) & " As Commission, 
                " & Val(Dgl1.Item(Col1AdditionalCommissionAmount, I).Value) & " As AdditionalCommission, 
                " & AgL.Chk_Text(Dgl1.Item(Col1MasterSupplier, I).Tag) & " As MasterSupplier, 
                " & AgL.Chk_Text(Dgl1.Item(Col1WInvoiceNo, I).Value) & " As WInvoiceNo, 
                " & AgL.Chk_Date(Dgl1.Item(Col1WInvoiceDate, I).Value) & " As WInvoiceDate, 
                " & Val(Dgl1.Item(Col1WQty, I).Value) & " As WQty, 
                " & Val(Dgl1.Item(Col1WFreight, I).Value) & " As WFreight, 
                " & Val(Dgl1.Item(Col1WPacking, I).Value) & " As WPacking, 
                " & Val(Dgl1.Item(Col1WAmount, I).Value) & " As WAmount, 
                " & AgL.Chk_Text(Dgl1.Item(Col1WPurchInvoiceDocId, I).Value) & " As WPurchInvoiceDocId,
                " & AgL.Chk_Text(Dgl1.Item(Col1GeneratedDocId, I).Value) & " As GeneratedDocId "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next

        mSr = 0
        For I As Integer = 0 To Dgl2.Rows.Count - 1
            If Dgl2.Item(Col2WSaleInvoiceDocId, I).Value <> "" Then
                mSr += 1
                mQry = "INSERT INTO WSaleInvoiceDetail (Code, Sr, PakkaSaleInvoiceDocId, AddedManuallySr, IsThirdPartyBilling, Party, InvoiceNo, InvoiceDate, ItemGroup, DiscountPer, AdditionalDiscountPer, ExtraDiscountPer, AdditionPer, Amount, AmountWithoutTax, Tax, Discount, MasterParty, ShipToParty, WSaleOrderDocId, WInvoiceNo, WInvoiceDate, WQty, WFreight, WPacking, WDiscount, WSaleInvoiceAmount, WSaleInvoiceDocId, GeneratedDocId)
                    Select " & AgL.Chk_Text(SearchCode) & " As Code,  " & mSr & " As Sr, 
                    " & AgL.Chk_Text(Dgl2.Item(Col2SaleInvoiceDocId, I).Value) & " As PakkaSaleInvoiceDocId, 
                    " & Val(Dgl2.Item(Col2AddedManuallySr, I).Value) & " As AddedManuallySr, 
                    " & Val(Dgl2.Item(Col2IsThirdPartyBilling, I).Value) & " As IsThirdPartyBilling, 
                    " & AgL.Chk_Text(Dgl2.Item(Col2Party, I).Tag) & " As Party, 
                    " & AgL.Chk_Text(Dgl2.Item(Col2InvoiceNo, I).Value) & " As InvoiceNo, 
                    " & AgL.Chk_Date(Dgl2.Item(Col2InvoiceDate, I).Value) & " As InvoiceDate, 
                    " & AgL.Chk_Text(Dgl2.Item(Col2ItemGroup, I).Tag) & " As ItemGroup, 
                    " & Val(Dgl2.Item(Col2DiscountPer, I).Value) & " As DiscountPer, 
                    " & Val(Dgl2.Item(Col2AdditionalDiscountPer, I).Value) & " As AdditionalDiscountPer, 
                    " & Val(Dgl2.Item(Col2ExtraDiscountPer, I).Value) & " As ExtraDiscountPer, 
                    " & Val(Dgl2.Item(Col2AdditionPer, I).Value) & " As AdditionPer, 
                    " & Val(Dgl2.Item(Col2Amount, I).Value) & " As Amount, 
                    " & Val(Dgl2.Item(Col2AmountWithoutTax, I).Value) & " As AmountWithoutTax, 
                    " & Val(Dgl2.Item(Col2Tax, I).Value) & " As Tax, 
                    " & Val(Dgl2.Item(Col2Discount, I).Value) & " As Discount, 
                    " & AgL.Chk_Text(Dgl2.Item(Col2MasterParty, I).Tag) & " As MasterParty, 
                    " & AgL.Chk_Text(Dgl2.Item(Col2ShipToParty, I).Tag) & " As ShipToParty, 
                    " & AgL.Chk_Text(Dgl2.Item(Col2WSaleOrderDocId, I).Value) & " As WSaleOrderDocId, 
                    " & AgL.Chk_Text(Dgl2.Item(Col2WInvoiceNo, I).Value) & " As WInvoiceNo, 
                    " & AgL.Chk_Date(Dgl2.Item(Col2WInvoiceDate, I).Value) & " As WInvoiceDate, 
                    " & Val(Dgl2.Item(Col2WQty, I).Value) & " As WQty, 
                    " & Val(Dgl2.Item(Col2WFreight, I).Value) & " As WFreight, 
                    " & Val(Dgl2.Item(Col2WPacking, I).Value) & " As WPacking, 
                    " & Val(Dgl2.Item(Col2WDiscount, I).Value) & " As WDiscount, 
                    " & Val(Dgl2.Item(Col2WSaleInvoiceAmount, I).Value) & " As WSaleInvoiceAmount ,
                    " & AgL.Chk_Text(Dgl2.Item(Col2WSaleInvoiceDocId, I).Value) & " As WSaleInvoiceDocId,
                    " & AgL.Chk_Text(Dgl2.Item(Col2GeneratedDocId, I).Value) & " As GeneratedDocId "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next

        mSr = 0
        For I As Integer = 0 To Dgl3.Rows.Count - 1
            mSr += 1
            mQry = "INSERT INTO WLedgerHeadDetail (Code, Sr, DrCr, V_Date, Party, LinkedParty, ReasonAc, Amount, Remark)
                    Select " & AgL.Chk_Text(SearchCode) & " As Code,  " & mSr & " As Sr, 
                    " & AgL.Chk_Text(Dgl3.Item(Col3DrCr, I).Value) & " As DrCr, 
                    " & AgL.Chk_Date(Dgl3.Item(Col3V_Date, I).Value) & " As V_Date, 
                    " & AgL.Chk_Text(Dgl3.Item(Col3Party, I).Tag) & " As Party, 
                    " & AgL.Chk_Text(Dgl3.Item(Col3LinkedParty, I).Tag) & " As LinkedParty, 
                    " & AgL.Chk_Text(Dgl3.Item(Col3ReasonAc, I).Value) & " As ReasonAc, 
                    " & Val(Dgl3.Item(Col3Amount, I).Value) & " As Amount, 
                    " & AgL.Chk_Text(Dgl3.Item(Col3Remark, I).Value) & " As Remark "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        Next
    End Sub
    Private Sub Dgl2_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl2.CellEnter
        Try
            If Dgl2.CurrentCell Is Nothing Then Exit Sub

            Select Case Dgl2.Columns(Dgl2.CurrentCell.ColumnIndex).Name
                Case Col2InvoiceNo, Col2InvoiceDate, Col2Tax, Col2Amount
                    If Val(Dgl2.Item(Col2AddedManuallySr, Dgl2.CurrentCell.RowIndex).Value) = 0 And
                        Val(Dgl2.Item(Col2IsThirdPartyBilling, Dgl2.CurrentCell.RowIndex).Value) = 0 Then
                        Dgl2.Item(Dgl2.CurrentCell.ColumnIndex, Dgl2.CurrentCell.RowIndex).ReadOnly = True
                    Else
                        Dgl2.Item(Dgl2.CurrentCell.ColumnIndex, Dgl2.CurrentCell.RowIndex).ReadOnly = False
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl2_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles Dgl2.EditingControl_Validating
        Try
            If Dgl2.CurrentCell Is Nothing Then Exit Sub

            Select Case Dgl2.Columns(Dgl2.CurrentCell.ColumnIndex).Name
                Case Col2WInvoiceNo
                    If Dgl2.Item(Col2InvoiceDate, Dgl2.CurrentCell.RowIndex).Value <> "" And
                            Dgl2.Item(Col2WInvoiceNo, Dgl2.CurrentCell.RowIndex).Value <> "" Then
                        Dgl2.Item(Col2WInvoiceDate, Dgl2.CurrentCell.RowIndex).Value = Dgl2.Item(Col2InvoiceDate, Dgl2.CurrentCell.RowIndex).Value
                    End If


                    'For I As Integer = 0 To Dgl2.Rows.Count - 1
                    '    If Dgl2.Item(Col2SaleInvoiceDocId, I).Value = Dgl2.Item(Col2SaleInvoiceDocId, Dgl2.CurrentCell.RowIndex).Value Then
                    '        Dgl2.Item(Col2WInvoiceNo, I).Value = Dgl2.Item(Col2WInvoiceNo, Dgl2.CurrentCell.RowIndex).Value
                    '        Dgl2.Item(Col2WInvoiceDate, I).Value = Dgl2.Item(Col2WInvoiceDate, Dgl2.CurrentCell.RowIndex).Value
                    '    End If
                    'Next
            End Select
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub BtnApprove_Click(sender As Object, e As EventArgs) Handles BtnApprove.Click
        mQry = "Update SaleInvoiceGeneratedEntries set ApproveBy = '" & AgL.PubUserName & "', ApproveDate = " & AgL.Chk_DateTime(Now()) & " Where Code = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        mQry = " Select L.ApproveBy, L.ApproveDate 
                From SaleInvoiceGeneratedEntries L
                Where Code = '" & mSearchCode & "' "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        LblApproveBy.Text = AgL.XNull(DtTemp.Rows(0)("ApproveBy")) & IIf(AgL.XNull(DtTemp.Rows(0)("ApproveBy")) <> "", "  " & AgL.XNull(DtTemp.Rows(0)("ApproveDate")), "")
        If LblApproveBy.Text <> "" Then
            BtnApprove.Visible = False
        Else
            BtnApprove.Visible = True
        End If
    End Sub
    Private Sub BtnAddItem_Click(sender As Object, e As EventArgs) Handles BtnAddItem.Click
        Dim I As Integer = 0

        Dim bAddedManualSr As Integer = 0
        For I = 0 To Dgl1.Rows.Count - 1
            bAddedManualSr = Val(Dgl1.Item(Col1AddedManuallySr, I).Value) + 1
        Next

        Dgl1.Rows.Add()
        I = Dgl1.Rows.Count - 1
        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
        Dgl1.Item(Col1AddedManuallySr, I).Value = bAddedManualSr
        Dgl1.Item(Col1SaleInvoiceDocId, I).Value = Dgl1.Item(Col1SaleInvoiceDocId, I - 1).Value
        Dgl1.Item(Col1PurchInvoiceDocId, I).Value = Dgl1.Item(Col1PurchInvoiceDocId, I - 1).Value
        Dgl1.Item(Col1Supplier, I).Tag = Dgl1.Item(Col1Supplier, I - 1).Tag
        Dgl1.Item(Col1Supplier, I).Value = Dgl1.Item(Col1Supplier, I - 1).Value
        Dgl1.Item(Col1MasterSupplier, I).Tag = Dgl1.Item(Col1MasterSupplier, I - 1).Tag
        Dgl1.Item(Col1MasterSupplier, I).Value = Dgl1.Item(Col1MasterSupplier, I - 1).Value
        Dgl1.Item(Col1InvoiceNo, I).Value = Dgl1.Item(Col1InvoiceNo, I - 1).Value
        Dgl1.Item(Col1InvoiceDate, I).Value = Dgl1.Item(Col1InvoiceDate, I - 1).Value
        Dgl1.Item(Col1ItemGroup, I).Tag = Dgl1.Item(Col1ItemGroup, I - 1).Tag
        Dgl1.Item(Col1ItemGroup, I).Value = Dgl1.Item(Col1ItemGroup, I - 1).Value
        Dgl1.Item(Col1InvoiceDiscountPer, I).Value = Dgl1.Item(Col1InvoiceDiscountPer, I - 1).Value
        Dgl1.Item(Col1InvoiceAdditionalDiscountPer, I).Value = Dgl1.Item(Col1InvoiceAdditionalDiscountPer, I - 1).Value
        Dgl1.Item(Col1Amount, I).Value = Dgl1.Item(Col1Amount, I - 1).Value
        Dgl1.Item(Col1AmountWithoutDiscountAndTax, I).Value = Dgl1.Item(Col1AmountWithoutDiscountAndTax, I - 1).Value
        Dgl1.Item(Col1DiscountPer, I).Value = Dgl1.Item(Col1DiscountPer, I - 1).Value
        Dgl1.Item(Col1AdditionalDiscountPer, I).Value = Dgl1.Item(Col1AdditionalDiscountPer, I - 1).Value
        Dgl1.Item(Col1AdditionPer, I).Value = Dgl1.Item(Col1AdditionPer, I - 1).Value


        Dgl2.Rows.Add()
        I = Dgl2.Rows.Count - 1
        Dgl2.Item(ColSNo, I).Value = Dgl2.Rows.Count
        Dgl2.Item(Col2AddedManuallySr, I).Value = bAddedManualSr
        Dgl2.Item(Col2SaleInvoiceDocId, I).Value = Dgl2.Item(Col2SaleInvoiceDocId, I - 1).Value
        Dgl2.Item(Col2SaleInvoiceDocId, I).Value = Dgl2.Item(Col2SaleInvoiceDocId, I - 1).Value
        Dgl2.Item(Col2WSaleOrderDocId, I).Value = Dgl2.Item(Col2WSaleOrderDocId, I - 1).Value
        Dgl2.Item(Col2Party, I).Tag = Dgl2.Item(Col2Party, I - 1).Tag
        Dgl2.Item(Col2Party, I).Value = Dgl2.Item(Col2Party, I - 1).Value
        Dgl2.Item(Col2MasterParty, I).Tag = Dgl2.Item(Col2MasterParty, I - 1).Tag
        Dgl2.Item(Col2MasterParty, I).Value = Dgl2.Item(Col2MasterParty, I - 1).Value
        Dgl2.Item(Col2InvoiceNo, I).Value = Dgl2.Item(Col2InvoiceNo, I - 1).Value
        Dgl2.Item(Col2InvoiceDate, I).Value = Dgl2.Item(Col2InvoiceDate, I - 1).Value
        Dgl2.Item(Col2ItemGroup, I).Tag = Dgl2.Item(Col2ItemGroup, I - 1).Tag
        Dgl2.Item(Col2ItemGroup, I).Value = Dgl2.Item(Col2ItemGroup, I - 1).Value
        Dgl2.Item(Col2DiscountPer, I).Value = Dgl2.Item(Col2DiscountPer, I - 1).Value
        Dgl2.Item(Col2AdditionalDiscountPer, I).Value = Dgl2.Item(Col2AdditionalDiscountPer, I - 1).Value
        Dgl2.Item(Col2ExtraDiscountPer, I).Value = Dgl2.Item(Col2ExtraDiscountPer, I - 1).Value
        Dgl2.Item(Col2Amount, I).Value = Dgl2.Item(Col2Amount, I - 1).Value
        Dgl2.Item(Col2AmountWithoutTax, I).Value = Dgl2.Item(Col2AmountWithoutTax, I - 1).Value
        Dgl2.Item(Col2DiscountPer, I).Value = Dgl2.Item(Col2DiscountPer, I - 1).Value
        Dgl2.Item(Col2AdditionalDiscountPer, I).Value = Dgl2.Item(Col2AdditionalDiscountPer, I - 1).Value
        Dgl2.Item(Col2ExtraDiscountPer, I).Value = Dgl2.Item(Col2ExtraDiscountPer, I - 1).Value
        Dgl2.Item(Col2AdditionPer, I).Value = Dgl2.Item(Col2AdditionPer, I - 1).Value
    End Sub
    Private Sub FCopyTransportDetail(DocId As String)
        mQry = "SELECT H.*, Transporter.Name as TransporterName
                    FROM SaleInvoiceTransport H                      
                    LEFT JOIN viewHelpSubgroup Transporter On H.Transporter = Transporter.Code 
                    WHERE H.DocId = '" & DocId & "' "
        Dim DtTransport As DataTable = AgL.FillData(mQry, Connection_Pakka).Tables(0)

        If DtTransport.Rows.Count > 0 Then
            ShowSaleInvoiceHeader("", False)
            CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowTransporter).Tag = FGetSubCodeFromOMSId(AgL.XNull(DtTransport.Rows(0)("Transporter")))
            CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowTransporter).Value = AgL.XNull(DtTransport.Rows(0)("TransporterName"))
            CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowVehicleNo).Value = AgL.XNull(DtTransport.Rows(0)("VehicleNo"))
            CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowShipMethod).Value = AgL.XNull(DtTransport.Rows(0)("ShipMethod"))
            CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowPreCarriageBy).Value = AgL.XNull(DtTransport.Rows(0)("PreCarriageBy"))
            CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowPreCarriagePlace).Value = AgL.XNull(DtTransport.Rows(0)("PreCarriagePlace"))
            CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowNoOfBales).Value = AgL.XNull(DtTransport.Rows(0)("NoOfBales"))
            CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowBookedFrom).Value = AgL.XNull(DtTransport.Rows(0)("BookedFrom"))
            CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowBookedTo).Value = AgL.XNull(DtTransport.Rows(0)("BookedTo"))
            CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowDestination).Value = AgL.XNull(DtTransport.Rows(0)("Destination"))
            CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowDescriptionOfGoods).Value = AgL.XNull(DtTransport.Rows(0)("DescriptionOfGoods"))
            CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowDescriptionOfPacking).Value = AgL.XNull(DtTransport.Rows(0)("DescriptionOfPacking"))
            CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowLrNo).Value = AgL.XNull(DtTransport.Rows(0)("LRNo"))
            CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowLrDate).Value = AgL.RetDate(AgL.XNull(DtTransport.Rows(0)("LRDate")))
            CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowPrivateMark).Value = AgL.XNull(DtTransport.Rows(0)("PrivateMark"))
            CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowWeight).Value = AgL.XNull(DtTransport.Rows(0)("Weight"))
            CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowWeight).Value = AgL.XNull(DtTransport.Rows(0)("ChargedWeight"))
            CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowFreight).Value = AgL.XNull(DtTransport.Rows(0)("Freight"))
            CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowLrPaymentType).Value = AgL.XNull(DtTransport.Rows(0)("PaymentType"))
            CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowRoadPermitNo).Value = AgL.XNull(DtTransport.Rows(0)("RoadPermitNo"))
            CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowRoadPermitDate).Value = AgL.RetDate(AgL.XNull(DtTransport.Rows(0)("RoadPermitDate")))
        End If
    End Sub
    Private Sub BtnEdit_Click(sender As Object, e As EventArgs) Handles BtnEdit.Click
        If mSearchCode = "" Then
            MsgBox("No Record Selected...!", MsgBoxStyle.Information)
            Exit Sub
        End If
        mMode = "E"

        Dim dtTemp As DataTable
        mQry = "Select * From Cloth_SupplierSettlementInvoices where PurchaseInvoiceDocId In (Select DocID From SaleInvoiceGeneratedEntries Where Code = '" & mSearchCode & "')"
        dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If dtTemp.Rows.Count > 0 Then
            If (AgL.PubUserName.ToUpper = "SA" Or AgL.PubUserName.ToUpper = "SUPER" Or AgL.PubIsUserAdmin = True) And CDate(AgL.PubLoginDate) < CDate("31-Jan-21") Then
                If MsgBox("Settlement Entry for purchase or sales is done. Do you want to modify Entry", vbYesNo) = vbNo Then
                    Exit Sub
                End If
            Else
                MsgBox("Settlement Entry for purchase or sales is done. Can't modify Entry")
                Exit Sub
            End If
        End If


        mQry = "Select * From Ledger where Clg_Date Is Not Null And DocId In (Select DocID From SaleInvoiceGeneratedEntries Where Code = '" & mSearchCode & "')"
        dtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If dtTemp.Rows.Count > 0 Then
            MsgBox("Reconciliation for purchase or sales is done. Can't modify Entry")
            Exit Sub
        End If



        Dgl1.ReadOnly = False
        Dgl2.ReadOnly = False
        BtnSave.Enabled = True
        BtnAdd.Enabled = False
        BtnDelete.Enabled = False
        BtnEdit.Enabled = False
        BtnAddItem.Enabled = False
        TxtOrderNo.Enabled = False
    End Sub
    Private Sub BtnFetchTransporterDetail_Click(sender As Object, e As EventArgs) Handles BtnFetchTransporterDetail.Click
        FetchLr()
    End Sub
    Private Sub FetchLr()
        For I As Integer = 0 To Dgl2.Rows.Count - 1
            If Dgl2.Item(Col2WInvoiceNo, I).Value <> "" Then
                If BtnTransportDetail.Tag IsNot Nothing Then
                    If AgL.XNull(CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowLrNo).Value) = "" Then
                        FCopyTransportDetail(Dgl2.Item(Col2InvoiceNo, I).Tag)
                    End If
                Else
                    FCopyTransportDetail(Dgl2.Item(Col2InvoiceNo, I).Tag)
                End If
            End If
        Next

        If BtnTransportDetail.Tag IsNot Nothing Then
            If AgL.XNull(CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowLrNo).Value) = "" Then
                FCopyTransportDetail(TxtOrderNo.Tag)
            End If
        End If

        If BtnTransportDetail.Tag IsNot Nothing Then
            If AgL.XNull(CType(BtnTransportDetail.Tag, FrmSaleInvoiceTransport).Dgl1.Item(FrmSaleInvoiceTransport.Col1Value, FrmSaleInvoiceTransport.rowLrNo).Value) <> "" Then
                BtnFetchTransporterDetail.BackColor = Color.Green
            End If
        End If
    End Sub
    Private Sub Dgl1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.RowEnter
        Dgl1.AgHelpDataSet(Col1Supplier) = Nothing
    End Sub
    Private Sub FPostTransactionReferences(SearchCode As String, Conn As Object, Cmd As Object)
        mQry = " Select * From SaleInvoiceGeneratedEntries Where Code = '" & SearchCode & "'"
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        For I As Integer = 0 To DtTemp.Rows.Count - 1
            For J As Integer = 0 To DtTemp.Rows.Count - 1
                If AgL.XNull(DtTemp.Rows(I)("DocId")) <> AgL.XNull(DtTemp.Rows(J)("DocId")) Then
                    mQry = "Insert Into TransactionReferences (DocID, ReferenceDocID, IsEditingAllowed, IsDeletingAllowed) 
                        Values (" & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("DocId"))) & ", 
                        " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(J)("DocId"))) & ", 1, 0) "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        Next
    End Sub
    Private Sub FLockPakkaEntries(SearchCode As String, Conn As Object, Cmd As Object, LockText As String)
        mQry = " Select * From SaleInvoiceGeneratedEntries Where Code = '" & SearchCode & "'"
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        For I As Integer = 0 To DtTemp.Rows.Count - 1
            mQry = " Select * From PurchInvoice Where DocId = '" & AgL.XNull(DtTemp.Rows(I)("DocId")) & "'"
            Dim DtPurchInvoice As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            For J As Integer = 0 To DtPurchInvoice.Rows.Count - 1
                mQry = " Update PurchInvoice Set LockText = " & AgL.Chk_Text(LockText) & " Where DocId = '" & AgL.XNull(DtPurchInvoice.Rows(J)("AMSDocId")) & "' "
                AgL.Dman_ExecuteNonQry(mQry, Connection_Pakka)
            Next

            mQry = " Select * From SaleInvoice Where DocId = '" & AgL.XNull(DtTemp.Rows(I)("DocId")) & "'"
            Dim DtSaleInvoice As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            For J As Integer = 0 To DtSaleInvoice.Rows.Count - 1
                mQry = " Update SaleInvoice Set LockText = " & AgL.Chk_Text(LockText) & " Where DocId = '" & AgL.XNull(DtSaleInvoice.Rows(J)("AMSDocId")) & "' "
                AgL.Dman_ExecuteNonQry(mQry, Connection_Pakka)
            Next
        Next
    End Sub
    Private Sub FCreateLog(SearchCode As String)
        Dim mLogText As String = ""

        mQry = " Select H.Type || ', ' || H.DocId || ', ' || IfNull(Si.ManualRefNo,Pi.VendorDocNo) As LockText
                From SaleInvoiceGeneratedEntries H
                LEFT JOIN SaleInvoice Si On H.DocId = Si.DocId
                LEFT JOIN PurchInvoice Pi On H.DocId = Pi.DocId
                Where H.Code = '" & SearchCode & "' "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        For I As Integer = 0 To DtTemp.Rows.Count - 1
            mLogText += AgL.XNull(DtTemp.Rows(I)("LockText")) & vbCrLf
        Next
        Call AgL.LogTableEntry(mSearchCode, Me.Text, mMode, AgL.PubMachineName, AgL.PubUserName, AgL.GetDateTime(AgL.GcnRead), AgL.GCn, AgL.ECmd,,,,,, AgL.PubSiteCode, AgL.PubDivCode, mLogText)
    End Sub
End Class