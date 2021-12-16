using QuanLyKho.Model;
using QuanLyKho.Model.InOutWarehouse;
using QuanLyKho.ViewModel.Dev.TikiAPI.Orders;
using QuanLyKho.ViewModel.Orders.Tiki;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyKho.ViewModel.Orders
{
    /// <summary>
    /// Phục vụ giao diện check đã lấy đủ số lượng của 1 sản phẩm từ kho cho 1 đơn hàng
    /// VD: 1/3 tức cần 3 sản phẩm xuất kho cho đơn hàng nhưng đã check được 1 sản phẩm
    /// </summary>
    public class ViewModelOrderCheckProductInWarehouse : ViewModelBase
    {
        // Không phải thay đổi bằng cách click chuột trươc tiếp vào checkbox, ta disable hàm Check
        public bool isDisableCheckFunction;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productTMDTCode">mã sản phẩm trên shop TMDT</param>
        /// <param name="quantity">Số lượng</param>
        public ViewModelOrderCheckProductInWarehouse(string productTMDTCode, int quantity, ViewModelProductInOrderViewBindingTiki inputParent)
        {
            // Từ mã sản phẩm vào bảng map lấy được danh sách sản phẩm trong kho tương ứng
            listCheckProduct = new ObservableCollection<ViewModelOrderCheckProductInWarehouseViewBinding>();
            List<ModelMappingSanPhamTMDT_SanPhamKho> ls = ModelMappingSanPhamTMDT_SanPhamKho.GetListModelMappingSanPhamTMDT_SanPhamKhoFromID(((App)Application.Current).actionModelMappingSanPhamTMDT_SanPhamKho, productTMDTCode);
            int indexTemp = -1;
            foreach (ModelMappingSanPhamTMDT_SanPhamKho e in ls)
            {
                indexTemp++;
                listCheckProduct.Add(new ViewModelOrderCheckProductInWarehouseViewBinding(e, quantity, indexTemp));
            }
            parent = inputParent;
            isDisableCheckFunction = false;
        }

        private ViewModelProductInOrderViewBindingTiki parent;

        private ObservableCollection<ViewModelOrderCheckProductInWarehouseViewBinding> plistCheckProduct;
        public ObservableCollection<ViewModelOrderCheckProductInWarehouseViewBinding> listCheckProduct
        {
            get
            {
                return plistCheckProduct;
            }

            set
            {
                if(plistCheckProduct != value)
                {
                    plistCheckProduct = value;
                    OnPropertyChanged("listCheckProduct");
                }
            }
        }

        private ViewModelOrderCheckProductInWarehouseViewBinding pitemSelected;
        public ViewModelOrderCheckProductInWarehouseViewBinding itemSelected
        {
            get
            {
                return pitemSelected;
            }

            set
            {
                if(pitemSelected != value)
                {
                    pitemSelected = value;
                    OnPropertyChanged("itemSelected");
                }
            }
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

        public void Check()
        {
            if (isDisableCheckFunction)
                return;

            itemSelected = listCheckProduct[ViewModelOrderCheckProductInWarehouseViewBinding.indexCheck];
            itemSelected.Update();
            OnPropertyChanged("itemSelected");
            UpdateStatusOfRowParent();
        }

        /// <summary>
        /// Update trạng thái kiểm số lượng sản phẩm trong kho trong đơn khi check/uncheck
        /// </summary>
        public void UpdateWhenCheckedFromParent(bool isChecked)
        {
            isDisableCheckFunction = true;
            foreach (ViewModelOrderCheckProductInWarehouseViewBinding e in listCheckProduct)
            {
                e.isChecked = isChecked;
                e.Update();
            }
            isDisableCheckFunction = false;
        }

        /// <summary>
        /// Check khi thêm 1 sản phẩm vào đơn hàng
        /// </summary>
        /// <param name="code"></param>
        /// <returns>1: sản phẩm không có trong đơn tương ứng 1 sản phẩm TMDT, 2: sản phẩm vượt số lượng, 0: thành công</returns>
        public int AddProduct(string inputCode)
        {
            int result = 1;
            foreach (ViewModelOrderCheckProductInWarehouseViewBinding e in listCheckProduct)
            {
                if(e.code == inputCode)
                {
                    if(e.checkedQuantity < e.needQuantity)
                    {
                        e.checkedQuantity++;
                        result = 0;
                        break;
                    }
                    else
                    {
                        result = 2;
                        break;
                    }
                }
            }
            if(result == 0)// Thêm sản phẩm thành công
                UpdateStatusOfRowParent();
            return result;
        }

        ///// <summary>
        ///// Kiểm tra sản phẩm đã được chọn đủ
        ///// </summary>
        ///// <returns></returns>
        //public bool CheckFull()
        //{
        //    bool isFull = true;
        //    foreach (ViewModelOrderCheckProductInWarehouseViewBindingTiki e in listCheckProduct)
        //    {
        //        if (e.isChecked == false)
        //        {
        //            isFull = false;
        //            break;
        //        }
        //    }
        //    return isFull;
        //}

        /// <summary>
        /// Kiểm tra sản phẩm đã được chọn đủ và cập nhật checkbox của row cha
        /// </summary>
        private void UpdateStatusOfRowParent()
        {
            // Kiểm tra các sản phẩm đều đã đủ
            bool isFull = true;
            foreach (ViewModelOrderCheckProductInWarehouseViewBinding e in listCheckProduct)
            {
                if (e.isChecked == false)
                {
                    isFull = false;
                    break;
                }
            }
            parent.UpdateIsCheckFromChildren(isFull);

        }
    }
}
