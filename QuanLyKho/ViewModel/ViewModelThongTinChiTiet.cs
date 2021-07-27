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

namespace QuanLyKho.ViewModel
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
        public ModelThongTinChiTiet sanPhamHienThi { get; set; }
        public ModelNhapXuatChiTiet nhapXuatChiTiet { get; set; }

        
        public ViewModelThongTinChiTiet()
        {
            vmMedia = new ViewModelMedia();
            try
            {
                sanPhamHienThi = new ModelThongTinChiTiet();
                nhapXuatChiTiet = new ModelNhapXuatChiTiet();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            _commandSave = new CommandThongTinChiTiet_Save(this);
            _commandDelete = new CommandThongTinChiTiet_Delete(this);
        }

        #region Mã sản phẩm
        private Boolean pIsDropDownOpen_listMaSanPham;

        public Boolean isDropDownOpen_listMaSanPham
        {
            get
            {
                return pIsDropDownOpen_listMaSanPham;
            }

            set
            {
                pIsDropDownOpen_listMaSanPham = value;
                OnPropertyChanged("isDropDownOpen_listMaSanPham");
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
        public string maSanPham
        {
            get
            {
                return sanPhamHienThi.maSanPham;
            }

            set
            {
                if (sanPhamHienThi.maSanPham != value)
                {
                    sanPhamHienThi.maSanPham = value;
                    OnPropertyChanged("maSanPham");
                    // Tìm sản phẩm có mã giống
                    listMaSanPham = sanPhamHienThi.SearchMaSanPhamAText(value, ParameterSearch.Same);
                    if (listMaSanPham.Count() == 1)
                    {
                        sanPhamHienThi.GetASanPhamFromMaSanPham(sanPhamHienThi.maSanPham);

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
                        OnPropertyChanged("thuMucMedia");
                        OnPropertyChanged("moTaChiTiet");

                        // Cập nhật các buffer khác
                        listTenSanPham = sanPhamHienThi.SearchTenSanPhamAText(tenSanPham, ParameterSearch.Same);
                        listNhaPhatHanh = sanPhamHienThi.SearchNhaPhatHanhAText(nhaPhatHanh, ParameterSearch.Same);
                        listNhaXuatBan = sanPhamHienThi.SearchNhaXuatBanAText(nhaXuatBan, ParameterSearch.Same);
                    }
                    else// Tìm sản phẩm có mã kết thúc giống
                    {
                        listMaSanPham = sanPhamHienThi.SearchMaSanPhamAText(value, ParameterSearch.Last);
                    }
                    if (listMaSanPham.Count != 0)
                        isDropDownOpen_listMaSanPham = true;
                    else
                        isDropDownOpen_listMaSanPham = false;
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
        private Boolean pIsDropDownOpen_listTenSanPham;

        public Boolean isDropDownOpen_listTenSanPham
        {
            get
            {
                return pIsDropDownOpen_listTenSanPham;
            }

            set
            {
                pIsDropDownOpen_listTenSanPham = value;
                OnPropertyChanged("isDropDownOpen_listTenSanPham");
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
                if (sanPhamHienThi.tenSanPham != value)
                {
                    sanPhamHienThi.tenSanPham = value;
                    OnPropertyChanged("tenSanPham");
                    // Tìm sản phẩm có mã giống
                    listTenSanPham = sanPhamHienThi.SearchTenSanPhamAText(value, ParameterSearch.Same);
                    if (listTenSanPham.Count() == 1)
                    {
                        sanPhamHienThi.GetASanPhamFromTenSanPham(sanPhamHienThi.tenSanPham);

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
                        OnPropertyChanged("thuMucMedia");
                        OnPropertyChanged("moTaChiTiet");

                        // Cập nhật các buffer khác
                        listMaSanPham = sanPhamHienThi.SearchMaSanPhamAText(maSanPham, ParameterSearch.Same);
                        listNhaPhatHanh = sanPhamHienThi.SearchNhaPhatHanhAText(nhaPhatHanh, ParameterSearch.Same);
                        listNhaXuatBan = sanPhamHienThi.SearchNhaXuatBanAText(nhaXuatBan, ParameterSearch.Same);
                    }
                    else// Tìm sản phẩm có tên chứa
                    {
                        listTenSanPham = sanPhamHienThi.SearchTenSanPhamAText(value, ParameterSearch.All);
                    }
                    if (listTenSanPham.Count != 0)
                        isDropDownOpen_listTenSanPham = true;
                    else
                        isDropDownOpen_listTenSanPham = false;
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
        private Boolean pIsDropDownOpen_listNhaPhatHanh;

        public Boolean isDropDownOpen_listNhaPhatHanh
        {
            get
            {
                return pIsDropDownOpen_listNhaPhatHanh;
            }

            set
            {
                pIsDropDownOpen_listNhaPhatHanh = value;
                OnPropertyChanged("isDropDownOpen_listNhaPhatHanh");
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
                    listNhaPhatHanh = sanPhamHienThi.SearchNhaPhatHanhAText(value, ParameterSearch.First);
                    if (listNhaPhatHanh.Count != 0)
                        isDropDownOpen_listNhaPhatHanh = true;
                    else
                        isDropDownOpen_listNhaPhatHanh = false;
                }
            }
        }
        #endregion

        #region Nhà Xuất Bản

        private Boolean pIsDropDownOpen_listNhaXuatBan;

        public Boolean isDropDownOpen_listNhaXuatBan
        {
            get
            {
                return pIsDropDownOpen_listNhaXuatBan;
            }

            set
            {
                pIsDropDownOpen_listNhaXuatBan = value;
                OnPropertyChanged("isDropDownOpen_listNhaXuatBan");
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
                    listNhaXuatBan = sanPhamHienThi.SearchNhaXuatBanAText(value, ParameterSearch.First);
                    if (listNhaXuatBan.Count != 0)
                        isDropDownOpen_listNhaXuatBan = true;
                    else
                        isDropDownOpen_listNhaXuatBan = false;
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

            // Tên sản phẩm không trông
            if(string.IsNullOrWhiteSpace(tenSanPham))
            {
                MessageBox.Show("Tên sản phẩm không được để trống");
                return false;
            }

            // Check năm xuất bản
            if (!Common.CheckTimeValid(namXuatBan, true))
            {
                MessageBox.Show("Thời gian nhập không đúng.");
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
                        if(!sanPhamHienThi.CanAddAProduceWithTenSP(maSanPham, tenSanPham, false))
                        {
                            MessageBox.Show(sanPhamHienThi.GetErrorMessage());
                            return;
                        }
                        if (!sanPhamHienThi.AddAProduceToXDocAndSave())
                        {
                            bResult = false;
                            break;
                        }
                    }
                    else // Cập nhật
                    {
                        if (!sanPhamHienThi.CanUpdateAProducde(maSanPham, tenSanPham, false))
                        {
                            MessageBox.Show(sanPhamHienThi.GetErrorMessage());
                            return;
                        }
                        if (!sanPhamHienThi.UpdateAProducToXDocAndSave())
                        {
                            bResult = false;
                            break;
                        }
                    }
                    // Lưu thông tin nhập xuất chi tiết
                    if(!nhapXuatChiTiet.AddOrUpdateAProduceToXDocAndSave(maSanPham, soLuongNhap))
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
                General.Common.ShowAutoClosingMessageBox("Lưu thành công", "Sản phẩm");
                // Cập nhật source của combobox
                listMaSanPham = sanPhamHienThi.SearchMaSanPhamAText(maSanPham, ParameterSearch.Last);
                listNhaXuatBan = sanPhamHienThi.SearchNhaXuatBanAText(nhaXuatBan, ParameterSearch.First);
                listNhaPhatHanh = sanPhamHienThi.SearchNhaPhatHanhAText(nhaPhatHanh, ParameterSearch.First);
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
            ObservableCollection<string> lTemp = sanPhamHienThi.SearchMaSanPhamAText(maSanPham, ParameterSearch.Same);
            if(lTemp.Count() == 0)
            {
                MessageBox.Show("Sản phẩm không tồn tại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            sanPhamHienThi.Delete();
            nhapXuatChiTiet.Delete(maSanPham);
            General.Common.ShowAutoClosingMessageBox("Xóa thành công", "Sản phẩm");
        }

        public void UpdateSanPhamHienThi()
        {
            listMaSanPham = sanPhamHienThi.ListMaSanPham();
            listTenSanPham = sanPhamHienThi.ListTenSanPham();
            listNhaPhatHanh = sanPhamHienThi.ListNhaPhatHanh();
            listNhaXuatBan = sanPhamHienThi.ListNhaXuatBan();
        }
    }
}
