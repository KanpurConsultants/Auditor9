Public Class FrmVoucherTypeRestriction
    Inherits AgTemplate.TempMaster
    Dim mQry$
    Public Const ColSNo As String = "Sr"
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1VoucherType As String = "Voucher Type"
    Public Const Col1VtCode As String = "Code"

    Public WithEvents TxtCopyFrom As AgControls.AgTextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents BtnFill As System.Windows.Forms.Button
    Public WithEvents TxtUserName As AgControls.AgTextBox

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.LblWEF = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.LblRateListDetail = New System.Windows.Forms.LinkLabel
        Me.LblUserNameReq = New System.Windows.Forms.Label
        Me.TxtUserName = New AgControls.AgTextBox
        Me.TxtCopyFrom = New AgControls.AgTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.BtnFill = New System.Windows.Forms.Button
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
        Me.GroupBox1.Location = New System.Drawing.Point(0, 400)
        Me.GroupBox1.Size = New System.Drawing.Size(904, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(14, 404)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(148, 404)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(554, 404)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(401, 404)
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
        Me.GroupBox2.Location = New System.Drawing.Point(704, 404)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(278, 404)
        Me.GBoxDivision.Text = "`"
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Tag = ""
        '
        'TxtStatus
        '
        Me.TxtStatus.AgSelectedValue = ""
        Me.TxtStatus.Tag = ""
        '
        'LblWEF
        '
        Me.LblWEF.AutoSize = True
        Me.LblWEF.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblWEF.Location = New System.Drawing.Point(269, 67)
        Me.LblWEF.Name = "LblWEF"
        Me.LblWEF.Size = New System.Drawing.Size(73, 16)
        Me.LblWEF.TabIndex = 689
        Me.LblWEF.Text = "User Name"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(161, 147)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(540, 225)
        Me.Pnl1.TabIndex = 4
        '
        'LblRateListDetail
        '
        Me.LblRateListDetail.BackColor = System.Drawing.Color.SteelBlue
        Me.LblRateListDetail.DisabledLinkColor = System.Drawing.Color.White
        Me.LblRateListDetail.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRateListDetail.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblRateListDetail.LinkColor = System.Drawing.Color.White
        Me.LblRateListDetail.Location = New System.Drawing.Point(161, 126)
        Me.LblRateListDetail.Name = "LblRateListDetail"
        Me.LblRateListDetail.Size = New System.Drawing.Size(128, 19)
        Me.LblRateListDetail.TabIndex = 734
        Me.LblRateListDetail.TabStop = True
        Me.LblRateListDetail.Text = "Voucher Type Detail"
        Me.LblRateListDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblUserNameReq
        '
        Me.LblUserNameReq.AutoSize = True
        Me.LblUserNameReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblUserNameReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblUserNameReq.Location = New System.Drawing.Point(377, 73)
        Me.LblUserNameReq.Name = "LblUserNameReq"
        Me.LblUserNameReq.Size = New System.Drawing.Size(10, 7)
        Me.LblUserNameReq.TabIndex = 736
        Me.LblUserNameReq.Text = "Ä"
        '
        'TxtUserName
        '
        Me.TxtUserName.AgAllowUserToEnableMasterHelp = False
        Me.TxtUserName.AgLastValueTag = Nothing
        Me.TxtUserName.AgLastValueText = Nothing
        Me.TxtUserName.AgMandatory = False
        Me.TxtUserName.AgMasterHelp = False
        Me.TxtUserName.AgNumberLeftPlaces = 0
        Me.TxtUserName.AgNumberNegetiveAllow = False
        Me.TxtUserName.AgNumberRightPlaces = 0
        Me.TxtUserName.AgPickFromLastValue = False
        Me.TxtUserName.AgRowFilter = ""
        Me.TxtUserName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtUserName.AgSelectedValue = Nothing
        Me.TxtUserName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtUserName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtUserName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtUserName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtUserName.Location = New System.Drawing.Point(393, 67)
        Me.TxtUserName.MaxLength = 20
        Me.TxtUserName.Name = "TxtUserName"
        Me.TxtUserName.Size = New System.Drawing.Size(200, 18)
        Me.TxtUserName.TabIndex = 1
        '
        'TxtCopyFrom
        '
        Me.TxtCopyFrom.AgAllowUserToEnableMasterHelp = False
        Me.TxtCopyFrom.AgLastValueTag = Nothing
        Me.TxtCopyFrom.AgLastValueText = Nothing
        Me.TxtCopyFrom.AgMandatory = False
        Me.TxtCopyFrom.AgMasterHelp = False
        Me.TxtCopyFrom.AgNumberLeftPlaces = 0
        Me.TxtCopyFrom.AgNumberNegetiveAllow = False
        Me.TxtCopyFrom.AgNumberRightPlaces = 0
        Me.TxtCopyFrom.AgPickFromLastValue = False
        Me.TxtCopyFrom.AgRowFilter = ""
        Me.TxtCopyFrom.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCopyFrom.AgSelectedValue = Nothing
        Me.TxtCopyFrom.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCopyFrom.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCopyFrom.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCopyFrom.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCopyFrom.Location = New System.Drawing.Point(393, 88)
        Me.TxtCopyFrom.MaxLength = 20
        Me.TxtCopyFrom.Name = "TxtCopyFrom"
        Me.TxtCopyFrom.Size = New System.Drawing.Size(200, 18)
        Me.TxtCopyFrom.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(269, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 16)
        Me.Label2.TabIndex = 741
        Me.Label2.Text = "Copy From"
        '
        'BtnFill
        '
        Me.BtnFill.BackColor = System.Drawing.Color.Transparent
        Me.BtnFill.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFill.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFill.Location = New System.Drawing.Point(649, 123)
        Me.BtnFill.Name = "BtnFill"
        Me.BtnFill.Size = New System.Drawing.Size(52, 23)
        Me.BtnFill.TabIndex = 3
        Me.BtnFill.Text = "Fill"
        Me.BtnFill.UseVisualStyleBackColor = False
        '
        'FrmUserVoucherTypePermission
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(862, 448)
        Me.Controls.Add(Me.BtnFill)
        Me.Controls.Add(Me.TxtCopyFrom)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LblRateListDetail)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.TxtUserName)
        Me.Controls.Add(Me.LblUserNameReq)
        Me.Controls.Add(Me.LblWEF)
        Me.Name = "FrmUserVoucherTypePermission"
        Me.Text = "Quality Master"
        Me.Controls.SetChildIndex(Me.LblWEF, 0)
        Me.Controls.SetChildIndex(Me.LblUserNameReq, 0)
        Me.Controls.SetChildIndex(Me.TxtUserName, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.LblRateListDetail, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.TxtCopyFrom, 0)
        Me.Controls.SetChildIndex(Me.BtnFill, 0)
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

    Public WithEvents LblWEF As System.Windows.Forms.Label
    Public WithEvents Pnl1 As System.Windows.Forms.Panel
    Public WithEvents LblRateListDetail As System.Windows.Forms.LinkLabel
    Friend WithEvents LblUserNameReq As System.Windows.Forms.Label
