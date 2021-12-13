using QuanLyKho.Model.InOutWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.ViewModel.Orders.Tiki
{
    public class ViewModelOrderCheckProductInWarehouseViewBinding : ViewModelBase
    {
        public static int indexCheck = -1;
        public ViewModelOrderCheckProductInWarehouseViewBinding(ModelMappingSanPhamTMDT_SanPhamKho e, int quantity, int inputIndex)
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
            Update();
            index = inputIndex;
        }

        /// <summary>
        /// Update trạng thái kiểm số lượng sản phẩm trong kho trong đơn khi check/uncheck
        /// </summary>
        public void Update()
        {
            if (isChecked)
            {
                checkedQuantity = needQuantity;
            }
            else
            {
                checkedQuantity = 0;
            }
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

        private int pcheckedQuantity;
        public int checkedQuantity
        {
            get
            {
                return pcheckedQuantity;
            }

            set
            {
                if(pcheckedQuantity != value)
                {
                    pcheckedQuantity = value;
                    statusOfQuantity = checkedQuantity.ToString() + @"/" + needQuantity.ToString();
                    if (pcheckedQuantity == needQuantity)
                        isChecked = true;
                }
            }
        }
        public int index;
    }
}
