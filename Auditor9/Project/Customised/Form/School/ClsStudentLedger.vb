Imports System.Data.SqlClient
Imports System.Data.SQLite
Imports System.IO
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants
Public Class ClsStudentLedger
    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim ErrorLog As String = ""

    Dim rowFromDate As Integer = 0
    Dim rowToDate As Integer = 1
    Dim rowStudent As Integer = 2

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
    Dim mHelpStudentQry$ = " Select Sg.Code As Code, Sg.Name AS Student, Sg.FatherName, Class.Name As Class
                        FROM ViewHelpSubgroup Sg 
                        LEFT JOIN City C On Sg.CityCode = C.CityCode 
                        LEFT JOIN (Select * From SubGroupAdmission Where PromotionDate Is Null) As Sgad ON Sg.Code = Sgad.SubCode 
                        LEFT JOIN SubGroup Class ON Sgad.Class = Class.SubCode 
                        Where Sg.SubGroupType = '" & ClsSchool.SubGroupType_Student & "'
                        Order By Sg.Name "
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
            ReportFrm.CreateHelpGrid("FromDate", "From Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
            ReportFrm.CreateHelpGrid("ToDate", "To Date", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
            ReportFrm.CreateHelpGrid("Student", "Student", AgLibrary.FrmReportLayout.FieldFilterDataType.StringType, AgLibrary.FrmReportLayout.FieldDataType.SingleSelection, mHelpStudentQry, "", 450, 725, 300)
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
        ProcStudentLedger()
    End Sub
    Public Sub New(ByVal mReportFrm As AgLibrary.FrmReportLayout)
        ReportFrm = mReportFrm
    End Sub
    Private Sub ProcStudentLedger()
        Dim mDueCondStr$ = ""
        Dim mRecCondStr$ = ""
        Dim mFeeDueQry As String = ""
        Dim mLateFeeDueQry As String = ""
        Dim mFeeRecQry As String = ""
        Dim mFeeDiscQry As String = ""

        Try
            If ReportFrm.FGetCode(rowStudent) = "" Then
                MsgBox("Please select student.", MsgBoxStyle.Information)
                Exit Sub
            End If

            mDueCondStr = " Where 1=1 "
            mDueCondStr = mDueCondStr & " AND Date(L.DueDate) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & ""
            mDueCondStr = mDueCondStr & " And L.SubCode = " & ReportFrm.FGetCode(rowStudent) & ""

            mRecCondStr = " Where 1=1 "
            mRecCondStr = mRecCondStr & " AND Date(H.V_Date) <= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowToDate)).ToString("s")) & ""
            mRecCondStr = mRecCondStr & " And H.SubCode = " & ReportFrm.FGetCode(rowStudent) & ""

            mFeeDueQry = " Select L.SubCode As StudentCode, Sg.Name As Student, Sg.FatherName,
                    'Fee Due : ' || Fee.Name As Narration, L.DueDate As V_Date,
                    L.FeeAmount As AmtDr, 0 As AmtCr
                    From FeeDueDetail L 
                    LEFT JOIN ViewHelpSubgroup Sg On L.SubCode = Sg.Code
                    LEFT JOIN Company C On L.Comp_Code = C.Comp_Code
                    LEFT JOIN SubGroup Class ON L.Class = Class.SubCode
                    LEFT JOIN SubGroup Fee On L.Fee = Fee.SubCode
                    LEFT JOIN SubGroup SubHead On L.SubHead = SubHead.SubCode " & mDueCondStr

            mLateFeeDueQry = " Select H.SubCode As StudentCode, Sg.Name As Student, Sg.FatherName,
                    'Late Fee Due' As Narration, H.V_Date As V_Date,
                    H.LateFee As AmtDr, 0 As AmtDr
                    From LedgerHead H 
                    LEFT JOIN LedgerHeadDetail Lhd On H.DocId = Lhd.DocId
                    LEFT JOIN ViewHelpSubgroup Sg On H.SubCode = Sg.Code " & mRecCondStr &
                    " And IfNull(H.LateFee,0) <> 0 "

            mFeeRecQry = " Select H.SubCode As StudentCode, Sg.Name As Student, Sg.FatherName,
                    'Fee Receipt' As Narration, H.V_Date As V_Date,
                    0 As AmtDr, Lhd.Amount As AmtDr
                    From LedgerHead H 
                    LEFT JOIN LedgerHeadDetail Lhd On H.DocId = Lhd.DocId
                    LEFT JOIN ViewHelpSubgroup Sg On H.SubCode = Sg.Code " & mRecCondStr

            mFeeDiscQry = " Select H.SubCode As StudentCode, Sg.Name As Student, Sg.FatherName,
                    'Discount Given' As Narration, H.V_Date As V_Date,
                    0 As AmtDr, H.Discount As AmtDr
                    From LedgerHead H 
                    LEFT JOIN LedgerHeadDetail Lhd On H.DocId = Lhd.DocId
                    LEFT JOIN ViewHelpSubgroup Sg On H.SubCode = Sg.Code " & mRecCondStr &
                    " And IfNull(H.Discount,0) <> 0 "

            RepTitle = "Student Ledger" : RepName = "StudentLedger"

            mQry = " Select NUll As V_Date, VMain.Student As Student, 'Opening' As Narration, 
                    Case When IfNull(Sum(VMain.AmtDr),0) - IfNull(Sum(VMain.AmtCr),0) > 0 Then IfNull(Sum(VMain.AmtDr),0) - IfNull(Sum(VMain.AmtCr),0) Else 0 End As AmtDr,
                    Case When IfNull(Sum(VMain.AmtDr),0) - IfNull(Sum(VMain.AmtCr),0) < 0 Then Abs(IfNull(Sum(VMain.AmtDr),0) - IfNull(Sum(VMain.AmtCr),0)) Else 0 End As AmtCr
                    From (" & mFeeDueQry & " UNION ALL 
                          " & mLateFeeDueQry & " UNION ALL 
                          " & mFeeRecQry & " UNION ALL 
                          " & mFeeDiscQry & ") As VMain 
                    Where Date(VMain.V_Date) < " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " 
                    Group By VMain.Student
                    Having IfNull(Sum(VMain.AmtDr),0) - IfNull(Sum(VMain.AmtCr),0) <> 0 "

            mQry += " UNION ALL "

            mQry += " Select VMain.V_Date, VMain.Student As Student, VMain.Narration As Narration, 
                    VMain.AmtDr, VMain.AmtCr
                    From (" & mFeeDueQry & " UNION ALL 
                          " & mLateFeeDueQry & " UNION ALL 
                          " & mFeeRecQry & " UNION ALL 
                          " & mFeeDiscQry & ") As VMain
                    Where Date(VMain.V_Date) >= " & AgL.Chk_Date(CDate(ReportFrm.FGetText(rowFromDate)).ToString("s")) & " 
                    Order By VMain.V_Date "
            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")
            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
End Class
