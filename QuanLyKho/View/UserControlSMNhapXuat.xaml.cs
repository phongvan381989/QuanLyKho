﻿using QuanLyKho.Model;
using QuanLyKho.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Linq;

namespace QuanLyKho.View
{
    /// <summary>
    /// Interaction logic for UserControlSMNhapXuat.xaml
    /// </summary>
    public partial class UserControlSMNhapXuat : UserControl
    {
        public UserControlSMNhapXuat()
        {
            InitializeComponent();
        }

        private void Button_Click_Luu(object sender, RoutedEventArgs e)
        {
            ModelThongTinChiTiet viewModel = (ModelThongTinChiTiet)DataContext;
            //MessageBox.Show(((App)Application.Current).GetPathDataXMLThongTinChiTiet());
            viewModel.Button_Click_Luu(sender, e);
        }

    }
}
