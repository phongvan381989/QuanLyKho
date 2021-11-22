using QuanLyKho.Model;
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
        /// Số thứ tự
        /// </summary>
        public int index { get; set; }

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
        public string quantity { get; set; }

        public ProductInOutWarehoseViewBinding(int inputIndex, string inputCode, string inputName, string inputQuantity)
        {
            index = inputIndex;
            code = inputCode;
            name = inputName;
            quantity = inputQuantity;
        }
        public ProductInOutWarehoseViewBinding(ProductInOutWarehoseViewBinding obj)
        {
            index = obj.index;
            code = obj.code;
            name = obj.name;
            quantity = obj.quantity;
        }

        public ProductInOutWarehoseViewBinding(int inputIndex, ModelThongTinChiTiet ttct)
        {
            index = inputIndex;
            code = ttct.maSanPham;
            name = ttct.tenSanPham;
            quantity = ttct.tonKho;
        }
    }
}
