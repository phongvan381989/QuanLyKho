using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.TikiApp.Orders
{
    public class BillingAddress
    {
        public string full_name { get; set; }

        public string street { get; set; }

        public string ward { get; set; }

        public string ward_tiki_code { get; set; }

        public string district { get; set; }

        public string district_tiki_code { get; set; }

        public string region { get; set; }

        public string region_tiki_code { get; set; }

        public string country { get; set; }

        public string country_id { get; set; }

    }
}
