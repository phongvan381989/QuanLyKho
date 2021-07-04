﻿using System;
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

        private void Button_Click_MMNhapXuat(object sender, RoutedEventArgs e)
        {
            MainStackPanelContent.Children.Clear();
            UserControlThongTinChiTiet ucThongTinChiTiet = new UserControlThongTinChiTiet();
            MainStackPanelContent.Children.Add(ucThongTinChiTiet);
            MyLogger.GetInstance().Debug("Click Nhập Xuất");

            SubMenu.Children.Clear();
            SubMenu.Children.Add(new UserControlSMNhapXuat());

            ViewModelThongTinChiTiet vmThongTinChiTiet = new ViewModelThongTinChiTiet();
            vmThongTinChiTiet.UpdateSanPhamHienThi();
            this.DataContext = vmThongTinChiTiet;
        }
    }
}
