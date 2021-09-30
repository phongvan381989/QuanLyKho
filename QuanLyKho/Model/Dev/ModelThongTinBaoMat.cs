using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace QuanLyKho.Model.Dev
{
    public class ModelThongTinBaoMat : ModelXML
    {
        public ModelThongTinBaoMat()
        {
            pathXML = ((App)Application.Current).GetPathDataXMLThongTinBaoMat();
            InitializeXDoc();
            InitializeStruct();
        }

        public Boolean SaveAccessToken()
        {
            var client = new RestClient("https://api.tiki.vn/sc/oauth2/token");
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Basic NjI0OTcxNjgyMDkyMjIyNjpDQXlUOUJ6Q3dTQXpFMkpzempud3huN3dxUnZlcDdFWg==");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", "6249716820922226");
            request.AddParameter("scope", "");
            IRestResponse response = client.Execute(request);
            //strHTTPResponse = response.Content;
            //ShowHTTPRequestAndResponse();
            return true;
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
            XElement eTTBM = null;
            eTTBM = xDoc.Element("ThongTinBaoMat");
            if(eTTBM == null)
            {
                // Tạo mới root
                XElement newE = new XElement("ThongTinBaoMat",
                new XElement("Tiki"));
                xDoc.Root.Add(newE);
                xDoc.Save(pathXML, SaveOptions.None);
                return true;
            }
            XElement eTiki = xDoc.Element("ThongTinBaoMat").Element("Tiki");
            if (eTiki == null)
            {
                XElement newE = new XElement("Tiki");
                eTTBM.Add(newE);
                xDoc.Save(pathXML, SaveOptions.None);
                return true;
            }

                return true;
        }
        /// <summary>
        /// Lưu client ID
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public Boolean Tiki_SaveClientID(string clientID)
        {
            XElement le = null;
            // Tìm mã sản phẩm đã tồn tại
            XElement le12 = xDoc
                .Element("ThongTinBaoMat");
            if (le12 == null)
                return false;
            le = xDoc
                .Element("ThongTinBaoMat")
                .Element("Tiki")
                .Element("ClientID");
            if (le != null) // Cập nhật đã tồn tại
            {
                le.Value = clientID;
            }
            else // Tạo mới
            {
                XElement newE = new XElement("ThongTinBaoMat",
                    new XElement("Tiki"),
                    new XElement("ClientID", clientID));

                xDoc.Add(newE);
                xDoc.Save(pathXML, SaveOptions.None);
            }
            return true;
        }
    }
}
