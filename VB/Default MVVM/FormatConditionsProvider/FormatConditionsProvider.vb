Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Collections.ObjectModel
Imports DevExpress.Xpf.Grid
Imports System.Collections.Specialized
Imports DevExpress.Xpf.Bars
Imports System.Windows.Controls

Namespace Default_MVVM
	Public NotInheritable Class FormatConditionsProvider

		Public Shared ReadOnly FormatConditionsProperty As DependencyProperty = DependencyProperty.RegisterAttached("FormatConditions", GetType(ObservableCollection(Of FormatCondition)), GetType(FormatConditionsProvider), New UIPropertyMetadata(New ObservableCollection(Of FormatCondition)()))

		Public Shared ReadOnly IsFormatConditionChangedProperty As DependencyProperty = DependencyProperty.RegisterAttached("IsFormatConditionChanged", GetType(Boolean), GetType(FormatConditionsProvider), New UIPropertyMetadata(False))

		Public Shared ReadOnly CellAppearanceProperty As DependencyProperty = DependencyProperty.RegisterAttached("CellAppearance", GetType(Appearance), GetType(FormatConditionsProvider), New UIPropertyMetadata(New Appearance()))

		Public Shared ReadOnly AllowFormatConditionsProperty As DependencyProperty = DependencyProperty.RegisterAttached("AllowFormatConditions", GetType(Boolean), GetType(FormatConditionsProvider), New UIPropertyMetadata(False, New PropertyChangedCallback(AddressOf OnAllowFormatConditionsChanged)))


		Private Sub New()
		End Sub
		Public Shared Sub SetFormatConditions(ByVal element As TableView, ByVal value As ObservableCollection(Of FormatCondition))
			element.SetValue(FormatConditionsProperty, value)
		End Sub

		Public Shared Function GetFormatConditions(ByVal element As TableView) As ObservableCollection(Of FormatCondition)
			Return CType(element.GetValue(FormatConditionsProperty), ObservableCollection(Of FormatCondition))
		End Function

		Public Shared Sub SetIsFormatConditionChanged(ByVal element As DependencyObject, ByVal value As Boolean)
			element.SetValue(IsFormatConditionChangedProperty, value)
		End Sub

		Public Shared Function GetIsFormatConditionChanged(ByVal element As DependencyObject) As Boolean
			Return CBool(element.GetValue(IsFormatConditionChangedProperty))
		End Function

		Public Shared Sub SetCellAppearance(ByVal element As CellContentPresenter, ByVal value As Appearance)
			element.SetValue(CellAppearanceProperty, value)
		End Sub

		Public Shared Function GetCellAppearance(ByVal element As CellContentPresenter) As Appearance
			Return CType(element.GetValue(CellAppearanceProperty), Appearance)
		End Function

		Public Shared Sub SetAllowFormatConditions(ByVal element As TableView, ByVal value As Boolean)
			element.SetValue(AllowFormatConditionsProperty, value)
		End Sub

		Public Shared Function GetAllowFormatConditions(ByVal element As TableView) As Boolean
			Return CBool(element.GetValue(AllowFormatConditionsProperty))
		End Function

		Private Shared ReadOnly Property ConditionCellStyle() As Style
			Get
				Return TryCast(Application.Current.Resources("ConditionCellStyle"), Style)
			End Get
		End Property
		Public Shared Sub OnAllowFormatConditionsChanged(ByVal obj As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
			Dim view As TableView = TryCast(obj, TableView)
			RemoveHandler view.ShowGridMenu, AddressOf OnShowGridMenu
			RemoveHandler view.CellValueChanged, AddressOf OnFormatConditionChanged
			view.CellStyle = Nothing
			If CBool(e.NewValue) Then
				view.CellStyle = ConditionCellStyle
				AddHandler view.ShowGridMenu, AddressOf OnShowGridMenu
				AddHandler view.CellValueChanged, AddressOf OnFormatConditionChanged
			End If
		End Sub

		Private Shared Sub OnFormatConditionChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Dim view As TableView = TryCast(sender, TableView)
			Dim presenter As CellContentPresenter = TryCast(view.GetCellElementByRowHandleAndColumn(view.FocusedRowHandle, view.FocusedColumn), CellContentPresenter)
			Dim rowData As RowData = presenter.RowData
			FormatConditionsProvider.SetIsFormatConditionChanged(rowData, False)
			FormatConditionsProvider.SetIsFormatConditionChanged(rowData, True)
		End Sub

		Private Shared Sub OnShowGridMenu(ByVal sender As Object, ByVal e As GridMenuEventArgs)
			Dim view As TableView = TryCast(sender, TableView)
			If e.MenuType.HasValue AndAlso e.MenuType = GridMenuType.Column Then
				Dim item As New BarButtonItem()
				item.Content = "Show Format Conditions"
				AddHandler item.ItemClick, AddressOf OnItemClick
				item.Tag = view
				e.MenuInfo.Menu.ItemLinks.Add(item)
			End If
		End Sub

		Private Shared Sub OnItemClick(ByVal sender As Object, ByVal e As ItemClickEventArgs)
			Dim view As TableView = TryCast(e.Item.Tag, TableView)
			Dim window As New FormatConditionsWindow(view)
			window.Owner = Application.Current.MainWindow
			window.ShowDialog()
		End Sub
	End Class
End Namespace
