using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel.Products.Tiki
{
    public class CommandProductTiki_SearchFromShopTMDT : ICommand
    {
        private ViewModelProductTiki vm;

        public CommandProductTiki_SearchFromShopTMDT(ViewModelProductTiki vmInput)
        {
            vm = vmInput;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            vm.SearchFromShopTMDT();
        }
        public event EventHandler CanExecuteChanged;
    }
}
