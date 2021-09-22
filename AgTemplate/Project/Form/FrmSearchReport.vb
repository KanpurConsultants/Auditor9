Imports System.IO
Imports System.Xml.Serialization
Imports Microsoft.Reporting.WinForms
Public Class FrmSearchReport
    Dim FSMain As FrmReportWindow
    Dim SCFMain As ClsStructure.StrucColumnFormating

    Public Sub New(ByVal DTDisplayVar As DataTable, ByVal FSVar As FrmReportWindow)
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call.

        Dim MSMain As New MemoryStream()
        FSMain = FSVar
        FWriteXml(MSMain)
        MSMain.Position = 0

        Me.RVMain.Reset()
        Me.RVMain.LocalReport.LoadReportDefinition(CType(MSMain, System.IO.Stream))
        Me.RVMain.LocalReport.DataSources.Add(New ReportDataSource("Search", DTDisplayVar))
        Me.RVMain.RefreshReport()
        Me.RVMain.Update()
    End Sub

    Public Sub FWriteXml(ByVal StmStream As Stream)
        Dim XsSerializer As New XmlSerializer(GetType(Rdl.Report))
        XsSerializer.Serialize(StmStream, FCreateReport())
    End Sub

    Private Function FCreateReport() As Rdl.Report
        Dim Report As New Rdl.Report()
        Report.Items = New Object() {FCreateDataSources(), FCreateBody(), FCreateDataSets(), "6.5in", "0.50in", "0.50in", "0.50in", "0.50in"}
        Report.ItemsElementName = New Rdl.ItemsChoiceType37() {Rdl.ItemsChoiceType37.DataSources, Rdl.ItemsChoiceType37.Body, Rdl.ItemsChoiceType37.DataSets, Rdl.ItemsChoiceType37.Width, Rdl.ItemsChoiceType37.LeftMargin, Rdl.ItemsChoiceType37.RightMargin, Rdl.ItemsChoiceType37.TopMargin, Rdl.ItemsChoiceType37.BottomMargin}
        Return Report
    End Function

    Private Function FCreateDataSources() As Rdl.DataSourcesType
        Dim DTSSDataSources As New Rdl.DataSourcesType()
        Dim DTSDataSource As New Rdl.DataSourceType()
        Dim CPTConnectionProperties As New Rdl.ConnectionPropertiesType()

        CPTConnectionProperties.Items = New Object() {"", "SQL"}
        CPTConnectionProperties.ItemsElementName = New Rdl.ItemsChoiceType() {Rdl.ItemsChoiceType.ConnectString, Rdl.ItemsChoiceType.DataProvider}

        DTSDataSource.Name = "Search"
        DTSDataSource.Items = New Object() {CPTConnectionProperties}

        DTSSDataSources.DataSource = New Rdl.DataSourceType() {DTSDataSource}
        Return DTSSDataSources
    End Function

    Private Function FCreateBody() As Rdl.BodyType
        Dim body As New Rdl.BodyType()
        body.Items = New Object() {CreateReportItems(), "0.25in"}
        body.ItemsElementName = New Rdl.ItemsChoiceType30() {Rdl.ItemsChoiceType30.ReportItems, Rdl.ItemsChoiceType30.Height}
        Return body
    End Function

    Private Function CreateReportItems() As Rdl.ReportItemsType
        Dim reportItems As New Rdl.ReportItemsType()
        reportItems.Items = New Object() {FCreateTableCellTextbox(0, ClsStructure.SectionType.ReportHeader, "ReportHeader"), FCreateTable()}
        Return reportItems
    End Function

    Private Function FCreateDataSets() As Rdl.DataSetsType
        Dim FTSFields As New Rdl.FieldsType()
        Dim FTField As Rdl.FieldType
        Dim DTSSDataSets As New Rdl.DataSetsType()
        Dim DTSDataSet As New Rdl.DataSetType()
        Dim QTQuery As New Rdl.QueryType()
        Dim I As Integer

        FTSFields.Field = New Rdl.FieldType(FSMain.DGL1.Columns.Count - 1) {}

        For I = 0 To FSMain.DGL1.Columns.Count - 1
            SCFMain = DirectCast(FSMain.DGL1.Columns(FGrdColumnIndex(I)).HeaderCell.Tag, ClsStructure.StrucColumnFormating)

            FTField = New Rdl.FieldType
            FTField.Name = Replace(FSMain.DGL1.Columns(FGrdColumnIndex(I)).Name, " ", "")

            FTField.Items = New Object() {FTField.Name}
            FTField.ItemsElementName = New Rdl.ItemsChoiceType1() {Rdl.ItemsChoiceType1.DataField}
            FTSFields.Field(I) = FTField
        Next I

        QTQuery.Items = New Object() {"Search", ""}
        QTQuery.ItemsElementName = New Rdl.ItemsChoiceType2() {Rdl.ItemsChoiceType2.DataSourceName, Rdl.ItemsChoiceType2.CommandText}


        DTSDataSet.Name = "Search"
        DTSDataSet.Items = New Object() {QTQuery, FTSFields}

        DTSSDataSets.DataSet = New Rdl.DataSetType() {DTSDataSet}

        FCreateDataSets = DTSSDataSets
    End Function

