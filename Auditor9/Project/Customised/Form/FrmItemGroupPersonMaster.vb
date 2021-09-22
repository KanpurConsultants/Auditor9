Imports System.ComponentModel
Imports AgLibrary.ClsMain.agConstants
Imports Customised.ClsMain

Public Class FrmItemGroupPersonMaster
    Public WithEvents DglMain As New AgControls.AgDataGrid
    Public Const Col1Head As String = "Head"
    Public Const Col1Value As String = "Value"
    Public Const Col1HeadOriginal As String = "Head Original"

    Dim rowParty As Integer = 0
    Dim rowItemGroup As Integer = 1
    Dim rowDiscountPer As Integer = 2
    Dim rowAdditionalDiscountPer As Integer = 3
    Dim rowAdditionPer As Integer = 4

    Public Const hcParty As String = "Party"
    Public Const hcItemGroup As String = "Item Group"
    Public Const hcDiscountPer As String = "Discount Per"
    Public Const hcAdditionalDiscountPer As String = "Additional Discount Per"
    Public Const hcAdditionPer As String = "Addition Per"

    Dim mQry As String = ""
    Public Sub Ini_Grid()
        DglMain.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(DglMain, Col1Head, 250, 0, Col1Head, True, True)
            .AddAgTextColumn(DglMain, Col1HeadOriginal, 150, 255, Col1HeadOriginal, False, True)
            .AddAgTextColumn(DglMain, Col1Value, 400, 0, Col1Value, True, False)
        End With
        AgL.AddAgDataGrid(DglMain, Pnl1)
        DglMain.EnableHeadersVisualStyles = False
        DglMain.ColumnHeadersHeight = 35
        DglMain.ColumnHeadersVisible = False
        DglMain.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgL.GridDesign(DglMain)
        DglMain.AgAllowFind = False
        DglMain.AllowUserToAddRows = False
        DglMain.AgSkipReadOnlyColumns = True

        DglMain.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Top
        DglMain.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        AgCL.GridSetiingShowXml(Me.Text & DglMain.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, DglMain, False)


        DglMain.Rows.Add(5)

        DglMain.Item(Col1Head, rowParty).Value = hcParty
        DglMain.Item(Col1Head, rowItemGroup).Value = hcItemGroup
        DglMain.Item(Col1Head, rowDiscountPer).Value = hcDiscountPer
        DglMain.Item(Col1Head, rowAdditionalDiscountPer).Value = hcAdditionalDiscountPer
        DglMain.Item(Col1Head, rowAdditionPer).Value = hcAdditionPer

        DglMain.Item(Col1Value, rowParty).Style.BackColor = Color.White
    End Sub
    Private Sub FrmReconcile_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ini_Grid()
        Me.StartPosition = FormStartPosition.CenterParent
    End Sub
    Private Sub FrmImportPurchaseFromExcel_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub DglMain_ColumnDisplayIndexChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles DglMain.ColumnDisplayIndexChanged
        Try
            AgCL.GridSetiingWriteXml(Me.Text & DglMain.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, DglMain)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub DglMain_ColumnWidthChanged(sender As Object, e As DataGridViewColumnEventArgs) Handles DglMain.ColumnWidthChanged
        Try
            AgCL.GridSetiingWriteXml(Me.Text & DglMain.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, DglMain)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub
    Private Sub DglMain_EditingControl_KeyDown(sender As Object, e As KeyEventArgs) Handles DglMain.EditingControl_KeyDown
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = DglMain.CurrentCell.RowIndex
            bColumnIndex = DglMain.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If bColumnIndex <> DglMain.Columns(Col1Value).Index Then Exit Sub

            Select Case DglMain.CurrentCell.RowIndex
                Case rowParty
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Sg.Code, Sg.Name FROM ViewHelpSubgroup Sg 
                                    Where Sg.SubGroupType = '" & SubgroupType.Customer & "' 
                                    ORDER BY Sg.Name "
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If

                Case rowItemGroup
                    If e.KeyCode <> Keys.Enter Then
                        If DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag Is Nothing Then
                            mQry = "SELECT Ig.Code, Ig.Description FROM ItemGroup Ig ORDER BY Ig.Description "
                            DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag = AgL.FillData(mQry, AgL.GCn)
                        End If

                        If DglMain.AgHelpDataSet(Col1Value) Is Nothing Then
                            DglMain.AgHelpDataSet(Col1Value) = DglMain.Item(Col1Head, DglMain.CurrentCell.RowIndex).Tag
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub DglMain_EditingControl_Validating(sender As Object, e As CancelEventArgs) Handles DglMain.EditingControl_Validating
        Try
            Select Case DglMain.CurrentCell.RowIndex
                Case rowParty, rowItemGroup
                    FSetPersonalDiscount()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FSetPersonalDiscount()
        Dim DtItem As DataTable
        mQry = "Select * 
                from ItemGroupPerson With (NoLock) 
                Where ItemGroup  = '" & DglMain.Item(Col1Value, rowItemGroup).Tag & "'
                And Person  = '" & DglMain.Item(Col1Value, rowParty).Tag & "' "
        DtItem = AgL.FillData(mQry, AgL.GCn).tables(0)

        If DtItem.Rows.Count > 0 Then
            DglMain.Item(Col1Value, rowDiscountPer).Value = AgL.VNull(DtItem.Rows(0)("DiscountPer"))
            DglMain.Item(Col1Value, rowAdditionalDiscountPer).Value = AgL.VNull(DtItem.Rows(0)("AdditionalDiscountPer"))
            DglMain.Item(Col1Value, rowAdditionPer).Value = AgL.VNull(DtItem.Rows(0)("AdditionPer"))
        End If
    End Sub
    Private Sub FSave()
        mQry = "Select Count(*) As Cnt
                from ItemGroupPerson With (NoLock) 
                Where ItemGroup  = '" & DglMain.Item(Col1Value, rowItemGroup).Tag & "'
                And Person  = '" & DglMain.Item(Col1Value, rowParty).Tag & "' "

        Try
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar()) = 0 Then
                mQry = " Inster Into ItemGroupPerson(ItemGroup, Person, DiscountPer, AdditionalDiscountPer, AdditionPer)
                    Values(" & AgL.Chk_Text(DglMain.Item(Col1Value, rowItemGroup).Tag) & ", 
                    " & AgL.Chk_Text(DglMain.Item(Col1Value, rowParty).Tag) & "
                    " & Val(DglMain.Item(Col1Value, rowDiscountPer).Value) & "
                    " & Val(DglMain.Item(Col1Value, rowAdditionalDiscountPer).Value) & "
                    " & Val(DglMain.Item(Col1Value, rowAdditionPer).Value) & ")"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                mQry = "UPDATE ItemGroupPerson
                    SET DiscountPer = " & Val(DglMain.Item(Col1Value, rowDiscountPer).Value) & ",
	                    AdditionalDiscountPer = " & Val(DglMain.Item(Col1Value, rowAdditionalDiscountPer).Value) & ",
	                    AdditionPer = " & Val(DglMain.Item(Col1Value, rowAdditionPer).Value) & " 
                        Where ItemCategory Is Null And ItemGroup  = '" & DglMain.Item(Col1Value, rowItemGroup).Tag & "'
                        And Person  = '" & DglMain.Item(Col1Value, rowParty).Tag & "' "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
            MsgBox("Saved Successfully.", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub DglMain_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DglMain.CellEnter
        Try
            If DglMain.CurrentCell Is Nothing Then Exit Sub
            If DglMain.CurrentCell.ColumnIndex = DglMain.Columns(Col1Value).Index Then
                DglMain.AgHelpDataSet(DglMain.CurrentCell.ColumnIndex) = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        FSave()
    End Sub
End Class