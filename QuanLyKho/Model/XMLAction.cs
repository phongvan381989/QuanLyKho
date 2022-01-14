using QuanLyKho.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuanLyKho.Model
{
    public class XMLAction
    {
        public XDocument xDoc;
        public string pathXML;

        public XMLAction()
        {
            xDoc = null;
            pathXML = string.Empty;
        }

        public XMLAction( string path)
        {
            pathXML = path;
            InitializeXDoc();
        }

        /// <summary>
        /// Check file xml tồn tại không? Không tồn tại tạo file mới
        /// </summary>
        /// <param name="path"></param>
        private void CheckAndCreateXML(string path, string fileName)
        {
            if (!File.Exists(path))
            {
                // Check thư mục data có tồn tại không. Nếu không tạo thư mục
                string folderPath = Path.GetDirectoryName(path);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                XDocument xmlDocument = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(fileName));

                xmlDocument.Save(path, SaveOptions.None);
            }
        }

        public void InitializeXDoc()
        {
            string fileName = Path.GetFileNameWithoutExtension(pathXML);
            if (xDoc == null)
            {
                CheckAndCreateXML(pathXML, fileName);
            }
            try
            {
                xDoc = XDocument.Load(pathXML);
            }
            catch (Exception e)
            {
                xDoc = null;
                MyLogger.GetInstance().Fatal("Không đọc được " + pathXML + ". " + e.Message);
                throw new Exception("Không đọc được " + fileName + ". " + e.Message);
            }
        }
    }
}
