Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class MDICheque
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub MDIMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Dim mCount As Integer = 0
        If e.KeyCode = Keys.Escape Then
            For Each ChildForm As Form In Me.MdiChildren
                mCount = mCount + 1
            Next

            If mCount = 0 Then
                If MsgBox("Do You Want to Exit?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    'End
                End If
            End If
        End If
    End Sub

    Public Function getx()
        Dim dpiX As Double
        Dim dpiPer As Double

        dpiX = Screen.PrimaryScreen.Bounds.Width
        dpiPer = Math.Round(dpiX / 1024, 0)
        MsgBox(dpiPer)
        Return dpiPer
    End Function
    Private Sub MDIMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim DtTemp As DataTable
        Dim mQry As String

        Try
            If AgL Is Nothing Then
                If FOpenIni(StrPath + IniName, AgLibrary.ClsConstant.PubSuperUserName, AgLibrary.ClsConstant.PubSuperUserPassword) Then
                    'If FOpenIni(StrPath + IniName, "Akash", "") Then
                    AgL.PubSiteCode = "1"
                    AgL.PubDivCode = "D"
                    AgL.PubLoginDate = DateTime.Now()
                    AgL.PubLastTransactionDate = Now()
                    'Dim clsf As New ClsMain(AgL)
                    'clsf.UpdateTableStructure()
                    'End

                    AgIniVar.FOpenConnection("2", "1", False)
                End If
                AgL.PubStopWatch.Start()

                AgL.PubDivCode = "D"

                Try
                    mCrd.Load(AgL.PubReportPath & "\" & "SaleInvoice_Print.rpt")
                Catch ex As Exception
                End Try


                MDI_Load_Things(Me)


            End If
        Catch ex As Exception
            MsgBox(ex.Message & " at Mdi Load")
        End Try
    End Sub

    Private Sub Mnu_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs)


        'Dim Cls_Accounts As New AgAccounts.ClsMain(AgL)
        'If AgL.StrCmp(e.ClickedItem.ToolTipText, "Accounts") Then
        '    Dim FrmObj_FromReference As Form = Nothing
        '    Dim objAccountsClsFunction As New AgAccounts.ClsFunction
        '    FrmObj_FromReference = objAccountsClsFunction.FOpen(e.ClickedItem.Name, e.ClickedItem.ToString, TargetEntryType.EntryPoint)
        '    If IsNothing(FrmObj_FromReference) Then Exit Sub
        '    FrmObj_FromReference.MdiParent = Me
        '    AgL.PubSearchRow = ""
        '    FrmObj_FromReference.Show()
        '    FrmObj_FromReference = Nothing
        '    Exit Sub
        'End If


        Dim FrmObj As Form
        Dim CFOpen As New ClsFunction
        Dim mTargetEntryType As TargetEntryType

        If e.ClickedItem.Tag Is Nothing Then e.ClickedItem.Tag = ""
        If e.ClickedItem.Tag.Trim = "" Then
            mTargetEntryType = TargetEntryType.EntryPoint
        ElseIf AgL.StrCmp(e.ClickedItem.Tag.Trim, "Grid Report") Then
            mTargetEntryType = TargetEntryType.GridReport
        Else
            mTargetEntryType = TargetEntryType.Report
        End If

        FrmObj = CFOpen.FOpen(e.ClickedItem.Name, e.ClickedItem.Text, mTargetEntryType)
        If FrmObj IsNot Nothing Then
            For I As Integer = 0 To Me.MdiChildren.Length - 1
                If Me.MdiChildren(I).WindowState = FormWindowState.Maximized Then
                    Me.MdiChildren(I).WindowState = FormWindowState.Normal
                End If
            Next


            FrmObj.MdiParent = Me
            'Try
            '    FrmObj.Visible = True
            'Catch ex As Exception
            'End Try
            FrmObj.Show()
            If FrmObj.Name <> "FrmReportLayout" Then
                FrmObj.WindowState = FormWindowState.Maximized
            End If
            FrmObj = Nothing
        End If
    End Sub





    Public Function FOpenForm(ByVal StrModuleName, ByVal StrMnuName, ByVal StrMnuText) As Form
        Select Case UCase(StrModuleName)
            Case UCase(ClsMain.ModuleName)
                Dim CFOpen As New Customised.ClsFunction
                FOpenForm = CFOpen.FOpen(StrMnuName, StrMnuText)
                CFOpen = Nothing

            Case Else
                FOpenForm = Nothing
        End Select
    End Function

    Private Sub MnuUpdateTableStructure_Click(sender As Object, e As EventArgs)
        Dim cf As New ClsMain(AgL)
        cf.UpdateTableStructure()
    End Sub

    Private Sub MnuChequeCompanyMaster_Click(sender As Object, e As EventArgs) Handles MnuChequeCompanyMaster.Click, MnuChequeBackupData.Click, MnuChequeBankMaster.Click, MnuChequeCompanyMaster.Click, MnuChequePrintCheque.Click, MnuChequeReport.Click

        Dim FrmObj As Form
        Dim CFOpen As New ClsFunction
        Dim mTargetEntryType As TargetEntryType

        If sender.Tag Is Nothing Then sender.Tag = ""
        If sender.Tag.Trim = "" Then
            mTargetEntryType = TargetEntryType.EntryPoint
        ElseIf AgL.StrCmp(sender.Tag.Trim, "Grid Report") Then
            mTargetEntryType = TargetEntryType.GridReport
        Else
            mTargetEntryType = TargetEntryType.Report
        End If

        FrmObj = CFOpen.FOpen(sender.Name, sender.Text, mTargetEntryType)
        If FrmObj IsNot Nothing Then
            For I As Integer = 0 To Me.MdiChildren.Length - 1
                If Me.MdiChildren(I).WindowState = FormWindowState.Maximized Then
                    Me.MdiChildren(I).WindowState = FormWindowState.Normal
                End If
            Next


            FrmObj.MdiParent = Me
            'Try
            '    FrmObj.Visible = True
            'Catch ex As Exception
            'End Try
            FrmObj.Show()
            If FrmObj.Name <> "FrmReportLayout" Then
                FrmObj.WindowState = FormWindowState.Maximized
            End If
            FrmObj = Nothing
        End If

    End Sub


End Class
