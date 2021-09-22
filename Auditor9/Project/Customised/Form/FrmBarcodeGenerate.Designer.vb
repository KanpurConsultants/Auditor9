<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBarcodeGenerate
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
        Me.LblTitle = New System.Windows.Forms.Label()
        Me.BtnPrintBarcode = New System.Windows.Forms.Button()
        Me.TxtSkipLables = New AgControls.AgTextBox()
        Me.LblBillNo = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(0, 585)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1015, 4)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(0, 42)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(984, 541)
        Me.Pnl1.TabIndex = 10
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.Location = New System.Drawing.Point(909, 595)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(64, 23)
        Me.BtnClose.TabIndex = 669
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.LblTitle)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(984, 41)
        Me.Panel1.TabIndex = 670
        '
        'LblTitle
        '
        Me.LblTitle.AutoSize = True
        Me.LblTitle.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTitle.Location = New System.Drawing.Point(16, 12)
        Me.LblTitle.Name = "LblTitle"
        Me.LblTitle.Size = New System.Drawing.Size(116, 16)
        Me.LblTitle.TabIndex = 11
        Me.LblTitle.Text = "Pending Records"
        '
        'BtnPrintBarcode
        '
        Me.BtnPrintBarcode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnPrintBarcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPrintBarcode.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPrintBarcode.Location = New System.Drawing.Point(12, 595)
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
        Me.TxtSkipLables.Location = New System.Drawing.Point(157, 595)
        Me.TxtSkipLables.MaxLength = 20
        Me.TxtSkipLables.Name = "TxtSkipLables"
        Me.TxtSkipLables.Size = New System.Drawing.Size(100, 21)
        Me.TxtSkipLables.TabIndex = 672
        '
        'LblBillNo
        '
        Me.LblBillNo.AutoSize = True
        Me.LblBillNo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBillNo.Location = New System.Drawing.Point(82, 600)
        Me.LblBillNo.Name = "LblBillNo"
        Me.LblBillNo.Size = New System.Drawing.Size(72, 13)
        Me.LblBillNo.TabIndex = 673
        Me.LblBillNo.Text = "Skip Lables"
        '
        'FrmBarcodeGenerate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 622)
        Me.Controls.Add(Me.LblBillNo)
        Me.Controls.Add(Me.TxtSkipLables)
        Me.Controls.Add(Me.BtnPrintBarcode)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.GroupBox1)
        Me.MaximizeBox = False
        Me.Name = "FrmBarcodeGenerate"
        Me.Text = "Generate Barcode"
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
    Friend WithEvents LblBillNo As Label
End Class
