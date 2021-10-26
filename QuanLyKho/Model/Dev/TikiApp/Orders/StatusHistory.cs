using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.TikiApp.Orders
{
    public class StatusHistory
    {
        /// <summary>
        /// 1105187480	Unique Id of the history entry
        /// </summary>
        public Int32 id { get; set; }

        /// <summary>
        /// successful_delivery	Order status
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 2020-08-10 18:50:17	When the order arrives at this status
        /// </summary>
        public DateTime created_at { get; set; }
    }
}
