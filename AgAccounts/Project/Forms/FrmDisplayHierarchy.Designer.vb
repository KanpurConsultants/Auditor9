<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDisplayHierarchy
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.PnlMain = New System.Windows.Forms.Panel()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.BtnPrint = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.LblDisplay = New System.Windows.Forms.Label()
        Me.BtnSettings = New System.Windows.Forms.Button()
        Me.BtnBackWard = New System.Windows.Forms.Button()
        Me.LblFilter = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblDisplayDate = New System.Windows.Forms.Label()
        Me.LblDisplaySite = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'PnlMain
        '
        Me.PnlMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlMain.Location = New System.Drawing.Point(12, 62)
        Me.PnlMain.Name = "PnlMain"
        Me.PnlMain.Size = New System.Drawing.Size(960, 502)
        Me.PnlMain.TabIndex = 0
        '
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnClose.Location = New System.Drawing.Point(889, 586)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(84, 26)
        Me.BtnClose.TabIndex = 31
        Me.BtnClose.Text = "Clos&e"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'BtnPrint
        '
        Me.BtnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPrint.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnPrint.Location = New System.Drawing.Point(799, 586)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(84, 26)
        Me.BtnPrint.TabIndex = 30
        Me.BtnPrint.Text = "&Print"
        Me.BtnPrint.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Location = New System.Drawing.Point(5, 572)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(974, 9)
        Me.GroupBox2.TabIndex = 32
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Tag = ""
        '
        'LblDisplay
        '
        Me.LblDisplay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblDisplay.BackColor = System.Drawing.Color.White
        Me.LblDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblDisplay.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.LblDisplay.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDisplay.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblDisplay.Location = New System.Drawing.Point(48, 29)
        Me.LblDisplay.Name = "LblDisplay"
        Me.LblDisplay.Size = New System.Drawing.Size(925, 26)
        Me.LblDisplay.TabIndex = 33
        Me.LblDisplay.Text = "Trial Balance"
        Me.LblDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BtnSettings
        '
        Me.BtnSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSettings.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnSettings.Location = New System.Drawing.Point(709, 586)
        Me.BtnSettings.Name = "BtnSettings"
        Me.BtnSettings.Size = New System.Drawing.Size(84, 26)
        Me.BtnSettings.TabIndex = 34
        Me.BtnSettings.Text = "&Settings"
        Me.BtnSettings.UseVisualStyleBackColor = True
        '
        'BtnBackWard
        '
        Me.BtnBackWard.BackColor = System.Drawing.Color.Transparent
        Me.BtnBackWard.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnBackWard.Font = New System.Drawing.Font("Webdings", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.BtnBackWard.ForeColor = System.Drawing.Color.Blue
        Me.BtnBackWard.Location = New System.Drawing.Point(12, 29)
        Me.BtnBackWard.Name = "BtnBackWard"
        Me.BtnBackWard.Size = New System.Drawing.Size(30, 26)
        Me.BtnBackWard.TabIndex = 31
        Me.BtnBackWard.Text = "3"
        Me.BtnBackWard.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnBackWard.UseVisualStyleBackColor = False
        '
        'LblFilter
        '
        Me.LblFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblFilter.BackColor = System.Drawing.Color.White
        Me.LblFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.LblFilter.Font = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFilter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblFilter.Location = New System.Drawing.Point(12, 586)
        Me.LblFilter.Name = "LblFilter"
        Me.LblFilter.Size = New System.Drawing.Size(691, 26)
        Me.LblFilter.TabIndex = 35
        Me.LblFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(260, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(479, 21)
        Me.Label1.TabIndex = 36
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblDisplayDate
        '
        Me.LblDisplayDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LblDisplayDate.BackColor = System.Drawing.Color.White
        Me.LblDisplayDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblDisplayDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.LblDisplayDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDisplayDate.ForeColor = System.Drawing.Color.Blue
        Me.LblDisplayDate.Location = New System.Drawing.Point(743, 4)
        Me.LblDisplayDate.Name = "LblDisplayDate"
        Me.LblDisplayDate.Size = New System.Drawing.Size(230, 21)
        Me.LblDisplayDate.TabIndex = 37
        Me.LblDisplayDate.Text = "Date"
        Me.LblDisplayDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblDisplaySite
        '
        Me.LblDisplaySite.BackColor = System.Drawing.Color.White
        Me.LblDisplaySite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LblDisplaySite.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.LblDisplaySite.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDisplaySite.ForeColor = System.Drawing.Color.Blue
        Me.LblDisplaySite.Location = New System.Drawing.Point(12, 4)
        Me.LblDisplaySite.Name = "LblDisplaySite"
        Me.LblDisplaySite.Size = New System.Drawing.Size(245, 21)
        Me.LblDisplaySite.TabIndex = 38
        Me.LblDisplaySite.Text = "SiteName"
        Me.LblDisplaySite.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FrmDisplayHierarchy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(982, 616)
        Me.Controls.Add(Me.LblDisplaySite)
        Me.Controls.Add(Me.LblDisplayDate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblFilter)
        Me.Controls.Add(Me.BtnBackWard)
        Me.Controls.Add(Me.BtnSettings)
        Me.Controls.Add(Me.LblDisplay)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.BtnPrint)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.PnlMain)
        Me.Name = "FrmDisplayHierarchy"
        Me.ShowIcon = False
        Me.Text = "Display Hierarchy"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PnlMain As System.Windows.Forms.Panel
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents BtnPrint As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents LblDisplay As System.Windows.Forms.Label
    Friend WithEvents BtnSettings As System.Windows.Forms.Button
    Friend WithEvents BtnBackWard As System.Windows.Forms.Button
    Friend WithEvents LblFilter As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LblDisplayDate As System.Windows.Forms.Label
    Friend WithEvents LblDisplaySite As System.Windows.Forms.Label
End Class
