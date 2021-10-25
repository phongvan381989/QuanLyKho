using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKho.Model.Dev.TikiApp.Orders
{
    public class OrderItemV2
    {
        /// <summary>
        /// 156453540	Unique Id of the order item
        /// </summary>
        public Int32 id { get; set; }
        /// <summary>
        /// 	Product information at the time of the purchasing
        /// </summary>
        public OrderItemProduct product { get; set; }
        /// <summary>
        /// Invoice information: Value, discounts and fees of customer and seller in this item
        /// </summary>
        public OrderItemInvoice invoice { get; set; }
        /// <summary>
        /// Seller basic information
        /// </summary>
        public OrderItemSeller seller { get; set; }
        /// <summary>
        /// Confirmation status and SLAs
        /// </summary>
        public OrderItemConfirmation confirmation { get; set; }
        /// <summary>
        /// Information of the requisitions of the product items into Tiki warehouse (which will be delivered to customer later then)
        /// </summary>
        public InventoryRequisition inventory_requisition { get; set; }
        /// <summary>
        /// Information of the withdrawals of the product items from Tiki warehouse
        /// </summary>
        public InventoryWithdrawal inventory_withdrawals { get; set; }
    }
}
