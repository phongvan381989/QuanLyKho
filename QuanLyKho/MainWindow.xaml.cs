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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserControl1 uC1 = new UserControl1();
            MainStackPanelContent.Children.Add(uC1);
            UserControl1 uC2 = new UserControl1();
            MainStackPanelContent.Children.Add(uC2);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainStackPanelContent.Children.Clear();
        }

        private void Button_Click_NhapKho(object sender, RoutedEventArgs e)
        {
            ThongTinChiTiet uc = new ThongTinChiTiet();
            MainStackPanelContent.Children.Add(uc);
            MyLogger.GetInstance().Info("Test log nhập kho");
        }
    }
}
