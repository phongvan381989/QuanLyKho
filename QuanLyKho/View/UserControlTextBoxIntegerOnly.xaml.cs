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

        private void textBoxValue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed(e.Text);
        }

        private Boolean TextBoxTextAllowed(String Text2)
        {
            // Check cho phép 1 ký tự dấu âm '-' ở đầu string
            string Temp = Text2;
            if (Temp.ElementAt(0) == '-')
            {
                Temp = Temp.Substring(1);
            }
            return Array.TrueForAll<Char>(Temp.ToCharArray(), delegate (Char c)
            {
                return Char.IsDigit(c) || Char.IsControl(c);
            });
        }

        private void textBoxValue_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String Text1 = (String)e.DataObject.GetData(typeof(String));
                if (!TextBoxTextAllowed(Text1)) e.CancelCommand();
            }
            else
                e.CancelCommand();
        }
    }
}
