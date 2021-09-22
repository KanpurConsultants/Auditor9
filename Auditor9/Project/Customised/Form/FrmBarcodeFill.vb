Imports AgLibrary.ClsMain.agConstants

Public Class FrmBarcodeFill
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1Barcode As String = "Barcode"

    Dim mQry As String = ""
    Dim mQty As Integer = 0
    Public mDocId As String = ""
    Public mItemCode As String = ""
    Public mSr As Integer
    Public mPurchaseRate As Double
    Public mSaleRate As Double
    Public mMRP As Double

    Public mBarcodeType As String = ""
    Public mBarcodePattern As String = ""

    Public Property Qty() As String
        Get
            Qty = mQty
        End Get
        Set(ByVal value As String)
            mQty = value
        End Set
    End Property

    Public Property DocNo() As String
        Get
            DocNo = LblDocNo.Text
        End Get
        Set(ByVal value As String)
            LblDocNo.Text = value
        End Set
    End Property
    Public Property ItemName() As String
        Get
            ItemName = LblItemName.Text
        End Get
        Set(ByVal value As String)
            LblItemName.Text = value
        End Set
    End Property

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub Ini_Grid()
        Dim I As Integer = 0
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 50, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Barcode, 230, 0, Col1Barcode, True, IIf(mBarcodePattern = BarcodePattern.Auto, True, False))
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple

        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AgAllowFind = False
        Dgl1.AllowUserToOrderColumns = True
        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple

        Dgl1.AllowUserToAddRows = False
        Dgl1.EnableHeadersVisualStyles = True
        AgL.GridDesign(Dgl1)

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub
    Private Sub FrmBarcode_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If mBarcodePattern = BarcodePattern.Auto Then
            BtnOK.Enabled = False
        End If
    End Sub
    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click, BtnCancel.Click
        Dim MyCommand As OleDb.OleDbDataAdapter = Nothing
        Select Case sender.name
            Case BtnOK.Name
                ProcSave()

            Case BtnCancel.Name
                Me.Close()
        End Select
    End Sub
    Private Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            Me.Close()
        End If
    End Sub
    Private Sub ProcSave()
        Dim mCode As Integer = 0
        Dim mPrimaryCode As Integer = 0
        Dim mTrans As String = ""
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing

        Dim mV_Type As String = "", mManualRefNo As String = "", mSubcode As String = "", mProcess As String = "", mGodown As String = ""

        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Barcode, I).Value.ToString() = "" Or Dgl1.Item(Col1Barcode, I).Value.ToString() = Nothing Then
                MsgBox("Barcode is blank at row no." + (I + 1).ToString + "...!", MsgBoxStyle.Information)
                Exit Sub
            End If
        Next

        mQry = " Select L.V_Type, L.RecId, L.SubCode, L.Process, L.Godown 
                From Stock L Where L.DocId = '" & mDocId & "' And Sr = " & Val(mSr) & ""
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            mV_Type = AgL.XNull(DtTemp.Rows(0)("V_Type"))
            mManualRefNo = AgL.XNull(DtTemp.Rows(0)("RecId"))
            mSubcode = AgL.XNull(DtTemp.Rows(0)("SubCode"))
            mProcess = AgL.XNull(DtTemp.Rows(0)("Process"))
            mGodown = AgL.XNull(DtTemp.Rows(0)("Godown"))
        End If

        mPrimaryCode = AgL.Dman_Execute("Select IfNull(Max(Code),0) From BarCode", AgL.GCn).ExecuteScalar()


        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            For I = 0 To Dgl1.Rows.Count - 1
                If Dgl1.Item(Col1Barcode, I).Tag = "" Or Dgl1.Item(Col1Barcode, I).Value = Nothing Then
                    mCode = AgL.Dman_Execute("Select IfNull(Max(Code),0) + 1 From BarCode", AgL.GCn).ExecuteScalar()
                    mQry = " INSERT INTO Barcode (Code, Div_Code, Description, Item, GenDocID, GenSr, Qty,SaleRate, PurchaseRate, MRP)
                    VALUES (" & AgL.Chk_Text(mCode) & ", " & AgL.Chk_Text(AgL.PubDivCode) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, I).Value) & ", " & AgL.Chk_Text(mItemCode) & ",
                    " & AgL.Chk_Text(mDocId) & ", " & mSr & ", 1, " & mSaleRate & ", " & mPurchaseRate & ", " & mMRP & ") "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                    mQry = " INSERT INTO BarcodeSiteDetail (Code,Div_Code, Site_Code, LastTrnDocID,
                        LastTrnSr, LastTrnV_Type, LastTrnManualRefNo,
                        LastTrnSubcode, LastTrnProcess, CurrentGodown, Status)
                        VALUES (" & AgL.Chk_Text(mCode) & ", " & AgL.Chk_Text(AgL.PubDivCode) & ", " & AgL.Chk_Text(AgL.PubSiteCode) & ",
                        " & AgL.Chk_Text(mDocId) & ", " & Val(mSr) & ", " & AgL.Chk_Text(mV_Type) & ", " & AgL.Chk_Text(mManualRefNo) & ",
                        " & AgL.Chk_Text(mSubcode) & ", " & AgL.Chk_Text(mProcess) & ", " & AgL.Chk_Text(mGodown) & ", 'Receive') "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                Else
                    mQry = " UPDATE Barcode
                            Set Description = " & AgL.Chk_Text(Dgl1.Item(Col1Barcode, I).Value) & ",
                            SaleRate = " & mSaleRate & ",
                            PurchaseRate = " & mPurchaseRate & ",
                            MRP = " & mMRP & "
                            Where Code = '" & Dgl1.Item(Col1Barcode, I).Tag & "'"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                End If
            Next
            AgL.ETrans.Commit()
            mTrans = "Commit"
            Me.Close()
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim I As Integer = 0, Cnt = 0
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Barcode
                    If mRowIndex <> Dgl1.Rows.Count - 1 Then
                        If MsgBox("Do you want to fill barcodes...?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                            For I = mRowIndex + 1 To Dgl1.Rows.Count - 1
                                Cnt = Cnt + 1
                                Dgl1.Item(Col1Barcode, I).Value = Dgl1.Item(Col1Barcode, mRowIndex).Value + Cnt
                            Next
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub MovRec()
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0

        mQry = "Select Code, Description From BarCode Where GenDocId = '" & mDocId & "' And GenSr = " & mSr & ""
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            Dgl1.Rows.Clear()
            For I = 0 To DtTemp.Rows.Count - 1
                Dgl1.Rows.Add()
                Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                Dgl1.Item(Col1Barcode, I).Tag = AgL.XNull(DtTemp.Rows(I)("Code"))
                Dgl1.Item(Col1Barcode, I).Value = AgL.XNull(DtTemp.Rows(I)("Description"))
            Next
        End If

        If mBarcodeType = BarcodeType.UniquePerPcs Then
            Dim mStartIndex As Integer = 0
            If Dgl1.Rows.Count < mQty Then
                mStartIndex = Dgl1.Rows.Count
                For I = mStartIndex To mQty - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                Next
            End If
        ElseIf mBarcodeType = BarcodeType.LotWise Then
            If Dgl1.Rows.Count < 1 Then
                Dgl1.Rows.Add()
                Dgl1.Item(ColSNo, 0).Value = Dgl1.Rows.Count
            End If
        ElseIf mBarcodeType = BarcodeType.Fixed Then
            If DtTemp.Rows.Count = 0 Then
                Dim mFixedBarcode$ = AgL.Dman_Execute("Select Bc.Description As Barcode
                                From Item I 
                                LEFT JOIN Barcode Bc On I.Barcode = Bc.Code
                                Where I.Code = '" & mItemCode & "' ", AgL.GCn).ExecuteScalar()
                If (mFixedBarcode <> Nothing) Then
                    Dgl1.Rows.Clear()
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, 0).Value = Dgl1.Rows.Count
                    Dgl1.Item(Col1Barcode, 0).Value = mFixedBarcode
                End If
            End If
        End If
    End Sub
End Class