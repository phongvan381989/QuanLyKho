using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel.InOutWarehouse
{
    public class CommandMedia_RotateLeft : ICommand
    {
        private ViewModelMedia vm;

        public CommandMedia_RotateLeft(ViewModelMedia vmInput)
        {
            vm = vmInput;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            vm.RotateLeft(parameter);
        }
        public event EventHandler CanExecuteChanged;
    }
}
