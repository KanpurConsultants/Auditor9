' Copyright 2006 Herre Kuijpers - <herre@xs4all.nl>
'
' This source file(s) may be redistributed, altered and custimized
' by any means PROVIDING the authors name and all copyright
' notices remain intact.
' THIS SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
' EXPRESS OR IMPLIED. USE IT AT YOUR OWN RISK. THE AUTHOR ACCEPTS NO
' LIABILITY FOR ANY DATA DAMAGE/LOSS THAT THIS PRODUCT MAY CAUSE.
'-----------------------------------------------------------------------

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D

Namespace OutlookStyleControls

    Partial Public Class AgSideBar
        Inherits UserControl

        ''' <summary>
        ''' the OutlookBarButtons class contains the list of buttons
        ''' it manages adding and removing buttons, and updates the Outlookbar control
        ''' respectively. Note that this is a class, not a control!
        ''' </summary>
#Region "OutlookBarButtons list"
        Public Class OutlookBarButtons
            Inherits CollectionBase
            'protected ArrayList List;
            Protected m_parent As AgSideBar
            Public ReadOnly Property Parent() As AgSideBar
                Get
                    Return m_parent
                End Get
            End Property

            Friend Sub New(ByVal parent As AgSideBar)
                MyBase.New()
                Me.m_parent = parent
            End Sub

            Default Public ReadOnly Property Item(ByVal index As Integer) As OutlookBarButton
                Get
                    Return CType(List(index), OutlookBarButton)
                End Get
            End Property

            Public Sub Add(ByVal item As OutlookBarButton)
                If List.Count = 0 Then
                    m_parent.SelectedButton = item
                End If
                List.Add(item)
                item.Parent = Me.Parent
                Parent.ButtonlistChanged()
            End Sub

            Public Function Add(ByVal text As String, ByVal image As Image) As OutlookBarButton
                Dim b As OutlookBarButton = New OutlookBarButton(Me.m_parent)
                b.Text = text
                b.Image = image
                Me.Add(b)
                Return b
            End Function

            Public Function Add(ByVal text As String) As OutlookBarButton
                Return Me.Add(text, Nothing)
            End Function

            Public Function Add() As OutlookBarButton
                Return Me.Add()
            End Function

            Public Sub Remove(ByVal button As OutlookBarButton)
                List.Remove(button)
                m_parent.ButtonlistChanged()
            End Sub

            Public Function IndexOf(ByVal value As Object) As Integer
                Return List.IndexOf(value)
            End Function

#Region "handle CollectionBase events"
            Protected Overrides Sub OnInsertComplete(ByVal index As Integer, ByVal value As Object)
                Dim b As OutlookBarButton = CType(value, OutlookBarButton)
                b.Parent = Me.m_parent
                m_parent.ButtonlistChanged()
                MyBase.OnInsertComplete(index, value)
            End Sub

            Protected Overrides Sub OnSetComplete(ByVal index As Integer, ByVal oldValue As Object, ByVal newValue As Object)
                Dim b As OutlookBarButton = CType(newValue, OutlookBarButton)
                b.Parent = Me.m_parent
                Parent.ButtonlistChanged()
                MyBase.OnSetComplete(index, oldValue, newValue)
            End Sub

            Protected Overrides Sub OnClearComplete()
                Parent.ButtonlistChanged()
                MyBase.OnClearComplete()
            End Sub
#End Region ' handle CollectionBase events
        End Class
#End Region ' OutlookBarButtons list

#Region "OutlookBar property definitions"

        ''' <summary>
        ''' buttons contains the list of clickable OutlookBarButtons
        ''' </summary>
        Protected m_buttons As OutlookBarButtons

        ''' <summary>
        ''' this variable remembers which button is currently selected
        ''' </summary>
        Protected m_selectedButton As OutlookBarButton = Nothing

        ''' <summary>
        ''' this variable remembers the button index over which the mouse is moving
        ''' </summary>
        Protected hoveringButtonIndex As Integer = -1

        ''' <summary>
        ''' property to set the buttonHeigt
        ''' default is 30
        ''' </summary>
        Protected m_buttonHeight As Integer
        <Description("Specifies the height of each button on the OutlookBar"), Category("Layout")> _
        Public Property ButtonHeight() As Integer
            Get
                Return m_buttonHeight
            End Get
            Set(ByVal value As Integer)
                If value > 18 Then
                    m_buttonHeight = value
                Else
                    m_buttonHeight = 18
                End If
            End Set
        End Property

        Protected gradientButtonDark As Color = Color.FromArgb(178, 193, 140)
        <Description("Dark gradient color of the button"), Category("Appearance")> _
        Public Property GradientButtonNormalDark() As Color
            Get
                Return gradientButtonDark
            End Get
            Set(ByVal value As Color)
                gradientButtonDark = value
            End Set
        End Property

        Protected gradientButtonLight As Color = Color.FromArgb(234, 240, 207)
        <Description("Light gradient color of the button"), Category("Appearance")> _
        Public Property GradientButtonNormalLight() As Color
            Get
                Return gradientButtonLight
            End Get
            Set(ByVal value As Color)
                gradientButtonLight = value
            End Set
        End Property

        Protected m_gradientButtonHoverDark As Color = Color.FromArgb(247, 192, 91)
        <Description("Dark gradient color of the button when the mouse is moving over it"), Category("Appearance")> _
        Public Property GradientButtonHoverDark() As Color
            Get
                Return m_gradientButtonHoverDark
            End Get
            Set(ByVal value As Color)
                m_gradientButtonHoverDark = value
            End Set
        End Property

        Protected m_gradientButtonHoverLight As Color = Color.FromArgb(255, 255, 220)
        <Description("Light gradient color of the button when the mouse is moving over it"), Category("Appearance")> _
        Public Property GradientButtonHoverLight() As Color
            Get
                Return m_gradientButtonHoverLight
            End Get
            Set(ByVal value As Color)
                m_gradientButtonHoverLight = value
            End Set
        End Property

        Protected m_gradientButtonSelectedDark As Color = Color.FromArgb(239, 150, 21)
        <Description("Dark gradient color of the seleced button"), Category("Appearance")> _
        Public Property GradientButtonSelectedDark() As Color
            Get
                Return m_gradientButtonSelectedDark
            End Get
            Set(ByVal value As Color)
                m_gradientButtonSelectedDark = value
            End Set
        End Property

        Protected m_gradientButtonSelectedLight As Color = Color.FromArgb(251, 230, 148)
        <Description("Light gradient color of the seleced button"), Category("Appearance")> _
        Public Property GradientButtonSelectedLight() As Color
            Get
                Return m_gradientButtonSelectedLight
            End Get
            Set(ByVal value As Color)
                m_gradientButtonSelectedLight = value
            End Set
        End Property


        ''' <summary>
        ''' when a button is selected programatically, it must update the control
        ''' and repaint the buttons
        ''' </summary>
        <Browsable(False)> _
        Public Property SelectedButton() As OutlookBarButton
            Get
                Return m_selectedButton
            End Get
            Set(ByVal value As OutlookBarButton)
                ' assign new selected button
                PaintSelectedButton(m_selectedButton, value)

                ' assign new selected button
                m_selectedButton = value
            End Set
        End Property

        '[Browsable(false)]
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
  Public ReadOnly Property Buttons() As OutlookBarButtons
            Get
                Return m_buttons
            End Get
        End Property

