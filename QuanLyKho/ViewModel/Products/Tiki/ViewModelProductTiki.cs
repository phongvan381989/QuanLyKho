using QuanLyKho.General;
using QuanLyKho.Model.Dev.TikiApp.Products;
using QuanLyKho.View.InOutWarehouse;
using QuanLyKho.ViewModel.Dev.TikiAPI;
using QuanLyKho.ViewModel.Dev.TikiAPI.Products;
using QuanLyKho.ViewModel.InOutWarehouse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyKho.ViewModel.Products
{
    public class ViewModelProductTiki : ViewModelBase
    {
        public ViewModelProductTiki()
        {
            pcommandProductTiki_GetListLatestProduct = new CommandProductTiki_GetListLatestProduct(this);
            pcommandProductTiki_GetProductDetail = new CommandProductTiki_GetProductDetail(this);
            // Lấy danh sách cửa hàng
            listHomeAddressShopUsing = CommonTikiAPI.GetListHomeAddressUsing();
            // Thêm tùy chọn tất cả shop nếu danh sách shop có từ 2 shop trở lên
            if (listHomeAddressShopUsing.Count() > 1)
                listHomeAddressShopUsing.Add("Tất cả");
            homeAddressIndex = listHomeAddressShopUsing.Count() - 1;
            isEnabledButtons = true;
            if (homeAddressIndex == -1)
                isEnabledButtons = false;
            indexProductInList = -1;
            lsProduct = new ObservableCollection<ProductViewBindingTiki>();
            lsProductFullInfo = new List<Product>();
        }

        private CommandProductTiki_GetListLatestProduct pcommandProductTiki_GetListLatestProduct;
        public CommandProductTiki_GetListLatestProduct commandProductTiki_GetListLatestProduct
        {
            get
            {
                return pcommandProductTiki_GetListLatestProduct;
            }
        }

        private CommandProductTiki_GetProductDetail pcommandProductTiki_GetProductDetail;
        public CommandProductTiki_GetProductDetail commandProductTiki_GetProductDetail
        {
            get
            {
                return pcommandProductTiki_GetProductDetail;
            }
        }

        private ObservableCollection<string> plistHomeAddressShopUsing;
        public ObservableCollection<String> listHomeAddressShopUsing
        {
            get
            {
                return plistHomeAddressShopUsing;
            }
            set
            {
                if (plistHomeAddressShopUsing != value)
                {
                    OnPropertyChanged("listHomeAddressShopUsing");
                    plistHomeAddressShopUsing = value;
                }
            }
        }

        private int phomeAddressIndex;
        public int homeAddressIndex
        {
            get
            {
                return phomeAddressIndex;
            }

            set
            {
                if (phomeAddressIndex != value)
                {
                    OnPropertyChanged("homeAddressIndex");
                    phomeAddressIndex = value;
                }
            }
        }

        private bool pisEnabledButtons;
        public bool isEnabledButtons
        {
            get
            {
                return pisEnabledButtons;
            }

            set
            {
                if (pisEnabledButtons != value)
                {
                    OnPropertyChanged("isEnabledButtons");
                    pisEnabledButtons = value;
                }
            }
        }

        private string ptextProductCodeGetDetail;
        public string textProductCodeGetDetail
        {
            get
            {
                return ptextProductCodeGetDetail;
            }

            set
            {
                if (ptextProductCodeGetDetail != value)
                {
                    ptextProductCodeGetDetail = value;
                    OnPropertyChanged("textProductCodeGetDetail");
                }
            }
        }

        private int pindexProductInList;
        public int indexProductInList
        {
            get
            {
                return pindexProductInList;
            }
            set
            {
                if (pindexProductInList != value)
                {
                    pindexProductInList = value;
                    OnPropertyChanged("indexProductInList");
                }
            }
        }

        private ObservableCollection<ProductViewBindingTiki> plsProduct;
        public ObservableCollection<ProductViewBindingTiki> lsProduct
        {
            get
            {
                return plsProduct;
            }

            set
            {
                if (plsProduct != value)
                {
                    OnPropertyChanged("lsProduct");
                    plsProduct = value;
                }
            }
        }

        private string phomeAddressUsing;
        public string homeAddressUsing
        {
            get
            {
                return phomeAddressUsing;
            }

            set
            {
                if (phomeAddressUsing != value)
                {
                    OnPropertyChanged("homeAddressUsing");
                    phomeAddressUsing = value;
                }
            }
        }

        private List<Product> lsProductFullInfo;

        /// <summary>
        /// Lấy danh sách tất cả sản phẩm
        /// </summary>
        public void GetListLatestProduct()
        {
            lsProduct.Clear();
            if (homeAddressIndex == -1)
                return;

            // Lấy sản phẩm của tất cả các shop
            if (listHomeAddressShopUsing.Count() > 1 &&
               homeAddressIndex == listHomeAddressShopUsing.Count() - 1)
            {
                lsProductFullInfo = GetListProductTiki.GetListLatestProductsFromAllShop(CommonTikiAPI.listTikiConfigAppUsing);
            }
            else
            {
                // Lấy sản phẩm của 1 shop
                lsProductFullInfo = GetListProductTiki.GetListLatestProductsFromOneShop(CommonTikiAPI.GetTikiConfigAppFromHomeAddress(homeAddressUsing));
            }

            int index = 0;
            foreach (Product e in lsProductFullInfo)
            {
                index++;
                // Download thumbnail của sản phẩm
                Common.DownloadImageAndSave(e.thumbnail, ((App)Application.Current).temporaryImageFolderPath);
                lsProduct.Add(new ProductViewBindingTiki(e, index));
            }
        }

        public void GetProductDetail()
        {
            if (string.IsNullOrEmpty(textProductCodeGetDetail))
            {
                MessageBox.Show("Chưa nhập mã sản phẩm.");
                return;
            }
            // Check xem mã sản phẩm có tồn tại

            int i = 0;
            int count = plsProduct.Count();
            for (i = 0; i < count; i++)
            {
                ProductViewBindingTiki e = plsProduct.ElementAt(i);
                if (e.product_id == textProductCodeGetDetail)
                {
                    break;
                }
            }
            
            if(i == count)
            {
                MessageBox.Show("Mã sản phẩm không chính xác.");
                return;
            }

            indexProductInList = i;

            Window wdMappingSanPhamTMDT_SanPhamKho = new Window
            {
                Content = new UserControlMappingSanPhamTMDT_SanPhamKho()
            };
            wdMappingSanPhamTMDT_SanPhamKho.DataContext = new ViewModelMappingSanPhamTMDT_SanPhamKho(itemProduct.product_id, itemProduct.name);

            wdMappingSanPhamTMDT_SanPhamKho.WindowState = WindowState.Maximized;
            wdMappingSanPhamTMDT_SanPhamKho.ShowDialog();
        }

        private ProductViewBindingTiki pitemProduct;
        public ProductViewBindingTiki itemProduct
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
                }
            }
        }
        public void RefreshView()
        {
            textProductCodeGetDetail = string.Empty;
            indexProductInList = -1;
            lsProduct.Clear();
            lsProductFullInfo.Clear();

        }
    }
}
