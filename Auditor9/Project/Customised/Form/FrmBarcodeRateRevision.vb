Imports System.IO
Imports AgLibrary.ClsMain.agConstants
Imports System.Xml
Imports Customised.ClsMain
Imports System.ComponentModel
Imports System.Linq
Imports System.Data.SqlClient
Imports System.Data.SQLite
Imports CrystalDecisions.CrystalReports.Engine

Public Class FrmBarcodeRateRevision
    Inherits AgTemplate.TempMaster
    Dim mQry$ = ""

    Public Const ColSNo As String = "S.No."
    Public WithEvents DglMain As New AgControls.AgDataGrid
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1BtnDetail As String = "Detail"
    Public Const Col1HeadOriginal As String = "Head Original"
    Public Const Col1LastValue As String = "Last Value"

    Public Const rowV_Date As Integer = 0
    Public Const rowRemark As Integer = 1

    Public Const hcV_Date As String = "Date"
    Public Const hcRemark As String = "Remark"

    Public Const Col1Barcode As String = "Barcode"
    Public Const Col1ItemCategory As String = "Item Category"
    Public Const Col1ItemGroup As String = "Item Group"
    Public Const Col1Item As String = "Item"
    Public Const Col1BarcodeType As String = "Barcode Type"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1PrintQty As String = "Print Qty"
    Public Const Col1Rate_Old As String = "Old Rate"
    Friend WithEvents LblSkipLabels As Label
    Friend WithEvents TxtSkipLables As AgControls.AgTextBox
    Public Const Col1Rate_New As String = "New Rate"

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.MnuOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuImportFromExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportFromDos = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportFromTally = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuBulkEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPrintBarcode = New System.Windows.Forms.ToolStripMenuItem()
        Me.OFDMain = New System.Windows.Forms.OpenFileDialog()
        Me.Pnl2 = New System.Windows.Forms.Panel()
        Me.LblBarcode = New System.Windows.Forms.Label()
        Me.TxtBarcode = New AgControls.AgTextBox()
        Me.LblSkipLabels = New System.Windows.Forms.Label()
        Me.TxtSkipLables = New AgControls.AgTextBox()
        Me.GrpUP.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MnuOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(974, 41)
        Me.Topctrl1.TabIndex = 2
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 564)
        Me.GroupBox1.Size = New System.Drawing.Size(1016, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(6, 568)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(142, 638)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(215, 568)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(400, 568)
        Me.GBoxApprove.Size = New System.Drawing.Size(147, 44)
        Me.GBoxApprove.Text = "Approved By"
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Location = New System.Drawing.Point(3, 23)
        Me.TxtApproveBy.Size = New System.Drawing.Size(141, 18)
        Me.TxtApproveBy.Tag = ""
        '
        'CmdDiscard
        '
        Me.CmdDiscard.Location = New System.Drawing.Point(118, 18)
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(702, 568)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(459, 568)
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
        'Pnl1
        '
        Me.Pnl1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl1.Location = New System.Drawing.Point(14, 47)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(948, 97)
        Me.Pnl1.TabIndex = 0
        '
        'MnuOptions
        '
        Me.MnuOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuImportFromExcel, Me.MnuImportFromDos, Me.MnuImportFromTally, Me.MnuBulkEdit, Me.MnuPrintBarcode})
        Me.MnuOptions.Name = "MnuOptions"
        Me.MnuOptions.Size = New System.Drawing.Size(172, 114)
        '
        'MnuImportFromExcel
        '
        Me.MnuImportFromExcel.Name = "MnuImportFromExcel"
        Me.MnuImportFromExcel.Size = New System.Drawing.Size(171, 22)
        Me.MnuImportFromExcel.Text = "Import From Excel"
        '
        'MnuImportFromDos
        '
        Me.MnuImportFromDos.Name = "MnuImportFromDos"
        Me.MnuImportFromDos.Size = New System.Drawing.Size(171, 22)
        Me.MnuImportFromDos.Text = "Import From Dos"
        '
        'MnuImportFromTally
        '
        Me.MnuImportFromTally.Name = "MnuImportFromTally"
        Me.MnuImportFromTally.Size = New System.Drawing.Size(171, 22)
        Me.MnuImportFromTally.Text = "Import From Tally"
        '
        'MnuBulkEdit
        '
        Me.MnuBulkEdit.Name = "MnuBulkEdit"
        Me.MnuBulkEdit.Size = New System.Drawing.Size(171, 22)
        Me.MnuBulkEdit.Text = "Bulk Edit"
        '
        'MnuPrintBarcode
        '
        Me.MnuPrintBarcode.Name = "MnuPrintBarcode"
        Me.MnuPrintBarcode.Size = New System.Drawing.Size(171, 22)
        Me.MnuPrintBarcode.Text = "Print Barcode"
        '
        'OFDMain
        '
        Me.OFDMain.FileName = "price.xls"
        Me.OFDMain.Filter = "*.xls|*.Xls"
        Me.OFDMain.InitialDirectory = "D:\"
        Me.OFDMain.ShowHelp = True
        Me.OFDMain.Title = "Select Excel File"
        '
        'Pnl2
        '
        Me.Pnl2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl2.Location = New System.Drawing.Point(17, 176)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(945, 382)
        Me.Pnl2.TabIndex = 1020
        '
        'LblBarcode
        '
        Me.LblBarcode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LblBarcode.AutoSize = True
        Me.LblBarcode.BackColor = System.Drawing.Color.Transparent
        Me.LblBarcode.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBarcode.Location = New System.Drawing.Point(15, 153)
        Me.LblBarcode.Name = "LblBarcode"
        Me.LblBarcode.Size = New System.Drawing.Size(136, 14)
        Me.LblBarcode.TabIndex = 3006
        Me.LblBarcode.Text = "Enter Barcode Here"
        '
        'TxtBarcode
        '
        Me.TxtBarcode.AgAllowUserToEnableMasterHelp = False
        Me.TxtBarcode.AgLastValueTag = Nothing
        Me.TxtBarcode.AgLastValueText = Nothing
        Me.TxtBarcode.AgMandatory = False
        Me.TxtBarcode.AgMasterHelp = False
        Me.TxtBarcode.AgNumberLeftPlaces = 8
        Me.TxtBarcode.AgNumberNegetiveAllow = False
        Me.TxtBarcode.AgNumberRightPlaces = 2
        Me.TxtBarcode.AgPickFromLastValue = False
        Me.TxtBarcode.AgRowFilter = ""
        Me.TxtBarcode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBarcode.AgSelectedValue = Nothing
        Me.TxtBarcode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBarcode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBarcode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TxtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBarcode.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBarcode.Location = New System.Drawing.Point(153, 151)
        Me.TxtBarcode.MaxLength = 20
        Me.TxtBarcode.Name = "TxtBarcode"
        Me.TxtBarcode.Size = New System.Drawing.Size(141, 19)
        Me.TxtBarcode.TabIndex = 1
        '
        'LblSkipLabels
        '
        Me.LblSkipLabels.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblSkipLabels.AutoSize = True
        Me.LblSkipLabels.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSkipLabels.Location = New System.Drawing.Point(775, 156)
        Me.LblSkipLabels.Name = "LblSkipLabels"
        Me.LblSkipLabels.Size = New System.Drawing.Size(81, 13)
        Me.LblSkipLabels.TabIndex = 3008
        Me.LblSkipLabels.Text = "Skip Lables"
        '
        'TxtSkipLables
        '
        Me.TxtSkipLables.AgAllowUserToEnableMasterHelp = False
        Me.TxtSkipLables.AgLastValueTag = Nothing
        Me.TxtSkipLables.AgLastValueText = Nothing
        Me.TxtSkipLables.AgMandatory = False
        Me.TxtSkipLables.AgMasterHelp = False
        Me.TxtSkipLables.AgNumberLeftPlaces = 2
        Me.TxtSkipLables.AgNumberNegetiveAllow = False
        Me.TxtSkipLables.AgNumberRightPlaces = 0
        Me.TxtSkipLables.AgPickFromLastValue = False
        Me.TxtSkipLables.AgRowFilter = ""
        Me.TxtSkipLables.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSkipLables.AgSelectedValue = Nothing
        Me.TxtSkipLables.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSkipLables.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtSkipLables.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtSkipLables.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSkipLables.Location = New System.Drawing.Point(862, 152)
        Me.TxtSkipLables.MaxLength = 20
        Me.TxtSkipLables.Name = "TxtSkipLables"
        Me.TxtSkipLables.Size = New System.Drawing.Size(100, 21)
        Me.TxtSkipLables.TabIndex = 3007
        '
        'FrmBarcodeRateRevision
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(974, 612)
        Me.ContextMenuStrip = Me.MnuOptions
        Me.Controls.Add(Me.LblSkipLabels)
        Me.Controls.Add(Me.TxtSkipLables)
        Me.Controls.Add(Me.LblBarcode)
        Me.Controls.Add(Me.TxtBarcode)
        Me.Controls.Add(Me.Pnl2)
        Me.Controls.Add(Me.Pnl1)
        Me.MaximizeBox = True
        Me.Name = "FrmBarcodeRateRevision"
        Me.Text = "Buyer Master"
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Pnl2, 0)
        Me.Controls.SetChildIndex(Me.TxtBarcode, 0)
        Me.Controls.SetChildIndex(Me.LblBarcode, 0)
        Me.Controls.SetChildIndex(Me.TxtSkipLables, 0)
        Me.Controls.SetChildIndex(Me.LblSkipLabels, 0)
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
        Me.MnuOptions.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MnuOptions As ContextMenuStrip
    Private components As System.ComponentModel.IContainer
    Friend WithEvents MnuImportFromExcel As ToolStripMenuItem
    Friend WithEvents MnuImportFromTally As ToolStripMenuItem
    Public WithEvents OFDMain As OpenFileDialog
    Friend WithEvents MnuBulkEdit As ToolStripMenuItem
    Friend WithEvents MnuPrintBarcode As ToolStripMenuItem
    Friend WithEvents MnuImportFromDos As ToolStripMenuItem
    Friend WithEvents Pnl1 As Panel
    Public WithEvents Pnl2 As Panel
    Public WithEvents LblBarcode As Label
    Public WithEvents TxtBarcode As AgControls.AgTextBox
