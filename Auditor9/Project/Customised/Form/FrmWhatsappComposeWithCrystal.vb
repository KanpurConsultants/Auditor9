Imports CrystalDecisions.CrystalReports.Engine
Imports System.Drawing.Printing.PrinterSettings
Imports System.IO
Imports Microsoft.Reporting.WinForms
Imports CrystalDecisions.Shared
Imports System.Linq
Imports System.Text
Imports System.Net
Imports Customised.ClsMain
Public Class FrmWhatsappComposeWithCrystal
    Inherits System.Windows.Forms.Form
    Dim mRepObj As New ReportDocument

    Dim mReportTitle As String = "", mReportSubTitle As String = ""
    Dim mAttachmentName As String = ""
    Dim mAttachmentSaveFolderName As String = "EMail"
    Private RequestUrl As String = FGetSettings(SettingFields.WhatsappRequestUrl, "E Invoice", "", "", "", "", "", "", "")
    Private Username As String = FGetSettings(SettingFields.WhatsappUsername, "E Invoice", "", "", "", "", "", "", "")
    Private Password As String = FGetSettings(SettingFields.WhatsappPassword, "E Invoice", "", "", "", "", "", "", "")

    'Dim RequestUrl As String = "http://app.laksmartindia.com/api/v1/message/create"
    'Dim Username As String = "Satyam Tripathi"
    'Dim Password As String = "KC@12345"

    Dim mSearchCode As String = ""
    Friend WithEvents BtnTo As Button
    Public WithEvents LblToEmail As Label
    Public WithEvents TxtToMobile As AgControls.AgTextBox
    Dim AgL As AgLibrary.ClsMain
    Public Property SearchCode() As String
        Get
            Return mSearchCode
        End Get
        Set(ByVal value As String)
            mSearchCode = value
        End Set
    End Property

