<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmRestoreDatabase
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmRestoreDatabase))
        Me.LblDatabasePath = New System.Windows.Forms.Label()
        Me.TxtDatabaseName = New AgControls.AgTextBox()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TxtDatabaseFilePath = New AgControls.AgTextBox()
        Me.LblServerName = New System.Windows.Forms.Label()
        Me.LblAcGroupReq = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LblTitle = New System.Windows.Forms.Label()
        Me.Opn = New System.Windows.Forms.OpenFileDialog()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.BtnSelectFile = New System.Windows.Forms.Button()
        Me.TxtBackupFilePath = New AgControls.AgTextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LblDatabasePath
        '
        resources.ApplyResources(Me.LblDatabasePath, "LblDatabasePath")
        Me.LblDatabasePath.Name = "LblDatabasePath"
        '
        'TxtDatabaseName
        '
        Me.TxtDatabaseName.AgAllowUserToEnableMasterHelp = False
        Me.TxtDatabaseName.AgLastValueTag = Nothing
        Me.TxtDatabaseName.AgLastValueText = Nothing
        Me.TxtDatabaseName.AgMandatory = True
        Me.TxtDatabaseName.AgMasterHelp = True
        Me.TxtDatabaseName.AgNumberLeftPlaces = 0
        Me.TxtDatabaseName.AgNumberNegetiveAllow = False
        Me.TxtDatabaseName.AgNumberRightPlaces = 0
        Me.TxtDatabaseName.AgPickFromLastValue = False
        Me.TxtDatabaseName.AgRowFilter = ""
        Me.TxtDatabaseName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDatabaseName.AgSelectedValue = Nothing
        Me.TxtDatabaseName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDatabaseName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDatabaseName.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.TxtDatabaseName, "TxtDatabaseName")
        Me.TxtDatabaseName.Name = "TxtDatabaseName"
        '
        'BtnCancel
        '
        resources.ApplyResources(Me.BtnCancel, "BtnCancel")
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'BtnOK
        '
        resources.ApplyResources(Me.BtnOK, "BtnOK")
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        '
        'TxtDatabaseFilePath
        '
        Me.TxtDatabaseFilePath.AgAllowUserToEnableMasterHelp = False
        Me.TxtDatabaseFilePath.AgLastValueTag = Nothing
        Me.TxtDatabaseFilePath.AgLastValueText = Nothing
        Me.TxtDatabaseFilePath.AgMandatory = True
        Me.TxtDatabaseFilePath.AgMasterHelp = True
        Me.TxtDatabaseFilePath.AgNumberLeftPlaces = 0
        Me.TxtDatabaseFilePath.AgNumberNegetiveAllow = False
        Me.TxtDatabaseFilePath.AgNumberRightPlaces = 0
        Me.TxtDatabaseFilePath.AgPickFromLastValue = False
        Me.TxtDatabaseFilePath.AgRowFilter = ""
        Me.TxtDatabaseFilePath.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDatabaseFilePath.AgSelectedValue = Nothing
        Me.TxtDatabaseFilePath.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDatabaseFilePath.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDatabaseFilePath.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.TxtDatabaseFilePath, "TxtDatabaseFilePath")
        Me.TxtDatabaseFilePath.Name = "TxtDatabaseFilePath"
        '
        'LblServerName
        '
        resources.ApplyResources(Me.LblServerName, "LblServerName")
        Me.LblServerName.Name = "LblServerName"
        '
        'LblAcGroupReq
        '
        resources.ApplyResources(Me.LblAcGroupReq, "LblAcGroupReq")
        Me.LblAcGroupReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblAcGroupReq.Name = "LblAcGroupReq"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Name = "Label1"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.LblTitle)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        '
        'LblTitle
        '
        resources.ApplyResources(Me.LblTitle, "LblTitle")
        Me.LblTitle.Name = "LblTitle"
        '
        'Opn
        '
        Me.Opn.FileName = "OpenFileDialog1"
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label7.Name = "Label7"
        '
        'BtnSelectFile
        '
        resources.ApplyResources(Me.BtnSelectFile, "BtnSelectFile")
        Me.BtnSelectFile.Name = "BtnSelectFile"
        Me.BtnSelectFile.UseVisualStyleBackColor = True
        '
        'TxtBackupFilePath
        '
        Me.TxtBackupFilePath.AgAllowUserToEnableMasterHelp = False
        Me.TxtBackupFilePath.AgLastValueTag = Nothing
        Me.TxtBackupFilePath.AgLastValueText = Nothing
        Me.TxtBackupFilePath.AgMandatory = True
        Me.TxtBackupFilePath.AgMasterHelp = True
        Me.TxtBackupFilePath.AgNumberLeftPlaces = 0
        Me.TxtBackupFilePath.AgNumberNegetiveAllow = False
        Me.TxtBackupFilePath.AgNumberRightPlaces = 0
        Me.TxtBackupFilePath.AgPickFromLastValue = False
        Me.TxtBackupFilePath.AgRowFilter = ""
        Me.TxtBackupFilePath.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBackupFilePath.AgSelectedValue = Nothing
        Me.TxtBackupFilePath.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBackupFilePath.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBackupFilePath.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.TxtBackupFilePath, "TxtBackupFilePath")
        Me.TxtBackupFilePath.Name = "TxtBackupFilePath"
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Name = "Label6"
        '
        'FrmRestoreDatabase
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.BtnSelectFile)
        Me.Controls.Add(Me.TxtBackupFilePath)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblAcGroupReq)
        Me.Controls.Add(Me.TxtDatabaseFilePath)
        Me.Controls.Add(Me.LblServerName)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TxtDatabaseName)
        Me.Controls.Add(Me.LblDatabasePath)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmRestoreDatabase"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents LblDatabasePath As Label
    Public WithEvents TxtDatabaseName As AgControls.AgTextBox
    Public WithEvents BtnCancel As Button
    Public WithEvents BtnOK As Button
    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents TxtDatabaseFilePath As AgControls.AgTextBox
    Public WithEvents LblServerName As Label
    Protected WithEvents LblAcGroupReq As Label
    Protected WithEvents Label1 As Label
    Public WithEvents Panel1 As Panel
    Public WithEvents LblTitle As Label
    Friend WithEvents Opn As OpenFileDialog
    Protected WithEvents Label7 As Label
    Public WithEvents BtnSelectFile As Button
    Public WithEvents TxtBackupFilePath As AgControls.AgTextBox
    Public WithEvents Label6 As Label
End Class
