Imports System.ComponentModel
Imports System.Data.SQLite
Public Class FrmScheme_Old
    Inherits AgTemplate.TempMaster

    Dim mQry$

    Public Const ColSNo As String = "SNo"
    Public WithEvents DGL1 As New AgControls.AgDataGrid
    Public Const Col1ItemCategory As String = "Item Category"
    Public Const Col1ItemGroup As String = "Item Group"
    Public Const Col1Item As String = "Item"
    Public Const Col1Base As String = "Base"
    Public Const Col1ValueGreaterThen As String = "Value Greater Then"
    Public Const Col1DiscountPer As String = "Discount Per"
    Public Const Col1DiscountAmount As String = "Discount Amount"
    Public Const Col1RewardPointsPer As String = "Reward Points Per"
    Public Const Col1RewardPoints As String = "Reward Points"

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtFromDate = New AgControls.AgTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtDescription = New AgControls.AgTextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TxtApplyOn = New AgControls.AgTextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtToDate = New AgControls.AgTextBox()
        Me.Label10 = New System.Windows.Forms.Label()
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
        Me.GroupBox1.Location = New System.Drawing.Point(0, 415)
        Me.GroupBox1.Size = New System.Drawing.Size(904, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(14, 419)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(148, 483)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(204, 419)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(401, 419)
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
        Me.GroupBox2.Location = New System.Drawing.Point(704, 419)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(434, 419)
        Me.GBoxDivision.Size = New System.Drawing.Size(150, 44)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Size = New System.Drawing.Size(144, 18)
        Me.TxtDivision.Tag = ""
        '
        'TxtStatus
        '
        Me.TxtStatus.AgSelectedValue = ""
        Me.TxtStatus.Tag = ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(312, 90)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 7)
        Me.Label2.TabIndex = 674
        Me.Label2.Text = "Ä"
        '
        'TxtFromDate
        '
        Me.TxtFromDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtFromDate.AgLastValueTag = Nothing
        Me.TxtFromDate.AgLastValueText = Nothing
        Me.TxtFromDate.AgMandatory = True
        Me.TxtFromDate.AgMasterHelp = False
        Me.TxtFromDate.AgNumberLeftPlaces = 0
        Me.TxtFromDate.AgNumberNegetiveAllow = False
        Me.TxtFromDate.AgNumberRightPlaces = 0
        Me.TxtFromDate.AgPickFromLastValue = False
        Me.TxtFromDate.AgRowFilter = ""
        Me.TxtFromDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFromDate.AgSelectedValue = Nothing
        Me.TxtFromDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFromDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtFromDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFromDate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFromDate.Location = New System.Drawing.Point(328, 82)
        Me.TxtFromDate.MaxLength = 50
        Me.TxtFromDate.Name = "TxtFromDate"
        Me.TxtFromDate.Size = New System.Drawing.Size(111, 16)
        Me.TxtFromDate.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(222, 84)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 14)
        Me.Label3.TabIndex = 673
        Me.Label3.Text = "From Date"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(312, 72)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(10, 7)
        Me.Label6.TabIndex = 1069
        Me.Label6.Text = "Ä"
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
        Me.TxtDescription.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDescription.Location = New System.Drawing.Point(328, 65)
        Me.TxtDescription.MaxLength = 50
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(313, 16)
        Me.TxtDescription.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(222, 66)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(82, 14)
        Me.Label7.TabIndex = 1068
        Me.Label7.Text = "Description"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(309, 107)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(10, 7)
        Me.Label8.TabIndex = 1072
        Me.Label8.Text = "Ä"
        '
        'TxtApplyOn
        '
        Me.TxtApplyOn.AgAllowUserToEnableMasterHelp = False
        Me.TxtApplyOn.AgLastValueTag = Nothing
        Me.TxtApplyOn.AgLastValueText = Nothing
        Me.TxtApplyOn.AgMandatory = True
        Me.TxtApplyOn.AgMasterHelp = False
        Me.TxtApplyOn.AgNumberLeftPlaces = 8
        Me.TxtApplyOn.AgNumberNegetiveAllow = False
        Me.TxtApplyOn.AgNumberRightPlaces = 0
        Me.TxtApplyOn.AgPickFromLastValue = False
        Me.TxtApplyOn.AgRowFilter = ""
        Me.TxtApplyOn.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtApplyOn.AgSelectedValue = Nothing
        Me.TxtApplyOn.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtApplyOn.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtApplyOn.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtApplyOn.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtApplyOn.Location = New System.Drawing.Point(328, 100)
        Me.TxtApplyOn.MaxLength = 8
        Me.TxtApplyOn.Name = "TxtApplyOn"
        Me.TxtApplyOn.Size = New System.Drawing.Size(313, 16)
        Me.TxtApplyOn.TabIndex = 4
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(222, 101)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(67, 14)
        Me.Label9.TabIndex = 1071
        Me.Label9.Text = "Apply On"
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl1.Location = New System.Drawing.Point(15, 138)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(833, 271)
        Me.Pnl1.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(514, 90)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 1077
        Me.Label1.Text = "Ä"
        '
        'TxtToDate
        '
        Me.TxtToDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtToDate.AgLastValueTag = Nothing
        Me.TxtToDate.AgLastValueText = Nothing
        Me.TxtToDate.AgMandatory = True
        Me.TxtToDate.AgMasterHelp = False
        Me.TxtToDate.AgNumberLeftPlaces = 0
        Me.TxtToDate.AgNumberNegetiveAllow = False
        Me.TxtToDate.AgNumberRightPlaces = 0
        Me.TxtToDate.AgPickFromLastValue = False
        Me.TxtToDate.AgRowFilter = ""
        Me.TxtToDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtToDate.AgSelectedValue = Nothing
        Me.TxtToDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtToDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtToDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtToDate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtToDate.Location = New System.Drawing.Point(530, 82)
        Me.TxtToDate.MaxLength = 50
        Me.TxtToDate.Name = "TxtToDate"
        Me.TxtToDate.Size = New System.Drawing.Size(111, 16)
        Me.TxtToDate.TabIndex = 3
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(450, 85)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(58, 14)
        Me.Label10.TabIndex = 1076
        Me.Label10.Text = "To Date"
        '
        'FrmScheme
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(862, 463)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtToDate)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TxtApplyOn)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtFromDate)
        Me.Controls.Add(Me.Label3)
        Me.Name = "FrmScheme"
        Me.Text = "Scheme Master"
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.TxtFromDate, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.TxtDescription, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.TxtApplyOn, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Label10, 0)
        Me.Controls.SetChildIndex(Me.TxtToDate, 0)
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
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents TxtFromDate As AgControls.AgTextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label6 As Label
    Public WithEvents TxtDescription As AgControls.AgTextBox
    Public WithEvents Label7 As Label
    Public WithEvents Label8 As Label
    Public WithEvents TxtApplyOn As AgControls.AgTextBox
    Public WithEvents Label9 As Label
    Public WithEvents Pnl1 As Panel
    Public WithEvents Label1 As Label
    Public WithEvents TxtToDate As AgControls.AgTextBox
    Public WithEvents Label10 As Label
