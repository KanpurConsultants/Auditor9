<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSmsCompose
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSmsCompose))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.BtnSend = New System.Windows.Forms.Button()
        Me.TxtMessage = New AgControls.AgTextBox()
        Me.LblToEmail = New System.Windows.Forms.Label()
        Me.TxtToMobile = New AgControls.AgTextBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.BtnTo = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
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
        'LblToEmail
        '
        resources.ApplyResources(Me.LblToEmail, "LblToEmail")
        Me.LblToEmail.Name = "LblToEmail"
        '
        'TxtToMobile
        '
        Me.TxtToMobile.AgAllowUserToEnableMasterHelp = False
        Me.TxtToMobile.AgLastValueTag = Nothing
        Me.TxtToMobile.AgLastValueText = Nothing
        Me.TxtToMobile.AgMandatory = False
        Me.TxtToMobile.AgMasterHelp = False
        Me.TxtToMobile.AgNumberLeftPlaces = 0
        Me.TxtToMobile.AgNumberNegetiveAllow = False
        Me.TxtToMobile.AgNumberRightPlaces = 0
        Me.TxtToMobile.AgPickFromLastValue = False
        Me.TxtToMobile.AgRowFilter = ""
        Me.TxtToMobile.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtToMobile.AgSelectedValue = Nothing
        Me.TxtToMobile.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtToMobile.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtToMobile.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.TxtToMobile, "TxtToMobile")
        Me.TxtToMobile.Name = "TxtToMobile"
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
        'BtnTo
        '
        Me.BtnTo.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.BtnTo, "BtnTo")
        Me.BtnTo.ForeColor = System.Drawing.Color.White
        Me.BtnTo.Name = "BtnTo"
        Me.BtnTo.UseVisualStyleBackColor = False
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
        'FrmSmsCompose
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.BtnTo)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.BtnSend)
        Me.Controls.Add(Me.TxtMessage)
        Me.Controls.Add(Me.LblToEmail)
        Me.Controls.Add(Me.TxtToMobile)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmSmsCompose"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents Label1 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents BtnSend As Button
    Public WithEvents TxtMessage As AgControls.AgTextBox
    Public WithEvents LblToEmail As Label
    Public WithEvents TxtToMobile As AgControls.AgTextBox
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents BtnTo As Button
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents GroupBox4 As GroupBox
End Class
