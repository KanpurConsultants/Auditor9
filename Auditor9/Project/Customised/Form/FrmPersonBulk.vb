Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants

Public Class FrmPersonBulk
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

    Protected Const ColSNo As String = "S.No."
    Protected Const Col1SearchCode As String = "Search Code"
    Protected Const Col1PartyType As String = "Party Type"
    Protected Const Col1Code As String = "Code"
    Protected Const Col1DispName As String = "Disp Name"
    Protected Const Col1Address As String = "Address"
    Protected Const Col1City As String = "City"
    Protected Const Col1PinNo As String = "Pin No"
    Protected Const Col1Contact As String = "Contact"
    Protected Const Col1Mobile As String = "Mobile"
    Protected Const Col1EMail As String = "EMail"
    Protected Const Col1AccountGroup As String = "Account Group"
    Protected Const Col1SalesTaxGroup As String = "Sales Tax Group"
    Protected Const Col1CreditDays As String = "Credit Days"
    Protected Const Col1CreditLimit As String = "Credit Limit"

    Protected Const Col1ContactPerson As String = "Contact Person"
    Protected Const Col1SalesTaxNo As String = "GST No"
    Protected Const Col1PanNo As String = "Pan No"
    Protected Const Col1AadharNo As String = "Aadhar No"
    Protected Const Col1MasterParty As String = "Master Party"
    Protected Const Col1Area As String = "Area"
    Protected Const Col1Agent As String = "Agent"
    Protected Const Col1Transporter As String = "Transporter"
    Protected Const Col1DistanceFromHere As String = "Distance From Here"


    Dim mQry As String = ""

    Dim DTFind As New DataTable
    Dim fld As String
    Public HlpSt As String

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Public Sub Ini_Grid()
        Dim I As Integer = 0
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 50, 5, ColSNo, True, True, False, , DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1SearchCode, 140, 0, Col1SearchCode, False, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1PartyType, 100, 0, Col1PartyType, True, False,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1Code, 100, 0, Col1Code, True, False,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1DispName, 200, 0, Col1DispName, True, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1Address, 250, 0, Col1Address, True, False,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1City, 120, 0, Col1City, True, False,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1PinNo, 70, 0, Col1PinNo, True, False,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1Contact, 90, 0, Col1Contact, True, False,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1Mobile, 90, 0, Col1Mobile, True, False,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1EMail, 150, 0, Col1EMail, True, False,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1AccountGroup, 150, 0, Col1AccountGroup, True, False,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1SalesTaxGroup, 70, 0, Col1SalesTaxGroup, True, False,,, DataGridViewColumnSortMode.Automatic)
            .AddAgNumberColumn(Dgl1, Col1CreditDays, 60, 10, 0, False, Col1CreditDays,,,,, DataGridViewColumnSortMode.Automatic)
            .AddAgNumberColumn(Dgl1, Col1CreditLimit, 60, 10, 0, False, Col1CreditLimit,,,,, DataGridViewColumnSortMode.Automatic)

            .AddAgTextColumn(Dgl1, Col1ContactPerson, 70, 0, Col1ContactPerson, True, False,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1SalesTaxNo, 70, 0, Col1SalesTaxNo, True, False,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1PanNo, 70, 0, Col1PanNo, True, False,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1AadharNo, 70, 0, Col1AadharNo, True, False,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1MasterParty, 70, 0, Col1MasterParty, True, False,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1Area, 70, 0, Col1Area, True, False,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1Agent, 70, 0, Col1Agent, True, False,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1Transporter, 70, 0, Col1Transporter, True, False,,, DataGridViewColumnSortMode.Automatic)
            .AddAgNumberColumn(Dgl1, Col1DistanceFromHere, 60, 10, 0, False, Col1DistanceFromHere,,,,, DataGridViewColumnSortMode.Automatic)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple

        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AgAllowFind = False
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.AgAllowFind = True

        Dgl1.AllowUserToAddRows = False
        Dgl1.EnableHeadersVisualStyles = True
        AgL.GridDesign(Dgl1)


        Dgl1.BackgroundColor = Color.White
        Dgl1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow
        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)

        For I = 0 To Dgl1.Columns.Count - 1
            Dgl1.Columns(I).ContextMenuStrip = MnuOptions
        Next
    End Sub
    Private Sub FrmBarcode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''AgL.WinSetting(Me, 654, 990, 0, 0)
        Ini_Grid()
        MovRec()
        Me.WindowState = FormWindowState.Maximized
    End Sub
    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim MyCommand As OleDb.OleDbDataAdapter = Nothing
        Select Case sender.name

        End Select
    End Sub
    Private Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
    End Sub
    Private Sub ProcSave(TableName As String, PrimaryKey As String, Code As String, FieldName As String, Value As Object)
        Dim mCode As Integer = 0
        Dim mPrimaryCode As Integer = 0
        Dim mTrans As String = ""
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            mQry = "UPDATE " + TableName + " Set " + FieldName + " = " + "'" + Value + "'" + " Where " & PrimaryKey & " = " + "'" + Code + "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProcSaveRegistration(mCode As String, RegistrationType As String, Value As Object)
        Dim mTrans As String = ""
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            If AgL.Dman_Execute("Select Count(*) From SubgroupRegistration Where SubCode = '" & mCode & "' And RegistrationType = '" & RegistrationType & "'", AgL.GCn).ExecuteScalar = 0 Then
                Dim mRegSr As Integer = AgL.Dman_Execute("Select IfNull(Max(Sr),0) + 1 From SubgroupRegistration Where SubCode = '" & mCode & "'", AgL.GCn).ExecuteScalar
                mQry = "Insert Into SubgroupRegistration(Subcode, Sr, RegistrationType, RegistrationNo)
                        Values ('" & mCode & "', " & mRegSr & ", '" & RegistrationType & "', 
                        " & AgL.Chk_Text(Value) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                mQry = " UPDATE SubgroupRegistration Set RegistrationNo = " & AgL.Chk_Text(Value) & "
                        Where SubCode = '" & mCode & "' 
                        And RegistrationType = '" & RegistrationType & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProcSaveLastValues(mCode As String, FieldName As String, Value As Object)
        Dim mTrans As String = ""
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            If AgL.Dman_Execute("Select Count(*) From SubgroupSiteDivisionDetail Where SubCode = '" & mCode & "'", AgL.GCn).ExecuteScalar = 0 Then
                mQry = "INSERT INTO SubgroupSiteDivisionDetail (SubCode, Div_Code, Site_Code, " & FieldName & ") 
                        Select '" & mCode & "', '" & AgL.PubDivCode & "', '" & AgL.PubSiteCode & "', '" & Value & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                mQry = " UPDATE SubgroupSiteDivisionDetail 
                        Set " & FieldName & " = " & AgL.Chk_Text(Value) & "
                        Where SubCode = '" & mCode & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim I As Integer = 0, Cnt = 0
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1PartyType
                    ProcSave("SubGroup", "SubCode", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "SubGroupType",
                            Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Tag)
                Case Col1Code
                    If Dgl1.Item(Col1Code, mRowIndex).Value = "" Or Dgl1.Item(Col1Code, mRowIndex).Value = Nothing Then
                        MsgBox("Code is a required fields.It Can not have blank value.", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                    mQry = "Select count(*) From SubGroup Where ManualCode ='" & Dgl1.Item(Col1Code, mRowIndex).Value & "' And SubCode<>'" & Dgl1.Item(Col1SearchCode, mRowIndex).Value & "'  AND Site_Code =" & AgL.Chk_Text(AgL.PubSiteCode) & ""
                    If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Code Already Exists")
                    ProcSave("SubGroup", "SubCode", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "ManualCode",
                             Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Value)
                    ProcSave("SubGroup", "SubCode", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "Name",
                            Dgl1.Item(Col1DispName, mRowIndex).Value + " {" + Dgl1.Item(Col1Code, mRowIndex).Value + "}")
                Case Col1DispName
                    If Dgl1.Item(Col1DispName, mRowIndex).Value = "" Or Dgl1.Item(Col1DispName, mRowIndex).Value = Nothing Then
                        MsgBox("Name is a required fields.It Can not have blank value.", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                    ProcSave("SubGroup", "SubCode", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "DispName",
                             Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Value)
                    ProcSave("SubGroup", "SubCode", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "Name",
                            Dgl1.Item(Col1DispName, mRowIndex).Value + " {" + Dgl1.Item(Col1Code, mRowIndex).Value + "}")
                Case Col1Address
                    ProcSave("SubGroup", "SubCode", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "Address",
                            Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Value)
                Case Col1City
                    ProcSave("SubGroup", "SubCode", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "CityCode",
                            Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Tag)
                Case Col1PinNo
                    ProcSave("SubGroup", "SubCode", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "Pin",
                            Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Value)
                Case Col1Contact
                    ProcSave("SubGroup", "SubCode", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "Phone",
                            Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Value)
                Case Col1Mobile
                    ProcSave("SubGroup", "SubCode", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "Mobile",
                            Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Value)
                Case Col1EMail
                    ProcSave("SubGroup", "SubCode", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "EMail",
                            Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Value)
                Case Col1AccountGroup
                    ProcSave("SubGroup", "SubCode", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "GroupCode",
                            Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Tag)
                Case Col1SalesTaxGroup
                    ProcSave("SubGroup", "SubCode", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "SalesTaxPostingGroup",
                            Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Tag)
                Case Col1CreditDays
                    ProcSave("SubGroup", "SubCode", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "CreditDays",
                            Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Value)
                Case Col1CreditLimit
                    ProcSave("SubGroup", "SubCode", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "CreditLimit",
                            Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Value)


                Case Col1ContactPerson
                    ProcSave("SubGroup", "SubCode", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "ContactPerson",
                            Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Value)
                Case Col1SalesTaxNo
                    If ValidateGstNo(Dgl1.Item(Col1SalesTaxGroup, mRowIndex).Tag, Dgl1.Item(Col1SalesTaxNo, mRowIndex).Value,
                                     Dgl1.Item(Col1City, mRowIndex).Tag) = False Then Exit Sub
                    ProcSaveRegistration(Dgl1.Item(Col1SearchCode, mRowIndex).Value, SubgroupRegistrationType.SalesTaxNo,
                            Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Value)
                Case Col1PanNo
                    ProcSaveRegistration(Dgl1.Item(Col1SearchCode, mRowIndex).Value, SubgroupRegistrationType.PanNo,
                            Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Value)
                Case Col1AadharNo
                    ProcSaveRegistration(Dgl1.Item(Col1SearchCode, mRowIndex).Value, SubgroupRegistrationType.AadharNo,
                            Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Value)
                Case Col1MasterParty
                    ProcSave("SubGroup", "SubCode", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "Parent",
                            Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Tag)
                Case Col1Area
                    ProcSave("SubGroup", "SubCode", Dgl1.Item(Col1SearchCode, mRowIndex).Value, "Area",
                            Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Tag)
                Case Col1Agent
                    ProcSaveLastValues(Dgl1.Item(Col1SearchCode, mRowIndex).Value, "Agent", Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Tag)
                Case Col1Transporter
                    ProcSaveLastValues(Dgl1.Item(Col1SearchCode, mRowIndex).Value, "Transporter", Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Tag)
                Case Col1DistanceFromHere
                    If AgL.Dman_Execute("Select Count(*) From SubgroupSiteDivisionDetail Where SubCode = '" & Dgl1.Item(Col1SearchCode, mRowIndex).Value & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code = '" & AgL.PubDivCode & "'", AgL.GCn).ExecuteScalar = 0 Then
                        mQry = "INSERT INTO SubgroupSiteDivisionDetail (SubCode, Site_Code, Div_Code, Distance) 
                                Select '" & Dgl1.Item(Col1SearchCode, mRowIndex).Value & "', '" & AgL.PubSiteCode & "', 
                                '" & AgL.PubDivCode & "', '" & Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Value & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    Else
                        mQry = "UPDATE SubgroupSiteDivisionDetail Set 
                                Distance = " & Dgl1.Item(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, mRowIndex).Value & "
                                Where SubCode = '" & Dgl1.Item(Col1SearchCode, mRowIndex).Value & "' 
                                Site_Code = '" & AgL.PubSiteCode & "' 
                                Div_Code = '" & AgL.PubDivCode & "' "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If

            End Select

            Dgl1.CurrentCell.Style.BackColor = Color.BurlyWood
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub MovRec()
        GetPartyDetail()
    End Sub
    Public Sub GetPartyDetail()
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0

        'mQry = " Select Sg.SubCode As SearchCode, Sg.SubGroupType, Sg.ManualCode As Code, Sg.DispName, Sg.Address,
        '        Sg.CityCode, C.CityName, Sg.Pin, Sg.Phone As Contact, Sg.Mobile, Sg.EMail,
        '        Sg.GroupCode, Ag.GroupName, Sg.SalesTaxPostingGroup, Sg.CreditDays, Sg.CreditLimit
        '        From SubGroup Sg 
        '        LEFT JOIN City C On Sg.CityCode = C.CityCode
        '        LEFT JOIN AcGroup Ag On Sg.GroupCode = Ag.GroupCode 
        '        Order By Sg.DispName "


        mQry = "Select S.SubCode, S.SubgroupType, S.ManualCode, S.DispName, S.Address, S.Pin, S.Phone, 
                    S.Mobile, S.EMail, S.GroupCode, Ag.GroupName, SalesTaxPostingGroup, S.CreditDays, S.CreditLimit, S.ContactPerson,
                    S.Parent, S.Area,
                    P.Name as ParentName, State.ManualCode as StateCode, A.Description as AreaName, S.CityCode, C.CityName ,
                    VReg.SalesTaxNo, VReg.PanNo, VReg.AadharNo, 
                    VLastValue.Agent, VLastValue.Transporter, VLastValue.AgentName, VLastValue.TransporterName
                    From SubGroup S 
                    LEFT JOIN AcGroup Ag On S.GroupCode = Ag.GroupCode 
                    Left Join viewHelpSubgroup P on S.Parent = P.Code
                    Left Join City C On S.CityCode = C.CityCode   
                    Left Join State On C.State = State.Code
                    Left Join Area A On S.Area = A.Code
                    LEFT JOIN (Select SubCode,
                                Max(Case When RegistrationType = '" & SubgroupRegistrationType.SalesTaxNo & "' Then RegistrationNo Else Null End) As SalesTaxNo,  
                                Max(Case When RegistrationType = '" & SubgroupRegistrationType.PanNo & "' Then RegistrationNo Else Null End) As PanNo ,  
                                Max(Case When RegistrationType = '" & SubgroupRegistrationType.AadharNo & "' Then RegistrationNo Else Null End) As AadharNo  
                                From SubgroupRegistration
                                Group By SubCode) As VReg On S.SubCode = VReg.SubCode 
                    LEFT JOIN (Select H.SubCode, Max(H.Agent) As Agent, Max(H.Transporter) As Transporter, 
                            Max(A.Name) as AgentName, Max(T.Name) as TransporterName 
                            From SubgroupSiteDivisionDetail H
                            Left Join viewHelpSubgroup A On H.Agent = A.Code 
                            Left Join viewHelpSubgroup T On H.Transporter = T.Code
                            Group By SubCode) As VLastValue  On S.SubCode = VLastValue.SubCode 
                    LEFT JOIN (Select SubCode
                                From SubgroupSiteDivisionDetail
                                Where Div_Code = '" & AgL.PubDivCode & "'
                                And Site_Code = '" & AgL.PubSiteCode & "'
                                GROUP By SubCode) As VSubGroupSite On S.SubCode = VSubGroupSite.SubCode 
                    Where S.SubgroupType Is Not Null 
                    Order By S.DispName "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                Dgl1.Rows.Add()
                Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                Dgl1.Item(Col1SearchCode, I).Value = AgL.XNull(DtTemp.Rows(I)("SubCode"))
                Dgl1.Item(Col1PartyType, I).Tag = AgL.XNull(DtTemp.Rows(I)("SubgroupType"))
                Dgl1.Item(Col1PartyType, I).Value = AgL.XNull(DtTemp.Rows(I)("SubgroupType"))
                Dgl1.Item(Col1Code, I).Value = AgL.XNull(DtTemp.Rows(I)("ManualCode"))
                Dgl1.Item(Col1DispName, I).Value = AgL.XNull(DtTemp.Rows(I)("DispName"))
                Dgl1.Item(Col1Address, I).Value = AgL.XNull(DtTemp.Rows(I)("Address")).ToString()
                Dgl1.Item(Col1City, I).Tag = AgL.XNull(DtTemp.Rows(I)("CityCode")).ToString()
                Dgl1.Item(Col1City, I).Value = AgL.XNull(DtTemp.Rows(I)("CityName")).ToString()
                Dgl1.Item(Col1PinNo, I).Value = AgL.XNull(DtTemp.Rows(I)("Pin")).ToString()
                Dgl1.Item(Col1Contact, I).Value = AgL.XNull(DtTemp.Rows(I)("Phone")).ToString()
                Dgl1.Item(Col1Mobile, I).Value = AgL.XNull(DtTemp.Rows(I)("Mobile")).ToString()
                Dgl1.Item(Col1EMail, I).Value = AgL.XNull(DtTemp.Rows(I)("EMail")).ToString()
                Dgl1.Item(Col1AccountGroup, I).Tag = AgL.XNull(DtTemp.Rows(I)("GroupCode")).ToString()
                Dgl1.Item(Col1AccountGroup, I).Value = AgL.XNull(DtTemp.Rows(I)("GroupName")).ToString()
                Dgl1.Item(Col1SalesTaxGroup, I).Value = AgL.XNull(DtTemp.Rows(I)("SalesTaxPostingGroup")).ToString()
                Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(DtTemp.Rows(I)("SalesTaxPostingGroup")).ToString()
                Dgl1.Item(Col1CreditDays, I).Value = AgL.VNull(DtTemp.Rows(I)("CreditDays")).ToString()
                Dgl1.Item(Col1CreditLimit, I).Value = AgL.VNull(DtTemp.Rows(I)("CreditLimit")).ToString()

                Dgl1.Item(Col1ContactPerson, I).Value = AgL.XNull(DtTemp.Rows(I)("ContactPerson")).ToString()

                Dgl1.Item(Col1SalesTaxNo, I).Value = AgL.XNull(DtTemp.Rows(I)("SalesTaxNo")).ToString()
                Dgl1.Item(Col1PanNo, I).Value = AgL.XNull(DtTemp.Rows(I)("PanNo")).ToString()
                Dgl1.Item(Col1AadharNo, I).Value = AgL.XNull(DtTemp.Rows(I)("AadharNo")).ToString()

                Dgl1.Item(Col1MasterParty, I).Tag = AgL.XNull(DtTemp.Rows(I)("Parent")).ToString()
                Dgl1.Item(Col1MasterParty, I).Value = AgL.XNull(DtTemp.Rows(I)("ParentName")).ToString()
                Dgl1.Item(Col1Area, I).Tag = AgL.XNull(DtTemp.Rows(I)("Area")).ToString()
                Dgl1.Item(Col1Area, I).Value = AgL.XNull(DtTemp.Rows(I)("AreaName")).ToString()
                Dgl1.Item(Col1Agent, I).Tag = AgL.XNull(DtTemp.Rows(I)("Agent")).ToString()
                Dgl1.Item(Col1Agent, I).Value = AgL.XNull(DtTemp.Rows(I)("AgentName")).ToString()
                Dgl1.Item(Col1Transporter, I).Tag = AgL.XNull(DtTemp.Rows(I)("Transporter")).ToString()
                Dgl1.Item(Col1Transporter, I).Value = AgL.XNull(DtTemp.Rows(I)("TransporterName")).ToString()
            Next
            Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
            Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells)
        End If
    End Sub
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1PartyType
                    If Dgl1.AgHelpDataSet(Col1PartyType) Is Nothing Then
                        mQry = " SELECT H.SubgroupType AS Code, SubgroupType as Name FROM SubGroupType H Where IfNull(IsActive,1)=1 And IfNull(IsCustomUI,0)=0  "
                        Dgl1.AgHelpDataSet(Col1PartyType) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1City
                    If Dgl1.AgHelpDataSet(Col1City) Is Nothing Then
                        mQry = "SELECT C.CityCode, C.CityName FROM City C  "
                        Dgl1.AgHelpDataSet(Col1City) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1AccountGroup
                    If Dgl1.AgHelpDataSet(Col1AccountGroup) Is Nothing Then
                        mQry = "SELECT Ag.GroupCode, Ag.GroupName FROM AcGroup Ag  "
                        Dgl1.AgHelpDataSet(Col1AccountGroup) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1SalesTaxGroup
                    If Dgl1.AgHelpDataSet(Col1SalesTaxGroup) Is Nothing Then
                        mQry = "Select Description As Code, Description As Description From PostingGroupSalesTaxParty "
                        Dgl1.AgHelpDataSet(Col1SalesTaxGroup) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1MasterParty
                    If Dgl1.AgHelpDataSet(Col1MasterParty) Is Nothing Then
                        mQry = "select Code, Name From viewHelpSubgroup Where Code <> '" & Dgl1.Item(Col1MasterParty, Dgl1.CurrentCell.RowIndex).Tag & "' Order By Name"
                        Dgl1.AgHelpDataSet(Col1MasterParty) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1Area
                    If Dgl1.AgHelpDataSet(Col1Area) Is Nothing Then
                        mQry = "select Code, Description From area Order By Description"
                        Dgl1.AgHelpDataSet(Col1Area) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1Agent
                    If Dgl1.AgHelpDataSet(Col1Agent) Is Nothing Then
                        If Dgl1.Item(Col1PartyType, bRowIndex).Value = AgLibrary.ClsMain.agConstants.SubgroupType.Supplier Then
                            mQry = "select Code, Name From viewHelpSubgroup Where subgroupType = '" & AgLibrary.ClsMain.agConstants.SubgroupType.PurchaseAgent & "' Order By Name"
                        Else
                            mQry = "select Code, Name From viewHelpSubgroup Where subgroupType = '" & AgLibrary.ClsMain.agConstants.SubgroupType.SalesAgent & "' Order By Name"
                        End If
                        Dgl1.AgHelpDataSet(Col1Agent) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1Transporter
                    If Dgl1.AgHelpDataSet(Col1Transporter) Is Nothing Then
                        mQry = "select Code, Name From viewHelpSubgroup Where subgroupType = '" & AgLibrary.ClsMain.agConstants.SubgroupType.Transporter & "' Order By Name"
                        Dgl1.AgHelpDataSet(Col1Transporter) = AgL.FillData(mQry, AgL.GCn)
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
    End Sub

    Private Sub FrmReportWindow_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub Dgl1_ColumnDisplayIndexChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles Dgl1.ColumnDisplayIndexChanged
        Try
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Dgl1_ColumnWidthChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles Dgl1.ColumnWidthChanged
        Try
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        Catch ex As Exception
        End Try
    End Sub

    Public Function ValidateGstNo(SalesTaxGroupParty As String, SalesTaxNo As String, City As String) As Boolean
        Dim mReason As String = ""

        Dim gStateCode As String = AgL.Dman_Execute("Select S.ManualCode From City C LEFT JOIN State S On C.State = S.Code Where C.CityCode = '" & City & "'", AgL.GCn).ExecuteScalar

        If SalesTaxGroupParty.ToUpper = "REGISTERED" Then
            If SalesTaxNo = "" Then
                mReason = "Gst No. Can not be blank"
            ElseIf Len(SalesTaxNo) <> 15 Then
                mReason = "Gst No. should be of 15 characters. Currently It is " & Len(SalesTaxNo).ToString
            ElseIf SalesTaxNo.ToString.Substring(0, 2) <> gStateCode Then
                mReason = "First two characteres of gst no are not matching with state code"
            Else
                ValidateGstNo = True
            End If
        Else
            If SalesTaxNo <> "" Then
                mReason = "Gst No. is only for registered dealers"
            End If
        End If
        If mReason <> "" Then
            MsgBox(mReason, MsgBoxStyle.Exclamation)
            ValidateGstNo = False
        Else
            ValidateGstNo = True
        End If
    End Function

    Private Sub MnuExportToExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MnuExportToExcel.Click, MnuFreezeColumns.Click
        Dim FileName As String = ""
        Dim bColumnIndex As Integer = Dgl1.CurrentCell.ColumnIndex
        Select Case sender.Name
            Case MnuExportToExcel.Name
                If MsgBox("Want to Export Grid Data", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Export Grid?...") = vbNo Then Exit Sub
                FileName = AgControls.Export.GetFileName(My.Computer.FileSystem.SpecialDirectories.Desktop)
                If FileName.Trim <> "" Then
                    Call AgControls.Export.exportExcel(Dgl1, FileName, Dgl1.Handle)
                End If

            Case MnuFreezeColumns.Name
                If MnuFreezeColumns.Checked Then
                    Dgl1.Columns(bColumnIndex).Frozen = True
                Else
                    For I As Integer = 0 To bColumnIndex
                        Dgl1.Columns(I).Frozen = False
                    Next
                End If
        End Select
    End Sub
End Class