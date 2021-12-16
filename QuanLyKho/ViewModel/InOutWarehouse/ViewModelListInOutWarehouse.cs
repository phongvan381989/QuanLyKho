using QuanLyKho.Model;
using QuanLyKho.View.InOutWarehouse;
using QuanLyKho.View.UserControlCommon;
using QuanLyKho.ViewModel.ViewModelCommon;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyKho.ViewModel.InOutWarehouse
{
    /// <summary>
    /// Danh sách sản phẩm trong kho
    /// </summary>
    public class ViewModelListInOutWarehouse : ViewModelBase
    {

        public ViewModelListInOutWarehouse()
        {
            _commandSearchFromCode = new CommandListInOutWarehouse_SearchFromCode(this);
            _commandSearchFromName = new CommandListInOutWarehouse_SearchFromName(this);
            indexInList = -1;
            textProductCode = string.Empty;
            listProductInOutWareHouse = new ObservableCollection<ProductInOutWarehoseViewBinding>();
            lsTTCT = ModelThongTinChiTiet.GetListProductFromCode(((App)Application.Current).actionModelThongTinChiTiet, "");
            SearchFromCode();
        }

        private int pindexInList;
        public int indexInList
        {
            get
            {
                return pindexInList;
            }
            set
            {
                if(pindexInList != value)
                {
                    pindexInList = value;
                    OnPropertyChanged("indexInList");
                }
            }
        }

        private string ptextProductCode;
        public string textProductCode
        {
            get
            {
                return ptextProductCode;
            }

            set
            {
                if(ptextProductCode != value)
                {
                    ptextProductCode = value;
                    OnPropertyChanged("textProductCode");
                }
            }
        }

        private ObservableCollection<ProductInOutWarehoseViewBinding> plistProductInOutWareHouse;
        public ObservableCollection<ProductInOutWarehoseViewBinding> listProductInOutWareHouse
        {
            get
            {
                return plistProductInOutWareHouse;
            }

            set
            {
                if (plistProductInOutWareHouse != value)
                {
                    OnPropertyChanged("listProductInOutWareHouse");
                    plistProductInOutWareHouse = value;
                }
            }
        }

        private string ptextProductName;
        public string textProductName
        {
            get
            {
                return ptextProductName;
            }

            set
            {
                if (ptextProductName != value)
                {
                    ptextProductName = value;
                    OnPropertyChanged("textProductName");
                }
            }
        }

        private ProductInOutWarehoseViewBinding pitemProduct;
        public ProductInOutWarehoseViewBinding itemProduct
        {
            get
            {
                return pitemProduct;
            }

            set
            {
                if(pitemProduct != value)
                {
                    pitemProduct = value;
                    OnPropertyChanged("itemProduct");
                    if (pitemProduct != null)
                    {
                        textProductCode = pitemProduct.code;
                        textProductName = pitemProduct.name;
                    }
                }
            }
        }

        private CommandListInOutWarehouse_SearchFromCode _commandSearchFromCode;
        public CommandListInOutWarehouse_SearchFromCode commandSearchFromCode
        {
            get
            {
                return _commandSearchFromCode;
            }
        }

        private CommandListInOutWarehouse_SearchFromName _commandSearchFromName;
        public CommandListInOutWarehouse_SearchFromName commandSearchFromName
        {
            get
            {
                return _commandSearchFromName;
            }
        }

        /// <summary>
        /// Cache lưu tất cả sản phẩm trong kho lấy cho nhanh
        /// </summary>
        List<ModelThongTinChiTiet> lsTTCT;
        public void SearchFromCode()
        {
            textProductName = string.Empty;
            listProductInOutWareHouse.Clear();
            if (string.IsNullOrEmpty(textProductCode)) // Lấy tất cả danh sách
            {
                int indexTemp = 0;
                foreach (ModelThongTinChiTiet e in lsTTCT)
                {
                    indexTemp++;
                    listProductInOutWareHouse.Add(new ProductInOutWarehoseViewBinding(indexTemp, e));
                }
            }
            else
            {
                int indexTemp = 0;
                foreach (ModelThongTinChiTiet e in lsTTCT)
                {
                    if (e.maSanPham.IndexOf(textProductCode, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        indexTemp++;
                        listProductInOutWareHouse.Add(new ProductInOutWarehoseViewBinding(indexTemp, e));
                    }
                }
            }
        }

        public void SearchFromName()
        {
            textProductCode = string.Empty;
            listProductInOutWareHouse.Clear();
            if (string.IsNullOrEmpty(textProductName)) // Lấy tất cả danh sách
            {
                int indexTemp = 0;
                foreach (ModelThongTinChiTiet e in lsTTCT)
                {
                    indexTemp++;
                    listProductInOutWareHouse.Add(new ProductInOutWarehoseViewBinding(indexTemp, e));
                }
            }
            else
            {
                int indexTemp = 0;
                foreach (ModelThongTinChiTiet e in lsTTCT)
                {
                    if (e.tenSanPham.IndexOf(textProductName, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        indexTemp++;
                        listProductInOutWareHouse.Add(new ProductInOutWarehoseViewBinding(indexTemp, e));
                    }
                }
            }
        }

        /// <summary>
        /// Hiển thị cửa sổ thông tin chi tiết sản phẩm trong kho
        /// </summary>
        public void GetProductInOutWarehosueDetail()
        {
            if (indexInList == -1)
            {
                MessageBox.Show("Chưa chọn sản phẩm.");
                return;
            }

            SubWindow wd = new SubWindow();
            wd.DataContext = new ViewModelSubWindow();

            wd.GetContainerContent().Children.Add(new UserControlThongTinChiTietViewOnly());
            wd.GetContainerContent().DataContext = new ViewModelThongTinChiTietViewOnly(textProductCode);
            wd.WindowState = WindowState.Maximized;
            wd.Title = "Thông Tin Chi Tiết Sản Phẩm";
            wd.ShowDialog();
        }
    }
}
