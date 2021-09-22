<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmVoucherTypeTimePlan
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
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.TxtFindDateLockSetting = New System.Windows.Forms.TextBox()
        Me.TC1 = New System.Windows.Forms.TabControl()
        Me.TpVoucherTypeDateLockSetting = New System.Windows.Forms.TabPage()
        Me.TPTimePlanSetting = New System.Windows.Forms.TabPage()
        Me.TxtFindTimePlanSetting = New System.Windows.Forms.TextBox()
        Me.Pnl2 = New System.Windows.Forms.Panel()
        Me.TPFinancialDateLockSetting = New System.Windows.Forms.TabPage()
        Me.Pnl3 = New System.Windows.Forms.Panel()
        Me.TxtFindFinancialYearLockSetting = New System.Windows.Forms.TextBox()
        Me.TC1.SuspendLayout()
        Me.TpVoucherTypeDateLockSetting.SuspendLayout()
        Me.TPTimePlanSetting.SuspendLayout()
        Me.TPFinancialDateLockSetting.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(0, 578)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1005, 4)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl1.Location = New System.Drawing.Point(4, 29)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(963, 509)
        Me.Pnl1.TabIndex = 10
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.Location = New System.Drawing.Point(898, 588)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(64, 25)
        Me.BtnClose.TabIndex = 669
        Me.BtnClose.Text = "Close"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'TxtFindDateLockSetting
        '
        Me.TxtFindDateLockSetting.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtFindDateLockSetting.Enabled = False
        Me.TxtFindDateLockSetting.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFindDateLockSetting.Location = New System.Drawing.Point(3, 2)
        Me.TxtFindDateLockSetting.Name = "TxtFindDateLockSetting"
        Me.TxtFindDateLockSetting.Size = New System.Drawing.Size(963, 26)
        Me.TxtFindDateLockSetting.TabIndex = 671
        Me.TxtFindDateLockSetting.Visible = False
        '
        'TC1
        '
        Me.TC1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TC1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TC1.Controls.Add(Me.TpVoucherTypeDateLockSetting)
        Me.TC1.Controls.Add(Me.TPTimePlanSetting)
        Me.TC1.Controls.Add(Me.TPFinancialDateLockSetting)
        Me.TC1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TC1.Location = New System.Drawing.Point(0, 2)
        Me.TC1.Name = "TC1"
        Me.TC1.SelectedIndex = 0
        Me.TC1.Size = New System.Drawing.Size(977, 573)
        Me.TC1.TabIndex = 0
        '
        'TpVoucherTypeDateLockSetting
        '
        Me.TpVoucherTypeDateLockSetting.Controls.Add(Me.Pnl1)
        Me.TpVoucherTypeDateLockSetting.Controls.Add(Me.TxtFindDateLockSetting)
        Me.TpVoucherTypeDateLockSetting.Location = New System.Drawing.Point(4, 25)
        Me.TpVoucherTypeDateLockSetting.Name = "TpVoucherTypeDateLockSetting"
        Me.TpVoucherTypeDateLockSetting.Padding = New System.Windows.Forms.Padding(3)
        Me.TpVoucherTypeDateLockSetting.Size = New System.Drawing.Size(969, 544)
        Me.TpVoucherTypeDateLockSetting.TabIndex = 0
        Me.TpVoucherTypeDateLockSetting.Text = "Date Lock Setting"
        Me.TpVoucherTypeDateLockSetting.UseVisualStyleBackColor = True
        '
        'TPTimePlanSetting
        '
        Me.TPTimePlanSetting.Controls.Add(Me.TxtFindTimePlanSetting)
        Me.TPTimePlanSetting.Controls.Add(Me.Pnl2)
        Me.TPTimePlanSetting.Location = New System.Drawing.Point(4, 25)
        Me.TPTimePlanSetting.Name = "TPTimePlanSetting"
        Me.TPTimePlanSetting.Padding = New System.Windows.Forms.Padding(3)
        Me.TPTimePlanSetting.Size = New System.Drawing.Size(969, 544)
        Me.TPTimePlanSetting.TabIndex = 1
        Me.TPTimePlanSetting.Text = "Time Plan Setting"
        Me.TPTimePlanSetting.UseVisualStyleBackColor = True
        '
        'TxtFindTimePlanSetting
        '
        Me.TxtFindTimePlanSetting.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtFindTimePlanSetting.Enabled = False
        Me.TxtFindTimePlanSetting.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFindTimePlanSetting.Location = New System.Drawing.Point(3, 3)
        Me.TxtFindTimePlanSetting.Name = "TxtFindTimePlanSetting"
        Me.TxtFindTimePlanSetting.Size = New System.Drawing.Size(963, 26)
        Me.TxtFindTimePlanSetting.TabIndex = 672
        Me.TxtFindTimePlanSetting.Visible = False
        '
        'Pnl2
        '
        Me.Pnl2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl2.Location = New System.Drawing.Point(3, 30)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(963, 509)
        Me.Pnl2.TabIndex = 11
        '
        'TPFinancialDateLockSetting
        '
        Me.TPFinancialDateLockSetting.Controls.Add(Me.TxtFindFinancialYearLockSetting)
        Me.TPFinancialDateLockSetting.Controls.Add(Me.Pnl3)
        Me.TPFinancialDateLockSetting.Location = New System.Drawing.Point(4, 25)
        Me.TPFinancialDateLockSetting.Name = "TPFinancialDateLockSetting"
        Me.TPFinancialDateLockSetting.Padding = New System.Windows.Forms.Padding(3)
        Me.TPFinancialDateLockSetting.Size = New System.Drawing.Size(969, 544)
        Me.TPFinancialDateLockSetting.TabIndex = 2
        Me.TPFinancialDateLockSetting.Text = "Financial Year Date Lock Setting"
        Me.TPFinancialDateLockSetting.UseVisualStyleBackColor = True
        '
        'Pnl3
        '
        Me.Pnl3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl3.Location = New System.Drawing.Point(3, 30)
        Me.Pnl3.Name = "Pnl3"
        Me.Pnl3.Size = New System.Drawing.Size(963, 497)
        Me.Pnl3.TabIndex = 12
        '
        'TxtFindFinancialYearLockSetting
        '
        Me.TxtFindFinancialYearLockSetting.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtFindFinancialYearLockSetting.Enabled = False
        Me.TxtFindFinancialYearLockSetting.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFindFinancialYearLockSetting.Location = New System.Drawing.Point(0, 3)
        Me.TxtFindFinancialYearLockSetting.Name = "TxtFindFinancialYearLockSetting"
        Me.TxtFindFinancialYearLockSetting.Size = New System.Drawing.Size(963, 26)
        Me.TxtFindFinancialYearLockSetting.TabIndex = 672
        Me.TxtFindFinancialYearLockSetting.Visible = False
        '
        'FrmVoucherTypeTimePlan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(974, 615)
        Me.Controls.Add(Me.TC1)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.Name = "FrmVoucherTypeTimePlan"
        Me.Text = "Time Plan Settings"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TC1.ResumeLayout(False)
        Me.TpVoucherTypeDateLockSetting.ResumeLayout(False)
        Me.TpVoucherTypeDateLockSetting.PerformLayout()
        Me.TPTimePlanSetting.ResumeLayout(False)
        Me.TPTimePlanSetting.PerformLayout()
        Me.TPFinancialDateLockSetting.ResumeLayout(False)
        Me.TPFinancialDateLockSetting.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Public WithEvents Pnl1 As System.Windows.Forms.Panel
    Public WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents TxtFindDateLockSetting As TextBox
    Friend WithEvents TC1 As TabControl
    Friend WithEvents TpVoucherTypeDateLockSetting As TabPage
    Friend WithEvents TPTimePlanSetting As TabPage
    Public WithEvents Pnl2 As Panel
    Friend WithEvents TxtFindTimePlanSetting As TextBox
    Friend WithEvents TPFinancialDateLockSetting As TabPage
    Public WithEvents Pnl3 As Panel
    Friend WithEvents TxtFindFinancialYearLockSetting As TextBox
End Class