#End Region

    Private Sub FrmShade_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        AgL.PubFindQry = " SELECT Code AS SearchCode, Remark As Remark 
                    FROM BarcodeRateRevision "
        AgL.PubFindQryOrdBy = "[Remark]"
    End Sub
    Private Sub FrmShade_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "BarcodeRateRevision"
        MainLineTableCsv = "BarcodeRateRevisionDetail"
    End Sub
    Private Sub FrmShade_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        mQry = "Select S.Code As SearchCode From BarcodeRateRevision S Where 1=1 "
        mQry += " Order by S.Code "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub
    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        mQry = "UPDATE BarcodeRateRevision " &
                " SET " &
                " V_Date = " & AgL.Chk_Date(DglMain(Col1Value, rowV_Date).Value) & ", " &
                " Remark = " & AgL.Chk_Text(DglMain(Col1Value, rowRemark).Value) & " " &
                " Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "DELETE FROM BarcodeRateRevisionDetail WHERE Code = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        Dim mSr As Integer = 0
        For I As Integer = 0 To Dgl1.Rows.Count - 1
            mSr += 1
            mQry = " INSERT INTO BarcodeRateRevisionDetail(Code, Sr, Barcode, PrintQty, Rate_Old, Rate_New)
                    Values('" & SearchCode & "', " & mSr & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, I).Tag) & ",
                    " & Val(Dgl1.Item(Col1PrintQty, I).Value) & ", 
                    " & Val(Dgl1.Item(Col1Rate_Old, I).Value) & ", 
                    " & Val(Dgl1.Item(Col1Rate_New, I).Value) & ")"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " UPDATE Barcode 
                    Set SaleRate = " & Val(Dgl1.Item(Col1Rate_New, I).Value) & "
                    Where Code = " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, I).Tag) & ""
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " UPDATE RateListDetail 
                    Set Rate = " & Val(Dgl1.Item(Col1Rate_New, I).Value) & "
                    Where Item In (Select Item From Barcode Where Code = " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, I).Tag) & ")"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " UPDATE Item
                    Set Rate = " & Val(Dgl1.Item(Col1Rate_New, I).Value) & "
                    Where Code In (Select Item From Barcode Where Code = " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, I).Tag) & ")"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        Next
    End Sub
    Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DtTemp As DataTable
        Dim I As Integer

        mQry = "Select S.* 
                From BarcodeRateRevision S 
                Where S.Code='" & SearchCode & "'"
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        With DtTemp
            If .Rows.Count > 0 Then
                DglMain.Item(Col1Value, rowV_Date).Value = AgL.XNull(.Rows(0)("V_Date"))
                DglMain.Item(Col1Value, rowRemark).Value = AgL.XNull(.Rows(0)("Remark"))
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
            End If
        End With

        mQry = "Select Bc.Description As BarcodeDesc, 
                I.Description As ItemDesc, I.Code As ItemCode,
                IC.Description As ItemCategoryDesc, 
                IG.Description As ItemGroupDesc, Bc.Qty, Bc.BarcodeType, L.* 
                From BarcodeRateRevisionDetail L
                LEFT JOIN Barcode Bc On L.Barcode = Bc.Code
                LEFT JOIN Item I ON Bc.Item = I.Code
                Left Join Item IG On I.ItemGroup = IG.Code
                Left Join Item IC On I.ItemCategory = IC.Code
                where L.Code = '" & mSearchCode & "'"
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        With DtTemp
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                    Dgl1.Item(Col1Barcode, I).Tag = AgL.XNull(.Rows(I)("Barcode"))
                    Dgl1.Item(Col1Barcode, I).Value = AgL.XNull(.Rows(I)("BarcodeDesc"))
                    Dgl1.Item(Col1ItemCategory, I).Value = AgL.XNull(.Rows(I)("ItemCategoryDesc"))
                    Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupDesc"))
                    Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("ItemCode"))
                    Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                    Dgl1.Item(Col1BarcodeType, I).Value = AgL.XNull(.Rows(I)("BarcodeType"))
                    Dgl1.Item(Col1Qty, I).Value = AgL.XNull(.Rows(I)("Qty"))
                    Dgl1.Item(Col1PrintQty, I).Value = AgL.XNull(.Rows(I)("PrintQty"))
                    Dgl1.Item(Col1Rate_Old, I).Value = AgL.XNull(.Rows(I)("Rate_Old"))
                    Dgl1.Item(Col1Rate_New, I).Value = AgL.XNull(.Rows(I)("Rate_New"))
                Next I
            End If
        End With
        TxtSkipLables.Enabled = True
    End Sub
    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub
    Private Sub FrmParty_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        If DglMain.Rows(rowRemark).Visible = True Then
            DglMain(Col1Value, rowV_Date).Value = AgL.PubLoginDate
            DglMain.CurrentCell = DglMain(Col1Value, rowRemark)
            DglMain.Focus()
        End If
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Private Sub FrmBarcodeRateRevision_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dim I As Integer

        DglMain.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DglMain, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(DglMain, Col1Head, 250, 255, Col1Head, True, True)
            .AddAgTextColumn(DglMain, Col1Mandatory, 12, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(DglMain, Col1Value, 630, 255, Col1Value, True, False)
            .AddAgButtonColumn(DglMain, Col1BtnDetail, 35, Col1BtnDetail, True, True)
            .AddAgTextColumn(DglMain, Col1HeadOriginal, 200, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(DglMain, Col1LastValue, 200, 255, Col1LastValue, False, True)

        End With
        AgL.AddAgDataGrid(DglMain, Pnl1)
        DglMain.EnableHeadersVisualStyles = False
        DglMain.Columns(Col1Mandatory).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings 2", 5.25, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        DglMain.Columns(Col1Mandatory).DefaultCellStyle.ForeColor = Color.Red
        DglMain.ColumnHeadersHeight = 35
        DglMain.AgSkipReadOnlyColumns = True
        DglMain.AllowUserToAddRows = False
        DglMain.RowHeadersVisible = False
        DglMain.ColumnHeadersVisible = False
        DglMain.BackgroundColor = Me.BackColor
        DglMain.BorderStyle = BorderStyle.None
        DglMain.AgAllowFind = False
        DglMain.Name = "DglMain"
        AgL.GridDesign(DglMain)
        DglMain.Anchor = AnchorStyles.Top + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom


        DglMain.Rows.Add(2)

        DglMain.Item(Col1Head, rowV_Date).Value = hcV_Date
        DglMain.Item(Col1Head, rowRemark).Value = hcRemark
        DglMain.Rows(rowRemark).Height = 50

        For I = 0 To DglMain.Rows.Count - 1
            DglMain(Col1HeadOriginal, I).Value = DglMain(Col1Head, I).Value
        Next

        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Barcode, 100, 0, Col1Barcode, False, True)
            .AddAgTextColumn(Dgl1, Col1ItemCategory, 100, 0, Col1ItemCategory, False, True)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 100, 0, Col1ItemGroup, False, True)
            .AddAgTextColumn(Dgl1, Col1Item, 250, 0, Col1Item, True, True)
            .AddAgTextColumn(Dgl1, Col1BarcodeType, 65, 0, " ", False, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 80, 2, 2, False, Col1Qty, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1PrintQty, 80, 2, 2, False, Col1PrintQty, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Rate_Old, 80, 2, 2, False, Col1Rate_Old, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1Rate_New, 80, 7, 2, False, Col1Rate_New, True, False, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl2)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.RowHeadersVisible = False
        DglMain.AllowUserToAddRows = False
        Dgl1.ColumnHeadersHeight = 40
        Dgl1.Visible = False
        Dgl1.BackgroundColor = Me.BackColor
        AgL.GridDesign(Dgl1)
        Dgl1.AllowUserToAddRows = False
        Dgl1.Name = "Dgl1"
        Dgl1.Anchor = AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom

        ApplyUISetting()

    End Sub
    Private Sub FrmBarcodeRateRevision_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dim I As Integer
        For I = 0 To DglMain.Rows.Count - 1
            DglMain.Item(Col1Value, I).Value = ""
            DglMain.Item(Col1Value, I).Tag = ""
            DglMain.Item(Col1BtnDetail, I).Tag = Nothing
            DglMain.Item(Col1BtnDetail, I) = New DataGridViewTextBoxCell
            DglMain(Col1BtnDetail, I).ReadOnly = True
        Next
        Dgl1.Rows.Clear()
    End Sub
    Private Sub FrmBarcodeRateRevision_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer

        passed = AgCL.AgCheckMandatory(Me)

        For I = 0 To DglMain.RowCount - 1
            If DglMain(Col1Mandatory, I).Value <> "" And DglMain.Rows(I).Visible Then
                If DglMain(Col1Value, I).Value = "" And DglMain(Col1BtnDetail, I).Value = "" Then
                    MsgBox(DglMain(Col1Head, I).Value & " can not be blank.")
                    DglMain.CurrentCell = DglMain(Col1Value, I)
                    DglMain.Focus()
                    passed = False
                    Exit Sub
                End If
            End If
        Next

        If Dgl1.Rows.Count = 0 Then
            MsgBox(" Line Detail can not be blank.", MsgBoxStyle.Information)
            passed = False : Exit Sub
        End If

        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Barcode, I).Value <> "" Then
                If Val(Dgl1.Item(Col1Rate_New, I).Value) = 0 Then
                    MsgBox(" New Rate can not be blank.", MsgBoxStyle.Information)
                    Dgl1.CurrentCell = Dgl1(Col1Rate_New, I)
                    Dgl1.Focus() : passed = False : Exit Sub
                End If
            End If
        Next
    End Sub
    Private Sub Dgl1_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles DglMain.EditingControl_Validating
        If DglMain.CurrentCell.ColumnIndex = DglMain.Columns(Col1Value).Index Then
            If DglMain.Item(Col1Mandatory, DglMain.CurrentCell.RowIndex).Value <> "" Then
                If DglMain(Col1Value, DglMain.CurrentCell.RowIndex).Value = "" Then
                    MsgBox(DglMain(Col1Head, DglMain.CurrentCell.RowIndex).Value & " can not be blank.")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Private Sub FrmBarcodeRateRevision_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Dim I As Integer
        For I = 0 To DglMain.Rows.Count - 1
            DglMain(Col1Head, I).Tag = Nothing
        Next
    End Sub
    Private Sub Dgl1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DglMain.CellContentClick
        If e.ColumnIndex = DglMain.Columns(Col1BtnDetail).Index And TypeOf (DglMain(Col1BtnDetail, e.RowIndex)) Is DataGridViewButtonCell Then
            Select Case e.RowIndex


            End Select
        End If
    End Sub
    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles DglMain.KeyDown
        If DglMain.CurrentCell Is Nothing Then Exit Sub
        If ClsMain.IsSpecialKeyPressed(e) Then Exit Sub
        If e.KeyCode = Keys.Enter Then Exit Sub
        If Topctrl1.Mode.ToUpper <> "BROWSE" Then
            If DglMain.CurrentCell.ColumnIndex = DglMain.Columns(Col1Value).Index Then
                If e.KeyCode = Keys.Delete Then
                    DglMain(Col1Value, DglMain.CurrentCell.RowIndex).Value = ""
                    DglMain(Col1Value, DglMain.CurrentCell.RowIndex).Tag = ""
                End If

                Select Case DglMain.CurrentCell.RowIndex
                End Select

                DglMain.DefaultCellStyle.WrapMode = DataGridViewTriState.True
                For I As Integer = 0 To DglMain.Rows.Count - 1
                    If AgL.XNull(DglMain.Item(Col1Value, I).Value) <> "" Then
                        If DglMain.Item(Col1Value, I).Value.ToString.Length < 500 Then
                            DglMain.AutoResizeRow(I, DataGridViewAutoSizeRowMode.AllCells)
                        Else
                            DglMain.Rows(I).Height = 50
                        End If
                    End If
                Next
            End If
        End If
    End Sub
    Private Sub Mnu_Click(sender As Object, e As EventArgs) Handles MnuImportFromExcel.Click, MnuPrintBarcode.Click
        Select Case sender.name
            Case MnuPrintBarcode.Name
                Dim FrmObj As FrmPrintBarcode
                FrmObj = New FrmPrintBarcode()
                FrmObj.DocId = mSearchCode
                FrmObj.PrintBarcodeFrom = Me.Name
                FrmObj.LblTitle.Text = Me.Name + "-" + DglMain.Item(Col1Value, rowV_Date).Value
                'FrmObj.LblTitle.Text = DglMain.Item(Col1Value, rowV_Type).Value + " - " + DglMain.Item(Col1Value, rowReferenceNo).Value
                FrmObj.StartPosition = FormStartPosition.CenterParent
                FrmObj.ShowDialog()


        End Select
    End Sub
    Private Sub TxtBarcode_Validating(sender As Object, e As CancelEventArgs) Handles TxtBarcode.Validating
        Dim DtBarcode As DataTable
        Dim DtBarcodeSiteDetail As DataTable

        If TxtBarcode.Text = "" Then Exit Sub
        If Validate_Barcode(sender.text) = False Then TxtBarcode.Text = "" : e.Cancel = True : Exit Sub

        mQry = "Select I.V_Type As ItemV_Type, I.Description As ItemDesc, 
                IC.Description As ItemCategoryDesc, 
                IG.Description As ItemGroupDesc, B.* 
                From Barcode B With (NoLock) 
                LEFT JOIN Item I ON B.Item = I.Code
                Left Join Item IG On I.ItemGroup = IG.Code
                Left Join Item IC On I.ItemCategory = IC.Code
                Where B.Description = '" & TxtBarcode.Text & "'"
        DtBarcode = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtBarcode.Rows.Count = 0 Then
            MsgBox("Invalid Barcode")
            TxtBarcode.Text = ""
            e.Cancel = True
            Exit Sub
        Else
            If AgL.XNull(DtBarcode.Rows(0)("Div_Code")) <> TxtDivision.Tag Then
                MsgBox("Barcode does not belong to current division. Can not continue.")
                TxtBarcode.Text = ""
                e.Cancel = True
                Exit Sub
            End If

            mQry = "Select Bs.* 
                    from BarcodeSiteDetail Bs With (NoLock) 
                    LEFT JOIN Barcode B On Bs.Code = B.Code
                    Where Bs.Code = '" & DtBarcode.Rows(0)("Code") & "' 
                    And Bs.Div_Code='" & TxtDivision.Tag & "' 
                    And Bs.Site_Code = '" & AgL.PubSiteCode & "' "
            DtBarcodeSiteDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtBarcodeSiteDetail.Rows.Count = 0 Then
                MsgBox("No record found for barcode for current site. Can not continue.")
                TxtBarcode.Text = ""
                e.Cancel = True
                Exit Sub
            End If


            Dim mRow As Integer
            mRow = Dgl1.Rows.Add()
            Dgl1.Item(ColSNo, mRow).Value = Dgl1.Rows.Count
            Dgl1.Item(Col1Barcode, mRow).Tag = AgL.XNull(DtBarcode.Rows(0)("Code"))
            Dgl1.Item(Col1Barcode, mRow).Value = AgL.XNull(DtBarcode.Rows(0)("Description"))
            Dgl1.Item(Col1ItemCategory, mRow).Value = AgL.XNull(DtBarcode.Rows(0)("ItemCategoryDesc"))
            Dgl1.Item(Col1ItemGroup, mRow).Value = AgL.XNull(DtBarcode.Rows(0)("ItemGroupDesc"))
            Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(DtBarcode.Rows(0)("ItemDesc"))
            Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(DtBarcode.Rows(0)("Qty"))
            Dgl1.Item(Col1PrintQty, mRow).Value = AgL.VNull(DtBarcode.Rows(0)("Qty"))
            Dgl1.Item(Col1Rate_Old, mRow).Value = AgL.VNull(DtBarcode.Rows(0)("SaleRate"))


            Calculation()
            Calculation()
            TxtBarcode.Text = ""
            TxtBarcode.Focus()
        End If
        Calculation()
    End Sub
    Private Function Validate_Barcode(BarcodeDescription As String) As Boolean
        Dim DtBarcodeLastValues As DataTable


        Validate_Barcode = True
    End Function
    Private Sub ApplyUISetting()
        GetUISetting(DglMain, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", ClsMain.GridTypeConstants.VerticalGrid)
        GetUISetting(Dgl1, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", ClsMain.GridTypeConstants.HorizontalGrid)
    End Sub
    Private Sub DglMain_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DglMain.CellEnter
        Try
            If DglMain.CurrentCell Is Nothing Then Exit Sub
            If Topctrl1.Mode = "BROWSE" Then
                DglMain.CurrentCell.ReadOnly = True
            End If

            If DglMain.CurrentCell.ColumnIndex <> DglMain.Columns(Col1Value).Index Then Exit Sub


            DglMain.AgHelpDataSet(DglMain.CurrentCell.ColumnIndex) = Nothing
            CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0

            Select Case DglMain.CurrentCell.RowIndex
                Case rowV_Date
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
                    DglMain.Rows(rowV_Date).ReadOnly = True
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmBarcodeRateRevision_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        MsgBox("This entry can not be edited.You can revise rates with new entry.", MsgBoxStyle.Information)
        Passed = False
    End Sub
    Private Sub FrmBarcodeRateRevision_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        MsgBox("This entry can not be edited.You can revise rates with new entry.", MsgBoxStyle.Information)
        Passed = False
    End Sub
    Private Sub FrmBarcodeRateRevision_BaseEvent_Topctrl_tbPrn(SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        FHPGD_PendingBarcodeToPrint()
    End Sub

    Private Function FHPGD_PendingBarcodeToPrint() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim strCond As String = ""
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Dim DtMain As New DataTable

        If Val(TxtSkipLables.Text) > 0 Then
            mQry = "Select 'o' As Tick, Cast(0 As BigInt) As Code, '' As Barcode, 
                    '' As ItemDesc, '' As ItemCategoryDesc, '' As ItemGroupDesc, 
                    '' as Dimension1Desc, '' as Dimension2Desc,
                    '' as Dimension3Desc, '' as Dimension4Desc, 
                    '' as SizeDesc, CAST(0 AS DECIMAL(18,2)) As PurchaseRate, 
                    CAST(0 AS DECIMAL(18,2)) As SaleRate, CAST(0 AS DECIMAL(18,2)) As MRP,
                    " & Val(TxtSkipLables.Text) & " As Qty,CAST(0 AS DECIMAL(18,2)) As ReceiveQty "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            DtMain.Merge(DtTemp)
        End If

        For I = 0 To Dgl1.Rows.Count - 1
            mQry = "Select 'o' As Tick, B.Code As Code, B.Description As Barcode, 
                        Case When Sku.V_Type = '" & ItemV_Type.SKU & "' Then I.Specification Else Sku.Specification End as ItemDesc,
                        IC.Description As ItemCategoryDesc, IG.Description As ItemGroupDesc, 
                        D1.Specification as Dimension1Desc, D2.Specification as Dimension2Desc,
                        D3.Specification as Dimension3Desc, D4.Specification as Dimension4Desc, 
                        Size.Specification as SizeDesc, CAST(B.PurchaseRate AS DECIMAL(18,2)) As PurchaseRate, 
                        CAST(B.SaleRate AS DECIMAL(18,2)) As SaleRate, CAST(B.MRP AS DECIMAL(18,2)) As MRP,
                        Cast(" & IIf(Dgl1.Item(Col1BarcodeType, I).Value = BarcodeType.UniquePerPcs, "B.Qty", Val(Dgl1.Item(Col1PrintQty, I).Value)) & " As Integer) as Qty, CAST(B.Qty AS DECIMAL(18,2)) As ReceiveQty
                        From BarCode B 
                        LEFT JOIN Item Sku on B.Item = Sku.Code  
                        Left Join Item IC On Sku.ItemCategory = IC.Code
                        Left Join Item IG On Sku.ItemGroup = IG.Code
                        LEFT JOIN Item I ON Sku.BaseItem = I.Code
                        LEFT JOIN Item D1 ON Sku.Dimension1 = D1.Code
                        LEFT JOIN Item D2 ON Sku.Dimension2 = D2.Code
                        LEFT JOIN Item D3 ON Sku.Dimension3 = D3.Code
                        LEFT JOIN Item D4 ON Sku.Dimension4 = D4.Code
                        LEFT JOIN Item Size ON Sku.Size = Size.Code
                        Where B.Code = '" & Dgl1.Item(Col1Barcode, I).Tag & "'"
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTemp.Rows.Count > 0 Then DtMain.Merge(DtTemp)
        Next


        PrintBarcodes(DtMain)
    End Function
    Private Sub PrintBarcodes(ByVal DtTemp As DataTable)
        'Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0, J As Integer = 0
        Dim bTempTable$ = ""
        Dim StrCondBale As String = ""
        Dim mCrd As New ReportDocument
        Dim ReportView As New AgLibrary.RepView
        Dim DsRep As New DataSet
        Dim RepName As String = "", RepTitle As String = ""

        Try
            RepName = "RepBarCodeImage" : RepTitle = "Item Barcode"
            Dim mDocReportFileName As String = FGetSettings(SettingFields.BarcodePrintReportFileName, SettingType.General)
            Dim mBarcodePrintTitle1 As String = FGetSettings(SettingFields.BarcodePrintTitle1, SettingType.General)
            Dim mBarcodePrintTitle2 As String = FGetSettings(SettingFields.BarcodePrintTitle2, SettingType.General)
            Dim mBarcodePrintTitle3 As String = FGetSettings(SettingFields.BarcodePrintTitle3, SettingType.General)
            Dim mBarcodeRatePrefix As String = FGetSettings(SettingFields.BarcodePrintSaleRatePrefix, SettingType.General)

            If mDocReportFileName = "" Then
                RepName = "Barcode_Print.rpt"
                'RepName = "Barcode_Print_3838.rpt"
            Else
                RepName = mDocReportFileName
            End If

            bTempTable = Guid.NewGuid.ToString   'AgL.GetGUID(AgL.GCn).ToString

            mQry = "CREATE TEMPORARY TABLE [#" & bTempTable & "] " &
                    " (Barcode nVarChar(100), BarCodeImg Image, ItemDesc nVarChar(100), 
                        ItemCategoryDesc nVarChar(100), ItemGroupDesc nVarChar(100), 
                        Dimension1Desc nVarChar(100), Dimension2Desc nVarChar(100),
                        Dimension3Desc nVarChar(100), Dimension4Desc nVarChar(100), 
                        SizeDesc nVarChar(100), PurchaseRate Float, SaleRate Float, MRP Float, ReceiveQty INT) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 1 To Val(DtTemp.Rows(I)("Qty"))
                        Dim sSQL As String = "Insert Into [#" & bTempTable & "] (Barcode, BarCodeImg, ItemDesc, ItemCategoryDesc, ItemGroupDesc, 
                        Dimension1Desc, Dimension2Desc, Dimension3Desc, Dimension4Desc, SizeDesc, PurchaseRate, SaleRate, MRP, ReceiveQty) " &
                        " Values(@Barcode, @BarCodeImg, @ItemDesc, @ItemCategoryDesc, @ItemGroupDesc, 
                        @Dimension1Desc, @Dimension2Desc, @Dimension3Desc, @Dimension4Desc, @SizeDesc, @PurchaseRate, @SaleRate, @MRP, @ReceiveQty)"
                        sSQL = AgL.GetBackendBasedQuery(sSQL)
                        If AgL.PubServerName = "" Then
                            Dim cmd As SQLiteCommand = New SQLiteCommand(sSQL, AgL.GCn)

                            Dim Barcode As SQLiteParameter = New SQLiteParameter("@Barcode", DbType.String)
                            Dim BarCodeImg As SQLiteParameter = New SQLiteParameter("@BarCodeImg", DbType.Binary)
                            Dim ItemDesc As SQLiteParameter = New SQLiteParameter("@ItemDesc", DbType.String)
                            Dim ItemCategoryDesc As SQLiteParameter = New SQLiteParameter("@ItemCategoryDesc", DbType.String)
                            Dim ItemGroupDesc As SQLiteParameter = New SQLiteParameter("@ItemGroupDesc", DbType.String)
                            Dim Dimension1Desc As SQLiteParameter = New SQLiteParameter("@Dimension1Desc", DbType.String)
                            Dim Dimension2Desc As SQLiteParameter = New SQLiteParameter("@Dimension2Desc", DbType.String)
                            Dim Dimension3Desc As SQLiteParameter = New SQLiteParameter("@Dimension3Desc", DbType.String)
                            Dim Dimension4Desc As SQLiteParameter = New SQLiteParameter("@Dimension4Desc", DbType.String)
                            Dim SizeDesc As SQLiteParameter = New SQLiteParameter("@SizeDesc", DbType.String)
                            Dim PurchaseRate As SQLiteParameter = New SQLiteParameter("@PurchaseRate", DbType.String)
                            Dim SaleRate As SQLiteParameter = New SQLiteParameter("@SaleRate", DbType.String)
                            Dim MRP As SQLiteParameter = New SQLiteParameter("@MRP", DbType.String)
                            Dim ReceiveQty As SQLiteParameter = New SQLiteParameter("@ReceiveQty", DbType.String)

                            Barcode.Value = AgL.XNull(DtTemp.Rows(I)("Barcode"))
                            ItemDesc.Value = AgL.XNull(DtTemp.Rows(I)("ItemDesc"))
                            ItemCategoryDesc.Value = AgL.XNull(DtTemp.Rows(I)("ItemCategoryDesc"))
                            ItemGroupDesc.Value = AgL.XNull(DtTemp.Rows(I)("ItemGroupDesc"))
                            Dimension1Desc.Value = AgL.XNull(DtTemp.Rows(I)("Dimension1Desc"))
                            Dimension2Desc.Value = AgL.XNull(DtTemp.Rows(I)("Dimension2Desc"))
                            Dimension3Desc.Value = AgL.XNull(DtTemp.Rows(I)("Dimension3Desc"))
                            Dimension4Desc.Value = AgL.XNull(DtTemp.Rows(I)("Dimension4Desc"))
                            SizeDesc.Value = AgL.XNull(DtTemp.Rows(I)("SizeDesc"))
                            PurchaseRate.Value = AgL.VNull(DtTemp.Rows(I)("PurchaseRate"))
                            SaleRate.Value = AgL.VNull(DtTemp.Rows(I)("SaleRate"))
                            MRP.Value = AgL.VNull(DtTemp.Rows(I)("MRP"))
                            ReceiveQty.Value = AgL.VNull(DtTemp.Rows(I)("ReceiveQty"))



                            If AgL.XNull(DtTemp.Rows(I)("Barcode")) <> "" Then
                                BarCodeImg.Value = GetBarcodeImage(AgL.XNull(DtTemp.Rows(I)("Barcode")), 200, 50)
                            Else
                                BarCodeImg.Value = GetBarcodeImage("0", 200, 50)
                            End If


                            cmd.Parameters.Add(Barcode)
                            cmd.Parameters.Add(BarCodeImg)
                            cmd.Parameters.Add(ItemDesc)
                            cmd.Parameters.Add(ItemCategoryDesc)
                            cmd.Parameters.Add(ItemGroupDesc)
                            cmd.Parameters.Add(Dimension1Desc)
                            cmd.Parameters.Add(Dimension2Desc)
                            cmd.Parameters.Add(Dimension3Desc)
                            cmd.Parameters.Add(Dimension4Desc)
                            cmd.Parameters.Add(SizeDesc)
                            cmd.Parameters.Add(PurchaseRate)
                            cmd.Parameters.Add(SaleRate)
                            cmd.Parameters.Add(MRP)
                            cmd.Parameters.Add(ReceiveQty)


                            cmd.ExecuteNonQuery()

                        Else
                            Dim cmd As SqlCommand = New SqlCommand(sSQL, AgL.GCn)

                            Dim Barcode As SqlParameter = New SqlParameter("@Barcode", DbType.String)
                            Dim BarCodeImg As SqlParameter = New SqlParameter("@BarCodeImg", DbType.Binary)
                            Dim ItemDesc As SqlParameter = New SqlParameter("@ItemDesc", DbType.String)
                            Dim ItemCategoryDesc As SqlParameter = New SqlParameter("@ItemCategoryDesc", DbType.String)
                            Dim ItemGroupDesc As SqlParameter = New SqlParameter("@ItemGroupDesc", DbType.String)
                            Dim Dimension1Desc As SqlParameter = New SqlParameter("@Dimension1Desc", DbType.String)
                            Dim Dimension2Desc As SqlParameter = New SqlParameter("@Dimension2Desc", DbType.String)
                            Dim Dimension3Desc As SqlParameter = New SqlParameter("@Dimension3Desc", DbType.String)
                            Dim Dimension4Desc As SqlParameter = New SqlParameter("@Dimension4Desc", DbType.String)
                            Dim SizeDesc As SqlParameter = New SqlParameter("@SizeDesc", DbType.String)
                            Dim PurchaseRate As SqlParameter = New SqlParameter("@PurchaseRate", DbType.String)
                            Dim SaleRate As SqlParameter = New SqlParameter("@SaleRate", DbType.String)
                            Dim MRP As SqlParameter = New SqlParameter("@MRP", DbType.String)
                            Dim ReceiveQty As SqlParameter = New SqlParameter("@ReceiveQty", DbType.String)


                            Barcode.Value = DtTemp.Rows(I)("Barcode")
                            ItemDesc.Value = DtTemp.Rows(I)("ItemDesc")
                            ItemCategoryDesc.Value = AgL.XNull(DtTemp.Rows(I)("ItemCategoryDesc"))
                            ItemGroupDesc.Value = AgL.XNull(DtTemp.Rows(I)("ItemGroupDesc"))
                            Dimension1Desc.Value = AgL.XNull(DtTemp.Rows(I)("Dimension1Desc"))
                            Dimension2Desc.Value = AgL.XNull(DtTemp.Rows(I)("Dimension2Desc"))
                            Dimension3Desc.Value = AgL.XNull(DtTemp.Rows(I)("Dimension3Desc"))
                            Dimension4Desc.Value = AgL.XNull(DtTemp.Rows(I)("Dimension4Desc"))
                            SizeDesc.Value = AgL.XNull(DtTemp.Rows(I)("SizeDesc"))
                            PurchaseRate.Value = AgL.VNull(DtTemp.Rows(I)("PurchaseRate"))
                            SaleRate.Value = AgL.VNull(DtTemp.Rows(I)("SaleRate"))
                            MRP.Value = AgL.VNull(DtTemp.Rows(I)("MRP"))
                            ReceiveQty.Value = AgL.VNull(DtTemp.Rows(I)("ReceiveQty"))


                            If AgL.XNull(DtTemp.Rows(I)("Barcode")) <> "" Then
                                BarCodeImg.Value = GetBarcodeImage(AgL.XNull(DtTemp.Rows(I)("Barcode")), 200, 50)
                            Else
                                BarCodeImg.Value = GetBarcodeImage("0", 200, 50)
                            End If


                            cmd.Parameters.Add(Barcode)
                            cmd.Parameters.Add(BarCodeImg)
                            cmd.Parameters.Add(ItemDesc)
                            cmd.Parameters.Add(ItemCategoryDesc)
                            cmd.Parameters.Add(ItemGroupDesc)
                            cmd.Parameters.Add(Dimension1Desc)
                            cmd.Parameters.Add(Dimension2Desc)
                            cmd.Parameters.Add(Dimension3Desc)
                            cmd.Parameters.Add(Dimension4Desc)
                            cmd.Parameters.Add(SizeDesc)
                            cmd.Parameters.Add(PurchaseRate)
                            cmd.Parameters.Add(SaleRate)
                            cmd.Parameters.Add(MRP)
                            cmd.Parameters.Add(ReceiveQty)
                            cmd.ExecuteNonQuery()
                        End If
                    Next
                Next

                If AgL.StrCmp(AgL.PubDBName, "NaveenSaree") Then
                    mQry = " Select H.Barcode, H.BarCodeImg, H.ItemDesc, H.ItemCategoryDesc, H.ItemGroupDesc, 
                        H.Dimension1Desc, H.Dimension2Desc, H.Dimension3Desc, H.Dimension4Desc, H.SizeDesc, H.PurchaseRate, H.SaleRate, H.MRP, H.ReceiveQty,
                        strftime('%m', PI.V_Date) ||  cast((cast(H.SaleRate as Int)) as text) || Substr(strftime('%Y', PI.V_Date),3,2) as DNo, IfNull(PI.VendorDocNo,'') || '-11' || cast((cast(H.PurchaseRate as Int)) as text) BillNo, D.DispName AS DivisionName 
                        From [#" & bTempTable & "] H 
                        Left Join Barcode B On H.BarCode = B.Code
                        Left Join PurchInvoice PI on PI.DocId = B.GenDocId
                        Left Join SubGroup D on D.SubCode = PI.Div_Code
                        Left Join Item I On B.Item = I.Code                         
                        Left Join Item IG On I.ItemGroup = IG.Code "
                ElseIf AgL.StrCmp(AgL.PubDBName, "Madhulika") Then
                    mQry = " Select Barcode, BarCodeImg, ItemDesc, ItemCategoryDesc, ItemGroupDesc,PI.VendorDocNo, 
                        Dimension1Desc, Dimension2Desc, Dimension3Desc, Dimension4Desc, SizeDesc, H.PurchaseRate, H.SaleRate, H.MRP, ReceiveQty, 
                        " & AgL.Chk_Text(mBarcodePrintTitle1) & " As Title1,
                        " & AgL.Chk_Text(mBarcodePrintTitle2) & " as Title2,
                        " & AgL.Chk_Text(mBarcodePrintTitle3) & " as Title3 
                         From [#" & bTempTable & "] H 
                         Left Join Barcode B On H.BarCode = B.Code
                         Left Join PurchInvoice PI on PI.DocId = B.GenDocId "
                Else
                    mQry = " Select Barcode, BarCodeImg, ItemDesc, ItemCategoryDesc, ItemGroupDesc, 
                        Dimension1Desc, Dimension2Desc, Dimension3Desc, Dimension4Desc, SizeDesc, PurchaseRate, SaleRate, MRP, ReceiveQty, 
                        " & AgL.Chk_Text(mBarcodePrintTitle1) & " As Title1,
                        " & AgL.Chk_Text(mBarcodePrintTitle2) & " as Title2,
                        " & AgL.Chk_Text(mBarcodePrintTitle3) & " as Title3 
                         From [#" & bTempTable & "] H "
                End If

                If mQry.Trim <> "" Then
                    DsRep = AgL.FillData(mQry, AgL.GCn)
                    AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)
                    mCrd.Load(AgL.PubReportPath & "\" & RepName)
                    mCrd.SetDataSource(DsRep.Tables(0))
                    CType(ReportView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
                    AgPL.Formula_Set(mCrd, RepTitle)
                    AgPL.Show_Report(ReportView, "* " & RepTitle & " *", Me.MdiParent)
                    'If mDocId <> "" Then
                    '    Call AgL.LogTableEntry(mDocId, Me.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
                    'End If
                End If
            Else
                If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")
            End If
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub
    Private Function GetBarcodeImage(ByVal TextValue As String, ByVal Width As Integer, ByVal Hight As Integer) As Byte()
        Dim b As BarcodeLib.Barcode
        b = New BarcodeLib.Barcode()

        Dim Img As Image
        b.Alignment = BarcodeLib.AlignmentPositions.CENTER
        b.IncludeLabel = False
        b.RotateFlipType = RotateFlipType.RotateNoneFlipNone
        b.LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER
        Img = b.Encode(BarcodeLib.TYPE.CODE39Extended, TextValue, IIf(TextValue = "0", Drawing.Color.White, Drawing.Color.Black), Drawing.Color.White, Width, Hight)
        GetBarcodeImage = b.Encoded_Image_Bytes
    End Function
    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        FGetSettings = mValue
    End Function
    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1PrintQty
                    Dgl1.ReadOnly = False
                    Dgl1.Columns(Col1PrintQty).ReadOnly = False
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
