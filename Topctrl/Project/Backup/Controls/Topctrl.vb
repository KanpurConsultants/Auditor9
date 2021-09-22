Public Class Topctrl
    Private Const indAdd As Byte = 0, indEdit As Byte = 1, indDel As Byte = 2, indCancel As Byte = 3
    Private Const indFirst As Byte = 5, indPre As Byte = 6, indNext As Byte = 7, indLast As Byte = 8
    Private Const indFind As Byte = 10, indPrint As Byte = 11
    Private indSave As Byte = 13, indDiscard As Byte = 14, indSite As Byte = 15, indRefresh As Byte = 16
    Private indExit As Byte = 18
    Private Const indNull As Byte = 99


    Public Event tbAdd()
    Public Event tbEdit()
    Public Event tbDel()
    Public Event tbCancel()
    Public Event tbFirst()
    Public Event tbPrev()
    Public Event tbNext()
    Public Event tbLast()
    Public Event tbFind()
    Public Event tbPrn()
    Public Event tbSave()
    Public Event tbDiscard()
    Public Event tbRef()
    Public Event tbSite()
    Public Event tbExit()
    Public Event PrvKeyCodeChange()
    Public Event EnabledChange()
    Private FrmParent As Object
    Private DTUP As DataTable
    Dim mAgSearchMethod As AgControls.AgLib.TxtSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive

    Public Property AgSearchMethod() As AgControls.AgLib.TxtSearchMethod
        Get
            AgSearchMethod = mAgSearchMethod
        End Get
        Set(ByVal value As AgControls.AgLib.TxtSearchMethod)
            mAgSearchMethod = value

        End Set
    End Property

    Public Enum NavDirect
        NavFirst = 1
        NavPrevious = 2
        NavNext = 3
        NavLast = 4
    End Enum
    Public Structure SetButtons
        Public RecCount As Byte
        Public NavDirection As NavDirect
    End Structure
    Public Sub BlankTextBoxes()
        Dim ObjTxt As Object
        Dim ObjGroup As Object

        For Each ObjTxt In FrmParent.Controls
            If TypeOf ObjTxt Is Label Then
                If UCase(ObjTxt.Tag) <> "FRAME" Then
                    ObjTxt.Tag = ""
                End If
            End If

            If TypeOf ObjTxt Is DateTimePicker Then
                ObjTxt.Tag = ""
            End If

            If TypeOf ObjTxt Is TextBox Then
                ObjTxt.Text = ""
                ObjTxt.Tag = ""
                ObjTxt.BackColor = Color.White
            End If

            If TypeOf ObjTxt Is GroupBox Then
                For Each ObjGroup In ObjTxt.Controls
                    If TypeOf ObjGroup Is TextBox Then
                        ObjGroup.Text = ""
                        ObjGroup.Tag = ""
                        ObjGroup.BackColor = Color.White
                    End If
                Next
            End If

            If TypeOf ObjTxt Is System.Windows.Forms.ComboBox Then
                ObjTxt.Text = ""
                ObjTxt.SelectedItem = ""
                ObjTxt.SelectedValue = ""
                ObjTxt.BackColor = Color.White
            End If

            If TypeOf ObjTxt Is GroupBox Then
                For Each ObjGroup In ObjTxt.Controls
                    If TypeOf ObjGroup Is ComboBox Then
                        ObjGroup.SelectedItem = ""
                        ObjGroup.SelectedValue = ""
                        ObjGroup.BackColor = Color.White
                    End If
                Next
            End If
        Next
    End Sub
    Public Sub BlankTextBoxes(ByVal CntObj As Object)
        Dim ObjTxt As Object
        Dim ObjGroup As Object
        Dim ObjTabPage As Object

        For Each ObjTxt In CntObj.Controls
            If TypeOf ObjTxt Is Label Then
                ObjTxt.Tag = ""
            End If

            If TypeOf ObjTxt Is DateTimePicker Then
                ObjTxt.Tag = ""
            End If

            If TypeOf ObjTxt Is TextBox Then
                ObjTxt.Text = ""
                ObjTxt.Tag = ""
                ObjTxt.BackColor = Color.White
            End If

            If TypeOf ObjTxt Is GroupBox Then
                For Each ObjGroup In ObjTxt.Controls
                    If TypeOf ObjGroup Is TextBox Then
                        ObjGroup.Text = ""
                        ObjGroup.Tag = ""
                        ObjGroup.BackColor = Color.White
                    End If
                Next
            End If

            If TypeOf ObjTxt Is System.Windows.Forms.ComboBox Then
                ObjTxt.Text = ""
                ObjTxt.SelectedItem = ""
                ObjTxt.SelectedValue = ""
                ObjTxt.BackColor = Color.White
            End If

            If TypeOf ObjTxt Is GroupBox Then
                For Each ObjGroup In ObjTxt.Controls
                    If TypeOf ObjGroup Is ComboBox Then
                        ObjGroup.SelectedItem = ""
                        ObjGroup.SelectedValue = ""
                        ObjGroup.BackColor = Color.White
                    End If
                Next
            End If

            If TypeOf ObjTxt Is TabControl Then
                For Each ObjTabPage In ObjTxt.Controls
                    BlankTextBoxes(ObjTabPage)
                Next
            End If
        Next
    End Sub

    Public Sub ChangeTextState(ByRef CntObj As Object, ByVal Enb As Boolean)
        Dim ObjTxt As Control
        Dim ObjGroup As Object
        Dim ObjTabPage As Object

        For Each ObjTxt In CntObj.Controls
            If TypeOf ObjTxt Is TextBox Then
                ObjTxt.Enabled = Enb
                ObjTxt.BackColor = Color.White
            End If


            If TypeOf ObjTxt Is AgControls.AgTextBox Then
                CType(ObjTxt, AgControls.AgTextBox).AgSearchMethod = Me.AgSearchMethod
            End If

            If TypeOf ObjTxt Is AgControls.AgDataGrid Then
                CType(ObjTxt, AgControls.AgDataGrid).AgSearchMethod = Me.AgSearchMethod
            End If

            If TypeOf ObjTxt Is GroupBox Then
                For Each ObjGroup In ObjTxt.Controls
                    If TypeOf ObjGroup Is TextBox Then
                        ObjGroup.Enabled = Enb
                        ObjGroup.BackColor = Color.White
                    End If
                Next
            End If

            If TypeOf ObjTxt Is System.Windows.Forms.ComboBox Then
                ObjTxt.Enabled = Enb
                ObjTxt.BackColor = Color.White
            End If

            If TypeOf ObjTxt Is System.Windows.Forms.DateTimePicker Then
                ObjTxt.Enabled = Enb
                ObjTxt.BackColor = Color.White
            End If

            If TypeOf ObjTxt Is GroupBox Then
                For Each ObjGroup In ObjTxt.Controls
                    If TypeOf ObjGroup Is ComboBox Then
                        ObjGroup.Enabled = Enb
                        ObjGroup.BackColor = Color.White
                    End If
                Next
            End If

            If TypeOf ObjTxt Is GroupBox Then
                If UCase(Trim(ObjTxt.Tag)) = "UP" Then
                    ObjTxt.Enabled = False
                    Dim DRUP() As DataRow
                    DRUP = DTUP.Select("UP='" & Trim(ObjTxt.Text) & "'")
                    If UBound(DRUP) >= 0 Then
                        ObjTxt.Enabled = Not Enb
                    End If
                End If
            End If

            If TypeOf ObjTxt Is TabControl Then
                For Each ObjTabPage In ObjTxt.Controls
                    ChangeTextState(ObjTabPage, Enb)
                Next
            End If

            If TypeOf ObjTxt Is AgControls.AgDataGrid Then
                Call ChangeAgGridState(ObjTxt, Enb)
            End If
        Next
    End Sub

    Public Sub ChangeAgGridState(ByRef AgGridObj As AgControls.AgDataGrid, ByVal Enb As Boolean)
        Dim i As Integer = 0

        For i = 0 To CType(AgGridObj, AgControls.AgDataGrid).Columns.Count - 1
            If TypeOf CType(AgGridObj, AgControls.AgDataGrid).Columns(i) Is AgControls.AgTextColumn Then
                With CType(CType(AgGridObj, AgControls.AgDataGrid).Columns(i), AgControls.AgTextColumn)
                    If Enb = True Then
                        .ReadOnly = .AgReadOnly
                    Else
                        .ReadOnly = True
                    End If
                End With

            ElseIf TypeOf CType(AgGridObj, AgControls.AgDataGrid).Columns(i) Is AgControls.AgCheckBoxColumn Then
                With CType(CType(AgGridObj, AgControls.AgDataGrid).Columns(i), AgControls.AgCheckBoxColumn)
                    If Enb = True Then
                        .ReadOnly = .AgReadOnly
                    Else
                        .ReadOnly = True
                    End If
                End With

            ElseIf TypeOf CType(AgGridObj, AgControls.AgDataGrid).Columns(i) Is AgControls.AgComboColumn Then
                With CType(CType(AgGridObj, AgControls.AgDataGrid).Columns(i), AgControls.AgComboColumn)
                    If Enb = True Then
                        .ReadOnly = .AgReadOnly
                    Else
                        .ReadOnly = True
                    End If
                End With

            ElseIf TypeOf CType(AgGridObj, AgControls.AgDataGrid).Columns(i) Is AgControls.AgImageColumn Then
                With CType(CType(AgGridObj, AgControls.AgDataGrid).Columns(i), AgControls.AgImageColumn)
                    If Enb = True Then
                        .ReadOnly = .AgReadOnly
                    Else
                        .ReadOnly = True
                    End If
                End With

            ElseIf TypeOf CType(AgGridObj, AgControls.AgDataGrid).Columns(i) Is AgControls.AgLinkColumn Then
                With CType(CType(AgGridObj, AgControls.AgDataGrid).Columns(i), AgControls.AgLinkColumn)
                    If Enb = True Then
                        .ReadOnly = .AgReadOnly
                    Else
                        .ReadOnly = True
                    End If
                End With

            ElseIf TypeOf CType(AgGridObj, AgControls.AgDataGrid).Columns(i) Is AgControls.AgButtonColumn Then
                With CType(CType(AgGridObj, AgControls.AgDataGrid).Columns(i), AgControls.AgButtonColumn)
                    If Enb = True Then
                        .ReadOnly = .AgReadOnly
                    Else
                        .ReadOnly = True
                    End If
                End With

            End If
        Next
    End Sub


    Public Property tAdd() As Boolean
        Get
            tAdd = ToolBar1.Buttons(indAdd).Enabled
        End Get
        Set(ByVal Value As Boolean)
            ToolBar1.Buttons(indAdd).Enabled = Value
        End Set
    End Property

    Public Property tEdit() As Boolean
        Get
            tEdit = ToolBar1.Buttons(indEdit).Enabled
        End Get
        Set(ByVal Value As Boolean)
            ToolBar1.Buttons(indEdit).Enabled = Value
        End Set
    End Property
    Public Property tDel() As Boolean
        Get
            tDel = ToolBar1.Buttons(indDel).Enabled
        End Get
        Set(ByVal Value As Boolean)
            ToolBar1.Buttons(indDel).Enabled = Value
        End Set
    End Property
    Public Property tCancel() As Boolean
        Get
            tCancel = ToolBar1.Buttons(indCancel).Enabled
        End Get
        Set(ByVal Value As Boolean)
            ToolBar1.Buttons(indCancel).Enabled = Value
        End Set
    End Property
    Public Property tFirst() As Boolean
        Get
            tFirst = ToolBar1.Buttons(indFirst).Enabled
        End Get
        Set(ByVal Value As Boolean)
            ToolBar1.Buttons(indFirst).Enabled = Value
        End Set
    End Property
    Public Property tPrev() As Boolean
        Get
            tPrev = ToolBar1.Buttons(indPre).Enabled
        End Get
        Set(ByVal Value As Boolean)
            ToolBar1.Buttons(indPre).Enabled = Value
        End Set
    End Property
    Public Property tNext() As Boolean
        Get
            tNext = ToolBar1.Buttons(indNext).Enabled
        End Get
        Set(ByVal Value As Boolean)
            ToolBar1.Buttons(indNext).Enabled = Value
        End Set
    End Property
    Public Property tRef() As Boolean
        Get
            tRef = ToolBar1.Buttons(indRefresh).Enabled
        End Get
        Set(ByVal Value As Boolean)
            ToolBar1.Buttons(indRefresh).Enabled = Value
        End Set
    End Property
    Public Property tSite() As Boolean
        Get
            tSite = ToolBar1.Buttons(indSite).Enabled
        End Get
        Set(ByVal Value As Boolean)
            ToolBar1.Buttons(indSite).Enabled = Value
        End Set
    End Property
    Public Property tLast() As Boolean
        Get
            tLast = ToolBar1.Buttons(indLast).Enabled
        End Get
        Set(ByVal Value As Boolean)
            ToolBar1.Buttons(indLast).Enabled = Value
        End Set
    End Property
    Public WriteOnly Property DispRec() As String
        Set(ByVal DispRec As String)
            LblRec.Text = DispRec
        End Set
    End Property
    Public Property Mode() As String
        Get
            Mode = ActiveMode.Text
        End Get
        Set(ByVal Value As String)
            ActiveMode.Text = Value
        End Set
    End Property
    Public Property tFind() As Boolean
        Get
            tFind = ToolBar1.Buttons(indFind).Enabled
        End Get
        Set(ByVal Value As Boolean)
            ToolBar1.Buttons(indFind).Enabled = Value
        End Set
    End Property
    Public Property tPrn() As Boolean
        Get
            tPrn = ToolBar1.Buttons(indPrint).Enabled
        End Get
        Set(ByVal Value As Boolean)
            ToolBar1.Buttons(indPrint).Enabled = Value
        End Set
    End Property
    Public Property tSave() As Boolean
        Get
            tSave = ToolBar1.Buttons(indSave).Enabled
        End Get
        Set(ByVal Value As Boolean)
            ToolBar1.Buttons(indSave).Enabled = Value
        End Set
    End Property
    Public Property tDiscard() As Boolean
        Get
            tDiscard = ToolBar1.Buttons(indDiscard).Enabled
        End Get
        Set(ByVal Value As Boolean)
            ToolBar1.Buttons(indDiscard).Enabled = Value
        End Set
    End Property
    Public Property tExit() As Boolean
        Get
            tExit = ToolBar1.Buttons(indExit).Enabled
        End Get
        Set(ByVal Value As Boolean)
            ToolBar1.Buttons(indExit).Enabled = Value
        End Set
    End Property
    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar1.ButtonClick
        Try
            FButtonClick(ToolBar1.Buttons.IndexOf(e.Button))
        Catch Ex As Exception
            MsgBox(Ex.Message.ToString, MsgBoxStyle.Exclamation, FrmParent.Text)
        End Try
    End Sub
    Public Sub FButtonClick(ByVal BytIndex As Byte, Optional ByVal SilentMode As Boolean = False)
        Select Case BytIndex
            Case indAdd
                SetDisp(False)
                BlankTextBoxes(FrmParent)
                ChangeTextState(Me.Parent, True)
                ActiveMode.Text = "Add"
                RaiseEvent tbAdd()
            Case indEdit
                SetDisp(False)
                ChangeTextState(Me.Parent, True)
                ActiveMode.Text = "Edit"
                RaiseEvent tbEdit()
            Case indDel
                RaiseEvent tbDel()
            Case indCancel
                ChangeTextState(Me.Parent, False)
                ActiveMode.Text = "Browse"
                RaiseEvent tbCancel()
            Case indFirst
                If FrmParent.BmBMaster IsNot Nothing Then
                    If FrmParent.BMBMaster.Count <= 0 Then Exit Sub
                    FrmParent.BMBMaster.Position = 0
                    FrmParent.MoveRec()
                    RaiseEvent tbFirst()
                End If
            Case indPre
                If FrmParent.BmBMaster IsNot Nothing Then
                    If FrmParent.BMBMaster.Count <= 0 Then Exit Sub
                    FrmParent.BMBMaster.Position -= 1
                    FrmParent.MoveRec()
                    RaiseEvent tbPrev()
                End If
            Case indNext
                If FrmParent.BmBMaster IsNot Nothing Then
                    If FrmParent.BMBMaster.Count <= 0 Then Exit Sub
                    FrmParent.BMBMaster.Position += 1
                    FrmParent.MoveRec()
                    RaiseEvent tbNext()
                End If
            Case indLast
                If FrmParent.BmBMaster IsNot Nothing Then
                    If FrmParent.BMBMaster.Count <= 0 Then Exit Sub
                    FrmParent.BMBMaster.Position = FrmParent.BMBMaster.Count - 1
                    FrmParent.MoveRec()
                    RaiseEvent tbLast()
                End If
            Case indFind
                RaiseEvent tbFind()
            Case indPrint
                RaiseEvent tbPrn()
            Case indSave
                RaiseEvent tbSave()
            Case indDiscard
                If Not SilentMode Then
                    If MsgBox("Are You Sure? You Want To Discard The Changes. ", MsgBoxStyle.Question + MsgBoxStyle.YesNo, FrmParent.Text) = MsgBoxResult.Yes Then
                        RaiseEvent tbDiscard()
                        ChangeTextState(Me.Parent, False)
                        SetDisp(True)
                        FrmParent.MoveRec()
                        ActiveMode.Text = "Browse"
                    End If
                Else
                    RaiseEvent tbDiscard()
                    ChangeTextState(Me.Parent, False)
                    SetDisp(True)
                    FrmParent.MoveRec()
                    ActiveMode.Text = "Browse"
                End If
            Case indSite
                RaiseEvent tbSite()
            Case indRefresh
                RaiseEvent tbRef()
            Case indExit
                RaiseEvent tbExit()
                'Try
                FrmParent.close()
                'Catch Ex As Exception
                '    MsgBox(Ex.Message.ToString)
                'End Try
            Case indNull
                ChangeTextState(Me.Parent, False)
                SetDisp(True)
                ActiveMode.Text = "Browse"
        End Select
    End Sub
    Public Sub SetDisp(ByVal Enb As Boolean)
        If Enb = True Then
            If InStr(Me.Tag, "A") <> 0 Then Me.tAdd = Enb Else Me.tAdd = Not Enb
            If InStr(Me.Tag, "E") <> 0 Then Me.tEdit = Enb Else Me.tEdit = Not Enb
            If InStr(Me.Tag, "D") <> 0 Then Me.tDel = Enb Else Me.tDel = Not Enb
            If InStr(Me.Tag, "P") <> 0 Then Me.tPrn = Enb Else Me.tPrn = Not Enb
        Else
            Me.tAdd = Enb
            Me.tEdit = Enb
            Me.tDel = Enb
            Me.tCancel = Enb
            Me.tPrn = Enb
        End If
        Me.tSite = Enb
        Me.tFirst = Enb
        Me.tPrev = Enb
        Me.tNext = Enb
        Me.tLast = Enb
        Me.tFind = Enb
        Me.tSave = Not Enb
        Me.tDiscard = Not Enb
        Me.tExit = Enb
        ChangeTextState(FrmParent, Not Enb)
        If ActiveMode.Text = "Add" Or ActiveMode.Text = "Edit" Then ActiveMode.Text = "Browse"
    End Sub
    Public Function BUTTONS(ByVal RecordCount As Byte, ByVal navSig As NavDirect) As Boolean
        On Error GoTo BTNERR
        Me.btbFirst.Enabled = True
        Me.btbPrevious.Enabled = True
        Me.btbLast.Enabled = True
        Me.btbNext.Enabled = True
        If navSig = NavDirect.NavFirst Or navSig = NavDirect.NavPrevious Then
            If navSig = NavDirect.NavFirst Or RecordCount = 1 Then
                Me.btbFirst.Enabled = False
                Me.btbPrevious.Enabled = False
            End If
        ElseIf navSig = NavDirect.NavLast Or navSig = NavDirect.NavNext Then
            If navSig = NavDirect.NavLast Or RecordCount = 1 Then
                Me.btbLast.Enabled = False
                Me.btbNext.Enabled = False
            End If
        End If
        Exit Function
