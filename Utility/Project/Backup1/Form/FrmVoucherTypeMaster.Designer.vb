<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVoucherTypeMaster
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
        Me.Topctrl1 = New Topctrl.Topctrl
        Me.LblV_Type = New System.Windows.Forms.Label
        Me.TxtV_Type = New AgControls.AgTextBox
        Me.LblV_TypeReq = New System.Windows.Forms.Label
        Me.LblDescriptionReq = New System.Windows.Forms.Label
        Me.LblDescription = New System.Windows.Forms.Label
        Me.TxtDescription = New AgControls.AgTextBox
        Me.LblShort_NameReq = New System.Windows.Forms.Label
        Me.LblShort_Name = New System.Windows.Forms.Label
        Me.TxtShort_Name = New AgControls.AgTextBox
        Me.LblCategoryReq = New System.Windows.Forms.Label
        Me.LblCategory = New System.Windows.Forms.Label
        Me.TxtCategory = New AgControls.AgTextBox
        Me.LblNCatReq = New System.Windows.Forms.Label
        Me.LblNCat = New System.Windows.Forms.Label
        Me.LblNumber_MethodReq = New System.Windows.Forms.Label
        Me.LblNumber_Method = New System.Windows.Forms.Label
        Me.TxtNumber_Method = New AgControls.AgTextBox
        Me.LblSaperate_NarrReq = New System.Windows.Forms.Label
        Me.LblSaperate_Narr = New System.Windows.Forms.Label
        Me.TxtSaperate_Narr = New AgControls.AgTextBox
        Me.LblCommon_NarrReq = New System.Windows.Forms.Label
        Me.LblCommon_Narr = New System.Windows.Forms.Label
        Me.TxtCommon_Narr = New AgControls.AgTextBox
        Me.LblChqNoReq = New System.Windows.Forms.Label
        Me.LblChqNo = New System.Windows.Forms.Label
        Me.TxtChqNo = New AgControls.AgTextBox
        Me.LblChqDtReq = New System.Windows.Forms.Label
        Me.LblChqDate = New System.Windows.Forms.Label
        Me.TxtChqDate = New AgControls.AgTextBox
        Me.LblClgDtReq = New System.Windows.Forms.Label
        Me.LblClgDt = New System.Windows.Forms.Label
        Me.TxtClgDt = New AgControls.AgTextBox
        Me.Tc1 = New System.Windows.Forms.TabControl
        Me.Tp1 = New System.Windows.Forms.TabPage
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.Tp2 = New System.Windows.Forms.TabPage
        Me.Pnl2 = New System.Windows.Forms.Panel
        Me.Tp3 = New System.Windows.Forms.TabPage
        Me.Pnl3 = New System.Windows.Forms.Panel
        Me.LblAffect_FA = New System.Windows.Forms.Label
        Me.TxtAffect_FA = New AgControls.AgTextBox
        Me.TxtNarration = New AgControls.AgTextBox
        Me.LblNarration = New System.Windows.Forms.Label
        Me.LblSystemDefine = New System.Windows.Forms.Label
        Me.LblIsShowVoucherReferenceReq = New System.Windows.Forms.Label
        Me.LblIsShowVoucherReference = New System.Windows.Forms.Label
        Me.TxtIsShowVoucherReference = New AgControls.AgTextBox
        Me.TxtNCat = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtMenuName = New AgControls.AgTextBox
        Me.TxtModule = New AgControls.AgTextBox
        Me.Tc1.SuspendLayout()
        Me.Tp1.SuspendLayout()
        Me.Tp2.SuspendLayout()
        Me.Tp3.SuspendLayout()
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
        Me.Topctrl1.TabIndex = 16
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
        'LblV_Type
        '
        Me.LblV_Type.AutoSize = True
        Me.LblV_Type.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblV_Type.Location = New System.Drawing.Point(158, 73)
        Me.LblV_Type.Name = "LblV_Type"
        Me.LblV_Type.Size = New System.Drawing.Size(86, 13)
        Me.LblV_Type.TabIndex = 209
        Me.LblV_Type.Text = "Voucher Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgAllowUserToEnableMasterHelp = False
        Me.TxtV_Type.AgLastValueTag = Nothing
        Me.TxtV_Type.AgLastValueText = Nothing
        Me.TxtV_Type.AgMandatory = True
        Me.TxtV_Type.AgMasterHelp = True
        Me.TxtV_Type.AgNumberLeftPlaces = 0
        Me.TxtV_Type.AgNumberNegetiveAllow = False
        Me.TxtV_Type.AgNumberRightPlaces = 0
        Me.TxtV_Type.AgPickFromLastValue = False
        Me.TxtV_Type.AgRowFilter = ""
        Me.TxtV_Type.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtV_Type.AgSelectedValue = Nothing
        Me.TxtV_Type.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtV_Type.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtV_Type.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtV_Type.Location = New System.Drawing.Point(315, 69)
        Me.TxtV_Type.MaxLength = 5
        Me.TxtV_Type.Name = "TxtV_Type"
        Me.TxtV_Type.Size = New System.Drawing.Size(100, 21)
        Me.TxtV_Type.TabIndex = 0
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.AutoSize = True
        Me.LblV_TypeReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblV_TypeReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblV_TypeReq.Location = New System.Drawing.Point(302, 77)
        Me.LblV_TypeReq.Name = "LblV_TypeReq"
        Me.LblV_TypeReq.Size = New System.Drawing.Size(10, 7)
        Me.LblV_TypeReq.TabIndex = 212
        Me.LblV_TypeReq.Text = "Ä"
        '
        'LblDescriptionReq
        '
        Me.LblDescriptionReq.AutoSize = True
        Me.LblDescriptionReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblDescriptionReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblDescriptionReq.Location = New System.Drawing.Point(302, 99)
        Me.LblDescriptionReq.Name = "LblDescriptionReq"
        Me.LblDescriptionReq.Size = New System.Drawing.Size(10, 7)
        Me.LblDescriptionReq.TabIndex = 215
        Me.LblDescriptionReq.Text = "Ä"
        '
        'LblDescription
        '
        Me.LblDescription.AutoSize = True
        Me.LblDescription.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDescription.Location = New System.Drawing.Point(158, 95)
        Me.LblDescription.Name = "LblDescription"
        Me.LblDescription.Size = New System.Drawing.Size(71, 13)
        Me.LblDescription.TabIndex = 214
        Me.LblDescription.Text = "Description"
        '
        'TxtDescription
        '
        Me.TxtDescription.AgAllowUserToEnableMasterHelp = False
        Me.TxtDescription.AgLastValueTag = Nothing
        Me.TxtDescription.AgLastValueText = Nothing
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
        Me.TxtDescription.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDescription.Location = New System.Drawing.Point(315, 91)
        Me.TxtDescription.MaxLength = 50
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(400, 21)
        Me.TxtDescription.TabIndex = 2
        '
        'LblShort_NameReq
        '
        Me.LblShort_NameReq.AutoSize = True
        Me.LblShort_NameReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblShort_NameReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblShort_NameReq.Location = New System.Drawing.Point(602, 77)
        Me.LblShort_NameReq.Name = "LblShort_NameReq"
        Me.LblShort_NameReq.Size = New System.Drawing.Size(10, 7)
        Me.LblShort_NameReq.TabIndex = 218
        Me.LblShort_NameReq.Text = "Ä"
        '
        'LblShort_Name
        '
        Me.LblShort_Name.AutoSize = True
        Me.LblShort_Name.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblShort_Name.Location = New System.Drawing.Point(462, 73)
        Me.LblShort_Name.Name = "LblShort_Name"
        Me.LblShort_Name.Size = New System.Drawing.Size(75, 13)
        Me.LblShort_Name.TabIndex = 217
        Me.LblShort_Name.Text = "Short Name"
        '
        'TxtShort_Name
        '
        Me.TxtShort_Name.AgAllowUserToEnableMasterHelp = False
        Me.TxtShort_Name.AgLastValueTag = Nothing
        Me.TxtShort_Name.AgLastValueText = Nothing
        Me.TxtShort_Name.AgMandatory = True
        Me.TxtShort_Name.AgMasterHelp = True
        Me.TxtShort_Name.AgNumberLeftPlaces = 0
        Me.TxtShort_Name.AgNumberNegetiveAllow = False
        Me.TxtShort_Name.AgNumberRightPlaces = 0
        Me.TxtShort_Name.AgPickFromLastValue = False
        Me.TxtShort_Name.AgRowFilter = ""
        Me.TxtShort_Name.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtShort_Name.AgSelectedValue = Nothing
        Me.TxtShort_Name.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtShort_Name.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtShort_Name.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtShort_Name.Location = New System.Drawing.Point(615, 69)
        Me.TxtShort_Name.MaxLength = 10
        Me.TxtShort_Name.Name = "TxtShort_Name"
        Me.TxtShort_Name.Size = New System.Drawing.Size(100, 21)
        Me.TxtShort_Name.TabIndex = 1
        '
        'LblCategoryReq
        '
        Me.LblCategoryReq.AutoSize = True
        Me.LblCategoryReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblCategoryReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblCategoryReq.Location = New System.Drawing.Point(302, 122)
        Me.LblCategoryReq.Name = "LblCategoryReq"
        Me.LblCategoryReq.Size = New System.Drawing.Size(10, 7)
        Me.LblCategoryReq.TabIndex = 221
        Me.LblCategoryReq.Text = "Ä"
        '
        'LblCategory
        '
        Me.LblCategory.AutoSize = True
        Me.LblCategory.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCategory.Location = New System.Drawing.Point(158, 118)
        Me.LblCategory.Name = "LblCategory"
        Me.LblCategory.Size = New System.Drawing.Size(60, 13)
        Me.LblCategory.TabIndex = 220
        Me.LblCategory.Text = "Category"
        '
        'TxtCategory
        '
        Me.TxtCategory.AgAllowUserToEnableMasterHelp = False
        Me.TxtCategory.AgLastValueTag = Nothing
        Me.TxtCategory.AgLastValueText = Nothing
        Me.TxtCategory.AgMandatory = True
        Me.TxtCategory.AgMasterHelp = False
        Me.TxtCategory.AgNumberLeftPlaces = 0
        Me.TxtCategory.AgNumberNegetiveAllow = False
        Me.TxtCategory.AgNumberRightPlaces = 0
        Me.TxtCategory.AgPickFromLastValue = False
        Me.TxtCategory.AgRowFilter = ""
        Me.TxtCategory.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCategory.AgSelectedValue = Nothing
        Me.TxtCategory.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCategory.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCategory.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCategory.Location = New System.Drawing.Point(315, 114)
        Me.TxtCategory.MaxLength = 5
        Me.TxtCategory.Name = "TxtCategory"
        Me.TxtCategory.Size = New System.Drawing.Size(100, 21)
        Me.TxtCategory.TabIndex = 3
        '
        'LblNCatReq
        '
        Me.LblNCatReq.AutoSize = True
        Me.LblNCatReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblNCatReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblNCatReq.Location = New System.Drawing.Point(602, 121)
        Me.LblNCatReq.Name = "LblNCatReq"
        Me.LblNCatReq.Size = New System.Drawing.Size(10, 7)
        Me.LblNCatReq.TabIndex = 224
        Me.LblNCatReq.Text = "Ä"
        '
        'LblNCat
        '
        Me.LblNCat.AutoSize = True
        Me.LblNCat.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNCat.Location = New System.Drawing.Point(463, 117)
        Me.LblNCat.Name = "LblNCat"
        Me.LblNCat.Size = New System.Drawing.Size(79, 13)
        Me.LblNCat.TabIndex = 223
        Me.LblNCat.Text = "Nature Code"
        '
        'LblNumber_MethodReq
        '
        Me.LblNumber_MethodReq.AutoSize = True
        Me.LblNumber_MethodReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblNumber_MethodReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblNumber_MethodReq.Location = New System.Drawing.Point(302, 143)
        Me.LblNumber_MethodReq.Name = "LblNumber_MethodReq"
        Me.LblNumber_MethodReq.Size = New System.Drawing.Size(10, 7)
        Me.LblNumber_MethodReq.TabIndex = 227
        Me.LblNumber_MethodReq.Text = "Ä"
        '
        'LblNumber_Method
        '
        Me.LblNumber_Method.AutoSize = True
        Me.LblNumber_Method.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNumber_Method.Location = New System.Drawing.Point(158, 140)
        Me.LblNumber_Method.Name = "LblNumber_Method"
        Me.LblNumber_Method.Size = New System.Drawing.Size(114, 13)
        Me.LblNumber_Method.TabIndex = 226
        Me.LblNumber_Method.Text = "Numbering Method"
        '
        'TxtNumber_Method
        '
        Me.TxtNumber_Method.AgAllowUserToEnableMasterHelp = False
        Me.TxtNumber_Method.AgLastValueTag = Nothing
        Me.TxtNumber_Method.AgLastValueText = Nothing
        Me.TxtNumber_Method.AgMandatory = True
        Me.TxtNumber_Method.AgMasterHelp = False
        Me.TxtNumber_Method.AgNumberLeftPlaces = 0
        Me.TxtNumber_Method.AgNumberNegetiveAllow = False
        Me.TxtNumber_Method.AgNumberRightPlaces = 0
        Me.TxtNumber_Method.AgPickFromLastValue = False
        Me.TxtNumber_Method.AgRowFilter = ""
        Me.TxtNumber_Method.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtNumber_Method.AgSelectedValue = Nothing
        Me.TxtNumber_Method.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtNumber_Method.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtNumber_Method.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNumber_Method.Location = New System.Drawing.Point(315, 136)
        Me.TxtNumber_Method.MaxLength = 9
        Me.TxtNumber_Method.Name = "TxtNumber_Method"
        Me.TxtNumber_Method.Size = New System.Drawing.Size(100, 21)
        Me.TxtNumber_Method.TabIndex = 5
        '
        'LblSaperate_NarrReq
        '
        Me.LblSaperate_NarrReq.AutoSize = True
        Me.LblSaperate_NarrReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblSaperate_NarrReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblSaperate_NarrReq.Location = New System.Drawing.Point(302, 188)
        Me.LblSaperate_NarrReq.Name = "LblSaperate_NarrReq"
        Me.LblSaperate_NarrReq.Size = New System.Drawing.Size(10, 7)
        Me.LblSaperate_NarrReq.TabIndex = 230
        Me.LblSaperate_NarrReq.Text = "Ä"
        '
        'LblSaperate_Narr
        '
        Me.LblSaperate_Narr.AutoSize = True
        Me.LblSaperate_Narr.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSaperate_Narr.Location = New System.Drawing.Point(158, 183)
        Me.LblSaperate_Narr.Name = "LblSaperate_Narr"
        Me.LblSaperate_Narr.Size = New System.Drawing.Size(137, 13)
        Me.LblSaperate_Narr.TabIndex = 229
        Me.LblSaperate_Narr.Text = "Is Saperate Narration?"
        '
        'TxtSaperate_Narr
        '
        Me.TxtSaperate_Narr.AgAllowUserToEnableMasterHelp = False
        Me.TxtSaperate_Narr.AgLastValueTag = Nothing
        Me.TxtSaperate_Narr.AgLastValueText = Nothing
        Me.TxtSaperate_Narr.AgMandatory = True
        Me.TxtSaperate_Narr.AgMasterHelp = False
        Me.TxtSaperate_Narr.AgNumberLeftPlaces = 0
        Me.TxtSaperate_Narr.AgNumberNegetiveAllow = False
        Me.TxtSaperate_Narr.AgNumberRightPlaces = 0
        Me.TxtSaperate_Narr.AgPickFromLastValue = False
        Me.TxtSaperate_Narr.AgRowFilter = ""
        Me.TxtSaperate_Narr.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSaperate_Narr.AgSelectedValue = Nothing
        Me.TxtSaperate_Narr.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSaperate_Narr.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtSaperate_Narr.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSaperate_Narr.Location = New System.Drawing.Point(315, 180)
        Me.TxtSaperate_Narr.MaxLength = 20
        Me.TxtSaperate_Narr.Name = "TxtSaperate_Narr"
        Me.TxtSaperate_Narr.Size = New System.Drawing.Size(100, 21)
        Me.TxtSaperate_Narr.TabIndex = 9
        '
        'LblCommon_NarrReq
        '
        Me.LblCommon_NarrReq.AutoSize = True
        Me.LblCommon_NarrReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblCommon_NarrReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblCommon_NarrReq.Location = New System.Drawing.Point(603, 188)
        Me.LblCommon_NarrReq.Name = "LblCommon_NarrReq"
        Me.LblCommon_NarrReq.Size = New System.Drawing.Size(10, 7)
        Me.LblCommon_NarrReq.TabIndex = 233
        Me.LblCommon_NarrReq.Text = "Ä"
        '
        'LblCommon_Narr
        '
        Me.LblCommon_Narr.AutoSize = True
        Me.LblCommon_Narr.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCommon_Narr.Location = New System.Drawing.Point(463, 183)
        Me.LblCommon_Narr.Name = "LblCommon_Narr"
        Me.LblCommon_Narr.Size = New System.Drawing.Size(137, 13)
        Me.LblCommon_Narr.TabIndex = 232
        Me.LblCommon_Narr.Text = "Is Common Narration?"
        '
        'TxtCommon_Narr
        '
        Me.TxtCommon_Narr.AgAllowUserToEnableMasterHelp = False
        Me.TxtCommon_Narr.AgLastValueTag = Nothing
        Me.TxtCommon_Narr.AgLastValueText = Nothing
        Me.TxtCommon_Narr.AgMandatory = True
        Me.TxtCommon_Narr.AgMasterHelp = False
        Me.TxtCommon_Narr.AgNumberLeftPlaces = 0
        Me.TxtCommon_Narr.AgNumberNegetiveAllow = False
        Me.TxtCommon_Narr.AgNumberRightPlaces = 0
        Me.TxtCommon_Narr.AgPickFromLastValue = False
        Me.TxtCommon_Narr.AgRowFilter = ""
        Me.TxtCommon_Narr.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCommon_Narr.AgSelectedValue = Nothing
        Me.TxtCommon_Narr.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCommon_Narr.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtCommon_Narr.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCommon_Narr.Location = New System.Drawing.Point(615, 180)
        Me.TxtCommon_Narr.MaxLength = 20
        Me.TxtCommon_Narr.Name = "TxtCommon_Narr"
        Me.TxtCommon_Narr.Size = New System.Drawing.Size(100, 21)
        Me.TxtCommon_Narr.TabIndex = 10
        '
        'LblChqNoReq
        '
        Me.LblChqNoReq.AutoSize = True
        Me.LblChqNoReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblChqNoReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblChqNoReq.Location = New System.Drawing.Point(602, 144)
        Me.LblChqNoReq.Name = "LblChqNoReq"
        Me.LblChqNoReq.Size = New System.Drawing.Size(10, 7)
        Me.LblChqNoReq.TabIndex = 236
        Me.LblChqNoReq.Text = "Ä"
        '
        'LblChqNo
        '
        Me.LblChqNo.AutoSize = True
        Me.LblChqNo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblChqNo.Location = New System.Drawing.Point(463, 140)
        Me.LblChqNo.Name = "LblChqNo"
        Me.LblChqNo.Size = New System.Drawing.Size(95, 13)
        Me.LblChqNo.TabIndex = 235
        Me.LblChqNo.Text = "Is Cheque No.?"
        '
        'TxtChqNo
        '
        Me.TxtChqNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtChqNo.AgLastValueTag = Nothing
        Me.TxtChqNo.AgLastValueText = Nothing
        Me.TxtChqNo.AgMandatory = True
        Me.TxtChqNo.AgMasterHelp = False
        Me.TxtChqNo.AgNumberLeftPlaces = 0
        Me.TxtChqNo.AgNumberNegetiveAllow = False
        Me.TxtChqNo.AgNumberRightPlaces = 0
        Me.TxtChqNo.AgPickFromLastValue = False
        Me.TxtChqNo.AgRowFilter = ""
        Me.TxtChqNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtChqNo.AgSelectedValue = Nothing
        Me.TxtChqNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtChqNo.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtChqNo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtChqNo.Location = New System.Drawing.Point(615, 136)
        Me.TxtChqNo.MaxLength = 20
        Me.TxtChqNo.Name = "TxtChqNo"
        Me.TxtChqNo.Size = New System.Drawing.Size(100, 21)
        Me.TxtChqNo.TabIndex = 6
        '
        'LblChqDtReq
        '
        Me.LblChqDtReq.AutoSize = True
        Me.LblChqDtReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblChqDtReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblChqDtReq.Location = New System.Drawing.Point(302, 166)
        Me.LblChqDtReq.Name = "LblChqDtReq"
        Me.LblChqDtReq.Size = New System.Drawing.Size(10, 7)
        Me.LblChqDtReq.TabIndex = 239
        Me.LblChqDtReq.Text = "Ä"
        '
        'LblChqDate
        '
        Me.LblChqDate.AutoSize = True
        Me.LblChqDate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblChqDate.Location = New System.Drawing.Point(158, 162)
        Me.LblChqDate.Name = "LblChqDate"
        Me.LblChqDate.Size = New System.Drawing.Size(103, 13)
        Me.LblChqDate.TabIndex = 238
        Me.LblChqDate.Text = "Is Cheque Date?"
        '
        'TxtChqDate
        '
        Me.TxtChqDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtChqDate.AgLastValueTag = Nothing
        Me.TxtChqDate.AgLastValueText = Nothing
        Me.TxtChqDate.AgMandatory = True
        Me.TxtChqDate.AgMasterHelp = False
        Me.TxtChqDate.AgNumberLeftPlaces = 0
        Me.TxtChqDate.AgNumberNegetiveAllow = False
        Me.TxtChqDate.AgNumberRightPlaces = 0
        Me.TxtChqDate.AgPickFromLastValue = False
        Me.TxtChqDate.AgRowFilter = ""
        Me.TxtChqDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtChqDate.AgSelectedValue = Nothing
        Me.TxtChqDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtChqDate.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtChqDate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtChqDate.Location = New System.Drawing.Point(315, 158)
        Me.TxtChqDate.MaxLength = 20
        Me.TxtChqDate.Name = "TxtChqDate"
        Me.TxtChqDate.Size = New System.Drawing.Size(100, 21)
        Me.TxtChqDate.TabIndex = 7
        '
        'LblClgDtReq
        '
        Me.LblClgDtReq.AutoSize = True
        Me.LblClgDtReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblClgDtReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblClgDtReq.Location = New System.Drawing.Point(602, 166)
        Me.LblClgDtReq.Name = "LblClgDtReq"
        Me.LblClgDtReq.Size = New System.Drawing.Size(10, 7)
        Me.LblClgDtReq.TabIndex = 242
        Me.LblClgDtReq.Text = "Ä"
        '
        'LblClgDt
        '
        Me.LblClgDt.AutoSize = True
        Me.LblClgDt.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblClgDt.Location = New System.Drawing.Point(463, 162)
        Me.LblClgDt.Name = "LblClgDt"
        Me.LblClgDt.Size = New System.Drawing.Size(107, 13)
        Me.LblClgDt.TabIndex = 241
        Me.LblClgDt.Text = "Is Clearing Date?"
        '
        'TxtClgDt
        '
        Me.TxtClgDt.AgAllowUserToEnableMasterHelp = False
        Me.TxtClgDt.AgLastValueTag = Nothing
        Me.TxtClgDt.AgLastValueText = Nothing
        Me.TxtClgDt.AgMandatory = True
        Me.TxtClgDt.AgMasterHelp = False
        Me.TxtClgDt.AgNumberLeftPlaces = 0
        Me.TxtClgDt.AgNumberNegetiveAllow = False
        Me.TxtClgDt.AgNumberRightPlaces = 0
        Me.TxtClgDt.AgPickFromLastValue = False
        Me.TxtClgDt.AgRowFilter = ""
        Me.TxtClgDt.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtClgDt.AgSelectedValue = Nothing
        Me.TxtClgDt.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtClgDt.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtClgDt.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtClgDt.Location = New System.Drawing.Point(615, 158)
        Me.TxtClgDt.MaxLength = 20
        Me.TxtClgDt.Name = "TxtClgDt"
        Me.TxtClgDt.Size = New System.Drawing.Size(100, 21)
        Me.TxtClgDt.TabIndex = 8
        '
        'Tc1
        '
        Me.Tc1.Controls.Add(Me.Tp1)
        Me.Tc1.Controls.Add(Me.Tp2)
        Me.Tc1.Controls.Add(Me.Tp3)
        Me.Tc1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Tc1.Location = New System.Drawing.Point(161, 298)
        Me.Tc1.Name = "Tc1"
        Me.Tc1.SelectedIndex = 0
        Me.Tc1.Size = New System.Drawing.Size(554, 275)
        Me.Tc1.TabIndex = 15
        '
        'Tp1
        '
        Me.Tp1.Controls.Add(Me.Pnl1)
        Me.Tp1.Location = New System.Drawing.Point(4, 22)
        Me.Tp1.Name = "Tp1"
        Me.Tp1.Padding = New System.Windows.Forms.Padding(3)
        Me.Tp1.Size = New System.Drawing.Size(546, 249)
        Me.Tp1.TabIndex = 0
        Me.Tp1.Text = "Prefix Details"
        Me.Tp1.UseVisualStyleBackColor = True
        '
        'Pnl1
        '
        Me.Pnl1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl1.Location = New System.Drawing.Point(33, 20)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(476, 208)
        Me.Pnl1.TabIndex = 0
        '
        'Tp2
        '
        Me.Tp2.Controls.Add(Me.Pnl2)
        Me.Tp2.Location = New System.Drawing.Point(4, 22)
        Me.Tp2.Name = "Tp2"
        Me.Tp2.Padding = New System.Windows.Forms.Padding(3)
        Me.Tp2.Size = New System.Drawing.Size(546, 249)
        Me.Tp2.TabIndex = 1
        Me.Tp2.Text = "Must Have A/C Group"
        Me.Tp2.UseVisualStyleBackColor = True
        '
        'Pnl2
        '
        Me.Pnl2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl2.Location = New System.Drawing.Point(63, 23)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(414, 206)
        Me.Pnl2.TabIndex = 15
        '
        'Tp3
        '
        Me.Tp3.Controls.Add(Me.Pnl3)
        Me.Tp3.Location = New System.Drawing.Point(4, 22)
        Me.Tp3.Name = "Tp3"
        Me.Tp3.Size = New System.Drawing.Size(546, 249)
        Me.Tp3.TabIndex = 2
        Me.Tp3.Text = "Not Show A/C Group"
        Me.Tp3.UseVisualStyleBackColor = True
        '
        'Pnl3
        '
        Me.Pnl3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Pnl3.Location = New System.Drawing.Point(64, 23)
        Me.Pnl3.Name = "Pnl3"
        Me.Pnl3.Size = New System.Drawing.Size(410, 207)
        Me.Pnl3.TabIndex = 16
        '
        'LblAffect_FA
        '
        Me.LblAffect_FA.AutoSize = True
        Me.LblAffect_FA.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAffect_FA.Location = New System.Drawing.Point(463, 250)
        Me.LblAffect_FA.Name = "LblAffect_FA"
        Me.LblAffect_FA.Size = New System.Drawing.Size(93, 13)
        Me.LblAffect_FA.TabIndex = 246
        Me.LblAffect_FA.Text = "Is FA Affected?"
        Me.LblAffect_FA.Visible = False
        '
        'TxtAffect_FA
        '
        Me.TxtAffect_FA.AgAllowUserToEnableMasterHelp = False
        Me.TxtAffect_FA.AgLastValueTag = Nothing
        Me.TxtAffect_FA.AgLastValueText = Nothing
        Me.TxtAffect_FA.AgMandatory = False
        Me.TxtAffect_FA.AgMasterHelp = False
        Me.TxtAffect_FA.AgNumberLeftPlaces = 0
        Me.TxtAffect_FA.AgNumberNegetiveAllow = False
        Me.TxtAffect_FA.AgNumberRightPlaces = 0
        Me.TxtAffect_FA.AgPickFromLastValue = False
        Me.TxtAffect_FA.AgRowFilter = ""
        Me.TxtAffect_FA.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAffect_FA.AgSelectedValue = Nothing
        Me.TxtAffect_FA.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAffect_FA.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtAffect_FA.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAffect_FA.Location = New System.Drawing.Point(615, 245)
        Me.TxtAffect_FA.MaxLength = 20
        Me.TxtAffect_FA.Name = "TxtAffect_FA"
        Me.TxtAffect_FA.Size = New System.Drawing.Size(100, 21)
        Me.TxtAffect_FA.TabIndex = 14
        Me.TxtAffect_FA.Text = "TxtAffect_FA"
        Me.TxtAffect_FA.Visible = False
        '
        'TxtNarration
        '
        Me.TxtNarration.AgAllowUserToEnableMasterHelp = False
        Me.TxtNarration.AgLastValueTag = Nothing
        Me.TxtNarration.AgLastValueText = Nothing
        Me.TxtNarration.AgMandatory = False
        Me.TxtNarration.AgMasterHelp = False
        Me.TxtNarration.AgNumberLeftPlaces = 0
        Me.TxtNarration.AgNumberNegetiveAllow = False
        Me.TxtNarration.AgNumberRightPlaces = 0
        Me.TxtNarration.AgPickFromLastValue = False
        Me.TxtNarration.AgRowFilter = ""
        Me.TxtNarration.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtNarration.AgSelectedValue = Nothing
        Me.TxtNarration.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtNarration.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtNarration.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNarration.Location = New System.Drawing.Point(315, 202)
        Me.TxtNarration.MaxLength = 255
        Me.TxtNarration.Name = "TxtNarration"
        Me.TxtNarration.Size = New System.Drawing.Size(400, 21)
        Me.TxtNarration.TabIndex = 11
        '
        'LblNarration
        '
        Me.LblNarration.AutoSize = True
        Me.LblNarration.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNarration.Location = New System.Drawing.Point(158, 206)
        Me.LblNarration.Name = "LblNarration"
        Me.LblNarration.Size = New System.Drawing.Size(116, 13)
        Me.LblNarration.TabIndex = 249
        Me.LblNarration.Text = "Common Narration"
        '
        'LblSystemDefine
        '
        Me.LblSystemDefine.AutoSize = True
        Me.LblSystemDefine.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSystemDefine.ForeColor = System.Drawing.Color.Blue
        Me.LblSystemDefine.Location = New System.Drawing.Point(721, 73)
        Me.LblSystemDefine.Name = "LblSystemDefine"
        Me.LblSystemDefine.Size = New System.Drawing.Size(101, 13)
        Me.LblSystemDefine.TabIndex = 250
        Me.LblSystemDefine.Text = "System Define"
        '
        'LblIsShowVoucherReferenceReq
        '
        Me.LblIsShowVoucherReferenceReq.AutoSize = True
        Me.LblIsShowVoucherReferenceReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblIsShowVoucherReferenceReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblIsShowVoucherReferenceReq.Location = New System.Drawing.Point(303, 253)
        Me.LblIsShowVoucherReferenceReq.Name = "LblIsShowVoucherReferenceReq"
        Me.LblIsShowVoucherReferenceReq.Size = New System.Drawing.Size(10, 7)
        Me.LblIsShowVoucherReferenceReq.TabIndex = 253
        Me.LblIsShowVoucherReferenceReq.Text = "Ä"
        '
        'LblIsShowVoucherReference
        '
        Me.LblIsShowVoucherReference.AutoSize = True
        Me.LblIsShowVoucherReference.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIsShowVoucherReference.Location = New System.Drawing.Point(158, 248)
        Me.LblIsShowVoucherReference.Name = "LblIsShowVoucherReference"
        Me.LblIsShowVoucherReference.Size = New System.Drawing.Size(143, 13)
        Me.LblIsShowVoucherReference.TabIndex = 252
        Me.LblIsShowVoucherReference.Text = "Show Reference Detail?"
        '
        'TxtIsShowVoucherReference
        '
        Me.TxtIsShowVoucherReference.AgAllowUserToEnableMasterHelp = False
        Me.TxtIsShowVoucherReference.AgLastValueTag = Nothing
        Me.TxtIsShowVoucherReference.AgLastValueText = Nothing
        Me.TxtIsShowVoucherReference.AgMandatory = True
        Me.TxtIsShowVoucherReference.AgMasterHelp = False
        Me.TxtIsShowVoucherReference.AgNumberLeftPlaces = 0
        Me.TxtIsShowVoucherReference.AgNumberNegetiveAllow = False
        Me.TxtIsShowVoucherReference.AgNumberRightPlaces = 0
        Me.TxtIsShowVoucherReference.AgPickFromLastValue = False
        Me.TxtIsShowVoucherReference.AgRowFilter = ""
        Me.TxtIsShowVoucherReference.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIsShowVoucherReference.AgSelectedValue = Nothing
        Me.TxtIsShowVoucherReference.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIsShowVoucherReference.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtIsShowVoucherReference.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIsShowVoucherReference.Location = New System.Drawing.Point(315, 245)
        Me.TxtIsShowVoucherReference.MaxLength = 20
        Me.TxtIsShowVoucherReference.Name = "TxtIsShowVoucherReference"
        Me.TxtIsShowVoucherReference.Size = New System.Drawing.Size(100, 21)
        Me.TxtIsShowVoucherReference.TabIndex = 13
        '
        'TxtNCat
        '
        Me.TxtNCat.AgAllowUserToEnableMasterHelp = False
        Me.TxtNCat.AgLastValueTag = Nothing
        Me.TxtNCat.AgLastValueText = Nothing
        Me.TxtNCat.AgMandatory = True
        Me.TxtNCat.AgMasterHelp = False
        Me.TxtNCat.AgNumberLeftPlaces = 0
        Me.TxtNCat.AgNumberNegetiveAllow = False
        Me.TxtNCat.AgNumberRightPlaces = 0
        Me.TxtNCat.AgPickFromLastValue = False
        Me.TxtNCat.AgRowFilter = ""
        Me.TxtNCat.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtNCat.AgSelectedValue = Nothing
        Me.TxtNCat.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtNCat.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtNCat.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNCat.Location = New System.Drawing.Point(615, 114)
        Me.TxtNCat.MaxLength = 5
        Me.TxtNCat.Name = "TxtNCat"
        Me.TxtNCat.Size = New System.Drawing.Size(100, 21)
        Me.TxtNCat.TabIndex = 256
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(158, 228)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 13)
        Me.Label3.TabIndex = 258
        Me.Label3.Text = "Menu Name"
        '
        'TxtMenuName
        '
        Me.TxtMenuName.AgAllowUserToEnableMasterHelp = False
        Me.TxtMenuName.AgLastValueTag = Nothing
        Me.TxtMenuName.AgLastValueText = Nothing
        Me.TxtMenuName.AgMandatory = False
        Me.TxtMenuName.AgMasterHelp = False
        Me.TxtMenuName.AgNumberLeftPlaces = 0
        Me.TxtMenuName.AgNumberNegetiveAllow = False
        Me.TxtMenuName.AgNumberRightPlaces = 0
        Me.TxtMenuName.AgPickFromLastValue = False
        Me.TxtMenuName.AgRowFilter = ""
        Me.TxtMenuName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtMenuName.AgSelectedValue = Nothing
        Me.TxtMenuName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtMenuName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtMenuName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMenuName.Location = New System.Drawing.Point(315, 224)
        Me.TxtMenuName.MaxLength = 255
        Me.TxtMenuName.Name = "TxtMenuName"
        Me.TxtMenuName.Size = New System.Drawing.Size(400, 21)
        Me.TxtMenuName.TabIndex = 12
        '
        'TxtModule
        '
        Me.TxtModule.AgAllowUserToEnableMasterHelp = False
        Me.TxtModule.AgLastValueTag = Nothing
        Me.TxtModule.AgLastValueText = Nothing
        Me.TxtModule.AgMandatory = False
        Me.TxtModule.AgMasterHelp = False
        Me.TxtModule.AgNumberLeftPlaces = 0
        Me.TxtModule.AgNumberNegetiveAllow = False
        Me.TxtModule.AgNumberRightPlaces = 0
        Me.TxtModule.AgPickFromLastValue = False
        Me.TxtModule.AgRowFilter = ""
        Me.TxtModule.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtModule.AgSelectedValue = Nothing
        Me.TxtModule.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtModule.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtModule.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtModule.Location = New System.Drawing.Point(726, 225)
        Me.TxtModule.MaxLength = 255
        Me.TxtModule.Name = "TxtModule"
        Me.TxtModule.Size = New System.Drawing.Size(98, 21)
        Me.TxtModule.TabIndex = 259
        Me.TxtModule.Visible = False
        '
        'FrmVoucherTypeMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(872, 616)
        Me.Controls.Add(Me.TxtModule)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtMenuName)
        Me.Controls.Add(Me.TxtNCat)
        Me.Controls.Add(Me.LblIsShowVoucherReferenceReq)
        Me.Controls.Add(Me.LblIsShowVoucherReference)
        Me.Controls.Add(Me.TxtIsShowVoucherReference)
        Me.Controls.Add(Me.LblSystemDefine)
        Me.Controls.Add(Me.LblNarration)
        Me.Controls.Add(Me.TxtNarration)
        Me.Controls.Add(Me.LblAffect_FA)
        Me.Controls.Add(Me.TxtAffect_FA)
        Me.Controls.Add(Me.Tc1)
        Me.Controls.Add(Me.LblClgDtReq)
        Me.Controls.Add(Me.LblClgDt)
        Me.Controls.Add(Me.TxtClgDt)
        Me.Controls.Add(Me.LblChqDtReq)
        Me.Controls.Add(Me.LblChqDate)
        Me.Controls.Add(Me.TxtChqDate)
        Me.Controls.Add(Me.LblChqNoReq)
        Me.Controls.Add(Me.LblChqNo)
        Me.Controls.Add(Me.TxtChqNo)
        Me.Controls.Add(Me.LblCommon_NarrReq)
        Me.Controls.Add(Me.LblCommon_Narr)
        Me.Controls.Add(Me.TxtCommon_Narr)
        Me.Controls.Add(Me.LblSaperate_NarrReq)
        Me.Controls.Add(Me.LblSaperate_Narr)
        Me.Controls.Add(Me.TxtSaperate_Narr)
        Me.Controls.Add(Me.LblNumber_MethodReq)
        Me.Controls.Add(Me.LblNumber_Method)
        Me.Controls.Add(Me.TxtNumber_Method)
        Me.Controls.Add(Me.LblNCatReq)
        Me.Controls.Add(Me.LblNCat)
        Me.Controls.Add(Me.LblCategoryReq)
        Me.Controls.Add(Me.LblCategory)
        Me.Controls.Add(Me.TxtCategory)
        Me.Controls.Add(Me.LblShort_NameReq)
        Me.Controls.Add(Me.LblShort_Name)
        Me.Controls.Add(Me.TxtShort_Name)
        Me.Controls.Add(Me.LblDescriptionReq)
        Me.Controls.Add(Me.LblDescription)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.LblV_TypeReq)
        Me.Controls.Add(Me.LblV_Type)
        Me.Controls.Add(Me.TxtV_Type)
        Me.Controls.Add(Me.Topctrl1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmVoucherTypeMaster"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "Voucher Type Master"
        Me.Tc1.ResumeLayout(False)
        Me.Tp1.ResumeLayout(False)
        Me.Tp2.ResumeLayout(False)
        Me.Tp3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Topctrl1 As Topctrl.Topctrl
    Friend WithEvents LblV_Type As System.Windows.Forms.Label
    Friend WithEvents TxtV_Type As AgControls.AgTextBox
    Friend WithEvents LblV_TypeReq As System.Windows.Forms.Label
    Friend WithEvents LblDescriptionReq As System.Windows.Forms.Label
    Friend WithEvents LblDescription As System.Windows.Forms.Label
    Friend WithEvents TxtDescription As AgControls.AgTextBox
    Friend WithEvents LblShort_NameReq As System.Windows.Forms.Label
    Friend WithEvents LblShort_Name As System.Windows.Forms.Label
    Friend WithEvents TxtShort_Name As AgControls.AgTextBox
    Friend WithEvents LblCategoryReq As System.Windows.Forms.Label
    Friend WithEvents LblCategory As System.Windows.Forms.Label
    Friend WithEvents TxtCategory As AgControls.AgTextBox
    Friend WithEvents LblNCatReq As System.Windows.Forms.Label
    Friend WithEvents LblNCat As System.Windows.Forms.Label
    Friend WithEvents LblNumber_MethodReq As System.Windows.Forms.Label
    Friend WithEvents LblNumber_Method As System.Windows.Forms.Label
    Friend WithEvents TxtNumber_Method As AgControls.AgTextBox
    Friend WithEvents LblSaperate_NarrReq As System.Windows.Forms.Label
    Friend WithEvents LblSaperate_Narr As System.Windows.Forms.Label
    Friend WithEvents TxtSaperate_Narr As AgControls.AgTextBox
    Friend WithEvents LblCommon_NarrReq As System.Windows.Forms.Label
    Friend WithEvents LblCommon_Narr As System.Windows.Forms.Label
    Friend WithEvents TxtCommon_Narr As AgControls.AgTextBox
    Friend WithEvents LblChqNoReq As System.Windows.Forms.Label
    Friend WithEvents LblChqNo As System.Windows.Forms.Label
    Friend WithEvents TxtChqNo As AgControls.AgTextBox
    Friend WithEvents LblChqDtReq As System.Windows.Forms.Label
    Friend WithEvents LblChqDate As System.Windows.Forms.Label
    Friend WithEvents TxtChqDate As AgControls.AgTextBox
    Friend WithEvents LblClgDtReq As System.Windows.Forms.Label
    Friend WithEvents LblClgDt As System.Windows.Forms.Label
    Friend WithEvents TxtClgDt As AgControls.AgTextBox
    Friend WithEvents Tc1 As System.Windows.Forms.TabControl
    Friend WithEvents Tp1 As System.Windows.Forms.TabPage
    Friend WithEvents Pnl1 As System.Windows.Forms.Panel
    Friend WithEvents Tp2 As System.Windows.Forms.TabPage
    Friend WithEvents Pnl2 As System.Windows.Forms.Panel
    Friend WithEvents Tp3 As System.Windows.Forms.TabPage
    Friend WithEvents Pnl3 As System.Windows.Forms.Panel
    Friend WithEvents LblAffect_FA As System.Windows.Forms.Label
    Friend WithEvents TxtAffect_FA As AgControls.AgTextBox
    Friend WithEvents TxtNarration As AgControls.AgTextBox
    Friend WithEvents LblNarration As System.Windows.Forms.Label
    Friend WithEvents LblSystemDefine As System.Windows.Forms.Label
    Friend WithEvents LblIsShowVoucherReferenceReq As System.Windows.Forms.Label
    Friend WithEvents LblIsShowVoucherReference As System.Windows.Forms.Label
    Friend WithEvents TxtIsShowVoucherReference As AgControls.AgTextBox
    Friend WithEvents TxtNCat As AgControls.AgTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TxtMenuName As AgControls.AgTextBox
    Friend WithEvents TxtModule As AgControls.AgTextBox
End Class
