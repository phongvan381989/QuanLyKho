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
    public class ModelThongTinChiTiet
    {
        public ModelThongTinChiTiet()
        {
        }

        /// <summary>
        /// Mô tả lỗi hiện tại
        /// </summary>
        private static string errorMessage;

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

        public void Refresh()
        {
            maSanPham = string.Empty;
            giaSanPham = string.Empty;
            tonKho = string.Empty;
            tonKhoCanhBaoHetHang = string.Empty;
            tenSanPham = string.Empty;
            tacGia = string.Empty;
            nguoiDich = string.Empty;
            nhaPhatHanh = string.Empty;
            nhaXuatBan = string.Empty;
            namXuatBan = string.Empty;
            kichThuocDai = string.Empty;
            kichThuocRong = string.Empty;
            kichThuocCao = string.Empty;
            khoiLuong = string.Empty;
            thuMucMedia = string.Empty;
            moTaChiTiet = string.Empty;
            viTriLuuKho = string.Empty;
        }

        /// <summary>
        /// Thêm 1 sản phẩm vào xDoc và lưu ra file
        /// </summary>
        public static Boolean AddAProduceToXDocAndSave(XMLAction action, ModelThongTinChiTiet ttct)
        {
            Int32 iTonKho = Common.ConvertStringToInt32(ttct.tonKho) + Common.ConvertStringToInt32(ttct.soLuongNhap);
            XElement aProduce = new XElement("SanPham",
                new XElement("MaSanPham", ttct.maSanPham),
                new XElement("GiaSanPham", ttct.giaSanPham),
                new XElement("TonKho", iTonKho.ToString()),
                new XElement("TonKhoCanhBaoHetHang", ttct.tonKhoCanhBaoHetHang),
                new XElement("TenSanPham", ttct.tenSanPham),
                new XElement("TacGia", ttct.tacGia),
                new XElement("NguoiDich", ttct.nguoiDich),
                new XElement("NhaPhatHanh", ttct.nhaPhatHanh),
                new XElement("NhaXuatBan", ttct.nhaXuatBan),
                new XElement("NamXuatBan", ttct.namXuatBan),
                new XElement("KichThuocDai", ttct.kichThuocDai),
                new XElement("KichThuocRong", ttct.kichThuocRong),
                new XElement("KichThuocCao", ttct.kichThuocCao),
                new XElement("KhoiLuong", ttct.khoiLuong),
                new XElement("ThuMucMedia", ttct.thuMucMedia),
                new XElement("MoTaChiTiet", ttct.moTaChiTiet),
                new XElement("ViTriLuuKho", ttct.viTriLuuKho)
                );

            action.xDoc.Root.Add(aProduce);
            action.xDoc.Save(action.pathXML, SaveOptions.None);
            ttct.tonKho = iTonKho.ToString();
            ttct.soLuongNhap = string.Empty;
            return true;
        }

        /// <summary>
        /// Lấy được XElement với mã sản phẩm
        /// </summary>
        /// <param name="maSP"></param>
        /// <returns></returns>
        private static XElement GetAXElementFromMaSanPham(XMLAction action, string maSP)
        {
            XElement eExist = null;
            if (action.xDoc == null)
                return eExist;

            IEnumerable<XElement> le = null;
            le = action.xDoc
                .Element("ThongTinChiTiet")
                .Elements("SanPham")
                .Where(e => e.Element("MaSanPham").Value == maSP);
            if (le != null && le.Count() != 0)
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
        private static XElement GetAXElementFromTenSanPham(XMLAction action, string tenSP)
        {
            XElement eExist = null;
            if (action.xDoc == null)
                return eExist;

            IEnumerable<XElement> le;
            le = action.xDoc
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
        public static Boolean UpdateAProducToXDocAndSave(XMLAction action, ModelThongTinChiTiet ttct)
        {
            XElement eExist = GetAXElementFromMaSanPham(action, ttct.maSanPham);
            if (eExist == null)
                return false;

            eExist.Element("GiaSanPham").Value = string.IsNullOrEmpty(ttct.giaSanPham) ? string.Empty : ttct.giaSanPham;
            Int32 iTonKho = Common.ConvertStringToInt32(ttct.tonKho) + Common.ConvertStringToInt32(ttct.soLuongNhap);
            eExist.Element("TonKho").Value = iTonKho.ToString();
            eExist.Element("TonKhoCanhBaoHetHang").Value = string.IsNullOrEmpty(ttct.tonKhoCanhBaoHetHang) ? string.Empty : ttct.tonKhoCanhBaoHetHang;
            eExist.Element("TenSanPham").Value = string.IsNullOrEmpty(ttct.tenSanPham) ? string.Empty : ttct.tenSanPham;
            eExist.Element("TacGia").Value = string.IsNullOrEmpty(ttct.tacGia) ? string.Empty : ttct.tacGia;
            eExist.Element("NguoiDich").Value = string.IsNullOrEmpty(ttct.nguoiDich) ? string.Empty : ttct.nguoiDich;
            eExist.Element("NhaPhatHanh").Value = string.IsNullOrEmpty(ttct.nhaPhatHanh) ? string.Empty : ttct.nhaPhatHanh;
            eExist.Element("NhaXuatBan").Value = string.IsNullOrEmpty(ttct.nhaPhatHanh) ? string.Empty : ttct.nhaXuatBan;
            eExist.Element("NamXuatBan").Value = string.IsNullOrEmpty(ttct.namXuatBan) ? string.Empty : ttct.namXuatBan;
            eExist.Element("KichThuocDai").Value = string.IsNullOrEmpty(ttct.kichThuocDai) ? string.Empty : ttct.kichThuocDai;
            eExist.Element("KichThuocRong").Value = string.IsNullOrEmpty(ttct.kichThuocRong) ? string.Empty : ttct.kichThuocRong;
            eExist.Element("KichThuocCao").Value = string.IsNullOrEmpty(ttct.kichThuocCao) ? string.Empty : ttct.kichThuocCao;
            eExist.Element("KhoiLuong").Value = string.IsNullOrEmpty(ttct.khoiLuong) ? string.Empty : ttct.khoiLuong;
            eExist.Element("ThuMucMedia").Value = string.IsNullOrEmpty(ttct.thuMucMedia) ? string.Empty : ttct.thuMucMedia;
            eExist.Element("MoTaChiTiet").Value = string.IsNullOrEmpty(ttct.moTaChiTiet) ? string.Empty : ttct.moTaChiTiet;
            eExist.Element("ViTriLuuKho").Value = string.IsNullOrEmpty(ttct.viTriLuuKho) ? string.Empty : ttct.viTriLuuKho;
            action.xDoc.Save(action.pathXML, SaveOptions.None);
            ttct.tonKho = iTonKho.ToString();
            ttct.soLuongNhap = string.Empty;
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
        public static ObservableCollection<string> ListGiaTriMotThanhPhanFromXDoc(XMLAction action, string name, Boolean isIncludeSame)
        {
            ObservableCollection<string> list = new ObservableCollection<string>();
            if (action.xDoc != null)
            {
                foreach (XElement element in action.xDoc.Descendants(name))
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
        /// Danh sách giá trị một thành phần với 1 text được tìm từ xDoc
        /// </summary>
        /// <param name="name">Tên thành phần.</param>
        /// <param name="strStart">Text bắt đầu.</param>
        /// <param name="parameterSearch">Tham số cách tìm kiếm</param>
        /// <param name="isIncludeSame">False:Kết quả trả về gồm các giá trị khác nhau. True: cá giá trị có thể giống nhau</param>
        /// <returns></returns>
        public ObservableCollection<string> ListGiaTriMotThanhPhanVoiATextFromXDoc(XMLAction action, string name, string str, ParameterSearch parameterSearch, Boolean isIncludeSame)
        {
           if (string.IsNullOrEmpty(str))
                return ListGiaTriMotThanhPhanFromXDoc(action, name, isIncludeSame);

            ObservableCollection<string> list = new ObservableCollection<string>();
            if (action.xDoc != null)
            {
                foreach (XElement element in action.xDoc.Descendants(name))
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

        public static Boolean Delete(XMLAction action, string maSP)
        {
            if(action.xDoc !=null)
            {
                action.xDoc
                    .Element("ThongTinChiTiet")
                    .Elements("SanPham")
                    .Where(e => e.Element("MaSanPham").Value == maSP).Remove();
                action.xDoc.Save(action.pathXML, SaveOptions.None);
            }
            return true;
        }

        /// <summary>
        /// Lấy thông tin từ 1 XElement <SanPham> cập nhật cho model
        /// </summary>
        /// <param name="element">XElemnet của tag <SanPham></param>
        /// <param name="sanPham">Đối tượng model cần cập nhật thông tin</param>
        private static ModelThongTinChiTiet ConvertXElementToModel(XElement element)
        {
            ModelThongTinChiTiet obj = new ModelThongTinChiTiet();
            obj.maSanPham = element.Element("MaSanPham")?.Value;
            obj.giaSanPham = element.Element("GiaSanPham")?.Value;
            obj.tonKho = element.Element("TonKho")?.Value;
            obj.tonKhoCanhBaoHetHang = element.Element("TonKhoCanhBaoHetHang")?.Value;
            obj.tenSanPham = element.Element("TenSanPham")?.Value;
            obj.tacGia = element.Element("TacGia")?.Value;
            obj.nguoiDich = element.Element("NguoiDich")?.Value;
            obj.nhaPhatHanh = element.Element("NhaPhatHanh")?.Value;
            obj.nhaXuatBan = element.Element("NhaXuatBan")?.Value;
            obj.namXuatBan = element.Element("NamXuatBan")?.Value;
            obj.kichThuocDai = element.Element("KichThuocDai")?.Value;
            obj.kichThuocRong = element.Element("KichThuocRong")?.Value;
            obj.kichThuocCao = element.Element("KichThuocCao")?.Value;
            obj.khoiLuong = element.Element("KhoiLuong")?.Value;
            obj.thuMucMedia = element.Element("ThuMucMedia")?.Value;
            obj.moTaChiTiet = element.Element("MoTaChiTiet")?.Value;
            obj.viTriLuuKho = element.Element("ViTriLuuKho")?.Value;
            return obj;
        }

        /// <summary>
        /// Từ mã sản phẩm lấy thông tin sản phẩm
        /// </summary>
        public static ModelThongTinChiTiet GetASanPhamFromMaSanPham(XMLAction action, string maSP)
        {
            if (string.IsNullOrEmpty(maSP))
                return null;

            XElement eExist = GetAXElementFromMaSanPham(action, maSP);
            if (eExist == null)
                return null;
            return ConvertXElementToModel(eExist);
        }

        /// <summary>
        /// Từ tên sản phẩm lấy thông tin sản phẩm
        /// </summary>
        public static ModelThongTinChiTiet GetASanPhamFromTenSanPham(XMLAction action, string tenSP)
        {
            if (string.IsNullOrEmpty(tenSP))
                return null;

            XElement eExist = GetAXElementFromTenSanPham(action, tenSP);
            if (eExist == null)
                return null;
            return ConvertXElementToModel(eExist);
        }

        /// <summary>
        /// Thêm thành phần thông tin mới vào mỗi <SanPham> tag
        /// </summary>
        /// <param name="name"> Tên thành phần mới</param>
        /// <param name="value">Giá trị thành phần mới</param>
        /// <return>Thành công trả về string trống. Ngược lại trả về thông tin lỗi</return>
        public string ThemThanhPhanMoi(XMLAction action, string name, string value)
        {
            string mes = string.Empty;
            if(action.xDoc == null)
            {
                mes = "Dữ liệu thông tin chi tiết sản phẩm chưa có.";
                return mes;
            }
            // Lấy <SanPham> đầu tiên, check xem tên mới đã tồn tại hay chưa
            XElement eFirstProduce = action.xDoc
                    .Element("ThongTinChiTiet")
                    .Element("SanPham");

            if(eFirstProduce == null)// chưa tồn tại bất cứ sản phẩm nào
            {
                eFirstProduce = new XElement("SanPham",
                    new XElement(name, value));
                action.xDoc.Root.Add(eFirstProduce);
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
            foreach(var element in action.xDoc.Root.Elements("SanPham"))
            {
                element.Add(new XElement(name, value));
            }
            action.xDoc.Save(action.pathXML, SaveOptions.None);
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
        public static Boolean CanUpdateAProducde(XMLAction action, string maSP, string tenSP, Boolean isCheckMaSPExist)
        {
            // Check maSanPham có tồn tại
            XElement eExist = null;
            if (isCheckMaSPExist)
            {
                eExist = GetAXElementFromMaSanPham(action, maSP);
                if (eExist == null)
                {
                    errorMessage = "Mã sản phẩm không tồn tại. Không thể cập nhật thông tin sản phẩm với mã này.";
                    MyLogger.GetInstance().Info(errorMessage);
                    return false;
                }
            }

            // Check tên sản phẩm là duy nhất, và thuộc mã sản phẩm trên
            eExist = GetAXElementFromTenSanPham(action, tenSP);
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
        public static Boolean CanAddAProduceWithTenSP(XMLAction action, string maSP, string tenSP, Boolean isCheckMaSPExist)
        {
            // Check maSanPham có tồn tại
            XElement eExist = null;
            if (isCheckMaSPExist)
            {
                eExist = GetAXElementFromMaSanPham(action, maSP);
                if (eExist != null)
                {
                    errorMessage = "Mã sản phẩm đã tồn tại. Không thể tạo mới sản phẩm với mã này.";
                    MyLogger.GetInstance().Info(errorMessage);
                    return false;
                }
            }
            // Check tên sản phẩm là duy nhất, và thuộc mã sản phẩm trên
            eExist = GetAXElementFromTenSanPham(action, tenSP);
            if (eExist != null)
            {
                errorMessage = "Tên sản phẩm đã tồn tại với mã sản phẩm khác. Không thể dùng tên này với sản phẩm tạo mới.";
                MyLogger.GetInstance().Info(errorMessage);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Lấy được danh sách sản phẩm trong kho theo mã.
        /// Mã trống hoặc null ta lấy danh sách tất cả sản phẩm trong kho
        /// </summary>
        /// <param name="maSP"></param>
        /// <returns>Rỗng nếu không tìm thấy</returns>
        public static List<ModelThongTinChiTiet> GetListProductFromCode(XMLAction action, string maSP)
        {
            List<ModelThongTinChiTiet> ls = new List<ModelThongTinChiTiet>();
            if (action.xDoc == null)
                return ls;
            if (string.IsNullOrEmpty(maSP)) // Lấy tất cả sản phẩm trong kho
            {
                IEnumerable<XElement> le;
                le = action.xDoc
                    .Element("ThongTinChiTiet")
                    .Elements("SanPham");
                foreach(XElement e in le)
                {
                    ls.Add(ConvertXElementToModel(e));
                }
            }
            else
            {
                ls.Add(GetASanPhamFromMaSanPham(action, maSP));
            }
            return ls;
        }

        public Boolean CreateSampleData(XMLAction action)
        {
            ModelThongTinChiTiet ttct = new ModelThongTinChiTiet();
            string mspTemp = "1234";
            string tspTemp = "Sau maru";
            try
            {
                for (int i = 0; i < 500; i++)
                {
                    string str = "_" + i.ToString();
                    ttct.maSanPham = mspTemp + str;
                    ttct.tenSanPham = tspTemp + str;
                    AddAProduceToXDocAndSave(action, ttct);
                }
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// Cập nhật tồn kho 1 sản phẩm
        /// </summary>
        /// <param name="maSanPham"></param>
        /// <param name="soLuong">Số lượng thêm vào kho</param>
        /// <param name="isSave">true: Save vào xml file, false: Không save</param>
        /// <returns></returns>
        static public Boolean UpdateTonKhoAProduct(XMLAction action, string maSanPham, string soLuongNhap, Boolean isSave)
        {
            XElement eExist = GetAXElementFromMaSanPham(action, maSanPham);
            if (eExist == null)
            {
                Common.CommonErrorMessage = "Sản phẩm không tồn tại trong kho.";
                return false;
            }

            Int32 iTonKho = Common.ConvertStringToInt32(eExist.Element("TonKho").Value) + Common.ConvertStringToInt32(soLuongNhap);
            if(iTonKho < 0)
            {
                Common.CommonErrorMessage = "Vượt quá số lượng sản phẩm trong kho.";
                return false;
            }
            eExist.Element("TonKho").Value = iTonKho.ToString();
            if (isSave)
                action.xDoc.Save(action.pathXML, SaveOptions.None);
            return true;
        }

        /// <summary>
        /// Cập nhật tồn kho 1 danh sách sản phẩm
        /// </summary>
        /// <param name="action"></param>
        /// <param name="lsMaSanPham"></param>
        /// <param name="lsSoLuongNhap"></param>
        /// <returns></returns>
        static public Boolean UpdateTonKhoListProduct(XMLAction action, List<string> lsMaSanPham, List<string> lsSoLuongNhap)
        {
            int count = lsMaSanPham.Count();
            for(int i = 0; i < count; i++ )
            {
                if(!UpdateTonKhoAProduct(action, lsMaSanPham[i], lsSoLuongNhap[i], false))
                {
                    return false;
                }
            }
            action.xDoc.Save(action.pathXML, SaveOptions.None);
            return true;
        }

        /// <summary>
        /// Kiểm tra số lượng trong kho đủ
        /// </summary>
        /// <param name="action"></param>
        /// <param name="maSanPham"></param>
        /// <param name="soLuongNhap"></param>
        /// <returns></returns>
        static public Boolean CheckQuantityEnough(XMLAction action, string maSanPham, string soLuongNhap)
        {
            XElement eExist = GetAXElementFromMaSanPham(action, maSanPham);
            if (eExist == null)
            {
                Common.CommonErrorMessage = "Sản phẩm không tồn tại trong kho.";
                return false;
            }

            // Vì số lượng nhập là âm
            if(Common.ConvertStringToInt32(eExist.Element("TonKho").Value) + Common.ConvertStringToInt32(soLuongNhap) < 0)
            {
                Common.CommonErrorMessage = "Vượt quá số lượng sản phẩm trong kho.";
                return false;
            }

            return true;
        }
    }
}
