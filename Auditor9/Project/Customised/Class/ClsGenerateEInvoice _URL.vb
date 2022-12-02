Imports System.IO
Imports AgLibrary
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Imports Microsoft.Reporting.WinForms
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Net
Imports System.Windows.Forms
Imports Customised.ClsMain
Public Class ClsGenerateEInvoice_URL

    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim mQry As String = ""
    Dim RepTitle As String = ""
    Dim EntryNCat As String = ""


    Dim DsReport As DataSet = New DataSet
    Dim DTReport As DataTable = New DataTable
    Dim IntLevel As Int16 = 0

    Dim WithEvents ReportFrm As FrmRepDisplay
    Public Const GFilter As Byte = 2
    Public Const GFilterCode As Byte = 4

    Const mReportType_PendingForEInvoice As String = "Pending For E-Invoice"
    Const mReportType_PendingForEWayBill As String = "Pending For E-Way Bill"
    Const mReportType_All As String = "All"



    Dim mShowReportType As String = ""
    Dim mReportDefaultText$ = ""

    Dim DsHeader As DataSet = Nothing

    Dim rowReportType As Integer = 0
    Dim rowFromDate As Integer = 1
    Dim rowToDate As Integer = 2


    Dim mDocId As String = ""


    Dim DTDivisionSiteDetail As DataTable = Nothing

    Dim mclient_id As String = ""
    Dim mClient_Secret As String = ""
    Dim mGspName As String = ""
    Dim mAspUserId As String = ""
    'Dim mBaseUrl As String = ""
    'Dim mAuthUrl As String = ""
    Dim mAspPassword As String = ""
    Dim mTokenExp As String = ""
    Dim mSek As String = ""
    Dim mAuthToken As String = ""
    Dim mAppKey As String = ""
    Dim mGstin As String = ""
    Dim mPassword As String = ""
    Dim mUserName As String = ""
    Dim mResponceHdr As String = ""
    'Dim mEwbBaseUrl As String = ""
    'Dim mCancelEWBUrl As String = ""
    Dim mResponse As String = ""

    Dim mAuthGenerationURL As String = ""
    Dim mIRNGenerationURL As String = ""
    Dim mEWayBillGenerationURL As String = ""
    Dim mSelectEWayBillURL As String = ""
    Dim mSaveEWayBillFileURL As String = ""
    Dim mIRNCancelURL As String = ""

    Const mEInvoiceMode_Sandbox As String = "Sandbox"
    Const mEInvoiceMode_Production As String = "Production"

    Dim mEInvoiceMode As String = mEInvoiceMode_Production

    'Dim Result As String, url As String, strIRN As String, strdata As String
    'Dim strAckNo As String, strAckDate As String, EWBImg As String, strQrCodeImage As String

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
    Dim mHelpPartyQry$ = " Select 'o' As Tick,  Sg.SubCode As Code, Sg.DispName || ',' ||  City.CityName AS Party, Sg.Address FROM SubGroup Sg Left Join City On Sg.CityCode = City.CityCode Where Sg.Nature In ('Customer','Supplier','Cash') "
    Dim mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select 'o' As Tick, Code, Description From State "
    Dim mHelpItemQry$ = "Select 'o' As Tick, Code, Description As [Item] From Item Where V_Type = '" & ItemV_Type.Item & "'"
    Dim mHelpPurchaseAgentQry$ = " Select 'o' As Tick, Sg.Code, Sg.Name AS Agent FROM viewHelpSubgroup Sg Where Sg.SubgroupType = '" & SubgroupType.PurchaseAgent & "' "
    Dim mHelpItemGroupQry$ = "Select 'o' As Tick, Code, Description As [Item Group] From ItemGroup "
    Dim mHelpItemCategoryQry$ = "Select 'o' As Tick, Code, Description As [Item Category] From ItemCategory "
    Dim mHelpItemTypeQry$ = "Select 'o' As Tick, Code, Name FROM ItemType "
    Dim mHelpLocationQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party FROM viewHelpSubGroup Sg Where Sg.Nature In ('Supplier','Stock') "
    Dim mHelpTagQry$ = "Select Distinct 'o' As Tick, H.Tags as Code, H.Tags as Description  FROM PurchInvoiceDetail H "
    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        FGetSettings = mValue
    End Function
    Public Sub Ini_Grid()
        Try
            mQry = "Select '" & mReportType_PendingForEInvoice & "' as Code, '" & mReportType_PendingForEInvoice & "' as Name 
                    Union All 
                    Select '" & mReportType_PendingForEWayBill & "' as Code, '" & mReportType_PendingForEWayBill & "' as Name 
                    Union All 
                    Select '" & mReportType_All & "' as Code, '" & mReportType_All & "' as Name"
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.SingleSelection, mQry, mReportType_PendingForEInvoice,,, 300)

            ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
            'ReportFrm.CreateHelpGrid("Site", "Site", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
            'ReportFrm.CreateHelpGrid("Division", "Division", FrmRepDisplay.FieldFilterDataType.StringType, FrmRepDisplay.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            'ReportFrm.CreateHelpGrid("Mobile", "Mobile", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.StringType, "", "")
            If mDocId <> "" Then ReportFrm.FilterGrid.Rows(rowReportType).Visible = False
            'ReportFrm.FilterGrid.Rows(rowMobile).Visible = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcGenerateEInvoice()
    End Sub
    Public Sub New(ByVal mReportFrm As FrmRepDisplay, Optional bDocId As String = "")
        LoadEInvoiceSessionValues()
        mDocId = bDocId
        'btnGeneInvoice_Click(mDocId)
        'btnGenEWBbyIrn_Click(mDocId)
        'bynGenEWBPdf_Click(mDocId)
        ReportFrm = mReportFrm


    End Sub
    Public Sub ProcGenerateEInvoice(Optional mFilterGrid As AgControls.AgDataGrid = Nothing,
                                Optional mGridRow As DataGridViewRow = Nothing)
        Try
            Dim mSaleCondStr$ = ""
            Dim mLedgerHeadCondStr$ = ""
            Dim mPurchaseReturnCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            RepTitle = "Generate E-Invoice"

            If mEInvoiceMode = mEInvoiceMode_Sandbox And AgL.StrCmp(AgL.PubUserName, "Super") = False Then
                MsgBox(" This is running in Testing Mode. You can't generate IRN now.", MsgBoxStyle.Information)
                Exit Sub
            End If


            If mFilterGrid IsNot Nothing And mGridRow IsNot Nothing Then
                If mGridRow.DataGridView.Columns.Contains("Search Code") = True Then
                    ClsMain.FOpenForm(mGridRow.Cells("Search Code").Value, ReportFrm)
                    ReportFrm.FiterGridCopy_Arr.RemoveAt(ReportFrm.FiterGridCopy_Arr.Count - 1)
                Else
                    Exit Sub
                End If
            End If

            If mDocId <> "" Then
                mQry = " Select H.V_Date, H.V_Type || '-' || H.ManualRefNo As InvoiceNo From SaleInvoice H Where H.DocId = '" & mDocId & "'"
                Dim DtInvoiceDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtInvoiceDetail.Rows.Count > 0 Then
                    ReportFrm.FilterGrid.Item(GFilter, rowFromDate).Value = ClsMain.FormatDate(AgL.XNull(DtInvoiceDetail.Rows(0)("V_Date")))
                    ReportFrm.FilterGrid.Item(GFilter, rowToDate).Value = ClsMain.FormatDate(AgL.XNull(DtInvoiceDetail.Rows(0)("V_Date")))
                End If
                mSaleCondStr = " Where H.DocId = '" & mDocId & "' "
            Else
                mSaleCondStr = " Where Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
                If AgL.XNull(ReportFrm.FilterGrid.Item(GFilter, rowReportType).Value) = mReportType_PendingForEInvoice Then
                    mSaleCondStr += " And H.EInvoiceIRN Is Null "
                ElseIf AgL.XNull(ReportFrm.FilterGrid.Item(GFilter, rowReportType).Value) = mReportType_PendingForEWayBill Then
                    mSaleCondStr += " And Sit.RoadPermitNo Is Null "
                End If
            End If
            mSaleCondStr += " And H.Div_Code = '" & AgL.PubDivCode & "' "
            mSaleCondStr += " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mSaleCondStr += " And Vt.NCat In ('" & Ncat.SaleInvoice & "','" & Ncat.SaleReturn & "') "
            mSaleCondStr += " And H.SaleToPartySalesTaxNo Is Not Null "




            'For LedgerHead Condition
            If mDocId <> "" Then
                mQry = " Select H.V_Date, H.V_Type || '-' || H.ManualRefNo As InvoiceNo From LedgerHead H Where H.DocId = '" & mDocId & "'"
                Dim DtInvoiceDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtInvoiceDetail.Rows.Count > 0 Then
                    ReportFrm.FilterGrid.Item(GFilter, rowFromDate).Value = ClsMain.FormatDate(AgL.XNull(DtInvoiceDetail.Rows(0)("V_Date")))
                    ReportFrm.FilterGrid.Item(GFilter, rowToDate).Value = ClsMain.FormatDate(AgL.XNull(DtInvoiceDetail.Rows(0)("V_Date")))
                End If
                mLedgerHeadCondStr = " Where H.DocId = '" & mDocId & "' "
            Else
                mLedgerHeadCondStr = " Where Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & " "
                If AgL.XNull(ReportFrm.FilterGrid.Item(GFilter, rowReportType).Value) = mReportType_PendingForEInvoice Then
                    mLedgerHeadCondStr += " And H.EInvoiceIRN Is Null "
                End If
            End If
            mLedgerHeadCondStr += " And H.Div_Code = '" & AgL.PubDivCode & "' "
            mLedgerHeadCondStr += " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mLedgerHeadCondStr += " And Vt.NCat In ('" & Ncat.DebitNoteCustomer & "','" & Ncat.CreditNoteCustomer & "') "
            mLedgerHeadCondStr += " And H.PartySalesTaxNo Is Not Null "


            'Sale Invoice Qry
            mQry = "Select " & IIf(mDocId <> "", "'þ'", "'o'") & " As Tick, '' As Exception, H.DocId  As SearchCode, Vt.Description As VoucherType, 
                '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo as InvoiceNo, 
                strftime('%d/%m/%Y', H.V_Date) As InvoiceDate, IfNull(H.Net_Amount,0) As InvoiceValue,
                Sg.DispName As Party, 
                Case When H.ShipToParty Is Not Null Then ShipTo.Pin Else H.SaleToPartyPinCode End As PinCode, 
                Case When H.ShipToParty Is Not Null Then ShipToState.Description Else S.Description End As State, 
                TSg.DispName As Transporter,
                VDist.Distance As Distance, H.EInvoiceIRN As Irn, 
                Sit.RoadPermitNo As EwayBillNo, Sit.RoadPermitDate As EwayBillDate
                From SaleInvoice H 
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                LEFT JOIN City C On H.SaleToPartyCity = C.CityCode
                LEFT JOIN State S On C.State = S.Code
                LEFT JOIN SubGroup Sg On H.SaleToParty = Sg.SubCode
                LEFT JOIN SubGroup ShipTo On H.ShipToParty = ShipTo.SubCode
                LEFT JOIN City ShipToCity On ShipTo.CityCode = ShipToCity.CityCode
                LEFT JOIN State ShipToState On ShipToCity.State = ShipToState.Code
                LEFT JOIN SaleInvoiceTransport Sit On H.DocId = Sit.DocId
                LEFT JOIN (Select SubCode, Max(Transporter) Transporter 
                            From SubgroupSiteDivisionDetail
                            Group By SubCode) As Hlt On H.SaleToParty = Hlt.SubCode
                LEFT JOIN SubGroup TSg ON IfNull(Sit.Transporter,Hlt.Transporter) = TSg.SubCode
                LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration 
                            Where RegistrationType = 'Sales Tax No') As VReg On H.SaleToParty = VReg.SubCode
                LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration 
                            Where RegistrationType = 'Sales Tax No') As VTranReg On TSg.SubCode = VTranReg.SubCode
                LEFT JOIN (Select SubCode, Distance
                            From SubgroupSiteDivisionDetail 
                            Where Site_Code = '" & AgL.PubSiteCode & "' 
                            And Div_Code = '" & AgL.PubDivCode & "') As VDist On IfNull(H.ShipToParty,H.SaleToParty) = VDist.SubCode " & mSaleCondStr

            mQry = mQry + " UNION ALL "
            mQry = mQry + " Select " & IIf(mDocId <> "", "'þ'", "'o'") & " As Tick, '' As Exception, H.DocId  As SearchCode, Vt.Description As VoucherType, 
                '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo as InvoiceNo, 
                strftime('%d/%m/%Y', H.V_Date) As InvoiceDate, IfNull(Hc.Net_Amount,0) As InvoiceValue,
                Sg.DispName As Party, 
                H.PartyPinCode As PinCode, 
                S.Description As State, 
                TSg.DispName As Transporter,
                VDist.Distance As Distance, H.EInvoiceIRN As Irn, 
                Sit.RoadPermitNo As EwayBillNo, Sit.RoadPermitDate As EwayBillDate
                From LedgerHead H 
                LEFT JOIN LedgerHeadCharges Hc On H.DocId = Hc.DocId
                LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                LEFT JOIN City C On H.PartyCity = C.CityCode
                LEFT JOIN State S On C.State = S.Code
                LEFT JOIN SubGroup Sg On H.SubCode = Sg.SubCode
                LEFT JOIN SubGroup ShipTo On '' = ShipTo.SubCode
                LEFT JOIN City ShipToCity On ShipTo.CityCode = ShipToCity.CityCode
                LEFT JOIN State ShipToState On ShipToCity.State = ShipToState.Code
                LEFT JOIN SaleInvoiceTransport Sit On H.DocId = Sit.DocId
                LEFT JOIN (Select SubCode, Max(Transporter) Transporter 
                            From SubgroupSiteDivisionDetail
                            Group By SubCode) As Hlt On H.SubCode = Hlt.SubCode
                LEFT JOIN SubGroup TSg ON IsNull(Sit.Transporter,Hlt.Transporter) = TSg.SubCode
                LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration 
                            Where RegistrationType = 'Sales Tax No') As VReg On H.SubCode = VReg.SubCode
                LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration 
                            Where RegistrationType = 'Sales Tax No') As VTranReg On TSg.SubCode = VTranReg.SubCode
                LEFT JOIN (Select SubCode, Distance
                            From SubgroupSiteDivisionDetail 
                            Where Site_Code = '" & AgL.PubSiteCode & "' 
                            And Div_Code = '" & AgL.PubDivCode & "') As VDist On IsNull(H.SubCode,'') = VDist.SubCode " & mLedgerHeadCondStr


            DsHeader = AgL.FillData(mQry, AgL.GCn)


            mQry = " SELECT H.DocID, IfNull(I.HSN,Ic.HSN) As HSN
                    FROM SaleInvoice H 
                    LEFT JOIN SaleInvoiceTransport Sit On H.DocId = Sit.DocId
                    LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type
                    LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocID
                    LEFT JOIN Item I ON L.Item = I.Code 
                    LEFT JOIN ItemCategory Ic On I.ItemCategory = Ic.Code
                    " & mSaleCondStr &
                    " And IfNull(I.HSN,Ic.HSN) Is Null "
            Dim DtLine As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            For I As Integer = 0 To DsHeader.Tables(0).Rows.Count - 1
                If AgL.XNull(DsHeader.Tables(0).Rows(I)("Pincode")) = "" Then
                    If AgL.XNull(DsHeader.Tables(0).Rows(I)("Exception")) <> "" Then DsHeader.Tables(0).Rows(I)("Exception") += vbCrLf
                    DsHeader.Tables(0).Rows(I)("Exception") += "Party Pin Code is blank."
                End If
                If AgL.XNull(DsHeader.Tables(0).Rows(I)("Distance")) = "" Or AgL.VNull(DsHeader.Tables(0).Rows(I)("Distance")) = 0 Then
                    If AgL.XNull(DsHeader.Tables(0).Rows(I)("Exception")) <> "" Then DsHeader.Tables(0).Rows(I)("Exception") += vbCrLf
                    DsHeader.Tables(0).Rows(I)("Exception") += "Party Distance is blank."
                End If
                If AgL.XNull(DsHeader.Tables(0).Rows(I)("Pincode")) <> "" Then
                    If Not System.Text.RegularExpressions.Regex.IsMatch(AgL.XNull(DsHeader.Tables(0).Rows(I)("Pincode")), "^[0-9 ]+$") Then
                        If AgL.XNull(DsHeader.Tables(0).Rows(I)("Exception")) <> "" Then DsHeader.Tables(0).Rows(I)("Exception") += vbCrLf
                        DsHeader.Tables(0).Rows(I)("Exception") += "Party Pin Code is not valid."
                    End If
                End If

                Dim DtRowLineDetail_ForHeader As DataRow() = DtLine.Select(" DocId = " + AgL.Chk_Text(DsHeader.Tables(0).Rows(I)("SearchCode")))
                If DtRowLineDetail_ForHeader.Length > 0 Then
                    For M As Integer = 0 To DtRowLineDetail_ForHeader.Length - 1
                        If AgL.XNull(DtRowLineDetail_ForHeader(M)("HSN")) = "" Then
                            If AgL.XNull(DsHeader.Tables(0).Rows(I)("Exception")) <> "" Then DsHeader.Tables(0).Rows(I)("Exception") += vbCrLf
                            DsHeader.Tables(0).Rows(I)("Exception") += "Some items have blank HSN Codes."
                        End If
                    Next
                End If


                'If AgL.XNull(DsRep.Tables(0).Rows(I)("Exception")) <> "" Then
                '    DsRep.Tables(0).Rows(I)("Tick") = "o"
                'End If
            Next

            If DsHeader.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records To Print!")

            mQry = "Select 'Generate E-Invoice' As MenuText, 'CreateEInvoice' As FunctionName "
            mQry += "UNION ALL "
            mQry += "Select 'Generate EWay Bill' As MenuText, 'CreateEWayBill' As FunctionName "
            mQry += "UNION ALL "
            mQry += "Select 'Cancel E-Invoice' As MenuText, 'CancelEInvoice' As FunctionName "
            mQry += "UNION ALL "
            mQry += "Select 'Create Json File For E-Invoice' As MenuText, 'CreateJsonFileForEInvoice' As FunctionName "
            Dim DtMenuList As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)


            ReportFrm.Text = "E Invoice Report"
            ReportFrm.ClsRep = Me
            ReportFrm.ReportProcName = "ProcGenerateEInvoice"
            ReportFrm.DTCustomMenus = DtMenuList

            ReportFrm.ProcFillGrid(DsHeader)

            If mEInvoiceMode = mEInvoiceMode_Sandbox Then
                ReportFrm.DGL1.BackgroundColor = Color.Yellow
                ReportFrm.DGL1.ColumnHeadersDefaultCellStyle.BackColor = Color.Blue
                ReportFrm.Text = "Working On Testing Mode."
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            DsHeader = Nothing
        End Try
    End Sub
    Private Sub LoadEInvoiceSessionValues()
        FGetSellerDivisionSiteData(AgL.PubDivCode, AgL.PubSiteCode)

        mclient_id = FGetSettings(SettingFields.EInvoiceClientId, "E Invoice")
        mClient_Secret = FGetSettings(SettingFields.EInvoiceClientSecret, "E Invoice")
        mGspName = FGetSettings(SettingFields.EInvoiceGSPName, "E Invoice")
        mAspUserId = FGetSettings(SettingFields.EInvoiceAspUserId, "E Invoice")
        mAspPassword = FGetSettings(SettingFields.EInvoiceAspPassword, "E Invoice")

        mAuthGenerationURL = FGetSettings(SettingFields.AuthGenerationURL, "E Invoice")
        mIRNGenerationURL = FGetSettings(SettingFields.IRNGenerationURL, "E Invoice")
        mEWayBillGenerationURL = FGetSettings(SettingFields.EWayBillGenerationURL, "E Invoice")
        mSelectEWayBillURL = FGetSettings(SettingFields.SelectEWayBillURL, "E Invoice")
        mSaveEWayBillFileURL = FGetSettings(SettingFields.SaveEWayBillFileURL, "E Invoice")
        mIRNCancelURL = FGetSettings(SettingFields.IRNCancelURL, "E Invoice")

        'mAuthUrl = FGetSettings(SettingFields.EInvoiceAuthURL, "E Invoice")
        'mBaseUrl = FGetSettings(SettingFields.EInvoiceBaseURL, "E Invoice")
        'mEwbBaseUrl = FGetSettings(SettingFields.EInvoiceEWBURL, "E Invoice")
        'mCancelEWBUrl = FGetSettings(SettingFields.EInvoiceCancelEWBURL, "E Invoice")

        mUserName = AgL.XNull(DTDivisionSiteDetail.Rows(0)("DivisionSiteEInvoiceUserName"))
        mPassword = AgL.XNull(DTDivisionSiteDetail.Rows(0)("DivisionSiteEInvoicePassword"))
        mGstin = AgL.XNull(DTDivisionSiteDetail.Rows(0)("DivisionSiteSalesTaxNo"))


        If mEInvoiceMode = mEInvoiceMode_Sandbox Then
            mGspName = "TaxPro_Sandbox"
            mAspUserId = "1655233121"
            mAspPassword = "P@ssw0rd!"

            mAuthGenerationURL = "http://gstsandbox.charteredinfo.com/eivital/dec/v1.04/auth?&aspid=<AspUserId>&password=<AspPassword>&Gstin=<Gstin>&user_name=<EInvioceUserName>&eInvPwd=<EInviocePassword>"
            mIRNGenerationURL = "http://gstsandbox.charteredinfo.com/eicore/dec/v1.03/Invoice?&aspid=<AspUserId>&password=<AspPassword>&Gstin=<Gstin>&user_name=<EInvioceUserName>&&AuthToken=<AuthToken>&QrCodeSize=250"
            mEWayBillGenerationURL = "http://gstsandbox.charteredinfo.com/eiewb/dec/v1.03/ewaybill?&aspid=<AspUserId>&password=<AspPassword>&Gstin=<Gstin>&user_name=<EInvioceUserName>&AuthToken=<AuthToken>"
            mSelectEWayBillURL = "http://gstsandbox.charteredinfo.com/ewaybillapi/dec/v1.03/ewayapi?SelEInvSb&action=GetEwayBill&aspid=<AspUserId>&password=<AspPassword>&gstin=<Gstin>&ewbNo=<EWBNumber>&authtoken=<AuthToken>"
            mSaveEWayBillFileURL = "http://gstsandbox.charteredinfo.com/ewaybillapi/dec/v1.03/ewayapi?SelEInvSb&action=GetEwayBill&aspid=<AspUserId>&password=<AspPassword>&gstin=<Gstin>&ewbNo=<EWBNumber>&authtoken=<AuthToken>"
            mIRNCancelURL = "http://gstsandbox.charteredinfo.com/eicore/dec/v1.03/Invoice/Cancel?&aspid=<AspUserId>&password=<AspPassword>&Gstin=<Gstin>&User_Name=<EInvioceUserName>&eInvPwd=<EInviocePassword>&AuthToken=<AuthToken>"

            mUserName = "TaxProEnvPON"
            mPassword = "abc34*"
            mGstin = "34AACCC1596Q002"

            DTDivisionSiteDetail.Rows(0)("DivisionSiteSalesTaxNo") = "34AACCC1596Q002"
            DTDivisionSiteDetail.Rows(0)("DivisionStateCode") = "34"
            DTDivisionSiteDetail.Rows(0)("DivisionPinCode") = "605005"
        End If

        'mBaseUrl = "http://gstsandbox.charteredinfo.com/eivital/dec/v1.03/"


    End Sub
    Private Sub FGetSellerDivisionSiteData(bDiv_Code As String, bSite_Code As String)
        Dim mDivisionSiteSalesTaxNo As String = ""
        Dim DivisionSiteEInvoiceUserName As String = ""
        Dim DivisionSiteEInvoicePassword As String = ""

        mQry = " Select VReg.SalesTaxNo As DivisionSiteSalesTaxNo, '' As DivisionSiteEInvoiceUserName, 
                '' As DivisionSiteEInvoicePassword, Sg.DispName As DivisionName, 
                Case When IsNull(Sm.Add1,'') || IsNull(Sm.Add2,'') || IsNull(Sm.Add3,'') <> ''
                        Then IsNull(Sm.Add1,'') || IsNull(Sm.Add2,'') || IsNull(Sm.Add3,'')
                        Else Sg.Address END As DivisionAddress,
                Case When IsNull(Sm.Add1,'') || IsNull(Sm.Add2,'') || IsNull(Sm.Add3,'') <> ''
                        Then Sm.PinNo Else Sg.PIN END As DivisionPinCode, 
                Case When IsNull(Sm.Add1,'') || IsNull(Sm.Add2,'') || IsNull(Sm.Add3,'') <> ''
                        Then Sc.CityName Else C.CityName END As DivisionCityName,
                Case When IsNull(Sm.Add1,'') || IsNull(Sm.Add2,'') || IsNull(Sm.Add3,'') <> ''
                        Then SS.ManualCode Else S.ManualCode END As DivisionStateCode
                From Division D
                LEFT JOIN SubGroup Sg On D.SubCode = Sg.SubCode
                LEFT JOIN City C On Sg.CityCode = C.CityCode
                LEFT JOIN State S On C.State = S.Code
                LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration 
                            Where RegistrationType = 'Sales Tax No') As VReg On D.SubCode = VReg.SubCode
                LEFT JOIN SiteMast Sm ON 1=1
                LEFT JOIN City SC On Sm.City_Code = SC.CityCode
                LEFT JOIN State SS On SC.State = SS.Code
                Where D.Div_Code = '" & bDiv_Code & "'
                And Sm.Code = '" & bSite_Code & "'"
        DTDivisionSiteDetail = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If AgL.XNull(DTDivisionSiteDetail.Rows(0)("DivisionSiteSalesTaxNo")) = "" Then
            mDivisionSiteSalesTaxNo = ClsMain.FGetSettings(ClsMain.SettingFields.DivisionSiteSalesTaxNo, SettingType.General, bDiv_Code, bSite_Code, "", "", "", "", "")
            If mDivisionSiteSalesTaxNo = "" Then
                MsgBox("Company GST No. is blank.", MsgBoxStyle.Information)
                Exit Sub
            Else
                DTDivisionSiteDetail.Rows(0)("DivisionSiteSalesTaxNo") = mDivisionSiteSalesTaxNo
            End If
        End If

        DivisionSiteEInvoiceUserName = ClsMain.FGetSettings(ClsMain.SettingFields.DivisionSiteEInvoiceUserName, "E Invoice", bDiv_Code, bSite_Code, "", "", "", "", "")
        DivisionSiteEInvoicePassword = ClsMain.FGetSettings(ClsMain.SettingFields.DivisionSiteEInvoicePassword, "E Invoice", bDiv_Code, bSite_Code, "", "", "", "", "")

        If DivisionSiteEInvoiceUserName = "" Then
            MsgBox("Company E-Invoice User Name Is Blank.", MsgBoxStyle.Information)
            Exit Sub
        Else
            DTDivisionSiteDetail.Rows(0)("DivisionSiteEInvoiceUserName") = DivisionSiteEInvoiceUserName
        End If

        If DivisionSiteEInvoicePassword = "" Then
            MsgBox("Company E-Invoice Password Is Blank.", MsgBoxStyle.Information)
            Exit Sub
        Else
            DTDivisionSiteDetail.Rows(0)("DivisionSiteEInvoicePassword") = DivisionSiteEInvoicePassword
        End If
    End Sub
    Private Function AuthToken() As String
        Dim Result As String, url As String
        'url = mBaseUrl & "auth?&aspid=" & mAspUserId & "&password=" & mAspPassword & "&Gstin=" & mGstin & "&user_name=" & mUserName & "&eInvPwd=" & mPassword

        url = mAuthGenerationURL.Replace("<AspUserId>", mAspUserId).
            Replace("<AspPassword>", mAspPassword).
            Replace("<Gstin>", mGstin).
            Replace("<EInvioceUserName>", mUserName).
            Replace("<EInviocePassword>", mPassword)

        Result = WebRequest(url)
        Dim p As Object = JSON.parse(Result)
        mAuthToken = p.Item("Data").Item("AuthToken")

        AuthToken = mAuthToken
    End Function
    Public Sub CreateEInvoice(DGL As AgControls.AgDataGrid)
        Dim mSearchCode As String = ""
        Dim strIrn As String, strAckNo As String, strAckDate As String, strQrCodeImage As String
        Dim Result As String, url As String, strdata As String
        Dim I As Integer = 0
        Dim mInvoiceNo As String = ""
        Dim mMessage As String = ""

        Try
            For I = 0 To DGL.Rows.Count - 1
                If DGL.Item("Search Code", I).Value IsNot Nothing And DGL.Item("Search Code", I).Value <> "" Then
                    If AgL.XNull(DGL.Item("Irn", I).Value) IsNot Nothing And AgL.XNull(DGL.Item("Irn", I).Value) <> "" Then
                        MsgBox("IRN Generated Already For Invoice No." & DGL.Item("Invoice No", I).Value & ". Can't Generate Again.", MsgBoxStyle.Information)
                        Exit Sub
                    End If
                End If
            Next

            For I = 0 To DGL.Rows.Count - 1
                If DGL.Item("Search Code", I).Value IsNot Nothing And DGL.Item("Search Code", I).Value <> "" Then
                    mSearchCode = DGL.Item("Search Code", I).Value
                    mInvoiceNo = DGL.Item("Invoice No", I).Value

                    strdata = FGetJsonForIrn(mSearchCode)

                    'url = "http://testapi.taxprogsp.co.in/eicore/dec/v1.03/Invoice?&aspid=" & mAspUserId & "&password=" & mAspPassword & "&Gstin=" & mGstin & "&user_name=" & mUserName & "&&AuthToken=" & AuthToken() & "&QrCodeSize=250"

                    mAuthToken = AuthToken()
                    url = mIRNGenerationURL.Replace("<AspUserId>", mAspUserId).
                                Replace("<AspPassword>", mAspPassword).
                                Replace("<Gstin>", mGstin).
                                Replace("<EInvioceUserName>", mUserName).
                                Replace("<EInviocePassword>", mPassword).
                                Replace("<AuthToken>", mAuthToken)

                    Result = WebRequestbody(url, strdata)

                    Dim p As Object = JSON.parse(Result)


                    If p.Item("Status") = "0" Then
                        If p.Item("ErrorDetails")(1).Item("ErrorCode") <> "0" Then
                            mMessage += p.Item("ErrorDetails")(1).Item("ErrorCode") & " : " & p.Item("ErrorDetails")(1).Item("ErrorMessage") & ". Error In Invoice No." & mInvoiceNo & vbCrLf
                            Continue For
                        End If
                    End If

                    Dim sOutputJson As Object = p.Item("Data")
                    p = JSON.parse(sOutputJson)

                    strIrn = p.Item("Irn")
                    strAckNo = p.Item("AckNo")
                    strAckDate = p.Item("AckDt")
                    strQrCodeImage = p.Item("QrCodeImage")

                    Dim DestinationPath As String = PubAttachmentPath + mSearchCode + "\"
                    If Not Directory.Exists(DestinationPath) Then
                        Directory.CreateDirectory(DestinationPath)
                    End If

                    Dim mByte() As Byte = Convert.FromBase64String(strQrCodeImage)
                    System.IO.File.WriteAllBytes(DestinationPath + "EInvoiceQRCode.png", mByte)

                    If strIrn = "" Then
                        mMessage = " IRN not generated for Invoice No." & mInvoiceNo & vbCrLf
                        Exit Sub
                    Else
                        mQry = " UPDATE SaleInvoice Set EInvoiceIRN = " & AgL.Chk_Text(strIrn) & ",
                            EInvoiceACKNo = " & AgL.Chk_Text(strAckNo) & ",
                            EInvoiceACKDate = " & AgL.Chk_Date(strAckDate) & "
                            Where DocId = '" & mSearchCode & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = " UPDATE LedgerHead Set EInvoiceIRN = " & AgL.Chk_Text(strIrn) & ",
                            EInvoiceACKNo = " & AgL.Chk_Text(strAckNo) & ",
                            EInvoiceACKDate = " & AgL.Chk_Date(strAckDate) & "
                            Where DocId = '" & mSearchCode & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = "Insert Into TransactionReferences (DocID, ReferenceDocID, Type, Remark, IsEditingAllowed, IsDeletingAllowed) 
                            Values (" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(mSearchCode) & ", 'E Invoice',
                            'E-Invoice is created. To make changes in this invoice yoou have to first cancel E-invoice.', 0, 0) "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mMessage += " E-Invoice Generated Successfully For Invoice No." & mInvoiceNo & vbCrLf
                        ReportFrm.DGL1.DataSource = Nothing
                    End If
                End If
            Next

            MsgBox(mMessage, MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Public Sub CreateEWayBill(DGL As AgControls.AgDataGrid)
        Dim mSearchCode As String = ""
        Dim I As Integer = 0
        Dim mIrn As String = ""
        Dim strEWayBillNo As String = ""
        Dim strEWayBillDate As String = ""
        Dim Result As String, url As String, strdata As String
        Dim mInvoiceNo As String = ""
        Dim mMessage As String = ""

        Try


            'For I = 0 To DGL.Rows.Count - 1
            '    If DGL.Item("Search Code", I).Value IsNot Nothing And DGL.Item("Search Code", I).Value <> "" Then
            '        If AgL.XNull(DGL.Item("Eway Bill No", I).Value) IsNot Nothing And AgL.XNull(DGL.Item("Eway Bill No", I).Value) <> "" Then
            '            MsgBox("Eway Bill Generated Already For Invoice No." & DGL.Item("Invoice No", I).Value & ". Can't Generate Again.", MsgBoxStyle.Information)
            '            Exit Sub
            '        End If
            '    End If
            'Next



            For I = 0 To DGL.Rows.Count - 1
                If DGL.Item("Search Code", I).Value IsNot Nothing And DGL.Item("Search Code", I).Value <> "" Then
                    mSearchCode = DGL.Item("Search Code", I).Value
                    mInvoiceNo = DGL.Item("Invoice No", I).Value

                    mQry = "Select H.DocId, H.EInvoiceIRN As Irn, IfNull(VDist.Distance,0) As transDistance,
                TSg.DispName As TransporterName, VTranReg.SalesTaxNo As TransporterSalesTaxNo,
                Sit.LRNo As TransDocNo, IfNull(Sit.LRDate,H.V_Date) As TransDocDate,
                Case When H.ShipToParty Is Not Null Then 2 Else 1 End As TransType, Sit.VehicleNo
                From SaleInvoice H 
                LEFT JOIN SaleInvoiceTransport Sit On H.DocId = Sit.DocId
                LEFT JOIN (Select SubCode, Max(Transporter) Transporter 
                            From SubgroupSiteDivisionDetail
                            Group By SubCode) As Hlt On H.SaleToParty = Hlt.SubCode
                LEFT JOIN SubGroup TSg ON IfNull(Sit.Transporter,Hlt.Transporter) = TSg.SubCode
                LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                            From SubgroupRegistration 
                            Where RegistrationType = 'Sales Tax No') As VTranReg On TSg.SubCode = VTranReg.SubCode
                LEFT JOIN (Select SubCode, Distance
                            From SubgroupSiteDivisionDetail
                            Where Site_Code = '" & AgL.PubSiteCode & "'
                            And Div_Code = '" & AgL.PubDivCode & "' ) As VDist On IfNull(H.ShipToParty,H.SaleToParty) = VDist.SubCode 
                Where H.DocId = '" & mSearchCode & "'"
                    Dim DtSaleInvoice As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

                    strdata = ""
                    strdata += ControlChars.Tab + ControlChars.Tab + "{" & vbCrLf
                    strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Irn"" :  """ & AgL.XNull(DtSaleInvoice.Rows(0)("IRN")) & """," & vbCrLf
                    strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Distance"" :  " & AgL.XNull(DtSaleInvoice.Rows(0)("transDistance")) & "," & vbCrLf
                    'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Distance"" :  1932," & vbCrLf
                    strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """TransId"" :  """ & AgL.XNull(DtSaleInvoice.Rows(0)("TransporterSalesTaxNo")) & """," & vbCrLf
                    strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """TransName"" :  """ & AgL.XNull(DtSaleInvoice.Rows(0)("TransporterName")) & """" & vbCrLf

                    'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """DispDtls"": {" & vbCrLf
                    'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Nm"" :  ""ABC company pvt ltd""," & vbCrLf
                    'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Addr1"" :  ""7th block, kuvempu layout""," & vbCrLf
                    'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Addr2"" :  ""kuvempu layout""," & vbCrLf
                    'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Loc"" :  ""Banagalore""," & vbCrLf
                    'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Pin"" :  562160," & vbCrLf
                    'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Stcd"" :  ""29""" & vbCrLf
                    'strdata += ControlChars.Tab + ControlChars.Tab + "}" & vbCrLf
                    strdata += ControlChars.Tab + ControlChars.Tab + "}"

                    'url = "http://testapi.taxprogsp.co.in/eiewb/dec/v1.03/ewaybill?&aspid=" & mAspUserId & "&password=" & mAspPassword & "&Gstin=" & mGstin & "&user_name=" & mUserName & "&AuthToken=" & AuthToken()

                    mAuthToken = AuthToken()
                    url = mEWayBillGenerationURL.Replace("<AspUserId>", mAspUserId).
                            Replace("<AspPassword>", mAspPassword).
                            Replace("<Gstin>", mGstin).
                            Replace("<EInvioceUserName>", mUserName).
                            Replace("<EInviocePassword>", mPassword).
                            Replace("<AuthToken>", mAuthToken)


                    Result = WebRequestbody(url, strdata)
                    Dim p As Object = JSON.parse(Result)

                    If p.Item("Status") = "0" Then
                        If p.Item("ErrorDetails")(1).Item("ErrorCode") <> "0" Then
                            If p.Item("ErrorDetails")(1).Item("ErrorMessage") = "EwayBill is already generated for this IRN" Then
                                If MsgBox(p.Item("ErrorDetails")(1).Item("ErrorCode") & " : " & p.Item("ErrorDetails")(1).Item("ErrorMessage") & ". Error In Invoice No." & mInvoiceNo, MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                                    SaveEWayBillPrint(mSearchCode)
                                    Exit Sub
                                Else
                                    Exit Sub
                                End If
                            Else
                                mMessage += p.Item("ErrorDetails")(1).Item("ErrorCode") & " : " & p.Item("ErrorDetails")(1).Item("ErrorMessage") & ". Error In Invoice No." & mInvoiceNo & vbCrLf
                                Continue For
                            End If
                        End If
                    End If

                    'If p.Item("Status_cd") = "0" Or p.Item("status_cd") = "0" Then
                    '    If p.Item("error").Item("error_cd") = "GSP752" Then
                    '        MsgBox("Error Code: " & p.Item("error").Item("error_cd") & vbCrLf & p.Item("error").Item("message"), MsgBoxStyle.Information)
                    '        If MsgBox("Do you want to call AuthToken", vbQuestion + vbYesNo + vbDefaultButton2, "Message Box Title") = MsgBoxResult.Yes Then
                    '            Call AuthToken()
                    '            If p.Item("Status") = "0" Then
                    '                MsgBox(p.Item("ErrorDetails")(1).Item("ErrorMessage"))
                    '                Exit Sub
                    '            Else
                    '                If p.Item("status_cd") = "0" Then
                    '                    MsgBox(p.Item("error").Item("message"))
                    '                    Exit Sub
                    '                End If
                    '                If p.Item("Message") <> "" Then 'Error due to improper response from the server.
                    '                    MsgBox(p.Item("Message")) 'Result
                    '                    Exit Sub
                    '                End If
                    '            End If
                    '        Else
                    '            Exit Sub
                    '        End If
                    '    Else
                    '        MsgBox(p.Item("error").Item("message"))
                    '        Exit Sub
                    '    End If
                    'End If

                    'If p.Item("Message") <> "" Then 'Error due to improper response from the server.
                    '    MsgBox(p.Item("Message"))
                    '    Exit Sub
                    'End If

                    'If p.Item("Status") = "0" Then
                    '    If p.Item("ErrorDetails")(1).Item("ErrorCode") <> "0" Then
                    '        MsgBox(p.Item("ErrorDetails")(1).Item("ErrorCode") & " : " & p.Item("ErrorDetails")(1).Item("ErrorMessage"), MsgBoxStyle.Information)
                    '        Exit Sub
                    '    Else
                    '        If p.Item("ErrorDetails")(1).Item("ErrorCode") <> "0" Then
                    '            MsgBox(p.Item("ErrorDetails")(1).Item("ErrorCode") & " : " & p.Item("ErrorDetails")(1).Item("ErrorMessage"), MsgBoxStyle.Information)
                    '            Exit Sub
                    '        End If
                    '    End If
                    'End If

                    Dim sOutputJson As Object = p.Item("Data")
                    p = JSON.parse(sOutputJson)



                    strEWayBillNo = p.Item("EwbNo")
                    strEWayBillDate = p.Item("EwbDt")

                    If strEWayBillNo = "" Then
                        mMessage = " EWay Bill not generated for Invoice No." & mInvoiceNo & vbCrLf
                        Exit Sub
                    Else
                        mQry = " UPDATE SaleInvoiceTransport Set 
                            RoadPermitNo = " & AgL.Chk_Text(strEWayBillNo) & ",
                            RoadPermitDate = " & AgL.Chk_Date(strEWayBillDate) & "
                            Where DocId = '" & mSearchCode & "'"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        SaveEWayBillPrint(mSearchCode)

                        mMessage += "EWay Bill Generated Successfully For Invoice No." & mInvoiceNo & vbCrLf
                        ReportFrm.DGL1.DataSource = Nothing
                    End If
                End If
            Next
            MsgBox(mMessage, MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub SaveEWayBillPrint(mDocId As String)
        Dim Result As String
        Dim url As String
        Dim EWBNumber As String
        Dim p As Object
        Dim FileName As String

        EWBNumber = AgL.XNull(AgL.Dman_Execute(" Select RoadPermitNo From SaleInvoiceTransport Where DocId = '" & mDocId & "'", AgL.GCn).ExecuteScalar())

        Dim DestinationPath As String = PubAttachmentPath + mDocId + "\"
        If Not Directory.Exists(DestinationPath) Then
            Directory.CreateDirectory(DestinationPath)
        End If

        FileName = DestinationPath & "\EwayBill - " & EWBNumber & ".pdf"   'Hold OutPut File
        'url = "http://testapi.taxprogsp.co.in/ewaybillapi/dec/v1.03/ewayapi?SelEInvSb&action=GetEwayBill&aspid=" & mAspUserId & "&password=" & mAspPassword & "&gstin=" & mGstin & "&ewbNo=" & EWBNumber & "&authtoken=" & AuthToken()

        mAuthToken = AuthToken()

        url = "https://einvapi.charteredinfo.com/v1.03/dec/ewayapi?action=GetEwayBill&aspid=1655233121&password=P@ssw0rd!&gstin=24AAXCS4102R1ZP&username=API_SSFA@1960&authtoken=" & mAuthToken & "&ewbNo=611285269158"

        url = mSelectEWayBillURL.Replace("<AspUserId>", mAspUserId).
                            Replace("<AspPassword>", mAspPassword).
                            Replace("<Gstin>", mGstin).
                            Replace("<EInvioceUserName>", mUserName).
                            Replace("<EInviocePassword>", mPassword).
                            Replace("<AuthToken>", mAuthToken).
                            Replace("<EWBNumber>", EWBNumber)

        Result = WebRequest(url)   'Hold Output of first APIcall
        p = JSON.parse(Result)

        'If p.Item("Status") = "0" Then
        '    If p.Item("ErrorDetails")(1).Item("ErrorCode") <> "0" Then
        '        MsgBox(p.Item("ErrorDetails")(1).Item("ErrorCode") & " : " & p.Item("ErrorDetails")(1).Item("ErrorMessage"), MsgBoxStyle.Information)
        '        Exit Sub
        '    End If
        'End If


        If InStr(1, Result, EWBNumber, 1) <> 0 Then 'Cheicking Valid EwayBill No Found in Result then
            url = "https://einvapi.charteredinfo.com/aspapi/v1.0/printdetailewb?&aspid=" & mAspUserId & "&password=" & mAspPassword & "&gstin=" & mGstin


            url = mSaveEWayBillFileURL.Replace("<AspUserId>", mAspUserId).
                            Replace("<AspPassword>", mAspPassword).
                            Replace("<Gstin>", mGstin).
                            Replace("<EInvioceUserName>", mUserName).
                            Replace("<EInviocePassword>", mPassword).
                            Replace("<AuthToken>", mAuthToken).
                            Replace("<EWBNumber>", EWBNumber)


            Dim http
            http = CreateObject("WinHttp.WinHttpRequest.5.1")
            http.Open("POST", url, False)
            http.SetRequestHeader("Content-Type", "application/json; charset=utf-8")
            http.Send(Result)   ' Send Output of  First API  to second
            'ResponseBody = WebRequestbody(url, Result)
            Dim BinaryStream
            BinaryStream = CreateObject("ADODB.Stream")
            BinaryStream.Type = 1
            BinaryStream.Open
            BinaryStream.Write(http.ResponseBody)
            BinaryStream.SaveToFile(FileName, 2)
        End If
    End Sub
    Public Sub CancelEInvoice(DGL As AgControls.AgDataGrid)
        Dim mSearchCode As String = ""
        Dim strIrn As String = ""
        Dim strData As String, url As String, Result As String
        Dim I As Integer = 0
        Dim mInvoiceNo As String = ""
        Dim mMessage As String = ""

        If MsgBox("Do you want to cancel these E-Invoices ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            For I = 0 To DGL.Rows.Count - 1
                If DGL.Item("Search Code", I).Value IsNot Nothing And DGL.Item("Search Code", I).Value <> "" Then
                    mSearchCode = DGL.Item("Search Code", I).Value

                    mQry = " Select H.DocId, H.EInvoiceIrn As Irn From SaleInvoice H Where H.DocId = '" & mSearchCode & "'"
                    Dim DtSaleInvoice As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

                    Dim StrSample As String
                    StrSample = "{" & vbCrLf &
                            "'Irn':'" & AgL.XNull(DtSaleInvoice.Rows(I)("IRN")) & " '," & vbCrLf &
                            "'CnlRsn':'1'," & vbCrLf &
                            "'CnlRem':'Wrong entry'" & vbCrLf &
                            "}"

                    strData = StrSample
                    strData = Replace(strData, "'", """")

                    'Production url = "https://api.taxprogsp.co.in/eicore/dec/v1.03/Invoice/Cancel?"
                    'url = "http://testapi.taxprogsp.co.in/eicore/dec/v1.03/Invoice/Cancel?&aspid=" & ASPID & "&password=" & ASPPassword & "&Gstin=34AACCC1596Q002&User_Name=TaxProEnvPON&eInvPwd=abc34*&AuthToken=" & TxtBoxAuthToken.Text
                    'url = TxtBBaseURL2.Text & "Invoice/Cancel?&aspid=" & ASPID & "&password=" & ASPPassword & "&gstin=" & GSTIN & "&User_name=" & eInvUserName & "&eInvPwd=" & eInvPwd & "&AuthToken=" & TxtBoxAuthToken.Text


                    mAuthToken = AuthToken()
                    url = mIRNCancelURL.Replace("<AspUserId>", mAspUserId).
                            Replace("<AspPassword>", mAspPassword).
                            Replace("<Gstin>", mGstin).
                            Replace("<EInvioceUserName>", mUserName).
                            Replace("<EInviocePassword>", mPassword).
                            Replace("<AuthToken>", mAuthToken)

                    Result = WebRequestbody(url, strData)
                    Dim p As Object = JSON.parse(Result)
                    If p.Item("Status") = "0" Then
                        If p.Item("ErrorDetails")(1).Item("ErrorCode") <> "0" Then
                            mMessage = p.Item("ErrorDetails")(1).Item("ErrorCode") & " : " & p.Item("ErrorDetails")(1).Item("ErrorMessage")
                            Continue For
                        End If
                    End If

                    p = JSON.parse(Result)
                    Dim sOutputJson As Object = p.Item("Data")
                    p = JSON.parse(sOutputJson)

                    mQry = " Update SaleInvoice Set EInvoiceIRN = Null, 
                            EInvoiceACKNo = Null, EInvoiceACKDate = Null 
                            Where DocId = '" & mSearchCode & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mQry = " Delete From TransactionReferences Where DocId = '" & mSearchCode & "'
                            And Type = 'E Invoice'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                    mMessage += "Irn No: " & p.Item("Irn") & vbNewLine & "CancelDate : " & p.Item("CancelDate") & vbCrLf
                End If
            Next

            MsgBox(mMessage, MsgBoxStyle.Information)
        End If
    End Sub
    Private Function FGetJsonForIrn(mSearchCode As String) As String
        Dim strdata As String = ""
        Dim strRemark As String = ""
        If AgL.PubServerName = "" Then
            strRemark = "substr(IsNull(L.Remarks,L.Remarks),0,300)"
        Else
            strRemark = "Substring(IsNull(L.Remarks,L.Remarks),0,300)"
        End If

        mQry = "SELECT H.Div_Code, H.Site_Code, H.V_Type AS InvoiceType, 
                        '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo As  InvoiceNo, 
                        H.V_Date AS InvoiceDate, Vt.NCat,
                        H.SaleToPartySalesTaxNo, H.SaleToPartyName, H.SaleToPartyAddress,
                        C.CityName AS SaleToPartyCityName, H.SaleToPartyPinCode, S.ManualCode AS SaleToPartyStateCode,
                        VShipToPartyReg.SalesTaxNo As ShipToPartySalesTaxNo, ShipParty.Name AS ShipToPartyName, ShipParty.Address AS ShipToPartyAddress, ShipCity.CityName AS ShipToPartyCity, 
                        ShipParty.Pin AS ShipToPartyPinCode, ShipState.ManualCode AS ShipToPartyStateCode,
                        CASE WHEN I.ItemType = 'SP' THEN 'Y' ELSE 'N' END AS IsService, I.Description AS ItemDesc,
                        IsNull(I.HSN, Ic.HSN) AS HSN, 
                        L.Qty, L.Unit, 
                        Round((L.Taxable_Amount + (L.DiscountAmount + L.AdditionalDiscountAmount)) / L.Qty,2) As Rate, 
                        L.Taxable_Amount + (L.DiscountAmount + L.AdditionalDiscountAmount) AS Amount, 
                        L.DiscountAmount + L.AdditionalDiscountAmount  As DiscountAmount, 
                        L.Taxable_Amount, Sti.GrossTaxRate,
                        L.Tax1, L.Tax2, L.Tax3, L.Tax4, L.Tax5, L.Net_Amount,
                        H.Taxable_Amount AS Header_Taxable_Amount, H.Tax1 AS Header_Tax1, H.Tax2 AS Header_Tax2, 
                        H.Tax3 AS Header_Tax3, H.Tax4 AS Header_Tax4, H.Tax5 AS Header_Tax5,
                        H.Round_Off AS Header_Round_Off, H.Net_Amount AS Header_Net_Amount
                        FROM SaleInvoice H 
                        Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type
                        LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                                    From SubgroupRegistration 
                                    Where RegistrationType = 'Sales Tax No') As VReg On H.SaleToParty = VReg.SubCode
                        LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                                    From SubgroupRegistration 
                                    Where RegistrationType = 'Sales Tax No') As VShipToPartyReg On H.ShipToParty = VShipToPartyReg.SubCode
                        LEFT JOIN City C ON H.SaleToPartyCity = C.CityCode
                        LEFT JOIN State S ON C.State = S.Code
                        LEFT JOIN Subgroup ShipParty ON H.ShipToParty = ShipParty.SubCode
                        LEFT JOIN City ShipCity ON ShipParty.CityCode = ShipCity.CityCode
                        LEFT JOIN State ShipState ON ShipCity.State = ShipState.Code
                        LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocId
                        LEFT JOIN Item I ON L.Item = I.Code
                        LEFT JOIN Item Ic ON I.ItemCategory = Ic.Code
                        LEFT JOIN PostingGroupSalesTaxItem Sti ON L.SalesTaxGroupItem = Sti.Description
                        WHERE H.DocID = '" & mSearchCode & "'"

        mQry = mQry + " UNION ALL "
        mQry = mQry + " SELECT H.Div_Code, H.Site_Code, H.V_Type AS InvoiceType, 
                        '" & IIf(AgL.PubPrintDivisionShortNameOnDocumentsYn, AgL.PubDivShortName, "") & IIf(AgL.PubPrintSiteShortNameOnDocumentsYn, AgL.PubSiteShortName, "") & "' || (Case When VT.Short_Name Is Not Null Then VT.Short_Name Else '' End) || H.ManualRefNo As  InvoiceNo, 
                        H.V_Date AS InvoiceDate, Vt.NCat,
                        H.PartySalesTaxNo, H.PartyName AS SaleToPartyName, H.PartyAddress AS SaleToPartyAddress,
                        C.CityName AS SaleToPartyCityName, H.PartyPinCode, S.ManualCode AS SaleToPartyStateCode,
                        VShipToPartyReg.SalesTaxNo As ShipToPartySalesTaxNo, ShipParty.Name AS ShipToPartyName, ShipParty.Address AS ShipToPartyAddress, ShipCity.CityName AS ShipToPartyCity, 
                        ShipParty.Pin AS ShipToPartyPinCode, ShipState.ManualCode AS ShipToPartyStateCode,
                        CASE WHEN I.ItemType = 'SP' THEN 'Y' ELSE 'N' END AS IsService,  " + strRemark + " AS ItemDesc,
                        L.HSN AS HSN, 
                        1 AS Qty, IfNull(L.Unit,'Nos') As Unit, 
                        Lc.Taxable_Amount AS Rate, 
                        Lc.Taxable_Amount AS Amount, 
                        0 As DiscountAmount, 
                        Lc.Taxable_Amount, Sti.GrossTaxRate,
                        Lc.Tax1, Lc.Tax2, Lc.Tax3, Lc.Tax4, Lc.Tax5, Lc.Net_Amount,
                        Hc.Taxable_Amount AS Header_Taxable_Amount, Hc.Tax1 AS Header_Tax1, Hc.Tax2 AS Header_Tax2, 
                        Hc.Tax3 AS Header_Tax3, Hc.Tax4 AS Header_Tax4, Hc.Tax5 AS Header_Tax5,
                        Hc.Round_Off AS Header_Round_Off, Hc.Net_Amount AS Header_Net_Amount
                        FROM LedgerHead H 
                        LEFT JOIN LedgerHeadCharges Hc ON H.DocID = Hc.DocID
                        Left Join Voucher_Type Vt  With (NoLock) On H.V_Type = Vt.V_Type
                        LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                                    From SubgroupRegistration 
                                    Where RegistrationType = 'Sales Tax No') As VReg On H.SubCode = VReg.SubCode
                        LEFT JOIN (Select Subcode, RegistrationNo As SalesTaxNo
                                    From SubgroupRegistration 
                                    Where RegistrationType = 'Sales Tax No') As VShipToPartyReg On '' = VShipToPartyReg.SubCode
                        LEFT JOIN City C ON H.PartyCity = C.CityCode
                        LEFT JOIN State S ON C.State = S.Code
                        LEFT JOIN Subgroup ShipParty ON '' = ShipParty.SubCode
                        LEFT JOIN City ShipCity ON ShipParty.CityCode = ShipCity.CityCode
                        LEFT JOIN State ShipState ON ShipCity.State = ShipState.Code
                        LEFT JOIN LedgerHeadDetail L ON H.DocID = L.DocId
                        LEFT JOIN LedgerHeadDetailCharges Lc ON L.DocId = Lc.DocId AND L.Sr = Lc.Sr
                        LEFT JOIN Item I ON '' = I.Code
                        LEFT JOIN Item Ic ON I.ItemCategory = Ic.Code
                        LEFT JOIN PostingGroupSalesTaxItem Sti ON L.SalesTaxGroupItem = Sti.Description
                        WHERE H.DocID = '" & mSearchCode & "'"
        Dim DtSaleInvoice As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        strdata = ""
        strdata += ControlChars.Tab + ControlChars.Tab + "{" & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Version"": ""1.1"", " & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """TranDtls"": {" & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """TaxSch"" :  ""GST""," & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """SupTyp"" :  ""B2B""," & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """RegRev""  : ""N""," & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """EcmGstin"" : null," & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """IgstOnIntra"":  ""N""" & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "}," & vbCrLf

        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """DocDtls"": {" & vbCrLf
        If AgL.XNull(DtSaleInvoice.Rows(0)("NCat")) = Ncat.DebitNoteCustomer Then
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Typ"":   ""DBN""," & vbCrLf
        ElseIf AgL.XNull(DtSaleInvoice.Rows(0)("NCat")) = Ncat.SaleReturn Or AgL.XNull(DtSaleInvoice.Rows(0)("NCat")) = Ncat.CreditNoteCustomer Then
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Typ"":   ""CRN""," & vbCrLf
        Else
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Typ"":   ""INV""," & vbCrLf
        End If
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """No"":  """ & AgL.XNull(DtSaleInvoice.Rows(0)("InvoiceNo")) & """," & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Dt"": """ & CDate(AgL.XNull(DtSaleInvoice.Rows(0)("InvoiceDate"))).ToString("dd/MM/yyyy") & """" & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "}," & vbCrLf

        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """SellerDtls"": {" & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Gstin"" :  """ & AgL.XNull(DTDivisionSiteDetail.Rows(0)("DivisionSiteSalesTaxNo")) & """," & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """LglNm"" :  """ & AgL.XNull(DTDivisionSiteDetail.Rows(0)("DivisionName")) & """," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """TrdNm"" :  """"," & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Addr1"" :  """ & AgL.XNull(DTDivisionSiteDetail.Rows(0)("DivisionAddress")).ToString.Replace("\", "").Replace(vbCrLf, "") & """," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Addr2"" :  """"," & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Loc"" :  """ & AgL.XNull(DTDivisionSiteDetail.Rows(0)("DivisionCityName")) & """," & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Pin"" :  " & AgL.XNull(DTDivisionSiteDetail.Rows(0)("DivisionPinCode")) & "," & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Stcd"" :  """ & AgL.XNull(DTDivisionSiteDetail.Rows(0)("DivisionStateCode")) & """" & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Ph"" :  """"" & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Em"": """"" & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "}," & vbCrLf

        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """BuyerDtls"":  {" & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Gstin"":   """ & IIf(AgL.XNull(DtSaleInvoice.Rows(0)("SaleToPartySalesTaxNo")) = "", "URP", AgL.XNull(DtSaleInvoice.Rows(0)("SaleToPartySalesTaxNo"))) & """," & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """LglNm"":   """ & AgL.XNull(DtSaleInvoice.Rows(0)("SaleToPartyName")) & """," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """TrdNm"":   """"," & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Pos"":   """ & AgL.XNull(DtSaleInvoice.Rows(0)("SaleToPartyStateCode")) & """," & vbCrLf
        If AgL.XNull(DtSaleInvoice.Rows(0)("SaleToPartyAddress")) <> "" Then
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Addr1"":   """ & AgL.XNull(DtSaleInvoice.Rows(0)("SaleToPartyAddress")).ToString.Replace("\", "").Replace(vbCrLf, "") & """," & vbCrLf
        Else
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Addr1"":   """ & AgL.XNull(DtSaleInvoice.Rows(0)("SaleToPartyCityName")).ToString.Replace("\", "").Replace(vbCrLf, "") & """," & vbCrLf
        End If
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Addr2"":   """"," & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Loc"":   """ & AgL.XNull(DtSaleInvoice.Rows(0)("SaleToPartyCityName")) & """," & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Pin"":   " & AgL.XNull(DtSaleInvoice.Rows(0)("SaleToPartyPinCode")) & "," & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Stcd"":   """ & AgL.XNull(DtSaleInvoice.Rows(0)("SaleToPartyStateCode")) & """" & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Ph"":   """"" & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Em"": """"" & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "}," & vbCrLf



        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """DispDtls"": {" & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Nm"" :  ""ABC company pvt ltd""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Addr1"" :  ""7th block, kuvempu layout""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Addr2"" :  ""kuvempu layout""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Loc"" :  ""Banagalore""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Pin"" :  562160," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Stcd"" : ""29""" & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "}," & vbCrLf



        If AgL.XNull(DtSaleInvoice.Rows(0)("ShipToPartyName")) <> "" Then
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """ShipDtls"": {" & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Gstin"" :  """ & AgL.XNull(DtSaleInvoice.Rows(0)("ShipToPartySalesTaxNo")) & """," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """LglNm"" :  """ & AgL.XNull(DtSaleInvoice.Rows(0)("ShipToPartyName")) & """," & vbCrLf
            'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """TrdNm"" :  """"," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Addr1"" :  """ & AgL.XNull(DtSaleInvoice.Rows(0)("ShipToPartyAddress")).ToString.Replace("\", "").Replace(vbCrLf, "") & """," & vbCrLf
            'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Addr2"" :  """"," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Loc""  : """ & AgL.XNull(DtSaleInvoice.Rows(0)("ShipToPartyCity")) & """," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Pin"" :  " & AgL.VNull(DtSaleInvoice.Rows(0)("ShipToPartyPinCode")) & "," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Stcd"": """ & AgL.XNull(DtSaleInvoice.Rows(0)("ShipToPartyStateCode")) & """" & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "}," & vbCrLf
        End If

        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """ItemList"": [" & vbCrLf
        For J As Integer = 0 To DtSaleInvoice.Rows.Count - 1
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "{" & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """SlNo"" :  """ & J + 1 & """," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """PrdDesc"" :  """ & AgL.XNull(DtSaleInvoice.Rows(J)("ItemDesc")) & """," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """IsServc""  : """ & AgL.XNull(DtSaleInvoice.Rows(J)("IsService")) & """," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """HsnCd"" :  """ & AgL.XNull(DtSaleInvoice.Rows(J)("HSN")) & """," & vbCrLf
            'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Barcde""  : """"," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Qty"" :  " & Math.Abs(AgL.VNull(DtSaleInvoice.Rows(J)("Qty"))) & "," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """FreeQty"":   0," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Unit"" :  """ & AgL.XNull(DtSaleInvoice.Rows(J)("Unit")) & """," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """UnitPrice"" :  " & Math.Abs(AgL.VNull(DtSaleInvoice.Rows(J)("Rate"))) & "," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """TotAmt"" :  " & Math.Abs(AgL.VNull(DtSaleInvoice.Rows(J)("Amount"))) & "," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Discount"" :  " & AgL.VNull(DtSaleInvoice.Rows(J)("DiscountAmount")) & "," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """PreTaxVal"":   0," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """AssAmt"" :  " & Math.Abs(AgL.VNull(DtSaleInvoice.Rows(J)("Taxable_Amount"))) & "," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """GstRt"" :  " & Math.Abs(AgL.VNull(DtSaleInvoice.Rows(J)("GrossTaxRate"))) & "," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """IgstAmt"" :  " & Math.Abs(AgL.VNull(DtSaleInvoice.Rows(J)("Tax1"))) & "," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """CgstAmt"" :  " & Math.Abs(AgL.VNull(DtSaleInvoice.Rows(J)("Tax2"))) & "," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """SgstAmt"" :  " & Math.Abs(AgL.VNull(DtSaleInvoice.Rows(J)("Tax3"))) & "," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """CesRt"" :  0," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """CesAmt"" :  0," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """CesNonAdvlAmt"" :  0," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """StateCesRt"" :  0," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """StateCesAmt"" :  0," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """StateCesNonAdvlAmt"" :  0," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """OthChrg"" :  0," & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """TotItemVal"" :  " & Math.Abs(AgL.VNull(DtSaleInvoice.Rows(J)("Net_Amount"))) & "" & vbCrLf
            'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """OrdLineRef"" :  ""3256""," & vbCrLf
            'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """OrgCntry"" :  ""AG""," & vbCrLf
            'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """PrdSlNo"" :  ""12345""," & vbCrLf

            'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """BchDtls"" :  {" & vbCrLf
            'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Nm"" :  ""123456""," & vbCrLf
            'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Expdt""  : ""01/08/2020""," & vbCrLf
            'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """wrDt"" :  ""01/09/2020""" & vbCrLf
            'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "}," & vbCrLf

            'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """AttribDtls"": [" & vbCrLf
            'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "{" & vbCrLf
            'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Nm"" :  ""Rice""," & vbCrLf
            'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Val"": ""10000""" & vbCrLf
            'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "}" & vbCrLf

            'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "]" & vbCrLf
            strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "}" & IIf(J < DtSaleInvoice.Rows.Count - 1, ",", "") & vbCrLf
        Next
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "]," & vbCrLf


        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """ValDtls"" :  {" & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """AssVal""  : " & Math.Abs(AgL.VNull(DtSaleInvoice.Rows(0)("Header_Taxable_Amount"))) & "," & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """CgstVal"" :  " & Math.Abs(AgL.VNull(DtSaleInvoice.Rows(0)("Header_Tax2"))) & "," & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """SgstVal""  : " & Math.Abs(AgL.VNull(DtSaleInvoice.Rows(0)("Header_Tax3"))) & "," & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """IgstVal"" :  " & Math.Abs(AgL.VNull(DtSaleInvoice.Rows(0)("Header_Tax1"))) & "," & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """CesVal""  : 0," & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """StCesVal""  : 0," & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Discount"" :  0," & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """OthChrg""  : 0," & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """RndOffAmt""  : " & AgL.VNull(DtSaleInvoice.Rows(0)("Header_Round_Off")) & "," & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """TotInvVal""  : " & Math.Abs(AgL.VNull(DtSaleInvoice.Rows(0)("Header_Net_Amount"))) & "," & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """TotInvValFc"": 0" & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "}" & vbCrLf


        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "}," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """PayDtls"" :  {" & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Nm"" :  ""ABCDE""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Accdet"" :  ""5697389713210""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Mode"" :  ""Cash""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Fininsbr"" :  ""SBIN11000""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Payterm"" :  ""100""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Payinstr"" :  ""Gift""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Crtrn"" :  ""test""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Dirdr"" :  ""test""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Crday"" :  100," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Paidamt"" :  10000," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Paymtdue"" :  5000" & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "}," & vbCrLf

        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """RefDtls"" :  {" & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """InvRm"":   ""TEST""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """DocPerdDtls"": {" & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """InvStDt"":   ""01/08/2020""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """InvEndDt"" : ""01/09/2020""" & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "}," & vbCrLf

        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """PrecDocDtls"": [" & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "{" & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """InvNo"":   ""DOC/002""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """InvDt"":   ""01/08/2020""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """OthRefNo"": ""123456""" & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "}" & vbCrLf

        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "]," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """ContrDtls"" :  [" & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "{" & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """RecAdvRefr"" :  ""Doc/003""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """RecAdvDt"" :  ""01/08/2020""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Tendrefr"" :  ""Abc001""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Contrrefr"" :  ""Co123""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Extrefr"" :  ""Yo456""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Projrefr"" :  ""Doc-456""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Porefr"" :  ""Doc-789""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """PoRefDt"" :  ""01/08/2020""" & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "}" & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "]" & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "}," & vbCrLf

        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """AddlDocDtls"" :  [" & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "{" & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Url"" :  ""https://einv-apisandbox.nic.in""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Docs"" :  ""Test Doc""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Info"" :  ""Document Test""" & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "}" & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "]," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """ExpDtls"": {" & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """ShipBNo"" :  ""A-248""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """ShipBDt"" :  ""01/08/2020""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """Port"" :  ""INABG1""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """RefClm"" :  ""N""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """ForCur"" :  ""AED""," & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + """CntCode"" :  ""AE""" & vbCrLf
        'strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "}" & vbCrLf
        strdata += ControlChars.Tab + ControlChars.Tab + ControlChars.Tab + "}"

        FGetJsonForIrn = strdata
    End Function
    Public Sub CreateJsonFileForEInvoice(DGL As AgControls.AgDataGrid)
        Dim mSearchCode As String = ""
        Dim I As Integer = 0
        Dim strdata As String

        Dim FilePath As String = ""
        Dim SaveFileDialogBox As SaveFileDialog
        Dim sFilePath As String = ""
        SaveFileDialogBox = New SaveFileDialog
        SaveFileDialogBox.Title = "File Name"
        FilePath = My.Computer.FileSystem.SpecialDirectories.Desktop
        SaveFileDialogBox.InitialDirectory = FilePath
        SaveFileDialogBox.FilterIndex = 1

        Dim mFileName As String = ""
        If AgL.XNull(ReportFrm.FilterGrid.Item(GFilter, rowFromDate).Value) = AgL.XNull(ReportFrm.FilterGrid.Item(GFilter, rowToDate).Value) Then
            mFileName = "JsonForEInvoice_" & AgL.XNull(ReportFrm.FilterGrid.Item(GFilter, rowFromDate).Value).ToString.Replace("/", "") & ".json"
        Else
            mFileName = "JsonForEInvoice_" & AgL.XNull(ReportFrm.FilterGrid.Item(GFilter, rowFromDate).Value).ToString.Replace("/", "") & "_To_" & AgL.XNull(ReportFrm.FilterGrid.Item(GFilter, rowToDate).Value).ToString.Replace("/", "") & ".json"
        End If

        SaveFileDialogBox.FileName = mFileName & ".json"
        If SaveFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Sub
        sFilePath = SaveFileDialogBox.FileName


        strdata = ""
        For I = 0 To DGL.Rows.Count - 1
            If DGL.Item("Search Code", I).Value IsNot Nothing And DGL.Item("Search Code", I).Value <> "" Then
                mSearchCode = DGL.Item("Search Code", I).Value
                If strdata <> "" Then strdata = strdata + "," + vbCrLf
                strdata += FGetJsonForIrn(mSearchCode)
            End If
        Next

        Dim fileExists As Boolean = File.Exists(sFilePath)
        If fileExists Then File.Delete(sFilePath)
        Dim StringTabPresses As String = ""
        Using sw As New StreamWriter(File.Open(sFilePath, FileMode.OpenOrCreate))
            sw.Write(strdata)
        End Using
    End Sub












End Class
