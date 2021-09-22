Imports CrystalDecisions.CrystalReports.Engine
Imports System.Drawing.Printing.PrinterSettings
Imports System.IO
Imports Microsoft.Reporting.WinForms
Imports CrystalDecisions.Shared
Imports System.Linq
Imports System.Text

Public Class FrmMailComposeWithCrystal
    Inherits System.Windows.Forms.Form
    Dim mRepObj As New ReportDocument

    Dim mReportTitle As String = "", mReportSubTitle As String = ""
    Friend WithEvents BtnAttachments As Button
    Dim mAttachmentName As String = ""
    Dim mAttachmentSaveFolderName As String = "EMail"
    Dim mSearchCode As String = ""
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
    Friend WithEvents BtnCc As Button
    Friend WithEvents BtnTo As Button
    Friend WithEvents GroupBox10 As GroupBox
    Friend WithEvents GroupBox11 As GroupBox
    Public WithEvents LblCc As Label
    Public WithEvents TxtCcEMail As AgControls.AgTextBox
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents GroupBox5 As GroupBox
    Public WithEvents Label1 As Label
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Public WithEvents TxtMessage As AgControls.AgTextBox
    Public WithEvents LblSubject As Label
    Public WithEvents TxtSubject As AgControls.AgTextBox
    Public WithEvents LblToEmail As Label
    Public WithEvents TxtToEmail As AgControls.AgTextBox
    Friend WithEvents BtnSend As Button
    Friend WithEvents GroupBox8 As GroupBox
    Friend WithEvents GroupBox9 As GroupBox

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMailComposeWithCrystal))
        Me.CrvReport = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.BtnCc = New System.Windows.Forms.Button()
        Me.BtnTo = New System.Windows.Forms.Button()
        Me.GroupBox10 = New System.Windows.Forms.GroupBox()
        Me.GroupBox11 = New System.Windows.Forms.GroupBox()
        Me.LblCc = New System.Windows.Forms.Label()
        Me.TxtCcEMail = New AgControls.AgTextBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TxtMessage = New AgControls.AgTextBox()
        Me.LblSubject = New System.Windows.Forms.Label()
        Me.TxtSubject = New AgControls.AgTextBox()
        Me.LblToEmail = New System.Windows.Forms.Label()
        Me.TxtToEmail = New AgControls.AgTextBox()
        Me.BtnSend = New System.Windows.Forms.Button()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.BtnAttachments = New System.Windows.Forms.Button()
        Me.GroupBox10.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
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
        'BtnCc
        '
        Me.BtnCc.BackColor = System.Drawing.Color.Transparent
        Me.BtnCc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCc.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnCc.ForeColor = System.Drawing.Color.White
        Me.BtnCc.Image = CType(resources.GetObject("BtnCc.Image"), System.Drawing.Image)
        Me.BtnCc.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BtnCc.Location = New System.Drawing.Point(386, 62)
        Me.BtnCc.Name = "BtnCc"
        Me.BtnCc.Size = New System.Drawing.Size(31, 28)
        Me.BtnCc.TabIndex = 919
        Me.BtnCc.UseVisualStyleBackColor = False
        '
        'BtnTo
        '
        Me.BtnTo.BackColor = System.Drawing.Color.Transparent
        Me.BtnTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnTo.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnTo.ForeColor = System.Drawing.Color.White
        Me.BtnTo.Image = CType(resources.GetObject("BtnTo.Image"), System.Drawing.Image)
        Me.BtnTo.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BtnTo.Location = New System.Drawing.Point(386, 26)
        Me.BtnTo.Name = "BtnTo"
        Me.BtnTo.Size = New System.Drawing.Size(31, 28)
        Me.BtnTo.TabIndex = 918
        Me.BtnTo.UseVisualStyleBackColor = False
        '
        'GroupBox10
        '
        Me.GroupBox10.Controls.Add(Me.GroupBox11)
        Me.GroupBox10.Location = New System.Drawing.Point(0, 91)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(421, 2)
        Me.GroupBox10.TabIndex = 917
        Me.GroupBox10.TabStop = False
        '
        'GroupBox11
        '
        Me.GroupBox11.Location = New System.Drawing.Point(0, 39)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Size = New System.Drawing.Size(415, 10)
        Me.GroupBox11.TabIndex = 885
        Me.GroupBox11.TabStop = False
        '
        'LblCc
        '
        Me.LblCc.AutoSize = True
        Me.LblCc.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LblCc.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LblCc.Location = New System.Drawing.Point(6, 69)
        Me.LblCc.Name = "LblCc"
        Me.LblCc.Size = New System.Drawing.Size(26, 16)
        Me.LblCc.TabIndex = 916
        Me.LblCc.Text = "Cc"
        '
        'TxtCcEMail
        '
        Me.TxtCcEMail.AgAllowUserToEnableMasterHelp = False
        Me.TxtCcEMail.AgLastValueTag = Nothing
        Me.TxtCcEMail.AgLastValueText = Nothing
        Me.TxtCcEMail.AgMandatory = False
        Me.TxtCcEMail.AgMasterHelp = False
        Me.TxtCcEMail.AgNumberLeftPlaces = 0
        Me.TxtCcEMail.AgNumberNegetiveAllow = False
        Me.TxtCcEMail.AgNumberRightPlaces = 0
        Me.TxtCcEMail.AgPickFromLastValue = False
        Me.TxtCcEMail.AgRowFilter = ""
        Me.TxtCcEMail.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCcEMail.AgSelectedValue = Nothing
        Me.TxtCcEMail.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCcEMail.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCcEMail.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCcEMail.Font = New System.Drawing.Font("Verdana", 11.25!)
        Me.TxtCcEMail.Location = New System.Drawing.Point(75, 68)
        Me.TxtCcEMail.MaxLength = 0
        Me.TxtCcEMail.Name = "TxtCcEMail"
        Me.TxtCcEMail.Size = New System.Drawing.Size(309, 19)
        Me.TxtCcEMail.TabIndex = 906
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.GroupBox7)
        Me.GroupBox6.Location = New System.Drawing.Point(2, 165)
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
        Me.Label1.Location = New System.Drawing.Point(6, 138)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 16)
        Me.Label1.TabIndex = 913
        Me.Label1.Text = "Message"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.GroupBox4)
        Me.GroupBox3.Location = New System.Drawing.Point(0, 128)
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Location = New System.Drawing.Point(0, 54)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(421, 2)
        Me.GroupBox1.TabIndex = 911
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(0, 39)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(415, 10)
        Me.GroupBox2.TabIndex = 885
        Me.GroupBox2.TabStop = False
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
        Me.TxtMessage.Location = New System.Drawing.Point(6, 182)
        Me.TxtMessage.MaxLength = 0
        Me.TxtMessage.Multiline = True
        Me.TxtMessage.Name = "TxtMessage"
        Me.TxtMessage.Size = New System.Drawing.Size(415, 379)
        Me.TxtMessage.TabIndex = 908
        '
        'LblSubject
        '
        Me.LblSubject.AutoSize = True
        Me.LblSubject.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LblSubject.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LblSubject.Location = New System.Drawing.Point(6, 106)
        Me.LblSubject.Name = "LblSubject"
        Me.LblSubject.Size = New System.Drawing.Size(63, 16)
        Me.LblSubject.TabIndex = 910
        Me.LblSubject.Text = "Subject"
        '
        'TxtSubject
        '
        Me.TxtSubject.AgAllowUserToEnableMasterHelp = False
        Me.TxtSubject.AgLastValueTag = Nothing
        Me.TxtSubject.AgLastValueText = Nothing
        Me.TxtSubject.AgMandatory = False
        Me.TxtSubject.AgMasterHelp = False
        Me.TxtSubject.AgNumberLeftPlaces = 0
        Me.TxtSubject.AgNumberNegetiveAllow = False
        Me.TxtSubject.AgNumberRightPlaces = 0
        Me.TxtSubject.AgPickFromLastValue = False
        Me.TxtSubject.AgRowFilter = ""
        Me.TxtSubject.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSubject.AgSelectedValue = Nothing
        Me.TxtSubject.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSubject.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSubject.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSubject.Font = New System.Drawing.Font("Verdana", 11.25!)
        Me.TxtSubject.Location = New System.Drawing.Point(75, 104)
        Me.TxtSubject.MaxLength = 0
        Me.TxtSubject.Name = "TxtSubject"
        Me.TxtSubject.Size = New System.Drawing.Size(346, 19)
        Me.TxtSubject.TabIndex = 907
        '
        'LblToEmail
        '
        Me.LblToEmail.AutoSize = True
        Me.LblToEmail.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LblToEmail.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LblToEmail.Location = New System.Drawing.Point(6, 33)
        Me.LblToEmail.Name = "LblToEmail"
        Me.LblToEmail.Size = New System.Drawing.Size(25, 16)
        Me.LblToEmail.TabIndex = 909
        Me.LblToEmail.Text = "To"
        '
        'TxtToEmail
        '
        Me.TxtToEmail.AgAllowUserToEnableMasterHelp = False
        Me.TxtToEmail.AgLastValueTag = Nothing
        Me.TxtToEmail.AgLastValueText = Nothing
        Me.TxtToEmail.AgMandatory = False
        Me.TxtToEmail.AgMasterHelp = False
        Me.TxtToEmail.AgNumberLeftPlaces = 0
        Me.TxtToEmail.AgNumberNegetiveAllow = False
        Me.TxtToEmail.AgNumberRightPlaces = 0
        Me.TxtToEmail.AgPickFromLastValue = False
        Me.TxtToEmail.AgRowFilter = ""
        Me.TxtToEmail.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtToEmail.AgSelectedValue = Nothing
        Me.TxtToEmail.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtToEmail.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtToEmail.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtToEmail.Font = New System.Drawing.Font("Verdana", 11.25!)
        Me.TxtToEmail.Location = New System.Drawing.Point(75, 32)
        Me.TxtToEmail.MaxLength = 0
        Me.TxtToEmail.Name = "TxtToEmail"
        Me.TxtToEmail.Size = New System.Drawing.Size(309, 19)
        Me.TxtToEmail.TabIndex = 905
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
        'BtnAttachments
        '
        Me.BtnAttachments.BackColor = System.Drawing.Color.SteelBlue
        Me.BtnAttachments.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAttachments.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold)
        Me.BtnAttachments.ForeColor = System.Drawing.Color.White
        Me.BtnAttachments.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.BtnAttachments.Location = New System.Drawing.Point(2, 583)
        Me.BtnAttachments.Name = "BtnAttachments"
        Me.BtnAttachments.Size = New System.Drawing.Size(111, 28)
        Me.BtnAttachments.TabIndex = 922
        Me.BtnAttachments.Text = "Attachments"
        Me.BtnAttachments.UseVisualStyleBackColor = False
        '
        'FrmMailComposeWithCrystal
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(970, 611)
        Me.Controls.Add(Me.BtnAttachments)
        Me.Controls.Add(Me.GroupBox8)
        Me.Controls.Add(Me.BtnSend)
        Me.Controls.Add(Me.BtnCc)
        Me.Controls.Add(Me.BtnTo)
        Me.Controls.Add(Me.GroupBox10)
        Me.Controls.Add(Me.LblCc)
        Me.Controls.Add(Me.TxtCcEMail)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TxtMessage)
        Me.Controls.Add(Me.LblSubject)
        Me.Controls.Add(Me.TxtSubject)
        Me.Controls.Add(Me.LblToEmail)
        Me.Controls.Add(Me.TxtToEmail)
        Me.Controls.Add(Me.CrvReport)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "FrmMailComposeWithCrystal"
        Me.Text = "EMail"
        Me.TopMost = True
        Me.GroupBox10.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
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
    Private Sub FrmMailComposeWithCrystal_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Location = New System.Drawing.Point(0, 0)
        CrvReport.EnableDrillDown = False
        CrvReport.Zoom(1)
    End Sub
    Private Sub FrmReportPrint_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Public Function FSendEMail() As Boolean
        Dim DtTemp As DataTable = Nothing
        Dim MLDFrom As System.Net.Mail.MailAddress
        Dim MLMMain As System.Net.Mail.MailMessage
        Dim SMTPMain As System.Net.Mail.SmtpClient
        Dim I As Integer
        Dim bBlnEnableSsl As Boolean = False
        Dim mQry$ = ""
        'Dim SmtpHost As String = "smtp.gmail.com"
        'Dim SmtpPort As String = "587"
        'Dim FromEmail As String = "equal2.noreply@gmail.com"
        'Dim FromEmailPassword As String = "P@ssw0rd!"
        Dim SmtpHost As String = ""
        Dim SmtpPort As String = ""
        Dim FromEmail As String = ""
        Dim FromEmailPassword As String = ""
        Dim FileName As String = ""
        Dim ToEMailArr As String() = Nothing
        Dim CcEMailArr As String() = Nothing


        mQry = "Select * From MailSender Where Div_Code = '" & AgL.PubDivCode & "' And Site_Code = '" & AgL.PubSiteCode & "'"
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTemp.Rows.Count = 0 Then
            mQry = "Select * From MailSender Where Div_Code = '" & AgL.PubDivCode & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTemp.Rows.Count = 0 Then
                mQry = "Select * From MailSender Where Site_Code = '" & AgL.PubSiteCode & "' "
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtTemp.Rows.Count = 0 Then
                    mQry = "Select * From MailSender "
                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                End If
            End If
        End If

        If DtTemp.Rows.Count = 0 Then
            MsgBox("Please define mail settings...!", MsgBoxStyle.Information)
            Exit Function
        End If

        If DtTemp.Rows.Count > 0 Then
            SmtpHost = AgL.XNull(DtTemp.Rows(0)("SmtpHost"))
            SmtpPort = AgL.XNull(DtTemp.Rows(0)("SmtpPort"))
            FromEmail = AgL.XNull(DtTemp.Rows(0)("FromEmailAddress"))
            FromEmailPassword = AgL.XNull(DtTemp.Rows(0)("FromEmailPassword"))
        End If

        If SmtpHost = "" Then MsgBox("Smtp Host is not defined in settings.") : Exit Function
        If SmtpPort = "" Then MsgBox("Smtp Port is not defined in settings.") : Exit Function
        If FromEmail = "" Then MsgBox("From Email is not defined in settings.") : Exit Function
        If FromEmailPassword = "" Then MsgBox("From Email Password is not defined in settings.") : Exit Function

        FileName = mAttachmentName + ".pdf"

        ToEMailArr = TxtToEmail.Text.Split(",")
        CcEMailArr = TxtCcEMail.Text.Split(",")

        Try
            SmtpHost = AgL.XNull(SmtpHost)
            SmtpPort = AgL.XNull(SmtpPort)

            MLDFrom = New System.Net.Mail.MailAddress(FromEmail)
            MLMMain = New System.Net.Mail.MailMessage()
            MLMMain.From = MLDFrom
            SMTPMain = New System.Net.Mail.SmtpClient(SmtpHost, SmtpPort)
            MLMMain.Body = TxtMessage.Text
            MLMMain.Subject = TxtSubject.Text

            For I = 0 To ToEMailArr.Length - 1
                If ToEMailArr(I) <> "" Then
                    MLMMain.To.Add(ToEMailArr(I))
                End If
            Next

            For I = 0 To CcEMailArr.Length - 1
                If CcEMailArr(I) <> "" Then
                    MLMMain.CC.Add(CcEMailArr(I))
                End If
            Next

            'Dim inputStream As MemoryStream = CType((CType(CrvReport.ReportSource, ReportDocument).ExportToStream(ExportFormatType.PortableDocFormat)), MemoryStream)
            'Dim PdfContent() As Byte = inputStream.ToArray


            'Dim MS As MemoryStream = New System.IO.MemoryStream(PdfContent)
            Dim MS As MemoryStream = CType((CType(CrvReport.ReportSource, ReportDocument).ExportToStream(ExportFormatType.PortableDocFormat)), MemoryStream)

            MLMMain.Attachments.Add(New System.Net.Mail.Attachment(MS, FileName))


            If BtnAttachments.Tag IsNot Nothing Then
                Dim AttachmentPath As String = PubAttachmentPath + mAttachmentSaveFolderName + "\"
                If Directory.Exists(AttachmentPath) Then
                    Dim di As New IO.DirectoryInfo(AttachmentPath)
                    Dim diar1 As IO.FileInfo() = di.GetFiles().ToArray
                    Dim dra As IO.FileInfo
                    For Each dra In diar1
                        MLMMain.Attachments.Add(New System.Net.Mail.Attachment(dra.FullName))
                    Next
                End If
            End If




            SMTPMain.Credentials = New Net.NetworkCredential(FromEmail, FromEmailPassword)
            SMTPMain.EnableSsl = True
            SMTPMain.Send(MLMMain)

            MLMMain.Dispose()
            FSendEMail = True


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
    Private Function FHPGD_EmailContacts() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mLineCond As String = ""
        Dim DtTemp As DataTable
        Dim mQry As String = ""

        mQry = " Select 'o' As Tick, SubCode As SearchKey, Name, Email From SubGroup Where EMail Is Not Null "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTemp.Rows.Count = 0 Then
            Exit Function
        End If

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DtTemp), "", 400, 720, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Name", 300, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "EMail", 300, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(3, "", "", ",", True)
        End If
        FHPGD_EmailContacts = StrRtn

        FRH_Multiple = Nothing
    End Function
    Private Sub BtnSend_Click(sender As Object, e As EventArgs) Handles BtnSend.Click
        If FSendEMail() = True Then
            MsgBox("Mail Send Sucessfully...!", MsgBoxStyle.Information)
        End If
    End Sub
    Private Sub BtnTo_Click(sender As Object, e As EventArgs) Handles BtnTo.Click, BtnCc.Click
        Select Case sender.Name
            Case BtnTo.Name
                TxtToEmail.Text = TxtToEmail.Text + "," + FHPGD_EmailContacts()
            Case BtnCc.Name
                TxtCcEMail.Text = TxtCcEMail.Text + "," + FHPGD_EmailContacts()
        End Select
    End Sub
    Public Function StreamToByteArray(inputStream As Stream) As Byte()

        'Using memoryStream = New MemoryStream()
        '    Dim count As Integer
        '    While ((count = inputStream.Read(bytes, 0, bytes.Length)) > 0)
        '        memoryStream.Write(bytes, 0, count)
        '    End While
        '    Return memoryStream.ToArray()
        'End Using


        Dim bytes = New Byte(inputStream.Length) {}
        Using memoryStream = New MemoryStream()
            For I As Integer = 0 To inputStream.Length - 1
                memoryStream.Write(bytes, 0, I)
            Next
            Return memoryStream.ToArray()
        End Using
    End Function
    Private Sub BtnAttachments_Click(sender As Object, e As EventArgs) Handles BtnAttachments.Click
        Dim FrmObj As New FrmAttachmentViewer()
        FrmObj.LblDocNo.Text = mReportSubTitle
        If mAttachmentSaveFolderName = "EMail" Then mAttachmentSaveFolderName = mAttachmentSaveFolderName + "\" + SearchCode
        FrmObj.SearchCode = mAttachmentSaveFolderName
        FrmObj.TableName = "SubGroupAttachments"
        FrmObj.StartPosition = FormStartPosition.CenterParent
        FrmObj.ShowDialog()

        BtnAttachments.Tag = FrmObj

        Dim AttachmentPath As String = PubAttachmentPath + mAttachmentSaveFolderName + "\" + mSearchCode + "\"
        If Directory.Exists(AttachmentPath) Then
            Dim FileCount As Integer = Directory.GetFiles(AttachmentPath).Count
            If FileCount > 0 Then BtnAttachments.Text = FileCount.ToString + IIf(FileCount = 1, " Attachment", " Attachments") Else BtnAttachments.Text = "Attachments"
        Else
            BtnAttachments.Text = "Attachments"
        End If
    End Sub

End Class
