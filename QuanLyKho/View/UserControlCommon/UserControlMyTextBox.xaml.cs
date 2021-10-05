using QuanLyKho.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyKho.View.UserControlCommon
{
    /// <summary>
    /// Interaction logic for UserControlMyTextBox.xaml
    /// </summary>
    public partial class UserControlMyTextBox : UserControl
    {
        public enum eMyTextBoxTypes
        {
            MyNormal,  // Text bình thường
            MyTime,  // Text thời gian
            MyInteger,  // Text số nguyên, cả âm và dương
            MyPositiveFloat, // Text số thực dương
            MyNotNullOrWhiteSpace, // Text không null, trống hay khoảng trắng
            MyMoneyVND // Text tiền VND
        }

        public UserControlMyTextBox()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty MyTextBoxTextProperty = DependencyProperty.Register("MyTextBoxText", typeof(String), typeof(UserControlMyTextBox), null);
        public String MyTextBoxText
        {
            get { return (String)GetValue(MyTextBoxTextProperty); }
            set { SetValue(MyTextBoxTextProperty, value); }
        }

        public static readonly DependencyProperty MyTextBoxTypeProperty = DependencyProperty.Register("MyTextBoxType", typeof(eMyTextBoxTypes), typeof(UserControlMyTextBox), null);
        public eMyTextBoxTypes MyTextBoxType
        {
            get { return (eMyTextBoxTypes)GetValue(MyTextBoxTypeProperty); }
            set { SetValue(MyTextBoxTypeProperty, value); }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (MyTextBoxType == eMyTextBoxTypes.MyNormal)
            {

            }
            else if (MyTextBoxType == eMyTextBoxTypes.MyTime)
            {
                MyTextBoxName.ToolTip = "Nhập thời gian: Ngày/Tháng/Năm, Tháng/Năm hoặc Năm";
            }
            else if (MyTextBoxType == eMyTextBoxTypes.MyInteger)
            {
                MyTextBoxName.ToolTip = "Nhập số tự nhiên dương hoặc âm. VD: 5, -5, 10, -10,...";
            }
            else if (MyTextBoxType == eMyTextBoxTypes.MyPositiveFloat)
            {
                MyTextBoxName.ToolTip = "Nhập số thực dương. VD: 5.0; 5.14; 5,0; 5,14;...";
            }
            else if (MyTextBoxType == eMyTextBoxTypes.MyNotNullOrWhiteSpace)
            {
                MyTextBoxName.ToolTip = "Không để để trống, không được nhập chỉ toàn ký tự trắng";
            }
            else if (MyTextBoxType == eMyTextBoxTypes.MyMoneyVND)
            {
                MyTextBoxName.ToolTip = "Nhập số tiền đơn vị đồng.";
            }
        }

        #region Thời gian
        /// <summary>
        /// Định dạng: YYYY
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private Boolean CheckYear(string text)
        {
            Int32 year;
            try
            {
                year = Int32.Parse(text);
            }
            catch (Exception ex)
            {
                MyLogger.GetInstance().Warn(ex.Message);
                return false;
            }
            if (year > 9999 || year < 1900)
                return false;

            return true;
        }

        /// <summary>
        /// Định dạng: M hoặc MM
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private Boolean CheckMonth(string text)
        {
            Int32 month;
            try
            {
                month = Int32.Parse(text);
            }
            catch (Exception ex)
            {
                MyLogger.GetInstance().Warn(ex.Message);
                return false;
            }
            if (month > 12 || month < 1)
                return false;

            return true;
        }

        /// <summary>
        /// Định dạng D hoặc DD
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private Boolean CheckDayOfMonth(string text)
        {
            Int32 day;
            try
            {
                day = Int32.Parse(text);
            }
            catch (Exception ex)
            {
                MyLogger.GetInstance().Warn(ex.Message);
                return false;
            }
            if (day > 31 || day < 1)
                return false;

            return true;
        }

        public Boolean CheckValidMyTime(string text)
        {
            char[] delimiterChars = { '_', '.', '-', '/' };
            string[] words = text.Split(delimiterChars);
            Boolean isOk = true;
            do
            {
                int length = words.Length;
                if (length > 3 || length < 1)
                {
                    isOk = false;
                    break;
                }

                // Text dạng YYYY
                if (length == 1)
                {
                    if (!CheckYear(words[0]))
                    {
                        isOk = false;
                        break;
                    }
                }
                else if (length == 2) // Text dạng MM/YYYY
                {
                    if (!CheckMonth(words[0]) || !CheckYear(words[1]))
                    {
                        isOk = false;
                        break;
                    }
                }
                else // Text dạng DD/MM/YYYY
                {
                    if (!CheckDayOfMonth(words[0]) || !CheckMonth(words[1]) || !CheckYear(words[2]))
                    {
                        isOk = false;
                        break;
                    }
                }

            } while (false);

            if (!isOk)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Text số nguyên, cả âm và dương
        public Boolean CheckValidMyInteger(string text)
        {
            Boolean isOk = true;
            Int32 result;
            if (!Int32.TryParse(text, out result))
            {
                isOk = false;
            }
            return isOk;
        }
        #endregion

        #region Số thực dương
        public Boolean CheckValidMyPositiveFloat(string text)
        {
            Boolean isOk = true;
            float result;
            if (!float.TryParse(text, out result))
            {
                isOk = false;
            }
            else
            {
                if (result < 0)
                {
                    isOk = false;
                }
            }
            return isOk;
        }
        #endregion

        #region Không để để trống, không được nhập chỉ toàn ký tự trắng
        public Boolean CheckValidNotNullOrWhiteSpace(string text)
        {
            Boolean isOk = true;
            if (string.IsNullOrWhiteSpace(text))
            {
                isOk = false;
            }
            return isOk;
        }
        #endregion

        #region Text tiền VND
        public Boolean CheckValidMyMoneyVND(string text)
        {
            Boolean isOk = true;

            return isOk;
        }
        #endregion

        public string CheckInvalidAndReturnErrorMessage()
        {
            string errorMessage = string.Empty;

            if (MyTextBoxType == eMyTextBoxTypes.MyNormal)
            {
                if (!CheckValidMyTime(MyTextBoxName.Text))
                {
                    errorMessage = "Thời gian nhập không đúng.";
                    MyTextBoxName.Text = string.Empty;
                }
            }
            else if (MyTextBoxType == eMyTextBoxTypes.MyTime)
            {
                if (!CheckValidMyTime(MyTextBoxText))
                {
                    errorMessage = "Thời gian nhập không chính xác.";
                }
            }
            else if (MyTextBoxType == eMyTextBoxTypes.MyInteger)
            {
                if (!CheckValidMyInteger(MyTextBoxText))
                {
                    errorMessage = "Số nguyên nhập không chính xác.";
                }
            }
            else if (MyTextBoxType == eMyTextBoxTypes.MyPositiveFloat)
            {
                if (!CheckValidMyPositiveFloat(MyTextBoxText))
                {
                    errorMessage = "Số thực dương nhập không chính xác.";
                }
            }
            else if (MyTextBoxType == eMyTextBoxTypes.MyNotNullOrWhiteSpace)
            {
                if (!CheckValidNotNullOrWhiteSpace(MyTextBoxText))
                {
                    errorMessage = "Không để để trống, không được nhập chỉ toàn ký tự trắng.";
                }
            }
            return errorMessage;
        }

        private void MyTextBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            /// Thêm dấu , mỗi 3 ký tự VD: 100,000,000
            if (MyTextBoxType == eMyTextBoxTypes.MyMoneyVND)
            {
                /// Xóa bỏ ký tự , đã thêm
                StringBuilder str =  new StringBuilder(MyTextBoxText);
                str.Replace(",", "");
                int oldLength = str.Length;
                int i = 1;
                while(oldLength - i * 3 > 0)
                {
                    str.Insert(oldLength - i * 3, ",");
                    i++;
                }

                Int32 caret = ((TextBox)sender).CaretIndex;
                if (MyTextBoxText.Length < str.Length)
                    caret++;
                else if (MyTextBoxText.Length > str.Length)
                    caret--;
                MyTextBoxText = str.ToString();
                ((TextBox)sender).CaretIndex = caret;
            }
        }
    }
}
