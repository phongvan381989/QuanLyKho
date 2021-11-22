using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel.InOutWarehouse
{
    public class CommandMappingSanPhamTMDT_SanPhamKho_Delete : ICommand
    {

        private ViewModelMappingSanPhamTMDT_SanPhamKho vm;

        public CommandMappingSanPhamTMDT_SanPhamKho_Delete(ViewModelMappingSanPhamTMDT_SanPhamKho vmInput)
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
