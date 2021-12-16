﻿using Newtonsoft.Json;
using QuanLyKho.Model;
using QuanLyKho.Model.Config;
using QuanLyKho.Model.Dev;
using QuanLyKho.Model.Dev.TikiApp;
using QuanLyKho.Model.Dev.TikiApp.Config;
using QuanLyKho.Model.Dev.TikiApp.Orders;
using QuanLyKho.Model.Dev.TikiApp.Products;
using QuanLyKho.Model.InOutWarehouse;
using QuanLyKho.ViewModel.Dev.TikiAPI;
using QuanLyKho.ViewModel.Dev.TikiAPI.Orders;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static QuanLyKho.Model.Dev.TikiApp.Orders.OrderItemFilterByDate;

namespace QuanLyKho.View.Dev
{
    /// <summary>
    /// Interaction logic for UserControlTikiTestAPI.xaml
    /// </summary>
    public partial class UserControlTikiTestAPI : UserControl
    {
        public UserControlTikiTestAPI()
        {
            InitializeComponent();
        }
        string strHTTPRequest;
        string strHTTPResponse;
        private void GetTokenAuthorization_Click(object sender, RoutedEventArgs e)
        {
            //Application.Current.MainWindow.;
        }

        private void ShowHTTPRequestAndResponse()
        {
            TextBoxHTTPRequest.Text = strHTTPRequest;
            TextboxHTTPResponse.Text = strHTTPResponse;
        }
        private void BtnGetTokenAuth_Click(object sender, RoutedEventArgs e)
        {
            string clientID = "6249716820922226";
            // TUNM Testing Start
            //ttbm.Tiki_InhouseSaveAppID("6249716820922226");
            //ttbm.Tiki_InhouseAppSaveHome("6249716820922226", "https://tiki.vn/cua-hang/play-with-me");
            //ttbm.Tiki_InhouseAppSaveSecret("6249716820922226", "vSnNO7JAWDaB_DzqKngm4y5MZTFjmflG");
            //ttbm.Tiki_InhouseAppSaveAccessToken("6249716820922226", "3LWSnEvmXuDYJQWF4NoMtoimH9vYr-3tnLon56rOelY.G0BFdINaZ6L01mPP9B4fNJkQAY8frzxGx9VnbQK4LsY");
            // TUNM Testing End

            //ttbm.Tiki_SaveClientID("1234567890");
            //var client = new RestClient("https://api.tiki.vn/integration/v1/sellers/me/inventories?status=1&type=1");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("Authorization", "Bearer " + ttbm.Tiki_InhouseGetAccessToken("6249716820922226"));//5xcP33zicKodO8crb_XWXqZ1nE6aO6_LD3qghSm91h4.vRgewC765wshvhHYzgdm5lm-PEBPe1KBsCKx5V70NAo");
            //var fullUrl = client.BuildUri(request);
            ////client.
            //strHTTPRequest = fullUrl.ToString();
            //IRestResponse response = client.Execute(request);
            //strHTTPResponse = response.Content;
            //ShowHTTPRequestAndResponse();

            var client = new RestClient(TikiConstValues.cstrAuthenHTTPAddress);
            RestRequest request = new RestRequest(Method.POST);
            //request.AddHeader("Authorization", "Basic NjI0OTcxNjgyMDkyMjIyNjpDQXlUOUJ6Q3dTQXpFMkpzempud3huN3dxUnZlcDdFWg==");
            request.AddHeader("Authorization", "Basic " + CommonTikiAPI.ttbm.Tiki_GetAppCredentialBase64Format(CommonTikiAPI.action, clientID));
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", clientID);
            request.AddParameter("scope", "");
            IRestResponse response = client.Execute(request);
            HttpStatusCode st = response.StatusCode;
            if (st == HttpStatusCode.Unauthorized)
                return;
            if (st == HttpStatusCode.OK)
            {
                strHTTPResponse = response.Content;
                ShowHTTPRequestAndResponse();

                TikiAuthorization accessToken = JsonConvert.DeserializeObject<TikiAuthorization>(response.Content);
                CommonTikiAPI.ttbm.Tiki_InhouseAppSaveAccessToken(CommonTikiAPI.action, clientID, accessToken);
            }
        }

