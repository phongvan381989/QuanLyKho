using QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyKho.ViewModel.InOutWarehouse
{
    public class ViewModelThongTinChiTietViewOnly : ViewModelBase
    {

        public ModelThongTinChiTiet sanPhamHienThi { get; set; }

        public ViewModelThongTinChiTietViewOnly(string maSP)
        {
            vmMedia = new ViewModelMedia();
            sanPhamHienThi = ModelThongTinChiTiet.GetASanPhamFromMaSanPham(((App)Application.Current).actionModelThongTinChiTiet, maSP);
            OnPropertyChangedAll();
        }

        #region Mã sản phẩm

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
                }
            }
        }
        #endregion

        #region Nhà Xuất Bản
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
                if (sanPhamHienThi.khoiLuong != value)
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
                if (sanPhamHienThi.viTriLuuKho != value)
                {
                    sanPhamHienThi.viTriLuuKho = value;
                    OnPropertyChanged("viTriLuuKho");
                }
            }
        }
    }
}
