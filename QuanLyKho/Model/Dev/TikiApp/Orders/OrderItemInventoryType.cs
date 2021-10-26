using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.TikiApp.Orders
{
    public class OrderItemInventoryType
    {
        public enum EnumOrderItemInventoryType
        {
            backorder,
            instock,
            preorder
        }
        static public string[] ArrayStringOrderItemInventoryType =
        {
            "backorder",
            "instock",
            "preorder"
        };
    }
}
