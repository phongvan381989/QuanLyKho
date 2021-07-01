using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.General
{
    class Common
    {
        /// <summary>
        /// Kiểm tra 1 string có là số nguyên hợp lệ
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// 
        public const Int32 maxInteger = 10000;
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
                return Int32.Parse(str);
            }
        }
    }
}
