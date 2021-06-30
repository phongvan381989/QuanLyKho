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

        //private void textBoxValue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    e.Handled = !TextBoxTextAllowed(e.Text);
        //}

        private string oldText;
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(String), typeof(UserControlTextBoxIntegerOnly), null);

        public String Title
        {
            get { return (String)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        ////public static readonly DependencyProperty MyTextProperty = DependencyProperty.Register("MyText", typeof(String), typeof(UserControlTextBoxIntegerOnly), null);
        //public string MyText
        //{
        //    get
        //    {
        //        return textBoxIntegerOnly.Text;
        //    }
        //    set
        //    {
        //        textBoxIntegerOnly.Text = value;
        //    }
        //}

        //private Boolean TextBoxTextAllowed(String Text)
        //{
        //    try
        //    {
        //        int result = Int32.Parse(Text);
        //    }
        //    catch (FormatException)
        //    {
        //        return false;
        //    }

        //    return true;

        //}

        //private void textBoxValue_Pasting(object sender, DataObjectPastingEventArgs e)
        //{
        //    if (e.DataObject.GetDataPresent(typeof(String)))
        //    {
        //        String Text1 = (String)e.DataObject.GetData(typeof(String));
        //        if (!TextBoxTextAllowed(Text1))
        //        {
        //            e.CancelCommand();
        //            ((TextBox)sender).Text = oldText;
        //        }
        //    }
        //    else
        //    {
        //        e.CancelCommand();
        //        ((TextBox)sender).Text = oldText;
        //    }
        //}

        //private void TextBoxIntegerOnly_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    TextBox textbox = ((TextBox)sender);
        //    if(!TextBoxTextAllowed(textbox.Text))
        //    {
        //        MessageBox.Show(string.Format(" {0} không đúng định dạng số nguyên.", textbox.Text));
        //    }
        //}


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
