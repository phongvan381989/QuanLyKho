using QuanLyKho.View.Product.Tiki;
using QuanLyKho.ViewModel.Products;
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

namespace QuanLyKho.View.Product
{
    /// <summary>
    /// Interaction logic for UserControlSMProduct.xaml
    /// </summary>
    public partial class UserControlSMProduct : UserControl
    {
        public UserControlSMProduct()
        {
            InitializeComponent();
        }

        private void TiKi_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)App.Current.MainWindow;
            mw.SetMainContentContainer(new UserControlProductTiki(), new ViewModelProductTiki());
        }
    }
}
