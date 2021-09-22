Imports System.Data.SQLite
Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmBudgetAnalysis
    Private LIEvent As ClsEvents
    Private WithEvents FGMain As New AgControls.AgDataGrid
    Private Const GSNo As Byte = 0
    Private Const GCostCenter As Byte = 1
    Private Const GCostCenterCode As Byte = 2
    Private Const GBudget As Byte = 3
    Private Const GAmount As Byte = 4
    Private Const GDifference As Byte = 5
    Private Const GDifferencePer As Byte = 6
    Private Sub FrmBudgetAnalysis_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            LIEvent = New ClsEvents(Me)
            Agl.WinSetting(Me, 595, 909, 0, 0)
            Agl.GridDesign(FGMain)
            IniGrid()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub IniGrid()
        FGMain.Height = PnlMain.Height
        FGMain.Width = PnlMain.Width
        FGMain.Top = PnlMain.Top
        FGMain.Left = PnlMain.Left
        Controls.Add(FGMain)
        FGMain.Visible = True
        FGMain.BringToFront()
        AgCl.AddAgTextColumn(FGMain, "SrNo", 50, 5, "S.No.", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Cost Center", 230, 35, "Cost Center", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "CostCenterCode", 0, 0, "Cost Center Code", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "Budget", 120, 13, "Budget", True, True, True)
        AgCl.AddAgTextColumn(FGMain, "Actual", 120, 13, "Actual", True, True, True)
        AgCl.AddAgTextColumn(FGMain, "Variance", 120, 13, "Variance", True, True, True)
        AgCl.AddAgTextColumn(FGMain, "VariancePer", 120, 13, "Variance %", True, True, True)
        FGMain.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        Agl.FSetSNo(FGMain, GSNo)
        FGMain.TabIndex = PnlMain.TabIndex
    End Sub
    Public Sub FFillBudget()
        Dim DTTemp As New DataTable
        Dim I As Integer
        Dim StrSQL As String
        Dim StrDateFrom As String = "", StrDateTo As String = ""

        FGMain.Rows.Clear()
        If AgL.RequiredField(TxtBudget, "Select Budget") Then Exit Sub

        DTTemp = cmain.FGetDatTable("Select DateFrom,DateTo From Budget Where DocId='" & TxtBudget.Tag & "'", Agl.Gcn)
        If DTTemp.Rows.Count > 0 Then
            StrDateFrom = Agl.Xnull(DTTemp.Rows(0).Item("DateFrom"))
            StrDateTo = Agl.Xnull(DTTemp.Rows(0).Item("DateTo"))
        End If

        StrSQL = "Select CCName,Max(Budget) As Budget,Sum(Actual) As Actual "
        StrSQL = StrSQL & "From ( "
        StrSQL = StrSQL & "SELECT CCM.Name AS CCName,BMD.Amount AS Budget,0 AS Actual "
        StrSQL = StrSQL & "FROM BudgetDet BMD Left Join "
        StrSQL = StrSQL & "CostCenterMast CCM ON BMD.CostCenter=CCM.Code "
        StrSQL = StrSQL & "WHERE BMD.DocId='" & TxtBudget.Tag & "' "
        StrSQL = StrSQL & "UNION ALL "
        StrSQL = StrSQL & "SELECT  Max(CCM.Name) AS CCName,0 AS Budget, "
        StrSQL = StrSQL & "(IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(LG.AmtCr),0)) As Actual "
        StrSQL = StrSQL & "FROM Ledger LG LEFT JOIN  "
        StrSQL = StrSQL & "CostCenterMast CCM ON LG.CostCenter=CCM.Code "
        StrSQL = StrSQL & "WHERE (LG.V_Date Between " & AgL.Chk_Text(CDate(StrDateFrom).ToString("s")) & " And " & AgL.Chk_Text(CDate(StrDateTo).ToString("s")) & ") And  "
        StrSQL = StrSQL & "LG.Site_Code='" & agl.PubSiteCode & "' And "
        StrSQL = StrSQL & "LG.CostCenter IN (SELECT BMD.CostCenter FROM BudgetDet BMD WHERE BMD.DocId='" & TxtBudget.Tag & "') "
        StrSQL = StrSQL & "GROUP BY LG.CostCenter "
        StrSQL = StrSQL & ") As Tmp "
        StrSQL = StrSQL & "Group By CCName "
        StrSQL = StrSQL & "Order By CCName "
        DTTemp = cmain.FGetDatTable(StrSQL, Agl.Gcn)
        If DTTemp.Rows.Count > 0 Then
            FGMain.Rows.Add(DTTemp.Rows.Count)
        End If
        For I = 0 To DTTemp.Rows.Count - 1
            FGMain.Item(GSNo, I).Value = I + 1
            FGMain.Item(GCostCenter, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("CCName"))
            FGMain.Item(GCostCenterCode, I).Value = ""
            FGMain.Item(GBudget, I).Value = Format(Agl.VNull(DTTemp.Rows(I).Item("Budget")), "0.00")
            FGMain.Item(GAmount, I).Value = Format(Agl.VNull(DTTemp.Rows(I).Item("Actual")), "0.00")
            FGMain.Item(GDifference, I).Value = Format(Val(FGMain.Item(GBudget, I).Value) - Val(FGMain.Item(GAmount, I).Value), "0.00")
            If Val(FGMain.Item(GBudget, I).Value) <> 0 Then
                FGMain.Item(GDifferencePer, I).Value = Format((Val(FGMain.Item(GDifference, I).Value) * 100) / Val(FGMain.Item(GBudget, I).Value), "0.00")
            Else
                FGMain.Item(GDifferencePer, I).Value = Format(0, "0.00")
            End If
        Next
        DTTemp.Dispose()
        DTTemp = Nothing
    End Sub
    Public Sub FTxtGotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        '======== Write Your Code Below =============
    End Sub
    Public Sub FTxtKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.name
            Case TxtBudget.Name
                If e.KeyCode = Keys.Delete Then sender.Text = "" : sender.Tag = ""
        End Select
    End Sub
    Public Sub FTxtKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.Name
            Case TxtBudget.Name
                FHP_Budget(e, sender)
        End Select
    End Sub
    Private Sub FHP_Budget(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String

        StrSendText = Cmain.FSendText(Txt, e.KeyChar)
        DTMain = AgL.FillData("Select DocId,Name From Budget Where Site_Code='" & AgL.PubSiteCode & "' And V_Prefix='" & AgL.PubCompVPrefix & "' Order By Name", AgL.GCn).Tables(0)

        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 200, 280, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(1)
                Txt.Tag = FRH.DRReturn.Item(0)
            End If
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub FrmBudgetAnalysis_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Agl.FPaintForm(Me, e, 0)
    End Sub
    Private Sub BtnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOK.Click, BtnShow.Click
        Select Case sender.Name
            Case BtnOK.Name
                Me.Close()
            Case BtnShow.Name
                FFillBudget()
        End Select
    End Sub
End Class
