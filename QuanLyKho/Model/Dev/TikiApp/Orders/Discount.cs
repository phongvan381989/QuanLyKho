using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.TikiApp.Orders
{
    public class Discount
    {
        public DiscountShippingFee discount_shipping_fee { get; set; }

        public DiscountCoupon discount_coupon { get; set; }

        public DiscountTikixu discount_tikixu { get; set; }
    }
}
