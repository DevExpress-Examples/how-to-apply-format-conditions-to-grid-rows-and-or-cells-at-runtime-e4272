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
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DXWpfApplication
Imports System.Collections.ObjectModel
Imports DevExpress.Xpf.Grid
Imports System.Windows.Markup
Imports DevExpress.Xpf.Core

Namespace Default_MVVM
	''' <summary>
	''' Interaction logic for MainWindow.xaml
	''' </summary>
	Partial Public Class MainWindow
		Inherits DXWindow
		Private ViewModel As TestDataViewsModel
		Public Sub New()
			InitializeComponent()
			ViewModel = New TestDataViewsModel()
			DataContext = ViewModel
		End Sub
		Private Sub GridControl_CustomUnboundColumnData(ByVal sender As Object, ByVal e As GridColumnDataEventArgs)
			If e.IsGetData Then
				e.Value = ViewModel.Records(e.ListSourceRowIndex).Number
			End If
		End Sub
	End Class
End Namespace