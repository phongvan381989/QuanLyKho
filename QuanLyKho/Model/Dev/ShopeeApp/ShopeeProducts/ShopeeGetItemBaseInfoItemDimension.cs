using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.ShopeeApp.ShopeeProducts
{
    public class ShopeeGetItemBaseInfoItemDimension
    {
        /// <summary>
        /// The length of package for this single item, the unit is CM.
        /// </summary>
        public int package_length { get; set; }

        /// <summary>
        /// The width of package for this single item, the unit is CM.
        /// </summary>
        public int package_width { get; set; }

        /// <summary>
        /// The height of package for this single item, the unit is CM
        /// </summary>
        public int package_height { get; set; }
    }
}
