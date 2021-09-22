Imports System.ComponentModel
Imports Customised.ClsMain

Public Class FrmDataCorrection
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Dim mSearchCode$ = ""
    Public DtV_TypeSettings As DataTable

    Public Const ColSNo As String = "S.No."
    Public Const Col1Remark As String = "Remark"

    Dim mQry As String = ""

    Private _backgroundWorker1 As System.ComponentModel.BackgroundWorker
    Private Delegate Sub UpdateLabelInvoker(ByVal text As String)

    Public Property SearchCode() As String
        Get
            SearchCode = mSearchCode
        End Get
        Set(ByVal value As String)
            mSearchCode = value
        End Set
    End Property

    Public Sub FCorrectData()
        FCorrectDatesInSqlite()
    End Sub

    Public Sub FCorrectDatesInSqlite()
        If AgL.PubServerName = "" Then
            Dim dtRecords As DataTable
            Dim iRecord As Integer

            LblStatus.Text = "Fetching Wrong Cheque Dates From Ledger Table "

            mQry = "Select DocID, V_SNo, Cast(Chq_Date as nVarchar) as Chq_Date From Ledger Where Chq_Date Is Not Null And Date(Chq_Date) is Null"
            dtRecords = AgL.FillData(mQry, AgL.GCn).Tables(0)
            For iRecord = 0 To dtRecords.Rows.Count - 1
                LblStatus.Text = "Correcting Wrong Cheque Dates From Ledger Table - " & (iRecord + 1).ToString & " / " & dtRecords.Rows.Count.ToString

                mQry = "Update Ledger 
                        Set Chq_Date = " & AgL.Chk_Date(dtRecords.Rows(iRecord)("Chq_Date")) & " 
                        Where DocID = " & AgL.Chk_Text(dtRecords.Rows(iRecord)("DocID")) & " 
                        And V_SNo = " & AgL.VNull(dtRecords.Rows(iRecord)("V_SNo")) & " "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
            Next
            Dgl1.Rows.Add("")
            Dgl1.Item(Col1Remark, Dgl1.Rows.Count - 1).Value = "Wrong Cheque Dates In Ledger Table (" & dtRecords.Rows.Count.ToString & ")"
            LblStatus.Text = "Completed Wrong Cheque Dates From Ledger Table "

        End If
    End Sub


    Public Sub Ini_Grid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Remark, 150, 255, Col1Remark, True, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgL.GridDesign(Dgl1)

        Dgl1.AllowUserToAddRows = False
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True


        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub

    Private Sub FrmReconcile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ini_Grid()
    End Sub


    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub

    Private Sub BtnSend_Click(sender As Object, e As EventArgs) Handles BtnSend.Click
        FCorrectData()


        '_backgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        '_backgroundWorker1.WorkerSupportsCancellation = False
        '_backgroundWorker1.WorkerReportsProgress = False
        'AddHandler Me._backgroundWorker1.DoWork, New DoWorkEventHandler(AddressOf Me.FCorrectData)
        '_backgroundWorker1.RunWorkerAsync()
    End Sub
End Class