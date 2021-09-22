Imports System.Data.SQLite
Public Class FrmClothSupplierSettlementHead
    Inherits AgTemplate.TempMaster

    Dim mQry$

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtDescription = New AgControls.AgTextBox()
        Me.LblDescription = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtPostInAc = New AgControls.AgTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtAdditionDeduction = New AgControls.AgTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtSr = New AgControls.AgTextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.LblCalculateOnMandatorySymbol = New System.Windows.Forms.Label()
        Me.TxtCalculateOn = New AgControls.AgTextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TxtRateCalculationType = New AgControls.AgTextBox()
        Me.Label11 = New System.Windows.Forms.Label()
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
        Me.Label1.Location = New System.Drawing.Point(317, 85)
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
        Me.TxtDescription.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDescription.Location = New System.Drawing.Point(333, 77)
        Me.TxtDescription.MaxLength = 50
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(370, 18)
        Me.TxtDescription.TabIndex = 1
        '
        'LblDescription
        '
        Me.LblDescription.AutoSize = True
        Me.LblDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDescription.Location = New System.Drawing.Point(180, 78)
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
        Me.Label2.Location = New System.Drawing.Point(317, 125)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 7)
        Me.Label2.TabIndex = 674
        Me.Label2.Text = "Ä"
        '
        'TxtPostInAc
        '
        Me.TxtPostInAc.AgAllowUserToEnableMasterHelp = False
        Me.TxtPostInAc.AgLastValueTag = Nothing
        Me.TxtPostInAc.AgLastValueText = Nothing
        Me.TxtPostInAc.AgMandatory = True
        Me.TxtPostInAc.AgMasterHelp = False
        Me.TxtPostInAc.AgNumberLeftPlaces = 0
        Me.TxtPostInAc.AgNumberNegetiveAllow = False
        Me.TxtPostInAc.AgNumberRightPlaces = 0
        Me.TxtPostInAc.AgPickFromLastValue = False
        Me.TxtPostInAc.AgRowFilter = ""
        Me.TxtPostInAc.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPostInAc.AgSelectedValue = Nothing
        Me.TxtPostInAc.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPostInAc.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPostInAc.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPostInAc.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPostInAc.Location = New System.Drawing.Point(333, 117)
        Me.TxtPostInAc.MaxLength = 50
        Me.TxtPostInAc.Name = "TxtPostInAc"
        Me.TxtPostInAc.Size = New System.Drawing.Size(370, 18)
        Me.TxtPostInAc.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(180, 118)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 16)
        Me.Label3.TabIndex = 673
        Me.Label3.Text = "Post In A/c"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(317, 105)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 7)
        Me.Label4.TabIndex = 677
        Me.Label4.Text = "Ä"
        '
        'TxtAdditionDeduction
        '
        Me.TxtAdditionDeduction.AgAllowUserToEnableMasterHelp = False
        Me.TxtAdditionDeduction.AgLastValueTag = Nothing
        Me.TxtAdditionDeduction.AgLastValueText = Nothing
        Me.TxtAdditionDeduction.AgMandatory = True
        Me.TxtAdditionDeduction.AgMasterHelp = False
        Me.TxtAdditionDeduction.AgNumberLeftPlaces = 0
        Me.TxtAdditionDeduction.AgNumberNegetiveAllow = False
        Me.TxtAdditionDeduction.AgNumberRightPlaces = 0
        Me.TxtAdditionDeduction.AgPickFromLastValue = False
        Me.TxtAdditionDeduction.AgRowFilter = ""
        Me.TxtAdditionDeduction.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAdditionDeduction.AgSelectedValue = Nothing
        Me.TxtAdditionDeduction.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAdditionDeduction.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAdditionDeduction.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAdditionDeduction.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAdditionDeduction.Location = New System.Drawing.Point(333, 97)
        Me.TxtAdditionDeduction.MaxLength = 50
        Me.TxtAdditionDeduction.Name = "TxtAdditionDeduction"
        Me.TxtAdditionDeduction.Size = New System.Drawing.Size(156, 18)
        Me.TxtAdditionDeduction.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(180, 98)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(117, 16)
        Me.Label5.TabIndex = 676
        Me.Label5.Text = "Addition/Deduction"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(580, 105)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(10, 7)
        Me.Label6.TabIndex = 680
        Me.Label6.Text = "Ä"
        '
        'TxtSr
        '
        Me.TxtSr.AgAllowUserToEnableMasterHelp = False
        Me.TxtSr.AgLastValueTag = Nothing
        Me.TxtSr.AgLastValueText = Nothing
        Me.TxtSr.AgMandatory = True
        Me.TxtSr.AgMasterHelp = False
        Me.TxtSr.AgNumberLeftPlaces = 1
        Me.TxtSr.AgNumberNegetiveAllow = False
        Me.TxtSr.AgNumberRightPlaces = 0
        Me.TxtSr.AgPickFromLastValue = False
        Me.TxtSr.AgRowFilter = ""
        Me.TxtSr.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSr.AgSelectedValue = Nothing
        Me.TxtSr.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSr.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtSr.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSr.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSr.Location = New System.Drawing.Point(596, 97)
        Me.TxtSr.MaxLength = 50
        Me.TxtSr.Name = "TxtSr"
        Me.TxtSr.Size = New System.Drawing.Size(107, 18)
        Me.TxtSr.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(517, 98)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 16)
        Me.Label7.TabIndex = 679
        Me.Label7.Text = "Serial"
        '
        'LblCalculateOnMandatorySymbol
        '
        Me.LblCalculateOnMandatorySymbol.AutoSize = True
        Me.LblCalculateOnMandatorySymbol.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblCalculateOnMandatorySymbol.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblCalculateOnMandatorySymbol.Location = New System.Drawing.Point(317, 162)
        Me.LblCalculateOnMandatorySymbol.Name = "LblCalculateOnMandatorySymbol"
        Me.LblCalculateOnMandatorySymbol.Size = New System.Drawing.Size(10, 7)
        Me.LblCalculateOnMandatorySymbol.TabIndex = 683
        Me.LblCalculateOnMandatorySymbol.Text = "Ä"
        '
        'TxtCalculateOn
        '
        Me.TxtCalculateOn.AgAllowUserToEnableMasterHelp = False
        Me.TxtCalculateOn.AgLastValueTag = Nothing
        Me.TxtCalculateOn.AgLastValueText = Nothing
        Me.TxtCalculateOn.AgMandatory = True
        Me.TxtCalculateOn.AgMasterHelp = False
        Me.TxtCalculateOn.AgNumberLeftPlaces = 0
        Me.TxtCalculateOn.AgNumberNegetiveAllow = False
        Me.TxtCalculateOn.AgNumberRightPlaces = 0
        Me.TxtCalculateOn.AgPickFromLastValue = False
        Me.TxtCalculateOn.AgRowFilter = ""
        Me.TxtCalculateOn.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCalculateOn.AgSelectedValue = Nothing
        Me.TxtCalculateOn.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCalculateOn.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCalculateOn.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCalculateOn.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCalculateOn.Location = New System.Drawing.Point(333, 157)
        Me.TxtCalculateOn.MaxLength = 50
        Me.TxtCalculateOn.Name = "TxtCalculateOn"
        Me.TxtCalculateOn.Size = New System.Drawing.Size(370, 18)
        Me.TxtCalculateOn.TabIndex = 6
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(180, 158)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(83, 16)
        Me.Label9.TabIndex = 682
        Me.Label9.Text = "Calculate On"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(317, 142)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(10, 7)
        Me.Label10.TabIndex = 686
        Me.Label10.Text = "Ä"
        '
        'TxtRateCalculationType
        '
        Me.TxtRateCalculationType.AgAllowUserToEnableMasterHelp = False
        Me.TxtRateCalculationType.AgLastValueTag = Nothing
        Me.TxtRateCalculationType.AgLastValueText = Nothing
        Me.TxtRateCalculationType.AgMandatory = True
        Me.TxtRateCalculationType.AgMasterHelp = False
        Me.TxtRateCalculationType.AgNumberLeftPlaces = 0
        Me.TxtRateCalculationType.AgNumberNegetiveAllow = False
        Me.TxtRateCalculationType.AgNumberRightPlaces = 0
        Me.TxtRateCalculationType.AgPickFromLastValue = False
        Me.TxtRateCalculationType.AgRowFilter = ""
        Me.TxtRateCalculationType.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtRateCalculationType.AgSelectedValue = Nothing
        Me.TxtRateCalculationType.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtRateCalculationType.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtRateCalculationType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtRateCalculationType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRateCalculationType.Location = New System.Drawing.Point(333, 137)
        Me.TxtRateCalculationType.MaxLength = 50
        Me.TxtRateCalculationType.Name = "TxtRateCalculationType"
        Me.TxtRateCalculationType.Size = New System.Drawing.Size(370, 18)
        Me.TxtRateCalculationType.TabIndex = 5
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(180, 138)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(134, 16)
        Me.Label11.TabIndex = 685
        Me.Label11.Text = "Rate Calculation Type"
        '
        'FrmClothSupplierSettlementHead
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(862, 267)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.TxtRateCalculationType)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.LblCalculateOnMandatorySymbol)
        Me.Controls.Add(Me.TxtCalculateOn)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtSr)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtAdditionDeduction)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtPostInAc)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.LblDescription)
        Me.Name = "FrmClothSupplierSettlementHead"
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
        Me.Controls.SetChildIndex(Me.TxtPostInAc, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.TxtAdditionDeduction, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.TxtSr, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.TxtCalculateOn, 0)
        Me.Controls.SetChildIndex(Me.LblCalculateOnMandatorySymbol, 0)
        Me.Controls.SetChildIndex(Me.Label11, 0)
        Me.Controls.SetChildIndex(Me.TxtRateCalculationType, 0)
        Me.Controls.SetChildIndex(Me.Label10, 0)
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
    Public WithEvents TxtPostInAc As AgControls.AgTextBox
    Public WithEvents Label3 As Label
    Public WithEvents Label4 As Label
    Public WithEvents TxtAdditionDeduction As AgControls.AgTextBox
    Public WithEvents Label5 As Label
    Public WithEvents Label6 As Label
    Public WithEvents TxtSr As AgControls.AgTextBox
    Public WithEvents Label7 As Label
    Public WithEvents LblCalculateOnMandatorySymbol As Label
    Public WithEvents TxtCalculateOn As AgControls.AgTextBox
    Public WithEvents Label9 As Label
    Public WithEvents Label10 As Label
    Public WithEvents TxtRateCalculationType As AgControls.AgTextBox
    Public WithEvents Label11 As Label
    Public WithEvents Label1 As System.Windows.Forms.Label
