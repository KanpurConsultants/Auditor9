Imports Customised.ClsMain

Public Class FrmStudentPromotion
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Dim mSearchCode$ = ""
    Public DtV_TypeSettings As DataTable
    Protected Const Col1Select As String = "Tick"
    Public Const ColSNo As String = "S.No."
    Public Const Col1Student As String = "Student"

    Public WithEvents DglMain As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1HeadOriginal As String = "Head Original"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"

    Public Const rowPromotionDate As Integer = 0
    Public Const rowFromClass As Integer = 1
    Public Const rowFromSection As Integer = 2
    Public Const rowToClass As Integer = 3
    Public Const rowToSection As Integer = 4
    Public Const rowBtnFill As Integer = 5

    Public Const hcPromotionDate As String = "Promotion Date"
    Public Const hcFromClass As String = "From Class"
    Public Const hcFromSection As String = "From Section"
    Public Const hcToClass As String = "To Class"
    Public Const hcToSection As String = "To Section"
    Public Const HcBtnOrderBalance As String = "Fill"

    Dim mQry As String = ""

    Public Property SearchCode() As String
        Get
            SearchCode = mSearchCode
        End Get
        Set(ByVal value As String)
            mSearchCode = value
        End Set
    End Property
    Public Sub Ini_Grid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 60, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Select, 60, 0, Col1Select, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Student, 400, 0, Col1Student, True, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgL.GridDesign(Dgl1)
        Dgl1.Name = "Dgl1"
        AgL.FSetDimensionCaptionForHorizontalGrid(Dgl1, AgL)

        Dgl1.AllowUserToAddRows = False
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.Columns(Col1Select).DefaultCellStyle.Font = New Font(New FontFamily("wingdings"), 14)

        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)

        DglMain.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DglMain, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(DglMain, Col1Head, 360, 255, Col1Head, True, True)
            .AddAgTextColumn(DglMain, Col1HeadOriginal, 260, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(DglMain, Col1Mandatory, 10, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(DglMain, Col1Value, 480, 255, Col1Value, True, False)
        End With
        AgL.AddAgDataGrid(DglMain, PnlMain)
        DglMain.EnableHeadersVisualStyles = False
        DglMain.ColumnHeadersHeight = 35
        DglMain.AgSkipReadOnlyColumns = True
        DglMain.AllowUserToAddRows = False
        DglMain.Name = "DglMain"
        AgL.GridDesign(DglMain)
        DglMain.Tag = "VerticalGrid"

        DglMain.Rows.Add(6)
        DglMain.Item(Col1Head, rowPromotionDate).Value = hcPromotionDate
        DglMain.Item(Col1Head, rowFromClass).Value = hcFromClass
        DglMain.Item(Col1Head, rowFromSection).Value = hcFromSection
        DglMain.Item(Col1Head, rowToClass).Value = hcToClass
        DglMain.Item(Col1Head, rowToSection).Value = hcToSection
        DglMain.Item(Col1Head, rowBtnFill).Value = HcBtnOrderBalance
        DglMain.Item(Col1Value, rowBtnFill) = New DataGridViewButtonCell
    End Sub
    Private Sub FrmReconcile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ini_Grid()
        MovRec()
    End Sub
    Private Sub MovRec()
        Dim mQry As String = ""

        mQry = "Select Sg.SubCode, Sg.Name As StudentName
                From SubgroupAdmission H
                LEFT JOIN SubGroup Sg On H.SubCode = Sg.SubCode
                Where H.Class = '" & DglMain.Item(Col1Value, rowFromClass).Tag & "'
                And H.Section = '" & DglMain.Item(Col1Value, rowFromSection).Tag & "'
                And PromotionDate Is Null
                And Sg.Name Is Not Null
                Order By Sg.Name "
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        With DtTemp
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I As Integer = 0 To .Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                    Dgl1.Item(Col1Select, I).Value = "þ"
                    Dgl1.Item(Col1Student, I).Tag = AgL.XNull(.Rows(I)("SubCode"))
                    Dgl1.Item(Col1Student, I).Value = AgL.XNull(.Rows(I)("StudentName"))
                Next I
            End If
        End With

        Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
    End Sub
    Private Sub FrmImportPurchaseFromExcel_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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

    Private Sub Dgl1_MouseUp(sender As Object, e As MouseEventArgs) Handles Dgl1.MouseUp
        Dim mRowIndex As Integer = Dgl1.CurrentCell.RowIndex
        Dim mColumnIndex As Integer = Dgl1.CurrentCell.ColumnIndex
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Select
                    If e.Button = Windows.Forms.MouseButtons.Left Then
                        If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1Select).Index Then
                            ClsMain.FManageTick(Dgl1, Dgl1.CurrentCell.ColumnIndex, Dgl1.Columns(Col1Student).Index)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Private Sub Dgl1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        Dim mRowIndex As Integer = Dgl1.CurrentCell.RowIndex
        Dim mColumnIndex As Integer = Dgl1.CurrentCell.ColumnIndex
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Select
                    If e.KeyCode = Keys.Space Then
                        ClsMain.FManageTick(Dgl1, Dgl1.CurrentCell.ColumnIndex, Dgl1.Columns(Col1Student).Index)
                    End If
            End Select
        Catch ex As Exception
            MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Private Sub FSave()
        Dim mTrans As String = ""
        Dim mSr As Integer = 0
        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            For I As Integer = 0 To Dgl1.Rows.Count - 1
                If Dgl1.Item(Col1Select, I).Value = "þ" Then
                    mQry = " UPDATE SubGroupAdmission Set PromotionDate = " & AgL.Chk_Date(DglMain.Item(Col1Value, rowPromotionDate).Value) & "
                            Where SubCode = '" & Dgl1.Item(Col1Student, I).Tag & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mSr = AgL.VNull(AgL.Dman_Execute(" Select Max(Sr) From SubgroupAdmission 
                        Where SubCode = '" & Dgl1.Item(Col1Student, I).Tag & "'", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar())

                    mSr += 1
                    mQry = "Insert Into SubgroupAdmission(Subcode, Sr, Comp_Code, Div_Code, Site_Code, 
                        AdmissionDate, Class, Section)
                        Values ('" & Dgl1.Item(Col1Student, I).Tag & "', " & mSr & ", " & AgL.Chk_Text(AgL.PubCompCode) & ", 
                        " & AgL.Chk_Text(AgL.PubDivCode) & ", " & AgL.Chk_Text(AgL.PubSiteCode) & ", 
                        " & AgL.Chk_Date(DglMain.Item(Col1Value, rowPromotionDate).Value) & ", 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowToClass).Tag) & ", 
                        " & AgL.Chk_Text(DglMain.Item(Col1Value, rowToSection).Tag) & "
                        ) "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    Dim mNarration As String = ""
                    mNarration = Dgl1.Item(Col1Student, I).Value & " Promoted From Class " & DglMain.Item(Col1Value, rowFromClass).Value & " " & DglMain.Item(Col1Value, rowFromSection).Value & " To " & DglMain.Item(Col1Value, rowToClass).Value & " " & DglMain.Item(Col1Value, rowToSection).Value
                    Call AgL.LogTableEntry(Dgl1.Item(Col1Student, I).Tag, Me.Text, "A", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd, mNarration)
                End If
            Next
            AgL.ETrans.Commit()
            mTrans = "Commit"
            MsgBox("Process Completed Successfully...!", MsgBoxStyle.Information)
            FBlankText()

        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub
    Private Sub DglMain_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DglMain.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim bNewMasterCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = DglMain.CurrentCell.RowIndex
            bColumnIndex = DglMain.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> DglMain.Columns(Col1Value).Index Then Exit Sub

            Select Case DglMain.CurrentCell.RowIndex
                Case rowFromClass, rowToClass
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " SELECT Sg.SubCode AS Code, Sg.Name
                                FROM Subgroup Sg With (NoLock)
                                Where Sg.SubgroupType = '" & ClsSchool.SubGroupType_Class & "' 
                                And IfNull(Sg.Status,'Active') = 'Active'"
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If

                Case rowFromSection, rowToSection
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = " SELECT Sg.SubCode AS Code, Sg.Name
                                FROM Subgroup Sg With (NoLock)
                                Where Sg.SubgroupType = '" & ClsSchool.SubGroupType_Section & "' 
                                And IfNull(Sg.Status,'Active') = 'Active'"
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub DglMain_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DglMain.CellEnter
        Try
            Dim mRow As Integer = DglMain.CurrentCell.RowIndex
            Dim mCol As Integer = DglMain.CurrentCell.ColumnIndex

            If DglMain.CurrentCell Is Nothing Then Exit Sub
            If DglMain.CurrentCell.ColumnIndex <> DglMain.Columns(Col1Value).Index Then Exit Sub
            DglMain.AgHelpDataSet(DglMain.CurrentCell.ColumnIndex) = Nothing
            CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0

            If DglMain.CurrentCell.ColumnIndex = DglMain.Columns(Col1Value).Index Then
                DglMain.AgHelpDataSet(DglMain.CurrentCell.ColumnIndex) = Nothing
                Select Case DglMain.CurrentCell.RowIndex
                    Case rowPromotionDate
                        CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
                End Select
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub DglMain_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DglMain.CellContentClick
        Select Case e.RowIndex
            Case rowBtnFill
                MovRec()
        End Select
    End Sub
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click, BtnCancel.Click
        Select Case sender.Name
            Case BtnSave.Name
                FSave()
            Case BtnCancel.Name
                Me.Close()
        End Select
    End Sub
    Private Sub FBlankText()
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        For I As Integer = 0 To DglMain.Rows.Count - 1
            If DglMain.Item(Col1Head, I).Value <> "Promotion Date" Then
                DglMain.Item(Col1Value, I).Tag = ""
                DglMain.Item(Col1Value, I).Value = ""
            End If
        Next
    End Sub
End Class