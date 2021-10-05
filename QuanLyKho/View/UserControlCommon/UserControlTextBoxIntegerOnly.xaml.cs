using System;
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

namespace QuanLyKho.View.UserControlCommon
{
    /// <summary>
    /// Interaction logic for UserControlTextBoxIntegerOnly.xaml
    /// Mục đích chỉ cho phép nhập số nguyên
    /// </summary>
    public partial class UserControlTextBoxIntegerOnly : UserControl
    {
        public UserControlTextBoxIntegerOnly()
        {
            InitializeComponent();
        }

        private string oldText = "";
        private Int32 oldCaret;
        public static readonly DependencyProperty IntegerTextProperty = DependencyProperty.Register("IntegerText", typeof(String), typeof(UserControlTextBoxIntegerOnly), null);

        public String IntegerText
        {
            get { return (String)GetValue(IntegerTextProperty); }
            set { SetValue(IntegerTextProperty, value); }
        }

        private void TextBoxIntegerOnly_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = ((TextBox)sender);
            if (string.Compare(textbox.Text, oldText) == 0)
                return;

            Int32 result;
            if(Int32.TryParse(textbox.Text, out result))
            {
                oldText = textbox.Text;
                oldCaret = textbox.CaretIndex;
                return;
            }
            textbox.Text = oldText;
            textbox.CaretIndex = oldCaret;
        }

        private void TextBoxIntegerOnly_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key <= Key.Down && e.Key >= Key.End)
            {
                oldCaret = ((TextBox)sender).CaretIndex;
            }
        }

        private void TextBoxIntegerOnly_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            oldCaret = ((TextBox)sender).CaretIndex;
        }
    }
}
