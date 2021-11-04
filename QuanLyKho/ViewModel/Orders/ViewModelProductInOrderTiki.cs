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
        //public ViewModelProductInOrderTiki()
        //{
        //    listProductInOrder = new ObservableCollection<ProductInOrderViewBindingTiki>();
        //    listProductInOrder.Add(new ProductInOrderViewBindingTiki());
        //    listProductInOrder.Add(new ProductInOrderViewBindingTiki());
        //    listProductInOrder.Add(new ProductInOrderViewBindingTiki(true, 1, ((App)Application.Current).temporaryImageFolderPath + @"\fish.jpg", 1));
        //    listProductInOrder.Add(new ProductInOrderViewBindingTiki(false, 11, ((App)Application.Current).temporaryImageFolderPath + @"\1.jpg", 11));
        //}

        public ViewModelProductInOrderTiki(Order order)
        {
            listProductInOrder = new ObservableCollection<ProductInOrderViewBindingTiki>();
            foreach(OrderItemV2 item in order.items)
            {
                listProductInOrder.Add(new ProductInOrderViewBindingTiki(item));
            }
        }

        private ObservableCollection<ProductInOrderViewBindingTiki> plistProductInOrder;
        public ObservableCollection<ProductInOrderViewBindingTiki> listProductInOrder
        {
            get
            {
                return plistProductInOrder;
            }

            set
            {
                if(plistProductInOrder != value)
                {
                    plistProductInOrder = value;
                    OnPropertyChanged("listProductInOrder;");
                }
            }
        }
    }
}
