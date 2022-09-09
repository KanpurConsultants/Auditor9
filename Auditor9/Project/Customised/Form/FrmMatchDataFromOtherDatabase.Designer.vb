<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMatchDataFromOtherDatabase
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.TxtExcelPath = New AgControls.AgTextBox()
        Me.BtnSelectExcelFile = New System.Windows.Forms.Button()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Opn = New System.Windows.Forms.OpenFileDialog()
        Me.LblChildProgress = New System.Windows.Forms.Label()
        Me.PrgBarChild = New System.Windows.Forms.ProgressBar()
        Me.PrgBarParent = New System.Windows.Forms.ProgressBar()
        Me.LblParentProgress = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.PnlMain = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(0, 509)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(627, 4)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(0, 37)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(595, 395)
        Me.Pnl1.TabIndex = 10
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
        Me.TxtExcelPath.Location = New System.Drawing.Point(0, 474)
        Me.TxtExcelPath.MaxLength = 50
        Me.TxtExcelPath.Multiline = True
        Me.TxtExcelPath.Name = "TxtExcelPath"
        Me.TxtExcelPath.Size = New System.Drawing.Size(557, 20)
        Me.TxtExcelPath.TabIndex = 666
        '
        'BtnSelectExcelFile
        '
        Me.BtnSelectExcelFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSelectExcelFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSelectExcelFile.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSelectExcelFile.Location = New System.Drawing.Point(563, 472)
        Me.BtnSelectExcelFile.Name = "BtnSelectExcelFile"
        Me.BtnSelectExcelFile.Size = New System.Drawing.Size(31, 23)
        Me.BtnSelectExcelFile.TabIndex = 667
        Me.BtnSelectExcelFile.Text = "..."
        Me.BtnSelectExcelFile.UseVisualStyleBackColor = True
        '
        'BtnOK
        '
        Me.BtnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOK.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOK.Location = New System.Drawing.Point(455, 623)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(64, 23)
        Me.BtnOK.TabIndex = 668
        Me.BtnOK.Text = "OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancel.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancel.Location = New System.Drawing.Point(525, 623)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(64, 23)
        Me.BtnCancel.TabIndex = 669
        Me.BtnCancel.Text = "Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Location = New System.Drawing.Point(-3, 452)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(627, 4)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label2.Location = New System.Drawing.Point(0, 442)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 23)
        Me.Label2.TabIndex = 670
        Me.Label2.Text = "Select File"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.White
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label3.Location = New System.Drawing.Point(4, 446)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(98, 23)
        Me.Label3.TabIndex = 671
        Me.Label3.Text = "Label3"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Opn
        '
        Me.Opn.FileName = "OpenFileDialog1"
        '
        'LblChildProgress
        '
        Me.LblChildProgress.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LblChildProgress.AutoSize = True
        Me.LblChildProgress.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblChildProgress.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.LblChildProgress.Location = New System.Drawing.Point(2, 522)
        Me.LblChildProgress.Name = "LblChildProgress"
        Me.LblChildProgress.Size = New System.Drawing.Size(19, 14)
        Me.LblChildProgress.TabIndex = 674
        Me.LblChildProgress.Text = "..."
        '
        'PrgBarChild
        '
        Me.PrgBarChild.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PrgBarChild.Location = New System.Drawing.Point(4, 541)
        Me.PrgBarChild.Maximum = 1960
        Me.PrgBarChild.Name = "PrgBarChild"
        Me.PrgBarChild.Size = New System.Drawing.Size(586, 22)
        Me.PrgBarChild.Step = 2
        Me.PrgBarChild.TabIndex = 675
        '
        'PrgBarParent
        '
        Me.PrgBarParent.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PrgBarParent.Location = New System.Drawing.Point(4, 582)
        Me.PrgBarParent.Maximum = 12
        Me.PrgBarParent.Name = "PrgBarParent"
        Me.PrgBarParent.Size = New System.Drawing.Size(586, 22)
        Me.PrgBarParent.TabIndex = 676
        '
        'LblParentProgress
        '
        Me.LblParentProgress.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LblParentProgress.AutoSize = True
        Me.LblParentProgress.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblParentProgress.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.LblParentProgress.Location = New System.Drawing.Point(2, 566)
        Me.LblParentProgress.Name = "LblParentProgress"
        Me.LblParentProgress.Size = New System.Drawing.Size(19, 14)
        Me.LblParentProgress.TabIndex = 677
        Me.LblParentProgress.Text = "..."
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox3.Location = New System.Drawing.Point(-15, 611)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(627, 4)
        Me.GroupBox3.TabIndex = 678
        Me.GroupBox3.TabStop = False
        '
        'PnlMain
        '
        Me.PnlMain.Location = New System.Drawing.Point(0, 2)
        Me.PnlMain.Name = "PnlMain"
        Me.PnlMain.Size = New System.Drawing.Size(595, 32)
        Me.PnlMain.TabIndex = 680
        '
        'FrmImportData_SadhviMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(596, 646)
        Me.Controls.Add(Me.PnlMain)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.LblParentProgress)
        Me.Controls.Add(Me.LblChildProgress)
        Me.Controls.Add(Me.PrgBarParent)
        Me.Controls.Add(Me.PrgBarChild)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.BtnSelectExcelFile)
        Me.Controls.Add(Me.TxtExcelPath)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.Name = "FrmImportData_SadhviMain"
        Me.Text = "Import From Excel"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents Pnl1 As System.Windows.Forms.Panel
    Public WithEvents TxtExcelPath As AgControls.AgTextBox
    Public WithEvents BtnSelectExcelFile As System.Windows.Forms.Button
    Public WithEvents BtnOK As System.Windows.Forms.Button
    Public WithEvents BtnCancel As System.Windows.Forms.Button
    Public WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Opn As System.Windows.Forms.OpenFileDialog
    Friend WithEvents LblChildProgress As Label
    Friend WithEvents PrgBarChild As ProgressBar
    Friend WithEvents PrgBarParent As ProgressBar
    Friend WithEvents LblParentProgress As Label
    Public WithEvents GroupBox3 As GroupBox
    Public WithEvents PnlMain As Panel
End Class
