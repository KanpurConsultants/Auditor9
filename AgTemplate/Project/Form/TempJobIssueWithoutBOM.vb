Public Class TempJobIssueWithoutBOM
    Inherits TempJobIssueCommon
    Protected WithEvents LblJobOrderNo As System.Windows.Forms.Label
    Protected WithEvents TxtJobOrder As AgControls.AgTextBox
    Dim mQry$

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.TxtJobOrder = New AgControls.AgTextBox
        Me.LblJobOrderNo = New System.Windows.Forms.Label
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
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(17, 231)
        Me.Pnl1.Size = New System.Drawing.Size(869, 181)
        '
        'TxtRemarks
        '
        Me.TxtRemarks.Location = New System.Drawing.Point(331, 134)
        Me.TxtRemarks.Size = New System.Drawing.Size(361, 18)
        '
        'Label30
        '
        Me.Label30.Location = New System.Drawing.Point(207, 135)
        '
        'LblJobIssueDetail
        '
        Me.LblJobIssueDetail.Location = New System.Drawing.Point(16, 210)
        '
        'TabControl1
        '
        Me.TabControl1.Size = New System.Drawing.Size(906, 189)
        '
        'TP1
        '
        Me.TP1.Controls.Add(Me.TxtJobOrder)
        Me.TP1.Controls.Add(Me.LblJobOrderNo)
        Me.TP1.Size = New System.Drawing.Size(898, 163)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
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
        Me.TP1.Controls.SetChildIndex(Me.TxtJobWorker, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblJobWorkerReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblJobOrderNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtJobOrder, 0)
        '
        'TxtJobOrder
        '
        Me.TxtJobOrder.AgMandatory = False
        Me.TxtJobOrder.AgMasterHelp = False
        Me.TxtJobOrder.AgNumberLeftPlaces = 0
        Me.TxtJobOrder.AgNumberNegetiveAllow = False
        Me.TxtJobOrder.AgNumberRightPlaces = 0
        Me.TxtJobOrder.AgPickFromLastValue = False
        Me.TxtJobOrder.AgRowFilter = ""
        Me.TxtJobOrder.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtJobOrder.AgSelectedValue = Nothing
        Me.TxtJobOrder.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtJobOrder.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtJobOrder.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtJobOrder.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtJobOrder.Location = New System.Drawing.Point(331, 114)
        Me.TxtJobOrder.MaxLength = 255
        Me.TxtJobOrder.Name = "TxtJobOrder"
        Me.TxtJobOrder.Size = New System.Drawing.Size(361, 18)
        Me.TxtJobOrder.TabIndex = 8
        '
        'LblJobOrderNo
        '
        Me.LblJobOrderNo.AutoSize = True
        Me.LblJobOrderNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobOrderNo.Location = New System.Drawing.Point(207, 115)
        Me.LblJobOrderNo.Name = "LblJobOrderNo"
        Me.LblJobOrderNo.Size = New System.Drawing.Size(84, 16)
        Me.LblJobOrderNo.TabIndex = 741
        Me.LblJobOrderNo.Text = "Job Order No"
        '
        'TempJobIssueWithoutBOM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(903, 492)
        Me.LogLineTableCsv = "JobIssueDetail_Log"
        Me.LogTableName = "JobIssRec_Log"
        Me.MainLineTableCsv = "JobIssueDetail"
        Me.MainTableName = "JobIssRec"
        Me.Name = "TempJobIssueWithoutBOM"
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

    'Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
    '    If Dgl1.CurrentCell Is Nothing Then Exit Sub
    '    Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
    '        Case Col1Item
    '            Dgl1.AgHelpDataSet(Col1Item, 9) = HelpDataSet.Item
    '            Dgl1.AgRowFilter(Dgl1.Columns(Col1Item).Index) = " IsDeleted = 0 " & _
    '                " And Status ='" & AgTemplate.ClsMain.EntryStatus.Active & "' "
    '    End Select
    'End Sub

    'Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
    '    mQry = "UPDATE JobIssRec_Log " & _
    '            " SET " & _
    '            " JobOrder = " & AgL.Chk_Text(TxtJobOrder.AgSelectedValue) & " " & _
    '            " Where UID = '" & mSearchCode & "'"
    '    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    'End Sub

    'Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
    '    Dim DsTemp As DataSet

    '    If FrmType = ClsMain.EntryPointType.Main Then
    '        mQry = "Select J.* " & _
    '            " From JobIssRec J " & _
    '            " Where J.DocID='" & SearchCode & "'"
    '    Else
    '        mQry = "Select J.* " & _
    '            " From JobIssRec_Log J " & _
    '            " Where J.UID='" & SearchCode & "'"

    '    End If
    '    DsTemp = AgL.FillData(mQry, AgL.GCn)
    '    With DsTemp.Tables(0)
    '        If .Rows.Count > 0 Then
    '            TxtJobOrder.AgSelectedValue = AgL.XNull(.Rows(0)("JobOrder"))
    '        End If
    '    End With
    'End Sub

    'Private Sub TempJobIssueWithoutBOM_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
    '    TxtJobOrder.AgHelpDataSet(6, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = HelpDataSet.JobOrder
    'End Sub

    'Private Sub TxtJobOrder_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtJobOrder.Enter
    '    Try
    '        TxtJobOrder.AgRowFilter = " IsDeleted = 0 " & _
    '            " And Status = '" & ClsMain.EntryStatus.Active & "' " & _
    '            " And " & ClsMain.RetDivFilterStr & " " & _
    '            " And JobWorker = '" & TxtJobWorker.AgSelectedValue & "' " & _
    '            " And JobOrderDate <= '" & TxtV_Date.Text & "' "
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    'Private Sub TempJobIssueWithoutBOM_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
    '    Dgl1.Columns(Col1JobOrder).Visible = False
    'End Sub

    'Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
    '    If Topctrl1.Mode = "Browse" Then Exit Sub
    '    Dim mRowIndex As Integer, mColumnIndex As Integer
    '    Dim DrTemp As DataRow() = Nothing
    '    Try
    '        mRowIndex = Dgl1.CurrentCell.RowIndex
    '        mColumnIndex = Dgl1.CurrentCell.ColumnIndex
    '        If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
    '        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
    '            Case Col1Item
    '                Dgl1.AgSelectedValue(Col1JobOrder, mRowIndex) = TxtJobOrder.AgSelectedValue
    '        End Select
    '        Call Calculation()
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub
End Class
