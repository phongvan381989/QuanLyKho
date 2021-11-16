using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel.InOutWarehouse
{
    public class CommandListInOutWarehouse_Search : ICommand
    {
        private ViewModelListInOutWarehouse vm;

        public CommandListInOutWarehouse_Search(ViewModelListInOutWarehouse vmInput)
        {
            vm = vmInput;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            vm.Search();
        }
        public event EventHandler CanExecuteChanged;
    }
}
