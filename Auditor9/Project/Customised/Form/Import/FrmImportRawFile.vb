Imports AgLibrary.ClsMain.agConstants

Public Class FrmImportRawFile
    Dim mQry As String = ""
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub
    Public Sub FImport()
        Dim mTrans As String = ""
        Dim mCode As String = ""
        Dim ErrorLog As String = ""
        Dim DtTemp As DataTable
        Dim DtMain As DataTable = Nothing
        Dim I As Integer
        'Dim FW As System.IO.StreamWriter = New System.IO.StreamWriter("C:\ImportLog.Txt", False, System.Text.Encoding.Default)
        Dim StrErrLog As String = ""
        mQry = "Select '' as Srl, 'City' as [Field Name], 'Text' as [Data Type], 10 as [Length] "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        Dim ObjFrmImport As New FrmImportFromExcel
        ObjFrmImport.Text = "Raw File Import"
        ObjFrmImport.Dgl1.DataSource = DtTemp
        ObjFrmImport.ShowDialog()

        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

        DtTemp = ObjFrmImport.P_DsExcelData.Tables(0)


        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            Dim mCityCode = AgL.GetMaxId("City", "CityCode", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

            Dim DtCity = DtTemp.DefaultView.ToTable(True, "City")
            For I = 0 To DtCity.Rows.Count - 1
                If AgL.XNull(DtCity.Rows(I)("City")) <> "" Then
                    If AgL.Dman_Execute("SELECT Count(*) From City where CityName = '" & AgL.XNull(DtCity.Rows(I)("City")) & "'", AgL.GCn).ExecuteScalar = 0 Then
                        Dim mCityCode_New = AgL.PubDivCode & AgL.PubSiteCode & (Convert.ToInt32(mCityCode.Replace(AgL.PubDivCode + AgL.PubSiteCode, "")) + I).ToString().PadLeft(4, "0")
                        mQry = " INSERT INTO City(CityCode, CityName, State, EntryBy, EntryDate, EntryType, EntryStatus)
                                    Select '" & mCityCode_New & "' As CityCode, " & AgL.Chk_Text(AgL.XNull(DtCity.Rows(I)("City"))) & " As City, 
                                    (SELECT Code From State where Description = '" & AgL.XNull(DtTemp.Rows(I)("State")) & "') As State, 
                                    '" & AgL.PubUserName & "' As EntryBy, " & AgL.Chk_Date(AgL.PubLoginDate) & " As EntryDate, 
                                    'Add' As EntryType, 'Open' As EntryStatus "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    End If
                End If
            Next

            AgL.ETrans.Commit()
            mTrans = "Commit"

        Catch ex As Exception
            AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
        If StrErrLog <> "" Then MsgBox(StrErrLog)
    End Sub
    Public Shared Sub ImportItemGroupTable(ItemGroupTable As FrmItemMaster.StructItemGroup)
        Dim mQry As String = ""
        If AgL.Dman_Execute("SELECT Count(*) From ItemGroup With (NoLock) where Description = " & AgL.Chk_Text(ItemGroupTable.Description) & " ", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar = 0 Then
            ItemGroupTable.ItemCategory = AgL.Dman_Execute("SELECT Code From ItemCategory With (NoLock) Where Replace(Description,' ','') = Replace('" & ItemGroupTable.ItemCategory & "',' ','')", IIf(AgL.PubServerName = "", AgL.GCn, AgL.GcnRead)).ExecuteScalar()

            mQry = " INSERT INTO Item(Code, Description, ItemCategory, ItemType, V_Type, Unit, Default_MarginPer, EntryBy, EntryDate, EntryType, EntryStatus, LockText, OMSId)
                    Select '" & ItemGroupTable.Code & "' As Code, " & AgL.Chk_Text(ItemGroupTable.Description) & " As Description, 
                    " & AgL.Chk_Text(ItemGroupTable.ItemCategory) & " As ItemCategory, 
                    " & AgL.Chk_Text(ItemGroupTable.ItemType) & " As ItemType, 
                    " & AgL.Chk_Text(ItemV_Type.ItemGroup) & " As ItemType, 
                    " & AgL.Chk_Text(ItemGroupTable.Unit) & " As Unit, 
                    0 As Default_MarginPer,
                    '" & ItemGroupTable.EntryBy & "' As EntryBy, 
                    " & AgL.Chk_Date(ItemGroupTable.EntryDate) & " As EntryDate, 
                    " & AgL.Chk_Text(ItemGroupTable.EntryType) & " As EntryType, 
                    " & AgL.Chk_Text(ItemGroupTable.EntryStatus) & " As EntryStatus, 
                    " & AgL.Chk_Text(ItemGroupTable.LockText) & " As LockText, 
                    " & AgL.Chk_Text(ItemGroupTable.OMSId) & " As OMSId "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        End If
    End Sub
End Class