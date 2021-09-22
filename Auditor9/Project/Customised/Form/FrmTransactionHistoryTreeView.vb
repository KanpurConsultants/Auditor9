Imports System.Data.SqlClient
Imports System.Xml
Public Class FrmTransactionHistoryTreeView
    Dim mQry As String = ""
    Private Sub FrmTransactionHistoryTreeView_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FFormatTree()
    End Sub
    Public Sub PopulateTreeView(mDocId As String, mActionDateTime As String)
        Try
            mQry = "SELECT Convert(XML,Modifications) FROM LogTable WHERE DocId = '" & mDocId & "' AND U_EntDt = '" & mActionDateTime & "'"
            Dim xDoc As XmlDocument = New XmlDocument()
            Dim cmd As SqlCommand = New SqlCommand(mQry, AgL.GCn)
            Dim rdr As SqlDataReader = cmd.ExecuteReader()
            If rdr.Read() Then
                If rdr.GetSqlXml(0).IsNull = True Then rdr.Close() : Exit Sub
                xDoc.Load(rdr.GetSqlXml(0).CreateReader())
            End If
            rdr.Close()
            TreeView1.Nodes.Clear()
            TreeView1.Nodes.Add(New TreeNode(xDoc.DocumentElement.Name))
            Dim tNode As TreeNode = New TreeNode()
            tNode = CType(TreeView1.Nodes(0), TreeNode)
            AddTreeNode(xDoc.DocumentElement, tNode)
            TreeView1.ExpandAll()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub AddTreeNode(xmlNode As XmlNode, treeNode As TreeNode)
        Dim xNode As XmlNode
        Dim tNode As TreeNode
        Dim xNodeList As XmlNodeList

        If (xmlNode.HasChildNodes) Then
            xNodeList = xmlNode.ChildNodes
            For x As Integer = 0 To xNodeList.Count - 1
                xNode = xmlNode.ChildNodes(x)
                treeNode.Nodes.Add(New TreeNode(xNode.Name))
                tNode = treeNode.Nodes(x)
                AddTreeNode(xNode, tNode)
            Next
        Else
            treeNode.Text = xmlNode.OuterXml.Trim()
        End If
    End Sub
    Private Sub FFormatTree()
        Me.TreeView1.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    End Sub
    Private Sub FrmImportPurchaseFromExcel_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
End Class