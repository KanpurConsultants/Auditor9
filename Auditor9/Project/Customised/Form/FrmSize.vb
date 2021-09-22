Imports System.ComponentModel
Imports System.Data.SQLite
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain.ConfigurableFields

Public Class FrmSize
    Inherits AgTemplate.TempMaster

    Dim mQry$
    Public Const ColSNo As String = "SNo"


    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Mandatory As String = ""
    Public Const Col1Value As String = "Value"
    Public Const Col1HeadOriginal As String = "Head Original"





    Dim rowShape As Integer = 0
    Dim rowUnit As Integer = 1
    Dim rowLength As Integer = 3
    Dim rowLengthFraction As Integer = 4
    Dim rowWidth As Integer = 5
    Dim rowWidthFraction As Integer = 6
    Dim rowThickness As Integer = 7
    Dim rowThicknessFraction As Integer = 8

    Dim rowDescription As Integer = 9
    Dim rowArea As Integer = 10
    Dim rowPerimeter As Integer = 11

    Public Const hcShape As String = "Shape"
    Public Const hcUnit As String = "Unit"
    Public Const hcSize As String = "Size"
    Public Const hcLength As String = "Length"
    Public Const hcLengthFraction As String = "Length Fraction"
    Public Const hcWidth As String = "Width"
    Public Const hcWidthFraction As String = "Width Fraction"
    Public Const hcThickness As String = "Thickness"
    Public Const hcThicknessFraction As String = "Thickness Fraction"
    Public Const hcArea As String = "Area"
    Public Const hcPerimeter As String = "Perimeter"


    Friend WithEvents Pnl1 As Panel


#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.LblIsSystemDefine = New System.Windows.Forms.Label()
        Me.ChkIsSystemDefine = New System.Windows.Forms.CheckBox()
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
        'LblIsSystemDefine
        '
        Me.LblIsSystemDefine.AutoSize = True
        Me.LblIsSystemDefine.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIsSystemDefine.ForeColor = System.Drawing.Color.Red
        Me.LblIsSystemDefine.Location = New System.Drawing.Point(718, 427)
        Me.LblIsSystemDefine.Name = "LblIsSystemDefine"
        Me.LblIsSystemDefine.Size = New System.Drawing.Size(96, 15)
        Me.LblIsSystemDefine.TabIndex = 1061
        Me.LblIsSystemDefine.Text = "IsSystemDefine"
        '
        'ChkIsSystemDefine
        '
        Me.ChkIsSystemDefine.AutoSize = True
        Me.ChkIsSystemDefine.BackColor = System.Drawing.Color.Transparent
        Me.ChkIsSystemDefine.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkIsSystemDefine.ForeColor = System.Drawing.Color.Red
        Me.ChkIsSystemDefine.Location = New System.Drawing.Point(702, 429)
        Me.ChkIsSystemDefine.Name = "ChkIsSystemDefine"
        Me.ChkIsSystemDefine.Size = New System.Drawing.Size(15, 14)
        Me.ChkIsSystemDefine.TabIndex = 1060
        Me.ChkIsSystemDefine.UseVisualStyleBackColor = False
        '
        'Pnl1
        '
        Me.Pnl1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl1.Location = New System.Drawing.Point(143, 56)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(536, 383)
        Me.Pnl1.TabIndex = 1
        '
        'FrmSize
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(865, 493)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.LblIsSystemDefine)
        Me.Controls.Add(Me.ChkIsSystemDefine)
        Me.Name = "FrmSize"
        Me.Text = "Quality Master"
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.ChkIsSystemDefine, 0)
        Me.Controls.SetChildIndex(Me.LblIsSystemDefine, 0)
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
        Me.PerformLayout()

    End Sub
    Public WithEvents LblIsSystemDefine As System.Windows.Forms.Label
    Friend WithEvents ChkIsSystemDefine As System.Windows.Forms.CheckBox
