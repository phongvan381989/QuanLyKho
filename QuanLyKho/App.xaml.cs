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
        private string configDataXMLThongTinChiTiet;
        private string configDataXMLNhapXuatChiTiet;
        private Int32 configTonKhoCanhBaoHetHangChung;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            pathApp = System.AppDomain.CurrentDomain.BaseDirectory;
            configDataXMLThongTinChiTiet = Path.Combine(pathApp, ConfigurationManager.AppSettings["XMLThongTinChiTiet"]);
            configDataXMLNhapXuatChiTiet = Path.Combine(pathApp, ConfigurationManager.AppSettings["XMLNhapXuatChiTiet"]);
            string strTemp = Path.Combine(pathApp, ConfigurationManager.AppSettings["TonKhoCanhBaoHetHangChung"]);
            if (!Int32.TryParse(strTemp, out configTonKhoCanhBaoHetHangChung))
                configTonKhoCanhBaoHetHangChung = 5;// default value
        }

        public string GetPathApp()
        {
            return pathApp;
        }

        public string GetPathDataXMLThongTinChiTiet()
        {
            return configDataXMLThongTinChiTiet;
        }

        public string GetPathDataXMLNhapXuatChiTiet()
        {
            return configDataXMLNhapXuatChiTiet;
        }

        public Int32 GetTonKhoCanhBaoHetHangChung()
        {
            return configTonKhoCanhBaoHetHangChung;
        }
    }
}
