﻿using QuanLyKho.Model;
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
        private ModelThongTinChiTiet sanPhamHienThi;

        public ViewModelThongTinChiTiet()
        {
            sanPhamHienThi = new ModelThongTinChiTiet();
            listNhaPhatHanh = new ObservableCollection<string>();
            _commandSave = new CommandThongTinChiTiet_Save(this);
        }

        public string maISBN
        {
            get
            {
                return sanPhamHienThi.maISBN;
            }

            set
            {
                if (sanPhamHienThi.maISBN != value)
                {
                    sanPhamHienThi.maISBN = value;
                    OnPropertyChanged("maISBN");
                }
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

        public ObservableCollection<string> listNhaPhatHanh { get; set; }

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
                }
            }
        }

        public string moTaChiTietSanPham
        {
            get
            {
                return sanPhamHienThi.moTaChiTietSanPham;
            }

            set
            {
                if (sanPhamHienThi.moTaChiTietSanPham != value)
                {
                    sanPhamHienThi.moTaChiTietSanPham = value;
                    OnPropertyChanged("moTaChiTietSanPham");
                }
            }
        }

        public void Save()
        {
            if(sanPhamHienThi.Save())
            {
                General.Common.ShowAutoClosingMessageBox("Lưu thành công", "Sản phẩm");
            }
            else
            {
                MessageBox.Show("Lưu không thành công", "Sản phẩm");
            }
        }

        public void UpdateSanPhamHienThi()
        {
            if (sanPhamHienThi == null)
                sanPhamHienThi = new ModelThongTinChiTiet();
            sanPhamHienThi.maISBN = "123456789";
            sanPhamHienThi.thuMucMedia = @"E:\TUNM\QuanLyKho\QuanLyKho\obj\Debug\View";
            sanPhamHienThi.moTaChiTietSanPham = @"sách quá là hay.";

            listNhaPhatHanh.Add("Giri");
            listNhaPhatHanh.Add("Taku");
            listNhaPhatHanh.Add("Saburo");
            listNhaPhatHanh.Add("Nene");
        }
    }
}
