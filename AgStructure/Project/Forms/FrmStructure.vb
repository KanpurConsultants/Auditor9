Imports System.Data.SQLite
Public Class FrmStructure
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable
    Dim mQry As String = "", mSearchCode As String = ""

    Private Const Col_SNo As String = "Sr"
    Public WithEvents DGL1 As New AgControls.AgDataGrid

    Private Const Col1_WEF As String = "WEF"
    Private Const Col1_Charges As String = "Charges"
    Private Const Col1_ChargesManualCode As String = "Manual Code"
    Private Const Col1_Charge_Type As String = "Charges Type"
    Private Const Col1_value_Type As String = "Value Type"
    Private Const Col1_value As String = "Value"
    Private Const Col1_Calculation As String = "Calculation"
    Private Const Col1_BaseColumn As String = "Base Column"
    Private Const Col1_PostAc As String = "Post A/c"
    Private Const Col1_DrCr As String = "Dr/Cr"
    Private Const Col1_LineItem As String = "Line Item"
    Private Const Col1_AffectCost As String = "Affect Cost"
    Private Const Col1_InactiveDate As String = "Inactive Date"
    Private Const Col1_VisibleInMaster As String = "Visible In Master"
    Private Const Col1_VisibleInMasterLine As String = "Visible in Master Line"
    Private Const Col1_VisibleInTransactionLine As String = "Visible in Transaction Line"
    Private Const Col1_VisibleInTransactionFooter As String = "Visible in Transaction Footer"
    Private Const Col1_HeaderPerField As String = "Header Per Field"
    Private Const Col1_HeaderAmtField As String = "Header Amt Field"
    Private Const Col1_LinePerField As String = "Line Per Field"
    Private Const Col1_LineAmtField As String = "Line Amt Field"
    Private Const Col1_GridDisplayIndex As String = "Grid Display Index"

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

    Private Sub Form_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        DTMaster = Nothing
    End Sub

    Private Sub IniGrid()
        Dim mChargeType As String = "Charges,SalesTax,SalesAdditionalTax,ExciseAssessableAmt,BasicExciseDuty,ExciseCess,ExciseEduCess,ExciseHEduCess,CustomDutyAssessableAmt,CustomDuty,CustomDutyEduCess,CustomDutyHEduCess,CustomAdditionalDuty,SalesTaxAssessableAmt,VAT,SAT,CST,Cost,ServiceTaxAssessableAmt,ServiceTax,ECess,HECess,EntryTax,OtherTax,ESI,EPF,Pension"
        Dim mValueType As String = "Percentage,Percentage Changeable,Percentage From Column,Percentage Or Amount,FixedValue,FixedValue Changeable,FixedValue From Column,Round_Off"
        AgL.AddAgDataGrid(DGL1, Pnl1)
        With AgCL
            .AddAgNumberColumn(DGL1, Col_SNo, 40, 5, 0, False, Col_SNo, True, False, True)
            .AddAgDateColumn(DGL1, Col1_WEF, 80, Col1_WEF)
            .AddAgTextColumn(DGL1, Col1_Charges, 170, 0, Col1_Charges, True, False, False)
            .AddAgTextColumn(DGL1, Col1_ChargesManualCode, 100, 0, Col1_ChargesManualCode, True, True, False)
            .AddAgListColumn(DGL1, mChargeType, Col1_Charge_Type, 80, mChargeType, Col1_Charge_Type, True, False, False)
            .AddAgListColumn(DGL1, mValueType, Col1_value_Type, 100, mValueType, Col1_value_Type, True, False, False)
            .AddAgTextColumn(DGL1, Col1_value, 100, 20, Col1_value, True, False, False)
            .AddAgTextColumn(DGL1, Col1_Calculation, 250, 255, Col1_Calculation, True, False, False)
            .AddAgTextColumn(DGL1, Col1_BaseColumn, 250, 50, Col1_BaseColumn, True, False, False)
            .AddAgTextColumn(DGL1, Col1_PostAc, 150, 0, Col1_PostAc, True, False, False)
            .AddAgListColumn(DGL1, "Dr,Cr", Col1_DrCr, 80, "Dr,Cr", Col1_DrCr, True, False, False)
            .AddAgListColumn(DGL1, "Yes,No", Col1_LineItem, 80, "1,0", Col1_LineItem, True, False, False)
            .AddAgListColumn(DGL1, "Addition,Deduction", Col1_AffectCost, 80, "1,0", Col1_AffectCost, True, False, False)
            .AddAgDateColumn(DGL1, Col1_InactiveDate, 80, Col1_InactiveDate)            
            .AddAgListColumn(DGL1, "Yes,No", Col1_VisibleInMaster, 80, "1,0", Col1_VisibleInMaster, True, False, False)
            .AddAgListColumn(DGL1, "Yes,No", Col1_VisibleInMasterLine, 80, "1,0", Col1_VisibleInMasterLine, True, False, False)
            .AddAgListColumn(DGL1, "Yes,No", Col1_VisibleInTransactionLine, 80, "1,0", Col1_VisibleInTransactionLine, True, False, False)
            .AddAgListColumn(DGL1, "Yes,No", Col1_VisibleInTransactionFooter, 80, "1,0", Col1_VisibleInTransactionFooter, True, False, False)
            .AddAgTextColumn(DGL1, Col1_HeaderPerField, 100, 0, Col1_HeaderPerField, True, False, False)
            .AddAgTextColumn(DGL1, Col1_HeaderAmtField, 100, 0, Col1_HeaderAmtField, True, False, False)
            .AddAgTextColumn(DGL1, Col1_LinePerField, 100, 0, Col1_LinePerField, True, False, False)
            .AddAgTextColumn(DGL1, Col1_LineAmtField, 100, 0, Col1_LineAmtField, True, False, False)
            .AddAgNumberColumn(DGL1, Col1_GridDisplayIndex, 100, 3, 0, False, Col1_GridDisplayIndex, True, False, True)

        End With
        DGL1.EnableHeadersVisualStyles = False
        DGL1.ColumnHeadersHeight = 50
    End Sub

    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Or e.KeyCode = Keys.F3 Or e.KeyCode = Keys.F4 Or e.KeyCode = (Keys.F And e.Control) Or e.KeyCode = (Keys.P And e.Control) _
        Or e.KeyCode = (Keys.S And e.Control) Or e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F5 Or e.KeyCode = Keys.F10 _
        Or e.KeyCode = Keys.Home Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Or e.KeyCode = Keys.End Then
            Topctrl1.TopKey_Down(e)
        End If


        If Me.ActiveControl IsNot Nothing Then
            If Me.ActiveControl.Name <> Topctrl1.Name And _
                Not (TypeOf (Me.ActiveControl) Is AgControls.AgDataGrid) Then
                If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
            End If
        End If
    End Sub

    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then Exit Sub
        If Me.ActiveControl Is Nothing Then Exit Sub
        AgL.CheckQuote(e)
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.WinSetting(Me, 650, 990, 0, 0)
            AgL.GridDesign(DGL1)

            IniGrid()
            FIniMaster()
            Ini_List()
            DispText()
            MoveRec()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FIniMaster(Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 1)
        mQry = "Select Code As SearchCode " & _
                " From Structure  "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub


    Sub Ini_List()
        mQry = "Select code As Code, Description  " & _
                " From Structure  " & _
                " Order By Description"
        TxtDescription.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
        TxtCopyFrom.AgHelpDataSet = TxtDescription.AgHelpDataSet.Copy

        mQry = "Select Code, Description, ManualCode, FieldName From Charges Order By Description"
        DGL1.AgHelpDataSet(Col1_Charges) = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select SubCode, Name From Subgroup Where " & AgL.PubSiteConditionCommonAc(AgL.PubIsHo, "Site_Code", AgL.PubSiteCode, "CommonAc") & " Order By Name "
        DGL1.AgHelpDataSet(Col1_PostAc) = AgL.FillData(mQry, AgL.GCn)

    End Sub

    Private Sub Topctrl1_StyleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Topctrl1.StyleChanged

    End Sub


    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        BlankText()
        DispText()
        TxtCode.Focus()
    End Sub

    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New Object
        Dim MastPos As Long
        Dim mTrans As Boolean = False


        Try
            MastPos = BMBMaster.Position


            If DTMaster.Rows.Count > 0 Then
                If MsgBox("Are You Sure To Delete This Record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, AgLibrary.ClsMain.PubMsgTitleInfo) = vbYes Then


                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = True

                    AgL.LedgerUnPost(AgL.GCn, AgL.ECmd, mSearchCode)

                    mQry = "Delete From Structure Where CODE = '" & mSearchCode & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mQry = "Delete From StructureDetail Where Code = '" & mSearchCode & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    Call AgL.LogTableEntry(mSearchCode, Me.Text, "D", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)

                    AgL.ETrans.Commit()
                    mTrans = False


                    FIniMaster(1)
                    Topctrl1_tbRef()
                    MoveRec()
                End If
            End If
        Catch Ex As Exception
            If mTrans = True Then AgL.ETrans.Rollback()
            MsgBox(Ex.Message, MsgBoxStyle.Information, AgLibrary.ClsMain.PubMsgTitleInfo)
        End Try
    End Sub

    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        FIniMaster(0, 0)
        Topctrl1.Focus()
    End Sub


    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        DispText()

    End Sub


    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox("No Records To Search.", vbInformation, AgLibrary.ClsMain.PubMsgTitleInfo) : Exit Sub
        Try


            AgL.PubFindQry = "Select  Code As SearchCode,  Description   " & _
                                " From  Structure   "

            AgL.PubFindQryOrdBy = "[Description]"


            '*************** common code start *****************
            AgL.PubObjFrmFind = New AgLibrary.FrmFind(AgL.PubFindQry, "Find", AgL)
            AgL.PubObjFrmFind.ShowDialog()
            AgL.PubObjFrmFind = Nothing
            If AgL.PubSearchRow <> " Then" Then
                AgL.PubDRFound = DTMaster.Rows.Find(AgL.PubSearchRow)
                BMBMaster.Position = DTMaster.Rows.IndexOf(AgL.PubDRFound)
                MoveRec()
            End If
            '*************** common code end  *****************
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Private Sub Topctrl1_tbRef() Handles Topctrl1.tbRef
        Ini_List()
    End Sub

    Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn

    End Sub

    Private Sub Topctrl1_tbSave() Handles Topctrl1.tbSave
        Dim MastPos As Long
        Dim I As Integer, mSr As Integer = 0
        Dim mTrans As Boolean = False
        Try
            MastPos = BMBMaster.Position

            If Not Data_Validation() Then Exit Sub


            If Topctrl1.Mode = "Add" Then
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From Structure Where Code='" & TxtCode.Text & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Code Already Exist!") : TxtCode.Focus() : Exit Sub
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From Structure Where Description='" & TxtDescription.Text & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Description Already Exist!") : TxtDescription.Focus() : Exit Sub

                mSearchCode = TxtCode.Text 'AgL.GetMaxId("Charges", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 6, True, True, , AgL.Gcn_ConnectionString)
            Else
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From Structure Where Code='" & TxtCode.Text & "' And Code<>'" & mSearchCode & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Code Already Exist!") : TxtCode.Focus() : Exit Sub
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From Structure Where Description='" & TxtDescription.Text & "' And Code<>'" & mSearchCode & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Description Already Exist!") : TxtDescription.Focus() : Exit Sub
            End If




            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True


            If Topctrl1.Mode = "Add" Then

                mQry = "INSERT INTO Structure " & _
                    "(Code,Div_Code, Site_Code," & _
                    "Description,PreparedBy,U_EntDt,U_AE) " & _
                    "VALUES " & _
                    "(" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(AgL.PubDivCode) & ", " & AgL.Chk_Text(AgL.PubSiteCode) & ",   " & _
                    "" & AgL.Chk_Text(TxtDescription.Text) & "," & _
                    " " & AgL.Chk_Text(AgL.PubUserName) & "," & AgL.ConvertDate(AgL.PubLoginDate) & ",'" & AgL.MidStr(Topctrl1.Mode, 0, 1) & "')"

                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                mQry = "UPDATE Structure " & _
                       "SET Description =" & AgL.Chk_Text(TxtDescription.Text) & ", " & _
                       " U_AE = 'E',	Edit_Date = " & AgL.ConvertDate(AgL.PubLoginDate) & ",	ModifiedBy = " & AgL.Chk_Text(AgL.PubUserName) & "  " & _
                      "Where Code = '" & mSearchCode & "' "

                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            mQry = "Delete From StructureDetail Where Code = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            With DGL1
                mSr = 0
                For I = 0 To .Rows.Count - 1
                    If .Item(Col1_Charges, I).Value <> "" Then
                        mSr = mSr + 1

                        mQry = "INSERT INTO  dbo.StructureDetail " & _
                        "( " & _
                        "Code, " & _
                        "Sr, " & _
                        "WEF, " & _
                        "Charges, " & _
                        "Charge_Type, " & _
                        "Value_Type, " & _
                        "Value, " & _
                        "Calculation, " & _
                        "BaseColumn, " & _
                        "PostAc, " & _
                        "DrCr, " & _
                        "LineItem, " & _
                        "AffectCost, " & _
                        "InactiveDate, " & _
                        "VisibleInMaster, " & _
                        "VisibleInMasterLine, " & _
                        "VisibleInTransactionLine, " & _
                        "VisibleInTransactionFooter, " & _
                        "HeaderPerField, " & _
                        "HeaderAmtField, " & _
                        "LinePerField, " & _
                        "LineAmtField, " & _
                        "GridDisplayIndex " & _
                        ") " & _
                        "VALUES" & _
                        "(" & _
                        "" & AgL.Chk_Text(mSearchCode) & ", " & _
                        "" & AgL.Chk_Text(DGL1.Item(Col_SNo, I).Value) & ", " & _
                        "" & AgL.Chk_Text(DGL1.Item(Col1_WEF, I).Value) & " , " & _
                        "" & AgL.Chk_Text(DGL1.Item(Col1_Charges, I).Tag) & " , " & _
                        "" & AgL.Chk_Text(DGL1.Item(Col1_Charge_Type, I).Tag) & " , " & _
                        "" & AgL.Chk_Text(DGL1.Item(Col1_value_Type, I).Tag) & " , " & _
                        "" & AgL.Chk_Text(DGL1.Item(Col1_value, I).Value) & " , " & _
                        "" & AgL.Chk_Text(DGL1.Item(Col1_Calculation, I).Value) & " , " & _
                        "" & AgL.Chk_Text(DGL1.Item(Col1_BaseColumn, I).Value) & " , " & _
                        "" & AgL.Chk_Text(DGL1.Item(Col1_PostAc, I).Tag) & " , " & _
                        "" & AgL.Chk_Text(DGL1.Item(Col1_DrCr, I).Tag) & " , " & _
                        "" & AgL.Chk_Text(DGL1.Item(Col1_LineItem, I).Tag) & " , " & _
                        "" & AgL.Chk_Text(DGL1.Item(Col1_AffectCost, I).Tag) & " , " & _
                        "" & AgL.Chk_Text(DGL1.Item(Col1_InactiveDate, I).Value) & "  ," & _
                        "" & AgL.Chk_Text(DGL1.Item(Col1_VisibleInMaster, I).Tag) & "  ," & _
                        "" & AgL.Chk_Text(DGL1.Item(Col1_VisibleInMasterLine, I).Tag) & "  ," & _
                        "" & AgL.Chk_Text(DGL1.Item(Col1_VisibleInTransactionLine, I).Tag) & "  ," & _
                        "" & AgL.Chk_Text(DGL1.Item(Col1_VisibleInTransactionFooter, I).Tag) & ",  " & _
                        "" & AgL.Chk_Text(DGL1.Item(Col1_HeaderPerField, I).Value) & ",  " & _
                        "" & AgL.Chk_Text(DGL1.Item(Col1_HeaderAmtField, I).Value) & ",  " & _
                        "" & AgL.Chk_Text(DGL1.Item(Col1_LinePerField, I).Value) & ",  " & _
                        "" & AgL.Chk_Text(DGL1.Item(Col1_LineAmtField, I).Value) & ",  " & _
                        "" & Val(DGL1.Item(Col1_GridDisplayIndex, I).Value) & "  " & _
                        ") "

                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                Next I
            End With


            Call AgL.LogTableEntry(mSearchCode, Me.Text, AgL.MidStr(Topctrl1.Mode, 0, 1), AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)


            AgL.ETrans.Commit()
            mTrans = False
            FIniMaster(0, 1)
            Topctrl1_tbRef()
            If Topctrl1.Mode = "Add" Then
                Topctrl1.LblDocId.Text = mSearchCode
                Topctrl1.FButtonClick(0)
                Exit Sub
            Else
                Topctrl1.SetDisp(True)
                MoveRec()
            End If
        Catch ex As Exception
            If mTrans = True Then AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub MoveRec()
        Dim DsTemp As DataSet = Nothing
        Dim MastPos As Long

        Try
            FClear()
            BlankText()
            If DTMaster.Rows.Count > 0 Then



                MastPos = BMBMaster.Position
                mSearchCode = DTMaster.Rows(MastPos)("SearchCode")
                mQry = "Select SI.* " & _
                        " From Structure Si " & _
                        " Where SI.Code='" & mSearchCode & "'"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    If .Rows.Count > 0 Then
                        TxtCode.Text = AgL.XNull(.Rows(0)("Code"))
                        TxtDescription.Text = AgL.XNull(.Rows(0)("Description"))
                        TxtPrepared.Text = AgL.XNull(.Rows(0)("PreparedBy"))
                        TxtModified.Text = AgL.XNull(.Rows(0)("ModifiedBy"))
                        GroupBox4.Visible = IIf(TxtModified.Text.Trim <> "", True, False)

                    End If
                End With


                MoveRecLine(mSearchCode)

            Else
                BlankText()
            End If
            Topctrl1.FSetDispRec(BMBMaster)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            DsTemp = Nothing
        End Try
    End Sub

    Private Sub MoveRecLine(ByVal mCode As String)
        Dim DsTemp As DataSet
        Dim I As Integer

        mQry = "Select SN.*, C.ManualCode " & _
            " From StructureDetail SN " & _
            " Left Join Charges C On SN.Charges = C.Code " & _
            " Where Sn.Code='" & mCode & "' Order By Sr"
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            DGL1.RowCount = 1
            DGL1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    DGL1.Rows.Add()
                    DGL1.Item(Col_SNo, I).Value = AgL.VNull(.Rows(I)("Sr"))
                    DGL1.Item(Col1_WEF, I).Value = AgL.XNull(.Rows(I)("WEF"))
                    DGL1.AgSelectedValue(Col1_Charges, I) = AgL.XNull(.Rows(I)("Charges"))
                    DGL1.Item(Col1_ChargesManualCode, I).Value = AgL.XNull(.Rows(I)("ManualCode"))
                    DGL1.AgSelectedValue(Col1_Charge_Type, I) = AgL.XNull(.Rows(I)("Charge_Type"))
                    DGL1.AgSelectedValue(Col1_value_Type, I) = AgL.XNull(.Rows(I)("Value_Type"))
                    DGL1.Item(Col1_value, I).Value = AgL.XNull(.Rows(I)("Value"))
                    DGL1.Item(Col1_Calculation, I).Value = AgL.XNull(.Rows(I)("Calculation"))
                    DGL1.Item(Col1_BaseColumn, I).Value = AgL.XNull(.Rows(I)("BaseColumn"))
                    DGL1.AgSelectedValue(Col1_PostAc, I) = AgL.XNull(.Rows(I)("PostAc"))
                    DGL1.AgSelectedValue(Col1_DrCr, I) = AgL.XNull(.Rows(I)("DrCr"))
                    DGL1.AgSelectedValue(Col1_LineItem, I) = Math.Abs(AgL.VNull(.Rows(I)("LineItem")))
                    DGL1.AgSelectedValue(Col1_AffectCost, I) = IIf(IsDBNull(.Rows(I)("AffectCost")), "", Math.Abs(AgL.VNull(.Rows(I)("AffectCost"))))
                    DGL1.Item(Col1_InactiveDate, I).Value = AgL.XNull(.Rows(I)("InactiveDate"))
                    DGL1.AgSelectedValue(Col1_VisibleInMaster, I) = Math.Abs(AgL.VNull(.Rows(I)("VisibleInMaster")))
                    DGL1.AgSelectedValue(Col1_VisibleInMasterLine, I) = Math.Abs(AgL.VNull(.Rows(I)("VisibleInMasterLine")))
                    DGL1.AgSelectedValue(Col1_VisibleInTransactionLine, I) = Math.Abs(AgL.VNull(.Rows(I)("VisibleInTransactionLine")))
                    DGL1.AgSelectedValue(Col1_VisibleInTransactionFooter, I) = Math.Abs(AgL.VNull(.Rows(I)("VisibleInTransactionFooter")))
                    DGL1.Item(Col1_HeaderPerField, I).Value = AgL.XNull(.Rows(I)("HeaderPerField"))
                    DGL1.Item(Col1_HeaderAmtField, I).Value = AgL.XNull(.Rows(I)("HeaderAmtField"))
                    DGL1.Item(Col1_LinePerField, I).Value = AgL.XNull(.Rows(I)("LinePerField"))
                    DGL1.Item(Col1_LineAmtField, I).Value = AgL.XNull(.Rows(I)("LineAmtField"))
                    DGL1.Item(Col1_GridDisplayIndex, I).Value = AgL.XNull(.Rows(I)("GridDisplayIndex"))
                Next I
            End If
        End With

    End Sub


    Private Sub BlankText()
        If Topctrl1.Mode <> "Add" Then Topctrl1.BlankTextBoxes()
        mSearchCode = ""

        DGL1.RowCount = 1 : DGL1.Rows.Clear()

    End Sub

    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
        If Topctrl1.Mode <> "Add" Then
            TxtCode.Enabled = False
        End If
    End Sub

    Private Sub DGL1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGL1.CellEnter
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer

        Try
            mRowIndex = DGL1.CurrentCell.RowIndex
            mColumnIndex = DGL1.CurrentCell.ColumnIndex

            If DGL1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then DGL1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case DGL1.Columns(DGL1.CurrentCell.ColumnIndex).Name
                Case Col1_Charges

            End Select

        Catch ex As Exception

        End Try
    End Sub

    Private Sub DGL1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DGL1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer

        Try
            mRowIndex = DGL1.CurrentCell.RowIndex
            mColumnIndex = DGL1.CurrentCell.ColumnIndex

            If DGL1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then DGL1.Item(mColumnIndex, mRowIndex).Value = ""

            Select Case DGL1.Columns(DGL1.CurrentCell.ColumnIndex).Name
                Case Col1_Charges
                    If Me.Controls("HelpDg") IsNot Nothing Then
                        With CType(Me.Controls("HelpDg"), AgControls.AgDataGrid)
                            If .CurrentCell IsNot Nothing Then
                                If DGL1.AgSelectedValue(mColumnIndex, mRowIndex).Trim <> "" Then
                                    DGL1.Item(Col1_ChargesManualCode, mRowIndex).Value = AgL.XNull(.Item("ManualCode", .CurrentCell.RowIndex).Value)
                                    If AgL.XNull(.Item("FieldName", .CurrentCell.RowIndex).Value) <> "" Then
                                        DGL1.Item(Col1_HeaderPerField, mRowIndex).Value = AgL.XNull(.Item("FieldName", .CurrentCell.RowIndex).Value) & "_Per"
                                        DGL1.Item(Col1_HeaderAmtField, mRowIndex).Value = AgL.XNull(.Item("FieldName", .CurrentCell.RowIndex).Value)
                                        DGL1.Item(Col1_LinePerField, mRowIndex).Value = AgL.XNull(.Item("FieldName", .CurrentCell.RowIndex).Value) & "_Per"
                                        DGL1.Item(Col1_LineAmtField, mRowIndex).Value = AgL.XNull(.Item("FieldName", .CurrentCell.RowIndex).Value)
                                    End If

                                    DGL1.AgSelectedValue(Col1_LineItem, mRowIndex) = "0"
                                    DGL1.AgSelectedValue(Col1_VisibleInMaster, mRowIndex) = "1"
                                    DGL1.AgSelectedValue(Col1_VisibleInMasterLine, mRowIndex) = "0"
                                    DGL1.AgSelectedValue(Col1_VisibleInTransactionLine, mRowIndex) = "0"
                                    DGL1.AgSelectedValue(Col1_VisibleInTransactionFooter, mRowIndex) = "1"
                                Else
                                    DGL1.Item(Col1_ChargesManualCode, mRowIndex).Value = ""
                                End If
                            End If
                        End With
                    End If


            End Select

            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles DGL1.EditingControlShowing
        If Topctrl1.Mode = "Browse" Then Exit Sub
        If TypeOf e.Control Is ComboBox Then
            e.Control.Text = ""
        End If
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGL1.KeyDown
        If Topctrl1.Mode <> "Browse" Then
            If e.Control And e.KeyCode = Keys.D Then
                sender.CurrentRow.Selected = True
            End If
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub

        Try
            Select Case sender.CurrentCell.ColumnIndex

            End Select
        Catch Ex As NullReferenceException
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub


    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles DGL1.RowsAdded
        'sender(Col_SNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub


    Private Sub DGL1_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles DGL1.RowsRemoved
        Try
            DTStruct.Rows.Remove(DTStruct.Rows.Item(e.RowIndex))
        Catch ex As Exception
        End Try
        AgL.FSetSNo(sender, DGL1.Columns(Col_SNo).Index)

        If DGL1.Rows.Count = 1 And Topctrl1.Mode = "Add" Then
            If DGL1.Item(Col1_Charges, 0).Value Is Nothing Then DGL1.Item(Col1_Charges, 0).Value = ""
        End If
    End Sub


    Private Sub FClear()
        DTStruct.Clear()
    End Sub
    Private Sub FAddRowStructure()
        Dim DRStruct As DataRow
        Try
            DRStruct = DTStruct.NewRow
            DTStruct.Rows.Add(DRStruct)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Control_Enter(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Select Case sender.name

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Try
            Select Case sender.NAME

            End Select

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function Data_Validation() As Boolean
        Dim I As Integer = 0
        Dim J As Integer = 0
        Try



            If AgCL.AgCheckMandatory(Me) = False Then Exit Function

            If AgCL.AgIsDuplicate(DGL1, "" & Col1_Charges & Col1_WEF & "") Then Exit Function

            With DGL1
                For I = 0 To .Rows.Count - 1
                    If .Item(Col1_Charges, I).Value Is Nothing Then .Item(Col1_Charges, I).Value = ""

                    If AgL.StrCmp(.Item(Col1_LineItem, I).Value, "No") And .Item(Col1_Charge_Type, I).Value = "Charges" And .Item(Col1_Calculation, I).Value = "" Then
                        Select Case UCase(.Item(Col1_value_Type, I).Value)
                            Case "FIXEDVALUE", "FIXEDVALUE CHANGEABLE"
                                If .Item(Col1_BaseColumn, I).Value = "" Then
                                    MsgBox("Base Column For Charge : " & .Item(Col1_Charges, I).Value & " Is Required")
                                    Exit Function
                                End If                                
                        End Select
                    End If
                Next
            End With



            '=================CHECKING OF DATABASE FIELD NAMES ARE PROPER OR NOT ==============
            For I = 0 To DGL1.Rows.Count - 1
                If DGL1.Item(Col1_Charges, I).Value <> "" Then
                    If DGL1.Item(Col1_WEF, I).Value = "" Then
                        MsgBox(Col1_WEF & " For Charge : " & DGL1.Item(Col1_Charges, I).Value & " Is Required")
                        Exit Function
                    End If

                    For J = I + 1 To DGL1.Rows.Count - 1
                        If DGL1.Item(Col1_Charges, I).Value = DGL1.Item(Col1_Charges, I).Value Then

                        End If
                    Next


                    Select Case UCase(DGL1.Item(Col1_value_Type, I).Value)
                        Case ClsMain.ValueType.FixedValue.ToUpper, ClsMain.ValueType.FixedValue_Changeable.ToUpper, ClsMain.ValueType.FixedValue_From_Column.ToUpper
                            If DGL1.Item(Col1_HeaderAmtField, I).Value = "" Then
                                MsgBox(Col1_HeaderAmtField & " For Charge : " & DGL1.Item(Col1_Charges, I).Value & " Is Required")
                                Exit Function
                            End If
                            If DGL1.Item(Col1_LineAmtField, I).Value = "" Then
                                MsgBox(Col1_LineAmtField & " For Charge : " & DGL1.Item(Col1_Charges, I).Value & " Is Required")
                                Exit Function
                            End If

                            DGL1.Item(Col1_HeaderPerField, I).Value = ""
                            DGL1.Item(Col1_LinePerField, I).Value = ""

                        Case ClsMain.ValueType.Percentage.ToUpper, ClsMain.ValueType.Percentage_Changeable.ToUpper, ClsMain.ValueType.Percentage_From_Column.ToUpper, ClsMain.ValueType.Percentage_Or_Amount.ToUpper
                            If DGL1.Item(Col1_HeaderPerField, I).Value = "" Then
                                MsgBox(Col1_HeaderPerField & " For Charge : " & DGL1.Item(Col1_Charges, I).Value & " Is Required")
                                Exit Function
                            End If

                            If DGL1.Item(Col1_HeaderAmtField, I).Value = "" Then
                                MsgBox(Col1_HeaderAmtField & " For Charge : " & DGL1.Item(Col1_Charges, I).Value & " Is Required")
                                Exit Function
                            End If

                            If DGL1.Item(Col1_LinePerField, I).Value = "" Then
                                MsgBox(Col1_LinePerField & " For Charge : " & DGL1.Item(Col1_Charges, I).Value & " Is Required")
                                Exit Function
                            End If

                            If DGL1.Item(Col1_LineAmtField, I).Value = "" Then
                                MsgBox(Col1_LineAmtField & " For Charge : " & DGL1.Item(Col1_Charges, I).Value & " Is Required")
                                Exit Function
                            End If
                    End Select
                End If
            Next
            '==================================================================================

            Data_Validation = True
        Catch ex As Exception
            MsgBox(ex.Message)
            Data_Validation = False
        End Try
    End Function

    Private Sub Calculation()

    End Sub

    Private Function AccountPosting() As Boolean


    End Function




    Private Sub Topctrl1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Topctrl1.Load

    End Sub

    Private Sub BtnCopyFrom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCopyFrom.Click
        If TxtCopyFrom.Text <> "" Then
            If MsgBox("Sure to Copy From Selected Value?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                MoveRecLine(TxtCopyFrom.AgSelectedValue)
            End If
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

End Class
