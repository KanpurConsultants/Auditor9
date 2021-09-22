<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmQuery
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.TxtQuery = New System.Windows.Forms.TextBox()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.DGL1 = New System.Windows.Forms.DataGridView()
        Me.LblMessage = New System.Windows.Forms.Label()
        Me.BtnGo = New System.Windows.Forms.Button()
        Me.TxtPassword = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.MnuMain = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuGenerate = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuInsert = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuWhereIn = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuExportToExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuFilter = New System.Windows.Forms.ToolStripMenuItem()
        Me.Dgl2 = New System.Windows.Forms.DataGridView()
        Me.MnuShowColumnTotals = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.DGL1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MnuMain.SuspendLayout()
        CType(Me.Dgl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtQuery
        '
        Me.TxtQuery.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtQuery.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtQuery.Location = New System.Drawing.Point(0, 0)
        Me.TxtQuery.MaxLength = 0
        Me.TxtQuery.Multiline = True
        Me.TxtQuery.Name = "TxtQuery"
        Me.TxtQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TxtQuery.Size = New System.Drawing.Size(731, 113)
        Me.TxtQuery.TabIndex = 0
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter1.Location = New System.Drawing.Point(0, 114)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(731, 279)
        Me.Splitter1.TabIndex = 1
        Me.Splitter1.TabStop = False
        '
        'DGL1
        '
        Me.DGL1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGL1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGL1.Location = New System.Drawing.Point(0, 114)
        Me.DGL1.Name = "DGL1"
        Me.DGL1.Size = New System.Drawing.Size(731, 216)
        Me.DGL1.TabIndex = 2
        '
        'LblMessage
        '
        Me.LblMessage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LblMessage.AutoSize = True
        Me.LblMessage.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMessage.Location = New System.Drawing.Point(0, 357)
        Me.LblMessage.Name = "LblMessage"
        Me.LblMessage.Size = New System.Drawing.Size(11, 14)
        Me.LblMessage.TabIndex = 3
        Me.LblMessage.Text = "."
        '
        'BtnGo
        '
        Me.BtnGo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnGo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnGo.Location = New System.Drawing.Point(644, 355)
        Me.BtnGo.Name = "BtnGo"
        Me.BtnGo.Size = New System.Drawing.Size(75, 23)
        Me.BtnGo.TabIndex = 4
        Me.BtnGo.Text = "GO"
        Me.BtnGo.UseVisualStyleBackColor = True
        '
        'TxtPassword
        '
        Me.TxtPassword.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtPassword.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPassword.Location = New System.Drawing.Point(481, 357)
        Me.TxtPassword.Name = "TxtPassword"
        Me.TxtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtPassword.Size = New System.Drawing.Size(131, 21)
        Me.TxtPassword.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(401, 359)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 14)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Password"
        '
        'MnuMain
        '
        Me.MnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuGenerate, Me.MnuExportToExcel, Me.MnuFilter, Me.MnuShowColumnTotals})
        Me.MnuMain.Name = "CMSMain"
        Me.MnuMain.ShowImageMargin = False
        Me.MnuMain.Size = New System.Drawing.Size(159, 114)
        '
        'MnuGenerate
        '
        Me.MnuGenerate.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuInsert, Me.MnuWhereIn})
        Me.MnuGenerate.Name = "MnuGenerate"
        Me.MnuGenerate.Size = New System.Drawing.Size(158, 22)
        Me.MnuGenerate.Text = "Generate"
        '
        'MnuInsert
        '
        Me.MnuInsert.Name = "MnuInsert"
        Me.MnuInsert.Size = New System.Drawing.Size(132, 22)
        Me.MnuInsert.Text = "Insert"
        '
        'MnuWhereIn
        '
        Me.MnuWhereIn.Name = "MnuWhereIn"
        Me.MnuWhereIn.Size = New System.Drawing.Size(132, 22)
        Me.MnuWhereIn.Text = "Where In ()"
        '
        'MnuExportToExcel
        '
        Me.MnuExportToExcel.Name = "MnuExportToExcel"
        Me.MnuExportToExcel.Size = New System.Drawing.Size(158, 22)
        Me.MnuExportToExcel.Text = "Export To Excel"
        '
        'MnuFilter
        '
        Me.MnuFilter.Name = "MnuFilter"
        Me.MnuFilter.Size = New System.Drawing.Size(158, 22)
        Me.MnuFilter.Text = "Filter"
        '
        'Dgl2
        '
        Me.Dgl2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Dgl2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Dgl2.Location = New System.Drawing.Point(0, 332)
        Me.Dgl2.Name = "Dgl2"
        Me.Dgl2.Size = New System.Drawing.Size(731, 22)
        Me.Dgl2.TabIndex = 7
        '
        'MnuShowColumnTotals
        '
        Me.MnuShowColumnTotals.Name = "MnuShowColumnTotals"
        Me.MnuShowColumnTotals.Size = New System.Drawing.Size(158, 22)
        Me.MnuShowColumnTotals.Text = "Show Column Totals"
        '
        'FrmQuery
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(731, 393)
        Me.Controls.Add(Me.Dgl2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtPassword)
        Me.Controls.Add(Me.BtnGo)
        Me.Controls.Add(Me.LblMessage)
        Me.Controls.Add(Me.DGL1)
        Me.Controls.Add(Me.Splitter1)
        Me.Controls.Add(Me.TxtQuery)
        Me.KeyPreview = True
        Me.Name = "FrmQuery"
        Me.Text = "FrmQuery"
        CType(Me.DGL1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MnuMain.ResumeLayout(False)
        CType(Me.Dgl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtQuery As TextBox
    Friend WithEvents Splitter1 As Splitter
    Friend WithEvents DGL1 As DataGridView
    Friend WithEvents LblMessage As Label
    Friend WithEvents BtnGo As Button
    Friend WithEvents TxtPassword As TextBox
    Friend WithEvents Label2 As Label
    Public WithEvents MnuMain As ContextMenuStrip
    Public WithEvents MnuGenerate As ToolStripMenuItem
    Friend WithEvents MnuInsert As ToolStripMenuItem
    Friend WithEvents MnuWhereIn As ToolStripMenuItem
    Public WithEvents MnuExportToExcel As ToolStripMenuItem
    Public WithEvents MnuFilter As ToolStripMenuItem
    Friend WithEvents Dgl2 As DataGridView
    Friend WithEvents MnuShowColumnTotals As ToolStripMenuItem
End Class
