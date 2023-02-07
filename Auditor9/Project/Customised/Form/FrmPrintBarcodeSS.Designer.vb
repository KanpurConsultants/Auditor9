<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPrintBarcodeSS
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TxtBarcodeType = New AgControls.AgTextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.LblTitle = New System.Windows.Forms.Label()
        Me.BtnPrintBarcode = New System.Windows.Forms.Button()
        Me.TxtSkipLables = New AgControls.AgTextBox()
        Me.LblSkipLabels = New System.Windows.Forms.Label()
        Me.BtnPreview = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(0, 339)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1015, 4)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(0, 42)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(984, 293)
        Me.Pnl1.TabIndex = 10
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.Location = New System.Drawing.Point(909, 349)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(64, 23)
        Me.BtnClose.TabIndex = 669
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.TxtBarcodeType)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.LblTitle)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(984, 41)
        Me.Panel1.TabIndex = 670
        '
        'TxtBarcodeType
        '
        Me.TxtBarcodeType.AgAllowUserToEnableMasterHelp = False
        Me.TxtBarcodeType.AgLastValueTag = Nothing
        Me.TxtBarcodeType.AgLastValueText = Nothing
        Me.TxtBarcodeType.AgMandatory = True
        Me.TxtBarcodeType.AgMasterHelp = False
        Me.TxtBarcodeType.AgNumberLeftPlaces = 0
        Me.TxtBarcodeType.AgNumberNegetiveAllow = False
        Me.TxtBarcodeType.AgNumberRightPlaces = 0
        Me.TxtBarcodeType.AgPickFromLastValue = False
        Me.TxtBarcodeType.AgRowFilter = ""
        Me.TxtBarcodeType.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBarcodeType.AgSelectedValue = Nothing
        Me.TxtBarcodeType.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBarcodeType.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBarcodeType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtBarcodeType.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBarcodeType.Location = New System.Drawing.Point(471, 9)
        Me.TxtBarcodeType.MaxLength = 20
        Me.TxtBarcodeType.Name = "TxtBarcodeType"
        Me.TxtBarcodeType.Size = New System.Drawing.Size(113, 23)
        Me.TxtBarcodeType.TabIndex = 1059
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(356, 13)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(97, 14)
        Me.Label15.TabIndex = 1058
        Me.Label15.Text = "Barcode Type"
        '
        'LblTitle
        '
        Me.LblTitle.AutoSize = True
        Me.LblTitle.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTitle.Location = New System.Drawing.Point(16, 12)
        Me.LblTitle.Name = "LblTitle"
        Me.LblTitle.Size = New System.Drawing.Size(35, 16)
        Me.LblTitle.TabIndex = 11
        Me.LblTitle.Text = "Title"
        Me.LblTitle.Visible = False
        '
        'BtnPrintBarcode
        '
        Me.BtnPrintBarcode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnPrintBarcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPrintBarcode.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPrintBarcode.Location = New System.Drawing.Point(12, 349)
        Me.BtnPrintBarcode.Name = "BtnPrintBarcode"
        Me.BtnPrintBarcode.Size = New System.Drawing.Size(64, 23)
        Me.BtnPrintBarcode.TabIndex = 671
        Me.BtnPrintBarcode.Text = "Print"
        Me.BtnPrintBarcode.UseVisualStyleBackColor = True
        '
        'TxtSkipLables
        '
        Me.TxtSkipLables.AgAllowUserToEnableMasterHelp = False
        Me.TxtSkipLables.AgLastValueTag = Nothing
        Me.TxtSkipLables.AgLastValueText = Nothing
        Me.TxtSkipLables.AgMandatory = False
        Me.TxtSkipLables.AgMasterHelp = False
        Me.TxtSkipLables.AgNumberLeftPlaces = 2
        Me.TxtSkipLables.AgNumberNegetiveAllow = False
        Me.TxtSkipLables.AgNumberRightPlaces = 0
        Me.TxtSkipLables.AgPickFromLastValue = False
        Me.TxtSkipLables.AgRowFilter = ""
        Me.TxtSkipLables.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSkipLables.AgSelectedValue = Nothing
        Me.TxtSkipLables.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSkipLables.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtSkipLables.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSkipLables.Location = New System.Drawing.Point(872, 11)
        Me.TxtSkipLables.MaxLength = 20
        Me.TxtSkipLables.Name = "TxtSkipLables"
        Me.TxtSkipLables.Size = New System.Drawing.Size(100, 21)
        Me.TxtSkipLables.TabIndex = 672
        Me.TxtSkipLables.Visible = False
        '
        'LblSkipLabels
        '
        Me.LblSkipLabels.AutoSize = True
        Me.LblSkipLabels.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSkipLabels.Location = New System.Drawing.Point(785, 15)
        Me.LblSkipLabels.Name = "LblSkipLabels"
        Me.LblSkipLabels.Size = New System.Drawing.Size(81, 13)
        Me.LblSkipLabels.TabIndex = 673
        Me.LblSkipLabels.Text = "Skip Lables"
        Me.LblSkipLabels.Visible = False
        '
        'BtnPreview
        '
        Me.BtnPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPreview.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPreview.Location = New System.Drawing.Point(82, 349)
        Me.BtnPreview.Name = "BtnPreview"
        Me.BtnPreview.Size = New System.Drawing.Size(78, 23)
        Me.BtnPreview.TabIndex = 674
        Me.BtnPreview.Text = "Preview"
        Me.BtnPreview.UseVisualStyleBackColor = True
        '
        'FrmPrintBarcodeSS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 376)
        Me.Controls.Add(Me.BtnPreview)
        Me.Controls.Add(Me.LblSkipLabels)
        Me.Controls.Add(Me.TxtSkipLables)
        Me.Controls.Add(Me.BtnPrintBarcode)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmPrintBarcodeSS"
        Me.Text = "Print Barcode"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents Pnl1 As System.Windows.Forms.Panel
    Public WithEvents BtnClose As System.Windows.Forms.Button
    Public WithEvents Panel1 As Panel
    Public WithEvents LblTitle As Label
    Public WithEvents BtnPrintBarcode As Button
    Friend WithEvents TxtSkipLables As AgControls.AgTextBox
    Friend WithEvents LblSkipLabels As Label
    Public WithEvents BtnPreview As Button
    Public WithEvents TxtBarcodeType As AgControls.AgTextBox
    Public WithEvents Label15 As Label
End Class
