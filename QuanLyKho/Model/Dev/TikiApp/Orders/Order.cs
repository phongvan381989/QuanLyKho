using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.TikiApp.Orders
{
    class Order
    {
        public string code { get; set; }
        public string relation_code { get; set; }
        public string fulfillment_type { get; set; }
        public string status { get; set; }
        public bool is_rma { get; set; }
        public bool is_vat_exporting { get; set; }
        public bool has_backorder_items { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public OrderInvoice invoice { get; set; }
        public TikiWarehouseInfo tiki_warehouse { get; set; }
        public IList<OrderItemV2> items { get; set; }
        public ShippingInfo shipping { get; set; }
        public PaymentInfo payment { get; set; }
        public DeliveryConfirmation delivery { get; set; }
        public CustomerInfo customer { get; set; }
        public InstallmentInfo installment_info { get; set; }
        public CancelInfo cancel_info { get; set; }
        public RMAInfo rma_info { get; set; }
        public IList<StatusHistory> status_histories { get; set; }
    }
}
