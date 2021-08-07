using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    public class CommandListBoxSearch_Collapse : ICommand
    {
        private ViewModelListBoxSearch vm;

        public CommandListBoxSearch_Collapse(ViewModelListBoxSearch vmInput)
        {
            vm = vmInput;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            vm.Collapse();
        }
        public event EventHandler CanExecuteChanged;
    }
}
