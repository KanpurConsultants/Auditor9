Imports System.Data.SQLite
Imports System.IO
Imports System.Xml
Imports AgAccounts.ClsMain

Public Class FrmVoucherEntry
    Public Const GSNo As Byte = 0
    Public Const GAcCode As Byte = 1
    Public Const GAcManaulCode As Byte = 2
    Public Const GAcName As Byte = 3
    Public Const GLinkAcCode As Byte = 4
    Public Const GLinkAcName As Byte = 5
    Public Const GCostCenter As Byte = 6
    Public Const GCostCenterCode As Byte = 7
    Public Const GRecId As Byte = 8
    Public Const GRecDate As Byte = 9
    Public Const GNarration As Byte = 10
    Public Const GDebit As Byte = 11
    Public Const GCredit As Byte = 12
    Public Const GChqDet_Btn As Byte = 13
    Public Const GTDS_Btn As Byte = 14
    Public Const GAdj_Btn As Byte = 15
    Public Const GChqNo As Byte = 16
    Public Const GChqDate As Byte = 17
    Public Const GTDSCategory As Byte = 18
    Public Const GTDSCategoryCode As Byte = 19
    Public Const GTDSOnAmount As Byte = 20
    Public Const GAcBal As Byte = 21
    Public Const GIAdj_Btn As Byte = 22
    Public Const GOrignalAmt As Byte = 23
    Public Const GTDSDeductFrom As Byte = 24
    Public Const GTDSDeductFromName As Byte = 25
    Public Const GClgDate As Byte = 26

    Public DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Public WithEvents FGMain As New AgControls.AgDataGrid
    Public LIEvent As ClsEvents


    Public SVTMain As ClsStructure.VoucherType
    Private DTStruct As New DataTable
    Private FormWorkAs As Byte
    Private BlnMaintainTDS As Boolean = True
    Public BlnTDSROff As Boolean = False
    Private BlnAutoPosting As Boolean = False
    Dim StrTypeTemp As String, StrTypeTagTemp As String, StrCurrentType As String
    Dim StrAcTemp As String, StrAcTagTemp As String, StrDateTemp As String
    Dim StrDefaultAcCode As String, StrDefaultAcName As String
    Dim StrCompareRecIdTemp As String, StrCompareDateTemp As String
    Dim RFNumberSystem As ClsMain.RecIdFormat

    Dim StrCopyDocId As String
    Dim mOpenDocId As String = ""
    Public Property OpenDocId() As String
        Get
            Return mOpenDocId
        End Get
        Set(ByVal value As String)
            mOpenDocId = value
        End Set
    End Property

    Private Sub FrmVoucherEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Or e.KeyCode = Keys.F3 Or e.KeyCode = Keys.F4 Or e.KeyCode = (Keys.F And e.Control) Or e.KeyCode = (Keys.P And e.Control) _
        Or e.KeyCode = (Keys.S And e.Control) Or e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F5 Or e.KeyCode = Keys.F10 _
        Or e.KeyCode = Keys.Home Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Or e.KeyCode = Keys.End Then
            Topctrl1.TopKey_Down(e)
        End If
    End Sub

    Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal FormWorkAsVar As ClsStructure.EntryType)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
        FormWorkAs = FormWorkAsVar
    End Sub

    Sub New(ByVal DTUP As DataTable, ByVal FormWorkAsVar As ClsStructure.EntryType)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, "***P", DTUP)
        Topctrl1.SetDisp(True)
        FormWorkAs = FormWorkAsVar
        Me.Text = "Voucher Entry (Post)"
    End Sub

    Private Sub FrmVoucherEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim DTTemp As DataTable
            LIEvent = New ClsEvents(Me)
            AgL.WinSetting(Me, 665, 990, 0, 0)
            AgL.GridDesign(FGMain)

            DTTemp = CMain.FGetDatTable("Select MaintainTDS,AutoPosting,VRNumberSystem,TDSROff From Enviro_Accounts", AgL.GCn)
            If DTTemp.Rows.Count > 0 Then BlnMaintainTDS = IIf(UCase(AgL.XNull(DTTemp.Rows(0).Item("MaintainTDS"))) = "Y", True, False)
            If DTTemp.Rows.Count > 0 Then BlnAutoPosting = IIf(UCase(AgL.XNull(DTTemp.Rows(0).Item("AutoPosting"))) = "Y", True, False)
            If DTTemp.Rows.Count > 0 Then RFNumberSystem = IIf(UCase(AgL.XNull(DTTemp.Rows(0).Item("VRNumberSystem"))) = "D", ClsMain.RecIdFormat.DD_MM,
                                                           IIf(UCase(AgL.XNull(DTTemp.Rows(0).Item("VRNumberSystem"))) = "M", ClsMain.RecIdFormat.MM, ClsMain.RecIdFormat.DD_MM_YY))
            If DTTemp.Rows.Count > 0 Then BlnTDSROff = IIf(UCase(AgL.XNull(DTTemp.Rows(0).Item("TDSROff"))) = "Y", True, False)
            DTTemp.Dispose()
            IniGrid()
            FIniMaster()
            MoveRec()

            If Not AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
                MnuImportFromDos.Visible = False
                MnuImportFromExcel.Visible = False
                MnuImportFromTally.Visible = False
                MnuImportFromTallyLedgerOpening.Visible = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub IniGrid()
        FIniStructure()
        FGMain.Height = PnlMain.Height
        FGMain.Width = PnlMain.Width
        FGMain.Top = PnlMain.Top
        FGMain.Left = PnlMain.Left
        PnlMain.Visible = False
        Controls.Add(FGMain)
        FGMain.Visible = True
        FGMain.BringToFront()
        AgCl.AddAgTextColumn(FGMain, "SNo", 42, 5, "S.No.", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "AcCode", 0, 5, "Ac Code", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "AcManual", 65, 0, "A/c Code", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "AcName", 190, 0, "A/c Name", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "LinkAcCode", 0, 5, "Link Ac Code", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "LinkAcName", 190, 0, "Linked A/c Name", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "CostCenter", 90, 0, "Cost Center", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "CostCenterCode", 0, 5, "CostCenterCode", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "Ref.No.", 80, 10, "Ref.No.", False, False, False)
        AgCl.AddAgDateColumn(FGMain, "Ref.Date", 90, "Ref.Date", False, False, False)
        AgCl.AddAgTextColumn(FGMain, "Narration", 190, 250, "Narration", True, False, False)
        AgCl.AddAgTextColumn(FGMain, "Dr", 110, 20, "Debit", True, False, True)
        AgCl.AddAgTextColumn(FGMain, "Cr", 110, 20, "Credit", True, False, True)

        AgCl.AddAgButtonColumn(FGMain, "ChqDet", 35, "Chq.")
        AgCl.AddAgButtonColumn(FGMain, "TDS", 35, "TDS")
        AgCl.AddAgButtonColumn(FGMain, "Adjustment", 35, "Adj.")
        AgCl.AddAgTextColumn(FGMain, "ChqNo", 0, 0, "ChqNo", False, False, False)
        AgCl.AddAgTextColumn(FGMain, "ChqDate", 0, 0, "ChqDate", False, False, False)
        AgCl.AddAgTextColumn(FGMain, "TDSCategory", 0, 0, "TDSCategory", False, False, False)
        AgCl.AddAgTextColumn(FGMain, "TDSCategoryCode", 0, 0, "TDSCategoryCode", False, False, False)
        AgCl.AddAgTextColumn(FGMain, "TDSOnAmount", 0, 0, "TDSOnAmount", False, False, False)
        AgCl.AddAgTextColumn(FGMain, "AcBal", 0, 0, "Acbal", False, False, False)
        AgCl.AddAgButtonColumn(FGMain, "ItemAdjustment", 35, "Item", False)
        AgCl.AddAgTextColumn(FGMain, "OrignalAmt", 0, 0, "OrignalAmt", False, False, False)
        AgCl.AddAgTextColumn(FGMain, "TDSDeductFrom", 0, 0, "TDSDeductFrom", False, False, False)
        AgCl.AddAgTextColumn(FGMain, "TDSDeductFromName", 0, 0, "TDSDeductFromName", False, False, False)
        AgCl.AddAgTextColumn(FGMain, "ClgDate", 0, 0, "ClgDate", False, False, False)

        FGMain.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
        FGMain.DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
        FGMain.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        AgL.FSetSNo(FGMain, GSNo)
        FGMain.TabIndex = PnlMain.TabIndex

        FGMain.AgAllowFind = False
    End Sub

    Private Sub FManageDisplay(ByVal BlnEnb As Boolean)
        FGMain.Columns(GNarration).ReadOnly = BlnEnb
        FGMain.Columns(GDebit).ReadOnly = BlnEnb
        FGMain.Columns(GCredit).ReadOnly = BlnEnb

        BtnJournal.Enabled = False
        BtnPayments.Enabled = False
        BtnReceipt.Enabled = False
        TxtPrepared.Enabled = False
        TxtModified.Enabled = False
        TxtVNo.Enabled = False
        TxtRecId.Enabled = False
        LblPtyBalance.Text = ""

        BtnImport.Enabled = False
        BtnRefreshVNo.Visible = False
        BtnPaste.Visible = False
        BtnCopy.Visible = False
    End Sub

    Public Sub FManageScreen(ByVal StrScreenType As String, Optional ByVal BlnClearData As Boolean = True)
        FGMain.Columns(GRecId).Visible = False

        If Trim(StrScreenType) = "" Then StrScreenType = "PMT"

        LblCurrentType.Tag = StrScreenType
        If BlnClearData Then
            FClear()
            TxtType.Tag = ""
            TxtType.Text = ""
            TxtVNo.Text = ""
            TxtVNo.Tag = ""
            FUpdateRowStructure(New ClsStructure.VoucherType, 0)
        End If

        Select Case Trim(UCase(StrScreenType))
            Case "PMT"
                LblAcName.Enabled = True
                TxtAcName.Enabled = True
                LblAcName.Text = "Credit A/c"
                LblCurrentType.Text = "PAYMENT"
                LblCurrentType.ForeColor = Color.FromArgb(247, 185, 237)
                LblFormBackColor.ForeColor = Color.FromArgb(247, 235, 237)

                FGMain.Columns(GDebit).Visible = True
                FGMain.Columns(GCredit).Visible = False
                If BlnMaintainTDS Then FGMain.Columns(GNarration).Width = 300 Else FGMain.Columns(GNarration).Width = 340

            Case "RCT"
                LblAcName.Enabled = True
                TxtAcName.Enabled = True
                LblAcName.Text = "Debit A/c"
                LblCurrentType.Text = "RECEIPT"
                LblCurrentType.ForeColor = Color.FromArgb(150, 200, 150)
                LblFormBackColor.ForeColor = Color.FromArgb(231, 239, 215)

                FGMain.Columns(GCredit).Visible = True
                FGMain.Columns(GDebit).Visible = False
                If BlnMaintainTDS Then FGMain.Columns(GNarration).Width = 300 Else FGMain.Columns(GNarration).Width = 340

            Case "JV"
                LblAcName.Enabled = False
                TxtAcName.Enabled = False
                LblAcName.Text = "A/c Name"
                LblCurrentType.Text = "JOURNAL"
                LblCurrentType.ForeColor = Color.FromArgb(200, 150, 150)
                LblFormBackColor.ForeColor = Color.FromArgb(249, 215, 203)
                FGMain.Columns(GDebit).Visible = True
                FGMain.Columns(GCredit).Visible = True

                If TxtType.Tag = "OB" Then FGMain.Columns(GRecId).Visible = True
                If TxtType.Tag = "OB" Then FGMain.Columns(GRecDate).Visible = True

                If BlnMaintainTDS Then FGMain.Columns(GNarration).Width = 190 Else FGMain.Columns(GNarration).Width = 230

                TxtAcName.Text = ""
                TxtAcName.Tag = ""
        End Select
        If BlnMaintainTDS Then FGMain.Columns(GTDS_Btn).Visible = True Else FGMain.Columns(GTDS_Btn).Visible = False
        FCalculate()
        If Topctrl1.Mode = "Browse" Then TxtAcName.Enabled = False
        Me.Refresh()
    End Sub

    Public Sub FCalculate()
        Dim I As Integer
        LblCrAmt.Text = 0
        LblDrAmt.Text = "-"
        LblDifferenceAmt.Text = 0

        For I = 0 To FGMain.Rows.Count - 1
            If Trim(FGMain(GAcCode, I).Value) <> "" Then
                LblCrAmt.Text = Format(Val(LblCrAmt.Text) + Val(FGMain(GCredit, I).Value), "0.00")
                LblDrAmt.Text = Format(Val(LblDrAmt.Text) + Val(FGMain(GDebit, I).Value), "0.00")
            End If
        Next

        If UCase(Trim(LblCurrentType.Tag)) <> "JV" Then
            If Val(LblCrAmt.Text) > Val(LblDrAmt.Text) Then
                LblDrAmt.Text = Format(Val(LblCrAmt.Text), "0.00")
            Else
                LblCrAmt.Text = Format(Val(LblDrAmt.Text), "0.00")
            End If
        End If

        If Val(LblDrAmt.Text) - Val(LblCrAmt.Text) > 0 Then
            LblDifferenceAmt.Text = "Cr " & Format(Val(LblDrAmt.Text) - Val(LblCrAmt.Text), "0.00")
        ElseIf Val(LblCrAmt.Text) - Val(LblDrAmt.Text) > 0 Then
            LblDifferenceAmt.Text = "Dr " & Format(Val(LblCrAmt.Text) - Val(LblDrAmt.Text), "0.00")
        End If
    End Sub

    Private Sub FIniMaster(Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 1)
        Dim mQry As String
        'mQry = "Select DocId As SearchCode From LedgerM Where Site_Code='" & AgL.PubSiteCode & "' And Div_Code='" & AgL.PubDivCode & "'  And (V_Date Between '" & CDate(AgL.PubStartDate).ToString("s") & "' and '" & CDate(AgL.PubEndDate).ToString("s") & "' OR V_Type='OB')  Order By V_Date,V_Type,Cast((Case When RecId GLOB '*[0-9]*' Then RecId Else 0 End) As BigInt)"
        mQry = "Select DocId As SearchCode From LedgerM Where Site_Code='" & AgL.PubSiteCode & "' And Div_Code='" & AgL.PubDivCode & "'  And (V_Date Between '" & CDate(AgL.PubStartDate).ToString("s") & "' and '" & CDate(AgL.PubEndDate).ToString("s") & "' OR V_Type='OB')  Order By V_Date,V_Type,V_No"
        mQry = AgL.GetBackendBasedQuery(mQry)
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)

        If mOpenDocId <> "" Then
            If DTMaster.Select("SearchCode='" & OpenDocId & "'").Length = 0 Then
                mQry = "Select DocID As SearchCode 
                        From LedgerM H  With (NoLock)
                        Where 1 = 1 And DocId = '" & mOpenDocId & "'  Order By V_Date , V_No  "
                mQry = AgL.GetBackendBasedQuery(mQry)
                Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
            End If
        End If
    End Sub
    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        FIniMaster(0, 0)
    End Sub

    Public Sub MoveRec()

        Dim DTTemp As New DataTable, StrCondition As String = ""
        Dim DTTemp1 As New DataTable
        Dim I As Integer, J As Int16

        FClear()
        FManageDisplay(True)
        FManageScreen(LblCurrentType.Tag)

        BtnRefreshVNo.Visible = False
        BtnPaste.Visible = False
        BtnCopy.Visible = True

        Topctrl1.BlankTextBoxes()
        If DTMaster.Rows.Count > 0 Then

            DTTemp = AgL.FillData("Select LM.DocId,LM.V_Type,LM.v_Prefix,LM.Site_Code,LM.V_No,LM.V_Date,LM.SubCode,  " &
                    "SG.Name As AcName,LM.PostedBy,LM.RecId, " &
                    "LM.Narration,LM.U_Name,LM.PreparedBy, " &
                    "VT.Description,VT.Category " &
                    "From LedgerM LM Left Join SubGroup SG On LM.SubCode=SG.SubCode Left Join " &
                    "Voucher_Type VT On VT.V_Type=LM.V_Type " &
                    "Where LM.DocId='" & AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "'", AgL.GCn).Tables(0)


            If DTTemp.Rows.Count > 0 Then


                TxtType.Text = AgL.XNull(DTTemp.Rows(0).Item("Description"))
                TxtType.Tag = AgL.XNull(DTTemp.Rows(0).Item("V_Type"))
                FManageScreen(AgL.XNull(DTTemp.Rows(0).Item("Category")), False)
                TxtModified.Text = AgL.XNull(DTTemp.Rows(0).Item("U_Name"))
                TxtPrepared.Text = AgL.XNull(DTTemp.Rows(0).Item("PreparedBy"))
                TxtPostedBy.Text = AgL.XNull(DTTemp.Rows(0).Item("PostedBy"))

                TxtVNo.Text = AgL.XNull(DTTemp.Rows(0).Item("V_No"))
                TxtVNo.Tag = AgL.XNull(DTTemp.Rows(0).Item("V_Prefix"))
                TxtVDate.Text = Format(CDate(AgL.XNull(DTTemp.Rows(0).Item("V_Date"))), "dd/MMM/yyyy")
                TxtAcName.Text = AgL.XNull(DTTemp.Rows(0).Item("AcName"))
                TxtAcName.Tag = AgL.XNull(DTTemp.Rows(0).Item("SubCode"))
                TxtNarration.Text = AgL.XNull(DTTemp.Rows(0).Item("Narration"))

                TxtRecId.Text = AgL.XNull(DTTemp.Rows(0).Item("RecId"))
                StrCompareRecIdTemp = AgL.XNull(DTTemp.Rows(0).Item("RecId"))
                StrCompareDateTemp = AgL.XNull(DTTemp.Rows(0).Item("V_Date"))
                StrCondition = ""
                If UCase(Trim(LblCurrentType.Tag)) = "PMT" Then
                    StrCondition = " And IfNull(AmtDr,0)>0 "
                ElseIf UCase(Trim(LblCurrentType.Tag)) = "RCT" Then
                    StrCondition = " And IfNull(AmtCr,0)>0 "
                End If

                DTTemp.Clear()
                DTTemp = AgL.FillData("Select LG.SubCode,SG.Name As AcName,SG.ManualCode,LG.V_SNo,CCM.Name As CCName,LG.CostCenter, LG.RecId,LG.V_Date, " &
                    "LG.AmtDr,LG.AmtCr,LG.Narration,LG.Chq_No,LG.Chq_Date, LG.Clg_Date,LG.TDSOnAmt,LG.TDSCategory,TC.Name As TDSCName,LG.OrignalAmt,LG.TDSDeductFrom,TDF.Name As TDFName " &
                    "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode Left Join " &
                    "TDSCat TC On TC.Code=LG.TDSCategory Left Join CostCenterMast CCM On CCM.Code=LG.CostCenter " &
                    "Left Join SubGroup TDF On TDF.SubCode=LG.TDSDeductFrom " &
                    "Where LG.DocId='" & AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "' And IfNull(LG.System_Generated,'N')='N' " & StrCondition & " ", AgL.GCn).Tables(0)

                If DTTemp.Rows.Count > 0 Then
                    FGMain.Rows.Add(DTTemp.Rows.Count)
                End If
                For I = 0 To DTTemp.Rows.Count - 1
                    FUpdateRowStructure(New ClsStructure.VoucherType, I)
                    FGMain(GSNo, I).Value = Trim(I + 1)

                    FGMain(GAcCode, I).Value = AgL.XNull(DTTemp.Rows(I).Item("SubCode"))
                    FGMain(GAcName, I).Value = AgL.XNull(DTTemp.Rows(I).Item("AcName"))
                    FGMain(GAcManaulCode, I).Value = AgL.XNull(DTTemp.Rows(I).Item("ManualCode"))
                    FGMain(GRecId, I).Value = Interaction.IIf(AgL.XNull(DTTemp.Rows(I).Item("RecId")).ToString.ToUpper = TxtRecId.Text.ToUpper, "", AgL.XNull(DTTemp.Rows(I).Item("RecId")))
                    FGMain(GRecDate, I).Value = Interaction.IIf(AgL.RetDate(AgL.XNull(DTTemp.Rows(I).Item("V_Date"))) = AgL.RetDate(TxtVDate.Text), "", AgL.RetDate(AgL.XNull(DTTemp.Rows(I).Item("V_Date"))))
                    FGMain(GNarration, I).Value = AgL.XNull(DTTemp.Rows(I).Item("Narration"))
                    FGMain(GDebit, I).Value = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0, Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00"), "")
                    FGMain(GCredit, I).Value = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0, Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00"), "")

                    FGMain(GChqNo, I).Value = AgL.XNull(DTTemp.Rows(I).Item("Chq_No"))
                    FGMain(GChqDate, I).Value = Format(AgL.XNull(DTTemp.Rows(I).Item("Chq_Date")), "Short Date")
                    FGMain(GClgDate, I).Value = AgL.XNull(DTTemp.Rows(I).Item("Clg_Date"))
                    If Trim(FGMain(GChqNo, I).Value) <> "" Then
                        FGMain(GChqDet_Btn, I).Style.BackColor = Color.LavenderBlush
                    End If

                    FGMain(GTDSCategory, I).Value = AgL.XNull(DTTemp.Rows(I).Item("TDSCName"))
                    FGMain(GTDSCategoryCode, I).Value = AgL.XNull(DTTemp.Rows(I).Item("TDSCategory"))

                    FGMain(GTDSDeductFrom, I).Value = AgL.XNull(DTTemp.Rows(I).Item("TDSDeductFrom"))
                    FGMain(GTDSDeductFromName, I).Value = AgL.XNull(DTTemp.Rows(I).Item("TDFName"))

                    FGMain(GTDSOnAmount, I).Value = AgL.XNull(DTTemp.Rows(I).Item("TDSOnAmt"))
                    FGMain(GCostCenter, I).Value = AgL.XNull(DTTemp.Rows(I).Item("CCName"))
                    FGMain(GCostCenterCode, I).Value = AgL.XNull(DTTemp.Rows(I).Item("CostCenter"))
                    FGMain(GOrignalAmt, I).Value = IIf(AgL.VNull(DTTemp.Rows(I).Item("OrignalAmt")) > 0, AgL.VNull(DTTemp.Rows(I).Item("OrignalAmt")), AgL.VNull(DTTemp.Rows(I).Item("TDSOnAmt")))

                    SVTMain = DTStruct.Rows(I).Item("SSDB")

                    If Trim(FGMain(GTDSCategoryCode, I).Value) <> "" Then
                        'For TDS
                        DTTemp1.Clear()
                        DTTemp1 = AgL.FillData("Select LG.FormulaString,LG.SubCode,SG.Name As AcName,SG.ManualCode, " &
                            "LG.AmtDr,LG.AmtCr,LG.TDSDesc,TCD.Name As DName,LG.TDSPer " &
                            "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode Left Join " &
                            "TDSCat_Description TCD On TCD.Code=LG.TDSDesc  " &
                            "Where LG.DocId='" & AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "' And IfNull(LG.System_Generated,'N')='Y' And LG.TDS_Of_V_SNo=" & AgL.VNull(DTTemp.Rows(I).Item("V_SNo")) & "  Order By V_SNo", AgL.GCn).Tables(0)

                        If DTTemp1.Rows.Count > 0 Then
                            FGMain(GTDS_Btn, I).Style.BackColor = Color.LavenderBlush
                            ReDim SVTMain.TDSVar(DTTemp1.Rows.Count - 1)
                        End If
                        For J = 0 To DTTemp1.Rows.Count - 1
                            SVTMain.TDSVar(J).StrDescCode = AgL.XNull(DTTemp1.Rows(J).Item("TDSDesc"))
                            SVTMain.TDSVar(J).StrDesc = AgL.XNull(DTTemp1.Rows(J).Item("DName"))
                            SVTMain.TDSVar(J).StrPostingAcCode = AgL.XNull(DTTemp1.Rows(J).Item("SubCode"))
                            SVTMain.TDSVar(J).StrPostingAc = AgL.XNull(DTTemp1.Rows(J).Item("AcName"))
                            SVTMain.TDSVar(J).DblPercentage = Format(AgL.VNull(DTTemp1.Rows(J).Item("TDSPer")), "0.000")
                            If AgL.VNull(DTTemp1.Rows(J).Item("AmtDr")) > 0 Then
                                SVTMain.TDSVar(J).DblAmount = Format(AgL.VNull(DTTemp1.Rows(J).Item("AmtDr")), "0.00")
                            Else
                                SVTMain.TDSVar(J).DblAmount = Format(AgL.VNull(DTTemp1.Rows(J).Item("AmtCr")), "0.00")
                            End If
                            SVTMain.TDSVar(J).StrFormula = AgL.XNull(DTTemp1.Rows(J).Item("FormulaString"))
                        Next
                    End If
                    DTTemp1 = CMain.FGetDatTable("Select LA.Vr_DocId From LedgerAdj LA Where LA.Vr_DocId='" & AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "' And LA.Vr_V_SNo=" & AgL.VNull(DTTemp.Rows(I).Item("V_SNo")) & " ", AgL.GCn)
                    If DTTemp1.Rows.Count > 0 Then
                        FGMain(GAdj_Btn, I).Style.BackColor = Color.LavenderBlush
                        'FFillLedgerAdj(I, Agl.VNull(DTTemp.Rows(I).Item("V_SNo")))
                        FMovRecLedgerAdj(I, AgL.VNull(DTTemp.Rows(I).Item("V_SNo")))
                    End If



                    ''For Item Adjustment
                    'DTTemp1.Clear()
                    'DTTemp1 = cmain.FGetDatTable("Select LIA.ItemCode,LIA.Quantity,LIA.Amount,IM.Name As IName, " & _
                    '    "IM.SKU,LIA.Remark " & _
                    '    "From LedgerItemAdj LIA " & _
                    '    "Left Join ItemMast IM On LIA.ItemCode=IM.Code " & _
                    '    "Where LIA.DocId='" & Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "' And LIA.V_SNo=" & Agl.VNull(DTTemp.Rows(I).Item("V_SNo")) & "  Order By IM.Name", Agl.Gcn)
                    'If DTTemp1.Rows.Count > 0 Then
                    '    FGMain(GIAdj_Btn, I).Style.BackColor = Color.LavenderBlush
                    '    ReDim SVTMain.LIAdjVar(DTTemp1.Rows.Count - 1)
                    'End If
                    'For J = 0 To DTTemp1.Rows.Count - 1
                    '    SVTMain.LIAdjVar(J).StrItemCode = Agl.Xnull(DTTemp1.Rows(J).Item("ItemCode"))
                    '    SVTMain.LIAdjVar(J).StrItemName = Agl.Xnull(DTTemp1.Rows(J).Item("IName"))
                    '    SVTMain.LIAdjVar(J).StrRemark = Agl.Xnull(DTTemp1.Rows(J).Item("Remark"))
                    '    SVTMain.LIAdjVar(J).StrUnit = Agl.Xnull(DTTemp1.Rows(J).Item("SKU"))
                    '    SVTMain.LIAdjVar(J).DblQuantity = Format(Agl.VNull(DTTemp1.Rows(J).Item("Quantity")), "0.000")
                    '    SVTMain.LIAdjVar(J).DblAmount = Format(Agl.VNull(DTTemp1.Rows(J).Item("Amount")), "0.00")
                    'Next

                    'FUpdateRowStructure(SVTMain, I)
                Next

            End If
        End If
        FUpdateRowStructure(New ClsStructure.VoucherType, FGMain.Rows.Count - 1)
        Topctrl1.FSetDispRec(BMBMaster)

        DTTemp = Nothing
        DTTemp1 = Nothing
        FCalculate()
    End Sub

    Public Sub FPasteRecord(ByVal StrCopyDocIdVar As String)
        Dim DTTemp As New DataTable, StrCondition As String = ""
        Dim DTTemp1 As New DataTable
        Dim I As Integer, J As Int16
        Dim StrSQL As String

        FClear()
        Topctrl1.BlankTextBoxes()
        StrSQL = ("Select LM.DocId,LM.V_Type,LM.v_Prefix,LM.Site_Code,LM.V_No,LM.V_Date,LM.SubCode,  " &
                "SG.Name As AcName,LM.PostedBy,LM.RecId, " &
                "LM.Narration,LM.U_Name,LM.PreparedBy, " &
                "VT.Description,VT.Category " &
                "From LedgerM LM Left Join SubGroup SG On LM.SubCode=SG.SubCode Left Join " &
                "Voucher_Type VT On VT.V_Type=LM.V_Type " &
                "Where LM.DocId='" & StrCopyDocIdVar & "'")

        DTTemp = CMain.FGetDatTable(StrSQL, AgL.GCn)
        If DTTemp.Rows.Count > 0 Then
            FManageScreen(AgL.XNull(DTTemp.Rows(0).Item("Category")))

            TxtType.Text = AgL.XNull(DTTemp.Rows(0).Item("Description"))
            TxtType.Tag = AgL.XNull(DTTemp.Rows(0).Item("V_Type"))

            TxtVDate.Text = AgL.XNull(DTTemp.Rows(0).Item("V_Date"))
            TxtAcName.Text = AgL.XNull(DTTemp.Rows(0).Item("AcName"))
            TxtAcName.Tag = AgL.XNull(DTTemp.Rows(0).Item("SubCode"))
            TxtNarration.Text = AgL.XNull(DTTemp.Rows(0).Item("Narration"))

            StrCondition = ""
            If UCase(Trim(LblCurrentType.Tag)) = "PMT" Then
                StrCondition = " And IfNull(AmtDr,0)>0 "
            ElseIf UCase(Trim(LblCurrentType.Tag)) = "RCT" Then
                StrCondition = " And IfNull(AmtCr,0)>0 "
            End If
            DTTemp.Clear()

            StrSQL = ("Select LG.SubCode,SG.Name As AcName,SG.ManualCode,LG.V_SNo,CCM.Name As CCName,LG.CostCenter, " &
                "LG.AmtDr,LG.AmtCr,LG.Narration,LG.Chq_No,LG.Chq_Date,LG.TDSOnAmt,LG.TDSCategory,TC.Name As TDSCName,LG.OrignalAmt,LG.TDSDeductFrom,TDF.Name As TDFName " &
                "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode Left Join " &
                "TDSCat TC On TC.Code=LG.TDSCategory Left Join CostCenterMast CCM On CCM.Code=LG.CostCenter " &
                "Left Join SubGroup TDF On TDF.SubCode=LG.TDSDeductFrom " &
                "Where LG.DocId='" & StrCopyDocIdVar & "' And IfNull(LG.System_Generated,'N')='N' " & StrCondition & " ")
            DTTemp = CMain.FGetDatTable(StrSQL, AgL.GCn)
            If DTTemp.Rows.Count > 0 Then
                FGMain.Rows.Add(DTTemp.Rows.Count)
            End If
            For I = 0 To DTTemp.Rows.Count - 1
                FUpdateRowStructure(New ClsStructure.VoucherType, I)
                FGMain(GSNo, I).Value = Trim(I + 1)

                FGMain(GAcCode, I).Value = AgL.XNull(DTTemp.Rows(I).Item("SubCode"))
                FGMain(GAcName, I).Value = AgL.XNull(DTTemp.Rows(I).Item("AcName"))
                FGMain(GAcManaulCode, I).Value = AgL.XNull(DTTemp.Rows(I).Item("ManualCode"))
                FGMain(GNarration, I).Value = AgL.XNull(DTTemp.Rows(I).Item("Narration"))
                FGMain(GDebit, I).Value = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0, Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00"), "")
                FGMain(GCredit, I).Value = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0, Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00"), "")

                FGMain(GCostCenter, I).Value = AgL.XNull(DTTemp.Rows(I).Item("CCName"))
                FGMain(GCostCenterCode, I).Value = AgL.XNull(DTTemp.Rows(I).Item("CostCenter"))

                FGMain(GTDSCategory, I).Value = AgL.XNull(DTTemp.Rows(I).Item("TDSCName"))
                FGMain(GTDSCategoryCode, I).Value = AgL.XNull(DTTemp.Rows(I).Item("TDSCategory"))

                FGMain(GTDSDeductFrom, I).Value = AgL.XNull(DTTemp.Rows(I).Item("TDSDeductFrom"))
                FGMain(GTDSDeductFromName, I).Value = AgL.XNull(DTTemp.Rows(I).Item("TDFName"))

                FGMain(GTDSOnAmount, I).Value = AgL.XNull(DTTemp.Rows(I).Item("TDSOnAmt"))
                FGMain(GOrignalAmt, I).Value = IIf(AgL.VNull(DTTemp.Rows(I).Item("OrignalAmt")) > 0, AgL.VNull(DTTemp.Rows(I).Item("OrignalAmt")), AgL.VNull(DTTemp.Rows(I).Item("TDSOnAmt")))

                SVTMain = DTStruct.Rows(I).Item("SSDB")

                If Trim(FGMain(GTDSCategoryCode, I).Value) <> "" Then
                    'For TDS
                    DTTemp1.Clear()
                    StrSQL = ("Select LG.FormulaString,LG.SubCode,SG.Name As AcName,SG.ManualCode, " &
                        "LG.AmtDr,LG.AmtCr,LG.TDSDesc,TCD.Name As DName,LG.TDSPer " &
                        "From Ledger LG Left Join SubGroup SG On LG.SubCode=SG.SubCode Left Join " &
                        "TDSCat_Description TCD On TCD.Code=LG.TDSDesc  " &
                        "Where LG.DocId='" & StrCopyDocId & "' And IfNull(LG.System_Generated,'N')='Y' And LG.TDS_Of_V_SNo=" & AgL.VNull(DTTemp.Rows(I).Item("V_SNo")) & "  Order By V_SNo")
                    DTTemp1 = CMain.FGetDatTable(StrSQL, AgL.GCn)
                    If DTTemp1.Rows.Count > 0 Then
                        FGMain(GTDS_Btn, I).Style.BackColor = Color.LavenderBlush
                        ReDim SVTMain.TDSVar(DTTemp1.Rows.Count - 1)
                    End If
                    For J = 0 To DTTemp1.Rows.Count - 1
                        SVTMain.TDSVar(J).StrDescCode = AgL.XNull(DTTemp1.Rows(J).Item("TDSDesc"))
                        SVTMain.TDSVar(J).StrDesc = AgL.XNull(DTTemp1.Rows(J).Item("DName"))
                        SVTMain.TDSVar(J).StrPostingAcCode = AgL.XNull(DTTemp1.Rows(J).Item("SubCode"))
                        SVTMain.TDSVar(J).StrPostingAc = AgL.XNull(DTTemp1.Rows(J).Item("AcName"))
                        SVTMain.TDSVar(J).DblPercentage = Format(AgL.VNull(DTTemp1.Rows(J).Item("TDSPer")), "0.000")
                        If AgL.VNull(DTTemp1.Rows(J).Item("AmtDr")) > 0 Then
                            SVTMain.TDSVar(J).DblAmount = Format(AgL.VNull(DTTemp1.Rows(J).Item("AmtDr")), "0.00")
                        Else
                            SVTMain.TDSVar(J).DblAmount = Format(AgL.VNull(DTTemp1.Rows(J).Item("AmtCr")), "0.00")
                        End If
                        SVTMain.TDSVar(J).StrFormula = AgL.XNull(DTTemp1.Rows(J).Item("FormulaString"))
                    Next
                End If

                FUpdateRowStructure(SVTMain, I)
            Next
        Else
            MsgBox("Copied Record Does Not Exists.")
        End If
        FUpdateRowStructure(New ClsStructure.VoucherType, FGMain.Rows.Count - 1)
        DTTemp = Nothing
        FCalculate()
    End Sub

    Public Sub FTxtGotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        '======== Write Your Code Below =============
    End Sub

    Public Sub FTxtKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.Name
            Case TxtAcName.Name, TxtType.Name
                If e.KeyCode = Keys.Delete Then
                    sender.Text = "" : sender.Tag = ""
                End If
        End Select
    End Sub

    Public Sub FTxtKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.Name
            Case TxtAcName.Name
                FHP_Customer(e, sender)
            Case TxtType.Name
                FHP_Type(e, sender)
                FManageScreen(LblCurrentType.Tag, False)
        End Select
        'TxtType.ContextMenu.Dispose()
    End Sub

    Private Sub FHP_Customer(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String, StrPrvText As String

        StrPrvText = Txt.Text
        StrSendText = CMain.FSendText(Txt, e.KeyChar)

        Dim mQry$ = "Select SG.SubCode,SG.Name,IfNull(CT.CityName,''),SG.ManualCode From SubGroup SG Left Join City CT On CT.CityCode=SG.CityCode Where " & AgL.PubSiteConditionCommonAc(AgL.PubIsHo, "SITE_CODE", AgL.PubSiteCode, "COMMONAC") & " And SG.Nature In (Select Nature From AcFilteration Where V_Type='" & TxtType.Tag & "') Order by SG.Name"
        DTMain = AgL.FillData("Select SG.SubCode,SG.Name,IfNull(CT.CityName,''),SG.ManualCode From SubGroup SG Left Join City CT On CT.CityCode=SG.CityCode Where " & AgL.PubSiteConditionCommonAc(AgL.PubIsHo, "SITE_CODE", AgL.PubSiteCode, "COMMONAC") & " And SG.Nature In (Select Nature From AcFilteration Where V_Type='" & TxtType.Tag & "') Order by SG.Name", AgL.GCn).Tables(0)


        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 300, 480, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(2, "City", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(3, "Code", 100, DataGridViewContentAlignment.MiddleLeft)

        FRH.ShowDialog()
        Txt.Text = StrPrvText
        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(1)
                Txt.Tag = FRH.DRReturn.Item(0)
                FShowLedgerBalance(Txt.Tag)
            End If

        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub

    Private Sub FShowLedgerBalance(ByVal StrSubCode As String)
        LblBalance.Text = FGetLedgerBalance(StrSubCode)
        LblBalance.ForeColor = IIf(Val(LblBalance.Text) > 0, Color.MediumBlue, Color.Maroon)
        LblBalance.Text = IIf(Val(LblBalance.Text) > 0, "Balance Dr " & Format(Math.Abs(Val(LblBalance.Text)), "0.00"), "Balance Cr " & Format(Math.Abs(Val(LblBalance.Text)), "0.00"))
    End Sub

    Private Sub FHP_Type(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String
        Dim StrSQL As String

        StrSQL = "Select VT.V_Type,VT.Description,VT.DefaultAc,SG.Name As DAName "
        StrSQL += "From Voucher_Type VT "
        StrSQL += "Left Join SubGroup SG On VT.DefaultAc=SG.SubCode "
        StrSQL += "Where VT.NCat='FA' And VT.Category='" & LblCurrentType.Tag & "'  "
        StrSQL += "Order By VT.Description "
        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        DTMain = AgL.FillData(StrSQL, AgL.GCn).Tables(0)

        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 200, 280, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(2, , 0, , False)
        FRH.FFormatColumn(3, , 0, , False)
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(1)
                Txt.Tag = FRH.DRReturn.Item(0)
                TxtAcName.Text = ""
                TxtAcName.Tag = ""
                If UCase(LblCurrentType.Tag) <> "JV" Then
                    TxtAcName.Text = AgL.XNull(FRH.DRReturn.Item("DAName"))
                    TxtAcName.Tag = AgL.XNull(FRH.DRReturn.Item("DefaultAc"))
                    FShowLedgerBalance(TxtAcName.Tag)
                Else
                    StrDefaultAcCode = AgL.XNull(FRH.DRReturn.Item("DefaultAc"))
                    StrDefaultAcName = AgL.XNull(FRH.DRReturn.Item("DAName"))
                End If
                FGenerateNo()
            End If
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub

    Private Sub FGenerateNo(Optional ByVal BlnGenerateOnlyNo As Boolean = False)
        If Trim(TxtType.Text) = "" Then Exit Sub
        If Not BlnGenerateOnlyNo Then If Topctrl1.Mode <> "Add" Then Exit Sub

        If UCase(Trim(TxtType.Tag)) = "OB" Then TxtVDate.Text = DateAdd(DateInterval.Day, -1, CDate(AgL.PubStartDate))

        If RFNumberSystem = ClsMain.RecIdFormat.DD_MM Then
            'TxtRecId.Text = CMain.FGetRecId(TxtVDate.Text, "LedgerM", "RecId", "V_Date", TxtType.Tag, True, ClsMain.RecIdFormat.DD_MM)
            TxtRecId.Text = ClsMain.FGetManualRefNo("RecId", "LedgerM", TxtType.Tag, TxtVDate.Text, AgL.PubDivCode, AgL.PubSiteCode, 0)
        ElseIf RFNumberSystem = ClsMain.RecIdFormat.MM Then
            'TxtRecId.Text = CMain.FGetRecId(TxtVDate.Text, "LedgerM", "RecId", "V_Date", TxtType.Tag, True, ClsMain.RecIdFormat.MM)
            TxtRecId.Text = ClsMain.FGetManualRefNo("RecId", "LedgerM", TxtType.Tag, TxtVDate.Text, AgL.PubDivCode, AgL.PubSiteCode, 0)
        End If

        If UCase(Trim(TxtType.Tag)) = "OB" Then TxtVDate.Text = DateAdd(DateInterval.Day, -1, CDate(AgL.PubStartDate))
        If Not BlnGenerateOnlyNo Then
            'If Trim(TxtVNo.Text) = "" Then StrDocID = CMain.FGetDoId(TxtVNo, TxtType.Tag, "LedgerM", "V_No", TxtVDate.Text)
            If Trim(TxtVNo.Text) = "" Then
                StrDocID = AgL.GetDocId(TxtType.Tag, CStr(TxtVNo.Text), CDate(TxtVDate.Text), AgL.GcnRead, AgL.PubDivCode, AgL.PubSiteCode)
                TxtVNo.Tag = Val(AgL.DeCodeDocID(StrDocID, AgLibrary.ClsMain.DocIdPart.VoucherPrefix))
                TxtVNo.Text = Val(AgL.DeCodeDocID(StrDocID, AgLibrary.ClsMain.DocIdPart.VoucherNo))
            End If

        End If




        If RFNumberSystem <> ClsMain.RecIdFormat.DD_MM And RFNumberSystem <> ClsMain.RecIdFormat.MM Then
            'TxtRecId.Text = CMain.FGetMaxNo("Select IfNull(Max(Cast(LM.RecId as Integer)),0)+1 As Mx From LedgerM LM Where (Case When LM.RecId  GLOB '*[0-9]*' Then LM.RecId  else 0 End)<>0 And LM.V_Prefix='" & TxtVNo.Tag & "' And LM.V_Type='" & TxtType.Tag & "' And LM.Site_Code='" & AgL.PubSiteCode & "' ", AgL.GCn)
            TxtRecId.Text = ClsMain.FGetManualRefNo("RecId", "LedgerM", TxtType.Tag, TxtVDate.Text, AgL.PubDivCode, AgL.PubSiteCode, 0)
        End If

        If UCase(Trim(TxtType.Tag)) = "OB" Then TxtVDate.Text = DateAdd(DateInterval.Day, -1, CDate(AgL.PubStartDate))
    End Sub
    Private Function FCheckGenerateNo(ByVal StrCurrentDocIdVar As String) As Boolean
        Dim StrFormat_1StHalf As String = "", StrFormat_2ndHalf As String = ""
        Dim DblDay As Int16 = 0, DblMonth As Int16 = 0
        Dim BlnRtn As Boolean = True

        DblDay = DatePart(DateInterval.Day, CDate(TxtVDate.Text))
        DblMonth = DatePart(DateInterval.Month, CDate(TxtVDate.Text))

        If RFNumberSystem = ClsMain.RecIdFormat.DD_MM Then
            StrFormat_1StHalf = Format(DblDay, "00")
            StrFormat_1StHalf += Format(DblMonth, "00")
            StrFormat_2ndHalf = "0000"
        ElseIf RFNumberSystem = ClsMain.RecIdFormat.MM Then
            StrFormat_1StHalf = Format(DblMonth, "00")
            StrFormat_2ndHalf = "000000"
        End If

        If RFNumberSystem = ClsMain.RecIdFormat.DD_MM Or RFNumberSystem = ClsMain.RecIdFormat.MM Then
            If StrFormat_1StHalf <> Mid(TxtRecId.Text, 1, Len(StrFormat_1StHalf)) Then
                BlnRtn = False
            End If

            If Len(StrFormat_1StHalf & StrFormat_2ndHalf) <> Len(TxtRecId.Text) Then
                BlnRtn = False
            End If

            If Not BlnRtn Then MsgBox("Please Check Voucher No. Format.") : TxtRecId.Focus()
        End If

        FCheckGenerateNo = BlnRtn
    End Function
    Private Sub FClear()
        FGMain.Rows.Clear()
        DTStruct.Clear()
        LblBalance.Text = ""
        StrCompareRecIdTemp = ""
        StrCompareDateTemp = ""
    End Sub
    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        FClear()
        FManageDisplay(False)
        BtnImport.Enabled = True
        BtnJournal.Enabled = True
        BtnPayments.Enabled = True
        BtnReceipt.Enabled = True
        BtnPaste.Visible = True

        FUpdateRowStructure(New ClsStructure.VoucherType, 0)
        TxtPrepared.Text = AgL.PubUserName
        TxtVDate.Text = AgL.PubLoginDate
        If StrCurrentType <> "" Then
            FManageScreen(StrCurrentType)
        Else
            FManageScreen(LblCurrentType.Tag)
        End If


        If StrCurrentType = LblCurrentType.Tag Then
            TxtType.Text = StrTypeTemp
            TxtType.Tag = StrTypeTagTemp
            TxtAcName.Text = StrAcTemp
            TxtAcName.Tag = StrAcTagTemp
            FShowLedgerBalance(TxtAcName.Tag)
            TxtVDate.Text = StrDateTemp
            'FGMain(GAcManaulCode, 0).Selected = True
            FGMain(GSNo, 0).Selected = True
            FGMain.Focus()
        Else
            TxtType.Focus()
        End If

        FGenerateNo()
    End Sub
    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim I As Integer
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New Object
        Dim DTTemp As DataTable

        Try
            If DTMaster.Rows.Count > 0 Then
                StrDocID = ""
                StrDocID = AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))


                If AgL.Dman_Execute("Select Count(*) From Ledger Where DocID = '" & StrDocID & "' And ReferenceDocID Is Not Null", AgL.GCn).ExecuteScalar() > 0 Then
                    MsgBox("Referenced with any other entry. Can not delete")
                    Topctrl1.FButtonClick(14, True)
                    Exit Sub
                End If

                If AgL.PubDivCode <> AgL.XNull(AgL.Dman_Execute("Select Div_Code From LedgerM Where DocId = '" & StrDocID & "'", AgL.GCn).ExecuteScalar()) Then
                    MsgBox("Cant't Delete Other Division Record...!", MsgBoxStyle.Information)
                    Topctrl1.FButtonClick(14, True)
                    Exit Sub
                End If

                If AgL.PubSiteCode <> AgL.XNull(AgL.Dman_Execute("Select Site_Code From LedgerM Where DocId = '" & StrDocID & "'", AgL.GCn).ExecuteScalar()) Then
                    MsgBox("Cant't Delete Other Site Record...!", MsgBoxStyle.Information)
                    Topctrl1.FButtonClick(14, True)
                    Exit Sub
                End If

                For I = 0 To FGMain.Rows.Count - 1
                    If FGMain.Item(GClgDate, I).Value <> "" Then
                        MsgBox("Row No. " & FGMain.Item(GSNo, I).Value.ToString & " is reconciled. Can't delete entry.")
                        Topctrl1.FButtonClick(14, True)
                        Exit Sub
                    End If
                Next


                If MsgBox(" Are You Sure To Delete ? ", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = vbYes Then
                    If Trim(Replace(StrDocID, "0", "")) = "" Then MsgBox(" Invalid " & "DocId.") : Exit Sub
                    If CMain.FGetMaxNo("Select Count(*) Cnt From DataAudit Where DocId='" & StrDocID & "' ", AgL.GCn) > 0 Then MsgBox("Record Has Been Audited. You Can Not Edit/ Delete This Record.") : Exit Sub
                    'If Not CMain.FGetMaxNo("Select Count(DocId) From LedgerM Where DocId='" & StrDocID & "' And IfNull(PostedBy,'')='' ", AgL.GCn) > 0 Then MsgBox("Corresponding Records Exist") : Exit Sub
                    DTTemp = CMain.FGetDatTable("Select LG.RecId,LG.V_Prefix,LG.V_Type From LedgerAdj LA Left Join Ledger LG On LA.Vr_DocId=LG.DocId Where LA.Adj_DocId='" & StrDocID & "' ", AgL.GCn)
                    If DTTemp.Rows.Count > 0 Then
                        MsgBox("This Record Has Been Adjusted Bill Wise (" & AgL.XNull(DTTemp.Rows(0).Item("RecId")) & "/" & AgL.XNull(DTTemp.Rows(0).Item("V_Prefix")) & "/" & AgL.XNull(DTTemp.Rows(0).Item("V_Type")) & "). " & "Corresponding Records Exist")
                        DTTemp.Dispose()
                        DTTemp = Nothing
                        Exit Sub
                    End If
                    DTTemp.Dispose()
                    DTTemp = Nothing

                    BlnTrans = True
                    GCnCmd = AgL.GCn.CreateCommand
                    GCnCmd.Transaction = AgL.GCn.BeginTransaction(IsolationLevel.Serializable)

                    GCnCmd.CommandText = "Delete From TransactionReferences Where DocId='" & (StrDocID) & "'"
                    GCnCmd.ExecuteNonQuery()
                    GCnCmd.CommandText = " Delete From SchemeQulified Where GeneratedDocId = '" & (StrDocID) & "'"
                    GCnCmd.ExecuteNonQuery()
                    GCnCmd.CommandText = "Delete From Stock Where DocId='" & (StrDocID) & "'"
                    GCnCmd.ExecuteNonQuery()
                    GCnCmd.CommandText = "Delete From LedgerItemAdj Where DocId='" & (StrDocID) & "'"
                    GCnCmd.ExecuteNonQuery()
                    'GCnCmd.CommandText = "Delete From DataTrfd Where DocId='" & (StrDocID) & "'"
                    'GCnCmd.ExecuteNonQuery()
                    GCnCmd.CommandText = "Delete From LedgerAdj Where Vr_DocId='" & (StrDocID) & "'"
                    GCnCmd.ExecuteNonQuery()
                    GCnCmd.CommandText = "Delete From Ledger Where DocId='" & (StrDocID) & "'"
                    GCnCmd.ExecuteNonQuery()
                    GCnCmd.CommandText = "Delete From LedgerM Where DocId='" & (StrDocID) & "'"
                    GCnCmd.ExecuteNonQuery()

                    GCnCmd.Transaction.Commit()
                    BlnTrans = False
                    FIniMaster(1)
                    MoveRec()
                End If
            End If
        Catch Ex As Exception
            If BlnTrans = True Then GCnCmd.Transaction.Rollback()
            If Err.Number = 5 Then    'foreign key - there exists related record in primary key table
                MsgBox("Corresponding Records Exist")
            Else
                MsgBox(Ex.Message)
            End If
        End Try
    End Sub
    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        Dim I As Integer
        Dim DTTemp As DataTable

        If DTMaster.Rows.Count > 0 Then
            StrDocID = AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))


            If AgL.Dman_Execute("Select Count(*) From Ledger Where DocID = '" & StrDocID & "' And ReferenceDocID Is Not Null", AgL.GCn).ExecuteScalar() > 0 Then
                MsgBox("Referenced with any other entry. Can not edit")
                Topctrl1.FButtonClick(14, True)
                Exit Sub
            End If


            If AgL.PubDivCode <> AgL.XNull(AgL.Dman_Execute("Select Div_Code From LedgerM Where DocId = '" & StrDocID & "'", AgL.GCn).ExecuteScalar()) Then
                    MsgBox("Cant't Edit Other Division Record...!", MsgBoxStyle.Information)
                    Topctrl1.FButtonClick(14, True)
                    Exit Sub
                End If

                If AgL.PubSiteCode <> AgL.XNull(AgL.Dman_Execute("Select Site_Code From LedgerM Where DocId = '" & StrDocID & "'", AgL.GCn).ExecuteScalar()) Then
                    MsgBox("Cant't Edit Other Site Record...!", MsgBoxStyle.Information)
                    Topctrl1.FButtonClick(14, True)
                    Exit Sub
                End If


                For I = 0 To FGMain.Rows.Count - 1
                    If FGMain.Item(GClgDate, I).Value <> "" Then
                        MsgBox("Row No. " & FGMain.Item(GSNo, I).Value.ToString & " is reconciled. Can't modify entry.")
                        Topctrl1.FButtonClick(14, True)
                        Exit Sub
                    End If
                Next

                If CMain.FGetMaxNo("Select Count(*) Cnt From DataAudit Where DocId='" & StrDocID & "' ", AgL.GCn) > 0 Then MsgBox("Record Has Been Audited. You Can Not Edit/ Delete This Record.") : Topctrl1.FButtonClick(99) : Exit Sub
                'If Not CMain.FGetMaxNo("Select Count(DocId) From LedgerM Where DocId='" & AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "' And IfNull(PostedBy,'')='' ", AgL.GCn) > 0 Then MsgBox(ClsMain.MsgEditChk) : Topctrl1.FButtonClick(99) : Exit Sub
                DTTemp = CMain.FGetDatTable("Select LG.RecId,LG.V_Prefix,LG.V_Type From LedgerAdj LA Left Join Ledger LG On LA.Vr_DocId=LG.DocId Where LA.Adj_DocId='" & AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "' ", AgL.GCn)
                If DTTemp.Rows.Count > 0 Then
                    MsgBox("This Record Has Been Adjusted Bill Wise (" & AgL.XNull(DTTemp.Rows(0).Item("RecId")) & "/" & AgL.XNull(DTTemp.Rows(0).Item("V_Prefix")) & "/" & AgL.XNull(DTTemp.Rows(0).Item("V_Type")) & "). " & ClsMain.MsgEditChk)
                    DTTemp.Dispose()
                    DTTemp = Nothing
                    Topctrl1.FButtonClick(99)
                    Exit Sub
                End If
                DTTemp.Dispose()
                DTTemp = Nothing

                FManageDisplay(False)
                TxtRecId.Enabled = True
                BtnImport.Enabled = True
                BtnRefreshVNo.Visible = True
                TxtType.Enabled = False
                FManageScreen(LblCurrentType.Tag, False)
                If TxtAcName.Enabled Then
                    TxtAcName.Focus()
                Else
                    FGMain.Focus()
                End If
            End If
    End Sub
    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox(ClsMain.MsgRecNotFnd) : Exit Sub
        Try
            'AgL.PubFindQry = "Select LM.DocId,RTrim(LM.RecId) As VNo,LM.V_Prefix As Prefix,LM.V_Type,VT.Description,Cast(LM.V_Date as Date Format 'DD-MMM-YY') As VDate,IfNull(SG.Name,'') As Account,IfNull(LM.PostedBy,'') As PostedBy " &
            '                      "From LedgerM LM Left Join Voucher_Type VT On LM.V_Type=VT.V_Type Left Join SubGroup SG On LM.SubCode=SG.SubCode Where LM.Site_Code='" & AgL.PubSiteCode & "' And LM.Div_Code='" & AgL.PubDivCode & "' And LM.V_Prefix='" & AgL.PubCompVPrefix & "' "
            AgL.PubFindQry = "Select LM.DocId,RTrim(LM.RecId) As VNo,LM.V_Prefix As Prefix,LM.V_Type,VT.Description,
                            strftime('%d-%m-%Y',LM.V_Date) As VDate,IfNull(SG.Name,'') As Account, L.AmtDr, L.AmtCr,IfNull(LM.PostedBy,'') As PostedBy 
                            From LedgerM LM
                            Left Join Ledger L On LM.DocID = L.DocID 
                            Left Join Voucher_Type VT On LM.V_Type=VT.V_Type 
                            Left Join viewHelpSubGroup SG On L.SubCode=SG.Code
                            Where LM.Site_Code='" & AgL.PubSiteCode & "' And LM.Div_Code='" & AgL.PubDivCode & "' And LM.V_Prefix='" & AgL.PubCompVPrefix & "' Or LM.V_Type = 'OB' "
            AgL.PubFindQryOrdBy = "VNo"
            'LIPublic.CreateAndSendArr("100,100,100,200,100,200,100")

            '*************** common code start *****************
            Dim Frmbj As AgLibrary.FrmFind = New AgLibrary.FrmFind(AgL.PubFindQry, Me.Text & " Find", AgL)
            Frmbj.ShowDialog()
            AgL.PubSearchRow = Frmbj.DGL1.Item(0, Frmbj.DGL1.CurrentRow.Index).Value.ToString

            FSearchRecord(AgL.PubSearchRow)
            '*************** common code end  *****************
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub
    Public Sub FSearchRecord(ByVal StrKeyField As String)
        Try
            If StrKeyField <> "" Then
                CMain.DRFound = DTMaster.Rows.Find(StrKeyField)
                BMBMaster.Position = DTMaster.Rows.IndexOf(CMain.DRFound)
                MoveRec()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Topctrl1_tbSave() Handles Topctrl1.tbSave

        Try
            '================================================================================
            '================== Write Your Validations In FSaveValidation() =================
            If Not FSaveValidation() Then Exit Sub
            '================================================================================
            If UCase(Trim(TxtType.Tag)) <> "OB" Then
                If Not CMain.FChkDate_FinancialYear(TxtVDate.Text) Then Exit Sub

            End If

            StrDocID = ""
            If Topctrl1.Mode = "Add" Then
                TxtVNo.Text = ""
                FGenerateNo()
            Else
                StrDocID = AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
                If Not FCheckGenerateNo(StrDocID) Then Exit Sub
            End If
            If Trim(Replace(StrDocID, "0", "")) = "" Then MsgBox(" Invalid " & "DocId.") : Exit Sub

            StrCurrentType = LblCurrentType.Tag
            StrTypeTemp = TxtType.Text
            StrTypeTagTemp = TxtType.Tag
            StrAcTemp = TxtAcName.Text
            StrAcTagTemp = TxtAcName.Tag
            StrDateTemp = TxtVDate.Text

            '================================================================================
            '====================== Write Your Save Code In FSave() =========================
            If Not FSave(Topctrl1.Mode, StrDocID, "LedgerM", "Ledger", "LedgerAdj", ClsStructure.EntryType.ForPosting) Then Exit Sub
            '================================================================================

            'If MsgBox("Do You Want To Print?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            '    FPrintGlobal(StrDocID, TxtType.Tag, "", Me, TxtVNo.Tag, True)
            'End If

            If Topctrl1.Mode = "Add" Then
                Topctrl1.LblDocId.Text = StrDocID
                Topctrl1.FButtonClick(0)
                Exit Sub
            Else
                Topctrl1.SetDisp(True)
                MoveRec()
            End If

        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub
    Private Function FSaveValidation() As Boolean
        Dim BlnRtn As Boolean = True

        Try
            If AgL.RequiredField(TxtType, "Type") Then Return False
            If AgL.RequiredField(TxtVDate, "Date") Then Return False

            If CDate(TxtVDate.Text) >= "01-Apr-2020" Then
                MsgBox("Entry point is not valid from 01-Apr-2020.")
                Return False
            End If

            If UCase(Trim(LblCurrentType.Tag)) <> "JV" Then
                If AgL.RequiredField(TxtAcName, LblAcName.Text) Then Return False
            End If
            FCalculate()
            If Not FCkhGrid() Then Return False
            If Not FChkNegativeCash() Then Return False




        Catch ex As Exception
            BlnRtn = False
            MsgBox(ex.Message)
        End Try

        Return BlnRtn
    End Function
    Private Function FChkNegativeCash() As Boolean
        Dim BlnRtn As Boolean
        Dim StrCurrentDocId As String
        Dim StrSQL As String
        Dim DTTemp As DataTable
        BlnRtn = True

        If Trim(UCase(LblCurrentType.Tag)) = "PMT" Then
            StrCurrentDocId = ""
            If DTMaster.Rows.Count > 0 Then StrCurrentDocId = AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
            StrSQL = "Select IfNull(Sum(LG.AmtDr),0)- (IfNull(Sum(LG.AmtCr),0) + " & Val(LblCrAmt.Text) & ")  As Bal "
            StrSQL += "From Ledger LG "
            StrSQL += "Where LG.SubCode='" & TxtAcName.Tag & "' And "
            StrSQL += "LG.DocId <> '" & StrDocID & "' "
            DTTemp = CMain.FGetDatTable(StrSQL, AgL.GCn)
            If AgL.XNull(DTTemp.Rows(0).Item("Bal")) < 0 Then
                If MsgBox("Account Name : " & TxtAcName.Text & vbCrLf & "Negative Balance." & vbCrLf & vbCrLf & "Do You Want To Continue?") = MsgBoxResult.No Then
                    BlnRtn = False
                End If
            End If
            DTTemp.Dispose()
        End If
        FChkNegativeCash = BlnRtn
    End Function
    Private Function FSave(ByVal StrMode As String, ByVal StrDocId As String, ByVal StrLedgerM As String,
    ByVal StrLedger As String, ByVal StrLedgerAdj As String, ByVal BytFormWorkAs As ClsStructure.EntryType) As Boolean

        Dim BlnRtn As Boolean = True
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New Object
        Dim I As Short, IntV_SNo As Integer, IntV_SNo_For_Stock As Integer, Int_Prv_V_SNo As Integer, J As Int16
        Dim DblCredit As Double, DblDebit As Double, DblCredit_Total As Double, DblDebit_Total As Double
        Dim Narration As String = "", BlnFlag As Boolean = False
        Dim StrNarrationForHeader As String = ""
        Dim StrContraTextJV As String = "", StrContraTextOther As String = "", StrContraTDS_BF As String = "", StrContraTDS As String = ""
        Dim StrChequeNo As String = "", StrChequeDt As String = ""

        Try
            '================================================
            '================= For JV =======================
            If UCase(Trim(LblCurrentType.Tag)) = "JV" And UCase(Trim(TxtType.Tag)) <> "OB" Then
                For I = 0 To FGMain.Rows.Count - 1
                    If Trim(FGMain(GAcName, I).Value) <> "" Then
                        If StrContraTextJV <> "" Then StrContraTextJV += vbCrLf
                        If Val(FGMain(GDebit, I).Value) > 0 Then
                            FPrepareContraText(False, StrContraTextJV, FGMain(GAcName, I).Value, FGMain(GDebit, I).Value, "Dr")
                        Else
                            FPrepareContraText(False, StrContraTextJV, FGMain(GAcName, I).Value, FGMain(GCredit, I).Value, "Cr")
                        End If
                    End If
                Next
            End If
            '================================================

            If CMain.DuplicacyChecking("Select Count(RecId) As Cnt From " & StrLedgerM & " LM Where LM.RecId='" & Trim(TxtRecId.Text) & "' And LM.DocId<>'" & (StrDocId) & "' And V_Prefix='" & TxtVNo.Tag & "' And V_Type='" & TxtType.Tag & "' And Site_Code='" & AgL.PubSiteCode & "' And Div_Code='" & AgL.PubDivCode & "'", "V.No. Already Exists.") Then TxtRecId.Focus() : Return False
            BlnTrans = True
            GCnCmd = AgL.GCn.CreateCommand
            GCnCmd.Transaction = AgL.GCn.BeginTransaction(IsolationLevel.Serializable)

            'If BytFormWorkAs = ClsStructure.EntryType.ForPosting Then
            '    GCnCmd.CommandText = "Delete From " & StrLedgerM & " Where DocId='" & (StrDocId) & "'"
            '    GCnCmd.ExecuteNonQuery()
            'End If

            GCnCmd.CommandText = "Delete From Stock Where DocId='" & (StrDocId) & "'"
            GCnCmd.ExecuteNonQuery()
            GCnCmd.CommandText = "Delete From " & StrLedgerAdj & " Where Vr_DocId='" & (StrDocId) & "'"
            GCnCmd.ExecuteNonQuery()
            GCnCmd.CommandText = "Delete From " & StrLedger & " Where DocId='" & (StrDocId) & "'"
            GCnCmd.ExecuteNonQuery()

            'Select Case BytFormWorkAs
            '    Case ClsStructure.EntryType.ForPosting
            '        BlnFlag = True
            '    Case ClsStructure.EntryType.ForEntry
            '        BlnFlag = True
            'End Select


            'If BlnFlag Then
            GCnCmd.CommandText = "Delete From LedgerItemAdj Where DocId='" & (StrDocId) & "'"
            GCnCmd.ExecuteNonQuery()

            If StrMode = "Add" Then
                GCnCmd.CommandText = "Insert Into " & StrLedgerM & "(DocId,V_Type,v_Prefix,Site_Code, Div_Code,V_No,V_Date,SubCode," &
                                     "Narration,PostedBy,RecId," &
                                     "U_Name,U_EntDt,U_AE,PreparedBy) Values " &
                                     "('" & (StrDocId) & "','" & TxtType.Tag & "','" & TxtVNo.Tag & "','" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "', " &
                                     "'" & TxtVNo.Text & "'," & AgL.Chk_Date(CDate(TxtVDate.Text).ToString("s")) & "," & AgL.Chk_Text(TxtAcName.Tag) & ", " &
                                     "" & AgL.Chk_Text(TxtNarration.Text) & ",'" & TxtPostedBy.Text & "','" & TxtRecId.Text & "'," &
                                     "'" & AgL.PubUserName & "','" & Format(AgL.PubLoginDate, "Short Date") & "'," &
                                     "'" & Microsoft.VisualBasic.Left(Topctrl1.Mode, 1) & "','" & AgL.PubUserName & "')"
            Else

                'If BytFormWorkAs = ClsStructure.EntryType.ForPosting Then
                '    GCnCmd.CommandText = "Insert Into " & StrLedgerM & "(DocId,V_Type,v_Prefix,Site_Code,Div_Code,V_No,V_Date,SubCode," &
                '                                             "Narration,PostedBy,RecId," &
                '                                             "U_Name,U_EntDt,U_AE,PreparedBy) Values " &
                '                                             "('" & (StrDocId) & "','" & TxtType.Tag & "','" & TxtVNo.Tag & "','" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "', " &
                '                                             "'" & TxtVNo.Text & "'," & AgL.Chk_Text(CDate(TxtVDate.Text).ToString("s")) & "," & AgL.Chk_Text(TxtAcName.Tag) & ", " &
                '                                             "" & AgL.Chk_Text(TxtNarration.Text) & ",'" & TxtPostedBy.Text & "','" & TxtRecId.Text & "'," &
                '                                             "'" & AgL.PubUserName & "','" & Format(AgL.PubLoginDate, "Short Date") & "'," &
                '                                             "'" & Microsoft.VisualBasic.Left(Topctrl1.Mode, 1) & "','" & AgL.PubUserName & "')"
                'Else
                GCnCmd.CommandText = "Update " & StrLedgerM & " Set "
                    GCnCmd.CommandText = GCnCmd.CommandText + "Site_Code='" & AgL.PubSiteCode & "', "
                    GCnCmd.CommandText = GCnCmd.CommandText + "Div_Code='" & AgL.PubDivCode & "', "
                GCnCmd.CommandText = GCnCmd.CommandText + "V_Date=" & AgL.Chk_Date(CDate(TxtVDate.Text).ToString("s")) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "SubCode=" & AgL.Chk_Text(TxtAcName.Tag) & ", "
                    GCnCmd.CommandText = GCnCmd.CommandText + "Narration=" & AgL.Chk_Text(TxtNarration.Text) & ", "
                    GCnCmd.CommandText = GCnCmd.CommandText + "RecId='" & TxtRecId.Text & "', "
                    GCnCmd.CommandText = GCnCmd.CommandText + "PostedBy='" & TxtPostedBy.Text & "', "
                    GCnCmd.CommandText = GCnCmd.CommandText + "Transfered='N', "
                    GCnCmd.CommandText = GCnCmd.CommandText + "U_Name='" & AgL.PubUserName & "', "
                    GCnCmd.CommandText = GCnCmd.CommandText + "U_EntDt='" & Format(AgL.PubLoginDate, "Short Date") & "', "
                    GCnCmd.CommandText = GCnCmd.CommandText + "U_AE='" & Microsoft.VisualBasic.Left(Topctrl1.Mode, 1) & "' "
                    GCnCmd.CommandText = GCnCmd.CommandText + "Where DocId='" & (StrDocId) & "' "
                End If
            'End If
            GCnCmd.ExecuteNonQuery()

            IntV_SNo_For_Stock = 0
            IntV_SNo = 0
            StrChequeNo = ""
            StrChequeDt = ""
            For I = 0 To FGMain.Rows.Count - 1
                If Trim(FGMain(GAcName, I).Value) <> "" Then
                    If StrContraTextOther <> "" Then StrContraTextOther += vbCrLf
                    If Val(FGMain(GDebit, I).Value) > 0 Then
                        FPrepareContraText(False, StrContraTextOther, FGMain(GAcName, I).Value, FGMain(GDebit, I).Value, "Dr")
                    Else
                        FPrepareContraText(False, StrContraTextOther, FGMain(GAcName, I).Value, FGMain(GCredit, I).Value, "Cr")
                    End If

                    If UCase(Trim(LblCurrentType.Tag)) <> "JV" Then
                        If FGMain.Columns(GDebit).Visible Then
                            FPrepareContraText(True, StrContraTextJV, TxtAcName.Text, FGMain(GDebit, I).Value, "Cr")
                        Else
                            FPrepareContraText(True, StrContraTextJV, TxtAcName.Text, FGMain(GCredit, I).Value, "Dr")
                        End If
                    End If

                    If Trim(FGMain(GNarration, I).Value) <> "" Then
                        If StrNarrationForHeader <> "" Then StrNarrationForHeader += vbCrLf
                        StrNarrationForHeader += AgL.Chk_Text(FGMain(GNarration, I).Value)
                    End If

                    Dim bRecId As String = TxtRecId.Text
                    Dim bRecDate As String = TxtVDate.Text
                    If FGMain.Item(GRecId, I).Value <> "" Then bRecId = FGMain.Item(GRecId, I).Value
                    If FGMain.Item(GRecDate, I).Value <> "" Then bRecDate = FGMain.Item(GRecDate, I).Value

                    IntV_SNo = IntV_SNo + 1
                    GCnCmd.CommandText = "Insert Into " & StrLedger & "(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr," &
                                     "Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,CostCenter,ContraText,OrignalAmt,TDSDeductFrom) Values " &
                                     "('" & (StrDocId) & "','" & bRecId & "'," & IntV_SNo & "," & AgL.Chk_Date(bRecDate) & "," & AgL.Chk_Text(FGMain(GAcCode, I).Value) & "," & AgL.Chk_Text(TxtAcName.Tag) & ", " &
                                     "" & Val(FGMain(GDebit, I).Value) & "," & Val(FGMain(GCredit, I).Value) & ", " &
                                     "" & AgL.Chk_Text(FGMain(GNarration, I).Value) & ",'" & TxtType.Tag & "','" & TxtVNo.Text & "','" & TxtVNo.Tag & "'," &
                                     "'" & AgL.PubSiteCode & "','" & AgL.PubDivCode & "'," & AgL.Chk_Text(FGMain(GChqNo, I).Value) & "," &
                                     "" & AgL.Chk_Date(Trim(FGMain(GChqDate, I).Value)) & "," & AgL.Chk_Text(FGMain(GTDSCategoryCode, I).Value) & "," &
                                     "" & Val(FGMain(GTDSOnAmount, I).Value) & "," & AgL.Chk_Text(FGMain(GCostCenterCode, I).Value) & ",'" & StrContraTextJV & "'," & Val(FGMain(GOrignalAmt, I).Value) & "," & AgL.Chk_Text(FGMain(GTDSDeductFrom, I).Value) & ")"
                    GCnCmd.ExecuteNonQuery()
                    Int_Prv_V_SNo = IntV_SNo

                    If Trim(FGMain(GChqNo, I).Value) <> "" Then
                        If Trim(StrChequeNo) = "" Then
                            StrChequeNo = Trim(FGMain(GChqNo, I).Value)
                            StrChequeDt = Trim(FGMain(GChqDate, I).Value)
                        ElseIf UCase(Trim(StrChequeNo)) <> UCase(Trim(FGMain(GChqNo, I).Value)) Then
                            StrChequeNo = ""
                            StrChequeDt = ""
                        End If
                    End If
                    SVTMain = DTStruct.Rows(I).Item("SSDB")

                    StrContraTDS = ""
                    'For TDS
                    If Trim(FGMain(GTDSCategoryCode, I).Value) <> "" Then
                        DblCredit_Total = 0
                        DblDebit_Total = 0
                        Narration = "TDS Deducted Against " & FGMain(GTDSCategory, I).Value & " On " & Val(FGMain(GTDSOnAmount, I).Value) & " From " & FGMain(GTDSDeductFromName, I).Value
                        For J = 0 To UBound(SVTMain.TDSVar)
                            If Trim(SVTMain.TDSVar(J).StrDescCode) <> "" Then
                                DblCredit = SVTMain.TDSVar(J).DblAmount
                                DblDebit = 0
                                DblDebit_Total = DblDebit_Total + DblCredit
                                DblCredit_Total = 0

                                FPrepareContraText(True, StrContraTDS_BF, FGMain(GAcName, I).Value, DblCredit, "Dr")
                                If StrContraTDS <> "" Then StrContraTDS += vbCrLf
                                FPrepareContraText(False, StrContraTDS, SVTMain.TDSVar(J).StrPostingAc, DblCredit, "Cr")

                                IntV_SNo = IntV_SNo + 1
                                GCnCmd.CommandText = "Insert Into " & StrLedger & "(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr," &
                                                             "Narration,V_Type,V_No,V_Prefix,Site_Code, DivCode,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,TDSDesc,TDSPer,TDS_Of_V_SNo,System_Generated,FormulaString,ContraText) Values " &
                                                             "('" & (StrDocId) & "','" & TxtRecId.Text & "'," & IntV_SNo & "," & AgL.Chk_Date(CDate(TxtVDate.Text).ToString("s")) & "," & AgL.Chk_Text(SVTMain.TDSVar(J).StrPostingAcCode) & "," & AgL.Chk_Text(FGMain(GAcCode, I).Value) & ", " &
                                                             "" & DblDebit & "," & DblCredit & ", " &
                                                             "" & AgL.Chk_Text(Narration) & " @ " & Trim(SVTMain.TDSVar(J).DblPercentage) & ",'" & TxtType.Tag & "','" & TxtVNo.Text & "','" & TxtVNo.Tag & "'," &
                                                             "'" & AgL.PubSiteCode & "','" & AgL.PubDivCode & "'," & AgL.Chk_Text("") & "," &
                                                             "" & AgL.Chk_Text("") & "," & AgL.Chk_Text(FGMain(GTDSCategoryCode, I).Value) & "," &
                                                             "" & Val(FGMain(GTDSOnAmount, I).Value) & "," & AgL.Chk_Text(SVTMain.TDSVar(J).StrDescCode) & "," & SVTMain.TDSVar(J).DblPercentage & "," & Int_Prv_V_SNo & ",'Y','" & SVTMain.TDSVar(J).StrFormula & "','" & StrContraTDS_BF & "')"
                                GCnCmd.ExecuteNonQuery()
                            End If
                        Next

                        '======== Inserting Sum Of TDS In Party A/c 
                        IntV_SNo = IntV_SNo + 1
                        GCnCmd.CommandText = "Insert Into " & StrLedger & "(DocId,RecId,V_SNo,V_Date,SubCode,AmtDr,AmtCr," &
                                         "Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,System_Generated,ContraText) Values " &
                                         "('" & (StrDocId) & "','" & TxtRecId.Text & "'," & IntV_SNo & "," & AgL.Chk_Date(CDate(TxtVDate.Text).ToString("s")) & "," & AgL.Chk_Text(FGMain(GAcCode, I).Value) & ", " &
                                         "" & DblDebit_Total & "," & DblCredit_Total & ", " &
                                         "" & AgL.Chk_Text(Narration) & ",'" & TxtType.Tag & "','" & TxtVNo.Text & "','" & TxtVNo.Tag & "'," &
                                         "'" & AgL.PubSiteCode & "','" & AgL.PubDivCode & "'," & AgL.Chk_Text("") & "," &
                                         "" & AgL.Chk_Text("") & ",'" & FGMain(GTDSCategoryCode, I).Value & "'," &
                                         "" & Val(FGMain(GTDSOnAmount, I).Value) & ",'Y','" & StrContraTDS & "')"
                        GCnCmd.ExecuteNonQuery()
                    End If

                    'For Ledger Adjustment
                    If Not SVTMain.LAdjVar Is Nothing Then
                        If SVTMain.LAdjVar.Length > 0 Then
                            For J = 0 To SVTMain.LAdjVar.Length - 1
                                If Val(SVTMain.LAdjVar(J).DblAdjustment) > 0 Then
                                    GCnCmd.CommandText = "Insert Into " & StrLedgerAdj & "(Adj_Type, Vr_DocId,Vr_V_SNo,Adj_DocId,Adj_V_SNo,Amount,Site_Code,Div_Code) Values " &
                                                 " ('" & (SVTMain.LAdjVar(J).StrAdj_Type) & "','" & (StrDocId) & "', " &
                                                 " " & Int_Prv_V_SNo & ",'" & (SVTMain.LAdjVar(J).StrDocId) & "', " &
                                                 " " & Val(SVTMain.LAdjVar(J).StrV_SNo) & ", " &
                                                 " " & IIf(AgL.StrCmp(SVTMain.LAdjVar(J).StrAdjustmentDrCr, "Cr"), -SVTMain.LAdjVar(J).DblAdjustment, SVTMain.LAdjVar(J).DblAdjustment) & ", " &
                                                 " '" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "')"
                                    GCnCmd.ExecuteNonQuery()
                                End If
                            Next
                        End If
                    End If

                    'For Ledger Item Adjustment
                    If Not SVTMain.LIAdjVar Is Nothing Then
                        If SVTMain.LIAdjVar.Length > 0 Then
                            For J = 0 To UBound(SVTMain.LIAdjVar)
                                If Trim(SVTMain.LIAdjVar(J).StrItemCode) <> "" Then
                                    IntV_SNo_For_Stock += 1
                                    GCnCmd.CommandText = "Insert Into LedgerItemAdj(DocId,V_SNo,ItemCode,Quantity,Amount,Remark) Values " &
                                                     "('" & (StrDocId) & "'," & Int_Prv_V_SNo & "," & AgL.Chk_Text(SVTMain.LIAdjVar(J).StrItemCode) & "," & Val(SVTMain.LIAdjVar(J).DblQuantity) & ", " &
                                                     "" & SVTMain.LIAdjVar(J).DblAmount & ",'" & SVTMain.LIAdjVar(J).StrRemark & "')"
                                    GCnCmd.ExecuteNonQuery()

                                    If BytFormWorkAs = ClsStructure.EntryType.ForPosting Then
                                        GCnCmd.CommandText = "Insert Into Stock(DocId,V_Type,RecId,V_Date,V_SNo,ItemCode,LandedValue,Remark,EType_IR,Site_Code,Div_Code) Values " &
                                                         "('" & (StrDocId) & "', '" & TxtType.Tag & "','" & TxtRecId.Text & "'," & AgL.Chk_Date(CDate(TxtVDate.Text).ToString("s")) & " ," & IntV_SNo_For_Stock & "," & AgL.Chk_Text(SVTMain.LIAdjVar(J).StrItemCode) & ", " &
                                                         "" & IIf(Val(FGMain(GDebit, I).Value) > 0, SVTMain.LIAdjVar(J).DblAmount, 0 - SVTMain.LIAdjVar(J).DblAmount) & ",'" & SVTMain.LIAdjVar(J).StrRemark & "','R','" & AgL.PubSiteCode & "','" & AgL.PubDivCode & "')"
                                        GCnCmd.ExecuteNonQuery()
                                    End If
                                End If
                            Next
                        End If
                    End If

                End If
            Next

            If UCase(Trim(LblCurrentType.Tag)) <> "JV" Then
                IntV_SNo = IntV_SNo + 1
                If StrChequeDt <> "" Then StrChequeDt = CDate(StrChequeDt).ToString("s")
                GCnCmd.CommandText = "Insert Into " & StrLedger & "(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr," &
                                                     "Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,System_Generated,ContraText,Chq_No,Chq_Date) Values " &
                                                     "('" & (StrDocId) & "','" & TxtRecId.Text & "'," & IntV_SNo & "," & AgL.Chk_Date(CDate(TxtVDate.Text).ToString("s")) & "," & AgL.Chk_Text(TxtAcName.Tag) & "," & AgL.Chk_Text("") & ", " &
                                                     "" & IIf(FGMain.Columns(GCredit).Visible, Val(LblCrAmt.Text), 0) & "," &
                                                     "" & IIf(FGMain.Columns(GDebit).Visible, Val(LblDrAmt.Text), 0) & ", " &
                                                     "" & AgL.Chk_Text(StrNarrationForHeader) & ",'" & TxtType.Tag & "','" & TxtVNo.Text & "'," &
                                                     "'" & TxtVNo.Tag & "','" & AgL.PubSiteCode & "','" & AgL.PubDivCode & "','Y','" & StrContraTextOther & "','" & StrChequeNo & "'," & AgL.Chk_Date(StrChequeDt) & ")"
                GCnCmd.ExecuteNonQuery()
            End If
            'End If

            '======================== For Posted By Updation ======================
            GCnCmd.CommandText = "Update LedgerM Set "
            GCnCmd.CommandText = GCnCmd.CommandText + "PostedBy='" & TxtPostedBy.Text & "' "
            GCnCmd.CommandText = GCnCmd.CommandText + "Where DocId='" & (StrDocId) & "' "
            GCnCmd.ExecuteNonQuery()


            'AgL.UpdateVoucherCounter(StrDocId, CDate(CDate(TxtVDate.Text).ToString("s")), AgL.GCn, GCnCmd, AgL.PubDivCode, AgL.PubSiteCode)
            If Topctrl1.Mode = "Add" Then
                'GCnCmd.CommandText = "Update voucher_prefix set start_srl_no = " & Val(TxtVNo.Text) + 1 & " where v_type = " & AgL.Chk_Text(TxtType.Tag) & " and prefix=" & AgL.Chk_Text(TxtVNo.Tag) & " And Div_Code = " & AgL.Chk_Text(AgL.PubDivCode) & " And Site_Code = " & AgL.Chk_Text(AgL.PubSiteCode) & " and start_srl_no <= " & Val(TxtVNo.Text) & " "
                GCnCmd.CommandText = "Update voucher_prefix set start_srl_no = " & Val(TxtVNo.Text) + 1 & " where v_type = " & AgL.Chk_Text(TxtType.Tag) & " and prefix=" & AgL.Chk_Text(TxtVNo.Tag) & "  and start_srl_no <= " & Val(TxtVNo.Text) & " "
                GCnCmd.ExecuteNonQuery()
            End If

            '======================================================================
            GCnCmd.Transaction.Commit()
            BlnTrans = False

        Catch ex As Exception
            If BlnTrans = True Then GCnCmd.Transaction.Rollback()
            BlnRtn = False
            MsgBox(ex.Message)
        End Try

        Return BlnRtn
    End Function
    Private Shared Sub FPrepareContraText(ByVal BlnOverWrite As Boolean, ByRef StrContraTextVar As String,
    ByVal StrContraName As String, ByVal DblAmount As Double, ByVal StrDrCr As String)
        Dim IntNameMaxLen As Integer = 35, IntAmtMaxLen As Integer = 18, IntSpaceNeeded As Integer = 2

        If BlnOverWrite Then
            StrContraTextVar = Mid(Trim(StrContraName), 1, IntNameMaxLen) & Space((IntNameMaxLen + IntSpaceNeeded) - Len(Mid(Trim(StrContraName), 1, IntNameMaxLen))) & Space(IntAmtMaxLen - Len(Format(Val(DblAmount), "##,##,##,##,##0.00"))) & Format(Val(DblAmount), "##,##,##,##,##0.00") & " " & Trim(StrDrCr)
        Else
            StrContraTextVar += Mid(Trim(StrContraName), 1, IntNameMaxLen) & Space((IntNameMaxLen + IntSpaceNeeded) - Len(Mid(Trim(StrContraName), 1, IntNameMaxLen))) & Space(IntAmtMaxLen - Len(Format(Val(DblAmount), "##,##,##,##,##0.00"))) & Format(Val(DblAmount), "##,##,##,##,##0.00") & " " & Trim(StrDrCr)
        End If
    End Sub

    Private Sub FrmVoucherEntry_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)

        LblBG.BackColor = Color.LemonChiffon
        LblTotalName.BackColor = Color.LemonChiffon
        LblCrName.BackColor = Color.LemonChiffon
        LblDrName.BackColor = Color.LemonChiffon
        LblDifferenceName.BackColor = Color.LemonChiffon
        LblDrAmt.BackColor = Color.LemonChiffon
        LblCrAmt.BackColor = Color.LemonChiffon
        LblDifferenceAmt.BackColor = Color.LemonChiffon
        LblPtyBalance.BackColor = Color.LemonChiffon
    End Sub
    Private Sub TxtOrdDate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtVDate.Validated
        Select Case sender.name
            Case TxtVDate.Name
                sender.Text = AgL.RetDate(sender.Text)
                FGenerateNo()

                If CDate(sender.text) >= "01-Apr-2020" Then
                    MsgBox("Entry point is not valid from this 01-Apr-2020.")
                    Exit Sub
                End If
        End Select
    End Sub
    Private Sub FIniStructure()
        Dim DCStruct As New DataColumn
        Try
            DTStruct = New DataTable
            DTStruct.Clear()
            DCStruct = New DataColumn
            DCStruct.DataType = System.Type.GetType("System.Object")
            DCStruct.ColumnName = "SSDB"
            DTStruct.Columns.Add(DCStruct)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub FUpdateRowStructure(ByVal SSDBVar As ClsStructure.VoucherType, ByVal IntCurentPosition As Integer)
        Dim ObjTemp As Object

        'Checking If DataRow Exists On Partcular Index
        'If Not Then Create A New Row
        Try
