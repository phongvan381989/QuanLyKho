using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel.InOutWarehouse
{
    public class CommandMappingSanPhamTMDT_SanPhamKho_Save : ICommand
    {

        private ViewModelMappingSanPhamTMDT_SanPhamKho vm;

        public CommandMappingSanPhamTMDT_SanPhamKho_Save(ViewModelMappingSanPhamTMDT_SanPhamKho vmInput)
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
