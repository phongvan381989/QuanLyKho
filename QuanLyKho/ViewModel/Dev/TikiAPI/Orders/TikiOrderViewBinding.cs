using QuanLyKho.Model.Dev.TikiApp.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.ViewModel.Dev.TikiAPI.Orders
{
    /// <summary>
    /// Đơn hàng phục vụ binding hiển thị
    /// </summary>
    public class TikiOrderViewBinding
    {
        /// <summary>
        /// id đơn hàng
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Mã đơn hàng
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// complete	The status where the order is in the process to customer hands
        /// </summary>
        public string status { get; set; }

        public List<int> listProductId { get; set; }

        /// <summary>
        /// 2020-08-10 18:50:17	When the order is created
        /// </summary>
        public DateTime created_at { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> listThumbnail { get; set; }

        public TikiOrderViewBinding()
        {
            id = -1;
            code = string.Empty;
            status = string.Empty;
            listProductId = new List<int>();
            created_at = new DateTime();
            listThumbnail = new List<string>();
        }

        /// <summary>
        /// Từ 1 đối tượng object chứa đầy đủ thông tin như Tiki trả về, ta lấy thông tin cần thiết phục vụ binding
        /// </summary>
        /// <param name="order"></param>
        public TikiOrderViewBinding(Order order)
        {
            id = order.id;
            code = order.code;
            status = order.status;
            listProductId = new List<int>();
            listThumbnail = new List<string>();
            foreach (OrderItemV2 e in order.items)
            {
                listProductId.Add(e.id);
                listThumbnail.Add(e.product.thumbnail);
            }
            created_at = order.created_at;
        }
    }
}
