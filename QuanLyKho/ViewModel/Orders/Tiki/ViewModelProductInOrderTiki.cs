﻿using QuanLyKho.General;
using QuanLyKho.Model;
using QuanLyKho.Model.Dev.TikiApp.Orders;
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
        public ViewModelProductInOrderTiki(Order order)
        {
            listProductTMDTInOrder = new ObservableCollection<ViewModelProductInOrderViewBindingTiki>();
            int index = -1;
            foreach (OrderItemV2 item in order.items)
            {
                index++;
                listProductTMDTInOrder.Add(new ViewModelProductInOrderViewBindingTiki(item, index, this));
            }
            commandAddProductToOrder = new CommandProductInOrderTiki_AddProductToOrder(this);
            isDisableCheckFunction = false;
        }
        public CommandProductInOrderTiki_AddProductToOrder commandAddProductToOrder { get; set; }

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

            // Nếu sản phẩm đã chọn đủ. Ta hiện thông báo đã chọn đủ sản phẩm và lưu dữ liệu xuất kho
            bool isFull = true;
            foreach(ViewModelProductInOrderViewBindingTiki e in listProductTMDTInOrder)
            {
                if (e.isChecked == false)
                {
                    isFull = false;
                    break;
                }
            }
            if(isFull)
            {
                Common.ShowAutoClosingMessageBox("Đơn hàng đã đủ sản phẩm.", "Kiểm Tra Sản Phẩm Trong Đơn");
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
