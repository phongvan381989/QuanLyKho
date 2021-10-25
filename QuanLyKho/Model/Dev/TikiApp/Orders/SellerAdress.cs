using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.TikiApp.Orders
{
    /// <summary>
    /// Seller adress
    /// </summary>
    public class SellerAdress
    {
        public string street { get; set; }

        public string ward { get; set; }

        public string district { get; set; }

        public string region { get; set; }

        public string country { get; set; }
    }
}
