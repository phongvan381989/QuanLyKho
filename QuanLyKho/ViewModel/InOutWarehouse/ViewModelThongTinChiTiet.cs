using QuanLyKho.General;
using QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKho.ViewModel.InOutWarehouse
{
    public class ViewModelThongTinChiTiet : ViewModelBase
    {
        private CommandThongTinChiTiet_Save _commandSave;
        public ICommand commandSave
        {
            get
            {
                return _commandSave;
            }
        }
        private CommandThongTinChiTiet_Delete _commandDelete;
        public ICommand commandDelete
        {
            get
            {
                return _commandDelete;
            }
        }
        public XMLAction actionModelThongTinChiTiet { get; set; }
        public ModelThongTinChiTiet sanPhamHienThi { get; set; }

        public XMLAction actionModelNhapXuatChiTiet { get; set; }
        public ModelNhapXuatChiTiet nhapXuatChiTiet { get; set; }

        private CommandThongTinChiTiet_ListInOutWarehouse _commandGetListInOutWarehouse;
        public CommandThongTinChiTiet_ListInOutWarehouse commandGetListInOutWarehouse
        {
            get
            {
                return _commandGetListInOutWarehouse;
            }
        }

        private Visibility pvisibilitySaveDelete;
        public Visibility visibilitySaveDelete
        {
            get
            {
                return pvisibilitySaveDelete;
            }

            set
            {
                if(pvisibilitySaveDelete !=value)
                {
                    pvisibilitySaveDelete = value;
                    OnPropertyChanged("visibilitySaveDelete");
                }
            }
        }


        #region list lưu toàn bộ dữ liệu xml db phục vụ truy xuất nhanh thành phần cho màn hình nhập xuất sản phẩm vào kho
        private ObservableCollection<string> listNhaPhatHanhAllBuffer;
        private ObservableCollection<string> listNhaXuatBanAllBuffer;
        private ObservableCollection<string> listMaSanPhamAllBuffer;
        private ObservableCollection<string> listTenSanPhamAllBuffer;
        public void InitializeBuffer(XMLAction action)
        {
            listMaSanPhamAllBuffer = ModelThongTinChiTiet.ListGiaTriMotThanhPhanFromXDoc(action, "MaSanPham", false);
            listTenSanPhamAllBuffer = ModelThongTinChiTiet.ListGiaTriMotThanhPhanFromXDoc(action, "TenSanPham", false);
            listNhaPhatHanhAllBuffer = ModelThongTinChiTiet.ListGiaTriMotThanhPhanFromXDoc(action, "NhaPhatHanh", false);
            listNhaXuatBanAllBuffer = ModelThongTinChiTiet.ListGiaTriMotThanhPhanFromXDoc(action, "NhaXuatBan", false);
        }

        /// <summary>
        /// Từ tên thành phần get list tương ứng
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private ObservableCollection<string> GetListFromName(string name)
        {
            if (name == "MaSanPham")
                return listMaSanPhamAllBuffer;
            else if (name == "NhaPhatHanh")
                return listNhaPhatHanhAllBuffer;
            else if (name == "NhaXuatBan")
                return listNhaXuatBanAllBuffer;
            else if (name == "TenSanPham")
                return listTenSanPhamAllBuffer;
            return null;
        }

        /// <summary>
        /// Danh sách tất cả nhà phát hành
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<string> ListNhaPhatHanh()
        {
            return GetListFromName("NhaPhatHanh");
        }

        /// <summary>
        /// Danh sách giá trị một thành phần với 1 text được tìm từ list
        /// </summary>
        /// <param name="name">Tên thành phần.</param>
        /// <param name="str">Text cần được chứa.</param>
        /// <param name="parameterSearch">Tham số cách tìm kiếm</param>
        /// <returns></returns>
        public ObservableCollection<string> ListGiaTriMotThanhPhanVoiATextFromList(string name, string str, ParameterSearch parameterSearch)
        {
            ObservableCollection<string> listOriginal = GetListFromName(name);
            if (string.IsNullOrEmpty(str))
            {
                return listOriginal;
            }

            ObservableCollection<string> list = new ObservableCollection<string>();
            Int32 count = listOriginal.Count();
            for (Int32 i = 0; i < count; i++)
            {
                if ((parameterSearch == ParameterSearch.First
                    && listOriginal[i].StartsWith(str, StringComparison.OrdinalIgnoreCase))

                    || (parameterSearch == ParameterSearch.Last
                    && listOriginal[i].EndsWith(str, StringComparison.OrdinalIgnoreCase))

                    || (parameterSearch == ParameterSearch.All
                    && listOriginal[i].IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0)

                    || (parameterSearch == ParameterSearch.Same
                    && listOriginal[i].Equals(str) == true))
                {
                    list.Add(listOriginal[i]);
                }
            }
            return list;
        }

        /// <summary>
        /// Tìm kiếm nhà phát hành
        /// </summary>
        /// <param name="str"></param>
        /// <param name="parameterSearch">Tham số cách tìm kiếm</param>
        /// <returns></returns>
        public ObservableCollection<string> SearchNhaPhatHanhAText(string str, ParameterSearch parameterSearch)
        {
            return ListGiaTriMotThanhPhanVoiATextFromList("NhaPhatHanh", str, parameterSearch);
        }

        /// <summary>
        /// Tìm kiếm nhà xuất bản
        /// </summary>
        /// <param name="str"></param>
        /// <param name="parameterSearch">Tham số cách tìm kiếm</param>
        /// <returns></returns>
        public ObservableCollection<string> SearchNhaXuatBanAText(string str, ParameterSearch parameterSearch)
        {
            return ListGiaTriMotThanhPhanVoiATextFromList("NhaXuatBan", str, parameterSearch);
        }

        /// <summary>
        /// Danh sách tất cả nhà xuất bản
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<string> ListNhaXuatBan()
        {
            return GetListFromName("NhaXuatBan");
        }

        /// <summary>
        /// Tìm kiếm mã sản phẩm
        /// </summary>
        /// <param name="str"></param>
        /// <param name="parameterSearch">Tham số cách tìm kiếm</param>
        /// <returns></returns>
        public ObservableCollection<string> SearchMaSanPhamAText(string str, ParameterSearch parameterSearch)
        {
            return ListGiaTriMotThanhPhanVoiATextFromList("MaSanPham", str, parameterSearch);
        }

        /// <summary>
        /// Tìm kiếm tên sản phẩm
        /// </summary>
        /// <param name="str"></param>
        /// <param name="parameterSearch">Tham số cách tìm kiếm</param>
        /// <returns></returns>
        public ObservableCollection<string> SearchTenSanPhamAText(string str, ParameterSearch parameterSearch)
        {
            return ListGiaTriMotThanhPhanVoiATextFromList("TenSanPham", str, parameterSearch);
        }

        /// <summary>
        /// Danh sách tất cả mã sản phẩm
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<string> ListMaSanPham()
        {
            return GetListFromName("MaSanPham");
        }

        /// <summary>
        /// Danh sách tất cả tên sản phẩm
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<string> ListTenSanPham()
        {
            return GetListFromName("TenSanPham");
        }
        #endregion

        public ViewModelThongTinChiTiet()
        {
            vmMedia = new ViewModelMedia();
            actionModelThongTinChiTiet = new XMLAction(((App)Application.Current).GetPathDataXMLThongTinChiTiet());

            actionModelNhapXuatChiTiet = new XMLAction(((App)Application.Current).GetPathDataXMLNhapXuatChiTiet());
            try
            {
                sanPhamHienThi = new ModelThongTinChiTiet();
                InitializeBuffer(actionModelThongTinChiTiet);

                nhapXuatChiTiet = new ModelNhapXuatChiTiet();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            _commandSave = new CommandThongTinChiTiet_Save(this);
            _commandDelete = new CommandThongTinChiTiet_Delete(this);
            _commandGetListInOutWarehouse = new CommandThongTinChiTiet_ListInOutWarehouse(this);
            visibilitySaveDelete = Visibility.Visible;
        }

        #region Mã sản phẩm
        private Boolean pbListBoxSearchPopupIsOpenMSP;
        public Boolean blistBoxSearchPopupIsOpenMSP
        {
            get
            {
                return pbListBoxSearchPopupIsOpenMSP;
            }
            set
            {
                if (pbListBoxSearchPopupIsOpenMSP != value)
                {
                    pbListBoxSearchPopupIsOpenMSP = value;
                    OnPropertyChanged("blistBoxSearchPopupIsOpenMSP");
                }
            }
        }

        private Boolean pbCheckSelectedItemFromListMSP;
        public Boolean bCheckSelectedItemFromListMSP
        {
            get
            {
                return pbCheckSelectedItemFromListMSP;
            }
            set
            {
                pbCheckSelectedItemFromListMSP = value;
                OnPropertyChanged("bCheckSelectedItemFromListMSP");
            }
        }

        private ObservableCollection<string> plistMaSanPham;

        public ObservableCollection<string> listMaSanPham
        {
            get
            {
                return plistMaSanPham;
            }
            set
            {
                plistMaSanPham = value;
                OnPropertyChanged("listMaSanPham");
            }
        }

        private void OnPropertyChangedAll()
        {
            OnPropertyChanged("maSanPham");
            OnPropertyChanged("giaSanPham");
            OnPropertyChanged("tonKho");
            OnPropertyChanged("tonKhoCanhBaoHetHang");
            OnPropertyChanged("tenSanPham");
            OnPropertyChanged("tacGia");
            OnPropertyChanged("nguoiDich");
            OnPropertyChanged("nhaPhatHanh");
            OnPropertyChanged("nhaXuatBan");
            OnPropertyChanged("namXuatBan");
            OnPropertyChanged("kichThuocDai");
            OnPropertyChanged("kichThuocRong");
            OnPropertyChanged("kichThuocCao");
            OnPropertyChanged("khoiLuong");
            OnPropertyChanged("thuMucMedia");
            vmMedia.folderPath = thuMucMedia;
            OnPropertyChanged("moTaChiTiet");
            OnPropertyChanged("viTriLuuKho");
        }

        private void UpdateListBufferAll()
        {
            listMaSanPham = SearchMaSanPhamAText(maSanPham, ParameterSearch.Same);
            listTenSanPham = SearchTenSanPhamAText(tenSanPham, ParameterSearch.Same);
            listNhaPhatHanh = SearchNhaPhatHanhAText(nhaPhatHanh, ParameterSearch.Same);
            listNhaXuatBan = SearchNhaXuatBanAText(nhaXuatBan, ParameterSearch.Same);
        }
        public string maSanPham
        {
            get
            {
                return sanPhamHienThi.maSanPham;
            }

            set
            {
                if (!bCheckSelectedItemFromListMSP)
                {
                    if (sanPhamHienThi.maSanPham != value)
                    {
                        sanPhamHienThi.maSanPham = value;
                        OnPropertyChanged("maSanPham");
                        listMaSanPham = SearchMaSanPhamAText(value, ParameterSearch.Last);
                        blistBoxSearchPopupIsOpenMSP = true;
                    }
                }
                else
                {
                    sanPhamHienThi.maSanPham = value;
                    sanPhamHienThi = ModelThongTinChiTiet.GetASanPhamFromMaSanPham(actionModelThongTinChiTiet, sanPhamHienThi.maSanPham);
                    UpdateListBufferAll();
                    OnPropertyChangedAll();

                    bCheckSelectedItemFromListMSP = false;

                }
            }
        }
        #endregion

        public string giaSanPham
        {
            get
            {
                return sanPhamHienThi.giaSanPham;
            }

            set
            {
                if (sanPhamHienThi.giaSanPham != value)
                {
                    sanPhamHienThi.giaSanPham = value;
                    OnPropertyChanged("giaSanPham");
                }
            }
        }

        public string soLuongNhap
        {
            get
            {
                return sanPhamHienThi.soLuongNhap;
            }

            set
            {
                if (sanPhamHienThi.soLuongNhap != value)
                {
                    sanPhamHienThi.soLuongNhap = value;
                    OnPropertyChanged("soLuongNhap");
                }
            }
        }

        public string tonKho
        {
            get
            {
                return sanPhamHienThi.tonKho;
            }

            set
            {
                if (sanPhamHienThi.tonKho != value)
                {
                    sanPhamHienThi.tonKho = value;
                    OnPropertyChanged("tonKho");
                }
            }
        }

        public string tonKhoCanhBaoHetHang
        {
            get
            {
                return sanPhamHienThi.tonKhoCanhBaoHetHang;
            }

            set
            {
                if (sanPhamHienThi.tonKhoCanhBaoHetHang != value)
                {
                    sanPhamHienThi.tonKhoCanhBaoHetHang = value;
                    OnPropertyChanged("tonKhoCanhBaoHetHang");
                }
            }
        }

        #region Tên Sản Phẩm
        private Boolean pbListBoxSearchPopupIsOpenTSP;
        public Boolean blistBoxSearchPopupIsOpenTSP
        {
            get
            {
                return pbListBoxSearchPopupIsOpenTSP;
            }
            set
            {
                if (pbListBoxSearchPopupIsOpenTSP != value)
                {
                    pbListBoxSearchPopupIsOpenTSP = value;
                    OnPropertyChanged("blistBoxSearchPopupIsOpenTSP");
                }
            }
        }

        private Boolean pbCheckSelectedItemFromListTSP;
        public Boolean bCheckSelectedItemFromListTSP
        {
            get
            {
                return pbCheckSelectedItemFromListTSP;
            }
            set
            {
                pbCheckSelectedItemFromListTSP = value;
                OnPropertyChanged("bCheckSelectedItemFromListTSP");
            }
        }

        private ObservableCollection<string> plistTenSanPham;

        public ObservableCollection<string> listTenSanPham
        {
            get
            {
                return plistTenSanPham;
            }
            set
            {
                plistTenSanPham = value;
                OnPropertyChanged("listTenSanPham");
            }
        }

        public string tenSanPham
        {
            get
            {
                return sanPhamHienThi.tenSanPham;
            }

            set
            {
                if(!bCheckSelectedItemFromListTSP)
                {
                    if (sanPhamHienThi.tenSanPham != value)
                    {
                        sanPhamHienThi.tenSanPham = value;
                        OnPropertyChanged("tenSanPham");
                        listTenSanPham = SearchTenSanPhamAText(value, ParameterSearch.All);
                        blistBoxSearchPopupIsOpenTSP = true;
                    }
                }
                else
                {
                    sanPhamHienThi.tenSanPham = value;
                    sanPhamHienThi = ModelThongTinChiTiet.GetASanPhamFromTenSanPham(actionModelThongTinChiTiet, sanPhamHienThi.tenSanPham);
                    UpdateListBufferAll();
                    OnPropertyChangedAll();
                    bCheckSelectedItemFromListTSP = false;
                }
            }
        }
        #endregion

        public string tacGia
        {
            get
            {
                return sanPhamHienThi.tacGia;
            }

            set
            {
                if (sanPhamHienThi.tacGia != value)
                {
                    sanPhamHienThi.tacGia = value;
                    OnPropertyChanged("tacGia");
                }
            }
        }

        public string nguoiDich
        {
            get
            {
                return sanPhamHienThi.nguoiDich;
            }

            set
            {
                if (sanPhamHienThi.nguoiDich != value)
                {
                    sanPhamHienThi.nguoiDich = value;
                    OnPropertyChanged("nguoiDich");
                }
            }
        }

        #region Nhà phát hành
        private Boolean pbListBoxSearchPopupIsOpenNPH;
        public Boolean blistBoxSearchPopupIsOpenNPH
        {
            get
            {
                return pbListBoxSearchPopupIsOpenNPH;
            }
            set
            {
                if (pbListBoxSearchPopupIsOpenNPH != value)
                {
                    pbListBoxSearchPopupIsOpenNPH = value;
                    OnPropertyChanged("blistBoxSearchPopupIsOpenNPH");
                }
            }
        }

        private Boolean pbCheckSelectedItemFromListNPH;
        public Boolean bCheckSelectedItemFromListNPH
        {
            get
            {
                return pbCheckSelectedItemFromListNPH;
            }
            set
            {
                pbCheckSelectedItemFromListNPH = value;
                OnPropertyChanged("bCheckSelectedItemFromListNPH");
            }
        }

        private ObservableCollection<string> plistNhaPhatHanh;

        public ObservableCollection<string> listNhaPhatHanh
        {
            get
            {
                return plistNhaPhatHanh;
            }
            set
            {
                plistNhaPhatHanh = value;
                OnPropertyChanged("listNhaPhatHanh");
            }
        }

        public string nhaPhatHanh
        {
            get
            {
                return sanPhamHienThi.nhaPhatHanh;
            }

            set
            {
                if (sanPhamHienThi.nhaPhatHanh != value)
                {
                    sanPhamHienThi.nhaPhatHanh = value;
                    OnPropertyChanged("nhaPhatHanh");
                    listNhaPhatHanh = SearchNhaPhatHanhAText(value, ParameterSearch.First);

                    if (listNhaPhatHanh.Count != 0)
                    {
                        if (bCheckSelectedItemFromListNPH)
                        {
                            blistBoxSearchPopupIsOpenNPH = false;
                            bCheckSelectedItemFromListNPH = false;
                        }
                        else
                        {
                            blistBoxSearchPopupIsOpenNPH = true;
                        }
                    }
                    else
                        blistBoxSearchPopupIsOpenNPH = false;
                }
            }
        }
        #endregion

        #region Nhà Xuất Bản

        private Boolean pbListBoxSearchPopupIsOpenNXB;
        public Boolean blistBoxSearchPopupIsOpenNXB
        {
            get
            {
                return pbListBoxSearchPopupIsOpenNXB;
            }
            set
            {
                if (pbListBoxSearchPopupIsOpenNXB != value)
                {
                    pbListBoxSearchPopupIsOpenNXB = value;
                    OnPropertyChanged("blistBoxSearchPopupIsOpenNXB");
                }
            }
        }

        private Boolean pbCheckSelectedItemFromListNXB;
        public Boolean bCheckSelectedItemFromListNXB
        {
            get
            {
                return pbCheckSelectedItemFromListNXB;
            }
            set
            {
                pbCheckSelectedItemFromListNXB = value;
                OnPropertyChanged("bCheckSelectedItemFromListNXB");
            }
        }

        private ObservableCollection<string> plistNhaXuatBan;

        public ObservableCollection<string> listNhaXuatBan
        {
            get
            {
                return plistNhaXuatBan;
            }
            set
            {
                plistNhaXuatBan = value;
                OnPropertyChanged("listNhaXuatBan");
            }
        }

        

        public string nhaXuatBan
        {
            get
            {
                return sanPhamHienThi.nhaXuatBan;
            }

            set
            {
                if (sanPhamHienThi.nhaXuatBan != value)
                {
                    sanPhamHienThi.nhaXuatBan = value;
                    OnPropertyChanged("nhaXuatBan");
                    listNhaXuatBan = SearchNhaXuatBanAText(value, ParameterSearch.First);
                    if (listNhaXuatBan.Count != 0)
                    {
                        if (bCheckSelectedItemFromListNXB)
                        {
                            blistBoxSearchPopupIsOpenNXB = false;
                            bCheckSelectedItemFromListNXB = false;
                        }
                        else
                        {
                            blistBoxSearchPopupIsOpenNXB = true;
                        }
                    }
                    else
                        blistBoxSearchPopupIsOpenNXB = false;
                }
            }
        }
        #endregion

        public string namXuatBan
        {
            get
            {
                return sanPhamHienThi.namXuatBan;
            }

            set
            {
                if (sanPhamHienThi.namXuatBan != value)
                {
                    sanPhamHienThi.namXuatBan = value;
                    OnPropertyChanged("namXuatBan");
                }
            }
        }

        public string kichThuocDai
        {
            get
            {
                return sanPhamHienThi.kichThuocDai;
            }

            set
            {
                if (sanPhamHienThi.kichThuocDai != value)
                {
                    sanPhamHienThi.kichThuocDai = value;
                    OnPropertyChanged("kichThuocDai");
                }
            }
        }

        public string kichThuocRong
        {
            get
            {
                return sanPhamHienThi.kichThuocRong;
            }

            set
            {
                if (sanPhamHienThi.kichThuocRong != value)
                {
                    sanPhamHienThi.kichThuocRong = value;
                    OnPropertyChanged("kichThuocRong");
                }
            }
        }

        public string kichThuocCao
        {
            get
            {
                return sanPhamHienThi.kichThuocCao;
            }

            set
            {
                if (sanPhamHienThi.kichThuocCao != value)
                {
                    sanPhamHienThi.kichThuocCao = value;
                    OnPropertyChanged("kichThuocCao");
                }
            }
        }

        public string khoiLuong
        {
            get
            {
                return sanPhamHienThi.khoiLuong;
            }
            set
            {
                if(sanPhamHienThi.khoiLuong != value)
                {
                    sanPhamHienThi.khoiLuong = value;
                    OnPropertyChanged("khoiLuong");
                }
            }
        }

        public string thuMucMedia
        {
            get
            {
                return sanPhamHienThi.thuMucMedia;
            }

            set
            {
                if (sanPhamHienThi.thuMucMedia != value)
                {
                    sanPhamHienThi.thuMucMedia = value;
                    OnPropertyChanged("thuMucMedia");
                    vmMedia.folderPath = sanPhamHienThi.thuMucMedia;
                }
            }
        }

        public ViewModelMedia vmMedia { get; set; }

        public string moTaChiTiet
        {
            get
            {
                return sanPhamHienThi.moTaChiTiet;
            }

            set
            {
                if (sanPhamHienThi.moTaChiTiet != value)
                {
                    sanPhamHienThi.moTaChiTiet = value;
                    OnPropertyChanged("moTaChiTiet");
                }
            }
        }

        public string viTriLuuKho
        {
            get
            {
                return sanPhamHienThi.viTriLuuKho;
            }

            set
            {
                if(sanPhamHienThi.viTriLuuKho != value)
                {
                    sanPhamHienThi.viTriLuuKho = value;
                    OnPropertyChanged("viTriLuuKho");
                }
            }
        }


        /// <summary>
        /// Check dữ liệu nhập vào đúng định dạng, giá trị
        /// </summary>
        /// <returns></returns>
        public Boolean CheckValidInputs()
        {
            // Mã sản phẩm không trống
            if (string.IsNullOrWhiteSpace(maSanPham))
            {
                MessageBox.Show("Mã sản phẩm không được để trống");
                return false;
            }

            // Tên sản phẩm không trống
            if(string.IsNullOrWhiteSpace(tenSanPham))
            {
                MessageBox.Show("Tên sản phẩm không được để trống");
                return false;
            }

            return true;
        }

        public void Save()
        {
            if(!CheckValidInputs())
            {
                return;
            }
            Boolean bResult = true;
            string strError = "";
            try
            {
                do
                {
                    // Nếu mã sản phẩm chưa tồn tại, tạo mới
                    if (listMaSanPham.Count() == 0)
                    {
                        if(!sanPhamHienThi.CanAddAProduceWithTenSP(actionModelThongTinChiTiet, maSanPham, tenSanPham, false))
                        {
                            MessageBox.Show(sanPhamHienThi.GetErrorMessage());
                            return;
                        }
                        if (!sanPhamHienThi.AddAProduceToXDocAndSave(actionModelThongTinChiTiet))
                        {
                            // Cập nhật vào list truy xuất nhanh
                            InitializeBuffer(actionModelThongTinChiTiet);
                            bResult = false;
                            break;
                        }
                    }
                    else // Cập nhật
                    {
                        if (!sanPhamHienThi.CanUpdateAProducde(actionModelThongTinChiTiet, maSanPham, tenSanPham, false))
                        {
                            MessageBox.Show(sanPhamHienThi.GetErrorMessage());
                            return;
                        }
                        if (!sanPhamHienThi.UpdateAProducToXDocAndSave(actionModelThongTinChiTiet))
                        {
                            bResult = false;
                            break;
                        }
                    }
                    // Lưu thông tin nhập xuất chi tiết
                    if(!nhapXuatChiTiet.AddOrUpdateAProduceToXDocAndSave(actionModelNhapXuatChiTiet, maSanPham, soLuongNhap))
                    {
                        bResult = false;
                        break;
                    }
                } while (false);
            }
            catch (FormatException ex)
            {
                bResult = false;
                strError = ex.Message;
            }
            catch(OverflowException ex)
            {
                bResult = false;
                strError = ex.Message;
            }
            catch (Exception ex)
            {
                bResult = false;
                strError = ex.Message;
            }

            if (bResult)
            {
                OnPropertyChanged("tonKho");
                General.Common.ShowAutoClosingMessageBox("Lưu thành công", "Lưu");
                // Cập nhật source của combobox
                listMaSanPham = SearchMaSanPhamAText(maSanPham, ParameterSearch.Last);
                listTenSanPham = SearchTenSanPhamAText(tenSanPham, ParameterSearch.First);
                listNhaXuatBan = SearchNhaXuatBanAText(nhaXuatBan, ParameterSearch.First);
                listNhaPhatHanh = SearchNhaPhatHanhAText(nhaPhatHanh, ParameterSearch.First);

                //if (sanPhamHienThi.CreateSampleData())
                //{
                //    General.Common.ShowAutoClosingMessageBox("CreateSampleData thành công", "Sản phẩm");
                //}
                //else
                //{
                //    General.Common.ShowAutoClosingMessageBox("CreateSampleData thất bại", "Sản phẩm");
                //}
            }
            else
            {
                MessageBox.Show("Lưu không thành công!" + strError, "Sản phẩm", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Delete()
        {
            MessageBoxResult boxResult = MessageBox.Show("Không thể lấy lại thông tin đã xóa. Bạn chắc chắn muốn xóa sản phẩm này và dữ liệu liên quan?",
                "Xóa sản phẩm", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (boxResult == MessageBoxResult.No)
                return;

            if(string.IsNullOrWhiteSpace(maSanPham))
            {
                MessageBox.Show("Không thể xóa vì ô mã sản phẩm trống", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Check sản phẩm có tồn tại
            ObservableCollection<string> lTemp = SearchMaSanPhamAText(maSanPham, ParameterSearch.Same);
            if(lTemp.Count() == 0)
            {
                MessageBox.Show("Sản phẩm không tồn tại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            sanPhamHienThi.Delete(actionModelThongTinChiTiet);
            nhapXuatChiTiet.Delete(actionModelNhapXuatChiTiet, maSanPham);
            // Cập nhật vào list truy xuất nhanh
            InitializeBuffer(actionModelThongTinChiTiet);
            General.Common.ShowAutoClosingMessageBox("Xóa thành công", "Xóa");
        }

        public void UpdateSanPhamHienThi()
        {
            listMaSanPham = ListMaSanPham();
            listTenSanPham = ListTenSanPham();
            listNhaPhatHanh = ListNhaPhatHanh();
            listNhaXuatBan = ListNhaXuatBan();
        }

        public void GetListInOutWarehouse()
        {
            visibilitySaveDelete = Visibility.Collapsed;
            MainWindow mw = (MainWindow)App.Current.MainWindow;
            mw.GetListInOutInWarehouse();
        }
    }
}