#End Region

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If AgL.RequiredField(TxtUserName, "User Name") Then passed = False : Exit Sub
        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1VoucherType).Index) Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl1, "" & Dgl1.Columns(Col1VoucherType).Index & "") Then passed = False : Exit Sub

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From User_Exclude_VType Where UserName ='" & TxtUserName.Tag & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code = '" & AgL.PubDivCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Permission For " & TxtUserName.Text & " Already Exist!")
        Else
            mQry = "Select count(*) From User_Exclude_VType Where UserName ='" & TxtUserName.Tag & "'  And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code = '" & AgL.PubDivCode & "' And Code <>'" & mInternalCode & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Permission For " & TxtUserName.Text & " Already Exist!")
        End If

    End Sub

    Public Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        mConStr = "WHERE  H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code = '" & AgL.PubSiteCode & "'    "
        AgL.PubFindQry = "SELECT H.Code, H.UserName [User] " & _
                        " FROM User_Exclude_VType H "
        AgL.PubFindQryOrdBy = "[User]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "User_Exclude_VType"
        MainLineTableCsv = "User_Exclude_VTypeDetail"
        LogTableName = "User_Exclude_VType_Log"
        LogLineTableCsv = "User_Exclude_VTypeDetail_Log"
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        mQry = " UPDATE User_Exclude_VType SET " & _
                " UserName = " & AgL.Chk_Text(TxtUserName.Text) & ",	" & _
                " Site_Code = " & AgL.Chk_Text(AgL.PubSiteCode) & "	" & _
                " Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "Delete From User_Exclude_VTypeDetail Where Code = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        Dim I As Integer
        With Dgl1
            For I = 0 To .RowCount - 1
                If .Item(Col1VoucherType, I).Value <> "" Then
                    mQry = " INSERT INTO User_Exclude_VTypeDetail( Code,	Sr,	UserName, V_Type ) " & _
                            " VALUES ( " & AgL.Chk_Text(mSearchCode) & ", " & Val(I + 1) & " , " & AgL.Chk_Text(TxtUserName.Text) & ", " & AgL.Chk_Text(.Item(Col1VoucherType, I).Tag) & "	) "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            Next
        End With

        AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = "SELECT USER_NAME AS Code , USER_NAME  FROM UserMast  ORDER BY USER_NAME "
        TxtUserName.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
        TxtCopyFrom.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT V_Type AS Code, Description, V_Type  FROM Voucher_Type ORDER BY Description "
        Dgl1.AgHelpDataSet(Col1VoucherType) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FrmQuality1_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1VoucherType, 300, 0, Col1VoucherType, True, False, False)
            .AddAgTextColumn(Dgl1, Col1VtCode, 100, 0, Col1VtCode, True, True, False)
        End With
        AgL.GridDesign(Dgl1)
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 40

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet
        Dim I As Integer

        mQry = " Select H.* From User_Exclude_VType H Where H.Code = '" & mSearchCode & "' "
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtUserName.AgSelectedValue = AgL.XNull(.Rows(0)("UserName"))

                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------

                mQry = " SELECT L.*  " & _
                        " FROM User_Exclude_VTypeDetail L " & _
                        " Where L.Code = '" & SearchCode & "'"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.AgSelectedValue(Col1VoucherType, I) = AgL.XNull(.Rows(I)("V_Type"))
                            Dgl1.Item(Col1VtCode, I).Value = AgL.XNull(.Rows(I)("V_Type"))
                        Next I
                    End If
                End With
            End If
        End With
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtUserName.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtUserName.Focus()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmQuality1_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    End Sub

    Private Sub FrmYarn_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mConStr$ = ""
        mConStr = " WHERE H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code = '" & AgL.PubSiteCode & "'  "
        mQry = " Select H.Code As SearchCode " & _
                " From User_Exclude_VType H " & mConStr
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmItemGroup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 480, 868)
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub

        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub

        If e.KeyCode = Keys.Enter Then
            If Dgl1.CurrentCell.ColumnIndex = 1 Then
                If Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Value Is Nothing Then Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Value = ""
                If Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Value = "" Then
                    If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, "Save") = MsgBoxResult.Yes Then
                        Topctrl1.FButtonClick(13)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub BtnFill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFill.Click
        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
        If TxtCopyFrom.AgSelectedValue = "" Then MsgBox("First Select Copy From User !") : TxtCopyFrom.Focus() : Exit Sub

        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Try
            mQry = " SELECT L.* " & _
                    " FROM User_Exclude_VType H  " & _
                    " LEFT JOIN User_Exclude_VTypeDetail L ON L.Code = H.Code   " & _
                    " WHERE H.UserName = " & AgL.Chk_Text(TxtCopyFrom.AgSelectedValue) & " Order By L.Sr "

            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            With DtTemp
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = AgL.VNull(.Rows(I)("Sr"))
                        Dgl1.AgSelectedValue(Col1VoucherType, I) = AgL.XNull(.Rows(I)("V_Type"))
                        Dgl1.Item(Col1VtCode, I).Value = AgL.XNull(.Rows(I)("V_Type"))
                    Next
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1VoucherType
                    Validating_VType(mRowIndex)

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Validating_VType(ByVal mRow As Integer)
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl1.Item(Col1VoucherType, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1VoucherType, mRow).ToString.Trim = "" Then
                Dgl1.Item(Col1VtCode, mRow).Value = ""
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.Item(Col1VtCode, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("V_Type").Value)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub
End Class
