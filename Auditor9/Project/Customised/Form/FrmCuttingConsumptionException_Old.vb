Imports System.IO
Imports AgLibrary.ClsMain.agConstants
Imports System.Xml
Imports Customised.ClsMain
Imports System.ComponentModel
Imports System.Linq
Public Class FrmCuttingConsumptionException_Old
    Inherits AgTemplate.TempMaster
    Dim mQry$ = ""
    Dim mIsReturnValue As Boolean = False
    Dim mProcess As String = "PCutting"
    Dim mTransactionReferenceType As String = "Cutting Consumption"

    Public Const ColSNo As String = "S.No."
    Public WithEvents DglMain As New AgControls.AgDataGrid
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1BtnDetail As String = "Detail"
    Public Const Col1HeadOriginal As String = "Head Original"
    Public Const Col1LastValue As String = "Last Value"

    Public Const rowItemCategory As Integer = 0
    Public Const rowDimension3 As Integer = 1
    Public Const rowBatchQty As Integer = 2
    Public Const rowBtnFill As Integer = 3

    Public Const hcItemCategory As String = "Item Category"
    Public Const hcDimension3 As String = "Design"
    Public Const hcBatchQty As String = "Batch Qty"
    Public Const hcBtnFill As String = "Fill"

    Public Const Col1Item As String = "Item"
    Public Const Col1RawMaterial As String = "Fabric Size"
    Public Const Col1Dimension3 As String = "Design"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1Remark As String = "Remark"
    Public Const Col1GeneratedItem As String = "Generated Item"
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
        Me.PnlHead = New System.Windows.Forms.Panel()
        Me.MnuOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuImportFromExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportFromDos = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportFromTally = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuBulkEdit = New System.Windows.Forms.ToolStripMenuItem()
        Me.OFDMain = New System.Windows.Forms.OpenFileDialog()
        Me.BtnAttachments = New System.Windows.Forms.Button()
        Me.Pnl1 = New System.Windows.Forms.Panel()
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
        'PnlHead
        '
        Me.PnlHead.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PnlHead.Location = New System.Drawing.Point(2, 47)
        Me.PnlHead.Name = "PnlHead"
        Me.PnlHead.Size = New System.Drawing.Size(971, 170)
        Me.PnlHead.TabIndex = 15
        '
        'MnuOptions
        '
        Me.MnuOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuImportFromExcel, Me.MnuImportFromDos, Me.MnuImportFromTally, Me.MnuBulkEdit})
        Me.MnuOptions.Name = "MnuOptions"
        Me.MnuOptions.Size = New System.Drawing.Size(171, 92)
        '
        'MnuImportFromExcel
        '
        Me.MnuImportFromExcel.Name = "MnuImportFromExcel"
        Me.MnuImportFromExcel.Size = New System.Drawing.Size(170, 22)
        Me.MnuImportFromExcel.Text = "Import From Excel"
        '
        'MnuImportFromDos
        '
        Me.MnuImportFromDos.Name = "MnuImportFromDos"
        Me.MnuImportFromDos.Size = New System.Drawing.Size(170, 22)
        Me.MnuImportFromDos.Text = "Import From Dos"
        '
        'MnuImportFromTally
        '
        Me.MnuImportFromTally.Name = "MnuImportFromTally"
        Me.MnuImportFromTally.Size = New System.Drawing.Size(170, 22)
        Me.MnuImportFromTally.Text = "Import From Tally"
        '
        'MnuBulkEdit
        '
        Me.MnuBulkEdit.Name = "MnuBulkEdit"
        Me.MnuBulkEdit.Size = New System.Drawing.Size(170, 22)
        Me.MnuBulkEdit.Text = "Bulk Edit"
        '
        'OFDMain
        '
        Me.OFDMain.FileName = "price.xls"
        Me.OFDMain.Filter = "*.xls|*.Xls"
        Me.OFDMain.InitialDirectory = "D:\"
        Me.OFDMain.ShowHelp = True
        Me.OFDMain.Title = "Select Excel File"
        '
        'BtnAttachments
        '
        Me.BtnAttachments.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAttachments.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAttachments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnAttachments.Location = New System.Drawing.Point(613, 580)
        Me.BtnAttachments.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnAttachments.Name = "BtnAttachments"
        Me.BtnAttachments.Size = New System.Drawing.Size(69, 23)
        Me.BtnAttachments.TabIndex = 1019
        Me.BtnAttachments.TabStop = False
        Me.BtnAttachments.Text = "Add Attachments"
        Me.BtnAttachments.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnAttachments.UseVisualStyleBackColor = True
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl1.Location = New System.Drawing.Point(2, 223)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(971, 335)
        Me.Pnl1.TabIndex = 1020
        '
        'FrmCuttingConsumptionException
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(974, 612)
        Me.ContextMenuStrip = Me.MnuOptions
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.BtnAttachments)
        Me.Controls.Add(Me.PnlHead)
        Me.MaximizeBox = True
        Me.Name = "FrmCuttingConsumptionException"
        Me.Text = "Cutting Consumption"
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.PnlHead, 0)
        Me.Controls.SetChildIndex(Me.BtnAttachments, 0)
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
        Me.MnuOptions.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MnuOptions As ContextMenuStrip
    Private components As System.ComponentModel.IContainer
    Friend WithEvents MnuImportFromExcel As ToolStripMenuItem
    Friend WithEvents MnuImportFromTally As ToolStripMenuItem
    Public WithEvents OFDMain As OpenFileDialog
    Friend WithEvents MnuBulkEdit As ToolStripMenuItem
    Friend WithEvents MnuImportFromDos As ToolStripMenuItem
    Protected WithEvents BtnAttachments As Button
    Friend WithEvents PnlHead As Panel
    Public WithEvents Pnl1 As Panel
