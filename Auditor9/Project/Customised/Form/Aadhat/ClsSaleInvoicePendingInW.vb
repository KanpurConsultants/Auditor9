Imports System.ComponentModel
Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Public Class ClsSaleInvoicePendingInW

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

    Public Const Col1Select As String = "Tick"
    Public Const Col1SearchCode As String = "Search Code"
    Public Const Col1SearchSr As String = "Search Sr"
    Public Const Col1Exception As String = "Exception"
    Public Const Col1DocType As String = "Doc Type"
    Public Const Col1DocNo As String = "Doc No"
    Public Const Col1DocDate As String = "Doc Date"
    Public Const Col1PartyName As String = "Party Name"
    Public Const Col1LinkedPartyName As String = "Linked Party Name"
    Public Const Col1Amount As String = "Amount"
    Public Const Col1Remarks As String = "Remarks"


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
    Dim mHelpSubGroupQry$ = "Select 'o' As Tick, Sg.Code, Sg.Name, Sg.SubgroupType FROM ViewHelpSubgroup Sg Where Sg.SubGroupType In ('" & SubgroupType.Customer & "','" & SubgroupType.Supplier & "','" & SubgroupType.LedgerAccount & "')  "

    Public Sub Ini_Grid()
        Try
            mQry = "Select 'Pending' as Code, 'Pending' as Name "
            mQry += "  Union All "
            mQry += "Select 'Recorded' as Code, 'Recorded' as Name "
            mQry += "  Union All "
            mQry += "Select 'All' as Code, 'All' as Name "
            ReportFrm.CreateHelpGrid("Record Type", "Record Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, "Pending")
            ReportFrm.CreateHelpGrid("Account Name", "Account Name", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSubGroupQry)
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
            Dim mPurchaseReturnCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            Dim mDbPath As String
            mDbPath = AgL.INIRead(StrPath + "\" + IniName, "CompanyInfo", "ActualDBPath", "")
            Try
                AgL.Dman_ExecuteNonQry(" attach '" & mDbPath & "' as ODB", AgL.GCn)
            Catch ex As Exception
                'MsgBox(ex.Message)
            End Try



            RepTitle = "Sale Invoice Pening In W"
            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                Else
                    Exit Sub
                End If
            End If



            'mCondStr = " Where PH.V_Type='SI' And Date(PH.V_Date) between " & AgL.Chk_Date(AgL.PubStartDate) & " And " & AgL.Chk_Date(AgL.PubEndDate) & " "
            mCondStr = " Where PH.V_Type='SI' And Date(PH.V_Date) >='2020-01-01' "
            If ReportFrm.FGetText(0) = "Pending" Then
                mCondStr += " And H.DocID Is Null "
            ElseIf ReportFrm.FGetText(0) = "Recorded" Then
                mCondStr += " And H.DocID Is Not Null "
            End If
            mCondStr += ReportFrm.GetWhereCondition("L.Subcode", 1)
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("PH.Site_Code", 2), "''", "'")
            mCondStr = mCondStr & Replace(ReportFrm.GetWhereCondition("PH.Div_Code", 3), "''", "'")



            'mQry = "Select " & IIf(bDocId <> "", "'þ'", "'o'") & " As Tick, '' As Exception, H.DocID as SearchCode, 0 as SearchSr, 
            '        Site.Name as Site, Div.Div_Name as Division, PH.V_Date DocDate, PH.ManualRefNo as DocNo, Psg.Name as PartyName, 
            '        Pbsg.Name as LinkedAccount, PH.Net_Amount, Ph.Remarks, WH.ManualRefNo as KachchaDocNo, WH.V_Date as KachchaDocDate, WH.Net_Amount as KachchaNet_Amount
            '        from ODB.SaleInvoice PH
            '        Left Join ODB.SiteMast Site On PH.Site_Code = Site.Code
            '        Left Join ODB.Division Div On  PH.Div_Code = Div.Div_Code
            '        Left Join ODB.ViewHelpSubgroup Psg On PH.SaleToParty = Psg.Code
            '        Left Join ODB.ViewHelpSubgroup Pbsg On PH.BillToParty = Pbsg.Code
            '        Left Join SaleInvoice H On PH.ManualRefNo = H.ManualRefNo 
            '                                And PH.Site_Code = H.Site_Code 
            '                                And PH.Div_Code = H.Div_Code
            '                                And H.V_Type = 'SI'
            '                                And Date(H.V_Date) between " & AgL.Chk_Date(AgL.PubStartDate) & " And " & AgL.Chk_Date(AgL.PubEndDate) & "                
            '        Left Join SaleInvoiceGeneratedEntries LH On H.DocID = LH.DocID
            '        Left Join  SaleInvoiceGeneratedEntries LWH On LH.Code = LWH.Code And LWH.V_Type = 'WSI'
            '        Left Join SaleInvoice as WH On LWH.DocId = WH.DocID                 
            '        " + mCondStr

            'mQry = "Select H.DocID as SearchCode, 0 as SearchSr, 
            '        Site.Name as Site, Div.Div_Name as Division, PH.V_Date DocDate, PH.ManualRefNo as DocNo,
            '        (Select Max(sSO.ManualRefNo) From ODB.SaleInvoice sSO Where sSO.DocId = (Select Min(sSI.SaleInvoice) From ODB.SaleInvoiceDetail sSI Where sSI.DocId = PH.DocId)) as OrderNo,
            '        Psg.Name as PartyName, 
            '        Pbsg.Name as LinkedAccount, PH.Net_Amount, Ph.Remarks, (Case When LH.DocID Is Not Null Then IfNull(WH.ManualRefNo,'100%') Else WH.ManualRefNo End) as KachchaDocNo, WH.V_Date as KachchaDocDate, WH.Net_Amount as KachchaNet_Amount
            '        from ODB.SaleInvoice PH
            '        Left Join ODB.SiteMast Site On PH.Site_Code = Site.Code
            '        Left Join ODB.Division Div On  PH.Div_Code = Div.Div_Code
            '        Left Join ODB.ViewHelpSubgroup Psg On PH.SaleToParty = Psg.Code
            '        Left Join ODB.ViewHelpSubgroup Pbsg On PH.BillToParty = Pbsg.Code
            '        Left Join SaleInvoice H On PH.DocID = H.OmsID
            '        Left Join SaleInvoiceGeneratedEntries LH On H.DocID = LH.DocID
            '        Left Join  SaleInvoiceGeneratedEntries LWH On LH.Code = LWH.Code And LWH.V_Type = 'WSI'
            '        Left Join SaleInvoice as WH On LWH.DocId = WH.DocID                 
            '        " + mCondStr

            'mQry = mQry + " Order By PH.V_Date, Div.Div_Name, Site.Name, Cast(PH.ManualRefNo as Integer) "

            Dim sQryBrand As String
            sQryBrand = "(Select group_concat(Brand ,',') || ',' From (Select IfNull(sGroup.Description, sItem.Description)  as Brand  from ODB.SaleInvoiceDetail sSID  With (NoLock) Left Join ODB.Item sItem On sSID.Item = sItem.Code Left Join ODB.Item sGroup On sItem.ItemGroup = sGroup.Code Where sSID.DocID = PH.DocID And sItem.ItemType = '" & ItemTypeCode.TradingProduct & "' Group By IfNull(sGroup.Description, sItem.Description)))"


            mQry = "Select H.DocID as SearchCode, 0 as SearchSr, 
                    Site.Name as Site, Div.Div_Name as Division, PH.V_Date DocDate, PH.ManualRefNo as DocNo,
                    Psg.Name as PartyName, Pbsg.Name as LinkedAccount, 
                    " & sQryBrand & " as Brand,
                    PH.Net_Amount, Ph.Remarks, 
                    H.ManualRefNo as KachchaDocNo, H.V_Date as KachchaDocDate, H.Net_Amount as KachchaNet_Amount
                    from ODB.SaleInvoice PH
                    Left Join ODB.SiteMast Site On PH.Site_Code = Site.Code
                    Left Join ODB.Division Div On  PH.Div_Code = Div.Div_Code
                    Left Join ODB.ViewHelpSubgroup Psg On PH.SaleToParty = Psg.Code
                    Left Join ODB.ViewHelpSubgroup Pbsg On PH.BillToParty = Pbsg.Code
                    Left Join SaleInvoice H On PH.DocID = H.AmsDocId
                    " + mCondStr

            mQry = mQry + " Order By PH.V_Date, Div.Div_Name, Site.Name, Cast(PH.ManualRefNo as Integer) "


            DsHeader = AgL.FillData(mQry, AgL.GCn)




            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")


            ReportFrm.Text = "Sale Invoice Pening In W"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcMain"

            'ReportFrm.InputColumnsStr = Col1FavouringName
            'mQry = "Select 'Create JSON File' As MenuText, 'FCreateJSONFile' As FunctionName"
            'Dim DtMenuList As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            'ReportFrm.DTCustomMenus = DtMenuList

            ReportFrm.ProcFillGrid(DsHeader)


            'ReportFrm.DGL1.ReadOnly = False
            'For I As Integer = 0 To ReportFrm.DGL1.Columns.Count - 1
            '    ReportFrm.DGL1.Columns(I).ReadOnly = True
            'Next
            'ReportFrm.DGL1.Columns(Col1FavouringName).ReadOnly = False
            'ReportFrm.DGL1.Columns(Col1ChequeNo).ReadOnly = False
            'ReportFrm.DGL1.Columns(Col1ChequeDate).ReadOnly = False
            'ReportFrm.DGL1.Columns(Col1AccountPayeeYn).ReadOnly = False
            ReportFrm.DGL1.AutoResizeRows()

        Catch ex As Exception
                MsgBox(ex.Message)
            DsHeader = Nothing
        End Try
    End Sub

    Public Sub FProceed()
        Dim I As Integer = 0
        Dim mV_Type As String = ""
        Dim mTrans As String = ""
        Dim mMainQry As String
        Dim mRecordCount As Integer
        Dim mRecordCountException As Integer

        If ReportFrm.FGetText(1) = "" Then
            MsgBox("Please input Scheme Process Date...!", MsgBoxStyle.Information)
            Exit Sub
        End If

        Try

            mMainQry = ""
            mRecordCount = 0
            mRecordCountException = 0
            For I = 0 To ReportFrm.DGL1.Rows.Count - 1
                If ReportFrm.DGL1.Item(Col1Select, I).Value = "þ" Then
                    If Val(ReportFrm.DGL1.Item(Col1Amount, I).Value) > 0 Then
                        mRecordCount += 1
                        'If mMainQry <> "" Then mMainQry += " Union All "
                        'mMainQry += " Select " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1SearchCode, I).Value) & " as DocID,
                        '                   " & Val(ReportFrm.DGL1.Item(Col1SearchSr, I).Value) & " as Sr,
                        '                   " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1ChequeText, I).Value) & " as ChequeText,
                        '                   " & AgL.Chk_Text(ReportFrm.DGL1.Item(Col1AccountPayeeYn, I).Value) & " as AccountPayeeYn
                        '            "

                        If AgL.XNull(ReportFrm.DGL1.Item(Col1Exception, I).Value) <> "" Then
                            mRecordCountException += 1
                        End If
                    End If
                End If
            Next

            If mRecordCount = 0 Then
                MsgBox("No record selected to proceed")
                Exit Sub
            End If


            If mRecordCountException > 0 Then
                MsgBox("Please clear exception before proceeding")
                Exit Sub
            End If

            Dim objRepPrint As Object
            objRepPrint = New AgLibrary.RepView(AgL)

            ClsMain.FPrintThisDocument(ReportFrm, objRepPrint, "", mMainQry, "Cheque_Print.rpt", ".", , , , "", AgL.PubLoginDate, False)

            'ReportFrm.DGL1.DataSource = Nothing
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
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
