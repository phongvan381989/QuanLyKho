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
    public class ModelMappingSanPhamTMDT_SanPhamKho
    {
        private const string eTikiSanPhamTMDTName = "SanPhamTMDT";
        private const string eTikiMaSanPhamTMDTName = "MaSanPhamTMDT";
        private const string eTikiName = "ID";

        public ModelMappingSanPhamTMDT_SanPhamKho()
        {
        }
        /// <summary>
        /// Khởi tạo cấu trúc node cho file
        /// </summary>
        /// <returns></returns>
        public Boolean InitializeStruct(XMLAction action)
        {
            Tiki_InitializeStruct(action);
            return true;
        }

        /// <summary>
        /// Khởi tạo cấu trúc node của TIKI
        /// </summary>
        /// <returns></returns>
        private Boolean Tiki_InitializeStruct(XMLAction action)
        {
            XElement eMap = null;
            eMap = action.xDoc.Element("MappingSanPhamTMDT_SanPhamKho");
            if (eMap == null)
            {
                // Tạo mới root
                XElement newE = new XElement("MappingSanPhamTMDT_SanPhamKho",
                new XElement("Tiki"));
                action.xDoc.Root.Add(newE);
                action.xDoc.Save(action.pathXML, SaveOptions.None);
                return true;
            }
            XElement eTiki = action.xDoc.Element("MappingSanPhamTMDT_SanPhamKho").Element("Tiki");
            if (eTiki == null)
            {
                XElement newE = new XElement("Tiki");
                eMap.Add(newE);
                action.xDoc.Save(action.pathXML, SaveOptions.None);
                return true;
            }

            return true;
        }

        /// <summary>
        /// Lấy được Tiki Node
        /// </summary>
        /// <returns></returns>
        private static XElement TiKi_GetTikiNode(XMLAction action)
        {
            return action.xDoc
                .Element("MappingSanPhamTMDT_SanPhamKho")
                .Element("Tiki");
        }

        /// <summary>
        /// Kiểm tra Id sản phẩm TMDT (MaSanPhamTMDT)đã tồn tại
        /// </summary>
        /// <param name="appID"></param>
        /// <returns></returns>
        public static Boolean Tiki_CheckMaSanPhamTMDTExist(XMLAction action, string ID)
        {
            XElement eTiki = TiKi_GetTikiNode(action);
            IEnumerable<XElement> lElement = null;
            lElement = eTiki.Elements(eTikiSanPhamTMDTName).Where(e => e.Element(eTikiMaSanPhamTMDTName).Value == ID);
            if (lElement == null || lElement.Count() == 0)
                return false;
            return true;
        }

        /// <summary>
        /// Thêm mới hoặc cập nhật mapping id sản phẩm với sản phẩm trong kho
        /// </summary>
        /// <param name="action"></param>
        /// <param name="idSPTMDT"></param>
        /// <param name="ID">id sản phẩm trong kho</param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public static string Tiki_AddOrUpdate(XMLAction action, string idSPTMDT, string ID, int quantity)
        {
            if (string.IsNullOrWhiteSpace(idSPTMDT) ||
                string.IsNullOrEmpty(ID))
                return "Mã sản phẩm TMDT hoặc trong kho không đúng.";

            XElement eTiki = TiKi_GetTikiNode(action);
            IEnumerable<XElement> lElement = null;
            lElement = eTiki.Elements(eTikiSanPhamTMDTName).Where(e => e.Element(eTikiMaSanPhamTMDTName).Value == idSPTMDT);
            if (lElement == null || lElement.Count() == 0) // Thêm mới sản phẩm trên sàn TMDT
            {
                XElement newE = new XElement(eTikiSanPhamTMDTName);
                newE.Add(new XElement(eTikiMaSanPhamTMDTName, idSPTMDT));
                newE.Add(new XElement(eTikiName, ID, new XAttribute("SoLuong", quantity.ToString())));
                eTiki.Add(newE);
            }
            else// Cập nhật sản phẩm trên sàn TMDT
            {
                XElement eOldSPTMDT = lElement.ElementAt(0);
                IEnumerable<XElement> leID = eOldSPTMDT.Elements(eTikiName).Where(e => e.Value == ID);
                if (leID == null || leID.Count() == 0)// Thêm mới sản phẩm trong kho
                {
                    eOldSPTMDT.Add(new XElement(eTikiName, ID, new XAttribute("SoLuong", quantity.ToString())));
                }
                else // Cập nhật số lượng
                {
                    leID.ElementAt(0).Attribute("SoLuong").Value = quantity.ToString();
                }
            }
            action.xDoc.Save(action.pathXML, SaveOptions.None);
            return string.Empty;
        }

        public static string Tiki_Delete(XMLAction action, string idSPTMDT)
        {
            if (string.IsNullOrWhiteSpace(idSPTMDT))
                return "Mã sản phẩm TMDT hoặc trong kho không đúng.";

            XElement eTiki = TiKi_GetTikiNode(action);
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
            action.xDoc.Save(action.pathXML, SaveOptions.None);
            return string.Empty;
        }
    }
}
