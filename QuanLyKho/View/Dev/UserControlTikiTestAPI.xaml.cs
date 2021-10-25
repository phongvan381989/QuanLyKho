using Newtonsoft.Json;
using QuanLyKho.Model.Config;
using QuanLyKho.Model.Dev;
using QuanLyKho.Model.Dev.TikiApp;
using QuanLyKho.Model.Dev.TikiApp.Config;
using QuanLyKho.ViewModel.Dev.TikiAPI;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyKho.View.Dev
{
    /// <summary>
    /// Interaction logic for UserControlTikiTestAPI.xaml
    /// </summary>
    public partial class UserControlTikiTestAPI : UserControl
    {
        public UserControlTikiTestAPI()
        {
            InitializeComponent();
        }
        string strHTTPRequest;
        string strHTTPResponse;

        private void GetTokenAuthorization_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.MainWindow.;
        }

        private void ShowHTTPRequestAndResponse()
        {
            TextBoxHTTPRequest.Text = strHTTPRequest;
            TextboxHTTPResponse.Text = strHTTPResponse;
        }
        private void BtnGetTokenAuth_Click(object sender, RoutedEventArgs e)
        {
            string clientID = "6249716820922226";
            // TUNM Testing Start
            //ttbm.Tiki_InhouseSaveAppID("6249716820922226");
            //ttbm.Tiki_InhouseAppSaveHome("6249716820922226", "https://tiki.vn/cua-hang/play-with-me");
            //ttbm.Tiki_InhouseAppSaveSecret("6249716820922226", "vSnNO7JAWDaB_DzqKngm4y5MZTFjmflG");
            //ttbm.Tiki_InhouseAppSaveAccessToken("6249716820922226", "3LWSnEvmXuDYJQWF4NoMtoimH9vYr-3tnLon56rOelY.G0BFdINaZ6L01mPP9B4fNJkQAY8frzxGx9VnbQK4LsY");
            // TUNM Testing End

            //ttbm.Tiki_SaveClientID("1234567890");
            //var client = new RestClient("https://api.tiki.vn/integration/v1/sellers/me/inventories?status=1&type=1");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("Authorization", "Bearer " + ttbm.Tiki_InhouseGetAccessToken("6249716820922226"));//5xcP33zicKodO8crb_XWXqZ1nE6aO6_LD3qghSm91h4.vRgewC765wshvhHYzgdm5lm-PEBPe1KBsCKx5V70NAo");
            //var fullUrl = client.BuildUri(request);
            ////client.
            //strHTTPRequest = fullUrl.ToString();
            //IRestResponse response = client.Execute(request);
            //strHTTPResponse = response.Content;
            //ShowHTTPRequestAndResponse();

            var client = new RestClient(TikiConstValues.cstrAuthenHTTPAddress);
            RestRequest request = new RestRequest(Method.POST);
            //request.AddHeader("Authorization", "Basic NjI0OTcxNjgyMDkyMjIyNjpDQXlUOUJ6Q3dTQXpFMkpzempud3huN3dxUnZlcDdFWg==");
            request.AddHeader("Authorization", "Basic " + CommonTikiAPI.ttbm.Tiki_GetAppCredentialBase64Format(clientID));
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", clientID);
            request.AddParameter("scope", "");
            IRestResponse response = client.Execute(request);
            HttpStatusCode st = response.StatusCode;
            if (st == HttpStatusCode.Unauthorized)
                return;
            if (st == HttpStatusCode.OK)
            {
                strHTTPResponse = response.Content;
                ShowHTTPRequestAndResponse();

                TikiAuthorization accessToken = JsonConvert.DeserializeObject<TikiAuthorization>(response.Content);
                CommonTikiAPI.ttbm.Tiki_InhouseAppSaveAccessToken(clientID, accessToken);
            }
        }

        //App Name: CallAPIFromApplicaitionCSharp
        //App ID: 6249716820922226
        //App Secret: 9IfqSeQngzps840fhdQoZ0GLC34gTPob
        //Home: https://tiki.vn/cua-hang/play-with-me
        private void BtnGetListOrder_Click(object sender, RoutedEventArgs e)
        {
            string appID = "6249716820922226";
            string contentBearer = CommonTikiAPI.ttbm.Tiki_InhouseGetAccessToken(appID);
            if(string.IsNullOrEmpty(contentBearer))
            {
                string str = CommonTikiAPI.GetDataAuthorization(appID);
                if (!string.IsNullOrEmpty(str))
                {
                    MessageBox.Show(str);
                }
                contentBearer = CommonTikiAPI.ttbm.Tiki_InhouseGetAccessToken(appID);
            }
            var client = new RestClient("https://api.tiki.vn/integration/v2/orders?page=1&limit=20&status=queueing&item_inventory_type=backorder&item_confirmation_status=waiting&filter_date_by=today");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + contentBearer);
            var fullUrl = client.BuildUri(request);
            //client.
            strHTTPRequest = fullUrl.ToString();
            IRestResponse response = client.Execute(request);
            strHTTPResponse = response.Content;
            ShowHTTPRequestAndResponse();
        }
    }
}
