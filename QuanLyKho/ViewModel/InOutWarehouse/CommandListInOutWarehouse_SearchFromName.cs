using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel.InOutWarehouse
{
    public class CommandListInOutWarehouse_SearchFromName : ICommand
    {
        private ViewModelListInOutWarehouse vm;

        public CommandListInOutWarehouse_SearchFromName(ViewModelListInOutWarehouse vmInput)
        {
            vm = vmInput;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            vm.SearchFromName();
        }
        public event EventHandler CanExecuteChanged;
    }
}
