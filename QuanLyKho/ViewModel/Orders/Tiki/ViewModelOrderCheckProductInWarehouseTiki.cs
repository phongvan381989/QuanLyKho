using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.ViewModel.Orders
{
    /// <summary>
    /// Phục vụ giao diện check đã lấy đủ số lượng của 1 sản phẩm từ kho cho 1 đơn hàng
    /// VD: 1/3 tức cần 3 sản phẩm xuất kho cho đơn hàng nhưng đã check được 1 sản phẩm
    /// </summary>
    public class ViewModelOrderCheckProductInWarehouseTiki : ViewModelBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productTMDTCode">mã sản phẩm trên shop TMDT</param>
        /// <param name="quantity">Số lượng</param>
        public ViewModelOrderCheckProductInWarehouseTiki(string productTMDTCode, int quantity)
        {

        }

        private Boolean pisChecked;
        public Boolean isChecked
        {
            get
            {
                return pisChecked;
            }

            set
            {
                if(pisChecked != value)
                {
                    pisChecked = value;
                    OnPropertyChanged("isChecked");
                }
            }
        }

        public string code { get; set; }

        public string name { get; set; }

        public string positionInWarehouse { set; get; }

        /// <summary>
        /// VD: 1/3 tức cần 3 sản phẩm xuất kho cho đơn hàng nhưng đã check được 1 sản phẩm
        /// </summary>
        public string statusOfQuantity { set; get; }

        private int needQuantity;

        private int checkedQuantity;
    }
}
