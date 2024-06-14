using MS.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class OrderDetail : IEntity
    {
        // 订单明细表
        public long id { get; set; }
        public long order_id { get; set;}   // 订单id
        public long dish_id { get; set; }   // 菜品id
        public long setmeal_id { get; set; }    // 套餐id
        public string name { get; set; }    // 菜品或者套餐名字
        public string image { get; set; }   // 菜品或者套餐名字
        public decimal amount { get; set; } // 金额
        public int number { get; set; } // 数量
        public string dish_flavor { get; set; } // 口味


    }
}
