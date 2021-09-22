<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVoucherPrint
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
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.BtnPrint = New System.Windows.Forms.Button()
        Me.txtFromdate = New AgControls.AgTextBox()
        Me.txtVoucherType = New AgControls.AgTextBox()
        Me.Txtvrno = New AgControls.AgTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtToDate = New AgControls.AgTextBox()
        Me.SuspendLayout()
        '
        'BtnCancel
        '
        Me.BtnCancel.BackColor = System.Drawing.Color.Silver
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancel.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnCancel.Location = New System.Drawing.Point(318, 236)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(88, 26)
        Me.BtnCancel.TabIndex = 5
        Me.BtnCancel.Text = "Clos&e"
        Me.BtnCancel.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(4, 221)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(401, 9)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        '
        'BtnPrint
        '
        Me.BtnPrint.BackColor = System.Drawing.Color.LightGray
        Me.BtnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPrint.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnPrint.Location = New System.Drawing.Point(226, 236)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(88, 26)
        Me.BtnPrint.TabIndex = 4
        Me.BtnPrint.Text = "Print"
        Me.BtnPrint.UseVisualStyleBackColor = False
        '
        'txtFromdate
        '
        Me.txtFromdate.AgAllowUserToEnableMasterHelp = False
        Me.txtFromdate.AgLastValueTag = Nothing
        Me.txtFromdate.AgLastValueText = Nothing
        Me.txtFromdate.AgMandatory = False
        Me.txtFromdate.AgMasterHelp = False
        Me.txtFromdate.AgNumberLeftPlaces = 0
        Me.txtFromdate.AgNumberNegetiveAllow = False
        Me.txtFromdate.AgNumberRightPlaces = 0
        Me.txtFromdate.AgPickFromLastValue = False
        Me.txtFromdate.AgRowFilter = ""
        Me.txtFromdate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.txtFromdate.AgSelectedValue = Nothing
        Me.txtFromdate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.txtFromdate.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.txtFromdate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFromdate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromdate.Location = New System.Drawing.Point(115, 84)
        Me.txtFromdate.Name = "txtFromdate"
        Me.txtFromdate.Size = New System.Drawing.Size(101, 18)
        Me.txtFromdate.TabIndex = 0
        '
        'txtVoucherType
        '
        Me.txtVoucherType.AgAllowUserToEnableMasterHelp = False
        Me.txtVoucherType.AgLastValueTag = Nothing
        Me.txtVoucherType.AgLastValueText = Nothing
        Me.txtVoucherType.AgMandatory = False
        Me.txtVoucherType.AgMasterHelp = False
        Me.txtVoucherType.AgNumberLeftPlaces = 0
        Me.txtVoucherType.AgNumberNegetiveAllow = False
        Me.txtVoucherType.AgNumberRightPlaces = 0
        Me.txtVoucherType.AgPickFromLastValue = False
        Me.txtVoucherType.AgRowFilter = ""
        Me.txtVoucherType.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.txtVoucherType.AgSelectedValue = Nothing
        Me.txtVoucherType.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.txtVoucherType.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.txtVoucherType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtVoucherType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVoucherType.Location = New System.Drawing.Point(115, 105)
        Me.txtVoucherType.Name = "txtVoucherType"
        Me.txtVoucherType.Size = New System.Drawing.Size(272, 18)
        Me.txtVoucherType.TabIndex = 2
        '
        'Txtvrno
        '
        Me.Txtvrno.AgAllowUserToEnableMasterHelp = False
        Me.Txtvrno.AgLastValueTag = Nothing
        Me.Txtvrno.AgLastValueText = Nothing
        Me.Txtvrno.AgMandatory = False
        Me.Txtvrno.AgMasterHelp = False
        Me.Txtvrno.AgNumberLeftPlaces = 0
        Me.Txtvrno.AgNumberNegetiveAllow = False
        Me.Txtvrno.AgNumberRightPlaces = 0
        Me.Txtvrno.AgPickFromLastValue = False
        Me.Txtvrno.AgRowFilter = ""
        Me.Txtvrno.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.Txtvrno.AgSelectedValue = Nothing
        Me.Txtvrno.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.Txtvrno.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.Txtvrno.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Txtvrno.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Txtvrno.Location = New System.Drawing.Point(115, 126)
        Me.Txtvrno.Name = "Txtvrno"
        Me.Txtvrno.Size = New System.Drawing.Size(272, 18)
        Me.Txtvrno.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(21, 84)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 16)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "From Date"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 105)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 16)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Voucher Type"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(21, 126)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 16)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Vr.No"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(226, 83)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(52, 16)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "To Date"
        '
        'txtToDate
        '
        Me.txtToDate.AgAllowUserToEnableMasterHelp = False
        Me.txtToDate.AgLastValueTag = Nothing
        Me.txtToDate.AgLastValueText = Nothing
        Me.txtToDate.AgMandatory = False
        Me.txtToDate.AgMasterHelp = False
        Me.txtToDate.AgNumberLeftPlaces = 0
        Me.txtToDate.AgNumberNegetiveAllow = False
        Me.txtToDate.AgNumberRightPlaces = 0
        Me.txtToDate.AgPickFromLastValue = False
        Me.txtToDate.AgRowFilter = ""
        Me.txtToDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.txtToDate.AgSelectedValue = Nothing
        Me.txtToDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.txtToDate.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.txtToDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtToDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToDate.Location = New System.Drawing.Point(285, 84)
        Me.txtToDate.Name = "txtToDate"
        Me.txtToDate.Size = New System.Drawing.Size(101, 18)
        Me.txtToDate.TabIndex = 1
        '
        'FrmVoucherPrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(410, 269)
        Me.Controls.Add(Me.txtToDate)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Txtvrno)
        Me.Controls.Add(Me.txtVoucherType)
        Me.Controls.Add(Me.txtFromdate)
        Me.Controls.Add(Me.BtnPrint)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmVoucherPrint"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Print"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnPrint As System.Windows.Forms.Button
    Friend WithEvents txtFromdate As AgControls.AgTextBox
    Friend WithEvents txtVoucherType As AgControls.AgTextBox
    Friend WithEvents Txtvrno As AgControls.AgTextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtToDate As AgControls.AgTextBox
End Class
