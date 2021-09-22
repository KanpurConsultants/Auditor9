Imports System.Text
Imports System.Data.SQLite

Public Class ClsMain
    Public CFOpen As New ClsFunction
    Public ModuleTable() As AgLibrary.ClsMain.LITable


#Region "Copied From LoginInfo"

    Public Enum EAlignment
        EA_Right = 0
        EA_Middle = 1
        EA_Left = 2
    End Enum
    Public Enum RecIdFormat
        DD_MM_YY = 0
        DD_MM = 1
        MM = 2
    End Enum

    Sub New(ByVal LIVar As AgLibrary.ClsMain)
        AgL = LIVar

    End Sub


    Public Structure LIException
        Dim StrValue1 As String
        Dim StrValue2 As String
        Dim StrValue3 As String
        Dim StrValue4 As String
        Dim StrValue5 As String
        Dim StrMessage As String
    End Structure
    Public Enum SQLDataType
        VarCharMax = 0
        VarChar = 1
        SmallInt = 2
        Int = 3
        TinyInt = 4
        SmallDateTime = 5
        Float = 6
    End Enum
    Public Structure LITable
        Dim StrModuleName As String
        Dim StrName As String
        Dim FKey() As LIForeignKey
        Dim ColItem() As LIColumn
    End Structure
    Public Structure LIColumn
        Dim StrName As String
        Dim StrDataType As String
        Dim IntLength As String
        Dim BlnPrimaryKey As Boolean
        Dim BlnAllowNull As Boolean
        Dim StrDefaultValue As String
    End Structure
    Public Structure LIForeignKey
        Dim StrOnColumn As String
        Dim StrWithColumn As String
        Dim StrWithTable As String
    End Structure
    Public Function FGetAllSiteList()
        Dim DTTemp As DataTable
        Dim I As Integer
        Dim StrSiteList As String

        StrSiteList = ""

        DTTemp = FGetDatTable("Select Code From SiteMast ", AgL.GCn)
        For I = 0 To DTTemp.Rows.Count - 1
            If StrSiteList <> "" Then StrSiteList += ","
            StrSiteList += "|" & AgL.XNull(DTTemp.Rows(I).Item("Code")) & "|"
        Next

        FGetAllSiteList = StrSiteList
    End Function

    Public Function FChkDate_FinancialYear(ByVal StrVDate As String) As Boolean
        Dim BlnRtn As Boolean = True

        If Not (CDate(AgL.PubStartDate) <= CDate(StrVDate) And CDate(AgL.PubEndDate) >= CDate(StrVDate)) Then
            BlnRtn = False
            MsgBox(ClsMain.Msg_2)
        End If

        FChkDate_FinancialYear = BlnRtn
    End Function

    Public Function FSendText(ByVal Txt As Object, ByVal Chr As Char)
        Dim StrSendText As String
        If Txt.Text = "" Then
            StrSendText = Chr
        Else
            StrSendText = Txt.Text
        End If
        Return StrSendText
    End Function
    Public DRFound As DataRow
    Public PubSiteList As String
    Public PubSiteListCharIndex As String

    Public ClrPubBackColorForm As Color = Color.FromArgb(224, 224, 224)
    Public Const MsgDeleteChk As String = "Record Is In Use. You Cannot Delete This Record."
    Public Const MsgEditChk As String = "Record Is In Use. You Cannot Edit This Record."
    Public Const MsgRecNotFnd As String = "No Record Found."
    Public Const MsgDeleteCnf As String = "Are You Sure. You Want To Delete This Record?"
    Public Const MsgSaveCnf As String = "Are You Sure. You Want To Save This Record?"
    Public Const MsgInvalid As String = "Invalid"
    Public Const MsgDuplicate As String = "Duplicate"
    Public Const MsgPlsFill As String = "Please Fill"
    Public Const MsgSave As String = "Document Save Successfully."
    Public Const MsgSystemDefine As String = "Record Is System Define. You Cannot Edit/ Delete This Record."
    Public Const MsgTDSApplicable As String = "TDS Is Applicable For This Account. Do You Want to Apply It?"

    Public Const Msg_0 As String = "Access Denied.Please Check User Name/ Password."
    Public Const Msg_1 As String = "Prefix Not Defined For Company."
    Public Const Msg_2 As String = "You Can Do Entry In Current Financial Year Only."
    Public Const Msg_3 As String = "Total Adjustment Is Not Matching With Amount To Be Adjusted."
    Public Const Msg_4 As String = "(Adjusted+Adjustment) Should Not Be Greater Than Bill Amount."
    Public Const Msg_5 As String = "Record Has Already Been Posted."
    Public Const Msg_6 As String = "Debit Amount Should Be Equal To Credit Amount."
    Public Const Msg_7 As String = "Confirm Password Not Matched."
    Public Const Msg_8 As String = "Save In Fields Must Not Be Repeatable."
    Public Const Msg_9 As String = "Are You Sure. You Want To Copy User?"
    Public Const Msg_10 As String = "User Copied Successfully."


    Public Function FGetType(ByVal SQLDT As SQLDataType) As String
        Dim StrRtn As String

        Select Case SQLDT
            Case SQLDataType.Float
                StrRtn = "Float"
            Case SQLDataType.Int
                StrRtn = "Int"
            Case SQLDataType.VarChar
                StrRtn = "VarChar"
            Case SQLDataType.VarCharMax
                StrRtn = "VarChar(MAX)"
            Case SQLDataType.SmallDateTime
                StrRtn = "SmallDateTime"
            Case SQLDataType.TinyInt
                StrRtn = "TinyInt"
            Case SQLDataType.SmallInt
                StrRtn = "SmallInt"
            Case Else
                StrRtn = ""
        End Select
        Return StrRtn
    End Function


    Public Sub FormulaSet(ByVal Rpt As Object, ByVal StrReportTitle As String, Optional ByVal FGrid As AgControls.AgDataGrid = Nothing, _
    Optional ByVal GFieldName As Byte = 0, Optional ByVal GFilter As Byte = 1, Optional ByVal GDisplayOnReport As Byte = 6)
        Dim I As Int16, J As Int16
        Dim StrField As String = "", StrFilter As String = "", StrValue As String = ""
        Dim StrbField As StringBuilder, IntMaxLength As Integer = 0

        For I = 0 To Rpt.DataDefinition.FormulaFields.Count - 1
            Select Case UCase(Rpt.DataDefinition.FormulaFields.Item(I).Name)
                Case "COMPANYNAME"
                    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & AgL.PubCompName & "'"
                Case "COMPANYADDRESS"
                    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & AgL.PubCompAdd1 & ", " & AgL.PubCompAdd2 & "'"
                Case "COMPANYCITY"
                    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & AgL.PubCompCity & " - " & AgL.PubCompPinCode & " '"
                Case "COUNTRY"
                    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & AgL.PubCompCountry & "'"
                Case "TITLE"
                    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & StrReportTitle & "'"
                Case "CST"
                    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & AgL.PubCompCST & "'"
                Case "FAX"
                    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & AgL.PubCompFax & "'"
                Case "PHONENO"
                    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & AgL.PubCompPhone & "'"
                Case "TINNO"
                    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & AgL.PubCompTIN & "'"
                Case "COMPANYYEAR"
                    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & AgL.PubCompYear & "'"
                Case "COMPANYSTARTDATE"
                    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & AgL.PubStartDate & "'"
                Case "COMPANYENDDATE"
                    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & AgL.PubEndDate & "'"
                Case UCase("FrmRptFormulaField")
                    Try
                        If Not FGrid Is Nothing Then
                            For J = 0 To FGrid.Rows.Count - 1
                                If FGrid(GDisplayOnReport, J).Value = "þ" Then
                                    If IntMaxLength < Len(FGrid(GFieldName, J).Value) Then
                                        IntMaxLength = Len(FGrid(GFieldName, J).Value)
                                    End If
                                End If
                            Next

                            For J = 0 To FGrid.Rows.Count - 1
                                If FGrid(GDisplayOnReport, J).Value = "þ" Then
                                    If StrValue <> "" Then
                                        StrValue = StrValue & "|"
                                    End If
                                    StrField = FGrid(GFieldName, J).Value
                                    StrbField = New StringBuilder(StrField, IntMaxLength)
                                    StrbField.Append(" ", IntMaxLength - Len(StrField))

                                    StrFilter = FGrid(GFilter, J).Value
                                    StrValue = StrValue & StrbField.ToString & " : " & StrFilter
                                End If
                            Next
                            Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & StrValue & "| '"
                        End If
                    Catch ex As Exception
                    End Try
            End Select
        Next
    End Sub

    Public Sub NumPress(ByRef TEXT As AgControls.AgTextBox, ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal LeftPlace As Integer, ByVal RightPlace As Integer, ByVal pAllowNegative As Boolean)
        On Error Resume Next
        Dim myString As String
        If RightPlace = 0 Then myString = "0123456789-" & TEXT.Tag Else myString = "0123456789.-" & TEXT.Tag
        If Asc(e.KeyChar) > 26 Then
            If InStr(myString, e.KeyChar) = 0 Then e.Handled = True
            If pAllowNegative <> True Then
                If (InStr(TEXT.Text, "-") <> 0) Or Asc(e.KeyChar) = 45 Then e.Handled = True
            End If
            If InStr(TEXT.Text, ".") <> 0 Then
                If Asc(e.KeyChar) = 46 Then e.Handled = True
                If InStr(TEXT.Text, "-") <> 0 Then
                    If InStr(TEXT.Text, ".") - 1 > LeftPlace And TEXT.SelectionStart < InStr(TEXT.Text, ".") Then
                        e.Handled = True
                    ElseIf Len(TEXT.Text) >= InStr(TEXT.Text, ".") + RightPlace And TEXT.SelectionStart >= InStr(TEXT.Text, ".") Then
                        e.Handled = True
                    End If
                Else
                    If InStr(TEXT.Text, ".") > LeftPlace And TEXT.SelectionStart < InStr(TEXT.Text, ".") Then
                        e.Handled = True
                    ElseIf Len(TEXT.Text) >= InStr(TEXT.Text, ".") + RightPlace And TEXT.SelectionStart >= InStr(TEXT.Text, ".") Then
                        e.Handled = True
                    End If
                End If
            Else
                If Asc(e.KeyChar) = 46 Then Exit Sub
                If InStr(TEXT.Text, "-") <> 0 Then
                    If Len(TEXT.Text) - 1 >= LeftPlace Then e.Handled = True
                Else
                    If Len(TEXT.Text) >= LeftPlace And Asc(e.KeyChar) <> 45 Then e.Handled = True
                End If
            End If
        ElseIf Asc(e.KeyChar) = 8 And InStr(TEXT.Text, "-") <> 0 And Mid(TEXT.Text, TEXT.SelectionStart, 1) = "." And Mid(TEXT.Text, TEXT.SelectionStart + 1, 1) <> "" And Len(TEXT.Text) - 1 - RightPlace >= LeftPlace Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = 8 And InStr(TEXT.Text, "-") = 0 And Mid(TEXT.Text, TEXT.SelectionStart, 1) = "." And Mid(TEXT.Text, TEXT.SelectionStart + 1, 1) <> "" And Len(TEXT.Text) - RightPlace >= LeftPlace Then
            e.Handled = True
        End If
    End Sub

    Public Function FGetMaxNo(ByVal StrSQL As String, ByVal SQLCon As Object) As Integer
        'Dim GCnCmd1 As New Object
        Dim IntRtnValue As Integer
        Try
            'GCnCmd1.Connection = SQLCon
            'GCnCmd1.CommandText = StrSQL
            'IntRtnValue = GCnCmd1.ExecuteScalar
            IntRtnValue = AgL.Dman_Execute(StrSQL, AgL.GcnRead).ExecuteScalar

        Catch ex As Exception
            IntRtnValue = 0
        End Try
        Return (IntRtnValue)
    End Function
    Public Function FGetMaxNo(ByVal StrSQL As String, ByVal SQLCon As OleDb.OleDbConnection) As Integer
        Dim GCnCmd1 As New OleDb.OleDbCommand
        Dim IntRtnValue As Integer
        Try
            GCnCmd1.Connection = SQLCon
            GCnCmd1.CommandText = StrSQL
            IntRtnValue = GCnCmd1.ExecuteScalar
        Catch ex As Exception
            IntRtnValue = 0
        End Try
        Return (IntRtnValue)
    End Function

    Public Function FGetMaxNoWithSiteCode(ByVal StrTableName As String, ByVal StrFieldName As String, ByVal SQLCon As Object) As Integer
        Dim GCnCmd1 As New Object
        Dim IntRtnValue As Integer
        Try
            GCnCmd1.Connection = SQLCon
            GCnCmd1.CommandText = "Select IfNull(Max(Convert(Bigint,SubString(" & StrFieldName & ", " & Len(AgL.PubSiteCode) + 1 & ", " &
                                  "(Case When (Len(" & StrFieldName & ")-" & Len(AgL.PubSiteCode) & ")>0 Then " &
                                  "(Len(" & StrFieldName & ")-" & Len(AgL.PubSiteCode) & ") End)))),0)+1 As Mx " &
                                  "From " & StrTableName & " Where " &
                                  "SubString(" & StrFieldName & ",1," & Len(AgL.PubSiteCode) & ")='" & AgL.PubSiteCode & "' " &
                                  "And IsNumeric(SubString(" & StrFieldName & "," & Len(AgL.PubSiteCode) & " + 1, Len(" & StrFieldName & ")- " & Len(AgL.PubSiteCode) & "))>0 "
            IntRtnValue = GCnCmd1.ExecuteScalar
        Catch ex As Exception
            IntRtnValue = 0
        End Try
        Return (IntRtnValue)
    End Function

    Public Function FGetDoId(ByVal TxtVNo As TextBox, ByVal StrVType As String, ByVal StrTable As String,
                             ByVal StrVNoField As String, ByVal StrVDate As String, Optional ByVal BlnFiterVType As Boolean = True) As String
        Dim GCnCmd1 As New Object
        Dim StrPrefix As String = "", StrRtnDocId As String = "", StrVNo As String = ""
        Dim StrSQL As String = ""

        If StrVDate <> "" Then
            If (CDate(AgL.PubStartDate) <= CDate(StrVDate) And CDate(AgL.PubEndDate) >= CDate(StrVDate)) Then
                'If FGetMaxNo("Select Count(*) As Cnt From Voucher_Type Where V_Type='" & StrVType & "' And IfNull(AcceptEntryAfter,'')<'" & StrVDate & "'", AgL.GCn) > 0 Then
                If FGetMaxNo("Select Count(*) As Cnt From Voucher_Type Where V_Type='" & StrVType & "' ", AgL.GCn) > 0 Then
                    StrPrefix = AgL.PubCompVPrefix
                    If Trim(StrPrefix) = "" Then
                        MsgBox(ClsMain.Msg_1)
                    Else
                        If Trim(TxtVNo.Text) <> "" Then
                            StrVNo = Trim(TxtVNo.Text)
                        Else
                            If BlnFiterVType Then
                                StrSQL = " And V_Type='" & StrVType & "' "
                            End If
                            GCnCmd1.Connection = AgL.GCn
                            GCnCmd1.CommandText = "Select IfNull(Max(Cast(" & StrVNoField & " as Integer)),0)+1 As Mx From " & StrTable & " Where (Case When  " & StrVNoField & " GLOB '*[0-9]*' Then  " & StrVNoField & " Else 0 End)<>0 And V_Prefix='" & StrPrefix & "' And Site_Code='" & AgL.PubSiteCode & "' " & StrSQL & ""
                            StrVNo = AgL.XNull(GCnCmd1.ExecuteScalar)
                        End If
                        TxtVNo.Text = StrVNo
                        TxtVNo.Tag = StrPrefix
                        If Len(Trim(AgL.PubDivCode)) <> 1 Then AgL.PubDivCode = "D"
                        StrRtnDocId = AgL.PubDivCode + AgL.PubSiteCode + Space(2 - Len(AgL.PubSiteCode)) + StrVType + Space(5 - Len(StrVType)) + Space(5 - Len(StrPrefix)) + StrPrefix + Space(8 - Len(StrVNo)) + StrVNo
                    End If
                Else
                    MsgBox("Date In Which You Are Adding An Entry Is Being Locked By Your Administrator.")
                End If
            Else
                MsgBox(ClsMain.Msg_2)
            End If
        End If
        Return (StrRtnDocId)
    End Function

    Public Function FGetDatTable(ByVal StrSqlQuery As String, ByVal DBCon As Object) As DataTable
        Dim DTTemp As New DataTable("T")

        DTTemp = AgL.FillData(StrSqlQuery, DBCon).Tables(0)
        Return DTTemp
    End Function

    Public Function FGetDatTable(ByVal StrSqlQuery As String, ByVal DBCon As OleDb.OleDbConnection) As DataTable
        Dim DTTemp As New DataTable("T")
        Dim ADTemp As OleDb.OleDbDataAdapter

        ADTemp = New OleDb.OleDbDataAdapter(StrSqlQuery, DBCon)
        ADTemp.SelectCommand.CommandTimeout = 0
        ADTemp.Fill(DTTemp)
        Return DTTemp
    End Function

    Public Sub SetEnableDisable(ByVal Frm As Form, ByVal mSet As Boolean)
        Dim Control As Object = Nothing
        For Each Control In Frm.Controls
            If TypeOf Control Is TextBox Then
                Control.enabled = mSet
                Control.BackColor = Color.White
            End If

            If TypeOf Control Is AgControls.AgDataGrid Then
                Control.enabled = mSet
            End If
        Next
    End Sub

    Public Sub FShowReport(ByVal RpdReg As CrystalDecisions.CrystalReports.Engine.ReportDocument,
    ByVal FrmMDI As Form, ByVal StrReportCaption As String, Optional ByVal BlnDirectPrint As Boolean = False,
    Optional ByVal StrPaperSizeName As String = "", Optional ByVal StrLandScape As String = "")

        Dim PDPrint As System.Drawing.Printing.PrintDocument
        Dim PRDGMain As PrintDialog = Nothing
        Dim I As Integer
        Dim IntRawKind As Integer
        Dim NRepView As RepView

        If Trim(StrPaperSizeName) <> "" Then
            PDPrint = New System.Drawing.Printing.PrintDocument()
            For I = 0 To PDPrint.PrinterSettings.PaperSizes.Count - 1
                If UCase(Trim(PDPrint.PrinterSettings.PaperSizes(I).PaperName)) = UCase(Trim(StrPaperSizeName)) Then
                    IntRawKind = CInt(PDPrint.PrinterSettings.PaperSizes(I).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(PDPrint.PrinterSettings.PaperSizes(I)))
                    RpdReg.PrintOptions.PaperSize = IntRawKind
                    RpdReg.PrintOptions.PaperOrientation = IIf(Trim(UCase(StrLandScape)) = "Y", CrystalDecisions.Shared.PaperOrientation.Landscape, CrystalDecisions.Shared.PaperOrientation.Portrait)

                    If Not BlnDirectPrint Then
                        PRDGMain = New PrintDialog
                        PRDGMain.PrinterSettings.PrinterName = PDPrint.PrinterSettings.PrinterName
                        PRDGMain.PrinterSettings.DefaultPageSettings.PaperSize = PDPrint.PrinterSettings.PaperSizes(I)
                        PRDGMain.PrinterSettings.DefaultPageSettings.Landscape = IIf(Trim(UCase(StrLandScape)) = "Y", True, False)
                    End If
                    Exit For
                End If
            Next
        End If

        If BlnDirectPrint Then
            RpdReg.PrintToPrinter(1, True, 1, 1)
        Else
            If PRDGMain Is Nothing Then PRDGMain = New PrintDialog
            NRepView = New RepView(PRDGMain, AgL)
            NRepView.RepObj = RpdReg
            NRepView.MdiParent = FrmMDI
            NRepView.Text = StrReportCaption
            NRepView.Show()
        End If
    End Sub

    Public Function DuplicacyChecking(ByVal mQuery As String, ByVal StrMsg As String) As Boolean
        Dim BlnRtn As Boolean

        BlnRtn = False

        If AgL.Dman_Execute(mQuery, AgL.GCn).ExecuteScalar > 0 Then
            MsgBox(StrMsg)
            BlnRtn = True
        End If

        DuplicacyChecking = BlnRtn
    End Function
    Public Function FRemoveSpace(ByVal StrValue As String) As String
        Dim StrRtn As String = ""
        Dim BlnFlag As Boolean = False
        Dim I As Integer

        For I = 1 To Len(StrValue)
            If Mid(StrValue, I, 1) = " " Then
                BlnFlag = True
            Else
                If BlnFlag Then StrRtn += " "
                StrRtn += Mid(StrValue, I, 1)
                BlnFlag = False
            End If
        Next

        FRemoveSpace = StrRtn
    End Function
    Public Shared Function FGetManualRefNo(ByVal FieldName As String, ByVal TableName As String, ByVal V_Type As String, ByVal V_Date As String, ByVal Div_Code As String, ByVal Site_Code As String, Optional ByVal RefType As Integer = 0) As String
        Dim mQry$
        Dim mStartSrlNo As Integer = 0
        Dim mStartDate As String, mEndDate As String
        Dim mRef_Prefix$ = ""
        Dim mRef_PadLength As Integer = 0

        If V_Type = "OB" Then
            mStartDate = DateAdd(DateInterval.Year, -1, CDate(AgL.PubStartDate))
            mEndDate = AgL.PubEndDate 'DateAdd(DateInterval.Year, -1, CDate(AgL.PubEndDate))
        Else
            mStartDate = AgL.PubStartDate
            mEndDate = AgL.PubEndDate
        End If

        'If CDate(V_Date) >= CDate("01/Apr/2018") And CDate(V_Date) <= CDate("31/Mar/2019") Then
        '    mStartDate = "01/Apr/2018"
        '    mEndDate = "31/Mar/2019"
        'ElseIf CDate(V_Date) >= CDate("01/Apr/2017") And CDate(V_Date) <= CDate("31/Mar/2018") Then
        '    mStartDate = "01/Apr/2017"
        '    mEndDate = "31/Mar/2018"
        'ElseIf CDate(V_Date) >= CDate("01/Apr/2016") And CDate(V_Date) <= CDate("31/Mar/2017") Then
        '    mStartDate = "01/Apr/2016"
        '    mEndDate = "31/Mar/2017"
        'ElseIf CDate(V_Date) >= CDate("01/Apr/2015") And CDate(V_Date) <= CDate("31/Mar/2016") Then
        '    mStartDate = "01/Apr/2015"
        '    mEndDate = "31/Mar/2016"
        'ElseIf CDate(V_Date) >= CDate("01/Apr/2014") And CDate(V_Date) <= CDate("31/Mar/2015") Then
        '    mStartDate = "01/Apr/2014"
        '        mEndDate = "31/Mar/2015"
        '    ElseIf CDate(V_Date) >= CDate("01/Apr/2013") And CDate(V_Date) <= CDate("31/Mar/2014") Then
        '    mStartDate = "01/Apr/2013"
        '    mEndDate = "31/Mar/2014"
        'ElseIf CDate(V_Date) >= CDate("01/Apr/2012") And CDate(V_Date) <= CDate("31/Mar/2013") Then
        '    mStartDate = "01/Apr/2012"
        '    mEndDate = "31/Mar/2013"
        'ElseIf CDate(V_Date) >= CDate("01/Apr/2011") And CDate(V_Date) <= CDate("31/Mar/2012") Then
        '    mStartDate = "01/Apr/2011"
        '    mEndDate = "31/Mar/2012"
        'ElseIf CDate(V_Date) >= CDate("01/Apr/2010") And CDate(V_Date) <= CDate("31/Mar/2011") Then
        '    mStartDate = "01/Apr/2010"
        '    mEndDate = "31/Mar/2011"
        'Else
        '    mStartDate = "01/Apr/2009"
        '    mEndDate = "31/Mar/2010"
        'End If

        Select Case RefType
            Case 1
                mQry = "Select IfNull(Max(Convert(Numeric,Replace(Replace(" & FieldName & ",'-',''),'.',''))),0)+1 From " & TableName & " With (NoLock)  WHERE isnumeric(Replace(Replace(" & FieldName & ",'-',''),'.',''))>0 And V_Type = '" & V_Type & "' And Div_Code = '" & Div_Code & "' and Site_Code = '" & Site_Code & "'  And Date(V_Date) = '" & AgL.PubLoginDate & "'  AND IsNumeric(" & FieldName & ") = 0 "
                FGetManualRefNo = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
                FGetManualRefNo = CDate(AgL.PubLoginDate).ToString("yyMMdd").ToString + "-" + FGetManualRefNo.ToString.PadLeft(4, "0")

            Case Else
                mRef_Prefix = AgL.XNull(AgL.Dman_Execute("Select Ref_Prefix From Voucher_Prefix With (NoLock) Where V_Type = '" & V_Type & "' And Div_Code = '" & Div_Code & "' And Site_Code = '" & Site_Code & "' And Date(Date_From) = '" & CDate(mStartDate).ToString("s") & "' And Date(Date_To) = '" & CDate(mEndDate).ToString("s") & "'", AgL.GcnRead).ExecuteScalar)
                If mRef_Prefix = "" Then
                    If CDate(V_Date) >= CDate("01/Apr/2013") And CDate(V_Date) <= CDate("31/Mar/2014") Then
                        mQry = "Select IfNull(Max(Cast(Replace(Replace(" & FieldName & ",'-',''),'.','') as integer)),0)+1 From " & TableName & " With (NoLock)  WHERE  ABS(Replace(Replace(" & FieldName & ",'-',''),'.',''))>0 And V_Type = '" & V_Type & "' And Div_Code = '" & Div_Code & "' and Site_Code = '" & Site_Code & "'  And Date(V_Date) Between '" & CDate(mStartDate).ToString("s") & "' and  '" & CDate(mEndDate).ToString("s") & "' "
                        FGetManualRefNo = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
                        If Val(FGetManualRefNo) > 1300000 Then
                            FGetManualRefNo = Val(FGetManualRefNo) - 1300000
                        ElseIf Val(FGetManualRefNo) > 130000 Then
                            FGetManualRefNo = Val(FGetManualRefNo) - 130000
                        Else
                            FGetManualRefNo = FGetManualRefNo
                        End If
                        FGetManualRefNo = "13-" + FGetManualRefNo.ToString.PadLeft(4, "0")

                    Else
                        mQry = "Select IfNull(Max(Cast(Replace(Replace(" & FieldName & ",'-',''),'.','') as integer)),0)+1 From " & TableName & " With (NoLock) WHERE ABS(Replace(Replace(" & FieldName & ",'-',''),'.',''))>0 And V_Type = '" & V_Type & "' And Div_Code = '" & Div_Code & "' and Site_Code = '" & Site_Code & "'  And Date(V_Date) Between " & AgL.DateForFilter(mStartDate) & " and  " & AgL.DateForFilter(mEndDate) & "    "

                        FGetManualRefNo = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
                    End If
                Else
                    mQry = "Select IfNull(Max(Cast(Replace(Replace(Replace(Substring(" & FieldName & "," & mRef_Prefix.Length & ", 20)  ,'-',''),'.',''),'" & mRef_Prefix & "','') as Integer)),0) + 1 From " & TableName & " With (NoLock)  WHERE Abs(Replace(Replace(Replace(" & FieldName & ",'-',''),'.',''),'" & mRef_Prefix & "',''))>0 And V_Type = '" & V_Type & "' And Div_Code = '" & Div_Code & "' and Site_Code = '" & Site_Code & "'  And Date(V_Date) Between '" & CDate(mStartDate).ToString("s") & "' and  '" & CDate(mEndDate).ToString("s") & "' And Left(" & FieldName & "," & mRef_Prefix.Length & ")='" & mRef_Prefix & "'   "
                    FGetManualRefNo = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar

                    mRef_PadLength = AgL.VNull(AgL.Dman_Execute("Select Ref_PadLength From Voucher_Prefix With (NoLock) Where V_Type = '" & V_Type & "' And Div_Code = '" & Div_Code & "' And Site_Code = '" & Site_Code & "' And Date(Date_From) = '" & CDate(mStartDate).ToString("s") & "' And Date(Date_To) = '" & CDate(mEndDate).ToString("s") & "'", AgL.GcnRead).ExecuteScalar)
                    If mRef_PadLength = 0 Then
                        FGetManualRefNo = mRef_Prefix & FGetManualRefNo.ToString.PadLeft(4, "0")
                    Else
                        FGetManualRefNo = mRef_Prefix & FGetManualRefNo.ToString.PadLeft(mRef_PadLength, "0")
                    End If
                End If
        End Select
    End Function

    Function FGetRecId(ByVal StrDate As String, ByVal StrTableName As String, ByVal StrColName As String, ByVal StrDateField As String,
    Optional ByVal StrV_Type As String = "", Optional ByVal BlnFilterType As Boolean = False,
    Optional ByVal ApplyFormat As RecIdFormat = RecIdFormat.DD_MM) As String
        Dim DTTemp As New DataTable
        Dim StrSQL As String
        Dim DblVNo As Integer = 0, DblDay As Int16 = 0, DblMonth As Int16 = 0, DblYear As Int16 = 0
        Dim StrRtn As String = ""
        Dim StrCondition As String = ""

        If InStr(StrV_Type, "'") > 0 Then
            StrCondition = "And V_Type In (" & StrV_Type & ") "
        Else
            If BlnFilterType Then StrCondition = "And V_Type='" & StrV_Type & "' "
        End If

        If ApplyFormat = RecIdFormat.MM Then
            StrSQL = "Select IfNull(Max(Cast(SubString(IfNull(" & StrColName & ",''),3,6) As Float)),0)+1 As Mx, "
            StrSQL += "Day('" & StrDate & "') As DY,Month('" & StrDate & "') As MT,Year('" & StrDate & "') As YR "
            StrSQL += "From " & StrTableName & " Where Month(" & StrDateField & ")=" & Month(StrDate) & " And "
            StrSQL += "Year(" & StrDateField & ")=" & Year(StrDate) & " And "
            StrSQL += "Site_Code='" & AgL.PubSiteCode & "' " & StrCondition
            DTTemp = FGetDatTable(StrSQL, AgL.GCn)
            If DTTemp.Rows.Count > 0 Then
                DblVNo = AgL.VNull(DTTemp.Rows(0).Item("Mx"))
                DblDay = AgL.VNull(DTTemp.Rows(0).Item("DY"))
                DblMonth = AgL.VNull(DTTemp.Rows(0).Item("MT"))
                DblYear = AgL.VNull(DTTemp.Rows(0).Item("YR"))
            End If
            DTTemp.Dispose()
            StrRtn = Format(DblMonth, "00") + Format(DblVNo, "000000")
        ElseIf ApplyFormat = RecIdFormat.DD_MM Then
            StrSQL = "Select IfNull(Max(Cast(SubString(IfNull(" & StrColName & ",''),5,4) As Float)),0)+1 As Mx, "
            StrSQL += "Day('" & StrDate & "') As DY,Month('" & StrDate & "') As MT,Year('" & StrDate & "') As YR "
            StrSQL += "From " & StrTableName & " Where " & StrDateField & "='" & StrDate & "' And "
            StrSQL += "Site_Code='" & AgL.PubSiteCode & "' " & StrCondition
            DTTemp = FGetDatTable(StrSQL, AgL.GCn)
            If DTTemp.Rows.Count > 0 Then
                DblVNo = AgL.VNull(DTTemp.Rows(0).Item("Mx"))
                DblDay = AgL.VNull(DTTemp.Rows(0).Item("DY"))
                DblMonth = AgL.VNull(DTTemp.Rows(0).Item("MT"))
                DblYear = AgL.VNull(DTTemp.Rows(0).Item("YR"))
            End If
            DTTemp.Dispose()
            StrRtn = Format(DblDay, "00") + Format(DblMonth, "00") + Format(DblVNo, "0000")
        ElseIf ApplyFormat = RecIdFormat.DD_MM_YY Then
            StrSQL = "Select IfNull(Max(Cast(SubString(IfNull(" & StrColName & ",''),7,4) As Float)),0)+1 As Mx, "
            StrSQL += "Day('" & StrDate & "') As DY,Month('" & StrDate & "') As MT,Year('" & StrDate & "') As YR "
            StrSQL += "From " & StrTableName & " Where " & StrDateField & "='" & StrDate & "' And "
            StrSQL += "Site_Code='" & AgL.PubSiteCode & "' " & StrCondition
            DTTemp = FGetDatTable(StrSQL, AgL.GCn)
            If DTTemp.Rows.Count > 0 Then
                DblVNo = AgL.VNull(DTTemp.Rows(0).Item("Mx"))
                DblDay = AgL.VNull(DTTemp.Rows(0).Item("DY"))
                DblMonth = AgL.VNull(DTTemp.Rows(0).Item("MT"))
                DblYear = Right(AgL.XNull(DTTemp.Rows(0).Item("YR")), 2)
            End If
            DTTemp.Dispose()
            StrRtn = Format(DblDay, "00") + Format(DblMonth, "00") + Format(DblYear, "00") + Format(DblVNo, "0000")
        End If
        FGetRecId = StrRtn
    End Function


