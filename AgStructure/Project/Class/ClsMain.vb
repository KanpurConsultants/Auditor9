Imports System.Data.SQLite
Public Class ClsMain
    Public CFOpen As New ClsFunction
    Public Const ModuleName As String = "AgStructure"

    Sub New(ByVal AgLibVar As AgLibrary.ClsMain)
        AgL = AgLibVar
    End Sub

    Public Enum EntryPointType
        Main
        Log
    End Enum

    Public Shared Function RetDivFilterStr() As String
        Try
            RetDivFilterStr = "IfNull(Div_Code,'" & AgL.PubDivCode & "') = '" & AgL.PubDivCode & "'"
        Catch ex As Exception
            RetDivFilterStr = ""
            MsgBox(ex.Message)
        End Try
    End Function


    Public Class ValueType
        Public Const Percentage As String = "Percentage"
        Public Const Percentage_Changeable As String = "Percentage Changeable"
        Public Const Percentage_From_Column As String = "Percentage From Column"
        Public Const Percentage_Or_Amount As String = "Percentage Or Amount"
        Public Const FixedValue As String = "FixedValue"
        Public Const FixedValue_Changeable As String = "FixedValue Changeable"
        Public Const FixedValue_From_Column As String = "FixedValue From Column"
    End Class


    Public Shared Function FGetStructureFromNCat(ByVal NCat As String, ByVal Conn As Object) As String
        Dim DtTemp As DataTable = Nothing
        Dim Agl As New AgLibrary.ClsMain
        Dim mStructure$ = ""
        Try
            DtTemp = Agl.FillData("Select * From VoucherCat Where NCat = '" & NCat & "'", Conn).Tables(0)
            If DtTemp.Rows.Count > 0 Then
                mStructure = Agl.XNull(DtTemp.Rows(0)("Structure"))
            End If

        Catch ex As Exception
            MsgBox(ex.Message & " In FGetStructureFromNCat Function")
        Finally
            FGetStructureFromNCat = mStructure
            If DtTemp IsNot Nothing Then DtTemp.Dispose()
        End Try
    End Function


    Public Shared Function FUpdateFooterDataFromLineDataStr(ByVal StrStructureCode As String, ByVal StrSearchCode As String, ByVal StrFooterTableName As String, ByVal StrFooterTableSearchField As String, ByVal StrLineTableName As String, ByVal StrLineTableSearchField As String)
        Dim DtTemp As DataTable
        Dim sqlConn As Object
        Dim mQry$, I%
        Dim StrUpdate$ = ""
        Dim StrSelect$ = ""

        sqlConn = New Object
        sqlConn.ConnectionString = AgL.Gcn_ConnectionString
        sqlConn.Open()

        mQry = "Select Value_Type, LineItem, HeaderPerField, HeaderAmtField, LinePerField, LineAmtField From StructureDetail Where Code = '" & StrStructureCode & "'"
        DtTemp = AgL.FillData(mQry, sqlConn).Tables(0)
        For I = 0 To DtTemp.Rows.Count - 1
            If AgL.XNull(DtTemp.Rows(I)("HeaderPerField")) <> "" Then
                StrUpdate += IIf(StrUpdate = "", "", ",") & StrFooterTableName & "." & AgL.XNull(DtTemp.Rows(I)("HeaderPerField")) & " = X." & AgL.XNull(DtTemp.Rows(I)("HeaderPerField"))
                If AgL.VNull(DtTemp.Rows(I)("LineItem")) = "1" Then
                    StrSelect += IIf(StrSelect = "", "", ",") & " 0 " & AgL.XNull(DtTemp.Rows(I)("HeaderPerField"))
                Else
                    StrSelect += IIf(StrSelect = "", "", ",") & " Max(" & AgL.XNull(DtTemp.Rows(I)("LinePerField")) & ") " & AgL.XNull(DtTemp.Rows(I)("HeaderPerField"))
                End If
            End If

            If AgL.XNull(DtTemp.Rows(I)("HeaderAmtField")) <> "" Then
                StrUpdate += IIf(StrUpdate = "", "", ",") & StrFooterTableName & "." & AgL.XNull(DtTemp.Rows(I)("HeaderAmtField")) & " = X." & AgL.XNull(DtTemp.Rows(I)("HeaderAmtField"))
                StrSelect += IIf(StrSelect = "", "", ",") & " Sum(" & AgL.XNull(DtTemp.Rows(I)("LineAmtField")) & ") " & AgL.XNull(DtTemp.Rows(I)("HeaderAmtField"))
            End If
        Next

        StrUpdate = "Update " & StrFooterTableName & " Set " & StrUpdate & " From "
        StrSelect = "(Select " & StrSelect & " From " & StrLineTableName & "  Where " & StrLineTableSearchField & " = '" & StrSearchCode & "') As X Where " & StrFooterTableName & "." & StrFooterTableSearchField & " = '" & StrSearchCode & "'"

        FUpdateFooterDataFromLineDataStr = StrUpdate + StrSelect
    End Function

