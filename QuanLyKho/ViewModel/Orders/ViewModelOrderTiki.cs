using QuanLyKho.Model.Dev.TikiApp.Orders;
using QuanLyKho.ViewModel.Dev.TikiAPI;
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
    public class ViewModelOrderTiki : ViewModelBase
    {
        public ViewModelOrderTiki()
        {
            pcommandGetListAllOrderNeedAvailabilityConfirmation = new CommandOrderTiki_GetListAllOrderNeedAvailabilityConfirmation(this);
            pcommandOrderTiki_GetOrderDetail = new CommandOrderTiki_GetOrderDetail(this);

            // Lấy danh sách cửa hàng
            listHomeAdressShopUsing = CommonTikiAPI.GetListHomeAddressUsing();
            // Thêm tùy chọn tất cả shop nếu sanh sách shop có từ 2 shop trở lên
            if(listHomeAdressShopUsing.Count() > 1)
                listHomeAdressShopUsing.Add("Tất cả");
            homeAddressIndex = listHomeAdressShopUsing.Count() - 1;
            isEnabledButtons = true;
            if (homeAddressIndex == -1)
                isEnabledButtons = false;
            listOrder = new ObservableCollection<TikiOrderViewBinding>();
            currentSelecteOrder = new TikiOrderViewBinding();
        }
        private CommandOrderTiki_GetListAllOrderNeedAvailabilityConfirmation pcommandGetListAllOrderNeedAvailabilityConfirmation;
        public CommandOrderTiki_GetListAllOrderNeedAvailabilityConfirmation commandGetListAllOrderNeedAvailabilityConfirmation
        {
            get
            {
                return pcommandGetListAllOrderNeedAvailabilityConfirmation;
            }
        }

        private CommandOrderTiki_GetOrderDetail pcommandOrderTiki_GetOrderDetail;
        public CommandOrderTiki_GetOrderDetail commandOrderTiki_GetOrderDetail
        {
            get
            {
                return pcommandOrderTiki_GetOrderDetail;
            }
        }

        private ObservableCollection<string> plistHomeAdressShopUsing;
        public ObservableCollection<String> listHomeAdressShopUsing
        {
            get
            {
                return plistHomeAdressShopUsing;
            }
            set
            {
                if(plistHomeAdressShopUsing != value)
                {
                    OnPropertyChanged("listHomeAdressShopUsing");
                    plistHomeAdressShopUsing = value;
                }
            }
        }

        private string phomeAddressUsing;
        public string homeAddressUsing
        {
            get
            {
                return phomeAddressUsing;
            }

            set
            {
                if(phomeAddressUsing != value)
                {
                    OnPropertyChanged("homeAddressUsing");
                    phomeAddressUsing = value;
                }
            }
        }

        private int phomeAddressIndex;
        public int homeAddressIndex
        {
            get
            {
                return phomeAddressIndex;
            }

            set
            {
                if(phomeAddressIndex != value)
                {
                    OnPropertyChanged("homeAddressIndex");
                    phomeAddressIndex = value;
                }
            }
        }

        private bool pisEnabledButtons;
        public bool isEnabledButtons
        {
            get
            {
                return pisEnabledButtons;
            }

            set
            {
                if(pisEnabledButtons != value)
                {
                    OnPropertyChanged("isEnabledButtons");
                    pisEnabledButtons = value;
                }
            }
        }

        private ObservableCollection<TikiOrderViewBinding> plistOrder;
        public ObservableCollection<TikiOrderViewBinding> listOrder
        {
            get
            {
                return plistOrder;
            }

            set
            {
                if(plistOrder != value)
                {
                    OnPropertyChanged("listOrder");
                    plistOrder = value;
                }
            }
        }

        private TikiOrderViewBinding pcurrentSelecteOrder;
        public TikiOrderViewBinding currentSelecteOrder
        {
            get
            {
                return pcurrentSelecteOrder;
            }

            set
            {
                if(pcurrentSelecteOrder != value)
                {
                    OnPropertyChanged("currentSelecteOrder");
                    pcurrentSelecteOrder = value;
                }
            }
        }

        private string ptextOrderCodeGetDetail;
        public string textOrderCodeGetDetail
        {
            get
            {
                return ptextOrderCodeGetDetail;
            }

            set
            {
                if(ptextOrderCodeGetDetail != value)
                {
                    ptextOrderCodeGetDetail = value;
                    OnPropertyChanged("textOrderCodeGetDetail");
                }
            }
        }

        /// <summary>
        /// Lấy danh sách tất cả đơn hàng cần xác nhận còn hàng theo 1 shop, hoặc tất cả các shop
        /// </summary>
        public void GetListAllOrderNeedAvailabilityConfirmation()
        {
            listOrder.Clear();
            if (homeAddressIndex == -1)
                return;

            List<Order> lsOrderFullInfo = null;
            // Lấy đơn hàng của tất cả các shop
            if (listHomeAdressShopUsing.Count() > 1 &&
               homeAddressIndex == listHomeAdressShopUsing.Count() - 1)
            {
                lsOrderFullInfo = TikiGetListOrders.GetListAllOrderNeedAvailabilityConfirmationAllShops(CommonTikiAPI.listTikiConfigAppUsing);
            }
            else
            {
                // Lấy đơn hàng của 1 shop
                lsOrderFullInfo = TikiGetListOrders.GetListAllOrderNeedAvailabilityConfirmationOneShop(CommonTikiAPI.GetTikiConfigAppFromHomeAddress(homeAddressUsing));
            }

            foreach( Order e in lsOrderFullInfo)
            {
                listOrder.Add(new TikiOrderViewBinding(e));
            }
        }

        public void GetOrderDetail()
        {
            MessageBox.Show(textOrderCodeGetDetail);
        }
    }
}
