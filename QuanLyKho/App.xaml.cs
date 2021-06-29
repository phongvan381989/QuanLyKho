using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyKho
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private string pathApp;
        private string pathDataXMLThongTinChiTiet;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            pathApp = System.AppDomain.CurrentDomain.BaseDirectory;
            pathDataXMLThongTinChiTiet = Path.Combine(pathApp, ConfigurationManager.AppSettings["XMLThongTinChiTiet"]);
        }

        public string GetPathApp()
        {
            return pathApp;
        }

        public string GetPathDataXMLThongTinChiTiet()
        {
            return pathDataXMLThongTinChiTiet;
        }
    }
}
