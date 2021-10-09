using QuanLyKho.Model.Dev;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Config
{
    /// <summary>
    /// Chứa cấu hình để connect tới shop Tiki
    /// </summary>
    public class TikiConfigApp
    {
        public TikiConfigApp()
        {
            modelTTBM = new ModelThongTinBaoMatTiki();
            appID = string.Empty;
            homeAddress = string.Empty;
            secretAppCode = string.Empty;
        }
        private ModelThongTinBaoMatTiki modelTTBM;
        public string appID { get; set; }
        public string homeAddress { get; set; }
        public string secretAppCode { get; set; }
        /// <summary>
        /// Thêm hoặc cập nhật và lưu vào db
        /// </summary>
        public string Add()
        {
            modelTTBM.Tiki_InhouseAppAddOrUpdate(appID, homeAddress, secretAppCode);
            return string.Empty;
        }

        public string Delete()
        {
            if (!modelTTBM.Tiki_CheckClientIDExist(appID))
                return "ID Ứng Dụng không tồn tại";

            modelTTBM.Tiki_InhouseAppDelete(appID);
            return string.Empty;
        }
    }
}
