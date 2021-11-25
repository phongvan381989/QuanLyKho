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

namespace QuanLyKho.View.Order
{
    /// <summary>
    /// Interaction logic for UserControlOrderCheckProductInWarehouse.xaml
    /// </summary>
    public partial class UserControlOrderCheckProductInWarehouse : UserControl
    {
        public UserControlOrderCheckProductInWarehouse()
        {
            InitializeComponent();
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ViewModelOrderCheckProductInWarehouseTiki vm = (ViewModelOrderCheckProductInWarehouseTiki)this.DataContext;
            vm.Check();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ViewModelOrderCheckProductInWarehouseTiki vm = (ViewModelOrderCheckProductInWarehouseTiki)this.DataContext;
            vm.Check();
        }
    }
}
