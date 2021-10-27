using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.TikiApp.Orders
{
    public class DiscountShippingFee
    {
        public Double sellerDiscount { get; set; }

        public Double fee_amount { get; set; }

        public Int32 qty { get; set; }

        public List<ApplyDiscount> apply_discount { get; set; }

        public Double seller_subsidy { get; set; }

        public Double tiki_subsidy { get; set; }
    }
}
