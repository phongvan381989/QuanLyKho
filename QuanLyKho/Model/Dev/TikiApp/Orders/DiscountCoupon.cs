using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.TikiApp.Orders
{
    public class DiscountCoupon
    {
        public Double seller_discount { get; set; }

        public Double platform_discount { get; set; }

        public Double total_discount { get; set; }
    }
}
