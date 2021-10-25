using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.TikiApp.Orders
{
    /// <summary>
    /// Some basic seller information
    /// </summary>
    public class OrderItemSeller
    {
        /// <summary>
        /// 33	Unique seller Id
        /// </summary>
        public Int32 id { get; set; }
        /// <summary>
        /// Zero Shop	Seller or Store name
        /// </summary>
        public string name { get; set; }
    }
}
