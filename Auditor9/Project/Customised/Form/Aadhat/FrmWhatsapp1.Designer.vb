<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmWhatsapp1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmWhatsapp1))
        Me.BtnSendWhatsapp = New System.Windows.Forms.Button()
        Me.TxtMessage = New AgControls.AgTextBox()
        Me.BtnTo = New System.Windows.Forms.Button()
        Me.LblToEmail = New System.Windows.Forms.Label()
        Me.TxtToMobile = New AgControls.AgTextBox()
        Me.BtnAttachments = New System.Windows.Forms.Button()
        Me.TxtFilePath = New AgControls.AgTextBox()
        Me.SuspendLayout()
        '
        'BtnSendWhatsapp
        '
        Me.BtnSendWhatsapp.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnSendWhatsapp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSendWhatsapp.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnSendWhatsapp.ForeColor = System.Drawing.Color.White
        Me.BtnSendWhatsapp.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BtnSendWhatsapp.Location = New System.Drawing.Point(237, 338)
        Me.BtnSendWhatsapp.Name = "BtnSendWhatsapp"
        Me.BtnSendWhatsapp.Size = New System.Drawing.Size(201, 28)
        Me.BtnSendWhatsapp.TabIndex = 922
        Me.BtnSendWhatsapp.Text = "Send Whatsapp"
        Me.BtnSendWhatsapp.UseVisualStyleBackColor = False
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
        Me.TxtMessage.Location = New System.Drawing.Point(30, 58)
        Me.TxtMessage.MaxLength = 0
        Me.TxtMessage.Multiline = True
        Me.TxtMessage.Name = "TxtMessage"
        Me.TxtMessage.Size = New System.Drawing.Size(389, 230)
        Me.TxtMessage.TabIndex = 923
        '
        'BtnTo
        '
        Me.BtnTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnTo.BackColor = System.Drawing.Color.Transparent
        Me.BtnTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnTo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnTo.ForeColor = System.Drawing.Color.White
        Me.BtnTo.Image = CType(resources.GetObject("BtnTo.Image"), System.Drawing.Image)
        Me.BtnTo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BtnTo.Location = New System.Drawing.Point(388, 12)
        Me.BtnTo.Name = "BtnTo"
        Me.BtnTo.Size = New System.Drawing.Size(31, 28)
        Me.BtnTo.TabIndex = 928
        Me.BtnTo.UseVisualStyleBackColor = False
        '
        'LblToEmail
        '
        Me.LblToEmail.AutoSize = True
        Me.LblToEmail.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LblToEmail.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LblToEmail.Location = New System.Drawing.Point(27, 19)
        Me.LblToEmail.Name = "LblToEmail"
        Me.LblToEmail.Size = New System.Drawing.Size(78, 16)
        Me.LblToEmail.TabIndex = 927
        Me.LblToEmail.Text = "Mobile No"
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
        Me.TxtToMobile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtToMobile.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtToMobile.Font = New System.Drawing.Font("Verdana", 11.25!)
        Me.TxtToMobile.Location = New System.Drawing.Point(128, 18)
        Me.TxtToMobile.MaxLength = 0
        Me.TxtToMobile.Name = "TxtToMobile"
        Me.TxtToMobile.Size = New System.Drawing.Size(254, 19)
        Me.TxtToMobile.TabIndex = 926
        '
        'BtnAttachments
        '
        Me.BtnAttachments.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnAttachments.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAttachments.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnAttachments.ForeColor = System.Drawing.Color.White
        Me.BtnAttachments.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BtnAttachments.Location = New System.Drawing.Point(308, 299)
        Me.BtnAttachments.Name = "BtnAttachments"
        Me.BtnAttachments.Size = New System.Drawing.Size(111, 28)
        Me.BtnAttachments.TabIndex = 929
        Me.BtnAttachments.Text = "Attachments"
        Me.BtnAttachments.UseVisualStyleBackColor = False
        '
        'TxtFilePath
        '
        Me.TxtFilePath.AgAllowUserToEnableMasterHelp = False
        Me.TxtFilePath.AgLastValueTag = Nothing
        Me.TxtFilePath.AgLastValueText = Nothing
        Me.TxtFilePath.AgMandatory = True
        Me.TxtFilePath.AgMasterHelp = True
        Me.TxtFilePath.AgNumberLeftPlaces = 0
        Me.TxtFilePath.AgNumberNegetiveAllow = False
        Me.TxtFilePath.AgNumberRightPlaces = 0
        Me.TxtFilePath.AgPickFromLastValue = False
        Me.TxtFilePath.AgRowFilter = ""
        Me.TxtFilePath.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFilePath.AgSelectedValue = Nothing
        Me.TxtFilePath.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFilePath.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFilePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtFilePath.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFilePath.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFilePath.Location = New System.Drawing.Point(21, 301)
        Me.TxtFilePath.MaxLength = 50
        Me.TxtFilePath.Multiline = True
        Me.TxtFilePath.Name = "TxtFilePath"
        Me.TxtFilePath.ReadOnly = True
        Me.TxtFilePath.Size = New System.Drawing.Size(281, 20)
        Me.TxtFilePath.TabIndex = 930
        '
        'FrmWhatsapp1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(451, 389)
        Me.Controls.Add(Me.TxtFilePath)
        Me.Controls.Add(Me.BtnAttachments)
        Me.Controls.Add(Me.BtnTo)
        Me.Controls.Add(Me.LblToEmail)
        Me.Controls.Add(Me.TxtToMobile)
        Me.Controls.Add(Me.TxtMessage)
        Me.Controls.Add(Me.BtnSendWhatsapp)
        Me.MaximizeBox = False
        Me.Name = "FrmWhatsapp1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Send Whatsapp"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnSendWhatsapp As Button
    Public WithEvents TxtMessage As AgControls.AgTextBox
    Friend WithEvents BtnTo As Button
    Public WithEvents LblToEmail As Label
    Public WithEvents TxtToMobile As AgControls.AgTextBox
    Friend WithEvents BtnAttachments As Button
    Public WithEvents TxtFilePath As AgControls.AgTextBox
End Class
