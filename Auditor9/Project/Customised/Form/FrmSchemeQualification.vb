Imports System.Data.SQLite
Imports AgLibrary.ClsMain
Imports AgLibrary.ClsMain.agConstants

Public Class FrmSchemeQualification
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

    Protected Const ColSNo As String = "S.No."
    Protected Const Col1Select As String = "Tick"
    Protected Const Col1PartyName As String = "Party Name"
    Protected Const Col1InvoiceNo As String = "Invoice No"
    Protected Const Col1InvoiceDate As String = "Invoice Date"
    Protected Const Col1TotalQty As String = "Total Qty"
    Protected Const Col1InvoiceAmount As String = "Invoice Amount"
    Protected Const Col1Scheme As String = "Scheme"
    Protected Const Col1SchemeAmount As String = "Scheme Amount"


    Dim mQry As String = ""

    Dim DTFind As New DataTable
    Dim fld As String
    Public HlpSt As String
    Dim DtItemTypeSetting As DataTable
    Dim DtRateTypes As DataTable

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Public Sub Ini_Grid()
        Dim I As Integer = 0
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 50, 5, ColSNo, True, True, False, , DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1Select, 35, 0, Col1Select, True, True, False)
            .AddAgTextColumn(Dgl1, Col1PartyName, 320, 0, Col1PartyName, True, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1InvoiceNo, 100, 0, Col1InvoiceNo, True, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgDateColumn(Dgl1, Col1InvoiceDate, 100, Col1InvoiceDate, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalQty, 70, 8, 4, False, Col1TotalQty, True, True, True,, DataGridViewColumnSortMode.Automatic)
            .AddAgNumberColumn(Dgl1, Col1InvoiceAmount, 80, 8, 4, False, Col1InvoiceAmount, True, True, True,, DataGridViewColumnSortMode.Automatic)
            .AddAgTextColumn(Dgl1, Col1Scheme, 120, 0, Col1Scheme, True, True,,, DataGridViewColumnSortMode.Automatic)
            .AddAgNumberColumn(Dgl1, Col1SchemeAmount, 80, 8, 4, False, Col1SchemeAmount, True, True, True,, DataGridViewColumnSortMode.Automatic)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple

        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AgAllowFind = False
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.AgAllowFind = True

        Dgl1.AllowUserToAddRows = False
        Dgl1.EnableHeadersVisualStyles = True
        AgL.GridDesign(Dgl1)


        Dgl1.Columns(Col1Select).DefaultCellStyle.Font = New Font(New FontFamily("wingdings"), 14)
        Dgl1.BackgroundColor = Color.White
        Dgl1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow
        Dgl1.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)

        For I = 0 To Dgl1.Columns.Count - 1
            Dgl1.Columns(I).ContextMenuStrip = MnuOptions
        Next
    End Sub
    Private Sub FrmBarcode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''AgL.WinSetting(Me, 654, 990, 0, 0)
        Ini_Grid()
        MovRec()
    End Sub

    Private Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
    End Sub
    Public Sub MovRec()
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0

        mQry = "Select H.DocId, Sg.Name As PartyName, H.ManualRefNo As InvoiceNo, H.V_Date, VInvoiceDetail.TotalQty,
                H.Net_Amount As InvoiceAmount, Null As Scheme, 200 As SchemeAmount
                From SaleInvoice H 
                LEFT JOIN SubGroup Sg On H.SaleToParty = Sg.SubCode 
                LEFT JOIN (Select L.DocId, Sum(L.Qty) As TotalQty
                            From SaleInvoiceDetail L 
                            GROUP By L.DocId) As VInvoiceDetail On H.DocId = VInvoiceDetail.DocId "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                Dgl1.Rows.Add()
                Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                Dgl1.Item(Col1Select, I).Value = "þ"
                Dgl1.Item(Col1PartyName, I).Value = AgL.XNull(DtTemp.Rows(I)("PartyName"))
                Dgl1.Item(Col1InvoiceNo, I).Tag = AgL.XNull(DtTemp.Rows(I)("DocId"))
                Dgl1.Item(Col1InvoiceNo, I).Value = AgL.XNull(DtTemp.Rows(I)("InvoiceNo"))
                Dgl1.Item(Col1InvoiceDate, I).Value = ClsMain.FormatDate(AgL.XNull(DtTemp.Rows(I)("V_Date")))
                Dgl1.Item(Col1TotalQty, I).Value = AgL.VNull(DtTemp.Rows(I)("TotalQty"))
                Dgl1.Item(Col1InvoiceAmount, I).Value = AgL.VNull(DtTemp.Rows(I)("InvoiceAmount"))
                Dgl1.Item(Col1Scheme, I).Value = AgL.XNull(DtTemp.Rows(I)("Scheme"))
                Dgl1.Item(Col1SchemeAmount, I).Value = AgL.VNull(DtTemp.Rows(I)("SchemeAmount"))
            Next
            Dgl1.DefaultCellStyle.WrapMode = DataGridViewTriState.True
            Dgl1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells)
        End If
        Calculation()
    End Sub
    Private Sub FrmReportWindow_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub Dgl1_ColumnDisplayIndexChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles Dgl1.ColumnDisplayIndexChanged
        Try
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Dgl1_ColumnWidthChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles Dgl1.ColumnWidthChanged
        Try
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub MnuExportToExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MnuExportToExcel.Click, MnuFreezeColumns.Click
        Dim FileName As String = ""
        Dim bColumnIndex As Integer = Dgl1.CurrentCell.ColumnIndex
        Select Case sender.Name
            Case MnuExportToExcel.Name
                If MsgBox("Want to Export Grid Data", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Export Grid?...") = vbNo Then Exit Sub
                FileName = AgControls.Export.GetFileName(My.Computer.FileSystem.SpecialDirectories.Desktop)
                If FileName.Trim <> "" Then
                    Call AgControls.Export.exportExcel(Dgl1, FileName, Dgl1.Handle)
                End If

            Case MnuFreezeColumns.Name
                If MnuFreezeColumns.Checked Then
                    Dgl1.Columns(bColumnIndex).Frozen = True
                Else
                    For I As Integer = 0 To bColumnIndex
                        Dgl1.Columns(I).Frozen = False
                    Next
                End If
        End Select
    End Sub
    Private Sub Dgl1_MouseUp(sender As Object, e As MouseEventArgs) Handles Dgl1.MouseUp
        Dim mRowIndex As Integer = Dgl1.CurrentCell.RowIndex
        Dim mColumnIndex As Integer = Dgl1.CurrentCell.ColumnIndex
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Select
                    If e.Button = Windows.Forms.MouseButtons.Left Then
                        If Dgl1.CurrentCell.ColumnIndex = Dgl1.Columns(Col1Select).Index Then
                            ClsMain.FManageTick(Dgl1, Dgl1.CurrentCell.ColumnIndex, Dgl1.Columns(Col1InvoiceNo).Index)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Private Sub Dgl1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        Dim mRowIndex As Integer = Dgl1.CurrentCell.RowIndex
        Dim mColumnIndex As Integer = Dgl1.CurrentCell.ColumnIndex
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Select
                    If e.KeyCode = Keys.Space Then
                        ClsMain.FManageTick(Dgl1, Dgl1.CurrentCell.ColumnIndex, Dgl1.Columns(Col1InvoiceNo).Index)
                    End If
            End Select
        Catch ex As Exception
            MsgBox("System Exception : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
    Private Sub Calculation()
        LblTotalInvoiceAmount.Text = 0
        LblTotalSchemeAmount.Text = 0
        For I As Integer = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Select, I).Value = "þ" Then
                LblTotalInvoiceAmount.Text = Val(LblTotalInvoiceAmount.Text) + Val(Dgl1.Item(Col1InvoiceAmount, I).Value)
                LblTotalSchemeAmount.Text = Val(LblTotalSchemeAmount.Text) + Val(Dgl1.Item(Col1SchemeAmount, I).Value)
            End If
        Next
    End Sub

    Private Sub BtnOk_Click(sender As Object, e As EventArgs) Handles BtnOk.Click, BtnCancel.Click
        Select Case sender.name
            Case BtnOk.Name
                FProcessScheme()

            Case BtnCancel.Name
                Me.Close()
        End Select
    End Sub
    Private Sub FProcessScheme()
        Dim mTrans As String = ""
        Dim I As Integer = 0
        Dim bSelectionQry As String = ""
        Try
            Dim mV_Date As String = AgL.PubLoginDate
            Dim mV_Type As String = Ncat.CreditNoteCustomer
            'StrDocID = AgL.GetDocId(mV_Type, CStr(0), CDate(AgL.PubLoginDate), AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode)
            StrDocID = AgL.CreateDocId(AgL, "LedgerHead", mV_Type, CStr(0), CDate(AgL.PubLoginDate), AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode)
            Dim mV_No As String = Val(AgL.DeCodeDocID(StrDocID, AgLibrary.ClsMain.DocIdPart.VoucherNo))
            Dim mV_Prefix As String = AgL.DeCodeDocID(StrDocID, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            mQry = "INSERT INTO LedgerHead (DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code,
                     ReferenceNo, Subcode, DrCr, UptoDate, Remarks, Status, SalesTaxGroupParty, PlaceOfSupply, VendorSalesTaxNo,
                     Structure, CustomFields, PartyDocNo, PartyDocDate, EntryBy, EntryDate)
                     Select " & AgL.Chk_Text(StrDocID) & ", " & AgL.Chk_Text(mV_Type) & ", " & AgL.Chk_Text(mV_Prefix) & ", 
                     " & AgL.Chk_Text(mV_Date) & ", " & AgL.Chk_Text(mV_No) & ", " & AgL.Chk_Text(AgL.PubDivCode) & ", 
                     " & AgL.Chk_Text(AgL.PubSiteCode) & ",
                     Null As ReferenceNo, Null As Subcode, Null As DrCr, Null As UptoDate, 
                     Null As Remarks, Null As Status, Null As SalesTaxGroupParty, Null As PlaceOfSupply, Null As VendorSalesTaxNo,
                     Null As Structure, Null As CustomFields, Null As PartyDocNo, Null As PartyDocDate, 
                     " & AgL.PubUserName & " As EntryBy, " & AgL.PubLoginDate & " As EntryDate) "


            Dim mSr As Integer = 0
            For I = 0 To Dgl1.RowCount - 1
                If Dgl1.Item(Col1Select, I).Value <> "" Then
                    If Dgl1.Item(ColSNo, I).Tag Is Nothing And Dgl1.Rows(I).Visible = True Then
                        mSr += 1

                        If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                        bSelectionQry += " Select " & AgL.Chk_Text(StrDocID) & ", " & mSr & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1PartyName, I).Tag) & ", " &
                                        " Null As Specification, " &
                                        " Null As SalesTaxGroup, " &
                                        " 0 As Qty, " &
                                        " Null As Unit, " &
                                        " 0 As Rate, " &
                                        " " & Val(Dgl1.Item(Col1SchemeAmount, I).Value) & ", " &
                                        " Null As ChqNo, " &
                                        " Null As ChqDate, " &
                                        " Null As Remark, " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1InvoiceNo, I).Tag) & ", " &
                                        " Null As Sr, " &
                                        " Null As EffectiveDate "
                    End If
                End If
            Next

            If bSelectionQry <> "" Then
                mQry = "Insert Into LedgerHeadDetail(DocId, Sr, Subcode, Specification, SalesTaxGroupItem, " &
                   " Qty, Unit, Rate, Amount, ChqRefNo, ChqRefDate, Remarks, " &
                   " SpecificationDocId, SpecificationDocIdSr, EffectiveDate) " & bSelectionQry
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If


            mQry = "Select SubCode from SubGroup Where DispName = 'Scheme Account'"
            Dim mSchemeAccount As String = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)

            If mSchemeAccount = "" Then
                MsgBox("There is no account named scheme account...!", MsgBoxStyle.Information)
                Exit Sub
            End If

            mQry = "Select H.DocId, H.V_No, H.V_Type, H.V_Prefix, H.V_Date, L.SubCode, H.SubCode As ContraSub,
                    0 As AmtDr, L.Amount As AmtCr, H.Site_Code
                    From LedgerHead H 
                    LEFT JOIN LedgerHeadDetail L On H.DocId = L.DocId
                    Where H.DocId = '" & StrDocID & "'

                    UNION ALL 

                    Select H.DocId, H.V_No, H.V_Type, H.V_Prefix, H.V_Date, H.SubCode, L.SubCode As ContraSub,
                    0 As AmtDr, L.Amount As AmtCr, H.Site_Code
                    From LedgerHead H 
                    LEFT JOIN LedgerHeadDetail L On H.DocId = L.DocId
                    Where H.DocId = '" & StrDocID & "'"
            Dim DtLedger As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)

            mSr = 0
            For I = 0 To DtLedger.Rows.Count - 1
                mQry = "INSERT INTO Ledger(DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, SubCode, ContraSub, AmtDr, AmtCr)
                        VALUES (" & AgL.Chk_Text(AgL.XNull(DtLedger.Rows(I)("DocId"))) & ", " & mSr & ",
                        " & AgL.VNull(DtLedger.Rows(I)("V_No")) & ", " & AgL.Chk_Text(AgL.XNull(DtLedger.Rows(I)("V_Type"))) & ", 
                        " & AgL.Chk_Text(AgL.XNull(DtLedger.Rows(I)("V_Prefix"))) & ", 
                        " & AgL.Chk_Text(AgL.XNull(DtLedger.Rows(I)("V_Date"))) & ", 
                        " & AgL.Chk_Text(AgL.XNull(DtLedger.Rows(I)("SubCode"))) & ", 
                        " & AgL.Chk_Text(AgL.XNull(DtLedger.Rows(I)("ContraSub"))) & ", 
                        " & AgL.VNull(DtLedger.Rows(I)("AmtDr")) & ", 
                        " & AgL.VNull(DtLedger.Rows(I)("AmtCr")) & ") "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Next

            AgL.UpdateVoucherCounter(StrDocID, CDate(mV_Date), AgL.GCn, AgL.ECmd, AgL.PubDivCode, AgL.PubSiteCode)

            AgL.ETrans.Commit()
            mTrans = "Commit"

        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
End Class