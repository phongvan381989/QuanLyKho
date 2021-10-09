using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using QuanLyKho.Model.Dev.TikiDataClass;
using System.Collections.ObjectModel;

namespace QuanLyKho.Model.Dev
{
    public class ModelThongTinBaoMatTiki : ModelXML
    {
        private const string eTikiAuthorizationName = "Authorization";
        private const string eTikiAccesTokenName = "AccessToken";
        private const string eTikiIDName = "ID";
        private const string eTikiHomeName = "Home";
        private const string eTikiSecretName = "Secret";
        public string appID { get; set; }
        public string homeAddress { get; set; }
        public string secretAppCode { get; set; }
        public ModelThongTinBaoMatTiki()
        {
            pathXML = ((App)Application.Current).GetPathDataXMLThongTinBaoMat();
            InitializeXDoc();
            InitializeStruct();
            appID = string.Empty;
            homeAddress = string.Empty;
            secretAppCode = string.Empty;
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
        private XElement TiKi_GetClientNode(string appID)
        {
            XElement eTiki = TiKi_GetTikiNode();
            IEnumerable<XElement> lElement = null;
            lElement = eTiki.Elements("Application").Where(e => e.Element(eTikiIDName).Value == appID);
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
            if (TiKi_GetClientNode(appID) == null)
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
            lElement = eTiki.Elements("Application").Where(e=>e.Element(eTikiIDName).Value == appID);
            if (lElement != null && lElement.Count() != 0) 
            {
                return string.Empty;
            }
            else // Tạo mới
            {
                XElement newE = new XElement("Application",
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
            lElement = eTiki.Elements("Application").Where(e => e.Element(eTikiIDName).Value == appID);

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
            return TiKi_GetClientNode(appID).Element(eTikiHomeName).Value;
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
            lElement = eTiki.Elements("Application").Where(e => e.Element(eTikiIDName).Value == appID);

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
            return TiKi_GetClientNode(appID).Element(eTikiSecretName).Value;
        }

        /// <summary>
        /// Access token của 1 inhouse app
        /// </summary>
        /// <param name="appID">inhouse app ID</param>
        /// <param name="secret">Secret</param>
        /// <returns></returns>
        public string Tiki_InhouseAppSaveAccessToken(string appID, Authorization authorization)
        {
            if (authorization == null)
                return string.Empty;
            XElement eTiki = TiKi_GetTikiNode();

            IEnumerable<XElement> lElement = null;
            lElement = eTiki.Elements("Application").Where(e => e.Element(eTikiIDName).Value == appID);

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
            return TiKi_GetClientNode(appID).Element(eTikiAuthorizationName).Element(eTikiAccesTokenName).Value;
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
            lElement = eTiki.Elements("Application").Where(e => e.Element(eTikiIDName).Value == appID);
            if (lElement != null && lElement.Count() != 0)// Update
            {
                XElement newEHome = new XElement(eTikiHomeName, home);
                XElement newESecret = new XElement(eTikiSecretName, secret);
                lElement.ElementAt(0).Add(newEHome);
                lElement.ElementAt(0).Add(newESecret);
            }
            else // Tạo mới
            {
                XElement newE = new XElement("Application",
                                new XElement(eTikiIDName, appID),
                                new XElement(eTikiHomeName, home),
                                new XElement(eTikiSecretName, secret));
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
                .Elements("Application").Where(e => e.Element(eTikiIDName).Value == appID).Remove();
            xDoc.Save(pathXML, SaveOptions.None);
            return string.Empty;
        }

        /// <summary>
        /// Thêm hoặc cập nhật một cấu hình ứng dụngvà lưu vào db
        /// </summary>
        public string Add()
        {
            Tiki_InhouseAppAddOrUpdate(appID, homeAddress, secretAppCode);
            return string.Empty;
        }

        /// <summary>
        /// Xóa một cấu hình ứng dụng
        /// </summary>
        /// <returns></returns>
        public string Delete()
        {
            if (!Tiki_CheckClientIDExist(appID))
                return "ID Ứng Dụng không tồn tại";

            Tiki_InhouseAppDelete(appID);
            return string.Empty;
        }

        static public ObservableCollection<ModelThongTinBaoMatTiki> GetListTikiConfigApp()
        {
            ObservableCollection<ModelThongTinBaoMatTiki> list = null;
            return list;
        }
    }
}
