using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel.Config
{
    public class CommandTikiConfigApp_AddOrUpdate : ICommand
    {
        private ViewModelTikiConfigApp vm;

        public CommandTikiConfigApp_AddOrUpdate(ViewModelTikiConfigApp vmInput)
        {
            vm = vmInput;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            vm.AddOrUpdate();
        }
        public event EventHandler CanExecuteChanged;
    }
}
