using Newtonsoft.Json;
using QuanLyKho.General;
using QuanLyKho.Model;
using QuanLyKho.Model.Config;
using QuanLyKho.Model.Dev.ShopeeApp.ShopeeConfig;
using QuanLyKho.Model.Dev.ShopeeApp.ShopeeOrder;
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
            string tmp_partner_key = ttbm.Shopee_GetPartnerKey(action);
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

            long timest = Common.GetTimestampNow();

            string path = "/api/v2/auth/token/get";
            string tmp_base_string = String.Format("{0}{1}{2}", partner_id, path, timest);
            byte[] byte_partner_key = Encoding.UTF8.GetBytes(partner_key);
            byte[] byte_base_string = Encoding.UTF8.GetBytes(tmp_base_string);
            var hash = new HMACSHA256(byte_partner_key);
            byte[] tmp_sign = hash.ComputeHash(byte_base_string);
            string sign = BitConverter.ToString(tmp_sign).Replace("-", "").ToLower();

            string url = String.Format(cShopeeHost + path + "?partner_id={0}&timestamp={1}&sign={2}", partner_id, timest, sign);

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
            long timest = Common.GetTimestampNow();

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
            //MyLogger.GetInstance().Info(url);

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
            //MyLogger.GetInstance().Info(body);

            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            MyLogger.InfoRestLog(client, request, response);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
            //MyLogger.GetInstance().Info(response.Content);
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
            // Lưu giá trị cũ vào log
            MyLogger.GetInstance().Info("old access_token: " + ttbm.Shopee_GetAccessToken(action));
            MyLogger.GetInstance().Info("old refresh_token: " + ttbm.Shopee_GetRefreshToken(action));
            if (token != null)
            {
                ttbm.Shopee_UpdateAccessToken(action, token.access_token);
                ttbm.Shopee_UpdateRefreshToken(action, token.refresh_token);
            }
            return token;
        }

        //public static long GetTimestampNow()
        //{
        //    DateTime start = DateTime.Now;
        //    long timest = ((DateTimeOffset)start).ToUnixTimeSeconds();
        //    return timest;
        //}

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
            long timest = Common.GetTimestampNow();
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
                return null;


            if (response.StatusCode == HttpStatusCode.Forbidden) // Làm mới access token
            {
                if(ShopeeGetRefreshTokenShopLevel() == null)
                {
                    return null;
                }

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
        /// Lấy item theo các tham số
        /// </summary>
        /// <param name="update_time_from">  <= 0 để bỏ qua</param>
        /// <param name="update_time_to"> <= 0 để bỏ qua</param>
        /// <param name="offset"></param>
        /// <param name="page_size"></param>
        /// <param name="lsShopeeItemStatus"></param>
        /// <returns>null nếu không lấy thành công</returns>
        public static ShopeeGetItemListResponseHTTP ShopeeProductGetItemListOld(long update_time_from, long update_time_to,
            int offset, int page_size,
            List<ShopeeItemStatus> lsShopeeItemStatus)
        {
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
                return null;

            ShopeeGetItemListResponseHTTP objResponse = null;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    JsonSerializerSettings settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    objResponse = JsonConvert.DeserializeObject<ShopeeGetItemListResponseHTTP>(response.Content, settings);
                }
                catch (Exception ex)
                {
                    MyLogger.GetInstance().Warn(ex.Message);
                    return null;
                }
            }
            return objResponse;
        }

        public static ShopeeGetItemListResponseHTTP ShopeeProductGetItemList(List<DevNameValuePair> ls)
        {
            string path = "/api/v2/product/get_item_list";

            IRestResponse response = ShopeeGetMethod(path, ls);
            if (response == null)
                return null;

            ShopeeGetItemListResponseHTTP objResponse = null;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    JsonSerializerSettings settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    objResponse = JsonConvert.DeserializeObject<ShopeeGetItemListResponseHTTP>(response.Content, settings);
                }
                catch (Exception ex)
                {
                    MyLogger.GetInstance().Warn(ex.Message);
                    return null;
                }
            }
            return objResponse;
        }

        /// <summary>
        /// Lấy tất cả item. Item này chứa dữ liệu vô cùng base
        /// </summary>
        /// <returns>null nếu không lấy thành công</returns>
        public static List<ShopeeItem> ShopeeProductGetItemListAll()
        {
            List<ShopeeItem> rs = new List<ShopeeItem>();
            int offset = 0;
            int page_size = 50;
            List<ShopeeItemStatus> lsShopeeItemStatus = new List<ShopeeItemStatus>();
            lsShopeeItemStatus.Add(new ShopeeItemStatus(ShopeeItemStatus.EnumShopeeItemStatus.NORMAL));
            lsShopeeItemStatus.Add(new ShopeeItemStatus(ShopeeItemStatus.EnumShopeeItemStatus.UNLIST));
            lsShopeeItemStatus.Add(new ShopeeItemStatus(ShopeeItemStatus.EnumShopeeItemStatus.BANNED));
            lsShopeeItemStatus.Add(new ShopeeItemStatus(ShopeeItemStatus.EnumShopeeItemStatus.DELETED));

            List<DevNameValuePair> ls = new List<DevNameValuePair>();
            ls.Add(new DevNameValuePair("offset", offset.ToString()));
            ls.Add(new DevNameValuePair("page_size", page_size.ToString()));
 
            foreach (ShopeeItemStatus item in lsShopeeItemStatus)
            {
                ls.Add(new DevNameValuePair("item_status", item.GetString()));
            }

            Boolean isOk = true;
            while (true)
            {
                ShopeeGetItemListResponseHTTP objResponse = ShopeeProductGetItemList(ls);
                if(objResponse == null)
                {
                    isOk = false;
                    break;
                }
                if(objResponse.response.item != null)
                {
                    rs.AddRange(objResponse.response.item);
                    offset = offset + objResponse.response.item.Count();
                }
                if (!objResponse.response.has_next_page)
                    break;
            }
            if (!isOk)
                return null;
            return rs;
        }

        /// <summary>
        /// Lấy base info
        /// </summary>
        /// <param name="ls"> Chứa id item cần lấy base info</param>
        /// <returns>null nếu không lấy thành công</returns>
        public static ShopeeGetItemBaseInfoResponseHTTP ShopeeProductGetItemBaseInfo(List<DevNameValuePair> ls)
        {
            string path = "/api/v2/product/get_item_base_info";

            IRestResponse response = ShopeeGetMethod(path, ls);
            if (response == null)
                return null;
            ShopeeGetItemBaseInfoResponseHTTP objResponse = null;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    JsonSerializerSettings settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    objResponse = JsonConvert.DeserializeObject<ShopeeGetItemBaseInfoResponseHTTP>(response.Content, settings);
                }
                catch (Exception ex)
                {
                    MyLogger.GetInstance().Warn(ex.Message);
                    return null;
                }
            }

            return objResponse;
        }

        /// <summary>
        /// Lấy base info của tất cả item
        /// </summary>
        /// <returns>null nếu không lấy thành công</returns>
        public static List<ShopeeGetItemBaseInfoItem> ShopeeProductGetItemBaseInfoAll()
        {
            // Lấy danh sách id của item
            List<ShopeeItem> shopeeItems = ShopeeProductGetItemListAll();
            if (shopeeItems == null || shopeeItems.Count() == 0)
                return null;

            StringBuilder strListItemId = new StringBuilder();
            List<ShopeeGetItemBaseInfoItem> rs = new List<ShopeeGetItemBaseInfoItem>();
            Boolean isOk = true;
            int countItemID = shopeeItems.Count();
            int indexItemID = 0;
            int maxSize = 40;
            int i;
            while (indexItemID < countItemID)
            {
                strListItemId.Clear();
                for (i = indexItemID; (i < countItemID) && (i < indexItemID + maxSize) ;i++)
                {
                    strListItemId.Append(shopeeItems[i].item_id.ToString() + ",");
                }
                indexItemID = i;
                // xóa bỏ , cuối cùng
                strListItemId.Remove(strListItemId.Length - 1, 1);

                List<DevNameValuePair> ls = new List<DevNameValuePair>();
                // item_id_list Required item_id  limit [0,50]
                ls.Add(new DevNameValuePair("item_id_list", strListItemId.ToString()));

                // need_tax_info  If true, will return tax info in response.
                ls.Add(new DevNameValuePair("need_tax_info", "false"));

                // need_complaint_policy If true, will return complaint_policy in response.
                ls.Add(new DevNameValuePair("need_complaint_policy", "false"));

                ShopeeGetItemBaseInfoResponseHTTP objResponse = ShopeeProductGetItemBaseInfo(ls);

                if (objResponse == null || objResponse.response == null || objResponse.response.item_list == null)
                {
                    isOk = false;
                    break;
                }

                rs.AddRange(objResponse.response.item_list);
            }
            if (!isOk)
                return null;

            return rs;
        }
        #endregion

        #region Order
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ls"></param>
        /// <returns>null nếu không lấy thành công</returns>
        public static ShopeeGetOrderListResponseHTTP ShopeeOrderGetOrderList(List<DevNameValuePair> ls)
        {
            string path = "/api/v2/order/get_order_list";
            IRestResponse response = ShopeeGetMethod(path, ls);
            if (response == null)
                return null;

            ShopeeGetOrderListResponseHTTP objResponse = null;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    JsonSerializerSettings settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    objResponse = JsonConvert.DeserializeObject<ShopeeGetOrderListResponseHTTP>(response.Content, settings);
                }
                catch (Exception ex)
                {
                    MyLogger.GetInstance().Warn(ex.Message);
                    return null;
                }
            }
            return objResponse;
        }

        /// <summary>
        /// Lấy đơn hàng trong khoảng thời gian nhỏ hơn 15 ngày
        /// </summary>
        /// <returns>null nếu không lấy thành công</returns>
        public static List<ShopeeGetOrderListBaseInfo> ShopeeOrderGetOrderListAllWithSmallTimeRange(long time_from, long time_to, ShopeeOrderStatus status)
        {
            List<DevNameValuePair> ls = new List<DevNameValuePair>();
            // Required
            // The kind of time_from and time_to. Available value: create_time, update_time.
            ls.Add(new DevNameValuePair("time_range_field", "create_time"));

            // Required
            // The time_from and time_to fields specify a date range for retrieving orders (based on the time_range_field).
            // The time_from field is the starting date range.The maximum date range that may be specified with the time_from and time_to fields is 15 days.
            ls.Add(new DevNameValuePair("time_from", time_from.ToString()));

            // Required
            // The time_from and time_to fields specify a date range for retrieving orders (based on the time_range_field).
            // The time_from field is the starting date range. The maximum date range that may be specified with the time_from and time_to fields is 15 days.
            ls.Add(new DevNameValuePair("time_to", time_to.ToString()));

            // Required
            // Each result set is returned as a page of entries. Use the "page_size" filters to control the maximum number of entries to retrieve per page (i.e., per call).
            // This integer value is used to specify the maximum number of entries to return in a single "page" of data.The limit of page_size if between 1 and 100.
            ls.Add(new DevNameValuePair("page_size", "20"));

            // The order_status filter for retriveing orders and each one only every request.
            // Available value: UNPAID/READY_TO_SHIP/PROCESSED/SHIPPED/COMPLETED/IN_CANCEL/CANCELLED/INVOICE_PENDING
            if(status.index != ShopeeOrderStatus.EnumShopeeOrderStatus.ALL)
                ls.Add(new DevNameValuePair("order_status", status.GetString()));

            // Optional fields in response. Available value: order_status.
            ls.Add(new DevNameValuePair("response_optional_fields", "order_status"));

            // Specifies the starting entry of data to return in the current call. Default is "".
            // If data is more than one page, the offset can be some entry to start next call.
            ls.Add(new DevNameValuePair("cursor", "")); // Thêm vào sau cùng list để tiện cập nhật

            string strCursor = "";
            List<ShopeeGetOrderListBaseInfo> rs = new List<ShopeeGetOrderListBaseInfo>();
            Boolean isOK = true;
            while (true)
            {
                ls.RemoveAt(ls.Count() - 1);
                ls.Add(new DevNameValuePair("cursor", strCursor));

                ShopeeGetOrderListResponseHTTP orderListTemp = ShopeeOrderGetOrderList(ls);
                if (orderListTemp == null)
                {
                    isOK = false;
                    break;
                }

                if (orderListTemp.response == null ||
                    orderListTemp.response.order_list == null ||
                    orderListTemp.response.order_list.Count() == 0)
                {
                    isOK = false;
                    break;
                }
                rs.AddRange(orderListTemp.response.order_list);
                if (!orderListTemp.response.more)
                    break;
                strCursor = orderListTemp.response.next_cursor;
            }
            if (!isOK)
                return null;
            return rs;
        }

        /// <summary>
        /// Lấy đơn hàng trong khoảng thời gian không giới hạn
        /// </summary>
        /// <param name="time_from"></param>
        /// <param name="time_to"></param>
        /// <param name="status"></param>
        /// <returns>null nếu không lấy thành công</returns>
        public static List<ShopeeGetOrderListBaseInfo> ShopeeOrderGetOrderListAll(DateTime time_from, DateTime time_to, ShopeeOrderStatus status)
        {
            long ltime_from = Common.GetTimestamp(time_from);
            long ltime_to = Common.GetTimestamp(time_to);
            long ltime_toTemp;
            List<ShopeeGetOrderListBaseInfo> rs = new List<ShopeeGetOrderListBaseInfo>();
            Boolean isOK = true;
            // Ta lấy trong khoảng thời gian là 14 ngày. Khoảng max là 15 ngày
            long rangeTime = 14 * 24 * 60 * 60;
            while (ltime_from < ltime_to)
            {


                ltime_toTemp = ltime_from + rangeTime;
                if (ltime_toTemp > ltime_to)
                    ltime_toTemp = ltime_to;
                List<ShopeeGetOrderListBaseInfo> rsTemp = ShopeeOrderGetOrderListAllWithSmallTimeRange(ltime_from, ltime_toTemp, status);
                if(rsTemp == null)
                {
                    isOK = false;
                    break;
                }
                ltime_from = ltime_from + rangeTime;
                rs.AddRange(rsTemp);
            }
            if (!isOK)
                return null;

            return rs;
        }
        #endregion
    }
}
