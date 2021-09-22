Imports AgLibrary.ClsMain.agConstants
Public Class FrmRateType
    Inherits AgTemplate.TempMaster

    Dim mQry$

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.LblDescriptionReq = New System.Windows.Forms.Label()
        Me.TxtDescription = New AgControls.AgTextBox()
        Me.LblDescription = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtMargin = New AgControls.AgTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtDiscount = New AgControls.AgTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtCalculateOn = New AgControls.AgTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtProcess = New AgControls.AgTextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.BtnUpdateRateTypes = New System.Windows.Forms.Button()
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(224, 293)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(232, 223)
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
        Me.GBoxDivision.Location = New System.Drawing.Point(465, 223)
        Me.GBoxDivision.Size = New System.Drawing.Size(140, 44)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Size = New System.Drawing.Size(134, 18)
        Me.TxtDivision.Tag = ""
        '
        'TxtStatus
        '
        Me.TxtStatus.AgSelectedValue = ""
        Me.TxtStatus.Tag = ""
        '
        'LblDescriptionReq
        '
        Me.LblDescriptionReq.AutoSize = True
        Me.LblDescriptionReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblDescriptionReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblDescriptionReq.Location = New System.Drawing.Point(295, 125)
        Me.LblDescriptionReq.Name = "LblDescriptionReq"
        Me.LblDescriptionReq.Size = New System.Drawing.Size(10, 7)
        Me.LblDescriptionReq.TabIndex = 666
        Me.LblDescriptionReq.Text = "Ä"
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
        Me.TxtDescription.Location = New System.Drawing.Point(311, 117)
        Me.TxtDescription.MaxLength = 50
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(385, 16)
        Me.TxtDescription.TabIndex = 1
        '
        'LblDescription
        '
        Me.LblDescription.AutoSize = True
        Me.LblDescription.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDescription.Location = New System.Drawing.Point(200, 118)
        Me.LblDescription.Name = "LblDescription"
        Me.LblDescription.Size = New System.Drawing.Size(82, 14)
        Me.LblDescription.TabIndex = 661
        Me.LblDescription.Text = "Description"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(295, 143)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 677
        Me.Label1.Text = "Ä"
        '
        'TxtMargin
        '
        Me.TxtMargin.AgAllowUserToEnableMasterHelp = False
        Me.TxtMargin.AgLastValueTag = Nothing
        Me.TxtMargin.AgLastValueText = Nothing
        Me.TxtMargin.AgMandatory = True
        Me.TxtMargin.AgMasterHelp = True
        Me.TxtMargin.AgNumberLeftPlaces = 0
        Me.TxtMargin.AgNumberNegetiveAllow = False
        Me.TxtMargin.AgNumberRightPlaces = 0
        Me.TxtMargin.AgPickFromLastValue = False
        Me.TxtMargin.AgRowFilter = ""
        Me.TxtMargin.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtMargin.AgSelectedValue = Nothing
        Me.TxtMargin.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtMargin.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtMargin.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtMargin.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMargin.Location = New System.Drawing.Point(311, 135)
        Me.TxtMargin.MaxLength = 50
        Me.TxtMargin.Name = "TxtMargin"
        Me.TxtMargin.Size = New System.Drawing.Size(128, 16)
        Me.TxtMargin.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(200, 136)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 14)
        Me.Label2.TabIndex = 676
        Me.Label2.Text = "Margin %"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(552, 143)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 7)
        Me.Label3.TabIndex = 680
        Me.Label3.Text = "Ä"
        '
        'TxtDiscount
        '
        Me.TxtDiscount.AgAllowUserToEnableMasterHelp = False
        Me.TxtDiscount.AgLastValueTag = Nothing
        Me.TxtDiscount.AgLastValueText = Nothing
        Me.TxtDiscount.AgMandatory = True
        Me.TxtDiscount.AgMasterHelp = True
        Me.TxtDiscount.AgNumberLeftPlaces = 0
        Me.TxtDiscount.AgNumberNegetiveAllow = False
        Me.TxtDiscount.AgNumberRightPlaces = 0
        Me.TxtDiscount.AgPickFromLastValue = False
        Me.TxtDiscount.AgRowFilter = ""
        Me.TxtDiscount.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDiscount.AgSelectedValue = Nothing
        Me.TxtDiscount.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDiscount.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDiscount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDiscount.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDiscount.Location = New System.Drawing.Point(568, 135)
        Me.TxtDiscount.MaxLength = 50
        Me.TxtDiscount.Name = "TxtDiscount"
        Me.TxtDiscount.Size = New System.Drawing.Size(128, 16)
        Me.TxtDiscount.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(457, 136)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 14)
        Me.Label4.TabIndex = 679
        Me.Label4.Text = "Discount %"
        '
        'TxtCalculateOn
        '
        Me.TxtCalculateOn.AgAllowUserToEnableMasterHelp = False
        Me.TxtCalculateOn.AgLastValueTag = Nothing
        Me.TxtCalculateOn.AgLastValueText = Nothing
        Me.TxtCalculateOn.AgMandatory = False
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
        Me.TxtCalculateOn.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCalculateOn.Location = New System.Drawing.Point(311, 154)
        Me.TxtCalculateOn.MaxLength = 50
        Me.TxtCalculateOn.Name = "TxtCalculateOn"
        Me.TxtCalculateOn.Size = New System.Drawing.Size(385, 16)
        Me.TxtCalculateOn.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(200, 155)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(91, 14)
        Me.Label5.TabIndex = 682
        Me.Label5.Text = "Calculate On"
        '
        'TxtProcess
        '
        Me.TxtProcess.AgAllowUserToEnableMasterHelp = False
        Me.TxtProcess.AgLastValueTag = Nothing
        Me.TxtProcess.AgLastValueText = Nothing
        Me.TxtProcess.AgMandatory = False
        Me.TxtProcess.AgMasterHelp = False
        Me.TxtProcess.AgNumberLeftPlaces = 0
        Me.TxtProcess.AgNumberNegetiveAllow = False
        Me.TxtProcess.AgNumberRightPlaces = 0
        Me.TxtProcess.AgPickFromLastValue = False
        Me.TxtProcess.AgRowFilter = ""
        Me.TxtProcess.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtProcess.AgSelectedValue = Nothing
        Me.TxtProcess.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtProcess.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtProcess.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtProcess.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtProcess.Location = New System.Drawing.Point(311, 173)
        Me.TxtProcess.MaxLength = 50
        Me.TxtProcess.Name = "TxtProcess"
        Me.TxtProcess.Size = New System.Drawing.Size(385, 16)
        Me.TxtProcess.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(200, 174)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 14)
        Me.Label6.TabIndex = 684
        Me.Label6.Text = "Process"
        '
        'BtnUpdateRateTypes
        '
        Me.BtnUpdateRateTypes.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUpdateRateTypes.Location = New System.Drawing.Point(774, 105)
        Me.BtnUpdateRateTypes.Name = "BtnUpdateRateTypes"
        Me.BtnUpdateRateTypes.Size = New System.Drawing.Size(75, 83)
        Me.BtnUpdateRateTypes.TabIndex = 685
        Me.BtnUpdateRateTypes.Text = "Update All Rates For This Rate  Type"
        Me.BtnUpdateRateTypes.UseVisualStyleBackColor = True
        '
        'FrmRateType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(862, 267)
        Me.Controls.Add(Me.BtnUpdateRateTypes)
        Me.Controls.Add(Me.TxtProcess)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtCalculateOn)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtDiscount)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtMargin)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LblDescriptionReq)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.LblDescription)
        Me.Name = "FrmRateType"
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
        Me.Controls.SetChildIndex(Me.LblDescriptionReq, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.TxtMargin, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.TxtDiscount, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.TxtCalculateOn, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.TxtProcess, 0)
        Me.Controls.SetChildIndex(Me.BtnUpdateRateTypes, 0)
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
    Public WithEvents Label1 As Label
    Public WithEvents TxtMargin As AgControls.AgTextBox
    Public WithEvents Label2 As Label
    Public WithEvents Label3 As Label
    Public WithEvents TxtDiscount As AgControls.AgTextBox
    Public WithEvents Label4 As Label
    Public WithEvents TxtCalculateOn As AgControls.AgTextBox
    Public WithEvents Label5 As Label
    Public WithEvents TxtProcess As AgControls.AgTextBox
    Public WithEvents Label6 As Label
    Friend WithEvents BtnUpdateRateTypes As Button
    Public WithEvents LblDescriptionReq As System.Windows.Forms.Label
