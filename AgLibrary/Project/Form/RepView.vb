Imports CrystalDecisions.CrystalReports.Engine
Imports System.Drawing.Printing.PrinterSettings
Public Class RepView
    Inherits System.Windows.Forms.Form
    Dim mRepObj As New ReportDocument
    Friend WithEvents BtnWhatsapp As Button
    Dim AgL As AgLibrary.ClsMain
    Public mDefaultMobileNo As String = ""

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub
    Public Sub New(ByVal AgLibVar As AgLibrary.ClsMain)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        AgL = AgLibVar
    End Sub
    Public Property DefaultMobileNo() As String
        Get
            DefaultMobileNo = mDefaultMobileNo
        End Get
        Set(ByVal value As String)
            mDefaultMobileNo = value
        End Set
    End Property

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    Public WithEvents CrvReport As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents BtnPrintSetup As System.Windows.Forms.Button
    Friend WithEvents BtnEMail As Button

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.CrvReport = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.BtnPrintSetup = New System.Windows.Forms.Button()
        Me.BtnEMail = New System.Windows.Forms.Button()
        Me.BtnWhatsapp = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'CrvReport
        '
        Me.CrvReport.ActiveViewIndex = -1
        Me.CrvReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CrvReport.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.CrvReport.DisplayGroupTree = False
        Me.CrvReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CrvReport.Location = New System.Drawing.Point(0, 0)
        Me.CrvReport.Name = "CrvReport"
        Me.CrvReport.SelectionFormula = ""
        Me.CrvReport.Size = New System.Drawing.Size(709, 441)
        Me.CrvReport.TabIndex = 0
        Me.CrvReport.ViewTimeSelectionFormula = ""
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.btnClose.Location = New System.Drawing.Point(621, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.btnClose.Size = New System.Drawing.Size(84, 24)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "Clos&e"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'BtnPrintSetup
        '
        Me.BtnPrintSetup.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnPrintSetup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPrintSetup.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnPrintSetup.Location = New System.Drawing.Point(352, 3)
        Me.BtnPrintSetup.Name = "BtnPrintSetup"
        Me.BtnPrintSetup.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.BtnPrintSetup.Size = New System.Drawing.Size(84, 24)
        Me.BtnPrintSetup.TabIndex = 6
        Me.BtnPrintSetup.Text = "&Printer "
        Me.BtnPrintSetup.UseVisualStyleBackColor = True
        Me.BtnPrintSetup.Visible = False
        '
        'BtnEMail
        '
        Me.BtnEMail.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnEMail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnEMail.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnEMail.Location = New System.Drawing.Point(532, 3)
        Me.BtnEMail.Name = "BtnEMail"
        Me.BtnEMail.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.BtnEMail.Size = New System.Drawing.Size(84, 24)
        Me.BtnEMail.TabIndex = 7
        Me.BtnEMail.Text = "EMail"
        Me.BtnEMail.UseVisualStyleBackColor = True
        '
        'BtnWhatsapp
        '
        Me.BtnWhatsapp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnWhatsapp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnWhatsapp.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnWhatsapp.Location = New System.Drawing.Point(442, 3)
        Me.BtnWhatsapp.Name = "BtnWhatsapp"
        Me.BtnWhatsapp.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.BtnWhatsapp.Size = New System.Drawing.Size(84, 24)
        Me.BtnWhatsapp.TabIndex = 8
        Me.BtnWhatsapp.Text = "Whatsapp"
        Me.BtnWhatsapp.UseVisualStyleBackColor = True
        '
        'RepView
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(709, 441)
        Me.Controls.Add(Me.BtnWhatsapp)
        Me.Controls.Add(Me.BtnEMail)
        Me.Controls.Add(Me.BtnPrintSetup)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.CrvReport)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.Name = "RepView"
        Me.Text = "Report View"
        Me.TopMost = True
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub

