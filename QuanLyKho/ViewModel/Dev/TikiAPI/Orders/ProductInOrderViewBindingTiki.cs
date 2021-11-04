using QuanLyKho.Model.Dev.TikiApp.Orders;
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
        //public ProductInOrderViewBindingTiki()
        //{
        //    isSelected = isSelected;
        //    idInShop = -1;
        //    listProductsInWarehouse = new ObservableCollection<string>();
        //    listProductsInWarehouse.Add("123456(0/2)-AA-Miu bé nhỏ đừng khóc nhé");
        //    listProductsInWarehouse.Add("54234(0/2)-AB-Kenta thi chạy");
        //    listProductsInWarehouse.Add("56464(0/2)-AC-Gà con lon ton");
        //    listProductsInWarehouse.Add("123456(0/2)-AA-Miu bé nhỏ đừng khóc nhé");
        //    avartar = string.Empty;

        //    amount = 0;
        //}

        //public ProductInOrderViewBindingTiki(bool inputIsSelected, int inputId, string inputAvartar, int inputAmount)
        //{
        //    isSelected = inputIsSelected;
        //    idInShop = inputId;
        //    listProductsInWarehouse = new ObservableCollection<string>();
        //    listProductsInWarehouse.Add("123456(0/2)-AA-Miu bé nhỏ đừng khóc nhé");
        //    listProductsInWarehouse.Add("54234(0/2)-AB-Kenta thi chạy");
        //    listProductsInWarehouse.Add("56464(0/2)-AC-Gà con lon ton");
        //    listProductsInWarehouse.Add("123456(0/2)-AA-Miu bé nhỏ đừng khóc nhé");
        //    avartar = inputAvartar;
        //    amount = inputAmount;
        //}

        public ProductInOrderViewBindingTiki(OrderItemV2 orderItemV2)
        {
            isSelected = false;
            idInShop = orderItemV2.product.id;
            listProductsInWarehouse = new ObservableCollection<string>();
            listProductsInWarehouse.Add("123456(0/2)-AA-Miu bé nhỏ đừng khóc nhé");
            listProductsInWarehouse.Add("54234(0/2)-AB-Kenta thi chạy");
            listProductsInWarehouse.Add("56464(0/2)-AC-Gà con lon ton");
            listProductsInWarehouse.Add("123456(0/2)-AA-Miu bé nhỏ đừng khóc nhé");
            string thumbnail = orderItemV2.product.thumbnail;
            // Lấy tên file ảnh
            // Từ url lấy được tên ảnh
            int lastIndex = thumbnail.LastIndexOf('/');
            if (lastIndex == -1 || lastIndex == thumbnail.Length - 1)
            {
                avartar = string.Empty;
            }
            else
            {
                string fileName = thumbnail.Substring(lastIndex + 1);
                avartar = Path.Combine(((App)Application.Current).temporaryImageFolderPath, fileName);
            }
            amount = orderItemV2.qty;
        }

        /// <summary>
        /// Sản phẩm trong đơn đã đủ chưa?
        /// </summary>
        public bool isSelected { get; set; }

        /// <summary>
        /// id sản phẩm trên shop TMDT
        /// </summary>
        public int idInShop { get; set; } 

        /// <summary>
        /// Nếu sản phẩm trên shop TMDT là commbo sẽ gồm nhiều sản phẩm thực tế trong kho.
        /// danh sách sản phẩm lưu kho gồm mã sản phẩm - tên sản phẩm - vị trí trong kho, cách nhau bởi dấu phảy.
        /// VD: 
        /// 123456(0/2)-AA-Miu bé nhỏ đừng khóc nhé
        /// 54234(0/2)-AB-Kenta thi chạy
        /// 56464(0/2)-AC-Gà con lon ton
        /// (0/2): 0 là số lượng đã kiểm vào đơn, 2 là số lượng sản phẩm khách đặt
        /// </summary>
        public ObservableCollection<string> listProductsInWarehouse { get; set; }

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
