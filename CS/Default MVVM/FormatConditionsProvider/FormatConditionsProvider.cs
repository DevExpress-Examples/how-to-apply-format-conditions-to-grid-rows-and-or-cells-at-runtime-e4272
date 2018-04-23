using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;
using DevExpress.Xpf.Grid;
using System.Collections.Specialized;
using DevExpress.Xpf.Bars;
using System.Windows.Controls;

namespace Default_MVVM
{
    public static class FormatConditionsProvider
    {

        public static readonly DependencyProperty FormatConditionsProperty = DependencyProperty.RegisterAttached("FormatConditions",
                                                                                 typeof(ObservableCollection<FormatCondition>),
                                                                                 typeof(FormatConditionsProvider),
                                                                                 new UIPropertyMetadata(new ObservableCollection<FormatCondition>()));

        public static readonly DependencyProperty IsFormatConditionChangedProperty = DependencyProperty.RegisterAttached("IsFormatConditionChanged",
                                                                                 typeof(bool),
                                                                                 typeof(FormatConditionsProvider),
                                                                                 new UIPropertyMetadata(false));

        public static readonly DependencyProperty CellAppearanceProperty = DependencyProperty.RegisterAttached("CellAppearance",
                                                                             typeof(Appearance),
                                                                             typeof(FormatConditionsProvider),
                                                                             new UIPropertyMetadata(new Appearance()));

        public static readonly DependencyProperty AllowFormatConditionsProperty = DependencyProperty.RegisterAttached("AllowFormatConditions",
                                                                             typeof(bool),
                                                                             typeof(FormatConditionsProvider),
                                                                             new UIPropertyMetadata(false, new PropertyChangedCallback(OnAllowFormatConditionsChanged)));


        public static void SetFormatConditions(TableView element, ObservableCollection<FormatCondition> value)
        {
            element.SetValue(FormatConditionsProperty, value);
        }

        public static ObservableCollection<FormatCondition> GetFormatConditions(TableView element)
        {
            return (ObservableCollection<FormatCondition>)element.GetValue(FormatConditionsProperty);
        }

        public static void SetIsFormatConditionChanged(DependencyObject element, bool value)
        {
            element.SetValue(IsFormatConditionChangedProperty, value);
        }

        public static bool GetIsFormatConditionChanged(DependencyObject element)
        {
            return (bool)element.GetValue(IsFormatConditionChangedProperty);
        }

        public static void SetCellAppearance(CellContentPresenter element, Appearance value)
        {
            element.SetValue(CellAppearanceProperty, value);
        }

        public static Appearance GetCellAppearance(CellContentPresenter element)
        {
            return (Appearance)element.GetValue(CellAppearanceProperty);
        }

        public static void SetAllowFormatConditions(TableView element, bool value)
        {
            element.SetValue(AllowFormatConditionsProperty, value);
        }

        public static bool GetAllowFormatConditions(TableView element)
        {
            return (bool)element.GetValue(AllowFormatConditionsProperty);
        }

        private static Style ConditionCellStyle
        {
            get
            {
                return Application.Current.Resources["ConditionCellStyle"] as Style;
            }
        }
        public static void OnAllowFormatConditionsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            TableView view = obj as TableView;
            view.ShowGridMenu -= OnShowGridMenu;
            view.CellValueChanged -= OnFormatConditionChanged;
            view.CellStyle = null;
            if ((bool)e.NewValue)
            {
                view.CellStyle = ConditionCellStyle;
                view.ShowGridMenu += OnShowGridMenu;
                view.CellValueChanged += OnFormatConditionChanged;
            }
        }

        static void OnFormatConditionChanged(object sender, RoutedEventArgs e)
        {
            TableView view = sender as TableView;
            CellContentPresenter presenter = view.GetCellElementByRowHandleAndColumn(view.FocusedRowHandle, view.FocusedColumn) as CellContentPresenter;
            RowData rowData = presenter.RowData;
            FormatConditionsProvider.SetIsFormatConditionChanged(rowData, false);
            FormatConditionsProvider.SetIsFormatConditionChanged(rowData, true);
        }

        static void OnShowGridMenu(object sender, GridMenuEventArgs e)
        {
            TableView view = sender as TableView;
            if (e.MenuType.HasValue && e.MenuType == GridMenuType.Column)
            {
                BarButtonItem item = new BarButtonItem();
                item.Content = "Show Format Conditions";
                item.ItemClick += OnItemClick;
                item.Tag = view;
                e.MenuInfo.Menu.ItemLinks.Add(item);
            }
        }

        static void OnItemClick(object sender, ItemClickEventArgs e)
        {
            TableView view = e.Item.Tag as TableView;
            FormatConditionsWindow window = new FormatConditionsWindow(view);
            window.Owner = Application.Current.MainWindow;
            window.ShowDialog();
        }
    }
}
