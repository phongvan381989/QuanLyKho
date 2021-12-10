using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel.Orders
{
    public class CommandOrderTiki_GetListAllOrderNeedAvailabilityConfirmation : ICommand
    {
        private ViewModelOrderTiki vm;

        public CommandOrderTiki_GetListAllOrderNeedAvailabilityConfirmation(ViewModelOrderTiki vmInput)
        {
            vm = vmInput;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            vm.GetListOrder();
        }
        public event EventHandler CanExecuteChanged;
    }
}
