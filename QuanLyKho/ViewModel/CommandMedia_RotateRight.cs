using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    public class CommandMedia_RotateRight :ICommand
    {
        private ViewModelMedia vm;

        public CommandMedia_RotateRight(ViewModelMedia vmInput)
        {
            vm = vmInput;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            vm.RotateRight(parameter);
        }
        public event EventHandler CanExecuteChanged;
    }
}
