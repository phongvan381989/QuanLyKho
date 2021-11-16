using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void Search()
        {

        }
    }
}
