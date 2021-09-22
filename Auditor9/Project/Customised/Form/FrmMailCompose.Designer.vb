<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMailCompose
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMailCompose))
        Me.reportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.BtnSend = New System.Windows.Forms.Button()
        Me.TxtMessage = New AgControls.AgTextBox()
        Me.LblSubject = New System.Windows.Forms.Label()
        Me.TxtSubject = New AgControls.AgTextBox()
        Me.LblToEmail = New System.Windows.Forms.Label()
        Me.TxtToEmail = New AgControls.AgTextBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.GroupBox10 = New System.Windows.Forms.GroupBox()
        Me.GroupBox11 = New System.Windows.Forms.GroupBox()
        Me.LblCc = New System.Windows.Forms.Label()
        Me.TxtCcEMail = New AgControls.AgTextBox()
        Me.BtnTo = New System.Windows.Forms.Button()
        Me.BtnCc = New System.Windows.Forms.Button()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        Me.SuspendLayout()
        '
        'reportViewer1
        '
        resources.ApplyResources(Me.reportViewer1, "reportViewer1")
        Me.reportViewer1.DocumentMapWidth = 98
        Me.reportViewer1.Name = "reportViewer1"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.GroupBox4)
        resources.ApplyResources(Me.GroupBox3, "GroupBox3")
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.TabStop = False
        '
        'GroupBox4
        '
        resources.ApplyResources(Me.GroupBox4, "GroupBox4")
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.TabStop = False
        '
        'BtnSend
        '
        Me.BtnSend.BackColor = System.Drawing.Color.SteelBlue
        resources.ApplyResources(Me.BtnSend, "BtnSend")
        Me.BtnSend.ForeColor = System.Drawing.Color.White
        Me.BtnSend.Name = "BtnSend"
        Me.BtnSend.UseVisualStyleBackColor = False
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
        resources.ApplyResources(Me.TxtMessage, "TxtMessage")
        Me.TxtMessage.Name = "TxtMessage"
        '
        'LblSubject
        '
        resources.ApplyResources(Me.LblSubject, "LblSubject")
        Me.LblSubject.Name = "LblSubject"
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
        resources.ApplyResources(Me.TxtSubject, "TxtSubject")
        Me.TxtSubject.Name = "TxtSubject"
        '
        'LblToEmail
        '
        resources.ApplyResources(Me.LblToEmail, "LblToEmail")
        Me.LblToEmail.Name = "LblToEmail"
        '
        'TxtToEmail
        '
        Me.TxtToEmail.AgAllowUserToEnableMasterHelp = False
        Me.TxtToEmail.AgLastValueTag = Nothing
        Me.TxtToEmail.AgLastValueText = Nothing
        Me.TxtToEmail.AgMandatory = False
        Me.TxtToEmail.AgMasterHelp = False
        Me.TxtToEmail.AgNumberLeftPlaces = 0
        Me.TxtToEmail.AgNumberNegetiveAllow = False
        Me.TxtToEmail.AgNumberRightPlaces = 0
        Me.TxtToEmail.AgPickFromLastValue = False
        Me.TxtToEmail.AgRowFilter = ""
        Me.TxtToEmail.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtToEmail.AgSelectedValue = Nothing
        Me.TxtToEmail.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtToEmail.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtToEmail.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.TxtToEmail, "TxtToEmail")
        Me.TxtToEmail.Name = "TxtToEmail"
        '
        'GroupBox5
        '
        resources.ApplyResources(Me.GroupBox5, "GroupBox5")
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.TabStop = False
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.GroupBox7)
        resources.ApplyResources(Me.GroupBox6, "GroupBox6")
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.TabStop = False
        '
        'GroupBox7
        '
        resources.ApplyResources(Me.GroupBox7, "GroupBox7")
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.TabStop = False
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.GroupBox9)
        resources.ApplyResources(Me.GroupBox8, "GroupBox8")
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.TabStop = False
        '
        'GroupBox9
        '
        resources.ApplyResources(Me.GroupBox9, "GroupBox9")
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.TabStop = False
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.GroupBox11)
        resources.ApplyResources(Me.GroupBox10, "GroupBox10")
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.TabStop = False
        '
        'GroupBox11
        '
        resources.ApplyResources(Me.GroupBox11, "GroupBox11")
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.TabStop = False
        '
        'LblCc
        '
        resources.ApplyResources(Me.LblCc, "LblCc")
        Me.LblCc.Name = "LblCc"
        '
        'TxtCcEMail
        '
        Me.TxtCcEMail.AgAllowUserToEnableMasterHelp = False
        Me.TxtCcEMail.AgLastValueTag = Nothing
        Me.TxtCcEMail.AgLastValueText = Nothing
        Me.TxtCcEMail.AgMandatory = False
        Me.TxtCcEMail.AgMasterHelp = False
        Me.TxtCcEMail.AgNumberLeftPlaces = 0
        Me.TxtCcEMail.AgNumberNegetiveAllow = False
        Me.TxtCcEMail.AgNumberRightPlaces = 0
        Me.TxtCcEMail.AgPickFromLastValue = False
        Me.TxtCcEMail.AgRowFilter = ""
        Me.TxtCcEMail.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCcEMail.AgSelectedValue = Nothing
        Me.TxtCcEMail.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCcEMail.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCcEMail.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.TxtCcEMail, "TxtCcEMail")
        Me.TxtCcEMail.Name = "TxtCcEMail"
        '
        'BtnTo
        '
        Me.BtnTo.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.BtnTo, "BtnTo")
        Me.BtnTo.ForeColor = System.Drawing.Color.White
        Me.BtnTo.Name = "BtnTo"
        Me.BtnTo.UseVisualStyleBackColor = False
        '
        'BtnCc
        '
        Me.BtnCc.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.BtnCc, "BtnCc")
        Me.BtnCc.ForeColor = System.Drawing.Color.White
        Me.BtnCc.Name = "BtnCc"
        Me.BtnCc.UseVisualStyleBackColor = False
        '
        'FrmMailCompose
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.BtnCc)
        Me.Controls.Add(Me.BtnTo)
        Me.Controls.Add(Me.GroupBox10)
        Me.Controls.Add(Me.LblCc)
        Me.Controls.Add(Me.TxtCcEMail)
        Me.Controls.Add(Me.GroupBox8)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.BtnSend)
        Me.Controls.Add(Me.TxtMessage)
        Me.Controls.Add(Me.LblSubject)
        Me.Controls.Add(Me.TxtSubject)
        Me.Controls.Add(Me.LblToEmail)
        Me.Controls.Add(Me.TxtToEmail)
        Me.Controls.Add(Me.reportViewer1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmMailCompose"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox10.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents Label1 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents BtnSend As Button
    Public WithEvents TxtMessage As AgControls.AgTextBox
    Public WithEvents LblSubject As Label
    Public WithEvents TxtSubject As AgControls.AgTextBox
    Public WithEvents LblToEmail As Label
    Public WithEvents TxtToEmail As AgControls.AgTextBox
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents GroupBox8 As GroupBox
    Friend WithEvents GroupBox9 As GroupBox
    Friend WithEvents GroupBox10 As GroupBox
    Friend WithEvents GroupBox11 As GroupBox
    Public WithEvents LblCc As Label
    Public WithEvents TxtCcEMail As AgControls.AgTextBox
    Friend WithEvents BtnTo As Button
    Friend WithEvents BtnCc As Button
    Public WithEvents reportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
End Class
