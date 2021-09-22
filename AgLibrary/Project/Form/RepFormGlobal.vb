Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient
Imports System.Data.SQLite


Public Class RepFormGlobal

#Region "Danger Zone"
    Private WithEvents Dg As DataGridView
    Dim AgL As AgLibrary.ClsMain
    Dim Da As SQLiteDataAdapter
    Dim DsRep As DataSet = Nothing, DsRep1 As DataSet = Nothing, DsRep2 As DataSet = Nothing
    Dim DsHelp As DataSet = Nothing
    Dim mPrnHnd As PrintHandler

    Structure SelectionGrid
        Dim DgName As String
        Dim DgQry As String
        Dim OrderBy As String
        Dim GridString As String
        Dim FormulaString As String
        Dim DataTableName As String
    End Structure

    Private Class DgHelpCMenu
        Public Const SelectAll As String = "Select All"
        Public Const UnselectAll As String = "Unselect All"
    End Class

    Dim XSD_Rep As Boolean = False, PrnHnd_Rep As Boolean = False, ShowReportAuto As Boolean = True, Grid_Rep As Boolean = False
    Dim MessageBox As Boolean = False, SubRep1 As Boolean = False, SubRep2 As Boolean = False


    Dim RepTitle As String = "", RepName As String = "", mQry As String = "", Fld As String = "", mQry1 As String = ""
    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing
    Dim SelGrid() As SelectionGrid = Nothing

    

#End Region

#Region "Queries Definition"
    Dim mHelpCityQry$ = "Select Convert(BIT,0) As [Select],CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select Convert(BIT,0) As [Select],State_Code, State_Desc From State "
    Dim mHelpUserQry$ = "Select Convert(BIT,0) As [Select],User_Name As Code, User_Name As [User] From UserMast "
    Dim mHelpEntryPointQry$ = " Select Distinct Convert(BIT,0) As [Select], User_Permission.MnuText AS code , User_Permission.MnuText As [Entry Point] From User_Permission  "
    Dim mHelpBankQry$ = "Select Convert(BIT,0) As [Select],Bank_Code Code, Bank_Name As [Bank Name] From Bank "
    Dim mHelpBankBranchQry$ = "Select Convert(BIT,0) As [Select],BankBranch_Code Code, BankBranch_Name As [Bank Branch Name] From BankBranch "
#End Region

