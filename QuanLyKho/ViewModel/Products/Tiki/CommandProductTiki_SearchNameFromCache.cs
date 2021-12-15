using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel.Products.Tiki
{
    public class CommandProductTiki_SearchNameFromCache : ICommand
    {
        private ViewModelProductTiki vm;

        public CommandProductTiki_SearchNameFromCache(ViewModelProductTiki vmInput)
        {
            vm = vmInput;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            vm.SearchNameFromCache();
        }
        public event EventHandler CanExecuteChanged;
    }
}
