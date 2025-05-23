Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants

Public Class FrmPaymentMode

    Inherits AgTemplate.TempMaster

    Dim mQry$

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtDescription = New AgControls.AgTextBox()
        Me.LblDescription = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtPostToAc = New AgControls.AgTextBox()
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(148, 295)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(230, 223)
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
        Me.GBoxDivision.Location = New System.Drawing.Point(468, 223)
        Me.GBoxDivision.Size = New System.Drawing.Size(147, 44)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Size = New System.Drawing.Size(141, 18)
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
        Me.Label1.Location = New System.Drawing.Point(295, 102)
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
        Me.TxtDescription.Location = New System.Drawing.Point(311, 94)
        Me.TxtDescription.MaxLength = 50
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(385, 18)
        Me.TxtDescription.TabIndex = 1
        '
        'LblDescription
        '
        Me.LblDescription.AutoSize = True
        Me.LblDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDescription.Location = New System.Drawing.Point(173, 95)
        Me.LblDescription.Name = "LblDescription"
        Me.LblDescription.Size = New System.Drawing.Size(73, 16)
        Me.LblDescription.TabIndex = 661
        Me.LblDescription.Text = "Description"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(295, 123)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 7)
        Me.Label2.TabIndex = 674
        Me.Label2.Text = "�"
        '
        'TxtPostToAc
        '
        Me.TxtPostToAc.AgAllowUserToEnableMasterHelp = False
        Me.TxtPostToAc.AgLastValueTag = Nothing
        Me.TxtPostToAc.AgLastValueText = Nothing
        Me.TxtPostToAc.AgMandatory = True
        Me.TxtPostToAc.AgMasterHelp = False
        Me.TxtPostToAc.AgNumberLeftPlaces = 0
        Me.TxtPostToAc.AgNumberNegetiveAllow = False
        Me.TxtPostToAc.AgNumberRightPlaces = 0
        Me.TxtPostToAc.AgPickFromLastValue = False
        Me.TxtPostToAc.AgRowFilter = ""
        Me.TxtPostToAc.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPostToAc.AgSelectedValue = Nothing
        Me.TxtPostToAc.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPostToAc.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPostToAc.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPostToAc.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPostToAc.Location = New System.Drawing.Point(311, 114)
        Me.TxtPostToAc.MaxLength = 50
        Me.TxtPostToAc.Name = "TxtPostToAc"
        Me.TxtPostToAc.Size = New System.Drawing.Size(385, 18)
        Me.TxtPostToAc.TabIndex = 672
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(173, 115)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 16)
        Me.Label3.TabIndex = 673
        Me.Label3.Text = "Post To A/c"
        '
        'FrmPaymentMode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(862, 267)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtPostToAc)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.LblDescription)
        Me.Name = "FrmPaymentMode"
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
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.TxtPostToAc, 0)
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
    Public WithEvents Label2 As Label
    Public WithEvents TxtPostToAc As AgControls.AgTextBox
    Public WithEvents Label3 As Label
    Public WithEvents Label1 As System.Windows.Forms.Label
#End Region

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation

        If AgL.RequiredField(TxtDescription, LblDescription.Text) Then passed = False : Exit Sub

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From PaymentMode Where Description='" & TxtDescription.Text & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        Else
            mQry = "Select count(*) From PaymentMode Where Description='" & TxtDescription.Text & "' And Code <> '" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If
    End Sub

    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = " Where 1=1  "

        AgL.PubFindQry = "SELECT I.Code As SearchCode, I.Description  FROM PaymentMode I "
        AgL.PubFindQryOrdBy = "[Description]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "PaymentMode"
        MainLineTableCsv = "PaymentModeAccount"
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim mCode As String

        mQry = "UPDATE PaymentMode " &
                " SET " &
                " Description = " & AgL.Chk_Text(TxtDescription.Text) & " " &
                " Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        If Topctrl1.Mode = "Add" Then
            mCode = AgL.GetMaxId("PaymentModeAccount", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, Cmd, AgL.Gcn_ConnectionString)
            mQry = "Insert Into PaymentModeAccount(Code, PaymentMode, PostToAc)
                    Values (" & AgL.Chk_Text(mCode) & "," & AgL.Chk_Text(SearchCode) & ", " & AgL.Chk_Text(TxtPostToAc.Tag) & ")
                    "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        Else
            mCode = AgL.Dman_Execute("Select Code From PaymentModeAccount Where PaymentMode=" & AgL.Chk_Text(SearchCode) & " And Div_Code Is Null And Site_Code Is Null", AgL.GcnRead).ExecuteScalar
            mQry = "Update PaymentModeAccount Set PostToAc = " & AgL.Chk_Text(TxtPostToAc.Tag) & " where Code = '" & SearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = "Select Code, Description As Name " &
                " From PaymentMode " &
                " Order By Description "
        TxtDescription.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DtTemp As DataTable

        mQry = "Select H.* " &
                " From PaymentMode H " &
                " Where H.Code='" & SearchCode & "'"
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        With DtTemp
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtDescription.Text = AgL.XNull(.Rows(0)("Description"))
                mQry = "Select H.PostToAc, Sg.Name as PostToAcName 
                        From PaymentModeAccount H
                        Left Join viewHelpSubgroup Sg On H.PostToAc = Sg.Code
                        Where H.PaymentMode = '" & mInternalCode & "' And H.Div_code Is Null And H.Site_Code Is Null"
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    TxtPostToAc.Tag = AgL.XNull(DtTemp.Rows(0)("PostToAc"))
                    TxtPostToAc.Text = AgL.XNull(DtTemp.Rows(0)("PostToAcName"))
                End If
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
            Case TxtPostToAc.Name
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
                " From PaymentMode I " & mConStr &
                " Order By I.Description "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmPaymentMode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 350, 885)
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

    Private Function FRestrictSystemDefine() As Boolean
        If TxtEntryBy.Text.ToUpper = Users.System Then
            MsgBox("Can't Edit System Define Items...!", MsgBoxStyle.Information) : Topctrl1.FButtonClick(14, True)
            FRestrictSystemDefine = False
            Exit Function
        End If

        FRestrictSystemDefine = True
    End Function


    Private Function FGetRelationalData() As Boolean
        Try
            mQry = " Select Count(*) From SaleInvoicePayment Where PaymentMode = '" & mSearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                MsgBox(" Data Exists For Payment Mode " & TxtDescription.Text & " In Sale Invoice Payments . Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData")
            FGetRelationalData = True
        End Try
    End Function

    Private Sub ME_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        Passed = FRestrictSystemDefine()
        Passed = Not FGetRelationalData()
    End Sub

    Private Sub Txt_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtPostToAc.KeyDown
        If e.KeyCode <> Keys.Return Then
            If CType(sender, AgControls.AgTextBox).AgHelpDataSet Is Nothing Then
                mQry = "Select H.Code, H.Name From viewHelpSubgroup H Where H.Nature Not In ('Customer','Supplier') Order By H.Name"
                CType(sender, AgControls.AgTextBox).AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
            End If
        End If
    End Sub

    Private Sub FrmPaymentMode_BaseEvent_Approve_InTrans(SearchCode As String, Conn As Object, Cmd As Object) Handles Me.BaseEvent_Approve_InTrans

    End Sub

    Private Sub FrmPaymentMode_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        Passed = FRestrictSystemDefine()
    End Sub
End Class