#End Region

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer
        If Dgl1.Item(Col1Value, rowShape).Value = "" Then Err.Raise(1, , "Shape Is Required!")
        If Dgl1.Item(Col1Value, rowUnit).Value = "" Then Err.Raise(1, , "Unit Is Required!")

        If Val(Dgl1.Item(Col1Value, rowLength).Value) + Val(Dgl1.Item(Col1Value, rowLengthFraction).Value) = 0 Then Err.Raise(1, , "Length Is Required!")
        If Val(Dgl1.Item(Col1Value, rowWidth).Value) + Val(Dgl1.Item(Col1Value, rowWidthFraction).Value) = 0 Then Err.Raise(1, , "Width Is Required!")

        If Dgl1.Item(Col1Value, rowDescription).Value = "" Then Err.Raise(1, , "Description Is Required!")

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From Size Where Description='" & Dgl1.Item(Col1Value, rowDescription).Value & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        Else
            mQry = "Select count(*) From Size Where Description='" & Dgl1.Item(Col1Value, rowDescription).Value & "' And Code<>'" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If


        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Value, I).Value = Nothing Then Dgl1.Item(Col1Value, I).Value = ""
            If Dgl1.Item(Col1Value, I).Tag = Nothing Then Dgl1.Item(Col1Value, I).Tag = ""
        Next
    End Sub

    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        AgL.PubFindQry = "SELECT H.Code, H.Description, S.Description AS Shape, H.Unit, H.Length, H.LengthFraction, H.Width, H.WidthFraction, H.Thickness, H.ThicknessFraction, H.Area, H.Perimeter   
                            FROM Size H 
                            LEFT JOIN Shape S ON S.Code = H.Shape "
        AgL.PubFindQryOrdBy = "[Description]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Size"
    End Sub


    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As Object, ByVal Cmd As Object) Handles Me.BaseEvent_Save_InTrans
        mQry = "UPDATE Size 
                Set 
                Description = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowDescription).Value) & ", 
                Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowUnit).Tag) & ", 
                Shape = " & AgL.Chk_Text(Dgl1.Item(Col1Value, rowShape).Tag) & ", 
                Length = " & Val(Dgl1.Item(Col1Value, rowLength).Value) & ",     
                LengthFraction = " & Val(Dgl1.Item(Col1Value, rowLengthFraction).Value) & ",  
                Width = " & Val(Dgl1.Item(Col1Value, rowWidth).Value) & ",  
                WidthFraction = " & Val(Dgl1.Item(Col1Value, rowWidthFraction).Value) & ",  
                Thickness = " & Val(Dgl1.Item(Col1Value, rowThickness).Value) & ",  
                ThicknessFraction = " & Val(Dgl1.Item(Col1Value, rowThicknessFraction).Value) & ",   
                Area = " & Val(Dgl1.Item(Col1Value, rowArea).Value) & ",  
                Perimeter = " & Val(Dgl1.Item(Col1Value, rowPerimeter).Value) & "         
                Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList


    End Sub
    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = "SELECT H.*, S.Description AS ShapeName    
                FROM Size H 
                LEFT JOIN Shape S ON S.Code = H.Shape  " &
                " Where H.Code='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                FGetItemTypeSetting()

                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                Dgl1.Item(Col1Value, rowDescription).Value = AgL.XNull(.Rows(0)("Description"))
                Dgl1.Item(Col1Value, rowUnit).Tag = AgL.XNull(.Rows(0)("Unit"))
                Dgl1.Item(Col1Value, rowUnit).Value = AgL.XNull(.Rows(0)("Unit"))
                Dgl1.Item(Col1Value, rowShape).Tag = AgL.XNull(.Rows(0)("Shape"))
                Dgl1.Item(Col1Value, rowShape).Value = AgL.XNull(.Rows(0)("ShapeName"))
                Dgl1.Item(Col1Value, rowLength).Value = AgL.VNull(.Rows(0)("Length"))
                Dgl1.Item(Col1Value, rowLengthFraction).Value = AgL.VNull(.Rows(0)("LengthFraction"))
                Dgl1.Item(Col1Value, rowWidth).Value = AgL.VNull(.Rows(0)("Width"))
                Dgl1.Item(Col1Value, rowWidthFraction).Value = AgL.VNull(.Rows(0)("WidthFraction"))
                Dgl1.Item(Col1Value, rowThickness).Value = AgL.VNull(.Rows(0)("Thickness"))
                Dgl1.Item(Col1Value, rowThicknessFraction).Value = AgL.VNull(.Rows(0)("ThicknessFraction"))
                Dgl1.Item(Col1Value, rowArea).Value = AgL.VNull(.Rows(0)("Area"))
                Dgl1.Item(Col1Value, rowPerimeter).Value = AgL.VNull(.Rows(0)("Perimeter"))


                'ChkIsSystemDefine.Checked = AgL.VNull(.Rows(0)("IsSystemDefine"))
                'LblIsSystemDefine.Text = IIf(AgL.VNull(.Rows(0)("IsSystemDefine")) = 0, "User Define", "System Define")
                ChkIsSystemDefine.Enabled = False
            End If
        End With

        FrmSize_BaseFunction_DispText()
    End Sub

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
                " From Size I " &
                " Order By I.Description "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmSize_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 325, 885)
        FManageSystemDefine()
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

    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        Passed = FRestrictSystemDefine()


        FGetItemTypeSetting()
    End Sub

    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        Passed = FRestrictSystemDefine()
    End Sub

    Private Sub ChkIsSystemDefine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkIsSystemDefine.Click
        FManageSystemDefine()
    End Sub

    Private Sub FManageSystemDefine()
        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
            ChkIsSystemDefine.Visible = True
            ChkIsSystemDefine.Enabled = True
        Else
            ChkIsSystemDefine.Visible = False
            ChkIsSystemDefine.Enabled = False
        End If

        If ChkIsSystemDefine.Checked Then
            LblIsSystemDefine.Text = "System Define"
        Else
            LblIsSystemDefine.Text = "User Define"
        End If
    End Sub

    Private Function FRestrictSystemDefine() As Boolean
        If ChkIsSystemDefine.Checked = True Then
            If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
                If MsgBox("This is a System Define Item.Do You Want To Proceed...?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Topctrl1.FButtonClick(14, True)
                    FRestrictSystemDefine = False
                    Exit Function
                End If
            Else
                MsgBox("Can't Edit System Define Items...!", MsgBoxStyle.Information) : Topctrl1.FButtonClick(14, True)
                FRestrictSystemDefine = False
                Exit Function
            End If
        End If
        FManageSystemDefine()
        FRestrictSystemDefine = True
    End Function

    Private Sub FrmSize_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        Dim DsTemp As DataSet
        ChkIsSystemDefine.Checked = False
        FManageSystemDefine()

        'Dgl1.Item(Col1Value, rowArea).Value = AgL.VNull(AgL.PubDtEnviro.Rows(0)("Default_ProfitPer"))

        Dim I As Integer
        'mQry = " Select  H.Code, H.Description, H.Margin, H.Discount from RateType H Order By H.Sr "
        'DsTemp = AgL.FillData(mQry, AgL.GCn)
        'With DsTemp.Tables(0)
        '    Dgl2.RowCount = 1
        '    Dgl2.Rows.Clear()
        '    If .Rows.Count > 0 Then
        '        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
        '            Dgl2.Rows.Add()
        '            Dgl2.Item(ColSNo, I).Value = Dgl2.Rows.Count - 1
        '            Dgl2.Item(Col1RateType, I).Tag = AgL.XNull(.Rows(I)("Code"))
        '            Dgl2.Item(Col1RateType, I).Value = AgL.XNull(.Rows(I)("Description"))
        '            Dgl2.Item(Col1Margin, I).Value = Format(AgL.VNull(.Rows(I)("Margin")), "0.00")
        '            Dgl2.Item(Col1DiscountPer, I).Value = Format(AgL.VNull(.Rows(I)("Discount")), "0.00")
        '        Next I
        '        Dgl2.Visible = True
        '    Else
        '        Dgl2.Visible = False
        '    End If
        'End With

        'Dgl1(Col1Value, rowShape).Tag = ItemTypeCode.TradingProduct
        'Dgl1(Col1Value, rowShape).Value = "Trading Product"
        'Dgl1.Item(Col1Value, rowShape).Tag = ItemTypeCode.TradingProduct
        'Dgl1.Item(Col1Value, rowShape).Value = "Trading Product"

        FGetItemTypeSetting()
        'If DtItemTypeSetting.Rows(0)("IsSizeLinkedWithItemCategory") Then
        '    Dgl1(Col1Value, rowUnit).ReadOnly = False
        '    Dgl1.CurrentCell = Dgl1(Col1Value, rowUnit) 'Dgl1.FirstDisplayedCell
        '    Dgl1.Focus()
        'Else
        '    Dgl1(Col1Value, rowUnit).ReadOnly = True
        '    Dgl1.Item(Col1Value, rowUnit).Value = ""
        '    Dgl1.Item(Col1Value, rowUnit).Tag = ""
        '    Dgl1.Item(Col1Head, rowUnit).Tag = Nothing

        '    Dgl1.CurrentCell = Dgl1(Col1Value, rowDimension2) 'Dgl1.FirstDisplayedCell
        '    Dgl1.Focus()

        'End If

    End Sub

    Private Sub FrmSize_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dim I As Integer
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Head, 180, 255, Col1Head, True, True)
            .AddAgTextColumn(Dgl1, Col1HeadOriginal, 180, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(Dgl1, Col1Mandatory, 12, 20, Col1Mandatory, True, True)
            .AddAgTextColumn(Dgl1, Col1Value, 300, 255, Col1Value, True, False)
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


        Dgl1.Rows.Add(15)
        For I = 0 To Dgl1.Rows.Count - 1
            Dgl1.Rows(I).Visible = False
        Next

        Dgl1.Item(Col1Head, rowShape).Value = hcShape
        Dgl1.Item(Col1Head, rowUnit).Value = hcUnit
        Dgl1.Item(Col1Head, rowDescription).Value = hcSize
        Dgl1.Item(Col1Head, rowLength).Value = hcLength
        Dgl1.Item(Col1Head, rowLengthFraction).Value = hcLengthFraction
        Dgl1.Item(Col1Head, rowWidth).Value = hcWidth
        Dgl1.Item(Col1Head, rowWidthFraction).Value = hcWidthFraction
        Dgl1.Item(Col1Head, rowThickness).Value = hcThickness
        Dgl1.Item(Col1Head, rowThicknessFraction).Value = hcThicknessFraction
        Dgl1.Item(Col1Head, rowArea).Value = hcArea
        Dgl1.Item(Col1Head, rowPerimeter).Value = hcPerimeter
    End Sub

    Private Sub FGetItemTypeSetting()
        'If mItemTypeLastValue <> Dgl1(Col1Value, rowShape).Tag And Dgl1(Col1Value, rowShape).Tag <> "" Then
        '    mItemTypeLastValue = Dgl1(Col1Value, rowShape).Tag
        '    mQry = "Select * From ItemTypeSetting Where ItemType = '" & Dgl1(Col1Value, rowShape).Tag & "' And Div_Code = '" & TxtDivision.Tag & "' "
        '    DtItemTypeSetting = AgL.FillData(mQry, AgL.GCn).tables(0)
        '    If DtItemTypeSetting.Rows.Count = 0 Then
        '        mQry = "Select * From ItemTypeSetting Where ItemType = '" & Dgl1(Col1Value, rowShape).Tag & "' And Div_Code Is Null "
        '        DtItemTypeSetting = AgL.FillData(mQry, AgL.GCn).tables(0)
        '        If DtItemTypeSetting.Rows.Count = 0 Then
        '            mQry = "Select * From ItemTypeSetting Where ItemType Is Null And Div_Code Is Null "
        '            DtItemTypeSetting = AgL.FillData(mQry, AgL.GCn).tables(0)
        '            If DtItemTypeSetting.Rows.Count = 0 Then
        '                MsgBox("Item Type Setting Not Found")
        '            End If
        '        End If
        '    End If
        'End If

        ApplyItemTypeSetting(Dgl1(Col1Value, rowShape).Tag)
    End Sub


    Private Sub ApplyItemTypeSetting(ItemType As String)
        Dim mQry As String
        Dim DtTemp As DataTable
        Dim I As Integer, J As Integer
        Dim mDgl1RowCount As Integer
        Dim mDglRateTypeColumnCount As Integer
        Try

            mQry = "Select H.*
                    from EntryHeaderUISetting H                   
                    Where EntryName='FrmSize' And NCat = '" & ItemType & "' And GridName ='Dgl1' "

            mQry = "Select H.*
                    from EntryHeaderUISetting H                   
                    Where EntryName='FrmSize' And GridName ='Dgl1' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 0 To Dgl1.Rows.Count - 1
                        If AgL.XNull(DtTemp.Rows(I)("FieldName")) = Dgl1.Item(Col1Head, J).Value Then
                            Dgl1.Rows(J).Visible = AgL.VNull(DtTemp.Rows(I)("IsVisible"))
                            If AgL.VNull(DtTemp.Rows(I)("IsVisible")) Then mDgl1RowCount += 1
                            Dgl1.Item(Col1Mandatory, J).Value = IIf(AgL.VNull(DtTemp.Rows(I)("IsMandatory")), "Ä", "")
                            If AgL.XNull(DtTemp.Rows(I)("Caption")) <> "" Then
                                Dgl1.Item(Col1Head, J).Value = AgL.XNull(DtTemp.Rows(I)("Caption"))
                            End If
                            If AgL.VNull(DtTemp.Rows(I)("IsEditable")) = 0 Then Dgl1.Rows(J).ReadOnly = True
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


    Private Sub FrmSize_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        'If DtItemTypeSetting Is Nothing Then Exit Sub
        ChkIsSystemDefine.Enabled = False
        'Dgl2.Visible = False
        'If DtItemTypeSetting IsNot Nothing Then
        '    If DtItemTypeSetting.Rows(0)("IsSizeLinkedWithItemCategory") Then
        '        Dgl1(Col1Value, rowUnit).ReadOnly = IIf(Topctrl1.Mode <> "Browse", True, False)
        '    Else
        '        Dgl1(Col1Value, rowUnit).ReadOnly = False
        '    End If
        'Else
        '    Dgl1(Col1Value, rowUnit).ReadOnly = False
        'End If



        'Dgl1.Rows(rowPerimeter).Visible = DtItemTypeSetting.Rows(0)("IsApplicable_Barcode")
        'Dgl1.Rows(rowBarcodePattern).Visible = DtItemTypeSetting.Rows(0)("IsApplicable_Barcode")

    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Dim FrmObj As Form
        Dim StrUserPermission As String
        Dim DTUP As New DataTable

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
                Case rowLength, rowLengthFraction, rowWidth, rowWidthFraction, rowThickness, rowThicknessFraction
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberLeftPlaces = 0
                    CType(Dgl1.Columns(Col1Value), AgControls.AgTextColumn).AgNumberRightPlaces = 0

                    'Case rowShape
                    '    FrmObj = New FrmShape(StrUserPermission, DTUP)

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
                Case rowShape


                    If e.KeyCode = Keys.Insert Then
                        Call FOpenItemMaster(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex)
                    Else
                        If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = " Select Code, Description From Shape "
                            Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If
                        If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowUnit
                    If Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag Is Nothing Then
                        mQry = "SELECT Code, Code AS Description FROM Unit "
                        Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Value) Is Nothing Then
                        Dgl1.AgHelpDataSet(Col1Value) = Dgl1.Item(Col1Head, Dgl1.CurrentCell.RowIndex).Tag
                    End If



            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FOpenItemMaster(ByVal ColumnIndex As Integer, ByVal RowIndex As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim bItemCode$ = ""
        Dim objMdi As New MDIMain
        Dim StrUserPermission As String
        Dim DTUP As DataTable

        StrUserPermission = AgIniVar.FunGetUserPermission(ClsMain.ModuleName, objMdi.MnuItemMaster.Name, objMdi.MnuItemMaster.Text, DTUP)

        Dim frmObj As FrmShape

        frmObj = New FrmShape(StrUserPermission, DTUP)
        frmObj.EntryPointIniMode = AgTemplate.ClsMain.EntryPointIniMode.Insertion
        frmObj.StartPosition = FormStartPosition.CenterParent
        frmObj.IniGrid()
        'frmObj.TxtItemCategory.AgLastValueTag = Dgl1.Item(Col1PartyItemSpecification1, RowIndex).Tag
        'frmObj.TxtItemCategory.AgLastValueText = Dgl1.Item(Col1PartyItemSpecification1, RowIndex).Value
        'frmObj.Validate_ItemCategory()
        'frmObj.TxtItemGroup.AgLastValueTag = Dgl1.Item(Col1PartyItemSpecification2, RowIndex).Tag
        'frmObj.TxtItemGroup.AgLastValueText = Dgl1.Item(Col1PartyItemSpecification2, RowIndex).Value
        'frmObj.Validate_ItemGroup()
        frmObj.ShowDialog()
        bItemCode = frmObj.mSearchCode
        frmObj = Nothing


        Dgl1.Item(ColumnIndex, RowIndex).Value = ""
        Dgl1.Item(ColumnIndex, RowIndex).Tag = ""
        'Dgl1.CurrentCell = Dgl1.Item(ColumnIndex, RowIndex + 1)


        mQry = " Select Code, Description From Shape "
        Dgl1.Item(Col1Head, RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)


        DrTemp = Dgl1.AgHelpDataSet(Col1Head).Tables(0).Select("Code = '" & bItemCode & "'")
        Dgl1.Item(ColumnIndex, RowIndex).Tag = bItemCode
        Dgl1.Item(ColumnIndex, RowIndex).Value = AgL.XNull(AgL.Dman_Execute("Select Description From Shape Where Code = '" & Dgl1.Item(ColumnIndex, Dgl1.CurrentCell.RowIndex).Tag & "'", AgL.GCn).ExecuteScalar)
        'Validating_ItemCode(bItemCode, ColumnIndex, RowIndex)
        Dgl1.CurrentCell = Dgl1.Item(Col1Value, RowIndex)
        SendKeys.Send("{Enter}")
    End Sub

    'Private Sub DGLRateType_EditingControl_KeyDown(sender As Object, e As KeyEventArgs) Handles Dgl2.EditingControl_KeyDown
    '    Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
    '    Dim bItemCode As String = ""
    '    Dim DrTemp As DataRow() = Nothing
    '    Try
    '        bRowIndex = Dgl2.CurrentCell.RowIndex
    '        bColumnIndex = Dgl2.CurrentCell.ColumnIndex

    '        If e.KeyCode = Keys.Enter Then Exit Sub
    '        If Topctrl1.Mode = "Browse" Then Exit Sub


    '        Select Case Dgl2.Columns(Dgl2.CurrentCell.ColumnIndex).Name
    '            'Case Col1DiscountPattern
    '            '    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
    '            '        If Dgl2.AgHelpDataSet(bColumnIndex) Is Nothing Then
    '            '            mQry = ClsMain.GetStringsFromClassConstants(GetType(DiscountCalculationPattern))
    '            '            Dgl2.AgHelpDataSet(bColumnIndex) = AgL.FillData(mQry, AgL.GCn)
    '            '        End If
    '            '    End If
    '        End Select
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub

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
                'Case rowShape
                '    FGetItemTypeSetting()

                'If DtItemTypeSetting.Rows(0)("IsSizeLinkedWithItemCategory") Then

                '    Dgl1(Col1Value, rowUnit).ReadOnly = False
                'Else
                '    Dgl1(Col1Value, rowUnit).ReadOnly = True
                '    Dgl1(Col1Value, rowUnit).Value = ""
                '    Dgl1(Col1Value, rowUnit).Tag = ""
                '    Dgl1(Col1Head, rowUnit).Tag = Nothing
                'End If

                Case rowLength, rowLengthFraction, rowWidth, rowWidthFraction, rowShape, rowUnit
                    FCalculation()

            End Select
        End If
    End Sub

    Private Sub FCalculation()
        Dim mSizeName As String = ""
        Dim mLengthName As String = ""
        Dim mWidthName As String = ""
        Dim mArea As Decimal = 0
        Dim mPerimeter As Decimal = 0
        Dim munitFraction As Decimal = 0

        If Dgl1(Col1Value, rowShape).Value <> "" And Dgl1(Col1Value, rowUnit).Value <> "" Then
            If Dgl1(Col1Value, rowUnit).Value = ClsMain.UnitConstants.Meter Then
                munitFraction = 100
                mLengthName = (Val(Dgl1(Col1Value, rowLength).Value) * 100 + Val(Dgl1(Col1Value, rowLengthFraction).Value)).ToString()
                mWidthName = (Val(Dgl1(Col1Value, rowWidth).Value) * 100 + Val(Dgl1(Col1Value, rowWidthFraction).Value)).ToString()
                mSizeName = mLengthName & "X" & mWidthName
                mArea = Math.Round((Val(Dgl1(Col1Value, rowLength).Value) + Val(Dgl1(Col1Value, rowLengthFraction).Value) / munitFraction) * (Val(Dgl1(Col1Value, rowWidth).Value) + Val(Dgl1(Col1Value, rowWidthFraction).Value) / munitFraction), 3)
                mPerimeter = Math.Floor(((Val(Dgl1(Col1Value, rowLength).Value) + Val(Dgl1(Col1Value, rowLengthFraction).Value) / munitFraction) + (Val(Dgl1(Col1Value, rowWidth).Value) + Val(Dgl1(Col1Value, rowWidthFraction).Value) / munitFraction)) * 2)
            ElseIf Dgl1(Col1Value, rowUnit).Value = ClsMain.UnitConstants.Feet Then
                munitFraction = 12
                mLengthName = Dgl1(Col1Value, rowLength).Value & "`"
                If Val(Dgl1(Col1Value, rowLengthFraction).Value) <> 0 Then mLengthName = mLengthName + Dgl1(Col1Value, rowLengthFraction).Value & """"
                mWidthName = Dgl1(Col1Value, rowWidth).Value & "`"
                If Val(Dgl1(Col1Value, rowWidthFraction).Value) <> 0 Then mWidthName = mWidthName + Dgl1(Col1Value, rowWidthFraction).Value & """"
                mSizeName = mLengthName & "X" & mWidthName

                mArea = Math.Round((Val(Dgl1(Col1Value, rowLength).Value) + Val(Dgl1(Col1Value, rowLengthFraction).Value) / munitFraction) * (Val(Dgl1(Col1Value, rowWidth).Value) + Val(Dgl1(Col1Value, rowWidthFraction).Value) / munitFraction), 3)
                mPerimeter = Math.Floor(((Val(Dgl1(Col1Value, rowLength).Value) + Val(Dgl1(Col1Value, rowLengthFraction).Value) / munitFraction) + (Val(Dgl1(Col1Value, rowWidth).Value) + Val(Dgl1(Col1Value, rowWidthFraction).Value) / munitFraction)) * 2)

            End If


                If Dgl1(Col1Value, rowLength).Value = Dgl1(Col1Value, rowWidth).Value And Dgl1(Col1Value, rowLengthFraction).Value = Dgl1(Col1Value, rowWidthFraction).Value Then
                If Dgl1(Col1Value, rowShape).Value = ClsMain.ShapeConstants.Circle Then
                    mSizeName = mLengthName & " RD"
                Else
                    mSizeName = mLengthName & " SQ"
                End If
            End If


        End If

            Dgl1(Col1Value, rowDescription).Value = mSizeName
        Dgl1(Col1Value, rowArea).Value = mArea
        Dgl1(Col1Value, rowPerimeter).Value = mPerimeter

    End Sub

    Private Sub FrmSize_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dim i As Integer

        For i = 0 To Dgl1.Rows.Count - 1
            Dgl1(Col1Value, i).Value = ""
            Dgl1(Col1Value, i).Tag = ""
        Next
    End Sub
End Class