#End Region ' OutlookBar property definitions

#Region "OutlookBar events"


        <Serializable()> _
        Public Class ButtonClickEventArgs
            Inherits MouseEventArgs
            Public Sub New(ByVal button As OutlookBarButton, ByVal evt As MouseEventArgs)
                MyBase.New(evt.Button, evt.Clicks, evt.X, evt.Y, evt.Delta)
                SelectedButton = button
            End Sub

            Public ReadOnly SelectedButton As OutlookBarButton
        End Class

        Public Delegate Sub ButtonClickEventHandler(ByVal sender As Object, ByVal e As ButtonClickEventArgs)

        Public Shadows Event Click As ButtonClickEventHandler

#End Region ' OutlookBar events

#Region "OutlookBar functions"

        Public Sub New()
            InitializeComponent()
            m_buttons = New OutlookBarButtons(Me)
            m_buttonHeight = 30 ' set default to 30
        End Sub

        Private Sub PaintSelectedButton(ByVal prevButton As OutlookBarButton, ByVal newButton As OutlookBarButton)
            If prevButton Is newButton Then
                Return ' no change so return immediately
            End If

            Dim selIdx As Integer = -1
            Dim valIdx As Integer = -1

            ' find the indexes of the previous and new button
            selIdx = m_buttons.IndexOf(prevButton)
            valIdx = m_buttons.IndexOf(newButton)

            ' now reset selected button
            ' mouse is leaving control, so unhighlight anythign that is highlighted
            Dim g As Graphics = Graphics.FromHwnd(Me.Handle)
            If selIdx >= 0 Then
                ' un-highlight current hovering button
                m_buttons(selIdx).PaintButton(g, 1, selIdx * (m_buttonHeight + 1) + 1, False, False)
            End If

            If valIdx >= 0 Then
                ' highlight newly selected button
                m_buttons(valIdx).PaintButton(g, 1, valIdx * (m_buttonHeight + 1) + 1, True, False)
            End If
            g.Dispose()
        End Sub

        ''' <summary>
        ''' returns the button given the coordinates relative to the Outlookbar control
        ''' </summary>
        ''' <param name="x"></param>
        ''' <param name="y"></param>
        ''' <returns></returns>
        Public Function HitTest(ByVal x As Integer, ByVal y As Integer) As OutlookBarButton
            Dim index As Integer = CInt((y - 1) / (m_buttonHeight + 1))
            If index >= 0 AndAlso index < m_buttons.Count Then
                Return m_buttons(index)
            Else
                Return Nothing
            End If
        End Function

        ''' <summary>
        ''' this function will setup the control to cope with changes in the buttonlist 
        ''' that is, addition and removal of buttons
        ''' </summary>
        Private Sub ButtonlistChanged()
            If (Not Me.DesignMode) Then ' only set sizes automatically at runtime
                Me.MaximumSize = New Size(0, m_buttons.Count * (m_buttonHeight + 1) + 1)
            End If

            Me.Invalidate()
        End Sub

