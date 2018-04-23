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

namespace Default_MVVM {
    /// <summary>
    /// Interaction logic for FormatConditionsWindow.xaml
    /// </summary>
    public partial class FormatConditionsWindow : DXWindow, INotifyPropertyChanged {
        FormatCondition selectedFormatCondition;

        public FormatConditionsWindow(TableView gridView) {
            InitializeComponent();
            
            View = gridView;
            Appearance.DefaultFontFamily = TextBlock.GetFontFamily(gridView);
            FormatConditionsProvider.SetIsFormatConditionChanged(View, false);
            SelectedFormatCondition = FormatConditions.FirstOrDefault();

            DataContext = this;
        }

        public TableView View { get; private set; }

        public ObservableCollection<FormatCondition> FormatConditions {
            get { return FormatConditionsProvider.GetFormatConditions(View); }
        }

        public FormatCondition SelectedFormatCondition {
            get { return selectedFormatCondition; }
            set {
                if(selectedFormatCondition != value) {
                    selectedFormatCondition = value;
                    OnPropertyChanged("SelectedFormatCondition");
                }
            }
        }

        void OnAddItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            FormatCondition formatCondition = new FormatCondition();
            FormatConditions.Add(formatCondition);
            SelectedFormatCondition = formatCondition;
        }

        void OnRemoveItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            FormatConditions.Remove(SelectedFormatCondition);
            if(FormatConditions.Count > 0)
                SelectedFormatCondition = FormatConditions[FormatConditions.Count - 1];
        }

        void OnClearAllItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e) {
            FormatConditions.Clear();
        }

        void OnFilterControlLostFocus(object sender, RoutedEventArgs e) {
            FilterControl filterControl = sender as FilterControl;
            filterControl.ApplyFilter();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e) {
            FormatConditionsProvider.SetIsFormatConditionChanged(View, true);
            base.OnClosing(e);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName) {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
