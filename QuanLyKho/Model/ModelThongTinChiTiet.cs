using QuanLyKho.General;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            InitializeXDoc();
            InitializeBuffer();

        }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        #region list phục vụ truy xuất nhanh thành phần
        private ObservableCollection<string> listNhaPhatHanh;
        private ObservableCollection<string> listNhaXuatBan;
        private ObservableCollection<string> listMaSanPham;
        private void InitializeBuffer()
        {
            listMaSanPham = ListGiaTriMotThanhPhanFromXDoc("MaSanPham", false);
            listNhaPhatHanh = ListGiaTriMotThanhPhanFromXDoc("NhaPhatHanh", false);
            listNhaXuatBan = ListGiaTriMotThanhPhanFromXDoc("NhaXuatBan", false);
        }

        /// <summary>
        /// Từ tên thành phần get list tương ứng
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private ObservableCollection<string> GetListFromName(string name)
        {
            if (name == "MaSanPham")
                return listMaSanPham;
            else if (name == "NhaPhatHanh")
                return listNhaPhatHanh;
            else if (name == "NhaXuatBan")
                return listNhaXuatBan;
            return null;
        }
        #endregion

        public string maSanPham { get; set; }

        public string soLuongNhap { get; set; }

        public string tonKho { get; set; }

        public string tenSanPham { get; set; }

        public string tacGia { get; set; }

        public string nguoiDich { get; set; }

        public string nhaPhatHanh { get; set; }

        public string nhaXuatBan { get; set; }

        public string namXuatBan { get; set; }

        public string kichThuocDai { get; set; }

        public string kichThuocRong { get; set; }

        public string kichThuocCao { get; set; }

        public string thuMucMedia { get; set; }

        public string moTaChiTietSanPham { get; set; }

        private string pathXML;
        private XDocument xDoc;

        public void InitializeXDoc()
        {
            if(xDoc == null)
            {
                Common.CheckAndCreateXML(pathXML, "ThongTinChiTiet");
            }
            try
            {
                xDoc = XDocument.Load(pathXML);
            }
            catch (Exception e)
            {
                throw new Exception("Không đọc được file ThongTinChiTiet.xml. " + e.Message);
            }
        }

        /// <summary>
        /// Thêm 1 sản phẩm vào xDoc và lưu ra file
        /// </summary>
        public Boolean AddAProduceToXDocAndSave()
        {
            Int32 iTonKho = Common.ConvertStringToInt32(tonKho) + Common.ConvertStringToInt32(soLuongNhap);
            XElement aProduce = new XElement("SanPham",
                new XElement("MaSanPham", maSanPham),
                new XElement("TonKho", iTonKho.ToString()),
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

            xDoc.Root.Add(aProduce);
            xDoc.Save(pathXML, SaveOptions.None);
            tonKho = iTonKho.ToString();
            return true;
        }

        /// <summary>
        /// Cập nhật 1 sản phẩm vào xDoc và lưu ra file
        /// </summary>
        /// <returns></returns>
        public Boolean UpdateAProducToXDocAndSave()
        {
            Int32 iTonKho = Common.ConvertStringToInt32(tonKho) + Common.ConvertStringToInt32(soLuongNhap);
            IEnumerable<XElement> le;
            XElement eExist = null;

            le = xDoc
                .Element("ThongTinChiTiet")
                .Elements("SanPham")
                .Where(e => e.Element("MaSanPham").Value == maSanPham);
            if(le.Count() != 0 )
            {
                eExist = le.ElementAt(0);
            }
            if (eExist == null)
                return false;

            eExist.Element("TonKho").Value = iTonKho.ToString();
            eExist.Element("TenSanPham").Value = string.IsNullOrEmpty(tenSanPham)? string.Empty:tenSanPham;
            eExist.Element("TacGia").Value = string.IsNullOrEmpty(tacGia) ? string.Empty : tacGia;
            eExist.Element("NguoiDich").Value = string.IsNullOrEmpty(nguoiDich) ? string.Empty : nguoiDich;
            eExist.Element("NhaPhatHanh").Value = string.IsNullOrEmpty(nhaPhatHanh) ? string.Empty : nhaPhatHanh;
            eExist.Element("NhaXuatBan").Value = string.IsNullOrEmpty(nhaPhatHanh) ? string.Empty : nhaXuatBan;
            eExist.Element("NamXuatBan").Value = string.IsNullOrEmpty(namXuatBan) ? string.Empty : namXuatBan;
            eExist.Element("KichThuocDai").Value = string.IsNullOrEmpty(kichThuocDai) ? string.Empty : kichThuocDai;
            eExist.Element("KichThuocRong").Value = string.IsNullOrEmpty(kichThuocRong) ? string.Empty : kichThuocRong;
            eExist.Element("KichThuocCao").Value = string.IsNullOrEmpty(kichThuocCao) ? string.Empty : kichThuocCao;
            eExist.Element("ThuMucMedia").Value = string.IsNullOrEmpty(thuMucMedia) ? string.Empty : thuMucMedia;
            eExist.Element("MoTaChiTiet").Value = string.IsNullOrEmpty(moTaChiTietSanPham) ? string.Empty : moTaChiTietSanPham;
            xDoc.Save(pathXML, SaveOptions.None);
            tonKho = iTonKho.ToString();
            return true;
        }

        ///// <summary>
        ///// Lưu vào xaml file
        ///// </summary>
        //public Boolean Save()
        //{
        //    // Nếu mã sản phẩm chưa tồn tại, tạo mới

        //    return AddAProduceToXDocAndSave();
        //}

        /// <summary>
        /// Lấy được tất cả giá trị của 1 thành phần
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isIncludeSame">False:Kết quả trả về gồm các giá trị khác nhau. True: cá giá trị có thể giống nhau</param>
        /// <returns></returns>
        public ObservableCollection<string> ListGiaTriMotThanhPhanFromXDoc(string name, Boolean isIncludeSame)
        {
            ObservableCollection<string> list = new ObservableCollection<string>();
            if (xDoc != null)
            {
                foreach (XElement element in xDoc.Descendants(name))
                {
                    if (!string.IsNullOrEmpty(element.Value))
                    {
                        if (isIncludeSame)
                            list.Add(element.Value);
                        else
                        {
                            Int32 count = list.Count();
                            Boolean isExist = false;
                            for (Int32 i = 0; i < count; i++)
                            {
                                if (list[i] == element.Value)
                                {
                                    isExist = true;
                                    break;
                                }
                            }
                            if (!isExist)
                                list.Add(element.Value);
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Danh sách tất cả nhà phát hành
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<string> ListNhaPhatHanh()
        {
            return GetListFromName("NhaPhatHanh");
        }

        /// <summary>
        /// Danh sách giá trị một thành phần với 1 text được tìm từ xDoc
        /// </summary>
        /// <param name="name">Tên thành phần.</param>
        /// <param name="strStart">Text bắt đầu.</param>
        /// <param name="parameterSearch">Tham số cách tìm kiếm</param>
        /// <param name="isIncludeSame">False:Kết quả trả về gồm các giá trị khác nhau. True: cá giá trị có thể giống nhau</param>
        /// <returns></returns>
        public ObservableCollection<string> ListGiaTriMotThanhPhanVoiATextFromXDoc(string name, string str, ParameterSearch parameterSearch, Boolean isIncludeSame)
        {
           if (string.IsNullOrEmpty(str))
                return ListGiaTriMotThanhPhanFromXDoc(name, isIncludeSame);

            ObservableCollection<string> list = new ObservableCollection<string>();
            if (xDoc != null)
            {
                foreach (XElement element in xDoc.Descendants(name))
                {
                    if (!string.IsNullOrEmpty(element.Value))
                    {
                        if ((parameterSearch == ParameterSearch.First 
                            && element.Value.StartsWith(str, StringComparison.OrdinalIgnoreCase))

                            ||(parameterSearch == ParameterSearch.Last
                            && element.Value.EndsWith(str, StringComparison.OrdinalIgnoreCase))

                            ||(parameterSearch == ParameterSearch.All
                            && element.Value.IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0))
                        {
                            if(isIncludeSame)
                                list.Add(element.Value);
                            else
                            {
                                Int32 count = list.Count();
                                Boolean isExist = false;
                                for (Int32 i = 0; i < count; i++)
                                {
                                    if (list[i] == element.Value)
                                    {
                                        isExist = true;
                                        break;
                                    }
                                }
                                if (!isExist)
                                    list.Add(element.Value);
                            }
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Danh sách giá trị một thành phần với 1 text được tìm từ list
        /// </summary>
        /// <param name="name">Tên thành phần.</param>
        /// <param name="strStart">Text bắt đầu.</param>
        /// <param name="parameterSearch">Tham số cách tìm kiếm</param>
        /// <returns></returns>
        public ObservableCollection<string> ListGiaTriMotThanhPhanVoiATextFromList(string name, string str, ParameterSearch parameterSearch)
        {
            ObservableCollection<string> listOriginal = GetListFromName(name);
            if (string.IsNullOrEmpty(str))
            {
                return listOriginal;
            }

            ObservableCollection<string> list = new ObservableCollection<string>();
            Int32 count = listOriginal.Count();
            for (Int32 i = 0; i < count; i++)
            {
                if ((parameterSearch == ParameterSearch.First
                    && listOriginal[i].StartsWith(str, StringComparison.OrdinalIgnoreCase))

                    || (parameterSearch == ParameterSearch.Last
                    && listOriginal[i].EndsWith(str, StringComparison.OrdinalIgnoreCase))

                    || (parameterSearch == ParameterSearch.All
                    && listOriginal[i].IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    list.Add(listOriginal[i]);
                }
            }
            return list;
        }
        /// <summary>
        /// Tìm kiếm nhà phát hành có tên bắt đầu bằng 1 đoạn text
        /// </summary>
        /// <param name="str"></param>
        /// <param name="parameterSearch">Tham số cách tìm kiếm</param>
        /// <returns></returns>
        public ObservableCollection<string> SearchNhaPhatHanhAText(string str, ParameterSearch parameterSearch)
        {
            return ListGiaTriMotThanhPhanVoiATextFromList("NhaPhatHanh", str, parameterSearch);
        }

        /// <summary>
        /// Tìm kiếm nhà xuất bản có tên bắt đầu bằng 1 đoạn text
        /// </summary>
        /// <param name="str"></param>
        /// <param name="parameterSearch">Tham số cách tìm kiếm</param>
        /// <returns></returns>
        public ObservableCollection<string> SearchNhaXuatBanAText(string str, ParameterSearch parameterSearch)
        {
            return ListGiaTriMotThanhPhanVoiATextFromList("NhaXuatBan", str, parameterSearch);
        }

        /// <summary>
        /// Danh sách tất cả nhà xuất bản
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<string> ListNhaXuatBan()
        {
            return GetListFromName("NhaXuatBan");
        }

        /// <summary>
        /// Tìm kiếm nhà phát hành có tên bắt đầu bằng 1 đoạn text
        /// </summary>
        /// <param name="str"></param>
        /// <param name="parameterSearch">Tham số cách tìm kiếm</param>
        /// <returns></returns>
        public ObservableCollection<string> SearchMaSanPhamAText(string str, ParameterSearch parameterSearch)
        {
            return ListGiaTriMotThanhPhanVoiATextFromList("MaSanPham", str, parameterSearch);
        }

        /// <summary>
        /// Danh sách tất cả mã sản phẩm
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<string> ListMaSanPham()
        {
            return GetListFromName("MaSanPham");
        }
    }
}
