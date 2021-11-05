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
    /// Interaction logic for UserControlProductTiki.xaml
    /// </summary>
    public partial class UserControlProductTiki : UserControl
    {
        public UserControlProductTiki()
        {
            InitializeComponent();
        }

        private void CboxHomeAddressShopUsing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = (ViewModelProductTiki)this.DataContext;
            if (vm == null)
                return;
            vm.RefreshView();
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var vm = (ViewModelProductTiki)this.DataContext;
            vm.GetProductDetail();
        }
    }
}
