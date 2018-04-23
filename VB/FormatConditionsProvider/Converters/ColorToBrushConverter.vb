Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Data
Imports System.Windows.Media
Imports System.Windows.Markup

Namespace Default_MVVM
	Public Class ColorToBrushConverter
		Inherits MarkupExtension
		Implements IValueConverter
		Public Sub New()
		End Sub
		Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
			Dim brush As SolidColorBrush = TryCast(value, SolidColorBrush)
			If brush IsNot Nothing Then
				Return brush.Color
			End If
			Return Nothing
		End Function
		Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
			If TypeOf value Is Color Then
				Return New SolidColorBrush(CType(value, Color))
			End If
			Return Nothing
		End Function
		Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
			Return Me
		End Function
	End Class
End Namespace