#Region "Create Common Function "
    Private Function FCreateTableRows(ByVal SectionType As ClsStructure.SectionType, ByVal StrSectionName As String) As Rdl.TableRowsType
        Dim TRType As New Rdl.TableRowsType()
        TRType.TableRow = New Rdl.TableRowType(0) {}
        TRType.TableRow(0) = FCreateTableRow(SectionType, StrSectionName)
        Return TRType
    End Function

    Private Function FCreateTableRow(ByVal SectionType As ClsStructure.SectionType, ByVal StrSectionName As String) As Rdl.TableRowType
        Dim TblRWType As New Rdl.TableRowType()
        Select Case SectionType
            Case ClsStructure.SectionType.Header
                TblRWType.Items = New Object() {FCreateTableCells(SectionType, StrSectionName), "0.25in"}
            Case ClsStructure.SectionType.Footer
                TblRWType.Items = New Object() {FCreateTableCells(SectionType, StrSectionName), "0.25in"}
            Case ClsStructure.SectionType.Detail
                TblRWType.Items = New Object() {FCreateTableCells(SectionType, StrSectionName), "0.25in", FVisiblityType("MainReporting")}
        End Select
        Return TblRWType
    End Function

    Private Function FCreateTableCells(ByVal SectionType As ClsStructure.SectionType, ByVal StrSectionName As String) As Rdl.TableCellsType
        Dim TblCellsType As New Rdl.TableCellsType()
        TblCellsType.TableCell = New Rdl.TableCellType(FSMain.DGL1.Columns.Count - 1) {}
        Dim i As Integer
        For i = 0 To FSMain.DGL1.Columns.Count - 1
            SCFMain = DirectCast(FSMain.DGL1.Columns(i).HeaderCell.Tag, ClsStructure.StrucColumnFormating)
            TblCellsType.TableCell(i) = FCreateTableCell(i, SectionType, StrSectionName)
        Next
        Return TblCellsType
    End Function

    Private Function FCreateTableCell(ByVal ColumnIndex As Integer, ByVal SectionType As ClsStructure.SectionType, ByVal StrSectionName As String) As Rdl.TableCellType
        Dim TblCellType As New Rdl.TableCellType()
        TblCellType.Items = New Object() {FCreateTableCellReportItems(ColumnIndex, SectionType, StrSectionName)}
        Return TblCellType
    End Function

    Private Function FCreateTableCellReportItems(ByVal ColumnIndex As Integer, ByVal SectionType As ClsStructure.SectionType, ByVal StrSectionName As String) As Rdl.ReportItemsType
        Dim RIType As New Rdl.ReportItemsType()
        RIType.Items = New Object() {FCreateTableCellTextbox(ColumnIndex, SectionType, StrSectionName)}
        Return RIType
    End Function

    Private Function FCreateTableCellTextbox(ByVal ColumnIndex As Integer, ByVal SectionType As ClsStructure.SectionType, ByVal StrSectionName As String) As Rdl.TextboxType
        Dim TxtType As New Rdl.TextboxType()
        Dim I As Short
        Dim DblWidth As Double

        Select Case SectionType
            Case ClsStructure.SectionType.ReportHeader
                TxtType.Name = "MainReporting"
                For I = 0 To FSMain.DGL1.Columns.Count - 1
                    SCFMain = DirectCast(FSMain.DGL1.Columns(I).HeaderCell.Tag, ClsStructure.StrucColumnFormating)
                    If SCFMain.StrHideColumn = "N" Then
                        DblWidth += SCFMain.IntWidth * 1 / 100
                    End If
                Next

                DblWidth = Trim(Math.Round(DblWidth, 2))
                TxtType.Items = New Object() {FSMain.Text, CreateTableCellTextboxStyle(ColumnIndex), True, Trim(DblWidth) + "in"}
                TxtType.ItemsElementName = New Rdl.ItemsChoiceType14() {Rdl.ItemsChoiceType14.Value, Rdl.ItemsChoiceType14.Style, Rdl.ItemsChoiceType14.CanGrow, Rdl.ItemsChoiceType14.Width}
            Case ClsStructure.SectionType.Header
                SCFMain = DirectCast(FSMain.DGL1.Columns(FGrdColumnIndex(ColumnIndex)).HeaderCell.Tag, ClsStructure.StrucColumnFormating)
                TxtType.Name = Replace(FSMain.DGL1.Columns(FGrdColumnIndex(ColumnIndex)).Name, " ", "") + StrSectionName
                TxtType.Items = New Object() {Replace(FSMain.DGL1.Columns(FGrdColumnIndex(ColumnIndex)).Name, "_", " "), CreateHeaderTableCellTextboxStyle(FSMain.DGL1.Columns(ColumnIndex).DisplayIndex, SectionType), IIf(SCFMain.StrWrapColumn = "Y", True, False)}
                TxtType.ItemsElementName = New Rdl.ItemsChoiceType14() {Rdl.ItemsChoiceType14.Value, Rdl.ItemsChoiceType14.Style, Rdl.ItemsChoiceType14.CanGrow}
            Case ClsStructure.SectionType.Detail
                SCFMain = DirectCast(FSMain.DGL1.Columns(FGrdColumnIndex(ColumnIndex)).HeaderCell.Tag, ClsStructure.StrucColumnFormating)
                TxtType.Name = Replace(FSMain.DGL1.Columns(FGrdColumnIndex(ColumnIndex)).Name, " ", "") + StrSectionName
                TxtType.Items = New Object() {"=Fields!" + FSMain.DGL1.Columns(FGrdColumnIndex(ColumnIndex)).Name + ".Value", CreateHeaderTableCellTextboxStyle(FSMain.DGL1.Columns(ColumnIndex).DisplayIndex, SectionType), IIf(SCFMain.StrWrapColumn = "Y", True, False)}
                TxtType.ItemsElementName = New Rdl.ItemsChoiceType14() {Rdl.ItemsChoiceType14.Value, Rdl.ItemsChoiceType14.Style, Rdl.ItemsChoiceType14.CanGrow}
            Case ClsStructure.SectionType.Footer
                SCFMain = DirectCast(FSMain.DGL1.Columns(FGrdColumnIndex(ColumnIndex)).HeaderCell.Tag, ClsStructure.StrucColumnFormating)
                TxtType.Name = Replace(FSMain.DGL1.Columns(FGrdColumnIndex(ColumnIndex)).Name, " ", "") + StrSectionName
                TxtType.Items = New Object() {"", CreateHeaderTableCellTextboxStyle(ColumnIndex, ClsStructure.SectionType.Footer), IIf(SCFMain.StrWrapColumn = "Y", True, False)}
                TxtType.ItemsElementName = New Rdl.ItemsChoiceType14() {Rdl.ItemsChoiceType14.Value, Rdl.ItemsChoiceType14.Style, Rdl.ItemsChoiceType14.CanGrow}
        End Select
        Return TxtType
    End Function

    Private Function FGrdColumnIndex(ByVal IntColDisplayIndex As Integer) As Integer
        Dim I As Integer = 0
        For I = 0 To FSMain.DGL1.Columns.Count - 1
            If IntColDisplayIndex = FSMain.DGL1.Columns(I).DisplayIndex Then
                FGrdColumnIndex = FSMain.DGL1.Columns(I).Index
                Exit Function
            End If
        Next
    End Function

    Private Function CreateHeaderTableCellTextboxStyle(ByVal ColumnIndex As Integer, ByVal SectionType As ClsStructure.SectionType) As Rdl.StyleType
        Dim SType As New Rdl.StyleType()
        Dim bAlignment As String = "Left"


        Select Case FSMain.DGL1.Columns(FSMain.DGL1.Columns(FGrdColumnIndex(ColumnIndex)).Name).ValueType.ToString.ToUpper
            Case "SYSTEM.Int32", "System.Int64", "SYSTEM.DECIMAL", "SYSTEM.DOUBLE"
                bAlignment = "Right"
        End Select

        Select Case SectionType
            Case ClsStructure.SectionType.ReportHeader
                SType.Items = New Object() {"White", bAlignment, "Tahoma", "15pt", "700"}
                SType.ItemsElementName = New Rdl.ItemsChoiceType5() {Rdl.ItemsChoiceType5.BackgroundColor, Rdl.ItemsChoiceType5.TextAlign, Rdl.ItemsChoiceType5.FontFamily, Rdl.ItemsChoiceType5.FontSize, Rdl.ItemsChoiceType5.FontWeight}
            Case ClsStructure.SectionType.Header
                SType.Items = New Object() {"700", "8pt", "Tahoma", "#dddddd", bAlignment, FBorderSTyle(ColumnIndex, SectionType)}
                SType.ItemsElementName = New Rdl.ItemsChoiceType5() {Rdl.ItemsChoiceType5.FontWeight, Rdl.ItemsChoiceType5.FontSize, Rdl.ItemsChoiceType5.FontFamily, Rdl.ItemsChoiceType5.BackgroundColor, Rdl.ItemsChoiceType5.TextAlign, Rdl.ItemsChoiceType5.BorderStyle}
            Case ClsStructure.SectionType.Detail
                SType.Items = New Object() {"300", "8pt", "Tahoma", "White", bAlignment, FBorderSTyle(ColumnIndex, SectionType)}
                SType.ItemsElementName = New Rdl.ItemsChoiceType5() {Rdl.ItemsChoiceType5.FontWeight, Rdl.ItemsChoiceType5.FontSize, Rdl.ItemsChoiceType5.FontFamily, Rdl.ItemsChoiceType5.BackgroundColor, Rdl.ItemsChoiceType5.TextAlign, Rdl.ItemsChoiceType5.BorderStyle}
            Case ClsStructure.SectionType.Footer
                SType.Items = New Object() {"700", "8pt", "Tahoma", "WhiteSmoke", bAlignment, FBorderSTyle(ColumnIndex, SectionType)}
                SType.ItemsElementName = New Rdl.ItemsChoiceType5() {Rdl.ItemsChoiceType5.FontWeight, Rdl.ItemsChoiceType5.FontSize, Rdl.ItemsChoiceType5.FontFamily, Rdl.ItemsChoiceType5.BackgroundColor, Rdl.ItemsChoiceType5.TextAlign, Rdl.ItemsChoiceType5.BorderStyle}
        End Select
        Return SType
    End Function

    Private Function FBorderSTyle(ByVal ColumnIndex As Integer, ByVal SectionType As ClsStructure.SectionType) As Rdl.BorderColorStyleWidthType
        Dim BSType As New Rdl.BorderColorStyleWidthType
        BSType.Items = New Object() {"Solid", "Solid", "Solid", "Solid", "Solid"}
        BSType.ItemsElementName = New Rdl.ItemsChoiceType3() {Rdl.ItemsChoiceType3.Top, Rdl.ItemsChoiceType3.Bottom, Rdl.ItemsChoiceType3.Left, Rdl.ItemsChoiceType3.Right, Rdl.ItemsChoiceType3.Default}
        Return BSType
    End Function

    Private Function CreateTableCellTextboxStyle(ByVal ColumnIndex As Integer) As Rdl.StyleType
        Dim style As New Rdl.StyleType()
        style.Items = New Object() {"White", "Center", "Tahoma", "15pt", "700"}

        style.ItemsElementName = New Rdl.ItemsChoiceType5() {Rdl.ItemsChoiceType5.BackgroundColor, Rdl.ItemsChoiceType5.TextAlign, Rdl.ItemsChoiceType5.FontFamily, Rdl.ItemsChoiceType5.FontSize, Rdl.ItemsChoiceType5.FontWeight}
        Return style
    End Function