#End Region
    Private Sub FrmShade_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        'AgL.PubFindQry = " SELECT Code AS SearchCode
        '                FROM Item I Where I.V_Type = '" & ItemV_Type.BOM & "' "
        AgL.PubFindQry = " Select T.Code As SearchCode, Max(Ic.Description) As ItemCategory "
        AgL.PubFindQry += " From BomHead T "
        AgL.PubFindQry += " LEFT JOIN ItemCategory Ic On T.ItemCategory = Ic.Code "
        AgL.PubFindQry += " LEFT JOIN Item Rm On T.RawMaterial = Rm.Code "
        AgL.PubFindQry += " Group by Code "
        AgL.PubFindQry += " Order by Code"
        AgL.PubFindQryOrdBy = "[SearchCode]"
    End Sub
    Private Sub FrmShade_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "BomHead"
    End Sub
    Private Sub FrmShade_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        mQry = " Select Code As SearchCode From BomHead "
        mQry += " Where Type = 'Exception' "
        mQry += " Group by Code "
        mQry += " Order by Code "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub
    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DtTemp As DataTable
        Dim I As Integer

        mQry = " SELECT H.*, Ic.Description As ItemCategoryDesc, D3.Description As Dimension3Desc
                FROM BomHead H 
                LEFT JOIN ItemCategory Ic ON H.ItemCategory = Ic.Code
                LEFT JOIN Dimension3 D3 ON H.Dimension3 = D3.Code
                WHERE H.Code = '" & SearchCode & "'"
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        With DtTemp
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                DglMain.Item(Col1Value, rowItemCategory).Tag = AgL.XNull(.Rows(0)("ItemCategory"))
                DglMain.Item(Col1Value, rowItemCategory).Value = AgL.XNull(.Rows(0)("ItemCategoryDesc"))
                DglMain.Item(Col1Value, rowDimension3).Tag = AgL.XNull(.Rows(0)("Dimension3"))
                DglMain.Item(Col1Value, rowDimension3).Value = AgL.XNull(.Rows(0)("Dimension3Desc"))
            End If
        End With

        mQry = "SELECT L.*, L.Description AS MainItemDesc, BomItem.Description AS BomItemDesc, 
                S.Code As MainItemSize, S.Description As MainItemSizeDesc, 
                Raw.Code As RawMaterialCode, Raw.Description As RawMaterialDesc, 
                D3.Code As Dimension3Code, D3.Description As Dimension3Desc, 
                BomItem.Unit, L.DealQty As BatchQty
                FROM BomHead H 
                LEFT JOIN (SELECT It.ItemCategory, It.Size, It.RawMaterial, It.Dimension3, It.DealQty, It.Description, Bd.*
			                FROM BomDetail Bd
			                LEFT JOIN Item It ON Bd.Code = It.Code) AS L ON H.ItemCategory = L.ItemCategory AND H.Dimension3 = L.Dimension3
                LEFT JOIN Size S ON L.Size = S.Code
                LEFT JOIN Item Raw On L.RawMaterial = Raw.Code
                LEFT JOIN Item D3 On L.Dimension3 = D3.Code
                LEFT JOIN Item BomItem ON L.Item = BomItem.Code
                Where H.Code = '" & mSearchCode & "'  "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        With DtTemp
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                    Dgl1.Item(ColSNo, I).Tag = AgL.VNull(.Rows(I)("Sr"))
                    Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                    Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("BomItemDesc"))
                    Dgl1.Item(Col1RawMaterial, I).Tag = AgL.XNull(.Rows(I)("RawMaterialCode"))
                    Dgl1.Item(Col1RawMaterial, I).Value = AgL.XNull(.Rows(I)("RawMaterialDesc"))
                    Dgl1.Item(Col1Dimension3, I).Tag = AgL.XNull(.Rows(I)("Dimension3Code"))
                    Dgl1.Item(Col1Dimension3, I).Value = AgL.XNull(.Rows(I)("Dimension3Desc"))
                    Dgl1.Item(Col1Qty, I).Value = AgL.XNull(.Rows(I)("Qty"))
                    Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                    Dgl1.Item(Col1GeneratedItem, I).Tag = AgL.XNull(.Rows(I)("Code"))
                    Dgl1.Item(Col1GeneratedItem, I).Value = AgL.XNull(.Rows(I)("MainItemDesc"))
                Next I
                DglMain.Item(Col1Value, rowBatchQty).Value = AgL.XNull(.Rows(0)("BatchQty"))
            End If
        End With
        ApplyUISetting()
        SetAttachmentCaption()
        Topctrl1.tPrn = False
    End Sub
    Private Sub FrmSteward_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
            MnuImportFromExcel.Visible = False
            MnuImportFromTally.Visible = False
            MnuImportFromDos.Visible = False
        End If
    End Sub
    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub
    Private Sub FrmParty_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        SetAttachmentCaption()
        ApplyUISetting()

        If DglMain.Visible = True Then
            If DglMain.FirstDisplayedCell IsNot Nothing Then
                DglMain.CurrentCell = DglMain(Col1Value, DglMain.FirstDisplayedCell.RowIndex)
                DglMain.Focus()
            End If
        End If
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Private Sub FrmParty_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        DglMain.CurrentCell = DglMain(Col1Value, rowItemCategory)
        DglMain.Focus()
    End Sub
    Private Sub FrmCuttingConsumptionException_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
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
        AgL.AddAgDataGrid(DglMain, PnlHead)
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


        DglMain.Rows.Add(4)
        DglMain.Item(Col1Head, rowItemCategory).Value = hcItemCategory
        DglMain.Item(Col1Head, rowDimension3).Value = hcDimension3
        DglMain.Item(Col1Head, rowBatchQty).Value = hcBatchQty
        DglMain.Item(Col1Head, rowBtnFill).Value = hcBtnFill
        DglMain.Item(Col1Value, rowBtnFill) = New DataGridViewButtonCell


        For I = 0 To DglMain.Rows.Count - 1
            DglMain(Col1HeadOriginal, I).Value = DglMain(Col1Head, I).Value
        Next

        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Item, 150, 0, Col1Item, True, False)
            .AddAgTextColumn(Dgl1, Col1RawMaterial, 100, 0, Col1RawMaterial, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension3, 100, 0, Col1Dimension3, True, False)
            .AddAgTextColumn(Dgl1, Col1Qty, 70, 0, Col1Qty, True, False)
            .AddAgTextColumn(Dgl1, Col1Unit, 60, 0, Col1Unit, True, True)
            .AddAgTextColumn(Dgl1, Col1Remark, 200, 0, Col1Remark, True, False)
            .AddAgTextColumn(Dgl1, Col1GeneratedItem, 100, 0, Col1GeneratedItem, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.RowHeadersVisible = False
        DglMain.AllowUserToAddRows = False
        Dgl1.Visible = False
        Dgl1.BackgroundColor = Me.BackColor
        AgL.GridDesign(Dgl1)
        Dgl1.Name = "Dgl1"
        Dgl1.Anchor = AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom

        AgL.FSetDimensionCaptionForHorizontalGrid(Dgl1, AgL)
        ApplyUISetting()
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
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub DglMain_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DglMain.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim bNewMasterCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            If DglMain.CurrentCell Is Nothing Then Exit Sub

            bRowIndex = DglMain.CurrentCell.RowIndex
            bColumnIndex = DglMain.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> DglMain.Columns(Col1Value).Index Then Exit Sub

            Select Case DglMain.CurrentCell.RowIndex
                Case rowItemCategory
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Code, Description FROM ItemCategory 
                                    WHERE ItemType IN ('" & ItemTypeCode.TradingProduct & "','" & ItemTypeCode.ManufacturingProduct & "') 
                                    And IfNull(Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'"
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowDimension3
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = " Select H.Code, H.Description From Dimension3 H Order By H.Description "
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub FrmCuttingConsumptionException_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dim I As Integer
        For I = 0 To DglMain.Rows.Count - 1
            DglMain.Item(Col1Value, I).Value = ""
            DglMain.Item(Col1Value, I).Tag = ""
            DglMain.Item(Col1BtnDetail, I).Tag = Nothing
            DglMain.Item(Col1BtnDetail, I) = New DataGridViewTextBoxCell
            DglMain(Col1BtnDetail, I).ReadOnly = True
        Next
        Dgl1.Rows.Clear()


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

    Private Sub Dgl1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles Dgl1.EditingControlShowing, DglMain.EditingControlShowing
        If FGetSettings(SettingFields.DefaultTextCaseInMasters, SettingType.General) = TextCase.Upper Then

            DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Upper
        ElseIf FGetSettings(SettingFields.DefaultTextCaseInMasters, SettingType.General) = TextCase.Lower Then
            DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Lower
        End If
    End Sub
    Private Sub FrmCuttingConsumptionException_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
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


        If AgL.VNull(AgL.Dman_Execute("SELECT Count(*)
                FROM BomHead H 
                WHERE H.ItemCategory = '" & DglMain.Item(Col1Value, rowItemCategory).Tag & "' 
                AND H.Dimension3 = '" & DglMain.Item(Col1Value, rowDimension3).Tag & "'
                AND H.Code <> '" & mSearchCode & "'", AgL.GCn).ExecuteScalar()) > 0 Then
            MsgBox("Consumption already exist for " & DglMain.Item(Col1Value, rowItemCategory).Value + " and " & DglMain.Item(Col1Value, rowDimension3).Value, MsgBoxStyle.Information)
            DglMain.CurrentCell = DglMain(Col1Value, rowDimension3)
            DglMain.Focus()
            passed = False
            Exit Sub
        End If

        For I = 0 To Dgl1.RowCount - 1
            If AgL.XNull(Dgl1.Item(Col1GeneratedItem, I).Tag) = "" And AgL.XNull(Dgl1.Item(Col1GeneratedItem, I).Value) <> "" Then
                If AgL.Dman_Execute(" Select Count(*) From Item Where Description = '" & Dgl1.Item(Col1GeneratedItem, I).Value & "' ", AgL.GCn).ExecuteScalar() Then
                    MsgBox(Dgl1(Col1GeneratedItem, I).Value & " is duplicate.", MsgBoxStyle.Information)
                    Dgl1.CurrentCell = Dgl1(Col1Item, I)
                    Dgl1.Focus()
                    passed = False
                    Exit Sub
                End If
            End If

            mQry = " Select "
        Next
    End Sub
    Private Sub DglMain_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles DglMain.EditingControl_Validating
        Dim I As Integer
        Dim mSettingChangedRowIndex As Integer = -1
        Try
            If DglMain.CurrentCell IsNot Nothing Then
                If DglMain.CurrentCell.ColumnIndex = DglMain.Columns(Col1Value).Index Then
                    Select Case DglMain.CurrentCell.RowIndex
                        Case rowItemCategory
                            ApplyUISetting()
                            For I = 0 To Dgl1.Rows.Count - 1
                                Dgl1.Item(Col1GeneratedItem, I).Value = FCreateBomItemDesc(I)
                            Next
                            mSettingChangedRowIndex = rowItemCategory
                    End Select

                    If DglMain.CurrentCell IsNot Nothing Then
                        If DglMain.Item(Col1Mandatory, DglMain.CurrentCell.RowIndex).Value <> "" Then
                            If DglMain(Col1Value, DglMain.CurrentCell.RowIndex).Value = "" Then
                                MsgBox(DglMain(Col1Head, DglMain.CurrentCell.RowIndex).Value & " can not be blank.")
                                e.Cancel = True
                                Exit Sub
                            End If
                        End If
                    End If
                End If

                If mSettingChangedRowIndex >= 0 Then
                    For I = mSettingChangedRowIndex + 1 To DglMain.Rows.Count - 1
                        If DglMain.Rows(I).Visible = True Then
                            DglMain.CurrentCell = DglMain(Col1Value, I)
                            DglMain.Focus()
                            Exit For
                        End If
                    Next
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmCuttingConsumptionException_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Dim I As Integer
        For I = 0 To DglMain.Rows.Count - 1
            DglMain(Col1Head, I).Tag = Nothing
        Next
        Dgl1.AgHelpDataSet(Col1Item) = Nothing
        Dgl1.AgHelpDataSet(Col1RawMaterial) = Nothing
    End Sub

    Private Sub BtnAttachments_Click(sender As Object, e As EventArgs) Handles BtnAttachments.Click
        Dim FrmObj As New FrmAttachmentViewer()
        FrmObj.LblDocNo.Text = ""
        FrmObj.SearchCode = mSearchCode
        FrmObj.TableName = "SchemeAttachments"
        FrmObj.StartPosition = FormStartPosition.CenterParent
        FrmObj.ShowDialog()
        FrmObj.Dispose()
        FrmObj = Nothing
        SetAttachmentCaption()
    End Sub
    Private Sub SetAttachmentCaption()
        Dim AttachmentPath As String = PubAttachmentPath + mSearchCode + "\"
        If Directory.Exists(AttachmentPath) Then
            Dim FileCount As Integer = Directory.GetFiles(AttachmentPath).Count
            If FileCount > 0 Then BtnAttachments.Text = FileCount.ToString + IIf(FileCount = 1, " Attachment", " Attachments") Else BtnAttachments.Text = "Attachments"
        Else
            BtnAttachments.Text = "Attachments"
        End If
    End Sub
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1RawMaterial
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1RawMaterial) Is Nothing Then
                            mQry = " Select H.Code, H.Description From Dimension4 H Order By H.Description "
                            Dgl1.AgHelpDataSet(Col1RawMaterial) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1Item
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                            mQry = " Select H.Code, H.Description From ItemCategory H 
                                    Where H.ItemType = 'RP' 
                                    And IfNull(Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'
                                    Order By H.Description "
                            Dgl1.AgHelpDataSet(Col1Item) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, AgL.PubSiteCode, "", "", "", "", "")
        FGetSettings = mValue
    End Function
    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub
    Private Sub ApplyUISetting()
        GetUISetting(DglMain, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, "", "", DglMain.Item(Col1Value, rowItemCategory).Tag, "", ClsMain.GridTypeConstants.VerticalGrid)
        GetUISetting(Dgl1, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, "", "", DglMain.Item(Col1Value, rowItemCategory).Tag, "", ClsMain.GridTypeConstants.HorizontalGrid)
    End Sub
    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim bGeneratedMainItemCode As String = ""
        Dim I As Integer = 0
        Dim mSr As Integer = 0

        mQry = "UPDATE BomHead " &
                " SET " &
                " Type = 'Exception', " &
                " ItemCategory = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowItemCategory).Tag) & ", " &
                " Dimension3 = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowDimension3).Tag) & " " &
                " Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                If AgL.XNull(Dgl1.Item(Col1GeneratedItem, I).Tag) = "" Then
                    bGeneratedMainItemCode = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

                    mQry = " INSERT INTO Item (Code, ManualCode, Description, Unit, EntryBy, EntryDate, Status, 
                             Div_Code, Specification, ItemCategory, DealQty, RawMaterial, StockYN, V_Type)
                             Select " & AgL.Chk_Text(bGeneratedMainItemCode) & " As Code, Null As ManualCode, 
                             " & AgL.Chk_Text(Dgl1.Item(Col1GeneratedItem, I).Value) & " As Description, 'Nos' As Unit, 
                             " & AgL.Chk_Text(AgL.PubUserName) & " As EntryBy, " & AgL.Chk_Date(AgL.PubLoginDate) & " As EntryDate, 
                             " & AgL.Chk_Text(AgTemplate.ClsMain.EntryStatus.Active) & " As Status,  
                             " & AgL.Chk_Text(AgL.PubDivCode) & " As Div_Code, 
                             " & AgL.Chk_Text(Dgl1.Item(Col1GeneratedItem, I).Value) & " As Specification, 
                             " & AgL.Chk_Text(DglMain.Item(Col1Value, rowItemCategory).Tag) & " As ItemCategory, 
                             " & Val(DglMain.Item(Col1Value, rowBatchQty).Value) & " As BatchQty, 
                             " & AgL.Chk_Text(Dgl1.Item(Col1RawMaterial, I).Tag) & " As RawMaterial, 
                             0 As StockYN, " & AgL.Chk_Text(ItemV_Type.BOM) & " As V_Type "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mSr = AgL.VNull(AgL.Dman_Execute("Select IfNull(Max(Sr),0) + 1 
                                    From BomDetail With (NoLock) 
                                    Where Code = '" & bGeneratedMainItemCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())

                    mQry = " INSERT INTO BomDetail (Code, Sr, Process, Item, Qty)
                             Select " & AgL.Chk_Text(bGeneratedMainItemCode) & " As Code, 
                             " & mSr & " As Sr, 
                             " & AgL.Chk_Text(mProcess) & " As Process, 
                             " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & " As Item, 
                             " & Val(Dgl1.Item(Col1Qty, I).Value) & " As Qty "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                Else
                    mQry = "UPDATE Item 
                            SET Description = " & AgL.Chk_Text(Dgl1.Item(Col1GeneratedItem, I).Value) & ", 
                            RawMaterial = " & AgL.Chk_Text(Dgl1.Item(Col1RawMaterial, I).Tag) & ", 
                            DealQty = " & Val(DglMain.Item(Col1Value, rowBatchQty).Value) & ",
                            EntryBy = " & AgL.Chk_Text(AgL.PubUserName) & ", 
                            EntryDate = " & AgL.Chk_Date(AgL.PubLoginDate) & ", 
                            Specification = " & AgL.Chk_Text(Dgl1.Item(Col1GeneratedItem, I).Value) & " 
                            Where Code = " & AgL.Chk_Text(Dgl1.Item(Col1GeneratedItem, I).Tag) & "  "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mQry = " UPDATE BomDetail
                             SET Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ",
	                         Qty = " & Val(Dgl1.Item(Col1Qty, I).Value) & "
                             Where Code = " & AgL.Chk_Text(Dgl1.Item(Col1GeneratedItem, I).Tag) & " 
                             And Sr = " & AgL.Chk_Text(Dgl1.Item(ColSNo, I).Tag) & " "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            End If
        Next
    End Sub
    Function FCreateBomItemDesc(bRowIndex As Integer) As String
        Dim bItemDesc As String = ""
        bItemDesc = DglMain.Item(Col1Value, rowItemCategory).Value
        bItemDesc += "-"
        bItemDesc += DglMain.Item(Col1Value, rowDimension3).Value
        bItemDesc += "-"
        bItemDesc += Dgl1.Item(Col1RawMaterial, bRowIndex).Value
        FCreateBomItemDesc = bItemDesc
    End Function
    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim DtItem As DataTable
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1RawMaterial
                    Dgl1.Item(Col1GeneratedItem, mRowIndex).Value = FCreateBomItemDesc(mRowIndex)

                Case Col1Item
                    mQry = "Select I.Unit From Item I  With (NoLock) 
                            Where I.Code ='" & Dgl1.Item(Col1Item, mRowIndex).Tag & "'"
                    DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtItem.Rows.Count > 0 Then
                        Dgl1.Item(Col1Unit, mRowIndex).Value = AgL.XNull(DtItem.Rows(0)("Unit"))
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmCuttingConsumptionException_BaseEvent_ApproveDeletion_InTrans(SearchCode As String, Conn As Object, Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = " Delete From BomDetail Where Code = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " Delete From Item Where Code = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    Try
                        If Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Value = "" Then
                            If Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex - 1).Value IsNot Nothing Then
                                Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Tag = Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex - 1).Tag
                                Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Value = Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex - 1).Value
                                Dgl1.Item(Col1Unit, Dgl1.CurrentCell.RowIndex).Value = Dgl1.Item(Col1Unit, Dgl1.CurrentCell.RowIndex - 1).Value
                            End If
                        End If
                    Catch ex As Exception
                    End Try

                Case Col1RawMaterial
                    Try
                        If Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Value = "" Then
                            If Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex - 1).Value IsNot Nothing Then
                                Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Tag = Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex - 1).Tag
                                Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Value = Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex - 1).Value
                                Dgl1.Item(Col1Unit, Dgl1.CurrentCell.RowIndex).Value = Dgl1.Item(Col1Unit, Dgl1.CurrentCell.RowIndex - 1).Value
                            End If
                        End If
                    Catch ex As Exception
                    End Try
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
