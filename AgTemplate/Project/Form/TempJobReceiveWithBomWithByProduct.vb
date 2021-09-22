Public Class TempJobReceiveWithBomWithByProduct
    Inherits TempJobReceive
    Dim mQry$



    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.LblByProductDetail = New System.Windows.Forms.LinkLabel
        Me.Pnl3 = New System.Windows.Forms.Panel
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.LblTotalByProductMeasure = New System.Windows.Forms.Label
        Me.LblTotalByProductMeasureText = New System.Windows.Forms.Label
        Me.LblTotalByProductQty = New System.Windows.Forms.Label
        Me.LblTotalByProductQtyText = New System.Windows.Forms.Label
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GrpUP.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TP1.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'LinkLabel1
        '
        Me.LinkLabel1.Location = New System.Drawing.Point(3, 282)
        Me.LinkLabel1.Size = New System.Drawing.Size(126, 18)
        '
        'Panel2
        '
        Me.Panel2.Location = New System.Drawing.Point(4, 415)
        Me.Panel2.Size = New System.Drawing.Size(958, 23)
        '
        'Pnl2
        '
        Me.Pnl2.Location = New System.Drawing.Point(4, 301)
        Me.Pnl2.Size = New System.Drawing.Size(958, 114)
        '
        'BtnFill
        '
        Me.BtnFill.Location = New System.Drawing.Point(913, 280)
        '
        'Panel1
        '
        Me.Panel1.Location = New System.Drawing.Point(3, 257)
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(3, 137)
        Me.Pnl1.Size = New System.Drawing.Size(961, 120)
        '
        'LblJobReceiveDetail
        '
        Me.LblJobReceiveDetail.Size = New System.Drawing.Size(136, 18)
        '
        'Topctrl1
        '
        Me.Topctrl1.TabIndex = 4
        '
        'LblByProductDetail
        '
        Me.LblByProductDetail.BackColor = System.Drawing.Color.SteelBlue
        Me.LblByProductDetail.DisabledLinkColor = System.Drawing.Color.White
        Me.LblByProductDetail.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblByProductDetail.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblByProductDetail.LinkColor = System.Drawing.Color.White
        Me.LblByProductDetail.Location = New System.Drawing.Point(3, 440)
        Me.LblByProductDetail.Name = "LblByProductDetail"
        Me.LblByProductDetail.Size = New System.Drawing.Size(128, 17)
        Me.LblByProductDetail.TabIndex = 739
        Me.LblByProductDetail.TabStop = True
        Me.LblByProductDetail.Text = "By Product Detail"
        Me.LblByProductDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Pnl3
        '
        Me.Pnl3.Location = New System.Drawing.Point(3, 458)
        Me.Pnl3.Name = "Pnl3"
        Me.Pnl3.Size = New System.Drawing.Size(613, 94)
        Me.Pnl3.TabIndex = 3
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel4.Controls.Add(Me.LblTotalByProductMeasure)
        Me.Panel4.Controls.Add(Me.LblTotalByProductMeasureText)
        Me.Panel4.Controls.Add(Me.LblTotalByProductQty)
        Me.Panel4.Controls.Add(Me.LblTotalByProductQtyText)
        Me.Panel4.Location = New System.Drawing.Point(3, 552)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(613, 23)
        Me.Panel4.TabIndex = 738
        '
        'LblTotalByProductMeasure
        '
        Me.LblTotalByProductMeasure.AutoSize = True
        Me.LblTotalByProductMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalByProductMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalByProductMeasure.Location = New System.Drawing.Point(284, 3)
        Me.LblTotalByProductMeasure.Name = "LblTotalByProductMeasure"
        Me.LblTotalByProductMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalByProductMeasure.TabIndex = 666
        Me.LblTotalByProductMeasure.Text = "."
        Me.LblTotalByProductMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblTotalByProductMeasure.Visible = False
        '
        'LblTotalByProductMeasureText
        '
        Me.LblTotalByProductMeasureText.AutoSize = True
        Me.LblTotalByProductMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalByProductMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalByProductMeasureText.Location = New System.Drawing.Point(176, 3)
        Me.LblTotalByProductMeasureText.Name = "LblTotalByProductMeasureText"
        Me.LblTotalByProductMeasureText.Size = New System.Drawing.Size(106, 16)
        Me.LblTotalByProductMeasureText.TabIndex = 665
        Me.LblTotalByProductMeasureText.Text = "Total Measure :"
        Me.LblTotalByProductMeasureText.Visible = False
        '
        'LblTotalByProductQty
        '
        Me.LblTotalByProductQty.AutoSize = True
        Me.LblTotalByProductQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalByProductQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalByProductQty.Location = New System.Drawing.Point(116, 3)
        Me.LblTotalByProductQty.Name = "LblTotalByProductQty"
        Me.LblTotalByProductQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalByProductQty.TabIndex = 660
        Me.LblTotalByProductQty.Text = "."
        Me.LblTotalByProductQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalByProductQtyText
        '
        Me.LblTotalByProductQtyText.AutoSize = True
        Me.LblTotalByProductQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalByProductQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalByProductQtyText.Location = New System.Drawing.Point(31, 3)
        Me.LblTotalByProductQtyText.Name = "LblTotalByProductQtyText"
        Me.LblTotalByProductQtyText.Size = New System.Drawing.Size(73, 16)
        Me.LblTotalByProductQtyText.TabIndex = 659
        Me.LblTotalByProductQtyText.Text = "Total Qty :"
        '
        'TempJobReceiveWithBomWithByProduct
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(965, 616)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Pnl3)
        Me.Controls.Add(Me.LblByProductDetail)
        Me.Name = "TempJobReceiveWithBomWithByProduct"
        Me.Controls.SetChildIndex(Me.LblByProductDetail, 0)
        Me.Controls.SetChildIndex(Me.Pnl3, 0)
        Me.Controls.SetChildIndex(Me.Panel4, 0)
        Me.Controls.SetChildIndex(Me.PnlCalcGrid, 0)
        Me.Controls.SetChildIndex(Me.BtnFill, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.Panel2, 0)
        Me.Controls.SetChildIndex(Me.Pnl2, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.LblJobReceiveDetail, 0)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GBoxMoveToLog.ResumeLayout(False)
        Me.GBoxMoveToLog.PerformLayout()
        Me.GBoxApprove.ResumeLayout(False)
        Me.GBoxApprove.PerformLayout()
        Me.GBoxEntryType.ResumeLayout(False)
        Me.GBoxEntryType.PerformLayout()
        Me.GrpUP.ResumeLayout(False)
        Me.GrpUP.PerformLayout()
        Me.GBoxDivision.ResumeLayout(False)
        Me.GBoxDivision.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TP1.ResumeLayout(False)
        Me.TP1.PerformLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents LblByProductDetail As System.Windows.Forms.LinkLabel
    Protected WithEvents Pnl3 As System.Windows.Forms.Panel
    Protected WithEvents Panel4 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalByProductMeasure As System.Windows.Forms.Label
    Protected WithEvents LblTotalByProductMeasureText As System.Windows.Forms.Label
    Protected WithEvents LblTotalByProductQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalByProductQtyText As System.Windows.Forms.Label
#End Region






End Class
