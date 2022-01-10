using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.ViewModel.Dev.ShopeeAPI
{
    public class CommonShopeeAPI
    {
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

        public static string ShopeeGenerateAuthPartnerUrl(/*string secretKey, string url, string httpBody */)
        {
            DateTime start = DateTime.Now;
            long timest = ((DateTimeOffset)start).ToUnixTimeSeconds();
            // https://partner.shopeemobile.com/api/v2/shop/auth_partner
            // https://partner.test-stable.shopeemobile.com/api/v2/shop/auth_partner (test environment)
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
            return url;
        }

        public static string ShopeeGetTokenShopLevell(string code, string partner_id, string partner_key, string shop_id)
        {
            DateTime start = DateTime.Now;
            long timest = ((DateTimeOffset)start).ToUnixTimeSeconds();

            string host = "https://partner.shopeemobile.com";
            string path = "/api/v2/auth/token/get";
            string tmp_base_string = String.Format("{0}{1}{2}", partner_id, path, timest);
            byte[] byte_partner_key = Encoding.UTF8.GetBytes(partner_key);
            byte[] byte_base_string = Encoding.UTF8.GetBytes(tmp_base_string);
            var hash = new HMACSHA256(byte_partner_key);
            byte[] tmp_sign = hash.ComputeHash(byte_base_string);
            string sign = BitConverter.ToString(tmp_sign).Replace("-", "").ToLower();

            string url = String.Format(host + path + "?partner_id={0}&timestamp={1}&sign={2}", partner_id, timest, sign);
            return url;
        }

        /// <summary>
        /// Sinh ra tham số "the sign"
        /// </summary>
        /// <param name="partner_id"></param>
        /// <param name="api_path"></param>
        /// <param name="timestamp"></param>
        /// <param name="access_token"></param>
        /// <param name="shop_id"></param>
        /// <param name="partner_key"></param>
        /// <returns></returns>
        public static string GenerateSignatureShopeeAPI(
            //string partner_id, 
            //string api_path,
            //string timestamp, 
            //string access_token, 
            //string shop_id,
            /*string partner_key*/)
        {
            DateTime start = DateTime.Now;
            long timest = ((DateTimeOffset)start).ToUnixTimeSeconds();
            string host = "https://partner.shopeemobile.com";
            string path = "/api/v2/shop/get_shop_info";
            string access_token = "4f415a7a504d4d706a67626147677952";
            string shop_id = "137637267";
            string partner_id = "2002851";
            string partner_key = "19da669a5bc42ec97a43e16c041b3b364563561eabf749571cd9af0ba7821070";

            string tmp_base_string = String.Format("{0}{1}{2}{3}{4}", partner_id, path, timest, access_token, shop_id);
            byte[] byte_partner_key = Encoding.UTF8.GetBytes(partner_key);
            byte[] byte_base_string = Encoding.UTF8.GetBytes(tmp_base_string);
            var hash = new HMACSHA256(byte_partner_key);
            byte[] tmp_sign = hash.ComputeHash(byte_base_string);
            string sign = BitConverter.ToString(tmp_sign).Replace("-", "").ToLower();
            string url = String.Format(host + path + "?partner_id={0}&timestamp={1}&sign={2}&shop_id={3}&access_token={4}", partner_id, timest, sign, shop_id, access_token);
            return url;
        }
    }
}
