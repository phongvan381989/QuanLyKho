using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyKho.General
{
    public enum ParameterSearch
    {
        First,
        Last,
        All
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
        public static readonly Int32 timeToCloseMessageBox = 2000;

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
    }
}