#Region "Common Code"

    Private Sub RepFormGlobal_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        Da = Nothing
        DsRep = Nothing
        DsHelp = Nothing
    End Sub

    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If Me.ActiveControl IsNot Nothing Then
            If Not (TypeOf (Me.ActiveControl) Is AgControls.AgDataGrid) Then
                If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
            End If
        End If
    End Sub

    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then Exit Sub
        If Me.ActiveControl Is Nothing Then Exit Sub
        AgL.CheckQuote(e)
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.WinSetting(Me, 534, 750, 0, 0)
            TxtFind.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub

    Public Sub CreateHelpGrid(ByVal Query As String, ByVal TableName As String)
        Dim I As Integer

        If DsHelp Is Nothing Then DsHelp = New DataSet("Selection Grid")

        If SelGrid Is Nothing Then
            I = 0
        Else
            I = UBound(SelGrid) + 1
        End If
        ReDim Preserve SelGrid(I)
        SelGrid(I).DgQry = Query
        SelGrid(I).OrderBy = "Order By 3"
        SelGrid(I).DataTableName = TableName

        DsHelp = AgL.FillData("Select * From (" & SelGrid(I).DgQry & ") xyz " & SelGrid(I).OrderBy & " ", AgL.GCn)

        ''Primary Key Assign To Data Table
        Dim mCol(1) As DataColumn

        mCol(0) = DsHelp.Tables(TableName).Columns(1)
        DsHelp.Tables(TableName).PrimaryKey = mCol
        ''====================================
    End Sub

    Public Sub ReCreateHelpGrid(ByVal TableName As String, ByVal StrSelectionType As String)
        Dim I As Integer, bIntSelectIndex% = 0
        Dim bStrMainQry$ = "", bStrTempQry$ = ""
        Dim bDtTemp As DataTable = Nothing
        Try

            If SelGrid IsNot Nothing _
                And DsHelp IsNot Nothing Then

                For I = 0 To UBound(SelGrid)
                    If AgL.StrCmp(SelGrid(I).DataTableName, TableName) Then
                        bStrMainQry = SelGrid(I).DgQry.Trim
                        bIntSelectIndex = bStrMainQry.IndexOf("[Select]")

                        bStrTempQry = bStrMainQry.Substring(bIntSelectIndex, bStrMainQry.Length - bIntSelectIndex)


                        '"Select Convert(BIT,0) As "

                        If AgL.StrCmp(StrSelectionType, DgHelpCMenu.SelectAll) Then
                            bStrTempQry = "Select Convert(BIT,1) As " & bStrTempQry
                        Else
                            bStrTempQry = "Select Convert(BIT,0) As " & bStrTempQry
                        End If
                        SelGrid(I).DgQry = bStrTempQry

                        DsHelp = AgL.FillData("Select * From (" & SelGrid(I).DgQry & ") xyz " & SelGrid(I).OrderBy & " ", AgL.GCn)


                        ''Primary Key Assign To Data Table
                        Dim mCol(1) As DataColumn

                        mCol(0) = DsHelp.Tables(TableName).Columns(1)
                        DsHelp.Tables(TableName).PrimaryKey = mCol
                        ''====================================
                        Exit For
                    End If
                Next
            End If
            ''====================================
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub RemoveHelpGridFilter()
        Dim I As Integer

        If SelGrid IsNot Nothing Then
            For I = 0 To UBound(SelGrid) - 1
                DsHelp.Tables(SelGrid(I).DataTableName).DefaultView.RowFilter = Nothing
            Next
        End If
    End Sub

    Public Sub Arrange_Grid()
        Dim I As Integer, J As Integer
        Dim Cnode As TreeNode
        Try
            ''TreeView
            If DsHelp Is Nothing Then Exit Sub
            TreeView1.CheckBoxes = True
            For I = 0 To DsHelp.Tables.Count - 1
                Cnode = TreeView1.Nodes.Add(DsHelp.Tables(I).TableName)                
                SelGrid(I).DgName = "Dg" & I
                Dg = New DataGridView
                Dg.Name = SelGrid(I).DgName
                Dg.RowHeadersWidth = 30
                Dg.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken
                Dg.ContextMenuStrip = CMnu

                Cnode.Tag = I
                With Me
                    .Controls.Add(Dg)
                    AgL.GridDesign(CType(Me.Controls(SelGrid(I).DgName), DataGridView))

                    .Controls(SelGrid(I).DgName).Top = TreeView1.Location.Y   'TreeView1.Location.Y + (I * 20)
                    .Controls(SelGrid(I).DgName).Left = 370                   'TreeView1.Left + TreeView1.Width
                    .Controls(SelGrid(I).DgName).Width = 310
                    .Controls(SelGrid(I).DgName).Height = 200



                    CType(.Controls(SelGrid(I).DgName), DataGridView).DataSource = DsHelp.Tables(DsHelp.Tables(I).TableName)
                    CType(.Controls(SelGrid(I).DgName), DataGridView).Columns(0).Width = 50

                    CType(.Controls(SelGrid(I).DgName), DataGridView).Columns(1).Visible = False
                    CType(.Controls(SelGrid(I).DgName), DataGridView).Columns(2).Width = 240
                    CType(.Controls(SelGrid(I).DgName), DataGridView).AllowUserToAddRows = False
                    CType(.Controls(SelGrid(I).DgName), DataGridView).AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

                    For J = 0 To CType(.Controls(SelGrid(I).DgName), DataGridView).Columns.Count - 1
                        CType(.Controls(SelGrid(I).DgName), DataGridView).Columns(J).ReadOnly = True
                    Next


                    CType(.Controls(SelGrid(I).DgName), DataGridView).BringToFront()

                    RemoveHandler DirectCast(.Controls(SelGrid(I).DgName), System.Windows.Forms.DataGridView).KeyDown, AddressOf Grid_KeyDown
                    AddHandler DirectCast(.Controls(SelGrid(I).DgName), System.Windows.Forms.DataGridView).KeyDown, AddressOf Grid_KeyDown

                    RemoveHandler DirectCast(.Controls(SelGrid(I).DgName), System.Windows.Forms.DataGridView).KeyPress, AddressOf Grid_KeyPress
                    AddHandler DirectCast(.Controls(SelGrid(I).DgName), System.Windows.Forms.DataGridView).KeyPress, AddressOf Grid_KeyPress
                End With
                Cnode.Checked = True
            Next

            With LblHelp
                .Top = TxtFind.Top - 10
                .Left = 370
                .Width = 360
            End With

            If TreeView1.Nodes.Count > 0 Then
                TreeView1.SelectedNode = TreeView1.Nodes(0)
                LblHelp.Text = TreeView1.Nodes(0).Text
            Else
                LblHelp.Text = ""
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TreeView1_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterCheck
        Try
            If e.Node Is Nothing Then Exit Sub

            CType(sender, TreeView).SelectedNode = e.Node

            With CType(Me.Controls(SelGrid(sender.SelectedNode.tag).DgName), DataGridView)
                If .Columns(0) Is Nothing Then Exit Sub

                .Columns(0).ReadOnly = CType(sender, TreeView).SelectedNode.Checked


                LblHelp.Text = sender.SelectedNode.Text
                TxtFind.Text = ""
                DsHelp.Tables(SelGrid(sender.SelectedNode.Tag).DataTableName).DefaultView.RowFilter = Nothing
                CType(Me.Controls(SelGrid(sender.SelectedNode.tag).DgName), DataGridView).BringToFront()
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub ProcSelectHelpDg(ByVal DgHelp As DataGridView, ByVal StrSelectionType As String, ByVal StrTableName As String)
        Dim bIntI% = 0
        Try
            Me.Cursor = Cursors.WaitCursor

            With DgHelp
                If .Columns(0) Is Nothing Then Exit Sub

                For bIntI = 0 To .Rows.Count - 1
                    If AgL.StrCmp(StrSelectionType, DgHelpCMenu.SelectAll) Then
                        .Item(0, bIntI).Value = True
                    ElseIf AgL.StrCmp(StrSelectionType, DgHelpCMenu.UnselectAll) Then
                        .Item(0, bIntI).Value = False
                    End If
                Next
                'ReCreateHelpGrid(StrTableName, StrSelectionType)
            End With

        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub TreeView1_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        Try
            If sender.SelectedNode IsNot Nothing Then
                LblHelp.Text = sender.SelectedNode.Text
                TxtFind.Text = ""
                DsHelp.Tables(SelGrid(sender.SelectedNode.Tag).DataTableName).DefaultView.RowFilter = Nothing
                CType(Me.Controls(SelGrid(sender.SelectedNode.tag).DgName), DataGridView).BringToFront()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TreeView1_BeforeCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles TreeView1.BeforeCheck

    End Sub

    Private Sub TreeView1_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        Try
            If sender.SelectedNode IsNot Nothing Then
                LblHelp.Text = sender.SelectedNode.Text
                TxtFind.Text = ""
                DsHelp.Tables(SelGrid(sender.SelectedNode.Tag).DataTableName).DefaultView.RowFilter = Nothing
                CType(Me.Controls(SelGrid(sender.SelectedNode.tag).DgName), DataGridView).BringToFront()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Grid_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyCode = Keys.Delete Then
                TxtFind.Text = ""
                DsHelp.Tables(SelGrid(TreeView1.SelectedNode.Tag).DataTableName).DefaultView.RowFilter = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Grid_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            If CType(Me.Controls(SelGrid(TreeView1.SelectedNode.Tag).DgName), DataGridView).CurrentCell Is Nothing Then
                DsHelp.Tables(SelGrid(TreeView1.SelectedNode.Tag).DataTableName).DefaultView.RowFilter = Nothing
            End If

            Fld = CType(Me.Controls(SelGrid(TreeView1.SelectedNode.Tag).DgName), DataGridView).Columns(CType(Me.Controls(SelGrid(TreeView1.SelectedNode.Tag).DgName), DataGridView).CurrentCell.ColumnIndex).Name
            If Asc(e.KeyChar) = Keys.Back Then
                If TxtFind.Text <> "" Then TxtFind.Text = Microsoft.VisualBasic.Left(TxtFind.Text, Len(TxtFind.Text) - 1)
            End If

            If CType(Me.Controls(SelGrid(TreeView1.SelectedNode.Tag).DgName), DataGridView).Columns(CType(Me.Controls(SelGrid(TreeView1.SelectedNode.Tag).DgName), DataGridView).CurrentCell.ColumnIndex).Index > 0 Then
                TextBox1_KeyPress(TxtFind, e)
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtFind.KeyPress
        Try
            'RowsFilter(SelGrid(TreeView1.SelectedNode.Tag).DgQry, CType(Me.Controls(SelGrid(TreeView1.SelectedNode.Tag).DgName), DataGridView), sender, e, Fld, DsHelp.Tables(TreeView1.SelectedNode.Text))            
            RowsFilter(e)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Function RowsFilter(ByVal selStr As String, ByVal CtrlObj As Object, ByVal TXT As TextBox, ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal FndFldName As String, ByVal DTable As DataTable) As Int16
    '    Try
    '        Dim strExpr As String, findStr As String, bSelStr As String = ""
    '        Dim sa As String
    '        Dim IntRow As Int16
    '        Dim i As Int16
    '        sa = TXT.Text
    '        bSelStr = selStr

    '        If sa.Length = 0 And Asc(e.KeyChar) = 8 Then IntRow = 0 : CtrlObj.CurrentCell = CtrlObj(FndFldName, IntRow) : Exit Function
    '        If TXT.Text = "(null)" Then
    '            findStr = e.KeyChar
    '        Else
    '            findStr = IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19, TXT.Text, TXT.Text + e.KeyChar)
    '        End If
    '        strExpr = "ltrim([" & FndFldName & "])  like '%" & findStr & "%' "

    '        ''==================================< Filter DsHelp For Searching >====================================================
    '        DsHelp.Tables(SelGrid(TreeView1.SelectedNode.Tag).DataTableName).DefaultView.RowFilter = Nothing
    '        DsHelp.Tables(SelGrid(TreeView1.SelectedNode.Tag).DataTableName).DefaultView.RowFilter = " [" & FndFldName & "] like '%" & findStr & "%' "
    '        ''==================================< *************************** >====================================================

    '        selStr = "Select * From (" & selStr & ") xyz where " + strExpr & " order by [" & FndFldName & "]"
    '        Dim dtt As New DataTable, j As Int16
    '        Da = New SqlClient.SqlDataAdapter(selStr, AgL.GCn)
    '        Da.Fill(dtt)
    '        If dtt.Rows.Count > 0 Then
    '            For i = 0 To dtt.Rows.Count - 1
    '                For j = 0 To CType(Me.Controls(SelGrid(TreeView1.SelectedNode.Tag).DgName), DataGridView).RowCount - 1
    '                    If CType(Me.Controls(SelGrid(TreeView1.SelectedNode.Tag).DgName), DataGridView).Item(FndFldName, j).Value = dtt.Rows(i).Item(FndFldName) Then
    '                        CType(Me.Controls(SelGrid(TreeView1.SelectedNode.Tag).DgName), DataGridView).CurrentCell = CType(Me.Controls(SelGrid(TreeView1.SelectedNode.Tag).DgName), DataGridView)(FndFldName, j)
    '                        TXT.Text = TXT.Text + IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19, "", e.KeyChar)
    '                        Exit Try
    '                    End If
    '                Next
    '            Next

    '        Else
    '            strExpr = "[" & FndFldName & "]  like '%" & sa & "%' "
    '            selStr = "Select * From (" & bSelStr & ") xyz where " + strExpr & " order by [" & FndFldName & "]"

    '            ''==================================< Filter DsHelp For Searching >====================================================
    '            DsHelp.Tables(SelGrid(TreeView1.SelectedNode.Tag).DataTableName).DefaultView.RowFilter = Nothing
    '            DsHelp.Tables(SelGrid(TreeView1.SelectedNode.Tag).DataTableName).DefaultView.RowFilter = " [" & FndFldName & "] like '%" & sa & "%' "
    '            ''==================================< *************************** >====================================================

    '            Da = New SqlClient.SqlDataAdapter(selStr, AgL.GCn)
    '            Da.Fill(dtt)
    '            If dtt.Rows.Count > 0 Then
    '                For i = 0 To dtt.Rows.Count - 1
    '                    For j = 0 To CType(Me.Controls(SelGrid(TreeView1.SelectedNode.Tag).DgName), DataGridView).RowCount - 1
    '                        If CType(Me.Controls(SelGrid(TreeView1.SelectedNode.Tag).DgName), DataGridView).Item(FndFldName, j).Value = dtt.Rows(i).Item(FndFldName) Then
    '                            CType(Me.Controls(SelGrid(TreeView1.SelectedNode.Tag).DgName), DataGridView).CurrentCell = CType(Me.Controls(SelGrid(TreeView1.SelectedNode.Tag).DgName), DataGridView)(FndFldName, j)
    '                            TXT.Text = TXT.Text + IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19, "", e.KeyChar)
    '                            Exit Try
    '                        End If
    '                    Next
    '                Next
    '            Else
    '                DsHelp.Tables(SelGrid(TreeView1.SelectedNode.Tag).DataTableName).DefaultView.RowFilter = Nothing
    '            End If
    '            If Asc(e.KeyChar) <> Keys.Back Then e.Handled = True

    '        End If
    '    Catch ex As Exception
    '        'MsgBox(ex.Message)
    '    End Try

    'End Function

    '=============================================
    'This Function Is For Filtering(Searching) Row
    'And Returning Searched Text
    '==============================================
    Private Function RowsFilter(ByVal e As System.Windows.Forms.KeyPressEventArgs) As Int16
        Try
            Dim StrExpr As String, StrFind As String
            Dim StrValue As String, StrField As String
            'Dim SrtCol As Short



            If Not CType(Me.Controls(SelGrid(TreeView1.SelectedNode.Tag).DgName), DataGridView).Rows.Count > 0 Then Exit Function

            'SrtCol = Dg.Columns(Fld).Index
            StrField = Fld

            StrValue = TxtFind.Text
            If TxtFind.Text = "(null)" Then
                StrFind = e.KeyChar
            Else
                StrFind = IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19, TxtFind.Text, TxtFind.Text + e.KeyChar)
            End If

            StrExpr = "[" & StrField & "] like '%" & StrFind & "%' "
            DsHelp.Tables(SelGrid(TreeView1.SelectedNode.Tag).DataTableName).DefaultView.RowFilter = StrExpr
            If Not DsHelp.Tables(SelGrid(TreeView1.SelectedNode.Tag).DataTableName).DefaultView.Count > 0 Then
                TxtFind.Text = FFilterRecursive(StrField, Microsoft.VisualBasic.Left(StrFind, Microsoft.VisualBasic.Len(StrFind) - 1))
            Else
                TxtFind.Text = TxtFind.Text + IIf(Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = 4 Or Asc(e.KeyChar) = 19, "", e.KeyChar)
            End If
            If Asc(e.KeyChar) <> Keys.Back Then e.Handled = True
            CType(Me.Controls(SelGrid(TreeView1.SelectedNode.Tag).DgName), DataGridView).CurrentCell = CType(Me.Controls(SelGrid(TreeView1.SelectedNode.Tag).DgName), DataGridView)(StrField, 0)
        Catch ex As Exception
            MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
            DsHelp.Tables(SelGrid(TreeView1.SelectedNode.Tag).DataTableName).DefaultView.RowFilter = Nothing
        End Try
    End Function
    '========================================================
    'This Function Is For Filtering(Searching) Row Recursivly
    'And Returning Searched Text
    '========================================================
    Private Function FFilterRecursive(ByVal StrField As String, ByVal StrFind As String) As String
        Dim StrExpr As String
        Try
            StrExpr = "[" & StrField & "] like '%" & StrFind & "%' "
            DsHelp.Tables(SelGrid(TreeView1.SelectedNode.Tag).DataTableName).DefaultView.RowFilter = StrExpr
            If Not DsHelp.Tables(SelGrid(TreeView1.SelectedNode.Tag).DataTableName).DefaultView.Count > 0 Then
                StrFind = FFilterRecursive(StrField, Microsoft.VisualBasic.Left(StrFind, Microsoft.VisualBasic.Len(StrFind) - 1))
            End If
        Catch ex As Exception
            'MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, StrMsgTitle)
            DsHelp.Tables(SelGrid(TreeView1.SelectedNode.Tag).DataTableName).DefaultView.RowFilter = Nothing
        End Try
        Return StrFind
    End Function
    Public Sub Ini_Grp(Optional ByVal Date1_Text As String = "", Optional ByVal Date1_Value As String = "",
                        Optional ByVal Date2_Text As String = "", Optional ByVal Date2_Value As String = "",
                        Optional ByVal Cmbo1_Text As String = "", Optional ByVal Cmbo1_Value() As String = Nothing, Optional ByVal mConn1 As SQLiteConnection = Nothing,
                        Optional ByVal Cmbo2_Text As String = "", Optional ByVal Cmbo2_Value() As String = Nothing, Optional ByVal mConn2 As SQLiteConnection = Nothing,
                        Optional ByVal Cmbo3_Text As String = "", Optional ByVal Cmbo3_Value() As String = Nothing, Optional ByVal mConn3 As SQLiteConnection = Nothing,
                        Optional ByVal Cmbo4_Text As String = "", Optional ByVal Cmbo4_Value() As String = Nothing, Optional ByVal mConn4 As SQLiteConnection = Nothing,
                        Optional ByVal Cmbo5_Text As String = "", Optional ByVal Cmbo5_Value() As String = Nothing, Optional ByVal mConn5 As SQLiteConnection = Nothing
                        )
        Dim Row1Top() As Integer = {18, 14}, Row2Top() As Integer = {40, 36}, Row3Top() As Integer = {62, 58}, Row4Top() As Integer = {84, 80}, Row5Top() As Integer = {106, 102}, Row6Top() As Integer = {128, 124}, Row7Top() As Integer = {150, 146}
        Dim Row1 As Boolean, Row2 As Boolean, Row3 As Boolean, Row4 As Boolean, Row5 As Boolean, Row6 As Boolean, Row7 As Boolean
        Dim i As Integer, GrpHeight As Integer = 0
        Try
            If Date1_Text <> "" Then
                Row1 = True
                LblDate1.Visible = True
                LblDate1.Top = Row1Top(0)
                LblDate1.Text = Date1_Text

                Date1.Visible = True
                Date1.Top = Row1Top(1)
                Date1.Text = Date1_Value

                GrpHeight = GrpHeight + Date1.Height
            End If

            If Date2_Text <> "" Then
                If Row1 = False Then
                    Row1 = True
                    LblDate2.Top = Row1Top(0)
                    Date2.Top = Row1Top(1)
                Else
                    Row2 = True
                End If

                LblDate2.Visible = True
                LblDate2.Text = Date2_Text

                Date2.Visible = True
                Date2.Text = Date2_Value

                GrpHeight = GrpHeight + Date2.Height
            End If

            If Cmbo1_Text <> "" Then
                If Row1 = False Then
                    Row1 = True
                    LblCmbo1.Top = Row1Top(0)
                    Cmbo1.Top = Row1Top(1)
                ElseIf Row2 = False Then
                    Row2 = True
                    LblCmbo1.Top = Row2Top(0)
                    Cmbo1.Top = Row2Top(1)
                Else
                    Row3 = True
                End If

                LblCmbo1.Visible = True
                LblCmbo1.Text = Cmbo1_Text

                Cmbo1.Visible = True
                If Cmbo1_Value Is Nothing Then
                    Cmbo1.DropDownStyle = ComboBoxStyle.Simple
                    Cmbo1.AgCmboMaster = True
                Else
                    If mConn1 Is Nothing Then
                        For i = 0 To UBound(Cmbo1_Value)
                            Cmbo1.Items.Add(Cmbo1_Value(i))
                        Next
                        Cmbo1.Text = Cmbo1_Value(0)
                    Else
                        AgCL.IniAgHelpList(mConn1, Cmbo1, Cmbo1_Value(0), "Name", "Code")
                    End If

                End If

                GrpHeight = GrpHeight + Cmbo1.Height
            End If


            If Cmbo2_Text <> "" Then
                If Row1 = False Then
                    Row1 = True
                    LblCmbo2.Top = Row1Top(0)
                    Cmbo2.Top = Row1Top(1)
                ElseIf Row2 = False Then
                    Row2 = True
                    LblCmbo2.Top = Row2Top(0)
                    Cmbo2.Top = Row2Top(1)
                ElseIf Row3 = False Then
                    Row3 = True
                    LblCmbo2.Top = Row3Top(0)
                    Cmbo2.Top = Row3Top(1)
                Else
                    Row4 = True
                End If

                LblCmbo2.Visible = True
                LblCmbo2.Text = Cmbo2_Text

                Cmbo2.Visible = True
                If Cmbo2_Value Is Nothing Then
                    Cmbo2.DropDownStyle = ComboBoxStyle.Simple
                    Cmbo2.AgCmboMaster = True
                Else
                    If mConn2 Is Nothing Then
                        For i = 0 To UBound(Cmbo2_Value)
                            Cmbo2.Items.Add(Cmbo2_Value(i))
                        Next
                        Cmbo2.Text = Cmbo2_Value(0)
                    Else
                        AgCL.IniAgHelpList(mConn2, Cmbo2, Cmbo2_Value(0), "Name", "Code")
                    End If
                End If

                GrpHeight = GrpHeight + Cmbo2.Height
            End If

            If Cmbo3_Text <> "" Then
                If Row1 = False Then
                    Row1 = True
                    LblCmbo3.Top = Row1Top(0)
                    Cmbo3.Top = Row1Top(1)
                ElseIf Row2 = False Then
                    Row2 = True
                    LblCmbo3.Top = Row2Top(0)
                    Cmbo3.Top = Row2Top(1)
                ElseIf Row3 = False Then
                    Row3 = True
                    LblCmbo3.Top = Row3Top(0)
                    Cmbo3.Top = Row3Top(1)
                ElseIf Row4 = False Then
                    Row4 = True
                    LblCmbo3.Top = Row4Top(0)
                    Cmbo3.Top = Row4Top(1)
                Else
                    Row5 = True
                End If

                LblCmbo3.Visible = True
                LblCmbo3.Text = Cmbo3_Text

                Cmbo3.Visible = True
                If Cmbo3_Value Is Nothing Then
                    Cmbo3.DropDownStyle = ComboBoxStyle.Simple
                    Cmbo3.AgCmboMaster = True
                Else
                    If mConn3 Is Nothing Then
                        For i = 0 To UBound(Cmbo3_Value)
                            Cmbo3.Items.Add(Cmbo3_Value(i))
                        Next
                        Cmbo3.Text = Cmbo3_Value(0)
                    Else
                        AgCL.IniAgHelpList(mConn3, Cmbo3, Cmbo3_Value(0), "Name", "Code")
                    End If
                End If

                GrpHeight = GrpHeight + Cmbo3.Height
            End If

            If Cmbo4_Text <> "" Then
                If Row1 = False Then
                    Row1 = True
                    LblCmbo4.Top = Row1Top(0)
                    Cmbo4.Top = Row1Top(1)
                ElseIf Row2 = False Then
                    Row2 = True
                    LblCmbo4.Top = Row2Top(0)
                    Cmbo4.Top = Row2Top(1)
                ElseIf Row3 = False Then
                    Row3 = True
                    LblCmbo4.Top = Row3Top(0)
                    Cmbo4.Top = Row3Top(1)
                ElseIf Row4 = False Then
                    Row4 = True
                    LblCmbo4.Top = Row4Top(0)
                    Cmbo4.Top = Row4Top(1)
                ElseIf Row5 = False Then
                    Row5 = True
                    LblCmbo4.Top = Row5Top(0)
                    Cmbo4.Top = Row5Top(1)
                Else
                    Row6 = True
                End If

                LblCmbo4.Visible = True
                LblCmbo4.Text = Cmbo4_Text

                Cmbo4.Visible = True
                If Cmbo4_Value Is Nothing Then
                    Cmbo4.DropDownStyle = ComboBoxStyle.Simple
                    Cmbo4.AgCmboMaster = True
                Else
                    If mConn4 Is Nothing Then
                        For i = 0 To UBound(Cmbo4_Value)
                            Cmbo4.Items.Add(Cmbo4_Value(i))
                        Next
                        Cmbo4.Text = Cmbo4_Value(0)
                    Else
                        AgCL.IniAgHelpList(mConn4, Cmbo4, Cmbo4_Value(0), "Name", "Code")
                    End If
                End If

                GrpHeight = GrpHeight + Cmbo4.Height
            End If

            If Cmbo5_Text <> "" Then
                If Row1 = False Then
                    Row1 = True
                    LblCmbo5.Top = Row1Top(0)
                    Cmbo5.Top = Row1Top(1)
                ElseIf Row2 = False Then
                    Row2 = True
                    LblCmbo5.Top = Row2Top(0)
                    Cmbo5.Top = Row2Top(1)
                ElseIf Row3 = False Then
                    Row3 = True
                    LblCmbo5.Top = Row3Top(0)
                    Cmbo5.Top = Row3Top(1)
                ElseIf Row4 = False Then
                    Row4 = True
                    LblCmbo5.Top = Row4Top(0)
                    Cmbo5.Top = Row4Top(1)
                ElseIf Row5 = False Then
                    Row5 = True
                    LblCmbo5.Top = Row5Top(0)
                    Cmbo5.Top = Row5Top(1)
                ElseIf Row6 = False Then
                    Row6 = True
                    LblCmbo5.Top = Row6Top(0)
                    Cmbo5.Top = Row6Top(1)
                Else
                    Row7 = True
                End If

                LblCmbo5.Visible = True
                LblCmbo5.Text = Cmbo5_Text

                Cmbo5.Visible = True
                If Cmbo5_Value Is Nothing Then
                    Cmbo5.DropDownStyle = ComboBoxStyle.Simple
                    Cmbo5.AgCmboMaster = True
                Else
                    If mConn5 Is Nothing Then
                        For i = 0 To UBound(Cmbo5_Value)
                            Cmbo5.Items.Add(Cmbo5_Value(i))
                        Next
                        Cmbo5.Text = Cmbo5_Value(0)
                    Else
                        AgCL.IniAgHelpList(mConn5, Cmbo5, Cmbo5_Value(0), "Name", "Code")
                    End If
                End If

                GrpHeight = GrpHeight + Cmbo5.Height
            End If

            TreeView1.Height = TreeView1.Height + (Grp1.Height - (GrpHeight + 30))
            Grp1.Height = GrpHeight + 30

            TxtFind.Top = Grp1.Top + Grp1.Height + 5
            TreeView1.Top = TxtFind.Top + TxtFind.Height + 5
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub FillGridString()
        Dim I As Integer, J As Integer
        Try
            For I = 0 To UBound(SelGrid)
                CType(Me.Controls(SelGrid(I).DgName), DataGridView).Tag = ""
                TreeView1.Nodes(I).ToolTipText = ""

                If TreeView1.Nodes(I).Checked = False Then
                    For J = 0 To CType(Me.Controls(SelGrid(I).DgName), DataGridView).Rows.Count - 1
                        With CType(Me.Controls(SelGrid(I).DgName), DataGridView)
                            If CBool(.Item(0, J).Value) Then
                                If .Tag.ToString.Trim = "" Then
                                    .Tag = "'" & .Item(1, J).Value.ToString & "'"
                                Else
                                    .Tag = .Tag + ", " + "'" & .Item(1, J).Value.ToString & "'"
                                End If

                                If TreeView1.Nodes(I).ToolTipText.Trim = "" Then
                                    TreeView1.Nodes(I).ToolTipText = "For " & TreeView1.Nodes(I).Text & " : " & .Item(2, J).Value.ToString & ""
                                Else
                                    TreeView1.Nodes(I).ToolTipText = TreeView1.Nodes(I).ToolTipText + ", " + .Item(2, J).Value.ToString
                                End If

                            End If
                        End With
                    Next

                    If CType(Me.Controls(SelGrid(I).DgName), DataGridView).Tag.ToString.Trim = "" Then
                        TreeView1.Nodes(I).Checked = True
                        TreeView1.Nodes(I).ToolTipText = "For " & TreeView1.Nodes(I).Text & " : All"
                    End If
                Else
                    TreeView1.Nodes(I).ToolTipText = "For " & TreeView1.Nodes(I).Text & " : All"
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Function GetWhereCondition(ByVal FieldName As String, ByVal Index As Integer) As String
        Dim mCond$
        If CType(Me.Controls(SelGrid(Index).DgName), DataGridView).Tag.ToString.Trim <> "" Then
            mCond = " And " & FieldName & " In (" & CType(Me.Controls(SelGrid(Index).DgName), DataGridView).Tag & ")"
        Else
            mCond = ""
        End If

        GetWhereCondition = mCond
    End Function

    Public Function GetCodeString(ByVal Index As Integer) As String
        Dim mCond$
        If Index < TreeView1.Nodes.Count Then
            If CType(Me.Controls(SelGrid(Index).DgName), DataGridView).Tag.ToString.Trim <> "" Then
                mCond = Replace(CType(Me.Controls(SelGrid(Index).DgName), DataGridView).Tag, "'", "''")
            Else
                mCond = ""
            End If
        Else
            mCond = ""
        End If

        GetCodeString = mCond
    End Function
    Public Function GetHelpString(ByVal Index As Integer) As String
        Dim mHelpString$
        If TreeView1.Nodes(Index).ToolTipText.ToString.Trim <> "" Then
            mHelpString = " " & TreeView1.Nodes(Index).ToolTipText & " "
        Else
            mHelpString = ""
        End If

        GetHelpString = mHelpString
    End Function

    Public Function FillMainStreamCode(ByVal GridIndex As Integer, ByVal ColumnIndex As Integer, ByVal FieldName As String) As String
        Dim J As Integer
        Dim mCond$ = ""
        Try

            If TreeView1.Nodes(GridIndex).Checked = False Then
                For J = 0 To CType(Me.Controls(SelGrid(GridIndex).DgName), DataGridView).Rows.Count - 1
                    With CType(Me.Controls(SelGrid(GridIndex).DgName), DataGridView)
                        If CBool(.Item(0, J).Value) Then
                            If mCond = "" Then
                                mCond = "( Left(" & FieldName & ", " & Len(.Item(ColumnIndex, J).Value.ToString) & ") = '" & .Item(ColumnIndex, J).Value.ToString & "' "
                            Else
                                mCond = mCond + " Or " + " Left(" & FieldName & ", " & Len(.Item(ColumnIndex, J).Value.ToString) & ") = '" & .Item(ColumnIndex, J).Value.ToString & "' "
                            End If

                        End If
                    End With
                Next
                mCond = IIf(mCond <> "", mCond & " ) ", "")
            End If
            If mCond <> "" Then mCond = " And " & mCond

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            FillMainStreamCode = mCond
        End Try
    End Function

