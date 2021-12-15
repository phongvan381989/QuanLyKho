using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel.Products.Tiki
{
    public class CommandProductTiki_SearchCodeFromCache : ICommand
    {
        private ViewModelProductTiki vm;

        public CommandProductTiki_SearchCodeFromCache(ViewModelProductTiki vmInput)
        {
            vm = vmInput;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            vm.SearchCodeFromCache();
        }
        public event EventHandler CanExecuteChanged;
    }
}
