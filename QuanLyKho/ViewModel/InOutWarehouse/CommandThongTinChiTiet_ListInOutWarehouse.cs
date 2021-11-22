﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel.InOutWarehouse
{
    public class CommandThongTinChiTiet_ListInOutWarehouse : ICommand
    {
        private ViewModelThongTinChiTiet vm;

        public CommandThongTinChiTiet_ListInOutWarehouse(ViewModelThongTinChiTiet vmInput)
        {
            vm = vmInput;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            vm.GetListInOutWarehouse();
        }
        public event EventHandler CanExecuteChanged;
    }
}
