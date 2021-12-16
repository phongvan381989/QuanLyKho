using QuanLyKho.Model;
using QuanLyKho.Model.InOutWarehouse;
using QuanLyKho.ViewModel.Dev.TikiAPI.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyKho.ViewModel.Products
{
    public class ViewModelProductMappingProductInWarehouse : ViewModelBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productTMDTCode">mã sản phẩm trên shop TMDT</param>
        /// <param name="quantity">Số lượng</param>
        public ViewModelProductMappingProductInWarehouse(string productTMDTCode, ViewModelProductViewBindingTiki inputParent)
        {
            // Từ mã sản phẩm vào bảng map lấy được danh sách sản phẩm trong kho tương ứng
            listProductMapping = new ObservableCollection<ViewModelProductMappingProductInWarehouseViewBinding>();
            List<ModelMappingSanPhamTMDT_SanPhamKho> ls = ModelMappingSanPhamTMDT_SanPhamKho.GetListModelMappingSanPhamTMDT_SanPhamKhoFromID(((App)Application.Current).actionModelMappingSanPhamTMDT_SanPhamKho, productTMDTCode);
            foreach (ModelMappingSanPhamTMDT_SanPhamKho e in ls)
            {
                listProductMapping.Add(new ViewModelProductMappingProductInWarehouseViewBinding(e));
            }
            parent = inputParent;
            selectedIndex = -1;
        }
        public ViewModelProductViewBindingTiki parent;
        private int pselectedIndex;
        public int selectedIndex
        {
            get
            {
                return pselectedIndex;
            }

            set
            {
                if(pselectedIndex != value)
                {
                    pselectedIndex = value;
                    OnPropertyChanged("selectedIndex");
                    parent.parent.indexProductInList = parent.index - 1;
                }
            }
        }

        private ObservableCollection<ViewModelProductMappingProductInWarehouseViewBinding> plistProductMapping;
        public ObservableCollection<ViewModelProductMappingProductInWarehouseViewBinding> listProductMapping
        {
            get
            {
                return plistProductMapping;
            }

            set
            {
                if (plistProductMapping != value)
                {
                    plistProductMapping = value;
                    OnPropertyChanged("listProductMapping");
                }
            }
        }
    }
}
