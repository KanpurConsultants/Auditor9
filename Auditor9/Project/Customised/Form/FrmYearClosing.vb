Imports System.Drawing.Printing
Imports System.IO
Imports System.Linq
Imports System.Net
Imports Excel

Public Class FrmYearClosing
    Dim AgL As AgLibrary.ClsMain
    Dim mConnectionStr As String = "", mQry As String
    Public Sub New(ByVal AgLibVar As AgLibrary.ClsMain)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        AgL = AgLibVar
    End Sub
    Private Sub FrmReportPrint_Load(sender As Object, e As EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 654, 990, 0, 0)
        'Me.Location = New System.Drawing.Point(0, 0)
    End Sub
    Private Sub FrmReportPrint_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click, BtnCancel.Click
        Dim mTrans As String = ""


        Dim MyCommand As OleDb.OleDbDataAdapter = Nothing
        Select Case sender.name
            Case BtnOK.Name
                If TxtPassword.Text = "P@ssw0rd!" Then
                    Try
                        AgL.ECmd = AgL.GCn.CreateCommand
                        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                        AgL.ECmd.Transaction = AgL.ETrans
                        mTrans = "Begin"
                        FProcess1920(AgL.GCn, AgL.ECmd, AgL)
                        FProcess2021(AgL.GCn, AgL.ECmd, AgL)
                        FProcess2022(AgL.GCn, AgL.ECmd, AgL)
                        FProcess2023(AgL.GCn, AgL.ECmd, AgL)
                        FProcess202324(AgL.GCn, AgL.ECmd, AgL)
                        FProcess202425(AgL.GCn, AgL.ECmd, AgL)
                        FProcess202526(AgL.GCn, AgL.ECmd, AgL)
                        AgL.ETrans.Commit()
                        mTrans = "Commit"
                        MsgBox("Process Completed...!", MsgBoxStyle.Information)
                    Catch ex As Exception
                        AgL.ETrans.Rollback()
                        MsgBox(ex.Message)
                    End Try
                Else
                    MsgBox("Incorrect Password...!", MsgBoxStyle.Information)
                End If

            Case BtnCancel.Name
                Me.Close()
        End Select
    End Sub
    Public Shared Sub FProcess1920(Conn As Object, Cmd As Object, Agl As AgLibrary.ClsMain)
        Dim mQry As String = ""
        If Agl.VNull(Agl.Dman_Execute("SELECT Count(*) AS CompanyCnt 
                                FROM Company WHERE CYear = '2019-2020'", IIf(Agl.PubServerName = "", Agl.GCn, Agl.GcnRead)).ExecuteScalar()) = 0 Then
            Dim bNewCompCode As String = Agl.Dman_Execute("SELECT IsNull(CAST(C.Comp_Code AS INT),0) + 1 AS CompCode  
                                            FROM Company C", IIf(Agl.PubServerName = "", Agl.GCn, Agl.GcnRead)).ExecuteScalar().ToString()

            mQry = " INSERT INTO Company (Comp_Code,Div_Code,Comp_Name,CentralData_Path,PrevDBName,DbPrefix,Repo_Path,Start_Dt,End_Dt,address1,address2,city,
                    pin,phone,Email,fax,lstno,lstdate,cstno,cstdate,cyear,pyear,State,U_Name,U_EntDt,U_AE,DeletedYN,Country,V_Prefix,SerialKeyNo)
                    Select '" & bNewCompCode & "' As Comp_Code,Div_Code,Comp_Name,CentralData_Path,PrevDBName,DbPrefix,Repo_Path,
                    '2019-04-01 00:00:00' As Start_Dt,
                    '2020-03-31 00:00:00' As End_Dt,address1,address2,city,
                    pin,phone,Email,fax,lstno,lstdate,cstno,cstdate,
                    '2019-2020' As cyear,
                    '2018-2019' As pyear,State,U_Name,U_EntDt,U_AE,DeletedYN,Country,
                    '2019' As V_Prefix,SerialKeyNo
                    From Company Where Comp_Code = '1'"
            Agl.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        mQry = "INSERT INTO Voucher_Prefix(V_Type,
                   Date_From,
                   Prefix,
                   Start_Srl_No,
                   Date_To,
                   Comp_Code,
                   Site_Code,
                   Div_Code,
                   UpLoadDate,
                   Status_Add,
                   Status_Edit,
                   Status_Delete,
                   Status_Print)
            SELECT Vp.V_Type,
                   '2019-04-01' As Date_From,
                   '2019' As Prefix,
                   0 As Start_Srl_No,
                   '2020-03-31 23:59:59' As Date_To,
                   Vp.Comp_Code,
                   Vp.Site_Code,
                   Vp.Div_Code,
                   Vp.UpLoadDate,
                   Vp.Status_Add,
                   Vp.Status_Edit,
                   Vp.Status_Delete,
                   Vp.Status_Print
                FROM Voucher_Prefix Vp
                LEFT JOIN (
	                SELECT L.V_Type, L.Prefix FROM Voucher_Prefix L
                ) AS V1 ON Vp.V_Type = V1.V_Type AND V1.Prefix = '2019'
               Where Vp.Prefix = '2018'
                AND V1.V_Type IS NULL "
        Agl.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Public Shared Sub FProcess2021(Conn As Object, Cmd As Object, Agl As AgLibrary.ClsMain)
        Dim mQry As String = ""
        If Agl.VNull(Agl.Dman_Execute("SELECT Count(*) AS CompanyCnt 
                                FROM Company WHERE CYear = '2020-2021'", IIf(Agl.PubServerName = "", Agl.GCn, Agl.GcnRead)).ExecuteScalar()) = 0 Then
            Dim bNewCompCode As String = Agl.Dman_Execute("SELECT IsNull(Max(CAST(C.Comp_Code AS INT)),0) + 1 AS CompCode  
                                            FROM Company C", IIf(Agl.PubServerName = "", Agl.GCn, Agl.GcnRead)).ExecuteScalar().ToString()

            mQry = " INSERT INTO Company (Comp_Code,Div_Code,Comp_Name,CentralData_Path,PrevDBName,DbPrefix,Repo_Path,Start_Dt,End_Dt,address1,address2,city,
                    pin,phone,Email,fax,lstno,lstdate,cstno,cstdate,cyear,pyear,State,U_Name,U_EntDt,U_AE,DeletedYN,Country,V_Prefix,SerialKeyNo)
                    Select '" & bNewCompCode & "' As Comp_Code,Div_Code,Comp_Name,CentralData_Path,PrevDBName,DbPrefix,Repo_Path,
                    " & Agl.Chk_Date("01/Apr/2020") & " As Start_Dt,
                    " & Agl.Chk_Date("31/Mar/2021") & " As End_Dt,address1,address2,city,
                    pin,phone,Email,fax,lstno,lstdate,cstno,cstdate,
                    '2020-2021' As cyear,
                    '2019-2020' As pyear,State,U_Name,U_EntDt,U_AE,DeletedYN,Country,
                    '2020' As V_Prefix,SerialKeyNo
                    From Company Where Comp_Code = '1'"
            Agl.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        mQry = "INSERT INTO Voucher_Prefix(V_Type,
                   Date_From,
                   Prefix,
                   Start_Srl_No,
                   Date_To,
                   Comp_Code,
                   Site_Code,
                   Div_Code,
                   UpLoadDate,
                   Status_Add,
                   Status_Edit,
                   Status_Delete,
                   Status_Print)
            SELECT Vp.V_Type,
                   " & Agl.Chk_Date("01/Apr/2020") & " As Date_From,
                   '2020' As Prefix,
                   0 As Start_Srl_No,
                   " & Agl.Chk_Date("31/Mar/2021") & " As Date_To,
                   Vp.Comp_Code,
                   Vp.Site_Code,
                   Vp.Div_Code,
                   Vp.UpLoadDate,
                   Vp.Status_Add,
                   Vp.Status_Edit,
                   Vp.Status_Delete,
                   Vp.Status_Print
                FROM Voucher_Prefix Vp
                LEFT JOIN (
	                SELECT L.V_Type, L.Prefix FROM Voucher_Prefix L
                ) AS V1 ON Vp.V_Type = V1.V_Type AND V1.Prefix = '2020'
               Where Vp.Prefix = '2019'
                AND V1.V_Type IS NULL "
        Agl.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Public Shared Sub FProcess2022(Conn As Object, Cmd As Object, Agl As AgLibrary.ClsMain)
        Dim mQry As String = ""
        If Agl.VNull(Agl.Dman_Execute("SELECT Count(*) AS CompanyCnt 
                                FROM Company WHERE CYear = '2021-2022'", IIf(Agl.PubServerName = "", Agl.GCn, Agl.GcnRead)).ExecuteScalar()) = 0 Then
            Dim bNewCompCode As String = Agl.Dman_Execute("SELECT IsNull(Max(CAST(C.Comp_Code AS INT)),0) + 1 AS CompCode  
                                            FROM Company C", IIf(Agl.PubServerName = "", Agl.GCn, Agl.GcnRead)).ExecuteScalar().ToString()

            mQry = " INSERT INTO Company (Comp_Code,Div_Code,Comp_Name,CentralData_Path,PrevDBName,DbPrefix,Repo_Path,Start_Dt,End_Dt,address1,address2,city,
                    pin,phone,Email,fax,lstno,lstdate,cstno,cstdate,cyear,pyear,State,U_Name,U_EntDt,U_AE,DeletedYN,Country,V_Prefix,SerialKeyNo)
                    Select '" & bNewCompCode & "' As Comp_Code,Div_Code,Comp_Name,CentralData_Path,PrevDBName,DbPrefix,Repo_Path,
                    " & Agl.Chk_Date("01/Apr/2021") & " As Start_Dt,
                    " & Agl.Chk_Date("31/Mar/2022") & " As End_Dt,address1,address2,city,
                    pin,phone,Email,fax,lstno,lstdate,cstno,cstdate,
                    '2021-2022' As cyear,
                    '2020-2021' As pyear,State,U_Name,U_EntDt,U_AE,DeletedYN,Country,
                    '2021' As V_Prefix,SerialKeyNo
                    From Company Where Comp_Code = '1'"
            Agl.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        mQry = "INSERT INTO Voucher_Prefix(V_Type,
                   Date_From,
                   Prefix,
                   Start_Srl_No,
                   Date_To,
                   Comp_Code,
                   Site_Code,
                   Div_Code,
                   UpLoadDate,
                   Status_Add,
                   Status_Edit,
                   Status_Delete,
                   Status_Print)
            SELECT Vp.V_Type,
                   " & Agl.Chk_Date("01/Apr/2021") & " As Date_From,
                   '2021' As Prefix,
                   0 As Start_Srl_No,
                   " & Agl.Chk_Date("31/Mar/2022") & " As Date_To,
                   Vp.Comp_Code,
                   Vp.Site_Code,
                   Vp.Div_Code,
                   Vp.UpLoadDate,
                   Vp.Status_Add,
                   Vp.Status_Edit,
                   Vp.Status_Delete,
                   Vp.Status_Print
                FROM Voucher_Prefix Vp
                LEFT JOIN (
	                SELECT L.V_Type, L.Prefix FROM Voucher_Prefix L
                ) AS V1 ON Vp.V_Type = V1.V_Type AND V1.Prefix = '2021'
               Where Vp.Prefix = '2020'
                AND V1.V_Type IS NULL "
        Agl.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        mQry = "INSERT INTO UserSite (User_Name, CompCode, Sitelist, UpLoadDate, DivisionList)
                SELECT U.User_Name, (SELECT Cn.Comp_Code FROM Company Cn WHERE cyear = '2021-2022') AS CompCode, U.Sitelist, 
                U.UpLoadDate, U.DivisionList
                FROM UserSite U
                LEFT JOIN Company C ON U.CompCode = C.Comp_Code
                LEFT JOIN (
	                SELECT Us.User_Name, Us.CompCode 
	                FROM UserSite Us
	                LEFT JOIN Company Cp ON Us.CompCode = Cp.Comp_Code
	                WHERE Cp.cyear =  '2021-2022') AS V1 ON U.User_Name = V1.User_Name 
                WHERE C.cyear = '2020-2021'
                AND V1.User_Name IS NULL "
        Agl.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Public Shared Sub FProcess2023(Conn As Object, Cmd As Object, Agl As AgLibrary.ClsMain)
        Dim mQry As String = ""
        If Agl.VNull(Agl.Dman_Execute("SELECT Count(*) AS CompanyCnt 
                                FROM Company WHERE CYear = '2022-2023'", IIf(Agl.PubServerName = "", Agl.GCn, Agl.GcnRead)).ExecuteScalar()) = 0 Then
            Dim bNewCompCode As String = Agl.Dman_Execute("SELECT IsNull(Max(CAST(C.Comp_Code AS INT)),0) + 1 AS CompCode  
                                            FROM Company C", IIf(Agl.PubServerName = "", Agl.GCn, Agl.GcnRead)).ExecuteScalar().ToString()

            mQry = " INSERT INTO Company (Comp_Code,Div_Code,Comp_Name,CentralData_Path,PrevDBName,DbPrefix,Repo_Path,Start_Dt,End_Dt,address1,address2,city,
                    pin,phone,Email,fax,lstno,lstdate,cstno,cstdate,cyear,pyear,State,U_Name,U_EntDt,U_AE,DeletedYN,Country,V_Prefix,SerialKeyNo)
                    Select '" & bNewCompCode & "' As Comp_Code,Div_Code,Comp_Name,CentralData_Path,PrevDBName,DbPrefix,Repo_Path,
                    " & Agl.Chk_Date("01/Apr/2022") & " As Start_Dt,
                    " & Agl.Chk_Date("31/Mar/2023") & " As End_Dt,address1,address2,city,
                    pin,phone,Email,fax,lstno,lstdate,cstno,cstdate,
                    '2022-2023' As cyear,
                    '2021-2022' As pyear,State,U_Name,U_EntDt,U_AE,DeletedYN,Country,
                    '2022' As V_Prefix,SerialKeyNo
                    From Company Where Comp_Code = '1'"
            Agl.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        mQry = "INSERT INTO Voucher_Prefix(V_Type,
                   Date_From,
                   Prefix,
                   Start_Srl_No,
                   Date_To,
                   Comp_Code,
                   Site_Code,
                   Div_Code,
                   UpLoadDate,
                   Status_Add,
                   Status_Edit,
                   Status_Delete,
                   Status_Print)
            SELECT Vp.V_Type,
                   " & Agl.Chk_Date("01/Apr/2022") & " As Date_From,
                   '2022' As Prefix,
                   0 As Start_Srl_No,
                   " & Agl.Chk_Date("31/Mar/2023") & " As Date_To,
                   Vp.Comp_Code,
                   Vp.Site_Code,
                   Vp.Div_Code,
                   Vp.UpLoadDate,
                   Vp.Status_Add,
                   Vp.Status_Edit,
                   Vp.Status_Delete,
                   Vp.Status_Print
                FROM Voucher_Prefix Vp
                LEFT JOIN (
	                SELECT L.V_Type, L.Prefix FROM Voucher_Prefix L
                ) AS V1 ON Vp.V_Type = V1.V_Type AND V1.Prefix = '2022'
               Where Vp.Prefix = '2021'
                AND V1.V_Type IS NULL "
        Agl.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        mQry = "INSERT INTO UserSite (User_Name, CompCode, Sitelist, UpLoadDate, DivisionList)
                SELECT U.User_Name, (SELECT Cn.Comp_Code FROM Company Cn WHERE cyear = '2022-2023') AS CompCode, U.Sitelist, 
                U.UpLoadDate, U.DivisionList
                FROM UserSite U
                LEFT JOIN Company C ON U.CompCode = C.Comp_Code
                LEFT JOIN (
	                SELECT Us.User_Name, Us.CompCode 
	                FROM UserSite Us
	                LEFT JOIN Company Cp ON Us.CompCode = Cp.Comp_Code
	                WHERE Cp.cyear =  '2022-2023') AS V1 ON U.User_Name = V1.User_Name 
                WHERE C.cyear = '2021-2022'
                AND V1.User_Name IS NULL "
        Agl.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub
    Public Shared Sub FProcess202324(Conn As Object, Cmd As Object, Agl As AgLibrary.ClsMain)
        Dim mQry As String = ""
        If Agl.VNull(Agl.Dman_Execute("SELECT Count(*) AS CompanyCnt 
                                FROM Company WHERE CYear = '2023-2024'", IIf(Agl.PubServerName = "", Agl.GCn, Agl.GcnRead)).ExecuteScalar()) = 0 Then
            Dim bNewCompCode As String = Agl.Dman_Execute("SELECT IsNull(Max(CAST(C.Comp_Code AS INT)),0) + 1 AS CompCode  
                                            FROM Company C", IIf(Agl.PubServerName = "", Agl.GCn, Agl.GcnRead)).ExecuteScalar().ToString()

            mQry = " INSERT INTO Company (Comp_Code,Div_Code,Comp_Name,CentralData_Path,PrevDBName,DbPrefix,Repo_Path,Start_Dt,End_Dt,address1,address2,city,
                    pin,phone,Email,fax,lstno,lstdate,cstno,cstdate,cyear,pyear,State,U_Name,U_EntDt,U_AE,DeletedYN,Country,V_Prefix,SerialKeyNo)
                    Select '" & bNewCompCode & "' As Comp_Code,Div_Code,Comp_Name,CentralData_Path,PrevDBName,DbPrefix,Repo_Path,
                    " & Agl.Chk_Date("01/Apr/2023") & " As Start_Dt,
                    " & Agl.Chk_Date("31/Mar/2024") & " As End_Dt,address1,address2,city,
                    pin,phone,Email,fax,lstno,lstdate,cstno,cstdate,
                    '2023-2024' As cyear,
                    '2022-2023' As pyear,State,U_Name,U_EntDt,U_AE,DeletedYN,Country,
                    '2023' As V_Prefix,SerialKeyNo
                    From Company Where Comp_Code = '1'"
            Agl.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        mQry = "INSERT INTO Voucher_Prefix(V_Type,
                   Date_From,
                   Prefix,
                   Start_Srl_No,
                   Date_To,
                   Comp_Code,
                   Site_Code,
                   Div_Code,
                   UpLoadDate,
                   Status_Add,
                   Status_Edit,
                   Status_Delete,
                   Status_Print)
            SELECT Vp.V_Type,
                   " & Agl.Chk_Date("01/Apr/2023") & " As Date_From,
                   '2023' As Prefix,
                   0 As Start_Srl_No,
                   " & Agl.Chk_Date("31/Mar/2024") & " As Date_To,
                   Vp.Comp_Code,
                   Vp.Site_Code,
                   Vp.Div_Code,
                   Vp.UpLoadDate,
                   Vp.Status_Add,
                   Vp.Status_Edit,
                   Vp.Status_Delete,
                   Vp.Status_Print
                FROM Voucher_Prefix Vp
                LEFT JOIN (
	                SELECT L.V_Type, L.Prefix FROM Voucher_Prefix L
                ) AS V1 ON Vp.V_Type = V1.V_Type AND V1.Prefix = '2023'
               Where Vp.Prefix = '2022'
                AND V1.V_Type IS NULL "
        Agl.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        mQry = "INSERT INTO UserSite (User_Name, CompCode, Sitelist, UpLoadDate, DivisionList)
                SELECT U.User_Name, (SELECT Cn.Comp_Code FROM Company Cn WHERE cyear = '2023-2024') AS CompCode, U.Sitelist, 
                U.UpLoadDate, U.DivisionList
                FROM UserSite U
                LEFT JOIN Company C ON U.CompCode = C.Comp_Code
                LEFT JOIN (
	                SELECT Us.User_Name, Us.CompCode 
	                FROM UserSite Us
	                LEFT JOIN Company Cp ON Us.CompCode = Cp.Comp_Code
	                WHERE Cp.cyear =  '2023-2024') AS V1 ON U.User_Name = V1.User_Name 
                WHERE C.cyear = '2022-2023'
                AND V1.User_Name IS NULL "
        Agl.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Public Shared Sub FProcess202425(Conn As Object, Cmd As Object, Agl As AgLibrary.ClsMain)
        Dim mQry As String = ""
        If Agl.VNull(Agl.Dman_Execute("SELECT Count(*) AS CompanyCnt 
                                FROM Company WHERE CYear = '2024-2025'", IIf(Agl.PubServerName = "", Agl.GCn, Agl.GcnRead)).ExecuteScalar()) = 0 Then
            'Dim bNewCompCode As String = Agl.Dman_Execute("SELECT IsNull(Max(CAST(C.Comp_Code AS INT)),0) + 1 AS CompCode  
            '                                FROM Company C", IIf(Agl.PubServerName = "", Agl.GCn, Agl.GcnRead)).ExecuteScalar().ToString()

            Dim bNewCompCode As String = "7"

            mQry = " INSERT INTO Company (Comp_Code,Div_Code,Comp_Name,CentralData_Path,PrevDBName,DbPrefix,Repo_Path,Start_Dt,End_Dt,address1,address2,city,
                    pin,phone,Email,fax,lstno,lstdate,cstno,cstdate,cyear,pyear,State,U_Name,U_EntDt,U_AE,DeletedYN,Country,V_Prefix,SerialKeyNo)
                    Select '" & bNewCompCode & "' As Comp_Code,Div_Code,Comp_Name,CentralData_Path,PrevDBName,DbPrefix,Repo_Path,
                    " & Agl.Chk_Date("01/Apr/2024") & " As Start_Dt,
                    " & Agl.Chk_Date("31/Mar/2025") & " As End_Dt,address1,address2,city,
                    pin,phone,Email,fax,lstno,lstdate,cstno,cstdate,
                    '2024-2025' As cyear,
                    '2022-2024' As pyear,State,U_Name,U_EntDt,U_AE,DeletedYN,Country,
                    '2024' As V_Prefix,SerialKeyNo
                    From Company Where Comp_Code = '6'"
            Agl.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        mQry = "INSERT INTO Voucher_Prefix(V_Type,
                   Date_From,
                   Prefix,
                   Start_Srl_No,
                   Date_To,
                   Comp_Code,
                   Site_Code,
                   Div_Code,
                   UpLoadDate,
                   Status_Add,
                   Status_Edit,
                   Status_Delete,
                   Status_Print)
            SELECT Vp.V_Type,
                   " & Agl.Chk_Date("01/Apr/2024") & " As Date_From,
                   '2024' As Prefix,
                   0 As Start_Srl_No,
                   " & Agl.Chk_Date("31/Mar/2025") & " As Date_To,
                   Vp.Comp_Code,
                   Vp.Site_Code,
                   Vp.Div_Code,
                   Vp.UpLoadDate,
                   Vp.Status_Add,
                   Vp.Status_Edit,
                   Vp.Status_Delete,
                   Vp.Status_Print
                   FROM Voucher_Prefix Vp
                    LEFT JOIN 
                    (
	                    SELECT L.V_Type, L.Prefix FROM Voucher_Prefix L
                    ) AS V1 ON Vp.V_Type = V1.V_Type AND V1.Prefix = '2024'
                   Where Vp.Prefix = '2023'
                   AND V1.V_Type IS NULL "
        Agl.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        mQry = "INSERT INTO UserSite (User_Name, CompCode, Sitelist, UpLoadDate, DivisionList)
                SELECT U.User_Name, (SELECT Cn.Comp_Code FROM Company Cn WHERE cyear = '2024-2025') AS CompCode, U.Sitelist, 
                U.UpLoadDate, U.DivisionList
                FROM UserSite U
                LEFT JOIN Company C ON U.CompCode = C.Comp_Code
                LEFT JOIN (
	                SELECT Us.User_Name, Us.CompCode 
	                FROM UserSite Us
	                LEFT JOIN Company Cp ON Us.CompCode = Cp.Comp_Code
	                WHERE Cp.cyear =  '2024-2025') AS V1 ON U.User_Name = V1.User_Name 
                WHERE C.cyear = '2023-2024'
                AND V1.User_Name IS NULL "
        Agl.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Public Shared Sub FProcess202526(Conn As Object, Cmd As Object, Agl As AgLibrary.ClsMain)
        Dim mQry As String = ""
        If Agl.VNull(Agl.Dman_Execute("SELECT Count(*) AS CompanyCnt 
                                FROM Company WHERE CYear = '2025-2026'", IIf(Agl.PubServerName = "", Agl.GCn, Agl.GcnRead)).ExecuteScalar()) = 0 Then

            Dim bNewCompCode As String = "8"

            mQry = " INSERT INTO Company (Comp_Code,Div_Code,Comp_Name,CentralData_Path,PrevDBName,DbPrefix,Repo_Path,Start_Dt,End_Dt,address1,address2,city,
                    pin,phone,Email,fax,lstno,lstdate,cstno,cstdate,cyear,pyear,State,U_Name,U_EntDt,U_AE,DeletedYN,Country,V_Prefix,SerialKeyNo)
                    Select '" & bNewCompCode & "' As Comp_Code,Div_Code,Comp_Name,CentralData_Path,PrevDBName,DbPrefix,Repo_Path,
                    " & Agl.Chk_Date("01/Apr/2025") & " As Start_Dt,
                    " & Agl.Chk_Date("31/Mar/2026") & " As End_Dt,address1,address2,city,
                    pin,phone,Email,fax,lstno,lstdate,cstno,cstdate,
                    '2025-2026' As cyear,
                    '2024-2025' As pyear,State,U_Name,U_EntDt,U_AE,DeletedYN,Country,
                    '2025' As V_Prefix,SerialKeyNo
                    From Company Where Comp_Code = '7'"
            Agl.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        mQry = "INSERT INTO Voucher_Prefix(V_Type,
                   Date_From,
                   Prefix,
                   Start_Srl_No,
                   Date_To,
                   Comp_Code,
                   Site_Code,
                   Div_Code,
                   UpLoadDate,
                   Status_Add,
                   Status_Edit,
                   Status_Delete,
                   Status_Print)
            SELECT Vp.V_Type,
                   " & Agl.Chk_Date("01/Apr/2025") & " As Date_From,
                   '2025' As Prefix,
                   0 As Start_Srl_No,
                   " & Agl.Chk_Date("31/Mar/2026") & " As Date_To,
                   Vp.Comp_Code,
                   Vp.Site_Code,
                   Vp.Div_Code,
                   Vp.UpLoadDate,
                   Vp.Status_Add,
                   Vp.Status_Edit,
                   Vp.Status_Delete,
                   Vp.Status_Print
                   FROM Voucher_Prefix Vp
                    LEFT JOIN 
                    (
	                    SELECT L.V_Type, L.Prefix FROM Voucher_Prefix L
                    ) AS V1 ON Vp.V_Type = V1.V_Type AND V1.Prefix = '2025'
                   Where Vp.Prefix = '2024'
                   AND V1.V_Type IS NULL "
        Agl.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        mQry = "INSERT INTO UserSite (User_Name, CompCode, Sitelist, UpLoadDate, DivisionList)
                SELECT U.User_Name, (SELECT Cn.Comp_Code FROM Company Cn WHERE cyear = '2025-2026') AS CompCode, U.Sitelist, 
                U.UpLoadDate, U.DivisionList
                FROM UserSite U
                LEFT JOIN Company C ON U.CompCode = C.Comp_Code
                LEFT JOIN (
	                SELECT Us.User_Name, Us.CompCode 
	                FROM UserSite Us
	                LEFT JOIN Company Cp ON Us.CompCode = Cp.Comp_Code
	                WHERE Cp.cyear =  '2025-2026') AS V1 ON U.User_Name = V1.User_Name 
                WHERE C.cyear = '2024-2025'
                AND V1.User_Name IS NULL "
        Agl.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

End Class