        //App Name: CallAPIFromApplicaitionCSharp
        //App ID: 6249716820922226
        //App Secret: 9IfqSeQngzps840fhdQoZ0GLC34gTPob
        //Home: https://tiki.vn/cua-hang/play-with-me
        private void BtnGetListOrder_Click(object sender, RoutedEventArgs e)
        {
            //string appID = "6249716820922226";
            //string contentBearer = CommonTikiAPI.ttbm.Tiki_InhouseGetAccessToken(appID);
            //if(string.IsNullOrEmpty(contentBearer))
            //{
            //    string str = CommonTikiAPI.RefreshDataAuthorization(appID);
            //    if (!string.IsNullOrEmpty(str))
            //    {
            //        MessageBox.Show(str);
            //    }
            //    contentBearer = CommonTikiAPI.ttbm.Tiki_InhouseGetAccessToken(appID);
            //}
            //var client = new RestClient("https://api.tiki.vn/integration/v2/orders?page=1&limit=20&status=queueing&item_inventory_type=backorder&item_confirmation_status=waiting&filter_date_by=today");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("Authorization", "Bearer " + contentBearer);
            //var fullUrl = client.BuildUri(request);
            ////client.
            //strHTTPRequest = fullUrl.ToString();
            //IRestResponse response = client.Execute(request);
            //strHTTPResponse = response.Content;
            //ShowHTTPRequestAndResponse();
            if (CommonTikiAPI.listTikiConfigAppUsing.Count() == 0)
                return;

            List<QuanLyKho.Model.Dev.TikiApp.Orders.Order> lsOrder =  TikiGetListOrders.GetListOrderAShop(CommonTikiAPI.listTikiConfigAppUsing[0], EnumOrderItemFilterByDate.today);
        }

