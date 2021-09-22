Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SqlClient

Public Class ClsPrinting
    Dim AgL As ClsMain


    Public Sub Formula_Set(ByVal mCRD As ReportDocument, Optional ByVal mRepTitle As String = "", Optional ByVal Date1 As String = "", Optional ByVal Date2 As String = "")
        Dim i As Integer
        For i = 0 To mCRD.DataDefinition.FormulaFields.Count - 1
            Select Case AgL.UTrim(mCRD.DataDefinition.FormulaFields(i).Name)
                Case AgL.UTrim("Title")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & mRepTitle & "'"
                Case AgL.UTrim("comp_name")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.PubCompName & "'"
                Case AgL.UTrim("comp_add")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.PubCompAdd1 & "'"
                Case AgL.UTrim("RegOffice_FullAddress")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.PubCompAdd1 & "'"
                Case AgL.UTrim("RegOffice_City")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.PubCompAdd2 & "'"
                Case AgL.UTrim("comp_add1")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.PubCompAdd2 & "'"
                Case AgL.UTrim("comp_Pin")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.PubCompPinCode & "'"
                Case AgL.UTrim("comp_phone")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.PubCompPhone & "'"
                Case AgL.UTrim("comp_city")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.PubCompCity & "'"
                Case AgL.UTrim("Title")
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & mRepTitle & "'"
                Case AgL.UTrim("Division")
                    If AgL.PubDivName IsNot Nothing Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & AgL.PubDivName.ToUpper & " DIVISION" & "'"
                    End If
                Case AgL.UTrim("Tin_No")                    
                    mCRD.DataDefinition.FormulaFields(i).Text = "'" & "TIN NO : " & AgL.PubCompTIN & "'"
                Case AgL.UTrim("DateBetween")
                    If Date1 <> "" And Date2 <> "" Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "'" & "From Date " & Date1 & " To " & Date2 & " '"
                    ElseIf Date1 <> "" And Date2 = "" Then
                        mCRD.DataDefinition.FormulaFields(i).Text = "' " & "For Date : " & Date1 & " '"
                    End If

            End Select
        Next
    End Sub

    Public Sub New(ByVal Agl_Obj As AgLibrary.ClsMain)
        AgL = Agl_Obj
    End Sub

    Public Sub Generate_Report(ByVal mQry As String, ByRef mConn As SqlConnection, ByRef mCRD As ReportDocument, _
                                ByRef mRepView As RepView, ByVal mReportPath As String, ByVal mRepName As String, ByVal mRepTitle As String, _
                                ByVal mForm As Form, ByVal MDIForm As Object, Optional ByVal ShowRep As Boolean = True)
        mForm.Cursor = Cursors.WaitCursor
        Dim DsRep As DataSet

        Try
            DsRep = AgL.FillData(mQry, mConn)
            DsRep.WriteXmlSchema(mReportPath & "\" & mRepName & ".xsd")

            mCRD.Load(mReportPath & "\" & mRepName & ".rpt")

            mCRD.SetDataSource(DsRep.Tables(DsRep.Tables(0).TableName))


            mRepView.CrvReport.ReportSource = mCRD

            Formula_Set(mCRD, mRepTitle)

            If ShowRep = True Then Show_Report(mRepView, mRepTitle, MDIForm)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            mForm.Cursor = Cursors.Default
        End Try
    End Sub

    Public Sub Show_Report(ByVal mRepView As RepView, ByVal mRepTitle As String, ByVal MDIForm As Object)
        With mRepView
            .Text = mRepTitle
            If MDIForm IsNot Nothing Then
                .Icon = MDIForm.Icon
                .MdiParent = MDIForm.ActiveForm
            End If
            .Show()
            .Left = 0
            .Top = 0

            If .CrvReport.ReportSource IsNot Nothing Then
                .RepObj = .CrvReport.ReportSource
            End If
        End With
    End Sub

    Public Sub CreateFieldDefFile1(ByVal resultSet As DataSet, ByVal Path As System.String, ByVal sampleData As Boolean)
        If resultSet Is Nothing Then Exit Sub
        If resultSet.Tables(0).Rows.Count = 0 Then Exit Sub

        Dim NUMBER_OF_COLUMNS As Integer = resultSet.Tables(0).Columns.Count
        System.IO.File.Delete(path)
        Dim out_Renamed As System.IO.StreamWriter = New System.IO.StreamWriter(path, False, System.Text.Encoding.Default)
        Dim i As Integer = 0, crystalType As String = "", sampleDataForColumn As String = ""
        out_Renamed.Write("; Field definition file for table: ADORecordset" + vbCrLf)
        '     Iterate through each column in the meta data.
        With resultSet.Tables(0)
            For i = 1 To NUMBER_OF_COLUMNS
                out_Renamed.Write(System.Convert.ToString(.Columns(i - 1)) + vbTab)
                crystalType = getCrystalType(cDataTypeADODB(.Columns(i - 1).DataType.UnderlyingSystemType.ToString), IIf(cDataTypeADODB(.Columns(i - 1).DataType.UnderlyingSystemType.ToString) = ADODB.DataTypeEnum.adCurrency, True, False))
                out_Renamed.Write(crystalType + vbTab)
                If crystalType = "string" Then
                    'out_Renamed.Write(CStr(resultSet.Tables(0).Columns(i - 1).MaxLength) + vbTab)
                    out_Renamed.Write(CStr(255) + vbTab)
                End If
                '     Generate sample data if desired.
                If sampleData = True Then
                    sampleDataForColumn = getSampleData1(i, resultSet)
                    If Not IsDBNull(sampleDataForColumn) Then
                        If sampleDataForColumn <> "" Then
                            out_Renamed.Write(sampleDataForColumn + vbTab)
                        Else
                            out_Renamed.Write(vbTab)
                        End If
                    Else
                        out_Renamed.Write(vbTab)
                    End If
                End If
                out_Renamed.Write(vbCrLf)
            Next
            out_Renamed.Close()
        End With
        Exit Sub
    End Sub

    Private Function getCrystalType(ByVal sqlType As Integer, ByVal isCurrency As Boolean) As String
        Dim crType As String = "UNKNOWN"
        If isCurrency = True Then
            crType = "Currency"
        Else
            Select Case sqlType
                Case ADODB.DataTypeEnum.adBinary
                    crType = "BLOB"
                Case ADODB.DataTypeEnum.adVarBinary
                    crType = "BLOB"
                Case ADODB.DataTypeEnum.adLongVarBinary
                    crType = "BLOB"
                Case ADODB.DataTypeEnum.adBoolean
                    crType = "boolean"
                Case ADODB.DataTypeEnum.adTinyInt
                    crType = "byte"
                Case ADODB.DataTypeEnum.adDBTimeStamp
                    crType = "datetime"
                Case ADODB.DataTypeEnum.adDBTime
                    crType = "time"
                Case ADODB.DataTypeEnum.adDate
                    crType = "datetime"
                Case ADODB.DataTypeEnum.adLongVarChar
                    crType = "memo"
                Case ADODB.DataTypeEnum.adVarChar
                    crType = "string"
                Case ADODB.DataTypeEnum.adInteger
                    crType = "number"
                Case ADODB.DataTypeEnum.adSingle
                    crType = "number"
                Case ADODB.DataTypeEnum.adDouble
                    crType = "number"
                Case ADODB.DataTypeEnum.adNumeric
                    crType = "number"
                Case ADODB.DataTypeEnum.adBigInt
                    crType = "long"
                Case ADODB.DataTypeEnum.adSmallInt
                    crType = "short"
                Case ADODB.DataTypeEnum.adVarWChar
                    crType = "string"
                Case ADODB.DataTypeEnum.adWChar
                    crType = "string"
            End Select
        End If
        getCrystalType = crType
    End Function

    Public Function cDataTypeADODB(ByVal columnType As String) As ADODB.DataTypeEnum
        Select Case columnType
            Case "System.Boolean"
                Return ADODB.DataTypeEnum.adBoolean
            Case "System.Byte"
                Return ADODB.DataTypeEnum.adUnsignedTinyInt
            Case "System.Char"
                Return ADODB.DataTypeEnum.adChar
            Case "System.DateTime"
                Return ADODB.DataTypeEnum.adDate
            Case "System.Decimal"
                Return ADODB.DataTypeEnum.adCurrency
            Case "System.Double"
                Return ADODB.DataTypeEnum.adDouble
            Case "System.Int16"
                Return ADODB.DataTypeEnum.adSmallInt
            Case "System.Int32"
                Return ADODB.DataTypeEnum.adInteger
            Case "System.Int64"
                Return ADODB.DataTypeEnum.adBigInt
            Case "System.SByte"
                Return ADODB.DataTypeEnum.adTinyInt
            Case "System.Single"
                Return ADODB.DataTypeEnum.adSingle
            Case "System.UInt16"
                Return ADODB.DataTypeEnum.adUnsignedSmallInt
            Case "System.UInt32"
                Return ADODB.DataTypeEnum.adUnsignedInt
            Case "System.UInt64"
                Return ADODB.DataTypeEnum.adUnsignedBigInt
            Case "System.String"
                Return ADODB.DataTypeEnum.adVarChar
            Case "System.Byte[]"
                Return ADODB.DataTypeEnum.adLongVarBinary
            Case Else
                Return ADODB.DataTypeEnum.adVarChar
        End Select
    End Function

    Public Function cDataTypeADODB1(ByVal columnType As String) As ADODB.DataTypeEnum
        Select Case columnType
            Case "System.Boolean"
                Return ADODB.DataTypeEnum.adBoolean
            Case "System.Byte"
                Return ADODB.DataTypeEnum.adUnsignedTinyInt
            Case "System.Char"
                Return ADODB.DataTypeEnum.adChar
            Case "System.DateTime"
                Return ADODB.DataTypeEnum.adDate
            Case "System.Decimal"
                Return ADODB.DataTypeEnum.adCurrency
            Case "System.Double"
                Return ADODB.DataTypeEnum.adDouble
            Case "System.Int16"
                Return ADODB.DataTypeEnum.adSmallInt
            Case "System.Int32"
                Return ADODB.DataTypeEnum.adInteger
            Case "System.Int64"
                Return ADODB.DataTypeEnum.adBigInt
            Case "System.SByte"
                Return ADODB.DataTypeEnum.adTinyInt
            Case "System.Single"
                Return ADODB.DataTypeEnum.adSingle
            Case "System.UInt16"
                Return ADODB.DataTypeEnum.adUnsignedSmallInt
            Case "System.UInt32"
                Return ADODB.DataTypeEnum.adUnsignedInt
            Case "System.UInt64"
                Return ADODB.DataTypeEnum.adUnsignedBigInt
            Case "System.String"
                Return ADODB.DataTypeEnum.adVarChar
            Case Else
                Return ADODB.DataTypeEnum.adVarChar
        End Select
    End Function
    Private Function getSampleData1(ByVal column As Integer, ByVal resultSet As DataSet) As String
        With resultSet.Tables(0)
            Dim crystalType As String = getCrystalType(cDataTypeADODB(.Columns(column - 1).DataType.UnderlyingSystemType.ToString), IIf(cDataTypeADODB(.Columns(column - 1).DataType.UnderlyingSystemType.ToString) = ADODB.DataTypeEnum.adCurrency, True, False))
            Dim sampleData As String = ""
            If crystalType = "BLOB" Then
                getSampleData1 = ""
            Else
                sampleData = ""
                If .Rows.Count > 0 Then
                    sampleData = System.Convert.ToString(.Rows(0)(column - 1))
                End If
                getSampleData1 = sampleData
            End If
        End With
        Exit Function
    End Function


    Public Sub ReportCommonInformation(ByVal Agl As AgLibrary.ClsMain, ByVal mCrd As ReportDocument, ByVal ReportPath As String)
        Dim DsTemp As New DataSet
        Dim mQry As String

        Try
            mQry = "Select '" & Agl.PubCompName & "' as CompanyName, '" & Agl.PubCompAdd1 & "' as Add1, '" & Agl.PubCompAdd2 & "' as Add2, '" & Agl.PubCompAdd3 & "' as Add3, '" & Agl.PubCompCity & "' as City, '" & Agl.PubCompPhone & "' as Phone, '" & Agl.PubCompFax & "' as Fax, '" & Agl.PubCompPinCode & "' as Pin, '" & Agl.PubCompEMail & "' as Email, '" & Agl.PubCompCST & "' as CSTNo, '" & Agl.PubCompTIN & "' as TIN "
            DsTemp = Agl.FillData(mQry, Agl.GCn)

            CreateFieldDefFile1(DsTemp, ReportPath & "\CompanyInformation.ttx", True)
            mCrd.OpenSubreport("CompanyInformation.rpt").Database.Tables(0).SetDataSource(DsTemp.Tables(0))


            mQry = "Select '" & Agl.PubUserName & "' as UserName "
            DsTemp = Agl.FillData(mQry, Agl.GCn)
            CreateFieldDefFile1(DsTemp, ReportPath & "\ReportFooter.ttx", True)
            mCrd.OpenSubreport("ReportFooter.rpt").Database.Tables(0).SetDataSource(DsTemp.Tables(0))


        Catch ex As Exception
            'ClsErrHandler.HandleException(ex, " In AddCompanyInformation Method of AgLibrary.ClsPrinting")
        Finally
            DsTemp.Dispose()
        End Try
    End Sub

    Public Function GetRepNameCustomize(ByVal RepName As String, ByVal ReportPath As String) As String
        Dim bRepNameCustomize$ = "", bStrCompName$ = "", bStrCompCity$ = ""
        Try
            ''''''''''IF CUSTOMER NEED SOME CHANGE IN FORMAT OF A REPORT'''''''''''
            ''''''''''CUTOMIZE REPORT CAN BE CREATED WITHOUT CHANGE IN CODE''''''''
            ''''''''''WITH ADDING 6 CHAR OF COMPANY NAME AND 4 CHAR OF CITY NAME'''
            ''''''''''WITHOUT SPACES IN EXISTING REPORT NAME''''''''''''''''''''''''''''''''''''''
            If AgL.PubRegOfficeName Is Nothing Then AgL.PubRegOfficeName = ""
            If AgL.PubRegOfficeCity Is Nothing Then AgL.PubRegOfficeCity = ""

            If AgL.PubRegOfficeName.Trim <> "" Then bStrCompName = AgL.PubRegOfficeName Else bStrCompName = AgL.PubCompName
            If AgL.PubRegOfficeCity.Trim <> "" Then bStrCompCity = AgL.PubRegOfficeCity Else bStrCompCity = AgL.PubCompCity

            'bRepNameCustomize = RepName & AgL.MidStr(Replace(bStrCompName, " ", ""), 0, 6) & AgL.MidStr(Replace(bStrCompCity, " ", ""), 0, 4)
            bRepNameCustomize = RepName & "_" & AgL.PubCompShortName
            If System.IO.File.Exists(ReportPath & "\" & bRepNameCustomize & ".rpt") Then
                RepName = bRepNameCustomize
            Else
                bRepNameCustomize = RepName
            End If
            '''''''''''''''''''''''''''''''''''''''''''''''''''''

        Catch ex As Exception
            bRepNameCustomize = RepName
        Finally
            GetRepNameCustomize = bRepNameCustomize
        End Try
    End Function


End Class
