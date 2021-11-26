using QuanLyKho.Model.InOutWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.ViewModel.Orders.Tiki
{
    public class OrderCheckProductInWarehouseViewBindingTiki : ViewModelBase
    {
        public static int indexCheck = -1;
        public OrderCheckProductInWarehouseViewBindingTiki(ModelMappingSanPhamTMDT_SanPhamKho e, int quantity, int inputIndex)
        {
            isChecked = false;
            code = e.code;
            name = e.name;
            positionInWarehouse = e.position;
            checkedQuantity = 0;
            int result;
            if (Int32.TryParse(e.quantity, out result))
                needQuantity = quantity * result;
            else
                needQuantity = quantity;
            UpdateStatusOfQuantity();
            index = inputIndex;
        }
        public void UpdateStatusOfQuantity()
        {
            statusOfQuantity = checkedQuantity.ToString() + @"/" + needQuantity.ToString();
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
                if (pisChecked != value)
                {
                    indexCheck = index;
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
        private string pstatusOfQuantity;
        public string statusOfQuantity
        {
            get
            {
                return pstatusOfQuantity;
            }

            set
            {
                if(pstatusOfQuantity !=value)
                {
                    pstatusOfQuantity = value;
                    OnPropertyChanged("statusOfQuantity");
                }
            }
        }

        public int needQuantity;

        public int checkedQuantity;
        public int index;
    }
}
