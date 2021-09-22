Imports System.Data.OleDb
Imports System.IO

Public Class FrmCheckValidation
    Dim ErrorLog As String = ""

    Dim DtSale1V_Type As DataTable = Nothing
    Dim DtSale2V_Type As DataTable = Nothing
    Dim DtSale3V_Type As DataTable = Nothing
    Dim DtPurch1V_Type As DataTable = Nothing
    Dim DtPurch2V_Type As DataTable = Nothing
    Dim DtPurch3V_Type As DataTable = Nothing
    Dim DtLedgerV_Type As DataTable = Nothing
    Dim DtPaymentV_Type As DataTable = Nothing
    Dim DtDraftV_Type As DataTable = Nothing
    Dim DtGPurch1V_Type As DataTable = Nothing
    Dim DtGPurch2V_Type As DataTable = Nothing
    Private Sub BtnSelectExcelFile_Item_Click(sender As Object, e As EventArgs) Handles BtnSelectExcelFile_Item.Click
        If (FBDExportPath.ShowDialog() = DialogResult.OK) Then
            TxtFileLocation.Text = FBDExportPath.SelectedPath
            My.Settings.FilePath = TxtFileLocation.Text
            My.Settings.Save()
        End If
    End Sub
    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click

        FGetPartyFileValidation()
        FGetItemFileValidation()

        FGetSale1FileValidation()
        FGetSale2FileValidation()
        FGetSale3FileValidation()

        FGetPurch1FileValidation()
        FGetPurch2FileValidation()
        FGetPurch3FileValidation()

        FGetLedgerFileValidation()

        FGetPaymentFileValidation()
        FGetDraftFileValidation()

        FGetGPurch1FileValidation()
        FGetGPurch2FileValidation()

        FGetV_TypeFileValidation()
        FGetTrialBalanceFileValidation()

        If ErrorLog <> "" Then
            Dim bTempPath As String = Path.GetTempFileName()
            My.Computer.FileSystem.WriteAllText(bTempPath, ErrorLog, False)
            System.Diagnostics.Process.Start("notepad.exe", bTempPath)
        End If
    End Sub




    Private Sub FGetPartyFileValidation()
        Dim mFileName As String = "Party"
        Dim DtTemp As DataTable

        DtTemp = FGetData(mFileName)

        If DtTemp Is Nothing Then Exit Sub

        If Not DtTemp.Columns.Contains("Party_Type") Then ErrorLog += "Party_Type Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("Code") Then ErrorLog += "Code Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("DISPLAY") Then ErrorLog += "DISPLAY Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("NAME") Then ErrorLog += "NAME Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("ADDRESS") Then ErrorLog += "ADDRESS Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("CITY") Then ErrorLog += "CITY Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("STATE") Then ErrorLog += "STATE Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("PIN_NO") Then ErrorLog += "PIN_NO Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("CONTACT_NO") Then ErrorLog += "CONTACT_NO Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("MOBILE") Then ErrorLog += "MOBILE Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("EMAIL") Then ErrorLog += "EMAIL Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("ACC_GROUP") Then ErrorLog += "ACC_GROUP Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("ACC_GROUP_NATURE") Then ErrorLog += "ACC_GROUP_NATURE Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("TAX_GROUP") Then ErrorLog += "TAX_GROUP Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("CREDITDAYS") Then ErrorLog += "CREDITDAYS Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("LIMIT") Then ErrorLog += "LIMIT Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("CONTACT") Then ErrorLog += "CONTACT Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("GST_NO") Then ErrorLog += "GST_NO Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("PAN_NO") Then ErrorLog += "PAN_NO Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("AADHAR_NO") Then ErrorLog += "AADHAR_NO Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("MASTER") Then ErrorLog += "MASTER Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("AREA") Then ErrorLog += "AREA Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("AGENT") Then ErrorLog += "AGENT Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("TRANSPORT") Then ErrorLog += "TRANSPORT Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("DISTANCE") Then ErrorLog += "DISTANCE Field does not exist in " & mFileName & "  File." + vbCrLf

        If DtTemp.Columns.Contains("Party_Type") Then
            Dim DtParty_Type As DataTable = DtTemp.DefaultView.ToTable(True, "Party_Type")
            For I = 0 To DtParty_Type.Rows.Count - 1
                If XNull(DtParty_Type.Rows(I)("Party_Type")).ToString.Trim.ToUpper = "" Then
                    ErrorLog += "Some Parties have blank Party_Type in " & mFileName & " File." & vbCrLf
                ElseIf XNull(DtParty_Type.Rows(I)("Party_Type")).ToString.Trim.ToUpper <> ClsMain.SubGroupType.Customer.ToString.Trim.ToUpper And
                    XNull(DtParty_Type.Rows(I)("Party_Type")).ToString.Trim.ToUpper <> ClsMain.SubGroupType.PurchaseAgent.ToString.Trim.ToUpper And
                    XNull(DtParty_Type.Rows(I)("Party_Type")).ToString.Trim.ToUpper <> ClsMain.SubGroupType.RevenuePoint.ToString.Trim.ToUpper And
                    XNull(DtParty_Type.Rows(I)("Party_Type")).ToString.Trim.ToUpper <> ClsMain.SubGroupType.SalesAgent.ToString.Trim.ToUpper And
                    XNull(DtParty_Type.Rows(I)("Party_Type")).ToString.Trim.ToUpper <> ClsMain.SubGroupType.SalesRepresentative.ToString.Trim.ToUpper And
                    XNull(DtParty_Type.Rows(I)("Party_Type")).ToString.Trim.ToUpper <> ClsMain.SubGroupType.Supplier.ToString.Trim.ToUpper And
                    XNull(DtParty_Type.Rows(I)("Party_Type")).ToString.Trim.ToUpper <> ClsMain.SubGroupType.Transporter.ToString.Trim.ToUpper And
                    XNull(DtParty_Type.Rows(I)("Party_Type")).ToString.Trim.ToUpper <> ClsMain.SubGroupType.LedgerAccount.ToString.Trim.ToUpper Then
                    ErrorLog += "Party_Type named " & XNull(DtParty_Type.Rows(I)("Party_Type")).ToString.Trim + " found in " & mFileName & " File. It is not present in master."
                    ErrorLog += "Party_Type should be in " + ClsMain.SubGroupType.Customer + "," +
                            ClsMain.SubGroupType.PurchaseAgent + "," +
                            ClsMain.SubGroupType.RevenuePoint + "," +
                            ClsMain.SubGroupType.SalesAgent + "," +
                            ClsMain.SubGroupType.SalesRepresentative + "," +
                            ClsMain.SubGroupType.Supplier + "," +
                            ClsMain.SubGroupType.Transporter + "," +
                            ClsMain.SubGroupType.LedgerAccount & vbCrLf
                End If
            Next
        End If


        If DtTemp.Columns.Contains("ACC_GROUP_NATURE") Then
            Dim DtParty_Type As DataTable = DtTemp.DefaultView.ToTable(True, "ACC_GROUP_NATURE")
            For I = 0 To DtParty_Type.Rows.Count - 1
                If XNull(DtParty_Type.Rows(I)("ACC_GROUP_NATURE")).ToString.Trim.ToUpper = "" Then
                    ErrorLog += "Some Parties have blank ACC_GROUP_NATURE in " & mFileName & " File." & vbCrLf
                ElseIf XNull(DtParty_Type.Rows(I)("ACC_GROUP_NATURE")).ToString.Trim.ToUpper <> ClsMain.AccountGroupNature.Sales.ToString.Trim.ToUpper And
                        XNull(DtParty_Type.Rows(I)("ACC_GROUP_NATURE")).ToString.Trim.ToUpper <> ClsMain.AccountGroupNature.Purchase.ToString.Trim.ToUpper And
                        XNull(DtParty_Type.Rows(I)("ACC_GROUP_NATURE")).ToString.Trim.ToUpper <> ClsMain.AccountGroupNature.Bank.ToString.Trim.ToUpper And
                        XNull(DtParty_Type.Rows(I)("ACC_GROUP_NATURE")).ToString.Trim.ToUpper <> ClsMain.AccountGroupNature.Cash.ToString.Trim.ToUpper And
                        XNull(DtParty_Type.Rows(I)("ACC_GROUP_NATURE")).ToString.Trim.ToUpper <> ClsMain.AccountGroupNature.Customer.ToString.Trim.ToUpper And
                        XNull(DtParty_Type.Rows(I)("ACC_GROUP_NATURE")).ToString.Trim.ToUpper <> ClsMain.AccountGroupNature.Supplier.ToString.Trim.ToUpper And
                        XNull(DtParty_Type.Rows(I)("ACC_GROUP_NATURE")).ToString.Trim.ToUpper <> ClsMain.AccountGroupNature.Expenses.ToString.Trim.ToUpper And
                        XNull(DtParty_Type.Rows(I)("ACC_GROUP_NATURE")).ToString.Trim.ToUpper <> ClsMain.AccountGroupNature.Income.ToString.Trim.ToUpper And
                        XNull(DtParty_Type.Rows(I)("ACC_GROUP_NATURE")).ToString.Trim.ToUpper <> ClsMain.AccountGroupNature.Tax.ToString.Trim.ToUpper And
                        XNull(DtParty_Type.Rows(I)("ACC_GROUP_NATURE")).ToString.Trim.ToUpper <> ClsMain.AccountGroupNature.Others.ToString.Trim.ToUpper Then
                    ErrorLog += "ACC_GROUP_NATURE named " & XNull(DtParty_Type.Rows(I)("ACC_GROUP_NATURE")).ToString.Trim + " found in " & mFileName & " File. It is not present in master."
                    ErrorLog += "ACC_GROUP_NATURE should be in " + ClsMain.AccountGroupNature.Sales + "," +
                            ClsMain.AccountGroupNature.Purchase + "," +
                            ClsMain.AccountGroupNature.Bank + "," +
                            ClsMain.AccountGroupNature.Cash + "," +
                            ClsMain.AccountGroupNature.Customer + "," +
                            ClsMain.AccountGroupNature.Supplier + "," +
                            ClsMain.AccountGroupNature.Expenses + "," +
                            ClsMain.AccountGroupNature.Income + "," +
                            ClsMain.AccountGroupNature.Tax + "," +
                            ClsMain.AccountGroupNature.Others & vbCrLf
                End If
            Next
        End If


        If DtTemp.Columns.Contains("Party_Type") And DtTemp.Columns.Contains("TAX_GROUP") Then
            Dim DtSalesTaxGroup As DataTable = DtTemp.DefaultView.ToTable(True, "Party_Type", "TAX_GROUP")
            For I = 0 To DtSalesTaxGroup.Rows.Count - 1
                If XNull(DtSalesTaxGroup.Rows(I)("Party_Type")).ToString.Trim.ToUpper <> ClsMain.SubGroupType.LedgerAccount.ToString.Trim.ToUpper Then
                    If XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper = "" Then
                        ErrorLog += "Some Parties have blank TAX_GROUP in " & mFileName & " File." & vbCrLf
                    ElseIf XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper <> ClsMain.PostingGroupSalesTaxParty.Composition.ToString.Trim.ToUpper And
                        XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper <> ClsMain.PostingGroupSalesTaxParty.Registered.ToString.Trim.ToUpper And
                        XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper <> ClsMain.PostingGroupSalesTaxParty.Unregistered.ToString.Trim.ToUpper Then
                        ErrorLog += "TAX_GROUP named " & XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim + " found in " & mFileName & " File. It is not present in master."
                        ErrorLog += "TAX_GROUP should be in " + ClsMain.PostingGroupSalesTaxParty.Composition + "," +
                                ClsMain.PostingGroupSalesTaxParty.Registered + "," +
                                ClsMain.PostingGroupSalesTaxParty.Unregistered & vbCrLf
                    End If
                End If
            Next
        End If


        If DtTemp.Columns.Contains("Party_Type") And DtTemp.Columns.Contains("STATE") Then
            Dim DtState As DataTable = DtTemp.DefaultView.ToTable(True, "Party_Type", "STATE")
            For I = 0 To DtState.Rows.Count - 1
                If XNull(DtState.Rows(I)("Party_Type")).ToString.Trim.ToUpper <> ClsMain.SubGroupType.LedgerAccount.ToString.Trim.ToUpper Then
                    If XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper = "" Then
                        ErrorLog += "Some Parties have blank State in " & mFileName & " File." & vbCrLf
                    ElseIf XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.JAMMUANDKASHMIR.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.HIMACHALPRADESH.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.PUNJAB.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.CHANDIGARH.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.UTTARAKHAND.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.HARYANA.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.DELHI.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.RAJASTHAN.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.UTTARPRADESH.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.BIHAR.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.SIKKIM.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.ARUNACHALPRADESH.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.NAGALAND.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.MANIPUR.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.MIZORAM.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.TRIPURA.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.MEGHLAYA.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.ASSAM.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.WESTBENGAL.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.JHARKHAND.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.ODISHA.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.CHATTISGARH.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.MADHYAPRADESH.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.GUJARAT.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.DAMANANDDIU.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.DADRAANDNAGARHAVELI.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.MAHARASHTRA.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.ANDHRAPRADESHBEFOREDIVISION.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.KARNATAKA.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.GOA.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.LAKSHWADEEP.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.KERALA.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.TAMILNADU.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.PUDUCHERRY.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.ANDAMANANDNICOBARISLANDS.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.TELANGANA.ToString.Trim.ToUpper And
                        XNull(DtState.Rows(I)("STATE")).ToString.Trim.ToUpper <> ClsMain.State.ANDHRAPRADESHNew.ToString.Trim.ToUpper Then
                        ErrorLog += "STATE named " & XNull(DtState.Rows(I)("STATE")).ToString.Trim + " found in " & mFileName & " File. It is not present in master."
                        ErrorLog += "STATE should be in " + ClsMain.State.JAMMUANDKASHMIR + "," +
                        ClsMain.State.HIMACHALPRADESH + "," +
                        ClsMain.State.PUNJAB + "," +
                        ClsMain.State.CHANDIGARH + "," +
                        ClsMain.State.UTTARAKHAND + "," +
                        ClsMain.State.HARYANA + "," +
                        ClsMain.State.DELHI + "," +
                        ClsMain.State.RAJASTHAN + "," +
                        ClsMain.State.UTTARPRADESH + "," +
                        ClsMain.State.BIHAR + "," +
                        ClsMain.State.SIKKIM + "," +
                        ClsMain.State.ARUNACHALPRADESH + "," +
                        ClsMain.State.NAGALAND + "," +
                        ClsMain.State.MANIPUR + "," +
                        ClsMain.State.MIZORAM + "," +
                        ClsMain.State.TRIPURA + "," +
                        ClsMain.State.MEGHLAYA + "," +
                        ClsMain.State.ASSAM + "," +
                        ClsMain.State.WESTBENGAL + "," +
                        ClsMain.State.JHARKHAND + "," +
                        ClsMain.State.ODISHA + "," +
                        ClsMain.State.CHATTISGARH + "," +
                        ClsMain.State.MADHYAPRADESH + "," +
                        ClsMain.State.GUJARAT + "," +
                        ClsMain.State.DAMANANDDIU + "," +
                        ClsMain.State.DADRAANDNAGARHAVELI + "," +
                        ClsMain.State.MAHARASHTRA + "," +
                        ClsMain.State.ANDHRAPRADESHBEFOREDIVISION + "," +
                        ClsMain.State.KARNATAKA + "," +
                        ClsMain.State.GOA + "," +
                        ClsMain.State.LAKSHWADEEP + "," +
                        ClsMain.State.KERALA + "," +
                        ClsMain.State.TAMILNADU + "," +
                        ClsMain.State.PUDUCHERRY + "," +
                        ClsMain.State.ANDAMANANDNICOBARISLANDS + "," +
                        ClsMain.State.TELANGANA + "," +
                        ClsMain.State.ANDHRAPRADESHNew & vbCrLf
                    End If
                End If
            Next
        End If

        If DtTemp.Columns.Contains("Party_Type") And DtTemp.Columns.Contains("City") Then


            Dim DtCity As DataTable = DtTemp.DefaultView.ToTable(True, "Party_Type", "City")
            For I = 0 To DtCity.Rows.Count - 1
                If XNull(DtCity.Rows(I)("Party_Type")).ToString.Trim.ToUpper <> ClsMain.SubGroupType.LedgerAccount.ToString.Trim.ToUpper Then
                    If XNull(DtCity.Rows(I)("City")).ToString.Trim.ToUpper = "" Then
                        ErrorLog += "Some Parties have blank City in " & mFileName & " File." & vbCrLf
                    End If
                End If
            Next
        End If

        If DtTemp.Columns.Contains("Code") Then
            Dim DtRowBlank_Code As DataRow() = DtTemp.Select("Code = '' Or Code Is null ")
            If DtRowBlank_Code.Length > 0 Then
                For I = 0 To DtRowBlank_Code.Length - 1
                    If XNull(DtRowBlank_Code(I)("Name")).ToString.Trim <> "" Then
                        ErrorLog += "Code is blank For Code " + DtRowBlank_Code(I)("Name") & " in " & mFileName & " File." & vbCrLf
                    Else
                        ErrorLog += "Code is blank For some Parties in " & mFileName & " File." & vbCrLf
                    End If
                Next
            End If
        End If

        If DtTemp.Columns.Contains("Name") Then
            Dim DtRowBlank_Name As DataRow() = DtTemp.Select("Name = '' Or Name Is null ")
            If DtRowBlank_Name.Length > 0 Then
                For I = 0 To DtRowBlank_Name.Length - 1
                    If XNull(DtRowBlank_Name(I)("Code")).ToString.Trim <> "" Then
                        ErrorLog += "Name is blank For Code " + DtRowBlank_Name(I)("Code") & " in " & mFileName & " File." & vbCrLf
                    Else
                        ErrorLog += "Name is blank For some Parties in " & mFileName & " File." & vbCrLf
                    End If
                Next
            End If
        End If

        If DtTemp.Columns.Contains("Display") Then
            Dim DtRowBlank_Display As DataRow() = DtTemp.Select("Display = '' Or Display Is null ")
            If DtRowBlank_Display.Length > 0 Then
                For I = 0 To DtRowBlank_Display.Length - 1
                    If XNull(DtRowBlank_Display(I)("Code")).ToString.Trim <> "" Then
                        ErrorLog += "Display is blank For Code " + DtRowBlank_Display(I)("Code") & " in " & mFileName & " File." & vbCrLf
                    Else
                        ErrorLog += "Display is blank For some Parties in " & mFileName & " File." & vbCrLf
                    End If
                Next
            End If
        End If

        If DtTemp.Columns.Contains("Acc_Group") Then
            Dim DtRowBlank_Acc_Group As DataRow() = DtTemp.Select("Acc_Group = '' Or Acc_Group Is null ")
            If DtRowBlank_Acc_Group.Length > 0 Then
                For I = 0 To DtRowBlank_Acc_Group.Length - 1
                    If XNull(DtRowBlank_Acc_Group(I)("Code")).ToString.Trim <> "" Then
                        ErrorLog += "Acc_Group is blank For Code " + DtRowBlank_Acc_Group(I)("Code") & " in " & mFileName & " File." & vbCrLf
                    Else
                        ErrorLog += "Acc_Group is blank For some Parties in " & mFileName & " File." & vbCrLf
                    End If
                Next
            End If
        End If

        If ErrorLog <> "" Then ErrorLog += vbCrLf
    End Sub
    Private Sub FGetItemFileValidation()
        Dim mFileName As String = "Item"
        Dim DtTemp As DataTable

        DtTemp = FGetData(mFileName)

        If DtTemp Is Nothing Then Exit Sub

        If Not DtTemp.Columns.Contains("ITEM_CODE") Then ErrorLog += "ITEM_CODE Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("ITEM_NAME") Then ErrorLog += "ITEM_NAME Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("DISP_NAME") Then ErrorLog += "DISP_NAME Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("ITEM_GROUP") Then ErrorLog += "ITEM_GROUP Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("ITEM_CAT") Then ErrorLog += "ITEM_CAT Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("SPECIFIC") Then ErrorLog += "SPECIFIC Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("UNIT") Then ErrorLog += "UNIT Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("PUR_RATE") Then ErrorLog += "PUR_RATE Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("SALE_RATE") Then ErrorLog += "SALE_RATE Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("TAX_GROUP") Then ErrorLog += "TAX_GROUP Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("HSN_CODE") Then ErrorLog += "HSN_CODE Field does not exist in " & mFileName & "  File." + vbCrLf

        If DtTemp.Columns.Contains("TAX_GROUP") Then
            Dim DtSalesTaxGroup As DataTable = DtTemp.DefaultView.ToTable(True, "TAX_GROUP")
            For I = 0 To DtSalesTaxGroup.Rows.Count - 1
                If XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper = "" Then
                    ErrorLog += "Some Items have blank TAX_GROUP in " & mFileName & " File." & vbCrLf
                ElseIf XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper <> ClsMain.PostingGroupSalesTaxItem.GST0.ToString.Trim.ToUpper And
                    XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper <> ClsMain.PostingGroupSalesTaxItem.GST5.ToString.Trim.ToUpper And
                    XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper <> ClsMain.PostingGroupSalesTaxItem.GST12.ToString.Trim.ToUpper And
                    XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper <> ClsMain.PostingGroupSalesTaxItem.GST18.ToString.Trim.ToUpper And
                    XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper <> ClsMain.PostingGroupSalesTaxItem.GST28.ToString.Trim.ToUpper Then
                    ErrorLog += "TAX_GROUP named " & XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim + " found in " & mFileName & " File. It is not present in master."
                    ErrorLog += "TAX_GROUP should be in " + ClsMain.PostingGroupSalesTaxItem.GST0 + "," +
                        ClsMain.PostingGroupSalesTaxItem.GST5 + "," +
                        ClsMain.PostingGroupSalesTaxItem.GST12 + "," +
                        ClsMain.PostingGroupSalesTaxItem.GST18 + "," +
                        ClsMain.PostingGroupSalesTaxItem.GST28 & vbCrLf
                End If
            Next
        End If



        If DtTemp.Columns.Contains("item_code") Then
            Dim DtRowBlank_Item_Code As DataRow() = DtTemp.Select("item_code = '' Or item_code Is null ")
            If DtRowBlank_Item_Code.Length > 0 Then
                For I = 0 To DtRowBlank_Item_Code.Length - 1
                    If XNull(DtRowBlank_Item_Code(I)("Name")).ToString.Trim <> "" Then
                        ErrorLog += "Item_Code is blank For Item " + DtRowBlank_Item_Code(I)("item_name") & " in " & mFileName & " File." & vbCrLf
                    Else
                        ErrorLog += "Item_Code is blank For some Items in " & mFileName & " File." & vbCrLf
                    End If
                Next
            End If
        End If


        If DtTemp.Columns.Contains("Item_Name") Then
            Dim DtRowBlank_Item_Name As DataRow() = DtTemp.Select("Item_Name = '' Or Item_Name Is null ")
            If DtRowBlank_Item_Name.Length > 0 Then
                For I = 0 To DtRowBlank_Item_Name.Length - 1
                    If XNull(DtRowBlank_Item_Name(I)("Code")).ToString.Trim <> "" Then
                        ErrorLog += "Item_Name is blank For Code " + DtRowBlank_Item_Name(I)("Item_Code") & " in " & mFileName & " File." & vbCrLf
                    Else
                        ErrorLog += "Item_Name is blank For some Items in " & mFileName & " File." & vbCrLf
                    End If
                Next
            End If
        End If


        If DtTemp.Columns.Contains("Disp_Name") Then
            Dim DtRowBlank_Disp_Name As DataRow() = DtTemp.Select("Disp_Name = '' Or Disp_Name Is null ")
            If DtRowBlank_Disp_Name.Length > 0 Then
                For I = 0 To DtRowBlank_Disp_Name.Length - 1
                    If XNull(DtRowBlank_Disp_Name(I)("Code")).ToString.Trim <> "" Then
                        ErrorLog += "Disp_Name is blank For Code " + DtRowBlank_Disp_Name(I)("Item_Code") & " in " & mFileName & " File." & vbCrLf
                    Else
                        ErrorLog += "Disp_Name is blank For some Items in " & mFileName & " File." & vbCrLf
                    End If
                Next
            End If
        End If


        If DtTemp.Columns.Contains("Item_Group") Then
            Dim DtRowBlank_Item_Group As DataRow() = DtTemp.Select("Item_Group = '' Or Item_Group Is null ")
            If DtRowBlank_Item_Group.Length > 0 Then
                For I = 0 To DtRowBlank_Item_Group.Length - 1
                    If XNull(DtRowBlank_Item_Group(I)("Code")).ToString.Trim <> "" Then
                        ErrorLog += "Item_Group is blank For Code " + DtRowBlank_Item_Group(I)("Item_Code") & " in " & mFileName & " File." & vbCrLf
                    Else
                        ErrorLog += "Item_Group is blank For some Items in " & mFileName & " File." & vbCrLf
                    End If
                Next
            End If
        End If


        If ErrorLog <> "" Then ErrorLog += vbCrLf
    End Sub

    Private Sub FGetSale1FileValidation()
        Dim mFileName As String = "Sale1"
        Dim DtTemp As DataTable

        DtTemp = FGetData(mFileName)

        If DtTemp Is Nothing Then Exit Sub


        If Not DtTemp.Columns.Contains("V_TYPE") Then ErrorLog += "V_TYPE Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("V_NO") Then ErrorLog += "V_NO Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("V_DATE") Then ErrorLog += "V_DATE Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("INVOICE_NO") Then ErrorLog += "INVOICE_NO Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("SALE_PARTY") Then ErrorLog += "SALE_PARTY Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("PARTY_ADD") Then ErrorLog += "PARTY_ADD Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("PARTY_CITY") Then ErrorLog += "PARTY_CITY Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("PINCODE") Then ErrorLog += "PINCODE Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("GSTIN") Then ErrorLog += "GSTIN Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("BILL_PARTY") Then ErrorLog += "BILL_PARTY Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("AGENT") Then ErrorLog += "AGENT Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("TRANSPORT") Then ErrorLog += "TRANSPORT Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("TR_GSTIN") Then ErrorLog += "TR_GSTIN Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("RATE_TYPE") Then ErrorLog += "RATE_TYPE Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("TAX_GROUP") Then ErrorLog += "TAX_GROUP Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("PLACE_SUPP") Then ErrorLog += "PLACE_SUPP Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("REMARK") Then ErrorLog += "REMARK Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("TERMS") Then ErrorLog += "TERMS Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("CR_LIMIT") Then ErrorLog += "CR_LIMIT Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("CR_DAYS") Then ErrorLog += "CR_DAYS Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("SUBTOTAL1") Then ErrorLog += "SUBTOTAL1 Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("DED_PER") Then ErrorLog += "DED_PER Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("DEDUCTION") Then ErrorLog += "DEDUCTION Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("OT_CH_PER") Then ErrorLog += "OT_CH_PER Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("OT_CHARGE") Then ErrorLog += "OT_CHARGE Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("ROUND_OFF") Then ErrorLog += "ROUND_OFF Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("NET_AMOUNT") Then ErrorLog += "NET_AMOUNT Field does not exist in " & mFileName & " File." + vbCrLf


        If DtTemp.Columns.Contains("TAX_GROUP") Then
            Dim DtSalesTaxGroup As DataTable = DtTemp.DefaultView.ToTable(True, "TAX_GROUP")
            For I = 0 To DtSalesTaxGroup.Rows.Count - 1
                If XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper = "" Then
                    ErrorLog += "Some Parties have blank TAX_GROUP in " & mFileName & " File." & vbCrLf
                ElseIf XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper <> ClsMain.PostingGroupSalesTaxParty.Composition.ToString.Trim.ToUpper And
                        XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper <> ClsMain.PostingGroupSalesTaxParty.Registered.ToString.Trim.ToUpper And
                        XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper <> ClsMain.PostingGroupSalesTaxParty.Unregistered.ToString.Trim.ToUpper Then
                    ErrorLog += "TAX_GROUP named " & XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim + " found in " & mFileName & " File. It is not present in master."
                    ErrorLog += "TAX_GROUP should be in " + ClsMain.PostingGroupSalesTaxParty.Composition + "," +
                                    ClsMain.PostingGroupSalesTaxParty.Registered + "," +
                                    ClsMain.PostingGroupSalesTaxParty.Unregistered & vbCrLf
                End If
            Next
        End If

        If DtTemp.Columns.Contains("V_Type") Then
            Dim DtRowBlank_V_Type As DataRow() = DtTemp.Select("V_Type = '' Or V_Type Is null ")
            If DtRowBlank_V_Type.Length > 0 Then
                For I = 0 To DtRowBlank_V_Type.Length - 1
                    If XNull(DtRowBlank_V_Type(I)("Invoice_No")).ToString.Trim <> "" Then
                        ErrorLog += "V_Type is blank For Invoice_No " + DtRowBlank_V_Type(I)("Invoice_No") & " in " & mFileName & " File." & vbCrLf
                    Else
                        If Not ErrorLog.Contains("V_Type is blank For some invoices in " & mFileName & " File.") Then
                            ErrorLog += "V_Type is blank For some invoices in " & mFileName & " File." & vbCrLf
                        End If
                    End If
                Next
            End If
        End If

        DtSale1V_Type = DtTemp.DefaultView.ToTable(True, "V_Type")

        If ErrorLog <> "" Then ErrorLog += vbCrLf
    End Sub
    Private Sub FGetSale2FileValidation()
        Dim mFileName As String = "Sale2"
        Dim DtTemp As DataTable

        DtTemp = FGetData(mFileName)

        If DtTemp Is Nothing Then Exit Sub

        If Not DtTemp.Columns.Contains("V_TYPE") Then ErrorLog += "V_TYPE Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("INVOICE_NO") Then ErrorLog += "INVOICE_NO Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("TSR") Then ErrorLog += "TSR Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("ITEM_NAME") Then ErrorLog += "ITEM_NAME Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("SPECIFIC") Then ErrorLog += "SPECIFIC Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("TAX_GROUP") Then ErrorLog += "TAX_GROUP Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("QTY") Then ErrorLog += "QTY Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("UNIT") Then ErrorLog += "UNIT Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("PCS") Then ErrorLog += "PCS Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("Rate") Then ErrorLog += "Rate Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("DISC_PER") Then ErrorLog += "DISC_PER Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("DISC_AMT") Then ErrorLog += "DISC_AMT Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("ADISP_PER") Then ErrorLog += "ADISP_PER Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("ADISC_AMT") Then ErrorLog += "ADISC_AMT Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("AMOUNT") Then ErrorLog += "AMOUNT Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("REMARK") Then ErrorLog += "REMARK Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("BALE_NO") Then ErrorLog += "BALE_NO Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("LOT_NO") Then ErrorLog += "LOT_NO Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("GROSS_AMT") Then ErrorLog += "GROSS_AMT Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("TAXABLEAMT") Then ErrorLog += "TAXABLEAMT Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("TAX1_PER") Then ErrorLog += "TAX1_PER Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("TAX1") Then ErrorLog += "TAX1 Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("TAX2_PER") Then ErrorLog += "TAX2_PER Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("TAX2") Then ErrorLog += "TAX2 Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("TAX3_PER") Then ErrorLog += "TAX3_PER Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("TAX3") Then ErrorLog += "TAX3 Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("TAX4_PER") Then ErrorLog += "TAX4_PER Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("TAX4") Then ErrorLog += "TAX4 Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("TAX5_PER") Then ErrorLog += "TAX5_PER Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("TAX5") Then ErrorLog += "TAX5 Field does not exist in " & mFileName & " File." + vbCrLf


        If DtTemp.Columns.Contains("TAX_GROUP") Then
            Dim DtSalesTaxGroup As DataTable = DtTemp.DefaultView.ToTable(True, "TAX_GROUP")
            For I = 0 To DtSalesTaxGroup.Rows.Count - 1
                If XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper = "" Then
                    ErrorLog += "Some Invoices have blank TAX_GROUP in " & mFileName & " File." & vbCrLf
                ElseIf XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper <> ClsMain.PostingGroupSalesTaxItem.GST0.ToString.Trim.ToUpper And
                    XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper <> ClsMain.PostingGroupSalesTaxItem.GST5.ToString.Trim.ToUpper And
                    XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper <> ClsMain.PostingGroupSalesTaxItem.GST12.ToString.Trim.ToUpper And
                    XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper <> ClsMain.PostingGroupSalesTaxItem.GST18.ToString.Trim.ToUpper And
                    XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper <> ClsMain.PostingGroupSalesTaxItem.GST28.ToString.Trim.ToUpper Then
                    ErrorLog += "TAX_GROUP named " & XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim + " found in " & mFileName & " File. It is not present in master."
                    ErrorLog += "TAX_GROUP should be in " + ClsMain.PostingGroupSalesTaxItem.GST0 + "," +
                        ClsMain.PostingGroupSalesTaxItem.GST5 + "," +
                        ClsMain.PostingGroupSalesTaxItem.GST12 + "," +
                        ClsMain.PostingGroupSalesTaxItem.GST18 + "," +
                        ClsMain.PostingGroupSalesTaxItem.GST28 & vbCrLf
                End If
            Next
        End If

        DtSale2V_Type = DtTemp.DefaultView.ToTable(True, "V_Type")

        If ErrorLog <> "" Then ErrorLog += vbCrLf
    End Sub
    Private Sub FGetSale3FileValidation()
        Dim mFileName As String = "Sale3"
        Dim DtTemp As DataTable

        DtTemp = FGetData(mFileName)

        If DtTemp Is Nothing Then Exit Sub

        If Not DtTemp.Columns.Contains("V_TYPE") Then ErrorLog += "V_TYPE Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("INVOICE_NO") Then ErrorLog += "INVOICE_NO Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("TSr") Then ErrorLog += "TSR Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("Sr") Then ErrorLog += "Sr Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("Pcs") Then ErrorLog += "Pcs Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("Qty") Then ErrorLog += "Qty Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("TotalQty") Then ErrorLog += "TotalQty Field does not exist in " & mFileName & " File." + vbCrLf

        DtSale3V_Type = DtTemp.DefaultView.ToTable(True, "V_Type")

        If ErrorLog <> "" Then ErrorLog += vbCrLf
    End Sub
    Private Sub FGetPurch1FileValidation()
        Dim mFileName As String = "Purch1"
        Dim DtTemp As DataTable

        DtTemp = FGetData(mFileName)

        If DtTemp Is Nothing Then Exit Sub

        If Not DtTemp.Columns.Contains("V_TYPE") Then ErrorLog += "V_TYPE Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("v_no") Then ErrorLog += "v_no Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("v_date") Then ErrorLog += "v_date Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("invoice_no") Then ErrorLog += "invoice_no Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("vendor") Then ErrorLog += "vendor Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("vendor_add") Then ErrorLog += "vendor_add Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("vendorcity") Then ErrorLog += "vendorcity Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("pincode") Then ErrorLog += "pincode Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("mobile") Then ErrorLog += "mobile Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("gstin") Then ErrorLog += "gstin Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("doc_no") Then ErrorLog += "doc_no Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("doc_date") Then ErrorLog += "doc_date Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("bill_party") Then ErrorLog += "bill_party Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("agent") Then ErrorLog += "agent Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("tax_group") Then ErrorLog += "tax_group Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("place_supp") Then ErrorLog += "place_supp Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("remark") Then ErrorLog += "remark Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("subtotal1") Then ErrorLog += "subtotal1 Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("ded_per") Then ErrorLog += "ded_per Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("deduction") Then ErrorLog += "deduction Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("ot_ch_per") Then ErrorLog += "ot_ch_per Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("ot_charge") Then ErrorLog += "ot_charge Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("round_off") Then ErrorLog += "round_off Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("net_amount") Then ErrorLog += "net_amount Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("fv_no") Then ErrorLog += "fv_no Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("fv_date") Then ErrorLog += "fv_date Field does not exist in " & mFileName & " File." + vbCrLf

        If DtTemp.Columns.Contains("tax_group") Then
            Dim DtSalesTaxGroup As DataTable = DtTemp.DefaultView.ToTable(True, "TAX_GROUP")
            For I = 0 To DtSalesTaxGroup.Rows.Count - 1
                If XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper = "" Then
                    ErrorLog += "Some Parties have blank TAX_GROUP in " & mFileName & " File." & vbCrLf
                ElseIf XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper <> ClsMain.PostingGroupSalesTaxParty.Composition.ToString.Trim.ToUpper And
                    XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper <> ClsMain.PostingGroupSalesTaxParty.Registered.ToString.Trim.ToUpper And
                    XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper <> ClsMain.PostingGroupSalesTaxParty.Unregistered.ToString.Trim.ToUpper Then
                    ErrorLog += "TAX_GROUP named " & XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim + " found in " & mFileName & " File. It is not present in master."
                    ErrorLog += "TAX_GROUP should be in " + ClsMain.PostingGroupSalesTaxParty.Composition + "," +
                                ClsMain.PostingGroupSalesTaxParty.Registered + "," +
                                ClsMain.PostingGroupSalesTaxParty.Unregistered & vbCrLf
                End If
            Next
        End If

        DtPurch1V_Type = DtTemp.DefaultView.ToTable(True, "V_Type")

        If ErrorLog <> "" Then ErrorLog += vbCrLf
    End Sub
    Private Sub FGetPurch2FileValidation()
        Dim mFileName As String = "Purch2"
        Dim DtTemp As DataTable

        DtTemp = FGetData(mFileName)

        If DtTemp Is Nothing Then Exit Sub

        If Not DtTemp.Columns.Contains("V_TYPE") Then ErrorLog += "V_TYPE Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("INVOICE_NO") Then ErrorLog += "INVOICE_NO Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("TSR") Then ErrorLog += "TSR Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("item_name") Then ErrorLog += "item_name Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("specific") Then ErrorLog += "specific Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("bale_no") Then ErrorLog += "bale_no Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("tax_group") Then ErrorLog += "tax_group Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("qty") Then ErrorLog += "qty Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("unit") Then ErrorLog += "unit Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("Rate") Then ErrorLog += "Rate Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("disc_per") Then ErrorLog += "disc_per Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("disc_amt") Then ErrorLog += "disc_amt Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("adisc_per") Then ErrorLog += "adisc_per Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("adisc_amt") Then ErrorLog += "adisc_amt Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("amount") Then ErrorLog += "amount Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("remark") Then ErrorLog += "remark Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("lr_no") Then ErrorLog += "lr_no Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("lr_date") Then ErrorLog += "lr_date Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("lot_no") Then ErrorLog += "lot_no Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("gross_amt") Then ErrorLog += "gross_amt Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("taxableamt") Then ErrorLog += "taxableamt Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("tax1_per") Then ErrorLog += "tax1_per Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("tax1") Then ErrorLog += "tax1 Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("tax2_per") Then ErrorLog += "tax2_per Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("tax2") Then ErrorLog += "tax2 Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("tax3_per") Then ErrorLog += "tax3_per Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("tax3") Then ErrorLog += "tax3 Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("tax4_per") Then ErrorLog += "tax4_per Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("tax4") Then ErrorLog += "tax4 Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("tax5_per") Then ErrorLog += "tax5_per Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("tax5") Then ErrorLog += "tax5 Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("subtotal1") Then ErrorLog += "subtotal1 Field does not exist in " & mFileName & " File." + vbCrLf

        If DtTemp.Columns.Contains("tax_group") Then
            Dim DtSalesTaxGroup As DataTable = DtTemp.DefaultView.ToTable(True, "TAX_GROUP")
            For I = 0 To DtSalesTaxGroup.Rows.Count - 1
                If XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper = "" Then
                    ErrorLog += "Some Invoices have blank TAX_GROUP in " & mFileName & " File." & vbCrLf
                ElseIf XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper <> ClsMain.PostingGroupSalesTaxItem.GST0.ToString.Trim.ToUpper And
                    XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper <> ClsMain.PostingGroupSalesTaxItem.GST5.ToString.Trim.ToUpper And
                    XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper <> ClsMain.PostingGroupSalesTaxItem.GST12.ToString.Trim.ToUpper And
                    XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper <> ClsMain.PostingGroupSalesTaxItem.GST18.ToString.Trim.ToUpper And
                    XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim.ToUpper <> ClsMain.PostingGroupSalesTaxItem.GST28.ToString.Trim.ToUpper Then
                    ErrorLog += "TAX_GROUP named " & XNull(DtSalesTaxGroup.Rows(I)("TAX_GROUP")).ToString.Trim + " found in " & mFileName & " File. It is not present in master."
                    ErrorLog += "TAX_GROUP should be in " + ClsMain.PostingGroupSalesTaxItem.GST0 + "," +
                        ClsMain.PostingGroupSalesTaxItem.GST5 + "," +
                        ClsMain.PostingGroupSalesTaxItem.GST12 + "," +
                        ClsMain.PostingGroupSalesTaxItem.GST18 + "," +
                        ClsMain.PostingGroupSalesTaxItem.GST28 & vbCrLf
                End If
            Next
        End If

        DtPurch2V_Type = DtTemp.DefaultView.ToTable(True, "V_Type")

        If ErrorLog <> "" Then ErrorLog += vbCrLf
    End Sub
    Private Sub FGetPurch3FileValidation()
        Dim mFileName As String = "Purch3"
        Dim DtTemp As DataTable

        DtTemp = FGetData(mFileName)

        If DtTemp Is Nothing Then Exit Sub

        If Not DtTemp.Columns.Contains("V_TYPE") Then ErrorLog += "V_TYPE Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("INVOICE_NO") Then ErrorLog += "INVOICE_NO Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("TSr") Then ErrorLog += "TSR Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("Sr") Then ErrorLog += "Sr Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("Pcs") Then ErrorLog += "Pcs Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("Qty") Then ErrorLog += "Qty Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("TotalQty") Then ErrorLog += "TotalQty Field does not exist in " & mFileName & " File." + vbCrLf

        DtPurch3V_Type = DtTemp.DefaultView.ToTable(True, "V_Type")

        If ErrorLog <> "" Then ErrorLog += vbCrLf
    End Sub
    Private Sub FGetLedgerFileValidation()
        Dim mFileName As String = "Ledger"
        Dim DtTemp As DataTable

        DtTemp = FGetData(mFileName)

        If DtTemp Is Nothing Then Exit Sub

        If Not DtTemp.Columns.Contains("V_TYPE") Then ErrorLog += "V_TYPE Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("V_NO") Then ErrorLog += "V_NO Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("V_DATE") Then ErrorLog += "V_DATE Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("ledgername") Then ErrorLog += "ledgername Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("contraname") Then ErrorLog += "contraname Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("narration") Then ErrorLog += "narration Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("chq_no") Then ErrorLog += "chq_no Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("chq_date") Then ErrorLog += "chq_date Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("amt_dr") Then ErrorLog += "amt_dr Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("amt_cr") Then ErrorLog += "amt_cr Field does not exist in " & mFileName & " File." + vbCrLf

        DtLedgerV_Type = DtTemp.DefaultView.ToTable(True, "V_Type")

        If ErrorLog <> "" Then ErrorLog += vbCrLf
    End Sub
    Private Sub FGetPaymentFileValidation()
        Dim mFileName As String = "Payment"
        Dim DtTemp As DataTable

        DtTemp = FGetData(mFileName)

        If DtTemp Is Nothing Then Exit Sub

        If Not DtTemp.Columns.Contains("V_TYPE") Then ErrorLog += "V_TYPE Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("V_NO") Then ErrorLog += "V_NO Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("V_DATE") Then ErrorLog += "V_DATE Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("vendor") Then ErrorLog += "vendor Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("dr") Then ErrorLog += "dr Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("cr") Then ErrorLog += "cr Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("final") Then ErrorLog += "final Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("remark") Then ErrorLog += "remark Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("fv_no") Then ErrorLog += "fv_no Field does not exist in " & mFileName & " File." + vbCrLf

        DtPaymentV_Type = DtTemp.DefaultView.ToTable(True, "V_Type")

        If ErrorLog <> "" Then ErrorLog += vbCrLf
    End Sub
    Private Sub FGetDraftFileValidation()
        Dim mFileName As String = "Draft"
        Dim DtTemp As DataTable

        DtTemp = FGetData(mFileName)

        If DtTemp Is Nothing Then Exit Sub

        If Not DtTemp.Columns.Contains("V_TYPE") Then ErrorLog += "V_TYPE Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("V_NO") Then ErrorLog += "V_NO Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("V_DATE") Then ErrorLog += "V_DATE Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("bank_name") Then ErrorLog += "bank_name Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("chq_no") Then ErrorLog += "chq_no Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("chq_date") Then ErrorLog += "chq_date Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("amount") Then ErrorLog += "amount Field does not exist in " & mFileName & " File." + vbCrLf

        DtDraftV_Type = DtTemp.DefaultView.ToTable(True, "V_Type")

        If ErrorLog <> "" Then ErrorLog += vbCrLf
    End Sub
    Private Sub FGetGPurch1FileValidation()
        Dim mFileName As String = "GPURCH1"
        Dim DtTemp As DataTable

        DtTemp = FGetData(mFileName)

        If DtTemp Is Nothing Then Exit Sub

        If Not DtTemp.Columns.Contains("V_TYPE") Then ErrorLog += "V_TYPE Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("v_no") Then ErrorLog += "v_no Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("v_date") Then ErrorLog += "v_date Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("invoice_no") Then ErrorLog += "invoice_no Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("vendor") Then ErrorLog += "vendor Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("vendor_add") Then ErrorLog += "vendor_add Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("vendorcity") Then ErrorLog += "vendorcity Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("pincode") Then ErrorLog += "pincode Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("mobile") Then ErrorLog += "mobile Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("gstin") Then ErrorLog += "gstin Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("doc_no") Then ErrorLog += "doc_no Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("doc_date") Then ErrorLog += "doc_date Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("bill_party") Then ErrorLog += "bill_party Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("agent") Then ErrorLog += "agent Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("tax_group") Then ErrorLog += "tax_group Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("place_supp") Then ErrorLog += "place_supp Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("remark") Then ErrorLog += "remark Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("subtotal1") Then ErrorLog += "subtotal1 Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("ded_per") Then ErrorLog += "ded_per Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("deduction") Then ErrorLog += "deduction Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("ot_ch_per") Then ErrorLog += "ot_ch_per Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("ot_charge") Then ErrorLog += "ot_charge Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("round_off") Then ErrorLog += "round_off Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("net_amount") Then ErrorLog += "net_amount Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("fv_no") Then ErrorLog += "fv_no Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("fv_date") Then ErrorLog += "fv_date Field does not exist in " & mFileName & " File." + vbCrLf

        DtGPurch1V_Type = DtTemp.DefaultView.ToTable(True, "V_Type")

        If ErrorLog <> "" Then ErrorLog += vbCrLf
    End Sub
    Private Sub FGetGPurch2FileValidation()
        Dim mFileName As String = "GPURCH2"
        Dim DtTemp As DataTable

        DtTemp = FGetData(mFileName)

        If DtTemp Is Nothing Then Exit Sub

        If Not DtTemp.Columns.Contains("V_TYPE") Then ErrorLog += "V_TYPE Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("v_no") Then ErrorLog += "v_no Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("INVOICE_NO") Then ErrorLog += "INVOICE_NO Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("item_name") Then ErrorLog += "item_name Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("specific") Then ErrorLog += "specific Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("tax_group") Then ErrorLog += "tax_group Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("qty") Then ErrorLog += "qty Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("unit") Then ErrorLog += "unit Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("Rate") Then ErrorLog += "Rate Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("disc_per") Then ErrorLog += "disc_per Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("disc_amt") Then ErrorLog += "disc_amt Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("adisc_per") Then ErrorLog += "adisc_per Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("adisc_amt") Then ErrorLog += "adisc_amt Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("amount") Then ErrorLog += "amount Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("remark") Then ErrorLog += "remark Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("lr_no") Then ErrorLog += "lr_no Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("lr_date") Then ErrorLog += "lr_date Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("lot_no") Then ErrorLog += "lot_no Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("gross_amt") Then ErrorLog += "gross_amt Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("taxableamt") Then ErrorLog += "taxableamt Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("tax1_per") Then ErrorLog += "tax1_per Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("tax1") Then ErrorLog += "tax1 Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("tax2_per") Then ErrorLog += "tax2_per Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("tax2") Then ErrorLog += "tax2 Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("tax3_per") Then ErrorLog += "tax3_per Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("tax3") Then ErrorLog += "tax3 Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("tax4_per") Then ErrorLog += "tax4_per Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("tax4") Then ErrorLog += "tax4 Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("tax5_per") Then ErrorLog += "tax5_per Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("tax5") Then ErrorLog += "tax5 Field does not exist in " & mFileName & " File." + vbCrLf
        If Not DtTemp.Columns.Contains("subtotal1") Then ErrorLog += "subtotal1 Field does not exist in " & mFileName & " File." + vbCrLf

        DtGPurch2V_Type = DtTemp.DefaultView.ToTable(True, "V_Type")

        If ErrorLog <> "" Then ErrorLog += vbCrLf
    End Sub

    Private Sub FGetV_TypeFileValidation()
        Dim mFileName As String = "V_Type"
        Dim DtTemp As DataTable


        DtTemp = FGetData(mFileName)

        If DtTemp Is Nothing Then Exit Sub

        If Not DtTemp.Columns.Contains("V_Type") Then ErrorLog += "V_Type Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("Desc") Then ErrorLog += "Desc Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("Nature") Then ErrorLog += "Nature Field does not exist in " & mFileName & "  File." + vbCrLf

        For I As Integer = 0 To DtTemp.Rows.Count - 1
            If DtTemp.Columns.Contains("Desc") Then
                If XNull(DtTemp.Rows(I)("Desc")) = "" Then
                    ErrorLog += "Desc Field is blank at row no " & (I + 1).ToString & " in " & mFileName & "  File." + vbCrLf
                End If
            End If

            If DtTemp.Columns.Contains("Nature") Then
                If XNull(DtTemp.Rows(I)("Nature")) = "" Then
                    ErrorLog += "Nature Field is blank at row no " & (I + 1).ToString & " in " & mFileName & "  File." + vbCrLf
                ElseIf XNull(DtTemp.Rows(I)("Nature")) <> ClsMain.VoucherTypeNature.Sale And
                        (DtTemp.Rows(I)("Nature")) <> ClsMain.VoucherTypeNature.Purchase And
                        (DtTemp.Rows(I)("Nature")) <> ClsMain.VoucherTypeNature.DebitNote And
                        (DtTemp.Rows(I)("Nature")) <> ClsMain.VoucherTypeNature.CreditNote And
                        (DtTemp.Rows(I)("Nature")) <> ClsMain.VoucherTypeNature.Payment And
                        (DtTemp.Rows(I)("Nature")) <> ClsMain.VoucherTypeNature.Receipt And
                        (DtTemp.Rows(I)("Nature")) <> ClsMain.VoucherTypeNature.Journal Then
                    ErrorLog += "Nature named " & XNull(DtTemp.Rows(I)("Nature")).ToString.Trim + " found in " & mFileName & " File. It is not present in master."
                    ErrorLog += "Nature should be in " + ClsMain.VoucherTypeNature.Sale + "," +
                            ClsMain.VoucherTypeNature.Purchase + "," +
                            ClsMain.VoucherTypeNature.DebitNote + "," +
                            ClsMain.VoucherTypeNature.CreditNote + "," +
                            ClsMain.VoucherTypeNature.Payment + "," +
                            ClsMain.VoucherTypeNature.Receipt + "," +
                            ClsMain.VoucherTypeNature.Journal & vbCrLf
                End If
            End If
        Next

        FCheckVoucherTypeValidity(DtSale1V_Type, DtTemp, "Sale1")
        FCheckVoucherTypeValidity(DtSale2V_Type, DtTemp, "Sale2")
        FCheckVoucherTypeValidity(DtSale3V_Type, DtTemp, "Sale3")
        FCheckVoucherTypeValidity(DtPurch1V_Type, DtTemp, "Purch1")
        FCheckVoucherTypeValidity(DtPurch2V_Type, DtTemp, "Purch2")
        FCheckVoucherTypeValidity(DtPurch3V_Type, DtTemp, "Purch3")
        FCheckVoucherTypeValidity(DtLedgerV_Type, DtTemp, "Ledger")
        FCheckVoucherTypeValidity(DtPaymentV_Type, DtTemp, "Payment")
        FCheckVoucherTypeValidity(DtDraftV_Type, DtTemp, "Draft")
        FCheckVoucherTypeValidity(DtGPurch1V_Type, DtTemp, "GPurch1")
        FCheckVoucherTypeValidity(DtGPurch2V_Type, DtTemp, "GPurch2")

        If ErrorLog <> "" Then ErrorLog += vbCrLf
    End Sub

    Private Sub FGetTrialBalanceFileValidation()
        Dim mFileName As String = "TrialBalance"
        Dim DtTemp As DataTable

        DtTemp = FGetData(mFileName)

        If DtTemp Is Nothing Then Exit Sub

        If Not DtTemp.Columns.Contains("Name") Then ErrorLog += "Name Field does not exist in " & mFileName & "  File." + vbCrLf
        If Not DtTemp.Columns.Contains("Balance") Then ErrorLog += "Balance Field does not exist in " & mFileName & "  File." + vbCrLf

        If ErrorLog <> "" Then ErrorLog += vbCrLf
    End Sub
    Private Sub FCheckVoucherTypeValidity(DtTransactionV_Type As DataTable, DtMasterV_Type As DataTable, mFileName As String)
        Dim I As Integer = 0
        Dim J As Integer = 0
        Dim bFoundInMaster As Boolean = False

        For I = 0 To DtTransactionV_Type.Rows.Count - 1
            If XNull(DtTransactionV_Type.Rows(I)("V_Type")).ToString.Trim <> "" Then
                bFoundInMaster = False
                For J = 0 To DtMasterV_Type.Rows.Count - 1
                    If XNull(DtTransactionV_Type.Rows(I)("V_Type")).ToString.Trim.ToUpper =
                            XNull(DtMasterV_Type.Rows(J)("V_Type")).ToString.Trim.ToUpper Then
                        bFoundInMaster = True
                    End If
                Next
                If bFoundInMaster = False Then
                    ErrorLog += "V_Type " & XNull(DtTransactionV_Type.Rows(I)("V_Type")) & " found in " & mFileName & " File but it does not exist in V_Type File." + vbCrLf
                End If
            End If
        Next
    End Sub
    Private Function FGetData(mFileName As String) As DataTable
        Dim MyConnection As System.Data.OleDb.OleDbConnection
        Dim MyCommand As OleDb.OleDbDataAdapter

        Dim mFileNamewithPath As String = TxtFileLocation.Text + "/" + mFileName + ".xls"

        Dim DsExcelData As New DataSet

        If File.Exists(mFileNamewithPath) = False Then
            ErrorLog += mFileName + " File does Not exist in selected location." + vbCrLf
        Else
            MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; " &
                       "data source='" & mFileNamewithPath & " '; " & "Extended Properties=Excel 8.0;")

            MyConnection.Open()
            Dim DtSheetNames As DataTable = MyConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
            Dim IsShee1Exist As Boolean = False
            For I As Integer = 0 To DtSheetNames.Rows.Count - 1
                If DtSheetNames.Rows(I)("Table_Name").ToString.ToUpper = ("sheet1$").ToString.ToUpper Then
                    IsShee1Exist = True
                    Exit For
                End If
            Next

            If IsShee1Exist = False Then
                ErrorLog += "Sheet1 does not exist in selected file." + mFileName + vbCrLf
                MyConnection.Close()
            Else
                MyCommand = New System.Data.OleDb.OleDbDataAdapter("select *  from [sheet1$] ", MyConnection)
                MyCommand.Fill(DsExcelData)
            End If
        End If

        If DsExcelData.Tables.Count > 0 Then
            Return DsExcelData.Tables(0)
        Else
            Return Nothing
        End If
    End Function

    Public Function XNull(ByVal temp As Object) As Object
        If temp Is Nothing Then temp = ""
        XNull = CStr(IIf(IsDBNull(temp), "", temp))
    End Function

    Public Function VNull(ByRef temp As Object) As Object
        If temp Is Nothing Then temp = 0
        VNull = Val(IIf(IsDBNull(temp), 0, temp))
    End Function

    Private Sub FrmCheckValidation_Load(sender As Object, e As EventArgs) Handles Me.Load
        TxtFileLocation.Text = My.Settings.FilePath
    End Sub
End Class