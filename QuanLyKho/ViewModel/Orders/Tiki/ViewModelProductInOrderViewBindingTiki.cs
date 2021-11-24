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
    public class ViewModelProductInOrderViewBindingTiki : ViewModelBase
    {

        public ViewModelProductInOrderViewBindingTiki(OrderItemV2 orderItemV2)
        {
            isSelected = false;
            idInShop = orderItemV2.product.id;
            name = orderItemV2.product.name;
            // Từ id sản phẩm trên shop TMDT, lấy được danh sách sản phẩm trong kho
            string thumbnail = orderItemV2.product.thumbnail;
            // Lấy tên file ảnh
            // Từ url lấy được tên ảnh
            avatar = Common.GetNameFromURL(thumbnail);
            amount = orderItemV2.qty;
            vmOrderCheck = new ViewModelOrderCheckProductInWarehouseTiki(idInShop.ToString(), amount);
        }

        /// <summary>
        /// Sản phẩm trong đơn đã đủ chưa?
        /// </summary>
        public bool isSelected { get; set; }

        /// <summary>
        /// id sản phẩm trên shop TMDT
        /// </summary>
        public int idInShop { get; set; }

        public string name { get; set; }

        private ViewModelOrderCheckProductInWarehouseTiki pvmOrderCheck;
        public ViewModelOrderCheckProductInWarehouseTiki vmOrderCheck
        {
            get
            {
                return pvmOrderCheck;
            }
        set
            {
                if(pvmOrderCheck != value)
                {
                    pvmOrderCheck = value;
                    OnPropertyChanged("vmOrderCheck");
                }
            }
        }

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
