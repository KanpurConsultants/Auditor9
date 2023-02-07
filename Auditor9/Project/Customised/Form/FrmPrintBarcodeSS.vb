Imports System.Data.SqlClient
Imports System.Data.SQLite
Imports CrystalDecisions.CrystalReports.Engine
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class FrmPrintBarcodeSS
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const ColSNo As String = "S.No."
    Public Const Col1DocId As String = "DocId"
    Public Const Col1Sr As String = "Sr"
    Public Const Col1ItemCode As String = "Item Code"
    Public Const Col1RecId As String = "Rec Id"
    Public Const Col1V_Date As String = "Date"
    Public Const Col1Design As String = "Design"
    Public Const Col1Colour As String = "Colour"
    Public Const Col1Item As String = "Item"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1Dimension3 As String = "Dimension3"
    Public Const Col1Dimension4 As String = "Dimension4"
    Public Const Col1Size As String = "Size"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1PrintQty As String = "Print Qty"
    Public Const Col1BtnBarcodeDetail As String = "Barcode"
    Public Const Col1BarcodeType As String = "Barcode Type"
    Public Const Col1BarcodePattern As String = "Barcode Pattern"
    Public Const Col1PurchaseRate As String = "Purchase Rate"
    Public Const Col1SaleRate As String = "Sale Rate"
    Public Const Col1MRP As String = "MRP"

    Dim mQry As String = "", mDocId As String = ""

    Private Const PrintAction_PrintToPrinter As String = "Print To Printer"
    Private Const PrintAction_Preview As String = "Preview"
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
            '.AddAgTextColumn(Dgl1, Col1DocId, 40, 0, Col1DocId, False, False)
            '.AddAgTextColumn(Dgl1, Col1Sr, 40, 0, Col1Sr, False, False)
            '.AddAgTextColumn(Dgl1, Col1ItemCode, 40, 0, Col1ItemCode, False, False)
            '.AddAgTextColumn(Dgl1, Col1RecId, 100, 0, Col1RecId, True, True)
            '.AddAgTextColumn(Dgl1, Col1V_Date, 110, 0, Col1V_Date, True, True)
            .AddAgTextColumn(Dgl1, Col1Design, 120, 0, Col1Design, True, False)
            .AddAgTextColumn(Dgl1, Col1Colour, 120, 0, Col1Colour, True, False)
            .AddAgTextColumn(Dgl1, Col1Item, 120, 0, Col1Item, True, False)
            '.AddAgTextColumn(Dgl1, Col1Dimension1, 120, 0, Col1Dimension1, True, True)
            '.AddAgTextColumn(Dgl1, Col1Dimension2, 120, 0, Col1Dimension2, True, True)
            '.AddAgTextColumn(Dgl1, Col1Dimension3, 120, 0, Col1Dimension3, True, True)
            '.AddAgTextColumn(Dgl1, Col1Dimension4, 120, 0, Col1Dimension4, True, True)
            .AddAgTextColumn(Dgl1, Col1Size, 120, 0, Col1Size, True, False)
            '.AddAgNumberColumn(Dgl1, Col1Qty, 90, 8, 2, False, Col1Qty, True, True)
            .AddAgTextColumn(Dgl1, Col1MRP, 120, 0, Col1MRP, True, False)
            .AddAgNumberColumn(Dgl1, Col1PrintQty, 90, 8, 2, False, Col1PrintQty, True, False)
            '.AddAgButtonColumn(Dgl1, Col1BtnBarcodeDetail, 70, Col1BtnBarcodeDetail, True, False)
            '.AddAgTextColumn(Dgl1, Col1BarcodeType, 65, 0, " ", False, True)
            '.AddAgTextColumn(Dgl1, Col1BarcodePattern, 65, 0, " ", False, True)
            '.AddAgTextColumn(Dgl1, Col1SaleRate, 65, 0, " ", False, True)
            '.AddAgTextColumn(Dgl1, Col1PurchaseRate, 65, 0, " ", False, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 25
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True
        'Dgl1.AllowUserToAddRows = False
        Dgl1.Name = "Dgl1"
        AgL.FSetDimensionCaptionForHorizontalGrid(Dgl1, AgL)
        AgL.GridDesign(Dgl1)

        'ApplyUISetting()
        TxtBarcodeType.Tag = "Landscape"
        TxtBarcodeType.Text = "Landscape"
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub
    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub
    Private Sub Dgl1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Dim bColumnIndex As Integer = 0
        Dim bRowIndex As Integer = 0
        Dim I As Integer = 0
        Try
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex
            bRowIndex = Dgl1.CurrentCell.RowIndex
            Select Case Dgl1.Columns(e.ColumnIndex).Name
                Case Col1BtnBarcodeDetail
                    ShowBarcodeDetail(bRowIndex)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message & " in Dgl1_CellContentClick function")
        End Try
    End Sub

    Private Sub ShowBarcodeDetail(mRow As Integer)
        If Dgl1.Item(Col1BtnBarcodeDetail, mRow).Tag IsNot Nothing Then
            CType(Dgl1.Item(Col1BtnBarcodeDetail, mRow).Tag, FrmBarcodeFill).DocNo = " Reference No. : " + Dgl1.Item(Col1RecId, mRow).Value & ", Dated : " & Dgl1.Item(Col1V_Date, mRow).Value
            CType(Dgl1.Item(Col1BtnBarcodeDetail, mRow).Tag, FrmBarcodeFill).ItemName = " Item : " + Dgl1.Item(Col1Item, mRow).Value
            CType(Dgl1.Item(Col1BtnBarcodeDetail, mRow).Tag, FrmBarcodeFill).mDocId = Dgl1.Item(Col1DocId, mRow).Value
            CType(Dgl1.Item(Col1BtnBarcodeDetail, mRow).Tag, FrmBarcodeFill).mSr = Dgl1.Item(Col1Sr, mRow).Value
            CType(Dgl1.Item(Col1BtnBarcodeDetail, mRow).Tag, FrmBarcodeFill).mItemCode = Dgl1.Item(Col1ItemCode, mRow).Value
            CType(Dgl1.Item(Col1BtnBarcodeDetail, mRow).Tag, FrmBarcodeFill).Qty = Dgl1.Item(Col1Qty, mRow).Value
            CType(Dgl1.Item(Col1BtnBarcodeDetail, mRow).Tag, FrmBarcodeFill).mSaleRate = Dgl1.Item(Col1SaleRate, mRow).Value
            CType(Dgl1.Item(Col1BtnBarcodeDetail, mRow).Tag, FrmBarcodeFill).mPurchaseRate = Dgl1.Item(Col1PurchaseRate, mRow).Value
            CType(Dgl1.Item(Col1BtnBarcodeDetail, mRow).Tag, FrmBarcodeFill).mMRP = Dgl1.Item(Col1MRP, mRow).Value
            CType(Dgl1.Item(Col1BtnBarcodeDetail, mRow).Tag, FrmBarcodeFill).mBarcodeType = Dgl1.Item(Col1BarcodeType, mRow).Value
            CType(Dgl1.Item(Col1BtnBarcodeDetail, mRow).Tag, FrmBarcodeFill).mBarcodePattern = Dgl1.Item(Col1BarcodePattern, mRow).Value
            CType(Dgl1.Item(Col1BtnBarcodeDetail, mRow).Tag, FrmBarcodeFill).MovRec()
            CType(Dgl1.Item(Col1BtnBarcodeDetail, mRow).Tag, FrmBarcodeFill).StartPosition = FormStartPosition.CenterParent
            Dgl1.Item(Col1BtnBarcodeDetail, mRow).Tag.ShowDialog()
        Else
            Dim FrmObj As FrmBarcodeFill
            FrmObj = New FrmBarcodeFill
            FrmObj.DocNo = " Reference No. : " + Dgl1.Item(Col1RecId, mRow).Value & ", Dated : " & Dgl1.Item(Col1V_Date, mRow).Value
            FrmObj.ItemName = " Item : " + Dgl1.Item(Col1Item, mRow).Value
            FrmObj.mDocId = Dgl1.Item(Col1DocId, mRow).Value
            FrmObj.mSr = Dgl1.Item(Col1Sr, mRow).Value
            FrmObj.mItemCode = Dgl1.Item(Col1ItemCode, mRow).Value
            FrmObj.Qty = Dgl1.Item(Col1Qty, mRow).Value
            FrmObj.mSaleRate = Val(Dgl1.Item(Col1SaleRate, mRow).Value)
            FrmObj.mPurchaseRate = Val(Dgl1.Item(Col1PurchaseRate, mRow).Value)
            FrmObj.mMRP = Val(Dgl1.Item(Col1MRP, mRow).Value)
            FrmObj.mBarcodeType = Dgl1.Item(Col1BarcodeType, mRow).Value
            FrmObj.mBarcodePattern = Dgl1.Item(Col1BarcodePattern, mRow).Value
            FrmObj.Ini_Grid()
            FrmObj.MovRec()
            Dgl1.Item(Col1BtnBarcodeDetail, mRow).Tag = FrmObj
            CType(Dgl1.Item(Col1BtnBarcodeDetail, mRow).Tag, FrmBarcodeFill).StartPosition = FormStartPosition.CenterParent
            Dgl1.Item(Col1BtnBarcodeDetail, mRow).Tag.ShowDialog()
        End If

        If AgL.Dman_Execute("Select Count(*) From barcode Where GenDocId = '" & Dgl1.Item(Col1DocId, mRow).Value & "' And GenSr = " & Dgl1.Item(Col1Sr, mRow).Value & "", AgL.GCn).ExecuteScalar = Dgl1.Item(Col1Qty, mRow).Value Then
            Dgl1.Rows(mRow).DefaultCellStyle.BackColor = Color.LightCyan
        End If
    End Sub
    Private Sub FrmBarcode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ini_Grid()
        'If mDocId <> "" Then
        '    FillBarcodeFromDocId()
        'Else
        '    FillPendingTransaction()
        'End If
        ''AgL.WinSetting(Me, 654, 990, 0, 0)
    End Sub
    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Dim MyCommand As OleDb.OleDbDataAdapter = Nothing
        Select Case sender.name

            Case BtnClose.Name
                Me.Dispose()
        End Select
    End Sub


    Private Sub BtnPrintBarcode_Click(sender As Object, e As EventArgs) Handles BtnPrintBarcode.Click, BtnPreview.Click
        Dim strTicked As String

        Select Case sender.name
            Case BtnPrintBarcode.Name
                strTicked = FHPGD_PendingBarcodeToPrint(PrintAction_PrintToPrinter)
            Case BtnPreview.Name
                strTicked = FHPGD_PendingBarcodeToPrint(PrintAction_Preview)
        End Select
    End Sub
    Private Function FHPGD_PendingBarcodeToPrint(PrintAction As String) As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim strCond As String = ""
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Dim DtMain As New DataTable

        'For I = 0 To Dgl1.Rows.Count - 1
        '    mQry = "Select 'o' As Tick, B.Code As Code, B.Description As Barcode, I.Description As Item, Cast(B.Qty as Integer) as Qty
        '            From BarCode B 
        '            LEFT JOIN Item I on B.Item = I.Code  
        '            Where B.GenDocId = '" & Dgl1.Item(Col1DocId, I).Value & "' 
        '            And B.GenSr = " & Dgl1.Item(Col1Sr, I).Value & ""
        '    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        '    If DtTemp.Rows.Count > 0 Then
        '        DtMain.Merge(DtTemp)
        '    Else
        '        If Dgl1.Item(Col1BarcodeType, I).Value = BarcodeType.Fixed Then
        '            mQry = "Select 'o' As Tick, B.Code As Code, B.Description As Barcode, 
        '                    I.Description As Item, CAST(" & Val(Dgl1.Item(Col1PrintQty, I).Value) & " as Integer) As Qty
        '                    From Item I 
        '                    LEFT JOIN Barcode B On I.Barcode = B.Code
        '                    Where I.Code = '" & Dgl1.Item(Col1ItemCode, I).Value & "' "
        '            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        '            If DtTemp.Rows.Count > 0 Then DtMain.Merge(DtTemp)
        '        End If
        '    End If
        'Next


        If Val(TxtSkipLables.Text) > 0 Then
            mQry = "Select 'o' As Tick, Cast(0 As BigInt) As Code, '' As Barcode, 
                    '' As ItemDesc, '' As ItemCategoryDesc, '' As ItemGroupDesc, 
                    '' as Dimension1Desc, '' as Dimension2Desc,
                    '' as Dimension3Desc, '' as Dimension4Desc, 
                    '' as SizeDesc, CAST(0.00 AS DECIMAL(18,2)) As PurchaseRate, 
                    CAST(0.00 AS DECIMAL(18,2)) As SaleRate, CAST(0.00 AS DECIMAL(18,2)) As MRP,
                    CAST(" & Val(TxtSkipLables.Text) & " AS Float) As Qty,
                    CAST(0.00 AS DECIMAL(18,2)) As ReceiveQty "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            DtMain.Merge(DtTemp)
        End If

        For I = 0 To Dgl1.Rows.Count - 1
            If Val(Dgl1.Item(Col1PrintQty, I).Value) <> 0 Then
                'If Dgl1.Item(Col1BarcodeType, I).Value = BarcodeType.Fixed Then
                '    mQry = "Select 'o' As Tick, B.Code As Code, B.Description As Barcode, 
                '        Case When Sku.V_Type = '" & ItemV_Type.SKU & "' Then I.Specification Else Sku.Specification End as ItemDesc,
                '        IC.Description As ItemCategoryDesc, IG.Description As ItemGroupDesc, 
                '        D1.Specification as Dimension1Desc, D2.Specification as Dimension2Desc,
                '        D3.Specification as Dimension3Desc, D4.Specification as Dimension4Desc, 
                '        Size.Specification as SizeDesc, CAST(IfNull(B.PurchaseRate,0)*1.0 AS DECIMAL(18,2)) As PurchaseRate, 
                '        CAST(IfNull(B.SaleRate,0)*1.0 AS DECIMAL(18,2)) As SaleRate, CAST(IfNull(B.MRP,0)*1.0 AS DECIMAL(18,2)) As MRP,
                '        CAST(" & Val(Dgl1.Item(Col1PrintQty, I).Value) & " as Float) As Qty, CAST(IfNull(B.Qty,0)*1.0 AS DECIMAL(18,2)) As ReceiveQty
                '        From Item Sku
                '        Left Join Item IC On Sku.ItemCategory = IC.Code
                '        Left Join Item IG On Sku.ItemGroup = IG.Code
                '        LEFT JOIN Item I ON Sku.BaseItem = I.Code
                '        LEFT JOIN Item D1 ON Sku.Dimension1 = D1.Code
                '        LEFT JOIN Item D2 ON Sku.Dimension2 = D2.Code
                '        LEFT JOIN Item D3 ON Sku.Dimension3 = D3.Code
                '        LEFT JOIN Item D4 ON Sku.Dimension4 = D4.Code
                '        LEFT JOIN Item Size ON Sku.Size = Size.Code
                '        LEFT JOIN Barcode B On Sku.Barcode = B.Code
                '        Where Sku.Code = '" & Dgl1.Item(Col1ItemCode, I).Value & "' "
                '    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                'Else
                '    mQry = "Select 'o' As Tick, B.Code As Code, B.Description As Barcode, 
                '        Case When Sku.V_Type = '" & ItemV_Type.SKU & "' Then I.Specification Else Sku.Specification End as ItemDesc,
                '        IC.Description As ItemCategoryDesc, IG.Description As ItemGroupDesc, 
                '        D1.Specification as Dimension1Desc, D2.Specification as Dimension2Desc,
                '        D3.Specification as Dimension3Desc, D4.Specification as Dimension4Desc, 
                '        Size.Specification as SizeDesc, CAST(IfNull(B.PurchaseRate,0)*1.0 AS DECIMAL(18,2)) As PurchaseRate, 
                '        CAST(IfNull(B.SaleRate,0)*1.0 AS DECIMAL(18,2)) As SaleRate, CAST(IfNull(B.MRP,0)*1.0 AS DECIMAL(18,2)) As MRP,
                '        Cast(" & IIf(Dgl1.Item(Col1BarcodeType, I).Value = BarcodeType.UniquePerPcs, "B.Qty", Val(Dgl1.Item(Col1PrintQty, I).Value)) & " As Float) as Qty, CAST(IfNull(B.Qty,0)*1.0 AS DECIMAL(18,2)) As ReceiveQty
                '        From BarCode B 
                '        LEFT JOIN Item Sku on B.Item = Sku.Code  
                '        Left Join Item IC On Sku.ItemCategory = IC.Code
                '        Left Join Item IG On Sku.ItemGroup = IG.Code
                '        LEFT JOIN Item I ON Sku.BaseItem = I.Code
                '        LEFT JOIN Item D1 ON Sku.Dimension1 = D1.Code
                '        LEFT JOIN Item D2 ON Sku.Dimension2 = D2.Code
                '        LEFT JOIN Item D3 ON Sku.Dimension3 = D3.Code
                '        LEFT JOIN Item D4 ON Sku.Dimension4 = D4.Code
                '        LEFT JOIN Item Size ON Sku.Size = Size.Code
                '        Where B.GenDocId = '" & Dgl1.Item(Col1DocId, I).Value & "' 
                '        And B.GenSr = " & Dgl1.Item(Col1Sr, I).Value & ""
                '    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                'End If
                mQry = "Select 'o' As Tick, '" & Dgl1.Item(Col1Design, I).Value & "' As Code, SUBSTR('" & Dgl1.Item(Col1Design, I).Value & "" & Dgl1.Item(Col1Colour, I).Value & "" & Dgl1.Item(Col1Size, I).Value & "',0,10) As Barcode, 
                        '" & Dgl1.Item(Col1Item, I).Value & "' as ItemDesc,
                        '" & Dgl1.Item(Col1Design, I).Value & "' As ItemCategoryDesc, '" & Dgl1.Item(Col1Colour, I).Value & "' As ItemGroupDesc, 
                        Null as Dimension1Desc, Null as Dimension2Desc, Null as Dimension3Desc, Null as Dimension4Desc, 
                        '" & Dgl1.Item(Col1Size, I).Value & "' as SizeDesc, Null As PurchaseRate, 
                        CAST(" & Val(Dgl1.Item(Col1MRP, I).Value) & " as Float) As SaleRate, CAST(" & Val(Dgl1.Item(Col1MRP, I).Value) & " as Float) As MRP,
                        CAST(" & Val(Dgl1.Item(Col1PrintQty, I).Value) & " as Float) As Qty, Null As ReceiveQty
                         "
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtTemp.Rows.Count > 0 Then DtMain.Merge(DtTemp)
            End If
        Next


        PrintBarcodes(DtMain, PrintAction)
        'FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtMain), "", 500, 600, , , False)
        'FRH_Multiple.ChkAll.Visible = False
        'FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        'FRH_Multiple.FFormatColumn(1, , 0, , False)
        'FRH_Multiple.FFormatColumn(2, "Barcode", 100, DataGridViewContentAlignment.MiddleLeft)
        'FRH_Multiple.FFormatColumn(3, "Item", 250, DataGridViewContentAlignment.MiddleLeft)
        'FRH_Multiple.FFormatColumn(4, "Qty", 90, DataGridViewContentAlignment.MiddleRight)
        'FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        'FRH_Multiple.ShowDialog()

        'If FRH_Multiple.BytBtnValue = 0 Then
        '    StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        'End If
        'FHPGD_PendingBarcodeToPrint = StrRtn

        'FRH_Multiple = Nothing
    End Function


    Private Function FGetSettings(FieldName As String, SettingType As String) As String
        Dim mValue As String
        mValue = ClsMain.FGetSettings(FieldName, SettingType, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        FGetSettings = mValue
    End Function

    Private Sub PrintBarcodes(ByVal DtTemp As DataTable, PrintAction As String)
        Dim I As Integer = 0, J As Integer = 0
        Dim bTempTable$ = ""
        Dim StrCondBale As String = ""
        Dim mCrd As New ReportDocument
        Dim ReportView As New AgLibrary.RepView
        Dim DsRep As New DataSet
        Dim RepName As String = "", RepTitle As String = ""

        Try
            RepName = "RepBarCodeImage" : RepTitle = "Item Barcode"
            Dim mDocReportFileName As String = FGetSettings(SettingFields.BarcodePrintReportFileName, SettingType.General)
            Dim mBarcodePrintTitle1 As String = FGetSettings(SettingFields.BarcodePrintTitle1, SettingType.General)
            Dim mBarcodePrintTitle2 As String = FGetSettings(SettingFields.BarcodePrintTitle2, SettingType.General)
            Dim mBarcodePrintTitle3 As String = FGetSettings(SettingFields.BarcodePrintTitle3, SettingType.General)
            Dim mBarcodeRatePrefix As String = FGetSettings(SettingFields.BarcodePrintSaleRatePrefix, SettingType.General)

            mBarcodePrintTitle1 = "AERO CLUB"
            If (TxtBarcodeType.Text = "Portrait") Then
                RepName = "Barcode_Print_SSAERO_Portrait.rpt"
            Else
                RepName = "Barcode_Print_SSAERO_Landscape.rpt"
            End If


            bTempTable = Guid.NewGuid.ToString

            mQry = "CREATE TEMPORARY TABLE [#" & bTempTable & "] " &
                    " (Code nVarchar(20),Barcode nVarChar(100), BarCodeImg Image, ItemDesc nVarChar(200), 
                        ItemCategoryDesc nVarChar(100), ItemGroupDesc nVarChar(100), 
                        Dimension1Desc nVarChar(100), Dimension2Desc nVarChar(100),
                        Dimension3Desc nVarChar(100), Dimension4Desc nVarChar(100), 
                        SizeDesc nVarChar(100), PurchaseRate Float, SaleRate Float, MRP Float, ReceiveQty Float) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    For J = 1 To Val(DtTemp.Rows(I)("Qty"))
                        Dim sSQL As String = "Insert Into [#" & bTempTable & "] (Code,Barcode, BarCodeImg, ItemDesc, ItemCategoryDesc, ItemGroupDesc, 
                        Dimension1Desc, Dimension2Desc, Dimension3Desc, Dimension4Desc, SizeDesc, PurchaseRate, SaleRate, MRP, ReceiveQty) " &
                        " Values(@Code, @Barcode, @BarCodeImg, @ItemDesc, @ItemCategoryDesc, @ItemGroupDesc, 
                        @Dimension1Desc, @Dimension2Desc, @Dimension3Desc, @Dimension4Desc, @SizeDesc, @PurchaseRate, @SaleRate, @MRP, @ReceiveQty)"
                        sSQL = AgL.GetBackendBasedQuery(sSQL)
                        If AgL.PubServerName = "" Then
                            Dim cmd As SQLiteCommand = New SQLiteCommand(sSQL, AgL.GCn)

                            Dim Code As SQLiteParameter = New SQLiteParameter("@Code", DbType.String)
                            Dim Barcode As SQLiteParameter = New SQLiteParameter("@Barcode", DbType.String)
                            Dim BarCodeImg As SQLiteParameter = New SQLiteParameter("@BarCodeImg", DbType.Binary)
                            Dim ItemDesc As SQLiteParameter = New SQLiteParameter("@ItemDesc", DbType.String)
                            Dim ItemCategoryDesc As SQLiteParameter = New SQLiteParameter("@ItemCategoryDesc", DbType.String)
                            Dim ItemGroupDesc As SQLiteParameter = New SQLiteParameter("@ItemGroupDesc", DbType.String)
                            Dim Dimension1Desc As SQLiteParameter = New SQLiteParameter("@Dimension1Desc", DbType.String)
                            Dim Dimension2Desc As SQLiteParameter = New SQLiteParameter("@Dimension2Desc", DbType.String)
                            Dim Dimension3Desc As SQLiteParameter = New SQLiteParameter("@Dimension3Desc", DbType.String)
                            Dim Dimension4Desc As SQLiteParameter = New SQLiteParameter("@Dimension4Desc", DbType.String)
                            Dim SizeDesc As SQLiteParameter = New SQLiteParameter("@SizeDesc", DbType.String)
                            Dim PurchaseRate As SQLiteParameter = New SQLiteParameter("@PurchaseRate", DbType.String)
                            Dim SaleRate As SQLiteParameter = New SQLiteParameter("@SaleRate", DbType.String)
                            Dim MRP As SQLiteParameter = New SQLiteParameter("@MRP", DbType.String)
                            Dim ReceiveQty As SQLiteParameter = New SQLiteParameter("@ReceiveQty", DbType.String)

                            Code.Value = AgL.XNull(DtTemp.Rows(I)("Code"))
                            Barcode.Value = AgL.XNull(DtTemp.Rows(I)("Barcode"))
                            ItemDesc.Value = AgL.XNull(DtTemp.Rows(I)("ItemDesc"))
                            ItemCategoryDesc.Value = AgL.XNull(DtTemp.Rows(I)("ItemCategoryDesc"))
                            ItemGroupDesc.Value = AgL.XNull(DtTemp.Rows(I)("ItemGroupDesc"))
                            Dimension1Desc.Value = AgL.XNull(DtTemp.Rows(I)("Dimension1Desc"))
                            Dimension2Desc.Value = AgL.XNull(DtTemp.Rows(I)("Dimension2Desc"))
                            Dimension3Desc.Value = AgL.XNull(DtTemp.Rows(I)("Dimension3Desc"))
                            Dimension4Desc.Value = AgL.XNull(DtTemp.Rows(I)("Dimension4Desc"))
                            SizeDesc.Value = AgL.XNull(DtTemp.Rows(I)("SizeDesc"))
                            PurchaseRate.Value = AgL.VNull(DtTemp.Rows(I)("PurchaseRate"))
                            SaleRate.Value = AgL.VNull(DtTemp.Rows(I)("SaleRate"))
                            MRP.Value = AgL.VNull(DtTemp.Rows(I)("MRP"))
                            ReceiveQty.Value = AgL.VNull(DtTemp.Rows(I)("ReceiveQty"))



                            If AgL.XNull(DtTemp.Rows(I)("Barcode")) <> "" Then
                                BarCodeImg.Value = GetBarcodeImage(AgL.XNull(DtTemp.Rows(I)("Barcode")), 350, 50)
                            Else
                                BarCodeImg.Value = GetBarcodeImage("0", 200, 50)
                            End If


                            cmd.Parameters.Add(Code)
                            cmd.Parameters.Add(Barcode)
                            cmd.Parameters.Add(BarCodeImg)
                            cmd.Parameters.Add(ItemDesc)
                            cmd.Parameters.Add(ItemCategoryDesc)
                            cmd.Parameters.Add(ItemGroupDesc)
                            cmd.Parameters.Add(Dimension1Desc)
                            cmd.Parameters.Add(Dimension2Desc)
                            cmd.Parameters.Add(Dimension3Desc)
                            cmd.Parameters.Add(Dimension4Desc)
                            cmd.Parameters.Add(SizeDesc)
                            cmd.Parameters.Add(PurchaseRate)
                            cmd.Parameters.Add(SaleRate)
                            cmd.Parameters.Add(MRP)
                            cmd.Parameters.Add(ReceiveQty)


                            cmd.ExecuteNonQuery()

                        Else
                            Dim cmd As SqlCommand = New SqlCommand(sSQL, AgL.GCn)

                            Dim Code As SqlParameter = New SqlParameter("@Code", DbType.String)
                            Dim Barcode As SqlParameter = New SqlParameter("@Barcode", DbType.String)
                            Dim BarCodeImg As SqlParameter = New SqlParameter("@BarCodeImg", DbType.Binary)
                            Dim ItemDesc As SqlParameter = New SqlParameter("@ItemDesc", DbType.String)
                            Dim ItemCategoryDesc As SqlParameter = New SqlParameter("@ItemCategoryDesc", DbType.String)
                            Dim ItemGroupDesc As SqlParameter = New SqlParameter("@ItemGroupDesc", DbType.String)
                            Dim Dimension1Desc As SqlParameter = New SqlParameter("@Dimension1Desc", DbType.String)
                            Dim Dimension2Desc As SqlParameter = New SqlParameter("@Dimension2Desc", DbType.String)
                            Dim Dimension3Desc As SqlParameter = New SqlParameter("@Dimension3Desc", DbType.String)
                            Dim Dimension4Desc As SqlParameter = New SqlParameter("@Dimension4Desc", DbType.String)
                            Dim SizeDesc As SqlParameter = New SqlParameter("@SizeDesc", DbType.String)
                            Dim PurchaseRate As SqlParameter = New SqlParameter("@PurchaseRate", DbType.String)
                            Dim SaleRate As SqlParameter = New SqlParameter("@SaleRate", DbType.String)
                            Dim MRP As SqlParameter = New SqlParameter("@MRP", DbType.String)
                            Dim ReceiveQty As SqlParameter = New SqlParameter("@ReceiveQty", DbType.String)


                            Code.Value = DtTemp.Rows(I)("Code")
                            Barcode.Value = DtTemp.Rows(I)("Barcode")
                            ItemDesc.Value = DtTemp.Rows(I)("ItemDesc")
                            ItemCategoryDesc.Value = AgL.XNull(DtTemp.Rows(I)("ItemCategoryDesc"))
                            ItemGroupDesc.Value = AgL.XNull(DtTemp.Rows(I)("ItemGroupDesc"))
                            Dimension1Desc.Value = AgL.XNull(DtTemp.Rows(I)("Dimension1Desc"))
                            Dimension2Desc.Value = AgL.XNull(DtTemp.Rows(I)("Dimension2Desc"))
                            Dimension3Desc.Value = AgL.XNull(DtTemp.Rows(I)("Dimension3Desc"))
                            Dimension4Desc.Value = AgL.XNull(DtTemp.Rows(I)("Dimension4Desc"))
                            SizeDesc.Value = AgL.XNull(DtTemp.Rows(I)("SizeDesc"))
                            PurchaseRate.Value = AgL.VNull(DtTemp.Rows(I)("PurchaseRate"))
                            SaleRate.Value = AgL.VNull(DtTemp.Rows(I)("SaleRate"))
                            MRP.Value = AgL.VNull(DtTemp.Rows(I)("MRP"))
                            ReceiveQty.Value = AgL.VNull(DtTemp.Rows(I)("ReceiveQty"))


                            If AgL.XNull(DtTemp.Rows(I)("Barcode")) <> "" Then
                                BarCodeImg.Value = GetBarcodeImage(AgL.XNull(DtTemp.Rows(I)("Barcode")), 200, 50)
                            Else
                                BarCodeImg.Value = GetBarcodeImage("0", 200, 50)
                            End If


                            cmd.Parameters.Add(Code)
                            cmd.Parameters.Add(Barcode)
                            cmd.Parameters.Add(BarCodeImg)
                            cmd.Parameters.Add(ItemDesc)
                            cmd.Parameters.Add(ItemCategoryDesc)
                            cmd.Parameters.Add(ItemGroupDesc)
                            cmd.Parameters.Add(Dimension1Desc)
                            cmd.Parameters.Add(Dimension2Desc)
                            cmd.Parameters.Add(Dimension3Desc)
                            cmd.Parameters.Add(Dimension4Desc)
                            cmd.Parameters.Add(SizeDesc)
                            cmd.Parameters.Add(PurchaseRate)
                            cmd.Parameters.Add(SaleRate)
                            cmd.Parameters.Add(MRP)
                            cmd.Parameters.Add(ReceiveQty)
                            cmd.ExecuteNonQuery()
                        End If
                    Next
                Next





                mQry = " Select H.Code, H.Barcode, H.BarCodeImg, H.ItemDesc, H.ItemCategoryDesc, IfNull(Ig.PrintingDescription, H.ItemGroupDesc) as ItemGroupDesc, 
                        H.Dimension1Desc, H.Dimension2Desc, H.Dimension3Desc, H.Dimension4Desc, H.SizeDesc, 
                        H.PurchaseRate, H.SaleRate, H.MRP, H.ReceiveQty, B.GenSr, I.PurchaseRate as ItemMasterPurchaseRate, I.Rate as ItemMasterSaleRate, 
                        " & AgL.Chk_Text(mBarcodeRatePrefix) & " as BarcodeRatePrefix,
                        " & AgL.Chk_Text(mBarcodePrintTitle1) & " as Title1,
                        " & AgL.Chk_Text(mBarcodePrintTitle2) & " as Title2,
                        " & AgL.Chk_Text(mBarcodePrintTitle3) & " as Title3 "

                If RepName = "Barcode_Print_Coded.rpt" Then
                    Dim bRateTypeQry = "Select Rt.Code, Rt.Description As RateTypeDesc
                        From RateType Rt "
                    Dim DtRateTypes As DataTable = AgL.FillData(bRateTypeQry, AgL.GCn).Tables(0)

                    For K As Integer = 0 To DtRateTypes.Rows.Count - 1
                        mQry += ", (Select Rate*1.0 From RateListDetail Where Item = I.Code And RateType = '" & DtRateTypes.Rows(K)("Code") & "') As [" & DtRateTypes.Rows(K)("RateTypeDesc") & "]  "
                        'If K <> DtRateTypes.Rows.Count - 1 Then mQry += ", "
                    Next
                Else
                    Dim bBarcodeRateType As String = ""
                    bBarcodeRateType = ClsMain.FGetSettings(SettingFields.BarcodePrintSaleRateType, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
                    mQry += ", (Select Rate*1.0 From RateListDetail Where Item = I.Code And RateType = '" & bBarcodeRateType & "')  As SaleRate_RateType  "

                    Dim bBarcodeRateTypeEncoded As String = ""
                    bBarcodeRateTypeEncoded = ClsMain.FGetSettings(SettingFields.BarcodePrintSaleRateTypeEncoded, SettingType.General, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
                    mQry += ", IfNull((Select Rate*1.0 From RateListDetail Where Item = I.Code And RateType = '" & bBarcodeRateTypeEncoded & "'),0)  As SaleRate_RateTypeEncoded  "
                End If




                If AgL.PubServerName = "" Then
                    mQry = mQry & ", Abs(Cast(RANDOM() % (900) AS INT)) + 100 as RandomNo1, Abs(Cast(RANDOM() % (900)  AS INT)) + 100 as RandomNo2 "
                Else
                    mQry = mQry & ", Convert(INT,RAND()*(900)) + 100 as RandomNo1, Convert(INT,RAND()*(900)) + 100 as RandomNo2 "
                End If




                mQry = mQry & " From [#" & bTempTable & "] H 
                        Left Join Barcode B On H.Code = B.Code
                        Left Join Item I On B.Item = I.Code                         
                        Left Join Item IG On I.ItemGroup = IG.Code "





                If mQry.Trim <> "" Then
                    DsRep = AgL.FillData(mQry, AgL.GCn)
                    AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)
                    mCrd.Load(AgL.PubReportPath & "\" & RepName)
                    mCrd.SetDataSource(DsRep.Tables(0))
                    CType(ReportView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd



                    AgPL.Formula_Set(mCrd, RepTitle)
                    If PrintAction = PrintAction_Preview Then
                        'ReportView.CrvReport.ShowPrintButton = False
                        AgPL.Show_Report(ReportView, "* " & RepTitle & " *", Me.MdiParent)
                    Else
                        mCrd.PrintToPrinter(1, True, 0, 0)
                    End If
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
        Img = b.Encode(BarcodeLib.TYPE.CODE128, TextValue, IIf(TextValue = "0", Drawing.Color.White, Drawing.Color.Black), Drawing.Color.White, Width, Hight)
        GetBarcodeImage = b.Encoded_Image_Bytes
    End Function
    Private Sub Dgl1_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            Dim bColumnIndex As Integer = Dgl1.CurrentCell.ColumnIndex
            Dim bRowIndex As Integer = Dgl1.CurrentCell.RowIndex
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1PrintQty
                    'If Dgl1.Item(Col1BarcodeType, Dgl1.CurrentCell.RowIndex).Value = BarcodeType.UniquePerPcs Then
                    '    Dgl1.Item(Col1PrintQty, bRowIndex).ReadOnly = True
                    'Else
                    '    Dgl1.Item(Col1PrintQty, bRowIndex).ReadOnly = False
                    'End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmBarcodeGenerate_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub TxtBarcodeType_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtBarcodeType.KeyDown
        Try
            Select Case sender.Name
                Case TxtBarcodeType.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtBarcodeType.AgHelpDataSet Is Nothing Then
                            mQry = "SELECT 'Portrait' Code, 'Portrait' AS Unit UNION All  SELECT 'Landscape' Code, 'Landscape' AS Unit"
                            TxtBarcodeType.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ApplyUISetting()
        ClsMain.GetUISetting_WithDataTables(Dgl1, Me.Name, AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", ClsMain.GridTypeConstants.HorizontalGrid)
    End Sub
End Class