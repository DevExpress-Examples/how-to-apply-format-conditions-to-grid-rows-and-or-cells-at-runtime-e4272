using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Markup;
using DevExpress.Xpf.Grid;
using System.Collections.ObjectModel;
using Default_MVVM;
using System.ComponentModel;
using DevExpress.Data.Filtering.Helpers;
using System.Windows.Controls;

namespace Default_MVVM {
    public class CellAppearanceConverter : MarkupExtension, IMultiValueConverter {
        public Appearance DefaultAppearance { get; private set; }
        public CellAppearanceConverter() { }
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            GridColumn column = values[4] as GridColumn;
            CellContentPresenter cellPresenter = values[3] as CellContentPresenter;
            EditGridCellData data = cellPresenter.DataContext as EditGridCellData;
            TableView view = data.View as TableView;
            if(DefaultAppearance == null)
                CreateDefaultAppearance(cellPresenter);
            Appearance appearance = GetDefaultAppearance();
            bool isFormatconditionChanged = (bool)values[1] || (bool)values[2];
            if(isFormatconditionChanged) {
                object row = values[0];
                ObservableCollection<FormatCondition> conditions = FormatConditionsProvider.GetFormatConditions(view);
                PropertyDescriptorCollection descriptors = TypeDescriptor.GetProperties(row);
                foreach(FormatCondition condition in conditions) {
                    if(column != null && CanApplyFormatCondition(cellPresenter, condition, row, descriptors)) {
                        Assign(appearance, condition.Appearance);
                    }
                }
            }
            return appearance;
        }

        void CreateDefaultAppearance(CellContentPresenter presenter) {
            DefaultAppearance = new Appearance();
            Assign(DefaultAppearance, presenter);
        }

        Appearance GetDefaultAppearance() {
            Appearance appearance = new Appearance();
            Assign(appearance, DefaultAppearance);
            return appearance;
        }

        void Assign(Appearance targetAppearance, CellContentPresenter sourcePresenter) {
            targetAppearance.Background = sourcePresenter.Background;
            targetAppearance.Foreground = sourcePresenter.Foreground;
            targetAppearance.FontFamily = sourcePresenter.FontFamily;
            targetAppearance.FontStyle = TextBlock.GetFontStyle(sourcePresenter);
            targetAppearance.FontSize = sourcePresenter.FontSize;
        }

        bool CanApplyFormatCondition(CellContentPresenter presenter, FormatCondition condition, object row, PropertyDescriptorCollection descriptors) {
            ExpressionEvaluator evaluator = new ExpressionEvaluator(descriptors, condition.Criteria);
            return ((string.IsNullOrEmpty(condition.FieldName) || presenter.Column.FieldName.Equals(condition.FieldName)))
                && !object.Equals(condition.Criteria, null) && evaluator.Fit(row);
        }

        void Assign(Appearance targetAppearance, Appearance sourceAppearance) {
            if(sourceAppearance.Background != null)
                targetAppearance.Background = sourceAppearance.Background;
            if(sourceAppearance.Foreground != null)
                targetAppearance.Foreground = sourceAppearance.Foreground;
            if(sourceAppearance.FontStyle != null)
                targetAppearance.FontStyle = sourceAppearance.FontStyle;
            if(sourceAppearance.FontSize != 0)
                targetAppearance.FontSize = sourceAppearance.FontSize;
            if(sourceAppearance.FontFamily != null)
                targetAppearance.FontFamily = sourceAppearance.FontFamily;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}