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
    public partial class UserControlSoDuong : UserControl
    {
        public UserControlSoDuong()
        {
            InitializeComponent();
        }

        private string oldText;
        //private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        //private static bool IsTextAllowed(string text)
        //{
        //    return !_regex.IsMatch(text);
        //}
        //private void TextBoxSoDuong_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    e.Handled = !IsTextAllowed(e.Text);
        //}

        //private void TextBoxSoDuong_Pasting(object sender, DataObjectPastingEventArgs e)
        //{
        //    if (e.DataObject.GetDataPresent(typeof(String)))
        //    {
        //        String text = (String)e.DataObject.GetData(typeof(String));
        //        if (!IsTextAllowed(text))
        //        {
        //            e.CancelCommand();
        //        }
        //    }
        //    else
        //    {
        //        e.CancelCommand();
        //    }
        //}

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
