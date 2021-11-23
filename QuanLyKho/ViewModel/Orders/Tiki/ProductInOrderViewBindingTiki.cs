using QuanLyKho.General;
using QuanLyKho.Model.Dev.TikiApp.Orders;
using QuanLyKho.ViewModel.Orders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyKho.ViewModel.Dev.TikiAPI.Orders
{
    /// <summary>
    /// Phục vụ binding khi hiển thị chi tiết 1 đơn
    /// </summary>
    public class ProductInOrderViewBindingTiki
    {

        public ProductInOrderViewBindingTiki(OrderItemV2 orderItemV2)
        {
            isSelected = false;
            idInShop = orderItemV2.product.id;
            // Từ id sản phẩm trên shop TMDT, lấy được danh sách sản phẩm trong kho

            //listProductsInWarehouse = new ObservableCollection<string>();
            //listProductsInWarehouse.Add("123456(0/2)-AA-Miu bé nhỏ đừng khóc nhé");
            //listProductsInWarehouse.Add("54234(0/2)-AB-Kenta thi chạy");
            //listProductsInWarehouse.Add("56464(0/2)-AC-Gà con lon ton");
            //listProductsInWarehouse.Add("123456(0/2)-AA-Miu bé nhỏ đừng khóc nhé");
            string thumbnail = orderItemV2.product.thumbnail;
            // Lấy tên file ảnh
            // Từ url lấy được tên ảnh
            avatar = Common.GetNameFromURL(thumbnail);
            amount = orderItemV2.qty;

            vmOrderCheck = new ViewModelOrderCheckProductInWarehouseTiki("123", 2);
        }

        /// <summary>
        /// Sản phẩm trong đơn đã đủ chưa?
        /// </summary>
        public bool isSelected { get; set; }

        /// <summary>
        /// id sản phẩm trên shop TMDT
        /// </summary>
        public int idInShop { get; set; } 

        public ViewModelOrderCheckProductInWarehouseTiki vmOrderCheck { get; set; }

        /// <summary>
        /// Đường dẫn chứa ảnh đại diện
        /// </summary>
        public string avatar { get; set; }

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
