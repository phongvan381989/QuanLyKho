using QuanLyKho.General;
using System;
using System.Collections;
using System.Collections.Generic;
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
    //public class ComboBoxSearchSelectedAction
    //{
    //    public event Action<string> selectedAction;

    //    //// What would call this??
    //    //protected void OnMyEvent(EventArgs e)
    //    //{
    //    //    if (MyEvent != null)
    //    //        MyEvent(this, e);
    //    //}
    //}

    /// <summary>
    /// Interaction logic for UserControlComboBoxSearch.xaml
    /// </summary>
    public partial class UserControlComboBoxSearch : UserControl
    {
        public UserControlComboBoxSearch()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ComboBoxSearchItemSourceProperty = DependencyProperty.Register("ComboBoxSearchItemSource", typeof(IEnumerable), typeof(UserControlComboBoxSearch), null);

        public IEnumerable ComboBoxSearchItemSource
        {
            get { return (IEnumerable)GetValue(ComboBoxSearchItemSourceProperty); }
            set { SetValue(ComboBoxSearchItemSourceProperty, value); }
        }

        public static readonly DependencyProperty ComboBoxSearchTextProperty = DependencyProperty.Register("ComboBoxSearchText", typeof(String), typeof(UserControlComboBoxSearch), null);

        public String ComboBoxSearchText
        {
            get { return (String)GetValue(ComboBoxSearchTextProperty); }
            set { SetValue(ComboBoxSearchTextProperty, value); }
        }

        /// <summary>
        /// Giá trị để chỉ cách tìm kiếm
        /// 0: Tham số bắt đầu text
        /// 1: Tham số kết thúc text
        /// 2: Tham số được tìm thấy ở text
        /// </summary>
        public static readonly DependencyProperty Int32ParameterSearchProperty = DependencyProperty.Register("Int32ParameterSearch", typeof(ParameterSearch), typeof(UserControlComboBoxSearch), null);

        public ParameterSearch Int32ParameterSearch
        {
            get { return (ParameterSearch)GetValue(Int32ParameterSearchProperty); }
            set { SetValue(Int32ParameterSearchProperty, value); }
        }

        public static readonly DependencyProperty ComboBoxSearchIsDropDownOpenProperty = DependencyProperty.Register("ComboBoxSearchIsDropDownOpen", typeof(Boolean), typeof(UserControlComboBoxSearch), null);

        public Boolean ComboBoxSearchIsDropDownOpen
        {
            get { return (Boolean)GetValue(ComboBoxSearchIsDropDownOpenProperty); }
            set { SetValue(ComboBoxSearchIsDropDownOpenProperty, value); }
        }

        //// Register the routed event
        //public static readonly RoutedEvent ComboBoxSearchSelectionChangedEvent =
        //EventManager.RegisterRoutedEvent("ComboBoxSearchSelectionChanged", RoutingStrategy.Bubble,
        //typeof(RoutedEventHandler), typeof(UserControlComboBoxSearch));

        //// .NET wrapper
        //public event RoutedEventHandler ComboBoxSearchSelectionChanged
        //{
        //    add { AddHandler(ComboBoxSearchSelectionChangedEvent, value); }
        //    remove { RemoveHandler(ComboBoxSearchSelectionChangedEvent, value); }
        //}

        // Raise the routed event "SelectionChanged"
        //private void ComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        //{
        //RaiseEvent(new RoutedEventArgs(UserControlComboBoxSearch.ComboBoxSearchSelectionChangedEvent));
        ////    int x;
        ////    x = 10;
        //}

        private Int32 iCarret = -1;
        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)((ComboBox)sender).Template.FindName("PART_EditableTextBox", (ComboBox)sender);
            string str = textBox.Text;
            string str1 = textBox.SelectedText;
            //if(iCarret >= 0)
                textBox.SelectionStart = iCarret;
            //else
                //textBox.SelectionStart = textBox.CaretIndex;
            textBox.SelectionLength = 0;
        }

        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //TextBox textBox = (TextBox)((ComboBox)sender).Template.FindName("PART_EditableTextBox", (ComboBox)sender);
            //iCarret = textBox.CaretIndex;
        }

        /// <summary>
        /// Lấy được carret trước khi combobox sổ list, dẫn tới select text ở ô input text là thay đổi carret
        /// </summary>
        public void GetCarretBeforeIsDropDownOpenTrue()
        {
            TextBox textBox = (TextBox)MyComboxSearch.Template.FindName("PART_EditableTextBox", (ComboBox)MyComboxSearch);
            iCarret = textBox.CaretIndex;
        }

        private void MyComboxSearch_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            TextBox textBox = (TextBox)((ComboBox)sender).Template.FindName("PART_EditableTextBox", (ComboBox)sender);
            iCarret = textBox.CaretIndex;
        }
    }
}
