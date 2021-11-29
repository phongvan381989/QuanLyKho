using QuanLyKho.Model;
using QuanLyKho.Model.Dev.TikiApp.Orders;
using QuanLyKho.View.InOutWarehouse;
using QuanLyKho.View.Order;
using QuanLyKho.ViewModel.Dev.TikiAPI.Orders;
using QuanLyKho.ViewModel.InOutWarehouse;
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
    public class ViewModelProductInOrderTiki : ViewModelBase
    {
        public ViewModelProductInOrderTiki(Order order)
        {
            listProductTMDTInOrder = new ObservableCollection<ViewModelProductInOrderViewBindingTiki>();
            int index = -1;
            foreach (OrderItemV2 item in order.items)
            {
                index++;
                listProductTMDTInOrder.Add(new ViewModelProductInOrderViewBindingTiki(item, index));
            }
            commandAddProductToOrder = new CommandProductInOrderTiki_AddProductToOrder(this);
        }

        public CommandProductInOrderTiki_AddProductToOrder commandAddProductToOrder { get; set; }
        /// <summary>
        /// Từ id sản phẩm shop TMDT lấy mapping sản phẩm trong kho.
        /// </summary>
        public void GetMapping()
        {
            if(itemSelected == null)
                return;

            Window wdOrderDetail = new Window
            {
                Content = new UserControlMappingSanPhamTMDT_SanPhamKho()
            };
            ViewModelMappingSanPhamTMDT_SanPhamKho vm = new ViewModelMappingSanPhamTMDT_SanPhamKho(itemSelected.idInShop.ToString(), itemSelected.name);
            vm.optionVisibility = Visibility.Collapsed;
            wdOrderDetail.DataContext = vm;
            wdOrderDetail.WindowState = WindowState.Maximized;
            wdOrderDetail.ShowDialog();
        }

        public void Check()
        {
            itemSelected = listProductTMDTInOrder[ViewModelProductInOrderViewBindingTiki.indexCheck];

            itemSelected.Update();
            OnPropertyChanged("itemSelected");
        }

        private string pcode;
        public string code
        {
            get
            {
                return pcode;
            }

            set
            {
                if(pcode != value)
                {
                    pcode = value;
                    OnPropertyChanged("code");
                }
            }
        }

        public ViewModelProductInOrderViewBindingTiki itemSelected {get; set; }

        private ObservableCollection<ViewModelProductInOrderViewBindingTiki> plistProductTMDTInOrder;
        public ObservableCollection<ViewModelProductInOrderViewBindingTiki> listProductTMDTInOrder
        {
            get
            {
                return plistProductTMDTInOrder;
            }

            set
            {
                if(plistProductTMDTInOrder != value)
                {
                    plistProductTMDTInOrder = value;
                    OnPropertyChanged("listProductTMDTInOrder;");
                }
            }
        }

        /// <summary>
        /// Thêm 1 sản phẩm vào đơn hàng.
        /// </summary>
        public void AddProductToOrder()
        {
            //MessageBox.Show(code);
            // Duyệt danh sách sản phẩm trong đơn, gặp sản phẩm đang add thì tăng số lượng lên 1
            // và không quá số lượng đơn hàng đã đặt.
            //ViewModelOrderCheckProductInWarehouseTiki orderCheckTemp;
            int result = 0;
            foreach (ViewModelProductInOrderViewBindingTiki ePIO in listProductTMDTInOrder)
            {

                int resultTemp = ePIO.vmOrderCheck.AddProduct(code);
                if (resultTemp == 0) // Thành công
                {

                    result = 0;
                    //if (ePIO.vmOrderCheck.CheckFull())
                    //    ePIO.isChecked = true;
                    break;
                }
                else if (resultTemp == 1) // Sản phẩm không có trong đơn
                {
                    if (result == 0)
                        result = 1;
                    continue;
                }
                else // Sản phẩm đã thêm đủ số lượng
                {
                    result = 2;
                }
            }
            if(result == 0)
            {
                code = string.Empty;
            }
            else if(result == 1)
            {
                MessageBox.Show("Mã sản phẩm kiểm tra không có trong đơn hàng");
            }
            else if(result == 2)
            {
                MessageBox.Show("Mã sản phẩm kiểm tra đã đủ số lượng");
            }
        }
    }
}
