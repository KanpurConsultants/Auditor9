Imports System.Drawing.Printing
Imports System.IO
Imports System.Text.RegularExpressions
Imports Microsoft.Reporting.WinForms

Public Class FrmRepPrint

    Dim dsReport As New DataSet()
    Dim dsReportTotal As New DataSet()
    Dim mReportStr As String = ""
    Dim I As Integer = 0
    Dim mDGL1 As AgControls.AgDataGrid
    Dim mDGL2 As AgControls.AgDataGrid
    Dim mFilterGrid As AgControls.AgDataGrid
    Dim mReportFontSize As Integer = 8
    Dim mTotalColumnWidth As Double = 0
    Dim A4PortraitSizeWidth As Integer = 850
    Dim A4LandscapeSizeWidth As Integer = 1100
    Dim mReportTitle As String = "", mReportSubTitle As String = ""
    Dim mShowReportFooter As Boolean = True
    Dim AgL As AgLibrary.ClsMain
    Dim ds_font As New DataSet

    Public Sub New(ByVal AgLibVar As ClsMain)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AgL = AgLibVar

    End Sub

    Public Property DGL1() As AgControls.AgDataGrid
        Get
            DGL1 = mDGL1
        End Get
        Set(ByVal value As AgControls.AgDataGrid)
            mDGL1 = value
        End Set
    End Property
    Public Property DGL2() As AgControls.AgDataGrid
        Get
            DGL2 = mDGL2
        End Get
        Set(ByVal value As AgControls.AgDataGrid)
            mDGL2 = value
        End Set
    End Property
    Public Property FilterGrid() As AgControls.AgDataGrid
        Get
            FilterGrid = mFilterGrid
        End Get
        Set(ByVal value As AgControls.AgDataGrid)
            mFilterGrid = value
        End Set
    End Property
    Public Property ReportTitle() As String
        Get
            ReportTitle = mReportTitle
        End Get
        Set(ByVal value As String)
            mReportTitle = value
        End Set
    End Property
    Public Property ReportSubTitle() As String
        Get
            ReportSubTitle = mReportSubTitle
        End Get
        Set(ByVal value As String)
            mReportSubTitle = value
        End Set
    End Property
    Public Property ShowReportFooter() As Boolean
        Get
            ShowReportFooter = mShowReportFooter
        End Get
        Set(ByVal value As Boolean)
            mShowReportFooter = value
        End Set
    End Property
    Private Sub FSetPageSetting(ByRef reportViewer1 As ReportViewer)


        For I = 0 To DGL1.Columns.Count - 1
            If DGL1.Columns(I).Visible = True Then
                mTotalColumnWidth += DGL1.Columns(I).Width
            End If
        Next

        Dim ps As New PageSettings()
        ps.Margins = New Margins(40, 10, 20, 20)
        If mTotalColumnWidth - 80 <= A4PortraitSizeWidth Then
            ps.PaperSize = New PaperSize("A4", 850, 1100)
            ps.PaperSize.RawKind = PaperKind.A4
        Else
            ps.Landscape = True
            ps.PaperSize = New PaperSize("A4", 850, 1100)
            ps.PaperSize.RawKind = PaperKind.A4
        End If
        reportViewer1.SetPageSettings(ps)
        reportViewer1.RefreshReport()
    End Sub
    Private Sub FrmReportPrint_Load(sender As Object, e As EventArgs) Handles Me.Load
        reportViewer1.RefreshReport()
        Me.WindowState = FormWindowState.Maximized
    End Sub
    Private Function FGetColumnWidthMultiplier()
        If mTotalColumnWidth <= A4PortraitSizeWidth Then
            Return 0.016666666666666666 * 0.52
        ElseIf mTotalColumnWidth <= A4LandscapeSizeWidth Then
            Return 0.016666666666666666 * 0.52
        Else
            Return 0.016666666666666666 * 0.52
        End If
    End Function
    Private Function GetDataSetFromDataGridView(ByVal dgv As AgControls.AgDataGrid) As DataSet
        Dim ds As New DataSet

        Dim OnlyTickedRecords As Boolean = False
        Dim bCellFontDataTable As DataTable = Nothing

        Try

            If dgv.Columns.Contains("Tick") Then
                If dgv.Name.ToUpper = "DGL1" Then
                    If MsgBox("Do you want to print only ticked records?", vbYesNo) = vbYes Then
                        OnlyTickedRecords = True
                    End If
                End If
            End If



            ' Add Table
            ds.Tables.Add("Table1")
            If dgv.Name = "DGL1" Then ds_font.Tables.Add("Table1")

            ' Add Columns
            Dim col As DataColumn
            For Each dgvCol As DataGridViewColumn In dgv.Columns
                If dgvCol.Visible = True Then
                    col = New DataColumn(dgvCol.Name.Replace(" ", ""))
                    ds.Tables("Table1").Columns.Add(col)
                    If dgv.Name = "DGL1" Then ds_font.Tables("Table1").Columns.Add(col.ColumnName)
                    If dgvCol.ValueType Is Nothing Then
                        col.DataType = GetType(String)
                    Else
                        col.DataType = dgvCol.ValueType
                    End If
                End If
            Next

            'Add Rows from the datagridview
            Dim row_Font As DataRow
            Dim row As DataRow
            For i As Integer = 0 To dgv.Rows.Count - 1
                If OnlyTickedRecords = False Then
                    row = ds.Tables("Table1").Rows.Add
                    If dgv.Name = "DGL1" Then row_Font = ds_font.Tables("Table1").Rows.Add
                Else
                    If dgv.Rows.Item(i).Cells("Tick").Value = "þ" Then
                        row = ds.Tables("Table1").Rows.Add
                        If dgv.Name = "DGL1" Then row_Font = ds_font.Tables("Table1").Rows.Add
                    End If
                End If

                For Each column As DataGridViewColumn In dgv.Columns
                    If column.Visible = True Then
                        If OnlyTickedRecords = False Then
                            If AgL.XNull(dgv.Rows.Item(i).Cells(column.Name).Value).GetType.ToString = "System.Decimal" Or
                            AgL.XNull(dgv.Rows.Item(i).Cells(column.Name).Value).GetType.ToString = "System.Float" Or
                            AgL.XNull(dgv.Rows.Item(i).Cells(column.Name).Value).GetType.ToString = "System.Double" Then
                                If Not column.Name.Contains("Qty") And Not column.Name.Contains("Rate") Then
                                    row.Item(column.Name.Replace(" ", "")) = Math.Round(dgv.Rows.Item(i).Cells(column.Name).Value, 2)
                                    If dgv.Name = "DGL1" Then
                                        If dgv.Rows.Item(i).Cells(column.Name).Style.Font Is Nothing Then
                                            row_Font.Item(column.Name.Replace(" ", "")) = "False"
                                        Else
                                            row_Font.Item(column.Name.Replace(" ", "")) = dgv.Rows.Item(i).Cells(column.Name).Style.Font.Bold
                                        End If
                                    End If
                                Else
                                    row.Item(column.Name.Replace(" ", "")) = dgv.Rows.Item(i).Cells(column.Name).Value

                                    If dgv.Name = "DGL1" Then
                                        If dgv.Rows.Item(i).Cells(column.Name).Style.Font Is Nothing Then
                                            row_Font.Item(column.Name.Replace(" ", "")) = "False"
                                        Else
                                            row_Font.Item(column.Name.Replace(" ", "")) = dgv.Rows.Item(i).Cells(column.Name).Style.Font.Bold
                                        End If
                                    End If
                                End If
                            Else
                                row.Item(column.Name.Replace(" ", "")) = dgv.Rows.Item(i).Cells(column.Name).Value

                                If dgv.Name = "DGL1" Then
                                    If dgv.Rows.Item(i).Cells(column.Name).Style.Font Is Nothing Then
                                        row_Font.Item(column.Name.Replace(" ", "")) = "False"
                                    Else
                                        row_Font.Item(column.Name.Replace(" ", "")) = dgv.Rows.Item(i).Cells(column.Name).Style.Font.Bold
                                    End If
                                End If
                            End If
                        Else
                            If dgv.Rows.Item(i).Cells("Tick").Value = "þ" Then
                                If AgL.XNull(dgv.Rows.Item(i).Cells(column.Name).Value).GetType.ToString = "System.Decimal" Or
                                AgL.XNull(dgv.Rows.Item(i).Cells(column.Name).Value).GetType.ToString = "System.Float" Or
                                AgL.XNull(dgv.Rows.Item(i).Cells(column.Name).Value).GetType.ToString = "System.Double" Then
                                    If Not column.Name.Contains("Qty") And Not column.Name.Contains("Rate") Then
                                        row.Item(column.Name.Replace(" ", "")) = Math.Round(dgv.Rows.Item(i).Cells(column.Name).Value, 2)

                                        If dgv.Name = "DGL1" Then
                                            If dgv.Rows.Item(i).Cells(column.Name).Style.Font Is Nothing Then
                                                row_Font.Item(column.Name.Replace(" ", "")) = "False"
                                            Else
                                                row_Font.Item(column.Name.Replace(" ", "")) = dgv.Rows.Item(i).Cells(column.Name).Style.Font.Bold
                                            End If
                                        End If
                                    Else
                                        row.Item(column.Name.Replace(" ", "")) = dgv.Rows.Item(i).Cells(column.Name).Value

                                        If dgv.Name = "DGL1" Then
                                            If dgv.Rows.Item(i).Cells(column.Name).Style.Font Is Nothing Then
                                                row_Font.Item(column.Name.Replace(" ", "")) = "False"
                                            Else
                                                row_Font.Item(column.Name.Replace(" ", "")) = dgv.Rows.Item(i).Cells(column.Name).Style.Font.Bold
                                            End If
                                        End If
                                    End If
                                Else
                                    row.Item(column.Name.Replace(" ", "")) = dgv.Rows.Item(i).Cells(column.Name).Value

                                    If dgv.Name = "DGL1" Then
                                        If dgv.Rows.Item(i).Cells(column.Name).Style.Font Is Nothing Then
                                            row_Font.Item(column.Name.Replace(" ", "")) = "False"
                                        Else
                                            row_Font.Item(column.Name.Replace(" ", "")) = dgv.Rows.Item(i).Cells(column.Name).Style.Font.Bold
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
            Next

            Return ds
        Catch ex As Exception
            MsgBox("CRITICAL ERROR : Exception caught while converting dataGridView to DataSet (dgvtods).. " & Chr(10) & ex.Message)
            Return Nothing
        End Try
    End Function
    Public Sub ProcessPrint(ByRef reportViewer1 As ReportViewer)
        FSetPageSetting(reportViewer1)
        reportViewer1.Visible = True
        Dim id As Integer = 0
        reportViewer1.ProcessingMode = ProcessingMode.Local
        dsReport = GetDataSetFromDataGridView(mDGL1)
        dsReportTotal = GetDataSetFromDataGridView(mDGL2)

        CreateRDLFile("GridReport", GetReportStr(reportViewer1))
        reportViewer1.LocalReport.ReportPath = AgL.PubReportPath + "GridReport.rdlc"

        If (dsReport.Tables(0).Rows.Count > 0) Then
            Dim rds As New ReportDataSource("DsMain", dsReport.Tables(0))
            reportViewer1.LocalReport.DataSources.Clear()
            reportViewer1.LocalReport.DataSources.Add(rds)

            reportViewer1.LocalReport.SetParameters(New ReportParameter("ReportTitle", mReportTitle))
            reportViewer1.LocalReport.SetParameters(New ReportParameter("ReportSubtitle", mReportSubTitle))

            If mFilterGrid IsNot Nothing Then
                Dim ParameterCnt As Integer = 1
                For I = 0 To mFilterGrid.Rows.Count - 1
                    If I <= 11 Then
                        If mFilterGrid(FrmRepDisplay.GFilter, I).Value <> "" And
                            mFilterGrid(FrmRepDisplay.GFilter, I).Value IsNot Nothing And
                            mFilterGrid.Rows(I).Visible = True And
                            Not mFilterGrid(FrmRepDisplay.GFieldName, I).Value.ToString.Contains("Show ") And
                            mFilterGrid(FrmRepDisplay.GFilter, I).Value <> "All" Then
                            reportViewer1.LocalReport.SetParameters(New ReportParameter("FilterStr" + ParameterCnt.ToString(),
                                            mFilterGrid(FrmRepDisplay.GFieldName, I).Value.ToString + " : " + mFilterGrid(FrmRepDisplay.GFilter, I).Value.ToString))
                            ParameterCnt += 1
                        End If
                    End If
                Next
            End If



            reportViewer1.LocalReport.Refresh()
            reportViewer1.RefreshReport()
        End If
    End Sub




    Public Sub CreateRDLFile(FileName As String, FileCode As String)
        Dim FileFullPath As String = ""
        FileFullPath = AgL.PubReportPath + FileName + ".rdlc"
        If File.Exists(FileFullPath) Then
            File.Delete(FileFullPath)
        End If

        Dim sw As StreamWriter = File.CreateText(FileFullPath)
        sw.AutoFlush = True
        sw.Write(FileCode)
        sw.Close()
    End Sub
    Private Function GetReportStr(ByRef reportViewer1 As ReportViewer) As String


        FWriteDataSource()
        FWriteDataSet()
        FWriteReportParamters()
        FWriteColumns()
        FWriteHeaderRow()
        FWriteDetailRow()
        FWriteFooterRow()
        FWriteTableColumnHirarichy()
        FWriteFilters()
        FWritePageProperties(reportViewer1)

        Return mReportStr
    End Function
    Private Function FGetColumnsAlignment(DataTaleColumnName As String) As String
        Dim I As Integer = 0
        Dim retStr As String = "Left"
        For I = 0 To DGL1.Columns.Count - 1
            If DGL1.Columns(I).Name.Replace(" ", "") = DataTaleColumnName Then
                retStr = IIf(DGL1.Columns(I).DefaultCellStyle.Alignment = DataGridViewContentAlignment.NotSet, "Left", "Right")
                Exit For
            End If
        Next
        Return retStr
    End Function

    Private Sub FWriteDataSource()
        mReportStr += "<?xml version=""1.0"" encoding=""utf-8""?>
        <Report xmlns:rd=""http//schemas.microsoft.com/SQLServer/reporting/reportdesigner"" xmlns=""http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition"">
          <DataSources>
            <DataSource Name=""dsReport"">
              <ConnectionProperties>
                <DataProvider>System.Data.DataSet</DataProvider>
                <ConnectString>/* Local Connection */</ConnectString>
              </ConnectionProperties>
              <rd:DataSourceID> 129c7661-a7a6-4c74-ac60-3066e46fc40d</rd:DataSourceID>
            </DataSource>
          </DataSources>"
    End Sub
    Private Sub FWriteDataSet()
        mReportStr += "<DataSets>
            <DataSet Name=""DsMain"">
              <Fields>"

        For I = 0 To dsReport.Tables(0).Columns.Count - 1
            mReportStr += "<Field Name=""" & dsReport.Tables(0).Columns(I).ColumnName & """>
                  <DataField>" & dsReport.Tables(0).Columns(I).ColumnName & "</DataField>
                  <rd:TypeName>" & dsReport.Tables(0).Columns(I).DataType.ToString & "</rd:TypeName>
                </Field>"
        Next

        mReportStr += "</Fields>
              <Query>
                <DataSourceName>dsReport</DataSourceName>
                <CommandText>/* Local Query */</CommandText>
              </Query>
              <rd:DataSetInfo>
                <rd:DataSetName>dsReport</rd:DataSetName>
                <rd:SchemaPath>D:\Working Copy\DesktopApp\branches\Developing\Auditor9\Release\Reports\dsReport.xsd</rd:SchemaPath>
                <rd:TableName>PurchaseOrderReport</rd:TableName>
                <rd:TableAdapterFillMethod>Fill</rd:TableAdapterFillMethod>
                <rd:TableAdapterGetDataMethod>GetData</rd:TableAdapterGetDataMethod>
                <rd:TableAdapterName>PurchaseOrderReportTableAdapter</rd:TableAdapterName>
              </rd:DataSetInfo>
            </DataSet>
          </DataSets>"
    End Sub

    Private Sub FWriteReportParamters()
        mReportStr += "<ReportParameters>"
        For I = 1 To 12
            mReportStr += "<ReportParameter Name = ""FilterStr" & I.ToString & """>
                    <DataType>String</DataType>
                    <Nullable>true</Nullable>
                    <AllowBlank>true</AllowBlank>
                    <Prompt>ReportParameter1</Prompt>
                </ReportParameter>"
        Next
        mReportStr += "<ReportParameter Name = ""ReportTitle"">
                    <DataType>String</DataType>
                    <Nullable>true</Nullable>
                    <AllowBlank>true</AllowBlank>
                    <Prompt>ReportParameter1</Prompt>
                </ReportParameter>
                <ReportParameter Name = ""CompanyName"">
                    <DataType>String</DataType>
                    <Nullable>true</Nullable>
                    <AllowBlank>true</AllowBlank>
                    <Prompt>ReportParameter1</Prompt>
                </ReportParameter>
                <ReportParameter Name = ""ReportSubtitle"">
                    <DataType>String</DataType>
                    <Nullable>true</Nullable>
                    <AllowBlank>true</AllowBlank>
                    <Prompt>ReportParameter1</Prompt>
                </ReportParameter>
                <ReportParameter Name = ""DivisionName"">
                    <DataType>String</DataType>
                    <Nullable>true</Nullable>
                    <AllowBlank>true</AllowBlank>
                    <Prompt>ReportParameter1</Prompt>
                </ReportParameter>"
        mReportStr += "</ReportParameters>"
    End Sub

    Private Sub FWriteFilters()
        mReportStr += "<Tablix Name=""Tablix3"">
        <TablixBody>
          <TablixColumns>
            <TablixColumn>
              <Width>3.57292in</Width>
            </TablixColumn>
            <TablixColumn>
              <Width>3.57292in</Width>
            </TablixColumn>
          </TablixColumns>
          <TablixRows>
            <TablixRow>
              <Height>0.2in</Height>
              <TablixCells>
                <TablixCell>
                  <CellContents>
                    <Textbox Name=""Textbox12"">
                      <CanGrow>true</CanGrow>
                      <KeepTogether>true</KeepTogether>
                      <Paragraphs>
                        <Paragraph>
                          <TextRuns>
                            <TextRun>
                              <Value>=Parameters!FilterStr1.Value</Value>
                                <Style>
                                    <FontFamily>Verdana</FontFamily>
                                    <FontSize>8pt</FontSize>
                                    <FontWeight>Bold</FontWeight>
                                </Style>
                            </TextRun>
                          </TextRuns>
                          <Style />
                        </Paragraph>
                      </Paragraphs>
                      <rd:DefaultName>Textbox12</rd:DefaultName>
                      <Style>
                        <Border>
                          <Color>LightGrey</Color>
                          <Style>None</Style>
                        </Border>
                        <PaddingLeft>2pt</PaddingLeft>
                        <PaddingRight>2pt</PaddingRight>
                        <PaddingTop>2pt</PaddingTop>
                        <PaddingBottom>2pt</PaddingBottom>
                      </Style>
                    </Textbox>
                  </CellContents>
                </TablixCell>
                <TablixCell>
                  <CellContents>
                    <Textbox Name=""Textbox14"">
                      <CanGrow>true</CanGrow>
                      <KeepTogether>true</KeepTogether>
                      <Paragraphs>
                        <Paragraph>
                          <TextRuns>
                            <TextRun>
                              <Value>=Parameters!FilterStr2.Value</Value>
                                <Style>
                                    <FontFamily>Verdana</FontFamily>
                                    <FontSize>8pt</FontSize>
                                    <FontWeight>Bold</FontWeight>
                                </Style>
                            </TextRun>
                          </TextRuns>
                          <Style />
                        </Paragraph>
                      </Paragraphs>
                      <rd:DefaultName>Textbox14</rd:DefaultName>
                      <Style>
                        <Border>
                          <Color>LightGrey</Color>
                          <Style>None</Style>
                        </Border>
                        <PaddingLeft>2pt</PaddingLeft>
                        <PaddingRight>2pt</PaddingRight>
                        <PaddingTop>2pt</PaddingTop>
                        <PaddingBottom>2pt</PaddingBottom>
                      </Style>
                    </Textbox>
                  </CellContents>
                </TablixCell>
              </TablixCells>
            </TablixRow>
            <TablixRow>
              <Height>0.2in</Height>
              <TablixCells>
                <TablixCell>
                  <CellContents>
                    <Textbox Name=""Textbox15"">
                      <CanGrow>true</CanGrow>
                      <KeepTogether>true</KeepTogether>
                      <Paragraphs>
                        <Paragraph>
                          <TextRuns>
                            <TextRun>
                              <Value>=Parameters!FilterStr3.Value</Value>
                                <Style>
                                    <FontFamily>Verdana</FontFamily>
                                    <FontSize>8pt</FontSize>
                                    <FontWeight>Bold</FontWeight>
                                </Style>
                            </TextRun>
                          </TextRuns>
                          <Style />
                        </Paragraph>
                      </Paragraphs>
                      <rd:DefaultName>Textbox15</rd:DefaultName>
                      <Style>
                        <Border>
                          <Color>LightGrey</Color>
                          <Style>None</Style>
                        </Border>
                        <PaddingLeft>2pt</PaddingLeft>
                        <PaddingRight>2pt</PaddingRight>
                        <PaddingTop>2pt</PaddingTop>
                        <PaddingBottom>2pt</PaddingBottom>
                      </Style>
                    </Textbox>
                  </CellContents>
                </TablixCell>
                <TablixCell>
                  <CellContents>
                    <Textbox Name=""Textbox16"">
                      <CanGrow>true</CanGrow>
                      <KeepTogether>true</KeepTogether>
                      <Paragraphs>
                        <Paragraph>
                          <TextRuns>
                            <TextRun>
                              <Value>=Parameters!FilterStr4.Value</Value>
                                <Style>
                                    <FontFamily>Verdana</FontFamily>
                                    <FontSize>8pt</FontSize>
                                    <FontWeight>Bold</FontWeight>
                                </Style>
                            </TextRun>
                          </TextRuns>
                          <Style />
                        </Paragraph>
                      </Paragraphs>
                      <rd:DefaultName>Textbox16</rd:DefaultName>
                      <Style>
                        <Border>
                          <Color>LightGrey</Color>
                          <Style>None</Style>
                        </Border>
                        <PaddingLeft>2pt</PaddingLeft>
                        <PaddingRight>2pt</PaddingRight>
                        <PaddingTop>2pt</PaddingTop>
                        <PaddingBottom>2pt</PaddingBottom>
                      </Style>
                    </Textbox>
                  </CellContents>
                </TablixCell>
              </TablixCells>
            </TablixRow>
            <TablixRow>
              <Height>0.2in</Height>
              <TablixCells>
                <TablixCell>
                  <CellContents>
                    <Textbox Name=""Textbox17"">
                      <CanGrow>true</CanGrow>
                      <KeepTogether>true</KeepTogether>
                      <Paragraphs>
                        <Paragraph>
                          <TextRuns>
                            <TextRun>
                              <Value>=Parameters!FilterStr5.Value</Value>
                                <Style>
                                    <FontFamily>Verdana</FontFamily>
                                    <FontSize>8pt</FontSize>
                                    <FontWeight>Bold</FontWeight>
                                </Style>
                            </TextRun>
                          </TextRuns>
                            <Style />
                        </Paragraph>
                      </Paragraphs>
                      <rd:DefaultName>Textbox17</rd:DefaultName>
                      <Style>
                        <Border>
                          <Color>LightGrey</Color>
                          <Style>None</Style>
                        </Border>
                        <PaddingLeft>2pt</PaddingLeft>
                        <PaddingRight>2pt</PaddingRight>
                        <PaddingTop>2pt</PaddingTop>
                        <PaddingBottom>2pt</PaddingBottom>
                      </Style>
                    </Textbox>
                  </CellContents>
                </TablixCell>
                <TablixCell>
                  <CellContents>
                    <Textbox Name=""Textbox20"">
                      <CanGrow>true</CanGrow>
                      <KeepTogether>true</KeepTogether>
                      <Paragraphs>
                        <Paragraph>
                          <TextRuns>
                            <TextRun>
                              <Value>=Parameters!FilterStr6.Value</Value>
                            <Style>
                                <FontFamily>Verdana</FontFamily>
                                <FontSize>8pt</FontSize>
                                <FontWeight>Bold</FontWeight>
                            </Style>
                            </TextRun>
                          </TextRuns>
                          <Style />
                        </Paragraph>
                      </Paragraphs>
                      <rd:DefaultName>Textbox20</rd:DefaultName>
                      <Style>
                        <Border>
                          <Color>LightGrey</Color>
                          <Style>None</Style>
                        </Border>
                        <PaddingLeft>2pt</PaddingLeft>
                        <PaddingRight>2pt</PaddingRight>
                        <PaddingTop>2pt</PaddingTop>
                        <PaddingBottom>2pt</PaddingBottom>
                      </Style>
                    </Textbox>
                  </CellContents>
                </TablixCell>
              </TablixCells>
            </TablixRow>
            <TablixRow>
              <Height>0.2in</Height>
              <TablixCells>
                <TablixCell>
                  <CellContents>
                    <Textbox Name=""Textbox21"">
                      <CanGrow>true</CanGrow>
                      <KeepTogether>true</KeepTogether>
                      <Paragraphs>
                        <Paragraph>
                          <TextRuns>
                            <TextRun>
                              <Value>=Parameters!FilterStr7.Value</Value>
                            <Style>
                                <FontFamily>Verdana</FontFamily>
                                <FontSize>8pt</FontSize>
                                <FontWeight>Bold</FontWeight>
                            </Style>
                            </TextRun>
                          </TextRuns>
                          <Style />
                        </Paragraph>
                      </Paragraphs>
                      <rd:DefaultName>Textbox21</rd:DefaultName>
                      <Style>
                        <Border>
                          <Color>LightGrey</Color>
                          <Style>None</Style>
                        </Border>
                        <PaddingLeft>2pt</PaddingLeft>
                        <PaddingRight>2pt</PaddingRight>
                        <PaddingTop>2pt</PaddingTop>
                        <PaddingBottom>2pt</PaddingBottom>
                      </Style>
                    </Textbox>
                  </CellContents>
                </TablixCell>
                <TablixCell>
                  <CellContents>
                    <Textbox Name=""Textbox22"">
                      <CanGrow>true</CanGrow>
                      <KeepTogether>true</KeepTogether>
                      <Paragraphs>
                        <Paragraph>
                          <TextRuns>
                            <TextRun>
                              <Value>=Parameters!FilterStr8.Value</Value>
                            <Style>
                                <FontFamily>Verdana</FontFamily>
                                <FontSize>8pt</FontSize>
                                <FontWeight>Bold</FontWeight>
                            </Style>
                            </TextRun>
                          </TextRuns>
                          <Style />
                        </Paragraph>
                      </Paragraphs>
                      <rd:DefaultName>Textbox22</rd:DefaultName>
                      <Style>
                        <Border>
                          <Color>LightGrey</Color>
                          <Style>None</Style>
                        </Border>
                        <PaddingLeft>2pt</PaddingLeft>
                        <PaddingRight>2pt</PaddingRight>
                        <PaddingTop>2pt</PaddingTop>
                        <PaddingBottom>2pt</PaddingBottom>
                      </Style>
                    </Textbox>
                  </CellContents>
                </TablixCell>
              </TablixCells>
            </TablixRow>
            <TablixRow>
              <Height>0.2in</Height>
              <TablixCells>
                <TablixCell>
                  <CellContents>
                    <Textbox Name=""Textbox23"">
                      <CanGrow>true</CanGrow>
                      <KeepTogether>true</KeepTogether>
                      <Paragraphs>
                        <Paragraph>
                          <TextRuns>
                            <TextRun>
                              <Value>=Parameters!FilterStr9.Value</Value>
                            <Style>
                                <FontFamily>Verdana</FontFamily>
                                <FontSize>8pt</FontSize>
                                <FontWeight>Bold</FontWeight>
                            </Style>
                            </TextRun>
                          </TextRuns>
                          <Style />
                        </Paragraph>
                      </Paragraphs>
                      <rd:DefaultName>Textbox23</rd:DefaultName>
                      <Style>
                        <Border>
                          <Color>LightGrey</Color>
                          <Style>None</Style>
                        </Border>
                        <PaddingLeft>2pt</PaddingLeft>
                        <PaddingRight>2pt</PaddingRight>
                        <PaddingTop>2pt</PaddingTop>
                        <PaddingBottom>2pt</PaddingBottom>
                      </Style>
                    </Textbox>
                  </CellContents>
                </TablixCell>
                <TablixCell>
                  <CellContents>
                    <Textbox Name=""Textbox24"">
                      <CanGrow>true</CanGrow>
                      <KeepTogether>true</KeepTogether>
                      <Paragraphs>
                        <Paragraph>
                          <TextRuns>
                            <TextRun>
                              <Value>=Parameters!FilterStr10.Value</Value>
                            <Style>
                                <FontFamily>Verdana</FontFamily>
                                <FontSize>8pt</FontSize>
                                <FontWeight>Bold</FontWeight>
                            </Style>
                            </TextRun>
                          </TextRuns>
                          <Style />
                        </Paragraph>
                      </Paragraphs>
                      <rd:DefaultName>Textbox24</rd:DefaultName>
                      <Style>
                        <Border>
                          <Color>LightGrey</Color>
                          <Style>None</Style>
                        </Border>
                        <PaddingLeft>2pt</PaddingLeft>
                        <PaddingRight>2pt</PaddingRight>
                        <PaddingTop>2pt</PaddingTop>
                        <PaddingBottom>2pt</PaddingBottom>
                      </Style>
                    </Textbox>
                  </CellContents>
                </TablixCell>
              </TablixCells>
            </TablixRow>
            <TablixRow>
              <Height>0.2in</Height>
              <TablixCells>
                <TablixCell>
                  <CellContents>
                    <Textbox Name=""Textbox11"">
                      <CanGrow>true</CanGrow>
                      <KeepTogether>true</KeepTogether>
                      <Paragraphs>
                        <Paragraph>
                          <TextRuns>
                            <TextRun>
                              <Value>=Parameters!FilterStr11.Value</Value>
                            <Style>
                                <FontFamily>Verdana</FontFamily>
                                <FontSize>8pt</FontSize>
                                <FontWeight>Bold</FontWeight>
                            </Style>
                            </TextRun>
                          </TextRuns>
                          <Style />
                        </Paragraph>
                      </Paragraphs>
                      <rd:DefaultName>Textbox11</rd:DefaultName>
                      <Style>
                        <Border>
                          <Color>LightGrey</Color>
                          <Style>None</Style>
                        </Border>
                        <PaddingLeft>2pt</PaddingLeft>
                        <PaddingRight>2pt</PaddingRight>
                        <PaddingTop>2pt</PaddingTop>
                        <PaddingBottom>2pt</PaddingBottom>
                      </Style>
                    </Textbox>
                  </CellContents>
                </TablixCell>
                <TablixCell>
                  <CellContents>
                    <Textbox Name=""Textbox13"">
                      <CanGrow>true</CanGrow>
                      <KeepTogether>true</KeepTogether>
                      <Paragraphs>
                        <Paragraph>
                          <TextRuns>
                            <TextRun>
                              <Value>=Parameters!FilterStr12.Value</Value>
                            <Style>
                                <FontFamily>Verdana</FontFamily>
                                <FontSize>8pt</FontSize>
                                <FontWeight>Bold</FontWeight>
                            </Style>
                            </TextRun>
                          </TextRuns>
                          <Style />
                        </Paragraph>
                      </Paragraphs>
                      <rd:DefaultName>Textbox13</rd:DefaultName>
                      <Style>
                        <Border>
                          <Color>LightGrey</Color>
                          <Style>None</Style>
                        </Border>
                        <PaddingLeft>2pt</PaddingLeft>
                        <PaddingRight>2pt</PaddingRight>
                        <PaddingTop>2pt</PaddingTop>
                        <PaddingBottom>2pt</PaddingBottom>
                      </Style>
                    </Textbox>
                  </CellContents>
                </TablixCell>
              </TablixCells>
            </TablixRow>
          </TablixRows>
        </TablixBody>
        <TablixColumnHierarchy>
          <TablixMembers>
            <TablixMember />
            <TablixMember />
          </TablixMembers>
        </TablixColumnHierarchy>
        <TablixRowHierarchy>
          <TablixMembers>
            <TablixMember>
              <Visibility>
                <Hidden>=Parameters!FilterStr1.Value="""" And Parameters!FilterStr2.Value=""""</Hidden>
              </Visibility>
            </TablixMember>
            <TablixMember>
              <Visibility>
                <Hidden>=Parameters!FilterStr3.Value="""" And Parameters!FilterStr4.Value=""""</Hidden>
              </Visibility>
            </TablixMember>
            <TablixMember>
              <Visibility>
                <Hidden>=Parameters!FilterStr5.Value="""" And Parameters!FilterStr6.Value=""""</Hidden>
              </Visibility>
            </TablixMember>
            <TablixMember>
              <Visibility>
                <Hidden>=Parameters!FilterStr7.Value="""" And Parameters!FilterStr8.Value=""""</Hidden>
              </Visibility>
            </TablixMember>
            <TablixMember>
              <Visibility>
                <Hidden>=Parameters!FilterStr9.Value="""" And Parameters!FilterStr10.Value=""""</Hidden>
              </Visibility>
            </TablixMember>
            <TablixMember>
              <Visibility>
                <Hidden>=Parameters!FilterStr11.Value = """" And Parameters!FilterStr12.Value = """"</Hidden>
              </Visibility>
            </TablixMember>
          </TablixMembers>
        </TablixRowHierarchy>
        <Height>1.5in</Height>
        <Width>7.14584in</Width>
        <ZIndex>1</ZIndex>
        <Style>
          <Border>
            <Style>None</Style>
          </Border>
        </Style>
      </Tablix>"

        mReportStr += "</ReportItems>
            <Height>3.125in</Height>
            <Style />
          </Body>
          <Width>7.27in</Width>"
    End Sub
    Private Sub FWriteColumns()
        mReportStr += "<Body>
            <ReportItems>
              <Tablix Name=""Tablix1"">
                <TablixBody>
                    <TablixColumns>"

        For I = 0 To DGL1.Columns.Count - 1
            If DGL1.Columns(I).Visible = True Then
                mReportStr += "<TablixColumn>
                      <Width>" & DGL1.Columns(I).Width * FGetColumnWidthMultiplier() & "in</Width>
                    </TablixColumn>"
            End If
        Next

        mReportStr += "</TablixColumns>"
    End Sub

    Private Sub FWriteHeaderRow()
        mReportStr += "<TablixRows>"

        mReportStr += "<TablixRow>
                      <Height>0.25in</Height>
                      <TablixCells>"

        For I = 0 To DGL1.Columns.Count - 1
            If DGL1.Columns(I).Visible = True Then
                mReportStr += "<TablixCell>
                          <CellContents>
                            <Textbox Name=""Txt" & DGL1.Columns(I).Name.Replace(" ", "") & "Header"">
                              <CanGrow>true</CanGrow>
                              <KeepTogether>true</KeepTogether>
                              <Paragraphs>
                                <Paragraph>
                                  <TextRuns>
                                    <TextRun>
                                      <Value>" & DGL1.Columns(I).HeaderText & "</Value>
                                      <Style>
                                        <FontFamily>Verdana</FontFamily>
                                        <FontSize>" & mReportFontSize.ToString & "pt</FontSize>
                                        <FontWeight>Bold</FontWeight>
                                      </Style>
                                    </TextRun>
                                  </TextRuns>
                                    <Style>
                                        <TextAlign>" & IIf(DGL1.Columns(I).DefaultCellStyle.Alignment = DataGridViewContentAlignment.NotSet, "Left", "Right") & "</TextAlign>
                                    </Style>
                                </Paragraph>
                              </Paragraphs>
                              <rd:DefaultName>Txt" & DGL1.Columns(I).Name.Replace(" ", "") & "Header</rd:DefaultName>
                              <Style>
                                <Border>
                                  <Color>Silver</Color>
                                  <Style>Solid</Style>
                                </Border>
                                <BackgroundColor>Gainsboro</BackgroundColor>
                                <PaddingLeft>2pt</PaddingLeft>
                                <PaddingRight>2pt</PaddingRight>
                                <PaddingTop>2pt</PaddingTop>
                                <PaddingBottom>2pt</PaddingBottom>
                              </Style>
                            </Textbox>
                          </CellContents>
                        </TablixCell>"
            End If
        Next

        mReportStr += "</TablixCells>
                </TablixRow>"
    End Sub
    Private Sub FWriteDetailRow()
        mReportStr += "<TablixRow>
                      <Height>0.25in</Height>
                      <TablixCells>"

        For I = 0 To dsReport.Tables(0).Columns.Count - 1
            mReportStr += "<TablixCell>
                          <CellContents>
                            <Textbox Name=""" & dsReport.Tables(0).Columns(I).ColumnName & """>
                              <CanGrow>true</CanGrow>
                              <KeepTogether>true</KeepTogether>
                              <Paragraphs>
                                <Paragraph>
                                  <TextRuns>
                                    <TextRun>
                                      <Value>=Fields!" & dsReport.Tables(0).Columns(I).ColumnName & ".Value</Value>
                                        <Style>
                                            <FontFamily>Verdana</FontFamily>"
            mReportStr += "<FontWeight>"
            mReportStr += "=IIf("

            Dim bBoldStr As String = ""
            For J As Integer = 0 To ds_font.Tables(0).Rows.Count - 1
                If AgL.XNull(ds_font.Tables(0).Rows(J)(I)) = "True" Then
                    If bBoldStr <> "" Then bBoldStr += " Or "
                    bBoldStr += "RowNumber(Nothing) = " & (J + 1) & ""
                End If
            Next
            If bBoldStr <> "" Then
                mReportStr += bBoldStr
            Else
                mReportStr += "1=2"
            End If
            mReportStr += ", ""Bold"", ""Regular"")"
            mReportStr += "</FontWeight>"

            mReportStr += "<FontSize>" & mReportFontSize.ToString & "pt</FontSize>
                                            " & IIf(dsReport.Tables(0).Columns(I).DataType.ToString() = "System.DateTime", "<Format>dd-MMM-yy</Format>", IIf(dsReport.Tables(0).Columns(I).DataType.ToString() = "System.Double", "<Format>0.00</Format>", "")) & "
                                        </Style>
                                    </TextRun>
                                  </TextRuns>
                                    <Style>
                                        <TextAlign>" & FGetColumnsAlignment(dsReport.Tables(0).Columns(I).ColumnName) & "</TextAlign>
                                    </Style>
                                </Paragraph>
                              </Paragraphs>
                              <rd:DefaultName>" & dsReport.Tables(0).Columns(I).ColumnName & "</rd:DefaultName>
                              <Style>
                                <Border>
                                  <Color>Silver</Color>
                                  <Style>Solid</Style>
                                </Border>
                                <BackgroundColor>=iif(RowNumber(nothing) Mod 2 , ""White"",""WhiteSmoke"")</BackgroundColor>
                                <PaddingLeft>2pt</PaddingLeft>
                                <PaddingRight>2pt</PaddingRight>
                                <PaddingTop>2pt</PaddingTop>
                                <PaddingBottom>2pt</PaddingBottom>
                              </Style>
                            </Textbox>
                          </CellContents>
                        </TablixCell>"
        Next

        mReportStr += "</TablixCells>
                    </TablixRow>"
    End Sub
    'Private Sub FWriteFooterRow()
    '    mReportStr += "<TablixRow>
    '                  <Height>0.25in</Height>
    '                  <TablixCells>"


    '    For I = 0 To dsReport.Tables(0).Columns.Count - 1
    '            mReportStr += "<TablixCell>
    '                      <CellContents>
    '                        <Textbox Name=""Txt" & dsReport.Tables(0).Columns(I).ColumnName & "Footer"">
    '                          <CanGrow>true</CanGrow>
    '                          <KeepTogether>true</KeepTogether>
    '                          <Paragraphs>
    '                            <Paragraph>
    '                              <TextRuns>
    '                                <TextRun>
    '                                    " & IIf(dsReport.Tables(0).Columns(I).DataType.ToString() = "System.Double", "<Value>=Sum(Fields!" & dsReport.Tables(0).Columns(I).ColumnName & ".Value)</Value>", "<Value />") & "
    '                                  <Style>
    '                                    <FontFamily>Verdana</FontFamily>
    '                                    <FontSize>" & mReportFontSize.ToString & "pt</FontSize>
    '                                    <FontWeight>Bold</FontWeight>
    '                                  </Style>
    '                                </TextRun>
    '                              </TextRuns>
    '                                <Style>
    '                                    <TextAlign>" & FGetColumnsAlignment(dsReport.Tables(0).Columns(I).ColumnName) & "</TextAlign>
    '                                </Style>
    '                            </Paragraph>
    '                          </Paragraphs>
    '                          <rd:DefaultName>Txt" & dsReport.Tables(0).Columns(I).ColumnName & "Footer</rd:DefaultName>
    '                          <Style>
    '                            <Border>
    '                              <Color>Silver</Color>
    '                              <Style>Solid</Style>
    '                            </Border>
    '                            <PaddingLeft>2pt</PaddingLeft>
    '                            <PaddingRight>2pt</PaddingRight>
    '                            <PaddingTop>2pt</PaddingTop>
    '                            <PaddingBottom>2pt</PaddingBottom>
    '                          </Style>
    '                        </Textbox>
    '                      </CellContents>
    '                    </TablixCell>"
    '        Next

    '        mReportStr += "</TablixCells>
    '                </TablixRow>"


    '    mReportStr += "</TablixRows>
    '            </TablixBody>"
    'End Sub

    Private Sub FWriteFooterRow()
        mReportStr += "<TablixRow>
                      <Height>0.25in</Height>
                      <TablixCells>"


        For I = 0 To dsReportTotal.Tables(0).Columns.Count - 1
            mReportStr += "<TablixCell>
                          <CellContents>
                            <Textbox Name=""Txt" & dsReportTotal.Tables(0).Columns(I).ColumnName & "Footer"">
                              <CanGrow>true</CanGrow>
                              <KeepTogether>true</KeepTogether>
                              <Paragraphs>
                                <Paragraph>
                                  <TextRuns>
                                    <TextRun>
                                        <Value>" & dsReportTotal.Tables(0).Rows(0)(I) & "</Value>
                                      <Style>
                                        <FontFamily>Verdana</FontFamily>
                                        <FontSize>" & mReportFontSize.ToString & "pt</FontSize>
                                        <FontWeight>Bold</FontWeight>
                                      </Style>
                                    </TextRun>
                                  </TextRuns>
                                    <Style>
                                        <TextAlign>" & FGetColumnsAlignment(dsReportTotal.Tables(0).Columns(I).ColumnName) & "</TextAlign>
                                    </Style>
                                </Paragraph>
                              </Paragraphs>
                              <rd:DefaultName>Txt" & dsReportTotal.Tables(0).Columns(I).ColumnName & "Footer</rd:DefaultName>
                              <Style>
                                <Border>
                                  <Color>Silver</Color>
                                  <Style>Solid</Style>
                                </Border>
                                <PaddingLeft>2pt</PaddingLeft>
                                <PaddingRight>2pt</PaddingRight>
                                <PaddingTop>2pt</PaddingTop>
                                <PaddingBottom>2pt</PaddingBottom>
                              </Style>
                            </Textbox>
                          </CellContents>
                        </TablixCell>"
        Next

        mReportStr += "</TablixCells>
                    </TablixRow>"


        mReportStr += "</TablixRows>
                </TablixBody>"
    End Sub
    Private Sub FWriteTableColumnHirarichy()
        mReportStr += "<TablixColumnHierarchy>
                  <TablixMembers>"

        For I = 0 To dsReport.Tables(0).Columns.Count - 1
            mReportStr += "<TablixMember />"
        Next
        mReportStr += "</TablixMembers>
                </TablixColumnHierarchy>
                <TablixRowHierarchy>
                  <TablixMembers>
                    <TablixMember>
                      <KeepWithGroup>After</KeepWithGroup>
                      <RepeatOnNewPage>true</RepeatOnNewPage>
                    </TablixMember>
                    <TablixMember>
                      <Group Name=""Details"" />
                    </TablixMember>
                    <TablixMember>
                      <KeepWithGroup>Before</KeepWithGroup>
                    </TablixMember>
                  </TablixMembers>
                </TablixRowHierarchy>
                <DataSetName>DsMain</DataSetName>
                <Top>1.25in</Top>
                <Height>0.75in</Height>
                <Width>2.46529in</Width>
                <Style>
                  <Border>
                    <Style>None</Style>
                  </Border>
                    <FontFamily>Verdana</FontFamily>
                    <FontSize>8pt</FontSize>
                    <FontWeight>Bold</FontWeight>
                </Style>
              </Tablix>"




    End Sub

    Private Sub FWritePageProperties(ByRef reportViewer1 As ReportViewer)
        Dim PageHeight As Double = 0
        Dim PageWidth As Double = 0
        If reportViewer1.GetPageSettings.Landscape = True Then
            PageHeight = 8.27
            PageWidth = 11.69
        Else
            PageHeight = 11.69
            PageWidth = 8.27
        End If

        mReportStr += "<Page>
            <PageHeader>
              <Height>0.655in</Height>
              <PrintOnFirstPage>true</PrintOnFirstPage>
              <PrintOnLastPage>true</PrintOnLastPage>
                <ReportItems>
                <Textbox Name=""ReportTitle"">
                    <CanGrow>true</CanGrow>
                    <KeepTogether>true</KeepTogether>
                    <Paragraphs>
                    <Paragraph>
                        <TextRuns>
                        <TextRun>
                            <Value>=Parameters!ReportTitle.Value</Value>
                            <Style>
                            <FontSize>14pt</FontSize>
                            <FontWeight>Bold</FontWeight>
                            </Style>
                        </TextRun>
                        </TextRuns>
                        <Style />
                    </Paragraph>
                    </Paragraphs>
                    <rd:DefaultName>ReportTitle</rd:DefaultName>
                    <Height>0.36458in</Height>
                    <Width>4.36458in</Width>
                    <Style>
                    <Border>
                        <Style>None</Style>
                    </Border>
                    <PaddingLeft>2pt</PaddingLeft>
                    <PaddingRight>2pt</PaddingRight>
                    <PaddingTop>2pt</PaddingTop>
                    <PaddingBottom>2pt</PaddingBottom>
                    </Style>
                </Textbox>
                <Textbox Name=""CompanyName"">
                    <CanGrow>true</CanGrow>
                    <KeepTogether>true</KeepTogether>
                    <Paragraphs>
                    <Paragraph>
                        <TextRuns>
                        <TextRun>
                            <Value>=Parameters!CompanyName.Value</Value>
                            <Style>
                            <FontSize>12pt</FontSize>
                            <FontWeight>Bold</FontWeight>
                            </Style>
                        </TextRun>
                        </TextRuns>
                        <Style>
                        <TextAlign>Right</TextAlign>
                        </Style>
                    </Paragraph>
                    </Paragraphs>
                    <rd:DefaultName>CompanyName</rd:DefaultName>
                    <Left>4.4675in</Left>
                    <Height>0.25in</Height>
                    <Width>2.7825in</Width>
                    <ZIndex>1</ZIndex>
                    <Style>
                    <Border>
                        <Style>None</Style>
                    </Border>
                    <PaddingLeft>2pt</PaddingLeft>
                    <PaddingRight>2pt</PaddingRight>
                    <PaddingTop>2pt</PaddingTop>
                    <PaddingBottom>2pt</PaddingBottom>
                    </Style>
                </Textbox>
                <Textbox Name=""ReportSubtitle"">
                    <CanGrow>true</CanGrow>
                    <KeepTogether>true</KeepTogether>
                    <Paragraphs>
                    <Paragraph>
                        <TextRuns>
                        <TextRun>
                            <Value>=Parameters!ReportSubtitle.Value</Value>
                            <Style>
                            <FontSize>12pt</FontSize>
                            <FontWeight>Bold</FontWeight>
                            </Style>
                        </TextRun>
                        </TextRuns>
                        <Style />
                    </Paragraph>
                    </Paragraphs>
                    <rd:DefaultName>ReportSubtitle</rd:DefaultName>
                    <Top>0.34792in</Top>
                    <Height>0.25in</Height>
                    <Width>6.36458in</Width>
                    <ZIndex>2</ZIndex>
                    <Style>
                    <Border>
                        <Style>None</Style>
                    </Border>
                    <PaddingLeft>2pt</PaddingLeft>
                    <PaddingRight>2pt</PaddingRight>
                    <PaddingTop>2pt</PaddingTop>
                    <PaddingBottom>2pt</PaddingBottom>
                    </Style>
                </Textbox>
                <Textbox Name=""DivisionName"">
                    <CanGrow>true</CanGrow>
                    <KeepTogether>true</KeepTogether>
                    <Paragraphs>
                    <Paragraph>
                        <TextRuns>
                        <TextRun>
                            <Value>=Parameters!DivisionName.Value</Value>
                            <Style>
                            <FontWeight>Bold</FontWeight>
                            </Style>
                        </TextRun>
                        </TextRuns>
                        <Style>
                        <TextAlign>Right</TextAlign>
                        </Style>
                    </Paragraph>
                    </Paragraphs>
                    <rd:DefaultName>DivisionName</rd:DefaultName>
                    <Top>0.30556in</Top>
                    <Left>4.4675in</Left>
                    <Height>0.25in</Height>
                    <Width>2.7825in</Width>
                    <ZIndex>3</ZIndex>
                    <Style>
                    <Border>
                        <Style>None</Style>
                    </Border>
                    <PaddingLeft>2pt</PaddingLeft>
                    <PaddingRight>2pt</PaddingRight>
                    <PaddingTop>2pt</PaddingTop>
                    <PaddingBottom>2pt</PaddingBottom>
                    </Style>
                </Textbox>
                </ReportItems>
              <Style>
                <Border>
                  <Style>None</Style>
                </Border>
              </Style>
            </PageHeader>
            <PageFooter>
              <Height>0.01042in</Height>
              <PrintOnFirstPage>true</PrintOnFirstPage>
              <PrintOnLastPage>true</PrintOnLastPage>
              <Style>
                <Border>
                  <Style>None</Style>
                </Border>
              </Style>
            </PageFooter>
            <PageHeight>" & PageHeight & "in</PageHeight>
            <PageWidth>" & PageWidth & "in</PageWidth>
            <InteractiveHeight>11.69in</InteractiveHeight>
            <InteractiveWidth>8.27in</InteractiveWidth>
            <LeftMargin>0.25in</LeftMargin>
            <RightMargin>0.25in</RightMargin>
            <TopMargin>0.17in</TopMargin>
            <BottomMargin>0.5in</BottomMargin>
            <Style>
              <Border>
                <Color>White</Color>
                <Style>None</Style>
              </Border>
            </Style>
          </Page>"

        mReportStr += "<rd:ReportID>15c4a7f5-3a06-4987-a113-c1b9ad4150d2</rd:ReportID>
          <rd:ReportUnitType>Inch</rd:ReportUnitType>
        </Report>"
    End Sub

    Private Sub FrmReportPrint_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class