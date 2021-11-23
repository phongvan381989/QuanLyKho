using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel.Orders
{
    public class CommandOrderTiki_GetOrderDetail : ICommand
    {
        private ViewModelOrderTiki vm;

        public CommandOrderTiki_GetOrderDetail(ViewModelOrderTiki vmInput)
        {
            vm = vmInput;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            vm.GetOrderDetail();
        }
        public event EventHandler CanExecuteChanged;
    }
}
