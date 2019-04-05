<!-- default file list -->
*Files to look at*:

* [FormatConditionsForm.xaml](./CS/Default MVVM/FormatConditionsForm.xaml) (VB: [FormatConditionsForm.xaml](./VB/Default MVVM/FormatConditionsForm.xaml))
* [FormatConditionsForm.xaml.cs](./CS/Default MVVM/FormatConditionsForm.xaml.cs) (VB: [FormatConditionsForm.xaml.vb](./VB/Default MVVM/FormatConditionsForm.xaml.vb))
* [Appearance.cs](./CS/Default MVVM/FormatConditionsProvider/Appearance.cs) (VB: [Appearance.vb](./VB/Default MVVM/FormatConditionsProvider/Appearance.vb))
* [ConditionalCellStyle.xaml](./CS/Default MVVM/FormatConditionsProvider/ConditionalCellStyle.xaml) (VB: [ConditionalCellStyle.xaml](./VB/Default MVVM/FormatConditionsProvider/ConditionalCellStyle.xaml))
* [CellAppearanceConverter.cs](./CS/Default MVVM/FormatConditionsProvider/Converters/CellAppearanceConverter.cs) (VB: [CellAppearanceConverter.vb](./VB/Default MVVM/FormatConditionsProvider/Converters/CellAppearanceConverter.vb))
* [ColorToBrushConverter.cs](./CS/Default MVVM/FormatConditionsProvider/Converters/ColorToBrushConverter.cs) (VB: [ColorToBrushConverter.vb](./VB/Default MVVM/FormatConditionsProvider/Converters/ColorToBrushConverter.vb))
* [FormatCondition.cs](./CS/Default MVVM/FormatConditionsProvider/FormatCondition.cs) (VB: [FormatConditionsWindow.xaml.vb](./VB/Default MVVM/FormatConditionsProvider/FormatConditionsWindow.xaml.vb))
* [FormatConditionsProvider.cs](./CS/Default MVVM/FormatConditionsProvider/FormatConditionsProvider.cs) (VB: [FormatConditionsProvider.vb](./VB/Default MVVM/FormatConditionsProvider/FormatConditionsProvider.vb))
* [FormatConditionsWindow.xaml](./CS/Default MVVM/FormatConditionsProvider/FormatConditionsWindow.xaml) (VB: [FormatConditionsWindow.xaml](./VB/Default MVVM/FormatConditionsProvider/FormatConditionsWindow.xaml))
* [FormatConditionsWindow.xaml.cs](./CS/Default MVVM/FormatConditionsProvider/FormatConditionsWindow.xaml.cs) (VB: [FormatConditionsWindow.xaml.vb](./VB/Default MVVM/FormatConditionsProvider/FormatConditionsWindow.xaml.vb))
* [MainWindow.xaml](./CS/Default MVVM/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/Default MVVM/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/Default MVVM/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/Default MVVM/MainWindow.xaml.vb))
* [ViewModel.cs](./CS/Default MVVM/ViewModel.cs) (VB: [ViewModel.vb](./VB/Default MVVM/ViewModel.vb))
<!-- default file list end -->
# How to: Apply format conditions to grid rows and/or cells at runtime


<p>This example illustrates how to apply format conditions to grid rows and/or cells at runtime.</p>


<h3>Description</h3>

<p>To enable this feature, you need to set the attached <strong>FormatConditionsProvider.AllowFormatConditions</strong> property to true for TableView. In this case, a new "<strong>Show Format Conditions</strong>" item will appear in the <strong>Column Context Menu</strong>. Simply right-click any column header to invoke this menu and choose the "Show Format Conditions" item. As a result, <strong>FormatConditionsWindow</strong> will be shown. This dialog allows you to create <u>multiple Style Format Conditions</u>. To create a new one, click the Add button. On the right, choose any column from a drop-down window if you wish to apply your custom style to cells of a particular column. If you wish to apply the style to a whole row, leave this option empty. The next step is to set the <strong>Background</strong>, and/or <strong>Foreground</strong>, and/or <strong>Font</strong> settings. Finally, set your custom condition by using the <strong>Criteria</strong> section where you will find FilterControl. All the changes on the right side will be reflected in a corresponding item on the left.</p>
<p><img data-image="a94d39cc-d16b-4748-8e6d-04efa483db41"></p>

<br/>


