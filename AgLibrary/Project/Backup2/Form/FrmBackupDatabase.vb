Public Class FrmBackupDatase
    Dim AgL As AgLibrary.ClsMain
    Dim mQry$

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            IniGrid()
            FIniMaster()
            LblInfo.Text = "" : LblInfo.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmBackupDatase_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub

    Private Sub IniGrid()
        '<Exicutable Code>
    End Sub

    Sub FIniMaster()
        '<Exicutable Code>
    End Sub

    Private Sub Btn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBackupDatabase.Click
        Dim mSearchCode$ = ""

        Try
            Select Case sender.name
                Case BtnBackupDatabase.Name
                    If MsgBox("Are You Sure To Take Database Backup? ", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

                    mSearchCode = "Database Backup"

                    Call BackupDataBase(AgL.GCn, AgL.ECompConn, AgL.GCnRep, AgL.GcnImage)

            End Select

            Call AgL.LogTableEntry(mSearchCode, Me.Text, AgLibrary.ClsConstant.BackupDatabaseMode, AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, , mSearchCode, , , , , AgL.PubSiteCode, AgL.PubDivCode)

        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Public Function BackupDataBase(ByVal mConn As SqlClient.SqlConnection, ByVal mCompConn As SqlClient.SqlConnection, _
                                   Optional ByVal mAgReportConn As SqlClient.SqlConnection = Nothing, _
                                   Optional ByVal mImageConn As SqlClient.SqlConnection = Nothing) As Boolean

        Dim StrBackupPath As String, DataFileName As String, TransactionFileName As String
        Dim mBackupFile As String = ""
        Dim AgZip As New FrmAgZip(AgL)
        Try
            Me.Cursor = Cursors.WaitCursor

            BackupDataBase = True
            Me.Refresh()
            LblInfo.Text = "" : LblInfo.Refresh()

            ' BackUp Current Database
            If AgL.PubDataBackUpPath Is Nothing Then AgL.PubDataBackUpPath = ""
            If AgL.PubDataBackUpPath.Trim = "" Then
                AgL.PubDataBackUpPath = AgL.FunGetDataFilePath(mConn)
            End If

            DataFileName = "" : TransactionFileName = ""

            DataFileName = AgL.FunGetLogicalDataFileName(mConn)
            TransactionFileName = AgL.FunGetLogicalLogFileName(mConn)

            ' BackUp Database
            StrBackupPath = AgL.PubDataBackUpPath

            mBackupFile = StrBackupPath & AgL.PubDBName & "_" & CStr(Replace(Replace(Replace(Format(Now, "dd/MMM/yy HH:mm:ss"), "/", ""), ":", ""), " ", "_")) + ".bak"

            LblInfo.Text = "Creating Backup Of Current Database ... " & vbCrLf & "Backup File Path : " & mBackupFile
            LblInfo.Refresh()


            If System.IO.File.Exists(mBackupFile) Then
                System.IO.File.Delete(mBackupFile)
            End If

            AgL.Dman_ExecuteNonQry("BACKUP DATABASE " & AgL.PubDBName & "  TO  Disk =  '" & mBackupFile & "' ", mConn)

            LblInfo.Text = "Current Database Backup Completed ..."
            LblInfo.Refresh()


            ' BackUp Company Database
            If Not AgL.StrCmp(AgL.PubDBName, AgL.PubCompanyDBName) Then
                LblInfo.Text = "Creating Backup Of Company Database ..." : LblInfo.Refresh()


                DataFileName = "" : TransactionFileName = "" : mBackupFile = ""

                DataFileName = AgL.FunGetLogicalDataFileName(mCompConn)
                TransactionFileName = AgL.FunGetLogicalLogFileName(mCompConn)


                mBackupFile = AgL.PubDataBackUpPath & AgL.PubCompanyDBName & "_" & CStr(Replace(Replace(Replace(Format(Now, "dd/MMM/yy HH:mm:ss"), "/", ""), ":", ""), " ", "_")) + ".bak"

                LblInfo.Text = "Creating Backup Of Company Database ... " & vbCrLf & "Backup File Path : " & mBackupFile
                LblInfo.Refresh()

                If System.IO.File.Exists(mBackupFile) Then
                    System.IO.File.Delete(mBackupFile)
                End If

                AgL.Dman_ExecuteNonQry("BACKUP DATABASE " & AgL.PubCompanyDBName & "  TO  Disk =  '" & mBackupFile & "' ", mCompConn)
            End If

            If AgL.PubAgReportPath.Trim <> "" And mAgReportConn IsNot Nothing Then
                LblInfo.Text = "Creating Backup Of Report Database ..." : LblInfo.Refresh()

                DataFileName = "" : TransactionFileName = "" : mBackupFile = ""

                DataFileName = AgL.FunGetLogicalDataFileName(mAgReportConn)
                TransactionFileName = AgL.FunGetLogicalLogFileName(mAgReportConn)


                mBackupFile = AgL.PubDataBackUpPath & AgL.PubAgReportPath & "_" & CStr(Replace(Replace(Replace(Format(Now, "dd/MMM/yy HH:mm:ss"), "/", ""), ":", ""), " ", "_")) + ".bak"

                LblInfo.Text = "Creating Backup Of Report Database ... " & vbCrLf & "Backup File Path : " & mBackupFile
                LblInfo.Refresh()

                If System.IO.File.Exists(mBackupFile) Then
                    System.IO.File.Delete(mBackupFile)
                End If

                AgL.Dman_ExecuteNonQry("BACKUP DATABASE " & AgL.PubAgReportPath & "  TO  Disk =  '" & mBackupFile & "' ", mAgReportConn)
            End If

            If AgL.XNull(AgL.PubImageDBName).ToString.Trim <> "" And mImageConn IsNot Nothing Then
                LblInfo.Text = "Creating Backup Of Image Database ..." : LblInfo.Refresh()

                DataFileName = "" : TransactionFileName = "" : mBackupFile = ""

                DataFileName = AgL.FunGetLogicalDataFileName(mImageConn)
                TransactionFileName = AgL.FunGetLogicalLogFileName(mImageConn)


                mBackupFile = AgL.PubDataBackUpPath & AgL.PubImageDBName & "_" & CStr(Replace(Replace(Replace(Format(Now, "dd/MMM/yy HH:mm:ss"), "/", ""), ":", ""), " ", "_")) + ".bak"

                LblInfo.Text = "Creating Backup Of Image Database ... " & vbCrLf & "Backup File Path : " & mBackupFile
                LblInfo.Refresh()

                If System.IO.File.Exists(mBackupFile) Then
                    System.IO.File.Delete(mBackupFile)
                End If

                AgL.Dman_ExecuteNonQry("BACKUP DATABASE " & AgL.PubImageDBName & "  TO  Disk =  '" & mBackupFile & "' ", mImageConn)
            End If

            BackupDataBase = False

            MsgBox("Backup Process Completed!...")

        Catch ex As Exception
            BackupDataBase = True
            MsgBox(ex.Message, vbCritical, Me.Text)
        Finally
            Me.Refresh()
            LblInfo.Text = "" : LblInfo.Refresh()
            Me.Cursor = Cursors.Default
        End Try
    End Function

#Region "Class Initialization"
    Public Sub New(ByVal AgLibVar As ClsMain)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AgL = AgLibVar
    End Sub
#End Region

    
End Class