<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintEnvelopePrint
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintEnvelopePrint))
        Me.pnlHeader = New System.Windows.Forms.Panel
        Me.lblHeading = New System.Windows.Forms.Label
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.pnlContent = New System.Windows.Forms.Panel
        Me.pnlForm = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblEnvelope = New System.Windows.Forms.Label
        Me.btnClose = New System.Windows.Forms.Button
        Me.cmbEnvelopeSize = New System.Windows.Forms.ComboBox
        Me.btnAddressSelect = New System.Windows.Forms.Button
        Me.lbAddress = New System.Windows.Forms.Label
        Me.btnEnvelopeSelect = New System.Windows.Forms.Button
        Me.cmbEnvelopeName = New System.Windows.Forms.ComboBox
        Me.btnPrint = New System.Windows.Forms.Button
        Me.pnlFooter = New System.Windows.Forms.Panel
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pnlHeader.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlContent.SuspendLayout()
        Me.pnlForm.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.AutoScroll = True
        Me.pnlHeader.Controls.Add(Me.lblHeading)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(728, 55)
        Me.pnlHeader.TabIndex = 0
        '
        'lblHeading
        '
        Me.lblHeading.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblHeading.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeading.ForeColor = System.Drawing.Color.OrangeRed
        Me.lblHeading.Location = New System.Drawing.Point(0, 0)
        Me.lblHeading.Name = "lblHeading"
        Me.lblHeading.Size = New System.Drawing.Size(728, 42)
        Me.lblHeading.TabIndex = 0
        Me.lblHeading.Text = "PRINT ENVELOPE"
        Me.lblHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlContent)
        Me.pnlMain.Controls.Add(Me.pnlFooter)
        Me.pnlMain.Controls.Add(Me.pnlHeader)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(728, 417)
        Me.pnlMain.TabIndex = 10
        '
        'pnlContent
        '
        Me.pnlContent.AutoScroll = True
        Me.pnlContent.Controls.Add(Me.pnlForm)
        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContent.Location = New System.Drawing.Point(0, 55)
        Me.pnlContent.Name = "pnlContent"
        Me.pnlContent.Size = New System.Drawing.Size(728, 352)
        Me.pnlContent.TabIndex = 0
        '
        'pnlForm
        '
        Me.pnlForm.Controls.Add(Me.Panel1)
        Me.pnlForm.Location = New System.Drawing.Point(20, 22)
        Me.pnlForm.Name = "pnlForm"
        Me.pnlForm.Size = New System.Drawing.Size(671, 301)
        Me.pnlForm.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.lblEnvelope)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.cmbEnvelopeSize)
        Me.Panel1.Controls.Add(Me.btnAddressSelect)
        Me.Panel1.Controls.Add(Me.lbAddress)
        Me.Panel1.Controls.Add(Me.btnEnvelopeSelect)
        Me.Panel1.Controls.Add(Me.cmbEnvelopeName)
        Me.Panel1.Controls.Add(Me.btnPrint)
        Me.Panel1.Location = New System.Drawing.Point(24, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(617, 256)
        Me.Panel1.TabIndex = 16
        '
        'lblEnvelope
        '
        Me.lblEnvelope.AutoSize = True
        Me.lblEnvelope.BackColor = System.Drawing.Color.Transparent
        Me.lblEnvelope.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEnvelope.Location = New System.Drawing.Point(100, 42)
        Me.lblEnvelope.Name = "lblEnvelope"
        Me.lblEnvelope.Size = New System.Drawing.Size(121, 16)
        Me.lblEnvelope.TabIndex = 6
        Me.lblEnvelope.Text = "Envelope Name"
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.Transparent
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.exit_32
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClose.Location = New System.Drawing.Point(312, 180)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(104, 38)
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "CLOSE"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'cmbEnvelopeSize
        '
        Me.cmbEnvelopeSize.FormattingEnabled = True
        Me.cmbEnvelopeSize.Location = New System.Drawing.Point(100, 58)
        Me.cmbEnvelopeSize.Name = "cmbEnvelopeSize"
        Me.cmbEnvelopeSize.Size = New System.Drawing.Size(347, 21)
        Me.cmbEnvelopeSize.TabIndex = 7
        '
        'btnAddressSelect
        '
        Me.btnAddressSelect.BackColor = System.Drawing.Color.Transparent
        Me.btnAddressSelect.BackgroundImage = Global.DenariuChequeWriterSoft.My.Resources.Resources.SETTINGS_ICON_64
        Me.btnAddressSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddressSelect.FlatAppearance.BorderSize = 0
        Me.btnAddressSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddressSelect.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddressSelect.Location = New System.Drawing.Point(453, 111)
        Me.btnAddressSelect.Name = "btnAddressSelect"
        Me.btnAddressSelect.Size = New System.Drawing.Size(64, 64)
        Me.btnAddressSelect.TabIndex = 12
        Me.btnAddressSelect.UseVisualStyleBackColor = False
        '
        'lbAddress
        '
        Me.lbAddress.AutoSize = True
        Me.lbAddress.BackColor = System.Drawing.Color.Transparent
        Me.lbAddress.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbAddress.Location = New System.Drawing.Point(100, 114)
        Me.lbAddress.Name = "lbAddress"
        Me.lbAddress.Size = New System.Drawing.Size(138, 16)
        Me.lbAddress.TabIndex = 8
        Me.lbAddress.Text = "Envelope Address"
        '
        'btnEnvelopeSelect
        '
        Me.btnEnvelopeSelect.BackColor = System.Drawing.Color.Transparent
        Me.btnEnvelopeSelect.BackgroundImage = Global.DenariuChequeWriterSoft.My.Resources.Resources.SETTINGS_ICON_64
        Me.btnEnvelopeSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnEnvelopeSelect.FlatAppearance.BorderSize = 0
        Me.btnEnvelopeSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEnvelopeSelect.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEnvelopeSelect.Location = New System.Drawing.Point(453, 39)
        Me.btnEnvelopeSelect.Name = "btnEnvelopeSelect"
        Me.btnEnvelopeSelect.Size = New System.Drawing.Size(64, 64)
        Me.btnEnvelopeSelect.TabIndex = 11
        Me.btnEnvelopeSelect.UseVisualStyleBackColor = False
        '
        'cmbEnvelopeName
        '
        Me.cmbEnvelopeName.FormattingEnabled = True
        Me.cmbEnvelopeName.Location = New System.Drawing.Point(100, 130)
        Me.cmbEnvelopeName.Name = "cmbEnvelopeName"
        Me.cmbEnvelopeName.Size = New System.Drawing.Size(347, 21)
        Me.cmbEnvelopeName.TabIndex = 9
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.Transparent
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.print_envelope_32
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPrint.Location = New System.Drawing.Point(206, 180)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(98, 38)
        Me.btnPrint.TabIndex = 10
        Me.btnPrint.Text = "PRINT"
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'pnlFooter
        '
        Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFooter.Location = New System.Drawing.Point(0, 407)
        Me.pnlFooter.Name = "pnlFooter"
        Me.pnlFooter.Size = New System.Drawing.Size(728, 10)
        Me.pnlFooter.TabIndex = 1
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'frmPrintEnvelopePrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(728, 417)
        Me.Controls.Add(Me.pnlMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmPrintEnvelopePrint"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.pnlContent.ResumeLayout(False)
        Me.pnlForm.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeading As System.Windows.Forms.Label
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlContent As System.Windows.Forms.Panel
    Friend WithEvents pnlForm As System.Windows.Forms.Panel
    Friend WithEvents pnlFooter As System.Windows.Forms.Panel
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblEnvelope As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents cmbEnvelopeSize As System.Windows.Forms.ComboBox
    Friend WithEvents btnAddressSelect As System.Windows.Forms.Button
    Friend WithEvents lbAddress As System.Windows.Forms.Label
    Friend WithEvents btnEnvelopeSelect As System.Windows.Forms.Button
    Friend WithEvents cmbEnvelopeName As System.Windows.Forms.ComboBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
End Class
