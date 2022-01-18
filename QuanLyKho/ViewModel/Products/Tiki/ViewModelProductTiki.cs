using QuanLyKho.General;
using QuanLyKho.Model.Dev.TikiApp.Products;
using QuanLyKho.View.InOutWarehouse;
using QuanLyKho.View.UserControlCommon;
using QuanLyKho.ViewModel.Dev.TikiAPI;
using QuanLyKho.ViewModel.Dev.TikiAPI.Products;
using QuanLyKho.ViewModel.InOutWarehouse;
using QuanLyKho.ViewModel.Products.Tiki;
using QuanLyKho.ViewModel.ViewModelCommon;
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
            commandProductTiki_GetListLatestProduct = new CommandProductTiki_GetListLatestProduct(this);
            commandProductTiki_GetProductDetail = new CommandProductTiki_GetProductDetail(this);
            commandProductTiki_GetListProductDontMapping = new CommandProductTiki_GetListProductDontMapping(this);
            commandProductTiki_SearchFromShopTMDT = new CommandProductTiki_SearchFromShopTMDT(this);
            commandProductTiki_SearchCodeFromCache = new CommandProductTiki_SearchCodeFromCache(this);
            commandProductTiki_SearchNameFromCache = new CommandProductTiki_SearchNameFromCache(this);
            // Lấy danh sách cửa hàng
            listHomeAddressShopUsing = CommonTikiAPI.GetListHomeAddressUsing();
            // Thêm tùy chọn tất cả shop nếu danh sách shop có từ 2 shop trở lên
            if (listHomeAddressShopUsing.Count() > 1)
                listHomeAddressShopUsing.Add("Tất cả");
            homeAddressIndex = listHomeAddressShopUsing.Count() - 1;
            indexProductInList = -1;
            lsProduct = new ObservableCollection<ViewModelProductViewBindingTiki>();
            lsProductFullInfo = new List<Product>();
        }

        public CommandProductTiki_GetListLatestProduct commandProductTiki_GetListLatestProduct { get; set; }

        public CommandProductTiki_GetProductDetail commandProductTiki_GetProductDetail { get; set; }

        public CommandProductTiki_GetListProductDontMapping commandProductTiki_GetListProductDontMapping { get; set; }


        public CommandProductTiki_SearchFromShopTMDT commandProductTiki_SearchFromShopTMDT { get; set; }

        public CommandProductTiki_SearchCodeFromCache commandProductTiki_SearchCodeFromCache { get; set; }
        public CommandProductTiki_SearchNameFromCache commandProductTiki_SearchNameFromCache { get; set; }

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

        private string ptextProductCodeSearchFromShopTMDT;
        public string textProductCodeSearchFromShopTMDT
        {
            get
            {
                return ptextProductCodeSearchFromShopTMDT;
            }

            set
            {
                if (ptextProductCodeSearchFromShopTMDT != value)
                {
                    ptextProductCodeSearchFromShopTMDT = value;
                    OnPropertyChanged("textProductCodeSearchFromShopTMDT");
                }
            }
        }

        private string ptextProductCodeSearchFromCache;
        public string textProductCodeSearchFromCache
        {
            get
            {
                return ptextProductCodeSearchFromCache;
            }

            set
            {
                if(ptextProductCodeSearchFromCache != value)
                {
                    ptextProductCodeSearchFromCache = value;
                    OnPropertyChanged("textProductCodeSearchFromCache");
                }
            }
        }

        private string ptextProductNameSearchFromCache;
        public string textProductNameSearchFromCache
        {
            get
            {
                return ptextProductNameSearchFromCache;
            }

            set
            {
                if(ptextProductNameSearchFromCache != value)
                {
                    ptextProductNameSearchFromCache = value;
                    OnPropertyChanged("textProductNameSearchFromCache");
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

        private ObservableCollection<ViewModelProductViewBindingTiki> plsProduct;
        public ObservableCollection<ViewModelProductViewBindingTiki> lsProduct
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
            lsProductFullInfo.Clear();
            if (homeAddressIndex == -1)
                return;

            // Thực hiện hàm này lâu, ta hiện cửa sổ thông báo đợi
            WaitingWindow waitingWindow = new WaitingWindow();
            waitingWindow.Show();

            try
            {
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
                    Common.DownloadImageAndSave(e.thumbnail, ((App)Application.Current).temporaryTikiImageFolderPath);
                    lsProduct.Add(new ViewModelProductViewBindingTiki(e, index, this));
                }
                indexProductInList = -1;
            }
            catch (Exception ex)
            {
                MyLogger.GetInstance().Warn(ex.Message);
            }
            finally
            {
                waitingWindow.Close();
            }
        }

        /// <summary>
        /// Từ mã sản phẩm, lấy được sản phẩm ở shopTMDT
        /// </summary>
        public void SearchFromShopTMDT()
        {
            if (homeAddressIndex == -1)
                return;

            if(string.IsNullOrEmpty(textProductCodeSearchFromShopTMDT))
                return;

            lsProduct.Clear();
            lsProductFullInfo.Clear();

            Product pro = null;
            // Lấy sản phẩm của tất cả các shop
            if (listHomeAddressShopUsing.Count() > 1 &&
               homeAddressIndex == listHomeAddressShopUsing.Count() - 1)
            {
                pro = GetListProductTiki.GetProductFromAllShop(CommonTikiAPI.listTikiConfigAppUsing, textProductCodeSearchFromShopTMDT);
            }
            else
            {
                // Lấy sản phẩm của 1 shop
                pro = GetListProductTiki.GetProductFromOneShop(CommonTikiAPI.GetTikiConfigAppFromHomeAddress(homeAddressUsing), textProductCodeSearchFromShopTMDT);

            }
            if (pro == null)
                return;

            lsProductFullInfo.Add(pro);
            // Download thumbnail của sản phẩm
            Common.DownloadImageAndSave(pro.thumbnail, ((App)Application.Current).temporaryTikiImageFolderPath);
            lsProduct.Add(new ViewModelProductViewBindingTiki(pro, 1, this));
            indexProductInList = -1;
        }

        /// <summary>
        /// Từ mã sản phẩm tìm kiếm từ cache
        /// </summary>
        public void SearchCodeFromCache()
        {
            lsProduct.Clear();

            int index = 0;
            foreach (Product e in lsProductFullInfo)
            {
                string code = e.product_id.ToString();
                if (string.IsNullOrEmpty(textProductCodeSearchFromCache) || code.IndexOf(textProductCodeSearchFromCache, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    index++;
                    // Download thumbnail của sản phẩm
                    Common.DownloadImageAndSave(e.thumbnail, ((App)Application.Current).temporaryTikiImageFolderPath);
                    lsProduct.Add(new ViewModelProductViewBindingTiki(e, index, this));
                }
            }
            indexProductInList = -1;
            if (lsProduct.Count() == 0)
                MessageBox.Show("Không tìm thấy kết quả nào.");
        }

        /// <summary>
        /// Từ tên sản phẩm tìm kiếm từ cache
        /// </summary>
        public void SearchNameFromCache()
        {
            lsProduct.Clear();

            int index = 0;
            foreach (Product e in lsProductFullInfo)
            {
                if (string.IsNullOrEmpty(textProductNameSearchFromCache) || e.name.IndexOf(textProductNameSearchFromCache, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    index++;
                    // Download thumbnail của sản phẩm
                    Common.DownloadImageAndSave(e.thumbnail, ((App)Application.Current).temporaryTikiImageFolderPath);
                    lsProduct.Add(new ViewModelProductViewBindingTiki(e, index, this));
                }
            }
            indexProductInList = -1;
            if (lsProduct.Count() == 0)
                MessageBox.Show("Không tìm thấy kết quả nào.");
        }

        /// <summary>
        /// Lấy danh sách sản phẩm chưa được liên kết với sản phẩm trong kho
        /// </summary>
        public void GetListProductDontMapping()
        {
            if(lsProductFullInfo.Count() == 0)
            {
                MessageBox.Show("Danh sách sản phẩm trống.");
            }

            ObservableCollection<ViewModelProductViewBindingTiki> lsProductNoMapping = new ObservableCollection<ViewModelProductViewBindingTiki>();
            foreach (ViewModelProductViewBindingTiki e in lsProduct)
            {
                if (e.vmProductTikiMapping.listProductMapping.Count() == 0)
                {
                    lsProductNoMapping.Add(e);
                }
            }
            lsProduct.Clear();
            int count = lsProductNoMapping.Count();
            for (int i = 1; i <= count; i++)
            {
                lsProductNoMapping[i - 1].index = i;
                lsProduct.Add(lsProductNoMapping[i - 1]);
            }
            indexProductInList = -1;
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
                ViewModelProductViewBindingTiki e = plsProduct.ElementAt(i);
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

            SubWindow wd = new SubWindow();
            wd.DataContext = new ViewModelSubWindow();
            wd.GetContainerContent().Children.Add(new UserControlMappingSanPhamTMDT_SanPhamKho());
            wd.GetContainerContent().DataContext = new ViewModelMappingSanPhamTMDT_SanPhamKho(itemProduct.product_id, itemProduct.name);
            wd.WindowState = WindowState.Maximized;
            wd.Title = "Thông Tin Liên Kết Sản Phẩm Tiki và Kho Thực Tế";
            wd.ShowDialog();
        }

        private ViewModelProductViewBindingTiki pitemProduct;
        public ViewModelProductViewBindingTiki itemProduct
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
