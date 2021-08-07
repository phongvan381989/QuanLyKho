using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyKho.View
{
    /// <summary>
    /// Interaction logic for UserControlListBoxSearch.xaml
    /// </summary>
    public partial class UserControlListBoxSearch : UserControl
    {
        public UserControlListBoxSearch()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ListBoxSearchItemSourceProperty = DependencyProperty.Register("ListBoxSearchItemSource", typeof(IEnumerable), typeof(UserControlListBoxSearch), null);
        public IEnumerable ListBoxSearchItemSource
        {
            get { return (IEnumerable)GetValue(ListBoxSearchItemSourceProperty); }
            set { SetValue(ListBoxSearchItemSourceProperty, value); }
        }

        public static readonly DependencyProperty ListBoxSearchTextProperty = DependencyProperty.Register("ListBoxSearchText", typeof(String), typeof(UserControlListBoxSearch), null);
        public String ListBoxSearchText
        {
            get { return (String)GetValue(ListBoxSearchTextProperty); }
            set { SetValue(ListBoxSearchTextProperty, value); }
        }
    }
}
