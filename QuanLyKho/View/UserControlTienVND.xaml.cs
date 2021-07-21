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
    /// Interaction logic for UserControlTienVND.xaml
    /// Định dạng text: 50,000
    /// Không cho phép số âm
    /// </summary>
    public partial class UserControlTienVND : UserControl
    {
        public UserControlTienVND()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TienVNDTextProperty = DependencyProperty.Register("TienVNDText", typeof(String), typeof(UserControlTienVND), null);

        public String TienVNDText
        {
            get { return (String)GetValue(TienVNDTextProperty); }
            set { SetValue(TienVNDTextProperty, value); }
        }
        private string oldText = "";
        private void TextBoxTienVND_TextChanged(object sender, TextChangedEventArgs e)
        {
            string  textbox = ((TextBox)sender).Text;
            if (string.IsNullOrEmpty(textbox))
            {
                oldText = "";
                return;
            }
            int length = textbox.Length;
            StringBuilder sb = new StringBuilder("", 10);
            // Chỉ lấy các ký tự 0->9
            for (int i = 0; i < length; i++)
            {
                if(textbox.ElementAt(i) >= '0' && textbox.ElementAt(i) <=9)
                {
                    if (textbox.ElementAt(i) == '0' && sb.Length == 0)
                        break;
                    sb.Append(textbox.ElementAt(i));
                }
            }

            try
            {
                int result = Int32.Parse(sb.ToString());
                if (result < 1000000000) // check giới hạn trên
                {
                    // Thêm ','
                    if (sb.Length > 4 && sb.Length < 7)
                        sb.Insert(sb.Length - 3, ',');
                    else if(sb.Length > 6 && sb.Length < 10)

                    oldText = textbox.Text;
                }
            }
            catch (FormatException)
            {
                ((TextBox)sender).Text = oldText;
            }
        }
    }
}
