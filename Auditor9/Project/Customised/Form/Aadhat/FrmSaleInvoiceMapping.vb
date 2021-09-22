Imports System.ComponentModel
Public Class FrmSaleInvoiceMapping
    Dim mQry As String = ""
    Private Function FDataValidation() As Boolean
        mQry = "Select Count(*) As Cnt
                From SaleInvoice H 
                Where ManualRefNo = '" & TxtKachhaSaleInvoiceNo.Text & "' 
                And V_Type = 'WSI'
                And Site_Code = '" & AgL.PubSiteCode & "'
                And Div_Code = '" & AgL.PubDivCode & "'"
        If CDate(TxtKachhaSaleInvoiceDate.Text) >= "01/Apr/2018" And
            CDate(TxtKachhaSaleInvoiceDate.Text) <= "31/Mar/2019" Then
            mQry += " And V_Prefix = '2018'"
        ElseIf CDate(TxtKachhaSaleInvoiceDate.Text) >= "01/Apr/2019" And
            CDate(TxtKachhaSaleInvoiceDate.Text) <= "31/Mar/2020" Then
            mQry += " And V_Prefix = '2019'"
        End If

        If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar()) > 0 Then
            MsgBox("Kachha Invoice number already exists...!", MsgBoxStyle.Information)
            FDataValidation = False
            TxtKachhaSaleInvoiceNo.Focus()
            Exit Function
        End If

        If AgL.XNull(TxtKachhaSaleInvoiceNo.Tag) <> "" Then
            MsgBox("Pakka Invoice no is already mapped...!", MsgBoxStyle.Information)
            FDataValidation = False
            TxtKachhaSaleInvoiceNo.Focus()
            Exit Function
        End If

        FDataValidation = True
    End Function
    Private Sub BlankText()
        TxtPakkaSaleInvoiceNo.Tag = "" : TxtPakkaSaleInvoiceNo.Text = ""
        TxtKachhaSaleInvoiceNo.Tag = "" : TxtKachhaSaleInvoiceNo.Text = ""
        TxtKachhaSaleInvoiceDate.Tag = "" : TxtKachhaSaleInvoiceDate.Text = ""
        TxtLrNo.Tag = "" : TxtLrNo.Text = ""
        TxtLrDate.Tag = "" : TxtLrDate.Text = ""
        TxtNoOfBales.Tag = "" : TxtNoOfBales.Text = ""
        TxtCode.Tag = "" : TxtCode.Text = ""
    End Sub
    Public Sub FPostSaleData_ForDifference(Conn As Object, Cmd As Object)
        Dim ErrorLog As String = ""
        Dim DtMain As DataTable = Nothing
        Dim dtTemp As DataTable = Nothing
        Dim I As Integer
        Dim StrErrLog As String = ""
        Dim mRow As Integer = 0


        Dim Tot_Gross_Amount As Double = 0
        Dim Tot_Taxable_Amount As Double = 0
        Dim Tot_Tax1 As Double = 0
        Dim Tot_Tax2 As Double = 0
        Dim Tot_Tax3 As Double = 0
        Dim Tot_Tax4 As Double = 0
        Dim Tot_Tax5 As Double = 0
        Dim Tot_SubTotal1 As Double = 0


        Tot_Gross_Amount = 0
        Tot_Taxable_Amount = 0
        Tot_Tax1 = 0
        Tot_Tax2 = 0
        Tot_Tax3 = 0
        Tot_Tax4 = 0
        Tot_Tax5 = 0
        Tot_SubTotal1 = 0

        mQry = " Select H.*, VLine.TotalQty, VLine.SaleOrderNo, VLine.ItemGroup,
                VLine.SaleOrder
                From SaleInvoice H 
                LEFT JOIN (
                    Select L.DocId, Sum(L.Qty) As TotalQty, Max(So.ManualRefNo) As SaleOrderNo, 
                    L.SaleInvoice As SaleOrder, Ig.Code As ItemGroup
                    From SaleInvoiceDetail L 
                    LEFT JOIN SaleOrder So On L.SaleInvoice = So.DocId
                    LEFT JOIN Item I ON L.Item = I.Code
                    LEFT JOIN ItemGroup Ig ON I.ItemGroup = Ig.Code
                    Group By L.DocID) As VLine On H.DocId = VLine.DocId
                Where H.DocId = '" & TxtPakkaSaleInvoiceNo.Tag & "'"
        Dim DtPakkaSaleInvoice As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim SaleInvoiceTableList(0) As FrmSaleInvoiceDirect_WithDimension_ShyamaShyam.StructSaleInvoice

        'In One Transaction only one Sale Invoice Can be Generated.The First Sale Invoice No will be 
        'Considered As Sale Invoice No
        If AgL.XNull(TxtKachhaSaleInvoiceNo.Text) <> "" Then
            'If AgL.VNull(Dgl2.Item(Col2WSaleInvoiceAmount, mRow).Value) <> 0 Then
            SaleInvoiceTableList(0).DocID = ""
            SaleInvoiceTableList(0).V_Type = "WSI"
            SaleInvoiceTableList(0).V_Prefix = ""
            SaleInvoiceTableList(0).Site_Code = AgL.PubSiteCode
            SaleInvoiceTableList(0).Div_Code = AgL.PubDivCode
            SaleInvoiceTableList(0).V_No = 0
            SaleInvoiceTableList(0).V_Date = TxtKachhaSaleInvoiceDate.Text
            'SaleInvoiceTableList(0).V_Date = Dgl2.Item(Col2InvoiceDate, mRow).Value
            SaleInvoiceTableList(0).ManualRefNo = TxtKachhaSaleInvoiceNo.Text
            SaleInvoiceTableList(0).SaleToParty = AgL.XNull(DtPakkaSaleInvoice.Rows(0)("SaleToParty"))
            SaleInvoiceTableList(0).SaleToPartyName = AgL.XNull(DtPakkaSaleInvoice.Rows(0)("SaleToPartyName"))
            SaleInvoiceTableList(0).AgentCode = ""
            SaleInvoiceTableList(0).AgentName = ""
            SaleInvoiceTableList(0).BillToPartyCode = AgL.XNull(DtPakkaSaleInvoice.Rows(0)("BillToParty"))
            SaleInvoiceTableList(0).BillToPartyName = ""
            SaleInvoiceTableList(0).SaleToPartyAddress = ""
            SaleInvoiceTableList(0).SaleToPartyCityCode = ""
            SaleInvoiceTableList(0).SaleToPartyMobile = ""
            SaleInvoiceTableList(0).SaleToPartySalesTaxNo = ""
            SaleInvoiceTableList(0).ShipToAddress = ""
            SaleInvoiceTableList(0).SalesTaxGroupParty = ""
            SaleInvoiceTableList(0).PlaceOfSupply = AgL.XNull(DtPakkaSaleInvoice.Rows(0)("PlaceOfSupply"))
            SaleInvoiceTableList(0).StructureCode = ""
            SaleInvoiceTableList(0).CustomFields = ""
            SaleInvoiceTableList(0).ReferenceDocId = ""
            SaleInvoiceTableList(0).Tags = AgL.XNull(DtPakkaSaleInvoice.Rows(0)("Tags"))
            SaleInvoiceTableList(0).Remarks = "Pakka Invoice No : " + TxtPakkaSaleInvoiceNo.Text +
                                                        " And Invoice Amount : " + AgL.VNull(DtPakkaSaleInvoice.Rows(0)("Net_Amount")).ToString
            SaleInvoiceTableList(0).Status = "Active"
            SaleInvoiceTableList(0).EntryBy = AgL.PubUserName
            SaleInvoiceTableList(0).EntryDate = AgL.GetDateTime(AgL.GcnRead)
            SaleInvoiceTableList(0).ApproveBy = ""
            SaleInvoiceTableList(0).ApproveDate = ""
            SaleInvoiceTableList(0).MoveToLog = ""
            SaleInvoiceTableList(0).MoveToLogDate = ""
            SaleInvoiceTableList(0).UploadDate = ""
            SaleInvoiceTableList(0).LockText = "Genereded From Sale Invoice W Entry.Can't Edit."

            SaleInvoiceTableList(0).Deduction_Per = 0
            SaleInvoiceTableList(0).Deduction = 0
            SaleInvoiceTableList(0).Other_Charge_Per = 0
            SaleInvoiceTableList(0).Other_Charge = 0
            SaleInvoiceTableList(0).Round_Off = 0
            SaleInvoiceTableList(0).Net_Amount = 0


            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Sr = UBound(SaleInvoiceTableList) + 1
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_ItemCode = AgL.XNull(DtPakkaSaleInvoice.Rows(0)("ItemGroup"))
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_ItemName = ""
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Specification = ""
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_SalesTaxGroupItem = "GST 0%"
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_ReferenceNo = ""
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_DocQty = AgL.VNull(DtPakkaSaleInvoice.Rows(0)("TotalQty"))
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_FreeQty = 0
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Qty = AgL.VNull(DtPakkaSaleInvoice.Rows(0)("TotalQty"))
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Unit = "Nos"
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Pcs = 0
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_UnitMultiplier = 0
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_DealUnit = ""
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_DocDealQty = ""
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_DiscountPer = 0
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_DiscountAmount = 0
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_AdditionalDiscountPer = 0
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_AdditionalDiscountAmount = 0
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_AdditionPer = 0
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_AdditionAmount = 0
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Amount = 0
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Rate = 0
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Remark = ""
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_BaleNo = ""
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_LotNo = ""
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_ReferenceDocId = ""
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_SaleInvoice = AgL.XNull(DtPakkaSaleInvoice.Rows(0)("SaleOrder"))
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_SaleInvoiceSr = 1
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_GrossWeight = 0
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_NetWeight = 0
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Gross_Amount = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Amount
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Taxable_Amount = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Amount
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax1_Per = 0
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax1 = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Amount * SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax1_Per / 100
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax2_Per = 0
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax2 = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Amount * SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax2_Per / 100
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax3_Per = 0
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax3 = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Amount * SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax3_Per / 100
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax4_Per = 0
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax4 = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Amount * SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax4_Per / 100
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax5_Per = 0
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax5 = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Amount * SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax5_Per / 100
            SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_SubTotal1 = SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Amount +
                                                                SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax1 +
                                                                SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax2 +
                                                                SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax3 +
                                                                SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax4 +
                                                                SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax5


            'For Header Values
            Tot_Gross_Amount += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Gross_Amount
            Tot_Taxable_Amount += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Taxable_Amount
            Tot_Tax1 += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax1
            Tot_Tax2 += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax2
            Tot_Tax3 += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax3
            Tot_Tax4 += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax4
            Tot_Tax5 += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_Tax5
            Tot_SubTotal1 += SaleInvoiceTableList(UBound(SaleInvoiceTableList)).Line_SubTotal1


            'SaleInvoiceTableList(UBound(SaleInvoiceTableList)) = SaleInvoiceTable
            ReDim Preserve SaleInvoiceTableList(UBound(SaleInvoiceTableList) + 1)


            SaleInvoiceTableList(0).Gross_Amount = Tot_Gross_Amount
            SaleInvoiceTableList(0).Taxable_Amount = Tot_Taxable_Amount
            SaleInvoiceTableList(0).Tax1 = Tot_Tax1
            SaleInvoiceTableList(0).Tax2 = Tot_Tax2
            SaleInvoiceTableList(0).Tax3 = Tot_Tax3
            SaleInvoiceTableList(0).Tax4 = Tot_Tax4
            SaleInvoiceTableList(0).Tax5 = Tot_Tax5
            SaleInvoiceTableList(0).SubTotal1 = Tot_SubTotal1
            SaleInvoiceTableList(0).Other_Charge = 0
            SaleInvoiceTableList(0).Deduction = 0
            SaleInvoiceTableList(0).Round_Off = Math.Round(Math.Round(SaleInvoiceTableList(0).SubTotal1) - SaleInvoiceTableList(0).SubTotal1, 2)
            SaleInvoiceTableList(0).Net_Amount = Math.Round(SaleInvoiceTableList(0).SubTotal1)

            Dim Tot_RoundOff As Double = 0
            Dim Tot_NetAmount As Double = 0
            For J As Integer = 0 To SaleInvoiceTableList.Length - 1
                If Val(SaleInvoiceTableList(0).Gross_Amount) > 0 Then
                    SaleInvoiceTableList(J).Line_Round_Off = Math.Round(SaleInvoiceTableList(0).Round_Off * SaleInvoiceTableList(J).Line_Gross_Amount / SaleInvoiceTableList(0).Gross_Amount, 2)
                    SaleInvoiceTableList(J).Line_Net_Amount = Math.Round(SaleInvoiceTableList(0).Net_Amount * SaleInvoiceTableList(J).Line_Gross_Amount / SaleInvoiceTableList(0).Gross_Amount, 2)
                End If
                Tot_RoundOff += SaleInvoiceTableList(J).Line_Round_Off
                Tot_NetAmount += SaleInvoiceTableList(J).Line_Net_Amount
            Next

            If Tot_RoundOff <> SaleInvoiceTableList(0).Round_Off Then
                SaleInvoiceTableList(0).Line_Round_Off = SaleInvoiceTableList(0).Line_Round_Off + (SaleInvoiceTableList(0).Round_Off - Tot_RoundOff)
            End If

            If Tot_NetAmount <> SaleInvoiceTableList(0).Net_Amount Then
                SaleInvoiceTableList(0).Line_Net_Amount = SaleInvoiceTableList(0).Line_Net_Amount + (SaleInvoiceTableList(0).Net_Amount - Tot_NetAmount)
            End If

            'If SaleInvoiceTableList(0).Net_Amount > 0 Then
            Dim bDocId As String = FrmSaleInvoiceDirect_WithDimension_ShyamaShyam.InsertSaleInvoice(SaleInvoiceTableList)
            If AgL.XNull(bDocId) <> "" And (AgL.XNull(SaleInvoiceTableList(0).V_Type) = "SI" Or AgL.XNull(SaleInvoiceTableList(0).V_Type) = "WSI") Then
                TxtKachhaSaleInvoiceNo.Tag = bDocId

                mQry = " INSERT INTO SaleInvoiceGeneratedEntries(Code, Type, DocId, SaleOrderNo, SaleOrderDocId, Site_Code, Div_Code, V_Type) 
                        Select '" & TxtCode.Text & "' As Code, 
                        'Sale Invoice', '" & bDocId & "', '" & AgL.XNull(DtPakkaSaleInvoice.Rows(0)("SaleOrderNo")) & "', 
                        '" & AgL.XNull(DtPakkaSaleInvoice.Rows(0)("SaleOrder")) & "', 
                        '" & AgL.PubSiteCode & "', 
                        '" & AgL.PubDivCode & "', '" & SaleInvoiceTableList(0).V_Type & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = "Insert Into TransactionReferences (DocID, ReferenceDocID, IsEditingAllowed, IsDeletingAllowed) 
                        Values (" & AgL.Chk_Text(bDocId) & ", '" & bDocId & "', 1, 0) "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                If AgL.VNull(AgL.Dman_Execute("Select Count(*) From SaleInvoiceTransport Where DocId = '" & AgL.XNull(DtPakkaSaleInvoice.Rows(0)("DocId")) & "'", AgL.GCn).ExecuteScalar()) = 0 Then
                    mQry = "INSERT INTO SaleInvoiceTransport (DocID, LrNo, LrDate, NoOfBales)
                        Values( '" & AgL.XNull(DtPakkaSaleInvoice.Rows(0)("DocId")) & "', 
                        " & AgL.Chk_Text(TxtLrNo.Text) & ", 
                        " & AgL.Chk_Date(TxtLrDate.Text) & ",
                        " & Val(TxtNoOfBales.Text) & ") "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                Else
                    mQry = " UPDATE SaleInvoiceTransport
                            Set LrNo = " & AgL.Chk_Text(TxtLrNo.Text) & ", 
                            LrDate = " & AgL.Chk_Date(TxtLrDate.Text) & ",
                            NoOfBales = " & AgL.Chk_Text(TxtNoOfBales.Text) & "
                            Where DocId = '" & AgL.XNull(DtPakkaSaleInvoice.Rows(0)("DocId")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If

                mQry = "INSERT INTO Ledger (DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, 
                        SubCode, AmtDr, AmtCr, Narration, Site_Code, DivCode, RecId, LinkedSubcode)
                        Select H.DocId, 1 As V_Sno, H.V_No, H.V_Type, H.V_Prefix, 
                        H.V_Date, H.SaleToParty As SubCode, 0 As AmtDr, 0 As AmtCr, 
                        H.Remarks As Narration, H.Site_Code, H.Div_Code, 
                        H.ManualRefNo As RecId, H.BillToParty 
                        From SaleInvoice H
                        Where H.DocId = '" & bDocId & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
        End If
    End Sub
    Private Sub TxtPakkaSaleInvoiceNo_Validating(sender As Object, e As CancelEventArgs) Handles TxtPakkaSaleInvoiceNo.Validating
        mQry = "SELECT Si.DocId As KachhaSaleInvoice, Si.ManualRefNo As KachhaSaleInvoiceNo, 
                IfNull(Si.V_Date,H.V_Date) As KachhaSaleInvoiceDate, Sit.LrNo, Sit.LrDate, Ge.Code
                FROM SaleInvoice H 
                LEFT JOIN SaleInvoiceGeneratedEntries Ge ON H.DocID = Ge.DocId
                LEFT JOIN SaleInvoiceGeneratedEntries Ge1 ON Ge.Code = Ge1.Code AND Ge1.V_Type = 'WSI'
                LEFT JOIN SaleInvoice Si On Ge1.DocId = Si.DocId
                LEFT JOIN SaleInvoiceTransport Sit On H.DocId = Sit.DocId
                WHERE H.DocId = '" & TxtPakkaSaleInvoiceNo.Tag & "' "
        Dim DtPakkaSaleInvoice As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If DtPakkaSaleInvoice.Rows.Count > 0 Then
            TxtKachhaSaleInvoiceNo.Tag = AgL.XNull(DtPakkaSaleInvoice.Rows(0)("KachhaSaleInvoice"))
            TxtKachhaSaleInvoiceNo.Text = AgL.XNull(DtPakkaSaleInvoice.Rows(0)("KachhaSaleInvoiceNo"))
            TxtKachhaSaleInvoiceDate.Text = ClsMain.FormatDate(AgL.XNull(DtPakkaSaleInvoice.Rows(0)("KachhaSaleInvoiceDate")))
            TxtLrNo.Text = AgL.XNull(DtPakkaSaleInvoice.Rows(0)("LrNo"))
            TxtLrDate.Text = ClsMain.FormatDate(AgL.XNull(DtPakkaSaleInvoice.Rows(0)("LrDate")))
            TxtCode.Text = AgL.XNull(DtPakkaSaleInvoice.Rows(0)("Code"))
        End If
    End Sub
    Private Sub TxtBuyer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtPakkaSaleInvoiceNo.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then Exit Sub
            Select Case sender.name
                Case TxtPakkaSaleInvoiceNo.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataset Is Nothing Then
                            mQry = "SELECT H.DocID, H.ManualRefNo, H.V_Date, H.SaleToPartyName
                                    FROM SaleInvoice H 
                                    LEFT JOIN SaleInvoiceGeneratedEntries Ge ON H.DocID = Ge.DocId
                                    LEFT JOIN SaleInvoiceGeneratedEntries Ge1 ON Ge.Code = Ge1.Code AND Ge1.V_Type = 'WSI'
                                    WHERE H.V_Type = 'SI'
                                    AND Ge.Code IS NOT NULL
                                    AND Ge1.DocId IS NULL
                                    AND H.Site_Code = '" & AgL.PubSiteCode & "'
                                    AND H.Div_Code = '" & AgL.PubDivCode & "' "
                            TxtPakkaSaleInvoiceNo.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FProcSave()
        Dim mTrans As String = ""
        If FDataValidation() = False Then Exit Sub
        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            FPostSaleData_ForDifference(AgL.GCn, AgL.ECmd)

            AgL.ETrans.Commit()
            mTrans = "Commit"

            MsgBox("Entry Saved Successfully...", MsgBoxStyle.Information)

            BlankText()
            TxtPakkaSaleInvoiceNo.Focus()
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        FProcSave()

    End Sub
    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If Me.ActiveControl IsNot Nothing Then
            If TypeOf (Me.ActiveControl) Is TextBox Then
                If Not CType(Me.ActiveControl, TextBox).Multiline Then
                    If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
                End If
            End If

            If e.KeyCode = (Keys.S And e.Control) Then
                FProcSave()
            End If
        End If
    End Sub
End Class