<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMaster
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMaster))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.MasterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PAYEEREGISTRATIONToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ENVELOPEToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ENVELOPESIZEToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ENVELOPEADDRESSToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.BANKREGISTRATIONToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NEWBANKREGISTRATIONALIGNMENTToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ENVELOPEToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ENVELOPEADDRESSToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ENVELOPESIZEToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PRINTCHEQUEToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PRINTENVELOPEToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.REPORTToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.COMPANYWISECHEQUEISSUEREPORTToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogOutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BackupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestoreToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EXITToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.lbWecomeMsg = New System.Windows.Forms.Label()
        Me.PRINTPAYMENTVOUCHERToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MasterToolStripMenuItem, Me.PAYEEREGISTRATIONToolStripMenuItem, Me.ENVELOPEToolStripMenuItem, Me.BANKREGISTRATIONToolStripMenuItem, Me.PrintToolStripMenuItem, Me.REPORTToolStripMenuItem, Me.LogOutToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.EXITToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1184, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'MasterToolStripMenuItem
        '
        Me.MasterToolStripMenuItem.Name = "MasterToolStripMenuItem"
        Me.MasterToolStripMenuItem.Size = New System.Drawing.Size(161, 20)
        Me.MasterToolStripMenuItem.Text = "COMPANY REGISTRATION"
        '
        'PAYEEREGISTRATIONToolStripMenuItem
        '
        Me.PAYEEREGISTRATIONToolStripMenuItem.Name = "PAYEEREGISTRATIONToolStripMenuItem"
        Me.PAYEEREGISTRATIONToolStripMenuItem.Size = New System.Drawing.Size(136, 20)
        Me.PAYEEREGISTRATIONToolStripMenuItem.Text = "PAYEE REGISTRATION"
        '
        'ENVELOPEToolStripMenuItem
        '
        Me.ENVELOPEToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ENVELOPESIZEToolStripMenuItem1, Me.ENVELOPEADDRESSToolStripMenuItem1})
        Me.ENVELOPEToolStripMenuItem.Name = "ENVELOPEToolStripMenuItem"
        Me.ENVELOPEToolStripMenuItem.Size = New System.Drawing.Size(75, 20)
        Me.ENVELOPEToolStripMenuItem.Text = "ENVELOPE"
        Me.ENVELOPEToolStripMenuItem.Visible = False
        '
        'ENVELOPESIZEToolStripMenuItem1
        '
        Me.ENVELOPESIZEToolStripMenuItem1.Name = "ENVELOPESIZEToolStripMenuItem1"
        Me.ENVELOPESIZEToolStripMenuItem1.Size = New System.Drawing.Size(182, 22)
        Me.ENVELOPESIZEToolStripMenuItem1.Text = "ENVELOPE ADDRESS"
        '
        'ENVELOPEADDRESSToolStripMenuItem1
        '
        Me.ENVELOPEADDRESSToolStripMenuItem1.Name = "ENVELOPEADDRESSToolStripMenuItem1"
        Me.ENVELOPEADDRESSToolStripMenuItem1.Size = New System.Drawing.Size(182, 22)
        Me.ENVELOPEADDRESSToolStripMenuItem1.Text = "ENVELOPE SIZE"
        '
        'BANKREGISTRATIONToolStripMenuItem
        '
        Me.BANKREGISTRATIONToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NEWBANKREGISTRATIONALIGNMENTToolStripMenuItem, Me.ENVELOPEToolStripMenuItem1})
        Me.BANKREGISTRATIONToolStripMenuItem.Name = "BANKREGISTRATIONToolStripMenuItem"
        Me.BANKREGISTRATIONToolStripMenuItem.Size = New System.Drawing.Size(71, 20)
        Me.BANKREGISTRATIONToolStripMenuItem.Text = "SETTINGS"
        '
        'NEWBANKREGISTRATIONALIGNMENTToolStripMenuItem
        '
        Me.NEWBANKREGISTRATIONALIGNMENTToolStripMenuItem.Name = "NEWBANKREGISTRATIONALIGNMENTToolStripMenuItem"
        Me.NEWBANKREGISTRATIONALIGNMENTToolStripMenuItem.Size = New System.Drawing.Size(300, 22)
        Me.NEWBANKREGISTRATIONALIGNMENTToolStripMenuItem.Text = "NEW BANK REGISTRATION && ALIGNMENT"
        '
        'ENVELOPEToolStripMenuItem1
        '
        Me.ENVELOPEToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ENVELOPEADDRESSToolStripMenuItem, Me.ENVELOPESIZEToolStripMenuItem})
        Me.ENVELOPEToolStripMenuItem1.Name = "ENVELOPEToolStripMenuItem1"
        Me.ENVELOPEToolStripMenuItem1.Size = New System.Drawing.Size(300, 22)
        Me.ENVELOPEToolStripMenuItem1.Text = "ENVELOPE"
        '
        'ENVELOPEADDRESSToolStripMenuItem
        '
        Me.ENVELOPEADDRESSToolStripMenuItem.Name = "ENVELOPEADDRESSToolStripMenuItem"
        Me.ENVELOPEADDRESSToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.ENVELOPEADDRESSToolStripMenuItem.Text = "ENVELOPE ADDRESS"
        '
        'ENVELOPESIZEToolStripMenuItem
        '
        Me.ENVELOPESIZEToolStripMenuItem.Name = "ENVELOPESIZEToolStripMenuItem"
        Me.ENVELOPESIZEToolStripMenuItem.Size = New System.Drawing.Size(182, 22)
        Me.ENVELOPESIZEToolStripMenuItem.Text = "ENVELOPE SIZE"
        '
        'PrintToolStripMenuItem
        '
        Me.PrintToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PRINTCHEQUEToolStripMenuItem, Me.PRINTENVELOPEToolStripMenuItem, Me.PRINTPAYMENTVOUCHERToolStripMenuItem})
        Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
        Me.PrintToolStripMenuItem.Size = New System.Drawing.Size(52, 20)
        Me.PrintToolStripMenuItem.Text = "PRINT"
        '
        'PRINTCHEQUEToolStripMenuItem
        '
        Me.PRINTCHEQUEToolStripMenuItem.Name = "PRINTCHEQUEToolStripMenuItem"
        Me.PRINTCHEQUEToolStripMenuItem.Size = New System.Drawing.Size(222, 22)
        Me.PRINTCHEQUEToolStripMenuItem.Text = "PRINT CHEQUE"
        '
        'PRINTENVELOPEToolStripMenuItem
        '
        Me.PRINTENVELOPEToolStripMenuItem.Name = "PRINTENVELOPEToolStripMenuItem"
        Me.PRINTENVELOPEToolStripMenuItem.Size = New System.Drawing.Size(222, 22)
        Me.PRINTENVELOPEToolStripMenuItem.Text = "PRINT ENVELOPE"
        '
        'REPORTToolStripMenuItem
        '
        Me.REPORTToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.COMPANYWISECHEQUEISSUEREPORTToolStripMenuItem})
        Me.REPORTToolStripMenuItem.Name = "REPORTToolStripMenuItem"
        Me.REPORTToolStripMenuItem.Size = New System.Drawing.Size(62, 20)
        Me.REPORTToolStripMenuItem.Text = "REPORT"
        '
        'COMPANYWISECHEQUEISSUEREPORTToolStripMenuItem
        '
        Me.COMPANYWISECHEQUEISSUEREPORTToolStripMenuItem.Name = "COMPANYWISECHEQUEISSUEREPORTToolStripMenuItem"
        Me.COMPANYWISECHEQUEISSUEREPORTToolStripMenuItem.Size = New System.Drawing.Size(198, 22)
        Me.COMPANYWISECHEQUEISSUEREPORTToolStripMenuItem.Text = "CHEQUE ISSUE REPORT"
        '
        'LogOutToolStripMenuItem
        '
        Me.LogOutToolStripMenuItem.Name = "LogOutToolStripMenuItem"
        Me.LogOutToolStripMenuItem.Size = New System.Drawing.Size(69, 20)
        Me.LogOutToolStripMenuItem.Text = "LOG OUT"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BackupToolStripMenuItem, Me.RestoreToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(56, 20)
        Me.ToolsToolStripMenuItem.Text = "TOOLS"
        Me.ToolsToolStripMenuItem.Visible = False
        '
        'BackupToolStripMenuItem
        '
        Me.BackupToolStripMenuItem.Name = "BackupToolStripMenuItem"
        Me.BackupToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.BackupToolStripMenuItem.Text = "BACKUP"
        '
        'RestoreToolStripMenuItem
        '
        Me.RestoreToolStripMenuItem.Name = "RestoreToolStripMenuItem"
        Me.RestoreToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.RestoreToolStripMenuItem.Text = "RESTORE"
        '
        'EXITToolStripMenuItem
        '
        Me.EXITToolStripMenuItem.Name = "EXITToolStripMenuItem"
        Me.EXITToolStripMenuItem.Size = New System.Drawing.Size(42, 20)
        Me.EXITToolStripMenuItem.Text = "EXIT"
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Filter = "Backup File|*.jbk"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.Filter = "Backup File|*.jbk"
        '
        'Timer1
        '
        '
        'lbWecomeMsg
        '
        Me.lbWecomeMsg.AutoSize = True
        Me.lbWecomeMsg.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lbWecomeMsg.Font = New System.Drawing.Font("Times New Roman", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbWecomeMsg.ForeColor = System.Drawing.Color.Maroon
        Me.lbWecomeMsg.Location = New System.Drawing.Point(592, 311)
        Me.lbWecomeMsg.Name = "lbWecomeMsg"
        Me.lbWecomeMsg.Size = New System.Drawing.Size(0, 40)
        Me.lbWecomeMsg.TabIndex = 2
        Me.lbWecomeMsg.Visible = False
        '
        'PRINTPAYMENTVOUCHERToolStripMenuItem
        '
        Me.PRINTPAYMENTVOUCHERToolStripMenuItem.Name = "PRINTPAYMENTVOUCHERToolStripMenuItem"
        Me.PRINTPAYMENTVOUCHERToolStripMenuItem.Size = New System.Drawing.Size(222, 22)
        Me.PRINTPAYMENTVOUCHERToolStripMenuItem.Text = "PRINT PAYMENT VOUCHER"
        '
        'frmMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1184, 662)
        Me.Controls.Add(Me.lbWecomeMsg)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frmMaster"
        Me.Text = "ChequeWritePro V1.0"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents MasterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PAYEEREGISTRATIONToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BANKREGISTRATIONToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PRINTCHEQUEToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PRINTENVELOPEToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents REPORTToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents COMPANYWISECHEQUEISSUEREPORTToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents lbWecomeMsg As System.Windows.Forms.Label
    Friend WithEvents EXITToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NEWBANKREGISTRATIONALIGNMENTToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ENVELOPEToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ENVELOPESIZEToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ENVELOPEADDRESSToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BackupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RestoreToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LogOutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ENVELOPEToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ENVELOPEADDRESSToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ENVELOPESIZEToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PRINTPAYMENTVOUCHERToolStripMenuItem As ToolStripMenuItem
End Class
