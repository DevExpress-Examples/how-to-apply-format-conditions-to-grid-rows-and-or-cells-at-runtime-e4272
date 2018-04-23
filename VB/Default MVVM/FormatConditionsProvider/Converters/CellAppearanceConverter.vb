Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Markup
Imports DevExpress.Xpf.Grid
Imports System.Collections.ObjectModel
Imports Default_MVVM
Imports System.ComponentModel
Imports DevExpress.Data.Filtering.Helpers

Namespace Default_MVVM
	Public Class CellAppearanceConverter
		Inherits MarkupExtension
		Implements IMultiValueConverter
		' Fields...
		Private _DefaultAppearance As Appearance

		Public Sub New()

		End Sub

		Public Property DefaultAppearance() As Appearance
			Get
				Return _DefaultAppearance
			End Get
			Set(ByVal value As Appearance)
				_DefaultAppearance = value
			End Set
		End Property

		Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
			Return Me
		End Function

		Public Function Convert(ByVal values() As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IMultiValueConverter.Convert
			Dim presenter As GridCellContentPresenter = TryCast(values(3), GridCellContentPresenter)
			Dim data As EditGridCellData = TryCast(presenter.DataContext, EditGridCellData)
			Dim view As TableView = TryCast(data.View, TableView)
			If DefaultAppearance Is Nothing AndAlso (Not presenter.IsFocusedCell) Then
				CreateDefaultAppearance(presenter)
			End If
			Dim appearance As Appearance = GetDefaultAppearance()
			Dim isFormatconditionChanged As Boolean = CBool(values(1)) OrElse CBool(values(2))
			If (Not isFormatconditionChanged) Then
				Return appearance
			End If
			Dim row As Object = values(0)
			Dim conditions As ObservableCollection(Of FormatCondition) = FormatConditionsProvider.GetFormatConditions(view)
			Dim descriptors As PropertyDescriptorCollection = TypeDescriptor.GetProperties(row)
			For Each condition As FormatCondition In conditions
				If CanApplyFormatCondition(presenter, condition, row, descriptors) Then
					Assign(appearance, condition.Appearance)
				End If
			Next condition
			Return appearance
		End Function

		Private Sub CreateDefaultAppearance(ByVal presenter As GridCellContentPresenter)
			DefaultAppearance = New Appearance()
			Assign(DefaultAppearance, presenter)
		End Sub

		Private Function GetDefaultAppearance() As Appearance
			Dim appearance As New Appearance()
			Assign(appearance, DefaultAppearance)
			Return appearance
		End Function

		Private Sub Assign(ByVal targetAppearance As Appearance, ByVal sourcePresenter As GridCellContentPresenter)
			targetAppearance.Background = sourcePresenter.Background
			targetAppearance.Foreground = sourcePresenter.Foreground
			targetAppearance.FontFamily = sourcePresenter.FontFamily
			targetAppearance.FontStyle = sourcePresenter.FontStyle
			targetAppearance.FontSize = sourcePresenter.FontSize
		End Sub

		Private Function CanApplyFormatCondition(ByVal presenter As GridCellContentPresenter, ByVal condition As FormatCondition, ByVal row As Object, ByVal descriptors As PropertyDescriptorCollection) As Boolean
			Dim evaluator As New ExpressionEvaluator(descriptors, condition.Criteria)
			Return ((String.IsNullOrEmpty(condition.FieldName) OrElse presenter.Column.FieldName.Equals(condition.FieldName))) AndAlso (Not Object.Equals(condition.Criteria, Nothing)) AndAlso evaluator.Fit(row)
		End Function

		Private Sub Assign(ByVal targetAppearance As Appearance, ByVal sourceAppearance As Appearance)
			If sourceAppearance.Background IsNot Nothing Then
				targetAppearance.Background = sourceAppearance.Background
			End If
			If sourceAppearance.Foreground IsNot Nothing Then
				targetAppearance.Foreground = sourceAppearance.Foreground
			End If
			If sourceAppearance.FontStyle IsNot Nothing Then
				targetAppearance.FontStyle = sourceAppearance.FontStyle
			End If
			If sourceAppearance.FontSize <> 0 Then
				targetAppearance.FontSize = sourceAppearance.FontSize
			End If
			If sourceAppearance.FontFamily IsNot Nothing Then
				targetAppearance.FontFamily = sourceAppearance.FontFamily
			End If
		End Sub

		Public Function ConvertBack(ByVal value As Object, ByVal targetTypes() As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace