Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain.ConfigurableFields

Public Class FrmPersonTemporaryCreditLimit
    Inherits AgTemplate.TempMaster

    Dim mQry$
    Public Const ColSNo As String = "SNo"
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1HeadOriginal As String = "Head Original"



    Dim rowParty As Integer = 0
    Dim rowFromDate As Integer = 1
    Dim rowToDate As Integer = 2
    Dim rowAmount As Integer = 3
    Dim rowCreditLimit As Integer = 4
    Dim rowResponsiblePerson As Integer = 5
    Dim rowRemark As Integer = 6


    Public Const HcParty As String = "Party Name"
    Public Const HcFromDate As String = "From Date"
    Public Const HcToDate As String = "To Date"
    Public Const HcAmount As String = "Amount"
    Public Const HcCreditLimit As String = "Credit Limit"
    Public Const HcResponsiblePerson As String = "Responsible Person"
    Public Const HcRemark As String = "Remark"



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
        Me.Topctrl1.Size = New System.Drawing.Size(865, 41)
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 445)
        Me.GroupBox1.Size = New System.Drawing.Size(907, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(14, 449)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(200, 510)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(228, 449)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(401, 449)
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
        Me.GroupBox2.Location = New System.Drawing.Point(704, 449)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(465, 449)
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
        Me.Pnl1.Location = New System.Drawing.Point(86, 56)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(688, 369)
        Me.Pnl1.TabIndex = 1
        '
        'FrmPersonTemporaryCreditLimit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(865, 493)
        Me.Controls.Add(Me.Pnl1)
        Me.Name = "FrmPersonTemporaryCreditLimit"
        Me.Text = "Quality Master"
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


        For I = 0 To Dgl1.RowCount - 1
            If Dgl1(Col1Mandatory, I).Value <> "" And Dgl1.Rows(I).Visible Then
                If Dgl1(Col1Value, I).Value.ToString = "" Then
                    MsgBox(Dgl1(Col1Head, I).Value & " can not be blank.")
                    Dgl1.CurrentCell = Dgl1(Col1Value, I)
                    Dgl1.Focus()
                    Exit Sub
                End If
            End If
        Next


        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From SubgroupTemporaryCreditLimit Where Subcode='" & Dgl1(Col1Value, rowParty).Tag & "' And FromDate = " & AgL.Chk_Date(Dgl1(Col1Value, rowFromDate).Value) & "  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
                If MsgBox("Already increased limit today, Do you want to continue?") = vbNo Then
                    passed = False
                    Exit Sub
                End If
            End If
        End If


        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1(Col1Value, I).Value = Nothing Then Dgl1(Col1Value, I).Value = ""
            If Dgl1(Col1Value, I).Tag = Nothing Then Dgl1(Col1Value, I).Tag = ""
        Next
    End Sub

    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        AgL.PubFindQry = "SELECT I.Code, Party.Name, I.FromDate, I.ToDate, I.Amount, Employee.Name as PassedBy, I.Remark
                        FROM SubgroupTemporaryCreditLimit I  
                        Left Join viewHelpSubgroup Party On I.Subcode = Party.Code
                        Left Join viewHelpSubgroup Employee On I.ResponsiblePerson = Employee.Code "
        AgL.PubFindQryOrdBy = "[Description]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "SubgroupTemporaryCreditLimit"
        MainLineTableCsv = ""
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans

        mQry = "UPDATE SubgroupTemporaryCreditLimit 
                Set 
                Subcode = " & AgL.Chk_Text(Dgl1(Col1Value, rowParty).Tag) & ",                 
                FromDate = " & AgL.Chk_Date(Dgl1(Col1Value, rowFromDate).Value) & ",                 
                ToDate = " & AgL.Chk_Date(Dgl1(Col1Value, rowFromDate).Value) & ",                 
                Amount = " & Val(Dgl1(Col1Value, rowAmount).Value) & ",                
                CreditLimit = " & Val(Dgl1(Col1Value, rowCreditLimit).Value) & ",                
                ResponsiblePerson = " & AgL.Chk_Text(Dgl1(Col1Value, rowResponsiblePerson).Tag) & ",                                                                
                Remark = " & AgL.Chk_Text(Dgl1(Col1Value, rowRemark).Value) & "
                Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList


    End Sub
    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = "Select H.*, Party.Name as PartyName, Employee.Name as EmployeeName  " &
            " From SubgroupTemporaryCreditLimit H " &
            " Left Join viewHelpSubgroup Party On H.Subcode = Party.Code " &
            " Left Join viewHelpSubgroup Employee On H.ResponsiblePerson = Employee.Code " &
            " Where H.Code='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))


                Dgl1(Col1Value, rowParty).Tag = AgL.XNull(.Rows(0)("Subcode"))
                Dgl1(Col1Value, rowParty).Value = AgL.XNull(.Rows(0)("PartyName"))
                Dgl1(Col1Value, rowFromDate).Value = ClsMain.FormatDate(AgL.XNull(.Rows(0)("FromDate")))
                Dgl1(Col1Value, rowToDate).Value = ClsMain.FormatDate(AgL.XNull(.Rows(0)("ToDate")))
                Dgl1(Col1Value, rowAmount).Value = AgL.VNull(.Rows(0)("Amount"))
                Dgl1(Col1Value, rowCreditLimit).Value = AgL.VNull(.Rows(0)("CreditLimit"))
                Dgl1(Col1Value, rowResponsiblePerson).Tag = AgL.XNull(.Rows(0)("ResponsiblePerson"))
                Dgl1(Col1Value, rowResponsiblePerson).Value = AgL.XNull(.Rows(0)("EmployeeName"))
                Dgl1(Col1Value, rowRemark).Value = AgL.XNull(.Rows(0)("Remark"))
            End If
        End With

        ApplyUISetting()
    End Sub

    'Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
    '    TxtDescription.Focus()
    'End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        Dgl1.CurrentCell = Dgl1(Col1Value, rowParty)
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
                " From SubgroupTemporaryCreditLimit I " &
                " Order By I.FromDate "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dim DtTemp As DataTable = Nothing
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.NAME
                'Case TxtItemCategory.Name
                'If TxtItemCategory.Visible = True Then
                '    If TxtItemCategory.AgSelectedValue <> "" Then
                '        TxtItemType.AgSelectedValue = AgL.FillData("Select ItemType From ItemCategory Where Code = '" & TxtItemCategory.AgSelectedValue & "' ", AgL.GCn).tables(0).rows(0)(0)
                '        'If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, "Save") = MsgBoxResult.Yes Then
                '        '    Topctrl1.FButtonClick(13)
                '        'End If
                '    End If
                'End If

                'Case TxtItemType.Name
                '    FGetItemTypeSetting()

                'mQry = "Select * From ItemTypeSetting Where ItemType = '" & TxtItemType.Tag & "' And Div_Code = '" & TxtDivision.Tag & "' "
                'DtItemTypeSetting = AgL.FillData(mQry, AgL.GCn).tables(0)
                'If DtItemTypeSetting.Rows.Count = 0 Then
                '    mQry = "Select * From ItemTypeSetting Where ItemType = '" & TxtItemType.Tag & "' And Div_Code Is Null "
                '    DtItemTypeSetting = AgL.FillData(mQry, AgL.GCn).tables(0)
                '    If DtItemTypeSetting.Rows.Count = 0 Then
                '        mQry = "Select * From ItemTypeSetting Where ItemType Is Null And Div_Code Is Null "
                '        DtItemTypeSetting = AgL.FillData(mQry, AgL.GCn).tables(0)
                '        If DtItemTypeSetting.Rows.Count = 0 Then
                '            MsgBox("Settings not found for selected Item Type.")
                '            sender.text = ""
                '            sender.tag = ""
                '        End If
                '    End If
                'End If



                'If TxtItemType.Visible = True Then
                '    If TxtItemType.AgSelectedValue <> "" Then
                '        If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, "Save") = MsgBoxResult.Yes Then
                '            Topctrl1.FButtonClick(13)
                '        End If
                '    End If
                'End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub






    Private Sub FrmPersonTemporaryCreditLimit_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        ApplyUISetting()

        Dgl1(Col1Value, rowFromDate).Value = AgL.PubLoginDate

        If Dgl1.Visible = True Then
            Dgl1.CurrentCell = Dgl1.FirstDisplayedCell
            Dgl1.CurrentCell = Dgl1(Col1Value, Dgl1.CurrentCell.RowIndex)
            Dgl1.Focus()
        End If
    End Sub

    Private Sub FrmPersonTemporaryCreditLimit_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dim I As Integer

        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Head, 180, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl1, Col1HeadOriginal, 180, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(Dgl1, Col1Mandatory, 12, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(Dgl1, Col1Value, 470, 255, Col1Value, True, False)
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
        AgL.GridDesign(Dgl1)


        Dgl1.Rows.Add(7)



        Dgl1(Col1Head, rowResponsiblePerson).Value = HcResponsiblePerson
        Dgl1(Col1Head, rowToDate).Value = HcToDate
        Dgl1(Col1Head, rowFromDate).Value = HcFromDate
        Dgl1(Col1Head, rowAmount).Value = HcAmount
        Dgl1(Col1Head, rowCreditLimit).Value = HcCreditLimit
        Dgl1(Col1Head, rowParty).Value = HcParty
        Dgl1(Col1Head, rowRemark).Value = HcRemark

        ApplyUISetting()
    End Sub





    Private Sub ApplyUISetting()
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
                    Where EntryName='" & Me.Name & "' And GridName ='Dgl1' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 0 To Dgl1.Rows.Count - 1
                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = Dgl1(Col1Head, J).Value Then
                            Dgl1.Rows(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
                            If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDgl1RowCount += 1
                            Dgl1(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
                            If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
                                Dgl1(Col1Head, J).Value = AgL.XNull(DtTemp.Rows(I)("Caption"))
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
                Case rowAmount
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberLeftPlaces = 8
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberRightPlaces = 0
                Case rowFromDate, rowToDate
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
                Case rowCreditLimit
                    Dgl1.Item(Col1Value, rowCreditLimit).ReadOnly = True
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> Dgl1.Columns(Col1Value).Index Then Exit Sub

            Select Case Dgl1.CurrentCell.RowIndex

                Case rowResponsiblePerson
                    If Dgl1(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT H.Code, H.Name From viewHelpSubgroup H Where H.SubgroupType ='" & SubgroupType.Employee & "' "
                        mQry += " Order By Name"

                        Dgl1(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If


                Case rowParty
                    If Dgl1(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT H.Code, H.Name 
                                From viewHelpSubgroup H 
                                LEFT JOIN SubGroupType Sgt On H.SubGroupType = Sgt.SubGroupType
                                Where IfNull(Sgt.Parent,Sgt.SubgroupType) ='" & SubgroupType.Customer & "' "
                        mQry += " Order By Name"

                        Dgl1(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
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
            If Dgl1(Col1Mandatory, mRow).Value <> "" Then
                If Dgl1(Col1Value, mRow).Value = "" Then
                    MsgBox(Dgl1(Col1Head, mRow).Value & " can not be blank.")
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        End If

        mRow = Dgl1.CurrentCell.RowIndex
        mColumn = Dgl1.CurrentCell.ColumnIndex

        Select Case mRow
            Case rowParty
                If AgL.XNull(Dgl1.Item(Col1Value, rowParty).Tag) <> "" Then
                    Dgl1.Item(Col1Value, rowCreditLimit).Value = AgL.VNull(AgL.Dman_Execute(" Select CreditLimit From SubGroup 
                        Where SubCode = '" & Dgl1.Item(Col1Value, rowParty).Tag & "'", AgL.GCn).ExecuteScalar())
                Else
                    Dgl1.Item(Col1Value, rowCreditLimit).Value = 0
                End If
        End Select
    End Sub

    Private Sub FrmPersonTemporaryCreditLimit_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dim i As Integer

        For i = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1Value, i).Value = ""
            Dgl1(Col1Value, i).Tag = ""
        Next
    End Sub

    Private Sub FrmPersonTemporaryCreditLimit_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Dim i As Integer

        For i = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1Head, i).Tag = Nothing
        Next
    End Sub
End Class
