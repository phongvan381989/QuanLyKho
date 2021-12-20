using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel.Orders.Tiki
{
    public class CommandProductInOrderTiki_ProductFull : ICommand
    {
        private ViewModelProductInOrderTiki vm;

        public CommandProductInOrderTiki_ProductFull(ViewModelProductInOrderTiki vmInput)
        {
            vm = vmInput;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            vm.ProductFull();
        }
        public event EventHandler CanExecuteChanged;
    }
}
