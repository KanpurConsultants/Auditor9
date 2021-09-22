Imports System.Drawing.Printing
Imports System.IO
Imports System.Linq
Imports System.Net
Imports Excel

Public Class FrmExportDataFromSqlServer
    Dim AgL As AgLibrary.ClsMain
    Dim mConnectionStr As String = "", mQry As String

    Private Const Party As String = "Party"
    Private Const Item As String = "Item"
    Private Const ItemRateList As String = "Item Rate List"
    Private Const Sale1 As String = "Sale1"
    Private Const Sale2 As String = "Sale2"
    Private Const Sale3 As String = "Sale3"
    Private Const Purch1 As String = "Purch1"
    Private Const Purch2 As String = "Purch2"
    Private Const Purch3 As String = "Purch3"
    Private Const Ledger As String = "Ledger"
    Private Const LedgerHead As String = "LedgerHead"
    Private Const LedgerHeadDetail As String = "LedgerHeadDetail"
    Private Const BuiltyHead As String = "BuiltyHead"
    Private Const BuiltyHeadDetail As String = "BuiltyHeadDetail"

    Private Const FromSoftware_Dataman As String = "Dataman"
    Private Const FromSoftware_Monark As String = "Monark"

    Public Sub New(ByVal AgLibVar As AgLibrary.ClsMain)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        AgL = AgLibVar
    End Sub
    Private Sub FrmReportPrint_Load(sender As Object, e As EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 654, 990, 0, 0)
        'Me.Location = New System.Drawing.Point(0, 0)
    End Sub
    Private Sub FrmReportPrint_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSelectFile.Click, BtnOK.Click, BtnCancel.Click
        Dim MyCommand As OleDb.OleDbDataAdapter = Nothing
        Select Case sender.name
            Case BtnSelectFile.Name
                If (FBDExportPath.ShowDialog() = DialogResult.OK) Then
                    TxtExportPath.Text = FBDExportPath.SelectedPath
                End If

            Case BtnOK.Name
                mConnectionStr = "Server=" & TxtServerName.Text & ";Database=" & TxtDatabaseName.Text & ";User Id=" & TxtUserName.Text & ";Password=" & TxtPassword.Text & ""

                If TxtExportSpecific.Text <> "" Then
                    FExportSpecificFile()
                Else
                    Select Case TxtFromSoftware.Text
                        Case FromSoftware_Dataman
                            FExportPartyData()
                            FExportItemData()
                            FExportItemRateListData()
                            FExportSale1Data()
                            FExportSale2Data()
                            FExportSale3Data()
                            FExportPurch1Data()
                            FExportPurch2Data()
                            FExportPurch3Data()
                            FExportLedgerData()
                            FExportLedgerHeadData()
                            FExportLedgerHeadDetailData()
                        Case FromSoftware_Monark
                            FExportPartyData_Monark()
                            FExportItemData_Monark()
                            'FExportItemRateListData()
                            'FExportSale1Data_Monark()
                            'FExportSale2Data_Monark()
                            'FExportSale3Data_Monark()
                            'FExportPurch1Data_Monark()
                            'FExportPurch2Data_Monark()
                            'FExportPurch3Data_Monark()
                            'FExportLedgerData_Monark()
                            'FExportLedgerHeadData_Monark()
                            'FExportLedgerHeadDetailData_Monark()
                            'FExportBuiltyHead_Monark()
                            'FExportBuiltyHeadDetail_Monark()
                    End Select
                End If
            Case BtnCancel.Name
                Me.Close()
        End Select
    End Sub
    Private Sub FExportSpecificFile()
        Select Case TxtFromSoftware.Text
            Case FromSoftware_Dataman
                Select Case TxtExportSpecific.Text
                    Case Party
                        FExportPartyData()
                    Case Item
                        FExportItemData()
                    Case ItemRateList
                        FExportItemRateListData()
                    Case Sale1
                        FExportSale1Data()
                    Case Sale2
                        FExportSale2Data()
                    Case Sale3
                        FExportSale3Data()
                    Case Purch1
                        FExportPurch1Data()
                    Case Purch2
                        FExportPurch2Data()
                    Case Purch3
                        FExportPurch3Data()
                    Case Ledger
                        FExportLedgerData()
                    Case LedgerHead
                        FExportLedgerHeadData()
                    Case LedgerHeadDetail
                        FExportLedgerHeadDetailData()
                End Select
            Case FromSoftware_Monark
                Select Case TxtExportSpecific.Text
                    Case Party
                        FExportPartyData_Monark()
                    Case Item
                        FExportItemData_Monark()
                    Case ItemRateList
                        'FExportItemRateListData_Monark()
                    Case Sale1
                        FExportSale1Data_Monark()
                    Case Sale2
                        FExportSale2Data_Monark()
                    Case Sale3
                        FExportSale3Data_Monark()
                    Case Purch1
                        FExportPurch1Data_Monark()
                    Case Purch2
                        FExportPurch2Data_Monark()
                    Case Purch3
                        FExportPurch3Data_Monark()
                    Case Ledger
                        FExportLedgerData_Monark()
                    Case LedgerHead
                        FExportLedgerHeadData_Monark()
                    Case LedgerHeadDetail
                        FExportLedgerHeadDetailData_Monark()
                    Case BuiltyHead
                        FExportBuiltyHead_Monark()
                    Case BuiltyHeadDetail
                        FExportBuiltyHeadDetail_Monark()
                End Select
        End Select
    End Sub

    Private Sub FExportPartyData()
        Try
            mQry = " SELECT CASE WHEN Sg.Nature IN ('Supplier','Transporter','Customer') THEN Sg.Nature
		        WHEN Sg.Nature = 'Broker' THEN 'Sales Agent'
                ELSE '' END AS [Party Type], 
                Sg.SubCode AS [Code], IsNull(Sg.Name,'') AS [Display Name], Sg.Name AS [Name], 
                IsNull(Sg.Add1,'') + IsNull(Sg.Add2,'') + IsNull(Sg.Add3,'') AS [Address],
                IsNull(C.CityName,'') AS [City], IsNull(S.Name,'') AS [State], IsNull(Sg.PIN,'') AS [Pin No], 
                IsNull(Sg.Phone,'') AS [Contact No], 
                IsNull(Sg.Mobile,'') AS [Mobile], 
                IsNull(Sg.EMail,'') AS [EMail], IsNull(Ag.GroupName,'') AS [Account Group], 
                CASE WHEN Sg.Party_Reg_UnReg IN ('UnRegister','UnRegist') THEN 'Unregistered'
                        WHEN Sg.Party_Reg_UnReg = 'Register' THEN 'Registered' 
                        WHEN Sg.Party_Reg_UnReg = 'Composit'THEN 'Composition'
                        ELSE 'Unregistered' END AS [Sales Tax Group],
                IsNull(Sg.CreditDays,0) AS [Credit Days], IsNull(Sg.CreditLimit,0) AS [Credit Limit], 
                IsNull(Sg.ConPerson,'') AS [Contact Person], 
                IsNull(Sg.GSTIN,'') AS [Sales Tax No], 
                IsNull(Sg.PANNo,'') AS [PAN No], IsNull(Sg.AadharNo,'') AS [Aadhar No], 
                IsNull(MasterParty.Name,'') AS [Master Party], IsNull(A.Name,'') AS [Area],
                IsNull(Broker.Name,'') AS [Agent], IsNull(Transporter.Name,'') AS [Transporter], 0 AS Distance
                FROM SubGroup Sg
                LEFT JOIN City C ON Sg.CityCode = C.CityCode
                LEFT JOIN StateMast S ON C.StateCode = S.Code
                LEFT JOIN AcGroup Ag ON Sg.GroupCode = Ag.GroupCode
                LEFT JOIN SubGroup MasterParty ON Sg.MasterParty = MasterParty.SubCode
                LEFT JOIN AreaMast A ON Sg.AreaCode = A.CODE
                LEFT JOIN SubGroup Broker ON Sg.Broker = Broker.SubCode
                LEFT JOIN SubGroup Transporter ON Sg.Transporter = Transporter.SubCode 

                UNION ALL 

                SELECT DISTINCT 'Customer' AS [Party Type], 
                'C' + Convert(NVARCHAR,row_number() OVER (ORDER BY  H.CashPartyName)) AS [Code],  H.CashPartyName AS [Display Name], 
                H.CashPartyName AS [Name], '' AS [Address], '' AS [City], '' AS [State], '' AS [Pin No], '' AS [Contact No], 
                '' AS [Mobile], '' AS [EMail], 'Sundry Debtors' AS [Account Group], 'Unregistered' AS [Sales Tax Group], 0 AS [Credit Days], 
                0 AS [Credit Limit], '' AS [Contact Person], 
                '' AS [Sales Tax No], '' AS [PAN No], '' AS [Aadhar No], '' AS [Master Party], '' AS [Area], 
                '' AS [Agent], '' AS [Transporter], 0 AS [Distance]
                FROM SALE1 H
                WHERE IsNull(CashPartyName,'') <> '' AND IsNull(CashParty,'') = ''
                GROUP BY H.CashPartyName  "
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + "\" + Party + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + Party + "." + ex.Message)
        End Try
    End Sub

    Private Sub FExportItemData()
        Try
            mQry = " SELECT I.Code AS [Item Code], I.Name + Space(10) + '[' + IsNull(Mm.Name,'') + ' | ' + IsNull(Gm.Name,'') + ']' AS [Item Name], 
                    I.Name + Space(10) + '[' + IsNull(Mm.Name,'') + ' | ' + IsNull(Gm.Name,'') + ']' AS [Item Display Name], 
                    Convert(NVARCHAR,IsNull(Mm.Name,'N.A.')) AS [Item Group], Gm.Name AS [Item Category], 
                    Convert(NVARCHAR,I.Name) AS [Specification], CASE WHEN I.Unit = 'Mtr' THEN 'Meter' ELSE I.Unit END AS [Unit], I.PurchRate AS [Purchase Rate], I.SaleRate AS [Sale Rate], 
                    'GST 5%' AS [Sales Tax Group], I.HSNCODE AS [HSN Code]
                    FROM Itemmast I 
                    LEFT JOIN MakeMast Mm ON I.Make = Mm.Code 
                    LEFT JOIN GradeMast Gm ON I.Grade = Gm.Code 
                    Order By I.Name "
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + "\" + Item + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + Item + "." + ex.Message)
        End Try
    End Sub
    Private Sub FExportItemRateListData()
        Try
            mQry = " SELECT I.Name + Space(10) + '[' + IsNull(Mm.Name,'') + ' | ' + IsNull(Gm.Name,'') + ']' AS [Item Name],  
                I.Dhara AS [Dhara Rate], I.Nett AS [Nett Rate], 0 AS [Super Nett Rate]
                FROM Itemmast I 
                LEFT JOIN MakeMast Mm ON I.Make = Mm.Code
                LEFT JOIN GradeMast Gm ON I.Grade = Gm.Code "
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + "\" + ItemRateList + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + Party + ex.Message)
        End Try
    End Sub

    Private Sub FExportSale1Data()
        Try
            mQry = " SELECT CASE WHEN H.V_Type = 'GSTI' THEN 'SI' 
 			 WHEN H.V_Type = 'GSCS' THEN 'SIC' 
 			 WHEN H.V_Type = 'GSRTI' THEN 'SR' END AS V_TYPE,  
                CASE WHEN H.V_Type IN ('GSTI','GSCS') THEN row_number() OVER (ORDER BY H.V_Date) ELSE H.V_No END AS V_No,
                Convert(NVARCHAR,H.V_Date) AS V_Date,
                H.V_Type + '-' + Convert(NVARCHAR,H.V_No) AS [Invoice No], 
                CASE WHEN CashSubGroup.Name IS NOT NULL THEN CashSubGroup.Name ELSE Party.Name END AS [Sale To Party],
                IsNull(Party.Add1,'') + IsNull(Party.Add2,'') + IsNull(Party.Add3,'') AS [Sale To Party Address],
                IsNull(C.CityName,'') AS [Sale To Party City], IsNull(Party.PIN,'') AS [Sale To Party Pincode],
                IsNull(Party.GSTIN,'') AS [Sale To Party Sales Tax No], IsNull(Party.Name,'') AS [Bill To Party],
                IsNull(Broker.Name,'') AS [Agent], '' AS [Rate Type],
                CASE WHEN Party.Party_Reg_UnReg IN ('UnRegister','UnRegist') THEN 'Unregistered'
                        WHEN Party.Party_Reg_UnReg = 'Register' THEN 'Registered' 
                        WHEN Party.Party_Reg_UnReg = 'Composit'THEN 'Composition'
                        ELSE 'Unregistered' END AS [Sales Tax Group Party],
                CASE WHEN IsNull(H.IGST_Value,0) > 0 THEN 'Outside State' ELSE 'Within State' END AS [Place Of Supply],
                '' AS [Sale To Party Doc No],
                '' AS [Sale To Party Doc Date],
                IsNull(H.Remarks,'') AS [Remark],'' AS [Terms And Conditions],
                0 AS [Credit Limit],
                IsNull(H.CreditDays,0) AS [Credit Days],
                IsNull(H.Total,0) AS [SubTotal1],0 AS [Deduction_Per], IsNull(H.DiscAmt,0) + IsNull(H.DedAmt,0) AS [Deduction],H.AddPer AS [Other_Charge_Per],
                H.AddAmt AS [Other_Charge], H.RoundOff AS [Round_Off], H.NetAmt AS [Net_Amount]
                FROM SALE1 H 
                LEFT JOIN SubGroup Party ON H.Party = Party.SubCode
                LEFT JOIN City C ON Party.CityCode= C.CityCode
                LEFT JOIN SubGroup Broker ON H.Broker = Broker.SubCode
                LEFT JOIN SubGroup CashSubGroup ON H.CashParty = CashSubGroup.SubCode
                LEFT JOIN (SELECT DocId, Count(*) AS Cnt FROM SALE2 GROUP BY DocId) VSale2 ON H.DoCId = VSale2.DocId
                WHERE H.V_Type IN ('GSTI','GSRTI','GSCS')
                AND VSale2.DocId IS NOT NULL
