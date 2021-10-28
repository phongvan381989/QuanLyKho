using QuanLyKho.ViewModel.Order;
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
    /// Interaction logic for UserControlSMOrder.xaml
    /// </summary>
    public partial class UserControlSMOrder : UserControl
    {
        public UserControlSMOrder()
        {
            InitializeComponent();
        }

        private void TiKi_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)App.Current.MainWindow;
            mw.SetMainContentContainer(new UserControlOrderTiki(), new ViewModelOrderTiki());
        }
    }
}
