using QuanLyKho.Model;
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
            _commandSearch = new CommandListInOutWarehouse_Search(this);
            indexInList = -1;
            textProductCode = string.Empty;
            actionModelThongTinChiTiet = new XMLAction(((App)Application.Current).GetPathDataXMLThongTinChiTiet());
            listProductInOutWareHouse = new ObservableCollection<ProductInOutWarehoseViewBinding>();
            lsTTCT = ModelThongTinChiTiet.GetListProductFromCode(actionModelThongTinChiTiet, "");
            Search();
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

        private CommandListInOutWarehouse_Search _commandSearch;
        public CommandListInOutWarehouse_Search commandSearch
        {
            get
            {
                return _commandSearch;
            }
        }

        XMLAction actionModelThongTinChiTiet;

        /// <summary>
        /// Cache lưu tất cả sản phẩm trong kho lấy cho nhanh
        /// </summary>
        List<ModelThongTinChiTiet> lsTTCT;
        public void Search()
        {
            listProductInOutWareHouse.Clear();
            if (string.IsNullOrEmpty(textProductCode)) // Lấy tất cả danh sách
            {
                foreach (ModelThongTinChiTiet e in lsTTCT)
                {
                    listProductInOutWareHouse.Add(new ProductInOutWarehoseViewBinding(e.maSanPham, e.tenSanPham, e.tonKho));
                }
            }
            else
            {
                foreach (ModelThongTinChiTiet e in lsTTCT)
                {
                    if(e.maSanPham.Equals(textProductCode))
                        listProductInOutWareHouse.Add(new ProductInOutWarehoseViewBinding(e.maSanPham, e.tenSanPham, e.tonKho));
                }
            }
        }
    }
}
