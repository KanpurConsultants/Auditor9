Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmServiceTax
    Private Const GSNo As Byte = 0
    Private Const GDescription As Byte = 1
    Private Const GDescriptionCode As Byte = 2
    Private Const GPosting As Byte = 3
    Private Const GPostingCode As Byte = 4
    Private Const GPersentage As Byte = 5
    Private Const GAmount As Byte = 6
    Private Const GFormula As Byte = 7

    '============ Adjustment =============
    Private Const GADocId As Byte = 0
    Private Const GAEntryNo As Byte = 1
    Private Const GAType As Byte = 2
    Private Const GAEntryDate As Byte = 3
    Private Const GAPCNo As Byte = 4
    Private Const GAPBNo As Byte = 5
    Private Const GAAmount As Byte = 6
    Private Const GAShow As Byte = 7

    '=========== Landed Value ============
    Private Const GLDocId As Byte = 0
    Private Const GLV_SNo As Byte = 1
    Private Const GLOValue As Byte = 2
    Private Const GLEValue As Byte = 3
    Private Const GLAAmount As Byte = 4

    Private DTVType As DataTable
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private LIEvent As ClsEvents
    Private FGSTR As DMStructure.DMStructure
    Private WithEvents FGMain As New AgControls.AgDataGrid
    Private WithEvents FGAdj As New AgControls.AgDataGrid
    Private WithEvents FGLValue As New DataGridView

    Dim BlnTDSROff As Boolean
    Dim FrmFind As New AgLibrary.frmFind(AgL)
    Dim StrCompareRecIdTemp As String
    Dim StrCompareDateTemp As String
    Dim RFNumberSystem As ClsMain.RecIdFormat
    Dim StrSrvTaxAdjRefType As String = ""
    Dim StrRefDocId As String = ""
    Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub
    Private Sub FrmServiceTax_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Or e.KeyCode = Keys.F3 Or e.KeyCode = Keys.F4 Or e.KeyCode = (Keys.F And e.Control) Or e.KeyCode = (Keys.P And e.Control) _
        Or e.KeyCode = (Keys.S And e.Control) Or e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F5 Or e.KeyCode = Keys.F10 _
        Or e.KeyCode = Keys.Home Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Or e.KeyCode = Keys.End Then
            Topctrl1.TopKey_Down(e)
        End If
    End Sub
    Private Sub FrmServiceTax_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim DTTemp As DataTable
        Try
            LIEvent = New ClsEvents(Me)
            Agl.WinSetting(Me, 665, 985, 0, 0)
            Agl.GridDesign(FGMain)
            AgL.GridDesign(FGAdj)

            DTTemp = cmain.FGetDatTable("Select VRNumberSystem,TDSROff,SrvTaxAdjRefType From Enviro_Accounts", Agl.Gcn)
            If DTTemp.Rows.Count > 0 Then BlnTDSROff = IIf(UCase(Agl.Xnull(DTTemp.Rows(0).Item("TDSROff"))) = "Y", True, False)
            If DTTemp.Rows.Count > 0 Then RFNumberSystem = IIf(UCase(Agl.Xnull(DTTemp.Rows(0).Item("VRNumberSystem"))) = "D", ClsMain.RecIdFormat.DD_MM, _
                                                           IIf(UCase(Agl.Xnull(DTTemp.Rows(0).Item("VRNumberSystem"))) = "M", ClsMain.RecIdFormat.MM, ClsMain.RecIdFormat.DD_MM_YY))
            If DTTemp.Rows.Count > 0 Then StrSrvTaxAdjRefType = Replace(Agl.Xnull(DTTemp.Rows(0).Item("SrvTaxAdjRefType")), "|", "'")

            DTTemp.Dispose()
            FIniMaster()
            IniGrid()
            MoveRec()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FIniMaster(Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 1)
        Topctrl1.FIniForm(DTMaster, Agl.Gcn, "Select DocId As SearchCode From STaxTrn Where Site_Code='" & agl.PubSiteCode & "' And V_Prefix='" & Agl.PubCompVPrefix & "' Order By V_Type,Cast((Case When IsNumeric(RecId)=1 Then RecId Else 0 End) As BigInt)", , , , , BytDel, BytRefresh)
    End Sub
    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        FIniMaster(0, 0)
    End Sub
    Private Sub IniGrid()
        FGMain.Height = PnlTDS.Height
        FGMain.Width = PnlTDS.Width
        FGMain.Top = PnlTDS.Top
        FGMain.Left = PnlTDS.Left
        Controls.Add(FGMain)
        FGMain.Visible = True
        FGMain.BringToFront()
        AgCl.AddAgTextColumn(FGMain, "SNo", 40, 5, "S.No.", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "Description", 120, 10, "Description", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "DescriptionCode", 0, 0, "DescriptionCode", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "Posting", 200, 10, "Posting A/c", True, True, False)
        AgCl.AddAgTextColumn(FGMain, "PostingCode", 0, 0, "PostingCode", False, True, False)
        AgCl.AddAgTextColumn(FGMain, "Percentage", 60, 10, "(%)Per.", True, True, True)
        AgCl.AddAgTextColumn(FGMain, "Amount", 100, 10, "Amount", True, True, True)
        AgCl.AddAgTextColumn(FGMain, "Formula", 0, 0, "Formula", False, True, False)
        FGMain.Anchor = PnlTDS.Anchor
        Agl.FSetSNo(FGMain, GSNo)
        FGMain.AllowUserToAddRows = False
        FGMain.TabIndex = PnlTDS.TabIndex

        '===================== Structure Grid ==========================
        'FGSTR = New DMStructure.DMStructure(PnlExp, Topctrl1, LIPublic)
        'Controls.Add(FGSTR)

        '======================== Adjustment ========================
        FGAdj.Height = PnlAdj.Height
        FGAdj.Width = PnlAdj.Width
        FGAdj.Top = PnlAdj.Top
        FGAdj.Left = PnlAdj.Left
        Controls.Add(FGAdj)
        FGAdj.Visible = True
        FGAdj.BringToFront()
        AgCl.AddAgTextColumn(FGAdj, "DocId", 0, 0, "DocId", False, True, True)
        AgCl.AddAgTextColumn(FGAdj, "Adj Ref. Entry No.", 80, 10, "Adj Ref. Entry No.", True, True, False)
        AgCl.AddAgTextColumn(FGAdj, "Type", 50, 0, "Type", True, True, False)
        AgCl.AddAgTextColumn(FGAdj, "Entry Date", 80, 0, "Entry Date", True, True, False)
        AgCl.AddAgTextColumn(FGAdj, "Party Chalan No.", 90, 10, "Party Challan No.", True, True, False)
        AgCl.AddAgTextColumn(FGAdj, "Party Bill No.", 75, 0, "Party Bill No.", True, True, False)
        AgCl.AddAgTextColumn(FGAdj, "Adjust Amount", 80, 0, "Adjust Amount", True, False, True)
        AgCl.AddAgButtonColumn(FGAdj, "", 20, "")
        FGAdj.Anchor = PnlAdj.Anchor
        FGAdj.TabIndex = PnlAdj.TabIndex
        FGAdj.ColumnHeadersHeight = 40
        FGAdj.ColumnHeadersDefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)
        FGAdj.DefaultCellStyle.Font = New Font("Arial", 9, FontStyle.Regular)

        '======================== Landed Value ========================
        FGLValue.AllowUserToAddRows = False
        AgCl.AddAgTextColumn(FGLValue, "DocId", 80, 0, "DocId", True, True, True)
        AgCl.AddAgTextColumn(FGLValue, "V_SNo", 80, 10, "V_Sno", True, True, False)
        AgCl.AddAgTextColumn(FGLValue, "OValue", 80, 0, "OValue", True, True, False)
        AgCl.AddAgTextColumn(FGLValue, "EValue", 80, 0, "EValue", True, True, False)
        AgCl.AddAgTextColumn(FGLValue, "AAmount", 80, 0, "AAmount", True, True, False)
    End Sub
    Public Sub MoveRec()
        Dim ADTemp As SqlClient.SqlDataAdapter
        Dim DTTemp As New DataTable
        Dim I As Integer
        Dim StrSQL As String

        FClear()
        FManageDisplay(True)
        Topctrl1.BlankTextBoxes()
        If DTMaster.Rows.Count > 0 Then
            ADTemp = New SqlClient.SqlDataAdapter("Select STT.*, " & _
                    "PT.Name As PTName,TC.Name As TCName, " & _
                    "CR.Name As CRName,CE.Name As CEName,CF.CityName As FName,CT.CityName As TName,VT.Description as VTName,STT.V_Type, " & _
                    "L.Docid As LDocid,L.Recid AS LRecid,STT.VrRef_SNo  " & _
                    "From STaxTrn STT Left Join " & _
                    "SubGroup CR On CR.SubCode=STT.Consignor Left Join " & _
                    "SubGroup CE On CE.SubCode=STT.Consignee Left Join " & _
                    "City CF On CF.CityCode=STT.FromPlace Left Join " & _
                    "City CT On CT.CityCode=STT.ToPlace Left Join " & _
                    "SubGroup PT On PT.SubCode=STT.PartyCode Left Join " & _
                    "TdsCat TC On TC.Code=STT.TdsCategory Left Join " & _
                    "Voucher_Type VT On VT.V_Type=STT.V_Type " & _
                    "LEFT JOIN  Ledger L ON L.Docid=STT.VrRefDocID And L.V_SNo=STT.VrRef_SNo  " & _
                    "Where STT.DocId='" & Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "'", Agl.Gcn)
            ADTemp.Fill(DTTemp)
            If DTTemp.Rows.Count > 0 Then
                StrCompareRecIdTemp = Agl.Xnull(DTTemp.Rows(0).Item("RecId"))
                StrCompareDateTemp = Agl.Xnull(DTTemp.Rows(0).Item("V_Date"))
                TxtVType.Text = Agl.Xnull(DTTemp.Rows(0).Item("VTName"))
                TxtVType.Tag = Agl.Xnull(DTTemp.Rows(0).Item("V_Type"))
                TxtV_No.Text = Agl.Xnull(DTTemp.Rows(0).Item("V_No"))
                TxtRecId.Text = Agl.Xnull(DTTemp.Rows(0).Item("RecId"))
                TxtV_No.Tag = Agl.Xnull(DTTemp.Rows(0).Item("V_Prefix"))
                TxtDate.Text = Agl.Xnull(DTTemp.Rows(0).Item("V_Date"))
                TxtParty.Text = Agl.Xnull(DTTemp.Rows(0).Item("PTName"))
                TxtParty.Tag = Agl.Xnull(DTTemp.Rows(0).Item("PartyCode"))
                TxtConsignor.Text = Agl.Xnull(DTTemp.Rows(0).Item("CRName"))
                TxtConsignor.Tag = Agl.Xnull(DTTemp.Rows(0).Item("Consignor"))
                TxtConsignee.Text = Agl.Xnull(DTTemp.Rows(0).Item("CEName"))
                TxtConsignee.Tag = Agl.Xnull(DTTemp.Rows(0).Item("Consignee"))
                TxtPlaceFrom.Text = Agl.Xnull(DTTemp.Rows(0).Item("FName"))
                TxtPlaceFrom.Tag = Agl.Xnull(DTTemp.Rows(0).Item("FromPlace"))
                TxtPlaceTo.Text = Agl.Xnull(DTTemp.Rows(0).Item("TName"))
                TxtPlaceTo.Tag = Agl.Xnull(DTTemp.Rows(0).Item("ToPlace"))
                TxtTDSCategory.Text = Agl.Xnull(DTTemp.Rows(0).Item("TCName"))
                TxtTDSCategory.Tag = Agl.Xnull(DTTemp.Rows(0).Item("TDSCategory"))
                TxtType.Tag = Agl.Xnull(DTTemp.Rows(0).Item("EntryType"))
                TxtType.Text = IIf(UCase(Agl.Xnull(DTTemp.Rows(0).Item("EntryType"))) = "N", "Non GTA", "GTA")
                TxtVehicleNo.Text = Agl.Xnull(DTTemp.Rows(0).Item("VehicleNo"))
                TxtAmount.Text = Agl.VNull(DTTemp.Rows(0).Item("GAmount"))
                TxtTDSCalOn.Text = Agl.VNull(DTTemp.Rows(0).Item("TDSCalOn"))
                TxtRemark.Text = Agl.Xnull(DTTemp.Rows(0).Item("Remark"))
                TxtDescription.Text = Agl.Xnull(DTTemp.Rows(0).Item("Description"))
                TxtConsigneeBNo.Text = Agl.Xnull(DTTemp.Rows(0).Item("ConsigneeBill"))
                TxtConsignorBNo.Text = Agl.Xnull(DTTemp.Rows(0).Item("ConsignorBill"))
                TxtPrepared.Text = Agl.Xnull(DTTemp.Rows(0).Item("PreparedBy"))
                TxtModified.Text = Agl.Xnull(DTTemp.Rows(0).Item("ModifiedBy"))
                TxtPtyBillNo.Text = Agl.Xnull(DTTemp.Rows(0).Item("PtyBillNo"))
                TxtPtyDate.Text = Agl.Xnull(DTTemp.Rows(0).Item("PtyBillDt"))
                TxtVRef.Text = Agl.Xnull(DTTemp.Rows(0).Item("LRecid"))
                TxtVRef.Tag = Agl.Xnull(DTTemp.Rows(0).Item("LDocid")) & "|:|" & Agl.Xnull(DTTemp.Rows(0).Item("VrRef_SNo"))
                FGSTR.FFillHeads(TxtVType.Tag, Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")), Agl.VNull(DTTemp.Rows(0).Item("GAmount")))

                If Trim(TxtTDSCategory.Tag) <> "" Then
                    'For TDS
                    DTTemp.Clear()
                    DTTemp = cmain.FGetDatTable("Select LG.FormulaString,LG.SubCode,SG.Name As AcName,SG.ManualCode, " & _
                        "LG.AmtDr,LG.AmtCr,LG.TDSDesc,TCD.Name As DName,LG.TDSPer " & _
                        "From Ledger_Temp LG Left Join SubGroup SG On LG.SubCode=SG.SubCode Left Join " & _
                        "TDSCat_Description TCD On TCD.Code=LG.TDSDesc  " & _
                        "Where LG.DocId='" & Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "' And IsNull(LG.System_Generated,'N')='Y' And IsNull(LG.TDS_Of_V_SNo,0)<>0  Order By V_SNo", Agl.Gcn)
                    If DTTemp.Rows.Count > 0 Then
                        FGMain.Rows.Add(DTTemp.Rows.Count)
                    End If
                    For I = 0 To DTTemp.Rows.Count - 1
                        FGMain(GSNo, I).Value = Trim(I + 1)
                        FGMain(GDescriptionCode, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("TDSDesc"))
                        FGMain(GDescription, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("DName"))
                        FGMain(GPostingCode, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("SubCode"))
                        FGMain(GPosting, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("AcName"))
                        FGMain(GPersentage, I).Value = Format(Agl.VNull(DTTemp.Rows(I).Item("TDSPer")), "0.000")
                        If Agl.VNull(DTTemp.Rows(I).Item("AmtDr")) > 0 Then
                            FGMain(GAmount, I).Value = Format(Agl.VNull(DTTemp.Rows(I).Item("AmtDr")), "0.00")
                        Else
                            FGMain(GAmount, I).Value = Format(Agl.VNull(DTTemp.Rows(I).Item("AmtCr")), "0.00")
                        End If
                        FGMain(GFormula, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("FormulaString"))
                    Next
                End If

                DTTemp.Clear()
                FGAdj.Rows.Clear()
                StrSQL = "Select ST.DocId,Max(ST.V_Type) As V_Type,Max(ST.RecId) As RecId, "
                StrSQL += "Max(ST.V_Date) As V_Date,Max(ST.PtyChallanNo) As PtyChallanNo, "
                StrSQL += "Max(ST.PtyBillNo) As PtyBillNo,Max(SOA.AdjAmount) As AdjAmount "
                StrSQL += "From StockOtherAdjustment SOA "
                StrSQL += "Left Join Stock ST On SOA.StockRefDocId=ST.DocId "
                StrSQL += "Where SOA.DocId='" & Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "' "
                StrSQL += "Group By ST.DocId "
                StrSQL += "Order By Max(ST.V_Date),Max(ST.V_Type),Max(ST.RecId) "
                DTTemp = cmain.FGetDatTable(StrSQL, Agl.Gcn)
                For I = 0 To DTTemp.Rows.Count - 1
                    FGAdj.Rows.Add()
                    FGAdj(GADocId, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("DocId"))
                    FGAdj(GAEntryNo, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("RecId"))
                    FGAdj(GAType, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("V_Type"))
                    FGAdj(GAEntryDate, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("V_Date"))
                    FGAdj(GAPCNo, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("PtyChallanNo"))
                    FGAdj(GAPBNo, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("PtyBillNo"))
                    FGAdj(GAAmount, I).Value = Format(Agl.VNull(DTTemp.Rows(I).Item("AdjAmount")), "0.00")
                Next
            End If
        End If

        Topctrl1.FSetDispRec(BMBMaster)
        ADTemp = Nothing
        DTTemp = Nothing
        FCalculate()
    End Sub
    Private Sub FManageDisplay(ByVal BlnEnb As Boolean)
        FGAdj.Columns(GAAmount).ReadOnly = BlnEnb

        TxtV_No.Enabled = False
        TxtRecId.Enabled = False
        TxtPrepared.Enabled = False
        TxtModified.Enabled = False
        TxtType.Enabled = False
    End Sub
    Public Sub FTxtGotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        '======== Write Your Code Below =============
    End Sub
    Public Sub FTxtKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.Name
            Case TxtConsignee.Name, TxtConsignor.Name, TxtPlaceFrom.Name, TxtPlaceTo.Name, _
                 TxtVType.Name, TxtTDSCategory.Name, TxtParty.Name, TxtVRef.Name
                If e.KeyCode = Keys.Delete Then
                    sender.Text = "" : sender.Tag = ""
                    If TxtTDSCategory.Name = sender.Name Then
                        FGMain.Rows.Clear()
                        FCalculate()
                    End If
                End If
        End Select
    End Sub
    Public Sub FTxtKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '======== Write Your Code Below =============
        Select Case sender.Name
            Case TxtVType.Name
                FHP_Vtype(e, sender)
            Case TxtConsignee.Name, TxtConsignor.Name, TxtParty.Name
                FHP_Subgroup(e, sender)
            Case TxtPlaceFrom.Name, TxtPlaceTo.Name
                FHP_City(e, sender)
            Case TxtTDSCategory.Name
                FHP_TDSCategory(e, sender)
            Case TxtAmount.Name, TxtTDSCalOn.Name
                CMain.NumPress(TxtAmount, e, 10, 2, False)
            Case TxtVRef.Name
                FHP_VoucherNo(e, sender)
        End Select
    End Sub
    Private Sub FHP_TDSCategory(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable, I As Integer
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String

        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        AgL.ADMain = New SqlClient.SqlDataAdapter("Select Code,Name From TDSCat Order By Name", AgL.GCn)
        AgL.ADMain.Fill(DTMain)
        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 200, 280, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(1)
                Txt.Tag = FRH.DRReturn.Item(0)
                FGMain.Rows.Clear()
                DTMain = CMain.FGetDatTable("Select TC.TDSDesc,TCD.Name As DName,TC.ACCode,SG.Name As AName,TC.Percentage,TC.FormulaString From TDSCat_Det TC Left Join TDSCat_Description TCD On TC.TDSDesc=TCD.Code Left Join SubGroup SG On TC.AcCode=SG.SubCode Where TC.Code='" & Txt.Tag & "' Order By TC.SrNo", AgL.GCn)
                For I = 0 To DTMain.Rows.Count - 1
                    FGMain.Rows.Add()
                    FGMain(GSNo, I).Value = Trim(I + 1)
                    FGMain(GDescription, I).Value = AgL.XNull(DTMain.Rows(I).Item("DName"))
                    FGMain(GDescriptionCode, I).Value = AgL.XNull(DTMain.Rows(I).Item("TDSDesc"))
                    FGMain(GPosting, I).Value = AgL.XNull(DTMain.Rows(I).Item("AName"))
                    FGMain(GPostingCode, I).Value = AgL.XNull(DTMain.Rows(I).Item("ACCode"))
                    FGMain(GPersentage, I).Value = Format(AgL.VNull(DTMain.Rows(I).Item("Percentage")), "0.000")
                    FGMain(GFormula, I).Value = AgL.XNull(DTMain.Rows(I).Item("FormulaString"))
                Next
            End If
        End If
        FRH = Nothing
        e.KeyChar = ""
        FCalculate()
    End Sub
    Private Sub FHP_Vtype(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String, StrPrvText As String

        StrPrvText = Txt.Text
        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        AgL.ADMain = New SqlClient.SqlDataAdapter("Select V_type,Description From Voucher_Type VT Where Category in ('STAX') order  by V_type", AgL.GCn)
        AgL.ADMain.Fill(DTMain)
        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 300, 350, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Type", 270, DataGridViewContentAlignment.MiddleLeft)
        FRH.ShowDialog()
        Txt.Text = StrPrvText
        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(1)
                Txt.Tag = FRH.DRReturn.Item(0)
                If UCase(Trim(Txt.Tag)) = "STAX" Or UCase(Trim(Txt.Tag)) = "STAXR" Or UCase(Trim(Txt.Tag)) = "STXOW" Or UCase(Trim(Txt.Tag)) = "STXOR" Then
                    TxtType.Text = "GTA"
                    TxtType.Tag = "G"
                Else
                    TxtType.Text = "NON GTA"
                    TxtType.Tag = "N"
                End If
                FGSTR.FFillHeads(Txt.Tag, "", 0)
                StrDocID = CMain.FGetDoId(TxtV_No, TxtVType.Tag, "STaxTrn", "V_No", TxtDate.Text, True)
                FGenerateNo()
            End If
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub FGenerateNo()
        If Trim(TxtVType.Text) = "" Then Exit Sub
        If TxtDate.Text = StrCompareDateTemp Then
            TxtRecId.Text = StrCompareRecIdTemp
        Else
            If RFNumberSystem = ClsMain.RecIdFormat.DD_MM Then
                TxtRecId.Text = CMain.FGetRecId(TxtDate.Text, "STaxTrn", "RecId", "V_Date", TxtVType.Tag, True, ClsMain.RecIdFormat.DD_MM)
            ElseIf RFNumberSystem = ClsMain.RecIdFormat.MM Then
                TxtRecId.Text = CMain.FGetRecId(TxtDate.Text, "STaxTrn", "RecId", "V_Date", TxtVType.Tag, True, ClsMain.RecIdFormat.MM)
            End If
        End If
        If Trim(TxtV_No.Text) = "" Then StrDocID = CMain.FGetDoId(TxtV_No, TxtVType.Tag, "STaxTrn", "V_No", TxtDate.Text)
        If Topctrl1.Mode = "Add" Then
            If RFNumberSystem <> ClsMain.RecIdFormat.DD_MM And RFNumberSystem <> ClsMain.RecIdFormat.MM Then
                TxtRecId.Text = CMain.FGetMaxNo("Select IsNull(Max(Convert(Bigint,ST.RecId)),0)+1 As Mx From STaxTrn ST Where IsNumeric(ST.RecId)<>0 And ST.V_Prefix='" & TxtV_No.Tag & "' And LM.V_Type='" & TxtVType.Tag & "' And LM.Site_Code='" & AgL.PubSiteCode & "' ", AgL.GCn)
            End If
        End If
    End Sub
    Private Sub FHP_Subgroup(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String

        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        AgL.ADMain = New SqlClient.SqlDataAdapter("Select SG.SubCode As SearchCode,SG.Name,SG.ManualCode,IsNull(CT.CityName,''),AG.GroupName From SubGroup SG Left Join City CT On CT.CityCode=SG.CityCode Left Join AcGroup AG ON AG.GroupCode=SG.GroupCode Where SG.SiteList Like '%|" & AgL.PubSiteCode & "|%' Order By SG.Name ", AgL.GCn)
        AgL.ADMain.Fill(DTMain)
        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 300, 780, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 250, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(2, "Code", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(3, "City", 150, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(4, "Group Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(1)
                Txt.Tag = FRH.DRReturn.Item(0)

                If Txt.Name = TxtParty.Name Then
                    FGSTR.FSetValue(DMStructure.ClsStructure.GrdIndex.GAccountCode, "NAMT", Txt.Tag)
                    FGSTR.FSetValue(DMStructure.ClsStructure.GrdIndex.GAccount, "NAMT", Txt.Text)
                End If
            End If
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub

    Private Sub FHP_VoucherNo(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String
        Dim StrSql As String = ""

        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        StrSql = "SELECT (L.DocId+'|:|'+Str(L.V_SNO)) AS Code,L.Docid AS Docid,L.RecId AS VoucherNo,L.V_Date AS VDate,VT.V_Type AS VType,SG.Name AS Debit_AC,L.AmtDr,L.Chq_No,L.Chq_Date,L.V_SNO  FROM Ledger L "
        StrSql += "LEFT JOIN Voucher_Type VT ON VT.V_Type=L.V_Type  "
        StrSql += "LEFT JOIN SubGroup SG ON SG.SubCode=L.SubCode  "
        StrSql += "WHERE VT.Category='PMT'  And L.AmtDr>0 "
        StrSql += "ORDER BY L.V_Date,L.RecId,L.V_Sno "

        AgL.ADMain = New SqlClient.SqlDataAdapter(StrSql, AgL.GCn)
        AgL.ADMain.Fill(DTMain)
        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 300, 870, (Top + Txt.Top + GrpBox.Top) + 85, 140)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, , 0, , False)
        FRH.FFormatColumn(2, "VoucherNo", 80, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(3, "VDate", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(4, "VType", 70, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(5, "Debit A/C", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(6, "Amount Dr.", 120, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(7, "Cheque No.", 115, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(8, "Cheque Dt.", 105, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(9, "VSNO", 0, , False)
        FRH.StartPosition = FormStartPosition.CenterScreen
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(2)
                Txt.Tag = AgL.XNull(FRH.DRReturn.Item("Code"))
            End If
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub

    Private Sub FHP_City(ByRef e As System.Windows.Forms.KeyPressEventArgs, ByVal Txt As TextBox)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String

        StrSendText = CMain.FSendText(Txt, e.KeyChar)
        AgL.ADMain = New SqlClient.SqlDataAdapter("Select	CityCode,CityName From City " & _
                                                       "Order By CityName ", AgL.GCn)
        AgL.ADMain.Fill(DTMain)
        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 300, 230, (Top + Txt.Top) + 85, Left + Txt.Left + 3)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Name", 150, DataGridViewContentAlignment.MiddleLeft)
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                Txt.Text = FRH.DRReturn.Item(1)
                Txt.Tag = FRH.DRReturn.Item(0)
            End If
        End If
        FRH = Nothing
        e.KeyChar = ""
    End Sub
    Private Sub FGAdj_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles FGAdj.EditingControlShowing
        If TypeOf e.Control Is AgControls.AgTextBox Then
            RemoveHandler DirectCast(e.Control, AgControls.AgTextBox).KeyPress, AddressOf FGrdNumPress
            AddHandler DirectCast(e.Control, AgControls.AgTextBox).KeyPress, AddressOf FGrdNumPress
        End If
    End Sub
    Private Sub FGrdNumPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Select Case FGAdj.CurrentCell.ColumnIndex
            Case GAAmount
                CMain.NumPress(sender, e, 10, 2, False)
        End Select
    End Sub
    Private Sub FGAdj_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles FGAdj.CellContentClick
        Select Case e.ColumnIndex
            Case GAShow
                FOpenForm(e.RowIndex)
        End Select
    End Sub
    Private Sub FGAdj_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles FGAdj.KeyDown
        If Topctrl1.Mode <> "Browse" Then
            If e.Control And e.KeyCode = Keys.D Then
                FGAdj.CurrentRow.Selected = True
            End If
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
        Try
            Select Case FGAdj.CurrentCell.ColumnIndex
                Case GAEntryNo
                    FHPGD_AdjRefNo(e)
            End Select
        Catch Ex As NullReferenceException
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub
    Private Sub FHPGD_AdjRefNo(ByRef e As System.Windows.Forms.KeyEventArgs)
        Dim DTMain As New DataTable
        Dim FRH As DMHelpGrid.FrmHelpGrid
        Dim StrSendText As String
        Dim StrSQL As String

        If Topctrl1.Mode = "Browse" Then Exit Sub
        If Not CMain.FGrdDisableKeys(e) Then Exit Sub
        StrSendText = Cmain.FSendText(FGAdj, Chr(e.KeyCode))

        If StrSrvTaxAdjRefType = "" Then MsgBox("Please Define (Show Service Tax Adjustment Type) In Enviro.") : Exit Sub
        StrSQL = "Select ST.DocId,Max(ST.RecId) As RecId,Max(ST.V_Type) As V_Type, "
        StrSQL += "Max(ST.V_Date) As V_Date,Max(ST.PtyChallanNo) As PtyChallanNo, "
        StrSQL += "Max(ST.PtyBillNo) As PtyBillNo,Max(SG.Name) As PName "
        StrSQL += "From Stock ST "
        StrSQL += "Left Join SubGroup SG On ST.PartyCode=SG.SubCode "
        StrSQL += "Where ST.V_Type In (" & StrSrvTaxAdjRefType & ") And  "
        StrSQL += "ST.Site_Code='" & agl.PubSiteCode & "' And  "
        StrSQL += "(ST.V_Date Between '" & Agl.PubStartDate & "' And '" & Agl.PubEndDate & "') "
        StrSQL += "Group By ST.DocId "
        StrSQL += "Having IsNull(Min(ST.LandedValue),0)>0 "
        StrSQL += "Order By Max(ST.V_Date),Max(ST.V_Type),Max(ST.RecId) "
        DTMain = cmain.FGetDatTable(StrSQL, Agl.Gcn)

        FRH = New DMHelpGrid.FrmHelpGrid(New DataView(DTMain), StrSendText, 300, 780)
        FRH.FFormatColumn(0, , 0, , False)
        FRH.FFormatColumn(1, "Entry No.", 50, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(2, "Type", 60, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(3, "Entry Date", 90, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(4, "Party Challan No.", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(5, "Party Bill No.", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH.FFormatColumn(6, "Party Name", 300, DataGridViewContentAlignment.MiddleLeft)

        FRH.StartPosition = FormStartPosition.CenterScreen
        FRH.ShowDialog()

        If FRH.BytBtnValue = 0 Then
            If Not FRH.DRReturn.Equals(Nothing) Then
                FGAdj(GADocId, FGAdj.CurrentCell.RowIndex).Value = Agl.Xnull(FRH.DRReturn.Item("DocId"))
                FGAdj(GAEntryNo, FGAdj.CurrentCell.RowIndex).Value = Agl.Xnull(FRH.DRReturn.Item("RecId"))
                FGAdj(GAType, FGAdj.CurrentCell.RowIndex).Value = Agl.Xnull(FRH.DRReturn.Item("V_Type"))
                FGAdj(GAEntryDate, FGAdj.CurrentCell.RowIndex).Value = Agl.Xnull(FRH.DRReturn.Item("V_Date"))
                FGAdj(GAPCNo, FGAdj.CurrentCell.RowIndex).Value = Agl.Xnull(FRH.DRReturn.Item("PtyChallanNo"))
                FGAdj(GAPBNo, FGAdj.CurrentCell.RowIndex).Value = Agl.Xnull(FRH.DRReturn.Item("PtyBillNo"))
            End If
        End If
        FRH = Nothing
    End Sub
    Private Sub FClear()
        FGSTR.FGMain.Rows.Clear()
        FGMain.Rows.Clear()
        FGAdj.Rows.Clear()
        StrCompareRecIdTemp = ""
        StrCompareDateTemp = ""
    End Sub
    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        FManageDisplay(False)
        FClear()
        TxtDate.Text = Agl.PubLoginDate
        TxtPrepared.Text = Agl.PubUserName
        TxtVType.Focus()
    End Sub
    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New SqlClient.SqlCommand
        Dim DTTemp As New DataTable

        Try
            If DTMaster.Rows.Count > 0 Then
                If MsgBox(" Delete Conflict ", MsgBoxStyle.YesNo) = vbYes Then
                    If CMain.FGetMaxNo("Select Count(*) From LedgerAdj_Temp Where Adj_DocId='" & Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "' ", Agl.Gcn) > 0 Then MsgBox("Record Has Been Adjusted. You Cannot Edit/Delete This Record.") : Exit Sub
                    StrDocID = ""
                    StrDocID = Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
                    If Trim(Replace(StrDocID, "0", "")) = "" Then MsgBox(" Invalid " & "DocId.") : Exit Sub
                    If CMain.FGetMaxNo("Select Count(*) Cnt From DataAudit Where DocId='" & StrDocID & "' ", Agl.Gcn) > 0 Then MsgBox("Record Has Been Audited. You Can Not Edit/ Delete This Record.") : Exit Sub

                    BlnTrans = True
                    GCnCmd.Connection = Agl.Gcn
                    GCnCmd.Transaction = Agl.Gcn.BeginTransaction(IsolationLevel.Serializable)

                    FGSTR.FDeleteHeads(StrDocID, GCnCmd)

                    GCnCmd.CommandText = "Update	Stock Set OtherAdjustment=0 "
                    GCnCmd.CommandText += "From Stock "
                    GCnCmd.CommandText += "Left Join (Select SOA.StockRefDocId,SOA.StockRefV_SNo,Sum(SOA.LandedValue) As LValue  "
                    GCnCmd.CommandText += "From StockOtherAdjustment SOA  "
                    GCnCmd.CommandText += "Where SOA.DocId<>'" & StrDocID & "' "
                    GCnCmd.CommandText += "Group By SOA.StockRefDocId,SOA.StockRefV_SNo "
                    GCnCmd.CommandText += ") As Tmp On Tmp.StockRefDocId=Stock.DocId And Tmp.StockRefV_SNo=Stock.V_SNo "
                    GCnCmd.CommandText += "Where Stock.DocId In  "
                    GCnCmd.CommandText += "(Select SOA.StockRefDocId From StockOtherAdjustment SOA Where SOA.DocId='" & StrDocID & "') "
                    GCnCmd.ExecuteNonQuery()

                    GCnCmd.CommandText = "Delete From StockOtherAdjustment Where DocId='" & (StrDocID) & "'"
                    GCnCmd.ExecuteNonQuery()
                    GCnCmd.CommandText = "Delete From STaxTrn Where DocId='" & (StrDocID) & "'"
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
        If DTMaster.Rows.Count > 0 Then
            StrDocID = Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
            If CMain.FGetMaxNo("Select Count(*) Cnt From DataAudit Where DocId='" & StrDocID & "' ", Agl.Gcn) > 0 Then MsgBox("Record Has Been Audited. You Can Not Edit/ Delete This Record.") : Topctrl1.FButtonClick(99) : Exit Sub
            If CMain.FGetMaxNo("Select Count(*) From LedgerAdj_Temp Where Adj_DocId='" & Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")) & "' ", Agl.Gcn) > 0 Then MsgBox("Record Has Been Adjusted. You Cannot Edit/Delete This Record.") : Topctrl1.FButtonClick(99) : Exit Sub
            FManageDisplay(False)
            TxtVType.Enabled = False
            TxtParty.Focus()
        End If
    End Sub
    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox(ClsMain.MsgRecNotFnd) : Exit Sub
        Try
            agl.PubFindQry = "Select STT.DocId,STT.RecId As EntryNo,Convert(NVarChar(12),STT.V_Date,103) As VDate,CR.Name As Consignor, " & _
                    "STT.Description,PT.Name As Party,STT.ConsignorBill,STT.GAmount As Value " & _
                    "From STaxTrn STT Left Join " & _
                    "SubGroup CR On CR.SubCode=STT.Consignor Left Join " & _
                    "SubGroup PT On PT.SubCode=STT.PartyCode " & _
                    "Where STT.Site_Code='" & agl.PubSiteCode & "' And STT.V_Prefix='" & Agl.PubCompVPrefix & "' "
            agl.PubFindQryOrdBy = "EntryNo"
            'LIPublic.CreateAndSendArr("100,100,150,100,150,100,100")

            '*************** common code start *****************

            FrmFind.ShowDialog()

            FSearchRecord(agl.PubSearchRow)
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
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New SqlClient.SqlCommand
        Dim StrNarration As String = ""
        Dim StrRefDocIdTemp As String, StrRefVSNo As String
        Dim StrTemp() As String = Nothing
        Dim I As Integer

        Try
            If AgL.RequiredField(TxtVType, "VType") = False Then Exit Sub
            If AgL.RequiredField(TxtRecId, "Entry No") = False Then Exit Sub
            If AgL.RequiredField(TxtDate, "Date") = False Then Exit Sub
            If AgL.RequiredField(TxtParty, "Party") = False Then Exit Sub
            If AgL.RequiredField(TxtConsignor, "Consignor") = False Then Exit Sub
            If AgL.RequiredField(TxtType, "Type") = False Then Exit Sub
            If AgL.RequiredField(TxtAmount, "Amount") = False Then Exit Sub

            FGSTR.CalOtherAddDed(Val(TxtAmount.Text))
            If Not FGSTR.FChkGrid() Then Exit Sub
            If Not FCalculateLandedValue() Then Exit Sub

            StrDocID = ""
            If Topctrl1.Mode = "Add" Then
                TxtV_No.Text = ""
                TxtRecId.Text = ""
                StrDocID = CMain.FGetDoId(TxtV_No, TxtVType.Tag, "STaxTrn", "V_No", TxtDate.Text)
                FGenerateNo()
            Else
                FGenerateNo()
                StrDocID = Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode"))
            End If

            If Trim(Replace(StrDocID, "0", "")) = "" Then MsgBox(" Invalid " & "DocId.") : Exit Sub

            BlnTrans = True
            GCnCmd.Connection = Agl.Gcn
            GCnCmd.Transaction = Agl.Gcn.BeginTransaction(IsolationLevel.Serializable)

            GCnCmd.CommandText = "Update	Stock Set OtherAdjustment=0 "
            GCnCmd.CommandText += "From Stock "
            GCnCmd.CommandText += "Left Join (Select SOA.StockRefDocId,SOA.StockRefV_SNo,Sum(SOA.LandedValue) As LValue  "
            GCnCmd.CommandText += "From StockOtherAdjustment SOA  "
            GCnCmd.CommandText += "Where SOA.DocId<>'" & StrDocID & "' "
            GCnCmd.CommandText += "Group By SOA.StockRefDocId,SOA.StockRefV_SNo "
            GCnCmd.CommandText += ") As Tmp On Tmp.StockRefDocId=Stock.DocId And Tmp.StockRefV_SNo=Stock.V_SNo "
            GCnCmd.CommandText += "Where Stock.DocId In  "
            GCnCmd.CommandText += "(Select SOA.StockRefDocId From StockOtherAdjustment SOA Where SOA.DocId='" & StrDocID & "') "
            GCnCmd.ExecuteNonQuery()

            GCnCmd.CommandText = "Delete From StockOtherAdjustment Where DocId='" & (StrDocID) & "'"
            GCnCmd.ExecuteNonQuery()

            StrRefDocIdTemp = ""
            StrRefVSNo = ""
            If Trim(Replace(TxtVRef.Tag, "|:|", "")) <> "" Then
                StrTemp = Split(TxtVRef.Tag, "|:|")
                StrRefDocIdTemp = StrTemp(0)
                StrRefVSNo = StrTemp(1)
                StrTemp = Nothing
            End If

            If Topctrl1.Mode = "Add" Then
                GCnCmd.CommandText = "Insert Into STaxTrn(DocId,V_Type,V_No,RecId,V_Prefix,V_Date, " & _
                                     "Consignor,Consignee,VehicleNo,Description," & _
                                     "FromPlace,ToPlace,ConsigneeBill,ConsignorBill," & _
                                     "GAmount,Remark,EntryType, " & _
                                     "PreparedBy,ModifiedBy,Site_Code," & _
                                     "U_EntDt,U_AE,PartyCode,TDSCategory,TDSCalOn,PtyBillNo,PtyBillDt,VrRefDocID,VrRef_SNo) Values " & _
                                     "('" & (StrDocID) & "','" & TxtVType.Tag & "','" & Agl.Chk_Text(TxtV_No.Text) & "','" & Agl.Chk_Text(TxtRecId.Text) & "','" & Agl.Chk_Text(TxtV_No.Tag) & "'," & Agl.ConvertDate(TxtDate.Text) & ", " & _
                                     "" & Agl.Chk_Text(TxtConsignor.Tag) & "," & Agl.Chk_Text(TxtConsignee.Tag) & ",'" & Agl.Chk_Text(TxtVehicleNo.Text) & "','" & Agl.Chk_Text(TxtDescription.Text) & "'," & _
                                     "" & Agl.Chk_Text(TxtPlaceFrom.Tag) & "," & Agl.Chk_Text(TxtPlaceTo.Tag) & ",'" & Agl.Chk_Text(TxtConsigneeBNo.Text) & "','" & Agl.Chk_Text(TxtConsignorBNo.Text) & "'," & _
                                     "" & Val(TxtAmount.Text) & ",'" & Agl.Chk_Text(TxtRemark.Text) & "','" & TxtType.Tag & "'," & _
                                     "'" & Agl.PubUserName & "','" & Agl.PubUserName & "','" & agl.PubSiteCode & "'," & _
                                     "'" & Format(Agl.PubLoginDate, "Short Date") & "','" & Microsoft.VisualBasic.Left(Topctrl1.Mode, 1) & "'," & _
                                     "" & Agl.Chk_Text(TxtParty.Tag) & "," & Agl.Chk_Text(TxtTDSCategory.Tag) & "," & Val(TxtTDSCalOn.Text) & ",'" & Agl.Chk_Text(TxtPtyBillNo.Text) & "'," & Agl.ConvertDate(TxtPtyDate.Text) & "," & Agl.Chk_Text(StrRefDocIdTemp) & "," & Agl.Chk_Text(StrRefVSNo) & ")"
            Else
                GCnCmd.CommandText = "Update STaxTrn Set "
                GCnCmd.CommandText = GCnCmd.CommandText + "V_Date=" & Agl.ConvertDate(TxtDate.Text) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "RecId='" & Agl.Chk_Text(TxtRecId.Text) & "', "
                GCnCmd.CommandText = GCnCmd.CommandText + "EntryType='" & TxtType.Tag & "', "
                GCnCmd.CommandText = GCnCmd.CommandText + "PartyCode=" & Agl.Chk_Text(TxtParty.Tag) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "PtyBillNo='" & Agl.Chk_Text(TxtPtyBillNo.Text) & "',"
                GCnCmd.CommandText = GCnCmd.CommandText + "PtyBillDt=" & Agl.ConvertDate(TxtPtyDate.Text) & ","
                GCnCmd.CommandText = GCnCmd.CommandText + "TDSCategory=" & Agl.Chk_Text(TxtTDSCategory.Tag) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "TDSCalOn=" & Val(TxtTDSCalOn.Text) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "Consignor=" & Agl.Chk_Text(TxtConsignor.Tag) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "Consignee=" & Agl.Chk_Text(TxtConsignee.Tag) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "FromPlace=" & Agl.Chk_Text(TxtPlaceFrom.Tag) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "ToPlace=" & Agl.Chk_Text(TxtPlaceTo.Tag) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "VehicleNo='" & Agl.Chk_Text(TxtVehicleNo.Text) & "', "
                GCnCmd.CommandText = GCnCmd.CommandText + "Description='" & Agl.Chk_Text(TxtDescription.Text) & "', "
                GCnCmd.CommandText = GCnCmd.CommandText + "ConsigneeBill='" & Agl.Chk_Text(TxtConsigneeBNo.Text) & "', "
                GCnCmd.CommandText = GCnCmd.CommandText + "ConsignorBill='" & Agl.Chk_Text(TxtConsignorBNo.Text) & "', "
                GCnCmd.CommandText = GCnCmd.CommandText + "GAmount=" & Val(TxtAmount.Text) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "Remark='" & Agl.Chk_Text(TxtRemark.Text) & "', "
                GCnCmd.CommandText = GCnCmd.CommandText + "Transfered='N', "
                GCnCmd.CommandText = GCnCmd.CommandText + "ModifiedBy='" & Agl.PubUserName & "', "
                GCnCmd.CommandText = GCnCmd.CommandText + "U_EntDt='" & Format(Agl.PubLoginDate, "Short Date") & "', "
                GCnCmd.CommandText = GCnCmd.CommandText + "U_AE='" & Microsoft.VisualBasic.Left(Topctrl1.Mode, 1) & "', "
                GCnCmd.CommandText = GCnCmd.CommandText + "VrRefDocID=" & Agl.Chk_Text(StrRefDocIdTemp) & ", "
                GCnCmd.CommandText = GCnCmd.CommandText + "VrRef_SNo=" & Agl.Chk_Text(StrRefVSNo) & " "
                GCnCmd.CommandText = GCnCmd.CommandText + "Where DocId='" & (StrDocID) & "' "
            End If
            GCnCmd.ExecuteNonQuery()

            StrNarration = ""
            If Trim(TxtPtyBillNo.Text) <> "" Then StrNarration = "PB / GR No. " & TxtPtyBillNo.Text & " DT." & TxtPtyDate.Text & ", "
            If Trim(TxtConsigneeBNo.Text) <> "" Then StrNarration += "Through CB. No. " & TxtConsigneeBNo.Text & ", "
            If Trim(TxtVehicleNo.Text) <> "" Then StrNarration += "Vehicle No. " & TxtVehicleNo.Text

            If Trim(Replace(TxtVRef.Tag, "|:|", "")) = "" Then
                FGSTR.FSaveHeads(StrDocID, "DocId", GCnCmd, TxtV_No.Text, TxtRecId.Text, TxtVType.Tag, TxtV_No.Tag, TxtDate.Text, True, StrNarration)
                FSaveTDS("Ledger_Temp", GCnCmd)
                FSaveTDS("Ledger", GCnCmd)
            Else
                FGSTR.FSaveHeads(StrDocID, "DocId", GCnCmd, TxtV_No.Text, TxtRecId.Text, TxtVType.Tag, TxtV_No.Tag, TxtDate.Text, False, StrNarration)
            End If

            For I = 0 To FGLValue.Rows.Count - 1
                GCnCmd.CommandText = "Insert Into StockOtherAdjustment "
                GCnCmd.CommandText += "(DocId,V_SNo,StockRefDocId,StockRefV_SNo,"
                GCnCmd.CommandText += "LandedValue,AdjAmount) Values("
                GCnCmd.CommandText += "'" & StrDocID & "'," & I & ","
                GCnCmd.CommandText += "'" & FGLValue(GLDocId, I).Value & "',"
                GCnCmd.CommandText += "" & Val(FGLValue(GLV_SNo, I).Value) & ","
                GCnCmd.CommandText += "" & Val(FGLValue(GLEValue, I).Value) & ","
                GCnCmd.CommandText += "" & Val(FGLValue(GLAAmount, I).Value) & ")"
                GCnCmd.ExecuteNonQuery()
            Next

            If FGLValue.Rows.Count > 0 Then
                GCnCmd.CommandText = "Update	Stock Set OtherAdjustment=Tmp.LValue "
                GCnCmd.CommandText += "From Stock "
                GCnCmd.CommandText += "Left Join (Select SOA.StockRefDocId,SOA.StockRefV_SNo,Sum(SOA.LandedValue) As LValue "
                GCnCmd.CommandText += "From StockOtherAdjustment SOA  "
                GCnCmd.CommandText += "Where StockRefDocId In (" & StrRefDocId & ") "
                GCnCmd.CommandText += "Group By SOA.StockRefDocId,SOA.StockRefV_SNo "
                GCnCmd.CommandText += ") As Tmp On Tmp.StockRefDocId=Stock.DocId And Tmp.StockRefV_SNo=Stock.V_SNo "
                GCnCmd.CommandText += "Where Stock.DocId In (" & StrRefDocId & ") "
                GCnCmd.ExecuteNonQuery()
            End If

            GCnCmd.Transaction.Commit()
            BlnTrans = False

            If Topctrl1.Mode = "Add" Then
                Topctrl1.LblDocId.Text = StrDocID
                Topctrl1.FButtonClick(0)
                Exit Sub
            Else
                Topctrl1.SetDisp(True)
                MoveRec()
            End If

        Catch Ex As Exception
            If BlnTrans = True Then GCnCmd.Transaction.Rollback()
            MsgBox(Ex.Message)
        End Try
    End Sub
    Private Function FCalculateLandedValue() As Boolean
        Dim I As Integer, J As Integer
        Dim DTTemp As DataTable
        Dim DblTotalValue As Double = 0, DblDistributeValue As Double = 0, DblDistributed As Double = 0
        Dim BlnRtn As Boolean = True
        Dim IntPrvIndex As Integer

        FGLValue.Rows.Clear()
        StrRefDocId = ""
        For I = 0 To FGAdj.Rows.Count - 1
            If Trim(FGAdj(GADocId, I).Value) <> "" Then

                If Not Val(FGAdj(GAAmount, I).Value) > 0 Then
                    MsgBox("Please Define in Enviro" & " Vaild Amount.")
                    FGAdj(GAAmount, I).Selected = True
                    BlnRtn = False
                    FGAdj.Focus()
                    Exit For
                End If

                If Not BlnRtn Then
                    Exit For
                End If

                If StrRefDocId <> "" Then StrRefDocId = StrRefDocId & ","
                StrRefDocId = StrRefDocId & "'" & Trim(FGAdj(GADocId, I).Value) & "'"
                For J = I + 1 To FGAdj.Rows.Count - 1
                    If Trim(UCase(FGAdj(GADocId, I).Value)) = Trim(UCase(FGAdj(GADocId, J).Value)) Then
                        MsgBox(ClsMain.MsgDuplicate & " Adj Ref. No.")
                        FGAdj(GAEntryNo, J).Selected = True
                        BlnRtn = False
                        FGAdj.Focus()
                        Exit For
                    End If
                Next
            End If
        Next

        If BlnRtn Then
            If StrRefDocId <> "" Then
                IntPrvIndex = 0
                DTTemp = cmain.FGetDatTable("Select ST.DocId,ST.V_SNo,ST.LandedValue " & _
                                               "From Stock ST " & _
                                               "Where DocId In (" & StrRefDocId & ") " & _
                                               "Order By ST.DocId,ST.V_SNo", Agl.Gcn)
                For I = 0 To DTTemp.Rows.Count - 1
                    FGLValue.Rows.Add()
                    FGLValue(GLDocId, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("DocId"))
                    FGLValue(GLV_SNo, I).Value = Agl.Xnull(DTTemp.Rows(I).Item("V_SNo"))
                    FGLValue(GLOValue, I).Value = Agl.VNull(DTTemp.Rows(I).Item("LandedValue"))
                    DblTotalValue += Agl.VNull(DTTemp.Rows(I).Item("LandedValue"))

                    '==========================
                    If (DTTemp.Rows.Count - 1) = I Then GoTo LblCLV

                    If Trim(UCase(Agl.Xnull(DTTemp.Rows(I).Item("DocId")))) <> Trim(UCase(Agl.Xnull(DTTemp.Rows(I + 1).Item("DocId")))) Then
LblCLV:
                        DblDistributeValue = FGetAdjAmount(FGLValue(GLDocId, I).Value)
                        DblDistributed = 0
                        For J = IntPrvIndex To FGLValue.Rows.Count - 1
                            FGLValue(GLAAmount, J).Value = DblDistributeValue
                            If J = (FGLValue.Rows.Count - 1) Then
                                FGLValue(GLEValue, J).Value = Format(DblDistributeValue - DblDistributed, "0.00")
                            ElseIf DblTotalValue > 0 Then
                                FGLValue(GLEValue, J).Value = Format((Val(FGLValue(GLOValue, J).Value) * DblDistributeValue) / DblTotalValue, "0.00")
                                DblDistributed += Val(FGLValue(GLEValue, J).Value)
                            Else
                                FGLValue(GLEValue, J).Value = 0
                            End If
                        Next
                        DblTotalValue = 0
                        IntPrvIndex = (FGLValue.Rows.Count - 1) + 1
                    End If
                Next
                DTTemp.Dispose()
            End If
        End If

        FCalculateLandedValue = BlnRtn
    End Function
    Private Function FGetAdjAmount(ByVal StrSearchDocId As String) As Double
        Dim DblAdjAmount As Integer = -1
        Dim I As Integer

        For I = 0 To FGAdj.Rows.Count - 1
            If Trim(UCase(FGAdj(GADocId, I).Value)) = Trim(UCase(StrSearchDocId)) Then
                DblAdjAmount = Val(FGAdj(GAAmount, I).Value)
                Exit For
            End If
        Next

        FGetAdjAmount = DblAdjAmount
    End Function
    Private Sub FSaveTDS(ByVal StrLedger As String, ByVal GCnCmd As SqlClient.SqlCommand)
        Dim StrContraTDS As String, StrContraTDS_BF As String = "", StrNarration As String
        Dim DblCredit_Total As Double
        Dim DblDebit_Total As Double
        Dim DblCredit As Double
        Dim DblDebit As Double
        Dim J As Integer, IntV_SNo As Integer, Int_Prv_V_SNo As Integer
        Dim SQLDRTemp As SqlClient.SqlDataReader

        GCnCmd.CommandText = "Select IsNull(Max(V_SNo),0)+1 As Mx From " & StrLedger & " Where DocId='" & StrDocID & "'"
        SQLDRTemp = GCnCmd.ExecuteReader()
        SQLDRTemp.Read()
        Int_Prv_V_SNo = SQLDRTemp.Item("Mx")
        SQLDRTemp.Close()

        IntV_SNo = Int_Prv_V_SNo
        StrContraTDS = ""
        'For TDS
        If Trim(TxtTDSCategory.Tag) <> "" Then
            DblCredit_Total = 0
            DblDebit_Total = 0
            StrNarration = Microsoft.VisualBasic.Left("TDS Deducted Against " & TxtTDSCategory.Text & " On " & Val(TxtTDSCalOn.Text), 255)
            For J = 0 To FGMain.Rows.Count - 1
                DblCredit = Val(FGMain(GAmount, J).Value)
                DblDebit = 0
                DblDebit_Total = DblDebit_Total + DblCredit
                DblCredit_Total = 0

                FPrepareContraText(True, StrContraTDS_BF, TxtParty.Text, DblCredit, "Dr")
                If StrContraTDS <> "" Then StrContraTDS += vbCrLf
                FPrepareContraText(False, StrContraTDS, Trim(FGMain(GPosting, J).Value), DblCredit, "Cr")

                IntV_SNo = IntV_SNo + 1
                GCnCmd.CommandText = "Insert Into " & StrLedger & "(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr," & _
                                             "Narration,V_Type,V_No,V_Prefix,Site_Code,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,TDSDesc,TDSPer,TDS_Of_V_SNo,System_Generated,FormulaString,ContraText) Values " & _
                                             "('" & (StrDocID) & "','" & TxtRecId.Text & "'," & IntV_SNo & "," & Agl.ConvertDate(TxtDate.Text) & "," & Agl.Chk_Text(FGMain(GPostingCode, J).Value) & "," & Agl.Chk_Text(TxtParty.Tag) & ", " & _
                                             "" & DblDebit & "," & DblCredit & ", " & _
                                             "'" & Agl.Chk_Text(StrNarration) & " @ " & Trim(FGMain(GPersentage, J).Value) & "','" & TxtVType.Tag & "','" & TxtV_No.Text & "','" & TxtV_No.Tag & "'," & _
                                             "'" & agl.PubSiteCode & "','" & Agl.Chk_Text("") & "'," & _
                                             "" & Agl.ConvertDate("") & "," & Agl.Chk_Text(TxtTDSCategory.Tag) & "," & _
                                             "" & Val(TxtTDSCalOn.Text) & "," & Agl.Chk_Text(FGMain(GDescriptionCode, J).Value) & "," & Val(FGMain(GPersentage, J).Value) & "," & Int_Prv_V_SNo & ",'Y','" & FGMain(GFormula, J).Value & "','" & StrContraTDS_BF & "')"
                GCnCmd.ExecuteNonQuery()
            Next

            '======== Inserting Sum Of TDS In Party A/c 
            IntV_SNo = IntV_SNo + 1
            GCnCmd.CommandText = "Insert Into " & StrLedger & "(DocId,RecId,V_SNo,V_Date,SubCode,AmtDr,AmtCr," & _
                             "Narration,V_Type,V_No,V_Prefix,Site_Code,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,System_Generated,ContraText) Values " & _
                             "('" & (StrDocID) & "','" & TxtRecId.Text & "'," & IntV_SNo & "," & Agl.ConvertDate(TxtDate.Text) & "," & Agl.Chk_Text(TxtParty.Tag) & ", " & _
                             "" & DblDebit_Total & "," & DblCredit_Total & ", " & _
                             "'" & Agl.Chk_Text(StrNarration) & "','" & TxtVType.Tag & "','" & TxtV_No.Text & "','" & TxtV_No.Tag & "'," & _
                             "'" & agl.PubSiteCode & "','" & Agl.Chk_Text("") & "'," & _
                             "" & Agl.ConvertDate("") & ",'" & TxtTDSCategory.Tag & "'," & _
                             "" & Val(TxtTDSCalOn.Text) & ",'Y','" & StrContraTDS & "')"
            GCnCmd.ExecuteNonQuery()
        End If
    End Sub
    Private Sub FPrepareContraText(ByVal BlnOverWrite As Boolean, ByRef StrContraTextVar As String, _
    ByVal StrContraName As String, ByVal DblAmount As Double, ByVal StrDrCr As String)
        Dim IntNameMaxLen As Integer = 35, IntAmtMaxLen As Integer = 18, IntSpaceNeeded As Integer = 2

        If BlnOverWrite Then
            StrContraTextVar = Mid(Trim(StrContraName), 1, IntNameMaxLen) & Space((IntNameMaxLen + IntSpaceNeeded) - Len(Mid(Trim(StrContraName), 1, IntNameMaxLen))) & Space(IntAmtMaxLen - Len(Format(Val(DblAmount), "##,##,##,##,##0.00"))) & Format(Val(DblAmount), "##,##,##,##,##0.00") & " " & Trim(StrDrCr)
        Else
            StrContraTextVar += Mid(Trim(StrContraName), 1, IntNameMaxLen) & Space((IntNameMaxLen + IntSpaceNeeded) - Len(Mid(Trim(StrContraName), 1, IntNameMaxLen))) & Space(IntAmtMaxLen - Len(Format(Val(DblAmount), "##,##,##,##,##0.00"))) & Format(Val(DblAmount), "##,##,##,##,##0.00") & " " & Trim(StrDrCr)
        End If
    End Sub
    Private Sub FrmServiceTax_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Agl.FPaintForm(Me, e, Topctrl1.Height)
        LblBG.BackColor = Color.LemonChiffon
        LblTotalName.BackColor = Color.LemonChiffon
        LblTDSAmt.BackColor = Color.LemonChiffon
    End Sub
    Private Sub TxtDate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtDate.Validated, TxtAmount.Validated, TxtTDSCalOn.Validated, TxtPtyDate.Validated
        Select Case sender.Name
            Case TxtDate.Name
                TxtDate.Text = Agl.RetDate(TxtDate.Text)
                FGenerateNo()
            Case TxtPtyDate.Name
                sender.Text = Agl.RetDate(sender.Text)
            Case TxtAmount.Name
                FGSTR.CalOtherAddDed(Val(TxtAmount.Text))
            Case TxtTDSCalOn.Name
                FCalculate()
        End Select
    End Sub
    Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn
        Me.Cursor = Cursors.WaitCursor
        Try
            FPrintGlobal(Agl.Xnull(DTMaster.Rows(BMBMaster.Position).Item("SearchCode")), TxtVType.Tag, "Service Tax", Me, "")
        Catch Ex As Exception
        End Try
        Me.Cursor = Cursors.Default
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

        DblGrossValue = Val(TxtTDSCalOn.Text)
        LblTDSAmt.Text = "0.00"
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
            If BlnTDSROff Then
                FGMain(GAmount, I).Value = Format((DblValue * Val(FGMain(GPersentage, I).Value)) / 100, "0")
            Else
                FGMain(GAmount, I).Value = Format((DblValue * Val(FGMain(GPersentage, I).Value)) / 100, "0.00")
            End If
            FGMain(GAmount, I).Value = Format(Val(FGMain(GAmount, I).Value), "0.00")
            DblTotal = DblTotal + Val(FGMain(GAmount, I).Value)
        Next

        LblTDSAmt.Text = Format(DblTotal, "0.00")
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
    Private Sub FOpenForm(ByVal IntRowIndex As Integer)
        Dim FrmObjMDI As Object
        Dim FrmObj As Object
        Dim DTRow() As DataRow
        Dim StrModuleName As String = ""
        Dim StrMnuName As String = ""
        Dim StrMnuText As String = ""

        Try
            If StrSrvTaxAdjRefType = "" Then MsgBox("Please Define (Show Service Tax Adjustment Type) In Enviro.") : Exit Sub
            If Trim(FGAdj(GADocId, IntRowIndex).Value) = "" Then Exit Sub

            If DTVType Is Nothing Then DTVType = cmain.FGetDatTable("Select V_Type,MnuName,MnuText,MnuAttachedInModule From Voucher_Type Where IsNull(MnuName,'')<>'' And V_Type In (" & StrSrvTaxAdjRefType & ") Order By V_Type ", Agl.Gcn)
            DTRow = DTVType.Select("V_Type='" & Trim(FGAdj(GAType, IntRowIndex).Value) & "'")
            If DTRow.Length > 0 Then
                StrModuleName = Agl.Xnull(DTRow(0).Item("MnuAttachedInModule"))
                StrMnuName = AgL.XNull(DTRow(0).Item("MnuName"))
                StrMnuText = AgL.XNull(DTRow(0).Item("MnuText"))

                FrmObjMDI = Me.MdiParent
                FrmObj = FrmObjMDI.FOpenForm(StrModuleName, StrMnuName, StrMnuText)
                FrmObj.FSearchRecord(Trim(FGAdj(GADocId, IntRowIndex).Value))
            Else
                MsgBox("Define Details For This Voucher Type.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
