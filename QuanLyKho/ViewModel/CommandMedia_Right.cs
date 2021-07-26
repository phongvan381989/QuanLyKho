using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    class CommandMedia_Right : ICommand
    {
        private ViewModelMedia vm;

        public CommandMedia_Right(ViewModelMedia vmInput)
        {
            vm = vmInput;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            vm.Right();
        }
        public event EventHandler CanExecuteChanged;
    }
}
