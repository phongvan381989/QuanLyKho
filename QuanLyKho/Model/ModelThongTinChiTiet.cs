using QuanLyKho.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace QuanLyKho.Model
{
    public class ModelThongTinChiTiet : INotifyPropertyChanged
    {
        public ModelThongTinChiTiet()
        {
            pathXML = ((App)Application.Current).GetPathDataXMLThongTinChiTiet();
        }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private string _maISBN;
        public string maISBN
        {
            get
            {
                return _maISBN;
            }

            set
            {
                if(_maISBN != value)
                {
                    _maISBN = value;
                    RaisePropertyChanged("maISBN");
                }
            }
        }

        private string _maSanPham;
        public string maSanPham
        {
            get
            {
                return _maSanPham;
            }

            set
            {
                if (_maSanPham != value)
                {
                    _maSanPham = value;
                    RaisePropertyChanged("maSanPham");
                }
            }
        }

        private string _soLuongNhap;
        public string soLuongNhap
        {
            get
            {
                return _soLuongNhap;
            }

            set
            {
                if (_soLuongNhap != value)
                {
                    _soLuongNhap = value;
                    RaisePropertyChanged("soLuongNhap");
                }
            }
        }

        private string _tonKho;
        public string tonKho
        {
            get
            {
                return _tonKho;
            }

            set
            {
                if (_tonKho != value)
                {
                    _tonKho = value;
                    RaisePropertyChanged("tonKho");
                }
            }
        }

        private string _tenSanPham;
        public string tenSanPham
        {
            get
            {
                return _tenSanPham;
            }

            set
            {
                if (_tenSanPham != value)
                {
                    _tenSanPham = value;
                    RaisePropertyChanged("tenSanPham");
                }
            }
        }

        private string _tacGia;
        public string tacGia
        {
            get
            {
                return _tacGia;
            }

            set
            {
                if (_tacGia != value)
                {
                    _tacGia = value;
                    RaisePropertyChanged("tacGia");
                }
            }
        }

        private string _nguoiDich;
        public string nguoiDich
        {
            get
            {
                return _nguoiDich;
            }

            set
            {
                if (_nguoiDich != value)
                {
                    _nguoiDich = value;
                    RaisePropertyChanged("nguoiDich");
                }
            }
        }

        private string _nhaPhatHanh;
        public string nhaPhatHanh
        {
            get
            {
                return _nhaPhatHanh;
            }

            set
            {
                if (_nhaPhatHanh != value)
                {
                    _nhaPhatHanh = value;
                    RaisePropertyChanged("nhaPhatHanh");
                }
            }
        }

        private string _nhaXuatBan;
        public string nhaXuatBan
        {
            get
            {
                return _nhaXuatBan;
            }

            set
            {
                if (_nhaXuatBan != value)
                {
                    _nhaXuatBan = value;
                    RaisePropertyChanged("nhaXuatBan");
                }
            }
        }

        private string _namXuatBan;
        public string namXuatBan
        {
            get
            {
                return _namXuatBan;
            }

            set
            {
                if (_namXuatBan != value)
                {
                    _namXuatBan = value;
                    RaisePropertyChanged("namXuatBan");
                }
            }
        }

        private string _kichThuocDai;
        public string kichThuocDai
        {
            get
            {
                return _kichThuocDai;
            }

            set
            {
                if (_kichThuocDai != value)
                {
                    _kichThuocDai = value;
                    RaisePropertyChanged("kichThuocDai");
                }
            }
        }

        private string _kichThuocRong;
        public string kichThuocRong
        {
            get
            {
                return _kichThuocRong;
            }

            set
            {
                if (_kichThuocRong != value)
                {
                    _kichThuocRong = value;
                    RaisePropertyChanged("kichThuocRong");
                }
            }
        }

        private string _kichThuocCao;
        public string kichThuocCao
        {
            get
            {
                return _kichThuocCao;
            }

            set
            {
                if (_kichThuocCao != value)
                {
                    _kichThuocCao = value;
                    RaisePropertyChanged("kichThuocCao");
                }
            }
        }

        private string _thuMucMedia;
        public string thuMucMedia
        {
            get
            {
                return _thuMucMedia;
            }

            set
            {
                if (_thuMucMedia != value)
                {
                    _thuMucMedia = value;
                    RaisePropertyChanged("thuMucMedia");
                }
            }
        }

        private string _moTaChiTietSanPham;
        public string moTaChiTietSanPham
        {
            get
            {
                return _moTaChiTietSanPham;
            }

            set
            {
                if (_moTaChiTietSanPham != value)
                {
                    _moTaChiTietSanPham = value;
                    RaisePropertyChanged("moTaChiTietSanPham");
                }
            }
        }

        private string pathXML;
        private XDocument xDoc;

        public void InitializeXDoc()
        {
            if(xDoc == null)
            {
                CheckAndCreateXML(pathXML);
                xDoc = XDocument.Load(pathXML);
            }
            SaveXDoc(pathXML);
        }

        public void SaveXDoc(string path)
        {
            xDoc.Save(path, SaveOptions.None);
        }

        /// <summary>
        /// Thêm 1 sản phẩm vào xDoc và lưu ra file
        /// </summary>
        public bool AddAProduceToXDocAndSave()
        {

            try
            {
                XElement aProduce = new XElement("MaSanPham",
                    new XElement("MaISBN", maISBN),
                    new XElement("TonKho", tonKho),
                    new XElement("TenSanPham", tenSanPham),
                    new XElement("TacGia", tacGia),
                    new XElement("NguoiDich", nguoiDich),
                    new XElement("NhaPhatHanh", nhaPhatHanh),
                    new XElement("NhaXuatBan", nhaXuatBan),
                    new XElement("NamXuatBan", namXuatBan),
                    new XElement("KichThuocDai", kichThuocDai),
                    new XElement("KichThuocRong", kichThuocRong),
                    new XElement("KichThuocCao", kichThuocCao),
                    new XElement("ThuMucMedia", thuMucMedia),
                    new XElement("MoTaChiTiet", moTaChiTietSanPham)
                    );

                aProduce.SetAttributeValue("Id", maSanPham);

                xDoc.Root.Add(aProduce);
                SaveXDoc(pathXML);
            }
            catch(Exception e)
            {
                MessageBox.Show("ERROR: " + e.ToString());
                MyLogger.GetInstance().Error(e.ToString());
                return false;
            }
            return true;
        }

        public void Button_Click_Luu(object sender, RoutedEventArgs e)
        {
            InitializeXDoc();
            AddAProduceToXDocAndSave();
        }

        /// <summary>
        /// Check file ThongTinChiTiet.xml tồn tại không? Không tồn tại tạo file mới
        /// </summary>
        /// <param name="path"></param>
        private void CheckAndCreateXML(string path)
        {
            if (!File.Exists(path))
            {
                XDocument xmlDocument = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("ThongTinChiTiet"));

                xmlDocument.Save(path, SaveOptions.None);
            }
        }
    }
}
