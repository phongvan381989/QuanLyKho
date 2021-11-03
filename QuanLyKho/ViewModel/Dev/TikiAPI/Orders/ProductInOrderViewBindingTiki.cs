using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.ViewModel.Dev.TikiAPI.Orders
{
    /// <summary>
    /// Phục vụ binding khi hiển thị chi tiết 1 đơn
    /// </summary>
    public class ProductInOrderViewBindingTiki
    {
        public ProductInOrderViewBindingTiki()
        {
            isSelected = isSelected;
            idInShop = -1;
            codesInWarehouse = string.Empty;
            avartar = string.Empty;
            //location = string.Empty;
            amount = 0;
        }

        public ProductInOrderViewBindingTiki(bool inputIsSelected, int inputId, string inputAvartar, int inputAmount)
        {
            isSelected = inputIsSelected;
            idInShop = inputId;
            codesInWarehouse = string.Empty;
            avartar = inputAvartar;
            //location = inputLocation;
            amount = inputAmount;
        }

        /// <summary>
        /// Sản phẩm trong đơn đã đủ chưa?
        /// </summary>
        public bool isSelected { get; set; }

        /// <summary>
        /// id sản phẩm trên shop
        /// </summary>
        public int idInShop { get; set; } 

        /// <summary>
        /// Nếu sản phẩm trên shop TMDT là commbo sẽ gồm nhiều sản phẩm thực tế trong kho.
        /// danh sách mã sản phẩm lưu kho, mã mỗi sản phẩm cách nhau bởi", ". VD: 123456(0/2), 54234(0/2), 56464(0/2)
        /// (0/2): 0 là số lượng đã kiểm vào đơn, 2 là số lượng sản phẩm khách đặt
        /// </summary>
        public string codesInWarehouse { get; set; }

        /// <summary>
        /// Đường dẫn chứa ảnh đại diện
        /// </summary>
        public string avartar { get; set; }

        ///// <summary>
        ///// vị trí lưu sản phẩm trong kho
        ///// </summary>
        //public string location { get; set; }

        /// <summary>
        /// Số lượng sản phẩm trong đơn hàng
        /// </summary>
        public int amount { get; set; }
    }
}
