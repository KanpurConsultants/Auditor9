Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class FrmArea
    Inherits AgTemplate.TempMaster

    Dim mQry$

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtDescription = New AgControls.AgTextBox()
        Me.LblDescription = New System.Windows.Forms.Label()
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
        Me.Topctrl1.TabIndex = 3
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 219)
        Me.GroupBox1.Size = New System.Drawing.Size(904, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(14, 223)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(148, 286)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(221, 223)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(401, 223)
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
        Me.GroupBox2.Location = New System.Drawing.Point(704, 223)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(457, 223)
        Me.GBoxDivision.Size = New System.Drawing.Size(135, 44)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Size = New System.Drawing.Size(129, 18)
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
        Me.Label1.Location = New System.Drawing.Point(295, 124)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 666
        Me.Label1.Text = "�"
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
        Me.TxtDescription.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDescription.Location = New System.Drawing.Point(311, 116)
        Me.TxtDescription.MaxLength = 50
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(385, 18)
        Me.TxtDescription.TabIndex = 1
        '
        'LblDescription
        '
        Me.LblDescription.AutoSize = True
        Me.LblDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDescription.Location = New System.Drawing.Point(173, 117)
        Me.LblDescription.Name = "LblDescription"
        Me.LblDescription.Size = New System.Drawing.Size(73, 16)
        Me.LblDescription.TabIndex = 661
        Me.LblDescription.Text = "Description"
        '
        'FrmArea
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(862, 267)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.LblDescription)
        Me.Name = "FrmArea"
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
    Public WithEvents Label1 As System.Windows.Forms.Label
#End Region

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation

        If AgL.RequiredField(TxtDescription, LblDescription.Text) Then passed = False : Exit Sub

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From Area Where Description='" & TxtDescription.Text & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        Else
            mQry = "Select count(*) From Area Where Description='" & TxtDescription.Text & "' And Code <> '" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If
    End Sub

    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = " Where 1=1  "

        AgL.PubFindQry = "SELECT I.Code As SearchCode, I.Description  FROM Area I "
        AgL.PubFindQryOrdBy = "[Description]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Area"
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        mQry = "UPDATE Area " &
                " SET " &
                " Description = " & AgL.Chk_Text(TxtDescription.Text) & " " &
                " Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = "Select Code, Description As Name " &
                " From Area " &
                " Order By Description "
        TxtDescription.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = "Select H.* " &
                " From Area H " &
                " Where H.Code='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtDescription.Text = AgL.XNull(.Rows(0)("Description"))
            End If
        End With
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtDescription.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtDescription.Focus()
    End Sub

    Private Sub TxtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtDescription.KeyDown
        Select Case sender.Name
            Case TxtDescription.Name
                If e.KeyCode = Keys.Enter Then
                    If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                        Topctrl1.FButtonClick(13)
                    End If
                End If
        End Select
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
                " From Area I " & mConStr &
                " Order By I.Description "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmArea_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 300, 885)
    End Sub

    Private Sub TxtManualCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)

    End Sub

    Private Sub TxtItemCategory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtDescription.KeyDown
        Select Case sender.NAME
            Case TxtDescription.Name
                'If e.KeyCode = Keys.Enter Then
                '    If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                '        Topctrl1.FButtonClick(13)
                '        e.Handled = True
                '    End If
                'End If
        End Select
    End Sub

    Private Function FGetRelationalData() As Boolean
        Try
            'mQry = " Select Count(*) From ItemCategory Where Area = '" & mSearchCode & "'"
            'If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
            '    MsgBox(" Data Exists For Item " & TxtDescription.Text & " In Category Master . Can't Delete Entry", MsgBoxStyle.Information)
            '    FGetRelationalData = True
            '    Exit Function
            'End If
        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData")
            FGetRelationalData = True
        End Try
    End Function

    Private Sub ME_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        Passed = Not FGetRelationalData()
    End Sub

    Private Sub FrmArea_BaseEvent_Save_PostTrans(SearchCode As String) Handles Me.BaseEvent_Save_PostTrans
        AgL.ApplyFeature_Area()
    End Sub

    Private Sub FrmArea_BaseEvent_ApproveDeletion_PostTrans(SearchCode As String) Handles Me.BaseEvent_ApproveDeletion_PostTrans
        AgL.ApplyFeature_Area()
    End Sub
    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, TxtDivision.Tag, AgL.PubSiteCode, "", "", "", "", "")
        FGetSettings = mValue
    End Function

    Private Sub FrmArea_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
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
    Public Shared Sub ImportAreaTable(AreaTable As StructArea)
        Dim mQry As String = ""
        If AgL.Dman_Execute("SELECT Count(*) From Area With (NoLock) Where Description = '" & AreaTable.Description & "'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = " INSERT INTO Area(Code, Description, EntryBy, EntryDate, EntryType, EntryStatus, OMSId)
                    Select '" & AreaTable.Code & "' As AreaCode, " & AgL.Chk_Text(AreaTable.Description) & " As Area, 
                    '" & AreaTable.EntryBy & "' As EntryBy, " & AgL.Chk_Date(AreaTable.EntryDate) & " As EntryDate, 
                    '" & AreaTable.EntryType & "' As EntryType, '" & AreaTable.EntryStatus & "' As EntryStatus, 
                    '" & AreaTable.OMSId & "' As OMSId "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Else
            mQry = " UPDATE Area Set OMSId = '" & AreaTable.OMSId & "' 
                    Where Description = '" & AreaTable.Description & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If
    End Sub
    Public Structure StructArea
        Dim Code As String
        Dim Description As String
        Dim EntryBy As String
        Dim EntryDate As String
        Dim EntryType As String
        Dim EntryStatus As String
        Dim OMSId As String
    End Structure
End Class
