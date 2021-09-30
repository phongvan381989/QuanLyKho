using QuanLyKho.Model.Dev;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for UserControlTiki.xaml
    /// </summary>
    public partial class UserControlTiki : UserControl
    {
        public UserControlTiki()
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
            ModelThongTinBaoMat ttbm = new ModelThongTinBaoMat();
            //ttbm.Tiki_SaveClientID("1234567890");
            //var client = new RestClient("https://api.tiki.vn/integration/v1/sellers/me/inventories?status=1&type=1");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("Authorization", "Bearer 5xcP33zicKodO8crb_XWXqZ1nE6aO6_LD3qghSm91h4.vRgewC765wshvhHYzgdm5lm-PEBPe1KBsCKx5V70NAo");
            //var fullUrl = client.BuildUri(request);
            ////client.
            //strHTTPRequest = fullUrl.ToString();
            //IRestResponse response = client.Execute(request);
            //strHTTPResponse = response.Content;
            //ShowHTTPRequestAndResponse();

            //var client = new RestClient("https://api.tiki.vn/sc/oauth2/token");
            //RestRequest request = new RestRequest(Method.POST);
            //request.AddHeader("Authorization", "Basic NjI0OTcxNjgyMDkyMjIyNjpDQXlUOUJ6Q3dTQXpFMkpzempud3huN3dxUnZlcDdFWg==");
            //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            //request.AddParameter("grant_type", "client_credentials");
            //request.AddParameter("client_id", "6249716820922226");
            //request.AddParameter("scope", "");
            //IRestResponse response = client.Execute(request);
            //strHTTPResponse = response.Content;
            //ShowHTTPRequestAndResponse();
        }
    }
}
