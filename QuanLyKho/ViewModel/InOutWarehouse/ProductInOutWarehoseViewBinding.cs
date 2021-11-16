using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.ViewModel.InOutWarehouse
{
    /// <summary>
    /// Phục vụ view binding cho danh sách sản phẩm trong kho
    /// </summary>
    public class ProductInOutWarehoseViewBinding
    {
        /// <summary>
        /// Mã sản phẩm
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        public string name { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public int quantity { get; set; }
    }
}
