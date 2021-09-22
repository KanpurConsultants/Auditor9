Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain
Imports Customised.ClsMain.ConfigurableFields
Public Class FrmClassFee
    Inherits AgTemplate.TempMaster

    Dim mQry$

    Public Const ColSNo As String = "SNo"
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Fee As String = "Fee"
    Public Const Col1SubHead As String = "Sub Head"
    Public Const Col1Recurrence As String = "Recurrence"
    Public Const Col1Narration As String = "Narration"
    Public Const Col1Amount As String = "Amount"
    Public Const Col1DueDate As String = "Due Date"

    Public WithEvents DglMain As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1HeadOriginal As String = "Head Original"

    Dim rowClass As Integer = 0
    Dim rowFeeStructureName As Integer = 1

    Public Const hcClass As String = "Class"
    Public Const hcFeeStructureName As String = "Fee Structure Name"

    Dim mItemTypeLastValue As String

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.PnlMain = New System.Windows.Forms.Panel()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.GrpUP.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'Pnl1
        '
        Me.Pnl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl1.Location = New System.Drawing.Point(1, 175)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(960, 381)
        Me.Pnl1.TabIndex = 2
        '
        'PnlMain
        '
        Me.PnlMain.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PnlMain.Location = New System.Drawing.Point(1, 43)
        Me.PnlMain.Name = "PnlMain"
        Me.PnlMain.Size = New System.Drawing.Size(959, 108)
        Me.PnlMain.TabIndex = 1
        '
        'LinkLabel1
        '
        Me.LinkLabel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(-1, 153)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(147, 21)
        Me.LinkLabel1.TabIndex = 1063
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Fee Detail"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FrmClassFee
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(961, 606)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.PnlMain)
        Me.Controls.Add(Me.Pnl1)
        Me.MaximizeBox = True
        Me.Name = "FrmClassFee"
        Me.Text = "Quality Master"
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.PnlMain, 0)
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
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Pnl1 As Panel
    Friend WithEvents PnlMain As Panel
    Public WithEvents LinkLabel1 As LinkLabel
