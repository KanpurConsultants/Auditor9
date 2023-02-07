Imports System.Data.SQLite
Imports System.IO
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class FrmCity
    Inherits AgTemplate.TempMaster

    Dim mQry$
    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Division As String = "Division"
    Public Const Col1Site As String = "Site"
    Public WithEvents Pnl1 As Panel
    Friend WithEvents TxtZone As AgControls.AgTextBox
    Friend WithEvents LblZone As Label
    Public Const Col1Distance As String = "Distance"

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.TxtCountry = New AgControls.AgTextBox()
        Me.LblCountry = New System.Windows.Forms.Label()
        Me.TxtState = New AgControls.AgTextBox()
        Me.LblState = New System.Windows.Forms.Label()
        Me.LblCityNameReq = New System.Windows.Forms.Label()
        Me.TxtCityName = New AgControls.AgTextBox()
        Me.LblCityName = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.MnuOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuImport = New System.Windows.Forms.ToolStripMenuItem()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.TxtZone = New AgControls.AgTextBox()
        Me.LblZone = New System.Windows.Forms.Label()
        Me.GrpUP.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MnuOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(862, 41)
        Me.Topctrl1.TabIndex = 5
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 414)
        Me.GroupBox1.Size = New System.Drawing.Size(904, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(14, 418)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(143, 479)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(226, 418)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(399, 418)
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
        Me.GroupBox2.Location = New System.Drawing.Point(703, 418)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(485, 418)
        Me.GBoxDivision.Size = New System.Drawing.Size(135, 44)
        '
        'TxtDivision
        '
        Me.TxtDivision.Size = New System.Drawing.Size(129, 18)
        '
        'TxtCountry
        '
        Me.TxtCountry.AgAllowUserToEnableMasterHelp = False
        Me.TxtCountry.AgLastValueTag = Nothing
        Me.TxtCountry.AgLastValueText = Nothing
        Me.TxtCountry.AgMandatory = False
        Me.TxtCountry.AgMasterHelp = True
        Me.TxtCountry.AgNumberLeftPlaces = 0
        Me.TxtCountry.AgNumberNegetiveAllow = False
        Me.TxtCountry.AgNumberRightPlaces = 0
        Me.TxtCountry.AgPickFromLastValue = False
        Me.TxtCountry.AgRowFilter = ""
        Me.TxtCountry.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCountry.AgSelectedValue = Nothing
        Me.TxtCountry.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCountry.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCountry.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCountry.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCountry.Location = New System.Drawing.Point(330, 123)
        Me.TxtCountry.MaxLength = 50
        Me.TxtCountry.Multiline = True
        Me.TxtCountry.Name = "TxtCountry"
        Me.TxtCountry.Size = New System.Drawing.Size(345, 20)
        Me.TxtCountry.TabIndex = 2
        Me.TxtCountry.Visible = False
        '
        'LblCountry
        '
        Me.LblCountry.AutoSize = True
        Me.LblCountry.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCountry.Location = New System.Drawing.Point(213, 127)
        Me.LblCountry.Name = "LblCountry"
        Me.LblCountry.Size = New System.Drawing.Size(53, 16)
        Me.LblCountry.TabIndex = 682
        Me.LblCountry.Text = "Country"
        Me.LblCountry.Visible = False
        '
        'TxtState
        '
        Me.TxtState.AgAllowUserToEnableMasterHelp = False
        Me.TxtState.AgLastValueTag = Nothing
        Me.TxtState.AgLastValueText = Nothing
        Me.TxtState.AgMandatory = True
        Me.TxtState.AgMasterHelp = False
        Me.TxtState.AgNumberLeftPlaces = 0
        Me.TxtState.AgNumberNegetiveAllow = False
        Me.TxtState.AgNumberRightPlaces = 0
        Me.TxtState.AgPickFromLastValue = False
        Me.TxtState.AgRowFilter = ""
        Me.TxtState.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtState.AgSelectedValue = Nothing
        Me.TxtState.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtState.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtState.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtState.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtState.Location = New System.Drawing.Point(330, 101)
        Me.TxtState.MaxLength = 50
        Me.TxtState.Multiline = True
        Me.TxtState.Name = "TxtState"
        Me.TxtState.Size = New System.Drawing.Size(345, 20)
        Me.TxtState.TabIndex = 1
        '
        'LblState
        '
        Me.LblState.AutoSize = True
        Me.LblState.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblState.Location = New System.Drawing.Point(214, 104)
        Me.LblState.Name = "LblState"
        Me.LblState.Size = New System.Drawing.Size(39, 16)
        Me.LblState.TabIndex = 681
        Me.LblState.Text = "State"
        '
        'LblCityNameReq
        '
        Me.LblCityNameReq.AutoSize = True
        Me.LblCityNameReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblCityNameReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblCityNameReq.Location = New System.Drawing.Point(314, 87)
        Me.LblCityNameReq.Name = "LblCityNameReq"
        Me.LblCityNameReq.Size = New System.Drawing.Size(10, 7)
        Me.LblCityNameReq.TabIndex = 666
        Me.LblCityNameReq.Text = "Ä"
        '
        'TxtCityName
        '
        Me.TxtCityName.AgAllowUserToEnableMasterHelp = False
        Me.TxtCityName.AgLastValueTag = Nothing
        Me.TxtCityName.AgLastValueText = Nothing
        Me.TxtCityName.AgMandatory = True
        Me.TxtCityName.AgMasterHelp = True
        Me.TxtCityName.AgNumberLeftPlaces = 0
        Me.TxtCityName.AgNumberNegetiveAllow = False
        Me.TxtCityName.AgNumberRightPlaces = 0
        Me.TxtCityName.AgPickFromLastValue = False
        Me.TxtCityName.AgRowFilter = ""
        Me.TxtCityName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCityName.AgSelectedValue = Nothing
        Me.TxtCityName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCityName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCityName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCityName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCityName.Location = New System.Drawing.Point(330, 79)
        Me.TxtCityName.MaxLength = 50
        Me.TxtCityName.Multiline = True
        Me.TxtCityName.Name = "TxtCityName"
        Me.TxtCityName.Size = New System.Drawing.Size(345, 20)
        Me.TxtCityName.TabIndex = 0
        '
        'LblCityName
        '
        Me.LblCityName.AutoSize = True
        Me.LblCityName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCityName.Location = New System.Drawing.Point(214, 82)
        Me.LblCityName.Name = "LblCityName"
        Me.LblCityName.Size = New System.Drawing.Size(69, 16)
        Me.LblCityName.TabIndex = 661
        Me.LblCityName.Text = "City Name"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(313, 108)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 683
        Me.Label1.Text = "Ä"
        '
        'MnuOptions
        '
        Me.MnuOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuImport})
        Me.MnuOptions.Name = "MnuOptions"
        Me.MnuOptions.Size = New System.Drawing.Size(111, 26)
        '
        'MnuImport
        '
        Me.MnuImport.Name = "MnuImport"
        Me.MnuImport.Size = New System.Drawing.Size(110, 22)
        Me.MnuImport.Text = "Import"
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl1.Location = New System.Drawing.Point(286, 188)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(338, 191)
        Me.Pnl1.TabIndex = 4
        '
        'TxtZone
        '
        Me.TxtZone.AgAllowUserToEnableMasterHelp = False
        Me.TxtZone.AgLastValueTag = Nothing
        Me.TxtZone.AgLastValueText = Nothing
        Me.TxtZone.AgMandatory = False
        Me.TxtZone.AgMasterHelp = False
        Me.TxtZone.AgNumberLeftPlaces = 0
        Me.TxtZone.AgNumberNegetiveAllow = False
        Me.TxtZone.AgNumberRightPlaces = 0
        Me.TxtZone.AgPickFromLastValue = False
        Me.TxtZone.AgRowFilter = ""
        Me.TxtZone.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtZone.AgSelectedValue = Nothing
        Me.TxtZone.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtZone.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtZone.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtZone.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtZone.Location = New System.Drawing.Point(330, 145)
        Me.TxtZone.MaxLength = 50
        Me.TxtZone.Multiline = True
        Me.TxtZone.Name = "TxtZone"
        Me.TxtZone.Size = New System.Drawing.Size(345, 20)
        Me.TxtZone.TabIndex = 3
        Me.TxtZone.Visible = False
        '
        'LblZone
        '
        Me.LblZone.AutoSize = True
        Me.LblZone.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblZone.Location = New System.Drawing.Point(213, 149)
        Me.LblZone.Name = "LblZone"
        Me.LblZone.Size = New System.Drawing.Size(36, 16)
        Me.LblZone.TabIndex = 685
        Me.LblZone.Text = "Zone"
        Me.LblZone.Visible = False
        '
        'FrmCity
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(862, 462)
        Me.ContextMenuStrip = Me.MnuOptions
        Me.Controls.Add(Me.TxtZone)
        Me.Controls.Add(Me.LblZone)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtCountry)
        Me.Controls.Add(Me.LblCountry)
        Me.Controls.Add(Me.TxtState)
        Me.Controls.Add(Me.LblState)
        Me.Controls.Add(Me.LblCityNameReq)
        Me.Controls.Add(Me.TxtCityName)
        Me.Controls.Add(Me.LblCityName)
        Me.Name = "FrmCity"
        Me.Text = "Quality Master"
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.LblCityName, 0)
        Me.Controls.SetChildIndex(Me.TxtCityName, 0)
        Me.Controls.SetChildIndex(Me.LblCityNameReq, 0)
        Me.Controls.SetChildIndex(Me.LblState, 0)
        Me.Controls.SetChildIndex(Me.TxtState, 0)
        Me.Controls.SetChildIndex(Me.LblCountry, 0)
        Me.Controls.SetChildIndex(Me.TxtCountry, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.LblZone, 0)
        Me.Controls.SetChildIndex(Me.TxtZone, 0)
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
        Me.MnuOptions.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LblCityName As System.Windows.Forms.Label
    Friend WithEvents TxtCityName As AgControls.AgTextBox
    Friend WithEvents LblCityNameReq As System.Windows.Forms.Label
    Friend WithEvents LblState As System.Windows.Forms.Label
    Friend WithEvents TxtState As AgControls.AgTextBox
    Friend WithEvents LblCountry As System.Windows.Forms.Label
    Friend WithEvents Label1 As Label
    Friend WithEvents MnuOptions As ContextMenuStrip
    Private components As System.ComponentModel.IContainer
    Friend WithEvents MnuImport As ToolStripMenuItem
    Friend WithEvents TxtCountry As AgControls.AgTextBox


