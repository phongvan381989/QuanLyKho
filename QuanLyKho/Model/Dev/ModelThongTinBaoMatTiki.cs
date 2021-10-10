﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using QuanLyKho.Model.Dev.TikiDataClass;
using QuanLyKho.Model.Config;
using System.Collections.ObjectModel;

namespace QuanLyKho.Model.Dev
{
    public class ModelThongTinBaoMatTiki : ModelXML
    {
        private const string eTikiApplicationName = "Application";
        private const string eTikiAuthorizationName = "Authorization";
        private const string eTikiAccesTokenName = "AccessToken";
        private const string eTikiIDName = "ID";
        private const string eTikiHomeName = "Home";
        private const string eTikiSecretName = "Secret";
        private const string eTikiUsingAppName = "UsingApp";
        static public XDocument xDoc = null; // Biến thao tác duy nhất cho mọi đối tượng
        public ModelThongTinBaoMatTiki()
        {
            pathXML = ((App)Application.Current).GetPathDataXMLThongTinBaoMat();
            InitializeXDoc(ref xDoc);
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
        /// Lấy được Tiki Node
        /// </summary>
        /// <returns></returns>
        private XElement TiKi_GetTikiNode()
        {
            return xDoc
                .Element("ThongTinBaoMat")
                .Element("Tiki");
        }

        /// <summary>
        /// Lấy được Application Node theo ID
        /// </summary>
        /// <param name="appID"></param>
        /// <returns></returns>
        private XElement TiKi_GetApplicationNode(string appID)
        {
            XElement eTiki = TiKi_GetTikiNode();
            IEnumerable<XElement> lElement = null;
            lElement = eTiki.Elements(eTikiApplicationName).Where(e => e.Element(eTikiIDName).Value == appID);
            if (lElement == null)
                return null;
            return lElement.ElementAt(0);
        }

        /// <summary>
        /// Kiểm tra Application ID đã tồn tại
        /// </summary>
        /// <param name="appID"></param>
        /// <returns></returns>
        public Boolean Tiki_CheckClientIDExist(string appID)
        {
            if (TiKi_GetApplicationNode(appID) == null)
                return false;
            return true;
        }

        /// <summary>
        /// Lưu inhouse application ID
        /// </summary>
        /// <param name="appID"></param>
        /// <returns></returns>
        public string Tiki_InhouseSaveAppID(string appID)
        {
            XElement eTiki = TiKi_GetTikiNode();

            IEnumerable<XElement> lElement = null;
            lElement = eTiki.Elements(eTikiApplicationName).Where(e=>e.Element(eTikiIDName).Value == appID);
            if (lElement != null && lElement.Count() != 0) 
            {
                return string.Empty;
            }
            else // Tạo mới
            {
                XElement newE = new XElement(eTikiApplicationName,
                                new XElement(eTikiIDName, appID));

                eTiki.Add(newE);
                xDoc.Save(pathXML, SaveOptions.None);
            }
            return string.Empty;
        }

        /// <summary>
        /// URL của shop tương ứng với 1 inhouse app
        /// </summary>
        /// <param name="appID">inhouse app ID</param>
        /// <param name="home">URL</param>
        /// <returns></returns>
        public string Tiki_InhouseAppSaveHome(string appID, string home)
        {
            if (home == null)
                home = string.Empty;
            XElement eTiki = TiKi_GetTikiNode();

            IEnumerable<XElement> lElement = null;
            lElement = eTiki.Elements(eTikiApplicationName).Where(e => e.Element(eTikiIDName).Value == appID);

            XElement eHome = lElement.ElementAt(0).Element(eTikiHomeName);
            if(eHome == null)
            {
                XElement newE =  new XElement(eTikiHomeName, home);
                lElement.ElementAt(0).Add(newE);
                xDoc.Save(pathXML, SaveOptions.None);
            }
            else
            {
                if (eHome.Value != home)
                {
                    eHome.Value = home;
                    xDoc.Save(pathXML, SaveOptions.None);
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Get URL của shop tương ứng với 1 inhouse app
        /// </summary>
        /// <param name="appID">inhouse app ID</param>
        /// <returns></returns>
        public string Tiki_InhouseGetHome(string appID)
        {
            return TiKi_GetApplicationNode(appID).Element(eTikiHomeName).Value;
        }

        /// <summary>
        /// Secret của 1 inhouse app
        /// </summary>
        /// <param name="appID">inhouse app ID</param>
        /// <param name="secret">Secret</param>
        /// <returns></returns>
        public string Tiki_InhouseAppSaveSecret(string appID, string secret)
        {
            if (secret == null)
                secret = string.Empty;
            XElement eTiki = TiKi_GetTikiNode();

            IEnumerable<XElement> lElement = null;
            lElement = eTiki.Elements(eTikiApplicationName).Where(e => e.Element(eTikiIDName).Value == appID);

            XElement eSecret = lElement.ElementAt(0).Element(eTikiSecretName);
            if (eSecret == null)
            {
                XElement newE = new XElement(eTikiSecretName, secret);
                lElement.ElementAt(0).Add(newE);
                xDoc.Save(pathXML, SaveOptions.None);
            }
            else
            {
                if (eSecret.Value != secret)
                {
                    eSecret.Value = secret;
                    xDoc.Save(pathXML, SaveOptions.None);
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Get secret của inhouse app
        /// </summary>
        /// <param name="appID">inhouse app ID</param>
        /// <returns></returns>
        public string Tiki_InhouseGetSecret(string appID)
        {
            return TiKi_GetApplicationNode(appID).Element(eTikiSecretName).Value;
        }

        /// <summary>
        /// Access token của 1 inhouse app
        /// </summary>
        /// <param name="appID">inhouse app ID</param>
        /// <param name="secret">Secret</param>
        /// <returns></returns>
        public string Tiki_InhouseAppSaveAccessToken(string appID, DataAuthorization authorization)
        {
            if (authorization == null)
                return string.Empty;
            XElement eTiki = TiKi_GetTikiNode();

            IEnumerable<XElement> lElement = null;
            lElement = eTiki.Elements(eTikiApplicationName).Where(e => e.Element(eTikiIDName).Value == appID);

            XElement eAuthorization = lElement.ElementAt(0).Element(eTikiAuthorizationName);
            if (eAuthorization == null)
            {
                XElement newE = new XElement(eTikiAuthorizationName, 
                                             new XElement(eTikiAccesTokenName, authorization.access_token),
                                             new XElement ("ExpiresIn", authorization.expires_in),
                                             new XElement("Scope", authorization.scope),
                                             new XElement("TokenType", authorization.token_type));
                lElement.ElementAt(0).Add(newE);
                xDoc.Save(pathXML, SaveOptions.None);
            }
            else
            {
                eAuthorization.Element(eTikiAccesTokenName).Value = authorization.access_token;
                eAuthorization.Element("ExpiresIn").Value = authorization.expires_in;
                eAuthorization.Element("Scope").Value = authorization.scope;
                eAuthorization.Element("TokenType").Value = authorization.token_type;
                xDoc.Save(pathXML, SaveOptions.None);
            }
            return string.Empty;
        }

        /// <summary>
        /// Get Access Token của shop tương ứng với 1 inhouse app
        /// </summary>
        /// <param name="appID">inhouse app ID</param>
        /// <returns></returns>
        public string Tiki_InhouseGetAccessToken(string appID)
        {
            return TiKi_GetApplicationNode(appID).Element(eTikiAuthorizationName).Element(eTikiAccesTokenName).Value;
        }

        /// <summary>
        /// 1.Given an app credentials with id = 7590139168389961, and secret = tfSl0c6VFv3fAB_z9F-m22IhEnmwq6ew
        /// 2.Join them with a semi-colon we have 7590139168389961:tfSl0c6VFv3fAB_z9F-m22IhEnmwq6ew
        /// 3.Encode the result with Base64 we have
        /// </summary>
        /// <param name="appID">inhouse app ID</param>
        /// <returns></returns>
        public string Tiki_GetAppCredentialBase64Format(string appID)
        {
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(appID + ":" + Tiki_InhouseGetSecret(appID));
            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appID"></param>
        /// <param name="home"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public string Tiki_InhouseAppAddOrUpdate(string appID, string home, string secret)
        {
            XElement eTiki = TiKi_GetTikiNode();

            IEnumerable<XElement> lElement = null;
            lElement = eTiki.Elements(eTikiApplicationName).Where(e => e.Element(eTikiIDName).Value == appID);
            if (lElement != null && lElement.Count() == 1)// Update
            {
                lElement.ElementAt(0).Element(eTikiHomeName).Value = home;
                lElement.ElementAt(0).Element(eTikiSecretName).Value = secret;
            }
            else // Tạo mới
            {
                XElement newE = new XElement(eTikiApplicationName,
                                new XElement(eTikiIDName, appID),
                                new XElement(eTikiHomeName, home),
                                new XElement(eTikiSecretName, secret),
                                new XElement(eTikiUsingAppName, DataTikiConfigApp.constNotUsingApp));
                eTiki.Add(newE);
            }
            xDoc.Save(pathXML, SaveOptions.None);
            return string.Empty;;
        }

        /// <summary>
        /// Phải check appID tồn tại trước khi gọi
        /// </summary>
        /// <param name="appID"></param>
        /// <returns></returns>
        public string Tiki_InhouseAppDelete(string appID)
        {
            xDoc
                .Element("ThongTinBaoMat")
                .Element("Tiki")
                .Elements(eTikiApplicationName).Where(e => e.Element(eTikiIDName).Value == appID).Remove();
            xDoc.Save(pathXML, SaveOptions.None);
            return string.Empty;
        }

        /// <summary>
        /// Thêm hoặc cập nhật một cấu hình ứng dụngvà lưu vào db
        /// </summary>
        public string Tiki_InhouseAppAddOrUpdate(DataTikiConfigApp tikiConfigApp)
        {
            Tiki_InhouseAppAddOrUpdate(tikiConfigApp.appID, tikiConfigApp.homeAddress, tikiConfigApp.secretAppCode);
            return string.Empty;
        }

        /// <summary>
        /// Xóa một cấu hình ứng dụng
        /// </summary>
        /// <returns></returns>
        public string Tiki_InhouseAppDelete(DataTikiConfigApp tikiConfigApp)
        {
            if (!Tiki_CheckClientIDExist(tikiConfigApp.appID))
                return "ID Ứng Dụng không tồn tại";
            Tiki_InhouseAppDelete(tikiConfigApp.appID);
            return string.Empty;
        }

        /// <summary>
        /// Convert 1 node <Application> tới đối tượng TikiConfigApp
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        DataTikiConfigApp Tiki_InhouseAppConvertXElementToOjectTikiConfigApp(XElement xElement)
        {
            if (xElement.Name != eTikiApplicationName)
                return null;
            DataTikiConfigApp tikiConfig = new DataTikiConfigApp(xElement.Element(eTikiIDName).Value,
                xElement.Element(eTikiHomeName).Value,
                xElement.Element(eTikiSecretName).Value,
                xElement.Element(eTikiUsingAppName).Value);
            return tikiConfig;
        }

        /// <summary>
        /// Get list tiki config phục vụ binding
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<DataTikiConfigApp> Tiki_InhouseAppGetListTikiConfigApp()
        {
            IEnumerable<XElement> lElement = xDoc
                .Element("ThongTinBaoMat")
                .Element("Tiki").Elements(eTikiApplicationName);
            if (lElement == null || lElement.Count() == 0)
                return null;

            ObservableCollection<DataTikiConfigApp> list = new ObservableCollection<DataTikiConfigApp>();
            foreach(XElement e in lElement)
            {
                list.Add(Tiki_InhouseAppConvertXElementToOjectTikiConfigApp(e));
            }
            return list;
        }

        /// <summary>
        /// Set sử dụng ứng dụng liên kết với cửa hàng.
        /// </summary>
        /// <param name="appID"></param>
        /// <returns></returns>
        public string Tiki_InhouseSetUsingApp(DataTikiConfigApp tikiConfigApp)
        {
            return Tiki_InhouseSetUsingApp(tikiConfigApp.appID);
        }

        /// <summary>
        /// Set sử dụng ứng dụng liên kết với cửa hàng hoặc hủy
        /// </summary>
        /// <param name="appID"></param>
        /// <returns></returns>
            public string Tiki_InhouseSetUsingApp(string appID)
        {
            if (!Tiki_CheckClientIDExist(appID))
                return "ID ứng dụng không tồn tại.";

            string home = Tiki_InhouseGetHome(appID);
            // Set không sử dụng với tất cả ứng dụng trừ ứng dụng có appID bằng tham số đầu vào
            XElement eTiki = TiKi_GetTikiNode();
            IEnumerable<XElement> lElement = null;
            lElement =  xDoc
                .Element("ThongTinBaoMat")
                .Element("Tiki")
                 .Elements(eTikiApplicationName);
            foreach(XElement e in lElement)
            {
                if (e.Element(eTikiHomeName).Value == home && e.Element(eTikiIDName).Value == appID)
                {
                    if (e.Element(eTikiUsingAppName).Value == DataTikiConfigApp.constNotUsingApp) // Set Sử dụng
                        e.Element(eTikiUsingAppName).Value = DataTikiConfigApp.constUsingApp;
                    else // Hủy sử dụng
                    {
                        e.Element(eTikiUsingAppName).Value = DataTikiConfigApp.constNotUsingApp;
                        break;
                    }
                }
                else
                    e.Element(eTikiUsingAppName).Value = DataTikiConfigApp.constNotUsingApp;
            }
            xDoc.Save(pathXML, SaveOptions.None);
            return string.Empty;
        }
    }
}
