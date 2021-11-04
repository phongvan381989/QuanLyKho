using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.TikiApp.Orders
{
    /// <summary>
    /// Deserialize tới đối tượng của lớp này khi lấy danh sách đơn hàng
    /// </summary>
    public class PageOrders
    {
        public List<Order> data { get; set; }
        public PagingOrder paging { get; set; }
    }
}
