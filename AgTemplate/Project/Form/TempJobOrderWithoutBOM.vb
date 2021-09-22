Public Class TempJobOrderWithoutBOM
    Inherits AgTemplate.TempJobOrderCommon

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GrpUP.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TP1.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Location = New System.Drawing.Point(4, 339)
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(4, 163)
        Me.Pnl1.Size = New System.Drawing.Size(972, 176)
        '
        'BtnFill
        '
        Me.BtnFill.Location = New System.Drawing.Point(873, 520)
        Me.BtnFill.Visible = False
        '
        'Pnl2
        '
        Me.Pnl2.Location = New System.Drawing.Point(716, 518)
        Me.Pnl2.Size = New System.Drawing.Size(140, 14)
        Me.Pnl2.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Location = New System.Drawing.Point(719, 551)
        Me.Panel2.Size = New System.Drawing.Size(258, 22)
        Me.Panel2.Visible = False
        '
        'LinkLabel5
        '
        Me.LinkLabel5.Location = New System.Drawing.Point(715, 498)
        Me.LinkLabel5.Visible = False
        '
        'LinkLabel1
        '
        Me.LinkLabel1.Location = New System.Drawing.Point(4, 142)
        '
        'TxtTermsAndConditions
        '
        Me.TxtTermsAndConditions.Location = New System.Drawing.Point(698, 387)
        Me.TxtTermsAndConditions.Size = New System.Drawing.Size(277, 92)
        '
        'LblTotalBomMeasure
        '
        Me.LblTotalBomMeasure.Location = New System.Drawing.Point(231, 6)
        '
        'LblTotalConsumptionMeasureText
        '
        Me.LblTotalConsumptionMeasureText.Location = New System.Drawing.Point(119, 6)
        Me.LblTotalConsumptionMeasureText.Visible = False
        '
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Location = New System.Drawing.Point(349, 366)
        Me.PnlCalcGrid.Size = New System.Drawing.Size(341, 113)
        '
        'LinkLabel2
        '
        Me.LinkLabel2.Location = New System.Drawing.Point(694, 366)
        Me.LinkLabel2.Size = New System.Drawing.Size(281, 19)
        '
        'Pnl3
        '
        Me.Pnl3.Location = New System.Drawing.Point(4, 386)
        Me.Pnl3.Size = New System.Drawing.Size(341, 93)
        '
        'LblJobInstructions
        '
        Me.LblJobInstructions.Location = New System.Drawing.Point(4, 366)
        Me.LblJobInstructions.Size = New System.Drawing.Size(341, 19)
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(832, 487)
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(653, 487)
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(466, 487)
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(150, 487)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 487)
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(2, 483)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(300, 487)
        '
        'TempJobOrderWithoutBOM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(984, 528)
        Me.LogLineTableCsv = "JobOrderdetail_Log,JobOrderBOM_Log,JobOrderQCInstruction_Log,Structure_TransFoote" & _
            "r_Log,Structure_TransLine_Log"
        Me.LogTableName = "JobOrder_Log"
        Me.MainLineTableCsv = "JobOrderdetail,JobOrderBOM,JobOrderQCInstruction,Structure_TransFooter,Structure_" & _
            "TransLine"
        Me.MainTableName = "JobOrder"
        Me.Name = "TempJobOrderWithoutBOM"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
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
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region

    Private Sub TempJobOrderWithoutBOM_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl2.Visible = False
    End Sub
End Class
