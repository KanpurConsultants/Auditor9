Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Imports System.ComponentModel

Public Class FrmBankReconciliation
    Private Const GSNo As Byte = 0
    Private Const GVType As Byte = 1
    Private Const GVDate As Byte = 2
    Private Const GNarration As Byte = 3
    Private Const GChqNo As Byte = 4
    Private Const GChqDate As Byte = 5
    Private Const GClrDate As Byte = 6
    Private Const GDocId As Byte = 7
    Private Const GDebit As Byte = 8
    Private Const GCredit As Byte = 9
    Private Const GBalance As Byte = 10

    Dim DbBankAmount As Double = 0
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Public WithEvents FGMain As New AgControls.AgDataGrid
    Private LIEvent As ClsEvents
    Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Private Sub FrmBankReconciliation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            LIEvent = New ClsEvents(Me)
            AgL.WinSetting(Me, 665, 990, 0, 0)
            AgL.GridDesign(FGMain)
            IniGrid()

            TxtDate.Focus()
            TxtDate.Text = AgL.PubLoginDate
            TxtType.Text = "Uncleared"
            TxtType.Tag = "U"
            TxtshowContra.Text = "No"
            TxtshowContra.Tag = "N"

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub IniGrid()
        FGMain.Height = PnlMain.Height
        FGMain.Width = PnlMain.Width
        FGMain.Top = PnlMain.Top
        FGMain.Left = PnlMain.Left
        PnlMain.Visible = False
        Controls.Add(FGMain)
        FGMain.Visible = True
        FGMain.BringToFront()
        AgCl.AddAgTextColumn(FGMain, "SNo", 42, 5, "S.No.", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "VType", 90, 5, "V.No.", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "VDate", 80, 0, "Date", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Narration", 330, 100, "Narration", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "ChqNo", 54, 12, "Chq No", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "ChqDate", 70, 20, "Chq. Date", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "ClrDate", 75, 20, "Clear Date", True, False, True)
        AgCl.AddAgTextColumn(FGMain, "DocId", 0, 0, "DocID", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "Debit", 80, 10, "Debit", True, True, True)
        AgCl.AddAgTextColumn(FGMain, "Credit", 80, 10, "Credit", True, True, True)
        AgCl.AddAgTextColumn(FGMain, "Balance", 80, 10, "Balance", True, True, True)

        FGMain.AllowUserToAddRows = False
        FGMain.Anchor = PnlMain.Anchor
        FGMain.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Arial"), 9)
        FGMain.DefaultCellStyle.Font = New Font(New FontFamily("Arial"), 8)
        FGMain.TabIndex = PnlMain.TabIndex
    End Sub
    Public Sub FTxtGotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        '======== Write Your Code Below =============	
    End Sub
    Public Sub FTxtKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        '======== Write Your Code Below =============	
        Select Case sender.Name
            Case TxtBankName.Name, TxtType.Name, TxtshowContra.Name
                If e.KeyCode = Keys.Delete Then
                    sender.Text = "" : sender.Tag = ""
                End If
        End Select
    End Sub
    Public Sub FTxtKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '======== Write Your Code Below =============	
        Select Case sender.Name
            Case TxtType.Name
                FHP_Type(e, sender)
            Case TxtBankName.Name
                FHP_BankName(e, sender)
            Case TxtshowContra.Name
                FHP_Contra(e, sender)
            Case TxtSite.Name
                FHP_SiteName(e, sender)
            Case TxtDivision.Name
                FHP_DivisionName(e, sender)

        End Select
    End Sub
    Private Sub FHP_Type(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String
        Dim StrSQL As String

        StrSQL = "Select 'C' AS Code, 'Cleared' as Name 
                  Union All 
                  Select 'U' as Code, 'Uncleared' as Name 
                  Union All 
                  Select 'A' as Code, 'All' as Name 
                  Order By Name "

        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        DTMain = AgL.FillData(StrSQL, AgL.GCn).Tables(0)

        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 200, 180, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(1)
                Txt.Tag = FRH.DRReturn.Item(0)
                FGMain.Rows.Clear()
            End If
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub FHP_Contra(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String
        Dim StrSQL As String


        StrSQL = "Select 'Y' as Code, 'Yes' as Name Union All Select 'N' as Code, 'No' as Name Order By Name"

        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        DTMain = AgL.FillData(StrSQL, AgL.GCn).Tables(0)

        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 200, 180, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(1)
                Txt.Tag = FRH.DRReturn.Item(0)
                FGMain.Rows.Clear()
            End If
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub FClear()
        FGMain.Rows.Clear()
    End Sub
    Private Sub FSave()
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New Object
        Dim I As Short
        Try
            If AgL.RequiredField(TxtDate, "As On Date.") Then Exit Sub
            If AgL.RequiredField(TxtBankName, "Bank Name.") Then Exit Sub
            If AgL.RequiredField(TxtType, "Type.") Then Exit Sub
            If AgL.RequiredField(TxtshowContra, "Show Contra.") Then Exit Sub

            If Not FGMain.Rows.Count > 0 Then MsgBox("There Are No Records To Save.") : Exit Sub

            BlnTrans = True
            GCnCmd = AgL.GCn.CreateCommand
            GCnCmd.Transaction = AgL.GCn.BeginTransaction(IsolationLevel.Serializable)
            For I = 0 To FGMain.Rows.Count - 1
                If Trim(FGMain(GDocId, I).Value) <> "" And Trim(FGMain(GClrDate, I).Value) <> "" Then
                    GCnCmd.CommandText = "Update Ledger Set "
                    GCnCmd.CommandText += "Clg_Date=" & AgL.Chk_Date(CDate(Trim(FGMain(GClrDate, I).Value)).ToString("s")) & " "
                    GCnCmd.CommandText += "Where DocId='" & FGMain(GDocId, I).Value & "' And V_SNo=" & Val(FGMain(GSNo, I).Value) & " "
                    GCnCmd.ExecuteNonQuery()
                ElseIf Trim(FGMain(GDocId, I).Value) <> "" And Trim(FGMain(GClrDate, I).Value) = "" Then
                    GCnCmd.CommandText = "Update Ledger Set "
                    GCnCmd.CommandText += "Clg_Date=Null "
                    GCnCmd.CommandText += "Where DocId='" & FGMain(GDocId, I).Value & "' And V_SNo=" & Val(FGMain(GSNo, I).Value) & " "
                    GCnCmd.ExecuteNonQuery()
                End If
            Next
            GCnCmd.Transaction.Commit()
            BlnTrans = False
            MsgBox(ClsMain.MsgSave)
            FFillGrid()
        Catch Ex As Exception
            If BlnTrans = True Then GCnCmd.Transaction.Rollback()
            MsgBox(Ex.Message)
        End Try
    End Sub
    Private Sub FrmBankReconciliation_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
        LblBG.BackColor = Color.LemonChiffon
        LblTitle.BackColor = Color.LemonChiffon
        LblAmountnotReflected.BackColor = Color.LemonChiffon
        Lblbalance.BackColor = Color.LemonChiffon
        Lblbank.BackColor = Color.LemonChiffon
        LblCompanyBal.BackColor = Color.LemonChiffon
        LblAmtNotClg_Dr.BackColor = Color.LemonChiffon
        LblAmtNotClg_Cr.BackColor = Color.LemonChiffon
        LblClgAmt.BackColor = Color.LemonChiffon
    End Sub
    Private Sub FGMain_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles FGMain.CellBeginEdit
        Select Case e.ColumnIndex
            Case GClrDate
                If FGMain(GDocId, e.RowIndex).Value = "" Then e.Cancel = True
        End Select
    End Sub
    Private Sub FGMain_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles FGMain.CellEndEdit
        Select Case e.ColumnIndex
            Case GClrDate
                FGMain(GClrDate, e.RowIndex).Value = AgL.RetDate(Trim(FGMain(GClrDate, e.RowIndex).Value))
                FCalculate()
        End Select
    End Sub
    Private Sub FFillGrid()
        Dim DTTemp As New DataTable
        Dim StrSQL As String, StrCondition As String = ""
        Dim I As Integer, J As Integer
        Dim Color_Main As Color, Color_A As Color, Color_B As Color

        If AgL.RequiredField(TxtDate, "As on Date.") Then Exit Sub
        If AgL.RequiredField(TxtBankName, "Bank Name.") Then Exit Sub
        If AgL.RequiredField(TxtType, "Type") Then Exit Sub

        FGMain.Rows.Clear()
        LblAmtNotClg_Dr.Text = 0
        LblClgAmt.Text = 0
        DbBankAmount = 0
        Color_A = Color.Linen
        Color_B = Color.Cornsilk

        Try
            If AgL.PubServerName = "" Then
                If UCase(TxtType.Tag) = "C" Then StrCondition = "And Date(LG.Clg_Date) Is Not Null And Date(LG.Clg_Date) <= " & AgL.Chk_Date(TxtDate.Text) & " "
                If UCase(TxtType.Tag) = "U" Then StrCondition = "And (LG.Clg_Date  Is Null  Or Date(LG.Clg_Date) > " & AgL.Chk_Date(TxtDate.Text) & ")  "
                If TxtFromDate.Text <> "" Then StrCondition += "And  Date(LG.V_Date) >= " & AgL.Chk_Date(TxtFromDate.Text) & "  "
            Else
                If UCase(TxtType.Tag) = "C" Then StrCondition = "And LG.Clg_Date Is Not Null And LG.Clg_Date <= " & AgL.Chk_Date(TxtDate.Text) & " "
                If UCase(TxtType.Tag) = "U" Then StrCondition = "And (LG.Clg_Date  Is Null  Or LG.Clg_Date > " & AgL.Chk_Date(TxtDate.Text) & ")  "
                If TxtFromDate.Text <> "" Then StrCondition += "And  LG.V_Date >= " & AgL.Chk_Date(TxtFromDate.Text) & "  "
            End If

            StrSQL = "Select LG.RecId,LG.V_Date As VDate,LG.V_Type As VType, "
            StrSQL += "LG.AmtDr As Debit,LG.AmtCr As Credit,LG.Narration, "
            StrSQL += "LG.Chq_No,LG.Chq_Date,LG.Clg_Date As ClgDate,LG.DocId,LG.V_SNo,ContraText "
            StrSQL += "From Ledger LG "
            StrSQL += "Where LG.SubCode='" & TxtBankName.Tag & "' "
            If AgL.PubServerName = "" Then
                StrSQL += "And Date(LG.V_Date) <= " & AgL.Chk_Date(TxtDate.Text) & " "
            Else
                StrSQL += "And LG.V_Date <= " & AgL.Chk_Date(TxtDate.Text) & " "
            End If
            If Val(TxtFromAmount.Text) > 0 Then
                StrSQL += " And IfNull(LG.AmtDr,0) + IfNull(LG.AmtCr,0) >= " & Val(TxtFromAmount.Text) & ""
            End If
            If Val(TxtToAmount.Text) > 0 Then
                StrSQL += " And IfNull(LG.AmtDr,0) + IfNull(LG.AmtCr,0) <= " & Val(TxtToAmount.Text) & ""
            End If

            StrSQL += StrCondition
            StrSQL += "Order By LG.V_Date,LG.V_Type,LG.RecId "
            DTTemp = CMain.FGetDatTable(StrSQL, AgL.GCn)

            For I = 0 To DTTemp.Rows.Count - 1
                FGMain.Rows.Add()
                J = FGMain.Rows.Count - 1
                If Color_Main = Color_B Then
                    Color_Main = Color_A
                Else
                    Color_Main = Color_B
                End If

                FGMain(GSNo, J).Value = AgL.VNull(DTTemp.Rows(I).Item("V_SNo"))
                FGMain(GVType, J).Value = AgL.XNull(DTTemp.Rows(I).Item("VType")) + "-" + AgL.XNull(DTTemp.Rows(I).Item("RecId"))
                FGMain(GVDate, J).Value = AgL.XNull(DTTemp.Rows(I).Item("VDate"))
                FGMain(GNarration, J).Value = AgL.XNull(DTTemp.Rows(I).Item("Narration"))
                FGMain(GDebit, J).Value = AgL.VNull(DTTemp.Rows(I).Item("Debit"))
                If Val(FGMain(GDebit, J).Value) = 0 Then FGMain(GDebit, J).Value = ""
                FGMain(GCredit, J).Value = AgL.XNull(DTTemp.Rows(I).Item("Credit"))
                If Val(FGMain(GCredit, J).Value) = 0 Then FGMain(GCredit, J).Value = ""
                If J = 0 Then
                    FGMain(GBalance, J).Value = Format(Val(FGMain(GDebit, J).Value) - Val(FGMain(GCredit, J).Value), "0.00")
                Else
                    If FGMain(GBalance, J - 1).Value.ToString.ToUpper.Contains("CR") Then
                        FGMain(GBalance, J).Value = Format(-Val(FGMain(GBalance, J - 1).Value) + Val(FGMain(GDebit, J).Value) - Val(FGMain(GCredit, J).Value), "0.00")
                    Else
                        FGMain(GBalance, J).Value = Format(Val(FGMain(GBalance, J - 1).Value) + Val(FGMain(GDebit, J).Value) - Val(FGMain(GCredit, J).Value), "0.00")
                    End If
                End If
                If Val(FGMain(GBalance, J).Value) < 0 Then
                    FGMain(GBalance, J).Value = Format(Math.Abs(Val(FGMain(GBalance, J).Value)), "0.00") & " Cr"
                Else
                    FGMain(GBalance, J).Value = Format(Math.Abs(Val(FGMain(GBalance, J).Value)), "0.00") & " Dr"
                End If
                FGMain(GChqNo, J).Value = AgL.XNull(DTTemp.Rows(I).Item("Chq_No"))
                FGMain(GChqDate, J).Value = AgL.XNull(DTTemp.Rows(I).Item("Chq_Date"))
                FGMain(GClrDate, J).Value = AgL.XNull(DTTemp.Rows(I).Item("ClgDate"))
                FGMain(GDocId, J).Value = AgL.XNull(DTTemp.Rows(I).Item("DocID"))
                FGMain.Rows(J).DefaultCellStyle.BackColor = Color_Main

                If UCase(Trim(TxtshowContra.Tag)) = "Y" Then
                    If Trim(AgL.XNull(DTTemp.Rows(I).Item("ContraText"))) <> "" Then
                        FGMain.Rows.Add()
                        J = FGMain.Rows.Count - 1
                        FGMain.Rows(J).DefaultCellStyle.BackColor = Color_Main
                        FGMain.Rows(J).DefaultCellStyle.Font = New Font("Courier New", 9, FontStyle.Italic)
                        FGMain(GNarration, J).Value = AgL.XNull(DTTemp.Rows(I).Item("ContraText"))
                        FGMain(GNarration, J).Style.WrapMode = DataGridViewTriState.True
                        FGMain.Rows(J).Height = Split(AgL.XNull(DTTemp.Rows(I).Item("ContraText")), vbCrLf).Length * 20
                    End If
                End If
            Next
            DTTemp.Clear()

            StrSQL = "Select (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(AmtCr),0)) As Amount "
            StrSQL += "From Ledger LG "
            If AgL.PubServerName = "" Then
                StrSQL += "Where Date(LG.V_Date)<=" & AgL.Chk_Date(TxtDate.Text) & " And "
            Else
                StrSQL += "Where LG.V_Date<=" & AgL.Chk_Date(TxtDate.Text) & " And "
            End If


            StrSQL += "LG.SubCode='" & TxtBankName.Tag & "' "
            StrSQL += "Group By LG.SubCode "
            DTTemp = CMain.FGetDatTable(StrSQL, AgL.GCn)
            If DTTemp.Rows.Count > 0 Then
                If AgL.VNull(DTTemp.Rows(0).Item("Amount")) < 0 Then
                    LblCompanyBal.Text = Format(Math.Abs(AgL.VNull(DTTemp.Rows(0).Item("Amount"))), "0.00") + " Cr"
                Else
                    LblCompanyBal.Text = Format(Math.Abs(AgL.VNull(DTTemp.Rows(0).Item("Amount"))), "0.00") + " Dr"
                End If
            End If
            DTTemp.Clear()

            StrSQL = "Select (IfNull(Sum(LG.AmtDr),0)-IfNull(Sum(AmtCr),0)) As Amount "
            StrSQL += "From Ledger LG  "
            If AgL.PubServerName = "" Then
                StrSQL += "Where Date(LG.V_Date)<=" & AgL.Chk_Date(TxtDate.Text) & " And "
            Else
                StrSQL += "Where LG.V_Date<=" & AgL.Chk_Date(TxtDate.Text) & " And "
            End If
            StrSQL += "LG.SubCode='" & TxtBankName.Tag & "' And "
            If AgL.PubServerName = "" Then
                StrSQL += " Date(LG.Clg_Date) <=" & AgL.Chk_Date(TxtDate.Text) & " "
            Else
                StrSQL += "LG.Clg_Date <=" & AgL.Chk_Date(TxtDate.Text) & " "
            End If
            StrSQL += "Group By LG.SubCode "
            DTTemp = CMain.FGetDatTable(StrSQL, AgL.GCn)
            If DTTemp.Rows.Count > 0 Then
                If TxtType.Tag = "U" Then
                    DbBankAmount = AgL.VNull(DTTemp.Rows(0).Item("Amount"))
                Else
                    DbBankAmount = 0
                End If

                If AgL.VNull(DTTemp.Rows(0).Item("Amount")) < 0 Then
                    LblClgAmt.Text = Format(Math.Abs(AgL.VNull(DTTemp.Rows(0).Item("Amount"))), "0.00") + " Cr"
                Else
                    LblClgAmt.Text = Format(Math.Abs(AgL.VNull(DTTemp.Rows(0).Item("Amount"))), "0.00") + " Dr"
                End If
            End If
            DTTemp.Dispose()
            FCalculate()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FCalculate()
        Dim I As Integer
        Dim DblDrAmt As Double = 0
        Dim DblCrAmt As Double = 0
        Dim DblCgDrAmt As Double = 0
        Dim DblCgCrAmt As Double = 0
        Dim DblBankFinalAmt As Double = 0
        Dim mClearDays As Integer = 0

        LblAmtNotClg_Dr.Text = 0
        LblAmtNotClg_Cr.Text = 0
        LblClgAmt.Text = 0
        For I = 0 To FGMain.Rows.Count - 1
            If Trim(FGMain(GClrDate, I).Value) <> "" Then
                mClearDays = DateDiff(DateInterval.Day, CDate(Trim(FGMain(GClrDate, I).Value)), CDate(TxtDate.Text))
            Else
                mClearDays = 0
            End If
            'If Trim(FGMain(GDocId, I).Value) <> "" And Trim(FGMain(GClrDate, I).Value) = "" Then
            If Trim(FGMain(GDocId, I).Value) <> "" And mClearDays <= 0 Then
                DblDrAmt += Val(FGMain(GDebit, I).Value)
                DblCrAmt += Val(FGMain(GCredit, I).Value)
            Else
                DblCgDrAmt += Val(FGMain(GDebit, I).Value)
                DblCgCrAmt += Val(FGMain(GCredit, I).Value)
            End If
        Next

        DblBankFinalAmt = DbBankAmount + (DblCgDrAmt - DblCgCrAmt)

        LblAmtNotClg_Dr.Text = Format(DblDrAmt, "0.00") + " Dr"
        LblAmtNotClg_Cr.Text = Format(DblCrAmt, "0.00") + " Cr"

        If DblBankFinalAmt < 0 Then
            LblClgAmt.Text = Format(Math.Abs(DblBankFinalAmt), "0.00") + " Cr"
        Else
            LblClgAmt.Text = Format(Math.Abs(DblBankFinalAmt), "0.00") + " Dr"
        End If
    End Sub
    Private Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click, BtnExit.Click, BtnFillGrid.Click, BtnPrint.Click
        Select Case sender.Name
            Case BtnSave.Name
                FSave()
            Case BtnFillGrid.Name
                FFillGrid()
            Case BtnExit.Name
                Me.Close()
            Case BtnPrint.Name
                FFillDTPrint()
        End Select
    End Sub
    Private Sub FFillDTPrint()
        Dim RptReg As New ReportDocument
        Dim I As Integer, J As Integer
        Dim DRRow As DataRow
        Dim DTPrint As New DataTable("T")
        Dim StrReportName As String

        Me.Cursor = Cursors.WaitCursor
        DTPrint.Columns.Add("GSNo", System.Type.GetType("System.String"))
        DTPrint.Columns.Add("VType", System.Type.GetType("System.String"))
        DTPrint.Columns.Add("VDate", System.Type.GetType("System.String"))
        DTPrint.Columns.Add("Narration", System.Type.GetType("System.String"))
        DTPrint.Columns.Add("ChqNo", System.Type.GetType("System.String"))
        DTPrint.Columns.Add("ChqDate", System.Type.GetType("System.String"))
        DTPrint.Columns.Add("ClearDate", System.Type.GetType("System.String"))
        DTPrint.Columns.Add("DocID", System.Type.GetType("System.String"))
        DTPrint.Columns.Add("Debit", System.Type.GetType("System.Double"))
        DTPrint.Columns.Add("Credit", System.Type.GetType("System.Double"))

        For I = 0 To FGMain.Rows.Count - 1
            DRRow = DTPrint.NewRow()
            For J = 0 To DTPrint.Columns.Count - 1
                Select Case J
                    Case GDebit, GCredit
                        DRRow(J) = Val(FGMain(J, I).Value)
                    Case Else
                        DRRow(J) = Trim(FGMain(J, I).Value)
                End Select
            Next
            DTPrint.Rows.Add(DRRow)
        Next


        Try
            AgL.PubReportTitle = "Bank Reconcilation Report"

            StrReportName = "Bank Reconcilation"
            DTPrint.WriteXmlSchema(AgL.PubReportPath & "\" & StrReportName & ".Xml")
            RptReg.Load(AgL.PubReportPath & "\" & StrReportName & ".rpt")
            RptReg.SetDataSource(DTPrint)
            FormulaSet(RptReg, Me)

            For I = 0 To RptReg.DataDefinition.FormulaFields.Count - 1
                Select Case CStr(UCase(RptReg.DataDefinition.FormulaFields.Item(I).Name))
                    Case "DATENAME"
                        RptReg.DataDefinition.FormulaFields.Item(I).Text = " " & AgL.Chk_Text(CDate(TxtDate.Text).ToString("s")) & " "
                    Case "BANKNAME"
                        RptReg.DataDefinition.FormulaFields.Item(I).Text = "'" & TxtBankName.Text & "' "
                    Case "COMPANYBAL"
                        RptReg.DataDefinition.FormulaFields.Item(I).Text = "'" & LblCompanyBal.Text & "' "
                    Case "NOTCLGAMTCR"
                        RptReg.DataDefinition.FormulaFields.Item(I).Text = "'" & LblAmtNotClg_Cr.Text & "' "
                    Case "NOTCLGAMTDR"
                        RptReg.DataDefinition.FormulaFields.Item(I).Text = "'" & LblAmtNotClg_Dr.Text & "' "
                    Case "CLGAMT"
                        RptReg.DataDefinition.FormulaFields.Item(I).Text = "'" & LblClgAmt.Text & "' "
                End Select
            Next
            CMain.FShowReport(RptReg, Me.MdiParent, Me.Text)
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub FHP_BankName(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String

        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        DTMain = AgL.FillData("Select SubCode,Name from Subgroup where Nature In ('Bank','Cash') And (SiteList Like '%|" & AgL.PubSiteCode & "|%' Or SiteList Is Null) Order By Name", AgL.GCn).Tables(0)

        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 200, 280, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(1)
                Txt.Tag = FRH.DRReturn.Item(0)
                FGMain.Rows.Clear()
            End If
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub

    Private Sub FHP_SiteName(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String

        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        DTMain = AgL.FillData("SELECT Code, Name FROM SiteMast ORDER BY Name ", AgL.GCn).Tables(0)

        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 200, 280, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(1)
                Txt.Tag = FRH.DRReturn.Item(0)
                FGMain.Rows.Clear()
            End If
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub

    Private Sub FHP_DivisionName(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String

        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        DTMain = AgL.FillData("SELECT Div_Code Code, Div_Name Name FROM Division ORDER BY Div_Name ", AgL.GCn).Tables(0)

        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 200, 280, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(1)
                Txt.Tag = FRH.DRReturn.Item(0)
                FGMain.Rows.Clear()
            End If
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub


    Private Sub TxtDate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtDate.Validated
        Select Case sender.name
            Case TxtDate.Name
                sender.Text = AgL.RetDate(sender.Text)
        End Select
    End Sub

    Private Sub FGMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FGMain.KeyDown
        Try
            Select Case FGMain.CurrentCell.ColumnIndex
                Case GClrDate
                    If e.KeyCode = 46 Then
                        FGMain.CurrentCell.Value = ""
                        FCalculate()
                    End If
            End Select
        Catch Ex As NullReferenceException
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Private Sub FGMain_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles FGMain.EditingControl_Validating

    End Sub


End Class