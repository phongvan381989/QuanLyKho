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
            listShopUsing = new ObservableCollection<string>();
            listShopUsing.Add("shop1");
            listShopUsing.Add("shop2");
            listShopUsing.Add("shop3");
        }
        private CommandOrderTiki_GetListAllOrderNeedAvailabilityConfirmation pcommandGetListAllOrderNeedAvailabilityConfirmation;
        public CommandOrderTiki_GetListAllOrderNeedAvailabilityConfirmation commandGetListAllOrderNeedAvailabilityConfirmation
        {
            get
            {
                return pcommandGetListAllOrderNeedAvailabilityConfirmation;
            }
        }

        public ObservableCollection<String> listShopUsing;
        public void GetListAllOrderNeedAvailabilityConfirmation()
        {
            int x = 10;
            x = 2 * x;
        }
    }
}
