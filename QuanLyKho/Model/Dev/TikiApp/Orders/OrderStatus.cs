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
    public enum OrderStatus
    {
        //Value              |Value-VN    |Description |Description – VN
        queueing,           //    cho_in  tiki received   Chờ in,
        canceled,           //    canceled    canceled order  Đã hủy
        complete,           //    complete    complete order  Đơn hàng đã hoàn thành
        successful_delivery,// giao_hang_thanh_cong    successful delivery Giao hàng thành công
        processing,         //  processing  order is being processing   đơn hàng đang được xử lý
        waiting_payment,    // doi_thanh_toan  waiting for payment đơn hàng đợi thanh toán
        handover_to_partner,// ban_giao_doi_tac    handover to partner đơn hàng đã bàn giao đối tác
        closed,             //  closed  closed  đơn hàng đã đóng
        packaging,          //   dang_dong_goi   packaging   đơn hàng đang được đóng gói
        picking,            // dang_lay_hang   picking shipper đang lấy hàng
        shipping,           //   dang_van_chuyen shipping    shipper đang vận chuyển
        paid,               //    da_thanh_toan   paid    đã thanh toán
        delivered,          //  giao_hang_thanh_cong    success delivery    giao hàng thành công
        holded,             //  holded  holded
        ready_to_ship,      //   len_ke  ready to ship   đơn hàng sẵn sàng được giao
        payment_review,     //  payment_review  payment review
        returned,           //    returned        đơn hàng đã được trả
        finished_packing,   //    dong_goi_xong   finished packing    đơn hàng đã đóng gói xong

    }
}
