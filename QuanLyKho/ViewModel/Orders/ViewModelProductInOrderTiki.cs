using QuanLyKho.ViewModel.Dev.TikiAPI.Orders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.ViewModel.Orders
{
    public class ViewModelProductInOrderTiki : ViewModelBase
    {
        public ViewModelProductInOrderTiki()
        {
            listProductInOrder = new ObservableCollection<ProductInOrderViewBindingTiki>();
            listProductInOrder.Add(new ProductInOrderViewBindingTiki());
            listProductInOrder.Add(new ProductInOrderViewBindingTiki());
            listProductInOrder.Add(new ProductInOrderViewBindingTiki(true, 1, System.AppDomain.CurrentDomain.BaseDirectory + @"\Temporary\ImageTest\fish.jpg", 1));
            listProductInOrder.Add(new ProductInOrderViewBindingTiki(false, 11, System.AppDomain.CurrentDomain.BaseDirectory + @"\Temporary\ImageTest\1.jpg", 11));
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
