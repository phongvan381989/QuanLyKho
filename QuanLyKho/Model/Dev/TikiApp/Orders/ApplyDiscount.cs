using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.TikiApp.Orders
{
    public class ApplyDiscount
    {
        public Int32 rule_id { get; set; }

        public string type { get; set; }

        public Double amount { get; set; }

        public Double seller_sponsor { get; set; }

        public Double tiki_sponsor { get; set; }
    }
}
