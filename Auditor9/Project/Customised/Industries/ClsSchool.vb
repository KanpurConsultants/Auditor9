Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain
Public Class ClsSchool
    Private mQry As String = ""
    Private mItemTypeFieldQry As String = ""

    Public Const SubGroupType_Fee As String = "Fee"
    Public Const SubGroupType_FeeHead As String = "Fee Head"
    Public Const SubGroupType_Class As String = "Class"
    Public Const SubGroupType_Section As String = "Section"
    Public Const SubGroupType_Facility As String = "Facility"
    Public Const SubGroupType_FacilityHead As String = "Facility Head"
    Public Const SubGroupType_Student As String = "Student"
    Public Const SubGroupType_House As String = "House"

    Public Const ItemV_Type_ClassFee As String = "CFee"
    Public Const ItemV_Type_FacilityFee As String = "FFee"

    Public Const Recurrence_Monthly As String = "Monthly"
    Public Const Recurrence_BiMonthly As String = "Bimonthly"
    Public Const Recurrence_Quarterly As String = "Quarterly"
    Public Const Recurrence_HalfYearly As String = "Half Yearly"
    Public Const Recurrence_Yearly As String = "Yearly"
    Public Const Recurrence_OnceInALifeTime As String = "Once In A Life Time"

    'Public Const Account_FeeDue As String = "Fee Due A/c"
    Public Const Account_FeeDiscount As String = "FeeDisc"

    Public Const NCat_FeeDue As String = "FD"
    Public Const NCat_FeeReceipt As String = "FR"

    Public Const SettingFields_LateFeeAfterDays = "Late Fee After Days"
    Public Const SettingFields_LateFeeAmount = "Late Fee Amount"
    Public Const SettingFields_LateFeeRecurrence = "Late Fee Recurrence"

    Public Const Fee_TuitionFee As String = "TUTFEE"
    Public Const Fee_LateFee As String = "LATEFEE"
    Public Sub FSeedData_School()
        Dim ClsObj As New Customised.ClsMain(AgL)
        Try
            If ClsMain.IsScopeOfWorkContains(IndustryType.SchoolIndustry) Then
                FInitVariables()

                FAlterTable_LedgerHead()
                FAlterTable_SubGroup()

                FCreateTable_FeeStructureRecurrence()
                FCreateTable_FeeStructure()
                FCreateTable_SubgroupAdmission()
                FCreateTable_SubgroupFacility()
                FCreateTable_FeeAdjustmentDetail()

                FCreateView_FeeDueDetail()

                FConfigure_SubGroupType_SingleLineMasters(ClsObj, SubGroupType_Fee)
                FConfigure_SubGroupType_SingleLineMasters(ClsObj, SubGroupType_FeeHead)
                FConfigure_SubGroupType_SingleLineMasters(ClsObj, SubGroupType_Class)
                FConfigure_SubGroupType_SingleLineMasters(ClsObj, SubGroupType_Section)
                FConfigure_SubGroupType_SingleLineMasters(ClsObj, SubgroupType.Caste)
                FConfigure_SubGroupType_SingleLineMasters(ClsObj, SubgroupType.Religion)
                FConfigure_SubGroupType_SingleLineMasters(ClsObj, SubGroupType_Facility)
                FConfigure_SubGroupType_SingleLineMasters(ClsObj, SubGroupType_FacilityHead)
                FConfigure_SubGroupType_SingleLineMasters(ClsObj, SubGroupType_House)
                FConfigure_Student(ClsObj)
                FConfigure_Voucher_Type(ClsObj)

                FConfigure_Accounts(ClsObj)
                FConfigure_Fee(ClsObj)
                FConfigure_ClassFee(ClsObj)
                FConfigure_FacilityFee(ClsObj)
                FConfigure_FeeDue(ClsObj)
                FConfigure_FeeReceipt(ClsObj)
                FConfigure_Settings(ClsObj)

                FYearEnd()
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " In FSeedData_SchoolIndustry")
        End Try
    End Sub
    Private Sub FInitVariables()
        mItemTypeFieldQry = "SELECT Code, Name FROM ItemType Order By Name"
    End Sub
    Private Sub FConfigure_SubGroupType_SingleLineMasters(ClsObj As ClsMain, SubGroupTypeStr As String)
        If AgL.FillData("Select * from SubGroupType Where SubgroupType='" & SubGroupTypeStr & "'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " Insert Into SubGroupType (SubgroupType,IsCustomUI,IsActive, Parent)
                        Values ('" & SubGroupTypeStr & "',0, 1, '" & SubGroupTypeStr & "');                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If

        If AgL.FillData("Select * from SubgroupTypeSetting Where SubgroupType='" & SubGroupTypeStr & "'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = "INSERT INTO SubgroupTypeSetting (SubgroupType, Div_Code, Site_Code, AcGroupCode, PersonCanHaveSiteWiseAgentYn, PersonCanHaveDivisionWiseAgentYn, PersonCanHaveSiteWiseTransporterYn, PersonCanHaveDivisionWiseTransporterYn, PersonCanHaveSiteWiseRateTypeYn, PersonCanHaveDivisionWiseRateTypeYn, PersonCanHaveItemGroupWiseInterestSlabYn, PersonCanHaveItemCategoryWiseInterestSlabYn, PersonCanHaveItemGroupWiseDiscountYn, PersonCanHaveItemCategoryWiseDiscountYn, PersonCanHaveOwnDistanceYn, Default_SalesTaxGroupPerson, FilterInclude_SubgroupTypeForMasterParty)
                    VALUES ('" & SubGroupTypeStr & "', NULL, NULL, '0020', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 'Unregistered', NULL) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupTypeStr, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.SubgroupType, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupTypeStr, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Code, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupTypeStr, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Name, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupTypeStr, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.AcGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmPerson", SubGroupTypeStr, "Dgl1", ConfigurableFields.FrmPersonHeaderDgl1.Remarks, 0, 0)
    End Sub

    Private Sub FConfigure_Student(ClsObj As ClsMain)
        If AgL.FillData("Select * from SubGroupType Where SubgroupType='" & SubGroupType_Student & "'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " Insert Into SubGroupType (SubgroupType,IsCustomUI,IsActive, Parent)
                        Values ('" & SubGroupType_Student & "',0, 1, '" & SubGroupType_Student & "');                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If

        If AgL.FillData("Select * from SubgroupTypeSetting Where SubgroupType='" & SubGroupType_Student & "'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = "INSERT INTO SubgroupTypeSetting (SubgroupType, Div_Code, Site_Code, AcGroupCode, PersonCanHaveSiteWiseAgentYn, PersonCanHaveDivisionWiseAgentYn, PersonCanHaveSiteWiseTransporterYn, PersonCanHaveDivisionWiseTransporterYn, PersonCanHaveSiteWiseRateTypeYn, PersonCanHaveDivisionWiseRateTypeYn, PersonCanHaveItemGroupWiseInterestSlabYn, PersonCanHaveItemCategoryWiseInterestSlabYn, PersonCanHaveItemGroupWiseDiscountYn, PersonCanHaveItemCategoryWiseDiscountYn, PersonCanHaveOwnDistanceYn, Default_SalesTaxGroupPerson, FilterInclude_SubgroupTypeForMasterParty)
                    VALUES ('" & SubGroupType_Student & "', NULL, NULL, '0020', 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 'Unregistered', NULL) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcSubgroupType)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcCode)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcName, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcPrintingDescription, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcFatherName, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcMotherName, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcAddress, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcCity, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcPincode, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcContactNo, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcMobile, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcEmail, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcSite)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcAcGroup)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcContactPerson)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcPanNo)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcAadharNo)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcParent)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcArea)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcBankName)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcBankAccount)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcBankIFSC)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcShowAccountInOtherDivisions)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcShowAccountInOtherSites)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcBlockedTransactions)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcLockText)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcReligion, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcCaste, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcGender, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcAdmissionDate, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcLeftDate, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcDOB, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcFeeHead, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcClass, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcSection, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcRollNo, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcHouse, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcDiscount, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmStudent", SubGroupType_Student, "DglMain", FrmStudent.hcRemarks)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStudent", SubGroupType_Student, "Dgl1", FrmStudent.ColSNo, True,,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStudent", SubGroupType_Student, "Dgl1", FrmStudent.Col1Facility, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStudent", SubGroupType_Student, "Dgl1", FrmStudent.Col1FacilitySubHead, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStudent", SubGroupType_Student, "Dgl1", FrmStudent.Col1StartDate, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStudent", SubGroupType_Student, "Dgl1", FrmStudent.Col1EndDate, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStudent", SubGroupType_Student, "Dgl1", FrmStudent.Col1ChargeableFrom, True,,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStudent", SubGroupType_Student, "Dgl1", FrmStudent.Col1ChargeableUpTo, True,,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmStudent", SubGroupType_Student, "Dgl1", FrmStudent.Col1Remark, True)
    End Sub
    Private Sub FConfigure_Voucher_Type(ClsObj As ClsMain)
        Dim MdiObj As New MDISchool
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(NCat_FeeDue, "Fee Due", NCat_FeeDue, VoucherCategory.Journal, "", "Customised", MdiObj.MnuFeeDueEntry.Name, MdiObj.MnuFeeDueEntry.Text)
        ClsObj.FSeedSingleIfNotExists_Voucher_Type(NCat_FeeReceipt, "Fee Receipt", NCat_FeeReceipt, VoucherCategory.Receipt, "", "Customised", MdiObj.MnuFeeReceiptEntry.Name, MdiObj.MnuFeeReceiptEntry.Text)
    End Sub
    Private Sub FConfigure_ClassFee(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmClassFee", "", "DglMain", FrmClassFee.hcClass, 1, 0, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmClassFee", "", "DglMain", FrmClassFee.hcFeeStructureName, 0, 0, 0)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmClassFee", "", "Dgl1", FrmClassFee.ColSNo, True,,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmClassFee", "", "Dgl1", FrmClassFee.Col1Fee, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmClassFee", "", "Dgl1", FrmClassFee.Col1SubHead, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmClassFee", "", "Dgl1", FrmClassFee.Col1Recurrence, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmClassFee", "", "Dgl1", FrmClassFee.Col1Narration, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmClassFee", "", "Dgl1", FrmClassFee.Col1DueDate, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmClassFee", "", "Dgl1", FrmClassFee.Col1Amount, True)
    End Sub
    Private Sub FConfigure_FacilityFee(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFacilityFee", "", "DglMain", FrmFacilityFee.hcFacility, 1, 0, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFacilityFee", "", "DglMain", FrmFacilityFee.hcFeeStructureName, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFacilityFee", "", "DglMain", FrmFacilityFee.hcRecurrence, 1, 1)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmFacilityFee", "", "Dgl1", FrmFacilityFee.ColSNo, True,,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmFacilityFee", "", "Dgl1", FrmFacilityFee.Col1SubHead, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmFacilityFee", "", "Dgl1", FrmFacilityFee.Col1Narration, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmFacilityFee", "", "Dgl1", FrmFacilityFee.Col1DueDate, True)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmFacilityFee", "", "Dgl1", FrmFacilityFee.Col1Amount, True)
    End Sub
    Private Sub FConfigure_FeeReceipt(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeReceiptEntry", NCat_FeeReceipt, "DglMain", AgTemplate.TempTransaction1.hcSite_Code, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeReceiptEntry", NCat_FeeReceipt, "DglMain", AgTemplate.TempTransaction1.hcV_Type, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeReceiptEntry", NCat_FeeReceipt, "DglMain", AgTemplate.TempTransaction1.hcV_Date, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeReceiptEntry", NCat_FeeReceipt, "DglMain", AgTemplate.TempTransaction1.hcV_No, 0, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeReceiptEntry", NCat_FeeReceipt, "DglMain", AgTemplate.TempTransaction1.hcReferenceNo, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeReceiptEntry", NCat_FeeReceipt, "DglMain", AgTemplate.TempTransaction1.hcSettingGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeReceiptEntry", NCat_FeeReceipt, "DglMain", FrmFeeReceiptEntry.hcClass, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeReceiptEntry", NCat_FeeReceipt, "DglMain", FrmFeeReceiptEntry.hcStudent, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeReceiptEntry", NCat_FeeReceipt, "DglMain", FrmFeeReceiptEntry.hcPaymentAc, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeReceiptEntry", NCat_FeeReceipt, "DglMain", FrmFeeReceiptEntry.hcRemarks, 1)

        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeReceiptEntry", NCat_FeeReceipt, "Dgl2", FrmFeeReceiptEntry.hcTotalDueAmount, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeReceiptEntry", NCat_FeeReceipt, "Dgl2", FrmFeeReceiptEntry.hcLateFeeCalculated, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeReceiptEntry", NCat_FeeReceipt, "Dgl2", FrmFeeReceiptEntry.hcLateFee, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeReceiptEntry", NCat_FeeReceipt, "Dgl2", FrmFeeReceiptEntry.hcDiscount, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeReceiptEntry", NCat_FeeReceipt, "Dgl2", FrmFeeReceiptEntry.hcPayableAmount, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeReceiptEntry", NCat_FeeReceipt, "Dgl2", FrmFeeReceiptEntry.hcPaidAmount, 1, 1)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmFeeReceiptEntry", NCat_FeeReceipt, "Dgl1", FrmFeeReceiptEntry.ColSNo, True, True,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmFeeReceiptEntry", NCat_FeeReceipt, "Dgl1", FrmFeeReceiptEntry.Col1Comp_Code, True,,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmFeeReceiptEntry", NCat_FeeReceipt, "Dgl1", FrmFeeReceiptEntry.Col1Class, True,,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmFeeReceiptEntry", NCat_FeeReceipt, "Dgl1", FrmFeeReceiptEntry.Col1Fee, True,,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmFeeReceiptEntry", NCat_FeeReceipt, "Dgl1", FrmFeeReceiptEntry.Col1SubHead, True,,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmFeeReceiptEntry", NCat_FeeReceipt, "Dgl1", FrmFeeReceiptEntry.Col1DueDate, True,,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmFeeReceiptEntry", NCat_FeeReceipt, "Dgl1", FrmFeeReceiptEntry.Col1Amount, True,,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmFeeReceiptEntry", NCat_FeeReceipt, "Dgl1", FrmFeeReceiptEntry.Col1AdjustedAmount, True,,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmFeeReceiptEntry", NCat_FeeReceipt, "Dgl1", FrmFeeReceiptEntry.Col1IsFeeDueExplicitly, False,,, False)
    End Sub
    Private Sub FConfigure_Accounts(ClsObj As ClsMain)
        'ClsObj.FSeedSingleIfNotExist_Subgroup(Account_FeeDue, "Fee Due A/c", SubgroupType.LedgerAccount, "0009", "L", "Others", "", "", AgL.PubDivCode, AgL.PubSiteCode, "", "System Defined")
        ClsObj.FSeedSingleIfNotExist_Subgroup(Account_FeeDiscount, "Fee Discount A/c", SubgroupType.LedgerAccount, "0009", "L", "Others", "", "", AgL.PubDivCode, AgL.PubSiteCode, "", "System Defined")
    End Sub

    Private Sub FConfigure_FeeDue(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeDueEntry", NCat_FeeDue, "DglMain", AgTemplate.TempTransaction1.hcSite_Code, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeDueEntry", NCat_FeeDue, "DglMain", AgTemplate.TempTransaction1.hcV_Type, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeDueEntry", NCat_FeeDue, "DglMain", AgTemplate.TempTransaction1.hcV_Date, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeDueEntry", NCat_FeeDue, "DglMain", AgTemplate.TempTransaction1.hcV_No, 0, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeDueEntry", NCat_FeeDue, "DglMain", AgTemplate.TempTransaction1.hcReferenceNo, 1, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeDueEntry", NCat_FeeDue, "DglMain", AgTemplate.TempTransaction1.hcSettingGroup, 0, 0, 0)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeDueEntry", NCat_FeeDue, "DglMain", FrmFeeDueEntry.hcClass, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeDueEntry", NCat_FeeDue, "DglMain", FrmFeeDueEntry.hcFee, 1, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeDueEntry", NCat_FeeDue, "DglMain", FrmFeeDueEntry.hcAmount, 1)
        ClsObj.FSeedSingleIfNotExist_EntryHeaderUISetting("FrmFeeDueEntry", NCat_FeeDue, "DglMain", FrmFeeDueEntry.hcRemarks, 1)

        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmFeeDueEntry", NCat_FeeDue, "Dgl1", FrmFeeDueEntry.ColSNo, True, True,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmFeeDueEntry", NCat_FeeDue, "Dgl1", FrmFeeDueEntry.Col1SubCode, True,,, False)
        ClsObj.FSeedSingleIfNotExist_EntryLineUISetting("FrmFeeDueEntry", NCat_FeeDue, "Dgl1", FrmFeeDueEntry.Col1Amount, True,,, False)
    End Sub
    Private Sub FConfigure_Settings(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields_LateFeeAfterDays, "", AgDataType.Number)
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields_LateFeeAmount, "", AgDataType.Number)

        mQry = "Select '" & ClsSchool.Recurrence_Monthly & "' As Code, '" & ClsSchool.Recurrence_Monthly & "' As Name
                UNION ALL 
                Select '" & ClsSchool.Recurrence_BiMonthly & "' As Code, '" & ClsSchool.Recurrence_BiMonthly & "' As Name
                UNION ALL 
                Select '" & ClsSchool.Recurrence_Quarterly & "' As Code, '" & ClsSchool.Recurrence_Quarterly & "' As Name 
                UNION ALL 
                Select '" & ClsSchool.Recurrence_HalfYearly & "' As Code, '" & ClsSchool.Recurrence_HalfYearly & "' As Name 
                UNION ALL 
                Select '" & ClsSchool.Recurrence_Yearly & "' As Code, '" & ClsSchool.Recurrence_Yearly & "' As Name 
                UNION ALL 
                Select '" & ClsSchool.Recurrence_OnceInALifeTime & "' As Code, '" & ClsSchool.Recurrence_OnceInALifeTime & "' As Name "
        ClsObj.FSeedSingleIfNotExist_Setting(SettingType.General, "", "", SettingFields_LateFeeRecurrence, "", AgDataType.Text, 50, mQry)
    End Sub
    Private Sub FConfigure_Fee(ClsObj As ClsMain)
        ClsObj.FSeedSingleIfNotExist_Subgroup(Fee_TuitionFee, "Tuition Fee", SubGroupType_Fee, "0009", "L", "Others", "", "", AgL.PubDivCode, AgL.PubSiteCode, "", "System Defined")
        ClsObj.FSeedSingleIfNotExist_Subgroup(Fee_LateFee, "Late Fee", SubGroupType_Fee, "0009", "L", "Others", "", "", AgL.PubDivCode, AgL.PubSiteCode, "", "System Defined")
    End Sub
    Private Sub FAlterTable_LedgerHead()
        Try
            AgL.AddFieldSqlite(AgL.GcnMain, "LedgerHead", "LateFeeCalculated", "Float", "0", False)
            AgL.AddFieldSqlite(AgL.GcnMain, "LedgerHead", "LateFee", "Float", "0", False)
            AgL.AddFieldSqlite(AgL.GcnMain, "LedgerHead", "Discount", "Float", "0", False)
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_LedgerHead]")
        End Try
    End Sub
    Private Sub FAlterTable_SubGroup()
        Try
            AgL.AddFieldSqlite(AgL.GcnMain, "SubGroup", "Discount", "Float", "0", False)
            AgL.AddFieldSqlite(AgL.GcnMain, "SubGroup", "LeftDate", "DateTime", "", True)
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_LedgerHead]")
        End Try
    End Sub
    Private Sub FCreateTable_FeeStructureRecurrence()
        Dim mQry As String
        Try
            If Not AgL.IsTableExist("FeeStructureRecurrence", AgL.GcnMain) Then
                mQry = " CREATE TABLE [FeeStructureRecurrence] (Code nVarchar(10),
                        [Sr] int NOT NULL,
                        PRIMARY KEY ([Code],[Sr])); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeStructureRecurrence", "Class", "nVarchar(10)", "", True, "References SubGroup(SubCode)")
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeStructureRecurrence", "Fee", "nVarchar(10)", "", True, "References SubGroup(SubCode)")
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeStructureRecurrence", "SubHead", "nVarchar(10)", "", True, "References SubGroup(SubCode)")
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeStructureRecurrence", "Narration", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeStructureRecurrence", "Recurrence", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeStructureRecurrence", "Amount", "float", "0", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeStructureRecurrence", "DueDate", "DateTime", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeStructureRecurrence", "Comp_Code", "nVarchar(5)", "", True, "References Company(Comp_Code)")
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeStructureRecurrence", "Div_Code", "nVarchar(1)", "", True, "References Division(Div_Code)")
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeStructureRecurrence", "Site_Code", "nVarchar(2)", "", True, "References SiteMast(Code)")
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_FeeStructure]")
        End Try
    End Sub
    Private Sub FCreateTable_FeeStructure()
        Dim mQry As String
        Try
            If Not AgL.IsTableExist("FeeStructure", AgL.GcnMain) Then
                mQry = " CREATE TABLE [FeeStructure] (Code nVarchar(10),
                        [Sr] int NOT NULL,
                        PRIMARY KEY ([Code],[Sr])); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeStructure", "Class", "nVarchar(10)", "", True, "References SubGroup(SubCode)")
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeStructure", "Fee", "nVarchar(10)", "", True, "References SubGroup(SubCode)")
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeStructure", "SubHead", "nVarchar(10)", "", True, "References SubGroup(SubCode)")
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeStructure", "Recurrence", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeStructure", "Narration", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeStructure", "Amount", "float", "0", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeStructure", "DueDate", "DateTime", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeStructure", "Comp_Code", "nVarchar(5)", "", True, "References Company(Comp_Code)")
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeStructure", "Div_Code", "nVarchar(1)", "", True, "References Division(Div_Code)")
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeStructure", "Site_Code", "nVarchar(2)", "", True, "References SiteMast(Code)")
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_FeeStructure]")
        End Try
    End Sub
    Private Sub FCreateTable_FeeAdjustmentDetail()
        Dim mQry As String
        Try
            If Not AgL.IsTableExist("FeeAdjustmentDetail", AgL.GcnMain) Then
                mQry = " CREATE TABLE [FeeAdjustmentDetail] ([DocID] nVarchar(21) NOT NULL  COLLATE NOCASE, 
                   [Sr] Int Not Null,
                   PRIMARY KEY ([DocID],[Sr]) ); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeAdjustmentDetail", "Class", "nVarchar(10)", "", True, "References Subgroup(Subcode)")
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeAdjustmentDetail", "Fee", "nVarchar(10)", "", True, "References Subgroup(Subcode)")
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeAdjustmentDetail", "SubHead", "nVarchar(10)", "", True, "References Subgroup(Subcode)")
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeAdjustmentDetail", "Comp_Code", "nVarchar(5)", "", True, "References Company(Comp_Code)")
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeAdjustmentDetail", "DueDate", "DateTime", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeAdjustmentDetail", "Amount", "Float", "0", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeAdjustmentDetail", "AdjustedAmount", "Float", "0", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "FeeAdjustmentDetail", "IsFeeDueExplicitly", "Bit", "0", True)
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_FeeAdjustmentDetail]")
        End Try
    End Sub
    Private Sub FCreateTable_SubgroupAdmission()
        Dim mQry As String
        Try
            If Not AgL.IsTableExist("SubgroupAdmission", AgL.GcnMain) Then
                mQry = " CREATE TABLE [SubgroupAdmission] ([Subcode] nVarchar(10) NOT NULL References Subgroup(Subcode),                    
                   [Sr] int NOT NULL,
                   PRIMARY KEY ([Subcode],[Sr]) ); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
            AgL.AddFieldSqlite(AgL.GcnMain, "SubgroupAdmission", "Comp_Code ", "nVarchar(20)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "SubgroupAdmission", "Div_Code", "nVarchar(1)", "", True, "References Division(Div_Code)")
            AgL.AddFieldSqlite(AgL.GcnMain, "SubgroupAdmission", "Site_Code", "nVarchar(2)", "", True, "References SiteMast(Code)")
            AgL.AddFieldSqlite(AgL.GcnMain, "SubgroupAdmission", "AdmissionDate", "DateTime", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "SubgroupAdmission", "PromotionDate", "DateTime", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "SubgroupAdmission", "PromotionFrom", "nVarchar(10)", "", True, "References SubGroup(SubCode)")
            AgL.AddFieldSqlite(AgL.GcnMain, "SubgroupAdmission", "FeeHead", "nVarchar(10)", "", True, "References SubGroup(SubCode)")
            AgL.AddFieldSqlite(AgL.GcnMain, "SubgroupAdmission", "Class", "nVarchar(10)", "", True, "References SubGroup(SubCode)")
            AgL.AddFieldSqlite(AgL.GcnMain, "SubgroupAdmission", "Section", "nVarchar(10)", "", True, "References SubGroup(SubCode)")
            AgL.AddFieldSqlite(AgL.GcnMain, "SubgroupAdmission", "RollNo", "nVarchar(20)", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "SubgroupAdmission", "House", "nVarchar(10)", "", True, "References Subgroup(SubCode)")
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_SubgroupAdmission]")
        End Try
    End Sub
    Private Sub FCreateTable_SubgroupFacility()
        Dim mQry As String
        Try
            If Not AgL.IsTableExist("SubgroupFacility", AgL.GcnMain) Then
                mQry = " CREATE TABLE [SubgroupFacility] ([Subcode] nVarchar(10) NOT NULL References Subgroup(Subcode),                    
                   [Sr] int NOT NULL,
                   PRIMARY KEY ([Subcode],[Sr]) ); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
            AgL.AddFieldSqlite(AgL.GcnMain, "SubgroupFacility", "Div_Code", "nVarchar(1)", "", True, "References Division(Div_Code)")
            AgL.AddFieldSqlite(AgL.GcnMain, "SubgroupFacility", "Site_Code", "nVarchar(2)", "", True, "References SiteMast(Code)")
            AgL.AddFieldSqlite(AgL.GcnMain, "SubgroupFacility", "Facility", "nVarchar(10)", "", True, "References SubGroup(SubCode)")
            AgL.AddFieldSqlite(AgL.GcnMain, "SubgroupFacility", "FacilitySubHead", "nVarchar(10)", "", True, "References Subgroup(Subcode)")
            AgL.AddFieldSqlite(AgL.GcnMain, "SubgroupFacility", "StartDate", "DateTime", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "SubgroupFacility", "EndDate", "DateTime", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "SubgroupFacility", "ChargeableFrom", "DateTime", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "SubgroupFacility", "ChargeableUpto", "DateTime", "", True)
            AgL.AddFieldSqlite(AgL.GcnMain, "SubgroupFacility", "Remark", "nVarchar(255)", "", True)
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_SubgroupFacility]")
        End Try
    End Sub
    Private Sub FCreateView_FeeDueDetail()
        Dim mQry As String
        Try
            If AgL.PubServerName = "" Then
                AgL.Dman_ExecuteNonQry("Drop View IF Exists FeeDueDetail;", AgL.GcnMain)
            Else
                AgL.Dman_ExecuteNonQry("If object_id('FeeDueDetail') is not null drop view LrBaleSiteDetail;", AgL.GcnMain)
            End If

            Dim mClassFeeDueIfOnceInALifeTime_False As String = ""
            Dim mClassFeeDueIfOnceInALifeTime_True As String = ""
            Dim mFaciltyFeeDueIfOnceInALifeTime_False As String = ""
            Dim mFaciltyFeeDueIfOnceInALifeTime_True As String = ""
            Dim mFeeDueExplicitily As String = ""

            mClassFeeDueIfOnceInALifeTime_False = "Select H.Subcode, Fs.Site_Code, Fs.Div_Code, Fs.Comp_Code, Fs.Class, Fs.Fee, Fs.SubHead, Fs.DueDate, Fs.Amount As FeeAmount, IfNull(VAdjust.Amount,0) As ReceivedAmount,
                        Fs.Amount - IfNull(VAdjust.Amount,0) As BalanceAmount, 0 As IsFeeDueExplicitly  
                        From SubgroupAdmission H
                        LEFT JOIN FeeStructure Fs ON H.Class = Fs.Class And H.Comp_Code = Fs.Comp_Code
                        LEFT JOIN SubGroup Sg On H.SubCode = Sg.SubCode
                        LEFT JOIN (
                            Select H.SubCode, H.Site_Code, H.Div_Code, L.Comp_Code, L.Class, L.Fee, L.SubHead, L.DueDate, Sum(L.AdjustedAmount) As Amount
                            From FeeAdjustmentDetail L 
                            LEFT JOIN LedgerHead H On L.DocId = H.DocId
                            Group By H.SubCode, H.Site_Code, H.Div_Code, L.Comp_Code, 
                            L.Class, L.Fee, L.SubHead, L.DueDate
                        ) As VAdjust On H.SubCode = VAdjust.SubCode
                            And Fs.Site_Code = VAdjust.Site_Code And Fs.Div_Code = VAdjust.Div_Code 
                            And Fs.Comp_Code = VAdjust.Comp_Code 
                            And Fs.Class = VAdjust.Class
                            And Fs.Fee = VAdjust.Fee 
                            And IfNull(Fs.SubHead,'') = IfNull(VAdjust.SubHead,'') 
                            And Fs.DueDate = VAdjust.DueDate
                        Where Fs.Recurrence <> '" & ClsSchool.Recurrence_OnceInALifeTime & "'
                        And Sg.LeftDate Is Null "

            mClassFeeDueIfOnceInALifeTime_True = " Select H.Subcode, Fs.Site_Code, Fs.Div_Code, Fs.Comp_Code, Fs.Class, Fs.Fee, Fs.SubHead, Fs.DueDate, Fs.Amount As FeeAmount, IfNull(VAdjust.Amount,0) As ReceivedAmount, 
                        Fs.Amount - IfNull(VAdjust.Amount,0) As BalanceAmount, 0 As IsFeeDueExplicitly    
                        From SubgroupAdmission H
                        LEFT JOIN FeeStructure Fs ON H.Class = Fs.Class And H.Comp_Code = Fs.Comp_Code
                        LEFT JOIN SubGroup Sg On H.SubCode = Sg.SubCode
                        LEFT JOIN (
                            Select H.SubCode, H.Site_Code, H.Div_Code, L.Comp_Code, L.Class, L.Fee, L.SubHead, L.DueDate, Sum(L.AdjustedAmount) As Amount
                            From FeeAdjustmentDetail L 
                            LEFT JOIN LedgerHead H On L.DocId = H.DocId
                            Group By H.SubCode, H.Site_Code, H.Div_Code, L.Comp_Code, 
                            L.Class, L.Fee, L.SubHead, L.DueDate
                        ) As VAdjust On H.SubCode = VAdjust.SubCode
                            And Fs.Site_Code = VAdjust.Site_Code And Fs.Div_Code = VAdjust.Div_Code 
                            And Fs.Comp_Code = VAdjust.Comp_Code 
                            And Fs.Class = VAdjust.Class
                            And Fs.Fee = VAdjust.Fee 
                            And IfNull(Fs.SubHead,'') = IfNull(VAdjust.SubHead,'') 
                            And Fs.DueDate = VAdjust.DueDate
                        Where Fs.Recurrence = '" & ClsSchool.Recurrence_OnceInALifeTime & "'
                        And H.AdmissionDate >= Fs.DueDate
                        And Sg.LeftDate Is Null "

            mFaciltyFeeDueIfOnceInALifeTime_False = " Select H.Subcode, Fs.Site_Code, Fs.Div_Code, Fs.Comp_Code, Sgad.Class As Class, Fs.Fee, Fs.SubHead, Fs.DueDate, Fs.Amount,
                        IfNull(VAdjust.Amount,0) As ReceivedAmount, Fs.Amount - IfNull(VAdjust.Amount,0) As BalanceAmount, 0 As IsFeeDueExplicitly      
                        From SubgroupFacility H
                        LEFT JOIN (Select * From SubGroupAdmission Where PromotionDate Is Null) As Sgad ON H.SubCode = Sgad.SubCode 
                        LEFT JOIN SubGroup Sg On H.SubCode = Sg.SubCode
                        LEFT JOIN FeeStructure Fs On H.Facility = Fs.Fee 
                                And IfNull(H.FacilitySubHead,'') = IfNull(Fs.SubHead,'') 
                                And H.Div_Code = Fs.Div_Code And H.Site_Code = Fs.Site_Code
                        LEFT JOIN (
                            Select H.SubCode, H.Site_Code, H.Div_Code, L.Comp_Code, L.Class, L.Fee, L.SubHead, L.DueDate, Sum(L.AdjustedAmount) As Amount
                            From FeeAdjustmentDetail L 
                            LEFT JOIN LedgerHead H On L.DocId = H.DocId
                            Group By H.SubCode, H.Site_Code, H.Div_Code, L.Comp_Code, 
                            L.Class, L.Fee, L.SubHead, L.DueDate
                        ) As VAdjust On H.SubCode = VAdjust.SubCode
                            And Fs.Site_Code = VAdjust.Site_Code And Fs.Div_Code = VAdjust.Div_Code 
                            And Fs.Comp_Code = VAdjust.Comp_Code 
                            And Fs.Fee = VAdjust.Fee 
                            And IfNull(Fs.SubHead,'') = IfNull(VAdjust.SubHead,'')
                            And Fs.DueDate = VAdjust.DueDate
                        where  Fs.Class Is Null
                        And Fs.DueDate Between H.ChargeableFrom And ChargeableUpto 
                        And Fs.Recurrence <> '" & ClsSchool.Recurrence_OnceInALifeTime & "'
                        And Sg.LeftDate Is Null "

            mFaciltyFeeDueIfOnceInALifeTime_True = " Select H.Subcode, Fs.Site_Code, Fs.Div_Code, Fs.Comp_Code, Sgad.Class As Class, Fs.Fee, Fs.SubHead, Fs.DueDate, Fs.Amount,
                        IfNull(VAdjust.Amount,0) As ReceivedAmount, Fs.Amount - IfNull(VAdjust.Amount,0) As BalanceAmount, 0 As IsFeeDueExplicitly      
                        From SubgroupFacility H
                        LEFT JOIN (Select * From SubGroupAdmission Where PromotionDate Is Null) As Sgad ON H.SubCode = Sgad.SubCode 
                        LEFT JOIN SubGroup Sg On H.SubCode = Sg.SubCode
                        LEFT JOIN FeeStructure Fs On H.Facility = Fs.Fee 
                                And IfNull(H.FacilitySubHead,'') = IfNull(Fs.SubHead,'') 
                                And H.Div_Code = Fs.Div_Code And H.Site_Code = Fs.Site_Code
                        LEFT JOIN (
                            Select H.SubCode, H.Site_Code, H.Div_Code, L.Comp_Code, L.Class, L.Fee, L.SubHead, L.DueDate, Sum(L.AdjustedAmount) As Amount
                            From FeeAdjustmentDetail L 
                            LEFT JOIN LedgerHead H On L.DocId = H.DocId
                            Group By H.SubCode, H.Site_Code, H.Div_Code, L.Comp_Code, 
                            L.Class, L.Fee, L.SubHead, L.DueDate
                        ) As VAdjust On H.SubCode = VAdjust.SubCode
                            And Fs.Site_Code = VAdjust.Site_Code And Fs.Div_Code = VAdjust.Div_Code 
                            And Fs.Comp_Code = VAdjust.Comp_Code 
                            And Fs.Fee = VAdjust.Fee 
                            And IfNull(Fs.SubHead,'') = IfNull(VAdjust.SubHead,'')
                            And Fs.DueDate = VAdjust.DueDate
                        Where Fs.Class Is Null
                        And Fs.DueDate Between H.ChargeableFrom And ChargeableUpto 
                        And Fs.Recurrence = '" & ClsSchool.Recurrence_OnceInALifeTime & "'
                        And Sgad.AdmissionDate >= Fs.DueDate
                        And Sg.LeftDate Is Null "

            mFeeDueExplicitily = " Select  L.Subcode, H.Site_Code, H.Div_Code, Sgad.Comp_Code, Sgad.Class As Class, H.Subcode As Fee, NUll As SubHead, H.V_Date As DueDate, L.Amount,
                    IfNull(VAdjust.Amount,0) As ReceivedAmount, L.Amount - IfNull(VAdjust.Amount,0) As BalanceAmount, 1 As IsFeeDueExplicitly      
                    From LedgerHead H 
                    LEFT JOIN LedgerHeadDetail L On H.DocId = L.DocId
                    LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                    LEFT JOIN Company C On H.V_Date Between C.Start_Dt And End_Dt
                    LEFT JOIN SubGroupAdmission Sgad On L.SubCode = Sgad.SubCode And Sgad.Comp_Code = C.Comp_Code
                    LEFT JOIN SubGroup Sg On H.SubCode = Sg.SubCode
                    LEFT JOIN(
                        Select H.SubCode, H.Site_Code, H.Div_Code, L.Comp_Code, L.Class, L.Fee, L.SubHead, L.DueDate, Sum(L.AdjustedAmount) As Amount
                        From FeeAdjustmentDetail L 
                        LEFT JOIN LedgerHead H On L.DocId = H.DocId
                        Group By H.SubCode, H.Site_Code, H.Div_Code, L.Comp_Code, 
                        L.Class, L.Fee, L.SubHead, L.DueDate
                    ) As VAdjust On L.SubCode = VAdjust.SubCode
                        And H.Site_Code = VAdjust.Site_Code And H.Div_Code = VAdjust.Div_Code 
                        And C.Comp_Code = VAdjust.Comp_Code 
                        And H.SubCode = VAdjust.Fee 
                        And Date(H.V_Date) = Date(VAdjust.DueDate)
                Where Vt.NCat = '" & ClsSchool.NCat_FeeDue & "'
                And Sg.LeftDate Is Null "


            mQry = " CREATE VIEW FeeDueDetail AS " &
                    mClassFeeDueIfOnceInALifeTime_False &
                    " UNION ALL " &
                    mClassFeeDueIfOnceInALifeTime_True &
                    " UNION ALL " &
                    mFaciltyFeeDueIfOnceInALifeTime_False &
                    " UNION ALL " &
                    mFaciltyFeeDueIfOnceInALifeTime_True &
                    " UNION ALL " &
                    mFeeDueExplicitily



            'mQry = " CREATE VIEW FeeDueDetail As
            '            Select H.Subcode, Fs.Site_Code, Fs.Div_Code, Fs.Comp_Code, Fs.Class, Fs.Fee, Fs.SubHead, Fs.DueDate, Fs.Amount As FeeAmount, IfNull(VAdjust.Amount,0) As ReceivedAmount,
            '            Fs.Amount - IfNull(VAdjust.Amount,0) As BalanceAmount  
            '            From SubgroupAdmission H
            '            LEFT JOIN FeeStructure Fs ON H.Class = Fs.Class
            '            LEFT JOIN (
            '                Select H.SubCode, H.Site_Code, H.Div_Code, L.Comp_Code, L.Class, L.Fee, L.SubHead, L.DueDate, Sum(L.AdjustedAmount) As Amount
            '                From FeeAdjustmentDetail L 
            '                LEFT JOIN LedgerHead H On L.DocId = H.DocId
            '                Group By H.Site_Code, H.Div_Code, L.Comp_Code, L.Class, L.Fee, L.SubHead
            '            ) As VAdjust On H.SubCode = VAdjust.SubCode
            '                And Fs.Site_Code = VAdjust.Site_Code And Fs.Div_Code = VAdjust.Div_Code 
            '                And Fs.Comp_Code = VAdjust.Comp_Code And Fs.Class = VAdjust.Class
            '                And Fs.Fee = VAdjust.Fee 
            '                And IfNull(Fs.SubHead,'') = IfNull(VAdjust.SubHead,'') 
            '                And Fs.DueDate = VAdjust.DueDate
            '            Where Fs.Recurrence <> '" & ClsSchool.Recurrence_OnceInALifeTime & "'

            '            UNION ALL

            '            Select H.Subcode, Fs.Site_Code, Fs.Div_Code, Fs.Comp_Code, Sgad.Class As Class, Fs.Fee, Fs.SubHead, Fs.DueDate, Fs.Amount,
            '            IfNull(VAdjust.Amount,0) As ReceivedAmount, Fs.Amount - IfNull(VAdjust.Amount,0) As BalanceAmount  
            '            From SubgroupFacility H
            '            LEFT JOIN (Select * From SubGroupAdmission Where PromotionDate Is Null) As Sgad ON H.SubCode = Sgad.SubCode 
            '            LEFT JOIN FeeStructure Fs On H.Facility = Fs.Fee 
            '                    And IfNull(H.FacilitySubHead,'') = IfNull(Fs.SubHead,'') 
            '                    And H.Div_Code = Fs.Div_Code And H.Site_Code = Fs.Site_Code
            '            LEFT JOIN (
            '                Select H.SubCode, H.Site_Code, H.Div_Code, L.Comp_Code, L.Class, L.Fee, L.SubHead, L.DueDate, Sum(L.AdjustedAmount) As Amount
            '                From FeeAdjustmentDetail L 
            '                LEFT JOIN LedgerHead H On L.DocId = H.DocId
            '                Group By H.Site_Code, H.Div_Code, L.Comp_Code, L.Class, L.Fee, L.SubHead
            '            ) As VAdjust On H.SubCode = VAdjust.SubCode
            '                And Fs.Site_Code = VAdjust.Site_Code And Fs.Div_Code = VAdjust.Div_Code 
            '                And Fs.Comp_Code = VAdjust.Comp_Code 
            '                And Fs.Fee = VAdjust.Fee 
            '                And IfNull(Fs.SubHead,'') = IfNull(VAdjust.SubHead,'')
            '                And Fs.DueDate = VAdjust.DueDate
            '            where  Fs.Class Is Null
            '            And Fs.DueDate Between H.ChargeableFrom And ChargeableUpto 
            '            And Fs.Recurrence <> '" & ClsSchool.Recurrence_OnceInALifeTime & "'

            '            UNION ALL

            '            Select H.Subcode, Fs.Site_Code, Fs.Div_Code, Fs.Comp_Code, Fs.Class, Fs.Fee, Fs.SubHead, Fs.DueDate, Fs.Amount As FeeAmount, IfNull(VAdjust.Amount,0) As ReceivedAmount, Fs.Amount - IfNull(VAdjust.Amount,0) As BalanceAmount  
            '            From SubgroupAdmission H
            '            LEFT JOIN FeeStructure Fs ON H.Class = Fs.Class
            '            LEFT JOIN (
            '                Select H.SubCode, H.Site_Code, H.Div_Code, L.Comp_Code, L.Class, L.Fee, L.SubHead, L.DueDate, Sum(L.AdjustedAmount) As Amount
            '                From FeeAdjustmentDetail L 
            '                LEFT JOIN LedgerHead H On L.DocId = H.DocId
            '                Group By H.Site_Code, H.Div_Code, L.Comp_Code, L.Class, L.Fee, L.SubHead
            '            ) As VAdjust On H.SubCode = VAdjust.SubCode
            '                And Fs.Site_Code = VAdjust.Site_Code And Fs.Div_Code = VAdjust.Div_Code 
            '                And Fs.Comp_Code = VAdjust.Comp_Code And Fs.Class = VAdjust.Class
            '                And Fs.Fee = VAdjust.Fee 
            '                And IfNull(Fs.SubHead,'') = IfNull(VAdjust.SubHead,'') 
            '                And Fs.DueDate = VAdjust.DueDate
            '            Where Fs.Recurrence = '" & ClsSchool.Recurrence_OnceInALifeTime & "'
            '            And H.PromotionFrom Is Null 

            '            UNION ALL

            '            Select H.Subcode, Fs.Site_Code, Fs.Div_Code, Fs.Comp_Code, Sgad.Class As Class, Fs.Fee, Fs.SubHead, Fs.DueDate, Fs.Amount,
            '            IfNull(VAdjust.Amount,0) As ReceivedAmount, Fs.Amount - IfNull(VAdjust.Amount,0) As BalanceAmount  
            '            From SubgroupFacility H
            '            LEFT JOIN (Select * From SubGroupAdmission Where PromotionDate Is Null) As Sgad ON H.SubCode = Sgad.SubCode 
            '            LEFT JOIN FeeStructure Fs On H.Facility = Fs.Fee 
            '                    And IfNull(H.FacilitySubHead,'') = IfNull(Fs.SubHead,'') 
            '                    And H.Div_Code = Fs.Div_Code And H.Site_Code = Fs.Site_Code
            '            LEFT JOIN (
            '                Select H.SubCode, H.Site_Code, H.Div_Code, L.Comp_Code, L.Class, L.Fee, L.SubHead, L.DueDate, Sum(L.AdjustedAmount) As Amount
            '                From FeeAdjustmentDetail L 
            '                LEFT JOIN LedgerHead H On L.DocId = H.DocId
            '                Group By H.Site_Code, H.Div_Code, L.Comp_Code, L.Class, L.Fee, L.SubHead
            '            ) As VAdjust On H.SubCode = VAdjust.SubCode
            '                And Fs.Site_Code = VAdjust.Site_Code And Fs.Div_Code = VAdjust.Div_Code 
            '                And Fs.Comp_Code = VAdjust.Comp_Code 
            '                And Fs.Fee = VAdjust.Fee 
            '                And IfNull(Fs.SubHead,'') = IfNull(VAdjust.SubHead,'')
            '                And Fs.DueDate = VAdjust.DueDate
            '            Where Fs.Class Is Null
            '            And Fs.DueDate Between H.ChargeableFrom And ChargeableUpto 
            '            And Fs.Recurrence = '" & ClsSchool.Recurrence_OnceInALifeTime & "'
            '            And Sgad.PromotionFrom Is Null "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateView_FeeDueDetail]")
        End Try
    End Sub
    Private Sub FYearEnd()
        Dim mCode As String = ""
        Dim mSr As Integer = 0

        mQry = " Select Comp_Code, Comp_Name From Company Where Comp_Code = '" & AgL.PubCompCode & "'"
        Dim DtCompDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim bComp_Code As String = AgL.XNull(DtCompDetail.Rows(0)("Comp_Code"))
        Dim bComp_Name As String = AgL.XNull(DtCompDetail.Rows(0)("Comp_Name"))

        Dim bPrev_Comp_Code As String = (Convert.ToInt32(bComp_Code) - 1).ToString
        mQry = " Select Comp_Code, Comp_Name From Company Where Comp_Code = '" & bPrev_Comp_Code & "'  "
        Dim DtPrevCompDetail As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
        Dim bPrev_Comp_Name As String = AgL.XNull(DtPrevCompDetail.Rows(0)("Comp_Name"))

        mQry = " Select Distinct Fs.Code, I.Description
                From FeeStructure Fs 
                LEFT JOIN Item I ON Fs.Code = I.Code
                Where Fs.Comp_Code = '" & bPrev_Comp_Code & "'"
        Dim DtFeeStructureCodes As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

        For I As Integer = 0 To DtFeeStructureCodes.Rows.Count - 1
            mQry = " Select I.Description, I.Specification, I.V_Type, I.ItemType, 
                I.Status, I.Div_Code As Item_Div_Code,
                PFs.Class, PFs.Fee, PFs.SubHead, PFs.Recurrence, PFs.Narration, PFs.Amount, PFs.DueDate, 
                PFs.Comp_Code, PFs.Div_Code, PFs.Site_Code
                From (Select * From FeeStructure Where Comp_Code = '" & bPrev_Comp_Code & "' And Code = '" & AgL.XNull(DtFeeStructureCodes.Rows(I)("Code")) & "' ) As PFs
                LEFT JOIN (Select * From FeeStructure Where Comp_Code = '" & bComp_Code & "') As Fs
                        On IfNull(Fs.Class,'') = IfNull(PFs.Class,'') And IfNull(Fs.Fee,'') = IfNull(PFs.Fee,'')
                        And IfNull(Fs.SubHead,'') = IfNull(PFs.SubHead,'') And IfNull(Fs.Div_Code,'') = IfNull(PFs.Div_Code,'') 
                        And IfNull(Fs.Site_Code,'') = IfNull(PFs.Site_Code,'') 
                LEFT JOIN Item I ON PFs.Code = I.Code
                Where PFs.Code Is Not Null And Fs.Code Is Null "
            Dim DtFeeStructure As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)


            If DtFeeStructure.Rows.Count > 0 Then
                Dim bItemDesc_New As String = AgL.XNull(DtFeeStructure.Rows(0)("Description")).ToString.Replace(bPrev_Comp_Name, bComp_Name)
                If AgL.VNull(AgL.Dman_Execute(" Select Count(*) From Item Where Description = '" & bItemDesc_New & "'", AgL.GCn).ExecuteScalar) = 0 Then
                    mCode = AgL.GetMaxId("Item", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)
                    mQry = " Insert Into Item (Code, ItemType, V_Type, Specification, Description, 
                        EntryBy, EntryDate, Status, Div_Code)"
                    mQry += " Values(" & AgL.Chk_Text(mCode) & ", 
                        " & AgL.Chk_Text(AgL.XNull(DtFeeStructure.Rows(0)("ItemType"))) & ", 
                        " & AgL.Chk_Text(AgL.XNull(DtFeeStructure.Rows(0)("V_Type"))) & ",
                        " & AgL.Chk_Text(AgL.XNull(DtFeeStructure.Rows(0)("Specification"))) & ", 
                        " & AgL.Chk_Text(bItemDesc_New) & ",
                        'Automatic', " & AgL.Chk_Date(AgL.PubLoginDate) & ",
                        " & AgL.Chk_Text(AgL.XNull(DtFeeStructure.Rows(0)("Status"))) & ", 
                        " & AgL.Chk_Text(AgL.XNull(DtFeeStructure.Rows(0)("Item_Div_Code"))) & "
                        )"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                Else
                    mCode = AgL.XNull(AgL.Dman_Execute(" Select Code From Item Where Description = '" & bItemDesc_New & "'", AgL.GCn).ExecuteScalar)
                End If


                For J As Integer = 0 To DtFeeStructure.Rows.Count - 1
                    mSr += 1
                    mQry = " Insert Into FeeStructure(Code, Sr, Class, Fee, SubHead, Recurrence, Narration, Amount, DueDate, 
                        Comp_Code, Div_Code, Site_Code) "
                    mQry += " Values (" & AgL.Chk_Text(mCode) & ", " & Val(mSr) & ",
                    " & AgL.Chk_Text(AgL.XNull(DtFeeStructure.Rows(J)("Class"))) & ",
                    " & AgL.Chk_Text(AgL.XNull(DtFeeStructure.Rows(J)("Fee"))) & ",
                    " & AgL.Chk_Text(AgL.XNull(DtFeeStructure.Rows(J)("SubHead"))) & ",
                    " & AgL.Chk_Text(AgL.XNull(DtFeeStructure.Rows(J)("Recurrence"))) & ",
                    " & AgL.Chk_Text(AgL.XNull(DtFeeStructure.Rows(J)("Narration"))) & ",
                    " & Val(AgL.VNull(DtFeeStructure.Rows(J)("Amount"))) & ",
                    " & AgL.Chk_Date(DateAdd(DateInterval.Year, 1, CDate(AgL.XNull(DtFeeStructure.Rows(J)("DueDate"))))) & ",
                    " & AgL.Chk_Text(bComp_Code) & ",
                    " & AgL.Chk_Text(AgL.XNull(DtFeeStructure.Rows(J)("Div_Code"))) & ",
                    " & AgL.Chk_Text(AgL.XNull(DtFeeStructure.Rows(J)("Site_Code"))) & "
                    )"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                Next
            End If

            mQry = " Select I.Description, I.Specification, I.V_Type, I.ItemType, 
                I.Status, I.Div_Code As Item_Div_Code,
                PFs.Class, PFs.Fee, PFs.SubHead, PFs.Recurrence, PFs.Narration, PFs.Amount, PFs.DueDate, 
                PFs.Comp_Code, PFs.Div_Code, PFs.Site_Code
                From (Select * From FeeStructureRecurrence Where Comp_Code = '" & bPrev_Comp_Code & "' And Code = '" & AgL.XNull(DtFeeStructureCodes.Rows(I)("Code")) & "' ) As PFs
                LEFT JOIN (Select * From FeeStructureRecurrence Where Comp_Code = '" & bComp_Code & "') As Fs
                        On IfNull(Fs.Class,'') = IfNull(PFs.Class,'') And IfNull(Fs.Fee,'') = IfNull(PFs.Fee,'')
                        And IfNull(Fs.SubHead,'') = IfNull(PFs.SubHead,'') And IfNull(Fs.Div_Code,'') = IfNull(PFs.Div_Code,'') 
                        And IfNull(Fs.Site_Code,'') = IfNull(PFs.Site_Code,'') 
                LEFT JOIN Item I ON PFs.Code = I.Code
                Where PFs.Code Is Not Null And Fs.Code Is Null "
            Dim DtFeeStructureRecurrence As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtFeeStructureRecurrence.Rows.Count > 0 Then
                mSr = 0
                For J As Integer = 0 To DtFeeStructureRecurrence.Rows.Count - 1
                    mSr += 1
                    mQry = " Insert Into FeeStructureRecurrence(Code, Sr, Class, Fee, SubHead, Recurrence, Narration, Amount, DueDate, 
                        Comp_Code, Div_Code, Site_Code) "
                    mQry += " Values (" & AgL.Chk_Text(mCode) & ", " & Val(mSr) & ",
                    " & AgL.Chk_Text(AgL.XNull(DtFeeStructureRecurrence.Rows(J)("Class"))) & ",
                    " & AgL.Chk_Text(AgL.XNull(DtFeeStructureRecurrence.Rows(J)("Fee"))) & ",
                    " & AgL.Chk_Text(AgL.XNull(DtFeeStructureRecurrence.Rows(J)("SubHead"))) & ",
                    " & AgL.Chk_Text(AgL.XNull(DtFeeStructureRecurrence.Rows(J)("Recurrence"))) & ",
                    " & AgL.Chk_Text(AgL.XNull(DtFeeStructureRecurrence.Rows(J)("Narration"))) & ",
                    " & Val(AgL.VNull(DtFeeStructureRecurrence.Rows(J)("Amount"))) & ",
                    " & AgL.Chk_Date(DateAdd(DateInterval.Year, 1, CDate(AgL.XNull(DtFeeStructureRecurrence.Rows(J)("DueDate"))))) & ",
                    " & AgL.Chk_Text(bComp_Code) & ",
                    " & AgL.Chk_Text(AgL.XNull(DtFeeStructureRecurrence.Rows(J)("Div_Code"))) & ",
                    " & AgL.Chk_Text(AgL.XNull(DtFeeStructureRecurrence.Rows(J)("Site_Code"))) & "
                    )"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                Next
            End If
        Next
    End Sub
End Class
