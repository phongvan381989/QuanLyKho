using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using QuanLyKho.General;
using QuanLyKho.View.InOutWarehouse;
using QuanLyKho.View.Dev;
using QuanLyKho.ViewModel.InOutWarehouse;
using QuanLyKho.View.Config;
using QuanLyKho.ViewModel;
using QuanLyKho.ViewModel.Config;
using QuanLyKho.View.Order;
using QuanLyKho.ViewModel.Order;
//using QuanLyKho.ViewModel.D;
namespace QuanLyKho
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum MainMenuSelectIndex
        {
            None,
            NhapXuat,
            Config,
            Order,
            Dev
        }

        public MainMenuSelectIndex mainMenuSelect { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            mainMenuSelect = MainMenuSelectIndex.None;
            //Application.Current.MainWindow = this;
        }

        /// <summary>
        /// Đối tượng MainContentContainer chứa 1 usercontrol tại 1 thời điểm. Hàm này lấy type của usercontrol hiện tại
        /// </summary>
        private Type GetTypeOfMainContentContainer()
        {
            if(MainContentContainer.Children.Count == 1)
            {
                foreach (UIElement child in MainContentContainer.Children)
                {
                    return child.GetType();
                }
            }
            return null;
        }

        private void MMNhapXuat_Click(object sender, RoutedEventArgs e)
        {
            if (mainMenuSelect == MainMenuSelectIndex.NhapXuat)
                return;
            mainMenuSelect = MainMenuSelectIndex.NhapXuat;

            ViewModelThongTinChiTiet vmThongTinChiTiet = new ViewModelThongTinChiTiet();

            // Phải đọc được file db
            if (vmThongTinChiTiet.sanPhamHienThi != null && vmThongTinChiTiet.nhapXuatChiTiet != null)
            {
                vmThongTinChiTiet.UpdateSanPhamHienThi();
                this.DataContext = vmThongTinChiTiet;

                MainContentContainer.Children.Clear();
                UserControlThongTinChiTiet ucThongTinChiTiet = new UserControlThongTinChiTiet();
                MainContentContainer.Children.Add(ucThongTinChiTiet);

                SubMenuContainer.Children.Clear();
                SubMenuContainer.Children.Add(new UserControlSMNhapXuat());
            }
        }

        private void MMDevelop_Click(object sender, RoutedEventArgs e)
        {
            if (mainMenuSelect == MainMenuSelectIndex.Dev)
                return;
            mainMenuSelect = MainMenuSelectIndex.Dev;
            MainContentContainer.Children.Clear();
            MainContentContainer.Children.Add(new UserControlTikiTestAPI());
            SubMenuContainer.Children.Clear();
            SubMenuContainer.Children.Add(new UserControlSMDevelop());
        }

        private void MMConfig_Click(object sender, RoutedEventArgs e)
        {
            if (mainMenuSelect == MainMenuSelectIndex.Config)
                return;

            SubMenuContainer.Children.Clear();
            SubMenuContainer.Children.Add(new UserControlSMConfig());

            mainMenuSelect = MainMenuSelectIndex.Config;
            SetMainContentContainer(new UserControlConfigTikiApp(), new ViewModelTikiConfigApp());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userControl">UserControl</param>
        /// <param name="viewBase"> Datacontext</param>
        public void SetMainContentContainer(UserControl userControl, ViewModelBase viewBase)
        {
            if (GetTypeOfMainContentContainer() == userControl.GetType())
                return;

            MainContentContainer.Children.Clear();
            MainContentContainer.Children.Add(userControl);
            MainContentContainer.DataContext = viewBase;
        }

        private void MMOrder_Click(object sender, RoutedEventArgs e)
        {
            if (mainMenuSelect == MainMenuSelectIndex.Order)
                return;

            SubMenuContainer.Children.Clear();
            SubMenuContainer.Children.Add(new UserControlSMOrder());

            mainMenuSelect = MainMenuSelectIndex.Order;
            SetMainContentContainer(new UserControlOrderTiki(), new ViewModelOrderTiki());
        }
    }
}
