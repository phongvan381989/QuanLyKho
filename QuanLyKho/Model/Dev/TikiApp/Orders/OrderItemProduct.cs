using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.TikiApp.Orders
{
    public class OrderItemProduct
    {
        public Int32 id { get; set; }
        public string type { get; set; }
        public Int32 super_id { get; set; }
        public Int32 master_id { get; set; }
        public string sku { get; set; }
        public string name { get; set; }
        public string catalog_group_name { get; set; }
        public string inventory_type { get; set; }
        public IList<string> imeis { get; set; }
        public IList<string> serial_numbers { get; set; }
        public string thumbnail { get; set; }
        public string seller_product_code { get; set; }
        public string seller_supply_method { get; set; }
    }
}
