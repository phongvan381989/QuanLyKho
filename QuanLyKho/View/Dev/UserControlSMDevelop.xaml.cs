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

namespace QuanLyKho.View.Dev
{
    /// <summary>
    /// Interaction logic for UserControlSMDevelop.xaml
    /// </summary>
    public partial class UserControlSMDevelop : UserControl
    {
        public UserControlSMDevelop()
        {
            InitializeComponent();
        }

        private void TiKi_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            mw.MMDevelop_SM_Tiki();
        }

        private void Shopee_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)Application.Current.MainWindow;
            mw.MMDevelop_SM_Shopee();
        }
    }
}