#Region "Rate FIFO/Average Update"
    Public Sub FUpdateFIFO_Average(Optional ByVal StrItemTypesFor As String = "",
    Optional ByVal StrItemsFor As String = "", Optional ByVal StrTableName As String = "Stock")
        Dim I As Integer
        Dim StrSQL As String, StrPrvItem As String = ""
        Dim StrCondition As String = ""
        Dim DTTemp As DataTable
        Dim DTStroreRec As DataTable, DTAdjustDN_CN As DataTable
        Dim DblStockQty As Double, DblStockValue As Double
        Dim BlnFIFOUpdate As Boolean = False, BlnWAverageUpdate As Boolean = False
        Dim DRTemp As DataRow
        Dim DblIssueValue_FIFO As Double, DblIssueValue_WAvg As Double
        Dim DblQty_Iss As Double
        Dim SQLCmd As Object
        Dim FrmPB As FrmProgressBar

        StrItemTypesFor = Trim(StrItemTypesFor)
        StrItemsFor = Trim(StrItemsFor)
        If StrItemTypesFor = "" And StrItemsFor = "" Then MsgBox("No Item Type / Item Mentioned.") : Exit Sub

        'StrSQL = "Declare @TempTable Table(DocId NvarChar(21),Item NVarChar(10),Qty Float,Amount Float) "
        'StrSQL += "Select DocId,Item,Qty,Amount From @TempTable "

        StrSQL = "Select '' As DocId,'' As Item,'' As Qty,'' As Amount Where 1=2  "
        DTStroreRec = FGetDatTable(StrSQL, AgL.GCn)

        'StrSQL = "Declare @TempTable Table(Neg_Pos NVarChar(1),Qty Float,Amount Float) "
        'StrSQL += "Select Neg_Pos,Qty,Amount From @TempTable "
        StrSQL = "Select '' As Neg_Pos,'' As Qty,'' As Amount Where 1=2  "
        DTAdjustDN_CN = FGetDatTable(StrSQL, AgL.GCn)

        SQLCmd = New Object
        FrmPB = New FrmProgressBar
        FrmPB.Show()

        If StrItemsFor <> "" Then
            StrCondition = "IM.Code In ( " & StrItemsFor & ") "
        Else
            StrCondition = "IM.ItemType In ( " & StrItemTypesFor & ") "
        End If

        StrSQL = "Select	ST.DocId,ST.Sr, ST.Item,IfNull(ST.EType_IR,'') As EType_IR, "
        '======= Qty_Rec =======
        StrSQL += "(Case	When (IfNull(ST.EType_IR,'')='R' And IfNull(ST.Qty_Rec,0)>0) Then Abs(IfNull(ST.Qty_Rec,0))  "
        StrSQL += "When (IfNull(ST.EType_IR,'')='I' And IfNull(ST.Qty_Iss,0)<0) Then Abs(IfNull(ST.Qty_Iss,0))  "
        StrSQL += "Else 0 End) As Qty_Rec, "
        '====== RecValue ======
        StrSQL += "Round((Case	When (IfNull(ST.EType_IR,'')='R' And IfNull(ST.Landed_Value,0)+IfNull(ST.OtherAdjustment,0)>0) Then Abs(IfNull(ST.Landed_Value,0)+IfNull(ST.OtherAdjustment,0))  "
        StrSQL += "When (IfNull(ST.EType_IR,'')='I' And IfNull(ST.Landed_Value,0)+IfNull(ST.OtherAdjustment,0)<0) Then Abs(IfNull(ST.Landed_Value,0)+IfNull(ST.OtherAdjustment,0))  "
        StrSQL += "Else 0 End),2) As RecValue, "
        '====== Qty_Iss ======
        StrSQL += "(Case	When (IfNull(ST.EType_IR,'')='I' And IfNull(ST.Qty_Iss,0)>0) Then Abs(IfNull(ST.Qty_Iss,0)) "
        StrSQL += "When (IfNull(ST.EType_IR,'')='R' And IfNull(ST.Qty_Rec,0)<0) Then Abs(IfNull(ST.Qty_Rec,0))  "
        StrSQL += "Else 0 End) As Qty_Iss, "
        '===== IssueValue =====
        StrSQL += "Round((Case	When (IfNull(ST.EType_IR,'')='I' And IfNull(ST.Landed_Value,0)+IfNull(ST.OtherAdjustment,0)>0) Then Abs(IfNull(ST.Landed_Value,0)+IfNull(ST.OtherAdjustment,0))  "
        StrSQL += "When (IfNull(ST.EType_IR,'')='R' And IfNull(ST.Landed_Value,0)+IfNull(OtherAdjustment,0)<0) Then Abs(IfNull(ST.Landed_Value,0)+IfNull(ST.OtherAdjustment,0))  "
        StrSQL += "Else 0 End),2) As IssueValue, "
        '===== ToBeUpdated ====
        StrSQL += "(Case	When IfNull(ST.Landed_Value,0)+IfNull(ST.OtherAdjustment,0)<0 Or IfNull(ST.EType_IR,'')='R' Or "
        StrSQL += "(IfNull(ST.Qty_Iss,0)=0 And IfNull(ST.Qty_Rec,0)=0) Then 'N' Else 'Y' End) As ToBeUpdated, "

        StrSQL += "(Case	When IfNull(ST.Landed_Value,0)+IfNull(ST.OtherAdjustment,0)<0 Then 'N' Else 'P' End) As Neg_Pos "

        StrSQL += "From " & StrTableName & " ST "
        StrSQL += "Left Join Item IM On IM.Code=ST.Item "
        StrSQL += "Left Join Voucher_Type VT On VT.V_Type=ST.V_Type "
        StrSQL += "Where " & StrCondition

        StrSQL += "Order By ST.Item,ST.V_Date,IfNull(VT.SerialNo,0),ST.RecId "

        DTTemp = FGetDatTable(StrSQL, AgL.GCn)

        SQLCmd = AgL.GCn.CreateCommand

        SQLCmd.CommandText = "Update " & StrTableName & " Set FifoAmt=IfNull(Landed_Value,0)+IfNull(OtherAdjustment,0),AVGAmt=Landed_Value Where Item In (Select Code From Item IM Where " & StrCondition & ") "
        SQLCmd.ExecuteNonQuery()

        DblStockQty = 0
        DblStockValue = 0
        DblIssueValue_FIFO = 0
        DblIssueValue_WAvg = 0

        For I = 0 To DTTemp.Rows.Count - 1
            FrmPB.Text = Trim(DTTemp.Rows.Count - 1) & " / " & Trim(I)
            FrmPB.FMoveBar()

            If StrPrvItem <> AgL.XNull(DTTemp.Rows(I)("Item")) Then
                DTAdjustDN_CN.Rows.Clear()
                DTStroreRec.Rows.Clear()
                DblStockValue = 0
                DblStockQty = 0
            End If

            If DTTemp.Rows(I)("Qty_Rec") > 0 Or DTTemp.Rows(I)("RecValue") > 0 Then
                If AgL.VNull(DTTemp.Rows(I)("Qty_Rec")) > 0 Then
                    DRTemp = DTStroreRec.NewRow
                    DRTemp.Item("DocId") = DTTemp.Rows(I)("DocId")
                    DRTemp.Item("Item") = DTTemp.Rows(I)("Item")
                    DRTemp.Item("Qty") = DTTemp.Rows(I)("Qty_Rec")
                    DRTemp.Item("Amount") = DTTemp.Rows(I)("RecValue")

                    DTStroreRec.Rows.Add(DRTemp)
                End If
                FAdjustDN_CN(DTStroreRec, DTAdjustDN_CN, DTTemp.Rows(I)("Qty_Rec"), DTTemp.Rows(I)("RecValue"), DTTemp.Rows(I)("EType_IR"), DTTemp.Rows(I)("Neg_Pos"))

                DblStockQty += DTTemp.Rows(I)("Qty_Rec")
                DblStockValue += DTTemp.Rows(I)("RecValue")

            ElseIf DTTemp.Rows(I)("Qty_Iss") > 0 Or DTTemp.Rows(I)("IssueValue") > 0 Then
                DblQty_Iss = DTTemp.Rows(I)("Qty_Iss")
                DblIssueValue_FIFO = 0
                DblIssueValue_WAvg = 0
                BlnFIFOUpdate = False
                BlnWAverageUpdate = False

                FAdjustDN_CN(DTStroreRec, DTAdjustDN_CN, DTTemp.Rows(I)("Qty_Iss"), DTTemp.Rows(I)("IssueValue"), DTTemp.Rows(I)("EType_IR"), DTTemp.Rows(I)("Neg_Pos"))

                If Trim(UCase(DTTemp.Rows(I)("Neg_Pos"))) = "P" Then
                    While DblQty_Iss > 0
                        BlnFIFOUpdate = True
                        If Not DTStroreRec.Rows.Count > 0 Then Exit While
                        If DTStroreRec.Rows(0)("Qty") = 0 And DTStroreRec.Rows(0)("Amount") = 0 Then
                            DTStroreRec.Rows(0).Delete()
                        Else
                            If (DTStroreRec.Rows(0)("Qty") - DblQty_Iss) > 0 Then
                                DblIssueValue_FIFO += DblQty_Iss * Format(DTStroreRec.Rows(0)("Amount") / DTStroreRec.Rows(0)("Qty"), "0.00000")
                                DTStroreRec.Rows(0).Item("Amount") -= DblQty_Iss * Format(DTStroreRec.Rows(0)("Amount") / DTStroreRec.Rows(0)("Qty"), "0.00000")
                                DTStroreRec.Rows(0).Item("Qty") -= DblQty_Iss
                                DblQty_Iss = 0
                            ElseIf (DTStroreRec.Rows(0)("Qty") - DblQty_Iss) <= 0 Then
                                DblIssueValue_FIFO += DTStroreRec.Rows(0)("Qty") * Format(DTStroreRec.Rows(0)("Amount") / DTStroreRec.Rows(0)("Qty"), "0.00000")
                                DblQty_Iss = DblQty_Iss - DTStroreRec.Rows(0)("Qty")
                                DTStroreRec.Rows(0).Delete()
                            End If
                        End If
                    End While

                    '==============================================
                    '=========== For Weightage Average ============
                    '==============================================
                    If DTTemp.Rows(I)("Qty_Iss") > 0 Then
                        If DblStockQty > 0 Then
                            DblIssueValue_WAvg = DTTemp.Rows(I)("Qty_Iss") * Format((DblStockValue / DblStockQty), "0.00000")
                        End If
                        BlnWAverageUpdate = True
                    End If
                    '==============================================
                    '==============================================
                    '==============================================
                End If

                '==============================================
                '=========== For Weightage Average ============
                '==============================================

                If Not BlnWAverageUpdate Then
                    DblIssueValue_WAvg = DTTemp.Rows(I)("IssueValue")
                End If

                DblStockQty -= DTTemp.Rows(I)("Qty_Iss")
                DblStockValue -= DblIssueValue_WAvg
                '==============================================
                '==============================================
                '==============================================

                If BlnFIFOUpdate And BlnWAverageUpdate Then
                    SQLCmd.CommandText = "Update " & StrTableName & " Set  FifoAmt = " & DblIssueValue_FIFO & ", "
                    SQLCmd.CommandText += "AVGAmt = " & DblIssueValue_WAvg & " "
                    SQLCmd.CommandText += "Where DocId='" & DTTemp.Rows(I).Item("DocID") & "' And "
                    SQLCmd.CommandText += "Item='" & DTTemp.Rows(I).Item("Item") & "' And "
                    SQLCmd.CommandText += "Sr=" & DTTemp.Rows(I).Item("Sr") & " "
                    SQLCmd.ExecuteNonQuery()
                ElseIf BlnFIFOUpdate Then
                    SQLCmd.CommandText = "Update " & StrTableName & " Set  FifoAmt = " & DblIssueValue_FIFO & " "
                    SQLCmd.CommandText += "Where DocId='" & DTTemp.Rows(I).Item("DocID") & "' And "
                    SQLCmd.CommandText += "Item='" & DTTemp.Rows(I).Item("Item") & "' And "
                    SQLCmd.CommandText += "Sr=" & DTTemp.Rows(I).Item("Sr") & " "
                    SQLCmd.ExecuteNonQuery()
                ElseIf BlnWAverageUpdate Then
                    SQLCmd.CommandText = "Update " & StrTableName & " Set  "
                    SQLCmd.CommandText += "AVGAmt = " & DblIssueValue_WAvg & " "
                    SQLCmd.CommandText += "Where DocId='" & DTTemp.Rows(I).Item("DocID") & "' And "
                    SQLCmd.CommandText += "Item='" & DTTemp.Rows(I).Item("Item") & "' And "
                    SQLCmd.CommandText += "Sr=" & DTTemp.Rows(I).Item("Sr") & " "
                    SQLCmd.ExecuteNonQuery()
                End If
            End If
            StrPrvItem = DTTemp.Rows(I)("Item")
        Next

        DTTemp.Dispose()
        DTTemp = Nothing
        SQLCmd.Dispose()
        SQLCmd = Nothing
        FrmPB.Dispose()
        FrmPB = Nothing
    End Sub
    Private Sub FAdjustDN_CN(ByRef DTStroreRecVar As DataTable, ByRef DTAdjustDN_CNVar As DataTable,
    ByVal DblQtyVar As Double, ByVal DblValueVar As Double,
    ByVal StrETypeVar As String, ByVal StrNeg_Pos As String)

        Dim IntIndexForQty As Integer = 0, IntIndexForAmount As Integer = 0
        Dim DRTemp As DataRow, BlnIsWorking As Boolean

        If (DblQtyVar = 0 And Trim(UCase(StrETypeVar)) = "R") Or (Trim(UCase(StrNeg_Pos)) = "N" And Trim(UCase(StrETypeVar)) = "I") Then
            DRTemp = DTAdjustDN_CNVar.NewRow
            DRTemp.Item("Neg_Pos") = StrNeg_Pos
            DRTemp.Item("Amount") = DblValueVar
            DRTemp.Item("Qty") = DblQtyVar
            DTAdjustDN_CNVar.Rows.Add(DRTemp)
        End If

        IntIndexForAmount = DTStroreRecVar.Rows.Count - 1
        IntIndexForQty = DTStroreRecVar.Rows.Count - 1
        While True
            '============================================
            '============ Exit while Conditions =========
            '============================================
            If Not DTAdjustDN_CNVar.Rows.Count > 0 Then Exit While
            If IntIndexForQty < 0 And IntIndexForAmount < 0 Then Exit While
            '============================================
            '============================================
            '============================================

            If Trim(UCase(DTAdjustDN_CNVar.Rows(0).Item("Neg_Pos"))) = "P" Then
                DTStroreRecVar.Rows(IntIndexForAmount)("Amount") += DTAdjustDN_CNVar.Rows(0).Item("Amount")
                DTAdjustDN_CNVar.Rows(0).Delete()
            ElseIf Trim(UCase(DTAdjustDN_CNVar.Rows(0).Item("Neg_Pos"))) = "N" Then
                BlnIsWorking = False
                If DTAdjustDN_CNVar.Rows(0).Item("Qty") <> 0 And IntIndexForQty >= 0 Then
                    BlnIsWorking = True
                    If (DTStroreRecVar.Rows(IntIndexForQty)("Qty") - DTAdjustDN_CNVar.Rows(0).Item("Qty")) > 0 Then

                        DTStroreRecVar.Rows(IntIndexForQty)("Qty") -= DTAdjustDN_CNVar.Rows(0).Item("Qty")
                        DTAdjustDN_CNVar.Rows(0).Item("Qty") = 0
                    Else
                        DTAdjustDN_CNVar.Rows(0).Item("Qty") -= DTStroreRecVar.Rows(IntIndexForQty)("Qty")
                        DTStroreRecVar.Rows(IntIndexForQty)("Qty") = 0
                        IntIndexForQty -= 1
                    End If
                End If

                If DTAdjustDN_CNVar.Rows(0).Item("Amount") <> 0 And IntIndexForAmount >= 0 Then
                    BlnIsWorking = True
                    If (DTStroreRecVar.Rows(IntIndexForAmount)("Amount") - DTAdjustDN_CNVar.Rows(0).Item("Amount")) > 0 Then

                        DTStroreRecVar.Rows(IntIndexForAmount)("Amount") -= DTAdjustDN_CNVar.Rows(0).Item("Amount")
                        DTAdjustDN_CNVar.Rows(0).Item("Amount") = 0
                    Else
                        DTAdjustDN_CNVar.Rows(0).Item("Amount") -= DTStroreRecVar.Rows(IntIndexForQty)("Amount")
                        DTStroreRecVar.Rows(IntIndexForAmount)("Amount") = 0
                        IntIndexForAmount -= 1
                    End If
                End If

                If DTAdjustDN_CNVar.Rows(0).Item("Amount") = 0 And DTAdjustDN_CNVar.Rows(0).Item("Qty") = 0 Then
                    DTAdjustDN_CNVar.Rows(0).Delete()
                End If
                If Not BlnIsWorking Then Exit While
            End If
        End While
    End Sub
