using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Editors.ExpressionEditor;
using DevExpress.Xpf.Editors.Filtering;
using System.Collections.ObjectModel;
using DevExpress.Data.Filtering.Helpers;
using System.ComponentModel;
using DevExpress.Xpf.Core;

namespace Default_MVVM
{
    /// <summary>
    /// Interaction logic for FormatConditionsWindow.xaml
    /// </summary>
    public partial class FormatConditionsWindow : DXWindow, INotifyPropertyChanged
    {
        private FormatCondition _SelectedFormatCondition;
        private TableView _View;
        public FormatConditionsWindow(TableView view)
        {
            InitializeComponent();
            _View = view;
            DataContext = this;
            FormatConditionsProvider.SetIsFormatConditionChanged(View, false);
            if (FormatConditions.Count > 0)
                SelectedFormatCondition = FormatConditions[0];
        }

        public ObservableCollection<FormatCondition> FormatConditions
        {
            get { return FormatConditionsProvider.GetFormatConditions(View); }
        }

        public FormatCondition SelectedFormatCondition
        {
            get { return _SelectedFormatCondition; }
            set {
                if (_SelectedFormatCondition != value)
                {
                    _SelectedFormatCondition = value;
                    OnPropertyChanged("SelectedFormatCondition");
                }
            }
        }

        public TableView View
        {
            get { return _View; }
        }

        private void btnAdd_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            FormatCondition formatCondition = new FormatCondition();
            FormatConditions.Add(formatCondition);
            SelectedFormatCondition = formatCondition;
        }

        private void btnRemove_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            FormatConditions.Remove(SelectedFormatCondition);
            if (FormatConditions.Count > 0)
                SelectedFormatCondition = FormatConditions[FormatConditions.Count - 1];
        }

        private void btnClearAll_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            FormatConditions.Clear();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            FormatConditionsProvider.SetIsFormatConditionChanged(View, true);
            base.OnClosing(e);
        }

        private void filterControl_LostFocus(object sender, RoutedEventArgs e)
        {
            FilterControl filterControl = sender as FilterControl;
            filterControl.ApplyFilter();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
