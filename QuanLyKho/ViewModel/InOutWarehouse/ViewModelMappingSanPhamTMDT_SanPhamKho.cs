using QuanLyKho.General;
using QuanLyKho.Model;
using QuanLyKho.Model.InOutWarehouse;
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
    public class ViewModelMappingSanPhamTMDT_SanPhamKho : ViewModelBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">mã sản phẩm trên shop TMDT</param>
        /// <param name="name">tên sản phẩm trên shop TMDT</param>
        public ViewModelMappingSanPhamTMDT_SanPhamKho(string code, string name)
        {
            vmListInOutWarehouse = new ViewModelListInOutWarehouse();
            indexInListOfProductOnTMDT = -1;
            textProductNameOnTMDT = name;

            textProductCodeOnTMDT = code;

            listProductOfProductOnTMDT = new ObservableCollection<ProductInOutWarehoseViewBinding>();
            UpdateListViewBinding();

            commandSave = new CommandMappingSanPhamTMDT_SanPhamKho_Save(this);
            commandDelete = new CommandMappingSanPhamTMDT_SanPhamKho_Delete(this);
            optionVisibility = Visibility.Visible;
        }

        public ViewModelListInOutWarehouse vmListInOutWarehouse { get; set; }

        private void UpdateIndex()
        {
            // Cập nhật số thứ tự
            int count = listProductOfProductOnTMDT.Count();
            for (int i = 0; i < count; i++)
            {
                listProductOfProductOnTMDT[i].index = i + 1;
            }
        }

        private void UpdateListViewBinding()
        {
            List<ModelMappingSanPhamTMDT_SanPhamKho> ls = ModelMappingSanPhamTMDT_SanPhamKho.GetListModelMappingSanPhamTMDT_SanPhamKhoFromID(((App)Application.Current).actionModelMappingSanPhamTMDT_SanPhamKho, textProductCodeOnTMDT);
            foreach(ModelMappingSanPhamTMDT_SanPhamKho obj in ls)
            {
                listProductOfProductOnTMDT.Add(new ProductInOutWarehoseViewBinding(0, obj.code, obj.name, obj.quantity, obj.position));
            }

            UpdateIndex();
            return;
        }


        private string ptextProductCodeOnTMDT;
        public string textProductCodeOnTMDT
        {
            get
            {
                return ptextProductCodeOnTMDT;
            }

            set
            {
                if (ptextProductCodeOnTMDT != value)
                {
                    ptextProductCodeOnTMDT = value;
                    OnPropertyChanged("textProductCodeOnTMDT");
                }
            }
        }

        private string ptextQuantity;
        public string textQuantity
        {
            get
            {
                return ptextQuantity;
            }
            set
            {
                if (ptextQuantity != value)
                {
                    ptextQuantity = value;
                    OnPropertyChanged("textQuantity");
                }
            }
        }

        private string ptextProductNameOnTMDT;
        public string textProductNameOnTMDT
        {
            get
            {
                return ptextProductNameOnTMDT;
            }

            set
            {
                if (ptextProductNameOnTMDT != value)
                {
                    ptextProductNameOnTMDT = value;
                    OnPropertyChanged("textProductNameOnTMDT");
                }
            }
        }


        private ObservableCollection<ProductInOutWarehoseViewBinding> plistProductOfProductOnTMDT;
        public ObservableCollection<ProductInOutWarehoseViewBinding> listProductOfProductOnTMDT
        {
            get
            {
                return plistProductOfProductOnTMDT;
            }
            set
            {
                if (plistProductOfProductOnTMDT != value)
                {
                    plistProductOfProductOnTMDT = value;
                    OnPropertyChanged("listProductOfProductOnTMDT");
                }
            }
        }

        private ProductInOutWarehoseViewBinding pitemProductOfProductOnTMDT;
        public ProductInOutWarehoseViewBinding itemProductOfProductOnTMDT
        {
            get
            {
                return pitemProductOfProductOnTMDT;
            }

            set
            {
                if (pitemProductOfProductOnTMDT != value)
                {
                    pitemProductOfProductOnTMDT = value;
                    OnPropertyChanged("itemProductOfProductOnTMDT");
                }
            }
        }

        private int pindexInListOfProductOnTMDT;
        public int indexInListOfProductOnTMDT
        {
            get
            {
                return pindexInListOfProductOnTMDT;
            }

            set
            {
                if (pindexInListOfProductOnTMDT != value)
                {
                    pindexInListOfProductOnTMDT = value;
                    OnPropertyChanged("indexInListOfProductOnTMDT");
                }
            }
        }

        //private CommandMappingSanPhamTMDT_SanPhamKho_Save _commandSave;
        public CommandMappingSanPhamTMDT_SanPhamKho_Save commandSave { get; set; }
        public CommandMappingSanPhamTMDT_SanPhamKho_Delete commandDelete { get; set; }

        public void Save()
        {
            // Check điều kiện
            if (vmListInOutWarehouse.itemProduct == null)
            {
                MessageBox.Show("Chưa chọn sản phẩm trong kho.");
                return;
            }

            int result = -1;
            if (!Int32.TryParse(textQuantity, out result) || result == 0)
            {
                MessageBox.Show("Chưa chọn số lượng sản phẩm.");
                return;
            }
            // Cập nhật list view binding
            int count = listProductOfProductOnTMDT.Count();
            for (int i = 0; i < count; i++)
            {
                if (listProductOfProductOnTMDT[i].code == vmListInOutWarehouse.itemProduct.code)
                {
                    listProductOfProductOnTMDT.RemoveAt(i);
                    break;
                }
            }
            listProductOfProductOnTMDT.Add(new ProductInOutWarehoseViewBinding(0, vmListInOutWarehouse.itemProduct.code,
                vmListInOutWarehouse.itemProduct.name, textQuantity, vmListInOutWarehouse.itemProduct.position));

            UpdateIndex();

            // Lưu vào xml data
            foreach (ProductInOutWarehoseViewBinding e in listProductOfProductOnTMDT)
            {
                string str = ModelMappingSanPhamTMDT_SanPhamKho.Tiki_AddOrUpdate(((App)Application.Current).actionModelMappingSanPhamTMDT_SanPhamKho, textProductCodeOnTMDT, e.code, e.name, e.quantity, e.position);
                if (!string.IsNullOrEmpty(str))
                {
                    MyLogger.GetInstance().Warn(str);
                    MessageBox.Show("Lưu thất bại. Vui lòng thử lại. Lỗi: " + str);
                    // Xóa
                    str = ModelMappingSanPhamTMDT_SanPhamKho.Tiki_Delete(((App)Application.Current).actionModelMappingSanPhamTMDT_SanPhamKho, textProductCodeOnTMDT);
                    if (!string.IsNullOrEmpty(str))
                    {
                        MyLogger.GetInstance().Warn(str);
                        MessageBox.Show("Xóa thất bại. Vui lòng thử lại. Lỗi: " + str);
                    }
                    listProductOfProductOnTMDT.Clear();
                    return;
                }
            }
            textQuantity = string.Empty;
        }

        public void Delete()
        {
            string str = ModelMappingSanPhamTMDT_SanPhamKho.Tiki_Delete(((App)Application.Current).actionModelMappingSanPhamTMDT_SanPhamKho, textProductCodeOnTMDT);
            if (!string.IsNullOrEmpty(str))
            {
                MyLogger.GetInstance().Warn(str);
                MessageBox.Show("Xóa thất bại. Vui lòng thử lại. Lỗi: " + str);
            }
            listProductOfProductOnTMDT.Clear();
            textQuantity = string.Empty;
            return;
        }

        public void GetProductInOutWarehosueDetail()
        {
            if (indexInListOfProductOnTMDT == -1)
            {
                MessageBox.Show("Chưa chọn sản phẩm.");
                return;
            }

            SubWindow wd = new SubWindow();
            wd.DataContext = new ViewModelSubWindow();

            wd.GetContainerContent().Children.Add(new UserControlThongTinChiTietViewOnly());
            wd.GetContainerContent().DataContext = new ViewModelThongTinChiTietViewOnly(itemProductOfProductOnTMDT.code);
            wd.WindowState = WindowState.Maximized;
            wd.Title = "Thông Tin Chi Tiết Sản Phẩm";
            wd.ShowDialog();
        }

        public Visibility optionVisibility { get; set; }
    }
}