#End Region
    Public WriteOnly Property RepObj()
        Set(ByVal Value)
            CrvReport.ReportSource = Value
            mRepObj = Value
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

    Private Sub RepView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CrvReport.EnableDrillDown = False
        btnClose.BringToFront()
    End Sub


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    '    Private Sub BtnPrintSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrintSetup.Click
    '        Dim PrintDialog1 As New PrintDialog()
    '        Dim result As DialogResult = PrintDialog1.ShowDialog()
    '        If (result = Windows.Forms.DialogResult.OK) Then
    '            mRepObj.PrintOptions.PrinterName = PrintDialog1.PrinterSettings.PrinterName
    '            If PrintDialog1.PrinterSettings.DefaultPageSettings.Landscape = True Then
    '                mRepObj.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape
    '            Else
    '                mRepObj.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
    '            End If
    '            Select Case PrintDialog1.PrinterSettings.DefaultPageSettings.PaperSize.PaperName
    '                'Case "A2" ' A2 paper (420 mm by 594 mm).  
    '                Case "A3" ' A3 paper (297 mm by 420 mm).  
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA3
    '                    ' '' ''Case "A3Extra" ' A3 extra paper (322 mm by 445 mm).  
    '                    ' '' ''Case "A3ExtraTransverse" ' A3 extra transverse paper (322 mm by 445 mm).  "
    '                    ' '' ''Case "A3Rotated" ' A3 rotated paper (420 mm by 297 mm).  
    '                Case "A3Transverse" ' A3 transverse paper (297 mm by 420 mm).  
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA3
    '                Case "A4" ' A4 paper (210 mm by 297 mm).  
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4
    '                    ' '' ''Case "A4Extra" ' A4 extra paper (236 mm by 322 mm). This value is specific to the PostScript driver and is used only by Linotronic printers to help save paper.  
    '                    ' '' ''Case "A4Plus" ' A4 plus paper (210 mm by 330 mm).  
    '                    ' '' ''Case "A4Rotated" ' A4 rotated paper (297 mm by 210 mm). Requires Windows 98, Windows NT 4.0, or later.  "
    '                Case "A4Small" ' A4 small paper (210 mm by 297 mm).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4Small
    '                    ' '' ''Case "A4Transverse" ' A4 transverse paper (210 mm by 297 mm).  "
    '                Case "A5" ' A5 paper (148 mm by 210 mm).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA5
    '                    '' '' ''Case "A5Extra" ' A5 extra paper (174 mm by 235 mm).  "
    '                    '' '' ''Case "A5Rotated" ' A5 rotated paper (210 mm by 148 mm). Requires Windows 98, Windows NT 4.0, or later.  "
    '                    '' '' ''Case "A5Transverse" ' A5 transverse paper (148 mm by 210 mm).  "
    '                    '' '' ''Case "A6" ' A6 paper (105 mm by 148 mm). Requires Windows 98, Windows NT 4.0, or later.  "
    '                    '' '' ''Case "A6Rotated" ' A6 rotated paper (148 mm by 105 mm). Requires Windows 98, Windows NT 4.0, or later.  "
    '                    '' '' ''Case "APlus" ' SuperA/SuperA/A4 paper (227 mm by 356 mm).  "
    '                    '' '' ''Case "B4" ' B4 paper (250 mm by 353 mm).  "
    '                    '' '' ''Case "B4Envelope" ' B4 envelope (250 mm by 353 mm).  "
    '                    '' '' ''Case "B4JisRotated" ' JIS B4 rotated paper (364 mm by 257 mm). Requires Windows 98, Windows NT 4.0, or later.  "
    '                    '' '' ''Case "B5" ' B5 paper (176 mm by 250 mm).  "
    '                    '' '' ''Case "B5Envelope" ' B5 envelope (176 mm by 250 mm).  "
    '                    '' '' ''Case "B5Extra" ' ISO B5 extra paper (201 mm by 276 mm).  "
    '                    '' '' ''Case "B5JisRotated" ' JIS B5 rotated paper (257 mm by 182 mm). Requires Windows 98, Windows NT 4.0, or later.  "
    '                    '' '' ''Case "B5Transverse" ' JIS B5 transverse paper (182 mm by 257 mm).  "
    '                    '' '' ''Case "B6Envelope" ' B6 envelope (176 mm by 125 mm).  "
    '                    '' '' ''Case "B6Jis" ' JIS B6 paper (128 mm by 182 mm). Requires Windows 98, Windows NT 4.0, or later.  "
    '                    '' '' ''Case "B6JisRotated" ' JIS B6 rotated paper (182 mm by 128 mm). Requires Windows 98, Windows NT 4.0, or later.  "
    '                    '' '' ''Case "BPlus" ' SuperB/SuperB/A3 paper (305 mm by 487 mm).  "
    '                Case "C3Envelope" ' C3 envelope (324 mm by 458 mm).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperEnvelopeC3
    '                Case "C4Envelope" ' C4 envelope (229 mm by 324 mm).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperEnvelopeC4
    '                Case "C5Envelope" ' C5 envelope (162 mm by 229 mm).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperEnvelopeC5
    '                Case "C65Envelope" ' C65 envelope (114 mm by 229 mm).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperEnvelopeC65
    '                Case "C6Envelope" ' C6 envelope (114 mm by 162 mm).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperEnvelopeC6
    '                Case "CSheet" ' C paper (17 in. by 22 in.).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperCsheet
    '                Case "Custom" ' The paper size is defined by the user.  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
    '                Case "DLEnvelope" ' DL envelope (110 mm by 220 mm).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperEnvelopeDL
    '                Case "DSheet" ' D paper (22 in. by 34 in.).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperDsheet
    '                Case "ESheet" ' E paper (34 in. by 44 in.).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperEsheet
    '                Case "Executive" ' Executive paper (7.25 in. by 10.5 in.).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperExecutive
    '                Case "Folio" ' Folio paper (8.5 in. by 13 in.).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperFolio
    '                Case "GermanLegalFanfold" ' German legal fanfold (8.5 in. by 13 in.).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperFanfoldLegalGerman
    '                Case "GermanStandardFanfold" ' German standard fanfold (8.5 in. by 12 in.).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperFanfoldStdGerman
    '                    ' '' ''Case "InviteEnvelope" ' Invitation envelope (220 mm by 220 mm).  "
    '                    ' '' ''Case "IsoB4" ' ISO B4 (250 mm by 353 mm).  "
    '                Case "Ledger" ' Ledger paper (17 in. by 11 in.).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLedger
    '                Case "Legal" ' Legal paper (8.5 in. by 14 in.).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLegal
    '                    ' '' ''Case "LegalExtra" ' Legal extra paper (9.275 in. by 15 in.). This value is specific to the PostScript driver and is used only by Linotronic printers in order to conserve paper.  "
    '                Case "Letter" ' Letter paper (8.5 in. by 11 in.).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLetter
    '                    ' '' ''Case "LetterExtra" ' Letter extra paper (9.275 in. by 12 in.). This value is specific to the PostScript driver and is used only by Linotronic printers in order to conserve paper.  "
    '                    ' '' ''Case "LetterExtraTransverse " 'Letter extra transverse paper (9.275 in. by 12 in.).  "
    '                    ' '' ''Case "LetterPlus" ' Letter plus paper (8.5 in. by 12.69 in.).  "
    '                    ' '' ''Case "LetterRotated" ' Letter rotated paper (11 in. by 8.5 in.).  "
    '                Case "LetterSmall" ' Letter small paper (8.5 in. by 11 in.).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperLetterSmall
    '                    ' '' ''Case "LetterTransverse" ' Letter transverse paper (8.275 in. by 11 in.).  "
    '                    ' '' ''Case "MonarchEnvelope" ' Monarch envelope (3.875 in. by 7.5 in.).  "
    '                Case "Note" ' Note paper (8.5 in. by 11 in.).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperNote
    '                    ' '' ''Case "Number10Envelope" ' #10 envelope (4.125 in. by 9.5 in.).  "
    '                    ' '' ''Case "Number11Envelope" ' #11 envelope (4.5 in. by 10.375 in.).  "
    '                    ' '' ''Case "Number12Envelope" ' #12 envelope (4.75 in. by 11 in.).  "
    '                    ' '' ''Case "Number14Envelope" ' #14 envelope (5 in. by 11.5 in.).  "
    '                    ' '' ''Case "Number9Envelope" ' #9 envelope (3.875 in. by 8.875 in.).  "
    '                    ' '' ''Case "PersonalEnvelope" ' 6 3/4 envelope (3.625 in. by 6.5 in.).  "
    '                Case "Quarto" ' Quarto paper (215 mm by 275 mm).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperQuarto
    '                    ' '' ''Case "Standard10x11" ' Standard paper (10 in. by 11 in.).  "
    '                Case "Standard10x14" ' Standard paper (10 in. by 14 in.).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.Paper10x14
    '                Case "Standard11x17" ' Standard paper (11 in. by 17 in.).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.Paper11x17
    '                    ' '' ''Case "Standard12x11" ' Standard paper (12 in. by 11 in.). Requires Windows 98, Windows NT 4.0, or later.  "
    '                    ' '' ''Case "Standard15x11" ' Standard paper (15 in. by 11 in.).  "
    '                    ' '' ''Case "Standard9x11" ' Standard paper (9 in. by 11 in.).  "
    '                Case "Statement" ' Statement paper (5.5 in. by 8.5 in.).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperStatement
    '                Case "Tabloid" ' Tabloid paper (11 in. by 17 in.).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperTabloid
    '                Case "USStandardFanfold" ' US standard fanfold (14.875 in. by 11 in.).  "
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperFanfoldUS
    '                Case Else
    '                    mRepObj.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
    '            End Select
    '        End If
    '        CrvReport.ReportSource = mRepObj
    '    End Sub

    Private Sub BtnPrintSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrintSetup.Click
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
    Private Sub BtnEMail_Click(sender As Object, e As EventArgs) Handles BtnEMail.Click
        Dim mObjEMail As New FrmMailComposeWithCrystal(AgL)
        CType(mObjEMail.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = CType(Me.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource
        mObjEMail.AttachmentName = Me.Text
        mObjEMail.MdiParent = Me.MdiParent
        mObjEMail.WindowState = FormWindowState.Maximized
        mObjEMail.Show()
        mObjEMail.TxtToEmail.Text = AgL.FGetSettings(AgL, "Mail To", "General", AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        mObjEMail.TxtCcEMail.Text = AgL.FGetSettings(AgL, "Mail Cc", "General", AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        mObjEMail.TxtSubject.Text = AgL.FGetSettings(AgL, "Mail Subject", "General", AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        mObjEMail.TxtMessage.Text = AgL.FGetSettings(AgL, "Mail Message", "General", AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
    End Sub

    Private Sub BtnWhatsapp_Click(sender As Object, e As EventArgs) Handles BtnWhatsapp.Click
        Dim mObjEMail As New FrmWhatsappComposeWithCrystal(AgL)
        CType(mObjEMail.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = CType(Me.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource
        mObjEMail.AttachmentName = Me.Text
        mObjEMail.MdiParent = Me.MdiParent
        mObjEMail.DefaultMobileNo = mDefaultMobileNo
        mObjEMail.WindowState = FormWindowState.Maximized
        mObjEMail.Show()
        mObjEMail.TxtToMobile.Text = AgL.FGetSettings(AgL, "Mobile No", "General", AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
        mObjEMail.TxtMessage.Text = AgL.FGetSettings(AgL, "Mail Message", "General", AgL.PubDivCode, AgL.PubSiteCode, "", "", "", "", "")
    End Sub
End Class
