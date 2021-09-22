Imports CrystalDecisions.CrystalReports.Engine
Imports System.Drawing.Printing.PrinterSettings
Public Class RepView
    Inherits System.Windows.Forms.Form
    Dim RpdObj As New ReportDocument
    Dim PRDGMain As PrintDialog
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
    Friend WithEvents CrvReport As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents BtnClose As System.Windows.Forms.Button
    Friend WithEvents BtnPrintSetup As System.Windows.Forms.Button

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RepView))
        Me.CrvReport = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.BtnClose = New System.Windows.Forms.Button
        Me.BtnPrintSetup = New System.Windows.Forms.Button
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
        'BtnClose
        '
        Me.BtnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClose.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnClose.Location = New System.Drawing.Point(621, 3)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.BtnClose.Size = New System.Drawing.Size(84, 24)
        Me.BtnClose.TabIndex = 4
        Me.BtnClose.Text = "Clos&e"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'BtnPrintSetup
        '
        Me.BtnPrintSetup.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnPrintSetup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPrintSetup.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnPrintSetup.Location = New System.Drawing.Point(533, 3)
        Me.BtnPrintSetup.Name = "BtnPrintSetup"
        Me.BtnPrintSetup.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.BtnPrintSetup.Size = New System.Drawing.Size(84, 24)
        Me.BtnPrintSetup.TabIndex = 6
        Me.BtnPrintSetup.Text = "&Printer "
        Me.BtnPrintSetup.UseVisualStyleBackColor = True
        '
        'RepView
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(709, 441)
        Me.Controls.Add(Me.BtnPrintSetup)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.CrvReport)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "RepView"
        Me.Text = "Report View"
        Me.TopMost = True
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub

#End Region
    Sub New(ByVal PRDGMainVar As PrintDialog)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        PRDGMain = PRDGMainVar
    End Sub
    Public WriteOnly Property RepObj()
        Set(ByVal Value)
            CrvReport.ReportSource = Value
            RpdObj = Value
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
        BtnClose.BringToFront()
    End Sub
    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub
    Private Sub BtnPrintSetup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrintSetup.Click
        Dim Result As DialogResult = PRDGMain.ShowDialog()

        If (Result = Windows.Forms.DialogResult.OK) Then
            RpdObj.PrintOptions.PrinterName = PRDGMain.PrinterSettings.PrinterName
            If PRDGMain.PrinterSettings.DefaultPageSettings.Landscape = True Then
                RpdObj.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape
            Else
                RpdObj.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Portrait
            End If
            RpdObj.PrintOptions.PaperSize = PRDGMain.PrinterSettings.DefaultPageSettings.PaperSize.RawKind
            CrvReport.ReportSource = RpdObj
            RpdObj.PrintToPrinter(PRDGMain.PrinterSettings.Copies, PRDGMain.PrinterSettings.Collate, PRDGMain.PrinterSettings.MinimumPage, PRDGMain.PrinterSettings.MinimumPage)
        End If
    End Sub
End Class
