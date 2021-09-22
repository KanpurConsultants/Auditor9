Imports System.Threading

Public Class clsThread
    Public m_ThreadList As New ArrayList
    Dim mQry$
    Dim mStrThreadCounter As String


    'This variable will hold the Thread Index that are passed from the main window, when we created it
    Private m_ThreadIndex As Integer

    'This is local thread variable. We will send the value of this variable to the parent form
    Private m_Counter As Integer = 0

    'We will need this variable to pass argument to the method of the main window
    Private m_Args(1) As Object

    'This will hold the ref to the main window,
    Private m_MainWindow As Form

    'We have to create a delegate to call the Notification Method on the main form (ReceiveThreadMessage())
    'Delete is nothing, but a Type Safe Pointer. It works same way like Callback function in C++/VC++
    'If you like to know more about delegates plz visit http://www.15seconds.com/issue/020815.htm
    'This delete sub should have same signature (declaration) as the method, that are there in the main from.
    'Which we will call from this thread. 

    'Here we are going to call the method ReceiveThreadMessage() of the main form. 
    'So the declaration of the delete should be same as ReceiveThreadMessage()

    Private Delegate Sub NotifyMainWindow(ByVal ThreadIndex As Integer, ByVal Counter As Integer)
    'We need an object of this deletegate
    Private m_NotifyMainWindow As NotifyMainWindow

    Public Sub New(ByVal ThreadIndex As Integer) ', ByRef MainWindow As MDIMain)
        m_ThreadIndex = ThreadIndex
        'm_MainWindow = MainWindow

        'We need to point our delegate to the Method, which we want to call from this thread
        'm_NotifyMainWindow = AddressOf MainWindow.ReceiveThreadMessage
    End Sub

    Public Sub StartThread()
        While True
            m_Counter = m_Counter + 1

            'we need to create this array such a way, that it should contains the no of elements, that we need
            'to pass as the arguments.
            'Here we will pass two arguments ThreadIndex and Counter, so we took the array with 2 elements.
            'Now we need to place the variable to the appropriate position of this array.
            'Like : Our First Argument is ThreadIndex, so we will put ThreadIndex into the first element and 
            'm_counter into the second element.
            'Basically you have to put the variables into the array with the same sequence that is there in the
            'argument list
            ReDim m_Args(1)
            m_Args(0) = m_ThreadIndex
            m_Args(1) = m_Counter

            'Call the notificaiton method on the main window by the delegate
            m_MainWindow.Invoke(m_NotifyMainWindow, m_Args)

            'wait for some time before continuing loop
            Thread.Sleep(1000)

        End While
    End Sub



    Private Sub ProcStartthread()
        'Check if the 1st element of the arraylist (m_ThreadList) contains any thread object
        If Not m_ThreadList(0) Is Nothing Then
            'If the 1st element contains any thread object. Then check its status. If its running (IsAlive) then no need start it again
            If CType(m_ThreadList(0), Thread).IsAlive Then
                MsgBox("You can not start this thread again. Becoz this thread is still alive !!", MsgBoxStyle.Critical)
            Else
                'Thread object is there but thread is not active. Its dead !! You can create new again
                GoTo StartThread
            End If
        Else
            'No thread object is there in 1st element of our thread list. You can create new 
            GoTo StartThread
        End If
        Exit Sub
