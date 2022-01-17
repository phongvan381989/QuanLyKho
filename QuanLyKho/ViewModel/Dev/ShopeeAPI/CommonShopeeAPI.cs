using Newtonsoft.Json;
using QuanLyKho.General;
using QuanLyKho.Model;
using QuanLyKho.Model.Config;
using QuanLyKho.Model.Dev.ShopeeApp.ShopeeConfig;
using QuanLyKho.Model.Dev.ShopeeApp.ShopeeProducts;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyKho.ViewModel.Dev.ShopeeAPI
{
    public class CommonShopeeAPI
    {
        public const string cShopeeHost = "https://partner.shopeemobile.com";
        public  static XMLAction action = ((App)Application.Current).actionModelThongTinBaoMat;
        public static ModelThongTinBaoMatTiki ttbm = ((App) Application.Current).ttbm;

        /// <summary>
        /// Generate Authorization Token
        /// </summary>
        /// <param name="redirectURL"></param>
        /// <param name="partnerKey"></param>
        /// <returns></returns>
        public static string ShopeeCalToken(String redirectURL, String partnerKey)
        {
            String str = string.Empty;
            String baseStr = partnerKey + redirectURL;
            SHA256 mySHA256 = SHA256.Create();
            byte[] hashValue = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(baseStr));
            str = BitConverter.ToString(hashValue);
            return str.Replace("-", "").ToLower();
        }

        /// <summary>
        /// Generate fixed authorization URL:
        /// </summary>
        /// <returns></returns>
        public static string ShopeeGenerateAuthPartnerUrl()
        {
            DateTime start = DateTime.Now;
            long timest = ((DateTimeOffset)start).ToUnixTimeSeconds();
            string host = "https://partner.shopeemobile.com";
            string path = "/api/v2/shop/auth_partner";
            string redirect = "https://vnexpress.net/";
            long partner_id = 2002851;
            string tmp_partner_key = "19da669a5bc42ec97a43e16c041b3b364563561eabf749571cd9af0ba7821070";
            string tmp_base_string = String.Format("{0}{1}{2}", partner_id, path, timest);
            byte[] partner_key = Encoding.UTF8.GetBytes(tmp_partner_key);
            byte[] base_string = Encoding.UTF8.GetBytes(tmp_base_string);
            var hash = new HMACSHA256(partner_key);
            byte[] tmp_sign = hash.ComputeHash(base_string);
            string sign = BitConverter.ToString(tmp_sign).Replace("-", "").ToLower();
            string url = String.Format(host + path + "?partner_id={0}&timestamp={1}&sign={2}&redirect={3}", partner_id, timest, sign, redirect);
            MyLogger.GetInstance().Info(url);
            return url;
        }

        /// <summary>
        /// Nhận được access token sau khi được chủ shop ủy quyền
        /// </summary>
        /// <returns></returns>
        public static ShopeeToken ShopeeGetTokenShopLevel()
        {
            string shop_id = ttbm.Shopee_GetShopId(action);
            string partner_id = ttbm.Shopee_GetPartnerId(action);
            string partner_key = ttbm.Shopee_GetPartnerKey(action);
            string code = ttbm.Shopee_GetCode(action);

            long timest = GetTimestamp();

            string path = "/api/v2/auth/token/get";
            string tmp_base_string = String.Format("{0}{1}{2}", partner_id, path, timest);
            byte[] byte_partner_key = Encoding.UTF8.GetBytes(partner_key);
            byte[] byte_base_string = Encoding.UTF8.GetBytes(tmp_base_string);
            var hash = new HMACSHA256(byte_partner_key);
            byte[] tmp_sign = hash.ComputeHash(byte_base_string);
            string sign = BitConverter.ToString(tmp_sign).Replace("-", "").ToLower();

            string url = String.Format(cShopeeHost + path + "?partner_id={0}&timestamp={1}&sign={2}", partner_id, timest, sign);

            MyLogger.GetInstance().Info(url);

            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = @"{
            " + "\n" +
            @"    ""code"":""" + code + @""",
            " + "\n" +
            @"    ""shop_id"":" + shop_id + @",
            " + "\n" +
            @"    ""partner_id"":" + partner_id + @"
            " + "\n" +
            @"}";
            MyLogger.GetInstance().Info(body);

            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            MyLogger.InfoRestLog(client, request, response);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
            ShopeeToken token = null;
            try
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                token = JsonConvert.DeserializeObject<ShopeeToken>(response.Content, settings);

            }
            catch (Exception ex)
            {
                MyLogger.GetInstance().Warn(ex.Message);
                return null;
            }
            if (token != null)
            {
                ttbm.Shopee_UpdateAccessToken(action, token.access_token);
                ttbm.Shopee_UpdateRefreshToken(action, token.refresh_token);
            }

             return token;

        }

        /// <summary>
        /// Làm mới access token khi bị hết hạn
        /// </summary>
        /// <param name="shop_id"></param>
        /// <param name="partner_id"></param>
        /// <param name="partner_key"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public static ShopeeToken ShopeeGetRefreshTokenShopLevel()
        {
            long timest = GetTimestamp();

            string shop_id = ttbm.Shopee_GetShopId(action);
            string partner_id = ttbm.Shopee_GetPartnerId(action);
            string partner_key = ttbm.Shopee_GetPartnerKey(action);
            string refreh_token = ttbm.Shopee_GetRefreshToken(action);

            string path = "/api/v2/auth/access_token/get";
            string tmp_base_string = String.Format("{0}{1}{2}", partner_id, path, timest);
            byte[] byte_partner_key = Encoding.UTF8.GetBytes(partner_key);
            byte[] byte_base_string = Encoding.UTF8.GetBytes(tmp_base_string);
            var hash = new HMACSHA256(byte_partner_key);
            byte[] tmp_sign = hash.ComputeHash(byte_base_string);
            string sign = BitConverter.ToString(tmp_sign).Replace("-", "").ToLower();
            string url = String.Format(cShopeeHost + path + "?partner_id={0}&timestamp={1}&sign={2}", partner_id, timest, sign);
            MyLogger.GetInstance().Info(url);

            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = @"{
" + "\n" +
            @"    ""refresh_token"":""" + refreh_token + @""",
" + "\n" +
            @"    ""shop_id"":" + shop_id.ToString() + @",
" + "\n" +
            @"    ""partner_id"":" + partner_id + @"
" + "\n" +
            @"}";
            MyLogger.GetInstance().Info(body);

            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                MyLogger.InfoRestLog(client, request, response);
                return null;
            }
            MyLogger.GetInstance().Info(response.Content);
            ShopeeToken token = null;
            try
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                token = JsonConvert.DeserializeObject<ShopeeToken>(response.Content, settings);

            }
            catch (Exception ex)
            {
                MyLogger.GetInstance().Warn(ex.Message);
                return null;
            }
            if (token != null)
            {
                ttbm.Shopee_UpdateAccessToken(action, token.access_token);
                ttbm.Shopee_UpdateRefreshToken(action, token.refresh_token);
            }
            return token;
        }

        public static long GetTimestamp()
        {
            DateTime start = DateTime.Now;
            long timest = ((DateTimeOffset)start).ToUnixTimeSeconds();
            return timest;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="partner_id"></param>
        /// <param name="access_token"></param>
        /// <param name="shop_id"></param>
        /// <param name="partner_key"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GenerateURLShopeeAPI(string path, List<DevNameValuePair> ls)
        {
            long timest = GetTimestamp();
            string partner_id = ttbm.Shopee_GetPartnerId(action);
            string access_token = ttbm.Shopee_GetAccessToken(action);
            string shop_id = ttbm.Shopee_GetShopId(action);
            string partner_key = ttbm.Shopee_GetPartnerKey(action);

            string tmp_base_string = String.Format("{0}{1}{2}{3}{4}", partner_id, path, timest, access_token, shop_id);
            byte[] byte_partner_key = Encoding.UTF8.GetBytes(partner_key);
            byte[] byte_base_string = Encoding.UTF8.GetBytes(tmp_base_string);
            var hash = new HMACSHA256(byte_partner_key);
            byte[] tmp_sign = hash.ComputeHash(byte_base_string);
            string sign = BitConverter.ToString(tmp_sign).Replace("-", "").ToLower();
            string url = String.Format(CommonShopeeAPI.cShopeeHost + path + "?partner_id={0}&timestamp={1}&sign={2}&shop_id={3}&access_token={4}" + DevNameValuePair.GetQueryStringWithAndPrefix(ls), partner_id, timest, sign, shop_id, access_token);
            return url;
        }

        public static IRestResponse ShopeeGetMethod(string path, List<DevNameValuePair> ls)
        {
            string url = GenerateURLShopeeAPI(path, ls);
            RestClient client = new RestClient(url);
            client.Timeout = -1;
            RestRequest request = new RestRequest(Method.GET);
            IRestResponse response = null;
            try
            {
                response = client.Execute(request);
                MyLogger.InfoRestLog(client, request, response);
            }
            catch (Exception ex)
            {
                MyLogger.GetInstance().Warn(ex.Message);
                return null;
            }
            if (response == null)
                return response;


            if (response.StatusCode == HttpStatusCode.Forbidden) // Làm mới access token
            {
                ShopeeGetRefreshTokenShopLevel();

                url = GenerateURLShopeeAPI(path, ls);
                client = new RestClient(url);
                client.Timeout = -1;
                request = new RestRequest(Method.GET);

                try
                {
                    response = client.Execute(request);
                    MyLogger.InfoRestLog(client, request, response);
                }
                catch (Exception ex)
                {
                    MyLogger.GetInstance().Warn(ex.Message);
                }
            }
            return response;
        }

        public static string ShopeeGetShopInfo()
        {
            string json = string.Empty;
            string path = "/api/v2/shop/get_shop_info";

            IRestResponse response = ShopeeGetMethod(path, null);
            if (response == null)
                return json;

            if (response.StatusCode == HttpStatusCode.OK)
                json = response.Content;

            return json;
        }
        #region Product

        /// <summary>
        /// 
        /// </summary>
        /// <param name="update_time_from"> -1 để bỏ qua</param>
        /// <param name="update_time_to"> -1 để bỏ qua</param>
        /// <returns></returns>
        public static string ShopeeProductGetItemList(long update_time_from, long update_time_to,
            int offset, int page_size,
            List<ShopeeItemStatus> lsShopeeItemStatus)
        {
            string json = string.Empty;
            string path = "/api/v2/product/get_item_list";

            List<DevNameValuePair> ls = new List<DevNameValuePair>();
            ls.Add(new DevNameValuePair("offset", offset.ToString()));
            ls.Add(new DevNameValuePair("page_size", page_size.ToString()));
            if(update_time_from > 0 && update_time_to > 0 && update_time_to > update_time_from)
            {
                ls.Add(new DevNameValuePair("update_time_from", update_time_from.ToString()));
                ls.Add(new DevNameValuePair("update_time_to", update_time_to.ToString()));
            }
            foreach(ShopeeItemStatus item in lsShopeeItemStatus)
            {
                ls.Add(new DevNameValuePair("item_status", item.GetString()));
            }

            IRestResponse response = ShopeeGetMethod(path, ls);
            if (response == null)
                return json;

            if (response.StatusCode == HttpStatusCode.OK)
                json = response.Content;

            return json;
        }

        public static string ShopeeProductGetItemBaseInfo()
        {
            string json = string.Empty;
            string path = "/api/v2/product/get_item_base_info";

            List<DevNameValuePair> ls = new List<DevNameValuePair>();
            ls.Add(new DevNameValuePair("item_id_list", "11241094731,10641420129"));
            ls.Add(new DevNameValuePair("need_tax_info", "false"));
            ls.Add(new DevNameValuePair("need_complaint_policy", "false"));

            IRestResponse response = ShopeeGetMethod(path, ls);
            if (response == null)
                return json;

            if (response.StatusCode == HttpStatusCode.OK)
                json = response.Content;

            return json;
        }
        #endregion
    }
}
