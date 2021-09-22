Imports System.IO
Imports System.Linq
Imports System.Text

Public Class FrmAttachmentViewer
    Inherits System.Windows.Forms.Form
    Implements IMessageFilter
    Dim CtrlWidth As Integer
    Dim CtrlHeight As Integer
    Dim PicWidth As Integer
    Dim PicHeight As Integer
    Dim XLocation As Integer
    Dim YLocation As Integer
    Dim PictureBoxCnt As Integer

    Private mSearchCode As String = ""
    Private mTableName As String = ""
    Dim mQry As String = ""
    Dim AgL As AgLibrary.ClsMain

    Public Sub New(ByVal AgLibVar As AgLibrary.ClsMain)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        AgL = AgLibVar
    End Sub
    Public Property SearchCode() As String
        Get
            Return mSearchCode
        End Get
        Set(ByVal value As String)
            mSearchCode = value
        End Set
    End Property
    Public Property TableName() As String
        Get
            Return mTableName
        End Get
        Set(ByVal value As String)
            mTableName = value
        End Set
    End Property
    Private Sub DispText()
        Dim bSourcePath As String = ""
        mQry = " Select ScannerPath From ComputerSetting Where ComputerName = '" & My.Computer.Name & "'"
        bSourcePath = AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar()
        If bSourcePath = "" Then
            mQry = " Select ScannerPath From ComputerSetting "
            bSourcePath = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar())
        End If
        If bSourcePath <> "" Then
            LblPath.Text = bSourcePath
        Else
            BtnPickFrom.Visible = False
            LblPath.Visible = False
        End If
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        XLocation = 25
        YLocation = 50
        PicWidth = 117
        PicHeight = 109

        CtrlHeight = Me.Height
        CtrlWidth = Me.Width
        MovRec()
        Me.AutoScroll = True
        DispText()
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNewAttachment.Click
        Dim OPD As New OpenFileDialog
        OPD.Multiselect = True
        If OPD.ShowDialog = DialogResult.OK Then
            FGetFileNames(OPD.FileNames)
        End If
    End Sub
    Private Sub FGetFileNames(FileNames As String())
      Try
		For I As Integer = 0 To FileNames.Length - 1
            Dim FileNameWithoutFullPath As String = New FileInfo(FileNames(I)).Name
            Dim FilePath As String = New FileInfo(FileNames(I)).DirectoryName

            Dim FileExtension As String = New FileInfo(FileNames(I)).Extension
            If FileExtension <> ".pdf" And FileExtension <> ".doc" And
                FileExtension <> ".docx" And FileExtension <> ".xls" And
                FileExtension <> ".xlsx" And FileExtension <> ".jpg" And
                FileExtension <> ".jpeg" And FileExtension <> ".bmp" And
                FileExtension <> ".png" And FileExtension <> ".gif" Then
                MsgBox(FileNameWithoutFullPath + " is not allowed for attachment.File Extension is not suppored...!", MsgBoxStyle.Information)
                Continue For
            End If

            Dim NewSavedFileName As String = FSave(FileNames(I), FilePath)
            DrawPictureBox(NewSavedFileName + FileNameWithoutFullPath, FileNameWithoutFullPath)
        Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub




    Private Function FSave(FileNameWithFullPath As String, FilePath As String) As String
        Try
	        Dim SourcePath As String = FilePath
	        Dim DestinationPath As String = PubAttachmentPath + mSearchCode + "\"
	        If Not Directory.Exists(DestinationPath) Then
	            Directory.CreateDirectory(DestinationPath)
	        End If
	        Dim file = New FileInfo(FileNameWithFullPath)
	
	        If System.IO.File.Exists(DestinationPath + file.Name) Then
	            Err.Raise(1, "", "File is already attached.")
	        End If
	        file.CopyTo(Path.Combine(DestinationPath, file.Name), True)
	        Return DestinationPath
        Catch ex As Exception
            MsgBox(ex.Message)
            Return ""
        End Try
    End Function
    Private Sub DrawPictureBox(ByVal _filename As String, ByVal _displayname As String)
        Try
	        Dim Pic1 As New PictureBox
	        Pic1.Location = New System.Drawing.Point(XLocation, YLocation)
	        XLocation = XLocation + PicWidth + 20
	        If XLocation + PicWidth >= CtrlWidth Then
	            XLocation = 25
	            YLocation = YLocation + PicHeight + 30
	        End If
	        Pic1.Name = "PictureBox" & PictureBoxCnt
	        PictureBoxCnt += 1
	        Pic1.Size = New System.Drawing.Size(PicWidth, PicHeight)
	        Pic1.TabIndex = 0
	        Pic1.TabStop = False
	        Pic1.BorderStyle = BorderStyle.Fixed3D
	        Me.ToolTip1.SetToolTip(Pic1, _displayname)
	        AddHandler Pic1.MouseEnter, AddressOf Pic1_MouseEnter
	        AddHandler Pic1.MouseLeave, AddressOf Pic1_MouseLeave
	        AddHandler Pic1.DoubleClick, AddressOf Pic1_DoubleClick
	        Me.Controls.Add(Pic1)
	
	        Dim FileExtension As String = New FileInfo(_filename).Extension
	        If FileExtension = ".pdf" Then
	            Pic1.Image = My.Resources.PdfIcon
	        ElseIf FileExtension = ".doc" Or FileExtension = ".docx" Then
	            Pic1.Image = My.Resources.wordicon
	        ElseIf FileExtension = ".xls" Or FileExtension = ".xlsx" Then
	            Pic1.Image = My.Resources.ExcelIcon
	        Else
	            Pic1.Image = Image.FromFile(_filename)
	        End If
	
		    Pic1.Tag = _filename
	        Pic1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
	
	        Dim BtnRemove As New Label
	        BtnRemove.Name = "BtnRemove" & PictureBoxCnt
	        BtnRemove.Tag = Pic1
	        BtnRemove.BackgroundImage = My.Resources.Cancel
	        BtnRemove.Width = BtnRemove.BackgroundImage.Width
	        BtnRemove.Cursor = System.Windows.Forms.Cursors.Hand
	        BtnRemove.BackgroundImageLayout = ImageLayout.Center
	        BtnRemove.Location = New System.Drawing.Point(Pic1.Location.X + Pic1.Width - 10, Pic1.Location.Y - 10)
	        AddHandler BtnRemove.Click, AddressOf BtnRemove_Click
	        Me.Controls.Add(BtnRemove)
	        BtnRemove.BringToFront()
	
	        Dim LblImageCaption As New Label
	        LblImageCaption.Name = "LblImageCaption" & PictureBoxCnt
	        LblImageCaption.Tag = BtnRemove.Name
	        LblImageCaption.Text = _displayname
	        LblImageCaption.TextAlign = ContentAlignment.MiddleCenter
	        LblImageCaption.Location = New System.Drawing.Point(Pic1.Location.X, Pic1.Location.Y + Pic1.Height)
	        LblImageCaption.Font = New Font(New FontFamily("Verdana"), 9, FontStyle.Bold)
	        Me.Controls.Add(LblImageCaption)
	        LblImageCaption.BringToFront()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Pic1_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Pic As PictureBox
        Pic = sender
        Pic.BorderStyle = BorderStyle.FixedSingle
    End Sub
    Private Sub Pic1_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Pic As PictureBox
        Pic = sender
        Pic.BorderStyle = BorderStyle.Fixed3D
    End Sub
    Private Sub Pic1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Pic As PictureBox
        Pic = sender
        System.Diagnostics.Process.Start(Pic.Tag)
    End Sub
    Private Sub BtnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If MsgBox("Are you sure to remove this attachment ? ", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                Dim BtnReomve As Label = sender
                Dim Pic As PictureBox = sender.Tag
                Dim Img As Image = Pic.Image
                Pic.Image = Nothing
                Img.Dispose()

                For I As Integer = 0 To Me.Controls.Count - 1
                    If Me.Controls(I).GetType.ToString = GetType(Label).ToString Then
                        If Me.Controls(I).Tag IsNot Nothing Then
                            If Me.Controls(I).Name.Contains("LblImageCaption") Then
                                If Me.Controls(I).Tag = BtnReomve.Name Then
                                    Me.Controls.Remove(Me.Controls(I))
                                    Exit For
                                End If
                            End If
                        End If
                    End If
                Next
                Dim AttachmentPath As String = Pic.Tag
                Me.Controls.Remove(BtnReomve.Tag)
                Me.Controls.Remove(BtnReomve)
                IO.File.Delete(AttachmentPath)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmAttachmentViewer_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Private Sub MovRec()
        Try
            Dim AttachmentPath As String = PubAttachmentPath + mSearchCode + "\"
            If Directory.Exists(AttachmentPath) Then
                Dim di As New IO.DirectoryInfo(AttachmentPath)
                Dim diar1 As IO.FileInfo() = di.GetFiles().ToArray
                Dim dra As IO.FileInfo
                For Each dra In diar1
                    DrawPictureBox(dra.FullName, dra.Name)
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FrmAttachmentViewer_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        For I As Integer = 0 To Me.Controls.Count - 1
            If Me.Controls(I).GetType.ToString = GetType(PictureBox).ToString Then
                Dim Img As Image = CType(Me.Controls(I), PictureBox).Image
                If Img IsNot Nothing Then
                    CType(Me.Controls(I), PictureBox).Image = Nothing
                    Img.Dispose()
                End If
            End If
        Next
        Application.RemoveMessageFilter(Me)
    End Sub


    '----------------------------------------------------

    Public Function PreFilterMessage(ByRef m As System.Windows.Forms.Message) As Boolean Implements IMessageFilter.PreFilterMessage
        If m.Msg = WM_DROPFILES Then
            Dim nfiles As Integer = DragQueryFile(m.WParam, -1, Nothing, 0) '<- this code to handle multiple dropped files.. not really neccesary for this example
            Dim i As Integer
            For i = 0 To nfiles
                Dim sb As StringBuilder = New StringBuilder(256)
                DragQueryFile(m.WParam, i, sb, 256)
                HandleDroppedFiles(sb.ToString())
            Next
            DragFinish(m.WParam)
            Return True
        End If
        Return False
    End Function

    Public Sub HandleDroppedFiles(ByVal file As String)
        If Len(file) > 0 Then
            LoadPicture(file)
        End If
    End Sub

    Public Function LoadPicture(ByVal File As String) As Boolean
        If Len(File) > 0 Then
            Dim FileArray(0) As String
            FileArray(0) = File
            FGetFileNames(FileArray)
            Return True
        End If
        Return False
    End Function

    Private Declare Function DragAcceptFiles Lib "shell32.dll" (ByVal hwnd As IntPtr, ByVal accept As Boolean) As Long
    Private Declare Function DragQueryFile Lib "shell32.dll" (ByVal hdrop As IntPtr, ByVal ifile As Integer, ByVal fname As StringBuilder, ByVal fnsize As Integer) As Integer
    Private Declare Sub DragFinish Lib "Shell32.dll" (ByVal hdrop As IntPtr)
    Public Const WM_DROPFILES As Integer = 563

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Application.AddMessageFilter(Me)
        DragAcceptFiles(Me.Handle, True)
    End Sub
    'Private Sub BtnPickFrom_Click(sender As Object, e As EventArgs) Handles BtnPickFrom.Click
    '    If LblPath.Text = "Not Defined" Then MsgBox("Source path is not defined...!", MsgBoxStyle.Information) : Exit Sub
    '    Dim SourcePath As String = LblPath.Text
    '    Dim DestinationPath As String = PubAttachmentPath + mSearchCode

    '    Dim bDirectoryInfo As New DirectoryInfo(SourcePath)
    '    Dim mFileArr As FileInfo() = bDirectoryInfo.GetFiles()

    '    If mFileArr.Count = 0 Then MsgBox("No files found in selected path...!", MsgBoxStyle.Information) : Exit Sub

    '    Dim mFile As FileInfo
    '    For Each mFile In mFileArr
    '        My.Computer.FileSystem.MoveFile(SourcePath + "\" + mFile.Name, DestinationPath + "\" + mFile.Name)
    '        DrawPictureBox(DestinationPath + "\" + mFile.Name, mFile.Name)
    '    Next mFile
    'End Sub
    Private Sub BtnPickFrom_Click(sender As Object, e As EventArgs) Handles BtnPickFrom.Click
        If LblPath.Text = "Not Defined" Then MsgBox("Source path is not defined...!", MsgBoxStyle.Information) : Exit Sub
        Dim SourcePath As String = LblPath.Text
        Dim DestinationPath As String = PubAttachmentPath + mSearchCode

        CopyDirectory(SourcePath, DestinationPath)

        Dim bDirectoryInfo As New DirectoryInfo(DestinationPath)
        Dim mFileArr As FileInfo() = bDirectoryInfo.GetFiles()

        If mFileArr.Count = 0 Then MsgBox("No files found in selected path...!", MsgBoxStyle.Information) : Exit Sub

        Dim mFile As FileInfo
        For Each mFile In mFileArr
            DrawPictureBox(DestinationPath + "\" + mFile.Name, mFile.Name)
        Next mFile
    End Sub
    Public Sub CopyDirectory(ByVal sourcePath As String, ByVal destinationPath As String)
        Dim sourceDirectoryInfo As New System.IO.DirectoryInfo(sourcePath)

        ' If the destination folder don't exist then create it
        If Not System.IO.Directory.Exists(destinationPath) Then
            System.IO.Directory.CreateDirectory(destinationPath)
        End If

        Dim fileSystemInfo As System.IO.FileSystemInfo
        For Each fileSystemInfo In sourceDirectoryInfo.GetFileSystemInfos
            Dim destinationFileName As String =
                System.IO.Path.Combine(destinationPath, fileSystemInfo.Name)

            ' Now check whether its a file or a folder and take action accordingly
            Dim FAtt As FileAttributes = File.GetAttributes(fileSystemInfo.FullName)
            If ((FAtt And IO.FileAttributes.Hidden) <> IO.FileAttributes.Hidden) Then
                If TypeOf fileSystemInfo Is System.IO.FileInfo Then
                    If System.IO.File.Exists(destinationFileName) Then
                        Dim SourceFileNameWithFullPath_New As String = fileSystemInfo.FullName.Replace(fileSystemInfo.Extension, "") + "1" + fileSystemInfo.Extension
                        Dim SourceFileName_New As String = fileSystemInfo.Name.Replace(fileSystemInfo.Extension, "") + "1" + fileSystemInfo.Extension
                        Dim DestinationFileNameWithFullPath_New As String = System.IO.Path.Combine(destinationPath, SourceFileName_New)
                        My.Computer.FileSystem.RenameFile(fileSystemInfo.FullName, SourceFileName_New)
                        System.IO.File.Copy(SourceFileNameWithFullPath_New, DestinationFileNameWithFullPath_New, True)
                        System.IO.File.Delete(SourceFileNameWithFullPath_New)
                    Else
                        System.IO.File.Copy(fileSystemInfo.FullName, destinationFileName, True)
                        System.IO.File.Delete(fileSystemInfo.FullName)
                    End If
                Else
                    ' Recursively call the mothod to copy all the neste folders
                    CopyDirectory(fileSystemInfo.FullName, destinationPath)
                    System.IO.Directory.Delete(fileSystemInfo.FullName, False)
                End If
            End If
        Next
    End Sub
End Class
