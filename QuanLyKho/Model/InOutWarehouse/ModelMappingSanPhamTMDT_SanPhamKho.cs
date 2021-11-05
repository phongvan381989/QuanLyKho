using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace QuanLyKho.Model.InOutWarehouse
{
    /// <summary>
    /// Mapping 1 sản phẩm trên TMDT với 1 hoặc nhiều sản phẩm thực tế trong kho. 
    /// VD: 1 commbo sách 6 cuốn trên sàn TMDT map với 6 cuốn lẻ trong kho
    /// <?xml version="1.0" encoding="utf-8" standalone="yes"?>
    /// File XML có format như sau:
    /// <MappingSanPhamTMDT_SanPhamKho>
    ///   <Tiki>
    ///     <SanPhamTMDT>
	///       <MaSanPhamTMDT>12346</MaSanPhamTMDT>
    ///       <ID>11</ID>
    ///       <ID>12</ID>
    ///       <ID>13</ID>
    ///       <ID>14</ID>
    ///     </SanPhamTMDT>
	///     <SanPhamTMDT>
	///       <MaSanPhamTMDT>abcdef</MaSanPhamTMDT>
    ///       <ID>11</ID>
    ///       <ID>12</ID>
    ///       <ID>13</ID>
    ///       <ID>14</ID>
    ///     </SanPhamTMDT>
    ///     <SanPhamTMDT>
	///       <MaSanPhamTMDT>123abc</MaSanPhamTMDT>
    ///       <ID>11</ID>
    ///       <ID>12</ID>
    ///       <ID>13</ID>
    ///       <ID>14</ID>
    ///     </SanPhamTMDT>
	///     <SanPhamTMDT>
	///       <MaSanPhamTMDT>def456</MaSanPhamTMDT>
    ///       <ID>11</ID>
    ///       <ID>12</ID>
    ///       <ID>13</ID>
    ///       <ID>14</ID>
    ///     </SanPhamTMDT>
    ///   </Tiki>
    /// </MappingSanPhamTMDT_Kho>
    /// </summary>
    public class ModelMappingSanPhamTMDT_SanPhamKho : ModelXML
    {
        private const string eTikiSanPhamTMDTName = "SanPhamTMDT";
        private const string eTikiMaSanPhamTMDTName = "MaSanPhamTMDT";
        private const string eTikiName = "ID";

        static public XDocument xDoc = null; // Biến thao tác xml data duy nhất cho mọi đối tượng
        public ModelMappingSanPhamTMDT_SanPhamKho()
        {
            pathXML = ((App)Application.Current).GetPathDataXMLMappingSanPhamTMDT_SanPhamKho();
            InitializeXDoc(ref xDoc);
            InitializeStruct();
        }
        /// <summary>
        /// Khởi tạo cấu trúc node cho file
        /// </summary>
        /// <returns></returns>
        private Boolean InitializeStruct()
        {
            Tiki_InitializeStruct();
            return true;
        }

        /// <summary>
        /// Khởi tạo cấu trúc node của TIKI
        /// </summary>
        /// <returns></returns>
        private Boolean Tiki_InitializeStruct()
        {
            XElement eMap = null;
            eMap = xDoc.Element("MappingSanPhamTMDT_SanPhamKho");
            if (eMap == null)
            {
                // Tạo mới root
                XElement newE = new XElement("MappingSanPhamTMDT_SanPhamKho",
                new XElement("Tiki"));
                xDoc.Root.Add(newE);
                xDoc.Save(pathXML, SaveOptions.None);
                return true;
            }
            XElement eTiki = xDoc.Element("MappingSanPhamTMDT_SanPhamKho").Element("Tiki");
            if (eTiki == null)
            {
                XElement newE = new XElement("Tiki");
                eMap.Add(newE);
                xDoc.Save(pathXML, SaveOptions.None);
                return true;
            }

            return true;
        }

        /// <summary>
        /// Lấy được Tiki Node
        /// </summary>
        /// <returns></returns>
        private XElement TiKi_GetTikiNode()
        {
            return xDoc
                .Element("MappingSanPhamTMDT_SanPhamKho")
                .Element("Tiki");
        }

        /// <summary>
        /// Kiểm tra Id sản phẩm TMDT (MaSanPhamTMDT)đã tồn tại
        /// </summary>
        /// <param name="appID"></param>
        /// <returns></returns>
        public Boolean Tiki_CheckMaSanPhamTMDTExist(string ID)
        {
            XElement eTiki = TiKi_GetTikiNode();
            IEnumerable<XElement> lElement = null;
            lElement = eTiki.Elements(eTikiSanPhamTMDTName).Where(e => e.Element(eTikiMaSanPhamTMDTName).Value == ID);
            if (lElement == null || lElement.Count() == 0)
                return false;
            return true;
        }

        /// <summary>
        /// Thêm mới hoặc cập nhật mapping id sản phẩm với sản phẩm trong kho
        /// </summary>
        /// <param name="idSPTMDT"></param>
        /// <param name="lsID"></param>
        /// <returns></returns>
        public string Tiki_AddOrUpdate(string idSPTMDT, List<string> lsID)
        {
            if (string.IsNullOrWhiteSpace(idSPTMDT) ||
                lsID == null)
                return "Mã sản phẩm TMDT hoặc trong kho không đúng.";

            XElement eTiki = TiKi_GetTikiNode();
            IEnumerable<XElement> lElement = null;
            lElement = eTiki.Elements(eTikiSanPhamTMDTName).Where(e => e.Element(eTikiMaSanPhamTMDTName).Value == idSPTMDT);
            if (lElement == null || lElement.Count() == 0) // Thêm mới
            {
                XElement newE = new XElement(eTikiSanPhamTMDTName);
                newE.Add(new XElement(eTikiMaSanPhamTMDTName, idSPTMDT));
                foreach(string str in lsID)
                {
                    newE.Add(new XElement(eTikiName, str));
                }
                eTiki.Add(newE);
            }
            else// Cập nhật
            {
                XElement eOldSPTMDT = lElement.ElementAt(0);
                // Xóa bỏ element bên trong
                eOldSPTMDT.RemoveAll();
                eOldSPTMDT.Add(new XElement(eTikiMaSanPhamTMDTName, idSPTMDT));
                foreach (string str in lsID)
                {
                    eOldSPTMDT.Add(new XElement(eTikiName, str));
                }
            }
            xDoc.Save(pathXML, SaveOptions.None);
            return string.Empty;
        }

        public string Tiki_Delete(string idSPTMDT)
        {
            if (string.IsNullOrWhiteSpace(idSPTMDT))
                return "Mã sản phẩm TMDT hoặc trong kho không đúng.";

            XElement eTiki = TiKi_GetTikiNode();
            IEnumerable<XElement> lElement = null;
            lElement = eTiki.Elements(eTikiSanPhamTMDTName).Where(e => e.Element(eTikiMaSanPhamTMDTName).Value == idSPTMDT);
            if (lElement == null || lElement.Count() == 0) // Thêm mới
            {
                return "Mã sản phẩm TMDT không tồn tại.";
            }
            else// Xóa
            {
                lElement.Remove();
            }
            xDoc.Save(pathXML, SaveOptions.None);
            return string.Empty;
        }
    }
}
