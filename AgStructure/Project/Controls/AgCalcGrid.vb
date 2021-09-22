Imports System.Windows.Forms
Imports System.Data.SQLite
Public Class AgCalcGrid

    Inherits AgControls.AgDataGrid

    Public Event Calculated()

    Dim Agl As AgLibrary.ClsMain
    Dim AgCl As New AgControls.AgLib


    Dim DtPostingGroupSalesTax As DataTable
    Dim DrPostingGroupSalesTax As DataRow()


    Dim mReadOnlyColor As System.Drawing.Color = Color.Beige
    Dim mQry$


    Private Const Col_Charges As Byte = 0
    Private Const Col_Charge_Type As Byte = 1
    Private Const Col_Value_Type As Byte = 2
    Private Const Col_Value As Byte = 3
    Private Const Col_Calculation As Byte = 4
    Private Const Col_BaseColumn As Byte = 5
    Private Const Col_PostAc As Byte = 6
    Private Const Col_PostAcFromColumn As Byte = 7
    Private Const Col_ContraAc As Byte = 8
    Private Const Col_ContraAcFromColumn As Byte = 9
    Private Const Col_DrCr As Byte = 10
    Private Const Col_LineItem As Byte = 11
    Private Const Col_AffectCost As Byte = 12
    Private Const Col_Active As Byte = 13
    Private Const Col_ChargesManualCode As Byte = 14
    Private Const Col_Percentage As Byte = 15
    Private Const Col_Amount As Byte = 16
    Private Const Col_VisibleInMaster As Byte = 17
    Private Const Col_VisibleInMasterLine As Byte = 18
    Private Const Col_VisibleInTransactionLine As Byte = 19
    Private Const Col_VisibleInTransactionFooter As Byte = 20
    Private Const Col_GridDisplayIndex As Byte = 21
    Private Const Col_HeaderPerField As Byte = 22
    Private Const Col_HeaderAmtField As Byte = 23
    Private Const Col_LinePerField As Byte = 24
    Private Const Col_LineAmtField As Byte = 25
    Dim mFixedRows As Integer
    Dim mAgCalcRounding As Integer = 2
    Dim mCurrFixedRows As Integer

    Public Enum AgCalcGridColumn
        Col_Charges = 0
        Col_Charge_Type = 1
        Col_Value_Type = 2
        Col_Value = 3
        Col_Calculation = 4
        Col_BaseColumn = 5
        Col_PostAc = 6
        Col_PostAcFromColumn = 7
        Col_ContraAc = 8
        Col_ContraAcFromColumn = 9
        Col_DrCr = 10
        Col_LineItem = 11
        Col_AffectCost = 12
        Col_Active = 13
        Col_ChargesManualCode = 14
        Col_Percentage = 15
        Col_Amount = 16
        Col_VisibleInMaster = 17
        Col_VisibleInMasterLine = 18
        Col_VisibleInTransactionLine = 19
        Col_VisibleInTransactionFooter = 20
        Col_GridDisplayIndex = 21
        Col_HeaderPerField = 22
        Col_HeaderAmtField = 23
        Col_LinePerField = 24
        Col_LineAmtField = 25
    End Enum

    Dim mLineGrid As AgControls.AgDataGrid
    Dim mStructure As String
    Dim mColumnGross As Integer = -1
    Dim mColumnSalesTaxGroupItem As Integer = -1
    Dim mColumnMandatory As Integer = -1
    Dim mIsMaster As Boolean = False
    Dim mSite_Code As String
    Dim mDiv_Code As String
    Dim mSalesTaxGroupItem As String
    Dim mSalesTaxGroupParty As String
    Dim mPartyAc As String
    Dim mPaidAc As String
    Dim mNarration As String
    Dim mPaidAmount As Double
    Dim mVoucherCategory As String = ""
    Dim mPlaceOfSupply As String = ""

    Dim mNCat As String


    Dim mFrmType As ClsMain.EntryPointType = ClsMain.EntryPointType.Main

    Enum LineColumnType
        Percentage = 0
        Amount = 1
        PostAc = 2
        ContraAc = 3
    End Enum

    Sub New()
        IniMe()
    End Sub

    Public Property FrmType() As ClsMain.EntryPointType
        Get
            Return mFrmType
        End Get
        Set(ByVal value As ClsMain.EntryPointType)
            mFrmType = value
        End Set
    End Property

    Public Property AgChargesValue(ByVal Charges As String, ByVal ColumnIndex As AgCalcGridColumn) As String

        Get
            Dim i As Integer
            AgChargesValue = ""
            For i = 0 To Me.RowCount - 1
                If Agl.StrCmp(Me.AgSelectedValue(Col_Charges, i), Charges) Or Agl.StrCmp(Me.Item(Col_ChargesManualCode, i).Value, Charges) Then
                    Return Me.Item(ColumnIndex, i).Value

                End If
            Next
        End Get

        Set(ByVal value As String)
            Dim i As Integer
            For i = 0 To Me.RowCount - 1
                If Agl.StrCmp(Me.AgSelectedValue(Col_Charges, i), Charges) Or Agl.StrCmp(Me.Item(Col_ChargesManualCode, i).Value, Charges) Then
                    Me.Item(ColumnIndex, i).Value = value
                End If
            Next
        End Set
    End Property


    Public Sub FCopyStructureLine(ByVal DocID As String, ByVal Dgl As AgControls.AgDataGrid, ByVal GridRowIndex As Integer, ByVal StructureRowIndex As Integer)
        Dim I As Integer
        Dim DtTemp As DataTable
        Dim mQry As String

        Try
            mQry = "SELECT L.*, C.Description as ChargesName FROM Structure_TransLine L  Left Join Charges C  On L.Charges=C.Code WHERE L.DocID ='" & DocID & "' AND L.TSr = " & StructureRowIndex & " "
            DtTemp = Agl.FillData(mQry, Agl.GcnRead).Tables(0)
            For I = 0 To DtTemp.Rows.Count - 1
                If Agl.XNull(DtTemp.Rows(I)("Charges")) <> "" Then
                    If IsGridColumnExist(Dgl, GetColNamePer(DtTemp.Rows(I)("ChargesName"))) Then
                        Dgl.Item(GetColNamePer(DtTemp.Rows(I)("ChargesName")), GridRowIndex).value = Agl.VNull(DtTemp.Rows(I)("Percentage"))
                    End If

                    If IsGridColumnExist(Dgl, GetColName(DtTemp.Rows(I)("ChargesName"))) Then
                        Dgl.Item(GetColName(DtTemp.Rows(I)("ChargesName")), GridRowIndex).value = Agl.VNull(DtTemp.Rows(I)("Amount"))
                    End If

                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub FCopyStructureFooter(ByVal DocID As String, ByRef AgCGrid As AgCalcGrid)
        Dim I As Integer, J As Integer
        Dim DtTemp As DataTable
        Dim mQry As String

        Try
            mQry = "SELECT H.*, C.Description as ChargesName FROM Structure_TransFooter H  Left Join Charges C  On H.Charges=C.Code WHERE H.DocID ='" & DocID & "' "
            DtTemp = Agl.FillData(mQry, Agl.GcnRead).Tables(0)
            For I = 0 To DtTemp.Rows.Count - 1
                If Agl.XNull(DtTemp.Rows(I)("Charges")) <> "" Then
                    For J = 0 To AgCGrid.Rows.Count - 1
                        If Agl.StrCmp(AgCGrid.AgSelectedValue(AgCalcGrid.Col_Charges, J), Agl.XNull(DtTemp.Rows(I)("Charges"))) Then
                            AgCGrid.Item(AgCalcGrid.Col_Percentage, J).Value = Agl.VNull(DtTemp.Rows(I)("Percentage"))
                            AgCGrid.Item(AgCalcGrid.Col_Amount, J).Value = Agl.VNull(DtTemp.Rows(I)("Amount"))
                        End If
                    Next
                End If
            Next
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Function IsGridColumnExist(ByVal Dgl As AgControls.AgDataGrid, ByVal ColumnName As String) As Boolean
        Try
            IsGridColumnExist = False
            Dgl.Item(ColumnName, Dgl.CurrentCell.RowIndex).Value = Dgl.Item(ColumnName, Dgl.CurrentCell.RowIndex).Value
            IsGridColumnExist = True
        Catch ex As Exception
        End Try
    End Function

    Public Property AgChargesValue(ByVal Charges As String, ByVal RowIndex As Integer, ByVal ColumnIndex As LineColumnType) As String
        Get
            Dim i As Integer
            AgChargesValue = ""
            For i = 0 To Me.RowCount - 1
                If Agl.StrCmp(Me.AgSelectedValue(Col_Charges, i), Charges) Or Agl.StrCmp(Me.Item(Col_ChargesManualCode, i).Value, Charges) Then
                    If ColumnIndex = LineColumnType.Percentage Then
                        Select Case UCase(Me.Item(Col_Value_Type, i).Value)
                            Case "PERCENTAGE", "PERCENTAGE CHANGEABLE", "PERCENTAGE FROM COLUMN"
                                Return mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, i).Value), RowIndex).value
                        End Select
                    ElseIf ColumnIndex = LineColumnType.PostAc Then
                        Return mLineGrid.Item(GetColNamePostAc(Me.Item(Col_Charges, i).Value), RowIndex).value
                    ElseIf ColumnIndex = LineColumnType.ContraAc Then
                        Return mLineGrid.Item(GetColNameContraAc(Me.Item(Col_Charges, i).Value), RowIndex).value
                    Else
                        Return mLineGrid.Item(GetColName(Me.Item(Col_Charges, i).Value), RowIndex).value
                    End If
                End If
            Next
        End Get

        Set(ByVal value As String)
            Dim i As Integer
            For i = 0 To Me.RowCount - 1
                If Agl.StrCmp(Me.AgSelectedValue(Col_Charges, i), Charges) Or Agl.StrCmp(Me.Item(Col_ChargesManualCode, i).Value, Charges) Then
                    If ColumnIndex = LineColumnType.Percentage Then
                        Select Case UCase(Me.Item(Col_Value_Type, i).Value)
                            Case "PERCENTAGE", "PERCENTAGE CHANGEABLE", "PERCENTAGE FROM COLUMN"
                                mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, i).Value), RowIndex).value = value
                        End Select
                    Else
                        mLineGrid.Item(GetColName(Me.Item(Col_Charges, i).Value), RowIndex).value = value
                    End If
                End If
            Next
        End Set
    End Property

    Public Property AgFixedRows() As Integer
        Get
            If Me.RowCount < mFixedRows Then
                Return Me.RowCount
            Else
                Return mFixedRows
            End If
        End Get

        Set(ByVal value As Integer)
            mFixedRows = value
            mCurrFixedRows = value
        End Set
    End Property

    Public Property AgCalcRounding() As Integer
        Get
            Return mAgCalcRounding
        End Get

        Set(ByVal value As Integer)
            mAgCalcRounding = value
        End Set
    End Property

    Public Property AgIsMaster() As Boolean
        Get
            Return mIsMaster
        End Get

        Set(ByVal value As Boolean)
            mIsMaster = value
        End Set
    End Property

    Public Property AgLibVar() As AgLibrary.ClsMain
        Get
            Return Agl
        End Get

        Set(ByVal value As AgLibrary.ClsMain)
            Agl = value
            If mSite_Code = "" And Agl IsNot Nothing Then mSite_Code = Agl.PubSiteCode
            If mDiv_Code = "" And Agl IsNot Nothing Then mDiv_Code = Agl.PubDivCode
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

    Public Property AgStructure() As String
        Get
            Return mStructure
        End Get

        Set(ByVal value As String)
            mStructure = value
            If mStructure = "" Then
                Me.Visible = False
            Else
                Me.Visible = True
            End If
        End Set
    End Property

    Public Property AgNCat() As String

        Get
            Return mNCat
        End Get

        Set(ByVal value As String)

            mNCat = value
        End Set
    End Property


    Public Property AgSite_Code() As String
        Get
            Return mSite_Code
        End Get

        Set(ByVal value As String)
            mSite_Code = value
        End Set
    End Property

    Public Property AgDiv_Code() As String
        Get
            Return mDiv_Code
        End Get

        Set(ByVal value As String)
            mDiv_Code = value
        End Set
    End Property

    Public Property AgLineGridPostingGroupSalesTaxProd() As String
        Get
            Return mColumnSalesTaxGroupItem
        End Get

        Set(ByVal value As String)
            mColumnSalesTaxGroupItem = value
        End Set
    End Property


    Public Property AgPostingGroupSalesTaxItem() As String
        Get
            Return mSalesTaxGroupItem
        End Get

        Set(ByVal value As String)
            mSalesTaxGroupItem = value
        End Set
    End Property


    Public Property AgPostingGroupSalesTaxParty() As String
        Get
            Return mSalesTaxGroupParty
        End Get

        Set(ByVal value As String)
            mSalesTaxGroupParty = value
        End Set
    End Property


    Public Property AgPlaceOfSupply() As String
        Get
            Return mPlaceOfSupply
        End Get

        Set(ByVal value As String)
            mPlaceOfSupply = value
        End Set
    End Property


    Public Property AgPostingPartyAc() As String
        Get
            Return mPartyAc
        End Get

        Set(ByVal value As String)
            mPartyAc = value
        End Set
    End Property

    Public Property AgVoucherCategory() As String
        Get
            Return mVoucherCategory
        End Get

        Set(ByVal value As String)
            mVoucherCategory = value
        End Set
    End Property

    Public Property AgPaidAc() As String
        Get
            Return mPaidAc
        End Get

        Set(ByVal value As String)
            mPaidAc = value
        End Set
    End Property
    'Narration
    Public Property AgNarration() As String
        Get
            Return mNarration
        End Get

        Set(ByVal value As String)
            mNarration = value
        End Set
    End Property

    Public Property AgPaidMAount() As Double
        Get
            Return mPaidAmount
        End Get

        Set(ByVal value As Double)
            mPaidAmount = value
        End Set
    End Property

    Public Property AgLineGridGrossColumn() As Integer
        Get
            Return mColumnGross
        End Get

        Set(ByVal value As Integer)
            mColumnGross = value
        End Set
    End Property

    Public Property AgLineGridMandatoryColumn() As Integer
        Get
            Return mColumnMandatory
        End Get

        Set(ByVal value As Integer)
            mColumnMandatory = value
        End Set
    End Property

    Structure AgLineStructure
        Dim Charges As String
        Dim Charge_Type As String
        Dim Value_Type As String
        Dim Value As String
        Dim Calculation As String
        Dim PostAc As String
        Dim PostAcFromColumn As String
        Dim ContraAc As String
        Dim ContraAcFromColumn As String

        Dim DrCr As String
        Dim LineItem As String
        Dim AffectCost As String
        Dim Active As String
    End Structure

    Public Function GetStructure(ByVal NCAT As String) As String
        Dim DtTemp As DataTable
        mQry = "Select Structure From Structure_NCat Where NCAT = '" & NCAT & "' "
        DtTemp = Agl.FillData(mQry, Agl.GCn).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            Return DtTemp.Rows(0)(0)
        Else
            Return ""
            MsgBox("Structure not Defined for Selected Voucher Type." & vbCrLf & "Can't Continue.")
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
        Me.ColumnHeadersVisible = False
        'Me.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue
        'Me.ColumnHeadersDefaultCellStyle.ForeColor = Color.White        
        Me.AgReadOnlyColumnColor = mReadOnlyColor
        Dim mColumnNamePrefix As String = "AgFG"
        With AgCl
            .AddAgTextColumn(Me, mColumnNamePrefix & "Charges", 150, 0, "HEAD", True, True, False)
            .AddAgTextColumn(Me, mColumnNamePrefix & "Charge_Type", 170, 0, "Charges Type", False, True, False)
            .AddAgTextColumn(Me, mColumnNamePrefix & "Value_Type", 170, 0, "Value Type", False, True, False)
            .AddAgTextColumn(Me, mColumnNamePrefix & "Value", 170, 0, "Value", False, True, False)
            .AddAgTextColumn(Me, mColumnNamePrefix & "Calculation", 250, 0, "Calculation", False, True, False)
            .AddAgTextColumn(Me, mColumnNamePrefix & "BaseColumn", 250, 0, "BaseColumn", False, True, False)
            .AddAgTextColumn(Me, mColumnNamePrefix & "PostAc", 150, 0, "Post A/c", False, True, False)
            .AddAgTextColumn(Me, mColumnNamePrefix & "PostAcFromColumn", 150, 0, "Post A/c From Column", False, True, False)
            .AddAgTextColumn(Me, mColumnNamePrefix & "ContraAc", 150, 0, "Contra A/c", False, True, False)
            .AddAgTextColumn(Me, mColumnNamePrefix & "ContraAcFromColumn", 150, 0, "Contra A/c From Column", False, True, False)
            .AddAgTextColumn(Me, mColumnNamePrefix & "DrCr", 150, 0, "Dr Cr", False, True, False)
            .AddAgTextColumn(Me, mColumnNamePrefix & "LineItem", 150, 0, "Line Item", False, True, False)
            .AddAgTextColumn(Me, mColumnNamePrefix & "Affect Cost", 150, 0, "Affect Code", False, True, False)
            .AddAgTextColumn(Me, mColumnNamePrefix & "Active", 150, 0, "Active", False, True, False)
            .AddAgTextColumn(Me, mColumnNamePrefix & "ChargesManualCode", 150, 0, "ChargesManualCode", False, True, False)
            '.AddAgTextColumn(Me, mColumnNamePrefix & "Percentage", 70, 0, " % ", True, False, False)
            .AddAgNumberColumn(Me, mColumnNamePrefix & "Percentage", 40, 3, 3, True, " @ ", True, False, True)
            '.AddAgTextColumn(Me, mColumnNamePrefix & "Amount", 100, 0, " Amount ", True, False, False)
            .AddAgNumberColumn(Me, mColumnNamePrefix & "Amount", 100, 10, 2, True, " Amount ", True, False, True)
            .AddAgTextColumn(Me, mColumnNamePrefix & "VisibleInMaster", 150, 0, "Visible In Master", False, True, False)
            .AddAgTextColumn(Me, mColumnNamePrefix & "VisibleInMasterLine", 150, 0, "Visible In Master Line", False, True, False)
            .AddAgTextColumn(Me, mColumnNamePrefix & "VisibleInTransactionLine", 150, 0, "VisibleInTransactionLine", False, True, False)
            .AddAgTextColumn(Me, mColumnNamePrefix & "VisibleInTransactionFooter", 150, 0, "Visible In Transaction Footer", False, True, False)
            .AddAgNumberColumn(Me, mColumnNamePrefix & "GridDisplayIndex", 100, 3, 0, False, "Grid Display Index", False, True, False)
            .AddAgTextColumn(Me, mColumnNamePrefix & "HeaderPerField", 170, 0, "Header Per Field", False, True, False)
            .AddAgTextColumn(Me, mColumnNamePrefix & "HeaderAmtField", 170, 0, "Header Amt Field", False, True, False)
            .AddAgTextColumn(Me, mColumnNamePrefix & "LinePerField", 170, 0, "Line Per Field", False, True, False)
            .AddAgTextColumn(Me, mColumnNamePrefix & "LineAmtField", 170, 0, "Line Amt Field", False, True, False)

        End With
        Me.EnableHeadersVisualStyles = False

    End Sub

    Function GetColName(ByVal Column_Caption As String)
        GetColName = Column_Caption
    End Function

    Function GetColNamePer(ByVal Column_Caption As String)
        GetColNamePer = Column_Caption & " @"
    End Function

    Function GetColNamePostAc(ByVal Column_Caption As String)
        GetColNamePostAc = Column_Caption & " PostAc"
    End Function

    Function GetColNameContraAc(ByVal Column_Caption As String)
        GetColNameContraAc = Column_Caption & " ContraAc"
    End Function

    Public Sub Ini_Grid(ByVal mNCat As String, ByVal mV_Date As String)
        Dim DtTemp As DataTable
        Dim I As Integer
        Dim mLineColumnPrefix$
        Dim mColumnName$
        Dim mColumnNamePer$
        Dim mColumnNamePostAc$
        Dim mColumnNameContraAc$


        If mStructure = "" Then Exit Sub
        'If mLineGrid Is Nothing Then Exit Sub

        Me.RowCount = 1
        Me.Rows.Clear()


        If Agl IsNot Nothing Then


            If Not mLineGrid Is Nothing Then
                If mColumnSalesTaxGroupItem >= 0 Then
                    mQry = "Select * From PostingGroupSalesTax Where Site_Code = '" & mSite_Code & "' And Div_Code = '" & mDiv_Code & "' "
                    DtPostingGroupSalesTax = Agl.FillData(mQry, Agl.GCn).Tables(0)
                    If DtPostingGroupSalesTax.Rows.Count = 0 Then
                        mQry = "Select * From PostingGroupSalesTax Where Site_Code  Is Null And Div_Code = '" & mDiv_Code & "' "
                        DtPostingGroupSalesTax = Agl.FillData(mQry, Agl.GCn).Tables(0)
                        If DtPostingGroupSalesTax.Rows.Count = 0 Then
                            mQry = "Select * From PostingGroupSalesTax Where Site_Code = '" & mSite_Code & "' And Div_Code Is Null "
                            DtPostingGroupSalesTax = Agl.FillData(mQry, Agl.GCn).Tables(0)
                            If DtPostingGroupSalesTax.Rows.Count = 0 Then
                                mQry = "Select * From PostingGroupSalesTax Where Site_Code Is Null And Div_Code Is Null "
                                DtPostingGroupSalesTax = Agl.FillData(mQry, Agl.GCn).Tables(0)
                            End If
                        End If
                    End If
                End If
            End If
        End If


        Me.AgHelpDataSet(Col_Charges, 1) = Agl.FillData("Select Code, Description, ManualCode From Charges Order By Description", Agl.GCn)

        mLineColumnPrefix = ""


        'mQry = "Select SD.Code,SD.Sr,SD.Charges,SD.Charge_Type,SD.Value_Type,SD.Value, " &
        '       "SD.Calculation,SD.BaseColumn,SD.LineItem,SD.AffectCost, " &
        '       "SD.VisibleInMaster, SD.VisibleInMasterLine,SD.VisibleInTransactionLine, " &
        '       "SD.VisibleInTransactionFooter,SD.HeaderPerField,SD.HeaderAmtField,SD.LinePerField, " &
        '       "SD.LineAmtField, C.ManualCode, SD.PostAc, SD.DrCr,SD.PostAcFromColumn, " &
        '       "SD.GridDisplayIndex " &
        '       "From StructureDetail SD " &
        '       "Left Join Charges C On SD.Charges=C.Code " &
        '       "Where SD.Code = '" & mStructure & "' " &
        '       "And  IfNull(SD.WEF,'" & CDate(mV_Date).ToString("u") & "') <= '" & CDate(mV_Date).ToString("u") & "'  " &
        '       "And IfNull(SD.InactiveDate,'" & (DateAdd(DateInterval.Day, 1, CDate(mV_Date))).ToString("u") & "') >= '" & CDate(mV_Date).ToString("u") & "' " &
        '       "Order By SD.Sr"
        mQry = "Select SD.Code,SD.Sr,SD.Charges,SD.Charge_Type,SD.Value_Type,SD.Value, " &
               "SD.Calculation,SD.BaseColumn,SD.LineItem,SD.AffectCost, " &
               "SD.VisibleInMaster, SD.VisibleInMasterLine,SD.VisibleInTransactionLine, " &
               "SD.VisibleInTransactionFooter,SD.HeaderPerField,SD.HeaderAmtField,SD.LinePerField, " &
               "SD.LineAmtField, C.ManualCode, SD.PostAc, SD.DrCr,SD.PostAcFromColumn, SD.ContraAc, SD.ContraAcFromColumn, " &
               "SD.GridDisplayIndex " &
               "From StructureDetail SD " &
               "Left Join Charges C On SD.Charges=C.Code " &
               "Where SD.Code = '" & mStructure & "' " &
               "Order By SD.Sr"
        DtTemp = Agl.FillData(mQry, Agl.GCn).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            With DtTemp



                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Me.Rows.Add()
                        'Me.Item(Col_SNo, I).Value = Me.Rows.Count - 1
                        Me.AgSelectedValue(Col_Charges, I) = Agl.XNull(.Rows(I)("Charges"))
                        Me.Item(Col_ChargesManualCode, I).Value = Agl.XNull(.Rows(I)("ManualCode"))
                        Me.Item(Col_Charge_Type, I).Value = Agl.XNull(.Rows(I)("Charge_Type"))
                        Me.Item(Col_Value_Type, I).Value = Agl.XNull(.Rows(I)("Value_Type"))
                        Me.Item(Col_Value, I).Value = Agl.XNull(.Rows(I)("Value"))
                        Me.Item(Col_Calculation, I).Value = Agl.XNull(.Rows(I)("Calculation"))
                        Me.Item(Col_BaseColumn, I).Value = Agl.XNull(.Rows(I)("BaseColumn"))
                        Me.Item(Col_DrCr, I).Value = Agl.XNull(.Rows(I)("DrCr"))
                        Me.Item(Col_PostAc, I).Value = Agl.XNull(.Rows(I)("PostAc"))
                        Me.Item(Col_PostAcFromColumn, I).Value = Agl.XNull(.Rows(I)("PostAcFromColumn"))
                        Me.Item(Col_ContraAc, I).Value = Agl.XNull(.Rows(I)("ContraAc"))
                        Me.Item(Col_ContraAcFromColumn, I).Value = Agl.XNull(.Rows(I)("ContraAcFromColumn"))

                        Me.Item(Col_LineItem, I).Value = Math.Abs(Agl.VNull(.Rows(I)("LineItem")))
                        Me.Item(Col_AffectCost, I).Value = IIf(IsDBNull(.Rows(I)("AffectCost")), "", Math.Abs(Agl.VNull(.Rows(I)("AffectCost"))))
                        'Me.Item(Col_Active, I).Value = Math.Abs(Agl.VNull(.Rows(I)("Active")))
                        Me.Item(Col_VisibleInMaster, I).Value = Math.Abs(Agl.VNull(.Rows(I)("VisibleInMaster")))
                        Me.Item(Col_VisibleInMasterLine, I).Value = Math.Abs(Agl.VNull(.Rows(I)("VisibleInMasterLine")))
                        Me.Item(Col_VisibleInTransactionLine, I).Value = Math.Abs(Agl.VNull(.Rows(I)("VisibleInTransactionLine")))
                        Me.Item(Col_VisibleInTransactionFooter, I).Value = Math.Abs(Agl.VNull(.Rows(I)("VisibleInTransactionFooter")))
                        Me.Item(Col_GridDisplayIndex, I).Value = Math.Abs(Agl.VNull(.Rows(I)("GridDisplayIndex")))
                        Me.Item(Col_HeaderPerField, I).Value = Agl.XNull(.Rows(I)("HeaderPerField"))
                        Me.Item(Col_HeaderAmtField, I).Value = Agl.XNull(.Rows(I)("HeaderAmtField"))
                        Me.Item(Col_LinePerField, I).Value = Agl.XNull(.Rows(I)("LinePerField"))
                        Me.Item(Col_LineAmtField, I).Value = Agl.XNull(.Rows(I)("LineAmtField"))

                        mColumnName = GetColName(Me.Item(Col_Charges, I).Value)
                        mColumnNamePer = GetColNamePer(Me.Item(Col_Charges, I).Value)
                        mColumnNamePostAc = GetColNamePostAc(Me.Item(Col_Charges, I).Value)
                        mColumnNameContraAc = GetColNameContraAc(Me.Item(Col_Charges, I).Value)


                        Select Case UCase(Me.Item(Col_Value_Type, I).Value)
                            Case "FIXEDVALUE"
                                If Me.Rows(I).Visible = False Then Me.Rows(I).Visible = True
                                Me.CurrentCell = Me(Col_Percentage, I)
                                Me.CurrentCell.ReadOnly = True
                                Me.CurrentCell.Style.BackColor = mReadOnlyColor
                                Me.CurrentCell = Me(Col_Amount, I)
                                Me.CurrentCell.ReadOnly = True
                                Me.CurrentCell.Style.BackColor = mReadOnlyColor


                                Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInTransactionFooter, I).Value = 1)
                                If mIsMaster Then
                                    Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInMaster, I).Value = 1)
                                End If

                                If Not mLineGrid Is Nothing Then
                                    AgCl.AddAgNumberColumn(mLineGrid, mColumnName, 100, 8, 2, True, Me.Item(Col_Charges, I).Value, IIf(mIsMaster, CBool(Me.Item(Col_VisibleInMasterLine, I).Value = 1), CBool(Me.Item(Col_VisibleInTransactionLine, I).Value = 1)), True, True)
                                    If Agl.StrCmp(Agl.PubUserName, "Super") Then mLineGrid.Columns(mColumnName).ToolTipText = mColumnName

                                    AgCl.AddAgTextColumn(mLineGrid, mColumnNamePostAc, 100, 0, Me.Item(Col_Charges, I).Value & " Post A/c", False)
                                    If Agl.StrCmp(Agl.PubUserName, "Super") Then mLineGrid.Columns(mColumnNamePostAc).ToolTipText = mColumnNamePostAc
                                    AgCl.AddAgTextColumn(mLineGrid, mColumnNameContraAc, 100, 0, Me.Item(Col_Charges, I).Value & " Contra A/c", False)
                                    If Agl.StrCmp(Agl.PubUserName, "Super") Then mLineGrid.Columns(mColumnNameContraAc).ToolTipText = mColumnNameContraAc

                                End If

                                If CBool(Me.Item(Col_LineItem, I).Value = "1") AndAlso Not mLineGrid Is Nothing Then
                                    mLineGrid.AgDefaultValue(mColumnName) = Agl.XNull(.Rows(I)("Value"))
                                Else
                                    Me.Item(Col_Amount, I).Value = Agl.XNull(.Rows(I)("Value"))
                                End If
                            Case "PERCENTAGE"
                                If Me.Rows(I).Visible = False Then Me.Rows(I).Visible = True
                                Me.CurrentCell = Me(Col_Percentage, I)
                                Me.CurrentCell.ReadOnly = True
                                Me.CurrentCell.Style.BackColor = mReadOnlyColor
                                Me.CurrentCell = Me(Col_Amount, I)
                                Me.CurrentCell.ReadOnly = True
                                Me.CurrentCell.Style.BackColor = mReadOnlyColor

                                Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInTransactionFooter, I).Value = 1)
                                If mIsMaster Then
                                    Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInMaster, I).Value = 1)
                                End If


                                If Not mLineGrid Is Nothing Then
                                    AgCl.AddAgNumberColumn(mLineGrid, mColumnNamePer, 100, 8, 2, True, Me.Item(Col_Charges, I).Value & " @", IIf(mIsMaster, CBool(Me.Item(Col_VisibleInMasterLine, I).Value = 1), CBool(Me.Item(Col_VisibleInTransactionLine, I).Value = 1)), True, True)
                                    If Agl.StrCmp(Agl.PubUserName, "Super") Then mLineGrid.Columns(mColumnNamePer).ToolTipText = mColumnNamePer
                                    AgCl.AddAgNumberColumn(mLineGrid, mColumnName, 100, 8, 2, True, Me.Item(Col_Charges, I).Value, IIf(mIsMaster, CBool(Me.Item(Col_VisibleInMasterLine, I).Value = 1), CBool(Me.Item(Col_VisibleInTransactionLine, I).Value = 1)), True, True)
                                    If Agl.StrCmp(Agl.PubUserName, "Super") Then mLineGrid.Columns(mColumnName).ToolTipText = mColumnName
                                    AgCl.AddAgTextColumn(mLineGrid, mColumnNamePostAc, 100, 0, Me.Item(Col_Charges, I).Value & " Post A/c", False)
                                    If Agl.StrCmp(Agl.PubUserName, "Super") Then mLineGrid.Columns(mColumnNamePostAc).ToolTipText = mColumnNamePostAc
                                    AgCl.AddAgTextColumn(mLineGrid, mColumnNameContraAc, 100, 0, Me.Item(Col_Charges, I).Value & " Contra A/c", False)
                                    If Agl.StrCmp(Agl.PubUserName, "Super") Then mLineGrid.Columns(mColumnNameContraAc).ToolTipText = mColumnNameContraAc

                                End If



                                If CBool(Me.Item(Col_LineItem, I).Value = "1") AndAlso Not mLineGrid Is Nothing Then
                                    mLineGrid.AgDefaultValue(mColumnNamePer) = Agl.XNull(.Rows(I)("Value"))
                                Else
                                    Me.Item(Col_Percentage, I).Value = Agl.XNull(.Rows(I)("Value"))
                                End If

                            Case "FIXEDVALUE CHANGEABLE"
                                If Me.Rows(I).Visible = False Then Me.Rows(I).Visible = True
                                Me.CurrentCell = Me(Col_Percentage, I)
                                Me.CurrentCell.ReadOnly = True
                                Me.CurrentCell.Style.BackColor = mReadOnlyColor



                                If Not mLineGrid Is Nothing Then
                                    AgCl.AddAgNumberColumn(mLineGrid, mColumnName, 100, 8, 2, True, Me.Item(Col_Charges, I).Value, IIf(mIsMaster, CBool(Me.Item(Col_VisibleInMasterLine, I).Value = 1), CBool(Me.Item(Col_VisibleInTransactionLine, I).Value = 1)), Not CBool(Me.Item(Col_LineItem, I).Value = "1"), True)
                                    AgCl.AddAgTextColumn(mLineGrid, mColumnNamePostAc, 100, 0, Me.Item(Col_Charges, I).Value & " Post A/c", False)
                                    If Agl.StrCmp(Agl.PubUserName, "Super") Then mLineGrid.Columns(mColumnNamePostAc).ToolTipText = mColumnNamePostAc
                                    AgCl.AddAgTextColumn(mLineGrid, mColumnNameContraAc, 100, 0, Me.Item(Col_Charges, I).Value & " Contra A/c", False)
                                    If Agl.StrCmp(Agl.PubUserName, "Super") Then mLineGrid.Columns(mColumnNameContraAc).ToolTipText = mColumnNameContraAc
                                End If



                                If CBool(Me.Item(Col_LineItem, I).Value = "1") Then
                                    If Not mLineGrid Is Nothing Then
                                        Me.CurrentCell = Me(Col_Amount, I)
                                        Me.CurrentCell.ReadOnly = True
                                        Me.CurrentCell.Style.BackColor = mReadOnlyColor
                                        mLineGrid.AgDefaultValue(mColumnName) = Agl.XNull(.Rows(I)("Value"))
                                    End If
                                Else
                                    Me.Item(Col_Amount, I).Value = Agl.XNull(.Rows(I)("Value"))
                                End If

                                Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInTransactionFooter, I).Value = 1)
                                If mIsMaster Then
                                    Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInMaster, I).Value = 1)
                                End If

                            Case "PERCENTAGE CHANGEABLE"
                                If Me.Rows(I).Visible = False Then Me.Rows(I).Visible = True
                                Me.CurrentCell = Me(Col_Amount, I)
                                Me.CurrentCell.ReadOnly = True
                                Me.CurrentCell.Style.BackColor = mReadOnlyColor

                                If Not mLineGrid Is Nothing Then
                                    AgCl.AddAgNumberColumn(mLineGrid, mColumnNamePer, 100, 8, 2, True, Me.Item(Col_Charges, I).Value & " @", IIf(mIsMaster, CBool(Me.Item(Col_VisibleInMasterLine, I).Value = 1), CBool(Me.Item(Col_VisibleInTransactionLine, I).Value = 1)), Not CBool(Me.Item(Col_LineItem, I).Value = "1"), True)
                                    AgCl.AddAgNumberColumn(mLineGrid, mColumnName, 100, 8, 2, True, Me.Item(Col_Charges, I).Value, IIf(mIsMaster, CBool(Me.Item(Col_VisibleInMasterLine, I).Value = 1), CBool(Me.Item(Col_VisibleInTransactionLine, I).Value = 1)), True, True)
                                    AgCl.AddAgTextColumn(mLineGrid, mColumnNamePostAc, 100, 0, Me.Item(Col_Charges, I).Value & " Post A/c", False)
                                    If Agl.StrCmp(Agl.PubUserName, "Super") Then mLineGrid.Columns(mColumnNamePostAc).ToolTipText = mColumnNamePostAc
                                    AgCl.AddAgTextColumn(mLineGrid, mColumnNameContraAc, 100, 0, Me.Item(Col_Charges, I).Value & " Contra A/c", False)
                                    If Agl.StrCmp(Agl.PubUserName, "Super") Then mLineGrid.Columns(mColumnNameContraAc).ToolTipText = mColumnNameContraAc

                                End If


                                If CBool(Me.Item(Col_LineItem, I).Value = "1") AndAlso Not mLineGrid Is Nothing Then
                                    Me.CurrentCell = Me(Col_Percentage, I)
                                    Me.CurrentCell.ReadOnly = True
                                    Me.CurrentCell.Style.BackColor = mReadOnlyColor
                                    mLineGrid.AgDefaultValue(mColumnNamePer) = Agl.XNull(.Rows(I)("Value"))
                                Else
                                    Me.Item(Col_Percentage, I).Value = Agl.XNull(.Rows(I)("Value"))
                                End If
                                Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInTransactionFooter, I).Value = 1)
                                If mIsMaster Then
                                    Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInMaster, I).Value = 1)
                                End If

                            Case "PERCENTAGE OR AMOUNT"
                                If Me.Rows(I).Visible = False Then Me.Rows(I).Visible = True

                                If Not mLineGrid Is Nothing Then
                                    AgCl.AddAgNumberColumn(mLineGrid, mColumnNamePer, 100, 8, 2, True, Me.Item(Col_Charges, I).Value & " @", IIf(mIsMaster, CBool(Me.Item(Col_VisibleInMasterLine, I).Value = 1), CBool(Me.Item(Col_VisibleInTransactionLine, I).Value = 1)), Not CBool(Me.Item(Col_LineItem, I).Value = "1"), True)
                                    AgCl.AddAgNumberColumn(mLineGrid, mColumnName, 100, 8, 2, True, Me.Item(Col_Charges, I).Value, IIf(mIsMaster, CBool(Me.Item(Col_VisibleInMasterLine, I).Value = 1), CBool(Me.Item(Col_VisibleInTransactionLine, I).Value = 1)), Not CBool(Me.Item(Col_LineItem, I).Value = "1"), True)
                                    AgCl.AddAgTextColumn(mLineGrid, mColumnNamePostAc, 100, 0, Me.Item(Col_Charges, I).Value & " Post A/c", False)
                                    If Agl.StrCmp(Agl.PubUserName, "Super") Then mLineGrid.Columns(mColumnNamePostAc).ToolTipText = mColumnNamePostAc
                                    AgCl.AddAgTextColumn(mLineGrid, mColumnNameContraAc, 100, 0, Me.Item(Col_Charges, I).Value & " Contra A/c", False)
                                    If Agl.StrCmp(Agl.PubUserName, "Super") Then mLineGrid.Columns(mColumnNameContraAc).ToolTipText = mColumnNameContraAc

                                    If CBool(Me.Item(Col_VisibleInTransactionLine, I).Value = 1) Then
                                        Me.CurrentCell = Me(Col_Amount, I)
                                        Me.CurrentCell.ReadOnly = True
                                        Me.CurrentCell.Style.BackColor = mReadOnlyColor
                                    End If
                                End If



                                If CBool(Me.Item(Col_LineItem, I).Value = "1") AndAlso Not mLineGrid Is Nothing Then
                                    Me.CurrentCell = Me(Col_Percentage, I)
                                    Me.CurrentCell.ReadOnly = True
                                    Me.CurrentCell.Style.BackColor = mReadOnlyColor

                                    Me.CurrentCell = Me(Col_Amount, I)
                                    Me.CurrentCell.ReadOnly = True
                                    Me.CurrentCell.Style.BackColor = mReadOnlyColor

                                    mLineGrid.AgDefaultValue(mColumnNamePer) = Agl.XNull(.Rows(I)("Value"))
                                Else
                                    Me.Item(Col_Percentage, I).Value = Agl.XNull(.Rows(I)("Value"))
                                End If
                                Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInTransactionFooter, I).Value = 1)
                                If mIsMaster Then
                                    Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInMaster, I).Value = 1)
                                End If


                            Case "PERCENTAGE FROM COLUMN"
                                If Me.Rows(I).Visible = False Then Me.Rows(I).Visible = True
                                Me.CurrentCell = Me(Col_Amount, I)
                                Me.CurrentCell.ReadOnly = True
                                Me.CurrentCell.Style.BackColor = mReadOnlyColor
                                Me.CurrentCell = Me(Col_Amount, I)
                                Me.CurrentCell.ReadOnly = True
                                Me.CurrentCell.Style.BackColor = mReadOnlyColor

                                Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInTransactionFooter, I).Value = 1)
                                If mIsMaster Then
                                    Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInMaster, I).Value = 1)
                                End If

                                If Not mLineGrid Is Nothing Then
                                    AgCl.AddAgNumberColumn(mLineGrid, mColumnName, 100, 8, 2, True, Me.Item(Col_Charges, I).Value & " @", IIf(mIsMaster, CBool(Me.Item(Col_VisibleInMasterLine, I).Value = 1), CBool(Me.Item(Col_VisibleInTransactionLine, I).Value = 1)), True, True)
                                    AgCl.AddAgNumberColumn(mLineGrid, mColumnName, 100, 8, 2, True, Me.Item(Col_Charges, I).Value, IIf(mIsMaster, CBool(Me.Item(Col_VisibleInMasterLine, I).Value = 1), CBool(Me.Item(Col_VisibleInTransactionLine, I).Value = 1)), True, True)
                                    AgCl.AddAgTextColumn(mLineGrid, mColumnNamePostAc, 100, 0, Me.Item(Col_Charges, I).Value & " Post A/c", False)
                                    If Agl.StrCmp(Agl.PubUserName, "Super") Then mLineGrid.Columns(mColumnNamePostAc).ToolTipText = mColumnNamePostAc
                                    AgCl.AddAgTextColumn(mLineGrid, mColumnNameContraAc, 100, 0, Me.Item(Col_Charges, I).Value & " Contra A/c", False)
                                    If Agl.StrCmp(Agl.PubUserName, "Super") Then mLineGrid.Columns(mColumnNameContraAc).ToolTipText = mColumnNameContraAc

                                End If

                            Case "FIXEDVALUE FROM COLUMN"
                                If Me.Rows(I).Visible = False Then Me.Rows(I).Visible = True
                                Me.CurrentCell = Me(Col_Amount, I)
                                Me.CurrentCell.ReadOnly = True
                                Me.CurrentCell.Style.BackColor = mReadOnlyColor
                                Me.CurrentCell = Me(Col_Amount, I)
                                Me.CurrentCell.ReadOnly = True
                                Me.CurrentCell.Style.BackColor = mReadOnlyColor

                                Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInTransactionFooter, I).Value = 1)
                                If mIsMaster Then
                                    Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInMaster, I).Value = 1)
                                End If

                                If Not mLineGrid Is Nothing Then
                                    AgCl.AddAgNumberColumn(mLineGrid, mColumnName, 100, 8, 2, True, Me.Item(Col_Charges, I).Value, IIf(mIsMaster, CBool(Me.Item(Col_VisibleInMasterLine, I).Value = 1), CBool(Me.Item(Col_VisibleInTransactionLine, I).Value = 1)), True, True)
                                    AgCl.AddAgTextColumn(mLineGrid, mColumnNamePostAc, 100, 0, Me.Item(Col_Charges, I).Value & " Post A/c", False)
                                    If Agl.StrCmp(Agl.PubUserName, "Super") Then mLineGrid.Columns(mColumnNamePostAc).ToolTipText = mColumnNamePostAc
                                    AgCl.AddAgTextColumn(mLineGrid, mColumnNameContraAc, 100, 0, Me.Item(Col_Charges, I).Value & " Contra A/c", False)
                                    If Agl.StrCmp(Agl.PubUserName, "Super") Then mLineGrid.Columns(mColumnNameContraAc).ToolTipText = mColumnNameContraAc
                                End If

                            Case Else
                                If Not mLineGrid Is Nothing Then
                                    AgCl.AddAgNumberColumn(mLineGrid, mColumnName, 100, 8, 2, True, Me.Item(Col_Charges, I).Value, IIf(mIsMaster, CBool(Me.Item(Col_VisibleInMasterLine, I).Value = 1), CBool(Me.Item(Col_VisibleInTransactionLine, I).Value = 1)), True, True)
                                    AgCl.AddAgTextColumn(mLineGrid, mColumnNamePostAc, 100, 0, Me.Item(Col_Charges, I).Value & " Post A/c", False)
                                    If Agl.StrCmp(Agl.PubUserName, "Super") Then mLineGrid.Columns(mColumnNamePostAc).ToolTipText = mColumnNamePostAc
                                    AgCl.AddAgTextColumn(mLineGrid, mColumnNameContraAc, 100, 0, Me.Item(Col_Charges, I).Value & " Contra A/c", False)
                                    If Agl.StrCmp(Agl.PubUserName, "Super") Then mLineGrid.Columns(mColumnNameContraAc).ToolTipText = mColumnNameContraAc

                                End If
                        End Select

                    Next I
                End If
            End With

            If Not mLineGrid Is Nothing Then
                SetGridDisplayIndex()
            End If
        End If

        If Not mLineGrid Is Nothing Then
            AgCl.AddAgTextColumn(mLineGrid, "Is Database Value", "80", 10, "Is Database Value", False, True)
        End If

        Me.AgSkipReadOnlyColumns = True
        Me.AgAllowFind = False
        Agl.GridDesign(Me)
    End Sub

    Public Sub SetGridDisplayIndex()
        Dim I As Integer

        For I = 0 To Me.Rows.Count - 1
            If Val(Me.Item(Col_GridDisplayIndex, I).Value) > 0 Then
                Select Case UCase(Me.Item(Col_Value_Type, I).Value)
                    Case "FIXEDVALUE", "FIXEDVALUE CHANGEABLE", "FIXEDVALUE FROM COLUMN"
                        mLineGrid.Columns(GetColName(Me.Item(Col_Charges, I).Value)).DisplayIndex = Val(Me.Item(Col_GridDisplayIndex, I).Value)
                    Case "PERCENTAGE", "PERCENTAGE CHANGEABLE", "PERCENTAGE FROM COLUMN"
                        mLineGrid.Columns(GetColName(Me.Item(Col_Charges, I).Value)).DisplayIndex = Val(Me.Item(Col_GridDisplayIndex, I).Value)
                        mLineGrid.Columns(GetColNamePer(Me.Item(Col_Charges, I).Value)).DisplayIndex = Val(Me.Item(Col_GridDisplayIndex, I).Value)
                End Select
            End If
        Next
    End Sub

    Public Sub Grid_Disp()
        Dim I As Integer
        For I = 0 To Me.Rows.Count - 1
            Select Case UCase(Me.Item(Col_Value_Type, I).Value)
                Case "FIXEDVALUE"
                    If Me.Rows(I).Visible = False Then Me.Rows(I).Visible = True
                    Me.CurrentCell = Me(Col_Percentage, I)
                    Me.CurrentCell.ReadOnly = True
                    Me.CurrentCell.Style.BackColor = mReadOnlyColor
                    Me.CurrentCell = Me(Col_Amount, I)
                    Me.CurrentCell.ReadOnly = True
                    Me.CurrentCell.Style.BackColor = mReadOnlyColor


                    Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInTransactionFooter, I).Value = 1)
                    If mIsMaster Then
                        Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInMaster, I).Value = 1)
                    End If

                Case "PERCENTAGE"
                    If Me.Rows(I).Visible = False Then Me.Rows(I).Visible = True
                    Me.CurrentCell = Me(Col_Percentage, I)
                    Me.CurrentCell.ReadOnly = True
                    Me.CurrentCell.Style.BackColor = mReadOnlyColor
                    Me.CurrentCell = Me(Col_Amount, I)
                    Me.CurrentCell.ReadOnly = True
                    Me.CurrentCell.Style.BackColor = mReadOnlyColor

                    Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInTransactionFooter, I).Value = 1)
                    If mIsMaster Then
                        Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInMaster, I).Value = 1)
                    End If




                Case "FIXEDVALUE CHANGEABLE"
                    If Me.Rows(I).Visible = False Then Me.Rows(I).Visible = True
                    Me.CurrentCell = Me(Col_Percentage, I)
                    Me.CurrentCell.ReadOnly = True
                    Me.CurrentCell.Style.BackColor = mReadOnlyColor





                    If CBool(Me.Item(Col_LineItem, I).Value = "1") Then
                        Me.CurrentCell = Me(Col_Amount, I)
                        Me.CurrentCell.ReadOnly = True
                        Me.CurrentCell.Style.BackColor = mReadOnlyColor
                    End If

                    Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInTransactionFooter, I).Value = 1)
                    If mIsMaster Then
                        Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInMaster, I).Value = 1)
                    End If

                Case "PERCENTAGE CHANGEABLE"
                    If Me.Rows(I).Visible = False Then Me.Rows(I).Visible = True
                    Me.CurrentCell = Me(Col_Amount, I)
                    Me.CurrentCell.ReadOnly = True
                    Me.CurrentCell.Style.BackColor = mReadOnlyColor




                    If CBool(Me.Item(Col_LineItem, I).Value = "1") Then
                        Me.CurrentCell = Me(Col_Percentage, I)
                        Me.CurrentCell.ReadOnly = True
                        Me.CurrentCell.Style.BackColor = mReadOnlyColor
                    End If
                    Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInTransactionFooter, I).Value = 1)
                    If mIsMaster Then
                        Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInMaster, I).Value = 1)
                    End If

                Case "PERCENTAGE OR AMOUNT"
                    If Me.Rows(I).Visible = False Then Me.Rows(I).Visible = True
                    'Me.CurrentCell = Me(Col_Amount, I)
                    'Me.CurrentCell.ReadOnly = True
                    'Me.CurrentCell.Style.BackColor = mReadOnlyColor


                    If CBool(Me.Item(Col_LineItem, I).Value = "1") Then
                        Me.CurrentCell = Me(Col_Amount, I)
                        Me.CurrentCell.ReadOnly = True
                        Me.CurrentCell.Style.BackColor = mReadOnlyColor


                        Me.CurrentCell = Me(Col_Percentage, I)
                        Me.CurrentCell.ReadOnly = True
                        Me.CurrentCell.Style.BackColor = mReadOnlyColor
                    End If
                    Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInTransactionFooter, I).Value = 1)
                    If mIsMaster Then
                        Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInMaster, I).Value = 1)
                    End If

                Case "PERCENTAGE FROM COLUMN"
                    If Me.Rows(I).Visible = False Then Me.Rows(I).Visible = True
                    Me.CurrentCell = Me(Col_Amount, I)
                    Me.CurrentCell.ReadOnly = True
                    Me.CurrentCell.Style.BackColor = mReadOnlyColor
                    Me.CurrentCell = Me(Col_Amount, I)
                    Me.CurrentCell.ReadOnly = True
                    Me.CurrentCell.Style.BackColor = mReadOnlyColor

                    Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInTransactionFooter, I).Value = 1)
                    If mIsMaster Then
                        Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInMaster, I).Value = 1)
                    End If


                Case "FIXEDVALUE FROM COLUMN"
                    If Me.Rows(I).Visible = False Then Me.Rows(I).Visible = True
                    Me.CurrentCell = Me(Col_Amount, I)
                    Me.CurrentCell.ReadOnly = True
                    Me.CurrentCell.Style.BackColor = mReadOnlyColor
                    Me.CurrentCell = Me(Col_Amount, I)
                    Me.CurrentCell.ReadOnly = True
                    Me.CurrentCell.Style.BackColor = mReadOnlyColor

                    Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInTransactionFooter, I).Value = 1)
                    If mIsMaster Then
                        Me.CurrentRow.Visible = CBool(Me.Item(Col_VisibleInMaster, I).Value = 1)
                    End If

                Case Else
            End Select
        Next I
    End Sub

    'Public Sub Calculation(Optional ByVal IsReverse As Boolean = False)
    '    Dim I As Integer
    '    Dim iL As Integer
    '    Dim J As Integer
    '    Dim K As Integer
    '    Dim StrConvFormula$ = ""
    '    Dim StrOrgFormula$, StrTemp$
    '    Dim BlnFlag As Boolean
    '    Dim StrCode$ = ""
    '    Dim mTotalGrossAmount As Double
    '    Dim mTotalTempAmount As Double
    '    Dim mUsedValue As Double
    '    Dim mCost As Double
    '    Dim bBaseColumn As Integer
    '    Dim bTotalBaseColumnAmt As Integer
    '    Dim bFirstRowHavingValueInBaseColumn As Integer
    '    Dim mRowIndex_RoundOff As Integer = -1
    '    Dim mRowIndex_RoundOff_BaseColumn As String = ""


    '    If mStructure = "" Then Exit Sub
    '    mTotalGrossAmount = 0
    '    If Not mLineGrid Is Nothing Then



    '        For J = 0 To mLineGrid.RowCount - 1
    '            If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
    '                If mColumnSalesTaxGroupItem > -1 Then
    '                    DrPostingGroupSalesTax = DtPostingGroupSalesTax.Select("PostingGroupSalesTaxItem='" & mLineGrid.AgSelectedValue(mColumnSalesTaxGroupItem, J) & "' And PostingGroupSalesTaxParty='" & mSalesTaxGroupParty & "' ", "WEF Desc")
    '                    If DrPostingGroupSalesTax.Length <= 0 Then
    '                        DrPostingGroupSalesTax = DtPostingGroupSalesTax.Select("PostingGroupSalesTaxItem='" & mSalesTaxGroupItem & "' And PostingGroupSalesTaxParty='" & mSalesTaxGroupParty & "' ", "WEF Desc")
    '                    End If
    '                ElseIf mSalesTaxGroupItem <> "" Then
    '                    DrPostingGroupSalesTax = DtPostingGroupSalesTax.Select("PostingGroupSalesTaxItem='" & mSalesTaxGroupItem & "' And PostingGroupSalesTaxParty='" & mSalesTaxGroupParty & "' ", "WEF Desc")
    '                End If


    '                For I = 0 To Me.Rows.Count - 1
    '                    Select Case UCase(Me.Item(Col_Charge_Type, I).Value)
    '                        Case "SALESTAXASSESSABLEAMT"
    '                            If mVoucherCategory.ToUpper = "" Then Err.Raise(1, , "Vouhcer category must be defined either purchase or sales, if SalesTax is used in structure.")
    '                            If mVoucherCategory.ToUpper <> "SALES" And mVoucherCategory.ToUpper <> "PURCH" Then Err.Raise(1, , "Vouhcer category must be either purchase or sales, if SalesTax is used in structure.")
    '                            If mColumnSalesTaxGroupItem >= 0 And mSalesTaxGroupParty <> "" Then
    '                                If DrPostingGroupSalesTax.Length > 0 Then
    '                                    If Me.Item(Col_LineItem, I).Value Then
    '                                        If mVoucherCategory.ToUpper = "SALES" Then
    '                                            mLineGrid.Item(GetColNamePostAc(Me.Item(Col_Charges, I).Value), J).value = Agl.XNull(DrPostingGroupSalesTax(0)("SalesAc"))
    '                                        ElseIf mVoucherCategory.ToUpper = "PURCH" Then
    '                                            mLineGrid.Item(GetColNamePostAc(Me.Item(Col_Charges, I).Value), J).value = Agl.XNull(DrPostingGroupSalesTax(0)("PurchaseAc"))
    '                                        End If
    '                                    End If
    '                                End If
    '                            Else
    '                                mLineGrid.Item(GetColNamePostAc(Me.Item(Col_Charges, I).Value), J).value = ""
    '                            End If

    '                        Case "SALESTAX"
    '                            If mVoucherCategory.ToUpper = "" Then Err.Raise(1, , "Vouhcer category must be defined either purchase or sales, if SalesTax is used in structure.")
    '                            If mVoucherCategory.ToUpper <> "SALES" And mVoucherCategory.ToUpper <> "PURCH" Then Err.Raise(1, , "Vouhcer category must be either purchase or sales, if SalesTax is used in structure.")

    '                            If mColumnSalesTaxGroupItem >= 0 And mSalesTaxGroupParty <> "" Then
    '                                If DrPostingGroupSalesTax.Length > 0 Then
    '                                    If Me.Item(Col_LineItem, I).Value Then
    '                                        mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).value = Agl.XNull(DrPostingGroupSalesTax(0)("SalesTax"))
    '                                        If mVoucherCategory.ToUpper = "SALES" Then
    '                                            mLineGrid.Item(GetColNamePostAc(Me.Item(Col_Charges, I).Value), J).value = Agl.XNull(DrPostingGroupSalesTax(0)("SalesTaxOnSalesAc"))
    '                                        ElseIf mVoucherCategory.ToUpper = "PURCH" Then
    '                                            mLineGrid.Item(GetColNamePostAc(Me.Item(Col_Charges, I).Value), J).value = Agl.XNull(DrPostingGroupSalesTax(0)("SalesTaxOnPurchaseAc"))
    '                                        End If
    '                                    Else
    '                                        Me.Item(Col_Percentage, I).Value = Agl.XNull(DrPostingGroupSalesTax(0)("SalesTax"))
    '                                    End If
    '                                Else
    '                                    MsgBox("SalesTax posting groups not defined for SalesTax in selected branch and division.")
    '                                End If
    '                            Else
    '                                mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).value = ""
    '                                mLineGrid.Item(GetColNamePostAc(Me.Item(Col_Charges, I).Value), J).value = ""
    '                                Me.Item(Col_Percentage, I).Value = ""
    '                            End If

    '                        Case "SALESADDITIONALTAX"
    '                            If mColumnSalesTaxGroupItem >= 0 And mSalesTaxGroupParty <> "" Then
    '                                If DrPostingGroupSalesTax.Length > 0 Then
    '                                    If Me.Item(Col_LineItem, I).Value Then
    '                                        mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).value = Agl.XNull(DrPostingGroupSalesTax(0)("AdditionalTax"))
    '                                        If mVoucherCategory.ToUpper = "SALES" Then
    '                                            mLineGrid.Item(GetColNamePostAc(Me.Item(Col_Charges, I).Value), J).value = Agl.XNull(DrPostingGroupSalesTax(0)("AdditionalTaxOnSalesAc"))
    '                                        ElseIf mVoucherCategory.ToUpper = "PURCH" Then
    '                                            mLineGrid.Item(GetColNamePostAc(Me.Item(Col_Charges, I).Value), J).value = Agl.XNull(DrPostingGroupSalesTax(0)("AdditionalTaxOnPurchaseAc"))
    '                                        End If
    '                                    Else
    '                                        Me.Item(Col_Percentage, I).Value = Agl.XNull(DrPostingGroupSalesTax(0)("AdditionalTax"))
    '                                    End If
    '                                Else
    '                                    MsgBox("SalesTax posting groups not defined for Sales Additional Tax in selected branch and division.")
    '                                End If
    '                            Else
    '                                mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).value = ""
    '                                Me.Item(Col_Percentage, I).Value = ""
    '                            End If

    '                    End Select
    '                Next

    '            End If
    '        Next




    '        For I = 0 To Me.RowCount - 1

    '            If Me.Item(Col_BaseColumn, I).Value <> "" Then

    '                bBaseColumn = mLineGrid.Columns(Me.Item(Col_BaseColumn, I).Value).Index


    '                If UCase(Me.Item(Col_Value_Type, I).Value) = "ROUND_OFF" Then
    '                    Me.Item(Col_Amount, I).Value = ""
    '                    mRowIndex_RoundOff = I
    '                    mRowIndex_RoundOff_BaseColumn = Me.Item(Col_BaseColumn, I).Value
    '                End If


    '                bFirstRowHavingValueInBaseColumn = -1
    '                bTotalBaseColumnAmt = 0
    '                mUsedValue = 0

    '                For J = 0 To mLineGrid.RowCount - 1
    '                    If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
    '                        bTotalBaseColumnAmt += Val(mLineGrid.Item(bBaseColumn, J).Value)
    '                        If Val(mLineGrid.Item(bBaseColumn, J).Value) > 0 And bFirstRowHavingValueInBaseColumn < 0 Then
    '                            bFirstRowHavingValueInBaseColumn = J
    '                        End If
    '                    End If
    '                Next

    '                If Not IsReverse Then
    '                    Select Case UCase(Me.Item(Col_Value_Type, I).Value)
    '                        Case "FIXEDVALUE", "FIXEDVALUE CHANGEABLE"
    '                            If Me.Item(Col_LineItem, I).Value = 0 Then
    '                                For J = mLineGrid.RowCount - 1 To 0 Step -1
    '                                    If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
    '                                        If J = bFirstRowHavingValueInBaseColumn Then
    '                                            mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) - mUsedValue, "0.00")
    '                                            mUsedValue = 0
    '                                        Else
    '                                            mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) * Val(mLineGrid.Item(bBaseColumn, J).Value) / bTotalBaseColumnAmt, "0.00")
    '                                            mUsedValue += mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value
    '                                        End If
    '                                    End If
    '                                Next
    '                            End If

    '                        Case "PERCENTAGE OR AMOUNT"
    '                            If Me.Item(Col_LineItem, I).Value = 0 Then
    '                                If Val(Me.Item(Col_Percentage, I).Value) = 0 Then
    '                                    For J = mLineGrid.RowCount - 1 To 0 Step -1
    '                                        If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
    '                                            If J = bFirstRowHavingValueInBaseColumn Then
    '                                                mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) - mUsedValue, "0.00")
    '                                                mUsedValue = 0
    '                                            Else
    '                                                mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) * Val(mLineGrid.Item(bBaseColumn, J).Value) / bTotalBaseColumnAmt, "0.00")
    '                                                mUsedValue += mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value
    '                                            End If
    '                                        End If
    '                                    Next
    '                                End If
    '                            End If

    '                    End Select
    '                End If
    '            End If

    '            Select Case UCase(Me.Item(Col_Value_Type, I).Value)
    '                Case "FIXEDVALUE FROM COLUMN"
    '                    For J = 0 To mLineGrid.RowCount - 1
    '                        If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
    '                            mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).VALUE = mLineGrid.Item(Me.Item(Col_Value, I).Value, J).VALUE
    '                        End If
    '                    Next
    '                Case "PERCENTAGE FROM COLUMN"
    '                    For J = 0 To mLineGrid.RowCount - 1
    '                        If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
    '                            mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = mLineGrid.Item(Me.Item(Col_Value, I).Value, J).VALUE
    '                        End If
    '                    Next
    '                Case "FIXEDVALUE CHANGEABLE", "FIXEDVALUE"
    '                    'If Val(Me.Item(Col_Amount, I).Value) = 0 And Val(Me.Item(Col_Value, I).Value) > 0 Then
    '                    '    Me.Item(Col_Amount, I).Value = Val(Me.Item(Col_Value, I).Value)
    '                    'End If
    '                Case "PERCENTAGE", "PERCENTAGE CHANGEABLE"
    '                    If Me.Item(Col_LineItem, I).Value <> 1 Then
    '                        For J = 0 To mLineGrid.RowCount - 1
    '                            If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
    '                                mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = Val(Me.Item(Col_Percentage, I).Value)
    '                            End If
    '                        Next
    '                    End If
    '                Case "PERCENTAGE OR AMOUNT"
    '                    If Me.Item(Col_LineItem, I).Value <> 1 Then
    '                        For J = 0 To mLineGrid.RowCount - 1
    '                            If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
    '                                If Val(Me.Item(Col_Percentage, I).Value) > 0 Then
    '                                    mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = Val(Me.Item(Col_Percentage, I).Value)
    '                                Else
    '                                    mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = 0
    '                                End If
    '                            End If
    '                        Next
    '                    End If

    '            End Select
    '        Next



    '        Dim mColIndex As Integer = 0
    '        For I = 0 To Me.RowCount - 1
    '            StrOrgFormula = Trim(Me.Item(Col_Calculation, I).Value)
    '            StrConvFormula = ""
    '            If Not StrOrgFormula = "" Then
    '                For iL = 0 To mLineGrid.RowCount - 1
    '                    If UCase(Me.Item(Col_Value_Type, I).Value) = "PERCENTAGE OR AMOUNT" Then
    '                        If Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), iL).Value) = 0 Then
    '                            StrOrgFormula = ""
    '                        Else
    '                            StrOrgFormula = Trim(Me.Item(Col_Calculation, I).Value)
    '                        End If
    '                    ElseIf UCase(Me.Item(Col_Value_Type, I).Value) = "ROUND_OFF" Then
    '                        StrOrgFormula = ""
    '                        mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), iL).Value = 0
    '                    End If

    '                    If Not StrOrgFormula = "" Then
    '                        For J = 1 To Len(Trim(Me.Item(Col_Calculation, I).Value))
    '                            StrTemp = UCase(Mid(StrOrgFormula, J, 1))
    '                            If BlnFlag Then
    '                                If StrTemp = "}" Then
    '                                    BlnFlag = False
    '                                    For K = 0 To Me.RowCount - 1
    '                                        If Agl.StrCmp(Me.Item(Col_ChargesManualCode, K).Value, Replace(StrCode, "@", "")) Then
    '                                            mColIndex = K
    '                                            Exit For
    '                                        End If
    '                                    Next
    '                                    'mColIndex = Val(StrCode) - 1
    '                                    If I = mColIndex Then
    '                                        Select Case UCase(Me.Item(Col_Value_Type, I).Value)
    '                                            Case "PERCENTAGE", "PERCENTAGE CHANGEABLE", "PERCENTAGE FROM COLUMN", "PERCENTAGE OR AMOUNT"
    '                                                If Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.00") < 0 Then
    '                                                    StrConvFormula = StrConvFormula + "(" + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000") + ")"
    '                                                Else
    '                                                    StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
    '                                                End If
    '                                            Case Else
    '                                                If InStr(StrTemp, "@") > 0 Then
    '                                                    StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
    '                                                Else
    '                                                    StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
    '                                                End If
    '                                        End Select
    '                                    Else
    '                                        If Format(Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.00") < 0 Then
    '                                            StrConvFormula = StrConvFormula + "(" + Format(Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000") + ")"
    '                                        Else
    '                                            If InStr(StrCode, "@") > 0 Then
    '                                                StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
    '                                            Else
    '                                                StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
    '                                            End If
    '                                        End If
    '                                    End If
    '                                    StrCode = ""
    '                                Else
    '                                    StrCode = StrCode + StrTemp
    '                                End If
    '                            Else
    '                                If StrTemp = "{" Then
    '                                    StrCode = ""
    '                                    BlnFlag = True
    '                                Else
    '                                    StrConvFormula = StrConvFormula + StrTemp
    '                                End If
    '                            End If

    '                        Next
    '                    End If
    '                    If StrConvFormula <> "" Then
    '                        mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), iL).Value = Format(Agl.FillData(Agl.Chk_Qry("Select " & FilterQuery(StrConvFormula, iL)), Agl.GCn).Tables(0).Rows(0)(0), "0.".PadRight(mAgCalcRounding + 2, "0"))
    '                        StrConvFormula = ""
    '                    End If
    '                Next

    '            End If
    '        Next





    '        If mRowIndex_RoundOff > 0 Then
    '            mColIndex = 0
    '            For I = 0 To Me.RowCount - 1
    '                If I = mRowIndex_RoundOff Then
    '                    StrOrgFormula = Trim(Me.Item(Col_Calculation, I).Value)
    '                    StrConvFormula = ""
    '                    If Not StrOrgFormula = "" Then
    '                        For J = 1 To Len(Trim(Me.Item(Col_Calculation, I).Value))
    '                            StrTemp = UCase(Mid(StrOrgFormula, J, 1))
    '                            If BlnFlag Then
    '                                If StrTemp = "}" Then
    '                                    BlnFlag = False
    '                                    For K = 0 To Me.RowCount - 1
    '                                        If Agl.StrCmp(Me.Item(Col_ChargesManualCode, K).Value, Replace(StrCode, "@", "")) Then
    '                                            mColIndex = K
    '                                            Exit For
    '                                        End If
    '                                    Next

    '                                    StrConvFormula = StrConvFormula + CStr(Val(Me.Item(Col_Amount, mColIndex).Value))
    '                                    StrCode = ""
    '                                Else
    '                                    StrCode = StrCode + StrTemp
    '                                End If
    '                            Else
    '                                If StrTemp = "{" Then
    '                                    StrCode = ""
    '                                    BlnFlag = True
    '                                Else
    '                                    StrConvFormula = StrConvFormula + StrTemp
    '                                End If
    '                            End If
    '                        Next

    '                        If StrConvFormula <> "" Then
    '                            Me.Item(Col_Amount, I).Value = Agl.FillData(Agl.Chk_Qry("Select " & StrConvFormula), Agl.GCn).Tables(0).Rows(0)(0)
    '                            StrConvFormula = ""
    '                        End If

    '                    End If
    '                End If
    '            Next
    '        End If





    '        For I = 0 To Me.RowCount - 1
    '            If Me.Item(Col_BaseColumn, I).Value <> "" Then
    '                bBaseColumn = mLineGrid.Columns(Me.Item(Col_BaseColumn, I).Value).Index

    '                bFirstRowHavingValueInBaseColumn = -1
    '                bTotalBaseColumnAmt = 0
    '                mUsedValue = 0
    '                For J = 0 To mLineGrid.RowCount - 1
    '                    If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
    '                        bTotalBaseColumnAmt += Val(mLineGrid.Item(bBaseColumn, J).Value)
    '                        If Val(mLineGrid.Item(bBaseColumn, J).Value) > 0 And bFirstRowHavingValueInBaseColumn < 0 Then
    '                            bFirstRowHavingValueInBaseColumn = J
    '                        End If
    '                    End If
    '                Next


    '                If Not IsReverse Then
    '                    Select Case UCase(Me.Item(Col_Value_Type, I).Value)
    '                        Case "FIXEDVALUE", "FIXEDVALUE CHANGEABLE", "ROUND_OFF"
    '                            If Me.Item(Col_LineItem, I).Value = 0 Then
    '                                For J = mLineGrid.RowCount - 1 To 0 Step -1
    '                                    If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
    '                                        If J = bFirstRowHavingValueInBaseColumn Then
    '                                            mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) - mUsedValue, "0.00")
    '                                            mUsedValue = 0
    '                                        Else
    '                                            mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) * Val(mLineGrid.Item(bBaseColumn, J).Value) / bTotalBaseColumnAmt, "0.00")
    '                                            mUsedValue += mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value
    '                                        End If
    '                                    End If
    '                                Next
    '                            End If

    '                        Case "PERCENTAGE OR AMOUNT"
    '                            If Me.Item(Col_LineItem, I).Value = 0 Then
    '                                If Val(Me.Item(Col_Amount, I).Value) <= 0 Then
    '                                    For J = mLineGrid.RowCount - 1 To 0 Step -1
    '                                        If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
    '                                            If J = bFirstRowHavingValueInBaseColumn Then
    '                                                mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) - mUsedValue, "0.00")
    '                                                mUsedValue = 0
    '                                            Else
    '                                                mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) * Val(mLineGrid.Item(bBaseColumn, J).Value) / bTotalBaseColumnAmt, "0.00")
    '                                                mUsedValue += mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value
    '                                            End If
    '                                        End If
    '                                    Next
    '                                Else
    '                                    For J = 0 To mLineGrid.RowCount - 1
    '                                        If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
    '                                            mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = Val(Me.Item(Col_Percentage, I).Value)
    '                                        End If
    '                                    Next
    '                                End If
    '                            End If

    '                        Case "FIXEDVALUE FROM COLUMN"
    '                            For J = 0 To mLineGrid.RowCount - 1
    '                                If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
    '                                    mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).VALUE = mLineGrid.Item(Me.Item(Col_Value, I).Value, J).VALUE
    '                                End If
    '                            Next
    '                        Case "PERCENTAGE FROM COLUMN"
    '                            For J = 0 To mLineGrid.RowCount - 1
    '                                If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
    '                                    mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = mLineGrid.Item(Me.Item(Col_Value, I).Value, J).VALUE
    '                                End If
    '                            Next
    '                        Case "PERCENTAGE", "PERCENTAGE CHANGEABLE"
    '                            If Me.Item(Col_LineItem, I).Value <> 1 Then
    '                                For J = 0 To mLineGrid.RowCount - 1
    '                                    If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
    '                                        mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = Val(Me.Item(Col_Percentage, I).Value)
    '                                    End If
    '                                Next
    '                            End If
    '                    End Select
    '                End If
    '            End If
    '        Next



    '        If mRowIndex_RoundOff > 0 Then
    '            mColIndex = 0
    '            iL = 0
    '            For I = 0 To Me.RowCount - 1
    '                If Me.Item(Col_Charges, I).Value.ToString.ToUpper = mRowIndex_RoundOff_BaseColumn.ToUpper Then
    '                    StrOrgFormula = Trim(Me.Item(Col_Calculation, I).Value)
    '                    StrConvFormula = ""
    '                    If Not StrOrgFormula = "" Then
    '                        For iL = 0 To mLineGrid.RowCount - 1
    '                            If UCase(Me.Item(Col_Value_Type, I).Value) = "PERCENTAGE OR AMOUNT" Then
    '                                If Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), iL).Value) = 0 Then
    '                                    StrOrgFormula = ""
    '                                Else
    '                                    StrOrgFormula = Trim(Me.Item(Col_Calculation, I).Value)
    '                                End If
    '                            ElseIf UCase(Me.Item(Col_Value_Type, I).Value) = "ROUND_OFF" Then
    '                                StrOrgFormula = ""
    '                                mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), iL).Value = 0
    '                            End If

    '                            If Not StrOrgFormula = "" Then
    '                                For J = 1 To Len(Trim(Me.Item(Col_Calculation, I).Value))
    '                                    StrTemp = UCase(Mid(StrOrgFormula, J, 1))
    '                                    If BlnFlag Then
    '                                        If StrTemp = "}" Then
    '                                            BlnFlag = False
    '                                            For K = 0 To Me.RowCount - 1
    '                                                If Agl.StrCmp(Me.Item(Col_ChargesManualCode, K).Value, Replace(StrCode, "@", "")) Then
    '                                                    mColIndex = K
    '                                                    Exit For
    '                                                End If
    '                                            Next
    '                                            'mColIndex = Val(StrCode) - 1
    '                                            If I = mColIndex Then
    '                                                Select Case UCase(Me.Item(Col_Value_Type, I).Value)
    '                                                    Case "PERCENTAGE", "PERCENTAGE CHANGEABLE", "PERCENTAGE FROM COLUMN", "PERCENTAGE OR AMOUNT"
    '                                                        If Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.00") < 0 Then
    '                                                            StrConvFormula = StrConvFormula + "(" + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000") + ")"
    '                                                        Else
    '                                                            StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
    '                                                        End If
    '                                                    Case Else
    '                                                        If InStr(StrTemp, "@") > 0 Then
    '                                                            StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
    '                                                        Else
    '                                                            StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
    '                                                        End If
    '                                                End Select
    '                                            Else
    '                                                If Format(Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.00") < 0 Then
    '                                                    StrConvFormula = StrConvFormula + "(" + Format(Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000") + ")"
    '                                                Else
    '                                                    If InStr(StrCode, "@") > 0 Then
    '                                                        StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
    '                                                    Else
    '                                                        StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
    '                                                    End If
    '                                                End If
    '                                            End If
    '                                            StrCode = ""
    '                                        Else
    '                                            StrCode = StrCode + StrTemp
    '                                        End If
    '                                    Else
    '                                        If StrTemp = "{" Then
    '                                            StrCode = ""
    '                                            BlnFlag = True
    '                                        Else
    '                                            StrConvFormula = StrConvFormula + StrTemp
    '                                        End If
    '                                    End If

    '                                Next
    '                            End If
    '                            If StrConvFormula <> "" Then
    '                                mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), iL).Value = Format(Agl.FillData(Agl.Chk_Qry("Select " & FilterQuery(StrConvFormula, iL)), Agl.GCn).Tables(0).Rows(0)(0), "0.".PadRight(mAgCalcRounding + 2, "0"))
    '                                StrConvFormula = ""
    '                            End If
    '                        Next

    '                    End If
    '                End If
    '            Next
    '        End If
















    '        For I = 0 To mLineGrid.Rows.Count - 1
    '            mCost = 0
    '            For J = 0 To Me.RowCount - 1
    '                If Agl.StrCmp(Me.Item(Col_Charge_Type, J).Value, "Cost") Then
    '                    mLineGrid.Item(GetColName(Me.Item(Col_Charges, J).Value), I).value = Format(mCost, "0.00")
    '                    Exit For
    '                End If
    '                If Me.Item(Col_AffectCost, J).Value = "1" Then
    '                    mCost += Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, J).Value), I).value)
    '                ElseIf Me.Item(Col_AffectCost, J).Value = "0" Then
    '                    mCost -= Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, J).Value), I).value)
    '                End If
    '            Next
    '        Next


    '        For I = 0 To Me.RowCount - 1
    '            mTotalTempAmount = 0
    '            Select Case UCase(Me.Item(Col_Value_Type, I).Value)
    '                Case Else
    '                    For J = 0 To mLineGrid.RowCount - 1
    '                        If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
    '                            mTotalTempAmount += Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value)
    '                        End If
    '                    Next
    '                    Me.Item(Col_Amount, I).Value = Format(mTotalTempAmount, "0.00")

    '            End Select
    '        Next



















    '    Else     'if line grid not specified then only footer grid calculation will be called



    '        If mSalesTaxGroupItem <> "" Then
    '            DrPostingGroupSalesTax = DtPostingGroupSalesTax.Select("PostingGroupSalesTaxItem='" & mSalesTaxGroupItem & "' And PostingGroupSalesTaxParty='" & mSalesTaxGroupParty & "' ", "WEF Desc")
    '        End If


    '        For I = 0 To Me.Rows.Count - 1
    '            Select Case UCase(Me.Item(Col_Charge_Type, I).Value)
    '                Case "VAT"
    '                    If mSalesTaxGroupItem <> "" And mSalesTaxGroupParty <> "" Then
    '                        If DrPostingGroupSalesTax.Length > 0 Then
    '                            Me.Item(Col_Percentage, I).Value = DrPostingGroupSalesTax(0)("VAT")
    '                        Else
    '                            MsgBox("SalesTax posting groups not defined for VAT in selected branch and division.")
    '                        End If
    '                    Else
    '                        Me.Item(Col_Percentage, I).Value = ""
    '                    End If
    '                Case "CST"
    '                    If mSalesTaxGroupItem <> "" And mSalesTaxGroupParty <> "" Then
    '                        If DrPostingGroupSalesTax.Length > 0 Then
    '                            Me.Item(Col_Percentage, I).Value = DrPostingGroupSalesTax(0)("CST")
    '                        Else
    '                            MsgBox("SalesTax posting groups not defined for CST in selected branch and division.")
    '                        End If
    '                    Else
    '                        Me.Item(Col_Percentage, I).Value = ""
    '                    End If

    '                Case "SALESTAX"
    '                    If mSalesTaxGroupItem <> "" And mSalesTaxGroupParty <> "" Then
    '                        If DrPostingGroupSalesTax.Length > 0 Then
    '                            Me.Item(Col_Percentage, I).Value = DrPostingGroupSalesTax(0)("SalesTax")
    '                        Else
    '                            MsgBox("SalesTax posting groups not defined for SalesTax in selected branch and division.")
    '                        End If
    '                    Else
    '                        Me.Item(Col_Percentage, I).Value = ""
    '                    End If

    '                Case "SALESADDITIONALTAX"
    '                    If mSalesTaxGroupItem <> "" And mSalesTaxGroupParty <> "" Then
    '                        If DrPostingGroupSalesTax.Length > 0 Then
    '                            Me.Item(Col_Percentage, I).Value = DrPostingGroupSalesTax(0)("AdditionalTax")
    '                        Else
    '                            MsgBox("SalesTax posting groups not defined for Sales Additional Tax in selected branch and division.")
    '                        End If
    '                    Else
    '                        Me.Item(Col_Percentage, I).Value = ""
    '                    End If

    '                Case "SAT"
    '                    If mSalesTaxGroupItem <> "" And mSalesTaxGroupParty <> "" Then
    '                        If DrPostingGroupSalesTax.Length > 0 Then
    '                            Me.Item(Col_Percentage, I).Value = DrPostingGroupSalesTax(0)("AdditionalTax")
    '                        Else
    '                            MsgBox("SalesTax posting groups not defined for SAT in selected branch and division.")
    '                        End If
    '                    Else
    '                        Me.Item(Col_Percentage, I).Value = ""
    '                    End If
    '            End Select
    '        Next I



    '        Dim mColIndex As Integer = 0
    '        For I = 0 To Me.RowCount - 1
    '            StrOrgFormula = Trim(Me.Item(Col_Calculation, I).Value)
    '            If UCase(Me.Item(Col_Value_Type, I).Value) = "PERCENTAGE OR AMOUNT" Then
    '                If Val((Me.Item(Col_Percentage, I).Value)) <= 0 Then
    '                    StrOrgFormula = ""
    '                End If
    '            End If

    '            StrConvFormula = ""
    '            If Not StrOrgFormula = "" Then
    '                For J = 1 To Len(Trim(Me.Item(Col_Calculation, I).Value))
    '                    StrTemp = UCase(Mid(StrOrgFormula, J, 1))
    '                    If BlnFlag Then
    '                        If StrTemp = "}" Then
    '                            BlnFlag = False
    '                            For K = 0 To Me.RowCount - 1
    '                                If Agl.StrCmp(Me.Item(Col_ChargesManualCode, K).Value, Replace(StrCode, "@", "")) Then
    '                                    mColIndex = K
    '                                    Exit For
    '                                End If
    '                            Next

    '                            If I = mColIndex Then
    '                                Select Case UCase(Me.Item(Col_Value_Type, I).Value)
    '                                    Case "PERCENTAGE", "PERCENTAGE CHANGEABLE", "PERCENTAGE FROM COLUMN"
    '                                        'StrConvFormula = StrConvFormula + Format(Val(Me.Item(Col_Percentage, mColIndex).Value), "0.00")
    '                                        StrConvFormula = StrConvFormula + CStr(Val(Me.Item(Col_Percentage, mColIndex).Value))
    '                                    Case Else
    '                                        If InStr(StrTemp, "@") > 0 Then
    '                                            StrConvFormula = StrConvFormula + CStr(Val(Me.Item(Col_Percentage, mColIndex).Value))
    '                                        Else
    '                                            StrConvFormula = StrConvFormula + CStr(Val(Me.Item(Col_Amount, mColIndex).Value))
    '                                        End If

    '                                End Select
    '                            Else
    '                                StrConvFormula = StrConvFormula + CStr(Val(Me.Item(Col_Amount, mColIndex).Value))
    '                            End If
    '                            StrCode = ""
    '                        Else
    '                            StrCode = StrCode + StrTemp
    '                        End If
    '                    Else
    '                        If StrTemp = "{" Then
    '                            StrCode = ""
    '                            BlnFlag = True
    '                        Else
    '                            StrConvFormula = StrConvFormula + StrTemp
    '                        End If
    '                    End If

    '                Next

    '                If StrConvFormula <> "" Then
    '                    Me.Item(Col_Amount, I).Value = Agl.FillData(Agl.Chk_Qry("Select " & FilterQuery(StrConvFormula, iL)), Agl.GCn).Tables(0).Rows(0)(0)
    '                    StrConvFormula = ""
    '                End If

    '            End If
    '        Next




    '        'For I = 0 To Me.RowCount - 1
    '        '    If Me.Item(Col_BaseColumn, I).Value <> "" Then
    '        '        bBaseColumn = mLineGrid.Columns(Me.Item(Col_BaseColumn, I).Value).Index

    '        '        bFirstRowHavingValueInBaseColumn = -1
    '        '        bTotalBaseColumnAmt = 0
    '        '        mUsedValue = 0
    '        '        For J = 0 To mLineGrid.RowCount - 1
    '        '            If mLineGrid.Item(mColumnMandatory, J).Value <> ""  And mLineGrid.Rows(J).Visible = True Then
    '        '                bTotalBaseColumnAmt += Val(mLineGrid.Item(bBaseColumn, J).Value)
    '        '                If Val(mLineGrid.Item(bBaseColumn, J).Value) > 0 And bFirstRowHavingValueInBaseColumn < 0 Then
    '        '                    bFirstRowHavingValueInBaseColumn = J
    '        '                End If
    '        '            End If
    '        '        Next



    '        '        Select Case UCase(Me.Item(Col_Value_Type, I).Value)
    '        '            Case "FIXEDVALUE", "FIXEDVALUE CHANGEABLE"
    '        '                If Me.Item(Col_LineItem, I).Value = 0 Then
    '        '                    For J = mLineGrid.RowCount - 1 To 0 Step -1
    '        '                        If mLineGrid.Item(mColumnMandatory, J).Value <> ""  And mLineGrid.Rows(J).Visible = True Then
    '        '                            If J = bFirstRowHavingValueInBaseColumn Then
    '        '                                mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) - mUsedValue, "0.00")
    '        '                                mUsedValue = 0
    '        '                            Else
    '        '                                mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) * Val(mLineGrid.Item(bBaseColumn, J).Value) / bTotalBaseColumnAmt, "0.00")
    '        '                                mUsedValue += mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value
    '        '                            End If
    '        '                        End If
    '        '                    Next
    '        '                End If
    '        '            Case "FIXEDVALUE FROM COLUMN"
    '        '                For J = 0 To mLineGrid.RowCount - 1
    '        '                    If mLineGrid.Item(mColumnMandatory, J).Value <> ""  And mLineGrid.Rows(J).Visible = True Then
    '        '                        mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).VALUE = mLineGrid.Item(Me.Item(Col_Value, I).Value, J).VALUE
    '        '                    End If
    '        '                Next
    '        '            Case "PERCENTAGE FROM COLUMN"
    '        '                For J = 0 To mLineGrid.RowCount - 1
    '        '                    If mLineGrid.Item(mColumnMandatory, J).Value <> ""  And mLineGrid.Rows(J).Visible = True Then
    '        '                        mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = mLineGrid.Item(Me.Item(Col_Value, I).Value, J).VALUE
    '        '                    End If
    '        '                Next
    '        '            Case "PERCENTAGE", "PERCENTAGE CHANGEABLE"
    '        '                If Me.Item(Col_LineItem, I).Value <> 1 Then
    '        '                    For J = 0 To mLineGrid.RowCount - 1
    '        '                        If mLineGrid.Item(mColumnMandatory, J).Value <> ""  And mLineGrid.Rows(J).Visible = True Then
    '        '                            mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = Val(Me.Item(Col_Percentage, I).Value)
    '        '                        End If
    '        '                    Next
    '        '                End If
    '        '        End Select
    '        '    End If
    '        'Next




    '        mCost = 0
    '        For J = 0 To Me.RowCount - 1
    '            If Agl.StrCmp(Me.Item(Col_Charge_Type, J).Value, "Cost") Then
    '                Me.Item(Col_Amount, J).Value = Format(mCost, "0.00")
    '                Exit For
    '            End If
    '            If Me.Item(Col_AffectCost, J).Value = "1" Then
    '                mCost += Val(Me.Item(Col_Amount, J).Value)
    '            ElseIf Me.Item(Col_AffectCost, J).Value = "0" Then
    '                mCost -= Val(Me.Item(Col_Amount, J).Value)
    '            End If
    '        Next
    '    End If

    '    RaiseEvent Calculated()

    'End Sub

    Public Sub Calculation(Optional ByVal IsReverse As Boolean = False)
        Dim I As Integer
        Dim iL As Integer
        Dim J As Integer
        Dim K As Integer
        Dim StrConvFormula$ = ""
        Dim StrOrgFormula$, StrTemp$
        Dim BlnFlag As Boolean
        Dim StrCode$ = ""
        Dim mTotalGrossAmount As Double
        Dim mTotalTempAmount As Double
        Dim mUsedValue As Double
        Dim mCost As Double
        Dim bBaseColumn As Integer
        Dim bTotalBaseColumnAmt As Integer
        Dim bFirstRowHavingValueInBaseColumn As Integer
        Dim mRowIndex_RoundOff As Integer = -1
        Dim mRowIndex_RoundOff_BaseColumn As String = ""


        If mStructure = "" Then Exit Sub
        mTotalGrossAmount = 0
        If Not mLineGrid Is Nothing Then



            For J = 0 To mLineGrid.RowCount - 1
                If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then

                    For I = 0 To Me.Rows.Count - 1
                        If UCase(Me.Item(Col_Charge_Type, I).Value) <> "CHARGES" Then
                            If mColumnSalesTaxGroupItem > -1 Then
                                DrPostingGroupSalesTax = DtPostingGroupSalesTax.Select("PostingGroupSalesTaxItem='" & mLineGrid.AgSelectedValue(mColumnSalesTaxGroupItem, J) & "' And PostingGroupSalesTaxParty='" & mSalesTaxGroupParty & "' And PlaceOfSupply = '" & mPlaceOfSupply & "' And Process = '" & mVoucherCategory & "' And ChargeType='" & UCase(Me.Item(Col_Charge_Type, I).Value) & "'  ", "WEF Desc")
                                If DrPostingGroupSalesTax.Length <= 0 Then
                                    DrPostingGroupSalesTax = DtPostingGroupSalesTax.Select("PostingGroupSalesTaxItem='" & mSalesTaxGroupItem & "' And PostingGroupSalesTaxParty='" & mSalesTaxGroupParty & "'  And PlaceOfSupply = '" & mPlaceOfSupply & "' And Process = '" & mVoucherCategory & "' And ChargeType='" & UCase(Me.Item(Col_Charge_Type, I).Value) & "'  ", "WEF Desc")
                                End If
                            ElseIf mSalesTaxGroupItem <> "" Then
                                DrPostingGroupSalesTax = DtPostingGroupSalesTax.Select("PostingGroupSalesTaxItem='" & mSalesTaxGroupItem & "' And PostingGroupSalesTaxParty='" & mSalesTaxGroupParty & "'  And PlaceOfSupply = '" & mPlaceOfSupply & "' And Process = '" & mVoucherCategory & "' And ChargeType='" & UCase(Me.Item(Col_Charge_Type, I).Value) & "'  ", "WEF Desc")
                            End If

                            If DrPostingGroupSalesTax IsNot Nothing Then
                                If DrPostingGroupSalesTax.Length > 0 Then
                                    If Me.Item(Col_LineItem, I).Value Then
                                        If Agl.VNull(DrPostingGroupSalesTax(0)("Percentage")) > 0 Then
                                            mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).value = Agl.XNull(DrPostingGroupSalesTax(0)("Percentage"))
                                        End If
                                        If Agl.XNull(DrPostingGroupSalesTax(0)("LedgerAc")) <> "" Then
                                            mLineGrid.Item(GetColNamePostAc(Me.Item(Col_Charges, I).Value), J).value = Agl.XNull(DrPostingGroupSalesTax(0)("LedgerAc"))
                                        End If
                                    End If
                                Else
                                    If UCase(Me.Item(Col_Value_Type, I).Value).ToString.StartsWith("PERCENTAGE") Then
                                        mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).value = ""
                                    End If
                                End If
                            End If
                        End If


                        If Me.Item(Col_PostAcFromColumn, I).Value <> "" Then
                            Try

                                If mLineGrid.Item(Me.Item(Col_PostAcFromColumn, I).Value, J).Tag = "" Or mLineGrid.Item(Me.Item(Col_PostAcFromColumn, I).Value, J).Tag Is Nothing Then
                                    'mLineGrid.Item(GetColNamePostAc(Me.Item(Col_Charges, I).Value), J).value = mLineGrid.Item(Me.Item(Col_PostAcFromColumn, I).Value, J).value
                                Else
                                    mLineGrid.Item(GetColNamePostAc(Me.Item(Col_Charges, I).Value), J).value = mLineGrid.Item(Me.Item(Col_PostAcFromColumn, I).Value, J).Tag
                                End If
                            Catch ex As Exception

                            End Try
                        End If
                        If Me.Item(Col_ContraAcFromColumn, I).Value <> "" Then
                            If mLineGrid.Item(Me.Item(Col_ContraAcFromColumn, I).Value, J).Tag = "" Or mLineGrid.Item(Me.Item(Col_ContraAcFromColumn, I).Value, J).Tag Is Nothing Then
                                'mLineGrid.Item(GetColNameContraAc(Me.Item(Col_Charges, I).Value), J).value = mLineGrid.Item(Me.Item(Col_ContraAcFromColumn, I).Value, J).value
                            Else
                                mLineGrid.Item(GetColNameContraAc(Me.Item(Col_Charges, I).Value), J).value = mLineGrid.Item(Me.Item(Col_ContraAcFromColumn, I).Value, J).Tag
                            End If
                        End If

                    Next

                End If
            Next




            For I = 0 To Me.RowCount - 1

                If Me.Item(Col_BaseColumn, I).Value <> "" Then

                    bBaseColumn = mLineGrid.Columns(Me.Item(Col_BaseColumn, I).Value).Index


                    If UCase(Me.Item(Col_Value_Type, I).Value) = "ROUND_OFF" Then
                        Me.Item(Col_Amount, I).Value = ""
                        mRowIndex_RoundOff = I
                        mRowIndex_RoundOff_BaseColumn = Me.Item(Col_BaseColumn, I).Value
                    End If


                    bFirstRowHavingValueInBaseColumn = -1
                    bTotalBaseColumnAmt = 0
                    mUsedValue = 0

                    For J = 0 To mLineGrid.RowCount - 1
                        If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                            bTotalBaseColumnAmt += Val(mLineGrid.Item(bBaseColumn, J).Value)
                            If Val(mLineGrid.Item(bBaseColumn, J).Value) > 0 And bFirstRowHavingValueInBaseColumn < 0 Then
                                bFirstRowHavingValueInBaseColumn = J
                            End If
                        End If
                    Next

                    If Not IsReverse Then
                        Select Case UCase(Me.Item(Col_Value_Type, I).Value)
                            Case "FIXEDVALUE", "FIXEDVALUE CHANGEABLE"
                                If Me.Item(Col_LineItem, I).Value = 0 Then
                                    For J = mLineGrid.RowCount - 1 To 0 Step -1
                                        If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                            If J = bFirstRowHavingValueInBaseColumn Then
                                                mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) - mUsedValue, "0.00")
                                                mUsedValue = 0
                                            Else
                                                mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) * Val(mLineGrid.Item(bBaseColumn, J).Value) / bTotalBaseColumnAmt, "0.00")
                                                mUsedValue += mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value
                                            End If
                                        End If
                                    Next
                                End If

                            Case "PERCENTAGE OR AMOUNT"
                                If Me.Item(Col_LineItem, I).Value = 0 Then
                                    If Val(Me.Item(Col_Percentage, I).Value) = 0 Then
                                        For J = mLineGrid.RowCount - 1 To 0 Step -1
                                            If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                                If J = bFirstRowHavingValueInBaseColumn Then
                                                    mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) - mUsedValue, "0.00")
                                                    mUsedValue = 0
                                                Else
                                                    mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) * Val(mLineGrid.Item(bBaseColumn, J).Value) / bTotalBaseColumnAmt, "0.00")
                                                    mUsedValue += mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value
                                                End If
                                            End If
                                        Next
                                    End If
                                End If

                        End Select
                    End If
                End If

                Select Case UCase(Me.Item(Col_Value_Type, I).Value)
                    Case "FIXEDVALUE FROM COLUMN"
                        For J = 0 To mLineGrid.RowCount - 1
                            If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).VALUE = mLineGrid.Item(Me.Item(Col_Value, I).Value, J).VALUE
                            End If
                        Next
                    Case "PERCENTAGE FROM COLUMN"
                        For J = 0 To mLineGrid.RowCount - 1
                            If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = mLineGrid.Item(Me.Item(Col_Value, I).Value, J).VALUE
                            End If
                        Next
                    Case "FIXEDVALUE CHANGEABLE", "FIXEDVALUE"
                        'If Val(Me.Item(Col_Amount, I).Value) = 0 And Val(Me.Item(Col_Value, I).Value) > 0 Then
                        '    Me.Item(Col_Amount, I).Value = Val(Me.Item(Col_Value, I).Value)
                        'End If
                    Case "PERCENTAGE", "PERCENTAGE CHANGEABLE"
                        If Me.Item(Col_LineItem, I).Value <> 1 Then
                            For J = 0 To mLineGrid.RowCount - 1
                                If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                    mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = Val(Me.Item(Col_Percentage, I).Value)
                                End If
                            Next
                        End If
                    Case "PERCENTAGE OR AMOUNT"
                        If Me.Item(Col_LineItem, I).Value <> 1 Then
                            For J = 0 To mLineGrid.RowCount - 1
                                If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                    If Val(Me.Item(Col_Percentage, I).Value) > 0 Then
                                        mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = Val(Me.Item(Col_Percentage, I).Value)
                                    Else
                                        mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = 0
                                    End If
                                End If
                            Next
                        End If

                End Select
            Next



            Dim mColIndex As Integer = 0
            For I = 0 To Me.RowCount - 1
                StrOrgFormula = Trim(Me.Item(Col_Calculation, I).Value)
                StrConvFormula = ""
                If Not StrOrgFormula = "" Then
                    For iL = 0 To mLineGrid.RowCount - 1
                        If UCase(Me.Item(Col_Value_Type, I).Value) = "PERCENTAGE OR AMOUNT" Then
                            If Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), iL).Value) = 0 Then
                                StrOrgFormula = ""
                            Else
                                StrOrgFormula = Trim(Me.Item(Col_Calculation, I).Value)
                            End If
                        ElseIf UCase(Me.Item(Col_Value_Type, I).Value) = "ROUND_OFF" Then
                            StrOrgFormula = ""
                            mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), iL).Value = 0
                        End If

                        If Not StrOrgFormula = "" Then
                            For J = 1 To Len(Trim(Me.Item(Col_Calculation, I).Value))
                                StrTemp = UCase(Mid(StrOrgFormula, J, 1))
                                If BlnFlag Then
                                    If StrTemp = "}" Then
                                        BlnFlag = False
                                        For K = 0 To Me.RowCount - 1
                                            If Agl.StrCmp(Me.Item(Col_ChargesManualCode, K).Value, Replace(StrCode, "@", "")) Then
                                                mColIndex = K
                                                Exit For
                                            End If
                                        Next
                                        'mColIndex = Val(StrCode) - 1
                                        If I = mColIndex Then
                                            Select Case UCase(Me.Item(Col_Value_Type, I).Value)
                                                Case "PERCENTAGE", "PERCENTAGE CHANGEABLE", "PERCENTAGE FROM COLUMN", "PERCENTAGE OR AMOUNT"
                                                    If Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.00") < 0 Then
                                                        StrConvFormula = StrConvFormula + "(" + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000") + ")"
                                                    Else
                                                        StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
                                                    End If
                                                Case Else
                                                    If InStr(StrTemp, "@") > 0 Then
                                                        StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
                                                    Else
                                                        StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
                                                    End If
                                            End Select
                                        Else
                                            If Format(Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.00") < 0 Then
                                                If InStr(StrCode, "@") > 0 Then
                                                    StrConvFormula = StrConvFormula + "(" + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000") + ")"
                                                Else
                                                    StrConvFormula = StrConvFormula + "(" + Format(Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000") + ")"
                                                End If
                                            Else
                                                If InStr(StrCode, "@") > 0 Then
                                                    StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
                                                Else
                                                    StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
                                                End If
                                            End If
                                        End If
                                        StrCode = ""
                                    Else
                                        StrCode = StrCode + StrTemp
                                    End If
                                Else
                                    If StrTemp = "{" Then
                                        StrCode = ""
                                        BlnFlag = True
                                    Else
                                        StrConvFormula = StrConvFormula + StrTemp
                                    End If
                                End If

                            Next
                        End If
                        If StrConvFormula <> "" Then
                            'mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), iL).Value = Format(Agl.FillData(Agl.Chk_Qry("Select " & FilterQuery(StrConvFormula, iL)), Agl.GCn).Tables(0).Rows(0)(0), "0.".PadRight(mAgCalcRounding + 2, "0"))
                            mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), iL).Value = Format(New DataTable().Compute(FilterQuery(StrConvFormula, iL), Nothing), "0.".PadRight(mAgCalcRounding + 2, "0"))
                            StrConvFormula = ""
                        End If
                    Next

                End If
            Next





            If mRowIndex_RoundOff > 0 Then
                mColIndex = 0
                For I = 0 To Me.RowCount - 1
                    If I = mRowIndex_RoundOff Then
                        StrOrgFormula = Trim(Me.Item(Col_Calculation, I).Value)
                        StrConvFormula = ""
                        If Not StrOrgFormula = "" Then
                            For J = 1 To Len(Trim(Me.Item(Col_Calculation, I).Value))
                                StrTemp = UCase(Mid(StrOrgFormula, J, 1))
                                If BlnFlag Then
                                    If StrTemp = "}" Then
                                        BlnFlag = False
                                        For K = 0 To Me.RowCount - 1
                                            If Agl.StrCmp(Me.Item(Col_ChargesManualCode, K).Value, Replace(StrCode, "@", "")) Then
                                                mColIndex = K
                                                Exit For
                                            End If
                                        Next

                                        StrConvFormula = StrConvFormula + CStr(Val(Me.Item(Col_Amount, mColIndex).Value))
                                        StrCode = ""
                                    Else
                                        StrCode = StrCode + StrTemp
                                    End If
                                Else
                                    If StrTemp = "{" Then
                                        StrCode = ""
                                        BlnFlag = True
                                    Else
                                        StrConvFormula = StrConvFormula + StrTemp
                                    End If
                                End If
                            Next

                            If StrConvFormula <> "" Then
                                'Me.Item(Col_Amount, I).Value = Agl.FillData(Agl.Chk_Qry("Select " & StrConvFormula), Agl.GCn).Tables(0).Rows(0)(0)
                                Me.Item(Col_Amount, I).Value = New DataTable().Compute(StrConvFormula.Replace("ROUND", "Convert").Replace(",0", ",'System.Int32'"), Nothing)
                                StrConvFormula = ""
                            End If

                        End If
                    End If
                Next
            End If





            For I = 0 To Me.RowCount - 1
                If Me.Item(Col_BaseColumn, I).Value <> "" Then
                    bBaseColumn = mLineGrid.Columns(Me.Item(Col_BaseColumn, I).Value).Index

                    bFirstRowHavingValueInBaseColumn = -1
                    bTotalBaseColumnAmt = 0
                    mUsedValue = 0
                    For J = 0 To mLineGrid.RowCount - 1
                        If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                            bTotalBaseColumnAmt += Val(mLineGrid.Item(bBaseColumn, J).Value)
                            If Val(mLineGrid.Item(bBaseColumn, J).Value) > 0 And bFirstRowHavingValueInBaseColumn < 0 Then
                                bFirstRowHavingValueInBaseColumn = J
                            End If
                        End If
                    Next


                    If Not IsReverse Then
                        Select Case UCase(Me.Item(Col_Value_Type, I).Value)
                            Case "FIXEDVALUE", "FIXEDVALUE CHANGEABLE", "ROUND_OFF"
                                If Me.Item(Col_LineItem, I).Value = 0 Then
                                    For J = mLineGrid.RowCount - 1 To 0 Step -1
                                        If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                            If J = bFirstRowHavingValueInBaseColumn Then
                                                mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) - mUsedValue, "0.00")
                                                mUsedValue = 0
                                            Else
                                                mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) * Val(mLineGrid.Item(bBaseColumn, J).Value) / bTotalBaseColumnAmt, "0.00")
                                                mUsedValue += mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value
                                            End If
                                        End If
                                    Next
                                End If

                            Case "PERCENTAGE OR AMOUNT"
                                If Me.Item(Col_LineItem, I).Value = 0 Then
                                    If Val(Me.Item(Col_Amount, I).Value) > 0 And Val(Me.Item(Col_Percentage, I).Value) <= 0 Then
                                        For J = mLineGrid.RowCount - 1 To 0 Step -1
                                            If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                                If J = bFirstRowHavingValueInBaseColumn Then
                                                    mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) - mUsedValue, "0.00")
                                                    mUsedValue = 0
                                                Else
                                                    mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) * Val(mLineGrid.Item(bBaseColumn, J).Value) / bTotalBaseColumnAmt, "0.00")
                                                    mUsedValue += mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value
                                                End If
                                            End If
                                        Next
                                    Else
                                        For J = 0 To mLineGrid.RowCount - 1
                                            If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                                mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = Val(Me.Item(Col_Percentage, I).Value)
                                            End If
                                        Next
                                    End If
                                End If

                            Case "FIXEDVALUE FROM COLUMN"
                                For J = 0 To mLineGrid.RowCount - 1
                                    If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                        mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).VALUE = mLineGrid.Item(Me.Item(Col_Value, I).Value, J).VALUE
                                    End If
                                Next
                            Case "PERCENTAGE FROM COLUMN"
                                For J = 0 To mLineGrid.RowCount - 1
                                    If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                        mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = mLineGrid.Item(Me.Item(Col_Value, I).Value, J).VALUE
                                    End If
                                Next
                            Case "PERCENTAGE", "PERCENTAGE CHANGEABLE"
                                If Me.Item(Col_LineItem, I).Value <> 1 Then
                                    For J = 0 To mLineGrid.RowCount - 1
                                        If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                            mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = Val(Me.Item(Col_Percentage, I).Value)
                                        End If
                                    Next
                                End If
                        End Select
                    End If
                End If
            Next



            If mRowIndex_RoundOff > 0 Then
                mColIndex = 0
                iL = 0
                For I = 0 To Me.RowCount - 1
                    If Me.Item(Col_Charges, I).Value.ToString.ToUpper = mRowIndex_RoundOff_BaseColumn.ToUpper Then
                        StrOrgFormula = Trim(Me.Item(Col_Calculation, I).Value)
                        StrConvFormula = ""
                        If Not StrOrgFormula = "" Then
                            For iL = 0 To mLineGrid.RowCount - 1
                                If UCase(Me.Item(Col_Value_Type, I).Value) = "PERCENTAGE OR AMOUNT" Then
                                    If Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), iL).Value) = 0 Then
                                        StrOrgFormula = ""
                                    Else
                                        StrOrgFormula = Trim(Me.Item(Col_Calculation, I).Value)
                                    End If
                                ElseIf UCase(Me.Item(Col_Value_Type, I).Value) = "ROUND_OFF" Then
                                    StrOrgFormula = ""
                                    mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), iL).Value = 0
                                End If

                                If Not StrOrgFormula = "" Then
                                    For J = 1 To Len(Trim(Me.Item(Col_Calculation, I).Value))
                                        StrTemp = UCase(Mid(StrOrgFormula, J, 1))
                                        If BlnFlag Then
                                            If StrTemp = "}" Then
                                                BlnFlag = False
                                                For K = 0 To Me.RowCount - 1
                                                    If Agl.StrCmp(Me.Item(Col_ChargesManualCode, K).Value, Replace(StrCode, "@", "")) Then
                                                        mColIndex = K
                                                        Exit For
                                                    End If
                                                Next
                                                'mColIndex = Val(StrCode) - 1
                                                If I = mColIndex Then
                                                    Select Case UCase(Me.Item(Col_Value_Type, I).Value)
                                                        Case "PERCENTAGE", "PERCENTAGE CHANGEABLE", "PERCENTAGE FROM COLUMN", "PERCENTAGE OR AMOUNT"
                                                            If Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.00") < 0 Then
                                                                StrConvFormula = StrConvFormula + "(" + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000") + ")"
                                                            Else
                                                                StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
                                                            End If
                                                        Case Else
                                                            If InStr(StrTemp, "@") > 0 Then
                                                                StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
                                                            Else
                                                                StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
                                                            End If
                                                    End Select
                                                Else
                                                    If Format(Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.00") < 0 Then
                                                        StrConvFormula = StrConvFormula + "(" + Format(Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000") + ")"
                                                    Else
                                                        If InStr(StrCode, "@") > 0 Then
                                                            StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
                                                        Else
                                                            StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
                                                        End If
                                                    End If
                                                End If
                                                StrCode = ""
                                            Else
                                                StrCode = StrCode + StrTemp
                                            End If
                                        Else
                                            If StrTemp = "{" Then
                                                StrCode = ""
                                                BlnFlag = True
                                            Else
                                                StrConvFormula = StrConvFormula + StrTemp
                                            End If
                                        End If

                                    Next
                                End If
                                If StrConvFormula <> "" Then
                                    'mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), iL).Value = Format(Agl.FillData(Agl.Chk_Qry("Select " & FilterQuery(StrConvFormula, iL)), Agl.GCn).Tables(0).Rows(0)(0), "0.".PadRight(mAgCalcRounding + 2, "0"))
                                    mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), iL).Value = Format(New DataTable().Compute(FilterQuery(StrConvFormula, iL), Nothing), "0.".PadRight(mAgCalcRounding + 2, "0"))
                                    StrConvFormula = ""
                                End If
                            Next

                        End If
                    End If
                Next
            End If
















            For I = 0 To mLineGrid.Rows.Count - 1
                mCost = 0
                For J = 0 To Me.RowCount - 1
                    If Agl.StrCmp(Me.Item(Col_Charge_Type, J).Value, "Cost") Then
                        mLineGrid.Item(GetColName(Me.Item(Col_Charges, J).Value), I).value = Format(mCost, "0.00")
                        Exit For
                    End If
                    If Me.Item(Col_AffectCost, J).Value = "1" Then
                        mCost += Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, J).Value), I).value)
                    ElseIf Me.Item(Col_AffectCost, J).Value = "0" Then
                        mCost -= Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, J).Value), I).value)
                    End If
                Next
            Next


            For I = 0 To Me.RowCount - 1
                mTotalTempAmount = 0
                Select Case UCase(Me.Item(Col_Value_Type, I).Value)
                    Case Else
                        For J = 0 To mLineGrid.RowCount - 1
                            If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                mTotalTempAmount += Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value)
                            End If
                        Next
                        Me.Item(Col_Amount, I).Value = Format(mTotalTempAmount, "0.00")

                End Select
            Next










        Else     'if line grid not specified then only footer grid calculation will be called



            If mSalesTaxGroupItem <> "" Then
                DrPostingGroupSalesTax = DtPostingGroupSalesTax.Select("PostingGroupSalesTaxItem='" & mSalesTaxGroupItem & "' And PostingGroupSalesTaxParty='" & mSalesTaxGroupParty & "' ", "WEF Desc")
            End If


            For I = 0 To Me.Rows.Count - 1
                Select Case UCase(Me.Item(Col_Charge_Type, I).Value)
                    Case "VAT"
                        If mSalesTaxGroupItem <> "" And mSalesTaxGroupParty <> "" Then
                            If DrPostingGroupSalesTax.Length > 0 Then
                                Me.Item(Col_Percentage, I).Value = DrPostingGroupSalesTax(0)("VAT")
                            Else
                                MsgBox("SalesTax posting groups not defined for VAT in selected branch and division.")
                            End If
                        Else
                            Me.Item(Col_Percentage, I).Value = ""
                        End If
                    Case "CST"
                        If mSalesTaxGroupItem <> "" And mSalesTaxGroupParty <> "" Then
                            If DrPostingGroupSalesTax.Length > 0 Then
                                Me.Item(Col_Percentage, I).Value = DrPostingGroupSalesTax(0)("CST")
                            Else
                                MsgBox("SalesTax posting groups not defined for CST in selected branch and division.")
                            End If
                        Else
                            Me.Item(Col_Percentage, I).Value = ""
                        End If

                    Case "SALESTAX"
                        If mSalesTaxGroupItem <> "" And mSalesTaxGroupParty <> "" Then
                            If DrPostingGroupSalesTax.Length > 0 Then
                                Me.Item(Col_Percentage, I).Value = DrPostingGroupSalesTax(0)("SalesTax")
                            Else
                                MsgBox("SalesTax posting groups not defined for SalesTax in selected branch and division.")
                            End If
                        Else
                            Me.Item(Col_Percentage, I).Value = ""
                        End If

                    Case "SALESADDITIONALTAX"
                        If mSalesTaxGroupItem <> "" And mSalesTaxGroupParty <> "" Then
                            If DrPostingGroupSalesTax.Length > 0 Then
                                Me.Item(Col_Percentage, I).Value = DrPostingGroupSalesTax(0)("AdditionalTax")
                            Else
                                MsgBox("SalesTax posting groups not defined for Sales Additional Tax in selected branch and division.")
                            End If
                        Else
                            Me.Item(Col_Percentage, I).Value = ""
                        End If

                    Case "SAT"
                        If mSalesTaxGroupItem <> "" And mSalesTaxGroupParty <> "" Then
                            If DrPostingGroupSalesTax.Length > 0 Then
                                Me.Item(Col_Percentage, I).Value = DrPostingGroupSalesTax(0)("AdditionalTax")
                            Else
                                MsgBox("SalesTax posting groups not defined for SAT in selected branch and division.")
                            End If
                        Else
                            Me.Item(Col_Percentage, I).Value = ""
                        End If
                End Select
            Next I



            Dim mColIndex As Integer = 0
            For I = 0 To Me.RowCount - 1
                StrOrgFormula = Trim(Me.Item(Col_Calculation, I).Value)
                If UCase(Me.Item(Col_Value_Type, I).Value) = "PERCENTAGE OR AMOUNT" Then
                    If Val((Me.Item(Col_Percentage, I).Value)) <= 0 Then
                        StrOrgFormula = ""
                    End If
                End If

                StrConvFormula = ""
                If Not StrOrgFormula = "" Then
                    For J = 1 To Len(Trim(Me.Item(Col_Calculation, I).Value))
                        StrTemp = UCase(Mid(StrOrgFormula, J, 1))
                        If BlnFlag Then
                            If StrTemp = "}" Then
                                BlnFlag = False
                                For K = 0 To Me.RowCount - 1
                                    If Agl.StrCmp(Me.Item(Col_ChargesManualCode, K).Value, Replace(StrCode, "@", "")) Then
                                        mColIndex = K
                                        Exit For
                                    End If
                                Next

                                If I = mColIndex Then
                                    Select Case UCase(Me.Item(Col_Value_Type, I).Value)
                                        Case "PERCENTAGE", "PERCENTAGE CHANGEABLE", "PERCENTAGE FROM COLUMN"
                                            'StrConvFormula = StrConvFormula + Format(Val(Me.Item(Col_Percentage, mColIndex).Value), "0.00")
                                            StrConvFormula = StrConvFormula + CStr(Val(Me.Item(Col_Percentage, mColIndex).Value))
                                        Case Else
                                            If InStr(StrTemp, "@") > 0 Then
                                                StrConvFormula = StrConvFormula + CStr(Val(Me.Item(Col_Percentage, mColIndex).Value))
                                            Else
                                                StrConvFormula = StrConvFormula + CStr(Val(Me.Item(Col_Amount, mColIndex).Value))
                                            End If

                                    End Select
                                Else
                                    StrConvFormula = StrConvFormula + CStr(Val(Me.Item(Col_Amount, mColIndex).Value))
                                End If
                                StrCode = ""
                            Else
                                StrCode = StrCode + StrTemp
                            End If
                        Else
                            If StrTemp = "{" Then
                                StrCode = ""
                                BlnFlag = True
                            Else
                                StrConvFormula = StrConvFormula + StrTemp
                            End If
                        End If
                    Next

                    If StrConvFormula <> "" Then
                        'Me.Item(Col_Amount, I).Value = Agl.FillData(Agl.Chk_Qry("Select " & FilterQuery(StrConvFormula, iL)), Agl.GCn).Tables(0).Rows(0)(0)
                        Me.Item(Col_Amount, I).Value = New DataTable().Compute(FilterQuery(StrConvFormula, iL), Nothing)
                        StrConvFormula = ""
                    End If
                End If
            Next




            'For I = 0 To Me.RowCount - 1
            '    If Me.Item(Col_BaseColumn, I).Value <> "" Then
            '        bBaseColumn = mLineGrid.Columns(Me.Item(Col_BaseColumn, I).Value).Index

            '        bFirstRowHavingValueInBaseColumn = -1
            '        bTotalBaseColumnAmt = 0
            '        mUsedValue = 0
            '        For J = 0 To mLineGrid.RowCount - 1
            '            If mLineGrid.Item(mColumnMandatory, J).Value <> ""  And mLineGrid.Rows(J).Visible = True Then
            '                bTotalBaseColumnAmt += Val(mLineGrid.Item(bBaseColumn, J).Value)
            '                If Val(mLineGrid.Item(bBaseColumn, J).Value) > 0 And bFirstRowHavingValueInBaseColumn < 0 Then
            '                    bFirstRowHavingValueInBaseColumn = J
            '                End If
            '            End If
            '        Next



            '        Select Case UCase(Me.Item(Col_Value_Type, I).Value)
            '            Case "FIXEDVALUE", "FIXEDVALUE CHANGEABLE"
            '                If Me.Item(Col_LineItem, I).Value = 0 Then
            '                    For J = mLineGrid.RowCount - 1 To 0 Step -1
            '                        If mLineGrid.Item(mColumnMandatory, J).Value <> ""  And mLineGrid.Rows(J).Visible = True Then
            '                            If J = bFirstRowHavingValueInBaseColumn Then
            '                                mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) - mUsedValue, "0.00")
            '                                mUsedValue = 0
            '                            Else
            '                                mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) * Val(mLineGrid.Item(bBaseColumn, J).Value) / bTotalBaseColumnAmt, "0.00")
            '                                mUsedValue += mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value
            '                            End If
            '                        End If
            '                    Next
            '                End If
            '            Case "FIXEDVALUE FROM COLUMN"
            '                For J = 0 To mLineGrid.RowCount - 1
            '                    If mLineGrid.Item(mColumnMandatory, J).Value <> ""  And mLineGrid.Rows(J).Visible = True Then
            '                        mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).VALUE = mLineGrid.Item(Me.Item(Col_Value, I).Value, J).VALUE
            '                    End If
            '                Next
            '            Case "PERCENTAGE FROM COLUMN"
            '                For J = 0 To mLineGrid.RowCount - 1
            '                    If mLineGrid.Item(mColumnMandatory, J).Value <> ""  And mLineGrid.Rows(J).Visible = True Then
            '                        mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = mLineGrid.Item(Me.Item(Col_Value, I).Value, J).VALUE
            '                    End If
            '                Next
            '            Case "PERCENTAGE", "PERCENTAGE CHANGEABLE"
            '                If Me.Item(Col_LineItem, I).Value <> 1 Then
            '                    For J = 0 To mLineGrid.RowCount - 1
            '                        If mLineGrid.Item(mColumnMandatory, J).Value <> ""  And mLineGrid.Rows(J).Visible = True Then
            '                            mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = Val(Me.Item(Col_Percentage, I).Value)
            '                        End If
            '                    Next
            '                End If
            '        End Select
            '    End If
            'Next




            mCost = 0
            For J = 0 To Me.RowCount - 1
                If Agl.StrCmp(Me.Item(Col_Charge_Type, J).Value, "Cost") Then
                    Me.Item(Col_Amount, J).Value = Format(mCost, "0.00")
                    Exit For
                End If
                If Me.Item(Col_AffectCost, J).Value = "1" Then
                    mCost += Val(Me.Item(Col_Amount, J).Value)
                ElseIf Me.Item(Col_AffectCost, J).Value = "0" Then
                    mCost -= Val(Me.Item(Col_Amount, J).Value)
                End If
            Next
        End If

        RaiseEvent Calculated()

    End Sub


    Public Sub CalculationRow(mRow As Integer, Optional ByVal IsReverse As Boolean = False)
        Dim I As Integer
        Dim iL As Integer
        Dim J As Integer
        Dim K As Integer
        Dim StrConvFormula$ = ""
        Dim StrOrgFormula$, StrTemp$
        Dim BlnFlag As Boolean
        Dim StrCode$ = ""
        Dim mTotalGrossAmount As Double
        Dim mTotalTempAmount As Double
        Dim mUsedValue As Double
        Dim mCost As Double
        Dim bBaseColumn As Integer
        Dim bTotalBaseColumnAmt As Integer
        Dim bFirstRowHavingValueInBaseColumn As Integer
        Dim mRowIndex_RoundOff As Integer = -1
        Dim mRowIndex_RoundOff_BaseColumn As String = ""


        If mStructure = "" Then Exit Sub
        mTotalGrossAmount = 0
        If Not mLineGrid Is Nothing Then



            For J = 0 To mLineGrid.RowCount - 1
                If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then

                    For I = 0 To Me.Rows.Count - 1
                        If UCase(Me.Item(Col_Charge_Type, I).Value) <> "CHARGES" Then
                            If mColumnSalesTaxGroupItem > -1 Then
                                DrPostingGroupSalesTax = DtPostingGroupSalesTax.Select("PostingGroupSalesTaxItem='" & mLineGrid.AgSelectedValue(mColumnSalesTaxGroupItem, J) & "' And PostingGroupSalesTaxParty='" & mSalesTaxGroupParty & "' And PlaceOfSupply = '" & mPlaceOfSupply & "' And Process = '" & mVoucherCategory & "' And ChargeType='" & UCase(Me.Item(Col_Charge_Type, I).Value) & "'  ", "WEF Desc")
                                If DrPostingGroupSalesTax.Length <= 0 Then
                                    DrPostingGroupSalesTax = DtPostingGroupSalesTax.Select("PostingGroupSalesTaxItem='" & mSalesTaxGroupItem & "' And PostingGroupSalesTaxParty='" & mSalesTaxGroupParty & "'  And PlaceOfSupply = '" & mPlaceOfSupply & "' And Process = '" & mVoucherCategory & "' And ChargeType='" & UCase(Me.Item(Col_Charge_Type, I).Value) & "'  ", "WEF Desc")
                                End If
                            ElseIf mSalesTaxGroupItem <> "" Then
                                DrPostingGroupSalesTax = DtPostingGroupSalesTax.Select("PostingGroupSalesTaxItem='" & mSalesTaxGroupItem & "' And PostingGroupSalesTaxParty='" & mSalesTaxGroupParty & "'  And PlaceOfSupply = '" & mPlaceOfSupply & "' And Process = '" & mVoucherCategory & "' And ChargeType='" & UCase(Me.Item(Col_Charge_Type, I).Value) & "'  ", "WEF Desc")
                            End If

                            If DrPostingGroupSalesTax IsNot Nothing Then
                                If DrPostingGroupSalesTax.Length > 0 Then
                                    If Me.Item(Col_LineItem, I).Value Then
                                        If Agl.VNull(DrPostingGroupSalesTax(0)("Percentage")) > 0 Then
                                            mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).value = Agl.XNull(DrPostingGroupSalesTax(0)("Percentage"))
                                        End If
                                        If Agl.XNull(DrPostingGroupSalesTax(0)("LedgerAc")) <> "" Then
                                            mLineGrid.Item(GetColNamePostAc(Me.Item(Col_Charges, I).Value), J).value = Agl.XNull(DrPostingGroupSalesTax(0)("LedgerAc"))
                                        End If
                                    End If
                                Else
                                    If UCase(Me.Item(Col_Value_Type, I).Value).ToString.StartsWith("PERCENTAGE") Then
                                        mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).value = ""
                                    End If
                                End If
                            End If
                        End If


                        If Me.Item(Col_PostAcFromColumn, I).Value <> "" Then
                            Try

                                If mLineGrid.Item(Me.Item(Col_PostAcFromColumn, I).Value, J).Tag = "" Or mLineGrid.Item(Me.Item(Col_PostAcFromColumn, I).Value, J).Tag Is Nothing Then
                                    'mLineGrid.Item(GetColNamePostAc(Me.Item(Col_Charges, I).Value), J).value = mLineGrid.Item(Me.Item(Col_PostAcFromColumn, I).Value, J).value
                                Else
                                    mLineGrid.Item(GetColNamePostAc(Me.Item(Col_Charges, I).Value), J).value = mLineGrid.Item(Me.Item(Col_PostAcFromColumn, I).Value, J).Tag
                                End If
                            Catch ex As Exception

                            End Try
                        End If
                        If Me.Item(Col_ContraAcFromColumn, I).Value <> "" Then
                            If mLineGrid.Item(Me.Item(Col_ContraAcFromColumn, I).Value, J).Tag = "" Or mLineGrid.Item(Me.Item(Col_ContraAcFromColumn, I).Value, J).Tag Is Nothing Then
                                'mLineGrid.Item(GetColNameContraAc(Me.Item(Col_Charges, I).Value), J).value = mLineGrid.Item(Me.Item(Col_ContraAcFromColumn, I).Value, J).value
                            Else
                                mLineGrid.Item(GetColNameContraAc(Me.Item(Col_Charges, I).Value), J).value = mLineGrid.Item(Me.Item(Col_ContraAcFromColumn, I).Value, J).Tag
                            End If
                        End If

                    Next

                End If
            Next




            For I = 0 To Me.RowCount - 1

                If Me.Item(Col_BaseColumn, I).Value <> "" Then

                    bBaseColumn = mLineGrid.Columns(Me.Item(Col_BaseColumn, I).Value).Index


                    If UCase(Me.Item(Col_Value_Type, I).Value) = "ROUND_OFF" Then
                        Me.Item(Col_Amount, I).Value = ""
                        mRowIndex_RoundOff = I
                        mRowIndex_RoundOff_BaseColumn = Me.Item(Col_BaseColumn, I).Value
                    End If


                    bFirstRowHavingValueInBaseColumn = -1
                    bTotalBaseColumnAmt = 0
                    mUsedValue = 0

                    For J = 0 To mLineGrid.RowCount - 1
                        If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                            bTotalBaseColumnAmt += Val(mLineGrid.Item(bBaseColumn, J).Value)
                            If Val(mLineGrid.Item(bBaseColumn, J).Value) > 0 And bFirstRowHavingValueInBaseColumn < 0 Then
                                bFirstRowHavingValueInBaseColumn = J
                            End If
                        End If
                    Next

                    If Not IsReverse Then
                        Select Case UCase(Me.Item(Col_Value_Type, I).Value)
                            Case "FIXEDVALUE", "FIXEDVALUE CHANGEABLE"
                                If Me.Item(Col_LineItem, I).Value = 0 Then
                                    For J = mLineGrid.RowCount - 1 To 0 Step -1
                                        If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                            If J = bFirstRowHavingValueInBaseColumn Then
                                                mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) - mUsedValue, "0.00")
                                                mUsedValue = 0
                                            Else
                                                mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) * Val(mLineGrid.Item(bBaseColumn, J).Value) / bTotalBaseColumnAmt, "0.00")
                                                mUsedValue += mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value
                                            End If
                                        End If
                                    Next
                                End If

                            Case "PERCENTAGE OR AMOUNT"
                                If Me.Item(Col_LineItem, I).Value = 0 Then
                                    If Val(Me.Item(Col_Percentage, I).Value) = 0 Then
                                        For J = mLineGrid.RowCount - 1 To 0 Step -1
                                            If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                                If J = bFirstRowHavingValueInBaseColumn Then
                                                    mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) - mUsedValue, "0.00")
                                                    mUsedValue = 0
                                                Else
                                                    mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) * Val(mLineGrid.Item(bBaseColumn, J).Value) / bTotalBaseColumnAmt, "0.00")
                                                    mUsedValue += mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value
                                                End If
                                            End If
                                        Next
                                    End If
                                End If

                        End Select
                    End If
                End If

                Select Case UCase(Me.Item(Col_Value_Type, I).Value)
                    Case "FIXEDVALUE FROM COLUMN"
                        For J = 0 To mLineGrid.RowCount - 1
                            If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).VALUE = mLineGrid.Item(Me.Item(Col_Value, I).Value, J).VALUE
                            End If
                        Next
                    Case "PERCENTAGE FROM COLUMN"
                        For J = 0 To mLineGrid.RowCount - 1
                            If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = mLineGrid.Item(Me.Item(Col_Value, I).Value, J).VALUE
                            End If
                        Next
                    Case "FIXEDVALUE CHANGEABLE", "FIXEDVALUE"
                        'If Val(Me.Item(Col_Amount, I).Value) = 0 And Val(Me.Item(Col_Value, I).Value) > 0 Then
                        '    Me.Item(Col_Amount, I).Value = Val(Me.Item(Col_Value, I).Value)
                        'End If
                    Case "PERCENTAGE", "PERCENTAGE CHANGEABLE"
                        If Me.Item(Col_LineItem, I).Value <> 1 Then
                            For J = 0 To mLineGrid.RowCount - 1
                                If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                    mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = Val(Me.Item(Col_Percentage, I).Value)
                                End If
                            Next
                        End If
                    Case "PERCENTAGE OR AMOUNT"
                        If Me.Item(Col_LineItem, I).Value <> 1 Then
                            For J = 0 To mLineGrid.RowCount - 1
                                If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                    If Val(Me.Item(Col_Percentage, I).Value) > 0 Then
                                        mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = Val(Me.Item(Col_Percentage, I).Value)
                                    Else
                                        mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = 0
                                    End If
                                End If
                            Next
                        End If

                End Select
            Next



            Dim mColIndex As Integer = 0
            For I = 0 To Me.RowCount - 1
                StrOrgFormula = Trim(Me.Item(Col_Calculation, I).Value)
                StrConvFormula = ""
                If Not StrOrgFormula = "" Then
                    For iL = 0 To mLineGrid.RowCount - 1
                        If UCase(Me.Item(Col_Value_Type, I).Value) = "PERCENTAGE OR AMOUNT" Then
                            If Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), iL).Value) = 0 Then
                                StrOrgFormula = ""
                            Else
                                StrOrgFormula = Trim(Me.Item(Col_Calculation, I).Value)
                            End If
                        ElseIf UCase(Me.Item(Col_Value_Type, I).Value) = "ROUND_OFF" Then
                            StrOrgFormula = ""
                            mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), iL).Value = 0
                        End If

                        If Not StrOrgFormula = "" Then
                            For J = 1 To Len(Trim(Me.Item(Col_Calculation, I).Value))
                                StrTemp = UCase(Mid(StrOrgFormula, J, 1))
                                If BlnFlag Then
                                    If StrTemp = "}" Then
                                        BlnFlag = False
                                        For K = 0 To Me.RowCount - 1
                                            If Agl.StrCmp(Me.Item(Col_ChargesManualCode, K).Value, Replace(StrCode, "@", "")) Then
                                                mColIndex = K
                                                Exit For
                                            End If
                                        Next
                                        'mColIndex = Val(StrCode) - 1
                                        If I = mColIndex Then
                                            Select Case UCase(Me.Item(Col_Value_Type, I).Value)
                                                Case "PERCENTAGE", "PERCENTAGE CHANGEABLE", "PERCENTAGE FROM COLUMN", "PERCENTAGE OR AMOUNT"
                                                    If Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.00") < 0 Then
                                                        StrConvFormula = StrConvFormula + "(" + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000") + ")"
                                                    Else
                                                        StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
                                                    End If
                                                Case Else
                                                    If InStr(StrTemp, "@") > 0 Then
                                                        StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
                                                    Else
                                                        StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
                                                    End If
                                            End Select
                                        Else
                                            If Format(Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.00") < 0 Then
                                                If InStr(StrCode, "@") > 0 Then
                                                    StrConvFormula = StrConvFormula + "(" + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000") + ")"
                                                Else
                                                    StrConvFormula = StrConvFormula + "(" + Format(Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000") + ")"
                                                End If
                                            Else
                                                If InStr(StrCode, "@") > 0 Then
                                                    StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
                                                Else
                                                    StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
                                                End If
                                            End If
                                        End If
                                        StrCode = ""
                                    Else
                                        StrCode = StrCode + StrTemp
                                    End If
                                Else
                                    If StrTemp = "{" Then
                                        StrCode = ""
                                        BlnFlag = True
                                    Else
                                        StrConvFormula = StrConvFormula + StrTemp
                                    End If
                                End If

                            Next
                        End If
                        If StrConvFormula <> "" Then
                            mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), iL).Value = Format(Agl.FillData(Agl.Chk_Qry("Select " & FilterQuery(StrConvFormula, iL)), Agl.GCn).Tables(0).Rows(0)(0), "0.".PadRight(mAgCalcRounding + 2, "0"))
                            StrConvFormula = ""
                        End If
                    Next

                End If
            Next





            If mRowIndex_RoundOff > 0 Then
                mColIndex = 0
                For I = 0 To Me.RowCount - 1
                    If I = mRowIndex_RoundOff Then
                        StrOrgFormula = Trim(Me.Item(Col_Calculation, I).Value)
                        StrConvFormula = ""
                        If Not StrOrgFormula = "" Then
                            For J = 1 To Len(Trim(Me.Item(Col_Calculation, I).Value))
                                StrTemp = UCase(Mid(StrOrgFormula, J, 1))
                                If BlnFlag Then
                                    If StrTemp = "}" Then
                                        BlnFlag = False
                                        For K = 0 To Me.RowCount - 1
                                            If Agl.StrCmp(Me.Item(Col_ChargesManualCode, K).Value, Replace(StrCode, "@", "")) Then
                                                mColIndex = K
                                                Exit For
                                            End If
                                        Next

                                        StrConvFormula = StrConvFormula + CStr(Val(Me.Item(Col_Amount, mColIndex).Value))
                                        StrCode = ""
                                    Else
                                        StrCode = StrCode + StrTemp
                                    End If
                                Else
                                    If StrTemp = "{" Then
                                        StrCode = ""
                                        BlnFlag = True
                                    Else
                                        StrConvFormula = StrConvFormula + StrTemp
                                    End If
                                End If
                            Next

                            If StrConvFormula <> "" Then
                                Me.Item(Col_Amount, I).Value = Agl.FillData(Agl.Chk_Qry("Select " & StrConvFormula), Agl.GCn).Tables(0).Rows(0)(0)
                                StrConvFormula = ""
                            End If

                        End If
                    End If
                Next
            End If





            For I = 0 To Me.RowCount - 1
                If Me.Item(Col_BaseColumn, I).Value <> "" Then
                    bBaseColumn = mLineGrid.Columns(Me.Item(Col_BaseColumn, I).Value).Index

                    bFirstRowHavingValueInBaseColumn = -1
                    bTotalBaseColumnAmt = 0
                    mUsedValue = 0
                    For J = 0 To mLineGrid.RowCount - 1
                        If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                            bTotalBaseColumnAmt += Val(mLineGrid.Item(bBaseColumn, J).Value)
                            If Val(mLineGrid.Item(bBaseColumn, J).Value) > 0 And bFirstRowHavingValueInBaseColumn < 0 Then
                                bFirstRowHavingValueInBaseColumn = J
                            End If
                        End If
                    Next


                    If Not IsReverse Then
                        Select Case UCase(Me.Item(Col_Value_Type, I).Value)
                            Case "FIXEDVALUE", "FIXEDVALUE CHANGEABLE", "ROUND_OFF"
                                If Me.Item(Col_LineItem, I).Value = 0 Then
                                    For J = mLineGrid.RowCount - 1 To 0 Step -1
                                        If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                            If J = bFirstRowHavingValueInBaseColumn Then
                                                mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) - mUsedValue, "0.00")
                                                mUsedValue = 0
                                            Else
                                                mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) * Val(mLineGrid.Item(bBaseColumn, J).Value) / bTotalBaseColumnAmt, "0.00")
                                                mUsedValue += mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value
                                            End If
                                        End If
                                    Next
                                End If

                            Case "PERCENTAGE OR AMOUNT"
                                If Me.Item(Col_LineItem, I).Value = 0 Then
                                    If Val(Me.Item(Col_Amount, I).Value) > 0 And Val(Me.Item(Col_Percentage, I).Value) <= 0 Then
                                        For J = mLineGrid.RowCount - 1 To 0 Step -1
                                            If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                                If J = bFirstRowHavingValueInBaseColumn Then
                                                    mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) - mUsedValue, "0.00")
                                                    mUsedValue = 0
                                                Else
                                                    mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) * Val(mLineGrid.Item(bBaseColumn, J).Value) / bTotalBaseColumnAmt, "0.00")
                                                    mUsedValue += mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value
                                                End If
                                            End If
                                        Next
                                    Else
                                        For J = 0 To mLineGrid.RowCount - 1
                                            If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                                mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = Val(Me.Item(Col_Percentage, I).Value)
                                            End If
                                        Next
                                    End If
                                End If

                            Case "FIXEDVALUE FROM COLUMN"
                                For J = 0 To mLineGrid.RowCount - 1
                                    If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                        mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).VALUE = mLineGrid.Item(Me.Item(Col_Value, I).Value, J).VALUE
                                    End If
                                Next
                            Case "PERCENTAGE FROM COLUMN"
                                For J = 0 To mLineGrid.RowCount - 1
                                    If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                        mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = mLineGrid.Item(Me.Item(Col_Value, I).Value, J).VALUE
                                    End If
                                Next
                            Case "PERCENTAGE", "PERCENTAGE CHANGEABLE"
                                If Me.Item(Col_LineItem, I).Value <> 1 Then
                                    For J = 0 To mLineGrid.RowCount - 1
                                        If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                            mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = Val(Me.Item(Col_Percentage, I).Value)
                                        End If
                                    Next
                                End If
                        End Select
                    End If
                End If
            Next



            If mRowIndex_RoundOff > 0 Then
                mColIndex = 0
                iL = 0
                For I = 0 To Me.RowCount - 1
                    If Me.Item(Col_Charges, I).Value.ToString.ToUpper = mRowIndex_RoundOff_BaseColumn.ToUpper Then
                        StrOrgFormula = Trim(Me.Item(Col_Calculation, I).Value)
                        StrConvFormula = ""
                        If Not StrOrgFormula = "" Then
                            For iL = 0 To mLineGrid.RowCount - 1
                                If UCase(Me.Item(Col_Value_Type, I).Value) = "PERCENTAGE OR AMOUNT" Then
                                    If Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), iL).Value) = 0 Then
                                        StrOrgFormula = ""
                                    Else
                                        StrOrgFormula = Trim(Me.Item(Col_Calculation, I).Value)
                                    End If
                                ElseIf UCase(Me.Item(Col_Value_Type, I).Value) = "ROUND_OFF" Then
                                    StrOrgFormula = ""
                                    mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), iL).Value = 0
                                End If

                                If Not StrOrgFormula = "" Then
                                    For J = 1 To Len(Trim(Me.Item(Col_Calculation, I).Value))
                                        StrTemp = UCase(Mid(StrOrgFormula, J, 1))
                                        If BlnFlag Then
                                            If StrTemp = "}" Then
                                                BlnFlag = False
                                                For K = 0 To Me.RowCount - 1
                                                    If Agl.StrCmp(Me.Item(Col_ChargesManualCode, K).Value, Replace(StrCode, "@", "")) Then
                                                        mColIndex = K
                                                        Exit For
                                                    End If
                                                Next
                                                'mColIndex = Val(StrCode) - 1
                                                If I = mColIndex Then
                                                    Select Case UCase(Me.Item(Col_Value_Type, I).Value)
                                                        Case "PERCENTAGE", "PERCENTAGE CHANGEABLE", "PERCENTAGE FROM COLUMN", "PERCENTAGE OR AMOUNT"
                                                            If Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.00") < 0 Then
                                                                StrConvFormula = StrConvFormula + "(" + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000") + ")"
                                                            Else
                                                                StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
                                                            End If
                                                        Case Else
                                                            If InStr(StrTemp, "@") > 0 Then
                                                                StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
                                                            Else
                                                                StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
                                                            End If
                                                    End Select
                                                Else
                                                    If Format(Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.00") < 0 Then
                                                        StrConvFormula = StrConvFormula + "(" + Format(Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000") + ")"
                                                    Else
                                                        If InStr(StrCode, "@") > 0 Then
                                                            StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
                                                        Else
                                                            StrConvFormula = StrConvFormula + Format(Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, mColIndex).Value), iL).Value), "0.000000")
                                                        End If
                                                    End If
                                                End If
                                                StrCode = ""
                                            Else
                                                StrCode = StrCode + StrTemp
                                            End If
                                        Else
                                            If StrTemp = "{" Then
                                                StrCode = ""
                                                BlnFlag = True
                                            Else
                                                StrConvFormula = StrConvFormula + StrTemp
                                            End If
                                        End If

                                    Next
                                End If
                                If StrConvFormula <> "" Then
                                    mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), iL).Value = Format(Agl.FillData(Agl.Chk_Qry("Select " & FilterQuery(StrConvFormula, iL)), Agl.GCn).Tables(0).Rows(0)(0), "0.".PadRight(mAgCalcRounding + 2, "0"))
                                    StrConvFormula = ""
                                End If
                            Next

                        End If
                    End If
                Next
            End If
















            For I = 0 To mLineGrid.Rows.Count - 1
                mCost = 0
                For J = 0 To Me.RowCount - 1
                    If Agl.StrCmp(Me.Item(Col_Charge_Type, J).Value, "Cost") Then
                        mLineGrid.Item(GetColName(Me.Item(Col_Charges, J).Value), I).value = Format(mCost, "0.00")
                        Exit For
                    End If
                    If Me.Item(Col_AffectCost, J).Value = "1" Then
                        mCost += Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, J).Value), I).value)
                    ElseIf Me.Item(Col_AffectCost, J).Value = "0" Then
                        mCost -= Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, J).Value), I).value)
                    End If
                Next
            Next


            For I = 0 To Me.RowCount - 1
                mTotalTempAmount = 0
                Select Case UCase(Me.Item(Col_Value_Type, I).Value)
                    Case Else
                        For J = 0 To mLineGrid.RowCount - 1
                            If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                                mTotalTempAmount += Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value)
                            End If
                        Next
                        Me.Item(Col_Amount, I).Value = Format(mTotalTempAmount, "0.00")

                End Select
            Next










        Else     'if line grid not specified then only footer grid calculation will be called



            If mSalesTaxGroupItem <> "" Then
                DrPostingGroupSalesTax = DtPostingGroupSalesTax.Select("PostingGroupSalesTaxItem='" & mSalesTaxGroupItem & "' And PostingGroupSalesTaxParty='" & mSalesTaxGroupParty & "' ", "WEF Desc")
            End If


            For I = 0 To Me.Rows.Count - 1
                Select Case UCase(Me.Item(Col_Charge_Type, I).Value)
                    Case "VAT"
                        If mSalesTaxGroupItem <> "" And mSalesTaxGroupParty <> "" Then
                            If DrPostingGroupSalesTax.Length > 0 Then
                                Me.Item(Col_Percentage, I).Value = DrPostingGroupSalesTax(0)("VAT")
                            Else
                                MsgBox("SalesTax posting groups not defined for VAT in selected branch and division.")
                            End If
                        Else
                            Me.Item(Col_Percentage, I).Value = ""
                        End If
                    Case "CST"
                        If mSalesTaxGroupItem <> "" And mSalesTaxGroupParty <> "" Then
                            If DrPostingGroupSalesTax.Length > 0 Then
                                Me.Item(Col_Percentage, I).Value = DrPostingGroupSalesTax(0)("CST")
                            Else
                                MsgBox("SalesTax posting groups not defined for CST in selected branch and division.")
                            End If
                        Else
                            Me.Item(Col_Percentage, I).Value = ""
                        End If

                    Case "SALESTAX"
                        If mSalesTaxGroupItem <> "" And mSalesTaxGroupParty <> "" Then
                            If DrPostingGroupSalesTax.Length > 0 Then
                                Me.Item(Col_Percentage, I).Value = DrPostingGroupSalesTax(0)("SalesTax")
                            Else
                                MsgBox("SalesTax posting groups not defined for SalesTax in selected branch and division.")
                            End If
                        Else
                            Me.Item(Col_Percentage, I).Value = ""
                        End If

                    Case "SALESADDITIONALTAX"
                        If mSalesTaxGroupItem <> "" And mSalesTaxGroupParty <> "" Then
                            If DrPostingGroupSalesTax.Length > 0 Then
                                Me.Item(Col_Percentage, I).Value = DrPostingGroupSalesTax(0)("AdditionalTax")
                            Else
                                MsgBox("SalesTax posting groups not defined for Sales Additional Tax in selected branch and division.")
                            End If
                        Else
                            Me.Item(Col_Percentage, I).Value = ""
                        End If

                    Case "SAT"
                        If mSalesTaxGroupItem <> "" And mSalesTaxGroupParty <> "" Then
                            If DrPostingGroupSalesTax.Length > 0 Then
                                Me.Item(Col_Percentage, I).Value = DrPostingGroupSalesTax(0)("AdditionalTax")
                            Else
                                MsgBox("SalesTax posting groups not defined for SAT in selected branch and division.")
                            End If
                        Else
                            Me.Item(Col_Percentage, I).Value = ""
                        End If
                End Select
            Next I



            Dim mColIndex As Integer = 0
            For I = 0 To Me.RowCount - 1
                StrOrgFormula = Trim(Me.Item(Col_Calculation, I).Value)
                If UCase(Me.Item(Col_Value_Type, I).Value) = "PERCENTAGE OR AMOUNT" Then
                    If Val((Me.Item(Col_Percentage, I).Value)) <= 0 Then
                        StrOrgFormula = ""
                    End If
                End If

                StrConvFormula = ""
                If Not StrOrgFormula = "" Then
                    For J = 1 To Len(Trim(Me.Item(Col_Calculation, I).Value))
                        StrTemp = UCase(Mid(StrOrgFormula, J, 1))
                        If BlnFlag Then
                            If StrTemp = "}" Then
                                BlnFlag = False
                                For K = 0 To Me.RowCount - 1
                                    If Agl.StrCmp(Me.Item(Col_ChargesManualCode, K).Value, Replace(StrCode, "@", "")) Then
                                        mColIndex = K
                                        Exit For
                                    End If
                                Next

                                If I = mColIndex Then
                                    Select Case UCase(Me.Item(Col_Value_Type, I).Value)
                                        Case "PERCENTAGE", "PERCENTAGE CHANGEABLE", "PERCENTAGE FROM COLUMN"
                                            'StrConvFormula = StrConvFormula + Format(Val(Me.Item(Col_Percentage, mColIndex).Value), "0.00")
                                            StrConvFormula = StrConvFormula + CStr(Val(Me.Item(Col_Percentage, mColIndex).Value))
                                        Case Else
                                            If InStr(StrTemp, "@") > 0 Then
                                                StrConvFormula = StrConvFormula + CStr(Val(Me.Item(Col_Percentage, mColIndex).Value))
                                            Else
                                                StrConvFormula = StrConvFormula + CStr(Val(Me.Item(Col_Amount, mColIndex).Value))
                                            End If

                                    End Select
                                Else
                                    StrConvFormula = StrConvFormula + CStr(Val(Me.Item(Col_Amount, mColIndex).Value))
                                End If
                                StrCode = ""
                            Else
                                StrCode = StrCode + StrTemp
                            End If
                        Else
                            If StrTemp = "{" Then
                                StrCode = ""
                                BlnFlag = True
                            Else
                                StrConvFormula = StrConvFormula + StrTemp
                            End If
                        End If

                    Next

                    If StrConvFormula <> "" Then
                        Me.Item(Col_Amount, I).Value = Agl.FillData(Agl.Chk_Qry("Select " & FilterQuery(StrConvFormula, iL)), Agl.GCn).Tables(0).Rows(0)(0)
                        StrConvFormula = ""
                    End If

                End If
            Next




            'For I = 0 To Me.RowCount - 1
            '    If Me.Item(Col_BaseColumn, I).Value <> "" Then
            '        bBaseColumn = mLineGrid.Columns(Me.Item(Col_BaseColumn, I).Value).Index

            '        bFirstRowHavingValueInBaseColumn = -1
            '        bTotalBaseColumnAmt = 0
            '        mUsedValue = 0
            '        For J = 0 To mLineGrid.RowCount - 1
            '            If mLineGrid.Item(mColumnMandatory, J).Value <> ""  And mLineGrid.Rows(J).Visible = True Then
            '                bTotalBaseColumnAmt += Val(mLineGrid.Item(bBaseColumn, J).Value)
            '                If Val(mLineGrid.Item(bBaseColumn, J).Value) > 0 And bFirstRowHavingValueInBaseColumn < 0 Then
            '                    bFirstRowHavingValueInBaseColumn = J
            '                End If
            '            End If
            '        Next



            '        Select Case UCase(Me.Item(Col_Value_Type, I).Value)
            '            Case "FIXEDVALUE", "FIXEDVALUE CHANGEABLE"
            '                If Me.Item(Col_LineItem, I).Value = 0 Then
            '                    For J = mLineGrid.RowCount - 1 To 0 Step -1
            '                        If mLineGrid.Item(mColumnMandatory, J).Value <> ""  And mLineGrid.Rows(J).Visible = True Then
            '                            If J = bFirstRowHavingValueInBaseColumn Then
            '                                mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) - mUsedValue, "0.00")
            '                                mUsedValue = 0
            '                            Else
            '                                mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value = Format(Val(Me.Item(Col_Amount, I).Value) * Val(mLineGrid.Item(bBaseColumn, J).Value) / bTotalBaseColumnAmt, "0.00")
            '                                mUsedValue += mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).value
            '                            End If
            '                        End If
            '                    Next
            '                End If
            '            Case "FIXEDVALUE FROM COLUMN"
            '                For J = 0 To mLineGrid.RowCount - 1
            '                    If mLineGrid.Item(mColumnMandatory, J).Value <> ""  And mLineGrid.Rows(J).Visible = True Then
            '                        mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), J).VALUE = mLineGrid.Item(Me.Item(Col_Value, I).Value, J).VALUE
            '                    End If
            '                Next
            '            Case "PERCENTAGE FROM COLUMN"
            '                For J = 0 To mLineGrid.RowCount - 1
            '                    If mLineGrid.Item(mColumnMandatory, J).Value <> ""  And mLineGrid.Rows(J).Visible = True Then
            '                        mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = mLineGrid.Item(Me.Item(Col_Value, I).Value, J).VALUE
            '                    End If
            '                Next
            '            Case "PERCENTAGE", "PERCENTAGE CHANGEABLE"
            '                If Me.Item(Col_LineItem, I).Value <> 1 Then
            '                    For J = 0 To mLineGrid.RowCount - 1
            '                        If mLineGrid.Item(mColumnMandatory, J).Value <> ""  And mLineGrid.Rows(J).Visible = True Then
            '                            mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), J).VALUE = Val(Me.Item(Col_Percentage, I).Value)
            '                        End If
            '                    Next
            '                End If
            '        End Select
            '    End If
            'Next




            mCost = 0
            For J = 0 To Me.RowCount - 1
                If Agl.StrCmp(Me.Item(Col_Charge_Type, J).Value, "Cost") Then
                    Me.Item(Col_Amount, J).Value = Format(mCost, "0.00")
                    Exit For
                End If
                If Me.Item(Col_AffectCost, J).Value = "1" Then
                    mCost += Val(Me.Item(Col_Amount, J).Value)
                ElseIf Me.Item(Col_AffectCost, J).Value = "0" Then
                    mCost -= Val(Me.Item(Col_Amount, J).Value)
                End If
            Next
        End If

        RaiseEvent Calculated()

    End Sub



    Function FilterQuery(ByVal Qry As String, ByVal RowNum As Integer)
        Dim I As Integer
        Dim mColName As String = ""
        Dim mFlag As Boolean
        Dim mIsText As Boolean
        Dim mFilteredQry As String = ""
        Dim mPickedChar As Char

        If InStr(Qry, "|") > 0 Then
            For I = 0 To Qry.Length - 1
                mPickedChar = Agl.MidStr(Qry, I, 1)
                If mFlag And Not mPickedChar = "|" Then
                    mColName = mColName + mPickedChar
                ElseIf mFlag And mPickedChar = "|" Then
                    If Me.mLineGrid(mColName, RowNum).Value Is Nothing Then Me.mLineGrid(mColName, RowNum).Value = ""
                    If Not mIsText Then
                        mFilteredQry = mFilteredQry + CStr(Val(Me.mLineGrid(mColName, RowNum).Value))
                    Else
                        mFilteredQry = mFilteredQry + Me.mLineGrid(mColName, RowNum).Value.ToString
                    End If
                    mFlag = False
                ElseIf Agl.MidStr(Qry, I, 1) = "|" And Not mFlag Then
                    mFlag = True
                    mColName = ""
                    mIsText = False
                    If I <> 0 Then
                        If Agl.MidStr(Qry, I - 1, 1) = "`" Then
                            mIsText = True
                        End If
                    End If
                Else
                    mFilteredQry = mFilteredQry + mPickedChar
                End If
            Next
        Else
            mFilteredQry = Qry
        End If

        FilterQuery = mFilteredQry
    End Function

    Public Sub Save_TransFooter(ByVal DocID As String, ByVal mConn As Object, ByVal mCmd As Object, Optional ByVal UID As String = "")
        Dim mQry$, I%
        Dim Sr% = 0
        Dim mTableName$ = ""
        Dim strUpdateQry As String


        strUpdateQry = ""
        If FrmType = ClsMain.EntryPointType.Main Then
            mQry = "Delete From dbo.Structure_TransFooter Where DocID = '" & DocID & "'"
            Agl.Dman_ExecuteNonQry(mQry, mConn, mCmd)

            mQry = "Delete From dbo.Structure_TransLine Where DocID = '" & DocID & "'"
            Agl.Dman_ExecuteNonQry(mQry, mConn, mCmd)

            For I = 0 To Me.RowCount - 1
                If Me.Item(Col_Charges, I).Value <> "" Then
                    Sr += 1
                    'mQry = "INSERT INTO dbo.Structure_TransFooter(UID,DocID,Sr,Charges,Charge_Type,Value_Type,Value,Calculation,BaseColumn,PostAc,PostAcFromColumn,DrCr,LineItem,AffectCost,Percentage,Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, GridDisplayIndex) " & _
                    '       " VALUES 	(" & Agl.Chk_Text(UID) & ", " & Agl.Chk_Text(DocID) & ", " & Sr & "," & Agl.Chk_Text(Me.AgSelectedValue(Col_Charges, I)) & "," & Agl.Chk_Text(Me.Item(Col_Charge_Type, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_Value_Type, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_Value, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_Calculation, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_BaseColumn, I).Value) & "," & Agl.Chk_Text(IIf(Agl.StrCmp(Me.Item(Col_PostAc, I).Value, "|Party|"), Me.AgPostingPartyAc, Me.Item(Col_PostAc, I).Value)) & "," & Agl.Chk_Text(Me.Item(Col_PostAcFromColumn, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_DrCr, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_LineItem, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_AffectCost, I).Value) & "," & Val(Me.Item(Col_Percentage, I).Value) & "," & Val(Me.Item(Col_Amount, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_VisibleInMaster, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_VisibleInMasterLine, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_VisibleInTransactionLine, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_VisibleInTransactionFooter, I).Value) & ", " & Val(Me.Item(Col_GridDisplayIndex, I).Value) & ")"

                    mQry = "INSERT INTO dbo.Structure_TransFooter(UID,DocID,Sr,Charges,Charge_Type,Value_Type,Value,Calculation,BaseColumn,PostAc,PostAcFromColumn,ContraAc,ContraAcFromColumn,DrCr,LineItem,AffectCost,Percentage,Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, GridDisplayIndex) " &
                           " VALUES 	(" & Agl.Chk_Text(UID) & ", " & Agl.Chk_Text(DocID) & ", " & Sr & "," & Agl.Chk_Text(Me.AgSelectedValue(Col_Charges, I)) & "," & Agl.Chk_Text(Me.Item(Col_Charge_Type, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_Value_Type, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_Value, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_Calculation, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_BaseColumn, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_PostAc, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_PostAcFromColumn, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_ContraAc, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_ContraAcFromColumn, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_DrCr, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_LineItem, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_AffectCost, I).Value) & "," & Val(Me.Item(Col_Percentage, I).Value) & "," & Val(Me.Item(Col_Amount, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_VisibleInMaster, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_VisibleInMasterLine, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_VisibleInTransactionLine, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_VisibleInTransactionFooter, I).Value) & ", " & Val(Me.Item(Col_GridDisplayIndex, I).Value) & ")"

                    Agl.Dman_ExecuteNonQry(mQry, mConn, mCmd)



                End If
            Next


        Else
            mQry = "Delete From dbo.Structure_TransFooter_Log Where UID = '" & UID & "'"
            Agl.Dman_ExecuteNonQry(mQry, mConn, mCmd)

            mQry = "Delete From dbo.Structure_TransLine_Log Where UID = '" & UID & "'"
            Agl.Dman_ExecuteNonQry(mQry, mConn, mCmd)

            For I = 0 To Me.RowCount - 1
                If Me.Item(Col_Charges, I).Value <> "" Then
                    Sr += 1
                    'mQry = "INSERT INTO dbo.Structure_TransFooter_Log(UID, DocID,Sr,Charges,Charge_Type,Value_Type,Value,Calculation,BaseColumn,PostAc,PostAcFromColumn,DrCr,LineItem,AffectCost,Percentage,Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, GridDisplayIndex) " & _
                    '       " VALUES 	(" & Agl.Chk_Text(UID) & ",  " & Agl.Chk_Text(DocID) & ", " & Sr & "," & Agl.Chk_Text(Me.AgSelectedValue(Col_Charges, I)) & "," & Agl.Chk_Text(Me.Item(Col_Charge_Type, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_Value_Type, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_Value, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_Calculation, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_BaseColumn, I).Value) & "," & Agl.Chk_Text(IIf(Agl.StrCmp(Me.Item(Col_PostAc, I).Value, "|Party|"), Me.AgPostingPartyAc, Me.Item(Col_PostAc, I).Value)) & "," & Agl.Chk_Text(Me.Item(Col_PostAcFromColumn, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_DrCr, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_LineItem, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_AffectCost, I).Value) & "," & Val(Me.Item(Col_Percentage, I).Value) & "," & Val(Me.Item(Col_Amount, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_VisibleInMaster, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_VisibleInMasterLine, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_VisibleInTransactionLine, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_VisibleInTransactionFooter, I).Value) & "," & Val(Me.Item(Col_GridDisplayIndex, I).Value) & ")"

                    mQry = "INSERT INTO dbo.Structure_TransFooter_Log(UID, DocID,Sr,Charges,Charge_Type,Value_Type,Value,Calculation,BaseColumn,PostAc,PostAcFromColumn, ContraAc, ContraAcFromColumn,DrCr,LineItem,AffectCost,Percentage,Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, GridDisplayIndex) " &
                           " VALUES 	(" & Agl.Chk_Text(UID) & ",  " & Agl.Chk_Text(DocID) & ", " & Sr & "," & Agl.Chk_Text(Me.AgSelectedValue(Col_Charges, I)) & "," & Agl.Chk_Text(Me.Item(Col_Charge_Type, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_Value_Type, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_Value, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_Calculation, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_BaseColumn, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_PostAc, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_PostAcFromColumn, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_ContraAc, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_ContraAcFromColumn, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_DrCr, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_LineItem, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_AffectCost, I).Value) & "," & Val(Me.Item(Col_Percentage, I).Value) & "," & Val(Me.Item(Col_Amount, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_VisibleInMaster, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_VisibleInMasterLine, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_VisibleInTransactionLine, I).Value) & "," & Agl.Chk_Text(Me.Item(Col_VisibleInTransactionFooter, I).Value) & "," & Val(Me.Item(Col_GridDisplayIndex, I).Value) & ")"

                    Agl.Dman_ExecuteNonQry(mQry, mConn, mCmd)



                End If
            Next


        End If


    End Sub

    Public Sub FUpdateFooterTable(ByVal mTableName As String, ByVal mKeyFieldName As String, ByVal mSearchKey As String, ByVal mConn As Object, ByVal mCmd As Object)
        Dim I%
        Dim strUpdateQry As String


        strUpdateQry = ""
        For I = 0 To Me.RowCount - 1
            If Me.Item(Col_Charges, I).Value <> "" Then
                If Me.Item(Col_HeaderPerField, I).Value <> "" Then
                    strUpdateQry += Me.Item(Col_HeaderPerField, I).Value & " = " & Val(Me.Item(Col_Percentage, I).Value) & ","
                End If

                If Me.Item(Col_HeaderAmtField, I).Value <> "" Then
                    strUpdateQry += Me.Item(Col_HeaderAmtField, I).Value & " = " & Val(Me.Item(Col_Amount, I).Value) & ","
                End If
            End If
        Next

        If strUpdateQry <> "" Then
            strUpdateQry = strUpdateQry.Substring(0, strUpdateQry.Length - 1)
            strUpdateQry = " Update " & mTableName & " Set " & strUpdateQry
            strUpdateQry = strUpdateQry & " Where " & mKeyFieldName & " = '" & mSearchKey & "'"
            Agl.Dman_ExecuteNonQry(strUpdateQry, mConn, mCmd)
        End If
    End Sub



    Public Sub Save_TransLine(ByVal DocID As String, ByVal TransSr As Integer, ByVal CurrRow As Integer, ByVal mConn As Object, ByVal mCmd As Object, Optional ByVal UID As String = "")
        Dim mQry$, I% ', J%
        Dim Sr%
        Dim mTableName$ = ""

        'For J = 0 To mLineGrid.RowCount - 1
        'If mLineGrid.Item(mColumnMandatory, J).Value <> "" Then

        If FrmType = ClsMain.EntryPointType.Main Then
            mTableName = "Structure_TransLine"
        Else
            mTableName = "Structure_TransLine_Log"
        End If

        Sr = 0
        For I = 0 To Me.RowCount - 1
            If Me.Item(Col_Charges, I).Value <> "" Then
                Sr += 1

                Select Case UCase(Me.Item(Col_Value_Type, I).Value)
                    Case "PERCENTAGE", "PERCENTAGE CHANGEABLE", "PERCENTAGE FROM COLUMN", "PERCENTAGE OR AMOUNT"
                        mQry = "INSERT iNTO " & mTableName & " (UID,DocID,TSr,Sr,Charges,Percentage,Amount) " &
                               " VALUES (" & Agl.Chk_Text(UID) & ", " & Agl.Chk_Text(DocID) & "," & TransSr & "," & Sr & "," & Agl.Chk_Text(Me.AgSelectedValue(Col_Charges, I)) & "," & Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), CurrRow).VALUE) & "," & Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), CurrRow).VALUE) & ")"
                    Case Else
                        mQry = "INSERT iNTO " & mTableName & " (UID,DocID,TSr,Sr,Charges,Percentage,Amount) " &
                               " VALUES (" & Agl.Chk_Text(UID) & "," & Agl.Chk_Text(DocID) & "," & TransSr & "," & Sr & "," & Agl.Chk_Text(Me.AgSelectedValue(Col_Charges, I)) & ",0," & Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), CurrRow).VALUE) & ")"
                End Select

                Agl.Dman_ExecuteNonQry(mQry, mConn, mCmd)
            End If
        Next
        'End If
        'Next



    End Sub

    Public Sub FUpdateLineTable(ByVal mTableName As String, ByVal mKeyFieldName As String, ByVal mSrKeyFieldName As String, ByVal mSearchKey As String, ByVal TransSr As Integer, ByVal CurrRow As Integer, ByVal mConn As Object, ByVal mCmd As Object)
        Dim I%
        Dim strUpdateQry$

        strUpdateQry = ""

        For I = 0 To Me.RowCount - 1
            If Me.Item(Col_Charges, I).Value <> "" Then
                If Me.Item(Col_LinePerField, I).Value <> "" Then
                    strUpdateQry += Me.Item(Col_LinePerField, I).Value & "=" & Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), CurrRow).VALUE) & ","
                End If

                If Me.Item(Col_LineAmtField, I).Value <> "" Then
                    strUpdateQry += Me.Item(Col_LineAmtField, I).Value & "=" & Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), CurrRow).VALUE) & ","
                End If
            End If
        Next


        If strUpdateQry <> "" Then
            strUpdateQry = strUpdateQry.Substring(0, strUpdateQry.Length - 1)
            strUpdateQry = " Update " & mTableName & " Set " & strUpdateQry
            strUpdateQry = strUpdateQry & " Where " & mKeyFieldName & " = '" & mSearchKey & "' And " & mSrKeyFieldName & " = " & TransSr & " "

            Agl.Dman_ExecuteNonQry(strUpdateQry, mConn, mCmd)

        End If


    End Sub


    Public Function FFooterTableUpdateStr(Optional ByVal MultiplyWithMinus As Boolean = False) As String
        Dim I%
        Dim strUpdateQry As String


        strUpdateQry = ""
        For I = 0 To Me.RowCount - 1
            If Me.Item(Col_Charges, I).Value <> "" Then
                If Me.Item(Col_HeaderPerField, I).Value <> "" Then
                    strUpdateQry += Me.Item(Col_HeaderPerField, I).Value & " = " & Val(Me.Item(Col_Percentage, I).Value) & ","
                End If

                If Me.Item(Col_HeaderAmtField, I).Value <> "" Then
                    strUpdateQry += Me.Item(Col_HeaderAmtField, I).Value & " =  " & IIf(MultiplyWithMinus, -1, 1) * Val(Me.Item(Col_Amount, I).Value) & ","
                End If
            End If
        Next

        If strUpdateQry <> "" Then
            strUpdateQry = strUpdateQry.Substring(0, strUpdateQry.Length - 1)
        End If

        FFooterTableUpdateStr = strUpdateQry
    End Function

    Public Function FLineTableUpdateStr(ByVal CurrRow As Integer, Optional ByVal MultiplyWithMinus As Boolean = False) As String
        Dim I%
        Dim strUpdateQry$

        strUpdateQry = ""

        For I = 0 To Me.RowCount - 1
            If Me.Item(Col_Charges, I).Value <> "" Then
                If Me.Item(Col_LinePerField, I).Value <> "" Then
                    strUpdateQry += " " & Me.Item(Col_LinePerField, I).Value & " = " & Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), CurrRow).VALUE) & ","
                End If

                If Me.Item(Col_LineAmtField, I).Value <> "" Then
                    strUpdateQry += " " & Me.Item(Col_LineAmtField, I).Value & " = " & IIf(MultiplyWithMinus, -1, 1) * Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), CurrRow).VALUE) & ","
                End If
            End If
        Next

        If strUpdateQry <> "" Then
            strUpdateQry = strUpdateQry.Substring(0, strUpdateQry.Length - 1)
        End If

        FLineTableUpdateStr = strUpdateQry
    End Function


    Public Function FFooterTableFieldNameStr() As String
        Dim I%
        Dim strUpdateQry As String


        strUpdateQry = ""
        For I = 0 To Me.RowCount - 1
            If Me.Item(Col_Charges, I).Value <> "" Then
                If Me.Item(Col_HeaderPerField, I).Value <> "" Then
                    strUpdateQry += Me.Item(Col_HeaderPerField, I).Value & ","
                End If

                If Me.Item(Col_HeaderAmtField, I).Value <> "" Then
                    strUpdateQry += Me.Item(Col_HeaderAmtField, I).Value & ","
                End If
            End If
        Next

        If strUpdateQry <> "" Then
            strUpdateQry = strUpdateQry.Substring(0, strUpdateQry.Length - 1)
        End If

        FFooterTableFieldNameStr = strUpdateQry
    End Function

    Public Function FFooterTableFieldNameStr(ByVal FieldPrefix As String, ByVal AliasPrefix As String) As String
        Dim I%
        Dim strUpdateQry As String


        strUpdateQry = ""
        For I = 0 To Me.RowCount - 1
            If Me.Item(Col_Charges, I).Value <> "" Then
                If Me.Item(Col_HeaderPerField, I).Value <> "" Then
                    strUpdateQry += FieldPrefix & Me.Item(Col_HeaderPerField, I).Value & "  " & AliasPrefix & Me.Item(Col_HeaderPerField, I).Value & ","
                End If

                If Me.Item(Col_HeaderAmtField, I).Value <> "" Then
                    strUpdateQry += FieldPrefix & Me.Item(Col_HeaderAmtField, I).Value & "  " & AliasPrefix & Me.Item(Col_HeaderAmtField, I).Value & ","
                End If
            End If
        Next

        If strUpdateQry <> "" Then
            strUpdateQry = strUpdateQry.Substring(0, strUpdateQry.Length - 1)
        End If

        FFooterTableFieldNameStr = strUpdateQry
    End Function
    Public Function FLineTableSchemaStr() As String
        Dim I%
        Dim strUpdateQry$

        strUpdateQry = ""

        For I = 0 To Me.RowCount - 1
            If Me.Item(Col_Charges, I).Value <> "" Then
                If Me.Item(Col_LinePerField, I).Value <> "" Then
                    strUpdateQry += Me.Item(Col_LinePerField, I).Value & "  Float,"
                End If

                If Me.Item(Col_LineAmtField, I).Value <> "" Then
                    strUpdateQry += Me.Item(Col_LineAmtField, I).Value & " Float,"
                End If
            End If
        Next


        If strUpdateQry <> "" Then
            strUpdateQry = strUpdateQry.Substring(0, strUpdateQry.Length - 1)
        End If

        FLineTableSchemaStr = strUpdateQry

    End Function


    Public Function FLineTableFieldNameStr() As String
        Dim I%
        Dim strUpdateQry$

        strUpdateQry = ""

        For I = 0 To Me.RowCount - 1
            If Me.Item(Col_Charges, I).Value <> "" Then
                If Me.Item(Col_LinePerField, I).Value <> "" Then
                    strUpdateQry += Me.Item(Col_LinePerField, I).Value & ","
                End If

                If Me.Item(Col_LineAmtField, I).Value <> "" Then
                    strUpdateQry += Me.Item(Col_LineAmtField, I).Value & ","
                End If
            End If
        Next


        If strUpdateQry <> "" Then
            strUpdateQry = strUpdateQry.Substring(0, strUpdateQry.Length - 1)
        End If

        FLineTableFieldNameStr = strUpdateQry

    End Function

    Public Function FLineTableFieldNameStr(ByVal FieldPrefix As String, ByVal AliasPrefix As String) As String
        Dim I%
        Dim strUpdateQry$

        strUpdateQry = ""

        For I = 0 To Me.RowCount - 1
            If Me.Item(Col_Charges, I).Value <> "" Then
                If Me.Item(Col_LinePerField, I).Value <> "" Then
                    strUpdateQry += FieldPrefix & Me.Item(Col_LinePerField, I).Value & "  " & AliasPrefix & Me.Item(Col_LinePerField, I).Value & ","
                End If

                If Me.Item(Col_LineAmtField, I).Value <> "" Then
                    strUpdateQry += FieldPrefix & Me.Item(Col_LineAmtField, I).Value & "  " & AliasPrefix & Me.Item(Col_LineAmtField, I).Value & ","
                End If
            End If
        Next


        If strUpdateQry <> "" Then
            strUpdateQry = strUpdateQry.Substring(0, strUpdateQry.Length - 1)
        End If

        FLineTableFieldNameStr = strUpdateQry

    End Function


    Public Function FLineTableFieldValuesStr(ByVal CurrRow As Integer, Optional ByVal MultiplyWithMinus As Boolean = False) As String
        Dim I%
        Dim strUpdateQry$

        strUpdateQry = ""

        For I = 0 To Me.RowCount - 1
            If Me.Item(Col_Charges, I).Value <> "" Then
                If Me.Item(Col_LinePerField, I).Value <> "" Then
                    strUpdateQry += "" & Val(mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), CurrRow).VALUE) & ","
                End If

                If Me.Item(Col_LineAmtField, I).Value <> "" Then
                    strUpdateQry += "" & IIf(MultiplyWithMinus, -1, 1) * Val(mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), CurrRow).VALUE) & ","
                End If
            End If
        Next

        If strUpdateQry <> "" Then
            strUpdateQry = strUpdateQry.Substring(0, strUpdateQry.Length - 1)
        End If

        FLineTableFieldValuesStr = strUpdateQry
    End Function




    Public Sub MoveRec_TransFooter(ByVal mSearchCode As String)
        Dim mQry$ = "", I%
        Dim DtTemp As DataTable = Nothing
        If mSearchCode = "" Then Exit Sub
        Try
            If FrmType = ClsMain.EntryPointType.Main Then
                mQry = "Select F.*,C.ManualCode from (Select * From Structure_TransFooter  Where DocID = '" & mSearchCode & "') F Left Join Charges C On F.Charges = C.Code  Order By Sr"
            Else
                mQry = "Select F.*,C.ManualCode from (Select * From Structure_TransFooter_Log  Where UID = '" & mSearchCode & "') F Left Join Charges C On F.Charges = C.Code  Order By Sr"
            End If
            DtTemp = Agl.FillData(mQry, Agl.GCn).tables(0)

            With DtTemp
                If DtTemp.Rows.Count > 0 Then

                    Me.RowCount = 1
                    Me.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To .Rows.Count - 1
                            Me.Rows.Add()
                            'Me.Item(Col_SNo, I).Value = Me.Rows.Count - 1
                            Me.AgSelectedValue(Col_Charges, I) = Agl.XNull(.Rows(I)("Charges"))
                            Me.Item(Col_ChargesManualCode, I).Value = Agl.XNull(.Rows(I)("ManualCode"))
                            Me.Item(Col_Charge_Type, I).Value = Agl.XNull(.Rows(I)("Charge_Type"))
                            Me.Item(Col_Value_Type, I).Value = Agl.XNull(.Rows(I)("Value_Type"))
                            Me.Item(Col_Value, I).Value = Agl.XNull(.Rows(I)("Value"))
                            Me.Item(Col_Calculation, I).Value = Agl.XNull(.Rows(I)("Calculation"))
                            Me.Item(Col_BaseColumn, I).Value = Agl.XNull(.Rows(I)("BaseColumn"))
                            Me.Item(Col_DrCr, I).Value = Agl.XNull(.Rows(I)("DrCr"))
                            Me.Item(Col_PostAc, I).Value = Agl.XNull(.Rows(I)("PostAc"))
                            Me.Item(Col_PostAcFromColumn, I).Value = Agl.XNull(.Rows(I)("PostAcFromColumn"))
                            Me.Item(Col_ContraAc, I).Value = Agl.XNull(.Rows(I)("ContraAc"))
                            Me.Item(Col_ContraAcFromColumn, I).Value = Agl.XNull(.Rows(I)("ContraAcFromColumn"))
                            Me.Item(Col_LineItem, I).Value = Math.Abs(Agl.VNull(.Rows(I)("LineItem")))
                            Me.Item(Col_AffectCost, I).Value = IIf(IsDBNull(.Rows(I)("AffectCost")), "", Math.Abs(Agl.VNull(.Rows(I)("AffectCost"))))
                            'Me.Item(Col_AffectCost, I).Value = Math.Abs(Agl.VNull(.Rows(I)("AffectCost")))
                            Me.Item(Col_Active, I).Value = 1
                            Me.Item(Col_Percentage, I).Value = IIf(Agl.VNull(.Rows(I)("Percentage")) = 0, "", Agl.VNull(.Rows(I)("Percentage")))
                            Me.Item(Col_Amount, I).Value = Agl.VNull(.Rows(I)("Amount"))
                            Me.Item(Col_VisibleInMaster, I).Value = Math.Abs(Agl.VNull(.Rows(I)("VisibleInMaster")))
                            Me.Item(Col_VisibleInMasterLine, I).Value = Math.Abs(Agl.VNull(.Rows(I)("VisibleInMasterLine")))
                            Me.Item(Col_VisibleInTransactionLine, I).Value = Math.Abs(Agl.VNull(.Rows(I)("VisibleInTransactionLine")))
                            Me.Item(Col_VisibleInTransactionFooter, I).Value = Math.Abs(Agl.VNull(.Rows(I)("VisibleInTransactionFooter")))
                            Me.Item(Col_GridDisplayIndex, I).Value = Agl.VNull(.Rows(I)("GridDisplayIndex"))
                            Me.Item(Col_HeaderPerField, I).Value = Agl.XNull(.Rows(I)("HeaderPerField"))
                            Me.Item(Col_HeaderAmtField, I).Value = Agl.XNull(.Rows(I)("HeaderAmtField"))
                            Me.Item(Col_LinePerField, I).Value = Agl.XNull(.Rows(I)("LinePerField"))
                            Me.Item(Col_LineAmtField, I).Value = Agl.XNull(.Rows(I)("LineAmtField"))
                        Next
                    End If
                End If
            End With
            SetGridDisplayIndex()
            Grid_Disp()
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & " In MoveRec_TransFooter Procedure of AgCalcGrid")
        Finally
            DtTemp.Dispose()
        End Try


    End Sub


    Public Sub FMoveRecFooterTable(ByVal DtTemp As DataTable, ByVal NCat As String, ByVal mV_Date As String, Optional ByVal MultiplyWithMinus As Boolean = False)
        Dim mQry$ = "", I%
        'Dim DtTemp As DataTable = Nothing
        Dim DtStructureDetail As DataTable = Nothing
        If AgStructure = "" Then Exit Sub


        Try

            'mQry = "Select L.*, C.ManualCode as ChargesManualCode " &
            '       "From StructureDetail L  " &
            '       "Left Join Charges C  On L.Charges = C.Code " &
            '       "Where L.Code = '" & AgStructure & "' " &
            '       "and IfNull(L.WEF,'" & CDate(mV_Date).ToString("u") & "') <= '" & CDate(mV_Date).ToString("u") & "' " &
            '       "And IfNull(L.InactiveDate,'" & (DateAdd(DateInterval.Day, 1, CDate(mV_Date))).ToString("u") & "') > '" & CDate(mV_Date).ToString("u") & "'  "

            mQry = "Select L.*, C.ManualCode as ChargesManualCode 
                   From StructureDetail L 
                   Left Join Charges C  On L.Charges = C.Code 
                   Where L.Code = '" & AgStructure & "' "

            DtStructureDetail = Agl.FillData(mQry, Agl.GCn).Tables(0)
            With DtStructureDetail
                If DtStructureDetail.Rows.Count > 0 Then
                    Me.RowCount = 1
                    Me.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To .Rows.Count - 1
                            Me.Rows.Add()
                            'Me.Item(Col_SNo, I).Value = Me.Rows.Count - 1
                            Me.AgSelectedValue(Col_Charges, I) = Agl.XNull(.Rows(I)("Charges"))
                            Me.Item(Col_ChargesManualCode, I).Value = Agl.XNull(.Rows(I)("ChargesManualCode"))
                            Me.Item(Col_Charge_Type, I).Value = Agl.XNull(.Rows(I)("Charge_Type"))
                            Me.Item(Col_Value_Type, I).Value = Agl.XNull(.Rows(I)("Value_Type"))
                            Me.Item(Col_Value, I).Value = Agl.XNull(.Rows(I)("Value"))
                            Me.Item(Col_Calculation, I).Value = Agl.XNull(.Rows(I)("Calculation"))
                            Me.Item(Col_BaseColumn, I).Value = Agl.XNull(.Rows(I)("BaseColumn"))
                            Me.Item(Col_DrCr, I).Value = Agl.XNull(.Rows(I)("DrCr"))
                            Me.Item(Col_PostAc, I).Value = Agl.XNull(.Rows(I)("PostAc"))
                            Me.Item(Col_PostAcFromColumn, I).Value = Agl.XNull(.Rows(I)("PostAcFromColumn"))
                            Me.Item(Col_ContraAc, I).Value = Agl.XNull(.Rows(I)("ContraAc"))
                            Me.Item(Col_ContraAcFromColumn, I).Value = Agl.XNull(.Rows(I)("ContraAcFromColumn"))
                            Me.Item(Col_LineItem, I).Value = Math.Abs(Agl.VNull(.Rows(I)("LineItem")))
                            Me.Item(Col_AffectCost, I).Value = IIf(IsDBNull(.Rows(I)("AffectCost")), "", Math.Abs(Agl.VNull(.Rows(I)("AffectCost"))))
                            'Me.Item(Col_AffectCost, I).Value = Math.Abs(Agl.VNull(.Rows(I)("AffectCost")))
                            Me.Item(Col_Active, I).Value = 1
                            If Agl.XNull(.Rows(I)("HeaderPerField")) <> "" Then
                                Me.Item(Col_Percentage, I).Value = IIf(Agl.VNull(DtTemp.Rows(0)(Agl.XNull(.Rows(I)("HeaderPerField")))) = 0, "", DtTemp.Rows(0)(Agl.XNull(.Rows(I)("HeaderPerField"))))
                            End If

                            Me.Item(Col_Amount, I).Value = IIf(MultiplyWithMinus, -1, 1) * Agl.VNull(DtTemp.Rows(0)(Agl.XNull(.Rows(I)("HeaderAmtField"))))

                            Me.Item(Col_VisibleInMaster, I).Value = Math.Abs(Agl.VNull(.Rows(I)("VisibleInMaster")))
                            Me.Item(Col_VisibleInMasterLine, I).Value = Math.Abs(Agl.VNull(.Rows(I)("VisibleInMasterLine")))
                            Me.Item(Col_VisibleInTransactionLine, I).Value = Math.Abs(Agl.VNull(.Rows(I)("VisibleInTransactionLine")))
                            Me.Item(Col_VisibleInTransactionFooter, I).Value = Math.Abs(Agl.VNull(.Rows(I)("VisibleInTransactionFooter")))
                            Me.Item(Col_GridDisplayIndex, I).Value = Agl.VNull(.Rows(I)("GridDisplayIndex"))
                            Me.Item(Col_HeaderPerField, I).Value = Agl.XNull(.Rows(I)("HeaderPerField"))
                            Me.Item(Col_HeaderAmtField, I).Value = Agl.XNull(.Rows(I)("HeaderAmtField"))
                            Me.Item(Col_LinePerField, I).Value = Agl.XNull(.Rows(I)("LinePerField"))
                            Me.Item(Col_LineAmtField, I).Value = Agl.XNull(.Rows(I)("LineAmtField"))
                        Next
                    End If
                End If
            End With
            SetGridDisplayIndex()
            Grid_Disp()
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & " In MoveRec_TransFooter Procedure of AgCalcGrid")
        Finally
            DtTemp.Dispose()
        End Try


    End Sub

    Public Sub FMoveRecLineTable(ByVal DtTemp As DataTable, ByVal CurrRow As Integer, Optional ByVal MultiplyWithMinus As Boolean = False)
        Dim mQry$ = "", I%

        If AgStructure = "" Then Exit Sub
        Try
            For I = 0 To Me.RowCount - 1
                If Me.Item(Col_Charges, I).Value <> "" Then
                    If Agl.XNull(Me.Item(Col_LinePerField, I).Value) <> "" Then
                        mLineGrid.Item(GetColNamePer(Me.Item(Col_Charges, I).Value), CurrRow).value = Agl.VNull(DtTemp.Rows(CurrRow)(Agl.XNull(Me.Item(Col_LinePerField, I).Value)))
                    End If
                    If Agl.XNull(Me.Item(Col_LineAmtField, I).Value) <> "" Then
                        mLineGrid.Item(GetColName(Me.Item(Col_Charges, I).Value), CurrRow).value = IIf(MultiplyWithMinus, -1, 1) * Agl.VNull(DtTemp.Rows(CurrRow)(Agl.XNull(Me.Item(Col_LineAmtField, I).Value)))
                    End If
                    mLineGrid.Item("Is Database Value", CurrRow).Value = "1"
                End If
            Next


        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & " In MoveRec_TransLine Procedure of AgCalcGrid")
        Finally
            DtTemp.Dispose()
        End Try
    End Sub

    Public Sub FMoveRecLineLedgerAc()
        Dim J As Integer, I As Integer

        If DtPostingGroupSalesTax Is Nothing Then Exit Sub

        For J = 0 To mLineGrid.RowCount - 1
            If mLineGrid.Item(mColumnMandatory, J).Value <> "" And mLineGrid.Rows(J).Visible = True Then
                If mColumnSalesTaxGroupItem > -1 Then
                    DrPostingGroupSalesTax = DtPostingGroupSalesTax.Select("PostingGroupSalesTaxItem='" & mLineGrid.AgSelectedValue(mColumnSalesTaxGroupItem, J) & "' And PostingGroupSalesTaxParty='" & mSalesTaxGroupParty & "' ", "WEF Desc")
                    If DrPostingGroupSalesTax.Length <= 0 Then
                        DrPostingGroupSalesTax = DtPostingGroupSalesTax.Select("PostingGroupSalesTaxItem='" & mSalesTaxGroupItem & "' And PostingGroupSalesTaxParty='" & mSalesTaxGroupParty & "' ", "WEF Desc")
                    End If
                ElseIf mSalesTaxGroupItem <> "" Then
                    DrPostingGroupSalesTax = DtPostingGroupSalesTax.Select("PostingGroupSalesTaxItem='" & mSalesTaxGroupItem & "' And PostingGroupSalesTaxParty='" & mSalesTaxGroupParty & "' ", "WEF Desc")
                End If




                For I = 0 To Me.Rows.Count - 1
                    Select Case UCase(Me.Item(Col_Charge_Type, I).Value)
                        Case "SALESTAXASSESSABLEAMT"
                            If mVoucherCategory.ToUpper = "" Then Err.Raise(1, , "Vouhcer category must be defined either purchase or sales, if SalesTax is used in structure.")
                            If mVoucherCategory.ToUpper <> "SALES" And mVoucherCategory.ToUpper <> "PURCH" Then Err.Raise(1, , "Vouhcer category must be either purchase or sales, if SalesTax is used in structure.")
                            If mColumnSalesTaxGroupItem >= 0 And mSalesTaxGroupParty <> "" Then
                                If DrPostingGroupSalesTax.Length > 0 Then
                                    If Me.Item(Col_LineItem, I).Value Then
                                        If mVoucherCategory.ToUpper = "SALES" Then
                                            mLineGrid.Item(GetColNamePostAc(Me.Item(Col_Charges, I).Value), J).value = DrPostingGroupSalesTax(0)("SalesAc")
                                        ElseIf mVoucherCategory.ToUpper = "PURCH" Then
                                            mLineGrid.Item(GetColNamePostAc(Me.Item(Col_Charges, I).Value), J).value = DrPostingGroupSalesTax(0)("PurchaseAc")
                                        End If
                                    End If
                                End If
                            Else
                                mLineGrid.Item(GetColNamePostAc(Me.Item(Col_Charges, I).Value), J).value = ""
                            End If

                        Case "SALESTAX"
                            If mVoucherCategory.ToUpper = "" Then Err.Raise(1, , "Vouhcer category must be defined either purchase or sales, if SalesTax is used in structure.")
                            If mVoucherCategory.ToUpper <> "SALES" And mVoucherCategory.ToUpper <> "PURCH" Then Err.Raise(1, , "Vouhcer category must be either purchase or sales, if SalesTax is used in structure.")

                            If mColumnSalesTaxGroupItem >= 0 And mSalesTaxGroupParty <> "" Then
                                If DrPostingGroupSalesTax.Length > 0 Then
                                    If Me.Item(Col_LineItem, I).Value Then
                                        If mVoucherCategory.ToUpper = "SALES" Then
                                            mLineGrid.Item(GetColNamePostAc(Me.Item(Col_Charges, I).Value), J).value = DrPostingGroupSalesTax(0)("SalesTaxOnSalesAc")
                                        ElseIf mVoucherCategory.ToUpper = "PURCH" Then
                                            mLineGrid.Item(GetColNamePostAc(Me.Item(Col_Charges, I).Value), J).value = DrPostingGroupSalesTax(0)("SalesTaxOnPurchaseAc")
                                        End If
                                    End If
                                Else
                                    MsgBox("SalesTax posting groups not defined for SalesTax in selected branch and division.")
                                End If
                            Else
                                mLineGrid.Item(GetColNamePostAc(Me.Item(Col_Charges, I).Value), J).value = ""
                            End If

                        Case "SALESADDITIONALTAX"
                            If mColumnSalesTaxGroupItem >= 0 And mSalesTaxGroupParty <> "" Then
                                If DrPostingGroupSalesTax.Length > 0 Then
                                    If Me.Item(Col_LineItem, I).Value Then
                                        If mVoucherCategory.ToUpper = "SALES" Then
                                            mLineGrid.Item(GetColNamePostAc(Me.Item(Col_Charges, I).Value), J).value = DrPostingGroupSalesTax(0)("AdditionalTaxOnSalesAc")
                                        ElseIf mVoucherCategory.ToUpper = "PURCH" Then
                                            mLineGrid.Item(GetColNamePostAc(Me.Item(Col_Charges, I).Value), J).value = DrPostingGroupSalesTax(0)("AdditionalTaxOnPurchaseAc")
                                        End If
                                    End If
                                Else
                                    MsgBox("SalesTax posting groups not defined for Sales Additional Tax in selected branch and division.")
                                End If
                            Else
                            End If




                    End Select
                Next

            End If
        Next

    End Sub



    Public Sub MoveRec_TransLine(ByVal mSearchCode As String, ByVal TransSr As Integer, ByVal CurrRow As Integer)
        Dim mQry$ = "", I%
        Dim DtTemp As DataTable = Nothing

        Try
            If FrmType = ClsMain.EntryPointType.Main Then
                mQry = "Select S.*, IfNull(F.Value_Type,'') as Value_Type, C.Description from (Select * From Structure_TransLine  Where DocID = '" & mSearchCode & "' And Tsr = " & TransSr & ") S Left Join Structure_TransFooter F   On S.DocID = F.DocID and S.Charges = F.Charges Left Join Charges C   On S.Charges = C.Code Order By S.Sr"
            Else
                mQry = "Select S.*, IfNull(F.Value_Type,'') as Value_Type, C.Description from (Select * From Structure_TransLine_Log   Where UID = '" & mSearchCode & "' And Tsr = " & TransSr & ") S  Left Join Structure_TransFooter_Log F   On S.UID = F.UID and S.Charges = F.Charges Left Join Charges C   On S.Charges = C.Code Order By S.Sr"
            End If
            DtTemp = Agl.FillData(mQry, Agl.GCn).tables(0)

            With DtTemp
                If DtTemp.Rows.Count > 0 Then

                    For I = 0 To .Rows.Count - 1
                        Select Case UCase(.Rows(I)("Value_Type"))
                            Case "PERCENTAGE", "PERCENTAGE CHANGEABLE", "PERCENTAGE FROM COLUMN", "PERCENTAGE OR AMOUNT"
                                mLineGrid.Item(GetColNamePer(.Rows(I)("Description")), CurrRow).value = .Rows(I)("Percentage")
                                mLineGrid.Item(GetColName(.Rows(I)("Description")), CurrRow).value = .Rows(I)("Amount")
                            Case Else
                                mLineGrid.Item(GetColName(.Rows(I)("Description")), CurrRow).value = .Rows(I)("Amount")
                        End Select
                        mLineGrid.Item("Is Database Value", CurrRow).Value = "1"
                    Next
                End If

            End With
        Catch ex As Exception
            MsgBox(ex.Message & vbCrLf & " In MoveRec_TransLine Procedure of AgCalcGrid")
        Finally
            DtTemp.Dispose()
        End Try


    End Sub



    Private Sub AgCalcGrid_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.EditingControl_Validating
        Dim mObj As Object
        mObj = Me.FindForm
        mObj.Calculation()
        Calculation()
        'Calculation()
    End Sub

    Public Shared Function AgStructureSubQuery(ByVal Agl As AgLibrary.ClsMain, ByVal NCat As String, Optional ByVal EntryPointType As ClsMain.EntryPointType = ClsMain.EntryPointType.Main) As String
        Dim mQry, mTableFooter, mTableLine

        If EntryPointType = ClsMain.EntryPointType.Main Then
            mTableFooter = "Structure_TransFooter"
            mTableLine = "Structure_TransLine"
        Else
            mTableFooter = "Structure_TransFooter_Log"
            mTableLine = "Structure_TransLine_Log"
        End If

        mQry = "DECLARE  @XColumns NVARCHAR(Max) " & _
            "SET @XColumns = '' " & _
            "Select @XColumns = @XColumns  + ' SUM(Case Charges WHEN ''' + [a].[Column] + ''' THEN Amount ELSE 0 END) AS [SL_' + [a].[Description] + '], ' + (CASE WHEN [a].Value_Type='PERCENTAGE' OR [a].Value_Type='PERCENTAGE CHANGEABLE' OR [a].Value_Type='PERCENTAGE OR AMOUNT' OR [a].Value_Type='PERCENTAGE FROM COLUMN' THEN ' SUM(Case Charges WHEN ''' + [a].[Column] + ''' THEN Percentage ELSE 0 END) AS [SL_' + [a].[Description] + '_Per],' ELSE ''  END) " & _
            "From " & _
            "(SELECT  SL.Charges  as [Column],  Max(C.Description) As Description, Max(SL.Value_Type) as Value_Type " & _
            "FROM " & mTableFooter & " SL LEFT JOIN Charges C ON SL.Charges =C.Code  Where LTrim(RTrim(SubString(DocID,4,5))) in (Select V_Type From Voucher_Type Where NCat = '" & NCat & "') Group By SL.Charges) as a " & _
            "SET @XColumns = 'SELECT UID, DocID, TSR,' + @XColumns + ' Count(Amount) as SL_RecordCount FROM " & mTableLine & " '  "
        If NCat <> "" Then
            mQry = mQry & "SET @XColumns =@XColumns + ' Where LTrim(RTrim(SubString(DocID,4,5))) in (Select V_Type From Voucher_Type Where NCat = ''" & NCat & "'') ' "
        End If
        mQry = mQry & "SET @XColumns = @XColumns + ' GROUP BY UID, DocID, TSR ' "
        mQry = mQry & "SElect @XColumns "
        mQry = Agl.XNull(Agl.FillData(mQry, Agl.GCn).Tables(0).Rows(0)(0))
        AgStructureSubQuery = mQry
    End Function

    Public Shared Function AgStructureSubQueryFooter(ByVal Agl As AgLibrary.ClsMain, ByVal NCat As String, Optional ByVal EntryPointType As ClsMain.EntryPointType = ClsMain.EntryPointType.Main) As String
        Dim mQry, mTableFooter As String
        If EntryPointType = ClsMain.EntryPointType.Main Then
            mTableFooter = "Structure_TransFooter"
        Else
            mTableFooter = "Structure_TransFooter_Log"
        End If

        mQry = "DECLARE  @XColumns NVARCHAR(Max) " & _
            "SET @XColumns = '' " & _
            "Select @XColumns = @XColumns  + ' SUM(Case Charges WHEN ''' + [a].[Column] + ''' THEN Amount ELSE 0 END) AS [SF_' + [a].[Description] + '], ' + (CASE WHEN [a].Value_Type='PERCENTAGE' OR [a].Value_Type='PERCENTAGE OR AMOUNT' OR [a].Value_Type='PERCENTAGE CHANGEABLE' OR [a].Value_Type='PERCENTAGE FROM COLUMN' THEN ' SUM(Case Charges WHEN ''' + [a].[Column] + ''' THEN Percentage ELSE 0 END) AS [SF_' + [a].[Description] + '_Per],' ELSE ''  END) " & _
            "From " & _
            "(SELECT SL.Charges  as [Column],  Max(C.Description) as Description, Max(SL.Value_Type) as Value_Type " & _
            "FROM " & mTableFooter & " SL LEFT JOIN Charges C ON SL.Charges =C.Code Where LTrim(RTrim(SubString(DocID,4,5))) in (Select V_Type From Voucher_Type Where NCat = '" & NCat & "') Group By SL.Charges) as a " & _
            "SET @XColumns = 'SELECT UID, DocID, ' + @XColumns + ' Count(Amount) as SF_RecordCount FROM " & mTableFooter & " '  "
        If NCat <> "" Then
            mQry = mQry & "SET @XColumns =@XColumns + ' Where LTrim(RTrim(SubString(DocID,4,5))) in (Select V_Type From Voucher_Type Where NCat = ''" & NCat & "'') ' "
        End If
        mQry = mQry & "SET @XColumns = @XColumns + ' GROUP BY UID, DocID ' "
        mQry = mQry & "SElect @XColumns "
        mQry = Agl.XNull(Agl.FillData(mQry, Agl.GCn).Tables(0).Rows(0)(0))
        AgStructureSubQueryFooter = mQry
    End Function

    Public Function PostingLedgAry() As AgLibrary.ClsMain.LedgRec()
        Dim LedgAry() As AgLibrary.ClsMain.LedgRec
        Dim I As Integer, J As Integer
        Dim mNarr As String = "", mCommonNarr$ = ""

        mCommonNarr = ""
        If mCommonNarr.Length > 255 Then mCommonNarr = Agl.MidStr(mCommonNarr, 0, 255)


        If Me.AgPostingPartyAc = "" Then
            Err.Raise(1, , "Party A/c is not defined")
        End If

        If Me.AgPaidMAount > 0 Then
            If Me.AgPaidAc = "" Then
                Err.Raise(1, , "Paid amount is not defined")
            End If

        End If
        ReDim Preserve LedgAry(I)

        mNarr = Me.AgNarration


        For J = 0 To Me.Rows.Count - 1
            If Agl.XNull(Me.Item(Col_PostAc, J).Value) <> "" And Val(Me.Item(Col_Amount, J).Value) <> 0 Then
                I = UBound(LedgAry) + 1
                ReDim Preserve LedgAry(I)

                If Agl.StrCmp(Agl.XNull(Me.Item(Col_PostAc, J).Value), "|PARTY|") Then
                    LedgAry(I).SubCode = Me.AgPostingPartyAc
                Else
                    LedgAry(I).SubCode = Agl.XNull(Me.Item(Col_PostAc, J).Value)
                End If

                If Agl.StrCmp(Me.Item(Col_DrCr, J).Value, "Dr") Then
                    If Val(Me.Item(Col_Amount, J).Value) >= 0 Then
                        LedgAry(I).AmtDr = Val(Me.Item(Col_Amount, J).Value)
                    Else
                        LedgAry(I).AmtCr = Math.Abs(Val(Me.Item(Col_Amount, J).Value))
                    End If
                ElseIf Agl.StrCmp(Me.Item(Col_DrCr, J).Value, "Cr") Then
                    If Val(Me.Item(Col_Amount, J).Value) >= 0 Then
                        LedgAry(I).AmtCr = Val(Me.Item(Col_Amount, J).Value)
                    Else
                        LedgAry(I).AmtDr = Math.Abs(Val(Me.Item(Col_Amount, J).Value))
                    End If
                End If

                LedgAry(I).Narration = mNarr


            End If


        Next


        'End If

        If Me.AgPaidMAount > 0 Then
            I = UBound(LedgAry) + 1
            ReDim Preserve LedgAry(I)
            LedgAry(I).AmtDr = Val(Me.AgPaidMAount)
            LedgAry(I).SubCode = Me.AgPaidAc
            LedgAry(I).Narration = mNarr
            I = UBound(LedgAry) + 1
            ReDim Preserve LedgAry(I)
            LedgAry(I).AmtCr = Val(Me.AgPaidMAount)
            LedgAry(I).SubCode = Me.AgPostingPartyAc
            LedgAry(I).Narration = mNarr
        End If



        PostingLedgAry = LedgAry

        'If Agl.LedgerPost(Agl.MidStr(Topctrl1.Mode, 0, 1), LedgAry, Agl.GCn, Agl.ECmd, mSearchCode, CDate(TxtV_Date.Text), Agl.PubUserName, Agl.PubLoginDate, mCommonNarr, , Agl.Gcn_ConnectionString) = False Then
        '    AccountPosting = False : Err.Raise(1, , "Error in Ledger Posting")
        'End If

    End Function
End Class

