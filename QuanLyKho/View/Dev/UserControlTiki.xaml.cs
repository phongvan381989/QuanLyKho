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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var client = new RestClient("https://api.tiki.vn/integration/v1/sellers/me/inventories?status=1&type=1");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer oBFbzjNVNjZzITue5k9scD4sgkeotAdVMjwNtlpm754.qU-HcuKjwct2sNQhe6vaTMIt3a0KLd0yNYmsIx_LUFg");
            var fullUrl = client.BuildUri(request);
            //client.
            strHTTPRequest = fullUrl.ToString();
            IRestResponse response = client.Execute(request);
            strHTTPResponse = response.Content;
            ShowHTTPRequestAndResponse();
        }
    }
}
