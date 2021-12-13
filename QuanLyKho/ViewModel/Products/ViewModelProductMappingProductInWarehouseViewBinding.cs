using QuanLyKho.Model.InOutWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.ViewModel.Products
{
    public class ViewModelProductMappingProductInWarehouseViewBinding
    {
        public ViewModelProductMappingProductInWarehouseViewBinding(ModelMappingSanPhamTMDT_SanPhamKho e)
        {

            code = e.code;
            name = e.name;
            positionInWarehouse = e.position;
            quantity = e.quantity;
            
        }
        public string code { get; set; }

        public string name { get; set; }

        public string positionInWarehouse { set; get; }

        public string quantity { set; get; }
    }
}
