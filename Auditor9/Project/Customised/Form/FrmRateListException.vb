Imports System.IO
Imports AgLibrary.ClsMain.agConstants
Imports System.Xml
Imports Customised.ClsMain
Imports System.ComponentModel
Imports System.Linq
Public Class FrmRateListException
    Inherits AgTemplate.TempMaster
    Dim mQry$ = ""
    Dim mIsReturnValue As Boolean = False

    Public Const ColSNo As String = "S.No."
    Public WithEvents DglMain As New AgControls.AgDataGrid
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1BtnDetail As String = "Detail"
    Public Const Col1HeadOriginal As String = "Head Original"
    Public Const Col1LastValue As String = "Last Value"

    Public Const rowWef As Integer = 0
    Public Const rowProcess As Integer = 1
    Public Const rowRateCategory As Integer = 2
    Public Const rowItemCategory As Integer = 3
    Public Const rowItemGroup As Integer = 4
    Public Const rowItem As Integer = 5
    Public Const rowDimension1 As Integer = 6
    Public Const rowDimension2 As Integer = 7
    Public Const rowDimension3 As Integer = 8
    Public Const rowDimension4 As Integer = 9
    Public Const rowSize As Integer = 10
    Public Const rowParty As Integer = 11
    Public Const rowRateType As Integer = 12
    Public Const rowBtnFill As Integer = 13
    Public Const rowBtnAttachments As Integer = 14


    Public Const hcWEF As String = "W.E.F."
    Public Const hcProcess As String = "Process"
    Public Const hcRateCategory As String = "Rate Category"
    Public Const hcItemCategory As String = "Item Category"
    Public Const hcItemGroup As String = "Item Group"
    Public Const hcItem As String = "Item"
    Public Const hcDimension1 As String = "Dimension 1"
    Public Const hcDimension2 As String = "Dimension 2"
    Public Const hcDimension3 As String = "Dimension 3"
    Public Const hcDimension4 As String = "Dimension 4"
    Public Const hcSize As String = "Size"
    Public Const hcParty As String = "Party"
    Public Const hcRateType As String = "RateType"
    Public Const hcBtnFill As String = "Fill"
    Public Const HcBtnAttachments As String = "Attachments"

    Public Const Col1Process As String = "Process"
    Public Const Col1Party As String = "Party"
    Public Const Col1RateType As String = "Rate Type"
    Public Const Col1ItemCategory As String = "Item Category"
    Public Const Col1ItemGroup As String = "Item Group"
    Public Const Col1Item As String = "Item"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1Dimension3 As String = "Dimension3"
    Public Const Col1Dimension4 As String = "Dimension4"
    Public Const Col1Size As String = "Size"
    Public Const Col1Rate As String = "Rate"
    Public Const Col1BtnLineDetail As String = "Line Detail"
    Public Const Col1BtnConsumption As String = "Consumption"
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
        Me.PnlHead.Size = New System.Drawing.Size(972, 170)
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
        'Pnl1
        '
        Me.Pnl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl1.Location = New System.Drawing.Point(2, 223)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(972, 335)
        Me.Pnl1.TabIndex = 1020
        '
        'FrmRateListException
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(974, 612)
        Me.ContextMenuStrip = Me.MnuOptions
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.PnlHead)
        Me.MaximizeBox = True
        Me.Name = "FrmRateListException"
        Me.Text = "Buyer Master"
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.PnlHead, 0)
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
    Friend WithEvents PnlHead As Panel
    Public WithEvents Pnl1 As Panel
