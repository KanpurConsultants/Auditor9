Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmSaleInvoiceOtherDetail
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        Me.EntryNCat = AgTemplate.ClsMain.Temp_NCat.ShipmentEntry
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.TxtInvoiceNo = New AgControls.AgTextBox
        Me.LblInvoiceNo = New System.Windows.Forms.Label
        Me.PnlCustomGrid = New System.Windows.Forms.Panel
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.TxtManualRefNo = New AgControls.AgTextBox
        Me.LblManualRefNo = New System.Windows.Forms.Label
        Me.LblManualOrderNoReq = New System.Windows.Forms.Label
        Me.LblShipmentDetail = New System.Windows.Forms.LinkLabel
        Me.TxtCustomFields = New AgControls.AgTextBox
        Me.GroupBox2.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GrpUP.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TP1.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(832, 482)
        Me.GroupBox2.Size = New System.Drawing.Size(148, 40)
        '
        'TxtStatus
        '
        Me.TxtStatus.AgSelectedValue = ""
        Me.TxtStatus.Location = New System.Drawing.Point(29, 19)
        Me.TxtStatus.Tag = ""
        '
        'CmdStatus
        '
        Me.CmdStatus.Size = New System.Drawing.Size(26, 19)
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(653, 482)
        Me.GBoxMoveToLog.Size = New System.Drawing.Size(148, 40)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Location = New System.Drawing.Point(3, 19)
        Me.TxtMoveToLog.Size = New System.Drawing.Size(142, 18)
        Me.TxtMoveToLog.Tag = ""
        '
        'CmdMoveToLog
        '
        Me.CmdMoveToLog.Size = New System.Drawing.Size(26, 19)
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(466, 482)
        Me.GBoxApprove.Size = New System.Drawing.Size(148, 40)
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Location = New System.Drawing.Point(29, 19)
        Me.TxtApproveBy.Tag = ""
        '
        'CmdDiscard
        '
        Me.CmdDiscard.Size = New System.Drawing.Size(26, 19)
        '
        'CmdApprove
        '
        Me.CmdApprove.Size = New System.Drawing.Size(26, 19)
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(150, 482)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 482)
        Me.GrpUP.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GroupBox1
        '
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 478)
        Me.GroupBox1.Size = New System.Drawing.Size(1002, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(300, 482)
        Me.GBoxDivision.Size = New System.Drawing.Size(114, 40)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Location = New System.Drawing.Point(3, 19)
        Me.TxtDivision.Tag = ""
        '
        'TxtDocId
        '
        Me.TxtDocId.AgSelectedValue = ""
        Me.TxtDocId.BackColor = System.Drawing.Color.White
        Me.TxtDocId.Tag = ""
        Me.TxtDocId.Text = ""
        '
        'LblV_No
        '
        Me.LblV_No.Location = New System.Drawing.Point(471, 36)
        Me.LblV_No.Size = New System.Drawing.Size(87, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Shipment No."
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(593, 35)
        Me.TxtV_No.Size = New System.Drawing.Size(149, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(349, 41)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(240, 36)
        Me.LblV_Date.Size = New System.Drawing.Size(94, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Shipment Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(577, 21)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(365, 35)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(471, 17)
        Me.LblV_Type.Size = New System.Drawing.Size(95, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Shipment Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(593, 15)
        Me.TxtV_Type.Size = New System.Drawing.Size(149, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(349, 21)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(240, 16)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(365, 15)
        Me.TxtSite_Code.Size = New System.Drawing.Size(100, 18)
        Me.TxtSite_Code.TabIndex = 0
        Me.TxtSite_Code.Tag = ""
        '
        'LblDocId
        '
        Me.LblDocId.Tag = ""
        '
        'LblPrefix
        '
        Me.LblPrefix.Location = New System.Drawing.Point(531, 36)
        Me.LblPrefix.Tag = ""
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-4, 19)
        Me.TabControl1.Size = New System.Drawing.Size(990, 130)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.LblManualOrderNoReq)
        Me.TP1.Controls.Add(Me.TxtManualRefNo)
        Me.TP1.Controls.Add(Me.LblManualRefNo)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.TxtInvoiceNo)
        Me.TP1.Controls.Add(Me.LblInvoiceNo)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(982, 104)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblInvoiceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtInvoiceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblManualOrderNoReq, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(984, 41)
        Me.Topctrl1.TabIndex = 2
        '
        'TxtInvoiceNo
        '
        Me.TxtInvoiceNo.AgMandatory = False
        Me.TxtInvoiceNo.AgMasterHelp = False
        Me.TxtInvoiceNo.AgNumberLeftPlaces = 8
        Me.TxtInvoiceNo.AgNumberNegetiveAllow = False
        Me.TxtInvoiceNo.AgNumberRightPlaces = 2
        Me.TxtInvoiceNo.AgPickFromLastValue = False
        Me.TxtInvoiceNo.AgRowFilter = ""
        Me.TxtInvoiceNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtInvoiceNo.AgSelectedValue = Nothing
        Me.TxtInvoiceNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtInvoiceNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtInvoiceNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtInvoiceNo.Location = New System.Drawing.Point(593, 55)
        Me.TxtInvoiceNo.MaxLength = 20
        Me.TxtInvoiceNo.Name = "TxtInvoiceNo"
        Me.TxtInvoiceNo.Size = New System.Drawing.Size(149, 18)
        Me.TxtInvoiceNo.TabIndex = 5
        '
        'LblInvoiceNo
        '
        Me.LblInvoiceNo.AutoSize = True
        Me.LblInvoiceNo.BackColor = System.Drawing.Color.Transparent
        Me.LblInvoiceNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblInvoiceNo.Location = New System.Drawing.Point(471, 55)
        Me.LblInvoiceNo.Name = "LblInvoiceNo"
        Me.LblInvoiceNo.Size = New System.Drawing.Size(71, 16)
        Me.LblInvoiceNo.TabIndex = 706
        Me.LblInvoiceNo.Text = "Invioce No."
        '
        'PnlCustomGrid
        '
        Me.PnlCustomGrid.Location = New System.Drawing.Point(8, 173)
        Me.PnlCustomGrid.Name = "PnlCustomGrid"
        Me.PnlCustomGrid.Size = New System.Drawing.Size(968, 299)
        Me.PnlCustomGrid.TabIndex = 1
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(240, 76)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(60, 16)
        Me.Label30.TabIndex = 723
        Me.Label30.Text = "Remarks"
        '
        'TxtRemarks
        '
        Me.TxtRemarks.AgMandatory = False
        Me.TxtRemarks.AgMasterHelp = False
        Me.TxtRemarks.AgNumberLeftPlaces = 0
        Me.TxtRemarks.AgNumberNegetiveAllow = False
        Me.TxtRemarks.AgNumberRightPlaces = 0
        Me.TxtRemarks.AgPickFromLastValue = False
        Me.TxtRemarks.AgRowFilter = ""
        Me.TxtRemarks.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtRemarks.AgSelectedValue = Nothing
        Me.TxtRemarks.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtRemarks.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtRemarks.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemarks.Location = New System.Drawing.Point(365, 74)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(377, 18)
        Me.TxtRemarks.TabIndex = 6
        '
        'TxtManualRefNo
        '
        Me.TxtManualRefNo.AgMandatory = True
        Me.TxtManualRefNo.AgMasterHelp = True
        Me.TxtManualRefNo.AgNumberLeftPlaces = 8
        Me.TxtManualRefNo.AgNumberNegetiveAllow = False
        Me.TxtManualRefNo.AgNumberRightPlaces = 2
        Me.TxtManualRefNo.AgPickFromLastValue = False
        Me.TxtManualRefNo.AgRowFilter = ""
        Me.TxtManualRefNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtManualRefNo.AgSelectedValue = Nothing
        Me.TxtManualRefNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtManualRefNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtManualRefNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtManualRefNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtManualRefNo.Location = New System.Drawing.Point(365, 55)
        Me.TxtManualRefNo.MaxLength = 50
        Me.TxtManualRefNo.Name = "TxtManualRefNo"
        Me.TxtManualRefNo.Size = New System.Drawing.Size(100, 18)
        Me.TxtManualRefNo.TabIndex = 4
        '
        'LblManualRefNo
        '
        Me.LblManualRefNo.AutoSize = True
        Me.LblManualRefNo.BackColor = System.Drawing.Color.Transparent
        Me.LblManualRefNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualRefNo.Location = New System.Drawing.Point(240, 56)
        Me.LblManualRefNo.Name = "LblManualRefNo"
        Me.LblManualRefNo.Size = New System.Drawing.Size(97, 16)
        Me.LblManualRefNo.TabIndex = 730
        Me.LblManualRefNo.Text = "Manual Ref No."
        '
        'LblManualOrderNoReq
        '
        Me.LblManualOrderNoReq.AutoSize = True
        Me.LblManualOrderNoReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblManualOrderNoReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblManualOrderNoReq.Location = New System.Drawing.Point(349, 61)
        Me.LblManualOrderNoReq.Name = "LblManualOrderNoReq"
        Me.LblManualOrderNoReq.Size = New System.Drawing.Size(10, 7)
        Me.LblManualOrderNoReq.TabIndex = 731
        Me.LblManualOrderNoReq.Text = "Ä"
        '
        'LblShipmentDetail
        '
        Me.LblShipmentDetail.BackColor = System.Drawing.Color.SteelBlue
        Me.LblShipmentDetail.DisabledLinkColor = System.Drawing.Color.White
        Me.LblShipmentDetail.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblShipmentDetail.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblShipmentDetail.LinkColor = System.Drawing.Color.White
        Me.LblShipmentDetail.Location = New System.Drawing.Point(8, 152)
        Me.LblShipmentDetail.Name = "LblShipmentDetail"
        Me.LblShipmentDetail.Size = New System.Drawing.Size(124, 20)
        Me.LblShipmentDetail.TabIndex = 734
        Me.LblShipmentDetail.TabStop = True
        Me.LblShipmentDetail.Text = "Shipment Detail"
        Me.LblShipmentDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCustomFields
        '
        Me.TxtCustomFields.AgMandatory = False
        Me.TxtCustomFields.AgMasterHelp = False
        Me.TxtCustomFields.AgNumberLeftPlaces = 8
        Me.TxtCustomFields.AgNumberNegetiveAllow = False
        Me.TxtCustomFields.AgNumberRightPlaces = 2
        Me.TxtCustomFields.AgPickFromLastValue = False
        Me.TxtCustomFields.AgRowFilter = ""
        Me.TxtCustomFields.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCustomFields.AgSelectedValue = Nothing
        Me.TxtCustomFields.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCustomFields.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCustomFields.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCustomFields.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCustomFields.Location = New System.Drawing.Point(474, 493)
        Me.TxtCustomFields.MaxLength = 20
        Me.TxtCustomFields.Name = "TxtCustomFields"
        Me.TxtCustomFields.Size = New System.Drawing.Size(72, 18)
        Me.TxtCustomFields.TabIndex = 770
        Me.TxtCustomFields.Text = "AgTextBox1"
        Me.TxtCustomFields.Visible = False
        '
        'TempProductionOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 523)
        Me.Controls.Add(Me.TxtCustomFields)
        Me.Controls.Add(Me.LblShipmentDetail)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Name = "TempProductionOrder"
        Me.Text = "Template Production Order"
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.PnlCustomGrid, 0)
        Me.Controls.SetChildIndex(Me.LblShipmentDetail, 0)
        Me.Controls.SetChildIndex(Me.TxtCustomFields, 0)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GBoxMoveToLog.ResumeLayout(False)
        Me.GBoxMoveToLog.PerformLayout()
        Me.GBoxApprove.ResumeLayout(False)
        Me.GBoxApprove.PerformLayout()
        Me.GBoxEntryType.ResumeLayout(False)
        Me.GBoxEntryType.PerformLayout()
        Me.GrpUP.ResumeLayout(False)
        Me.GrpUP.PerformLayout()
        Me.GBoxDivision.ResumeLayout(False)
        Me.GBoxDivision.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TP1.ResumeLayout(False)
        Me.TP1.PerformLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Protected WithEvents TxtInvoiceNo As AgControls.AgTextBox
    Protected WithEvents LblInvoiceNo As System.Windows.Forms.Label
    Protected WithEvents PnlCustomGrid As System.Windows.Forms.Panel
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents TxtManualRefNo As AgControls.AgTextBox
    Protected WithEvents LblManualRefNo As System.Windows.Forms.Label
    Protected WithEvents LblManualOrderNoReq As System.Windows.Forms.Label
    Protected WithEvents TxtCustomFields As AgControls.AgTextBox
    Protected WithEvents LblShipmentDetail As System.Windows.Forms.LinkLabel
#End Region

    Private Sub FrmProductionOrder_BaseEvent_FindLog() Handles Me.BaseEvent_FindLog
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        AgL.PubFindQry = " SELECT H.UID AS SearchCode, H.V_Type AS [Shipment_Type], H.V_Date AS [Shipment_Date], " & _
            " H.V_No AS [Shipment_No], H.Remarks, H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date],  " & _
            " H.EntryStatus AS [Entry_Status], Sm.Name, D.Div_Name " & _
            " FROM  SaleInvoiceOtherDetail_Log H " & _
            " LEFT JOIN Division D ON D.Div_Code=H.Div_Code  " & _
            " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code  " & _
            " LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
            " Where H.EntryStatus = '" & AgTemplate.ClsMain.LogStatus.LogOpen & "'" & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And IsNull(H.IsDeleted,0)=0  And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        AgL.PubFindQry = " SELECT H.UID AS SearchCode, H.V_Type AS [Shipment_Type], H.V_Date AS [Shipment_Date], " & _
                  " H.V_No AS [Shipment_No], H.Remarks, H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date],  " & _
                  " H.EntryStatus AS [Entry_Status], Sm.Name, D.Div_Name " & _
                  " FROM  SaleInvoiceOtherDetail_Log H " & _
                  " LEFT JOIN Division D ON D.Div_Code=H.Div_Code  " & _
                  " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code  " & _
                  " LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
                  " Where 1=1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "SaleInvoiceOtherDetail"
        LogTableName = "SaleInvoiceOtherDetail_Log"
        MainLineTableCsv = "CustomFields_Trans"
        LogLineTableCsv = "CustomFields_Trans_Log"

        AgL.AddAgDataGrid(AgCustomGrid1, PnlCustomGrid)

        AgCustomGrid1.AgLibVar = AgL
        AgCustomGrid1.SplitGrid = True
        AgCustomGrid1.MnuText = Me.Name
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("Po.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("Po.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "Po.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = " Select Po.DocID As SearchCode " & _
            " From SaleInvoiceOtherDetail Po " & _
            " Left Join Voucher_Type Vt On Po.V_Type = Vt.V_Type  " & _
            " Where IsNull(IsDeleted,0) = 0  " & mCondStr & "  Order By Po.V_Date Desc "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMastLog(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMastLog
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("Po.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("Po.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "Po.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = "Select Po.UID As SearchCode " & _
               " From SaleInvoiceOtherDetail_Log Po " & _
               " Left Join Voucher_Type Vt On Po.V_Type = Vt.V_Type  " & _
               " Where Po.EntryStatus='" & LogStatus.LogOpen & "' " & mCondStr & " Order By EntryDate"

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        AgCustomGrid1.Ini_Grid(mSearchCode)


        FrmProductionOrder_BaseFunction_FIniList()
        'Ini_List()
    End Sub


    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        mQry = "UPDATE SaleInvoiceOtherDetail_Log " & _
                " SET " & _
                " InvoiceNo = " & AgL.Chk_Text(TxtInvoiceNo.AgSelectedValue) & ", " & _
                " ManualRefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " & _
                " CustomFields = " & AgL.Chk_Text(TxtCustomFields.AgSelectedValue) & ", " & _
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & " " & _
                " Where UID = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        AgCustomGrid1.Save_TransFooter(mInternalCode, Conn, Cmd, SearchCode)
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DrTemp As DataRow() = Nothing

        Dim DsTemp As DataSet

        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = "Select Po.* " & _
                " From SaleInvoiceOtherDetail Po " & _
                " Where Po.DocID='" & SearchCode & "'"
        Else
            mQry = "Select Po.* " & _
                " From SaleInvoiceOtherDetail_Log Po " & _
                " Where Po.UID='" & SearchCode & "'"
        End If
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtCustomFields.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                If AgL.XNull(.Rows(0)("CustomFields")) <> "" Then
                    TxtCustomFields.AgSelectedValue = AgL.XNull(.Rows(0)("CustomFields"))
                End If
                AgCustomGrid1.FrmType = Me.FrmType
                AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue
                IniGrid()
                TxtInvoiceNo.AgSelectedValue = AgL.XNull(.Rows(0)("InvoiceNO"))
                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))

                AgCustomGrid1.MoveRec_TransFooter(SearchCode)
                Calculation()
                '-------------------------------------------------------------
            End If
        End With
    End Sub

    Private Sub FrmProductionOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 660, 992, 0, 0)
        AgCustomGrid1.FrmType = Me.FrmType
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.AgSelectedValue, AgL.GCn)
        AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue
        IniGrid()
        ProcFillReferenceNo()
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = " SELECT H.DocID AS Code, H.ReferenceNo AS InvoiceNo, h.V_Date As InvoiceDate, " & _
                " IsNull(H.IsDeleted,0) AS IsDeleted, " & _
                " IsNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status , " & _
                " H.Div_Code, H.MoveToLog " & _
                " FROM SaleInvoice H  "
        TxtInvoiceNo.AgHelpDataSet(5, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT H.Code, H.Description FROM CustomFields H "
        TxtCustomFields.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        If AgL.RequiredField(TxtManualRefNo, LblManualRefNo.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtInvoiceNo, LblInvoiceNo.Text) Then passed = False : Exit Sub
        passed = FCheckDuplicateRefNo()
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
    End Sub

    Private Function Validate_SaleOrder() As Boolean
        Dim DrTemp As DataRow() = Nothing
        Try
            If TxtInvoiceNo.Text <> "" Then
                DrTemp = TxtInvoiceNo.AgHelpDataSet.Tables(0).Select("Code = '" & TxtInvoiceNo.AgSelectedValue & "' ")
                If DrTemp.Length > 0 Then
                    If Not AgL.StrCmp(AgL.XNull(DrTemp(0)("MoveToLog")), "") Then
                        MsgBox("Currently Invoice No """ & TxtInvoiceNo.Text & """ Is In Log For Modification." & vbCrLf & "Can't Continue...!", MsgBoxStyle.Information)
                        If AgL.StrCmp(Topctrl1.Mode, "Add") Then TxtInvoiceNo.Text = ""
                        Exit Function
                    End If

                    If Not AgL.StrCmp(AgL.XNull(DrTemp(0)("Status")), AgTemplate.ClsMain.EntryStatus.Active) Then
                        MsgBox("Currently Invoice No """ & TxtInvoiceNo.Text & """ Is Not In Active State." & vbCrLf & "Can't Continue...!", MsgBoxStyle.Information)
                        If AgL.StrCmp(Topctrl1.Mode, "Add") Then TxtInvoiceNo.Text = ""
                        Exit Function
                    End If
                End If
            End If
            Validate_SaleOrder = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtInvoiceNo.Validating, TxtManualRefNo.Validating
        Dim DrTemp As DataRow() = Nothing
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            Select Case sender.name
                Case TxtV_Type.Name
                    TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.AgSelectedValue, AgL.GcnRead)
                    AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue
                    IniGrid()
                    ProcFillReferenceNo()

                Case TxtInvoiceNo.Name
                    e.Cancel = Not Validate_SaleOrder()

                Case TxtManualRefNo.Name
                    e.Cancel = Not FCheckDuplicateRefNo()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FCheckDuplicateRefNo() As Boolean
        FCheckDuplicateRefNo = True
        If Topctrl1.Mode = "Add" Then
            mQry = " SELECT COUNT(*) FROM SaleInvoiceOtherDetail WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'   " & _
                    " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And IsNull(IsDeleted,0) = 0  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtManualRefNo.Focus()
        Else
            mQry = " SELECT COUNT(*) FROM SaleInvoiceOtherDetail WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'  " & _
                    " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And IsNull(IsDeleted,0) = 0 AND DocID <>'" & mInternalCode & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtManualRefNo.Focus()
        End If
    End Function


    Private Sub Txt_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtInvoiceNo.Enter
        Try
            Select Case sender.name
                Case TxtInvoiceNo.Name
                    If TxtV_Date.Text <> "" Then
                        TxtInvoiceNo.AgRowFilter = " IsDeleted = 0 " & _
                            " And InvoiceDate <= '" & TxtV_Date.Text & "' " & _
                            " And " & AgTemplate.ClsMain.RetDivFilterStr() & " " & _
                            " And Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "'"
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProcFillReferenceNo()
        If TxtManualRefNo.Text = "" Then
            If TxtV_Type.Text.ToString.Trim <> "" Or TxtV_Type.AgSelectedValue.Trim <> "" Then
                TxtManualRefNo.Text = TxtV_No.Text
            End If
        End If
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmSaleInvoiceOtherDetail_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        Dim mCrd As New ReportDocument
        Dim ReportView As New AgLibrary.RepView
        Dim DsRep As New DataSet
        Dim DsRep1 As New DataSet
        Dim strQry As String = "", RepName As String = "", RepTitle As String = "", strQry1 As String = ""
        Dim bTableName As String = "", bSecTableName As String = "", bStructJoin As String = ""
        Dim bCondstr As String = ""

        Try
            Me.Cursor = Cursors.WaitCursor

            RepName = "Rug_ExportRegister_Print" : RepTitle = "EXPORT REGISTER"

            If FrmType = ClsMain.EntryPointType.Main Then

                bTableName = "SaleInvoiceOtherDetail"
                bStructJoin = " LEFT JOIN (" & AgCustomFields.AgCustomGrid.AgCustomFieldSubQueryFooter(AgL, TxtV_Type.AgSelectedValue, FrmType) & ") As SF On H.DocId = SF.DocId "
                bCondstr = " Where H.DocID='" & mInternalCode & "'"


            ElseIf FrmType = ClsMain.EntryPointType.Log Then
                bTableName = "SaleInvoiceOtherDetail_Log"
                bStructJoin = " LEFT JOIN (" & AgCustomFields.AgCustomGrid.AgCustomFieldSubQueryFooter(AgL, TxtV_Type.AgSelectedValue, FrmType) & ") As SF On H.UID = SF.UID "
                bCondstr = " Where H.UID='" & mSearchCode & "'"
            End If


            strQry = " SELECT H.DocID, SI.DocID AS InvoiceNo , SI.V_Date, SI.Site_Code, SI.ReferenceNo, SI.SaleToParty, SI.SaleToPartyName, " & _
                    " SI.SaleToPartyAddress , SI.SaleToPartyCity , C.CityName AS PartyCityName, SI.SaleToPartyMobile , SI.SaleOrder, SI.SaleChallan, " & _
                    " SI.OrderNo, SI.OrderDate, SI.RollNo, SI.DescriptionOfgoods, SI.PackingMatrialDescription ,SI.noofkindsofpackgs, " & _
                    " SI.Compositions, SI.OthersRefrence, SI.Grosswt, SI.Netwt, SI.TotalBale, SI.PriceMode, SI.TotalQty, SI.TotalMeasure, SI.TotalAmount, " & _
                    " SF.* " & _
                    " FROM " & bTableName & " H " & _
                    " LEFT JOIN SaleInvoice SI ON SI.DocId = H.InvoiceNo " & _
                    " " & bStructJoin & " " & _
                    " LEFT JOIN City C ON C.CityCode  = SI.SaleToPartyCity  " & _
                    " " & bCondstr & " "

            strQry1 = " SELECT Q.Description AS Quality, L.DocId AS InvoiceNo  " & _
                    " FROM SaleInvoiceDetail L   " & _
                    " LEFT JOIN RUG_CarpetSku CS ON CS.Code=L.Item  " & _
                    " LEFT JOIN RUG_Design D ON D.Code=CS.Design   " & _
                    " LEFT JOIN RUG_Quality Q ON Q.Code=D.QualityCode  " & _
                    " GROUP BY Q.Description, L.DocId  "

            AgL.ADMain = New SqlClient.SqlDataAdapter(strQry, AgL.GCn)
            AgL.ADMain.Fill(DsRep)
            AgL.ADMain = New SqlClient.SqlDataAdapter(strQry1, AgL.GCn)
            AgL.ADMain.Fill(DsRep1)


            AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)
            AgPL.CreateFieldDefFile1(DsRep1, AgL.PubReportPath & "\" & RepName & "1.ttx", True)
            mCrd.Load(AgL.PubReportPath & "\" & RepName & ".rpt")
            mCrd.SetDataSource(DsRep.Tables(0))

            mCrd.OpenSubreport("SUBREP1").Database.Tables(0).SetDataSource(DsRep1.Tables(0))
            CType(ReportView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
            AgPL.Formula_Set(mCrd, RepTitle)
            AgPL.Show_Report(ReportView, "* " & RepTitle & " *", Me.MdiParent)

            Call AgL.LogTableEntry(mSearchCode, Me.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
End Class
