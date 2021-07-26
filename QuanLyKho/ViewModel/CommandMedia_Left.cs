using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    class CommandMedia_Left : ICommand
    {
        private ViewModelMedia vm;

        public CommandMedia_Left(ViewModelMedia vmInput)
        {
            vm = vmInput;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            vm.Left();
        }
        public event EventHandler CanExecuteChanged;
    }
}
