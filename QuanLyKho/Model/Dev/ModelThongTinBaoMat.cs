using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyKho.Model.Dev
{
    public class ModelThongTinBaoMat : ModelXML
    {
        public ModelThongTinBaoMat()
        {
            pathXML = ((App)Application.Current).GetPathDataXMLThongTinBaoMat();
            InitializeXDoc();
        }
    }
}
