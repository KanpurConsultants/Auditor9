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
        FVoucher_Type(MdlTable, "Voucher_Type")

        FCustomFieldsHead(MdlTable, "CustomFieldsHead", EntryPointType.Main)

        FCustomFields(MdlTable, "CustomFields", EntryPointType.Main)

        FCustomFieldsDetail(MdlTable, "CustomFieldsDetail", EntryPointType.Main)

        FCustomFields_Trans(MdlTable, "CustomFields_Trans_Log", EntryPointType.Log)
        FCustomFields_Trans(MdlTable, "CustomFields_Trans", EntryPointType.Main)

        FAddStructureFields(MdlTable)
    End Sub

    Private Sub FVoucher_Type(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "CustomFields", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetFKeyValue(MdlTable, "CustomFields", "Code", "CustomFields")
    End Sub

    Private Sub FCustomFieldsHead(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ManualCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "PreparedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
    End Sub

    Private Sub FCustomFields(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
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
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "TableName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "PrimaryField", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
    End Sub

    Private Sub FCustomFieldsDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "Head", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Value_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
        AgL.FSetColumnValue(MdlTable, "FLength", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Value", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "Default_Value", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "Active", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "IsMandatory", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "TableName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "PrimaryField", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "UpdateField", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "UpdateFieldType", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "HeaderField", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "HeaderField", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "HeaderFieldDataType", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "HeaderFieldLength", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetFKeyValue(MdlTable, "Code", "Code", "CustomFields")
        AgL.FSetFKeyValue(MdlTable, "Heads", "Code", "CustomFildsHead")
    End Sub

    Private Sub FCustomFields_Trans(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "CustomFields", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Head", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "Value", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "MnuText", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Data", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "Value_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
        AgL.FSetColumnValue(MdlTable, "FLength", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TableName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "PrimaryField", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "HeaderField", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "UpdateField", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "UpdateFieldType", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)

        AgL.FSetFKeyValue(MdlTable, "Head", "Code", "CustomFieldsHead")
    End Sub

    'Private Sub FCustomFieldDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
    '    AgL.FAddTable(MdlTable, StrTableName, ModuleName)

    '    AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
    '    AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
    '    AgL.FSetColumnValue(MdlTable, "Heads", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
    '    AgL.FSetColumnValue(MdlTable, "Value_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
    '    AgL.FSetColumnValue(MdlTable, "FLength", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, , , "0")
    '    AgL.FSetColumnValue(MdlTable, "Value", AgLibrary.ClsMain.SQLDataType.VarCharMax)
    '    AgL.FSetColumnValue(MdlTable, "Default_Value", AgLibrary.ClsMain.SQLDataType.VarCharMax)
    '    AgL.FSetColumnValue(MdlTable, "Active", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
    '    AgL.FSetColumnValue(MdlTable, "RowID", AgLibrary.ClsMain.SQLDataType.IDENTITY)


    '    AgL.FSetFKeyValue(MdlTable, "Code", "Code", "CustomFields")
    '    AgL.FSetFKeyValue(MdlTable, "Heads", "Code", "CustomFildsHead")
    'End Sub

    'Private Sub FCustomFields_Trans(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
    '    AgL.FAddTable(MdlTable, StrTableName, ModuleName)

    '    AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
    '    AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
    '    AgL.FSetColumnValue(MdlTable, "CustomFields", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
    '    AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
    '    AgL.FSetColumnValue(MdlTable, "TSr", AgLibrary.ClsMain.SQLDataType.Int, , True)
    '    AgL.FSetColumnValue(MdlTable, "Head", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
    '    AgL.FSetColumnValue(MdlTable, "Value", AgLibrary.ClsMain.SQLDataType.VarCharMax)
    '    AgL.FSetColumnValue(MdlTable, "RowID", AgLibrary.ClsMain.SQLDataType.IDENTITY, )


    '    AgL.FSetFKeyValue(MdlTable, "Head", "Code", "CustomFieldsHead")
    'End Sub

#End Region

    Public Shared Function FGetCustomFieldFromV_Type(ByVal V_Type As String, ByVal Conn As Object) As String
        Dim DtTemp As DataTable = Nothing
        Dim Agl As New AgLibrary.ClsMain
        Dim mCustomField$ = ""
        Try
            DtTemp = Agl.FillData("Select * From Voucher_Type Where V_Type = '" & V_Type & "'", Conn).Tables(0)
            If DtTemp.Rows.Count > 0 Then
                mCustomField = Agl.XNull(DtTemp.Rows(0)("CustomFields"))
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " In FGetCustomFieldFromV_Type Function")
        Finally
            FGetCustomFieldFromV_Type = mCustomField
            If DtTemp IsNot Nothing Then DtTemp.Dispose()
        End Try
    End Function

    Private Sub FAddStructureFields(ByRef MdlTable() As AgLibrary.ClsMain.LITable)
        Try
            Dim DtVType As DataTable
            Dim DtCustomFields As DataTable
            Dim mQry$
            Dim I As Integer
            Dim J As Integer
            mQry = "Select CustomFields, HeaderTable, Ht.Name as HeaderTableName, LHT.Name as LogHeaderTableName  " &
                   " From Voucher_Type " &
                   " Left Join Sys.Objects HT On Voucher_Type.HeaderTable = HT.Object_ID " &
                   " Left Join Sys.Objects LHT On Voucher_Type.LogHeaderTable = LHT.Object_ID " &
                   " Where CustomFields Is Not Null "
            DtVType = AgL.FillData(mQry, AgL.GCn).Tables(0)


            For I = 0 To DtVType.Rows.Count - 1
                mQry = "Select HeaderField, HeaderFieldDataType, HeaderFieldLength From CustomFieldsDetail  Where Code = '" & DtVType.Rows(I)("CustomFields") & "' And Head Is Not Null  "
                DtCustomFields = AgL.FillData(mQry, AgL.GCn).Tables(0)

                If DtCustomFields.Rows.Count > 0 Then
                    '===========ADD FIELDS IN HEADER TABLE========================
                    If AgL.XNull(DtVType.Rows(I)("HeaderTableName")) <> "" Then
                        AgL.FAddTable(MdlTable, DtVType.Rows(I)("HeaderTableName"), ModuleName)
                        For J = 0 To DtCustomFields.Rows.Count - 1
                            If AgL.XNull(DtCustomFields.Rows(J)("HeaderField")) <> "" Then
                                AgL.FSetColumnValue(MdlTable, AgL.XNull(DtCustomFields.Rows(J)("HeaderField")), AgL.VNull(DtCustomFields.Rows(J)("HeaderFieldDataType")), AgL.VNull(DtCustomFields.Rows(J)("HeaderFieldLength")))
                            End If
                        Next
                    End If
                    '===========================================================

                    '===========ADD FIELDS IN LOG HEADER TABLE========================
                    If AgL.XNull(DtVType.Rows(I)("LogHeaderTableName")) <> "" Then
                        AgL.FAddTable(MdlTable, DtVType.Rows(I)("LogHeaderTableName"), ModuleName)
                        For J = 0 To DtCustomFields.Rows.Count - 1
                            If AgL.XNull(DtCustomFields.Rows(J)("HeaderField")) <> "" Then
                                AgL.FSetColumnValue(MdlTable, AgL.XNull(DtCustomFields.Rows(J)("HeaderField")), AgL.VNull(DtCustomFields.Rows(J)("HeaderFieldDataType")), AgL.VNull(DtCustomFields.Rows(J)("HeaderFieldLength")))
                            End If
                        Next
                    End If
                    '=================================================================
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub
End Class