LblRecursive:
            If IntCurentPosition >= 0 Then
                ObjTemp = DTStruct.Rows(IntCurentPosition).Item("SSDB")
            End If
        Catch ex As Exception
            FAddRowStructure()
            GoTo LblRecursive
        End Try

        'Updating Row Of Particular Index
        Try
            If IntCurentPosition >= 0 Then
                DTStruct.Rows(IntCurentPosition).Item("SSDB") = SSDBVar
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FAddRowStructure()
        Dim DRStruct As DataRow
        Try

            DRStruct = DTStruct.NewRow
            DTStruct.Rows.Add(DRStruct)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FGMain_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles FGMain.CellBeginEdit
        Select Case e.ColumnIndex
            Case GDebit
                'If Val(FGMain(GCredit, e.RowIndex).Value) <> 0 Or Trim(FGMain(GTDSCategory, e.RowIndex).Value) <> "" Or FChkAdjExists(e.RowIndex) Then
                If Trim(FGMain(GTDSCategory, e.RowIndex).Value) <> "" Or FChkAdjExists(e.RowIndex) Then
                    e.Cancel = True
                Else
                    FGMain(GCredit, e.RowIndex).Value = ""
                End If
            Case GCredit
                'If Val(FGMain(GDebit, e.RowIndex).Value) <> 0 Or Trim(FGMain(GTDSCategory, e.RowIndex).Value) <> "" Or FChkAdjExists(e.RowIndex) Then
                If Trim(FGMain(GTDSCategory, e.RowIndex).Value) <> "" Or FChkAdjExists(e.RowIndex) Then
                    e.Cancel = True
                Else
                    FGMain(GDebit, e.RowIndex).Value = ""
                End If
        End Select
    End Sub
    Private Function FChkAdjExists(ByVal IntRow As Integer) As Boolean
        Dim I As Integer
        Dim BlnFlag As Boolean = False

        SVTMain = DTStruct.Rows(FGMain.CurrentRow.Index).Item("SSDB")
        If Not SVTMain.LAdjVar Is Nothing Then
            For I = 0 To UBound(SVTMain.LAdjVar)
                If Val(SVTMain.LAdjVar(I).DblAdjustment) > 0 Then
                    BlnFlag = True
                    Exit For
                End If
            Next
        End If
        FChkAdjExists = BlnFlag
    End Function
    Private Sub FGMain_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles FGMain.CellEndEdit
        FCalculate()
    End Sub
    Private Sub FGMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FGMain.KeyDown
        If Topctrl1.Mode <> "Browse" Then
            If e.Control Then
                If e.KeyCode = Keys.D Then
                    FGMain.CurrentRow.Selected = True
                Else
                    Exit Sub
                End If
            End If
            If FGMain.SelectedCells.Count > 0 Then If FGMain.CurrentCell.ColumnIndex = GNarration And e.Control And e.KeyCode = Keys.V Then FGMain(GNarration, FGMain.CurrentCell.RowIndex).Value = Clipboard.GetText
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
        Try
            Select Case FGMain.CurrentCell.ColumnIndex
                Case GAcManaulCode
                    If Not FChkAdjExists(FGMain.CurrentCell.RowIndex) Then FHPGD_Account(e)
                Case GAcName
                    If Not FChkAdjExists(FGMain.CurrentCell.RowIndex) Then FHPGD_AccountName(e)
                Case GCostCenter
                    FHPGD_CostCenter(e)
                Case GNarration
                    FHPGD_Narration(e)
            End Select
        Catch Ex As NullReferenceException
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub
    Private Sub FGMain_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles FGMain.RowsAdded
        FUpdateRowStructure(New ClsStructure.VoucherType, e.RowIndex)
        FGMain(GSNo, FGMain.Rows.Count - 1).Value = Trim(FGMain.Rows.Count)
    End Sub
    Private Sub FGMain_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles FGMain.RowsRemoved
        Try
            DTStruct.Rows.Remove(DTStruct.Rows.Item(e.RowIndex))
        Catch
        End Try
        AgL.FSetSNo(FGMain, GSNo)
        FCalculate()
    End Sub
    Private Sub FGMain_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles FGMain.EditingControlShowing
        If TypeOf e.Control Is AgControls.AgTextBox Then
            RemoveHandler DirectCast(e.Control, AgControls.AgTextBox).KeyPress, AddressOf FGrdNumPress
            AddHandler DirectCast(e.Control, AgControls.AgTextBox).KeyPress, AddressOf FGrdNumPress
        End If
    End Sub
    Private Sub FGrdNumPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Select Case FGMain.CurrentCell.ColumnIndex
            Case GDebit, GCredit
                CMain.NumPress(sender, e, 10, 2, False)
        End Select
    End Sub
    Private Sub FHPGD_Account(ByRef e As System.Windows.Forms.KeyEventArgs)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String
        If Topctrl1.Mode = "Browse" Then Exit Sub

        If Not CMain.FGrdDisableKeys(e) Then Exit Sub

        StrSendText = CMain.FSendText(FGMain, Chr(e.KeyCode))


        DTMain = AgL.FillData("Select SG.SubCode As SearchCode,SG.ManualCode,SG.Name,IfNull(CT.CityName,''),SG.Nature,CCM.Name As CCName,SG.CostCenter From SubGroup SG Left Join CostCenterMast CCM On CCM.Code=SG.CostCenter Left Join City CT On CT.CityCode=SG.CityCode Where " & AgL.PubSiteConditionCommonAc(AgL.PubIsHo, "SG.SITE_CODE", AgL.PubSiteCode, "COMMONAC") & " Order By SG.ManualCode ", AgL.GCn).Tables(0)

        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 300, 860)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(3, "City", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(4, "Nature", 80, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(5, "Cost Center", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(6, , 0, , False)

        FRH.StartPosition = FormStartPosition.CenterScreen
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                FGMain(GAcCode, FGMain.CurrentCell.RowIndex).Value = FRH.DRReturn.Item(0)
                FGMain(GAcManaulCode, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(1))
                FGMain(GAcName, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(2))
                FGMain(GCostCenter, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(5))
                FGMain(GCostCenterCode, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(6))
                Call FCheckTDSApplicable(FRH.DRReturn.Item(0))
                FGMain(GAcName, FGMain.CurrentCell.RowIndex).ToolTipText = FGetLedgerBalance(FGMain(GAcCode, FGMain.CurrentCell.RowIndex).Value)
                FGMain(GAcName, FGMain.CurrentCell.RowIndex).ToolTipText = IIf(Val(FGMain(GAcName, FGMain.CurrentCell.RowIndex).ToolTipText) > 0, "Balance Dr " & Format(Math.Abs(Val(FGMain(GAcName, FGMain.CurrentCell.RowIndex).ToolTipText)), "0.00"), "Balance Cr " & Format(Math.Abs(Val(FGMain(GAcName, FGMain.CurrentCell.RowIndex).ToolTipText)), "0.00"))
                FGMain(GAcManaulCode, FGMain.CurrentCell.RowIndex).ToolTipText = FGMain(GAcName, FGMain.CurrentCell.RowIndex).ToolTipText
                FGMain(GAcBal, FGMain.CurrentCell.RowIndex).Value = FGMain(GAcName, FGMain.CurrentCell.RowIndex).ToolTipText
                LblPtyBalance.Text = FGMain(GAcBal, FGMain.CurrentCell.RowIndex).Value
                FMaintainLineBalance(FGMain.CurrentCell.RowIndex)

                If Trim(FGMain(GNarration, FGMain.CurrentCell.RowIndex).Value) = "" And FGMain.CurrentCell.RowIndex > 0 Then
                    FGMain(GNarration, FGMain.CurrentCell.RowIndex).Value = FGMain(GNarration, FGMain.CurrentCell.RowIndex - 1).Value
                End If
            End If
        End If
        FRH = Nothing
    End Sub
    Private Sub FCheckTDSApplicable(ByVal StrAcCode As String)
        Dim DTMain As New DataTable
        Dim FrmObj As Form = Nothing
        Dim StrTDSCategory As String
        Dim I As Integer

        DTMain = CMain.FGetDatTable("Select TDS_Catg  From subgroup Where Subcode='" & StrAcCode & "' And IfNull(TDS_Catg,'')<>'' ", AgL.GCn)
        If DTMain.Rows.Count > 0 Then
            StrTDSCategory = AgL.XNull(DTMain.Rows(I).Item("TDS_Catg"))
            DTMain.Clear()
            If MsgBox(ClsMain.MsgTDSApplicable, MsgBoxStyle.YesNo) = vbYes Then
                DTMain = CMain.FGetDatTable("select TD.FormulaString,TC.Code,TC.name ,TD.Percentage As TDSper,SG.Name As AcName,TCD.Code As TDSDesc,TCD.Name  as DName,SG.SubCode   " &
                         "From TDSCat TC Left Join TDSCat_Det TD on TC.code=TD.code " &
                         "Left Join subgroup SG on Sg.subcode=TD.ACcode  " &
                         "Left Join  TDSCat_Description AS TCD on TCD.code=TD.TDSDesc " &
                         "Where TC.code='" & StrTDSCategory & "' ", AgL.GCn)
                SVTMain = DTStruct.Rows(I).Item("SSDB")
                If DTMain.Rows.Count > 0 Then
                    FGMain(GTDS_Btn, I).Style.BackColor = Color.LavenderBlush
                    ReDim SVTMain.TDSVar(DTMain.Rows.Count - 1)
                End If

                FGMain(GTDSCategoryCode, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(DTMain.Rows(I).Item("code"))
                FGMain(GTDSCategory, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(DTMain.Rows(I).Item("name"))
                FrmObj = New FrmVoucherEntry_TDS(Me, FGMain.CurrentCell.RowIndex, FGMain(GTDSCategoryCode, FGMain.CurrentCell.RowIndex).Value, FGMain(GTDSCategory, FGMain.CurrentCell.RowIndex).Value, FGMain(GTDSDeductFrom, FGMain.CurrentCell.RowIndex).Value, FGMain(GTDSDeductFromName, FGMain.CurrentCell.RowIndex).Value, 0, 0, True)
                For I = 0 To DTMain.Rows.Count - 1
                    SVTMain.TDSVar(I).StrDescCode = AgL.XNull(DTMain.Rows(I).Item("TDSDesc"))
                    SVTMain.TDSVar(I).StrDesc = AgL.XNull(DTMain.Rows(I).Item("DName"))
                    SVTMain.TDSVar(I).StrPostingAcCode = AgL.XNull(DTMain.Rows(I).Item("SubCode"))
                    SVTMain.TDSVar(I).StrPostingAc = AgL.XNull(DTMain.Rows(I).Item("AcName"))
                    SVTMain.TDSVar(I).DblPercentage = Format(AgL.VNull(DTMain.Rows(I).Item("TDSPer")), "0.00")
                    SVTMain.TDSVar(I).StrFormula = AgL.XNull(DTMain.Rows(I).Item("FormulaString"))
                Next
                FUpdateRowStructure(SVTMain, FGMain.CurrentCell.RowIndex)
                FrmObj.ShowDialog()
                FrmObj.Dispose()
            End If
            FrmObj = Nothing
            DTMain.Dispose()
            DTMain = Nothing
        End If
    End Sub
    Private Sub FHPGD_AccountName(ByRef e As System.Windows.Forms.KeyEventArgs)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String
        If Topctrl1.Mode = "Browse" Then Exit Sub

        If Not CMain.FGrdDisableKeys(e) Then Exit Sub
        StrSendText = CMain.FSendText(FGMain, Chr(e.KeyCode))

        DTMain = AgL.FillData("Select SG.SubCode As SearchCode,SG.Name,SG.ManualCode," &
        "IfNull(CT.CityName,''),SG.Nature,CCM.Name As CCName,SG.CostCenter,AG.GroupName, Sg.SubgroupType " &
        "From SubGroup SG Left Join " &
        "AcGroup AG On AG.GroupCode=SG.GroupCode Left Join " &
        "CostCenterMast CCM On CCM.Code=SG.CostCenter Left Join " &
        "City CT On CT.CityCode=SG.CityCode " &
        "Where " & AgL.PubSiteConditionCommonAc(AgL.PubIsHo, "SG.SITE_CODE", AgL.PubSiteCode, "COMMONAC") & " Order By SG.Name ", AgL.GCn).Tables(0)


        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 300, 910)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 300, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(2, "Code", 0, , False)
        FRH.FFormatColumn(3, "City", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(4, "Nature", 80, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(5, "Cost Center", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(6, , 0, , False)
        FRH.FFormatColumn(7, "Group", 150, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(7, "A/c Type", 150, DataGridViewContentAlignment.MiddleLeft)
        FRH.StartPosition = FormStartPosition.CenterScreen
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                FGMain(GAcCode, FGMain.CurrentCell.RowIndex).Value = FRH.DRReturn.Item(0)
                FGMain(GAcName, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(1))
                FGMain(GAcManaulCode, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(2))
                FGMain(GCostCenter, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(5))
                FGMain(GCostCenterCode, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(6))
                Call FCheckTDSApplicable(FRH.DRReturn.Item(0))

                FGMain(GAcName, FGMain.CurrentCell.RowIndex).ToolTipText = FGetLedgerBalance(FGMain(GAcCode, FGMain.CurrentCell.RowIndex).Value)
                FGMain(GAcName, FGMain.CurrentCell.RowIndex).ToolTipText = IIf(Val(FGMain(GAcName, FGMain.CurrentCell.RowIndex).ToolTipText) > 0, "Balance Dr " & Format(Math.Abs(Val(FGMain(GAcName, FGMain.CurrentCell.RowIndex).ToolTipText)), "0.00"), "Balance Cr " & Format(Math.Abs(Val(FGMain(GAcName, FGMain.CurrentCell.RowIndex).ToolTipText)), "0.00"))
                FGMain(GAcManaulCode, FGMain.CurrentCell.RowIndex).ToolTipText = FGMain(GAcName, FGMain.CurrentCell.RowIndex).ToolTipText
                FGMain(GAcBal, FGMain.CurrentCell.RowIndex).Value = FGMain(GAcName, FGMain.CurrentCell.RowIndex).ToolTipText
                LblPtyBalance.Text = FGMain(GAcBal, FGMain.CurrentCell.RowIndex).Value
                FMaintainLineBalance(FGMain.CurrentCell.RowIndex)

                If Trim(FGMain(GNarration, FGMain.CurrentCell.RowIndex).Value) = "" And FGMain.CurrentCell.RowIndex > 0 Then
                    FGMain(GNarration, FGMain.CurrentCell.RowIndex).Value = FGMain(GNarration, FGMain.CurrentCell.RowIndex - 1).Value
                End If
            End If
        End If
        FRH = Nothing
    End Sub
    Private Sub FMaintainLineBalance(ByVal IntRowIndex As Integer)
        Dim IntColIndex As Integer, DblDifference As Double

        FCalculate()
        IntColIndex = IIf(UCase(Mid(Trim(LblDifferenceAmt.Text), 1, 2)) = "DR", GDebit,
                      IIf(UCase(Mid(Trim(LblDifferenceAmt.Text), 1, 2)) = "CR", GCredit, -1))

        DblDifference = Val(Mid(Trim(LblDifferenceAmt.Text), 3, Len(Trim(LblDifferenceAmt.Text))))
        If IntColIndex > 0 Then
            If Val(FGMain(GDebit, IntRowIndex).Value) = 0 And Val(FGMain(GCredit, IntRowIndex).Value) = 0 Then FGMain(IntColIndex, IntRowIndex).Value = DblDifference
            FCalculate()
        End If
    End Sub
    Private Sub FHPGD_Narration(ByRef e As System.Windows.Forms.KeyEventArgs)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String
        If Topctrl1.Mode = "Browse" Then Exit Sub

        If e.KeyCode = Keys.Delete Then
            FGMain(GNarration, FGMain.CurrentCell.RowIndex).Value = ""
        End If
        If Not e.KeyCode = Keys.Insert Then Exit Sub

        StrSendText = CMain.FSendText(FGMain, Chr(e.KeyCode))
        DTMain = AgL.FillData("Select Code,Name From NarrationMast Order By Name ", AgL.GCn).Tables(0)

        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 300, 480)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Narration", 400, DataGridViewContentAlignment.MiddleLeft)
        FRH.StartPosition = FormStartPosition.CenterScreen
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                FGMain(GNarration, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(1))
            End If
        End If
        FRH = Nothing
    End Sub
    Private Sub FHPGD_CostCenter(ByRef e As System.Windows.Forms.KeyEventArgs)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String
        If Topctrl1.Mode = "Browse" Then Exit Sub

        If e.KeyCode = Keys.Delete Then
            FGMain(GCostCenter, FGMain.CurrentCell.RowIndex).Value = ""
            FGMain(GCostCenterCode, FGMain.CurrentCell.RowIndex).Value = ""
        End If
        If Not CMain.FGrdDisableKeys(e) Then Exit Sub

        StrSendText = CMain.FSendText(FGMain, Chr(e.KeyCode))
        DTMain = AgL.FillData("Select CCM.Code,CCM.Name From CostCenterMast CCM Order By CCM.Name ", AgL.GCn).Tables(0)

        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 300, 280)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Cost Center", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.StartPosition = FormStartPosition.CenterScreen
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                FGMain(GCostCenter, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(1))
                FGMain(GCostCenterCode, FGMain.CurrentCell.RowIndex).Value = AgL.XNull(FRH.DRReturn.Item(0))
            End If
        End If
        FRH = Nothing
    End Sub
    Private Function FCkhGrid() As Boolean
        Dim I As Integer
        Dim BlnRtn As Boolean, BlnItemExists As Boolean

        BlnRtn = True
        BlnItemExists = False
        For I = 0 To FGMain.Rows.Count - 1
            If Trim(FGMain(GAcName, I).Value) <> "" Then
                BlnItemExists = True

                If Val(FGMain(GDebit, I).Value) <= 0 And Val(FGMain(GCredit, I).Value) <= 0 Then
                    MsgBox("Please Define in Enviro" & " Vaild Amount.")
                    FGMain(GAcName, I).Selected = True
                    BlnRtn = False
                    FGMain.Focus()
                    Exit For
                End If
            End If

            If Not BlnRtn Then
                Exit For
            End If
        Next

        If Not BlnItemExists Then
            MsgBox("Please Define in Enviro" & "Entry.")
            FGMain(GAcName, 0).Selected = True
            BlnRtn = False
            FGMain.Focus()
        End If

        If BlnRtn Then
            If Val(LblCrAmt.Text) <> Val(LblDrAmt.Text) And UCase(TxtType.Tag) <> "OB" Then
                If Trim(StrDefaultAcCode) = "" Or Trim(FGMain(GAcCode, FGMain.Rows.Count - 1).Value) <> "" Then
                    MsgBox(ClsMain.Msg_6)
                    FGMain(GDebit, 0).Selected = True
                    BlnRtn = False
                    FGMain.Focus()
                Else
                    FGMain(GAcCode, FGMain.Rows.Count - 1).Value = StrDefaultAcCode
                    FGMain(GAcName, FGMain.Rows.Count - 1).Value = StrDefaultAcName
                    FGMain(GNarration, FGMain.Rows.Count - 1).Value = FGMain(GNarration, FGMain.Rows.Count - 2).Value
                    FGMain(GChqNo, FGMain.Rows.Count - 1).Value = FGMain(GChqNo, FGMain.Rows.Count - 2).Value
                    FGMain(GChqDate, FGMain.Rows.Count - 1).Value = FGMain(GChqDate, FGMain.Rows.Count - 2).Value
                    FMaintainLineBalance(FGMain.Rows.Count - 1)
                End If
            End If
        End If

        FCkhGrid = BlnRtn
    End Function

    Private Sub BtnPayments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles BtnPayments.Click, BtnJournal.Click, BtnReceipt.Click, BtnPostedBy.Click, BtnRefreshVNo.Click,
    BtnCopy.Click, BtnPaste.Click

        Dim GCnCmd As New Object

        Select Case sender.name
            Case BtnPayments.Name
                FManageScreen("PMT")
                TxtType.Focus()
            Case BtnReceipt.Name
                FManageScreen("RCT")
                TxtType.Focus()
            Case BtnJournal.Name
                FManageScreen("JV")
                TxtType.Focus()
            Case BtnPostedBy.Name
                If DTMaster.Rows.Count > 0 Then
                    If MsgBox(ClsMain.MsgSaveCnf) = MsgBoxResult.No Then Exit Sub
                    '================================================================================
                    '================== Write Your Validations In FSaveValidation() =================
                    If Not FSaveValidation() Then Exit Sub
                    '================================================================================

                    StrDocID = AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
                    If CMain.FGetMaxNo("Select Count(*) Cnt From DataAudit Where DocId='" & StrDocID & "' ", AgL.GCn) > 0 Then MsgBox("Record Has Been Audited. You Can Not Edit/ Delete This Record.") : Exit Sub
                    If Not CMain.FGetMaxNo("Select Count(DocId) From LedgerM Where DocId='" & StrDocID & "'", AgL.GCn) > 0 Then MsgBox(ClsMain.MsgRecNotFnd) : Exit Sub
                    If Not CMain.FGetMaxNo("Select Count(DocId) From LedgerM Where DocId='" & StrDocID & "' And IfNull(PostedBy,'')=''", AgL.GCn) > 0 Then
                        If MsgBox("Are You Sure? You Want To UnPost This Record.") = MsgBoxResult.No Then
                            Exit Sub
                        End If
                        TxtPostedBy.Text = ""
                    Else
                        TxtPostedBy.Text = AgL.PubUserName
                    End If

                    '================================================================================
                    '====================== Write Your Save Code In FSave() =========================
                    If Not FSave("Add", StrDocID, "LedgerM", "Ledger", "LedgerAdj", ClsStructure.EntryType.ForPosting) Then Exit Sub
                    '================================================================================

                    FIniMaster(1)
                    MoveRec()
                End If
            Case BtnRefreshVNo.Name
                FGenerateNo(True)
            Case BtnCopy.Name
                If DTMaster.Rows.Count > 0 Then StrCopyDocId = AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
            Case BtnPaste.Name
                FPasteRecord(StrCopyDocId)
        End Select
    End Sub
    Private Sub FGMain_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles FGMain.CellContentClick
        Dim FrmObj As Form
        Dim DblTemp As Double

        If e.RowIndex < 0 Then Exit Sub
        Select Case e.ColumnIndex
            Case GChqDet_Btn
                If Topctrl1.Mode = "Browse" Then
                    FrmObj = New FrmVoucherEntry_Chq_Det(Me, e.RowIndex, False, TxtAcName.Tag)
                Else
                    FrmObj = New FrmVoucherEntry_Chq_Det(Me, e.RowIndex, True, TxtAcName.Tag)
                End If
                FrmObj.ShowDialog()
                FrmObj.Dispose()
                FrmObj = Nothing

            Case GTDS_Btn
                If Trim(FGMain(GAcName, e.RowIndex).Value) = "" Then MsgBox("Please Mention A/c Name.") : Exit Sub

                If Val(FGMain(GTDSOnAmount, e.RowIndex).Value) > 0 Then
                    DblTemp = Val(FGMain(GTDSOnAmount, e.RowIndex).Value)
                ElseIf Val(FGMain(GCredit, e.RowIndex).Value) > 0 Then
                    DblTemp = Val(FGMain(GCredit, e.RowIndex).Value)
                ElseIf Val(FGMain(GDebit, e.RowIndex).Value) > 0 Then
                    DblTemp = Val(FGMain(GDebit, e.RowIndex).Value)
                Else
                    DblTemp = 0
                End If

                SVTMain = DTStruct.Rows(FGMain.CurrentRow.Index).Item("SSDB")
                If SVTMain.TDSVar Is Nothing Then ReDim SVTMain.TDSVar(1)
                If Topctrl1.Mode = "Browse" Then
                    FrmObj = New FrmVoucherEntry_TDS(Me, e.RowIndex, FGMain(GTDSCategoryCode, e.RowIndex).Value, FGMain(GTDSCategory, e.RowIndex).Value, FGMain(GTDSDeductFrom, e.RowIndex).Value, FGMain(GTDSDeductFromName, e.RowIndex).Value, DblTemp, IIf(Val(FGMain(GOrignalAmt, e.RowIndex).Value) > 0, Val(FGMain(GOrignalAmt, e.RowIndex).Value), DblTemp), False)
                Else
                    FrmObj = New FrmVoucherEntry_TDS(Me, e.RowIndex, FGMain(GTDSCategoryCode, e.RowIndex).Value, FGMain(GTDSCategory, e.RowIndex).Value, FGMain(GTDSDeductFrom, e.RowIndex).Value, FGMain(GTDSDeductFromName, e.RowIndex).Value, DblTemp, IIf(Val(FGMain(GOrignalAmt, e.RowIndex).Value) > 0, Val(FGMain(GOrignalAmt, e.RowIndex).Value), DblTemp), True)
                End If
                FrmObj.ShowDialog()
                FrmObj.Dispose()
                FrmObj = Nothing

            Case GAdj_Btn
                If Trim(FGMain(GAcName, e.RowIndex).Value) = "" Then MsgBox("Please Mention A/c Name.") : Exit Sub

                SVTMain = DTStruct.Rows(FGMain.CurrentRow.Index).Item("SSDB")
                'If SVTMain.LAdjVar Is Nothing Then FFillLedgerAdj(e.RowIndex, "")

                If Val(FGMain(GDebit, e.RowIndex).Value) > 0 Then DblTemp = Val(FGMain(GDebit, e.RowIndex).Value) Else DblTemp = Val(FGMain(GCredit, e.RowIndex).Value)

                If Topctrl1.Mode = "Browse" Then
                    FrmObj = New FrmVoucherEntry_LedgerAdjNew(Me, e.RowIndex, FGMain(GAcName, e.RowIndex).Tag, FGMain(GAcName, e.RowIndex).Value, DblTemp, StrDocID, False)
                Else
                    FrmObj = New FrmVoucherEntry_LedgerAdjNew(Me, e.RowIndex, FGMain(GAcName, e.RowIndex).Tag, FGMain(GAcName, e.RowIndex).Value, DblTemp, StrDocID, True)
                End If


                FrmObj.ShowDialog()
                FrmObj.Dispose()
                FrmObj = Nothing

            Case GIAdj_Btn
                If Trim(FGMain(GAcName, e.RowIndex).Value) = "" Then MsgBox("Please Mention A/c Name.") : Exit Sub

                SVTMain = DTStruct.Rows(FGMain.CurrentRow.Index).Item("SSDB")
                If SVTMain.LIAdjVar Is Nothing Then ReDim SVTMain.LIAdjVar(1)

                If Val(FGMain(GDebit, e.RowIndex).Value) > 0 Then DblTemp = Val(FGMain(GDebit, e.RowIndex).Value) Else DblTemp = Val(FGMain(GCredit, e.RowIndex).Value)

                If Topctrl1.Mode = "Browse" Then
                    FrmObj = New FrmVoucherEntry_LedgerItemAdj(Me, e.RowIndex, FGMain(GAcName, e.RowIndex).Value, DblTemp, False)
                Else
                    FrmObj = New FrmVoucherEntry_LedgerItemAdj(Me, e.RowIndex, FGMain(GAcName, e.RowIndex).Value, DblTemp, True)
                End If
                FrmObj.ShowDialog()
                FrmObj.Dispose()
                FrmObj = Nothing
        End Select
    End Sub

    'Private Sub FMovRecLedgerAdj(ByVal IntRow As Integer, ByVal StrV_SNo As String)
    '    Dim DTTemp As DataTable
    '    Dim StrSQL As String = ""
    '    Dim I As Integer
    '    Dim StrCurrentDocId$ = ""
    '    Dim StrFieldContra As String = ""

    '    StrCurrentDocId = AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))

    '    If Val(FGMain(GDebit, IntRow).Value) > 0 Then
    '        StrFieldContra = " LG.AmtCr "
    '    ElseIf Val(FGMain(GCredit, IntRow).Value) > 0 Then
    '        StrFieldContra = " LG.AmtDr "
    '    End If
    '    If StrFieldContra = "" Then Exit Sub

    '    StrSQL = StrSQL + "Select LA.Adj_Type, LA.Adj_DocId As DocId,LA.Adj_V_SNo As V_SNo, LG.RecId As RecId, LG.V_Type, LG.V_Date, "
    '    StrSQL = StrSQL + "LG.Narration, IfNull(" & StrFieldContra & ",0) As BillAmt,0 As Adjusted,LA.Amount As Adjustment	"
    '    StrSQL = StrSQL + "From LedgerAdj LA Left Join Ledger LG On LA.Adj_DocId = LG.DocId And LA.Adj_V_SNo = LG.V_SNo "
    '    StrSQL = StrSQL + "Where LA.Vr_DocId='" & StrCurrentDocId & "' "
    '    StrSQL = StrSQL + "And LA.Vr_V_SNo='" & StrV_SNo & "' "

    '    DTTemp = CMain.FGetDatTable(StrSQL, AgL.GCn)
    '    If DTTemp.Rows.Count > 0 Then
    '        ReDim SVTMain.LAdjVar(DTTemp.Rows.Count)
    '    End If
    '    For I = 0 To DTTemp.Rows.Count - 1
    '        SVTMain.LAdjVar(I).StrAdj_Type = AgL.XNull(DTTemp.Rows(I).Item("Adj_Type"))
    '        SVTMain.LAdjVar(I).StrDocId = AgL.XNull(DTTemp.Rows(I).Item("DocId"))
    '        SVTMain.LAdjVar(I).StrV_No = AgL.XNull(DTTemp.Rows(I).Item("RecId"))
    '        SVTMain.LAdjVar(I).StrV_SNo = AgL.XNull(DTTemp.Rows(I).Item("V_SNo"))
    '        SVTMain.LAdjVar(I).StrV_Date = AgL.XNull(DTTemp.Rows(I).Item("V_Date"))
    '        SVTMain.LAdjVar(I).StrV_Type = AgL.XNull(DTTemp.Rows(I).Item("V_Type"))
    '        SVTMain.LAdjVar(I).StrNarration = AgL.XNull(DTTemp.Rows(I).Item("Narration"))
    '        SVTMain.LAdjVar(I).DblBillAmt = Format(AgL.VNull(DTTemp.Rows(I).Item("BillAmt")), "0.00")
    '        SVTMain.LAdjVar(I).DblAdjusted = Format(AgL.VNull(DTTemp.Rows(I).Item("Adjusted")), "0.00")
    '        SVTMain.LAdjVar(I).DblBalanceAmt = Format(Val(SVTMain.LAdjVar(I).DblBillAmt) - Val(SVTMain.LAdjVar(I).DblAdjusted), "0.00")
    '        SVTMain.LAdjVar(I).DblAdjustment = Format(Math.Abs(AgL.VNull(DTTemp.Rows(I).Item("Adjustment"))), "0.00")
    '        SVTMain.LAdjVar(I).StrAdjustmentDrCr = IIf(AgL.VNull(DTTemp.Rows(I).Item("Adjustment")) < 0, "Cr", "Dr")
    '    Next
    '    DTStruct.Rows(IntRow).Item("SSDB") = SVTMain
    '    DTTemp.Dispose()
    '    DTTemp = Nothing
    'End Sub

    Private Sub FFillLedgerAdj(ByVal IntRow As Integer, ByVal StrV_SNo As String)
        Dim DTTemp As DataTable
        Dim StrSQL As String
        Dim StrCondition As String
        Dim StrFieldContra As String = ""
        Dim StrCurrentDocId As String
        Dim StrJoinCondition As String
        Dim I As Integer

        If Topctrl1.Mode = "Add" Then
            StrCurrentDocId = ""
        Else
            StrCurrentDocId = AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
        End If

        If Trim(StrV_SNo) = "" Then
            StrJoinCondition = " And LA.Vr_V_SNo=-1 "
        Else
            StrJoinCondition = " And LA.Vr_V_SNo=" & StrV_SNo & " "
        End If

        If Val(FGMain(GDebit, IntRow).Value) > 0 Then
            StrFieldContra = " LG.AmtCr "
        ElseIf Val(FGMain(GCredit, IntRow).Value) > 0 Then
            StrFieldContra = " LG.AmtDr "
        End If
        If StrFieldContra = "" Then Exit Sub
        StrCondition = "Where LG.SubCode='" & FGMain(GAcCode, IntRow).Value & "' "
        If Not AgL.PubIsHo Then StrCondition += "And LG.Site_Code='" & AgL.PubSiteCode & "' "

        StrSQL = "Select DocId,V_SNo,Max(RecId) As RecId,Max(V_Type) As V_Type,Max(V_Date) As V_Date,Max(Narration) As Narration, "
        StrSQL = StrSQL + "Max(BillAmt) As BillAmt,Sum(Adjusted) As Adjusted,Sum(Adjustment) As Adjustment "
        StrSQL = StrSQL + "From ( "
        StrSQL = StrSQL + "Select  LG.DocId,LG.V_SNo,(IfNull(LG.RecId,'')+'-'+IfNull(LG.Site_Code,'')) As RecId, "
        StrSQL = StrSQL + "LG.V_Type, LG.V_Date, LG.Narration, "
        StrSQL = StrSQL + "IfNull(" & StrFieldContra & ",0) As BillAmt,0 As Adjusted,0 As Adjustment "
        StrSQL = StrSQL + "From Ledger LG "
        StrSQL = StrSQL + StrCondition
        StrSQL = StrSQL + "And IfNull(" & StrFieldContra & ",0)>0 "
        StrSQL = StrSQL + "Union All "
        StrSQL = StrSQL + "Select	LA.Adj_DocId As DocId,LA.Adj_V_SNo As V_SNo,Null As RecId,Null As V_Type,Null As V_Date, "
        StrSQL = StrSQL + "Null As Narration,0 As BillAmt,LA.Amount As Adjusted,0 As Adjustment	 "
        StrSQL = StrSQL + "From LedgerAdj LA Left Join Ledger LG On LA.Adj_DocId=LG.DocId And LA.Adj_V_SNo=LG.V_SNo "
        StrSQL = StrSQL + StrCondition
        StrSQL = StrSQL + "And LA.Vr_DocId<>'" & StrCurrentDocId & "' "
        StrSQL = StrSQL + "Union All "
        StrSQL = StrSQL + "Select	LA.Adj_DocId As DocId,LA.Adj_V_SNo As V_SNo,Null As RecId,Null As V_Type,Null As V_Date, "
        StrSQL = StrSQL + "Null As Narration,0 As BillAmt,0 As Adjusted,LA.Amount As Adjustment	"
        StrSQL = StrSQL + "From LedgerAdj LA Left Join Ledger LG On LA.Adj_DocId=LG.DocId And LA.Adj_V_SNo=LG.V_SNo "
        StrSQL = StrSQL + StrJoinCondition
        StrSQL = StrSQL + StrCondition
        StrSQL = StrSQL + "And LA.Vr_DocId='" & StrCurrentDocId & "' "
        StrSQL = StrSQL + ") As Tmp "
        StrSQL = StrSQL + "Group By DocId,V_SNo "
        StrSQL = StrSQL + "Having	(IfNull(Max(BillAmt),0)-IfNull(Sum(Adjusted),0))>0"
        StrSQL = StrSQL + "Order By Max(V_Date),Max(RecId) "


        DTTemp = CMain.FGetDatTable(StrSQL, AgL.GCn)
        If DTTemp.Rows.Count > 0 Then
            ReDim SVTMain.LAdjVar(DTTemp.Rows.Count)
        End If
        For I = 0 To DTTemp.Rows.Count - 1
            SVTMain.LAdjVar(I).StrDocId = AgL.XNull(DTTemp.Rows(I).Item("DocId"))
            SVTMain.LAdjVar(I).StrV_No = AgL.XNull(DTTemp.Rows(I).Item("RecId"))
            SVTMain.LAdjVar(I).StrV_SNo = AgL.XNull(DTTemp.Rows(I).Item("V_SNo"))
            SVTMain.LAdjVar(I).StrV_Date = AgL.XNull(DTTemp.Rows(I).Item("V_Date"))
            SVTMain.LAdjVar(I).StrV_Type = AgL.XNull(DTTemp.Rows(I).Item("V_Type"))
            SVTMain.LAdjVar(I).StrNarration = AgL.XNull(DTTemp.Rows(I).Item("Narration"))
            SVTMain.LAdjVar(I).DblBillAmt = Format(AgL.VNull(DTTemp.Rows(I).Item("BillAmt")), "0.00")
            SVTMain.LAdjVar(I).DblAdjusted = Format(AgL.VNull(DTTemp.Rows(I).Item("Adjusted")), "0.00")
            SVTMain.LAdjVar(I).DblAdjustment = Format(AgL.VNull(DTTemp.Rows(I).Item("Adjustment")), "0.00")
        Next
        DTTemp.Dispose()
        DTTemp = Nothing
    End Sub
    'Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn
    '    Me.Cursor = Cursors.WaitCursor
    '    Try
    '        FPrintGlobal(Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")), TxtType.Tag, "", Me, TxtVNo.Tag)
    '    Catch Ex As Exception
    '    End Try
    '    Me.Cursor = Cursors.Default
    'End Sub
    ' changes done by preeti for multiple printing 20/4/10
    Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn
        Dim FrmObj_Show As FrmVoucherPrint
        If DTMaster.Rows.Count > 0 Then
            FrmObj_Show = New FrmVoucherPrint(AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")), TxtType.Tag, TxtType.Text, TxtVDate.Text, TxtRecId.Text, Me)
            FrmObj_Show.MdiParent = Me.MdiParent
            FrmObj_Show.Show()
        End If
        FrmObj_Show = Nothing
    End Sub

    Private Sub FGMain_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles FGMain.CellEnter
        If Topctrl1.Mode = "Browse" Or FGMain.Rows.Count <= 0 Then Exit Sub
        If FGMain.CurrentCell.ColumnIndex = GAcName Or FGMain.CurrentCell.ColumnIndex = GAcManaulCode Then
            LblPtyBalance.Text = FGMain(GAcBal, FGMain.CurrentCell.RowIndex).Value
        Else
            LblPtyBalance.Text = ""
        End If

    End Sub
    Private Sub BtnImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnImport.Click
        OFDMain.Filter = "*.xls|*.Xls"
        If OFDMain.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        If MsgBox("Are You Sure? You Want To Import Excel.") = MsgBoxResult.Yes Then
            FExcelFill()
        End If
    End Sub
    Private Sub FExcelFill()
        Dim XlsCon As New System.Data.OleDb.OleDbConnection
        Dim DTTemp As New DataTable
        Dim DTTemp1 As DataTable
        Dim StrSQL As String
        Dim I As Integer, IntRowCounter As Integer, IntStartFrom As Integer
        Dim BlnChk As Boolean

        BlnChk = True
        FClear()
        Try

            XlsCon = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; data source= '" + OFDMain.FileName + "' ;Extended Properties=Excel 8.0;")
            XlsCon.Open()
            IntStartFrom = 0
            Select Case Trim(UCase(LblCurrentType.Tag))
                Case "PMT"
                    StrSQL = "Select Xsl.AcCode,Xsl.Dr As AmtDr,0 As AmtCr,Xsl.Narration, "
                    StrSQL += "Xsl.Cr,Xsl.CHQ_No,Xsl.CHQ_DT "
                    StrSQL += "From [sheet1$] As Xsl "
                    StrSQL += "Order By Xsl.Cr Desc "
                    DTTemp = CMain.FGetDatTable(StrSQL, XlsCon)
                    BlnChk = False
                    If DTTemp.Rows.Count > 1 Then
                        If Not AgL.VNull(DTTemp.Rows(1).Item("Cr")) > 0 Then
                            BlnChk = True
                        End If
                    End If
                    IntStartFrom = 1
                Case "RCT"
                    StrSQL = "Select Xsl.AcCode,0 As AmtDr,Xsl.Cr As AmtCr,Xsl.Narration, "
                    StrSQL += "Xsl.Dr,Xsl.CHQ_No,Xsl.CHQ_DT "
                    StrSQL += "From [sheet1$] As Xsl "
                    StrSQL += "Order By Xsl.Dr Desc "
                    DTTemp = CMain.FGetDatTable(StrSQL, XlsCon)
                    BlnChk = False
                    If DTTemp.Rows.Count > 1 Then
                        If Not AgL.VNull(DTTemp.Rows(1).Item("Dr")) > 0 Then
                            BlnChk = True
                        End If
                    End If
                    IntStartFrom = 1
                Case Else
                    StrSQL = "Select Xsl.AcCode,Xsl.Dr As AmtDr,Xsl.Cr As AmtCr,Xsl.Narration, "
                    StrSQL += "Xsl.CHQ_No,Xsl.CHQ_DT "
                    StrSQL += "From [sheet1$] As Xsl "
                    DTTemp = CMain.FGetDatTable(StrSQL, XlsCon)
                    IntStartFrom = 0
            End Select

            If BlnChk Then
                If IntStartFrom <> 0 Then
                    DTTemp1 = CMain.FGetDatTable("Select SG.Name As AcName,SG.ManualCode,SG.SubCode " &
                                                    "From SubGroup SG  " &
                                                    "Where RTrim(LTrim(IfNull(SG.ManualCode,'')))='" & Trim(AgL.XNull(DTTemp.Rows(I).Item("AcCode"))) & "'", AgL.GCn)
                    If DTTemp1.Rows.Count > 0 Then
                        TxtAcName.Text = AgL.XNull(DTTemp1.Rows(0).Item("AcName"))
                        TxtAcName.Tag = AgL.XNull(DTTemp1.Rows(0).Item("SubCode"))
                    End If
                End If
                For I = IntStartFrom To DTTemp.Rows.Count - 1
                    FGMain.Rows.Add()
                    IntRowCounter = FGMain.Rows.Count - 2
                    FUpdateRowStructure(New ClsStructure.VoucherType, IntRowCounter)
                    FGMain(GSNo, IntRowCounter).Value = Trim(IntRowCounter + 1)
                    FGMain(GAcManaulCode, IntRowCounter).Value = Trim(AgL.XNull(DTTemp.Rows(I).Item("AcCode")))
                    FGMain(GDebit, IntRowCounter).Value = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0, Format(AgL.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00"), "")
                    FGMain(GCredit, IntRowCounter).Value = IIf(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")) > 0, Format(AgL.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00"), "")
                    FGMain(GNarration, IntRowCounter).Value = AgL.XNull(DTTemp.Rows(I).Item("Narration"))
                    FGMain(GChqNo, IntRowCounter).Value = AgL.XNull(DTTemp.Rows(I).Item("CHQ_No"))
                    FGMain(GChqDate, IntRowCounter).Value = AgL.XNull(DTTemp.Rows(I).Item("CHQ_Dt"))

                    DTTemp1 = CMain.FGetDatTable("Select SG.Name As AcName,SG.ManualCode,SG.SubCode " &
                                                    "From SubGroup SG  " &
                                                    "Where RTrim(LTrim(IfNull(SG.ManualCode,'')))='" & Trim(AgL.XNull(DTTemp.Rows(I).Item("AcCode"))) & "'", AgL.GCn)
                    If DTTemp1.Rows.Count > 0 Then
                        FGMain(GAcCode, IntRowCounter).Value = AgL.XNull(DTTemp1.Rows(0).Item("SubCode"))
                        FGMain(GAcName, IntRowCounter).Value = AgL.XNull(DTTemp1.Rows(0).Item("AcName"))
                    End If

                    SVTMain = DTStruct.Rows(IntRowCounter).Item("SSDB")
                    FUpdateRowStructure(SVTMain, IntRowCounter)
                Next
            Else
                MsgBox("Import Failed.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        DTTemp.Dispose()
        XlsCon.Close()
        XlsCon.Dispose()
        FUpdateRowStructure(New ClsStructure.VoucherType, FGMain.Rows.Count - 1)
        FCalculate()
    End Sub

    Private Sub Topctrl1_Load(sender As Object, e As EventArgs) Handles Topctrl1.Load

    End Sub

    Private Sub FGMain_RowLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles FGMain.RowLeave
        Try
            FGMain.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.White
        Catch ex As Exception
        End Try
    End Sub
    Private Sub FGMain_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles FGMain.RowEnter
        Try
            FGMain.Rows(e.RowIndex).DefaultCellStyle.BackColor = FGMain.ColumnHeadersDefaultCellStyle.BackColor
        Catch ex As Exception
        End Try
    End Sub

    Public Sub FindMove(ByVal bDocId As String)
        Try
            If bDocId <> "" Then
                AgL.PubSearchRow = bDocId
                If AgL.PubMoveRecApplicable Then
                    AgL.PubDRFound = DTMaster.Rows.Find(AgL.PubSearchRow)
                    BMBMaster.Position = DTMaster.Rows.IndexOf(AgL.PubDRFound)
                End If
                Call MoveRec()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub FMovRecLedgerAdj(ByVal IntRow As Integer, ByVal StrV_SNo As String)
    '    Dim DTTemp As DataTable
    '    Dim StrSQL As String = ""
    '    Dim I As Integer
    '    Dim StrCurrentDocId$ = ""
    '    Dim StrFieldContra As String = ""
    '    Dim StrBalanceDrCrField As String = ""
    '    Dim StrCondition As String = ""

    '    StrCurrentDocId = AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))

    '    If Val(FGMain(FrmVoucherEntry.GDebit, IntRow).Value) > 0 Then
    '        StrFieldContra = " LG.AmtCr "
    '        StrBalanceDrCrField = "Case When Max(BillAmt) - IfNull(Sum(Adjusted),0) < 0 Then 'Dr' Else 'Cr' End BalanceDrCr "
    '    ElseIf Val(FGMain(FrmVoucherEntry.GCredit, IntRow).Value) > 0 Then
    '        StrFieldContra = " LG.AmtDr "
    '        StrBalanceDrCrField = "Case When Max(BillAmt) - IfNull(Sum(Adjusted),0) < 0 Then 'Cr' Else 'Dr' End BalanceDrCr "
    '    End If
    '    If StrFieldContra = "" Then Exit Sub
    '    StrCondition = "Where LG.SubCode='" & FGMain(FrmVoucherEntry.GAcCode, IntRow).Value & "' "
    '    If Not AgL.PubIsHo Then StrCondition += "And LG.Site_Code='" & AgL.PubSiteCode & "' "


    '    'StrSQL = "Select DocId, V_SNo, Max(Adj_Type) As Adj_Type, Max(RecId) As RecId, Max(V_Type) As V_Type, " & _
    '    '                    " Max(V_Date) As V_Date, Max(Narration) As Narration, " & _
    '    '                    " Max(BillAmt) As BillAmt, Max(DueDate) As DueDate, " & _
    '    '                    " Max(BillAmt) - IfNull(Sum(Adjusted),0) As PendingAmt , " & _
    '    '                    " " & StrBalanceDrCrField & ", " & _
    '    '                    " Sum(Adjusted) As Adjusted, Sum(Adjustment) As Adjustment " & _
    '    '                    " From ( " & _
    '    '                    "       Select Null As Adj_Type, LG.DocId,LG.V_SNo,LG.RecId, " & _
    '    '                    "       LG.V_Type, LG.V_Date, Dateadd(Day,IfNull(LG.CreditDays,0), LG.V_Date) As DueDate, LG.Narration, " & _
    '    '                    "       IfNull(" & StrFieldContra & ",0) As BillAmt,0 As Adjusted,0 As Adjustment " & _
    '    '                    "       From Ledger LG " & StrCondition & _
    '    '                    "       And IfNull(" & StrFieldContra & ",0)>0 " & _
    '    '                    " Union All " & _
    '    '                    "       Select	Null As Adj_Type, LA.Adj_DocId As DocId,LA.Adj_V_SNo As V_SNo,Null As RecId,Null As V_Type,Null As V_Date, " & _
    '    '                    "       Null As Narration,0 As BillAmt, Null As DueDate, Abs(LA.Amount) As Adjusted,0 As Adjustment	 " & _
    '    '                    "       From LedgerAdj LA " & _
    '    '                    "       Left Join Ledger LG On LA.Adj_DocId=LG.DocId And LA.Adj_V_SNo=LG.V_SNo " & StrCondition & _
    '    '                    "       And LA.Vr_DocId<>'" & StrCurrentDocId & "' " & _
    '    '                    " Union All " & _
    '    '                    "       Select	LA.Adj_Type, LA.Adj_DocId As DocId,LA.Adj_V_SNo As V_SNo,Null As RecId,Null As V_Type,Null As V_Date, " & _
    '    '                    "       Null As Narration, 0 As BillAmt, Null As DueDate,0 As Adjusted, Abs(LA.Amount) As Adjustment	" & _
    '    '                    "       From LedgerAdj LA " & _
    '    '                    "       Left Join Ledger LG On LA.Adj_DocId=LG.DocId And LA.Adj_V_SNo=LG.V_SNo " & StrCondition & _
    '    '                    "       And LA.Vr_DocId='" & StrCurrentDocId & "' " & _
    '    '                    " ) As Tmp " & _
    '    '                    " Group By DocId, V_SNo " & _
    '    '                    " Having Sum(Adjustment) > 0 " & _
    '    '                    " Order By Max(V_Date),Max(RecId) "

    '    StrSQL = "Select DocId, V_SNo, Max(Adj_Type) As Adj_Type, Max(RecId) As RecId, Max(V_Type) As V_Type, " & _
    '                        " Max(V_Date) As V_Date, Max(Narration) As Narration, Max(Name) As Name, " & _
    '                        " Abs(Sum(BillAmt)) As BillAmt, Max(DueDate) As DueDate, " & _
    '                        " Abs(Sum(BillAmt) + IfNull(Sum(Adjusted),0)) As PendingAmt , " & _
    '                        " " & StrBalanceDrCrField & ", " & _
    '                        " Sum(Adjusted) As Adjusted, Sum(Adjustment) As Adjustment " & _
    '                        " From ( " & _
    '                        "       Select  Null As Adj_Type, LG.DocId,LG.V_SNo,LG.RecId, " & _
    '                        "       LG.V_Type, LG.V_Date, " & _
    '                        "       Dateadd(Day,IfNull(LG.CreditDays,0), LG.V_Date) As DueDate, LG.Narration, Sg.Name," & _
    '                        "       IfNull(LG.AmtDr,0) - IfNull(LG.AmtCr,0) As BillAmt,0 As Adjusted,0 As Adjustment " & _
    '                        "       From Ledger LG " & _
    '                        "       LEFT JOIN SubGroup Sg On Lg.SubCode = Sg.SubCode " & StrCondition & _
    '                        "       And IfNull(LG.AmtDr,0) - IfNull(LG.AmtCr,0) <> 0 " & _
    '                        " Union All " & _
    '                        "       Select	Null As Adj_Type, LA.Adj_DocId As DocId,LA.Adj_V_SNo As V_SNo,Null As RecId, " & _
    '                        "       Null As V_Type,Null As V_Date, " & _
    '                        "       Null As DueDate, Null As Narration, Sg.Name As Name, " & _
    '                        "       0 As BillAmt, LA.Amount As Adjusted,0 As Adjustment	 " & _
    '                        "       From LedgerAdj LA " & _
    '                        "       Left Join Ledger LG On LA.Adj_DocId=LG.DocId And LA.Adj_V_SNo=LG.V_SNo " & _
    '                        "       LEFT JOIN SubGroup Sg On Lg.SubCode = Sg.SubCode " & StrCondition & _
    '                        "       And LA.Vr_DocId<>'" & StrCurrentDocId & "' " & _
    '                        " Union All " & _
    '                        "       Select	LA.Adj_Type, LA.Adj_DocId As DocId,LA.Adj_V_SNo As V_SNo,Null As RecId, " & _
    '                        "       Null As V_Type,Null As V_Date, " & _
    '                        "       Null As DueDate,Null As Narration, Sg.Name As Name, " & _
    '                        "       0 As BillAmt, 0 As Adjusted, LA.Amount As Adjustment	" & _
    '                        "       From LedgerAdj LA " & _
    '                        "       Left Join Ledger LG On LA.Adj_DocId=LG.DocId And LA.Adj_V_SNo=LG.V_SNo " & _
    '                        "       LEFT JOIN SubGroup Sg On Lg.SubCode = Sg.SubCode " & StrCondition & _
    '                        "       And LA.Vr_DocId='" & StrCurrentDocId & "' " & _
    '                        " ) As Tmp " & _
    '                        " Group By DocId, V_SNo " & _
    '                        " Having Sum(Adjustment) > 0 " & _
    '                        " Order By Max(V_Date),Max(RecId) "

    '    DTTemp = CMain.FGetDatTable(StrSQL, AgL.GCn)
    '    If DTTemp.Rows.Count > 0 Then
    '        ReDim SVTMain.LAdjVar(DTTemp.Rows.Count)
    '    End If
    '    For I = 0 To DTTemp.Rows.Count - 1
    '        SVTMain.LAdjVar(I).StrAdj_Type = AgL.XNull(DTTemp.Rows(I).Item("Adj_Type"))
    '        SVTMain.LAdjVar(I).StrDocId = AgL.XNull(DTTemp.Rows(I).Item("DocId"))
    '        SVTMain.LAdjVar(I).StrV_No = AgL.XNull(DTTemp.Rows(I).Item("RecId"))
    '        SVTMain.LAdjVar(I).StrV_SNo = AgL.XNull(DTTemp.Rows(I).Item("V_SNo"))
    '        SVTMain.LAdjVar(I).StrV_Date = AgL.XNull(DTTemp.Rows(I).Item("V_Date"))
    '        SVTMain.LAdjVar(I).StrV_Type = AgL.XNull(DTTemp.Rows(I).Item("V_Type"))
    '        SVTMain.LAdjVar(I).StrNarration = AgL.XNull(DTTemp.Rows(I).Item("Narration"))
    '        SVTMain.LAdjVar(I).DblBillAmt = Format(AgL.VNull(DTTemp.Rows(I).Item("BillAmt")), "0.00")
    '        SVTMain.LAdjVar(I).DblAdjusted = Format(AgL.VNull(DTTemp.Rows(I).Item("Adjusted")), "0.00")
    '        SVTMain.LAdjVar(I).DblBalanceAmt = Format(AgL.VNull(DTTemp.Rows(I).Item("PendingAmt")), "0.00")
    '        SVTMain.LAdjVar(I).StrBalanceAmtDrCr = AgL.XNull(DTTemp.Rows(I).Item("BalanceDrCr"))
    '        SVTMain.LAdjVar(I).DblAdjustment = Format(Math.Abs(AgL.VNull(DTTemp.Rows(I).Item("Adjustment"))), "0.00")

    '        If SVTMain.LAdjVar(I).StrBalanceAmtDrCr = "Cr" Then
    '            SVTMain.LAdjVar(I).StrAdjustmentDrCr = "Dr"
    '        Else
    '            SVTMain.LAdjVar(I).StrAdjustmentDrCr = "Cr"
    '        End If
    '    Next
    '    DTStruct.Rows(IntRow).Item("SSDB") = SVTMain
    '    DTTemp.Dispose()
    '    DTTemp = Nothing
    'End Sub

    Private Sub FMovRecLedgerAdj(ByVal IntRow As Integer, ByVal StrV_SNo As String)
        Dim StrSql$ = ""
        Dim DTTemp As DataTable = Nothing
        Dim DTMain As New DataTable
        Dim StrSendText As String = ""
        Dim StrCondition As String
        Dim StrFieldContra As String = ""
        Dim StrCurrentDocId As String = ""
        Dim StrBalanceDrCrField As String = ""
        Dim I As Integer = 0

        StrCurrentDocId = AgL.XNull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
        StrBalanceDrCrField = "Case When Sum(BillAmt) + IfNull(Sum(Adjusted),0) < 0 Then 'Cr' Else 'Dr' End BalanceDrCr "
        StrCondition = "Where 1=1 "
        If Not AgL.PubIsHo Then StrCondition += "And LG.Site_Code='" & AgL.PubSiteCode & "' "



        StrSql = "Select DocId, V_SNo, Max(Adj_Type) As Adj_Type, Max(RecId) As RecId, Max(V_Type) As V_Type, " &
                            " Max(V_Date) As V_Date, Max(Narration) As Narration, Max(Name) As Name, " &
                            " Abs(Sum(BillAmt)) As BillAmt, Max(DueDate) As DueDate, " &
                            " Abs(Sum(BillAmt) + IfNull(Sum(Adjusted),0)) As PendingAmt , " &
                            " " & StrBalanceDrCrField & ", " &
                            " Sum(Adjusted) As Adjusted, Sum(Adjustment) As Adjustment " &
                            " From ( " &
                            "       Select Null As Adj_Type,  LG.DocId,LG.V_SNo,LG.RecId, " &
                            "       LG.V_Type, LG.V_Date, " &
                            "        Date(LG.V_date, '+' ||IfNull(LG.CreditDays,0) || ' day') As DueDate, LG.Narration, Sg.Name," &
                            "       IfNull(LG.AmtDr,0) - IfNull(LG.AmtCr,0) As BillAmt,0 As Adjusted,0 As Adjustment " &
                            "       From Ledger LG " &
                            "       LEFT JOIN SubGroup Sg On Lg.SubCode = Sg.SubCode " & StrCondition &
                            "       And IfNull(LG.AmtDr,0) - IfNull(LG.AmtCr,0) <> 0 " &
                            " Union All " &
                            "       Select	Null As Adj_Type, LA.Adj_DocId As DocId,LA.Adj_V_SNo As V_SNo,Null As RecId, " &
                            "       Null As V_Type,Null As V_Date, " &
                            "       Null As DueDate, Null As Narration, Sg.Name As Name, " &
                            "       0 As BillAmt, LA.Amount As Adjusted,0 As Adjustment	 " &
                            "       From LedgerAdj LA " &
                            "       Left Join Ledger LG On LA.Adj_DocId=LG.DocId And LA.Adj_V_SNo=LG.V_SNo " &
                            "       LEFT JOIN SubGroup Sg On Lg.SubCode = Sg.SubCode " & StrCondition &
                            "       And LA.Vr_DocId<>'" & StrCurrentDocId & "' and LA.Vr_V_Sno='" & StrV_SNo & "' " &
                            " Union All " &
                            "       Select	LA.Adj_Type, LA.Adj_DocId As DocId,LA.Adj_V_SNo As V_SNo,Null As RecId, " &
                            "       Null As V_Type,Null As V_Date, " &
                            "       Null As DueDate,Null As Narration, Sg.Name As Name, " &
                            "       0 As BillAmt, 0 As Adjusted, LA.Amount As Adjustment	" &
                            "       From LedgerAdj LA " &
                            "       Left Join Ledger LG On LA.Adj_DocId=LG.DocId And LA.Adj_V_SNo=LG.V_SNo " &
                            "       LEFT JOIN SubGroup Sg On Lg.SubCode = Sg.SubCode " & StrCondition &
                            "       And LA.Vr_DocId ='" & StrCurrentDocId & "'  and LA.Vr_V_Sno='" & StrV_SNo & "' " &
                            " ) As Tmp " &
                            " Group By DocId, V_SNo " &
                            " Having Sum(Adjustment) <> 0 " &
                            " Order By Max(V_Date),Max(RecId) "

        DTTemp = CMain.FGetDatTable(StrSql, AgL.GCn)
        If DTTemp.Rows.Count > 0 Then
            ReDim SVTMain.LAdjVar(DTTemp.Rows.Count)
        End If
        For I = 0 To DTTemp.Rows.Count - 1
            SVTMain.LAdjVar(I).StrAdj_Type = AgL.XNull(DTTemp.Rows(I).Item("Adj_Type"))
            SVTMain.LAdjVar(I).StrDocId = AgL.XNull(DTTemp.Rows(I).Item("DocId"))
            SVTMain.LAdjVar(I).StrV_No = AgL.XNull(DTTemp.Rows(I).Item("RecId"))
            SVTMain.LAdjVar(I).StrV_SNo = AgL.XNull(DTTemp.Rows(I).Item("V_SNo"))
            SVTMain.LAdjVar(I).StrV_Date = AgL.XNull(DTTemp.Rows(I).Item("V_Date"))
            SVTMain.LAdjVar(I).StrV_Type = AgL.XNull(DTTemp.Rows(I).Item("V_Type"))
            SVTMain.LAdjVar(I).StrNarration = AgL.XNull(DTTemp.Rows(I).Item("Narration"))
            SVTMain.LAdjVar(I).DblBillAmt = Format(AgL.VNull(DTTemp.Rows(I).Item("BillAmt")), "0.00")
            SVTMain.LAdjVar(I).DblAdjusted = Format(AgL.VNull(DTTemp.Rows(I).Item("Adjusted")), "0.00")
            SVTMain.LAdjVar(I).DblBalanceAmt = Format(AgL.VNull(DTTemp.Rows(I).Item("PendingAmt")), "0.00")
            SVTMain.LAdjVar(I).StrBalanceAmtDrCr = AgL.XNull(DTTemp.Rows(I).Item("BalanceDrCr"))
            SVTMain.LAdjVar(I).DblAdjustment = Format(Math.Abs(AgL.VNull(DTTemp.Rows(I).Item("Adjustment"))), "0.00")

            If AgL.VNull(DTTemp.Rows(I).Item("Adjustment")) < 0 Then
                SVTMain.LAdjVar(I).StrAdjustmentDrCr = "Cr"
            Else
                SVTMain.LAdjVar(I).StrAdjustmentDrCr = "Dr"
            End If

        Next
        DTStruct.Rows(IntRow).Item("SSDB") = SVTMain
        DTTemp.Dispose()
        DTTemp = Nothing
    End Sub

    Private Sub Topctrl1_RightToLeftChanged(sender As Object, e As EventArgs) Handles Topctrl1.RightToLeftChanged

    End Sub

    Private Sub Topctrl1_tbFirst() Handles Topctrl1.tbFirst

    End Sub
    Private Sub MnuImport_Click(sender As Object, e As EventArgs) Handles MnuImportFromExcel.Click, MnuImportFromDos.Click, MnuImportFromTally.Click, MnuImportFromTallyLedgerOpening.Click, MnuImportFromDosLedgerOpening.Click
        Select Case sender.name
            Case MnuImportFromExcel.Name
                FImportFromExcel(ImportFor.Excel, False)

            Case MnuImportFromDos.Name
                FImportFromExcel(ImportFor.Dos, False)

            Case MnuImportFromDosLedgerOpening.Name
                FImportFromExcel(ImportFor.Dos, True)

            Case MnuImportFromTally.Name
                FImportFromTally()

            Case MnuImportFromTallyLedgerOpening.Name
                FImportFromTallyLedgerOpening()
        End Select
    End Sub
    Public Sub FImportFromTally()
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtTemp As New DataTable
        Dim I As Integer = 0, J As Integer = 0
        Dim bHeadSubCodeName As String = ""
        Dim FileNameWithPath As String = ""

        OFDMain.Filter = "*.xml|*.XML"
        If OFDMain.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        FileNameWithPath = OFDMain.FileName

        'Dim FileNameWithPath As String = My.Application.Info.DirectoryPath & "\TallyXML\PaymentRegister.xml"
        'Dim FileNameWithPath As String = My.Application.Info.DirectoryPath & "\TallyXML\ReceiptRegister.xml"

        Dim doc As New XmlDocument()
        doc.Load(FileNameWithPath)

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            Dim VoucherElementList As XmlNodeList = doc.GetElementsByTagName("VOUCHER")

            For I = 0 To VoucherElementList.Count - 1
                Dim VoucherEntryTableList(0) As StructVoucherEntry
                If VoucherElementList(I).SelectNodes("ALLLEDGERENTRIES.LIST") IsNot Nothing Then
                    For J = 0 To VoucherElementList(I).SelectNodes("ALLLEDGERENTRIES.LIST").Count - 1
                        Dim VoucherEntryTable As New StructVoucherEntry

                        VoucherEntryTable.DocId = ""

                        If VoucherElementList(I).SelectSingleNode("VOUCHERTYPENAME") IsNot Nothing Then
                            If VoucherElementList(I).SelectSingleNode("VOUCHERTYPENAME").ChildNodes.Count > 0 Then
                                If VoucherElementList(I).SelectSingleNode("VOUCHERTYPENAME").ChildNodes(0).Value = "Payment" Then
                                    VoucherEntryTable.V_Type = "BP"
                                ElseIf VoucherElementList(I).SelectSingleNode("VOUCHERTYPENAME").ChildNodes(0).Value = "Receipt" Then
                                    VoucherEntryTable.V_Type = "BR"
                                ElseIf VoucherElementList(I).SelectSingleNode("VOUCHERTYPENAME").ChildNodes(0).Value = "Contra" Then
                                    VoucherEntryTable.V_Type = "JV"
                                ElseIf VoucherElementList(I).SelectSingleNode("VOUCHERTYPENAME").ChildNodes(0).Value = "CHEQUE RETURN" Then
                                    VoucherEntryTable.V_Type = "JV"
                                ElseIf VoucherElementList(I).SelectSingleNode("VOUCHERTYPENAME").ChildNodes(0).Value = "Journal" Then
                                    VoucherEntryTable.V_Type = "JV"
                                End If
                            End If
                        End If


                        VoucherEntryTable.V_Prefix = ""
                        VoucherEntryTable.Site_Code = AgL.PubSiteCode
                        VoucherEntryTable.Div_Code = AgL.PubDivCode


                        If VoucherElementList(I).SelectSingleNode("VOUCHERNUMBER") IsNot Nothing Then
                            If VoucherElementList(I).SelectSingleNode("VOUCHERNUMBER").ChildNodes.Count > 0 Then
                                VoucherEntryTable.V_No = VoucherElementList(I).SelectSingleNode("VOUCHERNUMBER").ChildNodes(0).Value
                            End If
                        End If

                        If VoucherElementList(I).SelectSingleNode("DATE") IsNot Nothing Then
                            If VoucherElementList(I).SelectSingleNode("DATE").ChildNodes.Count > 0 Then
                                VoucherEntryTable.V_Date = VoucherElementList(I).SelectSingleNode("DATE").ChildNodes(0).Value.ToString.Substring(6, 2) + "/" +
                                        VoucherElementList(I).SelectSingleNode("DATE").ChildNodes(0).Value.ToString.Substring(4, 2) + "/" +
                                        VoucherElementList(I).SelectSingleNode("DATE").ChildNodes(0).Value.ToString.Substring(0, 4)
                            End If
                        End If

                        VoucherEntryTable.SubCode = ""

                        'If VoucherElementList(I).SelectSingleNode("PARTYLEDGERNAME") IsNot Nothing Then
                        '    If VoucherElementList(I).SelectSingleNode("PARTYLEDGERNAME").ChildNodes.Count > 0 Then
                        '        VoucherEntryTable.SubCodeName = VoucherElementList(I).SelectSingleNode("PARTYLEDGERNAME").ChildNodes(0).Value
                        '    End If
                        'End If

                        If VoucherElementList(I).SelectSingleNode("Narration") IsNot Nothing Then
                            If VoucherElementList(I).SelectSingleNode("Narration").ChildNodes.Count > 0 Then
                                VoucherEntryTable.Narration = VoucherElementList(I).SelectSingleNode("Narration").ChildNodes(0).Value
                            End If
                        End If

                        VoucherEntryTable.PostedBy = AgL.PubUserName
                        VoucherEntryTable.RecId = VoucherEntryTable.V_No
                        VoucherEntryTable.U_Name = AgL.PubUserName
                        VoucherEntryTable.U_EntDt = AgL.GetDateTime(AgL.GcnRead)
                        VoucherEntryTable.U_AE = "A"
                        VoucherEntryTable.PreparedBy = AgL.PubUserName

                        If VoucherElementList(I).SelectNodes("ALLLEDGERENTRIES.LIST").Item(J).SelectSingleNode("LEDGERNAME") IsNot Nothing Then


                            VoucherEntryTable.Line_RecId = VoucherEntryTable.V_No
                            VoucherEntryTable.Line_RecDate = VoucherEntryTable.V_Date
                            VoucherEntryTable.Line_V_SNo = J + 1
                            VoucherEntryTable.Line_V_Date = VoucherEntryTable.V_Date
                            VoucherEntryTable.Line_SubCode = ""
                            VoucherEntryTable.Line_SubCodeName = VoucherElementList(I).SelectNodes("ALLLEDGERENTRIES.LIST").Item(J).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value

                            VoucherEntryTable.Line_ContraSub = ""
                            VoucherEntryTable.Line_ContraSubName = ""


                            'If VoucherElementList(I).SelectNodes("ALLLEDGERENTRIES.LIST").Item(J).SelectSingleNode("AMOUNT").ChildNodes(0).Value < 0 Then
                            '    VoucherEntryTable.Line_AmtDr = Math.Abs(Convert.ToDecimal(VoucherElementList(I).SelectNodes("ALLLEDGERENTRIES.LIST").Item(J).SelectSingleNode("AMOUNT").ChildNodes(0).Value))
                            '    VoucherEntryTable.Line_AmtCr = 0
                            'ElseIf VoucherElementList(I).SelectNodes("ALLLEDGERENTRIES.LIST").Item(J).SelectSingleNode("AMOUNT").ChildNodes(0).Value > 0 Then
                            '    VoucherEntryTable.Line_AmtDr = 0
                            '    VoucherEntryTable.Line_AmtCr = VoucherElementList(I).SelectNodes("ALLLEDGERENTRIES.LIST").Item(J).SelectSingleNode("AMOUNT").ChildNodes(0).Value
                            'End If

                            If VoucherEntryTable.V_Type = "BP" Then
                                If VoucherElementList(I).SelectNodes("ALLLEDGERENTRIES.LIST").Item(J).SelectSingleNode("ISDEEMEDPOSITIVE").ChildNodes(0).Value = "No" Then
                                    bHeadSubCodeName = VoucherElementList(I).SelectNodes("ALLLEDGERENTRIES.LIST").Item(J).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value
                                End If
                            ElseIf VoucherEntryTable.V_Type = "BR" Then
                                If VoucherElementList(I).SelectNodes("ALLLEDGERENTRIES.LIST").Item(J).SelectSingleNode("ISDEEMEDPOSITIVE").ChildNodes(0).Value = "Yes" Then
                                    bHeadSubCodeName = VoucherElementList(I).SelectNodes("ALLLEDGERENTRIES.LIST").Item(J).SelectSingleNode("LEDGERNAME").ChildNodes(0).Value
                                End If
                            End If

                            'If VoucherElementList(I).SelectSingleNode("VOUCHERTYPENAME").ChildNodes(0).Value = "Contra" Then
                            '    If VoucherElementList(I).SelectNodes("ALLLEDGERENTRIES.LIST").Item(J).SelectSingleNode("ISDEEMEDPOSITIVE").ChildNodes(0).Value = "No" Then
                            '        VoucherEntryTable.Line_AmtDr = Math.Abs(Convert.ToDecimal(VoucherElementList(I).SelectNodes("ALLLEDGERENTRIES.LIST").Item(J).SelectSingleNode("AMOUNT").ChildNodes(0).Value))
                            '        VoucherEntryTable.Line_AmtCr = 0
                            '    ElseIf VoucherElementList(I).SelectNodes("ALLLEDGERENTRIES.LIST").Item(J).SelectSingleNode("ISDEEMEDPOSITIVE").ChildNodes(0).Value = "Yes" Then
                            '        VoucherEntryTable.Line_AmtDr = 0
                            '        VoucherEntryTable.Line_AmtCr = Math.Abs(Convert.ToDecimal(VoucherElementList(I).SelectNodes("ALLLEDGERENTRIES.LIST").Item(J).SelectSingleNode("AMOUNT").ChildNodes(0).Value))
                            '    End If
                            'Else
                            If VoucherElementList(I).SelectNodes("ALLLEDGERENTRIES.LIST").Item(J).SelectSingleNode("ISDEEMEDPOSITIVE").ChildNodes(0).Value = "Yes" Then
                                VoucherEntryTable.Line_AmtDr = Math.Abs(Convert.ToDecimal(VoucherElementList(I).SelectNodes("ALLLEDGERENTRIES.LIST").Item(J).SelectSingleNode("AMOUNT").ChildNodes(0).Value))
                                VoucherEntryTable.Line_AmtCr = 0
                            ElseIf VoucherElementList(I).SelectNodes("ALLLEDGERENTRIES.LIST").Item(J).SelectSingleNode("ISDEEMEDPOSITIVE").ChildNodes(0).Value = "No" Then
                                VoucherEntryTable.Line_AmtDr = 0
                                VoucherEntryTable.Line_AmtCr = Math.Abs(Convert.ToDecimal(VoucherElementList(I).SelectNodes("ALLLEDGERENTRIES.LIST").Item(J).SelectSingleNode("AMOUNT").ChildNodes(0).Value))
                            End If
                            'End If



                            VoucherEntryTable.Line_Narration = ""
                            VoucherEntryTable.Line_Chq_No = ""
                            VoucherEntryTable.Line_Chq_Date = ""
                            VoucherEntryTable.Line_TDSCategory = ""
                            VoucherEntryTable.Line_TDSOnAmt = 0
                            VoucherEntryTable.Line_CostCenter = ""
                            VoucherEntryTable.Line_ContraText = ""
                            VoucherEntryTable.Line_OrignalAmt = 0
                            VoucherEntryTable.Line_TDSDeductFrom = ""

                            VoucherEntryTableList(UBound(VoucherEntryTableList)) = VoucherEntryTable
                            ReDim Preserve VoucherEntryTableList(UBound(VoucherEntryTableList) + 1)
                        End If
                    Next

                    Dim V_Type_New As String = ""

                    If AgL.Dman_Execute("SELECT Ag.Nature FROM SubGroup Sg LEFT JOIN AcGroup Ag ON Sg.GroupCode = Ag.GroupCode WHERE Sg.Name = '" & bHeadSubCodeName & "'", AgL.GCn).ExecuteScalar() = "Cash" Then
                        If VoucherEntryTableList(0).V_Type = "BR" Then V_Type_New = "CR"
                        If VoucherEntryTableList(0).V_Type = "BP" Then V_Type_New = "CP"
                    End If


                    For J = 0 To VoucherEntryTableList.Length - 1
                        If VoucherEntryTableList(J).DocId IsNot Nothing Then
                            If bHeadSubCodeName <> "" Then
                                VoucherEntryTableList(J).SubCodeName = bHeadSubCodeName
                                If V_Type_New <> "" Then VoucherEntryTableList(J).V_Type = V_Type_New
                            End If
                        End If
                    Next
                    InsertVoucherEntry(VoucherEntryTableList)
                End If
            Next I
            AgL.ETrans.Commit()
            mTrans = "Commit"

        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub FImportFromTallyLedgerOpening()
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtTemp As New DataTable
        Dim I As Integer = 0
        Dim bHeadSubCodeName As String = ""
        Dim FileNameWithPath As String = ""

        OFDMain.Filter = "*.xml|*.XML"
        If OFDMain.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Exit Sub
        FileNameWithPath = OFDMain.FileName

        Dim doc As New XmlDocument()
        doc.Load(FileNameWithPath)

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            Dim LedgerElementList As XmlNodeList = doc.GetElementsByTagName("LEDGER")

            For I = 0 To LedgerElementList.Count - 1
                If LedgerElementList(I).SelectSingleNode("OPENINGBALANCE") IsNot Nothing Then
                    If LedgerElementList(I).SelectSingleNode("OPENINGBALANCE").ChildNodes.Count > 0 Then
                        Dim VoucherEntryTableList(0) As StructVoucherEntry
                        Dim VoucherEntryTable As New StructVoucherEntry

                        VoucherEntryTable.DocId = ""
                        VoucherEntryTable.V_Type = "OB"
                        VoucherEntryTable.V_Prefix = ""
                        VoucherEntryTable.Site_Code = AgL.PubSiteCode
                        VoucherEntryTable.Div_Code = AgL.PubDivCode
                        VoucherEntryTable.V_No = ""
                        VoucherEntryTable.V_Date = AgL.Dman_Execute("Select Min(Date_From) From Voucher_Prefix Where V_Type = '" & VoucherEntryTable.V_Type & "'", AgL.GCn).ExecuteScalar()
                        VoucherEntryTable.SubCode = ""
                        VoucherEntryTable.Narration = "Opening"
                        VoucherEntryTable.PostedBy = ""
                        VoucherEntryTable.RecId = VoucherEntryTable.V_No
                        VoucherEntryTable.U_Name = AgL.PubUserName
                        VoucherEntryTable.U_EntDt = AgL.GetDateTime(AgL.GcnRead)
                        VoucherEntryTable.U_AE = "A"
                        VoucherEntryTable.PreparedBy = AgL.PubUserName
                        VoucherEntryTable.Line_RecId = VoucherEntryTable.V_No
                        VoucherEntryTable.Line_RecDate = VoucherEntryTable.V_Date
                        VoucherEntryTable.Line_V_SNo = I + 1
                        VoucherEntryTable.Line_V_Date = VoucherEntryTable.V_Date
                        VoucherEntryTable.Line_SubCode = ""
                        VoucherEntryTable.Line_SubCodeName = LedgerElementList(I).Attributes("NAME").Value
                        VoucherEntryTable.Line_ContraSub = ""
                        VoucherEntryTable.Line_ContraSubName = ""


                        If LedgerElementList(I).SelectSingleNode("OPENINGBALANCE").ChildNodes(0).Value < 0 Then
                            VoucherEntryTable.Line_AmtDr = Math.Abs(Convert.ToDecimal(LedgerElementList(I).SelectSingleNode("OPENINGBALANCE").ChildNodes(0).Value))
                            VoucherEntryTable.Line_AmtCr = 0
                        Else
                            VoucherEntryTable.Line_AmtDr = 0
                            VoucherEntryTable.Line_AmtCr = Math.Abs(Convert.ToDecimal(LedgerElementList(I).SelectSingleNode("OPENINGBALANCE").ChildNodes(0).Value))
                        End If


                        VoucherEntryTable.Line_Narration = ""
                        VoucherEntryTable.Line_Chq_No = ""
                        VoucherEntryTable.Line_Chq_Date = ""
                        VoucherEntryTable.Line_TDSCategory = ""
                        VoucherEntryTable.Line_TDSOnAmt = 0
                        VoucherEntryTable.Line_CostCenter = ""
                        VoucherEntryTable.Line_ContraText = ""
                        VoucherEntryTable.Line_OrignalAmt = 0
                        VoucherEntryTable.Line_TDSDeductFrom = ""

                        VoucherEntryTableList(UBound(VoucherEntryTableList)) = VoucherEntryTable
                        ReDim Preserve VoucherEntryTableList(UBound(VoucherEntryTableList) + 1)
                        InsertVoucherEntry(VoucherEntryTableList)
                    End If
                End If
            Next I
            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Shared Function InsertVoucherEntry(VoucherEntryTableList As StructVoucherEntry()) As String
        Dim BlnRtn As Boolean = True
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New Object
        Dim I As Short, IntV_SNo As Integer, IntV_SNo_For_Stock As Integer, Int_Prv_V_SNo As Integer, J As Int16
        Dim Narration As String = "", BlnFlag As Boolean = False
        Dim StrNarrationForHeader As String = ""
        Dim StrContraTextJV As String = "", StrContraTextOther As String = "", StrContraTDS_BF As String = "", StrContraTDS As String = ""
        Dim StrChequeNo As String = "", StrChequeDt As String = "", mQry As String = ""
        Dim Debit_Total As Double = 0, Credit_Total As Double = 0

        '================================================
        '================= For JV =======================
        If UCase(Trim(VoucherEntryTableList(0).V_Type)) = "JV" And UCase(Trim(VoucherEntryTableList(0).V_Type)) <> "OB" Then
            For I = 0 To VoucherEntryTableList.Length - 1
                If Trim(VoucherEntryTableList(I).SubCodeName) <> "" Then
                    If StrContraTextJV <> "" Then StrContraTextJV += vbCrLf
                    If Val(VoucherEntryTableList(I).Line_AmtDr) > 0 Then
                        FPrepareContraText(False, StrContraTextJV, VoucherEntryTableList(I).SubCodeName, VoucherEntryTableList(I).Line_AmtDr, "Dr")
                    Else
                        FPrepareContraText(False, StrContraTextJV, VoucherEntryTableList(I).SubCodeName, VoucherEntryTableList(I).Line_AmtCr, "Cr")
                    End If
                End If
            Next
        End If
        '================================================

        VoucherEntryTableList(0).DocId = AgL.GetDocId(VoucherEntryTableList(0).V_Type, CStr(VoucherEntryTableList(0).V_No), CDate(VoucherEntryTableList(0).V_Date), IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead), AgL.PubDivCode, AgL.PubSiteCode)

        VoucherEntryTableList(0).V_No = Val(AgL.DeCodeDocID(VoucherEntryTableList(0).DocId, AgLibrary.ClsMain.DocIdPart.VoucherNo))

        If VoucherEntryTableList(0).RecId = "" Or VoucherEntryTableList(0).RecId Is Nothing Then
            VoucherEntryTableList(0).RecId = VoucherEntryTableList(0).V_No
        End If


        If VoucherEntryTableList(0).DocId = "" Or VoucherEntryTableList(0).DocId = Nothing Then
            Throw New System.Exception("DocId is not generated for Voucher No." + VoucherEntryTableList(0).V_No.ToString)
        End If

        VoucherEntryTableList(0).V_Prefix = Val(AgL.DeCodeDocID(VoucherEntryTableList(0).DocId, AgLibrary.ClsMain.DocIdPart.VoucherPrefix))


        If AgL.XNull(VoucherEntryTableList(0).SubCode) <> "" Then
            VoucherEntryTableList(0).SubCode = AgL.Dman_Execute("Select SubCode From SubGroup With (NoLock) Where Upper(RTrim(LTrim(Name))) =  " & AgL.Chk_Text(AgL.XNull(VoucherEntryTableList(0).SubCodeName).ToString().Trim.ToUpper) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
        End If

        mQry = "Insert Into LedgerM(DocId,V_Type,v_Prefix,Site_Code, Div_Code,V_No,V_Date,SubCode," &
                "Narration,PostedBy,RecId," &
                "U_Name,U_EntDt,U_AE,PreparedBy) Values " &
                "('" & (VoucherEntryTableList(0).DocId) & "','" & VoucherEntryTableList(0).V_Type & "','" & VoucherEntryTableList(0).V_Prefix & "',
                '" & VoucherEntryTableList(0).Site_Code & "', '" & VoucherEntryTableList(0).Div_Code & "', " &
                "'" & VoucherEntryTableList(0).V_No & "'," & AgL.Chk_Date(VoucherEntryTableList(0).V_Date) & ",
                " & AgL.Chk_Text(VoucherEntryTableList(0).SubCode) & ", " &
                "" & AgL.Chk_Text(VoucherEntryTableList(0).Narration) & ",'" & VoucherEntryTableList(0).PostedBy & "',
                '" & VoucherEntryTableList(0).RecId & "'," &
                "'" & VoucherEntryTableList(0).U_Name & "'," & AgL.Chk_Date(VoucherEntryTableList(0).U_EntDt) & "," &
                "'" & VoucherEntryTableList(0).U_AE & "','" & VoucherEntryTableList(0).PreparedBy & "')"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)




        IntV_SNo_For_Stock = 0
        IntV_SNo = 0
        StrChequeNo = ""
        StrChequeDt = ""

        Dim mLedgerHeadSubCode As String = ""

        For I = 0 To VoucherEntryTableList.Length - 1
            If Trim(VoucherEntryTableList(0).DocId) IsNot Nothing And Trim(VoucherEntryTableList(0).DocId) <> "" Then
                If Trim(VoucherEntryTableList(I).Line_SubCodeName) IsNot Nothing And Trim(VoucherEntryTableList(I).Line_SubCodeName) <> "" Then
                    If Trim(VoucherEntryTableList(I).SubCodeName) <> Trim(VoucherEntryTableList(I).Line_SubCodeName) Then
                        If StrContraTextOther <> "" Then StrContraTextOther += vbCrLf
                        If Val(VoucherEntryTableList(I).Line_AmtDr) > 0 Then
                            FPrepareContraText(False, StrContraTextOther, VoucherEntryTableList(I).Line_SubCodeName, VoucherEntryTableList(I).Line_AmtDr, "Dr")
                        Else
                            FPrepareContraText(False, StrContraTextOther, VoucherEntryTableList(I).Line_SubCodeName, VoucherEntryTableList(I).Line_AmtCr, "Cr")
                        End If

                        'If UCase(Trim(LblCurrentType.Tag)) <> "JV" Then
                        '    If FGMain.Columns(GDebit).Visible Then
                        '        FPrepareContraText(True, StrContraTextJV, VoucherEntryTableList(0).Line_SubCodeName, VoucherEntryTableList(I).Line_AmtDr, "Cr")
                        '    Else
                        '        FPrepareContraText(True, StrContraTextJV, VoucherEntryTableList(0).Line_SubCodeName, VoucherEntryTableList(I).Line_AmtCr, "Dr")
                        '    End If
                        'End If

                        If Trim(VoucherEntryTableList(I).Line_Narration) <> "" Then
                            If StrNarrationForHeader <> "" Then StrNarrationForHeader += vbCrLf
                            StrNarrationForHeader += AgL.Chk_Text(VoucherEntryTableList(I).Line_Narration)
                        End If

                        Dim bRecId As String = VoucherEntryTableList(0).RecId
                        Dim bRecDate As String = VoucherEntryTableList(0).V_Date
                        If VoucherEntryTableList(I).Line_RecId <> "" Then bRecId = VoucherEntryTableList(I).Line_RecId
                        If VoucherEntryTableList(I).Line_RecDate <> "" Then bRecDate = VoucherEntryTableList(I).Line_RecDate

                        If AgL.XNull(VoucherEntryTableList(I).Line_SubCode) <> "" Then
                            VoucherEntryTableList(I).Line_SubCode = AgL.Dman_Execute("Select SubCode From SubGroup Where Upper(RTrim(LTrim(Name))) = " & AgL.Chk_Text(AgL.XNull(VoucherEntryTableList(I).Line_SubCodeName).ToString().Trim.ToUpper) & "", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()
                        End If

                        Debit_Total += Val(VoucherEntryTableList(I).Line_AmtDr)
                        Credit_Total += Val(VoucherEntryTableList(I).Line_AmtCr)

                        IntV_SNo = IntV_SNo + 1
                        mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr," &
                            "Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,CostCenter,ContraText,OrignalAmt,TDSDeductFrom) Values " &
                            "('" & (VoucherEntryTableList(0).DocId) & "','" & bRecId & "'," & IntV_SNo & "," & AgL.Chk_Date(bRecDate) & "," & AgL.Chk_Text(VoucherEntryTableList(I).Line_SubCode) & "," & AgL.Chk_Text(VoucherEntryTableList(0).SubCode) & ", " &
                            "" & Val(VoucherEntryTableList(I).Line_AmtDr) & "," & Val(VoucherEntryTableList(I).Line_AmtCr) & ", " &
                            "" & AgL.Chk_Text(VoucherEntryTableList(I).Line_Narration) & ",'" & VoucherEntryTableList(0).V_Type & "','" & VoucherEntryTableList(0).V_No & "','" & VoucherEntryTableList(0).V_Prefix & "'," &
                            "'" & VoucherEntryTableList(0).Site_Code & "','" & VoucherEntryTableList(0).Div_Code & "'," & AgL.Chk_Text(VoucherEntryTableList(0).Line_Chq_No) & "," &
                            "" & AgL.Chk_Date(VoucherEntryTableList(I).Line_Chq_Date) & "," & AgL.Chk_Text(VoucherEntryTableList(I).Line_TDSCategory) & "," &
                            "" & Val(VoucherEntryTableList(I).Line_TDSOnAmt) & "," & AgL.Chk_Text(VoucherEntryTableList(I).Line_CostCenter) & "," & AgL.Chk_Text(StrContraTextJV) & "," & Val(VoucherEntryTableList(I).Line_OrignalAmt) & "," & AgL.Chk_Text(VoucherEntryTableList(I).Line_TDSDeductFrom) & ")"
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                        Int_Prv_V_SNo = IntV_SNo

                        If Trim(VoucherEntryTableList(I).Line_Chq_No) <> "" Then
                            If Trim(StrChequeNo) = "" Then
                                StrChequeNo = Trim(VoucherEntryTableList(I).Line_Chq_No)
                                StrChequeDt = Trim(VoucherEntryTableList(I).Line_Chq_Date)
                            ElseIf UCase(Trim(StrChequeNo)) <> UCase(Trim(VoucherEntryTableList(I).Line_Chq_No)) Then
                                StrChequeNo = ""
                                StrChequeDt = ""
                            End If
                        End If
                        'SVTMain = DTStruct.Rows(I).Item("SSDB")

                        StrContraTDS = ""
                        If mLedgerHeadSubCode = "" Then mLedgerHeadSubCode = VoucherEntryTableList(I).Line_SubCode
                    End If
                End If
            End If
        Next

        mQry = "INSERT INTO LedgerHead (DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, ManualRefNo,
                    Subcode, DrCr, UptoDate, Remarks, Status, SalesTaxGroupParty, PlaceOfSupply,
                    PartySalesTaxNo, Structure, CustomFields, PartyDocNo, PartyDocDate,
                    EntryBy, EntryDate)
                    Select DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, V_No As ManualRefNo,
                    " & AgL.Chk_Text(mLedgerHeadSubCode) & " As Subcode, Null As DrCr, Null As UptoDate, " & AgL.Chk_Text(VoucherEntryTableList(0).Remark) & " As Remarks, Null As Status, Null As SalesTaxGroupParty, 
                    Null As PlaceOfSupply,
                    Null As PartySalesTaxNo, Null As Structure, Null As CustomFields, Null As PartyDocNo, Null As PartyDocDate,
                    U_Name As EntryBy, U_EntDt As EntryDate
                    From LedgerM Where DocId = '" & VoucherEntryTableList(0).DocId & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        If UCase(Trim(VoucherEntryTableList(0).V_Type)) <> "JV" And UCase(Trim(VoucherEntryTableList(0).V_Type)) <> "OB" Then
            If StrNarrationForHeader.Length > 255 Then StrNarrationForHeader = StrNarrationForHeader.Substring(0, 254)
            IntV_SNo = IntV_SNo + 1
            If StrChequeDt <> "" Then StrChequeDt = CDate(StrChequeDt).ToString("s")
            mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr," &
                    "Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,ContraText,Chq_No,Chq_Date) Values " &
                    "('" & (VoucherEntryTableList(0).DocId) & "','" & VoucherEntryTableList(0).RecId & "'," & IntV_SNo & ",
                    " & AgL.Chk_Date(VoucherEntryTableList(0).V_Date) & "," & AgL.Chk_Text(VoucherEntryTableList(0).SubCode) & ",
                    " & AgL.Chk_Text("") & ", " &
                    "" & IIf(Credit_Total > 0, Val(Credit_Total), 0) & "," &
                    "" & IIf(Debit_Total > 0, Val(Debit_Total), 0) & ", " &
                    "" & AgL.Chk_Text(StrNarrationForHeader) & ",'" & VoucherEntryTableList(0).V_Type & "','" & VoucherEntryTableList(0).V_No & "'," &
                    "'" & VoucherEntryTableList(0).V_Prefix & "',
                    '" & VoucherEntryTableList(0).Site_Code & "','" & VoucherEntryTableList(0).Div_Code & "',
                    " & AgL.Chk_Text(StrContraTextOther) & ",'" & StrChequeNo & "'," & AgL.Chk_Date(StrChequeDt) & ")"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If

        '======================== For Posted By Updation ======================
        mQry = "Update LedgerM Set "
        mQry = mQry + "PostedBy='" & VoucherEntryTableList(0).PostedBy & "' "
        mQry = mQry + "Where DocId='" & (VoucherEntryTableList(0).DocId) & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "Update voucher_prefix set start_srl_no = " & Val(VoucherEntryTableList(0).V_No) & " 
            where v_type = " & AgL.Chk_Text(VoucherEntryTableList(0).V_Type) & " and prefix=" & AgL.Chk_Text(VoucherEntryTableList(0).V_Prefix) & ""
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Return VoucherEntryTableList(0).DocId
    End Function


    Public Structure StructVoucherEntry
        ''''''''''''''''''''''''''''LedgerM''''''''''''''''''''''''''''''''''''''''''
        Dim DocId As String
        Dim V_Type As String
        Dim V_Prefix As String
        Dim Site_Code As String
        Dim Div_Code As String
        Dim V_No As String
        Dim V_Date As String
        Dim SubCode As String
        Dim SubCodeName As String
        Dim Narration As String
        Dim PostedBy As String
        Dim RecId As String
        Dim U_Name As String
        Dim U_EntDt As String
        Dim U_AE As String
        Dim PreparedBy As String
        Dim Remark As String

        '''''''''''''''''''''''''''Ledger'''''''''''''''''''''''''''''''''''''''''''''
        Dim Line_RecId As String
        Dim Line_RecDate As String
        Dim Line_V_SNo As String
        Dim Line_V_Date As String
        Dim Line_SubCode As String
        Dim Line_SubCodeName As String
        Dim Line_ContraSub As String
        Dim Line_ContraSubName As String
        Dim Line_AmtDr As Double
        Dim Line_AmtCr As Double
        Dim Line_Narration As String
        Dim Line_Chq_No As String
        Dim Line_Chq_Date As String
        Dim Line_TDSCategory As String
        Dim Line_TDSOnAmt As String
        Dim Line_CostCenter As String
        Dim Line_ContraText As String
        Dim Line_OrignalAmt As String
        Dim Line_TDSDeductFrom
    End Structure
    Public Sub FImportFromExcel_Old()
        Dim mQry As String = ""
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtFile1 As DataTable
        Dim DtSaleInvoiceDimensionDetail As DataTable
        Dim DtMain As DataTable = Nothing

        Dim I As Integer
        Dim J As Integer
        Dim K As Integer
        Dim M As Integer
        Dim N As Integer
        'Dim FW As System.IO.StreamWriter = New System.IO.StreamWriter("C:\ImportLog.Txt", False, System.Text.Encoding.Default)
        Dim StrErrLog As String = ""

        mQry = "Select '' as Srl, 'V_TYPE' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'V_NO' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'V_Date' as [Field Name], 'Date' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Ledger Account Name' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Contra Ledger Account Name' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Narration' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Chq No' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Chq Date' as [Field Name], 'Date' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Amt Dr' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'Amt Cr' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        DtFile1 = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim ObjFrmImport As New FrmImportFromExcel
        ObjFrmImport.Text = "Voucher Entry Import From Excel"
        ObjFrmImport.Dgl1.DataSource = DtFile1
        ObjFrmImport.StartPosition = FormStartPosition.CenterScreen
        ObjFrmImport.ShowDialog()

        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

        DtFile1 = ObjFrmImport.P_DsExcelData.Tables(0)


        Dim DtV_Type = DtFile1.DefaultView.ToTable(True, "V_Type")
        For I = 0 To DtV_Type.Rows.Count - 1
            If AgL.XNull(DtV_Type.Rows(I)("V_Type")) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From Voucher_TYpe where V_Type = '" & AgL.XNull(DtV_Type.Rows(I)("V_Type")) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Voucher Types Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Voucher Types Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtV_Type.Rows(I)("V_Type")) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtV_Type.Rows(I)("V_Type")) & ", "
                    End If
                End If
            End If
        Next

        Dim DtSaleToParty = DtFile1.DefaultView.ToTable(True, "Sale To Party")
        For I = 0 To DtSaleToParty.Rows.Count - 1
            If AgL.XNull(DtSaleToParty.Rows(I)("Sale To Party")) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From SubGroup where Name = '" & AgL.XNull(DtSaleToParty.Rows(I)("Sale To Party")) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Parties Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Parties Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtSaleToParty.Rows(I)("Sale To Party")) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtSaleToParty.Rows(I)("Sale To Party")) & ", "
                    End If
                End If
            End If
        Next

        Dim DtBillToParty = DtFile1.DefaultView.ToTable(True, "Bill To Party")
        For I = 0 To DtBillToParty.Rows.Count - 1
            If AgL.XNull(DtBillToParty.Rows(I)("Bill To Party")) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From SubGroup where Name = '" & AgL.XNull(DtBillToParty.Rows(I)("Bill To Party")) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Parties Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Parties Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtBillToParty.Rows(I)("Bill To Party")) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtBillToParty.Rows(I)("Bill To Party")) & ", "
                    End If
                End If
            End If
        Next

        Dim DtAgent = DtFile1.DefaultView.ToTable(True, "Agent")
        For I = 0 To DtAgent.Rows.Count - 1
            If AgL.XNull(DtAgent.Rows(I)("Agent")) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From SubGroup where Name = '" & AgL.XNull(DtAgent.Rows(I)("Agent")) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Agents Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Agents Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtAgent.Rows(I)("Agent")) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtAgent.Rows(I)("Agent")) & ", "
                    End If
                End If
            End If
        Next

        Dim DtRateType = DtFile1.DefaultView.ToTable(True, "Rate Type")
        For I = 0 To DtRateType.Rows.Count - 1
            If AgL.XNull(DtRateType.Rows(I)("Rate Type")) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From RateTYpe where Description = '" & AgL.XNull(DtRateType.Rows(I)("Rate Type")) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Rate Types Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Rate Types Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtRateType.Rows(I)("Rate Type")) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtRateType.Rows(I)("Rate Type")) & ", "
                    End If
                End If
            End If
        Next

        Dim DtSalesTaxGroupParty = DtFile1.DefaultView.ToTable(True, "Sales Tax Group Party")
        For I = 0 To DtSalesTaxGroupParty.Rows.Count - 1
            If AgL.XNull(DtSalesTaxGroupParty.Rows(I)("Sales Tax Group Party")) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From PostingGroupSalesTaxParty where Description = '" & AgL.XNull(DtSalesTaxGroupParty.Rows(I)("Sales Tax Group Party")) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Sales Tax Group Parties Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Sales Tax Group Parties Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtSalesTaxGroupParty.Rows(I)("Sales Tax Group Party")) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtSalesTaxGroupParty.Rows(I)("Sales Tax Group Party")) & ", "
                    End If
                End If
            End If
        Next




        For I = 0 To DtFile1.Rows.Count - 1
            If AgL.XNull(DtFile1.Rows(I)("Sale To Party")) = "" Then
                ErrorLog += "Sale To Party is blank at row no." + (I + 2).ToString() & vbCrLf
            End If

            If AgL.XNull(DtFile1.Rows(I)("Bill To Party")) = "" Then
                ErrorLog += "Bill To Party is blank at row no." + (I + 2).ToString() & vbCrLf
            End If

            If AgL.XNull(DtFile1.Rows(I)("Sales Tax Group Party")) = "" Then
                ErrorLog += "Sales Tax Group Party is blank at row no." + (I + 2).ToString() & vbCrLf
            End If

            If AgL.XNull(DtFile1.Rows(I)("V_Date")) = "" Then
                ErrorLog += "V_Date is blank at row no." + (I + 2).ToString() & vbCrLf
            End If

            If AgL.XNull(DtFile1.Rows(I)("V_Type")) = "" Then
                ErrorLog += "V_Type is blank at row no." + (I + 2).ToString() & vbCrLf
            End If
        Next

        Dim DtItem = DtFile1.DefaultView.ToTable(True, "Item Name")
        For I = 0 To DtItem.Rows.Count - 1
            If AgL.XNull(DtItem.Rows(I)("Item Name")) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From Item where Description = '" & AgL.XNull(DtItem.Rows(I)("Item Name")) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Item Names Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Item Names Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtItem.Rows(I)("Item Name")) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtItem.Rows(I)("Item Name")) & ", "
                    End If
                End If
            End If
        Next

        Dim DtSalesTaxGroupItem = DtFile1.DefaultView.ToTable(True, "Sales Tax Group Item")
        For I = 0 To DtSalesTaxGroupItem.Rows.Count - 1
            If AgL.XNull(DtSalesTaxGroupItem.Rows(I)("Sales Tax Group Item")) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From PostingGroupSalesTaxItem where Description = '" & AgL.XNull(DtSalesTaxGroupItem.Rows(I)("Sales Tax Group Item")) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These SalesTaxGroupItems Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These SalesTaxGroupItems Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtSalesTaxGroupItem.Rows(I)("Sales Tax Group Item")) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtSalesTaxGroupItem.Rows(I)("Sales Tax Group Item")) & ", "
                    End If
                End If
            End If
        Next

        For I = 0 To DtFile1.Rows.Count - 1
            If AgL.XNull(DtFile1.Rows(I)("Item Name")) = "" Then
                ErrorLog += "Item Name is blank at row no." + (I + 2).ToString() & vbCrLf
            End If

            If AgL.XNull(DtFile1.Rows(I)("Sales Tax Group Item")) = "" Then
                ErrorLog += "Sales Tax Group Item is blank at row no." + (I + 2).ToString() & vbCrLf
            End If
        Next

        If ErrorLog <> "" Then
            If File.Exists(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt") Then
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt", ErrorLog, False)
            Else
                File.Create(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt")
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt", ErrorLog, False)
            End If
            System.Diagnostics.Process.Start("notepad.exe", My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt")
            Exit Sub
        End If

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"


            For I = 0 To DtFile1.Rows.Count - 1
                Dim mDocId = AgL.GetDocId(AgL.XNull(DtFile1.Rows(I)("V_Type")), CStr(DtFile1.Rows(I)("V_Type")), CDate(AgL.XNull(DtFile1.Rows(I)("V_Date"))),
                                          AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode)

                Dim mV_No As String = Val(AgL.DeCodeDocID(mDocId, AgLibrary.ClsMain.DocIdPart.VoucherNo))
                Dim mV_Prefix As String = AgL.DeCodeDocID(mDocId, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)

                Dim mSaleToParty As String = ""
                Dim mSaleToPartyName As String = ""
                Dim mSaleToPartyAddress As String = ""
                Dim mSaleToPartyCity As String = ""
                Dim mSaleToPartyMobile As String = ""
                Dim mSaleToPartySalesTaxNo As String = ""

                mQry = "SELECT Sg.SubCode As SaleToParty, Name As SaleToPartyName, Address As SaleToPartyAddress, CityCode As SaleToPartyCity, Mobile As SaleToPartyMobile, Sgr.RegistrationNo As SaleToPartySalesTaxNo
                        FROM Subgroup Sg
                        left join (Select SubCode, RegistrationNo From SubgroupRegistration Where RegistrationType = 'GSTN No') As Sgr On Sg.Subcode = Sgr.Subcode
                        Where Sg.Name =  '" & AgL.XNull(DtFile1.Rows(I)("Sale To Party")) & "'"
                Dim DtAcGroup As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If (DtAcGroup.Rows.Count > 0) Then
                    mSaleToParty = AgL.XNull(DtAcGroup.Rows(0)("SaleToParty"))
                    mSaleToPartyName = AgL.XNull(DtAcGroup.Rows(0)("SaleToPartyName"))
                    mSaleToPartyAddress = AgL.XNull(DtAcGroup.Rows(0)("SaleToPartyAddress"))
                    mSaleToPartyCity = AgL.XNull(DtAcGroup.Rows(0)("SaleToPartyCity"))
                    mSaleToPartyMobile = AgL.XNull(DtAcGroup.Rows(0)("SaleToPartyMobile"))
                    mSaleToPartySalesTaxNo = AgL.XNull(DtAcGroup.Rows(0)("SaleToPartySalesTaxNo"))
                End If



                Dim mBillToParty As String = AgL.Dman_Execute("SELECT Sg.SubCode As BillToParty
                        FROM Subgroup Sg
                        Where Sg.Name =  '" & AgL.XNull(DtFile1.Rows(I)("Bill To Party")) & "'", AgL.GCn).ExecuteScalar()

                If AgL.Dman_Execute("SELECT Count(*) From SaleInvoice where V_Type = '" & AgL.XNull(DtFile1.Rows(I)("V_Type")) & "' And ReferenceNo = '" & AgL.XNull(DtFile1.Rows(I)("Manual Ref No")) & "' ", AgL.GCn).ExecuteScalar = 0 Then
                    mQry = " INSERT INTO SaleInvoice (DocID,  V_Type,  V_Prefix, V_Date,  V_No,  Div_Code,  Site_Code,
                             ReferenceNo,  SaleToParty,  BillToParty,  Agent, SaleToPartyName,  SaleToPartyAddress,
                             SaleToPartyCity,  SaleToPartyMobile, SaleToPartySalesTaxNo,  ShipToAddress,
                             RateType,  SalesTaxGroupParty, PlaceOfSupply,  Structure,
                             CustomFields,  SaleToPartyDocNo, SaleToPartyDocDate,  ReferenceDocId,
                             Remarks,  TermsAndConditions, Gross_Amount,  Taxable_Amount,
                             Tax1_Per,  Tax1,  Tax2_Per, Tax2,  Tax3_Per,  Tax3,
                             Tax4_Per,  Tax4,  Tax5_Per, Tax5,  SubTotal1,  Deduction_Per,
                             Deduction,  Other_Charge_Per,  Other_Charge, Round_Off,  Net_Amount,  PaidAmt,
                             CreditLimit,  CreditDays,  Status, EntryBy,  EntryDate,  ApproveBy,
                             ApproveDate,  MoveToLog,  MoveToLogDate, UploadDate)
                             Select  " & AgL.Chk_Text(mDocId) & ",  
                             " & AgL.Chk_Text(AgL.XNull(DtFile1.Rows(I)("V_Type"))) & ",  
                             " & AgL.Chk_Text(mV_Prefix) & ",  
                             " & AgL.Chk_Date(AgL.XNull(DtFile1.Rows(I)("V_Date"))) & ",  
                             " & AgL.Chk_Text(mV_No) & ",  
                             " & AgL.Chk_Text(AgL.PubDivCode) & ",
                             " & AgL.Chk_Text(AgL.PubSiteCode) & ",  " & AgL.Chk_Text(AgL.XNull(DtFile1.Rows(I)("Manual Ref No"))) & ",  
                             " & AgL.Chk_Text(mSaleToParty) & ", 
                             (SELECT SubCode  From SubGroup WHERE Name = '" & AgL.XNull(DtFile1.Rows(I)("Bill To Party")) & "') As BillToParty,
                             (SELECT SubCode  From SubGroup WHERE Name = '" & AgL.XNull(DtFile1.Rows(I)("Agent")) & "') As Agent,
                             " & AgL.Chk_Text(mSaleToPartyName) & ",
                             " & AgL.Chk_Text(mSaleToPartyAddress) & ",  " & AgL.Chk_Text(mSaleToPartyCity) & ",  
                             " & AgL.Chk_Text(mSaleToPartyMobile) & ", " & AgL.Chk_Text(mSaleToPartySalesTaxNo) & ",  
                             " & AgL.Chk_Text(AgL.XNull(DtFile1.Rows(I)("Ship To Address"))) & ",  
                             (SELECT Code  From RateType Where Description = '" & AgL.XNull(DtFile1.Rows(I)("Rate Type")) & "') As RateType,
                             '" & AgL.XNull(DtFile1.Rows(I)("Sales Tax Group Party")) & "' As SalesTaxGroupParty,
                             " & AgL.Chk_Text(AgL.XNull(DtFile1.Rows(I)("Place Of Supply"))) & ",  
                             (Select IfNull(Max(Structure),'') From Voucher_Type Where V_Type = '" & AgL.XNull(DtFile1.Rows(I)("V_Type")) & "') As Structure, 
                             Null As CustomFields,  
                              " & AgL.Chk_Text(AgL.XNull(DtFile1.Rows(I)("Sale To Party Doc No"))) & ",  
                              " & AgL.Chk_Date(AgL.XNull(DtFile1.Rows(I)("Sale To Party Doc Date"))) & ",  
                              Null As ReferenceDocId,  " & AgL.Chk_Text(AgL.XNull(DtFile1.Rows(I)("Remark"))) & ",  
                              " & AgL.Chk_Text(AgL.XNull(DtFile1.Rows(I)("Terms And Conditions"))) & ", 
                              " & AgL.VNull(DtFile1.Rows(I)("Gross Amount")) & ",  
                              " & AgL.VNull(DtFile1.Rows(I)("Taxable_Amount")) & ",  
                              " & AgL.VNull(DtFile1.Rows(I)("Tax1_Per")) & " As Tax1_Per,
                              " & AgL.VNull(DtFile1.Rows(I)("Tax1")) & " As Tax1,  
                              " & AgL.VNull(DtFile1.Rows(I)("Tax2_Per")) & " As Tax2_Per,  
                              " & AgL.VNull(DtFile1.Rows(I)("Tax2")) & " As Tax2, 
                              " & AgL.VNull(DtFile1.Rows(I)("Tax3_Per")) & " As Tax3_Per,  
                              " & AgL.VNull(DtFile1.Rows(I)("Tax3")) & " As Tax3,  
                              " & AgL.VNull(DtFile1.Rows(I)("Tax4_Per")) & " As Tax4_Per,
                              " & AgL.VNull(DtFile1.Rows(I)("Tax4")) & " As Tax4,  
                              " & AgL.VNull(DtFile1.Rows(I)("Tax5_Per")) & " As Tax5_Per,  
                              " & AgL.VNull(DtFile1.Rows(I)("Tax5")) & " As Tax5, 
                              " & AgL.VNull(DtFile1.Rows(I)("SubTotal1")) & " As SubTotal1,  
                              " & AgL.VNull(DtFile1.Rows(I)("Deduction_Per")) & " As Deduction_Per,  
                              " & AgL.VNull(DtFile1.Rows(I)("Deduction")) & " As Deduction,
                              " & AgL.VNull(DtFile1.Rows(I)("Other_Charge_Per")) & " As Other_Charge_Per,  
                              " & AgL.VNull(DtFile1.Rows(I)("Other_Charge")) & " As Other_Charge,  
                              " & AgL.VNull(DtFile1.Rows(I)("Round_Off")) & " As Round_Off, 
                              " & AgL.VNull(DtFile1.Rows(I)("Net_Amount")) & " As Net_Amount,  
                              0 As PaidAmt,  
                              " & AgL.VNull(DtFile1.Rows(I)("Credit Limit")) & " As CreditLimit,
                              " & AgL.VNull(DtFile1.Rows(I)("Credit Days")) & " As CreditDays,  
                              'Active' As Status,  
                              " & AgL.Chk_Text(AgL.PubUserName) & " As EntryBy, 
                              " & AgL.Chk_Date(AgL.PubLoginDate) & "  As EntryDate,  
                              Null As ApproveBy,  Null As ApproveDate,
                              Null As MoveToLog,  Null As MoveToLogDate,  Null As UploadDate"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)



                    Dim DtSaleInvoiceDetail_ForHeader As New DataTable
                    For M = 0 To DtFile1.Columns.Count - 1
                        Dim DColumn As New DataColumn
                        DColumn.ColumnName = DtFile1.Columns(M).ColumnName
                        DtSaleInvoiceDetail_ForHeader.Columns.Add(DColumn)
                    Next

                    Dim DtRowSaleInvoiceDetail_ForHeader As DataRow() = DtFile1.Select("V_Type = " + AgL.Chk_Text(AgL.XNull(DtFile1.Rows(I)("V_Type"))) + " And [Manual Ref No] = " + AgL.Chk_Text(AgL.XNull(DtFile1.Rows(I)("Manual Ref No"))))
                    If DtRowSaleInvoiceDetail_ForHeader.Length > 0 Then
                        For M = 0 To DtRowSaleInvoiceDetail_ForHeader.Length - 1
                            DtSaleInvoiceDetail_ForHeader.Rows.Add()
                            For N = 0 To DtSaleInvoiceDetail_ForHeader.Columns.Count - 1
                                DtSaleInvoiceDetail_ForHeader.Rows(M)(N) = DtRowSaleInvoiceDetail_ForHeader(M)(N)
                            Next
                        Next
                    End If

                    For J = 0 To DtSaleInvoiceDetail_ForHeader.Rows.Count - 1
                        mQry = "Insert Into SaleInvoiceDetail(DocId, Sr, Item, Specification, SalesTaxGroupItem, 
                           DocQty, FreeQty, Qty, Unit, Pcs, UnitMultiplier, DealUnit, 
                           DocDealQty, Rate, DiscountPer, DiscountAmount, AdditionalDiscountPer, AdditionalDiscountAmount,  
                           Amount, Remark, BaleNo, LotNo,  
                           ReferenceDocId, ReferenceDocIdSr, 
                           SaleInvoice, SaleInvoiceSr, V_Nature, GrossWeight, NetWeight, Gross_Amount, Taxable_Amount,
                           Tax1_Per, Tax1, Tax2_Per, Tax2, Tax3_Per, Tax3, Tax4_Per, Tax4, Tax5_Per, Tax5, SubTotal1, Deduction_Per, 
                           Deduction, Other_Charge_Per, Other_Charge, Round_Off, Net_Amount)
                           Select " & AgL.Chk_Text(mDocId) & ", " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("TSr")) & ", " &
                            " (SELECT Code From Item WHERE Description = '" & AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Item Name")) & "') As Item, " &
                            " " & AgL.Chk_Text(AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Specification"))) & ", " &
                            " " & AgL.Chk_Text(AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Sales Tax Group Item"))) & ", " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Doc Qty")) & ", " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Free Qty")) & ", " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Qty")) & ", " &
                            " " & AgL.Chk_Text(AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Unit"))) & ", " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Pcs")) & ", " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Unit Multiplier")) & ", " &
                            " " & AgL.Chk_Text(AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Deal Unit"))) & ", " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Doc Deal Qty")) & ", " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Rate")) & ", " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Discount Per")) & ", " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Discount Amount")) & ", " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Additional Discount Per")) & ", " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Additional Discount Amount")) & ", " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Amount")) & ", " &
                            " " & AgL.Chk_Text(AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Remark"))) & ", " &
                            " " & AgL.Chk_Text(AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Bale No"))) & ", " &
                            " " & AgL.Chk_Text(AgL.XNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Lot No"))) & ", " &
                            " Null As ReferenceDocId, " &
                            " Null As ReferenceDocIdSr, " &
                            " " & AgL.Chk_Text(mDocId) & " As SaleInvoice, " &
                            " " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("TSr")) & " As Sr, " &
                            " 'Invoice' As V_Nature,
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Gross Weight")) & ", " & "
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Net Weight")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Gross_Amount")) & ", " & "
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Taxable_Amount")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax1_Per")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax1")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax2_Per")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax2")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax3_Per")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax3")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax4_Per")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax4")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax5_Per")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Tax5")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("SubTotal1")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Deduction_Per")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Deduction")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Other_Charge_Per")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Other_Charge")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Round_Off")) & ", 
                            " & AgL.VNull(DtSaleInvoiceDetail_ForHeader.Rows(J)("Net_Amount")) & ""
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)






                        Dim DtSaleInvoiceDimensionDetail_ForHeader As New DataTable
                        For M = 0 To DtSaleInvoiceDimensionDetail.Columns.Count - 1
                            Dim DColumn As New DataColumn
                            DColumn.ColumnName = DtSaleInvoiceDimensionDetail.Columns(M).ColumnName
                            DtSaleInvoiceDimensionDetail_ForHeader.Columns.Add(DColumn)
                        Next

                        Dim DtRowSaleInvoiceDimensionDetail_ForHeader As DataRow() = DtSaleInvoiceDimensionDetail.Select("V_Type = " + AgL.Chk_Text(AgL.XNull(DtFile1.Rows(J)("V_Type"))) + " And [Manual Ref No] = " + AgL.Chk_Text(AgL.XNull(DtFile1.Rows(J)("Manual Ref No"))) + " And TSr = " + AgL.XNull(DtFile1.Rows(J)("TSr")), "TSr")
                        If DtRowSaleInvoiceDimensionDetail_ForHeader.Length > 0 Then
                            For M = 0 To DtRowSaleInvoiceDetail_ForHeader.Length - 1
                                'DtSaleInvoiceDimensionDetail_ForHeader.Rows.Add(DtRowSaleInvoiceDimensionDetail_ForHeader(M))
                                DtSaleInvoiceDetail_ForHeader.Rows.Add()
                                For N = 0 To DtSaleInvoiceDimensionDetail_ForHeader.Columns.Count - 1
                                    DtSaleInvoiceDimensionDetail_ForHeader.Rows(M)(N) = DtRowSaleInvoiceDimensionDetail_ForHeader(M)(N)
                                Next
                            Next
                        End If




                        For K = 0 To DtSaleInvoiceDimensionDetail_ForHeader.Rows.Count - 1
                            mQry = " INSERT INTO SaleInvoiceDimensionDetail (DocID, TSr, SR, Specification, Pcs, Qty, TotalQty) 
                                    Select " & AgL.Chk_Text(mDocId) & ", 
                                    " & AgL.VNull(DtSaleInvoiceDimensionDetail_ForHeader.Rows(K)("TSr")) & " As Sr, 
                                    " & (K + 1) & ", 
                                    " & AgL.Chk_Text(AgL.XNull(DtSaleInvoiceDimensionDetail_ForHeader.Rows(K)("Specification"))) & ", 
                                    " & AgL.VNull(DtSaleInvoiceDimensionDetail_ForHeader.Rows(K)("Pcs")) & ", 
                                    " & AgL.VNull(DtSaleInvoiceDimensionDetail_ForHeader.Rows(K)("Qty")) & ", 
                                    " & AgL.VNull(DtSaleInvoiceDimensionDetail_ForHeader.Rows(K)("TotalQty")) & " "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                        Next
                    Next

                    mQry = "Insert Into Stock(DocID, TSr, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, 
                                  SubCode, SalesTaxGroupParty,  Item,  LotNo, 
                                  EType_IR, Qty_Iss, Qty_Rec, Unit, UnitMultiplier, DealQty_Iss , DealQty_Rec, DealUnit, 
                                  ReferenceDocID, ReferenceDocIDSr, Rate, Amount, Landed_Value) 
                                  Select L.DocId, L.Sr, L.Sr, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.ReferenceNo, 
                                  H.Div_Code, H.Site_Code, H.SaleToParty,  H.SalesTaxGroupParty,  L.Item,
                                  L.LotNo, 'I', 
                                  Case When  IfNull(L.Qty,0) >= 0 Then L.Qty Else 0 End As Qty_Iss, 
                                  Case When  IfNull(L.Qty,0) < 0 Then L.Qty Else 0 End As Qty_Rec, 
                                  L.Unit, L.UnitMultiplier, 
                                  Case When  IfNull(L.DealQty,0) >= 0 Then L.DealQty Else 0 End As DealQty_Iss, 
                                  Case When  IfNull(L.DealQty,0) < 0 Then L.DealQty Else 0 End As DealQty_Rec, 
                                  L.DealUnit,  
                                  L.ReferenceDocId, L.ReferenceDocIdSr, 
                                  L.Amount/L.Qty, L.Amount, L.Amount
                                  FROM SaleInvoiceDetail L    
                                  LEFT JOIN SaleInvoice H On L.DocId = H.DocId 
                                  WHERE L.DocId =  '" & mDocId & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


                    AgL.UpdateVoucherCounter(mDocId, CDate(AgL.XNull(DtFile1.Rows(I)("V_Date"))), AgL.GCn, AgL.ECmd, AgL.PubDivCode, AgL.PubSiteCode)
                End If
            Next

            AgL.ETrans.Commit()
            mTrans = "Commit"

        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
        If StrErrLog <> "" Then MsgBox(StrErrLog)


        For I = 0 To DTMaster.Rows.Count - 1
            BMBMaster.Position = I
            MoveRec()
        Next
    End Sub
    Public Sub FImportFromExcel(bImportFor As ImportFor, IsOpening As Boolean)
        Dim mQry As String = ""
        Dim bHeadSubCodeName As String = ""
        Dim mTrans As String = ""
        Dim ErrorLog As String = ""
        Dim DtPurchInvoice As DataTable = Nothing
        Dim DtPurchInvoice_DataFields As DataTable
        Dim DtLedger As DataTable
        Dim DtLedger_DataFields As DataTable
        Dim DtMain As DataTable = Nothing

        Dim I As Integer
        Dim J As Integer
        Dim K As Integer
        Dim M As Integer
        Dim N As Integer
        Dim StrErrLog As String = ""


        mQry = "Select '' as Srl, '" & GetFieldAliasName(bImportFor, "V_TYPE") & "' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "V_NO") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "V_Date") & "' as [Field Name], 'Date' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Vendor Name") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        DtPurchInvoice_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = "Select '' as Srl, '" & GetFieldAliasName(bImportFor, "V_TYPE") & "' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "V_NO") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "V_Date") & "' as [Field Name], 'Date' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Ledger Account Name") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Contra Ledger Account Name") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Narration") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Chq No") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Chq Date") & "' as [Field Name], 'Date' as [Data Type], Null as [Length], '' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Amt Dr") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Amt Cr") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
        DtLedger_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim ObjFrmImport As Object
        If IsOpening = True Then
            ObjFrmImport = New FrmImportVoucherFromExcel
            ObjFrmImport.Dgl1.DataSource = DtLedger_DataFields
            ObjFrmImport.Dgl2.DataSource = DtPurchInvoice_DataFields
        Else
            ObjFrmImport = New FrmImportFromExcel
            ObjFrmImport.Dgl1.DataSource = DtLedger_DataFields
        End If

        ObjFrmImport.Text = "Voucher Entry Import"
        ObjFrmImport.Dgl1.DataSource = DtLedger_DataFields
        ObjFrmImport.StartPosition = FormStartPosition.CenterScreen
        ObjFrmImport.ShowDialog()

        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

        If IsOpening = True Then
            DtLedger = ObjFrmImport.P_DsExcelData_PurchInvoice.Tables(0)
            DtPurchInvoice = ObjFrmImport.P_DsExcelData_PurchInvoiceDetail.Tables(0)
        Else
            DtLedger = ObjFrmImport.P_DsExcelData.Tables(0)
        End If


        If bImportFor = ImportFor.Dos Then
            Dim DtV_TypeListInFile As DataTable = DtLedger.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "V_Type"))
            For I = 0 To DtV_TypeListInFile.Rows.Count - 1
                If AgL.XNull(DtV_TypeListInFile.Rows(I)("V_Type")).ToString.Trim <> "CO" And
                        AgL.XNull(DtV_TypeListInFile.Rows(I)("V_Type")).ToString.Trim <> "DO" And
                        AgL.XNull(DtV_TypeListInFile.Rows(I)("V_Type")).ToString.Trim <> "OO" And
                        AgL.XNull(DtV_TypeListInFile.Rows(I)("V_Type")).ToString.Trim <> "FR" And
                        AgL.XNull(DtV_TypeListInFile.Rows(I)("V_Type")).ToString.Trim <> "J" And
                        AgL.XNull(DtV_TypeListInFile.Rows(I)("V_Type")).ToString.Trim <> "ZR" And
                        AgL.XNull(DtV_TypeListInFile.Rows(I)("V_Type")).ToString.Trim <> "ZD" And
                        AgL.XNull(DtV_TypeListInFile.Rows(I)("V_Type")).ToString.Trim <> "ZC" And
                        AgL.XNull(DtV_TypeListInFile.Rows(I)("V_Type")).ToString.Trim <> "MP" And
                        AgL.XNull(DtV_TypeListInFile.Rows(I)("V_Type")).ToString.Trim <> "XC" And
                        AgL.XNull(DtV_TypeListInFile.Rows(I)("V_Type")).ToString.Trim <> "BP" And
                        AgL.XNull(DtV_TypeListInFile.Rows(I)("V_Type")).ToString.Trim <> "BR" And
                        AgL.XNull(DtV_TypeListInFile.Rows(I)("V_Type")).ToString.Trim <> "PR" And
                        AgL.XNull(DtV_TypeListInFile.Rows(I)("V_Type")).ToString.Trim <> "CP" And
                        AgL.XNull(DtV_TypeListInFile.Rows(I)("V_Type")).ToString.Trim <> "CR" Then
                    If ErrorLog.Contains("These Voucher_Types are not Considered") = False Then
                        ErrorLog += vbCrLf & "These Voucher_Types are not Considered" & vbCrLf
                        ErrorLog += AgL.XNull(DtV_TypeListInFile.Rows(I)(GetFieldAliasName(bImportFor, "V_Type"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtV_TypeListInFile.Rows(I)(GetFieldAliasName(bImportFor, "V_Type"))) & ", "
                    End If
                End If
            Next


            ''''''''''''''For Filtering Data To Import In This Entry'''''''''''''''''''''''''''''''''''
            Dim DtLedger_Filtered As New DataTable
            Dim DtLedgerRows_Filtered As DataRow()
            DtLedger_Filtered = DtLedger.Clone
            If IsOpening = True Then
                DtLedgerRows_Filtered = DtLedger.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] In ('CO','DO')")
            Else
                DtLedgerRows_Filtered = DtLedger.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] In ('OO','FR','XC','J','BP','BR','CP','CR')")
            End If
            For I = 0 To DtLedgerRows_Filtered.Length - 1
                DtLedger_Filtered.ImportRow(DtLedgerRows_Filtered(I))
            Next
            DtLedger = DtLedger_Filtered
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            DtLedger.Columns.Add("File_V_Type")


            For I = 0 To DtLedger.Rows.Count - 1
                DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "File_V_Type")) = DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim
                If DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "CO" Then
                    DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "OB"
                ElseIf DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "DO" Then
                    DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "OB"
                ElseIf DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "OO" Then
                    DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "OB"
                ElseIf DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "FR" Then
                    DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "JV"
                ElseIf DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "J" Then
                    DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "JV"
                ElseIf DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "XC" Then
                    DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "JV"
                End If

                If DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name")).ToString().Trim() = "CASH A/C." Then
                    DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name")) = "CASH A/C"
                End If

                If DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name")).ToString().Trim() = "CASH A/C." Then
                    DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name")) = "CASH A/C"
                End If
            Next
        End If




        Dim DtV_Date = DtLedger.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "V_Date"))
        For I = 0 To DtV_Date.Rows.Count - 1
            If AgL.XNull(DtV_Date.Rows(I)(GetFieldAliasName(bImportFor, "V_Date"))) <> "" Then
                If CDate(AgL.XNull(DtV_Date.Rows(I)(GetFieldAliasName(bImportFor, "V_Date")))).Year < "2000" Then
                    If ErrorLog.Contains("These Dates are not valid") = False Then
                        ErrorLog += vbCrLf & "These Dates are not valid" & vbCrLf
                        ErrorLog += AgL.XNull(DtV_Date.Rows(I)(GetFieldAliasName(bImportFor, "V_Date"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtV_Date.Rows(I)(GetFieldAliasName(bImportFor, "V_Date"))) & ", "
                    End If
                End If
            End If
        Next

        Dim DtV_Type = DtLedger.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "V_Type"))
        For I = 0 To DtV_Type.Rows.Count - 1
            If AgL.XNull(DtV_Type.Rows(I)(GetFieldAliasName(bImportFor, "V_Type"))) <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From Voucher_TYpe where V_Type = '" & AgL.XNull(DtV_Type.Rows(I)(GetFieldAliasName(bImportFor, "V_Type"))) & "'", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Voucher Types Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Voucher Types Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtV_Type.Rows(I)(GetFieldAliasName(bImportFor, "V_Type"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtV_Type.Rows(I)(GetFieldAliasName(bImportFor, "V_Type"))) & ", "
                    End If
                End If
            End If
        Next

        Dim DtLedgerAccount = DtLedger.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Ledger Account Name"))
        For I = 0 To DtLedgerAccount.Rows.Count - 1
            If AgL.XNull(DtLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString().Trim <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From SubGroup where Upper(RTrim(LTrim(Name))) = " & AgL.Chk_Text(AgL.XNull(DtLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString().Trim().ToUpper) & "", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Ledger Accounts Are Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Ledger Accounts Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name"))) & ", "
                    End If
                End If
            End If
        Next

        Dim DtContraLedgerAccount = DtLedger.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))
        For I = 0 To DtContraLedgerAccount.Rows.Count - 1
            If AgL.XNull(DtContraLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))).ToString().Trim <> "" Then
                If AgL.Dman_Execute("SELECT Count(*) From SubGroup where Upper(RTrim(LTrim(Name))) = " & AgL.Chk_Text(AgL.XNull(DtContraLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))).ToString().Trim.ToUpper) & "", AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog.Contains("These Ledger Accounts Not Present In Master") = False Then
                        ErrorLog += vbCrLf & "These Ledger Accounts Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtContraLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtContraLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))) & ", "
                    End If
                End If
            End If
        Next

        For I = 0 To DtLedger_DataFields.Rows.Count - 1
            If AgL.XNull(DtLedger_DataFields.Rows(I)("Remark")).ToString().Contains("Mandatory") Then
                If Not DtLedger.Columns.Contains(AgL.XNull(DtLedger_DataFields.Rows(I)("Field Name")).ToString()) Then
                    If ErrorLog.Contains("These fields are not present is excel file") = False Then
                        ErrorLog += vbCrLf & "These fields are not present is excel file" & vbCrLf
                        ErrorLog += AgL.XNull(DtLedger_DataFields.Rows(I)("Field Name")).ToString() & ", "
                    Else
                        ErrorLog += AgL.XNull(DtLedger_DataFields.Rows(I)("Field Name")).ToString() & ", "
                    End If
                End If
            End If
        Next

        If ErrorLog <> "" Then
            If File.Exists(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt") Then
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt", ErrorLog, False)
            Else
                File.Create(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt")
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt", ErrorLog, False)
            End If
            System.Diagnostics.Process.Start("notepad.exe", My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt")
            Exit Sub
        End If

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            Dim DtLedgerHeader As DataTable

            If bImportFor = ImportFor.Dos Then
                DtLedgerHeader = DtLedger.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "V_Type"),
                                                              GetFieldAliasName(bImportFor, "V_No"),
                                                              GetFieldAliasName(bImportFor, "V_Date"),
                                                              GetFieldAliasName(bImportFor, "File_V_Type"))
            Else
                DtLedgerHeader = DtLedger.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "V_Type"),
                                              GetFieldAliasName(bImportFor, "V_No"),
                                              GetFieldAliasName(bImportFor, "V_Date"))
            End If

            For I = 0 To DtLedgerHeader.Rows.Count - 1
                bHeadSubCodeName = ""
                Dim VoucherEntryTableList(0) As StructVoucherEntry
                Dim VoucherEntryTable As New StructVoucherEntry

                VoucherEntryTable.DocId = ""
                VoucherEntryTable.V_Type = AgL.XNull(DtLedgerHeader.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")))
                VoucherEntryTable.V_Prefix = ""
                VoucherEntryTable.Site_Code = AgL.PubSiteCode
                VoucherEntryTable.Div_Code = AgL.PubDivCode
                VoucherEntryTable.V_No = AgL.VNull(DtLedgerHeader.Rows(I)(GetFieldAliasName(bImportFor, "V_No")))
                VoucherEntryTable.V_Date = AgL.XNull(DtLedgerHeader.Rows(I)(GetFieldAliasName(bImportFor, "V_Date")))
                VoucherEntryTable.SubCode = ""
                VoucherEntryTable.SubCodeName = ""

                VoucherEntryTable.Narration = ""
                VoucherEntryTable.PostedBy = ""
                VoucherEntryTable.RecId = AgL.VNull(DtLedgerHeader.Rows(I)(GetFieldAliasName(bImportFor, "V_No")))
                VoucherEntryTable.U_Name = AgL.PubUserName
                VoucherEntryTable.U_EntDt = AgL.PubLoginDate
                VoucherEntryTable.U_AE = "A"
                VoucherEntryTable.PreparedBy = AgL.PubUserName


                Dim DtLedger_ForHeader As New DataTable
                For M = 0 To DtLedger.Columns.Count - 1
                    Dim DColumn As New DataColumn
                    DColumn.ColumnName = DtLedger.Columns(M).ColumnName
                    DtLedger_ForHeader.Columns.Add(DColumn)
                Next


                Dim DtRowLedger_ForHeader As DataRow()
                If DtLedger.Columns.Contains(GetFieldAliasName(bImportFor, "File_V_Type")) Then
                    DtRowLedger_ForHeader = DtLedger.Select("[" & GetFieldAliasName(bImportFor, "File_V_Type") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedgerHeader.Rows(I)("File_V_Type"))) + " And [" & GetFieldAliasName(bImportFor, "V_No") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedgerHeader.Rows(I)(GetFieldAliasName(bImportFor, "V_No")))) + " And [" & GetFieldAliasName(bImportFor, "V_Date") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedgerHeader.Rows(I)(GetFieldAliasName(bImportFor, "V_Date")))))
                Else
                    DtRowLedger_ForHeader = DtLedger.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedgerHeader.Rows(I)("V_Type"))) + " And [" & GetFieldAliasName(bImportFor, "V_No") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedgerHeader.Rows(I)(GetFieldAliasName(bImportFor, "V_No")))) + " And [" & GetFieldAliasName(bImportFor, "V_Date") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedgerHeader.Rows(I)(GetFieldAliasName(bImportFor, "V_Date")))))
                End If

                If DtRowLedger_ForHeader.Length > 0 Then
                    For M = 0 To DtRowLedger_ForHeader.Length - 1
                        DtLedger_ForHeader.Rows.Add()
                        For N = 0 To DtLedger_ForHeader.Columns.Count - 1
                            DtLedger_ForHeader.Rows(M)(N) = DtRowLedger_ForHeader(M)(N)
                        Next
                    Next
                End If



                For J = 0 To DtLedger_ForHeader.Rows.Count - 1
                    VoucherEntryTable.Line_RecId = AgL.VNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "V_No")))
                    VoucherEntryTable.Line_RecDate = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "V_Date")))
                    VoucherEntryTable.Line_V_SNo = 0
                    VoucherEntryTable.Line_V_Date = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "V_Date")))
                    VoucherEntryTable.Line_SubCode = ""
                    VoucherEntryTable.Line_SubCodeName = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim
                    VoucherEntryTable.Line_ContraSub = ""
                    VoucherEntryTable.Line_ContraSubName = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))).ToString.Trim
                    VoucherEntryTable.Line_AmtDr = AgL.VNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amt Dr")))
                    VoucherEntryTable.Line_AmtCr = AgL.VNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amt Cr")))
                    VoucherEntryTable.Line_Narration = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Narration")))
                    VoucherEntryTable.Line_Chq_No = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Chq No"))).ToString.Trim
                    VoucherEntryTable.Line_Chq_Date = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Chq Date"))).ToString.Trim
                    VoucherEntryTable.Line_TDSCategory = ""
                    VoucherEntryTable.Line_TDSOnAmt = ""
                    VoucherEntryTable.Line_CostCenter = ""
                    VoucherEntryTable.Line_ContraText = ""
                    VoucherEntryTable.Line_OrignalAmt = ""
                    VoucherEntryTable.Line_TDSDeductFrom = ""

                    If VoucherEntryTable.V_Type = "BP" Or VoucherEntryTable.V_Type = "CP" Then
                        If VoucherEntryTable.Line_AmtCr > 0 Then
                            bHeadSubCodeName = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim
                        End If
                    ElseIf VoucherEntryTable.V_Type = "BR" Or VoucherEntryTable.V_Type = "CR" Then
                        If VoucherEntryTable.Line_AmtDr > 0 Then
                            bHeadSubCodeName = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim
                        End If
                    End If


                    If DtPurchInvoice IsNot Nothing Then
                        Dim DtRowPurchInvoice_ForHeader As DataRow() = Nothing
                        DtRowPurchInvoice_ForHeader = DtPurchInvoice.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedger_ForHeader.Rows(J)("File_V_Type"))) + " And [invoice_no] = " + AgL.Chk_Text(AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "V_No")))))
                        If DtRowPurchInvoice_ForHeader IsNot Nothing Then
                            If DtRowPurchInvoice_ForHeader.Length > 0 Then VoucherEntryTable.Remark = DtRowPurchInvoice_ForHeader(0)("fv_no")
                        End If
                    End If

                    VoucherEntryTableList(UBound(VoucherEntryTableList)) = VoucherEntryTable
                    ReDim Preserve VoucherEntryTableList(UBound(VoucherEntryTableList) + 1)
                Next


                For J = 0 To VoucherEntryTableList.Length - 1
                    If bHeadSubCodeName <> "" Then
                        VoucherEntryTableList(J).SubCodeName = bHeadSubCodeName
                    End If
                Next
                InsertVoucherEntry(VoucherEntryTableList)
            Next

            AgL.ETrans.Commit()
            mTrans = "Commit"

        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
        If StrErrLog <> "" Then MsgBox(StrErrLog)
    End Sub

    Private Function GetFieldAliasName(bImportFor As ImportFor, bFieldName As String)
        Dim bAliasName As String = bFieldName
        If bImportFor = ImportFor.Dos Then
            Select Case bFieldName

                Case "V_TYPE"
                    bAliasName = "V_TYPE"
                Case "V_NO"
                    bAliasName = "V_NO"
                Case "V_Date"
                    bAliasName = "V_DATE"
                Case "Ledger Account Name"
                    bAliasName = "ledgername"
                Case "Contra Ledger Account Name"
                    bAliasName = "contraname"
                Case "Narration"
                    bAliasName = "narration"
                Case "Chq No"
                    bAliasName = "chq_no"
                Case "Chq Date"
                    bAliasName = "chq_date"
                Case "Amt Dr"
                    bAliasName = "amt_dr"
                Case "Amt Cr"
                    bAliasName = "amt_cr"
            End Select

            Return bAliasName
        Else
            Return bFieldName
        End If
    End Function

    'Public Sub FImportOpeningFromExcel(bImportFor As ImportFor)
    '    Dim mQry As String = ""
    '    Dim bHeadSubCodeName As String = ""
    '    Dim mTrans As String = ""
    '    Dim ErrorLog As String = ""
    '    Dim DtPurchaseInvoice As DataTable
    '    Dim DtPurchInvoice_DataFields As DataTable
    '    Dim DtLedger As DataTable
    '    Dim DtLedger_DataFields As DataTable
    '    Dim DtMain As DataTable = Nothing

    '    Dim I As Integer
    '    Dim J As Integer
    '    Dim K As Integer
    '    Dim M As Integer
    '    Dim N As Integer
    '    Dim StrErrLog As String = ""

    '    mQry = "Select '' as Srl, '" & GetFieldAliasName(bImportFor, "V_TYPE") & "' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
    '    mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "V_NO") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
    '    mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "V_Date") & "' as [Field Name], 'Date' as [Data Type], Null as [Length], 'Mandatory' as Remark "
    '    mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Vendor Name") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
    '    DtPurchInvoice_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)


    '    mQry = "Select '' as Srl, '" & GetFieldAliasName(bImportFor, "V_TYPE") & "' as [Field Name], 'Text' as [Data Type], 5 as [Length], 'Mandatory' as Remark "
    '    mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "V_NO") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
    '    mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "V_Date") & "' as [Field Name], 'Date' as [Data Type], Null as [Length], 'Mandatory' as Remark "
    '    mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Ledger Account Name") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], 'Mandatory' as Remark "
    '    mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Contra Ledger Account Name") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
    '    mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Narration") & "' as [Field Name], 'Text' as [Data Type], 255 as [Length], '' as Remark "
    '    mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Chq No") & "' as [Field Name], 'Text' as [Data Type], 50 as [Length], '' as Remark "
    '    mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Chq Date") & "' as [Field Name], 'Date' as [Data Type], Null as [Length], '' as Remark "
    '    mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Amt Dr") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
    '    mQry = mQry + "Union All Select  '' as Srl,'" & GetFieldAliasName(bImportFor, "Amt Cr") & "' as [Field Name], 'Number' as [Data Type], Null as [Length], 'Mandatory' as Remark "
    '    DtLedger_DataFields = AgL.FillData(mQry, AgL.GCn).Tables(0)

    '    Dim ObjFrmImport As New FrmImportFromExcel
    '    ObjFrmImport.Text = "Voucher Entry Import"
    '    ObjFrmImport.Dgl1.DataSource = DtPurchInvoice_DataFields
    '    ObjFrmImport.Dgl1.DataSource = DtLedger_DataFields
    '    ObjFrmImport.StartPosition = FormStartPosition.CenterScreen
    '    ObjFrmImport.ShowDialog()

    '    If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

    '    DtLedger = ObjFrmImport.P_DsExcelData.Tables(0)

    '    If bImportFor = ImportFor.Dos Then
    '        ''''''''''''''For Filtering Data To Import In This Entry'''''''''''''''''''''''''''''''''''
    '        Dim DtLedger_Filtered As New DataTable
    '        DtLedger_Filtered = DtLedger.Clone
    '        Dim DtLedgerRows_Filtered As DataRow() = DtLedger.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] In ('CO','DO')")
    '        For I = 0 To DtLedgerRows_Filtered.Length - 1
    '            DtLedger_Filtered.ImportRow(DtLedgerRows_Filtered(I))
    '        Next
    '        DtLedger = DtLedger_Filtered
    '        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    '        For I = 0 To DtLedger.Rows.Count - 1
    '            If DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "CO" Then
    '                DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "OB"
    '            ElseIf DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")).ToString.Trim = "DO" Then
    '                DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")) = "OB"
    '            End If

    '            If DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name")).ToString().Trim() = "CASH A/C." Then
    '                DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name")) = "CASH A/C"
    '            End If

    '            If DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name")).ToString().Trim() = "CASH A/C." Then
    '                DtLedger.Rows(I)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name")) = "CASH A/C"
    '            End If
    '        Next
    '    End If




    '    Dim DtV_Date = DtLedger.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "V_Date"))
    '    For I = 0 To DtV_Date.Rows.Count - 1
    '        If AgL.XNull(DtV_Date.Rows(I)(GetFieldAliasName(bImportFor, "V_Date"))) <> "" Then
    '            If CDate(AgL.XNull(DtV_Date.Rows(I)(GetFieldAliasName(bImportFor, "V_Date")))).Year < "2010" Then
    '                If ErrorLog.Contains("These Dates are not valid") = False Then
    '                    ErrorLog += vbCrLf & "These Dates are not valid" & vbCrLf
    '                    ErrorLog += AgL.XNull(DtV_Date.Rows(I)(GetFieldAliasName(bImportFor, "V_Date"))) & ", "
    '                Else
    '                    ErrorLog += AgL.XNull(DtV_Date.Rows(I)(GetFieldAliasName(bImportFor, "V_Date"))) & ", "
    '                End If
    '            End If
    '        End If
    '    Next

    '    Dim DtV_Type = DtLedger.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "V_Type"))
    '    For I = 0 To DtV_Type.Rows.Count - 1
    '        If AgL.XNull(DtV_Type.Rows(I)(GetFieldAliasName(bImportFor, "V_Type"))) <> "" Then
    '            If AgL.Dman_Execute("SELECT Count(*) From Voucher_TYpe where V_Type = '" & AgL.XNull(DtV_Type.Rows(I)(GetFieldAliasName(bImportFor, "V_Type"))) & "'", AgL.GCn).ExecuteScalar = 0 Then
    '                If ErrorLog.Contains("These Voucher Types Are Not Present In Master") = False Then
    '                    ErrorLog += vbCrLf & "These Voucher Types Not Present In Master" & vbCrLf
    '                    ErrorLog += AgL.XNull(DtV_Type.Rows(I)(GetFieldAliasName(bImportFor, "V_Type"))) & ", "
    '                Else
    '                    ErrorLog += AgL.XNull(DtV_Type.Rows(I)(GetFieldAliasName(bImportFor, "V_Type"))) & ", "
    '                End If
    '            End If
    '        End If
    '    Next

    '    Dim DtLedgerAccount = DtLedger.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Ledger Account Name"))
    '    For I = 0 To DtLedgerAccount.Rows.Count - 1
    '        If AgL.XNull(DtLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString().Trim <> "" Then
    '            If AgL.Dman_Execute("SELECT Count(*) From SubGroup where Name = " & AgL.Chk_Text(AgL.XNull(DtLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString().Trim()) & "", AgL.GCn).ExecuteScalar = 0 Then
    '                If ErrorLog.Contains("These Ledger Accounts Are Not Present In Master") = False Then
    '                    ErrorLog += vbCrLf & "These Ledger Accounts Are Not Present In Master" & vbCrLf
    '                    ErrorLog += AgL.XNull(DtLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name"))) & ", "
    '                Else
    '                    ErrorLog += AgL.XNull(DtLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Ledger Account Name"))) & ", "
    '                End If
    '            End If
    '        End If
    '    Next

    '    Dim DtContraLedgerAccount = DtLedger.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))
    '    For I = 0 To DtContraLedgerAccount.Rows.Count - 1
    '        If AgL.XNull(DtContraLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))).ToString().Trim <> "" Then
    '            If AgL.Dman_Execute("SELECT Count(*) From SubGroup where Name = " & AgL.Chk_Text(AgL.XNull(DtContraLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))).ToString().Trim) & "", AgL.GCn).ExecuteScalar = 0 Then
    '                If ErrorLog.Contains("These Ledger Accounts Not Present In Master") = False Then
    '                    ErrorLog += vbCrLf & "These Ledger Accounts Are Not Present In Master" & vbCrLf
    '                    ErrorLog += AgL.XNull(DtContraLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))) & ", "
    '                Else
    '                    ErrorLog += AgL.XNull(DtContraLedgerAccount.Rows(I)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))) & ", "
    '                End If
    '            End If
    '        End If
    '    Next

    '    For I = 0 To DtLedger_DataFields.Rows.Count - 1
    '        If AgL.XNull(DtLedger_DataFields.Rows(I)("Remark")).ToString().Contains("Mandatory") Then
    '            If Not DtLedger.Columns.Contains(AgL.XNull(DtLedger_DataFields.Rows(I)("Field Name")).ToString()) Then
    '                If ErrorLog.Contains("These fields are not present is excel file") = False Then
    '                    ErrorLog += vbCrLf & "These fields are not present is excel file" & vbCrLf
    '                    ErrorLog += AgL.XNull(DtLedger_DataFields.Rows(I)("Field Name")).ToString() & ", "
    '                Else
    '                    ErrorLog += AgL.XNull(DtLedger_DataFields.Rows(I)("Field Name")).ToString() & ", "
    '                End If
    '            End If
    '        End If
    '    Next

    '    If ErrorLog <> "" Then
    '        If File.Exists(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt") Then
    '            My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt", ErrorLog, False)
    '        Else
    '            File.Create(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt")
    '            My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt", ErrorLog, False)
    '        End If
    '        System.Diagnostics.Process.Start("notepad.exe", My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt")
    '        Exit Sub
    '    End If

    '    Try
    '        AgL.ECmd = AgL.GCn.CreateCommand
    '        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
    '        AgL.ECmd.Transaction = AgL.ETrans
    '        mTrans = "Begin"



    '        Dim DtLedgerHeader = DtLedger.DefaultView.ToTable(True, GetFieldAliasName(bImportFor, "V_Type"),
    '                                                          GetFieldAliasName(bImportFor, "V_No"),
    '                                                          GetFieldAliasName(bImportFor, "V_Date"))

    '        For I = 0 To DtLedgerHeader.Rows.Count - 1
    '            bHeadSubCodeName = ""
    '            Dim VoucherEntryTableList(0) As StructVoucherEntry
    '            Dim VoucherEntryTable As New StructVoucherEntry

    '            VoucherEntryTable.DocId = ""
    '            VoucherEntryTable.V_Type = AgL.XNull(DtLedgerHeader.Rows(I)(GetFieldAliasName(bImportFor, "V_Type")))
    '            VoucherEntryTable.V_Prefix = ""
    '            VoucherEntryTable.Site_Code = AgL.PubSiteCode
    '            VoucherEntryTable.Div_Code = AgL.PubDivCode
    '            VoucherEntryTable.V_No = AgL.VNull(DtLedgerHeader.Rows(I)(GetFieldAliasName(bImportFor, "V_No")))
    '            VoucherEntryTable.V_Date = AgL.XNull(DtLedgerHeader.Rows(I)(GetFieldAliasName(bImportFor, "V_Date")))
    '            VoucherEntryTable.SubCode = ""
    '            VoucherEntryTable.SubCodeName = ""

    '            VoucherEntryTable.Narration = ""
    '            VoucherEntryTable.PostedBy = ""
    '            VoucherEntryTable.RecId = AgL.VNull(DtLedgerHeader.Rows(I)(GetFieldAliasName(bImportFor, "V_No")))
    '            VoucherEntryTable.U_Name = AgL.PubUserName
    '            VoucherEntryTable.U_EntDt = AgL.PubLoginDate
    '            VoucherEntryTable.U_AE = "A"
    '            VoucherEntryTable.PreparedBy = AgL.PubUserName


    '            Dim DtLedger_ForHeader As New DataTable
    '            For M = 0 To DtLedger.Columns.Count - 1
    '                Dim DColumn As New DataColumn
    '                DColumn.ColumnName = DtLedger.Columns(M).ColumnName
    '                DtLedger_ForHeader.Columns.Add(DColumn)
    '            Next

    '            Dim DtRowLedger_ForHeader As DataRow() = DtLedger.Select("[" & GetFieldAliasName(bImportFor, "V_Type") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedgerHeader.Rows(I)("V_Type"))) + " And [" & GetFieldAliasName(bImportFor, "V_No") & "] = " + AgL.Chk_Text(AgL.XNull(DtLedgerHeader.Rows(I)(GetFieldAliasName(bImportFor, "V_No")))))
    '            If DtRowLedger_ForHeader.Length > 0 Then
    '                For M = 0 To DtRowLedger_ForHeader.Length - 1
    '                    DtLedger_ForHeader.Rows.Add()
    '                    For N = 0 To DtLedger_ForHeader.Columns.Count - 1
    '                        DtLedger_ForHeader.Rows(M)(N) = DtRowLedger_ForHeader(M)(N)
    '                    Next
    '                Next
    '            End If

    '            For J = 0 To DtLedger_ForHeader.Rows.Count - 1
    '                VoucherEntryTable.Line_RecId = AgL.VNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "V_No")))
    '                VoucherEntryTable.Line_RecDate = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "V_Date")))
    '                VoucherEntryTable.Line_V_SNo = 0
    '                VoucherEntryTable.Line_V_Date = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "V_Date")))
    '                VoucherEntryTable.Line_SubCode = ""
    '                VoucherEntryTable.Line_SubCodeName = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim
    '                VoucherEntryTable.Line_ContraSub = ""
    '                VoucherEntryTable.Line_ContraSubName = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Contra Ledger Account Name"))).ToString.Trim
    '                VoucherEntryTable.Line_AmtDr = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amt Dr")))
    '                VoucherEntryTable.Line_AmtCr = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Amt Cr")))
    '                VoucherEntryTable.Line_Narration = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Narration")))
    '                VoucherEntryTable.Line_Chq_No = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Chq No"))).ToString.Trim
    '                VoucherEntryTable.Line_Chq_Date = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Chq Date"))).ToString.Trim
    '                VoucherEntryTable.Line_TDSCategory = ""
    '                VoucherEntryTable.Line_TDSOnAmt = ""
    '                VoucherEntryTable.Line_CostCenter = ""
    '                VoucherEntryTable.Line_ContraText = ""
    '                VoucherEntryTable.Line_OrignalAmt = ""
    '                VoucherEntryTable.Line_TDSDeductFrom = ""

    '                If VoucherEntryTable.V_Type = "BP" Or VoucherEntryTable.V_Type = "CP" Then
    '                    If VoucherEntryTable.Line_AmtCr > 0 Then
    '                        bHeadSubCodeName = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim
    '                    End If
    '                ElseIf VoucherEntryTable.V_Type = "BR" Or VoucherEntryTable.V_Type = "CR" Then
    '                    If VoucherEntryTable.Line_AmtDr > 0 Then
    '                        bHeadSubCodeName = AgL.XNull(DtLedger_ForHeader.Rows(J)(GetFieldAliasName(bImportFor, "Ledger Account Name"))).ToString.Trim
    '                    End If
    '                End If

    '                VoucherEntryTableList(UBound(VoucherEntryTableList)) = VoucherEntryTable
    '                ReDim Preserve VoucherEntryTableList(UBound(VoucherEntryTableList) + 1)
    '            Next


    '            For J = 0 To VoucherEntryTableList.Length - 1
    '                If bHeadSubCodeName <> "" Then
    '                    VoucherEntryTableList(J).SubCodeName = bHeadSubCodeName
    '                End If
    '            Next
    '            InsertVoucherEntry(VoucherEntryTableList)
    '        Next

    '        AgL.ETrans.Commit()
    '        mTrans = "Commit"

    '    Catch ex As Exception
    '        AgL.ETrans.Rollback()
    '        MsgBox(ex.Message)
    '    End Try
    '    If StrErrLog <> "" Then MsgBox(StrErrLog)
    'End Sub
End Class