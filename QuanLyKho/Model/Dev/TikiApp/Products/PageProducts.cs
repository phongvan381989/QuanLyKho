using QuanLyKho.Model.Dev.TikiApp.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.TikiApp.Products
{
    /// <summary>
    /// Deserialize tới đối tượng của lớp này khi lấy danh sách sản phẩm
    /// </summary>
    public class PageProducts
    {
        public List<Product> data { get; set; }

        public PagingOrder paging { get; set; }
    }
}