#End Region

    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, AgL.PubSiteCode, "", "", "", "", "")
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

        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Fee, I).Value <> "" Then
                If Dgl1.Item(Col1DueDate, I).Value = "" Then
                    MsgBox("Due Date is blank at row no." & (I + 1).ToString)
                    Dgl1.CurrentCell = Dgl1.Item(Col1DueDate, I)
                    Dgl1.Focus()
                    passed = False : Exit Sub
                End If

                If Dgl1.Item(Col1Recurrence, I).Value = "" Then
                    MsgBox("Recurrence is blank at row no." & (I + 1).ToString)
                    Dgl1.CurrentCell = Dgl1.Item(Col1DueDate, I)
                    Dgl1.Focus()
                    passed = False : Exit Sub
                End If
            End If
        Next

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From Item Where Description='" & DglMain.Item(Col1Value, rowFeeStructureName).Value & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        Else
            mQry = "Select count(*) From Item Where Description='" & DglMain.Item(Col1Value, rowFeeStructureName).Value & "' And Code<>'" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If


        For I = 0 To DglMain.Rows.Count - 1
            If DglMain.Item(Col1Value, I).Value = Nothing Then DglMain.Item(Col1Value, I).Value = ""
            If DglMain.Item(Col1Value, I).Tag = Nothing Then DglMain.Item(Col1Value, I).Tag = ""
        Next
    End Sub
    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        AgL.PubFindQry = "SELECT H.Code, H.Description as Name
                            FROM Item H
                            WHERE H.V_Type =" & AgL.Chk_Text(ClsSchool.ItemV_Type_ClassFee) & " "
        AgL.PubFindQryOrdBy = "[Description]"
    End Sub
    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Item"
        MainLineTableCsv = "FeeStructureRecurrence"
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer
        Dim mSr As Integer = 0

        mQry = "UPDATE Item 
                Set 
                V_Type = '" & ClsSchool.ItemV_Type_ClassFee & "',
                ItemType = '" & ClsSchool.ItemV_Type_ClassFee & "',
                Specification = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowClass).Value) & ",
                Description = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowFeeStructureName).Value) & "
                Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "DELETE FROM FeeStructureRecurrence WHERE Code  = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Fee, I).Value <> "" Then
                mSr += 1
                FCreateSubHead(I, AgL.GCn, AgL.ECmd)
                mQry = "INSERT INTO FeeStructureRecurrence (Code, Sr, Class, Fee, SubHead, Recurrence, Narration, 
                        Amount, DueDate, Comp_Code, Div_Code, Site_Code)
                        Select '" & SearchCode & "', " & mSr & ", 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowClass).Tag) & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1Fee, I).Tag) & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1SubHead, I).Tag) & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1Recurrence, I).Value) & ", 
                        " & AgL.Chk_Text(Dgl1.Item(Col1Narration, I).Value) & ", 
                        " & Val(Dgl1.Item(Col1Amount, I).Value) & ", 
                        " & AgL.Chk_Date(Dgl1.Item(Col1DueDate, I).Value) & ", 
                        " & AgL.Chk_Text(AgL.PubCompCode) & ",
                        " & AgL.Chk_Text(AgL.PubDivCode) & ",
                        " & AgL.Chk_Text(AgL.PubSiteCode) & " "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
        Next

        FSaveFeeStructure(SearchCode, AgL.GCn, AgL.ECmd)

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        End If
    End Sub
    Private Sub FCreateSubHead(mRow As Integer, Conn As Object, Cmd As Object)
        If AgL.XNull(Dgl1.Item(Col1SubHead, mRow).Tag) = "" Then
            mQry = " Select SubCode From SubGroup Where Name = '" & Dgl1.Item(Col1SubHead, mRow).Value & "'"
            Dgl1.Item(Col1SubHead, mRow).Tag = AgL.Dman_Execute(mQry, IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
            If AgL.XNull(Dgl1.Item(Col1SubHead, mRow).Tag) = "" Then
                Dim mSubHeadCode As String = AgL.GetMaxId("SubGroup", "SubCode", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
                mQry = "INSERT INTO SubGroup(SubCode, SubGroupType, Name, DispName, Parent) " &
                    " VALUES(" & AgL.Chk_Text(mSubHeadCode) & ", " &
                    " " & AgL.Chk_Text(ClsSchool.SubGroupType_FeeHead) & ", " &
                    " " & AgL.Chk_Text(Dgl1.Item(Col1SubHead, mRow).Value) & ", " &
                    " " & AgL.Chk_Text(Dgl1.Item(Col1SubHead, mRow).Value) & ", " &
                    " " & AgL.Chk_Text(Dgl1.Item(Col1Fee, mRow).Tag) & ")"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                Dgl1.Item(Col1SubHead, mRow).Tag = mSubHeadCode
            Else
                mQry = " UPDATE SubGroup 
                        Set Name = " & AgL.Chk_Text(Dgl1.Item(Col1SubHead, mRow).Value) & ",
                        DispName = " & AgL.Chk_Text(Dgl1.Item(Col1SubHead, mRow).Value) & ",
                        Parent = " & AgL.Chk_Text(Dgl1.Item(Col1Fee, mRow).Tag) & "
                        Where SubCode = '" & AgL.XNull(Dgl1.Item(Col1SubHead, mRow).Tag) & "'"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Else
            mQry = " UPDATE SubGroup 
                    Set Name = " & AgL.Chk_Text(Dgl1.Item(Col1SubHead, mRow).Value) & ",
                    DispName = " & AgL.Chk_Text(Dgl1.Item(Col1SubHead, mRow).Value) & ",
                    Parent = " & AgL.Chk_Text(Dgl1.Item(Col1Fee, mRow).Tag) & "
                    Where SubCode = '" & AgL.XNull(Dgl1.Item(Col1SubHead, mRow).Tag) & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub
    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet
        mQry = "SELECT H.* FROM Item H WHERE H.Code ='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                DglMain.Item(Col1Value, rowFeeStructureName).Value = AgL.XNull(.Rows(0)("Description"))
            End If
        End With


        Dim I As Integer
        mQry = "SELECT H.*, Class.Name As ClassName, Fee.Name As FeeName, 
                SubHead.Name As SubHeadName
                FROM FeeStructureRecurrence H
                LEFT JOIN SubGroup Class ON H.Class = Class.SubCode
                LEFT JOIN SubGroup Fee ON H.Fee = Fee.SubCode
                LEFT JOIN SubGroup SubHead ON H.SubHead = SubHead.SubCode
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
                    DglMain.Item(Col1Value, rowClass).Tag = AgL.XNull(.Rows(0)("Class"))
                    DglMain.Item(Col1Value, rowClass).Value = AgL.XNull(.Rows(0)("ClassName"))
                    Dgl1.Item(Col1Fee, I).Tag = AgL.XNull(.Rows(I)("Fee"))
                    Dgl1.Item(Col1Fee, I).Value = AgL.XNull(.Rows(I)("FeeName"))
                    Dgl1.Item(Col1SubHead, I).Tag = AgL.XNull(.Rows(I)("SubHead"))
                    Dgl1.Item(Col1SubHead, I).Value = AgL.XNull(.Rows(I)("SubHeadName"))
                    Dgl1.Item(Col1Recurrence, I).Value = AgL.XNull(.Rows(I)("Recurrence"))
                    Dgl1.Item(Col1Narration, I).Value = AgL.XNull(.Rows(I)("Narration"))
                    Dgl1.Item(Col1DueDate, I).Value = ClsMain.FormatDate(AgL.XNull(.Rows(I)("DueDate")))
                    Dgl1.Item(Col1Amount, I).Value = AgL.VNull(.Rows(I)("Amount"))
                Next I
                Dgl1.Visible = True
            Else
                Dgl1.Visible = False
            End If
        End With
    End Sub
    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        DglMain.CurrentCell = DglMain.FirstDisplayedCell
        DglMain.Focus()
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
        mQry = "SELECT I.Code AS SearchCode FROM Item I  
                WHERE I.V_Type =  '" & ClsSchool.ItemV_Type_ClassFee & "'" &
                " Order By I.Code "
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
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
    End Sub
    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
    End Sub
    Private Sub FrmItemBOM_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        If DglMain.Rows(rowClass).Visible Then DglMain.CurrentCell = DglMain(Col1Value, rowClass)
        DglMain.Focus()
    End Sub
    Private Sub FrmItemBOM_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dim I As Integer
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Fee, 120, 0, Col1Fee, True, False, False)
            .AddAgTextColumn(Dgl1, Col1SubHead, 180, 0, Col1SubHead, True, False, False)
            .AddAgTextColumn(Dgl1, Col1Recurrence, 180, 0, Col1Recurrence, True, False, False)
            .AddAgTextColumn(Dgl1, Col1Narration, 180, 0, Col1Narration, True, False, False)
            .AddAgDateColumn(Dgl1, Col1DueDate, 120, Col1DueDate, True, False)
            .AddAgNumberColumn(Dgl1, Col1Amount, 80, 5, 2, False, Col1Amount, True, False, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.RowHeadersVisible = False
        Dgl1.BackgroundColor = Me.BackColor
        'Dgl2.AllowUserToAddRows = False
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
        AgL.AddAgDataGrid(DglMain, PnlMain)
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
        DglMain.Anchor = AnchorStyles.Top + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom
        DglMain.Name = "DglMain"

        DglMain.Rows.Add(2)
        'For I = 0 To Dgl1.Rows.Count - 1
        '    Dgl1.Rows(I).Visible = False
        'Next

        DglMain.Item(Col1Head, rowClass).Value = hcClass
        DglMain.Item(Col1Head, rowFeeStructureName).Value = hcFeeStructureName

        For I = 0 To DglMain.Rows.Count - 1
            DglMain(Col1HeadOriginal, I).Value = DglMain(Col1Head, I).Value
        Next

        ApplyUISetting()

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub
    Private Sub DglRateType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub

        If e.Control And e.KeyCode = Keys.D Then
            If IsFeeStructureChangeAllowed() = False Then
                MsgBox("Fee Receipt exists for this class. Can't change fee structure.", MsgBoxStyle.Information)
                Exit Sub
            Else
                sender.CurrentRow.Selected = True
            End If
        End If

        If e.Control Or e.Shift Or e.Alt Then Exit Sub

        If e.KeyCode = Keys.Enter Then
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
            CType(DglMain.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = False

            Select Case DglMain.CurrentCell.RowIndex
                Case rowFeeStructureName
                    DglMain.Item(Col1Value, rowFeeStructureName).ReadOnly = True
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
                Case rowClass
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " SELECT Sg.SubCode AS Code, Sg.Name
                                FROM Subgroup Sg With (NoLock)
                                Where Sg.SubgroupType = '" & ClsSchool.SubGroupType_Class & "' 
                                And IfNull(Sg.Status,'Active') = 'Active'"
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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
                Case Col1Fee
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            mQry = " SELECT Sg.SubCode AS Code, Sg.Name
                                    FROM Subgroup Sg With (NoLock)
                                    Where Sg.SubgroupType = '" & ClsSchool.SubGroupType_Fee & "' 
                                    And IfNull(Sg.Status,'Active') = 'Active'"
                            Dgl1.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1Recurrence
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(bColumnIndex) Is Nothing Then
                            mQry = " Select '" & ClsSchool.Recurrence_Monthly & "' As Code, '" & ClsSchool.Recurrence_Monthly & "' As Name
                                    UNION ALL 
                                    Select '" & ClsSchool.Recurrence_BiMonthly & "' As Code, '" & ClsSchool.Recurrence_BiMonthly & "' As Name
                                    UNION ALL 
                                    Select '" & ClsSchool.Recurrence_Quarterly & "' As Code, '" & ClsSchool.Recurrence_Quarterly & "' As Name 
                                    UNION ALL 
                                    Select '" & ClsSchool.Recurrence_HalfYearly & "' As Code, '" & ClsSchool.Recurrence_HalfYearly & "' As Name 
                                    UNION ALL 
                                    Select '" & ClsSchool.Recurrence_Yearly & "' As Code, '" & ClsSchool.Recurrence_Yearly & "' As Name 
                                    UNION ALL 
                                    Select '" & ClsSchool.Recurrence_OnceInALifeTime & "' As Code, '" & ClsSchool.Recurrence_OnceInALifeTime & "' As Name "
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

            Select Case mRow
                Case rowClass
                    mQry = " Select cyear From Company Where Comp_Code = '" & AgL.PubCompCode & "' "
                    Dim bCompYear As String = AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar()
                    DglMain.Item(Col1Value, rowFeeStructureName).Value = DglMain.Item(Col1Value, rowClass).Value + "-" + bCompYear + "-" + AgL.PubSiteName + "-" + AgL.PubDivName
            End Select
        End If
        Calculation()
    End Sub

    Private Sub FrmItemBOM_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dim i As Integer

        For i = 0 To DglMain.Rows.Count - 1
            DglMain(Col1Value, i).Value = ""
            DglMain(Col1Value, i).Tag = ""
        Next


        Dgl1.Rows.Clear()
        Dgl1.RowCount = 1

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
    End Sub
    Private Sub FrmClassFee_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        If Dgl1.AgHelpDataSet(Col1Fee) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Fee).Dispose() : Dgl1.AgHelpDataSet(Col1Fee) = Nothing
        If Dgl1.AgHelpDataSet(Col1SubHead) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1SubHead).Dispose() : Dgl1.AgHelpDataSet(Col1SubHead) = Nothing

        For I As Integer = 0 To DglMain.Rows.Count - 1
            DglMain(Col1Head, I).Tag = Nothing
        Next
    End Sub
    Private Sub DGL2_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub
    Private Sub ApplyUISetting()
        GetUISetting_WithDataTables(DglMain, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", ClsMain.GridTypeConstants.VerticalGrid)
        GetUISetting_WithDataTables(Dgl1, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", ClsMain.GridTypeConstants.HorizontalGrid)
    End Sub
    Private Sub Dgl1_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles Dgl1.CellBeginEdit
        Try
            If Dgl1.CurrentCell Is Nothing Then Exit Sub

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Fee, Col1SubHead, Col1DueDate
                    If IsFeeStructureChangeAllowed() = False Then
                        If AgL.XNull(Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Value) <> "" Then
                            MsgBox("Fee Receipt exists for this class. Can't change fee structure.", MsgBoxStyle.Information)
                            e.Cancel = True
                            Exit Sub
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub DglMain_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles DglMain.CellBeginEdit
        Try
            If DglMain.CurrentCell Is Nothing Then Exit Sub

            Select Case DglMain.CurrentCell.RowIndex
                Case rowClass
                    If IsFeeStructureChangeAllowed() = False Then
                        MsgBox("Fee Receipt exists for this class. Can't change fee structure.", MsgBoxStyle.Information)
                        e.Cancel = True
                        Exit Sub
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function IsFeeStructureChangeAllowed() As Boolean
        IsFeeStructureChangeAllowed = True
        If Not AgL.StrCmp(Topctrl1.Mode, "Add") Then
            If AgL.VNull(AgL.Dman_Execute(" Select Count(*) From FeeAdjustmentDetail 
                            Where Class = '" & DglMain.Item(Col1Value, rowClass).Tag & "'", AgL.GCn).ExecuteScalar()) > 0 Then
                IsFeeStructureChangeAllowed = False
            End If
        End If
    End Function
    Private Sub FSaveFeeStructure(SearchCode As String, Conn As Object, Cmd As Object)
        mQry = " Delete From FeeStructure Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Select * From FeeStructureRecurrence Where Code = '" & SearchCode & "'"
        Dim DtTemp As DataTable = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)

        Dim mDueDateYear As Integer = 0
        Dim mDueDateMonth As Integer = 0
        Dim mDueDateDay As Integer = 0

        For I As Integer = 0 To DtTemp.Rows.Count - 1
            mDueDateDay = CDate(Dgl1.Item(Col1DueDate, I).Value).Day
            mDueDateMonth = CDate(Dgl1.Item(Col1DueDate, I).Value).Month
            mDueDateYear = CDate(Dgl1.Item(Col1DueDate, I).Value).Year
            If AgL.XNull(DtTemp.Rows(I)("Recurrence")) = ClsSchool.Recurrence_Monthly Then
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 0), Conn, Cmd)
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 1), Conn, Cmd)
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 2), Conn, Cmd)
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 3), Conn, Cmd)
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 4), Conn, Cmd)
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 5), Conn, Cmd)
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 6), Conn, Cmd)
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 7), Conn, Cmd)
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 8), Conn, Cmd)
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 9), Conn, Cmd)
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 10), Conn, Cmd)
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 11), Conn, Cmd)
            ElseIf AgL.XNull(DtTemp.Rows(I)("Recurrence")) = ClsSchool.Recurrence_BiMonthly Then
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 0), Conn, Cmd)
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 2), Conn, Cmd)
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 4), Conn, Cmd)
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 6), Conn, Cmd)
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 8), Conn, Cmd)
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 10), Conn, Cmd)
            ElseIf AgL.XNull(DtTemp.Rows(I)("Recurrence")) = ClsSchool.Recurrence_Quarterly Then
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 0), Conn, Cmd)
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 3), Conn, Cmd)
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 6), Conn, Cmd)
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 9), Conn, Cmd)
            ElseIf AgL.XNull(DtTemp.Rows(I)("Recurrence")) = ClsSchool.Recurrence_HalfYearly Then
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 0), Conn, Cmd)
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 6), Conn, Cmd)
            ElseIf AgL.XNull(DtTemp.Rows(I)("Recurrence")) = ClsSchool.Recurrence_Yearly Then
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 0), Conn, Cmd)
            ElseIf AgL.XNull(DtTemp.Rows(I)("Recurrence")) = ClsSchool.Recurrence_OnceInALifeTime Then
                FInsertFeeStructure(SearchCode, DtTemp, I, FReCalculateNewDueDate(mDueDateDay, mDueDateMonth, mDueDateYear, 0), Conn, Cmd)
            End If
        Next
    End Sub
    Private Function FReCalculateNewDueDate(mDueDateDay As Integer, mDueDateMonth As Integer, mDueDateYear As Integer, Counter As Integer) As String
        mDueDateMonth = mDueDateMonth + Counter
        If mDueDateMonth > 12 Then
            mDueDateMonth = mDueDateMonth - 12
            mDueDateYear = mDueDateYear + 1
        End If
        FReCalculateNewDueDate = mDueDateDay & "/" & mDueDateMonth & "/" & mDueDateYear
    End Function
    Private Sub FInsertFeeStructure(SearchCode As String, DtTemp As DataTable, mRow As Integer, DueDate As String,
                                    Conn As Object, Cmd As Object)
        Dim mSr As Integer = AgL.VNull(AgL.Dman_Execute("Select IfNull(Max(Sr),0) + 1
                    From FeeStructure 
                    Where Code = '" & SearchCode & "'", IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar())

        mQry = "INSERT INTO FeeStructure (Code, Sr, Class, Fee, SubHead, Recurrence,
                Amount, DueDate, Comp_Code, Div_Code, Site_Code)
                Select '" & SearchCode & "', " & mSr & ", 
                " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(mRow)("Class"))) & ", 
                " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(mRow)("Fee"))) & ", 
                " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(mRow)("SubHead"))) & ", 
                " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(mRow)("Recurrence"))) & ", 
                " & AgL.VNull(DtTemp.Rows(mRow)("Amount")) & ", 
                " & AgL.Chk_Date(DueDate) & ", 
                " & AgL.Chk_Text(AgL.PubCompCode) & ",
                " & AgL.Chk_Text(AgL.PubDivCode) & ",
                " & AgL.Chk_Text(AgL.PubSiteCode) & " "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub
    Private Sub FrmClassFee_BaseEvent_ApproveDeletion_InTrans(SearchCode As String, Conn As Object, Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = " Delete From FeeStructure Where Code = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub
    Private Sub FrmSaleInvoiceDirect_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        If Not AgL.StrCmp(Topctrl1.Mode, "Add") Then
            If IsFeeStructureChangeAllowed() = False Then
                MsgBox("Fee Receipt exists for this class. Can't change fee structure.", MsgBoxStyle.Information)
                Passed = False
                Exit Sub
            End If
        End If
    End Sub
End Class
