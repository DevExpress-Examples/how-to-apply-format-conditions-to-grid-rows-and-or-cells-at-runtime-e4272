Imports System
Imports System.Collections.ObjectModel

Namespace Default_MVVM
    Public Class TestDataViewModel
        Public Property Records() As ObservableCollection(Of TestObjectViewModel)
        Public Sub New()
            Records = New ObservableCollection(Of TestObjectViewModel)()
            For i As Integer = 0 To 199
                Records.Add(New TestObjectViewModel() With {.Text = "Row" & i, .Number = i, .Date = Date.Now.AddDays(i).AddHours(i)})
            Next i
        End Sub
    End Class

    Public Class TestObjectViewModel
        Public Property Text() As String
        Public Property Number() As Integer
        Public Property [Date]() As Date
    End Class
End Namespace
