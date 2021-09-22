Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain
Public Class FrmUnitConversion
    Inherits AgTemplate.TempMaster

    Dim mQry$

    Public WithEvents DglMain As New AgControls.AgDataGrid
    Public Const ColSNo As String = "SNo"
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1LastValue As String = "Last Value"
    Public Const Col1HeadOriginal As String = "Head Original"


    Dim rowFromUnit As Integer = 0
    Dim rowToUnit As Integer = 1
    Dim rowMultiplier As Integer = 2


    Public Const hcFromUnit As String = "From Unit"
    Public Const hcToUnit As String = "To Unit"
    Public Const hcMultiplier As String = "Multiplier"


#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.PnlMain = New System.Windows.Forms.Panel()
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
        Me.GroupBox1.Location = New System.Drawing.Point(0, 318)
        Me.GroupBox1.Size = New System.Drawing.Size(939, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(14, 322)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(148, 387)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(231, 322)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(401, 322)
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
        Me.GroupBox2.Location = New System.Drawing.Point(704, 322)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(470, 322)
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
        'PnlMain
        '
        Me.PnlMain.Location = New System.Drawing.Point(7, 45)
        Me.PnlMain.Name = "PnlMain"
        Me.PnlMain.Size = New System.Drawing.Size(886, 260)
        Me.PnlMain.TabIndex = 1064
        '
        'FrmUnitConversion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(897, 366)
        Me.Controls.Add(Me.PnlMain)
        Me.MaximizeBox = True
        Me.Name = "FrmUnitConversion"
        Me.Text = "Item Category Master"
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.PnlMain, 0)
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
    Public WithEvents PnlMain As Panel
#End Region

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If DglMain.Item(Col1Value, rowFromUnit).Value.Trim = "" Then Err.Raise(1, , "From Unit Is Required!")
        If DglMain.Item(Col1Value, rowToUnit).Value.Trim = "" Then Err.Raise(1, , "To Unit Is Required!")
        If DglMain.Item(Col1Value, rowMultiplier).Value.Trim = "" Then Err.Raise(1, , "Multiplier Is Required!")

        If DglMain.Item(Col1Value, rowFromUnit).Value = DglMain.Item(Col1Value, rowToUnit).Value Then
            Err.Raise(1, , "From Unit and To Unit can't be same.")
        End If

        If Topctrl1.Mode = "Add" Then
            mQry = "Select Count(*) From UnitConversion Where FromUnit ='" & DglMain.Item(Col1Value, rowFromUnit).Value & "' And ToUnit ='" & DglMain.Item(Col1Value, rowToUnit).Value & "'   "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "From Unit And To Unit Combination Already Exist!")
        Else
            mQry = "Select Count(*) From UnitConversion Where FromUnit ='" & DglMain.Item(Col1Value, rowFromUnit).Value & "' And ToUnit ='" & DglMain.Item(Col1Value, rowToUnit).Value & "' And Code <> '" & mInternalCode & "'"
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "From Unit And To Unit Combination Already Exist!")
        End If
    End Sub
    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        AgL.PubFindQry = "SELECT I.Code As SearchCode, I.FromUnit, I.ToUnit, I.Multiplier " &
                        " FROM UnitConversion I  " &
                        " Where Item Is Null "
        AgL.PubFindQryOrdBy = "[FromUnit]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "UnitConversion"
    End Sub
    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer

        mQry = "UPDATE UnitConversion
                Set 
                FromUnit = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowFromUnit).Value) & ", 
                ToUnit = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowToUnit).Value) & ", 
                Multiplier = " & Val(DglMain.Item(Col1Value, rowMultiplier).Value) & " 
                Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub
    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = "Select H.* From UnitConversion H Where H.Code='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(DsTemp.Tables(0).Rows(0)("Code"))
                DglMain.Item(Col1Value, rowFromUnit).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("FromUnit"))
                DglMain.Item(Col1Value, rowToUnit).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("ToUnit"))
                DglMain.Item(Col1Value, rowMultiplier).Value = AgL.XNull(DsTemp.Tables(0).Rows(0)("Multiplier"))
            End If
        End With
        FrmUnitConversion_BaseFunction_DispText()
    End Sub
    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        DglMain.CurrentCell = DglMain.Item(Col1Value, rowFromUnit)
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
            " From UnitConversion I " &
            " Where Item Is Null " &
            " Order By I.Code "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub
    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub
    Private Sub FrmUnitConversion_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 360, 885)
    End Sub
    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
    End Sub
    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
    End Sub
    Private Sub FrmUnitConversion_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        Try
            ApplyUISetting()
            DglMain.CurrentCell = DglMain.Item(Col1Value, rowFromUnit)
            DglMain.Focus()
        Catch ex As Exception
            MsgBox(ex.Message & " [FrmUnitConversion_BaseEvent_Topctrl_tbAdd]")
        End Try
    End Sub
    Private Sub FrmUnitConversion_BaseFunction_DispText() Handles Me.BaseFunction_DispText
    End Sub
    Private Sub FrmUnitConversion_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        DglMain.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DglMain, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(DglMain, Col1Head, 300, 255, Col1Head, True, True)
            .AddAgTextColumn(DglMain, Col1HeadOriginal, 180, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(DglMain, Col1Mandatory, 12, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(DglMain, Col1Value, 500, 255, Col1Value, True, False)
            .AddAgTextColumn(DglMain, Col1LastValue, 300, 255, Col1LastValue, False, False)
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
        DglMain.BackgroundColor = Me.BackColor
        AgL.GridDesign(DglMain)
        DglMain.Anchor = AnchorStyles.Top + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom
        DglMain.Name = "DglMain"

        DglMain.Rows.Add(3)

        DglMain.Item(Col1Head, rowFromUnit).Value = hcFromUnit
        DglMain.Item(Col1Head, rowToUnit).Value = hcToUnit
        DglMain.Item(Col1Head, rowMultiplier).Value = hcMultiplier

        For I As Integer = 0 To DglMain.Rows.Count - 1
            DglMain(Col1HeadOriginal, I).Value = DglMain(Col1Head, I).Value
        Next

        ApplyUISetting()
    End Sub
    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, AgL.PubSiteCode, "", "", "", "", "")
        FGetSettings = mValue
    End Function
    Private Sub FrmUnitConversion_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dim i As Integer

        For i = 0 To DglMain.Rows.Count - 1
            DglMain(Col1Value, i).Value = ""
            DglMain(Col1Value, i).Tag = ""
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
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DglMain.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim DrTemp As DataRow() = Nothing
        Try
            If DglMain.CurrentCell Is Nothing Then Exit Sub
            bRowIndex = DglMain.CurrentCell.RowIndex
            bColumnIndex = DglMain.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> DglMain.Columns(Col1Value).Index Then Exit Sub

            Select Case DglMain.CurrentCell.RowIndex
                Case rowFromUnit, rowToUnit
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " SELECT Code, Code as Name  FROM Unit where IsActive=1 Order By Code "
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
                    SendKeys.Send("{Tab}")
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
    Private Sub ApplyUISetting()
        ClsMain.GetUISetting(DglMain, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", ClsMain.GridTypeConstants.VerticalGrid)
    End Sub
End Class
