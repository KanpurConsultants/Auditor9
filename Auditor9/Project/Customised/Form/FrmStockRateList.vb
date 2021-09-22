Imports System.Data.SqlClient
Imports System.Data.SQLite
Imports CrystalDecisions.CrystalReports.Engine
Imports AgLibrary.ClsMain.agConstants

Public Class FrmStockRateList
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1DocId As String = "DocId"
    Protected Const Col1TSr As String = "TSr"
    Protected Const Col1Sr As String = "Sr"
    Protected Const Col1RecId As String = "Inv. No."
    Protected Const Col1V_Date As String = "Inv. Date"
    Protected Const Col1ItemName As String = "Item Name"
    Protected Const Col1Dimension1 As String = "Dimension1"
    Protected Const Col1Dimension2 As String = "Dimension2"
    Protected Const Col1Dimension3 As String = "Dimension3"
    Protected Const Col1Dimension4 As String = "Dimension4"
    Protected Const Col1LotNo As String = "Lot No."
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1Discount As String = "Discount"
    Protected Const Col1MRP As String = "MRP"
    Protected Const Col1LandedRate As String = "Landed Rate"
    Protected Const Col1MarginPer As String = "Margin %"
    Protected Const Col1SaleRate As String = "Sale Rate"
    Protected Const Col1BtnRateTypes As String = "Rate Types"


    Dim mQry As String = "", mDocId As String = ""
    Public Property DocId() As String
        Get
            DocId = mDocId
        End Get
        Set(ByVal value As String)
            mDocId = value
        End Set
    End Property

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Private Sub Ini_Grid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 50, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1DocId, 40, 0, Col1DocId, True, True)
            .AddAgTextColumn(Dgl1, Col1TSr, 40, 0, Col1TSr, False, True)
            .AddAgTextColumn(Dgl1, Col1Sr, 40, 0, Col1Sr, False, False)

            .AddAgTextColumn(Dgl1, Col1RecId, 100, 0, Col1RecId, True, True)
            .AddAgTextColumn(Dgl1, Col1V_Date, 90, 0, Col1V_Date, True, True)
            .AddAgTextColumn(Dgl1, Col1ItemName, 440, 0, Col1ItemName, True, True)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, Col1Dimension1, True, True)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, Col1Dimension2, True, True)
            .AddAgTextColumn(Dgl1, Col1Dimension3, 100, 0, Col1Dimension3, True, True)
            .AddAgTextColumn(Dgl1, Col1Dimension4, 100, 0, Col1Dimension4, True, True)
            .AddAgTextColumn(Dgl1, Col1LotNo, 100, 0, Col1LotNo, True, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 90, 8, 2, False, Col1Qty, True, True)
            .AddAgNumberColumn(Dgl1, Col1Rate, 90, 8, 2, False, Col1Qty, True, True)
            .AddAgNumberColumn(Dgl1, Col1Discount, 90, 8, 2, False, Col1Discount, True, True)
            .AddAgNumberColumn(Dgl1, Col1MRP, 90, 8, 2, False, Col1MRP, True, True)
            .AddAgNumberColumn(Dgl1, Col1LandedRate, 90, 8, 2, False, Col1LandedRate, True, True)
            .AddAgNumberColumn(Dgl1, Col1MarginPer, 90, 8, 2, False, Col1MarginPer, True, False)
            .AddAgNumberColumn(Dgl1, Col1SaleRate, 90, 8, 2, False, Col1SaleRate, True, False)
            .AddAgButtonColumn(Dgl1, Col1BtnRateTypes, 60, Col1BtnRateTypes, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 25
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.AllowUserToAddRows = False
        AgL.GridDesign(Dgl1)

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub

    Private Sub Dgl1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Dim bColumnIndex As Integer = 0
        Dim bRowIndex As Integer = 0
        Dim I As Integer = 0
        Try
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex
            bRowIndex = Dgl1.CurrentCell.RowIndex
            Select Case Dgl1.Columns(e.ColumnIndex).Name
                Case Col1BtnRateTypes
                    ShowBarcodeDetail(bRowIndex)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message & " in Dgl1_CellContentClick function")
        End Try
    End Sub

    Private Sub ShowBarcodeDetail(mRow As Integer)
        'If Dgl1.Item(Col1BtnRateTypes, mRow).Tag IsNot Nothing Then
        '    CType(Dgl1.Item(Col1BtnRateTypes, mRow).Tag, FrmBarcodeFill).DocNo = " Reference No. : " + Dgl1.Item(Col1RecId, mRow).Value & ", Dated : " & Dgl1.Item(Col1V_Date, mRow).Value
        '    CType(Dgl1.Item(Col1BtnRateTypes, mRow).Tag, FrmBarcodeFill).ItemName = " Item : " + Dgl1.Item(Col1ItemName, mRow).Value
        '    CType(Dgl1.Item(Col1BtnRateTypes, mRow).Tag, FrmBarcodeFill).mDocId = Dgl1.Item(Col1DocId, mRow).Value
        '    CType(Dgl1.Item(Col1BtnRateTypes, mRow).Tag, FrmBarcodeFill).mSr = Dgl1.Item(Col1Sr, mRow).Value
        '    CType(Dgl1.Item(Col1BtnRateTypes, mRow).Tag, FrmBarcodeFill).Qty = Dgl1.Item(Col1Qty, mRow).Value
        '    CType(Dgl1.Item(Col1BtnRateTypes, mRow).Tag, FrmBarcodeFill).mBarcodeType = Dgl1.Item(Col1BarcodeType, mRow).Value
        '    CType(Dgl1.Item(Col1BtnRateTypes, mRow).Tag, FrmBarcodeFill).mBarcodePattern = Dgl1.Item(Col1BarcodePattern, mRow).Value
        '    CType(Dgl1.Item(Col1BtnRateTypes, mRow).Tag, FrmBarcodeFill).MovRec()
        '    CType(Dgl1.Item(Col1BtnRateTypes, mRow).Tag, FrmBarcodeFill).StartPosition = FormStartPosition.CenterParent
        '    Dgl1.Item(Col1BtnRateTypes, mRow).Tag.ShowDialog()
        'Else

        '    Dim FrmObj As FrmBarcodeFill
        '    FrmObj = New FrmBarcodeFill
        '    FrmObj.DocNo = " Reference No. : " + Dgl1.Item(Col1RecId, mRow).Value & ", Dated : " & Dgl1.Item(Col1V_Date, mRow).Value
        '    FrmObj.ItemName = " Item : " + Dgl1.Item(Col1ItemName, mRow).Value
        '    FrmObj.mDocId = Dgl1.Item(Col1DocId, mRow).Value
        '    FrmObj.mSr = Dgl1.Item(Col1Sr, mRow).Value
        '    FrmObj.mItemCode = Dgl1.Item(Col1ItemCode, mRow).Value
        '    FrmObj.Qty = Dgl1.Item(Col1Qty, mRow).Value
        '    FrmObj.mBarcodeType = Dgl1.Item(Col1BarcodeType, mRow).Value
        '    FrmObj.mBarcodePattern = Dgl1.Item(Col1BarcodePattern, mRow).Value
        '    FrmObj.Ini_Grid()
        '    FrmObj.MovRec()
        '    Dgl1.Item(Col1BtnRateTypes, mRow).Tag = FrmObj
        '    CType(Dgl1.Item(Col1BtnRateTypes, mRow).Tag, FrmBarcodeFill).StartPosition = FormStartPosition.CenterParent
        '    Dgl1.Item(Col1BtnRateTypes, mRow).Tag.ShowDialog()
        'End If

        'If AgL.Dman_Execute("Select Count(*) From barcode Where GenDocId = '" & Dgl1.Item(Col1DocId, mRow).Value & "' And GenSr = " & Dgl1.Item(Col1Sr, mRow).Value & "", AgL.GCn).ExecuteScalar = Dgl1.Item(Col1Qty, mRow).Value Then
        '    Dgl1.Rows(mRow).DefaultCellStyle.BackColor = Color.LightCyan
        'End If
    End Sub
    Private Sub FrmBarcode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ini_Grid()
        If mDocId <> "" Then
            FillBarcodeFromDocId()
        Else
            FillPendingTransaction()
        End If
        AgL.WinSetting(Me, 654, 990, 0, 0)
    End Sub
    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Dim MyCommand As OleDb.OleDbDataAdapter = Nothing
        Select Case sender.name
            Case BtnClose.Name
                Me.Dispose()
        End Select
    End Sub

    Public Sub FillPendingTransaction()
        Dim DtTemp As DataTable
        Dim I As Integer = 0

        mQry = "Select L.DocId, L.TSr, L.Sr, L.DivCode || L.Site_Code || '-' || L.V_Type || '-' || L.RecId As RecId, H.PartyDocNo, H.PartyDocDate, L.V_Date As V_Date, 
                L.Item As ItemCode, I.Description As ItemName, D1.Description as Dimension1Name, D2.Description as Dimension2Name, 
                D3.Description as Dimension3Name, D4.Description as Dimension4Name, 
                L.Qty_Rec As Qty, Ig.BarcodeType As BarcodeType,  
                Ig.BarcodePattern As BarcodePattern
                From Stock L 
                Left Join StockHead H On L.DocID = H.DocID
                left join Item I on L.Item = I.Code
                LEFT JOIN ItemGroup Ig ON I.ItemGroup = Ig.Code
                Left Join Dimension1 D1 On L.Dimension1 = D1.Code
                Left Join Dimension2 D2 On L.Dimension2 = D2.Code
                Left Join Dimension3 D3 On L.Dimension3 = D3.Code
                Left Join Dimension4 D4 On L.Dimension4 = D4.Code
                LEFT JOIN ( 
                    Select GenDocId, GenSr, Count(*) As GeneratedBarcodes
                    From BarCode
                    Group By GenDocId, GenSr 
                ) As VBarCode On L.DocId = VBarCode.GenDocId And L.Sr = VBarCode.GenSr 
                Where Ig.BarcodeType <> '" & BarcodeType.NA & "'
                And IfNull(L.Qty_Rec,0) > 0 And IfNull(L.Qty_Rec,0) > IfNull(VBarCode.GeneratedBarcodes,0) "

        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        With DtTemp
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To .Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                    Dgl1.Item(Col1DocId, I).Value = AgL.XNull(.Rows(I)("DocId"))
                    Dgl1.Item(Col1Sr, I).Value = AgL.XNull(.Rows(I)("Sr"))
                    Dgl1.Item(Col1RecId, I).Value = AgL.XNull(.Rows(I)("RecId"))
                    Dgl1.Item(Col1V_Date, I).Value = CDate(AgL.XNull(.Rows(I)("V_Date"))).ToString("dd/MMM/yyyy")

                    Dgl1.Item(Col1ItemName, I).Value = AgL.XNull(.Rows(I)("ItemName"))
                    Dgl1.Item(Col1Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))

                Next
            End If
        End With
    End Sub

    Public Sub FillBarcodeFromDocId()
        Dim DtTemp As DataTable
        Dim I As Integer = 0

        mQry = "Select L.DocId, L.Sr, L.V_Type || '-' || L.RecId As RecId, L.V_Date As V_Date, 
                L.Item As ItemCode, I.Description As ItemName, L.Qty_Rec As Qty, Ig.BarcodeType As BarcodeType, 
                Ig.BarcodePattern As BarcodePattern
                From Stock L 
                LEFT JOIN Item I ON L.Item = I.Code 
                LEFT JOIN ItemGroup Ig ON I.ItemGroup = Ig.Code 
                Where L.DocId = '" & mDocId & "'
                And Ig.BarcodeType <> '" & BarcodeType.NA & "'"
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        With DtTemp
            Dgl1.Rows.Clear()
            If .Rows.Count > 0 Then
                For I = 0 To .Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                    Dgl1.Item(Col1DocId, I).Value = AgL.XNull(.Rows(I)("DocId"))
                    Dgl1.Item(Col1Sr, I).Value = AgL.XNull(.Rows(I)("Sr"))
                    Dgl1.Item(Col1RecId, I).Value = AgL.XNull(.Rows(I)("RecId"))
                    Dgl1.Item(Col1V_Date, I).Value = CDate(AgL.XNull(.Rows(I)("V_Date"))).ToString("dd/MMM/yyyy")

                    Dgl1.Item(Col1ItemName, I).Value = AgL.XNull(.Rows(I)("ItemName"))
                    Dgl1.Item(Col1Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))
                Next
            End If
        End With
    End Sub

    Private Sub BtnPrintBarcode_Click(sender As Object, e As EventArgs) Handles BtnPrintBarcode.Click
        Dim strTicked As String
        strTicked = FHPGD_PendingBarcodeToPrint()
        If strTicked <> "" Then
            PrintBarcodes(strTicked)
        End If
    End Sub
    Private Function FHPGD_PendingBarcodeToPrint() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim strCond As String = ""
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Dim DtMain As New DataTable

        For I = 0 To Dgl1.Rows.Count - 1
            mQry = "Select 'o' As Tick, B.Code As Code, B.Description As Barcode, I.Description As Item, B.Qty 
                    From BarCode B 
                    LEFT JOIN Item I on B.Item = I.Code  
                    Where GenDocId = '" & Dgl1.Item(Col1DocId, I).Value & "' 
                    And GenSr = " & Dgl1.Item(Col1Sr, I).Value & ""
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTemp.Rows.Count > 0 Then
                DtMain.Merge(DtTemp)
            Else
                'If Dgl1.Item(Col1BarcodeType, I).Value = BarcodeType.Fixed Then
                'mQry = "Select 'o' As Tick, B.Code As Code, B.Description As Barcode, 
                '                I.Description As Item, CAST(" & Val(Dgl1.Item(Col1Qty, I).Value) & " as Double) As Qty
                '                From Item I 
                '                LEFT JOIN Barcode B On I.Barcode = B.Code
                '                Where I.Code = '" & Dgl1.Item(Col1ItemName, I).Value & "' "
                '    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                '    If DtTemp.Rows.Count > 0 Then DtMain.Merge(DtTemp)
                'End If
            End If
        Next

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtMain), "", 500, 600, , , False)
        FRH_Multiple.ChkAll.Visible = False
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Barcode", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Item", 250, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Qty", 90, DataGridViewContentAlignment.MiddleRight)
        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingBarcodeToPrint = StrRtn

        FRH_Multiple = Nothing
    End Function
    Private Sub PrintBarcodes(ByVal strBarcode As String)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0, J As Integer = 0
        Dim bTempTable$ = ""
        Dim StrCondBale As String = ""
        Dim mCrd As New ReportDocument
        Dim ReportView As New AgLibrary.RepView
        Dim DsRep As New DataSet
        Dim RepName As String = "", RepTitle As String = ""

        Try
            RepName = "RepBarCodeImage" : RepTitle = "Item Barcode"
            bTempTable = AgL.GetGUID(AgL.GCn).ToString

            mQry = "CREATE TEMP TABLE [#" & bTempTable & "] " &
                    " (Barcode nVarChar(100), ItemDesc nVarChar(100), BarCodeImg Image)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            mQry = ""

            If Val(TxtSkipLables.Text) > 0 Then
                For I = 1 To Val(TxtSkipLables.Text)
                    If mQry.Trim <> "" Then mQry = mQry & " UNION ALL "
                    mQry = mQry & " Select null As [Barcode], Null As ItemDesc, 0 As Qty "
                Next
            End If

            If mQry.Trim <> "" Then mQry = mQry & " UNION ALL "
            mQry += " Select B.Description As Barcode, I.Description As ItemDesc, B.Qty
                From Barcode B
                LEFT JOIN Item I On B.Item = I.Code 
                Where B.Code In (" & strBarcode & ")
                And B.Qty > 0 "

            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 1 To Val(DtTemp.Rows(I)("Qty"))
                        Dim sSQL As String = "Insert Into [#" & bTempTable & "] (Barcode, ItemDesc, BarCodeImg) " &
                           " Values(@Barcode, @ItemDesc, @BarCodeImg)"

                        Dim cmd As SQLiteCommand = New SQLiteCommand(sSQL, AgL.GCn)

                        Dim Barcode As SQLiteParameter = New SQLiteParameter("@Barcode", DbType.String)
                        Dim ItemDesc As SQLiteParameter = New SQLiteParameter("@ItemDesc", DbType.String)
                        Dim BarCodeImg As SQLiteParameter = New SQLiteParameter("@BarCodeImg", DbType.Binary)


                        Barcode.Value = DtTemp.Rows(I)("Barcode")
                        ItemDesc.Value = DtTemp.Rows(I)("ItemDesc")


                        If AgL.XNull(DtTemp.Rows(I)("Barcode")) <> "" Then
                            BarCodeImg.Value = GetBarcodeImage(AgL.XNull(DtTemp.Rows(I)("Barcode")), 600, 200)
                        Else
                            BarCodeImg.Value = GetBarcodeImage("0", 400, 150)
                        End If


                        cmd.Parameters.Add(Barcode)
                        cmd.Parameters.Add(ItemDesc)
                        cmd.Parameters.Add(BarCodeImg)
                        cmd.ExecuteNonQuery()
                    Next
                Next

                mQry = " Select Barcode, ItemDesc, BarCodeImg " &
                        " From [#" & bTempTable & "] H "

                If mQry.Trim <> "" Then
                    DsRep = AgL.FillData(mQry, AgL.GCn)
                    AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)
                    mCrd.Load(AgL.PubReportPath & "\" & RepName & ".rpt")
                    mCrd.SetDataSource(DsRep.Tables(0))
                    CType(ReportView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
                    AgPL.Formula_Set(mCrd, RepTitle)
                    AgPL.Show_Report(ReportView, "* " & RepTitle & " *", Me.MdiParent)
                    If mDocId <> "" Then
                        Call AgL.LogTableEntry(mDocId, Me.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
                    End If
                End If
            Else
                If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")
            End If
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub




    Private Function GetBarcodeImage(ByVal TextValue As String, ByVal Width As Integer, ByVal Hight As Integer) As Byte()
        Dim b As BarcodeLib.Barcode
        b = New BarcodeLib.Barcode()

        Dim Img As Image
        b.Alignment = BarcodeLib.AlignmentPositions.CENTER
        b.IncludeLabel = False
        b.RotateFlipType = RotateFlipType.RotateNoneFlipNone
        b.LabelPosition = BarcodeLib.LabelPositions.BOTTOMCENTER
        Img = b.Encode(BarcodeLib.TYPE.CODE39Extended, TextValue, IIf(TextValue = "0", Drawing.Color.White, Drawing.Color.Black), Drawing.Color.White, Width, Hight)
        GetBarcodeImage = b.Encoded_Image_Bytes
    End Function
End Class