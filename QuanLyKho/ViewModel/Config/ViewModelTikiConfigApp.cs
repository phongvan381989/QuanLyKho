﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using QuanLyKho.Model.Config;
using QuanLyKho.Model.Dev;
using QuanLyKho.General;

namespace QuanLyKho.ViewModel.Config
{
    public class ViewModelTikiConfigApp : ViewModelBase
    {
        public ViewModelTikiConfigApp()
        {
            pdataTikiConfigApp = new DataTikiConfigApp();
            ttbmTiki = new ModelThongTinBaoMatTiki();
            listTikiConfigApp = ttbmTiki.Tiki_InhouseAppGetListTikiConfigApp();
            pcommandAdd = new CommandTikiConfigApp_AddOrUpdate(this);
            pcommandDelete = new CommandTikiConfigApp_Delete(this);
            pcommandUse = new CommandTikiConfigApp_Use(this);
        }
        private CommandTikiConfigApp_AddOrUpdate pcommandAdd;
        public CommandTikiConfigApp_AddOrUpdate commandAdd
        {
            get
            {
                return pcommandAdd;
            }
        }
        private CommandTikiConfigApp_Delete pcommandDelete;
        public CommandTikiConfigApp_Delete commandDelete
        {
            get
            {
                return pcommandDelete;
            }
        }
        private CommandTikiConfigApp_Use pcommandUse;
        public CommandTikiConfigApp_Use commandUse
        {
            get
            {
                return pcommandUse;
            }
        }
        private ObservableCollection<DataTikiConfigApp> plistTikiConfigApp;
        public ObservableCollection<DataTikiConfigApp> listTikiConfigApp
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

        public DataTikiConfigApp pdataTikiConfigApp;
        public DataTikiConfigApp dataTikiConfigApp
        {
            get
            {
                return pdataTikiConfigApp;
            }
            set
            {
                if (pdataTikiConfigApp != value)
                {
                    if (value != null)
                    {
                        appID = value.appID;
                        homeAddress = value.homeAddress;
                        secretAppCode = value.secretAppCode;
                        usingApp = value.usingApp;
                    }
                    else
                    {
                        appID = string.Empty;
                        homeAddress = string.Empty;
                        secretAppCode = string.Empty;
                        usingApp = string.Empty;
                    }
                }
            }
        }
        private ModelThongTinBaoMatTiki ttbmTiki { get; set; }

        public string appID
        {
            get
            {
                return pdataTikiConfigApp.appID;
            }

            set
            {
                if (pdataTikiConfigApp.appID != value)
                {
                    pdataTikiConfigApp.appID = value;
                    OnPropertyChanged("appID");
                }
            }
        }

        public string homeAddress
        {
            get
            {
                return pdataTikiConfigApp.homeAddress;
            }

            set
            {
                if (pdataTikiConfigApp.homeAddress != value)
                {
                    pdataTikiConfigApp.homeAddress = value;
                    OnPropertyChanged("homeAddress");
                }
            }
        }

        public string secretAppCode
        {
            get
            {
                return pdataTikiConfigApp.secretAppCode;
            }

            set
            {
                if (pdataTikiConfigApp.secretAppCode != value)
                {
                    pdataTikiConfigApp.secretAppCode = value;
                    OnPropertyChanged("secretAppCode");
                }
            }
        }

        public string usingApp
        {
            get
            {
                return pdataTikiConfigApp.usingApp;
            }

            set
            {
                if (pdataTikiConfigApp.usingApp != value)
                {
                    pdataTikiConfigApp.usingApp = value;
                    OnPropertyChanged("usingApp");
                }
            }
        }

        /// <summary>
        /// Thêm/cập nhât và lưu vào db
        /// </summary>
        public void AddOrUpdate()
        {
            string appIDTemp = pdataTikiConfigApp.appID;
            string str = ttbmTiki.Tiki_InhouseAppAddOrUpdate(pdataTikiConfigApp);
            if (str != string.Empty)
            {
                MessageBox.Show(str);
                return;
            }
            listTikiConfigApp = ttbmTiki.Tiki_InhouseAppGetListTikiConfigApp();
            Int32 count = listTikiConfigApp.Count();
            for(int i = 0; i < count; i++)
            {
                if(listTikiConfigApp[i].appID == appIDTemp)
                {
                    dataTikiConfigApp = new DataTikiConfigApp(listTikiConfigApp[i]);
                }
            }
            //UpdateList();
            Common.ShowAutoClosingMessageBox("Thêm mới thành công.", "Thêm/Cập Nhật");
        }

        /// <summary>
        /// Xóa
        /// </summary>
        public void Delete()
        {
            string str = ttbmTiki.Tiki_InhouseAppDelete(dataTikiConfigApp);
            //dataTikiConfigApp = new DataTikiConfigApp();
            if (str != string.Empty)
            {
                MessageBox.Show(str);
                return;
            }
            listTikiConfigApp = ttbmTiki.Tiki_InhouseAppGetListTikiConfigApp();
            Common.ShowAutoClosingMessageBox("Xóa thành công.", "Xóa");
        }

        /// <summary>
        /// Set sử dụng ID ứng dụng
        /// </summary>
        public void Use()
        {
            string appIDTemp = pdataTikiConfigApp.appID;
            string str = ttbmTiki.Tiki_InhouseSetUsingApp(dataTikiConfigApp);
            //UpdateList();
            if (str != string.Empty)
            {
                MessageBox.Show(str);
                return;
            }
            listTikiConfigApp = ttbmTiki.Tiki_InhouseAppGetListTikiConfigApp();
            Int32 count = listTikiConfigApp.Count();
            for (int i = 0; i < count; i++)
            {
                if (listTikiConfigApp[i].appID == appIDTemp)
                {
                    dataTikiConfigApp = new DataTikiConfigApp(listTikiConfigApp[i]);
                }
            }
            Common.ShowAutoClosingMessageBox("Set thành công.", "Hủy/Sử Dụng");
        }
    }
}
