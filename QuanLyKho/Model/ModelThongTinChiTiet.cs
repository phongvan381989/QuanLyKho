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
    public class ModelThongTinChiTiet : ModelXML
    {
        public ModelThongTinChiTiet()
        {
            pathXML = ((App)Application.Current).GetPathDataXMLThongTinChiTiet();
            InitializeXDoc();
            InitializeBuffer();
            //ThemThanhPhanMoi("KhoiLuong", null);
        }

        #region list phục vụ truy xuất nhanh thành phần
        private ObservableCollection<string> listNhaPhatHanh;
        private ObservableCollection<string> listNhaXuatBan;
        private ObservableCollection<string> listMaSanPham;
        private ObservableCollection<string> listTenSanPham;
        private void InitializeBuffer()
        {
            listMaSanPham = ListGiaTriMotThanhPhanFromXDoc("MaSanPham", false);
            listTenSanPham = ListGiaTriMotThanhPhanFromXDoc("TenSanPham", false);
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
            else if (name == "TenSanPham")
                return listTenSanPham;
            return null;
        }
        #endregion

        /// <summary>
        /// Mô tả lỗi hiện tại
        /// </summary>
        private string errorMessage;

        public string GetErrorMessage()
        {
            return errorMessage;
        }
        /// <summary>
        /// Đây là trường key, trong file mã sản phẩm là duy nhất tương ứng với 1 sản phẩm
        /// </summary>
        public string maSanPham { get; set; } // Trường này là unique

        public string giaSanPham { get; set; }

        public string soLuongNhap { get; set; }

        public string tonKho { get; set; }

        public string tonKhoCanhBaoHetHang { get; set; }

        /// <summary>
        /// Trường này là unique
        /// </summary>
        public string tenSanPham { get; set; }

        public string tacGia { get; set; }

        public string nguoiDich { get; set; }

        public string nhaPhatHanh { get; set; }

        public string nhaXuatBan { get; set; }

        public string namXuatBan { get; set; }

        public string kichThuocDai { get; set; }

        public string kichThuocRong { get; set; }

        public string kichThuocCao { get; set; }

        public string khoiLuong { get; set; }

        public string thuMucMedia { get; set; }

        public string moTaChiTiet { get; set; }

        public string viTriLuuKho { get; set; }

        /// <summary>
        /// Thêm 1 sản phẩm vào xDoc và lưu ra file
        /// </summary>
        public Boolean AddAProduceToXDocAndSave()
        {
            Int32 iTonKho = Common.ConvertStringToInt32(tonKho) + Common.ConvertStringToInt32(soLuongNhap);
            XElement aProduce = new XElement("SanPham",
                new XElement("MaSanPham", maSanPham),
                new XElement("GiaSanPham", giaSanPham),
                new XElement("TonKho", iTonKho.ToString()),
                new XElement("TonKhoCanhBaoHetHang", tonKhoCanhBaoHetHang),
                new XElement("TenSanPham", tenSanPham),
                new XElement("TacGia", tacGia),
                new XElement("NguoiDich", nguoiDich),
                new XElement("NhaPhatHanh", nhaPhatHanh),
                new XElement("NhaXuatBan", nhaXuatBan),
                new XElement("NamXuatBan", namXuatBan),
                new XElement("KichThuocDai", kichThuocDai),
                new XElement("KichThuocRong", kichThuocRong),
                new XElement("KichThuocCao", kichThuocCao),
                new XElement("KhoiLuong", khoiLuong),
                new XElement("ThuMucMedia", thuMucMedia),
                new XElement("MoTaChiTiet", moTaChiTiet),
                new XElement("ViTriLuuKho", viTriLuuKho)
                );

            xDoc.Root.Add(aProduce);
            xDoc.Save(pathXML, SaveOptions.None);
            tonKho = iTonKho.ToString();
            // Cập nhật vào list truy xuất nhanh
            InitializeBuffer();

            return true;
        }

        /// <summary>
        /// Lấy được XElement với mã sản phẩm
        /// </summary>
        /// <param name="maSP"></param>
        /// <returns></returns>
        private XElement GetAXElementFromMaSanPham(string maSP)
        {
            XElement eExist = null;
            if (xDoc == null)
                return eExist;

            IEnumerable<XElement> le;
            le = xDoc
                .Element("ThongTinChiTiet")
                .Elements("SanPham")
                .Where(e => e.Element("MaSanPham").Value == maSP);
            if (le.Count() != 0)
            {
                eExist = le.ElementAt(0);
            }
            return eExist;
        }

        /// <summary>
        ///  Lấy XElement sản phẩm từ tên sản phẩm
        /// </summary>
        /// <param name="tenSP"></param>
        /// <returns></returns>
        private XElement GetAXElementFromTenSanPham(string tenSP)
        {
            XElement eExist = null;
            if (xDoc == null)
                return eExist;

            IEnumerable<XElement> le;
            le = xDoc
                .Element("ThongTinChiTiet")
                .Elements("SanPham")
                .Where(e => e.Element("TenSanPham").Value == tenSP);
            if (le.Count() != 0)
            {
                eExist = le.ElementAt(0);
            }
            return eExist;
        }

        /// <summary>
        /// Cập nhật 1 sản phẩm vào xDoc và lưu ra file
        /// </summary>
        /// <returns></returns>
        public Boolean UpdateAProducToXDocAndSave()
        {
            XElement eExist = GetAXElementFromMaSanPham(maSanPham);
            if (eExist == null)
                return false;

            eExist.Element("GiaSanPham").Value = string.IsNullOrEmpty(giaSanPham) ? string.Empty : giaSanPham;
            Int32 iTonKho = Common.ConvertStringToInt32(tonKho) + Common.ConvertStringToInt32(soLuongNhap);
            eExist.Element("TonKho").Value = iTonKho.ToString();
            eExist.Element("TonKhoCanhBaoHetHang").Value = string.IsNullOrEmpty(tonKhoCanhBaoHetHang) ? string.Empty : tonKhoCanhBaoHetHang;
            eExist.Element("TenSanPham").Value = string.IsNullOrEmpty(tenSanPham) ? string.Empty : tenSanPham;
            eExist.Element("TacGia").Value = string.IsNullOrEmpty(tacGia) ? string.Empty : tacGia;
            eExist.Element("NguoiDich").Value = string.IsNullOrEmpty(nguoiDich) ? string.Empty : nguoiDich;
            eExist.Element("NhaPhatHanh").Value = string.IsNullOrEmpty(nhaPhatHanh) ? string.Empty : nhaPhatHanh;
            eExist.Element("NhaXuatBan").Value = string.IsNullOrEmpty(nhaPhatHanh) ? string.Empty : nhaXuatBan;
            eExist.Element("NamXuatBan").Value = string.IsNullOrEmpty(namXuatBan) ? string.Empty : namXuatBan;
            eExist.Element("KichThuocDai").Value = string.IsNullOrEmpty(kichThuocDai) ? string.Empty : kichThuocDai;
            eExist.Element("KichThuocRong").Value = string.IsNullOrEmpty(kichThuocRong) ? string.Empty : kichThuocRong;
            eExist.Element("KichThuocCao").Value = string.IsNullOrEmpty(kichThuocCao) ? string.Empty : kichThuocCao;
            eExist.Element("KhoiLuong").Value = string.IsNullOrEmpty(khoiLuong) ? string.Empty : khoiLuong;
            eExist.Element("ThuMucMedia").Value = string.IsNullOrEmpty(thuMucMedia) ? string.Empty : thuMucMedia;
            eExist.Element("MoTaChiTiet").Value = string.IsNullOrEmpty(moTaChiTiet) ? string.Empty : moTaChiTiet;
            eExist.Element("ViTriLuuKho").Value = string.IsNullOrEmpty(viTriLuuKho) ? string.Empty : viTriLuuKho;
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
                            && element.Value.IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0)

                            || (parameterSearch == ParameterSearch.Same
                                && string.Compare(element.Value, str) == 0))
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
        /// <param name="str">Text cần được chứa.</param>
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
                    && listOriginal[i].IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0)

                    || (parameterSearch == ParameterSearch.Same
                    && listOriginal[i].Equals(str) == true))
                {
                    list.Add(listOriginal[i]);
                }
            }
            return list;
        }
        /// <summary>
        /// Tìm kiếm nhà phát hành
        /// </summary>
        /// <param name="str"></param>
        /// <param name="parameterSearch">Tham số cách tìm kiếm</param>
        /// <returns></returns>
        public ObservableCollection<string> SearchNhaPhatHanhAText(string str, ParameterSearch parameterSearch)
        {
            return ListGiaTriMotThanhPhanVoiATextFromList("NhaPhatHanh", str, parameterSearch);
        }

        /// <summary>
        /// Tìm kiếm nhà xuất bản
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
        /// Tìm kiếm mã sản phẩm
        /// </summary>
        /// <param name="str"></param>
        /// <param name="parameterSearch">Tham số cách tìm kiếm</param>
        /// <returns></returns>
        public ObservableCollection<string> SearchMaSanPhamAText(string str, ParameterSearch parameterSearch)
        {
            return ListGiaTriMotThanhPhanVoiATextFromList("MaSanPham", str, parameterSearch);
        }

        /// <summary>
        /// Tìm kiếm tên sản phẩm
        /// </summary>
        /// <param name="str"></param>
        /// <param name="parameterSearch">Tham số cách tìm kiếm</param>
        /// <returns></returns>
        public ObservableCollection<string> SearchTenSanPhamAText(string str, ParameterSearch parameterSearch)
        {
            return ListGiaTriMotThanhPhanVoiATextFromList("TenSanPham", str, parameterSearch);
        }

        /// <summary>
        /// Danh sách tất cả mã sản phẩm
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<string> ListMaSanPham()
        {
            return GetListFromName("MaSanPham");
        }

        /// <summary>
        /// Danh sách tất cả tên sản phẩm
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<string> ListTenSanPham()
        {
            return GetListFromName("TenSanPham");
        }

        public Boolean Delete()
        {
            if(xDoc!=null)
            {
                xDoc
                    .Element("ThongTinChiTiet")
                    .Elements("SanPham")
                    .Where(e => e.Element("MaSanPham").Value == maSanPham).Remove();
                xDoc.Save(pathXML, SaveOptions.None);
            }
            return true;
        }

        /// <summary>
        /// Lấy thông tin từ 1 XElement <SanPham> cập nhật cho model
        /// </summary>
        /// <param name="element">XElemnet của tag <SanPham></param>
        /// <param name="sanPham">Đối tượng model cần cập nhật thông tin</param>
        private void ConvertXElementToModel(XElement element)
        {
            maSanPham = element.Element("MaSanPham")?.Value;
            giaSanPham = element.Element("GiaSanPham")?.Value;
            tonKho = element.Element("TonKho")?.Value;
            tonKhoCanhBaoHetHang = element.Element("TonKhoCanhBaoHetHang")?.Value;
            tenSanPham = element.Element("TenSanPham")?.Value;
            tacGia = element.Element("TacGia")?.Value;
            nguoiDich = element.Element("NguoiDich")?.Value;
            nhaPhatHanh = element.Element("NhaPhatHanh")?.Value;
            nhaXuatBan = element.Element("NhaXuatBan")?.Value;
            namXuatBan = element.Element("NamXuatBan")?.Value;
            kichThuocDai = element.Element("KichThuocDai")?.Value;
            kichThuocRong = element.Element("KichThuocRong")?.Value;
            kichThuocCao = element.Element("KichThuocCao")?.Value;
            khoiLuong = element.Element("KhoiLuong")?.Value;
            thuMucMedia = element.Element("ThuMucMedia")?.Value;
            moTaChiTiet = element.Element("MoTaChiTiet")?.Value;
            viTriLuuKho = element.Element("ViTriLuuKho")?.Value;
        }

        /// <summary>
        /// Từ mã sản phẩm lấy thông tin sản phẩm
        /// </summary>
        public void GetASanPhamFromMaSanPham(string maSP)
        {
            if (string.IsNullOrEmpty(maSP))
                return;

            XElement eExist = GetAXElementFromMaSanPham(maSP);
            if (eExist == null)
                return;
            ConvertXElementToModel(eExist);
        }

        /// <summary>
        /// Từ tên sản phẩm lấy thông tin sản phẩm
        /// </summary>
        public void GetASanPhamFromTenSanPham(string tenSP)
        {
            if (string.IsNullOrEmpty(tenSP))
                return;

            XElement eExist = GetAXElementFromTenSanPham(tenSP);
            if (eExist == null)
                return;
            ConvertXElementToModel(eExist);
        }

        /// <summary>
        /// Thêm thành phần thông tin mới vào mỗi <SanPham> tag
        /// </summary>
        /// <param name="name"> Tên thành phần mới</param>
        /// <param name="value">Giá trị thành phần mới</param>
        /// <return>Thành công trả về string trống. Ngược lại trả về thông tin lỗi</return>
        public string ThemThanhPhanMoi(string name, string value)
        {
            string mes = string.Empty;
            if(xDoc == null)
            {
                mes = "Dữ liệu thông tin chi tiết sản phẩm chưa có.";
                return mes;
            }
            // Lấy <SanPham> đầu tiên, check xem tên mới đã tồn tại hay chưa
            XElement eFirstProduce = xDoc
                    .Element("ThongTinChiTiet")
                    .Element("SanPham");

            if(eFirstProduce == null)// chưa tồn tại bất cứ sản phẩm nào
            {
                eFirstProduce = new XElement("SanPham",
                    new XElement(name, value));
                xDoc.Root.Add(eFirstProduce);
            }

            // Check tên thành phần mới đã tồn tại
            foreach (var element in eFirstProduce.Elements())
            {
                if(string.Compare( element.Name.LocalName, name) == 0)
                {
                    mes = "Thành phần mới đã tồn tại.";
                    return mes;
                }
            }

            // Thêm thành phần mới
            foreach(var element in xDoc.Root.Elements("SanPham"))
            {
                element.Add(new XElement(name, value));
            }
            xDoc.Save(pathXML, SaveOptions.None);
            return mes;
        }

        /// <summary>
        /// Check dữ liệu hợp lệ trước khi update
        /// maSanPham phải tổn tại, tenSanPham không được trùng với tenSanPham ở các maSanPham khác
        /// </summary>
        /// <param name="maSP"></param>
        /// <param name="tenSP"></param>
        /// <param name="isCheckExist">true: Có check mã sản phẩm tồn tại hay không</param>
        /// <returns></returns>
        public Boolean CanUpdateAProducde(string maSP, string tenSP, Boolean isCheckMaSPExist)
        {
            // Check maSanPham có tồn tại
            XElement eExist = null;
            if (isCheckMaSPExist)
            {
                eExist = GetAXElementFromMaSanPham(maSP);
                if (eExist == null)
                {
                    errorMessage = "Mã sản phẩm không tồn tại. Không thể cập nhật thông tin sản phẩm với mã này.";
                    MyLogger.GetInstance().Info(errorMessage);
                    return false;
                }
            }

            // Check tên sản phẩm là duy nhất, và thuộc mã sản phẩm trên
            eExist = GetAXElementFromTenSanPham(tenSP);
            if(eExist != null && string.Compare(eExist.Element("MaSanPham")?.Value, maSP) != 0)
            {
                errorMessage = "Tên sản phẩm đã tồn tại với mã sản phẩm khác. Không thể cập nhật thông tin sản phẩm với tên này.";
                MyLogger.GetInstance().Info(errorMessage);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Check dữ liệu hợp lệ trước khi thêm mới 1 sản phẩm
        /// maSanPham phải không tồn tại, tenSanPham không tồn tại
        /// </summary>
        /// <param name="maSP">Phải đảm bảo chưa tồn tại trước khi gọi hàm</param>
        /// <param name="tenSP"></param>
        /// <param name="isCheckExist">true: Có check mã sản phẩm tồn tại hay không</param>
        /// <returns></returns>
        public Boolean CanAddAProduceWithTenSP(string maSP, string tenSP, Boolean isCheckMaSPExist)
        {
            // Check maSanPham có tồn tại
            XElement eExist = null;
            if (isCheckMaSPExist)
            {
                eExist = GetAXElementFromMaSanPham(maSP);
                if (eExist != null)
                {
                    errorMessage = "Mã sản phẩm đã tồn tại. Không thể tạo mới sản phẩm với mã này.";
                    MyLogger.GetInstance().Info(errorMessage);
                    return false;
                }
            }
            // Check tên sản phẩm là duy nhất, và thuộc mã sản phẩm trên
            eExist = GetAXElementFromTenSanPham(tenSP);
            if (eExist != null)
            {
                errorMessage = "Tên sản phẩm đã tồn tại với mã sản phẩm khác. Không thể dùng tên này với sản phẩm tạo mới.";
                MyLogger.GetInstance().Info(errorMessage);
                return false;
            }

            return true;
        }


        public Boolean CreateSampleData()
        {
            string mspTemp = maSanPham;
            string tspTemp = tenSanPham;
            try
            {
                for (int i = 0; i < 500; i++)
                {
                    string str = "_" + i.ToString();
                    maSanPham = mspTemp + str;
                    tenSanPham = tspTemp + str;
                    AddAProduceToXDocAndSave();
                }
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }
    }
}
