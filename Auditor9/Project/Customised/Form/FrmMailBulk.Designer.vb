<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMailBulk
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtMessage = New AgControls.AgTextBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.LblSubject = New System.Windows.Forms.Label()
        Me.TxtSubject = New AgControls.AgTextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.BtnAttachments = New System.Windows.Forms.Button()
        Me.BtnSend = New System.Windows.Forms.Button()
        Me.LblProgress = New System.Windows.Forms.Label()
        Me.MnuOptions = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuImportLeadsFromExcel = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TxtAcGroup = New AgControls.AgTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.TxtCity = New AgControls.AgTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.GroupBox10 = New System.Windows.Forms.GroupBox()
        Me.TxtTestMail = New AgControls.AgTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.MnuOptions.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(1, 148)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 16)
        Me.Label1.TabIndex = 915
        Me.Label1.Text = "Message"
        '
        'TxtMessage
        '
        Me.TxtMessage.AgAllowUserToEnableMasterHelp = False
        Me.TxtMessage.AgLastValueTag = Nothing
        Me.TxtMessage.AgLastValueText = Nothing
        Me.TxtMessage.AgMandatory = False
        Me.TxtMessage.AgMasterHelp = False
        Me.TxtMessage.AgNumberLeftPlaces = 0
        Me.TxtMessage.AgNumberNegetiveAllow = False
        Me.TxtMessage.AgNumberRightPlaces = 0
        Me.TxtMessage.AgPickFromLastValue = False
        Me.TxtMessage.AgRowFilter = ""
        Me.TxtMessage.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtMessage.AgSelectedValue = Nothing
        Me.TxtMessage.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtMessage.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtMessage.BackColor = System.Drawing.Color.White
        Me.TxtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtMessage.Font = New System.Drawing.Font("Verdana", 11.25!)
        Me.TxtMessage.Location = New System.Drawing.Point(6, 195)
        Me.TxtMessage.MaxLength = 0
        Me.TxtMessage.Multiline = True
        Me.TxtMessage.Name = "TxtMessage"
        Me.TxtMessage.Size = New System.Drawing.Size(415, 366)
        Me.TxtMessage.TabIndex = 4
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.GroupBox7)
        Me.GroupBox6.Location = New System.Drawing.Point(0, 170)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(421, 2)
        Me.GroupBox6.TabIndex = 917
        Me.GroupBox6.TabStop = False
        '
        'GroupBox7
        '
        Me.GroupBox7.Location = New System.Drawing.Point(0, 39)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(415, 10)
        Me.GroupBox7.TabIndex = 885
        Me.GroupBox7.TabStop = False
        '
        'LblSubject
        '
        Me.LblSubject.AutoSize = True
        Me.LblSubject.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LblSubject.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LblSubject.Location = New System.Drawing.Point(1, 113)
        Me.LblSubject.Name = "LblSubject"
        Me.LblSubject.Size = New System.Drawing.Size(63, 16)
        Me.LblSubject.TabIndex = 918
        Me.LblSubject.Text = "Subject"
        '
        'TxtSubject
        '
        Me.TxtSubject.AgAllowUserToEnableMasterHelp = False
        Me.TxtSubject.AgLastValueTag = Nothing
        Me.TxtSubject.AgLastValueText = Nothing
        Me.TxtSubject.AgMandatory = False
        Me.TxtSubject.AgMasterHelp = False
        Me.TxtSubject.AgNumberLeftPlaces = 0
        Me.TxtSubject.AgNumberNegetiveAllow = False
        Me.TxtSubject.AgNumberRightPlaces = 0
        Me.TxtSubject.AgPickFromLastValue = False
        Me.TxtSubject.AgRowFilter = ""
        Me.TxtSubject.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSubject.AgSelectedValue = Nothing
        Me.TxtSubject.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSubject.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSubject.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSubject.Font = New System.Drawing.Font("Verdana", 11.25!)
        Me.TxtSubject.Location = New System.Drawing.Point(81, 110)
        Me.TxtSubject.MaxLength = 0
        Me.TxtSubject.Name = "TxtSubject"
        Me.TxtSubject.Size = New System.Drawing.Size(346, 19)
        Me.TxtSubject.TabIndex = 3
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.GroupBox4)
        Me.GroupBox3.Location = New System.Drawing.Point(0, 134)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(421, 2)
        Me.GroupBox3.TabIndex = 920
        Me.GroupBox3.TabStop = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Location = New System.Drawing.Point(0, 39)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(415, 10)
        Me.GroupBox4.TabIndex = 885
        Me.GroupBox4.TabStop = False
        '
        'BtnAttachments
        '
        Me.BtnAttachments.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnAttachments.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAttachments.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnAttachments.ForeColor = System.Drawing.Color.White
        Me.BtnAttachments.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BtnAttachments.Location = New System.Drawing.Point(4, 579)
        Me.BtnAttachments.Name = "BtnAttachments"
        Me.BtnAttachments.Size = New System.Drawing.Size(111, 28)
        Me.BtnAttachments.TabIndex = 924
        Me.BtnAttachments.Text = "Attachments"
        Me.BtnAttachments.UseVisualStyleBackColor = False
        '
        'BtnSend
        '
        Me.BtnSend.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSend.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnSend.ForeColor = System.Drawing.Color.White
        Me.BtnSend.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BtnSend.Location = New System.Drawing.Point(345, 579)
        Me.BtnSend.Name = "BtnSend"
        Me.BtnSend.Size = New System.Drawing.Size(80, 28)
        Me.BtnSend.TabIndex = 923
        Me.BtnSend.Text = "Send"
        Me.BtnSend.UseVisualStyleBackColor = False
        '
        'LblProgress
        '
        Me.LblProgress.AutoSize = True
        Me.LblProgress.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblProgress.ForeColor = System.Drawing.Color.Blue
        Me.LblProgress.Location = New System.Drawing.Point(130, 583)
        Me.LblProgress.Name = "LblProgress"
        Me.LblProgress.Size = New System.Drawing.Size(0, 18)
        Me.LblProgress.TabIndex = 925
        '
        'MnuOptions
        '
        Me.MnuOptions.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuImportLeadsFromExcel})
        Me.MnuOptions.Name = "MnuOptions"
        Me.MnuOptions.Size = New System.Drawing.Size(204, 26)
        '
        'MnuImportLeadsFromExcel
        '
        Me.MnuImportLeadsFromExcel.Name = "MnuImportLeadsFromExcel"
        Me.MnuImportLeadsFromExcel.Size = New System.Drawing.Size(203, 22)
        Me.MnuImportLeadsFromExcel.Text = "Import Leads From Excel"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 33)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(421, 2)
        Me.GroupBox1.TabIndex = 928
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(0, 39)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(415, 10)
        Me.GroupBox2.TabIndex = 885
        Me.GroupBox2.TabStop = False
        '
        'TxtAcGroup
        '
        Me.TxtAcGroup.AgAllowUserToEnableMasterHelp = False
        Me.TxtAcGroup.AgLastValueTag = Nothing
        Me.TxtAcGroup.AgLastValueText = Nothing
        Me.TxtAcGroup.AgMandatory = False
        Me.TxtAcGroup.AgMasterHelp = False
        Me.TxtAcGroup.AgNumberLeftPlaces = 0
        Me.TxtAcGroup.AgNumberNegetiveAllow = False
        Me.TxtAcGroup.AgNumberRightPlaces = 0
        Me.TxtAcGroup.AgPickFromLastValue = False
        Me.TxtAcGroup.AgRowFilter = ""
        Me.TxtAcGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAcGroup.AgSelectedValue = Nothing
        Me.TxtAcGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAcGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAcGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAcGroup.Font = New System.Drawing.Font("Verdana", 11.25!)
        Me.TxtAcGroup.Location = New System.Drawing.Point(85, 11)
        Me.TxtAcGroup.MaxLength = 0
        Me.TxtAcGroup.Name = "TxtAcGroup"
        Me.TxtAcGroup.Size = New System.Drawing.Size(346, 19)
        Me.TxtAcGroup.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(5, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 16)
        Me.Label2.TabIndex = 926
        Me.Label2.Text = "A/c Group"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.GroupBox8)
        Me.GroupBox5.Location = New System.Drawing.Point(1, 67)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(421, 2)
        Me.GroupBox5.TabIndex = 931
        Me.GroupBox5.TabStop = False
        '
        'GroupBox8
        '
        Me.GroupBox8.Location = New System.Drawing.Point(0, 39)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(415, 10)
        Me.GroupBox8.TabIndex = 885
        Me.GroupBox8.TabStop = False
        '
        'TxtCity
        '
        Me.TxtCity.AgAllowUserToEnableMasterHelp = False
        Me.TxtCity.AgLastValueTag = Nothing
        Me.TxtCity.AgLastValueText = Nothing
        Me.TxtCity.AgMandatory = False
        Me.TxtCity.AgMasterHelp = False
        Me.TxtCity.AgNumberLeftPlaces = 0
        Me.TxtCity.AgNumberNegetiveAllow = False
        Me.TxtCity.AgNumberRightPlaces = 0
        Me.TxtCity.AgPickFromLastValue = False
        Me.TxtCity.AgRowFilter = ""
        Me.TxtCity.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCity.AgSelectedValue = Nothing
        Me.TxtCity.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCity.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCity.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCity.Font = New System.Drawing.Font("Verdana", 11.25!)
        Me.TxtCity.Location = New System.Drawing.Point(82, 45)
        Me.TxtCity.MaxLength = 0
        Me.TxtCity.Name = "TxtCity"
        Me.TxtCity.Size = New System.Drawing.Size(346, 19)
        Me.TxtCity.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(2, 46)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 16)
        Me.Label3.TabIndex = 929
        Me.Label3.Text = "City"
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.GroupBox10)
        Me.GroupBox9.Location = New System.Drawing.Point(1, 99)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(421, 2)
        Me.GroupBox9.TabIndex = 934
        Me.GroupBox9.TabStop = False
        '
        'GroupBox10
        '
        Me.GroupBox10.Location = New System.Drawing.Point(0, 39)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(415, 10)
        Me.GroupBox10.TabIndex = 885
        Me.GroupBox10.TabStop = False
        '
        'TxtTestMail
        '
        Me.TxtTestMail.AgAllowUserToEnableMasterHelp = False
        Me.TxtTestMail.AgLastValueTag = Nothing
        Me.TxtTestMail.AgLastValueText = Nothing
        Me.TxtTestMail.AgMandatory = False
        Me.TxtTestMail.AgMasterHelp = False
        Me.TxtTestMail.AgNumberLeftPlaces = 0
        Me.TxtTestMail.AgNumberNegetiveAllow = False
        Me.TxtTestMail.AgNumberRightPlaces = 0
        Me.TxtTestMail.AgPickFromLastValue = False
        Me.TxtTestMail.AgRowFilter = ""
        Me.TxtTestMail.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTestMail.AgSelectedValue = Nothing
        Me.TxtTestMail.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTestMail.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtTestMail.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTestMail.Font = New System.Drawing.Font("Verdana", 11.25!)
        Me.TxtTestMail.Location = New System.Drawing.Point(82, 77)
        Me.TxtTestMail.MaxLength = 0
        Me.TxtTestMail.Name = "TxtTestMail"
        Me.TxtTestMail.Size = New System.Drawing.Size(346, 19)
        Me.TxtTestMail.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(2, 78)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 16)
        Me.Label4.TabIndex = 933
        Me.Label4.Text = "Test Mail"
        '
        'FrmMailBulk
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(429, 615)
        Me.ContextMenuStrip = Me.MnuOptions
        Me.Controls.Add(Me.GroupBox9)
        Me.Controls.Add(Me.TxtTestMail)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.TxtCity)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TxtAcGroup)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LblProgress)
        Me.Controls.Add(Me.BtnAttachments)
        Me.Controls.Add(Me.BtnSend)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.TxtSubject)
        Me.Controls.Add(Me.LblSubject)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtMessage)
        Me.MinimizeBox = False
        Me.Name = "FrmMailBulk"
        Me.Text = "Bulk EMail"
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.MnuOptions.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox9.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents Label1 As Label
    Public WithEvents TxtMessage As AgControls.AgTextBox
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents GroupBox7 As GroupBox
    Public WithEvents LblSubject As Label
    Public WithEvents TxtSubject As AgControls.AgTextBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents BtnAttachments As Button
    Friend WithEvents BtnSend As Button
    Friend WithEvents LblProgress As Label
    Friend WithEvents MnuOptions As ContextMenuStrip
    Friend WithEvents MnuImportLeadsFromExcel As ToolStripMenuItem
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Public WithEvents TxtAcGroup As AgControls.AgTextBox
    Public WithEvents Label2 As Label
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents GroupBox8 As GroupBox
    Public WithEvents TxtCity As AgControls.AgTextBox
    Public WithEvents Label3 As Label
    Friend WithEvents GroupBox9 As GroupBox
    Friend WithEvents GroupBox10 As GroupBox
    Public WithEvents TxtTestMail As AgControls.AgTextBox
    Public WithEvents Label4 As Label
End Class
