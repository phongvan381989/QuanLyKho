using QuanLyKho.ViewModel.Config;
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

namespace QuanLyKho.View.Config
{
    /// <summary>
    /// Interaction logic for UserControlSMConfig.xaml
    /// </summary>
    public partial class UserControlSMConfig : UserControl
    {
        public UserControlSMConfig()
        {
            InitializeComponent();
        }

        private void TiKi_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = (MainWindow)App.Current.MainWindow;
            mw.SetMainContentContainer(new UserControlConfigTiki(), new ViewModelConfigTiki());
        }
    }
}
