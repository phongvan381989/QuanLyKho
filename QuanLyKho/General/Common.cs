using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace QuanLyKho.General
{
    public enum ParameterSearch
    {
        First,
        Last,
        All,
        Same
    }

    class Common
    {
        /// <summary>
        /// Kiểm tra 1 string có là số nguyên hợp lệ
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// 
        public const Int32 maxInteger = 10000;

        /// <summary>
        /// Thời gian để 1 message box tự đóng tính theo miliseconds
        /// </summary>
        public static Int32 timeToCloseMessageBox = 2000;

        public static Boolean IsInteger(string text)
        {
            if (String.IsNullOrEmpty(text))
                return false;

            // Check cho phép 1 ký tự dấu âm '-' ở đầu string
            if (text.Length == 1 && text.ElementAt(0)== '-')
                return false;

            int l = text.Length;
            
            for(int i = 1; i < l; i++)
            {
                if (!Char.IsDigit(text.ElementAt(i)))
                    return false;
            }

            try
            {
                int result = Int32.Parse(text);
                if(result > maxInteger)
                {
                    return false;
                }
                Console.WriteLine(result);
            }
            catch (FormatException)
            {
                //Console.WriteLine($"Unable to parse '{input}'");
                MyLogger.GetInstance().DebugFormat("Unable to parse '{text}'", text);
                return false;
            }
            return true;
        }

        public static Int32 ConvertStringToInt32(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return 0;
            }
            else
            {
                try
                {
                    return Int32.Parse(str);
                }
                catch(FormatException ex)
                {
                    throw new FormatException("Không thể chuyển đổi sang số do sai định dạng. " + ex.Message );
                }
                catch(OverflowException ex)
                {
                    throw new OverflowException("Giá trị số quá lớn. " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Hàm trả về fail, chi tiết lỗi sẽ được lưu trong biến này
        /// </summary>
        public static string CommonErrorMessage;

        #region Message box tự động đóng sau n giây
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true, CharSet = CharSet.Auto)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.Dll")]
        static extern int PostMessage(IntPtr hWnd, UInt32 msg, int wParam, int lParam);

        private const UInt32 WM_CLOSE = 0x0010;

        public static void ShowAutoClosingMessageBox(string message, string caption)
        {
            var timer = new System.Timers.Timer(timeToCloseMessageBox) { AutoReset = false };
            timer.Elapsed += delegate
            {
                IntPtr hWnd = FindWindowByCaption(IntPtr.Zero, caption);
                if (hWnd.ToInt32() != 0)
                    PostMessage(hWnd, WM_CLOSE, 0, 0);
            };
            timer.Enabled = true;
            MessageBox.Show(message, caption);
        }
        #endregion

        #region Check thời gian hợp lệ
        /// <summary>
        /// Định dạng: YYYY
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Boolean CheckYear(string text)
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
        public static Boolean CheckMonth(string text)
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
        public static Boolean CheckDayOfMonth(string text)
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

        /// <summary>
        ///  Check text có thể convert sang dạng thời gian
        ///  03/08/1989 -> DD/MM/YYYY
        ///  3/8/1989 -> DD/MM/YYYY
        ///  8/1989 -> MM/YYYY
        ///  1989->YYYY
        /// </summary>
        /// <param name="text"></param>
        /// <param name="enableNullOrEmpty">True: Cho phép text là null hay empty</param>
        /// <returns></returns>
        public static Boolean CheckTimeValid(string text, Boolean enableNullOrEmpty)
        {
            if (string.IsNullOrEmpty(text))
            {
                if (enableNullOrEmpty)
                    return true;
                else
                    return false;
            }

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
                        try
                        {
                            DateTime dt = DateTime.ParseExact(text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        catch (Exception ex)
                        {
                            string ms = ex.Message;
                            isOk = false;
                        }
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

        /// <summary>
        /// Check xem ảnh đã tồn tại trong thư mục hay chưa? Nếu chưa ải ảnh từ địa chỉ web và lưu
        /// </summary>
        /// <param name="url">https://salt.tikicdn.com/cache/280x280/ts/product/c5/53/ad/991011e797c67d6910b87491ddeee138.png</param>
        /// <param name="pathFolder">Thư mục chứa ảnh</param>
        public static void DownloadImageAndSave(string url, string pathFolder)
        {
            // Từ url lấy được tên ảnh
            string fileName = GetNameFromURL(url);
            if (string.IsNullOrEmpty(fileName))
                return;

            // Check xem ảnh đã tồn tại hay chưa?
            if (File.Exists(Path.Combine(pathFolder, fileName)))
                return;

            RestClient client = new RestClient(url);
            client.Timeout = -1;
            RestRequest request = new RestRequest(Method.GET);
            //IRestResponse response = client.Execute(request);


            var fileBytes = client.DownloadData(request);
            File.WriteAllBytes(Path.Combine(pathFolder, fileName), fileBytes);
        }

        /// <summary>
        ///  Từ url lấy được tên file
        /// </summary>
        /// <param name="url"https://salt.tikicdn.com/cache/280x280/ts/product/c5/53/ad/991011e797c67d6910b87491ddeee138.pngEX: </param>
        /// <returns></returns>
        public static String GetNameFromURL(string url)
        {
            string name = string.Empty;
            // Lấy tên file ảnh
            // Từ url lấy được tên ảnh
            int lastIndex = url.LastIndexOf('/');
            if (lastIndex == -1 || lastIndex == url.Length - 1)
            {
            }
            else
            {
                string fileName = url.Substring(lastIndex + 1);
                name = Path.Combine(((App)Application.Current).temporaryTikiImageFolderPath, fileName);
            }
            return name;
        }
    }
}