#End Region

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If AgL.RequiredField(TxtDescription, LblDescription.Text) Then passed = False : Exit Sub

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From Cloth_SupplierSettlementAdjustmentHead Where Description='" & TxtDescription.Text & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        Else
            mQry = "Select count(*) From Cloth_SupplierSettlementAdjustmentHead Where Description='" & TxtDescription.Text & "' And Code <> '" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If
    End Sub

    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = " Where 1=1  "

        AgL.PubFindQry = "SELECT I.Code As SearchCode, I.Description  FROM Cloth_SupplierSettlementAdjustmentHead I "
        AgL.PubFindQryOrdBy = "[Description]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Cloth_SupplierSettlementAdjustmentHead"
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        mQry = "UPDATE Cloth_SupplierSettlementAdjustmentHead 
                Set 
                Description = " & AgL.Chk_Text(TxtDescription.Text) & ", 
                AdditionDeduction = " & AgL.Chk_Text(TxtAdditionDeduction.Text) & ", 
                Sr = " & AgL.Chk_Text(TxtSr.Text) & ", 
                PostInAc = " & AgL.Chk_Text(TxtPostInAc.Tag) & ", 
                RateCalculationType = " & AgL.Chk_Text(TxtRateCalculationType.Text) & ", 
                CalculateOn = " & AgL.Chk_Text(TxtCalculateOn.Tag) & " 
                Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = "Select Code, Description As Name " &
                " From Cloth_SupplierSettlementAdjustmentHead " &
                " Order By Description "
        TxtDescription.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select Code, Name 
                From viewHelpSubgroup 
                Where Nature Not in ('Customer','Supplier')
                Order By Name "
        TxtPostInAc.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select 'ADDITION' as Code, 'ADDITION' as Name Union All Select 'DEDUCTION' as Code, 'DEDUCTION' as Name "
        TxtAdditionDeduction.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select 'N/A' as Code, 'N/A' as Name Union All Select 'Multiply' as Code, 'Multiply' as Name  Union All Select 'Percentage' as Code, 'Percentage' as Name "
        TxtRateCalculationType.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = "Select H.*, Sg.Name As PostInAcName 
                From Cloth_SupplierSettlementAdjustmentHead H 
                Left Join viewHelpSubgroup Sg on H.PostInAc = Sg.Code                
                Where H.Code='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtDescription.Text = AgL.XNull(.Rows(0)("Description"))
                TxtAdditionDeduction.Text = AgL.XNull(.Rows(0)("AdditionDeduction"))
                TxtSr.Text = AgL.XNull(.Rows(0)("Sr"))
                TxtPostInAc.Text = AgL.XNull(.Rows(0)("PostInAcName"))
                TxtPostInAc.Tag = AgL.XNull(.Rows(0)("PostInAc"))
                TxtRateCalculationType.Text = AgL.XNull(.Rows(0)("RateCalculationType"))
                TxtCalculateOn.Tag = AgL.XNull(.Rows(0)("CalculateOn"))
                Select Case TxtCalculateOn.Tag.ToString.ToUpper
                    Case "QTY", "TAXABLE AMOUNT", "NET AMOUNT", "SUB TOTAL"
                        TxtCalculateOn.Text = TxtCalculateOn.Tag
                    Case Else
                        TxtCalculateOn.Text = AgL.XNull(AgL.Dman_Execute("Select Description From Cloth_SupplierSettlementAdjustmentHead where Code='" & TxtCalculateOn.Tag & "'", AgL.GCn).ExecuteScalar)
                End Select
            End If
        End With
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtDescription.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtDescription.Focus()
    End Sub

    Private Sub TxtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtDescription.KeyDown, TxtRateCalculationType.KeyDown, TxtCalculateOn.KeyDown
        Select Case sender.Name
            Case TxtRateCalculationType.Name
                If e.KeyCode = Keys.Enter Then
                    If TxtRateCalculationType.Text.ToUpper = "N/A" Then
                        If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                            Topctrl1.FButtonClick(13)
                        End If
                    End If
                End If

            Case TxtCalculateOn.Name
                If e.KeyCode <> Keys.Enter Then
                    If sender.AgHelpDataSet Is Nothing Then
                        mQry = "Select 'QTY' as Code, 'QTY' as Description  
                                Union All Select 'TAXABLE AMOUNT' as Code, 'TAXABLE AMOUNT' as Description  
                                Union All Select 'NET AMOUNT' as Code, 'NET AMOUNT' as Description 
                                Union All Select 'SUB TOTAL' as Code, 'SUB TOTAL' as Description 
                                UNION ALL Select Code, Description From Cloth_SupplierSettlementAdjustmentHead 
                                Where Code<> '" & mSearchCode & "'"
                        TxtCalculateOn.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If
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
                " From Cloth_SupplierSettlementAdjustmentHead I " & mConStr &
                " Order By I.Description "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmCloth_SupplierSettlementAdjustmentHead_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 300, 885)
    End Sub

    Private Sub TxtManualCode_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)

    End Sub

    Private Function FGetRelationalData() As Boolean
        Try
            mQry = " Select Count(*) From Cloth_SupplierSettlementInvoicesAdjustment Where AdjustmentHead = '" & mSearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                MsgBox(" Data Exists For Item " & TxtDescription.Text & " In Supplier Settlement . Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData")
            FGetRelationalData = True
        End Try
    End Function

    Private Sub ME_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        Passed = Not FGetRelationalData()
    End Sub



    Private Sub FrmClothSupplierSettlementHead_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd

        TxtSr.Text = AgL.Dman_Execute("SELECT IFNULL(MAX(SR),0)+1 FROM Cloth_SupplierSettlementAdjustmentHead", AgL.GCn).ExecuteScalar
        TxtRateCalculationType.Text = "N/A"
        TxtRateCalculationType.Tag = "N/A"
    End Sub

    Private Sub FrmClothSupplierSettlementHead_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        TxtCalculateOn.AgHelpDataSet = Nothing
    End Sub

    Private Sub TxtRateCalculationType_TextChanged(sender As Object, e As EventArgs) Handles TxtRateCalculationType.TextChanged
        If TxtRateCalculationType.Text.ToUpper <> "N/A" And TxtRateCalculationType.Text.ToUpper <> "" Then
            TxtCalculateOn.AgMandatory = True
            LblCalculateOnMandatorySymbol.Visible = True
        Else
            TxtCalculateOn.AgMandatory = False
            LblCalculateOnMandatorySymbol.Visible = False
        End If
    End Sub
End Class
