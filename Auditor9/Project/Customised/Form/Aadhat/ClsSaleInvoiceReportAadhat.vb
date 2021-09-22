Imports System.ComponentModel
Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsSaleInvoiceReportAadhat

    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim mQry As String = ""
    Dim RepTitle As String = ""

    Dim DsReport As DataSet = New DataSet
    Dim DTReport As DataTable = New DataTable
    Dim IntLevel As Int16 = 0

    Dim WithEvents ReportFrm As FrmRepDisplay
    Public Const GFilter As Byte = 2
    Public Const GFilterCode As Byte = 4



    Dim mShowReportType As String = ""

    Dim DsHeader As DataSet = Nothing

    Public Property GRepFormName() As String
        Get
            GRepFormName = mGRepFormName
        End Get
        Set(ByVal value As String)
            mGRepFormName = value
        End Set
    End Property
    Public Property ShowReportType() As String
        Get
            ShowReportType = mShowReportType
        End Get
        Set(ByVal value As String)
            mShowReportType = value
        End Set
    End Property

    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name FROM SiteMast "
    Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code As Code, Div_Name As Name From Division "
    Dim mHelpYesNoQry$ = " Select 'Yes' As Code, 'Yes' AS [Value] Union All Select 'No' As Code, 'No' AS [Value] "
    Dim mHelpWPartyQry$ = "Select 'o' As Tick, Sg.Code, Sg.Name, Sg.SubgroupType FROM ViewHelpSubgroup Sg Where Sg.SubGroupType In ('Master Customer')  "
    Dim mHelpSubPartyQry$ = "Select 'o' As Tick, Sg.Code, Sg.Name, Sg.SubgroupType FROM ViewHelpSubgroup Sg Where Sg.SubGroupType In ('" & SubgroupType.Customer & "')  "

    Public Sub Ini_Grid()
        Try
            mQry = "Select 'Header Detail' as Code, 'Header Detail' as Name "
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Header Detail")
            ReportFrm.CreateHelpGrid("FromDate", "From Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.DateType, "", AgL.PubEndDate)
            ReportFrm.CreateHelpGrid("W Party Name", "W Party Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpWPartyQry)
            ReportFrm.CreateHelpGrid("Sub Party Name", "Sub Party Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpWPartyQry)
            ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            'ReportFrm.BtnProceed.Visible = True
            'ReportFrm.BtnProceed.Text = "Print Cheque"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcMain()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay)
        ReportFrm = mReportFrm
    End Sub

    Public Sub ProcMain(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing, Optional bDocId As String = "")
        Try
            Dim mCondStr$ = ""
            Dim mCondStrDivSite$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"



            RepTitle = "Sale Invoice Pening In W"
            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                Else
                    Exit Sub
                End If
            End If


            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(1)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(2)).ToString("s")) & " "
            mCondStr += ReportFrm.GetWhereCondition("H.SaleToParty", 3)
            mCondStr += ReportFrm.GetWhereCondition("H.BillToParty", 4)
            mCondStrDivSite = mCondStrDivSite & Replace(ReportFrm.GetWhereCondition("Site_Code", 5), "''", "'")
            mCondStrDivSite = mCondStrDivSite & Replace(ReportFrm.GetWhereCondition("Div_Code", 6), "''", "'")


            mQry = "Select VMain.DocID as SearchCode, VMain.V_Type, VMain.InvoiceNo, VMain.V_Date, VMain.Item, VMain.Party, Vmain.LinkedParty, VMain.Amount, VMain.BillAmount
                    From (
                            SELECT ge.Code, Max(CASE WHEN H.V_Type ='WSI' Then H.DocID ELSE NULL END) AS DocId, 
                            Max(CASE WHEN H.V_Type ='WSI' Then H.V_Type ELSE NULL END) AS V_Type, 
                            Max(CASE WHEN H.V_Type ='WSI' Then H.ManualRefNo  ELSE NULL END) AS InvoiceNo ,
                            Max(CASE WHEN H.V_Type ='WSI' Then H.V_Date ELSE NULL END) as ActualV_Date, 
                            strftime('%d-%m-%Y',Max(CASE WHEN H.V_Type ='WSI' Then H.V_Date ELSE NULL END)) AS V_Date, 
                            Max(CASE WHEN H.V_Type ='WSI' Then I.Description ELSE NULL END) AS Item, 
                            Max(Sg.Name) AS Party, Max(lsg.Name) AS LinkedParty, 
                            Sum(L.Amount) AS Amount,  Sum(L.Net_Amount) AS BillAmount
                            FROM SaleInvoiceGeneratedEntries ge
                            LEFT JOIN (Select * From SaleInvoice Where 1=1 " & mCondStrDivSite & ")  H ON ge.DocId = H.DocID
                            LEFT JOIN saleInvoicedetail L ON H.DocID = L.DocID 
                            LEFT JOIN Item I ON L.Item = I.Code 
                            LEFT JOIN Item IG ON I.ItemGroup = IG.Code
                            LEFT JOIN viewHelpSubgroup sg ON H.SaleToParty = Sg.Code  
                            LEFT JOIN viewHelpSubgroup lsg ON H.BillToParty = Lsg.Code  
                            LEFT JOIN voucher_type Vt ON H.V_Type = Vt.V_Type 
                            WHERE vt.NCat ='SI' " + mCondStr + "
                            GROUP BY Ge.Code 
                        ) as VMain
                    ORDER BY Try_Parse(Replace(VMain.InvoiceNo,'-','') as Integer)  
                    "

            DsHeader = AgL.FillData(mQry, AgL.GCn)




            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")


            ReportFrm.Text = "Sale Invoice Report Aadhat"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcMain"


            ReportFrm.ProcFillGrid(DsHeader)
            ReportFrm.DGL1.AutoResizeRows()

        Catch ex As Exception
            MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub

    Public Sub FProceed()
    End Sub
    Private Sub ReportFrm_BtnProceedPressed() Handles ReportFrm.BtnProceedPressed
        FProceed()
    End Sub


    Private Sub ObjRepFormGlobal_Dgl1KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ReportFrm.Dgl1KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            If ReportFrm.DGL1.CurrentCell Is Nothing Then Exit Sub

            bRowIndex = ReportFrm.DGL1.CurrentCell.RowIndex
            bColumnIndex = ReportFrm.DGL1.CurrentCell.ColumnIndex

            If ClsMain.IsSpecialKeyPressed(e) = True Then Exit Sub

            Select Case ReportFrm.DGL1.Columns(bColumnIndex).Name
                'Case Col1AccountPayeeYn
                '    If Not ClsMain.IsSpecialKeyPressed(e) Then
                '        If e.KeyCode = Keys.N Then
                '            ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Value = "NO"
                '        Else
                '            ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Value = "YES"
                '        End If
                '    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ObjRepFormGlobal_Dgl1CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles ReportFrm.DGL1CellBeginEdit
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0

        Try
            If ReportFrm.DGL1.CurrentCell Is Nothing Then Exit Sub

            bRowIndex = ReportFrm.DGL1.CurrentCell.RowIndex
            bColumnIndex = ReportFrm.DGL1.CurrentCell.ColumnIndex

            Select Case ReportFrm.DGL1.Columns(bColumnIndex).Name
                'Case Col1AccountPayeeYn
                '    e.Cancel = True
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub ObjRepFormGlobal_Dgl1CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles ReportFrm.DGL1CellEnter
        Dim bRowIndex As Integer
        Dim bColumnIndex As Integer
        Try
            If ReportFrm.DGL1.CurrentCell Is Nothing Then Exit Sub

            Select Case ReportFrm.DGL1.Columns(ReportFrm.DGL1.CurrentCell.ColumnIndex).Name
            End Select

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ReportFrm_DGL1EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles ReportFrm.DGL1EditingControl_Validating
        Dim bRowIndex As Integer, bColumnIndex As Integer

        bRowIndex = ReportFrm.DGL1.CurrentCell.RowIndex
        bColumnIndex = ReportFrm.DGL1.CurrentCell.ColumnIndex

        Select Case ReportFrm.DGL1.Columns(bColumnIndex).Name
            'Case Col1FavouringName
            '    FPrepareTextForChequePrintingAfterFilling(bRowIndex)
            'Case Col1ChequeDate
            '    ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Value = AgL.RetDate(ReportFrm.DGL1.Item(bColumnIndex, bRowIndex).Value)
            '    FPrepareTextForChequePrintingAfterFilling(bRowIndex)
        End Select
    End Sub
End Class
