Public Class FrmItemGroup
    Inherits AgTemplate.TempMaster

    Dim mQry$

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtDescription = New AgControls.AgTextBox
        Me.LblDescription = New System.Windows.Forms.Label
        Me.TxtItemCategory = New AgControls.AgTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtItemType = New AgControls.AgTextBox
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
        Me.Topctrl1.TabIndex = 9
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(148, 223)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(554, 223)
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
        Me.GBoxDivision.Location = New System.Drawing.Point(278, 223)
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(294, 86)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 666
        Me.Label1.Text = "Ä"
        '
        'TxtDescription
        '
        Me.TxtDescription.AgAllowUserToEnableMasterHelp = False
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
        Me.TxtDescription.Location = New System.Drawing.Point(310, 78)
        Me.TxtDescription.MaxLength = 50
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(385, 18)
        Me.TxtDescription.TabIndex = 0
        '
        'LblDescription
        '
        Me.LblDescription.AutoSize = True
        Me.LblDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDescription.Location = New System.Drawing.Point(199, 79)
        Me.LblDescription.Name = "LblDescription"
        Me.LblDescription.Size = New System.Drawing.Size(72, 16)
        Me.LblDescription.TabIndex = 661
        Me.LblDescription.Text = "Item Group"
        '
        'TxtItemCategory
        '
        Me.TxtItemCategory.AgAllowUserToEnableMasterHelp = False
        Me.TxtItemCategory.AgMandatory = False
        Me.TxtItemCategory.AgMasterHelp = False
        Me.TxtItemCategory.AgNumberLeftPlaces = 0
        Me.TxtItemCategory.AgNumberNegetiveAllow = False
        Me.TxtItemCategory.AgNumberRightPlaces = 0
        Me.TxtItemCategory.AgPickFromLastValue = False
        Me.TxtItemCategory.AgRowFilter = ""
        Me.TxtItemCategory.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtItemCategory.AgSelectedValue = Nothing
        Me.TxtItemCategory.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtItemCategory.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtItemCategory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtItemCategory.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemCategory.Location = New System.Drawing.Point(310, 99)
        Me.TxtItemCategory.MaxLength = 20
        Me.TxtItemCategory.Name = "TxtItemCategory"
        Me.TxtItemCategory.Size = New System.Drawing.Size(385, 18)
        Me.TxtItemCategory.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(199, 101)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 16)
        Me.Label2.TabIndex = 699
        Me.Label2.Text = "Item Category"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(294, 108)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 7)
        Me.Label3.TabIndex = 700
        Me.Label3.Text = "Ä"
        '
        'TxtItemType
        '
        Me.TxtItemType.AgAllowUserToEnableMasterHelp = False
        Me.TxtItemType.AgMandatory = False
        Me.TxtItemType.AgMasterHelp = True
        Me.TxtItemType.AgNumberLeftPlaces = 0
        Me.TxtItemType.AgNumberNegetiveAllow = False
        Me.TxtItemType.AgNumberRightPlaces = 0
        Me.TxtItemType.AgPickFromLastValue = False
        Me.TxtItemType.AgRowFilter = ""
        Me.TxtItemType.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtItemType.AgSelectedValue = Nothing
        Me.TxtItemType.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtItemType.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtItemType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtItemType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemType.Location = New System.Drawing.Point(57, 168)
        Me.TxtItemType.MaxLength = 20
        Me.TxtItemType.Name = "TxtItemType"
        Me.TxtItemType.Size = New System.Drawing.Size(129, 18)
        Me.TxtItemType.TabIndex = 712
        Me.TxtItemType.Visible = False
        '
        'FrmItemGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(862, 267)
        Me.Controls.Add(Me.TxtItemType)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtItemCategory)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.LblDescription)
        Me.Name = "FrmItemGroup"
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
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.TxtItemCategory, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.TxtItemType, 0)
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
    Public WithEvents TxtItemCategory As AgControls.AgTextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents TxtItemType As AgControls.AgTextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
#End Region


    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If TxtDescription.Text.Trim = "" Then Err.Raise(1, , "Description Is Required!")
        If TxtItemCategory.Text.Trim = "" Then Err.Raise(1, , "Item Category Is Required!")

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From ItemGroup Where Description='" & TxtDescription.Text & "' And " & AgTemplate.ClsMain.RetDivFilterStr & "  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")

        Else
            mQry = "Select count(*) From ItemGroup Where Description='" & TxtDescription.Text & "' And Code<>'" & mInternalCode & "' And " & AgTemplate.ClsMain.RetDivFilterStr & "  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")

        End If
    End Sub

    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        AgL.PubFindQry = "SELECT I.Code, I.Description as Item_Group, C.Description as Item_Category  " & _
                        " FROM ItemGroup I  " & _
                        " Left Join ItemCategory C On I.ItemCategory = C.Code "
        AgL.PubFindQryOrdBy = "[Description]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "ItemGroup"
        LogTableName = "ItemGroup_Log"
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        mQry = "UPDATE ItemGroup " & _
                " SET " & _
                " Description = " & AgL.Chk_Text(TxtDescription.Text) & ", " & _
                " ItemCategory = " & AgL.Chk_Text(TxtItemCategory.Tag) & ", " & _
                " ItemType = " & AgL.Chk_Text(TxtItemType.Text) & " " & _
                " Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = "Select Code, Description As Name " & _
                " From ItemGroup " & _
                " Order By Description"
        TxtDescription.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
    End Sub


    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = "Select H.*, C.Description as ItemCategoryDesc  " & _
            " From ItemGroup H " & _
            " Left Join ItemCategory C On H.ItemCategory = C.Code " & _
            " Where H.Code='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtDescription.Text = AgL.XNull(.Rows(0)("Description"))
                TxtItemCategory.Tag = AgL.XNull(.Rows(0)("ItemCategory"))
                TxtItemCategory.Text = AgL.XNull(.Rows(0)("ItemCategoryDesc"))
                TxtItemType.Text = AgL.XNull(.Rows(0)("ItemType"))
            End If
        End With
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtDescription.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtDescription.Focus()
    End Sub

    Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn
    End Sub

    Private Sub Control_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtDescription.Enter
        Try
            Select Case sender.name
            End Select
        Catch ex As Exception
        End Try
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
        mQry = "Select I.Code As SearchCode " & _
                " From ItemGroup I " & mConStr & _
                " Order By I.Description "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub


    Private Sub TxtItemCategory_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtItemCategory.Enter
        mQry = "Select Code, Description, ItemType " & _
               "From ItemCategory " & _
               "Order By Description "
        TxtItemCategory.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
    End Sub


    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmItemGroup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 300, 885)
    End Sub

    Private Sub TxtItemCategory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtItemCategory.KeyDown
        If e.KeyCode = Keys.Enter Then

        End If
    End Sub

    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtItemCategory.Validating
        Dim DtTemp As DataTable = Nothing
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.NAME
                Case TxtItemCategory.Name
                    If sender.text.ToString.Trim <> "" Then
                        If sender.AgHelpDataSet IsNot Nothing Then
                            DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                            TxtItemType.Text = AgL.XNull(DrTemp(0)("ItemType"))
                        End If
                    Else
                        TxtItemType.Text = ""
                    End If

                    If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, "Save") = MsgBoxResult.Yes Then
                        Topctrl1.FButtonClick(13)
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
