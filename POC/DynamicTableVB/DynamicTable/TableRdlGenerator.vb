Imports System
Imports System.Data
Imports System.Collections.Generic
Imports System.Text

Namespace DynamicTable

    Public Class TableRdlGenerator

        Private m_fields As List(Of String)

        Public Property Fields() As List(Of String)
            Get
                Return m_fields
            End Get
            Set(ByVal value As List(Of String))
                m_fields = value
            End Set
        End Property

        Public Function CreateTable() As Rdl.TableType
            Dim table As New Rdl.TableType()
            table.Name = "Table1"
            table.Items = New Object() {CreateTableColumns(), CreateHeader(), CreateDetails()}
            table.ItemsElementName = New Rdl.ItemsChoiceType21() {Rdl.ItemsChoiceType21.TableColumns, Rdl.ItemsChoiceType21.Header, Rdl.ItemsChoiceType21.Details}
            Return table
        End Function

        Private Function CreateHeader() As Rdl.HeaderType
            Dim header As New Rdl.HeaderType()
            header.Items = New Object() {CreateHeaderTableRows()}
            header.ItemsElementName = New Rdl.ItemsChoiceType20() {Rdl.ItemsChoiceType20.TableRows}
            Return header
        End Function

        Private Function CreateHeaderTableRows() As Rdl.TableRowsType
            Dim headerTableRows As New Rdl.TableRowsType()
            headerTableRows.TableRow = New Rdl.TableRowType() {CreateHeaderTableRow()}
            Return headerTableRows
        End Function

        Private Function CreateHeaderTableRow() As Rdl.TableRowType
            Dim headerTableRow As New Rdl.TableRowType()
            headerTableRow.Items = New Object() {CreateHeaderTableCells(), "0.25in"}
            Return headerTableRow
        End Function

        Private Function CreateHeaderTableCells() As Rdl.TableCellsType
            Dim headerTableCells As New Rdl.TableCellsType()
            headerTableCells.TableCell = New Rdl.TableCellType(m_fields.Count) {}
            Dim i As Integer
            For i = 0 To m_fields.Count - 1
                headerTableCells.TableCell(i) = CreateHeaderTableCell(m_fields(i))
            Next i
            Return headerTableCells
        End Function

        Private Function CreateHeaderTableCell(ByVal fieldName As String) As Rdl.TableCellType
            Dim headerTableCell As New Rdl.TableCellType()
            headerTableCell.Items = New Object() {CreateHeaderTableCellReportItems(fieldName)}
            Return headerTableCell
        End Function

        Private Function CreateHeaderTableCellReportItems(ByVal fieldName As String) As Rdl.ReportItemsType
            Dim headerTableCellReportItems As New Rdl.ReportItemsType()
            headerTableCellReportItems.Items = New Object() {CreateHeaderTableCellTextbox(fieldName)}
            Return headerTableCellReportItems
        End Function

        Private Function CreateHeaderTableCellTextbox(ByVal fieldName As String) As Rdl.TextboxType
            Dim headerTableCellTextbox As New Rdl.TextboxType()
            headerTableCellTextbox.Name = fieldName + "_Header"
            headerTableCellTextbox.Items = New Object() {fieldName, CreateHeaderTableCellTextboxStyle(), True}
            headerTableCellTextbox.ItemsElementName = New Rdl.ItemsChoiceType14() {Rdl.ItemsChoiceType14.Value, Rdl.ItemsChoiceType14.Style, Rdl.ItemsChoiceType14.CanGrow}
            Return headerTableCellTextbox
        End Function

        Private Function CreateHeaderTableCellTextboxStyle() As Rdl.StyleType
            Dim headerTableCellTextboxStyle As New Rdl.StyleType()
            headerTableCellTextboxStyle.Items = New Object() {"700", "14pt"}
            headerTableCellTextboxStyle.ItemsElementName = New Rdl.ItemsChoiceType5() {Rdl.ItemsChoiceType5.FontWeight, Rdl.ItemsChoiceType5.FontSize}
            Return headerTableCellTextboxStyle
        End Function

        Private Function CreateDetails() As Rdl.DetailsType
            Dim details As New Rdl.DetailsType()
            details.Items = New Object() {CreateTableRows()}
            Return details
        End Function

        Private Function CreateTableRows() As Rdl.TableRowsType
            Dim tableRows As New Rdl.TableRowsType()
            tableRows.TableRow = New Rdl.TableRowType() {CreateTableRow()}
            Return tableRows
        End Function

        Private Function CreateTableRow() As Rdl.TableRowType
            Dim tableRow As New Rdl.TableRowType()
            tableRow.Items = New Object() {CreateTableCells(), "0.25in"}
            Return tableRow
        End Function

        Private Function CreateTableCells() As Rdl.TableCellsType
            Dim tableCells As New Rdl.TableCellsType()
            tableCells.TableCell = New Rdl.TableCellType(m_fields.Count) {}
            Dim i As Integer
            For i = 0 To m_fields.Count - 1
                tableCells.TableCell(i) = CreateTableCell(m_fields(i))
            Next i
            Return tableCells
        End Function

        Private Function CreateTableCell(ByVal fieldName As String) As Rdl.TableCellType
            Dim tableCell As New Rdl.TableCellType()
            tableCell.Items = New Object() {CreateTableCellReportItems(fieldName)}
            Return tableCell
        End Function

        Private Function CreateTableCellReportItems(ByVal fieldName As String) As Rdl.ReportItemsType
            Dim reportItems As New Rdl.ReportItemsType()
            reportItems.Items = New Object() {CreateTableCellTextbox(fieldName)}
            Return reportItems
        End Function

        Private Function CreateTableCellTextbox(ByVal fieldName As String) As Rdl.TextboxType
            Dim textbox As New Rdl.TextboxType()
            textbox.Name = fieldName
            textbox.Items = New Object() {"=Fields!" + fieldName + ".Value", CreateTableCellTextboxStyle(), True}
            textbox.ItemsElementName = New Rdl.ItemsChoiceType14() {Rdl.ItemsChoiceType14.Value, Rdl.ItemsChoiceType14.Style, Rdl.ItemsChoiceType14.CanGrow}
            Return textbox
        End Function

        Private Function CreateTableCellTextboxStyle() As Rdl.StyleType
            Dim style As New Rdl.StyleType()
            style.Items = New Object() {"=iif(RowNumber(Nothing) mod 2, ""AliceBlue"", ""White"")", "Left"}
            style.ItemsElementName = New Rdl.ItemsChoiceType5() {Rdl.ItemsChoiceType5.BackgroundColor, Rdl.ItemsChoiceType5.TextAlign}
            Return style
        End Function

        Private Function CreateTableColumns() As Rdl.TableColumnsType
            Dim tableColumns As New Rdl.TableColumnsType()
            tableColumns.TableColumn = New Rdl.TableColumnType(m_fields.Count) {}
            Dim i As Integer
            For i = 0 To m_fields.Count - 1
                tableColumns.TableColumn(i) = CreateTableColumn()
            Next i
            Return tableColumns
        End Function

        Private Function CreateTableColumn() As Rdl.TableColumnType
            Dim tableColumn As New Rdl.TableColumnType()
            tableColumn.Items = New Object() {"2in"}
            Return tableColumn
        End Function
    End Class
End Namespace