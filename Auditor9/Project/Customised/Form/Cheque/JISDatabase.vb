Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports System.Windows.Forms
Imports System.IO

Public Class JISDatabase

    Dim TBs As New Hashtable
    Dim VWs As New Hashtable
    Dim Connection As Object
    Dim adp As Object

    Sub New()
        Dim ConnectionString, ConnectionType As String
        Dim ds As New DataSet()
        ds.ReadXml(Application.StartupPath + "\\App.Xml")
        ConnectionString = ds.Tables(0).Rows(0).Item("DBConStr").ToString()
        ConnectionType = ds.Tables(0).Rows(0).Item("DBType").ToString().ToUpper
        Connect(ConnectionString, ConnectionType)
    End Sub

    Sub New(ByVal ConnectionString As String, ByVal ConnectionType As String)
        Connect(ConnectionString, ConnectionType)
    End Sub

    Private Sub Connect(ByVal ConnectionString As String, ByVal ConnectionType As String)
        Try
            ConStr = ConnectionString
            ConType = ConnectionType

            If ConType = "SQL" Then
                Connection = New SqlConnection(ConStr)
            ElseIf ConType = "OLEDB" Then
                Connection = New OleDbConnection(ConStr)
            ElseIf ConType = "ODBC" Then
                Connection = New OdbcConnection(ConStr)
            End If


            Dim dv As New DataView
            If Connection.State <> ConnectionState.Open Then Connection.Open()

            dv.Table = Connection.GetSchema("Tables")
            For i As Integer = 0 To dv.Count - 1
                If dv.Item(i)("Table_Type") = "TABLE" Or dv.Item(i)("Table_Type") = "BASE TABLE" Then
                    TBs.Add(dv.Item(i)("Table_Name"), New JISTable(dv.Item(i)("Table_Name"), Connection))
                ElseIf dv.Item(i)("Table_Type") = "VIEW" Then
                    VWs.Add(dv.Item(i)("Table_Name"), New JISTable(dv.Item(i)("Table_Name"), Connection))
                End If


            Next

            If Connection.State <> ConnectionState.Open Then Connection.Open()
            dv.Table = Connection.GetSchema("Columns")
            For i As Integer = 0 To dv.Count - 1
                If TBs.Contains(dv.Item(i)("Table_Name")) Then
                    TBs(dv.Item(i)("Table_Name")).FieldName.Add(dv.Item(i)("column_name"), New JISField(dv.Item(i)("column_name"), dv.Item(i)("ordinal_position"), dv.Item(i)("data_type")))
                End If

                If VWs.Contains(dv.Item(i)("Table_Name")) Then
                    VWs(dv.Item(i)("Table_Name")).FieldName.Add(dv.Item(i)("column_name"), New JISField(dv.Item(i)("column_name"), dv.Item(i)("ordinal_position"), dv.Item(i)("data_type")))
                End If
            Next

            For Each tb1 As Object In TBs
                ReDim tb1.value.ArrFieldNames(tb1.value.FieldName.count - 1)
                ReDim tb1.value.ArrFieldTypes(tb1.value.FieldName.count - 1)
                For Each f1 As Object In tb1.value.FieldName
                    tb1.value.ArrFieldNames(f1.value.FieldPosition - 1) = f1.value.FieldName
                    tb1.value.ArrFieldTypes(f1.value.FieldPosition - 1) = f1.value.FieldType
                Next
                tb1.value.FieldNames = Join(tb1.value.ArrFieldNames, ",")
            Next


            For Each tb1 As Object In VWs
                ReDim tb1.value.ArrFieldNames(tb1.value.FieldName.count - 1)
                ReDim tb1.value.ArrFieldTypes(tb1.value.FieldName.count - 1)
                For Each f1 As Object In tb1.value.FieldName
                    tb1.value.ArrFieldNames(f1.value.FieldPosition - 1) = f1.value.FieldName
                    tb1.value.ArrFieldTypes(f1.value.FieldPosition - 1) = f1.value.FieldType
                Next
                tb1.value.FieldNames = Join(tb1.value.ArrFieldNames, ",")
            Next


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Connection.close()

    End Sub

    Public Sub Disconnect()
        ExecuteQuery("Use Master")
        Connection.Dispose()

    End Sub


    Public Function Table(ByVal TableName As String) As JISTable
        Return TBs(TableName)
    End Function

    Public Function View(ByVal ViewName As String) As JISTable
        Return VWs(ViewName)
    End Function


    Public Function ExecuteQuery(ByVal Qry As String) As Object
        Dim RValue As Object = ""

        If ConType = "SQL" Then
            RValue = ExecuteQuerySQL(Qry)
        ElseIf ConType = "OLEDB" Then
            RValue = ExecuteQueryOLEDB(Qry)
        ElseIf ConType = "ODBC" Then
            RValue = ExecuteQueryODBC(Qry)
        End If

        Return RValue

    End Function


    Private Function ExecuteQueryOLEDB(ByVal Qry As String) As Object
        Try
            If Connection.State <> ConnectionState.Open Then Connection.Open()

            Dim cmdText As String = Qry
            Dim command As New OleDbCommand(cmdText, Connection)
            If (InStr(Qry.ToLower(), "select") = 1) Then
                adp = New OleDbDataAdapter(command)
                Dim ds As New DataSet()
                Dim dv As New DataView
                adp.Fill(ds, 0)

                dv = ds.Tables(0).DefaultView
                Connection.Close()
                Return dv
            Else
                Dim i As Integer = command.ExecuteNonQuery()
                Connection.Close()
                Return i
            End If
        Catch e As OleDbException
            Connection.Close()
            Return Nothing
        End Try
    End Function

    Private Function ExecuteQuerySQL(ByVal Qry As String) As Object
        Try
            If Connection.State <> ConnectionState.Open Then Connection.Open()

            Dim cmdText As String = Qry
            Dim command As New SqlCommand(cmdText, Connection)
            If (InStr(Qry.ToLower(), "select") = 1) Then
                adp = New SqlDataAdapter(command)
                Dim ds As New DataSet()
                Dim dv As New DataView
                adp.Fill(ds, 0)

                dv = ds.Tables(0).DefaultView
                Connection.Close()
                Return dv
            Else
                Dim i As Integer = command.ExecuteNonQuery()
                Connection.Close()
                Return i
            End If
        Catch e As SqlException
            Connection.Close()
            Return Nothing
        End Try
    End Function

    Private Function ExecuteQueryODBC(ByVal Qry As String) As Object
        Try
            If Connection.State <> ConnectionState.Open Then Connection.Open()

            Dim cmdText As String = Qry
            Dim command As New OdbcCommand(cmdText, Connection)
            If (InStr(Qry.ToLower(), "select") = 1) Then
                adp = New OdbcDataAdapter(command)
                Dim ds As New DataSet()
                Dim dv As New DataView
                adp.Fill(ds, 0)

                dv = ds.Tables(0).DefaultView
                Connection.Close()
                Return dv
            Else
                Dim i As Integer = command.ExecuteNonQuery()
                Connection.Close()
                Return i
            End If
        Catch e As OdbcException
            Connection.Close()
            Return Nothing
        End Try
    End Function


