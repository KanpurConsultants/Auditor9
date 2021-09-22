Public Class FrmRateList
    Inherits AgTemplate.TempMaster

    Public Const ColSNo As String = "Sr"
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Item As String = "Item"
    Public Const Col1RateType As String = "Rate Type"
    Public Const Col1Rate As String = "Rate"
    Public Const Col1PrevRate As String = "Prev Rate"

    Dim mQry$

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.TxtWEF = New AgControls.AgTextBox
        Me.LblWEF = New System.Windows.Forms.Label
        Me.TxtRate = New AgControls.AgTextBox
        Me.LblRate = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.LblRateListDetail = New System.Windows.Forms.LinkLabel
        Me.BtnFill = New System.Windows.Forms.Button
        Me.LblDescriptionReq = New System.Windows.Forms.Label
        Me.TxtRateType = New AgControls.AgTextBox
        Me.Label1 = New System.Windows.Forms.Label
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
        Me.Topctrl1.TabIndex = 8
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
        Me.TxtStatus.Location = New System.Drawing.Point(3, 23)
        Me.TxtStatus.Size = New System.Drawing.Size(142, 18)
        Me.TxtStatus.Tag = ""
        '
        'TxtWEF
        '
        Me.TxtWEF.AgAllowUserToEnableMasterHelp = False
        Me.TxtWEF.AgMandatory = True
        Me.TxtWEF.AgMasterHelp = True
        Me.TxtWEF.AgNumberLeftPlaces = 0
        Me.TxtWEF.AgNumberNegetiveAllow = False
        Me.TxtWEF.AgNumberRightPlaces = 0
        Me.TxtWEF.AgPickFromLastValue = False
        Me.TxtWEF.AgRowFilter = ""
        Me.TxtWEF.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtWEF.AgSelectedValue = Nothing
        Me.TxtWEF.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtWEF.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtWEF.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtWEF.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtWEF.Location = New System.Drawing.Point(298, 56)
        Me.TxtWEF.MaxLength = 20
        Me.TxtWEF.Name = "TxtWEF"
        Me.TxtWEF.Size = New System.Drawing.Size(129, 18)
        Me.TxtWEF.TabIndex = 0
        '
        'LblWEF
        '
        Me.LblWEF.AutoSize = True
        Me.LblWEF.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblWEF.Location = New System.Drawing.Point(207, 57)
        Me.LblWEF.Name = "LblWEF"
        Me.LblWEF.Size = New System.Drawing.Size(38, 16)
        Me.LblWEF.TabIndex = 689
        Me.LblWEF.Text = "WEF"
        '
        'TxtRate
        '
        Me.TxtRate.AgAllowUserToEnableMasterHelp = False
        Me.TxtRate.AgMandatory = False
        Me.TxtRate.AgMasterHelp = False
        Me.TxtRate.AgNumberLeftPlaces = 0
        Me.TxtRate.AgNumberNegetiveAllow = False
        Me.TxtRate.AgNumberRightPlaces = 0
        Me.TxtRate.AgPickFromLastValue = False
        Me.TxtRate.AgRowFilter = ""
        Me.TxtRate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtRate.AgSelectedValue = Nothing
        Me.TxtRate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtRate.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtRate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtRate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRate.Location = New System.Drawing.Point(298, 76)
        Me.TxtRate.MaxLength = 20
        Me.TxtRate.Name = "TxtRate"
        Me.TxtRate.Size = New System.Drawing.Size(129, 18)
        Me.TxtRate.TabIndex = 5
        '
        'LblRate
        '
        Me.LblRate.AutoSize = True
        Me.LblRate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRate.Location = New System.Drawing.Point(207, 77)
        Me.LblRate.Name = "LblRate"
        Me.LblRate.Size = New System.Drawing.Size(35, 16)
        Me.LblRate.TabIndex = 712
        Me.LblRate.Text = "Rate"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(165, 154)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(535, 240)
        Me.Pnl1.TabIndex = 7
        '
        'LblRateListDetail
        '
        Me.LblRateListDetail.BackColor = System.Drawing.Color.SteelBlue
        Me.LblRateListDetail.DisabledLinkColor = System.Drawing.Color.White
        Me.LblRateListDetail.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRateListDetail.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblRateListDetail.LinkColor = System.Drawing.Color.White
        Me.LblRateListDetail.Location = New System.Drawing.Point(165, 134)
        Me.LblRateListDetail.Name = "LblRateListDetail"
        Me.LblRateListDetail.Size = New System.Drawing.Size(128, 19)
        Me.LblRateListDetail.TabIndex = 734
        Me.LblRateListDetail.TabStop = True
        Me.LblRateListDetail.Text = "Rate List Detail"
        Me.LblRateListDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnFill
        '
        Me.BtnFill.BackColor = System.Drawing.Color.Transparent
        Me.BtnFill.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFill.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFill.Location = New System.Drawing.Point(665, 130)
        Me.BtnFill.Name = "BtnFill"
        Me.BtnFill.Size = New System.Drawing.Size(35, 23)
        Me.BtnFill.TabIndex = 6
        Me.BtnFill.Text = "Fill"
        Me.BtnFill.UseVisualStyleBackColor = False
        '
        'LblDescriptionReq
        '
        Me.LblDescriptionReq.AutoSize = True
        Me.LblDescriptionReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblDescriptionReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblDescriptionReq.Location = New System.Drawing.Point(281, 64)
        Me.LblDescriptionReq.Name = "LblDescriptionReq"
        Me.LblDescriptionReq.Size = New System.Drawing.Size(10, 7)
        Me.LblDescriptionReq.TabIndex = 736
        Me.LblDescriptionReq.Text = "Ä"
        '
        'TxtRateType
        '
        Me.TxtRateType.AgAllowUserToEnableMasterHelp = False
        Me.TxtRateType.AgMandatory = False
        Me.TxtRateType.AgMasterHelp = False
        Me.TxtRateType.AgNumberLeftPlaces = 0
        Me.TxtRateType.AgNumberNegetiveAllow = False
        Me.TxtRateType.AgNumberRightPlaces = 0
        Me.TxtRateType.AgPickFromLastValue = False
        Me.TxtRateType.AgRowFilter = ""
        Me.TxtRateType.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtRateType.AgSelectedValue = Nothing
        Me.TxtRateType.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtRateType.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtRateType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtRateType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRateType.Location = New System.Drawing.Point(507, 57)
        Me.TxtRateType.MaxLength = 20
        Me.TxtRateType.Name = "TxtRateType"
        Me.TxtRateType.Size = New System.Drawing.Size(149, 18)
        Me.TxtRateType.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(433, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 16)
        Me.Label1.TabIndex = 740
        Me.Label1.Text = "Rate Type"
        '
        'FrmRateList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(862, 448)
        Me.Controls.Add(Me.BtnFill)
        Me.Controls.Add(Me.LblRateListDetail)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.TxtRateType)
        Me.Controls.Add(Me.TxtRate)
        Me.Controls.Add(Me.LblRate)
        Me.Controls.Add(Me.TxtWEF)
        Me.Controls.Add(Me.LblDescriptionReq)
        Me.Controls.Add(Me.LblWEF)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FrmRateList"
        Me.Text = "Quality Master"
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.LblWEF, 0)
        Me.Controls.SetChildIndex(Me.LblDescriptionReq, 0)
        Me.Controls.SetChildIndex(Me.TxtWEF, 0)
        Me.Controls.SetChildIndex(Me.LblRate, 0)
        Me.Controls.SetChildIndex(Me.TxtRate, 0)
        Me.Controls.SetChildIndex(Me.TxtRateType, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.LblRateListDetail, 0)
        Me.Controls.SetChildIndex(Me.BtnFill, 0)
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

    Public WithEvents TxtWEF As AgControls.AgTextBox
    Public WithEvents LblWEF As System.Windows.Forms.Label
    Public WithEvents TxtRate As AgControls.AgTextBox
    Public WithEvents LblRate As System.Windows.Forms.Label
    Public WithEvents Pnl1 As System.Windows.Forms.Panel
    Public WithEvents LblRateListDetail As System.Windows.Forms.LinkLabel
    Public WithEvents BtnFill As System.Windows.Forms.Button
    Friend WithEvents LblDescriptionReq As System.Windows.Forms.Label
    Public WithEvents TxtRateType As AgControls.AgTextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
