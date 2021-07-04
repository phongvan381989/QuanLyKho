using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    public class CommandThongTinChiTiet_Save: ICommand
    {
        private ViewModelThongTinChiTiet vm;

        public CommandThongTinChiTiet_Save(ViewModelThongTinChiTiet vmInput)
        {
            vm = vmInput;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            vm.Save();
        }
        public event EventHandler CanExecuteChanged;
    }
}
