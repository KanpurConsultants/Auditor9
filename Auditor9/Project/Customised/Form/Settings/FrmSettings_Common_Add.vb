Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class FrmSettings_Common_Add
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"

    Public Const rowSettingType As Integer = 0
    Public Const rowSite As Integer = 1
    Public Const rowDivision As Integer = 2
    Public Const rowCategory As Integer = 3
    Public Const rowNCat As Integer = 4
    Public Const rowVoucherType As Integer = 5
    Public Const rowProcess As Integer = 6
    Public Const rowSettingGroup As Integer = 7
    Public Const rowFieldName As Integer = 8
    Public Const rowDataType As Integer = 9
    Public Const rowDataLength As Integer = 10
    Public Const rowHelpQuery As Integer = 11
    Public Const rowHelpQueryType As Integer = 12
    Public Const rowHelpSelectionType As Integer = 13

    Dim mUserAction As String = "None"
    Public ReadOnly Property UserAction() As String
        Get
            UserAction = mUserAction
        End Get
    End Property
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    'Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
    '    AgL.FPaintForm(Me, e, 0)
    'End Sub

    Public Sub IniGrid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Head, 160, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl1, Col1Mandatory, 12, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(Dgl1, Col1Value, 300, 255, Col1Value, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        AgL.GridDesign(Dgl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToAddRows = False


        Dgl1.Rows.Add(14)
        Dgl1.Item(Col1Head, rowSettingType).Value = "SettingType"
        Dgl1.Item(Col1Head, rowSite).Value = "Site"
        Dgl1.Item(Col1Head, rowDivision).Value = "Division"
        Dgl1.Item(Col1Head, rowCategory).Value = "Category"
        Dgl1.Item(Col1Head, rowNCat).Value = "NCat"
        Dgl1.Item(Col1Head, rowVoucherType).Value = "Voucher Type"
        Dgl1.Item(Col1Head, rowProcess).Value = "Process"
        Dgl1.Item(Col1Head, rowSettingGroup).Value = "Setting Group"
        Dgl1.Item(Col1Head, rowFieldName).Value = "Field Name"
        Dgl1.Item(Col1Head, rowDataType).Value = "Data Type"
        Dgl1.Item(Col1Head, rowDataLength).Value = "Data Length"
        Dgl1.Item(Col1Head, rowHelpQuery).Value = "Help Query"
        Dgl1.Item(Col1Head, rowHelpQueryType).Value = "Help Query Type"
        Dgl1.Item(Col1Head, rowHelpSelectionType).Value = "Help Selection Type"
    End Sub
    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            Me.Close()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            IniGrid()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub

            Select Case Dgl1.CurrentCell.RowIndex
                Case rowSettingType
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "Select 'General' As Code, 'General' As Name "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case rowSite
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "Select S.Code, S.Name from SiteMast S Order by S.Code "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case rowDivision
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "Select D.Div_Code, D.Div_Name from Division D Order by D.Div_Code "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case rowCategory
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "Select Distinct Category, Category As Name " &
                                " From Voucher_Type " &
                                " Order By Category "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case rowNCat
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "Select Distinct NCat, NCat As Name " &
                                " From Voucher_Type " &
                                " Order By NCat "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case rowVoucherType
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT V_Type AS Code, V_Type AS Name FROM Voucher_Type ORDER BY V_Type "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case rowProcess
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Name From viewHelpSubgroup Sg  With (NoLock) 
                                Where SubgroupType ='" & SubgroupType.Process & "' 
                                And IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case rowSettingGroup
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Name FROM SettingGroup "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case rowDataType
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT '" & AgDataType.Text & "' As Code, '" & AgDataType.Text & "' As Code "
                        mQry += " UNION ALL "
                        mQry += "Select '" & AgDataType.Number & "'  As Code, '" & AgDataType.Number & "' As Code "
                        mQry += " UNION ALL "
                        mQry += "SELECT '" & AgDataType.DateType & "'  As Code, '" & AgDataType.DateType & "' As Code "
                        mQry += " UNION ALL "
                        mQry += "Select '" & AgDataType.DateTime & "'  As Code, '" & AgDataType.DateTime & "' As Code "
                        mQry += " UNION ALL "
                        mQry += "SELECT '" & AgDataType.YesNo & "'  As Code, '" & AgDataType.YesNo & "' As Code "
                        mQry += " UNION ALL "
                        mQry += "Select '" & AgDataType.Password & "' As Code, '" & AgDataType.Password & "' As Code "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case rowHelpQueryType
                    mQry = "SELECT '" & AgHelpQueryType.SqlQuery & "' As Code, '" & AgHelpQueryType.SqlQuery & "' As Name "
                    mQry += " UNION ALL "
                    mQry += "Select '" & AgHelpQueryType.CSV & "' As Code, '" & AgHelpQueryType.CSV & "' As Name "
                    mQry += " UNION ALL "
                    mQry += "SELECT '" & AgHelpQueryType.ClassName & "' As Code, '" & AgHelpQueryType.ClassName & "' As Name "
                    mQry += " UNION ALL "
                    mQry += "Select '" & AgHelpQueryType.TableName & "' As Code, '" & AgHelpQueryType.TableName & "' As Name "
                    mQry += " UNION ALL "
                    mQry += "SELECT '" & AgHelpQueryType.ViewName & "' As Code, '" & AgHelpQueryType.ViewName & "' As Name "
                    mQry += " UNION ALL "
                    mQry += "Select '" & AgHelpQueryType.SubgroupType & "' As Code, '" & AgHelpQueryType.SubgroupType & "' As Name "
                    mQry += " UNION ALL "
                    mQry += "SELECT '" & AgHelpQueryType.ItemV_Type & "' As Code, '" & AgHelpQueryType.ItemV_Type & "' As Name "
                    mQry += " UNION ALL "
                    mQry += "Select '" & AgHelpQueryType.None & "' As Code, '" & AgHelpQueryType.None & "' As Name "
                    Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)


                Case rowHelpSelectionType
                    mQry = "SELECT '" & AgHelpSelectionType.None & "' As Code, '" & AgHelpSelectionType.None & "' As Name "
                    mQry += " UNION ALL "
                    mQry += "Select '" & AgHelpSelectionType.SingleSelect & "' As Code, '" & AgHelpSelectionType.SingleSelect & "' As Name "
                    mQry += " UNION ALL "
                    mQry += "SELECT '" & AgHelpSelectionType.MultiSelect & "' As Code, '" & AgHelpSelectionType.MultiSelect & "' As Name "
                    Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
            End Select

            If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        Dim I As Integer = 0
        Select Case sender.Name
            Case BtnOk.Name
                mOkButtonPressed = True

                mQry = "Select Count(*) From Setting 
                        Where 1=1 "
                If AgL.XNull(Dgl1.Item(Col1Value, rowSite).Tag) <> "" Then
                    mQry += " And IfNull(Site_Code,'')  = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowSite).Tag) & " "
                End If
                If AgL.XNull(Dgl1.Item(Col1Value, rowDivision).Tag) <> "" Then
                    mQry += " And IfNull(Div_Code,'')  = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDivision).Tag) & " "
                End If
                If AgL.XNull(Dgl1.Item(Col1Value, rowCategory).Tag) <> "" Then
                    mQry += " And IfNull(Category,'')  = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowCategory).Tag) & " "
                End If
                If AgL.XNull(Dgl1.Item(Col1Value, rowNCat).Tag) <> "" Then
                    mQry += " And IfNull(NCat,'')  = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowNCat).Tag) & " "
                End If
                If AgL.XNull(Dgl1.Item(Col1Value, rowVoucherType).Tag) <> "" Then
                    mQry += " And IfNull(VoucherType,'')  = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowVoucherType).Tag) & " "
                End If
                If AgL.XNull(Dgl1.Item(Col1Value, rowProcess).Tag) <> "" Then
                    mQry += " And IfNull(Process,'')  = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowProcess).Tag) & " "
                End If
                If AgL.XNull(Dgl1.Item(Col1Value, rowSettingGroup).Tag) <> "" Then
                    mQry += " And IfNull(SettingGroup,'')  = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowSettingGroup).Tag) & " "
                End If
                mQry += " And IfNull(FieldName,'')  = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowFieldName).Value) & " "
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
                    MsgBox("Setting already exist...!", MsgBoxStyle.Information)
                    Exit Sub
                End If

                FSave()
                mUserAction = sender.text
                Me.Close()
        End Select
    End Sub
    Public Sub FSave()
        Dim DtFields As DataTable = Nothing
        Dim I As Integer = 0
        Dim mColumnFields = ""

        Dim bCode As String = AgL.GetMaxId("Setting", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

        mQry = "INSERT INTO Setting (Code, SettingType, Div_Code, Site_Code, 
                Category, NCat, VoucherType, Process, SettingGroup, FieldName, DataType, 
                DataLength, HelpQuery, HelpQueryType, HelpSelectionType, EntryBy, EntryDate)
                VALUES (" & AgL.Chk_Text(bCode) & ", 
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowSettingType).Tag) & ", 
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDivision).Tag) & ",  
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowSite).Tag) & ", 
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowCategory).Tag) & ", 
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowNCat).Tag) & ", 
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowVoucherType).Tag) & ", 
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowProcess).Tag) & ", 
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowSettingGroup).Tag) & ", 
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowFieldName).Value) & ", 
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDataType).Tag) & ", 
                " & Val(Dgl1.Item(Col1Value, rowDataLength).Value) & ", 
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowHelpQuery).Value) & ", 
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowHelpQueryType).Tag) & ", 
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowHelpSelectionType).Tag) & ", 
                " & AgL.Chk_Text(AgL.PubUserName) & ", 
                " & AgL.Chk_Date(AgL.PubLoginDate) & ")"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub
    Private Sub Dgl1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
    End Sub
End Class