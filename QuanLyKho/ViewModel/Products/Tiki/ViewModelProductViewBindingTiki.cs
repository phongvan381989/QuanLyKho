using QuanLyKho.General;
using QuanLyKho.Model.Dev.TikiApp.Products;
using QuanLyKho.ViewModel.Products;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.ViewModel.Dev.TikiAPI.Products
{
    /// <summary>
    /// Sản phẩm phục vụ binding
    /// </summary>
    public class ViewModelProductViewBindingTiki : ViewModelBase
    {
        public ViewModelProductViewBindingTiki(Product product, int inputIndex, ViewModelProductTiki inputParent)
        {
            index = inputIndex;
            product_id = product.product_id.ToString();
            sku = product.sku;
            name = product.name;
            // Lấy tên file ảnh
            // Từ url lấy được tên ảnh
            avatar = Common.GetNameFromURL(product.thumbnail);
            strActive = (product.active == 0) ? "Đang Tắt" : "Đang Bật";
            strHidden = (product.is_hidden == false) ? "Đang Ẩn" : "Đang Hiện";
            price = product.price;
            market_price = product.market_price;

            parent = inputParent;
            vmProductTikiMapping = new ViewModelProductMappingProductInWarehouse(product_id, this);
        }

        public ViewModelProductTiki parent;
        /// <summary>
        /// Số thứ tự
        /// </summary>
        public int index { get; set; }

        /// <summary>
        /// Unique product ID
        /// </summary>
        public string product_id { get; set; }

        /// <summary>
        /// SKU of product
        /// </summary>
        public string sku { get; set; }

        /// <summary>
        /// Name of product
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Đường dẫn chứa ảnh đại diện
        /// </summary>
        public string avatar { get; set; }

        /// <summary>
        /// product is active (1) or inactive
        /// </summary>
        public string strActive { get; set; }

        /// <summary>
        /// product is hidden.
        /// </summary>
        public string strHidden { get; set; }

        /// <summary>
        /// the sell price of a product
        /// </summary>
        public Int32 price { get; set; }

        /// <summary>
        /// the price before discount of a product
        /// </summary>
        public Int32 market_price { get; set; }

        private ViewModelProductMappingProductInWarehouse pvmProductTikiMapping;
        public ViewModelProductMappingProductInWarehouse vmProductTikiMapping
        {
            get
            {
                return pvmProductTikiMapping;
            }
            set
            {
                if (pvmProductTikiMapping != value)
                {
                    pvmProductTikiMapping = value;
                    OnPropertyChanged("vmProductTikiMapping");
                }
            }
        }
    }
}
