<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAgZip
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnDirBrowse = New System.Windows.Forms.Button
        Me.btnZipUp = New System.Windows.Forms.Button
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.ProgressBar2 = New System.Windows.Forms.ProgressBar
        Me.LblStatus = New System.Windows.Forms.Label
        Me.btnCancel = New System.Windows.Forms.Button
        Me.TxtDirToZip = New AgControls.AgTextBox
        Me.TxtZipToCreate = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.LblBank_NameReq = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Directory To Zip"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(109, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Zip File To Create"
        '
        'btnDirBrowse
        '
        Me.btnDirBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDirBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDirBrowse.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDirBrowse.Location = New System.Drawing.Point(470, 33)
        Me.btnDirBrowse.Name = "btnDirBrowse"
        Me.btnDirBrowse.Size = New System.Drawing.Size(32, 21)
        Me.btnDirBrowse.TabIndex = 4
        Me.btnDirBrowse.Text = "..."
        Me.btnDirBrowse.UseVisualStyleBackColor = True
        '
        'btnZipUp
        '
        Me.btnZipUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZipUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnZipUp.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnZipUp.Location = New System.Drawing.Point(408, 107)
        Me.btnZipUp.Name = "btnZipUp"
        Me.btnZipUp.Size = New System.Drawing.Size(94, 23)
        Me.btnZipUp.TabIndex = 0
        Me.btnZipUp.Text = "Zip It!"
        Me.btnZipUp.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(11, 140)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(490, 13)
        Me.ProgressBar1.TabIndex = 6
        '
        'ProgressBar2
        '
        Me.ProgressBar2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar2.Location = New System.Drawing.Point(11, 159)
        Me.ProgressBar2.Name = "ProgressBar2"
        Me.ProgressBar2.Size = New System.Drawing.Size(490, 13)
        Me.ProgressBar2.TabIndex = 7
        '
        'LblStatus
        '
        Me.LblStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblStatus.AutoSize = True
        Me.LblStatus.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblStatus.Location = New System.Drawing.Point(11, 182)
        Me.LblStatus.Name = "LblStatus"
        Me.LblStatus.Size = New System.Drawing.Size(19, 13)
        Me.LblStatus.TabIndex = 8
        Me.LblStatus.Text = "..."
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Enabled = False
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(308, 107)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(94, 23)
        Me.btnCancel.TabIndex = 8
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'TxtDirToZip
        '
        Me.TxtDirToZip.AgMandatory = True
        Me.TxtDirToZip.AgMasterHelp = True
        Me.TxtDirToZip.AgNumberLeftPlaces = 0
        Me.TxtDirToZip.AgNumberNegetiveAllow = False
        Me.TxtDirToZip.AgNumberRightPlaces = 0
        Me.TxtDirToZip.AgPickFromLastValue = False
        Me.TxtDirToZip.AgRowFilter = ""
        Me.TxtDirToZip.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDirToZip.AgSelectedValue = Nothing
        Me.TxtDirToZip.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDirToZip.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDirToZip.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDirToZip.Location = New System.Drawing.Point(140, 33)
        Me.TxtDirToZip.MaxLength = 0
        Me.TxtDirToZip.Name = "TxtDirToZip"
        Me.TxtDirToZip.Size = New System.Drawing.Size(325, 21)
        Me.TxtDirToZip.TabIndex = 9
        '
        'TxtZipToCreate
        '
        Me.TxtZipToCreate.AgMandatory = True
        Me.TxtZipToCreate.AgMasterHelp = True
        Me.TxtZipToCreate.AgNumberLeftPlaces = 0
        Me.TxtZipToCreate.AgNumberNegetiveAllow = False
        Me.TxtZipToCreate.AgNumberRightPlaces = 0
        Me.TxtZipToCreate.AgPickFromLastValue = False
        Me.TxtZipToCreate.AgRowFilter = ""
        Me.TxtZipToCreate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtZipToCreate.AgSelectedValue = Nothing
        Me.TxtZipToCreate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtZipToCreate.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtZipToCreate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtZipToCreate.Location = New System.Drawing.Point(140, 55)
        Me.TxtZipToCreate.MaxLength = 0
        Me.TxtZipToCreate.Name = "TxtZipToCreate"
        Me.TxtZipToCreate.Size = New System.Drawing.Size(325, 21)
        Me.TxtZipToCreate.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(128, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 7)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Ä"
        '
        'LblBank_NameReq
        '
        Me.LblBank_NameReq.AutoSize = True
        Me.LblBank_NameReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblBank_NameReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblBank_NameReq.Location = New System.Drawing.Point(128, 40)
        Me.LblBank_NameReq.Name = "LblBank_NameReq"
        Me.LblBank_NameReq.Size = New System.Drawing.Size(10, 7)
        Me.LblBank_NameReq.TabIndex = 11
        Me.LblBank_NameReq.Text = "Ä"
        '
        'FrmAgZip
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(523, 220)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.LblBank_NameReq)
        Me.Controls.Add(Me.TxtZipToCreate)
        Me.Controls.Add(Me.TxtDirToZip)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.LblStatus)
        Me.Controls.Add(Me.ProgressBar2)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.btnZipUp)
        Me.Controls.Add(Me.btnDirBrowse)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(500, 208)
        Me.Name = "FrmAgZip"
        Me.Text = "AG Zip Creator"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnDirBrowse As System.Windows.Forms.Button
    Friend WithEvents btnZipUp As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents ProgressBar2 As System.Windows.Forms.ProgressBar
    Friend WithEvents LblStatus As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents TxtDirToZip As AgControls.AgTextBox
    Friend WithEvents TxtZipToCreate As AgControls.AgTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents LblBank_NameReq As System.Windows.Forms.Label

End Class
