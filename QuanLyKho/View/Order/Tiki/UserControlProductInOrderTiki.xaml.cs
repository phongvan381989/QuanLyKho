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
    /// Interaction logic for UserControlOrderDetailTiki.xaml
    /// </summary>
    public partial class UserControlProductInOrderTiki : UserControl
    {
        public UserControlProductInOrderTiki()
        {
            InitializeComponent();
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var vm = (ViewModelProductInOrderTiki)this.DataContext;
            vm.GetMapping();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var vm = (ViewModelProductInOrderTiki)this.DataContext;
            vm.Check();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var vm = (ViewModelProductInOrderTiki)this.DataContext;
            vm.Check();
        }
    }
}
