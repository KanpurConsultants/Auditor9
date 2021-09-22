Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class FrmAccountGroup
    Inherits AgTemplate.TempMaster

    Dim mQry$

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1LastValue As String = "Last Value"
    Public Const Col1HeadOriginal As String = "Head Original"

    Dim rowGroupName As Integer = 0
    Dim rowGroupUnder As Integer = 1
    Dim rowGroupSimilarTo As Integer = 2

    Public Const hcGroupName As String = "Group Name"
    Public Const hcGroupUnder As String = "Group Under"
    Public WithEvents LblSystemDefine As Label
    Public Const hcGroupSimilarTo As String = "Group Similar To"

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.LblSystemDefine = New System.Windows.Forms.Label()
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
        'LblSystemDefine
        '
        Me.LblSystemDefine.AutoSize = True
        Me.LblSystemDefine.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSystemDefine.ForeColor = System.Drawing.Color.Red
        Me.LblSystemDefine.Location = New System.Drawing.Point(148, 483)
        Me.LblSystemDefine.Name = "LblSystemDefine"
        Me.LblSystemDefine.Size = New System.Drawing.Size(10, 15)
        Me.LblSystemDefine.TabIndex = 1063
        Me.LblSystemDefine.Text = "."
        '
        'FrmAccountGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(897, 506)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.LblSystemDefine)
        Me.MaximizeBox = True
        Me.Name = "FrmAccountGroup"
        Me.Text = "Item Category Master"
        Me.Controls.SetChildIndex(Me.LblSystemDefine, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
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
        Me.PerformLayout()

    End Sub
    Public WithEvents Pnl1 As Panel
#End Region
    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If Dgl1.Item(Col1Value, rowGroupName).Value.Trim = "" Then Err.Raise(1, , "Group Name Is Required!")

        If AgL.XNull(Dgl1.Item(Col1Value, rowGroupUnder).Tag) <> "" And
            AgL.XNull(Dgl1.Item(Col1Value, rowGroupSimilarTo).Tag) <> "" Then
            MsgBox(" Only one value should be mentioned either Group Under or Group To.", MsgBoxStyle.Information)
            passed = False : Exit Sub
        End If

        If Topctrl1.Mode = "Add" Then
            mSearchCode = AgL.GetMaxId("AcGroup", "GroupCode", AgL.GCn, "", "", 4, True)
            mInternalCode = mSearchCode
            mQry = "Select count(*) From AcGroup Where GroupName ='" & Dgl1.Item(Col1Value, rowGroupName).Value & "'"
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Group Name Exist!")
        Else
            mQry = "Select count(*) From AcGroup Where GroupName ='" & Dgl1.Item(Col1Value, rowGroupName).Value & "' And GroupCode <>'" & mInternalCode & "'"
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Group Name Already Exist!")
        End If
    End Sub
    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        AgL.PubFindQry = "SELECT H.GroupCode, H.GroupName FROM AcGroup H "
        AgL.PubFindQryOrdBy = "[GroupName]"
    End Sub
    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "AcGroup"
        PrimaryField = "GroupCode"
    End Sub
    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim mPickDetailsFromGroup As String = ""
        Dim mGroupNature As String = ""
        Dim mNature As String = ""

        If AgL.XNull(Dgl1.Item(Col1Value, rowGroupUnder).Tag) <> "" Then
            mPickDetailsFromGroup = AgL.XNull(Dgl1.Item(Col1Value, rowGroupUnder).Tag)
        ElseIf AgL.XNull(Dgl1.Item(Col1Value, rowGroupSimilarTo).Tag) <> "" Then
            mPickDetailsFromGroup = AgL.XNull(Dgl1.Item(Col1Value, rowGroupSimilarTo).Tag)
        Else
            mPickDetailsFromGroup = SearchCode
        End If

        mQry = "Select * from AcGroup With (NoLock) Where GroupCode = '" & mPickDetailsFromGroup & "' "
        Dim DtAcGroup As DataTable = AgL.FillData(mQry, AgL.GcnRead).tables(0)
        If DtAcGroup.Rows.Count > 0 Then
            mGroupNature = AgL.XNull(DtAcGroup.Rows(0)("GroupNature"))
            mNature = AgL.XNull(DtAcGroup.Rows(0)("Nature"))
        End If

        If mGroupNature = "" Then Err.Raise(1, , "Group Nature is blank!")
        If mNature = "" Then Err.Raise(1, , "Nature is blank!")

        mQry = "UPDATE AcGroup
                Set 
                GroupName = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowGroupName).Value) & ", 
                ContraGroupName = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowGroupName).Value) & ", 
                GroupUnder = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowGroupUnder).Tag) & ", 
                GroupSimilarTo = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowGroupSimilarTo).Tag) & ", 
                GroupNature = " & AgL.Chk_Text(mGroupNature) & ", 
                Nature = " & AgL.Chk_Text(mNature) & ",
                SysGroup = 'N' 
                Where GroupCode = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "Update Subgroup Set 
                GroupNature = " & AgL.Chk_Text(mGroupNature) & ", 
                Nature = " & AgL.Chk_Text(mNature) & "
                Where GroupCode = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub
    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = "Select H.*, Uh.GroupName As GroupUnderName, Sh.GroupName As GroupSimilarToName
                 From AcGroup H 
                 Left Join AcGroup Uh On H.GroupUnder = Uh.GroupCode
                Left Join AcGroup Sh On H.GroupSimilarTo = Sh.GroupCode
                 Where H.GroupCode='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(DsTemp.Tables(0).Rows(0)("GroupCode"))
                Dgl1.Item(Col1Value, rowGroupName).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("GroupName"))
                Dgl1.Item(Col1Value, rowGroupUnder).Tag = AgL.XNull(DsTemp.Tables(0).Rows(0)("GroupUnder"))
                Dgl1.Item(Col1Value, rowGroupUnder).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("GroupUnderName"))
                Dgl1.Item(Col1Value, rowGroupSimilarTo).Tag = AgL.XNull(DsTemp.Tables(0).Rows(0)("GroupSimilarTo"))
                Dgl1.Item(Col1Value, rowGroupSimilarTo).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("GroupSimilarToName"))
                LblSystemDefine.Text = IIf(AgL.XNull(DsTemp.Tables(0).Rows(0)("SysGroup")) = "Y", "System Define", "User Define")
            End If
        End With
    End Sub
    Private Function FGetRelationalData() As Boolean
        Try
            mQry = " Select Count(*) From SubGroup Where GroupCode = '" & mSearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                MsgBox(" Data Exists For Account Group " & Dgl1.Item(Col1Value, rowGroupName).Value & " In Party Master . Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If

            mQry = " Select Count(*) From AcGroup Where GroupUnder = '" & mSearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                MsgBox(" Data Exists For Account Group " & Dgl1.Item(Col1Value, rowGroupName).Value & " In Account Group Master. Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData")
            FGetRelationalData = True
        End Try
    End Function
    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        Dgl1.CurrentCell = Dgl1.Item(Col1Value, rowGroupName)
        Dgl1.Focus()
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
        mQry = "Select H.GroupCode As SearchCode " &
            " From AcGroup H " &
            " Order By H.GroupName "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub
    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub
    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        Passed = FRestrictSystemDefine()

        If ClsMain.IsEntryLockedWithLockText("AcGroup", "GroupCode", mSearchCode) = True Then
            Passed = False
            Exit Sub
        End If
    End Sub
    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        Passed = FRestrictSystemDefine()
        If Passed = False Then Exit Sub
        Passed = Not FGetRelationalData()

        If ClsMain.IsEntryLockedWithLockText("AcGroup", "GroupCode", mSearchCode) = True Then
            Passed = False
            Exit Sub
        End If
    End Sub
    Private Function FRestrictSystemDefine() As Boolean
        If LblSystemDefine.Text = "System Define" Then
            If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
                If MsgBox("This is a System Define Item.Do You Want To Proceed...?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Topctrl1.FButtonClick(14, True)
                    FRestrictSystemDefine = False
                    Exit Function
                End If
            Else
                MsgBox("Can't Edit System Define Items...!", MsgBoxStyle.Information) : Topctrl1.FButtonClick(14, True)
                FRestrictSystemDefine = False
                Exit Function
            End If
        End If
        FRestrictSystemDefine = True
    End Function
    Private Sub FrmAccountGroup_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        Try
            Dgl1.CurrentCell = Dgl1.Item(Col1Value, rowGroupName)
            Dgl1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message & " [FrmAccountGroup_BaseEvent_Topctrl_tbAdd]")
        End Try
    End Sub
    Private Sub FrmAccountGroup_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Head, 300, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl1, Col1HeadOriginal, 180, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(Dgl1, Col1Mandatory, 12, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(Dgl1, Col1Value, 500, 255, Col1Value, True, False)
            .AddAgTextColumn(Dgl1, Col1LastValue, 300, 255, Col1LastValue, False, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.Columns(Col1Mandatory).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings 2", 5.25, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Dgl1.Columns(Col1Mandatory).DefaultCellStyle.ForeColor = Color.Red
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToAddRows = False
        Dgl1.RowHeadersVisible = False
        Dgl1.ColumnHeadersVisible = False
        Dgl1.BackgroundColor = Me.BackColor
        AgL.GridDesign(Dgl1)
        Dgl1.Anchor = AnchorStyles.Top + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom

        Dgl1.Rows.Add(3)

        Dgl1.Item(Col1Head, rowGroupName).Value = hcGroupName
        Dgl1.Item(Col1Head, rowGroupUnder).Value = hcGroupUnder
        Dgl1.Item(Col1Head, rowGroupSimilarTo).Value = hcGroupSimilarTo

        For I As Integer = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1HeadOriginal, I).Value = Dgl1(Col1Head, I).Value
        Next
        AgL.FSetDimensionCaptionForVerticalGrid(Dgl1, AgL)
    End Sub
    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, AgL.PubSiteCode, "", "", "", "", "")
        FGetSettings = mValue
    End Function
    Private Sub FrmAccountGroup_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dim i As Integer

        For i = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1Value, i).Value = ""
            Dgl1(Col1Value, i).Tag = ""
        Next

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
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub

            Select Case Dgl1.CurrentCell.RowIndex
                Case rowGroupName
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "Select GroupCode, GroupName As Name " &
                            " From AcGroup " &
                            " Order By GroupName "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If
                    CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True

                Case rowGroupUnder, rowGroupSimilarTo
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "Select GroupCode, GroupName As Name " &
                            " From AcGroup " &
                            " Where GroupCode <> '" & mSearchCode & "' " &
                            " Order By GroupName "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Dim mRow As Integer
        Dim mColumn As Integer
        mRow = Dgl1.CurrentCell.RowIndex
        mColumn = Dgl1.CurrentCell.ColumnIndex
        If mColumn = Dgl1.Columns(Col1Value).Index Then
            If Dgl1.Item(Col1Mandatory, mRow).Value <> "" Then
                If Dgl1(Col1Value, mRow).Value = "" Then
                    MsgBox(Dgl1(Col1Head, mRow).Value & " can not be blank.")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        End If
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
            CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = False

            Select Case Dgl1.CurrentCell.RowIndex
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
