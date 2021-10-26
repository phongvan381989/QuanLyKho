using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.TikiApp.Orders
{
    public class Order
    {
        public Int32 id { get; set; }

        /// <summary>
        /// 745467462	Unique order code
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// cross_border	How the order is fulfilled a.k.a the fulfillment model of the order
        /// See order fulfillment type
        /// </summary>
        public string fulfillment_type { get; set; }

        /// <summary>
        /// complete	The status where the order is in the process to customer hands
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// 	All Order Item V2s details
        /// </summary>
        public List<OrderItemV2> items { get; set; }

        /// <summary>
        /// 896493516	The order code of the parent order
        /// The parent order is splited or modified into this (and others) order
        /// </summary>
        public string relation_code { get; set; }

        /// <summary>
        /// false	Is this order a replacement for a returned product or a servicing product (RMA flow)
        /// </summary>
        public bool is_rma { get; set; }
        /// <summary>
        /// false	If the customer requested to export VAT bill
        /// </summary>
        public bool is_vat_exporting { get; set; }
        /// <summary>
        /// true	If this order has at least 1 Order Item V2 with inventory_type=backorder
        /// </summary>
        public bool has_backorder_items { get; set; }
        /// <summary>
        /// 2020-08-10 18:50:17	When the order is created
        /// </summary>
        public DateTime created_at { get; set; }
        /// <summary>
        /// 2020-08-18 11:10:02	The latest update time of the order
        /// </summary>
        public DateTime updated_at { get; set; }
        /// <summary>
        /// 	The order invoice data: Total value, discounts, fees of customer and seller
        /// </summary>
        public OrderInvoice invoice { get; set; }
        /// <summary>
        /// 	The Tiki warehouse that fulfills the order
        /// </summary>
        public TikiWarehouseInfo tiki_warehouse { get; set; }
        /// <summary>
        /// 	Shipping information of the order: Shipping status, plan, partner, address
        /// </summary>
        public ShippingInfo shipping { get; set; }
        /// <summary>
        /// 	Payment information
        /// </summary>
        public PaymentInfo payment { get; set; }
        /// <summary>
        /// 	Delivery confirmation
        /// </summary>
        public DeliveryConfirmation delivery { get; set; }
        /// <summary>
        /// 	Customer information
        /// </summary>
        public CustomerInfo customer { get; set; }
        /// <summary>
        /// 	Installment information
        /// </summary>
        public InstallmentInfo installment_info { get; set; }
        /// <summary>
        /// 	Cancellation details
        /// </summary>
        public CancelInfo cancel_info { get; set; }
        /// <summary>
        /// 	RMA information
        /// </summary>
        public RMAInfo rma_info { get; set; }
        /// <summary>
        /// 	All the status changes of this order
        /// </summary>
        public IList<StatusHistory> status_histories { get; set; }
    }
}
