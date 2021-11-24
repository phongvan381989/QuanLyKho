using QuanLyKho.Model;
using QuanLyKho.Model.Dev.TikiApp.Orders;
using QuanLyKho.View.InOutWarehouse;
using QuanLyKho.View.Order;
using QuanLyKho.ViewModel.Dev.TikiAPI.Orders;
using QuanLyKho.ViewModel.InOutWarehouse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyKho.ViewModel.Orders
{
    public class ViewModelProductInOrderTiki : ViewModelBase
    {
        public ViewModelProductInOrderTiki(Order order)
        {
            listProductTMDTInOrder = new ObservableCollection<ViewModelProductInOrderViewBindingTiki>();
            foreach (OrderItemV2 item in order.items)
            {
                listProductTMDTInOrder.Add(new ViewModelProductInOrderViewBindingTiki(item));
            }

        }
        /// <summary>
        /// Từ id sản phẩm shop TMDT lấy mapping sản phẩm trong kho.
        /// </summary>
        public void GetMapping()
        {
            if(itemSelected == null)
                return;

            Window wdOrderDetail = new Window
            {
                Content = new UserControlMappingSanPhamTMDT_SanPhamKho()
            };
            ViewModelMappingSanPhamTMDT_SanPhamKho vm = new ViewModelMappingSanPhamTMDT_SanPhamKho(itemSelected.idInShop.ToString(), itemSelected.name);
            vm.optionVisibility = Visibility.Collapsed;
            wdOrderDetail.DataContext = vm;
            wdOrderDetail.WindowState = WindowState.Maximized;
            wdOrderDetail.ShowDialog();
        }

        public ViewModelProductInOrderViewBindingTiki itemSelected {get; set; }

        private ObservableCollection<ViewModelProductInOrderViewBindingTiki> plistProductTMDTInOrder;
        public ObservableCollection<ViewModelProductInOrderViewBindingTiki> listProductTMDTInOrder
        {
            get
            {
                return plistProductTMDTInOrder;
            }

            set
            {
                if(plistProductTMDTInOrder != value)
                {
                    plistProductTMDTInOrder = value;
                    OnPropertyChanged("listProductTMDTInOrder;");
                }
            }
        }
    }
}
