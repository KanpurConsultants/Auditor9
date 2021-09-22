<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReportTool
    Inherits System.Windows.Forms.Form
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If Disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.          [Ag]
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.CboDescription = New AgControls.AgComboBox
        Me.LblDescription = New System.Windows.Forms.Label
        Me.LblDescriptionReq = New System.Windows.Forms.Label
        Me.TxtQuery = New AgControls.AgTextBox
        Me.LblQuery = New System.Windows.Forms.Label
        Me.LblQueryReq = New System.Windows.Forms.Label
        Me.Pnl3 = New System.Windows.Forms.Panel
        Me.Pnl4 = New System.Windows.Forms.Panel
        Me.BtnParseQuery = New System.Windows.Forms.Button
        Me.DgOutput = New AgControls.AgDataGrid
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.DgOutputTotals = New AgControls.AgDataGrid
        Me.GrpBoxUserReports = New System.Windows.Forms.GroupBox
        Me.BtnCancelUserReport = New System.Windows.Forms.Button
        Me.BtnOkUserReports = New System.Windows.Forms.Button
        Me.Lbl = New System.Windows.Forms.Label
        Me.TxtUserReport = New AgControls.AgTextBox
        Me.BtnGo = New System.Windows.Forms.Button
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.BtnFilterRecords = New System.Windows.Forms.Button
        Me.BtnColumnSelection = New System.Windows.Forms.Button
        Me.BtnReportGroups = New System.Windows.Forms.Button
        Me.GrpBoxReportCriteria = New System.Windows.Forms.GroupBox
        Me.BtnOkReportCriteria = New System.Windows.Forms.Button
        Me.GrpBoxColumnSelection = New System.Windows.Forms.GroupBox
        Me.BtnOkColumnSelection = New System.Windows.Forms.Button
        Me.GrpBoxPrintSettings = New System.Windows.Forms.GroupBox
        Me.Pnl2 = New System.Windows.Forms.Panel
        Me.BtnOkPrintSettings = New System.Windows.Forms.Button
        Me.GrpBoxReportGroups = New System.Windows.Forms.GroupBox
        Me.BtnOkReportGroups = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.BtnPrintSettings = New System.Windows.Forms.Button
        Me.BtnOrderBy = New System.Windows.Forms.Button
        Me.BtnSaveReportSettings = New System.Windows.Forms.Button
        Me.BtnPrint = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.BtnNewUserReport = New System.Windows.Forms.Button
        Me.BtnUserReports = New System.Windows.Forms.Button
        Me.OptUserReport = New System.Windows.Forms.RadioButton
        Me.OptSystemReport = New System.Windows.Forms.RadioButton
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.OptSummaryReport = New System.Windows.Forms.RadioButton
        Me.OptDetailedReport = New System.Windows.Forms.RadioButton
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.GrpBoxDataSorting = New System.Windows.Forms.GroupBox
        Me.Pnl5 = New System.Windows.Forms.Panel
        Me.BtnOkDataSorting = New System.Windows.Forms.Button
        Me.GrpDataGroups = New System.Windows.Forms.GroupBox
        Me.PnlDataGroups = New System.Windows.Forms.Panel
        Me.BtnDataGroup = New System.Windows.Forms.Button
        Me.Topctrl1 = New Topctrl.Topctrl
        CType(Me.DgOutput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DgOutputTotals, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpBoxUserReports.SuspendLayout()
        Me.GrpBoxReportCriteria.SuspendLayout()
        Me.GrpBoxColumnSelection.SuspendLayout()
        Me.GrpBoxPrintSettings.SuspendLayout()
        Me.GrpBoxReportGroups.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GrpBoxDataSorting.SuspendLayout()
        Me.GrpDataGroups.SuspendLayout()
        Me.SuspendLayout()
        '
        'CboDescription
        '
        Me.CboDescription.AgCmboMaster = True
        Me.CboDescription.AgMandatory = False
        Me.CboDescription.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboDescription.FormattingEnabled = True
        Me.CboDescription.Location = New System.Drawing.Point(20, 623)
        Me.CboDescription.MaxLength = 50
        Me.CboDescription.Name = "CboDescription"
        Me.CboDescription.Size = New System.Drawing.Size(331, 24)
        Me.CboDescription.TabIndex = 15
        Me.CboDescription.Text = "CboDescription"
        Me.CboDescription.Visible = False
        '
        'LblDescription
        '
        Me.LblDescription.AutoSize = True
        Me.LblDescription.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDescription.Location = New System.Drawing.Point(138, 508)
        Me.LblDescription.Name = "LblDescription"
        Me.LblDescription.Size = New System.Drawing.Size(96, 16)
        Me.LblDescription.TabIndex = 0
        Me.LblDescription.Text = "Description"
        Me.LblDescription.Visible = False
        '
        'LblDescriptionReq
        '
        Me.LblDescriptionReq.AutoSize = True
        Me.LblDescriptionReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblDescriptionReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblDescriptionReq.Location = New System.Drawing.Point(4, 629)
        Me.LblDescriptionReq.Name = "LblDescriptionReq"
        Me.LblDescriptionReq.Size = New System.Drawing.Size(10, 7)
        Me.LblDescriptionReq.TabIndex = 0
        Me.LblDescriptionReq.Text = "Ä"
        Me.LblDescriptionReq.Visible = False
        '
        'TxtQuery
        '
        Me.TxtQuery.AgMandatory = False
        Me.TxtQuery.AgMasterHelp = False
        Me.TxtQuery.AgNumberLeftPlaces = 0
        Me.TxtQuery.AgNumberNegetiveAllow = False
        Me.TxtQuery.AgNumberRightPlaces = 0
        Me.TxtQuery.AgPickFromLastValue = False
        Me.TxtQuery.AgSelectedValue = Nothing
        Me.TxtQuery.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtQuery.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtQuery.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtQuery.Location = New System.Drawing.Point(357, 605)
        Me.TxtQuery.MaxLength = 0
        Me.TxtQuery.Multiline = True
        Me.TxtQuery.Name = "TxtQuery"
        Me.TxtQuery.Size = New System.Drawing.Size(465, 42)
        Me.TxtQuery.TabIndex = 16
        Me.TxtQuery.Text = "TxtQuery"
        Me.TxtQuery.Visible = False
        '
        'LblQuery
        '
        Me.LblQuery.AutoSize = True
        Me.LblQuery.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblQuery.Location = New System.Drawing.Point(238, 539)
        Me.LblQuery.Name = "LblQuery"
        Me.LblQuery.Size = New System.Drawing.Size(48, 16)
        Me.LblQuery.TabIndex = 0
        Me.LblQuery.Text = "Query"
        Me.LblQuery.Visible = False
        '
        'LblQueryReq
        '
        Me.LblQueryReq.AutoSize = True
        Me.LblQueryReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblQueryReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblQueryReq.Location = New System.Drawing.Point(157, 545)
        Me.LblQueryReq.Name = "LblQueryReq"
        Me.LblQueryReq.Size = New System.Drawing.Size(10, 7)
        Me.LblQueryReq.TabIndex = 0
        Me.LblQueryReq.Text = "Ä"
        Me.LblQueryReq.Visible = False
        '
        'Pnl3
        '
        Me.Pnl3.Location = New System.Drawing.Point(18, 29)
        Me.Pnl3.Name = "Pnl3"
        Me.Pnl3.Size = New System.Drawing.Size(458, 98)
        Me.Pnl3.TabIndex = 102
        '
        'Pnl4
        '
        Me.Pnl4.Location = New System.Drawing.Point(16, 27)
        Me.Pnl4.Name = "Pnl4"
        Me.Pnl4.Size = New System.Drawing.Size(514, 164)
        Me.Pnl4.TabIndex = 102
        Me.Pnl4.Visible = False
        '
        'BtnParseQuery
        '
        Me.BtnParseQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnParseQuery.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnParseQuery.Location = New System.Drawing.Point(159, 466)
        Me.BtnParseQuery.Name = "BtnParseQuery"
        Me.BtnParseQuery.Size = New System.Drawing.Size(92, 24)
        Me.BtnParseQuery.TabIndex = 103
        Me.BtnParseQuery.Text = "Parse Query"
        Me.BtnParseQuery.UseVisualStyleBackColor = True
        '
        'DgOutput
        '
        Me.DgOutput.AllowUserToAddRows = False
        Me.DgOutput.AllowUserToDeleteRows = False
        Me.DgOutput.AllowUserToOrderColumns = True
        Me.DgOutput.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DgOutput.CancelEditingControlValidating = True
        Me.DgOutput.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.Gainsboro
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgOutput.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        Me.DgOutput.ColumnHeadersHeight = 30
        Me.DgOutput.EnableHeadersVisualStyles = False
        Me.DgOutput.Location = New System.Drawing.Point(6, 19)
        Me.DgOutput.Name = "DgOutput"
        Me.DgOutput.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.Gainsboro
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgOutput.RowHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.DgOutput.RowHeadersWidth = 20
        Me.DgOutput.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridViewCellStyle9.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgOutput.RowsDefaultCellStyle = DataGridViewCellStyle9
        Me.DgOutput.Size = New System.Drawing.Size(962, 373)
        Me.DgOutput.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DgOutputTotals)
        Me.GroupBox1.Controls.Add(Me.GrpBoxUserReports)
        Me.GroupBox1.Controls.Add(Me.LblDescription)
        Me.GroupBox1.Controls.Add(Me.LblQuery)
        Me.GroupBox1.Controls.Add(Me.BtnParseQuery)
        Me.GroupBox1.Controls.Add(Me.LblQueryReq)
        Me.GroupBox1.Controls.Add(Me.DgOutput)
        Me.GroupBox1.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(6, 63)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(974, 439)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Output"
        '
        'DgOutputTotals
        '
        Me.DgOutputTotals.AllowUserToAddRows = False
        Me.DgOutputTotals.AllowUserToDeleteRows = False
        Me.DgOutputTotals.AllowUserToOrderColumns = True
        Me.DgOutputTotals.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.DgOutputTotals.CancelEditingControlValidating = True
        Me.DgOutputTotals.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle10.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle10.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgOutputTotals.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle10
        Me.DgOutputTotals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgOutputTotals.ColumnHeadersVisible = False
        Me.DgOutputTotals.EnableHeadersVisualStyles = False
        Me.DgOutputTotals.Location = New System.Drawing.Point(6, 392)
        Me.DgOutputTotals.Name = "DgOutputTotals"
        Me.DgOutputTotals.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle11.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgOutputTotals.RowHeadersDefaultCellStyle = DataGridViewCellStyle11
        Me.DgOutputTotals.RowHeadersWidth = 20
        DataGridViewCellStyle12.BackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DgOutputTotals.RowsDefaultCellStyle = DataGridViewCellStyle12
        Me.DgOutputTotals.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.DgOutputTotals.Size = New System.Drawing.Size(962, 35)
        Me.DgOutputTotals.TabIndex = 2
        '
        'GrpBoxUserReports
        '
        Me.GrpBoxUserReports.Controls.Add(Me.BtnCancelUserReport)
        Me.GrpBoxUserReports.Controls.Add(Me.BtnOkUserReports)
        Me.GrpBoxUserReports.Controls.Add(Me.Lbl)
        Me.GrpBoxUserReports.Controls.Add(Me.TxtUserReport)
        Me.GrpBoxUserReports.Location = New System.Drawing.Point(34, 65)
        Me.GrpBoxUserReports.Name = "GrpBoxUserReports"
        Me.GrpBoxUserReports.Size = New System.Drawing.Size(393, 117)
        Me.GrpBoxUserReports.TabIndex = 104
        Me.GrpBoxUserReports.TabStop = False
        Me.GrpBoxUserReports.Text = "User Reports"
        Me.GrpBoxUserReports.Visible = False
        '
        'BtnCancelUserReport
        '
        Me.BtnCancelUserReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancelUserReport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancelUserReport.Location = New System.Drawing.Point(198, 77)
        Me.BtnCancelUserReport.Name = "BtnCancelUserReport"
        Me.BtnCancelUserReport.Size = New System.Drawing.Size(93, 23)
        Me.BtnCancelUserReport.TabIndex = 117
        Me.BtnCancelUserReport.Text = "Delete"
        Me.BtnCancelUserReport.UseVisualStyleBackColor = True
        '
        'BtnOkUserReports
        '
        Me.BtnOkUserReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOkUserReports.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOkUserReports.Location = New System.Drawing.Point(99, 77)
        Me.BtnOkUserReports.Name = "BtnOkUserReports"
        Me.BtnOkUserReports.Size = New System.Drawing.Size(93, 23)
        Me.BtnOkUserReports.TabIndex = 116
        Me.BtnOkUserReports.Text = "OK"
        Me.BtnOkUserReports.UseVisualStyleBackColor = True
        '
        'Lbl
        '
        Me.Lbl.AutoSize = True
        Me.Lbl.Location = New System.Drawing.Point(16, 23)
        Me.Lbl.Name = "Lbl"
        Me.Lbl.Size = New System.Drawing.Size(96, 16)
        Me.Lbl.TabIndex = 3
        Me.Lbl.Text = "Report Name"
        '
        'TxtUserReport
        '
        Me.TxtUserReport.AgMandatory = False
        Me.TxtUserReport.AgMasterHelp = True
        Me.TxtUserReport.AgNumberLeftPlaces = 0
        Me.TxtUserReport.AgNumberNegetiveAllow = False
        Me.TxtUserReport.AgNumberRightPlaces = 0
        Me.TxtUserReport.AgPickFromLastValue = False
        Me.TxtUserReport.AgSelectedValue = Nothing
        Me.TxtUserReport.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtUserReport.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtUserReport.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtUserReport.Location = New System.Drawing.Point(123, 21)
        Me.TxtUserReport.Name = "TxtUserReport"
        Me.TxtUserReport.Size = New System.Drawing.Size(232, 21)
        Me.TxtUserReport.TabIndex = 2
        '
        'BtnGo
        '
        Me.BtnGo.BackColor = System.Drawing.Color.LemonChiffon
        Me.BtnGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnGo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnGo.Location = New System.Drawing.Point(14, 11)
        Me.BtnGo.Name = "BtnGo"
        Me.BtnGo.Size = New System.Drawing.Size(88, 23)
        Me.BtnGo.TabIndex = 114
        Me.BtnGo.Text = "PROCEED"
        Me.BtnGo.UseVisualStyleBackColor = False
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(18, 27)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(633, 50)
        Me.Pnl1.TabIndex = 103
        Me.Pnl1.Visible = False
        '
        'BtnFilterRecords
        '
        Me.BtnFilterRecords.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFilterRecords.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFilterRecords.Location = New System.Drawing.Point(15, 11)
        Me.BtnFilterRecords.Name = "BtnFilterRecords"
        Me.BtnFilterRecords.Size = New System.Drawing.Size(114, 23)
        Me.BtnFilterRecords.TabIndex = 115
        Me.BtnFilterRecords.Text = "Report Criteria"
        Me.BtnFilterRecords.UseVisualStyleBackColor = True
        '
        'BtnColumnSelection
        '
        Me.BtnColumnSelection.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnColumnSelection.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnColumnSelection.Location = New System.Drawing.Point(134, 11)
        Me.BtnColumnSelection.Name = "BtnColumnSelection"
        Me.BtnColumnSelection.Size = New System.Drawing.Size(114, 23)
        Me.BtnColumnSelection.TabIndex = 116
        Me.BtnColumnSelection.Text = "Column Settings"
        Me.BtnColumnSelection.UseVisualStyleBackColor = True
        '
        'BtnReportGroups
        '
        Me.BtnReportGroups.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnReportGroups.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnReportGroups.Location = New System.Drawing.Point(15, 35)
        Me.BtnReportGroups.Name = "BtnReportGroups"
        Me.BtnReportGroups.Size = New System.Drawing.Size(114, 23)
        Me.BtnReportGroups.TabIndex = 117
        Me.BtnReportGroups.Text = "Report Groups"
        Me.BtnReportGroups.UseVisualStyleBackColor = True
        '
        'GrpBoxReportCriteria
        '
        Me.GrpBoxReportCriteria.Controls.Add(Me.BtnOkReportCriteria)
        Me.GrpBoxReportCriteria.Controls.Add(Me.Pnl3)
        Me.GrpBoxReportCriteria.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpBoxReportCriteria.Location = New System.Drawing.Point(742, 635)
        Me.GrpBoxReportCriteria.Name = "GrpBoxReportCriteria"
        Me.GrpBoxReportCriteria.Size = New System.Drawing.Size(492, 168)
        Me.GrpBoxReportCriteria.TabIndex = 0
        Me.GrpBoxReportCriteria.TabStop = False
        Me.GrpBoxReportCriteria.Text = "Report Criteria"
        Me.GrpBoxReportCriteria.Visible = False
        '
        'BtnOkReportCriteria
        '
        Me.BtnOkReportCriteria.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOkReportCriteria.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOkReportCriteria.Location = New System.Drawing.Point(205, 135)
        Me.BtnOkReportCriteria.Name = "BtnOkReportCriteria"
        Me.BtnOkReportCriteria.Size = New System.Drawing.Size(93, 23)
        Me.BtnOkReportCriteria.TabIndex = 116
        Me.BtnOkReportCriteria.Text = "OK"
        Me.BtnOkReportCriteria.UseVisualStyleBackColor = True
        '
        'GrpBoxColumnSelection
        '
        Me.GrpBoxColumnSelection.Controls.Add(Me.BtnOkColumnSelection)
        Me.GrpBoxColumnSelection.Controls.Add(Me.Pnl4)
        Me.GrpBoxColumnSelection.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpBoxColumnSelection.Location = New System.Drawing.Point(179, 508)
        Me.GrpBoxColumnSelection.Name = "GrpBoxColumnSelection"
        Me.GrpBoxColumnSelection.Size = New System.Drawing.Size(548, 226)
        Me.GrpBoxColumnSelection.TabIndex = 0
        Me.GrpBoxColumnSelection.TabStop = False
        Me.GrpBoxColumnSelection.Text = "Column Selection"
        Me.GrpBoxColumnSelection.Visible = False
        '
        'BtnOkColumnSelection
        '
        Me.BtnOkColumnSelection.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOkColumnSelection.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOkColumnSelection.Location = New System.Drawing.Point(215, 197)
        Me.BtnOkColumnSelection.Name = "BtnOkColumnSelection"
        Me.BtnOkColumnSelection.Size = New System.Drawing.Size(93, 23)
        Me.BtnOkColumnSelection.TabIndex = 115
        Me.BtnOkColumnSelection.Text = "OK"
        Me.BtnOkColumnSelection.UseVisualStyleBackColor = True
        '
        'GrpBoxPrintSettings
        '
        Me.GrpBoxPrintSettings.Controls.Add(Me.Pnl2)
        Me.GrpBoxPrintSettings.Controls.Add(Me.BtnOkPrintSettings)
        Me.GrpBoxPrintSettings.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpBoxPrintSettings.Location = New System.Drawing.Point(259, 508)
        Me.GrpBoxPrintSettings.Name = "GrpBoxPrintSettings"
        Me.GrpBoxPrintSettings.Size = New System.Drawing.Size(584, 168)
        Me.GrpBoxPrintSettings.TabIndex = 117
        Me.GrpBoxPrintSettings.TabStop = False
        Me.GrpBoxPrintSettings.Text = "Print Settings"
        Me.GrpBoxPrintSettings.Visible = False
        '
        'Pnl2
        '
        Me.Pnl2.Location = New System.Drawing.Point(18, 29)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(548, 98)
        Me.Pnl2.TabIndex = 102
        '
        'BtnOkPrintSettings
        '
        Me.BtnOkPrintSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOkPrintSettings.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOkPrintSettings.Location = New System.Drawing.Point(246, 133)
        Me.BtnOkPrintSettings.Name = "BtnOkPrintSettings"
        Me.BtnOkPrintSettings.Size = New System.Drawing.Size(93, 23)
        Me.BtnOkPrintSettings.TabIndex = 116
        Me.BtnOkPrintSettings.Text = "OK"
        Me.BtnOkPrintSettings.UseVisualStyleBackColor = True
        '
        'GrpBoxReportGroups
        '
        Me.GrpBoxReportGroups.Controls.Add(Me.BtnOkReportGroups)
        Me.GrpBoxReportGroups.Controls.Add(Me.Pnl1)
        Me.GrpBoxReportGroups.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpBoxReportGroups.Location = New System.Drawing.Point(850, 694)
        Me.GrpBoxReportGroups.Name = "GrpBoxReportGroups"
        Me.GrpBoxReportGroups.Size = New System.Drawing.Size(674, 115)
        Me.GrpBoxReportGroups.TabIndex = 3
        Me.GrpBoxReportGroups.TabStop = False
        Me.GrpBoxReportGroups.Text = "Report Groups"
        Me.GrpBoxReportGroups.Visible = False
        '
        'BtnOkReportGroups
        '
        Me.BtnOkReportGroups.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOkReportGroups.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOkReportGroups.Location = New System.Drawing.Point(291, 83)
        Me.BtnOkReportGroups.Name = "BtnOkReportGroups"
        Me.BtnOkReportGroups.Size = New System.Drawing.Size(93, 23)
        Me.BtnOkReportGroups.TabIndex = 117
        Me.BtnOkReportGroups.Text = "OK"
        Me.BtnOkReportGroups.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.BtnPrintSettings)
        Me.GroupBox2.Controls.Add(Me.BtnOrderBy)
        Me.GroupBox2.Controls.Add(Me.BtnSaveReportSettings)
        Me.GroupBox2.Controls.Add(Me.BtnFilterRecords)
        Me.GroupBox2.Controls.Add(Me.BtnColumnSelection)
        Me.GroupBox2.Controls.Add(Me.BtnReportGroups)
        Me.GroupBox2.Location = New System.Drawing.Point(601, -1)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(379, 63)
        Me.GroupBox2.TabIndex = 118
        Me.GroupBox2.TabStop = False
        '
        'BtnPrintSettings
        '
        Me.BtnPrintSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPrintSettings.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPrintSettings.Location = New System.Drawing.Point(254, 11)
        Me.BtnPrintSettings.Name = "BtnPrintSettings"
        Me.BtnPrintSettings.Size = New System.Drawing.Size(114, 23)
        Me.BtnPrintSettings.TabIndex = 121
        Me.BtnPrintSettings.Text = "Print Settings"
        Me.BtnPrintSettings.UseVisualStyleBackColor = True
        '
        'BtnOrderBy
        '
        Me.BtnOrderBy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOrderBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOrderBy.Location = New System.Drawing.Point(134, 35)
        Me.BtnOrderBy.Name = "BtnOrderBy"
        Me.BtnOrderBy.Size = New System.Drawing.Size(114, 23)
        Me.BtnOrderBy.TabIndex = 120
        Me.BtnOrderBy.Text = "Data Sorting"
        Me.BtnOrderBy.UseVisualStyleBackColor = True
        '
        'BtnSaveReportSettings
        '
        Me.BtnSaveReportSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSaveReportSettings.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSaveReportSettings.Location = New System.Drawing.Point(254, 35)
        Me.BtnSaveReportSettings.Name = "BtnSaveReportSettings"
        Me.BtnSaveReportSettings.Size = New System.Drawing.Size(114, 23)
        Me.BtnSaveReportSettings.TabIndex = 118
        Me.BtnSaveReportSettings.Text = "Save Settings"
        Me.BtnSaveReportSettings.UseVisualStyleBackColor = True
        '
        'BtnPrint
        '
        Me.BtnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPrint.Location = New System.Drawing.Point(14, 35)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(88, 23)
        Me.BtnPrint.TabIndex = 119
        Me.BtnPrint.Text = "Print"
        Me.BtnPrint.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.BtnNewUserReport)
        Me.GroupBox3.Controls.Add(Me.BtnUserReports)
        Me.GroupBox3.Controls.Add(Me.OptUserReport)
        Me.GroupBox3.Controls.Add(Me.OptSystemReport)
        Me.GroupBox3.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(6, -1)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(251, 63)
        Me.GroupBox3.TabIndex = 119
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Report Designed By"
        '
        'BtnNewUserReport
        '
        Me.BtnNewUserReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnNewUserReport.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnNewUserReport.Location = New System.Drawing.Point(189, 34)
        Me.BtnNewUserReport.Name = "BtnNewUserReport"
        Me.BtnNewUserReport.Size = New System.Drawing.Size(45, 21)
        Me.BtnNewUserReport.TabIndex = 123
        Me.BtnNewUserReport.Text = "New"
        Me.BtnNewUserReport.UseVisualStyleBackColor = True
        '
        'BtnUserReports
        '
        Me.BtnUserReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnUserReports.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnUserReports.Location = New System.Drawing.Point(155, 34)
        Me.BtnUserReports.Name = "BtnUserReports"
        Me.BtnUserReports.Size = New System.Drawing.Size(28, 21)
        Me.BtnUserReports.TabIndex = 122
        Me.BtnUserReports.Text = "..."
        Me.BtnUserReports.UseVisualStyleBackColor = True
        '
        'OptUserReport
        '
        Me.OptUserReport.AutoSize = True
        Me.OptUserReport.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptUserReport.Location = New System.Drawing.Point(14, 35)
        Me.OptUserReport.Name = "OptUserReport"
        Me.OptUserReport.Size = New System.Drawing.Size(58, 20)
        Me.OptUserReport.TabIndex = 121
        Me.OptUserReport.TabStop = True
        Me.OptUserReport.Text = "User"
        Me.OptUserReport.UseVisualStyleBackColor = True
        '
        'OptSystemReport
        '
        Me.OptSystemReport.AutoSize = True
        Me.OptSystemReport.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptSystemReport.Location = New System.Drawing.Point(14, 16)
        Me.OptSystemReport.Name = "OptSystemReport"
        Me.OptSystemReport.Size = New System.Drawing.Size(74, 20)
        Me.OptSystemReport.TabIndex = 120
        Me.OptSystemReport.TabStop = True
        Me.OptSystemReport.Text = "System"
        Me.OptSystemReport.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.OptSummaryReport)
        Me.GroupBox4.Controls.Add(Me.OptDetailedReport)
        Me.GroupBox4.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(302, -1)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(125, 63)
        Me.GroupBox4.TabIndex = 122
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Report Type"
        '
        'OptSummaryReport
        '
        Me.OptSummaryReport.AutoSize = True
        Me.OptSummaryReport.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptSummaryReport.Location = New System.Drawing.Point(12, 35)
        Me.OptSummaryReport.Name = "OptSummaryReport"
        Me.OptSummaryReport.Size = New System.Drawing.Size(82, 20)
        Me.OptSummaryReport.TabIndex = 121
        Me.OptSummaryReport.TabStop = True
        Me.OptSummaryReport.Text = "Summary"
        Me.OptSummaryReport.UseVisualStyleBackColor = True
        '
        'OptDetailedReport
        '
        Me.OptDetailedReport.AutoSize = True
        Me.OptDetailedReport.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptDetailedReport.Location = New System.Drawing.Point(12, 16)
        Me.OptDetailedReport.Name = "OptDetailedReport"
        Me.OptDetailedReport.Size = New System.Drawing.Size(90, 20)
        Me.OptDetailedReport.TabIndex = 120
        Me.OptDetailedReport.TabStop = True
        Me.OptDetailedReport.Text = "Detailed"
        Me.OptDetailedReport.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.BtnGo)
        Me.GroupBox5.Controls.Add(Me.BtnPrint)
        Me.GroupBox5.Location = New System.Drawing.Point(445, -1)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(120, 63)
        Me.GroupBox5.TabIndex = 123
        Me.GroupBox5.TabStop = False
        '
        'GrpBoxDataSorting
        '
        Me.GrpBoxDataSorting.Controls.Add(Me.Pnl5)
        Me.GrpBoxDataSorting.Controls.Add(Me.BtnOkDataSorting)
        Me.GrpBoxDataSorting.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpBoxDataSorting.Location = New System.Drawing.Point(6, 590)
        Me.GrpBoxDataSorting.Name = "GrpBoxDataSorting"
        Me.GrpBoxDataSorting.Size = New System.Drawing.Size(288, 168)
        Me.GrpBoxDataSorting.TabIndex = 124
        Me.GrpBoxDataSorting.TabStop = False
        Me.GrpBoxDataSorting.Text = "Data Sorting"
        Me.GrpBoxDataSorting.Visible = False
        '
        'Pnl5
        '
        Me.Pnl5.Location = New System.Drawing.Point(14, 29)
        Me.Pnl5.Name = "Pnl5"
        Me.Pnl5.Size = New System.Drawing.Size(259, 98)
        Me.Pnl5.TabIndex = 102
        '
        'BtnOkDataSorting
        '
        Me.BtnOkDataSorting.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOkDataSorting.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOkDataSorting.Location = New System.Drawing.Point(90, 133)
        Me.BtnOkDataSorting.Name = "BtnOkDataSorting"
        Me.BtnOkDataSorting.Size = New System.Drawing.Size(93, 23)
        Me.BtnOkDataSorting.TabIndex = 116
        Me.BtnOkDataSorting.Text = "OK"
        Me.BtnOkDataSorting.UseVisualStyleBackColor = True
        '
        'GrpDataGroups
        '
        Me.GrpDataGroups.Controls.Add(Me.PnlDataGroups)
        Me.GrpDataGroups.Controls.Add(Me.BtnDataGroup)
        Me.GrpDataGroups.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpDataGroups.Location = New System.Drawing.Point(12, 517)
        Me.GrpDataGroups.Name = "GrpDataGroups"
        Me.GrpDataGroups.Size = New System.Drawing.Size(288, 168)
        Me.GrpDataGroups.TabIndex = 125
        Me.GrpDataGroups.TabStop = False
        Me.GrpDataGroups.Text = "Data Groups"
        Me.GrpDataGroups.Visible = False
        '
        'PnlDataGroups
        '
        Me.PnlDataGroups.Location = New System.Drawing.Point(14, 29)
        Me.PnlDataGroups.Name = "PnlDataGroups"
        Me.PnlDataGroups.Size = New System.Drawing.Size(259, 98)
        Me.PnlDataGroups.TabIndex = 102
        '
        'BtnDataGroup
        '
        Me.BtnDataGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnDataGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDataGroup.Location = New System.Drawing.Point(90, 133)
        Me.BtnDataGroup.Name = "BtnDataGroup"
        Me.BtnDataGroup.Size = New System.Drawing.Size(93, 23)
        Me.BtnDataGroup.TabIndex = 116
        Me.BtnDataGroup.Text = "OK"
        Me.BtnDataGroup.UseVisualStyleBackColor = True
        '
        'Topctrl1
        '
        Me.Topctrl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Topctrl1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Topctrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Topctrl1.Location = New System.Drawing.Point(12, 730)
        Me.Topctrl1.Mode = "Add"
        Me.Topctrl1.Name = "Topctrl1"
        Me.Topctrl1.Size = New System.Drawing.Size(662, 41)
        Me.Topctrl1.TabIndex = 0
        Me.Topctrl1.tAdd = True
        Me.Topctrl1.tCancel = True
        Me.Topctrl1.tDel = True
        Me.Topctrl1.tDiscard = False
        Me.Topctrl1.tEdit = True
        Me.Topctrl1.tExit = True
        Me.Topctrl1.tFind = True
        Me.Topctrl1.tFirst = True
        Me.Topctrl1.tLast = True
        Me.Topctrl1.tNext = True
        Me.Topctrl1.tPrev = True
        Me.Topctrl1.tPrn = True
        Me.Topctrl1.tRef = True
        Me.Topctrl1.tSave = False
        Me.Topctrl1.tSite = True
        Me.Topctrl1.Visible = False
        '
        'FrmReportTool
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(992, 746)
        Me.Controls.Add(Me.GrpDataGroups)
        Me.Controls.Add(Me.GrpBoxPrintSettings)
        Me.Controls.Add(Me.GrpBoxDataSorting)
        Me.Controls.Add(Me.Topctrl1)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GrpBoxColumnSelection)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GrpBoxReportGroups)
        Me.Controls.Add(Me.TxtQuery)
        Me.Controls.Add(Me.CboDescription)
        Me.Controls.Add(Me.LblDescriptionReq)
        Me.Controls.Add(Me.GrpBoxReportCriteria)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.Name = "FrmReportTool"
        Me.Text = "Report"
        CType(Me.DgOutput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DgOutputTotals, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpBoxUserReports.ResumeLayout(False)
        Me.GrpBoxUserReports.PerformLayout()
        Me.GrpBoxReportCriteria.ResumeLayout(False)
        Me.GrpBoxColumnSelection.ResumeLayout(False)
        Me.GrpBoxPrintSettings.ResumeLayout(False)
        Me.GrpBoxReportGroups.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GrpBoxDataSorting.ResumeLayout(False)
        Me.GrpDataGroups.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Topctrl1 As Topctrl.Topctrl
    Friend WithEvents CboDescription As AgControls.AgComboBox
    Friend WithEvents LblDescription As System.Windows.Forms.Label
    Friend WithEvents LblDescriptionReq As System.Windows.Forms.Label
    Friend WithEvents TxtQuery As AgControls.AgTextBox
    Friend WithEvents LblQuery As System.Windows.Forms.Label
    Friend WithEvents LblQueryReq As System.Windows.Forms.Label
    Friend WithEvents Pnl3 As System.Windows.Forms.Panel
    Friend WithEvents Pnl4 As System.Windows.Forms.Panel
    Friend WithEvents BtnParseQuery As System.Windows.Forms.Button
    Friend WithEvents DgOutput As AgControls.AgDataGrid
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnGo As System.Windows.Forms.Button
    Friend WithEvents DgOutputTotals As AgControls.AgDataGrid
    Friend WithEvents Pnl1 As System.Windows.Forms.Panel
    Friend WithEvents BtnFilterRecords As System.Windows.Forms.Button
    Friend WithEvents BtnColumnSelection As System.Windows.Forms.Button
    Friend WithEvents BtnReportGroups As System.Windows.Forms.Button
    Friend WithEvents GrpBoxColumnSelection As System.Windows.Forms.GroupBox
    Friend WithEvents GrpBoxReportCriteria As System.Windows.Forms.GroupBox
    Friend WithEvents BtnOkColumnSelection As System.Windows.Forms.Button
    Friend WithEvents BtnOkReportCriteria As System.Windows.Forms.Button
    Friend WithEvents GrpBoxReportGroups As System.Windows.Forms.GroupBox
    Friend WithEvents BtnOkReportGroups As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents OptUserReport As System.Windows.Forms.RadioButton
    Friend WithEvents OptSystemReport As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents OptSummaryReport As System.Windows.Forms.RadioButton
    Friend WithEvents OptDetailedReport As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnSaveReportSettings As System.Windows.Forms.Button
    Friend WithEvents GrpBoxUserReports As System.Windows.Forms.GroupBox
    Friend WithEvents BtnUserReports As System.Windows.Forms.Button
    Friend WithEvents TxtUserReport As AgControls.AgTextBox
    Friend WithEvents Lbl As System.Windows.Forms.Label
    Friend WithEvents BtnOkUserReports As System.Windows.Forms.Button
    Friend WithEvents BtnCancelUserReport As System.Windows.Forms.Button
    Friend WithEvents BtnPrint As System.Windows.Forms.Button
    Friend WithEvents BtnOrderBy As System.Windows.Forms.Button
    Friend WithEvents GrpBoxPrintSettings As System.Windows.Forms.GroupBox
    Friend WithEvents BtnOkPrintSettings As System.Windows.Forms.Button
    Friend WithEvents Pnl2 As System.Windows.Forms.Panel
    Friend WithEvents BtnPrintSettings As System.Windows.Forms.Button
    Friend WithEvents GrpBoxDataSorting As System.Windows.Forms.GroupBox
    Friend WithEvents Pnl5 As System.Windows.Forms.Panel
    Friend WithEvents BtnOkDataSorting As System.Windows.Forms.Button
    Friend WithEvents GrpDataGroups As System.Windows.Forms.GroupBox
    Friend WithEvents PnlDataGroups As System.Windows.Forms.Panel
    Friend WithEvents BtnDataGroup As System.Windows.Forms.Button
    Friend WithEvents BtnNewUserReport As System.Windows.Forms.Button
End Class
