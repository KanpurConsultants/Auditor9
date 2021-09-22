
Module ClsLib
    Public Function RetDate(ByRef Txt As String) As String
        On Error GoTo err1
        If Txt = "" Then RetDate = "" : Exit Function
        If Txt.Length >= 11 Then
            If Txt.IndexOf("/") <> -1 Then
                If Txt.LastIndexOf("/") <> -1 Then
                    If Txt.IndexOf("/") <> Txt.LastIndexOf("/") Then
                        RetDate = Format(CDate(Txt), "dd/MMM/yyyy") : Exit Function
                    End If
                End If
            End If
        End If

        Dim mDay As Long, mMonth As String, mYear As String, Txt1 As String, Test As Long
        mDay = 0 : mMonth = "" : mYear = 0
        Txt1 = Trim(Txt)
        '''' FOR DAY
        Test = InStr(1, Txt1, "/")
        If Test = 0 Then Test = InStr(1, Txt1, "-")
        If Test = 0 Then Test = InStr(1, Txt1, ".")
        If Test <> 0 Then
            If IsNumeric(Mid(Txt1, 1, Test - 1)) Then
                mDay = Val(Mid(Txt1, 1, Test - 1))
            Else
                mMonth = Mid(Txt1, 1, Test - 1)
            End If
        End If
        If Test = 0 Then
            If IsNumeric(Txt1) Then
                mDay = Val(Txt1)
            Else
                mMonth = Txt1
            End If
            GoTo EXITFLAG
        End If
        ''''' FOR MONTH
        If mMonth = "" Then
            Txt1 = Mid(Txt1, Test + 1)
            Test = InStr(1, Txt1, "/")
            If Test = 0 Then Test = InStr(1, Txt1, "-")
            If Test = 0 Then Test = InStr(1, Txt1, ".")
            If Test <> 0 Then mMonth = Mid(Txt1, 1, Test - 1)
            If Test = 0 Then
                mMonth = Txt1
                GoTo EXITFLAG
            End If
        End If
        ''''FOR YEAR
        mYear = Format(Val(Mid(Txt1, Test + 1)), "00")
EXITFLAG:
        If Val(mYear) = 0 Then mYear = Date.Today.Year
        If mYear > 1999 Then mYear = Microsoft.VisualBasic.Right(Str(mYear), 2)
        mYear = Val(Mid(CStr(Date.Today.Year), 1, 4 - Len(Trim(CStr(mYear)))) + Trim(CStr(mYear)))
        If mDay < 0 Then mDay = 0
        mMonth = Mid(mMonth, 1, 3)
        Select Case Trim(UCase(mMonth))
            Case "1", "01", "J", "JA", "JAN"
                mMonth = "Jan"
            Case "2", "02", "F", "FE", "FEB"
                mMonth = "Feb"
            Case "3", "03", "M", "MA", "MAR"
                mMonth = "Mar"
            Case "4", "04", "A", "AP", "APR"
                mMonth = "Apr"
            Case "5", "05", "MAY"
                mMonth = "May"
            Case "6", "06", "JU", "JUN"
                mMonth = "Jun"
            Case "7", "07", "JUL"
                mMonth = "Jul"
            Case "8", "08", "AU", "AUG"
                mMonth = "Aug"
            Case "9", "09", "S", "SE", "SEP"
                mMonth = "Sep"
            Case "10", "O", "OC", "OCT"
                mMonth = "Oct"
            Case "11", "N", "NO", "NOV"
                mMonth = "Nov"
            Case "12", "D", "DE", "DEC"
                mMonth = "Dec"
            Case Else
                mMonth = Format(Date.Today, "MMM")
        End Select
        Select Case Trim(UCase(mMonth))
            Case "1", "01", "J", "JA", "JAN"
                If mDay > 31 Then mDay = 0
            Case "2", "02", "F", "FE", "FEB"
                If mDay > IIf(mYear Mod 4 = 0, 29, 28) Then mDay = 0
            Case "3", "03", "M", "MA", "MAR"
                If mDay > 31 Then mDay = 0
            Case "4", "04", "A", "AP", "APR"
                If mDay > 30 Then mDay = 0
            Case "5", "05", "MAY"
                If mDay > 31 Then mDay = 0
            Case "6", "06", "JU", "JUN"
                If mDay > 30 Then mDay = 0
            Case "7", "07", "JUL"
                If mDay > 31 Then mDay = 0
            Case "8", "08", "AU", "AUG"
                If mDay > 31 Then mDay = 0
            Case "9", "09", "S", "SE", "SEP"
                If mDay > 30 Then mDay = 0
            Case "10", "O", "OC", "OCT"
                If mDay > 31 Then mDay = 0
            Case "11", "N", "NO", "NOV"
                If mDay > 30 Then mDay = 0
            Case "12", "D", "DE", "DEC"
                If mDay > 31 Then mDay = 0
            Case Else
                mDay = 0
        End Select
        If mDay = 0 Then mDay = Today.Day
        RetDate = Format(mDay, "00") + "/" + Trim(mMonth) + "/" + Trim(Str(mYear))
        Exit Function
err1:
        ' For Overflow Pd.Check
        If Err.Number = 6 Then Resume Next
    End Function

    Public Function ComputeNum(ByVal Expr As String) As Double
        Dim mExpr As String
        Dim mResult As Double
        If Left(Expr, 1) <> "=" Then Exit Function
        mExpr = Replace(Expr, "=", "")
        mResult = New DataTable().Compute(mExpr, "")
        ComputeNum = mResult
    End Function

    Public Sub NumPress(ByRef TEXT As System.Windows.Forms.TextBox, ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal LeftPlace As Integer, ByVal RightPlace As Integer, ByVal pAllowNegative As Boolean)
        On Error Resume Next
        Dim myString As String

        If TEXT.Text = "" Then
            If e.KeyChar = "=" Then
                Exit Sub
            End If
        End If

        If Left(TEXT.Text, 1) = "=" Then Exit Sub

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


    Public Function GetFileName(Optional ByVal FilePath As String = "") As String
        Dim SaveFileDialogBox As SaveFileDialog
        Dim sFilePath As String = ""
        Try
            SaveFileDialogBox = New SaveFileDialog

            SaveFileDialogBox.Title = "File Name"
            SaveFileDialogBox.Filter = "Microsoft Excel Worksheet(*.xls)|*.xls|XLSX Files(*.xlsx)|*.xlsx"

            If FilePath.Trim = "" Then FilePath = My.Application.Info.DirectoryPath
            SaveFileDialogBox.InitialDirectory = FilePath
            SaveFileDialogBox.DefaultExt = "*.xls"
            SaveFileDialogBox.FilterIndex = 1


            SaveFileDialogBox.FileName = ""

            If SaveFileDialogBox.ShowDialog = Windows.Forms.DialogResult.Cancel Then Exit Function

            sFilePath = SaveFileDialogBox.FileName
        Catch ex As Exception
        Finally
            GetFileName = sFilePath
        End Try
    End Function
    
End Module
