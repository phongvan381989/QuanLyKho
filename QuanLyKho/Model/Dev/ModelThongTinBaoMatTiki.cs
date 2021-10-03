using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using QuanLyKho.Model.Dev.TikiDataClass;

namespace QuanLyKho.Model.Dev
{
    public class ModelThongTinBaoMatTiki : ModelXML
    {
        private const string eTikiAuthorizationName = "Authorization";
        private const string eTikiAccesTokenName = "AccessToken";
        private const string eTikiIDName = "ID";
        private const string eTikiIDHomeName = "Home";
        private const string eTikiSecretName = "Secret";
        public ModelThongTinBaoMatTiki()
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
        /// Lấy được Client Node theo ID
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        private XElement TiKi_GetClientNode(string clientID)
        {
            XElement eTiki = TiKi_GetTikiNode();
            IEnumerable<XElement> lElement = null;
            lElement = eTiki.Elements("Client").Where(e => e.Element(eTikiIDName).Value == clientID);
            return lElement.ElementAt(0);
        }

        /// <summary>
        /// Lưu inhouse application ID (Client ID)
        /// </summary>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public string Tiki_InhouseSaveAppID(string clientID)
        {
            XElement eTiki = TiKi_GetTikiNode();

            IEnumerable<XElement> lElement = null;
            lElement = eTiki.Elements("Client").Where(e=>e.Element(eTikiIDName).Value == clientID);
            if (lElement != null && lElement.Count() != 0) 
            {
                return string.Empty;
            }
            else // Tạo mới
            {
                XElement newE = new XElement("Client",
                                new XElement(eTikiIDName, clientID));

                eTiki.Add(newE);
                xDoc.Save(pathXML, SaveOptions.None);
            }
            return string.Empty;
        }

        /// <summary>
        /// URL của shop tương ứng với 1 inhouse app
        /// </summary>
        /// <param name="clientID">inhouse app ID</param>
        /// <param name="home">URL</param>
        /// <returns></returns>
        public string Tiki_InhouseAppSaveHome(string clientID, string home)
        {
            if (home == null)
                home = string.Empty;
            XElement eTiki = TiKi_GetTikiNode();

            IEnumerable<XElement> lElement = null;
            lElement = eTiki.Elements("Client").Where(e => e.Element(eTikiIDName).Value == clientID);

            XElement eHome = lElement.ElementAt(0).Element(eTikiIDHomeName);
            if(eHome == null)
            {
                XElement newE =  new XElement(eTikiIDHomeName, home);
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
        /// <param name="clientID">inhouse app ID</param>
        /// <returns></returns>
        public string Tiki_InhouseGetHome(string clientID)
        {
            return TiKi_GetClientNode(clientID).Element(eTikiIDHomeName).Value;
        }

        /// <summary>
        /// Secret của 1 inhouse app
        /// </summary>
        /// <param name="clientID">inhouse app ID</param>
        /// <param name="secret">Secret</param>
        /// <returns></returns>
        public string Tiki_InhouseAppSaveSecret(string clientID, string secret)
        {
            if (secret == null)
                secret = string.Empty;
            XElement eTiki = TiKi_GetTikiNode();

            IEnumerable<XElement> lElement = null;
            lElement = eTiki.Elements("Client").Where(e => e.Element(eTikiIDName).Value == clientID);

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
        /// <param name="clientID">inhouse app ID</param>
        /// <returns></returns>
        public string Tiki_InhouseGetSecret(string clientID)
        {
            return TiKi_GetClientNode(clientID).Element(eTikiSecretName).Value;
        }

        /// <summary>
        /// Access token của 1 inhouse app
        /// </summary>
        /// <param name="clientID">inhouse app ID</param>
        /// <param name="secret">Secret</param>
        /// <returns></returns>
        public string Tiki_InhouseAppSaveAccessToken(string clientID, Authorization authorization)
        {
            if (authorization == null)
                return string.Empty;
            XElement eTiki = TiKi_GetTikiNode();

            IEnumerable<XElement> lElement = null;
            lElement = eTiki.Elements("Client").Where(e => e.Element(eTikiIDName).Value == clientID);

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
        /// <param name="clientID">inhouse app ID</param>
        /// <returns></returns>
        public string Tiki_InhouseGetAccessToken(string clientID)
        {
            return TiKi_GetClientNode(clientID).Element(eTikiAuthorizationName).Element(eTikiAccesTokenName).Value;
        }

        /// <summary>
        /// 1.Given an app credentials with id = 7590139168389961, and secret = tfSl0c6VFv3fAB_z9F-m22IhEnmwq6ew
        /// 2.Join them with a semi-colon we have 7590139168389961:tfSl0c6VFv3fAB_z9F-m22IhEnmwq6ew
        /// 3.Encode the result with Base64 we have
        /// </summary>
        /// <param name="clientID">inhouse app ID</param>
        /// <returns></returns>
        public string Tiki_GetAppCredentialBase64Format(string clientID)
        {
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(clientID + ":" + Tiki_InhouseGetSecret(clientID));
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
