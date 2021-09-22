<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProcess
    Inherits System.Windows.Forms.Form
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If Disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.          [Ag]
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Topctrl1 = New Topctrl.Topctrl
        Me.TxtManualCode = New AgControls.AgTextBox
        Me.LblManualCode = New System.Windows.Forms.Label
        Me.TxtDescription = New AgControls.AgTextBox
        Me.LblDescription = New System.Windows.Forms.Label
        Me.TxtSubCode = New AgControls.AgTextBox
        Me.LblLedgerAc = New System.Windows.Forms.Label
        Me.LblJobWorkerReq = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtProcessGroup = New AgControls.AgTextBox
        Me.LblProcessGroup = New System.Windows.Forms.Label
        Me.TxtQCGroup = New AgControls.AgTextBox
        Me.LblQCGroup = New System.Windows.Forms.Label
        Me.TxtStructure = New AgControls.AgTextBox
        Me.LblStructure = New System.Windows.Forms.Label
        Me.TxtInsideOutside = New AgControls.AgTextBox
        Me.LblInsideOutside = New System.Windows.Forms.Label
        Me.TxtDefaultJobOrderFor = New AgControls.AgTextBox
        Me.LblDefaultJobOrderFor = New System.Windows.Forms.Label
        Me.TxtDefaultBillingType = New AgControls.AgTextBox
        Me.LblDefaultBillingType = New System.Windows.Forms.Label
        Me.TxtPrevProcess = New AgControls.AgTextBox
        Me.LblPreviousProcess = New System.Windows.Forms.Label
        Me.TxtProcessIssueNCat = New AgControls.AgTextBox
        Me.LblProcessIssueNCat = New System.Windows.Forms.Label
        Me.TxtProcessIssueNCatDescription = New AgControls.AgTextBox
        Me.LblProcessIssueNCatDescription = New System.Windows.Forms.Label
        Me.TxtProcessReceiveNCatDescription = New AgControls.AgTextBox
        Me.LblProcessReceiveNCatDescription = New System.Windows.Forms.Label
        Me.TxtProcessReceiveNCat = New AgControls.AgTextBox
        Me.LblProcessReceiveNCat = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.TxtProcessReturnNCatDescription = New AgControls.AgTextBox
        Me.LblProcessReturnNCatDescription = New System.Windows.Forms.Label
        Me.TxtProcessReturnNCat = New AgControls.AgTextBox
        Me.LblProcessReturnNCat = New System.Windows.Forms.Label
        Me.LblJobOnReq = New System.Windows.Forms.Label
        Me.TxtJobOn = New AgControls.AgTextBox
        Me.LblJobOn = New System.Windows.Forms.Label
        Me.TxtProcessCancelDesc = New AgControls.AgTextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.TxtProcessCancelNcat = New AgControls.AgTextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.TxtProcessInvoiceNCatDescription = New AgControls.AgTextBox
        Me.LblProcessInvoiceNCatDescription = New System.Windows.Forms.Label
        Me.TxtProcessInvoiceNCat = New AgControls.AgTextBox
        Me.LblProcessInvoiceNCat = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Topctrl1
        '
        Me.Topctrl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Me.Topctrl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Topctrl1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Topctrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Topctrl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Topctrl1.Location = New System.Drawing.Point(0, 0)
        Me.Topctrl1.Mode = "Browse"
        Me.Topctrl1.Name = "Topctrl1"
        Me.Topctrl1.Size = New System.Drawing.Size(872, 41)
        Me.Topctrl1.TabIndex = 21
        Me.Topctrl1.tAdd = True
        Me.Topctrl1.tCancel = True
        Me.Topctrl1.tDel = True
        Me.Topctrl1.tDiscard = False
        Me.Topctrl1.tEdit = True
        Me.Topctrl1.tExit = True
        Me.Topctrl1.tFind = True
        Me.Topctrl1.tFirst = True
        Me.Topctrl1.tLast = True
        Me.Topctrl1.tNext = True
        Me.Topctrl1.tPrev = True
        Me.Topctrl1.tPrn = True
        Me.Topctrl1.tRef = True
        Me.Topctrl1.tSave = False
        Me.Topctrl1.tSite = True
        '
        'TxtManualCode
        '
        Me.TxtManualCode.AgMandatory = True
        Me.TxtManualCode.AgMasterHelp = True
        Me.TxtManualCode.AgNumberLeftPlaces = 0
        Me.TxtManualCode.AgNumberNegetiveAllow = False
        Me.TxtManualCode.AgNumberRightPlaces = 0
        Me.TxtManualCode.AgPickFromLastValue = False
        Me.TxtManualCode.AgRowFilter = ""
        Me.TxtManualCode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtManualCode.AgSelectedValue = Nothing
        Me.TxtManualCode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtManualCode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtManualCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtManualCode.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TxtManualCode.Location = New System.Drawing.Point(350, 66)
        Me.TxtManualCode.MaxLength = 5
        Me.TxtManualCode.Name = "TxtManualCode"
        Me.TxtManualCode.Size = New System.Drawing.Size(100, 18)
        Me.TxtManualCode.TabIndex = 0
        '
        'LblManualCode
        '
        Me.LblManualCode.AutoSize = True
        Me.LblManualCode.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.LblManualCode.Location = New System.Drawing.Point(186, 67)
        Me.LblManualCode.Name = "LblManualCode"
        Me.LblManualCode.Size = New System.Drawing.Size(38, 16)
        Me.LblManualCode.TabIndex = 0
        Me.LblManualCode.Text = "Code"
        '
        'TxtDescription
        '
        Me.TxtDescription.AgMandatory = True
        Me.TxtDescription.AgMasterHelp = True
        Me.TxtDescription.AgNumberLeftPlaces = 0
        Me.TxtDescription.AgNumberNegetiveAllow = False
        Me.TxtDescription.AgNumberRightPlaces = 0
        Me.TxtDescription.AgPickFromLastValue = False
        Me.TxtDescription.AgRowFilter = ""
        Me.TxtDescription.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDescription.AgSelectedValue = Nothing
        Me.TxtDescription.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDescription.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDescription.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TxtDescription.Location = New System.Drawing.Point(350, 86)
        Me.TxtDescription.MaxLength = 50
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(336, 18)
        Me.TxtDescription.TabIndex = 1
        '
        'LblDescription
        '
        Me.LblDescription.AutoSize = True
        Me.LblDescription.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.LblDescription.Location = New System.Drawing.Point(186, 87)
        Me.LblDescription.Name = "LblDescription"
        Me.LblDescription.Size = New System.Drawing.Size(73, 16)
        Me.LblDescription.TabIndex = 150
        Me.LblDescription.Text = "Description"
        '
        'TxtSubCode
        '
        Me.TxtSubCode.AgMandatory = False
        Me.TxtSubCode.AgMasterHelp = False
        Me.TxtSubCode.AgNumberLeftPlaces = 0
        Me.TxtSubCode.AgNumberNegetiveAllow = False
        Me.TxtSubCode.AgNumberRightPlaces = 0
        Me.TxtSubCode.AgPickFromLastValue = False
        Me.TxtSubCode.AgRowFilter = ""
        Me.TxtSubCode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSubCode.AgSelectedValue = Nothing
        Me.TxtSubCode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSubCode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSubCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSubCode.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TxtSubCode.Location = New System.Drawing.Point(350, 126)
        Me.TxtSubCode.MaxLength = 50
        Me.TxtSubCode.Name = "TxtSubCode"
        Me.TxtSubCode.Size = New System.Drawing.Size(336, 18)
        Me.TxtSubCode.TabIndex = 3
        '
        'LblLedgerAc
        '
        Me.LblLedgerAc.AutoSize = True
        Me.LblLedgerAc.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.LblLedgerAc.Location = New System.Drawing.Point(186, 127)
        Me.LblLedgerAc.Name = "LblLedgerAc"
        Me.LblLedgerAc.Size = New System.Drawing.Size(71, 16)
        Me.LblLedgerAc.TabIndex = 152
        Me.LblLedgerAc.Text = "Ledger A/c"
        '
        'LblJobWorkerReq
        '
        Me.LblJobWorkerReq.AutoSize = True
        Me.LblJobWorkerReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblJobWorkerReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblJobWorkerReq.Location = New System.Drawing.Point(334, 74)
        Me.LblJobWorkerReq.Name = "LblJobWorkerReq"
        Me.LblJobWorkerReq.Size = New System.Drawing.Size(10, 7)
        Me.LblJobWorkerReq.TabIndex = 733
        Me.LblJobWorkerReq.Text = "Ä"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(334, 92)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 734
        Me.Label1.Text = "Ä"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(334, 112)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 7)
        Me.Label2.TabIndex = 737
        Me.Label2.Text = "Ä"
        '
        'TxtProcessGroup
        '
        Me.TxtProcessGroup.AgMandatory = False
        Me.TxtProcessGroup.AgMasterHelp = False
        Me.TxtProcessGroup.AgNumberLeftPlaces = 0
        Me.TxtProcessGroup.AgNumberNegetiveAllow = False
        Me.TxtProcessGroup.AgNumberRightPlaces = 0
        Me.TxtProcessGroup.AgPickFromLastValue = False
        Me.TxtProcessGroup.AgRowFilter = ""
        Me.TxtProcessGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtProcessGroup.AgSelectedValue = Nothing
        Me.TxtProcessGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtProcessGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtProcessGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtProcessGroup.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TxtProcessGroup.Location = New System.Drawing.Point(350, 106)
        Me.TxtProcessGroup.MaxLength = 10
        Me.TxtProcessGroup.Name = "TxtProcessGroup"
        Me.TxtProcessGroup.Size = New System.Drawing.Size(336, 18)
        Me.TxtProcessGroup.TabIndex = 2
        '
        'LblProcessGroup
        '
        Me.LblProcessGroup.AutoSize = True
        Me.LblProcessGroup.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.LblProcessGroup.Location = New System.Drawing.Point(186, 107)
        Me.LblProcessGroup.Name = "LblProcessGroup"
        Me.LblProcessGroup.Size = New System.Drawing.Size(95, 16)
        Me.LblProcessGroup.TabIndex = 736
        Me.LblProcessGroup.Text = "Process Group"
        '
        'TxtQCGroup
        '
        Me.TxtQCGroup.AgMandatory = False
        Me.TxtQCGroup.AgMasterHelp = False
        Me.TxtQCGroup.AgNumberLeftPlaces = 0
        Me.TxtQCGroup.AgNumberNegetiveAllow = False
        Me.TxtQCGroup.AgNumberRightPlaces = 0
        Me.TxtQCGroup.AgPickFromLastValue = False
        Me.TxtQCGroup.AgRowFilter = ""
        Me.TxtQCGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtQCGroup.AgSelectedValue = Nothing
        Me.TxtQCGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtQCGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtQCGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtQCGroup.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TxtQCGroup.Location = New System.Drawing.Point(350, 146)
        Me.TxtQCGroup.MaxLength = 50
        Me.TxtQCGroup.Name = "TxtQCGroup"
        Me.TxtQCGroup.Size = New System.Drawing.Size(336, 18)
        Me.TxtQCGroup.TabIndex = 4
        '
        'LblQCGroup
        '
        Me.LblQCGroup.AutoSize = True
        Me.LblQCGroup.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.LblQCGroup.Location = New System.Drawing.Point(186, 147)
        Me.LblQCGroup.Name = "LblQCGroup"
        Me.LblQCGroup.Size = New System.Drawing.Size(66, 16)
        Me.LblQCGroup.TabIndex = 739
        Me.LblQCGroup.Text = "QC Group"
        '
        'TxtStructure
        '
        Me.TxtStructure.AgMandatory = False
        Me.TxtStructure.AgMasterHelp = False
        Me.TxtStructure.AgNumberLeftPlaces = 0
        Me.TxtStructure.AgNumberNegetiveAllow = False
        Me.TxtStructure.AgNumberRightPlaces = 0
        Me.TxtStructure.AgPickFromLastValue = False
        Me.TxtStructure.AgRowFilter = ""
        Me.TxtStructure.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtStructure.AgSelectedValue = Nothing
        Me.TxtStructure.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtStructure.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtStructure.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtStructure.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TxtStructure.Location = New System.Drawing.Point(350, 166)
        Me.TxtStructure.MaxLength = 50
        Me.TxtStructure.Name = "TxtStructure"
        Me.TxtStructure.Size = New System.Drawing.Size(336, 18)
        Me.TxtStructure.TabIndex = 5
        '
        'LblStructure
        '
        Me.LblStructure.AutoSize = True
        Me.LblStructure.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.LblStructure.Location = New System.Drawing.Point(186, 167)
        Me.LblStructure.Name = "LblStructure"
        Me.LblStructure.Size = New System.Drawing.Size(61, 16)
        Me.LblStructure.TabIndex = 741
        Me.LblStructure.Text = "Structure"
        '
        'TxtInsideOutside
        '
        Me.TxtInsideOutside.AgMandatory = True
        Me.TxtInsideOutside.AgMasterHelp = False
        Me.TxtInsideOutside.AgNumberLeftPlaces = 0
        Me.TxtInsideOutside.AgNumberNegetiveAllow = False
        Me.TxtInsideOutside.AgNumberRightPlaces = 0
        Me.TxtInsideOutside.AgPickFromLastValue = False
        Me.TxtInsideOutside.AgRowFilter = ""
        Me.TxtInsideOutside.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtInsideOutside.AgSelectedValue = Nothing
        Me.TxtInsideOutside.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtInsideOutside.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtInsideOutside.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtInsideOutside.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TxtInsideOutside.Location = New System.Drawing.Point(350, 186)
        Me.TxtInsideOutside.MaxLength = 50
        Me.TxtInsideOutside.Name = "TxtInsideOutside"
        Me.TxtInsideOutside.Size = New System.Drawing.Size(336, 18)
        Me.TxtInsideOutside.TabIndex = 6
        '
        'LblInsideOutside
        '
        Me.LblInsideOutside.AutoSize = True
        Me.LblInsideOutside.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.LblInsideOutside.Location = New System.Drawing.Point(186, 187)
        Me.LblInsideOutside.Name = "LblInsideOutside"
        Me.LblInsideOutside.Size = New System.Drawing.Size(91, 16)
        Me.LblInsideOutside.TabIndex = 743
        Me.LblInsideOutside.Text = "Inside/Outside"
        '
        'TxtDefaultJobOrderFor
        '
        Me.TxtDefaultJobOrderFor.AgMandatory = True
        Me.TxtDefaultJobOrderFor.AgMasterHelp = False
        Me.TxtDefaultJobOrderFor.AgNumberLeftPlaces = 0
        Me.TxtDefaultJobOrderFor.AgNumberNegetiveAllow = False
        Me.TxtDefaultJobOrderFor.AgNumberRightPlaces = 0
        Me.TxtDefaultJobOrderFor.AgPickFromLastValue = False
        Me.TxtDefaultJobOrderFor.AgRowFilter = ""
        Me.TxtDefaultJobOrderFor.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDefaultJobOrderFor.AgSelectedValue = Nothing
        Me.TxtDefaultJobOrderFor.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDefaultJobOrderFor.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDefaultJobOrderFor.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDefaultJobOrderFor.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TxtDefaultJobOrderFor.Location = New System.Drawing.Point(350, 206)
        Me.TxtDefaultJobOrderFor.MaxLength = 50
        Me.TxtDefaultJobOrderFor.Name = "TxtDefaultJobOrderFor"
        Me.TxtDefaultJobOrderFor.Size = New System.Drawing.Size(336, 18)
        Me.TxtDefaultJobOrderFor.TabIndex = 7
        '
        'LblDefaultJobOrderFor
        '
        Me.LblDefaultJobOrderFor.AutoSize = True
        Me.LblDefaultJobOrderFor.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.LblDefaultJobOrderFor.Location = New System.Drawing.Point(186, 207)
        Me.LblDefaultJobOrderFor.Name = "LblDefaultJobOrderFor"
        Me.LblDefaultJobOrderFor.Size = New System.Drawing.Size(131, 16)
        Me.LblDefaultJobOrderFor.TabIndex = 745
        Me.LblDefaultJobOrderFor.Text = "Default Job Order For"
        '
        'TxtDefaultBillingType
        '
        Me.TxtDefaultBillingType.AgMandatory = True
        Me.TxtDefaultBillingType.AgMasterHelp = False
        Me.TxtDefaultBillingType.AgNumberLeftPlaces = 0
        Me.TxtDefaultBillingType.AgNumberNegetiveAllow = False
        Me.TxtDefaultBillingType.AgNumberRightPlaces = 0
        Me.TxtDefaultBillingType.AgPickFromLastValue = False
        Me.TxtDefaultBillingType.AgRowFilter = ""
        Me.TxtDefaultBillingType.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDefaultBillingType.AgSelectedValue = Nothing
        Me.TxtDefaultBillingType.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDefaultBillingType.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDefaultBillingType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDefaultBillingType.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TxtDefaultBillingType.Location = New System.Drawing.Point(350, 226)
        Me.TxtDefaultBillingType.MaxLength = 50
        Me.TxtDefaultBillingType.Name = "TxtDefaultBillingType"
        Me.TxtDefaultBillingType.Size = New System.Drawing.Size(336, 18)
        Me.TxtDefaultBillingType.TabIndex = 8
        '
        'LblDefaultBillingType
        '
        Me.LblDefaultBillingType.AutoSize = True
        Me.LblDefaultBillingType.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.LblDefaultBillingType.Location = New System.Drawing.Point(186, 227)
        Me.LblDefaultBillingType.Name = "LblDefaultBillingType"
        Me.LblDefaultBillingType.Size = New System.Drawing.Size(119, 16)
        Me.LblDefaultBillingType.TabIndex = 747
        Me.LblDefaultBillingType.Text = "Default Billing Type"
        '
        'TxtPrevProcess
        '
        Me.TxtPrevProcess.AgMandatory = False
        Me.TxtPrevProcess.AgMasterHelp = False
        Me.TxtPrevProcess.AgNumberLeftPlaces = 0
        Me.TxtPrevProcess.AgNumberNegetiveAllow = False
        Me.TxtPrevProcess.AgNumberRightPlaces = 0
        Me.TxtPrevProcess.AgPickFromLastValue = False
        Me.TxtPrevProcess.AgRowFilter = ""
        Me.TxtPrevProcess.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPrevProcess.AgSelectedValue = Nothing
        Me.TxtPrevProcess.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPrevProcess.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPrevProcess.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPrevProcess.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TxtPrevProcess.Location = New System.Drawing.Point(350, 266)
        Me.TxtPrevProcess.MaxLength = 50
        Me.TxtPrevProcess.Name = "TxtPrevProcess"
        Me.TxtPrevProcess.Size = New System.Drawing.Size(336, 18)
        Me.TxtPrevProcess.TabIndex = 10
        '
        'LblPreviousProcess
        '
        Me.LblPreviousProcess.AutoSize = True
        Me.LblPreviousProcess.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.LblPreviousProcess.Location = New System.Drawing.Point(186, 267)
        Me.LblPreviousProcess.Name = "LblPreviousProcess"
        Me.LblPreviousProcess.Size = New System.Drawing.Size(109, 16)
        Me.LblPreviousProcess.TabIndex = 749
        Me.LblPreviousProcess.Text = "Previous Process"
        '
        'TxtProcessIssueNCat
        '
        Me.TxtProcessIssueNCat.AgMandatory = False
        Me.TxtProcessIssueNCat.AgMasterHelp = True
        Me.TxtProcessIssueNCat.AgNumberLeftPlaces = 0
        Me.TxtProcessIssueNCat.AgNumberNegetiveAllow = False
        Me.TxtProcessIssueNCat.AgNumberRightPlaces = 0
        Me.TxtProcessIssueNCat.AgPickFromLastValue = False
        Me.TxtProcessIssueNCat.AgRowFilter = ""
        Me.TxtProcessIssueNCat.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtProcessIssueNCat.AgSelectedValue = Nothing
        Me.TxtProcessIssueNCat.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtProcessIssueNCat.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtProcessIssueNCat.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtProcessIssueNCat.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TxtProcessIssueNCat.Location = New System.Drawing.Point(350, 286)
        Me.TxtProcessIssueNCat.MaxLength = 5
        Me.TxtProcessIssueNCat.Name = "TxtProcessIssueNCat"
        Me.TxtProcessIssueNCat.Size = New System.Drawing.Size(70, 18)
        Me.TxtProcessIssueNCat.TabIndex = 11
        '
        'LblProcessIssueNCat
        '
        Me.LblProcessIssueNCat.AutoSize = True
        Me.LblProcessIssueNCat.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.LblProcessIssueNCat.Location = New System.Drawing.Point(186, 287)
        Me.LblProcessIssueNCat.Name = "LblProcessIssueNCat"
        Me.LblProcessIssueNCat.Size = New System.Drawing.Size(124, 16)
        Me.LblProcessIssueNCat.TabIndex = 751
        Me.LblProcessIssueNCat.Text = "Process Issue NCat"
        '
        'TxtProcessIssueNCatDescription
        '
        Me.TxtProcessIssueNCatDescription.AgMandatory = False
        Me.TxtProcessIssueNCatDescription.AgMasterHelp = True
        Me.TxtProcessIssueNCatDescription.AgNumberLeftPlaces = 0
        Me.TxtProcessIssueNCatDescription.AgNumberNegetiveAllow = False
        Me.TxtProcessIssueNCatDescription.AgNumberRightPlaces = 0
        Me.TxtProcessIssueNCatDescription.AgPickFromLastValue = False
        Me.TxtProcessIssueNCatDescription.AgRowFilter = ""
        Me.TxtProcessIssueNCatDescription.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtProcessIssueNCatDescription.AgSelectedValue = Nothing
        Me.TxtProcessIssueNCatDescription.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtProcessIssueNCatDescription.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtProcessIssueNCatDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtProcessIssueNCatDescription.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TxtProcessIssueNCatDescription.Location = New System.Drawing.Point(526, 286)
        Me.TxtProcessIssueNCatDescription.MaxLength = 50
        Me.TxtProcessIssueNCatDescription.Name = "TxtProcessIssueNCatDescription"
        Me.TxtProcessIssueNCatDescription.Size = New System.Drawing.Size(160, 18)
        Me.TxtProcessIssueNCatDescription.TabIndex = 12
        '
        'LblProcessIssueNCatDescription
        '
        Me.LblProcessIssueNCatDescription.AutoSize = True
        Me.LblProcessIssueNCatDescription.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.LblProcessIssueNCatDescription.Location = New System.Drawing.Point(424, 287)
        Me.LblProcessIssueNCatDescription.Name = "LblProcessIssueNCatDescription"
        Me.LblProcessIssueNCatDescription.Size = New System.Drawing.Size(73, 16)
        Me.LblProcessIssueNCatDescription.TabIndex = 753
        Me.LblProcessIssueNCatDescription.Text = "Description"
        '
        'TxtProcessReceiveNCatDescription
        '
        Me.TxtProcessReceiveNCatDescription.AgMandatory = False
        Me.TxtProcessReceiveNCatDescription.AgMasterHelp = True
        Me.TxtProcessReceiveNCatDescription.AgNumberLeftPlaces = 0
        Me.TxtProcessReceiveNCatDescription.AgNumberNegetiveAllow = False
        Me.TxtProcessReceiveNCatDescription.AgNumberRightPlaces = 0
        Me.TxtProcessReceiveNCatDescription.AgPickFromLastValue = False
        Me.TxtProcessReceiveNCatDescription.AgRowFilter = ""
        Me.TxtProcessReceiveNCatDescription.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtProcessReceiveNCatDescription.AgSelectedValue = Nothing
        Me.TxtProcessReceiveNCatDescription.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtProcessReceiveNCatDescription.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtProcessReceiveNCatDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtProcessReceiveNCatDescription.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TxtProcessReceiveNCatDescription.Location = New System.Drawing.Point(526, 306)
        Me.TxtProcessReceiveNCatDescription.MaxLength = 50
        Me.TxtProcessReceiveNCatDescription.Name = "TxtProcessReceiveNCatDescription"
        Me.TxtProcessReceiveNCatDescription.Size = New System.Drawing.Size(160, 18)
        Me.TxtProcessReceiveNCatDescription.TabIndex = 14
        '
        'LblProcessReceiveNCatDescription
        '
        Me.LblProcessReceiveNCatDescription.AutoSize = True
        Me.LblProcessReceiveNCatDescription.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.LblProcessReceiveNCatDescription.Location = New System.Drawing.Point(424, 307)
        Me.LblProcessReceiveNCatDescription.Name = "LblProcessReceiveNCatDescription"
        Me.LblProcessReceiveNCatDescription.Size = New System.Drawing.Size(73, 16)
        Me.LblProcessReceiveNCatDescription.TabIndex = 757
        Me.LblProcessReceiveNCatDescription.Text = "Description"
        '
        'TxtProcessReceiveNCat
        '
        Me.TxtProcessReceiveNCat.AgMandatory = False
        Me.TxtProcessReceiveNCat.AgMasterHelp = True
        Me.TxtProcessReceiveNCat.AgNumberLeftPlaces = 0
        Me.TxtProcessReceiveNCat.AgNumberNegetiveAllow = False
        Me.TxtProcessReceiveNCat.AgNumberRightPlaces = 0
        Me.TxtProcessReceiveNCat.AgPickFromLastValue = False
        Me.TxtProcessReceiveNCat.AgRowFilter = ""
        Me.TxtProcessReceiveNCat.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtProcessReceiveNCat.AgSelectedValue = Nothing
        Me.TxtProcessReceiveNCat.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtProcessReceiveNCat.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtProcessReceiveNCat.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtProcessReceiveNCat.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TxtProcessReceiveNCat.Location = New System.Drawing.Point(350, 306)
        Me.TxtProcessReceiveNCat.MaxLength = 5
        Me.TxtProcessReceiveNCat.Name = "TxtProcessReceiveNCat"
        Me.TxtProcessReceiveNCat.Size = New System.Drawing.Size(70, 18)
        Me.TxtProcessReceiveNCat.TabIndex = 13
        '
        'LblProcessReceiveNCat
        '
        Me.LblProcessReceiveNCat.AutoSize = True
        Me.LblProcessReceiveNCat.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.LblProcessReceiveNCat.Location = New System.Drawing.Point(186, 307)
        Me.LblProcessReceiveNCat.Name = "LblProcessReceiveNCat"
        Me.LblProcessReceiveNCat.Size = New System.Drawing.Size(138, 16)
        Me.LblProcessReceiveNCat.TabIndex = 755
        Me.LblProcessReceiveNCat.Text = "Process Receive NCat"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(334, 191)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 7)
        Me.Label3.TabIndex = 758
        Me.Label3.Text = "Ä"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(334, 211)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 7)
        Me.Label4.TabIndex = 759
        Me.Label4.Text = "Ä"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(334, 232)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 7)
        Me.Label5.TabIndex = 760
        Me.Label5.Text = "Ä"
        '
        'TxtProcessReturnNCatDescription
        '
        Me.TxtProcessReturnNCatDescription.AgMandatory = False
        Me.TxtProcessReturnNCatDescription.AgMasterHelp = True
        Me.TxtProcessReturnNCatDescription.AgNumberLeftPlaces = 0
        Me.TxtProcessReturnNCatDescription.AgNumberNegetiveAllow = False
        Me.TxtProcessReturnNCatDescription.AgNumberRightPlaces = 0
        Me.TxtProcessReturnNCatDescription.AgPickFromLastValue = False
        Me.TxtProcessReturnNCatDescription.AgRowFilter = ""
        Me.TxtProcessReturnNCatDescription.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtProcessReturnNCatDescription.AgSelectedValue = Nothing
        Me.TxtProcessReturnNCatDescription.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtProcessReturnNCatDescription.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtProcessReturnNCatDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtProcessReturnNCatDescription.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TxtProcessReturnNCatDescription.Location = New System.Drawing.Point(526, 326)
        Me.TxtProcessReturnNCatDescription.MaxLength = 50
        Me.TxtProcessReturnNCatDescription.Name = "TxtProcessReturnNCatDescription"
        Me.TxtProcessReturnNCatDescription.Size = New System.Drawing.Size(160, 18)
        Me.TxtProcessReturnNCatDescription.TabIndex = 16
        '
        'LblProcessReturnNCatDescription
        '
        Me.LblProcessReturnNCatDescription.AutoSize = True
        Me.LblProcessReturnNCatDescription.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.LblProcessReturnNCatDescription.Location = New System.Drawing.Point(424, 327)
        Me.LblProcessReturnNCatDescription.Name = "LblProcessReturnNCatDescription"
        Me.LblProcessReturnNCatDescription.Size = New System.Drawing.Size(73, 16)
        Me.LblProcessReturnNCatDescription.TabIndex = 769
        Me.LblProcessReturnNCatDescription.Text = "Description"
        '
        'TxtProcessReturnNCat
        '
        Me.TxtProcessReturnNCat.AgMandatory = False
        Me.TxtProcessReturnNCat.AgMasterHelp = True
        Me.TxtProcessReturnNCat.AgNumberLeftPlaces = 0
        Me.TxtProcessReturnNCat.AgNumberNegetiveAllow = False
        Me.TxtProcessReturnNCat.AgNumberRightPlaces = 0
        Me.TxtProcessReturnNCat.AgPickFromLastValue = False
        Me.TxtProcessReturnNCat.AgRowFilter = ""
        Me.TxtProcessReturnNCat.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtProcessReturnNCat.AgSelectedValue = Nothing
        Me.TxtProcessReturnNCat.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtProcessReturnNCat.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtProcessReturnNCat.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtProcessReturnNCat.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TxtProcessReturnNCat.Location = New System.Drawing.Point(350, 326)
        Me.TxtProcessReturnNCat.MaxLength = 5
        Me.TxtProcessReturnNCat.Name = "TxtProcessReturnNCat"
        Me.TxtProcessReturnNCat.Size = New System.Drawing.Size(70, 18)
        Me.TxtProcessReturnNCat.TabIndex = 15
        '
        'LblProcessReturnNCat
        '
        Me.LblProcessReturnNCat.AutoSize = True
        Me.LblProcessReturnNCat.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.LblProcessReturnNCat.Location = New System.Drawing.Point(186, 327)
        Me.LblProcessReturnNCat.Name = "LblProcessReturnNCat"
        Me.LblProcessReturnNCat.Size = New System.Drawing.Size(131, 16)
        Me.LblProcessReturnNCat.TabIndex = 768
        Me.LblProcessReturnNCat.Text = "Process Return NCat"
        '
        'LblJobOnReq
        '
        Me.LblJobOnReq.AutoSize = True
        Me.LblJobOnReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblJobOnReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblJobOnReq.Location = New System.Drawing.Point(334, 252)
        Me.LblJobOnReq.Name = "LblJobOnReq"
        Me.LblJobOnReq.Size = New System.Drawing.Size(10, 7)
        Me.LblJobOnReq.TabIndex = 774
        Me.LblJobOnReq.Text = "Ä"
        '
        'TxtJobOn
        '
        Me.TxtJobOn.AgMandatory = True
        Me.TxtJobOn.AgMasterHelp = False
        Me.TxtJobOn.AgNumberLeftPlaces = 0
        Me.TxtJobOn.AgNumberNegetiveAllow = False
        Me.TxtJobOn.AgNumberRightPlaces = 0
        Me.TxtJobOn.AgPickFromLastValue = False
        Me.TxtJobOn.AgRowFilter = ""
        Me.TxtJobOn.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtJobOn.AgSelectedValue = Nothing
        Me.TxtJobOn.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtJobOn.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtJobOn.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtJobOn.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TxtJobOn.Location = New System.Drawing.Point(350, 246)
        Me.TxtJobOn.MaxLength = 50
        Me.TxtJobOn.Name = "TxtJobOn"
        Me.TxtJobOn.Size = New System.Drawing.Size(336, 18)
        Me.TxtJobOn.TabIndex = 9
        '
        'LblJobOn
        '
        Me.LblJobOn.AutoSize = True
        Me.LblJobOn.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.LblJobOn.Location = New System.Drawing.Point(186, 247)
        Me.LblJobOn.Name = "LblJobOn"
        Me.LblJobOn.Size = New System.Drawing.Size(49, 16)
        Me.LblJobOn.TabIndex = 773
        Me.LblJobOn.Text = "Job On"
        '
        'TxtProcessCancelDesc
        '
        Me.TxtProcessCancelDesc.AgMandatory = False
        Me.TxtProcessCancelDesc.AgMasterHelp = True
        Me.TxtProcessCancelDesc.AgNumberLeftPlaces = 0
        Me.TxtProcessCancelDesc.AgNumberNegetiveAllow = False
        Me.TxtProcessCancelDesc.AgNumberRightPlaces = 0
        Me.TxtProcessCancelDesc.AgPickFromLastValue = False
        Me.TxtProcessCancelDesc.AgRowFilter = ""
        Me.TxtProcessCancelDesc.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtProcessCancelDesc.AgSelectedValue = Nothing
        Me.TxtProcessCancelDesc.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtProcessCancelDesc.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtProcessCancelDesc.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtProcessCancelDesc.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TxtProcessCancelDesc.Location = New System.Drawing.Point(526, 346)
        Me.TxtProcessCancelDesc.MaxLength = 50
        Me.TxtProcessCancelDesc.Name = "TxtProcessCancelDesc"
        Me.TxtProcessCancelDesc.Size = New System.Drawing.Size(160, 18)
        Me.TxtProcessCancelDesc.TabIndex = 18
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label6.Location = New System.Drawing.Point(424, 347)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(73, 16)
        Me.Label6.TabIndex = 778
        Me.Label6.Text = "Description"
        '
        'TxtProcessCancelNcat
        '
        Me.TxtProcessCancelNcat.AgMandatory = False
        Me.TxtProcessCancelNcat.AgMasterHelp = True
        Me.TxtProcessCancelNcat.AgNumberLeftPlaces = 0
        Me.TxtProcessCancelNcat.AgNumberNegetiveAllow = False
        Me.TxtProcessCancelNcat.AgNumberRightPlaces = 0
        Me.TxtProcessCancelNcat.AgPickFromLastValue = False
        Me.TxtProcessCancelNcat.AgRowFilter = ""
        Me.TxtProcessCancelNcat.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtProcessCancelNcat.AgSelectedValue = Nothing
        Me.TxtProcessCancelNcat.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtProcessCancelNcat.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtProcessCancelNcat.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtProcessCancelNcat.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TxtProcessCancelNcat.Location = New System.Drawing.Point(350, 346)
        Me.TxtProcessCancelNcat.MaxLength = 5
        Me.TxtProcessCancelNcat.Name = "TxtProcessCancelNcat"
        Me.TxtProcessCancelNcat.Size = New System.Drawing.Size(70, 18)
        Me.TxtProcessCancelNcat.TabIndex = 17
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.Label7.Location = New System.Drawing.Point(186, 347)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(133, 16)
        Me.Label7.TabIndex = 777
        Me.Label7.Text = "Process Cancel NCat"
        '
        'TxtProcessInvoiceNCatDescription
        '
        Me.TxtProcessInvoiceNCatDescription.AgMandatory = False
        Me.TxtProcessInvoiceNCatDescription.AgMasterHelp = True
        Me.TxtProcessInvoiceNCatDescription.AgNumberLeftPlaces = 0
        Me.TxtProcessInvoiceNCatDescription.AgNumberNegetiveAllow = False
        Me.TxtProcessInvoiceNCatDescription.AgNumberRightPlaces = 0
        Me.TxtProcessInvoiceNCatDescription.AgPickFromLastValue = False
        Me.TxtProcessInvoiceNCatDescription.AgRowFilter = ""
        Me.TxtProcessInvoiceNCatDescription.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtProcessInvoiceNCatDescription.AgSelectedValue = Nothing
        Me.TxtProcessInvoiceNCatDescription.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtProcessInvoiceNCatDescription.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtProcessInvoiceNCatDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtProcessInvoiceNCatDescription.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TxtProcessInvoiceNCatDescription.Location = New System.Drawing.Point(526, 366)
        Me.TxtProcessInvoiceNCatDescription.MaxLength = 50
        Me.TxtProcessInvoiceNCatDescription.Name = "TxtProcessInvoiceNCatDescription"
        Me.TxtProcessInvoiceNCatDescription.Size = New System.Drawing.Size(160, 18)
        Me.TxtProcessInvoiceNCatDescription.TabIndex = 20
        '
        'LblProcessInvoiceNCatDescription
        '
        Me.LblProcessInvoiceNCatDescription.AutoSize = True
        Me.LblProcessInvoiceNCatDescription.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.LblProcessInvoiceNCatDescription.Location = New System.Drawing.Point(424, 367)
        Me.LblProcessInvoiceNCatDescription.Name = "LblProcessInvoiceNCatDescription"
        Me.LblProcessInvoiceNCatDescription.Size = New System.Drawing.Size(73, 16)
        Me.LblProcessInvoiceNCatDescription.TabIndex = 782
        Me.LblProcessInvoiceNCatDescription.Text = "Description"
        '
        'TxtProcessInvoiceNCat
        '
        Me.TxtProcessInvoiceNCat.AgMandatory = False
        Me.TxtProcessInvoiceNCat.AgMasterHelp = True
        Me.TxtProcessInvoiceNCat.AgNumberLeftPlaces = 0
        Me.TxtProcessInvoiceNCat.AgNumberNegetiveAllow = False
        Me.TxtProcessInvoiceNCat.AgNumberRightPlaces = 0
        Me.TxtProcessInvoiceNCat.AgPickFromLastValue = False
        Me.TxtProcessInvoiceNCat.AgRowFilter = ""
        Me.TxtProcessInvoiceNCat.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtProcessInvoiceNCat.AgSelectedValue = Nothing
        Me.TxtProcessInvoiceNCat.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtProcessInvoiceNCat.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtProcessInvoiceNCat.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtProcessInvoiceNCat.Font = New System.Drawing.Font("Arial", 11.25!)
        Me.TxtProcessInvoiceNCat.Location = New System.Drawing.Point(350, 366)
        Me.TxtProcessInvoiceNCat.MaxLength = 5
        Me.TxtProcessInvoiceNCat.Name = "TxtProcessInvoiceNCat"
        Me.TxtProcessInvoiceNCat.Size = New System.Drawing.Size(70, 18)
        Me.TxtProcessInvoiceNCat.TabIndex = 19
        '
        'LblProcessInvoiceNCat
        '
        Me.LblProcessInvoiceNCat.AutoSize = True
        Me.LblProcessInvoiceNCat.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.LblProcessInvoiceNCat.Location = New System.Drawing.Point(186, 367)
        Me.LblProcessInvoiceNCat.Name = "LblProcessInvoiceNCat"
        Me.LblProcessInvoiceNCat.Size = New System.Drawing.Size(132, 16)
        Me.LblProcessInvoiceNCat.TabIndex = 781
        Me.LblProcessInvoiceNCat.Text = "Process Invoice NCat"
        '
        'FrmProcess
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(872, 431)
        Me.Controls.Add(Me.TxtProcessInvoiceNCatDescription)
        Me.Controls.Add(Me.LblProcessInvoiceNCatDescription)
        Me.Controls.Add(Me.TxtProcessInvoiceNCat)
        Me.Controls.Add(Me.LblProcessInvoiceNCat)
        Me.Controls.Add(Me.TxtProcessCancelDesc)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtProcessCancelNcat)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.LblJobOnReq)
        Me.Controls.Add(Me.TxtJobOn)
        Me.Controls.Add(Me.LblJobOn)
        Me.Controls.Add(Me.TxtProcessReturnNCatDescription)
        Me.Controls.Add(Me.LblProcessReturnNCatDescription)
        Me.Controls.Add(Me.TxtProcessReturnNCat)
        Me.Controls.Add(Me.LblProcessReturnNCat)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtProcessReceiveNCatDescription)
        Me.Controls.Add(Me.LblProcessReceiveNCatDescription)
        Me.Controls.Add(Me.TxtProcessReceiveNCat)
        Me.Controls.Add(Me.LblProcessReceiveNCat)
        Me.Controls.Add(Me.TxtProcessIssueNCatDescription)
        Me.Controls.Add(Me.LblProcessIssueNCatDescription)
        Me.Controls.Add(Me.TxtProcessIssueNCat)
        Me.Controls.Add(Me.LblProcessIssueNCat)
        Me.Controls.Add(Me.TxtPrevProcess)
        Me.Controls.Add(Me.LblPreviousProcess)
        Me.Controls.Add(Me.TxtDefaultBillingType)
        Me.Controls.Add(Me.LblDefaultBillingType)
        Me.Controls.Add(Me.TxtDefaultJobOrderFor)
        Me.Controls.Add(Me.LblDefaultJobOrderFor)
        Me.Controls.Add(Me.TxtInsideOutside)
        Me.Controls.Add(Me.LblInsideOutside)
        Me.Controls.Add(Me.TxtStructure)
        Me.Controls.Add(Me.LblStructure)
        Me.Controls.Add(Me.TxtQCGroup)
        Me.Controls.Add(Me.LblQCGroup)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtProcessGroup)
        Me.Controls.Add(Me.LblProcessGroup)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblJobWorkerReq)
        Me.Controls.Add(Me.TxtSubCode)
        Me.Controls.Add(Me.LblLedgerAc)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.LblDescription)
        Me.Controls.Add(Me.Topctrl1)
        Me.Controls.Add(Me.TxtManualCode)
        Me.Controls.Add(Me.LblManualCode)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmProcess"
        Me.Text = "Process Master"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Topctrl1 As Topctrl.Topctrl
    Friend WithEvents TxtManualCode As AgControls.AgTextBox
    Friend WithEvents LblManualCode As System.Windows.Forms.Label
    Friend WithEvents TxtDescription As AgControls.AgTextBox
    Friend WithEvents LblDescription As System.Windows.Forms.Label
    Friend WithEvents TxtSubCode As AgControls.AgTextBox
    Friend WithEvents LblLedgerAc As System.Windows.Forms.Label
    Protected WithEvents LblJobWorkerReq As System.Windows.Forms.Label
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtProcessGroup As AgControls.AgTextBox
    Friend WithEvents LblProcessGroup As System.Windows.Forms.Label
    Friend WithEvents TxtQCGroup As AgControls.AgTextBox
    Friend WithEvents LblQCGroup As System.Windows.Forms.Label
    Friend WithEvents TxtStructure As AgControls.AgTextBox
    Friend WithEvents LblStructure As System.Windows.Forms.Label
    Friend WithEvents TxtInsideOutside As AgControls.AgTextBox
    Friend WithEvents LblInsideOutside As System.Windows.Forms.Label
    Friend WithEvents TxtDefaultJobOrderFor As AgControls.AgTextBox
    Friend WithEvents LblDefaultJobOrderFor As System.Windows.Forms.Label
    Friend WithEvents TxtDefaultBillingType As AgControls.AgTextBox
    Friend WithEvents LblDefaultBillingType As System.Windows.Forms.Label
    Friend WithEvents TxtPrevProcess As AgControls.AgTextBox
    Friend WithEvents LblPreviousProcess As System.Windows.Forms.Label
    Friend WithEvents TxtProcessIssueNCat As AgControls.AgTextBox
    Friend WithEvents LblProcessIssueNCat As System.Windows.Forms.Label
    Friend WithEvents TxtProcessIssueNCatDescription As AgControls.AgTextBox
    Friend WithEvents LblProcessIssueNCatDescription As System.Windows.Forms.Label
    Friend WithEvents TxtProcessReceiveNCatDescription As AgControls.AgTextBox
    Friend WithEvents LblProcessReceiveNCatDescription As System.Windows.Forms.Label
    Friend WithEvents TxtProcessReceiveNCat As AgControls.AgTextBox
    Friend WithEvents LblProcessReceiveNCat As System.Windows.Forms.Label
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TxtProcessReturnNCatDescription As AgControls.AgTextBox
    Friend WithEvents LblProcessReturnNCatDescription As System.Windows.Forms.Label
    Friend WithEvents TxtProcessReturnNCat As AgControls.AgTextBox
    Friend WithEvents LblProcessReturnNCat As System.Windows.Forms.Label
    Protected WithEvents LblJobOnReq As System.Windows.Forms.Label
    Friend WithEvents TxtJobOn As AgControls.AgTextBox
    Friend WithEvents LblJobOn As System.Windows.Forms.Label
    Friend WithEvents TxtProcessCancelDesc As AgControls.AgTextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtProcessCancelNcat As AgControls.AgTextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TxtProcessInvoiceNCatDescription As AgControls.AgTextBox
    Friend WithEvents LblProcessInvoiceNCatDescription As System.Windows.Forms.Label
    Friend WithEvents TxtProcessInvoiceNCat As AgControls.AgTextBox
    Friend WithEvents LblProcessInvoiceNCat As System.Windows.Forms.Label
End Class
