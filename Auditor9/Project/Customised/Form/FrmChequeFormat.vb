Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain.ConfigurableFields

Public Class FrmChequeFormat

    Inherits AgTemplate.TempMaster

    Dim mQry$
    Public Const ColSNo As String = "SNo"


    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1LastValue As String = "Last Value"
    Public Const Col1HeadOriginal As String = "Head Original"


    Dim rowDescription As Integer = 0
    Dim rowDateFormat As Integer = 1
    Dim rowDateSpacing As Integer = 2
    Dim rowAccountPayeeYN As Integer = 3
    Dim rowFormat As Integer = 4

    Public Const hcDescription As String = "Description"
    Public Const hcDateFormat As String = "Date Format"
    Public Const hcDateSpacing As String = "Date Spacing"
    Public Const hcAccountPayeeYN As String = "Account Payee YN"
    Public Const hcFormat As String = "Format"


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
        'FrmChequeFormat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(974, 519)
        Me.Controls.Add(Me.Pnl1)
        Me.MaximizeBox = True
        Me.Name = "FrmChequeFormat"
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
        Dgl1.EndEdit()

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1(Col1Mandatory, I).Value <> "" And Dgl1.Rows(I).Visible Then
                If Dgl1(Col1Value, I).Value.ToString = "" Then
                    MsgBox(Dgl1(Col1Head, I).Value & " can not be blank.")
                    Dgl1.CurrentCell = Dgl1(Col1Value, I)
                    Dgl1.Focus()
                    passed = False
                    Exit Sub
                End If
            End If
        Next

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From ChequeFormat Where Description='" & Dgl1.Item(Col1Value, rowDescription).Value & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        Else
            mQry = "Select count(*) From ChequeFormat Where Description='" & Dgl1.Item(Col1Value, rowDescription).Value & "' And Code<>'" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If


        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Value, I).Value = Nothing Then Dgl1.Item(Col1Value, I).Value = ""
            If Dgl1.Item(Col1Value, I).Tag = Nothing Then Dgl1.Item(Col1Value, I).Tag = ""
        Next

        SetLastValues()
    End Sub

    Private Sub SetLastValues()
        Dim I As Integer
        For I = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1LastValue, I).Value = Dgl1(Col1Value, I).Value
            Dgl1(Col1LastValue, I).Tag = Dgl1(Col1Value, I).Tag
        Next
    End Sub


    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        AgL.PubFindQry = "SELECT I.Code As SearchCode, I.Description  
                        FROM ChequeFormat I  "
        AgL.PubFindQryOrdBy = "[Description]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "ChequeFormat"
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans

        mQry = "UPDATE ChequeFormat 
                Set 
                Description = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDescription).Value) & ", 
                DateFormat = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDateFormat).Value) & ", 
                DateSpacing = " & Val(Dgl1.Item(Col1Value, rowDateSpacing).Value) & ", 
                AccountPayeeYn = " & IIf(Dgl1.Item(Col1Value, rowAccountPayeeYN).Value.ToUpper = "NO", 0, 1) & ", 
                Format = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowFormat).Value) & "                
                Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList


    End Sub
    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = "Select H.*  " &
            " From ChequeFormat H " &
            " Where H.Code='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))


                Dgl1.Item(Col1Value, rowDescription).Value = AgL.XNull(.Rows(0)("Description"))
                Dgl1.Item(Col1Value, rowDateFormat).Value = AgL.XNull(.Rows(0)("DateFormat"))
                Dgl1.Item(Col1Value, rowDateSpacing).Value = AgL.VNull(.Rows(0)("DateSpacing"))
                Dgl1.Item(Col1Value, rowAccountPayeeYN).Value = IIf((.Rows(0)("AccountPayeeYn")), "Yes", "No")
                Dgl1.Item(Col1Value, rowFormat).Value = AgL.XNull(.Rows(0)("Format"))

            End If
        End With




        SetLastValues()
        Frm_BaseFunction_DispText()
    End Sub

    'Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
    '    TxtDescription.Focus()
    'End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        Dgl1.CurrentCell = Dgl1(Col1Value, rowDescription)
        Dgl1.Focus()
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
                " From ChequeFormat I " &
                " Order By I.Description "
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
            'mQry = " Select Count(*) From ChequeFormat Where Code = '" & mSearchCode & "'"
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
        Dgl1.CurrentCell = Dgl1(Col1Value, rowDescription) 'Dgl1.FirstDisplayedCell
        Dgl1.Focus()
    End Sub

    Private Sub Frm_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dim I As Integer

        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Head, 125, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl1, Col1HeadOriginal, 180, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(Dgl1, Col1Mandatory, 12, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(Dgl1, Col1Value, 815, 255, Col1Value, True, False)
            .AddAgTextColumn(Dgl1, Col1LastValue, 300, 255, Col1LastValue, False, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.Columns(Col1Mandatory).DefaultCellStyle.Font = New System.Drawing.Font("Wingdings 2", 5.25, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Dgl1.Columns(Col1Mandatory).DefaultCellStyle.ForeColor = Color.Red
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToAddRows = False
        Dgl1.RowHeadersVisible = False
        Dgl1.ColumnHeadersVisible = False
        Dgl1.BackgroundColor = Me.BackColor
        AgL.GridDesign(Dgl1)
        Dgl1.Anchor = AnchorStyles.Top + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom



        Dgl1.Rows.Add(5)

        Dgl1.Item(Col1Head, rowDescription).Value = hcDescription
        Dgl1.Item(Col1Head, rowDateFormat).Value = hcDateFormat
        Dgl1.Item(Col1Head, rowDateSpacing).Value = hcDateSpacing
        Dgl1.Item(Col1Head, rowAccountPayeeYN).Value = hcAccountPayeeYN
        Dgl1.Item(Col1Head, rowFormat).Value = hcFormat

        Dgl1.Rows(rowFormat).Height = 400
        Dgl1(Col1Value, rowFormat).Style.Alignment = DataGridViewContentAlignment.TopLeft
        Dgl1(Col1Value, rowFormat).Style.WrapMode = DataGridViewTriState.True



        For I = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1HeadOriginal, I).Value = Dgl1(Col1Head, I).Value
        Next
    End Sub

    Private Sub ApplyItemTypeSetting(ItemType As String)
        Dim mQry As String
        Dim DtTemp As DataTable
        Dim I As Integer, J As Integer
        Dim mDgl1RowCount As Integer

        Try

            For I = 0 To Dgl1.Rows.Count - 1
                Dgl1.Rows(I).Visible = False
            Next


            mQry = "Select H.*
                    from EntryHeaderUISetting H                   
                    Where EntryName='FrmChequeFormat' And NCat = '" & ItemType & "' And GridName ='Dgl1' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 0 To Dgl1.Rows.Count - 1
                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = Dgl1.Item(Col1HeadOriginal, J).Value Then
                            Dgl1.Rows(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
                            If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDgl1RowCount += 1
                            Dgl1.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
                            If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
                                Dgl1.Item(Col1Head, J).Value = AgL.XNull(DtTemp.Rows(I)("Caption"))
                            End If
                            'MsgBox(NameOf(rowAdditionalDiscountPatternPurchase))
                        End If
                    Next
                Next
            End If
            If mDgl1RowCount = 0 Then Dgl1.Visible = False Else Dgl1.Visible = True

        Catch ex As Exception
            MsgBox(ex.Message & " [ApplySubgroupTypeSetting]")
        End Try
    End Sub


    Private Sub Frm_BaseFunction_DispText() Handles Me.BaseFunction_DispText

    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try

            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            If Topctrl1.Mode = "BROWSE" Then
                Dgl1.CurrentCell.ReadOnly = True
            End If

            If Dgl1.CurrentCell.ColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub


            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
            CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).MaxInputLength = 0
            CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = False

            Select Case Dgl1.CurrentCell.RowIndex
                Case rowDateSpacing
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberLeftPlaces = 1
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberRightPlaces = 0
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberNegetiveAllow = False
            End Select
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
                Case rowDescription
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "Select Code, Description As Name " &
                                " From ChequeFormat " &
                                " Order By Description"
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If
                    CType(Dgl1.CurrentCell.OwningColumn, AgControls.AgTextColumn).AgMasterHelp = True

                Case rowDateFormat
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT 'DDMMYYYY' as Code, 'DDMMYYYY' as Name "
                            mQry += " UNION ALL "
                            mQry += "SELECT 'DD-MMM-YYYY' as Code, 'DD-MMM-YYYY' as Name "

                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If



            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Dim mRow As Integer
        Dim mColumn As Integer
        mRow = Dgl1.CurrentCell.RowIndex
        mColumn = Dgl1.CurrentCell.ColumnIndex
        If mColumn = Dgl1.Columns(Col1Value).Index Then
            If Dgl1.Item(Col1Mandatory, mRow).Value <> "" Then
                If Dgl1(Col1Value, mRow).Value = "" Then
                    MsgBox(Dgl1(Col1Head, mRow).Value & " can not be blank.")
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

        For i = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1Value, i).Value = ""
            Dgl1(Col1Value, i).Tag = ""
        Next

    End Sub

    Private Sub Frm_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Dim i As Integer

        For i = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1Head, i).Tag = Nothing
        Next
    End Sub

    Private Sub Frm_BaseEvent_ApproveDeletion_InTrans(SearchCode As String, Conn As Object, Cmd As Object) Handles Me.BaseEvent_ApproveDeletion_InTrans
    End Sub

    Private Sub Dgl1_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles Dgl1.CellBeginEdit
        Dim mRow As Integer
        mRow = Dgl1.CurrentCell.RowIndex
        If Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name = Col1Value Then
            Select Case mRow
                Case rowAccountPayeeYN
                    e.Cancel = True
            End Select
        End If
    End Sub

    Private Sub Dgl1_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl1.KeyDown
        Dim mRow As Integer
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        If ClsMain.IsSpecialKeyPressed(e) Then Exit Sub
        mRow = Dgl1.CurrentCell.RowIndex

        If Topctrl1.Mode.ToUpper <> "BROWSE" Then
            If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1Value).Index Then
                If e.KeyCode = Keys.Delete Then
                    Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Value = ""
                    Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex).Tag = ""
                End If

                Select Case Dgl1.CurrentCell.RowIndex
                    Case rowAccountPayeeYN
                        If Not ClsMain.IsSpecialKeyPressed(e) Then
                            If e.KeyCode = Keys.N Then
                                Dgl1.Item(Col1Value, mRow).Value = "NO"
                            Else
                                Dgl1.Item(Col1Value, mRow).Value = "YES"
                            End If
                        End If
                End Select
            End If
        End If
    End Sub


    Public Shared Sub FSeedTable()
        Dim mFormat As String

        mFormat = "


                                                                                                                                                                      <CHQ_DATE>




               <Party_Name>




                      <AMOUNT_IN_WORDS>




                                                                                                                                                                  *** <AMOUNT>


