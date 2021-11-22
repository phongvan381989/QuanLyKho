using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.ViewModel.InOutWarehouse
{
    /// <summary>
    /// Phục vụ view binding danh sách sản phẩm trong kho được map với 1 sản phẩm trên sàn TMDT
    /// </summary>
    class MappingSanPhamTMDT_SanPhamKhoViewBinding
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

        public MappingSanPhamTMDT_SanPhamKhoViewBinding(string inputCode, string inputName, int inputQuantity)
        {
            code = inputCode;
            name = inputName;
            quantity = inputQuantity;
        }

        public MappingSanPhamTMDT_SanPhamKhoViewBinding(string inputCode, string inputName, string inputQuantity)
        {
            code = inputCode;
            name = inputName;
            quantity = Int32.Parse(inputQuantity);
        }
    }
}
