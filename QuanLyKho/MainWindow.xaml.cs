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
using QuanLyKho.General;
using QuanLyKho.View;
using QuanLyKho.ViewModel;

namespace QuanLyKho
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    UserControl1 uC1 = new UserControl1();
        //    MainStackPanelContent.Children.Add(uC1);
        //    UserControl1 uC2 = new UserControl1();
        //    MainStackPanelContent.Children.Add(uC2);
        //}

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    MainStackPanelContent.Children.Clear();
        //}

        private void Button_Click_MMNhapXuat(object sender, RoutedEventArgs e)
        {
            MainStackPanelContent.Children.Clear();
            UserControlThongTinChiTiet ucThongTinChiTiet = new UserControlThongTinChiTiet();
            MainStackPanelContent.Children.Add(ucThongTinChiTiet);
            MyLogger.GetInstance().Debug("Click Nhập Xuất");

            ViewModelThongTinChiTiet vmThongTinChiTiet = new ViewModelThongTinChiTiet();
            vmThongTinChiTiet.UpdateSanPhamHienThi();
            /*ucThongTinChiTiet.*/DataContext = vmThongTinChiTiet.sanPhamHienThi;

            SubMenu.Children.Clear();
            SubMenu.Children.Add(new UserControlSMNhapXuat());
        }
    }
}
