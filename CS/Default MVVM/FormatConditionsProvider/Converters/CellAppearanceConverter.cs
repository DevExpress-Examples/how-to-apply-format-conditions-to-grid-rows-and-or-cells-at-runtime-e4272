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

namespace Default_MVVM
{
    public class CellAppearanceConverter : MarkupExtension, IMultiValueConverter
    {
        // Fields...
        private Appearance _DefaultAppearance;

        public CellAppearanceConverter()
        {

        }

        public Appearance DefaultAppearance
        {
            get { return _DefaultAppearance; }
            set { _DefaultAppearance = value; }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            GridColumn gridColumn = values[4] as GridColumn;
            GridCellContentPresenter presenter = values[3] as GridCellContentPresenter;
            EditGridCellData data = presenter.DataContext as EditGridCellData; 
            TableView view = data.View as TableView; 
            if (DefaultAppearance == null && !presenter.IsFocusedCell)
                CreateDefaultAppearance(presenter);
            Appearance appearance = GetDefaultAppearance();
            bool isFormatconditionChanged = (bool)values[1] || (bool)values[2];
            if (!isFormatconditionChanged)
                return appearance;
            object row = values[0];
            ObservableCollection<FormatCondition> conditions = FormatConditionsProvider.GetFormatConditions(view);
            PropertyDescriptorCollection descriptors = TypeDescriptor.GetProperties(row);
            foreach (FormatCondition condition in conditions)
                if (CanApplyFormatCondition(presenter, condition, row, descriptors, gridColumn))
                    Assign(appearance, condition.Appearance);
            return appearance;
        }

        private void CreateDefaultAppearance(GridCellContentPresenter presenter)
        {
            DefaultAppearance = new Appearance();
            Assign(DefaultAppearance, presenter);
        }

        private Appearance GetDefaultAppearance()
        {
            Appearance appearance = new Appearance();
            Assign(appearance, DefaultAppearance);
            return appearance;
        }

        private void Assign(Appearance targetAppearance, GridCellContentPresenter sourcePresenter)
        {
            targetAppearance.Background = sourcePresenter.Background;
            targetAppearance.Foreground = sourcePresenter.Foreground;
            targetAppearance.FontFamily = sourcePresenter.FontFamily;
            targetAppearance.FontStyle = sourcePresenter.FontStyle;
            targetAppearance.FontSize = sourcePresenter.FontSize;
        }

        private bool CanApplyFormatCondition(GridCellContentPresenter presenter, FormatCondition condition, object row, PropertyDescriptorCollection descriptors, GridColumn column)
        {
            if(column == null) return false;
            ExpressionEvaluator evaluator = new ExpressionEvaluator(descriptors, condition.Criteria);
            return ((string.IsNullOrEmpty(condition.FieldName) || presenter.Column.FieldName.Equals(condition.FieldName)))
                && !object.Equals(condition.Criteria, null) && evaluator.Fit(row);
        }

        private void Assign(Appearance targetAppearance, Appearance sourceAppearance)
        {
            if (sourceAppearance.Background != null)
                targetAppearance.Background = sourceAppearance.Background;
            if (sourceAppearance.Foreground != null)
                targetAppearance.Foreground = sourceAppearance.Foreground;
            if (sourceAppearance.FontStyle != null)
                targetAppearance.FontStyle = sourceAppearance.FontStyle;
            if (sourceAppearance.FontSize != 0)
                targetAppearance.FontSize = sourceAppearance.FontSize;
            if (sourceAppearance.FontFamily != null)
                targetAppearance.FontFamily = sourceAppearance.FontFamily;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}