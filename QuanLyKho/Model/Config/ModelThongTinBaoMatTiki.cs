using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using QuanLyKho.Model.Dev.TikiApp;
using QuanLyKho.Model.Config;
using System.Collections.ObjectModel;
using QuanLyKho.Model.Dev.TikiApp.Config;
using QuanLyKho.General;

namespace QuanLyKho.Model.Config
{
    public class ModelThongTinBaoMatTiki// : ModelXML
    {
        private const string eTikiApplicationName = "Application";
        private const string eTikiAuthorizationName = "Authorization";
        private const string eTikiAccesTokenName = "AccessToken";
        private const string eTikiExpiresInName = "ExpiresIn";
        private const string eTikiScopeName = "Scope";
        private const string eTikiTokenTypeName = "TokenType";
        private const string eTikiIDName = "ID";
        private const string eTikiHomeName = "Home";
        private const string eTikiSecretName = "Secret";
        private const string eTikiUsingAppName = "UsingApp";
        //static public XDocument xDoc = null; // Biến thao tác xml data duy nhất cho mọi đối tượng
        public ModelThongTinBaoMatTiki()
        {
            //pathXML = ((App)Application.Current).GetPathDataXMLThongTinBaoMat();
            //InitializeXDoc(ref xDoc);
            //InitializeStruct();
        }

        public Boolean SaveAccessToken()
        {
            var client = new RestClient(TikiConstValues.cstrAuthenHTTPAddress);//("https://api.tiki.vn/sc/oauth2/token");
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Basic NjI0OTcxNjgyMDkyMjIyNjpDQXlUOUJ6Q3dTQXpFMkpzempud3huN3dxUnZlcDdFWg==");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", "6249716820922226");
            request.AddParameter("scope", "");
            try
            {
                IRestResponse response = client.Execute(request);
            }
            catch(Exception ex)
            {
                MyLogger.GetInstance().Warn(ex.Message);
            }
            return true;
        }

        /// <summary>
        /// Khởi tạo cấu trúc node cho file
        /// </summary>
        /// <returns></returns>
        private Boolean InitializeStruct(XMLAction action)
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
            XElement eTTBM = null;
            eTTBM = action.xDoc.Element("ThongTinBaoMat");
            if(eTTBM == null)
            {
                // Tạo mới root
                XElement newE = new XElement("ThongTinBaoMat",
                new XElement("Tiki"));
                action.xDoc.Root.Add(newE);
                action.xDoc.Save(action.pathXML, SaveOptions.None);
                return true;
            }
            XElement eTiki = action.xDoc.Element("ThongTinBaoMat").Element("Tiki");
            if (eTiki == null)
            {
                XElement newE = new XElement("Tiki");
                eTTBM.Add(newE);
                action.xDoc.Save(action.pathXML, SaveOptions.None);
                return true;
            }

