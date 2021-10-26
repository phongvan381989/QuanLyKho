using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.TikiApp.Orders
{
    /// <summary>
    /// Deserialize response HTTP to object of the class
    /// </summary>
    public class ListOrders
    {
        public List<Order> /*List<string>*/ data { get; set; }
        public Paging paging { get; set; }
    }
}
