<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmServerNotFound
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
        Me.TxtSqlServerName = New AgControls.AgTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.TxtDatabaseName = New AgControls.AgTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtDatabasePassword = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.BtnOk = New System.Windows.Forms.Button
        Me.BtnCancel = New System.Windows.Forms.Button
        Me.BtnCreateNewDatabase = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'TxtSqlServerName
        '
        Me.TxtSqlServerName.AgMandatory = True
        Me.TxtSqlServerName.AgMasterHelp = False
        Me.TxtSqlServerName.AgNumberLeftPlaces = 0
        Me.TxtSqlServerName.AgNumberNegetiveAllow = False
        Me.TxtSqlServerName.AgNumberRightPlaces = 0
        Me.TxtSqlServerName.AgPickFromLastValue = False
        Me.TxtSqlServerName.AgRowFilter = ""
        Me.TxtSqlServerName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSqlServerName.AgSelectedValue = Nothing
        Me.TxtSqlServerName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSqlServerName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSqlServerName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSqlServerName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSqlServerName.Location = New System.Drawing.Point(144, 27)
        Me.TxtSqlServerName.MaxLength = 50
        Me.TxtSqlServerName.Name = "TxtSqlServerName"
        Me.TxtSqlServerName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TxtSqlServerName.Size = New System.Drawing.Size(304, 18)
        Me.TxtSqlServerName.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(105, 16)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Sql Server Name"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Location = New System.Drawing.Point(-7, 122)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(489, 4)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        '
        'TxtDatabaseName
        '
        Me.TxtDatabaseName.AgMandatory = True
        Me.TxtDatabaseName.AgMasterHelp = False
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
        Me.TxtDatabaseName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDatabaseName.Location = New System.Drawing.Point(144, 47)
        Me.TxtDatabaseName.MaxLength = 50
        Me.TxtDatabaseName.Name = "TxtDatabaseName"
        Me.TxtDatabaseName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TxtDatabaseName.Size = New System.Drawing.Size(304, 18)
        Me.TxtDatabaseName.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 16)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Database Name"
        '
        'TxtDatabasePassword
        '
        Me.TxtDatabasePassword.AgMandatory = True
        Me.TxtDatabasePassword.AgMasterHelp = False
        Me.TxtDatabasePassword.AgNumberLeftPlaces = 0
        Me.TxtDatabasePassword.AgNumberNegetiveAllow = False
        Me.TxtDatabasePassword.AgNumberRightPlaces = 0
        Me.TxtDatabasePassword.AgPickFromLastValue = False
        Me.TxtDatabasePassword.AgRowFilter = ""
        Me.TxtDatabasePassword.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDatabasePassword.AgSelectedValue = Nothing
        Me.TxtDatabasePassword.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDatabasePassword.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDatabasePassword.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDatabasePassword.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDatabasePassword.Location = New System.Drawing.Point(144, 67)
        Me.TxtDatabasePassword.MaxLength = 50
        Me.TxtDatabasePassword.Name = "TxtDatabasePassword"
        Me.TxtDatabasePassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(9632)
        Me.TxtDatabasePassword.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TxtDatabasePassword.Size = New System.Drawing.Size(304, 18)
        Me.TxtDatabasePassword.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 68)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(124, 16)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Database Password"
        '
        'BtnOk
        '
        Me.BtnOk.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOk.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOk.Location = New System.Drawing.Point(76, 132)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(79, 24)
        Me.BtnOk.TabIndex = 3
        Me.BtnOk.Text = "OK"
        Me.BtnOk.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancel.Location = New System.Drawing.Point(164, 132)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(79, 24)
        Me.BtnCancel.TabIndex = 4
        Me.BtnCancel.Text = "Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'BtnCreateNewDatabase
        '
        Me.BtnCreateNewDatabase.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnCreateNewDatabase.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCreateNewDatabase.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCreateNewDatabase.Location = New System.Drawing.Point(251, 132)
        Me.BtnCreateNewDatabase.Name = "BtnCreateNewDatabase"
        Me.BtnCreateNewDatabase.Size = New System.Drawing.Size(143, 24)
        Me.BtnCreateNewDatabase.TabIndex = 12
        Me.BtnCreateNewDatabase.Text = "Create New Database"
        Me.BtnCreateNewDatabase.UseVisualStyleBackColor = True
        '
        'FrmServerNotFound
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(471, 159)
        Me.Controls.Add(Me.BtnCreateNewDatabase)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOk)
        Me.Controls.Add(Me.TxtDatabasePassword)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtDatabaseName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TxtSqlServerName)
        Me.Controls.Add(Me.Label2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "FrmServerNotFound"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Database Information"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TxtSqlServerName As AgControls.AgTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtDatabaseName As AgControls.AgTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TxtDatabasePassword As AgControls.AgTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents BtnOk As System.Windows.Forms.Button
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents BtnCreateNewDatabase As System.Windows.Forms.Button
End Class
