Public Class ClsReportProcedures

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

#Region "Common Reports Constant"
    Private Const CityList As String = "CityList"
    Private Const UserWiseEntryReport As String = "UserWiseEntryReport"
    Private Const UserWiseEntryTargetReport As String = "UserWiseEntryTargetReport"
#End Region

#Region "Reports Constant"
    Private Const QualityMasterReport As String = "QualityMasterReport"
    Private Const QualityMasterLogReport As String = "QualityMasterLogReport"
    Private Const DesignMasterReport As String = "DesignMasterReport"
    Private Const DesignMasterLogReport As String = "DesignMasterLogReport"


#End Region

#Region "Queries Definition"
    Dim mHelpCityQry$ = "Select Convert(BIT,0) As [Select],CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select Convert(BIT,0) As [Select],State_Code, State_Desc From State "
    Dim mHelpUserQry$ = "Select Convert(BIT,0) As [Select],User_Name As Code, User_Name As [User] From UserMast "
    Dim mHelpSiteQry$ = "Select Convert(BIT,0) As [Select], Code, Name As [Site] From SiteMast Where " & AgL.PubSiteCondition("Code", AgL.PubSiteCode) & " "

#End Region

    Dim DsRep As DataSet = Nothing, DsRep1 As DataSet = Nothing, DsRep2 As DataSet = Nothing
    Dim mQry$ = "", RepName$ = "", RepTitle$ = ""

#Region "Initializing Grid"
    Public Sub Ini_Grid()
        Try
            Dim I As Integer = 0
            Select Case GRepFormName

                Case QualityMasterReport
                    'ObjRFG.CreateHelpGrid(mHelpConstructionQry, "Costruction")


            End Select
            Call ObjRFG.Arrange_Grid()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region


    Private Sub ObjRepFormGlobal_ProcessReport() Handles ObjRFG.ProcessReport
        Select Case mGRepFormName

            Case QualityMasterReport
                ProcQualityMasterReport()


        End Select
    End Sub

    Public Sub New(ByVal mObjRepFormGlobal As AgLibrary.RepFormGlobal)
        ObjRFG = mObjRepFormGlobal
    End Sub

#Region "Quality Master Report"
    Private Sub ProcQualityMasterReport()
        Try
            Call ObjRFG.FillGridString()

            Dim mCondStr$ = ""

            mCondStr = " where 1=1 "

            mCondStr = mCondStr & ObjRFG.GetWhereCondition("RQ.Construction", 0)

            mQry = " SELECT RQ.Code,RQ.ManualCode,RQ.Description,RQ.Construction,RQ.StdRugWeight,RQ.PileWeight,RQ.PileHeight," & _
                    " RQ.TuftPerSqrInch,RQ.WashingType,RQ.Clipping,RQ.Fringes,RQ.TotalQty,RQ.EntryBy,RQ.EntryDate,RQ.EntryType, " & _
                    " RQ.EntryStatus, RQ.ApproveBy, RQ.ApproveDate, RQ.DiscardBy, RQ.DiscardDate, RQ.MoveToLog, RQ.MoveToLogDate, RQ.IsDeleted " & _
                    " FROM RUG_Quality RQ " & _
                    " " & mCondStr & ""

            DsRep = AgL.FillData(mQry, AgL.GCn)
            RepName = "RUG_QualityMasterReport" : RepTitle = "Quality Master Report"

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ObjRFG.PrintReport(DsRep, RepName, RepTitle, ReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try


    End Sub
#End Region


End Class
