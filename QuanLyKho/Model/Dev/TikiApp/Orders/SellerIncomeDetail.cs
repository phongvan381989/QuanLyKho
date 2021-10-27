using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.TikiApp.Orders
{
    public class SellerIncomeDetail
    {
        public Double item_price { get; set; }

        public Int32 item_qty { get; set; }

        public Double shipping_fee { get; set; }

        public List<SellerFee> seller_fees { get; set; }

        public Double sub_total { get; set; }

        public Double seller_income { get; set; }

        public Discount discount { get; set; }
    }
}
