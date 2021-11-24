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
    //<? xml version="1.0" encoding="utf-8" standalone="yes"?>
    //<MappingSanPhamTMDT_SanPhamKho>
    //  <Tiki>
    //    <SanPhamTMDT>
    //      <MaSanPhamTMDT>135614783</MaSanPhamTMDT>
    //      <ID Name = "Maru" Quantity="1">12346</ID>
    //      <ID Name = "Miu miu" Quantity="2">12347</ID>
    //      <ID Name = "Miu bé nhỏ" Quantity="3">12348</ID>
    //    </SanPhamTMDT>
    //    <SanPhamTMDT>
    //      <MaSanPhamTMDT>135613016</MaSanPhamTMDT>
    //      <ID Name = "Taku cậu bé mộng mơ quá" Quantity="2">1234567</ID>
    //      <ID Name = "Taku cậu bé mộng mơ quá_0" Quantity="2">1234567_0</ID>
    //      <ID Name = "Taku cậu bé mộng mơ quá_1" Quantity="22">1234567_1</ID>
    //    </SanPhamTMDT>
    //  </Tiki>
    //</MappingSanPhamTMDT_SanPhamKho>
    /// </summary>
    public class ModelMappingSanPhamTMDT_SanPhamKho
    {
        private const string eTikiSanPhamTMDTName = "SanPhamTMDT";
        private const string eTikiMaSanPhamTMDTName = "MaSanPhamTMDT";
        private const string eTikiName = "ID";

        public string code { get; set; }
        public string quantity { get; set; }
        public string name { get; set; }
        public string position { get; set; }

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
        /// <param name="id">id sản phẩm trong kho</param>
        /// <param name="name">tên sản phẩm trong kho</param>
        /// <param name="quantity"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static string Tiki_AddOrUpdate(XMLAction action, string idSPTMDT, string id, string name, string quantity, string position)
        {
            if (string.IsNullOrWhiteSpace(idSPTMDT) ||
                string.IsNullOrEmpty(id))
                return "Mã sản phẩm TMDT hoặc trong kho không đúng.";

            XElement eTiki = TiKi_GetTikiNode(action);
            IEnumerable<XElement> lElement = null;
            lElement = eTiki.Elements(eTikiSanPhamTMDTName).Where(e => e.Element(eTikiMaSanPhamTMDTName).Value == idSPTMDT);
            if (lElement == null || lElement.Count() == 0) // Thêm mới sản phẩm trên sàn TMDT
            {
                XElement newE = new XElement(eTikiSanPhamTMDTName);
                newE.Add(new XElement(eTikiMaSanPhamTMDTName, idSPTMDT));
                newE.Add(new XElement(eTikiName, id,
                         new XAttribute("Name", name),
                         new XAttribute("Quantity", quantity.ToString()),
                         new XAttribute("Position", position)));
                eTiki.Add(newE);
            }
            else// Cập nhật sản phẩm trên sàn TMDT
            {
                XElement eOldSPTMDT = lElement.ElementAt(0);
                IEnumerable<XElement> leID = eOldSPTMDT.Elements(eTikiName).Where(e => e.Value == id);
                if (leID == null || leID.Count() == 0)// Thêm mới sản phẩm trong kho
                {
                    eOldSPTMDT.Add(
                        new XElement(eTikiName, id, new XAttribute("Name", name),
                         new XAttribute("Quantity", quantity.ToString()),
                         new XAttribute("Position", position)));
                }
                else // Cập nhật
                {
                    leID.ElementAt(0).Attribute("Name").Value = name;
                    leID.ElementAt(0).Attribute("Quantity").Value = quantity.ToString();
                    leID.ElementAt(0).Attribute("Position").Value = position;
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

        /// <summary>
        /// Lấy danh sách đối tượng từ ID sản phẩm trên shop TMDT
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<ModelMappingSanPhamTMDT_SanPhamKho> GetListModelMappingSanPhamTMDT_SanPhamKhoFromID(XMLAction action, string idSPTMDT)
        {
            List<ModelMappingSanPhamTMDT_SanPhamKho> ls = new List<ModelMappingSanPhamTMDT_SanPhamKho>();
            if (string.IsNullOrEmpty(idSPTMDT))
                return ls;
            XElement eTiki = TiKi_GetTikiNode(action);
            IEnumerable<XElement> lElement = null;
            lElement = eTiki.Elements(eTikiSanPhamTMDTName).Where(e => e.Element(eTikiMaSanPhamTMDTName).Value == idSPTMDT);
            if(lElement != null && lElement.Count() == 1)
            {
                foreach(XElement e in lElement.Elements(eTikiName))
                {

                    ModelMappingSanPhamTMDT_SanPhamKho obj = new ModelMappingSanPhamTMDT_SanPhamKho();
                    obj.code = e.Value;
                    obj.quantity = e.Attribute("Quantity").Value;
                    obj.name = e.Attribute("Name").Value;
                    obj.position = e.Attribute("Position").Value;
                    ls.Add(obj);
                }
            }
            return ls;
        }
    }
}
