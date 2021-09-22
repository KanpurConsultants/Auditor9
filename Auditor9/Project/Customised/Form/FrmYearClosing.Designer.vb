<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmYearClosing
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmYearClosing))
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LblTitle = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtPassword = New AgControls.AgTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.FBDExportPath = New System.Windows.Forms.FolderBrowserDialog()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
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
        'FrmYearClosing
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtPassword)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmYearClosing"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents BtnCancel As Button
    Public WithEvents BtnOK As Button
    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents Panel1 As Panel
    Public WithEvents LblTitle As Label
    Protected WithEvents Label4 As Label
    Public WithEvents TxtPassword As AgControls.AgTextBox
    Public WithEvents Label5 As Label
    Friend WithEvents FBDExportPath As FolderBrowserDialog
End Class