            return true;
        }

        /// <summary>
        /// Lấy được Tiki Node
        /// </summary>
        /// <returns></returns>
        private XElement TiKi_GetTikiNode(XMLAction action)
        {
            return action.xDoc
                .Element("ThongTinBaoMat")
                .Element("Tiki");
        }

        /// <summary>
        /// Lấy được Application Node theo ID
        /// </summary>
        /// <param name="appID"></param>
        /// <returns></returns>
        private XElement TiKi_GetApplicationNode(XMLAction action, string appID)
        {
            XElement eTiki = TiKi_GetTikiNode(action);
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
        public Boolean Tiki_CheckClientIDExist(XMLAction action, string appID)
        {
            if (TiKi_GetApplicationNode(action, appID) == null)
                return false;
            return true;
        }

        /// <summary>
        /// Lưu inhouse application ID
        /// </summary>
        /// <param name="appID"></param>
        /// <returns></returns>
        public string Tiki_InhouseSaveAppID(XMLAction action, string appID)
        {
            XElement eTiki = TiKi_GetTikiNode(action);

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
                action.xDoc.Save(action.pathXML, SaveOptions.None);
            }
            return string.Empty;
        }

        /// <summary>
        /// URL của shop tương ứng với 1 inhouse app
        /// </summary>
        /// <param name="appID">inhouse app ID</param>
        /// <param name="home">URL</param>
        /// <returns></returns>
        public string Tiki_InhouseAppSaveHome(XMLAction action, string appID, string home)
        {
            if (home == null)
                home = string.Empty;
            XElement eTiki = TiKi_GetTikiNode(action);

            IEnumerable<XElement> lElement = null;
            lElement = eTiki.Elements(eTikiApplicationName).Where(e => e.Element(eTikiIDName).Value == appID);

            XElement eHome = lElement.ElementAt(0).Element(eTikiHomeName);
            if(eHome == null)
            {
                XElement newE =  new XElement(eTikiHomeName, home);
                lElement.ElementAt(0).Add(newE);
                action.xDoc.Save(action.pathXML, SaveOptions.None);
            }
            else
            {
                if (eHome.Value != home)
                {
                    eHome.Value = home;
                    action.xDoc.Save(action.pathXML, SaveOptions.None);
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Get URL của shop tương ứng với 1 inhouse app
        /// </summary>
        /// <param name="appID">inhouse app ID</param>
        /// <returns></returns>
        public string Tiki_InhouseGetHome(XMLAction action, string appID)
        {
            return TiKi_GetApplicationNode(action, appID).Element(eTikiHomeName).Value;
        }

        /// <summary>
        /// Secret của 1 inhouse app
        /// </summary>
        /// <param name="appID">inhouse app ID</param>
        /// <param name="secret">Secret</param>
        /// <returns></returns>
        public string Tiki_InhouseAppSaveSecret(XMLAction action, string appID, string secret)
        {
            if (secret == null)
                secret = string.Empty;
            XElement eTiki = TiKi_GetTikiNode(action);

            IEnumerable<XElement> lElement = null;
            lElement = eTiki.Elements(eTikiApplicationName).Where(e => e.Element(eTikiIDName).Value == appID);

            XElement eSecret = lElement.ElementAt(0).Element(eTikiSecretName);
            if (eSecret == null)
            {
                XElement newE = new XElement(eTikiSecretName, secret);
                lElement.ElementAt(0).Add(newE);
                action.xDoc.Save(action.pathXML, SaveOptions.None);
            }
            else
            {
                if (eSecret.Value != secret)
                {
                    eSecret.Value = secret;
                    action.xDoc.Save(action.pathXML, SaveOptions.None);
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Get secret của inhouse app
        /// </summary>
        /// <param name="appID">inhouse app ID</param>
        /// <returns></returns>
        public string Tiki_InhouseGetSecret(XMLAction action, string appID)
        {
            return TiKi_GetApplicationNode(action, appID).Element(eTikiSecretName).Value;
        }

        /// <summary>
        /// Access token của 1 inhouse app
        /// </summary>
        /// <param name="appID">inhouse app ID</param>
        /// <param name="secret">Secret</param>
        /// <returns></returns>
        public string Tiki_InhouseAppSaveAccessToken(XMLAction action, string appID, TikiAuthorization authorization)
        {
            if (authorization == null)
                return string.Empty;
            XElement appNode = TiKi_GetApplicationNode(action, appID);
            XElement eAuthorization = appNode.Element(eTikiAuthorizationName);
            if (eAuthorization == null)
            {
                XElement newE = new XElement(eTikiAuthorizationName, 
                                             new XElement(eTikiAccesTokenName, authorization.access_token),
                                             new XElement ("ExpiresIn", authorization.expires_in),
                                             new XElement("Scope", authorization.scope),
                                             new XElement("TokenType", authorization.token_type));
                appNode.Add(newE);
            }
            else
            {
                eAuthorization.Element(eTikiAccesTokenName).Value = authorization.access_token;
                eAuthorization.Element("ExpiresIn").Value = authorization.expires_in;
                eAuthorization.Element("Scope").Value = authorization.scope;
                eAuthorization.Element("TokenType").Value = authorization.token_type;
            }
            action.xDoc.Save(action.pathXML, SaveOptions.None);
            return string.Empty;
        }

        /// <summary>
        /// Get Access Token của shop tương ứng với 1 inhouse app
        /// </summary>
        /// <param name="appID">inhouse app ID</param>
        /// <returns></returns>
        public string Tiki_InhouseGetAccessToken(XMLAction action, string appID)
        {
            XElement authoNode = TiKi_GetApplicationNode(action, appID).Element(eTikiAuthorizationName);
            if (authoNode == null)
                return string.Empty;
            XElement accessTokenNode = authoNode.Element(eTikiAccesTokenName);
            if (accessTokenNode == null)
                return string.Empty;

            return accessTokenNode.Value;
        }

        /// <summary>
        /// 1.Given an app credentials with id = 7590139168389961, and secret = tfSl0c6VFv3fAB_z9F-m22IhEnmwq6ew
        /// 2.Join them with a semi-colon we have 7590139168389961:tfSl0c6VFv3fAB_z9F-m22IhEnmwq6ew
        /// 3.Encode the result with Base64 we have
        /// </summary>
        /// <param name="appID">inhouse app ID</param>
        /// <returns></returns>
        public string Tiki_GetAppCredentialBase64Format(XMLAction action, string appID)
        {
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(appID + ":" + Tiki_InhouseGetSecret(action, appID));
            return Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appID"></param>
        /// <param name="home"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public string Tiki_InhouseAppAddOrUpdate(XMLAction action, string appID, string home, string secret)
        {
            XElement eTiki = TiKi_GetTikiNode(action);

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
                                new XElement(eTikiUsingAppName, TikiConfigApp.constNotUsingApp),
                                new XElement(eTikiAuthorizationName,
                                             new XElement(eTikiAccesTokenName),
                                             new XElement("ExpiresIn"),
                                             new XElement("Scope"),
                                             new XElement("TokenType")));
                eTiki.Add(newE);
            }
            action.xDoc.Save(action.pathXML, SaveOptions.None);
            return string.Empty;;
        }

        /// <summary>
        /// Phải check appID tồn tại trước khi gọi
        /// </summary>
        /// <param name="appID"></param>
        /// <returns></returns>
        public string Tiki_InhouseAppDelete(XMLAction action, string appID)
        {
            action.xDoc
                .Element("ThongTinBaoMat")
                .Element("Tiki")
                .Elements(eTikiApplicationName).Where(e => e.Element(eTikiIDName).Value == appID).Remove();
            action.xDoc.Save(action.pathXML, SaveOptions.None);
            return string.Empty;
        }

        /// <summary>
        /// Thêm hoặc cập nhật một cấu hình ứng dụngvà lưu vào db
        /// </summary>
        public string Tiki_InhouseAppAddOrUpdate(XMLAction action, TikiConfigApp tikiConfigApp)
        {
            Tiki_InhouseAppAddOrUpdate(action, tikiConfigApp.appID, tikiConfigApp.homeAddress, tikiConfigApp.secretAppCode);
            return string.Empty;
        }

        /// <summary>
        /// Xóa một cấu hình ứng dụng
        /// </summary>
        /// <returns></returns>
        public string Tiki_InhouseAppDelete(XMLAction action, TikiConfigApp tikiConfigApp)
        {
            if (!Tiki_CheckClientIDExist(action, tikiConfigApp.appID))
                return "ID Ứng Dụng không tồn tại";
            Tiki_InhouseAppDelete(action, tikiConfigApp.appID);
            return string.Empty;
        }

        /// <summary>
        /// Convert 1 node <Application> tới đối tượng TikiConfigApp
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        TikiConfigApp Tiki_InhouseAppConvertXElementToOjectTikiConfigApp(XElement xElement)
        {
            if (xElement.Name != eTikiApplicationName)
                return null;

            TikiConfigApp tikiConfig = new TikiConfigApp(xElement.Element(eTikiIDName).Value,
                xElement.Element(eTikiHomeName).Value,
                xElement.Element(eTikiSecretName).Value,
                xElement.Element(eTikiUsingAppName).Value,
                new TikiAuthorization(
                    xElement.Element(eTikiAuthorizationName).Element(eTikiAccesTokenName).Value,
                    xElement.Element(eTikiAuthorizationName).Element(eTikiExpiresInName).Value,
                    xElement.Element(eTikiAuthorizationName).Element(eTikiScopeName).Value,
                    xElement.Element(eTikiAuthorizationName).Element(eTikiTokenTypeName).Value
                    ));
            return tikiConfig;
        }

        /// <summary>
        /// Get list tiki config phục vụ binding
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<TikiConfigApp> Tiki_InhouseAppGetListTikiConfigApp(XMLAction action)
        {
            IEnumerable<XElement> lElement = action.xDoc
                .Element("ThongTinBaoMat")
                .Element("Tiki").Elements(eTikiApplicationName);
            if (lElement == null || lElement.Count() == 0)
                return null;

            ObservableCollection<TikiConfigApp> list = new ObservableCollection<TikiConfigApp>();
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
        public string Tiki_InhouseSetUsingApp(XMLAction action, TikiConfigApp tikiConfigApp)
        {
            return Tiki_InhouseSetUsingApp(action, tikiConfigApp.appID);
        }

        /// <summary>
        /// Set sử dụng ứng dụng liên kết với cửa hàng hoặc hủy
        /// </summary>
        /// <param name="appID"></param>
        /// <returns></returns>
        public string Tiki_InhouseSetUsingApp(XMLAction action, string appID)
        {
            if (!Tiki_CheckClientIDExist(action, appID))
                return "ID ứng dụng không tồn tại.";

            string home = Tiki_InhouseGetHome(action, appID);
            // Set không sử dụng với tất cả ứng dụng trừ ứng dụng có appID bằng tham số đầu vào
            XElement eTiki = TiKi_GetTikiNode(action);
            IEnumerable<XElement> lElement = null;
            lElement = action.xDoc
                .Element("ThongTinBaoMat")
                .Element("Tiki")
                .Elements(eTikiApplicationName);
            foreach(XElement e in lElement)
            {
                if (e.Element(eTikiHomeName).Value == home && e.Element(eTikiIDName).Value == appID)
                {
                    if (e.Element(eTikiUsingAppName).Value == TikiConfigApp.constNotUsingApp) // Set Sử dụng
                        e.Element(eTikiUsingAppName).Value = TikiConfigApp.constUsingApp;
                    else // Hủy sử dụng
                    {
                        e.Element(eTikiUsingAppName).Value = TikiConfigApp.constNotUsingApp;
                        break;
                    }
                }
                else if(e.Element(eTikiHomeName).Value == home)
                    e.Element(eTikiUsingAppName).Value = TikiConfigApp.constNotUsingApp;
            }
            action.xDoc.Save(action.pathXML, SaveOptions.None);
            return string.Empty;
        }

        /// <summary>
        /// Lấy được danh sách ứng dụng đang được sử dụng
        /// </summary>
        /// <returns> Trả về null nếu không có</returns>
        public List<TikiConfigApp> Tiki_InhouseAppGetListUsingApp(XMLAction action)
        {
            IEnumerable<XElement> lElement = action.xDoc
                .Element("ThongTinBaoMat")
                .Element("Tiki").Elements(eTikiApplicationName);
            if (lElement == null || lElement.Count() == 0)
                return null;

            List<TikiConfigApp> list = new List<TikiConfigApp>();
            foreach (XElement e in lElement)
            {
                if(e.Element(eTikiUsingAppName).Value == TikiConfigApp.constUsingApp)
                list.Add(Tiki_InhouseAppConvertXElementToOjectTikiConfigApp(e));
            }
            return list;
        }
    }
}
