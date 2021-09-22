Imports System.Windows.Forms

Public Class AgTextColumn
    Inherits DataGridViewTextBoxColumn
    Dim mMasterHelp As Boolean = False

    Public Enum TxtValueType
        Text_Value = 0
        Number_Value = 1
        Date_Value = 2
        YesNo_Value = 3
    End Enum

    Public Enum TxtCase
        None = 0
        Upper_Case = 1
        Lower_Case = 2
        Sentance_Case = 3
    End Enum

    Public Property AgMasterHelp() As Boolean
        Get
            AgMasterHelp = mMasterHelp
        End Get
        Set(ByVal value As Boolean)
            mMasterHelp = value
        End Set
    End Property

    Dim mHelpDataSet As DataSet = Nothing
    Dim mTopOfContainer As Integer
    Dim mLeftOfContainer As Integer
    Dim mHelpGridHeight As Integer
    Dim mLastHiddenColumns As Integer

    Dim mMandatory As Boolean = False
    Dim mNumLeft As Integer = 0
    Dim mNumRight As Integer = 0
    Dim mNegetiveAllow As Boolean = False
    Dim mTitleCase As Boolean = False
    Dim mValueType As TxtValueType = TxtValueType.Text_Value
    Dim mTxtCase As TxtCase = TxtCase.None
    Dim mAllowDuplicate As Boolean = True
    Dim mReadOnly As Boolean = False
    Dim mAgRowFilter As String = ""
    Dim mDefaultValue As String = ""

    Public Property AgDefaultValue() As String
        Get
            AgDefaultValue = mDefaultValue
        End Get
        Set(ByVal value As String)
            mDefaultValue = value
        End Set
    End Property


    Public Property AgRowFilter() As String
        Get
            AgRowFilter = mAgRowFilter
        End Get
        Set(ByVal value As String)
            mAgRowFilter = value
        End Set
    End Property


    Public Property AgAllowDuplicate() As Boolean
        Get
            AgAllowDuplicate = mAllowDuplicate
        End Get
        Set(ByVal value As Boolean)
            mAllowDuplicate = value
        End Set
    End Property


    Public Property AgHelpDataSet() As DataSet
        Get
            AgHelpDataSet = mHelpDataSet
        End Get
        Set(ByVal value As DataSet)
            mHelpDataSet = value
        End Set
    End Property

    Public Property AgTopOfContainer() As Integer
        Get
            AgTopOfContainer = mTopOfContainer
        End Get
        Set(ByVal value As Integer)
            mTopOfContainer = value
        End Set
    End Property

    Public Property AgLeftOfContainer() As Integer
        Get
            AgLeftOfContainer = mLeftOfContainer
        End Get
        Set(ByVal value As Integer)
            mLeftOfContainer = value
        End Set
    End Property

    Public Property AgHelpGridHeight() As Integer
        Get
            AgHelpGridHeight = mHelpGridHeight
        End Get
        Set(ByVal value As Integer)
            mHelpGridHeight = value
        End Set
    End Property

    Public Property AgLastHiddenColumns() As Integer
        Get
            AgLastHiddenColumns = mLastHiddenColumns
        End Get
        Set(ByVal value As Integer)
            mLastHiddenColumns = value
        End Set
    End Property


    Public Property AgMandatory() As Boolean
        Get
            AgMandatory = mMandatory
        End Get
        Set(ByVal value As Boolean)
            mMandatory = value
        End Set
    End Property

    Public Property AgNumberLeftPlaces() As Integer
        Get
            AgNumberLeftPlaces = mNumLeft
        End Get
        Set(ByVal value As Integer)
            mNumLeft = value
        End Set
    End Property

    Public Property AgNumberRightPlaces() As Integer
        Get
            AgNumberRightPlaces = mNumRight
        End Get
        Set(ByVal value As Integer)
            mNumRight = value
        End Set
    End Property

    Public Property AgNumberNegetiveAllow() As Boolean
        Get
            AgNumberNegetiveAllow = mNegetiveAllow
        End Get
        Set(ByVal value As Boolean)
            mNegetiveAllow = value
        End Set
    End Property

    Public Property AgValueType() As TxtValueType
        Get
            AgValueType = mValueType
        End Get
        Set(ByVal value As TxtValueType)
            mValueType = value
            If mValueType <> TxtValueType.Number_Value Then
                mNegetiveAllow = False
                mNumLeft = 0
                mNumRight = 0
            End If
        End Set
    End Property

    Public Property AgTxtCase() As TxtCase
        Get
            AgTxtCase = mTxtCase
        End Get
        Set(ByVal value As TxtCase)
            mTxtCase = value
        End Set
    End Property

    Public Property AgReadOnly() As Boolean
        Get
            AgReadOnly = mReadOnly
        End Get
        Set(ByVal value As Boolean)
            mReadOnly = value
        End Set
    End Property
End Class
