using QuanLyKho.General;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace QuanLyKho.Model
{
    public class ModelNhapXuatChiTiet
    {
        private string pathXML;
        private XDocument xDoc;

        public ModelNhapXuatChiTiet()
        {
            pathXML = ((App)Application.Current).GetPathDataXMLNhapXuatChiTiet();
            InitializeXDoc();
        }

        public void InitializeXDoc()
        {
            if (xDoc == null)
            {
                Common.CheckAndCreateXML(pathXML, "NhapXuatChiTiet");
            }
            try
            {
                xDoc = XDocument.Load(pathXML);
            }
            catch (Exception e)
            {
                throw new Exception("Không đọc được file NhapXuatChiTiet.xml. " + e.Message);
            }
        }

        #region list phục vụ truy xuất nhanh thành phần
        //private ObservableCollection<string> listMaSanPham;
        //private void InitializeBuffer()
        //{
        //    listMaSanPham = new ObservableCollection<string>();
        //    if (xDoc != null)
        //    {
        //        foreach (XElement element in xDoc.Descendants("MaSanPham"))
        //        {
        //            if (!string.IsNullOrEmpty(element.Value))
        //            {
        //                listMaSanPham.Add(element.Value);
        //            }
        //        }
        //    }
        //}
        #endregion

        /// <summary>
        /// Thêm mới hoặc cập nhật 1 sản phẩm vào xDoc và lưu ra file
        /// </summary>
        public Boolean AddOrUpdateAProduceToXDocAndSave(string maSanPham, string soLuongNhap)
        {
            // Tìm xem mã sản phẩm đã tồn tại
            IEnumerable<XElement> le;
            XElement eExist = null;
            le = xDoc
                .Element("NhapXuatChiTiet")
                .Elements("SanPham")
                .Where(e => e.Element("MaSanPham").Value == maSanPham);
            if(le.Count() != 0)
            {
                eExist = le.ElementAt(0);
            }
            string time = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss");
            Int32 iSoLuongNhap = Common.ConvertStringToInt32(soLuongNhap);
            if (eExist == null) // tạo mới mã sản phẩm
            {
                XElement aProduce = new XElement("SanPham",
                    new XElement("MaSanPham", maSanPham),
                    new XElement("SoLuongThoiGian",
                    new XElement("SoLuong", iSoLuongNhap.ToString()),
                    new XElement("ThoiGian", time)
                    ));

                xDoc.Root.Add(aProduce);
            }
            else// Cập nhật
            {
                XElement el = new XElement("SoLuongThoiGian",
                    new XElement("SoLuong", iSoLuongNhap.ToString()),
                    new XElement("ThoiGian", time)
                    );
                eExist.Add(el);
            }
            xDoc.Save(pathXML, SaveOptions.None);
            return true;
        }
    }
}
