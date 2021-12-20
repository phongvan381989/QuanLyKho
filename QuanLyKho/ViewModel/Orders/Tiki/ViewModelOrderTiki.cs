using QuanLyKho.General;
using QuanLyKho.Model.Dev.TikiApp.Orders;
using QuanLyKho.View.Order;
using QuanLyKho.View.Order.Tiki;
using QuanLyKho.View.UserControlCommon;
using QuanLyKho.ViewModel.Dev.TikiAPI;
using QuanLyKho.ViewModel.Dev.TikiAPI.Orders;
using QuanLyKho.ViewModel.ViewModelCommon;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static QuanLyKho.Model.Dev.TikiApp.Orders.OrderItemFilterByDate;

namespace QuanLyKho.ViewModel.Orders
{
    public class ViewModelOrderTiki : ViewModelBase
    {

        public ViewModelOrderTiki()
        {
            pcommandGetListOrder = new CommandOrderTiki_GetListAllOrderNeedAvailabilityConfirmation(this);
            pcommandOrderTiki_GetOrderDetail = new CommandOrderTiki_GetOrderDetail(this);

            // Lấy danh sách cửa hàng
            listHomeAddressShopUsing = CommonTikiAPI.GetListHomeAddressUsing();
            // Thêm tùy chọn tất cả shop nếu danh sách shop có từ 2 shop trở lên
            if(listHomeAddressShopUsing.Count() > 1)
                listHomeAddressShopUsing.Add("Tất cả");
            homeAddressIndex = listHomeAddressShopUsing.Count() - 1;
            isEnabledButtons = true;
            if (homeAddressIndex == -1)
                isEnabledButtons = false;
            listOrder = new ObservableCollection<TikiOrderViewBinding>();
            currentSelecteOrder = new TikiOrderViewBinding();
            lsOrderFullInfo = new List<Order>();
            indexOrderInList = -1;
            interval = (int)EnumOrderItemFilterByDate.today;
        }
        private CommandOrderTiki_GetListAllOrderNeedAvailabilityConfirmation pcommandGetListOrder;
        public CommandOrderTiki_GetListAllOrderNeedAvailabilityConfirmation commandGetListOrder
        {
            get
            {
                return pcommandGetListOrder;
            }
        }

        private int pinterval;
        public int interval
        {
            get
            {
                return pinterval;
            }
            set
            {
                if (pinterval != value)
                {
                    pinterval = value;
                    OnPropertyChanged("interval");
                }
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

        private ObservableCollection<string> plistHomeAddressShopUsing;
        public ObservableCollection<String> listHomeAddressShopUsing
        {
            get
            {
                return plistHomeAddressShopUsing;
            }
            set
            {
                if(plistHomeAddressShopUsing != value)
                {
                    OnPropertyChanged("listHomeAddressShopUsing");
                    plistHomeAddressShopUsing = value;
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

        private List<Order> lsOrderFullInfo;

        /// <summary>
        /// Lấy danh sách tất cả đơn hàng cần xác nhận còn hàng theo 1 shop, hoặc tất cả các shop
        /// </summary>
        public void GetListOrder()
        {
            listOrder.Clear();
            if (homeAddressIndex == -1)
                return;

            // Lấy đơn hàng của tất cả các shop
            if (listHomeAddressShopUsing.Count() > 1 &&
               homeAddressIndex == listHomeAddressShopUsing.Count() - 1)
            {
                lsOrderFullInfo = TikiGetListOrders.GetListOrderAllShops(CommonTikiAPI.listTikiConfigAppUsing, (EnumOrderItemFilterByDate)interval);
            }
            else
            {
                // Lấy đơn hàng của 1 shop
                lsOrderFullInfo = TikiGetListOrders.GetListOrderAShop(CommonTikiAPI.GetTikiConfigAppFromHomeAddress(homeAddressUsing), (EnumOrderItemFilterByDate)interval);
            }

            foreach( Order e in lsOrderFullInfo)
            {
                // Download thumbnail của sản phẩm
                foreach (OrderItemV2 eItem in e.items)
                {
                    Common.DownloadImageAndSave(eItem.product.thumbnail, ((App)Application.Current).temporaryImageFolderPath);
                }
                listOrder.Add(new TikiOrderViewBinding(e));
            }
        }

        private int pindexOrderInList;
        public int indexOrderInList
        {
            get
            {
                return pindexOrderInList;
            }
            set
            {
                if(pindexOrderInList  != value)
                {
                    pindexOrderInList = value;
                    OnPropertyChanged("indexOrderInList");
                }
            }
        }

        public void GetOrderDetail()
        {
            if(indexOrderInList == -1)
            {
                MessageBox.Show("Chưa chọn đơn.");
                return;
            }

            SubWindow wd = new SubWindow();
            wd.DataContext = new ViewModelSubWindow();

            wd.GetContainerContent().Children.Add(new UserControlProductInOrderTiki());
            wd.GetContainerContent().DataContext = new ViewModelProductInOrderTiki(lsOrderFullInfo[indexOrderInList], wd);
            wd.WindowState = WindowState.Maximized;
            wd.Title = "Kiểm Tra Số Lượng Sản Phẩm Trong Đơn";
            wd.ShowDialog();
            textOrderCodeGetDetail = string.Empty;
            indexOrderInList = -1;
        }

        public void RefreshView()
        {
            textOrderCodeGetDetail = string.Empty;
            indexOrderInList = -1;
            listOrder.Clear();
            lsOrderFullInfo.Clear();

        }
    }
}
