using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.TikiApp.Products
{
    public class Product
    {
        /// <summary>
        /// Unique product ID
        /// </summary>
        public Int32 product_id { get; set; }

        /// <summary>
        /// SKU of product
        /// </summary>
        public string sku { get; set; }

        /// <summary>
        /// Name of product
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// master_child or master_simple product ID
        /// </summary>
        public Int32 master_id { get; set; }

        /// <summary>
        /// master_child or master_simple product SKU
        /// </summary>
        public string master_sku { get; set; }

        /// <summary>
        /// master_configurable product ID
        /// </summary>
        public Int32 super_id { get; set; }

        /// <summary>
        /// master_configurable product SKU
        /// </summary>
        public string super_sku { get; set; }

        /// <summary>
        /// product is active (1) or inactive
        /// </summary>
        public Int32 active { get; set; }

        /// <summary>
        /// product is hidden.
        /// </summary>
        public bool is_hidden { get; set; }

        /// <summary>
        /// seller product code
        /// </summary>
        public string original_sku { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// product entity type of TIKI system. See TIKI theory for further information.
        /// </summary>
        public string entity_type { get; set; }

        /// <summary>
        /// the sell price of a product
        /// </summary>
        public Int32 price { get; set; }

        /// <summary>
        /// the price before discount of a product
        /// </summary>
        public Int32 market_price { get; set; }

        /// <summary>
        /// when the product is created
        /// </summary>
        public DateTime created_at { get; set; }

        /// <summary>
        /// When the product is updated
        /// </summary>
        public DateTime updated_at { get; set; }

        /// <summary>
        /// Thumbnail image URL
        /// </summary>
        public string thumbnail { get; set; }

        /// <summary>
        /// Seller info of product
        /// </summary>
        public Seller seller { get; set; }

        /// <summary>
        /// option variant of configurable product. See TIKI theory for further information.
        /// </summary>
        public Object option_attributes { get; set; }

        /// <summary>
        /// attributes of product
        /// </summary>
        public Object attributes { get; set; }

        /// <summary>
        /// The image gallery of product on TIKI
        /// </summary>
        public List<Image> images { get; set; }

        /// <summary>
        /// The categories that products are belong
        /// </summary>
        public List<Category> categories { get; set; }

        /// <summary>
        /// inventory information
        /// </summary>
        public Object inventory { get; set; }
    }
}
