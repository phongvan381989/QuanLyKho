using QuanLyKho.ViewModel.Orders;
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

namespace QuanLyKho.View.Order.Tiki
{
    /// <summary>
    /// Interaction logic for UserControlOrderTiki.xaml
    /// </summary>
    public partial class UserControlOrderTiki : UserControl
    {
        public UserControlOrderTiki()
        {
            InitializeComponent();
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var vm = (ViewModelOrderTiki)this.DataContext;
            vm.GetOrderDetail();
        }

        private void CboxHomeAddressShopUsing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = (ViewModelOrderTiki)this.DataContext;
            if (vm == null)
                return;
            vm.RefreshView();
        }
    }
}