BTNERR:
        If Err.Number > 0 Then MsgBox(Err.Description, MsgBoxStyle.Exclamation, FrmParent.Text)
    End Function
    Public WriteOnly Property SetButtonsMode() As SetButtons
        Set(ByVal Value As SetButtons)
            BUTTONS(Value.RecCount, Value.NavDirection)
        End Set
    End Property
    Public WriteOnly Property FetchKeys() As System.Windows.Forms.KeyEventArgs
        Set(ByVal Value As System.Windows.Forms.KeyEventArgs)
            'Alter Section starts
            If ToolBar1.Buttons(indAdd).Enabled And Value.KeyCode = Keys.F2 Then ToolBar1_ButtonClick(Me, New Windows.Forms.ToolBarButtonClickEventArgs(btbAdd))
            If ToolBar1.Buttons(indEdit).Enabled And Value.KeyCode = Keys.F3 Then ToolBar1_ButtonClick(Me, New Windows.Forms.ToolBarButtonClickEventArgs(btbEdit))
            If ToolBar1.Buttons(indDel).Enabled And Value.KeyCode = Keys.F4 Then ToolBar1_ButtonClick(Me, New Windows.Forms.ToolBarButtonClickEventArgs(btbDel))
            If ToolBar1.Buttons(indCancel).Enabled And Value.KeyCode = Keys.F8 Then ToolBar1_ButtonClick(Me, New Windows.Forms.ToolBarButtonClickEventArgs(btbCancel))
            'EOF Alter section

            'Navigate Section starts
            If ToolBar1.Buttons(indFirst).Enabled And Value.KeyCode = Keys.Home Then ToolBar1_ButtonClick(Me, New Windows.Forms.ToolBarButtonClickEventArgs(btbFirst))
            If ToolBar1.Buttons(indLast).Enabled And Value.KeyCode = Keys.End Then ToolBar1_ButtonClick(Me, New Windows.Forms.ToolBarButtonClickEventArgs(btbLast))
            If ToolBar1.Buttons(indPre).Enabled And Value.KeyCode = Keys.PageUp Then ToolBar1_ButtonClick(Me, New Windows.Forms.ToolBarButtonClickEventArgs(btbPrevious))
            If ToolBar1.Buttons(indNext).Enabled And Value.KeyCode = Keys.PageDown Then ToolBar1_ButtonClick(Me, New Windows.Forms.ToolBarButtonClickEventArgs(btbNext))
            'EOF Navigate section

            'Update section starts
            If ToolBar1.Buttons(indRefresh).Enabled And Value.KeyCode = Keys.F5 Then ToolBar1_ButtonClick(Me, New Windows.Forms.ToolBarButtonClickEventArgs(btbRefresh))
            If ToolBar1.Buttons(indSite).Enabled And Value.KeyCode = Keys.F11 Then ToolBar1_ButtonClick(Me, New Windows.Forms.ToolBarButtonClickEventArgs(btbSite))

            If ToolBar1.Buttons(indSave).Enabled And (Value.KeyCode = Keys.S And Value.Control) Then ToolBar1_ButtonClick(Me, New Windows.Forms.ToolBarButtonClickEventArgs(btbSave))
            If ToolBar1.Buttons(indDiscard).Enabled And Value.KeyCode = Keys.Escape Then ToolBar1_ButtonClick(Me, New Windows.Forms.ToolBarButtonClickEventArgs(btbDiscard))
            'EOF Update section

            If ToolBar1.Buttons(indExit).Enabled And Value.KeyCode = Keys.F10 Then ToolBar1_ButtonClick(Me, New Windows.Forms.ToolBarButtonClickEventArgs(btbExit))
        End Set
    End Property
    Public Function TopKey_Down(ByRef e As System.Windows.Forms.KeyEventArgs) As Boolean
        Select Case e.KeyValue
            Case Keys.F2    '113
                If ToolBar1.Buttons(indAdd).Enabled Then TopKey_Down = True : FButtonClick(indAdd)
            Case Keys.F3    '114
                If ToolBar1.Buttons(indEdit).Enabled Then TopKey_Down = True : FButtonClick(indEdit)
            Case Keys.F4    '115
                If ToolBar1.Buttons(indDel).Enabled Then TopKey_Down = True : FButtonClick(indDel)
            Case (Keys.F And e.Control)   'Ctrl+F (70 + 2)
                If ToolBar1.Buttons(indFind).Enabled Then TopKey_Down = True : FButtonClick(indFind)
            Case (Keys.P And e.Control)   'Ctrl+P (80 + 2)
                If ToolBar1.Buttons(indPrint).Enabled Then TopKey_Down = True : FButtonClick(indPrint)
            Case (Keys.S And e.Control)   'Ctrl+S (83 + 2)
                If ToolBar1.Buttons(indSave).Enabled Then TopKey_Down = True : FButtonClick(indSave)
            Case Keys.Escape   'vbKeyE And Shift = 2   'Ctrl+E (69 + 2)
                If ToolBar1.Buttons(indDiscard).Enabled Then TopKey_Down = True : FButtonClick(indDiscard)
            Case Keys.F5
                If ToolBar1.Buttons(indRefresh).Enabled Then TopKey_Down = True : FButtonClick(indRefresh)
            Case Keys.F11
                If ToolBar1.Buttons(indSite).Enabled Then TopKey_Down = True : FButtonClick(indSite)
            Case Keys.F10
                If ToolBar1.Buttons(indExit).Enabled Then TopKey_Down = True : FButtonClick(indExit)
            Case Keys.Home          ' Home (For Move First)
                If ToolBar1.Buttons(indFirst).Enabled Then TopKey_Down = True : FButtonClick(indFirst)
            Case Keys.PageUp        ' PageUp (For Move Previous)
                If ToolBar1.Buttons(indPre).Enabled Then TopKey_Down = True : FButtonClick(indPre)
            Case Keys.PageDown      ' PageDown (For Move Next)
                If ToolBar1.Buttons(indNext).Enabled Then TopKey_Down = True : FButtonClick(indNext)
            Case Keys.End           ' End (For Move Last)
                If ToolBar1.Buttons(indLast).Enabled Then TopKey_Down = True : FButtonClick(indLast)
        End Select
    End Function
    Private Sub Topctrl_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Height = 41
    End Sub
    Public Sub FSetDispRec(ByRef BMB As BindingManagerBase)
        Me.DispRec = BMB.Position + 1 & "/" & BMB.Count
        If BMB.Position = 0 Then
            btbFirst.Enabled = False
            btbPrevious.Enabled = False
            btbNext.Enabled = True
            btbLast.Enabled = True
        ElseIf BMB.Position = BMB.Count - 1 Then
            btbFirst.Enabled = True
            btbPrevious.Enabled = True
            btbNext.Enabled = False
            btbLast.Enabled = False
        Else
            btbFirst.Enabled = True
            btbPrevious.Enabled = True
            btbNext.Enabled = True
            btbLast.Enabled = True
        End If
        If BMB.Position < 0 Then
            btbDel.Enabled = False
            btbEdit.Enabled = False
            btbFirst.Enabled = False
            btbPrevious.Enabled = False
            btbNext.Enabled = False
            btbLast.Enabled = False
            btbFind.Enabled = False
            btbPrint.Enabled = False

        End If
    End Sub
    Public Sub FSetParent(ByVal Frm As Form, ByVal StrPermission As String, ByVal DTVar As DataTable)
        FrmParent = Frm
        DTUP = DTVar
        Me.Tag = StrPermission
        LblDocId.Text = ""
    End Sub
    Public Sub FIniForm(ByRef DTMaster As DataTable, ByVal DBCon As SqlClient.SqlConnection, ByVal StrSQL As String, _
    Optional ByVal BlnIsMaster As Boolean = False, Optional ByVal CmbMaster As System.Windows.Forms.ComboBox = Nothing, _
    Optional ByVal StrCmbValue As String = "", Optional ByVal StrCmbDisplay As String = "", _
    Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 0)

        Dim IntCurrectPosition As Integer
        Dim DCTemp(1) As DataColumn
        Dim ADTemp As SqlClient.SqlDataAdapter

        If FrmParent.BMBMaster IsNot Nothing Then
            IntCurrectPosition = FrmParent.BMBMaster.Position
        End If

        If BytRefresh = 0 Then IntCurrectPosition = FrmParent.BMBMaster.Position
        If BytDel = 1 Then
            IntCurrectPosition = FrmParent.BMBMaster.Position
            If IntCurrectPosition > FrmParent.BMBMaster.Count - 1 Then
                IntCurrectPosition = FrmParent.BMBMaster.Count - 1
            End If
        End If

        If LblDocId.Text <> "" Or BytRefresh = 1 Or BytDel = 1 Then
            DTMaster.Clear()
            DTMaster.Dispose()
            DTMaster = Nothing
            DTMaster = New DataTable
            ADTemp = New SqlClient.SqlDataAdapter(StrSQL, DBCon)
            ADTemp.Fill(DTMaster)

            DCTemp(0) = DTMaster.Columns(0)
            DTMaster.PrimaryKey = DCTemp
            FrmParent.BMBMaster = BindingContext(DTMaster)

            '==============================
            '===== For Master Combobox ====
            '============ Start ===========
            '==============================
            If BlnIsMaster Then
                Dim DTCmb As New DataTable
                DTCmb = DTMaster
                IniMasterHelpList(CmbMaster, DTCmb, StrCmbValue, StrCmbDisplay)
            End If
            '==============================
            '===== For Master Combobox ====
            '============ End =============
            '==============================

            If LblDocId.Text <> "" Then
                FrmParent.BMBMaster.Position = DTMaster.Rows.IndexOf(DTMaster.Rows.Find(LblDocId.Text))
                IntCurrectPosition = FrmParent.BMBMaster.Position
                LblDocId.Text = ""
            End If
        End If

        If UCase(Mode) = "BROWSE" And BytDel = 0 Then IntCurrectPosition = FrmParent.BMBMaster.Count - 1
        FrmParent.BMBMaster.Position = IntCurrectPosition
        FSetDispRec(FrmParent.BMBMaster)
    End Sub
    Public Sub FIniForm(ByRef DTMaster As DataTable, ByVal DBCon As OleDb.OleDbConnection, ByVal StrSQL As String, _
    Optional ByVal BlnIsMaster As Boolean = False, Optional ByVal CmbMaster As System.Windows.Forms.ComboBox = Nothing, _
    Optional ByVal StrCmbValue As String = "", Optional ByVal StrCmbDisplay As String = "", _
    Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 0)

        Dim IntCurrectPosition As Integer
        Dim DCTemp(1) As DataColumn
        Dim ADTemp As OleDb.OleDbDataAdapter

        If BytRefresh = 0 Then IntCurrectPosition = FrmParent.BMBMaster.Position
        If BytDel = 1 Then
            IntCurrectPosition = FrmParent.BMBMaster.Position
            If IntCurrectPosition > FrmParent.BMBMaster.Count - 1 Then
                IntCurrectPosition = FrmParent.BMBMaster.Count - 1
            End If
        End If

        If LblDocId.Text <> "" Or BytRefresh = 1 Or BytDel = 1 Then
            DTMaster.Clear()
            DTMaster.Dispose()
            DTMaster = Nothing
            DTMaster = New DataTable
            ADTemp = New OleDb.OleDbDataAdapter(StrSQL, DBCon)
            ADTemp.Fill(DTMaster)

            DCTemp(0) = DTMaster.Columns(0)
            DTMaster.PrimaryKey = DCTemp
            FrmParent.BMBMaster = BindingContext(DTMaster)

            '==============================
            '===== For Master Combobox ====
            '============ Start ===========
            '==============================
            If BlnIsMaster Then
                Dim DTCmb As New DataTable
                DTCmb = DTMaster
                IniMasterHelpList(CmbMaster, DTCmb, StrCmbValue, StrCmbDisplay)
            End If
            '==============================
            '===== For Master Combobox ====
            '============ End =============
            '==============================

            If LblDocId.Text <> "" Then
                FrmParent.BMBMaster.Position = DTMaster.Rows.IndexOf(DTMaster.Rows.Find(LblDocId.Text))
                IntCurrectPosition = FrmParent.BMBMaster.Position
                LblDocId.Text = ""
            End If
        End If

        If UCase(Mode) = "BROWSE" And BytDel = 0 Then IntCurrectPosition = FrmParent.BMBMaster.Count - 1
        FrmParent.BMBMaster.Position = IntCurrectPosition
        FSetDispRec(FrmParent.BMBMaster)
    End Sub
    Public Sub IniMasterHelpList(ByVal ListBox As System.Windows.Forms.ComboBox, ByVal DT As DataTable, ByVal StrValue As String, ByVal StrDisplay As String)
        ListBox.DropDownStyle = ComboBoxStyle.DropDown
        ListBox.AutoCompleteSource = AutoCompleteSource.ListItems
        ListBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        ListBox.DataSource = DT
        ListBox.DisplayMember = StrDisplay
        ListBox.ValueMember = StrValue
    End Sub
    Public Sub FManageSite(ByVal StrTableName As String, ByVal SQLUpdateQuery As String, ByVal StrCurrentValue As String, ByVal DBActiveCon As SqlClient.SqlConnection)
        Dim GCnCmd As New SqlClient.SqlCommand
        Dim StrValue As String

        StrValue = FHPGD_Site(DBActiveCon, StrCurrentValue)
        If StrValue <> "" Then
            GCnCmd.Connection = DBActiveCon
            GCnCmd.CommandText = Replace(SQLUpdateQuery, "@", StrValue)
            GCnCmd.ExecuteNonQuery()
        End If
        GCnCmd.Dispose()
        GCnCmd = Nothing
    End Sub
    Public Sub FManageSite(ByVal StrTableName As String, ByVal SQLUpdateQuery As String, ByVal StrCurrentValue As String, ByVal DBActiveCon As SqlClient.SqlConnection, ByVal DBActiveConOledb As OleDb.OleDbConnection)
        Dim GCnCmd As New OleDb.OleDbCommand
        Dim StrValue As String

        StrValue = FHPGD_Site(DBActiveCon, StrCurrentValue)
        If StrValue <> "" Then
            GCnCmd.Connection = DBActiveConOledb
            GCnCmd.CommandText = Replace(SQLUpdateQuery, "@", StrValue)
            GCnCmd.ExecuteNonQuery()
        End If
        GCnCmd.Dispose()
        GCnCmd = Nothing

    End Sub
    Private Function FHPGD_Site(ByVal DBActiveCon As SqlClient.SqlConnection, ByVal StrCurrentValue As String)
        'Dim DTMain As New DataTable
        'Dim ADMain As SqlClient.SqlDataAdapter
        'Dim FRH As DMHelpGrid.FrmHelpGrid_Multi
        'Dim StrSendText As String
        Dim StrRtn As String = ""

        'StrSendText = ""
        'StrCurrentValue = Trim(Replace(StrCurrentValue, "|", "'"))
        'If StrCurrentValue = "" Then StrCurrentValue = "''"

        'ADMain = New SqlClient.SqlDataAdapter("Select  (CASE WHEN " & _
        '                              "(SELECT Count(*) FROM SiteMast SM1 WHERE SM1.Code=SM.Code AND SM1.Code In (" & StrCurrentValue & "))>0 Then " & _
        '                              "'þ' ELSE 'o' END) AS Tick, " & _
        '                              "SM.Code,SM.Name From SiteMast SM Order By SM.Name ", DBActiveCon)
        'ADMain.Fill(DTMain)
        'FRH = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(DTMain), StrSendText, 350, 380, , , False)
        'FRH.FFormatColumn(0, "Tick", 42, DataGridViewContentAlignment.MiddleCenter, True)
        'FRH.FFormatColumn(1, , 0, , False)
        'FRH.FFormatColumn(2, "Division", 250, DataGridViewContentAlignment.MiddleLeft)
        'FRH.StartPosition = FormStartPosition.CenterScreen
        'FRH.ShowDialog()

        'If FRH.BytBtnValue = 0 Then
        '    StrRtn = Trim(FRH.FFetchData(1, "|", "|", ",", True))
        '    If Trim(StrRtn) = "" Then StrRtn = "||"
        'End If
        'FRH = Nothing
        Return StrRtn
    End Function
End Class
