Imports System.Data.SQLite
Public Class FrmChequePrinting
    Private Const GSearchCode As Byte = 0
    Private Const GV_Type As Byte = 1
    Private Const GV_Date As Byte = 2
    Private Const GRecId As Byte = 3
    Private Const GAccountName As Byte = 4
    Private Const GChequeNo As Byte = 5
    Private Const GChequeDt As Byte = 6
    Private Const GDebit As Byte = 7
    Private Const GCredit As Byte = 8
    Private Const GPrintName As Byte = 9
    Private Const GV_SNo As Byte = 10

    Private LIEvent As ClsEvents
    Public WithEvents FGMain As New AgControls.AgDataGrid
    Private Sub FrmChequePrinting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LIEvent = New ClsEvents(Me)
        AgL.WinSetting(Me, 660, 980, 0, 0)
        AgL.GridDesign(FGMain)
        TxtFromDate.Text = AgL.PubLoginDate
        TxtToDate.Text = AgL.PubLoginDate
        IniGrid()
    End Sub
    Private Sub IniGrid()
        FGMain.Columns.Clear()
        FGMain.Height = PnlMain.Height
        FGMain.Width = PnlMain.Width
        FGMain.Top = PnlMain.Top
        FGMain.Left = PnlMain.Left
        PnlMain.Visible = False
        Controls.Add(FGMain)
        FGMain.Visible = True
        FGMain.BringToFront()
        FGMain.AllowUserToAddRows = False
        AgCl.AddAgTextColumn(FGMain, "SearchCode", 0, 0, "SearchCode", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "Type", 50, 0, "Type", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Date", 80, 0, "Date", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "RecId", 70, 0, "RecId", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "AccountName", 190, 0, "Account", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Cheque No.", 80, 20, "Cheque No.", True, False, True)
        AgCl.AddAgTextColumn(FGMain, "Cheque Date", 80, 20, "Cheque Date", True, False, True)
        AgCl.AddAgTextColumn(FGMain, "Debit", 80, 0, "Debit", True, True, True)
        AgCl.AddAgTextColumn(FGMain, "Credit", 80, 0, "Credit", True, True, True)
        AgCl.AddAgTextColumn(FGMain, "Print Name", 242, 50, "Print Name", True, False, False)
        AgCl.AddAgTextColumn(FGMain, "V_SNo", 0, 0, "V_SNo", False, True, False)

        FGMain.DefaultCellStyle.Font = New Font("Arial", 9)
        FGMain.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 9)

        FGMain.ColumnHeadersHeight = 40
        FGMain.Anchor = PnlMain.Anchor
        FGMain.TabIndex = PnlMain.TabIndex
    End Sub
    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click, BtnPrint.Click, BtnFill.Click
        Select Case sender.name
            Case BtnClose.Name
                Me.Close()
            Case BtnFill.Name
                FFillCheque()
            Case BtnPrint.Name
                FPrint()
        End Select
    End Sub
    Private Sub FPrint()
        Dim RptReg As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim GCnCmd As New Object
        Dim I As Integer, DblAmount As Double
        Dim StrSQL As String
        Dim DTTemp As DataTable
        Dim StrPaperSize As String

        If Not FGMain.Rows.Count > 0 Then MsgBox("There Is No Record To Print.") : Exit Sub
        If MsgBox("Are You Sure? You Want To Print Cheque(s)." & vbCrLf & "This Will Update Cheque No. In Voucher(s).") = MsgBoxResult.No Then Exit Sub

        Me.Cursor = Cursors.WaitCursor
        StrSQL = "Declare @TmpTable Table (Chq_No VarChar(20),Chq_Date SmallDateTime,FavourTo VarChar(50),Amount Float,SNo Int) "
        GCnCmd = AgL.GCn.CreateCommand
        For I = 0 To FGMain.Rows.Count - 1
            GCnCmd.CommandText = "Update Ledger Set "
            GCnCmd.CommandText += "Chq_No=" & AgL.Chk_Text(FGMain(GChequeNo, I).Value) & ", "
            GCnCmd.CommandText += "Chq_Date=" & AgL.Chk_Qry(CDate(FGMain(GChequeDt, I).Value.ToString).ToString("s")) & " "
            GCnCmd.CommandText += "Where DocId='" & FGMain(GSearchCode, I).Value & "' "
            GCnCmd.CommandText += "And V_SNo=" & Val(FGMain(GV_SNo, I).Value) & " "
            GCnCmd.ExecuteNonQuery()

            GCnCmd.CommandText = "Update Ledger_Temp Set "
            GCnCmd.CommandText += "Chq_No=" & AgL.Chk_Text(FGMain(GChequeNo, I).Value) & ", "
            GCnCmd.CommandText += "Chq_Date=" & AgL.Chk_Qry(CDate(FGMain(GChequeDt, I).Value.ToString).ToString("s")) & " "
            GCnCmd.CommandText += "Where DocId='" & FGMain(GSearchCode, I).Value & "' "
            GCnCmd.CommandText += "And V_SNo=" & Val(FGMain(GV_SNo, I).Value) & " "
            GCnCmd.ExecuteNonQuery()

            If Val(FGMain(GDebit, I).Value) > 0 Then
                DblAmount = Val(FGMain(GDebit, I).Value)
            Else
                DblAmount = Val(FGMain(GCredit, I).Value)
            End If

            StrSQL += "Insert Into @TmpTable (Chq_No,Chq_Date,FavourTo,Amount,SNo) Values ( "
            StrSQL += "" & AgL.Chk_Text(FGMain(GChequeNo, I).Value) & ","
            StrSQL += "" & AgL.Chk_Text(CDate(FGMain(GChequeDt, I).Value.ToString).ToString("s")) & ","
            StrSQL += "" & AgL.Chk_Text(FGMain(GPrintName, I).Value) & ","
            StrSQL += "" & DblAmount & "," & I & ")"
        Next

        StrSQL += "Select Chq_No,Chq_Date,FavourTo,Amount,SNo From @TmpTable Order By SNo"
        DTTemp = CMain.FGetDatTable(StrSQL, AgL.GCn)


        DTTemp.WriteXmlSchema(AgL.PubReportPath & "\ChequePrinting.Xml")
        RptReg.Load(AgL.PubReportPath & "\ChequePrinting.Rpt")

        StrPaperSize = FChequePrintSetup(TxtBankName.Tag, RptReg)
        RptReg.SetDataSource(DTTemp)
        CMain.FShowReport(RptReg, Me.MdiParent, Me.Name, , StrPaperSize, "N")

        Me.Cursor = Cursors.Default
    End Sub
    Private Function FChequePrintSetup(ByVal StrBankCode As String, ByRef RptReg As CrystalDecisions.CrystalReports.Engine.ReportDocument) As String
        Dim DTTemp As DataTable
        Dim StrPaperSize As String = ""
        DTTemp = CMain.FGetDatTable("Select * From ChequePrintSetup Where BankCode='" & StrBankCode & "'", AgL.GCn)

        If DTTemp.Rows.Count > 0 Then
            RptReg.DataDefinition.FormulaFields("FrmAuthorized").Text = "'" & AgL.XNull(DTTemp.Rows(0).Item("AuthorizedBy")) & "'"
            RptReg.DataDefinition.FormulaFields("FrmFor").Text = "'" & AgL.XNull(DTTemp.Rows(0).Item("CompanyName")) & "'"

            RptReg.ReportDefinition.Sections("SctMain").ReportObjects.Item("ChqDate").Left = AgL.VNull(DTTemp.Rows(0).Item("ChequeDate_Left"))
            RptReg.ReportDefinition.Sections("SctMain").ReportObjects.Item("ChqDate").Top = AgL.VNull(DTTemp.Rows(0).Item("ChequeDate_Top"))

            RptReg.ReportDefinition.Sections("SctMain").ReportObjects.Item("FavourOf").Left = AgL.VNull(DTTemp.Rows(0).Item("FavourOf_Left"))
            RptReg.ReportDefinition.Sections("SctMain").ReportObjects.Item("FavourOf").Top = AgL.VNull(DTTemp.Rows(0).Item("FavourOf_Top"))

            RptReg.ReportDefinition.Sections("SctMain").ReportObjects.Item("AmountsInWords").Left = AgL.VNull(DTTemp.Rows(0).Item("AmountInWords_Left"))
            RptReg.ReportDefinition.Sections("SctMain").ReportObjects.Item("AmountsInWords").Top = AgL.VNull(DTTemp.Rows(0).Item("AmountInWords_Top"))

            RptReg.ReportDefinition.Sections("SctMain").ReportObjects.Item("FrmAuthorized").Left = AgL.VNull(DTTemp.Rows(0).Item("Authorized_Left"))
            RptReg.ReportDefinition.Sections("SctMain").ReportObjects.Item("FrmAuthorized").Top = AgL.VNull(DTTemp.Rows(0).Item("Authorized_Top"))

            RptReg.ReportDefinition.Sections("SctMain").ReportObjects.Item("FrmFor").Left = AgL.VNull(DTTemp.Rows(0).Item("CompanyName_Left"))
            RptReg.ReportDefinition.Sections("SctMain").ReportObjects.Item("FrmFor").Top = AgL.VNull(DTTemp.Rows(0).Item("CompanyName_Top"))

            RptReg.ReportDefinition.Sections("SctMain").ReportObjects.Item("Amount").Left = AgL.VNull(DTTemp.Rows(0).Item("Amount_Left"))
            RptReg.ReportDefinition.Sections("SctMain").ReportObjects.Item("Amount").Top = AgL.VNull(DTTemp.Rows(0).Item("Amount_Top"))
            StrPaperSize = AgL.XNull(DTTemp.Rows(0).Item("PaperSizeName"))
        Else
            MsgBox("Cheque Print Setup Is Not Define For This Bank.")
        End If
        DTTemp.Dispose()
        FChequePrintSetup = StrPaperSize

    End Function
    Private Sub FFillCheque()
        Dim I As Integer
        Dim DblChequeNo As Double

        If MsgBox("Are You Sure? You Want To Replace Cheque No.") = MsgBoxResult.Yes Then
            DblChequeNo = Val(TxtChequeNo.Text)
            If DblChequeNo > 0 Then
                For I = 0 To FGMain.Rows.Count - 1
                    FGMain(GChequeNo, I).Value = Trim(DblChequeNo)
                    If Trim(TxtChequeDt.Text) <> "" Then FGMain(GChequeDt, I).Value = TxtChequeDt.Text
                    DblChequeNo += 1
                Next
            End If
        End If
    End Sub
    Private Sub FFillGrid()
        Dim DTTemp As New DataTable
        Dim I As Integer
        Dim StrSQL As String, StrCompare As String = ""
        Dim Color_Main As Color, Color_A As Color, Color_B As Color
        Dim StrCondition As String = ""
        Dim FrmPB As New FrmProgressBar

        FrmPB.Show()
        FrmPB.FMoveBar()
        FGMain.Rows.Clear()
        Color_A = Color.Linen
        Color_B = Color.Cornsilk

        If UCase(Trim(TxtVNo.Text)) = "ALL" Then
            StrCondition = "(Select LG1.DocId From Ledger LG1 "
            StrCondition += "Where (LG1.V_Date Between '" & CDate(TxtFromDate.Text).ToString("s") & "' And '" & CDate(TxtToDate.Text).ToString("s") & "') "
            StrCondition += "And LG1.V_Type='" & TxtVType.Tag & "' And LG1.Site_Code='" & AgL.PubSiteCode & "' "
            StrCondition += "And LG1.SubCode='" & TxtBankName.Tag & "' )"
        Else
            StrCondition = " (" & TxtVNo.Tag & ") "
        End If

        StrSQL = "Select LG.DocId As SearchCode,LG.V_Type,LG.V_No,LG.V_Date,SG.Name As Party, "
        StrSQL += "LG.AmtDr,LG.AmtCr,LG.Chq_No,LG.Chq_Date,LG.V_SNo "
        StrSQL += "From Ledger LG Left Join "
        StrSQL += "DataAudit DA On LG.DocId=DA.DocId Left Join "
        StrSQL += "SubGroup SG On LG.SubCode=SG.SubCode "
        StrSQL += "Where LG.DocId In "
        StrSQL += StrCondition
        StrSQL += "Order By LG.V_Date,LG.V_No,LG.V_SNo "

        FrmPB.FMoveBar()
        DTTemp = CMain.FGetDatTable(StrSQL, AgL.GCn)
        FrmPB.FMoveBar()
        If DTTemp.Rows.Count > 0 Then
            FGMain.Rows.Add(DTTemp.Rows.Count)
        End If
        For I = 0 To DTTemp.Rows.Count - 1
            If StrCompare <> AgL.XNull(DTTemp.Rows(I).Item("SearchCode")) Then
                If Color_Main = Color_B Then
                    Color_Main = Color_A
                Else
                    Color_Main = Color_B
                End If
            End If

            FGMain(GSearchCode, I).Value = AgL.XNull(DTTemp.Rows(I).Item("SearchCode"))
            FGMain(GV_Type, I).Value = AgL.XNull(DTTemp.Rows(I).Item("V_Type"))
            FGMain(GV_Date, I).Value = AgL.XNull(DTTemp.Rows(I).Item("V_Date"))
            FGMain(GRecId, I).Value = AgL.XNull(DTTemp.Rows(I).Item("RecId"))
            FGMain(GAccountName, I).Value = AgL.XNull(DTTemp.Rows(I).Item("Party"))
            FGMain(GDebit, I).Value = Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.000")
            FGMain(GCredit, I).Value = Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.000")
            FGMain(GPrintName, I).Value = AgL.XNull(DTTemp.Rows(I).Item("Party"))

            FGMain(GChequeNo, I).Value = AgL.XNull(DTTemp.Rows(I).Item("Chq_No"))
            FGMain(GChequeDt, I).Value = AgL.XNull(DTTemp.Rows(I).Item("Chq_Date"))
            FGMain(GV_SNo, I).Value = AgL.XNull(DTTemp.Rows(I).Item("V_SNo"))

            If Val(FGMain(GDebit, I).Value) = 0 Then FGMain(GDebit, I).Value = ""
            If Val(FGMain(GCredit, I).Value) = 0 Then FGMain(GCredit, I).Value = ""
            FGMain.Rows(I).DefaultCellStyle.BackColor = Color_Main
            StrCompare = AgL.XNull(DTTemp.Rows(I).Item("SearchCode"))
            FrmPB.FMoveBar()
        Next
        DTTemp.Clear()
        DTTemp = Nothing
        FrmPB.Dispose()
    End Sub
    Public Sub FTxtKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.Name
            Case TxtBankName.Name, TxtVType.Name, TxtVNo.Name
                If e.KeyCode = Keys.Delete Then
                    sender.Text = "" : sender.Tag = ""
                End If
        End Select
    End Sub

    Public Sub FTxtKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.Name
            Case TxtBankName.Name
                FHP_Account(e, sender)
            Case TxtVType.Name
                FHP_Type(e, sender)
            Case TxtVNo.Name
                FHP_VNo(e, sender)
        End Select
    End Sub
    Public Sub FTxtGotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        '======== Write Your Code Below =============
    End Sub
    Private Sub FHP_Account(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim StrSendText As String, StrPrvText As String
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSQL As String

        StrPrvText = Txt.Text
        StrSendText = CMain.FSendText(Txt, e.KeyChar)

        StrSQL = "Select SG.SubCode As Code,SG.Name,SG.ManualCode,AG.GroupName "
        StrSQL += "From SubGroup SG "
        StrSQL += "Left Join AcGroup AG ON AG.GroupCode=SG.GroupCode "
        StrSQL += "Where SG.Nature='Bank' "
        StrSQL += "Order By SG.Name "

        DTMain = CMain.FGetDatTable(StrSQL, AgL.GCn)
        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 300, 630, (Top + Txt.Top) + 85, Left + Txt.Left + 3)

        FRH.FFormatColumn(0, "Code", 0, , False)
        FRH.FFormatColumn(1, "Name", 250, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(2, "Manual Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(3, "Group Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.StartPosition = FormStartPosition.CenterScreen
        FRH.ShowDialog()

        Txt.Text = StrPrvText
        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(1)
                Txt.Tag = FRH.DRReturn.Item(0)
                TxtVNo.Text = ""
                TxtVNo.Tag = ""
            End If
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub FHP_Type(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String
        Dim StrSQL As String

        StrSQL = "Select VT.V_Type,VT.Description,VT.DefaultAc,SG.Name As DAName "
        StrSQL += "From Voucher_Type VT "
        StrSQL += "Left Join SubGroup SG On VT.DefaultAc=SG.SubCode "
        StrSQL += "Where VT.Category In ('PMT','RCT','JV') "
        StrSQL += "Order By VT.Description "
        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        DTMain = AgL.FillData(StrSQL, AgL.GCn).Tables(0)

        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 200, 280, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(2, , 0, , False)
        FRH.FFormatColumn(3, , 0, , False)
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(1)
                Txt.Tag = FRH.DRReturn.Item(0)
                TxtBankName.Text = AgL.XNull(FRH.DRReturn.Item("DAName"))
                TxtBankName.Tag = AgL.XNull(FRH.DRReturn.Item("DefaultAc"))
                TxtVNo.Text = ""
                TxtVNo.Tag = ""
            End If
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub FHP_VNo(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSQL As String
        Dim StrSendText As String, StrPrvText As String

        StrPrvText = Txt.Text
        StrSendText = CMain.FSendText(Txt, e.KeyChar)

        StrSQL = "Select 'o' As Tick, LG.DocId,LG.V_No From LedgerM_Temp LG "
        StrSQL += "Where LG.DocId In "
        StrSQL += "(Select LGT.DocId From Ledger_Temp LGT "
        StrSQL += "Where LGT.Site_Code='" & AgL.PubSiteCode & "' And "
        StrSQL += "LGT.V_Type='" & TxtVType.Tag & "' And LGT.SubCode='" & TxtBankName.Tag & "' And "
        StrSQL += "(LGT.V_Date Between '" & AgL.RetDate(CDate(TxtFromDate.Text).ToString("s")) & "' And "
        StrSQL += "'" & AgL.RetDate(TxtToDate.Text) & "')) "
        StrSQL += "Order By LG.V_Date "

        DTMain = CMain.FGetDatTable(StrSQL, AgL.GCn)
        FRH = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DTMain), StrSendText, 380, 350, (Top + Txt.Top) + 85, Left + Txt.Left + 3, False)

        FRH.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter)
        FRH.FFormatColumn(1, "Code", 0, , False)
        FRH.FFormatColumn(2, "Name", 200, DataGridViewContentAlignment.MiddleLeft)

        FRH.StartPosition = FormStartPosition.CenterScreen
        FRH.ShowDialog()

        Txt.Text = StrPrvText

        If FRH.BytBtnValue = 0 Then
            Txt.Text = FRH.FFetchData(2, "", "", ",")
            Txt.Tag = FRH.FFetchData(1, "'", "'", ",", True)
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub FGMain_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles FGMain.CellEndEdit
        Select Case e.ColumnIndex
            Case GChequeDt
                FGMain(e.ColumnIndex, e.RowIndex).Value = AgL.RetDate(FGMain(e.ColumnIndex, e.RowIndex).Value.ToString)
        End Select
    End Sub

    Private Sub FGMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FGMain.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            FGMain.CurrentRow.Selected = True
        End If
    End Sub
    Private Sub BtnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnShow.Click

        If AgL.RequiredField(TxtFromDate, "From Date") Then Exit Sub
        If AgL.RequiredField(TxtToDate, "To Date") Then Exit Sub
        If AgL.RequiredField(TxtVType, "Voucher Type") Then Exit Sub
        If AgL.RequiredField(TxtBankName, "Bank Name") Then Exit Sub
        If AgL.RequiredField(TxtVNo, "Voucher No.") Then Exit Sub

        FFillGrid()
    End Sub
    Private Sub FrmChequePrinting_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub
    Private Sub TxtFromDate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtFromDate.Validated, TxtToDate.Validated, TxtChequeDt.Validated
        Select Case sender.Name
            Case TxtFromDate.Name, TxtToDate.Name, TxtChequeDt.Name
                sender.Text = AgL.RetDate(sender.Text)
        End Select
    End Sub
End Class