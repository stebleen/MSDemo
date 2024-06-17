using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities.admin
{
    public class OrderDetailsResponse
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public int Status { get; set; }
        public long UserId { get; set; }
        public long AddressBookId { get; set; }
        public string Consignee { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; } // 地址
        public decimal Amount { get; set; } // 实收金额
        public string Phone { get; set; }
        public int PayMethod { get; set; } // 支付方式 1微信,2支付宝
        public int PayStatus { get; set; } // 支付状态 0未支付 1已支付 2退款
        public string Remark { get; set; }
        public string OrderDishes { get; set; }
        public string OrderTime { get; set; } // 格式化为字符串，以便能正确处理null值
        public string CheckoutTime { get; set; }
        public int PackAmount { get; set; }    // 打包费

        public string CancelTime { get; set; }
        public string CancelReason { get; set; }
        public string RejectionReason { get; set; }
        public string EstimatedDeliveryTime { get; set; }
        public string DeliveryTime { get; set; }
        public int DeliveryStatus { get; set; }    // 配送状态  1立即送出  0选择具体时间

        public int TablewareNumber { get; set; }   // 餐具数量
        public int TablewareStatus { get; set; }   // 餐具数量状态  1按餐量提供  0选择具
        // 订单详情列表
        public List<OrderDetailVO> OrderDetailList { get; set; }
    }

    public class OrderDetailVO
    {
        public long Id { get; set; }
        public long OrderId { get; set; }   // 订单id
        public long? DishId { get; set; }   // 菜品id
        public long? SetmealId { get; set; }    // 套餐id
        public string Name { get; set; }    // 菜品或者套餐名字
        public string Image { get; set; }   // 菜品或者套餐名字
        public decimal Amount { get; set; } // 金额
        public int Number { get; set; } // 数量
        public string DishFlavor { get; set; } // 口味
    }
}
