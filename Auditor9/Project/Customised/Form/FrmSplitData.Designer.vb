<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSplitData
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
        Me.BtnSync = New System.Windows.Forms.Button()
        Me.LblProgress = New System.Windows.Forms.Label()
        Me.BtnSelectExcelFile = New System.Windows.Forms.Button()
        Me.TxtExcelPath = New AgControls.AgTextBox()
        Me.SuspendLayout()
        '
        'BtnSync
        '
        Me.BtnSync.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSync.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSync.Location = New System.Drawing.Point(111, 76)
        Me.BtnSync.Name = "BtnSync"
        Me.BtnSync.Size = New System.Drawing.Size(160, 23)
        Me.BtnSync.TabIndex = 0
        Me.BtnSync.Text = "Sync"
        Me.BtnSync.UseVisualStyleBackColor = True
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
        'BtnSelectExcelFile
        '
        Me.BtnSelectExcelFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSelectExcelFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSelectExcelFile.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSelectExcelFile.Location = New System.Drawing.Point(395, 168)
        Me.BtnSelectExcelFile.Name = "BtnSelectExcelFile"
        Me.BtnSelectExcelFile.Size = New System.Drawing.Size(31, 23)
        Me.BtnSelectExcelFile.TabIndex = 669
        Me.BtnSelectExcelFile.Text = "..."
        Me.BtnSelectExcelFile.UseVisualStyleBackColor = True
        '
        'TxtExcelPath
        '
        Me.TxtExcelPath.AgAllowUserToEnableMasterHelp = False
        Me.TxtExcelPath.AgLastValueTag = Nothing
        Me.TxtExcelPath.AgLastValueText = Nothing
        Me.TxtExcelPath.AgMandatory = True
        Me.TxtExcelPath.AgMasterHelp = True
        Me.TxtExcelPath.AgNumberLeftPlaces = 0
        Me.TxtExcelPath.AgNumberNegetiveAllow = False
        Me.TxtExcelPath.AgNumberRightPlaces = 0
        Me.TxtExcelPath.AgPickFromLastValue = False
        Me.TxtExcelPath.AgRowFilter = ""
        Me.TxtExcelPath.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtExcelPath.AgSelectedValue = Nothing
        Me.TxtExcelPath.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtExcelPath.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtExcelPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtExcelPath.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtExcelPath.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtExcelPath.Location = New System.Drawing.Point(15, 169)
        Me.TxtExcelPath.MaxLength = 50
        Me.TxtExcelPath.Multiline = True
        Me.TxtExcelPath.Name = "TxtExcelPath"
        Me.TxtExcelPath.Size = New System.Drawing.Size(374, 20)
        Me.TxtExcelPath.TabIndex = 668
        '
        'FrmSplitData
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(456, 276)
        Me.Controls.Add(Me.BtnSelectExcelFile)
        Me.Controls.Add(Me.TxtExcelPath)
        Me.Controls.Add(Me.LblProgress)
        Me.Controls.Add(Me.BtnSync)
        Me.Name = "FrmSplitData"
        Me.Text = "Sync Data"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnSync As Button
    Friend WithEvents LblProgress As Label
    Public WithEvents BtnSelectExcelFile As Button
    Public WithEvents TxtExcelPath As AgControls.AgTextBox
End Class