#End Region

#Region "Report Common Formulas"
    Public Sub Formula_Set(ByVal mCRD As ReportDocument, Optional ByVal mRepTitle As String = "", Optional ByVal Date1 As String = "", Optional ByVal Date2 As String = "")
        Dim bCountHelpDG As Integer = TreeView1.Nodes.Count
        Dim i As Integer
        For i = 0 To mCRD.DataDefinition.FormulaFields.Count - 1
            Select Case AgL.UTrim(mCRD.DataDefinition.FormulaFields(i).Name)
                Case AgL.UTrim("comp_name")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.PubCompName & "'"
                Case AgL.UTrim("comp_add")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.PubCompAdd1 & "'"
                Case AgL.UTrim("comp_add1")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.PubCompAdd2 & "'"
                Case AgL.UTrim("comp_Pin")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.PubCompPinCode & "'"
                Case AgL.UTrim("comp_phone")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.PubCompPhone & "'"
                Case AgL.UTrim("comp_city")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.PubCompCity & "'"
                Case AgL.UTrim("Title")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & mRepTitle & "'"
                Case AgL.UTrim("Division")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.PubDivName & " DIVISION" & "'"
                Case AgL.UTrim("Tin_No")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & "TIN NO : " & AgL.PubCompTIN & "'"
                Case AgL.UTrim("DateBetween")
                    If Date1 <> "" And Date2 <> "" Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & "From Date " & Date1 & " To " & Date2 & " '"
                    ElseIf Date1 <> "" And Date2 = "" Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "' " & "For Date : " & Date1 & " '"
                    End If
                Case AgL.UTrim("Site_Name")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & "Branch Name : " & AgL.PubSiteName & " { " & AgL.PubSiteManualCode & " } '"
                Case AgL.UTrim("FormulaStr1")
                    If bCountHelpDG > 0 Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & GetHelpString(0) & " '"
                    End If
                Case AgL.UTrim("FormulaStr2")
                    If bCountHelpDG > 1 Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & GetHelpString(1) & " '"
                    End If
                Case AgL.UTrim("FormulaStr3")
                    If bCountHelpDG > 2 Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & GetHelpString(2) & " '"
                    End If
                Case AgL.UTrim("FormulaStr4")
                    If bCountHelpDG > 3 Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & GetHelpString(3) & " '"
                    End If
                Case AgL.UTrim("FormulaStr5")
                    If bCountHelpDG > 4 Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & GetHelpString(4) & " '"
                    End If
                Case AgL.UTrim("FormulaStr6")
                    If bCountHelpDG > 5 Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & GetHelpString(5) & " '"
                    End If
                Case AgL.UTrim("FormulaStr7")
                    If bCountHelpDG > 6 Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & GetHelpString(6) & " '"
                    End If
                Case AgL.UTrim("FormulaStr8")
                    If bCountHelpDG > 7 Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & GetHelpString(7) & " '"
                    End If
                Case AgL.UTrim("FormulaStr9")
                    If bCountHelpDG > 8 Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & GetHelpString(8) & " '"
                    End If
                Case AgL.UTrim("FormulaStr10")
                    If bCountHelpDG > 9 Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & GetHelpString(9) & " '"
                    End If
                Case AgL.UTrim("FormulaStr11")
                    If bCountHelpDG > 10 Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & GetHelpString(10) & " '"
                    End If
                Case AgL.UTrim("FormulaStr12")
                    If bCountHelpDG > 11 Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & GetHelpString(11) & " '"
                    End If
                Case AgL.UTrim("List1")
                    If Cmbo1.Visible = True Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & LblCmbo1.Text & " : " & Cmbo1.Text & " '"
                    End If
                Case AgL.UTrim("List2")
                    If Cmbo2.Visible = True Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & LblCmbo2.Text & " : " & Cmbo2.Text & " '"
                    End If
                Case AgL.UTrim("List3")
                    If Cmbo3.Visible = True Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & LblCmbo3.Text & " : " & Cmbo3.Text & " '"
                    End If
                Case AgL.UTrim("List4")
                    If Cmbo4.Visible = True Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & LblCmbo4.Text & " : " & Cmbo4.Text & " '"
                    End If
                Case AgL.UTrim("List5")
                    If Cmbo5.Visible = True Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & LblCmbo5.Text & " : " & Cmbo5.Text & " '"
                    End If

            End Select
        Next
		Formula_SetSubRep(mCRD, mRepTitle, Date1, Date2)
    End Sub
    
    Public Sub Formula_SetSubRep(ByVal mCRD As ReportDocument, Optional ByVal mRepTitle As String = "", Optional ByVal Date1 As String = "", Optional ByVal Date2 As String = "")
        Dim bCountHelpDG As Integer = TreeView1.Nodes.Count
        Dim i As Integer

        Try

            With mCRD.OpenSubreport("ReportCommonFormula.rpt").DataDefinition
                For i = 0 To .FormulaFields.Count - 1
                    Select Case AgL.UTrim(.FormulaFields(i).Name)
                        Case AgL.UTrim("DateBetween")
                            If Date1 <> "" And Date2 <> "" Then
                                .FormulaFields(i).Text = "'" & "From Date " & Date1 & " To " & Date2 & " '"
                            ElseIf Date1 <> "" And Date2 = "" Then
                                .FormulaFields(i).Text = "' " & "For Date : " & Date1 & " '"
                            End If
                        Case AgL.UTrim("Site_Name")
                            .FormulaFields(i).Text = "'" & "Branch Name : " & AgL.PubSiteName & " { " & AgL.PubSiteManualCode & " } '"
                        Case AgL.UTrim("FormulaStr1")
                            If bCountHelpDG > 0 Then
                                .FormulaFields(i).Text = "'" & GetHelpString(0) & " '"
                            End If
                        Case AgL.UTrim("FormulaStr2")
                            If bCountHelpDG > 1 Then
                                .FormulaFields(i).Text = "'" & GetHelpString(1) & " '"
                            End If
                        Case AgL.UTrim("FormulaStr3")
                            If bCountHelpDG > 2 Then
                                .FormulaFields(i).Text = "'" & GetHelpString(2) & " '"
                            End If
                        Case AgL.UTrim("FormulaStr4")
                            If bCountHelpDG > 3 Then
                                .FormulaFields(i).Text = "'" & GetHelpString(3) & " '"
                            End If
                        Case AgL.UTrim("FormulaStr5")
                            If bCountHelpDG > 4 Then
                                .FormulaFields(i).Text = "'" & GetHelpString(4) & " '"
                            End If
                        Case AgL.UTrim("FormulaStr6")
                            If bCountHelpDG > 5 Then
                                .FormulaFields(i).Text = "'" & GetHelpString(5) & " '"
                            End If
                        Case AgL.UTrim("FormulaStr7")
                            If bCountHelpDG > 6 Then
                                .FormulaFields(i).Text = "'" & GetHelpString(6) & " '"
                            End If
                        Case AgL.UTrim("FormulaStr8")
                            If bCountHelpDG > 7 Then
                                .FormulaFields(i).Text = "'" & GetHelpString(7) & " '"
                            End If
                        Case AgL.UTrim("FormulaStr9")
                            If bCountHelpDG > 8 Then
                                .FormulaFields(i).Text = "'" & GetHelpString(8) & " '"
                            End If
                        Case AgL.UTrim("FormulaStr10")
                            If bCountHelpDG > 9 Then
                                .FormulaFields(i).Text = "'" & GetHelpString(9) & " '"
                            End If
                        Case AgL.UTrim("FormulaStr11")
                            If bCountHelpDG > 10 Then
                                .FormulaFields(i).Text = "'" & GetHelpString(10) & " '"
                            End If
                        Case AgL.UTrim("FormulaStr12")
                            If bCountHelpDG > 11 Then
                                .FormulaFields(i).Text = "'" & GetHelpString(11) & " '"
                            End If
                        Case AgL.UTrim("List1")
                            If Cmbo1.Visible = True Then
                                .FormulaFields(i).Text = "'" & LblCmbo1.Text & " : " & Cmbo1.Text & " '"
                            End If
                        Case AgL.UTrim("List2")
                            If Cmbo2.Visible = True Then
                                .FormulaFields(i).Text = "'" & LblCmbo2.Text & " : " & Cmbo2.Text & " '"
                            End If
                        Case AgL.UTrim("List3")
                            If Cmbo3.Visible = True Then
                                .FormulaFields(i).Text = "'" & LblCmbo3.Text & " : " & Cmbo3.Text & " '"
                            End If
                        Case AgL.UTrim("List4")
                            If Cmbo4.Visible = True Then
                                .FormulaFields(i).Text = "'" & LblCmbo4.Text & " : " & Cmbo4.Text & " '"
                            End If
                        Case AgL.UTrim("List5")
                            If Cmbo5.Visible = True Then
                                .FormulaFields(i).Text = "'" & LblCmbo5.Text & " : " & Cmbo5.Text & " '"
                            End If
                    End Select

                Next
            End With
        Catch ex As Exception

        End Try
    End Sub
