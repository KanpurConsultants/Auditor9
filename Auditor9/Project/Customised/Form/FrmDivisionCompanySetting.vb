Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain
Public Class FrmDivisionCompanySetting
    Inherits AgTemplate.TempMaster

    Dim mQry$

    Public WithEvents DglMain As New AgControls.AgDataGrid
    Public Const ColSNo As String = "Srl."
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1LastValue As String = "Last Value"
    Public Const Col1HeadOriginal As String = "Head Original"


    Dim rowDivision As Integer = 0
    Dim rowCompany As Integer = 1
    Dim rowOpeningStockValue As Integer = 2
    Dim rowClosingStockValue As Integer = 3
    Dim rowRemark As Integer = 4

    Public Const hcDivision As String = "Division"
    Public Const hcCompany As String = "Company"
    Public Const hcOpeningStockValue As String = "Opening Stock Value"
    Public Const hcClosingStockValue As String = "Closing Stock Value"
    Public Const hcRemark As String = "Remark"

    Dim DtItemTypeSetting As DataTable
    Dim mItemTypeLastValue As String


#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.Pnl1 = New System.Windows.Forms.Panel()
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
        Me.Topctrl1.Size = New System.Drawing.Size(897, 41)
        Me.Topctrl1.TabIndex = 12
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 458)
        Me.GroupBox1.Size = New System.Drawing.Size(939, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(14, 462)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(148, 527)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(231, 462)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(401, 462)
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
        Me.GroupBox2.Location = New System.Drawing.Point(704, 462)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(470, 462)
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
        Me.Pnl1.Location = New System.Drawing.Point(7, 45)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(886, 407)
        Me.Pnl1.TabIndex = 1064
        '
        'FrmDivisionCompanySetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(897, 506)
        Me.Controls.Add(Me.Pnl1)
        Me.MaximizeBox = True
        Me.Name = "FrmDivisionCompanySetting"
        Me.Text = "Division Company Settings"
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
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
        Me.ResumeLayout(False)
    End Sub
    Public WithEvents Pnl1 As Panel
#End Region
    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If DglMain.Item(Col1Value, rowDivision).Value.Trim = "" Then Err.Raise(1, , "Division Is Required!")
        If DglMain.Item(Col1Value, rowCompany).Value.Trim = "" Then Err.Raise(1, , "Company Is Required!")

        If Topctrl1.Mode = "Add" Then

            mQry = "Select count(*) From DivisionCompanySetting Where Div_Code = '" & DglMain.Item(Col1Value, rowDivision).Tag & "' And Comp_Code = '" & DglMain.Item(Col1Value, rowCompany).Tag & "'  And Remark = '" & DglMain.Item(Col1Value, rowRemark).Value & "'"
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        Else
            mQry = "Select count(*) From DivisionCompanySetting Where Div_Code = '" & DglMain.Item(Col1Value, rowDivision).Tag & "' And Comp_Code = '" & DglMain.Item(Col1Value, rowCompany).Tag & "'  And Remark = '" & DglMain.Item(Col1Value, rowRemark).Value & "' And Code<>'" & mInternalCode & "'"
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If
    End Sub
    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        AgL.PubFindQry = "Select D.Div_Name as Div_Name, C.cyear As Comp_Name, H.OpeningStockValue, H.ClosingStockValue, H.Remark
                         From DivisionCompanySetting H 
                         Left Join Division D On H.Div_Code = D.Div_Code
                         LEFT JOIN Company C On H.Comp_Code = C.Comp_Code "
        AgL.PubFindQryOrdBy = "[Description]"
    End Sub
    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "DivisionCompanySetting"
    End Sub
    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer

        mQry = "UPDATE DivisionCompanySetting
                Set 
                Div_Code = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowDivision).Tag) & ", 
                Comp_Code = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowCompany).Tag) & ", 
                OpeningStockValue = " & Val(DglMain.Item(Col1Value, rowOpeningStockValue).Value) & ",
                ClosingStockValue = " & Val(DglMain.Item(Col1Value, rowClosingStockValue).Value) & ",
                Remark = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowRemark).Value) & "
                Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub
    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = "Select H.*, D.Div_Name as Div_Name, C.cyear As Comp_Name
                 From DivisionCompanySetting H 
                 Left Join Division D On H.Div_Code = D.Div_Code
                 LEFT JOIN Company C On H.Comp_Code = C.Comp_Code
                 Where H.Code = '" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(DsTemp.Tables(0).Rows(0)("Code"))
                DglMain.Item(Col1Value, rowDivision).Tag = AgL.XNull(DsTemp.Tables(0).Rows(0)("Div_Code"))
                DglMain.Item(Col1Value, rowDivision).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("Div_Name"))
                DglMain.Item(Col1Value, rowCompany).Tag = AgL.XNull(DsTemp.Tables(0).Rows(0)("Comp_Code"))
                DglMain.Item(Col1Value, rowCompany).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("Comp_Name"))
                DglMain.Item(Col1Value, rowOpeningStockValue).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("OpeningStockValue"))
                DglMain.Item(Col1Value, rowClosingStockValue).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("ClosingStockValue"))
                DglMain.Item(Col1Value, rowRemark).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("Remark"))
            End If
        End With
    End Sub
    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        DglMain.CurrentCell = DglMain.Item(Col1Value, rowDivision)
        DglMain.Focus()
    End Sub
    Private Sub TxtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                Topctrl1.FButtonClick(13)
            End If
        End If
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
        mQry = "Select I.Code As SearchCode " &
            " From DivisionCompanySetting I " &
            " Order By I.Comp_Code "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub
    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub
    Private Sub FrmDivisionCompanySetting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 360, 885)
    End Sub
    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        If ClsMain.IsEntryLockedWithLockText("Item", "Code", mSearchCode) = True Then
            Passed = False
            Exit Sub
        End If
    End Sub
    Private Sub FrmDivisionCompanySetting_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        Try
            DglMain.CurrentCell = DglMain.Item(Col1Value, DglMain.FirstDisplayedCell.RowIndex)
            DglMain.Focus()
        Catch ex As Exception
            MsgBox(ex.Message & " [FrmDivisionCompanySetting_BaseEvent_Topctrl_tbAdd]")
        End Try
    End Sub
    Private Sub FrmDivisionCompanySetting_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        DglMain.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DglMain, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(DglMain, Col1Head, 300, 255, Col1Head, True, True)
            .AddAgTextColumn(DglMain, Col1HeadOriginal, 180, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(DglMain, Col1Mandatory, 12, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(DglMain, Col1Value, 500, 255, Col1Value, True, False)
            .AddAgTextColumn(DglMain, Col1LastValue, 300, 255, Col1LastValue, False, False)
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
        DglMain.BackgroundColor = Me.BackColor
        AgL.GridDesign(DglMain)
        DglMain.Anchor = AnchorStyles.Top + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom

        DglMain.Rows.Add(5)

        DglMain.Item(Col1Head, rowDivision).Value = hcDivision
        DglMain.Item(Col1Head, rowCompany).Value = hcCompany
        DglMain.Item(Col1Head, rowOpeningStockValue).Value = hcOpeningStockValue
        DglMain.Item(Col1Head, rowClosingStockValue).Value = hcClosingStockValue
        DglMain.Item(Col1Head, rowRemark).Value = hcRemark

        For I As Integer = 0 To DglMain.Rows.Count - 1
            DglMain(Col1HeadOriginal, I).Value = DglMain(Col1Head, I).Value
        Next

        AgL.FSetDimensionCaptionForVerticalGrid(DglMain, AgL)
    End Sub
    Private Sub FrmDivisionCompanySetting_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dim i As Integer

        For i = 0 To DglMain.Rows.Count - 1
            DglMain(Col1Value, i).Value = ""
            DglMain(Col1Value, i).Tag = ""
        Next
    End Sub
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DglMain.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            If DglMain.CurrentCell Is Nothing Then Exit Sub
            bRowIndex = DglMain.CurrentCell.RowIndex
            bColumnIndex = DglMain.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> DglMain.Columns(Col1Value).Index Then Exit Sub

            Select Case DglMain.CurrentCell.RowIndex
                Case rowDivision
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "Select Div_Code As Code, Div_Name As Name " &
                            " From Division " &
                            " Order By Div_Name "
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If

                Case rowCompany
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "Select Comp_Code As Code, cyear As Name " &
                            " From Company " &
                            " Order By Comp_Name "
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
    Private Sub Dgl1_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles DglMain.EditingControl_Validating
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
        End If
    End Sub
    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DglMain.CellEnter
        Try
            If DglMain.CurrentCell Is Nothing Then Exit Sub
            If Topctrl1.Mode = "BROWSE" Then
                DglMain.CurrentCell.ReadOnly = True
            End If

            If Me.Visible And sender.ReadOnly = False Then
                If sender.CurrentCell.ColumnIndex = sender.Columns(Col1Head).Index Or
                    sender.CurrentCell.ColumnIndex = sender.Columns(Col1Mandatory).Index Then
                    'SendKeys.Send("{Tab}")
                End If
            End If


            If DglMain.CurrentCell.ColumnIndex <> DglMain.Columns(Col1Value).Index Then Exit Sub


            DglMain.AgHelpDataSet(DglMain.CurrentCell.ColumnIndex) = Nothing
            CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0
            CType(DglMain.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = False

            Select Case DglMain.CurrentCell.RowIndex
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmDivisionCompanySetting_BaseEvent_Save_PostTrans(SearchCode As String) Handles Me.BaseEvent_Save_PostTrans
        ClsMain.FCreateItemDataTable()
    End Sub
    Private Sub LblIsSystemDefine_Click(sender As Object, e As EventArgs)
    End Sub
    Private Sub FrmDivisionCompanySetting_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Dim I As Integer
        For I = 0 To DglMain.Rows.Count - 1
            DglMain(Col1Head, I).Tag = Nothing
        Next
    End Sub
End Class
