<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSaleInvoiceMapping
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
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.TxtPakkaSaleInvoiceNo = New AgControls.AgTextBox()
        Me.LblBarcode = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtKachhaSaleInvoiceNo = New AgControls.AgTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtKachhaSaleInvoiceDate = New AgControls.AgTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtLrNo = New AgControls.AgTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtLrDate = New AgControls.AgTextBox()
        Me.TxtCode = New AgControls.AgTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtNoOfBales = New AgControls.AgTextBox()
        Me.SuspendLayout()
        '
        'BtnSave
        '
        Me.BtnSave.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSave.Location = New System.Drawing.Point(425, 216)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(75, 23)
        Me.BtnSave.TabIndex = 6
        Me.BtnSave.Text = "Save"
        Me.BtnSave.UseVisualStyleBackColor = True
        '
        'TxtPakkaSaleInvoiceNo
        '
        Me.TxtPakkaSaleInvoiceNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtPakkaSaleInvoiceNo.AgLastValueTag = Nothing
        Me.TxtPakkaSaleInvoiceNo.AgLastValueText = Nothing
        Me.TxtPakkaSaleInvoiceNo.AgMandatory = False
        Me.TxtPakkaSaleInvoiceNo.AgMasterHelp = False
        Me.TxtPakkaSaleInvoiceNo.AgNumberLeftPlaces = 8
        Me.TxtPakkaSaleInvoiceNo.AgNumberNegetiveAllow = False
        Me.TxtPakkaSaleInvoiceNo.AgNumberRightPlaces = 2
        Me.TxtPakkaSaleInvoiceNo.AgPickFromLastValue = False
        Me.TxtPakkaSaleInvoiceNo.AgRowFilter = ""
        Me.TxtPakkaSaleInvoiceNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPakkaSaleInvoiceNo.AgSelectedValue = Nothing
        Me.TxtPakkaSaleInvoiceNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPakkaSaleInvoiceNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPakkaSaleInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPakkaSaleInvoiceNo.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPakkaSaleInvoiceNo.Location = New System.Drawing.Point(196, 19)
        Me.TxtPakkaSaleInvoiceNo.MaxLength = 20
        Me.TxtPakkaSaleInvoiceNo.Name = "TxtPakkaSaleInvoiceNo"
        Me.TxtPakkaSaleInvoiceNo.Size = New System.Drawing.Size(304, 19)
        Me.TxtPakkaSaleInvoiceNo.TabIndex = 0
        '
        'LblBarcode
        '
        Me.LblBarcode.AutoSize = True
        Me.LblBarcode.BackColor = System.Drawing.Color.Transparent
        Me.LblBarcode.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBarcode.Location = New System.Drawing.Point(12, 22)
        Me.LblBarcode.Name = "LblBarcode"
        Me.LblBarcode.Size = New System.Drawing.Size(156, 14)
        Me.LblBarcode.TabIndex = 3005
        Me.LblBarcode.Text = "Pakka Sale Invoice No"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(163, 14)
        Me.Label1.TabIndex = 3007
        Me.Label1.Text = "Kachha Sale Invoice No"
        '
        'TxtKachhaSaleInvoiceNo
        '
        Me.TxtKachhaSaleInvoiceNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtKachhaSaleInvoiceNo.AgLastValueTag = Nothing
        Me.TxtKachhaSaleInvoiceNo.AgLastValueText = Nothing
        Me.TxtKachhaSaleInvoiceNo.AgMandatory = False
        Me.TxtKachhaSaleInvoiceNo.AgMasterHelp = False
        Me.TxtKachhaSaleInvoiceNo.AgNumberLeftPlaces = 8
        Me.TxtKachhaSaleInvoiceNo.AgNumberNegetiveAllow = False
        Me.TxtKachhaSaleInvoiceNo.AgNumberRightPlaces = 2
        Me.TxtKachhaSaleInvoiceNo.AgPickFromLastValue = False
        Me.TxtKachhaSaleInvoiceNo.AgRowFilter = ""
        Me.TxtKachhaSaleInvoiceNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtKachhaSaleInvoiceNo.AgSelectedValue = Nothing
        Me.TxtKachhaSaleInvoiceNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtKachhaSaleInvoiceNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtKachhaSaleInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtKachhaSaleInvoiceNo.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtKachhaSaleInvoiceNo.Location = New System.Drawing.Point(196, 41)
        Me.TxtKachhaSaleInvoiceNo.MaxLength = 20
        Me.TxtKachhaSaleInvoiceNo.Name = "TxtKachhaSaleInvoiceNo"
        Me.TxtKachhaSaleInvoiceNo.Size = New System.Drawing.Size(304, 19)
        Me.TxtKachhaSaleInvoiceNo.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 67)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(176, 14)
        Me.Label2.TabIndex = 3009
        Me.Label2.Text = "Kachha Sale Invoice Date"
        '
        'TxtKachhaSaleInvoiceDate
        '
        Me.TxtKachhaSaleInvoiceDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtKachhaSaleInvoiceDate.AgLastValueTag = Nothing
        Me.TxtKachhaSaleInvoiceDate.AgLastValueText = Nothing
        Me.TxtKachhaSaleInvoiceDate.AgMandatory = False
        Me.TxtKachhaSaleInvoiceDate.AgMasterHelp = False
        Me.TxtKachhaSaleInvoiceDate.AgNumberLeftPlaces = 8
        Me.TxtKachhaSaleInvoiceDate.AgNumberNegetiveAllow = False
        Me.TxtKachhaSaleInvoiceDate.AgNumberRightPlaces = 2
        Me.TxtKachhaSaleInvoiceDate.AgPickFromLastValue = False
        Me.TxtKachhaSaleInvoiceDate.AgRowFilter = ""
        Me.TxtKachhaSaleInvoiceDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtKachhaSaleInvoiceDate.AgSelectedValue = Nothing
        Me.TxtKachhaSaleInvoiceDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtKachhaSaleInvoiceDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtKachhaSaleInvoiceDate.BackColor = System.Drawing.Color.White
        Me.TxtKachhaSaleInvoiceDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtKachhaSaleInvoiceDate.Enabled = False
        Me.TxtKachhaSaleInvoiceDate.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtKachhaSaleInvoiceDate.Location = New System.Drawing.Point(196, 63)
        Me.TxtKachhaSaleInvoiceDate.MaxLength = 20
        Me.TxtKachhaSaleInvoiceDate.Name = "TxtKachhaSaleInvoiceDate"
        Me.TxtKachhaSaleInvoiceDate.Size = New System.Drawing.Size(304, 19)
        Me.TxtKachhaSaleInvoiceDate.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 89)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 14)
        Me.Label3.TabIndex = 3011
        Me.Label3.Text = "LR No"
        '
        'TxtLrNo
        '
        Me.TxtLrNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtLrNo.AgLastValueTag = Nothing
        Me.TxtLrNo.AgLastValueText = Nothing
        Me.TxtLrNo.AgMandatory = False
        Me.TxtLrNo.AgMasterHelp = False
        Me.TxtLrNo.AgNumberLeftPlaces = 8
        Me.TxtLrNo.AgNumberNegetiveAllow = False
        Me.TxtLrNo.AgNumberRightPlaces = 2
        Me.TxtLrNo.AgPickFromLastValue = False
        Me.TxtLrNo.AgRowFilter = ""
        Me.TxtLrNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtLrNo.AgSelectedValue = Nothing
        Me.TxtLrNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtLrNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtLrNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtLrNo.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLrNo.Location = New System.Drawing.Point(196, 85)
        Me.TxtLrNo.MaxLength = 20
        Me.TxtLrNo.Name = "TxtLrNo"
        Me.TxtLrNo.Size = New System.Drawing.Size(304, 19)
        Me.TxtLrNo.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 111)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 14)
        Me.Label4.TabIndex = 3013
        Me.Label4.Text = "LR Date"
        '
        'TxtLrDate
        '
        Me.TxtLrDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtLrDate.AgLastValueTag = Nothing
        Me.TxtLrDate.AgLastValueText = Nothing
        Me.TxtLrDate.AgMandatory = False
        Me.TxtLrDate.AgMasterHelp = False
        Me.TxtLrDate.AgNumberLeftPlaces = 8
        Me.TxtLrDate.AgNumberNegetiveAllow = False
        Me.TxtLrDate.AgNumberRightPlaces = 2
        Me.TxtLrDate.AgPickFromLastValue = False
        Me.TxtLrDate.AgRowFilter = ""
        Me.TxtLrDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtLrDate.AgSelectedValue = Nothing
        Me.TxtLrDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtLrDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtLrDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtLrDate.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLrDate.Location = New System.Drawing.Point(196, 107)
        Me.TxtLrDate.MaxLength = 20
        Me.TxtLrDate.Name = "TxtLrDate"
        Me.TxtLrDate.Size = New System.Drawing.Size(304, 19)
        Me.TxtLrDate.TabIndex = 4
        '
        'TxtCode
        '
        Me.TxtCode.AgAllowUserToEnableMasterHelp = False
        Me.TxtCode.AgLastValueTag = Nothing
        Me.TxtCode.AgLastValueText = Nothing
        Me.TxtCode.AgMandatory = False
        Me.TxtCode.AgMasterHelp = False
        Me.TxtCode.AgNumberLeftPlaces = 8
        Me.TxtCode.AgNumberNegetiveAllow = False
        Me.TxtCode.AgNumberRightPlaces = 2
        Me.TxtCode.AgPickFromLastValue = False
        Me.TxtCode.AgRowFilter = ""
        Me.TxtCode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCode.AgSelectedValue = Nothing
        Me.TxtCode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCode.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCode.Location = New System.Drawing.Point(15, 216)
        Me.TxtCode.MaxLength = 20
        Me.TxtCode.Name = "TxtCode"
        Me.TxtCode.Size = New System.Drawing.Size(122, 19)
        Me.TxtCode.TabIndex = 3014
        Me.TxtCode.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 133)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(86, 14)
        Me.Label5.TabIndex = 3016
        Me.Label5.Text = "No. of Bales"
        '
        'TxtNoOfBales
        '
        Me.TxtNoOfBales.AgAllowUserToEnableMasterHelp = False
        Me.TxtNoOfBales.AgLastValueTag = Nothing
        Me.TxtNoOfBales.AgLastValueText = Nothing
        Me.TxtNoOfBales.AgMandatory = False
        Me.TxtNoOfBales.AgMasterHelp = False
        Me.TxtNoOfBales.AgNumberLeftPlaces = 3
        Me.TxtNoOfBales.AgNumberNegetiveAllow = False
        Me.TxtNoOfBales.AgNumberRightPlaces = 0
        Me.TxtNoOfBales.AgPickFromLastValue = False
        Me.TxtNoOfBales.AgRowFilter = ""
        Me.TxtNoOfBales.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtNoOfBales.AgSelectedValue = Nothing
        Me.TxtNoOfBales.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtNoOfBales.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtNoOfBales.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtNoOfBales.Font = New System.Drawing.Font("Verdana", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNoOfBales.Location = New System.Drawing.Point(196, 129)
        Me.TxtNoOfBales.MaxLength = 20
        Me.TxtNoOfBales.Name = "TxtNoOfBales"
        Me.TxtNoOfBales.Size = New System.Drawing.Size(304, 19)
        Me.TxtNoOfBales.TabIndex = 5
        '
        'FrmSaleInvoiceMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(520, 265)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TxtNoOfBales)
        Me.Controls.Add(Me.TxtCode)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtLrDate)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtLrNo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtKachhaSaleInvoiceDate)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtKachhaSaleInvoiceNo)
        Me.Controls.Add(Me.LblBarcode)
        Me.Controls.Add(Me.TxtPakkaSaleInvoiceNo)
        Me.Controls.Add(Me.BtnSave)
        Me.KeyPreview = True
        Me.Name = "FrmSaleInvoiceMapping"
        Me.Text = "FrmSaleInvoiceMapping"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnSave As Button
    Public WithEvents TxtPakkaSaleInvoiceNo As AgControls.AgTextBox
    Public WithEvents LblBarcode As Label
    Public WithEvents Label1 As Label
    Public WithEvents TxtKachhaSaleInvoiceNo As AgControls.AgTextBox
    Public WithEvents Label2 As Label
    Public WithEvents TxtKachhaSaleInvoiceDate As AgControls.AgTextBox
    Public WithEvents Label3 As Label
    Public WithEvents TxtLrNo As AgControls.AgTextBox
    Public WithEvents Label4 As Label
    Public WithEvents TxtLrDate As AgControls.AgTextBox
    Public WithEvents TxtCode As AgControls.AgTextBox
    Public WithEvents Label5 As Label
    Public WithEvents TxtNoOfBales As AgControls.AgTextBox
End Class