#End Region

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim RateChanged As Boolean = False

        If AgL.RequiredField(TxtWEF, "WEF") Then passed = False : Exit Sub

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1Rate, I).Value) <> Val(.Item(Col1PrevRate, I).Value) Then
                        RateChanged = True
                    End If
                End If
            Next
        End With

        If RateChanged = False Then MsgBox("No Changes Found In Rates.Can't Continue.", MsgBoxStyle.Information) : passed = False : Exit Sub
    End Sub

    Public Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1  "
        AgL.PubFindQry = "SELECT H.Code, H.WEF, Rt.Description As RateType " & _
                        " FROM RateList H " & _
                        " LEFT JOIN RateType Rt ON H.RateType = Rt.Code "
        AgL.PubFindQryOrdBy = "[WEF]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "RateList"
        MainLineTableCsv = "RateListDetail"
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer = 0, bSr As Integer = 0

        mQry = "UPDATE RateList " & _
                " SET " & _
                " WEF = " & AgL.Chk_Text(TxtWEF.Text) & ", " & _
                " RateType = " & AgL.Chk_Text(TxtRateType.AgSelectedValue) & " " & _
                " Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "Delete From RateListDetail Where Code = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        With Dgl1
            For I = 0 To .RowCount - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1Rate, I).Value) <> Val(.Item(Col1PrevRate, I).Value) Then
                        bSr += 1
                        mQry = "INSERT INTO RateListDetail(Code, Sr, WEF, Item, RateType, Rate) " & _
                               " VALUES (" & AgL.Chk_Text(SearchCode) & ", " & _
                               " " & bSr & ", " & AgL.Chk_Text(TxtWEF.Text) & ", " & _
                               " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Item, I)) & ", " & _
                               " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1RateType, I)) & ", " & _
                               " " & Val(Dgl1.Item(Col1Rate, I).Value) & " " & _
                               " ) "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                End If
            Next
        End With
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = " Select I.Code, I.Description As Item From Item I "
        Dgl1.AgHelpDataSet(Col1Item) = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT H.Code, H.Description FROM RateType H "
        Dgl1.AgHelpDataSet(Col1RateType) = AgL.FillData(mQry, AgL.GCn)
        TxtRateType.AgHelpDataSet = Dgl1.AgHelpDataSet(Col1RateType)
    End Sub

    Private Sub FrmQuality1_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Item, 200, 0, Col1Item, True, False, False)
            .AddAgTextColumn(Dgl1, Col1RateType, 100, 0, Col1RateType, True, False, False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 80, 5, 2, False, Col1Rate, True, False, False)
            .AddAgNumberColumn(Dgl1, Col1PrevRate, 80, 5, 2, False, Col1PrevRate, False, False, False)
        End With
        AgL.GridDesign(Dgl1)
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 25
    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet
        Dim I As Integer

        mQry = " Select H.* From RateList H Where H.Code = '" & mSearchCode & "' "
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtWEF.Text = AgL.XNull(.Rows(0)("WEF"))
                TxtRateType.AgSelectedValue = AgL.XNull(.Rows(0)("RateType"))

                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------

                mQry = "Select * from RateListDetail Where Code = '" & SearchCode & "'"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.AgSelectedValue(Col1Item, I) = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.AgSelectedValue(Col1RateType, I) = AgL.XNull(.Rows(I)("RateType"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                        Next I
                    End If
                End With
                Calculation()
            End If
        End With
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtWEF.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtWEF.Focus()
    End Sub

    Private Sub Control_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtWEF.Enter
        Try
            Select Case sender.name
            End Select
        Catch ex As Exception
        End Try
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

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmQuality1_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    End Sub

    Private Sub FrmYarn_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mConStr$ = ""
        mConStr = " WHERE 1=1  "
        mQry = " Select H.Code As SearchCode " & _
                " From RateList H " & mConStr
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
                        Topctrl1.FButtonClick(11)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub TxtVendor_Enter(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub BtnFill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFill.Click
    '    Dim DtTemp As DataTable = Nothing
    '    Dim I As Integer = 0
    '    Dim mConStr$ = ""
    '    Try
    '        mConStr = " Where Rt.Code = '" & TxtRateType.AgSelectedValue & "'"

    '        mQry = " SELECT I.Code As Item, Rt.Code As RateType " & _
    '                " FROM Item I " & _
    '                " LEFT JOIN RateType  Rt ON 1=1 " & mConStr
    '        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
    '        With DtTemp
    '            Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    '            If .Rows.Count > 0 Then
    '                For I = 0 To .Rows.Count - 1
    '                    Dgl1.Rows.Add()
    '                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
    '                    Dgl1.AgSelectedValue(Col1Item, I) = AgL.XNull(.Rows(I)("Item"))
    '                    Dgl1.AgSelectedValue(Col1RateType, I) = AgL.XNull(.Rows(I)("RateType"))
    '                    Dgl1.Item(Col1Rate, I).Value = TxtRate.Text
    '                Next
    '            End If
    '        End With
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub BtnFill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFill.Click
        Dim I As Integer = 0
        Try
            mQry = " SELECT I.Code AS SearchCode,I.Description," & _
                            " I.EntryBy AS [Entry_By], I.EntryDate AS [Entry_Date], I.EntryType AS [Entry_Type], " & _
                            " RD.Description AS Design, RS.Description AS Size  " & _
                            " FROM Item I " & _
                            " LEFT JOIN RUG_Design RD ON RD.Code = I.Design  " & _
                            " LEFT JOIN Rug_Size RS ON RS.Code = I.Size  " & _
                            " WHERE I.ItemType = '" & ClsMain.ItemType.FinishedMaterial & "' "
            AgL.PubFindQryOrdBy = "[Description]"

            Dim FrmObj As AgTemplate.FrmReportWindow = New AgTemplate.FrmReportWindow(mQry, "Select Items")
            FrmObj.ShowDialog()

            If FrmObj.DGL1.Rows.Count > 0 Then
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                For I = 0 To FrmObj.DGL1.Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                    Dgl1.AgSelectedValue(Col1Item, I) = FrmObj.DGL1.Item("SearchCode", I).Value
                    Dgl1.AgSelectedValue(Col1RateType, I) = TxtRateType.AgSelectedValue
                    If Val(TxtRate.Text) <> 0 Then
                        Dgl1.Item(Col1Rate, I).Value = TxtRate.Text
                    Else
                        mQry = " SELECT TOP 1 L.Rate FROM RateListDetail L WHERE L.Item = '" & Dgl1.AgSelectedValue(Col1Item, I) & "'  AND IsNull(L.RateType,'') = '" & Dgl1.AgSelectedValue(Col1RateType, I) & "'  ORDER BY L.WEF DESC "
                        Dgl1.Item(Col1Rate, I).Value = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
                        Dgl1.Item(Col1PrevRate, I).Value = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
