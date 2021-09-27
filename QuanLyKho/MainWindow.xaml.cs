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
using QuanLyKho.View;
using QuanLyKho.View.Dev;
using QuanLyKho.ViewModel;
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
        /// Đối tượng MainStackPanelContent chưa 1 usercontrol tại 1 thời điểm. Hàm này lấy type của usercontrol hiện tại
        /// </summary>
        private Type GetTypeOfMainStackPanelContent()
        {
            if(MainStackPanelContent.Children.Count == 1)
            {
                foreach (UIElement child in MainStackPanelContent.Children)
                {
                    return child.GetType();
                }
            }
            return null;
        }
        private void Button_Click_MMNhapXuat(object sender, RoutedEventArgs e)
        {
            //if (GetTypeOfMainStackPanelContent() == typeof(UserControlThongTinChiTiet))
            //    return;
            if (mainMenuSelect == MainMenuSelectIndex.NhapXuat)
                return;
            mainMenuSelect = MainMenuSelectIndex.NhapXuat;

            ViewModelThongTinChiTiet vmThongTinChiTiet = new ViewModelThongTinChiTiet();

            // Phải đọc được file db
            if (vmThongTinChiTiet.sanPhamHienThi != null)
            {
                vmThongTinChiTiet.UpdateSanPhamHienThi();
                this.DataContext = vmThongTinChiTiet;

                MainStackPanelContent.Children.Clear();
                UserControlThongTinChiTiet ucThongTinChiTiet = new UserControlThongTinChiTiet();
                MainStackPanelContent.Children.Add(ucThongTinChiTiet);

                SubMenu.Children.Clear();
                SubMenu.Children.Add(new UserControlSMNhapXuat());
            }
        }

        private void MMDevelop_Click(object sender, RoutedEventArgs e)
        {
            if (mainMenuSelect == MainMenuSelectIndex.Dev)
                return;
            mainMenuSelect = MainMenuSelectIndex.Dev;
            MainStackPanelContent.Children.Clear();
            MainStackPanelContent.Children.Add(new UserControlTiki());
            SubMenu.Children.Clear();
            SubMenu.Children.Add(new UserControlSMDevelop());
        }

        public void MMDevelop_SM_Tiki()
        {
            MainStackPanelContent.Children.Clear();
            MainStackPanelContent.Children.Add(new UserControlTiki());
        }

        public void MMDevelop_SM_Shopee()
        {
            MainStackPanelContent.Children.Clear();
            MainStackPanelContent.Children.Add(new UserControlShopee());
        }
    }
}
