Imports System.Data.SqlClient
Imports System.Data.SQLite
Imports System.IO
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Public Class ClsStudentList
    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim ErrorLog As String = ""

    Dim rowClass As Integer = 0
    Dim rowStudent As Integer = 1
    Dim rowDivision As Integer = 2
    Dim rowSite As Integer = 3


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
        ProcStudentList()
    End Sub
    Public Sub New(ByVal mReportFrm As AgLibrary.FrmReportLayout)
        ReportFrm = mReportFrm
    End Sub
    Private Sub ProcStudentList()
        Dim mCondStr$ = ""

        Try
            RepName = "StudentList" : RepTitle = "Student List"

            mCondStr = " Where Sg.SubGroupType = '" & ClsSchool.SubGroupType_Student & "'"
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sgad.Class", rowClass)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.SubCode", rowStudent)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.Div_Code", rowDivision).Replace("''", "'")
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Sg.Site_Code", rowSite).Replace("''", "'")

            mQry = " Select Sg.SubCode As Code, VSg.Name AS Student, Sg.FatherName, Sg.Address || ', ' || C.CityName As Address, Class.Name As Class,
                    IfNull(Sg.Phone,'') || ', ' || IfNull(Sg.Mobile,'') As ContactNo, Sg.Email
                    FROM SubGroup Sg 
                    LEFT JOIN ViewHelpSubgroup VSg On Sg.SubCode = VSg.Code
                    LEFT JOIN City C On Sg.CityCode = C.CityCode 
                    LEFT JOIN (Select * From SubGroupAdmission Where PromotionDate Is Null) As Sgad ON Sg.SubCode = Sgad.SubCode 
                    LEFT JOIN SubGroup Class ON Sgad.Class = Class.SubCode " & mCondStr &
                    " Order By Sg.Name "
            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")
            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
End Class
