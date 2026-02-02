<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSendWhatsappMessage
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
        Me.BtnSendMessageForTodaySaleInvoice = New System.Windows.Forms.Button()
        Me.LblProgress = New System.Windows.Forms.Label()
        Me.BtnStartToSendWhatsapp = New System.Windows.Forms.Button()
        Me.BtnSendMessageForTodayLRUpdate = New System.Windows.Forms.Button()
        Me.TxtDate = New AgControls.AgTextBox()
        Me.LblDate = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'BtnSendMessageForTodaySaleInvoice
        '
        Me.BtnSendMessageForTodaySaleInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSendMessageForTodaySaleInvoice.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSendMessageForTodaySaleInvoice.Location = New System.Drawing.Point(70, 81)
        Me.BtnSendMessageForTodaySaleInvoice.Name = "BtnSendMessageForTodaySaleInvoice"
        Me.BtnSendMessageForTodaySaleInvoice.Size = New System.Drawing.Size(303, 23)
        Me.BtnSendMessageForTodaySaleInvoice.TabIndex = 0
        Me.BtnSendMessageForTodaySaleInvoice.Text = "Send Message For Today Sale Invoice"
        Me.BtnSendMessageForTodaySaleInvoice.UseVisualStyleBackColor = True
        '
        'LblProgress
        '
        Me.LblProgress.AutoSize = True
        Me.LblProgress.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblProgress.ForeColor = System.Drawing.Color.Blue
        Me.LblProgress.Location = New System.Drawing.Point(12, 37)
        Me.LblProgress.Name = "LblProgress"
        Me.LblProgress.Size = New System.Drawing.Size(0, 18)
        Me.LblProgress.TabIndex = 1
        '
        'BtnStartToSendWhatsapp
        '
        Me.BtnStartToSendWhatsapp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnStartToSendWhatsapp.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnStartToSendWhatsapp.Location = New System.Drawing.Point(70, 49)
        Me.BtnStartToSendWhatsapp.Name = "BtnStartToSendWhatsapp"
        Me.BtnStartToSendWhatsapp.Size = New System.Drawing.Size(303, 23)
        Me.BtnStartToSendWhatsapp.TabIndex = 2
        Me.BtnStartToSendWhatsapp.Text = "Start To Send Whatsapp"
        Me.BtnStartToSendWhatsapp.UseVisualStyleBackColor = True
        '
        'BtnSendMessageForTodayLRUpdate
        '
        Me.BtnSendMessageForTodayLRUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSendMessageForTodayLRUpdate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSendMessageForTodayLRUpdate.Location = New System.Drawing.Point(71, 110)
        Me.BtnSendMessageForTodayLRUpdate.Name = "BtnSendMessageForTodayLRUpdate"
        Me.BtnSendMessageForTodayLRUpdate.Size = New System.Drawing.Size(303, 23)
        Me.BtnSendMessageForTodayLRUpdate.TabIndex = 3
        Me.BtnSendMessageForTodayLRUpdate.Text = "Send Message For Today LR Update"
        Me.BtnSendMessageForTodayLRUpdate.UseVisualStyleBackColor = True
        '
        'TxtDate
        '
        Me.TxtDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtDate.AgLastValueTag = Nothing
        Me.TxtDate.AgLastValueText = Nothing
        Me.TxtDate.AgMandatory = False
        Me.TxtDate.AgMasterHelp = False
        Me.TxtDate.AgNumberLeftPlaces = 0
        Me.TxtDate.AgNumberNegetiveAllow = False
        Me.TxtDate.AgNumberRightPlaces = 0
        Me.TxtDate.AgPickFromLastValue = False
        Me.TxtDate.AgRowFilter = ""
        Me.TxtDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDate.AgSelectedValue = Nothing
        Me.TxtDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDate.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDate.Location = New System.Drawing.Point(183, 20)
        Me.TxtDate.MaxLength = 255
        Me.TxtDate.Name = "TxtDate"
        Me.TxtDate.Size = New System.Drawing.Size(163, 16)
        Me.TxtDate.TabIndex = 743
        '
        'LblDate
        '
        Me.LblDate.AutoSize = True
        Me.LblDate.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDate.Location = New System.Drawing.Point(113, 21)
        Me.LblDate.Name = "LblDate"
        Me.LblDate.Size = New System.Drawing.Size(38, 14)
        Me.LblDate.TabIndex = 744
        Me.LblDate.Text = "Date"
        '
        'FrmSendWhatsappMessage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(456, 208)
        Me.Controls.Add(Me.TxtDate)
        Me.Controls.Add(Me.LblDate)
        Me.Controls.Add(Me.BtnSendMessageForTodayLRUpdate)
        Me.Controls.Add(Me.BtnStartToSendWhatsapp)
        Me.Controls.Add(Me.LblProgress)
        Me.Controls.Add(Me.BtnSendMessageForTodaySaleInvoice)
        Me.Name = "FrmSendWhatsappMessage"
        Me.Text = "Send Whatsapp Message"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnSendMessageForTodaySaleInvoice As Button
    Friend WithEvents LblProgress As Label
    Friend WithEvents BtnStartToSendWhatsapp As Button
    Friend WithEvents BtnSendMessageForTodayLRUpdate As Button
    Protected WithEvents TxtDate As AgControls.AgTextBox
    Protected WithEvents LblDate As Label
End Class
