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

namespace QuanLyKho.View.UserControlCommon
{
    /// <summary>
    /// Interaction logic for UserControlListBoxSearch.xaml
    /// </summary>
    public partial class UserControlListBoxSearch : UserControl
    {
        public UserControlListBoxSearch()
        {
            InitializeComponent();
            bListBoxSearchPopupIsOpen = false;
            bListBoxSearchCheckSelectedItem = false;
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
        
        public static readonly DependencyProperty ListBoxSearchPopupIsOpenProperty = DependencyProperty.Register("bListBoxSearchPopupIsOpen", typeof(Boolean), typeof(UserControlListBoxSearch), null);
        public Boolean bListBoxSearchPopupIsOpen
        {
            get { return (Boolean)GetValue(ListBoxSearchPopupIsOpenProperty); }
            set { SetValue(ListBoxSearchPopupIsOpenProperty, value); }
        }

        // Khi item của listbox được chọn bằng enter, double chuột biến này có giá trị true.
        public static readonly DependencyProperty ListBoxSearchCheckSelectedItemProperty = DependencyProperty.Register("bListBoxSearchCheckSelectedItem", typeof(Boolean), typeof(UserControlListBoxSearch), null);
        public Boolean bListBoxSearchCheckSelectedItem
        {
            get { return (Boolean)GetValue(ListBoxSearchCheckSelectedItemProperty); }
            set { SetValue(ListBoxSearchCheckSelectedItemProperty, value); }
        }

        public static readonly DependencyProperty ListBoxSearchToolTipProperty = DependencyProperty.Register("ListBoxSearchToolTip", typeof(Object), typeof(UserControlListBoxSearch), null);
        public Object ListBoxSearchToolTip
        {
            get { return (Boolean)GetValue(ListBoxSearchToolTipProperty); }
            set { SetValue(ListBoxSearchToolTipProperty, value); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bListBoxSearchPopupIsOpen = !bListBoxSearchPopupIsOpen;
        }

        private void ListBox_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            ListBox lb = sender as ListBox;
            if (lb != null && lb.HasItems)
                lb.SelectedIndex = 0;
        }

        private void ESelectAITem(object sender)
        {
            ListBox lb = sender as ListBox;
            if (lb != null && lb.SelectedIndex != -1)
            {
                bListBoxSearchPopupIsOpen = false;
                bListBoxSearchCheckSelectedItem = true;
                TextBoxSearchValue.Text = lb.SelectedValue.ToString();
            }
        }

        private void ListBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ESelectAITem(sender);
            }
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ESelectAITem(sender);
        }

        private void UserControl_LostFocus(object sender, RoutedEventArgs e)
        {
            var us = (UserControlListBoxSearch)sender;
            if(!us.IsKeyboardFocusWithin && !PopupResult.IsKeyboardFocusWithin)
                bListBoxSearchPopupIsOpen = false;
        }

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                bListBoxSearchPopupIsOpen = false;
            }
            else if(e.Key == Key.Down)
            {
                if (TextBoxSearchValue.IsKeyboardFocused)
                {
                    if (!bListBoxSearchPopupIsOpen)
                        bListBoxSearchPopupIsOpen = true;
                    if (ListBoxResultSearchValue.HasItems)
                    {
                        ListBoxResultSearchValue.SelectedIndex = 0;
                        var listBoxItem = (ListBoxItem)ListBoxResultSearchValue
                                             .ItemContainerGenerator
                                               .ContainerFromItem(ListBoxResultSearchValue.SelectedItem);
                        listBoxItem.Focus();
                    }
                }
            }
            else if(e.Key == Key.Enter)
            {
                bListBoxSearchPopupIsOpen = false;
            }
        }
    }
}
