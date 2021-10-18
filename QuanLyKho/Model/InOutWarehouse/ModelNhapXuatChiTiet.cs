﻿using QuanLyKho.General;
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
    public class ModelNhapXuatChiTiet : ModelXML
    {
        static public XDocument xDoc = null; // Biến thao tác xml data duy nhất cho mọi đối tượng
        public ModelNhapXuatChiTiet()
        {
            pathXML = ((App)Application.Current).GetPathDataXMLNhapXuatChiTiet();
            InitializeXDoc(ref xDoc);
        }

        /// <summary>
        /// Thêm mới hoặc cập nhật 1 sản phẩm vào xDoc và lưu ra file
        /// </summary>
        public Boolean AddOrUpdateAProduceToXDocAndSave(string maSanPham, string soLuongNhap)
        {
            // Tìm mã sản phẩm đã tồn tại
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

        public Boolean Delete(string maSanPham)
        {
            if (xDoc != null)
            {
                xDoc
                    .Element("NhapXuatChiTiet")
                    .Elements("SanPham")
                    .Where(e => e.Element("MaSanPham").Value == maSanPham).Remove();
                xDoc.Save(pathXML, SaveOptions.None);
            }
            return true;
        }
    }
}
