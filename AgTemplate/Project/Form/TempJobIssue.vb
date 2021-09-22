Public Class TempJobIssue
    Inherits TempJobIssueCommon
    Protected WithEvents LblForJobOrder As System.Windows.Forms.Label
    Protected WithEvents TxtForJobOrder As AgControls.AgTextBox

    Dim mQry$

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.TxtForJobOrder = New AgControls.AgTextBox
        Me.LblForJobOrder = New System.Windows.Forms.Label
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
        Me.SuspendLayout()
        '
        'TxtGodown
        '
        Me.TxtGodown.AgSelectedValue = ""
        Me.TxtGodown.BackColor = System.Drawing.Color.White
        Me.TxtGodown.Tag = ""
        '
        'LblGodown
        '
        Me.LblGodown.Tag = ""
        '
        'TxtRemarks
        '
        Me.TxtRemarks.AgSelectedValue = ""
        Me.TxtRemarks.BackColor = System.Drawing.Color.White
        Me.TxtRemarks.Tag = ""
        '
        'Label30
        '
        Me.Label30.Tag = ""
        '
        'LblGodownReq
        '
        Me.LblGodownReq.Tag = ""
        '
        'TxtManualRefNo
        '
        Me.TxtManualRefNo.AgSelectedValue = ""
        Me.TxtManualRefNo.BackColor = System.Drawing.Color.White
        Me.TxtManualRefNo.Size = New System.Drawing.Size(110, 18)
        Me.TxtManualRefNo.Tag = ""
        '
        'LblManualRefNo
        '
        Me.LblManualRefNo.Tag = ""
        '
        'TxtProcess
        '
        Me.TxtProcess.AgSelectedValue = ""
        Me.TxtProcess.BackColor = System.Drawing.Color.White
        Me.TxtProcess.Location = New System.Drawing.Point(770, 115)
        Me.TxtProcess.Tag = ""
        '
        'LblProcess
        '
        Me.LblProcess.Location = New System.Drawing.Point(769, 95)
        Me.LblProcess.Tag = ""
        '
        'LblJobWorkerReq
        '
        Me.LblJobWorkerReq.Tag = ""
        '
        'TxtJobWorker
        '
        Me.TxtJobWorker.AgSelectedValue = ""
        Me.TxtJobWorker.BackColor = System.Drawing.Color.White
        Me.TxtJobWorker.Tag = ""
        '
        'LblJobWorker
        '
        Me.LblJobWorker.Tag = ""
        '
        'LblJobIssueDetail
        '
        Me.LblJobIssueDetail.Tag = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(240, 451)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(470, 451)
        '
        'LblPrefix
        '
        Me.LblPrefix.Location = New System.Drawing.Point(830, 96)
        '
        'TP1
        '
        Me.TP1.Controls.Add(Me.TxtForJobOrder)
        Me.TP1.Controls.Add(Me.LblForJobOrder)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblGodownReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblJobWorker, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtJobWorker, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblJobWorkerReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblForJobOrder, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtForJobOrder, 0)
        '
        'TxtForJobOrder
        '
        Me.TxtForJobOrder.AgMandatory = False
        Me.TxtForJobOrder.AgMasterHelp = False
        Me.TxtForJobOrder.AgNumberLeftPlaces = 0
        Me.TxtForJobOrder.AgNumberNegetiveAllow = False
        Me.TxtForJobOrder.AgNumberRightPlaces = 0
        Me.TxtForJobOrder.AgPickFromLastValue = False
        Me.TxtForJobOrder.AgRowFilter = ""
        Me.TxtForJobOrder.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtForJobOrder.AgSelectedValue = Nothing
        Me.TxtForJobOrder.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtForJobOrder.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtForJobOrder.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtForJobOrder.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtForJobOrder.Location = New System.Drawing.Point(562, 54)
        Me.TxtForJobOrder.MaxLength = 255
        Me.TxtForJobOrder.Name = "TxtForJobOrder"
        Me.TxtForJobOrder.Size = New System.Drawing.Size(130, 18)
        Me.TxtForJobOrder.TabIndex = 744
        '
        'LblForJobOrder
        '
        Me.LblForJobOrder.AutoSize = True
        Me.LblForJobOrder.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblForJobOrder.Location = New System.Drawing.Point(447, 54)
        Me.LblForJobOrder.Name = "LblForJobOrder"
        Me.LblForJobOrder.Size = New System.Drawing.Size(87, 16)
        Me.LblForJobOrder.TabIndex = 745
        Me.LblForJobOrder.Text = "For Job Order"
        '
        'TempJobIssue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(903, 492)
        Me.LogLineTableCsv = "JobIssueDetail_Log"
        Me.LogTableName = "JobIssRec_Log"
        Me.MainLineTableCsv = "JobIssueDetail"
        Me.MainTableName = "JobIssRec"
        Me.Name = "TempJobIssue"
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
        Me.ResumeLayout(False)

    End Sub
#End Region

    Private Sub TempJobIssue_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd

    End Sub

    Private Sub TempJobIssue_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        With AgCL

        End With
    End Sub

    Private Sub TempJobIssue_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec

    End Sub
End Class
