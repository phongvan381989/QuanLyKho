using QuanLyKho.General;
using QuanLyKho.Model;
using QuanLyKho.Model.Dev.TikiApp.Orders;
using QuanLyKho.Model.InOutWarehouse;
using QuanLyKho.View.InOutWarehouse;
using QuanLyKho.View.Order;
using QuanLyKho.View.UserControlCommon;
using QuanLyKho.ViewModel.Dev.TikiAPI.Orders;
using QuanLyKho.ViewModel.InOutWarehouse;
using QuanLyKho.ViewModel.Orders.Tiki;
using QuanLyKho.ViewModel.ViewModelCommon;
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
        // Không phải thay đổi bằng cách click chuột trươc tiếp vào checkbox, ta disable hàm Check
        public bool isDisableCheckFunction;
        public ViewModelProductInOrderTiki(Order order, SubWindow inputParentWidow)
        {
            listProductTMDTInOrder = new ObservableCollection<ViewModelProductInOrderViewBindingTiki>();
            int index = -1;
            foreach (OrderItemV2 item in order.items)
            {
                index++;
                listProductTMDTInOrder.Add(new ViewModelProductInOrderViewBindingTiki(item, index, this));
            }
            commandAddProductToOrder = new CommandProductInOrderTiki_AddProductToOrder(this);
            commandProductFull = new CommandProductInOrderTiki_ProductFull(this);
            isDisableCheckFunction = false;
            parentWindow = inputParentWidow;
        }
        public CommandProductInOrderTiki_AddProductToOrder commandAddProductToOrder { get; set; }
        public CommandProductInOrderTiki_ProductFull commandProductFull { get; set; }


        private SubWindow parentWindow;
        public void Check()
        {
            itemSelected = listProductTMDTInOrder[ViewModelProductInOrderViewBindingTiki.indexCheck];
            OnPropertyChanged("itemSelected");
            if (isDisableCheckFunction)
            {
                isDisableCheckFunction = false;
            }
            else
            {
                itemSelected.UpdateWhenCheckedFromParent();
            }

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
                MessageBox.Show("Mã sản phẩm kiểm tra không có trong đơn hàng", "Kiểm Tra Sản Phẩm Trong Đơn");
            }
            else if(result == 2)
            {
                MessageBox.Show("Mã sản phẩm kiểm tra đã đủ số lượng", "Kiểm Tra Sản Phẩm Trong Đơn");
            }
        }

        public void ProductFull()
        {
            // Nếu sản phẩm đã chọn đủ. Ta hiện thông báo đã chọn đủ sản phẩm và lưu dữ liệu xuất kho
            bool isFull = true;
            foreach (ViewModelProductInOrderViewBindingTiki e in listProductTMDTInOrder)
            {
                if (e.isChecked == false)
                {
                    isFull = false;
                    break;
                }
            }

            if (isFull)
            {
                // Lưu thông tin nhập xuất chi tiết
                List<string> lsMaSanPham = new List<string>();
                List<string> lsSoLuongNhap = new List<string>();
                int count, i;
                foreach (ViewModelProductInOrderViewBindingTiki e in listProductTMDTInOrder)
                {
                    if(e.vmOrderCheck.listCheckProduct.Count() == 0)
                    {
                            MessageBox.Show("Sản phẩm " + e.idInShop +" trên shop TMDT chưa được gắn với sản phẩm trong kho.", "Kiểm Tra Sản Phẩm Trong Đơn");
                            return;
                    }
                    foreach (ViewModelOrderCheckProductInWarehouseViewBinding ee in e.vmOrderCheck.listCheckProduct)
                    {
                        // Check mã sản phẩm đã được thêm vào danh sách chưa
                        count = lsMaSanPham.Count();
                        for(i = 0; i < count; i++)
                        {
                            if (ee.code == lsMaSanPham[i])
                                break;
                        }
                        if (i == count) // Mã sản phẩm chưa được thêm vào danh sách
                        {
                            lsMaSanPham.Add(ee.code);
                            lsSoLuongNhap.Add((ee.needQuantity * -1).ToString());
                        }
                        else
                        {
                            lsSoLuongNhap[i] = (Int32.Parse(lsSoLuongNhap[i]) - ee.needQuantity).ToString();
                        }
                    }
                }

                // Check trong kho đủ số lượng sản phẩm ko?
                count = lsMaSanPham.Count();

                for (i = 0; i < count; i++)
                {
                    if(!ModelThongTinChiTiet.CheckQuantityEnough(((App)Application.Current).actionModelThongTinChiTiet, lsMaSanPham[i], lsSoLuongNhap[i]))
                    {
                        if (string.IsNullOrEmpty(Common.CommonErrorMessage))
                        {
                            MessageBox.Show("Cập nhật tồn kho lỗi.", "Kiểm Tra Sản Phẩm Trong Đơn");
                        }
                        else
                        {
                            MessageBox.Show(Common.CommonErrorMessage, "Kiểm Tra Sản Phẩm Trong Đơn");
                            Common.CommonErrorMessage = string.Empty;
                        }
                        return;
                    }
                }

                if(!ModelNhapXuatChiTiet.AddOrUpdateListProduceToXDocAndSave(((App)Application.Current).actionModelNhapXuatChiTiet, lsMaSanPham, lsSoLuongNhap))
                {
                    MessageBox.Show("Lưu thông tin nhập xuất chi tiết thất bại.", "Kiểm Tra Sản Phẩm Trong Đơn");
                    return;
                }

                // Lưu thông tin tồn kho
                if(!ModelThongTinChiTiet.UpdateTonKhoListProduct(((App)Application.Current).actionModelThongTinChiTiet, lsMaSanPham, lsSoLuongNhap))
                {
                    if (string.IsNullOrEmpty(Common.CommonErrorMessage))
                    {
                        MessageBox.Show("Cập nhật tồn kho lỗi.", "Kiểm Tra Sản Phẩm Trong Đơn");
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật tồn kho lỗi. " + Common.CommonErrorMessage, "Kiểm Tra Sản Phẩm Trong Đơn");
                        Common.CommonErrorMessage = string.Empty;
                    }
                    return;
                }

                // Hiện thông báo
                Common.ShowAutoClosingMessageBox("Đơn hàng đã đủ sản phẩm.", "Kiểm Tra Sản Phẩm Trong Đơn");

                // Đóng window hiện tại
                if (parentWindow != null)
                    parentWindow.Close();
            }
            else
            {
                MessageBox.Show("Đơn hàng chưa đủ sản phẩm.", "Kiểm Tra Sản Phẩm Trong Đơn");
            }
        }

        public void GetProductInShopTMDTDetail()
        {
            SubWindow wd = new SubWindow();
            wd.DataContext = new ViewModelSubWindow();
            wd.GetContainerContent().Children.Add(new UserControlMappingSanPhamTMDT_SanPhamKho());
            wd.GetContainerContent().DataContext = new ViewModelMappingSanPhamTMDT_SanPhamKho(itemSelected.idInShop.ToString(), itemSelected.name);
            wd.WindowState = WindowState.Maximized;
            wd.Title = "Thông Tin Liên Kết Sản Phẩm Tiki và Kho Thực Tế";
            wd.ShowDialog();

            // Load lại dữ liệu
            ViewModelOrderCheckProductInWarehouse obj = new ViewModelOrderCheckProductInWarehouse(itemSelected.idInShop.ToString(), itemSelected.amount, itemSelected);
            itemSelected.vmOrderCheck = obj;
            itemSelected.isChecked = false;
        }
    }
}
