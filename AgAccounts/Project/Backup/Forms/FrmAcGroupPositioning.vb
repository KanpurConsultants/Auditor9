Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmAcGroupPositioning
    Private IntSNo As Integer
    Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Private Sub FrmAcGroupPositioning_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Agl.WinSetting(Me, 665, 705, 0, 0)
        FFillGrid()
    End Sub
    Private Sub FrmAcGroupPositioning_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(sender, e, 0)
        LblBG.BackColor = Color.LemonChiffon
    End Sub
    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles BtnClose.Click, BtnDown.Click, BtnUp.Click, BtnSave.Click, BtnDefault.Click

        Select Case sender.name
            Case BtnDefault.Name
                FSetDefault()
            Case BtnClose.Name
                Me.Close()
            Case BtnDown.Name
                FMoveDown()
            Case BtnUp.Name
                FMoveUp()
            Case BtnSave.Name
                IntSNo = 0
                FDelete()
                FTraverseNode(TrvMain.Nodes(0))
                MsgBox("Record Saved Successfully.")
        End Select
    End Sub
    Private Sub FSetDefault()
        Dim SQlCmd As New SqlClient.SqlCommand

        If MsgBox("Are You Sure? You Want To Set Defaut Positioning.") = MsgBoxResult.Yes Then
            SQlCmd.Connection = Agl.Gcn
            SQlCmd.CommandText = "Delete From AcGroupPositioning"
            SQlCmd.ExecuteNonQuery()
            SQlCmd.Dispose()
            FFillGrid()
        End If
    End Sub
    Private Sub FFillGrid()
        Dim DTTemp As DataTable
        Dim StrSQL As String
        Dim I As Integer
        Dim StrGroupUnder As String
        Dim StrGroupCode As String
        Dim TrvNode As TreeNode

        If CMain.FGetMaxNo("Select Count(*) As Cnt From AcGroupPositioning ", Agl.Gcn) > 0 Then
            StrSQL = "Select GroupCode,Max(GroupName) As GroupName,Max(V_SNo) As V_SNo,Max(GUnder) As GUnder,Max(GUnderCode) As GUnderCode, "
            StrSQL += "Max(Level) As Level,Max(ExpandGroup) As ExpandGroup  "
            StrSQL += "From "
            StrSQL += "(  "
            StrSQL += "Select	AG.GroupCode,AG.GroupName,Null As V_SNo,AG.GroupName As GUnder,AG.GroupCode As GUnderCode, "
            StrSQL += "Null As Level,Null As ExpandGroup  "
            StrSQL += "From AcGroup AG  "
            StrSQL += "Where AG.GroupCode Not In  "
            StrSQL += "(Select AGP.GroupCode From AcGroupPath AGP)  "
            StrSQL += "Union All "
            StrSQL += "Select	AGP.GroupCode,AG.GroupName,Null As V_SNo,AGGU.GroupName As GUnder,  "
            StrSQL += "AGP.GroupUnder As GUnderCode,Null As Level,Null As ExpandGroup  "
            StrSQL += "From AcGroupPath AGP Left Join  "
            StrSQL += "AcGroup AG On AGP.GroupCode=AG.GroupCode Left Join  "
            StrSQL += "AcGroup AGGU On AGP.GroupUnder=AGGU.GroupCode  "
            StrSQL += "Where AGP.SNo In  "
            StrSQL += "(Select Max(AGP1.SNo) From AcGroupPath AGP1 Where AGP1.GroupCode=AGP.GroupCode)  "
            StrSQL += "Union All "
            StrSQL += "Select	AGP.GroupCode,Null As GroupName,AGP.V_SNo,Null As GUnder,Null As GUnderCode, "
            StrSQL += "AGP.Level, AGP.ExpandGroup "
            StrSQL += "From AcGroupPositioning AGP "
            StrSQL += ") As Tmp  "
            StrSQL += "Group By GroupCode "
            StrSQL += "Having IsNull(Max(GroupName),'')<>'' "
            StrSQL += "Order By IsNull(Max(V_SNo),32767),Max(GroupName) "
        Else
            StrSQL = "Select GroupCode,Max(GroupName) As GroupName,Max(V_SNo) As V_SNo,Max(GUnder) As GUnder,Max(GUnderCode) As GUnderCode, "
            StrSQL += "Max(Level) As Level,Max(ExpandGroup) As ExpandGroup  "
            StrSQL += "From "
            StrSQL += "(  "
            StrSQL += "Select	AG.GroupCode,AG.GroupName,1 As V_SNo,AG.GroupName As GUnder,AG.GroupCode As GUnderCode, "
            StrSQL += "1 As Level,'Y' As ExpandGroup  "
            StrSQL += "From AcGroup AG  "
            StrSQL += "Where AG.GroupCode Not In  "
            StrSQL += "(Select AGP.GroupCode From AcGroupPath AGP)  "
            StrSQL += "Union All "
            StrSQL += "Select	AGP.GroupCode,AG.GroupName,2 As V_SNo,AGGU.GroupName As GUnder,  "
            StrSQL += "AGP.GroupUnder As GUnderCode,Null As Level,'Y' As ExpandGroup  "
            StrSQL += "From AcGroupPath AGP Left Join  "
            StrSQL += "AcGroup AG On AGP.GroupCode=AG.GroupCode Left Join  "
            StrSQL += "AcGroup AGGU On AGP.GroupUnder=AGGU.GroupCode  "
            StrSQL += "Where AGP.SNo In  "
            StrSQL += "(Select Max(AGP1.SNo) From AcGroupPath AGP1 Where AGP1.GroupCode=AGP.GroupCode)  "
            StrSQL += ") As Tmp  "
            StrSQL += "Group By GroupCode "
            StrSQL += "Having IsNull(Max(GroupName),'')<>'' "
            StrSQL += "Order By Max(V_SNo),Max(GroupName) "
        End If

        DTTemp = cmain.FGetDatTable(StrSQL, Agl.Gcn)
        TrvMain.Nodes.Clear()
        TrvMain.Nodes.Add("|Main|", "Account Groups")
        TrvMain.Nodes(0).Name = "Account Groups"
        TrvMain.Nodes(0).Tag = "|Main|"
        For I = 0 To DTTemp.Rows.Count - 1
            StrGroupCode = Agl.Xnull(DTTemp.Rows(I).Item("GroupCode"))
            StrGroupUnder = Agl.Xnull(DTTemp.Rows(I).Item("GUnderCode"))
            TrvNode = FGetNode(StrGroupUnder, TrvMain.TopNode)
            If TrvNode Is Nothing Then TrvNode = TrvMain.TopNode
            TrvNode.Nodes.Add(Agl.Xnull(DTTemp.Rows(I).Item("GroupCode")), _
                              Agl.Xnull(DTTemp.Rows(I).Item("GroupName")))
            TrvNode.Nodes(StrGroupCode).Tag = Agl.Xnull(DTTemp.Rows(I).Item("GroupCode"))
            If UCase(Trim(Agl.Xnull(DTTemp.Rows(I).Item("ExpandGroup")))) = "N" Then TrvNode.Nodes(StrGroupCode).Checked = True
        Next
        TrvMain.ExpandAll()
    End Sub
    Public Function FGetNode(ByVal StrFindTag As String, ByVal TrvNode As TreeNode) As TreeNode
        Dim StrTag As String
        Dim TrvRtnNode As TreeNode = Nothing

        StrTag = TrvNode.Tag
        If StrTag = StrFindTag Then
            TrvRtnNode = TrvNode
        Else
            If TrvNode.Nodes.Count > 0 Then TrvRtnNode = FGetNode(StrFindTag, TrvNode.FirstNode)
            If TrvRtnNode Is Nothing Then
                If Not TrvNode.NextNode Is Nothing Then TrvRtnNode = FGetNode(StrFindTag, TrvNode.NextNode)
            End If
        End If

        FGetNode = TrvRtnNode
    End Function
    Private Sub FMoveUp()
        Dim TrvSelectedNode As TreeNode
        Dim TrvParentNode As TreeNode
        Dim IntIndex As Integer

        Try
            TrvMain.SelectedNode.Collapse()
            If TrvMain.SelectedNode.PrevNode Is Nothing Then Exit Sub
            TrvParentNode = TrvMain.SelectedNode.Parent
            IntIndex = TrvMain.SelectedNode.PrevNode.Index
            TrvSelectedNode = TrvMain.SelectedNode

            If Not TrvSelectedNode.Equals(TrvParentNode.FirstNode) Then
                TrvMain.SelectedNode.Remove()
                TrvParentNode.Nodes.Insert(IntIndex, TrvSelectedNode)
                TrvMain.SelectedNode = TrvSelectedNode
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub FMoveDown()
        Dim TrvSelectedNode As TreeNode
        Dim TrvParentNode As TreeNode
        Dim IntIndex As Integer

        Try
            TrvMain.SelectedNode.Collapse()
            If TrvMain.SelectedNode.NextNode Is Nothing Then Exit Sub
            TrvParentNode = TrvMain.SelectedNode.Parent
            IntIndex = TrvMain.SelectedNode.NextNode.Index
            TrvSelectedNode = TrvMain.SelectedNode

            If Not TrvSelectedNode.Equals(TrvParentNode.LastNode) Then
                TrvMain.SelectedNode.Remove()
                TrvParentNode.Nodes.Insert(IntIndex, TrvSelectedNode)
                TrvMain.SelectedNode = TrvSelectedNode
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub FTraverseNode(ByVal TrvNode As TreeNode)
        If TrvNode Is Nothing Then Exit Sub
        FSave(TrvNode)
        If Not TrvNode.Checked Then
            If TrvNode.Nodes.Count > 0 Then
                FTraverseNode(TrvNode.Nodes(0))
                FTraverseNode(TrvNode.NextNode)
            Else
                FTraverseNode(TrvNode.NextNode)
            End If
        Else
            FTraverseNode(TrvNode.NextNode)
        End If
    End Sub
    Private Sub FDelete()
        Dim SQLCmd As New SqlClient.SqlCommand

        SQLCmd.Connection = Agl.Gcn
        SQLCmd.CommandText = "Delete From AcGroupPositioning "
        SQLCmd.ExecuteNonQuery()
        SQLCmd.Dispose()
    End Sub
    Private Sub FSave(ByVal TrvNode As TreeNode)
        Dim SQLCmd As New SqlClient.SqlCommand

        If UCase(Trim(TrvNode.Tag)) = "|MAIN|" Then Exit Sub
        IntSNo += 1
        SQLCmd.Connection = Agl.Gcn
        SQLCmd.CommandText = "Insert Into AcGroupPositioning(GroupCode,V_SNo,Level,ExpandGroup) Values( "
        SQLCmd.CommandText += "'" & TrvNode.Tag & "'," & IntSNo & "," & TrvNode.Level & ",'" & IIf(TrvNode.Checked, "N", "Y") & "') "
        SQLCmd.ExecuteNonQuery()
        SQLCmd.Dispose()
    End Sub
    Private Sub TrvMain_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TrvMain.AfterCheck
        If e.Node.Checked Then
            e.Node.Collapse()
            e.Node.BackColor = Color.AntiqueWhite
        Else
            e.Node.BackColor = Color.White
        End If
    End Sub
    Private Sub TrvMain_BeforeExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles TrvMain.BeforeExpand
        If e.Node.Checked Then e.Cancel = True
    End Sub
End Class