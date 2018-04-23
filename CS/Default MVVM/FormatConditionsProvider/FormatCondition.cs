using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using DevExpress.Data.Filtering;
using System.ComponentModel;

namespace Default_MVVM
{
    public class FormatCondition : INotifyPropertyChanged
    {

        // Fields...
        private string _FieldName;
        private CriteriaOperator _Criteria;
        private Appearance _Appearance;

        public Appearance Appearance
        {
            get
            {
                if (_Appearance == null)
                    _Appearance = new Appearance();
                return _Appearance;
            }
            set
            {
                if (_Appearance != value)
                {
                    _Appearance = value;
                    OnPropertyChanged("Appearance");
                }
            }
        }

        public CriteriaOperator Criteria
        {
            get { return _Criteria; }
            set
            {
                if (!object.Equals(_Criteria, value))
                {
                    _Criteria = value;
                    OnPropertyChanged("Criteria");
                }
            }
        }

        public string FieldName
        {
            get { return _FieldName; }
            set
            {
                if (_FieldName != value)
                {
                    _FieldName = value;
                    OnPropertyChanged("FieldName");
                }
            }
        }

        public override string ToString()
        {
            return string.Format("Condition FieldName = {0}, Criteria = {1}", FieldName, Criteria);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
