using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace QuanLyKho.ViewModel
{
    public class ViewModelListBoxSearch : ViewModelBase
    {
        public ViewModelListBoxSearch()
        {
            _commandCollapse = new CommandListBoxSearch_Collapse(this);
        }
        private Boolean pbCollapse;
        public Boolean bCollapse
        {
            get
            {
                return pbCollapse;
            }

            set
            {
                if (pbCollapse != value)
                {
                    pbCollapse = value;
                    OnPropertyChanged("bCollapse");
                }
            }
        }

        public void Collapse()
        {
            bCollapse = !bCollapse;
        }

        private CommandListBoxSearch_Collapse _commandCollapse;
        public CommandListBoxSearch_Collapse commandCollapse
        {
            get
            {
                return _commandCollapse;
            }
        }
        //public IValueConverter collapseConverter { get; set; }

    }

    [ValueConversion(typeof(Boolean), typeof(Visibility))]
    public class ColapseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Boolean bC = (Boolean)value;
            Visibility vs;
            if (bC)
                vs = Visibility.Collapsed;
            else
                vs = Visibility.Visible;
            return vs;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility vs = (Visibility)value;
            Boolean bC;
            if (vs == Visibility.Collapsed)
                bC = true;
            else
                bC = false;

            return bC;
        }
    }
}
