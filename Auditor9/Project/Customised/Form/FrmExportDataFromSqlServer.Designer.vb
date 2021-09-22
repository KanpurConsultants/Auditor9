<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmExportDataFromSqlServer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmExportDataFromSqlServer))
        Me.LblDatabasePath = New System.Windows.Forms.Label()
        Me.TxtDatabaseName = New AgControls.AgTextBox()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TxtServerName = New AgControls.AgTextBox()
        Me.LblServerName = New System.Windows.Forms.Label()
        Me.LblAcGroupReq = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LblTitle = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtUserName = New AgControls.AgTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtPassword = New AgControls.AgTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtExportPath = New AgControls.AgTextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.BtnSelectFile = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.FBDExportPath = New System.Windows.Forms.FolderBrowserDialog()
        Me.TxtExportSpecific = New AgControls.AgTextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TxtFromSoftware = New AgControls.AgTextBox()
        Me.Label10 = New System.Windows.Forms.Label()
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
        'TxtServerName
        '
        Me.TxtServerName.AgAllowUserToEnableMasterHelp = False
        Me.TxtServerName.AgLastValueTag = Nothing
        Me.TxtServerName.AgLastValueText = Nothing
        Me.TxtServerName.AgMandatory = True
        Me.TxtServerName.AgMasterHelp = True
        Me.TxtServerName.AgNumberLeftPlaces = 0
        Me.TxtServerName.AgNumberNegetiveAllow = False
        Me.TxtServerName.AgNumberRightPlaces = 0
        Me.TxtServerName.AgPickFromLastValue = False
        Me.TxtServerName.AgRowFilter = ""
        Me.TxtServerName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtServerName.AgSelectedValue = Nothing
        Me.TxtServerName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtServerName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtServerName.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.TxtServerName, "TxtServerName")
        Me.TxtServerName.Name = "TxtServerName"
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
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Name = "Label2"
        '
        'TxtUserName
        '
        Me.TxtUserName.AgAllowUserToEnableMasterHelp = False
        Me.TxtUserName.AgLastValueTag = Nothing
        Me.TxtUserName.AgLastValueText = Nothing
        Me.TxtUserName.AgMandatory = True
        Me.TxtUserName.AgMasterHelp = True
        Me.TxtUserName.AgNumberLeftPlaces = 0
        Me.TxtUserName.AgNumberNegetiveAllow = False
        Me.TxtUserName.AgNumberRightPlaces = 0
        Me.TxtUserName.AgPickFromLastValue = False
        Me.TxtUserName.AgRowFilter = ""
        Me.TxtUserName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtUserName.AgSelectedValue = Nothing
        Me.TxtUserName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtUserName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtUserName.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.TxtUserName, "TxtUserName")
        Me.TxtUserName.Name = "TxtUserName"
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Name = "Label4"
        '
        'TxtPassword
        '
        Me.TxtPassword.AgAllowUserToEnableMasterHelp = False
        Me.TxtPassword.AgLastValueTag = Nothing
        Me.TxtPassword.AgLastValueText = Nothing
        Me.TxtPassword.AgMandatory = True
        Me.TxtPassword.AgMasterHelp = True
        Me.TxtPassword.AgNumberLeftPlaces = 0
        Me.TxtPassword.AgNumberNegetiveAllow = False
        Me.TxtPassword.AgNumberRightPlaces = 0
        Me.TxtPassword.AgPickFromLastValue = False
        Me.TxtPassword.AgRowFilter = ""
        Me.TxtPassword.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPassword.AgSelectedValue = Nothing
        Me.TxtPassword.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPassword.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.TxtPassword, "TxtPassword")
        Me.TxtPassword.Name = "TxtPassword"
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        '
        'TxtExportPath
        '
        Me.TxtExportPath.AgAllowUserToEnableMasterHelp = False
        Me.TxtExportPath.AgLastValueTag = Nothing
        Me.TxtExportPath.AgLastValueText = Nothing
        Me.TxtExportPath.AgMandatory = True
        Me.TxtExportPath.AgMasterHelp = True
        Me.TxtExportPath.AgNumberLeftPlaces = 0
        Me.TxtExportPath.AgNumberNegetiveAllow = False
        Me.TxtExportPath.AgNumberRightPlaces = 0
        Me.TxtExportPath.AgPickFromLastValue = False
        Me.TxtExportPath.AgRowFilter = ""
        Me.TxtExportPath.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtExportPath.AgSelectedValue = Nothing
        Me.TxtExportPath.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtExportPath.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtExportPath.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.TxtExportPath, "TxtExportPath")
        Me.TxtExportPath.Name = "TxtExportPath"
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Name = "Label6"
        '
        'BtnSelectFile
        '
        resources.ApplyResources(Me.BtnSelectFile, "BtnSelectFile")
        Me.BtnSelectFile.Name = "BtnSelectFile"
        Me.BtnSelectFile.UseVisualStyleBackColor = True
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label7.Name = "Label7"
        '
        'TxtExportSpecific
        '
        Me.TxtExportSpecific.AgAllowUserToEnableMasterHelp = False
        Me.TxtExportSpecific.AgLastValueTag = Nothing
        Me.TxtExportSpecific.AgLastValueText = Nothing
        Me.TxtExportSpecific.AgMandatory = True
        Me.TxtExportSpecific.AgMasterHelp = False
        Me.TxtExportSpecific.AgNumberLeftPlaces = 0
        Me.TxtExportSpecific.AgNumberNegetiveAllow = False
        Me.TxtExportSpecific.AgNumberRightPlaces = 0
        Me.TxtExportSpecific.AgPickFromLastValue = False
        Me.TxtExportSpecific.AgRowFilter = ""
        Me.TxtExportSpecific.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtExportSpecific.AgSelectedValue = Nothing
        Me.TxtExportSpecific.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtExportSpecific.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtExportSpecific.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.TxtExportSpecific, "TxtExportSpecific")
        Me.TxtExportSpecific.Name = "TxtExportSpecific"
        '
        'Label8
        '
        resources.ApplyResources(Me.Label8, "Label8")
        Me.Label8.Name = "Label8"
        '
        'Label9
        '
        resources.ApplyResources(Me.Label9, "Label9")
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label9.Name = "Label9"
        '
        'TxtFromSoftware
        '
        Me.TxtFromSoftware.AgAllowUserToEnableMasterHelp = False
        Me.TxtFromSoftware.AgLastValueTag = Nothing
        Me.TxtFromSoftware.AgLastValueText = Nothing
        Me.TxtFromSoftware.AgMandatory = True
        Me.TxtFromSoftware.AgMasterHelp = False
        Me.TxtFromSoftware.AgNumberLeftPlaces = 0
        Me.TxtFromSoftware.AgNumberNegetiveAllow = False
        Me.TxtFromSoftware.AgNumberRightPlaces = 0
        Me.TxtFromSoftware.AgPickFromLastValue = False
        Me.TxtFromSoftware.AgRowFilter = ""
        Me.TxtFromSoftware.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFromSoftware.AgSelectedValue = Nothing
        Me.TxtFromSoftware.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFromSoftware.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFromSoftware.BorderStyle = System.Windows.Forms.BorderStyle.None
        resources.ApplyResources(Me.TxtFromSoftware, "TxtFromSoftware")
        Me.TxtFromSoftware.Name = "TxtFromSoftware"
        '
        'Label10
        '
        resources.ApplyResources(Me.Label10, "Label10")
        Me.Label10.Name = "Label10"
        '
        'FrmExportDataFromSqlServer
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TxtFromSoftware)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.TxtExportSpecific)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.BtnSelectFile)
        Me.Controls.Add(Me.TxtExportPath)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtPassword)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtUserName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblAcGroupReq)
        Me.Controls.Add(Me.TxtServerName)
        Me.Controls.Add(Me.LblServerName)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TxtDatabaseName)
        Me.Controls.Add(Me.LblDatabasePath)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmExportDataFromSqlServer"
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
    Public WithEvents TxtServerName As AgControls.AgTextBox
    Public WithEvents LblServerName As Label
    Protected WithEvents LblAcGroupReq As Label
    Protected WithEvents Label1 As Label
    Public WithEvents Panel1 As Panel
    Public WithEvents LblTitle As Label
    Protected WithEvents Label2 As Label
    Public WithEvents TxtUserName As AgControls.AgTextBox
    Public WithEvents Label3 As Label
    Protected WithEvents Label4 As Label
    Public WithEvents TxtPassword As AgControls.AgTextBox
    Public WithEvents Label5 As Label
    Public WithEvents TxtExportPath As AgControls.AgTextBox
    Public WithEvents Label6 As Label
    Public WithEvents BtnSelectFile As Button
    Protected WithEvents Label7 As Label
    Friend WithEvents FBDExportPath As FolderBrowserDialog
    Public WithEvents TxtExportSpecific As AgControls.AgTextBox
    Public WithEvents Label8 As Label
    Protected WithEvents Label9 As Label
    Public WithEvents TxtFromSoftware As AgControls.AgTextBox
    Public WithEvents Label10 As Label
End Class
