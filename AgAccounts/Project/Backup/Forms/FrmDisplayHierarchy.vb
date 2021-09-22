Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmDisplayHierarchy
    Private Const GGRCode As Byte = 0
    Private Const GDocId As Byte = 1
    Private Const GGRName As Byte = 2
    Private Const GVNo As Byte = 3
    Private Const GVType As Byte = 4
    Private Const GVDate As Byte = 5
    Private Const GNarration As Byte = 6
    Private Const GOpening As Byte = 7
    Private Const GDR_CR_OP As Byte = 8
    Private Const GDebit As Byte = 9
    Private Const GGRCodeCredit As Byte = 10
    Private Const GGRNameCredit As Byte = 11
    Private Const GCredit As Byte = 12
    Private Const GClosing As Byte = 13
    Private Const GDR_CR_CL As Byte = 14
    Private Const GGR_SG As Byte = 15


    '====== Short Name Used In For Particular Heads
    Private Const CnsBalanceSheet As String = "BLNS"
    Private Const CnsProfitAndLoss As String = "PRLS"
    Private Const CnsTrialBalance As String = "TRLB"
    Private Const CnsDTrialBalance As String = "DTRLB"

    Dim WithEvents FGMain As New AgControls.AgDataGrid
    Private DTVType As DataTable
    Dim StrSQLQuery As String
    Dim IntLevel As Int16 = 0
    Dim StrPreviousCode As String = "", StrPreviousName As String = ""
    Dim DTPrint As New DataTable("T")
    Private Sub FSetDTColumn()
        DTPrint.Columns.Add("GRCode", System.Type.GetType("System.String"))
        DTPrint.Columns.Add("DocId", System.Type.GetType("System.String"))
        DTPrint.Columns.Add("GRName", System.Type.GetType("System.String"))
        DTPrint.Columns.Add("VNo", System.Type.GetType("System.String"))
        DTPrint.Columns.Add("VType", System.Type.GetType("System.String"))
        DTPrint.Columns.Add("VDate", System.Type.GetType("System.String"))
        DTPrint.Columns.Add("Narration", System.Type.GetType("System.String"))
        DTPrint.Columns.Add("Opening", System.Type.GetType("System.Double"))
        DTPrint.Columns.Add("DR_CR_OP", System.Type.GetType("System.String"))
        DTPrint.Columns.Add("Debit", System.Type.GetType("System.Double"))
        DTPrint.Columns.Add("GRCodeCredit", System.Type.GetType("System.String"))
        DTPrint.Columns.Add("GRNameCredit", System.Type.GetType("System.String"))
        DTPrint.Columns.Add("Credit", System.Type.GetType("System.Double"))
        DTPrint.Columns.Add("Closing", System.Type.GetType("System.Double"))
        DTPrint.Columns.Add("DR_CR_CL", System.Type.GetType("System.String"))
        DTPrint.Columns.Add("GR_SG", System.Type.GetType("System.String"))
    End Sub
    Private Sub IniGrid(ByVal DspType As ClsStructure.DisplayType)
        FGMain.Rows.Clear()
        FGMain.Columns.Clear()
        FGMain.Height = PnlMain.Height
        FGMain.Width = PnlMain.Width
        FGMain.Top = PnlMain.Top
        FGMain.Left = PnlMain.Left
        PnlMain.Visible = False
        Controls.Add(FGMain)
        FGMain.Visible = True
        FGMain.BringToFront()

        FGMain.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        If DspType = ClsStructure.DisplayType.Ledger Then
            FGMain.AllowUserToAddRows = False
            AgCl.AddAgTextColumn(FGMain, "GRCode", 0, 5, "GRCode", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "DocId", 0, 5, "DocId", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "GRName", 450, 0, "Particulars", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "VNo", 72, 5, "Vr.No.", True, True, False)
            AgCl.AddAgTextColumn(FGMain, "VType", 50, 5, "Type", True, True, False)
            AgCl.AddAgTextColumn(FGMain, "VDate", 80, 5, "Date", True, True, False)
            AgCl.AddAgTextColumn(FGMain, "Narration", 415, 5, "Narration", True, True, False)
            AgCl.AddAgTextColumn(FGMain, "Opening", 150, 20, "Opening", False, True, True)
            AgCl.AddAgTextColumn(FGMain, "", 150, 20, "", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "Debit", 100, 20, "Debit", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "GRCodeCredit", 0, 5, "GRCodeCredit", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "GRNameCredit", 450, 0, "ParticularsCredit", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "Credit", 100, 20, "Credit", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "Balance", 100, 20, "Balance", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "", 21, 20, "", True, True, False)
            AgCl.AddAgTextColumn(FGMain, "GR_SG", 0, 5, "Group Or SubGroup", False, True, False)
        ElseIf DspType = ClsStructure.DisplayType.GroupBalance Or DspType = ClsStructure.DisplayType.TrailBalance Then
            AgCl.AddAgTextColumn(FGMain, "GRCode", 0, 5, "GRCode", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "DocId", 0, 5, "DocId", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "GRName", 630, 0, "Particulars", True, True, False)
            AgCl.AddAgTextColumn(FGMain, "VNo", 0, 5, "Vr.No.", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "VType", 0, 5, "Vr.Type", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "VDate", 0, 5, "Date", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "Narration", 0, 5, "Narration", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "Opening", 150, 20, "Opening", False, True, True)
            AgCl.AddAgTextColumn(FGMain, "", 150, 20, "", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "Debit", 150, 20, "Debit", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "GRCodeCredit", 0, 5, "GRCodeCredit", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "GRNameCredit", 450, 0, "ParticularsCredit", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "Credit", 150, 20, "Credit", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "Closing", 150, 20, "Closing", False, True, True)
            AgCl.AddAgTextColumn(FGMain, "", 150, 20, "", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "GR_SG", 0, 5, "Group Or SubGroup", False, True, False)
        ElseIf DspType = ClsStructure.DisplayType.DTrailBalance Then
            AgCl.AddAgTextColumn(FGMain, "GRCode", 0, 5, "GRCode", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "DocId", 0, 5, "DocId", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "GRName", 415, 0, "Particulars", True, True, False)
            AgCl.AddAgTextColumn(FGMain, "VNo", 0, 5, "Vr.No.", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "VType", 0, 5, "Vr.Type", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "VDate", 0, 5, "Date", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "Narration", 0, 5, "Narration", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "Opening", 120, 20, "Opening", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "", 21, 20, "", True, True, False)
            AgCl.AddAgTextColumn(FGMain, "Debit", 120, 20, "Debit", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "GRCodeCredit", 0, 5, "GRCodeCredit", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "GRNameCredit", 450, 0, "ParticularsCredit", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "Credit", 120, 20, "Credit", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "Closing", 120, 20, "Closing", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "", 21, 20, "", True, True, False)
            AgCl.AddAgTextColumn(FGMain, "GR_SG", 0, 5, "Group Or SubGroup", False, True, False)
        ElseIf DspType = ClsStructure.DisplayType.BalanceSheet Then
            AgCl.AddAgTextColumn(FGMain, "GRCode", 0, 5, "GRCode", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "DocId", 0, 5, "DocId", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "GRName", 340, 0, "Liabilities", True, True, False)
            AgCl.AddAgTextColumn(FGMain, "VNo", 0, 5, "Vr.No.", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "VType", 0, 5, "Vr.Type", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "VDate", 0, 5, "Date", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "Narration", 0, 5, "Narration", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "Opening", 150, 20, "Opening", False, True, True)
            AgCl.AddAgTextColumn(FGMain, "", 150, 20, "", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "Debit", 125, 20, "Amount", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "GRCodeCredit", 0, 5, "GRCodeCredit", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "GRNameCredit", 340, 0, "Assets", True, True, False)
            AgCl.AddAgTextColumn(FGMain, "Credit", 125, 20, "Amount", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "Closing", 150, 20, "Closing", False, True, True)
            AgCl.AddAgTextColumn(FGMain, "", 150, 20, "", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "GR_SG", 0, 5, "Group Or SubGroup", False, True, False)
            FGMain.SelectionMode = DataGridViewSelectionMode.CellSelect
        ElseIf DspType = ClsStructure.DisplayType.ProfitAndLoss Then
            AgCl.AddAgTextColumn(FGMain, "GRCode", 0, 5, "GRCode", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "DocId", 0, 5, "DocId", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "GRName", 340, 0, "Particulars", True, True, False)
            AgCl.AddAgTextColumn(FGMain, "VNo", 0, 5, "Vr.No.", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "VType", 0, 5, "Vr.Type", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "VDate", 0, 5, "Date", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "Narration", 0, 5, "Narration", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "Opening", 150, 20, "Opening", False, True, True)
            AgCl.AddAgTextColumn(FGMain, "", 150, 20, "", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "Debit", 125, 20, "Amount", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "GRCodeCredit", 0, 5, "GRCodeCredit", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "GRNameCredit", 340, 0, "Particulars", True, True, False)
            AgCl.AddAgTextColumn(FGMain, "Credit", 125, 20, "Amount", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "Closing", 150, 20, "Closing", False, True, True)
            AgCl.AddAgTextColumn(FGMain, "", 150, 20, "", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "GR_SG", 0, 5, "Group Or SubGroup", False, True, False)
            FGMain.SelectionMode = DataGridViewSelectionMode.CellSelect
        End If

        FGMain.BackgroundColor = Color.White
        FGMain.RowsDefaultCellStyle.SelectionForeColor = Color.Black
        FGMain.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        FGMain.TabIndex = PnlMain.TabIndex
        FGMain.GridColor = Color.White
        FGMain.AllowUserToDeleteRows = False
        FGMain.AllowUserToAddRows = False
    End Sub
    Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        If Trim(DHSMain.StrFromDate) = "" Then
            DHSMain.StrFromDate = AgL.PubStartDate
            DHSMain.StrToDate = AgL.PubEndDate
            DHSMain.StrSiteCode = "'" + AgL.PubSiteCode + "'"
            DHSMain.StrSiteName = AgL.PubSiteName
            DHSMain.StrZeroBalace = "Y"
            DHSMain.StrShowContra = "N"
            DHSMain.DblClosingStock = 0
        End If
        FSetDTColumn()
    End Sub
    Private Sub FrmDisplayHierarchy_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 650, 990, 0, 0)
        Agl.GridDesign(FGMain)
    End Sub
    Private Sub FrmDisplayHierarchy_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Agl.FPaintForm(sender, e, 0)
        LblDisplay.BackColor = Color.White
        LblFilter.BackColor = Color.White
    End Sub
    Private Function FForColumn(ByVal IntColIndex As Integer) As Integer
        If FGMain.Columns(GGRNameCredit).Visible Then
            If Not (IntColIndex = GGRName Or IntColIndex = GGRNameCredit) Then IntColIndex = -1
        Else
            IntColIndex = GGRName
        End If
        FForColumn = IntColIndex
    End Function
    Private Sub FSetFilter(ByVal IntKeyCode As Integer)
        Dim StrFilter As String

        StrFilter = LblFilter.Text
        Select Case IntKeyCode
            Case Keys.Delete, Keys.Left, Keys.Right, Keys.Enter, Keys.Escape
                StrFilter = ""
        End Select
        LblFilter.Text = StrFilter
    End Sub
    Private Sub FFilter(ByVal ChKeyChar As Char, ByVal IntColIndex As Integer)
        Dim I As Integer
        Dim IntFilterLenght As Integer
        Dim StrFilter As String

        IntColIndex = FForColumn(IntColIndex)
        If IntColIndex < 0 Then LblFilter.Text = "" : Exit Sub

        StrFilter = LblFilter.Text
        Select Case Asc(ChKeyChar)
            Case Keys.Back
                If StrFilter <> "" Then StrFilter = Microsoft.VisualBasic.Left(StrFilter, Len(StrFilter) - 1)
            Case Nothing
            Case Else
                StrFilter = StrFilter & ChKeyChar
        End Select

        IntFilterLenght = Len(StrFilter)
        If StrFilter <> "" Then
            For I = 0 To FGMain.Rows.Count - 1
                If UCase(StrFilter) = UCase(Mid(FGMain(IntColIndex, I).Value, 1, IntFilterLenght)) Then
                    FGMain(IntColIndex, I).Selected = True
                    LblFilter.Text = StrFilter
                    Exit For
                End If
            Next
        Else
            LblFilter.Text = StrFilter
        End If
    End Sub
    Private Sub FBalanceSheet_Disp()
        Dim StrCondition1 As String = ""
        Dim DTTemp As DataTable
        Dim DblDebit_Total As Double, DblCredit_Total As Double
        Dim I As Integer, J As Integer, DblNet_Profit_Loss As Double = 0

        Try
            IniGrid(ClsStructure.DisplayType.BalanceSheet)
            LblDisplay.Text = "Balance Sheet"
            LblDisplay.Tag = CnsBalanceSheet
            LblDisplayDate.Text = "As On : " & DHSMain.StrToDate
            LblDisplaySite.Text = DHSMain.StrSiteName
            StrCondition1 = " Where LG.V_Date <= " & AgL.ConvertDate(DHSMain.StrToDate) & " And LG.Site_Code In (" & DHSMain.StrSiteCode & ")"

            '========== For Detail Section =======

            StrSQLQuery = "Select	(Case IsNull(AG1.GroupCode,'') When '' Then IsNull(AG.GroupCode,'') "
            StrSQLQuery = StrSQLQuery + "Else IsNull(AG1.GroupCode,'') End)  As GroupCode, "
            StrSQLQuery = StrSQLQuery + "Max(Case IsNull(AG1.GroupName,'') When '' Then IsNull(AG.GroupName,'') "
            StrSQLQuery = StrSQLQuery + "Else IsNull(AG1.GroupName,'') End)  As GName, "
            StrSQLQuery = StrSQLQuery + "(Case When (IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0))>0 Then  "
            StrSQLQuery = StrSQLQuery + "(IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
            StrSQLQuery = StrSQLQuery + "(Case When (IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0))>0 Then "
            StrSQLQuery = StrSQLQuery + "(IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr, "
            StrSQLQuery = StrSQLQuery + "Max(Case IsNull(AG1.GroupCode,'') When '' Then IsNull(AG.ContraGroupName,'') Else IsNull(AG1.ContraGroupName,'') End)  "
            StrSQLQuery = StrSQLQuery + "As ContraGroupName, "
            StrSQLQuery = StrSQLQuery + "Max(Case IsNull(AG1.GroupCode,'') When '' Then IsNull(AG.GroupNature,'') Else IsNull(AG1.GroupNature,'') End)   "
            StrSQLQuery = StrSQLQuery + "As GroupNature "

            StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode  Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroup AG On AG.GroupCode=SG.GroupCode Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroupPath AGP On AGP.GroupCode=AG.GroupCode And AGP.SNo=" & IntLevel & " Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroup AG1 On AG1.GroupCode=AGP.GroupUnder "
            StrSQLQuery = StrSQLQuery + StrCondition1
            StrSQLQuery = StrSQLQuery + "And AG.GroupNature In ('A','L') "

            StrSQLQuery = StrSQLQuery + "Group By (Case IsNull(AG1.GroupCode,'') When '' Then IsNull(AG.GroupCode,'') "
            StrSQLQuery = StrSQLQuery + "Else IsNull(AG1.GroupCode,'') End) "
            StrSQLQuery = StrSQLQuery + "Having (IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) <> 0 "
            StrSQLQuery = StrSQLQuery + "Order By Max(Case IsNull(AG1.GroupName,'') When '' Then IsNull(AG.GroupName,'') "
            StrSQLQuery = StrSQLQuery + "Else IsNull(AG1.GroupName,'') End) "

            DTTemp = cmain.FGetDatTable(StrSQLQuery, Agl.Gcn)

            If DTTemp.Rows.Count > 0 Then
                FGMain.Rows.Add(DTTemp.Rows.Count + 2)
            End If
            DblDebit_Total = 0
            DblCredit_Total = 0
            For I = 0 To DTTemp.Rows.Count - 1
                If Agl.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0 Then
                    J = FFindEmptyRow(GGRNameCredit)
                    FGMain(GGRCodeCredit, J).Value = Agl.Xnull(DTTemp.Rows(I).Item("GroupCode"))
                    If UCase(Agl.Xnull(DTTemp.Rows(I).Item("GroupNature"))) = "A" Then
                        FGMain(GGRNameCredit, J).Value = Agl.Xnull(DTTemp.Rows(I).Item("GName"))
                    Else
                        FGMain(GGRNameCredit, J).Value = Agl.Xnull(DTTemp.Rows(I).Item("ContraGroupName"))
                    End If
                    FGMain(GCredit, J).Value = Format(Agl.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
                    DblCredit_Total = DblCredit_Total + Val(FGMain(GCredit, J).Value)
                ElseIf Agl.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0 Then
                    J = FFindEmptyRow(GGRName)
                    FGMain(GGRCode, J).Value = Agl.Xnull(DTTemp.Rows(I).Item("GroupCode"))
                    If UCase(Agl.Xnull(DTTemp.Rows(I).Item("GroupNature"))) = "L" Then
                        FGMain(GGRName, J).Value = Agl.Xnull(DTTemp.Rows(I).Item("GName"))
                    Else
                        FGMain(GGRName, J).Value = Agl.Xnull(DTTemp.Rows(I).Item("ContraGroupName"))
                    End If
                    FGMain(GDebit, J).Value = Format(Agl.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
                    DblDebit_Total = DblDebit_Total + Val(FGMain(GDebit, J).Value)
                End If
                FGMain(GGR_SG, J).Value = "A"
                FGMain.Rows(J).DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Bold)
            Next
            DTTemp.Clear()
            DTTemp.Dispose()

            If DHSMain.DblClosingStock > 0 Then
                J = FFindEmptyRow(GGRNameCredit)
                FGMain(GGRNameCredit, J).Value = "Closing Stock"
                FGMain(GCredit, J).Value = DHSMain.DblClosingStock
                DblCredit_Total = DblCredit_Total + DHSMain.DblClosingStock
                FGMain(GGRNameCredit, J).Style.Font = New Font("Arial", 9, FontStyle.Regular)
                FGMain(GCredit, J).Style.Font = New Font("Arial", 9, FontStyle.Regular)
            End If

            DTTemp = FGetTRDDataTable()

            For I = 0 To DTTemp.Rows.Count - 1
                If Agl.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0 Then
                    DblNet_Profit_Loss = DblNet_Profit_Loss - Format(Agl.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
                ElseIf Agl.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0 Then
                    DblNet_Profit_Loss = DblNet_Profit_Loss + Format(Agl.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
                End If
            Next
            DTTemp.Clear()
            DTTemp.Dispose()
            DTTemp = FGetPLDataTable()

            If DHSMain.DblClosingStock > 0 Then DblNet_Profit_Loss = DblNet_Profit_Loss + DHSMain.DblClosingStock

            For I = 0 To DTTemp.Rows.Count - 1
                If Agl.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0 Then
                    DblNet_Profit_Loss = DblNet_Profit_Loss - Format(Agl.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
                ElseIf Agl.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0 Then
                    DblNet_Profit_Loss = DblNet_Profit_Loss + Format(Agl.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
                End If
            Next

            If DblNet_Profit_Loss < 0 Then
                J = FFindEmptyRow(GGRNameCredit)
                If J < FFindEmptyRow(GGRName) Then J = FFindEmptyRow(GGRName)

                FGMain(GGRNameCredit, J).Value = "Net Loss"
                FGMain(GCredit, J).Value = Format(Math.Abs(DblNet_Profit_Loss), "0.00")
                DblCredit_Total = DblCredit_Total + Format(Math.Abs(DblNet_Profit_Loss), "0.00")
                FGMain(GGRNameCredit, J).Style.Font = New Font("Arial", 9, FontStyle.Bold)
                FGMain(GCredit, J).Style.Font = New Font("Arial", 9, FontStyle.Bold)
                FGMain(GGRNameCredit, J).Style.ForeColor = Color.Red
                FGMain(GCredit, J).Style.ForeColor = Color.Red
                FGMain(GGR_SG, J).Value = CnsProfitAndLoss
            ElseIf DblNet_Profit_Loss > 0 Then
                J = FFindEmptyRow(GGRName)
                If J < FFindEmptyRow(GGRNameCredit) Then J = FFindEmptyRow(GGRNameCredit)
                FGMain(GGRName, J).Value = "Net Profit"
                FGMain(GDebit, J).Value = Format(Math.Abs(DblNet_Profit_Loss), "0.00")
                DblDebit_Total = DblDebit_Total + Format(Math.Abs(DblNet_Profit_Loss), "0.00")
                FGMain(GGRName, J).Style.Font = New Font("Arial", 9, FontStyle.Bold)
                FGMain(GDebit, J).Style.Font = New Font("Arial", 9, FontStyle.Bold)
                FGMain(GGRName, J).Style.ForeColor = Color.Green
                FGMain(GDebit, J).Style.ForeColor = Color.Green
                FGMain(GGR_SG, J).Value = CnsProfitAndLoss
            End If

            If (DblDebit_Total - DblCredit_Total) > 0.001 Then
                J = FFindEmptyRow(GGRNameCredit)
                FGMain(GGRNameCredit, J).Value = "Difference In Trial Balance"
                FGMain(GCredit, J).Value = Format((DblDebit_Total - DblCredit_Total), "0.00")
                DblCredit_Total = DblCredit_Total + Format((DblDebit_Total - DblCredit_Total), "0.00")
                FGMain(GGRNameCredit, J).Style.ForeColor = Color.Red
                FGMain(GCredit, J).Style.ForeColor = Color.Red
            ElseIf (DblCredit_Total - DblDebit_Total) > 0.001 Then
                J = FFindEmptyRow(GGRName)
                FGMain(GGRName, J).Value = "Difference In Trial Balance"
                FGMain(GDebit, J).Value = Format((DblCredit_Total - DblDebit_Total), "0.00")
                DblDebit_Total = DblDebit_Total + Format((DblCredit_Total - DblDebit_Total), "0.00")
                FGMain(GGRName, J).Style.ForeColor = Color.Green
                FGMain(GDebit, J).Style.ForeColor = Color.Green
            End If

            FGMain.Rows(FGMain.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DarkGray
            FGMain.Rows(FGMain.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.White
            FGMain.Rows(FGMain.Rows.Count - 1).DefaultCellStyle.Font = New Font("Arial", 11, FontStyle.Bold)
            FGMain(GDebit, FGMain.Rows.Count - 1).Value = Format(DblDebit_Total, "0.00")
            FGMain(GCredit, FGMain.Rows.Count - 1).Value = Format(DblCredit_Total, "0.00")
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub
    Private Function FGetTRDDataTable() As DataTable
        Dim StrCondition1 As String = ""
        Dim DTTemp As DataTable

        Try
            StrCondition1 = " Where LG.V_Date <= " & AgL.ConvertDate(DHSMain.StrToDate) & " And LG.Site_Code In (" & DHSMain.StrSiteCode & ") "
            StrCondition1 += " And LG.V_Date >= (Case When Ag.GroupNature in ('R','E') Then '" & AgL.PubStartDate & "' Else '01/Jan/1900' End) "

            '========== For Detail Section =======
            StrSQLQuery = "Select	(Case IsNull(AG1.GroupCode,'') When '' Then IsNull(AG.GroupCode,'') "
            StrSQLQuery = StrSQLQuery + "Else IsNull(AG1.GroupCode,'') End)  As GroupCode, "
            StrSQLQuery = StrSQLQuery + "Max(Case IsNull(AG1.GroupName,'') When '' Then IsNull(AG.GroupName,'') "
            StrSQLQuery = StrSQLQuery + "Else IsNull(AG1.GroupName,'') End)  As GName, "
            StrSQLQuery = StrSQLQuery + "(Case When (IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0))>0 Then  "
            StrSQLQuery = StrSQLQuery + "(IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
            StrSQLQuery = StrSQLQuery + "(Case When (IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0))>0 Then "
            StrSQLQuery = StrSQLQuery + "(IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr, "
            StrSQLQuery = StrSQLQuery + "Max(AG.ContraGroupName) As ContraGroupName,Max(AG.GroupNature) As GroupNature "
            StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode  Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroup AG On AG.GroupCode=SG.GroupCode Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroupPath AGP On AGP.GroupCode=AG.GroupCode And AGP.SNo=" & IntLevel & " Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroup AG1 On AG1.GroupCode=AGP.GroupUnder "
            StrSQLQuery = StrSQLQuery + StrCondition1
            StrSQLQuery = StrSQLQuery + "And AG.GroupNature In ('R','E') "

            '=================== For Only PL Data =====================
            StrSQLQuery = StrSQLQuery + "And (AG.Nature In ('Direct','Purchase','Sales') Or "
            StrSQLQuery = StrSQLQuery + "AG1.Nature In ('Direct','Purchase','Sales')) "
            '==========================================================

            StrSQLQuery = StrSQLQuery + "Group By (Case IsNull(AG1.GroupCode,'') When '' Then IsNull(AG.GroupCode,'') "
            StrSQLQuery = StrSQLQuery + "Else IsNull(AG1.GroupCode,'') End) "
            StrSQLQuery = StrSQLQuery + "Having (IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) <> 0 "
            StrSQLQuery = StrSQLQuery + "Order By Max(Case IsNull(AG1.GroupName,'') When '' Then IsNull(AG.GroupName,'') "
            StrSQLQuery = StrSQLQuery + "Else IsNull(AG1.GroupName,'') End) "

            DTTemp = cmain.FGetDatTable(StrSQLQuery, Agl.Gcn)
        Catch ex As Exception
            DTTemp = Nothing
        End Try

        Return DTTemp
    End Function
    Private Function FGetPLDataTable() As DataTable
        Dim StrCondition1 As String = ""
        Dim DTTemp As DataTable

        Try
            StrCondition1 = " Where LG.V_Date <= " & AgL.ConvertDate(DHSMain.StrToDate) & " And LG.Site_Code In (" & DHSMain.StrSiteCode & ") "
            StrCondition1 += " And LG.V_Date >= (Case When Ag.GroupNature in ('R','E') Then '" & AgL.PubStartDate & "' Else '01/Jan/1900' End) "
            '========== For Detail Section =======
            StrSQLQuery = "Select	(Case IsNull(AG1.GroupCode,'') When '' Then IsNull(AG.GroupCode,'') "
            StrSQLQuery = StrSQLQuery + "Else IsNull(AG1.GroupCode,'') End)  As GroupCode, "
            StrSQLQuery = StrSQLQuery + "Max(Case IsNull(AG1.GroupName,'') When '' Then IsNull(AG.GroupName,'') "
            StrSQLQuery = StrSQLQuery + "Else IsNull(AG1.GroupName,'') End)  As GName, "
            StrSQLQuery = StrSQLQuery + "(Case When (IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0))>0 Then  "
            StrSQLQuery = StrSQLQuery + "(IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
            StrSQLQuery = StrSQLQuery + "(Case When (IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0))>0 Then "
            StrSQLQuery = StrSQLQuery + "(IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr, "
            StrSQLQuery = StrSQLQuery + "Max(AG.ContraGroupName) As ContraGroupName,Max(AG.GroupNature) As GroupNature "
            StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode  Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroup AG On AG.GroupCode=SG.GroupCode Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroupPath AGP On AGP.GroupCode=AG.GroupCode And AGP.SNo=" & IntLevel & " Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroup AG1 On AG1.GroupCode=AGP.GroupUnder "
            StrSQLQuery = StrSQLQuery + StrCondition1
            StrSQLQuery = StrSQLQuery + "And AG.GroupNature In ('R','E') "

            '=================== For Only PL Data =====================
            StrSQLQuery = StrSQLQuery + "And (AG.Nature Not In ('Direct','Purchase','Sales') Or "
            StrSQLQuery = StrSQLQuery + "AG1.Nature Not In ('Direct','Purchase','Sales')) "
            '==========================================================

            StrSQLQuery = StrSQLQuery + "Group By (Case IsNull(AG1.GroupCode,'') When '' Then IsNull(AG.GroupCode,'') "
            StrSQLQuery = StrSQLQuery + "Else IsNull(AG1.GroupCode,'') End) "
            StrSQLQuery = StrSQLQuery + "Having (IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) <> 0 "
            StrSQLQuery = StrSQLQuery + "Order By Max(Case IsNull(AG1.GroupName,'') When '' Then IsNull(AG.GroupName,'') "
            StrSQLQuery = StrSQLQuery + "Else IsNull(AG1.GroupName,'') End) "

            DTTemp = cmain.FGetDatTable(StrSQLQuery, Agl.Gcn)
        Catch ex As Exception
            DTTemp = Nothing
        End Try

        Return DTTemp
    End Function
    Private Sub FProfitAndLoss_Disp()
        Dim DTTemp As DataTable
        Dim DblDebit_Total As Double, DblCredit_Total As Double, DblGrossProfit As Double, DblNetProfit As Double
        Dim I As Integer, J As Integer, IntFindRowFrom As Integer

        Try
            IniGrid(ClsStructure.DisplayType.ProfitAndLoss)
            LblDisplay.Text = "Profit And Loss"
            LblDisplay.Tag = CnsProfitAndLoss
            LblDisplayDate.Text = "As On : " & DHSMain.StrToDate
            LblDisplaySite.Text = DHSMain.StrSiteName
            '========== For Detail Section =======


            '========= For Trading A/c ===========
            DTTemp = FGetTRDDataTable()

            DblDebit_Total = 0
            DblCredit_Total = 0
            For I = 0 To DTTemp.Rows.Count - 1
                If Agl.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0 Then
                    J = FFindEmptyRow(GGRNameCredit)
                    FGMain(GGRCodeCredit, J).Value = Agl.Xnull(DTTemp.Rows(I).Item("GroupCode"))
                    If UCase(Agl.Xnull(DTTemp.Rows(I).Item("GroupNature"))) = "R" Then
                        FGMain(GGRNameCredit, J).Value = Agl.Xnull(DTTemp.Rows(I).Item("GName"))
                    Else
                        FGMain(GGRNameCredit, J).Value = Agl.Xnull(DTTemp.Rows(I).Item("ContraGroupName"))
                    End If
                    FGMain(GCredit, J).Value = Format(Agl.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
                    DblCredit_Total = DblCredit_Total + Format(Agl.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
                ElseIf Agl.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0 Then
                    J = FFindEmptyRow(GGRName)
                    FGMain(GGRCode, J).Value = Agl.Xnull(DTTemp.Rows(I).Item("GroupCode"))
                    If UCase(Agl.Xnull(DTTemp.Rows(I).Item("GroupNature"))) = "E" Then
                        FGMain(GGRName, J).Value = Agl.Xnull(DTTemp.Rows(I).Item("GName"))
                    Else
                        FGMain(GGRName, J).Value = Agl.Xnull(DTTemp.Rows(I).Item("ContraGroupName"))
                    End If
                    FGMain(GDebit, J).Value = Format(Agl.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
                    DblDebit_Total = DblDebit_Total + Format(Agl.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
                End If
                FGMain(GGR_SG, J).Value = "A"
                FGMain.Rows(J).DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Bold)
            Next

            If DHSMain.DblClosingStock > 0 Then
                J = FFindEmptyRow(GGRNameCredit)
                FGMain(GGRNameCredit, J).Value = "Closing Stock"
                FGMain(GCredit, J).Value = DHSMain.DblClosingStock
                DblCredit_Total = DblCredit_Total + DHSMain.DblClosingStock
                FGMain(GGRNameCredit, J).Style.Font = New Font("Arial", 9, FontStyle.Regular)
                FGMain(GCredit, J).Style.Font = New Font("Arial", 9, FontStyle.Regular)
            End If

            DblGrossProfit = (DblDebit_Total - DblCredit_Total)
            If (DblDebit_Total - DblCredit_Total) > 0 Then
                J = FFindEmptyRow(GGRNameCredit)
                FGMain(GGRNameCredit, J).Value = "Gross Loss"
                FGMain(GCredit, J).Value = Format((DblDebit_Total - DblCredit_Total), "0.00")
                DblCredit_Total = DblCredit_Total + Format((DblDebit_Total - DblCredit_Total), "0.00")
                DblDebit_Total = DblCredit_Total
                FGMain(GGRNameCredit, J).Style.Font = New Font("Arial", 9, FontStyle.Bold)
                FGMain(GCredit, J).Style.Font = New Font("Arial", 9, FontStyle.Bold)
                FGMain(GGRNameCredit, J).Style.ForeColor = Color.Red
                FGMain(GCredit, J).Style.ForeColor = Color.Red
            ElseIf (DblCredit_Total - DblDebit_Total) > 0 Then
                J = FFindEmptyRow(GGRName)
                FGMain(GGRName, J).Value = "Gross Profit"
                FGMain(GDebit, J).Value = Format((DblCredit_Total - DblDebit_Total), "0.00")
                DblDebit_Total = DblDebit_Total + Format((DblCredit_Total - DblDebit_Total), "0.00")
                DblCredit_Total = DblDebit_Total
                FGMain(GGRName, J).Style.Font = New Font("Arial", 9, FontStyle.Bold)
                FGMain(GDebit, J).Style.Font = New Font("Arial", 9, FontStyle.Bold)
                FGMain(GGRName, J).Style.ForeColor = Color.Green
                FGMain(GDebit, J).Style.ForeColor = Color.Green
            End If

            If DblDebit_Total > 0 Then
                FGMain.Rows.Add()
                FGMain.Rows.Add()
                FGMain.Rows(FGMain.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DarkGray
                FGMain.Rows(FGMain.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.White
                FGMain.Rows(FGMain.Rows.Count - 1).DefaultCellStyle.Font = New Font("Arial", 11, FontStyle.Bold)
                FGMain(GDebit, FGMain.Rows.Count - 1).Value = Format(DblDebit_Total, "0.00")
                FGMain(GCredit, FGMain.Rows.Count - 1).Value = Format(DblCredit_Total, "0.00")
                FGMain.Rows.Add()
            End If
            '==========================================


            '============ For P/L A/c =================
            IntFindRowFrom = FGMain.Rows.Count
            If DblGrossProfit > 0 Then
                J = FFindEmptyRow(GGRName, IntFindRowFrom)
                FGMain(GGRName, J).Value = "Gross Loss"
                FGMain(GDebit, J).Value = Format(Math.Abs(DblGrossProfit), "0.00")
                FGMain(GGRName, J).Style.Font = New Font("Arial", 9, FontStyle.Bold)
                FGMain(GDebit, J).Style.Font = New Font("Arial", 9, FontStyle.Bold)
                FGMain(GGRName, J).Style.ForeColor = Color.Red
                FGMain(GDebit, J).Style.ForeColor = Color.Red
            ElseIf DblGrossProfit < 0 Then
                J = FFindEmptyRow(GGRNameCredit, IntFindRowFrom)
                FGMain(GGRNameCredit, J).Value = "Gross Profit"
                FGMain(GCredit, J).Value = Format(Math.Abs(DblGrossProfit), "0.00")
                FGMain(GGRNameCredit, J).Style.Font = New Font("Arial", 9, FontStyle.Bold)
                FGMain(GCredit, J).Style.Font = New Font("Arial", 9, FontStyle.Bold)
                FGMain(GGRNameCredit, J).Style.ForeColor = Color.Green
                FGMain(GCredit, J).Style.ForeColor = Color.Green
            End If
            DTTemp = FGetPLDataTable()

            DblDebit_Total = 0
            DblCredit_Total = 0
            For I = 0 To DTTemp.Rows.Count - 1
                If Agl.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0 Then
                    J = FFindEmptyRow(GGRNameCredit, IntFindRowFrom)
                    FGMain(GGRCodeCredit, J).Value = Agl.Xnull(DTTemp.Rows(I).Item("GroupCode"))
                    If UCase(Agl.Xnull(DTTemp.Rows(I).Item("GroupNature"))) = "R" Then
                        FGMain(GGRNameCredit, J).Value = Agl.Xnull(DTTemp.Rows(I).Item("GName"))
                    Else
                        FGMain(GGRNameCredit, J).Value = Agl.Xnull(DTTemp.Rows(I).Item("ContraGroupName"))
                    End If
                    FGMain(GCredit, J).Value = Format(Agl.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
                    DblCredit_Total = DblCredit_Total + Format(Agl.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
                ElseIf Agl.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0 Then
                    J = FFindEmptyRow(GGRName, IntFindRowFrom)
                    FGMain(GGRCode, J).Value = Agl.Xnull(DTTemp.Rows(I).Item("GroupCode"))
                    If UCase(Agl.Xnull(DTTemp.Rows(I).Item("GroupNature"))) = "E" Then
                        FGMain(GGRName, J).Value = Agl.Xnull(DTTemp.Rows(I).Item("GName"))
                    Else
                        FGMain(GGRName, J).Value = Agl.Xnull(DTTemp.Rows(I).Item("ContraGroupName"))
                    End If
                    FGMain(GDebit, J).Value = Format(Agl.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
                    DblDebit_Total = DblDebit_Total + Format(Agl.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
                End If
                FGMain(GGR_SG, J).Value = "A"
                FGMain.Rows(J).DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Bold)
            Next

            DblNetProfit = DblGrossProfit + (DblDebit_Total - DblCredit_Total)
            If DblNetProfit > 0 Then
                J = FFindEmptyRow(GGRNameCredit, IntFindRowFrom)
                FGMain(GGRNameCredit, J).Value = "Net Loss"
                FGMain(GCredit, J).Value = Format(Math.Abs(DblNetProfit), "0.00")
                DblCredit_Total = DblCredit_Total + Format(Math.Abs(DblNetProfit), "0.00")
                DblDebit_Total = DblCredit_Total
                FGMain(GGRNameCredit, J).Style.Font = New Font("Arial", 9, FontStyle.Bold)
                FGMain(GCredit, J).Style.Font = New Font("Arial", 9, FontStyle.Bold)
                FGMain(GGRNameCredit, J).Style.ForeColor = Color.Red
                FGMain(GCredit, J).Style.ForeColor = Color.Red
            ElseIf DblNetProfit < 0 Then
                J = FFindEmptyRow(GGRName, IntFindRowFrom)
                FGMain(GGRName, J).Value = "Net Profit"
                FGMain(GDebit, J).Value = Format(Math.Abs(DblNetProfit), "0.00")
                DblDebit_Total = DblDebit_Total + Format(Math.Abs(DblNetProfit), "0.00")
                DblCredit_Total = DblDebit_Total
                FGMain(GGRName, J).Style.Font = New Font("Arial", 9, FontStyle.Bold)
                FGMain(GDebit, J).Style.Font = New Font("Arial", 9, FontStyle.Bold)
                FGMain(GGRName, J).Style.ForeColor = Color.Green
                FGMain(GDebit, J).Style.ForeColor = Color.Green
            End If

            FGMain.Rows.Add()
            FGMain.Rows.Add()
            FGMain.Rows(FGMain.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DarkGray
            FGMain.Rows(FGMain.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.White
            FGMain.Rows(FGMain.Rows.Count - 1).DefaultCellStyle.Font = New Font("Arial", 11, FontStyle.Bold)
            FGMain(GDebit, FGMain.Rows.Count - 1).Value = Format(DblDebit_Total, "0.00")
            FGMain(GCredit, FGMain.Rows.Count - 1).Value = Format(DblCredit_Total, "0.00")
            FGMain.Rows.Add()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub FTrailBalance_Disp()
        Dim StrCondition1 As String = ""
        Dim DTTemp As DataTable
        Dim DblDebit_Total As Double, DblCredit_Total As Double
        Dim StrConditionZeroBal As String = ""
        Dim I As Integer
        Try
            IniGrid(ClsStructure.DisplayType.TrailBalance)
            LblDisplay.Text = "Trial Balance"
            LblDisplay.Tag = CnsTrialBalance
            LblDisplayDate.Text = "As On : " & DHSMain.StrToDate
            LblDisplaySite.Text = DHSMain.StrSiteName
            If UCase(DHSMain.StrZeroBalace) = "N" Then StrConditionZeroBal = "Having (Round(IsNull(Sum(LG.AmtDr),0),2)-Round(IsNull(Sum(LG.AmtCr),0),2)) <> 0 "
            StrCondition1 = " Where LG.V_Date <= " & AgL.ConvertDate(DHSMain.StrToDate) & " And LG.Site_Code In (" & DHSMain.StrSiteCode & ")"
            StrCondition1 += " And LG.V_Date >= (Case When Ag.GroupNature in ('R','E') Then '" & AgL.PubStartDate & "' Else '01/Jan/1900' End) "
            '========== For Detail Section =======

            StrSQLQuery = "Select	(Case IsNull(AG1.GroupCode,'') When '' Then IsNull(AG.GroupCode,'') "
            StrSQLQuery = StrSQLQuery + "Else IsNull(AG1.GroupCode,'') End)  As GroupCode, "
            StrSQLQuery = StrSQLQuery + "Max(Case IsNull(AG1.GroupName,'') When '' Then IsNull(AG.GroupName,'') "
            StrSQLQuery = StrSQLQuery + "Else IsNull(AG1.GroupName,'') End)  As GName, "
            StrSQLQuery = StrSQLQuery + "(Case When (IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0))>0 Then  "
            StrSQLQuery = StrSQLQuery + "(IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
            StrSQLQuery = StrSQLQuery + "(Case When (IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0))>0 Then "
            StrSQLQuery = StrSQLQuery + "(IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr "
            StrSQLQuery = StrSQLQuery + "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode  Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroup AG On AG.GroupCode=SG.GroupCode Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroupPath AGP On AGP.GroupCode=AG.GroupCode And AGP.SNo=" & IntLevel & " Left Join "
            StrSQLQuery = StrSQLQuery + "AcGroup AG1 On AG1.GroupCode=AGP.GroupUnder "
            StrSQLQuery = StrSQLQuery + StrCondition1

            StrSQLQuery = StrSQLQuery + "Group By (Case IsNull(AG1.GroupCode,'') When '' Then IsNull(AG.GroupCode,'') "
            StrSQLQuery = StrSQLQuery + "Else IsNull(AG1.GroupCode,'') End) "
            StrSQLQuery = StrSQLQuery + StrConditionZeroBal
            StrSQLQuery = StrSQLQuery + "Order By Max(Case IsNull(AG1.GroupName,'') When '' Then IsNull(AG.GroupName,'') "
            StrSQLQuery = StrSQLQuery + "Else IsNull(AG1.GroupName,'') End) "

            DTTemp = cmain.FGetDatTable(StrSQLQuery, Agl.Gcn)

            If DTTemp.Rows.Count > 0 Then
                FGMain.Rows.Add(DTTemp.Rows.Count + 1)
            End If
            DblDebit_Total = 0
            DblCredit_Total = 0
            For I = 0 To DTTemp.Rows.Count - 1
                FGMain(GGR_SG, I).Value = "A"
                FGMain(GGRCode, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("GroupCode"))
                FGMain(GGRName, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("GName"))
                FGMain(GDebit, I).Value = IIf(Agl.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0, Format(Agl.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00"), "")
                FGMain(GCredit, I).Value = IIf(Agl.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0, Format(Agl.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00"), "")
                DblDebit_Total = DblDebit_Total + Format(Agl.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
                DblCredit_Total = DblCredit_Total + Format(Agl.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
                FGMain.Rows(I).DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Bold)
            Next

            If (DblDebit_Total - DblCredit_Total) > 0 Then
                FGMain(GGRName, I).Value = "Difference In Trial Balance"
                FGMain(GCredit, I).Value = Format((DblDebit_Total - DblCredit_Total), "0.00")
                DblCredit_Total = DblCredit_Total + Format((DblDebit_Total - DblCredit_Total), "0.00")
                FGMain.Rows(I).DefaultCellStyle.ForeColor = Color.Red
            ElseIf (DblCredit_Total - DblDebit_Total) > 0 Then
                FGMain(GGRName, I).Value = "Difference In Trial Balance"
                FGMain(GDebit, I).Value = Format((DblCredit_Total - DblDebit_Total), "0.00")
                DblDebit_Total = DblDebit_Total + Format((DblCredit_Total - DblDebit_Total), "0.00")
                FGMain.Rows(I).DefaultCellStyle.ForeColor = Color.Red
            End If

            FGMain.Rows(I + 1).DefaultCellStyle.BackColor = Color.DarkGray
            FGMain.Rows(I + 1).DefaultCellStyle.ForeColor = Color.White
            FGMain.Rows(I + 1).DefaultCellStyle.Font = New Font("Arial", 11, FontStyle.Bold)
            FGMain(GDebit, I + 1).Value = Format(DblDebit_Total, "0.00")
            FGMain(GCredit, I + 1).Value = Format(DblCredit_Total, "0.00")
        Catch ex As Exception
        End Try
    End Sub
    Private Sub FDTrailBalance_Disp()
        Dim StrCondition1 As String = "", StrConditionOP As String = ""
        Dim DTTemp As DataTable
        Dim DblDebit_Total As Double, DblCredit_Total As Double
        Dim DblCLDR As Double, DblCLCR As Double
        Dim StrConditionZeroBal As String = ""
        Dim StrConditionAcGroup As String = ""
        Dim I As Integer
        Try
            IniGrid(ClsStructure.DisplayType.DTrailBalance)
            LblDisplay.Text = "Detail Trial Balance"
            LblDisplay.Tag = CnsDTrialBalance
            LblDisplayDate.Text = "Date : " & DHSMain.StrFromDate & " To " & DHSMain.StrToDate
            LblDisplaySite.Text = DHSMain.StrSiteName


            If UCase(DHSMain.StrZeroBalace) = "N" Then StrConditionZeroBal = "Having (IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) <> 0 "
            StrConditionOP = " Where LG.V_Date < " & AgL.ConvertDate(DHSMain.StrFromDate) & " And LG.Site_Code In (" & DHSMain.StrSiteCode & ")"
            StrConditionOP += " And LG.V_Date >= (Case When Ag.GroupNature in ('R','E') Then '" & AgL.PubStartDate & "' Else '01/Jan/1900' End) "
            StrCondition1 = " Where (LG.V_Date Between " & AgL.ConvertDate(DHSMain.StrFromDate) & " And " & AgL.ConvertDate(DHSMain.StrToDate) & " ) And LG.Site_Code In (" & DHSMain.StrSiteCode & ")"
            If DHSMain.StrAcGroup <> "" Then StrConditionAcGroup = " And Sg.GroupCode In  (" & DHSMain.StrAcGroup & ") "
            If DHSMain.StrAcGroup <> "" Then StrConditionAcGroup = " And LG.CostCenter In  (" & DHSMain.StrCostCenter & ") "
            '========== For Detail Section =======

            StrSQLQuery = "Select SubCode, "
            StrSQLQuery += "Max(SName) As SName, "
            StrSQLQuery += "IsNull(Sum(OPBal),0) As OPBal, "
            StrSQLQuery += "IsNull(Sum(AmtDr),0) As AmtDr, "
            StrSQLQuery += "IsNull(Sum(AmtCr),0) As AmtCr "
            StrSQLQuery += "From ( "
            StrSQLQuery += "Select IsNull(SG.SubCode,'') As SubCode, "
            StrSQLQuery += "(IsNull(Max(SG.Name),'') + ' - ' + IsNull(Max(CT.CityName),'')) As SName, "
            StrSQLQuery += "(IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) As OPBal, "
            StrSQLQuery += "0 As AmtDr, "
            StrSQLQuery += "0 As AmtCr "
            StrSQLQuery += "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode  "
            StrSQLQuery += "Left Join AcGroup Ag On Ag.GroupCode=SG.GroupCode "
            StrSQLQuery += "Left Join City CT On CT.CityCode=SG.CityCode "
            StrSQLQuery += StrConditionOP & StrConditionAcGroup
            StrSQLQuery += "Group By IsNull(SG.SubCode,'') "
            StrSQLQuery += "Having (IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) <> 0 "
            StrSQLQuery += "Union All "
            StrSQLQuery += "Select	IsNull(SG.SubCode,'') As SubCode, "
            StrSQLQuery += "(IsNull(Max(SG.Name),'') + ' - ' + IsNull(Max(CT.CityName),'')) As SName, "
            StrSQLQuery += "0 As OPBal, "
            StrSQLQuery += "(Case When (IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0))>0 Then  "
            StrSQLQuery += "(IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
            StrSQLQuery += "(Case When (IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0))>0 Then "
            StrSQLQuery += "(IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr "
            StrSQLQuery += "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode Left Join "
            StrSQLQuery += "City CT On CT.CityCode=SG.CityCode "
            StrSQLQuery += StrCondition1 & StrConditionAcGroup
            StrSQLQuery += "Group By IsNull(SG.SubCode,'') "
            StrSQLQuery += StrConditionZeroBal
            StrSQLQuery += ") As Tmp "
            StrSQLQuery += "Group By SubCode "
            StrSQLQuery += "Order By IsNull(Max(SName),'')  "

            DTTemp = cmain.FGetDatTable(StrSQLQuery, Agl.Gcn)

            If DTTemp.Rows.Count > 0 Then
                FGMain.Rows.Add(DTTemp.Rows.Count + 1)
            End If
            DblDebit_Total = 0
            DblCredit_Total = 0
            For I = 0 To DTTemp.Rows.Count - 1
                DblCLCR = 0
                DblCLDR = 0
                FGMain(GGR_SG, I).Value = "S"
                FGMain(GGRCode, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("SubCode"))
                FGMain(GGRName, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("SName"))
                FGMain(GOpening, I).Value = IIf(Agl.VNull(DTTemp.Rows(I).Item("OPBal")) <> 0, Format(Math.Abs(Agl.VNull(DTTemp.Rows(I).Item("OPBal"))), "0.00"), "")
                FGMain(GDR_CR_OP, I).Value = IIf(Agl.VNull(DTTemp.Rows(I).Item("OPBal")) > 0, "Dr", "Cr")
                If Agl.VNull(DTTemp.Rows(I).Item("OPBal")) = 0 Then FGMain(GDR_CR_OP, I).Value = ""
                If Agl.VNull(DTTemp.Rows(I).Item("OPBal")) > 0 Then DblCLDR = Math.Abs(Agl.VNull(DTTemp.Rows(I).Item("OPBal"))) Else DblCLCR = Math.Abs(Agl.VNull(DTTemp.Rows(I).Item("OPBal")))

                FGMain(GDebit, I).Value = IIf(Agl.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0, Format(Agl.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00"), "")
                FGMain(GCredit, I).Value = IIf(Agl.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0, Format(Agl.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00"), "")
                DblDebit_Total = DblDebit_Total + Format(Agl.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
                DblCredit_Total = DblCredit_Total + Format(Agl.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
                DblCLDR += Val(FGMain(GDebit, I).Value)
                DblCLCR += Val(FGMain(GCredit, I).Value)
                FGMain(GClosing, I).Value = IIf((DblCLDR - DblCLCR) <> 0, Format(Math.Abs(DblCLDR - DblCLCR), "0.00"), "")
                FGMain(GDR_CR_CL, I).Value = IIf((DblCLDR - DblCLCR) > 0, "Dr", "Cr")
                If (DblCLDR - DblCLCR) = 0 Then FGMain(GDR_CR_CL, I).Value = ""
                FGMain.Rows(I).DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
            Next

            If (DblDebit_Total - DblCredit_Total) > 0 Then
                FGMain(GGRName, I).Value = "Difference In Trial Balance"
                FGMain(GCredit, I).Value = Format((DblDebit_Total - DblCredit_Total), "0.00")
                DblCredit_Total = DblCredit_Total + Format((DblDebit_Total - DblCredit_Total), "0.00")
                FGMain.Rows(I).DefaultCellStyle.ForeColor = Color.Red
            ElseIf (DblCredit_Total - DblDebit_Total) > 0 Then
                FGMain(GGRName, I).Value = "Difference In Trial Balance"
                FGMain(GDebit, I).Value = Format((DblCredit_Total - DblDebit_Total), "0.00")
                DblDebit_Total = DblDebit_Total + Format((DblCredit_Total - DblDebit_Total), "0.00")
                FGMain.Rows(I).DefaultCellStyle.ForeColor = Color.Red
            End If

            FGMain.Rows(I + 1).DefaultCellStyle.BackColor = Color.DarkGray
            FGMain.Rows(I + 1).DefaultCellStyle.ForeColor = Color.White
            FGMain.Rows(I + 1).DefaultCellStyle.Font = New Font("Arial", 11, FontStyle.Bold)
            FGMain(GDebit, I + 1).Value = Format(DblDebit_Total, "0.00")
            FGMain(GCredit, I + 1).Value = Format(DblCredit_Total, "0.00")
        Catch ex As Exception
        End Try
    End Sub
    Private Sub FDisplay_Level_Group(ByVal StrForCode As String, ByVal StrForName As String)
        Dim StrCondition1 As String = ""
        Dim DTTemp As DataTable
        Dim DblDebit_Total As Double, DblCredit_Total As Double
        Dim StrConditionZeroBal As String = ""
        Dim I As Integer

        Try
            IniGrid(ClsStructure.DisplayType.GroupBalance)
            LblDisplay.Text = StrForName
            LblDisplay.Tag = StrForCode
            LblDisplayDate.Text = "As On : " & DHSMain.StrToDate
            LblDisplaySite.Text = DHSMain.StrSiteName
            If UCase(DHSMain.StrZeroBalace) = "N" Then StrConditionZeroBal = "Having (Round(IsNull(Sum(LG.AmtDr),0),2)-Round(IsNull(Sum(LG.AmtCr),0),2)) <> 0 "
            StrCondition1 = " Where LG.V_Date <= " & AgL.ConvertDate(DHSMain.StrToDate) & " And LG.Site_Code In (" & DHSMain.StrSiteCode & ") "
            StrCondition1 += " And LG.V_Date >= (Case When Ag.GroupNature in ('R','E') Then '" & AgL.PubStartDate & "' Else '01/Jan/1900' End) "

            '========== For Detail Section =======
            StrSQLQuery = "Select	(Case When IsNull(AG1.GroupCode,'')<>'' Then 'A+'+IsNull(AG1.GroupCode,'') "
            StrSQLQuery += "When IsNull(AG.GroupUnder,'')='" & StrForCode & "' Then 'A+'+IsNull(AG.GroupCode,'') "
            StrSQLQuery += "Else 'S+'+IsNull(SG.SubCode,'')  End)  As GroupCode, "
            StrSQLQuery += "Max(Case When IsNull(AG1.GroupCode,'')<>'' Then IsNull(AG1.GroupName,'') "
            StrSQLQuery += "When IsNull(AG.GroupUnder,'')='" & StrForCode & "' Then IsNull(AG.GroupName,'') "
            StrSQLQuery += "Else IsNull(SG.Name,'') + ' - ' + IsNull(CT.CityName,'') End)  As GName, "
            StrSQLQuery += "(Case When (IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0))>0 Then  "
            StrSQLQuery += "(IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
            StrSQLQuery += "(Case When (IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0))>0 Then "
            StrSQLQuery += "(IsNull(Sum(LG.AmtCr),0)-IsNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr "
            StrSQLQuery += "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode  Left Join "
            StrSQLQuery += "City CT On CT.CityCode=SG.CityCode Left Join "
            StrSQLQuery += "AcGroup AG On AG.GroupCode=SG.GroupCode Left Join "
            StrSQLQuery += "AcGroupPath AGP On AGP.GroupCode=AG.GroupCode And AGP.SNo=" & IntLevel & " Left Join "
            StrSQLQuery += "AcGroup AG1 On AG1.GroupCode=AGP.GroupUnder "
            StrSQLQuery += StrCondition1
            StrSQLQuery += "And (AG.GroupCode In "
            StrSQLQuery += "(Select GroupCode From AcGroupPath AGP Where AGP.GroupUnder='" & StrForCode & "') "
            StrSQLQuery += "Or AG.GroupCode='" & StrForCode & "') "

            StrSQLQuery += "Group By (Case When IsNull(AG1.GroupCode,'')<>'' Then 'A+'+IsNull(AG1.GroupCode,'') "
            StrSQLQuery += "When IsNull(AG.GroupUnder,'')='" & StrForCode & "' Then 'A+'+IsNull(AG.GroupCode,'') "
            StrSQLQuery += "Else 'S+'+IsNull(SG.SubCode,'')  End) "

            StrSQLQuery += StrConditionZeroBal

            StrSQLQuery += "Order By Max(Case When IsNull(AG1.GroupCode,'')<>'' Then IsNull(AG1.GroupName,'') "
            StrSQLQuery += "When IsNull(AG.GroupUnder,'')='" & StrForCode & "' Then IsNull(AG.GroupName,'') "
            StrSQLQuery += "Else IsNull(SG.Name,'') + ' - ' + IsNull(CT.CityName,'') End) "

            DTTemp = cmain.FGetDatTable(StrSQLQuery, Agl.Gcn)

            If DTTemp.Rows.Count > 0 Then
                FGMain.Rows.Add(DTTemp.Rows.Count + 1)
            Else
                FGMain.Rows.Add(1)
            End If
            DblDebit_Total = 0
            DblCredit_Total = 0
            For I = 0 To DTTemp.Rows.Count - 1
                FGMain(GGR_SG, I).Value = Mid(Agl.Xnull(DTTemp.Rows(I).Item("GroupCode")), 1, 1)
                FGMain(GGRCode, I).Value = Mid(Agl.Xnull(DTTemp.Rows(I).Item("GroupCode")), 3, Len(Agl.Xnull(DTTemp.Rows(I).Item("GroupCode"))))
                FGMain(GGRName, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("GName"))
                FGMain(GDebit, I).Value = IIf(Agl.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0, Format(Agl.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00"), "")
                FGMain(GCredit, I).Value = IIf(Agl.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0, Format(Agl.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00"), "")
                DblDebit_Total = DblDebit_Total + Format(Agl.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
                DblCredit_Total = DblCredit_Total + Format(Agl.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")

                If UCase(FGMain(GGR_SG, I).Value) = "S" Then
                    FGMain.Rows(I).DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
                Else
                    FGMain.Rows(I).DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Bold)
                End If
            Next

            If (DblDebit_Total - DblCredit_Total) > 0 Then
                FGMain(GGRName, I + 1).Value = "Balance"
                FGMain(GDebit, I + 1).Value = Format((DblDebit_Total - DblCredit_Total), "0.00")
                DblDebit_Total = DblDebit_Total + Format((DblDebit_Total - DblCredit_Total), "0.00")
                FGMain.Rows(I + 1).DefaultCellStyle.ForeColor = Color.White
                FGMain.Rows(I + 1).DefaultCellStyle.Font = New Font("Arial", 11, FontStyle.Bold)
            ElseIf (DblCredit_Total - DblDebit_Total) > 0 Then
                FGMain(GGRName, I + 1).Value = "Balance"
                FGMain(GCredit, I + 1).Value = Format((DblCredit_Total - DblDebit_Total), "0.00")
                DblCredit_Total = DblCredit_Total + Format((DblCredit_Total - DblDebit_Total), "0.00")
                FGMain.Rows(I + 1).DefaultCellStyle.ForeColor = Color.White
                FGMain.Rows(I + 1).DefaultCellStyle.Font = New Font("Arial", 11, FontStyle.Bold)
            End If

            FGMain.Rows(I + 1).DefaultCellStyle.BackColor = Color.DarkGray
        Catch ex As Exception
        End Try
    End Sub
    Private Sub FDisplay_SubGroup(ByVal StrForCode As String, ByVal StrForName As String)
        Dim StrCondition1 As String = "", StrConditionOP As String = ""
        Dim DTTemp As DataTable
        Dim DblDebit_Total As Double, DblCredit_Total As Double, DblOpening As Double
        Dim I As Integer, J As Integer
        Dim Color_Main As Color, Color_A As Color, Color_B As Color

        Try
            IniGrid(ClsStructure.DisplayType.Ledger)
            LblDisplay.Text = StrForName
            LblDisplay.Tag = StrForCode
            LblDisplayDate.Text = "Date : " & DHSMain.StrFromDate & " To " & DHSMain.StrToDate
            LblDisplaySite.Text = DHSMain.StrSiteName
            StrConditionOP = " Where LG.V_Date < " & AgL.ConvertDate(DHSMain.StrFromDate) & " And LG.Site_Code In (" & DHSMain.StrSiteCode & ") "
            StrConditionOP += " And LG.V_Date >= (Case When Ag.GroupNature in ('R','E') Then '" & AgL.PubStartDate & "' Else '01/Jan/1900' End) "
            StrCondition1 = " Where (LG.V_Date Between  " & AgL.ConvertDate(DHSMain.StrFromDate) & " And " & AgL.ConvertDate(DHSMain.StrToDate) & ") And LG.Site_Code In (" & DHSMain.StrSiteCode & ") "

            '========== For Detail Section =======
            StrSQLQuery = "Select	Null As DocId,'Opening' As Narration, "
            StrSQLQuery = StrSQLQuery + "(Case When (IsNull(Sum(AmtDr),0)-IsNull(Sum(LG.AmtCr),0))>0 Then "
            StrSQLQuery = StrSQLQuery + "(IsNull(Sum(AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) Else 0 End) As AmtDr, "
            StrSQLQuery = StrSQLQuery + "(Case When (IsNull(Sum(AmtCr),0)-IsNull(Sum(LG.AmtDr),0))>0 Then "
            StrSQLQuery = StrSQLQuery + "(IsNull(Sum(AmtCr),0)-IsNull(Sum(LG.AmtDr),0)) Else 0 End) As AmtCr, "
            StrSQLQuery = StrSQLQuery + "Null As V_No,Null As V_Type,Null As V_Date,0 As SNo,'' As ContraText, "
            StrSQLQuery = StrSQLQuery + "Null As SerialNo "
            StrSQLQuery = StrSQLQuery + "From Ledger LG "
            StrSQLQuery = StrSQLQuery + "Left Join Subgroup SG On Sg.SubCode = LG.SubCode "
            StrSQLQuery = StrSQLQuery + "Left Join AcGroup AG On Ag.GroupCode = Sg.GroupCode "
            StrSQLQuery = StrSQLQuery + StrConditionOP
            StrSQLQuery = StrSQLQuery + "And LG.SubCode='" & StrForCode & "' "
            StrSQLQuery = StrSQLQuery + "Having (IsNull(Sum(AmtDr),0)-IsNull(Sum(LG.AmtCr),0))<>0 "

            StrSQLQuery = StrSQLQuery + "Union All "    
            StrSQLQuery = StrSQLQuery + "Select	LG.DocId,LG.Narration,LG.AmtDr,LG.AmtCr,LG.RecID As V_No,"
            StrSQLQuery = StrSQLQuery + "LG.V_Type,LG.V_Date,1 As SNo, ContraText,Lg.RecID as SerialNo "
            StrSQLQuery = StrSQLQuery + "From Ledger LG "
            StrSQLQuery = StrSQLQuery + "Left Join Voucher_Type VT On LG.V_Type=VT.V_Type "
            StrSQLQuery = StrSQLQuery + StrCondition1
            StrSQLQuery = StrSQLQuery + "And LG.SubCode='" & StrForCode & "' "
            StrSQLQuery = StrSQLQuery + "Order By SNo,V_Date,SerialNo,V_No"
            DTTemp = cmain.FGetDatTable(StrSQLQuery, Agl.Gcn)


            Color_A = Color.Linen
            Color_B = Color.Cornsilk

            DblDebit_Total = 0
            DblCredit_Total = 0
            For I = 0 To DTTemp.Rows.Count - 1

                FGMain.Rows.Add()
                J = FGMain.Rows.Count - 1

                If Color_Main = Color_B Then
                    Color_Main = Color_A
                Else
                    Color_Main = Color_B
                End If

                FGMain(GGR_SG, J).Value = "T"
                FGMain(GDocId, J).Value = Agl.Xnull(DTTemp.Rows(I).Item("DocId"))
                FGMain(GVNo, J).Value = Agl.Xnull(DTTemp.Rows(I).Item("V_No"))
                FGMain(GVType, J).Value = Agl.Xnull(DTTemp.Rows(I).Item("V_Type"))
                FGMain(GVDate, J).Value = Format(Agl.Xnull(DTTemp.Rows(I).Item("V_Date")), "Short Date")
                FGMain(GNarration, J).Value = Agl.Xnull(DTTemp.Rows(I).Item("Narration"))

                If Agl.VNull(DTTemp.Rows(I).Item("SNo")) <> 0 Then
                    FGMain(GDebit, J).Value = IIf(Agl.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0, Format(Agl.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00"), "")
                    FGMain(GCredit, J).Value = IIf(Agl.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0, Format(Agl.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00"), "")
                    DblDebit_Total = DblDebit_Total + Format(Agl.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
                    DblCredit_Total = DblCredit_Total + Format(Agl.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
                Else
                    DblOpening = IIf(Agl.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0, Val(Format(Agl.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")), 0 - Val(Format(Agl.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")))
                End If

                FGMain(GClosing, J).Value = IIf((DblOpening + DblDebit_Total - DblCredit_Total) <> 0, Format(Math.Abs(DblOpening + DblDebit_Total - DblCredit_Total), "0.00"), "")

                FGMain(GDR_CR_CL, J).Value = IIf((DblOpening + DblDebit_Total - DblCredit_Total) > 0, "Dr", "Cr")
                If (DblOpening + DblDebit_Total - DblCredit_Total) = 0 Then FGMain(GDR_CR_CL, J).Value = ""
                FGMain.Rows(J).DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
                FGMain.Rows(J).DefaultCellStyle.BackColor = Color_Main

                If Trim(UCase(DHSMain.StrShowContra)) = "Y" Then
                    If Trim(Agl.Xnull(DTTemp.Rows(I).Item("ContraText"))) <> "" Then
                        FGMain.Rows.Add()
                        J = FGMain.Rows.Count - 1
                        FGMain.Rows(J).DefaultCellStyle.BackColor = Color_Main
                        FGMain.Rows(J).DefaultCellStyle.Font = New Font("Courier New", 9, FontStyle.Italic)
                        FGMain(GNarration, J).Value = Agl.Xnull(DTTemp.Rows(I).Item("ContraText"))
                        FGMain(GNarration, J).Style.WrapMode = DataGridViewTriState.True
                        FGMain.Rows(J).Height = Split(Agl.Xnull(DTTemp.Rows(I).Item("ContraText")), vbCrLf).Length * 20
                    End If
                End If
            Next

            FGMain.Rows.Add()
            FGMain.Rows.Add()
            J = FGMain.Rows.Count - 1

            FGMain(GGRName, J).Value = ""
            FGMain(GNarration, J).Value = "Total"
            FGMain(GDebit, J).Value = Format(DblDebit_Total, "0.00")
            FGMain(GCredit, J).Value = Format(DblCredit_Total, "0.00")
            FGMain(GClosing, J).Value = IIf((DblOpening + DblDebit_Total - DblCredit_Total) <> 0, Format(Math.Abs(DblOpening + DblDebit_Total - DblCredit_Total), "0.00"), "")
            FGMain(GDR_CR_CL, J).Value = IIf((DblOpening + DblDebit_Total - DblCredit_Total) > 0, "Dr", "Cr")
            FGMain.Rows(J).DefaultCellStyle.ForeColor = Color.White
            FGMain.Rows(J).DefaultCellStyle.BackColor = Color.DarkGray
            FGMain.Rows(J).DefaultCellStyle.Font = New Font("Arial", 11, FontStyle.Bold)

        Catch ex As Exception
        End Try
    End Sub
    '============ Only For FDisplay_SubGroup ===============
    Private Sub FCalculateLedger()
        Dim I As Integer
        Dim DblDebit_Total As Double, DblCredit_Total As Double

        DblDebit_Total = 0
        DblCredit_Total = 0
        For I = 0 To FGMain.Rows.Count - 1
            If Trim(FGMain(GDocId, I).Value) = "" And Trim(FGMain(GNarration, I).Value) = "" Then Exit For

            DblDebit_Total += Val(FGMain(GDebit, I).Value)
            DblCredit_Total += Val(FGMain(GCredit, I).Value)

            FGMain(GClosing, I).Value = IIf((DblDebit_Total - DblCredit_Total) <> 0, Format(Math.Abs(DblDebit_Total - DblCredit_Total), "0.00"), "")
            FGMain(GDR_CR_CL, I).Value = IIf((DblDebit_Total - DblCredit_Total) > 0, "Dr", "Cr")
            If (DblDebit_Total - DblCredit_Total) = 0 Then FGMain(GDR_CR_CL, I).Value = ""
        Next

        FGMain(GGRName, FGMain.Rows.Count - 1).Value = ""
        FGMain(GNarration, FGMain.Rows.Count - 1).Value = "Total"
        FGMain(GDebit, FGMain.Rows.Count - 1).Value = Format(DblDebit_Total, "0.00")
        FGMain(GCredit, FGMain.Rows.Count - 1).Value = Format(DblCredit_Total, "0.00")
        FGMain.Rows(FGMain.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.White
        FGMain.Rows(FGMain.Rows.Count - 1).DefaultCellStyle.BackColor = Color.DarkGray
    End Sub
    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles BtnClose.Click, BtnSettings.Click, BtnBackWard.Click, BtnPrint.Click

        Dim FrmObj As FrmDisplayHierarchy_Settings
        Select Case sender.name
            Case BtnClose.Name
                Me.Close()
            Case BtnSettings.Name
                FrmObj = New FrmDisplayHierarchy_Settings(Me)
                FrmObj.ShowDialog()
                FrmObj.Dispose()
                FrmObj = Nothing

                If LblDisplay.Tag = CnsBalanceSheet Then
                    FBalanceSheet_Disp()
                ElseIf LblDisplay.Tag = CnsProfitAndLoss Then
                    FProfitAndLoss_Disp()
                ElseIf LblDisplay.Tag = CnsTrialBalance Then
                    FTrailBalance_Disp()
                ElseIf LblDisplay.Tag = CnsDTrialBalance Then
                    FDTrailBalance_Disp()
                ElseIf UCase(FGMain(GGR_SG, 0).Value) = "T" Then
                    FDisplay_SubGroup(LblDisplay.Tag, LblDisplay.Text)
                Else
                    FDisplay_Level_Group(LblDisplay.Tag, LblDisplay.Text)
                End If
            Case BtnBackWard.Name
                FBackward()
            Case BtnPrint.Name
                If LblDisplay.Tag = CnsBalanceSheet Then
                    FPrint("BalanceRep")
                ElseIf LblDisplay.Tag = CnsProfitAndLoss Then
                    FPrint("ProfitLossRep")
                ElseIf LblDisplay.Tag = CnsTrialBalance Then
                    FPrint("TrialRep")
                ElseIf LblDisplay.Tag = CnsDTrialBalance Then
                    FPrint("TrialDetail_Disp")
                ElseIf UCase(FGMain(GGR_SG, 0).Value) <> "T" Then
                    FPrint("TrialRep")
                ElseIf UCase(FGMain(GGR_SG, 0).Value) = "T" Then
                    FPrint("Ledger_Disp")
                End If
        End Select
    End Sub
    Private Sub FGMain_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles FGMain.CellDoubleClick
        Call FForward(e.RowIndex, e.ColumnIndex)
    End Sub
    Private Sub FGMain_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles FGMain.KeyPress
        Try

            FFilter(e.KeyChar, FGMain.CurrentCell.ColumnIndex)
        Catch ex As Exception

        End Try

    End Sub
    Private Sub FGMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FGMain.KeyDown
        If e.KeyCode = Keys.Enter Then
                    Call FForward(FGMain.CurrentCell.RowIndex, FGMain.CurrentCell.ColumnIndex)
        ElseIf e.KeyCode = Keys.Escape Then
            Call FBackward()
        ElseIf e.KeyCode = Keys.H And e.Control = True Then
            If FGMain.Columns(GVNo).Visible Then
                If FGMain.Rows.Count > 2 Then
                    If FGMain.CurrentCell.RowIndex <> (FGMain.Rows.Count - 1) And FGMain.CurrentCell.RowIndex <> (FGMain.Rows.Count - 2) Then
                        FGMain.Rows.RemoveAt(FGMain.CurrentCell.RowIndex)
                        FCalculateLedger()
                    End If
                End If
            End If
        End If
        FSetFilter(e.KeyCode)
    End Sub
    Private Sub FBackward()
        Dim StrForCode As String = "", StrForName As String = ""

        If IntLevel <= 2 Then
            If IntLevel <> 1 Then IntLevel = IntLevel - 1
            StrForCode = Mid(StrPreviousCode, InStrRev(StrPreviousCode, "||") + 2, Len(StrPreviousCode))
            If StrPreviousCode <> "" Then
                StrPreviousCode = Mid(StrPreviousCode, 1, InStrRev(StrPreviousCode, "||") - 1)
            End If
            If StrForCode = CnsBalanceSheet Then
                FBalanceSheet_Disp()
            ElseIf StrForCode = CnsProfitAndLoss Then
                FProfitAndLoss_Disp()
            ElseIf StrForCode = CnsTrialBalance Then
                FTrailBalance_Disp()
            ElseIf StrForCode = CnsDTrialBalance Then
                FDTrailBalance_Disp()
            End If
        ElseIf IntLevel > 1 Then
            IntLevel = IntLevel - 1
            StrForCode = Mid(StrPreviousCode, InStrRev(StrPreviousCode, "||") + 2, Len(StrPreviousCode))
            StrPreviousCode = Mid(StrPreviousCode, 1, InStrRev(StrPreviousCode, "||") - 1)
            StrForName = Mid(StrPreviousName, InStrRev(StrPreviousName, "||") + 2, Len(StrPreviousName))
            StrPreviousName = Mid(StrPreviousName, 1, InStrRev(StrPreviousName, "||") - 1)
            FDisplay_Level_Group(StrForCode, StrForName)
        End If

        '======================================================
        '=== To Go On Same Record From Where User Had Come ====
        '======================================================
        LblFilter.Text = Mid(LblFilter.Tag, 2, Len(LblFilter.Tag))
        FFilter("", Val(Microsoft.VisualBasic.Left(LblFilter.Tag, 1)))
    End Sub
    Public Sub FForward(Optional ByVal IntRow As Integer = 0, Optional ByVal IntCol As Integer = 0, Optional ByVal DspType As ClsStructure.DisplayType = ClsStructure.DisplayType.BalanceSheet)
        Dim StrForCode As String = "", StrForName As String = ""


        If IntLevel <> 0 And IntRow < 0 Then Exit Sub
        If IntLevel = 0 Then
            StrPreviousCode = ""
            StrPreviousName = ""
            IntLevel = IntLevel + 1

            If DspType = ClsStructure.DisplayType.TrailBalance Then
                FTrailBalance_Disp()
            ElseIf DspType = ClsStructure.DisplayType.DTrailBalance Then
                FDTrailBalance_Disp()
            ElseIf DspType = ClsStructure.DisplayType.ProfitAndLoss Then
                FProfitAndLoss_Disp()
            ElseIf DspType = ClsStructure.DisplayType.BalanceSheet Then
                FBalanceSheet_Disp()
            End If
        ElseIf UCase(FGMain(GGR_SG, IntRow).Value) = "S" Then
            LblFilter.Tag = GGRName & FGMain(GGRName, IntRow).Value

            StrPreviousCode = StrPreviousCode + "||" + LblDisplay.Tag
            StrPreviousName = StrPreviousName + "||" + LblDisplay.Text
            StrForCode = FGMain(GGRCode, IntRow).Value
            StrForName = FGMain(GGRName, IntRow).Value
            If StrForCode <> "" Then
                IntLevel = IntLevel + 1
            End If
            FDisplay_SubGroup(StrForCode, StrForName)
        ElseIf IntLevel > 0 And UCase(FGMain(GGR_SG, IntRow).Value) <> "T" And UCase(FGMain(GGR_SG, IntRow).Value) <> UCase("") And UCase(FGMain(GGR_SG, IntRow).Value) <> Nothing Then
            If LblDisplay.Tag = CnsBalanceSheet Or LblDisplay.Tag = CnsProfitAndLoss Then
                Select Case IntCol
                    Case GGRName, GDebit
                        StrForCode = FGMain(GGRCode, IntRow).Value
                        StrForName = FGMain(GGRName, IntRow).Value
                    Case GGRNameCredit, GCredit
                        StrForCode = FGMain(GGRCodeCredit, IntRow).Value
                        StrForName = FGMain(GGRNameCredit, IntRow).Value
                End Select
            Else
                StrForCode = FGMain(GGRCode, IntRow).Value
                StrForName = FGMain(GGRName, IntRow).Value
            End If

            If UCase(FGMain(GGR_SG, IntRow).Value) = CnsProfitAndLoss Then
                If StrForName <> "" Then
                    StrPreviousCode = StrPreviousCode + "||" + LblDisplay.Tag
                    StrPreviousName = StrPreviousName + "||" + LblDisplay.Text
                    FProfitAndLoss_Disp()
                End If
            Else
                If StrForCode <> "" Then
                    StrPreviousCode = StrPreviousCode + "||" + LblDisplay.Tag
                    StrPreviousName = StrPreviousName + "||" + LblDisplay.Text
                    IntLevel = IntLevel + 1
                    FDisplay_Level_Group(StrForCode, StrForName)
                End If
            End If
        ElseIf UCase(FGMain(GGR_SG, IntRow).Value) = "T" Then
            FOpenForm(FGMain.CurrentCell.RowIndex)
        End If
    End Sub
        Private Sub FOpenForm(ByVal IntRowIndex As Integer)
        Dim FrmObjMDI As Object
        Dim FrmObj As Object
        Dim DTRow() As DataRow
        Dim StrModuleName As String = ""
        Dim StrMnuName As String = ""
        Dim StrMnuText As String = ""


        Try
            If DTVType Is Nothing Then DTVType = cmain.FGetDatTable("Select V_Type,MnuName,MnuText,MnuAttachedInModule From Voucher_Type Where IsNull(MnuName,'')<>'' Order By V_Type ", Agl.Gcn)
            DTRow = DTVType.Select("V_Type='" & Trim(FGMain(GVType, IntRowIndex).Value) & "'")
            If DTRow.Length > 0 Then
                StrModuleName = Agl.Xnull(DTRow(0).Item("MnuAttachedInModule"))
                StrMnuName = Agl.Xnull(DTRow(0).Item("MnuName"))
                StrMnuText = Agl.Xnull(DTRow(0).Item("MnuText"))

                FrmObjMDI = Me.MdiParent
                FrmObj = FrmObjMDI.FOpenForm(StrModuleName, StrMnuName, StrMnuText)                
                FrmObj.MdiParent = Me.MdiParent
                FrmObj.Show()
                FrmObj.FindMove(Trim(FGMain(GDocId, IntRowIndex).Value))
                FrmObj = Nothing
            Else
                MsgBox("Define Details For This Voucher Type.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function FFindEmptyRow(ByVal IntCol As Integer, Optional ByVal IntFindFrom As Integer = 0) As Integer
        Dim I As Integer, BlnFlag As Boolean

        BlnFlag = True
        For I = IntFindFrom To FGMain.Rows.Count - 1
            If FGMain(IntCol, I).Value = "" Or FGMain(IntCol, I).Value = Nothing Then
                BlnFlag = False
                Exit For
            End If
        Next

        If BlnFlag Then
            FGMain.Rows.Add(1)
            I = FGMain.Rows.Count - 1
        End If
        FFindEmptyRow = I
    End Function
    Private Sub FFillDTPrint()
        Dim I As Integer, J As Integer
        Dim DRRow As DataRow

        DTPrint.Rows.Clear()
        For I = 0 To FGMain.Rows.Count - 1
            DRRow = DTPrint.NewRow()
            For J = 0 To DTPrint.Columns.Count - 1
                Select Case J
                    Case GOpening, GDebit, GCredit, GClosing
                        DRRow(J) = Val(FGMain(J, I).Value)
                    Case Else
                        DRRow(J) = Trim(FGMain(J, I).Value)
                End Select
            Next
            DTPrint.Rows.Add(DRRow)
        Next
    End Sub
    Public Sub FPrint(ByVal StrReportName As String)
        Dim RptReg As New ReportDocument
        Dim I As Integer

        Me.Cursor = Cursors.WaitCursor
        Try
            AgL.PubReportTitle = LblDisplay.Text

            FFillDTPrint()

            DTPrint.WriteXmlSchema(AgL.PubReportPath & "\" & StrReportName & ".Xml")
            RptReg.Load(AgL.PubReportPath & "\" & StrReportName & ".rpt")
            RptReg.SetDataSource(DTPrint)
            FormulaSet(RptReg, Me)

            For I = 0 To RptReg.DataDefinition.FormulaFields.Count - 1
                Select Case CStr(UCase(RptReg.DataDefinition.FormulaFields.Item(I).Name))
                    Case "FDATE"
                        RptReg.DataDefinition.FormulaFields.Item(I).Text = " " & Agl.ConvertDate(DHSMain.StrFromDate) & " "
                    Case "TDATE"
                        RptReg.DataDefinition.FormulaFields.Item(I).Text = " " & Agl.ConvertDate(DHSMain.StrToDate) & " "
                End Select
            Next
            CMain.FShowReport(RptReg, Me.MdiParent, Me.Text)
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub FGMain_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles FGMain.UserDeletingRow
        e.Cancel = True
    End Sub
End Class