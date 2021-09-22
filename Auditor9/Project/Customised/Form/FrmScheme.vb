Imports System.IO
Imports AgLibrary.ClsMain.agConstants
Imports System.Xml
Imports Customised.ClsMain
Imports System.ComponentModel
Imports System.Linq
Public Class FrmScheme
    Inherits AgTemplate.TempMaster
    Dim mQry$ = ""
    Dim mIsReturnValue As Boolean = False

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public WithEvents Dgl2 As New AgControls.AgDataGrid

    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1BtnDetail As String = "Detail"
    Public Const Col1HeadOriginal As String = "Head Original"
    Public Const Col1LastValue As String = "Last Value"

    Public Const Col2ValueGreaterThen As String = "Value Greater Then"
    Public Const Col2DiscountPer As String = "Discount Per"
    Public Const Col2DiscountAmount As String = "Discount Amount"

    Public Const rowDescription As Integer = 0
    Public Const rowFromDate As Integer = 1
    Public Const rowToDate As Integer = 2
    Public Const rowProcess As Integer = 3
    Public Const rowApplyOn As Integer = 4
    Public Const rowBase As Integer = 5
    Public Const rowPostToAccount As Integer = 6
    Public Const rowPostToSubGroupType As Integer = 7
    Public Const rowPostEntryAs As Integer = 8
    Public Const rowIncludeParty As Integer = 9
    Public Const rowExcludeParty As Integer = 10
    Public Const rowIncludeItemCategory As Integer = 11
    Public Const rowExcludeItemCategory As Integer = 12
    Public Const rowIncludeItemGroup As Integer = 13
    Public Const rowExcludeItemGroup As Integer = 14
    Public Const rowIncludeItem As Integer = 15
    Public Const rowExcludeItem As Integer = 16
    Public Const rowIncludeSite As Integer = 17
    Public Const rowExcludeSite As Integer = 18
    Public Const rowIncludeDivision As Integer = 19
    Public Const rowExcludeDivision As Integer = 20



    Public Const hcDescription As String = "Description"
    Public Const hcFromDate As String = "From Date"
    Public Const hcToDate As String = "To Date"
    Public Const hcProcess As String = "Process"
    Public Const hcApplyOn As String = "Apply On"
    Public Const hcBase As String = "Base"
    Public Const hcPostToAccount As String = "Post To Account"
    Public Const hcPostToSubGroupType As String = "Post To Customer/Supplier"
    Public Const hcPostEntryAs As String = "Post Entry As"
    Public Const hcIncludeParty As String = "Include Party"
    Public Const hcExcludeParty As String = "Exclude Party"
    Public Const hcIncludeItemCategory As String = "Include Item Category"
    Public Const hcExcludeItemCategory As String = "Exclude Item Category"
    Public Const hcIncludeItemGroup As String = "Include Item Group"
    Public Const hcExcludeItemGroup As String = "Exclude Item Group"
    Public Const hcIncludeItem As String = "Include Item"
    Public Const hcExcludeItem As String = "Exclude Item"
    Public Const hcIncludeSite As String = "Include Site"
    Public Const hcExcludeSite As String = "Exclude Site"
    Public Const hcIncludeDivision As String = "Include Division"
    Public Const hcExcludeDivision As String = "Exclude Division"
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
        Me.OFDMain = New System.Windows.Forms.OpenFileDialog()
        Me.BtnAttachments = New System.Windows.Forms.Button()
        Me.Pnl2 = New System.Windows.Forms.Panel()
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
        'Pnl1
        '
        Me.Pnl1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl1.Location = New System.Drawing.Point(14, 47)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(948, 393)
        Me.Pnl1.TabIndex = 15
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
        'Pnl2
        '
        Me.Pnl2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl2.Location = New System.Drawing.Point(17, 446)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(945, 112)
        Me.Pnl2.TabIndex = 1020
        '
        'FrmScheme
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(974, 612)
        Me.ContextMenuStrip = Me.MnuOptions
        Me.Controls.Add(Me.Pnl2)
        Me.Controls.Add(Me.BtnAttachments)
        Me.Controls.Add(Me.Pnl1)
        Me.MaximizeBox = True
        Me.Name = "FrmScheme"
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
        Me.Controls.SetChildIndex(Me.BtnAttachments, 0)
        Me.Controls.SetChildIndex(Me.Pnl2, 0)
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
    Friend WithEvents Pnl1 As Panel
    Public WithEvents Pnl2 As Panel