#End Region ' OutlookBar functions

#Region "OutlookBar control event handlers"

        Private Sub OutlookBar_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            ' initiate the render style flags of the control
            SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.ResizeRedraw Or ControlStyles.DoubleBuffer Or ControlStyles.Selectable Or ControlStyles.UserMouse, True)
        End Sub

        Private Sub OutlookBar_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles MyBase.Paint
            Dim top As Integer = 1
            For Each b As OutlookBarButton In Buttons
                b.PaintButton(e.Graphics, 1, top, b.Equals(Me.m_selectedButton), False)
                top += b.Height + 1
            Next b
        End Sub

        Private Sub OutlookBar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Click
            If Not (TypeOf e Is MouseEventArgs) Then
                Return
            End If

            ' case to MouseEventArgs so position and mousebutton clicked can be used
            Dim mea As MouseEventArgs = CType(e, MouseEventArgs)

            ' only continue if left mouse button was clicked
            If mea.Button <> Windows.Forms.MouseButtons.Left Then
                Return
            End If

            Dim index As Integer = CInt((mea.Y - 1) / (m_buttonHeight + 1))

            If index < 0 OrElse index >= m_buttons.Count Then
                Return
            End If

            Dim button As OutlookBarButton = m_buttons(index)
            If button Is Nothing Then
                Return
            End If
            If (Not button.Enabled) Then
                Return
            End If

            ' ok, all checks passed so assign the new selected button
            ' and raise the event
            SelectedButton = button

            Dim bce As ButtonClickEventArgs = New ButtonClickEventArgs(m_selectedButton, mea)
            If Not ClickEvent Is Nothing Then ' only invoke on left mouse click
                ClickEvent.Invoke(Me, bce)
            End If
        End Sub

        Private Sub OutlookBar_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.DoubleClick
            'TODO: only if you intend to support a doubleclick
            ' this can be implemented exactly like the click event
        End Sub


        Private Sub OutlookBar_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.MouseLeave
            ' mouse is leaving control, so unhighlight anything that is highlighted
            If hoveringButtonIndex >= 0 Then
                ' so we need to change the hoveringButtonIndex to the new index
                Dim g As Graphics = Graphics.FromHwnd(Me.Handle)
                Dim b1 As OutlookBarButton = m_buttons(hoveringButtonIndex)

                ' un-highlight current hovering button
                b1.PaintButton(g, 1, hoveringButtonIndex * (m_buttonHeight + 1) + 1, b1.Equals(m_selectedButton), False)
                hoveringButtonIndex = -1
                g.Dispose()
            End If
        End Sub

        Private Sub OutlookBar_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove
            If e.Button = Windows.Forms.MouseButtons.None Then
                ' determine over which button the mouse is moving
                Dim index As Integer = CInt((e.Location.Y - 1) / (m_buttonHeight + 1))
                If index >= 0 AndAlso index < m_buttons.Count Then
                    If hoveringButtonIndex = index Then
                        Return ' nothing changed so we're done, current button stays highlighted
                    End If

                    ' so we need to change the hoveringButtonIndex to the new index
                    Dim g As Graphics = Graphics.FromHwnd(Me.Handle)

                    If hoveringButtonIndex >= 0 Then
                        Dim b1 As OutlookBarButton = m_buttons(hoveringButtonIndex)

                        ' un-highlight current hovering button
                        b1.PaintButton(g, 1, hoveringButtonIndex * (m_buttonHeight + 1) + 1, b1.Equals(m_selectedButton), False)
                    End If

                    ' highlight new hovering button
                    Dim b2 As OutlookBarButton = m_buttons(index)
                    b2.PaintButton(g, 1, index * (m_buttonHeight + 1) + 1, b2.Equals(m_selectedButton), True)
                    hoveringButtonIndex = index ' set to new index
                    g.Dispose()

                Else
                    ' no hovering button, so un-highlight all.
                    If hoveringButtonIndex >= 0 Then
                        ' so we need to change the hoveringButtonIndex to the new index
                        Dim g As Graphics = Graphics.FromHwnd(Me.Handle)
                        Dim b1 As OutlookBarButton = m_buttons(hoveringButtonIndex)

                        ' un-highlight current hovering button
                        b1.PaintButton(g, 1, hoveringButtonIndex * (m_buttonHeight + 1) + 1, b1.Equals(m_selectedButton), False)
                        hoveringButtonIndex = -1
                        g.Dispose()
                    End If
                End If
            End If
        End Sub

        ''' <summary>
        ''' isResizing is used as a signal, so this method is not called recusively
        ''' this prevents a stack overflow
        ''' </summary>
        Private isResizing As Boolean = False
        Private Sub OutlookBar_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Resize
            ' only set sizes automatically at runtime
            If (Not Me.DesignMode) Then
                If (Not isResizing) Then
                    isResizing = True
                    If (Me.Height - 1) Mod (m_buttonHeight + 1) > 0 Then
                        Me.Height = CInt(((Me.Height - 1) / (m_buttonHeight + 1)) * (m_buttonHeight + 1) + 1)
                    End If
                    Me.Invalidate()
                    isResizing = False
                End If
            End If
        End Sub

