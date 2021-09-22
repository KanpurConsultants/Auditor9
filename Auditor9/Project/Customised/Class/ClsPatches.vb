Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class ClsPatches
    Public Shared Sub UpdateTableStructure_Client()
        UpdateTableStructure_ShyamaShyam()
        UpdateTableStructure_ShyamaShyam_W()
    End Sub
    Public Shared Sub UpdateTableStructure_ShyamaShyam()
        Dim mQry As String = ""
        Dim ClsObj As New ClsMain(AgL)

        Try
            If AgL.StrCmp(AgL.PubCompName, "Shyama Shyam") Then
                If AgL.FillData("Select * from SubGroupType Where SubgroupType='Master Customer'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                    mQry = " 
                        Insert Into SubGroupType (SubgroupType,IsActive, Parent)
                        Values ('Master Customer',1, '" & SubgroupType.Customer & "'); "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                End If

                If AgL.FillData("Select * from SubGroupType Where SubgroupType='Master Supplier'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                    mQry = " 
                        Insert Into SubGroupType (SubgroupType,IsActive, Parent)
                        Values ('Master Supplier',1, '" & SubgroupType.Supplier & "'); "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                End If

                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.SubgroupType, 1, 1, 1)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Code, 0, 1, 1)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Name, 1, 1, 1)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.PrintingDescription, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Address, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.City, 1, 1, 1)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Pincode, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.ContactNo, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Mobile, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Email, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.AcGroup, 1, 1, 1)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.SalesTaxGroup, 1, 1, 1)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.ContactPerson, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.SalesTaxNo, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.PanNo, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.AadharNo, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Parent, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Area, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Agent, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Transporter, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.InterestSlab, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.RateType, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Distance, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Discount, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.CreditDays, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.CreditLimit, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Remarks, 0, 0)

                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.SubgroupType, 1, 1, 1)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Code, 0, 1, 1)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Name, 1, 1, 1)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.PrintingDescription, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Address, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.City, 1, 1, 1)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Pincode, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.ContactNo, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Mobile, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Email, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.AcGroup, 1, 1, 1)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.SalesTaxGroup, 1, 1, 1)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.ContactPerson, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.SalesTaxNo, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.PanNo, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.AadharNo, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Parent, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Area, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Agent, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Transporter, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.InterestSlab, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.RateType, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Distance, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Discount, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.CreditDays, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.CreditLimit, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Remarks, 0, 0)

                If AgL.FillData("Select * from Voucher_Prefix Where Site_Code='2'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                    mQry = " INSERT INTO Voucher_Prefix(V_Type,Date_From, Prefix,Start_Srl_No, Date_To,   Comp_Code, Site_Code, Div_Code,  Ref_Prefix,Ref_PadLength)
                        Select V_Type,Date_From, Prefix,Start_Srl_No,  Date_To,   Comp_Code, '2' As Site_Code, Div_Code,  Ref_Prefix,Ref_PadLength From Voucher_Prefix Where Site_Code = '1';; "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                End If

                If AgL.FillData("Select * from Voucher_Prefix Where Site_Code='3'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                    mQry = " INSERT INTO Voucher_Prefix(V_Type,Date_From, Prefix,Start_Srl_No, Date_To,   Comp_Code, Site_Code, Div_Code,  Ref_Prefix,Ref_PadLength)
                        Select V_Type,Date_From, Prefix,Start_Srl_No,  Date_To,   Comp_Code, '3' As Site_Code, Div_Code,  Ref_Prefix,Ref_PadLength From Voucher_Prefix Where Site_Code = '1';; "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                End If

                If AgL.FillData("Select * from Voucher_Prefix Where Site_Code='4'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                    mQry = " INSERT INTO Voucher_Prefix(V_Type,Date_From, Prefix,Start_Srl_No, Date_To,   Comp_Code, Site_Code, Div_Code,  Ref_Prefix,Ref_PadLength)
                        Select V_Type,Date_From, Prefix,Start_Srl_No,  Date_To,   Comp_Code, '4' As Site_Code, Div_Code,  Ref_Prefix,Ref_PadLength From Voucher_Prefix Where Site_Code = '1';; "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                End If

                If AgL.FillData("Select * from Voucher_Prefix Where Site_Code='5'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                    mQry = " INSERT INTO Voucher_Prefix(V_Type,Date_From, Prefix,Start_Srl_No, Date_To,   Comp_Code, Site_Code, Div_Code,  Ref_Prefix,Ref_PadLength)
                        Select V_Type,Date_From, Prefix,Start_Srl_No,  Date_To,   Comp_Code, '5' As Site_Code, Div_Code,  Ref_Prefix,Ref_PadLength From Voucher_Prefix Where Site_Code = '1';; "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                End If

                If AgL.FillData("SELECT * FROM Subgroup WHERE SubgroupType = 'Customer' 
                            AND SalesTaxPostingGroup = 'UnRegistered' 
                            AND Parent IS NULL", AgL.GcnMain).tables(0).Rows.Count > 0 Then
                    mQry = " UPDATE SubGroup Set SubGroupType = 'Master Customer' WHERE SubgroupType = 'Customer' 
                            AND SalesTaxPostingGroup = 'UnRegistered' 
                            AND Parent IS NULL "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                End If

                If AgL.FillData("SELECT * FROM Subgroup WHERE SubgroupType = 'Supplier' 
                            AND SalesTaxPostingGroup = 'UnRegistered' 
                            AND Parent IS NULL", AgL.GcnMain).tables(0).Rows.Count > 0 Then
                    mQry = " UPDATE SubGroup Set SubGroupType = 'Master Supplier' WHERE SubgroupType = 'Supplier' 
                            AND SalesTaxPostingGroup = 'UnRegistered' 
                            AND Parent IS NULL "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                End If

                mQry = "Select Ig.Code, Ic.Code As ItemCategory
                        From Item Ig 
                        Left Join Department D On Ig.Department = D.Code
                        LEFT JOIN ItemCategory Ic On D.Description = Ic.Description
                        Where Ig.Department Is Not Null 
                        And Ig.ItemCategory Is Null
                        And Ic.Code Is Not Null
                        And Ig.V_Type = 'IG' "
                Dim DtItemGroup As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

                For I As Integer = 0 To DtItemGroup.Rows.Count - 1
                    mQry = "UPDATE Item Set ItemCategory = '" & AgL.XNull(DtItemGroup.Rows(I)("ItemCategory")) & "'
                            Where Code = '" & AgL.XNull(DtItemGroup.Rows(I)("Code")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                Next

                mQry = "UPDATE SaleInvoiceSetting Set CalculateContraBalanceOnValueYN = 1 Where Code = 'SI'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " UPDATE Division Set ScopeOfWork = '+Cloth Aadhat Module+Sales Order Module'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)



                Try
                    If Not AgL.IsTableExist("SaleInvoiceGeneratedEntries", AgL.GcnMain) Then
                        mQry = " CREATE TABLE [SaleInvoiceGeneratedEntries] ( Code nVarchar(10) Not Null COLLATE NOCASE); "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                    End If
                    AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoiceGeneratedEntries", "Type", "nVarchar(20)", "", True)
                    AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoiceGeneratedEntries", "DocId", "nVarchar(21)", "", True)
                    AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoiceGeneratedEntries", "SaleOrderNo", "nVarchar(20)", "", True)
                    AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoiceGeneratedEntries", "Site_Code", "nVarchar(2)", "", True, " References SiteMast(Code) COLLATE NOCASE")
                    AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoiceGeneratedEntries", "Div_Code", "nVarchar(1)", "", True, " References Division(Div_Code) COLLATE NOCASE")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " [UpdateTableStructure_ShyamaShyam] ")
        End Try
    End Sub

    Public Shared Sub UpdateTableStructure_ShyamaShyam_W()
        Dim mQry As String = ""
        Dim ClsObj As New ClsMain(AgL)

        Try
            If AgL.StrCmp(AgL.PubCompName, "Shyama Shyam W") Then

                mQry = "Update Division Set ScopeOfWork = '+Cloth Aadhat Module +Double Entry Module'"
                AgL.Dman_Execute(mQry, AgL.GcnMain)

                If AgL.FillData("Select * from SubGroupType Where SubgroupType='Master Customer'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                    mQry = " 
                        Insert Into SubGroupType (SubgroupType,IsActive, Parent)
                        Values ('Master Customer',1, '" & SubgroupType.Customer & "'); "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                End If

                If AgL.FillData("Select * from SubGroupType Where SubgroupType='Master Supplier'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                    mQry = " 
                        Insert Into SubGroupType (SubgroupType,IsActive, Parent)
                        Values ('Master Supplier',1, '" & SubgroupType.Supplier & "'); "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                End If

                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.SubgroupType, 1, 1, 1)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Code, 0, 1, 1)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Name, 1, 1, 1)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.PrintingDescription, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Address, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.City, 1, 1, 1)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Pincode, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.ContactNo, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Mobile, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Email, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.AcGroup, 1, 1, 1)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.SalesTaxGroup, 1, 1, 1)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.ContactPerson, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.SalesTaxNo, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.PanNo, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.AadharNo, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Parent, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Area, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Agent, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Transporter, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.InterestSlab, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.RateType, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Distance, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Discount, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.CreditDays, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.CreditLimit, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Customer", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Remarks, 0, 0)

                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.SubgroupType, 1, 1, 1)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Code, 0, 1, 1)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Name, 1, 1, 1)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.PrintingDescription, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Address, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.City, 1, 1, 1)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Pincode, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.ContactNo, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Mobile, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Email, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.AcGroup, 1, 1, 1)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.SalesTaxGroup, 1, 1, 1)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.ContactPerson, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.SalesTaxNo, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.PanNo, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.AadharNo, 1, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Parent, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Area, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Agent, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Transporter, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.InterestSlab, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.RateType, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Distance, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Discount, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.CreditDays, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.CreditLimit, 0, 0)
                ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", "Master Supplier", "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Remarks, 0, 0)



                If AgL.FillData("Select Count(*) from SiteMast Where Code = '2'", AgL.GcnMain).tables(0).Rows(0)(0) = 0 Then
                    mQry = " INSERT INTO SiteMast
                    (Code, Name, HO_YN, Add1, Add2, Add3, City_Code, Phone, Mobile, PinNo, U_Name, U_EntDt, U_AE, Edit_Date, ModifiedBy, ManualCode, RowId, UpLoadDate, Active, AcCode, SqlServer, DataPath, DataPathMain, SqlUser, SqlPassword, CreditLimit, ApprovedBy, ApprovedDate, GPX1, GPX2, GPN1, GPN2, LastNarration, IEC, TIN, Director, ExciseDivision, DrugLicenseNo, PAN)
                    VALUES('2', 'AHMEDABAD', 'N', Null, NULL, NULL, 'D10008', Null, NULL, Null, 'SA', '2008-08-06 00:00:00', 'E', '2013-03-30 00:00:00', 'SA', 'AHMEDABAD', 1, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL,  NULL, NULL, NULL, NULL, NULL, NULL, '---', NULL);
                   "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                End If

                If AgL.FillData("Select Count(*) from SiteMast Where Code = '3'", AgL.GcnMain).tables(0).Rows(0)(0) = 0 Then
                    mQry = " INSERT INTO SiteMast
                    (Code, Name, HO_YN, Add1, Add2, Add3, City_Code, Phone, Mobile, PinNo, U_Name, U_EntDt, U_AE, Edit_Date, ModifiedBy, ManualCode, RowId, UpLoadDate, Active, AcCode, SqlServer, DataPath, DataPathMain, SqlUser, SqlPassword, CreditLimit, ApprovedBy, ApprovedDate, GPX1, GPX2, GPN1, GPN2, LastNarration, IEC, TIN, Director, ExciseDivision, DrugLicenseNo, PAN)
                    VALUES('3', 'LUDIYANA', 'N', Null, NULL, NULL, 'D10007', Null, NULL, Null, 'SA', '2008-08-06 00:00:00', 'E', '2013-03-30 00:00:00', 'SA', 'LUDIYANA', 1, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL,  NULL, NULL, NULL, NULL, NULL, NULL, '---', NULL);
                   "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                End If

                If AgL.FillData("Select Count(*) from SiteMast Where Code = '4'", AgL.GcnMain).tables(0).Rows(0)(0) = 0 Then
                    mQry = " INSERT INTO SiteMast
                    (Code, Name, HO_YN, Add1, Add2, Add3, City_Code, Phone, Mobile, PinNo, U_Name, U_EntDt, U_AE, Edit_Date, ModifiedBy, ManualCode, RowId, UpLoadDate, Active, AcCode, SqlServer, DataPath, DataPathMain, SqlUser, SqlPassword, CreditLimit, ApprovedBy, ApprovedDate, GPX1, GPX2, GPN1, GPN2, LastNarration, IEC, TIN, Director, ExciseDivision, DrugLicenseNo, PAN)
                    VALUES('4', 'DELHI', 'N', Null, NULL, NULL, 'D10009', Null, NULL, Null, 'SA', '2008-08-06 00:00:00', 'E', '2013-03-30 00:00:00', 'SA', 'DELHI', 1, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL,  NULL, NULL, NULL, NULL, NULL, NULL, '---', NULL);
                   "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                End If

                If AgL.FillData("Select Count(*) from SiteMast Where Code = '5'", AgL.GcnMain).tables(0).Rows(0)(0) = 0 Then
                    mQry = " INSERT INTO SiteMast
                    (Code, Name, HO_YN, Add1, Add2, Add3, City_Code, Phone, Mobile, PinNo, U_Name, U_EntDt, U_AE, Edit_Date, ModifiedBy, ManualCode, RowId, UpLoadDate, Active, AcCode, SqlServer, DataPath, DataPathMain, SqlUser, SqlPassword, CreditLimit, ApprovedBy, ApprovedDate, GPX1, GPX2, GPN1, GPN2, LastNarration, IEC, TIN, Director, ExciseDivision, DrugLicenseNo, PAN)
                    VALUES('5', 'JABALPUR', 'N', Null, NULL, NULL, 'D10010', Null, NULL, Null, 'SA', '2008-08-06 00:00:00', 'E', '2013-03-30 00:00:00', 'SA', 'JABALPUR', 1, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL,  NULL, NULL, NULL, NULL, NULL, NULL, '---', NULL);
                   "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                End If




                If AgL.FillData("Select * from Voucher_Prefix Where Site_Code='2'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                    mQry = " INSERT INTO Voucher_Prefix(V_Type,Date_From, Prefix,Start_Srl_No, Date_To,   Comp_Code, Site_Code, Div_Code,  Ref_Prefix,Ref_PadLength)
                        Select V_Type,Date_From, Prefix,Start_Srl_No,  Date_To,   Comp_Code, '2' As Site_Code, Div_Code,  Ref_Prefix,Ref_PadLength From Voucher_Prefix Where Site_Code = '1';; "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                End If

                If AgL.FillData("Select * from Voucher_Prefix Where Site_Code='3'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                    mQry = " INSERT INTO Voucher_Prefix(V_Type,Date_From, Prefix,Start_Srl_No, Date_To,   Comp_Code, Site_Code, Div_Code,  Ref_Prefix,Ref_PadLength)
                        Select V_Type,Date_From, Prefix,Start_Srl_No,  Date_To,   Comp_Code, '3' As Site_Code, Div_Code,  Ref_Prefix,Ref_PadLength From Voucher_Prefix Where Site_Code = '1';; "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                End If

                If AgL.FillData("Select * from Voucher_Prefix Where Site_Code='4'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                    mQry = " INSERT INTO Voucher_Prefix(V_Type,Date_From, Prefix,Start_Srl_No, Date_To,   Comp_Code, Site_Code, Div_Code,  Ref_Prefix,Ref_PadLength)
                        Select V_Type,Date_From, Prefix,Start_Srl_No,  Date_To,   Comp_Code, '4' As Site_Code, Div_Code,  Ref_Prefix,Ref_PadLength From Voucher_Prefix Where Site_Code = '1';; "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                End If

                If AgL.FillData("Select * from Voucher_Prefix Where Site_Code='5'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                    mQry = " INSERT INTO Voucher_Prefix(V_Type,Date_From, Prefix,Start_Srl_No, Date_To,   Comp_Code, Site_Code, Div_Code,  Ref_Prefix,Ref_PadLength)
                        Select V_Type,Date_From, Prefix,Start_Srl_No,  Date_To,   Comp_Code, '5' As Site_Code, Div_Code,  Ref_Prefix,Ref_PadLength From Voucher_Prefix Where Site_Code = '1';; "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                End If

                If AgL.FillData("SELECT * FROM Subgroup WHERE SubgroupType = 'Customer' 
                            AND SalesTaxPostingGroup = 'UnRegistered' 
                            AND Parent IS NULL", AgL.GcnMain).tables(0).Rows.Count > 0 Then
                    mQry = " UPDATE SubGroup Set SubGroupType = 'Master Customer' WHERE SubgroupType = 'Customer' 
                            AND SalesTaxPostingGroup = 'UnRegistered' 
                            AND Parent IS NULL "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                End If

                If AgL.FillData("SELECT * FROM Subgroup WHERE SubgroupType = 'Supplier' 
                            AND SalesTaxPostingGroup = 'UnRegistered' 
                            AND Parent IS NULL", AgL.GcnMain).tables(0).Rows.Count > 0 Then
                    mQry = " UPDATE SubGroup Set SubGroupType = 'Master Supplier' WHERE SubgroupType = 'Supplier' 
                            AND SalesTaxPostingGroup = 'UnRegistered' 
                            AND Parent IS NULL "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                End If

                mQry = "Select Ig.Code, Ic.Code As ItemCategory
                        From Item Ig 
                        Left Join Department D On Ig.Department = D.Code
                        LEFT JOIN ItemCategory Ic On D.Description = Ic.Description
                        Where Ig.Department Is Not Null 
                        And Ig.ItemCategory Is Null
                        And Ic.Code Is Not Null
                        And Ig.V_Type = 'IG' "
                Dim DtItemGroup As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

                For I As Integer = 0 To DtItemGroup.Rows.Count - 1
                    mQry = "UPDATE Item Set ItemCategory = '" & AgL.XNull(DtItemGroup.Rows(I)("ItemCategory")) & "'
                            Where Code = '" & AgL.XNull(DtItemGroup.Rows(I)("Code")) & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                Next

                mQry = "UPDATE SaleInvoiceSetting Set CalculateContraBalanceOnValueYN = 1 Where Code = 'SI'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                mQry = " UPDATE Division Set ScopeOfWork = '+Double Entry Module+Sales Order Module'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

                Try
                    If Not AgL.IsTableExist("SaleInvoiceGeneratedEntries", AgL.GcnMain) Then
                        mQry = " CREATE TABLE [SaleInvoiceGeneratedEntries] ( Code nVarchar(10) Not Null COLLATE NOCASE); "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                    End If
                    AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoiceGeneratedEntries", "Type", "nVarchar(20)", "", True)
                    AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoiceGeneratedEntries", "DocId", "nVarchar(21)", "", True)
                    AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoiceGeneratedEntries", "SaleOrderNo", "nVarchar(20)", "", True)
                    AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoiceGeneratedEntries", "Site_Code", "nVarchar(2)", "", True, " References SiteMast(Code) COLLATE NOCASE")
                    AgL.AddFieldSqlite(AgL.GcnMain, "SaleInvoiceGeneratedEntries", "Div_Code", "nVarchar(1)", "", True, " References Division(Div_Code) COLLATE NOCASE")
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " [UpdateTableStructure_ShyamaShyam_K] ")
        End Try
    End Sub
End Class
