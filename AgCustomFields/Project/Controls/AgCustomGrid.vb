Imports System.Windows.Forms
Imports System.Data.SQLite
Public Class AgCustomGrid
    Inherits AgControls.AgDataGrid

    Dim mNCat$ = "", mType$ = "", mQry$ = ""
    Dim Agl As AgLibrary.ClsMain
    Dim AgCl As New AgControls.AgLib

    Protected Const Col1CustomFields As String = "CustomFields"

    Protected Const Col1Heads1 As String = "Heads1"
    Protected Const Col1Data1 As String = "Data1"
    Protected Const Col1FLength1 As String = "FLength1"
    Protected Const Col1ValueType1 As String = "ValueType1"
    Protected Const Col1Value1 As String = "Value1"
    Protected Const Col1TableName1 As String = "TableName1"
    Protected Const Col1PrimaryField1 As String = "PrimaryField1"
    Protected Const Col1UpdateField1 As String = "UpdateField1"
    Protected Const Col1UpdateFieldType1 As String = "UpdateFieldType1"
    Protected Const Col1HeaderField1 As String = "HeaderField1"

    Protected Const Col1Heads2 As String = "Heads2"
    Protected Const Col1Data2 As String = "Data2"
    Protected Const Col1FLength2 As String = "FLength2"
    Protected Const Col1ValueType2 As String = "ValueType2"
    Protected Const Col1Value2 As String = "Value2"
    Protected Const Col1TableName2 As String = "TableName2"
    Protected Const Col1PrimaryField2 As String = "PrimaryField2"
    Protected Const Col1UpdateField2 As String = "UpdateField2"
    Protected Const Col1UpdateFieldType2 As String = "UpdateFieldType2"
    Protected Const Col1HeaderField2 As String = "HeaderField2"

    Private Const ValueType_Text As String = "Text"
    Private Const ValueType_Number As String = "Number"
    Private Const ValueType_Date As String = "Date"
    Private Const ValueType_List As String = "List"
    Private Const ValueType_Help As String = "Help"

    Dim mFrmType As ClsMain.EntryPointType = ClsMain.EntryPointType.Main
    Dim mCustom As String = "", mMnuText$ = ""
    Dim mSplitGrid As Boolean = False
    Dim mLineGrid As AgControls.AgDataGrid

    Public Enum AgCustomGridColumns
        Col_Heads1 = 1
        Col_Data1 = 2
        Col_Heads2 = 10
        Col_Data2 = 11
    End Enum

    Sub New()
        IniMe()
    End Sub

    Public Property AgLibVar() As AgLibrary.ClsMain
        Get
            Return Agl
        End Get
        Set(ByVal value As AgLibrary.ClsMain)
            Agl = value
        End Set
    End Property

    Public Property MnuText() As String
        Get
            Return mMnuText
        End Get
        Set(ByVal value As String)
            mMnuText = value
        End Set
    End Property

    Public Property AgLineGrid() As AgControls.AgDataGrid
        Get
            Return mLineGrid
        End Get

        Set(ByVal value As AgControls.AgDataGrid)
            mLineGrid = value
        End Set
    End Property

    Public Property SplitGrid() As Boolean
        Get
            Return SplitGrid
        End Get
        Set(ByVal value As Boolean)
            mSplitGrid = value
        End Set
    End Property

    Public Property AgCustom() As String
        Get
            Return mCustom
        End Get

        Set(ByVal value As String)
            mCustom = value
            If mCustom = "" Then
                Me.Visible = False
            Else
                Me.Visible = True
            End If
        End Set
    End Property

    Public Property FrmType() As ClsMain.EntryPointType
        Get
            Return mFrmType
        End Get
        Set(ByVal value As ClsMain.EntryPointType)
            mFrmType = value
        End Set
    End Property

    Public Function GetCustomField(ByVal V_Type As String) As String
        Dim DtTemp As DataTable
        mQry = "Select CustomFields From Voucher_Type Where V_Type = '" & V_Type & "' "
        DtTemp = Agl.FillData(mQry, Agl.GCn).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            Return DtTemp.Rows(0)(0)
        Else
            Return ""
            MsgBox("Custom Field not Defined for Selected Voucher Type." & vbCrLf & "Can't Continue.")
        End If
    End Function

    Sub IniMe()
        Me.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        Me.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Arial"), 9)
        Me.DefaultCellStyle.Font = New Font(New FontFamily("Arial"), 8)
        Me.ColumnHeadersHeight = 25
        Me.AllowUserToAddRows = False
        Me.ColumnCount = 0
        Me.RowHeadersVisible = False
        Me.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue
        Me.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        Me.AllowUserToResizeRows = False
        Me.MultiSelect = False
        Dim mColumnNamePrefix As String = "AgFG"
        With AgCl
            .AddAgTextColumn(Me, Col1CustomFields, 40, 5, Col1CustomFields, False, True, False)

            .AddAgTextColumn(Me, Col1Heads1, 150, 5, Col1Heads1, True, True, False)
            .AddAgTextColumn(Me, Col1Data1, 200, 0, Col1Data1, True, False)
            .AddAgTextColumn(Me, Col1FLength1, 240, 0, Col1FLength1, False, True)
            .AddAgTextColumn(Me, Col1ValueType1, 130, 0, Col1ValueType1, False, True)
            .AddAgTextColumn(Me, Col1Value1, 50, 0, Col1Value1, False, True)
            .AddAgTextColumn(Me, Col1TableName1, 50, 0, Col1TableName1, False, True)
            .AddAgTextColumn(Me, Col1PrimaryField1, 50, 0, Col1PrimaryField1, False, True)
            .AddAgTextColumn(Me, Col1UpdateField1, 50, 0, Col1UpdateField1, False, True)
            .AddAgTextColumn(Me, Col1UpdateFieldType1, 50, 0, Col1UpdateFieldType1, False, True)
            .AddAgTextColumn(Me, Col1HeaderField1, 50, 0, Col1HeaderField1, False, True)

            .AddAgTextColumn(Me, Col1Heads2, 150, 5, Col1Heads2, False, True, False)
            .AddAgTextColumn(Me, Col1Data2, 200, 0, Col1Data2, False, False)
            .AddAgTextColumn(Me, Col1FLength2, 240, 0, Col1FLength2, False, True)
            .AddAgTextColumn(Me, Col1ValueType2, 130, 0, Col1ValueType2, False, True)
            .AddAgTextColumn(Me, Col1Value2, 50, 0, Col1Value2, False, True)
            .AddAgTextColumn(Me, Col1TableName2, 50, 0, Col1TableName2, False, True)
            .AddAgTextColumn(Me, Col1PrimaryField2, 50, 0, Col1PrimaryField2, False, True)
            .AddAgTextColumn(Me, Col1UpdateField2, 50, 0, Col1UpdateField2, False, True)
            .AddAgTextColumn(Me, Col1UpdateFieldType2, 50, 0, Col1UpdateFieldType2, False, True)
            .AddAgTextColumn(Me, Col1HeaderField2, 50, 0, Col1HeaderField2, False, True)
        End With
        Me.EnableHeadersVisualStyles = False
        Me.AgSkipReadOnlyColumns = True
        Me.DefaultCellStyle.WrapMode = DataGridViewTriState.True
    End Sub

    Public Sub Save_TransFooter(ByVal DocID As String, ByVal mConn As Object,
                                ByVal mCmd As Object, Optional ByVal UID As String = "")
        Dim mQry$, I%
        Dim Sr% = 0
        Dim mTableName$ = ""

        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = "Delete From CustomFields_Trans Where DocID = '" & DocID & "'"
            Agl.Dman_ExecuteNonQry(mQry, mConn, mCmd)

            For I = 0 To Me.RowCount - 1
                If Me.Item(Col1Heads1, I).Value <> "" Then
                    Sr += 1
                    mQry = "INSERT INTO CustomFields_Trans(UID, DocID, Sr, CustomFields, MnuText, " &
                           " Head, Data, Value, Value_Type, FLength, TableName, PrimaryField, UpdateField, UpdateFieldType) " &
                           " VALUES(" & Agl.Chk_Text(UID) & ", " &
                           " " & Agl.Chk_Text(DocID) & ",  " & Sr & ", " &
                           " " & Agl.Chk_Text(Me.Item(Col1CustomFields, I).Value) & ", " &
                           " " & Agl.Chk_Text(mMnuText) & ", " &
                           " " & Agl.Chk_Text(Me.AgSelectedValue(Col1Heads1, I)) & ", " &
                           " " & Agl.Chk_Text(IIf(Agl.StrCmp(Me.Item(Col1ValueType1, I).Value, ValueType_Help), Me.AgSelectedValue(Col1Data1, I), Me.Item(Col1Data1, I).Value)) & ", " &
                           " " & Agl.Chk_Text(Me.Item(Col1Value1, I).Value) & ", " &
                           " " & Agl.Chk_Text(Me.Item(Col1ValueType1, I).Value) & ", " &
                           " " & Agl.Chk_Text(Me.Item(Col1FLength1, I).Value) & ", " &
                           " " & Agl.Chk_Text(Me.Item(Col1TableName1, I).Value) & ", " &
                           " " & Agl.Chk_Text(Me.Item(Col1PrimaryField1, I).Value) & ", " &
                           " " & Agl.Chk_Text(Me.Item(Col1UpdateField1, I).Value) & ", " &
                           " " & Agl.Chk_Text(Me.Item(Col1UpdateFieldType1, I).Value) & " " &
                           " ) "
                    Agl.Dman_ExecuteNonQry(mQry, mConn, mCmd)

                    Call ProcUpdateHeader(DocID, Me.Item(Col1TableName1, I).Value,
                                          Me.Item(Col1UpdateField1, I).Value,
                                          Me.Item(Col1UpdateFieldType1, I).Value, Me.Item(Col1PrimaryField1, I).Value,
                                          IIf(Agl.StrCmp(Me.Item(Col1ValueType1, I).Value, ValueType_Help), Me.AgSelectedValue(Col1Data1, I), Me.Item(Col1Data1, I).Value),
                                          mConn, mCmd)
                End If

                If mSplitGrid Then
                    If Me.Item(Col1Heads2, I).Value <> "" Then
                        Sr += 1
                        mQry = "INSERT INTO CustomFields_Trans(UID, DocID, Sr, CustomFields, MnuText, " &
                               " Head, Data, Value, Value_Type, FLength, TableName, PrimaryField, UpdateField, UpdateFieldType) " &
                               " VALUES(" & Agl.Chk_Text(UID) & ", " &
                               " " & Agl.Chk_Text(DocID) & ",  " & Sr & ", " &
                               " " & Agl.Chk_Text(Me.Item(Col1CustomFields, I).Value) & ", " &
                               " " & Agl.Chk_Text(mMnuText) & ", " &
                               " " & Agl.Chk_Text(Me.AgSelectedValue(Col1Heads2, I)) & ", " &
                               " " & Agl.Chk_Text(IIf(Agl.StrCmp(Me.Item(Col1ValueType2, I).Value, ValueType_Help), Me.AgSelectedValue(Col1Data2, I), Me.Item(Col1Data2, I).Value)) & ", " &
                               " " & Agl.Chk_Text(Me.Item(Col1Value2, I).Value) & ", " &
                               " " & Agl.Chk_Text(Me.Item(Col1ValueType2, I).Value) & ", " &
                               " " & Agl.Chk_Text(Me.Item(Col1FLength2, I).Value) & ", " &
                               " " & Agl.Chk_Text(Me.Item(Col1TableName2, I).Value) & ", " &
                               " " & Agl.Chk_Text(Me.Item(Col1PrimaryField2, I).Value) & ", " &
                               " " & Agl.Chk_Text(Me.Item(Col1UpdateField2, I).Value) & ", " &
                               " " & Agl.Chk_Text(Me.Item(Col1UpdateFieldType2, I).Value) & " " &
                               " ) "
                        Agl.Dman_ExecuteNonQry(mQry, mConn, mCmd)

                        Call ProcUpdateHeader(DocID, Me.Item(Col1TableName2, I).Value,
                                              Me.Item(Col1UpdateField2, I).Value, Me.Item(Col1UpdateFieldType2, I).Value,
                                              Me.Item(Col1PrimaryField2, I).Value,
                                              IIf(Agl.StrCmp(Me.Item(Col1ValueType2, I).Value, ValueType_Help), Me.AgSelectedValue(Col1Data2, I), Me.Item(Col1Data2, I).Value),
                                              mConn, mCmd)
                    End If
                End If
            Next
        Else
            mQry = "Delete From CustomFields_Trans_Log Where UID = '" & UID & "'"
            Agl.Dman_ExecuteNonQry(mQry, mConn, mCmd)

            For I = 0 To Me.RowCount - 1
                If Me.Item(Col1Heads1, I).Value <> "" Then
                    Sr += 1
                    mQry = "INSERT INTO CustomFields_Trans_Log(UID, DocID, Sr, CustomFields, MnuText, " &
                           " Head, Data, Value, Value_Type, FLength, TableName, PrimaryField, UpdateField, UpdateFieldType) " &
                           " VALUES(" & Agl.Chk_Text(UID) & ",  " &
                           " " & Agl.Chk_Text(DocID) & ",  " & Sr & ", " &
                           " " & Agl.Chk_Text(Me.Item(Col1CustomFields, I).Value) & ", " &
                           " " & Agl.Chk_Text(mMnuText) & ", " &
                           " " & Agl.Chk_Text(Me.AgSelectedValue(Col1Heads1, I)) & ", " &
                           " " & Agl.Chk_Text(IIf(Agl.StrCmp(Me.Item(Col1ValueType1, I).Value, ValueType_Help), Me.AgSelectedValue(Col1Data1, I), Me.Item(Col1Data1, I).Value)) & ", " &
                           " " & Agl.Chk_Text(Me.Item(Col1Value1, I).Value) & ", " &
                           " " & Agl.Chk_Text(Me.Item(Col1ValueType1, I).Value) & ", " &
                           " " & Agl.Chk_Text(Me.Item(Col1FLength1, I).Value) & ", " &
                           " " & Agl.Chk_Text(Me.Item(Col1TableName1, I).Value) & ", " &
                           " " & Agl.Chk_Text(Me.Item(Col1PrimaryField1, I).Value) & ", " &
                           " " & Agl.Chk_Text(Me.Item(Col1UpdateField1, I).Value) & ", " &
                           " " & Agl.Chk_Text(Me.Item(Col1UpdateFieldType1, I).Value) & " " &
                           " ) "
                    Agl.Dman_ExecuteNonQry(mQry, mConn, mCmd)

                    Call ProcUpdateHeader(UID, Me.Item(Col1TableName1, I).Value,
                                         Me.Item(Col1UpdateField1, I).Value,
                                         Me.Item(Col1UpdateFieldType1, I).Value, Me.Item(Col1PrimaryField1, I).Value,
                                         IIf(Agl.StrCmp(Me.Item(Col1ValueType1, I).Value, ValueType_Help), Me.AgSelectedValue(Col1Data1, I), Me.Item(Col1Data1, I).Value),
                                         mConn, mCmd)

                End If

                If mSplitGrid Then
                    If Me.Item(Col1Heads2, I).Value <> "" Then
                        Sr += 1
                        mQry = "INSERT INTO CustomFields_Trans_Log(UID, DocID, Sr, CustomFields, MnuText, " &
                               " Head, Data, Value, Value_Type, FLength, TableName, PrimaryField, UpdateField, UpdateFieldType) " &
                               " VALUES(" & Agl.Chk_Text(UID) & ", " &
                               " " & Agl.Chk_Text(DocID) & ",  " & Sr & ", " &
                               " " & Agl.Chk_Text(Me.Item(Col1CustomFields, I).Value) & ", " &
                               " " & Agl.Chk_Text(mMnuText) & ", " &
                               " " & Agl.Chk_Text(Me.AgSelectedValue(Col1Heads2, I)) & ", " &
                               " " & Agl.Chk_Text(IIf(Agl.StrCmp(Me.Item(Col1ValueType2, I).Value, ValueType_Help), Me.AgSelectedValue(Col1Data2, I), Me.Item(Col1Data2, I).Value)) & ", " &
                               " " & Agl.Chk_Text(Me.Item(Col1Value2, I).Value) & ", " &
                               " " & Agl.Chk_Text(Me.Item(Col1ValueType2, I).Value) & ", " &
                               " " & Agl.Chk_Text(Me.Item(Col1FLength2, I).Value) & ", " &
                               " " & Agl.Chk_Text(Me.Item(Col1TableName2, I).Value) & ", " &
                               " " & Agl.Chk_Text(Me.Item(Col1PrimaryField2, I).Value) & ", " &
                               " " & Agl.Chk_Text(Me.Item(Col1UpdateField2, I).Value) & ", " &
                               " " & Agl.Chk_Text(Me.Item(Col1UpdateFieldType2, I).Value) & " " &
                               " ) "
                        Agl.Dman_ExecuteNonQry(mQry, mConn, mCmd)

                        Call ProcUpdateHeader(UID, Me.Item(Col1TableName2, I).Value,
                                              Me.Item(Col1UpdateField2, I).Value, Me.Item(Col1UpdateFieldType2, I).Value,
                                              Me.Item(Col1PrimaryField2, I).Value,
                                              IIf(Agl.StrCmp(Me.Item(Col1ValueType2, I).Value, ValueType_Help), Me.AgSelectedValue(Col1Data2, I), Me.Item(Col1Data2, I).Value),
                                              mConn, mCmd)
                    End If
                End If
            Next
        End If
        Call AgCl.GridSetiingWriteXml(MnuText + "-CustomGrid", Me)
    End Sub

    Public Sub MoveRec_TransFooter(ByVal mSearchCode As String)
        Dim mQry$ = ""
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0, bRowIndex As Integer = 0

        Try
            If FrmType = ClsMain.EntryPointType.Main Then
                mQry = "Select F.* " & _
                        " from CustomFields_Trans F " & _
                        " Where F.DocID = '" & mSearchCode & "' " & _
                        " And F.MnuText = '" & mMnuText & "'   " & _
                        " Order By F.Sr "
            Else
                mQry = "Select F.* " & _
                        " from CustomFields_Trans_Log F " & _
                        " Where F.UID = '" & mSearchCode & "' " & _
                        " And F.MnuText = '" & mMnuText & "'   " & _
                        " Order By F.Sr "
            End If
            DtTemp = Agl.FillData(mQry, Agl.GCn).Tables(0)

            With DtTemp
                Me.RowCount = 1 : Me.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Me.Rows.Add()
                        Me.Item(Col1CustomFields, bRowIndex).Value = Agl.XNull(.Rows(I)("CustomFields"))
                        Me.AgSelectedValue(Col1Heads1, bRowIndex) = Agl.XNull(.Rows(I)("Head"))
                        Me.Item(Col1Value1, bRowIndex).Value = Agl.XNull(.Rows(I)("Value"))
                        Me.Item(Col1ValueType1, bRowIndex).Value = Agl.XNull(.Rows(I)("Value_Type"))
                        Me.Item(Col1FLength1, bRowIndex).Value = Agl.XNull(.Rows(I)("FLength"))
                        If Agl.StrCmp(Agl.XNull(.Rows(I)("Value_Type")), ValueType_Help) Then
                            Me.AgHelpDataSet(Col1Data1) = Agl.FillData(Agl.XNull(.Rows(I)("Value")), Agl.GCn)
                            Me.AgSelectedValue(Col1Data1, bRowIndex) = IIf(Agl.StrCmp(Agl.XNull(.Rows(I)("Value_Type")), ValueType_Number), Agl.VNull(.Rows(I)("Data")), Agl.XNull(.Rows(I)("Data")))
                        Else
                            Me.Item(Col1Data1, bRowIndex).Value = IIf(Agl.StrCmp(Agl.XNull(.Rows(I)("Value_Type")), ValueType_Number), Agl.VNull(.Rows(I)("Data")), Agl.XNull(.Rows(I)("Data")))
                        End If
                        Me.Item(Col1TableName1, bRowIndex).Value = Agl.XNull(.Rows(I)("TableName"))
                        Me.Item(Col1PrimaryField1, bRowIndex).Value = Agl.XNull(.Rows(I)("PrimaryField"))
                        Me.Item(Col1UpdateField1, bRowIndex).Value = Agl.XNull(.Rows(I)("UpdateField"))
                        Me.Item(Col1UpdateFieldType1, bRowIndex).Value = Agl.XNull(.Rows(I)("UpdateFieldType"))

                        If mSplitGrid Then
                            I = I + 1
                            If I <= .Rows.Count - 1 Then
                                Me.AgSelectedValue(Col1Heads2, bRowIndex) = Agl.XNull(.Rows(I)("Head"))
                                Me.Item(Col1Value2, bRowIndex).Value = Agl.XNull(.Rows(I)("Value"))
                                Me.Item(Col1ValueType2, bRowIndex).Value = Agl.XNull(.Rows(I)("Value_Type"))
                                Me.Item(Col1FLength2, bRowIndex).Value = Agl.XNull(.Rows(I)("FLength"))
                                If Agl.StrCmp(Agl.XNull(.Rows(I)("Value_Type")), ValueType_Help) Then
                                    Me.AgHelpDataSet(Col1Data2) = Agl.FillData(Agl.XNull(.Rows(I)("Value")), Agl.GCn)
                                    Me.AgSelectedValue(Col1Data2, bRowIndex) = IIf(Agl.StrCmp(Agl.XNull(.Rows(I)("Value_Type")), ValueType_Number), Agl.VNull(.Rows(I)("Data")), Agl.XNull(.Rows(I)("Data")))
                                Else
                                    Me.Item(Col1Data2, bRowIndex).Value = IIf(Agl.StrCmp(Agl.XNull(.Rows(I)("Value_Type")), ValueType_Number), Agl.VNull(.Rows(I)("Data")), Agl.XNull(.Rows(I)("Data")))
                                End If
                                Me.Item(Col1TableName2, bRowIndex).Value = Agl.XNull(.Rows(I)("TableName"))
                                Me.Item(Col1PrimaryField2, bRowIndex).Value = Agl.XNull(.Rows(I)("PrimaryField"))
                                Me.Item(Col1UpdateField2, bRowIndex).Value = Agl.XNull(.Rows(I)("UpdateField"))
                                Me.Item(Col1UpdateFieldType2, bRowIndex).Value = Agl.XNull(.Rows(I)("UpdateFieldType"))
                            End If
                        End If
                        bRowIndex = bRowIndex + 1
                    Next I
                End If
            End With
            Call AgCl.GridSetiingShowXml(MnuText + "-CustomGrid", Me)
            Call ProcAdjustGrid()
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & " In MoveRec_TransFooter Procedure of AgCustomField", MsgBoxStyle.Exclamation)
        Finally
            DtTemp.Dispose()
        End Try
    End Sub

    Public Sub Ini_Grid(Optional ByVal mSearchCode As String = "")
        Dim I As Integer = 0, bRowIndex As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Dim bData$ = ""
        Try
            Me.AgHelpDataSet(Col1Heads1, 1) = Agl.FillData("Select Code, Description From CustomFieldsHead Order By Description", Agl.GCn)
            If mSplitGrid Then
                Me.AgHelpDataSet(Col1Heads2, 1) = Agl.FillData("Select Code, Description From CustomFieldsHead Order By Description", Agl.GCn)
            End If

            If mSearchCode = "" Then
                mQry = " SELECT L.Code AS CustomFields, L.Head, '' AS Data, L.FLength, L.Value, L.Value_Type, " & _
                        " H.TableName, H.PrimaryField, L.UpdateField, L.UpdateFieldType, L.HeaderField  " & _
                        " FROM CustomFieldsDetail L  " & _
                        " LEFT JOIN CustomFields H ON L.Code = H.Code " & _
                        " WHERE L.Code = '" & mCustom & "' " & _
                        " And IfNull(L.Active,0) <> 0 "
            Else
                If FrmType = ClsMain.EntryPointType.Main Then
                    mQry = " SELECT H.* FROM CustomFields_Trans H WHERE H.DocID = '" & mSearchCode & "' And H.MnuText = '" & mMnuText & "'  ORDER BY H.Sr "
                Else
                    mQry = " SELECT H.* FROM CustomFields_Trans_Log H WHERE H.UID = '" & mSearchCode & "' And H.MnuText = '" & mMnuText & "' ORDER BY H.Sr "
                End If
            End If
            DtTemp = Agl.FillData(mQry, Agl.GCn).Tables(0)

            With DtTemp
                Me.RowCount = 1 : Me.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Me.Rows.Add()
                        Me.Item(Col1CustomFields, bRowIndex).Value = Agl.XNull(.Rows(I)("CustomFields"))
                        Me.AgSelectedValue(Col1Heads1, bRowIndex) = Agl.XNull(.Rows(I)("Head"))
                        Me.Item(Col1Value1, bRowIndex).Value = Agl.XNull(.Rows(I)("Value"))
                        Me.Item(Col1ValueType1, bRowIndex).Value = Agl.XNull(.Rows(I)("Value_Type"))
                        Me.Item(Col1FLength1, bRowIndex).Value = Agl.XNull(.Rows(I)("FLength"))
                        If Agl.StrCmp(Agl.XNull(.Rows(I)("Value_Type")), ValueType_Help) Then
                            Me.AgHelpDataSet(Col1Data1) = Agl.FillData(Agl.XNull(.Rows(I)("Value")), Agl.GCn)
                            Me.AgSelectedValue(Col1Data1, bRowIndex) = IIf(Agl.StrCmp(Agl.XNull(.Rows(I)("Value_Type")), ValueType_Number), Agl.VNull(.Rows(I)("Data")).ToString, Agl.XNull(.Rows(I)("Data")))
                        Else
                            Me.Item(Col1Data1, bRowIndex).Value = IIf(Agl.StrCmp(Agl.XNull(.Rows(I)("Value_Type")), ValueType_Number), Agl.VNull(.Rows(I)("Data")).ToString, Agl.XNull(.Rows(I)("Data")))
                        End If
                        Me.Item(Col1TableName1, bRowIndex).Value = Agl.XNull(.Rows(I)("TableName"))
                        Me.Item(Col1PrimaryField1, bRowIndex).Value = Agl.XNull(.Rows(I)("PrimaryField"))
                        Me.Item(Col1UpdateField1, bRowIndex).Value = Agl.XNull(.Rows(I)("UpdateField"))
                        Me.Item(Col1UpdateFieldType1, bRowIndex).Value = Agl.XNull(.Rows(I)("UpdateFieldType"))
                        Me.Item(Col1HeaderField1, bRowIndex).Value = Agl.XNull(.Rows(I)("HeaderField"))

                        If mSplitGrid Then
                            I = I + 1
                            If I <= .Rows.Count - 1 Then
                                Me.AgSelectedValue(Col1Heads2, bRowIndex) = Agl.XNull(.Rows(I)("Head"))
                                Me.Item(Col1Value2, bRowIndex).Value = Agl.XNull(.Rows(I)("Value"))
                                Me.Item(Col1ValueType2, bRowIndex).Value = Agl.XNull(.Rows(I)("Value_Type"))
                                Me.Item(Col1FLength2, bRowIndex).Value = Agl.XNull(.Rows(I)("FLength"))
                                If Agl.StrCmp(Agl.XNull(.Rows(I)("Value_Type")), ValueType_Help) Then
                                    Me.AgHelpDataSet(Col1Data2) = Agl.FillData(Agl.XNull(.Rows(I)("Value")), Agl.GCn)
                                    Me.AgSelectedValue(Col1Data2, bRowIndex) = IIf(Agl.StrCmp(Agl.XNull(.Rows(I)("Value_Type")), ValueType_Number), Agl.VNull(.Rows(I)("Data")).ToString, Agl.XNull(.Rows(I)("Data")))
                                Else
                                    Me.Item(Col1Data2, bRowIndex).Value = IIf(Agl.StrCmp(Agl.XNull(.Rows(I)("Value_Type")), ValueType_Number), Agl.VNull(.Rows(I)("Data")).ToString, Agl.XNull(.Rows(I)("Data")))
                                End If
                                Me.Item(Col1TableName2, bRowIndex).Value = Agl.XNull(.Rows(I)("TableName"))
                                Me.Item(Col1PrimaryField2, bRowIndex).Value = Agl.XNull(.Rows(I)("PrimaryField"))
                                Me.Item(Col1UpdateField2, bRowIndex).Value = Agl.XNull(.Rows(I)("UpdateField"))
                                Me.Item(Col1UpdateFieldType2, bRowIndex).Value = Agl.XNull(.Rows(I)("UpdateFieldType"))
                                Me.Item(Col1HeaderField2, bRowIndex).Value = Agl.XNull(.Rows(I)("HeaderField"))
                            End If
                        End If
                        bRowIndex = bRowIndex + 1
                    Next I
                End If
            End With
            Call ProcAdjustGrid()
            Agl.GridDesign(Me)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProcAdjustGrid()
        Try
            Call AgCl.GridSetiingShowXml(MnuText + "-CustomGrid", Me)
            With Me
                .Columns(Col1Heads1).HeaderText = "Head"
                .Columns(Col1Heads2).HeaderText = "Head"
                .Columns(Col1Data1).HeaderText = "Data"
                .Columns(Col1Data2).HeaderText = "Data"
                If mSplitGrid Then
                    Me.Columns(Col1Heads2).Visible = True
                    Me.Columns(Col1Data2).Visible = True
                Else
                    Me.Columns(Col1Heads2).Visible = False
                    Me.Columns(Col1Data2).Visible = False
                End If
            End With

            Me.AutoResizeRows()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Me_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Me.CellEnter
        Dim bRowIndex As Integer = 0
        Dim bColumnIndex As Integer = 0

        Try
            bRowIndex = Me.CurrentCell.RowIndex
            bColumnIndex = Me.CurrentCell.ColumnIndex
            If Me.CurrentCell Is Nothing Then Exit Sub
            Select Case Me.Columns(Me.CurrentCell.ColumnIndex).Name
                Case Col1Data1
                    Select Case Me.Item(Col1ValueType1, bRowIndex).Value
                        Case ValueType_Text
                            CType(Me.Columns(Col1Data1), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
                            CType(Me.Columns(Col1Data1), AgControls.AgTextColumn).MaxInputLength = Val(Me.Item(Col1FLength1, Me.CurrentCell.RowIndex).Value)
                            Me.AgHelpDataSet(Col1Data1) = Nothing

                        Case ValueType_Number
                            CType(Me.Columns(Col1Data1), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                            If Strings.InStr(Me.Item(Col1FLength1, bRowIndex).Value, ",") Then
                                Dim LenArr() As String = Nothing
                                LenArr = Split(Me.Item(Col1FLength1, bRowIndex).Value, ",")
                                CType(Me.Columns(Col1Data1), AgControls.AgTextColumn).AgNumberLeftPlaces = Val(LenArr(0))
                                CType(Me.Columns(Col1Data1), AgControls.AgTextColumn).AgNumberRightPlaces = Val(LenArr(1))
                            Else
                                CType(Me.Columns(Col1Data1), AgControls.AgTextColumn).MaxInputLength = Val(Me.Item(Col1FLength1, Me.CurrentCell.RowIndex).Value)
                            End If
                            Me.AgHelpDataSet(Col1Data1) = Nothing

                        Case ValueType_Date
                            CType(Me.Columns(Col1Data1), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
                            Me.AgHelpDataSet(Col1Data1) = Nothing

                        Case ValueType_List
                            CType(Me.Columns(Col1Data1), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
                            If Me.Parent.GetType.Equals(GetType(TabPage)) Then
                                Me.AgHelpDataSet(Col1Data1, , Me.Parent.Top + Me.Parent.Parent.Top, Me.Parent.Left + Me.Parent.Parent.Left) = Agl.FillData(FunGetListQry(Me.Item(Col1Value1, bRowIndex).Value), Agl.GCn)
                            Else
                                Me.AgHelpDataSet(Col1Data1) = Agl.FillData(FunGetListQry(Me.Item(Col1Value1, bRowIndex).Value), Agl.GCn)
                            End If

                        Case ValueType_Help
                            CType(Me.Columns(Col1Data1), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
                            If Me.Parent.GetType.Equals(GetType(TabPage)) Then
                                Me.AgHelpDataSet(Col1Data1, , Me.Parent.Top + Me.Parent.Parent.Top, Me.Parent.Left + Me.Parent.Parent.Left) = Agl.FillData(Me.Item(Col1Value1, bRowIndex).Value, Agl.GCn)
                            Else
                                Me.AgHelpDataSet(Col1Data1) = Agl.FillData(Me.Item(Col1Value1, bRowIndex).Value, Agl.GCn)
                            End If

                    End Select

                Case Col1Data2
                    Select Case Me.Item(Col1ValueType2, bRowIndex).Value
                        Case ValueType_Text
                            CType(Me.Columns(Col1Data2), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
                            CType(Me.Columns(Col1Data2), AgControls.AgTextColumn).MaxInputLength = Val(Me.Item(Col1FLength2, Me.CurrentCell.RowIndex).Value)
                            Me.AgHelpDataSet(Col1Data2) = Nothing

                        Case ValueType_Number
                            CType(Me.Columns(Col1Data2), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Number_Value
                            If Strings.InStr(Me.Item(Col1FLength2, bRowIndex).Value, ",") Then
                                Dim LenArr() As String = Nothing
                                LenArr = Split(Me.Item(Col1FLength2, bRowIndex).Value, ",")
                                CType(Me.Columns(Col1Data2), AgControls.AgTextColumn).AgNumberLeftPlaces = Val(LenArr(0))
                                CType(Me.Columns(Col1Data2), AgControls.AgTextColumn).AgNumberRightPlaces = Val(LenArr(1))
                            Else
                                CType(Me.Columns(Col1Data2), AgControls.AgTextColumn).MaxInputLength = Val(Me.Item(Col1FLength2, Me.CurrentCell.RowIndex).Value)
                            End If
                            Me.AgHelpDataSet(Col1Data2) = Nothing

                        Case ValueType_Date
                            CType(Me.Columns(Col1Data2), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Date_Value
                            Me.AgHelpDataSet(Col1Data2) = Nothing

                        Case ValueType_List
                            CType(Me.Columns(Col1Data2), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
                            Me.AgHelpDataSet(Col1Data2) = Agl.FillData(FunGetListQry(Me.Item(Col1Value2, bRowIndex).Value), Agl.GCn)

                        Case ValueType_Help
                            CType(Me.Columns(Col1Data2), AgControls.AgTextColumn).AgValueType = AgControls.AgTextColumn.TxtValueType.Text_Value
                            Me.AgHelpDataSet(Col1Data2) = Agl.FillData(Me.Item(Col1Value2, bRowIndex).Value, Agl.GCn)
                    End Select
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Function FunGetListQry(ByVal List As String) As String
        Dim bListArr() As String = Nothing
        Dim I As Integer = 0
        Try
            mQry = ""
            bListArr = Split(List, ",")
            For I = 0 To bListArr.Length - 1
                If bListArr(I) <> "" Then
                    If mQry = "" Then
                        mQry = " Select '" & bListArr(I) & "' As Code, '" & bListArr(I) & "' As Description "
                    Else
                        mQry += "Union All Select '" & bListArr(I) & "' As Code, '" & bListArr(I) & "' As Description "
                    End If
                End If
            Next
            FunGetListQry = mQry
        Catch ex As Exception
            FunGetListQry = ""
            MsgBox(ex.Message)
        End Try
    End Function

    Public Sub ProcUpdateHeader(ByVal SearchCode As String, ByVal TableName As String,
                                ByVal UpdateField As String, ByVal UpdateFieldType As String,
                                ByVal PrimaryField As String, ByVal ValueToUpdate As String,
                                ByVal mConn As Object, ByVal mCmd As Object)
        If TableName = "" Or UpdateField = "" Or PrimaryField = "" Or ValueToUpdate = "" Then Exit Sub
        Dim bValueToUpdate$ = ""
        If UpdateFieldType = ValueType_Number Then
            bValueToUpdate = ValueToUpdate
        Else
            bValueToUpdate = Agl.Chk_Text(ValueToUpdate)
        End If
        mQry = " UPDATE " & TableName & " SET " & UpdateField & " = " & bValueToUpdate & " Where " & PrimaryField & " = '" & SearchCode & "'  "
        Agl.Dman_ExecuteNonQry(mQry, mConn, mCmd)
    End Sub

    Public Shared Function AgCustomFieldSubQueryFooter(ByVal Agl As AgLibrary.ClsMain, ByVal V_Type As String, _
                                                       Optional ByVal EntryPointType As ClsMain.EntryPointType = ClsMain.EntryPointType.Main) As String
        Dim mQry, mTableFooter As String
        If EntryPointType = ClsMain.EntryPointType.Main Then
            mTableFooter = "CustomFields_Trans"
        Else
            mTableFooter = "CustomFields_Trans_Log"
        End If

        mQry = "DECLARE  @XColumns NVARCHAR(Max)  " & _
                " SET @XColumns = ''   " & _
                " Select @XColumns = @XColumns  + ' Max(Case Head WHEN ''' + [a].[Column] + '''  " & _
                " THEN Data ELSE '''' END) AS [CF_' + [a].[Description] + '], '  " & _
                " From   " & _
                " (SELECT Distinct CL.Head AS [Column], C.Description " & _
                " FROM CustomFields_Trans CL  " & _
                " LEFT JOIN CustomFieldsHead C ON CL.Head = C.Code " & _
                " Where LTrim(RTrim(SubString(DocID,4,5))) = 'SHIP') AS A " & _
                " SET @XColumns = 'SELECT UID, DocID, ' + @XColumns +  " & _
                " ' Count(Data) as CF_RecordCount FROM " & mTableFooter & " '   "
        If V_Type <> "" Then
            mQry = mQry & "SET @XColumns = @XColumns + ' Where LTrim(RTrim(SubString(DocID,4,5))) = ''" & V_Type & "'' ' "
        End If
        mQry = mQry & "SET @XColumns = @XColumns + ' GROUP BY UID, DocID ' "
        mQry = mQry & "SElect @XColumns "
        mQry = Agl.XNull(Agl.FillData(mQry, Agl.GCn).Tables(0).Rows(0)(0))
        AgCustomFieldSubQueryFooter = mQry
    End Function

    Public Sub FMoveRecFooterTable(ByVal DtTemp As DataTable)
        Dim mQry$ = "", I%
        Dim DtCustomFieldDetail As DataTable = Nothing
        Dim bRowIndex As Integer = 0
        Try
            mQry = " SELECT L.*, C.ManualCode " & _
                    " FROM CustomFieldsDetail L  " & _
                    " LEFT JOIN CustomFieldsHead C  ON L.Head = C.Code " & _
                    " WHERE L.Code = '" & AgCustom & "' "
            DtCustomFieldDetail = Agl.FillData(mQry, Agl.GCn).Tables(0)

            With DtCustomFieldDetail
                Me.RowCount = 1 : Me.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Me.Rows.Add()
                        Me.Item(Col1CustomFields, bRowIndex).Value = Agl.XNull(.Rows(I)("Code"))
                        Me.AgSelectedValue(Col1Heads1, bRowIndex) = Agl.XNull(.Rows(I)("Head"))
                        Me.Item(Col1Value1, bRowIndex).Value = Agl.XNull(.Rows(I)("Value"))
                        Me.Item(Col1ValueType1, bRowIndex).Value = Agl.XNull(.Rows(I)("Value_Type"))
                        Me.Item(Col1FLength1, bRowIndex).Value = Agl.XNull(.Rows(I)("FLength"))

                        If Agl.StrCmp(Agl.XNull(.Rows(I)("Value_Type")), ValueType_Help) Then
                            Me.AgHelpDataSet(Col1Data1) = Agl.FillData(Agl.XNull(.Rows(I)("Value")), Agl.GCn)
                            Me.AgSelectedValue(Col1Data1, bRowIndex) = IIf(Agl.StrCmp(Agl.XNull(.Rows(I)("Value_Type")), ValueType_Number), Agl.VNull(DtTemp.Rows(0)(Agl.XNull(.Rows(I)("HeaderField")))).ToString, Agl.XNull(DtTemp.Rows(0)(Agl.XNull(.Rows(I)("HeaderField")))))
                        Else
                            Me.Item(Col1Data1, bRowIndex).Value = IIf(Agl.StrCmp(Agl.XNull(.Rows(I)("Value_Type")), ValueType_Number), Agl.VNull(DtTemp.Rows(0)(Agl.XNull(.Rows(I)("HeaderField")))).ToString, Agl.XNull(DtTemp.Rows(0)(Agl.XNull(.Rows(I)("HeaderField")))))
                        End If

                        Me.Item(Col1TableName1, bRowIndex).Value = Agl.XNull(.Rows(I)("TableName"))
                        Me.Item(Col1PrimaryField1, bRowIndex).Value = Agl.XNull(.Rows(I)("PrimaryField"))
                        Me.Item(Col1UpdateField1, bRowIndex).Value = Agl.XNull(.Rows(I)("UpdateField"))
                        Me.Item(Col1UpdateFieldType1, bRowIndex).Value = Agl.XNull(.Rows(I)("UpdateFieldType"))
                        Me.Item(Col1HeaderField1, bRowIndex).Value = Agl.XNull(.Rows(I)("HeaderField"))

                        If mSplitGrid Then
                            I = I + 1
                            If I <= .Rows.Count - 1 Then
                                Me.AgSelectedValue(Col1Heads2, bRowIndex) = Agl.XNull(.Rows(I)("Head"))
                                Me.Item(Col1Value2, bRowIndex).Value = Agl.XNull(.Rows(I)("Value"))
                                Me.Item(Col1ValueType2, bRowIndex).Value = Agl.XNull(.Rows(I)("Value_Type"))
                                Me.Item(Col1FLength2, bRowIndex).Value = Agl.XNull(.Rows(I)("FLength"))

                                If Agl.StrCmp(Agl.XNull(.Rows(I)("Value_Type")), ValueType_Help) Then
                                    Me.AgHelpDataSet(Col1Data2) = Agl.FillData(Agl.XNull(.Rows(I)("Value")), Agl.GCn)
                                    Me.AgSelectedValue(Col1Data2, bRowIndex) = IIf(Agl.StrCmp(Agl.XNull(.Rows(I)("Value_Type")), ValueType_Number), Agl.VNull(DtTemp.Rows(0)(Agl.XNull(.Rows(I)("HeaderField")))).ToString, Agl.XNull(DtTemp.Rows(0)(Agl.XNull(.Rows(I)("HeaderField")))))
                                Else
                                    Me.Item(Col1Data2, bRowIndex).Value = IIf(Agl.StrCmp(Agl.XNull(.Rows(I)("Value_Type")), ValueType_Number), Agl.VNull(DtTemp.Rows(0)(Agl.XNull(.Rows(I)("HeaderField")))).ToString, Agl.XNull(DtTemp.Rows(0)(Agl.XNull(.Rows(I)("HeaderField")))))
                                End If

                                Me.Item(Col1TableName2, bRowIndex).Value = Agl.XNull(.Rows(I)("TableName"))
                                Me.Item(Col1PrimaryField2, bRowIndex).Value = Agl.XNull(.Rows(I)("PrimaryField"))
                                Me.Item(Col1UpdateField2, bRowIndex).Value = Agl.XNull(.Rows(I)("UpdateField"))
                                Me.Item(Col1UpdateFieldType2, bRowIndex).Value = Agl.XNull(.Rows(I)("UpdateFieldType"))
                                Me.Item(Col1HeaderField2, bRowIndex).Value = Agl.XNull(.Rows(I)("HeaderField"))
                            End If
                        End If
                        bRowIndex = bRowIndex + 1
                    Next I
                End If
            End With
            Call AgCl.GridSetiingShowXml(MnuText + "-CustomGrid", Me)
            Call ProcAdjustGrid()
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & " In MoveRec_TransFooter Procedure of AgCalcGrid")
        Finally
            DtTemp.Dispose()
        End Try
    End Sub

    Public Function FFooterTableUpdateStr() As String
        Dim I As Integer = 0
        Dim strUpdateQry As String

        strUpdateQry = ","
        For I = 0 To Me.RowCount - 1
            If Me.Item(Col1Heads1, I).Value <> "" Then
                If Agl.StrCmp(Me.Item(Col1ValueType1, I).Value, ValueType_Help) Then
                    strUpdateQry += Me.Item(Col1HeaderField1, I).Value & " = " & Agl.Chk_Text(Me.AgSelectedValue(Col1Data1, I)) & ","
                ElseIf Agl.StrCmp(Me.Item(Col1ValueType1, I).Value, ValueType_Number) Then
                    strUpdateQry += Me.Item(Col1HeaderField1, I).Value & " = " & Val(Me.Item(Col1Data1, I).Value) & ","
                Else
                    strUpdateQry += Me.Item(Col1HeaderField1, I).Value & " = " & Agl.Chk_Text(Me.Item(Col1Data1, I).Value) & ","
                End If
            End If

            If mSplitGrid Then
                If Me.Item(Col1Heads2, I).Value <> "" Then
                    If Agl.StrCmp(Me.Item(Col1ValueType2, I).Value, ValueType_Help) Then
                        strUpdateQry += Me.Item(Col1HeaderField2, I).Value & " = " & Agl.Chk_Text(Me.AgSelectedValue(Col1Data2, I)) & ","
                    ElseIf Agl.StrCmp(Me.Item(Col1ValueType2, I).Value, ValueType_Number) Then
                        strUpdateQry += Me.Item(Col1HeaderField2, I).Value & " = " & Val(Me.Item(Col1Data2, I).Value) & ","
                    Else
                        strUpdateQry += Me.Item(Col1HeaderField2, I).Value & " = " & Agl.Chk_Text(Me.Item(Col1Data2, I).Value) & ","
                    End If
                End If
            End If
        Next

        If strUpdateQry <> "" Then
            strUpdateQry = strUpdateQry.Substring(0, strUpdateQry.Length - 1)
        End If

        FFooterTableUpdateStr = strUpdateQry
    End Function

    Public Function FHeaderTableFieldNameStr(ByVal FieldPrefix As String, ByVal AliasPrefix As String) As String
        Dim I%
        Dim strUpdateQry$

        strUpdateQry = ","

        For I = 0 To Me.RowCount - 1
            If Me.Item(Col1Heads1, I).Value <> "" Then
                If Me.Item(Col1HeaderField1, I).Value <> "" Then
                    strUpdateQry += FieldPrefix & Me.Item(Col1HeaderField1, I).Value & " As " & AliasPrefix & Me.Item(Col1HeaderField1, I).Value & ","
                End If

                If Me.Item(Col1HeaderField2, I).Value <> "" Then
                    strUpdateQry += FieldPrefix & Me.Item(Col1HeaderField2, I).Value & " As " & AliasPrefix & Me.Item(Col1HeaderField1, I).Value & ","
                End If
            End If
        Next

        If strUpdateQry <> "" Then
            strUpdateQry = strUpdateQry.Substring(0, strUpdateQry.Length - 1)
        End If
        FHeaderTableFieldNameStr = strUpdateQry
    End Function

    Private Sub AgCustomGrid_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.EditingControl_Validating
        Me.AutoResizeRows()
    End Sub
End Class

