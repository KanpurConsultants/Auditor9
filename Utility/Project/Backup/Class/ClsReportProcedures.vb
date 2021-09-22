Public Class ClsReportProcedures

    Public Sub New(ByVal mObjRepFormGlobal As AgLibrary.RepFormGlobal)
        ObjRFG = mObjRepFormGlobal
    End Sub

#Region "Danger Zone"
    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim WithEvents ObjRFG As AgLibrary.RepFormGlobal

    Public Property GRepFormName() As String
        Get
            GRepFormName = mGRepFormName
        End Get
        Set(ByVal value As String)
            mGRepFormName = value
        End Set
    End Property

#End Region

    Dim DsRep As DataSet = Nothing
    Dim mQry$ = "", RepName$ = "", RepTitle$ = ""

    Private Const UserWiseEntryReport As String = "UserWiseEntryReport"
    Private Const UserWiseEntryTargetReport As String = "UserWiseEntryTargetReport"


#Region "Queries Definition"
    Dim mHelpSalesManQry$ = "Select Convert(BIT,0) As [Select], SubCode As Code, Name As [SalesMan Name] From SubGroup Where 1=1 And " & AgL.PubSiteConditionCommonAc(AgL.PubIsHo, "Site_Code", AgL.PubSiteCode, "CommonAc") & " "
    Dim mHelpCustomerQry$ = "Select Convert(BIT,0) As [Select], SubCode As Code, Name As [Customer Name] From SubGroup Where Nature In ('Customer') And " & AgL.PubSiteConditionCommonAc(AgL.PubIsHo, "Site_Code", AgL.PubSiteCode, "CommonAc") & " "
    Dim mHelpAstrologerQry$ = "Select Convert(BIT,0) As [Select], SubCode As Code, Name As [Astrologer Name] From SubGroup Where 1=1 And " & AgL.PubSiteConditionCommonAc(AgL.PubIsHo, "Site_Code", AgL.PubSiteCode, "CommonAc") & " "
    Dim mHelpVehicleQry$ = "Select Convert(BIT,0) As [Select], Code As Code, Description As [Vehicle ] From Vehicle  "
    Dim mHelpVehicleDescriptionQry$ = "Select DISTINCT Convert(BIT,0) As [Select], Code, Description As [Vehicla No] From Vehicle "
    Dim mHelpCourierCompanyQry$ = "Select DISTINCT Convert(BIT,0) As [Select], Code, Description As [Courier Company] From CourierCompany "
    Dim mHelpPartyQry$ = "Select Convert(BIT,0) As [Select], SubCode As Code, Name As [Party Name] From SubGroup Where Nature In ('Customer','Supplier')  And " & AgL.PubSiteConditionCommonAc(AgL.PubIsHo, "Site_Code", AgL.PubSiteCode, "CommonAc") & ""

    Dim mHelpUserQry$ = "Select Convert(BIT,0) As [Select],User_Name As Code, User_Name As [User] From UserMast "
    Dim mHelpEntryPointQry$ = " Select Distinct Convert(BIT,0) As [Select], User_Permission.MnuText AS code , User_Permission.MnuText As [Entry Point] From User_Permission  "

#End Region

#Region "Initializing Grid"
    Public Sub Ini_Grid()
        Try
            Dim I As Integer = 0
            Select Case GRepFormName
                Case UserWiseEntryReport
                    Call ObjRFG.Ini_Grp("From Date", AgL.PubStartDate, "To Date", AgL.PubLoginDate)

                    ObjRFG.CreateHelpGrid(mHelpUserQry, "User")
                    ObjRFG.CreateHelpGrid("Select Distinct Convert(BIT,0) As [Select],EntryPoint As Code, EntryPoint As [Module] From LogTable ", "Module")


                Case UserWiseEntryTargetReport
                    StrArr1 = New String() {"Summary", "Detail"}
                    Call ObjRFG.Ini_Grp("From Date", AgL.PubStartDate, "To Date", AgL.PubLoginDate, "Type", StrArr1)

                    ObjRFG.CreateHelpGrid(mHelpUserQry, "UserName")
                    ObjRFG.CreateHelpGrid(mHelpEntryPointQry, "Entry")

            End Select
            Call ObjRFG.Arrange_Grid()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region


    Private Sub ObjRepFormGlobal_ProcessReport() Handles ObjRFG.ProcessReport
        Select Case mGRepFormName

            Case UserWiseEntryReport
                ProcUserWiseEntryReport()

            Case UserWiseEntryTargetReport
                ProcUserWiseEntryTargetReport()

        End Select
    End Sub

