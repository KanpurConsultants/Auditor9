<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintEnvelopeNew
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintEnvelopeNew))
        Me.Label1 = New System.Windows.Forms.Label
        Me.pnlHeader = New System.Windows.Forms.Panel
        Me.lblHeading = New System.Windows.Forms.Label
        Me.pnlContent = New System.Windows.Forms.Panel
        Me.pnlForm = New System.Windows.Forms.Panel
        Me.Panel1 = New System.Windows.Forms.Panel
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
        Me.txtEnvelopeHeight = New System.Windows.Forms.TextBox
        Me.lblZip = New System.Windows.Forms.Label
        Me.txtEnvelopeWidth = New System.Windows.Forms.TextBox
        Me.lblAddressLine2 = New System.Windows.Forms.Label
        Me.txtFromTop = New System.Windows.Forms.TextBox
        Me.lblFromTop = New System.Windows.Forms.Label
        Me.txtFromLeft = New System.Windows.Forms.TextBox
        Me.lblFromLeft = New System.Windows.Forms.Label
        Me.txtEnvName = New System.Windows.Forms.TextBox
        Me.lblEnvName = New System.Windows.Forms.Label
        Me.txtSearchbox = New System.Windows.Forms.TextBox
        Me.txtCode = New System.Windows.Forms.TextBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.pnlMain = New System.Windows.Forms.Panel
        Me.pnlFooter = New System.Windows.Forms.Panel
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.txtCompanyId = New System.Windows.Forms.TextBox
        Me.pnlHeader.SuspendLayout()
        Me.pnlContent.SuspendLayout()
        Me.pnlForm.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlToolBar.SuspendLayout()
        Me.pnlMain.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(25, -18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(113, 16)
        Me.Label1.TabIndex = 108
        Me.Label1.Text = " C&ustomer Name"
        '
        'pnlHeader
        '
        Me.pnlHeader.AutoScroll = True
        Me.pnlHeader.Controls.Add(Me.lblHeading)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(831, 52)
        Me.pnlHeader.TabIndex = 0
        '
        'lblHeading
        '
        Me.lblHeading.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblHeading.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeading.Location = New System.Drawing.Point(0, 0)
        Me.lblHeading.Name = "lblHeading"
        Me.lblHeading.Size = New System.Drawing.Size(831, 42)
        Me.lblHeading.TabIndex = 0
        Me.lblHeading.Text = "ENVELOPE SIZE"
        Me.lblHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlContent
        '
        Me.pnlContent.AutoScroll = True
        Me.pnlContent.Controls.Add(Me.pnlForm)
        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContent.Location = New System.Drawing.Point(0, 52)
        Me.pnlContent.Name = "pnlContent"
        Me.pnlContent.Size = New System.Drawing.Size(831, 306)
        Me.pnlContent.TabIndex = 0
        '
        'pnlForm
        '
        Me.pnlForm.Controls.Add(Me.Panel1)
        Me.pnlForm.Controls.Add(Me.Label1)
        Me.pnlForm.Location = New System.Drawing.Point(23, 16)
        Me.pnlForm.Name = "pnlForm"
        Me.pnlForm.Size = New System.Drawing.Size(775, 268)
        Me.pnlForm.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.txtCompanyId)
        Me.Panel1.Controls.Add(Me.txtFilePath)
        Me.Panel1.Controls.Add(Me.txtFileName)
        Me.Panel1.Controls.Add(Me.txtId)
        Me.Panel1.Controls.Add(Me.pnlToolBar)
        Me.Panel1.Controls.Add(Me.txtEnvelopeHeight)
        Me.Panel1.Controls.Add(Me.lblZip)
        Me.Panel1.Controls.Add(Me.txtEnvelopeWidth)
        Me.Panel1.Controls.Add(Me.lblAddressLine2)
        Me.Panel1.Controls.Add(Me.txtFromTop)
        Me.Panel1.Controls.Add(Me.lblFromTop)
        Me.Panel1.Controls.Add(Me.txtFromLeft)
        Me.Panel1.Controls.Add(Me.lblFromLeft)
        Me.Panel1.Controls.Add(Me.txtEnvName)
        Me.Panel1.Controls.Add(Me.lblEnvName)
        Me.Panel1.Controls.Add(Me.txtSearchbox)
        Me.Panel1.Controls.Add(Me.txtCode)
        Me.Panel1.Controls.Add(Me.btnSearch)
        Me.Panel1.Location = New System.Drawing.Point(17, 17)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(736, 232)
        Me.Panel1.TabIndex = 109
        '
        'txtFilePath
        '
        Me.txtFilePath.Location = New System.Drawing.Point(346, 250)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.Size = New System.Drawing.Size(123, 20)
        Me.txtFilePath.TabIndex = 87
        Me.txtFilePath.Visible = False
        '
        'txtFileName
        '
        Me.txtFileName.Location = New System.Drawing.Point(470, 258)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(123, 20)
        Me.txtFileName.TabIndex = 86
        Me.txtFileName.Visible = False
        '
        'txtId
        '
        Me.txtId.Location = New System.Drawing.Point(490, 258)
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(123, 20)
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
        Me.pnlToolBar.Location = New System.Drawing.Point(28, 136)
        Me.pnlToolBar.Name = "pnlToolBar"
        Me.pnlToolBar.Size = New System.Drawing.Size(693, 64)
        Me.pnlToolBar.TabIndex = 80
        '
        'btnExit
        '
        Me.btnExit.BackColor = System.Drawing.Color.White
        Me.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnExit.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExit.Image = Global.DenariuChequeWriterSoft.My.Resources.Resources.exit_32
        Me.btnExit.Location = New System.Drawing.Point(463, 8)
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
        Me.btnNew.Location = New System.Drawing.Point(3, 8)
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
        Me.btnSave.Location = New System.Drawing.Point(118, 8)
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
        Me.btnSearch1.Location = New System.Drawing.Point(578, 8)
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
        Me.btnView.Location = New System.Drawing.Point(348, 8)
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
        Me.btnDelete.Location = New System.Drawing.Point(233, 8)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(107, 49)
        Me.btnDelete.TabIndex = 14
        Me.btnDelete.Text = "&DELETE"
        Me.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnDelete.UseVisualStyleBackColor = False
        '
        'txtEnvelopeHeight
        '
        Me.txtEnvelopeHeight.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEnvelopeHeight.Location = New System.Drawing.Point(298, 166)
        Me.txtEnvelopeHeight.MaxLength = 50
        Me.txtEnvelopeHeight.Name = "txtEnvelopeHeight"
        Me.txtEnvelopeHeight.Size = New System.Drawing.Size(321, 22)
        Me.txtEnvelopeHeight.TabIndex = 15
        Me.txtEnvelopeHeight.Visible = False
        '
        'lblZip
        '
        Me.lblZip.AutoSize = True
        Me.lblZip.BackColor = System.Drawing.Color.Transparent
        Me.lblZip.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblZip.ForeColor = System.Drawing.Color.Black
        Me.lblZip.Location = New System.Drawing.Point(118, 168)
        Me.lblZip.Name = "lblZip"
        Me.lblZip.Size = New System.Drawing.Size(164, 16)
        Me.lblZip.TabIndex = 14
        Me.lblZip.Text = "Envelope Height (In mm)"
        Me.lblZip.Visible = False
        '
        'txtEnvelopeWidth
        '
        Me.txtEnvelopeWidth.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEnvelopeWidth.Location = New System.Drawing.Point(298, 134)
        Me.txtEnvelopeWidth.MaxLength = 50
        Me.txtEnvelopeWidth.Name = "txtEnvelopeWidth"
        Me.txtEnvelopeWidth.Size = New System.Drawing.Size(321, 22)
        Me.txtEnvelopeWidth.TabIndex = 13
        Me.txtEnvelopeWidth.Visible = False
        '
        'lblAddressLine2
        '
        Me.lblAddressLine2.AutoSize = True
        Me.lblAddressLine2.BackColor = System.Drawing.Color.Transparent
        Me.lblAddressLine2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddressLine2.ForeColor = System.Drawing.Color.Black
        Me.lblAddressLine2.Location = New System.Drawing.Point(118, 136)
        Me.lblAddressLine2.Name = "lblAddressLine2"
        Me.lblAddressLine2.Size = New System.Drawing.Size(160, 16)
        Me.lblAddressLine2.TabIndex = 12
        Me.lblAddressLine2.Text = "Envelope Width (In mm)"
        Me.lblAddressLine2.Visible = False
        '
        'txtFromTop
        '
        Me.txtFromTop.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromTop.Location = New System.Drawing.Point(298, 102)
        Me.txtFromTop.MaxLength = 50
        Me.txtFromTop.Name = "txtFromTop"
        Me.txtFromTop.Size = New System.Drawing.Size(321, 22)
        Me.txtFromTop.TabIndex = 11
        '
        'lblFromTop
        '
        Me.lblFromTop.AutoSize = True
        Me.lblFromTop.BackColor = System.Drawing.Color.Transparent
        Me.lblFromTop.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromTop.ForeColor = System.Drawing.Color.Black
        Me.lblFromTop.Location = New System.Drawing.Point(118, 104)
        Me.lblFromTop.Name = "lblFromTop"
        Me.lblFromTop.Size = New System.Drawing.Size(120, 16)
        Me.lblFromTop.TabIndex = 10
        Me.lblFromTop.Text = "From Top (In mm)"
        '
        'txtFromLeft
        '
        Me.txtFromLeft.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromLeft.Location = New System.Drawing.Point(298, 70)
        Me.txtFromLeft.MaxLength = 50
        Me.txtFromLeft.Name = "txtFromLeft"
        Me.txtFromLeft.Size = New System.Drawing.Size(321, 22)
        Me.txtFromLeft.TabIndex = 9
        '
        'lblFromLeft
        '
        Me.lblFromLeft.AutoSize = True
        Me.lblFromLeft.BackColor = System.Drawing.Color.Transparent
        Me.lblFromLeft.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromLeft.ForeColor = System.Drawing.Color.Black
        Me.lblFromLeft.Location = New System.Drawing.Point(118, 72)
        Me.lblFromLeft.Name = "lblFromLeft"
        Me.lblFromLeft.Size = New System.Drawing.Size(121, 16)
        Me.lblFromLeft.TabIndex = 8
        Me.lblFromLeft.Text = "From Left (In mm)"
        '
        'txtEnvName
        '
        Me.txtEnvName.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEnvName.Location = New System.Drawing.Point(298, 38)
        Me.txtEnvName.MaxLength = 50
        Me.txtEnvName.Name = "txtEnvName"
        Me.txtEnvName.Size = New System.Drawing.Size(321, 22)
        Me.txtEnvName.TabIndex = 7
        '
        'lblEnvName
        '
        Me.lblEnvName.AutoSize = True
        Me.lblEnvName.BackColor = System.Drawing.Color.Transparent
        Me.lblEnvName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEnvName.ForeColor = System.Drawing.Color.Black
        Me.lblEnvName.Location = New System.Drawing.Point(118, 40)
        Me.lblEnvName.Name = "lblEnvName"
        Me.lblEnvName.Size = New System.Drawing.Size(112, 16)
        Me.lblEnvName.TabIndex = 6
        Me.lblEnvName.Text = "Envelope  Name"
        '
        'txtSearchbox
        '
        Me.txtSearchbox.Location = New System.Drawing.Point(348, 306)
        Me.txtSearchbox.Name = "txtSearchbox"
        Me.txtSearchbox.Size = New System.Drawing.Size(123, 20)
        Me.txtSearchbox.TabIndex = 83
        Me.txtSearchbox.Visible = False
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(350, 280)
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
        Me.btnSearch.Location = New System.Drawing.Point(389, 297)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(66, 49)
        Me.btnSearch.TabIndex = 84
        Me.btnSearch.Text = "SEARC&H"
        Me.btnSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnSearch.UseVisualStyleBackColor = False
        Me.btnSearch.Visible = False
        '
        'pnlMain
        '
        Me.pnlMain.Controls.Add(Me.pnlContent)
        Me.pnlMain.Controls.Add(Me.pnlFooter)
        Me.pnlMain.Controls.Add(Me.pnlHeader)
        Me.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMain.Location = New System.Drawing.Point(0, 0)
        Me.pnlMain.Name = "pnlMain"
        Me.pnlMain.Size = New System.Drawing.Size(831, 368)
        Me.pnlMain.TabIndex = 8
        '
        'pnlFooter
        '
        Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFooter.Location = New System.Drawing.Point(0, 358)
        Me.pnlFooter.Name = "pnlFooter"
        Me.pnlFooter.Size = New System.Drawing.Size(831, 10)
        Me.pnlFooter.TabIndex = 1
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'txtCompanyId
        '
        Me.txtCompanyId.Location = New System.Drawing.Point(664, 68)
        Me.txtCompanyId.Name = "txtCompanyId"
        Me.txtCompanyId.Size = New System.Drawing.Size(49, 20)
        Me.txtCompanyId.TabIndex = 102
        Me.txtCompanyId.Visible = False
        '
        'frmPrintEnvelopeNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(831, 368)
        Me.Controls.Add(Me.pnlMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmPrintEnvelopeNew"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlContent.ResumeLayout(False)
        Me.pnlForm.ResumeLayout(False)
        Me.pnlForm.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlToolBar.ResumeLayout(False)
        Me.pnlMain.ResumeLayout(False)
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeading As System.Windows.Forms.Label
    Friend WithEvents pnlContent As System.Windows.Forms.Panel
    Friend WithEvents pnlForm As System.Windows.Forms.Panel
    Friend WithEvents pnlMain As System.Windows.Forms.Panel
    Friend WithEvents pnlFooter As System.Windows.Forms.Panel
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
    Friend WithEvents txtEnvelopeHeight As System.Windows.Forms.TextBox
    Friend WithEvents lblZip As System.Windows.Forms.Label
    Friend WithEvents txtEnvelopeWidth As System.Windows.Forms.TextBox
    Friend WithEvents lblAddressLine2 As System.Windows.Forms.Label
    Friend WithEvents txtFromTop As System.Windows.Forms.TextBox
    Friend WithEvents lblFromTop As System.Windows.Forms.Label
    Friend WithEvents txtFromLeft As System.Windows.Forms.TextBox
    Friend WithEvents lblFromLeft As System.Windows.Forms.Label
    Friend WithEvents txtEnvName As System.Windows.Forms.TextBox
    Friend WithEvents lblEnvName As System.Windows.Forms.Label
    Friend WithEvents txtSearchbox As System.Windows.Forms.TextBox
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtCompanyId As System.Windows.Forms.TextBox
End Class
