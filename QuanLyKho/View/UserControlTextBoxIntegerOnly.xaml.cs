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

namespace QuanLyKho.View
{
    /// <summary>
    /// Interaction logic for UserControlTextBoxIntegerOnly.xaml
    /// Mục đích chỉ cho phép nhập số nguyên, nhưng chưa xử lý được vấn đề nhập nhiều dấu -
    /// </summary>
    public partial class UserControlTextBoxIntegerOnly : UserControl
    {
        public UserControlTextBoxIntegerOnly()
        {
            InitializeComponent();
        }

        private string oldText;
        public static readonly DependencyProperty IntegerTextProperty = DependencyProperty.Register("IntegerText", typeof(String), typeof(UserControlTextBoxIntegerOnly), null);

        public String IntegerText
        {
            get { return (String)GetValue(IntegerTextProperty); }
            set { SetValue(IntegerTextProperty, value); }
        }

        private void TextBoxIntegerOnly_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = ((TextBox)sender);
            if(string.IsNullOrEmpty(textbox.Text))
            {
                oldText = "";
                return;
            }

            try
            {
                int result = Int32.Parse(textbox.Text);
                oldText = textbox.Text;
            }
            catch (FormatException)
            {
                textbox.Text = oldText;
            }
        }
    }
}
