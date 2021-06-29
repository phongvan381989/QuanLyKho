using QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.ViewModel
{
    class ViewModelThongTinChiTiet
    {
        public ModelThongTinChiTiet sanPhamHienThi
        {
            get;
            set;
        }

        public void UpdateSanPhamHienThi()
        {
            if (sanPhamHienThi == null)
                sanPhamHienThi = new ModelThongTinChiTiet();
            sanPhamHienThi.maISBN = "123456789";
        }
    }
}
