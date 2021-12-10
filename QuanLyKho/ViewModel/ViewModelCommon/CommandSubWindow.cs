using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKho.ViewModel.ViewModelCommon
{
    public class CommandSubWindow : ICommand
    {
        private ViewModelSubWindow vm;

        public CommandSubWindow(ViewModelSubWindow vmInput)
        {
            vm = vmInput;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            vm.Escapse((Window)parameter);
        }
        public event EventHandler CanExecuteChanged;
    }
}
