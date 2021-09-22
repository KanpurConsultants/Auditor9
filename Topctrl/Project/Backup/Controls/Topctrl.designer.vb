<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Topctrl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Topctrl))
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolBar1 = New System.Windows.Forms.ToolBar
        Me.btbAdd = New System.Windows.Forms.ToolBarButton
        Me.btbEdit = New System.Windows.Forms.ToolBarButton
        Me.btbDel = New System.Windows.Forms.ToolBarButton
        Me.btbCancel = New System.Windows.Forms.ToolBarButton
        Me.ToolBarButton1 = New System.Windows.Forms.ToolBarButton
        Me.btbFirst = New System.Windows.Forms.ToolBarButton
        Me.btbPrevious = New System.Windows.Forms.ToolBarButton
        Me.btbNext = New System.Windows.Forms.ToolBarButton
        Me.btbLast = New System.Windows.Forms.ToolBarButton
        Me.ToolBarButton2 = New System.Windows.Forms.ToolBarButton
        Me.btbFind = New System.Windows.Forms.ToolBarButton
        Me.btbPrint = New System.Windows.Forms.ToolBarButton
        Me.ToolBarButton4 = New System.Windows.Forms.ToolBarButton
        Me.btbSave = New System.Windows.Forms.ToolBarButton
        Me.btbDiscard = New System.Windows.Forms.ToolBarButton
        Me.btbSite = New System.Windows.Forms.ToolBarButton
        Me.btbRefresh = New System.Windows.Forms.ToolBarButton
        Me.ToolBarButton6 = New System.Windows.Forms.ToolBarButton
        Me.btbExit = New System.Windows.Forms.ToolBarButton
        Me.ActiveMode = New System.Windows.Forms.Label
        Me.LblRec = New System.Windows.Forms.Label
        Me.LblDocId = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "")
        Me.ImageList1.Images.SetKeyName(1, "")
        Me.ImageList1.Images.SetKeyName(2, "")
        Me.ImageList1.Images.SetKeyName(3, "")
        Me.ImageList1.Images.SetKeyName(4, "")
        Me.ImageList1.Images.SetKeyName(5, "")
        Me.ImageList1.Images.SetKeyName(6, "")
        Me.ImageList1.Images.SetKeyName(7, "")
        Me.ImageList1.Images.SetKeyName(8, "")
        Me.ImageList1.Images.SetKeyName(9, "")
        Me.ImageList1.Images.SetKeyName(10, "")
        Me.ImageList1.Images.SetKeyName(11, "")
        Me.ImageList1.Images.SetKeyName(12, "")
        Me.ImageList1.Images.SetKeyName(13, "MSGBOX03.ICO")
        '
        'ToolBar1
        '
        Me.ToolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.ToolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btbAdd, Me.btbEdit, Me.btbDel, Me.btbCancel, Me.ToolBarButton1, Me.btbFirst, Me.btbPrevious, Me.btbNext, Me.btbLast, Me.ToolBarButton2, Me.btbFind, Me.btbPrint, Me.ToolBarButton4, Me.btbSave, Me.btbDiscard, Me.btbSite, Me.btbRefresh, Me.ToolBarButton6, Me.btbExit})
        Me.ToolBar1.ButtonSize = New System.Drawing.Size(30, 80)
        Me.ToolBar1.DropDownArrows = True
        Me.ToolBar1.ImageList = Me.ImageList1
        Me.ToolBar1.Location = New System.Drawing.Point(0, 0)
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.ShowToolTips = True
        Me.ToolBar1.Size = New System.Drawing.Size(863, 42)
        Me.ToolBar1.TabIndex = 0
        Me.ToolBar1.TabStop = True
        '
        'btbAdd
        '
        Me.btbAdd.ImageIndex = 1
        Me.btbAdd.Name = "btbAdd"
        Me.btbAdd.Text = "Add"
        Me.btbAdd.ToolTipText = "[F2] Add New Record"
        '
        'btbEdit
        '
        Me.btbEdit.ImageIndex = 2
        Me.btbEdit.Name = "btbEdit"
        Me.btbEdit.Text = "Edit"
        Me.btbEdit.ToolTipText = "[F3] Edit Current Record"
        '
        'btbDel
        '
        Me.btbDel.ImageIndex = 0
        Me.btbDel.Name = "btbDel"
        Me.btbDel.Text = "Delete"
        Me.btbDel.ToolTipText = "[F4] Delete Current Record "
        '
        'btbCancel
        '
        Me.btbCancel.Name = "btbCancel"
        Me.btbCancel.ToolTipText = "Mark Record as Canceled"
        Me.btbCancel.Visible = False
        '
        'ToolBarButton1
        '
        Me.ToolBarButton1.Name = "ToolBarButton1"
        Me.ToolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btbFirst
        '
        Me.btbFirst.ImageIndex = 7
        Me.btbFirst.Name = "btbFirst"
        Me.btbFirst.Text = "First"
        Me.btbFirst.ToolTipText = "[Home] Navigate to First Record"
        '
        'btbPrevious
        '
        Me.btbPrevious.ImageIndex = 10
        Me.btbPrevious.Name = "btbPrevious"
        Me.btbPrevious.Text = "Previous"
        Me.btbPrevious.ToolTipText = "[Page Up] Navigate to Previous Record"
        '
        'btbNext
        '
        Me.btbNext.ImageIndex = 9
        Me.btbNext.Name = "btbNext"
        Me.btbNext.Text = "Next"
        Me.btbNext.ToolTipText = "[Page Down] Navigate to Next Record"
        '
        'btbLast
        '
        Me.btbLast.ImageIndex = 8
        Me.btbLast.Name = "btbLast"
        Me.btbLast.Text = "Last"
        Me.btbLast.ToolTipText = "[End] Navigate to Last Record"
        '
        'ToolBarButton2
        '
        Me.ToolBarButton2.Name = "ToolBarButton2"
        Me.ToolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btbFind
        '
        Me.btbFind.ImageIndex = 5
        Me.btbFind.Name = "btbFind"
        Me.btbFind.Text = "Find"
        Me.btbFind.ToolTipText = "[Ctrl+F] Search for Records"
        '
        'btbPrint
        '
        Me.btbPrint.ImageIndex = 6
        Me.btbPrint.Name = "btbPrint"
        Me.btbPrint.Text = "Print"
        Me.btbPrint.ToolTipText = "[Ctrl+P] Print Records"
        '
        'ToolBarButton4
        '
        Me.ToolBarButton4.Name = "ToolBarButton4"
        Me.ToolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btbSave
        '
        Me.btbSave.Enabled = False
        Me.btbSave.ImageIndex = 11
        Me.btbSave.Name = "btbSave"
        Me.btbSave.Text = "Save"
        Me.btbSave.ToolTipText = "[Ctrl+S] Save Record"
        '
        'btbDiscard
        '
        Me.btbDiscard.Enabled = False
        Me.btbDiscard.ImageIndex = 12
        Me.btbDiscard.Name = "btbDiscard"
        Me.btbDiscard.Text = "Cancel"
        Me.btbDiscard.ToolTipText = "[Esc] Discard Changes"
        '
        'btbSite
        '
        Me.btbSite.ImageIndex = 13
        Me.btbSite.Name = "btbSite"
        Me.btbSite.Text = "Site"
        Me.btbSite.ToolTipText = "[F11] Select Site"
        '
        'btbRefresh
        '
        Me.btbRefresh.ImageIndex = 3
        Me.btbRefresh.Name = "btbRefresh"
        Me.btbRefresh.Text = "Refresh"
        Me.btbRefresh.ToolTipText = "[F5] Retrieve Recent Changes "
        '
        'ToolBarButton6
        '
        Me.ToolBarButton6.Name = "ToolBarButton6"
        Me.ToolBarButton6.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btbExit
        '
        Me.btbExit.ImageIndex = 4
        Me.btbExit.Name = "btbExit"
        Me.btbExit.Text = "Exit"
        Me.btbExit.ToolTipText = "[F10] Close Current Window"
        '
        'ActiveMode
        '
        Me.ActiveMode.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.ActiveMode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ActiveMode.ForeColor = System.Drawing.Color.Maroon
        Me.ActiveMode.Location = New System.Drawing.Point(754, 8)
        Me.ActiveMode.Name = "ActiveMode"
        Me.ActiveMode.Size = New System.Drawing.Size(105, 20)
        Me.ActiveMode.TabIndex = 3
        Me.ActiveMode.Text = "Browse"
        Me.ActiveMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblRec
        '
        Me.LblRec.AutoSize = True
        Me.LblRec.ForeColor = System.Drawing.Color.Black
        Me.LblRec.Location = New System.Drawing.Point(705, 13)
        Me.LblRec.Name = "LblRec"
        Me.LblRec.Size = New System.Drawing.Size(24, 13)
        Me.LblRec.TabIndex = 4
        Me.LblRec.Text = "0/0"
        '
        'LblDocId
        '
        Me.LblDocId.AutoSize = True
        Me.LblDocId.ForeColor = System.Drawing.Color.Black
        Me.LblDocId.Location = New System.Drawing.Point(705, 26)
        Me.LblDocId.Name = "LblDocId"
        Me.LblDocId.Size = New System.Drawing.Size(54, 13)
        Me.LblDocId.TabIndex = 5
        Me.LblDocId.Text = "For DocId"
        Me.LblDocId.Visible = False
        '
        'Topctrl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.LblDocId)
        Me.Controls.Add(Me.LblRec)
        Me.Controls.Add(Me.ActiveMode)
        Me.Controls.Add(Me.ToolBar1)
        Me.Name = "Topctrl"
        Me.Size = New System.Drawing.Size(863, 39)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ToolBar1 As System.Windows.Forms.ToolBar
    Friend WithEvents btbAdd As System.Windows.Forms.ToolBarButton
    Friend WithEvents btbEdit As System.Windows.Forms.ToolBarButton
    Friend WithEvents btbDel As System.Windows.Forms.ToolBarButton
    Friend WithEvents btbCancel As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton1 As System.Windows.Forms.ToolBarButton
    Friend WithEvents btbFirst As System.Windows.Forms.ToolBarButton
    Friend WithEvents btbPrevious As System.Windows.Forms.ToolBarButton
    Friend WithEvents btbNext As System.Windows.Forms.ToolBarButton
    Friend WithEvents btbLast As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton2 As System.Windows.Forms.ToolBarButton
    Friend WithEvents btbFind As System.Windows.Forms.ToolBarButton
    Friend WithEvents btbPrint As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton4 As System.Windows.Forms.ToolBarButton
    Friend WithEvents btbSave As System.Windows.Forms.ToolBarButton
    Friend WithEvents btbDiscard As System.Windows.Forms.ToolBarButton
    Friend WithEvents btbSite As System.Windows.Forms.ToolBarButton
    Friend WithEvents btbRefresh As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton6 As System.Windows.Forms.ToolBarButton
    Friend WithEvents btbExit As System.Windows.Forms.ToolBarButton
    Friend WithEvents ActiveMode As System.Windows.Forms.Label
    Friend WithEvents LblRec As System.Windows.Forms.Label
    Public WithEvents LblDocId As System.Windows.Forms.Label

End Class