#Region "User Wise Entry Report"
    Private Sub ProcUserWiseEntryReport()
        Try

            Call ObjRFG.FillGridString()

            Dim mCondStr As String = ""


            If ObjRFG.IsRequiredField(AgLibrary.ClsMain.ReportFormGlobalControls.Date1_Control) Then Exit Sub
            If ObjRFG.IsRequiredField(AgLibrary.ClsMain.ReportFormGlobalControls.Date2_Control) Then Exit Sub

            mCondStr = mCondStr & " And CONVERT(SMALLDATETIME,REPLACE(CONVERT(VARCHAR, L.U_EntDt,106),' ','/')) Between " & AgL.ConvertDate(ObjRFG.ParameterDate1_Value) & " And " & AgL.ConvertDate(ObjRFG.ParameterDate2_Value) & " "

            mCondStr = mCondStr & ObjRFG.GetWhereCondition("L.U_Name", 0)
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("L.EntryPoint", 1)

            mQry = "Select L.U_Name, L.EntryPoint, Count(Case When U_AE='A' Then 1 End) As [Add], Count(Case When U_AE='E' Then 1 End) As [Edit], " & _
                   "Count(Case When U_AE='D' Then 1 End) As [Delete], Count(Case When U_AE='P' Then 1 End) As [Print], 0 As Email, 0 As Sms, 0 As Fax, " & _
                   "'" & ObjRFG.GetHelpString(0) & "' As SelGrid1, '" & ObjRFG.GetHelpString(1) & "' As SelGrid2 " & _
                   "From LogTable L "

            mQry = mQry & " Where 1=1  " & mCondStr
            mQry = mQry & " Group By L.U_Name, L.EntryPoint "
            DsRep = AgL.FillData(mQry, AgL.GCn)
            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")
            RepName = "UserWiseEntryReport" : RepTitle = "User Wise Entry Report"

            ObjRFG.PrintReport(DsRep, RepName, RepTitle, AgL.PubReportPath_Utility)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "User Wise Target Entry Detail"
    Private Sub ProcUserWiseEntryTargetReport()
        Try

            Call ObjRFG.FillGridString()

            Dim mCondStr As String = ""
            Dim mDays As Long = 0

            If ObjRFG.IsRequiredField(AgLibrary.ClsMain.ReportFormGlobalControls.Date1_Control) Then Exit Sub
            If ObjRFG.IsRequiredField(AgLibrary.ClsMain.ReportFormGlobalControls.Date2_Control) Then Exit Sub
            If ObjRFG.IsRequiredField(AgLibrary.ClsMain.ReportFormGlobalControls.Cmbo1_Control) Then Exit Sub

            mDays = DateDiff(DateInterval.Day, CDate(ObjRFG.ParameterDate1_Value), CDate(ObjRFG.ParameterDate2_Value))
            mCondStr = mCondStr & " And CONVERT(SMALLDATETIME,REPLACE(CONVERT(VARCHAR, LogTable.U_EntDt,106),' ','/')) Between " & AgL.ConvertDate(ObjRFG.ParameterDate1_Value) & " And " & AgL.ConvertDate(ObjRFG.ParameterDate2_Value) & " "
            mCondStr = mCondStr & " And ((Ut.Date_to >= " & AgL.ConvertDate(ObjRFG.ParameterDate1_Value) & " And Ut.Date_to <= " & AgL.ConvertDate(ObjRFG.ParameterDate2_Value) & ") Or Ut.Date_to Is Null ) "



            mCondStr = mCondStr & ObjRFG.GetWhereCondition("LogTable.U_Name", 0)
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("LogTable.EntryPoint", 1)

            If AgL.StrCmp(ObjRFG.ParameterCmbo1_Value, "Summary") Then
                mCondStr = mCondStr & " GROUP BY LogTable.U_Name ,LogTable.EntryPoint "

                mQry = " SELECT LogTable.EntryPoint,Max(convert(NVARCHAR,LogTable.U_EntDt,3)) as U_Entdt,LogTable.U_Name," & _
                       " sum(CASE WHEN LogTable.u_ae='A' THEN 1 ELSE 0 END) AS ActualAdd, " & _
                       " sum(CASE WHEN LogTable.u_ae='E' THEN 1 ELSE 0 END) AS ActualEdit, " & _
                       " sum(CASE WHEN LogTable.u_ae='D' THEN 1 ELSE 0 END) AS Actualdel, " & _
                       " sum(CASE WHEN LogTable.u_ae='P' THEN 1 ELSE 0 END) AS ActualPrint, " & _
                       " sum(CASE WHEN LogTable.u_ae='F' THEN 1 ELSE 0 END) AS ActualFax, " & _
                       " sum(CASE WHEN LogTable.u_ae='S' THEN 1 ELSE 0 END) AS ActualSms, " & _
                       " sum(CASE WHEN LogTable.u_ae='M' THEN 1 ELSE 0 END) AS ActualEmail, " & _
                       " Convert(Float,max(utd.Add_Target))*" & mDays & " AS AddTarget,Convert(Float,max(utd.Edit_Target))*" & mDays & " AS Edittarget, " & _
                       " Convert(Float,max(utd.Print_Target))*" & mDays & " AS printtarget,Convert(Float,max(utd.Fax_Target))*" & mDays & " AS faxtarget,Convert(Float,max(utd.Email_Target))*" & mDays & " AS Emailtarget, " & _
                       " Convert(Float,max(utd.Sms_Target))*" & mDays & " AS smstarget,'" & ObjRFG.GetHelpString(0) & "' As SelGrid1, '" & ObjRFG.GetHelpString(1) & "' As SelGrid2  " & _
                       " FROM LogTable  LEFT JOIN User_Target ut ON LogTable.U_Name=ut.UserName " & _
                       " LEFT JOIN User_Target_Detail utd ON ut.Code=utd.Code "

                mQry = mQry & " Where LogTable.U_Name <>''  " & mCondStr


                mQry = "Select EntryPoint, U_EntDt, U_Name, ActualAdd, ActualEdit, ActualDel, ActualPrint, ActualFax, " & _
                       "ActualSms, ActualEmail, AddTarget, EditTarget, PrintTarget, FaxTarget, EmailTarget, " & _
                       "SmsTarget, (Case When AddTarget<>0 then (ActualAdd/AddTarget)*100 End) As AddPer, " & _
                       "(Case When EditTarget<>0 then (ActualEdit/EditTarget)*100 End) As EditPer, " & _
                       "(Case When PrintTarget<>0 then (ActualPrint/PrintTarget) End) As PrintPer, SelGrid1, SelGrid2  " & _
                       "From (" & mQry & ") As X "

                DsRep = AgL.FillData(mQry, AgL.GCn)
                If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")


                RepName = "UserWiseEntryTargetReportSummary" : RepTitle = "User Wise Target Entry Summary"
                ObjRFG.PrintReport(DsRep, RepName, RepTitle, AgL.PubReportPath_Utility)
            Else

                mCondStr = mCondStr & " GROUP BY LogTable.U_Name ,LogTable.EntryPoint,convert(NVARCHAR,LogTable.U_EntDt,3) "

                mQry = " SELECT LogTable.EntryPoint,convert(NVARCHAR,LogTable.U_EntDt,3) as U_Entdt,LogTable.U_Name," & _
                       " sum(CASE WHEN LogTable.u_ae='A' THEN 1 ELSE 0 END) AS ActualAdd, " & _
                       " sum(CASE WHEN LogTable.u_ae='E' THEN 1 ELSE 0 END) AS ActualEdit, " & _
                       " sum(CASE WHEN LogTable.u_ae='D' THEN 1 ELSE 0 END) AS Actualdel, " & _
                       " sum(CASE WHEN LogTable.u_ae='P' THEN 1 ELSE 0 END) AS ActualPrint, " & _
                       " sum(CASE WHEN LogTable.u_ae='F' THEN 1 ELSE 0 END) AS ActualFax, " & _
                       " sum(CASE WHEN LogTable.u_ae='S' THEN 1 ELSE 0 END) AS ActualSms, " & _
                       " sum(CASE WHEN LogTable.u_ae='M' THEN 1 ELSE 0 END) AS ActualEmail, " & _
                       " Convert(Float,max(utd.Add_Target)) AS AddTarget,Convert(Float,max(utd.Edit_Target)) AS Edittarget, " & _
                       " Convert(Float,max(utd.Print_Target)) AS printtarget,Convert(Float,max(utd.Fax_Target)) AS faxtarget,Convert(Float,max(utd.Email_Target)) AS Emailtarget, " & _
                       " Convert(Float,max(utd.Sms_Target)) AS smstarget,'" & ObjRFG.GetHelpString(0) & "' As SelGrid1, '" & ObjRFG.GetHelpString(1) & "' As SelGrid2  " & _
                       " FROM LogTable  LEFT JOIN User_Target ut ON LogTable.U_Name=ut.UserName " & _
                       " LEFT JOIN User_Target_Detail utd ON ut.Code=utd.Code  "

                mQry = mQry & " Where LogTable.U_Name <>''  " & mCondStr


                mQry = "Select EntryPoint, U_EntDt, U_Name, ActualAdd, ActualEdit, ActualDel, ActualPrint, ActualFax, " & _
                       "ActualSms, ActualEmail, AddTarget, EditTarget, PrintTarget, FaxTarget, EmailTarget, " & _
                       "SmsTarget, (Case When AddTarget<>0 then (ActualAdd/AddTarget)*100 End) As AddPer, " & _
                       "(Case When EditTarget<>0 then (ActualEdit/EditTarget)*100 End) As EditPer, " & _
                       "(Case When PrintTarget<>0 then (ActualPrint/PrintTarget) End) As PrintPer, SelGrid1, SelGrid2  " & _
                       "From (" & mQry & ") As X "

                DsRep = AgL.FillData(mQry, AgL.GCn)
                If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

                RepName = "UserWiseEntryTargetReport" : RepTitle = "User Wise Target Entry Detail"
                ObjRFG.PrintReport(DsRep, RepName, RepTitle, AgL.PubReportPath_Utility)
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

End Class