#End Region

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If AgL.RequiredField(TxtDescription, LblDescription.Text) Then passed = False : Exit Sub

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From RateType Where Description ='" & TxtDescription.Text & "'   "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        Else
            mQry = "Select count(*) From RateType Where Description ='" & TxtDescription.Text & "' And Code <> '" & mSearchCode & "'   "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If
    End Sub

    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = "  "

        AgL.PubFindQry = "SELECT H.Code, H.Description, H.Margin  " &
                        " FROM RateType H  " & mConStr

        AgL.PubFindQryOrdBy = "[Name]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "RateType"
        PrimaryField = "Code"
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        mQry = "UPDATE RateType " &
                " SET " &
                " Description = " & AgL.Chk_Text(TxtDescription.Text) & ", " &
                " Margin = " & Val(TxtMargin.Text) & ", " &
                " Discount = " & Val(TxtDiscount.Text) & ", " &
                " CalculateOnRateType = " & AgL.Chk_Text(TxtCalculateOn.Tag) & " " &
                " Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)




        mQry = "SELECT Count(*)
                FROM Item I With (NoLock)
                Left Join ItemGroupRateType Igrt With (NoLock) on I.Code = Igrt.Code And Igrt.RateType = '" & SearchCode & "'
                WHERE I.V_Type ='" & ItemV_Type.ItemGroup & "' And Igrt.Code Is Null
                "
        If AgL.Dman_Execute(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar() > 0 Then
            mQry = "INSERT INTO ItemGroupRateType 
                    (Code, RateType, MarginCalculationPattern, Margin, 
                    DiscountCalculationPattern, DiscountPer, AdditionalDiscountCalculationPattern, AdditionalDiscountPer, 
                    AdditionCalculationPattern, AdditionPer)
                    SELECT I.Code, '" & mSearchCode & "' RateType, 'Percentage' MarginCalculationPattern, " & Val(TxtMargin.Text) & " Margin, 
                    'Percentage' DiscountCalculationPattern, " & Val(TxtDiscount.Text) & " DiscountPer, 'Percentage After Discount' AdditionalDiscountCalculationPattern, 0 AdditionalDiscountPer, 
                    'Percentage After Discount' AdditionCalculationPattern, 0 AdditionPer
                    FROM Item I
                    Left Join ItemGroupRateType Igrt on I.Code = Igrt.Code And Igrt.RateType = '" & SearchCode & "'
                    WHERE I.V_Type ='" & ItemV_Type.ItemGroup & "' And Igrt.Code Is Null "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If



        mQry = "SELECT I.Code, IfNull(SR.Rate,0) as Rate
                FROM Item I With (NoLock)
                Left Join RateListDetail SR On I.Code = SR.Item And SR.RateType Is Null
                Left Join RateListDetail RLD With (NoLock) on I.Code = RLD.Item And RLD.RateType = '" & SearchCode & "'                
                WHERE I.V_Type ='" & ItemV_Type.Item & "' And RLD.Code Is Null
                "
        Dim dtItems As DataTable
        Dim I As Integer
        Dim bRateListCode As String = ""
        dtItems = AgL.FillData(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).Tables(0)
        If dtItems.Rows.Count > 0 Then
            For I = 0 To dtItems.Rows.Count - 1
                mQry = "Select Max(Code) From RateList With (NoLock) Where GenDocId = '" & dtItems.Rows(I)("Code") & "' And GenV_Type='" & ItemV_Type.Item & "'"
                bRateListCode = AgL.XNull(AgL.Dman_Execute(mQry, IIf(AgL.PubServerName = "", Conn, AgL.GcnRead)).ExecuteScalar())
                If bRateListCode = "" Then
                    bRateListCode = AgL.GetMaxId("RateList", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

                    mQry = " INSERT INTO RateList(Code, WEF, EntryBy, EntryDate, EntryType, " &
                    " EntryStatus, Status, Div_Code, GenDocId, GenV_Type) " &
                    " VALUES (" & AgL.Chk_Text(bRateListCode) & ", " & AgL.Chk_Date(AgL.PubLoginDate) & ",	" &
                    " " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Date(AgL.PubLoginDate) & ", " &
                    " " & AgL.Chk_Text(Topctrl1.Mode) & ", 'Open', " & AgL.Chk_Text(AgTemplate.ClsMain.EntryStatus.Active) & ", " &
                    " '" & TxtDivision.AgSelectedValue & "', " & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(ItemV_Type.Item) & ")"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If

                Try
                    mQry = "INSERT INTO RateListDetail(Code, Sr, Item, RateType, Rate, DiscountPer, AdditionPer) " &
                  " VALUES (" & AgL.Chk_Text(bRateListCode) & ", " &
                  " " & Math.Round(Val(TxtMargin.Text), 0) & ",  " &
                  " " & AgL.Chk_Text(dtItems.Rows(I)("Code")) & ", " &
                  " " & AgL.Chk_Text(SearchCode) & ", " & Math.Round(Val(dtItems.Rows(I)("Rate") + (Val(dtItems.Rows(I)("Rate")) * Val(TxtMargin.Text) / 100)), 0) & ", " & Val(TxtDiscount.Text) & ", 0) "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                Catch ex As Exception
                End Try

            Next
        End If



        Dim mSr As Integer

        Dim bValueArr As String() = TxtProcess.Tag.ToString.Split(",")

        mQry = " Delete From RateTypeProcess Where Code = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        For I = 0 To bValueArr.Length - 1
            If bValueArr(I) <> "" Then
                mSr += 1
                mQry = "INSERT INTO RateTypeProcess(Code, Sr, Process) 
                        VALUES(" & AgL.Chk_Text(mSearchCode) & ", 
                        " & mSr & ", 
                        " & AgL.Chk_Text(bValueArr(I)) & ") "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next
    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet
        Dim DtTemp As DataTable

        mQry = "Select H.*, Rt.Description as CalculateOnRateTypeName, P.Name as ProcessName " &
            " From RateType H " &
            " Left Join RateType Rt On H.CalculateOnRateType = Rt.Code " &
            " Left Join Subgroup P On H.Process = P.Subcode  " &
            " Where H.Code ='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtDescription.Text = AgL.XNull(.Rows(0)("Description"))
                TxtCalculateOn.Tag = AgL.XNull(.Rows(0)("CalculateOnRateType"))
                TxtCalculateOn.Text = AgL.XNull(.Rows(0)("CalculateOnRateTypeName"))
                TxtMargin.Text = AgL.VNull(.Rows(0)("Margin"))
                TxtDiscount.Text = AgL.VNull(.Rows(0)("Discount"))
                TxtProcess.Tag = AgL.XNull(.Rows(0)("Process"))
                TxtProcess.Text = AgL.XNull(.Rows(0)("ProcessName"))
            End If
        End With

        mQry = "Select L.Process, Sg.Name As ProcessName
                From RateTypeProcess L 
                LEFT JOIN SubGroup Sg ON L.Process = Sg.SubCode
                Where L.Code = '" & mSearchCode & "' "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        For I As Integer = 0 To DtTemp.Rows.Count - 1
            If TxtProcess.Tag <> "" Then TxtProcess.Tag += ","
            If TxtProcess.Text <> "" Then TxtProcess.Text += ","
            TxtProcess.Tag += AgL.XNull(DtTemp.Rows(I)("Process"))
            TxtProcess.Text += AgL.XNull(DtTemp.Rows(I)("ProcessName"))
        Next
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

    Private Sub FrmYarn_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1    "
        mQry = "Select I.Code As SearchCode " &
                " From RateType I " & mConStr &
                " And (Case When I.IsDeleted Is Null Then 0 Else I.IsDeleted End)=0 Order By I.Description "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmRateType_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 300, 885)
    End Sub

    Private Sub TxtRateTypeCategory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                Topctrl1.FButtonClick(13)
            End If
        End If
    End Sub

    Private Sub TxtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtDescription.KeyDown, TxtCalculateOn.KeyDown
        Try
            Select Case sender.Name
                Case TxtDescription.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataSet Is Nothing Then
                            mQry = "Select Code, Description  " &
                                  " From RateType " &
                                  " Where 1=1 " &
                                  " Order By Description "
                            sender.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtCalculateOn.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataSet Is Nothing Then
                            mQry = "Select Code, Description  " &
                                  " From RateType " &
                                  " Where Code <> '" & mSearchCode & "' " &
                                  " Order By Description "
                            sender.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtProcess.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataSet Is Nothing Then
                            mQry = " SELECT Sg.SubCode AS Code, Sg.Name, Parent.Name as ParentName 
                            FROM Subgroup Sg With (NoLock)
                            Left Join Subgroup Parent On Parent.Subcode = Sg.Parent
                            Where Sg.SubgroupType = '" & AgLibrary.ClsMain.agConstants.SubgroupType.Process & "' 
                            And IfNull(Sg.Status,'Active') = 'Active' 
                            And Sg.Subcode <> '" & AgLibrary.ClsMain.agConstants.Process.Purchase & "'"
                            sender.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmRateType_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        TxtCalculateOn.AgHelpDataSet = Nothing
    End Sub
    Private Sub FHPGD_Process(ByRef bTag As String, ByRef bValue As String)
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtTemp As DataTable

        mQry = " SELECT 'o' As Tick, SubCode AS Code, Name As Name FROM SubGroup Where SubGroupType = '" & SubgroupType.Process & "' "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 400, 420, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            bTag = FRH_Multiple.FFetchData(1, "", "", ",", True)
            bValue = FRH_Multiple.FFetchData(2, "", "", ",", True)
        End If
        FRH_Multiple = Nothing
    End Sub
    Private Sub TxtProcess_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtProcess.KeyDown
        If ClsMain.IsSpecialKeyPressed(e) Then Exit Sub
        Select Case sender.Name
            Case TxtProcess.Name
                FHPGD_Process(TxtProcess.Tag, TxtProcess.Text)
        End Select
    End Sub
    Private Sub FrmRateType_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        TxtProcess.ReadOnly = True
        If AgL.StrCmp(Topctrl1.Mode, "Edit") Then
            BtnUpdateRateTypes.Enabled = True
        Else
            BtnUpdateRateTypes.Enabled = False
        End If
    End Sub

    Public Structure StructRateType
        Dim Code As String
        Dim Description As String
        Dim EntryBy As String
        Dim EntryDate As String
        Dim EntryType As String
        Dim EntryStatus As String
        Dim Status As String
        Dim Div_Code As String
        Dim LockText As String
        Dim OMSId As String
    End Structure


    Public Shared Sub ImportRateTypeTable(RateTypeTableList As StructRateType())
        Dim mQry As String = ""
        If AgL.VNull(AgL.Dman_Execute("Select Count(*) From RateType With (NoLock) where Description = " & AgL.Chk_Text(RateTypeTableList(0).Description) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar) = 0 Then
            mQry = " INSERT INTO RateType (Code, Description, EntryBy, EntryDate, 
                    EntryType, EntryStatus, Status, Div_Code, OMSId) 
                    Select '" & RateTypeTableList(0).Code & "' As Code, 
                    " & AgL.Chk_Text(RateTypeTableList(0).Description) & " As Description,                     
                    " & AgL.Chk_Text(RateTypeTableList(0).EntryBy) & " As EntryBy, 
                    " & AgL.Chk_Date(RateTypeTableList(0).EntryDate) & " As EntryDate, 
                    " & AgL.Chk_Text(RateTypeTableList(0).EntryType) & " As EntryType, 
                    " & AgL.Chk_Text(RateTypeTableList(0).EntryStatus) & " As EntryStatus, 
                    " & AgL.Chk_Text(RateTypeTableList(0).Status) & " As Status, 
                    " & AgL.Chk_Text(RateTypeTableList(0).Div_Code) & " As Div_Code,                     
                    " & AgL.Chk_Text(RateTypeTableList(0).OMSId) & " As OMSId "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Else
            Dim bIntrestSlabCode As String = AgL.XNull(AgL.Dman_Execute("Select Code 
                        From RateType With (NoLock) 
                        Where Description = " & AgL.Chk_Text(RateTypeTableList(0).Description) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar)


            mQry = " Delete From RateTypeDetail Where Code = '" & bIntrestSlabCode & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            RateTypeTableList(0).Code = bIntrestSlabCode
        End If
    End Sub

    Private Sub BtnUpdateRateTypes_Click(sender As Object, e As EventArgs) Handles BtnUpdateRateTypes.Click
        If AgL.StrCmp(Topctrl1.Mode, "Edit") And (AgL.StrCmp(AgL.PubUserName, "Sa") Or AgL.StrCmp(AgL.PubUserName, "Super")) Then
            If MsgBox(" This will change you current rates. Are you sure to continue ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                mQry = " Delete From RateListDetail Where IfNull(RateType,'') = '" & mSearchCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = " Delete From ItemGroupRateType Where IfNull(RateType,'') = '" & mSearchCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
        End If
        ProcSave()
    End Sub
End Class