#End Region



    Public Function FGrdDisableKeys(ByVal e As System.Windows.Forms.KeyEventArgs) As Boolean
        Dim BlnReturn As Boolean

        Select Case e.KeyCode
            Case 65, 66, 67, 68, 69, 70,
                 71, 72, 73, 74, 75, 76, 77, 78, 79, 80,
                 81, 82, 83, 84, 85, 86, 87, 88, 89, 90     'For Alphabets (A-Z)
                BlnReturn = True
            Case 48, 49, 50, 51, 52, 53, 54, 55, 56, 57     'For Alphabets (0-9)
                BlnReturn = True
            Case 96, 97, 98, 99, 100, 101, 102, 103,
                 104, 105                                   'For Alphabets (0-9) Num Keys
                BlnReturn = True
            Case 112                                        'For Function Key (F1)
                BlnReturn = True
            Case Else
                BlnReturn = False                           'For Other Keys
        End Select

        If e.Control Or e.Alt Or e.Shift Then BlnReturn = False
        FGrdDisableKeys = BlnReturn
    End Function

#End Region

    Public Enum ImportFor
        Dos
        Excel
    End Enum

    Public Sub FDatabase(ByRef MdlTable() As AgLibrary.ClsMain.LITable)
        '===================================
        '============ Item Related =========
        '===================================
        'FUnitMast(MdlTable, "UnitMast")
        'FItemCat(MdlTable, "ItemCat")
        'FItemGroup(MdlTable, "ItemGroup")
        'FVatCodeMast(MdlTable, "VatCodeMast")
        'FTariffMast(MdlTable, "TariffMast")
        'FItemMast(MdlTable, "ItemMast")
        'FItemType(MdlTable, "ItemType")
        '===================================
        '============ Item Related =========
        '===================================


        '===================================
        '============ Stock Base ===========
        '===================================
        'FDepartmentMast(MdlTable, "DepartmentMast")
        'FStock(MdlTable, "Stock")
        'FStockOtherAdjustment(MdlTable, "StockOtherAdjustment")
        '===================================
        '============ Stock Base ===========
        '===================================

        'FSubGroup(MdlTable, "SubGroup")
        'FLedger(MdlTable, "Ledger")
        'FLedger(MdlTable, "Ledger_Temp")
        'FChequeMast(MdlTable, "ChequeMast")

        'FCurrency(MdlTable, "Currency")
        'FAcGroup(MdlTable, "AcGroup")
        'FAcGroupPositioning(MdlTable, "AcGroupPositioning")
        'FAssetGroupMast(MdlTable, "AssetGroupMast")
        'FAssetMast(MdlTable, "AssetMast")
        'FAssetTransaction(MdlTable, "AssetTransaction")
        'FBankReconsilation(MdlTable, "BankReconsilation")
        'FTDSCat_Description(MdlTable, "TDSCat_Description")
        'FTdsCat(MdlTable, "TdsCat")
        'FTdsCat_Det(MdlTable, "TdsCat_Det")
        FCostCenterMast(MdlTable, "CostCenterMast")
        'FBudget(MdlTable, "Budget")
        'FBudgetDet(MdlTable, "BudgetDet")
        'FCity(MdlTable, "City")
        'FCountry(MdlTable, "Country")
        FAcGroupPath(MdlTable, "AcGroupPath")
        FEnviro_Accounts(MdlTable, "Enviro_Accounts")
        'FLedgerM(MdlTable, "LedgerM")
        'FLedgerM(MdlTable, "LedgerM_Temp")
        FLedgerAdj(MdlTable, "LedgerAdj_Temp")
        FLedgerAdj(MdlTable, "LedgerAdj")
        FLedgerItemAdj(MdlTable, "LedgerItemAdj")
        FNarrationMast(MdlTable, "NarrationMast")
        FAcFilteration(MdlTable, "AcFilteration")
        FLedgerGroup(MdlTable, "LedgerGroup")
        FZoneMast(MdlTable, "ZoneMast")
        'FSTaxTrn(MdlTable, "STaxTrn")
        'FChequePrintSetup(MdlTable, "ChequePrintSetup")

        '===================================
        '============ W System =============
        '===================================
        FDataTrfd(MdlTable, "DataTrfd")
    End Sub
    Private Sub FChequePrintSetup(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)

        AgL.FSetColumnValue(MdlTable, "BankCode", ClsMain.SQLDataType.VarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "Amount_Left", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Amount_Top", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "AmountInWords_Left", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "AmountInWords_Top", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "AuthorizedBy", ClsMain.SQLDataType.VarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Authorized_Left", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Authorized_Top", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ChequeDate_Left", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ChequeDate_Top", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "CompanyName", ClsMain.SQLDataType.VarChar, 50)
        AgL.FSetColumnValue(MdlTable, "CompanyName_Left", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "CompanyName_Top", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "FavourOf_Left", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "FavourOf_Top", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "PaperSizeName", ClsMain.SQLDataType.VarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Site_Code", ClsMain.SQLDataType.VarChar, 2)
        AgL.FSetColumnValue(MdlTable, "PreparedBy", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)

        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "BankCode", "SubCode", "SubGroup")
    End Sub
    Private Sub FDepartmentMast(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "Code", ClsMain.SQLDataType.VarChar, 6, True)
        AgL.FSetColumnValue(MdlTable, "Name", ClsMain.SQLDataType.VarChar, 35)
        AgL.FSetColumnValue(MdlTable, "Site_Code", ClsMain.SQLDataType.VarChar, 2)
        AgL.FSetColumnValue(MdlTable, "U_Name", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)

        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
    End Sub
    Private Sub FStock(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", ClsMain.SQLDataType.VarChar, 21, True)
        AgL.FSetColumnValue(MdlTable, "V_Type", ClsMain.SQLDataType.VarChar, 5)
        AgL.FSetColumnValue(MdlTable, "RecId", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "V_SNo", ClsMain.SQLDataType.SmallInt, , True)
        AgL.FSetColumnValue(MdlTable, "V_Date", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "PtyChallanNo", ClsMain.SQLDataType.VarChar, 50)
        AgL.FSetColumnValue(MdlTable, "PtyBillNo", ClsMain.SQLDataType.VarChar, 50)
        AgL.FSetColumnValue(MdlTable, "PartyCode", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ItemCode", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "RecQty", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "IssueQty", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "LandedRate", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "LandedValue", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "OtherAdjustment", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "FIFOValue", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "AverageValue", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "CostCenter", ClsMain.SQLDataType.VarChar, 6)
        AgL.FSetColumnValue(MdlTable, "Department", ClsMain.SQLDataType.VarChar, 6)
        AgL.FSetColumnValue(MdlTable, "Godown", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "OwnYN", ClsMain.SQLDataType.VarChar, 1, , , "Y")
        AgL.FSetColumnValue(MdlTable, "EType_IR", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Remark", ClsMain.SQLDataType.VarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Site_Code", ClsMain.SQLDataType.VarChar, 2)

        AgL.FSetFKeyValue(MdlTable, "CostCenter", "Code", "CostCenterMast")
        AgL.FSetFKeyValue(MdlTable, "Department", "Code", "DepartmentMast")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "PartyCode", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "ItemCode", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Godown", "Code", "Godown")
    End Sub
    Private Sub FStockOtherAdjustment(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", ClsMain.SQLDataType.VarChar, 21, True)
        AgL.FSetColumnValue(MdlTable, "V_SNo", ClsMain.SQLDataType.SmallInt, , True)
        AgL.FSetColumnValue(MdlTable, "StockRefDocId", ClsMain.SQLDataType.VarChar, 21)
        AgL.FSetColumnValue(MdlTable, "StockRefV_SNo", ClsMain.SQLDataType.SmallInt)
        AgL.FSetColumnValue(MdlTable, "AdjAmount", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "LandedValue", ClsMain.SQLDataType.Float)

        AgL.FSetFKeyValue(MdlTable, "StockRefDocId,StockRefV_SNo", "DocId,V_SNo", "Stock")
    End Sub
    Private Sub FItemType(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "Code", ClsMain.SQLDataType.VarChar, 2, True, False)
        AgL.FSetColumnValue(MdlTable, "Name", ClsMain.SQLDataType.VarChar, 15, False, False)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)
    End Sub
    Private Sub FUnitMast(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "Code", ClsMain.SQLDataType.VarChar, 6, , False)
        AgL.FSetColumnValue(MdlTable, "Unit", ClsMain.SQLDataType.VarChar, 6, True, False)
        AgL.FSetColumnValue(MdlTable, "U_Name", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)
    End Sub
    'Private Sub FItemCat(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
    '    Agl.FAddTable(MdlTable, StrTableName, StrModuleName)
    '    Agl.FSetColumnValue(MdlTable, "Code", ClsMain.SQLDataType.VarChar, 6, True, False)
    '    Agl.FSetColumnValue(MdlTable, "Name", ClsMain.SQLDataType.VarChar, 35, False, False)
    '    Agl.FSetColumnValue(MdlTable, "Type", ClsMain.SQLDataType.VarChar, 2)
    '    Agl.FSetColumnValue(MdlTable, "U_Name", ClsMain.SQLDataType.VarChar, 10)
    '    Agl.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
    '    Agl.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
    '    Agl.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)

    '    Agl.FSetFKeyValue(MdlTable, "Type", "Code", "ItemType")
    'End Sub
    'Private Sub FItemGroup(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
    '    Agl.FAddTable(MdlTable, StrTableName, StrModuleName)
    '    Agl.FSetColumnValue(MdlTable, "Code", ClsMain.SQLDataType.VarChar, 6, True, False)
    '    Agl.FSetColumnValue(MdlTable, "Name", ClsMain.SQLDataType.VarChar, 35, , False)
    '    Agl.FSetColumnValue(MdlTable, "CatCode", ClsMain.SQLDataType.VarChar, 6, , False)
    '    Agl.FSetColumnValue(MdlTable, "U_Name", ClsMain.SQLDataType.VarChar, 10)
    '    Agl.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
    '    Agl.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
    '    Agl.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)

    '    Agl.FSetFKeyValue(MdlTable, "CatCode", "Code", "ItemCat")
    'End Sub
    Private Sub FTariffMast(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "Code", ClsMain.SQLDataType.VarChar, 6, True, False)
        AgL.FSetColumnValue(MdlTable, "Name", ClsMain.SQLDataType.VarChar, 35, , False)
        AgL.FSetColumnValue(MdlTable, "ManualCode", ClsMain.SQLDataType.VarChar, 12, False, False)
        AgL.FSetColumnValue(MdlTable, "U_Name", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)
    End Sub
    Private Sub FVatCodeMast(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "Code", ClsMain.SQLDataType.VarChar, 6, True, False)
        AgL.FSetColumnValue(MdlTable, "Name", ClsMain.SQLDataType.VarChar, 35, , False)
        AgL.FSetColumnValue(MdlTable, "ManualCode", ClsMain.SQLDataType.VarChar, 12, False, False)
        AgL.FSetColumnValue(MdlTable, "U_Name", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)
    End Sub
    Private Sub FItemMast(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "Code", ClsMain.SQLDataType.VarChar, 10, True, False)
        AgL.FSetColumnValue(MdlTable, "Name", ClsMain.SQLDataType.VarChar, 50, False, False)
        AgL.FSetColumnValue(MdlTable, "ManualCode ", ClsMain.SQLDataType.VarChar, 21)
        AgL.FSetColumnValue(MdlTable, "ItemDesc", ClsMain.SQLDataType.VarChar, 250)
        AgL.FSetColumnValue(MdlTable, "ItemGroup", ClsMain.SQLDataType.VarChar, 6)
        AgL.FSetColumnValue(MdlTable, "TarrifSHNo", ClsMain.SQLDataType.VarChar, 6)
        AgL.FSetColumnValue(MdlTable, "VatCode", ClsMain.SQLDataType.VarChar, 6)
        AgL.FSetColumnValue(MdlTable, "ItemType", ClsMain.SQLDataType.VarChar, 2)
        AgL.FSetColumnValue(MdlTable, "SKU", ClsMain.SQLDataType.VarChar, 6)
        AgL.FSetColumnValue(MdlTable, "SecondryUnit", ClsMain.SQLDataType.VarChar, 6)
        AgL.FSetColumnValue(MdlTable, "ConversionRate", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MinStock", ClsMain.SQLDataType.Float, , , , "0")
        AgL.FSetColumnValue(MdlTable, "MaxStock", ClsMain.SQLDataType.Float, , , , "0")
        AgL.FSetColumnValue(MdlTable, "ReOrder", ClsMain.SQLDataType.Float, , , , "0")
        AgL.FSetColumnValue(MdlTable, "U_Name", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "SiteList", ClsMain.SQLDataType.VarChar, 500, False, False)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)

        AgL.FSetFKeyValue(MdlTable, "SecondryUnit", "Unit", "UnitMast")
        AgL.FSetFKeyValue(MdlTable, "SKU", "Unit", "UnitMast")
        AgL.FSetFKeyValue(MdlTable, "ItemGroup", "Code", "ItemGroup")
        AgL.FSetFKeyValue(MdlTable, "ItemType", "Code", "ItemType")
        AgL.FSetFKeyValue(MdlTable, "TarrifSHNo", "Code", "TariffMast")
        AgL.FSetFKeyValue(MdlTable, "VatCode", "Code", "VatCodeMast")
    End Sub
    Private Sub FSTaxTrn(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "DocId", ClsMain.SQLDataType.VarChar, 21, True, False)
        AgL.FSetColumnValue(MdlTable, "V_No", ClsMain.SQLDataType.VarChar, 8, , False)
        AgL.FSetColumnValue(MdlTable, "RecId", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "V_Type", ClsMain.SQLDataType.VarChar, 5, , False)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", ClsMain.SQLDataType.VarChar, 5, , False)
        AgL.FSetColumnValue(MdlTable, "V_Date", ClsMain.SQLDataType.SmallDateTime, , False)
        AgL.FSetColumnValue(MdlTable, "PtyBillNo", ClsMain.SQLDataType.VarChar, 20)
        AgL.FSetColumnValue(MdlTable, "PtyBillDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Consignee", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Consignor", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PartyCode", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "VehicleNo", ClsMain.SQLDataType.VarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Description", ClsMain.SQLDataType.VarChar, 100)
        AgL.FSetColumnValue(MdlTable, "FromPlace", ClsMain.SQLDataType.VarChar, 6)
        AgL.FSetColumnValue(MdlTable, "ToPlace", ClsMain.SQLDataType.VarChar, 6)
        AgL.FSetColumnValue(MdlTable, "ConsignorBill", ClsMain.SQLDataType.VarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ConsigneeBill", ClsMain.SQLDataType.VarChar, 50)

        AgL.FSetColumnValue(MdlTable, "TDSCategory", ClsMain.SQLDataType.VarChar, 6)
        AgL.FSetColumnValue(MdlTable, "TDSCalOn", ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "GAmount", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Exempted", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TaxableAmt", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ServiceTaxAmt", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ECessAmt", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "SHCessAmt", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "NetAmount", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "VrRefDocId", ClsMain.SQLDataType.VarChar, 21)
        AgL.FSetColumnValue(MdlTable, "VrRef_SNo", ClsMain.SQLDataType.SmallInt)

        AgL.FSetColumnValue(MdlTable, "Remark", ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "Site_Code", ClsMain.SQLDataType.VarChar, 2, , False)
        AgL.FSetColumnValue(MdlTable, "PreparedBy", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)


        AgL.FSetFKeyValue(MdlTable, "VrRefDocId,VrRef_SNo", "DocId,V_SNo", "Ledger")
        AgL.FSetFKeyValue(MdlTable, "Consignee", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "Consignor", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "PartyCode", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "TDSCategory", "Code", "TDSCat")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "FromPlace", "CityCode", "City")
        AgL.FSetFKeyValue(MdlTable, "ToPlace", "CityCode", "City")
    End Sub
    Private Sub FChequeMast(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "BCode", ClsMain.SQLDataType.VarChar, 10, True, False)
        AgL.FSetColumnValue(MdlTable, "ChequeNo", ClsMain.SQLDataType.VarChar, 20, True, False)
        AgL.FSetColumnValue(MdlTable, "V_Date", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_Name", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)

        AgL.FSetFKeyValue(MdlTable, "BCode", "SubCode", "SubGroup")
    End Sub
    Private Sub FAcGroupPositioning(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)

        AgL.FSetColumnValue(MdlTable, "GroupCode", ClsMain.SQLDataType.VarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "V_SNo", ClsMain.SQLDataType.SmallInt)
        AgL.FSetColumnValue(MdlTable, "Level", ClsMain.SQLDataType.TinyInt)
        AgL.FSetColumnValue(MdlTable, "ExpandGroup", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)

    End Sub
    Private Sub FDataTrfd(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", ClsMain.SQLDataType.VarChar, 21, True)
        AgL.FSetColumnValue(MdlTable, "U_Name", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)

    End Sub
    Private Sub FZoneMast(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "Code", ClsMain.SQLDataType.VarChar, 6, True, False)
        AgL.FSetColumnValue(MdlTable, "Name", ClsMain.SQLDataType.VarChar, 50)
        AgL.FSetColumnValue(MdlTable, "U_Name", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)
    End Sub
    Private Sub FLedgerGroup(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "Code", ClsMain.SQLDataType.VarChar, 10, True, False)
        AgL.FSetColumnValue(MdlTable, "Name", ClsMain.SQLDataType.VarChar, 100)
        AgL.FSetColumnValue(MdlTable, "U_Name", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)
    End Sub
    Private Sub FAcFilteration(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "V_Type", ClsMain.SQLDataType.VarChar, 5)
        AgL.FSetColumnValue(MdlTable, "Nature", ClsMain.SQLDataType.VarChar, 10)
    End Sub
    Private Sub FNarrationMast(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "Code", ClsMain.SQLDataType.VarChar, 6, True, False)
        AgL.FSetColumnValue(MdlTable, "Name", ClsMain.SQLDataType.VarChar, 100)
        AgL.FSetColumnValue(MdlTable, "U_Name", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)
    End Sub
    Private Sub FLedger(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "DocId", ClsMain.SQLDataType.VarChar, 21, True, False)
        AgL.FSetColumnValue(MdlTable, "V_No", ClsMain.SQLDataType.VarChar, 8, , False)
        AgL.FSetColumnValue(MdlTable, "RecId", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "V_Type", ClsMain.SQLDataType.VarChar, 5, , False)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", ClsMain.SQLDataType.VarChar, 5, , False)
        AgL.FSetColumnValue(MdlTable, "V_Date", ClsMain.SQLDataType.SmallDateTime, , False)
        AgL.FSetColumnValue(MdlTable, "V_SNo", ClsMain.SQLDataType.SmallInt, , True, False)
        AgL.FSetColumnValue(MdlTable, "SubCode", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ContraSub", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ContraText", ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "AmtDr", ClsMain.SQLDataType.Float, , , , "0")
        AgL.FSetColumnValue(MdlTable, "AmtCr", ClsMain.SQLDataType.Float, , , , "0")
        AgL.FSetColumnValue(MdlTable, "Chq_No", ClsMain.SQLDataType.VarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Chq_Date", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Clg_Date", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "TDSCategory", ClsMain.SQLDataType.VarChar, 6)
        AgL.FSetColumnValue(MdlTable, "TDSDeductFrom", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TdsDesc", ClsMain.SQLDataType.VarChar, 4)
        AgL.FSetColumnValue(MdlTable, "OrignalAmt", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TdsOnAmt", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TdsPer", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Tds_Of_V_Sno", ClsMain.SQLDataType.SmallInt)
        AgL.FSetColumnValue(MdlTable, "FormulaString", ClsMain.SQLDataType.VarChar, 100)
        AgL.FSetColumnValue(MdlTable, "CostCenter", ClsMain.SQLDataType.VarChar, 6)
        AgL.FSetColumnValue(MdlTable, "System_Generated", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Narration", ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "Site_Code", ClsMain.SQLDataType.VarChar, 2, , False)
        AgL.FSetColumnValue(MdlTable, "U_Name", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)


        AgL.FSetFKeyValue(MdlTable, "TDSDeductFrom", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "Subcode", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "ContraSub", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "TdsDesc", "Code", "TDSCat_Description")
        AgL.FSetFKeyValue(MdlTable, "TDSCategory", "Code", "TDSCat")
        AgL.FSetFKeyValue(MdlTable, "CostCenter", "Code", "CostCenterMast")
    End Sub
    Private Sub FLedgerItemAdj(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)

        AgL.FSetColumnValue(MdlTable, "DocId", ClsMain.SQLDataType.VarChar, 21, True)
        AgL.FSetColumnValue(MdlTable, "V_SNo", ClsMain.SQLDataType.SmallInt, , True)
        AgL.FSetColumnValue(MdlTable, "ItemCode", ClsMain.SQLDataType.VarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "Quantity", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Amount", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Remark", ClsMain.SQLDataType.VarChar, 100)

        AgL.FSetFKeyValue(MdlTable, "ItemCode", "Code", "ItemMast")
    End Sub
    Private Sub FSubGroup(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)

        AgL.FSetColumnValue(MdlTable, "SubCode", ClsMain.SQLDataType.VarChar, 10, True, False)
        AgL.FSetColumnValue(MdlTable, "SiteList", ClsMain.SQLDataType.VarChar, 500, False, False)
        AgL.FSetColumnValue(MdlTable, "Name", ClsMain.SQLDataType.VarChar, 50, , False)
        AgL.FSetColumnValue(MdlTable, "GroupCode", ClsMain.SQLDataType.VarChar, 10, , False)
        AgL.FSetColumnValue(MdlTable, "GroupNature", ClsMain.SQLDataType.VarChar, 1, , False)
        AgL.FSetColumnValue(MdlTable, "LedgerGroup", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ManualCode", ClsMain.SQLDataType.VarChar, 10, , False)
        AgL.FSetColumnValue(MdlTable, "Distributor", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Nature", ClsMain.SQLDataType.VarChar, 11, , False)
        AgL.FSetColumnValue(MdlTable, "ADD1", ClsMain.SQLDataType.VarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ADD2", ClsMain.SQLDataType.VarChar, 50)
        AgL.FSetColumnValue(MdlTable, "CityCode", ClsMain.SQLDataType.VarChar, 6)
        AgL.FSetColumnValue(MdlTable, "CountryCode", ClsMain.SQLDataType.VarChar, 6)
        AgL.FSetColumnValue(MdlTable, "PIN", ClsMain.SQLDataType.VarChar, 6)
        AgL.FSetColumnValue(MdlTable, "Phone", ClsMain.SQLDataType.VarChar, 35)
        AgL.FSetColumnValue(MdlTable, "Mobile", ClsMain.SQLDataType.VarChar, 35)
        AgL.FSetColumnValue(MdlTable, "FAX", ClsMain.SQLDataType.VarChar, 35)
        AgL.FSetColumnValue(MdlTable, "Zone", ClsMain.SQLDataType.VarChar, 6)

        AgL.FSetColumnValue(MdlTable, "DuplicateTIN", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "TINNo", ClsMain.SQLDataType.VarChar, 20)
        AgL.FSetColumnValue(MdlTable, "CSTNo", ClsMain.SQLDataType.VarChar, 40)
        AgL.FSetColumnValue(MdlTable, "STNo", ClsMain.SQLDataType.VarChar, 40)
        AgL.FSetColumnValue(MdlTable, "EMail", ClsMain.SQLDataType.VarChar, 40)
        AgL.FSetColumnValue(MdlTable, "PAN", ClsMain.SQLDataType.VarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Location", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "CreditLimit", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "DueDays", ClsMain.SQLDataType.SmallInt)
        AgL.FSetColumnValue(MdlTable, "TDS_Catg", ClsMain.SQLDataType.VarChar, 6)
        AgL.FSetColumnValue(MdlTable, "PolicyNo", ClsMain.SQLDataType.VarChar, 50)

        AgL.FSetColumnValue(MdlTable, "IECCode", ClsMain.SQLDataType.VarChar, 35)
        AgL.FSetColumnValue(MdlTable, "ECCCode", ClsMain.SQLDataType.VarChar, 35)
        AgL.FSetColumnValue(MdlTable, "Excise", ClsMain.SQLDataType.VarChar, 35)
        AgL.FSetColumnValue(MdlTable, "Range", ClsMain.SQLDataType.VarChar, 35)
        AgL.FSetColumnValue(MdlTable, "Division", ClsMain.SQLDataType.VarChar, 35)
        AgL.FSetColumnValue(MdlTable, "PartyType", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "PartyCat", ClsMain.SQLDataType.VarChar, 1)

        AgL.FSetColumnValue(MdlTable, "FBTOnPer", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "FBTPer", ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "ContactPerson", ClsMain.SQLDataType.VarChar, 35)
        AgL.FSetColumnValue(MdlTable, "Remark", ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "ActiveYN", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "CostCenter", ClsMain.SQLDataType.VarChar, 6)
        AgL.FSetColumnValue(MdlTable, "Site_Code", ClsMain.SQLDataType.VarChar, 2)
        AgL.FSetColumnValue(MdlTable, "U_Name", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)

        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "GroupCode", "GroupCode", "AcGroup")
        AgL.FSetFKeyValue(MdlTable, "LedgerGroup", "Code", "LedgerGroup")
        AgL.FSetFKeyValue(MdlTable, "Distributor", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "Zone", "Code", "ZoneMast")
        AgL.FSetFKeyValue(MdlTable, "CityCode", "CityCode", "City")
        AgL.FSetFKeyValue(MdlTable, "CountryCode", "Code", "Country")
        AgL.FSetFKeyValue(MdlTable, "CostCenter", "Code", "CostCenterMast")
        AgL.FSetFKeyValue(MdlTable, "TDS_Catg", "Code", "TdsCat")
    End Sub
    Private Sub FAcGroup(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "GroupCode", ClsMain.SQLDataType.VarChar, 10, True, False)
        AgL.FSetColumnValue(MdlTable, "SNo", ClsMain.SQLDataType.SmallInt)
        AgL.FSetColumnValue(MdlTable, "GroupName", ClsMain.SQLDataType.VarChar, 50, , False)
        AgL.FSetColumnValue(MdlTable, "GroupUnder", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Nature", ClsMain.SQLDataType.VarChar, 15)
        AgL.FSetColumnValue(MdlTable, "SysGroup", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "GroupNature", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "ContraGroupName", ClsMain.SQLDataType.VarChar, 50)
        AgL.FSetColumnValue(MdlTable, "U_Name", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)

        AgL.FSetFKeyValue(MdlTable, "GroupUnder", "GroupCode", "AcGroup")
    End Sub
    Private Sub FAssetGroupMast(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "Code", ClsMain.SQLDataType.VarChar, 6, True, False)
        AgL.FSetColumnValue(MdlTable, "Name", ClsMain.SQLDataType.VarChar, 50, , False)
        AgL.FSetColumnValue(MdlTable, "ManualCode", ClsMain.SQLDataType.VarChar, 10, , False)
        AgL.FSetColumnValue(MdlTable, "AcCode", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Depreciation", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "PreparedBy", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)

        AgL.FSetFKeyValue(MdlTable, "ACcode", "SubCode", "SubGroup")
    End Sub
    Private Sub FAssetMast(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "DocId", ClsMain.SQLDataType.VarChar, 21, True, False)
        AgL.FSetColumnValue(MdlTable, "V_Type", ClsMain.SQLDataType.VarChar, 5, , False)
        AgL.FSetColumnValue(MdlTable, "Asset_SYS_ID", ClsMain.SQLDataType.VarChar, 10, , False)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", ClsMain.SQLDataType.VarChar, 5, , False)
        AgL.FSetColumnValue(MdlTable, "Name", ClsMain.SQLDataType.VarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Asset_Manual_ID", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "AssetGroup", ClsMain.SQLDataType.VarChar, 6)
        AgL.FSetColumnValue(MdlTable, "Site_Code", ClsMain.SQLDataType.VarChar, 2)
        AgL.FSetColumnValue(MdlTable, "PreparedBy", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)

        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "AssetGroup", "Code", "AssetGroupMast")
        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
    End Sub

    Private Sub FAssetTransaction(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "DocId", ClsMain.SQLDataType.VarChar, 21, True, False)
        AgL.FSetColumnValue(MdlTable, "V_Type", ClsMain.SQLDataType.VarChar, 5, , False)
        AgL.FSetColumnValue(MdlTable, "V_No", ClsMain.SQLDataType.VarChar, 8, , False)
        AgL.FSetColumnValue(MdlTable, "RecId", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", ClsMain.SQLDataType.VarChar, 5, , False)
        AgL.FSetColumnValue(MdlTable, "V_Date", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Asset", ClsMain.SQLDataType.VarChar, 21)
        AgL.FSetColumnValue(MdlTable, "Amount", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Remark", ClsMain.SQLDataType.VarChar, 500)
        AgL.FSetColumnValue(MdlTable, "Site_Code", ClsMain.SQLDataType.VarChar, 2)
        AgL.FSetColumnValue(MdlTable, "PreparedBy", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)

        AgL.FSetFKeyValue(MdlTable, "Asset", "DocId", "AssetMast")
        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
    End Sub
    Private Sub FBankReconsilation(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "DocId", ClsMain.SQLDataType.VarChar, 21, True, False)
        AgL.FSetColumnValue(MdlTable, "V_Type", ClsMain.SQLDataType.VarChar, 5, , False)
        AgL.FSetColumnValue(MdlTable, "V_No", ClsMain.SQLDataType.VarChar, 8, , False)
        AgL.FSetColumnValue(MdlTable, "RecId", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "V_SNo", ClsMain.SQLDataType.SmallInt, , True, False)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", ClsMain.SQLDataType.VarChar, 5, , False)
        AgL.FSetColumnValue(MdlTable, "V_Date", ClsMain.SQLDataType.SmallDateTime, , , False)
        AgL.FSetColumnValue(MdlTable, "BankCode", ClsMain.SQLDataType.VarChar, 10, , False)
        AgL.FSetColumnValue(MdlTable, "AmtDr", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "AmtCr", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TType", ClsMain.SQLDataType.VarChar, 2)
        AgL.FSetColumnValue(MdlTable, "Chq_No", ClsMain.SQLDataType.VarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Clg_Date", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Site_Code", ClsMain.SQLDataType.VarChar, 2)
        AgL.FSetColumnValue(MdlTable, "U_Name", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)

        AgL.FSetFKeyValue(MdlTable, "BankCode", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
    End Sub
    Private Sub FCurrency(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "Code", ClsMain.SQLDataType.VarChar, 6, True, False)
        AgL.FSetColumnValue(MdlTable, "Name", ClsMain.SQLDataType.VarChar, 8, , False)
        AgL.FSetColumnValue(MdlTable, "Discription", ClsMain.SQLDataType.VarChar, 15)
        AgL.FSetColumnValue(MdlTable, "SmallCurrency", ClsMain.SQLDataType.VarChar, 8)
        AgL.FSetColumnValue(MdlTable, "Rate", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ModifiedByRate", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_Name", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)

    End Sub
    Private Sub FTDSCat_Description(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "Code", ClsMain.SQLDataType.VarChar, 4, True, False)
        AgL.FSetColumnValue(MdlTable, "Name", ClsMain.SQLDataType.VarChar, 25, , False)
        AgL.FSetColumnValue(MdlTable, "U_Name", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)

    End Sub
    Private Sub FTdsCat(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "Code", ClsMain.SQLDataType.VarChar, 6, True, False)
        AgL.FSetColumnValue(MdlTable, "Name", ClsMain.SQLDataType.VarChar, 35, , False)
        AgL.FSetColumnValue(MdlTable, "U_Name", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)

    End Sub
    Private Sub FTdsCat_Det(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "Code", ClsMain.SQLDataType.VarChar, 6, True, False)
        AgL.FSetColumnValue(MdlTable, "SrNo", ClsMain.SQLDataType.SmallInt, , True, False)
        AgL.FSetColumnValue(MdlTable, "AcCode", ClsMain.SQLDataType.VarChar, 10, , False)
        AgL.FSetColumnValue(MdlTable, "Percentage", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "FormulaString", ClsMain.SQLDataType.VarChar, 100)
        AgL.FSetColumnValue(MdlTable, "TdsDesc", ClsMain.SQLDataType.VarChar, 4)
        AgL.FSetColumnValue(MdlTable, "U_Name", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)

        AgL.FSetFKeyValue(MdlTable, "AcCode", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "TdsDesc", "Code", "TDSCat_Description")

    End Sub
    Private Sub FCostCenterMast(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "Code", ClsMain.SQLDataType.VarChar, 6, True, False)
        AgL.FSetColumnValue(MdlTable, "Name", ClsMain.SQLDataType.VarChar, 30, , False)
        AgL.FSetColumnValue(MdlTable, "U_Name", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)

    End Sub
    Private Sub FBudget(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "DocId", ClsMain.SQLDataType.VarChar, 21, True, False)
        AgL.FSetColumnValue(MdlTable, "V_Type", ClsMain.SQLDataType.VarChar, 5, , False)
        AgL.FSetColumnValue(MdlTable, "V_No", ClsMain.SQLDataType.VarChar, 8, , False)
        AgL.FSetColumnValue(MdlTable, "RecId", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", ClsMain.SQLDataType.VarChar, 5, , False)
        AgL.FSetColumnValue(MdlTable, "V_Date", ClsMain.SQLDataType.SmallDateTime, , False)
        AgL.FSetColumnValue(MdlTable, "Name", ClsMain.SQLDataType.VarChar, 30, , False)
        AgL.FSetColumnValue(MdlTable, "DateFrom", ClsMain.SQLDataType.SmallDateTime, , False)
        AgL.FSetColumnValue(MdlTable, "DateTo", ClsMain.SQLDataType.SmallDateTime, , False)
        AgL.FSetColumnValue(MdlTable, "PreparedBy", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", ClsMain.SQLDataType.VarChar, 10)

        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", ClsMain.SQLDataType.VarChar, 2, , False)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)

        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "V_Type", "V_Type", "Voucher_Type")
    End Sub
    Private Sub FBudgetDet(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)

        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "DocId", ClsMain.SQLDataType.VarChar, 21, True, False)
        AgL.FSetColumnValue(MdlTable, "SNo", ClsMain.SQLDataType.SmallInt, , True, False)
        AgL.FSetColumnValue(MdlTable, "CostCenter", ClsMain.SQLDataType.VarChar, 6, , False)
        AgL.FSetColumnValue(MdlTable, "Amount", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Site_Code", ClsMain.SQLDataType.VarChar, 2, , False)

        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        AgL.FSetFKeyValue(MdlTable, "CostCenter", "Code", "CostCenterMast")
    End Sub
    Private Sub FCity(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)

        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "CityCode", ClsMain.SQLDataType.VarChar, 6, True, False)
        AgL.FSetColumnValue(MdlTable, "CityName", ClsMain.SQLDataType.VarChar, 25, , False)
        AgL.FSetColumnValue(MdlTable, "State", ClsMain.SQLDataType.VarChar, 35)
        AgL.FSetColumnValue(MdlTable, "StdCode", ClsMain.SQLDataType.VarChar, 15)
        AgL.FSetColumnValue(MdlTable, "U_Name", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)

    End Sub

    Private Sub FCountry(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)

        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "Code", ClsMain.SQLDataType.VarChar, 6, True, False)
        AgL.FSetColumnValue(MdlTable, "Name", ClsMain.SQLDataType.VarChar, 25, , False)
        AgL.FSetColumnValue(MdlTable, "CurrencyCode", ClsMain.SQLDataType.VarChar, 6)
        AgL.FSetColumnValue(MdlTable, "U_Name", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)

    End Sub
    Private Sub FAcGroupPath(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)

        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "GroupCode", ClsMain.SQLDataType.VarChar, 10, True, False)
        AgL.FSetColumnValue(MdlTable, "SNo", ClsMain.SQLDataType.SmallInt, , True, False)
        AgL.FSetColumnValue(MdlTable, "GroupUnder", ClsMain.SQLDataType.VarChar, 10)

        AgL.FSetFKeyValue(MdlTable, "GroupCode", "GroupCode", "AcGroup")
        AgL.FSetFKeyValue(MdlTable, "GroupUnder", "GroupCode", "AcGroup")
    End Sub
    Private Sub FEnviro_Accounts(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "ID", ClsMain.SQLDataType.VarChar, 1, True, False)
        AgL.FSetColumnValue(MdlTable, "MaintainTDS", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "AutoPosting", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "VRNumberSystem", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "TDSROff", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "SrvTaxAdjRefType", ClsMain.SQLDataType.VarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)
    End Sub
    Private Sub FLedgerAdj(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "Vr_DocId", ClsMain.SQLDataType.VarChar, 21, , False)
        AgL.FSetColumnValue(MdlTable, "Vr_V_SNo", ClsMain.SQLDataType.SmallInt, , , False)
        AgL.FSetColumnValue(MdlTable, "Adj_DocID", ClsMain.SQLDataType.VarChar, 21, , False)
        AgL.FSetColumnValue(MdlTable, "Adj_V_SNo", ClsMain.SQLDataType.SmallInt, , , False)
        AgL.FSetColumnValue(MdlTable, "Amount", ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Site_Code", ClsMain.SQLDataType.VarChar, 2, , False)

        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
        If UCase("LedgerAdj") = UCase(Trim(StrTableName)) Then
            AgL.FSetFKeyValue(MdlTable, "Adj_DocId,Adj_V_SNo", "DocId,V_SNo", "Ledger")
        Else
            AgL.FSetFKeyValue(MdlTable, "Adj_DocId,Adj_V_SNo", "DocId,V_SNo", "Ledger_Temp")
        End If
    End Sub
    Private Sub FLedgerM(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, StrModuleName)
        AgL.FSetColumnValue(MdlTable, "DocId", ClsMain.SQLDataType.VarChar, 21, True, False)
        AgL.FSetColumnValue(MdlTable, "V_No", ClsMain.SQLDataType.VarChar, 8, , False)
        AgL.FSetColumnValue(MdlTable, "RecId", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "V_Type", ClsMain.SQLDataType.VarChar, 5, , False)
        AgL.FSetColumnValue(MdlTable, "V_Prefix", ClsMain.SQLDataType.VarChar, 5, , False)
        AgL.FSetColumnValue(MdlTable, "V_Date", ClsMain.SQLDataType.SmallDateTime, , False)
        AgL.FSetColumnValue(MdlTable, "SubCode", ClsMain.SQLDataType.VarChar, 10)

        AgL.FSetColumnValue(MdlTable, "Narration", ClsMain.SQLDataType.VarChar, 255)
        AgL.FSetColumnValue(MdlTable, "PreparedBY", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PostedBY", ClsMain.SQLDataType.VarChar, 10)

        AgL.FSetColumnValue(MdlTable, "Site_Code", ClsMain.SQLDataType.VarChar, 2, , False)
        AgL.FSetColumnValue(MdlTable, "U_Name", ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", ClsMain.SQLDataType.VarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Transfered", ClsMain.SQLDataType.VarChar, 1)

        AgL.FSetFKeyValue(MdlTable, "Subcode", "SubCode", "SubGroup")
        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
    End Sub
    Public Sub FInitilizer()
        Dim SQLCmd As New Object
        Dim StrSQL As String

        Try
            SQLCmd = AgL.GCn.CreateCommand

            '====================================================================
            '==================== Voucher Type Entry Starts =====================
            '====================================================================
            If Not CMain.FGetMaxNo("Select Count(*) As Cnt From Voucher_Type VT Where VT.V_Type='STAX'", AgL.GCn) > 0 Then
                SQLCmd.CommandText = "INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, ModifiedBy, PreparedBy, U_EntDt, U_AE,MnuName,MnuText,MnuAttachedInModule,AuditAllowed) "
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('FA', 'STAX', 'STAX', 'Inward GTA', 'STAX', 'Y', 'SA', 'SA', '13/Jun/2005 12:00:00 AM', 'E','MnuServiceTaxEntry','Service Tax Entry','Accounts','Y') "
                SQLCmd.ExecuteNonQuery()
            End If

            If Not CMain.FGetMaxNo("Select Count(*) As Cnt From Voucher_Type VT Where VT.V_Type='STAXR'", AgL.GCn) > 0 Then
                SQLCmd.CommandText = "INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, ModifiedBy, PreparedBy, U_EntDt, U_AE,MnuName,MnuText,MnuAttachedInModule,AuditAllowed) "
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('FA', 'STAX', 'STAXR', 'Inward GTA Reverse', 'STAXR', 'Y', 'SA', 'SA', '13/Jun/2005 12:00:00 AM', 'E','MnuServiceTaxEntry','Service Tax Entry','Accounts','Y') "
                SQLCmd.ExecuteNonQuery()
            End If

            If Not CMain.FGetMaxNo("Select Count(*) As Cnt From Voucher_Type VT Where VT.V_Type='STXOW'", AgL.GCn) > 0 Then
                SQLCmd.CommandText = "INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, ModifiedBy, PreparedBy, U_EntDt, U_AE,MnuName,MnuText,MnuAttachedInModule,AuditAllowed) "
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('FA', 'STAX', 'STXOW', 'Outward GTA', 'STXOW', 'Y', 'SA', 'SA', '13/Jun/2005 12:00:00 AM', 'E','MnuServiceTaxEntry','Service Tax Entry','Accounts','Y') "
                SQLCmd.ExecuteNonQuery()
            End If

            If Not CMain.FGetMaxNo("Select Count(*) As Cnt From Voucher_Type VT Where VT.V_Type='STXOR'", AgL.GCn) > 0 Then
                SQLCmd.CommandText = "INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, ModifiedBy, PreparedBy, U_EntDt, U_AE,MnuName,MnuText,MnuAttachedInModule,AuditAllowed) "
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('FA', 'STAX', 'STXOR', 'Outward GTA Reverse', 'STXOR', 'Y', 'SA', 'SA', '13/Jun/2005 12:00:00 AM', 'E','MnuServiceTaxEntry','Service Tax Entry','Accounts','Y') "
                SQLCmd.ExecuteNonQuery()
            End If

            If Not CMain.FGetMaxNo("Select Count(*) As Cnt From Voucher_Type VT Where VT.V_Type='STXNG'", AgL.GCn) > 0 Then
                SQLCmd.CommandText = "INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, ModifiedBy, PreparedBy, U_EntDt, U_AE,MnuName,MnuText,MnuAttachedInModule,AuditAllowed) "
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('FA', 'STAX', 'STXNG', 'Non GTA', 'STXNG', 'Y', 'SA', 'SA', '13/Jun/2005 12:00:00 AM', 'E','MnuServiceTaxEntry','Service Tax Entry','Accounts','Y') "
                SQLCmd.ExecuteNonQuery()
            End If

            If Not CMain.FGetMaxNo("Select Count(*) As Cnt From Voucher_Type VT Where VT.V_Type='STXNR'", AgL.GCn) > 0 Then
                SQLCmd.CommandText = "INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, ModifiedBy, PreparedBy, U_EntDt, U_AE,MnuName,MnuText,MnuAttachedInModule,AuditAllowed) "
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('FA', 'STAX', 'STXNR', 'Non GTA Reverse', 'STXNR', 'Y', 'SA', 'SA', '13/Jun/2005 12:00:00 AM', 'E','MnuServiceTaxEntry','Service Tax Entry','Accounts','Y') "
                SQLCmd.ExecuteNonQuery()
            End If

            If Not CMain.FGetMaxNo("Select Count(*) As Cnt From Voucher_Type VT Where VT.V_Type='ASSET'", AgL.GCn) > 0 Then
                SQLCmd.CommandText = "INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, ModifiedBy, PreparedBy, U_EntDt, U_AE,MnuName,MnuText,MnuAttachedInModule) "
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('FA', 'ASSET', 'ASSET', 'Fixed Asset', 'ASSET', 'Y', 'sa', 'sa', '07/Sep/2008 12:00:00 AM', 'A','','','')"
                SQLCmd.ExecuteNonQuery()
            End If

            If Not CMain.FGetMaxNo("Select Count(*) As Cnt From Voucher_Type VT Where VT.V_Type='ASTAP'", AgL.GCn) > 0 Then
                SQLCmd.CommandText = "INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, ModifiedBy, PreparedBy, U_EntDt, U_AE,MnuName,MnuText,MnuAttachedInModule) "
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('FA', 'ASTTR', 'ASTAP', 'Appreciation Asset', 'ASTAP', 'Y', 'sa', 'sa', '07/Sep/2008 12:00:00 AM', 'A','','','')"
                SQLCmd.ExecuteNonQuery()
            End If

            If Not CMain.FGetMaxNo("Select Count(*) As Cnt From Voucher_Type VT Where VT.V_Type='ASTOP'", AgL.GCn) > 0 Then
                SQLCmd.CommandText = "INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, ModifiedBy, PreparedBy, U_EntDt, U_AE,MnuName,MnuText,MnuAttachedInModule) "
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('FA', 'ASTTR', 'ASTOP', 'Opening Asset', 'ASTOP', 'Y', 'sa', 'sa', '07/Sep/2008 12:00:00 AM', 'A','','','')"
                SQLCmd.ExecuteNonQuery()
            End If

            If Not CMain.FGetMaxNo("Select Count(*) As Cnt From Voucher_Type VT Where VT.V_Type='ASTPR'", AgL.GCn) > 0 Then
                SQLCmd.CommandText = "INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, ModifiedBy, PreparedBy, U_EntDt, U_AE,MnuName,MnuText,MnuAttachedInModule) "
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('FA', 'ASTTR', 'ASTPR', 'Purchase Asset', 'ASTPR', 'Y', 'sa', 'sa', '07/Sep/2008 12:00:00 AM', 'A','','','')"
                SQLCmd.ExecuteNonQuery()
            End If

            If Not CMain.FGetMaxNo("Select Count(*) As Cnt From Voucher_Type VT Where VT.V_Type='ASTSL'", AgL.GCn) > 0 Then
                SQLCmd.CommandText = "INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, ModifiedBy, PreparedBy, U_EntDt, U_AE,MnuName,MnuText,MnuAttachedInModule) "
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('FA', 'ASTTR', 'ASTSL', 'Sale Asset', 'ASTSL', 'Y', 'sa', 'sa', '07/Sep/2008 12:00:00 AM', 'A','','','')"
                SQLCmd.ExecuteNonQuery()
            End If

            If Not CMain.FGetMaxNo("Select Count(*) As Cnt From Voucher_Type VT Where VT.V_Type='BDGT'", AgL.GCn) > 0 Then
                SQLCmd.CommandText = "INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, ModifiedBy, PreparedBy, U_EntDt, U_AE,MnuName,MnuText,MnuAttachedInModule) "
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('FA', 'BDGT', 'BDGT', 'Budget Schedule', 'BDGT', 'Y', 'sa', 'sa', '01/Sep/2008 12:00:00 AM', 'A','','','')"
                SQLCmd.ExecuteNonQuery()
            End If

            If Not CMain.FGetMaxNo("Select Count(*) As Cnt From Voucher_Type VT Where VT.V_Type='CN'", AgL.GCn) > 0 Then
                SQLCmd.CommandText = "INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, ModifiedBy, PreparedBy, U_EntDt, U_AE,MnuName,MnuText,MnuAttachedInModule,AuditAllowed) "
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('FA', 'JV', 'CN', 'Credit Note', 'CN', 'Y', 'SA', 'SA', '01/Jan/1900 12:00:00 AM', 'A','MnuVoucherEntry','Voucher Entry','Accounts','Y')"
                SQLCmd.ExecuteNonQuery()
            End If

            If Not CMain.FGetMaxNo("Select Count(*) As Cnt From Voucher_Type VT Where VT.V_Type='CPV'", AgL.GCn) > 0 Then
                SQLCmd.CommandText = "INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, ModifiedBy, PreparedBy, U_EntDt, U_AE,MnuName,MnuText,MnuAttachedInModule,AuditAllowed) "
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('FA', 'PMT', 'CPV', 'Cash Payment', 'CP', 'Y', 'SA', 'SA', '29/May/2007 12:00:00 AM', 'E','MnuVoucherEntry','Voucher Entry','Accounts','Y')"
                SQLCmd.ExecuteNonQuery()
            End If

            If Not CMain.FGetMaxNo("Select Count(*) As Cnt From Voucher_Type VT Where VT.V_Type='CRV'", AgL.GCn) > 0 Then
                SQLCmd.CommandText = "INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, ModifiedBy, PreparedBy, U_EntDt, U_AE,MnuName,MnuText,MnuAttachedInModule,AuditAllowed) "
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('FA', 'RCT', 'CRV', 'Cash Receipt', 'CR', 'Y', 'SA', 'SA', '11/Apr/2007 12:00:00 AM', 'E','MnuVoucherEntry','Voucher Entry','Accounts','Y')"
                SQLCmd.ExecuteNonQuery()
            End If

            If Not CMain.FGetMaxNo("Select Count(*) As Cnt From Voucher_Type VT Where VT.V_Type='DN'", AgL.GCn) > 0 Then
                SQLCmd.CommandText = "INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, ModifiedBy, PreparedBy, U_EntDt, U_AE,MnuName,MnuText,MnuAttachedInModule,AuditAllowed) "
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('FA', 'JV', 'DN', 'Debit Note', 'DN', 'Y', 'SA', 'SA', '01/Jan/1900 12:00:00 AM', 'A','MnuVoucherEntry','Voucher Entry','Accounts','Y')"
                SQLCmd.ExecuteNonQuery()
            End If

            If Not CMain.FGetMaxNo("Select Count(*) As Cnt From Voucher_Type VT Where VT.V_Type='JV'", AgL.GCn) > 0 Then
                SQLCmd.CommandText = "INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, ModifiedBy, PreparedBy, U_EntDt, U_AE,MnuName,MnuText,MnuAttachedInModule,AuditAllowed) "
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('FA', 'JV', 'JV', 'Journal', 'JV', 'Y', 'SA', 'SA', '27/Mar/2007 12:00:00 AM', 'E','MnuVoucherEntry','Voucher Entry','Accounts','Y')"
                SQLCmd.ExecuteNonQuery()
            End If


            If Not CMain.FGetMaxNo("Select Count(*) As Cnt From Voucher_Type VT Where VT.V_Type='CNTRA'", AgL.GCn) > 0 Then
                SQLCmd.CommandText = "INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, ModifiedBy, PreparedBy, U_EntDt, U_AE,MnuName,MnuText,MnuAttachedInModule,AuditAllowed) "
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('FA', 'JV', 'CNTRA', 'Contra', 'CNTRA', 'Y', 'sa', 'sa', '30/Aug/2008 12:00:00 AM', 'A','MnuVoucherEntry','Voucher Entry','Accounts','Y')"
                SQLCmd.ExecuteNonQuery()
            End If

            If Not CMain.FGetMaxNo("Select Count(*) As Cnt From Voucher_Type VT Where VT.V_Type='PMT'", AgL.GCn) > 0 Then
                SQLCmd.CommandText = "INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, ModifiedBy, PreparedBy, U_EntDt, U_AE,MnuName,MnuText,MnuAttachedInModule,AuditAllowed) "
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('FA', 'PMT', 'PMT', 'Bank Payment', 'PMT', 'Y', 'SA', 'SA', '29/May/2007 12:00:00 AM', 'E','MnuVoucherEntry','Voucher Entry','Accounts','Y')"
                SQLCmd.ExecuteNonQuery()
            End If

            If Not CMain.FGetMaxNo("Select Count(*) As Cnt From Voucher_Type VT Where VT.V_Type='RCT'", AgL.GCn) > 0 Then
                SQLCmd.CommandText = "INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, ModifiedBy, PreparedBy, U_EntDt, U_AE,MnuName,MnuText,MnuAttachedInModule,AuditAllowed) "
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('FA', 'RCT', 'RCT', 'Bank Receipt', 'RCT', 'Y', 'SA', 'SA', '13/Jun/2005 12:00:00 AM', 'E','MnuVoucherEntry','Voucher Entry','Accounts','Y') "
                SQLCmd.ExecuteNonQuery()
            End If

            If Not CMain.FGetMaxNo("Select Count(*) As Cnt From Voucher_Type VT Where VT.V_Type='BREC'", AgL.GCn) > 0 Then
                SQLCmd.CommandText = "INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, ModifiedBy, PreparedBy, U_EntDt, U_AE,MnuName,MnuText,MnuAttachedInModule) "
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('FA', 'BREC', 'BREC', 'Bank Reconcilation', 'BREC', 'Y', 'SA', 'SA', '13/Jun/2005 12:00:00 AM', 'E','','','') "
                SQLCmd.ExecuteNonQuery()
            End If


            If Not CMain.FGetMaxNo("Select Count(*) As Cnt From Voucher_Type VT Where VT.V_Type='OPBAL'", AgL.GCn) > 0 Then
                SQLCmd.CommandText = "INSERT INTO Voucher_Type (NCat, Category, V_Type, Description, Short_Name, SystemDefine, ModifiedBy, PreparedBy, U_EntDt, U_AE,MnuName,MnuText,MnuAttachedInModule,AuditAllowed) "
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('FA', 'JV', 'OPBAL', 'Opening Balance', 'OPBAL', 'Y', 'sa', 'sa', '30/Aug/2008 12:00:00 AM', 'A','MnuVoucherEntry','Voucher Entry','Accounts','Y')"
                SQLCmd.ExecuteNonQuery()
            End If


            '====================================================================
            '===================== Voucher Type Entry Ends ======================
            '====================================================================



            '====================================================================
            '=================== Account Group Entry Starts =====================
            '====================================================================
            If Not CMain.FGetMaxNo("Select Count(*) As Cnt From AcGroup ", AgL.GCn) > 0 Then
                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0001', NULL, 'Capital Account', 'Capital Account', NULL, 'L', 'Others', 'Y', 'SA', '01/Apr/2003 12:00:00 AM', 'A')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0002', NULL, 'Loan (Liability)', 'Loan (Liability)', NULL, 'L', 'Others', 'Y', 'SA', '01/Apr/2003 12:00:00 AM', 'A')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0003', NULL, 'Current Liabilities', 'Current Liabilities', NULL, 'L', 'Others', 'Y', 'SA', '01/Apr/2003 12:00:00 AM', 'A')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0004', NULL, 'Fixed Assets', 'Fixed Assets', NULL, 'A', 'Others', 'Y', 'SA', '01/Apr/2003 12:00:00 AM', 'A')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0005', NULL, 'Investments', 'Investments', NULL, 'A', 'Others', 'Y', 'SA', '01/Apr/2003 12:00:00 AM', 'A')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0006', NULL, 'Current Assets', 'Current Assets', NULL, 'A', 'Others', 'Y', 'SA', '01/Apr/2003 12:00:00 AM', 'A')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0007', NULL, 'Branch/Divisions', 'Branch/Divisions', NULL, 'A', 'Others', 'Y', 'SA', '01/Apr/2003 12:00:00 AM', 'A')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0008', NULL, 'Misc. Expences (Asset)', 'Misc. Expences (Asset)', NULL, 'A', 'Expenses', 'Y', 'SA', '01/Apr/2003 12:00:00 AM', 'A')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0009', NULL, 'Suspense A/c', 'Suspense A/c', NULL, 'A', 'Others', 'Y', 'SA', '02/Jul/2008 12:00:00 AM', 'E')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0010', NULL, 'Reserves & Surplus', 'Reserves & Surplus', NULL, 'L', 'Others', 'Y', 'SA', '02/Jul/2008 12:00:00 AM', 'E')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0011', NULL, 'Bank OD A/c', 'Bank OD A/c', NULL, 'L', 'Bank', 'Y', 'SA', '01/Apr/2003 12:00:00 AM', 'E')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0012', NULL, 'Secured Loans', 'Secured Loans', NULL, 'L', 'Others', 'Y', 'SA', '02/Jul/2008 12:00:00 AM', 'E')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0013', NULL, 'Unsecured Loans', 'Unsecured Loans', NULL, 'L', 'Others', 'Y', 'SA', '02/Jul/2008 12:00:00 AM', 'E')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0014', NULL, 'Duties & Taxes', 'Duties & Taxes', NULL, 'L', 'Expenses', 'Y', 'SA', '01/Apr/2003 12:00:00 AM', 'A')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0015', NULL, 'Provisions', 'Provisions', NULL, 'L', 'Expenses', 'Y', 'SA', '02/Jul/2008 12:00:00 AM', 'E')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0016', NULL, 'Sundry Creditors', 'Sundry Creditors', NULL, 'L', 'Supplier', 'Y', 'SA', '02/Jul/2008 12:00:00 AM', 'E')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0017', NULL, 'Opening Stock', 'Opening Stock', NULL, 'E', 'Direct', 'Y', 'SA', '01/Apr/2003 12:00:00 AM', 'A')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0018', NULL, 'Deposits (Asset)', 'Deposits (Asset)', NULL, 'A', 'Others', 'Y', 'SA', '01/Apr/2003 12:00:00 AM', 'A')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0019', NULL, 'Loans & Advances (Asset)', 'Loans & Advances (Asset)', NULL, 'A', 'Others', 'Y', 'SA', '02/Jul/2008 12:00:00 AM', 'E')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0020', NULL, 'Sundry Debtors', 'Sundry Debtors', NULL, 'A', 'Customer', 'Y', 'SA', '07/Jul/2008 12:00:00 AM', 'E')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0021', NULL, 'Cash-in-Hand', 'Cash-In-Hand', NULL, 'A', 'Cash', 'Y', 'SA', '05/Jul/2008 12:00:00 AM', 'E')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0022', NULL, 'Bank Accounts', 'Bank Accounts', NULL, 'A', 'Bank', 'Y', 'sa', '17/Aug/2008 12:00:00 AM', 'B')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0023', NULL, 'Sales Accounts', 'Sales Accounts', NULL, 'R', 'Sales', 'Y', 'SA', '01/Apr/2003 12:00:00 AM', 'A')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0024', NULL, 'Purchase Accounts', 'Purchase Accounts', NULL, 'E', 'Purchase', 'Y', 'SA', '01/Apr/2003 12:00:00 AM', 'A')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0025', NULL, 'Direct Incomes', 'Direct Incomes', NULL, 'R', 'Direct', 'Y', 'SA', '01/Apr/2003 12:00:00 AM', 'E')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0026', NULL, 'Direct Expenses', 'Direct Expenses', NULL, 'E', 'Direct', 'Y', 'SA', '01/Apr/2003 12:00:00 AM', 'E')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0027', NULL, 'Indirect Incomes', 'Indirect Incomes', NULL, 'R', 'Indirect', 'Y', 'SA', '01/Apr/2003 12:00:00 AM', 'E')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0028', NULL, 'Indirect Expenses', 'Indirect Expenses', NULL, 'E', 'Indirect', 'Y', 'SA', '05/Jul/2008 12:00:00 AM', 'E')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0029', NULL, 'Profit & Loss A/c', 'Profit & Loss A/c', NULL, 'L', 'Others', 'Y', 'SA', '01/Apr/2003 12:00:00 AM', 'A')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO AcGroup (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('0030', NULL, 'Closing Stock', 'Closing Stock', NULL, 'R', 'Direct', 'Y', 'SA', '01/Apr/2003 12:00:00 AM', 'A')"
                SQLCmd.ExecuteNonQuery()
            End If
            '====================================================================
            '===================== Account Group Entry Ends =====================
            '====================================================================


            '====================================================================
            '===================== Item Type Entry Starts =======================
            '====================================================================
            If Not CMain.FGetMaxNo("Select Count(*) As Cnt From ItemType ", AgL.GCn) > 0 Then
                SQLCmd.CommandText = "INSERT INTO ItemType (Code, Name)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('FM', 'Finished Mtrl.')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO ItemType (Code, Name)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('OT', 'Others')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO ItemType (Code, Name)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('PM', 'Packing Mtrl.')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO ItemType (Code, Name)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('RM', 'Raw Mtrl.')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO ItemType (Code, Name)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('SM', 'Store Mtrl.')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO ItemType (Code, Name)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('FL', 'Fuel')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO ItemType (Code, Name)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('CL', 'Coal')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO ItemType (Code, Name)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('CM', 'Chemical')"
                SQLCmd.ExecuteNonQuery()

                SQLCmd.CommandText = "INSERT INTO ItemType (Code, Name)"
                SQLCmd.CommandText = SQLCmd.CommandText & "VALUES ('SF', 'Semi Finished')"
                SQLCmd.ExecuteNonQuery()
            End If
            '====================================================================
            '======================== Item Type Entry End =======================
            '====================================================================


            '==================================================================================
            '======================== Function FGetItemDayStatus Starts =======================
            '==================================================================================
            StrSQL = "IF OBJECT_ID ( 'FGetItemDayStatus' ) IS NOT NULL Drop Function FGetItemDayStatus "
            SQLCmd.CommandText = StrSQL
            SQLCmd.ExecuteNonQuery()

            StrSQL = "Create Function [dbo].[FGetItemDayStatus]()  " & vbCrLf
            StrSQL += "Returns @IRTbl   " & vbCrLf
            StrSQL += "Table ( " & vbCrLf
            StrSQL += "ItemCode VarChar(10), " & vbCrLf
            StrSQL += "V_Date SmallDateTime,  " & vbCrLf
            StrSQL += "RecQty Float,  " & vbCrLf
            StrSQL += "RecValue Float, " & vbCrLf
            StrSQL += "IssueQty Float, " & vbCrLf
            StrSQL += "IssueValue Float, " & vbCrLf
            StrSQL += "RNTRecQty Float, " & vbCrLf
            StrSQL += "RNTRecValue Float, " & vbCrLf
            StrSQL += "RNTIssueQty Float, " & vbCrLf
            StrSQL += "RNTIssueValue Float, " & vbCrLf
            StrSQL += "DayOpQty Float,	 " & vbCrLf
            StrSQL += "DayOpValue Float, " & vbCrLf
            StrSQL += "DayClQty Float,	 " & vbCrLf
            StrSQL += "DayClValue Float) " & vbCrLf
            StrSQL += "AS " & vbCrLf
            StrSQL += "Begin  " & vbCrLf
            StrSQL += "Declare @RNTRecQty Float " & vbCrLf
            StrSQL += "Declare @RNTRecValue Float " & vbCrLf
            StrSQL += "Declare @RNTIssueQty Float " & vbCrLf
            StrSQL += "Declare @RNTIssueValue Float " & vbCrLf
            StrSQL += "Declare @ItemCode Varchar(10) " & vbCrLf
            StrSQL += "Declare @RNTDayOpValue Float " & vbCrLf
            StrSQL += "Declare @RNTDayClValue Float " & vbCrLf
            StrSQL += "Declare @RNTDayRate Float " & vbCrLf

            StrSQL += "Set @RNTRecQty = 0 " & vbCrLf
            StrSQL += "Set @RNTRecValue = 0 " & vbCrLf
            StrSQL += "Set @RNTIssueQty = 0 " & vbCrLf
            StrSQL += "Set @RNTIssueValue = 0 " & vbCrLf
            StrSQL += "Set @RNTDayOpValue = 0 " & vbCrLf
            StrSQL += "Set @RNTDayClValue = 0 " & vbCrLf
            StrSQL += "Set @RNTDayRate = 0 " & vbCrLf
            StrSQL += "Set @ItemCode = '' " & vbCrLf

            StrSQL += "INSERT	INTO @IRTbl  " & vbCrLf
            StrSQL += "Select	IfNull(ItemCode,'') As ItemCode,V_Date, " & vbCrLf

            StrSQL += "IfNull(Sum(Case When IfNull(IssueQty,0) < 0 Then Abs(IfNull(IssueQty,0)) When IfNull(RecQty,0) > 0 Then IfNull(RecQty,0) Else 0 End),0) As RecQty, " & vbCrLf
            StrSQL += "IfNull(Sum(Case When EType_IR='R' And IfNull(RecQty,0) >= 0 Then LandedValue When EType_IR='I' And IfNull(IssueQty,0) < 0 Then Abs(IfNull(LandedValue,0)) Else 0 End),0) As RecValue, " & vbCrLf
            StrSQL += "IfNull(Sum(Case When IfNull(RecQty,0) < 0 Then Abs(IfNull(RecQty,0)) When IfNull(IssueQty,0) > 0 Then IfNull(IssueQty,0) Else 0 End),0) As IssueQty, " & vbCrLf
            StrSQL += "0 As IssueValue, " & vbCrLf

            StrSQL += "Null As RNTRecQty,Null As RNTRecValue,Null As RNTIssueQty,Null As RNTIssueValue, " & vbCrLf
            StrSQL += "Null As DayOpQty,Null As DayOpValue,Null As DayClQty,Null As DayClValue " & vbCrLf
            StrSQL += "From Stock " & vbCrLf
            StrSQL += "Group By ItemCode,V_Date " & vbCrLf
            StrSQL += "ORDER BY ItemCode,V_Date " & vbCrLf


            StrSQL += "UPDATE @IRTbl " & vbCrLf
            StrSQL += "SET  " & vbCrLf
            StrSQL += "@RNTRecValue = RNTRecValue = (Case When @ItemCode=ItemCode Then @RNTRecValue Else 0 End) + RecValue, " & vbCrLf
            StrSQL += "@RNTIssueValue = RNTIssueValue = (Case When @ItemCode=ItemCode Then @RNTIssueValue Else 0 End) + IssueValue, " & vbCrLf
            StrSQL += "@RNTRecQty = RNTRecQty = (Case When @ItemCode=ItemCode Then @RNTRecQty Else 0 End) + RecQty, " & vbCrLf
            StrSQL += "@RNTIssueQty = RNTIssueQty = (Case When @ItemCode=ItemCode Then @RNTIssueQty Else 0 End) + IssueQty, " & vbCrLf
            StrSQL += "DayOpQty = (@RNTRecQty - @RNTIssueQty) - (RecQty - IssueQty) , " & vbCrLf
            StrSQL += "@RNTDayOpValue=DayOpValue = (Case When @ItemCode=ItemCode Then IfNull(@RNTDayClValue,0) Else 0 End) ,--- (RecValue - IssueValue), " & vbCrLf
            StrSQL += "DayClQty = @RNTRecQty - @RNTIssueQty , " & vbCrLf
            StrSQL += "@RNTDayClValue=DayClValue=(((Case When @ItemCode=ItemCode Then IfNull(@RNTDayOpValue,0) Else 0 End) + RecValue-IssueValue) / (Case When (RecQty+(@RNTRecQty-@RNTIssueQty)-(RecQty-IssueQty))=0 Then 1 Else (RecQty+(@RNTRecQty-@RNTIssueQty)-(RecQty-IssueQty)) End)  ) *((RecQty - IssueQty)+ (@RNTRecQty-@RNTIssueQty)-(RecQty - IssueQty) ), " & vbCrLf
            StrSQL += "@ItemCode=(Case When @ItemCode=ItemCode Then @ItemCode Else ItemCode End) " & vbCrLf
            StrSQL += "Return " & vbCrLf
            StrSQL += "End " & vbCrLf

            SQLCmd.CommandText = StrSQL
            SQLCmd.ExecuteNonQuery()

            '==================================================================================
            '======================== Function FGetItemDayStatus End ==========================
            '==================================================================================

            SQLCmd.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Public Sub UpdateTableStructure()
        AddNewTable()
        AddNewField()

        FDatabase(AgL.PubMdlTable)
    End Sub

    Private Sub AddNewField()
        Dim mQry$ = ""

        AgL.AddNewField(AgL.GCn, "Voucher_Type", "MnuAttachedInModule", "nVarchar(50)", , True)
        AgL.AddNewField(AgL.GCn, "Voucher_Type", "MnuName", "nVarchar(100)", , True)
        AgL.AddNewField(AgL.GCn, "Voucher_Type", "MnuText", "nVarchar(100)", , True)
        AgL.AddNewField(AgL.GCn, "Voucher_Type", "DefaultAc", "nVarchar(10)", , True)
        AgL.AddNewField(AgL.GCn, "Voucher_Type", "SerialNo", "Integer", , True)
        AgL.AddNewField(AgL.GCn, "Enviro_Accounts", "VrNumberSystem", "nVarchar(1)", , True)
        AgL.AddNewField(AgL.GCn, "Enviro_Accounts", "TdsROff", "nVarchar(1)", , True)
        AgL.AddNewField(AgL.GCn, "LedgerM", "RecID", "nVarchar(8)", , True)
        AgL.AddNewField(AgL.GCn, "Ledger", "RecID", "nVarchar(8)", , True)
        AgL.AddNewField(AgL.GCn, "Ledger", "ContraText", "nVarchar(Max)", , True)
        AgL.AddNewField(AgL.GCn, "Ledger", "FormulaString", "nVarchar(100)", , True)
        AgL.AddNewField(AgL.GCn, "Ledger", "TDSDeductFrom", "nVarchar(10)", , True)
        AgL.AddNewField(AgL.GCn, "Ledger", "OrignalAmt", "Float", , True)
        AgL.AddNewField(AgL.GCn, "LedgerM_Temp", "RecID", "nVarchar(8)", , True)
        AgL.AddNewField(AgL.GCn, "Ledger_Temp", "RecID", "nVarchar(8)", , True)
        AgL.AddNewField(AgL.GCn, "Ledger_Temp", "ContraText", "nVarchar(Max)", , True)
        AgL.AddNewField(AgL.GCn, "Ledger_Temp", "FormulaString", "nVarchar(100)", , True)
        AgL.AddNewField(AgL.GCn, "Ledger_Temp", "TDSDeductFrom", "nVarchar(10)", , True)
        AgL.AddNewField(AgL.GCn, "Ledger_Temp", "OrignalAmt", "Float", , True)
        AgL.AddNewField(AgL.GCn, "SubGroup", "CostCenter", "nVarchar(6)", , True)
        AgL.AddNewField(AgL.GCn, "SubGroup", "LedgerGroup", "nVarchar(10)", , True)
        AgL.AddNewField(AgL.GCn, "SubGroup", "Zone", "nVarchar(6)", , True)
        AgL.AddNewField(AgL.GCn, "SubGroup", "Distributor", "VARCHAR (10)", , True)
        AgL.AddNewField(AgL.GCn, "SubGroup", "STNo", "VARCHAR (40)", , True)
        AgL.AddNewField(AgL.GCn, "SubGroup", "IECCode", "VARCHAR (35)", , True)
        AgL.AddNewField(AgL.GCn, "SubGroup", "DuplicateTIN", "VARCHAR (1)", , True)
        AgL.AddNewField(AgL.GCn, "SubGroup", "PartyCat", "VARCHAR (1)", , True)
        AgL.AddNewField(AgL.GCn, "SubGroup", "PartyType", "VARCHAR (1)", , True)
        AgL.AddNewField(AgL.GCn, "SubGroup", "ECCCode", "VARCHAR (35)", , True)
        AgL.AddNewField(AgL.GCn, "SubGroup", "Excise", "VARCHAR (35)", , True)
        AgL.AddNewField(AgL.GCn, "SubGroup", "[Range]", "VARCHAR (35)", , True)
        AgL.AddNewField(AgL.GCn, "SubGroup", "Division", "VARCHAR (35)", , True)
        AgL.AddNewField(AgL.GCn, "SubGroup", "FBTOnPer", "FLOAT", , True)
        AgL.AddNewField(AgL.GCn, "SubGroup", "FBTPer", "FLOAT", , True)
        AgL.AddNewField(AgL.GCn, "SubGroup", "PolicyNo", "VARCHAR (50)", , True)
        AgL.AddNewField(AgL.GCn, "SubGroup", "Transfered", "VARCHAR (1)", , True)



    End Sub


    Private Sub AddNewTable()
        Dim mQry$ = "", mQry1$ = ""
        Try
            If Not AgL.IsTableExist("LedgerM_Temp", AgL.GCn) Then
                mQry = "CREATE TABLE LedgerM_Temp  " &
                   "( " &
                   "DocId      NVARCHAR (21) NOT NULL, " &
                   "V_No       NVARCHAR (10) NOT NULL, " &
                   "V_Type     NVARCHAR (5) NOT NULL, " &
                   "V_Prefix   NVARCHAR (5) NOT NULL, " &
                   "V_Date     SMALLDATETIME NULL, " &
                   "SubCode    NVARCHAR (10) NULL, " &
                   "Narration  NVARCHAR (255) NULL, " &
                   "PreparedBY NVARCHAR (10) NULL, " &
                   "PostedBY   NVARCHAR (10) NULL, " &
                   "Site_Code  NVARCHAR (2) NOT NULL, " &
                   "U_Name     NVARCHAR (10) NULL, " &
                   "U_EntDt    SMALLDATETIME NULL, " &
                   "U_AE       NVARCHAR (1) NULL, " &
                   "RowId      BIGINT IDENTITY NOT NULL, " &
                   "UpLoadDate SMALLDATETIME NULL " &
                   ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try





        Try
            If Not AgL.IsTableExist("Ledger_Temp", AgL.GCn) Then
                mQry = "   CREATE TABLE Ledger_Temp " &
                "( " &
                "DocId            NVARCHAR (21) NOT NULL, " &
                "V_No             NVARCHAR (10) NOT NULL, " &
                "V_Type           NVARCHAR (5) NOT NULL, " &
                "V_Prefix         NVARCHAR (5) NOT NULL, " &
                "V_Date           SMALLDATETIME NULL, " &
                "V_SNo            INT NOT NULL, " &
                "SubCode          NVARCHAR (10) NULL, " &
                "ContraSub        NVARCHAR (10) NULL, " &
                "AmtDr            FLOAT NULL, " &
                "AmtCr            FLOAT NULL, " &
                "Chq_No           NVARCHAR (20) NULL, " &
                "Chq_Date         SMALLDATETIME NULL, " &
                "Clg_Date         SMALLDATETIME NULL, " &
                "TDSCategory      NVARCHAR (6) NULL, " &
                "TdsDesc          NVARCHAR (4) NULL, " &
                "TdsOnAmt         FLOAT NULL, " &
                "TdsPer           FLOAT NULL, " &
                "Tds_Of_V_Sno     INT NULL, " &
                "CostCenter       NVARCHAR (6) NULL, " &
                "System_Generated NVARCHAR (1) NULL, " &
                "Narration        NVARCHAR (255) NULL, " &
                "Site_Code        NVARCHAR (2) NOT NULL, " &
                "U_Name           NVARCHAR (10) NULL, " &
                "U_EntDt          SMALLDATETIME NULL, " &
                "U_AE             NVARCHAR (1) NULL, " &
                "RowId            BIGINT IDENTITY NOT NULL, " &
                "UpLoadDate       SMALLDATETIME NULL " &
                ")"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try





        Try
            If Not AgL.IsTableExist("CostCType", AgL.GCn) Then
                mQry = "CREATE TABLE dbo.CostCType " &
                     "( " &
                     "Code NVARCHAR (2) NOT NULL, " &
                     "Name NVARCHAR (35) NOT NULL, " &
                     "CONSTRAINT PK_CostCType PRIMARY KEY (Code) " &
                     ")"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



        Try
            If Not AgL.IsTableExist("CostCGrp", AgL.GCn) Then
                mQry = "   CREATE TABLE CostCGrp " &
                "( " &
                "Code       NVARCHAR (6) NOT NULL, " &
                "Name       NVARCHAR (35) NOT NULL, " &
                "Type       NVARCHAR (2) NOT NULL, " &
                "U_Name     NVARCHAR (10) NULL, " &
                "U_EntDt    SMALLDATETIME NULL, " &
                "U_AE       NVARCHAR (1) NULL, " &
                "ModifiedBy NVARCHAR (10) NULL, " &
                "CONSTRAINT PK_CostCGrp PRIMARY KEY (Code), " &
                "CONSTRAINT FK_CostCGrp_CostCType_Type FOREIGN KEY (Type) REFERENCES CostCType (Code) " &
                ")"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



        Try
            If Not AgL.IsTableExist("CostCenterMast", AgL.GCn) Then
                mQry = "   CREATE TABLE CostCenterMast " &
                "( " &
                "Code    NVARCHAR (6) NOT NULL, " &
                "Name    NVARCHAR (30) NOT NULL, " &
                "U_Name  NVARCHAR (10) NULL, " &
                "U_EntDt SMALLDATETIME NULL, " &
                "U_AE    NVARCHAR (1) NULL, " &
                "CONSTRAINT PK_CostCenterMast PRIMARY KEY (Code) " &
                ")"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try




        Try
            If Not AgL.IsTableExist("CostCGrpDetail", AgL.GCn) Then
                mQry = "   CREATE TABLE CostCGrpDetail " &
                 "( " &
                 "CostCGrpCode   NVARCHAR (6) NOT NULL, " &
                 "CostCentreCode NVARCHAR (6) NOT NULL, " &
                 "U_Name         NVARCHAR (10) NULL, " &
                 "U_EntDt        SMALLDATETIME NULL, " &
                 "U_AE           NVARCHAR (1) NULL, " &
                 "CONSTRAINT PK_CostCGrpDetail PRIMARY KEY (CostCentreCode, CostCGrpCode), " &
                 "CONSTRAINT FK_CostCGrpDetail_CostCenterMast_CostCentreCode FOREIGN KEY (CostCentreCode) REFERENCES dbo.CostCenterMast (Code), " &
                 "CONSTRAINT FK_CostCGrpDetail_CostCGrp_CostCGrpCode FOREIGN KEY (CostCGrpCode) REFERENCES CostCGrp (Code) " &
                 ")"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try



        Try
            If Not AgL.IsTableExist("LedgerGroup", AgL.GCn) Then
                mQry = "   CREATE TABLE dbo.LedgerGroup " &
                "( " &
                "Code       VARCHAR (10) NOT NULL, " &
                "Name       VARCHAR (100) NULL, " &
                "U_Name     VARCHAR (10) NULL, " &
                "U_EntDt    SMALLDATETIME NULL, " &
                "U_AE       VARCHAR (1) NULL, " &
                "Transfered VARCHAR (1) NULL, " &
                "CONSTRAINT PK_LedgerGroup PRIMARY KEY (Code) " &
                ")"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try





        Try
            If Not AgL.IsTableExist("ZoneMast", AgL.GCn) Then
                mQry = "   CREATE TABLE dbo.ZoneMast " &
                "( " &
                "Code       VARCHAR (6) NOT NULL, " &
                "Name       VARCHAR (50) NULL, " &
                "U_Name     VARCHAR (10) NULL, " &
                "U_EntDt    SMALLDATETIME NULL, " &
                "U_AE       VARCHAR (1) NULL, " &
                "Transfered VARCHAR (1) NULL, " &
                "CONSTRAINT PK_ZoneMast PRIMARY KEY (Code)  " &
                ")"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Public Shared Sub FManageTick(dgl As DataGridView, columnIndex As Integer, mandatoryRowIndex As Integer)
        Dim I As Integer

        If dgl.CurrentCell.RowIndex < 0 Then Exit Sub
        For I = 0 To dgl.SelectedCells.Count - 1
            FTick(dgl, columnIndex, dgl.SelectedCells.Item(I).RowIndex, mandatoryRowIndex)
        Next
    End Sub
    Public Shared Sub FTick(dgl As DataGridView, ByVal IntColIndex As Integer, ByVal IntRowIndex As Integer, ByVal mandatoryRowIndex As Integer)
        If IntRowIndex < 0 Then Exit Sub
        'If IntColIndex <> 0 Then Exit Sub
        If dgl.CurrentCell.ColumnIndex = IntColIndex Then
            If dgl(mandatoryRowIndex, IntRowIndex).Value <> "" Then
                If dgl(IntColIndex, IntRowIndex).Value = "þ" Then
                    dgl(IntColIndex, IntRowIndex).Value = "o"
                Else
                    dgl(IntColIndex, IntRowIndex).Value = "þ"
                End If
            Else
                dgl(IntColIndex, IntRowIndex).Value = ""
            End If
        End If
    End Sub
    Public Shared Function FDivisionNameForCustomization(Optional NoOfCharsFromLeft As Integer = 100) As String
        Dim mQry As String
        Dim DivName As String
        mQry = "Select Upper(Substr(Name,1," & NoOfCharsFromLeft & ")) From Subgroup Where Subcode = 'D' "
        DivName = AgL.Dman_Execute(mQry, AgL.GcnMain).ExecuteScalar()
        FDivisionNameForCustomization = DivName
    End Function




#Region "Database Structure Related Functions/ Procedures "
    'Note:- This Is A Function For Creating Tables In Related Database
    'It Does Not Remove Fields But Add/Alter Fields And Keys
    Public Function FExecuteDBScript(ByVal ModuleTable() As LITable) As LIException()
        Dim StrSQL As String
        Dim LIExcpAry(0) As LIException

        'Note:- Making Temporary Tables And Collecting Keys 
        StrSQL = FCollectKeys(ModuleTable)

        'Note:- Droping Constraints PK/FK/Default Values
        FDropKeys(StrSQL, LIExcpAry)

        'Note:- Adding/Altering Tables In Database
        FSynchronizeTables(ModuleTable, LIExcpAry)

        'Note:- Setting Primary Keys/ Foreign Keys For All Tables
        FSetPK_FK(StrSQL, LIExcpAry)

        Return LIExcpAry
    End Function
    'Note:- Adding/Altering Tables In Database
    Public Sub FSynchronizeTables(ByRef ModuleTable() As LITable, ByRef LIExcpAry() As LIException)
        Dim I As Integer
        Dim J As Integer
        Dim FrmPGB As New FrmProgressBar

        FrmPGB.Show()
        FrmPGB.FMoveBar()

        For I = 0 To UBound(ModuleTable) - 1
            FrmPGB.FMoveBar()
            'Note:- Adding A Blank Table With Temporary Column __Temp__
            FAddException(LIExcpAry, FCreateTable(ModuleTable(I)))

            For J = 0 To UBound(ModuleTable(I).ColItem) - 1
                FrmPGB.FMoveBar()
                'Adding/Altering Columns
                FAddException(LIExcpAry, FCreateColumn(ModuleTable(I).StrName, ModuleTable(I).ColItem(J)))
            Next

            FrmPGB.FMoveBar()
            'Note:- Droping Temporary Column __Temp__
            FAddException(LIExcpAry, FDropColumn(ModuleTable(I).StrName, "__Temp__"))
        Next

        FrmPGB.Close()
        FrmPGB = Nothing
    End Sub
    Public Sub FSetPK_FK(ByRef StrSQL As String, ByRef LIExcpAry() As LIException)
        Dim I As Integer
        Dim DTTemp As DataTable
        Dim FrmPGB As New FrmProgressBar

        FrmPGB.Show()
        FrmPGB.FMoveBar()

        '======================================================================
        '============================== START ================================= 
        '======================================================================
        'Note:- Setting Primary Keys For All Tables
        DTTemp = FGetDatTable(StrSQL & "Select OnTable From @TmpTable Where ColType='PK' Group By OnTable ", AgL.GCn)
        For I = 0 To DTTemp.Rows.Count - 1
            FrmPGB.FMoveBar()
            FAddException(LIExcpAry, FSetPrimaryKey(StrSQL, AgL.XNull(DTTemp.Rows(I).Item("OnTable"))))
        Next
        DTTemp.Dispose()
        DTTemp = Nothing
        '======================================================================
        '============================= END ====================================
        '======================================================================


        '======================================================================
        '============================== START ================================= 
        '======================================================================
        'Note:- Setting Foreign Keys For All Tables
        DTTemp = FGetDatTable(StrSQL & "Select OnTable,OnColumn,WithTable,WithColumn " &
                    "From @TmpTable Where ColType='FK' " &
                    "Group By OnTable,OnColumn,WithTable,WithColumn ", AgL.GCn)
        For I = 0 To DTTemp.Rows.Count - 1
            FrmPGB.FMoveBar()
            FAddException(LIExcpAry, FSetForeignKey(AgL.XNull(DTTemp.Rows(I).Item("OnTable")), AgL.XNull(DTTemp.Rows(I).Item("WithTable")),
                         AgL.XNull(DTTemp.Rows(I).Item("OnColumn")), AgL.XNull(DTTemp.Rows(I).Item("WithColumn"))))
        Next
        DTTemp.Dispose()
        DTTemp = Nothing
        '======================================================================
        '============================= END ====================================
        '======================================================================

        FrmPGB.Close()
        FrmPGB = Nothing
    End Sub
    'Note:- Making Temporary Tables And Collecting Keys 
    Public Function FCollectKeys(ByRef ModuleTable() As LITable) As String
        Dim I As Integer
        Dim StrSQL As String
        Dim FrmPGB As New FrmProgressBar

        FrmPGB.Show()
        FrmPGB.FMoveBar()

        StrSQL = "Declare @TmpTable As Table(ColType NVarChar(20),OnColumn NVarChar(100), "
        StrSQL = StrSQL & "OnTable NVarChar(100),WithColumn NVarChar(100),WithTable NVarChar(100)) "
        For I = 0 To UBound(ModuleTable) - 1
            FrmPGB.FMoveBar()
            StrSQL = StrSQL & FCollectPrimaryKey(ModuleTable(I).StrName, ModuleTable(I).ColItem)
            StrSQL = StrSQL & FCollectForeignKey(ModuleTable(I).StrName, ModuleTable(I).FKey)
        Next
        FrmPGB.Close()
        FrmPGB = Nothing

        FCollectKeys = StrSQL
    End Function
    'Note:- Droping Constraints PK/FK/Default Values
    Public Sub FDropKeys(ByRef StrSQL As String, ByRef LIExcpAry() As LIException)
        Dim I As Integer
        Dim DTTemp As DataTable
        Dim FrmPGB As New FrmProgressBar

        FrmPGB.Show()
        FrmPGB.FMoveBar()

        DTTemp = FGetDatTable(StrSQL &
                    "Select SO.Name As DFName,SO1.Name As OnTable " &
                    "From SysObjects SO Left Join SysObjects SO1 On SO.Parent_Obj=SO1.ID " &
                    "Where SO.Parent_Obj In " &
                    "(Select ID From SysObjects Where Name In " &
                    "(Select OnTable From @TmpTable Group By OnTable)) " &
                    "Order By SO.XType", AgL.GCn)
        For I = 0 To DTTemp.Rows.Count - 1
            FrmPGB.FMoveBar()
            FAddException(LIExcpAry, FDropConstraint(AgL.XNull(DTTemp.Rows(I).Item("OnTable")),
                      AgL.XNull(DTTemp.Rows(I).Item("DFName"))))
        Next
        DTTemp.Dispose()
        DTTemp = Nothing

        FrmPGB.Close()
        FrmPGB = Nothing
    End Sub
    'Note:- This Procedure Drops All Constraint Of Related Tables
    Private Function FDropConstraint(ByVal StrOnTable As String, ByVal StrCnstName As String) As LIException
        Dim SQLCmd As New Object
        Dim LIExpRtn As New LIException

        Try
            LIExpRtn.StrValue1 = "FDropConstraint"
            LIExpRtn.StrValue2 = StrOnTable
            LIExpRtn.StrValue3 = StrCnstName

            SQLCmd.CommandTimeout = 0
            SQLCmd = AgL.GCn.CreateCommand
            SQLCmd.CommandText = "Alter Table " & StrOnTable & "  "
            SQLCmd.CommandText = SQLCmd.CommandText & "Drop Constraint " & StrCnstName
            SQLCmd.ExecuteNonQuery()
            LIExpRtn.StrValue4 = "Constraint Dropped Successfully."
        Catch ex As Exception
            LIExpRtn.StrMessage = ex.Message
        End Try
        Return LIExpRtn
    End Function
    'Note:- Ths Procedure Set Foreign Key For Related Tables
    Private Function FSetForeignKey(ByVal StrOnTable As String, ByVal StrWithTable As String,
    ByVal StrOnColumn As String, ByVal StrWithColumn As String) As LIException
        Dim StrSQL As String
        Dim SQLCmd As New Object
        Dim LIExpRtn As New LIException

        LIExpRtn.StrValue1 = "FSetForeignKey"
        LIExpRtn.StrValue2 = StrOnTable
        LIExpRtn.StrValue3 = StrOnColumn
        Try
            SQLCmd.CommandTimeout = 0
            SQLCmd = AgL.GCn.CreateCommand
            StrSQL = "Alter Table " & StrOnTable & " Add Constraint "
            StrSQL = StrSQL & "[FK_" & StrOnTable & "_"
            StrSQL = StrSQL & StrWithTable & "_" & Replace(StrOnColumn, ",", "_") & "] "
            StrSQL = StrSQL & "FOREIGN KEY(  "
            StrSQL = StrSQL & " [" & Replace(StrOnColumn, ",", "],[") & "]) "
            StrSQL = StrSQL & " REFERENCES [" & StrWithTable & "] (["
            StrSQL = StrSQL & Replace(StrWithColumn, ",", "],[") & "]) "

            SQLCmd.CommandText = StrSQL
            SQLCmd.ExecuteNonQuery()
            LIExpRtn.StrValue5 = "Key Addedd Successfully."
        Catch ex As Exception
            LIExpRtn.StrMessage = ex.Message
        End Try

        Return LIExpRtn
    End Function
    'Note:- Ths Procedure Set Primary Key For Related Tables
    Private Function FSetPrimaryKey(ByVal StrQuery As String, ByVal StrOnTable As String) As LIException
        Dim I As Integer
        Dim StrSQL As String
        Dim StrTemp As String
        Dim DTTemp As DataTable
        Dim SQLCmd As New Object
        Dim LIExpRtn As New LIException

        Try
            LIExpRtn.StrValue1 = "FSetPrimaryKey"
            LIExpRtn.StrValue2 = StrOnTable
            SQLCmd.CommandTimeout = 0
            SQLCmd = AgL.GCn.CreateCommand
            StrTemp = ""
            DTTemp = FGetDatTable(StrQuery & "Select OnTable,OnColumn  " &
                        "From @TmpTable Where ColType='PK' And OnTable='" & StrOnTable & "' " &
                        "Group By OnTable,OnColumn ", AgL.GCn)
            For I = 0 To DTTemp.Rows.Count - 1
                If StrTemp = "" Then
                    StrTemp = AgL.XNull(DTTemp.Rows(I).Item("OnColumn")) & " ASC "
                Else
                    StrTemp = StrTemp & " , " & AgL.XNull(DTTemp.Rows(I).Item("OnColumn")) & " ASC "
                End If
            Next
            LIExpRtn.StrValue3 = StrTemp

            If StrTemp <> "" Then
                StrSQL = "Alter Table " & StrOnTable & " Add Constraint [PK_" & StrOnTable & "] "
                StrSQL = StrSQL & "PRIMARY KEY CLUSTERED "
                StrSQL = StrSQL & "( " & StrTemp & " ) "
                SQLCmd.CommandText = StrSQL
                SQLCmd.ExecuteNonQuery()
            End If
            LIExpRtn.StrValue4 = "Key Added Successfully."
        Catch ex As Exception
            LIExpRtn.StrMessage = ex.Message
        End Try

        Return LIExpRtn
    End Function
    'Note:- This Function Searchs For Primary Keys In Array And Make A Temporary Query 
    'And Return That String
    Private Function FCollectPrimaryKey(ByVal StrTableName As String, ByVal ModuleTableCol() As LIColumn) As String
        Dim I As Integer
        Dim StrSQL = ""

        If Not ModuleTableCol Is Nothing Then
            For I = 0 To UBound(ModuleTableCol) - 1
                If ModuleTableCol(I).BlnPrimaryKey Then
                    StrSQL = StrSQL & "Insert Into @TmpTable(ColType,OnColumn,OnTable) Values( "
                    StrSQL = StrSQL & "'PK','" & ModuleTableCol(I).StrName & "','" & StrTableName & "') "
                End If
            Next
        End If
        Return StrSQL
    End Function
    'Note:- This Function Searchs For Foreign Keys In Array And Make A Temporary Query 
    'And Return That String
    Private Function FCollectForeignKey(ByVal StrTableName As String, ByVal ModuleTableFKey() As LIForeignKey) As String
        Dim I As Integer
        Dim StrSQL = ""

        If Not ModuleTableFKey Is Nothing Then
            For I = 0 To UBound(ModuleTableFKey) - 1
                StrSQL = StrSQL & "Insert Into @TmpTable(ColType,OnColumn,OnTable,WithColumn,WithTable) Values( "
                StrSQL = StrSQL & "'FK','" & ModuleTableFKey(I).StrOnColumn & "','" & StrTableName & "', "
                StrSQL = StrSQL & "'" & ModuleTableFKey(I).StrWithColumn & "','" & ModuleTableFKey(I).StrWithTable & "') "
            Next
        End If
        Return StrSQL
    End Function
    'Note:- This Function Create A Table With Temporary Field __Temp__
    Private Function FCreateTable(ByVal ModuleTable As LITable) As LIException
        Dim DTTemp As DataTable
        Dim SQLCmd As New Object
        Dim LIExpRtn As New LIException

        LIExpRtn.StrValue1 = "FCreateTable"
        LIExpRtn.StrValue2 = ModuleTable.StrName

        Try
            'Note:- Checking That Table Exist Or Not
            DTTemp = FGetDatTable("Select Count(*) from SysObjects Where Name='" & ModuleTable.StrName & "' ", AgL.GCn)
            If Not DTTemp.Rows(0).Item(0) > 0 Then
                SQLCmd = AgL.GCn.CreateCommand
                SQLCmd.CommandText = "Create Table " & ModuleTable.StrName & " (__Temp__ NVarChar(1)) "
                SQLCmd.ExecuteNonQuery()
                LIExpRtn.StrValue3 = "Table Created Successfully."
            Else
                LIExpRtn.StrValue3 = "Table Already Exist."
            End If
            DTTemp.Dispose()
            DTTemp = Nothing
        Catch ex As Exception
            LIExpRtn.StrMessage = ex.Message
        End Try

        Return LIExpRtn
    End Function
    'Note:- This Function Add/Alter Column And Set Default Value
    Private Function FCreateColumn(ByVal StrTableName As String, ByVal ModuleTableCol As LIColumn) As LIException
        Dim DTTemp As DataTable
        Dim SQLCmd As New Object
        Dim StrToDo As String = " Add "
        Dim StrLength As String = ""
        Dim StrCommand As String = ""
        Dim LIExpRtn As New LIException

        LIExpRtn.StrValue1 = "FCreateColumn"
        LIExpRtn.StrValue2 = StrTableName
        LIExpRtn.StrValue3 = ModuleTableCol.StrName

        Try
            'Note:- Checking Wheather To Add Or Alter Column
            DTTemp = FGetDatTable("Select Count(*),Max(SC.Name) As ColName,Max(ST.Name) As DType,Max(SC.Length) As Length " &
               "From SysColumns SC Left Join SysTypes ST On SC.XType=ST.XUserType " &
               "Where SC.Name='" & ModuleTableCol.StrName & "' And " &
               "SC.ID In (Select ID from SysObjects Where Name='" & StrTableName & "')", AgL.GCn)
            If Not DTTemp.Rows(0).Item(0) > 0 Then
                StrToDo = " Add "
                LIExpRtn.StrValue5 = LIExpRtn.StrValue5 & "Added Column "
            Else
                StrToDo = " Alter Column "
                FValidateColumnDif(LIExpRtn, AgL.XNull(DTTemp.Rows(0).Item("DType")), ModuleTableCol.StrDataType,
                                    AgL.VNull(DTTemp.Rows(0).Item("Length")), ModuleTableCol.IntLength)
                LIExpRtn.StrValue5 = LIExpRtn.StrValue5 & "Altered Column "
            End If
            DTTemp.Dispose() : DTTemp = Nothing

            'Note:- Checking Situations
            If ModuleTableCol.IntLength > 0 Then StrLength = " ( " & Trim(ModuleTableCol.IntLength) & " ) "
            If ModuleTableCol.BlnAllowNull Then StrCommand = " Null " Else StrCommand = " Not Null "
            If ModuleTableCol.BlnPrimaryKey Then StrCommand = " Not Null "


            'Note:- Adding/Altering Columns
            SQLCmd.CommandTimeout = 0
            SQLCmd = AgL.GCn.CreateCommand
            SQLCmd.CommandText = "Alter Table " & StrTableName & " " & StrToDo & " " &
                                 ModuleTableCol.StrName & " " &
                                 ModuleTableCol.StrDataType & " " &
                                 StrLength & " " & StrCommand
            SQLCmd.ExecuteNonQuery()


            '===================================================================
            '======================= Setting Default Value =====================
            '==============================Start================================
            '===================================================================

            '========= Droping Default Value If Exist ========
            DTTemp = FGetDatTable("Select Count(*) From SysColumns " &
               "Where XType='D' And Name='DF_" & StrTableName & "_" & ModuleTableCol.StrName & "'", AgL.GCn)
            If DTTemp.Rows(0).Item(0) > 0 Then
                SQLCmd.CommandText = "Alter Table " & StrTableName & " Drop CONSTRAINT DF_" & StrTableName & "_" & ModuleTableCol.StrName
                SQLCmd.ExecuteNonQuery()
            End If
            DTTemp.Dispose() : DTTemp = Nothing

            If Trim(ModuleTableCol.StrDefaultValue) <> "" Then
                '========= Adding Default Value To Column ========
                SQLCmd.CommandText = "Alter Table " & StrTableName & " ADD CONSTRAINT " &
                                     "DF_" & StrTableName & "_" & ModuleTableCol.StrName & " " &
                                     "Default '" & Trim(ModuleTableCol.StrDefaultValue) & "' " &
                                     "For " & ModuleTableCol.StrName
                SQLCmd.ExecuteNonQuery()
            End If
            '===================================================================
            '======================= Setting Default Value =====================
            '============================== End ================================
            '===================================================================

            LIExpRtn.StrValue5 = LIExpRtn.StrValue5 & " Successfully."
        Catch ex As Exception
            LIExpRtn.StrMessage = ex.Message
        End Try

        Return LIExpRtn
    End Function
    Private Sub FValidateColumnDif(ByRef LIExpRtn As LIException, ByVal StrDB_DType As String,
    ByVal StrMD_DType As String, ByVal IntDB_Length As Integer,
    ByVal IntMD_Length As Integer)

        If UCase(StrMD_DType) = UCase(FGetType(SQLDataType.VarCharMax)) Then StrMD_DType = FGetType(SQLDataType.VarChar) : IntMD_Length = -1
        If IntDB_Length > 0 Then If UCase(StrDB_DType) = UCase(FGetType(SQLDataType.VarChar)) Then IntDB_Length = IntDB_Length / 2
        If UCase(StrDB_DType) <> UCase(StrMD_DType) Then LIExpRtn.StrValue4 = StrDB_DType & " To " & StrMD_DType

        If IntMD_Length <> 0 Then
            If IntDB_Length <> IntMD_Length Then
                If Trim(LIExpRtn.StrValue4) = "" Then LIExpRtn.StrValue4 = StrMD_DType
                LIExpRtn.StrValue5 = Trim(IntDB_Length) & " To " & Trim(IntMD_Length)
            End If
        End If
    End Sub
    'Note:- Drops Related Column
    Private Function FDropColumn(ByVal StrTableName As String, ByVal StrColName As String) As LIException
        Dim SQLCmd As New Object
        Dim LIExcpRtn As New LIException
        Dim DTTemp As DataTable

        LIExcpRtn.StrValue1 = "FDropColumn"
        LIExcpRtn.StrValue2 = StrTableName
        LIExcpRtn.StrValue3 = StrColName

        Try
            DTTemp = FGetDatTable("Select Count(*) " & _
               "From SysColumns SC  " & _
               "Where SC.Name='" & StrColName & "' And " & _
               "SC.ID In (Select ID from SysObjects Where Name='" & StrTableName & "')", AgL.GCn)
            If DTTemp.Rows(0).Item(0) > 0 Then
                SQLCmd = AgL.GCn.CreateCommand
                SQLCmd.CommandText = "Alter Table " & StrTableName & " Drop Column " & StrColName
                SQLCmd.ExecuteNonQuery()
                LIExcpRtn.StrValue4 = "Column Dropped Successfully."
            Else
                LIExcpRtn.StrValue4 = "Column Does Not Exist."
            End If
        Catch ex As Exception
            LIExcpRtn.StrMessage = ex.Message
        End Try

        Return LIExcpRtn
    End Function
    'Note:- Sets Column Value To A Specified Array
    Public Sub FSetColumnValue(ByRef ModuleTable() As LITable, ByVal StrColumnName As String, ByVal SQLDataType As SQLDataType, _
    Optional ByVal IntLength As Int16 = 0, Optional ByVal BlnPrimaryKey As Boolean = False, Optional ByVal BlnAllowNull As Boolean = True, _
    Optional ByVal StrDefaultValue As String = "")

        Dim IntTblIndex As Integer
        Dim IntColIndex As Integer
        FAddColumn(ModuleTable)

        IntTblIndex = UBound(ModuleTable) - 1
        IntColIndex = UBound(ModuleTable(UBound(ModuleTable) - 1).ColItem) - 1

        ModuleTable(IntTblIndex).ColItem(IntColIndex).StrName = Trim(StrColumnName)
        ModuleTable(IntTblIndex).ColItem(IntColIndex).StrDataType = FGetType(SQLDataType)
        ModuleTable(IntTblIndex).ColItem(IntColIndex).IntLength = IntLength
        ModuleTable(IntTblIndex).ColItem(IntColIndex).BlnPrimaryKey = BlnPrimaryKey
        ModuleTable(IntTblIndex).ColItem(IntColIndex).BlnAllowNull = BlnAllowNull
        ModuleTable(IntTblIndex).ColItem(IntColIndex).StrDefaultValue = Trim(StrDefaultValue)
    End Sub
    'Note:- Sets Foreign Key For Column To A Specified Array
    Public Sub FSetFKeyValue(ByRef ModuleTable() As LITable, ByVal StrOnColumn As String, ByVal StrWithColumn As String, _
    ByVal StrWithTable As String)
        Dim IntTblIndex As Integer
        Dim IntColIndex As Integer
        FAddFKey(ModuleTable)

        IntTblIndex = UBound(ModuleTable) - 1
        IntColIndex = UBound(ModuleTable(UBound(ModuleTable) - 1).FKey) - 1

        ModuleTable(IntTblIndex).FKey(IntColIndex).StrOnColumn = Trim(Replace(StrOnColumn, " ", ""))
        ModuleTable(IntTblIndex).FKey(IntColIndex).StrWithColumn = Trim(Replace(StrWithColumn, " ", ""))
        ModuleTable(IntTblIndex).FKey(IntColIndex).StrWithTable = Trim(Replace(StrWithTable, " ", ""))
    End Sub
    'Note:- Inserting Row In An Array For Foreign Key
    Private Sub FAddFKey(ByRef ModuleTable() As LITable)
        Dim IntTblIndex As Integer

        IntTblIndex = UBound(ModuleTable) - 1
        If ModuleTable(UBound(ModuleTable) - 1).FKey Is Nothing Then
            ReDim ModuleTable(IntTblIndex).FKey(1)
        Else
            ReDim Preserve ModuleTable(IntTblIndex).FKey(UBound(ModuleTable(IntTblIndex).FKey) + 1)
        End If
    End Sub
    'Note:- Inserting Row In An Array For Columns
    Private Sub FAddColumn(ByRef ModuleTable() As LITable)
        Dim IntTblIndex As Integer

        IntTblIndex = UBound(ModuleTable) - 1
        If ModuleTable(UBound(ModuleTable) - 1).ColItem Is Nothing Then
            ReDim ModuleTable(IntTblIndex).ColItem(1)
        Else
            ReDim Preserve ModuleTable(IntTblIndex).ColItem(UBound(ModuleTable(IntTblIndex).ColItem) + 1)
        End If
    End Sub
    'Note:- Inserting Row In An Array For Table
    Public Sub FAddTable(ByRef ModuleTable() As LITable, ByVal StrTableName As String, ByVal StrModuleName As String)
        If ModuleTable Is Nothing Then
            ReDim ModuleTable(1)
        Else
            ReDim Preserve ModuleTable(UBound(ModuleTable) + 1)
        End If
        ModuleTable(UBound(ModuleTable) - 1).StrName = Trim(StrTableName)
        ModuleTable(UBound(ModuleTable) - 1).StrModuleName = Trim(StrModuleName)
    End Sub
    'Note:- Inserting Row In An Array For Exception
    Public Sub FAddException(ByRef LIExcpAry() As LIException, ByVal LIExcp As LIException)
        If LIExcpAry Is Nothing Then
            ReDim LIExcpAry(1)
        Else
            ReDim Preserve LIExcpAry(UBound(LIExcpAry) + 1)
        End If
        LIExcpAry(UBound(LIExcpAry) - 1) = LIExcp
    End Sub
    Public Function FGetExcpTable(ByVal LIExcpAry() As LIException) As String
        Dim StrRtn As String
        Dim I As Integer

        StrRtn = "Declare @TmpTable As Table(Value1 NVarChar(500),Value2 NVarChar(500), "
        StrRtn = StrRtn & "Value3 NVarChar(500),Value4 NVarChar(500),Value5 NVarChar(500),Msg NVarChar(500)) "
        For I = 0 To UBound(LIExcpAry) - 1
            StrRtn = StrRtn & "Insert Into @TmpTable(Value1,Value2,Value3,Value4,Value5,Msg) Values( "
            StrRtn = StrRtn & "'" & AgL.Chk_Quot(LIExcpAry(I).StrValue1) & "','" & AgL.Chk_Quot(LIExcpAry(I).StrValue2) & "', "
            StrRtn = StrRtn & "'" & AgL.Chk_Quot(LIExcpAry(I).StrValue3) & "','" & AgL.Chk_Quot(LIExcpAry(I).StrValue4) & "', "
            StrRtn = StrRtn & "'" & AgL.Chk_Quot(LIExcpAry(I).StrValue5) & "','" & AgL.Chk_Quot(LIExcpAry(I).StrMessage) & "') "
        Next

        Return StrRtn
    End Function
    Public Function FGetTableStructure(ByVal LIDB() As LITable) As String
        Dim StrRtn As String
        Dim I As Integer
        Dim J As Integer

        StrRtn = "Declare @TmpTable As Table(ModuleName NVarChar(100),TableName NVarChar(100), "
        StrRtn = StrRtn & "ColumnName NVarChar(100),ColDataType NVarChar(50),ColLength NVarChar(5), "
        StrRtn = StrRtn & "PrimaryKey NVarChar(10),AllowNull NVarChar(10),DefaultValue NVarChar(100), "
        StrRtn = StrRtn & "ForeignKey NVarChar(100),WithColumn NVarChar(100),WithTable NVarChar(100)) "

        For I = 0 To UBound(LIDB) - 1
            If Not LIDB(I).ColItem Is Nothing Then
                For J = 0 To UBound(LIDB(I).ColItem) - 1
                    StrRtn = StrRtn & "Insert Into @TmpTable(ModuleName,TableName, "
                    StrRtn = StrRtn & "ColumnName,ColDataType,ColLength,PrimaryKey, "
                    StrRtn = StrRtn & "AllowNull,DefaultValue) Values( "
                    StrRtn = StrRtn & "'" & AgL.Chk_Quot(LIDB(I).StrModuleName) & "','" & AgL.Chk_Quot(LIDB(I).StrName) & "', "
                    StrRtn = StrRtn & "'" & AgL.Chk_Quot(LIDB(I).ColItem(J).StrName) & "','" & AgL.Chk_Quot(LIDB(I).ColItem(J).StrDataType) & "', "
                    StrRtn = StrRtn & "'" & AgL.Chk_Quot(LIDB(I).ColItem(J).IntLength) & "','" & AgL.Chk_Quot(LIDB(I).ColItem(J).BlnPrimaryKey) & "', "
                    StrRtn = StrRtn & "'" & AgL.Chk_Quot(LIDB(I).ColItem(J).BlnAllowNull) & "','" & AgL.Chk_Quot(LIDB(I).ColItem(J).StrDefaultValue) & "') "
                Next
            End If

            If Not LIDB(I).FKey Is Nothing Then
                For J = 0 To UBound(LIDB(I).FKey) - 1
                    StrRtn = StrRtn & "Insert Into @TmpTable(ModuleName,TableName, "
                    StrRtn = StrRtn & "ForeignKey,WithColumn,WithTable) Values( "
                    StrRtn = StrRtn & "'" & AgL.Chk_Quot(LIDB(I).StrModuleName) & "','" & AgL.Chk_Quot(LIDB(I).StrName) & "', "
                    StrRtn = StrRtn & "'" & AgL.Chk_Quot(LIDB(I).FKey(J).StrOnColumn) & "','" & AgL.Chk_Quot(LIDB(I).FKey(J).StrWithColumn) & "', "
                    StrRtn = StrRtn & "'" & AgL.Chk_Quot(LIDB(I).FKey(J).StrWithTable) & "') "
                Next
            End If
        Next
        StrRtn = StrRtn & "Select * From @TmpTable "

        Return StrRtn
    End Function
#End Region

End Class
