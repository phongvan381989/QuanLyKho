using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.TikiApp.Orders
{
    /// <summary>
    /// 
    /// </summary>
    public class ShippingInfo
    {
        /// <summary>
        /// 2	Selected shipping partner unique Id
        /// </summary>
        public string partner_id { get; set; }

        /// <summary>
        /// GHN	Selected shipping partner name
        /// </summary>
        public string partner_name { get; set; }

        /// <summary>
        /// 31416789187639	Tracking number for the shipment
        /// </summary>
        public string tracking_code { get; set; }

        /// <summary>
        /// Delivered	Shipping status
        /// </summary>
        public string status { get; set; }

        public string pickup_shipping_code { get; set; }

        public string pickup_partner_code { get; set; }

        public string return_shipping_code { get; set; }

        public string return_partner_code { get; set; }

        public string delivery_shipping_code { get; set; }

        public string delivery_partner_code { get; set; }

        /// <summary>
        /// 	Shipping plan details
        /// </summary>
        public ShippingPlan plan { get; set; }

        /// <summary>
        /// 	Shipping address details
        /// </summary>
        public ShippingAddress address { get; set; }

        public ShippingDetail shipping_detail { get; set; }
    }
}
