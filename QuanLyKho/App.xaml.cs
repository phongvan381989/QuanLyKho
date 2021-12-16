using QuanLyKho.Model;
using QuanLyKho.Model.Config;
using QuanLyKho.ViewModel.Dev.TikiAPI;
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
        private string pathDataXMLNhapXuatChiTiet;
        private string pathDataXMLThongTinBaoMat;
        private string pathDataXMLMappingSanPhamTMDT_SanPhamKho;
        private Int32 configTonKhoCanhBaoHetHangChung;
        private List<string> listImageFormats;
        private List<string> listVideoFormats;
        public string temporaryFolderPath;
        public string temporaryImageFolderPath;

        //public List<TikiConfigApp> lTikiAppUsing;
        public XMLAction actionModelNhapXuatChiTiet { get; set; }
        public XMLAction actionModelThongTinChiTiet { get; set; }
        public XMLAction actionModelThongTinBaoMat { get; set; }
        public XMLAction actionModelMappingSanPhamTMDT_SanPhamKho { get; set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            pathApp = System.AppDomain.CurrentDomain.BaseDirectory;
            pathDataXMLThongTinChiTiet = Path.Combine(pathApp, ConfigurationManager.AppSettings["XMLThongTinChiTiet"]);
            pathDataXMLNhapXuatChiTiet = Path.Combine(pathApp, ConfigurationManager.AppSettings["XMLNhapXuatChiTiet"]);
            pathDataXMLThongTinBaoMat = Path.Combine(pathApp, ConfigurationManager.AppSettings["XMLThongTinBaoMat"]);
            pathDataXMLMappingSanPhamTMDT_SanPhamKho = Path.Combine(pathApp, ConfigurationManager.AppSettings["XMLMappingSanPhamTMDT_SanPhamKho"]);
            string strTemp = ConfigurationManager.AppSettings["TonKhoCanhBaoHetHangChung"];
            if (!Int32.TryParse(strTemp, out configTonKhoCanhBaoHetHangChung))
                configTonKhoCanhBaoHetHangChung = 5;// default value

            temporaryFolderPath = System.AppDomain.CurrentDomain.BaseDirectory + @"Temporary";
            temporaryImageFolderPath = temporaryFolderPath + @"\Image";

            actionModelNhapXuatChiTiet = new XMLAction(pathDataXMLNhapXuatChiTiet);
            actionModelThongTinChiTiet = new XMLAction(pathDataXMLThongTinChiTiet);
            actionModelThongTinBaoMat = new XMLAction(pathDataXMLThongTinBaoMat);
            actionModelMappingSanPhamTMDT_SanPhamKho = new XMLAction(pathDataXMLMappingSanPhamTMDT_SanPhamKho);

            CommonTikiAPI.GetListTikiConfigAppUsing();

        }

        public string GetPathApp()
        {
            return pathApp;
        }

        public Int32 GetTonKhoCanhBaoHetHangChung()
        {
            return configTonKhoCanhBaoHetHangChung;
        }

        public List<string> GetListImageFormats()
        {
            if(listImageFormats == null || listImageFormats.Count() == 0)
            {
                string strTemp = ConfigurationManager.AppSettings["DinhDangAnh"];
                listImageFormats = strTemp.Split(',').ToList();
            }
            return listImageFormats;
        }

        public List<string> GetListVideoFormats()
        {
            if (listVideoFormats == null || listVideoFormats.Count() == 0)
            {
                string strTemp = ConfigurationManager.AppSettings["DinhDangVideo"];
                listVideoFormats = strTemp.Split(',').ToList();
            }
            return listVideoFormats;
        }
    }
}
