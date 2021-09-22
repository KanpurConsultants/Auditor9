<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEnvelopeSizeNew
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEnvelopeSizeNew))
        Me.pnlHeader = New System.Windows.Forms.Panel
        Me.lblHeading = New System.Windows.Forms.Label
        Me.btnView = New System.Windows.Forms.Button
        Me.txtFilePath = New System.Windows.Forms.TextBox
        Me.btnDelete = New System.Windows.Forms.Button
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.pnlContent = New System.Windows.Forms.Panel
        Me.pnlForm = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtCountry = New System.Windows.Forms.TextBox
        Me.lblCountry = New System.Windows.Forms.Label
        Me.txtFileName = New System.Windows.Forms.TextBox
        Me.txtId = New System.Windows.Forms.TextBox
        Me.pnlToolBar = New System.Windows.Forms.Panel
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnNew = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnSearch1 = New System.Windows.Forms.Button
        Me.txtCity = New System.Windows.Forms.TextBox
        Me.lblCity = New System.Windows.Forms.Label
        Me.txtPostalCode = New System.Windows.Forms.TextBox
        Me.lblPostalCode = New System.Windows.Forms.Label
        Me.txtAddressLin2 = New System.Windows.Forms.TextBox
        Me.lblAddressLine2 = New System.Windows.Forms.Label
        Me.txtAddressLine1 = New System.Windows.Forms.TextBox
        Me.lblAddressLine1 = New System.Windows.Forms.Label
        Me.txtRecipientName = New System.Windows.Forms.TextBox
        Me.lblRecipientName = New System.Windows.Forms.Label
        Me.txtEnvelopeName = New System.Windows.Forms.TextBox
        Me.lblEnvelopeName = New System.Windows.Forms.Label
        Me.txtSearchbox = New System.Windows.Forms.TextBox
        Me.txtCode = New System.Windows.Forms.TextBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.pnlFooter = New System.Windows.Forms.Panel
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.txtCompanyId = New System.Windows.Forms.TextBox
        Me.pnlHeader.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        Me.pnlContent.SuspendLayout()
        Me.pnlForm.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlToolBar.SuspendLayout()
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
        Me.pnlHeader.Size = New System.Drawing.Size(804, 55)
        Me.pnlHeader.TabIndex = 0
        '
        'lblHeading
        '
        Me.lblHeading.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblHeading.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeading.ForeColor = System.Drawing.Color.OrangeRed
        Me.lblHeading.Location = New System.Drawing.Point(0, 0)
        Me.lblHeading.Name = "lblHeading"
        Me.lblHeading.Size = New System.Drawing.Size(804, 42)
        Me.lblHeading.TabIndex = 0
        Me.lblHeading.Text = "ENVELOPE ADDRESS"
        Me.lblHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnView
        '
        Me.btnView.BackColor = System.Drawing.Color.White
        Me.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnView.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold)
        Me.btnView.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.report_32
        Me.btnView.Location = New System.Drawing.Point(356, 8)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(107, 49)
        Me.btnView.TabIndex = 2
        Me.btnView.Text = " &VIEW"
        Me.btnView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnView.UseVisualStyleBackColor = False
        '
        'txtFilePath
        '
        Me.txtFilePath.Location = New System.Drawing.Point(346, 273)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.Size = New System.Drawing.Size(49, 20)
        Me.txtFilePath.TabIndex = 87
        Me.txtFilePath.Visible = False
        '
        'btnDelete
        '
        Me.btnDelete.BackColor = System.Drawing.Color.White
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDelete.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDelete.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.delete_32
        Me.btnDelete.Location = New System.Drawing.Point(241, 8)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(107, 49)
        Me.btnDelete.TabIndex = 14
        Me.btnDelete.Text = "&DELETE"
        Me.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlContent)
        Me.pnlMain.Controls.Add(Me.pnlFooter)
        Me.pnlMain.Controls.Add(Me.pnlHeader)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(804, 484)
        Me.pnlMain.TabIndex = 10
        '
        'pnlContent
        '
        Me.pnlContent.AutoScroll = True
        Me.pnlContent.Controls.Add(Me.pnlForm)
        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContent.Location = New System.Drawing.Point(0, 55)
        Me.pnlContent.Name = "pnlContent"
        Me.pnlContent.Size = New System.Drawing.Size(804, 419)
        Me.pnlContent.TabIndex = 0
        '
        'pnlForm
        '
        Me.pnlForm.Controls.Add(Me.Panel1)
        Me.pnlForm.Location = New System.Drawing.Point(20, 15)
        Me.pnlForm.Name = "pnlForm"
        Me.pnlForm.Size = New System.Drawing.Size(768, 390)
        Me.pnlForm.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.txtCompanyId)
        Me.Panel1.Controls.Add(Me.txtCountry)
        Me.Panel1.Controls.Add(Me.lblCountry)
        Me.Panel1.Controls.Add(Me.txtFilePath)
        Me.Panel1.Controls.Add(Me.txtFileName)
        Me.Panel1.Controls.Add(Me.txtId)
        Me.Panel1.Controls.Add(Me.pnlToolBar)
        Me.Panel1.Controls.Add(Me.txtCity)
        Me.Panel1.Controls.Add(Me.lblCity)
        Me.Panel1.Controls.Add(Me.txtPostalCode)
        Me.Panel1.Controls.Add(Me.lblPostalCode)
        Me.Panel1.Controls.Add(Me.txtAddressLin2)
        Me.Panel1.Controls.Add(Me.lblAddressLine2)
        Me.Panel1.Controls.Add(Me.txtAddressLine1)
        Me.Panel1.Controls.Add(Me.lblAddressLine1)
        Me.Panel1.Controls.Add(Me.txtRecipientName)
        Me.Panel1.Controls.Add(Me.lblRecipientName)
        Me.Panel1.Controls.Add(Me.txtEnvelopeName)
        Me.Panel1.Controls.Add(Me.lblEnvelopeName)
        Me.Panel1.Controls.Add(Me.txtSearchbox)
        Me.Panel1.Controls.Add(Me.txtCode)
        Me.Panel1.Controls.Add(Me.btnSearch)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(739, 360)
        Me.Panel1.TabIndex = 0
        '
        'txtCountry
        '
        Me.txtCountry.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCountry.Location = New System.Drawing.Point(266, 221)
        Me.txtCountry.MaxLength = 50
        Me.txtCountry.Name = "txtCountry"
        Me.txtCountry.Size = New System.Drawing.Size(321, 22)
        Me.txtCountry.TabIndex = 13
        '
        'lblCountry
        '
        Me.lblCountry.AutoSize = True
        Me.lblCountry.BackColor = System.Drawing.Color.Transparent
        Me.lblCountry.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCountry.ForeColor = System.Drawing.Color.Black
        Me.lblCountry.Location = New System.Drawing.Point(151, 224)
        Me.lblCountry.Name = "lblCountry"
        Me.lblCountry.Size = New System.Drawing.Size(57, 16)
        Me.lblCountry.TabIndex = 12
        Me.lblCountry.Text = "Country"
        '
        'txtFileName
        '
        Me.txtFileName.Location = New System.Drawing.Point(236, 279)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(45, 20)
        Me.txtFileName.TabIndex = 15
        Me.txtFileName.Visible = False
        '
        'txtId
        '
        Me.txtId.Location = New System.Drawing.Point(256, 279)
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(45, 20)
        Me.txtId.TabIndex = 81
        Me.txtId.Visible = False
        '
        'pnlToolBar
        '
        Me.pnlToolBar.BackColor = System.Drawing.Color.Transparent
        Me.pnlToolBar.Controls.Add(Me.btnExit)
        Me.pnlToolBar.Controls.Add(Me.btnNew)
        Me.pnlToolBar.Controls.Add(Me.btnSave)
        Me.pnlToolBar.Controls.Add(Me.btnSearch1)
        Me.pnlToolBar.Controls.Add(Me.btnView)
        Me.pnlToolBar.Controls.Add(Me.btnDelete)
        Me.pnlToolBar.Location = New System.Drawing.Point(17, 273)
        Me.pnlToolBar.Name = "pnlToolBar"
        Me.pnlToolBar.Size = New System.Drawing.Size(699, 67)
        Me.pnlToolBar.TabIndex = 14
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.White
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnExit.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.exit_32
        Me.btnExit.Location = New System.Drawing.Point(471, 8)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(107, 49)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = " E&XIT"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnExit.UseVisualStyleBackColor = False
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.Color.White
        Me.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnNew.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNew.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.new_32
        Me.btnNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNew.Location = New System.Drawing.Point(11, 8)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(107, 49)
        Me.btnNew.TabIndex = 1
        Me.btnNew.Text = "&NEW"
        Me.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.BackColor = System.Drawing.Color.White
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.save_32
        Me.btnSave.Location = New System.Drawing.Point(126, 8)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(107, 49)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "&SAVE"
        Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnSearch1
        '
        Me.btnSearch1.BackColor = System.Drawing.Color.White
        Me.btnSearch1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSearch1.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch1.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.search_32
        Me.btnSearch1.Location = New System.Drawing.Point(586, 8)
        Me.btnSearch1.Name = "btnSearch1"
        Me.btnSearch1.Size = New System.Drawing.Size(107, 49)
        Me.btnSearch1.TabIndex = 4
        Me.btnSearch1.Text = "SEARC&H"
        Me.btnSearch1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSearch1.UseVisualStyleBackColor = False
        '
        'txtCity
        '
        Me.txtCity.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCity.Location = New System.Drawing.Point(266, 189)
        Me.txtCity.MaxLength = 50
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(321, 22)
        Me.txtCity.TabIndex = 11
        '
        'lblCity
        '
        Me.lblCity.AutoSize = True
        Me.lblCity.BackColor = System.Drawing.Color.Transparent
        Me.lblCity.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCity.ForeColor = System.Drawing.Color.Black
        Me.lblCity.Location = New System.Drawing.Point(151, 192)
        Me.lblCity.Name = "lblCity"
        Me.lblCity.Size = New System.Drawing.Size(32, 16)
        Me.lblCity.TabIndex = 10
        Me.lblCity.Text = "City"
        '
        'txtPostalCode
        '
        Me.txtPostalCode.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPostalCode.Location = New System.Drawing.Point(266, 157)
        Me.txtPostalCode.MaxLength = 15
        Me.txtPostalCode.Name = "txtPostalCode"
        Me.txtPostalCode.Size = New System.Drawing.Size(321, 22)
        Me.txtPostalCode.TabIndex = 9
        '
        'lblPostalCode
        '
        Me.lblPostalCode.AutoSize = True
        Me.lblPostalCode.BackColor = System.Drawing.Color.Transparent
        Me.lblPostalCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPostalCode.ForeColor = System.Drawing.Color.Black
        Me.lblPostalCode.Location = New System.Drawing.Point(151, 160)
        Me.lblPostalCode.Name = "lblPostalCode"
        Me.lblPostalCode.Size = New System.Drawing.Size(84, 16)
        Me.lblPostalCode.TabIndex = 8
        Me.lblPostalCode.Text = "Postal Code"
        '
        'txtAddressLin2
        '
        Me.txtAddressLin2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddressLin2.Location = New System.Drawing.Point(266, 125)
        Me.txtAddressLin2.MaxLength = 50
        Me.txtAddressLin2.Name = "txtAddressLin2"
        Me.txtAddressLin2.Size = New System.Drawing.Size(321, 22)
        Me.txtAddressLin2.TabIndex = 7
        '
        'lblAddressLine2
        '
        Me.lblAddressLine2.AutoSize = True
        Me.lblAddressLine2.BackColor = System.Drawing.Color.Transparent
        Me.lblAddressLine2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddressLine2.ForeColor = System.Drawing.Color.Black
        Me.lblAddressLine2.Location = New System.Drawing.Point(151, 128)
        Me.lblAddressLine2.Name = "lblAddressLine2"
        Me.lblAddressLine2.Size = New System.Drawing.Size(97, 16)
        Me.lblAddressLine2.TabIndex = 6
        Me.lblAddressLine2.Text = "Address Line2"
        '
        'txtAddressLine1
        '
        Me.txtAddressLine1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddressLine1.Location = New System.Drawing.Point(266, 93)
        Me.txtAddressLine1.MaxLength = 50
        Me.txtAddressLine1.Name = "txtAddressLine1"
        Me.txtAddressLine1.Size = New System.Drawing.Size(321, 22)
        Me.txtAddressLine1.TabIndex = 5
        '
        'lblAddressLine1
        '
        Me.lblAddressLine1.AutoSize = True
        Me.lblAddressLine1.BackColor = System.Drawing.Color.Transparent
        Me.lblAddressLine1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddressLine1.ForeColor = System.Drawing.Color.Black
        Me.lblAddressLine1.Location = New System.Drawing.Point(151, 96)
        Me.lblAddressLine1.Name = "lblAddressLine1"
        Me.lblAddressLine1.Size = New System.Drawing.Size(97, 16)
        Me.lblAddressLine1.TabIndex = 4
        Me.lblAddressLine1.Text = "Address Line1"
        '
        'txtRecipientName
        '
        Me.txtRecipientName.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRecipientName.Location = New System.Drawing.Point(266, 61)
        Me.txtRecipientName.MaxLength = 50
        Me.txtRecipientName.Name = "txtRecipientName"
        Me.txtRecipientName.Size = New System.Drawing.Size(321, 22)
        Me.txtRecipientName.TabIndex = 3
        '
        'lblRecipientName
        '
        Me.lblRecipientName.AutoSize = True
        Me.lblRecipientName.BackColor = System.Drawing.Color.Transparent
        Me.lblRecipientName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRecipientName.ForeColor = System.Drawing.Color.Black
        Me.lblRecipientName.Location = New System.Drawing.Point(151, 64)
        Me.lblRecipientName.Name = "lblRecipientName"
        Me.lblRecipientName.Size = New System.Drawing.Size(109, 16)
        Me.lblRecipientName.TabIndex = 2
        Me.lblRecipientName.Text = "Recipient Name"
        '
        'txtEnvelopeName
        '
        Me.txtEnvelopeName.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEnvelopeName.Location = New System.Drawing.Point(266, 29)
        Me.txtEnvelopeName.MaxLength = 50
        Me.txtEnvelopeName.Name = "txtEnvelopeName"
        Me.txtEnvelopeName.Size = New System.Drawing.Size(321, 22)
        Me.txtEnvelopeName.TabIndex = 1
        '
        'lblEnvelopeName
        '
        Me.lblEnvelopeName.AutoSize = True
        Me.lblEnvelopeName.BackColor = System.Drawing.Color.Transparent
        Me.lblEnvelopeName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEnvelopeName.ForeColor = System.Drawing.Color.Black
        Me.lblEnvelopeName.Location = New System.Drawing.Point(151, 32)
        Me.lblEnvelopeName.Name = "lblEnvelopeName"
        Me.lblEnvelopeName.Size = New System.Drawing.Size(108, 16)
        Me.lblEnvelopeName.TabIndex = 0
        Me.lblEnvelopeName.Text = "Envelope Name"
        '
        'txtSearchbox
        '
        Me.txtSearchbox.Location = New System.Drawing.Point(560, 153)
        Me.txtSearchbox.Name = "txtSearchbox"
        Me.txtSearchbox.Size = New System.Drawing.Size(123, 20)
        Me.txtSearchbox.TabIndex = 83
        Me.txtSearchbox.Visible = False
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(381, 333)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(123, 20)
        Me.txtCode.TabIndex = 82
        Me.txtCode.Visible = False
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.White
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSearch.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSearch.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.search25
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSearch.Location = New System.Drawing.Point(560, 98)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(66, 49)
        Me.btnSearch.TabIndex = 84
        Me.btnSearch.Text = "SEARC&H"
        Me.btnSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnSearch.UseVisualStyleBackColor = False
        Me.btnSearch.Visible = False
        '
        'pnlFooter
        '
        Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFooter.Location = New System.Drawing.Point(0, 474)
        Me.pnlFooter.Name = "pnlFooter"
        Me.pnlFooter.Size = New System.Drawing.Size(804, 10)
        Me.pnlFooter.TabIndex = 1
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'txtCompanyId
        '
        Me.txtCompanyId.Location = New System.Drawing.Point(618, 220)
        Me.txtCompanyId.Name = "txtCompanyId"
        Me.txtCompanyId.Size = New System.Drawing.Size(49, 20)
        Me.txtCompanyId.TabIndex = 102
        Me.txtCompanyId.Visible = False
        '
        'frmEnvelopeSizeNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(804, 484)
        Me.Controls.Add(Me.pnlMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmEnvelopeSizeNew"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        Me.pnlContent.ResumeLayout(False)
        Me.pnlForm.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlToolBar.ResumeLayout(False)
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeading As System.Windows.Forms.Label
    Friend WithEvents btnView As System.Windows.Forms.Button
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlContent As System.Windows.Forms.Panel
    Friend WithEvents pnlForm As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents txtId As System.Windows.Forms.TextBox
    Friend WithEvents pnlToolBar As System.Windows.Forms.Panel
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnSearch1 As System.Windows.Forms.Button
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents lblCity As System.Windows.Forms.Label
    Friend WithEvents txtPostalCode As System.Windows.Forms.TextBox
    Friend WithEvents lblPostalCode As System.Windows.Forms.Label
    Friend WithEvents txtAddressLin2 As System.Windows.Forms.TextBox
    Friend WithEvents lblAddressLine2 As System.Windows.Forms.Label
    Friend WithEvents txtAddressLine1 As System.Windows.Forms.TextBox
    Friend WithEvents lblAddressLine1 As System.Windows.Forms.Label
    Friend WithEvents txtRecipientName As System.Windows.Forms.TextBox
    Friend WithEvents lblRecipientName As System.Windows.Forms.Label
    Friend WithEvents txtEnvelopeName As System.Windows.Forms.TextBox
    Friend WithEvents lblEnvelopeName As System.Windows.Forms.Label
    Friend WithEvents txtSearchbox As System.Windows.Forms.TextBox
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents pnlFooter As System.Windows.Forms.Panel
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents txtCountry As System.Windows.Forms.TextBox
    Friend WithEvents lblCountry As System.Windows.Forms.Label
    Friend WithEvents txtCompanyId As System.Windows.Forms.TextBox
End Class