And H.V_Date > '30/Jun/2018'
                ORDER BY CASE WHEN H.V_Type IN ('GSTI','GSCS') THEN 'SI' WHEN H.V_Type = 'GSRTI' THEN 'SR' END , 
                CASE WHEN H.V_Type IN ('GSTI','GSCS') THEN row_number() OVER (ORDER BY H.V_Date) ELSE H.V_No END "
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + "\" + Sale1 + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + Sale1 + "." + ex.Message)
        End Try
    End Sub

    Private Sub FExportSale2Data()
        Try
            mQry = "  SELECT CASE WHEN H.V_Type = 'GSTI' THEN 'SI' 
 			 WHEN H.V_Type = 'GSCS' THEN 'SIC' 
 			 WHEN H.V_Type = 'GSRTI' THEN 'SR' END AS V_TYPE,  
                H.V_Type + '-' + Convert(NVARCHAR,H.V_No) AS [Invoice No], 
                L.S_NO As TSr,
                I.Name + Space(10) + '[' + IsNull(Mm.Name,'') + ' | ' + IsNull(Gm.Name,'') + ']' AS [Item Name],
                '' AS [Specification],
                CASE WHEN IsNull(L.IGST_PER,0) + IsNull(L.CGST_PER,0) + IsNull(L.SGST_PER,0) = 0 THEN 'GST 0%'
                     WHEN IsNull(L.IGST_PER,0) + IsNull(L.CGST_PER,0) + IsNull(L.SGST_PER,0) = 5 THEN 'GST 5%'
                     WHEN IsNull(L.IGST_PER,0) + IsNull(L.CGST_PER,0) + IsNull(L.SGST_PER,0) = 12 THEN 'GST 12%'
                     WHEN IsNull(L.IGST_PER,0) + IsNull(L.CGST_PER,0) + IsNull(L.SGST_PER,0) = 18 THEN 'GST 18%'
                     WHEN IsNull(L.IGST_PER,0) + IsNull(L.CGST_PER,0) + IsNull(L.SGST_PER,0) = 28 THEN 'GST 28%'
                     ELSE 'GST 5%' END AS [Sales Tax Group Item],
                L.Qty AS [Qty],
                I.Unit AS [Unit],
                L.PCS AS [Pcs],
                1 AS [Unit Multiplier],
                I.Unit AS [Deal Unit],
                L.Qty AS [Deal Qty],
                L.Rate AS [Rate],
                0 AS [Discount Per],
                IsNull(FDiscAmt,0) + IsNull(L.FPDiscAmt2,0) AS [Discount Amount],
                0 AS [Additional Discount Per],
                0 AS [Additional Discount Amount],
                L.Amount AS [Amount],
                L.Remarks AS [Remark],
                L.Bale_No AS [Bale No],
                '' AS [Lot No],
                L.Amount AS [Gross_Amount],
                L.Amount AS [Taxable_Amount],
                L.IGST_PER AS [Tax1_Per],
                L.IGST_Value AS [Tax1],
                L.CGST_PER AS [Tax2_Per],
                L.CGST_Value AS [Tax2],
                L.SGST_PER AS [Tax3_Per],
                L.SGST_Value AS [Tax3],
                0 AS [Tax4_Per],
                0 AS [Tax4],
                0 AS [Tax5_Per],
                0 AS [Tax5],
                L.Amount + IsNull(L.IGST_Value,0) + IsNull(L.CGST_Value,0) + IsNull(L.SGST_Value,0) AS [SubTotal1]
                FROM SALE1 H 
                LEFT JOIN SALE2 L ON L.DoCId = H.DoCId
                LEFT JOIN Itemmast I ON L.Item = I.Code
                LEFT JOIN MakeMast Mm ON I.Make = Mm.Code
                LEFT JOIN GradeMast Gm ON I.Grade = Gm.Code
                WHERE L.DoCId IS NOT NULL 
