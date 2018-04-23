Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Editors.ExpressionEditor
Imports DevExpress.Xpf.Editors.Filtering
Imports System.Collections.ObjectModel
Imports DevExpress.Data.Filtering.Helpers
Imports System.ComponentModel
Imports DevExpress.Xpf.Core

Namespace Default_MVVM
	''' <summary>
	''' Interaction logic for FormatConditionsWindow.xaml
	''' </summary>
	Partial Public Class FormatConditionsWindow
		Inherits DXWindow
		Implements INotifyPropertyChanged
		Private _SelectedFormatCondition As FormatCondition
		Private _View As TableView
		Public Sub New(ByVal view As TableView)
			InitializeComponent()
			_View = view
			DataContext = Me
			FormatConditionsProvider.SetIsFormatConditionChanged(Me.View, False)
			If FormatConditions.Count > 0 Then
				SelectedFormatCondition = FormatConditions(0)
			End If
		End Sub

		Public ReadOnly Property FormatConditions() As ObservableCollection(Of FormatCondition)
			Get
				Return FormatConditionsProvider.GetFormatConditions(View)
			End Get
		End Property

		Public Property SelectedFormatCondition() As FormatCondition
			Get
				Return _SelectedFormatCondition
			End Get
			Set(ByVal value As FormatCondition)
				If _SelectedFormatCondition IsNot value Then
					_SelectedFormatCondition = value
					OnPropertyChanged("SelectedFormatCondition")
				End If
			End Set
		End Property

		Public ReadOnly Property View() As TableView
			Get
				Return _View
			End Get
		End Property

		Private Sub btnAdd_ItemClick(ByVal sender As Object, ByVal e As DevExpress.Xpf.Bars.ItemClickEventArgs)
			Dim formatCondition As New FormatCondition()
			FormatConditions.Add(formatCondition)
			SelectedFormatCondition = formatCondition
		End Sub

		Private Sub btnRemove_ItemClick(ByVal sender As Object, ByVal e As DevExpress.Xpf.Bars.ItemClickEventArgs)
			FormatConditions.Remove(SelectedFormatCondition)
			If FormatConditions.Count > 0 Then
				SelectedFormatCondition = FormatConditions(FormatConditions.Count - 1)
			End If
		End Sub

		Private Sub btnClearAll_ItemClick(ByVal sender As Object, ByVal e As DevExpress.Xpf.Bars.ItemClickEventArgs)
			FormatConditions.Clear()
		End Sub

		Protected Overrides Sub OnClosing(ByVal e As System.ComponentModel.CancelEventArgs)
			FormatConditionsProvider.SetIsFormatConditionChanged(View, True)
			MyBase.OnClosing(e)
		End Sub

		Private Sub filterControl_LostFocus(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Dim filterControl As FilterControl = TryCast(sender, FilterControl)
			filterControl.ApplyFilter()
		End Sub

		Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

		Private Sub OnPropertyChanged(ByVal propertyName As String)
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End Sub
	End Class
End Namespace