#End Region

    Private Sub FrmShade_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        AgL.PubFindQry = " SELECT Code AS SearchCode, Description As Name, FromDate, ToDate,
                            ApplyOn, Base FROM SchemeHead "
        AgL.PubFindQryOrdBy = "[Name]"
    End Sub

    Private Sub FrmShade_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "SchemeHead"
        MainLineTableCsv = "SchemeDetail,SchemeItemDetail,SchemePartyDetail,SchemeDivisionDetail,SchemeSiteDetail"
    End Sub
    Private Sub ApplyUISetting()
        Dim mQry As String
        Dim DtTemp As DataTable
        Dim I As Integer, J As Integer
        Dim mDgl1RowCount As Integer
        Dim mDgl2ColumnCount As Integer

        Try
            For I = 0 To Dgl1.Rows.Count - 1
                Dgl1.Rows(I).Visible = False
            Next


            mQry = "Select H.*
                    from EntryHeaderUISetting H                   
                    Where EntryName='" & Me.Name & "' And GridName ='" & Dgl1.Name & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 0 To Dgl1.Rows.Count - 1
                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = Dgl1.Item(Col1HeadOriginal, J).Value Then
                            Dgl1.Rows(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
                            If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDgl1RowCount += 1
                            Dgl1.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
                            If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
                                Dgl1.Item(Col1Head, J).Value = AgL.XNull(DtTemp.Rows(I)("Caption"))
                            End If
                        End If
                    Next
                Next
            End If
            If mDgl1RowCount = 0 Then Dgl1.Visible = False Else Dgl1.Visible = True



            For I = 0 To Dgl2.Columns.Count - 1
                Dgl2.Columns(I).Visible = False
            Next


            mQry = "Select H.*
                    from EntryLineUISetting H                    
                    Where EntryName='FrmScheme' And GridName ='Dgl2' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 0 To Dgl2.Columns.Count - 1
                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = Dgl2.Columns(J).Name Then
                            Dgl2.Columns(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
                            If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDgl2ColumnCount += 1
                            If Not IsDBNull(DtTemp.Rows(I)("DisplayIndex")) Then
                                Dgl2.Columns(J).DisplayIndex = AgL.VNull(DtTemp.Rows(I)("DisplayIndex"))
                            End If
                            If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
                                Dgl2.Columns(J).HeaderText = AgL.XNull(DtTemp.Rows(I)("Caption"))
                            End If
                            'Dgl1.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
                        End If
                    Next
                Next
            End If
            If mDgl2ColumnCount = 0 Then Dgl2.Visible = False Else Dgl2.Visible = True

        Catch ex As Exception
            MsgBox(ex.Message & " [ApplySchemeTypeSetting]")
        End Try
    End Sub
    Private Sub FrmShade_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        mQry = "Select S.Code As SearchCode From SchemeHead S Where 1=1 "
        mQry += " Order by S.Description "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub
    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        mQry = "UPDATE SchemeHead " &
                " SET " &
                " Description = " & AgL.Chk_Text(Dgl1(Col1Value, rowDescription).Value) & ", " &
                " FromDate = " & AgL.Chk_Date(Dgl1(Col1Value, rowFromDate).Value) & ", " &
                " ToDate = " & AgL.Chk_Date(Dgl1(Col1Value, rowToDate).Value) & ", " &
                " Process = " & AgL.Chk_Text(Dgl1(Col1Value, rowProcess).Tag) & ", " &
                " ApplyOn = " & AgL.Chk_Text(Dgl1(Col1Value, rowApplyOn).Value) & ", " &
                " Base = " & AgL.Chk_Text(Dgl1(Col1Value, rowBase).Value) & ", " &
                " PostToAccount = " & AgL.Chk_Text(Dgl1(Col1Value, rowPostToAccount).Tag) & ", " &
                " PostToSubGroupType = " & AgL.Chk_Text(Dgl1(Col1Value, rowPostToSubGroupType).Value) & ", " &
                " PostEntryAs = " & AgL.Chk_Text(Dgl1(Col1Value, rowPostEntryAs).Value) & " " &
                " Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "DELETE FROM SchemeDetail WHERE Code = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "DELETE FROM SchemeItemDetail WHERE Code = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "DELETE FROM SchemePartyDetail WHERE Code = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "DELETE FROM SchemeSiteDetail WHERE Code = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "DELETE FROM SchemeDivisionDetail WHERE Code = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        Call FPostSchemeDetail(Conn, Cmd)

        Call FPostSchemeItemDetail(rowIncludeItemCategory, 0, Conn, Cmd)
        Call FPostSchemeItemDetail(rowExcludeItemCategory, 1, Conn, Cmd)

        Call FPostSchemeItemDetail(rowIncludeItemGroup, 0, Conn, Cmd)
        Call FPostSchemeItemDetail(rowExcludeItemGroup, 1, Conn, Cmd)

        Call FPostSchemeItemDetail(rowIncludeItem, 0, Conn, Cmd)
        Call FPostSchemeItemDetail(rowExcludeItem, 1, Conn, Cmd)

        Call FPostSchemePartyDetail(rowIncludeParty, 0, Conn, Cmd)
        Call FPostSchemePartyDetail(rowExcludeParty, 1, Conn, Cmd)

        Call FPostSchemeSiteDetail(rowIncludeSite, 0, Conn, Cmd)
        Call FPostSchemeSiteDetail(rowExcludeSite, 1, Conn, Cmd)

        Call FPostSchemeDivisionDetail(rowIncludeDivision, 0, Conn, Cmd)
        Call FPostSchemeDivisionDetail(rowExcludeDivision, 1, Conn, Cmd)
    End Sub
    Private Sub FPostSchemeDetail(ByVal Conn As Object, ByVal Cmd As Object)
        Dim bRateListCode$ = ""
        Dim I As Integer, mSr As Integer

        mSr = AgL.Dman_Execute("Select IfNull(Max(Sr),0) From SchemeDetail With (NoLock) Where Code = '" & mSearchCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()

        For I = 0 To Dgl2.RowCount - 1
            If Dgl2.Item(Col2ValueGreaterThen, I).Value <> "" Then
                mSr += 1
                mQry = "INSERT INTO SchemeDetail(Code, Sr, ValueGreaterThen, DiscountPer, DiscountAmount) 
                        VALUES(" & AgL.Chk_Text(mSearchCode) & ", 
                        " & mSr & ", 
                        " & Val(Dgl2.Item(Col2ValueGreaterThen, I).Value) & ",
                        " & Val(Dgl2.Item(Col2DiscountPer, I).Value) & ",
                        " & Val(Dgl2.Item(Col2DiscountAmount, I).Value) & ") "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next
    End Sub

    Private Sub FPostSchemeItemDetail(RowNumber As Integer, IsExcluded As Byte, ByVal Conn As Object, ByVal Cmd As Object)
        Dim bRateListCode$ = ""
        Dim I As Integer, mSr As Integer

        Dim bValueArr As String() = Dgl1.Item(Col1Value, RowNumber).Tag.ToString.Split(",")

        mSr = AgL.Dman_Execute("Select IfNull(Max(Sr),0) From SchemeItemDetail With (NoLock) Where Code = '" & mSearchCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()

        For I = 0 To bValueArr.Length - 1
            If bValueArr(I) <> "" Then
                mSr += 1
                mQry = "INSERT INTO SchemeItemDetail(Code, Sr, Item, IsExcluded) 
                        VALUES(" & AgL.Chk_Text(mSearchCode) & ", 
                        " & mSr & ", 
                        " & AgL.Chk_Text(bValueArr(I)) & ",
                        " & Val(IsExcluded) & ") "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next
    End Sub
    Private Sub FPostSchemePartyDetail(RowNumber As Integer, IsExcluded As Byte, ByVal Conn As Object, ByVal Cmd As Object)
        Dim bRateListCode$ = ""
        Dim I As Integer, mSr As Integer

        Dim bValueArr As String() = Dgl1.Item(Col1Value, RowNumber).Tag.ToString.Split(",")

        mSr = AgL.Dman_Execute("Select IfNull(Max(Sr),0) From SchemePartyDetail With (NoLock) Where Code = '" & mSearchCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()

        For I = 0 To bValueArr.Length - 1
            If bValueArr(I) <> "" Then
                mSr += 1
                mQry = "INSERT INTO SchemePartyDetail(Code, Sr, SubCode, IsExcluded) 
                        VALUES(" & AgL.Chk_Text(mSearchCode) & ", 
                        " & mSr & ", 
                        " & AgL.Chk_Text(bValueArr(I)) & ",
                        " & Val(IsExcluded) & ") "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next
    End Sub
    Private Sub FPostSchemeSiteDetail(RowNumber As Integer, IsExcluded As Byte, ByVal Conn As Object, ByVal Cmd As Object)
        Dim bRateListCode$ = ""
        Dim I As Integer, mSr As Integer

        Dim bValueArr As String() = Dgl1.Item(Col1Value, RowNumber).Tag.ToString.Split(",")

        mSr = AgL.Dman_Execute("Select IfNull(Max(Sr),0) From SchemeSiteDetail With (NoLock) Where Code = '" & mSearchCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()

        For I = 0 To bValueArr.Length - 1
            If bValueArr(I) <> "" Then
                mSr += 1
                mQry = "INSERT INTO SchemeSiteDetail(Code, Sr, Site_Code, IsExcluded) 
                        VALUES(" & AgL.Chk_Text(mSearchCode) & ", 
                        " & mSr & ", 
                        " & AgL.Chk_Text(bValueArr(I)) & ",
                        " & Val(IsExcluded) & ") "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next
    End Sub
    Private Sub FPostSchemeDivisionDetail(RowNumber As Integer, IsExcluded As Byte, ByVal Conn As Object, ByVal Cmd As Object)
        Dim bRateListCode$ = ""
        Dim I As Integer, mSr As Integer

        Dim bValueArr As String() = Dgl1.Item(Col1Value, RowNumber).Tag.ToString.Split(",")

        mSr = AgL.Dman_Execute("Select IfNull(Max(Sr),0) From SchemeDivisionDetail With (NoLock) Where Code = '" & mSearchCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()

        For I = 0 To bValueArr.Length - 1
            If bValueArr(I) <> "" Then
                mSr += 1
                mQry = "INSERT INTO SchemeDivisionDetail(Code, Sr, Div_Code, IsExcluded) 
                        VALUES(" & AgL.Chk_Text(mSearchCode) & ", 
                        " & mSr & ", 
                        " & AgL.Chk_Text(bValueArr(I)) & ",
                        " & Val(IsExcluded) & ") "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next
    End Sub
    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DtTemp As DataTable
        Dim I As Integer

        mQry = "Select Sg.Name As ProcessName, Sg1.Name As PostToAccountName, S.* 
                From SchemeHead S 
                LEFT JOIN SubGroup Sg On S.Process = Sg.SubCode
                LEFT JOIN SubGroup Sg1 On S.PostToAccount = Sg1.SubCode
                Where S.Code='" & SearchCode & "'"
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        With DtTemp
            If .Rows.Count > 0 Then
                Dgl1.Item(Col1Value, rowDescription).Value = AgL.XNull(.Rows(0)("Description"))
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                Dgl1.Item(Col1Value, rowFromDate).Value = AgL.XNull(.Rows(0)("FromDate"))
                Dgl1.Item(Col1Value, rowToDate).Value = AgL.XNull(.Rows(0)("ToDate"))
                Dgl1.Item(Col1Value, rowProcess).Tag = AgL.XNull(.Rows(0)("Process"))
                Dgl1.Item(Col1Value, rowProcess).Value = AgL.XNull(.Rows(0)("ProcessName"))
                Dgl1.Item(Col1Value, rowApplyOn).Value = AgL.XNull(.Rows(0)("ApplyOn"))
                Dgl1.Item(Col1Value, rowBase).Value = AgL.XNull(.Rows(0)("Base"))
                Dgl1.Item(Col1Value, rowPostToAccount).Tag = AgL.XNull(.Rows(0)("PostToAccount"))
                Dgl1.Item(Col1Value, rowPostToAccount).Value = AgL.XNull(.Rows(0)("PostToAccountName"))
                Dgl1.Item(Col1Value, rowPostToSubGroupType).Value = AgL.XNull(.Rows(0)("PostToSubGroupType"))
                Dgl1.Item(Col1Value, rowPostEntryAs).Value = AgL.XNull(.Rows(0)("PostEntryAs"))
            End If
        End With

        mQry = "Select * From SchemeDetail where Code = '" & mSearchCode & "'"
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        With DtTemp
            Dgl2.RowCount = 1
            Dgl2.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    Dgl2.Rows.Add()
                    Dgl2.Item(ColSNo, I).Value = Dgl2.Rows.Count - 1
                    Dgl2.Item(Col2ValueGreaterThen, I).Value = AgL.XNull(.Rows(I)("ValueGreaterThen"))
                    Dgl2.Item(Col2DiscountPer, I).Value = AgL.XNull(.Rows(I)("DiscountPer"))
                    Dgl2.Item(Col2DiscountAmount, I).Value = AgL.XNull(.Rows(I)("DiscountAmount"))
                Next I
            End If
        End With



        mQry = "Select L.Item, IfNull(L.IsExcluded,0) As IsExcluded, I.V_Type As ItemV_Type, 
                I.Description As ItemDesc
                From SchemeItemDetail L 
                LEFT JOIN Item I ON L.Item = I.Code
                Where L.Code = '" & mSearchCode & "' "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        For I = 0 To DtTemp.Rows.Count - 1
            If AgL.XNull(DtTemp.Rows(I)("ItemV_Type")) = ItemV_Type.ItemCategory Then
                If AgL.VNull(DtTemp.Rows(I)("IsExcluded")) <> 0 Then
                    If Dgl1.Item(Col1Value, rowExcludeItemCategory).Tag <> "" Then Dgl1.Item(Col1Value, rowExcludeItemCategory).Tag += ","
                    If Dgl1.Item(Col1Value, rowExcludeItemCategory).Value <> "" Then Dgl1.Item(Col1Value, rowExcludeItemCategory).Value += ","
                    Dgl1.Item(Col1Value, rowExcludeItemCategory).Tag += AgL.XNull(DtTemp.Rows(I)("Item"))
                    Dgl1.Item(Col1Value, rowExcludeItemCategory).Value += AgL.XNull(DtTemp.Rows(I)("ItemDesc"))
                Else
                    If Dgl1.Item(Col1Value, rowIncludeItemCategory).Tag <> "" Then Dgl1.Item(Col1Value, rowIncludeItemCategory).Tag += ","
                    If Dgl1.Item(Col1Value, rowIncludeItemCategory).Value <> "" Then Dgl1.Item(Col1Value, rowIncludeItemCategory).Value += ","
                    Dgl1.Item(Col1Value, rowIncludeItemCategory).Tag += AgL.XNull(DtTemp.Rows(I)("Item"))
                    Dgl1.Item(Col1Value, rowIncludeItemCategory).Value += AgL.XNull(DtTemp.Rows(I)("ItemDesc"))
                End If
            ElseIf AgL.XNull(DtTemp.Rows(I)("ItemV_Type")) = ItemV_Type.ItemGroup Then
                If AgL.VNull(DtTemp.Rows(I)("IsExcluded")) <> 0 Then
                    If Dgl1.Item(Col1Value, rowExcludeItemGroup).Tag <> "" Then Dgl1.Item(Col1Value, rowExcludeItemGroup).Tag += ","
                    If Dgl1.Item(Col1Value, rowExcludeItemGroup).Value <> "" Then Dgl1.Item(Col1Value, rowExcludeItemGroup).Value += ","
                    Dgl1.Item(Col1Value, rowExcludeItemGroup).Tag += AgL.XNull(DtTemp.Rows(I)("Item"))
                    Dgl1.Item(Col1Value, rowExcludeItemGroup).Value += AgL.XNull(DtTemp.Rows(I)("ItemDesc"))
                Else
                    If Dgl1.Item(Col1Value, rowIncludeItemGroup).Tag <> "" Then Dgl1.Item(Col1Value, rowIncludeItemGroup).Tag += ","
                    If Dgl1.Item(Col1Value, rowIncludeItemGroup).Value <> "" Then Dgl1.Item(Col1Value, rowIncludeItemGroup).Value += ","
                    Dgl1.Item(Col1Value, rowIncludeItemGroup).Tag += AgL.XNull(DtTemp.Rows(I)("Item"))
                    Dgl1.Item(Col1Value, rowIncludeItemGroup).Value += AgL.XNull(DtTemp.Rows(I)("ItemDesc"))
                End If
            Else
                If AgL.VNull(DtTemp.Rows(I)("IsExcluded")) <> 0 Then
                    If Dgl1.Item(Col1Value, rowExcludeItem).Tag <> "" Then Dgl1.Item(Col1Value, rowExcludeItem).Tag += ","
                    If Dgl1.Item(Col1Value, rowExcludeItem).Value <> "" Then Dgl1.Item(Col1Value, rowExcludeItem).Value += ","
                    Dgl1.Item(Col1Value, rowExcludeItem).Tag += AgL.XNull(DtTemp.Rows(I)("Item"))
                    Dgl1.Item(Col1Value, rowExcludeItem).Value += AgL.XNull(DtTemp.Rows(I)("ItemDesc"))
                Else
                    If Dgl1.Item(Col1Value, rowIncludeItem).Tag <> "" Then Dgl1.Item(Col1Value, rowIncludeItem).Tag += ","
                    If Dgl1.Item(Col1Value, rowIncludeItem).Value <> "" Then Dgl1.Item(Col1Value, rowIncludeItem).Value += ","
                    Dgl1.Item(Col1Value, rowIncludeItem).Tag += AgL.XNull(DtTemp.Rows(I)("Item"))
                    Dgl1.Item(Col1Value, rowIncludeItem).Value += AgL.XNull(DtTemp.Rows(I)("ItemDesc"))
                End If
            End If
        Next


        mQry = "Select L.SubCode, IfNull(L.IsExcluded,0) As IsExcluded, Sg.Name As PartyName
                From SchemePartyDetail L 
                LEFT JOIN ViewHelpSubgroup Sg ON L.SubCode = Sg.Code
                Where L.Code = '" & mSearchCode & "' "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        For I = 0 To DtTemp.Rows.Count - 1
            If AgL.VNull(DtTemp.Rows(I)("IsExcluded")) <> 0 Then
                If Dgl1.Item(Col1Value, rowExcludeParty).Tag <> "" Then Dgl1.Item(Col1Value, rowExcludeParty).Tag += ","
                If Dgl1.Item(Col1Value, rowExcludeParty).Value <> "" Then Dgl1.Item(Col1Value, rowExcludeParty).Value += ","
                Dgl1.Item(Col1Value, rowExcludeParty).Tag += AgL.XNull(DtTemp.Rows(I)("SubCode"))
                Dgl1.Item(Col1Value, rowExcludeParty).Value += AgL.XNull(DtTemp.Rows(I)("PartyName"))
            Else
                If Dgl1.Item(Col1Value, rowIncludeParty).Tag <> "" Then Dgl1.Item(Col1Value, rowIncludeParty).Tag += ","
                If Dgl1.Item(Col1Value, rowIncludeParty).Value <> "" Then Dgl1.Item(Col1Value, rowIncludeParty).Value += ","
                Dgl1.Item(Col1Value, rowIncludeParty).Tag += AgL.XNull(DtTemp.Rows(I)("SubCode"))
                Dgl1.Item(Col1Value, rowIncludeParty).Value += AgL.XNull(DtTemp.Rows(I)("PartyName"))
            End If
        Next

        mQry = "Select L.Site_Code, IfNull(L.IsExcluded,0) As IsExcluded, S.Name As SiteName
                From SchemeSiteDetail L 
                LEFT JOIN SiteMast S ON L.Site_Code = S.Code
                Where L.Code = '" & mSearchCode & "' "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        For I = 0 To DtTemp.Rows.Count - 1
            If AgL.VNull(DtTemp.Rows(I)("IsExcluded")) <> 0 Then
                If Dgl1.Item(Col1Value, rowExcludeSite).Tag <> "" Then Dgl1.Item(Col1Value, rowExcludeSite).Tag += ","
                If Dgl1.Item(Col1Value, rowExcludeSite).Value <> "" Then Dgl1.Item(Col1Value, rowExcludeSite).Value += ","
                Dgl1.Item(Col1Value, rowExcludeSite).Tag += AgL.XNull(DtTemp.Rows(I)("Site_Code"))
                Dgl1.Item(Col1Value, rowExcludeSite).Value += AgL.XNull(DtTemp.Rows(I)("SiteName"))
            Else
                If Dgl1.Item(Col1Value, rowIncludeSite).Tag <> "" Then Dgl1.Item(Col1Value, rowIncludeSite).Tag += ","
                If Dgl1.Item(Col1Value, rowIncludeSite).Value <> "" Then Dgl1.Item(Col1Value, rowIncludeSite).Value += ","
                Dgl1.Item(Col1Value, rowIncludeSite).Tag += AgL.XNull(DtTemp.Rows(I)("Site_Code"))
                Dgl1.Item(Col1Value, rowIncludeSite).Value += AgL.XNull(DtTemp.Rows(I)("SiteName"))
            End If
        Next

        mQry = "Select L.Div_Code, IfNull(L.IsExcluded,0) As IsExcluded, D.Div_Name As DivisionName
                From SchemeDivisionDetail L 
                LEFT JOIN Division D ON L.Div_Code = D.Div_Code
                Where L.Code = '" & mSearchCode & "' "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        For I = 0 To DtTemp.Rows.Count - 1
            If AgL.VNull(DtTemp.Rows(I)("IsExcluded")) <> 0 Then
                If Dgl1.Item(Col1Value, rowExcludeDivision).Tag <> "" Then Dgl1.Item(Col1Value, rowExcludeDivision).Tag += ","
                If Dgl1.Item(Col1Value, rowExcludeDivision).Value <> "" Then Dgl1.Item(Col1Value, rowExcludeDivision).Value += ","
                Dgl1.Item(Col1Value, rowExcludeDivision).Tag += AgL.XNull(DtTemp.Rows(I)("Div_Code"))
                Dgl1.Item(Col1Value, rowExcludeDivision).Value += AgL.XNull(DtTemp.Rows(I)("DivisionName"))
            Else
                If Dgl1.Item(Col1Value, rowIncludeDivision).Tag <> "" Then Dgl1.Item(Col1Value, rowIncludeDivision).Tag += ","
                If Dgl1.Item(Col1Value, rowIncludeDivision).Value <> "" Then Dgl1.Item(Col1Value, rowIncludeDivision).Value += ","
                Dgl1.Item(Col1Value, rowIncludeDivision).Tag += AgL.XNull(DtTemp.Rows(I)("Div_Code"))
                Dgl1.Item(Col1Value, rowIncludeDivision).Value += AgL.XNull(DtTemp.Rows(I)("DivisionName"))
            End If
        Next


        Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        For I = 0 To Dgl1.Rows.Count - 1
            If AgL.XNull(Dgl1.Item(Col1Value, I).Value) <> "" Then
                If Dgl1.Item(Col1Value, I).Value.ToString.Length < 500 Then
                    Dgl1.AutoResizeRow(I, DataGridViewAutoSizeRowMode.AllCells)
                Else
                    Dgl1.Rows(I).Height = 50
                End If
            End If
        Next
        SetLastValues()
        SetAttachmentCaption()
        Topctrl1.tPrn = False
    End Sub
    Private Sub SetLastValues()
        Dim I As Integer
        For I = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1LastValue, I).Value = Dgl1(Col1Value, I).Value
            Dgl1(Col1LastValue, I).Tag = Dgl1(Col1Value, I).Tag
        Next
    End Sub
    Public Sub SaveDataInPersonLastTransactionValues(DocId As String, ByVal Conn As Object, ByVal Cmd As Object)
        Dim I As Integer, J As Integer
        Dim DtDivision As DataTable
        Dim DtSite As DataTable

        'mQry = "Delete from SchemeSiteDivisionDetail Where Code='" & DocId & "'"
        'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Select Div_Code, Div_Name From Division Order By Div_Name"
        DtDivision = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

        mQry = "Select Code, Name From SiteMast Order By Name"
        DtSite = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

        For J = 0 To DtDivision.Rows.Count - 1
            For I = 0 To DtSite.Rows.Count - 1
                If Topctrl1.Mode = "Add" Then
                    mQry = " INSERT INTO SchemeSiteDivisionDetail (Code, Div_Code, Site_Code) 
                                    VALUES (" & AgL.Chk_Text(DocId) & ", 
                                    " & AgL.Chk_Text(DtDivision.Rows(J)("Div_Code")) & ", 
                                    " & AgL.Chk_Text(DtSite.Rows(I)("Code")) & "                                     
                                    )"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                Else
                    mQry = "Select Count(*) from SchemeSiteDivisionDetail Where Div_Code = '" & DtDivision.Rows(J)("Div_Code") & "' And  Site_Code = '" & DtSite.Rows(I)("Code") & "' And Code = '" & DocId & "' "
                    If AgL.Dman_Execute(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar() = 0 Then
                        mQry = " INSERT INTO SchemeSiteDivisionDetail (Code, Div_Code, Site_Code) 
                                    VALUES (" & AgL.Chk_Text(DocId) & ", 
                                    " & AgL.Chk_Text(DtDivision.Rows(J)("Div_Code")) & ", 
                                    " & AgL.Chk_Text(DtSite.Rows(I)("Code")) & "                                     
                                    )"
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                End If
            Next
        Next
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

        If Dgl1.Rows(rowDescription).Visible = True Then
            Dgl1.CurrentCell = Dgl1(Col1Value, rowDescription)
            Dgl1.Focus()
        End If
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Private Sub FrmParty_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        mQry = " SELECT DocId FROM SchemeQulified L WHERE L.[Scheme] = '" & mSearchCode & "'  "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            MsgBox("Scheme is already processed for some invoices.Can't edit it.", MsgBoxStyle.Information) : Passed = False : Exit Sub
        End If


        Dgl1.CurrentCell = Dgl1(Col1Value, rowFromDate)
        Dgl1.Focus()
    End Sub
    Private Sub FrmScheme_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dim I As Integer

        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Head, 250, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl1, Col1Mandatory, 12, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(Dgl1, Col1Value, 630, 255, Col1Value, True, False)
            .AddAgButtonColumn(Dgl1, Col1BtnDetail, 35, Col1BtnDetail, True, True)
            .AddAgTextColumn(Dgl1, Col1HeadOriginal, 200, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(Dgl1, Col1LastValue, 200, 255, Col1LastValue, False, True)

        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.Columns(Col1Mandatory).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings 2", 5.25, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Dgl1.Columns(Col1Mandatory).DefaultCellStyle.ForeColor = Color.Red
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToAddRows = False
        Dgl1.RowHeadersVisible = False
        Dgl1.ColumnHeadersVisible = False
        Dgl1.BackgroundColor = Me.BackColor
        Dgl1.BorderStyle = BorderStyle.None
        Dgl1.AgAllowFind = False
        Dgl1.Name = "Dgl1"
        AgL.GridDesign(Dgl1)
        Dgl1.Anchor = AnchorStyles.Top + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom


        Dgl1.Rows.Add(21)

        Dgl1.Item(Col1Head, rowDescription).Value = hcDescription
        Dgl1.Item(Col1Head, rowFromDate).Value = hcFromDate
        Dgl1.Item(Col1Head, rowToDate).Value = hcToDate
        Dgl1.Item(Col1Head, rowProcess).Value = hcProcess
        Dgl1.Item(Col1Head, rowApplyOn).Value = hcApplyOn
        Dgl1.Item(Col1Head, rowBase).Value = hcBase
        Dgl1.Item(Col1Head, rowIncludeParty).Value = hcIncludeParty
        Dgl1.Item(Col1Head, rowExcludeParty).Value = hcExcludeParty
        Dgl1.Item(Col1Head, rowIncludeItemCategory).Value = hcIncludeItemCategory
        Dgl1.Item(Col1Head, rowExcludeItemCategory).Value = hcExcludeItemCategory
        Dgl1.Item(Col1Head, rowIncludeItemGroup).Value = hcIncludeItemGroup
        Dgl1.Item(Col1Head, rowExcludeItemGroup).Value = hcExcludeItemGroup
        Dgl1.Item(Col1Head, rowIncludeItem).Value = hcIncludeItem
        Dgl1.Item(Col1Head, rowExcludeItem).Value = hcExcludeItem
        Dgl1.Item(Col1Head, rowIncludeSite).Value = hcIncludeSite
        Dgl1.Item(Col1Head, rowExcludeSite).Value = hcExcludeSite
        Dgl1.Item(Col1Head, rowIncludeDivision).Value = hcIncludeDivision
        Dgl1.Item(Col1Head, rowExcludeDivision).Value = hcExcludeDivision
        Dgl1.Item(Col1Head, rowPostToAccount).Value = hcPostToAccount
        Dgl1.Item(Col1Head, rowPostToSubGroupType).Value = hcPostToSubGroupType
        Dgl1.Item(Col1Head, rowPostEntryAs).Value = hcPostEntryAs
        Dgl1(Col1Value, rowBase).Style.WrapMode = DataGridViewTriState.True

        For I = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1HeadOriginal, I).Value = Dgl1(Col1Head, I).Value
        Next


        Dgl2.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl2, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgNumberColumn(Dgl2, Col2ValueGreaterThen, 200, 7, 2, False, Col2ValueGreaterThen, True, False, True)
            .AddAgNumberColumn(Dgl2, Col2DiscountPer, 150, 2, 2, False, Col2DiscountPer, True, False, True)
            .AddAgNumberColumn(Dgl2, Col2DiscountAmount, 170, 7, 2, False, Col2DiscountAmount, True, False, True)
        End With
        AgL.AddAgDataGrid(Dgl2, Pnl2)
        Dgl2.EnableHeadersVisualStyles = False
        Dgl2.AgSkipReadOnlyColumns = True
        Dgl2.RowHeadersVisible = False
        Dgl1.AllowUserToAddRows = False
        Dgl2.Visible = False
        Dgl2.BackgroundColor = Me.BackColor
        AgL.GridDesign(Dgl2)
        Dgl2.Name = "Dgl2"
        Dgl2.Anchor = AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom

        ApplyUISetting()
    End Sub
    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            If Topctrl1.Mode = "BROWSE" Then
                Dgl1.CurrentCell.ReadOnly = True
            End If

            If Dgl1.CurrentCell.ColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub


            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0

            Select Case Dgl1.CurrentCell.RowIndex
                Case rowFromDate, rowToDate
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value

                Case rowIncludeItemCategory, rowExcludeItemCategory,
                     rowIncludeItemGroup, rowExcludeItemGroup,
                     rowIncludeItem, rowExcludeItem,
                     rowIncludeParty, rowExcludeParty,
                     rowIncludeSite, rowExcludeSite,
                     rowIncludeDivision, rowExcludeDivision
                    Dgl1.Item(Col1Value, Dgl1.CurrentCell.RowIndex).ReadOnly = True
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim bNewMasterCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub

            Select Case Dgl1.CurrentCell.RowIndex
                Case rowDescription
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = " Select H.Description As Code, H.Description As Name FROM SchemeHead H   "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value,,,,, True) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowApplyOn
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "Select 'Single Invoice' As Code, 'Single Invoice' As Name 
                                    Union All 
                                    Select 'Multiple Invoice' As Code, 'Multiple Invoice' As Name 
                                    Union All 
                                    Select 'Single Order' As Code, 'Single Order' As Name 
                                    Union All 
                                    Select 'Multiple Order' As Code, 'Multiple Order' As Name "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowBase
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "Select 'Quantity' as Code, 'Quantity' as Description 
                                UNION ALL 
                                Select 'Taxable Amount' as Code, 'Taxable Amount' as Description 
                                UNION ALL 
                                Select 'Net Amount' as Code, 'Net Amount' as Description
                                UNION ALL 
                                Select 'Rate' as Code, 'Rate' as Description "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowProcess
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Subcode AS Code, Name  FROM Subgroup WHERE SubgroupType = '" & SubgroupType.Process & "' "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowPostToAccount
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Subcode AS Code, Name  FROM Subgroup WHERE SubgroupType = '" & SubgroupType.LedgerAccount & "' "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowPostToSubGroupType
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT SubgroupType AS Code, SubgroupType As Name  
                                    FROM SubgroupType 
                                    WHERE SubgroupType In ('" & SubgroupType.Customer & "','" & SubgroupType.Supplier & "') "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowPostEntryAs
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "Select 'Debit/Credit Note' as Code, 'Debit/Credit Note' as Description 
                                UNION ALL 
                                Select 'Journal Voucher' as Code, 'Journal Voucher' as Description "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmScheme_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dim I As Integer
        For I = 0 To Dgl1.Rows.Count - 1
            Dgl1.Item(Col1Value, I).Value = ""
            Dgl1.Item(Col1Value, I).Tag = ""
            Dgl1.Item(Col1BtnDetail, I).Tag = Nothing
            Dgl1.Item(Col1BtnDetail, I) = New DataGridViewTextBoxCell
            Dgl1(Col1BtnDetail, I).ReadOnly = True
        Next
        Dgl2.Rows.Clear()
    End Sub
    Private Sub FrmScheme_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer

        passed = AgCL.AgCheckMandatory(Me)

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1(Col1Mandatory, I).Value <> "" And Dgl1.Rows(I).Visible Then
                If Dgl1(Col1Value, I).Value = "" And Dgl1(Col1BtnDetail, I).Value = "" Then
                    MsgBox(Dgl1(Col1Head, I).Value & " can not be blank.")
                    Dgl1.CurrentCell = Dgl1(Col1Value, I)
                    Dgl1.Focus()
                    passed = False
                    Exit Sub
                End If
            End If
        Next

        Dim IsBlankGrid As Boolean = True
        For I = 0 To Dgl2.RowCount - 1
            If Val(Dgl2(Col2ValueGreaterThen, I).Value) <> 0 Or Val(Dgl2(Col2DiscountPer, I).Value) <> 0 Or
                Val(Dgl2(Col2DiscountAmount, I).Value) <> 0 Then
                IsBlankGrid = False
            End If

            If Val(Dgl2(Col2DiscountPer, I).Value) <> 0 And Val(Dgl2(Col2DiscountAmount, I).Value) <> 0 Then
                MsgBox(" Discount Percentage And Discount Amount only one can be entered.", MsgBoxStyle.Information)
                Dgl2.CurrentCell = Dgl2(Col2DiscountAmount, I)
                Dgl2.Focus() : passed = False : Exit Sub
            End If
        Next

        If IsBlankGrid = True Then
            MsgBox(" Line Detail can not be blank.", MsgBoxStyle.Information)
            Dgl2.CurrentCell = Dgl2(Col2ValueGreaterThen, 0)
            Dgl2.Focus() : passed = False : Exit Sub
        End If






        SetLastValues()
    End Sub
    Private Sub Dgl1_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1Value).Index Then
            If Dgl1.Item(Col1Mandatory, Dgl1.CurrentCell.RowIndex).Value <> "" Then
                If Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Value = "" Then
                    MsgBox(Dgl1(Col1Head, Dgl1.CurrentCell.RowIndex).Value & " can not be blank.")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Private Sub FrmScheme_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Dim I As Integer
        For I = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1Head, I).Tag = Nothing
        Next
    End Sub
    Private Sub Dgl1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        If e.ColumnIndex = Dgl1.Columns(Col1BtnDetail).Index And TypeOf (Dgl1(Col1BtnDetail, e.RowIndex)) Is DataGridViewButtonCell Then
            Select Case e.RowIndex


            End Select
        End If
    End Sub
    Private Sub BtnAttachments_Click(sender As Object, e As EventArgs) Handles BtnAttachments.Click
        Dim FrmObj As New AgLibrary.FrmAttachmentViewer(AgL)
        FrmObj.LblDocNo.Text = "Party Name : " + Dgl1(Col1Value, rowToDate).Value
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
    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.KeyDown
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        If ClsMain.IsSpecialKeyPressed(e) Then Exit Sub
        If e.KeyCode = Keys.Enter Then Exit Sub
        If Topctrl1.Mode.ToUpper <> "BROWSE" Then
            If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1Value).Index Then
                If e.KeyCode = Keys.Delete Then
                    Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Value = ""
                    Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Tag = ""
                End If

                Select Case Dgl1.CurrentCell.RowIndex
                    Case rowIncludeItemCategory, rowExcludeItemCategory
                        FHPGD_ItemCategory(Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Tag, Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Value)
                    Case rowIncludeItemGroup, rowExcludeItemGroup
                        FHPGD_ItemGroup(Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Tag, Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Value)
                    Case rowIncludeItem, rowExcludeItem
                        FHPGD_Item(Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Tag, Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Value)
                    Case rowIncludeParty, rowExcludeParty
                        FHPGD_Party(Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Tag, Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Value)
                    Case rowIncludeSite, rowExcludeSite
                        FHPGD_Site(Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Tag, Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Value)
                    Case rowIncludeDivision, rowExcludeDivision
                        FHPGD_Division(Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Tag, Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Value)
                End Select

                Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
                For I As Integer = 0 To Dgl1.Rows.Count - 1
                    If AgL.XNull(Dgl1.Item(Col1Value, I).Value) <> "" Then
                        If Dgl1.Item(Col1Value, I).Value.ToString.Length < 500 Then
                            Dgl1.AutoResizeRow(I, DataGridViewAutoSizeRowMode.AllCells)
                        Else
                            Dgl1.Rows(I).Height = 50
                        End If
                    End If
                Next
            End If
        End If
    End Sub
    Private Sub FHPGD_ItemCategory(ByRef bTag As String, ByRef bValue As String)
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtTemp As DataTable

        mQry = " SELECT 'o' As Tick, Code AS Code, Description As Name FROM ItemCategory "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 400, 420, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            bTag = FRH_Multiple.FFetchData(1, "", "", ",", True)
            bValue = FRH_Multiple.FFetchData(2, "", "", ",", True)
        End If
        FRH_Multiple = Nothing
    End Sub
    Private Sub FHPGD_ItemGroup(ByRef bTag As String, ByRef bValue As String)
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtTemp As DataTable

        mQry = " SELECT 'o' As Tick, Code AS Code, Description As Name FROM ItemGroup "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 400, 420, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            bTag = FRH_Multiple.FFetchData(1, "", "", ",", True)
            bValue = FRH_Multiple.FFetchData(2, "", "", ",", True)
        End If
        FRH_Multiple = Nothing
    End Sub
    Private Sub FHPGD_Item(ByRef bTag As String, ByRef bValue As String)
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtTemp As DataTable

        mQry = " SELECT 'o' As Tick, Code AS Code, Description As Name FROM Item "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 400, 420, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            bTag = FRH_Multiple.FFetchData(1, "", "", ",", True)
            bValue = FRH_Multiple.FFetchData(2, "", "", ",", True)
        End If
        FRH_Multiple = Nothing
    End Sub
    Private Sub FHPGD_Party(ByRef bTag As String, ByRef bValue As String)
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtTemp As DataTable

        mQry = " SELECT 'o' As Tick, Code AS Code, Name FROM ViewHelpSubgroup Where SubGroupType = '" & SubgroupType.Customer & "' "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 500, 520, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Name", 400, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            bTag = FRH_Multiple.FFetchData(1, "", "", ",", True)
            bValue = FRH_Multiple.FFetchData(2, "", "", ",", True)
        End If
        FRH_Multiple = Nothing
    End Sub
    Private Sub FHPGD_Site(ByRef bTag As String, ByRef bValue As String)
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtTemp As DataTable

        mQry = " SELECT 'o' As Tick, Code AS Code, Name FROM SiteMast "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 400, 420, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            bTag = FRH_Multiple.FFetchData(1, "", "", ",", True)
            bValue = FRH_Multiple.FFetchData(2, "", "", ",", True)
        End If
        FRH_Multiple = Nothing
    End Sub
    Private Sub FHPGD_Division(ByRef bTag As String, ByRef bValue As String)
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtTemp As DataTable

        mQry = " SELECT 'o' As Tick, Div_Code AS Code, Div_Name As Name FROM Division "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 400, 420, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            bTag = FRH_Multiple.FFetchData(1, "", "", ",", True)
            bValue = FRH_Multiple.FFetchData(2, "", "", ",", True)
        End If
        FRH_Multiple = Nothing
    End Sub
    Private Sub FrmScheme_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        mQry = " SELECT DocId FROM SchemeQulified L WHERE L.[Scheme] = '" & mSearchCode & "'  "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            MsgBox("Scheme is already processed for some invoices.Can't Delete it.", MsgBoxStyle.Information) : Passed = False : Exit Sub
        End If
    End Sub
End Class