And H.V_Date > '30/Jun/2018'"
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + "\" + Sale2 + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + Sale2 + "." + ex.Message)
        End Try
    End Sub

    Private Sub FExportSale3Data()
        Try
            mQry = " SELECT H.V_Type AS [V_TYPE],
                    H.V_No AS [Invoice No], L.S_NO AS [TSr], L.S_NO1 AS [Sr], NULL AS [Specification],
                    L.PCS AS [Pcs], L.MTR AS [Qty], 0 AS [TotalQty]
                    FROM SALE3 L 
                    LEFT JOIN SALE2 S2 ON L.DoCId = S2.DoCId AND L.S_NO = S2.S_NO
                    LEFT JOIN SALE1 H ON H.DoCId = L.DoCId "
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + "\" + Sale3 + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + Sale3 + "." + ex.Message)
        End Try
    End Sub

    Private Sub FExportPurch1Data()
        Try
            mQry = " SELECT CASE WHEN H.V_Type = 'PBTI' THEN 'PI' WHEN H.V_Type = 'PRTI' THEN 'PR' END AS V_TYPE, 
                H.V_No  AS V_No,
                Convert(NVARCHAR,H.V_Date) AS V_Date,
                H.V_No AS [Invoice No], 
                IsNull(Party.Name,'') AS [Vendor],
                IsNull(Party.Add1,'') + IsNull(Party.Add2,'') + IsNull(Party.Add3,'') AS [Vendor Address],
                IsNull(C.CityName,'') AS [Vendor City], IsNull(Party.PIN,'') AS [Vendor Pincode], IsNull(Party.Mobile,'') AS [Vendor Mobile],
                IsNull(Party.GSTIN,'') AS [Vendor Sales Tax No], 
                '' AS [Vendor Doc No],
                '' AS [Vendor Doc Date],
                IsNull(Party.Name,'') AS [Bill To Party],
                IsNull(Broker.Name,'') AS [Agent], 
                CASE WHEN IsNull(VPurch2.TotalTax,0) > 0 THEN 'Registered'
                        ELSE 'Unregistered' END AS [Sales Tax Group Party],
                CASE WHEN IsNull(H.IGST_Value,0) > 0 THEN 'Outside State' ELSE 'Within State' END AS [Place Of Supply],
                '' AS [Ship To Address],
                IsNull(H.Remark,'') AS [Remark],
                IsNull(H.Total,0) AS [SubTotal1],0 AS [Deduction_Per], IsNull(H.DiscAmt,0) + IsNull(H.DedAmt,0) AS [Deduction],
                IsNull(H.AddPer,0) AS [Other_Charge_Per],
                IsNull(H.AddAmt,0) AS [Other_Charge], IsNull(H.RoundOff,0) AS [Round_Off], 
                IsNull(H.NetAmount,0) AS [Net_Amount]
                FROM Purch1 H 
                LEFT JOIN SubGroup Party ON H.PartyCode = Party.SubCode
                LEFT JOIN City C ON Party.CityCode= C.CityCode
                LEFT JOIN SubGroup Broker ON H.BrokerCode = Broker.SubCode
                LEFT JOIN (SELECT L.DoCId, IsNull(Sum(L.IGST_Value),0) + IsNull(Sum(L.CGST_Value),0) + IsNull(Sum(L.SGST_Value),0) AS TotalTax 
							FROM SALE2 L 
							GROUP BY L.DoCId) AS VPurch2 ON H.DocId = VPurch2.DocId 
                WHERE H.V_Type <> 'BS'"
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + "\" + Purch1 + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + Purch1 + "." + ex.Message)
        End Try
    End Sub
    Private Sub FExportPurch2Data()
        Try
            mQry = " SELECT CASE WHEN H.V_Type = 'PBTI' THEN 'PI' WHEN H.V_Type = 'PRTI' THEN 'PR' END AS V_TYPE, 
                    H.V_No AS [Invoice No],
                    L.S_NO As TSr,
                    I.Name + Space(10) + '[' + IsNull(Mm.Name,'') + ' | ' + IsNull(Gm.Name,'') + ']' AS [Item Name],
                    '' AS [Specification],
                    IsNull(L.Bale_No,'') AS [Bale No],
                    CASE WHEN IsNull(L.IGST_PER,0) + IsNull(L.CGST_PER,0) + IsNull(L.SGST_PER,0) = 0 THEN 'GST 0%'
                         WHEN IsNull(L.IGST_PER,0) + IsNull(L.CGST_PER,0) + IsNull(L.SGST_PER,0) = 5 THEN 'GST 5%'
                         WHEN IsNull(L.IGST_PER,0) + IsNull(L.CGST_PER,0) + IsNull(L.SGST_PER,0) = 12 THEN 'GST 12%'
                         WHEN IsNull(L.IGST_PER,0) + IsNull(L.CGST_PER,0) + IsNull(L.SGST_PER,0) = 18 THEN 'GST 18%'
                         WHEN IsNull(L.IGST_PER,0) + IsNull(L.CGST_PER,0) + IsNull(L.SGST_PER,0) = 28 THEN 'GST 28%'
                         ELSE 'GST 5%' END AS [Sales Tax Group Item],
                    0 AS [Profit Margin Per],
                    IsNull(L.Qty,0) AS [Qty],
                    IsNull(I.Unit,'') AS [Unit],
                    IsNull(L.PCS,0) AS [Pcs],
                    IsNull(I.Unit,'')  AS [Deal Unit],
                    IsNull(L.Qty,0) AS [Deal Qty],
                    IsNull(L.Rate,0) AS [Rate],
                    0 AS [Discount Per],
                    IsNull(FDiscAmt,0) + IsNull(L.FPDiscAmt2,0) AS [Discount Amount],
                    0 AS [Additional Discount Per],
                    0 AS [Additional Discount Amount],
                    IsNull(L.Amount,0) AS [Amount],
                    0 AS [Sale Rate],
                    0 AS [MRP],
                    IsNull(L.Remarks,'') AS [Remark],
                    '' AS [LR No],
                    '' AS [LR Date],
                    '' AS [Lot No],
                    IsNull(L.Amount,0) AS [Gross_Amount],
                    IsNull(L.Amount,0) AS [Taxable_Amount],
                    IsNull(L.IGST_PER,0) AS [Tax1_Per],
                    IsNull(L.IGST_Value,0) AS [Tax1],
                    IsNull(L.CGST_PER,0) AS [Tax2_Per],
                    IsNull(L.CGST_Value,0) AS [Tax2],
                    IsNull(L.SGST_PER,0) AS [Tax3_Per],
                    IsNull(L.SGST_Value,0) AS [Tax3],
                    0 AS [Tax4_Per],
                    0 AS [Tax4],
                    0 AS [Tax5_Per],
                    0 AS [Tax5],
                    IsNull(L.Amount,0) + IsNull(L.IGST_Value,0) + IsNull(L.CGST_Value,0) + IsNull(L.SGST_Value,0) AS [SubTotal1]
                    FROM Purch1 H 
                    LEFT JOIN SALE2 L ON L.DoCId = H.DoCId
                    LEFT JOIN Itemmast I ON L.Item = I.Code
                    LEFT JOIN MakeMast Mm ON I.Make = Mm.Code
                    LEFT JOIN GradeMast Gm ON I.Grade = Gm.Code
                    WHERE L.DoCId IS NOT NULL 
                    And H.V_Type <> 'BS'"
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + "\" + Purch2 + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + Purch2 + "." + ex.Message)
        End Try
    End Sub

    Private Sub FExportPurch3Data()
        Try
            mQry = " SELECT H.V_Type AS [V_TYPE],
                H.V_No AS [Invoice No], L.S_NO AS [TSr], L.S_NO1 AS [Sr], NULL AS [Specification],
                L.PCS AS [Pcs], L.MTR AS [Qty], 0 AS [TotalQty]
                FROM SALE3 L 
                LEFT JOIN SALE2 S2 ON L.DoCId = S2.DoCId AND L.S_NO = S2.S_NO
                LEFT JOIN Purch1 H ON H.DoCId = L.DoCId
                WHERE H.DocId IS NOT NULL "
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + "\" + Purch3 + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + Purch3 + "." + ex.Message)
        End Try
    End Sub

    Private Sub FExportLedgerData()
        Try
            mQry = " DECLARE @SinglLineLedger AS TABLE (DocId NVARCHAR(50))

                    INSERT INTO @SinglLineLedger(DocId)
                    SELECT L.DocId
                    FROM Ledger L 
                    WHERE L.V_Type <> 'F_AO'
                    GROUP BY L.DocId 
                    HAVING Abs(IsNull(Sum(L.AmtDr),0) - IsNull(Sum(L.AmtCr),0)) > 1


                    SELECT VLedger.V_Type, 
                    VLedger.V_No,
                    Convert(NVARCHAR,VLedger.V_Date) AS V_Date,
                    IsNull(VLedger.Name,'') AS [Ledger Account Name], IsNull(VLedger.ContraName,'')  AS [Contra Ledger Account Name],
                    IsNull(VLedger.Narration,'')  AS [Narration], IsNull(VLedger.Chq_No,'')  AS [Chq No], IsNull(VLedger.Chq_Date,'')  AS [Chq Date], 
                    IsNull(VLedger.AmtDr,0) AS [Amt Dr], IsNull(VLedger.AmtCr,0) AS [Amt Cr]
                    FROM 
                    (
                        SELECT CASE WHEN Vt.Description = 'Cash Receipt [Shop]' THEN 'CR'
                                WHEN Vt.Description = 'Opening Balance' THEN 'OB'
                                WHEN Vt.Description IN ('Contra','Purchase Bill','Purchase Bill Return', 'Sale', 'Sale Return',
                                'GST CASH SALE','GST PURCHASE','GST PURCHASE RETRUN','GST SALE','GST SALE RETURN','Debit Note (GST)','Credit Note') THEN 'JV'
                                ELSE L.V_Type END AS V_Type,
                        CASE WHEN Vt.Description = 'Cash Receipt [Shop]' THEN  L.V_No + 100000
                                WHEN Vt.Description = 'Contra' THEN  L.V_No + 200000
                                WHEN Vt.Description = 'Purchase Bill' THEN  L.V_No + 300000
                                WHEN Vt.Description = 'Purchase Bill Return' THEN  L.V_No + 400000
                                WHEN Vt.Description = 'Sale' THEN  L.V_No + 500000
                                WHEN Vt.Description = 'Sale Return' THEN  L.V_No + 600000
                                WHEN Vt.Description = 'GST CASH SALE' THEN L.V_No + 700000
                                WHEN Vt.Description = 'GST PURCHASE' THEN L.V_No + 800000
                                WHEN Vt.Description = 'GST PURCHASE RETRUN' THEN L.V_No + 900000
                                WHEN Vt.Description = 'GST SALE' THEN L.V_No + 1000000
                                WHEN Vt.Description = 'GST SALE RETURN' THEN L.V_No + 1100000
                                WHEN Vt.Description = 'Debit Note (GST)' THEN L.V_No + 1200000
                                WHEN Vt.Description = 'Credit Note' THEN L.V_No + 1300000
                                ELSE L.V_No END AS V_No,
                        L.V_Date, Sg.Name , Contra.Name AS ContraName,
                        L.Narration , L.Chq_No , L.Chq_Date , L.AmtDr , L.AmtCr 
                        FROM Ledger L
                        LEFT JOIN SubGroup Sg ON L.SubCode = Sg.SubCode
                        LEFT JOIN SubGroup Contra ON L.ContraSub = Contra.SubCode
                        LEFT JOIN Voucher_Type Vt ON L.V_Type = Vt.V_Type
                        LEFT JOIN Sale2 S2 ON L.DocId = S2.DoCId
                        LEFT JOIN Ledger_GST Lg ON L.DocId = Lg.DocId
                        LEFT JOIN @SinglLineLedger T ON L.DocId = T.DocId
                        WHERE T.DocId IS NOT NULL
                        AND S2.DoCId IS NULL AND Lg.DocId IS NULL


                        UNION ALL 

                        SELECT CASE WHEN Vt.Description = 'Cash Receipt [Shop]' THEN 'CR'
                                WHEN Vt.Description = 'Opening Balance' THEN 'OB'
                                WHEN Vt.Description IN ('Contra','Purchase Bill','Purchase Bill Return', 'Sale', 'Sale Return',
                                'GST CASH SALE','GST PURCHASE','GST PURCHASE RETRUN','GST SALE','GST SALE RETURN','Debit Note (GST)','Credit Note') THEN 'JV'
                                ELSE L.V_Type END AS V_Type,
                        CASE WHEN Vt.Description = 'Cash Receipt [Shop]' THEN  L.V_No + 100000
                                WHEN Vt.Description = 'Contra' THEN  L.V_No + 200000
                                WHEN Vt.Description = 'Purchase Bill' THEN  L.V_No + 300000
                                WHEN Vt.Description = 'Purchase Bill Return' THEN  L.V_No + 400000
                                WHEN Vt.Description = 'Sale' THEN  L.V_No + 500000
                                WHEN Vt.Description = 'Sale Return' THEN  L.V_No + 600000
                                WHEN Vt.Description = 'GST CASH SALE' THEN L.V_No + 700000
                                WHEN Vt.Description = 'GST PURCHASE' THEN L.V_No + 800000
                                WHEN Vt.Description = 'GST PURCHASE RETRUN' THEN L.V_No + 900000
                                WHEN Vt.Description = 'GST SALE' THEN L.V_No + 1000000
                                WHEN Vt.Description = 'GST SALE RETURN' THEN L.V_No + 1100000
                                WHEN Vt.Description = 'Debit Note (GST)' THEN L.V_No + 1200000
                                WHEN Vt.Description = 'Credit Note' THEN L.V_No + 1300000
                                ELSE L.V_No END AS V_No,
                        L.V_Date, Contra.Name , Sg.Name AS ContraName,
                        L.Narration , L.Chq_No , L.Chq_Date , L.AmtCr , L.AmtDr 
                        FROM Ledger L
                        LEFT JOIN SubGroup Sg ON L.SubCode = Sg.SubCode
                        LEFT JOIN SubGroup Contra ON L.ContraSub = Contra.SubCode
                        LEFT JOIN Voucher_Type Vt ON L.V_Type = Vt.V_Type
                        LEFT JOIN Sale2 S2 ON L.DocId = S2.DoCId
                        LEFT JOIN Ledger_GST Lg ON L.DocId = Lg.DocId
                        LEFT JOIN @SinglLineLedger T ON L.DocId = T.DocId
                        WHERE T.DocId IS NOT NULL AND L.ContraSub IS NOT NULL
                        AND S2.DoCId IS NULL AND Lg.DocId IS NULL

                        UNION ALL 

                        SELECT CASE WHEN Vt.Description = 'Cash Receipt [Shop]' THEN 'CR'
                                WHEN Vt.Description = 'Opening Balance' THEN 'OB'
                                WHEN Vt.Description IN ('Contra','Purchase Bill','Purchase Bill Return', 'Sale', 'Sale Return',
                                'GST CASH SALE','GST PURCHASE','GST PURCHASE RETRUN','GST SALE','GST SALE RETURN','Debit Note (GST)','Credit Note') THEN 'JV'
                                ELSE L.V_Type END AS V_Type,
                        CASE WHEN Vt.Description = 'Cash Receipt [Shop]' THEN  L.V_No + 100000
                                WHEN Vt.Description = 'Contra' THEN  L.V_No + 200000
                                WHEN Vt.Description = 'Purchase Bill' THEN  L.V_No + 300000
                                WHEN Vt.Description = 'Purchase Bill Return' THEN  L.V_No + 400000
                                WHEN Vt.Description = 'Sale' THEN  L.V_No + 500000
                                WHEN Vt.Description = 'Sale Return' THEN  L.V_No + 600000
                                WHEN Vt.Description = 'GST CASH SALE' THEN L.V_No + 700000
                                WHEN Vt.Description = 'GST PURCHASE' THEN L.V_No + 800000
                                WHEN Vt.Description = 'GST PURCHASE RETRUN' THEN L.V_No + 900000
                                WHEN Vt.Description = 'GST SALE' THEN L.V_No + 1000000
                                WHEN Vt.Description = 'GST SALE RETURN' THEN L.V_No + 1100000
                                WHEN Vt.Description = 'Credit Note' THEN L.V_No + 1300000
                                ELSE L.V_No END AS V_No,
                        L.V_Date, Sg.Name , Contra.Name AS ContraName,
                        L.Narration , L.Chq_No , L.Chq_Date , L.AmtDr , L.AmtCr 
                        FROM Ledger L 
                        LEFT JOIN SubGroup Sg ON L.SubCode = Sg.SubCode
                        LEFT JOIN SubGroup Contra ON L.ContraSub = Contra.SubCode
                        LEFT JOIN Voucher_Type Vt ON L.V_Type = Vt.V_Type
                        LEFT JOIN Sale2 S2 ON L.DocId = S2.DoCId
                        LEFT JOIN Ledger_GST Lg ON L.DocId = Lg.DocId
                        LEFT JOIN @SinglLineLedger T ON L.DocId = T.DocId
                        WHERE T.DocId IS NULL
                        AND S2.DoCId IS NULL AND Lg.DocId IS NULL
                    ) AS VLedger
                    LEFT JOIN Voucher_Type Vt ON VLedger.V_Type = Vt.V_Type
                    ORDER BY VLedger.V_Type, VLedger.V_No "
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + "\" + Ledger + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + Ledger + "." + ex.Message)
        End Try
    End Sub

    Private Sub FExportLedgerHeadData()
        Try
            mQry = " SELECT CASE WHEN L.V_TYPE = 'GSTCN' THEN 'CNS'
                             WHEN L.V_TYPE = 'GSTDN' THEN 'DNS' END AS [V_TYPE],
                L.VNo AS [V_NO],
                Convert(NVARCHAR,L.V_Date) AS V_Date,
                L.VNo AS [Entry No],
                Sg.Name AS [Party Name],
                L.TAXABLEVALUE AS [SubTotal1],
                0 AS [Deduction_Per],
                0 AS [Deduction],
                0 AS [Other_Charge_Per],
                0 AS [Other_Charge],
                0 AS [Round_Off],
                0 AS [Net_Amount]
                FROM Ledger_GST L 
                LEFT JOIN Voucher_Type Vt ON L.V_Type = Vt.V_Type
                LEFT JOIN SubGroup Sg ON L.Party_Code = Sg.SubCode
                WHERE Vt.Description IN ('Debit Note (GST)','Credit Note (GST)') "
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + "\" + LedgerHead + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + LedgerHead + "." + ex.Message)
        End Try
    End Sub

    Private Sub FExportLedgerHeadDetailData()
        Try
            mQry = " SELECT CASE WHEN L.V_TYPE = 'GSTCN' THEN 'CNS'
                    WHEN L.V_TYPE = 'GSTDN' THEN 'DNS' END AS [V_TYPE],
                    L.VNo AS [Entry No],
                    IsNull(Sg.Name,'') AS [Ledger Account Name],
                    '' AS Specification,
                    0 AS Qty,
                    '' AS Unit,
                    0 AS Rate,
                    IsNull(L.TAXABLEVALUE,0) AS Amount,
                    '' AS [Chq No],
                    '' AS [Chq Date],
                    '' AS [Remark],
                    IsNull(L.TAXABLEVALUE,0) AS [Gross_Amount],
                    IsNull(L.TAXABLEVALUE,0) AS [Taxable_Amount],
                    IsNull(L.IGST_PER,0) AS [Tax1_Per],
                    IsNull(L.IGST_VALUE,0) AS [Tax1],
                    IsNull(L.CGST_PER,0) AS [Tax2_Per],
                    IsNull(L.CGST_VALUE,0) AS [Tax2],
                    IsNull(L.SGST_PER,0) AS [Tax3_Per],
                    IsNull(L.SGST_VALUE,0) AS [Tax3],
                    0 AS [Tax4_Per],
                    0 AS [Tax4],
                    0 AS [Tax5_Per],
                    0 AS [Tax5],
                    IsNull(L.TAXABLEVALUE,0) + IsNull(L.IGST_Value,0) + IsNull(L.CGST_Value,0) + IsNull(L.SGST_Value,0) AS [SubTotal1]
                    FROM Ledger_GST L 
                    LEFT JOIN Voucher_Type Vt ON L.V_Type = Vt.V_Type
                    LEFT JOIN SubGroup Sg ON L.Ac_Line = Sg.SubCode
                    WHERE Vt.Description IN ('Debit Note (GST)','Credit Note (GST)') "
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + "\" + LedgerHeadDetail + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + LedgerHeadDetail + "." + ex.Message)
        End Try
    End Sub

    Private Sub FWriteExcelFile(datatableMain As System.Data.DataTable, FileNameWithPath As String)
        'Exit Sub
        'This section help you if your language is not English.
        If datatableMain.Rows.Count > 65500 Then
            MsgBox("No. Of Rows in DataTable is more then 65570.", MsgBoxStyle.Information) : Exit Sub
        End If
        System.Threading.Thread.CurrentThread.CurrentCulture =
                System.Globalization.CultureInfo.CreateSpecificCulture("en-US")
        Dim oExcel As Excel.Application
        Dim oBook As Excel.Workbook
        Dim oSheet As Excel.Worksheet
        oExcel = CreateObject("Excel.Application")
        oBook = oExcel.Workbooks.Add(Type.Missing)
        oSheet = oBook.Worksheets(1)

        Dim dc As System.Data.DataColumn
        Dim dr As System.Data.DataRow
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0

        ''Export the Columns to excel file
        'For Each dc In datatableMain.Columns
        '    colIndex = colIndex + 1
        '    oSheet.Cells(1, colIndex) = dc.ColumnName
        'Next

        ''Export the rows to excel file
        'For Each dr In datatableMain.Rows
        '    rowIndex = rowIndex + 1
        '    colIndex = 0
        '    For Each dc In datatableMain.Columns
        '        colIndex = colIndex + 1
        '        oSheet.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)
        '    Next
        'Next

        Dim Nbligne As Integer = datatableMain.Rows.Count
        For Each dc In datatableMain.Columns
            colIndex = colIndex + 1
            'Entête de colonnes (column headers)
            oSheet.Cells(1, colIndex) = dc.ColumnName
            'Données(data)
            'You can use CDbl instead of Cobj If your data is of type Double
            If Nbligne > 0 Then
                oSheet.Cells(2, colIndex).Resize(Nbligne, ).Value = oExcel.Application.transpose(datatableMain.Rows.OfType(Of DataRow)().[Select](Function(k) CObj(k(dc.ColumnName))).ToArray())
            End If
        Next

        oSheet.Columns.AutoFit()
        'Save file in final path
        oBook.SaveAs(FileNameWithPath, XlFileFormat.xlWorkbookNormal, Type.Missing,
        Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive,
        Type.Missing, Type.Missing, Type.Missing, Type.Missing)

        'Release the objects
        ReleaseObject(oSheet)
        oBook.Close(False, Type.Missing, Type.Missing)
        ReleaseObject(oBook)
        oExcel.Quit()
        ReleaseObject(oExcel)
        'Some time Office application does not quit after automation: 
        'so i am calling GC.Collect method.
        GC.Collect()
    End Sub
    Private Sub ReleaseObject(ByVal o As Object)
        Try
            While (System.Runtime.InteropServices.Marshal.ReleaseComObject(o) > 0)
            End While
        Catch
        Finally
            o = Nothing
        End Try
    End Sub

    Private Function FillData(Qry As String, ConnStr As String)
        Dim DsTemp As New DataSet
        Dim Da As New SqlClient.SqlDataAdapter(Qry, ConnStr)
        Da.Fill(DsTemp)
        Return DsTemp
    End Function

    Private Sub TxtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtExportSpecific.KeyDown
        Try
            Select Case sender.Name
                Case TxtExportSpecific.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtExportSpecific.AgHelpDataSet Is Nothing Then
                            mQry = "Select '" & Party & "' Code, '" & Party & "' As Name 
                                    UNION ALL 
                                    Select '" & Item & "' Code, '" & Item & "' As Name 
                                    UNION ALL 
                                    Select '" & ItemRateList & "' Code, '" & ItemRateList & "' As Name 
                                    UNION ALL 
                                    Select '" & Sale1 & "' Code, '" & Sale1 & "' As Name 
                                    UNION ALL 
                                    Select '" & Sale2 & "' Code, '" & Sale2 & "' As Name 
                                    UNION ALL 
                                    Select '" & Sale3 & "' Code, '" & Sale3 & "' As Name 
                                    UNION ALL 
                                    Select '" & Purch1 & "' Code, '" & Purch1 & "' As Name 
                                    UNION ALL 
                                    Select '" & Purch2 & "' Code, '" & Purch2 & "' As Name 
                                    UNION ALL 
                                    Select '" & Purch3 & "' Code, '" & Purch3 & "' As Name 
                                    UNION ALL 
                                    Select '" & Ledger & "' Code, '" & Ledger & "' As Name 
                                    UNION ALL 
                                    Select '" & LedgerHead & "' Code, '" & LedgerHead & "' As Name 
                                    UNION ALL 
                                    Select '" & LedgerHeadDetail & "' Code, '" & LedgerHeadDetail & "' As Name "
                            TxtExportSpecific.AgHelpDataSet(0) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FExportPartyData_Monark()
        Try
            mQry = " SELECT CASE WHEN Ag.NAME = 'SUNDRY DEBTORS' THEN 'Customer'
	                     WHEN Ag.NAME = 'SUNDRY CREDITORS' THEN 'Supplier' ELSE '' END AS [Party Type], 
	                Sg.SubCode AS [Code], IsNull(Sg.SUB_NAME,'') AS [Display Name], Sg.SUB_NAME AS [Name], 
	                IsNull(Sg.ADDRESS1,'') + IsNull(Sg.ADDRESS2,'') + IsNull(Sg.ADDRESS3,'') AS [Address],
	                IsNull(C.CityName,'') AS [City], IsNull(S.StateName,'') AS [State], IsNull(Sg.PINCode,'') AS [Pin No], 
	                IsNull(Sg.Phone,'') AS [Contact No], 
	                IsNull(Sg.Mobile,'') AS [Mobile], 
	                IsNull(Sg.EMail,'') AS [EMail], IsNull(Ag.NAME,'') AS [Account Group], 
	                CASE WHEN IsNull(Sg.GST_NO,'') <> '' THEN 'Registered'
	                        ELSE 'Unregistered' END AS [Sales Tax Group],
	                0 AS [Credit Days], IsNull(Sg.Limit,0) AS [Credit Limit], 
	                IsNull(Sg.C_PERSON,'') AS [Contact Person], 
	                IsNull(Sg.GST_NO,'') AS [Sales Tax No], 
	                IsNull(Sg.PAN_NO,'') AS [PAN No], IsNull(Sg.AAdhar_Card,'') AS [Aadhar No], 
	                '' AS [Master Party], IsNull(A.AreaName,'') AS [Area],
	                IsNull(Broker.Broker_Name,'') AS [Agent], IsNull(Transporter.TransportName,'') AS [Transporter], 0 AS Distance, 
	                Convert(NVARCHAR,Sg.SUBCODE) AS OMSId
	                FROM SUBGROUP Sg
	                LEFT JOIN CityMaster C ON Sg.city_code = C.CityCode
	                LEFT JOIN StateMasterNEW S ON C.StateCode = S.StateCode
	                LEFT JOIN AcGroup Ag ON Sg.Group_Code = Ag.Code
	                --LEFT JOIN SubGroup MasterParty ON Sg.MasterParty = MasterParty.SubCode
	                LEFT JOIN AreaMaster A ON Sg.Area_code = A.AreaCode
	                LEFT JOIN BrokerMaster Broker ON Sg.BrokerCode = Broker.Broker_Code
	                LEFT JOIN TransportMaster Transporter ON Sg.TRANSPORT_CODE = Transporter.TransportCode   
	
	                UNION ALL 
	
	                SELECT 'Transporter' AS [Party Type], 
	                Sg.TransportCode AS [Code], IsNull(Sg.TransportName,'') AS [Display Name], Sg.TransportName AS [Name], 
	                IsNull(Sg.ADDRESS,'') AS [Address],
	                '' AS [City], '' AS [State], '' AS [Pin No], 
	                IsNull(Sg.PhoneNo,'') AS [Contact No], 
	                '' AS [Mobile], 
	                '' AS [EMail], 'Transporter' AS [Account Group], 
	                CASE WHEN IsNull(Sg.TGSTIN_NO,'') <> '' THEN 'Registered'
	                        ELSE 'Unregistered' END AS [Sales Tax Group],
	                0 AS [Credit Days], 0 AS [Credit Limit], 
	                '' AS [Contact Person], 
	                IsNull(Sg.TGSTIN_NO,'') AS [Sales Tax No], 
	                '' AS [PAN No], '' AS [Aadhar No], 
	                '' AS [Master Party], '' AS [Area],
	                '' AS [Agent], '' AS [Transporter], 0 AS Distance, 'TR' + '-' + Convert(NVARCHAR,Sg.TransportCode) AS OMSId
	                FROM TransportMaster Sg 
	
	                UNION ALL 
	
	                SELECT 'Sales Agent' AS [Party Type], 
	                Sg.Broker_Code AS [Code], IsNull(Sg.Broker_Name,'') AS [Display Name], Sg.Broker_Name AS [Name], 
	                IsNull(Sg.ADDRESS,'') AS [Address],
	                '' AS [City], '' AS [State], '' AS [Pin No], 
	                IsNull(Sg.Phone,'') AS [Contact No], 
	                '' AS [Mobile], 
	                '' AS [EMail], 'Transporter' AS [Account Group], 
	                'Unregistered' AS [Sales Tax Group],
	                0 AS [Credit Days], 0 AS [Credit Limit], 
	                '' AS [Contact Person], 
	                '' AS [Sales Tax No], 
	                '' AS [PAN No], '' AS [Aadhar No], 
	                '' AS [Master Party], '' AS [Area],
	                '' AS [Agent], '' AS [Transporter], 0 AS Distance, 'BR' + '-' + Convert(NVARCHAR,Sg.Broker_Code) AS OMSId
	                FROM BrokerMaster Sg 

                    UNION ALL

                    SELECT 'Employee' AS [Party Type], 
                    Sg.Rep_Code AS [Code], IsNull(Sg.Rep_Name,'') AS [Display Name], Sg.Rep_Name AS [Name], 
                    IsNull(Sg.ADDRESS,'') AS [Address],
                    '' AS [City], '' AS [State], '' AS [Pin No], 
                    IsNull(Sg.Phone,'') AS [Contact No], 
                    '' AS [Mobile], 
                    '' AS [EMail], 'Employee' AS [Account Group], 
                    'Unregistered' AS [Sales Tax Group],
                    0 AS [Credit Days], 0 AS [Credit Limit], 
                    '' AS [Contact Person], 
                    '' AS [Sales Tax No], 
                    '' AS [PAN No], '' AS [Aadhar No], 
                    '' AS [Master Party], '' AS [Area],
                    '' AS [Agent], '' AS [Transporter], 0 AS Distance, 'RP' + '-' + Convert(NVARCHAR,Sg.Rep_Code) AS OMSId
                    FROM REPRESENTATIVE1  Sg 
	
	                UNION ALL 
	
	                SELECT DISTINCT 'Ledger Account' AS [Party Type], 
	                V1.Code AS [Code], IsNull(V1.Name,'') AS [Display Name], V1.Name AS [Name], 
	                '' AS [Address],
	                '' AS [City], '' AS [State], '' AS [Pin No], 
	                '' AS [Contact No], 
	                '' AS [Mobile], 
	                '' AS [EMail], IsNull(V1.Name,'') AS [Account Group], 
	                'Unregistered'  AS [Sales Tax Group],
	                0 AS [Credit Days], 0 AS [Credit Limit], 
	                '' AS [Contact Person], 
	                '' AS [Sales Tax No], 
	                '' AS [PAN No], '' AS [Aadhar No], 
	                '' AS [Master Party], '' AS [Area],
	                '' AS [Agent], '' AS [Transporter], 0 AS Distance, 'AG-' + '-' + Convert(NVARCHAR,V1.Code) AS OMSId 
	                FROM (
	                    SELECT Ag1.CODE AS Code, CASE WHEN Sg.SUB_NAME IS NULL THEN Ag1.NAME ELSE NULL END AS Name
	                    FROM Ledger L
	                    LEFT JOIN SubGroup Sg ON L.SUBCODE = Sg.SUBCODE
	                    LEFT JOIN ACGROUP Ag1 ON L.CODE = Ag1.CODE
	                    UNION ALL 
	                    SELECT Ag.CODE AS Code, CASE WHEN Contra.SUB_NAME IS NULL THEN Ag.NAME ELSE NULL END AS Name
	                    FROM Ledger L
	                    LEFT JOIN SubGroup Contra ON L.CONTRASUBCODE = Contra.SubCode
	                    LEFT JOIN ACGROUP Ag ON L.CONTRACODE = Ag.CODE
	                ) AS V1
	                WHERE V1.Name IS NOT NULL "
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + "\" + Party + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + Party + "." + ex.Message)
        End Try
    End Sub
    Private Sub FExportItemData_Monark()
        Try
            mQry = " Select I.GPROD_CODE As [Item Code], 
                    Convert(NVARCHAR(100),RTRIM(LTRIM(I.Product_Name))) + Space(10) + '[' + IsNull(Convert(NVARCHAR(100),RTRIM(LTRIM(Gm.Category_Name))),'N.A.') + ' | ' + IsNull(Convert(NVARCHAR(100),RTRIM(LTRIM(Mm.T_Product_Group_Name))),'N.A.') + ']' AS [Item Name], 
                    Convert(NVARCHAR(100),RTRIM(LTRIM(I.Product_Name))) + Space(10) + '[' + IsNull(Convert(NVARCHAR(100),RTRIM(LTRIM(Gm.Category_Name))),'N.A.') + ' | ' + IsNull(Convert(NVARCHAR(100),RTRIM(LTRIM(Mm.T_Product_Group_Name))),'N.A.') + ']' AS [Item Display Name], 
                    IsNull(Gm.Category_Name,'N.A.') AS [Item Group], 
                    IsNull(Mm.T_Product_Group_Name,'N.A.') AS [Item Category], 
                    RTRIM(LTRIM(I.Product_Name)) AS [Specification], 
                    CASE WHEN I.Unit = 'Mtr' THEN 'Meter' 
                    WHEN I.Unit = 'PCS. ' THEN 'Pcs' 
                    WHEN I.Unit IS NULL THEN 'Pcs' 
                    ELSE I.Unit END AS [Unit], I.PurchaseRate AS [Purchase Rate], 
                    IsNull(IsNull(I.NewSaleRate, I.BaseSaleRate),0) AS [Sale Rate], 
                    'GST 5%' AS [Sales Tax Group], IsNull(I.COMODITY_CODE,'') AS [HSN Code]
                    FROM ProductMaster I 
                    LEFT JOIN Product_Group Mm ON I.Group_Code = Mm.N_Product_Group_Code
                    LEFT JOIN categorymaster Gm ON I.Category_Code = Gm.Category_Code

                    UNION ALL 


                    SELECT Row_number() OVER (ORDER BY Max(I.Product_Name)) AS [Item Code], 
                    RTRIM(LTRIM(Max(I.Product_Name))) + Space(10) + '[' + IsNull(RTRIM(LTRIM(Max(Gm.Category_Name))),'N.A.') + ' | ' + IsNull(RTRIM(LTRIM(Max(Mm.T_Product_Group_Name))),'N.A.') + ']' AS [Item Name],
                    RTRIM(LTRIM(Max(I.Product_Name))) + Space(10) + '[' + IsNull(RTRIM(LTRIM(Max(Gm.Category_Name))),'N.A.') + ' | ' + IsNull(RTRIM(LTRIM(Max(Mm.T_Product_Group_Name))),'N.A.') + ']' AS [Item Display Name],
                    IsNull(Max(Gm.Category_Name),'N.A.')AS [Item Group], 
                    IsNull(Max(Mm.T_Product_Group_Name),'N.A.') AS [Item Category], 
                    RTRIM(LTRIM(Max(I.Product_Name))) AS [Specification], 
                    Max(CASE WHEN I.Unit = 'Mtr' THEN 'Meter' 
                    WHEN I.Unit = 'PCS. ' THEN 'Pcs' 
                    WHEN I.Unit IS NULL THEN 'Pcs' 
                    ELSE I.Unit END) AS [Unit], Max(I.PurchaseRate) AS [Purchase Rate], 
                    Max(IsNull(IsNull(I.NewSaleRate, I.BaseSaleRate),0)) AS [Sale Rate], 
                    'GST 5%' AS [Sales Tax Group], IsNull(MAx(I.COMODITY_CODE),'') AS [HSN Code]
                    FROM STOCK_1 H 
                    LEFT JOIN STOCK_2 L ON L.V_TYPE = H.V_TYPE AND L.V_NO = H.V_NO
                    LEFT JOIN ProductMaster I ON L.PROD_CODE = I.GPROD_CODE
                    LEFT JOIN Product_Group Mm ON I.Group_Code = Mm.N_Product_Group_Code
                    LEFT JOIN categorymaster Gm ON Gm.Category_Code = L.Category_code
                    WHERE L.V_NO IS NOT NULL
                    AND IsNull(L.Category_code,0) <> IsNull(I.Category_code,0)
                    AND H.V_Type IN ('TI','TR','PG','RG')
                    GROUP BY RTRIM(LTRIM(I.Product_Name)) + Space(10) + '[' + IsNull(RTRIM(LTRIM(Gm.Category_Name)),'') + ' | ' + IsNull(RTRIM(LTRIM(Mm.T_Product_Group_Name)),'') + ']'  "
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + "\" + Item + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + Item + "." + ex.Message)
        End Try
    End Sub

    Private Sub FExportSale1Data_Monark()
        Try
            mQry = " SELECT CASE WHEN H.V_Type = 'TI' THEN 'SI' 
		                        WHEN H.V_Type = 'TR' THEN 'SR' END AS V_TYPE,  
                        H.V_No AS V_No,
                        Convert(NVARCHAR,H.V_Date) AS V_Date,
                        Convert(NVARCHAR,H.V_No) AS [Invoice No], 
                        Party.SUB_NAME AS [Sale To Party],
                        IsNull(Party.ADDRESS1,'') + IsNull(Party.ADDRESS2,'') + IsNull(Party.ADDRESS3,'') + IsNull(Party.ADDRESS4,'') AS [Sale To Party Address],
                        IsNull(C.CityName,'') AS [Sale To Party City], IsNull(Party.PINCODE,'') AS [Sale To Party Pincode],
                        IsNull(Party.GST_NO,'') AS [Sale To Party Sales Tax No], IsNull(Party.SUB_NAME,'') AS [Bill To Party],
                        IsNull(Broker.Broker_Name,'') AS [Agent], '' AS [Rate Type],
                        CASE WHEN IsNull(Party.GST_NO,'') <> '' THEN 'Registered' 
                                ELSE 'Unregistered'
                                END AS [Sales Tax Group Party],
                        CASE WHEN H.TAXTYPE = 'EX' THEN 'Outside State' ELSE 'Within State' END AS [Place Of Supply],
                        '' AS [Sale To Party Doc No],
                        '' AS [Sale To Party Doc Date],
                        IsNull(H.Remark,'') AS [Remark],'' AS [Terms And Conditions],
                        0 AS [Credit Limit],
                        0AS [Credit Days],
                        IsNull(H.TAmount,0) AS [SubTotal1],0 AS [Deduction_Per], 
                        IsNull(H.DeductionAmt,0) + IsNull(H.DiscAmt,0) AS [Deduction],0 AS [Other_Charge_Per],
                        IsNull(H.AdditionAmt,0) + IsNull(H.AdditionChargesAmt,0) AS [Other_Charge], H.RoundOff AS [Round_Off], H.GrandTotal AS [Net_Amount]
                        FROM STOCK_1 H 
                        LEFT JOIN SubGroup Party ON H.SUBCODE = Party.SubCode
                        LEFT JOIN CityMaster C ON Party.city_code= C.CityCode
                        LEFT JOIN BrokerMaster Broker ON H.Broker_Code = Broker.Broker_Code
                        LEFT JOIN (SELECT V_Type, V_No, Count(*) AS Cnt FROM Stock_2 GROUP BY V_Type, V_No) VSale2 ON H.V_Type = VSale2.V_Type
			                        AND H.V_No = VSale2.V_No
                        WHERE H.V_Type IN ('TI','TR')
                        AND VSale2.V_No IS NOT NULL
                        ORDER BY H.V_TYPE, H.V_NO  "
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + "\" + Sale1 + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + Sale1 + "." + ex.Message)
        End Try
    End Sub
    Private Sub FExportSale2Data_Monark()
        Try
            mQry = " SELECT CASE WHEN H.V_Type = 'TI' THEN 'SI' 
			        WHEN H.V_Type = 'TR' THEN 'SR' END AS V_TYPE,  
                    Convert(NVARCHAR,H.V_No) AS [Invoice No], 
                    L.SNo AS TSr,
                    RTRIM(LTRIM(I.Product_Name)) + Space(10) + '[' + IsNull(RTRIM(LTRIM(Gm.Category_Name)),'N.A.') + ' | ' + IsNull(RTRIM(LTRIM(Mm.T_Product_Group_Name)),'N.A.') + ']' AS [Item Name],
                    '' AS [Specification],
                    CASE WHEN IsNull(L.CGSTPER,0) = 0 THEN 'GST 0%'
                         WHEN IsNull(L.CGSTPER,0) = 2.5 OR IsNull(L.CGSTPER,0) = 5 THEN 'GST 5%'
                         WHEN IsNull(L.CGSTPER,0) = 6 OR IsNull(L.CGSTPER,0) = 12 THEN 'GST 12%'
                         WHEN IsNull(L.CGSTPER,0) = 9 OR IsNull(L.CGSTPER,0) = 18 THEN 'GST 18%'
                         WHEN IsNull(L.CGSTPER,0) = 14 OR IsNull(L.CGSTPER,0) = 28 THEN 'GST 28%'
                         ELSE 'GST 5%' END AS [Sales Tax Group Item],
                    L.Pcs AS [Qty],
                    IsNull(I.Unit,'Pcs') AS [Unit],
                    L.PCS AS [Pcs],
                    1 AS [Unit Multiplier],
                    IsNull(I.Unit,'') AS [Deal Unit],
                    L.Pcs AS [Deal Qty],
                    L.Rate AS [Rate],
                    0 AS [Discount Per],
                    IsNull(L.Disc_Amt2,0) AS [Discount Amount],
                    0 AS [Additional Discount Per],
                    0 AS [Additional Discount Amount],
                    L.Amount AS [Amount],
                    '' AS [Remark],
                    '' AS [Bale No],
                    '' AS [Lot No],
                    L.Amount AS [Gross_Amount],
                    L.Amount AS [Taxable_Amount],
                    CASE WHEN L.SGSTPER = 0 THEN L.CGSTPER ELSE 0 END AS [Tax1_Per],
                    CASE WHEN L.SGSTPER = 0 THEN L.CGSTVALUE ELSE 0 END AS [Tax1_Per],
                    CASE WHEN L.SGSTPER <> 0 THEN L.CGSTPER ELSE 0 END AS [Tax2_Per],
                    CASE WHEN L.SGSTPER <> 0 THEN L.CGSTVALUE ELSE 0 END AS [Tax2_Per],
                    CASE WHEN L.SGSTPER <> 0 THEN L.SGSTPER ELSE 0 END AS [Tax3_Per],
                    CASE WHEN L.SGSTPER <> 0 THEN L.SGSTVALUE ELSE 0 END AS [Tax3_Per],
                    0 AS [Tax4_Per],
                    0 AS [Tax4],
                    0 AS [Tax5_Per],
                    0 AS [Tax5],
                    L.Amount + IsNull(L.CGSTVALUE,0) + IsNull(L.SGSTVALUE,0) AS [SubTotal1]
                    FROM STOCK_1 H 
                    LEFT JOIN STOCK_2 L ON L.V_TYPE = H.V_TYPE AND L.V_NO = H.V_NO
                    LEFT JOIN ProductMaster I ON L.PROD_CODE = I.GPROD_CODE
                    LEFT JOIN Product_Group Mm ON I.Group_Code = Mm.N_Product_Group_Code
                    LEFT JOIN categorymaster Gm ON Gm.Category_Code = IsNull(L.Category_code,I.Category_Code)
                    WHERE L.V_NO IS NOT NULL
                    AND H.V_Type IN ('TI','TR')
                    --AND H.V_DATE <= '30/Apr/2018'
                    --AND H.V_DATE > '30/Apr/2018' AND H.V_DATE <= '30/Jun/2018'
                    AND H.V_DATE > '30/Jun/2018'
                    ORDER BY H.V_TYPE, H.V_NO, L.SNo "
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + "\" + Sale2 + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + Sale2 + "." + ex.Message)
        End Try
    End Sub
    Private Sub FExportSale3Data_Monark()
        Try
            mQry = " SELECT H.V_Type AS [V_TYPE],
                    H.V_No AS [Invoice No], L.Sno AS [TSr], 
                    Row_number() OVER (PARTITION BY L.V_TYPE, L.V_NO, L.Sno ORDER BY L.V_TYPE, L.V_NO, L.Sno) AS [Sr],
                    '' AS [Specification], L.PCS1 AS [Pcs], L.MTR1 AS [Qty], 0 AS [TotalQty]
                    FROM STOCK_3 L 
                    LEFT JOIN STOCK_2 S2 ON L.V_TYPE = S2.V_TYPE AND L.V_NO = S2.V_NO AND L.Sno = S2.SNo
                    LEFT JOIN STOCK_1 H ON L.V_TYPE = H.V_TYPE AND L.V_NO = H.V_NO
                    WHERE H.V_Type IN ('TI','TR')
                    ORDER BY L.V_TYPE, L.V_NO, L.Sno "
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + "\" + Sale3 + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + Sale3 + "." + ex.Message)
        End Try
    End Sub
    Private Sub FExportPurch1Data_Monark()
        Try
            mQry = " SELECT CASE WHEN H.V_Type = 'PG' THEN 'PI' 
		                    WHEN H.V_Type = 'RG' THEN 'PR' END AS V_TYPE,  
                    H.V_No AS V_No,
                    Convert(NVARCHAR,H.V_Date) AS V_Date,
                    Convert(NVARCHAR,H.V_No) AS [Invoice No], 
                    Party.SUB_NAME AS [Vendor],
                    IsNull(Party.ADDRESS1,'') + IsNull(Party.ADDRESS2,'') + IsNull(Party.ADDRESS3,'') + IsNull(Party.ADDRESS4,'') AS [Vendor Address],
                    IsNull(C.CityName,'') AS [Vendor City], IsNull(Party.PINCODE,'') AS [Vendor Pincode],
                    IsNull(Party.Mobile,'') AS [Vendor Mobile],
                    IsNull(Party.GST_NO,'') AS [Vendor Sales Tax No], 
                    IsNull(H.Bill_No,'') AS [Vendor Doc No], IsNull(H.Bill_Date,'') AS [Vendor Doc Date],
                    IsNull(Party.SUB_NAME,'') AS [Bill To Party],
                    IsNull(Broker.Broker_Name,'') AS [Agent], 
                    CASE WHEN IsNull(Party.GST_NO,'') <> '' THEN 'Registered' 
                            ELSE 'Unregistered'
                            END AS [Sales Tax Group Party],
                    CASE WHEN H.TAXTYPE = 'EX' THEN 'Outside State' ELSE 'Within State' END AS [Place Of Supply],
                    '' AS [Ship To Address],
                    IsNull(H.Remark,'') AS [Remark],'' AS [Terms And Conditions],
                    IsNull(H.TAmount,0) AS [SubTotal1],0 AS [Deduction_Per], 
                    IsNull(H.DeductionAmt,0) + IsNull(H.DiscAmt,0) AS [Deduction],0 AS [Other_Charge_Per],
                    IsNull(H.AdditionAmt,0) + IsNull(H.AdditionChargesAmt,0) AS [Other_Charge], 
                    H.RoundOff AS [Round_Off], H.GrandTotal AS [Net_Amount]
                    FROM STOCK_1 H 
                    LEFT JOIN SubGroup Party ON H.SUBCODE = Party.SubCode
                    LEFT JOIN CityMaster C ON Party.city_code= C.CityCode
                    LEFT JOIN BrokerMaster Broker ON H.Broker_Code = Broker.Broker_Code
                    LEFT JOIN (SELECT V_Type, V_No, Count(*) AS Cnt FROM Stock_2 GROUP BY V_Type, V_No) VSale2 ON H.V_Type = VSale2.V_Type
			                    AND H.V_No = VSale2.V_No
                    WHERE H.V_Type IN ('PG','RG')
                    AND VSale2.V_No IS NOT NULL
                    ORDER BY H.V_TYPE, H.V_NO  "
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + "\" + Purch1 + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + Purch1 + "." + ex.Message)
        End Try
    End Sub
    Private Sub FExportPurch2Data_Monark()
        Try
            mQry = " SELECT CASE WHEN H.V_Type = 'PG' THEN 'PI' 
			                        WHEN H.V_Type = 'RG' THEN 'PR' END AS V_TYPE,  
                        Convert(NVARCHAR,H.V_No) AS [Invoice No], 
                        L.SNo AS TSr,
                        RTRIM(LTRIM(I.Product_Name)) + Space(10) + '[' + IsNull(RTRIM(LTRIM(Gm.Category_Name)),'N.A.') + ' | ' + IsNull(RTRIM(LTRIM(Mm.T_Product_Group_Name)),'N.A.') + ']' AS [Item Name],
                        '' AS [Specification],
                        '' AS [Bale No],
                        CASE WHEN IsNull(L.CGSTPER,0) = 0 THEN 'GST 0%'
                             WHEN IsNull(L.CGSTPER,0) = 2.5 OR IsNull(L.CGSTPER,0) = 5 THEN 'GST 5%'
                             WHEN IsNull(L.CGSTPER,0) = 6 OR IsNull(L.CGSTPER,0) = 12 THEN 'GST 12%'
                             WHEN IsNull(L.CGSTPER,0) = 9 OR IsNull(L.CGSTPER,0) = 18 THEN 'GST 18%'
                             WHEN IsNull(L.CGSTPER,0) = 14 OR IsNull(L.CGSTPER,0) = 28 THEN 'GST 28%'
                             ELSE 'GST 5%' END AS [Sales Tax Group Item],
                        0 AS [Profit Margin Per],
                        L.Pcs AS [Qty],
                        IsNull(I.Unit,'Pcs') AS [Unit],
                        L.PCS AS [Pcs],
                        IsNull(I.Unit,'Pcs')  AS [Deal Unit],
                        L.Pcs AS [Deal Qty],
                        L.Rate AS [Rate],
                        0 AS [Discount Per],
                        IsNull(L.Disc_Amt2,0) AS [Discount Amount],
                        0 AS [Additional Discount Per],
                        0 AS [Additional Discount Amount],
                        L.Amount AS [Amount],
                        '' AS [Remark],
                        '' AS [LR No],
                        '' AS [LR Date],
                        '' AS [Lot No],
                        L.Amount AS [Gross_Amount],
                        L.Amount AS [Taxable_Amount],
                        CASE WHEN L.SGSTPER = 0 THEN L.CGSTPER ELSE 0 END AS [Tax1_Per],
                        CASE WHEN L.SGSTPER = 0 THEN L.CGSTVALUE ELSE 0 END AS [Tax1],
                        CASE WHEN L.SGSTPER <> 0 THEN L.CGSTPER ELSE 0 END AS [Tax2_Per],
                        CASE WHEN L.SGSTPER <> 0 THEN L.CGSTVALUE ELSE 0 END AS [Tax2],
                        CASE WHEN L.SGSTPER <> 0 THEN L.SGSTPER ELSE 0 END AS [Tax3_Per],
                        CASE WHEN L.SGSTPER <> 0 THEN L.SGSTVALUE ELSE 0 END AS [Tax3],
                        0 AS [Tax4_Per],
                        0 AS [Tax4],
                        0 AS [Tax5_Per],
                        0 AS [Tax5],
                        L.Amount + IsNull(L.CGSTVALUE,0) + IsNull(L.SGSTVALUE,0) AS [SubTotal1]
                        FROM STOCK_1 H 
                        LEFT JOIN STOCK_2 L ON L.V_TYPE = H.V_TYPE AND L.V_NO = H.V_NO
                        LEFT JOIN ProductMaster I ON L.PROD_CODE = I.GPROD_CODE
                        LEFT JOIN Product_Group Mm ON I.Group_Code = Mm.N_Product_Group_Code
                        LEFT JOIN categorymaster Gm ON Gm.Category_Code = IsNull(L.Category_code,I.Category_Code)
                        WHERE L.V_NO IS NOT NULL
                        AND H.V_Type IN ('PG','RG')
                        ORDER BY H.V_TYPE, H.V_NO, L.SNo "
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + "\" + Purch2 + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + Purch2 + "." + ex.Message)
        End Try
    End Sub
    Private Sub FExportPurch3Data_Monark()
        Try
            mQry = " SELECT CASE WHEN H.V_Type = 'TI' THEN 'SI' 
			                        WHEN H.V_Type = 'TR' THEN 'SR' END AS V_TYPE,  
                        H.V_No AS [Invoice No], L.Sno AS [TSr], 
                        Row_number() OVER (PARTITION BY L.V_TYPE, L.V_NO, L.Sno ORDER BY L.V_TYPE, L.V_NO, L.Sno) AS [Sr],
                        '' AS [Specification], L.PCS1 AS [Pcs], L.MTR1 AS [Qty], 0 AS [TotalQty]
                        FROM STOCK_3 L 
                        LEFT JOIN STOCK_2 S2 ON L.V_TYPE = S2.V_TYPE AND L.V_NO = S2.V_NO AND L.Sno = S2.SNo
                        LEFT JOIN STOCK_1 H ON L.V_TYPE = H.V_TYPE AND L.V_NO = H.V_NO
                        WHERE H.V_Type IN ('PG','RG')
                        ORDER BY L.V_TYPE, L.V_NO, L.Sno "
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + "\" + Purch3 + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + Purch3 + "." + ex.Message)
        End Try
    End Sub
    Private Sub FExportLedgerData_Monark()
        Try
            mQry = " SELECT L.v_type AS V_Type, L.V_No AS V_No,L.V_Date, 
                    IsNull(Sg.SUB_NAME,Ag1.NAME) AS [Ledger Account Name], IsNull(Contra.SUB_NAME, Ag.NAME) AS [Contra Ledger Account Name],
                    IsNull(L.Narration,'') AS Narration, IsNull(L.CHEQUE_NO,'') AS [Chq No] , IsNull(L.CHEQUE_DATE,'') AS [Chq Date], 
                    L.CR AS AmtCr , 0 AS AmtDr
                    FROM Ledger L
                    LEFT JOIN SubGroup Sg ON L.SUBCODE = Sg.SUBCODE
                    LEFT JOIN SubGroup Contra ON L.CONTRASUBCODE = Contra.SubCode
                    LEFT JOIN ACGROUP Ag ON L.CONTRACODE = Ag.CODE
                    LEFT JOIN ACGROUP Ag1 ON L.CODE = Ag1.CODE
                    LEFT JOIN TYPES Vt ON L.v_type = Vt.TYPE_CODE
                    WHERE L.v_type IN ('BP','CP')
                    AND L.V_DATE >= '01/Apr/2018'

                    UNION ALL 

                    SELECT L.v_type AS V_Type, L.V_No AS V_No,L.V_Date, 
                    IsNull(Contra.SUB_NAME,Ag.NAME)  AS [Ledger Account Name], IsNull(Sg.SUB_NAME,Ag1.NAME) AS [Contra Ledger Account Name],
                    IsNull(L.Narration,'') AS Narration, IsNull(L.CHEQUE_NO,'') AS [Chq No] , IsNull(L.CHEQUE_DATE,'') AS [Chq Date] , 
                    0 AS AmtCr , L.CR AS AmtDr 
                    FROM Ledger L
                    LEFT JOIN SubGroup Sg ON L.SUBCODE = Sg.SUBCODE
                    LEFT JOIN SubGroup Contra ON L.CONTRASUBCODE = Contra.SubCode
                    LEFT JOIN ACGROUP Ag ON L.CONTRACODE = Ag.CODE
                    LEFT JOIN ACGROUP Ag1 ON L.CODE = Ag1.CODE
                    LEFT JOIN TYPES Vt ON L.v_type = Vt.TYPE_CODE
                    WHERE L.v_type IN ('BP','CP')
                    AND L.V_DATE >= '01/Apr/2018'

                    UNION ALL 

                    SELECT CASE WHEN Vt.T_NAME = 'PR' THEN 'BR' ELSE L.v_type END AS V_Type, 
                    L.V_No AS V_No,L.V_Date, 
                    IsNull(Sg.SUB_NAME,Ag1.NAME) AS [Ledger Account Name], IsNull(Contra.SUB_NAME,Ag.NAME) AS [Contra Ledger Account Name],
                    IsNull(L.Narration,'') AS Narration, IsNull(L.CHEQUE_NO,'') AS [Chq No] , IsNull(L.CHEQUE_DATE,'') AS [Chq Date] , 
                    L.CR AS AmtCr , 0 AS AmtDr 
                    FROM Ledger L
                    LEFT JOIN SubGroup Sg ON L.SUBCODE = Sg.SUBCODE
                    LEFT JOIN SubGroup Contra ON L.CONTRASUBCODE = Contra.SubCode
                    LEFT JOIN ACGROUP Ag ON L.CONTRACODE = Ag.CODE
                    LEFT JOIN ACGROUP Ag1 ON L.CODE = Ag1.CODE
                    LEFT JOIN TYPES Vt ON L.v_type = Vt.TYPE_CODE
                    WHERE L.v_type IN ('BR','CR','PR')
                    AND L.V_DATE >= '01/Apr/2018'

                    UNION ALL 

                    SELECT CASE WHEN Vt.T_NAME = 'PR' THEN 'BR' ELSE L.v_type END AS V_Type, 
                    L.V_No AS V_No,L.V_Date, 
                    IsNull(Contra.SUB_NAME,Ag.NAME) AS [Ledger Account Name], IsNull(Sg.SUB_NAME,Ag1.NAME) AS [Contra Ledger Account Name],
                    IsNull(L.Narration,'') AS Narration, IsNull(L.CHEQUE_NO,'') AS [Chq No] , IsNull(L.CHEQUE_DATE,'') AS [Chq Date] , 
                    0 AS AmtCr , L.CR AS AmtDr 
                    FROM Ledger L
                    LEFT JOIN SubGroup Sg ON L.SUBCODE = Sg.SUBCODE
                    LEFT JOIN SubGroup Contra ON L.CONTRASUBCODE = Contra.SubCode
                    LEFT JOIN ACGROUP Ag ON L.CONTRACODE = Ag.CODE
                    LEFT JOIN ACGROUP Ag1 ON L.CODE = Ag1.CODE
                    LEFT JOIN TYPES Vt ON L.v_type = Vt.TYPE_CODE
                    WHERE L.v_type IN ('BR','CR','PR')
                    AND L.V_DATE >= '01/Apr/2018'


                    UNION ALL 

                    SELECT 'JV' AS V_Type, L.V_No AS V_No,L.V_Date, 
                    IsNull(Sg.SUB_NAME,IsNull(Ag1.NAME,IsNull(Contra.SUB_NAME,Ag.NAME))) AS [Ledger Account Name], '' AS [Contra Ledger Account Name],
                    IsNull(L.Narration,'') AS Narration, IsNull(L.CHEQUE_NO,'') AS [Chq No] , IsNull(L.CHEQUE_DATE,'') AS [Chq Date] , 
                    CASE WHEN L.CR_DR = 'Cr' THEN L.CR ELSE 0 END AS AmtCr,
                    CASE WHEN L.CR_DR = 'Dr' THEN L.CR ELSE 0 END AS AmtDr
                    FROM Ledger L
                    LEFT JOIN SubGroup Sg ON L.SUBCODE = Sg.SUBCODE
                    LEFT JOIN SubGroup Contra ON L.CONTRASUBCODE = Contra.SubCode
                    LEFT JOIN ACGROUP Ag ON L.CONTRACODE = Ag.CODE
                    LEFT JOIN ACGROUP Ag1 ON L.CODE = Ag1.CODE
                    LEFT JOIN TYPES Vt ON L.v_type = Vt.TYPE_CODE
                    WHERE L.v_type IN ('JN') 
                    AND L.V_DATE >= '01/Apr/2018'
                    ORDER BY L.v_no "
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + "\" + Ledger + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + Ledger + "." + ex.Message)
        End Try
    End Sub
    Private Sub FExportLedgerHeadData_Monark()
        Try
            mQry = " SELECT CASE WHEN Vt.Description = 'CREDIT NOTE [P]' THEN 'CNC'
                    WHEN Vt.Description = 'DEBIT NOTE [R]' THEN 'DNS' END AS [V_TYPE],
                    Vt.Description,
                    L.V_NO AS [V_NO],
                    Convert(NVARCHAR,L.V_Date) AS V_Date,
                    L.V_NO AS [Entry No],
                    Sg.SUB_NAME AS [Party Name],
                    Sg.Name AS PartyName, Sg.Address AS PartyAddress, Sg.PIN AS PartyPinCode, 
                    Sg.CityCode AS PartyCity, Sg.Mobile AS PartyMobile, Sgr.RegistrationNo AS PartySalesTaxNo,
                    CASE WHEN C.State =  'D10009' THEN 'Within State' ELSE 'Outside State' END  AS PlaceOfSupply
                    L.TAmount AS [SubTotal1],
                    0 AS [Deduction_Per],
                    0 AS [Deduction],
                    0 AS [Other_Charge_Per],
                    0 AS [Other_Charge],
                    0 AS [Round_Off],
                    0 AS [Net_Amount]
                    FROM STOCK_1 L 
                    LEFT JOIN TYPE Vt ON L.v_type = Vt.V_Type
                    LEFT JOIN SubGroup Sg ON L.SUBCODE = Sg.SubCode
                    WHERE Vt.DESCRIPTION IN ('CREDIT NOTE [P]','DEBIT NOTE [R]') "
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + "\" + LedgerHead + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + LedgerHead + "." + ex.Message)
        End Try
    End Sub
    Private Sub FExportLedgerHeadDetailData_Monark()
        Try
            mQry = " SELECT CASE WHEN Vt.Description = 'CREDIT NOTE [P]' THEN 'CNC'
         	                        WHEN Vt.Description = 'DEBIT NOTE [R]' THEN 'DNS' END AS [V_TYPE],
                        L.V_No AS [Entry No],
                        IsNull(Sg.SUB_NAME,'') AS [Ledger Account Name],
                        '' AS Specification,
                        0 AS Qty,
                        '' AS Unit,
                        0 AS Rate,
                        IsNull(L.GrossAmount,0) AS Amount,
                        '' AS [Chq No],
                        '' AS [Chq Date],
                        '' AS [Remark],
                        IsNull(L.GrossAmount,0) AS [Gross_Amount],
                        IsNull(L.GrossAmount,0) AS [Taxable_Amount],
                        CASE WHEN L.SGSTPER = 0 THEN L.CGSTPER ELSE 0 END AS [Tax1_Per],
                        CASE WHEN L.SGSTPER = 0 THEN L.CGSTVALUE ELSE 0 END AS [Tax1],
                        CASE WHEN L.SGSTPER <> 0 THEN L.CGSTPER ELSE 0 END AS [Tax2_Per],
                        CASE WHEN L.SGSTPER <> 0 THEN L.CGSTVALUE ELSE 0 END AS [Tax2],
                        CASE WHEN L.SGSTPER <> 0 THEN L.SGSTPER ELSE 0 END AS [Tax3_Per],
                        CASE WHEN L.SGSTPER <> 0 THEN L.SGSTVALUE ELSE 0 END AS [Tax3],
                        0 AS [Tax4_Per],
                        0 AS [Tax4],
                        0 AS [Tax5_Per],
                        0 AS [Tax5],
                        IsNull(L.GrossAmount,0) + IsNull(L.CGSTVALUE,0) + IsNull(L.SGSTVALUE,0) AS [SubTotal1]
                        FROM STOCK_2 L 
                        LEFT JOIN TYPE Vt ON L.v_type = Vt.V_Type
                        LEFT JOIN SubGroup Sg ON L.SUBCODE = Sg.SubCode
                        WHERE Vt.DESCRIPTION IN ('CREDIT NOTE [P]','DEBIT NOTE [R]') "
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + "\" + LedgerHeadDetail + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + LedgerHeadDetail + "." + ex.Message)
        End Try
    End Sub

    Private Sub FExportBuiltyHead_Monark()
        Try
            mQry = " SELECT 'LR' AS [V_TYPE], H.V_No AS [Entry No],
                        Convert(NVARCHAR,H.V_Date) AS V_Date,
                        IsNull(Sg.SUB_NAME,'') AS [Party Name],
                        H.BUILTY_NO AS [Party Doc No], 
                        H.BUILTY_DATE AS [Party Doc Date],
                        Tm.TransportName AS [Transporter],
                        H.REMARK AS Remark
                        FROM BuiltyReceivedFromTransport1 H 
                        LEFT JOIN SUBGROUP  Sg ON H.PARTY_CODE = Sg.SUBCODE
                        LEFT JOIN TransportMaster Tm ON H.TRANSPORT_CODE = Tm.TransportCode
                        LEFT JOIN (SELECT V_Type, V_No, Count(*) AS Cnt FROM BuiltyReceivedFromTransport1 GROUP BY V_Type, V_No) AS VLine ON 	
		                        H.V_TYPE = VLine.V_Type AND H.V_NO = VLine.V_No
                        WHERE VLine.Cnt <> 0 "
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + " \" + LedgerHead + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + LedgerHead + "." + ex.Message)
        End Try
    End Sub
    Private Sub FExportBuiltyHeadDetail_Monark()
        Try
            mQry = " SELECT 'LR' AS [V_TYPE], H.V_No AS [Entry No], 
                    IsNull(Pm.Product_Name,'N.A.') AS [Item Name], L.MARKA_NO AS [Specification],
                    H.BUILTY_NO AS [Bale No],
                    L.BILL_NO AS [Lot No],
                    L.BILL_VALUE AS [Pcs],
                    0 AS [Qty],
                    'Nos' AS Unit,
                    L.FREIGHT AS Amount,
                    L.REMARK2 AS Remark
                    FROM BuiltyReceivedFromTransport1 H 
                    LEFT JOIN BuiltyReceivedFromTransport2 L ON H.V_TYPE = L.V_TYPE AND H.V_NO = L.V_NO
                    LEFT JOIN ProductMaster Pm ON L.PRODUCT_CODE = Pm.GPROD_CODE
                    WHERE L.V_NO IS NOT NULL "
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + "\" + LedgerHead + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + LedgerHead + "." + ex.Message)
        End Try
    End Sub

    Private Sub FExportOpeningStock()
        Try
            mQry = " SELECT '1' AS EntryNo, 'OS' AS V_Type, '31/Mar/2019' AS V_Date,
                        Max(I.Name) + Space(10) + '[' + IsNull(Max(M.Name),'') + ' | ' + IsNull(Max(G.Name),'') + ']' AS [Item Name],
                        '' AS Specification,
                        '' AS [Bale No],
                        '' AS [Lot No],
                        Sum(VMain.Qty) AS Qty, 
                        'Pcs' AS Unit,
                        0 AS Rate,
                        0 AS Amount,
                        '' AS Remark
                        --, Max(It.Description) AS ITemDescription
                        --VMain.Item, VMain.Make, VMain.Grade, Sum(VMain.Qty) AS Qty
                        FROM (
	                        SELECT L.Item, L.Make, L.Grade, -L.Qty AS Qty
	                        FROM SALE2 L 
	                        LEFT JOIN Voucher_Type Vt ON L.V_Type = Vt.V_Type
	                        WHERE L.V_DATE <= '31/Mar/2019'
	                        AND Vt.Description = 'GST PURCHASE RETRUN'
	
	                        UNION ALL 
	
	                        SELECT L.Item, L.Make, L.Grade, L.Qty AS Qty
	                        FROM SALE2 L 
	                        LEFT JOIN Voucher_Type Vt ON L.V_Type = Vt.V_Type
	                        WHERE L.V_DATE <= '31/Mar/2019'
	                        AND Vt.Description = 'GST SALE RETURN'
	
	                        UNION ALL 
	
	                        SELECT L.Item, L.Make, L.Grade, -L.Qty AS Qty
	                        FROM SALE2 L 
	                        LEFT JOIN Voucher_Type Vt ON L.V_Type = Vt.V_Type
	                        WHERE L.V_DATE <= '31/Mar/2019'
	                        AND Vt.Description = 'GST CASH SALE'
	
	                        UNION ALL 
	
	                        SELECT L.Item, L.Make, L.Grade, L.Qty AS Qty
	                        FROM SALE2 L 
	                        LEFT JOIN Voucher_Type Vt ON L.V_Type = Vt.V_Type
	                        WHERE L.V_DATE <= '31/Mar/2019'
	                        AND Vt.Description = 'GST PURCHASE'
	
	                        UNION ALL 
	
	                        SELECT L.Item, L.Make, L.Grade, -L.Qty AS Qty
	                        FROM SALE2 L 
	                        LEFT JOIN Voucher_Type Vt ON L.V_Type = Vt.V_Type
	                        WHERE L.V_DATE <= '31/Mar/2019'
	                        AND Vt.Description = 'Purchase Bill Return'
	
	                        UNION ALL 
	
	                        SELECT L.Item, L.Make, L.Grade, -L.Qty AS Qty
	                        FROM SALE2 L 
	                        LEFT JOIN Voucher_Type Vt ON L.V_Type = Vt.V_Type
	                        WHERE L.V_DATE <= '31/Mar/2019'
	                        AND Vt.Description = 'GST SALE'
                        ) AS VMain
                        LEFT JOIN Itemmast I ON VMain.Item = I.Code
                        LEFT JOIN MakeMast M ON VMain.Make = M.Code
                        LEFT JOIN GradeMast G ON VMain.Grade = G.Code
                        LEFT JOIN Sadhvi.dbo.Item It ON I.Name + Space(10) + '[' + IsNull(M.Name,'') + ' | ' + IsNull(G.Name,'') + ']' = It.Description
                        WHERE It.Description IS NOT NULL
                        GROUP BY VMain.Item, VMain.Make, VMain.Grade "
            Dim DtData As System.Data.DataTable = FillData(mQry, mConnectionStr).Tables(0)

            FWriteExcelFile(DtData, TxtExportPath.Text + " \" + LedgerHead + ".xls")
        Catch ex As Exception
            MsgBox("Error In Export" + LedgerHead + "." + ex.Message)
        End Try
    End Sub
    Private Sub FImportRateList_Monark()
        mQry = "INSERT INTO dbo.RateType (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, Margin, Sr, Discount, CalculateOnRateType, Process)
                VALUES ('D10004', 'NO LESS RATE DD', NULL, 'SUPER', '2018-12-11 12:48:15', NULL, NULL, NULL, NULL, NULL, NULL, 'Active', 'D', NULL, 6.25, 1, 0, NULL, NULL)

                INSERT INTO dbo.RateType (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, Margin, Sr, Discount, CalculateOnRateType, Process)
                VALUES ('D10003', 'BASE SALE RATE / CASH RATE', NULL, 'SUPER', '2018-12-11 12:47:54', NULL, NULL, NULL, NULL, NULL, NULL, 'Active', 'D', NULL, 0, 2, 0, NULL, NULL)

                INSERT INTO dbo.RateType (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, Margin, Sr, Discount, CalculateOnRateType, Process)
                VALUES ('D10002', 'DEALER RATE', NULL, 'SUPER', '2018-12-11 12:47:39', NULL, NULL, NULL, NULL, NULL, NULL, 'Active', 'D', NULL, 11, 3, 0, 'D10003', NULL)

                INSERT INTO dbo.RateType (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, Margin, Sr, Discount, CalculateOnRateType, Process)
                VALUES ('D10001', 'BATTA RATE', NULL, 'SUPER', '2018-12-10 19:33:45', NULL, NULL, NULL, NULL, 'SUPER', '2018-12-11 18:17:21', 'Active', 'D', NULL, 6.25, 4, 0, 'D10003', NULL)




                UPDATE Devi.dbo.Item
                SET Devi.dbo.Item.Rate = VMain.Rate_New
                FROM (
	                SELECT V1.Code, I.NoLessRate AS Rate_New, V1.Rate,
	                I.Product_Name, Mm.T_Product_Group_Name, Gm.Category_Name,
	                V1.Specification AS Item, V1.ItemGroupDesc AS ItemGroupDesc, V1.ItemCategoryDesc AS ItemCategoryDesc
	                FROM ProductMaster I 
	                LEFT JOIN Product_Group Mm ON I.Group_Code = Mm.N_Product_Group_Code
	                LEFT JOIN categorymaster Gm ON I.Category_Code = Gm.Category_Code
	                LEFT JOIN (
		                SELECT Ig.Description AS ItemGroupDesc, Ic.Description AS ItemCategoryDesc, I.*
		                FROM Devi.dbo.Item I 
		                LEFT JOIN Devi.dbo.ItemGroup Ig ON I.ItemGroup = Ig.Code
		                LEFT JOIN Devi.dbo.ItemCategory Ic ON I.ItemCategory = Ic.Code
	                ) AS V1 ON IsNull(I.Product_Name,'') = IsNull(V1.Specification,'')
			                AND IsNull(Mm.T_Product_Group_Name,'') = IsNull(V1.ItemCategoryDesc,'')
			                AND IsNull(Gm.Category_Name,'') = IsNull(V1.ItemGroupDesc,'')
	                WHERE V1.Specification IS NOT NULL
                ) AS VMain WHERE Devi.dbo.Item.Code = VMain.Code



                INSERT INTO RateList (Code, WEF, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate)
                SELECT I.Code, '01/Apr/2016', I.EntryBy, I.EntryDate, I.EntryType, I.EntryStatus, I.ApproveBy, I.ApproveDate
                FROM Item I




                INSERT INTO dbo.RateListDetail (Code, Sr, RateType, Item, Rate)
                SELECT I.Code, Rt.Sr, Rt.Code AS RateType, I.Code AS Item, 
                Round(IsNull(I.Rate,0) + (IsNull(I.Rate,0) * Rt.Margin / 100),0) AS Rate
                FROM Item I 
                LEFT JOIN RateType Rt ON 1 = 1
                WHERE Rt.Description = 'NO LESS RATE DD'
                AND IsNull(I.Rate,0) > 0



                INSERT INTO dbo.RateListDetail (Code, Sr, RateType, Item, Rate)
                SELECT V1.Code, Rt.Sr, Max(Rt.Code) AS RateType, Max(V1.Code) AS Item, 
                Max(CASE WHEN IsNull(I.BaseSaleRate,0) > 0 THEN IsNull(I.BaseSaleRate,0) ELSE IsNull(I.CashRate,0) END) AS Rate
                --I.NoLessRate AS Rate_New, V1.Rate,
                --I.Product_Name, Mm.T_Product_Group_Name, Gm.Category_Name,
                --V1.Specification AS Item, V1.ItemGroupDesc AS ItemGroupDesc, V1.ItemCategoryDesc AS ItemCategoryDesc
                FROM DEVI1920.dbo.ProductMaster I 
                LEFT JOIN DEVI1920.dbo.Product_Group Mm ON I.Group_Code = Mm.N_Product_Group_Code
                LEFT JOIN DEVI1920.dbo.categorymaster Gm ON I.Category_Code = Gm.Category_Code
                LEFT JOIN (
	                SELECT Ig.Description AS ItemGroupDesc, Ic.Description AS ItemCategoryDesc, I.*
	                FROM Devi.dbo.Item I 
	                LEFT JOIN Devi.dbo.ItemGroup Ig ON I.ItemGroup = Ig.Code
	                LEFT JOIN Devi.dbo.ItemCategory Ic ON I.ItemCategory = Ic.Code
                ) AS V1 ON IsNull(I.Product_Name,'') = IsNull(V1.Specification,'')
		                AND IsNull(Mm.T_Product_Group_Name,'') = IsNull(V1.ItemCategoryDesc,'')
		                AND IsNull(Gm.Category_Name,'') = IsNull(V1.ItemGroupDesc,'')
                LEFT JOIN RateType Rt ON 1=1
                WHERE V1.Specification IS NOT NULL
                AND Rt.Description = 'BASE SALE RATE / CASH RATE'
                GROUP BY V1.Code, Rt.Sr



                INSERT INTO dbo.RateListDetail (Code, Sr, RateType, Item, Rate)
                SELECT I.Code, Rt.Sr, Rt.Code AS RateType, I.Code AS Item, 
                Round(IsNull(Rd.Rate,0) + (IsNull(Rd.Rate,0) * Rt.Margin / 100),0) AS Rate
                FROM Item I 
                LEFT JOIN RateType Rt ON 1 = 1
                LEFT JOIN RateListDetail Rd ON I.Code = Rd.Code AND Rd.RateType = 'D10003'
                WHERE Rt.Description = 'DEALER RATE'
                AND IsNull(Rd.Rate,0) > 0



                INSERT INTO dbo.RateListDetail (Code, Sr, RateType, Item, Rate)
                SELECT I.Code, Rt.Sr, Rt.Code AS RateType, I.Code AS Item, 
                Round(IsNull(Rd.Rate,0) + (IsNull(Rd.Rate,0) * Rt.Margin / 100),0) AS Rate
                FROM Item I 
                LEFT JOIN RateType Rt ON 1 = 1
                LEFT JOIN RateListDetail Rd ON I.Code = Rd.Code AND Rd.RateType = 'D10003'
                WHERE Rt.Description = 'BATTA RATE'
                AND IsNull(Rd.Rate,0) > 0"
    End Sub


    Private Sub TxtFromSoftware_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtFromSoftware.KeyDown
        Select Case sender.Name
            Case TxtFromSoftware.Name
                If e.KeyCode <> Keys.Enter Then
                    If sender.AgHelpDataset Is Nothing Then
                        mQry = " Select '" & FromSoftware_Dataman & "' As Code, '" & FromSoftware_Dataman & "' As Name 
                                 UNION ALL 
                                 Select '" & FromSoftware_Monark & "' As Code, '" & FromSoftware_Monark & "' As Name "
                        TxtFromSoftware.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If
        End Select
    End Sub
End Class