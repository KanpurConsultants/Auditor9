Imports Microsoft.Reporting.WinForms
Imports System.Xml
Imports Customised.ClsMain
Imports System.IO
Imports AgLibrary.ClsMain.agConstants

Public Class FrmJournalEntry
    Inherits AgTemplate.TempTransaction
    Dim mQry$


    Dim Dgl As New AgControls.AgDataGrid

    '========================================================================
    '======================== DATA GRID AND COLUMNS DEFINITION ================
    '========================================================================
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Public Const Col1EffectiveDate As String = "Effective Date"
    Public Const Col1Subcode As String = "Subcode"
    Public Const Col1LinkedSubcode As String = "Linked Account"
    Public Const Col1AmountDr As String = "Amount Dr"
    Public Const Col1AmountCr As String = "Amount Cr"
    Public Const Col1Remark As String = "Remark"
    Public Const Col1CurrentBalance As String = "Current Balance"
    Public Const Col1Nature As String = "Nature"
    Public Const Col1SubgroupType As String = "A/c Type"
    Public Const Col1ReferenceNo As String = "Reference No"
    Public Const Col1ReferenceDate As String = "Reference Date"
    Public Const Col1AmsReferenceNo As String = "Ams Reference No"
    Public Const Col1AmsReferenceDate As String = "Ams Reference Date"
    Public Const Col1AmsReferenceAmount As String = "Ams Reference Amount"



    Dim mPrevRowIndex As Integer = 0
    Protected WithEvents LblTotalAmountCr As Label
    Protected WithEvents LblTotalAmountCrText As Label
    Dim mFlag_Import As Boolean = False

    Dim SettingFields_CopyRemarkInNextLineYn As Boolean = False    

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal strNCat As String, Optional ByVal strCustomUI As String = "")
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
        mCustomUI = strCustomUI
        EntryNCat = strNCat

    End Sub


    Private Sub ApplyUISetting(ItemType As String)
        Dim mQry As String
        Dim DtTemp As DataTable
        Dim I As Integer, J As Integer
        Dim mDgl1ColumnCount As Integer
        Try


            For I = 1 To Dgl1.Columns.Count - 1
                Dgl1.Columns(I).Visible = False
            Next

            mQry = "Select H.*
                    from EntryLineUISetting H                    
                    Where EntryName='" & Me.Name & "' And NCat In ('" & ItemType & "') And GridName ='" & Dgl1.Name & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 0 To Dgl1.Columns.Count - 1
                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = Dgl1.Columns(J).Name Then
                            Dgl1.Columns(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
                            Dgl1.Columns(J).ReadOnly = Not CType(AgL.VNull(DtTemp.Rows(I)("IsEditable")), Boolean)
                            If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDgl1ColumnCount += 1
                            If Not IsDBNull(DtTemp.Rows(I)("DisplayIndex")) Then
                                Dgl1.Columns(J).DisplayIndex = AgL.VNull(DtTemp.Rows(I)("DisplayIndex"))
                            End If
                            If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
                                Dgl1.Columns(J).HeaderText = AgL.XNull(DtTemp.Rows(I)("Caption"))
                            End If
                            'Dgl1.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
                        End If
                    Next
                Next
            End If
            If mDgl1ColumnCount = 0 Then Dgl1.Visible = False Else Dgl1.Visible = True

            If AgL.StrCmp(AgL.PubDBName, "RVN") Or AgL.StrCmp(AgL.PubDBName, "RVN1") Or AgL.StrCmp(AgL.PubDBName, "RVN2") Or AgL.StrCmp(AgL.PubDBName, "MLAW") Then
                Dgl1.Columns(Col1LinkedSubcode).Visible = True
            End If

        Catch ex As Exception
            MsgBox(ex.Message & " [ApplySubgroupTypeSetting]")
        End Try
    End Sub



#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmJournalEntry))
        Me.Dgl1 = New AgControls.AgDataGrid()
        Me.PnlTotals = New System.Windows.Forms.Panel()
        Me.LblTotalAmountCr = New System.Windows.Forms.Label()
        Me.LblTotalAmountCrText = New System.Windows.Forms.Label()
        Me.LblTotalAmountDr = New System.Windows.Forms.Label()
        Me.LblTotalAmountDrText = New System.Windows.Forms.Label()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.PnlCustomGrid = New System.Windows.Forms.Panel()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.TxtRemarks = New AgControls.AgTextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.LblCurrency = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.TxtCustomFields = New AgControls.AgTextBox()
        Me.GBoxImportFromExcel = New System.Windows.Forms.GroupBox()
        Me.BtnImprtFromExcel = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.LblCurrentBalance = New System.Windows.Forms.Label()
        Me.TxtVoucherCategory = New AgControls.AgTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.MnuOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuImportFromExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportFromDos = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportFromTally = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuImportGSTDataFromExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuEditSave = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuCancelEntry = New System.Windows.Forms.ToolStripMenuItem()
        Me.OFDMain = New System.Windows.Forms.OpenFileDialog()
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
        Me.PnlTotals.SuspendLayout()
        Me.Pnl1.SuspendLayout()
        Me.GBoxImportFromExcel.SuspendLayout()
        Me.MnuOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(829, 581)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(648, 581)
        Me.GBoxMoveToLog.Size = New System.Drawing.Size(148, 40)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Location = New System.Drawing.Point(3, 19)
        Me.TxtMoveToLog.Size = New System.Drawing.Size(142, 18)
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(467, 581)
        Me.GBoxApprove.Size = New System.Drawing.Size(148, 40)
        Me.GBoxApprove.Text = "Approved By"
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(168, 581)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 581)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 577)
        Me.GroupBox1.Size = New System.Drawing.Size(1002, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(320, 581)
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
        Me.LblV_No.Location = New System.Drawing.Point(276, 267)
        Me.LblV_No.Size = New System.Drawing.Size(71, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Invoice No."
        Me.LblV_No.Visible = False
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(384, 266)
        Me.TxtV_No.Size = New System.Drawing.Size(163, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtV_No.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(363, 31)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblV_Date.Location = New System.Drawing.Point(265, 26)
        Me.LblV_Date.Size = New System.Drawing.Size(77, 14)
        Me.LblV_Date.Tag = ""
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(576, 12)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtV_Date.Location = New System.Drawing.Point(380, 25)
        Me.TxtV_Date.Size = New System.Drawing.Size(100, 16)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblV_Type.Location = New System.Drawing.Point(484, 8)
        Me.LblV_Type.Size = New System.Drawing.Size(78, 14)
        Me.LblV_Type.Tag = ""
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtV_Type.Location = New System.Drawing.Point(594, 6)
        Me.TxtV_Type.Size = New System.Drawing.Size(200, 16)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(363, 12)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSite_Code.Location = New System.Drawing.Point(265, 7)
        Me.LblSite_Code.Size = New System.Drawing.Size(95, 14)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSite_Code.Location = New System.Drawing.Point(380, 6)
        Me.TxtSite_Code.Size = New System.Drawing.Size(100, 16)
        Me.TxtSite_Code.TabIndex = 0
        Me.TxtSite_Code.Tag = ""
        '
        'LblDocId
        '
        Me.LblDocId.Tag = ""
        '
        'LblPrefix
        '
        Me.LblPrefix.Location = New System.Drawing.Point(336, 267)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-4, 17)
        Me.TabControl1.Size = New System.Drawing.Size(992, 116)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.TxtVoucherCategory)
        Me.TP1.Controls.Add(Me.Panel3)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.Panel2)
        Me.TP1.Controls.Add(Me.LblCurrency)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(984, 90)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblCurrency, 0)
        Me.TP1.Controls.SetChildIndex(Me.Panel2, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.Panel3, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVoucherCategory, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(984, 41)
        Me.Topctrl1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(576, 31)
        Me.Label1.TabIndex = 737
        '
        'TxtReferenceNo
        '
        Me.TxtReferenceNo.AgNumberRightPlaces = 2
        Me.TxtReferenceNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtReferenceNo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReferenceNo.Location = New System.Drawing.Point(594, 25)
        Me.TxtReferenceNo.Size = New System.Drawing.Size(200, 16)
        Me.TxtReferenceNo.TabIndex = 3
        '
        'LblReferenceNo
        '
        Me.LblReferenceNo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblReferenceNo.Location = New System.Drawing.Point(484, 25)
        Me.LblReferenceNo.Size = New System.Drawing.Size(68, 14)
        Me.LblReferenceNo.TabIndex = 731
        Me.LblReferenceNo.Text = "Entry No."
        '
        'Dgl1
        '
        Me.Dgl1.AgAllowFind = True
        Me.Dgl1.AgLastColumn = -1
        Me.Dgl1.AgMandatoryColumn = 0
        Me.Dgl1.AgReadOnlyColumnColor = System.Drawing.Color.Ivory
        Me.Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.Dgl1.AgSkipReadOnlyColumns = False
        Me.Dgl1.CancelEditingControlValidating = False
        Me.Dgl1.GridSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.Dgl1.Location = New System.Drawing.Point(0, 0)
        Me.Dgl1.Name = "Dgl1"
        Me.Dgl1.Size = New System.Drawing.Size(240, 150)
        Me.Dgl1.TabIndex = 0
        '
        'PnlTotals
        '
        Me.PnlTotals.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlTotals.BackColor = System.Drawing.Color.Cornsilk
        Me.PnlTotals.Controls.Add(Me.LblTotalAmountCr)
        Me.PnlTotals.Controls.Add(Me.LblTotalAmountCrText)
        Me.PnlTotals.Controls.Add(Me.LblTotalAmountDr)
        Me.PnlTotals.Controls.Add(Me.LblTotalAmountDrText)
        Me.PnlTotals.Location = New System.Drawing.Point(4, 548)
        Me.PnlTotals.Name = "PnlTotals"
        Me.PnlTotals.Size = New System.Drawing.Size(974, 23)
        Me.PnlTotals.TabIndex = 694
        '
        'LblTotalAmountCr
        '
        Me.LblTotalAmountCr.AutoSize = True
        Me.LblTotalAmountCr.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmountCr.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalAmountCr.Location = New System.Drawing.Point(852, 4)
        Me.LblTotalAmountCr.Name = "LblTotalAmountCr"
        Me.LblTotalAmountCr.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalAmountCr.TabIndex = 664
        Me.LblTotalAmountCr.Text = "."
        Me.LblTotalAmountCr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalAmountCrText
        '
        Me.LblTotalAmountCrText.AutoSize = True
        Me.LblTotalAmountCrText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmountCrText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalAmountCrText.Location = New System.Drawing.Point(699, 3)
        Me.LblTotalAmountCrText.Name = "LblTotalAmountCrText"
        Me.LblTotalAmountCrText.Size = New System.Drawing.Size(118, 16)
        Me.LblTotalAmountCrText.TabIndex = 663
        Me.LblTotalAmountCrText.Text = "Total Amount Cr :"
        '
        'LblTotalAmountDr
        '
        Me.LblTotalAmountDr.AutoSize = True
        Me.LblTotalAmountDr.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmountDr.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalAmountDr.Location = New System.Drawing.Point(608, 4)
        Me.LblTotalAmountDr.Name = "LblTotalAmountDr"
        Me.LblTotalAmountDr.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalAmountDr.TabIndex = 662
        Me.LblTotalAmountDr.Text = "."
        Me.LblTotalAmountDr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalAmountDrText
        '
        Me.LblTotalAmountDrText.AutoSize = True
        Me.LblTotalAmountDrText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmountDrText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalAmountDrText.Location = New System.Drawing.Point(486, 3)
        Me.LblTotalAmountDrText.Name = "LblTotalAmountDrText"
        Me.LblTotalAmountDrText.Size = New System.Drawing.Size(118, 16)
        Me.LblTotalAmountDrText.TabIndex = 661
        Me.LblTotalAmountDrText.Text = "Total Amount Dr :"
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl1.Controls.Add(Me.PnlCustomGrid)
        Me.Pnl1.Location = New System.Drawing.Point(4, 158)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(973, 390)
        Me.Pnl1.TabIndex = 7
        '
        'PnlCustomGrid
        '
        Me.PnlCustomGrid.Location = New System.Drawing.Point(478, 141)
        Me.PnlCustomGrid.Name = "PnlCustomGrid"
        Me.PnlCustomGrid.Size = New System.Drawing.Size(17, 108)
        Me.PnlCustomGrid.TabIndex = 4
        Me.PnlCustomGrid.Visible = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(569, 222)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(61, 16)
        Me.Label25.TabIndex = 715
        Me.Label25.Text = "Structure"
        Me.Label25.Visible = False
        '
        'TxtRemarks
        '
        Me.TxtRemarks.AgAllowUserToEnableMasterHelp = False
        Me.TxtRemarks.AgLastValueTag = Nothing
        Me.TxtRemarks.AgLastValueText = Nothing
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
        Me.TxtRemarks.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemarks.Location = New System.Drawing.Point(380, 44)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(414, 16)
        Me.TxtRemarks.TabIndex = 5
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(265, 46)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(65, 14)
        Me.Label30.TabIndex = 723
        Me.Label30.Text = "Remarks"
        '
        'LblCurrency
        '
        Me.LblCurrency.AutoSize = True
        Me.LblCurrency.BackColor = System.Drawing.Color.Transparent
        Me.LblCurrency.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCurrency.Location = New System.Drawing.Point(332, 219)
        Me.LblCurrency.Name = "LblCurrency"
        Me.LblCurrency.Size = New System.Drawing.Size(60, 16)
        Me.LblCurrency.TabIndex = 735
        Me.LblCurrency.Text = "Currency"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(4, 137)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(230, 20)
        Me.LinkLabel1.TabIndex = 739
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Detail For Following Items"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtCustomFields
        '
        Me.TxtCustomFields.AgAllowUserToEnableMasterHelp = False
        Me.TxtCustomFields.AgLastValueTag = Nothing
        Me.TxtCustomFields.AgLastValueText = Nothing
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
        Me.TxtCustomFields.Location = New System.Drawing.Point(486, 594)
        Me.TxtCustomFields.MaxLength = 20
        Me.TxtCustomFields.Name = "TxtCustomFields"
        Me.TxtCustomFields.Size = New System.Drawing.Size(72, 18)
        Me.TxtCustomFields.TabIndex = 1011
        Me.TxtCustomFields.Text = "AgTextBox1"
        Me.TxtCustomFields.Visible = False
        '
        'GBoxImportFromExcel
        '
        Me.GBoxImportFromExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GBoxImportFromExcel.BackColor = System.Drawing.Color.Transparent
        Me.GBoxImportFromExcel.Controls.Add(Me.BtnImprtFromExcel)
        Me.GBoxImportFromExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GBoxImportFromExcel.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBoxImportFromExcel.ForeColor = System.Drawing.Color.Maroon
        Me.GBoxImportFromExcel.Location = New System.Drawing.Point(678, 576)
        Me.GBoxImportFromExcel.Name = "GBoxImportFromExcel"
        Me.GBoxImportFromExcel.Size = New System.Drawing.Size(99, 47)
        Me.GBoxImportFromExcel.TabIndex = 1013
        Me.GBoxImportFromExcel.TabStop = False
        Me.GBoxImportFromExcel.Tag = "UP"
        Me.GBoxImportFromExcel.Text = "Import From Excel"
        Me.GBoxImportFromExcel.Visible = False
        '
        'BtnImprtFromExcel
        '
        Me.BtnImprtFromExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnImprtFromExcel.Image = CType(resources.GetObject("BtnImprtFromExcel.Image"), System.Drawing.Image)
        Me.BtnImprtFromExcel.Location = New System.Drawing.Point(58, 9)
        Me.BtnImprtFromExcel.Name = "BtnImprtFromExcel"
        Me.BtnImprtFromExcel.Size = New System.Drawing.Size(36, 34)
        Me.BtnImprtFromExcel.TabIndex = 669
        Me.BtnImprtFromExcel.TabStop = False
        Me.BtnImprtFromExcel.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Location = New System.Drawing.Point(4, 119)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(973, 227)
        Me.Panel2.TabIndex = 1
        '
        'Panel3
        '
        Me.Panel3.Location = New System.Drawing.Point(4, 119)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(973, 227)
        Me.Panel3.TabIndex = 6
        '
        'LblCurrentBalance
        '
        Me.LblCurrentBalance.AutoSize = True
        Me.LblCurrentBalance.BackColor = System.Drawing.Color.Transparent
        Me.LblCurrentBalance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCurrentBalance.Location = New System.Drawing.Point(379, 140)
        Me.LblCurrentBalance.Name = "LblCurrentBalance"
        Me.LblCurrentBalance.Size = New System.Drawing.Size(38, 14)
        Me.LblCurrentBalance.TabIndex = 3004
        Me.LblCurrentBalance.Text = "0.00"
        Me.LblCurrentBalance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TxtVoucherCategory
        '
        Me.TxtVoucherCategory.AgAllowUserToEnableMasterHelp = False
        Me.TxtVoucherCategory.AgLastValueTag = Nothing
        Me.TxtVoucherCategory.AgLastValueText = Nothing
        Me.TxtVoucherCategory.AgMandatory = False
        Me.TxtVoucherCategory.AgMasterHelp = True
        Me.TxtVoucherCategory.AgNumberLeftPlaces = 8
        Me.TxtVoucherCategory.AgNumberNegetiveAllow = False
        Me.TxtVoucherCategory.AgNumberRightPlaces = 2
        Me.TxtVoucherCategory.AgPickFromLastValue = False
        Me.TxtVoucherCategory.AgRowFilter = ""
        Me.TxtVoucherCategory.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVoucherCategory.AgSelectedValue = Nothing
        Me.TxtVoucherCategory.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVoucherCategory.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVoucherCategory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVoucherCategory.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVoucherCategory.Location = New System.Drawing.Point(68, 31)
        Me.TxtVoucherCategory.MaxLength = 20
        Me.TxtVoucherCategory.Name = "TxtVoucherCategory"
        Me.TxtVoucherCategory.Size = New System.Drawing.Size(166, 16)
        Me.TxtVoucherCategory.TabIndex = 738
        Me.TxtVoucherCategory.Text = "VoucherCategory"
        Me.TxtVoucherCategory.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(261, 140)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(122, 14)
        Me.Label3.TabIndex = 3005
        Me.Label3.Text = "Current Balance :"
        '
        'MnuOptions
        '
        Me.MnuOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuImportFromExcel, Me.MnuImportFromDos, Me.MnuImportFromTally, Me.MnuImportGSTDataFromExcel, Me.MnuEditSave, Me.MnuCancelEntry})
        Me.MnuOptions.Name = "MnuOptions"
        Me.MnuOptions.Size = New System.Drawing.Size(222, 136)
        '
        'MnuImportFromExcel
        '
        Me.MnuImportFromExcel.Name = "MnuImportFromExcel"
        Me.MnuImportFromExcel.Size = New System.Drawing.Size(221, 22)
        Me.MnuImportFromExcel.Text = "Import From Excel"
        '
        'MnuImportFromDos
        '
        Me.MnuImportFromDos.Name = "MnuImportFromDos"
        Me.MnuImportFromDos.Size = New System.Drawing.Size(221, 22)
        Me.MnuImportFromDos.Text = "Import From Dos"
        '
        'MnuImportFromTally
        '
        Me.MnuImportFromTally.Name = "MnuImportFromTally"
        Me.MnuImportFromTally.Size = New System.Drawing.Size(221, 22)
        Me.MnuImportFromTally.Text = "Import From Tally"
        '
        'MnuImportGSTDataFromExcel
        '
        Me.MnuImportGSTDataFromExcel.Name = "MnuImportGSTDataFromExcel"
        Me.MnuImportGSTDataFromExcel.Size = New System.Drawing.Size(221, 22)
        Me.MnuImportGSTDataFromExcel.Text = "Import GST Data From Excel"
        '
        'MnuEditSave
        '
        Me.MnuEditSave.Name = "MnuEditSave"
        Me.MnuEditSave.Size = New System.Drawing.Size(221, 22)
        Me.MnuEditSave.Text = "Edit & Save"
        '
        'MnuCancelEntry
        '
        Me.MnuCancelEntry.Name = "MnuCancelEntry"
        Me.MnuCancelEntry.Size = New System.Drawing.Size(221, 22)
        Me.MnuCancelEntry.Text = "Cancel Entry"
        '
        'OFDMain
        '
        Me.OFDMain.FileName = "price.xls"
        Me.OFDMain.Filter = "*.xls|*.Xls"
        Me.OFDMain.InitialDirectory = "D:\"
        Me.OFDMain.ShowHelp = True
        Me.OFDMain.Title = "Select Excel File"
        '
        'FrmJournalEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 622)
        Me.ContextMenuStrip = Me.MnuOptions
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.LblCurrentBalance)
        Me.Controls.Add(Me.TxtCustomFields)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.PnlTotals)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.GBoxImportFromExcel)
        Me.Name = "FrmJournalEntry"
        Me.Text = "Sale Invoice"
        Me.Controls.SetChildIndex(Me.GBoxImportFromExcel, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.PnlTotals, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.TxtCustomFields, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.LblCurrentBalance, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
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
        Me.PnlTotals.ResumeLayout(False)
        Me.PnlTotals.PerformLayout()
        Me.Pnl1.ResumeLayout(False)
        Me.GBoxImportFromExcel.ResumeLayout(False)
        Me.MnuOptions.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Protected WithEvents PnlTotals As System.Windows.Forms.Panel
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents Label25 As System.Windows.Forms.Label
    Protected WithEvents LblTotalAmountDr As System.Windows.Forms.Label
    Protected WithEvents LblTotalAmountDrText As System.Windows.Forms.Label
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents LblCurrency As System.Windows.Forms.Label
    Protected WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Protected WithEvents TxtCustomFields As AgControls.AgTextBox
    Public WithEvents GBoxImportFromExcel As System.Windows.Forms.GroupBox
    Public WithEvents BtnImprtFromExcel As System.Windows.Forms.Button
    Protected WithEvents Panel3 As System.Windows.Forms.Panel
    Protected WithEvents Panel2 As System.Windows.Forms.Panel
    Protected WithEvents LblCurrentBalance As Label
    Public WithEvents TxtVoucherCategory As AgControls.AgTextBox
    Protected WithEvents Label3 As Label
    Friend WithEvents MnuOptions As ContextMenuStrip
    Private components As System.ComponentModel.IContainer
    Friend WithEvents MnuImportFromExcel As ToolStripMenuItem
    Friend WithEvents MnuImportFromTally As ToolStripMenuItem
    Friend WithEvents MnuEditSave As ToolStripMenuItem
    Public WithEvents OFDMain As OpenFileDialog
    Friend WithEvents MnuCancelEntry As ToolStripMenuItem
    Friend WithEvents MnuImportFromDos As ToolStripMenuItem
    Friend WithEvents MnuImportGSTDataFromExcel As ToolStripMenuItem
    Protected WithEvents PnlCustomGrid As Panel
#End Region

    Private Sub FrmLedgerHead_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
        Dim DsTemp As DataTable

        mQry = "Delete From TransactionReferences Where DocID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "LedgerHead"
        MainLineTableCsv = "LedgerHeadDetail,LedgerHeadCharges,LedgerHeadDetailCharges,Ledger"
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$

        mCondStr = " And ( Date(H.V_Date) >= " & AgL.Chk_Date(AgL.PubStartDate) & " And  Date(H.V_Date) <= " & AgL.Chk_Date(AgL.PubEndDate) & " Or Vt.NCat='OB') And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "' "
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"
        mCondStr = mCondStr & " And IfNull(Vt.CustomUI,'') = '" & mCustomUI & "'"

        mQry = "Select DocID As SearchCode " &
                " From LedgerHead H " &
                " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " &
                " Where 1 = 1  " & mCondStr & "  Order By V_Date , V_No  "
        mQry = AgL.GetBackendBasedQuery(mQry)
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " And ( Date(H.V_Date) >= " & AgL.Chk_Date(AgL.PubStartDate) & " And Date(H.V_Date) <= " & AgL.Chk_Date(AgL.PubEndDate) & " OR VT.NCAT='" & Ncat.OpeningBalance & "' )"
        mCondStr += " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "'"
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"
        mCondStr = mCondStr & " And IfNull(Vt.CustomUI,'') = '" & mCustomUI & "'"

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, Vt.Description AS [Entry_Type], H.V_Date AS Date, H.ManualRefNo AS [Entry_No], 
                             SG.Name AS [Account Name], L.Amount as AmountDr, L.AmountCr, H.Remarks,  
                             H.EntryBy As [Entry_By], H.EntryDate As [Entry_Date] 
                             FROM LedgerHead H 
                             Left Join LedgerHeadDetail L On H.DocID = L.DocID                             
                             LEFT JOIN viewHelpSubGroup SG On SG.Code  = L.Subcode 
                             LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type 
                             Where 1=1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgDateColumn(Dgl1, Col1EffectiveDate, 115, Col1EffectiveDate, True, False)
            .AddAgTextColumn(Dgl1, Col1Subcode, 250, 0, Col1Subcode, True, False)
            .AddAgTextColumn(Dgl1, Col1LinkedSubcode, 250, 0, Col1LinkedSubcode, True, False)
            .AddAgNumberColumn(Dgl1, Col1AmountDr, 120, 10, 2, False, Col1AmountDr, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1AmountCr, 120, 10, 2, False, Col1AmountCr, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Remark, 250, 255, Col1Remark, True, False)
            .AddAgTextColumn(Dgl1, Col1CurrentBalance, 150, 255, Col1CurrentBalance, False, False)
            .AddAgTextColumn(Dgl1, Col1Nature, 150, 255, Col1Nature, False, False)
            .AddAgTextColumn(Dgl1, Col1SubgroupType, 150, 255, Col1SubgroupType, False, False)
            .AddAgTextColumn(Dgl1, Col1ReferenceNo, 150, 255, Col1ReferenceNo, False, False)
            .AddAgDateColumn(Dgl1, Col1ReferenceDate, 115, Col1ReferenceDate, True, False)
            .AddAgTextColumn(Dgl1, Col1AmsReferenceNo, 150, 255, Col1AmsReferenceNo, False, False)
            .AddAgDateColumn(Dgl1, Col1AmsReferenceDate, 115, Col1AmsReferenceDate, True, False)
            .AddAgNumberColumn(Dgl1, Col1AmsReferenceAmount, 120, 8, 2, False, Col1AmsReferenceAmount, True, False, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        AgL.GridDesign(Dgl1)
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AgAllowFind = False
        Dgl1.AgLastColumn = Dgl1.Columns(Col1Remark).Index
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Dgl1.Anchor = Pnl1.Anchor

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)

        If LblV_Type.Tag <> "" Then
            ApplyUISetting(LblV_Type.Tag)
        Else
            ApplyUISetting(EntryNCat)
        End If

    End Sub


    Private Sub FrmSaleOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry$ = "", bInvoiceType$ = "", bStockSelectionQry$ = ""
        Dim bChargesSelectionQry$ = ""
        Dim mMultiplyWithMinus As Boolean = False



        If Topctrl1.Mode.ToUpper = "EDIT" Then
            mQry = "Delete from Ledger where docId='" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        mQry = " Update LedgerHead " &
                    " SET  " &
                    " ManualRefNo = " & AgL.Chk_Text(TxtReferenceNo.Text) & ", " &
                    " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " &
                    " UploadDate = Null, " &
                    " CustomFields = " & AgL.Chk_Text(TxtCustomFields.Tag) &
                    " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        If UCase(Topctrl1.Mode) = "ADD" Then
            mQry = "Insert Into LedgerHeadCharges(DocID) Values('" & mSearchCode & "') "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If


        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From LedgerHeadDetail  Where DocID = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Subcode, I).Value <> "" Then

                If mMultiplyWithMinus Then
                    Dgl1.Item(Col1AmountDr, I).Value = -Math.Abs(Val(Dgl1.Item(Col1AmountDr, I).Value))
                End If

                If Dgl1.Item(ColSNo, I).Tag Is Nothing And Dgl1.Rows(I).Visible = True Then
                    mSr += 1

                    mQry = "Insert Into LedgerHeadDetail(DocId, Sr,EffectiveDate, Subcode, LinkedSubcode, Amount, AmountCr, ReferenceNo, ReferenceDate, AmsReferenceNo, AmsReferenceDate, AmsReferenceAmount, Remarks) "
                    mQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " &
                            " " & AgL.Chk_Date(Dgl1.Item(Col1EffectiveDate, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1LinkedSubcode, I).Tag) & ", " &
                            " " & Val(Dgl1.Item(Col1AmountDr, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1AmountCr, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceNo, I).Value) & ", " &
                            " " & AgL.Chk_Date(Dgl1.Item(Col1ReferenceDate, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1AmsReferenceNo, I).Value) & ", " &
                            " " & AgL.Chk_Date(Dgl1.Item(Col1AmsReferenceDate, I).Value) & ", " &
                             " " & AgL.VNull(Dgl1.Item(Col1AmsReferenceAmount, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ""
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    'If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                    'bSelectionQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " &
                    '                        " " & AgL.Chk_Date(Dgl1.Item(Col1EffectiveDate, I).Value) & ", " &
                    '                        " " & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & ", " &
                    '                        " " & AgL.Chk_Text(Dgl1.Item(Col1LinkedSubcode, I).Tag) & ", " &
                    '                        " " & Val(Dgl1.Item(Col1AmountDr, I).Value) & ", " &
                    '                        " " & Val(Dgl1.Item(Col1AmountCr, I).Value) & ", " &
                    '                        " " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceNo, I).Value) & ", " &
                    '                        " " & AgL.Chk_Date(Dgl1.Item(Col1ReferenceDate, I).Value) & ", " &
                    '                        " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ""
                Else
                    If Dgl1.Rows(I).Visible = True Then
                        If Dgl1.Rows(I).DefaultCellStyle.BackColor <> RowLockedColour Then
                            mQry = " UPDATE LedgerHeadDetail " &
                                        " Set " &
                                        " EffectiveDate = " & AgL.Chk_Date(Dgl1.Item(Col1EffectiveDate, I).Value) & ", " &
                                        " Subcode = " & AgL.Chk_Text(Dgl1.Item(Col1Subcode, I).Tag) & ", " &
                                        " LinkedSubcode = " & AgL.Chk_Text(Dgl1.Item(Col1LinkedSubcode, I).Tag) & ", " &
                                        " Amount = " & Val(Dgl1.Item(Col1AmountDr, I).Value) & ", " &
                                        " AmountCr = " & Val(Dgl1.Item(Col1AmountCr, I).Value) & ", " &
                                        " ReferenceNo = " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceNo, I).Value) & ", " &
                                        " ReferenceDate = " & AgL.Chk_Date(Dgl1.Item(Col1ReferenceDate, I).Value) & ", " &
                                        " AmsReferenceNo = " & AgL.Chk_Text(Dgl1.Item(Col1AmsReferenceNo, I).Value) & ", " &
                                        " AmsReferenceDate = " & AgL.Chk_Date(Dgl1.Item(Col1AmsReferenceDate, I).Value) & ", " &
                                        " AmsReferenceAmount = " & AgL.VNull(Dgl1.Item(Col1AmsReferenceAmount, I).Value) & ", " &
                                        " Remarks = " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ", " &
                                        " UploadDate = Null " &
                                        " Where DocId = '" & mSearchCode & "' " &
                                        " And Sr = " & Dgl1.Item(ColSNo, I).Tag & " "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        End If
                    Else
                        Dim DtDocID As DataTable
                        mQry = "Select DocID From LedgerHeadDetail with (Nolock) Where DocId = '" & mSearchCode & "' And Sr = " & Val(Dgl1.Item(ColSNo, I).Tag) & ""
                        DtDocID = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

                        mQry = " Delete From LedgerHeadDetail Where DocId = '" & mSearchCode & "' And Sr = " & Val(Dgl1.Item(ColSNo, I).Tag) & "  "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                        mQry = "Delete From Ledger Where ReferenceDocID = '" & SearchCode & "' And ReferenceDocIdSr = " & Val(Dgl1.Item(ColSNo, I).Tag) & "  "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                        mQry = "Delete From TransactionReferences Where DocID = '" & SearchCode & "' And DocIDSr=" & Val(Dgl1.Item(ColSNo, I).Tag) & ""
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                        mQry = "Delete From LedgerHeadDetail Where ReferenceDocID = '" & SearchCode & "' And ReferenceDocIdSr = " & Val(Dgl1.Item(ColSNo, I).Tag) & " "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    End If
                End If

            End If
        Next

        'If bSelectionQry <> "" Then
        '    mQry = "Insert Into LedgerHeadDetail(DocId, Sr,EffectiveDate, Subcode, LinkedSubcode, Amount, AmountCr, ReferenceNo, ReferenceDate, Remarks " &
        '               " ) " & bSelectionQry
        '    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        'End If




        Dim mNarr As String = ""
        Dim mNarrParty As String = ""


        PostGridToAccounts(SearchCode, mMultiplyWithMinus, Conn, Cmd)

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        End If
    End Sub

    Sub PostGridToAccounts(DocID As String, MultiplyWithMinus As Boolean, ByRef Conn As Object, ByRef Cmd As Object)
        Dim mLedgerPostingData As String = ""
        Dim I As Integer
        Dim DtTemp As DataTable
        Dim mContraCodeDr As String = ""
        Dim mContraNameDr As String = ""
        Dim mContraNameCr As String = ""
        Dim mContraCodeCr As String = ""
        Dim mRecordCountDr As Integer
        Dim mRecordCountCr As Integer
        Dim mNarration As String = ""
        Dim StrContraTextJV As String = ""

        mRecordCountDr = 0
        mRecordCountCr = 0
        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Rows(I).Visible = True And (Val(Dgl1(Col1AmountDr, I).Value) <> 0 Or Val(Dgl1(Col1AmountCr, I).Value) <> 0) Then
                If Val(Dgl1(Col1AmountDr, I).Value) <> 0 Then
                    mContraCodeDr = AgL.XNull(Dgl1(Col1Subcode, I).Tag)
                    mContraNameDr = AgL.XNull(Dgl1(Col1Subcode, I).Value)
                    mRecordCountDr += 1
                Else
                    mContraCodeCr = AgL.XNull(Dgl1(Col1Subcode, I).Tag)
                    mContraNameCr = AgL.XNull(Dgl1(Col1Subcode, I).Value)
                    mRecordCountCr += 1
                End If
            End If
        Next

        If LblV_Type.Tag = Ncat.JournalVoucher Then
            If mRecordCountDr > 1 And mRecordCountCr > 1 Then
                mContraCodeCr = "" : mContraNameCr = ""
                mContraCodeDr = "" : mContraNameDr = ""
            End If
        End If

        If LblV_Type.Tag = Ncat.OpeningBalance Then
            mContraCodeCr = "" : mContraNameCr = ""
            mContraCodeDr = "" : mContraNameDr = ""
        End If


        Dim bTableName As String = "[" + Guid.NewGuid().ToString() + "]"

        If AgL.IsTableExist(bTableName.Replace("[", "").Replace("]", ""), IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)) Then
            mQry = "Drop Table " + bTableName
            AgL.Dman_ExecuteNonQry(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead))
        End If

        mQry = " CREATE TABLE " & bTableName & "(Subcode NVARCHAR(10), LinkedSubcode NVARCHAR(10), ContraAc NVARCHAR(10), 
                            AmtDr Float, AmtCr Float, Narration NVARCHAR(255), 
                            ChqNo NVARCHAR(20), ChqDate DateTime, EffectiveDate DateTime, 
                            ReferenceNo NVARCHAR(20), ReferenceDate DateTime) "
        AgL.Dman_ExecuteNonQry(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead))


        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Rows(I).Visible = True Then
                If Val(Dgl1(Col1AmountDr, I).Value) <> 0 Then
                    If LblV_Type.Tag = Ncat.OpeningBalance Then
                        mNarration = Dgl1(Col1Remark, I).Value
                        If AgL.XNull(Dgl1.Item(Col1ReferenceNo, I).Value) <> "" Then
                            mNarration = mNarration & " Ref.No. : " & AgL.XNull(Dgl1.Item(Col1ReferenceNo, I).Value)
                            If AgL.XNull(Dgl1.Item(Col1ReferenceDate, I).Value) <> "" Then
                                mNarration = mNarration & " Dated " & AgL.XNull(Dgl1.Item(Col1ReferenceDate, I).Value)
                            End If
                        End If

                        If AgL.XNull(Dgl1.Item(Col1AmsReferenceNo, I).Value) <> "" Then
                            mNarration = mNarration & " AMS No. : " & AgL.XNull(Dgl1.Item(Col1AmsReferenceNo, I).Value)
                            If AgL.XNull(Dgl1.Item(Col1AmsReferenceDate, I).Value) <> "" Then
                                mNarration = mNarration & " Dated " & AgL.XNull(Dgl1.Item(Col1AmsReferenceDate, I).Value)
                            End If

                            If AgL.VNull(Dgl1.Item(Col1AmsReferenceAmount, I).Value) > "0" Then
                                mNarration = mNarration & " AMS Amt. " & AgL.VNull(Dgl1.Item(Col1AmsReferenceAmount, I).Value).ToString
                            End If
                        End If
                    Else
                        mNarration = mContraNameCr & ". " & Dgl1(Col1Remark, I).Value
                    End If
                    If StrContraTextJV <> "" Then StrContraTextJV += vbCrLf
                    FPrepareContraText(False, StrContraTextJV, Dgl1(Col1Subcode, I).Tag, Val(Dgl1(Col1AmountDr, I).Value), "DR")
                    'If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                    mLedgerPostingData = " INSERT INTO " & bTableName & "(Subcode, LinkedSubcode, ContraAc, 
                            AmtDr, AmtCr, Narration, ChqNo, ChqDate, EffectiveDate, ReferenceNo, ReferenceDate)"
                    mLedgerPostingData += " Select " & AgL.Chk_Text(Dgl1(Col1Subcode, I).Tag) & " as Subcode, " & AgL.Chk_Text(Dgl1(Col1LinkedSubcode, I).Tag) & " as LinkedSubcode, " & AgL.Chk_Text(mContraCodeCr) & "  as ContraAc, " & Val(Dgl1(Col1AmountDr, I).Value) & " as AmtDr, 0 as AmtCr, " & AgL.Chk_Text(mNarration) & " as Narration, Null as ChqNo, Null as ChqDate, " & AgL.Chk_Date(Dgl1(Col1EffectiveDate, I).Value) & " as EffectiveDate,
                                " & AgL.Chk_Text(Dgl1(Col1ReferenceNo, I).Value) & " as ReferenceNo,
                                " & AgL.Chk_Date(Dgl1(Col1ReferenceDate, I).Value) & " as ReferenceDate "
                    AgL.Dman_ExecuteNonQry(mLedgerPostingData, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead))
                End If
                If Val(Dgl1(Col1AmountCr, I).Value) <> 0 Then
                    If LblV_Type.Tag = Ncat.OpeningBalance Then
                        mNarration = TxtV_Type.Text & " " & Dgl1(Col1Remark, I).Value
                    Else
                        mNarration = TxtV_Type.Text & " : " & mContraNameDr & ". " & Dgl1(Col1Remark, I).Value
                    End If
                    If StrContraTextJV <> "" Then StrContraTextJV += vbCrLf
                    FPrepareContraText(False, StrContraTextJV, Dgl1(Col1Subcode, I).Tag, Val(Dgl1(Col1AmountCr, I).Value), "CR")
                    'If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
                    mLedgerPostingData = " INSERT INTO " & bTableName & "(Subcode, LinkedSubcode, ContraAc, 
                            AmtDr, AmtCr, Narration, ChqNo, ChqDate, EffectiveDate, ReferenceNo, ReferenceDate)"
                    mLedgerPostingData += " Select " & AgL.Chk_Text(Dgl1(Col1Subcode, I).Tag) & " as Subcode, " & AgL.Chk_Text(Dgl1(Col1LinkedSubcode, I).Tag) & " as LinkedSubcode, " & AgL.Chk_Text(mContraCodeDr) & "  as ContraAc, 0 as AmtDr,  " & Val(Dgl1(Col1AmountCr, I).Value) & " as AmtCr, " & AgL.Chk_Text(mNarration) & " as Narration, Null as ChqNo, Null as ChqDate, " & AgL.Chk_Date(Dgl1(Col1EffectiveDate, I).Value) & " as EffectiveDate,
                                " & AgL.Chk_Text(Dgl1(Col1ReferenceNo, I).Value) & " as ReferenceNo,
                                " & AgL.Chk_Date(Dgl1(Col1ReferenceDate, I).Value) & " as ReferenceDate "
                    AgL.Dman_ExecuteNonQry(mLedgerPostingData, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead))
                End If
            End If
        Next



        'If mLedgerPostingData <> "" Then mLedgerPostingData += " UNION ALL "
        'mNarration = TxtV_Type.Text & " : " & mNarration
        'mLedgerPostingData += " Select " & AgL.Chk_Text(TxtPartyName.Tag) & " as Subcode, Null as LinkedSubcode, " & AgL.Chk_Text(TxtPartyName.Tag) & "  as ContraAc, " & IIf(mHeaderAccountDrCr.ToUpper = "DR", Val(LblTotalAmount.Text), 0) & " as AmtDr, " & IIf(mHeaderAccountDrCr.ToUpper = "CR", Val(LblTotalAmount.Text), 0) & " as AmtCr, " & AgL.Chk_Text(mNarration) & " as Narration, null as ChqNo, Null as ChqDate, Null as EffectiveDate "

        'If mLedgerPostingData = "" Then Exit Sub
        mLedgerPostingData = " Select * From " & bTableName & " With (NoLock) "

        mLedgerPostingData = "Select SubCode, LinkedSubcode, ContraAc, Narration, AmtDr*1.0 as AmtDr, AmtCr*1.0 as AmtCr, ChqNo, ChqDate, EffectiveDate, ReferenceNo, ReferenceDate  
                              From (" & mLedgerPostingData & ") as X  "
        DtTemp = AgL.FillData(mLedgerPostingData, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                mQry = "INSERT INTO Ledger
                        (DocId, V_SNo, V_No, V_Type, RecID, V_Prefix, 
                        V_Date, SubCode, LinkedSubcode, ContraSub, AmtDr, AmtCr, 
                        Chq_No, Chq_Date, EffectiveDate, Narration, ContraText, Site_Code, DivCode, 
                        U_Name, U_EntDt, U_AE)
                        VALUES('" & DocID & "', " & I + 1 & ", " & Val(TxtV_No.Text) & ", " & AgL.Chk_Text(TxtV_Type.Tag) & ", 
                        " & AgL.Chk_Text(IIf(AgL.XNull(DtTemp.Rows(I)("ReferenceNo")) <> "", AgL.XNull(DtTemp.Rows(I)("ReferenceNo")), TxtReferenceNo.Text)) & ", 
                        " & AgL.Chk_Text(LblPrefix.Text) & ",
                        " & AgL.Chk_Date(IIf(AgL.XNull(DtTemp.Rows(I)("ReferenceDate")) <> "", AgL.XNull(DtTemp.Rows(I)("ReferenceDate")), TxtV_Date.Text)) & ", 
                        " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Subcode"))) & ", " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("LinkedSubcode"))) & ", " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("ContraAc"))) & ", " & Val(AgL.VNull(DtTemp.Rows(I)("AmtDr"))) & ", " & Val(AgL.VNull(DtTemp.Rows(I)("AmtCr"))) & ",
                        " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("ChqNo"))) & "," & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("ChqDate"))) & "," & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("EffectiveDate"))) & "," & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("Narration"))) & ", " & AgL.Chk_Text(StrContraTextJV) & "," & AgL.Chk_Text(TxtSite_Code.Tag) & "," & AgL.Chk_Text(TxtDivision.Tag) & ",
                        " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Date(AgL.PubLoginDate) & ", 'A'
                        )"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            Next
        End If

        If AgL.IsTableExist(bTableName.Replace("[", "").Replace("]", ""), IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)) Then
            mQry = "Drop Table " + bTableName
            AgL.Dman_ExecuteNonQry(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead))
        End If
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim mMultiplyWithMinus As Boolean = False
        Dim DsTemp As DataSet

        LblTotalAmountDr.Text = 0
        LblTotalAmountCr.Text = 0

        mQry = " 
                Select H.*, Sg.Name as AccountName, Sg.Nature, VT.Category as VoucherCategory, HC.*                                 
                From (Select * From LedgerHead  Where DocID='" & SearchCode & "') H 
                Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type
                Left Join LedgerHeadCharges Hc on H.DocID = HC.DocID
                LEFT JOIN viewHelpSubgroup Sg  ON H.Subcode = Sg.Code                
                "
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                'TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                'TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.AgSelectedValue, AgL.GcnRead)


                TxtVoucherCategory.Text = AgL.XNull(.Rows(0)("VoucherCategory"))


                TxtCustomFields.AgSelectedValue = AgL.XNull(.Rows(0)("CustomFields"))


                IniGrid()

                TxtReferenceNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))



                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))




                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------

                mQry = "Select L.*, Sg.Name as AccountName, Lsg.Name as LinkedAccountName, Sg.Nature, Sg.SubgroupType, 
                        U.DecimalPlaces, U.DecimalPlaces As QtyDecimalPlaces, LC.* 
                        From (Select * From LedgerHeadDetail  Where DocId = '" & SearchCode & "') As L 
                        LEFT JOIN viewHelpSubgroup Sg  ON L.Subcode = Sg.Code 
                        LEFT JOIN viewHelpSubgroup Lsg  ON L.LinkedSubcode = Lsg.Code 
                        Left Join Unit U On L.Unit = U.Code 
                        Left Join LedgerHeadDetailCharges LC on L.DocID = LC.DocID And L.Sr = LC.Sr
                        Order By L.Sr "

                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))




                            Dgl1.Item(Col1Subcode, I).Tag = AgL.XNull(.Rows(I)("Subcode"))
                            Dgl1.Item(Col1Subcode, I).Value = AgL.XNull(.Rows(I)("AccountName"))

                            Dgl1.Item(Col1LinkedSubcode, I).Tag = AgL.XNull(.Rows(I)("LinkedSubcode"))
                            Dgl1.Item(Col1LinkedSubcode, I).Value = AgL.XNull(.Rows(I)("LinkedAccountName"))

                            Dgl1.Item(Col1ReferenceNo, I).Value = AgL.XNull(.Rows(I)("ReferenceNo"))
                            Dgl1.Item(Col1ReferenceDate, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("ReferenceDate")))

                            Dgl1.Item(Col1AmsReferenceNo, I).Value = AgL.XNull(.Rows(I)("AmsReferenceNo"))
                            Dgl1.Item(Col1AmsReferenceDate, I).Value = AgL.XNull(.Rows(I)("AmsReferenceDate"))
                            Dgl1.Item(Col1AmsReferenceAmount, I).Value = AgL.VNull(.Rows(I)("AmsReferenceAmount"))

                            Dgl1.Item(Col1SubgroupType, I).Value = AgL.XNull(.Rows(I)("SubgroupType"))
                            Dgl1.Item(Col1Nature, I).Value = AgL.XNull(.Rows(I)("Nature"))

                            Dgl1.Item(Col1CurrentBalance, I).Value = FGetCurrBal(Dgl1.Item(Col1Subcode, I).Tag, TxtV_Date.Text)
                            FShowCurrBal(I)

                            If AgL.VNull(.Rows(I)("Amount")) = 0 Then
                                Dgl1.Item(Col1AmountDr, I).Value = ""
                            Else
                                Dgl1.Item(Col1AmountDr, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Amount"))), "0.00")
                            End If
                            If AgL.VNull(.Rows(I)("AmountCr")) = 0 Then
                                Dgl1.Item(Col1AmountCr, I).Value = ""
                            Else
                                Dgl1.Item(Col1AmountCr, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("AmountCr"))), "0.00")
                            End If
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remarks"))





                            LblTotalAmountDr.Text = Val(LblTotalAmountDr.Text) + Val(Dgl1.Item(Col1AmountDr, I).Value)
                            LblTotalAmountCr.Text = Val(LblTotalAmountCr.Text) + Val(Dgl1.Item(Col1AmountCr, I).Value)



                            If AgL.Dman_Execute("Select Count(*) From TransactionReferences Where ReferenceDocId = '" & mSearchCode & "'
                                        And ReferenceSr = " & Val(Dgl1.Item(ColSNo, I).Tag) & "  And Type = '" & TransactionReferenceTypeConstants.Cancelled & "' ", AgL.GCn).ExecuteScalar() > 0 Then
                                Dgl1.Rows(I).DefaultCellStyle.BackColor = ColorConstants.Cancelled
                                Dgl1.Rows(I).ReadOnly = True
                            End If

                        Next I
                    End If
                End With
            End If
        End With
    End Sub

    Private Sub FrmSaleOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
    End Sub



    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtReferenceNo.Validating
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim FrmObj As New FrmSaleInvoiceParty
        Try
            Select Case sender.NAME
                Case TxtV_Type.Name
                    If TxtV_Type.Tag = "" Then Exit Sub
                    TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.AgSelectedValue, AgL.GcnRead)
                    TxtVoucherCategory.Text = AgL.Dman_Execute("Select Category From Voucher_Type Where V_Type = '" & TxtV_Type.Tag & "'", AgL.GCn).ExecuteScalar
                    If LblV_Type.Tag = Ncat.OpeningBalance Then
                        TxtV_Date.Text = ClsMain.FormatDate(DateAdd(DateInterval.Day, -1, CDate(AgL.PubStartDate)))
                    End If
                    IniGrid()
                    TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "LedgerHead", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                    FGetSettingVariableValuesForAddAndEdit()

                Case TxtReferenceNo.Name
                    e.Cancel = Not AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "LedgerHead",
                                        TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue,
                                        TxtSite_Code.AgSelectedValue, Topctrl1.Mode,
                                        TxtReferenceNo.Text, mSearchCode)

                Case TxtV_Date.Name
                    TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "LedgerHead", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FShowCurrBal(rowIndex As Integer) As Double
        LblCurrentBalance.Text = Format(Val(Dgl1.Item(Col1CurrentBalance, rowIndex).Value), "0.00")
        FShowCurrBal = Val(Dgl1.Item(Col1CurrentBalance, rowIndex).Value)

        If Val(LblCurrentBalance.Text) < 0 Then
            LblCurrentBalance.ForeColor = Color.Red
            LblCurrentBalance.Text = LblCurrentBalance.Text & " Cr."
        ElseIf Val(LblCurrentBalance.Text) > 0 Then
            LblCurrentBalance.ForeColor = Color.ForestGreen
            LblCurrentBalance.Text = LblCurrentBalance.Text & " Dr."
        Else
            LblCurrentBalance.ForeColor = Color.Black
        End If

    End Function

    Private Function FGetCurrBal(Subcode As String, V_Date As Date) As Double
        mQry = " Select IfNull(Sum(AmtDr),0) - IfNull(Sum(AmtCr),0) As CurrBal 
                From Ledger 
                Where SubCode = '" & Subcode & "' 
                And Date(V_Date) <= " & AgL.Chk_Date(CDate(TxtV_Date.Text).ToString("s")) & "
                And Ledger.Site_Code = '" & TxtSite_Code.Tag & "'
                And Ledger.DivCode = '" & TxtDivision.Tag & "'"

        FGetCurrBal = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
    End Function

    Private Sub FrmSaleOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd

        TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.AgSelectedValue, AgL.GCn)

        TxtVoucherCategory.Text = AgL.Dman_Execute("Select Category From Voucher_Type Where V_Type = '" & TxtV_Type.Tag & "'", AgL.GCn).ExecuteScalar


        IniGrid()
        TabControl1.SelectedTab = TP1

        If LblV_Type.Tag = Ncat.OpeningBalance Then
            TxtV_Date.Text = ClsMain.FormatDate(DateAdd(DateInterval.Day, -1, CDate(AgL.PubStartDate)))
        End If

        TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "LedgerHead", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
        FGetSettingVariableValuesForAddAndEdit()
    End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Subcode
                    mQry = "Select Sg.Nature, Sg.SubgroupType From viewHelpSubgroup Sg Where Sg.code = '" & Dgl1(Col1Subcode, mRowIndex).Tag & "' "
                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        Dgl1(Col1Nature, mRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("Nature"))
                        Dgl1(Col1SubgroupType, mRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("SubgroupType"))
                    End If

                    Dgl1.Item(Col1CurrentBalance, mRowIndex).Value = FGetCurrBal(Dgl1.Item(Col1Subcode, mRowIndex).Tag, TxtV_Date.Text)
                    FShowCurrBal(mRowIndex)

                    If ClsMain.IsScopeOfWorkContains("+Cloth Aadhat Module") Then
                        mQry = "Select Sg.Code, Sg.Name From viewHelpSubgroup Sg Where Sg.code = (Select Parent From Subgroup Where Subcode = '" & Dgl1(Col1Subcode, mRowIndex).Tag & "')"
                        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtTemp.Rows.Count > 0 Then
                            Dgl1(Col1LinkedSubcode, mRowIndex).Tag = AgL.XNull(DtTemp.Rows(0)("Code"))
                            Dgl1(Col1LinkedSubcode, mRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("Name"))
                        End If
                    End If

                    If SettingFields_CopyRemarkInNextLineYn = True Then
                        If mRowIndex > 0 Then
                            If Dgl1.Item(Col1Remark, mRowIndex).Value = "" And Dgl1.Item(Col1Remark, mRowIndex - 1).Value <> "" Then
                                Dgl1.Item(Col1Remark, mRowIndex).Value = Dgl1.Item(Col1Remark, mRowIndex - 1).Value
                            End If
                        End If
                    End If
                Case Col1AmountDr
                    Dgl1.Item(Col1AmountCr, mRowIndex).Value = 0
                Case Col1AmountCr
                    Dgl1.Item(Col1AmountDr, mRowIndex).Value = 0
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer

        If Topctrl1.Mode = "Browse" Then Exit Sub

        LblTotalAmountDr.Text = 0
        LblTotalAmountCr.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Rows(I).Visible Then
                If Dgl1.Item(Col1Subcode, I).Value <> "" Then
                    LblTotalAmountDr.Text = Val(LblTotalAmountDr.Text) + Val(Dgl1.Item(Col1AmountDr, I).Value)
                    LblTotalAmountCr.Text = Val(LblTotalAmountCr.Text) + Val(Dgl1.Item(Col1AmountCr, I).Value)
                End If
            End If
        Next

        If Dgl1.CurrentCell IsNot Nothing Then
            If Dgl1.Item(Col1Subcode, Dgl1.CurrentCell.RowIndex).Value <> "" And Val(Dgl1.Item(Col1AmountDr, Dgl1.CurrentCell.RowIndex).Value) = 0 And Val(Dgl1.Item(Col1AmountCr, Dgl1.CurrentCell.RowIndex).Value) = 0 Then
                If Val(LblTotalAmountDr.Text) - Val(LblTotalAmountCr.Text) > 0 Then
                    Dgl1.Item(Col1AmountCr, Dgl1.CurrentCell.RowIndex).Value = Val(LblTotalAmountDr.Text) - Val(LblTotalAmountCr.Text)
                    LblTotalAmountCr.Text = LblTotalAmountDr.Text
                End If
                If Val(LblTotalAmountDr.Text) - Val(LblTotalAmountCr.Text) < 0 Then
                    Dgl1.Item(Col1AmountDr, Dgl1.CurrentCell.RowIndex).Value = Math.Abs(Val(LblTotalAmountDr.Text) - Val(LblTotalAmountCr.Text))
                    LblTotalAmountDr.Text = LblTotalAmountCr.Text
                End If
            End If
        End If

        LblTotalAmountDr.Text = Val(LblTotalAmountDr.Text)
        LblTotalAmountCr.Text = Val(LblTotalAmountCr.Text)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim bQcPassedQty As Double = 0, bInvoicedQty As Double = 0
        Dim bOrderQty As Double = 0, bInvoiceQty As Double = 0
        Dim CheckDuplicateRef As Boolean

        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Subcode).Index) Then passed = False : Exit Sub





        With Dgl1
            For I = 0 To .Rows.Count - 1
                If Dgl1.Rows(I).Visible Then
                    If .Item(Col1Subcode, I).Value <> "" Then
                        If Val(.Item(Col1AmountDr, I).Value) = 0 And Val(.Item(Col1AmountCr, I).Value) = 0 Then
                            MsgBox("Amount Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                            .CurrentCell = .Item(Col1AmountDr, I) : Dgl1.Focus()
                            passed = False : Exit Sub
                        End If

                        If AgL.XNull(.Item(Col1ReferenceDate, I).Value) <> "" Then
                            If CDate(.Item(Col1ReferenceDate, I).Value) > CDate(AgL.PubEndDate) Then
                                MsgBox("Reference Date can not exceed year end date")
                                .CurrentCell = .Item(Col1ReferenceDate, I) : Dgl1.Focus()
                                passed = False : Exit Sub
                            End If
                            If FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
                                If CDate(.Item(Col1ReferenceDate, I).Value) > CDate("31-Dec-2019") Then
                                    MsgBox("Reference Date can not exceed year end date")
                                    .CurrentCell = .Item(Col1ReferenceDate, I) : Dgl1.Focus()
                                    passed = False : Exit Sub
                                End If
                            End If
                        End If

                        If ClsMain.IsScopeOfWorkContains("+Cloth Aadhat Module") Then
                            Select Case Dgl1.Item(Col1SubgroupType, I).Value.ToString.ToUpper
                                Case SubgroupType.Customer.ToUpper, SubgroupType.Supplier.ToUpper
                                    If AgL.XNull(Dgl1.Item(Col1LinkedSubcode, I).Value) = "" Then
                                        MsgBox("Linked Account can not be blank.")
                                        Dgl1.CurrentCell = Dgl1.Item(Col1LinkedSubcode, I)
                                        Dgl1.Focus()
                                        passed = False : Exit Sub
                                    End If
                            End Select
                        End If
                    End If
                End If
            Next
        End With

        If LblV_Type.Tag <> Ncat.OpeningBalance Then
            If Val(LblTotalAmountCr.Text) <> Val(LblTotalAmountDr.Text) Then
                MsgBox("Debit and Credit balances are not equal. Can not continue.")
                passed = False : Exit Sub
            End If
        End If

        CheckDuplicateRef = AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "LedgerHead",
                                        TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue,
                                        TxtSite_Code.AgSelectedValue, Topctrl1.Mode,
                                        TxtReferenceNo.Text, mSearchCode)

        If Not CheckDuplicateRef Then
            TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "LedgerHead", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
            CheckDuplicateRef = True
        End If

        passed = CheckDuplicateRef
    End Sub

    Private Sub TxtBuyer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyCode = Keys.Enter Then Exit Sub
            Select Case sender.name
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                'Case Col1Amount
                '    Dgl1.CurrentCell.ReadOnly = IIf(Dgl1.Item(Col1Amount, Dgl1.CurrentCell.RowIndex).Tag Is Nothing, False, True)
            End Select

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TempLedgerHead_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        'If BlnIsTotalDeliveryMeasureVisible = False Then LblTotalDeliveryMeasure.Visible = False : LblTotalDeliveryMeasureText.Visible = False
        'If BlnIsMeasureVisible = False Then LblTotalMeasure.Visible = False : LblTotalMeasureText.Visible = False
        'If BlnIsBaleNoVisible = False Then LblTotalBale.Visible = False : LblTotalBaleText.Visible = False
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown

        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.visible = False
            Calculation()
        End If
        'If e.Control And e.KeyCode = Keys.D Then
        '    sender.CurrentRow.Selected = True
        'End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub

        If Dgl1.CurrentCell IsNot Nothing Then
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Subcode

            End Select
        End If
    End Sub

    Private Sub FrmCarpetMaterialPlan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 654, 990, 0, 0)

        If Not AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
            MnuEditSave.Visible = False
            MnuImportFromDos.Visible = False
            MnuImportFromExcel.Visible = False
            MnuImportGSTDataFromExcel.Visible = False
            MnuImportFromTally.Visible = False
        End If
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub RbtInvoiceDirect_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If Dgl1.CurrentCell IsNot Nothing Then
                Select Case sender.Name

                End Select
            End If

            If Dgl1.AgHelpDataSet(Col1Subcode) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Subcode) = Nothing
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmLedgerHead_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        If Dgl1.AgHelpDataSet(Col1Subcode) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Subcode).Dispose() : Dgl1.AgHelpDataSet(Col1Subcode) = Nothing
        If Dgl1.AgHelpDataSet(Col1LinkedSubcode) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1LinkedSubcode).Dispose() : Dgl1.AgHelpDataSet(Col1LinkedSubcode) = Nothing
    End Sub


    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Subcode
                    'If e.KeyCode = Keys.Insert Then Call FOpenLedgerHead()
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Subcode) Is Nothing Then
                            FCreateHelpSubgroupLine()
                        End If
                    End If
                Case Col1LinkedSubcode
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1LinkedSubcode) Is Nothing Then
                            FCreateHelpLinkedSubgroupLine()
                        End If
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmSaleQuotation_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        GBoxImportFromExcel.Enabled = False
    End Sub

    Private Sub Dgl1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dgl1.Leave
        Dgl.Visible = False
    End Sub

    Private Sub FCheckDuplicate(ByVal mRow As Integer)
        Dim I As Integer = 0
        Try
            With Dgl1
                For I = 0 To .Rows.Count - 1
                    If .Item(Col1Subcode, I).Value <> "" Then
                        If mRow <> I Then
                            If AgL.StrCmp(.Item(Col1Subcode, I).Value, .Item(Col1Subcode, mRow).Value) Then
                                If MsgBox("Item " & .Item(Col1Subcode, I).Value & " Is Already Feeded At Row No " & .Item(ColSNo, I).Value & ".Do You Want To Continue ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                                    Dgl1.Item(Col1Subcode, mRow).Tag = "" : Dgl1.Item(Col1Subcode, mRow).Value = ""
                                End If
                                '.CurrentCell = .Item(Col1Item, I) : Dgl1.Focus()
                                '.Rows.Remove(.Rows(mRow)) : Exit Sub
                            End If
                        End If
                    End If
                Next
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        If Dgl1.Rows.Count > 0 Then
            Dgl1.CurrentCell = Dgl1.Item(Col1Subcode, Dgl1.Rows.Count - 1) : Dgl1.Focus()
        End If
    End Sub


    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, TxtSite_Code.Tag, TxtVoucherCategory.Tag, LblV_Type.Tag, TxtV_Type.Tag, "", "")
        FGetSettings = mValue
    End Function

    Private Sub FCreateHelpLinkedSubgroupLine()
        Dim strCond As String = ""

        If LblV_Type.Tag = Ncat.OpeningBalance Then
            If Dgl1.Item(Col1SubgroupType, Dgl1.CurrentCell.RowIndex).Value.ToString.ToUpper = SubgroupType.Supplier.ToUpper Then
                mQry = "
                        SELECT Sg.Code, Sg.Name, Sg.Address, Sg.SubgroupType
                        FROM viewHelpSubGroup Sg  With (NoLock)                
                        Where Sg.SubgroupType In ('Master Supplier') 
                       "
            Else
                mQry = "
                        SELECT Sg.Code, Sg.Name, Sg.Address, Sg.SubgroupType
                        FROM viewHelpSubGroup Sg  With (NoLock)                
                        Where Sg.SubgroupType In ('Master Customer') 
                       "
            End If
            Dgl1.AgHelpDataSet(Col1LinkedSubcode) = AgL.FillData(mQry, AgL.GCn)
        Else
            If (TxtV_Type.Tag = "JVA" Or TxtV_Type.Tag = "JV") And (AgL.StrCmp(AgL.PubDBName, "RVN") Or AgL.StrCmp(AgL.PubDBName, "RVN1") Or AgL.StrCmp(AgL.PubDBName, "RVN2") Or AgL.StrCmp(AgL.PubDBName, "MLAW")) Then
                mQry = "SELECT Sg.Code, Sg.Name, Ag.GroupName
                FROM viewHelpSubGroup Sg
                Left Join AcGroup Ag On Sg.GroupCode = Ag.GroupCode                 
                Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
                mQry = mQry & " And Sg.SubgroupType Not In ('Master Customer','Master Supplier', 'Ship To Party')"
            Else
                mQry = "SELECT Sg.Code, Sg.Name, Sg.Address, Sg.SubgroupType
                FROM viewHelpSubGroup Sg  With (NoLock)                
                Where Sg.Code In (Select LinkedSubcode From Ledger 
                                  Where Subcode='" & Dgl1(Col1Subcode, Dgl1.CurrentCell.RowIndex).Tag & "') 
                                  or Sg.Code =(Select Parent From Subgroup 
                                               Where Subcode ='" & Dgl1(Col1Subcode, Dgl1.CurrentCell.RowIndex).Tag & "') "
            End If
            Dgl1.AgHelpDataSet(Col1LinkedSubcode) = AgL.FillData(mQry, AgL.GCn)
        End If

    End Sub


    Private Sub FCreateHelpSubgroupLine()
        Dim strCond As String = ""

        If FGetSettings(SettingFields.FilterInclude_SubgroupTypeLine, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_SubgroupTypeLine, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || Sg.SubgroupType,'" & FGetSettings(SettingFields.FilterInclude_SubgroupTypeLine, SettingType.General) & "') > 0 OR Sg.SubgroupType Is Null) "
            ElseIf FGetSettings(SettingFields.FilterInclude_SubgroupTypeLine, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || Sg.SubgroupType,'" & FGetSettings(SettingFields.FilterInclude_SubgroupTypeLine, SettingType.General) & "') <= 0 OR Sg.SubgroupType Is Null) "
            End If
        End If

        If FGetSettings(SettingFields.FilterInclude_AcGroupLine, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_AcGroupLine, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || Sg.GroupCode,'" & FGetSettings(SettingFields.FilterInclude_AcGroupLine, SettingType.General) & "') > 0  "
            ElseIf FGetSettings(SettingFields.FilterInclude_AcGroupLine, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || Sg.GroupCode,'" & FGetSettings(SettingFields.FilterInclude_AcGroupLine, SettingType.General) & "') <= 0  "
            End If
        End If


        If FGetSettings(SettingFields.FilterInclude_NatureLine, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_NatureLine, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And CharIndex('+' || Sg.Nature,'" & FGetSettings(SettingFields.FilterInclude_NatureLine, SettingType.General) & "') > 0  "
            ElseIf FGetSettings(SettingFields.FilterInclude_NatureLine, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And CharIndex('-' || Sg.Nature,'" & FGetSettings(SettingFields.FilterInclude_NatureLine, SettingType.General) & "') <= 0  "
            End If
        End If


        mQry = "SELECT Sg.Code, Sg.Name, Ag.GroupName
                FROM viewHelpSubGroup Sg
                Left Join AcGroup Ag On Sg.GroupCode = Ag.GroupCode                 
                Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        mQry = mQry & " And Sg.SubgroupType Not In ('Master Customer','Master Supplier', 'Ship To Party')"
        Dgl1.AgHelpDataSet(Col1Subcode) = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT Sg.Code, Sg.Name, Ag.GroupName
                FROM viewHelpSubGroup Sg                 
                Left Join AcGroup Ag On Sg.GroupCode = Ag.GroupCode                 
                Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        mQry = mQry & " And Sg.SubgroupType In ('Master Customer','Master Supplier')"
        Dgl1.AgHelpDataSet(Col1LinkedSubcode) = AgL.FillData(mQry, AgL.GCn)

        'If LblV_Type.Tag = Ncat.OpeningBalance Then
        '    mQry = "SELECT Sg.Code, Sg.Name, Ag.GroupName
        '        FROM viewHelpSubGroup Sg
        '        Left Join AcGroup Ag On Sg.GroupCode = Ag.GroupCode                 
        '        Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        '    If FGetSettings(SettingFields.FilterInclude_AcTreeNodeType, SettingType.General).ToString.ToUpper.Contains(TreeNodeType.Leaf.ToUpper) Then
        '        mQry += " And Sg.Parent Is Not Null "
        '    ElseIf FGetSettings(SettingFields.FilterInclude_AcTreeNodeType, SettingType.General).ToString.ToUpper.Contains(TreeNodeType.Root.ToUpper) Then
        '        mQry += " And Sg.Parent Is Null "
        '    End If
        '    Dgl1.AgHelpDataSet(Col1Subcode) = AgL.FillData(mQry, AgL.GCn)

        '    mQry = "SELECT Sg.Code, Sg.Name, Ag.GroupName
        '        FROM viewHelpSubGroup Sg                 
        '        Left Join AcGroup Ag On Sg.GroupCode = Ag.GroupCode                 
        '        Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        '    If FGetSettings(SettingFields.FilterInclude_AcTreeNodeType, SettingType.General).ToString.ToUpper.Contains(TreeNodeType.Leaf.ToUpper) Then
        '        mQry += " And Sg.Parent Is Null "
        '    End If

        '    Dgl1.AgHelpDataSet(Col1LinkedSubcode) = AgL.FillData(mQry, AgL.GCn)
        'Else
        '    mQry = "SELECT Sg.Code, Sg.Name, Ag.GroupName
        '        FROM viewHelpSubGroup Sg                 
        '        Left Join AcGroup Ag On Sg.GroupCode = Ag.GroupCode                 
        '        Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond

        '    'If FGetSettings(SettingFields.FilterInclude_AcTreeNodeType, SettingType.General).ToString.ToUpper.Contains(TreeNodeType.Leaf.ToUpper) Then
        '    '    mQry += " And Sg.Parent Is Not Null "
        '    'ElseIf FGetSettings(SettingFields.FilterInclude_AcTreeNodeType, SettingType.General).ToString.ToUpper.Contains(TreeNodeType.Root.ToUpper) Then
        '    '    mQry += " And Sg.Parent Is Null "
        '    'End If

        '    Dgl1.AgHelpDataSet(Col1Subcode) = AgL.FillData(mQry, AgL.GCn)
        'End If
    End Sub

    Private Sub FrmLedgerHead_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        'FGetPrint(ClsMain.PrintFor.DocumentPrint)
        FGetPrint(SearchCode, ClsMain.PrintFor.DocumentPrint)
    End Sub

    'Private Sub FGetPrint(mPrintFor As ClsMain.PrintFor)
    '    Dim dsMain As DataTable
    '    Dim dsCompany As DataTable
    '    Dim mPrintTitle As String
    '    Dim PrintingCopies() As String
    '    Dim I As Integer, J As Integer
    '    Dim mPrintThisDocId As String
    '    Dim dtReferenceDocID As DataTable

    '    If mPrintFor = ClsMain.PrintFor.EMail Then
    '        PrintingCopies = ("").Split(",")
    '    Else
    '        PrintingCopies = FGetSettings(SettingFields.PrintingCopyCaptions, SettingType.General).ToString.Split(",")
    '    End If



    '    mQry = "SELECT L.DocId FROM LedgerHeadDetail L  WHERE ReferenceDocID ='" & mSearchCode & "' GROUP BY L.DocID "
    '    dtReferenceDocID = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
    '    If dtReferenceDocID.Rows.Count <= 0 Then
    '        mQry = "Select '" & mSearchCode & "' as DocID"
    '        dtReferenceDocID = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
    '    End If

    '    mPrintTitle = TxtV_Type.Text

    '    mQry = ""
    '    For J = 0 To dtReferenceDocID.Rows.Count - 1
    '        mPrintThisDocId = dtReferenceDocID.Rows(J)("DocID")
    '        For I = 1 To PrintingCopies.Length
    '            If mQry <> "" Then mQry = mQry + " Union All "
    '            mQry = mQry + "
    '            Select '" & I & "' as Copies, '" & AgL.XNull(PrintingCopies(I - 1)) & "' as CopyPrintingCaption, H.DocID, L.Sr, H.V_Date, VT.Description as Voucher_Type, VT.NCat, H.Div_Code || H.Site_Code || '-' || H.V_Type || '-' || H.ManualRefNo as VoucherNo,  
    '            H.PartyName, H.PartyAddress, C.CityName as CityName, State.ManualCode as StateCode, State.Description as StateName, 
    '            H.PartyMobile, Sg.ContactPerson, H.PartySalesTaxNo, (Select RegistrationNo From SubgroupRegistration Where RegistrationType='" & SubgroupRegistrationType.AadharNo & "' And Subcode=H.Subcode) as PartyAadharNo,
    '            SL.DispName as AccountNameLine, L.Specification as LineSpecification, SL.HSN,
    '            L.SalesTaxGroupItem, STGI.GrossTaxRate, abs(L.Qty) as Qty, L.Rate, L.Unit, U.DecimalPlaces as UnitDecimalPlaces,  
    '            abs(L.Amount) as Amount,Abs(LC.Taxable_Amount) as Taxable_Amount,Abs(Lc.Tax1_Per) as Tax1_Per, abs(Lc.Tax1) as Tax1, abs(Lc.Tax2_Per) as Tax2_Per, abs(Lc.Tax2) as Tax2, abs(Lc.Tax3_Per) as Tax3_Per, abs(Lc.Tax3) as Tax3, abs(Lc.Tax4_Per) as Tax4_Per, abs(Lc.Tax4) as Tax4, abs(Lc.Tax5_Per) as Tax5_Per, abs(Lc.Tax5) as Tax5, abs(Lc.Net_Amount) as Net_Amount, L.Remarks LRemarks, H.Remarks as HRemarks,
    '            abs(Hc.Gross_Amount) as H_Gross_Amount, Abs(Hc.Taxable_Amount) as H_Taxable_Amount,Abs(Hc.Tax1_Per) as H_Tax1_Per, Abs(Hc.Tax1) as H_Tax1, 
    '            Hc.Tax2_Per as H_Tax2_Per, abs(Hc.Tax2) as H_Tax2, Hc.Tax3_Per as H_Tax3_Per, abs(Hc.Tax3) as H_Tax3, Hc.Tax4_Per as H_Tax4_Per, abs(Hc.Tax4) as H_Tax4, 
    '            Hc.Tax5_Per as H_Tax5_Per, abs(Hc.Tax5) as H_Tax5, Hc.Deduction_Per as H_Deduction_Per, Hc.Deduction as H_Deduction, Hc.Other_Charge_Per as H_Other_Charge_Per, Hc.Other_Charge as H_Other_Charge, Hc.Round_Off, abs(Hc.Net_Amount) as H_Net_Amount, '" & AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_BankAccountDetail")) & "' as Default_BankAccountDetail,
    '            (Select IfNull(Sum(AmtDr),0) - IfNull(Sum(AmtCr),0) As CurrBal From Ledger Where SubCode = SL.Subcode) as Current_Balance, '" & AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsCenterAlignedCompanyInfoOnDocuments")) & "' as IsCenterAlignedCompanyInfoOnDocuments,
    '            '" & AgL.PubUserName & "' as PrintedByUser, '" & mPrintTitle & "' as PrintTitle
    '            from (Select * From LedgerHead Where DocID = '" & mPrintThisDocId & "') as H                
    '            Left Join LedgerHeadDetail L On H.DocID = L.DocID
    '            Left JOIN LedgerHeadCharges HC ON H.DocID = HC.DocId
    '            Left JOIN LedgerHeadDetailCharges LC ON L.DocID = LC.DocId AND L.Sr = LC.Sr
    '            Left Join Unit U On L.Unit = U.Code           
    '            Left Join City C On H.PartyCity = C.CityCode                   
    '            Left Join Subgroup SL On L.Subcode = SL.Subcode
    '            Left Join City CL On SL.CityCode = CL.CityCode
    '            Left Join State On C.State = State.Code                                
    '            Left Join PostingGroupSalesTaxItem STGI On L.SalesTaxGroupItem = STGI.Description
    '            Left Join Subgroup Sg On H.Subcode = Sg.Subcode
    '            Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type                
    '            "
    '        Next
    '    Next
    '    mQry = mQry + " Order By Copies, H.DocID, L.Sr "

    '    dsMain = AgL.FillData(mQry, AgL.GCn).Tables(0)





    '    'FReplaceInvoiceVariables(dsMain, TxtDivision.Tag, TxtSite_Code.Tag)

    '    dsCompany = ClsMain.GetDocumentHeaderDataTable(TxtDivision.Tag, TxtSite_Code.Tag, TxtV_Type.Tag)

    '    Dim objRepPrint As Object
    '    If mPrintFor = ClsMain.PrintFor.EMail Then
    '        'objRepPrint = New FrmMailCompose(AgL)
    '        'objRepPrint.TxtToEmail.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Email
    '        '        From StockHeadDetail H 
    '        '        LEFT JOIN SubGroup Sg On H.Subcode = Sg.SubCode
    '        '        Where H.DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
    '        'objRepPrint.TxtCcEmail.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Email
    '        '        From SaleInvoice H 
    '        '        LEFT JOIN SubGroup Sg On H.Agent = Sg.SubCode
    '        '        Where H.DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
    '        'objRepPrint.AttachmentName = "Invoice"
    '    Else
    '        objRepPrint = New FrmRepPrint(AgL)
    '    End If

    '    objRepPrint.reportViewer1.Visible = True
    '    Dim id As Integer = 0
    '    objRepPrint.reportViewer1.ProcessingMode = ProcessingMode.Local
    '    If AgL.PubUserName.ToUpper = "SUPER" Then
    '        dsMain = ClsMain.RemoveNullFromDataTable(dsMain)
    '        dsCompany = ClsMain.RemoveNullFromDataTable(dsCompany)
    '        dsMain.WriteXml(AgL.PubReportPath + "\VoucherEntry_DsMain.xml")
    '        dsCompany.WriteXml(AgL.PubReportPath + "\VoucherEntry_DsCompany.xml")
    '    End If


    '    objRepPrint.reportViewer1.LocalReport.ReportPath = AgL.PubReportPath + "\VoucherEntry.rdl"


    '    If (dsMain.Rows.Count = 0) Then
    '        MsgBox("No records found to print.")
    '    End If
    '    Dim rds As New ReportDataSource("DsMain", dsMain)
    '    Dim rdsCompany As New ReportDataSource("DsCompany", dsCompany)

    '    objRepPrint.reportViewer1.LocalReport.DataSources.Clear()
    '    objRepPrint.reportViewer1.LocalReport.DataSources.Add(rds)
    '    objRepPrint.reportViewer1.LocalReport.DataSources.Add(rdsCompany)


    '    objRepPrint.reportViewer1.LocalReport.Refresh()
    '    objRepPrint.reportViewer1.RefreshReport()
    '    objRepPrint.MdiParent = Me.MdiParent
    '    objRepPrint.Show()



    'End Sub

    Public Sub FGetPrint(ByVal SearchCode As String, mPrintFor As ClsMain.PrintFor, Optional ByVal IsPrintToPrinter As Boolean = False, Optional BulkCondStr As String = "")
        FGetPrintCrystal(SearchCode, mPrintFor, IsPrintToPrinter, BulkCondStr)
    End Sub

    Sub FGetPrintCrystal(ByVal SearchCode As String, mPrintFor As ClsMain.PrintFor, Optional ByVal IsPrintToPrinter As Boolean = False, Optional BulkCondStr As String = "")
        Dim mPrintTitle As String
        Dim PrintingCopies() As String
        Dim I As Integer, J As Integer



        mPrintTitle = AgL.Dman_Execute("Select IfNull(PrintingDescription, Description) From Voucher_Type Where V_Type = '" & TxtV_Type.Tag & "' ", AgL.GCn).ExecuteScalar()

        Dim mDocNoCaption As String = FGetSettings(SettingFields.DocumentPrintEntryNoCaption, SettingType.General)
        Dim mDocDateCaption As String = FGetSettings(SettingFields.DocumentPrintEntryDateCaption, SettingType.General)
        Dim mDocReportFileName As String = FGetSettings(SettingFields.DocumentPrintReportFileName, SettingType.General)


        Dim bPrimaryQry As String = ""
        If BulkCondStr <> "" Then
            bPrimaryQry = " Select * From LedgerHead  With (NoLock) Where DocID In (" & BulkCondStr & ")"
            PrintingCopies = FGetSettings(SettingFields.PrintingBulkCopyCaptions, SettingType.General).ToString.Split(",")
        Else
            bPrimaryQry = " Select * From LedgerHead  With (NoLock) Where DocID = '" & SearchCode & "'"
            PrintingCopies = FGetSettings(SettingFields.PrintingCopyCaptions, SettingType.General).ToString.Split(",")
        End If


        mQry = ""
        For I = 1 To PrintingCopies.Length
            If mQry <> "" Then mQry = mQry + " Union All "
            mQry = mQry + "
                Select '" & I & "' as Copies, '" & AgL.XNull(PrintingCopies(I - 1)) & "' as CopyPrintingCaption, '" & mDocNoCaption & "' as DocNoCaption, '" & mDocDateCaption & "' as DocDateCaption, SiteState.ManualCode as SiteStateCode, SiteState.Description as SiteStateName, H.DocID, L.Sr, H.V_Date, VT.Description as Voucher_Type, VT.NCat,                                 
                '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo  as InvoiceNo,                 
                BP.Subcode as PartySubcode, 
                (Case When SI.DocID Is Not Null Then SI.SaleToPartyName Else BP.DispName End) as PartyName, 
                (Case When SI.DocID Is Not Null then IfNull(SI.SaleToPartyAddress,'') Else IfNull(BP.Address,'') End) as PartyAddress, 
                (Case When SI.DocID Is Not Null then SIC.CityName Else IfNull(C.CityName,'') End) as CityName, 
                (Case When SI.DocID Is Not Null then IfNull(SI.SaleToPartyPinCode,'') Else IfNull(BP.Pin,'') End) as PartyPincode, 
                (Case When SI.DocID Is Not Null then IfNull(SICS.ManualCode,'') Else IfNull(State.ManualCode,'') End) as StateCode, 
                (Case When SI.DocID Is Not Null then IfNull(SICS.Description,'') Else IfNull(State.Description,'') End) as StateName, 
                (Case When SI.DocID Is Not Null then IfNull(SI.SaleToPartyMobile,'') Else IfNull(BP.Mobile,'') End) as PartyMobile, 
                BP.ContactPerson, 
                IfNull((SELECT RegistrationNo FROM SubgroupRegistration WHERE RegistrationType ='Sales Tax No' AND Subcode = (Case When RC.RowCnt <=1 Then L.Subcode else '' End)),'') as PartySalesTaxNo, 
                IfNull((SELECT RegistrationNo FROM SubgroupRegistration WHERE RegistrationType ='AADHAR NO' AND Subcode = (Case When RC.RowCnt <=1 Then L.Subcode else '' End)),'')  as PartyAadharNo, 
                IfNull((SELECT RegistrationNo FROM SubgroupRegistration WHERE RegistrationType ='PAN No' AND Subcode = (Case When RC.RowCnt <=1 Then L.Subcode else '' End)),'')  as PartyPanNo,
                '" & FGetSettings(SettingFields.TermsAndConditions, SettingType.General) & "' TermsAndConditions,       
                IfNull(H.PartyDocNo,IfNull(L.ReferenceNo,'')) as ReferenceNo,
                I.Name as LineAccountName, L.Specification as LineSpecification, IfNull(LRef.V_Date, IfNull(L.EffectiveDate,'')) as EffectiveDate,                
                IfNull(LRef.AmtDr+LRef.AmtCr,abs(L.Amount)) as Amount, L.AmountCr, IfNull(L.ChqRefNo,'') as ChqRefNo, IfNull(L.ChqRefDate,'') as ChqRefDate, IfNull(L.Remarks,'') as LRemarks, IfNull(H.Remarks,'') as HRemarks,                               
                '" & AgL.XNull(AgL.PubDtEnviro.Rows(0)("Default_BankAccountDetail")) & "' as Default_BankAccountDetail,
                '" & FGetSettings(SettingFields.DocumentPrintHeaderPattern, SettingType.General) & "' as DocumentPrintHeaderPattern, 
                '" & AgL.PubUserName & "' as PrintedByUser, H.EntryBy as EntryByUser, '" & mPrintTitle & "' as PrintTitle,
                '" & FGetSettings(SettingFields.DocumentPrintShowPrintDateTimeYn, SettingType.General) & "' as DocumentPrintShowPrintDateTimeYn                
                from (" & bPrimaryQry & ") as H                
                Left Join LedgerHeadDetail L  With (NoLock) On H.DocID = L.DocID
                Left Join LedgerHeadCharges HC On H.DocID = HC.DocID
                Left Join LedgerHeadDetailCharges LC On L.DocID = LC.DocID And L.Sr = LC.Sr
                Left Join Ledger LRef On L.DocID = LRef.ReferenceDocID And L.Sr = LRef.ReferenceDocIDSr And L.Subcode = LRef.Subcode
                Left Join SaleInvoice SI On L.SpecificationDocId = SI.DocId
                Left Join City SIC On SI.SaleToPartyCity = SIC.CityCode
                Left Join State SICS On SIC.State = SICS.Code
                Left Join (Select srL.DocID, Count(srL.DocId) as RowCnt From LedgerHeadDetail srL Where srL.DocID in ('" & IIf(BulkCondStr = "", SearchCode, BulkCondStr) & "') Group By srL.DocId) as RC On H.DocId = RC.DocId
                Left Join viewHelpSubgroup I  With (NoLock) On (Case When RC.RowCnt <=1 Then H.Subcode else L.Subcode End) = I.Code
                Left Join Subgroup BP With (NoLock) On (Case When RC.RowCnt <=1 Then L.Subcode else H.Subcode End) = BP.Subcode                                
                Left Join City C  With (NoLock) On BP.CityCode = C.CityCode
                Left Join State  With (NoLock) On C.State = State.Code                                
                Left Join PostingGroupSalesTaxItem STGI  With (NoLock) On L.SalesTaxGroupItem = STGI.Description                                
                Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type
                Left Join DocumentPrefix DP On VT.Category = DP.Category And H.Div_Code = DP.Div_Code                
                Left Join SiteMast Site On H.Site_Code = Site.Code
                Left Join City SiteCity On Site.City_Code = SiteCity.CityCode
                Left Join State SiteState On SiteCity.State = SiteState.Code
                "
        Next
        mQry = mQry + " Order By Copies, H.DocID, L.Sr "


        Dim objRepPrint As Object
        If mPrintFor = ClsMain.PrintFor.EMail Then
            objRepPrint = New AgLibrary.FrmMailComposeWithCrystal(AgL)
            'objRepPrint.TxtToEmail.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Email
            '        From SaleInvoice H  With (NoLock)
            '        LEFT JOIN SubGroup Sg  With (NoLock) On H.Party = Sg.SubCode
            '        Where H.DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
            'objRepPrint.TxtCcEmail.Text = AgL.XNull(AgL.Dman_Execute("Select Sg.Email
            '        From SaleInvoice H  With (NoLock)
            '        LEFT JOIN SubGroup Sg  With (NoLock) On H.Agent = Sg.SubCode
            '        Where H.DocId = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar())
            FGetMailConfiguration(objRepPrint, SearchCode)
            'objRepPrint.AttachmentName = "Invoice"
        Else
            objRepPrint = New AgLibrary.RepView(AgL)
        End If


        'If mDocReportFileName = "" Then
        ClsMain.FPrintThisDocument(Me, objRepPrint, TxtV_Type.Tag, mQry, "JournalVoucher_Print.rpt", mPrintTitle, , , , "", TxtV_Date.Text, IsPrintToPrinter)
        'Else
        'ClsMain.FPrintThisDocument(Me, objRepPrint, TxtV_Type.Tag, mQry, mDocReportFileName, mPrintTitle, , , , TxtPartyName.Tag, TxtV_Date.Text, IsPrintToPrinter)
        'End If
    End Sub
    Private Sub FGetMailConfiguration(objRepPrint As Object, SearchCode As String)
        Dim DtMailData As DataTable = AgL.FillData("Select Sg.DispName As DivisionName, 
                    Party.DispName As PartyName, Party.EMail As PartyEMail,
                    Agent.DispName As AgentName, Agent.EMail As AgentEMail
                    From SaleInvoice H 
                    LEFT JOIN Division D On H.Div_Code = D.Div_Code
                    LEFT JOIN SubGroup Sg On D.SubCode = Sg.SubCode
                    LEFT JOIN SubGroup Party On H.SaleToParty = Party.SubCode
                    LEFT JOIN SubGroup Agent On H.Agent = Agent.SubCode
                    Where H.DocId = '" & SearchCode & "'", AgL.GCn).Tables(0)

        objRepPrint.TxtToEmail.Text = FGetSettings(SettingFields.MailTo, SettingType.General)
        objRepPrint.TxtToEmail.Text = objRepPrint.TxtToEmail.Text.Replace("<PartyEMail>", AgL.XNull(DtMailData.Rows(0)("PartyEMail"))).
                Replace("<AgentEMail>", AgL.XNull(DtMailData.Rows(0)("AgentEMail")))

        objRepPrint.TxtCcEmail.Text = FGetSettings(SettingFields.MailCc, SettingType.General)
        objRepPrint.TxtCcEmail.Text = objRepPrint.TxtCcEmail.Text.Replace("<PartyEMail>", AgL.XNull(DtMailData.Rows(0)("PartyEMail"))).
                Replace("<AgentEMail>", AgL.XNull(DtMailData.Rows(0)("AgentEMail")))

        objRepPrint.TxtSubject.Text =
        objRepPrint.TxtSubject.Text = objRepPrint.TxtSubject.Text.Replace("<PartyName>", AgL.XNull(DtMailData.Rows(0)("PartyName"))).
                Replace("<EntryNo>", TxtReferenceNo.Text).Replace("<EntryDate>", TxtV_Date.Text).
                Replace("<DivisionName>", AgL.XNull(DtMailData.Rows(0)("DivisionName"))).
                Replace("<AgentName>", AgL.XNull(DtMailData.Rows(0)("AgentName")))

        objRepPrint.TxtMessage.Text = FGetSettings(SettingFields.MailMessage, SettingType.General)
        objRepPrint.TxtMessage.Text = objRepPrint.TxtMessage.Text.Replace("<PartyName>", AgL.XNull(DtMailData.Rows(0)("PartyName"))).
                Replace("<EntryNo>", TxtReferenceNo.Text).Replace("<EntryDate>", TxtV_Date.Text).
                Replace("<DivisionName>", AgL.XNull(DtMailData.Rows(0)("DivisionName"))).
                Replace("<AgentName>", AgL.XNull(DtMailData.Rows(0)("AgentName")))
        objRepPrint.AttachmentName = TxtReferenceNo.Text
    End Sub

    Private Sub Dgl1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.RowEnter
        FShowCurrBal(e.RowIndex)
    End Sub
    Private Sub MnuImport_Click(sender As Object, e As EventArgs) Handles MnuImportFromExcel.Click, MnuImportFromDos.Click, MnuImportFromTally.Click, MnuEditSave.Click, MnuCancelEntry.Click, MnuImportGSTDataFromExcel.Click
        Select Case sender.name
            Case MnuEditSave.Name
                FEditSaveAllEntries()

            Case MnuImportFromDos.Name
                'Dim objMdi As New MDIMain
                'Dim DTUP As DataTable
                'Dim StrUserPermission As String = AgIniVar.FunGetUserPermission(ClsMain.ModuleName, objMdi.MnuItemMaster.Name, objMdi.MnuItemMaster.Text, DTUP)

                'Dim FrmObj As New FrmVoucherEntry(StrUserPermission, DTUP, "")
                'FrmObj.FImportFromExcel(ImportFor.Dos)
                FImportOpeningFromExcel(ImportFor.Dos)
        End Select
    End Sub

    Private Sub FEditSaveAllEntries()
        mFlag_Import = True
        For I As Integer = 0 To DTMaster.Rows.Count - 1
            BMBMaster.Position = I
            MoveRec()
            Topctrl1.FButtonClick(1)
            Calculation()
            Topctrl1.FButtonClick(13)
        Next
        mFlag_Import = False
    End Sub
    Public Sub FImportOpeningFromExcel(bImportFor As ImportFor)
        Dim mQry As String = ""
        Dim bHeadSubCodeName As String = ""
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtLedger As DataTable
        Dim DtLedger_DataFields As DataTable
        Dim DtMain As DataTable = Nothing

        Dim I As Integer
        Dim J As Integer
        Dim K As Integer
        Dim M As Integer
        Dim N As Integer
        Dim StrErrLog As String = ""

        mQry = "Select '' as Srl, '" & GetFieldAliasName(bImportFor, "V_TYPE") & "' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "V_NO") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "V_Date") & "' as [Field Name], 'Date' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Ledger Account Name") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Contra Ledger Account Name") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Narration") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Chq No") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Chq Date") & "' as [Field Name], 'Date' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Amt Dr") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Amt Cr") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        DtLedger_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)


        Dim ObjFrmImport As Object
        If bImportFor = ImportFor.Dos Then
            ObjFrmImport = New FrmImportFromExcel
            ObjFrmImport.Dgl1.DataSource = DtLedger_DataFields
        Else
            ObjFrmImport = New FrmImportFromExcel
            ObjFrmImport.Dgl1.DataSource = DtLedger_DataFields
        End If

        ObjFrmImport.Text = "Voucher Entry Import"
        ObjFrmImport.StartPosition = FormStartPosition.CenterScreen
        ObjFrmImport.ShowDialog()

        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

        If bImportFor = ImportFor.Dos Then
            DtLedger = ObjFrmImport.P_DsExcelData.Tables(0)
        Else
            DtLedger = ObjFrmImport.P_DsExcelData.Tables(0)
        End If

        mFlag_Import = True

        ''''''''''''Getting Duplicate Party Records''''''''''''''''
        mQry = "SELECT SG.Name As PartyName, Count(*) AS Cnt
                    FROM Subgroup SG 
                    GROUP BY SG.Name
                    HAVING Count(*) > 1 "
        Dim DtDuplicateParties As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim bDuplicatePartiesStr As String = ""
        Dim bDuplicatePartiesExistInOpeningFileStr As String = ""
        For I = 0 To DtDuplicateParties.Rows.Count - 1
            If bDuplicatePartiesStr <> "" Then bDuplicatePartiesStr += ","
            bDuplicatePartiesStr += AgL.Chk_Text(DtDuplicateParties.Rows(I)("PartyName"))

            If DtLedger.Select("ledgername = " & AgL.Chk_Text(DtDuplicateParties.Rows(I)("PartyName")) & "").Length > 0 Then
                If Not bDuplicatePartiesExistInOpeningFileStr.Contains(AgL.XNull(DtDuplicateParties.Rows(I)("PartyName"))) Then
                    If bDuplicatePartiesExistInOpeningFileStr <> "" Then bDuplicatePartiesExistInOpeningFileStr += ","
                    bDuplicatePartiesExistInOpeningFileStr += AgL.XNull(DtDuplicateParties.Rows(I)("PartyName"))
                End If
            End If
        Next
        If bDuplicatePartiesStr = "" Then bDuplicatePartiesStr = "''"

        Dim DtLedger_Original As DataTable = DtLedger
        If bImportFor = ImportFor.Dos Then
            ''''''''''''''For Filtering Data To Import In This Entry'''''''''''''''''''''''''''''''''''
            Dim DtLedger_Filtered As New DataTable
            DtLedger_Filtered = DtLedger.Clone
            Dim DtLedgerRows_Filtered As DataRow() = DtLedger.Select("ledgername Not In (" & bDuplicatePartiesStr & ")")
            For I = 0 To DtLedgerRows_Filtered.Length - 1
                DtLedger_Filtered.ImportRow(DtLedgerRows_Filtered(I))
            Next
            DtLedger = DtLedger_Filtered
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            For I = 0 To DtLedger.Rows.Count - 1
                If DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name")).ToString().Trim() = "CASH A/C." Then
                    DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name")) = "CASH A/C"
                End If
            Next
        End If





        Dim DtLedgerAccount = DtLedger.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Ledger Account Name"))
        For I = 0 To DtLedgerAccount.Rows.Count - 1
            If AgL.XNull(DtLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString().Trim <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From SubGroup where LTRIM(RTRIM(Name)) = " & AgL.Chk_Text(AgL.XNull(DtLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString().Trim()) & "", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Ledger Accounts Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Ledger Accounts Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name"))) & ", "
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

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"


            Dim VoucherEntryTableList(0) As FrmVoucherEntry.StructLedgerHead
            Dim VoucherEntryTable As New FrmVoucherEntry.StructLedgerHead

            VoucherEntryTable.DocID = ""
            VoucherEntryTable.V_Type = "OB"
            VoucherEntryTable.V_Prefix = ""
            VoucherEntryTable.V_Date = "31/Mar/2019"
            VoucherEntryTable.V_No = 1
            VoucherEntryTable.Div_Code = AgL.PubDivCode
            VoucherEntryTable.Site_Code = AgL.PubSiteCode
            VoucherEntryTable.ManualRefNo = 1
            VoucherEntryTable.Subcode = ""
            VoucherEntryTable.SubcodeName = ""


            If VoucherEntryTable.V_Type = "JV" Or VoucherEntryTable.V_Type = "OB" Then
                mFlag_Import = False
            Else
                mFlag_Import = True
            End If



            VoucherEntryTable.UptoDate = ""
            VoucherEntryTable.Remarks = ""
            VoucherEntryTable.Status = "Active"
            VoucherEntryTable.SalesTaxGroupParty = ""
            VoucherEntryTable.PlaceOfSupply = ""
            VoucherEntryTable.PartySalesTaxNo = ""
            VoucherEntryTable.StructureCode = ""
            VoucherEntryTable.CustomFields = ""
            VoucherEntryTable.PartyDocNo = ""
            VoucherEntryTable.PartyDocDate = ""
            VoucherEntryTable.EntryBy = AgL.PubUserName
            VoucherEntryTable.EntryDate = AgL.GetDateTime(AgL.GcnRead)
            VoucherEntryTable.ApproveBy = ""
            VoucherEntryTable.ApproveDate = ""
            VoucherEntryTable.MoveToLog = ""
            VoucherEntryTable.MoveToLogDate = ""
            VoucherEntryTable.UploadDate = ""

            VoucherEntryTable.Gross_Amount = 0
            VoucherEntryTable.Taxable_Amount = 0
            VoucherEntryTable.Tax1_Per = 0
            VoucherEntryTable.Tax1 = 0
            VoucherEntryTable.Tax2_Per = 0
            VoucherEntryTable.Tax2 = 0
            VoucherEntryTable.Tax3_Per = 0
            VoucherEntryTable.Tax3 = 0
            VoucherEntryTable.Tax4_Per = 0
            VoucherEntryTable.Tax4 = 0
            VoucherEntryTable.Tax5_Per = 0
            VoucherEntryTable.Tax5 = 0
            VoucherEntryTable.SubTotal1 = 0
            VoucherEntryTable.Deduction_Per = 0
            VoucherEntryTable.Deduction = 0
            VoucherEntryTable.Other_Charge_Per = 0
            VoucherEntryTable.Other_Charge = 0
            VoucherEntryTable.Round_Off = 0
            VoucherEntryTable.Net_Amount = 0


            For J = 0 To DtLedger.Rows.Count - 1
                VoucherEntryTable.Line_Sr = J + 1
                VoucherEntryTable.Line_SubCode = ""
                VoucherEntryTable.Line_SubCodeName = AgL.XNull(DtLedger.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim
                VoucherEntryTable.Line_SpecificationDocID = ""
                VoucherEntryTable.Line_SpecificationDocIDSr = ""
                VoucherEntryTable.Line_Specification = ""
                VoucherEntryTable.Line_SalesTaxGroupItem = ""
                VoucherEntryTable.Line_Qty = 0
                VoucherEntryTable.Line_Unit = ""
                VoucherEntryTable.Line_Rate = 0
                VoucherEntryTable.Line_Amount = AgL.VNull(DtLedger.Rows(J)(GetFieldAliasName(bImportFor, "Amt Dr")))
                VoucherEntryTable.Line_Amount_Cr = AgL.VNull(DtLedger.Rows(J)(GetFieldAliasName(bImportFor, "Amt Cr")))
                VoucherEntryTable.Line_ChqRefNo = ""
                VoucherEntryTable.Line_ChqRefDate = ""
                VoucherEntryTable.Line_ReferenceNo = AgL.XNull(DtLedger.Rows(J)("v_no"))
                VoucherEntryTable.Line_ReferenceDate = AgL.XNull(DtLedger.Rows(J)("date"))
                VoucherEntryTable.Line_Remarks = ""
                VoucherEntryTable.Line_Gross_Amount = 0
                VoucherEntryTable.Line_Taxable_Amount = 0
                VoucherEntryTable.Line_Tax1_Per = 0
                VoucherEntryTable.Line_Tax1 = 0
                VoucherEntryTable.Line_Tax2_Per = 0
                VoucherEntryTable.Line_Tax2 = 0
                VoucherEntryTable.Line_Tax3_Per = 0
                VoucherEntryTable.Line_Tax3 = 0
                VoucherEntryTable.Line_Tax4_Per = 0
                VoucherEntryTable.Line_Tax4 = 0
                VoucherEntryTable.Line_Tax5_Per = 0
                VoucherEntryTable.Line_Tax5 = 0
                VoucherEntryTable.Line_SubTotal1 = 0
                VoucherEntryTable.Line_Deduction_Per = 0
                VoucherEntryTable.Line_Deduction = 0
                VoucherEntryTable.Line_Other_Charge_Per = 0
                VoucherEntryTable.Line_Other_Charge = 0
                VoucherEntryTable.Line_Round_Off = 0
                VoucherEntryTable.Line_Net_Amount = 0

                VoucherEntryTableList(UBound(VoucherEntryTableList)) = VoucherEntryTable
                ReDim Preserve VoucherEntryTableList(UBound(VoucherEntryTableList) + 1)
            Next
            FrmVoucherEntry.InsertLedgerHead(VoucherEntryTableList)

            AgL.ETrans.Commit()
            mTrans = "Commit"


            mFlag_Import = False


            If bDuplicatePartiesExistInOpeningFileStr <> "" Then
                bDuplicatePartiesExistInOpeningFileStr += vbCrLf
                bDuplicatePartiesExistInOpeningFileStr += " These Parties are duplicate.Please Enter Opening Values Manually."
                If File.Exists(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt") Then
                    My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt", bDuplicatePartiesExistInOpeningFileStr, False)
                Else
                    File.Create(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt")
                    My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt", bDuplicatePartiesExistInOpeningFileStr, False)
                End If
                System.Diagnostics.Process.Start("notepad.exe", My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt")
            End If
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
            mFlag_Import = False
        End Try
        If StrErrLog <> "" Then MsgBox(StrErrLog)
    End Sub
    Private Function GetFieldAliasName(bImportFor As ImportFor, bFieldName As String)
        Dim bAliasName As String = bFieldName
        If bImportFor = ImportFor.Dos Then
            Select Case bFieldName
                Case "V_TYPE"
                    bAliasName = "V_TYPE"
                Case "V_NO"
                    bAliasName = "V_NO"
                Case "V_Date"
                    bAliasName = "V_DATE"
                Case "Ledger Account Name"
                    bAliasName = "ledgername"
                Case "Contra Ledger Account Name"
                    bAliasName = "contraname"
                Case "Narration"
                    bAliasName = "narration"
                Case "Chq No"
                    bAliasName = "chq_no"
                Case "Chq Date"
                    bAliasName = "chq_date"
                Case "Amt Dr"
                    bAliasName = "dr"
                Case "Amt Cr"
                    bAliasName = "cr"




                Case "Party Name"
                    bAliasName = "vendor"
                Case "Line Ledger Account Name"
                    bAliasName = "item_name"
                Case "Entry No"
                    bAliasName = "V_No"
                Case "SubTotal1"
                    bAliasName = "SUBTOTAL1"
                Case "Deduction_Per"
                    bAliasName = "DED_PER"
                Case "Deduction"
                    bAliasName = "DEDUCTION"
                Case "Other_Charge_Per"
                    bAliasName = "OT_CH_PER"
                Case "Other_Charge"
                    bAliasName = "OT_CHARGE"
                Case "Round_Off"
                    bAliasName = "ROUND_OFF"
                Case "Net_Amount"
                    bAliasName = "NET_AMOUNT"
                Case "Gross_Amount"
                    bAliasName = "GROSS_AMT"
                Case "Taxable_Amount"
                    bAliasName = "TAXABLEAMT"
                Case "Tax1_Per"
                    bAliasName = "TAX1_PER"
                Case "Tax1"
                    bAliasName = "TAX1"
                Case "Tax2_Per"
                    bAliasName = "TAX2_PER"
                Case "Tax2"
                    bAliasName = "TAX2"
                Case "Tax3_Per"
                    bAliasName = "TAX3_PER"
                Case "Tax3"
                    bAliasName = "TAX3"
                Case "Tax4_Per"
                    bAliasName = "TAX4_PER"
                Case "Tax4"
                    bAliasName = "TAX4"
                Case "Tax5_Per"
                    bAliasName = "TAX5_PER"
                Case "Tax5"
                    bAliasName = "TAX5"
            End Select

            Return bAliasName
        Else
            Return bFieldName
        End If
    End Function

    Private Sub Dgl1_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles Dgl1.CellBeginEdit
        If Topctrl1.Mode.ToUpper = "BROWSE" Then
            e.Cancel = True
        End If
    End Sub

    Private Sub FrmJournalEntry_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        mQry = " SELECT Count(*) AS Cnt FROM TransactionReferences T 
                    WHERE T.ReferenceDocId = '" & mSearchCode & "' 
                    AND IfNull(T.IsEditingAllowed,0) = 0 "
        If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar() > 0 Then
            MsgBox("Some Refrential Entries Exist For This Entry.Can't Modify.", MsgBoxStyle.Information)
            Passed = False
            Exit Sub
        End If

        If ClsMain.IsEntryLockedWithLockText("LedgerHead", "DocId", mSearchCode) = True Then
            Passed = False
            Exit Sub
        End If
        FGetSettingVariableValuesForAddAndEdit()
    End Sub

    Private Sub FrmJournalEntry_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        mQry = " SELECT Count(*) AS Cnt FROM TransactionReferences T 
                    WHERE T.ReferenceDocId = '" & mSearchCode & "' 
                    AND IfNull(T.IsEditingAllowed,0) = 0 "
        If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar() > 0 Then
            MsgBox("Some Refrential Entries Exist For This Entry.Can't Modify.", MsgBoxStyle.Information)
            Passed = False
            Exit Sub
        End If

        If ClsMain.IsEntryLockedWithLockText("LedgerHead", "DocId", mSearchCode) = True Then
            Passed = False
            Exit Sub
        End If
    End Sub

    Private Sub FrmJournalEntry_BaseEvent_Save_PostTrans(SearchCode As String) Handles Me.BaseEvent_Save_PostTrans
        If AgL.StrCmp(Topctrl1.Mode, "Add") Then
            If FGetSettings(SettingFields.ActionToPrintOnAdd, SettingType.General) = ActionToPrint.AskAndPrintOnScreen Then
                If MsgBox("Do you want to print ? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    FGetPrint(SearchCode, ClsMain.PrintFor.DocumentPrint)
                End If
            ElseIf FGetSettings(SettingFields.ActionToPrintOnAdd, SettingType.General) = ActionToPrint.AskAndPrintToPrinter Then
                If MsgBox("Do you want to print ? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    FGetPrint(SearchCode, ClsMain.PrintFor.DocumentPrint, True)
                End If
            ElseIf FGetSettings(SettingFields.ActionToPrintOnAdd, SettingType.General) = ActionToPrint.PrintOnScreen Then
                FGetPrint(SearchCode, ClsMain.PrintFor.DocumentPrint)
            ElseIf FGetSettings(SettingFields.ActionToPrintOnAdd, SettingType.General) = ActionToPrint.PrintToPrinter Then
                FGetPrint(SearchCode, ClsMain.PrintFor.DocumentPrint, True)
            End If
        End If

    End Sub
    Private Sub FGetSettingVariableValuesForAddAndEdit()
        SettingFields_CopyRemarkInNextLineYn = CType(AgL.VNull(FGetSettings(SettingFields.CopyRemarkInNextLineYn, SettingType.General)), Boolean)
    End Sub
End Class
