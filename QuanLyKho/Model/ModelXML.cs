using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuanLyKho.Model
{
    public class ModelXML
    {
        public string pathXML;

        public ModelXML()
        {
            pathXML = string.Empty;
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

        public void InitializeXDoc(ref XDocument xDoc)
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
                throw new Exception("Không đọc được " + fileName + ". " + e.Message);
            }
        }
    }
}
