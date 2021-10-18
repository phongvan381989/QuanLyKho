using Newtonsoft.Json;
using QuanLyKho.Model.Config;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.ViewModel.Dev.TikiAPI
{
    public class CommonTikiAPI
    {
        static public ModelThongTinBaoMatTiki ttbm = new ModelThongTinBaoMatTiki();
        /// <summary>
        /// Khi Access token phục vụ authorization hết hạn, gọi hàm này lấy access token mới
        /// </summary>
        /// <param name="appID"></param>
        /// <returns>empty nếu thành công. Ngược lại trả về string mô tả lỗi</returns>
        static public string GetDataAuthorization(string appID)
        {
            var client = new RestClient("https://api.tiki.vn/sc/oauth2/token");
            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Basic " + ttbm.Tiki_GetAppCredentialBase64Format(appID));
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", appID);
            request.AddParameter("scope", "");
            IRestResponse response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return "Lấy quyền truy cập shop lỗi. Vui lòng thử lại.";
            }
            Model.Dev.TikiApp.DataAuthorization accessToken = JsonConvert.DeserializeObject<Model.Dev.TikiApp.DataAuthorization>(response.Content);
            ttbm.Tiki_InhouseAppSaveAccessToken(appID, accessToken);
            return string.Empty;
        }
    }
}
