using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel.InOutWarehouse
{
    public class CommandThongTinChiTiet_AddMoreMaSP : ICommand
    {
        private ViewModelThongTinChiTiet vm;

        public CommandThongTinChiTiet_AddMoreMaSP(ViewModelThongTinChiTiet vmInput)
        {
            vm = vmInput;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            vm.AddMoreMaSP();
        }
        public event EventHandler CanExecuteChanged;
    }
}
