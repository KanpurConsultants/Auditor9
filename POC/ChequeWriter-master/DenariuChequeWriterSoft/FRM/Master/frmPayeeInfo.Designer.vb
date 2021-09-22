<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPayeeInfo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPayeeInfo))
        Me.pnlHeader = New System.Windows.Forms.Panel
        Me.lblHeading = New System.Windows.Forms.Label
        Me.pnlContent = New System.Windows.Forms.Panel
        Me.pnlForm = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtCompanyId = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtFilePath = New System.Windows.Forms.TextBox
        Me.txtFileName = New System.Windows.Forms.TextBox
        Me.txtId = New System.Windows.Forms.TextBox
        Me.pnlToolBar = New System.Windows.Forms.Panel
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnNew = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnSearch1 = New System.Windows.Forms.Button
        Me.btnView = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.txtEMailId = New System.Windows.Forms.TextBox
        Me.lblEMailId = New System.Windows.Forms.Label
        Me.txtMobileNo = New System.Windows.Forms.TextBox
        Me.lblMobileNo = New System.Windows.Forms.Label
        Me.txtPhoneNo = New System.Windows.Forms.TextBox
        Me.lblPhoneNo = New System.Windows.Forms.Label
        Me.txtAddressLine2 = New System.Windows.Forms.TextBox
        Me.lblAddressLine2 = New System.Windows.Forms.Label
        Me.txtAddressLine1 = New System.Windows.Forms.TextBox
        Me.lblAddressLine1 = New System.Windows.Forms.Label
        Me.txtPayeeName = New System.Windows.Forms.TextBox
        Me.lblCompanyName = New System.Windows.Forms.Label
        Me.txtSearchbox = New System.Windows.Forms.TextBox
        Me.txtCode = New System.Windows.Forms.TextBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.pnlFooter = New System.Windows.Forms.Panel
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.pnlHeader.SuspendLayout()
        Me.pnlContent.SuspendLayout()
        Me.pnlForm.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlToolBar.SuspendLayout()
        Me.pnlMain.SuspendLayout()
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
        Me.pnlHeader.Size = New System.Drawing.Size(781, 55)
        Me.pnlHeader.TabIndex = 0
        '
        'lblHeading
        '
        Me.lblHeading.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblHeading.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeading.ForeColor = System.Drawing.Color.OrangeRed
        Me.lblHeading.Location = New System.Drawing.Point(0, 0)
        Me.lblHeading.Name = "lblHeading"
        Me.lblHeading.Size = New System.Drawing.Size(781, 42)
        Me.lblHeading.TabIndex = 0
        Me.lblHeading.Text = "PAYEE REGISTRATION"
        Me.lblHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlContent
        '
        Me.pnlContent.AutoScroll = True
        Me.pnlContent.Controls.Add(Me.pnlForm)
        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContent.Location = New System.Drawing.Point(0, 55)
        Me.pnlContent.Name = "pnlContent"
        Me.pnlContent.Size = New System.Drawing.Size(781, 371)
        Me.pnlContent.TabIndex = 0
        '
        'pnlForm
        '
        Me.pnlForm.Controls.Add(Me.Panel1)
        Me.pnlForm.Location = New System.Drawing.Point(10, 9)
        Me.pnlForm.Name = "pnlForm"
        Me.pnlForm.Size = New System.Drawing.Size(758, 350)
        Me.pnlForm.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.txtCompanyId)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.txtFilePath)
        Me.Panel1.Controls.Add(Me.txtFileName)
        Me.Panel1.Controls.Add(Me.txtId)
        Me.Panel1.Controls.Add(Me.pnlToolBar)
        Me.Panel1.Controls.Add(Me.txtEMailId)
        Me.Panel1.Controls.Add(Me.lblEMailId)
        Me.Panel1.Controls.Add(Me.txtMobileNo)
        Me.Panel1.Controls.Add(Me.lblMobileNo)
        Me.Panel1.Controls.Add(Me.txtPhoneNo)
        Me.Panel1.Controls.Add(Me.lblPhoneNo)
        Me.Panel1.Controls.Add(Me.txtAddressLine2)
        Me.Panel1.Controls.Add(Me.lblAddressLine2)
        Me.Panel1.Controls.Add(Me.txtAddressLine1)
        Me.Panel1.Controls.Add(Me.lblAddressLine1)
        Me.Panel1.Controls.Add(Me.txtPayeeName)
        Me.Panel1.Controls.Add(Me.lblCompanyName)
        Me.Panel1.Controls.Add(Me.txtSearchbox)
        Me.Panel1.Controls.Add(Me.txtCode)
        Me.Panel1.Controls.Add(Me.btnSearch)
        Me.Panel1.Location = New System.Drawing.Point(10, 9)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(739, 329)
        Me.Panel1.TabIndex = 26
        '
        'txtCompanyId
        '
        Me.txtCompanyId.Location = New System.Drawing.Point(655, 160)
        Me.txtCompanyId.Name = "txtCompanyId"
        Me.txtCompanyId.Size = New System.Drawing.Size(49, 20)
        Me.txtCompanyId.TabIndex = 101
        Me.txtCompanyId.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(19, 308)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(106, 15)
        Me.Label5.TabIndex = 99
        Me.Label5.Text = "*  Mandatory Filed"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(585, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(15, 20)
        Me.Label1.TabIndex = 95
        Me.Label1.Text = "*"
        '
        'txtFilePath
        '
        Me.txtFilePath.Location = New System.Drawing.Point(331, 192)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.Size = New System.Drawing.Size(49, 20)
        Me.txtFilePath.TabIndex = 87
        Me.txtFilePath.Visible = False
        '
        'txtFileName
        '
        Me.txtFileName.Location = New System.Drawing.Point(241, 218)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(45, 20)
        Me.txtFileName.TabIndex = 86
        Me.txtFileName.Visible = False
        '
        'txtId
        '
        Me.txtId.Location = New System.Drawing.Point(261, 218)
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
        Me.pnlToolBar.Location = New System.Drawing.Point(22, 238)
        Me.pnlToolBar.Name = "pnlToolBar"
        Me.pnlToolBar.Size = New System.Drawing.Size(699, 67)
        Me.pnlToolBar.TabIndex = 80
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
        Me.btnExit.TabIndex = 18
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
        Me.btnNew.TabIndex = 19
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
        Me.btnSearch1.TabIndex = 24
        Me.btnSearch1.Text = "SEARC&H"
        Me.btnSearch1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnSearch1.UseVisualStyleBackColor = False
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
        Me.btnView.TabIndex = 17
        Me.btnView.Text = " &VIEW"
        Me.btnView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnView.UseVisualStyleBackColor = False
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
        'txtEMailId
        '
        Me.txtEMailId.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEMailId.Location = New System.Drawing.Point(263, 186)
        Me.txtEMailId.MaxLength = 50
        Me.txtEMailId.Name = "txtEMailId"
        Me.txtEMailId.Size = New System.Drawing.Size(321, 22)
        Me.txtEMailId.TabIndex = 17
        '
        'lblEMailId
        '
        Me.lblEMailId.AutoSize = True
        Me.lblEMailId.BackColor = System.Drawing.Color.Transparent
        Me.lblEMailId.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEMailId.ForeColor = System.Drawing.Color.Black
        Me.lblEMailId.Location = New System.Drawing.Point(154, 192)
        Me.lblEMailId.Name = "lblEMailId"
        Me.lblEMailId.Size = New System.Drawing.Size(59, 16)
        Me.lblEMailId.TabIndex = 16
        Me.lblEMailId.Text = "EMail Id"
        '
        'txtMobileNo
        '
        Me.txtMobileNo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMobileNo.Location = New System.Drawing.Point(263, 154)
        Me.txtMobileNo.MaxLength = 15
        Me.txtMobileNo.Name = "txtMobileNo"
        Me.txtMobileNo.Size = New System.Drawing.Size(321, 22)
        Me.txtMobileNo.TabIndex = 15
        '
        'lblMobileNo
        '
        Me.lblMobileNo.AutoSize = True
        Me.lblMobileNo.BackColor = System.Drawing.Color.Transparent
        Me.lblMobileNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMobileNo.ForeColor = System.Drawing.Color.Black
        Me.lblMobileNo.Location = New System.Drawing.Point(154, 160)
        Me.lblMobileNo.Name = "lblMobileNo"
        Me.lblMobileNo.Size = New System.Drawing.Size(72, 16)
        Me.lblMobileNo.TabIndex = 14
        Me.lblMobileNo.Text = "Mobile No"
        '
        'txtPhoneNo
        '
        Me.txtPhoneNo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPhoneNo.Location = New System.Drawing.Point(263, 122)
        Me.txtPhoneNo.MaxLength = 15
        Me.txtPhoneNo.Name = "txtPhoneNo"
        Me.txtPhoneNo.Size = New System.Drawing.Size(321, 22)
        Me.txtPhoneNo.TabIndex = 13
        '
        'lblPhoneNo
        '
        Me.lblPhoneNo.AutoSize = True
        Me.lblPhoneNo.BackColor = System.Drawing.Color.Transparent
        Me.lblPhoneNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhoneNo.ForeColor = System.Drawing.Color.Black
        Me.lblPhoneNo.Location = New System.Drawing.Point(154, 128)
        Me.lblPhoneNo.Name = "lblPhoneNo"
        Me.lblPhoneNo.Size = New System.Drawing.Size(70, 16)
        Me.lblPhoneNo.TabIndex = 12
        Me.lblPhoneNo.Text = "Phone No"
        '
        'txtAddressLine2
        '
        Me.txtAddressLine2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddressLine2.Location = New System.Drawing.Point(263, 90)
        Me.txtAddressLine2.MaxLength = 50
        Me.txtAddressLine2.Name = "txtAddressLine2"
        Me.txtAddressLine2.Size = New System.Drawing.Size(321, 22)
        Me.txtAddressLine2.TabIndex = 11
        '
        'lblAddressLine2
        '
        Me.lblAddressLine2.AutoSize = True
        Me.lblAddressLine2.BackColor = System.Drawing.Color.Transparent
        Me.lblAddressLine2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddressLine2.ForeColor = System.Drawing.Color.Black
        Me.lblAddressLine2.Location = New System.Drawing.Point(154, 96)
        Me.lblAddressLine2.Name = "lblAddressLine2"
        Me.lblAddressLine2.Size = New System.Drawing.Size(97, 16)
        Me.lblAddressLine2.TabIndex = 10
        Me.lblAddressLine2.Text = "Address Line2"
        '
        'txtAddressLine1
        '
        Me.txtAddressLine1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddressLine1.Location = New System.Drawing.Point(263, 58)
        Me.txtAddressLine1.MaxLength = 50
        Me.txtAddressLine1.Name = "txtAddressLine1"
        Me.txtAddressLine1.Size = New System.Drawing.Size(321, 22)
        Me.txtAddressLine1.TabIndex = 9
        '
        'lblAddressLine1
        '
        Me.lblAddressLine1.AutoSize = True
        Me.lblAddressLine1.BackColor = System.Drawing.Color.Transparent
        Me.lblAddressLine1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddressLine1.ForeColor = System.Drawing.Color.Black
        Me.lblAddressLine1.Location = New System.Drawing.Point(154, 64)
        Me.lblAddressLine1.Name = "lblAddressLine1"
        Me.lblAddressLine1.Size = New System.Drawing.Size(97, 16)
        Me.lblAddressLine1.TabIndex = 8
        Me.lblAddressLine1.Text = "Address Line1"
        '
        'txtPayeeName
        '
        Me.txtPayeeName.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayeeName.Location = New System.Drawing.Point(263, 26)
        Me.txtPayeeName.MaxLength = 50
        Me.txtPayeeName.Name = "txtPayeeName"
        Me.txtPayeeName.Size = New System.Drawing.Size(321, 22)
        Me.txtPayeeName.TabIndex = 7
        '
        'lblCompanyName
        '
        Me.lblCompanyName.AutoSize = True
        Me.lblCompanyName.BackColor = System.Drawing.Color.Transparent
        Me.lblCompanyName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompanyName.ForeColor = System.Drawing.Color.Black
        Me.lblCompanyName.Location = New System.Drawing.Point(154, 32)
        Me.lblCompanyName.Name = "lblCompanyName"
        Me.lblCompanyName.Size = New System.Drawing.Size(45, 16)
        Me.lblCompanyName.TabIndex = 6
        Me.lblCompanyName.Text = "Name"
        '
        'txtSearchbox
        '
        Me.txtSearchbox.Location = New System.Drawing.Point(384, 298)
        Me.txtSearchbox.Name = "txtSearchbox"
        Me.txtSearchbox.Size = New System.Drawing.Size(123, 20)
        Me.txtSearchbox.TabIndex = 83
        Me.txtSearchbox.Visible = False
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(386, 272)
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
        Me.btnSearch.Location = New System.Drawing.Point(534, 26)
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
        Me.pnlFooter.Location = New System.Drawing.Point(0, 426)
        Me.pnlFooter.Name = "pnlFooter"
        Me.pnlFooter.Size = New System.Drawing.Size(781, 10)
        Me.pnlFooter.TabIndex = 1
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlContent)
        Me.pnlMain.Controls.Add(Me.pnlFooter)
        Me.pnlMain.Controls.Add(Me.pnlHeader)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(781, 436)
        Me.pnlMain.TabIndex = 9
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'frmPayeeInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(781, 436)
        Me.Controls.Add(Me.pnlMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPayeeInfo"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlContent.ResumeLayout(False)
        Me.pnlForm.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlToolBar.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeading As System.Windows.Forms.Label
    Friend WithEvents pnlContent As System.Windows.Forms.Panel
    Friend WithEvents pnlForm As System.Windows.Forms.Panel
    Friend WithEvents pnlFooter As System.Windows.Forms.Panel
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents txtId As System.Windows.Forms.TextBox
    Friend WithEvents pnlToolBar As System.Windows.Forms.Panel
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnSearch1 As System.Windows.Forms.Button
    Friend WithEvents btnView As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents txtEMailId As System.Windows.Forms.TextBox
    Friend WithEvents lblEMailId As System.Windows.Forms.Label
    Friend WithEvents txtMobileNo As System.Windows.Forms.TextBox
    Friend WithEvents lblMobileNo As System.Windows.Forms.Label
    Friend WithEvents txtPhoneNo As System.Windows.Forms.TextBox
    Friend WithEvents lblPhoneNo As System.Windows.Forms.Label
    Friend WithEvents txtAddressLine2 As System.Windows.Forms.TextBox
    Friend WithEvents lblAddressLine2 As System.Windows.Forms.Label
    Friend WithEvents txtAddressLine1 As System.Windows.Forms.TextBox
    Friend WithEvents lblAddressLine1 As System.Windows.Forms.Label
    Friend WithEvents txtPayeeName As System.Windows.Forms.TextBox
    Friend WithEvents lblCompanyName As System.Windows.Forms.Label
    Friend WithEvents txtSearchbox As System.Windows.Forms.TextBox
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtCompanyId As System.Windows.Forms.TextBox
End Class
