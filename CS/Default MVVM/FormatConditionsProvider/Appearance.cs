using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows;
using System.ComponentModel;

namespace Default_MVVM
{
    public class Appearance : INotifyPropertyChanged
    {
        // Fields...
        private FontStyle? _FontStyle;
        private double _FontSize;
        private FontFamily _FontFamily;
        private Brush _Foreground;
        private Brush _Background;

        public Brush Background
        {
            get { return _Background; }
            set {
                if (_Background != value)
                {
                    _Background = value;
                    OnPropertyChanged("Background");
                }
            }
        }


        public Brush Foreground
        {
            get { return _Foreground; }
            set {
                if (_Foreground != value)
                {
                    _Foreground = value;
                    OnPropertyChanged("Foreground");
                }
            }
        }


        public FontFamily FontFamily
        {
            get { return _FontFamily; }
            set {
                if (_FontFamily != value)
                {
                    _FontFamily = value;
                    OnPropertyChanged("FontFamily");
                }
            }
        }


        public double FontSize
        {
            get { return _FontSize; }
            set {
                if (_FontSize != value)
                {
                    _FontSize = value;
                    OnPropertyChanged("FontSize");
                }
            }
        }


        public FontStyle? FontStyle
        {
            get { return _FontStyle; }
            set {
                if (_FontStyle != value)
                {
                    _FontStyle = value;
                    OnPropertyChanged("FontStyle");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName){
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
