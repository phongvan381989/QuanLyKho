using QuanLyKho.Model;
using QuanLyKho.Model.InOutWarehouse;
using QuanLyKho.ViewModel.Orders.Tiki;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            // Từ mã sản phẩm vào bảng map lấy được danh sách sản phẩm trong kho tương ứng
            XMLAction actionModelMapping = new XMLAction(((App)Application.Current).GetPathDataXMLMappingSanPhamTMDT_SanPhamKho());
            listCheckProduct = new ObservableCollection<OrderCheckProductInWarehouseViewBindingTiki>();
            List<ModelMappingSanPhamTMDT_SanPhamKho> ls = ModelMappingSanPhamTMDT_SanPhamKho.GetListModelMappingSanPhamTMDT_SanPhamKhoFromID(actionModelMapping, productTMDTCode);
            ObservableCollection<OrderCheckProductInWarehouseViewBindingTiki> list = new ObservableCollection<OrderCheckProductInWarehouseViewBindingTiki>();
            int indexTemp = -1;
            foreach (ModelMappingSanPhamTMDT_SanPhamKho e in ls)
            {
                indexTemp++;
                list.Add(new OrderCheckProductInWarehouseViewBindingTiki(e, quantity, indexTemp));
            }
            listCheckProduct = list;
        }

        private ObservableCollection<OrderCheckProductInWarehouseViewBindingTiki> plistCheckProduct;
        public ObservableCollection<OrderCheckProductInWarehouseViewBindingTiki> listCheckProduct
        {
            get
            {
                return plistCheckProduct;
            }

            set
            {
                if(plistCheckProduct != value)
                {
                    plistCheckProduct = value;
                    OnPropertyChanged("listCheckProduct");
                }
            }
        }

        private OrderCheckProductInWarehouseViewBindingTiki pitemSelected;
        public OrderCheckProductInWarehouseViewBindingTiki itemSelected
        {
            get
            {
                return pitemSelected;
            }

            set
            {
                if(pitemSelected != value)
                {
                    pitemSelected = value;
                    OnPropertyChanged("itemSelected");
                }
            }
        }

        private int pindexInList;
        public int indexInList
        {
            get
            {
                return pindexInList;
            }
            
            set
            {
                if(pindexInList != value)
                {
                    pindexInList = value;
                    OnPropertyChanged("indexInList");
                }
            }
        }

        public void Check()
        {
            itemSelected = listCheckProduct[OrderCheckProductInWarehouseViewBindingTiki.indexCheck];
            if (itemSelected.isChecked)
            {
                itemSelected.checkedQuantity = itemSelected.needQuantity;
            }
            else
            {
                itemSelected.checkedQuantity = 0;
            }
            itemSelected.UpdateStatusOfQuantity();
            OnPropertyChanged("itemSelected");
        }
    }
}
