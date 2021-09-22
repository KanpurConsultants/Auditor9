Public Class FrmSmartFinder
    Private Const GSNo As Byte = 0
    Private Const GSearchCode As Byte = 1
    Private Const GV_Type As Byte = 2
    Private Const GV_Date As Byte = 3
    Private Const GRecId As Byte = 4
    Private Const GAccountName As Byte = 5
    Private Const GChequeNo As Byte = 6
    Private Const GItemName As Byte = 7
    Private Const GUnit As Byte = 8
    Private Const GIngoming As Byte = 9
    Private Const GOutgoing As Byte = 10
    Private Const GRemarks As Byte = 11

    Private LIEvent As ClsEvents
    Public WithEvents FGMain As New AgControls.AgDataGrid
    Private DTVType As DataTable
    Private Sub FrmSmartFinder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LIEvent = New ClsEvents(Me)
        Agl.WinSetting(Me, 660, 980, 0, 0)
        Agl.GridDesign(FGMain)
        TxtFindIn.Text = "Ledger"
        TxtFindIn.Tag = "L"
        TxtType.Text = "Both"
        TxtType.Tag = "B"
        TxtFromDate.Text = Agl.PubStartDate
        TxtToDate.Text = Agl.PubLoginDate
        TxtVoucherType.Text = "All"
        TxtAccountName.Text = "All"
        TxtItemName.Text = "All"
        TxtChequeNo.Text = "All"
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
        AgCl.AddAgTextColumn(FGMain, "S.No.", 42, 5, "S.No.", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "SearchCode", 0, 0, "SearchCode", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "Type", 60, 0, "Type", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Date", 80, 0, "Date", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "RecId", 80, 0, "RecId", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "AccountName", 250, 0, "Account", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Cheque No.", 80, 0, "Cheque No.", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Item", 150, 0, "Item", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Unit", 50, 0, "Unit", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Incoming", 100, 0, "Incoming", True, True, True)
        AgCl.AddAgTextColumn(FGMain, "Outgoing", 100, 0, "Outgoing", True, True, True)
        AgCl.AddAgTextColumn(FGMain, "Remarks", 230, 0, "Remarks", True, True, False)

        FGMain.DefaultCellStyle.Font = New Font("Arial", 9)
        FGMain.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 9)

        FGMain.Anchor = PnlMain.Anchor
        FGMain.TabIndex = PnlMain.TabIndex
        FSetGrid()
    End Sub
    Private Sub FSetGrid()
        FGMain.Rows.Clear()
        TxtItemName.Text = "All"
        TxtItemName.Tag = ""
        TxtChequeNo.Text = "All"
        TxtChequeNo.Tag = ""
        If TxtFindIn.Tag = "L" Then
            FGMain.Columns(GAccountName).Width = 250
            FGMain.Columns(GChequeNo).Width = 80
            FGMain.Columns(GRemarks).Width = 150

            FGMain.Columns(GIngoming).HeaderText = "Debit"
            FGMain.Columns(GOutgoing).HeaderText = "Cerdit"

            FGMain.Columns(GChequeNo).Visible = True
            FGMain.Columns(GItemName).Visible = False
            FGMain.Columns(GUnit).Visible = False
            TxtItemName.Enabled = False
            TxtChequeNo.Enabled = True
        ElseIf TxtFindIn.Tag = "S" Then
            FGMain.Columns(GAccountName).Width = 150
            FGMain.Columns(GRemarks).Width = 130
            FGMain.Columns(GIngoming).HeaderText = "Receive"
            FGMain.Columns(GOutgoing).HeaderText = "Issue"

            FGMain.Columns(GItemName).Visible = True
            FGMain.Columns(GUnit).Visible = True
            FGMain.Columns(GChequeNo).Visible = False

            TxtChequeNo.Enabled = False
            TxtItemName.Enabled = True
        End If
    End Sub
    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Select Case sender.name
            Case BtnClose.Name
                Me.Close()
        End Select
    End Sub
    Private Sub FFillGrid()
        Dim DTTemp As New DataTable
        Dim I As Integer
        Dim StrSQL As String, StrCompare As String = ""
        Dim Color_Main As Color, Color_A As Color, Color_B As Color
        Dim StrCondition As String
        Dim FrmPB As New FrmProgressBar

        FrmPB.Show()
        FrmPB.FMoveBar()
        FGMain.Rows.Clear()
        Color_A = Color.Khaki
        Color_B = Color.Cornsilk

        StrCondition = "Where (TBL.V_Date Between " & Agl.ConvertDate(TxtFromDate.Text) & " And " & Agl.ConvertDate(TxtToDate.Text) & ") "
        If TxtVoucherType.Tag <> "" Then
            StrCondition += " And TBL.V_Type In (" & TxtVoucherType.Tag & ") "
        Else
            StrCondition += " And TBL.V_Type In (Select V_Type From Voucher_Type Where IsNull(AuditAllowed,'N')='Y') "
        End If


        If UCase(Trim(TxtFindIn.Tag)) = "L" Then
            If TxtAccountName.Tag <> "" Then StrCondition += " And TBL.SubCode In (" & TxtAccountName.Tag & ") "
            If TxtChequeNo.Tag <> "" Then StrCondition += " And TBL.Chq_No In (" & TxtChequeNo.Tag & ") "
            If Trim(TxtNarration.Text) <> "" Then StrCondition += " And TBL.Narration Like '%" & TxtNarration.Text & "%' "

            If TxtFromAmount.Text <> "" Then
                StrCondition += " And ( "

                If UCase(Trim(TxtType.Tag)) = "D" Or UCase(Trim(TxtType.Tag)) = "B" Then
                    StrCondition += " ( "
                    If TxtFromAmount.Text <> "" Then StrCondition += " TBL.AmtDr >= " & Val(TxtFromAmount.Text) & " "
                    If TxtToAmount.Text <> "" Then StrCondition += " And TBL.AmtDr <= " & Val(TxtToAmount.Text) & " "
                    StrCondition += " ) "

                    If UCase(Trim(TxtType.Tag)) = "C" Or UCase(Trim(TxtType.Tag)) = "B" Then StrCondition += " Or "
                End If

                If UCase(Trim(TxtType.Tag)) = "C" Or UCase(Trim(TxtType.Tag)) = "B" Then
                    StrCondition += " ( "
                    If TxtFromAmount.Text <> "" Then StrCondition += " TBL.AmtCr >= " & Val(TxtFromAmount.Text) & " "
                    If TxtToAmount.Text <> "" Then StrCondition += " And TBL.AmtCr <= " & Val(TxtToAmount.Text) & " "
                    StrCondition += " ) "
                End If
                StrCondition += " ) "
            End If
        Else
            If TxtAccountName.Tag <> "" Then StrCondition += " And TBL.PartyCode In (" & TxtAccountName.Tag & ") "
            If TxtItemName.Tag <> "" Then StrCondition += " And TBL.ItemCode In (" & TxtItemName.Tag & ") "
            If Trim(TxtNarration.Text) <> "" Then StrCondition += " And TBL.Remark Like '%" & TxtNarration.Text & "%' "

            If TxtFromAmount.Text <> "" Then
                StrCondition += " And ( "

                If UCase(Trim(TxtType.Tag)) = "R" Or UCase(Trim(TxtType.Tag)) = "B" Then
                    StrCondition += " ( "
                    If TxtFromAmount.Text <> "" Then StrCondition += " TBL.RecQty >= " & Val(TxtFromAmount.Text) & " "
                    If TxtToAmount.Text <> "" Then StrCondition += " And TBL.RecQty <= " & Val(TxtToAmount.Text) & " "
                    StrCondition += " ) "
                    If UCase(Trim(TxtType.Tag)) = "I" Or UCase(Trim(TxtType.Tag)) = "B" Then StrCondition += " Or "
                End If

                If UCase(Trim(TxtType.Tag)) = "I" Or UCase(Trim(TxtType.Tag)) = "B" Then
                    StrCondition += " ( "
                    If TxtFromAmount.Text <> "" Then StrCondition += " TBL.IssueQty >= " & Val(TxtFromAmount.Text) & " "
                    If TxtToAmount.Text <> "" Then StrCondition += " And TBL.IssueQty <= " & Val(TxtToAmount.Text) & " "
                    StrCondition += " ) "
                End If

                StrCondition += " ) "
            End If
        End If

        If UCase(Trim(TxtFindIn.Tag)) = "L" Then
            StrSQL = "Select TBL.DocId As SearchCode,TBL.V_Type,TBL.RecId,TBL.V_Date,SG.Name As Party, "
            StrSQL += "TBL.AmtDr As Incoming,TBL.AmtCr As Outgoing,'' As IName,'' As Unit, "
            StrSQL += "TBL.Narration As Remark,TBL.Chq_No "
            StrSQL += "From Ledger_Temp TBL Left Join "
            StrSQL += "DataAudit DA On TBL.DocId=DA.DocId Left Join "
            StrSQL += "SubGroup SG On TBL.SubCode=SG.SubCode "
            StrSQL += StrCondition
            StrSQL += "Order By TBL.V_Date,TBL.RecId,TBL.V_Type "
        Else
            StrSQL = "Select TBL.DocId As SearchCode,TBL.V_Type,TBL.RecId,TBL.V_Date,SG.Name As Party, "
            StrSQL += "TBL.RecQty As Incoming,TBL.IssueQty As Outgoing,IM.Description As IName,IM.Unit,TBL.Remark,Null As Chq_No "
            StrSQL += "From Stock TBL Left Join "
            StrSQL += "DataAudit DA On TBL.DocId=DA.DocId Left Join "
            StrSQL += "SubGroup SG On TBL.PartyCode=SG.SubCode Left Join "
            StrSQL += "Item IM On IM.Code=TBL.ItemCode "
            StrSQL += StrCondition
            StrSQL += "Order By TBL.V_Date,TBL.RecId,TBL.V_Type "
        End If
        FrmPB.FMoveBar()
        DTTemp = cmain.FGetDatTable(StrSQL, Agl.Gcn)
        FrmPB.FMoveBar()
        If DTTemp.Rows.Count > 0 Then
            FGMain.Rows.Add(DTTemp.Rows.Count)
        End If
        For I = 0 To DTTemp.Rows.Count - 1
            If StrCompare <> Agl.Xnull(DTTemp.Rows(I).Item("SearchCode")) Then
                If Color_Main = Color_B Then
                    Color_Main = Color_A
                Else
                    Color_Main = Color_B
                End If
            End If
            If UCase(Trim(TxtAccountName.Tag)) = "A" Then
                FGMain(GSNo, I).Value = "þ"
            Else
                FGMain(GSNo, I).Value = "o"
            End If
            FGMain(GSNo, I).Value = Trim(I + 1)
            FGMain(GSearchCode, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("SearchCode"))
            FGMain(GV_Type, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("V_Type"))
            FGMain(GV_Date, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("V_Date"))
            FGMain(GRecId, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("RecId"))
            FGMain(GAccountName, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("Party"))
            FGMain(GItemName, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("IName"))
            FGMain(GUnit, I).Value = AgL.XNull(DTTemp.Rows(I).Item("Unit"))
            FGMain(GIngoming, I).Value = Format(Agl.VNull(DTTemp.Rows(I).Item("Incoming")), "0.000")
            FGMain(GOutgoing, I).Value = Format(Agl.VNull(DTTemp.Rows(I).Item("Outgoing")), "0.000")
            FGMain(GRemarks, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("Remark"))
            FGMain(GChequeNo, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("Chq_No"))

            If Val(FGMain(GIngoming, I).Value) = 0 Then FGMain(GIngoming, I).Value = ""
            If Val(FGMain(GOutgoing, I).Value) = 0 Then FGMain(GOutgoing, I).Value = ""
            FGMain.Rows(I).DefaultCellStyle.BackColor = Color_Main
            StrCompare = Agl.Xnull(DTTemp.Rows(I).Item("SearchCode"))
            FrmPB.FMoveBar()
        Next
        DTTemp.Clear()
        DTTemp = Nothing
        FrmPB.Dispose()
    End Sub
    Public Sub FTxtKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.Name
            Case TxtFindIn.Name, TxtAccountName.Name, TxtVoucherType.Name, TxtItemName.Name, _
                TxtType.Name, TxtChequeNo.Name
                If e.KeyCode = Keys.Delete Then
                    sender.Text = "" : sender.Tag = ""
                End If
        End Select
    End Sub

    Public Sub FTxtKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.Name
            Case TxtItemName.Name
                FHP_Item(e, sender)
            Case TxtAccountName.Name
                FHP_Accounts(e, sender)
            Case TxtFindIn.Name
                FHP_FindIn(e, sender)
            Case TxtVoucherType.Name
                FHP_VoucherType(e, sender)
            Case TxtType.Name
                FHP_Type(e, sender)
            Case TxtChequeNo.Name
                FHP_ChequeNo(e, sender)
            Case TxtFromAmount.Name, TxtToAmount.Name
                CMain.NumPress(sender, e, 10, 3, False)
        End Select
    End Sub
    Public Sub FTxtGotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        '======== Write Your Code Below =============
    End Sub
    Private Sub FHP_VoucherType(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSendText As String, StrPrvText As String
        Dim StrSQL As String

        StrPrvText = Txt.Text
        StrSendText = Cmain.FSendText(Txt, e.KeyChar)

        StrSQL = "Select 'o' As Tick,V_Type As Code,V_Type As Type,Description,Category From Voucher_Type Where IsNull(AuditAllowed,'N')='Y' Order By Category,V_Type"

        DTMain = cmain.FGetDatTable(StrSQL, Agl.Gcn)
        FRH = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DTMain), StrSendText, 350, 430, (Top + Txt.Top) + 85, Left + Txt.Left + 3, False)

        FRH.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(1, , 0, , False)
        FRH.FFormatColumn(2, "Type", 80, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(3, "Description", 150, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(4, "Category", 80, DataGridViewContentAlignment.MiddleLeft)

        FRH.ShowDialog()
        Txt.Text = StrPrvText
        If FRH.BytBtnValue = 0 Then
            Txt.Text = FRH.FFetchData(2, "", "", ",")
            Txt.Tag = FRH.FFetchData(1, "'", "'", ",", True)
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub FHP_ChequeNo(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSendText As String, StrPrvText As String
        Dim StrSQL As String

        StrPrvText = Txt.Text
        StrSendText = Cmain.FSendText(Txt, e.KeyChar)

        StrSQL = "Select 'o' As Tick,IsNull(LG.Chq_No,'') As Code,IsNull(LG.Chq_No,'') As Chq_No From Ledger_Temp LG Where IsNull(LG.Chq_No,'')<>'' Group By LG.Chq_No Order By LG.Chq_No"

        DTMain = cmain.FGetDatTable(StrSQL, Agl.Gcn)
        FRH = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DTMain), StrSendText, 350, 220, (Top + Txt.Top) + 85, Left + Txt.Left + 3, False)

        FRH.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(1, , 0, , False)
        FRH.FFormatColumn(2, "Cheque No.", 100, DataGridViewContentAlignment.MiddleLeft)

        FRH.ShowDialog()
        Txt.Text = StrPrvText
        If FRH.BytBtnValue = 0 Then
            Txt.Text = FRH.FFetchData(2, "", "", ",")
            Txt.Tag = FRH.FFetchData(1, "'", "'", ",", True)
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub FHP_Accounts(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSendText As String, StrPrvText As String
        Dim StrSQL As String

        StrPrvText = Txt.Text
        StrSendText = Cmain.FSendText(Txt, e.KeyChar)

        StrSQL = "Select 'o' As Tick,SG.SubCode,SG.Name,SG.ManualCode,IsNull(CT.CityName,'') "
        StrSQL += "From SubGroup SG Left Join City CT On CT.CityCode=SG.CityCode "
        StrSQL += "Order by SG.Name"

        DTMain = cmain.FGetDatTable(StrSQL, Agl.Gcn)
        FRH = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DTMain), StrSendText, 350, 520, (Top + Txt.Top) + 85, Left + Txt.Left - 100, False)

        FRH.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(1, , 0, , False)
        FRH.FFormatColumn(2, "Account Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(4, "City", 100, DataGridViewContentAlignment.MiddleLeft)

        FRH.ShowDialog()
        Txt.Text = StrPrvText
        If FRH.BytBtnValue = 0 Then
            Txt.Text = FRH.FFetchData(2, "", "", ",")
            Txt.Tag = FRH.FFetchData(1, "'", "'", ",", True)
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub FHP_Item(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSendText As String, StrPrvText As String
        Dim StrSQL As String

        StrPrvText = Txt.Text
        StrSendText = Cmain.FSendText(Txt, e.KeyChar)

        StrSQL = "Select 'o' As Tick,I.Code As SearchCode,I.Description as Name,I.ManualCode As Code,I.Unit,I.ItemGroup As IGName "
        StrSQL += "From Item I "        
        StrSQL += "Order By I.Name"

        DTMain = cmain.FGetDatTable(StrSQL, Agl.Gcn)
        FRH = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DTMain), StrSendText, 350, 630, (Top + Txt.Top) + 85, Left + Txt.Left - 200, False)

        FRH.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(1, , 0, , False)
        FRH.FFormatColumn(2, "Item Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(4, "Unit", 60, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(5, "Item Group", 150, DataGridViewContentAlignment.MiddleLeft)

        FRH.ShowDialog()
        Txt.Text = StrPrvText
        If FRH.BytBtnValue = 0 Then
            Txt.Text = FRH.FFetchData(2, "", "", ",")
            Txt.Tag = FRH.FFetchData(1, "'", "'", ",", True)
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub FHP_FindIn(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String, StrPrvText As String
        Dim StrSQL As String

        StrPrvText = Txt.Text
        StrSendText = Cmain.FSendText(Txt, e.KeyChar)
        StrSQL = "Declare @TempTable Table (Code NVarChar(1),Name NVarChar(10)) "
        StrSQL += "Insert Into @TempTable Values ('L','Ledger') "
        StrSQL += "Insert Into @TempTable Values ('S','Stock') "
        StrSQL += "Select Code,Name From @TempTable Order By Name "

        DTMain = cmain.FGetDatTable(StrSQL, Agl.Gcn)
        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 150, 180, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.ShowDialog()
        Txt.Text = StrPrvText
        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(1)
                Txt.Tag = FRH.DRReturn.Item(0)

                TxtType.Text = "Both"
                TxtType.Tag = "B"
                FSetGrid()
            End If
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub FHP_Type(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String, StrPrvText As String
        Dim StrSQL As String

        StrPrvText = Txt.Text
        StrSendText = Cmain.FSendText(Txt, e.KeyChar)
        If UCase(Trim(TxtFindIn.Tag)) = "L" Then
            StrSQL = "Declare @TempTable Table (Code NVarChar(1),Name NVarChar(10)) "
            StrSQL += "Insert Into @TempTable Values ('D','Debit') "
            StrSQL += "Insert Into @TempTable Values ('C','Credit') "
            StrSQL += "Insert Into @TempTable Values ('B','Both') "
            StrSQL += "Select Code,Name From @TempTable Order By Name "
        Else
            StrSQL = "Declare @TempTable Table (Code NVarChar(1),Name NVarChar(10)) "
            StrSQL += "Insert Into @TempTable Values ('I','Issue') "
            StrSQL += "Insert Into @TempTable Values ('R','Receive') "
            StrSQL += "Insert Into @TempTable Values ('B','Both') "
            StrSQL += "Select Code,Name From @TempTable Order By Name "
        End If
        DTMain = cmain.FGetDatTable(StrSQL, Agl.Gcn)
        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 150, 180, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.ShowDialog()
        Txt.Text = StrPrvText
        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(1)
                Txt.Tag = FRH.DRReturn.Item(0)
            End If
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub BtnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnShow.Click
        If AgL.RequiredField(TxtFindIn, "Find In") Then Exit Sub
        If AgL.RequiredField(TxtType, "Type") Then Exit Sub
        If AgL.RequiredField(TxtFromDate, "From Date") Then Exit Sub
        If AgL.RequiredField(TxtToDate, "To Date") Then Exit Sub
        If Val(TxtToAmount.Text) > 0 Then If Not Val(TxtFromAmount.Text) > 0 Then MsgBox("If To Amt/Qty Is Filled Then From Amt/Qty Is Mandatory.") : Exit Sub

        FFillGrid()
    End Sub
    Private Sub FrmSmartFinder_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Agl.FPaintForm(Me, e, 0)
    End Sub
    Private Sub TxtFromDate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtFromDate.Validated, TxtToDate.Validated
        Select Case sender.Name
            Case TxtFromDate.Name, TxtToDate.Name
                sender.Text = Agl.RetDate(sender.Text)
        End Select
    End Sub
    Private Sub FGMain_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles FGMain.KeyPress
        If e.KeyChar = Chr(Keys.Enter) Then
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
            DTRow = DTVType.Select("V_Type='" & Trim(FGMain(GV_Type, IntRowIndex).Value) & "'")
            If DTRow.Length > 0 Then
                StrModuleName = Agl.Xnull(DTRow(0).Item("MnuAttachedInModule"))
                StrMnuName = Agl.Xnull(DTRow(0).Item("MnuName"))
                StrMnuText = Agl.Xnull(DTRow(0).Item("MnuText"))

                FrmObjMDI = Me.MdiParent
                FrmObj = FrmObjMDI.FOpenForm(StrModuleName, StrMnuName, StrMnuText)
                FrmObj.FSearchRecord(Trim(FGMain(GSearchCode, IntRowIndex).Value))
            Else
                MsgBox("Define Details For This Voucher Type.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class