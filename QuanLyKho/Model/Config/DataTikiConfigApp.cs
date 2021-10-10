namespace QuanLyKho.Model.Config
{
    /// <summary>
    /// Chứa cấu hình để connect tới shop Tiki gồm: appID, homeAddress, secretAppCode
    /// </summary>
    public class DataTikiConfigApp
    {
        public const string constUsingApp = "Đang Sử Dụng";
        public const string constNotUsingApp = "Không Sử Dụng";
        public DataTikiConfigApp()
        {
            Empty();
        }
        public string appID { get; set; }
        public string homeAddress { get; set; }
        public string secretAppCode { get; set; }
        public string usingApp { get; set; } // Nhận 1 trong 2 giá trị: Đang Sử Dụng hoặc Không Sử Dụng

        public DataTikiConfigApp(string inputAppID, string inputHomeAddress, string inputSecretAppCode, string inputUsingApp)
        {
            appID = inputAppID;
            homeAddress = inputHomeAddress;
            secretAppCode = inputSecretAppCode;
            usingApp = inputUsingApp;
        }

        public void Empty()
        {
            appID = string.Empty;
            homeAddress = string.Empty;
            secretAppCode = string.Empty;
            usingApp = constNotUsingApp;
        }

        public void SetAllValue(string inputAppID, string inputHomeAddress, string inputSecretAppCode, string inputUsingApp)
        {
            appID = inputAppID;
            homeAddress = inputHomeAddress;
            secretAppCode = inputSecretAppCode;
            usingApp = inputUsingApp;
        }
    }
}
