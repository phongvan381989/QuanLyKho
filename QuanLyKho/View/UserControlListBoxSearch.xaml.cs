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
            ListBoxSearchVisibility = Visibility.Collapsed;
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
        
        public static readonly DependencyProperty ListBoxSearchVisibilityProperty = DependencyProperty.Register("ListBoxSearchVisibility", typeof(Visibility), typeof(UserControlListBoxSearch), null);
        public Visibility ListBoxSearchVisibility
        {
            get { return (Visibility)GetValue(ListBoxSearchVisibilityProperty); }
            set { SetValue(ListBoxSearchVisibilityProperty, value); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxSearchVisibility != Visibility.Visible)
                ListBoxSearchVisibility = Visibility.Visible;
            else
                ListBoxSearchVisibility = Visibility.Collapsed;
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
                    ListBoxSearchVisibility = Visibility.Collapsed;
                    TextBoxSearchValue.Text = lb.SelectedValue.ToString();
                }
            }
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox lb = sender as ListBox;
            if (lb != null && lb.SelectedIndex != -1)
            {
                ListBoxSearchVisibility = Visibility.Collapsed;
                TextBoxSearchValue.Text = lb.SelectedValue.ToString();
            }
        }

        private void ListBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ListBoxSearchVisibility = Visibility.Collapsed;
        }
    }
}
