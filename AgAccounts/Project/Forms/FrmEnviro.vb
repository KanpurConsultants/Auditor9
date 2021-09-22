Imports System.Data.SQLite
Public Class FrmEnviro
    Private LIEvent As ClsEvents
    Private Sub FrmEnviro_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LIEvent = New ClsEvents(Me)
        AgL.WinSetting(Me, 518, 788, 0, 0)
        MoveRec()
    End Sub
    Private Sub FrmEnviro_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub
    Public Sub FTxtGotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        '======== Write Your Code Below =============
    End Sub
    Public Sub FTxtKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.Name
            Case TxtMaintainTDS.Name, TxtAutoPosting.Name, TxtNumberingSystem.Name, TxtTDSROff.Name,
                TxtArrange.Name, TxtSrvTaxAdjRefType.Name
                If e.KeyCode = Keys.Delete Then
                    sender.Text = "" : sender.Tag = ""
                End If
        End Select
    End Sub
    Public Sub FTxtKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.Name
            Case TxtMaintainTDS.Name, TxtAutoPosting.Name, TxtTDSROff.Name
                FHP_YesNO(e, sender)
            Case TxtNumberingSystem.Name
                FHP_NSystem(e, sender)
            Case TxtArrange.Name
                FHP_ArrangeBy(e, sender)
            Case TxtSrvTaxAdjRefType.Name
                FHP_VoucherType(e, sender)
        End Select
    End Sub
    Private Sub FHP_VoucherType(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSendText As String, StrPrvText As String

        StrPrvText = Txt.Text
        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        DTMain = CMain.FGetDatTable("Select 'o' As Tick,VT.V_Type,VT.Description From Voucher_Type VT Order By VT.V_Type", AgL.GCn)
        FRH = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DTMain), StrSendText, 300, 420, (Top + Txt.Top) + 85, Left + Txt.Left + 3, False)
        FRH.FFormatColumn(0, "Tick", 42, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(1, "Type", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(2, "Description", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.StartPosition = FormStartPosition.CenterScreen
        FRH.ShowDialog()
        Txt.Text = StrPrvText
        If FRH.BytBtnValue = 0 Then
            Txt.Text = FRH.FFetchData(1, "", "", ",", False)
            Txt.Tag = FRH.FFetchData(1, "|", "|", ",", True)
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub FHP_ArrangeBy(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String
        Dim StrSQL As String

        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        StrSQL = "Declare @TmpTable Table(Code NVarChar(1),Name NVarChar(15)) "
        StrSQL += "Insert Into @TmpTable Values('A','ALL') "
        StrSQL += "Insert Into @TmpTable Values('B','Between Dates:') "
        StrSQL += "Select Code,Name From @TmpTable Order By Name "

        DTMain = CMain.FGetDatTable(StrSQL, AgL.GCn)
        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 200, 180, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 120, DataGridViewContentAlignment.MiddleLeft)
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(1)
                Txt.Tag = FRH.DRReturn.Item(0)

                If TxtArrange.Text = "ALL" Then
                    txtFrom.Text = AgL.PubStartDate : TxtTo.Text = AgL.PubEndDate
                    txtFrom.Enabled = False : TxtTo.Enabled = False
                Else
                    txtFrom.Enabled = True : TxtTo.Enabled = True
                End If

            End If
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub FHP_NSystem(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String, StrPrvText As String


        StrPrvText = Txt.Text
        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        DTMain = AgL.FillData("DECLARE @TMPTABLE TABLE (Code NVARCHAR(1),Name NVARCHAR (10)) " &
                "INSERT INTO @TMPTABLE VALUES ('D','Daily') " &
                "INSERT INTO @TMPTABLE VALUES ('M','Monthly') " &
                "INSERT INTO @TMPTABLE VALUES ('Y','Yearly') " &
                "SELECT Code,Name FROM @TMPTABLE", AgL.GCn).Tables(0)

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
    Private Sub FHP_YesNO(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String, StrPrvText As String


        StrPrvText = Txt.Text
        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        DTMain = AgL.FillData("DECLARE @TMPTABLE TABLE (Code NVARCHAR(1),Name NVARCHAR (3)) " &
                "INSERT INTO @TMPTABLE VALUES ('Y','Yes') " &
                "INSERT INTO @TMPTABLE VALUES ('N','No') " &
                "SELECT Code,Name FROM @TMPTABLE", AgL.GCn).Tables(0)

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
    Private Sub MoveRec()
        Dim DTTemp As New DataTable
        Dim DTTemp1 As DataTable = Nothing
        Dim I As Integer

        DTTemp = CMain.FGetDatTable("Select * From Enviro_Accounts", AgL.GCn)
        If DTTemp.Rows.Count > 0 Then

            '====================================================================    
            '======================= Voucher Entry =========================
            '====================================================================

            '===== Discount
            TxtMaintainTDS.Tag = AgL.XNull(DTTemp.Rows(0).Item("MaintainTDS"))
            TxtMaintainTDS.Text = IIf(UCase(AgL.XNull(DTTemp.Rows(0).Item("MaintainTDS"))) = "Y", "Yes", "No")

            TxtAutoPosting.Tag = AgL.XNull(DTTemp.Rows(0).Item("AutoPosting"))
            TxtAutoPosting.Text = IIf(UCase(AgL.XNull(DTTemp.Rows(0).Item("AutoPosting"))) = "Y", "Yes", "No")

            TxtNumberingSystem.Tag = AgL.XNull(DTTemp.Rows(0).Item("VRNumberSystem"))
            TxtNumberingSystem.Text = IIf(UCase(AgL.XNull(DTTemp.Rows(0).Item("VRNumberSystem"))) = "D", "Daily",
                                      IIf(UCase(AgL.XNull(DTTemp.Rows(0).Item("VRNumberSystem"))) = "M", "Monthly",
                                      IIf(UCase(AgL.XNull(DTTemp.Rows(0).Item("VRNumberSystem"))) = "Y", "Yearly", "")))

            TxtTDSROff.Tag = AgL.XNull(DTTemp.Rows(0).Item("TDSROff"))
            TxtTDSROff.Text = IIf(UCase(AgL.XNull(DTTemp.Rows(0).Item("TDSROff"))) = "Y", "Yes", "No")

            'TxtMaintainTDS.Text = FFetchValue(Of String)("Select SG.Name From Subgroup SG Where SG.SubCode='" & Agl.Xnull(DTTemp.Rows(0).Item("mmDiscAc")) & "'")

            TxtSrvTaxAdjRefType.Text = ""
            TxtSrvTaxAdjRefType.Tag = AgL.XNull(DTTemp.Rows(0).Item("SrvTaxAdjRefType"))
            If Trim(TxtSrvTaxAdjRefType.Tag) <> "" Then
                DTTemp1 = CMain.FGetDatTable("Select VT.V_Type From Voucher_Type VT Where VT.V_Type In (" & Replace(TxtSrvTaxAdjRefType.Tag, "|", "'") & ")", AgL.GCn)
                For I = 0 To DTTemp1.Rows.Count - 1
                    If Trim(TxtSrvTaxAdjRefType.Text) <> "" Then TxtSrvTaxAdjRefType.Text = TxtSrvTaxAdjRefType.Text & ","
                    TxtSrvTaxAdjRefType.Text = TxtSrvTaxAdjRefType.Text & AgL.XNull(DTTemp1.Rows(I).Item("V_Type"))
                Next
                DTTemp1.Dispose()
            End If


            '====================================================================    
            '======================= Voucher Entry End =========================
            '====================================================================
        End If
        DTTemp.Dispose()
    End Sub
    Private Function FFetchValue(Of DTRtn)(ByVal StrSQLQuery As String) As DTRtn
        Dim DTTemp As New DataTable
        Dim GrcRtn As DTRtn

        DTTemp = AgL.FillData(StrSQLQuery, AgL.GCn).Tables(0)
        If DTTemp.Rows.Count > 0 Then
            GrcRtn = DTTemp.Rows(0).Item(0)
        End If
        DTTemp.Dispose()
        Return GrcRtn
    End Function
    Private Sub FSave()
        Dim GCnCmd As New Object
        Dim DTTemp As New DataTable

        If AgL.RequiredField(TxtMaintainTDS, "Maintain TDS") Then Exit Sub
        If AgL.RequiredField(TxtAutoPosting, "Auto Posting") Then Exit Sub
        If AgL.RequiredField(TxtNumberingSystem, "Numbering System") Then Exit Sub
        If AgL.RequiredField(TxtTDSROff, "TDS Round Off") Then Exit Sub
        If AgL.RequiredField(TxtSrvTaxAdjRefType, "Adj .Ref. Voucher Type (For Service)") Then Exit Sub

        If MsgBox(ClsMain.MsgSaveCnf) = MsgBoxResult.No Then Exit Sub

        GCnCmd = AgL.GCn.CreateCommand
        DTTemp = AgL.FillData("Select ID From Enviro_Accounts", AgL.GCn).Tables(0)
        If Not DTTemp.Rows.Count > 0 Then
            GCnCmd.CommandText = "Insert Into Enviro_Accounts(ID) Values('1')"
            GCnCmd.ExecuteNonQuery()
        End If

        GCnCmd.CommandText = "Update Enviro_Accounts Set "
        GCnCmd.CommandText += "Transfered='N', "

        '====================================================================    
        '======================= Voucher Entry Module Began =================
        '====================================================================
        GCnCmd.CommandText += "MaintainTDS='" & TxtMaintainTDS.Tag & "', "
        GCnCmd.CommandText += "AutoPosting='" & TxtAutoPosting.Tag & "', "
        GCnCmd.CommandText += "TDSROff='" & TxtTDSROff.Tag & "', "
        GCnCmd.CommandText += "SrvTaxAdjRefType='" & TxtSrvTaxAdjRefType.Tag & "', "
        GCnCmd.CommandText += "VRNumberSystem='" & TxtNumberingSystem.Tag & "' "
        '====================================================================    
        '======================= Voucher Entry Module End =========================
        '====================================================================

        GCnCmd.ExecuteNonQuery()

        Me.Close()
    End Sub
    Private Sub FArrangeVoucher()
        Dim DTTemp As DataTable
        Dim StrNumberSystem As String
        Dim DTTempVT As DataTable
        Dim GCnCmd As New Object
        Dim StrOrignalValue As String = ""
        Dim StrValue As String = ""
        Dim StrData As String = ""
        Dim I As Integer
        Dim X As Integer
        Dim IntSNo As Long
        Dim StrPrefix As String
        Dim BlnTrans As Boolean = False
        Dim StrQry As String = "", StrCondition As String = ""

        If AgL.RequiredField(TxtArrange, "Arrange") Then Exit Sub

        If Trim(UCase(TxtArrange.Tag)) = "B" Then
            If AgL.RequiredField(txtFrom, "From") Then Exit Sub
            If AgL.RequiredField(TxtTo, "To") Then Exit Sub
            StrCondition = " And (V_Date Between " & AgL.Chk_Text(CDate(txtFrom.Text).ToString("s")) & " And " & AgL.Chk_Text(CDate(TxtTo.Text).ToString("s")) & ") "
        End If
        If MsgBox("Are You Sure.You Want To Serialize Voucher.") = MsgBoxResult.No Then Exit Sub

        Try
            StrNumberSystem = Trim(AgL.XNull(CMain.FGetDatTable("Select  VRNumberSystem " &
                              "From Enviro_Accounts  ", AgL.GCn).Rows(0).Item("VRNumberSystem")))
            If StrNumberSystem = "" Then MsgBox("Number System Is Not Defined.") : Exit Sub

            DTTempVT = CMain.FGetDatTable("SELECT V_Type FROM Voucher_Type " &
                      "WHERE Category IN ('JV','RCT','PMT')  ", AgL.GCn)
            For X = 0 To DTTempVT.Rows.Count - 1
                IntSNo = 0
                StrPrefix = ""
                StrQry = "Select DocId,V_Date,Day(V_Date) As DD, "
                StrQry += "Month(V_Date) As MM,Year(V_Date) As YY "
                StrQry += "From LedgerM_Temp "
                StrQry += "Where V_Type In ('" & AgL.XNull(DTTempVT.Rows(X).Item("V_Type")) & "') "
                StrQry += "And V_Prefix='" & AgL.PubCompVPrefix & "' "
                StrQry += StrCondition
                StrQry += "Order By V_Date,Cast(RecId As Int) "

                DTTemp = CMain.FGetDatTable(StrQry, AgL.GCn)
                GCnCmd = AgL.GCn.CreateCommand
                GCnCmd.Transaction = AgL.GCn.BeginTransaction(IsolationLevel.Serializable)
                BlnTrans = True
                For I = 0 To DTTemp.Rows.Count - 1
                    Select Case UCase(StrNumberSystem)
                        Case "D"
                            StrOrignalValue = Format(DTTemp.Rows(I).Item("V_Date"), "dd/MMM/yyyy")
                            StrPrefix = Trim(Format(AgL.VNull(DTTemp.Rows(I).Item("DD")), "00"))
                            StrPrefix += Trim(Format(AgL.VNull(DTTemp.Rows(I).Item("MM")), "00"))
                        Case "M"
                            StrOrignalValue = Format(DTTemp.Rows(I).Item("V_Date"), "MMM/yyyy")
                            StrPrefix = Trim(Format(AgL.VNull(DTTemp.Rows(I).Item("MM")), "00"))
                    End Select

                    If StrValue <> StrOrignalValue Then IntSNo = 0
                    StrValue = StrOrignalValue

                    IntSNo += 1

                    Select Case UCase(StrNumberSystem)
                        Case "D"
                            StrData = StrPrefix + Format(IntSNo, "0000")
                        Case "M"
                            StrData = StrPrefix + Format(IntSNo, "000000")
                        Case Else
                            StrData = IntSNo
                    End Select

                    GCnCmd.CommandText = "Update Stock Set RecId='" & StrData & "' Where DocId='" & AgL.XNull(DTTemp.Rows(I).Item("DocId")) & "'"
                    GCnCmd.ExecuteNonQuery()
                    GCnCmd.CommandText = "Update LedgerM_Temp Set RecId='" & StrData & "' Where DocId='" & AgL.XNull(DTTemp.Rows(I).Item("DocId")) & "'"
                    GCnCmd.ExecuteNonQuery()
                    GCnCmd.CommandText = "Update Ledger_Temp Set RecId='" & StrData & "' Where DocId='" & AgL.XNull(DTTemp.Rows(I).Item("DocId")) & "'"
                    GCnCmd.ExecuteNonQuery()
                    GCnCmd.CommandText = "Update LedgerM Set RecId='" & StrData & "' Where DocId='" & AgL.XNull(DTTemp.Rows(I).Item("DocId")) & "'"
                    GCnCmd.ExecuteNonQuery()
                    GCnCmd.CommandText = "Update Ledger Set RecId='" & StrData & "' Where DocId='" & AgL.XNull(DTTemp.Rows(I).Item("DocId")) & "'"
                    GCnCmd.ExecuteNonQuery()

                Next
                GCnCmd.Transaction.Commit()
                BlnTrans = False
                DTTemp.Dispose()
                DTTemp = Nothing
            Next
            MsgBox("Process Completed Successfully.")
        Catch ex As Exception
            If BlnTrans = True Then GCnCmd.Transaction.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
    Handles BtnSave.Click, BtnClose.Click, BtnVoucherEnt.Click

        Select Case sender.name
            Case BtnSave.Name
                FSave()
            Case BtnClose.Name
                Me.Close()
            Case BtnVoucherEnt.Name
                FArrangeVoucher()
        End Select

    End Sub
    Private Sub TxtArrange_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtArrange.Validated, txtFrom.Validated, TxtTo.Validated
        Select Case sender.name
            Case txtFrom.Name, TxtTo.Name
                sender.text = AgL.RetDate(sender.text)
        End Select
    End Sub
    Private Function FChkDate() As Boolean
        Dim BlnRtn As Boolean
        BlnRtn = True
        If TxtArrange.Text = "ALL" Then FChkDate = True : Exit Function
        If BlnRtn = True Then If AgL.RequiredField(txtFrom, "From Date:") = False Then BlnRtn = False
        If BlnRtn = True Then If AgL.RequiredField(TxtTo, "To Date:") = False Then BlnRtn = False
        If BlnRtn = True Then If CDate(TxtTo.Text) < CDate(txtFrom.Text) Then MsgBox("From Date Should Be less than To Date.") : txtFrom.Focus() : BlnRtn = False
        FChkDate = BlnRtn
    End Function
End Class