#Region " Structure Update Code "

    Public Sub UpdateTableStructure(ByRef MdlTable() As AgLibrary.ClsMain.LITable)
        Try
            Call CreateDatabase(MdlTable)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub UpdateTableInitialiser()
        Call DeleteField()
        Call CreateVType()
        Call CreateView()
        FInitialize_PostingGroupSalesTaxParty()
        FInitialize_PostingGroupSalesTaxItem()
    End Sub

    Private Sub FInitialize_PostingGroupSalesTaxParty()
        Dim mQry$
        If AgL.Dman_Execute("Select Count(*) From PostingGroupSalesTaxParty Where Description = 'Local'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "Insert Into PostingGroupSalesTaxParty(Description) Values ('Local')"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From PostingGroupSalesTaxParty Where Description = 'Central'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "Insert Into PostingGroupSalesTaxParty(Description) Values ('Central')"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If

        If AgL.Dman_Execute("Select Count(*) From PostingGroupSalesTaxParty Where Description = 'Central'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "Insert Into PostingGroupSalesTaxParty(Description) Values ('Central-Form C')"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If
    End Sub

    Private Sub FInitialize_PostingGroupSalesTaxItem()
        Dim mQry$
        If AgL.Dman_Execute("Select Count(*) From PostingGroupSalesTaxItem Where Description = 'General'", AgL.GCn).ExecuteScalar = 0 Then
            mQry = "Insert Into PostingGroupSalesTaxItem(Description) Values ('General')"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If
    End Sub


    Sub DeleteField()
        Try
            'If AgL.IsFieldExist("Design", "RUG_DesignImage", AgL.GCn) Then
            '    AgL.Dman_ExecuteNonQry("ALTER TABLE RUG_DesignImage DROP COLUMN Design", AgL.GCn)
            'End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CreateView()
        Dim mQry$ = ""
        '' Note Write Each View in Separate <Try---Catch> Section

        Try
            'mQry = "CREATE VIEW dbo.ViewSch_SessionProgramme AS " & _
            '        " SELECT  SP.*, S.ManualCode AS SessionManualCode, S.Description AS SessionDescription, S.StartDate AS SessionStartDate, S.EndDate AS SessionEndDate, P.Description AS ProgrammeDescription, P.ManualCode AS ProgrammeManualCode, P.ProgrammeDuration, P.Semesters AS ProgrammeSemesters, P.SemesterDuration AS ProgrammeSemesterDuration, P.ProgrammeNature , PN.Description AS ProgrammeNatureDescription  , P.ManualCode  +'/' + S.ManualCode   AS SessionProgramme " & _
            '        " FROM Sch_SessionProgramme SP " & _
            '        " LEFT JOIN Sch_Session S ON sp.Session =S.Code  " & _
            '        " LEFT JOIN Sch_Programme P ON SP.Programme =P.Code " & _
            '        " LEFT JOIN Sch_ProgrammeNature PN ON P.ProgrammeNature =PN.Code "

            'AgL.IsViewExist("ViewSch_SessionProgramme", AgL.GCn, True)
            'AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            'If AgL.PubOfflineApplicable Then
            '    AgL.IsViewExist("ViewSch_SessionProgramme", AgL.GcnSite, True)
            '    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnSite)
            'End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CreateVType()
        Try
            '===================================================< Sale Order V_Type >===================================================
            'AgL.CreateNCat(AgL.GCn, Carpet_ProjLib.ClsMain.NCat_CarpetSaleOrder, Carpet_ProjLib.ClsMain.Cat_CarpetSaleOrder, "Sale Order", AgL.PubSiteCode)
            'AgL.CreateVType(AgL.GCn, Carpet_ProjLib.ClsMain.NCat_CarpetSaleOrder, Carpet_ProjLib.ClsMain.Cat_CarpetSaleOrder, Carpet_ProjLib.ClsMain.NCat_CarpetSaleOrder, "Sale Order", Carpet_ProjLib.ClsMain.NCat_CarpetSaleOrder, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub CreateDatabase(ByRef MdlTable() As AgLibrary.ClsMain.LITable)
        FPostingGroupServiceTax(MdlTable, "PostingGroupServiceTax")

        FPostingGroupExcise(MdlTable, "PostingGroupExcise")
        FPostingGroupExciseItem(MdlTable, "PostingGroupExciseItem")
        FPostingGroupExciseParty(MdlTable, "PostingGroupExciseParty")

        FPostingGroupEntryTax(MdlTable, "PostingGroupEntryTax")
        FPostingGroupEntryTaxItem(MdlTable, "PostingGroupEntryTaxItem")
        FPostingGroupEntryTaxParty(MdlTable, "PostingGroupEntryTaxParty")

        FPostingGroupSalesTax(MdlTable, "PostingGroupSalesTax")
        FPostingGroupSalesTaxItem(MdlTable, "PostingGroupSalesTaxItem")
        FPostingGroupSalesTaxParty(MdlTable, "PostingGroupSalesTaxParty")


        FCharges(MdlTable, "Charges")
        FStructure(MdlTable, "Structure")
        FStructureDetail(MdlTable, "StructureDetail")
        FStructure_AcPosting(MdlTable, "Structure_AcPosting")
        FStructure_TransFooter(MdlTable, "Structure_TransFooter_Log", EntryPointType.Log)
        FStructure_TransFooter(MdlTable, "Structure_TransFooter", EntryPointType.Main)
        FStructure_TransLine(MdlTable, "Structure_TransLine_Log", EntryPointType.Log)
        FStructure_TransLine(MdlTable, "Structure_TransLine", EntryPointType.Main)
        FVoucherCat(MdlTable, "VoucherCat")



        FCustomFieldsHead(MdlTable, "CustomFieldsHead")
        FCustomFields(MdlTable, "CustomFields")
        FCustomFieldDetail(MdlTable, "CustomFieldsDetail")
        FCustomFields_Trans(MdlTable, "CustomFields_Trans_Log", EntryPointType.Log)
        FCustomFields_Trans(MdlTable, "CustomFields_Trans", EntryPointType.Main)
        FAddStructureFields(MdlTable)
    End Sub

    Private Sub FVoucherCat(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Structure", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "HeaderTable", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "LineTable", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "LogHeaderTable", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "LogLineTable", AgLibrary.ClsMain.SQLDataType.Int)


        AgL.FSetFKeyValue(MdlTable, "Structure", "Code", "Structure")
        AgL.FSetFKeyValue(MdlTable, "HeaderTable", "id", "sys.Objects")
        AgL.FSetFKeyValue(MdlTable, "LineTable", "id", "sys.Objects")
        AgL.FSetFKeyValue(MdlTable, "LogHeaderTable", "id", "sys.Objects")
        AgL.FSetFKeyValue(MdlTable, "LogLineTable", "id", "sys.Objects")
    End Sub


    Private Sub FCharges(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ManualCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "FieldName", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "PreparedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "RowID", AgLibrary.ClsMain.SQLDataType.IDENTITY, )
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
    End Sub

    Private Sub FStructure(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "HeaderTable", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "LineTable", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "PreparedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "RowID", AgLibrary.ClsMain.SQLDataType.IDENTITY, )
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
    End Sub

    Private Sub FStructureDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "WEF", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Charges", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "Charge_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
        AgL.FSetColumnValue(MdlTable, "Value_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
        AgL.FSetColumnValue(MdlTable, "Value", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Calculation", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "BaseColumn", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "PostAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PostAcFromColumn", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "DrCr", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "LineItem", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "AffectCost", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "InactiveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Percentage", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "VisibleInMaster", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "VisibleInMasterLine", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "VisibleInTransactionLine", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "VisibleInTransactionFooter", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "HeaderPerField", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "HeaderAmtField", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "LineAmtField", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "LinePerField", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "GridDisplayIndex", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "RowID", AgLibrary.ClsMain.SQLDataType.IDENTITY)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)


        AgL.FSetFKeyValue(MdlTable, "Code", "Code", "Structure")
        AgL.FSetFKeyValue(MdlTable, "Charges", "Code", "Charges")
        AgL.FSetFKeyValue(MdlTable, "PostAc", "SubCode", "SubGroup")

    End Sub

    Private Sub FStructure_AcPosting(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "NCat", AgLibrary.ClsMain.SQLDataType.nVarChar, 5, True)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Structure", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "Charges", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "PostAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ContraSub", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PostAcFromColumn", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "DrCr", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "RowID", AgLibrary.ClsMain.SQLDataType.IDENTITY, )
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetFKeyValue(MdlTable, "Structure", "Code", "Structure")
        AgL.FSetFKeyValue(MdlTable, "Charges", "Code", "Charges")
        'AgL.FSetFKeyValue(MdlTable, "PostAc", "SubCode", "SubGroup")
    End Sub


    Private Sub FStructure_TransFooter(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Charges", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "Charge_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
        AgL.FSetColumnValue(MdlTable, "Value_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
        AgL.FSetColumnValue(MdlTable, "Value", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Calculation", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "BaseColumn", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "PostAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PostAcFromColumn", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "DrCr", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "LineItem", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "AffectCost", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "Active", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "Percentage", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "VisibleInMaster", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "VisibleInMasterLine", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "VisibleInTransactionLine", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "VisibleInTransactionFooter", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "HeaderPerField", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "HeaderAmtField", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "LineAmtField", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "LinePerField", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "GridDisplayIndex", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "RowID", AgLibrary.ClsMain.SQLDataType.IDENTITY, )
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)


        AgL.FSetFKeyValue(MdlTable, "Code", "Code", "Structure")
        AgL.FSetFKeyValue(MdlTable, "Charges", "Code", "Charges")
        'AgL.FSetFKeyValue(MdlTable, "PostAc", "SubCode", "SubGroup")
    End Sub

    Private Sub FStructure_TransLine(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "TSr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Charges", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "Percentage", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "Amount", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "RowID", AgLibrary.ClsMain.SQLDataType.IDENTITY, )
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)


        AgL.FSetFKeyValue(MdlTable, "Code", "Code", "Structure")
        AgL.FSetFKeyValue(MdlTable, "Charges", "Code", "Charges")
    End Sub

    Private Sub FPostingGroupSalesTax(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2, True)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1, True)
        AgL.FSetColumnValue(MdlTable, "PostingGroupSalesTaxItem", AgLibrary.ClsMain.SQLDataType.nVarChar, 20, True)
        AgL.FSetColumnValue(MdlTable, "PostingGroupSalesTaxParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 20, True)
        AgL.FSetColumnValue(MdlTable, "WEF", AgLibrary.ClsMain.SQLDataType.SmallDateTime, , True)
        AgL.FSetColumnValue(MdlTable, "PurchaseAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SalesAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SalesTax", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "SalesTaxOnPurchaseAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SalesTaxOnSalesAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "AdditionalTax", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "AdditionalTaxOnPurchaseAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "AdditionalTaxOnSalesAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        'AgL.FSetColumnValue(MdlTable, "CustomDuty", AgLibrary.ClsMain.SQLDataType.Float)
        'AgL.FSetColumnValue(MdlTable, "CustomDutyAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        'AgL.FSetColumnValue(MdlTable, "CustomDutyECess", AgLibrary.ClsMain.SQLDataType.Float)
        'AgL.FSetColumnValue(MdlTable, "CustomDutyECessAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        'AgL.FSetColumnValue(MdlTable, "CustomDutyHECess", AgLibrary.ClsMain.SQLDataType.Float)
        'AgL.FSetColumnValue(MdlTable, "CustomDutyHECessAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        'AgL.FSetColumnValue(MdlTable, "CustomAdditionalDuty", AgLibrary.ClsMain.SQLDataType.Float)
        'AgL.FSetColumnValue(MdlTable, "CustomAdditionalDutyAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)


        AgL.FSetFKeyValue(MdlTable, "PostingGroupSalesTaxItem", "Description", "PostingGroupSalesTaxItem")
        AgL.FSetFKeyValue(MdlTable, "PostingGroupSalesTaxParty", "Description", "PostingGroupSalesTaxParty")
        AgL.FSetFKeyValue(MdlTable, "PurchaseAc", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "SalesAc", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "SalesTaxOnPurchaseAc", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "SalesTaxOnSalesAc", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "AdditionalTaxOnPurchaseAc", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "AdditionalTaxOnSalesAc", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
        'AgL.FSetFKeyValue(MdlTable, "CustomDutyAc", "SubCode", "SubGroup")
        'AgL.FSetFKeyValue(MdlTable, "CustomDutyECessAc", "SubCode", "SubGroup")
        'AgL.FSetFKeyValue(MdlTable, "CustomDutyHECessAc", "SubCode", "SubGroup")
        'AgL.FSetFKeyValue(MdlTable, "CustomAdditionalDutyAc", "SubCode", "SubGroup")
    End Sub

    Private Sub FPostingGroupSalesTaxItem(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 20, True)
        AgL.FSetColumnValue(MdlTable, "Active", AgLibrary.ClsMain.SQLDataType.Bit, , , , 1)
    End Sub

    Private Sub FPostingGroupSalesTaxParty(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 20, True)
        AgL.FSetColumnValue(MdlTable, "Active", AgLibrary.ClsMain.SQLDataType.Bit, , , , 1)
    End Sub

    Private Sub FPostingGroupServiceTax(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1, True)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2, True)
        AgL.FSetColumnValue(MdlTable, "WEF", AgLibrary.ClsMain.SQLDataType.SmallDateTime, , True)
        AgL.FSetColumnValue(MdlTable, "WEF", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ServiceTaxAssessableAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ServiceTax", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ServiceTaxAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ECess", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ECessAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "HECess", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "HECessAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Rowid", AgLibrary.ClsMain.SQLDataType.IDENTITY)

        AgL.FSetFKeyValue(MdlTable, "ServiceTaxAc", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "ECessAc", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "HECessAc", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")
    End Sub




    Private Sub FPostingGroupExcise(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1, True)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2, True)
        AgL.FSetColumnValue(MdlTable, "PostingGroupExciseItem", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "PostingGroupExciseParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "WEF", AgLibrary.ClsMain.SQLDataType.SmallDateTime, , True)
        AgL.FSetColumnValue(MdlTable, "Excise", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ExciseAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Cess", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "CessAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ECess", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ECessAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "HECess", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "HECessAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Rowid", AgLibrary.ClsMain.SQLDataType.IDENTITY)


        AgL.FSetFKeyValue(MdlTable, "PostingGroupExciseItem", "Description", "PostingGroupExciseItem")
        AgL.FSetFKeyValue(MdlTable, "PostingGroupExciseParty", "Description", "PostingGroupExciseParty")
        AgL.FSetFKeyValue(MdlTable, "ExciseAc", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "CessAc", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "ECessAc", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "HECessAc", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")

    End Sub

    Private Sub FPostingGroupExciseItem(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 20, True)
        AgL.FSetColumnValue(MdlTable, "Active", AgLibrary.ClsMain.SQLDataType.Bit, , , , 1)
    End Sub

    Private Sub FPostingGroupExciseParty(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 20, True)
        AgL.FSetColumnValue(MdlTable, "Active", AgLibrary.ClsMain.SQLDataType.Bit, , , , 1)
    End Sub

    Private Sub FPostingGroupEntryTax(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1, True)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2, True)
        AgL.FSetColumnValue(MdlTable, "PostingGroupEntryTaxItem", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "PostingGroupEntryTaxParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "WEF", AgLibrary.ClsMain.SQLDataType.SmallDateTime, , True)
        AgL.FSetColumnValue(MdlTable, "WEF", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryTax", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "EntryTaxAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Rowid", AgLibrary.ClsMain.SQLDataType.IDENTITY)

        AgL.FSetFKeyValue(MdlTable, "PostingGroupEntryTaxItem", "Description", "PostingGroupEntryTaxItem")
        AgL.FSetFKeyValue(MdlTable, "PostingGroupEntryTaxParty", "Description", "PostingGroupEntryTaxParty")
        AgL.FSetFKeyValue(MdlTable, "EntryTaxAc", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "Div_Code", "Div_Code", "Division")

    End Sub

    Private Sub FPostingGroupEntryTaxItem(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 20, True)
        AgL.FSetColumnValue(MdlTable, "Active", AgLibrary.ClsMain.SQLDataType.Bit, , , , 1)
    End Sub

    Private Sub FPostingGroupEntryTaxParty(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 20, True)
        AgL.FSetColumnValue(MdlTable, "Active", AgLibrary.ClsMain.SQLDataType.Bit, , , , 1)
    End Sub


    Private Sub FCustomFieldsHead(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ManualCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "PreparedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "RowID", AgLibrary.ClsMain.SQLDataType.IDENTITY, )
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
    End Sub

    Private Sub FCustomFields(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "HeaderTable", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "PreparedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "RowID", AgLibrary.ClsMain.SQLDataType.IDENTITY, )
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
    End Sub

    Private Sub FCustomFieldDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Heads", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "Value_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
        AgL.FSetColumnValue(MdlTable, "FLength", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, , , "0")
        AgL.FSetColumnValue(MdlTable, "Value", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "Default_Value", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "Active", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "IsMandatory", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "RowID", AgLibrary.ClsMain.SQLDataType.IDENTITY)


        AgL.FSetFKeyValue(MdlTable, "Code", "Code", "CustomFields")
        AgL.FSetFKeyValue(MdlTable, "Heads", "Code", "CustomFildsHead")
    End Sub

    Private Sub FCustomFields_Trans(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "CustomFields", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "TSr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Head", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "Value", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "RowID", AgLibrary.ClsMain.SQLDataType.IDENTITY, )
        AgL.FSetFKeyValue(MdlTable, "Head", "Code", "CustomFieldsHead")
    End Sub


    Private Sub FAddStructureFields(ByRef MdlTable() As AgLibrary.ClsMain.LITable)
        Try
            Dim DtVType As DataTable
            Dim DtStructureLine As DataTable
            Dim mQry$
            Dim I As Integer
            Dim J As Integer
            mQry = "Select Structure, HeaderTable, LineTable, Ht.Name as HeaderTableName, LHT.Name as LogHeaderTableName, Lt.Name as LineTableName, LLT.Name as LogLineTableName " &
                   "From VoucherCat " &
                   "Left Join Sys.Objects HT On VoucherCat.HeaderTable = HT.Object_ID " &
                   "Left Join Sys.Objects LHT On VoucherCat.LogHeaderTable = LHT.Object_ID " &
                   "Left Join Sys.Objects LT On VoucherCat.LineTable = Lt.Object_ID " &
                   "Left Join Sys.Objects LLT On VoucherCat.LogLineTable = LLT.Object_ID " &
                   "Where Structure Is Not Null"
            DtVType = AgL.FillData(mQry, AgL.GCn).Tables(0)
            For I = 0 To DtVType.Rows.Count - 1
                mQry = "Select HeaderPerField, HeaderAmtField From StructureDetail  Where Code = '" & DtVType.Rows(I)("Structure") & "' And HeaderAmtField Is Not Null  "
                DtStructureLine = AgL.FillData(mQry, AgL.GCn).Tables(0)

                If DtStructureLine.Rows.Count > 0 Then
                    '===========ADD FIELDS IN HEADER TABLE========================
                    If AgL.XNull(DtVType.Rows(I)("HeaderTableName")) <> "" Then
                        AgL.FAddTable(MdlTable, DtVType.Rows(I)("HeaderTableName"), ModuleName)
                        For J = 0 To DtStructureLine.Rows.Count - 1
                            If AgL.XNull(DtStructureLine.Rows(J)("HeaderPerField")) <> "" Then
                                AgL.FSetColumnValue(MdlTable, AgL.XNull(DtStructureLine.Rows(J)("HeaderPerField")), AgLibrary.ClsMain.SQLDataType.Float)
                            End If

                            If AgL.XNull(DtStructureLine.Rows(J)("HeaderAmtField")) <> "" Then
                                AgL.FSetColumnValue(MdlTable, AgL.XNull(DtStructureLine.Rows(J)("HeaderAmtField")), AgLibrary.ClsMain.SQLDataType.Float)
                            End If
                        Next

                    End If
                    '===========================================================



                    '===========ADD FIELDS IN LOG HEADER TABLE========================
                    If AgL.XNull(DtVType.Rows(I)("LogHeaderTableName")) <> "" Then
                        AgL.FAddTable(MdlTable, DtVType.Rows(I)("LogHeaderTableName"), ModuleName)
                        For J = 0 To DtStructureLine.Rows.Count - 1
                            If AgL.XNull(DtStructureLine.Rows(J)("HeaderPerField")) <> "" Then
                                AgL.FSetColumnValue(MdlTable, AgL.XNull(DtStructureLine.Rows(J)("HeaderPerField")), AgLibrary.ClsMain.SQLDataType.Float)
                            End If

                            If AgL.XNull(DtStructureLine.Rows(J)("HeaderAmtField")) <> "" Then
                                AgL.FSetColumnValue(MdlTable, AgL.XNull(DtStructureLine.Rows(J)("HeaderAmtField")), AgLibrary.ClsMain.SQLDataType.Float)
                            End If
                        Next
                    End If
                    '=================================================================
                End If




                mQry = "Select LinePerField, LineAmtField From StructureDetail  Where Code = '" & DtVType.Rows(I)("Structure") & "' And LineAmtField Is Not Null  "
                DtStructureLine = AgL.FillData(mQry, AgL.GCn).Tables(0)

                If DtStructureLine.Rows.Count > 0 Then
                    '===========ADD FIELDS IN LINE TABLE========================
                    If AgL.XNull(DtVType.Rows(I)("LineTableName")) <> "" Then
                        AgL.FAddTable(MdlTable, DtVType.Rows(I)("LineTableName"), ModuleName)
                        For J = 0 To DtStructureLine.Rows.Count - 1
                            If AgL.XNull(DtStructureLine.Rows(J)("LinePerField")) <> "" Then
                                AgL.FSetColumnValue(MdlTable, AgL.XNull(DtStructureLine.Rows(J)("LinePerField")), AgLibrary.ClsMain.SQLDataType.Float)
                            End If

                            If AgL.XNull(DtStructureLine.Rows(J)("LineAmtField")) <> "" Then
                                AgL.FSetColumnValue(MdlTable, AgL.XNull(DtStructureLine.Rows(J)("LineAmtField")), AgLibrary.ClsMain.SQLDataType.Float)
                            End If
                        Next
                    End If
                    '===========================================================




                    '===========ADD FIELDS IN LOG LINE TABLE========================
                    If AgL.XNull(DtVType.Rows(I)("LogLineTableName")) <> "" Then
                        AgL.FAddTable(MdlTable, DtVType.Rows(I)("LogLineTableName"), ModuleName)
                        For J = 0 To DtStructureLine.Rows.Count - 1
                            If AgL.XNull(DtStructureLine.Rows(J)("LinePerField")) <> "" Then
                                AgL.FSetColumnValue(MdlTable, AgL.XNull(DtStructureLine.Rows(J)("LinePerField")), AgLibrary.ClsMain.SQLDataType.Float)
                            End If

                            If AgL.XNull(DtStructureLine.Rows(J)("LineAmtField")) <> "" Then
                                AgL.FSetColumnValue(MdlTable, AgL.XNull(DtStructureLine.Rows(J)("LineAmtField")), AgLibrary.ClsMain.SQLDataType.Float)
                            End If
                        Next
                    End If
                    '===========================================================

                End If
            Next
        Catch ex As Exception

        End Try
    End Sub


#End Region



End Class