#End Region

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If TxtDescription.Text.Trim = "" Then Err.Raise(1, , "Description Is Required!")
        If TxtApplyOn.Text.Trim = "" Then Err.Raise(1, , "Apply On Is Required!")
        If TxtFromDate.Text.Trim = "" Then Err.Raise(1, , "From Date Is Required!")
        If TxtToDate.Text.Trim = "" Then Err.Raise(1, , "To Date Is Required!")

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From Scheme Where Description='" & TxtDescription.Text & "' And " & AgTemplate.ClsMain.RetDivFilterStr & "  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        Else
            mQry = "Select count(*) From Scheme Where Description='" & TxtDescription.Text & "' And Code<>'" & mInternalCode & "' And " & AgTemplate.ClsMain.RetDivFilterStr & "  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If
    End Sub

    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        AgL.PubFindQry = "SELECT S.Code, S.Description, S.FromDate, S.ToDate " &
                        " FROM Scheme S "
        AgL.PubFindQryOrdBy = "[Description]"
    End Sub
    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Scheme"
    End Sub
    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer
        Dim mSr As Integer = 0

        mQry = "UPDATE Scheme 
                Set 
                Description = " & AgL.Chk_Text(TxtDescription.Text) & ", 
                FromDate = " & AgL.Chk_Date(TxtFromDate.Text) & ", 
                ToDate = " & AgL.Chk_Date(TxtToDate.Text) & ", 
                ApplyOn = " & AgL.Chk_Text(TxtApplyOn.Text) & " 
                Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "Delete from SchemeDetail where Code = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        For I = 0 To DGL1.Rows.Count - 1
            If DGL1.Item(Col1Base, I).Value <> "" And DGL1.Item(Col1Base, I).Value IsNot Nothing Then
                mSr += 1
                mQry = " Insert Into SchemeDetail (Code, Sr, ItemCategory, ItemGroup, Item, Base, 
                        ValueGreaterThen, DiscountPer, DiscountAmount, RewardPointsPer, RewardPoints) " &
                       " Values ('" & SearchCode & "', " & mSr & ", " & AgL.Chk_Text(DGL1.Item(Col1ItemCategory, I).Tag) & ", 
                       " & AgL.Chk_Text(DGL1.Item(Col1ItemGroup, I).Tag) & ", 
                       " & AgL.Chk_Text(DGL1.Item(Col1Item, I).Tag) & ", 
                       " & AgL.Chk_Text(DGL1.Item(Col1Base, I).Value) & ", 
                       " & Val(DGL1.Item(Col1ValueGreaterThen, I).Value) & ", 
                       " & Val(DGL1.Item(Col1DiscountPer, I).Value) & ", 
                       " & Val(DGL1.Item(Col1DiscountAmount, I).Value) & ", 
                       " & Val(DGL1.Item(Col1RewardPointsPer, I).Value) & ", 
                       " & Val(DGL1.Item(Col1RewardPoints, I).Value) & " 
                       ) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
        Next
    End Sub
    Private Sub TxtBuyer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtDescription.KeyDown, TxtApplyOn.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then Exit Sub
            Select Case sender.name
                Case TxtDescription.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataset Is Nothing Then
                            mQry = "Select Code, Description As Name " &
                                    " From Scheme " &
                                    " Order By Description "
                            TxtDescription.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtApplyOn.Name
                    If CType(sender, AgControls.AgTextBox).AgHelpDataSet Is Nothing Then
                        If e.KeyCode <> Keys.Enter Then
                            mQry = "Select 'Single Invoice' As Code, 'Single Invoice' As Name " &
                                    " Union All " &
                                    " Select 'Multiple Invoice' As Code, 'Multiple Invoice' As Name "
                            TxtApplyOn.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = "Select H.*
                 From Scheme H 
                 Where H.Code='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(DsTemp.Tables(0).Rows(0)("Code"))
                TxtDescription.Text = AgL.XNull(DsTemp.Tables(0).Rows(0)("Description"))
                TxtFromDate.Text = ClsMain.FormatDate(AgL.XNull(DsTemp.Tables(0).Rows(0)("FromDate")))
                TxtToDate.Text = ClsMain.FormatDate(AgL.XNull(DsTemp.Tables(0).Rows(0)("ToDate")))
                TxtApplyOn.Text = AgL.XNull(DsTemp.Tables(0).Rows(0)("ApplyOn"))
            End If
        End With


        Dim I As Integer
        mQry = " Select L.*, Ic.Description As ItemCategoryDesc, Ig.Description As ItemGroupDesc,
                I.Description As ItemDesc
                From SchemeDetail L
                LEFT JOIN ItemCategory IC ON L.ItemCategory = Ic.Code
                LEFT JOIN ItemGroup Ig On L.ItemGroup = Ig.Code
                LEFT JOIN Item I ON L.Item = I.Code
                Where L.Code = '" & SearchCode & "' 
                Order By L.Sr "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            DGL1.RowCount = 1
            DGL1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    DGL1.Rows.Add()
                    DGL1.Item(ColSNo, I).Value = DGL1.Rows.Count - 1
                    DGL1.Item(Col1ItemCategory, I).Tag = AgL.XNull(.Rows(I)("ItemCategory"))
                    DGL1.Item(Col1ItemCategory, I).Value = AgL.XNull(.Rows(I)("ItemCategoryDesc"))
                    DGL1.Item(Col1ItemGroup, I).Tag = AgL.XNull(.Rows(I)("ItemGroup"))
                    DGL1.Item(Col1ItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupDesc"))
                    DGL1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                    DGL1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                    DGL1.Item(Col1Base, I).Value = AgL.XNull(.Rows(I)("Base"))
                    DGL1.Item(Col1ValueGreaterThen, I).Value = AgL.VNull(.Rows(I)("ValueGreaterThen"))
                    DGL1.Item(Col1DiscountPer, I).Value = AgL.VNull(.Rows(I)("DiscountPer"))
                    DGL1.Item(Col1DiscountAmount, I).Value = AgL.VNull(.Rows(I)("DiscountAmount"))
                    DGL1.Item(Col1RewardPointsPer, I).Value = AgL.VNull(.Rows(I)("RewardPointsPer"))
                    DGL1.Item(Col1RewardPoints, I).Value = AgL.VNull(.Rows(I)("RewardPoints"))
                Next I
            End If
        End With
    End Sub
    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtDescription.Focus()
    End Sub
    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtDescription.Focus()
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
            " From Scheme I " &
            " Order By I.Description "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub
    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub
    Private Sub FrmScheme_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 502, 878)
    End Sub
    Private Sub FrmScheme_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        DGL1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DGL1, ColSNo, 40, 5, ColSNo, False, True, False)
            .AddAgTextColumn(DGL1, Col1ItemCategory, 120, 0, Col1ItemCategory, True, False, False)
            .AddAgTextColumn(DGL1, Col1ItemGroup, 120, 0, Col1ItemGroup, True, False, False)
            .AddAgTextColumn(DGL1, Col1Item, 150, 0, Col1Item, True, False, False)
            .AddAgTextColumn(DGL1, Col1Base, 100, 0, Col1Base, True, False, False)
            .AddAgNumberColumn(DGL1, Col1ValueGreaterThen, 80, 8, 2, False, Col1ValueGreaterThen, True, False, True)
            .AddAgNumberColumn(DGL1, Col1DiscountPer, 80, 8, 2, False, Col1DiscountPer, True, False, True)
            .AddAgNumberColumn(DGL1, Col1DiscountAmount, 80, 8, 2, False, Col1DiscountAmount, True, False, True)
            .AddAgNumberColumn(DGL1, Col1RewardPointsPer, 80, 8, 2, False, Col1RewardPointsPer, False, False, True)
            .AddAgNumberColumn(DGL1, Col1RewardPoints, 80, 8, 2, False, Col1RewardPoints, False, False, True)
        End With
        AgL.AddAgDataGrid(DGL1, Pnl1)
        DGL1.EnableHeadersVisualStyles = False
        DGL1.AgSkipReadOnlyColumns = True
        DGL1.RowHeadersVisible = False
        DGL1.ColumnHeadersHeight = 48
        AgL.GridDesign(DGL1)
    End Sub
    Private Sub DGL1_EditingControl_KeyDown(sender As Object, e As KeyEventArgs) Handles DGL1.EditingControl_KeyDown
        Dim mQry As String
        Dim mRowIndex As Integer
        Dim mCondStr As String = ""
        mRowIndex = DGL1.CurrentCell.RowIndex
        Select Case DGL1.Columns(DGL1.CurrentCell.ColumnIndex).Name
            Case Col1ItemCategory
                If e.KeyCode <> Keys.Enter Then
                    If DGL1.AgHelpDataSet(Col1ItemCategory) Is Nothing Then
                        mQry = "Select Code as Code, Description  From ItemCategory Order By Description"
                        DGL1.AgHelpDataSet(Col1ItemCategory) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If

            Case Col1ItemGroup
                If e.KeyCode <> Keys.Enter Then
                    If DGL1.AgHelpDataSet(Col1ItemGroup) Is Nothing Then
                        If DGL1.Item(Col1ItemCategory, mRowIndex).Tag IsNot Nothing And DGL1.Item(Col1ItemCategory, mRowIndex).Tag <> "" Then
                            mCondStr = " And ItemCategory = " + AgL.Chk_Text(DGL1.Item(Col1ItemCategory, mRowIndex).Tag)
                        End If
                        mQry = "Select Code as Code, Description  From ItemGroup Where 1=1 " & mCondStr &
                                " Order By Description"
                        DGL1.AgHelpDataSet(Col1ItemGroup) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If

            Case Col1Item
                If e.KeyCode <> Keys.Enter Then
                    If DGL1.AgHelpDataSet(Col1Item) Is Nothing Then
                        If DGL1.Item(Col1ItemCategory, mRowIndex).Tag IsNot Nothing And DGL1.Item(Col1ItemCategory, mRowIndex).Tag <> "" Then
                            mCondStr = " And ItemCategory = " + AgL.Chk_Text(DGL1.Item(Col1ItemCategory, mRowIndex).Tag)
                        End If
                        If DGL1.Item(Col1ItemGroup, mRowIndex).Tag IsNot Nothing And DGL1.Item(Col1ItemGroup, mRowIndex).Tag <> "" Then
                            mCondStr = " And ItemGroup = " + AgL.Chk_Text(DGL1.Item(Col1ItemGroup, mRowIndex).Tag)
                        End If
                        mQry = "Select Code as Code, Description  From Item Where 1=1 " & mCondStr &
                                " Order By Description"
                        DGL1.AgHelpDataSet(Col1Item) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If

            Case Col1Base
                If e.KeyCode <> Keys.Enter Then
                    If DGL1.AgHelpDataSet(Col1Base) Is Nothing Then
                        mQry = "Select 'Quantity' as Code, 'Quantity' as Description 
                                UNION ALL 
                                Select 'Amount' as Code, 'Amount' as Description "
                        DGL1.AgHelpDataSet(Col1Base) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If
        End Select
    End Sub

    Private Sub FrmScheme_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        DGL1.RowCount = 1 : DGL1.Rows.Clear()
    End Sub
End Class
