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
        /// Số sản phẩm tồn kho
        /// </summary>
        public int quantity { get; set; }

        public ProductInOutWarehoseViewBinding(string inputCode, string inputName, int inputQuantity)
        {
            code = inputCode;
            name = inputName;
            quantity = inputQuantity;
        }

        public ProductInOutWarehoseViewBinding(string inputCode, string inputName, string inputQuantity)
        {
            code = inputCode;
            name = inputName;
            quantity = Int32.Parse(inputQuantity);
        }
    }
}
