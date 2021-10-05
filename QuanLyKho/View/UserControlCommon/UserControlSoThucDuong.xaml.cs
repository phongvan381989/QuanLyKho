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

namespace QuanLyKho.View.UserControlCommon
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
        private Int32 oldCaret;

        public static readonly DependencyProperty SoThucDuongTextProperty = DependencyProperty.Register("SoThucDuongText", typeof(String), typeof(UserControlSoThucDuong), null);

        public String SoThucDuongText
        {
            get { return (String)GetValue(SoThucDuongTextProperty); }
            set { SetValue(SoThucDuongTextProperty, value); }
        }

        private void TextBoxSoDuong_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = ((TextBox)sender);
            if (string.Compare(textbox.Text, oldText) == 0)
                return;


            float result;
            if(float.TryParse(textbox.Text, out result))
            {
                if (result > 0)
                {
                    oldText = textbox.Text;
                    oldCaret = textbox.CaretIndex;
                    return;
                }
            }

            textbox.Text = oldText;
            textbox.CaretIndex = oldCaret;
        }

        private void TextBoxSoDuong_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key <= Key.Down && e.Key >= Key.End)
            {
                oldCaret = ((TextBox)sender).CaretIndex;
            }
        }

        private void TextBoxSoDuong_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            oldCaret = ((TextBox)sender).CaretIndex;
        }
    }
}