#End Region

    Public Event ProcessReport()

    Public Property ParameterDate1_Text() As String
        Get
            ParameterDate1_Text = LblDate1.Text
        End Get
        Set(ByVal value As String)
            LblDate1.Text = value
        End Set
    End Property

    Public Property ParameterDate2_Text() As String
        Get
            ParameterDate2_Text = LblDate2.Text
        End Get
        Set(ByVal value As String)
            LblDate2.Text = value
        End Set
    End Property

    Public Property ParameterCmbo1_Text() As String
        Get
            ParameterCmbo1_Text = LblCmbo1.Text
        End Get
        Set(ByVal value As String)
            LblCmbo1.Text = value
        End Set
    End Property

    Public Property ParameterCmbo2_Text() As String
        Get
            ParameterCmbo2_Text = LblCmbo2.Text
        End Get
        Set(ByVal value As String)
            LblCmbo2.Text = value
        End Set
    End Property

    Public Property ParameterCmbo3_Text() As String
        Get
            ParameterCmbo3_Text = LblCmbo3.Text
        End Get
        Set(ByVal value As String)
            LblCmbo3.Text = value
        End Set
    End Property

    Public Property ParameterCmbo4_Text() As String
        Get
            ParameterCmbo4_Text = LblCmbo4.Text
        End Get
        Set(ByVal value As String)
            LblCmbo4.Text = value
        End Set
    End Property

    Public Property ParameterCmbo5_Text() As String
        Get
            ParameterCmbo5_Text = LblCmbo5.Text
        End Get
        Set(ByVal value As String)
            LblCmbo5.Text = value
        End Set
    End Property

    Public Property ParameterDate1_Value() As String
        Get
            ParameterDate1_Value = Date1.Text
        End Get
        Set(ByVal value As String)
            Date1.Text = value
        End Set
    End Property

    Public Property ParameterDate2_Value() As String
        Get
            ParameterDate2_Value = Date2.Text
        End Get
        Set(ByVal value As String)
            Date2.Text = value
        End Set
    End Property

    Public Property ParameterCmbo1_Value() As String
        Get
            ParameterCmbo1_Value = Cmbo1.Text
        End Get
        Set(ByVal value As String)
            Cmbo1.Text = value
        End Set
    End Property

    Public Property ParameterCmbo2_Value() As String
        Get
            ParameterCmbo2_Value = Cmbo2.Text
        End Get
        Set(ByVal value As String)
            Cmbo2.Text = value
        End Set
    End Property

    Public Property ParameterCmbo3_Value() As String
        Get
            ParameterCmbo3_Value = Cmbo3.Text
        End Get
        Set(ByVal value As String)
            Cmbo3.Text = value
        End Set
    End Property

    Public Property ParameterCmbo4_Value() As String
        Get
            ParameterCmbo4_Value = Cmbo4.Text
        End Get
        Set(ByVal value As String)
            Cmbo4.Text = value
        End Set
    End Property

    Public Property ParameterCmbo5_Value() As String
        Get
            ParameterCmbo5_Value = Cmbo5.Text
        End Get
        Set(ByVal value As String)
            Cmbo5.Text = value
        End Set
    End Property

    '************
    Public Property ParameterCmbo1_AgSelectedValue() As String
        Get
            ParameterCmbo1_AgSelectedValue = Cmbo1.SelectedValue
        End Get
        Set(ByVal value As String)
            Cmbo1.SelectedValue = value
        End Set
    End Property

    Public Property ParameterCmbo2_AgSelectedValue() As String
        Get
            ParameterCmbo2_AgSelectedValue = Cmbo2.SelectedValue
        End Get
        Set(ByVal value As String)
            Cmbo2.SelectedValue = value
        End Set
    End Property

    Public Property ParameterCmbo3_AgSelectedValue() As String
        Get
            ParameterCmbo3_AgSelectedValue = Cmbo3.SelectedValue
        End Get
        Set(ByVal value As String)
            Cmbo3.SelectedValue = value
        End Set
    End Property

    Public Property ParameterCmbo4_AgSelectedValue() As String
        Get
            ParameterCmbo4_AgSelectedValue = Cmbo4.SelectedValue
        End Get
        Set(ByVal value As String)
            Cmbo4.SelectedValue = value
        End Set
    End Property

    Public Property ParameterCmbo5_AgSelectedValue() As String
        Get
            ParameterCmbo5_AgSelectedValue = Cmbo5.SelectedValue
        End Get
        Set(ByVal value As String)
            Cmbo5.SelectedValue = value
        End Set
    End Property

    '==========************
    Public Property Date1_Enabled() As Boolean
        Get
            Date1_Enabled = Date1.Enabled
        End Get
        Set(ByVal value As Boolean)
            Date1.Enabled = value
        End Set
    End Property

    Public Property Date2_Enabled() As Boolean
        Get
            Date2_Enabled = Date2.Enabled
        End Get
        Set(ByVal value As Boolean)
            Date2.Enabled = value
        End Set
    End Property

    Public Property Cmbo1_Enabled() As Boolean
        Get
            Cmbo1_Enabled = Cmbo1.Enabled
        End Get
        Set(ByVal value As Boolean)
            Cmbo1.Enabled = value
        End Set
    End Property

    Public Property Cmbo2_Enabled() As Boolean
        Get
            Cmbo2_Enabled = Cmbo2.Enabled
        End Get
        Set(ByVal value As Boolean)
            Cmbo2.Enabled = value
        End Set
    End Property

    Public Property Cmbo3_Enabled() As Boolean
        Get
            Cmbo3_Enabled = Cmbo3.Enabled
        End Get
        Set(ByVal value As Boolean)
            Cmbo3.Enabled = value
        End Set
    End Property

    Public Property Cmbo4_Enabled() As Boolean
        Get
            Cmbo4_Enabled = Cmbo4.Enabled
        End Get
        Set(ByVal value As Boolean)
            Cmbo4.Enabled = value
        End Set
    End Property

    Public Property Cmbo5_Enabled() As Boolean
        Get
            Cmbo5_Enabled = Cmbo5.Enabled
        End Get
        Set(ByVal value As Boolean)
            Cmbo5.Enabled = value
        End Set
    End Property
    '************

    Public Property SubReport1DataSet() As DataSet
        Get
            SubReport1DataSet = DsRep1
        End Get
        Set(ByVal value As DataSet)
            DsRep1 = value
            If value IsNot Nothing Then
                SubRep1 = True
            Else
                SubRep1 = False
            End If
        End Set
    End Property

    Public Property SubReport2DataSet() As DataSet
        Get
            SubReport2DataSet = DsRep2
        End Get
        Set(ByVal value As DataSet)
            DsRep2 = value
            If value IsNot Nothing Then
                SubRep2 = True
            Else
                SubRep2 = False
            End If
        End Set
    End Property

    Public Property IsXSD_Report() As Boolean
        Get
            IsXSD_Report = XSD_Rep
        End Get
        Set(ByVal value As Boolean)
            XSD_Rep = value
        End Set
    End Property

    Public Property IsPrintHandler_Report() As Boolean
        Get
            IsPrintHandler_Report = PrnHnd_Rep
        End Get
        Set(ByVal value As Boolean)
            PrnHnd_Rep = value
        End Set
    End Property

    Public Property IsGrid_Report(ByVal Report_Query As String) As Boolean
        Get
            IsGrid_Report = Grid_Rep
            Report_Query = mQry
        End Get
        Set(ByVal value As Boolean)
            Grid_Rep = value
            mQry = Report_Query
        End Set
    End Property

    Public Property IsMessageBox_Report() As Boolean
        Get
            IsMessageBox_Report = MessageBox
        End Get
        Set(ByVal value As Boolean)
            MessageBox = value
        End Set
    End Property

    Public Property ShowReportAutomatically() As Boolean
        Get
            ShowReportAutomatically = ShowReportAuto
        End Get
        Set(ByVal value As Boolean)
            ShowReportAuto = value
        End Set
    End Property

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click, BtnExit.Click
        Dim mCrd As New ReportDocument
        Dim ReportView As New AgLibrary.RepView
        Dim FrmObj As Form = Nothing

        Try

            Select Case sender.name
                Case BtnExit.Name
                    Me.Dispose()
                Case BtnPrint.Name
                    Call RemoveHelpGridFilter()
                    RaiseEvent ProcessReport()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            XSD_Rep = False : PrnHnd_Rep = False : ShowReportAuto = True
            MessageBox = False
        End Try
    End Sub

    Public Sub PrintReport(ByVal DsRep As DataSet, ByVal RepName As String, ByVal RepTitle As String, Optional ByVal ReportPath As String = "")
        Dim mRepNameCustomize As String = ""
        Dim mCrd As New ReportDocument
        Dim ReportView As New AgLibrary.RepView
        Dim FrmObj As Form = Nothing

        Try
            Me.Cursor = Cursors.WaitCursor

            If MessageBox = True Then Exit Sub

            If ReportPath.Trim = "" Then ReportPath = AgL.PubReportPath

            If Grid_Rep = True Then
                If DsRep Is Nothing Then
                    FrmObj = AgRep.CFOpen.FOpen("MnuLoadReport", RepTitle, False, mQry)
                End If
            Else
                If DsRep IsNot Nothing Then
                    If PrnHnd_Rep = True Then
                        mPrnHnd.DataSetToPrint = DsRep
                        mPrnHnd.LineThreshold = DsRep.Tables(0).Rows.Count - 1
                        mPrnHnd.NumberOfColumns = DsRep.Tables(0).Columns.Count - 1
                        mPrnHnd.ReportTitle = RepTitle
                        mPrnHnd.TableIndex = 0
                        mPrnHnd.PageSetupDialog(True)
                        mPrnHnd.PrintPreview()
                    ElseIf XSD_Rep = True Then      ''.xsd File used for Rpt. as Data Definition
                        AgPL.Generate_Report(mQry, AgL.GCn, mCrd, ReportView, ReportPath, RepName, RepTitle, Me, Me.MdiParent, ShowReportAuto)
                        If ShowReportAuto = False Then
                            Formula_Set(mCrd, RepTitle)
                            AgPL.Show_Report(ReportView, RepTitle, Me.MdiParent)
                        End If
                    Else                            ''.ttx File used for Rpt. as Data Definition
                        AgPL.CreateFieldDefFile1(DsRep, ReportPath & "\" & RepName & ".ttx", True)
                        ''''''''''IF CUSTOMER NEED SOME CHANGE IN FORMAT OF A REPORT'''''''''''
                        ''''''''''CUTOMIZE REPORT CAN BE CREATED WITHOUT CHANGE IN CODE''''''''
                        ''''''''''WITH ADDING 6 CHAR OF COMPANY NAME AND 4 CHAR OF CITY NAME'''
                        ''''''''''WITHOUT SPACES IN EXISTING REPORT NAME''''''''''''''''''''''''''''''''''''''
                        RepName = AgPL.GetRepNameCustomize(RepName, ReportPath)
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''


                        mCrd.Load(ReportPath & "\" & RepName & ".rpt")

                        If SubRep1 = True Then AgPL.CreateFieldDefFile1(DsRep1, ReportPath & "\" & RepName & "1.ttx", True)
                        If SubRep2 = True Then AgPL.CreateFieldDefFile1(DsRep2, ReportPath & "\" & RepName & "2.ttx", True)

                        mCrd.SetDataSource(DsRep.Tables(0))
						AgPL.ReportCommonInformation(AgL, mCrd, ReportPath)

                        If SubRep1 = True Then mCrd.OpenSubreport("SUBREP1").Database.Tables(0).SetDataSource(DsRep1.Tables(0))
                        If SubRep2 = True Then mCrd.OpenSubreport("SUBREP2").Database.Tables(0).SetDataSource(DsRep2.Tables(0))

                        CType(ReportView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
                        Formula_Set(mCrd, RepTitle, Date1.Text, Date2.Text)
                        AgPL.Show_Report(ReportView, "* " & RepTitle & " *", Me.MdiParent)
                    End If
                End If
            End If

            ''#####<<Show Grid Report Form>>#####
            If FrmObj IsNot Nothing Then
                CType(FrmObj, AgReports.FrmReportTool).AllowUserReports = True
                FrmObj.MdiParent = Me.MdiParent
                FrmObj.Show()
                FrmObj = Nothing
            End If
            ''#####=========================#####

            Call AgL.LogTableEntry("Report", Me.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            XSD_Rep = False : PrnHnd_Rep = False : ShowReportAuto = True
            MessageBox = False
        End Try
    End Sub

    Public Function IsRequiredField(ByVal ControlName As AgLibrary.ClsMain.ReportFormGlobalControls, Optional ByVal DispText As String = "It", Optional ByVal NumField As Boolean = False) As Boolean
        Select Case ControlName
            Case AgLibrary.ClsMain.ReportFormGlobalControls.Date1_Control
                IsRequiredField = AgL.RequiredField(Date1, DispText, NumField)
            Case AgLibrary.ClsMain.ReportFormGlobalControls.Date2_Control
                IsRequiredField = AgL.RequiredField(Date2, DispText, NumField)
            Case AgLibrary.ClsMain.ReportFormGlobalControls.Cmbo1_Control
                IsRequiredField = AgL.RequiredField(Cmbo1, DispText, NumField)
            Case AgLibrary.ClsMain.ReportFormGlobalControls.Cmbo2_Control
                IsRequiredField = AgL.RequiredField(Cmbo2, DispText, NumField)
            Case AgLibrary.ClsMain.ReportFormGlobalControls.Cmbo3_Control
                IsRequiredField = AgL.RequiredField(Cmbo3, DispText, NumField)
            Case AgLibrary.ClsMain.ReportFormGlobalControls.Cmbo4_Control
                IsRequiredField = AgL.RequiredField(Cmbo4, DispText, NumField)
            Case AgLibrary.ClsMain.ReportFormGlobalControls.Cmbo5_Control
                IsRequiredField = AgL.RequiredField(Cmbo5, DispText, NumField)
            Case Else
                IsRequiredField = False
        End Select
    End Function


#Region "City Report"
    Private Sub ProcCityList()
        Try
            Call FillGridString()

            Dim mCondStr As String = ""

            If CType(Me.Controls(SelGrid(0).DgName), DataGridView).Tag.ToString.Trim <> "" Then
                mCondStr = mCondStr + " And City.CityCode In (" & CType(Me.Controls(SelGrid(0).DgName), DataGridView).Tag & ")"
            End If
            If CType(Me.Controls(SelGrid(1).DgName), DataGridView).Tag.ToString.Trim <> "" Then
                mCondStr = mCondStr + " And City.State_Code In (" & CType(Me.Controls(SelGrid(1).DgName), DataGridView).Tag & ")"
            End If

            mQry = "Select City.CityCode,City.CityName, State.State_Desc,'" & TreeView1.Nodes(0).ToolTipText & "' As ForCity,'" & TreeView1.Nodes(1).ToolTipText & "' ForState " & _
                    " From City Left Join State On City.State_Code=State.State_Code " & _
                    " Where 1=1 " & mCondStr
            DsRep = AgL.FillData(mQry, AgL.GCn)

            RepName = "CityList" : RepTitle = "City List"
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "User Wise Entry Report"
    Private Sub ProcUserWiseEntryReport()
        Try

            Call FillGridString()

            Dim mCondStr As String = ""


            If AgL.RequiredField(Date1) Then Exit Sub
            If AgL.RequiredField(Date2) Then Exit Sub
            mCondStr = mCondStr & " And CONVERT(SMALLDATETIME,REPLACE(CONVERT(VARCHAR, L.U_EntDt,106),' ','/')) Between " & AgL.ConvertDate(Date1) & " And " & AgL.ConvertDate(Date2) & " "

            mCondStr = mCondStr & GetWhereCondition("L.U_Name", 0)
            mCondStr = mCondStr & GetWhereCondition("L.EntryPoint", 1)

            mQry = "Select L.U_Name, L.EntryPoint, Count(Case When U_AE='A' Then 1 End) As [Add], Count(Case When U_AE='E' Then 1 End) As [Edit], " & _
                   "Count(Case When U_AE='D' Then 1 End) As [Delete], Count(Case When U_AE='P' Then 1 End) As [Print], 0 As Email, 0 As Sms, 0 As Fax, " & _
                   "'" & TreeView1.Nodes(0).ToolTipText & "' As SelGrid1, '" & TreeView1.Nodes(1).ToolTipText & "' As SelGrid2 " & _
                   "From LogTable L "

            mQry = mQry & " Where 1=1  " & mCondStr
            mQry = mQry & " Group By L.U_Name, L.EntryPoint "
            DsRep = AgL.FillData(mQry, AgL.GCn)
            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")
            RepName = "UserWiseEntryReport" : RepTitle = "User Wise Entry Report"
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "User Wise Target Entry Detail"
    Private Sub ProcUserWiseEntryTargetReport()
        Try

            Call FillGridString()

            Dim mCondStr As String = ""
            Dim mDays As Long = 0

            If AgL.RequiredField(Date1) Then Exit Sub
            If AgL.RequiredField(Date2) Then Exit Sub

            mDays = DateDiff(DateInterval.Day, CDate(Date1.Text), CDate(Date2.Text))
            mCondStr = mCondStr & " And CONVERT(SMALLDATETIME,REPLACE(CONVERT(VARCHAR, LogTable.U_EntDt,106),' ','/')) Between " & AgL.ConvertDate(Date1) & " And " & AgL.ConvertDate(Date2) & " "
            mCondStr = mCondStr & " And ((Ut.Date_to >= " & AgL.ConvertDate(Date1) & " And Ut.Date_to <= " & AgL.ConvertDate(Date2) & ") Or Ut.Date_to Is Null ) "



            mCondStr = mCondStr & GetWhereCondition("LogTable.U_Name", 0)
            mCondStr = mCondStr & GetWhereCondition("LogTable.EntryPoint", 1)

            If AgL.StrCmp(Cmbo1.Text, "Summary") Then
                mCondStr = mCondStr & " GROUP BY LogTable.U_Name ,LogTable.EntryPoint "

                mQry = " SELECT LogTable.EntryPoint,Max(convert(NVARCHAR,LogTable.U_EntDt,3)) as U_Entdt,LogTable.U_Name," & _
                       " sum(CASE WHEN LogTable.u_ae='A' THEN 1 ELSE 0 END) AS ActualAdd, " & _
                       " sum(CASE WHEN LogTable.u_ae='E' THEN 1 ELSE 0 END) AS ActualEdit, " & _
                       " sum(CASE WHEN LogTable.u_ae='D' THEN 1 ELSE 0 END) AS Actualdel, " & _
                       " sum(CASE WHEN LogTable.u_ae='P' THEN 1 ELSE 0 END) AS ActualPrint, " & _
                       " sum(CASE WHEN LogTable.u_ae='F' THEN 1 ELSE 0 END) AS ActualFax, " & _
                       " sum(CASE WHEN LogTable.u_ae='S' THEN 1 ELSE 0 END) AS ActualSms, " & _
                       " sum(CASE WHEN LogTable.u_ae='M' THEN 1 ELSE 0 END) AS ActualEmail, " & _
                       " Convert(Float,max(utd.Add_Target))*" & mDays & " AS AddTarget,Convert(Float,max(utd.Edit_Target))*" & mDays & " AS Edittarget, " & _
                       " Convert(Float,max(utd.Print_Target))*" & mDays & " AS printtarget,Convert(Float,max(utd.Fax_Target))*" & mDays & " AS faxtarget,Convert(Float,max(utd.Email_Target))*" & mDays & " AS Emailtarget, " & _
                       " Convert(Float,max(utd.Sms_Target))*" & mDays & " AS smstarget,'" & TreeView1.Nodes(0).ToolTipText & "' As SelGrid1, '" & TreeView1.Nodes(1).ToolTipText & "' As SelGrid2  " & _
                       " FROM LogTable  LEFT JOIN User_Target ut ON LogTable.U_Name=ut.UserName " & _
                       " LEFT JOIN User_Target_Detail utd ON ut.Code=utd.Code "

                mQry = mQry & " Where LogTable.U_Name <>''  " & mCondStr


                mQry = "Select EntryPoint, U_EntDt, U_Name, ActualAdd, ActualEdit, ActualDel, ActualPrint, ActualFax, " & _
                       "ActualSms, ActualEmail, AddTarget, EditTarget, PrintTarget, FaxTarget, EmailTarget, " & _
                       "SmsTarget, (Case When AddTarget<>0 then (ActualAdd/AddTarget)*100 End) As AddPer, " & _
                       "(Case When EditTarget<>0 then (ActualEdit/EditTarget)*100 End) As EditPer, " & _
                       "(Case When PrintTarget<>0 then (ActualPrint/PrintTarget) End) As PrintPer, SelGrid1, SelGrid2  " & _
                       "From (" & mQry & ") As X "

                DsRep = AgL.FillData(mQry, AgL.GCn)
                If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")


                RepName = "UserWiseEntryTargetReportSummary"
                RepTitle = "User Wise Target Entry Summary"
            Else

                mCondStr = mCondStr & " GROUP BY LogTable.U_Name ,LogTable.EntryPoint,convert(NVARCHAR,LogTable.U_EntDt,3) "

                mQry = " SELECT LogTable.EntryPoint,convert(NVARCHAR,LogTable.U_EntDt,3) as U_Entdt,LogTable.U_Name," & _
                       " sum(CASE WHEN LogTable.u_ae='A' THEN 1 ELSE 0 END) AS ActualAdd, " & _
                       " sum(CASE WHEN LogTable.u_ae='E' THEN 1 ELSE 0 END) AS ActualEdit, " & _
                       " sum(CASE WHEN LogTable.u_ae='D' THEN 1 ELSE 0 END) AS Actualdel, " & _
                       " sum(CASE WHEN LogTable.u_ae='P' THEN 1 ELSE 0 END) AS ActualPrint, " & _
                       " sum(CASE WHEN LogTable.u_ae='F' THEN 1 ELSE 0 END) AS ActualFax, " & _
                       " sum(CASE WHEN LogTable.u_ae='S' THEN 1 ELSE 0 END) AS ActualSms, " & _
                       " sum(CASE WHEN LogTable.u_ae='M' THEN 1 ELSE 0 END) AS ActualEmail, " & _
                       " Convert(Float,max(utd.Add_Target)) AS AddTarget,Convert(Float,max(utd.Edit_Target)) AS Edittarget, " & _
                       " Convert(Float,max(utd.Print_Target)) AS printtarget,Convert(Float,max(utd.Fax_Target)) AS faxtarget,Convert(Float,max(utd.Email_Target)) AS Emailtarget, " & _
                       " Convert(Float,max(utd.Sms_Target)) AS smstarget,'" & TreeView1.Nodes(0).ToolTipText & "' As SelGrid1, '" & TreeView1.Nodes(1).ToolTipText & "' As SelGrid2  " & _
                       " FROM LogTable  LEFT JOIN User_Target ut ON LogTable.U_Name=ut.UserName " & _
                       " LEFT JOIN User_Target_Detail utd ON ut.Code=utd.Code  "

                mQry = mQry & " Where LogTable.U_Name <>''  " & mCondStr


                mQry = "Select EntryPoint, U_EntDt, U_Name, ActualAdd, ActualEdit, ActualDel, ActualPrint, ActualFax, " & _
                       "ActualSms, ActualEmail, AddTarget, EditTarget, PrintTarget, FaxTarget, EmailTarget, " & _
                       "SmsTarget, (Case When AddTarget<>0 then (ActualAdd/AddTarget)*100 End) As AddPer, " & _
                       "(Case When EditTarget<>0 then (ActualEdit/EditTarget)*100 End) As EditPer, " & _
                       "(Case When PrintTarget<>0 then (ActualPrint/PrintTarget) End) As PrintPer, SelGrid1, SelGrid2  " & _
                       "From (" & mQry & ") As X "

                DsRep = AgL.FillData(mQry, AgL.GCn)
                If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

                RepName = "UserWiseEntryTargetReport"
                RepTitle = "User Wise Target Entry Detail"
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Class Initialization"
    Public Sub New(ByVal AgLibVar As ClsMain)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AgL = AgLibVar
        AgPL = New AgLibrary.ClsPrinting(AgL)
        'AgRep = New AgReports.ClsMain(AgL)
        mPrnHnd = New PrintHandler(AgL)
    End Sub

    Public Sub New(ByVal AgLibVar As ClsMain, ByVal AgRepVar As AgReports.ClsMain)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AgL = AgLibVar
        AgPL = New AgLibrary.ClsPrinting(AgL)
        AgRep = AgRepVar
        mPrnHnd = New PrintHandler(AgL)
    End Sub
#End Region

    Private Sub CMnu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
                                            CMnuSelectAll.Click, CMnuUnselectAll.Click

        Try
            If TreeView1.SelectedNode Is Nothing Then Exit Sub

            Select Case sender.name
                Case CMnuSelectAll.Name
                    If TreeView1.SelectedNode.Checked = False Then
                        Call ProcSelectHelpDg(CType(Me.Controls(SelGrid(TreeView1.SelectedNode.Tag).DgName), DataGridView), DgHelpCMenu.SelectAll, SelGrid(TreeView1.SelectedNode.Tag).DataTableName)
                    End If
                Case CMnuUnselectAll.Name
                    If TreeView1.SelectedNode.Checked = False Then
                        Call ProcSelectHelpDg(CType(Me.Controls(SelGrid(TreeView1.SelectedNode.Tag).DgName), DataGridView), DgHelpCMenu.UnselectAll, SelGrid(TreeView1.SelectedNode.Tag).DataTableName)
                    End If
            End Select

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
