using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    class CommandThongTinChiTiet_Delete : ICommand
    {
        private ViewModelThongTinChiTiet vm;

        public CommandThongTinChiTiet_Delete(ViewModelThongTinChiTiet vmInput)
        {
            vm = vmInput;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            vm.Delete();
        }
        public event EventHandler CanExecuteChanged;
    }
}
