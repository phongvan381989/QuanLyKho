using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.ShopeeApp.ShopeeProducts
{
    public class ShopeeItem
    {
        /// <summary>
        /// Shopee's unique identifier for an item.
        /// </summary>
        public long item_id {get; set;}

        /// <summary>
        /// Enumerated type that defines the current status of the item. Applicable values: NORMAL, DELETED, UNLIST and BANNED.
        /// </summary>
        public string item_status { get; set; }

        /// <summary>
        /// The update time of item
        /// </summary>
        public long update_time { get; set; }
    }
}
