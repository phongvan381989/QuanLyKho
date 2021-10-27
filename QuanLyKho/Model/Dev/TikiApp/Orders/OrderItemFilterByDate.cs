using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.TikiApp.Orders
{
    public class OrderItemFilterByDate
    {
        public enum EnumOrderItemFilterByDate
        {
            today,
            last7days,
            last30days
        }
        static public string[] ArrayStringOrderItemFilterByDate =
        {
            "today",
            "last7days",
            "last30days"
        };
    }
}
