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
		Private selectedFormatCondition_Renamed As FormatCondition

		Public Sub New(ByVal gridView As TableView)
			InitializeComponent()

			View = gridView
			Appearance.DefaultFontFamily = TextBlock.GetFontFamily(gridView)
			FormatConditionsProvider.SetIsFormatConditionChanged(View, False)
			SelectedFormatCondition = FormatConditions.FirstOrDefault()

			DataContext = Me
		End Sub

		Private privateView As TableView
		Public Property View() As TableView
			Get
				Return privateView
			End Get
			Private Set(ByVal value As TableView)
				privateView = value
			End Set
		End Property

		Public ReadOnly Property FormatConditions() As ObservableCollection(Of FormatCondition)
			Get
				Return FormatConditionsProvider.GetFormatConditions(View)
			End Get
		End Property

		Public Property SelectedFormatCondition() As FormatCondition
			Get
				Return selectedFormatCondition_Renamed
			End Get
			Set(ByVal value As FormatCondition)
				If selectedFormatCondition_Renamed IsNot value Then
					selectedFormatCondition_Renamed = value
					OnPropertyChanged("SelectedFormatCondition")
				End If
			End Set
		End Property

		Private Sub OnAddItemClick(ByVal sender As Object, ByVal e As DevExpress.Xpf.Bars.ItemClickEventArgs)
			Dim formatCondition As New FormatCondition()
			FormatConditions.Add(formatCondition)
			SelectedFormatCondition = formatCondition
		End Sub

		Private Sub OnRemoveItemClick(ByVal sender As Object, ByVal e As DevExpress.Xpf.Bars.ItemClickEventArgs)
			FormatConditions.Remove(SelectedFormatCondition)
			If FormatConditions.Count > 0 Then
				SelectedFormatCondition = FormatConditions(FormatConditions.Count - 1)
			End If
		End Sub

		Private Sub OnClearAllItemClick(ByVal sender As Object, ByVal e As DevExpress.Xpf.Bars.ItemClickEventArgs)
			FormatConditions.Clear()
		End Sub

		Private Sub OnFilterControlLostFocus(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Dim filterControl As FilterControl = TryCast(sender, FilterControl)
			filterControl.ApplyFilter()
		End Sub

		Protected Overrides Sub OnClosing(ByVal e As System.ComponentModel.CancelEventArgs)
			FormatConditionsProvider.SetIsFormatConditionChanged(View, True)
			MyBase.OnClosing(e)
		End Sub

		Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

		Private Sub OnPropertyChanged(ByVal propertyName As String)
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End Sub
	End Class
End Namespace
