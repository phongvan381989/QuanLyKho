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
    public class ModelNhapXuatChiTiet// : ModelXML
    {
        public ModelNhapXuatChiTiet()
        {
        }

        /// <summary>
        /// Thêm mới hoặc cập nhật 1 sản phẩm vào xDoc và lưu ra file
        /// </summary>
        public Boolean AddOrUpdateAProduceToXDocAndSave(XMLAction action, string maSanPham, string soLuongNhap)
        {
            // Tìm mã sản phẩm đã tồn tại
            IEnumerable<XElement> le;
            XElement eExist = null;
            le = action.xDoc
                .Element("NhapXuatChiTiet")
                .Elements("SanPham")
                .Where(e => e.Element("MaSanPham").Value == maSanPham);
            if(le.Count() != 0)
            {
                eExist = le.ElementAt(0);
            }
            string time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            Int32 iSoLuongNhap = Common.ConvertStringToInt32(soLuongNhap);
            if (eExist == null) // tạo mới mã sản phẩm
            {
                XElement aProduce = new XElement("SanPham",
                    new XElement("MaSanPham", maSanPham),
                    new XElement("SoLuongThoiGian",
                    new XElement("SoLuong", iSoLuongNhap.ToString()),
                    new XElement("ThoiGian", time)
                    ));

                action.xDoc.Root.Add(aProduce);
            }
            else// Cập nhật
            {
                XElement el = new XElement("SoLuongThoiGian",
                    new XElement("SoLuong", iSoLuongNhap.ToString()),
                    new XElement("ThoiGian", time)
                    );
                eExist.Add(el);
            }
            action.xDoc.Save(action.pathXML, SaveOptions.None);
            return true;
        }

        public Boolean Delete(XMLAction action, string maSanPham)
        {
            if (action.xDoc != null)
            {
                action.xDoc
                    .Element("NhapXuatChiTiet")
                    .Elements("SanPham")
                    .Where(e => e.Element("MaSanPham").Value == maSanPham).Remove();
                action.xDoc.Save(action.pathXML, SaveOptions.None);
            }
            return true;
        }
    }
}
