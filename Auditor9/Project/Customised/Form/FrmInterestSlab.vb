Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants

Public Class FrmInterestSlab
    Inherits AgTemplate.TempMaster

    Dim mQry$
    Public Const ColSNo As String = "SNo"
    Public WithEvents DGL1 As New AgControls.AgDataGrid
    Public Const Col1DaysGreaterThan As String = "Days Greater Than"
    Public WithEvents Label2 As Label
    Public WithEvents TxtLeaverageDays As AgControls.AgTextBox
    Public WithEvents Label3 As Label
    Public Const Col1InterestPer As String = "Interest %"



#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtDescription = New AgControls.AgTextBox()
        Me.LblDescription = New System.Windows.Forms.Label()
        Me.LblIsSystemDefine = New System.Windows.Forms.Label()
        Me.ChkIsSystemDefine = New System.Windows.Forms.CheckBox()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtLeaverageDays = New AgControls.AgTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
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
        Me.Topctrl1.Size = New System.Drawing.Size(862, 41)
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 313)
        Me.GroupBox1.Size = New System.Drawing.Size(904, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(14, 317)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(148, 382)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(238, 317)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(401, 317)
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
        Me.GroupBox2.Location = New System.Drawing.Point(704, 317)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(466, 317)
        Me.GBoxDivision.Size = New System.Drawing.Size(139, 44)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Size = New System.Drawing.Size(133, 18)
        Me.TxtDivision.Tag = ""
        '
        'TxtStatus
        '
        Me.TxtStatus.AgSelectedValue = ""
        Me.TxtStatus.Tag = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(304, 81)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 666
        Me.Label1.Text = "Ä"
        '
        'TxtDescription
        '
        Me.TxtDescription.AgAllowUserToEnableMasterHelp = False
        Me.TxtDescription.AgLastValueTag = Nothing
        Me.TxtDescription.AgLastValueText = Nothing
        Me.TxtDescription.AgMandatory = True
        Me.TxtDescription.AgMasterHelp = True
        Me.TxtDescription.AgNumberLeftPlaces = 0
        Me.TxtDescription.AgNumberNegetiveAllow = False
        Me.TxtDescription.AgNumberRightPlaces = 0
        Me.TxtDescription.AgPickFromLastValue = False
        Me.TxtDescription.AgRowFilter = ""
        Me.TxtDescription.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDescription.AgSelectedValue = Nothing
        Me.TxtDescription.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDescription.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDescription.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDescription.Location = New System.Drawing.Point(322, 73)
        Me.TxtDescription.MaxLength = 50
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(385, 20)
        Me.TxtDescription.TabIndex = 3
        '
        'LblDescription
        '
        Me.LblDescription.AutoSize = True
        Me.LblDescription.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDescription.Location = New System.Drawing.Point(168, 74)
        Me.LblDescription.Name = "LblDescription"
        Me.LblDescription.Size = New System.Drawing.Size(116, 18)
        Me.LblDescription.TabIndex = 661
        Me.LblDescription.Text = "Interest Slab"
        '
        'LblIsSystemDefine
        '
        Me.LblIsSystemDefine.AutoSize = True
        Me.LblIsSystemDefine.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIsSystemDefine.ForeColor = System.Drawing.Color.Red
        Me.LblIsSystemDefine.Location = New System.Drawing.Point(25, 207)
        Me.LblIsSystemDefine.Name = "LblIsSystemDefine"
        Me.LblIsSystemDefine.Size = New System.Drawing.Size(96, 15)
        Me.LblIsSystemDefine.TabIndex = 1061
        Me.LblIsSystemDefine.Text = "IsSystemDefine"
        '
        'ChkIsSystemDefine
        '
        Me.ChkIsSystemDefine.AutoSize = True
        Me.ChkIsSystemDefine.BackColor = System.Drawing.Color.Transparent
        Me.ChkIsSystemDefine.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkIsSystemDefine.ForeColor = System.Drawing.Color.Red
        Me.ChkIsSystemDefine.Location = New System.Drawing.Point(11, 208)
        Me.ChkIsSystemDefine.Name = "ChkIsSystemDefine"
        Me.ChkIsSystemDefine.Size = New System.Drawing.Size(15, 14)
        Me.ChkIsSystemDefine.TabIndex = 1060
        Me.ChkIsSystemDefine.UseVisualStyleBackColor = False
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl1.Location = New System.Drawing.Point(324, 167)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(283, 139)
        Me.Pnl1.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(304, 104)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 7)
        Me.Label2.TabIndex = 1064
        Me.Label2.Text = "Ä"
        '
        'TxtLeaverageDays
        '
        Me.TxtLeaverageDays.AgAllowUserToEnableMasterHelp = False
        Me.TxtLeaverageDays.AgLastValueTag = Nothing
        Me.TxtLeaverageDays.AgLastValueText = Nothing
        Me.TxtLeaverageDays.AgMandatory = True
        Me.TxtLeaverageDays.AgMasterHelp = True
        Me.TxtLeaverageDays.AgNumberLeftPlaces = 3
        Me.TxtLeaverageDays.AgNumberNegetiveAllow = False
        Me.TxtLeaverageDays.AgNumberRightPlaces = 0
        Me.TxtLeaverageDays.AgPickFromLastValue = False
        Me.TxtLeaverageDays.AgRowFilter = ""
        Me.TxtLeaverageDays.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtLeaverageDays.AgSelectedValue = Nothing
        Me.TxtLeaverageDays.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtLeaverageDays.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtLeaverageDays.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtLeaverageDays.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLeaverageDays.Location = New System.Drawing.Point(322, 96)
        Me.TxtLeaverageDays.MaxLength = 50
        Me.TxtLeaverageDays.Name = "TxtLeaverageDays"
        Me.TxtLeaverageDays.Size = New System.Drawing.Size(385, 20)
        Me.TxtLeaverageDays.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(168, 97)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(141, 18)
        Me.Label3.TabIndex = 1063
        Me.Label3.Text = "Leaverage Days"
        '
        'FrmInterestSlab
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(862, 361)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtLeaverageDays)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.LblIsSystemDefine)
        Me.Controls.Add(Me.ChkIsSystemDefine)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.LblDescription)
        Me.Name = "FrmInterestSlab"
        Me.Text = "Quality Master"
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.LblDescription, 0)
        Me.Controls.SetChildIndex(Me.TxtDescription, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.ChkIsSystemDefine, 0)
        Me.Controls.SetChildIndex(Me.LblIsSystemDefine, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.TxtLeaverageDays, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
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

    Public WithEvents LblDescription As System.Windows.Forms.Label
    Public WithEvents TxtDescription As AgControls.AgTextBox
    Public WithEvents LblIsSystemDefine As System.Windows.Forms.Label
    Friend WithEvents ChkIsSystemDefine As System.Windows.Forms.CheckBox
    Public WithEvents Pnl1 As Panel
    Public WithEvents Label1 As System.Windows.Forms.Label
#End Region

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If TxtDescription.Text.Trim = "" Then Err.Raise(1, , "Description Is Required!")

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From InterestSlab Where Description='" & TxtDescription.Text & "' And " & AgTemplate.ClsMain.RetDivFilterStr & "  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        Else
            mQry = "Select count(*) From InterestSlab Where Description='" & TxtDescription.Text & "' And Code<>'" & mInternalCode & "' And " & AgTemplate.ClsMain.RetDivFilterStr & "  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If
    End Sub

    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        AgL.PubFindQry = "SELECT I.Code, I.Description as Item_Group 
                        FROM InterestSlab I "
        AgL.PubFindQryOrdBy = "[Description]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "InterestSlab"
        MainLineTableCsv = "InterestSlabDetail"
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim DsTemp As DataSet
        Dim I As Integer

        mQry = "UPDATE InterestSlab 
                Set 
                Description = " & AgL.Chk_Text(TxtDescription.Text) & ", 
                LeaverageDays = " & Val(TxtLeaverageDays.Text) & ", 
                IsSystemDefine = " & Val(IIf(ChkIsSystemDefine.Checked, 1, 0)) & ",
                UploadDate = Null 
                Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "Delete from InterestSlabDetail where Code = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        For I = 0 To DGL1.Rows.Count - 1
            If AgL.XNull(DGL1.Item(Col1DaysGreaterThan, I).Value) <> "" Then
                mQry = " Insert Into InterestSlabDetail (Code,DaysGreaterThan, InterestRate) " &
                       " Values ('" & SearchCode & "', " & Val(DGL1.Item(Col1DaysGreaterThan, I).Value) & ", " & Val(DGL1.Item(Col1InterestPer, I).Value) & " )"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
        Next
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = "Select Code, Description As Name " &
                " From InterestSlab " &
                " Order By Description"
        TxtDescription.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

    End Sub
    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = "Select H.*  " &
            " From InterestSlab H " &
            " Where H.Code='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtDescription.Text = AgL.XNull(.Rows(0)("Description"))
                TxtLeaverageDays.Text = AgL.VNull(.Rows(0)("LeaverageDays"))

                ChkIsSystemDefine.Checked = AgL.VNull(.Rows(0)("IsSystemDefine"))
                LblIsSystemDefine.Text = IIf(AgL.VNull(.Rows(0)("IsSystemDefine")) = 0, "User Define", "System Define")
                ChkIsSystemDefine.Enabled = False
            End If
        End With


        Dim I As Integer
        mQry = " Select L.DaysGreaterThan, L.InterestRate 
                        From InterestSlabDetail L 
                        Where L.Code='" & SearchCode & "' 
                        Order By L.DaysGreaterThan "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            DGL1.RowCount = 1
            DGL1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    DGL1.Rows.Add()
                    DGL1.Item(ColSNo, I).Value = DGL1.Rows.Count - 1
                    DGL1.Item(Col1DaysGreaterThan, I).Value = Format(AgL.VNull(.Rows(I)("DaysGreaterThan")), "0.00")
                    DGL1.Item(Col1InterestPer, I).Value = Format(AgL.VNull(.Rows(I)("InterestRate")), "0.00")
                Next I
                DGL1.Visible = True
            Else
                DGL1.Visible = False
            End If
        End With
        FrmInterestSlab_BaseFunction_DispText()
    End Sub

    'Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
    '    TxtDescription.Focus()
    'End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtDescription.Focus()
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
                " From InterestSlab I " &
                " Order By I.Description "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmInterestSlab_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 325, 885)
        FManageSystemDefine()
    End Sub

    Private Sub TxtItemCategory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
        End If
    End Sub

    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim DtTemp As DataTable = Nothing
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.NAME
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        Passed = FRestrictSystemDefine()
    End Sub

    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        Passed = FRestrictSystemDefine()
    End Sub

    Private Sub ChkIsSystemDefine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkIsSystemDefine.Click
        FManageSystemDefine()
    End Sub

    Private Sub FManageSystemDefine()
        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
            ChkIsSystemDefine.Visible = True
            ChkIsSystemDefine.Enabled = True
        Else
            ChkIsSystemDefine.Visible = False
            ChkIsSystemDefine.Enabled = False
        End If

        If ChkIsSystemDefine.Checked Then
            LblIsSystemDefine.Text = "System Define"
        Else
            LblIsSystemDefine.Text = "User Define"
        End If
    End Sub

    Private Function FRestrictSystemDefine() As Boolean
        If ChkIsSystemDefine.Checked = True Then
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
        FManageSystemDefine()
        FRestrictSystemDefine = True
    End Function

    Private Sub FrmInterestSlab_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        ChkIsSystemDefine.Checked = False
        FManageSystemDefine()
    End Sub

    Private Sub FrmInterestSlab_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        DGL1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DGL1, ColSNo, 40, 5, ColSNo, False, True, False)

            .AddAgNumberColumn(DGL1, Col1DaysGreaterThan, 160, 3, 0, True, Col1DaysGreaterThan, True, False, True)
            .AddAgNumberColumn(DGL1, Col1InterestPer, 90, 2, 2, True, Col1InterestPer, True, False, True)
        End With
        AgL.AddAgDataGrid(DGL1, Pnl1)
        DGL1.EnableHeadersVisualStyles = False
        DGL1.AgSkipReadOnlyColumns = True
        DGL1.RowHeadersVisible = False
        AgL.GridDesign(DGL1)
    End Sub

    Private Sub Txt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            Select Case sender.name
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Dgl1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGL1.KeyDown
        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub

        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If

        If e.Control Or e.Shift Or e.Alt Then Exit Sub

        If e.KeyCode = Keys.Enter Then
            If DGL1.CurrentCell.ColumnIndex = DGL1.Columns(Col1DaysGreaterThan).Index Then
                If DGL1.Item(DGL1.CurrentCell.ColumnIndex, DGL1.CurrentCell.RowIndex).Value Is Nothing Then DGL1.Item(DGL1.CurrentCell.ColumnIndex, DGL1.CurrentCell.RowIndex).Value = ""
                If DGL1.Item(DGL1.CurrentCell.ColumnIndex, DGL1.CurrentCell.RowIndex).Value = "" Then
                    If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, "Save") = MsgBoxResult.Yes Then
                        Topctrl1.FButtonClick(13)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub FrmInterestSlab_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        ChkIsSystemDefine.Enabled = False
    End Sub

    Private Sub FrmInterestSlab_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        DGL1.RowCount = 1 : DGL1.Rows.Clear()
    End Sub
    Public Shared Sub ImportInterestSlabTable(InterestSlabTableList As StructInterestSlab())
        Dim mQry As String = ""
        If AgL.VNull(AgL.Dman_Execute("Select Count(*) From InterestSlab With (NoLock) where Description = " & AgL.Chk_Text(InterestSlabTableList(0).Description) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar) = 0 Then
            mQry = " INSERT INTO InterestSlab (Code, Description, LeaverageDays, EntryBy, EntryDate, 
                    EntryType, EntryStatus, Status, Div_Code, IsSystemDefine, OMSId) 
                    Select '" & InterestSlabTableList(0).Code & "' As Code, 
                    " & AgL.Chk_Text(InterestSlabTableList(0).Description) & " As Description, 
                    " & Val(InterestSlabTableList(0).LeaverageDays) & " As LeaverageDays, 
                    " & AgL.Chk_Text(InterestSlabTableList(0).EntryBy) & " As EntryBy, 
                    " & AgL.Chk_Date(InterestSlabTableList(0).EntryDate) & " As EntryDate, 
                    " & AgL.Chk_Text(InterestSlabTableList(0).EntryType) & " As EntryType, 
                    " & AgL.Chk_Text(InterestSlabTableList(0).EntryStatus) & " As EntryStatus, 
                    " & AgL.Chk_Text(InterestSlabTableList(0).Status) & " As Status, 
                    " & AgL.Chk_Text(InterestSlabTableList(0).Div_Code) & " As Div_Code, 
                    " & AgL.Chk_Text(InterestSlabTableList(0).IsSystemDefine) & " As IsSystemDefine, 
                    " & AgL.Chk_Text(InterestSlabTableList(0).OMSId) & " As OMSId "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Else
            Dim bIntrestSlabCode As String = AgL.XNull(AgL.Dman_Execute("Select Code 
                        From InterestSlab With (NoLock) 
                        Where Description = " & AgL.Chk_Text(InterestSlabTableList(0).Description) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar)

            mQry = " UPDATE InterestSlab Set LeaverageDays = " & Val(InterestSlabTableList(0).LeaverageDays) & "
                    Where Code = '" & bIntrestSlabCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mQry = " Delete From InterestSlabDetail Where Code = '" & bIntrestSlabCode & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            InterestSlabTableList(0).Code = bIntrestSlabCode
        End If

        For I As Integer = 0 To InterestSlabTableList.Length - 1
            If InterestSlabTableList(I).Line_DaysGreaterThan IsNot Nothing Then
                mQry = " INSERT INTO InterestSlabDetail(Code, DaysGreaterThan, InterestRate) 
                    Select '" & InterestSlabTableList(0).Code & "' As Code, 
                    " & Val(InterestSlabTableList(I).Line_DaysGreaterThan) & " As DaysGreaterThan, 
                    " & Val(InterestSlabTableList(I).Line_InterestRate) & " As InterestRate "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
        Next
    End Sub
    Public Structure StructInterestSlab
        Dim Code As String
        Dim Description As String
        Dim LeaverageDays As String
        Dim EntryBy As String
        Dim EntryDate As String
        Dim EntryType As String
        Dim EntryStatus As String
        Dim Status As String
        Dim Div_Code As String
        Dim IsSystemDefine As String
        Dim LockText As String
        Dim OMSId As String

        Dim Line_DaysGreaterThan As String
        Dim Line_InterestRate As String
        Dim Line_OMSId As String
    End Structure
End Class
