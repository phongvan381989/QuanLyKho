using QuanLyKho.Model;
using QuanLyKho.Model.Dev.TikiApp.Orders;
using QuanLyKho.ViewModel.Dev.TikiAPI.Orders;
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
        XMLAction actionModelMapping;
        public ViewModelProductInOrderTiki(Order order)
        {
            actionModelMapping = new XMLAction(((App)Application.Current).GetPathDataXMLMappingSanPhamTMDT_SanPhamKho());
            listProductTMDTInOrder = new ObservableCollection<ProductInOrderViewBindingTiki>();
            foreach(OrderItemV2 item in order.items)
            {
                listProductTMDTInOrder.Add(new ProductInOrderViewBindingTiki(item));
            }
        }

        private ObservableCollection<ProductInOrderViewBindingTiki> plistProductTMDTInOrder;
        public ObservableCollection<ProductInOrderViewBindingTiki> listProductTMDTInOrder
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
