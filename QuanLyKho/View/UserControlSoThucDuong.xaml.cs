using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for UserControlSoTuNhien.xaml
    /// </summary>
    public partial class UserControlSoThucDuong : UserControl
    {
        public UserControlSoThucDuong()
        {
            InitializeComponent();
        }

        private string oldText;

        public static readonly DependencyProperty SoThucDuongTextProperty = DependencyProperty.Register("SoThucDuongText", typeof(String), typeof(UserControlSoThucDuong), null);

        public String SoThucDuongText
        {
            get { return (String)GetValue(SoThucDuongTextProperty); }
            set { SetValue(SoThucDuongTextProperty, value); }
        }

        private void TextBoxSoDuong_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = ((TextBox)sender);
            if (string.IsNullOrEmpty(textbox.Text))
            {
                oldText = "";
                return;
            }

            try
            {
                float result = float.Parse(textbox.Text);
                oldText = textbox.Text;
            }
            catch (FormatException)
            {
                textbox.Text = oldText;
            }
        }
    }
}
