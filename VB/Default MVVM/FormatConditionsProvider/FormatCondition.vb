Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Media
Imports DevExpress.Data.Filtering
Imports System.ComponentModel

Namespace Default_MVVM
	Public Class FormatCondition
		Implements INotifyPropertyChanged

		' Fields...
		Private _FieldName As String
		Private _Criteria As CriteriaOperator
		Private _Appearance As Appearance

		Public Property Appearance() As Appearance
			Get
				If _Appearance Is Nothing Then
					_Appearance = New Appearance()
				End If
				Return _Appearance
			End Get
			Set(ByVal value As Appearance)
                If Object.Equals(_Appearance, value) Then
                    _Appearance = value
                    OnPropertyChanged("Appearance")
                End If
			End Set
		End Property

		Public Property Criteria() As CriteriaOperator
			Get
				Return _Criteria
			End Get
			Set(ByVal value As CriteriaOperator)
				If (Not Object.Equals(_Criteria, value)) Then
					_Criteria = value
					OnPropertyChanged("Criteria")
				End If
			End Set
		End Property

		Public Property FieldName() As String
			Get
				Return _FieldName
			End Get
			Set(ByVal value As String)
				If _FieldName <> value Then
					_FieldName = value
					OnPropertyChanged("FieldName")
				End If
			End Set
		End Property

		Public Overrides Function ToString() As String
			Return String.Format("Condition FieldName = {0}, Criteria = {1}", FieldName, Criteria)
		End Function

		Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

		Private Sub OnPropertyChanged(ByVal propertyName As String)
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End Sub
	End Class
End Namespace
