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
            ListBoxSearchPopupIsOpen = false;
            ListBoxSearchCheckSelectedItem = false;
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
        
        public static readonly DependencyProperty ListBoxSearchPopupIsOpenProperty = DependencyProperty.Register("ListBoxSearchPopupIsOpen", typeof(Boolean), typeof(UserControlListBoxSearch), null);
        public Boolean ListBoxSearchPopupIsOpen
        {
            get { return (Boolean)GetValue(ListBoxSearchPopupIsOpenProperty); }
            set { SetValue(ListBoxSearchPopupIsOpenProperty, value); }
        }

        // Khi item của listbox được chọn bằng enter, double chuột biến này có giá trị true.
        public static readonly DependencyProperty ListBoxSearchCheckSelectedItemProperty = DependencyProperty.Register("ListBoxSearchCheckSelectedItem", typeof(Boolean), typeof(UserControlListBoxSearch), null);
        public Boolean ListBoxSearchCheckSelectedItem
        {
            get { return (Boolean)GetValue(ListBoxSearchCheckSelectedItemProperty); }
            set { SetValue(ListBoxSearchCheckSelectedItemProperty, value); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListBoxSearchPopupIsOpen = !ListBoxSearchPopupIsOpen;
        }

        private void ListBox_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            ListBox lb = sender as ListBox;
            if (lb != null && lb.HasItems)
                lb.SelectedIndex = 0;
        }

        private void ListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ListBox lb = sender as ListBox;
                if (lb != null && lb.SelectedIndex != -1)
                {
                    ListBoxSearchPopupIsOpen = false;
                    ListBoxSearchCheckSelectedItem = true;
                    TextBoxSearchValue.Text = lb.SelectedValue.ToString();
                }
            }
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox lb = sender as ListBox;
            if (lb != null && lb.SelectedIndex != -1)
            {
                ListBoxSearchPopupIsOpen = false;
                ListBoxSearchCheckSelectedItem = true;
                TextBoxSearchValue.Text = lb.SelectedValue.ToString();
            }
        }

        private void ListBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //ListBoxSearchVisibility = Visibility.Collapsed;
        }
    }
}
