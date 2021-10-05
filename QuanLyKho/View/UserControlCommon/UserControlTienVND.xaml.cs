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
        private int oldCaret = -1;
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
            Int32 carret = ((TextBox)sender).CaretIndex;
            // Chỉ lấy các ký tự 0->9 và '-' ở đầu
            for (int i = 0; i < length; i++)
            {
                if(textbox.ElementAt(i) >= '0' && textbox.ElementAt(i) <= '9')
                {
                    if (textbox.ElementAt(i) == '0' && sb.Length == 0)// Số 0 ở đầu
                    {
                        if (carret >= i)
                            carret--;
                        continue;
                    }
                    sb.Append(textbox.ElementAt(i));
                    continue;
                }
                if (carret >= i)
                    carret--;
            }

            Int32 result;
            if (Int32.TryParse(sb.ToString(), out result))
            {
                // Thêm ','
                length = sb.Length;
                if (length > 9)
                {
                    sb.Insert(length - 3, ',');
                    sb.Insert(length - 6, ',');
                    sb.Insert(length - 9, ',');
                    if (carret > length - 3)
                        carret = carret + 3;
                    else if (carret > length - 6 )
                        carret = carret + 2;
                    else if (carret > length - 9)
                        carret = carret + 1;
                }
                else if (length > 6)
                {
                    sb.Insert(length - 3, ',');
                    sb.Insert(length - 6, ',');
                    if (carret > length - 3)
                        carret = carret + 2;
                    else if (carret > length -6)
                        carret = carret + 1;
                }
                else if (length > 3)
                {
                    sb.Insert(length - 3, ',');
                    if (carret > length - 3)
                        carret = carret + 1;
                }

                oldText = sb.ToString();
                oldCaret = carret;
                ((TextBox)sender).Text = sb.ToString();
                ((TextBox)sender).CaretIndex = carret;
            }
            else
            {
                ((TextBox)sender).Text = oldText;
                ((TextBox)sender).CaretIndex = oldCaret;
            }
        }

        private void TextBoxTienVND_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key <= Key.Down && e.Key >= Key.End)
            {
                oldCaret = ((TextBox)sender).CaretIndex;
            }
        }

        private void TextBoxTienVND_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            oldCaret = ((TextBox)sender).CaretIndex;
        }
    }
}
