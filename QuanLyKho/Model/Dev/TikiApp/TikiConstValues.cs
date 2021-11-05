using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.TikiApp
{
    /// <summary>
    /// Chứa những giá trị hardcode dùng chung
    /// </summary>
    public class TikiConstValues
    {
        static public string cstrAuthenHTTPAddress = "https://api.tiki.vn/sc/oauth2/token";
        static public string cstrOrdersHTTPAddess = "https://api.tiki.vn/integration/v2/orders";
        static public string cstrProductsHTTPAddess = "https://api.tiki.vn/integration/v2/products";
        static public string cstrPerPage = "20";
    }
}