.
"
        FSeedSingleIfNotExist("YES", "YES", mFormat, "DDMMYYYY", 3, True, AgL.PubDivCode, "SYSTEM DEFINED")



        mFormat = "


                                                                                                                                                                      <CHQ_DATE>




           <Party_Name>




                <AMOUNT_IN_WORDS>




                                                                                                                                                                  *** <AMOUNT>


.
"
        FSeedSingleIfNotExist("ICICI", "ICICI", mFormat, "DDMMYYYY", 3, True, AgL.PubDivCode, "SYSTEM DEFINED")
    End Sub


    Public Shared Function FSeedSingleIfNotExist(Code As String, Description As String, Format As String, DateFormat As String, DateSpacing As Integer, AccountPayeeYn As Boolean, Div_Code As String, LockText As String) As String
        Try
            Dim dtTemp As DataTable
            Dim mQry As String
            Dim mMaxId As String = Code
            Dim mFoundRecord As Boolean = True


            If Code = "" Then mMaxId = AgL.GetMaxId("ChequeFormat", "Code", AgL.GcnMain, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

            If Code <> "" Then
                If AgL.FillData("Select Code from ChequeFormat Where Code='" & Code & "'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                    mFoundRecord = False
                End If
            Else
                dtTemp = AgL.FillData("Select Code from ChequeFormat Where Description='" & Description & "'", AgL.GcnMain).tables(0)
                If dtTemp.Rows.Count > 0 Then
                    Code = dtTemp.Rows(0)("Code")
                Else
                    mFoundRecord = False
                End If
            End If
            If mFoundRecord = False Then
                mQry = "
                    INSERT INTO ChequeFormat (Code,  Description, Format, DateFormat, DateSpacing, AccountPayeeYn, EntryBy, EntryDate, Div_Code, LockText)
                    VALUES ('" & mMaxId & "', " & AgL.Chk_Text(Description) & ", " & AgL.Chk_Text(Format) & ", " & AgL.Chk_Text(DateFormat) & ", " & Val(DateSpacing) & ",  " & IIf(AccountPayeeYn, 1, 0) & ",  " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Date(AgL.PubLoginDate) & ", " & AgL.Chk_Text(Div_Code) & ", " & AgL.Chk_Text(LockText) & ")
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
            FSeedSingleIfNotExist = Code
        Catch ex As Exception
            MsgBox(ex.Message & " In FSeedSingleIfNotExist OF FrmChequeFormat For " & Description)

        End Try
    End Function

End Class
