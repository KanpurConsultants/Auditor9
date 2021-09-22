<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReportFormat
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
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.BtnOk = New System.Windows.Forms.Button
        Me.BtnCancel = New System.Windows.Forms.Button
        Me.TCMain = New System.Windows.Forms.TabControl
        Me.TPDisplaySet = New System.Windows.Forms.TabPage
        Me.BtnRowDownDisplay = New System.Windows.Forms.Button
        Me.BtnRowUpDisplay = New System.Windows.Forms.Button
        Me.TPSortSet = New System.Windows.Forms.TabPage
        Me.BtnRowDownSort = New System.Windows.Forms.Button
        Me.BtnRowUpSort = New System.Windows.Forms.Button
        Me.Pnl2 = New System.Windows.Forms.Panel
        Me.TPGroupSet = New System.Windows.Forms.TabPage
        Me.BtnRowDownGroup = New System.Windows.Forms.Button
        Me.BtnRowUpGroup = New System.Windows.Forms.Button
        Me.Pnl3 = New System.Windows.Forms.Panel
        Me.TpFilterSetting = New System.Windows.Forms.TabPage
        Me.Pnl4 = New System.Windows.Forms.Panel
        Me.BtnSaveDisplaySettings = New System.Windows.Forms.Button
        Me.BtnSaveSortSetting = New System.Windows.Forms.Button
        Me.BtnSaveFilterSettings = New System.Windows.Forms.Button
        Me.TCMain.SuspendLayout()
        Me.TPDisplaySet.SuspendLayout()
        Me.TPSortSet.SuspendLayout()
        Me.TPGroupSet.SuspendLayout()
        Me.TpFilterSetting.SuspendLayout()
        Me.SuspendLayout()
        '
        'Pnl1
        '
        Me.Pnl1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl1.Location = New System.Drawing.Point(3, 3)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(372, 354)
        Me.Pnl1.TabIndex = 1
        '
        'BtnOk
        '
        Me.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOk.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOk.Location = New System.Drawing.Point(457, 397)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(61, 23)
        Me.BtnOk.TabIndex = 3
        Me.BtnOk.Text = "OK"
        Me.BtnOk.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancel.Location = New System.Drawing.Point(524, 397)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(61, 23)
        Me.BtnCancel.TabIndex = 4
        Me.BtnCancel.Text = "Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'TCMain
        '
        Me.TCMain.Appearance = System.Windows.Forms.TabAppearance.Buttons
        Me.TCMain.Controls.Add(Me.TPDisplaySet)
        Me.TCMain.Controls.Add(Me.TPSortSet)
        Me.TCMain.Controls.Add(Me.TPGroupSet)
        Me.TCMain.Controls.Add(Me.TpFilterSetting)
        Me.TCMain.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TCMain.Location = New System.Drawing.Point(2, 1)
        Me.TCMain.Name = "TCMain"
        Me.TCMain.SelectedIndex = 0
        Me.TCMain.Size = New System.Drawing.Size(589, 389)
        Me.TCMain.TabIndex = 5
        '
        'TPDisplaySet
        '
        Me.TPDisplaySet.Controls.Add(Me.BtnSaveDisplaySettings)
        Me.TPDisplaySet.Controls.Add(Me.BtnRowDownDisplay)
        Me.TPDisplaySet.Controls.Add(Me.BtnRowUpDisplay)
        Me.TPDisplaySet.Controls.Add(Me.Pnl1)
        Me.TPDisplaySet.Location = New System.Drawing.Point(4, 25)
        Me.TPDisplaySet.Name = "TPDisplaySet"
        Me.TPDisplaySet.Padding = New System.Windows.Forms.Padding(3)
        Me.TPDisplaySet.Size = New System.Drawing.Size(581, 360)
        Me.TPDisplaySet.TabIndex = 0
        Me.TPDisplaySet.Text = "Display Setting"
        Me.TPDisplaySet.UseVisualStyleBackColor = True
        '
        'BtnRowDownDisplay
        '
        Me.BtnRowDownDisplay.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnRowDownDisplay.Location = New System.Drawing.Point(477, 32)
        Me.BtnRowDownDisplay.Name = "BtnRowDownDisplay"
        Me.BtnRowDownDisplay.Size = New System.Drawing.Size(101, 23)
        Me.BtnRowDownDisplay.TabIndex = 7
        Me.BtnRowDownDisplay.Text = "Move Down"
        Me.BtnRowDownDisplay.UseVisualStyleBackColor = True
        '
        'BtnRowUpDisplay
        '
        Me.BtnRowUpDisplay.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnRowUpDisplay.Location = New System.Drawing.Point(477, 6)
        Me.BtnRowUpDisplay.Name = "BtnRowUpDisplay"
        Me.BtnRowUpDisplay.Size = New System.Drawing.Size(101, 23)
        Me.BtnRowUpDisplay.TabIndex = 6
        Me.BtnRowUpDisplay.Text = "Move Up"
        Me.BtnRowUpDisplay.UseVisualStyleBackColor = True
        '
        'TPSortSet
        '
        Me.TPSortSet.Controls.Add(Me.BtnSaveSortSetting)
        Me.TPSortSet.Controls.Add(Me.BtnRowDownSort)
        Me.TPSortSet.Controls.Add(Me.BtnRowUpSort)
        Me.TPSortSet.Controls.Add(Me.Pnl2)
        Me.TPSortSet.Location = New System.Drawing.Point(4, 25)
        Me.TPSortSet.Name = "TPSortSet"
        Me.TPSortSet.Padding = New System.Windows.Forms.Padding(3)
        Me.TPSortSet.Size = New System.Drawing.Size(581, 360)
        Me.TPSortSet.TabIndex = 1
        Me.TPSortSet.Text = "Sort Setting"
        Me.TPSortSet.UseVisualStyleBackColor = True
        '
        'BtnRowDownSort
        '
        Me.BtnRowDownSort.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnRowDownSort.Location = New System.Drawing.Point(488, 28)
        Me.BtnRowDownSort.Name = "BtnRowDownSort"
        Me.BtnRowDownSort.Size = New System.Drawing.Size(90, 23)
        Me.BtnRowDownSort.TabIndex = 7
        Me.BtnRowDownSort.Text = "Move Down"
        Me.BtnRowDownSort.UseVisualStyleBackColor = True
        '
        'BtnRowUpSort
        '
        Me.BtnRowUpSort.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnRowUpSort.Location = New System.Drawing.Point(488, 3)
        Me.BtnRowUpSort.Name = "BtnRowUpSort"
        Me.BtnRowUpSort.Size = New System.Drawing.Size(90, 23)
        Me.BtnRowUpSort.TabIndex = 6
        Me.BtnRowUpSort.Text = "Move Up"
        Me.BtnRowUpSort.UseVisualStyleBackColor = True
        '
        'Pnl2
        '
        Me.Pnl2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl2.Location = New System.Drawing.Point(2, 3)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(400, 353)
        Me.Pnl2.TabIndex = 2
        '
        'TPGroupSet
        '
        Me.TPGroupSet.Controls.Add(Me.BtnRowDownGroup)
        Me.TPGroupSet.Controls.Add(Me.BtnRowUpGroup)
        Me.TPGroupSet.Controls.Add(Me.Pnl3)
        Me.TPGroupSet.Location = New System.Drawing.Point(4, 25)
        Me.TPGroupSet.Name = "TPGroupSet"
        Me.TPGroupSet.Size = New System.Drawing.Size(581, 360)
        Me.TPGroupSet.TabIndex = 2
        Me.TPGroupSet.Text = "Group Setting"
        Me.TPGroupSet.UseVisualStyleBackColor = True
        '
        'BtnRowDownGroup
        '
        Me.BtnRowDownGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnRowDownGroup.Location = New System.Drawing.Point(407, 47)
        Me.BtnRowDownGroup.Name = "BtnRowDownGroup"
        Me.BtnRowDownGroup.Size = New System.Drawing.Size(31, 40)
        Me.BtnRowDownGroup.TabIndex = 7
        Me.BtnRowDownGroup.Text = "Down"
        Me.BtnRowDownGroup.UseVisualStyleBackColor = True
        '
        'BtnRowUpGroup
        '
        Me.BtnRowUpGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnRowUpGroup.Location = New System.Drawing.Point(407, 3)
        Me.BtnRowUpGroup.Name = "BtnRowUpGroup"
        Me.BtnRowUpGroup.Size = New System.Drawing.Size(31, 40)
        Me.BtnRowUpGroup.TabIndex = 6
        Me.BtnRowUpGroup.Text = "UP"
        Me.BtnRowUpGroup.UseVisualStyleBackColor = True
        '
        'Pnl3
        '
        Me.Pnl3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl3.Location = New System.Drawing.Point(1, 3)
        Me.Pnl3.Name = "Pnl3"
        Me.Pnl3.Size = New System.Drawing.Size(400, 357)
        Me.Pnl3.TabIndex = 2
        '
        'TpFilterSetting
        '
        Me.TpFilterSetting.Controls.Add(Me.BtnSaveFilterSettings)
        Me.TpFilterSetting.Controls.Add(Me.Pnl4)
        Me.TpFilterSetting.Location = New System.Drawing.Point(4, 25)
        Me.TpFilterSetting.Name = "TpFilterSetting"
        Me.TpFilterSetting.Padding = New System.Windows.Forms.Padding(3)
        Me.TpFilterSetting.Size = New System.Drawing.Size(581, 360)
        Me.TpFilterSetting.TabIndex = 3
        Me.TpFilterSetting.Text = "Filter Setting"
        Me.TpFilterSetting.UseVisualStyleBackColor = True
        '
        'Pnl4
        '
        Me.Pnl4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl4.Location = New System.Drawing.Point(6, 3)
        Me.Pnl4.Name = "Pnl4"
        Me.Pnl4.Size = New System.Drawing.Size(506, 358)
        Me.Pnl4.TabIndex = 3
        '
        'BtnSaveDisplaySettings
        '
        Me.BtnSaveDisplaySettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSaveDisplaySettings.Location = New System.Drawing.Point(477, 59)
        Me.BtnSaveDisplaySettings.Name = "BtnSaveDisplaySettings"
        Me.BtnSaveDisplaySettings.Size = New System.Drawing.Size(101, 23)
        Me.BtnSaveDisplaySettings.TabIndex = 8
        Me.BtnSaveDisplaySettings.Text = "Save"
        Me.BtnSaveDisplaySettings.UseVisualStyleBackColor = True
        '
        'BtnSaveSortSetting
        '
        Me.BtnSaveSortSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSaveSortSetting.Location = New System.Drawing.Point(488, 55)
        Me.BtnSaveSortSetting.Name = "BtnSaveSortSetting"
        Me.BtnSaveSortSetting.Size = New System.Drawing.Size(90, 23)
        Me.BtnSaveSortSetting.TabIndex = 8
        Me.BtnSaveSortSetting.Text = "Save"
        Me.BtnSaveSortSetting.UseVisualStyleBackColor = True
        '
        'BtnSaveFilterSettings
        '
        Me.BtnSaveFilterSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSaveFilterSettings.Location = New System.Drawing.Point(518, 3)
        Me.BtnSaveFilterSettings.Name = "BtnSaveFilterSettings"
        Me.BtnSaveFilterSettings.Size = New System.Drawing.Size(57, 23)
        Me.BtnSaveFilterSettings.TabIndex = 9
        Me.BtnSaveFilterSettings.Text = "Save"
        Me.BtnSaveFilterSettings.UseVisualStyleBackColor = True
        '
        'FrmReportFormat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(590, 423)
        Me.Controls.Add(Me.TCMain)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOk)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmReportFormat"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Report Format"
        Me.TCMain.ResumeLayout(False)
        Me.TPDisplaySet.ResumeLayout(False)
        Me.TPSortSet.ResumeLayout(False)
        Me.TPGroupSet.ResumeLayout(False)
        Me.TpFilterSetting.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pnl1 As System.Windows.Forms.Panel
    Friend WithEvents BtnOk As System.Windows.Forms.Button
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents TCMain As System.Windows.Forms.TabControl
    Friend WithEvents TPDisplaySet As System.Windows.Forms.TabPage
    Friend WithEvents TPSortSet As System.Windows.Forms.TabPage
    Friend WithEvents TPGroupSet As System.Windows.Forms.TabPage
    Friend WithEvents Pnl2 As System.Windows.Forms.Panel
    Friend WithEvents Pnl3 As System.Windows.Forms.Panel
    Friend WithEvents BtnRowDownDisplay As System.Windows.Forms.Button
    Friend WithEvents BtnRowUpDisplay As System.Windows.Forms.Button
    Friend WithEvents BtnRowDownSort As System.Windows.Forms.Button
    Friend WithEvents BtnRowUpSort As System.Windows.Forms.Button
    Friend WithEvents BtnRowDownGroup As System.Windows.Forms.Button
    Friend WithEvents BtnRowUpGroup As System.Windows.Forms.Button
    Friend WithEvents TpFilterSetting As System.Windows.Forms.TabPage
    Friend WithEvents Pnl4 As System.Windows.Forms.Panel
    Friend WithEvents BtnSaveDisplaySettings As System.Windows.Forms.Button
    Friend WithEvents BtnSaveSortSetting As System.Windows.Forms.Button
    Friend WithEvents BtnSaveFilterSettings As System.Windows.Forms.Button
End Class
