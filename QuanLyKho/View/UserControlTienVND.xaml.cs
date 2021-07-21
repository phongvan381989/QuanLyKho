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
        private int oldCaretIndex = -1;
        private void TextBoxTienVND_TextChanged(object sender, TextChangedEventArgs e)
        {
            string  textbox = ((TextBox)sender).Text;
            if (string.Compare(textbox, oldText) == 0)
                return;
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
                if(textbox.ElementAt(i) >= '0' && textbox.ElementAt(i) <= '9')
                {
                    if (textbox.ElementAt(i) == '0' && sb.Length == 0)
                    {
                        ((TextBox)sender).Text = oldText;
                        ((TextBox)sender).CaretIndex = oldCaretIndex;
                        return;
                    }
                    sb.Append(textbox.ElementAt(i));
                }
            }

            try
            {
                int result = Int32.Parse(sb.ToString());
                // Thêm ','
                length = sb.Length;
                if (length > 3 && length < 7)
                    sb.Insert(length - 3, ',');
                else if(length > 6 && length < 10)
                {
                    sb.Insert(length - 3, ',');
                    sb.Insert(length - 6, ',');
                }
                else if(length > 9)
                {
                    sb.Insert(length - 3, ',');
                    sb.Insert(length - 6, ',');
                    sb.Insert(length - 9, ',');
                }
                int carret = -1;
                if (textbox.Length == oldText.Length + 1)
                    carret = ((TextBox)sender).CaretIndex + 1;
                else
                    carret = ((TextBox)sender).CaretIndex + 2;
                ((TextBox)sender).Text = sb.ToString();
                ((TextBox)sender).CaretIndex = carret;
                oldText = sb.ToString();
                oldCaretIndex = carret;
            }
            catch (Exception)
            {
                ((TextBox)sender).Text = oldText;
            }
        }
    }
}
