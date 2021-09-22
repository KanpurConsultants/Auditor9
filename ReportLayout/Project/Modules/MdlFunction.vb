Imports CrystalDecisions.CrystalReports.Engine
Imports System.Text

Module MdlFunction
    Public Sub NumPress(ByRef TEXT As System.Windows.Forms.TextBox, ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal LeftPlace As Integer, ByVal RightPlace As Integer, ByVal pAllowNegative As Boolean)
        On Error Resume Next
        Dim myString As String
        If RightPlace = 0 Then myString = "0123456789-" & TEXT.Tag Else myString = "0123456789.-" & TEXT.Tag
        If Asc(e.KeyChar) > 26 Then
            If InStr(myString, e.KeyChar) = 0 Then e.Handled = True
            If pAllowNegative <> True Then
                If (InStr(TEXT.Text, "-") <> 0) Or Asc(e.KeyChar) = 45 Then e.Handled = True
            End If
            If InStr(TEXT.Text, ".") <> 0 Then
                If Asc(e.KeyChar) = 46 Then e.Handled = True
                If InStr(TEXT.Text, "-") <> 0 Then
                    If InStr(TEXT.Text, ".") - 1 > LeftPlace And TEXT.SelectionStart < InStr(TEXT.Text, ".") Then
                        e.Handled = True
                    ElseIf Len(TEXT.Text) >= InStr(TEXT.Text, ".") + RightPlace And TEXT.SelectionStart >= InStr(TEXT.Text, ".") Then
                        e.Handled = True
                    End If
                Else
                    If InStr(TEXT.Text, ".") > LeftPlace And TEXT.SelectionStart < InStr(TEXT.Text, ".") Then
                        e.Handled = True
                    ElseIf Len(TEXT.Text) >= InStr(TEXT.Text, ".") + RightPlace And TEXT.SelectionStart >= InStr(TEXT.Text, ".") Then
                        e.Handled = True
                    End If
                End If
            Else
                If Asc(e.KeyChar) = 46 Then Exit Sub
                If InStr(TEXT.Text, "-") <> 0 Then
                    If Len(TEXT.Text) - 1 >= LeftPlace Then e.Handled = True
                Else
                    If Len(TEXT.Text) >= LeftPlace And Asc(e.KeyChar) <> 45 Then e.Handled = True
                End If
            End If
        ElseIf Asc(e.KeyChar) = 8 And InStr(TEXT.Text, "-") <> 0 And Mid(TEXT.Text, TEXT.SelectionStart, 1) = "." And Mid(TEXT.Text, TEXT.SelectionStart + 1, 1) <> "" And Len(TEXT.Text) - 1 - RightPlace >= LeftPlace Then
            e.Handled = True
        ElseIf Asc(e.KeyChar) = 8 And InStr(TEXT.Text, "-") = 0 And Mid(TEXT.Text, TEXT.SelectionStart, 1) = "." And Mid(TEXT.Text, TEXT.SelectionStart + 1, 1) <> "" And Len(TEXT.Text) - RightPlace >= LeftPlace Then
            e.Handled = True
        End If
    End Sub



    Public Sub FormulaSet(ByVal Rpt As Object, ByVal StrReportTitle As String, Optional ByVal FGrid As DataGridView = Nothing, _
Optional ByVal GFieldName As Byte = 0, Optional ByVal GFilter As Byte = 1, Optional ByVal GDisplayOnReport As Byte = 6)
        Dim I As Int16, J As Int16
        Dim StrField As String = "", StrFilter As String = "", StrValue As String = ""
        Dim StrbField As StringBuilder, IntMaxLength As Integer = 0

        For I = 0 To Rpt.DataDefinition.FormulaFields.Count - 1
            Select Case UCase(Rpt.DataDefinition.FormulaFields.Item(I).Name)
                Case "COMPANYNAME"
                    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & Agl.PubCompName & "'"
                Case "COMPANYADDRESS"
                    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & Agl.PubCompAdd1 & ", " & Agl.PubCompAdd2 & "'"
                Case "COMPANYCITY"
                    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & Agl.PubCompCity & " - " & Agl.PubCompPinCode & " '"
                Case "COUNTRY"
                    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & Agl.PubCompCountry & "'"
                Case "TITLE"
                    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & StrReportTitle & "'"
                Case "CST"
                    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & Agl.PubCompCST & "'"
                    'Case "CSTDATE"
                    '    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & Agl.PubCompCSTDate & "'"
                Case "FAX"
                    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & Agl.PubCompFax & "'"
                Case "PHONENO"
                    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & Agl.PubCompPhone & "'"
                Case "TINNO"
                    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & Agl.PubCompTIN & "'"
                    'Case "TINDATE"
                    '    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & Agl.PubCompTINDate & "'"
                Case "COMPANYYEAR"
                    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & Agl.PubCompYear & "'"
                Case "COMPANYSTARTDATE"
                    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & Agl.PubStartDate & "'"
                Case "COMPANYENDDATE"
                    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & Agl.PubEndDate & "'"
                    'Case "ECCNO"
                    '    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & Agl.PubCompEccNo & "'"
                    'Case "EXCOLLECTROTE"
                    '    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & Agl.PubCompExCollectrote & "'"
                    'Case "EXDIVISION"
                    '    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & Agl.PubCompExDivision & "'"
                    'Case "EXRANGE"
                    '    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & Agl.PubCompExRange & "'"
                    'Case "EXREGNO"
                    '    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & Agl.PubCompExRegNo & "'"
                    'Case "PAN"
                    '    Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & Agl.PubCompPAN & "'"
                Case UCase("FrmRptFormulaField")
                    Try
                        If Not FGrid Is Nothing Then
                            For J = 0 To FGrid.Rows.Count - 1
                                If FGrid(GDisplayOnReport, J).Value = "þ" Then
                                    If IntMaxLength < Len(FGrid(GFieldName, J).Value) Then
                                        IntMaxLength = Len(FGrid(GFieldName, J).Value)
                                    End If
                                End If
                            Next

                            For J = 0 To FGrid.Rows.Count - 1
                                If FGrid(GDisplayOnReport, J).Value = "þ" Then
                                    If StrValue <> "" Then
                                        StrValue = StrValue & "|"
                                    End If
                                    StrField = FGrid(GFieldName, J).Value
                                    StrbField = New StringBuilder(StrField, IntMaxLength)
                                    StrbField.Append(" ", IntMaxLength - Len(StrField))

                                    StrFilter = FGrid(GFilter, J).Value
                                    StrValue = StrValue & StrbField.ToString & " : " & StrFilter
                                End If
                            Next
                            Rpt.DataDefinition.FormulaFields.Item(I).Text = "'" & StrValue & "| '"
                        End If
                    Catch ex As Exception
                    End Try
            End Select
        Next
    End Sub



    Public Sub FShowReport(ByVal RpdReg As CrystalDecisions.CrystalReports.Engine.ReportDocument, _
    ByVal FrmMDI As Form, ByVal StrReportCaption As String, Optional ByVal BlnDirectPrint As Boolean = False, _
    Optional ByVal StrPaperSizeName As String = "", Optional ByVal StrLandScape As String = "")

        Dim PDPrint As System.Drawing.Printing.PrintDocument
        Dim PRDGMain As PrintDialog = Nothing
        Dim I As Integer
        Dim IntRawKind As Integer
        Dim NRepView As RepView

        If Trim(StrPaperSizeName) <> "" Then
            PDPrint = New System.Drawing.Printing.PrintDocument()
            For I = 0 To PDPrint.PrinterSettings.PaperSizes.Count - 1
                If UCase(Trim(PDPrint.PrinterSettings.PaperSizes(I).PaperName)) = UCase(Trim(StrPaperSizeName)) Then
                    IntRawKind = CInt(PDPrint.PrinterSettings.PaperSizes(I).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(PDPrint.PrinterSettings.PaperSizes(I)))
                    RpdReg.PrintOptions.PaperSize = IntRawKind
                    RpdReg.PrintOptions.PaperOrientation = IIf(Trim(UCase(StrLandScape)) = "Y", CrystalDecisions.Shared.PaperOrientation.Landscape, CrystalDecisions.Shared.PaperOrientation.Portrait)

                    If Not BlnDirectPrint Then
                        PRDGMain = New PrintDialog
                        PRDGMain.PrinterSettings.PrinterName = PDPrint.PrinterSettings.PrinterName
                        PRDGMain.PrinterSettings.DefaultPageSettings.PaperSize = PDPrint.PrinterSettings.PaperSizes(I)
                        PRDGMain.PrinterSettings.DefaultPageSettings.Landscape = IIf(Trim(UCase(StrLandScape)) = "Y", True, False)
                    End If
                    Exit For
                End If
            Next
        End If

        If BlnDirectPrint Then
            RpdReg.PrintToPrinter(1, True, 1, 1)
        Else
            If PRDGMain Is Nothing Then PRDGMain = New PrintDialog
            NRepView = New RepView(PRDGMain)
            NRepView.RepObj = RpdReg
            NRepView.MdiParent = FrmMDI
            NRepView.Text = StrReportCaption
            NRepView.Show()
        End If
    End Sub

End Module