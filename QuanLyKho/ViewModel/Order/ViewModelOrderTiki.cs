using QuanLyKho.ViewModel.Dev.TikiAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.ViewModel.Order
{
    public class ViewModelOrderTiki : ViewModelBase
    {
        public ViewModelOrderTiki()
        {
            pcommandGetListAllOrderNeedAvailabilityConfirmation = new CommandOrderTiki_GetListAllOrderNeedAvailabilityConfirmation(this);

            // Lấy danh sách cửa hàng
            listHomeAdressShopUsing = CommonTikiAPI.GetListHomeAddressUsing();
            // Thêm tùy chọn tất cả shop nếu sanh sách shop có từ 2 shop trở lên
            if(listHomeAdressShopUsing.Count() > 1)
                listHomeAdressShopUsing.Add("Tất cả");
            homeAddressIndex = listHomeAdressShopUsing.Count() - 1;
            isEnabledButtons = true;
            if (homeAddressIndex == -1)
                isEnabledButtons = false;
        }
        private CommandOrderTiki_GetListAllOrderNeedAvailabilityConfirmation pcommandGetListAllOrderNeedAvailabilityConfirmation;
        public CommandOrderTiki_GetListAllOrderNeedAvailabilityConfirmation commandGetListAllOrderNeedAvailabilityConfirmation
        {
            get
            {
                return pcommandGetListAllOrderNeedAvailabilityConfirmation;
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

        public void GetListAllOrderNeedAvailabilityConfirmation()
        {
            int x = 10;
            x = 2 * x;
        }
    }
}
