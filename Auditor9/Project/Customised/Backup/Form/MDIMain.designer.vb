<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MDIMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MnuMain = New System.Windows.Forms.MenuStrip
        Me.MnuMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuItemCategoryMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuItemGroupMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuItemMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuCustomerMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuSupplierMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuAgentMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuRateTypeMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuRateList = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuManufacturerMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuVatCommodityCodeMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuGodownMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuCustomized = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuSaleInvoiceDetailEntry = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuOpeningStockEntry = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuAdjustmentIssueEntry = New System.Windows.Forms.ToolStripMenuItem
        Me.MnnReports = New System.Windows.Forms.ToolStripMenuItem
        Me.ItemExpiryReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PurchaseAdviseReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuPurchaseIndentReport = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuVATReports = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuPartyOutstandingReport = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuBillWiseProfitability = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuDebtorsOutstandingOverDue = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuTools = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuAdjustSaleInvoices = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuHidden = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuSaleInvoice = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuWeavingOrderRatio = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'MnuMain
        '
        Me.MnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuMaster, Me.MnuCustomized, Me.MnuHidden})
        Me.MnuMain.Location = New System.Drawing.Point(0, 0)
        Me.MnuMain.Name = "MnuMain"
        Me.MnuMain.Size = New System.Drawing.Size(965, 24)
        Me.MnuMain.TabIndex = 1
        Me.MnuMain.Text = "MenuStrip1"
        '
        'MnuMaster
        '
        Me.MnuMaster.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuItemCategoryMaster, Me.MnuItemGroupMaster, Me.MnuItemMaster, Me.MnuCustomerMaster, Me.MnuSupplierMaster, Me.MnuAgentMaster, Me.MnuRateTypeMaster, Me.MnuRateList, Me.MnuManufacturerMaster, Me.MnuVatCommodityCodeMaster, Me.MnuGodownMaster})
        Me.MnuMaster.Name = "MnuMaster"
        Me.MnuMaster.Size = New System.Drawing.Size(55, 20)
        Me.MnuMaster.Text = "Master"
        '
        'MnuItemCategoryMaster
        '
        Me.MnuItemCategoryMaster.Name = "MnuItemCategoryMaster"
        Me.MnuItemCategoryMaster.Size = New System.Drawing.Size(228, 22)
        Me.MnuItemCategoryMaster.Text = "Item Category Master"
        '
        'MnuItemGroupMaster
        '
        Me.MnuItemGroupMaster.Name = "MnuItemGroupMaster"
        Me.MnuItemGroupMaster.Size = New System.Drawing.Size(228, 22)
        Me.MnuItemGroupMaster.Text = "Item Group Master"
        '
        'MnuItemMaster
        '
        Me.MnuItemMaster.Name = "MnuItemMaster"
        Me.MnuItemMaster.Size = New System.Drawing.Size(228, 22)
        Me.MnuItemMaster.Text = "Item Master"
        '
        'MnuCustomerMaster
        '
        Me.MnuCustomerMaster.Name = "MnuCustomerMaster"
        Me.MnuCustomerMaster.Size = New System.Drawing.Size(228, 22)
        Me.MnuCustomerMaster.Text = "Customer Master"
        '
        'MnuSupplierMaster
        '
        Me.MnuSupplierMaster.Name = "MnuSupplierMaster"
        Me.MnuSupplierMaster.Size = New System.Drawing.Size(228, 22)
        Me.MnuSupplierMaster.Text = "Supplier Master"
        '
        'MnuAgentMaster
        '
        Me.MnuAgentMaster.Name = "MnuAgentMaster"
        Me.MnuAgentMaster.Size = New System.Drawing.Size(228, 22)
        Me.MnuAgentMaster.Text = "Agent Master"
        '
        'MnuRateTypeMaster
        '
        Me.MnuRateTypeMaster.Name = "MnuRateTypeMaster"
        Me.MnuRateTypeMaster.Size = New System.Drawing.Size(228, 22)
        Me.MnuRateTypeMaster.Text = "Rate Type Master"
        '
        'MnuRateList
        '
        Me.MnuRateList.Name = "MnuRateList"
        Me.MnuRateList.Size = New System.Drawing.Size(228, 22)
        Me.MnuRateList.Text = "Rate List"
        '
        'MnuManufacturerMaster
        '
        Me.MnuManufacturerMaster.Name = "MnuManufacturerMaster"
        Me.MnuManufacturerMaster.Size = New System.Drawing.Size(228, 22)
        Me.MnuManufacturerMaster.Text = "Manufacturer Master"
        '
        'MnuVatCommodityCodeMaster
        '
        Me.MnuVatCommodityCodeMaster.Name = "MnuVatCommodityCodeMaster"
        Me.MnuVatCommodityCodeMaster.Size = New System.Drawing.Size(228, 22)
        Me.MnuVatCommodityCodeMaster.Text = "Vat Commodity Code Master"
        '
        'MnuGodownMaster
        '
        Me.MnuGodownMaster.Name = "MnuGodownMaster"
        Me.MnuGodownMaster.Size = New System.Drawing.Size(228, 22)
        Me.MnuGodownMaster.Text = "Godown Master"
        '
        'MnuCustomized
        '
        Me.MnuCustomized.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuSaleInvoiceDetailEntry, Me.MnuOpeningStockEntry, Me.MnuAdjustmentIssueEntry, Me.MnnReports, Me.MnuTools})
        Me.MnuCustomized.Name = "MnuCustomized"
        Me.MnuCustomized.Size = New System.Drawing.Size(82, 20)
        Me.MnuCustomized.Text = "Customized"
        '
        'MnuSaleInvoiceDetailEntry
        '
        Me.MnuSaleInvoiceDetailEntry.Name = "MnuSaleInvoiceDetailEntry"
        Me.MnuSaleInvoiceDetailEntry.Size = New System.Drawing.Size(199, 22)
        Me.MnuSaleInvoiceDetailEntry.Text = "Sale Invoice Detail Entry"
        '
        'MnuOpeningStockEntry
        '
        Me.MnuOpeningStockEntry.Name = "MnuOpeningStockEntry"
        Me.MnuOpeningStockEntry.Size = New System.Drawing.Size(199, 22)
        Me.MnuOpeningStockEntry.Text = "Opening Stock Entry"
        '
        'MnuAdjustmentIssueEntry
        '
        Me.MnuAdjustmentIssueEntry.Name = "MnuAdjustmentIssueEntry"
        Me.MnuAdjustmentIssueEntry.Size = New System.Drawing.Size(199, 22)
        Me.MnuAdjustmentIssueEntry.Text = "Adjustment Issue Entry"
        '
        'MnnReports
        '
        Me.MnnReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ItemExpiryReportToolStripMenuItem, Me.PurchaseAdviseReportToolStripMenuItem, Me.MnuPurchaseIndentReport, Me.MnuVATReports, Me.MnuPartyOutstandingReport, Me.MnuBillWiseProfitability, Me.MnuDebtorsOutstandingOverDue, Me.MnuWeavingOrderRatio})
        Me.MnnReports.Name = "MnnReports"
        Me.MnnReports.Size = New System.Drawing.Size(199, 22)
        Me.MnnReports.Text = "Reports"
        '
        'ItemExpiryReportToolStripMenuItem
        '
        Me.ItemExpiryReportToolStripMenuItem.Name = "ItemExpiryReportToolStripMenuItem"
        Me.ItemExpiryReportToolStripMenuItem.Size = New System.Drawing.Size(236, 22)
        Me.ItemExpiryReportToolStripMenuItem.Tag = "Report"
        Me.ItemExpiryReportToolStripMenuItem.Text = "Item Expiry Report"
        '
        'PurchaseAdviseReportToolStripMenuItem
        '
        Me.PurchaseAdviseReportToolStripMenuItem.Name = "PurchaseAdviseReportToolStripMenuItem"
        Me.PurchaseAdviseReportToolStripMenuItem.Size = New System.Drawing.Size(236, 22)
        Me.PurchaseAdviseReportToolStripMenuItem.Tag = "Report"
        Me.PurchaseAdviseReportToolStripMenuItem.Text = "Purchase Advise Report"
        '
        'MnuPurchaseIndentReport
        '
        Me.MnuPurchaseIndentReport.Name = "MnuPurchaseIndentReport"
        Me.MnuPurchaseIndentReport.Size = New System.Drawing.Size(236, 22)
        Me.MnuPurchaseIndentReport.Tag = "Report"
        Me.MnuPurchaseIndentReport.Text = "Purchase Indent Report"
        '
        'MnuVATReports
        '
        Me.MnuVATReports.Name = "MnuVATReports"
        Me.MnuVATReports.Size = New System.Drawing.Size(236, 22)
        Me.MnuVATReports.Tag = "Report"
        Me.MnuVATReports.Text = "VAT Reports"
        '
        'MnuPartyOutstandingReport
        '
        Me.MnuPartyOutstandingReport.Name = "MnuPartyOutstandingReport"
        Me.MnuPartyOutstandingReport.Size = New System.Drawing.Size(236, 22)
        Me.MnuPartyOutstandingReport.Tag = "Report"
        Me.MnuPartyOutstandingReport.Text = "Party Outstanding Report"
        '
        'MnuBillWiseProfitability
        '
        Me.MnuBillWiseProfitability.Name = "MnuBillWiseProfitability"
        Me.MnuBillWiseProfitability.Size = New System.Drawing.Size(236, 22)
        Me.MnuBillWiseProfitability.Tag = "REPORT"
        Me.MnuBillWiseProfitability.Text = "Bill Wise Profitability"
        '
        'MnuDebtorsOutstandingOverDue
        '
        Me.MnuDebtorsOutstandingOverDue.Name = "MnuDebtorsOutstandingOverDue"
        Me.MnuDebtorsOutstandingOverDue.Size = New System.Drawing.Size(236, 22)
        Me.MnuDebtorsOutstandingOverDue.Tag = "Report"
        Me.MnuDebtorsOutstandingOverDue.Text = "Debtors Outstanding Over Due"
        '
        'MnuTools
        '
        Me.MnuTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuAdjustSaleInvoices})
        Me.MnuTools.Name = "MnuTools"
        Me.MnuTools.Size = New System.Drawing.Size(199, 22)
        Me.MnuTools.Text = "Tools"
        '
        'MnuAdjustSaleInvoices
        '
        Me.MnuAdjustSaleInvoices.Name = "MnuAdjustSaleInvoices"
        Me.MnuAdjustSaleInvoices.Size = New System.Drawing.Size(178, 22)
        Me.MnuAdjustSaleInvoices.Text = "Adjust Sale Invoices"
        '
        'MnuHidden
        '
        Me.MnuHidden.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuSaleInvoice})
        Me.MnuHidden.Name = "MnuHidden"
        Me.MnuHidden.Size = New System.Drawing.Size(58, 20)
        Me.MnuHidden.Text = "Hidden"
        Me.MnuHidden.Visible = False
        '
        'MnuSaleInvoice
        '
        Me.MnuSaleInvoice.Name = "MnuSaleInvoice"
        Me.MnuSaleInvoice.Size = New System.Drawing.Size(136, 22)
        Me.MnuSaleInvoice.Text = "Sale Invoice"
        '
        'MnuWeavingOrderRatio
        '
        Me.MnuWeavingOrderRatio.Name = "MnuWeavingOrderRatio"
        Me.MnuWeavingOrderRatio.Size = New System.Drawing.Size(236, 22)
        Me.MnuWeavingOrderRatio.Tag = "Report"
        Me.MnuWeavingOrderRatio.Text = "Weaving Order Ratio"
        '
        'MDIMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(965, 661)
        Me.Controls.Add(Me.MnuMain)
        Me.IsMdiContainer = True
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MnuMain
        Me.Name = "MDIMain"
        Me.Text = "Customise"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MnuMain.ResumeLayout(False)
        Me.MnuMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStripMenuItem10 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents MnuMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuCustomerMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuSupplierMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuAgentMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuRateTypeMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuRateList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuItemMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuItemCategoryMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuItemGroupMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuManufacturerMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuVatCommodityCodeMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuGodownMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuCustomized As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuSaleInvoiceDetailEntry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuOpeningStockEntry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnnReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ItemExpiryReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PurchaseAdviseReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPurchaseIndentReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuVATReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPartyOutstandingReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuBillWiseProfitability As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuAdjustSaleInvoices As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuAdjustmentIssueEntry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuHidden As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuSaleInvoice As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuDebtorsOutstandingOverDue As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuWeavingOrderRatio As System.Windows.Forms.ToolStripMenuItem

End Class
