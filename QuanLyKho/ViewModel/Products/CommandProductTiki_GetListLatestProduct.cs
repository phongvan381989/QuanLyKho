using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel.Products
{
    public class CommandProductTiki_GetListLatestProduct : ICommand
    {
        private ViewModelProductTiki vm;

        public CommandProductTiki_GetListLatestProduct(ViewModelProductTiki vmInput)
        {
            vm = vmInput;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            vm.GetListLatestProduct();
        }
        public event EventHandler CanExecuteChanged;
    }
}