#End Region

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If AgL.RequiredField(TxtCityName, "City Name") Then passed = False : Exit Sub

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From City Where CityName ='" & TxtCityName.Text & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "City Name Already Exist!")
        Else
            mQry = "Select count(*) From City Where CityName ='" & TxtCityName.Text & "' And CityCode<>'" & mInternalCode & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "City Name Already Exist!")
        End If
    End Sub

    Private Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        AgL.PubFindQry = "SELECT CityCode, CityName , State " &
                            " FROM City " &
                            " WHERE IfNull(IsDeleted,0)=0 "
        AgL.PubFindQryOrdBy = "[CityName]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "City"
        PrimaryField = "CityCode"
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer
        Dim LineCode As String

        mQry = " Update City " &
                "   SET  " &
                "	CityName = " & AgL.Chk_Text(TxtCityName.Text) & ", " &
                "	State = " & AgL.Chk_Text(TxtState.Tag) & ", " &
                "	Country = " & AgL.Chk_Text(TxtCountry.Text) & ", " &
                "	Zone = " & AgL.Chk_Text(TxtZone.Tag) & " " &
                "   Where CityCode = '" & SearchCode & "' "

        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


        mQry = "Delete From CitySiteDivisionDetail Where CityCode = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


        For I = 0 To Dgl1.Rows.Count - 1
            If Val(Dgl1.Item(Col1Distance, I).Value) > 0 Then
                LineCode = SearchCode & Dgl1.Item(Col1Site, I).Tag
                mQry = "Insert Into CitySiteDivisionDetail(Code, CityCode, Div_Code, Site_Code, Distance)
                       Values (" & AgL.Chk_Text(LineCode) & ", " & AgL.Chk_Text(SearchCode) & ", Null, " & AgL.Chk_Text(Dgl1.Item(Col1Site, I).Tag) & ", " & Val(Dgl1.Item(Col1Distance, I).Value) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                If Topctrl1.Mode <> "Add" Then
                    mQry = "Update SubGroupSiteDivisionDetail Set Distance=" & Val(Dgl1.Item(Col1Distance, I).Value) & " Where Site_Code = " & AgL.Chk_Text(Dgl1.Item(Col1Site, I).Tag) & " And Subcode In (Select Subcode From Subgroup Where CityCode=" & AgL.Chk_Text(SearchCode) & ") "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            End If
        Next
    End Sub

    Private Sub FrmQuality1_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        If FDivisionNameForCustomization(22) = "W SHYAMA SHYAM FABRICS" Or ClsMain.FDivisionNameForCustomization(27) = "W SHYAMA SHYAM VENTURES LLP" Then
            LblZone.Visible = True
            TxtZone.Visible = True
        Else
            LblZone.Visible = False
            TxtZone.Visible = False
        End If
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList

        mQry = "Select CityCode as Code, CityName From City " &
            "  Order By CityName "
        TxtCityName.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select  Code, Description as State, Null Country
                From state                 
                Order By Description "
        TxtState.AgHelpDataSet(1) = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select Distinct Country As Code, Country 
                From City  
                WHERE Country Is Not Null
                Order By Country "
        TxtCountry.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select  Code, Description as Zone
                From Zone
                Order By Description "
        TxtZone.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FrmYarn_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        mQry = "Select CityCode As SearchCode " &
                " From City " &
                " WHERE (Case When City.IsDeleted Is Null Then 0 Else City.IsDeleted End)=0 " &
                " Order By CityName "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet
        Dim I As Integer

        mQry = "Select C.*, S.Description as StateName, Z.Description As ZoneName 
             From City C
             Left Join State S on C.State = S.Code
             Left Join Zone Z on C.Zone = Z.Code
            Where C.CityCode='" & SearchCode & "'"

        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("CityCode"))
                TxtCityName.Text = AgL.XNull(.Rows(0)("CityName"))
                TxtState.Tag = AgL.XNull(.Rows(0)("State"))
                TxtState.Text = AgL.XNull(.Rows(0)("StateName"))
                TxtCountry.Text = AgL.XNull(.Rows(0)("Country"))
                TxtZone.Tag = AgL.XNull(.Rows(0)("Zone"))
                TxtZone.Text = AgL.XNull(.Rows(0)("ZoneName"))
            End If
        End With

        mQry = "Select Null as Div_Code, Null as Div_Name, Site.Code as Site_Code, Site.Name as Site_Name, L.Distance
                From SiteMast Site  With (NoLock)                
                Left Join CitySiteDivisionDetail L With (NoLock) On Site.Code = L.Site_Code And L.CityCode = '" & SearchCode & "'  
                "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                    'Dgl1.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))
                    Dgl1.Item(Col1Site, I).Tag = AgL.XNull(.Rows(I)("Site_Code"))
                    Dgl1.Item(Col1Site, I).Value = AgL.XNull(.Rows(I)("Site_Name"))
                    Dgl1.Item(Col1Division, I).Tag = AgL.XNull(.Rows(I)("Div_Code"))
                    Dgl1.Item(Col1Division, I).Value = AgL.XNull(.Rows(I)("Div_Name"))
                    Dgl1.Item(Col1Distance, I).Value = AgL.XNull(.Rows(I)("Distance"))
                Next I
            End If
        End With

    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtCityName.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtCityName.Focus()
    End Sub

    Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn
    End Sub

    Private Sub Control_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCityName.Enter, TxtState.Enter, TxtCountry.Enter
        Try
            Select Case sender.name
                Case TxtCityName.Name

            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtState.Validating
        Dim DtTemp As DataTable = Nothing
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.NAME
                Case TxtState.Name
                    'If TxtCountry.Text = "" Then
                    '    If sender.text.ToString.Trim = "" Or sender.AgSelectedValue.Trim = "" Then
                    '        TxtCountry.Text = ""
                    '    Else
                    '        If sender.AgHelpDataSet IsNot Nothing Then
                    '            DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.text) & "")
                    '            TxtCountry.Text = AgL.XNull(DrTemp(0)("Country"))
                    '        End If
                    '    End If
                    'End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmItemGroup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 380, 868)
    End Sub

    Private Sub TxtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtState.KeyDown
        'If e.KeyCode = Keys.Enter Then
        '    If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
        '        Topctrl1.FButtonClick(13)
        '    End If
        'End If
    End Sub

    Public Sub FImport()
        Dim mTrans As String = ""
        Dim mCode As String = ""
        Dim ErrorLog As String = ""
        Dim DtTemp As DataTable
        Dim DtMain As DataTable = Nothing
        Dim I As Integer
        'Dim FW As System.IO.StreamWriter = New System.IO.StreamWriter("C:\ImportLog.Txt", False, System.Text.Encoding.Default)
        Dim StrErrLog As String = ""
        mQry = "Select '' as Srl, 'City' as [Field Name], 'Text' as [Data Type], 10 as [Length] "
        mQry = mQry + "Union All Select  '' as Srl,'State' as [Field Name], 'Text' as [Data Type], 50 as [Length] "

        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim ObjFrmImport As New FrmImportFromExcel
        ObjFrmImport.Text = "City Master Import"
        ObjFrmImport.Dgl1.DataSource = DtTemp
        ObjFrmImport.ShowDialog()

        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

        DtTemp = ObjFrmImport.P_DsExcelData.Tables(0)

        Dim mStateCode = AgL.GetMaxId("State", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

        Dim DtState = DtTemp.DefaultView.ToTable(True, "State")
        For I = 0 To DtState.Rows.Count - 1
            If AgL.XNull(DtState.Rows(I)("State")) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From State where Description = '" & AgL.XNull(DtState.Rows(I)("State")) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These States Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These States Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtState.Rows(I)("State")) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtState.Rows(I)("State")) & ", "
                    End If
                End If
            End If
        Next

        For I = 0 To DtTemp.Rows.Count - 1
            If AgL.XNull(DtTemp.Rows(I)("State")) = "" Then
                ErrorLog += "State is blank at row no." + (I + 2).ToString() & vbCrLf
            End If
        Next

        If ErrorLog <> "" Then
            If File.Exists(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt") Then
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt", ErrorLog, False)
            Else
                File.Create(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt")
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt", ErrorLog, False)
            End If
            System.Diagnostics.Process.Start("notepad.exe", My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt")
            Exit Sub
        End If


        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            Dim mCityCode = AgL.GetMaxId("City", "CityCode", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

            Dim DtCity = DtTemp.DefaultView.ToTable(True, "City")
            For I = 0 To DtCity.Rows.Count - 1
                If AgL.XNull(DtCity.Rows(I)("City")) <> "" Then
                    If AgL.Dman_Execute("SELECT Count(*) From City where CityName = '" & AgL.XNull(DtCity.Rows(I)("City")) & "'", AgL.GCn).ExecuteScalar = 0 Then
                        Dim mCityCode_New = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(mCityCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")
                        mQry = " INSERT INTO City(CityCode, CityName, State, EntryBy, EntryDate, EntryType, EntryStatus)
                                    Select '" & mCityCode_New & "' As CityCode, " & AgL.Chk_Text(AgL.XNull(DtCity.Rows(I)("City"))) & " As City, 
                                    (SELECT Code From State where Description = '" & AgL.XNull(DtTemp.Rows(I)("State")) & "') As State, 
                                    '" & AgL.PubUserName & "' As EntryBy, " & AgL.Chk_Date(AgL.PubLoginDate) & " As EntryDate, 
                                    'Add' As EntryType, 'Open' As EntryStatus "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                End If
            Next

            AgL.ETrans.Commit()
            mTrans = "Commit"

        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
        If StrErrLog <> "" Then MsgBox(StrErrLog)
    End Sub
    Private Sub MnuImport_Click(sender As Object, e As EventArgs) Handles MnuImport.Click
        FImport()
    End Sub

    Private Sub FrmCity_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        Dim I As Integer
        Dim DsTemp As DataSet

        mQry = "Select Null as Div_Code, Null as Div_Name, S.Code as Site_Code, S.Name as Site_Name, Null as Distance
                From SiteMast S "
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                    Dgl1.Item(Col1Site, I).Tag = AgL.XNull(.Rows(I)("Site_Code"))
                    Dgl1.Item(Col1Site, I).Value = AgL.XNull(.Rows(I)("Site_Name"))
                    Dgl1.Item(Col1Division, I).Tag = AgL.XNull(.Rows(I)("Div_Code"))
                    Dgl1.Item(Col1Division, I).Value = AgL.XNull(.Rows(I)("Div_Name"))
                    Dgl1.Item(Col1Distance, I).Value = AgL.XNull(.Rows(I)("Distance"))
                Next I
            End If
        End With
    End Sub

    Private Sub FrmCity_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Division, 160, 255, Col1Division, False, True)
            .AddAgTextColumn(Dgl1, Col1Site, 200, 255, Col1Site, True, True)
            .AddAgTextColumn(Dgl1, Col1Distance, 100, 255, Col1Distance, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.AllowUserToAddRows = False
        AgL.GridDesign(Dgl1)
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
    Public Shared Sub ImportCityTable(CityTable As StructCity)
        Dim mQry As String = ""
        If AgL.Dman_Execute("SELECT Count(*) From City With (NoLock) Where CityName = '" & CityTable.CityName & "'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = " INSERT INTO City(CityCode, CityName, State, EntryBy, EntryDate, EntryType, EntryStatus, OMSId)
                    Select '" & CityTable.CityCode & "' As CityCode, " & AgL.Chk_Text(CityTable.CityName) & " As City, 
                    " & AgL.Chk_Text(CityTable.State) & " As State, 
                    '" & CityTable.EntryBy & "' As EntryBy, " & AgL.Chk_Date(CityTable.EntryDate) & " As EntryDate, 
                    '" & CityTable.EntryType & "' As EntryType, '" & CityTable.EntryStatus & "' As EntryStatus, 
                    '" & CityTable.OMSId & "' As OMSId "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Else
            mQry = " UPDATE City Set OMSId = '" & CityTable.OMSId & "' 
                    Where CityName = '" & CityTable.CityName & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If
    End Sub
    Public Structure StructCity
        Dim CityCode As String
        Dim CityName As String
        Dim State As String
        Dim EntryBy As String
        Dim EntryDate As String
        Dim EntryType As String
        Dim EntryStatus As String
        Dim OMSId As String
    End Structure
End Class
