Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Media
Imports System.Windows
Imports System.ComponentModel

Namespace Default_MVVM
	Public Class Appearance
		Implements INotifyPropertyChanged
		' Fields...
		Private _FontStyle? As FontStyle
		Private _FontSize As Double
		Private _FontFamily As FontFamily
		Private _Foreground As Brush
		Private _Background As Brush

		Public Property Background() As Brush
			Get
				Return _Background
			End Get
			Set(ByVal value As Brush)
				If _Background IsNot value Then
					_Background = value
					OnPropertyChanged("Background")
				End If
			End Set
		End Property


		Public Property Foreground() As Brush
			Get
				Return _Foreground
			End Get
			Set(ByVal value As Brush)
				If _Foreground IsNot value Then
					_Foreground = value
					OnPropertyChanged("Foreground")
				End If
			End Set
		End Property


		Public Property FontFamily() As FontFamily
			Get
				Return _FontFamily
			End Get
			Set(ByVal value As FontFamily)
				If _FontFamily IsNot value Then
					_FontFamily = value
					OnPropertyChanged("FontFamily")
				End If
			End Set
		End Property


		Public Property FontSize() As Double
			Get
				Return _FontSize
			End Get
			Set(ByVal value As Double)
				If _FontSize <> value Then
					_FontSize = value
					OnPropertyChanged("FontSize")
				End If
			End Set
		End Property


		Public Property FontStyle() As FontStyle?
			Get
				Return _FontStyle
			End Get
			Set(ByVal value? As FontStyle)
				If Not _FontStyle.Equals(value) Then
					_FontStyle = value
					OnPropertyChanged("FontStyle")
				End If
			End Set
		End Property

		Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
		Private Sub OnPropertyChanged(ByVal propertyName As String)
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End Sub
	End Class
End Namespace