#End Region ' OutlookBar control event handlers

    End Class

    ''' <summary>
    ''' OutlookbarButton represents a button on the Outlookbar
    ''' this is an internally used class (not a control!)
    ''' </summary>
#Region "OutlookBarButton"
    Public Class OutlookBarButton ' : IComponent
        Private m_enabled As Boolean = True

        <Description("Indicates wether the button is enabled"), Category("Behavior"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> _
        Public Property Enabled() As Boolean
            Get
                Return m_enabled
            End Get
            Set(ByVal value As Boolean)
                m_enabled = Value
            End Set
        End Property

        Protected m_image As Image = Nothing
        <Description("The image that will be displayed on the button"), Category("Data"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> _
        Public Property Image() As Image
            Get
                Return m_image
            End Get
            Set(ByVal value As Image)
                m_image = Value
                m_parent.Invalidate()
            End Set
        End Property

        Protected m_tag As Object = Nothing
        <Description("User-defined data to be associated with the button"), Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> _
        Public Property Tag() As Object
            Get
                Return m_tag
            End Get
            Set(ByVal value As Object)
                m_tag = Value
            End Set
        End Property

        Public Sub New()
            Me.m_parent = New AgSideBar() ' set it to a dummy outlookbar control
            m_text = ""
        End Sub

        Public Sub New(ByVal parent As AgSideBar)
            Me.m_parent = parent
            m_text = ""
        End Sub

        Protected m_parent As AgSideBar

        Friend Property Parent() As AgSideBar
            Get
                Return m_parent
            End Get
            Set(ByVal value As AgSideBar)
                m_parent = Value
            End Set
        End Property

        Protected m_text As String
        <Description("The text that will be displayed on the button"), Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> _
        Public Property Text() As String
            Get
                Return m_text
            End Get
            Set(ByVal value As String)
                m_text = Value
                m_parent.Invalidate()
            End Set
        End Property

        Protected m_height As Integer
        Public ReadOnly Property Height() As Integer
            Get
                If m_parent Is Nothing Then
                    Return 30
                Else
                    Return m_parent.ButtonHeight
                End If
            End Get

        End Property

        Public ReadOnly Property Width() As Integer
            Get
                If m_parent Is Nothing Then
                    Return 60
                Else
                    Return m_parent.Width - 2
                End If
            End Get
        End Property

        ''' <summary>
        ''' the outlook button will paint itself on its container (the OutlookBar)
        ''' </summary>
        ''' <param name="graphics"></param>
        ''' <param name="x"></param>
        ''' <param name="y"></param>
        ''' <param name="IsSelected"></param>
        ''' <param name="IsHovering"></param>
        Public Sub PaintButton(ByVal graphics As Graphics, ByVal x As Integer, ByVal y As Integer, ByVal IsSelected As Boolean, ByVal IsHovering As Boolean)
            Dim br As Brush
            Dim rect As Rectangle = New Rectangle(0, y, Me.Width, Me.Height)
            If m_enabled Then
                If IsSelected Then
                    If IsHovering Then
                        br = New LinearGradientBrush(rect, m_parent.GradientButtonSelectedDark, m_parent.GradientButtonSelectedLight, 90.0F)
                    Else
                        br = New LinearGradientBrush(rect, m_parent.GradientButtonSelectedLight, m_parent.GradientButtonSelectedDark, 90.0F)
                    End If
                Else
                    If IsHovering Then
                        br = New LinearGradientBrush(rect, m_parent.GradientButtonHoverLight, m_parent.GradientButtonHoverDark, 90.0F)
                    Else
                        br = New LinearGradientBrush(rect, m_parent.GradientButtonNormalLight, m_parent.GradientButtonNormalDark, 90.0F)
                    End If
                End If
            Else
                br = New LinearGradientBrush(rect, m_parent.GradientButtonNormalLight, m_parent.GradientButtonNormalDark, 90.0F)
            End If

            graphics.FillRectangle(br, x, y, Me.Width, Me.Height)
            br.Dispose()

            If text.Length > 0 Then
                graphics.DrawString(Me.Text, m_parent.Font, Brushes.Black, 36, CSng(y + Me.Height / 2 - m_parent.Font.Height / 2))
            End If

            If Not m_image Is Nothing Then
                graphics.DrawImage(m_image, CSng(36 / 2 - m_image.Width / 2), CSng(y + Me.Height / 2 - m_image.Height / 2), m_image.Width, m_image.Height)
            End If
        End Sub
    End Class
#End Region


End Namespace
