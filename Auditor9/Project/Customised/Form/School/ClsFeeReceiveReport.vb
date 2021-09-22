Imports System.Data.SqlClient
Imports System.Data.SQLite
Imports System.IO
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Public Class ClsFeeReceiveReport
    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim ErrorLog As String = ""

    Dim rowReportType As Integer = 0
    Dim rowFromDate As Integer = 1
    Dim rowToDate As Integer = 2
    Dim rowClass As Integer = 3
    Dim rowStudent As Integer = 4
    Dim rowPaymentAc As Integer = 5
    Dim rowDivision As Integer = 6
    Dim rowSite As Integer = 7


    'Dim WithEvents ReportFrm As Aglibrary.FrmReportLayout
    Dim WithEvents ReportFrm As AgLibrary.FrmReportLayout
    Public Property GRepFormName() As String
        Get
            GRepFormName = mGRepFormName
        End Get
        Set(ByVal value As String)
            mGRepFormName = value
        End Set
    End Property

    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name As [Site] From SiteMast Where  Code In (" & AgL.PubSiteList & ")  "
    Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code as Code, Div_Name As [Division] From Division "
    Dim mHelpStudentQry$ = " Select 'o' As Tick,  Sg.Code, Sg.Name AS Party, Sg.Address
                    FROM viewHelpSubGroup Sg  
                    Where Sg.SubGroupType = '" & ClsSchool.SubGroupType_Student & "'
                    Order By Name "
    Dim mHelpPaymentAcQry$ = " Select 'o' As Tick,  Sg.SubCode AS Code, Sg.Name
                                    FROM Subgroup Sg With (NoLock)
                                    Where Sg.Nature In ('Bank','Cash') 
                                    And IfNull(Sg.Status,'Active') = 'Active' "
    Dim mHelpClassQry$ = " Select 'o' As Tick,  Sg.SubCode As Code, Sg.Name AS Class
                    FROM SubGroup Sg  
                    Where Sg.SubGroupType = '" & ClsSchool.SubGroupType_Class & "'"
    Dim mHelpYesNo$ = " Select 'Yes' As Code, 'Yes' AS [Value] Union All Select 'No' As Code, 'No' AS [Value] "

    Dim DsRep As DataSet = Nothing, DsRep1 As DataSet = Nothing, DsRep2 As DataSet = Nothing
    Dim mQry$ = "", RepName$ = "", RepTitle$ = "", OrderByStr$ = ""

    Dim StrMonth$ = ""
    Dim StrQuarter$ = ""
    Dim StrFinancialYear$ = ""
    Dim StrTaxPeriod$ = ""
    Public Sub Ini_Grid()
        Try
            mQry = "Select 'Summary' as Code, 'Summary' as Name 
                    Union All Select 'Detail' as Code, 'Detail' as Name "
            ReportFrm.CreateHelpGrid("Report Type", "Report Type", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Summary")
            ReportFrm.CreateHelpGrid("From Date", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("To Date", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
            ReportFrm.CreateHelpGrid("Class", "Class", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpClassQry, , 450, 325, 200)
            ReportFrm.CreateHelpGrid("Student", "Student", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpStudentQry, , 450, 725, 300)
            ReportFrm.CreateHelpGrid("PaymentAc", "Payment A/c", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpPaymentAcQry, , 450, 725, 300)
            ReportFrm.CreateHelpGrid("Division", "Division", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
            ReportFrm.CreateHelpGrid("Site", "Site", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function FGetVoucher_TypeQry(ByVal TableName As String) As String
        FGetVoucher_TypeQry = " SELECT Distinct 'o' As Tick, H.V_Type AS Code, Vt.Description AS [Voucher Type] " &
                                " FROM " & TableName & " H  " &
                                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type "
    End Function
    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        ProcFeeReceiveReport()
    End Sub
    Public Sub New(ByVal mReportFrm As AgLibrary.FrmReportLayout)
        ReportFrm = mReportFrm
    End Sub
    Private Sub ProcFeeReceiveReport()
        Dim mCondStr$ = ""
        Dim mMainQry As String = ""

        Try
            mCondStr = mCondStr & " And Vt.NCat = '" & ClsSchool.NCat_FeeReceipt & "'"
            mCondStr = mCondStr & " AND Date(H.V_Date) Between " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " And " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & ""
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sgad.Class", rowClass)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SubCode", rowStudent)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Lhd.SubCode", rowPaymentAc)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Div_Code", rowDivision).Replace("''", "'")
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Site_Code", rowSite).Replace("''", "'")

            mMainQry = " Select H.DocId, IfNull(L.DueDate,H.V_Date) As OrderByDate, L.Comp_Code, C.Comp_Name, H.SubCode As StudentCode, Sg.Name As Student, Sg.FatherName,
                    L.Class As ClassCode, Class.Name As Class, L.Fee As FeeCode, Fee.Name As Fee, 
                    L.SubHead As SubHeadCode, SubHead.Name As SubHead, L.DueDate As FeeDueDate, 
                    Case When Psg.Nature = 'Cash' Then L.AdjustedAmount Else 0 End As CashAmount,
                    Case When Psg.Nature <> 'Cash' Then L.AdjustedAmount Else 0 End As NonCashAmount
                    From LedgerHead H 
                    LEFT JOIN LedgerHeadDetail Lhd On H.DocId = Lhd.DocId
                    LEFT JOIN SuBGroup Psg On Lhd.SubCode = Psg.SubCode
                    LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                    LEFT JOIN FeeAdjustmentDetail L On H.DocId = L.DocId
                    LEFT JOIN ViewHelpSubgroup Sg On H.SubCode = Sg.Code
                    LEFT JOIN Company C On L.Comp_Code = C.Comp_Code
                    LEFT JOIN SubGroup Class ON L.Class = Class.SubCode
                    LEFT JOIN SubGroup Fee On L.Fee = Fee.SubCode
                    LEFT JOIN SubGroup SubHead On L.SubHead = SubHead.SubCode 
                    Where IfNull(L.AdjustedAmount,0) <> 0 " & mCondStr

            mMainQry += " UNION ALL "
            mMainQry += " Select H.DocId, H.V_Date As OrderByDate, Null As Comp_Code, Null As Comp_Name, H.SubCode As StudentCode, Sg.Name As Student, Sg.FatherName,
                    L.Class As ClassCode, Class.Name As Class, Null As FeeCode, 'Discount' As Fee, 
                    Null As SubHeadCode, Null As SubHead, Null As FeeDueDate, 
                    Case When Psg.Nature = 'Cash' Then -H.Discount Else 0 End As CashAmount,
                    Case When Psg.Nature <> 'Cash' Then -H.Discount Else 0 End As NonCashAmount
                    From LedgerHead H 
                    LEFT JOIN LedgerHeadDetail Lhd On H.DocId = Lhd.DocId
                    LEFT JOIN SubGroup Psg On Lhd.SubCode = Psg.SubCode
                    LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type
                    LEFT JOIN ViewHelpSubgroup Sg On H.SubCode = Sg.Code
                    LEFT JOIN (Select DocId, Max(Class) As Class From FeeAdjustmentDetail Group By DocId) As L ON H.DocId = L.DocId
                    LEFT JOIN SubGroup Class ON L.Class = Class.SubCode
                    Where IfNull(H.Discount,0) <> 0 " & mCondStr

            If ReportFrm.FGetText(rowReportType) = "Summary" Then
                RepTitle = "Fee Receive Summary" : RepName = "FeeReceiveReport_Summary"
                mQry = " Select Max(VMain.Student) As Student, Max(VMain.FatherName) As FatherName, 
                    Max(VMain.Class) As Class, 
                    Sum(VMain.CashAmount) As CashAmount,
                    Sum(VMain.NonCashAmount) As NonCashAmount
                    From (" & mMainQry & ") As VMain
                    Group By VMain.Student "
            Else
                RepTitle = "Fee Receive Detail" : RepName = "FeeReceiveReport_Detail"
                mQry = mMainQry & " Order By Sg.Name, H.DocId, IfNull(L.DueDate,H.V_Date) "
            End If

            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")
            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
End Class
