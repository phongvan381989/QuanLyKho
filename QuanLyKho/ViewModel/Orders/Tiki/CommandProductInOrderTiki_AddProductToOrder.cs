using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel.Orders.Tiki
{
    public class CommandProductInOrderTiki_AddProductToOrder : ICommand
    {
        private ViewModelProductInOrderTiki vm;

        public CommandProductInOrderTiki_AddProductToOrder(ViewModelProductInOrderTiki vmInput)
        {
            vm = vmInput;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            vm.AddProductToOrder();
        }
        public event EventHandler CanExecuteChanged;
    }
}
