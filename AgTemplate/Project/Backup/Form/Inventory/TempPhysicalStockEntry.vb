Public Class TempPhysicalStockEntry
    Inherits TempTransaction
    Dim mQry$

    Public Event BaseFunction_MoveRecLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer)
    Public Event BaseEvent_Save_InTransLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer, ByVal Conn As SqlClient.SqlConnection, ByVal Cmd As SqlClient.SqlCommand)

    Public WithEvents Dgl1 As AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1Item As String = "Item"
    Protected Const Col1Process As String = "Process"
    Protected Const Col1Status As String = "Status"
    Protected Const Col1CurrentStock As String = "Current Stock"
    Protected Const Col1PhysicalStock As String = "Physical Stock"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1Difference As String = "Difference"

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.TxtGodown = New AgControls.AgTextBox
        Me.LblSaleOrderNo = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalAmount = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.LblTotalPhysicalQty = New System.Windows.Forms.Label
        Me.LblTotalPhysicalQtyText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.GroupBox2.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GrpUP.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TP1.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Dgl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(746, 472)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(582, 472)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(415, 472)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(150, 472)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 472)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 468)
        Me.GroupBox1.Size = New System.Drawing.Size(919, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(285, 472)
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
        Me.LblV_No.Location = New System.Drawing.Point(392, 40)
        Me.LblV_No.Tag = ""
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(510, 39)
        Me.TxtV_No.Size = New System.Drawing.Size(149, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(266, 45)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(170, 40)
        Me.LblV_Date.Tag = ""
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(494, 25)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(282, 39)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(392, 21)
        Me.LblV_Type.Tag = ""
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(510, 19)
        Me.TxtV_Type.Size = New System.Drawing.Size(149, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(266, 25)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(170, 20)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(282, 19)
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
        Me.LblPrefix.Location = New System.Drawing.Point(728, 36)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-4, 42)
        Me.TabControl1.Size = New System.Drawing.Size(909, 158)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.TxtGodown)
        Me.TP1.Controls.Add(Me.LblSaleOrderNo)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(901, 132)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSaleOrderNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(901, 41)
        Me.Topctrl1.TabIndex = 2
        '
        'Dgl1
        '
        Me.Dgl1.AgMandatoryColumn = 0
        Me.Dgl1.AgReadOnlyColumnColor = System.Drawing.Color.Ivory
        Me.Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.Dgl1.CancelEditingControlValidating = False
        Me.Dgl1.Location = New System.Drawing.Point(0, 0)
        Me.Dgl1.Name = "Dgl1"
        Me.Dgl1.Size = New System.Drawing.Size(240, 150)
        Me.Dgl1.TabIndex = 0
        '
        'TxtGodown
        '
        Me.TxtGodown.AgMandatory = True
        Me.TxtGodown.AgMasterHelp = False
        Me.TxtGodown.AgNumberLeftPlaces = 8
        Me.TxtGodown.AgNumberNegetiveAllow = False
        Me.TxtGodown.AgNumberRightPlaces = 2
        Me.TxtGodown.AgPickFromLastValue = False
        Me.TxtGodown.AgRowFilter = ""
        Me.TxtGodown.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtGodown.AgSelectedValue = Nothing
        Me.TxtGodown.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtGodown.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtGodown.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtGodown.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGodown.Location = New System.Drawing.Point(282, 59)
        Me.TxtGodown.MaxLength = 20
        Me.TxtGodown.Name = "TxtGodown"
        Me.TxtGodown.Size = New System.Drawing.Size(377, 18)
        Me.TxtGodown.TabIndex = 4
        '
        'LblSaleOrderNo
        '
        Me.LblSaleOrderNo.AutoSize = True
        Me.LblSaleOrderNo.BackColor = System.Drawing.Color.Transparent
        Me.LblSaleOrderNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSaleOrderNo.Location = New System.Drawing.Point(170, 61)
        Me.LblSaleOrderNo.Name = "LblSaleOrderNo"
        Me.LblSaleOrderNo.Size = New System.Drawing.Size(55, 16)
        Me.LblSaleOrderNo.TabIndex = 706
        Me.LblSaleOrderNo.Text = "Godown"
        '
        'Panel1
        '
        Me.Panel1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalAmount)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.LblTotalPhysicalQty)
        Me.Panel1.Controls.Add(Me.LblTotalPhysicalQtyText)
        Me.Panel1.Location = New System.Drawing.Point(6, 443)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(888, 23)
        Me.Panel1.TabIndex = 694
        '
        'LblTotalAmount
        '
        Me.LblTotalAmount.AutoSize = True
        Me.LblTotalAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmount.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalAmount.Location = New System.Drawing.Point(750, 3)
        Me.LblTotalAmount.Name = "LblTotalAmount"
        Me.LblTotalAmount.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalAmount.TabIndex = 668
        Me.LblTotalAmount.Text = "."
        Me.LblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Maroon
        Me.Label4.Location = New System.Drawing.Point(639, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(101, 16)
        Me.Label4.TabIndex = 667
        Me.Label4.Text = "Total Amount :"
        '
        'LblTotalPhysicalQty
        '
        Me.LblTotalPhysicalQty.AutoSize = True
        Me.LblTotalPhysicalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalPhysicalQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalPhysicalQty.Location = New System.Drawing.Point(485, 3)
        Me.LblTotalPhysicalQty.Name = "LblTotalPhysicalQty"
        Me.LblTotalPhysicalQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalPhysicalQty.TabIndex = 660
        Me.LblTotalPhysicalQty.Text = "."
        Me.LblTotalPhysicalQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalPhysicalQtyText
        '
        Me.LblTotalPhysicalQtyText.AutoSize = True
        Me.LblTotalPhysicalQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalPhysicalQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalPhysicalQtyText.Location = New System.Drawing.Point(345, 3)
        Me.LblTotalPhysicalQtyText.Name = "LblTotalPhysicalQtyText"
        Me.LblTotalPhysicalQtyText.Size = New System.Drawing.Size(130, 16)
        Me.LblTotalPhysicalQtyText.TabIndex = 659
        Me.LblTotalPhysicalQtyText.Text = "Total Physical Qty :"
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Pnl1.Location = New System.Drawing.Point(6, 223)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(889, 220)
        Me.Pnl1.TabIndex = 1
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(170, 81)
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
        Me.TxtRemarks.Location = New System.Drawing.Point(282, 79)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(377, 18)
        Me.TxtRemarks.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(264, 67)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 724
        Me.Label1.Text = "Ä"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(6, 203)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(99, 20)
        Me.LinkLabel1.TabIndex = 740
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Item Detail"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TempPhysicalStockEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(901, 513)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Name = "TempPhysicalStockEntry"
        Me.Text = "Template Sale Order"
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
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
        CType(Me.Dgl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents TxtGodown As AgControls.AgTextBox
    Protected WithEvents LblSaleOrderNo As System.Windows.Forms.Label
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalPhysicalQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalPhysicalQtyText As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents LblTotalAmount As System.Windows.Forms.Label
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
#End Region

    Private Sub TempPhysicalStockEntry_BaseEvent_Approve_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Approve_InTrans
        Dim Stock As ClsMain.StructStock = Nothing
        Dim I As Integer = 0

        mQry = " Delete From Stock Where DocId = '" & mInternalCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                If Val(Dgl1.Item(Col1Difference, I).Value) <> 0 Then
                    With Stock
                        .UID = mSearchCode
                        .DocID = mInternalCode
                        .Sr = I
                        .V_Type = TxtV_Type.AgSelectedValue
                        .V_Prefix = LblPrefix.Text
                        .V_Date = TxtV_Date.Text
                        .V_No = TxtV_No.Text
                        .RecID = TxtV_No.Text
                        .Div_Code = TxtDivision.AgSelectedValue
                        .Site_Code = TxtSite_Code.AgSelectedValue
                        .Item = Dgl1.AgSelectedValue(Col1Item, I)
                        .Godown = TxtGodown.AgSelectedValue
                        .Qty_Iss = IIf(Val(Dgl1.Item(Col1Difference, I).Value) > 0, Math.Abs(Val(Dgl1.Item(Col1Difference, I).Value)), 0)
                        .Qty_Rec = IIf(Val(Dgl1.Item(Col1Difference, I).Value) < 0, Math.Abs(Val(Dgl1.Item(Col1Difference, I).Value)), 0)
                        .Unit = Dgl1.Item(Col1Unit, I).Value
                        .Rate = Dgl1.Item(Col1Rate, I).Value
                        .Amount = Dgl1.Item(Col1Amount, I).Value
                        .Status = ClsMain.StockStatus.Standard
                    End With
                End If
                Call ClsMain.ProcStockPost("Stock", Stock, Conn, Cmd)
            End If
        Next
    End Sub

    Private Sub Frm_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "StockHead"
        LogTableName = "StockHead_Log"
        MainLineTableCsv = "PhysicalStockDetail"
        LogLineTableCsv = "PhysicalStockDetail_LOG"
        AgL.GridDesign(Dgl1)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = " Select H.DocID As SearchCode " & _
            " From StockHead H " & _
            " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " & _
            " Where IsNull(IsDeleted,0) = 0  " & mCondStr & "  Order By H.V_Date Desc "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMastLog(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMastLog
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"
        mCondStr = mCondStr & " And EntryStatus='" & LogStatus.LogOpen & "' "

        mQry = " Select H.UID As SearchCode " & _
            " From StockHead_Log H " & _
            " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " & _
            " Where 1=1  " & mCondStr & "  Order By H.V_Date Desc "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        'AgL.PubFindQry = " SELECT H.DocID as SearchCode, Vt.Description AS [Entry Type], " & _
        '                    " H.V_Date AS [Entry Date], H.V_No AS [Entry No], G.Description as Godown " & _
        '                    " FROM StockHead H " & _
        '                    " LEFT JOIN voucher_type Vt ON H.V_Type = Vt.V_Type " & _
        '                    " LEFT JOIN Godown G ON H.FromGodown = G.Code " & _
        '                    " Where 1=1  " & mCondStr

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, H.V_Type AS [Adjustment Type], H.V_Prefix AS Prefix, H.V_Date AS Date, H.V_No AS [Entry No], " & _
                " H.FromProcess AS [FROM Process], H.ToProcess AS [TO Process], H.TotalQty AS [Total Qty], H.TotalMeasure AS [Total Measure],  " & _
                " H.Amount, H.Addition, H.Deduction, H.NetAmount, H.Remarks,  H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type],  " & _
                " H.EntryStatus AS [Entry Status], H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date], H.MoveToLog AS [Move To Log],  " & _
                " H.MoveToLogDate AS [Move To Log Date], H.Status, H.ReferenceDocID AS [Reference No], H.Structure, H.OrderBy AS [ORDER By], H.ManualRefNo AS [Manual No], " & _
                " D.Div_Name AS Division,SM.Name AS [Site Name],GF.Description AS [FROM Godown], GT.Description AS [To Godown] " & _
                " FROM  StockHead H  " & _
                " LEFT JOIN Division D ON D.Div_Code=H.Div_Code  " & _
                " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code  " & _
                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                " LEFT JOIN Godown GF ON GF.Code = H.FromGodown  " & _
                " LEFT JOIN Godown GT ON GT.Code = H.ToGodown  " & _
                " Where IsNull(H.IsDeleted,0) = 0  " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindLog() Handles Me.BaseEvent_FindLog
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        'AgL.PubFindQry = " SELECT H.UID as SearchCode, Vt.Description AS [Entry Type], " & _
        '                    " H.V_Date AS [Entry Date], H.V_No AS [Entry No], G.Description as Godown " & _
        '                    " FROM StockHead_Log H " & _
        '                    " LEFT JOIN Voucher_type Vt ON H.V_Type = Vt.V_Type " & _
        '                    " LEFT JOIN Godown G ON H.Godown = G.Code " & _
        '                    " Where H.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'  " & mCondStr

        AgL.PubFindQry = " SELECT H.UID AS SearchCode, H.V_Type AS [Adjustment Type], H.V_Prefix AS Prefix, H.V_Date AS Date, H.V_No AS [Entry No], " & _
                        " H.FromProcess AS [FROM Process], H.ToProcess AS [TO Process], H.TotalQty AS [Total Qty], H.TotalMeasure AS [Total Measure],  " & _
                        " H.Amount, H.Addition, H.Deduction, H.NetAmount, H.Remarks,  H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type],  " & _
                        " H.EntryStatus AS [Entry Status], H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date], H.MoveToLog AS [Move To Log],  " & _
                        " H.MoveToLogDate AS [Move To Log Date], H.Status, H.ReferenceDocID AS [Reference No], H.Structure, H.OrderBy AS [ORDER By], H.ManualRefNo AS [Manual No], " & _
                        " D.Div_Name AS Division,SM.Name AS [Site Name],GF.Description AS [FROM Godown], GT.Description AS [To Godown] " & _
                        " FROM  StockHead_Log H  " & _
                        " LEFT JOIN Division D ON D.Div_Code=H.Div_Code  " & _
                        " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code  " & _
                        " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                        " LEFT JOIN Godown GF ON GF.Code = H.FromGodown  " & _
                        " LEFT JOIN Godown GT ON GT.Code = H.ToGodown  " & _
                        " Where H.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'  " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Item, 200, 0, Col1Item, True, False, False)
            .AddAgTextColumn(Dgl1, Col1Process, 100, 0, Col1Process, True, False, False)
            .AddAgTextColumn(Dgl1, Col1Status, 85, 0, Col1Status, True, False, False)
            .AddAgNumberColumn(Dgl1, Col1CurrentStock, 100, 8, 4, False, Col1CurrentStock, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1PhysicalStock, 100, 8, 4, False, Col1PhysicalStock, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 75, 0, Col1Unit, True, True, False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 70, 8, 2, False, Col1Rate, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 100, 8, 2, False, Col1Amount, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1Difference, 100, 8, 2, False, Col1Difference, True, True, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.Anchor = AnchorStyles.None
        Panel1.Anchor = Dgl1.Anchor
        FrmProductionOrder_BaseFunction_FIniList()
        Dgl1.ColumnHeadersHeight = 35
        Ini_List()
    End Sub


    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer


        mQry = "UPDATE StockHead_Log " & _
                " SET " & _
                " TotalQty = " & Val(LblTotalPhysicalQty.Text) & ", " & _
                " Amount = " & Val(LblTotalAmount.Text) & ", " & _
                " FromGodown = " & AgL.Chk_Text(TxtGodown.AgSelectedValue) & ", " & _
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & " " & _
                " Where UID = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From PhysicalStockDetail_Log Where UID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        With Dgl1
            For I = 0 To .RowCount - 1
                If .Item(Col1Item, I).Value <> "" Then
                    mSr += 1
                    mQry = "INSERT INTO PhysicalStockDetail_Log(UID, DocId, Sr, Item, Process, " & _
                            " Status, CurrentStock, PhysicalStock, Unit, " & _
                            " Rate, Amount, Differnce) " & _
                            " VALUES('" & mSearchCode & "', '" & mInternalCode & "', " & Val(mSr) & ", " & _
                            " " & AgL.Chk_Text(.AgSelectedValue(Col1Item, I)) & ", " & _
                            " " & AgL.Chk_Text(.AgSelectedValue(Col1Process, I)) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col1Status, I).Value) & ", " & _
                            " " & Val(.Item(Col1CurrentStock, I).Value) & ", " & _
                            " " & Val(.Item(Col1PhysicalStock, I).Value) & ", " & _
                            " " & AgL.Chk_Text(.Item(Col1Unit, I).Value) & ", " & _
                            " " & Val(.Item(Col1Rate, I).Value) & ", " & _
                            " " & Val(.Item(Col1Amount, I).Value) & ", " & _
                            " " & Val(.Item(Col1Difference, I).Value) & ")"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    RaiseEvent BaseEvent_Save_InTransLine(SearchCode, mSr, I, Conn, Cmd)
                End If
            Next
        End With

        
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim DsTemp As DataSet

        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = "Select H.* " & _
                " From StockHead H " & _
                " Where H.DocID='" & SearchCode & "'"
        Else
            mQry = "Select H.* " & _
                " From StockHead_Log H " & _
                " Where H.UID='" & SearchCode & "'"

        End If
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                IniGrid()
                TxtGodown.AgSelectedValue = AgL.XNull(.Rows(0)("FromGodown"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                LblTotalPhysicalQty.Text = AgL.VNull(.Rows(0)("TotalQty"))
                LblTotalAmount.Text = AgL.VNull(.Rows(0)("Amount"))

                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------

                If FrmType = ClsMain.EntryPointType.Main Then
                    mQry = "Select * from PhysicalStockDetail where DocId = '" & SearchCode & "' Order By Sr"
                Else
                    mQry = "Select * from PhysicalStockDetail_Log where UID = '" & SearchCode & "' Order By Sr"
                End If

                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.AgSelectedValue(Col1Item, I) = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.AgSelectedValue(Col1Process, I) = AgL.XNull(.Rows(I)("Process"))
                            Dgl1.Item(Col1Status, I).Value = AgL.XNull(.Rows(I)("Status"))
                            Dgl1.Item(Col1CurrentStock, I).Value = AgL.VNull(.Rows(I)("CurrentStock"))
                            Dgl1.Item(Col1PhysicalStock, I).Value = AgL.VNull(.Rows(I)("PhysicalStock"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.XNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1Amount, I).Value = AgL.XNull(.Rows(I)("Amount"))

                            RaiseEvent BaseFunction_MoveRecLine(SearchCode, AgL.VNull(.Rows(I)("Sr")), I)
                        Next I
                    End If
                End With
                Calculation()
                '-------------------------------------------------------------
            End If
        End With

    End Sub

    Private Sub FrmProductionOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating
        Select Case sender.NAME
            Case TxtV_Type.Name
                IniGrid()

        End Select
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        IniGrid()
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = "SELECT I.Code, I.Description, I.Unit, I.ItemType, I.SalesTaxPostingGroup, Measure, MeasureUnit , " & _
                " IsNull(I.IsDeleted ,0) AS IsDeleted, IsNull(I.Status,'" & ClsMain.EntryStatus.Active & "') AS Status, " & _
                " I.Div_Code, I.Unit FROM Item I"
        Dgl1.AgHelpDataSet(Col1Item, 8) = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT Code, Description, Site_Code, IsNull(IsDeleted,0) as IsDeleted, IsNull(Status,'" & ClsMain.EntryStatus.Active & "') AS Status FROM Godown Order By Description"
        TxtGodown.AgHelpDataSet(3, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select P.NCat As Code, Vc.NCatDescription As Process, P.Div_Code " & _
                " From Process P " & _
                " LEFT JOIN VoucherCat Vc On P.NCat  = Vc.NCat " & _
                " Order By Vc.NCatDescription "
        Dgl1.AgHelpDataSet(Col1Process, 1) = AgL.FillData(mQry, AgL.GCn)

        Dgl1.AgHelpDataSet(Col1Status) = AgL.FillData(ClsMain.HelpQueries.StockStatus, AgL.GCn)
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            Case Col1Item
                Dgl1.AgRowFilter(Dgl1.Columns(Col1Item).Index) = " IsDeleted = 0 And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Status='" & ClsMain.EntryStatus.Active & "' "

            Case Col1Process
                Dgl1.AgRowFilter(Dgl1.Columns(Col1Item).Index) = " Div_Code = '" & TxtDivision.AgSelectedValue & "' "

        End Select
    End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Try
            If Topctrl1.Mode = "Browse" Then Exit Sub
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If Dgl1.Item(Col1Item, mRowIndex).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1Item, mRowIndex).ToString.Trim = "" Then
                        Dgl1.Item(Col1Unit, mRowIndex).Value = ""
                        Dgl1.Item(Col1CurrentStock, mRowIndex).Value = 0
                    Else
                        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then
                            DrTemp = Dgl1.AgHelpDataSet(Col1Item).Tables(0).Select("Code = '" & Dgl1.AgSelectedValue(Col1Item, mRowIndex) & "'")
                            Dgl1.Item(Col1Unit, mRowIndex).Value = AgL.XNull(DrTemp(0)("Unit"))
                            Dgl1.Item(Col1CurrentStock, mRowIndex).Value = ClsMain.FunRetStock(Dgl1.AgSelectedValue(Col1Item, mRowIndex), mInternalCode, , TxtGodown.AgSelectedValue, , Dgl1.Item(Col1Status, mRowIndex).Value, TxtV_Date.Text)
                        End If
                    End If
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer

        LblTotalPhysicalQty.Text = 0
        LblTotalAmount.Text = ""

        With Dgl1
            For I = 0 To .RowCount - 1
                If .Item(Col1Item, I).Value <> "" Then
                    .Item(Col1Difference, I).Value = Format(Val(.Item(Col1CurrentStock, I).Value) - Val(.Item(Col1PhysicalStock, I).Value), "0.00")
                    .Item(Col1Amount, I).Value = Format(Val(.Item(Col1PhysicalStock, I).Value) * Val(.Item(Col1Rate, I).Value), "0.00")

                    LblTotalPhysicalQty.Text = Val(LblTotalPhysicalQty.Text) + Val(.Item(Col1PhysicalStock, I).Value)
                    LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(.Item(Col1Amount, I).Value)
                End If
            Next
        End With
        LblTotalPhysicalQty.Text = Format(Val(LblTotalPhysicalQty.Text), "0.000")
        LblTotalAmount.Text = Format(Val(LblTotalAmount.Text), "0.00")
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0

        With Dgl1
            If AgL.RequiredField(TxtGodown, "Godown") Then passed = False : Exit Sub
            If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) = True Then passed = False : Exit Sub
            If AgCL.AgIsDuplicate(Dgl1, CStr(Dgl1.Columns(Col1Item).Index) + "," + CStr(Dgl1.Columns(Col1Process).Index) + "," + CStr(Dgl1.Columns(Col1Status).Index)) = True Then passed = False : Exit Sub

            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1PhysicalStock, I).Value) = 0 Then
                        MsgBox("Physical Stock Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1PhysicalStock, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If
                End If
            Next
        End With
    End Sub

    Private Sub Txt_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtGodown.Enter
        Select Case sender.name
            Case TxtGodown.Name
                TxtGodown.AgRowFilter = " Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And IsDeleted = 0 "

        End Select
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    End Sub
End Class