StartThread:
        'Create a object of our Thread Class (clsThread). The new thread will work with this object.
        'You can assume that this object will become a thread. Though it's not, But it will ease your understanding

        'When creating the object we can pass arguments to the constructer (New method) of the class for the new thread.
        'Thus when the thread will run independently, it will have these variables.

        'We will pass a integer as a thread index. So when this thread will send message to our main window (this form),
        'We will pass the index from the thread to this from, and we will be able to recognize the thread by the index.
        'We will be able to know, from which thread the message is comming.
        'Also we will pass a ref to this form to the thread, that the tread can communicate with this form
        Dim objThreadClass As New clsThread(1) ', Me)

        'Now we will create a Thread Object. When we create a thread object, we need to specify a start point of the thread.
        'This start point is basically a Method of the Object, that we have just made. 
        'When we will ask the thread to start. It will execute the specified method of the object
        Dim objNewThread As New Thread(AddressOf objThreadClass.StartThread)

        'Here we will make the IsBackgound property of the thread as True, thus if your main application terminates,
        'all the thread associated with the main programe (ProcessID) will be terminated too.
        'Read the following comments from MSDN.

        'A thread is either a background thread or a foreground thread. 
        'Background threads are identical to foreground threads, 
        'except that background threads do not prevent a process from terminating. 
        'Once all foreground threads belonging to a process have terminated, 
        'the common language runtime ends the process. 
        'Any remaining background threads are stopped and do not complete.
        objNewThread.IsBackground = True

        'Now we will ask our thread to start, which will basically fire the method of our thread class, 
        'that we have specifed as the start point (the method StartThread())
        objNewThread.Start()

        'Now we will add this thread object to our thread list (m_ThreadList). Becoz we will need this object
        'to terminate or suspend the tread. Without referance to this object, the tread can't be handled
        m_ThreadList.Item(0) = objNewThread

        'We are dealing with 4 fixed thread. If you want to deal with unknown numbers of threads
        'Simply add each tread object to the arraylist and pass the last index of the array list as the thread index

        'The code will be changed like following

        'Dim objThreadClass As New clsThread(m_ThreadList.count, Me)
        'm_ThreadList.add(objNewThread)

        'This way, when u will get message from thread, you have to deal with appropriate elemtn in this arraylist
        'You will get back the index from thread
    End Sub

    'This the method which will accept message from thread, you can not access any control of the form, directly
    'from the thread. Instead, you need to pass some value from the tread to the form and according to those values
    'you have to take appropriate action on the form.
    'This method should be Public. Otherwise from the thread, u will not be able to invoke it
    Public Sub ReceiveThreadMessage(ByVal ThreadIndex As Integer, ByVal Counter As Integer)
        Dim DsTemp As DataSet = Nothing
        Dim ObjFrmRemindMe As FrmRemindMe
        Dim DTUP As New DataTable
        'The arguments ThreadIndex and Counter will be recieved from the thread. The thread will pass these arguments, 
        'While invoking this method

        'Now first of all we will check from which thread we are getting message, its indicated by the argument ThreadIndex
        'According to this value we will look into our ThreadList (m_ThreadList).
        'We can access the corresponding thread object by m_ThreadList(ThreadIndex-1)
        'Here we have -1 becoz we have thread index from 1 but start index of arraylist is 0.
        'So position of the thread object in arraylist is ThreadIndex-1.

        'We will show the counter to the appropriate label on the form. There are 4 counter.
        'We will show according to, from which thread we are getting message

        'We will also check if we checked the checkbox to stop the thread on a particular value of counter
        Select Case ThreadIndex
            Case 1
                ObjFrmRemindMe = New FrmRemindMe("AEDP", DTUP)
                mStrThreadCounter = AgL.GetDateTime(AgL.GCn, AgL.ECmd)

                mQry = " SELECT H.ID, H.V_Date, H.V_Time, H.EntryBy, H.Narration, " & _
                        " H.Reminder_Date, H.Reminder_Time, L.Reminder_To, L.Status, " & _
                        " substring(Convert(NVARCHAR,H.V_Date,3),0,12) + ' '+ H.V_Time AS RemindDateTime " & _
                        " FROM Reminder H " & _
                        " LEFT JOIN ReminderDetail L ON H.ID=L.ID " & _
                        " WHERE H.Reminder_Date + H.Reminder_Time <= '" & mStrThreadCounter & "' " & _
                        " AND L.STATUS ='Active' AND L.Reminder_To =" & AgL.Chk_Text(AgL.PubUserName) & " "

                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    If .Rows.Count > 0 Then

                        CType(ObjFrmRemindMe, FrmRemindMe).ReminderId = AgL.XNull(.Rows(0)("ID"))
                        CType(ObjFrmRemindMe, FrmRemindMe).RemindBy = AgL.XNull(.Rows(0)("EntryBy"))
                        CType(ObjFrmRemindMe, FrmRemindMe).RemindDateTime = AgL.XNull(.Rows(0)("RemindDateTime"))
                        CType(ObjFrmRemindMe, FrmRemindMe).RemindNarration = AgL.XNull(.Rows(0)("Narration"))
                        'ObjFrmRemindMe.StartPosition = FormStartPosition.CenterScreen
                        ObjFrmRemindMe.ShowDialog()

                    End If
                End With

        End Select
    End Sub

End Class