#End Region
   
    Public Function FCreateTable() As Rdl.TableType
        Dim table As New Rdl.TableType()
        table.Name = "Table1"
        table.Items = New Object() {FCreateHeader("PageHeader"), FCreateDetails("Detail"), FCreateFooter("PageFooter"), FCreateTableColumns(), ".25in"}
        table.ItemsElementName = New Rdl.ItemsChoiceType21() {Rdl.ItemsChoiceType21.Header, Rdl.ItemsChoiceType21.Details, Rdl.ItemsChoiceType21.Footer, Rdl.ItemsChoiceType21.TableColumns, Rdl.ItemsChoiceType21.Top}
        Return table
    End Function

#Region "Table"

    Private Function FVisiblityType(ByVal StrName As String) As Rdl.VisibilityType
        Dim VisibilityType As New Rdl.VisibilityType
        VisibilityType.Items = New Object() {StrName}
        VisibilityType.ItemsElementName = New Rdl.ItemsChoiceType9() {Rdl.ItemsChoiceType9.ToggleItem}
        Return VisibilityType
    End Function

    Private Function FCreateTableGroupings() As Rdl.TableGroupsType
        Dim TableGroups As New Rdl.TableGroupsType()
        TableGroups.TableGroup() = New Rdl.TableGroupType(0) {}
        TableGroups.TableGroup(0) = FCreateTableGRoup(0)
        Return TableGroups
    End Function

    Private Function FColumnVisiblityType() As Rdl.VisibilityType
        Dim VisibilityType As New Rdl.VisibilityType
        VisibilityType.Items = New Object() {"true"}
        VisibilityType.ItemsElementName = New Rdl.ItemsChoiceType9() {Rdl.ItemsChoiceType9.Hidden}
        Return VisibilityType
    End Function

    Private Function FCreateTableGRoup(ByVal i As Integer) As Rdl.TableGroupType
        Dim tableColumn As New Rdl.TableGroupType()
        tableColumn.Items = New Object() {FCreateGroup(i), FCreateFooter("GroupFooter")}
        Return tableColumn
    End Function

    Private Function FCreateGroup(ByVal i As Integer) As Rdl.GroupingType
        Dim GRoupType As New Rdl.GroupingType()
        GRoupType.Name = "Table_Group" & i
        GRoupType.Items = New Object() {FCreateGroupExpression(i)}
        GRoupType.ItemsElementName = New Rdl.ItemsChoiceType17() {Rdl.ItemsChoiceType17.GroupExpressions}
        Return GRoupType
    End Function

    Private Function FCreateGroupExpression(ByVal i As Integer) As Rdl.GroupExpressionsType
        FCreateGroupExpression = New Rdl.GroupExpressionsType
        FCreateGroupExpression.GroupExpression = New String(0) {}
        FCreateGroupExpression.GroupExpression(0) = "=Fields!" + Replace(FSMain.DGL1.Columns(i).Name, " ", "") + ".Value"
        Return FCreateGroupExpression
    End Function

    Private Function FCreateTableColumns() As Rdl.TableColumnsType
        Dim TCSTableColumns As New Rdl.TableColumnsType()
        Dim TCTableColumn As Rdl.TableColumnType
        Dim I As Integer
        TCSTableColumns.TableColumn = New Rdl.TableColumnType(FSMain.DGL1.Columns.Count - 1) {}
        For I = 0 To FSMain.DGL1.Columns.Count - 1
            SCFMain = DirectCast(FSMain.DGL1.Columns(FGrdColumnIndex(I)).HeaderCell.Tag, ClsStructure.StrucColumnFormating)
            TCTableColumn = New Rdl.TableColumnType()
            If SCFMain.StrHideColumn = "Y" Then
                TCTableColumn.Items = New Object() {Trim(Math.Round(SCFMain.IntWidth * 1 / 100, 2)) & "in", FColumnVisiblityType()}
            Else
                TCTableColumn.Items = New Object() {Trim(Math.Round(SCFMain.IntWidth * 1 / 100, 2)) & "in"}
            End If
            TCSTableColumns.TableColumn(I) = TCTableColumn
        Next
        Return TCSTableColumns
    End Function
#End Region

#Region "Create Section"
    Private Function FCreateHeader(Optional ByVal StrSectionName As String = "") As Rdl.HeaderType
        Dim HDType As New Rdl.HeaderType()
        HDType.Items = New Object() {FCreateTableRows(ClsStructure.SectionType.Header, StrSectionName), True, True}
        HDType.ItemsElementName = New Rdl.ItemsChoiceType20() {Rdl.ItemsChoiceType20.TableRows, Rdl.ItemsChoiceType20.RepeatOnNewPage, Rdl.ItemsChoiceType20.FixedHeader}
        Return HDType
    End Function

    Private Function FCreateDetails(Optional ByVal StrSectionName As String = "") As Rdl.DetailsType
        Dim DTType As New Rdl.DetailsType()
        DTType.Items = New Object() {FCreateTableRows(ClsStructure.SectionType.Detail, StrSectionName)}
        Return DTType
    End Function

    Private Function FCreateFooter(Optional ByVal StrSectionName As String = "") As Rdl.FooterType
        Dim FTType As New Rdl.FooterType()
        FTType.Items = New Object() {FCreateTableRows(ClsStructure.SectionType.Footer, StrSectionName), False}
        Return FTType
    End Function
#End Region

    Private Sub FrmReportWindow_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class
