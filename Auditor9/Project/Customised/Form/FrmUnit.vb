Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain.ConfigurableFields

Public Class FrmUnit

    Inherits AgTemplate.TempMaster

    Dim mQry$
    Public Const ColSNo As String = "SNo"


    Public WithEvents DglMain As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1LastValue As String = "Last Value"
    Public Const Col1HeadOriginal As String = "Head Original"


    Dim rowCode As Integer = 0
    Dim rowDecimalPlaces As Integer = 1
    Dim rowShowDimensionDetailInPurchase As Integer = 2
    Dim rowShowDimensionDetailInSales As Integer = 3
    Dim rowUQC As Integer = 4
    Dim rowSymbol As Integer = 5
    Dim rowFractionName As Integer = 6
    Dim rowFractionUnits As Integer = 7
    Dim rowFractionSymbol As Integer = 8

    Public Const hcCode As String = "Code"
    Public Const hcDecimalPlaces As String = "Decimal Places"
    Public Const hcShowDimensionDetailInPurchase As String = "Show Dimension Detail In Purchase"
    Public Const hcShowDimensionDetailInSales As String = "Show Dimension Detail In Sales"
    Public Const hcUQC As String = "UQC"
    Public Const hcSymbol As String = "Symbol"
    Public Const hcFractionName As String = "Fraction Name"
    Public Const hcFractionUnits As String = "Fraction Units"
    Public Const hcFractionSymbol As String = "Fraction Symbol"


    Friend WithEvents Pnl1 As Panel
