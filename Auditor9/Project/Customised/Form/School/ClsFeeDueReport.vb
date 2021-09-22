Imports System.Data.SqlClient
Imports System.Data.SQLite
Imports System.IO
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Public Class ClsFeeDueReport
    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim ErrorLog As String = ""

    Dim rowReportType As Integer = 0
    Dim rowAsOnDate As Integer = 1
    Dim rowClass As Integer = 2
    Dim rowStudent As Integer = 3
    Dim rowDivision As Integer = 4
    Dim rowSite As Integer = 5



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
            ReportFrm.CreateHelpGrid("As On Date", "As On Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
            ReportFrm.CreateHelpGrid("Class", "Class", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpClassQry, , 450, 325, 200)
            ReportFrm.CreateHelpGrid("Student", "Student", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.MultiSelection, mHelpStudentQry, , 450, 725, 300)
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
        ProcFeeDueReport()
    End Sub
    Public Sub New(ByVal mReportFrm As AgLibrary.FrmReportLayout)
        ReportFrm = mReportFrm
    End Sub
    Private Sub ProcFeeDueReport()
        Dim mCondStr$ = ""
        Dim mMainQry As String = ""

        Try
            mCondStr = " Where IfNull(L.BalanceAmount,0) <> 0 "
            mCondStr = mCondStr & " AND Date(L.DueDate) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowAsOnDate)).ToString("s")) & ""
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Class", rowClass)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.SubCode", rowStudent)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Div_Code", rowDivision).Replace("''", "'")
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Site_Code", rowSite).Replace("''", "'")

            mMainQry = " Select L.Comp_Code, C.Comp_Name, L.SubCode As StudentCode, Sg.Name As Student, Sg.FatherName,
                    L.Class As ClassCode, Class.Name As Class, L.Fee As FeeCode, Fee.Name As Fee, 
                    L.SubHead As SubHeadCode, SubHead.Name As SubHead,  
                    L.FeeAmount, L.ReceivedAmount, L.BalanceAmount
                    From FeeDueDetail L 
                    LEFT JOIN ViewHelpSubgroup Sg On L.SubCode = Sg.Code
                    LEFT JOIN Company C On L.Comp_Code = C.Comp_Code
                    LEFT JOIN SubGroup Class ON L.Class = Class.SubCode
                    LEFT JOIN SubGroup Fee On L.Fee = Fee.SubCode
                    LEFT JOIN SubGroup SubHead On L.SubHead = SubHead.SubCode " & mCondStr

            If ReportFrm.FGetText(rowReportType) = "Summary" Then
                RepTitle = "Fee Due Summary" : RepName = "FeeDueReport_Summary"
                mQry = " Select Max(VMain.Student) As Student, Max(VMain.FatherName) As FatherName, 
                    Max(VMain.Class) As Class, 
                    Sum(VMain.BalanceAmount) As BalanceAmount
                    From (" & mMainQry & ") As VMain
                    Group By VMain.Student "
            ElseIf ReportFrm.FGetText(rowReportType) = "Summary" Then
                RepTitle = "Fee Due Detail" : RepName = "FeeDueReport_Detail"
                mQry = " Select VMain.* From (" & mMainQry & ") As VMain "
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
