using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities.admin
{
    public class OrderSearchResponseDto
    {
        public int Total { get; set; }
        public IEnumerable<OrderDto> Records { get; set; }
    }

    public class OrderDto
    {
        public long Id { get; set; }    // 订单id
        public string Number { get; set; }  // 订单号
        public int Status { get; set; } // 订单状态 1待付款 2待接单 3已接单 4派送中 5已完成 6已取消 7退款
        public long UserId { get; set; }   // 下单用户
        public long AddressBookId { get; set; }   // 地址id
        public string OrderTime { get; set; }    // 下单时间
        public string CheckoutTime { get; set; } //结账时间
        public int PayMethod { get; set; } // 支付方式 1微信,2支付宝
        public int PayStatus { get; set; } // 支付状态 0未支付 1已支付 2退款
        public decimal Amount { get; set; } // 实收金额
        public string Remark { get; set; }  // 备注
        public string Phone { get; set; }   // 手机号
        public string Address { get; set; } // 地址
        public string UserName { get; set; }   // 用户名称
        public string Consignee { get; set; }   // 收货人
        public string CancelReason { get; set; }   // 订单取消原因
        public string RejectionReason { get; set; }    // 订单拒绝原因
        public string CancelTime { get; set; }   // 订单取消时间
        public string EstimatedDeliveryTime { get; set; }   // 预计送达时间
        public int DeliveryStatus { get; set; }    // 配送状态  1立即送出  0选择具体时间
        public string DeliveryTime { get; set; }  // 送达时间
        public int PackAmount { get; set; }    // 打包费
        public int TablewareNumber { get; set; }   // 餐具数量
        public int TablewareStatus { get; set; }   // 餐具数量状态  1按餐量提供  0选择具体数量
        public string OrderDishes { get; set; } // 瘦肉粥*1;皮蛋瘦肉粥*1 格式
    }
}
