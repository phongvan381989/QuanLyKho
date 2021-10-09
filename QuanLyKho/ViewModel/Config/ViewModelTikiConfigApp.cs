using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using QuanLyKho.Model.Config;
using QuanLyKho.Model.Dev;

namespace QuanLyKho.ViewModel.Config
{
    public class ViewModelTikiConfigApp : ViewModelBase
    {
        public ViewModelTikiConfigApp()
        {
            plistTikiConfigApp = new ObservableCollection<ModelThongTinBaoMatTiki>();
            ttbmTiki = new ModelThongTinBaoMatTiki();
        }
        private ObservableCollection<ModelThongTinBaoMatTiki> plistTikiConfigApp;
        public ObservableCollection<ModelThongTinBaoMatTiki> listTikiConfigApp
        {
            get
            {
                return plistTikiConfigApp;
            }
            set
            {
                plistTikiConfigApp = value;
                OnPropertyChanged("listTikiConfigApp");
            }
        }

        public ModelThongTinBaoMatTiki ttbmTiki { get; set; }

        public string appID
        {
            get
            {
                return ttbmTiki.appID;
            }

            set
            {
                ttbmTiki.appID = value;
                OnPropertyChanged("appID");
            }
        }

        public string homeAddress
        {
            get
            {
                return ttbmTiki.homeAddress;
            }

            set
            {
                ttbmTiki.homeAddress = value;
                OnPropertyChanged("homeAddress");
            }
        }

        public string secretAppCode
        {
            get
            {
                return ttbmTiki.secretAppCode;
            }

            set
            {
                ttbmTiki.homeAddress = value;
                OnPropertyChanged("secretAppCode");
            }
        }

        /// <summary>
        /// Thêm và lưu vào db
        /// </summary>
        /// <param name="tikiConfig"></param>
        public void Add()
        {
            ttbmTiki.Add();
        }

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="tikiConfig"></param>
        public void Delete()
        {
            string str = ttbmTiki.Delete();
            if(str != string.Empty)
            {
                MessageBox.Show(str);
                return;
            }
        }
    }
}
