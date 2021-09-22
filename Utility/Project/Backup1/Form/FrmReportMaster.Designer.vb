<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReportMaster
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
        Me.TxtReportName = New AgControls.AgTextBox
        Me.TxtProcedure = New AgControls.AgTextBox
        Me.TxtSubProcedure = New AgControls.AgTextBox
        Me.BtnPrint = New System.Windows.Forms.Button
        Me.BtnGridView = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'TxtReportName
        '
        Me.TxtReportName.AgMandatory = False
        Me.TxtReportName.AgMasterHelp = False
        Me.TxtReportName.AgNumberLeftPlaces = 0
        Me.TxtReportName.AgNumberNegetiveAllow = False
        Me.TxtReportName.AgNumberRightPlaces = 0
        Me.TxtReportName.AgPickFromLastValue = False
        Me.TxtReportName.AgRowFilter = ""
        Me.TxtReportName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtReportName.AgSelectedValue = Nothing
        Me.TxtReportName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtReportName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtReportName.Location = New System.Drawing.Point(128, 22)
        Me.TxtReportName.Name = "TxtReportName"
        Me.TxtReportName.Size = New System.Drawing.Size(191, 21)
        Me.TxtReportName.TabIndex = 0
        '
        'TxtProcedure
        '
        Me.TxtProcedure.AgMandatory = False
        Me.TxtProcedure.AgMasterHelp = False
        Me.TxtProcedure.AgNumberLeftPlaces = 0
        Me.TxtProcedure.AgNumberNegetiveAllow = False
        Me.TxtProcedure.AgNumberRightPlaces = 0
        Me.TxtProcedure.AgPickFromLastValue = False
        Me.TxtProcedure.AgRowFilter = ""
        Me.TxtProcedure.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtProcedure.AgSelectedValue = Nothing
        Me.TxtProcedure.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtProcedure.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtProcedure.Location = New System.Drawing.Point(128, 49)
        Me.TxtProcedure.Name = "TxtProcedure"
        Me.TxtProcedure.Size = New System.Drawing.Size(191, 21)
        Me.TxtProcedure.TabIndex = 1
        '
        'TxtSubProcedure
        '
        Me.TxtSubProcedure.AgMandatory = False
        Me.TxtSubProcedure.AgMasterHelp = False
        Me.TxtSubProcedure.AgNumberLeftPlaces = 0
        Me.TxtSubProcedure.AgNumberNegetiveAllow = False
        Me.TxtSubProcedure.AgNumberRightPlaces = 0
        Me.TxtSubProcedure.AgPickFromLastValue = False
        Me.TxtSubProcedure.AgRowFilter = ""
        Me.TxtSubProcedure.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSubProcedure.AgSelectedValue = Nothing
        Me.TxtSubProcedure.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSubProcedure.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSubProcedure.Location = New System.Drawing.Point(128, 76)
        Me.TxtSubProcedure.Name = "TxtSubProcedure"
        Me.TxtSubProcedure.Size = New System.Drawing.Size(191, 21)
        Me.TxtSubProcedure.TabIndex = 2
        '
        'BtnPrint
        '
        Me.BtnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPrint.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPrint.Location = New System.Drawing.Point(96, 119)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(75, 24)
        Me.BtnPrint.TabIndex = 3
        Me.BtnPrint.Text = "Print"
        Me.BtnPrint.UseVisualStyleBackColor = True
        '
        'BtnGridView
        '
        Me.BtnGridView.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnGridView.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnGridView.Location = New System.Drawing.Point(177, 119)
        Me.BtnGridView.Name = "BtnGridView"
        Me.BtnGridView.Size = New System.Drawing.Size(75, 24)
        Me.BtnGridView.TabIndex = 4
        Me.BtnGridView.Text = "Grid View"
        Me.BtnGridView.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(31, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Report Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(31, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Procedure"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(31, 81)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Sub Procedure"
        '
        'FrmReportMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(341, 169)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BtnGridView)
        Me.Controls.Add(Me.BtnPrint)
        Me.Controls.Add(Me.TxtSubProcedure)
        Me.Controls.Add(Me.TxtProcedure)
        Me.Controls.Add(Me.TxtReportName)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "FrmReportMaster"
        Me.Text = "FrmReportMaster"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TxtReportName As AgControls.AgTextBox
    Friend WithEvents TxtProcedure As AgControls.AgTextBox
    Friend WithEvents TxtSubProcedure As AgControls.AgTextBox
    Friend WithEvents BtnPrint As System.Windows.Forms.Button
    Friend WithEvents BtnGridView As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
