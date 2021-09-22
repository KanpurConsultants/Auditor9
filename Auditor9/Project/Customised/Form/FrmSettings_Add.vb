Imports System.ComponentModel
Imports System.Data.SQLite
Public Class FrmSettings_Add
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1FieldName As String = "FieldName"
    Public Const Col1Value As String = "Value"

    Public Const rowTableName As Integer = 0
    Public Const rowVoucherType As Integer = 1
    Public Const rowSite As Integer = 2
    Public Const rowDivision As Integer = 3

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
            .AddAgTextColumn(Dgl1, Col1FieldName, 160, 255, Col1FieldName, True, True)
            .AddAgTextColumn(Dgl1, Col1Value, 300, 255, Col1Value, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        AgL.GridDesign(Dgl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToAddRows = False


        Dgl1.Rows.Add(4)
        Dgl1.Item(Col1Head, rowTableName).Value = "Table Name"
        Dgl1.Item(Col1Head, rowVoucherType).Value = "Voucher Type"
        Dgl1.Item(Col1Head, rowSite).Value = "Site"
        Dgl1.Item(Col1Head, rowDivision).Value = "Division"

        Dgl1.Item(Col1FieldName, rowVoucherType).Value = "V_Type"
        Dgl1.Item(Col1FieldName, rowSite).Value = "Site_Code"
        Dgl1.Item(Col1FieldName, rowDivision).Value = "Div_Code"
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
                Case rowTableName
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "Select 'SaleInvoiceSetting' As Code, 'SaleInvoiceSetting' As Name 
                                UNION ALL 
                                Select 'PurchaseInvoiceSetting' As Code, 'PurchaseInvoiceSetting' As Name 
                                UNION ALL 
                                Select 'LedgerHeadSetting' As Code, 'LedgerHeadSetting' As Name 
                                UNION ALL 
                                Select 'StockHeadSetting' As Code, 'StockHeadSetting' As Name 
                                UNION ALL 
                                Select 'DivisionSiteSetting' As Code, 'DivisionSiteSetting' As Name 
                                UNION ALL 
                                Select 'Enviro' As Code, 'Enviro' As Name 
                                UNION ALL 
                                Select 'ItemTypeSetting' As Code, 'ItemTypeSetting' As Name 
                                UNION ALL 
                                Select 'SubgroupTypeSetting' As Code, 'SubgroupTypeSetting' As Name 
                                UNION ALL 
                                Select 'MailSender' As Code, 'MailSender' As Name 
                                UNION ALL 
                                Select 'SmsSender' As Code, 'SmsSender' As Name 
                                UNION ALL 
                                Select 'ComputerSetting' As Code, 'ComputerSetting' As Name "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case rowVoucherType
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        If Dgl1.Item(Col1FieldName, rowVoucherType).Value = "V_Type" Then
                            mQry = "Select Vt.V_Type, Vt.Description from Voucher_Type Vt Order by Vt.V_Type "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        Else
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = Nothing
                        End If
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
            End Select
            If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        Dim DtFields As DataTable = Nothing
        Dim I As Integer = 0
        Select Case sender.Name
            Case BtnOk.Name
                mOkButtonPressed = True

                mQry = "PRAGMA table_info('" & Dgl1.Item(Col1Value, rowTableName).Value & "') "
                DtFields = AgL.FillData(mQry, AgL.GCn).Tables(0)

                mQry = "Select Count(*) From " & Dgl1.Item(Col1Value, rowTableName).Value & " 
                        Where 1=1 "


                If DtFields.Columns.Contains("V_Type") Then
                    mQry += " And IfNull(V_Type,'')  = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowVoucherType).Tag) & " "
                End If
                If DtFields.Columns.Contains("Site_Code") Then
                    mQry += " And IfNull(Site_Code,'')  = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowSite).Tag) & " "
                End If
                If DtFields.Columns.Contains("Div_Code") Then
                    mQry += " And IfNull(Div_Code,'')  = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDivision).Tag) & " "
                End If


                'mQry = "Select Count(*) From " & Dgl1.Item(Col1Value, rowTableName).Value & " 
                '    Where IfNull(V_Type,'')  = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowVoucherType).Tag) & "
                '    And IfNull(Site_Code,'')  = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowSite).Tag) & " 
                '    And IfNull(Div_Code,'')  = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDivision).Tag) & " "
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


        If Dgl1.Item(Col1Value, rowTableName).Value = "ComputerSetting" Then
            Dim bMaxCode As String = AgL.GetMaxId("ComputerSetting", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
            mQry = " Insert Into ComputerSetting(Code, ComputerName) 
                        Select " & bMaxCode & ", '" & Dgl1.Item(Col1Value, rowVoucherType).Value & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Else
            mQry = "PRAGMA table_info('" & Dgl1.Item(Col1Value, rowTableName).Value & "') "
            DtFields = AgL.FillData(mQry, AgL.GCn).Tables(0)

            For I = 0 To DtFields.Rows.Count - 1
                If AgL.XNull(DtFields.Rows(I)("Name")) <> "Code" And AgL.XNull(DtFields.Rows(I)("Name")) <> "Site_Code" And
                            AgL.XNull(DtFields.Rows(I)("Name")) <> "Div_Code" And
                            AgL.XNull(DtFields.Rows(I)("Name")) <> "V_Type" Then
                    mColumnFields += ", " + AgL.XNull(DtFields.Rows(I)("Name"))
                End If
            Next

            mQry = " INSERT INTO " & Dgl1.Item(Col1Value, rowTableName).Value & "(V_Type, Site_Code, Div_Code "
            mQry += mColumnFields
            mQry += ") "
            mQry += " Select " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowVoucherType).Tag) & ", 
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowSite).Tag) & ",  
                " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDivision).Tag) & " "
            mQry += mColumnFields
            mQry += " From " & Dgl1.Item(Col1Value, rowTableName).Value & ""

            If Dgl1.Item(Col1Value, rowVoucherType).Tag <> "" And Dgl1.Item(Col1Value, rowVoucherType).Tag IsNot Nothing Then
                If AgL.Dman_Execute("Select Count(*) From " & Dgl1.Item(Col1Value, rowTableName).Value & " 
                            Where V_Type = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowVoucherType).Tag) & " 
                            And Site_Code Is Null And Div_Code Is Null ", AgL.GCn).ExecuteScalar > 0 Then
                    mQry += " Where V_Type  = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowVoucherType).Tag) & " 
                                And Site_Code Is Null And Div_Code Is Null "
                ElseIf AgL.Dman_Execute("Select Count(*) From " & Dgl1.Item(Col1Value, rowTableName).Value & " 
                            Where V_Type = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowVoucherType).Tag) & " ",
                                        AgL.GCn).ExecuteScalar > 0 Then
                    mQry += " Where Code In (Select Max(Code) From " & Dgl1.Item(Col1Value, rowTableName).Value & "
                                         Where V_Type = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowVoucherType).Tag) & ") "
                Else
                    mQry += " Where Code In (Select Max(Code) From " & Dgl1.Item(Col1Value, rowTableName).Value & ") "
                End If
            End If
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If
    End Sub
    Private Sub Dgl1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
    End Sub
    Private Sub Dgl1_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Value
                    Select Case Dgl1.CurrentCell.RowIndex
                        Case rowTableName
                            If AgL.StrCmp(Dgl1.Item(Col1Value, rowTableName).Value, "ComputerSetting") Then
                                Dgl1.Item(Col1Head, rowVoucherType).Value = "Computer Name"
                                Dgl1.Item(Col1FieldName, rowVoucherType).Value = "ComputerName"
                            End If
                    End Select
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class