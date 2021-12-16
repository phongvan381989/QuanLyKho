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
        /// Thêm mới hoặc cập nhật 1 sản phẩm vào xDoc và lưu ra file hoặc không theo biến isSave
        /// </summary>
        public static Boolean AddOrUpdateAProduceToXDocAndSave(XMLAction action, string maSanPham, string soLuongNhap, Boolean isSave)
        {
            // Tìm mã sản phẩm đã tồn tại
            IEnumerable<XElement> le;
            XElement eExist = null;
            le = action.xDoc
                .Element("NhapXuatChiTiet")
                .Elements("SanPham")
                .Where(e => e.Attribute("MaSanPham").Value == maSanPham);
            if(le.Count() != 0)
            {
                eExist = le.ElementAt(0);
            }

            string time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            Int32 iSoLuongNhap = Common.ConvertStringToInt32(soLuongNhap);

            XElement sltg = new XElement("SoLuongThoiGian", "",
                    new XAttribute("SoLuong", iSoLuongNhap.ToString()),
                    new XAttribute("ThoiGian", time));
            if (eExist == null) // tạo mới mã sản phẩm
            {
                XElement aProduce = new XElement("SanPham", sltg,
                    new XAttribute("MaSanPham", maSanPham));

                action.xDoc.Root.Add(aProduce);
            }
            else// Cập nhật
            {
                eExist.Add(sltg);
            }

            if(isSave)
                action.xDoc.Save(action.pathXML, SaveOptions.None);
            return true;
        }

        /// <summary>
        /// Thêm mới hoặc cập nhật list sản phẩm vào xDoc và lưu ra file
        /// </summary>
        public static Boolean AddOrUpdateListProduceToXDocAndSave(XMLAction action, List<string> lsMaSanPham, List<string> lsSoLuongNhap)
        {
            int count = lsMaSanPham.Count();
            for(int i = 0; i < count; i++)
            {
                if (!AddOrUpdateAProduceToXDocAndSave(action, lsMaSanPham[i], lsSoLuongNhap[i], false))
                    return false;
            }
            action.xDoc.Save(action.pathXML, SaveOptions.None);
            return true;
        }

        public static Boolean Delete(XMLAction action, string maSanPham)
        {
            if (action.xDoc != null)
            {
                action.xDoc
                    .Element("NhapXuatChiTiet")
                    .Elements("SanPham")
                    .Where(e => e.Attribute("MaSanPham").Value == maSanPham).Remove();
                action.xDoc.Save(action.pathXML, SaveOptions.None);
            }
            return true;
        }
    }
}