#Region " Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private WithEvents CrvReport As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents GroupBox5 As GroupBox
    Public WithEvents Label1 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents GroupBox4 As GroupBox
    Public WithEvents TxtMessage As AgControls.AgTextBox
    Friend WithEvents BtnSend As Button
    Friend WithEvents GroupBox8 As GroupBox
    Friend WithEvents GroupBox9 As GroupBox

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmWhatsappComposeWithCrystal))
        Me.CrvReport = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.TxtMessage = New AgControls.AgTextBox()
        Me.BtnSend = New System.Windows.Forms.Button()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.BtnTo = New System.Windows.Forms.Button()
        Me.LblToEmail = New System.Windows.Forms.Label()
        Me.TxtToMobile = New AgControls.AgTextBox()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.SuspendLayout()
        '
        'CrvReport
        '
        Me.CrvReport.ActiveViewIndex = -1
        Me.CrvReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrvReport.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.CrvReport.DisplayGroupTree = False
        Me.CrvReport.Location = New System.Drawing.Point(475, 1)
        Me.CrvReport.Name = "CrvReport"
        Me.CrvReport.SelectionFormula = ""
        Me.CrvReport.Size = New System.Drawing.Size(500, 610)
        Me.CrvReport.TabIndex = 0
        Me.CrvReport.ViewTimeSelectionFormula = ""
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.GroupBox7)
        Me.GroupBox6.Location = New System.Drawing.Point(2, 124)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(421, 2)
        Me.GroupBox6.TabIndex = 915
        Me.GroupBox6.TabStop = False
        '
        'GroupBox7
        '
        Me.GroupBox7.Location = New System.Drawing.Point(0, 39)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(415, 10)
        Me.GroupBox7.TabIndex = 885
        Me.GroupBox7.TabStop = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Location = New System.Drawing.Point(449, -6)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(3, 632)
        Me.GroupBox5.TabIndex = 914
        Me.GroupBox5.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(6, 99)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 16)
        Me.Label1.TabIndex = 913
        Me.Label1.Text = "Message"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.GroupBox4)
        Me.GroupBox3.Location = New System.Drawing.Point(0, 66)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(421, 2)
        Me.GroupBox3.TabIndex = 912
        Me.GroupBox3.TabStop = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Location = New System.Drawing.Point(0, 39)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(415, 10)
        Me.GroupBox4.TabIndex = 885
        Me.GroupBox4.TabStop = False
        '
        'TxtMessage
        '
        Me.TxtMessage.AgAllowUserToEnableMasterHelp = False
        Me.TxtMessage.AgLastValueTag = Nothing
        Me.TxtMessage.AgLastValueText = Nothing
        Me.TxtMessage.AgMandatory = False
        Me.TxtMessage.AgMasterHelp = False
        Me.TxtMessage.AgNumberLeftPlaces = 0
        Me.TxtMessage.AgNumberNegetiveAllow = False
        Me.TxtMessage.AgNumberRightPlaces = 0
        Me.TxtMessage.AgPickFromLastValue = False
        Me.TxtMessage.AgRowFilter = ""
        Me.TxtMessage.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtMessage.AgSelectedValue = Nothing
        Me.TxtMessage.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtMessage.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtMessage.BackColor = System.Drawing.Color.White
        Me.TxtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtMessage.Font = New System.Drawing.Font("Verdana", 11.25!)
        Me.TxtMessage.Location = New System.Drawing.Point(6, 143)
        Me.TxtMessage.MaxLength = 0
        Me.TxtMessage.Multiline = True
        Me.TxtMessage.Name = "TxtMessage"
        Me.TxtMessage.Size = New System.Drawing.Size(415, 418)
        Me.TxtMessage.TabIndex = 908
        '
        'BtnSend
        '
        Me.BtnSend.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnSend.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnSend.ForeColor = System.Drawing.Color.White
        Me.BtnSend.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BtnSend.Location = New System.Drawing.Point(343, 583)
        Me.BtnSend.Name = "BtnSend"
        Me.BtnSend.Size = New System.Drawing.Size(80, 28)
        Me.BtnSend.TabIndex = 920
        Me.BtnSend.Text = "Send"
        Me.BtnSend.UseVisualStyleBackColor = False
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.GroupBox9)
        Me.GroupBox8.Location = New System.Drawing.Point(-4, 575)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(421, 2)
        Me.GroupBox8.TabIndex = 921
        Me.GroupBox8.TabStop = False
        '
        'GroupBox9
        '
        Me.GroupBox9.Location = New System.Drawing.Point(0, 39)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(415, 10)
        Me.GroupBox9.TabIndex = 885
        Me.GroupBox9.TabStop = False
        '
        'BtnTo
        '
        Me.BtnTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnTo.BackColor = System.Drawing.Color.Transparent
        Me.BtnTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnTo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnTo.ForeColor = System.Drawing.Color.White
        Me.BtnTo.Image = CType(resources.GetObject("BtnTo.Image"), System.Drawing.Image)
        Me.BtnTo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BtnTo.Location = New System.Drawing.Point(393, 32)
        Me.BtnTo.Name = "BtnTo"
        Me.BtnTo.Size = New System.Drawing.Size(31, 28)
        Me.BtnTo.TabIndex = 925
        Me.BtnTo.UseVisualStyleBackColor = False
        '
        'LblToEmail
        '
        Me.LblToEmail.AutoSize = True
        Me.LblToEmail.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LblToEmail.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LblToEmail.Location = New System.Drawing.Point(13, 39)
        Me.LblToEmail.Name = "LblToEmail"
        Me.LblToEmail.Size = New System.Drawing.Size(78, 16)
        Me.LblToEmail.TabIndex = 924
        Me.LblToEmail.Text = "Mobile No"
        '
        'TxtToMobile
        '
        Me.TxtToMobile.AgAllowUserToEnableMasterHelp = False
        Me.TxtToMobile.AgLastValueTag = Nothing
        Me.TxtToMobile.AgLastValueText = Nothing
        Me.TxtToMobile.AgMandatory = False
        Me.TxtToMobile.AgMasterHelp = False
        Me.TxtToMobile.AgNumberLeftPlaces = 0
        Me.TxtToMobile.AgNumberNegetiveAllow = False
        Me.TxtToMobile.AgNumberRightPlaces = 0
        Me.TxtToMobile.AgPickFromLastValue = False
        Me.TxtToMobile.AgRowFilter = ""
        Me.TxtToMobile.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtToMobile.AgSelectedValue = Nothing
        Me.TxtToMobile.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtToMobile.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtToMobile.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TxtToMobile.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtToMobile.Font = New System.Drawing.Font("Verdana", 11.25!)
        Me.TxtToMobile.Location = New System.Drawing.Point(114, 38)
        Me.TxtToMobile.MaxLength = 0
        Me.TxtToMobile.Name = "TxtToMobile"
        Me.TxtToMobile.Size = New System.Drawing.Size(273, 19)
        Me.TxtToMobile.TabIndex = 923
        '
        'FrmWhatsapp
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(970, 611)
        Me.Controls.Add(Me.BtnTo)
        Me.Controls.Add(Me.LblToEmail)
        Me.Controls.Add(Me.TxtToMobile)
        Me.Controls.Add(Me.GroupBox8)
        Me.Controls.Add(Me.BtnSend)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.TxtMessage)
        Me.Controls.Add(Me.CrvReport)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmWhatsapp"
        Me.Text = "Whatsapp"
        Me.TopMost = True
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    Public WriteOnly Property RepObj()
        Set(ByVal Value)
            CrvReport.ReportSource = Value
            mRepObj = Value
        End Set
    End Property
    Public Sub New(ByVal AgLibVar As AgLibrary.ClsMain)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        AgL = AgLibVar
    End Sub
    Public Property AttachmentName() As String
        Get
            AttachmentName = mAttachmentName
        End Get
        Set(ByVal value As String)
            mAttachmentName = value
        End Set
    End Property
    Public Property ReportTitle() As String
        Get
            ReportTitle = mReportTitle
        End Get
        Set(ByVal value As String)
            mReportTitle = value
        End Set
    End Property
    Public Property ReportSubTitle() As String
        Get
            ReportSubTitle = mReportSubTitle
        End Get
        Set(ByVal value As String)
            mReportSubTitle = value
        End Set
    End Property

    Private Sub RepView_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub
    Private Sub BtnPrintSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim PrintDialog1 As New PrintDialog()

        PrintDialog1.AllowCurrentPage = True
        PrintDialog1.AllowSomePages = True


        Dim result As DialogResult = PrintDialog1.ShowDialog()
        If (result = Windows.Forms.DialogResult.OK) Then
            mRepObj.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName
            If PrintDialog1.PrinterSettings.DefaultPageSettings.Landscape = True Then
                mRepObj.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape
            Else
                mRepObj.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            End If
            Select Case PrintDialog1.PrinterSettings.DefaultPageSettings.PaperSize.PaperName
                'Case "A2" ' A2 paper (420 mm by 594 mm).  
                Case "A3" ' A3 paper (297 mm by 420 mm).  
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA3
                ' '' ''Case "A3Extra" ' A3 extra paper (322 mm by 445 mm).  
                ' '' ''Case "A3ExtraTransverse" ' A3 extra transverse paper (322 mm by 445 mm).  "
                ' '' ''Case "A3Rotated" ' A3 rotated paper (420 mm by 297 mm).  
                Case "A3Transverse" ' A3 transverse paper (297 mm by 420 mm).  
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA3
                Case "A4" ' A4 paper (210 mm by 297 mm).  
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4
                ' '' ''Case "A4Extra" ' A4 extra paper (236 mm by 322 mm). This value is specific to the PostScript driver and is used only by Linotronic printers to help save paper.  
                ' '' ''Case "A4Plus" ' A4 plus paper (210 mm by 330 mm).  
                ' '' ''Case "A4Rotated" ' A4 rotated paper (297 mm by 210 mm). Requires Windows 98, Windows NT 4.0, or later.  "
                Case "A4Small" ' A4 small paper (210 mm by 297 mm).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4Small
                ' '' ''Case "A4Transverse" ' A4 transverse paper (210 mm by 297 mm).  "
                Case "A5" ' A5 paper (148 mm by 210 mm).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA5
                '' '' ''Case "A5Extra" ' A5 extra paper (174 mm by 235 mm).  "
                '' '' ''Case "A5Rotated" ' A5 rotated paper (210 mm by 148 mm). Requires Windows 98, Windows NT 4.0, or later.  "
                '' '' ''Case "A5Transverse" ' A5 transverse paper (148 mm by 210 mm).  "
                '' '' ''Case "A6" ' A6 paper (105 mm by 148 mm). Requires Windows 98, Windows NT 4.0, or later.  "
                '' '' ''Case "A6Rotated" ' A6 rotated paper (148 mm by 105 mm). Requires Windows 98, Windows NT 4.0, or later.  "
                '' '' ''Case "APlus" ' SuperA/SuperA/A4 paper (227 mm by 356 mm).  "
                '' '' ''Case "B4" ' B4 paper (250 mm by 353 mm).  "
                '' '' ''Case "B4Envelope" ' B4 envelope (250 mm by 353 mm).  "
                '' '' ''Case "B4JisRotated" ' JIS B4 rotated paper (364 mm by 257 mm). Requires Windows 98, Windows NT 4.0, or later.  "
                '' '' ''Case "B5" ' B5 paper (176 mm by 250 mm).  "
                '' '' ''Case "B5Envelope" ' B5 envelope (176 mm by 250 mm).  "
                '' '' ''Case "B5Extra" ' ISO B5 extra paper (201 mm by 276 mm).  "
                '' '' ''Case "B5JisRotated" ' JIS B5 rotated paper (257 mm by 182 mm). Requires Windows 98, Windows NT 4.0, or later.  "
                '' '' ''Case "B5Transverse" ' JIS B5 transverse paper (182 mm by 257 mm).  "
                '' '' ''Case "B6Envelope" ' B6 envelope (176 mm by 125 mm).  "
                '' '' ''Case "B6Jis" ' JIS B6 paper (128 mm by 182 mm). Requires Windows 98, Windows NT 4.0, or later.  "
                '' '' ''Case "B6JisRotated" ' JIS B6 rotated paper (182 mm by 128 mm). Requires Windows 98, Windows NT 4.0, or later.  "
                '' '' ''Case "BPlus" ' SuperB/SuperB/A3 paper (305 mm by 487 mm).  "
                Case "C3Envelope" ' C3 envelope (324 mm by 458 mm).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperEnvelopeC3
                Case "C4Envelope" ' C4 envelope (229 mm by 324 mm).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperEnvelopeC4
                Case "C5Envelope" ' C5 envelope (162 mm by 229 mm).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperEnvelopeC5
                Case "C65Envelope" ' C65 envelope (114 mm by 229 mm).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperEnvelopeC65
                Case "C6Envelope" ' C6 envelope (114 mm by 162 mm).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperEnvelopeC6
                Case "CSheet" ' C paper (17 in. by 22 in.).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperCsheet
                Case "Custom" ' The paper size is defined by the user.  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                Case "DLEnvelope" ' DL envelope (110 mm by 220 mm).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperEnvelopeDL
                Case "DSheet" ' D paper (22 in. by 34 in.).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperDsheet
                Case "ESheet" ' E paper (34 in. by 44 in.).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperEsheet
                Case "Executive" ' Executive paper (7.25 in. by 10.5 in.).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperExecutive
                Case "Folio" ' Folio paper (8.5 in. by 13 in.).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperFolio
                Case "GermanLegalFanfold" ' German legal fanfold (8.5 in. by 13 in.).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperFanfoldLegalGerman
                Case "GermanStandardFanfold" ' German standard fanfold (8.5 in. by 12 in.).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperFanfoldStdGerman
                ' '' ''Case "InviteEnvelope" ' Invitation envelope (220 mm by 220 mm).  "
                ' '' ''Case "IsoB4" ' ISO B4 (250 mm by 353 mm).  "
                Case "Ledger" ' Ledger paper (17 in. by 11 in.).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLedger
                Case "Legal" ' Legal paper (8.5 in. by 14 in.).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLegal
                ' '' ''Case "LegalExtra" ' Legal extra paper (9.275 in. by 15 in.). This value is specific to the PostScript driver and is used only by Linotronic printers in order to conserve paper.  "
                Case "Letter" ' Letter paper (8.5 in. by 11 in.).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLetter
                ' '' ''Case "LetterExtra" ' Letter extra paper (9.275 in. by 12 in.). This value is specific to the PostScript driver and is used only by Linotronic printers in order to conserve paper.  "
                ' '' ''Case "LetterExtraTransverse " 'Letter extra transverse paper (9.275 in. by 12 in.).  "
                ' '' ''Case "LetterPlus" ' Letter plus paper (8.5 in. by 12.69 in.).  "
                ' '' ''Case "LetterRotated" ' Letter rotated paper (11 in. by 8.5 in.).  "
                Case "LetterSmall" ' Letter small paper (8.5 in. by 11 in.).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLetterSmall
                ' '' ''Case "LetterTransverse" ' Letter transverse paper (8.275 in. by 11 in.).  "
                ' '' ''Case "MonarchEnvelope" ' Monarch envelope (3.875 in. by 7.5 in.).  "
                Case "Note" ' Note paper (8.5 in. by 11 in.).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperNote
                ' '' ''Case "Number10Envelope" ' #10 envelope (4.125 in. by 9.5 in.).  "
                ' '' ''Case "Number11Envelope" ' #11 envelope (4.5 in. by 10.375 in.).  "
                ' '' ''Case "Number12Envelope" ' #12 envelope (4.75 in. by 11 in.).  "
                ' '' ''Case "Number14Envelope" ' #14 envelope (5 in. by 11.5 in.).  "
                ' '' ''Case "Number9Envelope" ' #9 envelope (3.875 in. by 8.875 in.).  "
                ' '' ''Case "PersonalEnvelope" ' 6 3/4 envelope (3.625 in. by 6.5 in.).  "
                Case "Quarto" ' Quarto paper (215 mm by 275 mm).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperQuarto
                ' '' ''Case "Standard10x11" ' Standard paper (10 in. by 11 in.).  "
                Case "Standard10x14" ' Standard paper (10 in. by 14 in.).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.Paper10x14
                Case "Standard11x17" ' Standard paper (11 in. by 17 in.).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.Paper11x17
                ' '' ''Case "Standard12x11" ' Standard paper (12 in. by 11 in.). Requires Windows 98, Windows NT 4.0, or later.  "
                ' '' ''Case "Standard15x11" ' Standard paper (15 in. by 11 in.).  "
                ' '' ''Case "Standard9x11" ' Standard paper (9 in. by 11 in.).  "
                Case "Statement" ' Statement paper (5.5 in. by 8.5 in.).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperStatement
                Case "Tabloid" ' Tabloid paper (11 in. by 17 in.).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperTabloid
                Case "USStandardFanfold" ' US standard fanfold (14.875 in. by 11 in.).  "
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperFanfoldUS
                Case Else
                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
            End Select

            CrvReport.ReportSource = mRepObj
            mRepObj.PrintToPrinter(PrintDialog1.PrinterSettings.Copies, PrintDialog1.PrinterSettings.Collate, PrintDialog1.PrinterSettings.FromPage, PrintDialog1.PrinterSettings.ToPage)
        End If
    End Sub
    Private Sub FrmWhatsapp_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Location = New System.Drawing.Point(0, 0)
        CrvReport.EnableDrillDown = False
        CrvReport.Zoom(1)
    End Sub
    Private Sub FrmReportPrint_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub BtnSend_Click(sender As Object, e As EventArgs) Handles BtnSend.Click
        MsgBox(FSendWhatsapp(), MsgBoxStyle.Information)
    End Sub

    Private Sub BtnTo_Click(sender As Object, e As EventArgs) Handles BtnTo.Click
        Select Case sender.Name
            Case BtnTo.Name
                If TxtToMobile.Text <> "" Then
                    TxtToMobile.Text = TxtToMobile.Text + "," + FHPGD_PhoneContacts()
                Else
                    TxtToMobile.Text = FHPGD_PhoneContacts()
                End If
        End Select
    End Sub
    Private Function FHPGD_PhoneContacts() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtTemp As DataTable
        Dim mQry As String = ""

        mQry = " Select 'o' As Tick, Sg.SubCode As SearchKey, Sg.Name, C.CityName, Ag.GroupName, IfNull(Sg.Mobile,Sg.Phone) 
                From SubGroup Sg
                Left Join City C On Sg.CityCode = C.CityCode
                Left Join AcGroup AG On Sg.GroupCode = Ag.GroupCode
                Where IfNull(Sg.Mobile,Sg.Phone) Is Not Null And IfNull(Sg.Mobile,Sg.Phone) <> '' 
                Order By Sg.Name, C.CityName"
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTemp.Rows.Count = 0 Then
            Exit Function
        End If

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 400, 800, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Name", 280, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "City", 130, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Ac Group", 130, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(5, "Mobile", 130, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(5, "", "", ",", True)
        End If
        FHPGD_PhoneContacts = StrRtn

        FRH_Multiple = Nothing
    End Function
    Public Function FSendWhatsapp()
        Dim mQry As String = ""
        Dim DtTemp As DataTable = Nothing
        If TxtToMobile.Text.ToString.Replace(",", "") = "" Then
            FSendWhatsapp = "Invalid Mobile No"
            Exit Function
        End If
        Try
            Dim MobileNoList As String = TxtToMobile.Text
            Dim FileName As String = ""
            FileName = mAttachmentName + ".pdf"
            Dim Message As String = TxtMessage.Text.Replace(vbCrLf, "\n").Replace(vbLf, "\n")
            FSendWhatsapp = SendPDFByWhatsapp(MobileNoList, Message, FileName)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Public Function SendPDFByWhatsapp(receiverMobileNo As String, message As String, FileName As String) As String
        'Dim url As String = "http://app.laksmartindia.com/api/v1/message/create"
        'Dim username As String = "Satyam Tripathi"
        'Dim password As String = "KC@12345"


        ' 1. Combine username and password
        Dim authString As String = Username & ":" & Password

        ' 2. Convert to base64
        Dim authBytes As Byte() = Encoding.UTF8.GetBytes(authString)
        Dim authBase64 As String = Convert.ToBase64String(authBytes)

        Dim MS As MemoryStream = CType((CType(CrvReport.ReportSource, ReportDocument).ExportToStream(ExportFormatType.PortableDocFormat)), MemoryStream)
        Dim base64Body As String = Convert.ToBase64String(MS.ToArray())

        Dim json As String = "{
  ""receiverMobileNo"": ""+91" & receiverMobileNo & """,
  ""message"": [
    """ & message & """
  ],
  ""base64File"": [
    {
      ""name"": """ & FileName & """,
      ""body"": """ & base64Body & """
    }
  ]
}"

        Try
            Dim request As HttpWebRequest = CType(System.Net.WebRequest.Create(RequestUrl), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"
            request.Headers.Add("Authorization", "Basic " & authBase64)
            request.Accept = "application/json"

            ' Convert JSON to byte array
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(json)
            request.ContentLength = bytes.Length

            ' Write request body
            Using stream As Stream = request.GetRequestStream()
                stream.Write(bytes, 0, bytes.Length)
            End Using

            ' Get the response
            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            Using reader As New StreamReader(response.GetResponseStream())
                Dim responseText As String = reader.ReadToEnd()
                Console.WriteLine("Response: " & responseText)
            End Using
            Return ("Whatsapp Send Sucessfully !")
        Catch ex As WebException
            Console.WriteLine("Error: " & ex.Message)
            Return ("Server says: " & ex.Message)
            ' Optional: print server error response if any
            If ex.Response IsNot Nothing Then
                Using reader As New StreamReader(ex.Response.GetResponseStream())
                    Dim errorText As String = reader.ReadToEnd()
                    Console.WriteLine("Server says: " & errorText)
                    Return ("Server says: " & errorText)
                End Using
            End If
        End Try
    End Function

    Sub UploadFileToFtp(server As String, username As String, password As String, filePath As String, remoteFileName As String)
        Try
            ' Create an FTP request
            'Dim FUri As Uri = New Uri(server & remoteFileName)

            Dim request As System.Net.FtpWebRequest = DirectCast(System.Net.WebRequest.Create("ftp://216.48.180.109/public_html/sadhvi/" & mAttachmentName + ".pdf"), System.Net.WebRequest)

            'Dim request As FtpWebRequest = CType(WebRequest.Create(FUri), FtpWebRequest)
            'Dim request As FtpWebRequest = WebRequest.Create(server & "" & remoteFileName)
            'Dim request As FtpWebRequest = (FtpWebRequest)FtpWebRequest.Create(New Uri("ftp://" + ftpServerIP + "/outbox/" + objFile.Name));

            request.Method = WebRequestMethods.Ftp.UploadFile

            ' Set FTP credentials
            request.Credentials = New NetworkCredential(username, password)

            ' Enable binary transfer mode for file upload
            request.UseBinary = True
            request.UsePassive = True
            request.KeepAlive = False

            ' Read the file into a byte array
            Dim fileContents As Byte() = File.ReadAllBytes(filePath)

            ' Get the request stream and upload the file
            Using requestStream As Stream = request.GetRequestStream()
                requestStream.Write(fileContents, 0, fileContents.Length)
            End Using

            ' Get the response from the FTP server
            Using response As FtpWebResponse = CType(request.GetResponse(), FtpWebResponse)
                Console.WriteLine("Upload File Complete, status: " & response.StatusDescription)
            End Using
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
        End Try
    End Sub
    'Function UploadFileToFTPServer() As String
    '    ' FTP Server Information
    '    Dim ftpServer As String = "ftp://216.48.180.109"
    '    Dim ftpUsername As String = "equal2464"
    '    Dim ftpPassword As String = "tActL$*$P*67"
    '    Dim filePath As String = "d:/Sadhvi/13414.pdf"
    '    Dim remotePath As String = "/public_html/sadhvi/13414.pdf"

    '    Try
    '        ' Combine FTP server address and remote path
    '        Dim ftpUri As String = ftpServer & remotePath
    '        ' Create FTP Request
    '        Dim request As FtpWebRequest = CType(WebRequest.Create(ftpUri), FtpWebRequest)
    '        request.Method = WebRequestMethods.Ftp.UploadFile
    '        request.Credentials = New NetworkCredential(ftpUsername, ftpPassword)
    '        request.UseBinary = True
    '        request.KeepAlive = False

    '        ' Read the file to upload
    '        Dim fileContents As Byte() = File.ReadAllBytes(filePath)
    '        request.ContentLength = fileContents.Length

    '        ' Upload file to server
    '        Using requestStream As Stream = request.GetRequestStream()
    '            requestStream.Write(fileContents, 0, fileContents.Length)
    '        End Using

    '        ' Get response from server
    '        Using response As FtpWebResponse = CType(request.GetResponse(), FtpWebResponse)
    '            Console.WriteLine("Upload status: " & response.StatusDescription)
    '        End Using

    '    Catch ex As Exception
    '        Console.WriteLine("Error: " & ex.Message)
    '    End Try
    'End Function
    Function UploadFile() As String
        'Dim filePath As String, serverUrl As String
        'filePath = "d:\delivery_challan - Copy.pdf"
        'serverUrl = "ftp://216.48.180.109//sadhvi//"
        'Dim username As String = "equal2464"
        'Dim password As String = "tActL$*$P*67"
        'Try
        '    ' Ensure the file exists
        '    If Not File.Exists(filePath) Then
        '        Throw New FileNotFoundException("The file does not exist.")
        '    End If

        '    ' Create a WebClient instance
        '    Using client As New WebClient()
        '        ' Add a header if needed (e.g., for authentication)
        '        ' client.Headers.Add("Authorization", "Bearer your_token")
        '        client.Credentials = New NetworkCredential(UserName, password)

        '        ' Upload the file
        '        Dim responseBytes As Byte() = client.UploadFile(serverUrl, filePath)

        '        ' Convert the response to a string and return it
        '        Return System.Text.Encoding.UTF8.GetString(responseBytes)
        '    End Using

        'Catch ex As Exception
        '    ' Handle errors
        '    Return "Error: " & ex.Message
        'End Try
    End Function

End Class
