Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain
Imports Customised.ClsMain.ConfigurableFields

Public Class FrmCatalog
    Inherits AgTemplate.TempMaster

    Dim mQry$

    Public Const ColSNo As String = "SNo"
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1ItemType As String = "Item Type"
    Public Const Col1SKU As String = "SKU"
    Public Const Col1ItemCategory As String = "Item Category"
    Public Const Col1ItemGroup As String = "Item Group"
    Public Const Col1Item As String = "Item"
    Public Const Col1Dimension1 As String = "Dimension 1"
    Public Const Col1Dimension2 As String = "Dimension 2"
    Public Const Col1Dimension3 As String = "Dimension 3"
    Public Const Col1Dimension4 As String = "Dimension 4"
    Public Const Col1Size As String = "Size"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1Rate As String = "Rate"
    Public Const Col1DiscountPer As String = "Disc. %"
    Public Const Col1AdditionalDiscountPer As String = "Add. Disc. %"
    Public Const Col1AdditionPer As String = "Addition %"
    Public Const Col1ItemState As String = "Item State"


    Public Const Col1MItemCategory As String = "M Item Category"
    Public Const Col1MItemGroup As String = "M Item Group"
    Public Const Col1MItemSpecification As String = "M Item Specification"
    Public Const Col1MDimension1 As String = "M Dimension 1"
    Public Const Col1MDimension2 As String = "M Dimension 2"
    Public Const Col1MDimension3 As String = "M Dimension 3"
    Public Const Col1MDimension4 As String = "M Dimension 4"
    Public Const Col1MSize As String = "M Size"

    Public WithEvents DglMain As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1HeadOriginal As String = "Head Original"

    Dim rowSite_Code As Integer = 0
    Dim rowSpecification As Integer = 1
    Dim rowDescription As Integer = 2
    Dim rowSiteShortName As Integer = 3


    Public Const hcSite_Code As String = "Site"
    Public Const hcSpecification As String = "Specification"
    Public Const hcDescription As String = "Description"
    Public Const hcSiteShortName As String = "Site Short Name"

    Dim DtItemTypeSetting As DataTable
    Dim mItemTypeLastValue As String
    Public WithEvents LblAmount As Label
    Public WithEvents Label2 As Label
    Friend WithEvents MnuOptions As ContextMenuStrip
    Private components As IContainer
    Friend WithEvents MnuCopyRecord As ToolStripMenuItem
    Friend WithEvents MnuPasteRecord As ToolStripMenuItem
    Public Const CatalogV_Type As String = "Catalog"
    Friend WithEvents MnuPrintCustomerCopy As ToolStripMenuItem
    Dim gCopiedSearchCode As String = ""

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.LblIsSystemDefine = New System.Windows.Forms.Label()
        Me.PnlRateType = New System.Windows.Forms.Panel()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.PnlTotals = New System.Windows.Forms.Panel()
        Me.LblAmount = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LblTotalQty = New System.Windows.Forms.Label()
        Me.LblTotalQtyText = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.MnuOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuCopyRecord = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPasteRecord = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPrintCustomerCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.GrpUP.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlTotals.SuspendLayout()
        Me.MnuOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(961, 41)
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 558)
        Me.GroupBox1.Size = New System.Drawing.Size(1003, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(14, 562)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(200, 623)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(228, 562)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(401, 562)
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
        Me.GroupBox2.Location = New System.Drawing.Point(704, 562)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(465, 562)
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
        Me.LblIsSystemDefine.Location = New System.Drawing.Point(827, 536)
        Me.LblIsSystemDefine.Name = "LblIsSystemDefine"
        Me.LblIsSystemDefine.Size = New System.Drawing.Size(96, 15)
        Me.LblIsSystemDefine.TabIndex = 1061
        Me.LblIsSystemDefine.Text = "IsSystemDefine"
        '
        'PnlRateType
        '
        Me.PnlRateType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlRateType.Location = New System.Drawing.Point(2, 156)
        Me.PnlRateType.Name = "PnlRateType"
        Me.PnlRateType.Size = New System.Drawing.Size(959, 378)
        Me.PnlRateType.TabIndex = 2
        '
        'Pnl1
        '
        Me.Pnl1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl1.Location = New System.Drawing.Point(2, 43)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(959, 89)
        Me.Pnl1.TabIndex = 1
        '
        'PnlTotals
        '
        Me.PnlTotals.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlTotals.BackColor = System.Drawing.Color.Cornsilk
        Me.PnlTotals.Controls.Add(Me.LblAmount)
        Me.PnlTotals.Controls.Add(Me.Label2)
        Me.PnlTotals.Controls.Add(Me.LblTotalQty)
        Me.PnlTotals.Controls.Add(Me.LblTotalQtyText)
        Me.PnlTotals.Location = New System.Drawing.Point(0, 534)
        Me.PnlTotals.Name = "PnlTotals"
        Me.PnlTotals.Size = New System.Drawing.Size(961, 23)
        Me.PnlTotals.TabIndex = 1062
        '
        'LblAmount
        '
        Me.LblAmount.AutoSize = True
        Me.LblAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAmount.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblAmount.Location = New System.Drawing.Point(727, 3)
        Me.LblAmount.Name = "LblAmount"
        Me.LblAmount.Size = New System.Drawing.Size(12, 16)
        Me.LblAmount.TabIndex = 662
        Me.LblAmount.Text = "."
        Me.LblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Maroon
        Me.Label2.Location = New System.Drawing.Point(626, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 16)
        Me.Label2.TabIndex = 661
        Me.Label2.Text = "Total Amount :"
        '
        'LblTotalQty
        '
        Me.LblTotalQty.AutoSize = True
        Me.LblTotalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalQty.Location = New System.Drawing.Point(332, 3)
        Me.LblTotalQty.Name = "LblTotalQty"
        Me.LblTotalQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalQty.TabIndex = 660
        Me.LblTotalQty.Text = "."
        Me.LblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalQtyText
        '
        Me.LblTotalQtyText.AutoSize = True
        Me.LblTotalQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalQtyText.Location = New System.Drawing.Point(247, 3)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(72, 16)
        Me.LblTotalQtyText.TabIndex = 659
        Me.LblTotalQtyText.Text = "Total Qty :"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(-1, 134)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(147, 21)
        Me.LinkLabel1.TabIndex = 1063
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Catalog Detail"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'MnuOptions
        '
        Me.MnuOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuCopyRecord, Me.MnuPasteRecord, Me.MnuPrintCustomerCopy})
        Me.MnuOptions.Name = "MnuOptions"
        Me.MnuOptions.Size = New System.Drawing.Size(186, 92)
        '
        'MnuCopyRecord
        '
        Me.MnuCopyRecord.Name = "MnuCopyRecord"
        Me.MnuCopyRecord.Size = New System.Drawing.Size(185, 22)
        Me.MnuCopyRecord.Text = "Copy Record"
        '
        'MnuPasteRecord
        '
        Me.MnuPasteRecord.Name = "MnuPasteRecord"
        Me.MnuPasteRecord.Size = New System.Drawing.Size(185, 22)
        Me.MnuPasteRecord.Text = "Paste Record"
        '
        'MnuPrintCustomerCopy
        '
        Me.MnuPrintCustomerCopy.Name = "MnuPrintCustomerCopy"
        Me.MnuPrintCustomerCopy.Size = New System.Drawing.Size(185, 22)
        Me.MnuPrintCustomerCopy.Text = "Print Customer Copy"
        '
        'FrmCatalog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(961, 606)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.PnlTotals)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.PnlRateType)
        Me.Controls.Add(Me.LblIsSystemDefine)
        Me.MaximizeBox = True
        Me.Name = "FrmCatalog"
        Me.Text = "Quality Master"
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.LblIsSystemDefine, 0)
        Me.Controls.SetChildIndex(Me.PnlRateType, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.PnlTotals, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
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
        Me.PnlTotals.ResumeLayout(False)
        Me.PnlTotals.PerformLayout()
        Me.MnuOptions.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents LblIsSystemDefine As System.Windows.Forms.Label
    Public WithEvents PnlRateType As Panel
    Friend WithEvents Pnl1 As Panel
    Public WithEvents PnlTotals As Panel
    Public WithEvents LblTotalQty As Label
    Public WithEvents LblTotalQtyText As Label
    Public WithEvents LinkLabel1 As LinkLabel
#End Region

    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, AgL.PubSiteCode, "", "", CatalogV_Type, "", "")
        FGetSettings = mValue
    End Function
    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer


        For I = 0 To DglMain.RowCount - 1
            If DglMain(Col1Mandatory, I).Value <> "" And DglMain.Rows(I).Visible Then
                If DglMain(Col1Value, I).Value.ToString = "" Then
                    MsgBox(DglMain(Col1Head, I).Value.ToString & " can not be blank.")
                    DglMain.CurrentCell = DglMain(Col1Value, I)
                    DglMain.Focus()
                    passed = False : Exit Sub
                End If
            End If
        Next

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From Catalog Where Description='" & DglMain.Item(Col1Value, rowDescription).Value & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        Else
            mQry = "Select count(*) From Catalog Where Description='" & DglMain.Item(Col1Value, rowDescription).Value & "' And Code<>'" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If


        For I = 0 To DglMain.Rows.Count - 1
            If DglMain.Item(Col1Value, I).Value = Nothing Then DglMain.Item(Col1Value, I).Value = ""
            If DglMain.Item(Col1Value, I).Tag = Nothing Then DglMain.Item(Col1Value, I).Tag = ""
        Next


        For I = 0 To Dgl1.Rows.Count - 1
            If AgL.XNull(Dgl1.Item(Col1ItemCategory, I).Value) <> "" _
                        Or AgL.XNull(Dgl1.Item(Col1ItemGroup, I).Value) <> "" _
                        Or AgL.XNull(Dgl1.Item(Col1Item, I).Value) <> "" _
                        Or AgL.XNull(Dgl1.Item(Col1Dimension1, I).Value) <> "" _
                        Or AgL.XNull(Dgl1.Item(Col1Dimension2, I).Value) <> "" _
                        Or AgL.XNull(Dgl1.Item(Col1Dimension3, I).Value) <> "" _
                        Or AgL.XNull(Dgl1.Item(Col1Dimension4, I).Value) <> "" _
                        Or AgL.XNull(Dgl1.Item(Col1Size, I).Value) <> "" _
                       Then
                Dgl1.Item(Col1SKU, I).Tag = ClsMain.FGetSKUCode(Dgl1.Item(ColSNo, I).Value, ItemTypeCode.InternalProduct, Dgl1.Item(Col1ItemCategory, I).Tag, Dgl1.Item(Col1ItemCategory, I).Value _
                                   , Dgl1.Item(Col1ItemGroup, I).Tag, Dgl1.Item(Col1ItemGroup, I).Value _
                                   , Dgl1.Item(Col1Item, I).Tag, Dgl1.Item(Col1Item, I).Value _
                                   , Dgl1.Item(Col1Dimension1, I).Tag, Dgl1.Item(Col1Dimension1, I).Value _
                                   , Dgl1.Item(Col1Dimension2, I).Tag, Dgl1.Item(Col1Dimension2, I).Value _
                                   , Dgl1.Item(Col1Dimension3, I).Tag, Dgl1.Item(Col1Dimension3, I).Value _
                                   , Dgl1.Item(Col1Dimension4, I).Tag, Dgl1.Item(Col1Dimension4, I).Value _
                                   , Dgl1.Item(Col1Size, I).Tag, Dgl1.Item(Col1Size, I).Value _
                                   , Dgl1.Item(Col1MItemCategory, I).Value _
                                   , Dgl1.Item(Col1MItemGroup, I).Value _
                                   , Dgl1.Item(Col1MItemSpecification, I).Value _
                                   , Dgl1.Item(Col1MDimension1, I).Value _
                                   , Dgl1.Item(Col1MDimension2, I).Value _
                                   , Dgl1.Item(Col1MDimension3, I).Value _
                                   , Dgl1.Item(Col1MDimension4, I).Value _
                                   , Dgl1.Item(Col1MSize, I).Value
                                   )
                If Dgl1.Item(Col1SKU, I).Tag = "" Then
                    passed = False
                    Exit Sub
                End If
            End If


            For J As Integer = 0 To Dgl1.Rows.Count - 1
                If I <> J Then
                    If AgL.XNull(Dgl1.Item(Col1SKU, I).Tag) <> "" And AgL.XNull(Dgl1.Item(Col1SKU, J).Tag) <> "" Then
                        If AgL.StrCmp(Dgl1.Item(Col1SKU, I).Tag, Dgl1.Item(Col1SKU, J).Tag) Then
                            MsgBox("Item " & Dgl1.Item(Col1Item, I).Value & " Is Feeded At Row No " & Dgl1.Item(ColSNo, I).Value & " And " & Dgl1.Item(ColSNo, J).Value & ".", MsgBoxStyle.Information)
                            passed = False
                            Exit Sub
                        End If
                    End If
                End If
            Next
        Next
    End Sub
    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        AgL.PubFindQry = "SELECT H.Code, H.Description as Name
                            FROM Catalog H "
        AgL.PubFindQryOrdBy = "[Description]"
    End Sub
    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Catalog"
        MainLineTableCsv = "CatalogDetail"
    End Sub
    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer
        Dim mSr As Integer = 0

        mQry = "UPDATE Catalog 
                Set 
                Site_Code =" & AgL.Chk_Text(DglMain.Item(Col1Value, rowSite_Code).Tag) & ",
                Specification =" & AgL.Chk_Text(DglMain.Item(Col1Value, rowSpecification).Value) & ",
                Description =" & AgL.Chk_Text(DglMain.Item(Col1Value, rowDescription).Value) & "
                Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "DELETE FROM CatalogDetailSku WHERE Code  = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "DELETE FROM CatalogDetail WHERE Code  = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Item, I).Value <> "" And Val(Dgl1.Item(Col1Qty, I).Value) > 0 Then
                mSr += 1
                mQry = "INSERT INTO CatalogDetail (Code, Sr, Item, Qty, Unit, Rate, DiscountPer, AdditionalDiscountPer, AdditionPer, ItemState)
                        VALUES ('" & SearchCode & "', " & mSr & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1SKU, I).Tag) & ",                                                 
                        " & Val(Dgl1.Item(Col1Qty, I).Value) & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ",
                        " & Val(Dgl1.Item(Col1Rate, I).Value) & ",
                        " & Val(Dgl1.Item(Col1DiscountPer, I).Value) & ", 
                        " & Val(Dgl1.Item(Col1AdditionalDiscountPer, I).Value) & ", 
                        " & Val(Dgl1.Item(Col1AdditionPer, I).Value) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1ItemState, I).Tag) & "
                        ) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = "Insert Into CatalogDetailSku(Code, Sr, ItemCategory, ItemGroup, Item, 
                        Dimension1, Dimension2, Dimension3, Dimension4, Size) "
                mQry += " Values(" & AgL.Chk_Text(mSearchCode) & ", " & mSr & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, I).Tag) & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, I).Tag) & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, I).Tag) & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1Dimension4, I).Tag) & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1Size, I).Tag) & ")"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next
    End Sub
    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList

    End Sub
    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        MoveRecHeader(SearchCode)
        MoveRecLine(SearchCode)

        FrmItemBOM_BaseFunction_DispText()
    End Sub


    Private Sub MoveRecHeader(ByVal SearchCode As String)
        Dim DsTemp As DataSet
        mQry = "SELECT Sm.Name As Site_Name, H.*, SM.ShortName
                FROM Catalog H
                LEFT JOIN SiteMast Sm On H.Site_Code = Sm.Code
                WHERE H.Code ='" & SearchCode & "' "
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                DglMain.Item(Col1Value, rowSite_Code).Tag = AgL.XNull(.Rows(0)("Site_Code"))
                DglMain.Item(Col1Value, rowSite_Code).Value = AgL.XNull(.Rows(0)("Site_Name"))
                DglMain.Item(Col1Value, rowSpecification).Value = AgL.XNull(.Rows(0)("Specification"))
                DglMain.Item(Col1Value, rowDescription).Value = AgL.XNull(.Rows(0)("Description"))
                DglMain.Item(Col1Value, rowSiteShortName).Value = AgL.XNull(.Rows(0)("ShortName"))
                ApplyUISetting()
            End If
        End With

    End Sub

    Private Sub MoveRecLine(ByVal SearchCode As String)
        Dim DsTemp As DataSet
        Dim I As Integer
        mQry = "SELECT H.*, 
                Sku.Code As SkuCode, Sku.Description As SkuDescription, 
                Sku.BaseItem, Sku.ItemCategory, Sku.ItemGroup, SKU.Dimension1, SKU.Dimension2, Sku.Dimension3, Sku.Dimension4, Sku.Size, 
                It.Code As ItemType, It.Name As ItemTypeDesc,
                IC.Description as ItemCategoryName, IG.Description as ItemGroupName,
                D1.Description as Dimension1Name,D2.Description as Dimension2Name,
                D3.Description as Dimension3Name,D4.Description as Dimension4Name,
                Size.Description as SizeName, I.Code as ItemCode, I.Description as ItemName,
                ItemState.Description As ItemStateName,                
                I.ItemCategory as MItemCategory, I.ItemGroup as MItemGroup, I.Specification as MItemSpecification, 
                I.Dimension1 as MDimension1,  I.Dimension2 as MDimension2,  I.Dimension3 as MDimension3,  I.Dimension4 as MDimension4,  I.Size as MSize
                FROM CatalogDetail H
                LEFT JOIN Item Sku ON Sku.Code = H.Item 
                LEFT JOIN Item I ON I.Code = IfNull(Sku.BaseItem,Sku.Code) 
                LEFT JOIN ItemType It On Sku.ItemType = It.Code
                Left Join Item IC On Sku.ItemCategory = IC.Code
                Left Join Item IG On Sku.ItemGroup = IG.Code
                LEFT JOIN Item D1 ON D1.Code = Sku.Dimension1  
                LEFT JOIN Item D2 ON D2.Code = Sku.Dimension2
                LEFT JOIN Item D3 ON D3.Code = Sku.Dimension3
                LEFT JOIN Item D4 ON D4.Code = Sku.Dimension4
                LEFT JOIN Item Size ON Size.Code = Sku.Size
                LEFT JOIN Item ItemState On H.ItemState = ItemState.Code
                WHERE H.Code ='" & SearchCode & "'
                ORDER BY H.Sr "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                    Dgl1.Item(Col1SKU, I).Tag = AgL.XNull(.Rows(I)("SkuCode"))
                    Dgl1.Item(Col1SKU, I).Value = AgL.XNull(.Rows(I)("SkuDescription"))
                    Dgl1.Item(Col1ItemType, I).Tag = AgL.XNull(.Rows(I)("ItemType"))
                    Dgl1.Item(Col1ItemType, I).Value = AgL.XNull(.Rows(I)("ItemTypeDesc"))
                    Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("ItemCode"))
                    Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemName"))
                    Dgl1.Item(Col1ItemCategory, I).Tag = AgL.XNull(.Rows(I)("ItemCategory"))
                    Dgl1.Item(Col1ItemCategory, I).Value = AgL.XNull(.Rows(I)("ItemCategoryName"))
                    Dgl1.Item(Col1ItemGroup, I).Tag = AgL.XNull(.Rows(I)("ItemGroup"))
                    Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupName"))
                    Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                    Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Name"))
                    Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                    Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Name"))
                    Dgl1.Item(Col1Dimension3, I).Tag = AgL.XNull(.Rows(I)("Dimension3"))
                    Dgl1.Item(Col1Dimension3, I).Value = AgL.XNull(.Rows(I)("Dimension3Name"))
                    Dgl1.Item(Col1Dimension4, I).Tag = AgL.XNull(.Rows(I)("Dimension4"))
                    Dgl1.Item(Col1Dimension4, I).Value = AgL.XNull(.Rows(I)("Dimension4Name"))
                    Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                    Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.00")
                    Dgl1.Item(Col1Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")
                    Dgl1.Item(Col1DiscountPer, I).Value = Format(AgL.VNull(.Rows(I)("DiscountPer")), "0.00")
                    Dgl1.Item(Col1AdditionalDiscountPer, I).Value = Format(AgL.VNull(.Rows(I)("AdditionalDiscountPer")), "0.00")
                    Dgl1.Item(Col1AdditionPer, I).Value = Format(AgL.VNull(.Rows(I)("AdditionPer")), "0.00")
                    Dgl1.Item(Col1ItemState, I).Tag = AgL.XNull(.Rows(I)("ItemState"))
                    Dgl1.Item(Col1ItemState, I).Value = AgL.XNull(.Rows(I)("ItemStateName"))



                    Dgl1.Item(Col1MItemCategory, I).Tag = AgL.XNull(.Rows(I)("MItemCategory"))
                    Dgl1.Item(Col1MItemGroup, I).Tag = AgL.XNull(.Rows(I)("MItemGroup"))
                    Dgl1.Item(Col1MItemSpecification, I).Value = AgL.XNull(.Rows(I)("MItemSpecification"))
                    Dgl1.Item(Col1MDimension1, I).Tag = AgL.XNull(.Rows(I)("MDimension1"))
                    Dgl1.Item(Col1MDimension2, I).Tag = AgL.XNull(.Rows(I)("MDimension2"))
                    Dgl1.Item(Col1MDimension3, I).Tag = AgL.XNull(.Rows(I)("MDimension3"))
                    Dgl1.Item(Col1MDimension4, I).Tag = AgL.XNull(.Rows(I)("MDimension4"))
                    Dgl1.Item(Col1MSize, I).Tag = AgL.XNull(.Rows(I)("MSize"))

                    LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                Next I
                Dgl1.Visible = True
            Else
                Dgl1.Visible = False
            End If
        End With


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
        mQry = "SELECT I.Code AS SearchCode FROM Catalog I  
                Order By I.Code "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub
    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub
    Private Sub FrmItemBOM_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 325, 885)
    End Sub
    Private Sub TxtItemCategory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
        End If
    End Sub
    Private Sub Dgl2_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    Validating_ItemCode(Dgl1.Item(mColumnIndex, mRowIndex).Tag, mColumnIndex, mRowIndex)
            End Select
            FGeterateSkuName(mRowIndex)
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Validating_ItemCode(ItemCode As String, ByVal mColumn As Integer, ByVal mRow As Integer)
        Dim DtItem As DataTable = Nothing
        Try
            mQry = "Select I.Code, I.Description, I.ManualCode, I.Unit, I.Specification, I.ItemType
                    , I.ItemCategory, IC.Description as ItemCategoryName
                    , I.ItemGroup, IG.Description as ItemGroupName
                    , I.Dimension1, D1.Description as Dimension1Name
                    , I.Dimension2, D2.Description as Dimension2Name
                    , I.Dimension3, D3.Description as Dimension3Name
                    , I.Dimension4, D4.Description as Dimension4Name
                    , I.Size, Size.Description as SizeName 
                    From Item I  With (NoLock)
                    Left Join Item IC With (NoLock) On I.ItemCategory = IC.Code
                    Left Join Item IG With (NoLock) On I.ItemGroup = IG.Code
                    Left Join Item D1 With (NoLock) On I.Dimension1 = D1.Code
                    Left Join Item D2 With (NoLock) On I.Dimension2 = D2.Code
                    Left Join Item D3 With (NoLock) On I.Dimension3 = D3.Code
                    Left Join Item D4 With (NoLock) On I.Dimension4 = D1.Code
                    Left Join Item Size With (NoLock) On I.Size = Size.Code
                    Where I.Code ='" & ItemCode & "'"
            DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtItem.Rows.Count > 0 Then
                Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DtItem.Rows(0)("Unit"))
                Dgl1.Item(Col1Unit, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Unit"))

                Dgl1.Item(Col1ItemCategory, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemCategory"))
                Dgl1.Item(Col1ItemCategory, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemCategoryName"))

                If AgL.XNull(DtItem.Rows(0)("ItemGroup")) <> "" Then
                    Dgl1.Item(Col1ItemGroup, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemGroup"))
                    Dgl1.Item(Col1ItemGroup, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemGroupName"))
                End If

                Dgl1.Item(Col1MItemSpecification, mRow).Value = AgL.XNull(DtItem.Rows(0)("Specification"))
                If AgL.XNull(DtItem.Rows(0)("Dimension1")) <> "" Then
                    Dgl1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension1"))
                    Dgl1.Item(Col1Dimension1, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension1Name"))
                End If
                If AgL.XNull(DtItem.Rows(0)("Dimension2")) <> "" Then
                    Dgl1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension2"))
                    Dgl1.Item(Col1Dimension2, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension2Name"))
                End If
                If AgL.XNull(DtItem.Rows(0)("Dimension3")) <> "" Then
                    Dgl1.Item(Col1Dimension3, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension3"))
                    Dgl1.Item(Col1Dimension3, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension3Name"))
                End If
                If AgL.XNull(DtItem.Rows(0)("Dimension4")) <> "" Then
                    Dgl1.Item(Col1Dimension4, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension4"))
                    Dgl1.Item(Col1Dimension4, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension4Name"))
                End If
                If AgL.XNull(DtItem.Rows(0)("Size")) <> "" Then
                    Dgl1.Item(Col1Size, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Size"))
                    Dgl1.Item(Col1Size, mRow).Value = AgL.XNull(DtItem.Rows(0)("SizeName"))
                End If




                Dgl1.Item(Col1MItemCategory, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemCategory"))
                Dgl1.Item(Col1MItemCategory, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemCategoryName"))
                Dgl1.Item(Col1MItemGroup, mRow).Tag = AgL.XNull(DtItem.Rows(0)("ItemGroup"))
                Dgl1.Item(Col1MItemGroup, mRow).Value = AgL.XNull(DtItem.Rows(0)("ItemGroupName"))
                Dgl1.Item(Col1MItemSpecification, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Specification"))
                Dgl1.Item(Col1MDimension1, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension1"))
                Dgl1.Item(Col1MDimension1, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension1Name"))
                Dgl1.Item(Col1MDimension2, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension2"))
                Dgl1.Item(Col1MDimension2, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension2Name"))
                Dgl1.Item(Col1MDimension3, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension3"))
                Dgl1.Item(Col1MDimension3, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension3Name"))
                Dgl1.Item(Col1MDimension4, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Dimension4"))
                Dgl1.Item(Col1MDimension4, mRow).Value = AgL.XNull(DtItem.Rows(0)("Dimension4Name"))
                Dgl1.Item(Col1MSize, mRow).Tag = AgL.XNull(DtItem.Rows(0)("Size"))
                Dgl1.Item(Col1MSize, mRow).Value = AgL.XNull(DtItem.Rows(0)("SizeName"))
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub
    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        ApplyUISetting()
        Try
            Dgl1.CurrentCell = Dgl1.Item(Col1Item, Dgl1.Rows.Count - 1)
            Dgl1.Focus()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub FrmItemBOM_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        ApplyUISetting()
        If DglMain.Rows(rowSite_Code).Visible Then DglMain.CurrentCell = DglMain(Col1Value, rowSite_Code)
        DglMain.Focus()
    End Sub
    Private Sub FrmItemBOM_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dim I As Integer
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemType, 100, 0, AgL.PubCaptionItemType, False, False)
            .AddAgTextColumn(Dgl1, Col1ItemCategory, 180, 0, Col1ItemCategory, True, False, False)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 180, 0, Col1ItemGroup, True, False, False)
            .AddAgTextColumn(Dgl1, Col1Item, 300, 0, Col1Item, True, False, False)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 150, 0, AgL.PubCaptionDimension1, True, False, False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 150, 0, AgL.PubCaptionDimension2, True, False, False)
            .AddAgTextColumn(Dgl1, Col1Dimension3, 150, 0, AgL.PubCaptionDimension3, True, False, False)
            .AddAgTextColumn(Dgl1, Col1Dimension4, 150, 0, AgL.PubCaptionDimension4, True, False, False)
            .AddAgTextColumn(Dgl1, Col1Size, 120, 0, Col1Size, True, False, False)
            .AddAgTextColumn(Dgl1, Col1SKU, 300, 0, Col1SKU, True, False, False)
            .AddAgNumberColumn(Dgl1, Col1Qty, 70, 4, 2, False, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True, False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 80, 4, 2, False, Col1Rate, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1DiscountPer, 100, 2, 2, False, Col1DiscountPer, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1AdditionalDiscountPer, 100, 2, 2, False, Col1AdditionalDiscountPer, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1AdditionPer, 100, 2, 3, False, Col1AdditionPer, True, False, True)
            .AddAgTextColumn(Dgl1, Col1ItemState, 120, 0, Col1ItemState, True, False, False)

            .AddAgTextColumn(Dgl1, Col1MItemCategory, 100, 0, Col1MItemCategory, True, False, False)
            .AddAgTextColumn(Dgl1, Col1MItemGroup, 100, 0, Col1MItemGroup, True, False, False)
            .AddAgTextColumn(Dgl1, Col1MItemSpecification, 100, 0, Col1MItemSpecification, True, False, False)
            .AddAgTextColumn(Dgl1, Col1MDimension1, 100, 0, "M " & AgL.PubCaptionDimension1, True, False, False)
            .AddAgTextColumn(Dgl1, Col1MDimension2, 100, 0, "M " & AgL.PubCaptionDimension2, True, False, False)
            .AddAgTextColumn(Dgl1, Col1MDimension3, 100, 0, "M " & AgL.PubCaptionDimension3, True, False, False)
            .AddAgTextColumn(Dgl1, Col1MDimension4, 100, 0, "M " & AgL.PubCaptionDimension4, True, False, False)
            .AddAgTextColumn(Dgl1, Col1MSize, 100, 0, Col1MSize, True, False, False)
        End With
        AgL.AddAgDataGrid(Dgl1, PnlRateType)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.RowHeadersVisible = False
        Dgl1.BackgroundColor = Me.BackColor
        Dgl1.Name = "Dgl1"
        AgL.GridDesign(Dgl1)
        Dgl1.Anchor = AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom


        DglMain.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DglMain, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(DglMain, Col1Head, 200, 255, Col1Head, True, True)
            .AddAgTextColumn(DglMain, Col1HeadOriginal, 180, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(DglMain, Col1Mandatory, 12, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(DglMain, Col1Value, 580, 255, Col1Value, True, False)
        End With
        AgL.AddAgDataGrid(DglMain, Pnl1)
        DglMain.Columns(Col1Mandatory).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings 2", 5.25, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        DglMain.Columns(Col1Mandatory).DefaultCellStyle.ForeColor = Color.Red
        DglMain.EnableHeadersVisualStyles = False
        DglMain.ColumnHeadersHeight = 35
        DglMain.AgSkipReadOnlyColumns = True
        DglMain.AllowUserToAddRows = False
        DglMain.RowHeadersVisible = False
        DglMain.ColumnHeadersVisible = False
        AgL.GridDesign(DglMain)
        DglMain.BackgroundColor = Me.BackColor
        DglMain.Name = "DglMain"
        DglMain.Anchor = AnchorStyles.Top + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom


        DglMain.Rows.Add(4)
        'For I = 0 To Dgl1.Rows.Count - 1
        '    Dgl1.Rows(I).Visible = False
        'Next

        DglMain.Item(Col1Head, rowSite_Code).Value = hcSite_Code
        DglMain.Item(Col1Head, rowSpecification).Value = hcSpecification
        DglMain.Item(Col1Head, rowDescription).Value = hcDescription
        DglMain.Item(Col1Head, rowSiteShortName).Value = hcSiteShortName

        For I = 0 To DglMain.Rows.Count - 1
            DglMain(Col1HeadOriginal, I).Value = DglMain(Col1Head, I).Value
        Next
    End Sub
    Private Sub DglRateType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub

        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If

        If e.Control Or e.Shift Or e.Alt Then Exit Sub

        If e.KeyCode = Keys.Enter Then
            'If Dgl2.CurrentCell.ColumnIndex = Dgl2.Columns(Col1Margin).Index Then
            '    If Dgl2.Item(Dgl2.CurrentCell.ColumnIndex, Dgl2.CurrentCell.RowIndex).Value Is Nothing Then Dgl2.Item(Dgl2.CurrentCell.ColumnIndex, Dgl2.CurrentCell.RowIndex).Value = ""
            '    If Dgl2.Item(Dgl2.CurrentCell.ColumnIndex, Dgl2.CurrentCell.RowIndex).Value = "" Then
            '        If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, "Save") = MsgBoxResult.Yes Then
            '            Topctrl1.FButtonClick(13)
            '        End If
            '    End If
            'End If
        End If
    End Sub
    Private Sub FrmItemBOM_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        If DtItemTypeSetting Is Nothing Then Exit Sub
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DglMain.CellEnter
        Try
            If DglMain.CurrentCell Is Nothing Then Exit Sub
            If Topctrl1.Mode = "BROWSE" Then
                DglMain.CurrentCell.ReadOnly = True
            End If

            If DglMain.CurrentCell.ColumnIndex <> DglMain.Columns(Col1Value).Index Then Exit Sub


            DglMain.AgHelpDataSet(DglMain.CurrentCell.ColumnIndex) = Nothing
            CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0
            CType(DglMain.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = False

            Select Case DglMain.CurrentCell.RowIndex
                Case rowDescription
                    DglMain.Item(Col1Value, DglMain.CurrentCell.RowIndex).ReadOnly = True
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DglMain.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = DglMain.CurrentCell.RowIndex
            bColumnIndex = DglMain.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> DglMain.Columns(Col1Value).Index Then Exit Sub

            Select Case DglMain.CurrentCell.RowIndex
                Case rowSite_Code
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Code, Name FROM SiteMast "
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowSpecification
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "Select I.Specification As Code, I.Specification As Name 
                                    From Catalog I 
                                    Order By I.Specification "
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                            CType(DglMain.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True
                        End If
                    End If

                Case rowDescription
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "Select I.Description As Code, I.Description As Name 
                                    From Catalog I 
                                    Order By I.Description "
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                            CType(DglMain.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FCreateHelpItem(mRowIndex As Integer)
        Dim strCond As String = ""

        If FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) & "') > 0 OR I.ItemType Is Null) "
            ElseIf FGetSettings(SettingFields.FilterInclude_Process, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) & "') <= 0 OR I.ItemType Is Null) "
            End If
        End If

        If FGetSettings(SettingFields.FilterInclude_ItemV_Type, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_ItemV_Type, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || I.V_Type,'" & FGetSettings(SettingFields.FilterInclude_ItemV_Type, SettingType.General) & "') > 0 OR I.V_Type Is Null) "
            ElseIf FGetSettings(SettingFields.FilterInclude_Process, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || I.V_Type,'" & FGetSettings(SettingFields.FilterInclude_ItemV_Type, SettingType.General) & "') <= 0 OR I.V_Type Is Null) "
            End If
        Else
            strCond += " And I.V_Type = '" & ItemV_Type.Item & "' "
        End If


        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherDivisions")) Then
            strCond += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(I.ShowItemInOtherDivisions,0) =1 Or I.Div_Code Is Null) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherSites")) Then
            strCond += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(I.ShowItemInOtherSites,0) =1 Or I.Site_Code Is Null) "
        End If


        'If Dgl2.Item(Col1Value, rowItemType).Value <> "" And Dgl2.Columns(col1ItemType).Visible Then
        '    strCond += " And (I.ItemType = '" & Dgl1.Item(Col1Value, rowItemType).Tag & "' or I.ItemType Is Null) "
        'End If

        If Dgl1.Item(Col1ItemCategory, mRowIndex).Value <> "" And Dgl1.Columns(Col1ItemCategory).Visible Then
            strCond += " And (I.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, mRowIndex).Tag & "' or I.ItemCategory Is Null) "
        End If

        If Dgl1.Item(Col1ItemGroup, mRowIndex).Value <> "" And Dgl1.Columns(Col1ItemGroup).Visible Then
            strCond += " And (I.ItemGroup = '" & Dgl1.Item(Col1ItemGroup, mRowIndex).Tag & "' or I.ItemGroup Is Null) "
        End If

        mQry = " Select I.Code, I.Description, IT.Name as ItemType 
                                     From Item I With (Nolock)
                                     Left Join ItemType IT With (Nolock) On I.ItemType = IT.Code
                                     Where IfNull(I.Status,'Active') = 'Active' " & strCond & "
                                     Order By I.Description"
        Dgl1.AgHelpDataSet(Col1Item) = AgL.FillData(mQry, AgL.GCn)
    End Sub
    Private Sub FCreateHelpDimension4(mRowIndex As Integer)
        Dim strCond As String = ""

        If FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) <> "" Then
            If FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General).ToString.Substring(0, 1) = "+" Then
                strCond += " And (CharIndex('+' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) & "') > 0 OR I.ItemType Is Null) "
            ElseIf FGetSettings(SettingFields.FilterInclude_Process, SettingType.General).ToString.Substring(0, 1) = "-" Then
                strCond += " And (CharIndex('-' || IT.Parent,'" & FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General) & "') <= 0 OR I.ItemType Is Null) "
            End If
        End If



        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherDivisions")) Then
            strCond += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(I.ShowItemInOtherDivisions,0) =1 Or I.Div_Code Is Null) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherSites")) Then
            strCond += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(I.ShowItemInOtherSites,0) =1 Or I.Site_Code Is Null) "
        End If


        'If Dgl2.Item(Col1ItemType, mRowIndex).Value <> "" And Dgl2.Columns(Col1ItemType).Visible Then
        '    strCond += " And (I.ItemType = '" & Dgl2.Item(Col1ItemType, mRowIndex).Tag & "' or I.ItemType Is Null) "
        'End If

        If Dgl1.Item(Col1ItemCategory, mRowIndex).Value <> "" And Dgl1.Columns(Col1ItemCategory).Visible Then
            strCond += " And (I.ItemCategory = '" & Dgl1.Item(Col1ItemCategory, mRowIndex).Tag & "' or I.ItemCategory Is Null) "
        End If

        If Dgl1.Item(Col1ItemGroup, mRowIndex).Value <> "" And Dgl1.Columns(Col1ItemGroup).Visible Then
            strCond += " And (I.ItemGroup = '" & Dgl1.Item(Col1ItemGroup, mRowIndex).Tag & "' or I.ItemGroup Is Null) "
        End If


        mQry = " Select I.Code, I.Description, IT.Name as ItemType 
                                     From Item I With (Nolock)
                                     Left Join ItemType IT With (Nolock) On I.ItemType = IT.Code
                                     Where IfNull(I.Status,'Active') = 'Active' And I.V_Type = '" & ItemV_Type.Dimension4 & "' " & strCond & "
                                     Order By I.Description"
        Dgl1.AgHelpDataSet(Col1Dimension4) = AgL.FillData(mQry, AgL.GCn)
    End Sub
    Private Sub DGLRateType_EditingControl_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If Topctrl1.Mode = "Browse" Then Exit Sub


            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1ItemCategory
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            mQry = "SELECT Code, Description FROM Item Where V_Type='" & ItemV_Type.ItemCategory & "'"
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1ItemGroup
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            mQry = "SELECT Code, Description FROM Item Where V_Type='" & ItemV_Type.ItemGroup & "'"
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1Item
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            FCreateHelpItem(bRowIndex)
                        End If
                    End If
                Case Col1Dimension1
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            FCreateHelpDimension4(bRowIndex)
                        End If
                    End If
                Case Col1Dimension2
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            mQry = "SELECT I.Code, I.Description FROM Item I Where I.V_Type = '" & ItemV_Type.Dimension2 & "' Order By I.Description"
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
                Case Col1Dimension3
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            mQry = "SELECT I.Code, I.Description FROM Item I Where I.V_Type = '" & ItemV_Type.Dimension3 & "' Order By I.Description"
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1Dimension4
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            mQry = "SELECT I.Code, I.Description FROM Item I Where I.V_Type = '" & ItemV_Type.Dimension4 & "' Order By I.Description"
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
                Case Col1Size
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            mQry = "SELECT I.Code, I.Description FROM Item I Where I.V_Type = '" & ItemV_Type.SIZE & "' Order By I.Description"
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
                Case Col1Unit
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            mQry = "SELECT Code, Code as Description FROM Unit "
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1ItemState
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            mQry = "SELECT I.Code, I.Description FROM Item I Where I.V_Type = '" & ItemV_Type.ItemState & "' 
                                    And IfNull(I.Status,'Active') = 'Active' 
                                    Order By I.Description "
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles DglMain.EditingControl_Validating
        Dim DtTemp As DataTable
        Dim mRow As Integer
        Dim mColumn As Integer

        Try
            If DglMain.CurrentCell Is Nothing Then Exit Sub

            mRow = DglMain.CurrentCell.RowIndex
            mColumn = DglMain.CurrentCell.ColumnIndex

            If mColumn = DglMain.Columns(Col1Value).Index Then
                If DglMain.Item(Col1Mandatory, mRow).Value <> "" Then
                    If DglMain(Col1Value, mRow).Value = "" Then
                        MsgBox(DglMain(Col1Head, mRow).Value & " can not be blank.")
                        e.Cancel = True
                        Exit Sub
                    End If
                End If
            End If

            Select Case DglMain.CurrentCell.RowIndex
                Case rowSite_Code, rowSpecification
                    DglMain.Item(Col1Value, rowSiteShortName).Value = AgL.XNull(AgL.Dman_Execute("Select Max(ShortName) From SiteMast Where Code = '" & DglMain.Item(Col1Value, rowSite_Code).Tag & "'", AgL.GCn).ExecuteScalar())
                    DglMain.Item(Col1Value, rowDescription).Value = DglMain.Item(Col1Value, rowSiteShortName).Value + " " + DglMain.Item(Col1Value, rowSpecification).Value
            End Select
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmItemBOM_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dim i As Integer

        For i = 0 To DglMain.Rows.Count - 1
            DglMain(Col1Value, i).Value = ""
            DglMain(Col1Value, i).Tag = ""
        Next

        Dgl1.Rows.Clear()
        Dgl1.RowCount = 1

        LblTotalQty.Text = "."

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

    Private Sub Dgl1_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DglMain.EditingControlShowing, Dgl1.EditingControlShowing
        If FGetSettings(SettingFields.DefaultTextCaseInMasters, SettingType.General) = TextCase.Upper Then
            DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Upper
        ElseIf FGetSettings(SettingFields.DefaultTextCaseInMasters, SettingType.General) = TextCase.Lower Then
            DirectCast(e.Control, TextBox).CharacterCasing = CharacterCasing.Lower
        End If
    End Sub
    Private Sub FrmSaleOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer
        If Topctrl1.Mode = "Browse" Then Exit Sub
        LblTotalQty.Text = 0
        LblAmount.Text = 0
        Dim mAmount As Double

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1SKU, I).Value <> "" And Dgl1.Rows(I).Visible Then
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                mAmount = Val(Dgl1.Item(Col1Rate, I).Value) * Val(Dgl1.Item(Col1Qty, I).Value)
                mAmount = Math.Round(mAmount - (mAmount * Val(Dgl1.Item(Col1DiscountPer, I).Value) / 100), 2)
                LblAmount.Text = Val(LblAmount.Text) + mAmount
            End If
        Next
        LblTotalQty.Text = Val(LblTotalQty.Text)
        LblAmount.Text = Val(LblAmount.Text)
    End Sub
    Private Sub FrmCatalog_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        If Dgl1.AgHelpDataSet(Col1ItemCategory) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1ItemCategory).Dispose() : Dgl1.AgHelpDataSet(Col1ItemCategory) = Nothing
        If Dgl1.AgHelpDataSet(Col1ItemGroup) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1ItemGroup).Dispose() : Dgl1.AgHelpDataSet(Col1ItemGroup) = Nothing
        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
        If Dgl1.AgHelpDataSet(Col1Dimension1) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Dimension1).Dispose() : Dgl1.AgHelpDataSet(Col1Dimension1) = Nothing
        If Dgl1.AgHelpDataSet(Col1Dimension2) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Dimension2).Dispose() : Dgl1.AgHelpDataSet(Col1Dimension2) = Nothing
        If Dgl1.AgHelpDataSet(Col1Dimension3) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Dimension3).Dispose() : Dgl1.AgHelpDataSet(Col1Dimension3) = Nothing
        If Dgl1.AgHelpDataSet(Col1Dimension4) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Dimension4).Dispose() : Dgl1.AgHelpDataSet(Col1Dimension4) = Nothing
        If Dgl1.AgHelpDataSet(Col1Size) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Size).Dispose() : Dgl1.AgHelpDataSet(Col1Size) = Nothing

        For I As Integer = 0 To DglMain.Rows.Count - 1
            DglMain(Col1Head, I).Tag = Nothing
        Next
    End Sub
    Private Sub DGL2_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub
    Private Sub ApplyUISetting()
        GetUISetting(DglMain, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", ClsMain.GridTypeConstants.VerticalGrid)
        GetUISetting(Dgl1, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", ClsMain.GridTypeConstants.HorizontalGrid)
    End Sub
    Private Sub FGeterateSkuName(bRowIndex As Integer)
        If CType(AgL.VNull(ClsMain.FGetSettings(SettingFields.SkuManagementApplicableYN, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, Dgl1.Item(
                                                Col1ItemType, bRowIndex).Tag, "", ItemV_Type.SKU, "", "")), Boolean) = True Then
            If Dgl1.Item(Col1ItemCategory, bRowIndex).Value <> "" Or
                Dgl1.Item(Col1ItemGroup, bRowIndex).Value <> "" Or
                Dgl1.Item(Col1Item, bRowIndex).Value <> "" Or
                Dgl1.Item(Col1Dimension1, bRowIndex).Value <> "" Or
                Dgl1.Item(Col1Dimension2, bRowIndex).Value <> "" Or
                Dgl1.Item(Col1Dimension3, bRowIndex).Value <> "" Or
                Dgl1.Item(Col1Dimension4, bRowIndex).Value <> "" Or
                Dgl1.Item(Col1Size, bRowIndex).Value <> "" Then
                Dgl1.Item(Col1SKU, bRowIndex).Value = Dgl1.Item(Col1ItemCategory, bRowIndex).Value + " " +
                                    Dgl1.Item(Col1ItemGroup, bRowIndex).Value + " " +
                                    Dgl1.Item(Col1Item, bRowIndex).Value + " " +
                                    Dgl1.Item(Col1Dimension1, bRowIndex).Value + " " +
                                    Dgl1.Item(Col1Dimension2, bRowIndex).Value + " " +
                                    Dgl1.Item(Col1Dimension3, bRowIndex).Value + " " +
                                    Dgl1.Item(Col1Dimension4, bRowIndex).Value + " " +
                                    Dgl1.Item(Col1Size, bRowIndex).Value
                Dim DrSKU As DataRow() = AgL.PubDtItem.Select(" IsNull(ItemCategory,'') = '" & Dgl1.Item(Col1ItemCategory, bRowIndex).Tag & "'
                                    And IsNull(ItemGroup,'') = '" & Dgl1.Item(Col1ItemGroup, bRowIndex).Tag & "'
                                    And IsNull(BaseItem,'') = '" & Dgl1.Item(Col1Item, bRowIndex).Tag & "'
                                    And IsNull(Dimension1,'') = '" & Dgl1.Item(Col1Dimension1, bRowIndex).Tag & "'
                                    And IsNull(Dimension2,'') = '" & Dgl1.Item(Col1Dimension2, bRowIndex).Tag & "'
                                    And IsNull(Dimension3,'') = '" & Dgl1.Item(Col1Dimension3, bRowIndex).Tag & "'
                                    And IsNull(Dimension4,'') = '" & Dgl1.Item(Col1Dimension4, bRowIndex).Tag & "'
                                    And IsNull(Size,'') = '" & Dgl1.Item(Col1Size, bRowIndex).Tag & "'")
                If DrSKU.Length > 0 Then
                    Dgl1.Item(Col1SKU, bRowIndex).Tag = AgL.XNull(DrSKU(0)("Code"))
                End If
            Else
                Dgl1.Item(Col1SKU, bRowIndex).Tag = ""
                Dgl1.Item(Col1SKU, bRowIndex).Value = ""
            End If
        Else
            Dgl1.Item(Col1SKU, bRowIndex).Tag = Dgl1.Item(Col1Item, bRowIndex).Tag
            Dgl1.Item(Col1SKU, bRowIndex).Value = Dgl1.Item(Col1Item, bRowIndex).Value
        End If
    End Sub
    Private Sub DglControl_GotFocus(sender As Object, e As EventArgs) Handles Dgl1.GotFocus, DglMain.GotFocus
        Try
            For Each DglControl As Control In Me.Controls
                If TypeOf DglControl Is DataGridView Then
                    If Not AgL.StrCmp(DglControl.Name, sender.Name) And
                            Not AgL.StrCmp(DglControl.Name, "HelpDg") Then
                        If CType(DglControl, DataGridView).FirstDisplayedCell IsNot Nothing Then
                            CType(DglControl, DataGridView).CurrentCell = CType(DglControl, DataGridView).FirstDisplayedCell
                            CType(DglControl, DataGridView).CurrentCell.Selected = False
                        End If
                    End If
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FrmCatalog_BaseEvent_Topctrl_tbPrn(SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        FGetPrint(SearchCode, ClsMain.PrintFor.DocumentPrint)
    End Sub

    Public Sub FGetPrint(ByVal SearchCode As String, mPrintFor As ClsMain.PrintFor, Optional ByVal IsPrintToPrinter As Boolean = False, Optional BulkCondStr As String = "")
        FGetPrintCrystal(SearchCode, mPrintFor, IsPrintToPrinter, BulkCondStr)
    End Sub

    Sub FGetPrintCrystal(ByVal SearchCode As String, mPrintFor As ClsMain.PrintFor, Optional ByVal IsPrintToPrinter As Boolean = False, Optional BulkCondStr As String = "")
        Dim mPrintTitle As String
        Dim PrintingCopies() As String
        Dim I As Integer, J As Integer



        mPrintTitle = "Customer Copy"



        Dim bPrimaryQry As String = ""
        If BulkCondStr <> "" Then
            bPrimaryQry = " Select * From Catalog With (NoLock) Where Code In (" & BulkCondStr & ")"
            PrintingCopies = FGetSettings(SettingFields.PrintingBulkCopyCaptions, SettingType.General).ToString.Split(",")
        Else
            bPrimaryQry = " Select * From Catalog  With (NoLock) Where Code = '" & SearchCode & "'"
            PrintingCopies = FGetSettings(SettingFields.PrintingCopyCaptions, SettingType.General).ToString.Split(",")
        End If


        mQry = ""
        For I = 1 To PrintingCopies.Length
            If mQry <> "" Then mQry = mQry + " Union All "
            mQry = mQry + "
                Select '" & I & "' as Copies, '" & AgL.XNull(PrintingCopies(I - 1)) & "' as CopyPrintingCaption, H.Code, Site.Name as SiteName,  
                I.Specification as ItemSpecification, IG.Description as ItemGroupName, IC.Description as ItemCategoryName, IfNull(IState.Description,'') as ItemStateDesc, H.*, L.*,
                '" & FGetSettings(SettingFields.DocumentPrintHeaderPattern, SettingType.General) & "' as DocumentPrintHeaderPattern, 
                '" & AgL.PubUserName & "' as PrintedByUser, H.EntryBy as EntryByUser, '" & mPrintTitle & "' as PrintTitle,
                '" & FGetSettings(SettingFields.DocumentPrintShowPrintDateTimeYn, SettingType.General) & "' as DocumentPrintShowPrintDateTimeYn                
                from (" & bPrimaryQry & ") as H                
                Left Join CatalogDetail L  With (NoLock) On H.Code = L.Code
                Left Join Item I on L.Item = I.Code
                Left Join Item IG On I.ItemGroup = IG.Code
                Left Join Item IC On I.ItemCategory = IC.Code
                Left Join SiteMast Site On H.Site_Code = Site.Code
                Left Join Item IState On Istate.Code = L.ItemState
                "
        Next
        mQry = mQry + " Order By Copies, H.Code, L.Sr "


        Dim objRepPrint As Object
        If mPrintFor = ClsMain.PrintFor.EMail Then
            objRepPrint = New AgLibrary.FrmMailComposeWithCrystal(AgL)
            'FGetMailConfiguration(objRepPrint, SearchCode)
        Else
            objRepPrint = New AgLibrary.RepView(AgL)
        End If


        'If mDocReportFileName = "" Then
        If mPrintFor <> PrintFor.QA Then
            ClsMain.FPrintThisDocument(Me, objRepPrint, CatalogV_Type, mQry, "Catalog_Print.rpt", mPrintTitle, , , , "", AgL.PubLoginDate, IsPrintToPrinter)
        Else
            ClsMain.FPrintThisDocument(Me, objRepPrint, CatalogV_Type, mQry, "Catalog_Print_Thermal.rpt", mPrintTitle, , , , "", AgL.PubLoginDate, IsPrintToPrinter)
        End If
        'Else
        'ClsMain.FPrintThisDocument(Me, objRepPrint, TxtV_Type.Tag, mQry, mDocReportFileName, mPrintTitle, , , , TxtPartyName.Tag, TxtV_Date.Text, IsPrintToPrinter)
        'End If
    End Sub


    Public Structure StructCatalog
        Dim Code As String
        Dim Specification As String
        Dim Description As String
        Dim Site_Code As String
        Dim EntryBy As String
        Dim EntryDate As String
        Dim EntryType As String
        Dim EntryStatus As String
        Dim Status As String
        Dim Div_Code As String
        Dim UID As String
        Dim OmsId As String
        Dim UploadDate As String


        '''''''''''''''''''''''''''''''''Line Detail''''''''''''''''''''''''''''''''''
        Dim Line_Sr As String
        Dim Line_ItemCode As String
        Dim Line_ItemName As String
        Dim Line_ItemStateCode As String
        Dim Line_ItemStateName As String
        Dim Line_Qty As String
        Dim Line_Rate As String
        Dim Line_UploadDate As String
        Dim Line_DiscountPer As String
        Dim Line_AdditionalDiscountPer As String
        Dim Line_AdditionPer As String
        Dim Line_Unit As String
        Dim Line_OMSId As String
    End Structure

    Public Shared Sub InsertCatalog(CatalogTableList As StructCatalog())
        Dim mQry As String = ""

        If AgL.VNull(AgL.Dman_Execute("Select Count(*) From Catalog With (NoLock) where Description = " & AgL.Chk_Text(CatalogTableList(0).Description) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar) = 0 Then
            mQry = "INSERT INTO Catalog (Code, Description, EntryBy, EntryDate, EntryType, 
                    EntryStatus, Status, Div_Code, UID, OmsId, UploadDate, Site_Code, Specification)
                    VALUES (" & AgL.Chk_Text(CatalogTableList(0).Code) & ", 
                    " & AgL.Chk_Text(CatalogTableList(0).Description) & ", 
                    " & AgL.Chk_Text(CatalogTableList(0).EntryBy) & ", 
                    " & AgL.Chk_Text(CatalogTableList(0).EntryDate) & ", 
                    " & AgL.Chk_Text(CatalogTableList(0).EntryType) & ", 
                    " & AgL.Chk_Text(CatalogTableList(0).EntryStatus) & ", 
                    " & AgL.Chk_Text(CatalogTableList(0).Status) & ", 
                    " & AgL.Chk_Text(CatalogTableList(0).Div_Code) & ", 
                    " & AgL.Chk_Text(CatalogTableList(0).UID) & ", 
                    " & AgL.Chk_Text(CatalogTableList(0).OmsId) & ", 
                    " & AgL.Chk_Text(CatalogTableList(0).UploadDate) & ", 
                    " & AgL.Chk_Text(CatalogTableList(0).Site_Code) & ", 
                    " & AgL.Chk_Text(CatalogTableList(0).Specification) & ")"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            For I As Integer = 0 To CatalogTableList.Length - 1
                If CatalogTableList(I).Line_ItemName IsNot Nothing Then
                    If CatalogTableList(I).Line_ItemCode = "" Or CatalogTableList(I).Line_ItemCode Is Nothing Then
                        CatalogTableList(I).Line_ItemCode = AgL.Dman_Execute("SELECT Code FROM Item Where Description =  " & AgL.Chk_Text(CatalogTableList(I).Line_ItemName) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
                    End If

                    mQry = "INSERT INTO CatalogDetail (Code, Sr, Item, Qty, Rate, UploadDate, DiscountPer, 
                        AdditionalDiscountPer, AdditionPer, Unit, ItemState)
                        VALUES (" & AgL.Chk_Text(CatalogTableList(0).Code) & ", 
                        " & AgL.Chk_Text(CatalogTableList(I).Line_Sr) & ", 
                        " & AgL.Chk_Text(CatalogTableList(I).Line_ItemCode) & ", 
                        " & AgL.Chk_Text(CatalogTableList(I).Line_Qty) & ", 
                        " & AgL.Chk_Text(CatalogTableList(I).Line_Rate) & ", 
                        " & AgL.Chk_Text(CatalogTableList(I).Line_UploadDate) & ", 
                        " & AgL.Chk_Text(CatalogTableList(I).Line_DiscountPer) & ", 
                        " & AgL.Chk_Text(CatalogTableList(I).Line_AdditionalDiscountPer) & ", 
                        " & AgL.Chk_Text(CatalogTableList(I).Line_AdditionPer) & ", 
                        " & AgL.Chk_Text(CatalogTableList(I).Line_Unit) & ", 
                        " & AgL.Chk_Text(CatalogTableList(I).Line_ItemStateCode) & ") "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            Next
        Else
            mQry = " UPDATE Catalog Set OMSId = '" & CatalogTableList(0).OmsId & "' 
                    Where Description = '" & CatalogTableList(0).Description & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If
    End Sub

    Private Sub FrmSaleInvoiceDirect_BaseEvent_Topctrl_tbMore() Handles Me.BaseEvent_Topctrl_tbMore
        MnuOptions.Show(Topctrl1, Topctrl1.btbSite.Rectangle.X, Topctrl1.btbSite.Rectangle.Y + Topctrl1.btbSite.Rectangle.Size.Height)
    End Sub

    Private Sub MnuOptions_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MnuOptions.ItemClicked
        Select Case e.ClickedItem.Name
            Case MnuCopyRecord.Name
                gCopiedSearchCode = mSearchCode
            Case MnuPasteRecord.Name
                If gCopiedSearchCode <> "" Then
                    MoveRecLine(gCopiedSearchCode)
                Else
                    MsgBox("No data to paste")
                End If
            Case MnuPrintCustomerCopy.Name
                FGetPrint(mSearchCode, ClsMain.PrintFor.QA)
        End Select
    End Sub


End Class
