using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel.InOutWarehouse
{
    public class CommandListInOutWarehouse_SearchFromCode : ICommand
    {
        private ViewModelListInOutWarehouse vm;

        public CommandListInOutWarehouse_SearchFromCode(ViewModelListInOutWarehouse vmInput)
        {
            vm = vmInput;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            vm.SearchFromCode();
        }
        public event EventHandler CanExecuteChanged;
    }
}
