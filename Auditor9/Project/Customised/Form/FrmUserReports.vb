Public Class FrmUserReports
    'Dim WithEvents Dgl1 As New AgControls.AgDataGrid

    Dim mMainReportCode As String = ""
    Dim mQry As String
    Public UserReportCode As String
    Public UserReportDesc As String



    Private Const Col_Sno As Byte = 0
    Private Const Col1Report As Byte = 1
    Private Const Col1Show As Byte = 2
    Private Const Col1Delete As Byte = 3

    Public Property MainReportCode() As String
        Get
            MainReportCode = mMainReportCode
        End Get
        Set(ByVal value As String)
            mMainReportCode = value
        End Set
    End Property


    Sub Ini_Grid()
        'Dgl1.Height = Pnl1.Height
        'Dgl1.Width = Pnl1.Width
        'Dgl1.Top = Pnl1.Top
        'Dgl1.Left = Pnl1.Left
        'Pnl1.Visible = False
        'Dgl1.Visible = True
        'Dgl1.BringToFront()


        With AgCL
            .AddAgTextColumn(Dgl1, "Dgl1SNo", 40, 5, "S.No.", True, True, False)
            .AddAgTextColumn(Dgl1, "Dgl1Report", 150, 50, "Report Name", True, True, False)
            AgL.AddButtonColumn(Dgl1, "Dgl1Show", 50, "Show")
            AgL.AddButtonColumn(Dgl1, "Dgl1Delete", 50, "Delete")
        End With


        Dgl1.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        AgL.FSetSNo(Dgl1, Col_Sno)
        Dgl1.TabIndex = Pnl1.TabIndex
        Dgl1.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Arial"), 9)
        Dgl1.DefaultCellStyle.Font = New Font(New FontFamily("Arial"), 8)
    End Sub

    Sub MoveRec()
        Dim mQry As String
        Dim DtTemp As DataTable
        Dim I As Integer


        mQry = "Select Code, Description From Report_User Where Report_Main = '" & MainReportCode & "'"
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()
            With DtTemp
                For I = 0 To .Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(Col_Sno, I).Value = I + 1
                    Dgl1.Item(Col1Report, I).Value = .Rows(I)("Description")
                    Dgl1.Item(Col1Report, I).Tag = .Rows(I)("Code")
                Next
            End With
        End If
        Dgl1.AllowUserToAddRows = False

    End Sub
    Private Sub FrmUserReports_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.GridDesign(Dgl1)
        Ini_Grid()

        MoveRec()
        Dgl1.Visible = True
    End Sub

    Private Sub Dgl1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellClick
        Try
            Select Case e.ColumnIndex
                Case Col1Show
                    UserReportCode = Dgl1.Item(Col1Report, Dgl1.CurrentCell.RowIndex).Tag
                    UserReportDesc = Dgl1.Item(Col1Report, Dgl1.CurrentCell.RowIndex).Value
                    Me.Hide()
                Case Col1Delete
                    If MsgBox("Sure to Delete Report?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        If Dgl1.Item(Col1Report, Dgl1.CurrentCell.RowIndex).Value <> "" Then
                            mQry = "Delete From Report_User Where Code = '" & Dgl1.Item(Col1Report, Dgl1.CurrentCell.RowIndex).Tag & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                            mQry = "Delete From Report_Fields Where Code = '" & Dgl1.Item(Col1Report, Dgl1.CurrentCell.RowIndex).Tag & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                            mQry = "Delete From Report_Condition Where Code = '" & Dgl1.Item(Col1Report, Dgl1.CurrentCell.RowIndex).Tag & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                            mQry = "Delete From Report_Group Where Code = '" & Dgl1.Item(Col1Report, Dgl1.CurrentCell.RowIndex).Tag & "'"
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                            MoveRec()
                        End If
                    End If
            End Select
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Private Sub BtnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        UserReportCode = Dgl1.Item(Col1Report, Dgl1.CurrentCell.RowIndex).Tag
        Me.Hide()
    End Sub

    Private Sub BtnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        UserReportCode = ""
        Me.Hide()
    End Sub
End Class