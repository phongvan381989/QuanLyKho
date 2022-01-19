using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.ShopeeApp.ShopeeOrder
{
    public class ShopeeOrderStatus
    {
        public enum EnumShopeeOrderStatus
        {
            UNPAID,
            READY_TO_SHIP,
            PROCESSED,
            SHIPPED,
            COMPLETED,
            IN_CANCEL,
            CANCELLED,
            INVOICE_PENDING,
            ALL
        }

        public ShopeeOrderStatus()
        {
            index = EnumShopeeOrderStatus.ALL;
        }
        public ShopeeOrderStatus(EnumShopeeOrderStatus input)
        {
            index = input;
        }

        public EnumShopeeOrderStatus index;
        public string GetString()
        {
            string str = null;
            if (index == EnumShopeeOrderStatus.UNPAID)
                str = "UNPAID";
            else if (index == EnumShopeeOrderStatus.READY_TO_SHIP)
                str = "READY_TO_SHIP";
            else if (index == EnumShopeeOrderStatus.PROCESSED)
                str = "PROCESSED";
            else if (index == EnumShopeeOrderStatus.SHIPPED)
                str = "SHIPPED";
            if (index == EnumShopeeOrderStatus.COMPLETED)
                str = "COMPLETED";
            else if (index == EnumShopeeOrderStatus.IN_CANCEL)
                str = "IN_CANCEL";
            else if (index == EnumShopeeOrderStatus.CANCELLED)
                str = "CANCELLED";
            else if (index == EnumShopeeOrderStatus.INVOICE_PENDING)
                str = "INVOICE_PENDING";
            else if (index == EnumShopeeOrderStatus.ALL)
                str = "ALL";
            return str;
        }
    }
}
