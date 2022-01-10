using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.ViewModel.Dev.TikiAPI
{
    /// <summary>
    /// Chứa những giá trị hardcode dùng chung
    /// </summary>
    public class TikiConstValues
    {
        public const string cstrAuthenHTTPAddress = "https://api.tiki.vn/sc/oauth2/token";
        public const string cstrOrdersHTTPAddress = "https://api.tiki.vn/integration/v2/orders";
        public const string cstrProductsHTTPAddress = "https://api.tiki.vn/integration/v2/products";
        public const string cstrPerPage = "20";
    }
}