        private void TestSomething_Click(object sender, RoutedEventArgs e)
        {
            ////string json = @"{'data':[{'id':126222876,'code':'583518503','fulfillment_type':'dropship','status':'queueing','items':[{'id':213040707,'product':{'id':102566546,'type':'simple','super_id':0,'master_id':102566545,'sku':'9243568016837','name':'Bộ 4 quyển Sách Mê Cung Phát Triển Kỹ Năng  - Rèn Tư Duy Và Khả Năng Tập Trung Cho Bé (2-6 tuổi)','catalog_group_name':'Sách Tiếng Việt','inventory_type':'backorder','imeis':[],'serial_numbers':[],'thumbnail':'https://salt.tikicdn.com/cache/280x280/ts/product/09/52/04/d248ee4626ca1bbc5a39e1f0f3d6c1a9.jpg','seller_product_code':'','seller_supply_method':null},'seller':{'id':145228,'name':'Play With Me'},'confirmation':{'status':'waiting','confirmed_at':null,'available_confirm_sla':'2021-10-27 08:13:00','pickup_confirm_sla':null,'histories':[]},'parent_item_id':0,'price':92800,'qty':1,'fulfilled_at':null,'is_virtual':false,'is_ebook':false,'is_bookcare':false,'is_free_gift':false,'is_fulfilled':false,'backend_id':204657438,'applied_rule_ids':[2433424],'invoice':{'price':92800,'quantity':1,'subtotal':92800,'row_total':92800,'discount_amount':0,'discount_tikixu':0,'discount_promotion':0,'discount_percent':20,'discount_coupon':0,'discount_other':0,'discount_tikier':0,'discount_tiki_first':0,'discount_data':{},'is_seller_discount_coupon':false,'is_taxable':false,'fob_price':0,'seller_fee':0,'seller_income':92800,'fees':[]},'inventory_requisition':null,'inventory_withdrawals':[],'seller_inventory_id':null,'seller_inventory_name':null,'seller_income_detail':{'item_price':92800,'item_qty':1,'shipping_fee':10400,'seller_fees':[],'sub_total':100000,'seller_income':100000,'discount':{'discount_shipping_fee':{'sellerDiscount':7100,'fee_amount':10400,'qty':1,'apply_discount':[{'rule_id':2433424,'type':'seller','amount':7100,'seller_sponsor':3200,'tiki_sponsor':3900}],'seller_subsidy':3200,'tiki_subsidy':3900},'discount_coupon':{'seller_discount':0,'platform_discount':0,'total_discount':0},'discount_tikixu':{'amount':0}}}},{'id':213040708,'product':{'id':90126254,'type':'simple','super_id':0,'master_id':90126253,'sku':'9138454293241','name':'Gieo Hạt Giống Thương Yêu - Mẹ Yêu Con Mãi Mãi (Sách Cho Bé Từ 0-6 tuổi)','catalog_group_name':'Sách Tiếng Việt','inventory_type':'backorder','imeis':[],'serial_numbers':[],'thumbnail':'https://salt.tikicdn.com/cache/280x280/ts/product/79/c6/c1/4c18e0f77d6fd6d8fa354042683600b4.jpg','seller_product_code':'','seller_supply_method':null},'seller':{'id':145228,'name':'Play With Me'},'confirmation':{'status':'waiting','confirmed_at':null,'available_confirm_sla':'2021-10-27 08:13:00','pickup_confirm_sla':null,'histories':[]},'parent_item_id':0,'price':20000,'qty':1,'fulfilled_at':null,'is_virtual':false,'is_ebook':false,'is_bookcare':false,'is_free_gift':false,'is_fulfilled':false,'backend_id':204657439,'applied_rule_ids':[2433424],'invoice':{'price':20000,'quantity':1,'subtotal':20000,'row_total':20000,'discount_amount':0,'discount_tikixu':0,'discount_promotion':0,'discount_percent':0,'discount_coupon':0,'discount_other':0,'discount_tikier':0,'discount_tiki_first':0,'discount_data':{},'is_seller_discount_coupon':false,'is_taxable':false,'fob_price':0,'seller_fee':0,'seller_income':20000,'fees':[]},'inventory_requisition':null,'inventory_withdrawals':[],'seller_inventory_id':null,'seller_inventory_name':null,'seller_income_detail':{'item_price':20000,'item_qty':1,'shipping_fee':5200,'seller_fees':[],'sub_total':23600,'seller_income':23600,'discount':{'discount_shipping_fee':{'sellerDiscount':3500,'fee_amount':5200,'qty':1,'apply_discount':[{'rule_id':2433424,'type':'seller','amount':3500,'seller_sponsor':1600,'tiki_sponsor':1900}],'seller_subsidy':1600,'tiki_subsidy':1900},'discount_coupon':{'seller_discount':0,'platform_discount':0,'total_discount':0},'discount_tikixu':{'amount':0}}}},{'id':213040709,'product':{'id':87015479,'type':'simple','super_id':0,'master_id':19699938,'sku':'5396875429045','name':'Vũ Điệu Của Muôn Loài - Chú Hươu Không Biết Nhảy','catalog_group_name':'Sách Tiếng Việt','inventory_type':'backorder','imeis':[],'serial_numbers':[],'thumbnail':'https://salt.tikicdn.com/cache/280x280/ts/product/0e/81/02/43730750f93139d477725b13eee31480.jpg','seller_product_code':null,'seller_supply_method':null},'seller':{'id':145228,'name':'Play With Me'},'confirmation':{'status':'waiting','confirmed_at':null,'available_confirm_sla':'2021-10-27 08:13:00','pickup_confirm_sla':null,'histories':[]},'parent_item_id':0,'price':32000,'qty':1,'fulfilled_at':null,'is_virtual':false,'is_ebook':false,'is_bookcare':false,'is_free_gift':false,'is_fulfilled':false,'backend_id':204657440,'applied_rule_ids':[2433424],'invoice':{'price':32000,'quantity':1,'subtotal':32000,'row_total':32000,'discount_amount':0,'discount_tikixu':0,'discount_promotion':0,'discount_percent':0,'discount_coupon':0,'discount_other':0,'discount_tikier':0,'discount_tiki_first':0,'discount_data':{},'is_seller_discount_coupon':false,'is_taxable':false,'fob_price':0,'seller_fee':0,'seller_income':32000,'fees':[]},'inventory_requisition':null,'inventory_withdrawals':[],'seller_inventory_id':null,'seller_inventory_name':null,'seller_income_detail':{'item_price':32000,'item_qty':1,'shipping_fee':7800,'seller_fees':[],'sub_total':37400,'seller_income':37400,'discount':{'discount_shipping_fee':{'sellerDiscount':5300,'fee_amount':7800,'qty':1,'apply_discount':[{'rule_id':2433424,'type':'seller','amount':5300,'seller_sponsor':2400,'tiki_sponsor':2900}],'seller_subsidy':2400,'tiki_subsidy':2900},'discount_coupon':{'seller_discount':0,'platform_discount':0,'total_discount':0},'discount_tikixu':{'amount':0}}}},{'id':213040710,'product':{'id':110997366,'type':'simple','super_id':0,'master_id':2612569,'sku':'5550568422014','name':'10 Vạn Câu Hỏi Vì Sao - Khám Phá Thế Giới Đại Dương 1 (Tái Bản)','catalog_group_name':'Sách Tiếng Việt','inventory_type':'backorder','imeis':[],'serial_numbers':[],'thumbnail':'https://salt.tikicdn.com/cache/280x280/ts/product/b6/d2/a5/a77a4d0236899cbf06365f5a5ccb1e4b.jpg','seller_product_code':null,'seller_supply_method':null},'seller':{'id':145228,'name':'Play With Me'},'confirmation':{'status':'waiting','confirmed_at':null,'available_confirm_sla':'2021-10-27 08:13:00','pickup_confirm_sla':null,'histories':[]},'parent_item_id':0,'price':44000,'qty':1,'fulfilled_at':null,'is_virtual':false,'is_ebook':false,'is_bookcare':false,'is_free_gift':false,'is_fulfilled':false,'backend_id':204657441,'applied_rule_ids':[2433424],'invoice':{'price':44000,'quantity':1,'subtotal':44000,'row_total':44000,'discount_amount':0,'discount_tikixu':0,'discount_promotion':0,'discount_percent':20,'discount_coupon':0,'discount_other':0,'discount_tikier':0,'discount_tiki_first':0,'discount_data':{},'is_seller_discount_coupon':false,'is_taxable':false,'fob_price':0,'seller_fee':0,'seller_income':44000,'fees':[]},'inventory_requisition':null,'inventory_withdrawals':[],'seller_inventory_id':null,'seller_inventory_name':null,'seller_income_detail':{'item_price':44000,'item_qty':1,'shipping_fee':5200,'seller_fees':[],'sub_total':47600,'seller_income':47600,'discount':{'discount_shipping_fee':{'sellerDiscount':3500,'fee_amount':5200,'qty':1,'apply_discount':[{'rule_id':2433424,'type':'seller','amount':3500,'seller_sponsor':1600,'tiki_sponsor':1900}],'seller_subsidy':1600,'tiki_subsidy':1900},'discount_coupon':{'seller_discount':0,'platform_discount':0,'total_discount':0},'discount_tikixu':{'amount':0}}}},{'id':213040711,'product':{'id':90852875,'type':'simple','super_id':0,'master_id':2633657,'sku':'6796835348120','name':'Dạy Trẻ Không Cáu Giận 1 - Thỏ Con Nhõng Nhẽo (Tái Bản 2018)','catalog_group_name':'Sách Tiếng Việt','inventory_type':'backorder','imeis':[],'serial_numbers':[],'thumbnail':'https://salt.tikicdn.com/cache/280x280/ts/product/55/c0/91/b719c9431791acb371cd166ffeff5ba0.jpg','seller_product_code':null,'seller_supply_method':null},'seller':{'id':145228,'name':'Play With Me'},'confirmation':{'status':'waiting','confirmed_at':null,'available_confirm_sla':'2021-10-27 08:13:00','pickup_confirm_sla':null,'histories':[]},'parent_item_id':0,'price':35000,'qty':1,'fulfilled_at':null,'is_virtual':false,'is_ebook':false,'is_bookcare':false,'is_free_gift':false,'is_fulfilled':false,'backend_id':204657442,'applied_rule_ids':[2433424],'invoice':{'price':35000,'quantity':1,'subtotal':35000,'row_total':35000,'discount_amount':0,'discount_tikixu':0,'discount_promotion':0,'discount_percent':0,'discount_coupon':0,'discount_other':0,'discount_tikier':0,'discount_tiki_first':0,'discount_data':{},'is_seller_discount_coupon':false,'is_taxable':false,'fob_price':0,'seller_fee':0,'seller_income':35000,'fees':[]},'inventory_requisition':null,'inventory_withdrawals':[],'seller_inventory_id':null,'seller_inventory_name':null,'seller_income_detail':{'item_price':35000,'item_qty':1,'shipping_fee':5200,'seller_fees':[],'sub_total':38700,'seller_income':38700,'discount':{'discount_shipping_fee':{'sellerDiscount':3700,'fee_amount':5200,'qty':1,'apply_discount':[{'rule_id':2433424,'type':'seller','amount':3700,'seller_sponsor':1500,'tiki_sponsor':2200}],'seller_subsidy':1500,'tiki_subsidy':2200},'discount_coupon':{'seller_discount':0,'platform_discount':0,'total_discount':0},'discount_tikixu':{'amount':0}}}},{'id':213040712,'product':{'id':87105349,'type':'simple','super_id':0,'master_id':87105348,'sku':'4185033947142','name':'Truyện Cổ Tích Lừng Danh Thế Giới - Pinocchio Chú Bé Người Gỗ','catalog_group_name':'Sách Tiếng Việt','inventory_type':'backorder','imeis':[],'serial_numbers':[],'thumbnail':'https://salt.tikicdn.com/cache/280x280/ts/product/71/09/99/c9bfb1492e8f42a3baa1bdb1141cb298.jpg','seller_product_code':'','seller_supply_method':null},'seller':{'id':145228,'name':'Play With Me'},'confirmation':{'status':'waiting','confirmed_at':null,'available_confirm_sla':'2021-10-27 08:13:00','pickup_confirm_sla':null,'histories':[]},'parent_item_id':0,'price':20000,'qty':1,'fulfilled_at':null,'is_virtual':false,'is_ebook':false,'is_bookcare':false,'is_free_gift':false,'is_fulfilled':false,'backend_id':204657443,'applied_rule_ids':[2433424],'invoice':{'price':20000,'quantity':1,'subtotal':20000,'row_total':20000,'discount_amount':0,'discount_tikixu':0,'discount_promotion':0,'discount_percent':0,'discount_coupon':0,'discount_other':0,'discount_tikier':0,'discount_tiki_first':0,'discount_data':{},'is_seller_discount_coupon':false,'is_taxable':false,'fob_price':0,'seller_fee':0,'seller_income':20000,'fees':[]},'inventory_requisition':null,'inventory_withdrawals':[],'seller_inventory_id':null,'seller_inventory_name':null,'seller_income_detail':{'item_price':20000,'item_qty':1,'shipping_fee':5200,'seller_fees':[],'sub_total':23600,'seller_income':23600,'discount':{'discount_shipping_fee':{'sellerDiscount':3500,'fee_amount':5200,'qty':1,'apply_discount':[{'rule_id':2433424,'type':'seller','amount':3500,'seller_sponsor':1600,'tiki_sponsor':1900}],'seller_subsidy':1600,'tiki_subsidy':1900},'discount_coupon':{'seller_discount':0,'platform_discount':0,'total_discount':0},'discount_tikixu':{'amount':0}}}},{'id':213040713,'product':{'id':87379316,'type':'simple','super_id':0,'master_id':12107533,'sku':'1235439682420','name':'Khoa Học Thật Là Vui - Bí Ẩn Của Bầu Trời','catalog_group_name':'Sách Tiếng Việt','inventory_type':'backorder','imeis':[],'serial_numbers':[],'thumbnail':'https://salt.tikicdn.com/cache/280x280/ts/product/da/c1/9c/cdda7c27f0af7e5053557741e8ea2046.jpg','seller_product_code':null,'seller_supply_method':null},'seller':{'id':145228,'name':'Play With Me'},'confirmation':{'status':'waiting','confirmed_at':null,'available_confirm_sla':'2021-10-27 08:13:00','pickup_confirm_sla':null,'histories':[]},'parent_item_id':0,'price':25000,'qty':1,'fulfilled_at':null,'is_virtual':false,'is_ebook':false,'is_bookcare':false,'is_free_gift':false,'is_fulfilled':false,'backend_id':204657444,'applied_rule_ids':[2433424],'invoice':{'price':25000,'quantity':1,'subtotal':25000,'row_total':25000,'discount_amount':0,'discount_tikixu':0,'discount_promotion':0,'discount_percent':0,'discount_coupon':0,'discount_other':0,'discount_tikier':0,'discount_tiki_first':0,'discount_data':{},'is_seller_discount_coupon':false,'is_taxable':false,'fob_price':0,'seller_fee':0,'seller_income':25000,'fees':[]},'inventory_requisition':null,'inventory_withdrawals':[],'seller_inventory_id':null,'seller_inventory_name':null,'seller_income_detail':{'item_price':25000,'item_qty':1,'shipping_fee':5000,'seller_fees':[],'sub_total':28500,'seller_income':28500,'discount':{'discount_shipping_fee':{'sellerDiscount':3400,'fee_amount':5000,'qty':1,'apply_discount':[{'rule_id':2433424,'type':'seller','amount':3400,'seller_sponsor':1500,'tiki_sponsor':1900}],'seller_subsidy':1500,'tiki_subsidy':1900},'discount_coupon':{'seller_discount':0,'platform_discount':0,'total_discount':0},'discount_tikixu':{'amount':0}}}}],'status_histories':[],'is_virtual':false,'siblings':[{'code':'842386681'},{'code':'868042904'}],'tikixu_point_earning':0,'is_flower_gift':false,'dropship_already':false,'created_at':'2021-10-26 13:06:24','shipment_status_histories':[],'billing_address':{'full_name':'Nguyễn Thị Thu Hương','street':'Số nhà 139 - Ấp Lộc Tân','ward':'Xã Lộc Ninh','ward_tiki_code':'VN063003006','district':'Huyện Dương Minh Châu','district_tiki_code':'VN063003','region':'Tây Ninh','region_tiki_code':'VN063','country':'Việt Nam','country_id':'VN'},'main_substate_text_en':'Order verified','type':'simple','is_parent':false,'state_histories':[],'inventory_status':'backorder','platform':'frontend-desktop','linked_code':'','has_backorder_items':true,'multiseller_confirmation':{'seller_id':145228,'need_other_sellers_confirm':false},'original_code':'633356038','updated_at':'2021-10-26 13:12:15','shipping':{'partner_id':null,'partner_name':null,'tracking_code':null,'status':null,'pickup_shipping_code':null,'pickup_partner_code':null,'return_shipping_code':null,'return_partner_code':null,'delivery_shipping_code':null,'delivery_partner_code':null,'plan':{'id':1,'name':'TikiFAST Giao Tiết Kiệm','is_free_shipping':false,'promised_delivery_date':'2021-11-11 23:59:59','description':'Giao vào Thứ năm, 11/11'},'address':{'full_name':'Nguyễn Thị Thu Hương','street':'Số nhà 139 - Ấp Lộc Tân','ward':'Xã Lộc Ninh','ward_tiki_code':'VN063003006','district':'Huyện Dương Minh Châu','district_tiki_code':'VN063003','region':'Tây Ninh','region_tiki_code':'VN063','country':'Việt Nam','country_id':'VN','email':null,'phone':''},'shipping_detail':null},'children':[],'shipment_mappings':[],'backend_id':114778080,'main_substate':'order_verified','parent_code':'633356038','payment':{'method':'cod','is_prepaid':false,'status':'success','description':'Thanh toán tiền mặt khi nhận hàng'},'is_bookcare':false,'relation_code':'','is_rma':false,'delivery':{'delivery_confirmed':false,'delivery_confirmed_at':null,'delivery_confirmed_by_customer':false,'delivery_confirmed_by_customer_at':null,'delivery_note':null,'delivery_confirmation':[]},'main_state':'awaiting_confirmation','tiki_warehouse':{'id':17,'name':'Ha Noi 4','code':'hn4'},'labels':[],'applied_rule_ids':[2433424],'substate':'processing','is_vat_exporting':false,'main_state_text':'Chờ xác nhận','main_substate_text':'Tiki đã tiếp nhận đơn hàng','invoice':{'items_count':7,'items_quantity':7,'subtotal':268800,'grand_total':282800,'collectible_amount':282800,'discount_amount':0,'discount_tikixu':0,'discount_promotion':0,'discount_percent':40,'discount_coupon':0,'discount_other':0,'gift_card_amount':0,'gift_card_code':null,'coupon_code':null,'shipping_amount_after_discount':14000,'shipping_discount_amount':30000,'handling_fee':0,'other_fee':0,'total_seller_fee':0,'total_seller_income':268800,'purchased_at':'2021-10-26 13:06:24','tax_info':null},'main_state_text_en':'Awaiting Confirmation','customer':{'id':1552906,'full_name':'Bich Thao Nguyen'}}],'paging':{'total':1,'current_page':1,'from':1,'to':1,'per_page':20,'last_page':1}}";
            //string json = @"{'total':1,'current_page':1,'from':1,'to':1,'per_page':20,'last_page':1}";

            //JsonSerializerSettings settings = new JsonSerializerSettings
            //{
            //    NullValueHandling = NullValueHandling.Ignore,
            //    MissingMemberHandling = MissingMemberHandling.Ignore
            //};
            //PagingOrder pOrder = JsonConvert.DeserializeObject<PagingOrder>(json, settings);
            ////PagingProduct pProduct = JsonConvert.DeserializeObject<PagingProduct>(json, settings);

        }
    }
}
