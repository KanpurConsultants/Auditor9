Public Class FrmVoucherEntry_TDS
    Private Const GSNo As Byte = 0
    Private Const GDescription As Byte = 1
    Private Const GDescriptionCode As Byte = 2
    Private Const GPosting As Byte = 3
    Private Const GPostingCode As Byte = 4
    Private Const GPersentage As Byte = 5
    Private Const GAmount As Byte = 6
    Private Const GFormula As Byte = 7

    Dim FrmVEMain As FrmVoucherEntry
    Dim IntRowMain As Integer
    Private WithEvents FGMain As New AgControls.AgDataGrid
    Private LIEvent As ClsEvents
    Sub New(ByVal FrmVEVar As FrmVoucherEntry, ByVal IntRowVar As Integer, _
    ByVal StrTDSCategoryCode As String, ByVal StrTDSCategory As String, _
    ByVal StrTDSDeductFrom As String, ByVal StrTDSDeductFromName As String, _
    ByVal DblOnAmount As Double, ByVal DblAmount As Double, ByVal BlnMode As Boolean)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        FrmVEMain = FrmVEVar
        IntRowMain = IntRowVar
        TxtTDSCategory.Text = StrTDSCategory
        TxtTDSCategory.Tag = StrTDSCategoryCode
        TxtTDSDeductFrom.Text = StrTDSDeductFromName
        TxtTDSDeductFrom.Tag = StrTDSDeductFrom
        TxtOnAmount.Text = Format(DblOnAmount, "0.00")
        TxtAmount.Text = Format(DblAmount, "0.00")
        TxtTotalTDS.ReadOnly = True
        BtnOK.Enabled = BlnMode
    End Sub
    Private Sub FrmVoucherEntry_TDS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim I As Int16
        LIEvent = New ClsEvents(Me)
        Me.BackColor = CMain.ClrPubBackColorForm
        Agl.GridDesign(FGMain)
        IniGrid()
        IniList()
        CMain.SetEnableDisable(Me, BtnOK.Enabled)
        FGMain.Rows.Clear()

        Try
            FGMain.Rows.Add(UBound(FrmVEMain.SVTMain.TDSVar))
        Catch Ex As Exception
        End Try

        For I = 0 To UBound(FrmVEMain.SVTMain.TDSVar)
            FGMain(GSNo, I).Value = Trim(I + 1)
            FGMain(GDescription, I).Value = FrmVEMain.SVTMain.TDSVar(I).StrDesc
            FGMain(GDescriptionCode, I).Value = FrmVEMain.SVTMain.TDSVar(I).StrDescCode
            FGMain(GPosting, I).Value = FrmVEMain.SVTMain.TDSVar(I).StrPostingAc
            FGMain(GPostingCode, I).Value = FrmVEMain.SVTMain.TDSVar(I).StrPostingAcCode
            FGMain(GPersentage, I).Value = Format(FrmVEMain.SVTMain.TDSVar(I).DblPercentage, "0.000")
            FGMain(GAmount, I).Value = Format(FrmVEMain.SVTMain.TDSVar(I).DblAmount, "0.00")
            FGMain(GFormula, I).Value = FrmVEMain.SVTMain.TDSVar(I).StrFormula
        Next
        FCalculate()
    End Sub

    Sub IniList()
        Dim mQry$
        mQry = "Select SG.SubCode,SG.Name,IsNull(CT.CityName,''),SG.ManualCode From SubGroup SG Left Join City CT On CT.CityCode=SG.CityCode Where SG.SiteList Like '%|" & AgL.PubSiteCode & "|%' Order by SG.Name"
        TxtTDSDeductFrom.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select Code,Name From TDSCat Order By Name"
        TxtTDSCategory.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub IniGrid()
        FGMain.Height = PnlMain.Height
        FGMain.Width = PnlMain.Width
        FGMain.Top = PnlMain.Top
        FGMain.Left = PnlMain.Left
        Controls.Add(FGMain)
        FGMain.Visible = True
        FGMain.BringToFront()
        AgCl.AddAgTextColumn(FGMain, "SNo", 40, 5, "S.No.", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Description", 180, 10, "Description", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "DescriptionCode", 0, 0, "DescriptionCode", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "Posting", 300, 10, "Posting A/c", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "PostingCode", 0, 0, "PostingCode", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "Percentage", 60, 10, "(%)Per.", True, True, True)
        AgCl.AddAgTextColumn(FGMain, "Amount", 120, 10, "Amount", True, True, True)
        AgCl.AddAgTextColumn(FGMain, "Formula", 0, 0, "Formula", False, True, False)

        FGMain.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
        FGMain.DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
        FGMain.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        AgL.FSetSNo(FGMain, GSNo)
        FGMain.TabIndex = PnlMain.TabIndex
    End Sub
    Private Sub FGMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FGMain.KeyDown
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub
    Private Sub FGMain_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles FGMain.RowsAdded
        FGMain(GSNo, FGMain.Rows.Count - 1).Value = Trim(FGMain.Rows.Count)
    End Sub
    Private Sub FGMain_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles FGMain.RowsRemoved
        Agl.FSetSNo(FGMain, GSNo)
    End Sub
    Public Sub FTxtKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.Name
            Case TxtOnAmount.Name, TxtAmount.Name
                CMain.NumPress(sender, e, 10, 2, False)
        End Select

    End Sub
    Private Sub BtnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles BtnOK.Click, BtnCancel.Click
        Dim I As Integer

        Select Case sender.name
            Case BtnOK.Name
                If AgL.RequiredField(TxtTDSCategory, "TDS Category") Then Exit Sub
                If AgL.RequiredField(TxtTDSDeductFrom, "TDS Deduct From") Then Exit Sub
                If AgL.RequiredField(TxtOnAmount, "TDS On Amount") Then Exit Sub
                If Not Val(TxtAmount.Text) > 0 Then TxtAmount.Text = Format(Val(TxtOnAmount.Text), "0.00")
                If (Val(TxtAmount.Text) - Val(TxtTotalTDS.Text)) < 0 Then MsgBox("Total TDS Should Not Be Greater Than Amount.") : Exit Sub

                ReDim FrmVEMain.SVTMain.TDSVar(FGMain.Rows.Count - 1)
                For I = 0 To FGMain.Rows.Count - 1
                    If Trim(FGMain(GDescription, I).Value) <> "" Then
                        FrmVEMain.SVTMain.TDSVar(I).StrDesc = FGMain(GDescription, I).Value
                        FrmVEMain.SVTMain.TDSVar(I).StrDescCode = FGMain(GDescriptionCode, I).Value
                        FrmVEMain.SVTMain.TDSVar(I).StrPostingAc = FGMain(GPosting, I).Value
                        FrmVEMain.SVTMain.TDSVar(I).StrPostingAcCode = FGMain(GPostingCode, I).Value
                        FrmVEMain.SVTMain.TDSVar(I).DblPercentage = FGMain(GPersentage, I).Value
                        FrmVEMain.SVTMain.TDSVar(I).DblAmount = FGMain(GAmount, I).Value
                        FrmVEMain.SVTMain.TDSVar(I).StrFormula = FGMain(GFormula, I).Value
                    End If
                Next
                FrmVEMain.FGMain(FrmVoucherEntry.GTDSCategory, IntRowMain).Value = TxtTDSCategory.Text
                FrmVEMain.FGMain(FrmVoucherEntry.GTDSCategoryCode, IntRowMain).Value = TxtTDSCategory.Tag

                FrmVEMain.FGMain(FrmVoucherEntry.GTDSDeductFromName, IntRowMain).Value = TxtTDSDeductFrom.Text
                FrmVEMain.FGMain(FrmVoucherEntry.GTDSDeductFrom, IntRowMain).Value = TxtTDSDeductFrom.Tag

                FrmVEMain.FGMain(FrmVoucherEntry.GTDSOnAmount, IntRowMain).Value = TxtOnAmount.Text

                FrmVEMain.FGMain(FrmVoucherEntry.GOrignalAmt, IntRowMain).Value = Val(TxtAmount.Text)

                If Val(FrmVEMain.FGMain(FrmVoucherEntry.GCredit, IntRowMain).Value) > 0 Then
                    FrmVEMain.FGMain(FrmVoucherEntry.GCredit, IntRowMain).Value = Format(Val(TxtAmount.Text), "0.00")
                ElseIf Val(FrmVEMain.FGMain(FrmVoucherEntry.GDebit, IntRowMain).Value) > 0 Then
                    FrmVEMain.FGMain(FrmVoucherEntry.GDebit, IntRowMain).Value = Format(Val(TxtAmount.Text) - Val(TxtTotalTDS.Text), "0.00")
                ElseIf FrmVEMain.FGMain.Columns(FrmVoucherEntry.GCredit).Visible Then
                    FrmVEMain.FGMain(FrmVoucherEntry.GCredit, IntRowMain).Value = Format(Val(TxtAmount.Text), "0.00")
                ElseIf FrmVEMain.FGMain.Columns(FrmVoucherEntry.GDebit).Visible Then
                    FrmVEMain.FGMain(FrmVoucherEntry.GDebit, IntRowMain).Value = Format(Val(TxtAmount.Text) - Val(TxtTotalTDS.Text), "0.00")
                End If

                FrmVEMain.FUpdateRowStructure(FrmVEMain.SVTMain, IntRowMain)
                Me.Close()
            Case BtnCancel.Name
                Me.Close()
        End Select
    End Sub
    Private Sub FrmVoucherEntry_TDS_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Agl.FPaintForm(Me, e, 0)
    End Sub

    Private Sub TxtOnAmount_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtOnAmount.KeyUp
        Select Case sender.Name
            Case TxtOnAmount.Name
                FCalculate()
        End Select
    End Sub
    Public Sub FCalculate()
        Dim I As Integer
        Dim J As Integer
        Dim StrOrgFormula As String
        Dim StrTemp As String
        Dim StrConvFormula As String
        Dim BlnFlag As Boolean
        Dim StrCode As String = ""
        Dim DblValue As Double
        Dim DblTotal As Double = 0
        Dim DblGrossValue As Double

        DblGrossValue = Val(TxtOnAmount.Text)
        TxtTotalTDS.Text = "0.00"
        For I = 0 To FGMain.Rows.Count - 1
            StrOrgFormula = IIf(Trim(FGMain(GFormula, I).Value) = "", "[G]", Trim(FGMain(GFormula, I).Value))
            StrConvFormula = ""
            For J = 1 To Len(Trim(StrOrgFormula))
                StrTemp = UCase(Mid(StrOrgFormula, J, 1))
                If BlnFlag Then
                    If StrTemp = "]" Then
                        BlnFlag = False
                        If StrCode = "G" Then
                            StrConvFormula = StrConvFormula + Format(DblGrossValue, "0.00")
                        Else
                            StrConvFormula = StrConvFormula + Format(FOtherAddDedFetch(StrCode, 1), "0.00")
                        End If
                    Else
                        StrCode = StrCode + StrTemp
                    End If
                Else
                    If StrTemp = "[" Then
                        StrCode = ""
                        BlnFlag = True
                    Else
                        StrConvFormula = StrConvFormula + StrTemp
                    End If
                End If
            Next
            If Trim(StrConvFormula) = "" Then StrConvFormula = 0
            DblValue = FGetSinleValue("Select " & StrConvFormula & " As Result ", Agl.Gcn)
            If FrmVEMain.BlnTDSROff Then
                FGMain(GAmount, I).Value = Format((DblValue * Val(FGMain(GPersentage, I).Value)) / 100, "0")
            Else
                FGMain(GAmount, I).Value = Format((DblValue * Val(FGMain(GPersentage, I).Value)) / 100, "0.00")
            End If
            FGMain(GAmount, I).Value = Format(Val(FGMain(GAmount, I).Value), "0.00")
            DblTotal = DblTotal + Val(FGMain(GAmount, I).Value)
        Next

        TxtTotalTDS.Text = Format(DblTotal, "0.00")
    End Sub
    Public Function FOtherAddDedFetch(ByVal StrExpCode As String, ByVal FetchWhat As Integer) As Double 'FetchWhat 0=Persentage,1=Amount
        Dim I As Integer
        Dim RtnValue As Double
        RtnValue = 0

        For I = 0 To FGMain.Rows.Count - 1
            If UCase(Trim(FGMain.Item(GDescriptionCode, I).Value)) = Trim(UCase(StrExpCode)) Then
                Select Case FetchWhat
                    Case 0
                        RtnValue = Val(FGMain(GPersentage, I).Value)
                    Case 1
                        RtnValue = Val(FGMain.Item(GAmount, I).Value)
                End Select
                Exit For
            End If
        Next
        FOtherAddDedFetch = RtnValue
    End Function
End Class