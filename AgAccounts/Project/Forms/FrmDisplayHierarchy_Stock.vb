Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmDisplayHierarchy_Stock
    Private Const GSearchCode As Byte = 0
    Private Const GVDate As Byte = 1
    Private Const GVNo As Byte = 2
    Private Const GVType As Byte = 3
    Private Const GSearchName As Byte = 4
    Private Const GRate As Byte = 5
    Private Const GOQty As Byte = 6
    Private Const GOAmt As Byte = 7
    Private Const GRQty As Byte = 8
    Private Const GRAmt As Byte = 9
    Private Const GIQty As Byte = 10
    Private Const GIAmt As Byte = 11
    Private Const GCQty As Byte = 12
    Private Const GCAmt As Byte = 13

    Dim WithEvents FGMain As New AgControls.AgDataGrid
    Private DTVType As DataTable
    Dim StrSQLQuery As String

    Dim StrCurrentLevel As String
    '====== Short Name Used In For Particular Heads
    Private Const CnsCategory As String = "CTGR"
    Private Const CnsItemGroup As String = "GRUP"
    Private Const CnsItem As String = "ITEM"
    Private Const CnsTransaction As String = "TRNS"

    Dim StrPreviousCode As String = "", StrPreviousName As String = ""
    Dim DTPrint As New DataTable("T")
    Private Sub FSetDTColumn()
        DTPrint.Columns.Add("SearchCode", System.Type.GetType("System.String"))
        DTPrint.Columns.Add("VDate", System.Type.GetType("System.String"))
        DTPrint.Columns.Add("VNo", System.Type.GetType("System.String"))
        DTPrint.Columns.Add("VType", System.Type.GetType("System.String"))
        DTPrint.Columns.Add("SearchName", System.Type.GetType("System.String"))
        DTPrint.Columns.Add("OpnQty", System.Type.GetType("System.Double"))
        DTPrint.Columns.Add("OpnAmt", System.Type.GetType("System.Double"))
        DTPrint.Columns.Add("RecQty", System.Type.GetType("System.Double"))
        DTPrint.Columns.Add("RecAmt", System.Type.GetType("System.Double"))
        DTPrint.Columns.Add("IssQty", System.Type.GetType("System.Double"))
        DTPrint.Columns.Add("IssAmt", System.Type.GetType("System.Double"))
        DTPrint.Columns.Add("ClsQty", System.Type.GetType("System.Double"))
        DTPrint.Columns.Add("ClsAmt", System.Type.GetType("System.Double"))
    End Sub
    Private Sub IniGrid(ByVal DspType As ClsStructure.DisplayType_Stock)
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

        If DspType = ClsStructure.DisplayType_Stock.Category Then
            AgCl.AddAgTextColumn(FGMain, "SearchCode", 0, 5, "SearchCode", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "VDate", 0, 5, "Date", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "VNo", 0, 5, "Vr.No.", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "VType", 0, 5, "Vr.Type", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "SearchName", 320, 0, "Particulars", True, True, False)
            AgCl.AddAgTextColumn(FGMain, "Rate", 0, 0, "Rate", False, True, True)
            AgCl.AddAgTextColumn(FGMain, "OQty", 0, 0, "Opening Quantity", False, True, True)
            AgCl.AddAgTextColumn(FGMain, "OAmt", 150, 0, "Opening Amount", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "RQty", 0, 0, "Receive Quantity", False, True, True)
            AgCl.AddAgTextColumn(FGMain, "RAmt", 150, 0, "Receive Amount", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "GIQty", 0, 20, "Issue Quantity", False, True, True)
            AgCl.AddAgTextColumn(FGMain, "GIAmt", 150, 20, "Issue Amount", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "GCQty", 0, 20, "Closing Quantity", False, True, True)
            AgCl.AddAgTextColumn(FGMain, "GCAmt", 150, 20, "Closing Amount", True, True, True)
            FGMain.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 10, FontStyle.Regular)
            FGMain.DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Bold)
        ElseIf DspType = ClsStructure.DisplayType_Stock.Item Then
            AgCl.AddAgTextColumn(FGMain, "SearchCode", 0, 5, "SearchCode", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "VDate", 0, 5, "Date", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "VNo", 0, 5, "Vr.No.", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "VType", 0, 5, "Vr.Type", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "SearchName", 180, 0, "Particulars", True, True, False)
            AgCl.AddAgTextColumn(FGMain, "Rate", 0, 0, "Rate", False, True, True)
            AgCl.AddAgTextColumn(FGMain, "OQty", 90, 0, "Opening Quantity", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "OAmt", 100, 0, "Opening Amount", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "RQty", 90, 0, "Receive Quantity", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "RAmt", 90, 0, "Receive Amount", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "GIQty", 90, 20, "Issue Quantity", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "GIAmt", 90, 20, "Issue Amountt", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "GCQty", 90, 20, "Closing Quantity", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "GCAmt", 100, 20, "Closing Amountt", True, True, True)
            FGMain.DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
            FGMain.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
        ElseIf DspType = ClsStructure.DisplayType_Stock.Transaction Then
            AgCl.AddAgTextColumn(FGMain, "SearchCode", 0, 5, "SearchCode", False, True, False)
            AgCl.AddAgTextColumn(FGMain, "VDate", 90, 5, "Date", True, True, False)
            AgCl.AddAgTextColumn(FGMain, "VNo", 70, 5, "Vr.No.", True, True, False)
            AgCl.AddAgTextColumn(FGMain, "VType", 70, 5, "Vr.Type", True, True, False)
            AgCl.AddAgTextColumn(FGMain, "SearchName", 150, 0, "Particulars", True, True, False)
            AgCl.AddAgTextColumn(FGMain, "Rate", 90, 0, "Rate", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "OQty", 0, 0, "Opening Quantity", False, True, True)
            AgCl.AddAgTextColumn(FGMain, "OAmt", 0, 0, "Opening Amount", False, True, True)
            AgCl.AddAgTextColumn(FGMain, "RQty", 90, 0, "Receive Quantity", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "RAmt", 90, 0, "Receive Amount", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "GIQty", 90, 20, "Issue Quantity", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "GIAmt", 90, 20, "Issue Amountt", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "GCQty", 90, 20, "Closing Quantity", True, True, True)
            AgCl.AddAgTextColumn(FGMain, "GCAmt", 90, 20, "Closing Amountt", True, True, True)
            FGMain.DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
            FGMain.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
            FGMain.AllowUserToAddRows = False
        End If

        FGMain.BackgroundColor = Color.White
        FGMain.RowsDefaultCellStyle.SelectionForeColor = Color.Black
        FGMain.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        FGMain.TabIndex = PnlMain.TabIndex
        FGMain.GridColor = Color.White
        FGMain.AllowUserToDeleteRows = False
        FGMain.ColumnHeadersHeight = 40
    End Sub
    Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        If Trim(DHSSMain.StrFromDate) = "" Then
            DHSSMain.StrFromDate = AgL.PubStartDate
            DHSSMain.StrToDate = AgL.PubEndDate
            DHSSMain.StrGodownName = "All"
            DHSSMain.StrGodownCode = ""
            DHSSMain.StrSiteName = AgL.PubSiteName
            DHSSMain.StrSiteCode = "'" & AgL.PubSiteCode & "'"
            DHSSMain.StrZeroBalace = "Y"
            DHSSMain.StrReportType = "F"
            DHSSMain.StrItemCategory = "All"
            DHSSMain.StrItemCategoryCode = ""
            DHSSMain.StrItemGroup = "All"
            DHSSMain.StrItemGroupCode = ""
            DHSSMain.StrItemName = "All"
            DHSSMain.StrItemNameCode = ""
        End If
        FSetDTColumn()
    End Sub
    Private Sub FrmDisplayHierarchy_Stock_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 660, 990, 0, 0)
        Agl.GridDesign(FGMain)
    End Sub
    Private Sub FrmDisplayHierarchy_Stock_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Agl.FPaintForm(sender, e, 0)
        LblDisplay.BackColor = Color.White
        LblFilter.BackColor = Color.White
    End Sub
    Private Sub FCategory_Disp()
        Dim StrCondition1 As String = ""
        Dim StrConditionOP As String = ""
        Dim StrZeroBalance As String = ""
        Dim StrConditionExtra As String = ""
        Dim StrValueField As String = ""
        Dim DTTemp As DataTable
        Dim DblRAmount_Total As Double, DblIAmount_Total As Double, DblCAmount_Total As Double, DblOAmount_Total As Double
        Dim I As Integer

        Try
            IniGrid(ClsStructure.DisplayType_Stock.Category)
            LblDisplay.Text = "Stock Summary"
            StrCurrentLevel = CnsCategory
            If DHSSMain.StrReportType = "F" Then
                StrValueField = "ST.FifoAmt"
            Else
                StrValueField = "ST.AVGAmt"
            End If

            LblDisplayDate.Text = "Date : " & DHSSMain.StrFromDate & " To " & DHSSMain.StrToDate
            StrConditionOP = "Where V_Date< " & AgL.Chk_Text(CDate(DHSSMain.StrFromDate).ToString("s")) & ""
            StrCondition1 = " Where (V_Date Between " & AgL.Chk_Text(CDate(DHSSMain.StrFromDate).ToString("s")) & " And " & AgL.Chk_Text(CDate(DHSSMain.StrToDate).ToString("s")) & " ) "
            If UCase(DHSSMain.StrZeroBalace) = "N" Then StrZeroBalance = StrZeroBalance + " HAVING (sum(opvalue)+sum(TAB.RValue)-sum(Tab.ivalue))>0 "

            If Trim(DHSSMain.StrItemCategoryCode) <> "" Then
                If StrConditionExtra <> "" Then StrConditionExtra += " And "
                StrConditionExtra += " IG.CatCode In (" & DHSSMain.StrItemCategoryCode & ") "
            End If

            If Trim(DHSSMain.StrItemGroupCode) <> "" Then
                If StrConditionExtra <> "" Then StrConditionExtra += " And "
                StrConditionExtra += " IG.Code In (" & DHSSMain.StrItemGroupCode & ") "
            End If

            If Trim(DHSSMain.StrItemNameCode) <> "" Then
                If StrConditionExtra <> "" Then StrConditionExtra += " And "
                StrConditionExtra += " IM.Code In (" & DHSSMain.StrItemNameCode & ") "
            End If

            If StrConditionExtra <> "" Then StrConditionExtra = "Where " + StrConditionExtra

            If Trim(DHSSMain.StrGodownCode) <> "" Then
                StrConditionOP += " And ST.Godown In (" & DHSSMain.StrGodownCode & ") "
                StrCondition1 += " And ST.Godown In (" & DHSSMain.StrGodownCode & ") "
            End If

            If Trim(DHSSMain.StrSiteCode) <> "" Then
                StrConditionOP += " And ST.Site_Code In (" & DHSSMain.StrSiteCode & ") "
                StrCondition1 += " And ST.Site_Code In (" & DHSSMain.StrSiteCode & ") "
            End If

            'StrSQLQuery = "SELECT max(IM.ItemCategory) AS ItemCatCode,Max(ICat.Description) AS ItemcatName, "
            'StrSQLQuery = StrSQLQuery + "Sum(Opvalue) AS OpenAmount,Sum(Rvalue) AS Rval,Sum(Ivalue) AS Ival "
            'StrSQLQuery = StrSQLQuery + "FROM "
            'StrSQLQuery = StrSQLQuery + "( "
            'StrSQLQuery = StrSQLQuery + "SELECT (IfNull(Sum(ST.Qty_Rec),0) - IfNull(Sum(ST.Qty_iss),0)) As OPQty, (IfNull(Sum((Case	When IfNull(ST.Qty_Rec,0) <> 0  "
            'StrSQLQuery = StrSQLQuery + "Then " & StrValueField & "  Else 0 End)),0) - IfNull(Sum((Case	When IfNull(ST.Qty_Iss,0) <> 0 Then " & StrValueField & " Else 0 End)),0)) "
            'StrSQLQuery = StrSQLQuery + " As OPValue, 0 As RQty,0 As RValue,0 As IQty,0 As IValue,ST.Item From Stock ST "
            'StrSQLQuery = StrSQLQuery + " LEFT JOIN Item I On St.Item = I.Code "
            'StrSQLQuery = StrSQLQuery + StrConditionOP
            'StrSQLQuery = StrSQLQuery + "Group By Item"
            'StrSQLQuery = StrSQLQuery + " Union All  "
            'StrSQLQuery = StrSQLQuery + "Select 0 As OpQty,0 As OPValue, IfNull(Sum(ST.Qty_Rec),0) As RQty,  "
            'StrSQLQuery = StrSQLQuery + "IfNull(Sum((Case When IfNull(ST.Qty_Rec,0) <> 0  Then " & StrValueField & " Else 0 End)),0) "
            'StrSQLQuery = StrSQLQuery + " As RValue, IfNull(Sum(ST.Qty_iss),0) As IQty, "
            'StrSQLQuery = StrSQLQuery + "IfNull(Sum((Case When IfNull(ST.Qty_Iss,0) <> 0 Then " & StrValueField & " Else 0 End)),0) "
            'StrSQLQuery = StrSQLQuery + "As IValue,ST.Item "
            'StrSQLQuery = StrSQLQuery + "From Stock ST "
            'StrSQLQuery = StrSQLQuery + " LEFT JOIN Item I On St.Item = I.Code "
            'StrSQLQuery = StrSQLQuery + StrCondition1
            'StrSQLQuery = StrSQLQuery + "Group By Item "
            'StrSQLQuery = StrSQLQuery + ")"
            'StrSQLQuery = StrSQLQuery + " Tab  "
            'StrSQLQuery = StrSQLQuery + "LEFT JOIN Item IM ON IM.Code = Tab.Item "
            'StrSQLQuery = StrSQLQuery + "LEFT JOIN ItemCategory ICat ON IM.ItemCategory = ICat.Code "
            'StrSQLQuery = StrSQLQuery + StrConditionExtra
            'StrSQLQuery = StrSQLQuery + "GROUP BY IM.ItemCategory "
            'StrSQLQuery = StrSQLQuery + StrZeroBalance + "ORDER BY IM.ItemCategory "


            StrSQLQuery = "SELECT max(IM.ItemCategory) AS ItemCatCode,Max(ICat.Description) AS ItemcatName, "
            StrSQLQuery = StrSQLQuery + "Sum(Opvalue) AS OpenAmount,Sum(Rvalue) AS Rval,Sum(Ivalue) AS Ival "
            StrSQLQuery = StrSQLQuery + "FROM "
            StrSQLQuery = StrSQLQuery + "( "
            StrSQLQuery = StrSQLQuery + "SELECT (IfNull(Sum(ST.Qty_Rec),0) - IfNull(Sum(ST.Qty_iss),0)) As OPQty, (IfNull(Sum((Case	When IfNull(ST.Qty_Rec,0) <> 0  "
            StrSQLQuery = StrSQLQuery + "Then IfNull(ST.Qty_Rec,0) * IfNull(I.PurchaseRate,0) Else 0 End)),0) - IfNull(Sum((Case	When IfNull(ST.Qty_Iss,0) <> 0 Then IfNull(ST.Qty_Iss,0) * IfNull(I.PurchaseRate,0) Else 0 End)),0)) "
            StrSQLQuery = StrSQLQuery + " As OPValue, 0 As RQty,0 As RValue,0 As IQty,0 As IValue,ST.Item From Stock ST "
            StrSQLQuery = StrSQLQuery + " LEFT JOIN Item I On St.Item = I.Code "
            StrSQLQuery = StrSQLQuery + StrConditionOP
            StrSQLQuery = StrSQLQuery + "Group By Item"
            StrSQLQuery = StrSQLQuery + " Union All  "
            StrSQLQuery = StrSQLQuery + "Select 0 As OpQty,0 As OPValue, IfNull(Sum(ST.Qty_Rec),0) As RQty,  "
            StrSQLQuery = StrSQLQuery + "IfNull(Sum((Case When IfNull(ST.Qty_Rec,0) <> 0  Then IfNull(ST.Qty_Rec,0) * IfNull(I.PurchaseRate,0) Else 0 End)),0) "
            StrSQLQuery = StrSQLQuery + " As RValue, IfNull(Sum(ST.Qty_iss),0) As IQty, "
            StrSQLQuery = StrSQLQuery + "IfNull(Sum((Case When IfNull(ST.Qty_Iss,0) <> 0 Then IfNull(ST.Qty_Iss,0) * IfNull(I.PurchaseRate,0) Else 0 End)),0) "
            StrSQLQuery = StrSQLQuery + "As IValue,ST.Item "
            StrSQLQuery = StrSQLQuery + "From Stock ST "
            StrSQLQuery = StrSQLQuery + " LEFT JOIN Item I On St.Item = I.Code "
            StrSQLQuery = StrSQLQuery + StrCondition1
            StrSQLQuery = StrSQLQuery + "Group By Item "
            StrSQLQuery = StrSQLQuery + ")"
            StrSQLQuery = StrSQLQuery + " Tab  "
            StrSQLQuery = StrSQLQuery + "LEFT JOIN Item IM ON IM.Code = Tab.Item "
            StrSQLQuery = StrSQLQuery + "LEFT JOIN ItemCategory ICat ON IM.ItemCategory = ICat.Code "
            StrSQLQuery = StrSQLQuery + StrConditionExtra
            StrSQLQuery = StrSQLQuery + "GROUP BY IM.ItemCategory "
            StrSQLQuery = StrSQLQuery + StrZeroBalance + "ORDER BY IM.ItemCategory "

            DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

            If DTTemp.Rows.Count > 0 Then
                FGMain.Rows.Add(DTTemp.Rows.Count + 2)
            End If
            DblRAmount_Total = 0
            DblIAmount_Total = 0
            DblCAmount_Total = 0

            For I = 0 To DTTemp.Rows.Count - 1
                FGMain.Rows(I).DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Bold)
                FGMain(GSearchName, I).Value = AgL.XNull(DTTemp.Rows(I).Item("ItemCatName"))
                FGMain(GSearchCode, I).Value = AgL.XNull(DTTemp.Rows(I).Item("ItemCatCode"))
                FGMain(GOAmt, I).Value = Format(AgL.VNull(DTTemp.Rows(I).Item("OpenAmount")), "0.00")
                FGMain(GRAmt, I).Value = Format(AgL.VNull(DTTemp.Rows(I).Item("Rval")), "0.00")
                FGMain(GIAmt, I).Value = Format(AgL.VNull(DTTemp.Rows(I).Item("Ival")), "0.00")
                FGMain(GCAmt, I).Value = Format(AgL.VNull((DTTemp.Rows(I).Item("OpenAmount")) + AgL.VNull(DTTemp.Rows(I).Item("Rval"))) - AgL.VNull(DTTemp.Rows(I).Item("Ival")), "0.00")
                DblOAmount_Total = DblOAmount_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("OpenAmount")), "0.00")
                DblRAmount_Total = DblRAmount_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("Rval")), "0.00")
                DblIAmount_Total = DblIAmount_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("IVal")), "0.00")
                DblCAmount_Total = DblCAmount_Total + Format((AgL.VNull(DTTemp.Rows(I).Item("OpenAmount")) + AgL.VNull(DTTemp.Rows(I).Item("Rval"))) - AgL.VNull(DTTemp.Rows(I).Item("Ival")), "0.00")
            Next
            DTTemp.Clear()
            DTTemp.Dispose()
            FGMain.Rows(FGMain.Rows.Count - 1).DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230, 250)
            FGMain.Rows(FGMain.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.Black
            FGMain.Rows(FGMain.Rows.Count - 1).DefaultCellStyle.Font = New Font("Arial", 11, FontStyle.Regular)
            FGMain(GRAmt, FGMain.Rows.Count - 1).Value = Format(DblRAmount_Total, "0.00")
            FGMain(GSearchName, FGMain.Rows.Count - 1).Value = "Total"
            FGMain(GIAmt, FGMain.Rows.Count - 1).Value = Format(DblIAmount_Total, "0.00")
            FGMain(GCAmt, FGMain.Rows.Count - 1).Value = Format(DblCAmount_Total, "0.00")
            FGMain(GOAmt, FGMain.Rows.Count - 1).Value = Format(DblOAmount_Total, "0.00")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FGMain_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles FGMain.CellDoubleClick
        Call FForward(e.RowIndex, e.ColumnIndex)
    End Sub
    Private Sub FGMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FGMain.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call FForward(FGMain.CurrentCell.RowIndex, FGMain.CurrentCell.ColumnIndex)
        ElseIf e.KeyCode = Keys.Escape Then
            Call FBackward()
        ElseIf e.KeyCode = Keys.H And e.Control = True Then
            If FGMain.Rows.Count > 2 Then
                If FGMain(GSearchName, FGMain.CurrentCell.RowIndex).Value <> "Total" Then
                    FGMain.Rows.RemoveAt(FGMain.CurrentCell.RowIndex)
                    FCalculate()
                End If
            End If
            'End If
        End If
        FSetFilter(e.KeyCode)
    End Sub
    Private Sub FCalculate()
        Dim I As Integer
        Dim DblRQty_Total As Double, DblRAmount_Total As Double
        Dim DblIQty_Total As Double, DblIAmount_Total As Double, DblCQty_Total As Double, DblCAmt_Total As Double
        Dim DblOpQty_Total As Double, DblOpAmount_Total As Double

        DblRQty_Total = 0
        DblRAmount_Total = 0
        DblIQty_Total = 0
        DblIAmount_Total = 0
        DblCQty_Total = 0
        DblCAmt_Total = 0
        DblOpQty_Total = 0
        DblOpAmount_Total = 0

        For I = 0 To FGMain.Rows.Count - 1
            If FGMain(GSearchName, I).Value <> "Total" Then
                DblOpQty_Total += Val(FGMain(GOQty, I).Value)
                DblOpAmount_Total += Val(FGMain(GOAmt, I).Value)
                DblRQty_Total += Val(FGMain(GRQty, I).Value)
                DblRAmount_Total += Val(FGMain(GRAmt, I).Value)
                DblIQty_Total += Val(FGMain(GIQty, I).Value)
                DblIAmount_Total += Val(FGMain(GIAmt, I).Value)
                If StrCurrentLevel = CnsTransaction Then
                    If I <> 0 And FGMain(GSearchCode, I).Value <> "" Then
                        FGMain(GCQty, I).Value = Format((Val(FGMain(GCQty, I - 1).Value) + Val(FGMain(GRQty, I).Value)) - Val(FGMain(GIQty, I).Value), "0.000")
                        FGMain(GCAmt, I).Value = Format((Val(FGMain(GCAmt, I - 1).Value) + Val(FGMain(GRAmt, I).Value)) - Val(FGMain(GIAmt, I).Value), "0.00")
                    ElseIf I = 0 And FGMain(GSearchCode, I).Value <> "OPBAL" Then
                        FGMain(GCQty, I).Value = Format(Val(FGMain(GRQty, I).Value) - Val(FGMain(GIQty, I).Value), "0.000")
                        FGMain(GCAmt, I).Value = Format(Val(FGMain(GRAmt, I).Value) - Val(FGMain(GIAmt, I).Value), "0.00")
                    End If

                    If FGMain(GSearchCode, I).Value <> "" Then
                        DblCQty_Total = Val(FGMain(GCQty, I).Value)
                        DblCAmt_Total = Val(FGMain(GCAmt, I).Value)
                    End If
                Else
                    DblCQty_Total += Val(FGMain(GCQty, I).Value)
                    DblCAmt_Total += Val(FGMain(GCAmt, I).Value)
                End If
            End If
        Next

        FGMain(GOQty, FGMain.Rows.Count - 1).Value = Format(DblOpQty_Total, "0.000")
        FGMain(GOAmt, FGMain.Rows.Count - 1).Value = Format(DblOpAmount_Total, "0.00")
        FGMain(GRQty, FGMain.Rows.Count - 1).Value = Format(DblRQty_Total, "0.000")
        FGMain(GRAmt, FGMain.Rows.Count - 1).Value = Format(DblRAmount_Total, "0.00")
        FGMain(GIQty, FGMain.Rows.Count - 1).Value = Format(DblIQty_Total, "0.000")
        FGMain(GIAmt, FGMain.Rows.Count - 1).Value = Format(DblIAmount_Total, "0.00")
        FGMain(GCQty, FGMain.Rows.Count - 1).Value = Format(DblCQty_Total, "0.000")
        FGMain(GCAmt, FGMain.Rows.Count - 1).Value = Format(DblCAmt_Total, "0.00")

    End Sub
    Private Sub FItemGroup_Disp(ByVal StrForCode As String, ByVal StrForName As String)
        Dim StrCondition1 As String = ""
        Dim StrConditionOP As String = ""
        Dim StrZeroBalance As String = ""
        Dim StrValueField As String = ""
        Dim StrConditionExtra As String = ""
        Dim DTTemp As DataTable
        Dim DblRAmount_Total As Double, DblIAmount_Total As Double, DblCAmount_Total As Double, DblOAmount_Total As Double
        Dim I As Integer

        Try
            IniGrid(ClsStructure.DisplayType_Stock.Category)
            LblDisplay.Text = StrForName
            LblDisplay.Tag = StrForCode
            StrCurrentLevel = CnsItemGroup
            If DHSSMain.StrReportType = "F" Then
                StrValueField = "ST.FifoAmt"
            Else
                StrValueField = "ST.AVGAmt"
            End If
            LblDisplayDate.Text = "Date : " & DHSSMain.StrFromDate & " To " & DHSSMain.StrToDate
            StrConditionOP = "Where V_Date< " & AgL.Chk_Text(CDate(DHSSMain.StrFromDate).ToString("s")) & ""
            LblDisplaySite.Text = DHSSMain.StrSiteName
            StrCondition1 = " Where (V_Date Between " & AgL.Chk_Text(CDate(DHSSMain.StrFromDate).ToString("s")) & " And " & AgL.Chk_Text(CDate(DHSSMain.StrToDate).ToString("s")) & " ) "
            If UCase(DHSSMain.StrZeroBalace) = "N" Then StrZeroBalance = StrZeroBalance + " HAVING (sum(opvalue)+sum(TAB.RValue)-sum(Tab.ivalue))>0 "

            If Trim(DHSSMain.StrItemGroupCode) <> "" Then
                StrConditionExtra += " And IG.Code In (" & DHSSMain.StrItemGroupCode & ") "
            End If

            If Trim(DHSSMain.StrItemNameCode) <> "" Then
                StrConditionExtra += " And IM.Code In (" & DHSSMain.StrItemNameCode & ") "
            End If

            If Trim(DHSSMain.StrGodownCode) <> "" Then
                StrConditionOP += " And ST.Godown In (" & DHSSMain.StrGodownCode & ") "
                StrCondition1 += " And ST.Godown In (" & DHSSMain.StrGodownCode & ") "
            End If

            If Trim(DHSSMain.StrSiteCode) <> "" Then
                StrConditionOP += " And ST.Site_Code In (" & DHSSMain.StrSiteCode & ") "
                StrCondition1 += " And ST.Site_Code In (" & DHSSMain.StrSiteCode & ") "
            End If

            '========== For Detail Section =======

            'StrSQLQuery = "SELECT max(IM.ItemGroup) AS GroupCode,Max(Ig.Description) AS GroupName, "
            'StrSQLQuery = StrSQLQuery + "Sum(Opvalue) AS OpenAmount,Sum(Rvalue) AS Rval,Sum(Ivalue) AS Ival "
            'StrSQLQuery = StrSQLQuery + "FROM "
            'StrSQLQuery = StrSQLQuery + "( "
            'StrSQLQuery = StrSQLQuery + "SELECT (IfNull(Sum(ST.Qty_Rec),0) - IfNull(Sum(ST.Qty_Iss),0)) As OPQty, (IfNull(Sum((Case	When IfNull(ST.Qty_Rec,0) <> 0  "
            'StrSQLQuery = StrSQLQuery + "Then " & StrValueField & "  Else 0 End)),0) - IfNull(Sum((Case	When IfNull(ST.Qty_Iss,0) <> 0 Then " & StrValueField & " Else 0 End)),0)) "
            'StrSQLQuery = StrSQLQuery + " As OPValue, 0 As RQty,0 As RValue,0 As IQty,0 As IValue,ST.Item From Stock ST "
            'StrSQLQuery = StrSQLQuery + StrConditionOP
            'StrSQLQuery = StrSQLQuery + "Group By Item "
            'StrSQLQuery = StrSQLQuery + " Union All  "
            'StrSQLQuery = StrSQLQuery + "Select 0 As OpQty,0 As OPValue, IfNull(Sum(ST.Qty_Rec),0) As RQty,  "
            'StrSQLQuery = StrSQLQuery + "IfNull(Sum((Case When  IfNull(ST.Qty_Rec,0) <> 0 Then " & StrValueField & " Else 0 End)),0) "
            'StrSQLQuery = StrSQLQuery + " As RValue, IfNull(Sum(ST.Qty_Iss),0) As IQty, "
            'StrSQLQuery = StrSQLQuery + "IfNull(Sum((Case When  IfNull(ST.Qty_Iss,0) <> 0 Then " & StrValueField & " Else 0 End)),0) "
            'StrSQLQuery = StrSQLQuery + "As IValue,ST.Item "
            'StrSQLQuery = StrSQLQuery + "From Stock ST "
            'StrSQLQuery = StrSQLQuery + StrCondition1
            'StrSQLQuery = StrSQLQuery + " Group By Item "
            'StrSQLQuery = StrSQLQuery + ")"
            'StrSQLQuery = StrSQLQuery + " Tab  "
            'StrSQLQuery = StrSQLQuery + "LEFT JOIN Item IM ON IM.Code = Tab.Item "
            'StrSQLQuery = StrSQLQuery + "LEFT JOIN ItemGroup Ig ON IM.ItemGroup = Ig.Code "
            'StrSQLQuery = StrSQLQuery + "Where 1=1  "
            'StrSQLQuery = StrSQLQuery + StrConditionExtra
            'StrSQLQuery = StrSQLQuery + " GROUP BY IM.ItemGroup "
            'StrSQLQuery = StrSQLQuery + StrZeroBalance + "ORDER BY IM.ItemGroup "

            StrSQLQuery = "SELECT max(IM.ItemGroup) AS GroupCode,Max(Ig.Description) AS GroupName, "
            StrSQLQuery = StrSQLQuery + "Sum(Opvalue) AS OpenAmount,Sum(Rvalue) AS Rval,Sum(Ivalue) AS Ival "
            StrSQLQuery = StrSQLQuery + "FROM "
            StrSQLQuery = StrSQLQuery + "( "
            StrSQLQuery = StrSQLQuery + "SELECT (IfNull(Sum(ST.Qty_Rec),0) - IfNull(Sum(ST.Qty_Iss),0)) As OPQty, (IfNull(Sum((Case	When IfNull(ST.Qty_Rec,0) <> 0  "
            StrSQLQuery = StrSQLQuery + "Then IfNull(ST.Qty_Rec,0) * IfNull(I.PurchaseRate,0) Else 0 End)),0) - IfNull(Sum((Case	When IfNull(ST.Qty_Iss,0) <> 0 Then IfNull(ST.Qty_Iss,0) * IfNull(I.PurchaseRate,0) Else 0 End)),0)) "
            StrSQLQuery = StrSQLQuery + " As OPValue, 0 As RQty,0 As RValue,0 As IQty,0 As IValue,ST.Item From Stock ST "
            StrSQLQuery = StrSQLQuery + " LEFT JOIN Item I On St.Item = I.Code "
            StrSQLQuery = StrSQLQuery + StrConditionOP
            StrSQLQuery = StrSQLQuery + "Group By Item "
            StrSQLQuery = StrSQLQuery + " Union All  "
            StrSQLQuery = StrSQLQuery + "Select 0 As OpQty,0 As OPValue, IfNull(Sum(ST.Qty_Rec),0) As RQty,  "
            StrSQLQuery = StrSQLQuery + "IfNull(Sum((Case When  IfNull(ST.Qty_Rec,0) <> 0 Then IfNull(ST.Qty_Rec,0) * IfNull(I.PurchaseRate,0) Else 0 End)),0) "
            StrSQLQuery = StrSQLQuery + " As RValue, IfNull(Sum(ST.Qty_Iss),0) As IQty, "
            StrSQLQuery = StrSQLQuery + "IfNull(Sum((Case When  IfNull(ST.Qty_Iss,0) <> 0 Then IfNull(ST.Qty_Iss,0) * IfNull(I.PurchaseRate,0) Else 0 End)),0) "
            StrSQLQuery = StrSQLQuery + "As IValue,ST.Item "
            StrSQLQuery = StrSQLQuery + "From Stock ST "
            StrSQLQuery = StrSQLQuery + " LEFT JOIN Item I On St.Item = I.Code "
            StrSQLQuery = StrSQLQuery + StrCondition1
            StrSQLQuery = StrSQLQuery + " Group By Item "
            StrSQLQuery = StrSQLQuery + ")"
            StrSQLQuery = StrSQLQuery + " Tab  "
            StrSQLQuery = StrSQLQuery + "LEFT JOIN Item IM ON IM.Code = Tab.Item "
            StrSQLQuery = StrSQLQuery + "LEFT JOIN ItemGroup Ig ON IM.ItemGroup = Ig.Code "
            'StrSQLQuery = StrSQLQuery + "Where 1=1  "
            StrSQLQuery = StrSQLQuery + "Where IM.ItemCategory ='" & StrForCode & "' "
            StrSQLQuery = StrSQLQuery + StrConditionExtra
            StrSQLQuery = StrSQLQuery + " GROUP BY IM.ItemGroup "
            StrSQLQuery = StrSQLQuery + StrZeroBalance + "ORDER BY IM.ItemGroup "
            DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

            FGMain.Rows.Clear()
            If DTTemp.Rows.Count > 0 Then
                FGMain.Rows.Add(DTTemp.Rows.Count + 1)
            Else
                FGMain.Rows.Add(1)
            End If

            DblIAmount_Total = 0
            DblIAmount_Total = 0
            DblOAmount_Total = 0

            For I = 0 To DTTemp.Rows.Count - 1
                FGMain(GSearchCode, I).Value = AgL.XNull(DTTemp.Rows(I).Item("GroupCode"))
                FGMain(GSearchName, I).Value = AgL.XNull(DTTemp.Rows(I).Item("GroupName"))
                FGMain(GIAmt, I).Value = Format(AgL.VNull(DTTemp.Rows(I).Item("IVal")), "0.00")
                FGMain(GRAmt, I).Value = Format(AgL.VNull(DTTemp.Rows(I).Item("RVal")), "0.00")
                FGMain(GOAmt, I).Value = Format(AgL.VNull(DTTemp.Rows(I).Item("OpenAmount")), "0.00")
                FGMain(GCAmt, I).Value = Format(AgL.VNull((DTTemp.Rows(I).Item("OpenAmount")) + AgL.VNull(DTTemp.Rows(I).Item("Rval"))) - AgL.VNull(DTTemp.Rows(I).Item("Ival")), "0.00")
                DblRAmount_Total = DblRAmount_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("Rval")), "0.00")
                DblIAmount_Total = DblIAmount_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("IVal")), "0.00")
                DblCAmount_Total = DblCAmount_Total + Format(AgL.VNull((DTTemp.Rows(I).Item("OpenAmount")) + AgL.VNull(DTTemp.Rows(I).Item("Rval"))) - AgL.VNull(DTTemp.Rows(I).Item("Ival")), "0.00")
                DblOAmount_Total = DblOAmount_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("OpenAmount")), "0.00")
            Next

            DTTemp.Clear()
            DTTemp.Dispose()

            FGMain.Rows(FGMain.Rows.Count - 1).DefaultCellStyle.BackColor = FGMain.ColumnHeadersDefaultCellStyle.BackColor
            FGMain.Rows(FGMain.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.Black
            FGMain.Rows(FGMain.Rows.Count - 1).DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
            FGMain(GIAmt, FGMain.Rows.Count - 1).Value = Format(DblIAmount_Total, "0.00")
            FGMain(GRAmt, FGMain.Rows.Count - 1).Value = Format(DblRAmount_Total, "0.00")
            FGMain(GCAmt, FGMain.Rows.Count - 1).Value = Format(DblCAmount_Total, "0.00")
            FGMain(GOAmt, FGMain.Rows.Count - 1).Value = Format(DblOAmount_Total, "0.00")
            FGMain(GSearchName, FGMain.Rows.Count - 1).Value = "Total"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FItem_Disp(ByVal StrForCode As String, ByVal StrForName As String)
        Dim StrCondition1 As String = ""
        Dim StrConditionOP As String = ""
        Dim StrConditionExtra As String = ""
        Dim DTTemp As DataTable
        Dim DblRQty_Total As Double, DblRAmount_Total As Double
        Dim DblIQty_Total As Double, DblIAmount_Total As Double, DblCQty_Total As Double, DblCAmt_Total As Double
        Dim DblOpQty_Total As Double, DblOpAmount_Total As Double
        Dim I As Integer
        Dim StrGrp As String = ""
        Dim StrZeroBalance As String = ""
        Dim StrValueField As String = ""
        Try
            IniGrid(ClsStructure.DisplayType_Stock.Item)
            LblDisplay.Text = StrForName
            LblDisplay.Tag = StrForCode
            StrCurrentLevel = CnsItem
            If DHSSMain.StrReportType = "F" Then
                StrValueField = "ST.FifoAmt"
            Else
                StrValueField = "ST.AVGAmt"
            End If
            LblDisplayDate.Text = "Date : " & DHSSMain.StrFromDate & " To " & DHSSMain.StrToDate
            LblDisplaySite.Text = DHSSMain.StrSiteName
            StrConditionOP = "Where V_Date< " & AgL.Chk_Text(CDate(DHSSMain.StrFromDate).ToString("s")) & ""
            StrCondition1 = " Where (V_Date Between " & AgL.Chk_Text(CDate(DHSSMain.StrFromDate).ToString("s")) & " And " & AgL.Chk_Text(CDate(DHSSMain.StrToDate).ToString("s")) & " )"
            If UCase(DHSSMain.StrZeroBalace) = "N" Then StrZeroBalance = StrZeroBalance + " HAVING (Sum(opvalue)+Sum(TAB.RValue)-Sum(Tab.ivalue))>0 "

            If Trim(DHSSMain.StrItemNameCode) <> "" Then
                StrConditionExtra += " And IM.Code In (" & DHSSMain.StrItemNameCode & ") "
            End If

            If Trim(DHSSMain.StrGodownCode) <> "" Then
                StrConditionOP += " And ST.Godown In (" & DHSSMain.StrGodownCode & ") "
                StrCondition1 += " And ST.Godown In (" & DHSSMain.StrGodownCode & ") "
            End If

            If Trim(DHSSMain.StrSiteCode) <> "" Then
                StrConditionOP += " And ST.Site_Code In (" & DHSSMain.StrSiteCode & ") "
                StrCondition1 += " And ST.Site_Code In (" & DHSSMain.StrSiteCode & ") "
            End If

            'StrSQLQuery = "SELECT max(IM.Code) AS ItemCode,Max(IM.Description) AS ItemName, "
            'StrSQLQuery = StrSQLQuery + "Sum(Opvalue) AS OpenAmount,Sum(Rvalue) AS Rval,Sum(Ivalue) AS Ival,sum(IQty) AS IQty,Sum(RQty) AS RQty,Max(IM.Unit) As Unit,Sum(OpQty) As OpQty "
            'StrSQLQuery = StrSQLQuery + "FROM "
            'StrSQLQuery = StrSQLQuery + "( "
            'StrSQLQuery = StrSQLQuery + "SELECT (IfNull(Sum(ST.Qty_Rec),0) - IfNull(Sum(ST.Qty_Iss),0)) As OPQty, (IfNull(Sum((Case	When IfNull(ST.Qty_Rec,0) <> 0 "
            'StrSQLQuery = StrSQLQuery + "Then " & StrValueField & "  Else 0 End)),0) - IfNull(Sum((Case	When IfNull(ST.Qty_Iss,0) <> 0 Then " & StrValueField & " Else 0 End)),0)) "
            'StrSQLQuery = StrSQLQuery + " As OPValue, 0 As RQty,0 As RValue,0 As IQty,0 As IValue,ST.Item From Stock ST "
            'StrSQLQuery = StrSQLQuery + StrConditionOP
            'StrSQLQuery = StrSQLQuery + "Group By Item "
            'StrSQLQuery = StrSQLQuery + " Union All  "
            'StrSQLQuery = StrSQLQuery + "Select 0 As OpQty,0 As OPValue, IfNull(ST.Qty_Rec,0) As RQty,  "
            'StrSQLQuery = StrSQLQuery + "Case When IfNull(ST.Qty_Rec,0) <> 0 Then " & StrValueField & " Else 0 End "
            'StrSQLQuery = StrSQLQuery + " As RValue, IfNull(ST.Qty_Iss,0) As IQty, "
            'StrSQLQuery = StrSQLQuery + "Case When IfNull(ST.Qty_Iss,0) <> 0 Then " & StrValueField & " Else 0 End "
            'StrSQLQuery = StrSQLQuery + "As IValue,ST.Item "
            'StrSQLQuery = StrSQLQuery + "From Stock ST "
            'StrSQLQuery = StrSQLQuery + StrCondition1
            'StrSQLQuery = StrSQLQuery + ")"
            'StrSQLQuery = StrSQLQuery + " Tab  "
            'StrSQLQuery = StrSQLQuery + "LEFT JOIN Item IM ON IM.Code =Tab.Item "
            'StrSQLQuery = StrSQLQuery + "Where IM.ItemGroup ='" & StrForCode & "' "
            'StrSQLQuery = StrSQLQuery + StrConditionExtra
            'StrSQLQuery = StrSQLQuery + "GROUP BY Tab.Item "
            'StrSQLQuery = StrSQLQuery + StrZeroBalance + "ORDER BY Max(Im.Description) "

            StrSQLQuery = "SELECT max(IM.Code) AS ItemCode,Max(IM.Description) AS ItemName, "
            StrSQLQuery = StrSQLQuery + "Sum(Opvalue) AS OpenAmount,Sum(Rvalue) AS Rval,Sum(Ivalue) AS Ival,sum(IQty) AS IQty,Sum(RQty) AS RQty,Max(IM.Unit) As Unit,Sum(OpQty) As OpQty "
            StrSQLQuery = StrSQLQuery + "FROM "
            StrSQLQuery = StrSQLQuery + "( "
            StrSQLQuery = StrSQLQuery + "SELECT (IfNull(Sum(ST.Qty_Rec),0) - IfNull(Sum(ST.Qty_Iss),0)) As OPQty, (IfNull(Sum((Case	When IfNull(ST.Qty_Rec,0) <> 0 "
            StrSQLQuery = StrSQLQuery + "Then IfNull(ST.Qty_Rec,0) * IfNull(I.PurchaseRate,0) Else 0 End)),0) - IfNull(Sum((Case	When IfNull(ST.Qty_Iss,0) <> 0 Then IfNull(ST.Qty_Iss,0) * IfNull(I.PurchaseRate,0) Else 0 End)),0)) "
            StrSQLQuery = StrSQLQuery + " As OPValue, 0 As RQty,0 As RValue,0 As IQty,0 As IValue,ST.Item From Stock ST "
            StrSQLQuery = StrSQLQuery + " LEFT JOIN Item I On St.Item = I.Code "
            StrSQLQuery = StrSQLQuery + StrConditionOP
            StrSQLQuery = StrSQLQuery + "Group By Item "
            StrSQLQuery = StrSQLQuery + " Union All  "
            StrSQLQuery = StrSQLQuery + "Select 0 As OpQty,0 As OPValue, IfNull(ST.Qty_Rec,0) As RQty,  "
            StrSQLQuery = StrSQLQuery + "Case When IfNull(ST.Qty_Rec,0) <> 0 Then IfNull(ST.Qty_Rec,0) * IfNull(I.PurchaseRate,0) Else 0 End "
            StrSQLQuery = StrSQLQuery + " As RValue, IfNull(ST.Qty_Iss,0) As IQty, "
            StrSQLQuery = StrSQLQuery + "Case When IfNull(ST.Qty_Iss,0) <> 0 Then IfNull(ST.Qty_Iss,0) * IfNull(I.PurchaseRate,0) Else 0 End "
            StrSQLQuery = StrSQLQuery + "As IValue,ST.Item "
            StrSQLQuery = StrSQLQuery + "From Stock ST "
            StrSQLQuery = StrSQLQuery + " LEFT JOIN Item I On St.Item = I.Code "
            StrSQLQuery = StrSQLQuery + StrCondition1
            StrSQLQuery = StrSQLQuery + ")"
            StrSQLQuery = StrSQLQuery + " Tab  "
            StrSQLQuery = StrSQLQuery + "LEFT JOIN Item IM ON IM.Code =Tab.Item "
            StrSQLQuery = StrSQLQuery + "Where IM.ItemGroup ='" & StrForCode & "' "
            StrSQLQuery = StrSQLQuery + StrConditionExtra
            StrSQLQuery = StrSQLQuery + "GROUP BY Tab.Item "
            StrSQLQuery = StrSQLQuery + StrZeroBalance + "ORDER BY Max(Im.Description) "

            DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

            FGMain.Rows.Clear()
            If DTTemp.Rows.Count > 0 Then
                FGMain.Rows.Add(DTTemp.Rows.Count + 1)
            Else
                FGMain.Rows.Add(1)
            End If

            DblRQty_Total = 0
            DblRAmount_Total = 0
            DblIQty_Total = 0
            DblIAmount_Total = 0
            DblCQty_Total = 0
            DblCAmt_Total = 0
            For I = 0 To DTTemp.Rows.Count - 1
                FGMain(GSearchCode, I).Value = AgL.XNull(DTTemp.Rows(I).Item("ItemCode"))
                FGMain(GSearchName, I).Value = AgL.XNull(DTTemp.Rows(I).Item("ItemName")) + "   " + AgL.XNull(DTTemp.Rows(I).Item("Unit"))
                FGMain(GRQty, I).Value = Format(AgL.VNull(DTTemp.Rows(I).Item("RQty")), "0.000")
                FGMain(GRAmt, I).Value = Format(AgL.VNull(DTTemp.Rows(I).Item("Rval")), "0.00")
                FGMain(GIQty, I).Value = Format(AgL.VNull(DTTemp.Rows(I).Item("IQty")), "0.000")
                FGMain(GIAmt, I).Value = Format(AgL.VNull(DTTemp.Rows(I).Item("Ival")), "0.00")
                FGMain(GOQty, I).Value = Format(AgL.VNull(DTTemp.Rows(I).Item("OpQty")), "0.000")
                FGMain(GOAmt, I).Value = Format(AgL.VNull(DTTemp.Rows(I).Item("OpenAmount")), "0.00")
                FGMain(GCQty, I).Value = Format(AgL.VNull((DTTemp.Rows(I).Item("OpQty")) + AgL.VNull(DTTemp.Rows(I).Item("RQty"))) - AgL.VNull(DTTemp.Rows(I).Item("IQty")), "0.000")
                FGMain(GCAmt, I).Value = Format(AgL.VNull((DTTemp.Rows(I).Item("OpenAmount")) + AgL.VNull(DTTemp.Rows(I).Item("Rval"))) - AgL.VNull(DTTemp.Rows(I).Item("Ival")), "0.00")
                DblRQty_Total = DblRQty_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("RQty")), "0.000")
                DblRAmount_Total = DblRAmount_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("Rval")), "0.00")
                DblIQty_Total = DblIQty_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("IQty")), "0.000")
                DblIAmount_Total = DblIAmount_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("Ival")), "0.00")
                DblCQty_Total = DblCQty_Total + Format(AgL.VNull((DTTemp.Rows(I).Item("OpQty")) + AgL.VNull(DTTemp.Rows(I).Item("RQty"))) - AgL.VNull(DTTemp.Rows(I).Item("IQty")), "0.000")
                DblCAmt_Total = DblCAmt_Total + Format(AgL.VNull((DTTemp.Rows(I).Item("OpenAmount")) + AgL.VNull(DTTemp.Rows(I).Item("Rval"))) - AgL.VNull(DTTemp.Rows(I).Item("Ival")), "0.00")
                DblOpQty_Total = DblOpQty_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("OpQty")), "0.000")
                DblOpAmount_Total = DblOpAmount_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("OpenAmount")), "0.00")

            Next
            DTTemp.Clear()
            DTTemp.Dispose()

            FGMain.Rows(FGMain.Rows.Count - 1).DefaultCellStyle.BackColor = FGMain.ColumnHeadersDefaultCellStyle.BackColor
            FGMain.Rows(FGMain.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.Black
            FGMain.Rows(FGMain.Rows.Count - 1).DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
            FGMain(GRQty, FGMain.Rows.Count - 1).Value = Format(DblRQty_Total, "0.000")
            FGMain(GRAmt, FGMain.Rows.Count - 1).Value = Format(DblRAmount_Total, "0.00")
            FGMain(GIQty, FGMain.Rows.Count - 1).Value = Format(DblIQty_Total, "0.000")
            FGMain(GIAmt, FGMain.Rows.Count - 1).Value = Format(DblIAmount_Total, "0.00")
            FGMain(GCQty, FGMain.Rows.Count - 1).Value = Format(DblCQty_Total, "0.000")
            FGMain(GCAmt, FGMain.Rows.Count - 1).Value = Format(DblCAmt_Total, "0.00")
            FGMain(GOQty, FGMain.Rows.Count - 1).Value = Format(DblOpQty_Total, "0.000")
            FGMain(GOAmt, FGMain.Rows.Count - 1).Value = Format(DblOpAmount_Total, "0.00")
            FGMain(GSearchName, FGMain.Rows.Count - 1).Value = "Total"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FBackward()
        Dim StrForCode As String = "", StrForName As String = ""

        Select Case StrCurrentLevel
            Case CnsItemGroup
                StrForCode = Mid(StrPreviousCode, InStrRev(StrPreviousCode, "||") + 2, Len(StrPreviousCode))
                StrPreviousCode = Mid(StrPreviousCode, 1, InStrRev(StrPreviousCode, "||") - 1)
                StrForName = Mid(StrPreviousName, InStrRev(StrPreviousName, "||") + 2, Len(StrPreviousName))
                StrPreviousName = Mid(StrPreviousName, 1, InStrRev(StrPreviousName, "||") - 1)

                FCategory_Disp()
            Case CnsItem
                StrForCode = Mid(StrPreviousCode, InStrRev(StrPreviousCode, "||") + 2, Len(StrPreviousCode))
                StrPreviousCode = Mid(StrPreviousCode, 1, InStrRev(StrPreviousCode, "||") - 1)
                StrForName = Mid(StrPreviousName, InStrRev(StrPreviousName, "||") + 2, Len(StrPreviousName))
                StrPreviousName = Mid(StrPreviousName, 1, InStrRev(StrPreviousName, "||") - 1)

                FItemGroup_Disp(StrForCode, StrForName)
            Case CnsTransaction
                StrForCode = Mid(StrPreviousCode, InStrRev(StrPreviousCode, "||") + 2, Len(StrPreviousCode))
                StrPreviousCode = Mid(StrPreviousCode, 1, InStrRev(StrPreviousCode, "||") - 1)
                StrForName = Mid(StrPreviousName, InStrRev(StrPreviousName, "||") + 2, Len(StrPreviousName))
                StrPreviousName = Mid(StrPreviousName, 1, InStrRev(StrPreviousName, "||") - 1)

                FItem_Disp(StrForCode, StrForName)
                '=============================
            Case Else
                FCategory_Disp()
        End Select

        '======================================================
        '=== To Go On Same Record From Where User Had Came ====
        '======================================================
        LblFilter.Text = (Mid(LblFilter.Tag, InStrRev(LblFilter.Tag, "||") + 2, Len(LblFilter.Tag)))
        Try
            LblFilter.Tag = Mid(LblFilter.Tag, 1, InStrRev(LblFilter.Tag, "||") - 1)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        FFilter("", GSearchName)
    End Sub
    Public Sub FForward(Optional ByVal IntRow As Integer = 0, Optional ByVal IntCol As Integer = 0)
        Dim StrForCode As String = "", StrForName As String = ""

        Select Case StrCurrentLevel
            Case CnsCategory
                LblFilter.Tag = "||" & FGMain(GSearchName, IntRow).Value
                StrForCode = FGMain(GSearchCode, IntRow).Value
                StrForName = FGMain(GSearchName, IntRow).Value
                StrPreviousCode = "||"
                StrPreviousName = "||"

                FItemGroup_Disp(StrForCode, StrForName)
            Case CnsItemGroup
                LblFilter.Tag = LblFilter.Tag & "||" & FGMain(GSearchName, IntRow).Value
                StrForCode = FGMain(GSearchCode, IntRow).Value
                StrForName = FGMain(GSearchName, IntRow).Value
                StrPreviousCode = StrPreviousCode + "||" + LblDisplay.Tag
                StrPreviousName = StrPreviousName + "||" + LblDisplay.Text

                FItem_Disp(StrForCode, StrForName)
            Case CnsItem
                LblFilter.Tag = LblFilter.Tag & "||" & FGMain(GSearchName, IntRow).Value
                StrForCode = FGMain(GSearchCode, IntRow).Value
                StrForName = FGMain(GSearchName, IntRow).Value
                StrPreviousCode = StrPreviousCode + "||" + LblDisplay.Tag
                StrPreviousName = StrPreviousName + "||" + LblDisplay.Text

                FItemWiseTrans_Disp(StrForCode, StrForName)
            Case CnsTransaction
                FOpenForm(IntRow)
            Case Else
                FCategory_Disp()
        End Select

    End Sub

    Private Sub FOpenForm(ByVal IntRowIndex As Integer)
        Dim FrmObjMDI As Object
        Dim FrmObj As Object
        Dim DTRow() As DataRow
        Dim StrModuleName As String = ""
        Dim StrMnuName As String = ""
        Dim StrMnuText As String = ""

        Try
            Dim mQry$ = " Select V_Type,MnuName,MnuText,MnuAttachedInModule From Voucher_Type Where IfNull(MnuName,'')<>'' Order By V_Type  "
            If DTVType Is Nothing Then DTVType = CMain.FGetDatTable("Select V_Type,MnuName,MnuText,MnuAttachedInModule From Voucher_Type Where IfNull(MnuName,'')<>'' Order By V_Type ", AgL.GCn)
            DTRow = DTVType.Select("V_Type='" & Trim(FGMain(GVType, IntRowIndex).Value) & "'")
            If DTRow.Length > 0 Then
                StrModuleName = AgL.XNull(DTRow(0).Item("MnuAttachedInModule"))
                StrMnuName = AgL.XNull(DTRow(0).Item("MnuName"))
                StrMnuText = AgL.XNull(DTRow(0).Item("MnuText"))

                FrmObjMDI = Me.MdiParent
                FrmObj = FrmObjMDI.FOpenForm(StrModuleName, StrMnuName, StrMnuText)
                FrmObj.MdiParent = Me.MdiParent
                FrmObj.Show()
                FrmObj.FindMove(Trim(FGMain(GSearchCode, IntRowIndex).Value))
            Else
                MsgBox("Define Details For This Voucher Type.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FFindEmptyRow(ByVal IntCol As Integer) As Integer
        Dim I As Integer, BlnFlag As Boolean

        BlnFlag = True
        For I = 0 To FGMain.Rows.Count - 1
            If FGMain(IntCol, I).Value = "" Or FGMain(IntCol, I).Value = Nothing Then
                BlnFlag = False
                Exit For
            End If
        Next

        If BlnFlag Then
            FGMain.Rows.Add(1)
            I = FGMain.Rows.Count - 2
        End If
        FFindEmptyRow = I
    End Function

    Private Sub FGMain_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles FGMain.KeyPress
        If FGMain.CurrentCell IsNot Nothing Then
            FFilter(e.KeyChar, FGMain.CurrentCell.ColumnIndex)
        End If
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
    Private Function FForColumn(ByVal IntColIndex As Integer) As Integer
        IntColIndex = GSearchName
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
    Private Sub FGMain_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles FGMain.UserDeletingRow
        e.Cancel = True
    End Sub
    Private Sub FItemWiseTrans_Disp(ByVal StrForCode As String, ByVal StrForName As String)
        Dim StrCondition1 As String = ""
        Dim DTTemp As DataTable
        Dim DblPQty_Total As Double, DblSQty_Total As Double, DblPAmount_Total As Double, DblSAmount_Total As Double, DblCQty_Total As Double, DblCAmount_Total As Double
        Dim I As Integer
        Dim StrConditionOp As String = ""
        LblDisplayDate.Text = "Date : " & DHSSMain.StrFromDate & " To " & DHSSMain.StrToDate
        Dim DblClQty As Double = 0
        Dim DblClvalue As Double = 0
        Dim StrvalueField As String = ""

        Try
            IniGrid(ClsStructure.DisplayType_Stock.Transaction)
            LblDisplay.Text = StrForName
            LblDisplay.Tag = StrForCode
            StrCurrentLevel = CnsTransaction
            LblDisplaySite.Text = DHSSMain.StrSiteName
            StrSQLQuery = ""
            If DHSSMain.StrReportType = "F" Then
                StrvalueField = "ST.FifoAmt"
            Else
                StrvalueField = "ST.AVGAmt"
            End If

            StrCondition1 = " Where (V_Date Between " & AgL.Chk_Text(CDate(DHSSMain.StrFromDate).ToString("s")) & " And " & AgL.Chk_Text(CDate(DHSSMain.StrToDate).ToString("s")) & " ) "
            StrCondition1 = StrCondition1 + " And IM.Code='" & StrForCode & "'"
            StrConditionOp = " Where ST.V_Date < " & AgL.Chk_Text(CDate(DHSSMain.StrFromDate).ToString("s")) & " "

            If Trim(DHSSMain.StrGodownCode) <> "" Then
                StrConditionOp += " And ST.Godown In (" & DHSSMain.StrGodownCode & ") "
                StrCondition1 += " And ST.Godown In (" & DHSSMain.StrGodownCode & ") "
            End If

            If Trim(DHSSMain.StrSiteCode) <> "" Then
                StrConditionOp += " And ST.Site_Code In (" & DHSSMain.StrSiteCode & ") "
                StrCondition1 += " And ST.Site_Code In (" & DHSSMain.StrSiteCode & ") "
            End If

            'StrSQLQuery += "Select	'OPENING' As RecId,Null As  GroupCode,Null As VType,Null As VDate, "
            'StrSQLQuery += "ST.Item,Max(IM.Description) As ItemName,Max(IM.Unit) As Unit, "
            'StrSQLQuery += "(IfNull(Sum(ST.Qty_Rec),0) - IfNull(Sum(ST.Qty_Iss),0)) As OPQty, "
            'StrSQLQuery += "(IfNull(Sum((Case	When IfNull(ST.Qty_Rec,0) <> 0 Then " & StrvalueField & " Else 0 End)),0) -  "
            'StrSQLQuery += "IfNull(Sum((Case	When IfNull(ST.Qty_Iss,0) <> 0 Then " & StrvalueField & " Else 0 End)),0)) As OPValue, "
            'StrSQLQuery += " 0 As RQty,0 As RValue,0 As IQty,0 As IValue, 0 As SNo,"
            'StrSQLQuery += "0 As SerialNo,Null As GroupName "
            'StrSQLQuery += "From Stock ST "
            'StrSQLQuery += "Left Join Item IM On ST.Item=IM.Code "
            'StrSQLQuery += StrConditionOp
            'StrSQLQuery += " And IM.Code='" & StrForCode & "'"
            'StrSQLQuery += "Group By Item "
            'StrSQLQuery += " Union All "
            'StrSQLQuery += "Select ST.RecID,ST.DocId As GroupCode,ST.V_Type As V_Type,ST.V_Date As VDate,ST.Item, "
            'StrSQLQuery += "IfNull(IM.Description,'') As ItemName,IfNull(IM.Unit,'') As Unit, 0 As OpQty,"
            'StrSQLQuery += "0 As OPValue, IfNull(ST.Qty_Rec,0) As RQty, "
            'StrSQLQuery += "(Case When IfNull(ST.Qty_Rec,0) <> 0 Then IfNull(" & StrvalueField & ",0) Else 0 End) As RVal, "
            'StrSQLQuery += " IfNull(ST.Qty_Iss,0) As IQty, "
            'StrSQLQuery += "(Case When IfNull(ST.Qty_Iss,0) <> 0 Then IfNull(" & StrvalueField & ",0) Else 0 End) As IVal, "
            'StrSQLQuery += "1 As SNo,IfNull(VT.SerialNo,0) As SerialNo,SG.Name As Groupname "
            'StrSQLQuery += "From Stock ST "
            'StrSQLQuery += "Left Join Item IM On ST.Item=IM.Code "
            'StrSQLQuery += "Left Join Voucher_Type VT On VT.V_Type=ST.V_Type  "
            'StrSQLQuery += " LEFT JOIN SubGroup SG ON SG.SubCode=ST.SubCode   "
            'StrSQLQuery += StrCondition1
            'StrSQLQuery += " Order By Item,VDate,SNo,SerialNo,RecId "


            StrSQLQuery += "Select	'OPENING' As RecId,Null As  GroupCode,Null As VType,Null As VDate, "
            StrSQLQuery += "ST.Item,Max(IM.Description) As ItemName,Max(IM.Unit) As Unit, 0  as Rate, "
            StrSQLQuery += "(IfNull(Sum(ST.Qty_Rec),0) - IfNull(Sum(ST.Qty_Iss),0)) As OPQty, "
            StrSQLQuery += "(IfNull(Sum((Case	When IfNull(ST.Qty_Rec,0) <> 0 Then IfNull(ST.Qty_Rec,0) * IfNull(IM.PurchaseRate,0) Else 0 End)),0) -  "
            StrSQLQuery += "IfNull(Sum((Case	When IfNull(ST.Qty_Iss,0) <> 0 Then IfNull(ST.Qty_Iss,0) * IfNull(IM.PurchaseRate,0) Else 0 End)),0)) As OPValue, "
            StrSQLQuery += " 0 As RQty,0 As RValue,0 As IQty,0 As IValue, 0 As SNo,"
            StrSQLQuery += "0 As SerialNo,Null As GroupName "
            StrSQLQuery += "From Stock ST "
            StrSQLQuery += "Left Join Item IM On ST.Item=IM.Code "
            StrSQLQuery += StrConditionOp
            StrSQLQuery += " And IM.Code='" & StrForCode & "'"
            StrSQLQuery += "Group By Item "
            StrSQLQuery += " Union All "
            StrSQLQuery += "Select ST.RecID,ST.DocId As GroupCode,ST.V_Type As V_Type,ST.V_Date As VDate,ST.Item, "
            StrSQLQuery += "IfNull(IM.Description,'') As ItemName,IfNull(IM.Unit,'') As Unit, ST.Rate, 0 As OpQty,"
            StrSQLQuery += "0 As OPValue, IfNull(ST.Qty_Rec,0) As RQty, "
            StrSQLQuery += "(Case When IfNull(ST.Qty_Rec,0) <> 0 Then IfNull(ST.Qty_Rec,0) * IfNull(IM.PurchaseRate,0) Else 0 End) As RVal, "
            StrSQLQuery += " IfNull(ST.Qty_Iss,0) As IQty, "
            StrSQLQuery += "(Case When IfNull(ST.Qty_Iss,0) <> 0 Then IfNull(ST.Qty_Iss,0) * IfNull(IM.PurchaseRate,0)  Else 0 End) As IVal, "
            StrSQLQuery += "1 As SNo,IfNull(VT.SerialNo,0) As SerialNo,SG.Name As Groupname "
            StrSQLQuery += "From Stock ST "
            StrSQLQuery += "Left Join Item IM On ST.Item=IM.Code "
            StrSQLQuery += "Left Join Voucher_Type VT On VT.V_Type=ST.V_Type  "
            StrSQLQuery += " LEFT JOIN SubGroup SG ON SG.SubCode=ST.SubCode   "
            StrSQLQuery += StrCondition1
            StrSQLQuery += " Order By Item,VDate,SNo,SerialNo,RecId "


            DTTemp = CMain.FGetDatTable(StrSQLQuery, AgL.GCn)

            FGMain.Rows.Clear()
            DblPQty_Total = 0
            DblPAmount_Total = 0
            DblSQty_Total = 0
            DblSAmount_Total = 0
            DblCQty_Total = 0
            DblCAmount_Total = 0

            If DTTemp.Rows.Count > 0 Then
                FGMain.Rows.Add(DTTemp.Rows.Count + 1)
            Else
                FGMain.Rows.Add(1)
            End If

            For I = 0 To DTTemp.Rows.Count - 1

                If I = 0 And AgL.XNull(DTTemp.Rows(I).Item("RecId")) = "OPENING" Then
                    FGMain(GSearchCode, I).Value = "OPBAL"
                    FGMain(GSearchName, I).Value = "Opening Balance"
                    FGMain(GCQty, I).Value = AgL.VNull(DTTemp.Rows(I).Item("OpQty"))
                    FGMain(GCAmt, I).Value = AgL.VNull(DTTemp.Rows(I).Item("OPValue"))
                    DblClQty = Format(AgL.VNull(DTTemp.Rows(I).Item("OpQty")), "0.000")
                    DblClvalue = Format(AgL.VNull(DTTemp.Rows(I).Item("OPValue")), "0.00")
                ElseIf AgL.XNull(DTTemp.Rows(I).Item("Recid")) <> "" Then
                    FGMain(GSearchCode, I).Value = AgL.XNull(DTTemp.Rows(I).Item("GroupCode"))
                    FGMain(GSearchName, I).Value = AgL.XNull(DTTemp.Rows(I).Item("GroupName"))
                    FGMain(GVDate, I).Value = AgL.XNull(DTTemp.Rows(I).Item("Vdate"))
                    FGMain(GVNo, I).Value = AgL.XNull(DTTemp.Rows(I).Item("RecID"))
                    FGMain(GVType, I).Value = AgL.XNull(DTTemp.Rows(I).Item("VType"))
                    FGMain(GRate, I).Value = Format(AgL.VNull(DTTemp.Rows(I).Item("Rate")), "0.00")
                    FGMain(GRQty, I).Value = Format(AgL.VNull(DTTemp.Rows(I).Item("RQty")), "0.000")
                    FGMain(GRAmt, I).Value = Format(AgL.VNull(DTTemp.Rows(I).Item("RValue")), "0.00")
                    FGMain(GIQty, I).Value = Format(AgL.VNull(DTTemp.Rows(I).Item("IQty")), "0.000")
                    FGMain(GIAmt, I).Value = Format(AgL.VNull(DTTemp.Rows(I).Item("IValue")), "0.00")
                    FGMain(GCQty, I).Value = Format(DblClQty + (Format(AgL.VNull(DTTemp.Rows(I).Item("RQty")), "0.000") - Format(AgL.VNull(DTTemp.Rows(I).Item("IQty")), "0.000")), "0.000")
                    FGMain(GCAmt, I).Value = Format(DblClvalue + (Format(AgL.VNull(DTTemp.Rows(I).Item("RValue")), "0.00") - Format(AgL.VNull(DTTemp.Rows(I).Item("IValue")), "0.00")), "0.00")
                    DblClQty = Format(DblClQty + (Format(AgL.VNull(DTTemp.Rows(I).Item("RQty")), "0.000") - Format(AgL.VNull(DTTemp.Rows(I).Item("IQty")), "0.000")), "0.000")
                    DblClvalue = Format(DblClvalue + (Format(AgL.VNull(DTTemp.Rows(I).Item("RValue")), "0.00") - Format(AgL.VNull(DTTemp.Rows(I).Item("IValue")), "0.00")), "0.00")
                End If
                DblPQty_Total = DblPQty_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("RQty")), "0.00")


                DblPAmount_Total = DblPAmount_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("RValue")), "0.00")
                DblSQty_Total = (DblSQty_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("IQty")), "0.00"))
                DblSAmount_Total = DblSAmount_Total + Format(AgL.VNull(DTTemp.Rows(I).Item("IValue")), "0.00")
            Next

            FGMain.Rows.Add()
            FGMain(GSearchName, FGMain.Rows.Count - 1).Value = "Total"
            DTTemp.Clear()
            DTTemp.Dispose()
            FGMain.Rows(FGMain.Rows.Count - 1).DefaultCellStyle.BackColor = FGMain.ColumnHeadersDefaultCellStyle.BackColor
            FGMain.Rows(FGMain.Rows.Count - 1).DefaultCellStyle.ForeColor = Color.Black
            FGMain.Rows(FGMain.Rows.Count - 1).DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
            FGMain(GRQty, FGMain.Rows.Count - 1).Value = Format(DblPQty_Total, "0.000")
            FGMain(GRAmt, FGMain.Rows.Count - 1).Value = Format(DblPAmount_Total, "0.00")
            FGMain(GIQty, FGMain.Rows.Count - 1).Value = Format(DblSQty_Total, "0.000")
            FGMain(GIAmt, FGMain.Rows.Count - 1).Value = Format(DblSAmount_Total, "0.00")
            FGMain(GCQty, FGMain.Rows.Count - 1).Value = Format(DblClQty, "0.000")
            FGMain(GCAmt, FGMain.Rows.Count - 1).Value = Format(DblClvalue, "0.00")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles BtnClose.Click, BtnSettings.Click, BtnBackWard.Click, BtnPrint.Click, BtnRefresh.Click

        Dim FrmObj As Object

        Select Case sender.name
            Case BtnClose.Name
                Me.Close()
            Case BtnSettings.Name
                FrmObj = New FrmDisplayHierarchy_Settings_Stock(Me)
                FrmObj.ShowDialog()
                FrmObj.Dispose()
                FrmObj = Nothing
                FRefreshLevel()
            Case BtnBackWard.Name
                FBackward()
            Case BtnRefresh.Name
                FrmObj = New FrmAdjustingRates()
                FrmObj.ShowDialog()
                FrmObj.Dispose()
                FrmObj = Nothing
                FRefreshLevel()
            Case BtnPrint.Name
                If StrCurrentLevel = CnsCategory Then
                    FPrint("DispCatGrpRep")
                ElseIf StrCurrentLevel = CnsItemGroup Then
                    FPrint("DispCatGrpRep")
                ElseIf StrCurrentLevel = CnsItem Then
                    FPrint("DispItem")
                ElseIf StrCurrentLevel = CnsTransaction Then
                    FPrint("DispStockTransDetail")
                End If
        End Select
    End Sub
    Private Sub FFillDTPrint()
        Dim I As Integer, J As Integer
        Dim DRRow As DataRow

        DTPrint.Rows.Clear()
        For I = 0 To FGMain.Rows.Count - 1
            DRRow = DTPrint.NewRow()
            For J = 0 To DTPrint.Columns.Count - 1
                Select Case J
                    Case GOQty, GOAmt, GRQty, GRAmt, GIQty, GIAmt, GCQty, GCAmt
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
                        RptReg.DataDefinition.FormulaFields.Item(I).Text = " " & AgL.Chk_Text(CDate(DHSSMain.StrFromDate).ToString("s")) & " "
                    Case "TDATE"
                        RptReg.DataDefinition.FormulaFields.Item(I).Text = " " & AgL.Chk_Text(CDate(DHSSMain.StrToDate).ToString("s")) & " "
                End Select
            Next
            CMain.FShowReport(RptReg, Me.MdiParent, Me.Text)
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub FRefreshLevel()
        Dim IntRowIndex As Integer
        Dim IntColIndex As Integer

        IntRowIndex = FGMain.CurrentCell.RowIndex
        IntColIndex = FGMain.CurrentCell.ColumnIndex

        If StrCurrentLevel = CnsCategory Then
            FCategory_Disp()
        ElseIf StrCurrentLevel = CnsItemGroup Then
            FItemGroup_Disp(LblDisplay.Tag, LblDisplay.Text)
        ElseIf StrCurrentLevel = CnsItem Then
            FItem_Disp(LblDisplay.Tag, LblDisplay.Text)
        ElseIf StrCurrentLevel = CnsTransaction Then
            FItemWiseTrans_Disp(LblDisplay.Tag, LblDisplay.Text)
        Else

        End If

        If IntRowIndex > -1 And IntColIndex > -1 Then
            If IntRowIndex < FGMain.Rows.Count And IntColIndex < FGMain.Columns.Count Then
                FGMain(IntColIndex, IntRowIndex).Selected = True
            End If
        End If
    End Sub
End Class