#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.Pnl1 = New System.Windows.Forms.Panel()
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
        Me.Topctrl1.Size = New System.Drawing.Size(974, 41)
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 471)
        Me.GroupBox1.Size = New System.Drawing.Size(1016, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(14, 475)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(200, 536)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(228, 475)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(401, 475)
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
        Me.GroupBox2.Location = New System.Drawing.Point(704, 475)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(465, 475)
        Me.GBoxDivision.Size = New System.Drawing.Size(136, 44)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Size = New System.Drawing.Size(130, 18)
        Me.TxtDivision.Tag = ""
        '
        'TxtStatus
        '
        Me.TxtStatus.AgSelectedValue = ""
        Me.TxtStatus.Tag = ""
        '
        'Pnl1
        '
        Me.Pnl1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl1.Location = New System.Drawing.Point(0, 43)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(972, 426)
        Me.Pnl1.TabIndex = 1
        '
        'FrmUnit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(974, 519)
        Me.Controls.Add(Me.Pnl1)
        Me.MaximizeBox = True
        Me.Name = "FrmUnit"
        Me.Text = "Cheque Format"
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
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

    End Sub
#End Region

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer
        DglMain.EndEdit()

        For I = 0 To DglMain.RowCount - 1
            If DglMain(Col1Mandatory, I).Value <> "" And DglMain.Rows(I).Visible Then
                If DglMain(Col1Value, I).Value.ToString = "" Then
                    MsgBox(DglMain(Col1Head, I).Value & " can not be blank.")
                    DglMain.CurrentCell = DglMain(Col1Value, I)
                    DglMain.Focus()
                    passed = False
                    Exit Sub
                End If
            End If
        Next

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From Unit Where Code='" & DglMain.Item(Col1Value, rowCode).Value & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        Else
            mQry = "Select count(*) From Unit Where Code='" & DglMain.Item(Col1Value, rowCode).Value & "' And Code<>'" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If


        For I = 0 To DglMain.Rows.Count - 1
            If DglMain.Item(Col1Value, I).Value = Nothing Then DglMain.Item(Col1Value, I).Value = ""
            If DglMain.Item(Col1Value, I).Tag = Nothing Then DglMain.Item(Col1Value, I).Tag = ""
        Next

    End Sub
    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        AgL.PubFindQry = "SELECT I.Code As SearchCode, I.Code As Description  
                        FROM Unit I  "
        AgL.PubFindQryOrdBy = "[Description]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Unit"
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans

        mQry = "UPDATE Unit 
                Set 
                Code = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowCode).Value) & ", 
                DecimalPlaces = " & Val(DglMain.Item(Col1Value, rowDecimalPlaces).Value) & ", 
                ShowDimensionDetailInPurchase = " & IIf(DglMain.Item(Col1Value, rowShowDimensionDetailInPurchase).Value.ToUpper = "NO", 0, 1) & ",
                ShowDimensionDetailInSales = " & IIf(DglMain.Item(Col1Value, rowShowDimensionDetailInSales).Value.ToUpper = "NO", 0, 1) & ",
                UQC = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowUQC).Value) & ", 
                Symbol = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowSymbol).Value) & ", 
                IsActive = 1,
                FractionName = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowFractionName).Value) & ", 
                FractionUnits = " & Val(DglMain.Item(Col1Value, rowFractionUnits).Value) & ",
                FractionSymbol = " & AgL.Chk_Text(DglMain.Item(Col1Value, rowFractionSymbol).Value) & "                                
                Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub
    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = "Select H.*  " &
            " From Unit H " &
            " Where H.Code='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                DglMain.Item(Col1Value, rowCode).Value = AgL.XNull(.Rows(0)("Code"))
                DglMain.Item(Col1Value, rowDecimalPlaces).Value = AgL.VNull(.Rows(0)("DecimalPlaces"))
                DglMain.Item(Col1Value, rowShowDimensionDetailInSales).Value = IIf((.Rows(0)("ShowDimensionDetailInSales")), "Yes", "No")
                DglMain.Item(Col1Value, rowShowDimensionDetailInPurchase).Value = IIf((.Rows(0)("ShowDimensionDetailInPurchase")), "Yes", "No")
                DglMain.Item(Col1Value, rowUQC).Value = AgL.XNull(.Rows(0)("UQC"))
                DglMain.Item(Col1Value, rowSymbol).Value = AgL.XNull(.Rows(0)("Symbol"))
                DglMain.Item(Col1Value, rowFractionName).Value = AgL.XNull(.Rows(0)("FractionName"))
                DglMain.Item(Col1Value, rowFractionUnits).Value = AgL.VNull(.Rows(0)("FractionUnits"))
                DglMain.Item(Col1Value, rowFractionSymbol).Value = AgL.XNull(.Rows(0)("FractionSymbol"))
            End If
        End With

    End Sub
    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        DglMain.CurrentCell = DglMain(Col1Value, rowCode)
        DglMain.Focus()
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
                " From Unit I " &
                " Order By I.Code "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub
    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub
    Private Sub Frm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 325, 885)
    End Sub
    Private Sub TxtItemCategory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
        End If
    End Sub
    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim DtTemp As DataTable = Nothing
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.NAME


            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        'Passed = FRestrictSystemDefine()
    End Sub

    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        Passed = Not FGetRelationalData()
    End Sub

    Private Function FGetRelationalData() As Boolean
        Try
            'mQry = " Select Count(*) From Unit Where Code = '" & mSearchCode & "'"
            'If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
            '    MsgBox(" Data Exists For ItemGroup " & Dgl1(Col1Value, rowDescription).Value & " In Item Master. Can't Delete Entry", MsgBoxStyle.Information)
            '    FGetRelationalData = True
            '    Exit Function
            'End If
        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData")
            FGetRelationalData = True
        End Try
    End Function
    Private Sub Frm_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        DglMain.Item(Col1Value, rowShowDimensionDetailInPurchase).Value = "NO"
        DglMain.Item(Col1Value, rowShowDimensionDetailInSales).Value = "NO"
        ApplyUISetting()
        DglMain.CurrentCell = DglMain(Col1Value, rowCode) 'Dgl1.FirstDisplayedCell
        DglMain.Focus()
    End Sub
    Private Sub Frm_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dim I As Integer
        DglMain.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DglMain, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(DglMain, Col1Head, 300, 255, Col1Head, True, True)
            .AddAgTextColumn(DglMain, Col1HeadOriginal, 180, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(DglMain, Col1Mandatory, 12, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(DglMain, Col1Value, 640, 255, Col1Value, True, False)
            .AddAgTextColumn(DglMain, Col1LastValue, 300, 255, Col1LastValue, False, False)
        End With
        AgL.AddAgDataGrid(DglMain, Pnl1)
        DglMain.Columns(Col1Mandatory).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings 2", 5.25, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        DglMain.Columns(Col1Mandatory).DefaultCellStyle.ForeColor = Color.Red
        DglMain.EnableHeadersVisualStyles = False
        DglMain.ColumnHeadersHeight = 35
        DglMain.AgSkipReadOnlyColumns = True
        DglMain.AllowUserToAddRows = False
        DglMain.RowHeadersVisible = False
        DglMain.ColumnHeadersVisible = False
        DglMain.BackgroundColor = Me.BackColor
        AgL.GridDesign(DglMain)
        DglMain.Name = "DglMain"
        DglMain.Anchor = AnchorStyles.Top + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom



        DglMain.Rows.Add(9)

        DglMain.Item(Col1Head, rowCode).Value = hcCode
        DglMain.Item(Col1Head, rowUQC).Value = hcUQC
        DglMain.Item(Col1Head, rowDecimalPlaces).Value = hcDecimalPlaces
        DglMain.Item(Col1Head, rowShowDimensionDetailInPurchase).Value = hcShowDimensionDetailInPurchase
        DglMain.Item(Col1Head, rowShowDimensionDetailInSales).Value = hcShowDimensionDetailInSales
        DglMain.Item(Col1Head, rowSymbol).Value = hcSymbol
        DglMain.Item(Col1Head, rowFractionName).Value = hcFractionName
        DglMain.Item(Col1Head, rowFractionUnits).Value = hcFractionUnits
        DglMain.Item(Col1Head, rowFractionSymbol).Value = hcFractionSymbol

        DglMain(Col1Value, rowFractionUnits).Style.Alignment = DataGridViewContentAlignment.TopLeft
        DglMain(Col1Value, rowFractionUnits).Style.WrapMode = DataGridViewTriState.True



        For I = 0 To DglMain.Rows.Count - 1
            DglMain(Col1HeadOriginal, I).Value = DglMain(Col1Head, I).Value
        Next

        ApplyUISetting()
    End Sub
    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DglMain.CellEnter
        Try
            If DglMain.CurrentCell Is Nothing Then Exit Sub
            If Topctrl1.Mode = "BROWSE" Then
                DglMain.CurrentCell.ReadOnly = True
            End If

            If DglMain.CurrentCell.ColumnIndex <> DglMain.Columns(Col1Value).Index Then Exit Sub

            DglMain.AgHelpDataSet(DglMain.CurrentCell.ColumnIndex) = Nothing
            CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0
            CType(DglMain.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = False

            Select Case DglMain.CurrentCell.RowIndex
                Case rowFractionUnits
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgNumberLeftPlaces = 0
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgNumberRightPlaces = 5
                    CType(DglMain.Columns(Col1Value), AgControls.AgTextColumn).AgNumberNegetiveAllow = False
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DglMain.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = DglMain.CurrentCell.RowIndex
            bColumnIndex = DglMain.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> DglMain.Columns(Col1Value).Index Then Exit Sub

            Select Case DglMain.CurrentCell.RowIndex
                Case rowCode
                    If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "Select Code, Code As Name " &
                                " From Unit " &
                                " Order By Code"
                        DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                        DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                    End If
                    CType(DglMain.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True

                Case rowShowDimensionDetailInPurchase, rowShowDimensionDetailInSales
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT 'Yes' as Code, 'Yes' as Name Union All Select 'No' as Code, 'No' as Name "
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles DglMain.EditingControl_Validating
        Dim mRow As Integer
        Dim mColumn As Integer
        mRow = DglMain.CurrentCell.RowIndex
        mColumn = DglMain.CurrentCell.ColumnIndex
        If mColumn = DglMain.Columns(Col1Value).Index Then
            If DglMain.Item(Col1Mandatory, mRow).Value <> "" Then
                If DglMain(Col1Value, mRow).Value = "" Then
                    MsgBox(DglMain(Col1Head, mRow).Value & " can not be blank.")
                    e.Cancel = True
                    Exit Sub
                End If
            End If

            Select Case mRow
            End Select
        End If
    End Sub
    Private Sub Frm_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dim i As Integer
        For i = 0 To DglMain.Rows.Count - 1
            DglMain(Col1Value, i).Value = ""
            DglMain(Col1Value, i).Tag = ""
        Next
    End Sub
    Private Sub Frm_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Dim i As Integer

        For i = 0 To DglMain.Rows.Count - 1
            DglMain(Col1Head, i).Tag = Nothing
        Next
    End Sub
    Private Sub Frm_BaseEvent_ApproveDeletion_InTrans(SearchCode As String, Conn As Object, Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
    End Sub
    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles DglMain.KeyDown
        Dim mRow As Integer
        If DglMain.CurrentCell Is Nothing Then Exit Sub
        If ClsMain.IsSpecialKeyPressed(e) Then Exit Sub
        mRow = DglMain.CurrentCell.RowIndex

        If Topctrl1.Mode.ToUpper <> "BROWSE" Then
            If DglMain.CurrentCell.ColumnIndex = DglMain.Columns(Col1Value).Index Then
                If e.KeyCode = Keys.Delete Then
                    DglMain(Col1Value, DglMain.CurrentCell.RowIndex).Value = ""
                    DglMain(Col1Value, DglMain.CurrentCell.RowIndex).Tag = ""
                End If
            End If
        End If
    End Sub
    Private Sub ApplyUISetting()
        ClsMain.GetUISetting(DglMain, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", ClsMain.GridTypeConstants.VerticalGrid)
    End Sub
End Class

