Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms

Public Class ReportOptionsDialog

    Public Sub New(ByVal availableFields As List(Of String))

        InitializeComponent()

        Dim field As String
        For Each field In availableFields
            fieldsListBox.Items.Add(field)
        Next field
        Dim i As Integer
        For i = 0 To fieldsListBox.Items.Count - 1
            fieldsListBox.SetItemChecked(i, True)
        Next i

    End Sub

    Public Function GetSelectedFields() As List(Of String)
        Dim fields As List(Of String) = New List(Of String)
        For i As Int32 = 0 To fieldsListBox.CheckedItems.Count - 1
            fields.Add(fieldsListBox.CheckedItems(i).ToString())
        Next i
        Return fields
    End Function

    Private Sub okButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles okButton.Click
        If fieldsListBox.CheckedItems.Count = 0 Then
            MessageBox.Show("At least one field must be selected")
            Return
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
End Class