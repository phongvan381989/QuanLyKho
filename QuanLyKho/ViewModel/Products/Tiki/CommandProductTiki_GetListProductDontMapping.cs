using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel.Products.Tiki
{
    public class CommandProductTiki_GetListProductDontMapping : ICommand
    {
        private ViewModelProductTiki vm;

        public CommandProductTiki_GetListProductDontMapping(ViewModelProductTiki vmInput)
        {
            vm = vmInput;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            vm.GetListProductDontMapping();
        }
        public event EventHandler CanExecuteChanged;
    }
}
