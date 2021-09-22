Imports AgLibrary.ClsMain.agConstants

Public Class ClsDeleteAttachments

    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim ErrorLog As String = ""
    Dim mLogText As String = ""

    Dim WithEvents ReportFrm As AgLibrary.FrmReportLayout

    Public Property GRepFormName() As String
        Get
            GRepFormName = mGRepFormName
        End Get
        Set(ByVal value As String)
            mGRepFormName = value
        End Set
    End Property

    Dim DsRep As DataSet = Nothing, DsRep1 As DataSet = Nothing, DsRep2 As DataSet = Nothing
    Dim mQry$ = "", RepName$ = "", RepTitle$ = "", OrderByStr$ = ""

    Dim StrMonth$ = ""
    Dim StrQuarter$ = ""
    Dim StrFinancialYear$ = ""
    Dim StrTaxPeriod$ = ""

    Private Const rowAsOnDate As Integer = 0
    Public Sub Ini_Grid()
        Try
            ReportFrm.CreateHelpGrid("AsOnDate", "As On Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
            ReportFrm.BtnPrint.Text = "Delete Attachments"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcDeleteAttachments()
    End Sub
    Public Sub New(ByVal mReportFrm As AgLibrary.FrmReportLayout)
        ReportFrm = mReportFrm
    End Sub
    Private Sub ProcDeleteAttachments()
        Dim bConStr$ = ""
        Dim mDocId As String = ""
        Dim DtTemp As DataTable

        If ReportFrm.FGetText(0) = "" Then MsgBox("As On Date is required.", MsgBoxStyle.Information) : Exit Sub

        If MsgBox("Are you sure you want to proceed delete data ?" & vbNewLine & "This will wash selected data.", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "") = MsgBoxResult.Yes Then
            Try
                mQry = " Select H.DocId From PurchInvoice H "
                mQry += " Where Date(H.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0))) & ""
                mQry += " UNION ALL "
                mQry += " Select H.DocId From SaleOrder H "
                mQry += " Where Date(H.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0))) & ""
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

                For I As Integer = 0 To DtTemp.Rows.Count - 1
                    If System.IO.Directory.Exists(PubAttachmentPath + AgL.XNull(DtTemp.Rows(I)("DocId"))) = True Then
                        System.IO.Directory.Delete(PubAttachmentPath + AgL.XNull(DtTemp.Rows(I)("DocId")), True)
                    End If
                Next

                MsgBox("Process Complete.", MsgBoxStyle.Information)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
    Private Sub FDeleteLedgerHead()
        Dim DtParties As DataTable
        Dim DtLedger As DataTable
        Dim DtLedgerTotal As DataTable
        Dim mDeleteFullEntry As Boolean = False

        mQry = " Select * From SubGroup Sg Where 1=1 "
        mQry += ReportFrm.GetWhereCondition("SubCode", 1)
        DtParties = AgL.FillData(mQry, AgL.GCn).Tables(0)

        For P As Integer = 0 To DtParties.Rows.Count - 1
            mQry = " Select L.DocId, L.LinkedSubcode, Sum(L.AmtDr) As AmtDr, 
                    Sum(L.AmtCr) As AmtCr, Max(Vt.NCat) As NCat 
                    From Ledger L
                    LEFT JOIN Voucher_Type Vt On L.V_Type = Vt.V_Type
                    Where L.LinkedSubcode = '" & DtParties.Rows(P)("SubCode") & "' 
                    And Date(L.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) &
                    " Group By L.DocId, L.LinkedSubcode "
            DtLedger = AgL.FillData(mQry, AgL.GCn).Tables(0)

            For I As Integer = 0 To DtLedger.Rows.Count - 1
                mQry = " Select IfNull(Sum(AmtDr),0) As AmtDrTotal, 
                        IfNull(Sum(AmtCr),0) As AmtCrTotal
                        From Ledger 
                        Where DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "' "
                DtLedgerTotal = AgL.FillData(mQry, AgL.GCn).Tables(0)

                If AgL.VNull(DtLedger.Rows(I)("AmtDr")) = DtLedgerTotal.Rows(0)("AmtCrTotal") Then
                    mDeleteFullEntry = True
                End If

                If AgL.VNull(DtLedger.Rows(I)("AmtCr")) = DtLedgerTotal.Rows(0)("AmtDrTotal") Then
                    mDeleteFullEntry = True
                End If

                If AgL.VNull(AgL.Dman_Execute(" Select Count(Distinct LinkedSubcode) From Ledger 
                    Where DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "'", AgL.GCn).ExecuteScalar()) = 1 Then
                    mDeleteFullEntry = True
                End If

                If mDeleteFullEntry = True Then
                    mQry = "DELETE FROM Ledger Where DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = "DELETE FROM LedgerAdj " & " Where Vr_DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = "DELETE FROM LedgerHeadCharges Where DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = "DELETE FROM LedgerHeadDetail Where DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = "DELETE FROM LedgerHeadDetailCharges Where DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = "DELETE FROM LedgerHeadDetailChequePrinting Where DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = "DELETE FROM LedgerItemAdj Where DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = "DELETE FROM LedgerM Where DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    mQry = "DELETE FROM LedgerHead Where DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                Else
                    If AgL.XNull(DtLedger.Rows(I)("NCat")) <> Ncat.JournalVoucher Then
                        If AgL.VNull(AgL.Dman_Execute(" Select Count(*) 
                                From LedgerHeadDetail 
                                Where DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "'", AgL.GCn).ExecuteScalar()) <> 0 Then
                            mQry = " Delete From LedgerHeadDetail 
                                Where DocId = '" & AgL.XNull(DtLedger.Rows(I)("DocId")) & "'
                                And LinkedSubcode = '" & AgL.XNull(DtLedger.Rows(I)("LinkedSubcode")) & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                            FrmVoucherEntry.FGetCalculationData(AgL.XNull(DtLedger.Rows(I)("DocId")), AgL.GCn, AgL.ECmd)
                        End If
                    End If
                End If
            Next
            mQry = " Select L.DocId, L.V_Type || '-' || L.RecId As RecId, Sg.Name As PartyName  
                    From Ledger L
                    LEFT JOIN SubGroup Sg On L.LinkedSubcode = Sg.SubCode
                    Where L.LinkedSubcode = '" & DtParties.Rows(P)("Subcode") & "'
                    And Date(L.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(0)).ToString("s")) & ""
            Dim DtPendingLedgerEntries As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            Dim UnableToDelete As String = ""
            For I As Integer = 0 To DtPendingLedgerEntries.Rows.Count - 1
                UnableToDelete += "Unable To Delete Entry " & AgL.XNull(DtPendingLedgerEntries.Rows(I)("RecId")) & " For Party " & AgL.XNull(DtPendingLedgerEntries.Rows(I)("PartyName"))
            Next
            If UnableToDelete <> "" Then
                Err.Raise(1, "", UnableToDelete)
            End If
        Next
    End Sub
    Private Sub FCreateLog(bTable As String, bConStr As String, bQry As String)
        If mLogText = "" Then
            mLogText += " As On Date : " & ReportFrm.FGetText(0) & vbCrLf
            mLogText += " Party : " & ReportFrm.FGetText(1) & vbCrLf
        End If

        If bQry <> "" Then
            mQry = bQry
        Else
            mQry = " Select DocId, ManualRefNo As DocNo, V_Date As DocDate From " & bTable & bConStr
        End If
        Dim DtTemp As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        For I As Integer = 0 To DtTemp.Rows.Count - 1
            mLogText += " Affected Document DocId : " & AgL.XNull(DtTemp.Rows(I)("DocId")) & ", Doc No : " & AgL.XNull(DtTemp.Rows(I)("DocNo")) & ", Doc Date : " & AgL.XNull(DtTemp.Rows(I)("DocDate")) & vbCrLf
        Next
    End Sub
End Class
