

Imports System.Data.SQLite
Public Class FrmReportTool
    Dim AgCL As New AgControls.AgLib
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Public AllowUserReports As Boolean = True
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable
    Dim mQry As String = "", mSearchCode As String = ""
    Private Const Col_SNo As Byte = 0

    Public WithEvents DGL1 As New AgControls.AgDataGrid

    Public WithEvents DGL2 As New AgControls.AgDataGrid
    Private Const Col2Row1 As Byte = 1
    Private Const Col2Row2 As Byte = 2
    Private Const Col2Row3 As Byte = 3

    Public WithEvents DGL3 As New AgControls.AgDataGrid
    Private Const Col3Fld_Name As Byte = 1
    Private Const Col3Cond_Operator As Byte = 2
    Private Const Col3Value As Byte = 3
    Private Const Col3Condition As Byte = 4

    Public WithEvents DGL4 As New AgControls.AgDataGrid
    Private Const Col4Fld_Name As Byte = 1
    Private Const Col4PrintYn As Byte = 2
    Private Const Col4Aggregate_Function As Byte = 3
    Private Const Col4ColumnWidth As Byte = 4
    Private Const Col4Group As Byte = 5
    Private Const Col4WrapText As Byte = 6
    Private Const Col4LineDetail As Byte = 7
    Private Const Col4Fld_DataType As Byte = 8

    Public WithEvents DGL5 As New AgControls.AgDataGrid                 'DATA Order By Grid
    Private Const Col5Fld_Name As Byte = 1
    Private Const Col5Order As Byte = 2

    Public WithEvents DglDataGroups As New AgControls.AgDataGrid
    Private Const ColDataGroups_Fld_Name As Byte = 1
    Private Const ColDataGroups_PrintSubTotal As Byte = 2

    Dim myQryColums As String = ""



    Public Sub New()        
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, "AEDP", Nothing)
        Topctrl1.SetDisp(True)
    End Sub


    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        'AgL.FPaintForm(Me, e, 0)
    End Sub
    Private Sub Form_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        DTMaster = Nothing
    End Sub


    Private Sub IniGrid()

        Dgl5.Height = Pnl5.Height
        Dgl5.Width = Pnl5.Width
        Dgl5.Top = Pnl5.Top
        Dgl5.Left = Pnl5.Left
        Pnl5.Visible = False
        GrpBoxDataSorting.Controls.Add(DGL5)
        DGL5.Visible = True
        DGL5.BringToFront()
        With AgCL
            .AddAgTextColumn(DGL5, "Dgl5SNo", 40, 5, "S.No.", , True)
            .AddAgTextColumn(DGL5, "Dgl5Row1", 150, 50, "Field Name")
            .AddAgListColumn(DGL5, "Ascending,Descending", "Dgl5Order", 100, "1,0", "Order")
        End With
        DGL5.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        AgL.FSetSNo(DGL5, Col_SNo)
        DGL5.TabIndex = Pnl5.TabIndex
        Dgl5.RowHeadersVisible = False
        DGL5.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Arial"), 9)
        DGL5.DefaultCellStyle.Font = New Font(New FontFamily("Arial"), 8)


        dgl2.Height = pnl2.Height
        dgl2.Width = pnl2.Width
        dgl2.Top = pnl2.Top
        dgl2.Left = pnl2.Left
        Pnl2.Visible = False
        GrpBoxPrintSettings.Controls.Add(DGL2)
        DGL2.Visible = True
        DGL2.BringToFront()
        With AgCL
            .AddAgTextColumn(DGL2, "dgl2SNo", 40, 5, "S.No.", , True)
            .AddAgTextColumn(DGL2, "dgl2Row1", 150, 50, "Row 1")
            .AddAgTextColumn(DGL2, "dgl2Row2", 150, 50, "Row 2")
            .AddAgTextColumn(DGL2, "dgl2Row3", 150, 50, "Row 3")
        End With
        DGL2.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        AgL.FSetSNo(DGL2, Col_SNo)
        DGL2.TabIndex = Pnl2.TabIndex
        DGL2.RowHeadersVisible = False
        DGL2.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Arial"), 9)
        DGL2.DefaultCellStyle.Font = New Font(New FontFamily("Arial"), 8)



        DGL3.Height = Pnl3.Height
        DGL3.Width = Pnl3.Width
        DGL3.Top = Pnl3.Top
        DGL3.Left = Pnl3.Left
        Pnl3.Visible = False
        GrpBoxReportCriteria.Controls.Add(DGL3)
        DGL3.Visible = True
        DGL3.BringToFront()
        With AgCL
            .AddAgTextColumn(DGL3, "DGL3SNo", 40, 5, "S.No.", , True)
            .AddAgTextColumn(DGL3, "DGL3Fld_Name", 150, 50, "Fld_Name")
            mQry = "Select Code As Code, Description As Name From Report_ConditionalOperators " & _
                "  Order By Description"
            .AddAgTextColumn(DGL3, "DGL3Cond_Operator", 100, 100, "Operator")
            DGL3.AgHelpDataSet(Col3Cond_Operator) = AgL.FillData(mQry, AgL.GCn)
            .AddAgTextColumn(DGL3, "DGL3Value", 150, 50, "Value")
        End With
        DGL3.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        AgL.FSetSNo(DGL3, Col_SNo)
        DGL3.TabIndex = Pnl3.TabIndex
        DGL3.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Arial"), 9)
        DGL3.DefaultCellStyle.Font = New Font(New FontFamily("Arial"), 8)


        DGL4.Height = Pnl4.Height
        DGL4.Width = Pnl4.Width
        DGL4.Top = Pnl4.Top
        DGL4.Left = Pnl4.Left
        Pnl4.Visible = False
        GrpBoxColumnSelection.Controls.Add(DGL4)
        DGL4.Visible = True
        DGL4.BringToFront()

        With AgCL
            .AddAgTextColumn(DGL4, "DGL4SNo", 40, 5, "S.No.", True, True, False)
            .AddAgTextColumn(DGL4, "DGL4Fld_Name", 150, 50, "Fld_Name", True, False, False)
            .AddAgCheckBoxColumn(DGL4, "DGL4PrintYn", 60, "Print Y/N", True, False, False)
            .AddAgTextColumn(DGL4, "DGL4AgreagateFunction", 100, 100, "Grand Total", True, False, False, False)
            mQry = "Select Description As Code, Description As Name From Report_AggregateFunctions " & _
                   "Order By Description"
            DGL4.AgHelpDataSet(Col4Aggregate_Function) = AgL.FillData(mQry, AgL.GCn)
            .AddAgNumberColumn(DGL4, "DGL4ColumnWidth", 100, 3, 0, False, "ColumnWidth", True, False, True)
            .AddAgCheckBoxColumn(DGL4, "DGL4Group", 40, "", False)
            .AddAgCheckBoxColumn(DGL4, "DGL4WrapText", 80, "Wrap Text", True, False, False)
            .AddAgCheckBoxColumn(DGL4, "DGL4LineDetail", 80, "LineDetail", True, False, False)
            .AddAgTextColumn(DGL4, "DGL4Fld_DataType", 150, 50, "Data Type", False, False, False)
        End With

        DGL4.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        AgL.FSetSNo(DGL4, Col_SNo)
        DGL4.TabIndex = Pnl4.TabIndex
        DGL4.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Arial"), 9)
        DGL4.DefaultCellStyle.Font = New Font(New FontFamily("Arial"), 8)


        DgOutput.ReadOnly = True

    End Sub
    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Or e.KeyCode = Keys.F3 Or e.KeyCode = Keys.F4 Or e.KeyCode = (Keys.F And e.Control) Or e.KeyCode = (Keys.P And e.Control) _
        Or e.KeyCode = (Keys.S And e.Control) Or e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F5 Or e.KeyCode = Keys.F10 _
        Or e.KeyCode = Keys.Home Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Or e.KeyCode = Keys.End Then
            Topctrl1.TopKey_Down(e)
        End If

        If Me.ActiveControl IsNot Nothing Then
            If Me.ActiveControl.Name <> Topctrl1.Name And
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
        Dim DtTemp As DataTable
        Try
            If AgL Is Nothing Then
                If FOpenIni(StrPath + IniName, "SA", "") Then
                    '
                    DtTemp = AgL.FillData("Select IfNull(Comp_Code,'') From AgReports_Enviro", AgL.ECompConn).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        FOpenConnection(DtTemp.Rows(0)(0))
                        mQry = "Select Report_Name, Qry From Agreports_LastReport"
                        DtTemp = AgL.FillData(mQry, AgL.ECompConn).Tables(0)
                        mQry = AgL.XNull(DtTemp.Rows(0)("Qry"))
                        AgReport_Name = AgL.XNull(DtTemp.Rows(0)("Report_Name"))
                    Else
                        AgReport_Name = "Vehicle Hire Challan"
                        AgL.PubDBName = "Suvidha"
                        PubReportDataPath = "AgReports"
                        FOpenConnection("1")
                        mQry = "SELECT Vhc.Site_Code, S.Name AS [Site/Branch], Vhc.V_Prefix+'/'+ Cast(Vhc.V_No as nVarchar) AS Challan_VNo, Replace(Convert(VARCHAR,Vhc.V_Date,106),' ','/') AS Challan_VDate, Vhc.ChallanNo, Replace(Convert(VARCHAR,Vhc.ChallanDate,106),' ','/') As [Challan Date],  Cf.CityName AS [From City], Ct.CityName AS [To City], Vhc.VehicleNo, SgV.Name AS [Vendor Name], SgV.Phone + ', ' + SgV.Mobile  AS ContactNo, SgV.Add1 AS Vendor_Address1, SgV.Add2 AS Vendor_Address2, SgV.Add3 AS Vendor_Address3, Cv.CityName AS Vendor_City,  Vhc.ChassisNo, Vhc.EngineNo, Vhc.Driver, Replace(Convert(VARCHAR,Vhc.PermitFromDt,106),' ','/') As PermitFromDt, Replace(Convert(VARCHAR,Vhc.PermitUpToDt,106),' ','/') As [Permit Upto Dt], Replace(Convert(VARCHAR,Vhc.InsuranceFromDt,106),' ','/') As [Insurance From Dt],  Vhc.InsuranceUpToDt, Vhc.FitnessFromDt, Vhc.FitnessUpToDt, Vhc.DriverDL_No, Vhc.DriverDL_ExpiryDt, Vhc.TotalWeight, Vhc.Freight,  Vhc.TdsPer, Vhc.TdsAmt, Vhc.SubTotal1, Vhc.Loading, Vhc.SubTotal2, Vhc.AdminCharge, Vhc.GrandTotal, Vhc.Advance, Vhc.SubTotal3, Vhc.UnLoading,  Vhc.OtherCharge, Vhc.Deduction, Vhc.NetAmount   , 'For Site/Branch : All' As SelGrid1, 'For Vendor : All' As SelGrid2, 'For VHC No : All' As SelGrid3,  'VHC VDate From ' + '01/Apr/2009' + ' To ' + '01/Oct/2009' As ForPeriod   FROM VehicleHireChallan Vhc  LEFT JOIN SiteMast S ON Vhc.Site_Code = S.Code  LEFT JOIN City Cf ON Vhc.FromCity = Cf.CityCode   LEFT JOIN City Ct ON vhc.ToCity =Ct.CityCode  LEFT JOIN SubGroup SgV ON vhc.VendorAc = SgV.SubCode  LEFT JOIN City Cv ON SgV.CityCode = Cv.CityCode  Where 1=1   And Vhc.V_Date Between '01/Apr/2009' And '01/Oct/2009'  And Vhc.Site_Code='1' "

                        'End
                    End If
                End If

                AgReportQuery = Replace(mQry, "`", "'")


            End If

            If AllowUserReports Then
                GroupBox3.Visible = True
                GroupBox2.Visible = True
            Else
                GroupBox3.Visible = False
                GroupBox2.Visible = False
            End If


            TxtQuery.Text = AgReportQuery
            CboDescription.Text = AgReport_Name


            AgL.WinSetting(Me, 650, 1000, 0, 0)
            AgL.GridDesign(DGL3)
            AgL.GridDesign(DGL4)

            IniGrid()
            FIniMaster()
            Ini_List()
            DispText()

            ProcessReport(mSearchCode, AgL.GCn)
            If DTMaster.Rows.Count = 0 Then
                ProcessReport(mSearchCode, AgL.GCn)
                SaveRecord()
            Else
                MoveRec()
                ProcessReport(mSearchCode, AgL.GCn)
            End If

            OptSystemReport.Checked = True
            OptDetailedReport.Checked = True

            If TxtQuery.Text <> "" Then Call Show_Report()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub FIniMaster(Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 1, Optional ByVal UserReportCode As String = "")

        If OptUserReport.Checked Then
            mQry = "Select Report_User.Code As SearchCode " &
                   " From Report_User Where 1=1 "

            If UserReportCode <> "" Then
                mQry = mQry + " And Code = '" & UserReportCode & "' And Report_Main = '" & CboDescription.SelectedValue & "' "
            Else
                mQry = mQry + " And Description = '" & TxtUserReport.Text & "' And Report_Main = '" & CboDescription.SelectedValue & "' "
            End If

            'Topctrl1.FIniForm(DTMaster, Agl.Gcn, mQry, , , , , BytDel, BytRefresh)
            DTMaster = AgL.FillData(mQry, AgL.GCn).Tables(0)
        Else
            mQry = "Select Report_Main.Code As SearchCode " &
            " From Report_Main Where Description = '" & CboDescription.Text & "' "
            Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
        End If
    End Sub


    Sub Ini_List()
        mQry = "Select Code  As Code, Description As Name From Report_Main " &
            "  Order By Description"
        AgCL.IniAgHelpList(AgL.GCn, CboDescription, mQry, "Name", "Code")

    End Sub

    Private Sub Topctrl1_PaddingChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Topctrl1.PaddingChanged

    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        BlankText()
        DispText()
        CboDescription.Focus()
    End Sub
    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New SqlClient.SqlCommand
        Dim MastPos As Long
        Dim mTrans As Boolean = False


        Try
            MastPos = BMBMaster.Position


            If DTMaster.Rows.Count > 0 Then
                If MsgBox("Are You Sure To Delete This Record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = vbYes Then


                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = True
                    AgL.Dman_ExecuteNonQry("Delete From Report_Main Where Code='" & mSearchCode & "'", AgL.GCn, AgL.ECmd)


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
            MsgBox(Ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub
    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        FIniMaster(0, 0)
        Topctrl1.Focus()
    End Sub


    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        DispText()
        CboDescription.Focus()
    End Sub


    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox("No Records To Search.", vbInformation) : Exit Sub
        Try


            AgL.PubFindQry = "Select  Report_Main.Code As SearchCode,  Report_Main.Description As [Description],  Report_Main.Query As [Query],  Report_Main.WhereClause As [WhereClause]  From  Report_Main "
            AgL.PubFindQryOrdBy = "[Description]"

            '*************** common code start *****************
            Dim Frmbj As AgLibrary.FrmFind = New AgLibrary.FrmFind(AgL.PubFindQry, Me.Text & " Find", AgL)
            Frmbj.ShowDialog()
            AgL.PubSearchRow = Frmbj.DGL1.Item(0, Frmbj.DGL1.CurrentRow.Index).Value.ToString
            If AgL.PubSearchRow <> "" Then
                AgL.PubDRFound = DTMaster.Rows.Find(AgL.PubSearchRow)
                BMBMaster.Position = DTMaster.Rows.IndexOf(AgL.PubDRFound)
                MoveRec()
            End If

            'AgL.PubObjFrmFind = New AgLibrary.frmFind(AgL)
            'AgL.PubObjFrmFind.ShowDialog()
            'AgL.PubObjFrmFind = Nothing
            'If AgL.PubSearchRow <> "" Then
            '    AgL.PubDRFound = DTMaster.Rows.Find(AgL.PubSearchRow)
            '    BMBMaster.Position = DTMaster.Rows.IndexOf(AgL.PubDRFound)
            '    MoveRec()
            'End If
            '*************** common code end  *****************
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub


    Private Sub Topctrl1_tbRef() Handles Topctrl1.tbRef
        Ini_List()
    End Sub


    Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn
        Dim mPrnHnd As New ClsPrint
        Dim GCnCmd As New SqlClient.SqlCommand
        Dim ds As New DataSet
        Dim strQry As String = ""
        Dim strCondition As String = ""
        Dim arrGrpField() As Boolean
        Dim arrColumnOrder() As Integer
        Dim arrWrapText() As Boolean
        Dim i As Integer, j As Integer


        Try
            Me.Cursor = Cursors.WaitCursor
            AgL.PubReportTitle = "Addition Deduction Master"
            If Not DTMaster.Rows.Count > 0 Then
                MsgBox("No Records Found to Print!!!", vbInformation, "Information")
                Me.Cursor = Cursors.Default
                Exit Sub
            End If


            If DgOutput.DataSource Is Nothing Then MsgBox("No Records to Print") : Exit Sub


            ReDim arrColumnOrder(DgOutput.ColumnCount)
            For i = 0 To DgOutput.ColumnCount - 1
                For j = 0 To DgOutput.ColumnCount - 1
                    If DgOutput.Columns(j).DisplayIndex = i Then
                        arrColumnOrder(i) = j
                        Exit For
                    End If
                Next
            Next


            ReDim arrGrpField(DgOutput.ColumnCount)
            ReDim arrWrapText(DgOutput.ColumnCount)
            For i = 0 To DGL4.RowCount - 1
                For j = 0 To DgOutput.ColumnCount - 1
                    If AgL.StrCmp(DGL4.Item(Col4Fld_Name, i).Value, "[" & DgOutput.Columns(j).HeaderText & "]") Then
                        DgOutput.Columns(j).Tag = Val(DGL4.Item(Col4ColumnWidth, i).Value)
                        If DGL4.Item(Col4WrapText, i).Value Then
                            arrWrapText(j) = True
                        End If
                    End If
                Next
            Next

            For i = 0 To DGL1.ColumnCount - 1
                If DGL1.Item(i, 0).Value <> "" Then
                    arrGrpField(i) = True
                End If
            Next

            ds = Nothing
            mPrnHnd.arrWrapText = arrWrapText
            mPrnHnd.arrColumnOrder = arrColumnOrder
            mPrnHnd.arrGrpField = arrGrpField
            mPrnHnd.myDataGrid = DgOutput
            mPrnHnd.ReportTitle = "Addition Deduction Master"
            mPrnHnd.TableIndex = 0
            mPrnHnd.PageSetupDialog(True)
            mPrnHnd.PrintPreview()
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
        Me.Cursor = Cursors.Default
    End Sub



    Sub SaveRecord()
        Dim MastPos As Long
        Dim I As Integer
        Dim mTrans As Boolean = False
        Try
            MastPos = BMBMaster.Position


            If AgL.RequiredField(CboDescription) Then Exit Sub
            If AgL.RequiredField(TxtQuery) Then Exit Sub

            If DTMaster.Rows.Count > 0 Then
                Topctrl1.Mode = "Edit"
            Else
                Topctrl1.Mode = "Add"
            End If
            If Topctrl1.Mode = "Add" Then
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From Report_Main Where Description='" & CboDescription.Text & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Description Already Exist!") : CboDescription.Focus() : Exit Sub

                mSearchCode = AgL.GetMaxId("Report_Main", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 5)
            Else
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From Report_Main Where Description='" & CboDescription.Text & "' And Code<>'" & mSearchCode & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Description Already Exist!") : CboDescription.Focus() : Exit Sub
            End If



            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True


            If Topctrl1.Mode = "Add" Then
                mQry = "Insert Into Report_Main (Code, Description, Query, Div_Code, Site_Code, U_EntDt, U_Name, U_AE) Values('" & mSearchCode & "', " & AgL.Chk_Text(CboDescription.Text) & ", " & AgL.Chk_Text(TxtQuery.Text) & ",  '" & AgL.PubDivCode & "', '" & AgL.PubSiteCode & "', '" & Format(AgL.PubLoginDate, "Short Date") & "', '" & AgL.PubUserName & "', 'A') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                mQry = "Update Report_Main Set Description = " & AgL.Chk_Text(CboDescription.Text) & ", Query = " & AgL.Chk_Text(TxtQuery.Text) & ", Edit_Date='" & Format(AgL.PubLoginDate, "Short Date") & "', Edit_By = '" & AgL.PubUserName & "', U_AE = 'E' Where Code='" & mSearchCode & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            mQry = "Delete From Report_Condition Where Code = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            With DGL3
                For I = 0 To .Rows.Count - 1
                    If .Item(Col3Fld_Name, I).Value <> "" Then
                        mQry = "Insert Into Report_Condition ( Code, Sr, Fld_Name, Cond_Operator, Value, Div_Code, Site_Code, U_EntDt, U_Name, U_AE) Values('" & mSearchCode & "', " & I + 1 & "," & AgL.Chk_Text(.Item(Col3Fld_Name, I).Value) & ", " & AgL.Chk_Text(.Item(Col3Cond_Operator, I).Tag) & ", " & AgL.Chk_Text(.Item(Col3Value, I).Value) & ", '" & AgL.PubDivCode & "', '" & AgL.PubSiteCode & "', '" & Format(AgL.PubLoginDate, "Short Date") & "', '" & AgL.PubUserName & "','" & AgL.MidStr(Topctrl1.Mode, 0, 1) & "') "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                Next I
            End With

            mQry = "Delete From Report_Fields Where Code = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)



            With DGL4
                For I = 0 To .Rows.Count - 1
                    If .Item(Col4Fld_Name, I).Value <> "" Then
                        mQry = "Insert Into Report_Fields ( Code, Sr, Fld_Name, PrintYn, WrapText, LineDetail, GrandTotal, ColumnWidth, DataType, U_EntDt, U_Name, U_AE) Values('" & mSearchCode & "', " & I + 1 & "," & AgL.Chk_Text(.Item(Col4Fld_Name, I).Value) & ", " & Val(.Item(Col4PrintYn, I).Value) & ", " & Val(.Item(Col4WrapText, I).Value) & ", " & Val(.Item(Col4LineDetail, I).Value) & ", " & AgL.Chk_Text(.Item(Col4Aggregate_Function, I).Value) & ", " & Val(.Item(Col4ColumnWidth, I).Value) & ", " & AgL.Chk_Text(.Item(Col4Fld_DataType, I).Value) & ", '" & Format(AgL.PubLoginDate, "Short Date") & "', '" & AgL.PubUserName & "','" & AgL.MidStr(Topctrl1.Mode, 0, 1) & "') "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                Next I
            End With


            If DgOutput.DataSource IsNot Nothing Then
                mQry = "Update Report_Fields Set Sr = " & DGL4.RowCount & " Where Code = '" & mSearchCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                For I = 2 To DgOutput.ColumnCount - 1
                    mQry = "Update Report_Fields Set Sr = " & DgOutput.Columns(I).DisplayIndex & " Where Code = '" & mSearchCode & "' and Fld_Name = '[" & DgOutput.Columns(I).HeaderText & "]' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                Next
            End If

            mQry = "Delete From Report_Group Where Code = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            With DGL1
                For I = 0 To .ColumnCount - 1
                    If .Item(I, 0).Value <> "" Then
                        mQry = "Insert Into Report_Group (Code, Sr, Fld_Name, Asc_Desc, SubTotalYn, U_EntDt, U_Name, U_AE) Values('" & mSearchCode & "', " & I + 1 & "," & AgL.Chk_Text(.Item(I, 0).Value) & ", 1, 1, '" & Format(AgL.PubLoginDate, "Short Date") & "', '" & AgL.PubUserName & "','" & AgL.MidStr(Topctrl1.Mode, 0, 1) & "') "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                Next I
            End With


            mQry = "Delete From Report_PrintSettings Where Code = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            With DGL2
                For I = 0 To .Rows.Count - 1
                    If .Item(Col2Row1, I).Value <> "" Then
                        mQry = "Insert Into Report_PrintSettings ( Code, Sr, Row1, Row2, Row3) Values('" & mSearchCode & "', " & I + 1 & "," & AgL.Chk_Text(.Item(Col2Row1, I).Value) & ", " & AgL.Chk_Text(.Item(Col2Row2, I).Value) & ", " & AgL.Chk_Text(.Item(Col2Row3, I).Value) & ") "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                Next I
            End With

            mQry = "Delete From Report_Sort Where Code = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            With DGL5
                For I = 0 To .Rows.Count - 1
                    If .Item(Col5Fld_Name, I).Value <> "" Then
                        mQry = "Insert Into Report_Sort ( Code, Sr, Fld_Name, OrderType) Values('" & mSearchCode & "', " & I + 1 & "," & AgL.Chk_Text(.Item(Col5Fld_Name, I).Value) & "," & Val(IIf(.AgSelectedValue(Col5Order, I) = "", 1, .AgSelectedValue(Col5Order, I))) & ") "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                Next I
            End With



            'Call AgL.LogTableEntry(mSearchCode, Me.Text, AgL.MidStr(Topctrl1.Mode, 0, 1), AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, Agl.Gcn, AgL.ECmd)

            AgL.ETrans.Commit()
            mTrans = False
            MsgBox("Saved Successfully")
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

    Sub SaveRecord_User()
        Dim MastPos As Long
        Dim I As Integer
        Dim mSearchCodeUser$
        Dim mTrans As Boolean = False
        Try
            MastPos = BMBMaster.Position


            If AgL.RequiredField(TxtUserReport) Then Exit Sub


            If DTMaster.Rows.Count > 0 Then
                Topctrl1.Mode = "Edit"
            Else
                Topctrl1.Mode = "Add"
            End If
            If Topctrl1.Mode = "Add" Then
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From Report_User Where Description='" & TxtUserReport.Text & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Description Already Exist!") : TxtUserReport.Focus() : Exit Sub

                mSearchCodeUser = GetMaxId("Report_User", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 5)
            Else
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From Report_User Where Description='" & TxtUserReport.Text & "' And Code<>'" & TxtUserReport.AgSelectedValue & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Description Already Exist!") : TxtUserReport.Focus() : Exit Sub

                mSearchCodeUser = TxtUserReport.AgSelectedValue
            End If


            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True


            If Topctrl1.Mode = "Add" Then
                mQry = "Insert Into Report_User(Code, Description, Report_Main, Div_Code, Site_Code, U_EntDt, U_Name, U_AE) Values('" & mSearchCodeUser & "', " & AgL.Chk_Text(TxtUserReport.Text) & ", " & AgL.Chk_Text(CboDescription.SelectedValue) & ",  '" & AgL.PubDivCode & "', '" & AgL.PubSiteCode & "', '" & Format(AgL.PubLoginDate, "Short Date") & "', '" & AgL.PubUserName & "', 'A') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                mQry = "Update Report_User Set Description = " & AgL.Chk_Text(TxtUserReport.Text) & ", Report_Main = " & AgL.Chk_Text(CboDescription.SelectedValue) & ", Edit_Date='" & Format(AgL.PubLoginDate, "Short Date") & "', Edit_By = '" & AgL.PubUserName & "', U_AE = 'E' Where Code='" & mSearchCodeUser & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If


            mQry = "Delete From Report_Condition Where Code = '" & mSearchCodeUser & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            With DGL3
                For I = 0 To .Rows.Count - 1
                    If .Item(Col3Fld_Name, I).Value <> "" Then
                        mQry = "Insert Into Report_Condition ( Code, Sr, Fld_Name, Cond_Operator, Value, Div_Code, Site_Code, U_EntDt, U_Name, U_AE) Values('" & mSearchCodeUser & "', " & I + 1 & "," & AgL.Chk_Text(.Item(Col3Fld_Name, I).Value) & ", " & AgL.Chk_Text(.Item(Col3Cond_Operator, I).Tag) & ", " & AgL.Chk_Text(.Item(Col3Value, I).Value) & ", '" & AgL.PubDivCode & "', '" & AgL.PubSiteCode & "', '" & Format(AgL.PubLoginDate, "Short Date") & "', '" & AgL.PubUserName & "','" & AgL.MidStr(Topctrl1.Mode, 0, 1) & "') "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                Next I
            End With

            mQry = "Delete From Report_Fields Where Code = '" & mSearchCodeUser & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            With DGL4
                For I = 0 To .Rows.Count - 1
                    If .Item(Col4Fld_Name, I).Value <> "" Then
                        mQry = "Insert Into Report_Fields ( Code, Sr, Fld_Name, PrintYn, WrapText, LineDetail, GrandTotal, ColumnWidth, DataType, U_EntDt, U_Name, U_AE) Values('" & mSearchCodeUser & "', " & I + 1 & "," & AgL.Chk_Text(.Item(Col4Fld_Name, I).Value) & ", " & Val(.Item(Col4PrintYn, I).Value) & ", " & Val(.Item(Col4WrapText, I).Value) & ", " & Val(.Item(Col4LineDetail, I).Value) & ", " & AgL.Chk_Text(.Item(Col4Aggregate_Function, I).Value) & ", " & Val(.Item(Col4ColumnWidth, I).Value) & ", " & AgL.Chk_Text(.Item(Col4Fld_DataType, I).Value) & ", '" & Format(AgL.PubLoginDate, "Short Date") & "', '" & AgL.PubUserName & "','" & AgL.MidStr(Topctrl1.Mode, 0, 1) & "') "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                Next I
            End With

            If DgOutput.DataSource IsNot Nothing Then
                mQry = "Update Report_Fields Set Sr = " & DGL4.RowCount & " Where Code = '" & mSearchCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                For I = 2 To DgOutput.ColumnCount - 1
                    mQry = "Update Report_Fields Set Sr = " & DgOutput.Columns(I).DisplayIndex & " Where Code = '" & mSearchCode & "' and Fld_Name = '[" & DgOutput.Columns(I).HeaderText & "]' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                Next
            End If



            mQry = "Delete From Report_Group Where Code = '" & mSearchCodeUser & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            With DGL1
                For I = 0 To .ColumnCount - 1
                    If .Item(I, 0).Value <> "" Then
                        mQry = "Insert Into Report_Group (Code, Sr, Fld_Name, Asc_Desc, SubTotalYn, U_EntDt, U_Name, U_AE) Values('" & mSearchCodeUser & "', " & I + 1 & "," & AgL.Chk_Text(.Item(I, 0).Value) & ", 1, 1, '" & Format(AgL.PubLoginDate, "Short Date") & "', '" & AgL.PubUserName & "','" & AgL.MidStr(Topctrl1.Mode, 0, 1) & "') "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                Next I
            End With

            mQry = "Delete From Report_PrintSettings Where Code = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            With DGL2
                For I = 0 To .Rows.Count - 1
                    If .Item(Col2Row1, I).Value <> "" Then
                        mQry = "Insert Into Report_PrintSettings ( Code, Sr, Row1, Row2, Row3) Values('" & mSearchCodeUser & "', " & I + 1 & "," & AgL.Chk_Text(.Item(Col2Row1, I).Value) & ", " & AgL.Chk_Text(.Item(Col2Row2, I).Value) & ", " & AgL.Chk_Text(.Item(Col2Row3, I).Value) & ") "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                Next I
            End With

            mQry = "Delete From Report_Sort Where Code = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            With DGL5
                For I = 0 To .Rows.Count - 1
                    If .Item(Col5Fld_Name, I).Value <> "" Then
                        mQry = "Insert Into Report_Sort ( Code, Sr, Fld_Name, OrderType) Values('" & mSearchCodeUser & "', " & I + 1 & "," & AgL.Chk_Text(.Item(Col5Fld_Name, I).Value) & "," & Val(IIf(.AgSelectedValue(Col5Order, I) = "", 1, .AgSelectedValue(Col5Order, I))) & ") "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                Next I
            End With

            AgL.ETrans.Commit()
            mTrans = False
            MsgBox("Saved Successfully")
            FIniMaster()
            Topctrl1_tbRef()
            MoveRec_User(mSearchCodeUser)
            If Topctrl1.Mode = "Add" Then
                Topctrl1.LblDocId.Text = mSearchCodeUser
                Topctrl1.FButtonClick(0)
            Else
                Topctrl1.SetDisp(True)
                MoveRec()
            End If
        Catch ex As Exception
            If mTrans = True Then AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub Topctrl1_tbSave() Handles Topctrl1.tbSave
    End Sub

    Sub Print()
        Dim mPrnHnd As New ClsPrint
        Dim GCnCmd As New SqlClient.SqlCommand
        Dim ds As New DataSet
        Dim strQry As String = ""
        Dim strCondition As String = ""
        Dim arrGrpField() As Boolean
        Dim arrGrpFieldName() As String
        Dim arrColumnOrder() As Integer
        Dim arrWrapText() As Boolean
        Dim i As Integer, j As Integer
        Dim arrRow1Column() As String
        Dim arrRow2Column() As String
        Dim arrRow3Column() As String
        Dim Row1ColumnCount As Integer, Row2ColumnCount As Integer, Row3ColumnCount As Integer
        Dim arrColumnMaxWidth() As Integer
        Dim arrHeaderColumn() As String
        Dim arrLineDetailColumn() As String


        Try






            Me.Cursor = Cursors.WaitCursor
            AgL.PubReportTitle = "Addition Deduction Master"
            If Not DTMaster.Rows.Count > 0 Then
                MsgBox("No Records Found to Print!!!", vbInformation, "Information")
                Me.Cursor = Cursors.Default
                Exit Sub
            End If


            If DgOutput.DataSource Is Nothing Then MsgBox("No Records to Print") : Exit Sub


            ReDim arrColumnOrder(DgOutput.ColumnCount)
            For i = 0 To DgOutput.ColumnCount - 1
                For j = 0 To DgOutput.ColumnCount - 1
                    If DgOutput.Columns(j).DisplayIndex = i Then
                        arrColumnOrder(i) = j
                        Exit For
                    End If
                Next
            Next


            ReDim arrGrpField(DgOutput.ColumnCount)
            ReDim arrWrapText(DgOutput.ColumnCount)
            For i = 0 To DGL4.RowCount - 1
                For j = 0 To DgOutput.ColumnCount - 1
                    If AgL.StrCmp(DGL4.Item(Col4Fld_Name, i).Value, "[" & DgOutput.Columns(j).HeaderText & "]") Then
                        DgOutput.Columns(j).Tag = Val(DGL4.Item(Col4ColumnWidth, i).Value)
                        If CBool(AgL.VNull(DGL4.Item(Col4WrapText, i).Value)) Then
                            arrWrapText(j) = True
                        End If
                    End If
                Next
            Next

            For i = 0 To DGL1.ColumnCount - 1
                If DGL1.Item(i, 0).Value <> "" Then
                    arrGrpField(i) = True
                End If
            Next

            ReDim arrGrpFieldName(-1)
            For i = 0 To DGL1.ColumnCount - 1
                If DGL1.Item(i, 0).Value <> "" Then
                    If IsNothing(arrGrpFieldName) Then
                        ReDim arrGrpFieldName(0)
                    Else
                        ReDim Preserve arrGrpFieldName(UBound(arrGrpFieldName) + 1)
                    End If

                    arrGrpFieldName(i) = Replace(Replace(AgL.XNull(DGL1.Item(i, 0).Value), "[", ""), "]", "")
                Else
                    Exit For
                End If
            Next

            ReDim arrLineDetailColumn(-1)
            ReDim arrHeaderColumn(-1)
            For i = 0 To DGL4.RowCount - 1
                If DGL4.Item(Col4Fld_Name, i).Value <> "" Then
                    If AgL.VNull(DGL4.Item(Col4LineDetail, i).Value) Then
                        If IsNothing(arrLineDetailColumn) Then
                            ReDim arrLineDetailColumn(0)
                        Else
                            ReDim Preserve arrLineDetailColumn(UBound(arrLineDetailColumn) + 1)
                        End If

                        arrLineDetailColumn(UBound(arrLineDetailColumn)) = Replace(Replace(AgL.XNull(DGL4.Item(Col4Fld_Name, i).Value), "[", ""), "]", "")
                    Else
                        If IsNothing(arrHeaderColumn) Then
                            ReDim arrHeaderColumn(0)
                        Else
                            ReDim Preserve arrHeaderColumn(UBound(arrHeaderColumn) + 1)
                        End If

                        arrHeaderColumn(UBound(arrHeaderColumn)) = Replace(Replace(AgL.XNull(DGL4.Item(Col4Fld_Name, i).Value), "[", ""), "]", "")
                    End If
                Else
                    Exit For
                End If
            Next


            For i = 0 To DgOutput.Columns.Count - 1
                DgOutput.Columns(i).Tag = DgOutput.Columns(i).Width
            Next


            With DGL2
                ReDim arrRow1Column(.RowCount - 2) : ReDim arrRow2Column(.RowCount - 2) : ReDim arrRow3Column(.RowCount - 2)
                ReDim arrColumnMaxWidth(.RowCount - 2)
                Row1ColumnCount = 0 : Row2ColumnCount = 0 : Row3ColumnCount = 0

                For i = 0 To .Rows.Count - 1
                    If .Item(Col2Row1, i).Value <> "" Then
                        arrRow1Column(i) = Replace(Replace(AgL.XNull(.Item(Col2Row1, i).Value), "[", ""), "]", "")
                        If arrRow1Column(i) <> "" Then Row1ColumnCount += 1
                        arrRow2Column(i) = Replace(Replace(AgL.XNull(.Item(Col2Row2, i).Value), "[", ""), "]", "")
                        If arrRow2Column(i) <> "" Then Row2ColumnCount += 1
                        arrRow3Column(i) = Replace(Replace(AgL.XNull(.Item(Col2Row3, i).Value), "[", ""), "]", "")
                        If arrRow3Column(i) <> "" Then Row3ColumnCount += 1
                        arrColumnMaxWidth(i) = Val(DgOutput.Columns(arrRow1Column(i)).Tag) * 1.4
                    End If
                Next
            End With


            ds = Nothing

            mPrnHnd.arrRow1Columns = arrRow1Column
            mPrnHnd.arrRow2Columns = arrRow2Column
            mPrnHnd.arrRow3Columns = arrRow3Column
            mPrnHnd.Row1ColumnCount = Row1ColumnCount
            mPrnHnd.Row2ColumnCount = Row2ColumnCount
            mPrnHnd.Row3ColumnCount = Row3ColumnCount
            mPrnHnd.arrColumnMaxWidth = arrColumnMaxWidth
            mPrnHnd.arrWrapText = arrWrapText
            mPrnHnd.arrColumnOrder = arrColumnOrder
            mPrnHnd.arrGrpField = arrGrpField
            mPrnHnd.arrGrpFieldName = arrGrpFieldName
            mPrnHnd.arrHeaderColumn = arrHeaderColumn
            mPrnHnd.arrLineDetailColumn = arrLineDetailColumn
            mPrnHnd.myDataGrid = DgOutput
            mPrnHnd.ReportTitle = Me.Text
            mPrnHnd.TableIndex = 0
            mPrnHnd.PageSetupDialog(True)
            mPrnHnd.PrintPreview()
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
        Me.Cursor = Cursors.Default

    End Sub

    Public Sub MoveRec()
        Dim DsTemp As DataSet = Nothing
        Dim MastPos As Long
        Dim I As Integer
        Try
            FClear()
            BlankText()
            If DTMaster.Rows.Count > 0 Then
                MastPos = BMBMaster.Position
                mSearchCode = DTMaster.Rows(MastPos)("SearchCode")
                mQry = "Select Report_Main.* " &
                    " From Report_Main Where Code='" & mSearchCode & "'"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    If .Rows.Count > 0 Then
                        CboDescription.SelectedValue = AgL.XNull(.Rows(0)("Code"))
                        Me.Text = CboDescription.Text
                        'TxtQuery.Text = AgL.XNull(.Rows(0)("Query"))
                    End If
                End With
                ProcessReport(mSearchCode, AgL.GCn)
                mQry = "Select [Code], [Sr], [Fld_Name], [PrintYn], [WrapText], [LineDetail],[Div_Code],[Site_Code],[U_Name],[U_EntDt],[U_AE],[Edit_Date],[Edit_By],[GrandTotal],[ColumnWidth] " &
                    " From Report_Fields  Where Code='" & mSearchCode & "' Order By Sr"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    DGL4.RowCount = 1
                    DGL4.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            DGL4.Rows.Add()
                            DGL4.Item(Col_SNo, I).Value = DGL4.Rows.Count - 1
                            DGL4.Item(Col4Fld_Name, I).Value = .Rows(I)("Fld_Name")
                            DGL4.Item(Col4Aggregate_Function, I).Value = .Rows(I)("GrandTotal")
                            DGL4.Item(Col4PrintYn, I).Value = .Rows(I)("PrintYn")
                            DGL4.Item(Col4WrapText, I).Value = .Rows(I)("WrapText")
                            DGL4.Item(Col4LineDetail, I).Value = AgL.VNull(.Rows(I)("LineDetail"))
                        Next I
                    End If
                End With
                DGL4.Refresh()

                mQry = "Select Report_Condition.* " &
                " From Report_Condition Where Code='" & mSearchCode & "'"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    DGL3.RowCount = 1
                    DGL3.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            DGL3.Rows.Add()
                            DGL3.Item(Col_SNo, I).Value = DGL3.Rows.Count - 1
                            DGL3.Item(Col3Fld_Name, I).Value = .Rows(I)("Fld_Name")
                            DGL3.AgSelectedValue(Col3Cond_Operator, I) = .Rows(I)("Cond_Operator")
                            DGL3.Item(Col3Value, I).Value = .Rows(I)("Value")
                        Next I
                    End If
                End With



                mQry = "Select Report_PrintSettings.* " &
                " From Report_PrintSettings Where Code='" & mSearchCode & "' Order By Sr "
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    DGL2.RowCount = 1
                    DGL2.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            DGL2.Rows.Add()
                            DGL2.Item(Col_SNo, I).Value = DGL2.Rows.Count - 1
                            DGL2.Item(Col2Row1, I).Value = AgL.XNull(.Rows(I)("Row1"))
                            DGL2.Item(Col2Row2, I).Value = AgL.XNull(.Rows(I)("Row2"))
                            DGL2.Item(Col2Row3, I).Value = AgL.XNull(.Rows(I)("Row3"))
                        Next I

                    End If
                End With



                mQry = "Select Report_Sort.* " &
                " From Report_Sort Where Code='" & mSearchCode & "' Order By Sr "
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    DGL5.RowCount = 1
                    DGL5.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            DGL5.Rows.Add()
                            DGL5.Item(Col_SNo, I).Value = DGL5.Rows.Count - 1
                            DGL5.Item(Col5Fld_Name, I).Value = AgL.XNull(.Rows(I)("Fld_Name"))
                            DGL5.AgSelectedValue(Col5Order, I) = IIf(AgL.XNull(.Rows(I)("OrderType")), "1", "0")
                        Next I
                    End If
                End With


                With AgCL
                    For I = 0 To DGL4.Rows.Count - 1
                        .AddAgTextColumn(DGL1, "DGL1Group" & I + 1, 120, 100, "Group " & I + 1 & " Name", , False)
                        'DGL1.AgHelpDataSet(I) = AgL.FillData()
                    Next
                End With
                DGL1.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
                'AgL.FSetSNo(DGL1, Col_SNo)
                DGL1.TabIndex = Pnl1.TabIndex
                DGL1.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Arial"), 9)
                DGL1.DefaultCellStyle.Font = New Font(New FontFamily("Arial"), 8)
                DGL1.ScrollBars = ScrollBars.Vertical



                'mQry = "Select RG.*, Rf.Sr As Fld_Index " & _
                '" From Report_Group Rg " & _
                '" Left Join Report_Fields Rf On Rg.Fld_Name = Rf.Fld_Name " & _
                '" Where Rg.Code='" & mSearchCode & "'"
                mQry = "Select RG.*, RG.Sr As Fld_Index " &
                " From Report_Group Rg " &
                " Where Rg.Code='" & mSearchCode & "' Order By Rg.Sr"

                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    'DGL3.RowCount = 1
                    DGL1.Rows.Clear()
                    DGL1.Rows.Add()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            'DGL3.Item(Col_SNo, I).Value = DGL3.Rows.Count - 1
                            DGL1.Item(I, 0).Value = .Rows(I)("Fld_Name")
                            DGL1.Item(I, 0).Tag = AgL.VNull(.Rows(I)("Fld_Index"))
                        Next I
                    End If
                End With

                'Fill_Fields()
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

    Public Sub MoveRec_User(ByVal SearchCodeUser As String)
        Dim DsTemp As DataSet = Nothing
        Dim I As Integer
        Try
            BlankTextUser()

            If DTMaster.Rows.Count > 0 Then
                ProcessReport(SearchCodeUser, AgL.GCn)

                mQry = "Select [Code] ,[Sr],[Fld_Name], PrintYn, WrapText, LineDetail, [Div_Code], [Site_Code],[U_Name],[U_EntDt],[U_AE],[Edit_Date],[Edit_By],[GrandTotal],[ColumnWidth] " &
                    " From Report_Fields Where Code='" & SearchCodeUser & "' Order By Sr"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    DGL4.RowCount = 1
                    DGL4.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            DGL4.Rows.Add()
                            DGL4.Item(Col_SNo, I).Value = DGL4.Rows.Count - 1
                            DGL4.Item(Col4Fld_Name, I).Value = .Rows(I)("Fld_Name")
                            DGL4.Item(Col4Aggregate_Function, I).Value = .Rows(I)("GrandTotal")
                            DGL4.Item(Col4PrintYn, I).Value = .Rows(I)("PrintYn")
                            DGL4.Item(Col4WrapText, I).Value = .Rows(I)("WrapText")
                            DGL4.Item(Col4LineDetail, I).Value = AgL.VNull(.Rows(I)("LineDetail"))
                        Next I
                    End If
                End With
                DGL4.Refresh()

                mQry = "Select Report_Condition.* " &
                " From Report_Condition Where Code='" & SearchCodeUser & "'"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    DGL3.RowCount = 1
                    DGL3.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            DGL3.Rows.Add()
                            DGL3.Item(Col_SNo, I).Value = DGL3.Rows.Count - 1
                            DGL3.Item(Col3Fld_Name, I).Value = .Rows(I)("Fld_Name")
                            DGL3.AgSelectedValue(Col3Cond_Operator, I) = .Rows(I)("Cond_Operator")
                            DGL3.Item(Col3Value, I).Value = .Rows(I)("Value")
                        Next I
                    End If
                End With

                DGL1.ColumnCount = 0
                With AgCL
                    For I = 0 To DGL4.Rows.Count - 1
                        .AddAgTextColumn(DGL1, "DGL1Group" & I + 1, 120, 100, "Group " & I + 1 & " Name", , False)
                        'DGL1.AgHelpDataSet(I) = AgL.FillData()
                    Next
                End With
                DGL1.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
                'AgL.FSetSNo(DGL1, Col_SNo)
                DGL1.TabIndex = Pnl1.TabIndex
                DGL1.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Arial"), 9)
                DGL1.DefaultCellStyle.Font = New Font(New FontFamily("Arial"), 8)
                DGL1.ScrollBars = ScrollBars.Vertical



                mQry = "Select RG.*, Rg.Sr As Fld_Index " &
                " From Report_Group Rg " &
                " Where Rg.Code='" & SearchCodeUser & "' Order By Rg.Sr "
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    'DGL3.RowCount = 1
                    DGL1.Rows.Clear()
                    DGL1.Rows.Add()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            If AgL.XNull(.Rows(I)("Fld_Name")) <> "" Then
                                'DGL3.Item(Col_SNo, I).Value = DGL3.Rows.Count - 1
                                DGL1.Item(I, 0).Value = .Rows(I)("Fld_Name")
                                DGL1.Item(I, 0).Tag = AgL.VNull(.Rows(I)("Fld_Index"))
                            End If
                        Next I
                    End If
                End With


                mQry = "Select Report_PrintSettings.* " &
                " From Report_PrintSettings Where Code='" & SearchCodeUser & "' Order By Sr "
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    DGL2.RowCount = 1
                    DGL2.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            DGL2.Rows.Add()
                            DGL2.Item(Col_SNo, I).Value = DGL2.Rows.Count - 1
                            DGL2.Item(Col2Row1, I).Value = .Rows(I)("Row1")
                            DGL2.Item(Col2Row2, I).Value = .Rows(I)("Row2")
                            DGL2.Item(Col2Row3, I).Value = .Rows(I)("Row3")
                        Next I
                    End If
                End With



                mQry = "Select Report_Sort.* " &
                " From Report_Sort Where Code='" & SearchCodeUser & "' Order By Sr "
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    DGL5.RowCount = 1
                    DGL5.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            DGL2.Rows.Add()
                            DGL2.Item(Col_SNo, I).Value = DGL2.Rows.Count - 1
                            DGL2.Item(Col5Fld_Name, I).Value = .Rows(I)("Fld_Name")
                            DGL5.AgSelectedValue(Col5Order, I) = IIf(AgL.XNull(.Rows(I)("OrderType")) = False, 0, 1)
                        Next I
                    End If
                End With



                'Fill_Fields()
            Else
                BlankTextUser()
            End If
            Topctrl1.FSetDispRec(BMBMaster)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            DsTemp = Nothing
        End Try
    End Sub


    Private Sub Fill_Fields()
        Dim DsTemp As DataSet
        Dim I As Integer

        mQry = "Select Report_Fields.* " &
            " From Report_Fields Where Code='" & mSearchCode & "' Order By Sr"
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            DGL4.RowCount = 1
            DGL4.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    DGL4.Rows.Add()
                    DGL4.Item(Col_SNo, I).Value = DGL4.Rows.Count - 1
                    DGL4.Item(Col4Fld_Name, I).Value = .Rows(I)("Fld_Name")
                    DGL4.Item(Col4PrintYn, I).Value = Str(AgL.VNull(.Rows(I)("PrintYn")))
                Next I
            End If
        End With
    End Sub

    Private Sub BlankText()
        'If Topctrl1.Mode <> "Add" Then Topctrl1.BlankTextBoxes()
        mSearchCode = ""
        DGL3.RowCount = 1
        DGL3.Rows.Clear()
        DGL4.RowCount = 1
        DGL4.Rows.Clear()
    End Sub

    Private Sub BlankTextUser()
        DGL3.RowCount = 1
        DGL3.Rows.Clear()
        DGL4.RowCount = 1
        DGL4.Rows.Clear()
    End Sub

    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
    End Sub

    Private Sub DGL_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGL3.CellEnter, DGL4.CellEnter
        With CType(sender, AgControls.AgDataGrid)
            Select Case sender.name
                Case DGL3.Name
                    Select Case sender.CurrentCell.ColumnIndex
                        Case Col3Fld_Name
                            mQry = "Select Fld_Name, Fld_Name As [Field Name] From Report_Fields_Temp Where Code = '" & mSearchCode & "'"
                            CType(sender, AgControls.AgDataGrid).AgHelpDataSet(sender.currentcell.columnindex, 0, GrpBoxReportCriteria.Top, GrpBoxReportCriteria.Left) = AgL.FillData(mQry, AgL.GCn)
                    End Select

                Case DGL4.Name
                    Select Case sender.CurrentCell.ColumnIndex
                        Case Col4Fld_Name
                            mQry = "Select Fld_Name, Fld_Name As [Field Name] From Report_Fields_Temp  Where Code = '" & mSearchCode & "'"
                            CType(sender, AgControls.AgDataGrid).AgHelpDataSet(sender.currentcell.columnindex, 0, GrpBoxColumnSelection.Top, GrpBoxColumnSelection.Left) = AgL.FillData(mQry, AgL.GCn)
                    End Select
            End Select
        End With
    End Sub

    Private Sub DGL3_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGL3.CellEnter
        With CType(sender, AgControls.AgDataGrid)
            Select Case sender.CurrentCell.ColumnIndex
                Case Col3Fld_Name
                    mQry = "Select Fld_Name, Fld_Name As [Field Name] From Report_Fields_Temp Where Code = '" & mSearchCode & "'"
                    CType(sender, AgControls.AgDataGrid).AgHelpDataSet(sender.currentcell.columnindex, 0, GrpBoxReportCriteria.Top, GrpBoxReportCriteria.Left) = AgL.FillData(mQry, AgL.GCn)
                Case Col3Value
                    If AgL.XNull(.Item(Col3Fld_Name, .CurrentCell.RowIndex).Value) <> "" Then
                        mQry = "Select Distinct " & .Item(Col3Fld_Name, .CurrentCell.RowIndex).Value & " as Code, " & .Item(Col3Fld_Name, .CurrentCell.RowIndex).Value & " As Name From (" & TxtQuery.Text & ") As X "
                        .AgHelpDataSet(.CurrentCell.ColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                    Else
                        .CurrentCell.ReadOnly = True
                    End If
            End Select
        End With
    End Sub

    Private Sub DGL1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGL1.CellEnter
        With CType(sender, AgControls.AgDataGrid)
            Select Case sender.CurrentCell.ColumnIndex
                Case Else
                    mQry = "Select Sr, Fld_Name As [Field Name] From Report_Fields_Temp  Where Code = '" & mSearchCode & "'"
                    CType(sender, AgControls.AgDataGrid).AgHelpDataSet(sender.currentcell.columnindex) = AgL.FillData(mQry, AgL.GCn)
            End Select
        End With
    End Sub


    Private Sub DGL_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGL3.KeyDown, DGL4.KeyDown
        If Topctrl1.Mode <> "Browse" Then
            If e.Control And e.KeyCode = Keys.D Then
                sender.CurrentRow.Selected = True
            End If
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub

        Try
            Select Case sender.CurrentCell.ColumnIndex
                'Case <Dgl_Column>
                '    <Executable Code>
            End Select
        Catch Ex As NullReferenceException
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub



    Private Sub DGL_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles DGL3.RowsAdded, DGL4.RowsAdded, DGL2.RowsAdded, DGL5.RowsAdded
        sender(Col_SNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub DGL_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles DGL3.RowsRemoved, DGL4.RowsRemoved, DGL2.RowsRemoved, DGL5.RowsRemoved
        Try
            DTStruct.Rows.Remove(DTStruct.Rows.Item(e.RowIndex))
        Catch ex As Exception
        End Try

        AgL.FSetSNo(sender, Col_SNo)
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

    Private Sub ProcessReport(ByVal mSearchCode As String, ByVal mConn As SQLiteConnection)
        Dim DsTemp As DataSet

        Dim I As Integer
        Try
            If TxtQuery.Text = "" Then Exit Sub
            mQry = "Delete From Report_Fields_Temp Where Code = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, mConn)
            myQryColums = ""
            mQry = "Select * From (" & TxtQuery.Text & ") as X Limit 1"
            DsTemp = AgL.FillData(mQry, AgL.GCn)
            With DsTemp.Tables(0)
                For I = 0 To .Columns.Count - 1
                    mQry = "Insert into Report_Fields_Temp (Sr, Code, Fld_Name, DataType) Values(" & I & ", " & AgL.Chk_Text(mSearchCode) & ", '[" & AgL.XNull(.Columns(I).ColumnName) & "]','" & AgL.XNull(.Columns(I).DataType.Name) & "' )"
                    AgL.Dman_ExecuteNonQry(mQry, mConn)
                Next
            End With

            mQry = "SELECT Rt.Fld_Name, Cast(IfNull(R.PrintYn,-1) as Bit) AS PrintYn, IfNull(R.GrandTotal,'') As GrandTotal, IfNull(R.ColumnWidth,0) As ColumnWidth, Cast(IfNull(R.WrapText,0) as Bit) AS WrapText, Cast(IfNull(R.LineDetail,0) as Bit) AS LineDetail, Rt.DataType   FROM Report_Fields_Temp Rt LEFT JOIN Report_Fields R ON Rt.Fld_Name||Rt.Code =R.Fld_Name||R.Code Where Rt.Code='" & mSearchCode & "' Order By Rt.Sr,R.Sr "
            DsTemp = AgL.FillData(mQry, mConn)
            If DsTemp.Tables(0).Rows.Count > 0 Then
                DGL4.RowCount = 1
                DGL4.Rows.Clear()
                With DsTemp.Tables(0)
                    For I = 0 To .Rows.Count - 1
                        DGL4.Rows.Add()
                        DGL4.Item(Col_SNo, I).Value = I + 1
                        DGL4.Item(Col4Fld_Name, I).Value = .Rows(I)("Fld_Name")
                        DGL4.Item(Col4PrintYn, I).Value = .Rows(I)("PrintYn")
                        DGL4.Item(Col4Aggregate_Function, I).Value = .Rows(I)("GrandTotal")
                        DGL4.Item(Col4ColumnWidth, I).Value = IIf(.Rows(I)("ColumnWidth") > 0, .Rows(I)("ColumnWidth"), 100)
                        DGL4.Item(Col4WrapText, I).Value = .Rows(I)("WrapText")
                        DGL4.Item(Col4Fld_DataType, I).Value = .Rows(I)("DataType")
                    Next
                End With
            End If



            If DGL1.ColumnCount = 0 Then
                DGL1.Height = Pnl1.Height
                DGL1.Width = Pnl1.Width
                DGL1.Top = Pnl1.Top
                DGL1.Left = Pnl1.Left
                Pnl1.Visible = False
                GrpBoxReportGroups.Controls.Add(DGL1)
                DGL1.Visible = True
                DGL1.BringToFront()
                DGL1.ColumnCount = 0
                With AgCL
                    '.AddAgTextColumn(DGL1, "DGL1SNo", 40, 5, "S.No.", , True)
                    For I = 0 To DGL4.Rows.Count - 1
                        .AddAgTextColumn(DGL1, "DGL1Group" & I + 1, 120, 100, "Group " & I + 1 & " Name", , False)
                        'DGL1.AgHelpDataSet(I) = AgL.FillData()
                    Next
                End With
                DGL1.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
                'AgL.FSetSNo(DGL1, Col_SNo)
                DGL1.TabIndex = Pnl1.TabIndex
                DGL1.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Arial"), 9)
                DGL1.DefaultCellStyle.Font = New Font(New FontFamily("Arial"), 8)
                DGL1.ScrollBars = ScrollBars.None
            End If




        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            DsTemp = Nothing
        End Try

    End Sub


    Private Sub BtnGridPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim TblTemp As DataTable
        mQry = "Select  * From (" & TxtQuery.Text & ") as X"
        TblTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        DgOutput.DataSource = TblTemp
        AgCL.AgSetDataGridAutoWidths(DgOutput, 100, False)
    End Sub

    Private Sub BtnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGo.Click
        Show_Report()
    End Sub


    Function Is_Valid_Settings() As Boolean
        Dim i As Integer
        Dim iFld As Integer
        Dim mFldFound As Boolean = False
        Try
            Is_Valid_Settings = True
            For i = 0 To DGL5.RowCount - 1
                mFldFound = False
                If DGL5.Item(Col5Fld_Name, i).Value <> "" Then
                    For iFld = 0 To DGL4.RowCount - 1
                        If AgL.StrCmp(DGL5.Item(Col5Fld_Name, i).Value, DGL4.Item(Col4Fld_Name, iFld).Value) Then
                            mFldFound = True
                        End If
                    Next
                    If Not mFldFound Then Err.Raise(1, , DGL5.Item(Col5Fld_Name, i).Value + " In Order Not In Show List.")
                End If
            Next
        Catch Ex As Exception
            MsgBox(Ex.Message)
            Is_Valid_Settings = False
        End Try
    End Function


    Sub Show_Report()
        'Dim rptReg As New ReportDocument
        'Dim mPrnHnd As New PrintHandler
        Dim GCnCmd As New SqlClient.SqlCommand
        Dim ds As New DataSet
        Dim TblGrandTotal As DataTable
        Dim strQry As String = ""
        Dim strCondition As String = ""
        Dim mOrderBy$, mSelectFields$, mGrandTotalField$, mSubQrySortStr$
        Dim mMainQrySortStr$ = ""
        Dim I As Integer, J As Integer
        Try




            If Not Is_Valid_Settings() Then Exit Sub

            'If Not DTMaster.Rows.Count > 0 Then
            'MsgBox("No Records Found to Print!!!", vbInformation, "Information")
            'Me.Cursor = Cursors.Default
            'Exit Sub
            'End If

            Dim mRowIndex As Integer = -1
            For I = 0 To DGL1.ColumnCount - 1
                If AgL.XNull(DGL1.Item(I, 0).Value) <> "" Then
                    mRowIndex = -1
                    For J = 0 To DGL4.RowCount - 1
                        If DGL4.Item(Col4PrintYn, J).Value Then
                            mRowIndex += 1
                        End If

                        If AgL.StrCmp(AgL.XNull(DGL1.Item(I, 0).Value), AgL.XNull(DGL4.Item(Col4Fld_Name, J).Value)) Then
                            Exit For
                        End If
                    Next
                    DGL1.Item(I, 0).Tag = mRowIndex : mRowIndex = -1
                End If
            Next



            For J = 0 To DGL1.ColumnCount - 1
                If DGL1.Item(J, 0).Value <> "" Then
                    For I = 0 To DGL4.RowCount - 1
                        If AgL.StrCmp(DGL1.Item(J, 0).Value, DGL4.Item(Col4Fld_Name, I).Value) Then
                            DGL4.Item(Col4Group, I).Value = 1
                            Exit For
                        End If
                    Next
                End If
            Next


            mSelectFields = F_SelectFieldsClause()
            mGrandTotalField = F_GrandTotalClause()
            mOrderBy = F_OrderByClause()
            mSubQrySortStr = F_GroupBySortingClause(mMainQrySortStr)



            With DGL3
                For I = 0 To .RowCount - 1
                    If AgL.XNull(.Item(Col3Fld_Name, I).Value) <> "" And
                                AgL.XNull(.Item(Col3Cond_Operator, I).Value) <> "" And
                                AgL.XNull(.Item(Col3Value, I).Value) <> "" Then
                        strCondition = strCondition & IIf(strCondition <> "", " And ", "") & .Item(Col3Fld_Name, I).Value & .Item(Col3Cond_Operator, I).Tag & "'" & .Item(Col3Value, I).Value & "'  "
                    End If
                Next I
                If strCondition <> "" Then strCondition = "Where " & strCondition
            End With

            If Not OptSummaryReport.Checked Then
                strQry = TxtQuery.Text
                strQry = "Select   " & mMainQrySortStr & " As SortField, ' ' As [Record Type], " & mSelectFields & " From (" & strQry & ") As X " & strCondition
                strQry = strQry & " Union All " & "Select   'ZZZZZZZZZZZZZZZ' As SortField, 'Grand Total' As [Record Type], " & mGrandTotalField & " From (" & TxtQuery.Text & ") As X  " & strCondition
            End If



            If OptSummaryReport.Checked Then
                mRowIndex = -1
                For I = 0 To DGL1.ColumnCount - 1
                    If DGL1.Item(I, 0).Value <> "" Then
                        mRowIndex = I
                    End If
                Next
            End If



            With DGL1
                For I = 0 To DGL1.Columns.Count - 1
                    If AgL.XNull(.Item(I, 0).Value) <> "" Then
                        If Not OptSummaryReport.Checked Then
                            strQry = strQry & " Union All " & "Select   " & mSubQrySortStr & " + '" & .Columns.Count - I & "'  As SortField, Cast('" & I & "'  as nVarchar) As [Record Type], " & F_SubTotalClause(I) & " From (" & TxtQuery.Text & ") As X " & strCondition & F_GroupByClause(AgL.XNull(.Item(I, 0).Value))
                        Else
                            If I = 0 Then
                                strQry = strQry & "Select   " & mSubQrySortStr & " + '" & .Columns.Count - I & "'  As SortField, Cast('" & I & "' as nVarchar) As [Record Type], " & F_SubTotalSummaryClause(I, mRowIndex) & " From (" & TxtQuery.Text & ") As X " & strCondition & F_GroupByClause(AgL.XNull(.Item(I, 0).Value))
                            Else
                                strQry = strQry & " Union All " & "Select   " & mSubQrySortStr & " + '" & .Columns.Count - I & "'  As SortField, Cast('" & IIf(I = mRowIndex, DGL4.RowCount, I) & "' as nVarchar) As [Record Type], " & F_SubTotalSummaryClause(I, mRowIndex) & " From (" & TxtQuery.Text & ") As X " & strCondition & F_GroupByClause(AgL.XNull(.Item(I, 0).Value))
                            End If
                        End If
                    End If
                Next
            End With
            'strQry = strQry & IIf(Len(mOrderBy) > 0, " Order By " & mOrderBy, "")
            If strQry.Trim = "" Then Err.Raise(1, , "No Report Groups Found to Print Summary Report")
            'strQry = strQry & " Order By SortField "

            'For I = 0 To DGL5.RowCount - 1
            '    If AgL.XNull(DGL5.Item(Col5Fld_Name, I).Value) <> "" Then
            '        strQry = strQry & ", " & DGL5.Item(Col5Fld_Name, I).Value & " " & IIf(DGL5.AgSelectedValue(Col5Fld_Name, I) = "0", "Desc", "Asc")
            '    End If
            'Next



            AgL.ADMain = New SQLite.SQLiteDataAdapter(strQry, AgL.GCn)
            AgL.ADMain.Fill(ds)



            strQry = "Select ' ' As SortField, 'Grand Total' As [Record Type], " & mGrandTotalField & " From (" & TxtQuery.Text & ") As X Limit 1" & strCondition
            TblGrandTotal = AgL.FillData(strQry, AgL.GCn).Tables(0)



            If ds.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print")
            DgOutput.DataSource = Nothing
            DgOutputTotals.DataSource = Nothing
            DgOutput.DataSource = ds.Tables(0)
            DgOutputTotals.DataSource = TblGrandTotal
            DgOutputTotals.Rows(0).DefaultCellStyle.BackColor = Color.AliceBlue
            DgOutputTotals.ReadOnly = True

            For I = 0 To ds.Tables(0).Columns.Count - 1

                Select Case UCase(ds.Tables(0).Columns(I).DataType.ToString)
                    Case "SYSTEM.INT32", "SYSTEM.DECIMAL", "SYSTEM.DOUBLE"
                        DgOutput.Columns(I).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                        DgOutputTotals.Columns(I).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End Select
            Next

            For I = 0 To DgOutput.Rows.Count - 1
                If DgOutput.Item(1, I).Value.ToString.Trim <> "" Then
                    If Val(DgOutput.Item(1, I).Value.ToString.Trim) < DGL4.RowCount Then
                        For J = (Val(DgOutput.Item(1, I).Value) + 2) To DgOutput.ColumnCount - 1
                            DgOutput.CurrentCell = DgOutput(J, I)
                            DgOutput.CurrentCell.Style.BackColor = Color.AliceBlue   'Color.SteelBlue
                            'DgOutput.CurrentCell.Style.ForeColor = Color.White
                        Next
                        'DgOutput.Rows(I).DefaultCellStyle.BackColor = Color.Yellow
                    End If
                End If
            Next
            DgOutput.CurrentCell = DgOutput(2, 0)

            For I = 0 To DgOutput.ColumnCount - 1
                DgOutput.Columns(I).SortMode = DataGridViewColumnSortMode.NotSortable
            Next


            For I = 0 To DGL4.RowCount - 1
                For J = 0 To DgOutput.ColumnCount - 1
                    If AgL.StrCmp(DGL4.Item(Col4Fld_Name, I).Value, "[" & DgOutput.Columns(J).HeaderText & "]") Then
                        DgOutput.Columns(J).Tag = Val(DGL4.Item(Col4ColumnWidth, I).Value)
                    End If
                Next
            Next


            If Not OptSummaryReport.Checked And DgOutput.RowCount > 1 Then
                DgOutput.Rows(DgOutput.RowCount - 1).Visible = False
            End If


            For I = 0 To DgOutput.ColumnCount - 1
                DgOutputTotals.Columns(I).Width = DgOutput.Columns(I).Width
            Next


            DgOutput.Columns(0).Visible = False
            DgOutput.Columns(1).Visible = False
            DgOutputTotals.Columns(0).Visible = False
            DgOutputTotals.Columns(1).Visible = False
            'DgOutput.Rows(DgOutput.RowCount - 1).DefaultCellStyle.Font = New Font(New FontFamily("Arial"), 15)
            'DgOutput.CurrentCell = DgOutput(2, 0)
            'DgOutput.DefaultCellStyle.BackColor = Color.Beige

        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
        Me.Cursor = Cursors.Default

    End Sub



    Private Function F_GrandTotalClause() As String
        Dim mSelectFields$
        Dim I As Integer
        Dim mGroupIndex As Integer = -1

        mSelectFields = ""


        With DGL1
            For I = 0 To .ColumnCount - 1
                If .Item(I, 0).Value <> "" Then
                    mSelectFields = mSelectFields & "Null As " & AgL.XNull(.Item(I, 0).Value) & ","
                End If
            Next
        End With

        With DGL4
            For I = 0 To .RowCount - 1
                If .Item(Col4PrintYn, I).Value = True And .Item(Col4Group, I).Value = False Then
                    If AgL.XNull(.Item(Col4Aggregate_Function, I).Value) <> "" Then
                        mSelectFields = mSelectFields & AgL.XNull(.Item(Col4Aggregate_Function, I).Value) & "(" & AgL.XNull(.Item(Col4Fld_Name, I).Value) & "),"
                    Else
                        If Not OptSummaryReport.Checked Then
                            'If Not mGrandTotalWritten Then
                            'mSelectFields = mSelectFields & "'Grand Total' As " & AgL.XNull(.Item(Col4Fld_Name, I).Value) & ","
                            'mGrandTotalWritten = True
                            'Else
                            mSelectFields = mSelectFields & "Null As " & AgL.XNull(.Item(Col4Fld_Name, I).Value) & ","
                            'End If
                        End If
                        'mGroupIndex = -1
                    End If
                End If
            Next
        End With
        If mSelectFields.ToString.Length > 0 Then mSelectFields = Mid(mSelectFields, 1, Len(mSelectFields) - 1)

        F_GrandTotalClause = mSelectFields
    End Function

    Private Function F_SubTotalClause(ByVal mGroupRowIndex As Integer) As String
        Dim mSelectFields$
        Dim I As Integer
        Dim mGroupFieldIndex As Integer = -1

        mSelectFields = ""

        With DGL1
            For I = 0 To .ColumnCount - 1
                If .Item(I, 0).Value <> "" Then
                    If I = mGroupRowIndex Then
                        mSelectFields = mSelectFields & "'Sub Total' As " & AgL.XNull(.Item(I, 0).Value) & ","
                    ElseIf I > mGroupRowIndex Then
                        mSelectFields = mSelectFields & "Null As " & AgL.XNull(.Item(I, 0).Value) & ","
                    Else
                        mSelectFields = mSelectFields & "Max(" & AgL.XNull(.Item(I, 0).Value) & "),"
                    End If
                End If
            Next
        End With


        With DGL4
            For I = 0 To .RowCount - 1
                If .Item(Col4PrintYn, I).Value = True And .Item(Col4Group, I).Value = False Then
                    If AgL.XNull(.Item(Col4Aggregate_Function, I).Value) <> "" Then
                        mSelectFields = mSelectFields & AgL.XNull(.Item(Col4Aggregate_Function, I).Value) & "(" & AgL.XNull(.Item(Col4Fld_Name, I).Value) & "),"
                    Else
                        mSelectFields = mSelectFields & "Null As " & AgL.XNull(.Item(Col4Fld_Name, I).Value) & ","
                    End If
                End If
            Next
        End With
        mSelectFields = Mid(mSelectFields, 1, Len(mSelectFields) - 1)

        F_SubTotalClause = mSelectFields
    End Function

    Private Function F_SubTotalSummaryClause(ByVal mGroupRowIndex As Integer, ByVal mLastGroup As Integer) As String
        Dim mSelectFields$
        Dim I As Integer
        Dim mGroupFieldIndex As Integer = -1

        mSelectFields = ""


        With DGL1
            For I = 0 To .ColumnCount - 1
                If .Item(I, 0).Value <> "" Then
                    If I = mGroupRowIndex And I <> mLastGroup Then
                        mSelectFields = mSelectFields & "'Sub Total' As " & AgL.XNull(.Item(I, 0).Value) & ","
                    ElseIf I > mGroupRowIndex Then
                        mSelectFields = mSelectFields & "Null As " & AgL.XNull(.Item(I, 0).Value) & ","
                    Else
                        mSelectFields = mSelectFields & "Max(" & AgL.XNull(.Item(I, 0).Value) & ")  As " & AgL.XNull(.Item(I, 0).Value) & ","
                    End If
                End If
            Next
        End With

        'With DGL1
        '    For I = 0 To .ColumnCount - 1
        '        If .Item(I, 0).Value <> "" Then
        '            mSelectFields = mSelectFields & "Max(" & AgL.XNull(.Item(I, 0).Value) & "),"
        '        End If
        '    Next
        'End With


        With DGL4
            For I = 0 To .RowCount - 1
                If .Item(Col4PrintYn, I).Value = True And .Item(Col4Group, I).Value = False Then
                    If AgL.XNull(.Item(Col4Aggregate_Function, I).Value) <> "" Then
                        mSelectFields = mSelectFields & AgL.XNull(.Item(Col4Aggregate_Function, I).Value) & "(" & AgL.XNull(.Item(Col4Fld_Name, I).Value) & ") As " & AgL.XNull(.Item(Col4Fld_Name, I).Value) & ","
                    End If
                End If
            Next
        End With
        mSelectFields = Mid(mSelectFields, 1, Len(mSelectFields) - 1)

        F_SubTotalSummaryClause = mSelectFields
    End Function


    Private Function F_SelectFieldsClause() As String
        Dim mSelectFields$
        Dim I As Integer, J As Integer
        Dim mGroupDataType As String

        mSelectFields = ""



        With DGL1
            For I = 0 To .ColumnCount - 1
                If .Item(I, 0).Value <> "" Then
                    mGroupDataType = ""
                    For J = 0 To DGL4.ColumnCount - 1
                        If AgL.StrCmp(.Item(I, 0).Value, DGL4.Item(Col4Fld_Name, J).Value) Then
                            mGroupDataType = DGL4.Item(Col4Fld_DataType, J).Value
                        End If
                    Next
                    Select Case AgL.UTrim(mGroupDataType)
                        Case "DATETIME"
                            mSelectFields = mSelectFields & AgL.ConvertDateField(AgL.XNull(.Item(I, 0).Value)) & " as " & AgL.XNull(.Item(I, 0).Value) & ","
                        Case Else
                            mSelectFields = mSelectFields & AgL.XNull(.Item(I, 0).Value) & ","
                    End Select

                End If
            Next
        End With


        With DGL4
            For I = 0 To .RowCount - 1
                If .Item(Col4PrintYn, I).Value = True And .Item(Col4Group, I).Value = False Then
                    mSelectFields = mSelectFields & AgL.XNull(.Item(Col4Fld_Name, I).Value) & ","
                End If
            Next
        End With


        mSelectFields = Mid(mSelectFields, 1, Len(mSelectFields) - 1)

        F_SelectFieldsClause = mSelectFields
    End Function

    Private Function F_OrderByClause() As String
        Dim mOrderBy$
        Dim I As Integer
        mOrderBy = ""
        Dim mStructGrp() As PrintHandler.StructGroupBy = Nothing
        With DGL4
            For I = 0 To .RowCount - 1
                If AgL.XNull(.Item(Col4Fld_Name, I).Value) <> "" Then
                    If mStructGrp Is Nothing Then
                        ReDim mStructGrp(1)
                    Else
                        ReDim Preserve mStructGrp(UBound(mStructGrp) + 1)
                    End If
                    mStructGrp(I).FieldName = AgL.XNull(.Item(Col4Fld_Name, I).Value)
                    'mStructGrp(I).Ascending = AgL.VNull(.Item(Col4Asc_Desc, I).Value)
                    'mStructGrp(I).SubTotal = AgL.VNull(.Item(Col4SubTotalYN, I).Value)
                    'mStructGrp(I).GroupHeader = IIf(.Item(Col4GroupHeaderYN, I).Value = True, True, False)
                    mOrderBy = mOrderBy & AgL.XNull(.Item(Col4Fld_Name, I).Value) & IIf(I <> .RowCount - 2, ",", "")
                End If
            Next
        End With

        F_OrderByClause = mOrderBy
    End Function


    Private Function F_GroupByClause(ByVal mFieldName As String) As String
        Dim mGroupBy$
        Dim I As Integer
        mGroupBy = ""

        With DGL1
            For I = 0 To .ColumnCount - 1
                If AgL.XNull(.Item(I, 0).Value) <> "" Then
                    mGroupBy = mGroupBy & AgL.XNull(.Item(I, 0).Value) & ","
                    If AgL.StrCmp(AgL.XNull(.Item(I, 0).Value), mFieldName) Then Exit For
                End If
            Next
        End With

        mGroupBy = Mid(mGroupBy, 1, Len(mGroupBy) - 1)
        F_GroupByClause = " Group By " & mGroupBy
    End Function


    Private Function F_GroupBySortingClause(ByRef mMainQrySortStr As String) As String
        Dim mGroupBy$
        Dim I As Integer
        mGroupBy = ""
        mMainQrySortStr = ""
        With DGL1
            For I = 0 To .Columns.Count - 1
                If AgL.XNull(.Item(I, 0).Value) <> "" Then
                    mMainQrySortStr = mMainQrySortStr & "Replace(Cast(" & AgL.XNull(.Item(I, 0).Value) & " as nVarchar),' ','')+"
                    mGroupBy = mGroupBy & "Replace(Cast(Max(" & AgL.XNull(.Item(I, 0).Value) & ") as nVarchar),' ','')+"
                End If
            Next
        End With

        If Len(mGroupBy) > 0 Then mGroupBy = Mid(mGroupBy, 1, Len(mGroupBy) - 1)
        If Len(mMainQrySortStr) > 0 Then mMainQrySortStr = Mid(mMainQrySortStr, 1, Len(mMainQrySortStr) - 1)
        If mMainQrySortStr = "" Then mMainQrySortStr = "' '"
        F_GroupBySortingClause = mGroupBy
    End Function

    Private Sub P_ColumnDisplayClause()
        Dim I As Integer
        Dim mStructColumnPrintYn() As PrintHandler.StructFooter = Nothing
        With DGL4
            For I = 0 To .RowCount - 1
                If AgL.XNull(.Item(Col4Fld_Name, I).Value) <> "" Then
                    If mStructColumnPrintYn Is Nothing Then
                        ReDim mStructColumnPrintYn(1)
                    Else
                        ReDim Preserve mStructColumnPrintYn(UBound(mStructColumnPrintYn) + 1)
                    End If
                    mStructColumnPrintYn(I).FieldName = AgL.XNull(.Item(Col4Fld_Name, I).Value)
                    mStructColumnPrintYn(I).AggregateFunction = .Item(Col4PrintYn, I).Value
                End If
            Next
        End With

    End Sub

    Private Sub DgOutput_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles DgOutput.ColumnWidthChanged
        'DgOutputTotals.Columns(e.Column.Index).Width = e.Column.Width
    End Sub

    Private Sub BtnFilterRecords_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFilterRecords.Click
        GrpBoxReportCriteria.Left = 300
        GrpBoxReportCriteria.Top = 200
        GrpBoxReportCriteria.Visible = True
        GrpBoxColumnSelection.Visible = False
        GrpBoxReportGroups.Visible = False
        GrpBoxPrintSettings.Visible = False
    End Sub

    Private Sub BtnColumnSelection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnColumnSelection.Click

        GrpBoxColumnSelection.Left = 300
        GrpBoxColumnSelection.Top = 200
        GrpBoxColumnSelection.Visible = True
        GrpBoxReportCriteria.Visible = False
        GrpBoxReportGroups.Visible = False
        GrpBoxPrintSettings.Visible = False
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReportGroups.Click
        GrpBoxReportGroups.Left = 300
        GrpBoxReportGroups.Top = 200
        GrpBoxReportGroups.Visible = True
        GrpBoxColumnSelection.Visible = False
        GrpBoxReportCriteria.Visible = False
        GrpBoxPrintSettings.Visible = False
    End Sub

    Private Sub BtnOkReportCriteria_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOkReportCriteria.Click
        GrpBoxReportCriteria.Visible = False
    End Sub

    Private Sub BtnOkReportGroups_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOkReportGroups.Click
        GrpBoxReportGroups.Visible = False
    End Sub

    Private Sub BtnOkColumnSelection_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOkColumnSelection.Click
        GrpBoxColumnSelection.Visible = False
    End Sub


    Private Sub OptSystemReport_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptSystemReport.CheckedChanged
        If OptSystemReport.Checked Then
            FIniMaster()
            MoveRec()
            ProcessReport(mSearchCode, AgL.GCn)
            'BtnColumnSelection.Enabled = False
            'BtnReportGroups.Enabled = False
            BtnUserReports.Enabled = False
        End If
    End Sub

    Private Sub OptUserReport_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptUserReport.CheckedChanged
        If OptUserReport.Checked Then
            GrpBoxUserReports.Left = 300
            GrpBoxUserReports.Top = 200
            BtnColumnSelection.Enabled = True
            BtnReportGroups.Enabled = True
            BtnUserReports.Enabled = True

            If TxtUserReport.Text = "" Then
                'GrpBoxUserReports.Visible = True
                TxtUserReport.ReadOnly = False
                TxtUserReport.Enabled = True
                TxtUserReport.Focus()
                Exit Sub
            End If

        End If
    End Sub

    Private Sub BtnSaveReportSettings_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSaveReportSettings.Click
        If OptSystemReport.Checked Then
            SaveRecord()
        ElseIf OptUserReport.Checked Then
            SaveRecord_User()
        End If
    End Sub

    Private Sub DgOutput_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles DgOutput.Scroll
        DgOutputTotals.HorizontalScrollingOffset = e.NewValue
    End Sub

    Private Sub BtnUserReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUserReports.Click
        Dim mObj As FrmUserReports

        mObj = New FrmUserReports
        mObj.MainReportCode = CboDescription.SelectedValue
        mObj.ShowDialog()

        If mObj.UserReportCode <> "" Then
            TxtUserReport.Text = mObj.UserReportDesc
            TxtUserReport.Tag = mObj.UserReportCode
            FIniMaster(, , mObj.UserReportCode)
            MoveRec_User(mObj.UserReportCode)
            ProcessReport(mObj.UserReportCode, AgL.GCn)
            Show_Report()
        End If


        mObj.Dispose()
        'GrpBoxUserReports.Visible = True
        'TxtUserReport.Focus()
    End Sub

    Private Sub BtnOkUserReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOkUserReports.Click
        Dim DtTemp As DataTable
        If TxtUserReport.Text = "" Then
            GrpBoxUserReports.Visible = False
            OptDetailedReport.Checked = True
            Exit Sub
        End If

        mQry = "Select Description From Report_User Where Description = '" & TxtUserReport.Text & "' And Report_Main = '" & CboDescription.SelectedValue & "' "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTemp.Rows.Count = 0 Then
            FIniMaster()
            GrpBoxUserReports.Visible = False
        Else
            MsgBox("Report With Same Description Already Exist. Please Select Any Other Name.")
            TxtUserReport.Focus()
        End If
        DtTemp = Nothing
    End Sub

    Private Function GetMaxId(ByVal mTableName As String, ByVal mPrimaryField As String, ByVal mConn As SQLiteConnection,
                                ByVal PubDivCode As String, ByVal PubSiteCode As String, Optional ByVal mPad_Len As Integer = 0, Optional ByVal IsSiteWise As Boolean = False, Optional ByVal IsDivisionWise As Boolean = False, Optional ByVal mCmd As SQLiteCommand = Nothing)
        Dim CondStr As String = ""
        If mCmd Is Nothing Then
            mCmd = New SQLiteCommand
            mCmd = mConn.CreateCommand
        End If

        If IsDivisionWise Then CondStr = " And Left(" & mPrimaryField & ",1) = '" & PubDivCode & "' "
        If IsSiteWise Then CondStr = " And SubString(" & mPrimaryField & ",2,1) = '" & PubSiteCode & "' "

        mCmd = AgL.Dman_Execute("Select IfNull(Max(Cast(SubString(" & mPrimaryField & ",3,Len(" & mPrimaryField & ")) as BigInt)) ,0)+1 As MaxId From " & mTableName & " " &
                            " Where 1=1 " & CondStr & " ", mConn)

        GetMaxId = PubDivCode & PubSiteCode & mCmd.ExecuteScalar().ToString.PadLeft(mPad_Len, "0")
    End Function

    Private Sub BtnCancelUserReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancelUserReport.Click
        GrpBoxUserReports.Visible = False
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click
        Print()
    End Sub

    Private Sub BtnPrintSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrintSettings.Click
        GrpBoxPrintSettings.Left = 300
        GrpBoxPrintSettings.Top = 200
        HideGrid()
        GrpBoxPrintSettings.Visible = True


        mQry = "Select Fld_Name, Fld_Name As [Field Name] From Report_Fields_Temp  Where Code = '" & mSearchCode & "'"
        DGL2.AgHelpDataSet(Col2Row1, 0, GrpBoxPrintSettings.Top, GrpBoxPrintSettings.Left) = AgL.FillData(mQry, AgL.GCn)
        DGL2.AgHelpDataSet(Col2Row2, 0, GrpBoxPrintSettings.Top, GrpBoxPrintSettings.Left) = AgL.FillData(mQry, AgL.GCn)
        DGL2.AgHelpDataSet(Col2Row3, 0, GrpBoxPrintSettings.Top, GrpBoxPrintSettings.Left) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Sub HideGrid()
        GrpBoxReportGroups.Visible = False
        GrpBoxColumnSelection.Visible = False
        GrpBoxReportCriteria.Visible = False
        GrpBoxPrintSettings.Visible = False
        GrpBoxDataSorting.Visible = False
    End Sub

    Private Sub BtnOkDataSorting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOkDataSorting.Click
        GrpBoxDataSorting.Visible = False
    End Sub

    Private Sub BtnOkPrintSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOkPrintSettings.Click
        GrpBoxPrintSettings.Visible = False
    End Sub

    Private Sub BtnOrderBy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOrderBy.Click
        GrpBoxDataSorting.Left = 300
        GrpBoxDataSorting.Top = 200
        HideGrid()
        GrpBoxDataSorting.Visible = True

        mQry = "Select Fld_Name, Fld_Name As [Field Name] From Report_Fields_Temp  Where Code = '" & mSearchCode & "'"
        DGL5.AgHelpDataSet(Col5Fld_Name, 0, GrpBoxDataSorting.Top, GrpBoxDataSorting.Left) = AgL.FillData(mQry, AgL.GCn)
        AgCL.AgIsDuplicate(DGL5, Col5Fld_Name)

    End Sub

    Private Sub BtnPageSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub


    Private Sub BtnNewUserReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNewUserReport.Click
        GrpBoxUserReports.Visible = True
    End Sub
End Class
