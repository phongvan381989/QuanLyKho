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
        public static int indexCheck = -1;
        public ViewModelProductInOrderViewBindingTiki(OrderItemV2 orderItemV2, int inputIndex, ViewModelProductInOrderTiki inputParent)
        {
            isChecked = false;
            idInShop = orderItemV2.product.id;
            name = orderItemV2.product.name;
            // Từ id sản phẩm trên shop TMDT, lấy được danh sách sản phẩm trong kho
            string thumbnail = orderItemV2.product.thumbnail;
            // Lấy tên file ảnh
            // Từ url lấy được tên ảnh
            avatar = Common.GetNameFromURL(thumbnail);
            amount = orderItemV2.qty;
            vmOrderCheck = new ViewModelOrderCheckProductInWarehouse(idInShop.ToString(), amount, this);
            index = inputIndex;
            parent = inputParent;
        }

        public ViewModelProductInOrderTiki parent;
        /// <summary>
        /// Sản phẩm trong đơn đã đủ chưa?
        /// </summary>
        private bool pisChecked;
        public bool isChecked
        {
            get
            {
                return pisChecked;
            }
            set
            {
                if(pisChecked != value)
                {
                    indexCheck = index;
                    pisChecked = value;
                    OnPropertyChanged("isChecked");
                }
            }
        }

        /// <summary>
        /// id sản phẩm trên shop TMDT
        /// </summary>
        public int idInShop { get; set; }

        public string name { get; set; }

        private ViewModelOrderCheckProductInWarehouse pvmOrderCheck;
        public ViewModelOrderCheckProductInWarehouse vmOrderCheck
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

        /// <summary>
        /// Số lượng sản phẩm trong đơn hàng
        /// </summary>
        public int amount { get; set; }

        public int index;

        /// <summary>
        /// Update trạng thái kiểm số lượng sản phẩm trong kho trong đơn khi check/uncheck
        /// </summary>
        public void UpdateWhenCheckedFromParent()
        {
            pvmOrderCheck.UpdateWhenCheckedFromParent(isChecked);
        }

        /// <summary>
        /// Khi tất cả các dòng con được check thì dòng cha cũng check theo,
        /// khi có 1 dòng con bỏ check thì dòng cha cũng bỏ check theo
        /// </summary>
        public void UpdateIsCheckFromChildren(bool inputIsChecked)
        {
            //parent.istbFocus = false;
            if (isChecked != inputIsChecked)
            {
                parent.isDisableCheckFunction = true;
                isChecked = inputIsChecked;
            }
            //parent.istbFocus = true;
        }
    }
}
