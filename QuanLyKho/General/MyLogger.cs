using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.General
{
    /// <summary>
    /// Logging.
    /// </summary>
    public class MyLogger
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Get logger.
        /// </summary>
        /// <returns>The instance.</returns>
        public static log4net.ILog GetInstance()
        {
            return Logger;
        }

        /// <summary>
        /// Write log.
        /// </summary>
        /// <param name="message">Save this to log file.</param>
        public static void Write(string message)
        {
            Logger.Info(message);
        }
    }
}