#End Region

    Private Sub FrmShade_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        'AgL.PubFindQry = " SELECT Code AS SearchCode, WEF As WEF
        '                FROM RateList H 
        '                Where H.GenDocID Is Null "

        AgL.PubFindQry = " Select H.Code As SearchCode, H.WEF, P.Name As Process, Sg.Name As Party, IC.Description As ItemCategory, Ig.Description As ItemGroup,
                I.Description AS Item,
                D1.Description As " & AgL.PubCaptionDimension1 & ", D2.Description As " & AgL.PubCaptionDimension2 & ", 
                D3.Description AS " & AgL.PubCaptionDimension3 & ", D4.Description As " & AgL.PubCaptionDimension4 & ",
                S.Description AS Size, Rt.Description As RateType, L.Rate
                From RateList H 
                Left Join RateListDetail L ON H.Code = L.Code
                Left Join SubGroup P On L.Process = P.SubCode
                Left Join SubGroup Sg On L.SubCode = Sg.SubCode
                Left Join ItemCategory IC  With (NoLock) On L.ItemCategory = IC.Code 
                Left Join ItemGroup IG  With (NoLock) On L.ItemGroup = IG.Code 
                Left Join Item I On L.Item = I.Code
                Left Join Dimension1 D1 On L.Dimension1 = D1.Code
                Left Join Dimension2 D2 On L.Dimension2 = D2.Code
                Left Join Dimension3 D3 On L.Dimension3 = D3.Code
                Left Join Dimension4 D4 On L.Dimension4 = D4.Code
                Left Join Size S On L.Size = S.Code
                Left Join RateType Rt On L.RateType = Rt.Code
                WHERE H.GenDocId Is NULL 
                And H.V_Type = 'RTLE'"
        AgL.PubFindQryOrdBy = "[SearchCode]"
    End Sub

    Private Sub FrmShade_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "RateList"
        MainLineTableCsv = "RateListDetail"
    End Sub

    Private Sub FrmShade_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        mQry = "Select S.Code As SearchCode From RateList S Where GenDocID Is Null "
        mQry += " And V_Type = 'RTLE' "
        mQry += " Order by S.Code "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub
    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        mQry = "UPDATE RateList " &
                " SET " &
                " V_Type = 'RTLE', " &
                " WEF = " & AgL.Chk_Date(DglMain(Col1Value, rowWef).Value) & ", " &
                " RateCategory = " & AgL.Chk_Text(DglMain(Col1Value, rowRateCategory).Value) & " " &
                " Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "DELETE FROM RateListDetail WHERE Code = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From ItemRelation Where Code = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        For I As Integer = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1BtnLineDetail, I).Tag IsNot Nothing Then
                CType(Dgl1.Item(Col1BtnLineDetail, I).Tag, FrmRateListExceptionDetail).FPostRateListDetail(mSearchCode, Conn, Cmd)
            End If

            If Dgl1.Item(Col1BtnConsumption, I).Tag IsNot Nothing Then
                CType(Dgl1.Item(Col1BtnConsumption, I).Tag, FrmConsumptionException).FPostConsumptionException(Conn, Cmd)
            End If
        Next

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & DglMain.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, DglMain)
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        End If
        'Call FPostRateListDetail(Conn, Cmd)
    End Sub
    Private Sub FPostRateListDetail(ByVal Conn As Object, ByVal Cmd As Object)
        Dim bRateListCode$ = ""
        Dim I As Integer, mSr As Integer

        mSr = AgL.Dman_Execute("Select IfNull(Max(Sr),0) From RateListDetail With (NoLock) Where Code = '" & mSearchCode & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()

        For I = 0 To Dgl1.RowCount - 1
            If Val(Dgl1.Item(Col1Rate, I).Value) <> 0 Then
                mSr += 1
                mQry = "INSERT INTO RateListDetail(Code, Sr, Process, SubCode, RateType, ItemCategory, ItemGroup, Item, 
                        Dimension1, Dimension2, Dimension3, Dimension4, Size, Rate) 
                        VALUES(" & AgL.Chk_Text(mSearchCode) & ", 
                        " & mSr & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1Process, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Party, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1RateType, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Dimension4, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Size, I).Tag) & ",
                        " & Val(Dgl1.Item(Col1Rate, I).Value) & ") "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If

            For J As Integer = 0 To Dgl1.Columns.Count - 1
                If AgL.XNull(Dgl1.Columns(J).Tag) <> "" And Dgl1.Columns(J).HeaderText.Contains("Rate") Then
                    If Val(Dgl1.Item(J, I).Value) > 0 Then
                        mSr += 1
                        mQry = "INSERT INTO RateListDetail(Code, Sr, Process, SubCode, RateType, ItemCategory, ItemGroup, Item, 
                        Dimension1, Dimension2, Dimension3, Dimension4, Size, Rate) 
                        VALUES(" & AgL.Chk_Text(mSearchCode) & ", 
                        " & mSr & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1Process, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Party, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Columns(J).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1ItemCategory, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1ItemGroup, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Dimension3, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Dimension4, I).Tag) & ",
                        " & AgL.Chk_Text(Dgl1.Item(Col1Size, I).Tag) & ",
                        " & Val(Dgl1.Item(J, I).Value) & ") "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                End If
            Next


            Dim mCode As String = AgL.GetMaxId("ItemRelation", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
            mQry = " INSERT INTO ItemRelation(Code, Item, RelatedItem)
                    Select '" & mCode & "', " & AgL.Chk_Text(DglMain.Item(Col1Value, rowDimension3).Tag) & ", 
                    " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ""
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        Next
    End Sub
    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DtTemp As DataTable
        Dim I As Integer

        mQry = "Select S.* 
                From RateList S 
                Where S.Code='" & SearchCode & "'"
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        With DtTemp
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                DglMain.Item(Col1Value, rowWef).Value = AgL.XNull(.Rows(0)("WEF"))
                DglMain.Item(Col1Value, rowRateCategory).Value = AgL.XNull(.Rows(0)("RateCategory"))
            End If
        End With

        If AgL.Dman_Execute("Select Count(Distinct Process) From RateListDetail Where Code = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar() = 1 Then
            mQry = " Select L.Process,P.Name As ProcessName
                    From RateListDetail L 
                    LEFT JOIN SubGroup P On L.Process = P.SubCode
                    Where L.Code = '" & mSearchCode & "'"
            Dim DtProcess As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtProcess.Rows.Count > 0 Then
                DglMain.Item(Col1Value, rowProcess).Tag = AgL.XNull(DtProcess.Rows(0)("Process"))
                DglMain.Item(Col1Value, rowProcess).Value = AgL.XNull(DtProcess.Rows(0)("ProcessName"))
            End If
        End If

        FMovRecLineForMultipleLineProcess()

        'ApplyUISetting()
        'mQry = " SELECT Rt.Code As RateTypeCode, Rt.Description AS RateType
        '        FROM RateTypeProcess Rtp
        '        LEFT JOIN RateType Rt ON Rtp.Code = Rt.Code
        '        WHERE Rtp.Process = '" & DglMain.Item(Col1Value, rowProcess).Tag & "' "
        'Dim DtRateTypeForProcess As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        'If DtRateTypeForProcess.Rows.Count > 0 Then
        '    FMovRecLineForMultipleRateTypeProcess()
        'Else
        '    mQry = "Select P.Name As ProcessName, Ic.Description As ItemCategoryDesc, Ig.Description As ItemGroupDesc, I.Description As ItemDesc, 
        '        D1.Description As Dimension1Desc, D2.Description As Dimension2Desc,
        '        D3.Description As Dimension3Desc, D4.Description As Dimension4Desc,
        '        S.Description As SizeDesc, Rt.Description As RateTypeDesc, Sg.Name As PartyName, L.* 
        '        From RateListDetail L
        '        LEFT JOIN SubGroup P On L.Process = P.SubCode
        '        LEFT JOIN SubGroup Sg On L.SubCode = Sg.SubCode
        '        Left Join ItemCategory IC  With (NoLock) On L.ItemCategory = IC.Code 
        '        Left Join ItemGroup IG  With (NoLock) On L.ItemGroup = IG.Code 
        '        LEFT JOIN Item I On L.Item = I.Code
        '        LEFT JOIN Dimension1 D1 On L.Dimension1 = D1.Code
        '        LEFT JOIN Dimension2 D2 On L.Dimension2 = D2.Code
        '        LEFT JOIN Dimension3 D3 On L.Dimension3 = D3.Code
        '        LEFT JOIN Dimension4 D4 On L.Dimension4 = D4.Code
        '        LEFT JOIN Size S On L.Size = S.Code
        '        LEFT JOIN RateType Rt On L.RateType = Rt.Code
        '        Where L.Code = '" & mSearchCode & "'"
        '    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        '    With DtTemp
        '        Dgl1.RowCount = 1
        '        Dgl1.Rows.Clear()
        '        If .Rows.Count > 0 Then
        '            For I = 0 To DtTemp.Rows.Count - 1
        '                Dgl1.Rows.Add()
        '                Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
        '                Dgl1.Item(Col1Process, I).Tag = AgL.XNull(.Rows(I)("Process"))
        '                Dgl1.Item(Col1Process, I).Value = AgL.XNull(.Rows(I)("ProcessName"))
        '                Dgl1.Item(Col1Party, I).Tag = AgL.XNull(.Rows(I)("SubCode"))
        '                Dgl1.Item(Col1Party, I).Value = AgL.XNull(.Rows(I)("PartyName"))
        '                Dgl1.Item(Col1RateType, I).Tag = AgL.XNull(.Rows(I)("RateType"))
        '                Dgl1.Item(Col1RateType, I).Value = AgL.XNull(.Rows(I)("RateTypeDesc"))
        '                Dgl1.Item(Col1ItemCategory, I).Tag = AgL.XNull(.Rows(I)("ItemCategory"))
        '                Dgl1.Item(Col1ItemCategory, I).Value = AgL.XNull(.Rows(I)("ItemCategoryDesc"))
        '                Dgl1.Item(Col1ItemGroup, I).Tag = AgL.XNull(.Rows(I)("ItemGroup"))
        '                Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupDesc"))
        '                Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
        '                Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
        '                Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
        '                Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))
        '                Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
        '                Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))
        '                Dgl1.Item(Col1Dimension3, I).Tag = AgL.XNull(.Rows(I)("Dimension3"))
        '                Dgl1.Item(Col1Dimension3, I).Value = AgL.XNull(.Rows(I)("Dimension3Desc"))
        '                Dgl1.Item(Col1Dimension4, I).Tag = AgL.XNull(.Rows(I)("Dimension4"))
        '                Dgl1.Item(Col1Dimension4, I).Value = AgL.XNull(.Rows(I)("Dimension4Desc"))
        '                Dgl1.Item(Col1Size, I).Tag = AgL.XNull(.Rows(I)("Size"))
        '                Dgl1.Item(Col1Size, I).Value = AgL.XNull(.Rows(I)("SizeDesc"))
        '                Dgl1.Item(Col1Rate, I).Value = AgL.XNull(.Rows(I)("Rate"))
        '            Next I
        '        End If
        '    End With
        'End If

        If AgL.Dman_Execute("Select Count(Distinct SubCode) From RateListDetail Where Code = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar() = 1 Then
            DglMain.Item(Col1Value, rowParty).Tag = Dgl1.Item(Col1Party, 0).Tag
            DglMain.Item(Col1Value, rowParty).Value = Dgl1.Item(Col1Party, 0).Value
        End If

        If AgL.Dman_Execute("Select Count(Distinct ItemCategory) From RateListDetail Where Code = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar() = 1 Then
            DglMain.Item(Col1Value, rowItemCategory).Tag = Dgl1.Item(Col1ItemCategory, 0).Tag
            DglMain.Item(Col1Value, rowItemCategory).Value = Dgl1.Item(Col1ItemCategory, 0).Value
        End If

        If AgL.Dman_Execute("Select Count(Distinct ItemGroup) From RateListDetail Where Code = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar() = 1 Then
            DglMain.Item(Col1Value, rowItemGroup).Tag = Dgl1.Item(Col1ItemGroup, 0).Tag
            DglMain.Item(Col1Value, rowItemGroup).Value = Dgl1.Item(Col1ItemGroup, 0).Value
        End If

        If AgL.Dman_Execute("Select Count(Distinct Item) From RateListDetail Where Code = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar() = 1 Then
            DglMain.Item(Col1Value, rowItem).Tag = Dgl1.Item(Col1Item, 0).Tag
            DglMain.Item(Col1Value, rowItem).Value = Dgl1.Item(Col1Item, 0).Value
        End If

        If AgL.Dman_Execute("Select Count(Distinct Dimension1) From RateListDetail Where Code = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar() = 1 Then
            DglMain.Item(Col1Value, rowDimension1).Tag = Dgl1.Item(Col1Dimension1, 0).Tag
            DglMain.Item(Col1Value, rowDimension1).Value = Dgl1.Item(Col1Dimension1, 0).Value
        End If

        If AgL.Dman_Execute("Select Count(Distinct Dimension2) From RateListDetail Where Code = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar() = 1 Then
            DglMain.Item(Col1Value, rowDimension2).Tag = Dgl1.Item(Col1Dimension2, 0).Tag
            DglMain.Item(Col1Value, rowDimension2).Value = Dgl1.Item(Col1Dimension2, 0).Value
        End If

        If AgL.Dman_Execute("Select Count(Distinct Dimension3) From RateListDetail Where Code = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar() = 1 Then
            DglMain.Item(Col1Value, rowDimension3).Tag = Dgl1.Item(Col1Dimension3, 0).Tag
            DglMain.Item(Col1Value, rowDimension3).Value = Dgl1.Item(Col1Dimension3, 0).Value
        End If

        If AgL.Dman_Execute("Select Count(Distinct Dimension4) From RateListDetail Where Code = '" & mSearchCode & "'", AgL.GCn).ExecuteScalar() = 1 Then
            DglMain.Item(Col1Value, rowDimension4).Tag = Dgl1.Item(Col1Dimension4, 0).Tag
            DglMain.Item(Col1Value, rowDimension4).Value = Dgl1.Item(Col1Dimension4, 0).Value
        End If

        SetAttachmentCaption()
        Topctrl1.tPrn = False

        AgCL.GridSetiingShowXml(Me.Text & DglMain.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, DglMain, False)
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub
    Private Sub FMovRecLineForMultipleRateTypeProcess()
        Dim DtTemp As DataTable
        Dim I As Integer = 0

        mQry = "Select L.SubCode, L.ItemCategory, L.ItemGroup, L.Item, L.Dimension1, L.Dimension2, L.Dimension3, L.Dimension4, L.Size,
                        Max(Sg.Name) As PartyName, Max(Ic.Description) As ItemCategoryDesc, Max(Ig.Description) As ItemGroupDesc, Max(I.Description) As ItemDesc, 
                        Max(D1.Description) As Dimension1Desc, Max(D2.Description) As Dimension2Desc,
                        Max(D3.Description) As Dimension3Desc, Max(D4.Description) As Dimension4Desc,
                        Max(S.Description) As SizeDesc, Max(L.Sr) As Sr
                        From RateListDetail L
                        LEFT JOIN SubGroup Sg On L.SubCode = Sg.SubCode
                        Left Join ItemCategory IC  With (NoLock) On L.ItemCategory = IC.Code 
                        Left Join ItemGroup IG  With (NoLock) On L.ItemGroup = IG.Code 
                        LEFT JOIN Item I On L.Item = I.Code
                        LEFT JOIN Dimension1 D1 On L.Dimension1 = D1.Code
                        LEFT JOIN Dimension2 D2 On L.Dimension2 = D2.Code
                        LEFT JOIN Dimension3 D3 On L.Dimension3 = D3.Code
                        LEFT JOIN Dimension4 D4 On L.Dimension4 = D4.Code
                        LEFT JOIN Size S On L.Size = S.Code
                        Where L.Code = '" & mSearchCode & "'
                        Group By L.SubCode, L.ItemCategory, L.ItemGroup, L.Item, L.Dimension1, L.Dimension2, L.Dimension3, L.Dimension4, L.Size 
                        Order By Sr "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        With DtTemp
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                    Dgl1.Item(Col1Party, I).Tag = AgL.XNull(.Rows(I)("SubCode"))
                    Dgl1.Item(Col1Party, I).Value = AgL.XNull(.Rows(I)("PartyName"))
                    Dgl1.Item(Col1ItemCategory, I).Tag = AgL.XNull(.Rows(I)("ItemCategory"))
                    Dgl1.Item(Col1ItemCategory, I).Value = AgL.XNull(.Rows(I)("ItemCategoryDesc"))
                    Dgl1.Item(Col1ItemGroup, I).Tag = AgL.XNull(.Rows(I)("ItemGroup"))
                    Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupDesc"))
                    Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                    Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                    Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                    Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))
                    Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                    Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))
                    Dgl1.Item(Col1Dimension3, I).Tag = AgL.XNull(.Rows(I)("Dimension3"))
                    Dgl1.Item(Col1Dimension3, I).Value = AgL.XNull(.Rows(I)("Dimension3Desc"))
                    Dgl1.Item(Col1Dimension4, I).Tag = AgL.XNull(.Rows(I)("Dimension4"))
                    Dgl1.Item(Col1Dimension4, I).Value = AgL.XNull(.Rows(I)("Dimension4Desc"))
                    Dgl1.Item(Col1Size, I).Tag = AgL.XNull(.Rows(I)("Size"))
                    Dgl1.Item(Col1Size, I).Value = AgL.XNull(.Rows(I)("SizeDesc"))

                    mQry = " Select L.RateType As RateTypeCode, Rt.Description As RateType, L.Rate
                                From RateListDetail L 
                                LEFT JOIN RateType Rt On L.RateType = Rt.Code
                                Where L.Code = '" & mSearchCode & "'
                                And IsNull(L.SubCode,'') = '" & Dgl1.Item(Col1Party, I).Tag & "'
                                And IsNull(L.ItemCategory,'') = '" & Dgl1.Item(Col1ItemCategory, I).Tag & "'
                                And IsNull(L.ItemGroup,'') = '" & Dgl1.Item(Col1ItemGroup, I).Tag & "'
                                And IsNull(L.Item,'') = '" & Dgl1.Item(Col1Item, I).Tag & "'
                                And IsNull(L.Dimension1,'') = '" & Dgl1.Item(Col1Dimension1, I).Tag & "'
                                And IsNull(L.Dimension2,'') = '" & Dgl1.Item(Col1Dimension2, I).Tag & "'
                                And IsNull(L.Dimension3,'') = '" & Dgl1.Item(Col1Dimension3, I).Tag & "'
                                And IsNull(L.Dimension4,'') = '" & Dgl1.Item(Col1Dimension4, I).Tag & "'
                                And IsNull(L.Size,'') = '" & Dgl1.Item(Col1Size, I).Tag & "' "
                    Dim DtRateTypes As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

                    For J As Integer = 0 To DtRateTypes.Rows.Count - 1
                        If AgL.XNull(DtRateTypes.Rows(J)("RateType")) <> "" Then
                            Dgl1.Item(Col1Rate + " " + AgL.XNull(DtRateTypes.Rows(J)("RateType")), I).Value = AgL.VNull(DtRateTypes.Rows(J)("Rate"))
                        Else
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(DtRateTypes.Rows(J)("Rate"))
                        End If
                    Next
                Next I
            End If
        End With
    End Sub
    Private Sub FMovRecLineForMultipleLineProcess()
        Dim DtTemp As DataTable
        Dim I As Integer = 0

        mQry = "Select L.SubCode, L.ItemCategory, L.ItemGroup, L.Item, L.Dimension1, L.Dimension2, L.Dimension3, L.Dimension4, L.Size,
                        Max(Sg.Name) As PartyName, Max(Ic.Description) As ItemCategoryDesc, Max(Ig.Description) As ItemGroupDesc, Max(I.Description) As ItemDesc, 
                        Max(D1.Description) As Dimension1Desc, Max(D2.Description) As Dimension2Desc,
                        Max(D3.Description) As Dimension3Desc, Max(D4.Description) As Dimension4Desc,
                        Max(S.Description) As SizeDesc, Max(L.Sr) As Sr
                        From RateListDetail L
                        LEFT JOIN SubGroup Sg On L.SubCode = Sg.SubCode
                        Left Join ItemCategory IC  With (NoLock) On L.ItemCategory = IC.Code 
                        Left Join ItemGroup IG  With (NoLock) On L.ItemGroup = IG.Code 
                        LEFT JOIN Item I On L.Item = I.Code
                        LEFT JOIN Dimension1 D1 On L.Dimension1 = D1.Code
                        LEFT JOIN Dimension2 D2 On L.Dimension2 = D2.Code
                        LEFT JOIN Dimension3 D3 On L.Dimension3 = D3.Code
                        LEFT JOIN Dimension4 D4 On L.Dimension4 = D4.Code
                        LEFT JOIN Size S On L.Size = S.Code
                        Where L.Code = '" & mSearchCode & "'
                        Group By L.SubCode, L.ItemCategory, L.ItemGroup, L.Item, L.Dimension1, L.Dimension2, L.Dimension3, L.Dimension4, L.Size 
                        Order By Sr "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        With DtTemp
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                    Dgl1.Item(Col1Party, I).Tag = AgL.XNull(.Rows(I)("SubCode"))
                    Dgl1.Item(Col1Party, I).Value = AgL.XNull(.Rows(I)("PartyName"))
                    Dgl1.Item(Col1ItemCategory, I).Tag = AgL.XNull(.Rows(I)("ItemCategory"))
                    Dgl1.Item(Col1ItemCategory, I).Value = AgL.XNull(.Rows(I)("ItemCategoryDesc"))
                    Dgl1.Item(Col1ItemGroup, I).Tag = AgL.XNull(.Rows(I)("ItemGroup"))
                    Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupDesc"))
                    Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                    Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                    Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                    Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))
                    Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                    Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))
                    Dgl1.Item(Col1Dimension3, I).Tag = AgL.XNull(.Rows(I)("Dimension3"))
                    Dgl1.Item(Col1Dimension3, I).Value = AgL.XNull(.Rows(I)("Dimension3Desc"))
                    Dgl1.Item(Col1Dimension4, I).Tag = AgL.XNull(.Rows(I)("Dimension4"))
                    Dgl1.Item(Col1Dimension4, I).Value = AgL.XNull(.Rows(I)("Dimension4Desc"))
                    Dgl1.Item(Col1Size, I).Tag = AgL.XNull(.Rows(I)("Size"))
                    Dgl1.Item(Col1Size, I).Value = AgL.XNull(.Rows(I)("SizeDesc"))

                    Dim FrmObj As FrmRateListExceptionDetail
                    FrmObj = New FrmRateListExceptionDetail
                    FrmObj.EntryMode = Topctrl1.Mode
                    FrmObj.DglRow = Dgl1.Rows(I)
                    FrmObj.IniGrid(mSearchCode)
                    Dgl1.Item(Col1BtnLineDetail, I).Tag = FrmObj
                Next I
            End If
        End With
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
        'Dgl1.CurrentCell = Dgl1(Col1Value, rowWef)
        'Dgl1.Focus()

    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Private Sub FrmParty_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        DglMain.CurrentCell = DglMain.FirstDisplayedCell
        DglMain.Focus()
    End Sub
    Private Sub FrmRateListCustom_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
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


        DglMain.Rows.Add(15)
        DglMain.Item(Col1Head, rowWef).Value = hcWEF
        DglMain.Item(Col1Head, rowRateCategory).Value = hcRateCategory
        DglMain.Item(Col1Head, rowProcess).Value = hcProcess
        DglMain.Item(Col1Head, rowParty).Value = hcParty
        DglMain.Item(Col1Head, rowItemCategory).Value = hcItemCategory
        DglMain.Item(Col1Head, rowItemGroup).Value = hcItemGroup
        DglMain.Item(Col1Head, rowItem).Value = hcItem
        DglMain.Item(Col1Head, rowDimension1).Value = IIf(AgL.PubCaptionDimension1 <> "", AgL.PubCaptionDimension1, hcDimension1)
        DglMain.Item(Col1Head, rowDimension2).Value = IIf(AgL.PubCaptionDimension1 <> "", AgL.PubCaptionDimension2, hcDimension2)
        DglMain.Item(Col1Head, rowDimension3).Value = IIf(AgL.PubCaptionDimension1 <> "", AgL.PubCaptionDimension3, hcDimension3)
        DglMain.Item(Col1Head, rowDimension4).Value = IIf(AgL.PubCaptionDimension1 <> "", AgL.PubCaptionDimension4, hcDimension4)
        DglMain.Item(Col1Head, rowSize).Value = hcSize
        DglMain.Item(Col1Head, rowBtnFill).Value = hcBtnFill
        DglMain.Item(Col1Value, rowBtnFill) = New DataGridViewButtonCell
        DglMain.Item(Col1Head, rowBtnAttachments).Value = HcBtnAttachments
        DglMain.Item(Col1Value, rowBtnAttachments) = New DataGridViewButtonCell


        For I = 0 To DglMain.Rows.Count - 1
            DglMain(Col1HeadOriginal, I).Value = DglMain(Col1Head, I).Value
        Next

        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Process, 100, 0, Col1Process, True, False)
            .AddAgTextColumn(Dgl1, Col1Party, 100, 0, Col1Party, True, False)
            .AddAgTextColumn(Dgl1, Col1RateType, 100, 0, Col1RateType, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemCategory, 200, 0, Col1ItemCategory, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 200, 0, Col1ItemGroup, True, False)
            .AddAgTextColumn(Dgl1, Col1Item, 400, 0, Col1Item, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, Col1Dimension1, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, Col1Dimension2, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension3, 100, 0, Col1Dimension3, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension4, 100, 0, Col1Dimension4, True, False)
            .AddAgTextColumn(Dgl1, Col1Size, 100, 0, Col1Size, True, False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 80, 8, 2, False, Col1Rate, True, False, True)
            .AddAgButtonColumn(Dgl1, Col1BtnLineDetail, 30, " ")
            .AddAgButtonColumn(Dgl1, Col1BtnConsumption, 30, " ")
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.RowHeadersVisible = False
        DglMain.AllowUserToAddRows = False
        'Dgl1.Visible = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.BackgroundColor = Me.BackColor
        AgL.GridDesign(Dgl1)
        Dgl1.Name = "Dgl1"
        Dgl1.Anchor = AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom

        AgL.FSetDimensionCaptionForHorizontalGrid(Dgl1, AgL)
        ApplyUISetting()


    End Sub
    Private Sub AddRateTypeVariant(bProcess As String)
        mQry = " SELECT Rt.Code As RateTypeCode, Rt.Description AS RateType
                FROM RateTypeProcess Rtp
                LEFT JOIN RateType Rt ON Rtp.Code = Rt.Code
                WHERE Rtp.Process = '" & bProcess & "' "
        Dim DtRateTypeForProcess As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtRateTypeForProcess.Rows.Count > 0 Then
            'Dgl1.Columns(Col1Rate).Visible = False
            For I As Integer = 0 To DtRateTypeForProcess.Rows.Count - 1
                If Not Dgl1.Columns.Contains(Col1Rate + " " + AgL.XNull(DtRateTypeForProcess.Rows(I)("RateType"))) Then
                    AgCL.AddAgNumberColumn(Dgl1, Col1Rate + " " + AgL.XNull(DtRateTypeForProcess.Rows(I)("RateType")),
                        90, 8, 2, False, Col1Rate + " " + AgL.XNull(DtRateTypeForProcess.Rows(I)("RateType")),
                        True, False, True)
                    Dgl1.Columns(Col1Rate + " " + AgL.XNull(DtRateTypeForProcess.Rows(I)("RateType"))).Tag = AgL.XNull(DtRateTypeForProcess.Rows(I)("RateTypeCode"))
                Else
                    Dgl1.Columns(Col1Rate + " " + AgL.XNull(DtRateTypeForProcess.Rows(I)("RateType"))).visible = True
                End If
            Next
        End If
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

            Select Case DglMain.CurrentCell.RowIndex
                Case rowWef
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
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
                Case rowProcess
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Subcode AS Code, Name  FROM Subgroup WHERE SubgroupType = '" & SubgroupType.Process & "' 
                                    And IfNull(Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'"
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowParty
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = FCreateHelpSubgroup()
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value, 0) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowRateCategory
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT 'Rate Addition' As Code, 'Rate Addition' As Description "
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowItemCategory
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = FCreateHelpItemCategory()
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value, 0) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowItemGroup
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = FCreateHelpItemGroup()
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value, 0) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowItem
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = FCreateHelpItem()
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value, 0) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowDimension1
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = FCreateHelpDimension1()
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value, 0) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowDimension2
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = FCreateHelpDimension2()
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value, 0) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowDimension3
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = FCreateHelpDimension3()
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value, 0) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowDimension4
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = FCreateHelpDimension4()
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value, 0) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowSize
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = FCreateHelpSize()
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value, 0) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmRateListCustom_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
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
    Private Sub FrmRateListCustom_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
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

        If AgL.VNull(AgL.Dman_Execute("SELECT Count(*) AS Cnt
                        FROM RateList H 
                        LEFT JOIN RateListDetail L ON H.Code = L.Code
                        WHERE H.V_Type = 'RTLE'
                        AND L.Dimension3 =  '" & DglMain.Item(Col1Value, rowDimension3).Tag & "'
                        AND H.Code <> '" & mSearchCode & "'", AgL.GCn).ExecuteScalar()) > 0 Then
            MsgBox("Rates already exist for " & DglMain.Item(Col1Value, rowDimension3).Value, MsgBoxStyle.Information)
            DglMain.CurrentCell = DglMain(Col1Value, rowDimension3)
            DglMain.Focus()
            passed = False
            Exit Sub
        End If

        For I = 0 To Dgl1.Rows.Count - 1
            If I = 0 And Dgl1.Rows(I).Visible = True Then
                If AgL.XNull(Dgl1.Item(Col1Process, I).Value) = "" And
                    AgL.XNull(Dgl1.Item(Col1Party, I).Value) = "" And
                    AgL.XNull(Dgl1.Item(Col1RateType, I).Value) = "" And
                    AgL.XNull(Dgl1.Item(Col1ItemCategory, I).Value) = "" And
                    AgL.XNull(Dgl1.Item(Col1ItemGroup, I).Value) = "" And
                    AgL.XNull(Dgl1.Item(Col1Item, I).Value) = "" And
                    AgL.XNull(Dgl1.Item(Col1Dimension1, I).Value) = "" And
                    AgL.XNull(Dgl1.Item(Col1Dimension2, I).Value) = "" And
                    AgL.XNull(Dgl1.Item(Col1Dimension3, I).Value) = "" And
                    AgL.XNull(Dgl1.Item(Col1Dimension4, I).Value) = "" And
                    AgL.XNull(Dgl1.Item(Col1Size, I).Value) = "" And
                    AgL.VNull(Dgl1.Item(Col1Rate, I).Value) = 0 Then
                    MsgBox("No Data Entered To Save.", MsgBoxStyle.Information)
                    Dgl1.Focus()
                    passed = False
                    Exit Sub
                End If
            End If


            If AgL.XNull(Dgl1.Item(Col1Process, I).Value) = "" Then
                If AgL.XNull(DglMain.Item(Col1Value, rowProcess).Value) <> "" Then
                    Dgl1.Item(Col1Process, I).Value = AgL.XNull(DglMain.Item(Col1Value, rowProcess).Value)
                    Dgl1.Item(Col1Process, I).Tag = AgL.XNull(DglMain.Item(Col1Value, rowProcess).Tag)
                End If
            End If

            If AgL.XNull(Dgl1.Item(Col1ItemCategory, I).Value) = "" Then
                If AgL.XNull(DglMain.Item(Col1Value, rowItemCategory).Value) <> "" Then
                    Dgl1.Item(Col1ItemCategory, I).Value = AgL.XNull(DglMain.Item(Col1Value, rowItemCategory).Value)
                    Dgl1.Item(Col1ItemCategory, I).Tag = AgL.XNull(DglMain.Item(Col1Value, rowItemCategory).Tag)
                End If
            End If

            If AgL.XNull(Dgl1.Item(Col1ItemGroup, I).Value) = "" Then
                If AgL.XNull(DglMain.Item(Col1Value, rowItemGroup).Value) <> "" Then
                    Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(DglMain.Item(Col1Value, rowItemGroup).Value)
                    Dgl1.Item(Col1ItemGroup, I).Tag = AgL.XNull(DglMain.Item(Col1Value, rowItemGroup).Tag)
                End If
            End If

            If AgL.XNull(Dgl1.Item(Col1Dimension1, I).Value) = "" Then
                If AgL.XNull(DglMain.Item(Col1Value, rowDimension1).Value) <> "" Then
                    Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(DglMain.Item(Col1Value, rowDimension1).Value)
                    Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(DglMain.Item(Col1Value, rowDimension1).Tag)
                End If
            End If

            If AgL.XNull(Dgl1.Item(Col1Dimension2, I).Value) = "" Then
                If AgL.XNull(DglMain.Item(Col1Value, rowDimension2).Value) <> "" Then
                    Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(DglMain.Item(Col1Value, rowDimension2).Value)
                    Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(DglMain.Item(Col1Value, rowDimension2).Tag)
                End If
            End If

            If AgL.XNull(Dgl1.Item(Col1Dimension3, I).Value) = "" Then
                If AgL.XNull(DglMain.Item(Col1Value, rowDimension3).Value) <> "" Then
                    Dgl1.Item(Col1Dimension3, I).Value = AgL.XNull(DglMain.Item(Col1Value, rowDimension3).Value)
                    Dgl1.Item(Col1Dimension3, I).Tag = AgL.XNull(DglMain.Item(Col1Value, rowDimension3).Tag)
                End If
            End If

            If AgL.XNull(Dgl1.Item(Col1Dimension4, I).Value) = "" Then
                If AgL.XNull(DglMain.Item(Col1Value, rowDimension4).Value) <> "" Then
                    Dgl1.Item(Col1Dimension4, I).Value = AgL.XNull(DglMain.Item(Col1Value, rowDimension4).Value)
                    Dgl1.Item(Col1Dimension4, I).Tag = AgL.XNull(DglMain.Item(Col1Value, rowDimension4).Tag)
                End If
            End If

            If AgL.XNull(Dgl1.Item(Col1Size, I).Value) = "" Then
                If AgL.XNull(DglMain.Item(Col1Value, rowSize).Value) <> "" Then
                    Dgl1.Item(Col1Size, I).Value = AgL.XNull(DglMain.Item(Col1Value, rowSize).Value)
                    Dgl1.Item(Col1Size, I).Tag = AgL.XNull(DglMain.Item(Col1Value, rowSize).Tag)
                End If
            End If


            If AgL.XNull(Dgl1.Item(Col1Party, I).Tag) = "" Then
                Dgl1.Item(Col1Party, I).Tag = DglMain.Item(Col1Value, rowParty).Tag
            End If
        Next

        If AgL.XNull(DglMain(Col1Value, rowRateCategory).Value) = "" Then DglMain(Col1Value, rowRateCategory).Value = RateCategory.RateAddition
    End Sub
    Private Sub DglMain_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles DglMain.EditingControl_Validating
        Try
            If DglMain.CurrentCell IsNot Nothing Then
                If DglMain.CurrentCell.ColumnIndex = DglMain.Columns(Col1Value).Index Then
                    Select Case DglMain.CurrentCell.RowIndex
                        Case rowProcess
                            ApplyUISetting()
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
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmRateListCustom_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Dim I As Integer
        For I = 0 To DglMain.Rows.Count - 1
            DglMain(Col1Head, I).Tag = Nothing
        Next
    End Sub
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Party
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Party) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Party) = FCreateHelpSubgroup()
                        End If
                    End If

                Case Col1RateType
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1RateType) Is Nothing Then
                            mQry = " Select H.Code, H.Description From RateType H Order By H.Description "
                            Dgl1.AgHelpDataSet(Col1RateType) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1Item
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Item) = FCreateHelpItem()
                        End If
                    End If

                Case Col1ItemCategory
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If Dgl1.AgHelpDataSet(Col1ItemCategory) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1ItemCategory) = FCreateHelpItemCategory()
                        End If
                    End If

                Case Col1ItemGroup
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If Dgl1.AgHelpDataSet(Col1ItemGroup) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1ItemGroup) = FCreateHelpItemGroup()
                        End If
                    End If

                Case Col1Dimension1
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension1) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Dimension1) = FCreateHelpDimension1()
                        End If
                    End If

                Case Col1Dimension2
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension2) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Dimension2) = FCreateHelpDimension2()
                        End If
                    End If

                Case Col1Dimension3
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension3) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Dimension3) = FCreateHelpDimension3()
                        End If
                    End If

                Case Col1Dimension4
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension4) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Dimension4) = FCreateHelpDimension4()
                        End If
                    End If

                Case Col1Size
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Size) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Size) = FCreateHelpSize()
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function FCreateHelpItem() As DataSet
        Dim strCond As String = ""
        Dim FilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If FilterInclude_ItemType <> "" Then
            strCond += " And CharIndex('+' || I.ItemType,'" & FilterInclude_ItemType & "') > 0 "
            strCond += " And CharIndex('-' || I.ItemType,'" & FilterInclude_ItemType & "') <= 0 "
        End If

        Dim FilterInclude_ItemCategory As String = FGetSettings(SettingFields.FilterInclude_ItemCategory, SettingType.General)
        If FilterInclude_ItemType <> "" Then
            strCond += " And CharIndex('+' || I.ItemCategory,'" & FilterInclude_ItemCategory & "') > 0 "
            strCond += " And CharIndex('-' || I.ItemCategory,'" & FilterInclude_ItemCategory & "') <= 0 "
        End If

        Dim FilterInclude_ItemGroup As String = FGetSettings(SettingFields.FilterInclude_ItemGroup, SettingType.General)
        If FilterInclude_ItemType <> "" Then
            strCond += " And CharIndex('+' || I.ItemGroup,'" & FilterInclude_ItemGroup & "') > 0 "
            strCond += " And CharIndex('-' || I.ItemGroup,'" & FilterInclude_ItemGroup & "') <= 0 "
        End If

        Dim FilterInclude_Item As String = FGetSettings(SettingFields.FilterInclude_Item, SettingType.General)
        If FilterInclude_ItemType <> "" Then
            strCond += " And CharIndex('+' || I.Code,'" & FilterInclude_Item & "') > 0 "
            strCond += " And CharIndex('-' || I.Code,'" & FilterInclude_Item & "') <= 0 "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherDivisions")) Then
            strCond += " And (I.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(I.ShowItemInOtherDivisions,0) =1) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemsOfOtherSites")) Then
            strCond += " And (I.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(I.ShowItemInOtherSites,0) =1) "
        End If

        strCond += " And I.V_Type = '" & ItemV_Type.Item & "'"

        mQry = "SELECT I.Code, I.Description " &
                  " FROM Item I  With (NoLock) " &
                  " Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        'Dgl1.AgHelpDataSet(Col1Item) = AgL.FillData(mQry, AgL.GCn)
        Return AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Function FCreateHelpItemGroup() As DataSet
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        Dim FilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If FilterInclude_ItemType <> "" Then
            strCond += " And CharIndex('+' || I.ItemType,'" & FilterInclude_ItemType & "') > 0 "
            strCond += " And CharIndex('-' || I.ItemType,'" & FilterInclude_ItemType & "') <= 0 "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemGroupsOfOtherDivisions")) Then
            strCond += " And (IG.Div_Code = '" & AgL.PubDivCode & "' Or IfNull(IG.ShowItemGroupInOtherDivisions,0) =1) "
        End If

        If Not AgL.VNull(AgL.PubDtEnviro.Rows(0)("ShowItemGroupsOfOtherSites")) Then
            strCond += " And (IG.Site_Code = '" & AgL.PubSiteCode & "' Or IfNull(IG.ShowItemGroupInOtherSites,0) =1) "
        End If

        mQry = "SELECT I.Code, I.Description
                FROM ItemGroup I  With (NoLock)
                Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        'Dgl1.AgHelpDataSet(Col1ItemGroup) = AgL.FillData(mQry, AgL.GCn)
        Return AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Function FCreateHelpItemCategory() As DataSet
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        Dim FilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If FilterInclude_ItemType <> "" Then
            strCond += " And CharIndex('+' || I.ItemType,'" & FilterInclude_ItemType & "') > 0 "
            strCond += " And CharIndex('-' || I.ItemType,'" & FilterInclude_ItemType & "') <= 0 "
        End If

        mQry = "SELECT I.Code, I.Description
                        FROM ItemCategory I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        'Dgl1.AgHelpDataSet(Col1ItemCategory) = AgL.FillData(mQry, AgL.GCn)
        Return AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Function FCreateHelpDimension1() As DataSet
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        Dim FilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If FilterInclude_ItemType <> "" Then
            strCond += " And CharIndex('+' || I.ItemType,'" & FilterInclude_ItemType & "') > 0 "
            strCond += " And CharIndex('-' || I.ItemType,'" & FilterInclude_ItemType & "') <= 0 "
        End If

        mQry = "SELECT I.Code, I.Description
                        FROM Dimension1 I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        'Dgl1.AgHelpDataSet(Col1ItemCategory) = AgL.FillData(mQry, AgL.GCn)
        Return AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Function FCreateHelpDimension2() As DataSet
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        Dim FilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If FilterInclude_ItemType <> "" Then
            strCond += " And CharIndex('+' || I.ItemType,'" & FilterInclude_ItemType & "') > 0 "
            strCond += " And CharIndex('-' || I.ItemType,'" & FilterInclude_ItemType & "') <= 0 "
        End If

        mQry = "SELECT I.Code, I.Description
                        FROM Dimension2 I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        'Dgl1.AgHelpDataSet(Col1ItemCategory) = AgL.FillData(mQry, AgL.GCn)
        Return AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Function FCreateHelpDimension3() As DataSet
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        Dim FilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If FilterInclude_ItemType <> "" Then
            strCond += " And CharIndex('+' || I.ItemType,'" & FilterInclude_ItemType & "') > 0 "
            strCond += " And CharIndex('-' || I.ItemType,'" & FilterInclude_ItemType & "') <= 0 "
        End If

        mQry = "SELECT I.Code, I.Description
                        FROM Dimension3 I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        'Dgl1.AgHelpDataSet(Col1ItemCategory) = AgL.FillData(mQry, AgL.GCn)
        Return AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Function FCreateHelpDimension4() As DataSet
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        Dim FilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If FilterInclude_ItemType <> "" Then
            strCond += " And CharIndex('+' || I.ItemType,'" & FilterInclude_ItemType & "') > 0 "
            strCond += " And CharIndex('-' || I.ItemType,'" & FilterInclude_ItemType & "') <= 0 "
        End If

        mQry = "SELECT I.Code, I.Description
                        FROM Dimension4 I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        'Dgl1.AgHelpDataSet(Col1ItemCategory) = AgL.FillData(mQry, AgL.GCn)
        Return AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Function FCreateHelpSize() As DataSet
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        Dim FilterInclude_ItemType As String = FGetSettings(SettingFields.FilterInclude_ItemType, SettingType.General)
        If FilterInclude_ItemType <> "" Then
            strCond += " And CharIndex('+' || I.ItemType,'" & FilterInclude_ItemType & "') > 0 "
            strCond += " And CharIndex('-' || I.ItemType,'" & FilterInclude_ItemType & "') <= 0 "
        End If

        mQry = "SELECT I.Code, I.Description
                        FROM Size I  With (NoLock)
                        Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        'Dgl1.AgHelpDataSet(Col1ItemCategory) = AgL.FillData(mQry, AgL.GCn)
        Return AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Function FCreateHelpSubgroup() As DataSet
        Dim strCond As String = ""

        Dim FilterInclude_AcGroup As String = FGetSettings(SettingFields.FilterInclude_AcGroup, SettingType.General)
        If FilterInclude_AcGroup <> "" Then
            strCond += " And CharIndex('+' || Sg.GroupCode,'" & FilterInclude_AcGroup & "') > 0 "
            strCond += " And CharIndex('-' || Sg.GroupCode,'" & FilterInclude_AcGroup & "') <= 0 "
        End If

        Dim FilterInclude_SubgroupType As String = FGetSettings(SettingFields.FilterInclude_SubgroupType, SettingType.General)
        If FilterInclude_AcGroup <> "" Then
            strCond += " And CharIndex('+' || Sg.SubgroupType,'" & FilterInclude_SubgroupType & "') > 0 "
            strCond += " And CharIndex('-' || Sg.SubgroupType,'" & FilterInclude_SubgroupType & "') <= 0 "
        End If

        Dim FilterInclude_Nature As String = FGetSettings(SettingFields.FilterInclude_Nature, SettingType.General)
        If FilterInclude_AcGroup <> "" Then
            strCond += " And CharIndex('+' || Sg.Nature,'" & FilterInclude_Nature & "') > 0 "
            strCond += " And CharIndex('-' || Sg.Nature,'" & FilterInclude_Nature & "') <= 0 "
        End If

        strCond += " And Sg.Nature In ('" & ClsMain.SubGroupNature.Customer & "','" & ClsMain.SubGroupNature.Supplier & "','" & ClsMain.SubGroupNature.Bank & "')"

        mQry = "SELECT Sg.SubCode As Code, Sg.Name || ',' || IfNull(C.CityName,'') As Party " &
                " FROM Subgroup Sg  With (NoLock) " &
                " LEFT JOIN City C  With (NoLock) ON Sg.CityCode = C.CityCode  " &
                " Left Join AcGroup Ag  With (NoLock) on Sg.GroupCode = Ag.GroupCode " &
                " Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        'TxtParty.AgHelpDataSet(6, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
        Return AgL.FillData(mQry, AgL.GCn)
    End Function
    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, AgL.PubSiteCode, "", Ncat.RateList, "", DglMain.Item(Col1Value, rowProcess).Tag, "")
        FGetSettings = mValue
    End Function
    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub
    Private Sub ApplyUISetting()
        For I As Integer = 0 To DglMain.Rows.Count - 1
            DglMain.Rows(I).Visible = False
        Next

        For I As Integer = 0 To Dgl1.Columns.Count - 1
            Dgl1.Columns(I).Visible = False
        Next

        DglMain.Rows(rowWef).Visible = True
        DglMain.Rows(rowDimension3).Visible = True
        DglMain.Rows(rowBtnAttachments).Visible = True

        Dgl1.Columns(ColSNo).Visible = True
        Dgl1.Columns(Col1ItemCategory).Visible = True
        Dgl1.Columns(Col1Dimension1).Visible = True
        Dgl1.Columns(Col1Dimension2).Visible = True
        Dgl1.Columns(Col1BtnLineDetail).Visible = True
        Dgl1.Columns(Col1BtnConsumption).Visible = True
    End Sub
    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.visible = False
        End If
    End Sub
    Private Sub DglMain_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DglMain.CellContentClick
        Try
            If e.ColumnIndex = DglMain.Columns(Col1Value).Index And TypeOf (DglMain(Col1Value, e.RowIndex)) Is DataGridViewButtonCell Then
                Select Case e.RowIndex
                    Case rowBtnFill
                        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
                        If MsgBox("Do you want to fill ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                            FHPGD_FillCriteria()
                        End If

                    Case rowBtnAttachments
                        ShowAttachments()
                End Select
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Dim I As Integer
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            If Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name.Contains("Rate") Then
                If mRowIndex < Dgl1.Rows.Count - 2 Then
                    If MsgBox("Do you want to copy rate below ? ", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        For I = mRowIndex To Dgl1.Rows.Count - 1
                            If Dgl1.Item(Col1Party, I).Value <> "" Or
                                Dgl1.Item(Col1RateType, I).Value <> "" Or
                                Dgl1.Item(Col1ItemCategory, I).Value <> "" Or
                                Dgl1.Item(Col1ItemGroup, I).Value <> "" Or
                                Dgl1.Item(Col1Item, I).Value <> "" Or
                                Dgl1.Item(Col1Dimension1, I).Value <> "" Or
                                Dgl1.Item(Col1Dimension2, I).Value <> "" Or
                                Dgl1.Item(Col1Dimension3, I).Value <> "" Or
                                Dgl1.Item(Col1Dimension4, I).Value <> "" Or
                                Dgl1.Item(Col1Size, I).Value <> "" Then
                                Dgl1.Item(mColumnIndex, I).Value = Val(Dgl1.Item(mColumnIndex, mRowIndex).Value)
                            End If
                        Next
                    End If
                End If
            End If
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FHPGD_FillCriteria()
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtSelection As DataTable

        mQry = ""
        If Dgl1.Columns(Col1RateType).Visible = True Then
            mQry = " SELECT 'o' As Tick, 'RateType' AS Code, 'Rate Type' As Name "
        End If
        If Dgl1.Columns(Col1ItemCategory).Visible = True Then
            If mQry <> "" Then mQry += " UNION ALL "
            mQry += " SELECT 'o' As Tick, 'ItemCategory' AS Code, 'Item Category' As Name "
        End If
        If Dgl1.Columns(Col1ItemGroup).Visible = True Then
            If mQry <> "" Then mQry += " UNION ALL "
            mQry += " SELECT 'o' As Tick, 'ItemGroup' AS Code, 'Item Group' As Name "
        End If
        If Dgl1.Columns(Col1Item).Visible = True Then
            If mQry <> "" Then mQry += " UNION ALL "
            mQry += " SELECT 'o' As Tick, 'Item' AS Code, 'Code' As Name "
        End If
        If Dgl1.Columns(Col1Dimension1).Visible = True Then
            If mQry <> "" Then mQry += " UNION ALL "
            mQry += " SELECT 'o' As Tick, 'Dimension1' AS Code, '" & AgL.PubCaptionDimension1 & "' As Name "
        End If
        If Dgl1.Columns(Col1Dimension2).Visible = True Then
            If mQry <> "" Then mQry += " UNION ALL "
            mQry += " SELECT 'o' As Tick, 'Dimension2' AS Code, '" & AgL.PubCaptionDimension2 & "' As Name "
        End If
        If Dgl1.Columns(Col1Dimension3).Visible = True Then
            If mQry <> "" Then mQry += " UNION ALL "
            mQry += " SELECT 'o' As Tick, 'Dimension3' AS Code, '" & AgL.PubCaptionDimension3 & "' As Name "
        End If
        If Dgl1.Columns(Col1Dimension4).Visible = True Then
            If mQry <> "" Then mQry += " UNION ALL "
            mQry += " SELECT 'o' As Tick, 'Dimension4' AS Code, '" & AgL.PubCaptionDimension4 & "' As Name "
        End If
        If Dgl1.Columns(Col1Size).Visible = True Then
            If mQry <> "" Then mQry += " UNION ALL "
            mQry += " SELECT 'o' As Tick, 'Size' AS Code, 'Size' As Name "
        End If

        If mQry = "" Then MsgBox("No Filter Criteria Found...!", MsgBoxStyle.Information) : Exit Sub

        DtSelection = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If DtSelection.Rows.Count = 1 Then
            mQry = "Select Code, Description From " & AgL.XNull(DtSelection.Rows(0)("Code"))
            mQry += " Where 1=1 "
            If AgL.XNull(DglMain.Item(Col1Value, rowItemCategory).Tag) <> "" Then
                mQry += " And ItemCategory = '" & AgL.XNull(DglMain.Item(Col1Value, rowItemCategory).Tag) & "'"
            End If
            If AgL.XNull(DglMain.Item(Col1Value, rowItemGroup).Tag) <> "" Then
                mQry += " And ItemGroup = '" & AgL.XNull(DglMain.Item(Col1Value, rowItemGroup).Tag) & "'"
            End If
            Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            For I As Integer = 0 To DtTemp.Rows.Count - 1
                Dgl1.Rows.Add()
                Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1

                If AgL.XNull(DglMain.Item(Col1Value, rowItemCategory).Tag) <> "" Then
                    Dgl1.Item(Col1ItemCategory, I).Tag = AgL.XNull(DglMain.Item(Col1Value, rowItemCategory).Tag)
                    Dgl1.Item(Col1ItemCategory, I).Value = AgL.XNull(DglMain.Item(Col1Value, rowItemCategory).Value)
                End If

                If AgL.XNull(DglMain.Item(Col1Value, rowItemGroup).Tag) <> "" Then
                    Dgl1.Item(Col1ItemGroup, I).Tag = AgL.XNull(DglMain.Item(Col1Value, rowItemGroup).Tag)
                    Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(DglMain.Item(Col1Value, rowItemGroup).Value)
                End If

                Dgl1.Item(AgL.XNull(DtSelection.Rows(0)("Name")), I).Tag = AgL.XNull(DtTemp.Rows(I)("Code"))
                Dgl1.Item(AgL.XNull(DtSelection.Rows(0)("Name")), I).Value = AgL.XNull(DtTemp.Rows(I)("Description"))
            Next
        Else
            FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtSelection), "", 400, 420, , , False)
            FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
            FRH_Multiple.FFormatColumn(1, , 0, , False)
            FRH_Multiple.FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)

            FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
            FRH_Multiple.ShowDialog()

            Dim StrFilterCriteria As String = ""
            If FRH_Multiple.BytBtnValue = 0 Then
                StrFilterCriteria = FRH_Multiple.FFetchData(1, "", "", ",", True)
            End If

            Dim bSelectCaluse As String = ""
            Dim bFromClause As String = ""
            If StrFilterCriteria.Contains("'ItemCategory'") Then
                bSelectCaluse += " Ic.Code As ItemCategory, Ic.Description As ItemCategoryDesc. "
                bFromClause += " ItemCategory Ic,"
            End If
            If StrFilterCriteria.Contains("'ItemGroup'") Then
                bSelectCaluse += " Ig.Code As ItemGroup, Ig.Description As ItemGroupDesc. "
                bFromClause += " ItemGroup Ig,"
            End If
            If StrFilterCriteria.Contains("'Item'") Then
                bSelectCaluse += " I.Code As Item, I.Description As ItemDesc. "
                bFromClause += " Item I,"
            End If
            If StrFilterCriteria.Contains("'Dimension1'") Then
                bSelectCaluse += " D1.Code As Dimension1, D1.Description As Dimension1Desc. "
                bFromClause += " Dimension1 D1,"
            End If
            If StrFilterCriteria.Contains("'Dimension2'") Then
                bSelectCaluse += " D2.Code As Dimension2, D2.Description As Dimension2Desc. "
                bFromClause += " Dimension2 D2,"
            End If
            If StrFilterCriteria.Contains("'Dimension3'") Then
                bSelectCaluse += " D3.Code As Dimension3, D3.Description As Dimension3Desc. "
                bFromClause += " Dimension3 D3,"
            End If
            If StrFilterCriteria.Contains("'Dimension4'") Then
                bSelectCaluse += " D4.Code As Dimension4, D4.Description As Dimension4Desc. "
                bFromClause += " Dimension4 D4,"
            End If
            If StrFilterCriteria.Contains("'Size'") Then
                bSelectCaluse += " S.Code As Size, S.Description As SizeDesc. "
                bFromClause += " Size S,"
            End If

            bSelectCaluse = bFromClause.Substring(0, bSelectCaluse.Length - 1)
            bFromClause = bFromClause.Substring(0, bFromClause.Length - 1)

            mQry = " Select " + bSelectCaluse + " From " + bFromClause
            Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        End If
    End Sub
    Private Sub FrmRateListCustom_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        If AgL.StrCmp(Topctrl1.Mode, "Add") Then
            DglMain.Item(Col1Value, rowProcess).ReadOnly = False
        Else
            DglMain.Item(Col1Value, rowProcess).ReadOnly = True
        End If
    End Sub
    Private Sub Dgl1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Dim bRowIndex As Integer = 0
        Dim bColumnIndex As Integer = 0
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            Select Case Dgl1.Columns(bColumnIndex).Name
                Case Col1BtnLineDetail
                    ShowProcessDetail(bRowIndex)
                Case Col1BtnConsumption
                    ShowConsumption(bRowIndex)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ShowProcessDetail(mRow As Integer)
        If Dgl1.Item(Col1BtnLineDetail, mRow).Tag IsNot Nothing Then
            CType(Dgl1.Item(Col1BtnLineDetail, mRow).Tag, FrmRateListExceptionDetail).EntryMode = Topctrl1.Mode
            CType(Dgl1.Item(Col1BtnLineDetail, mRow).Tag, FrmRateListExceptionDetail).DglRow = Dgl1.Rows(mRow)
            Dgl1.Item(Col1BtnLineDetail, mRow).Tag.ShowDialog()
        Else
            Dim FrmObj As FrmRateListExceptionDetail
            FrmObj = New FrmRateListExceptionDetail
            FrmObj.EntryMode = Topctrl1.Mode
            FrmObj.DglRow = Dgl1.Rows(mRow)
            FrmObj.IniGrid(mSearchCode)
            Dgl1.Item(Col1BtnLineDetail, mRow).Tag = FrmObj
            Dgl1.Item(Col1BtnLineDetail, mRow).Tag.ShowDialog()
        End If
        Calculation()
    End Sub
    Private Sub ShowConsumption(mRow As Integer)
        If Dgl1.Item(Col1BtnConsumption, mRow).Tag IsNot Nothing Then
            CType(Dgl1.Item(Col1BtnConsumption, mRow).Tag, FrmConsumptionException).EntryMode = Topctrl1.Mode
            CType(Dgl1.Item(Col1BtnConsumption, mRow).Tag, FrmConsumptionException).DglRow = Dgl1.Rows(mRow)
            Dgl1.Item(Col1BtnConsumption, mRow).Tag.ShowDialog()
        Else
            Dim FrmObj As FrmConsumptionException
            FrmObj = New FrmConsumptionException
            FrmObj.EntryMode = Topctrl1.Mode
            FrmObj.DglRow = Dgl1.Rows(mRow)
            FrmObj.ItemCategory = Dgl1.Item(Col1ItemCategory, mRow).Tag
            FrmObj.ItemCategoryName = Dgl1.Item(Col1ItemCategory, mRow).Value
            FrmObj.Dimension3 = DglMain.Item(Col1Value, rowDimension3).Tag
            FrmObj.Dimension3Name = DglMain.Item(Col1Value, rowDimension3).Value
            FrmObj.IniGrid()
            Dgl1.Item(Col1BtnConsumption, mRow).Tag = FrmObj
            Dgl1.Item(Col1BtnConsumption, mRow).Tag.ShowDialog()
        End If
        Calculation()
    End Sub
    Private Sub ShowAttachments()
        Dim FrmObj As New AgLibrary.FrmAttachmentViewer(AgL)
        FrmObj.LblDocNo.Text = "Design : " + DglMain.Item(Col1Value, rowDimension3).Value
        FrmObj.SearchCode = "RateListException-" + mSearchCode
        FrmObj.StartPosition = FormStartPosition.CenterParent
        FrmObj.ShowDialog()
        FrmObj.Dispose()
        FrmObj = Nothing
        SetAttachmentCaption()
    End Sub
    Private Sub SetAttachmentCaption()
        Dim AttachmentPath As String = PubAttachmentPath + "RateListException-" + mSearchCode + "\"
        If Directory.Exists(AttachmentPath) Then
            Dim FileCount As Integer = Directory.GetFiles(AttachmentPath).Count
            If FileCount > 0 Then DglMain.Item(Col1Value, rowBtnAttachments).Value = FileCount.ToString + IIf(FileCount = 1, " Attachment", " Attachments") Else DglMain.Item(Col1Value, rowBtnAttachments).Value = ""
        Else
            DglMain.Item(Col1Value, rowBtnAttachments).Value = ""
        End If
    End Sub
End Class
