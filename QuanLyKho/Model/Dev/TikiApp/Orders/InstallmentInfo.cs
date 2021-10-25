using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.TikiApp.Orders
{
    public class InstallmentInfo
    {
        /// <summary>
        /// 240000.00	Installment fee of the order
        /// </summary>
        public Double installment_fee { get; set; }

        /// <summary>
        /// 12	Installment payment term in number of months
        /// </summary>
        public Int32 month { get; set; }

        /// <summary>
        /// 390833.00	The monthly pay during the payment term
        /// </summary>
        public Double monthly_pay { get; set; }
    }
}
