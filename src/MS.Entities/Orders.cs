using MS.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class Orders : IEntity
    {
        public long id { get; set; }    // 订单id
        public string number { get; set; }  // 订单号
        public int status { get; set; } // 订单状态 1待付款 2待接单 3已接单 4派送中 5已完成 6已取消 7退款
        public long user_id { get; set; }   // 下单用户
        public long address_book_id { get; set; }   // 地址id
        public DateTime order_time { get; set; }    // 下单时间
        public DateTime checkout_time { get; set; } //结账时间
        public int pay_method { get; set; } // 支付方式 1微信,2支付宝
        public int pay_status { get; set; } // 支付状态 0未支付 1已支付 2退款
        public decimal amount { get; set; } // 实收金额
        public string remark { get; set; }  // 备注
        public string phone { get; set; }   // 手机号
        public string address { get; set; } // 地址
        public string user_name { get; set; }   // 用户名称
        public string consignee { get; set; }   // 收货人
        public string cancel_reason { get; set; }   // 订单取消原因
        public string rejection_reason { get; set; }    // 订单拒绝原因
        public DateTime cancel_time { get; set; }   // 订单取消时间
        public DateTime estimated_delivery_time { get; set; }   // 预计送达时间
        public int delivery_status { get; set; }    // 配送状态  1立即送出  0选择具体时间
        public DateTime delivery_time { get;set; }  // 送达时间
        public int pack_amount { get; set; }    // 打包费
        public int tableware_number { get; set; }   // 餐具数量
        public int tableware_status { get; set; }   // 餐具数量状态  1按餐量提供  0选择具体数量
    }
}