End Class

Public Class JISTable

    Dim Connection As Object
    Dim adp As Object

    Public TableName As String

    Public FieldName As New Hashtable
    Public ArrFieldNames() As Object
    Public ArrFieldTypes() As Object
    Public FieldNames As String

    Public Prefix As String
    Public KeySize As String
    Public StartFrom As String

    Dim CTRLS() As Control
    Public State As String


    Sub New(ByVal TName As String, ByVal Con As Object)

        ReDim ArrFieldNames(0)
        ReDim ArrFieldTypes(0)
        Connection = Con
        FieldNames = ""
        TableName = TName
        Prefix = ""
        KeySize = 4
        StartFrom = 1
        Try
            Dim dv As New DataView
            dv = ExecuteQuery("Select * from AutoGeneration where TableName='" + TName + "'")
            If Not dv Is Nothing Then
                If dv.Count Then
                    Prefix = dv.Item(0)("Prefix").ToString()
                    KeySize = dv.Item(0)("KeySize").ToString()
                    StartFrom = dv.Item(0)("StartFrom").ToString()
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Function ExecuteQuery(ByVal Qry As String) As Object
        Dim RValue As Object = ""

        If ConType = "SQL" Then
            RValue = ExecuteQuerySQL(Qry)
        ElseIf ConType = "OLEDB" Then
            RValue = ExecuteQueryOLEDB(Qry)
        ElseIf ConType = "ODBC" Then
            RValue = ExecuteQueryODBC(Qry)
        End If

        Return RValue

    End Function


    Private Function ExecuteQueryOLEDB(ByVal Qry As String) As Object
        Try
            If Connection.State <> ConnectionState.Open Then Connection.Open()

            Dim cmdText As String = Qry
            Dim command As New OleDbCommand(cmdText, Connection)
            If (InStr(Qry.ToLower(), "select") = 1) Then
                adp = New OleDbDataAdapter(command)
                Dim ds As New DataSet()
                Dim dv As New DataView
                adp.Fill(ds, 0)

                dv = ds.Tables(0).DefaultView
                Connection.Close()
                Return dv
            Else
                Dim i As Integer = command.ExecuteNonQuery()
                Connection.Close()
                Return i
            End If
        Catch e As OleDbException
            Connection.Close()
            Return Nothing
        End Try
    End Function

    Private Function ExecuteQuerySQL(ByVal Qry As String) As Object
        Try
            If Connection.State <> ConnectionState.Open Then Connection.Open()

            Dim cmdText As String = Qry
            Dim command As New SqlCommand(cmdText, Connection)
            If (InStr(Qry.ToLower(), "select") = 1) Then
                adp = New SqlDataAdapter(command)
                Dim ds As New DataSet()
                Dim dv As New DataView
                adp.Fill(ds, 0)

                dv = ds.Tables(0).DefaultView
                Connection.Close()
                Return dv
            Else
                Dim i As Integer = command.ExecuteNonQuery()
                Connection.Close()
                Return i
            End If
        Catch e As SqlException
            Connection.Close()
            Return Nothing
        End Try
    End Function

    Private Function ExecuteQueryODBC(ByVal Qry As String) As Object
        Try
            If Connection.State <> ConnectionState.Open Then Connection.Open()

            Dim cmdText As String = Qry
            Dim command As New OdbcCommand(cmdText, Connection)
            If (InStr(Qry.ToLower(), "select") = 1) Then
                adp = New OdbcDataAdapter(command)
                Dim ds As New DataSet()
                Dim dv As New DataView
                adp.Fill(ds, 0)

                dv = ds.Tables(0).DefaultView
                Connection.Close()
                Return dv
            Else
                Dim i As Integer = command.ExecuteNonQuery()
                Connection.Close()
                Return i
            End If
        Catch e As OdbcException
            Connection.Close()
            Return Nothing
        End Try
    End Function


    Sub SetControls(ByVal ParamArray ctls() As Control)
        ReDim CTRLS(ctls.Length() - 1)
        For i As Integer = 0 To ctls.Length - 1
            CTRLS(i) = ctls(i)
        Next
    End Sub

    Function Data(ByVal FieldName As String, Optional ByVal WQry As String = "") As String

        Dim RValue As String = ""
        Dim dv As New DataView
        Dim Qry As String

        Qry = "Select " + FieldName + " from " + TableName
        If WQry <> "" Then Qry = Qry + " where " + WQry

        dv = ExecuteQuery(Qry)

        If Not dv Is Nothing Then
            If dv.Count Then
                RValue = dv.Item(0)(0).ToString()
            End If
        End If

        Return RValue
    End Function

    Function Column(ByVal FieldName As String, Optional ByVal WQry As String = "") As ArrayList
        Dim RValue As New ArrayList
        Dim dv As New DataView
        Dim Qry As String

        Qry = "Select distinct " + FieldName + " from " + TableName
        If WQry <> "" Then
            WQry += " AND "
        End If
        WQry += FieldName + "<>''"

        If WQry <> "" Then Qry = Qry + " where " + WQry

        dv = ExecuteQuery(Qry)

        If Not dv Is Nothing Then
            For i As Integer = 0 To dv.Count - 1
                RValue.Add(dv.Item(i)(0).ToString())
            Next
        End If
        Return RValue
    End Function

    Function Row(ByVal FieldNames As String, Optional ByVal WQry As String = "") As ArrayList
        Dim RValue As New ArrayList
        Dim dv As New DataView
        Dim Qry As String

        Qry = "Select " + FieldNames + " from " + TableName
        If WQry <> "" Then Qry = Qry + " where " + WQry

        dv = ExecuteQuery(Qry)
        If Not dv Is Nothing Then
            If dv.Count > 0 Then
                For i As Integer = 0 To dv.ToTable(TableName).Columns.Count - 1
                    RValue.Add(dv.Item(0)(i))
                Next
            End If
        End If

        Return RValue
    End Function

    Function Grid(Optional ByVal FieldNames As String = "*", Optional ByVal WQry As String = "", Optional ByVal OrderBy As String = "", Optional ByVal GroupBy As String = "") As DataView
        Dim Qry As String
        Qry = "Select " + FieldNames + " from " + TableName
        If WQry <> "" Then Qry = Qry + " where " + WQry
        If GroupBy.Trim() <> "" Then Qry = Qry + " Group By " + GroupBy
        If OrderBy.Trim() <> "" Then Qry = Qry + " order by " + OrderBy

        Return ExecuteQuery(Qry)
    End Function

    'Function Save(ByVal FieldNames() As Object, ByVal FieldValues() As Object, Optional ByVal WQry As String = "") As String
    '    Try
    '        Dim i As Integer
    '        Dim RValue As String = ""
    '        Dim str1 As String = ""
    '        Dim str2 As String = ""

    '        For i = 0 To FieldValues.Length - 2
    '            str1 = str1 + "@" + FieldNames(i).ToString() + "',N'"
    '            str2 = str2 + FieldNames(i).ToString() + "=@" + FieldNames(i).ToString() + ","
    '        Next
    '        str1 = str1 + "@" + FieldNames(i).ToString()
    '        str2 = str2 + FieldNames(i).ToString() + "=N'" + FieldNames(i).ToString()

    '        If Connection.State <> ConnectionState.Open Then Connection.Open()

    '        Dim cmdText As String = "update " + TableName + " set " & str2 & "' where "
    '        If WQry = "" Then
    '            cmdText = cmdText + FieldNames(0).ToString() + " =N'" & FieldValues(0).ToString() & "'"
    '        Else
    '            cmdText = cmdText + WQry
    '        End If


    '        Dim command As New SqlCommand(cmdText, Connection)
    '        For i = 0 To FieldValues.Length - 1
    '            command.Parameters.AddWithValue("@" + FieldNames(i).ToString(), FieldValues(i))
    '        Next
    '        i = command.ExecuteNonQuery()


    '        If (i <> 0) Then RValue = "Updated"
    '        If (i = 0) And WQry = "" Then
    '            cmdText = "insert  into " + TableName + " values ( N'" & str1 & "')"
    '            command = New SqlCommand(cmdText, Connection)
    '            For i = 0 To FieldValues.Length - 1
    '                command.Parameters.AddWithValue("@" + FieldNames(i).ToString(), FieldValues(i).ToString())
    '            Next
    '            i = command.ExecuteNonQuery()
    '            If i <> 0 Then RValue = "Inserted"
    '        End If
    '        Connection.Close()

    '        Return RValue
    '    Catch e As Exception
    '        Console.WriteLine(e.Message)
    '        Console.WriteLine(e.StackTrace)
    '        Return e.Message
    '    End Try

    'End Function


    Function Save(ByVal FieldNames() As Object, ByVal FieldValues() As Object, Optional ByVal WQry As String = "") As String
        Try
            Dim i As Integer
            Dim RValue As String = ""
            Dim str1 As String = ""
            Dim str2 As String = ""

            For i = 0 To FieldValues.Length - 2
                str1 = str1 + FieldValues(i).ToString() + "',N'"
                str2 = str2 + FieldNames(i).ToString() + "=N'" + FieldValues(i).ToString() + "',"
            Next
            str1 = str1 + FieldValues(i).ToString()
            str2 = str2 + FieldNames(i).ToString() + "=N'" + FieldValues(i).ToString()

            Connection.Open()
            Dim cmdText As String = "update " + TableName + " set " & str2 & "' where "
            If WQry = "" Then
                cmdText = cmdText + FieldNames(0).ToString() + " =N'" & FieldValues(0).ToString() & "'"
            Else
                cmdText = cmdText + WQry
            End If

            i = ExecuteQuery(cmdText)
            If (i <> 0) Then RValue = "Updated"
            If (i = 0) And WQry = "" Then
                cmdText = "insert  into " + TableName + " values ( N'" & str1 & "')"
                i = ExecuteQuery(cmdText)
                If i <> 0 Then RValue = "Inserted"
            End If
            Return RValue
        Catch e As Exception
            Console.WriteLine(e.Message)
            Console.WriteLine(e.StackTrace)
            Return e.Message
        End Try

    End Function

    Function Delete(Optional ByVal WQry As String = "") As String
        Try
            Dim cmdText As String = "Delete from " + TableName
            If WQry.Trim() <> "" Then cmdText = cmdText + " where " + WQry
            Dim i As Integer
            i = ExecuteQuery(cmdText)
            If i = 0 Then
                Return "Not Deleted"
            Else
                Return "Deleted"
            End If
        Catch e As Exception
            Console.WriteLine(e.Message)
            Console.WriteLine(e.StackTrace)
            Return e.Message.ToString()
        End Try
    End Function

    Function NewId() As String
        Dim NId As String
        NId = Data("Max(" + ArrFieldNames(0) + ")")
        If NId = "" Then NId = "0"
        NId = NId + 1
        If NId < StartFrom Then NId = StartFrom
        Return NId
    End Function

    'Function NewCode() As String
    '    Dim id As String = NewId()
    '    Return Prefix + Integer.Parse(id).ToString(StrDup(Integer.Parse(KeySize), "0"))
    'End Function

    Function ExistData(ByVal FieldName As String, ByVal FieldValue As String) As Boolean
        Dim str As String = Data(FieldName, FieldName + "=N'" + FieldValue + "'")
        If str = "" Then
            Return False
        Else
            Return True
        End If
    End Function

    Function ExistCode(ByVal Code) As Boolean
        Return ExistData(ArrFieldNames(1), Code)
    End Function

    Function ExistUserCode(ByVal UserCode) As Boolean
        Return ExistData(ArrFieldNames(2), UserCode)
    End Function

    Function IdByCode(ByVal Code As String) As String
        Return Data(ArrFieldNames(0), ArrFieldNames(1) + "='" + Code + "'")
    End Function

    Function IdByUserCode(ByVal UserCode As String) As String
        Return Data(ArrFieldNames(0), ArrFieldNames(2) + "=N'" + UserCode + "'")
    End Function

    Function CodeById(ByVal Id As String) As String
        Return Data(ArrFieldNames(1), ArrFieldNames(0) + "='" + Id + "'")
    End Function

    Function CodeByUserCode(ByVal UserCode As String) As String
        Return Data(ArrFieldNames(1), ArrFieldNames(2) + "=N'" + UserCode + "'")
    End Function

    Function UserCodeById(ByVal Id As String) As String
        Return Data(ArrFieldNames(2), ArrFieldNames(0) + "='" + Id + "'")
    End Function

    Function UserCodeByCode(ByVal Code As String) As String
        Return Data(ArrFieldNames(2), ArrFieldNames(1) + "='" + Code + "'")
    End Function

    Function Ids() As ArrayList
        Return Column(ArrFieldNames(0))
    End Function

    Function Codes() As ArrayList
        Return Column(ArrFieldNames(1))
    End Function

    Function UserCodes() As ArrayList
        Return Column(ArrFieldNames(2))
    End Function

    Function GetRecordById(ByVal Id As String) As ArrayList
        Return Row("*", ArrFieldNames(0) + "='" + Id + "'")
    End Function

    Function GetRecordByCode(ByVal Code As String) As ArrayList
        Return Row("*", ArrFieldNames(1) + "='" + Code + "'")
    End Function

    Function GetRecordByUserCode(ByVal UserCode As String) As ArrayList
        Return Row("*", ArrFieldNames(2) + "=N'" + UserCode + "'")
    End Function

    Public Function NewRecord() As String
        State = "New"
        CTRLS(0).Text = NewId()
        ' CTRLS(1).Text = NewCode()
        CTRLS(1).Focus()
        For i As Integer = 1 To CTRLS.Length - 1
            If TypeOf CTRLS(i) Is TextBox Then
                CType(CTRLS(i), TextBox).Text = ""
            ElseIf TypeOf CTRLS(i) Is ComboBox Then
                CType(CTRLS(i), ComboBox).SelectedIndex = -1
                CType(CTRLS(i), ComboBox).Text = ""
            ElseIf TypeOf CTRLS(i) Is DateTimePicker Then
                CType(CTRLS(i), DateTimePicker).Value = Now
            ElseIf TypeOf CTRLS(i) Is CheckBox Then
                CType(CTRLS(i), CheckBox).Checked = False
            ElseIf TypeOf CTRLS(i) Is PictureBox Then
                CType(CTRLS(i), PictureBox).Image = Nothing
            End If
        Next
        Return ""
    End Function

    Sub ViewRecord(ByVal Details As ArrayList)
        State = "View"
        For i As Integer = 0 To CTRLS.Length - 1
            If TypeOf CTRLS(i) Is TextBox Then
                CType(CTRLS(i), TextBox).Text = Details(i).ToString()
            ElseIf TypeOf CTRLS(i) Is ComboBox Then
                Dim cmb As ComboBox = CType(CTRLS(i), ComboBox)
                If cmb.AccessibleDescription = "" Then
                    cmb.Text = Details(i).ToString()
                Else
                    cmb.Text = JISDb.Table(cmb.AccessibleDescription).Data(cmb.DisplayMember, String.Format("{0} = '{1}'", cmb.ValueMember, Details(i).ToString()))
                End If
            ElseIf TypeOf CTRLS(i) Is DateTimePicker Then
                If Details(i).ToString() <> "" Then
                    CType(CTRLS(i), DateTimePicker).Value = Details(i).ToString()
                End If
            ElseIf TypeOf CTRLS(i) Is CheckBox Then
                CType(CTRLS(i), CheckBox).Checked = Details(i).ToString()
            ElseIf TypeOf CTRLS(i) Is PictureBox Then
                CType(CTRLS(i), PictureBox).Tag = Details(i)

                Dim img_buffer() As Byte
                img_buffer = CType(Details(i), Byte())
                Dim img_stream As New MemoryStream(img_buffer, True)
                img_stream.Write(img_buffer, 0, img_buffer.Length)
                CType(CTRLS(i), PictureBox).Image = New Bitmap(img_stream)
                img_stream.Close()

            End If
        Next
    End Sub

    Public Function ViewRecordById(ByVal Id As String) As String
        Dim Details As New ArrayList()
        Details = GetRecordById(Id)
        If Details.Count = 0 Then Return (Id + " is Not Found")
        ViewRecord(Details)
        Return ""
    End Function

    Public Function ViewRecordByCode(ByVal Code As String) As String
        Dim Details As New ArrayList()
        Details = GetRecordByCode(Code)
        If Details.Count = 0 Then Return (Code + " is Not Found")
        ViewRecord(Details)
        Return ""
    End Function

    Public Function ViewRecordByUserCode(ByVal UserCode As String) As String
        Dim Details As New ArrayList()
        Details = GetRecordByUserCode(UserCode)
        If Details.Count = 0 Then Return (UserCode + " is Not Found")
        ViewRecord(Details)
        Return ""
    End Function

    Public Function SaveRecord() As String
        If State = "New" And CTRLS.Length > 2 Then
            CTRLS(0).Text = NewId()
            'CTRLS(1).Text = NewCode()
        End If

        Dim FValues(CTRLS.Length - 1) As Object
        For i As Integer = 0 To CTRLS.Length - 1
            If TypeOf CTRLS(i) Is TextBox Then
                FValues(i) = CType(CTRLS(i), TextBox).Text
                If FValues(i) = "" Then
                    If ArrFieldTypes(i).ToString().ToLower() = "numeric" Or ArrFieldTypes(i).ToString().ToLower() = "float" Then
                        FValues(i) = "0"
                    End If
                End If
            ElseIf TypeOf CTRLS(i) Is ComboBox Then
                Dim cmb As ComboBox = CType(CTRLS(i), ComboBox)
                If cmb.AccessibleDescription = "" Then
                    FValues(i) = cmb.Text
                Else
                    FValues(i) = JISDb.Table(cmb.AccessibleDescription).Data(cmb.ValueMember, String.Format("{0} = '{1}'", cmb.DisplayMember, cmb.Text))
                End If

            ElseIf TypeOf CTRLS(i) Is DateTimePicker Then
                FValues(i) = DateToString(CType(CTRLS(i), DateTimePicker).Value)
            ElseIf TypeOf CTRLS(i) Is CheckBox Then
                FValues(i) = CInt(CType(CTRLS(i), CheckBox).Checked).ToString()
            ElseIf TypeOf CTRLS(i) Is PictureBox Then
                FValues(i) = CType(CTRLS(i), PictureBox).Tag
            End If
        Next

        Return Save(ArrFieldNames, FValues)
    End Function

    Public Function SaveRecord(ByVal ParamArray FValues() As Object) As String
        Return Save(ArrFieldNames, FValues)
    End Function

    Public Function DeleteRecord() As String
        If State = "New" Then Return ""
        Return DeleteRecordById(CTRLS(0).Text)
    End Function

    Public Function DeleteRecordById(ByVal Id As String) As String
        Return Delete(ArrFieldNames(0) + "='" + Id + "'")
    End Function

    Public Function DeleteRecordByCode(ByVal Code As String) As String
        Return Delete(ArrFieldNames(1) + "='" + Code + "'")
    End Function

    Public Function DeleteRecordByUserCode(ByVal UserCode As String) As String
        Return Delete(ArrFieldNames(2) + "=N'" + UserCode + "'")
    End Function

End Class

Public Class JISField
    Public FieldName As String
    Public FieldPosition As String
    Public FieldType As String

    Sub New(ByVal FName As String, ByVal FPos As String, Optional ByVal FType As String = "")
        FieldName = FName
        FieldPosition = FPos
        FieldType = FType
    End Sub

End Class
