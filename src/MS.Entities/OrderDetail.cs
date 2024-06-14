using MS.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class OrderDetail : IEntity
    {
        // 订单明细表
        public long Id { get; set; }
        public long OrderId { get; set;}   // 订单id
        public long DishId { get; set; }   // 菜品id
        public long SetmealId { get; set; }    // 套餐id
        public string Name { get; set; }    // 菜品或者套餐名字
        public string Image { get; set; }   // 菜品或者套餐名字
        public decimal Amount { get; set; } // 金额
        public int Number { get; set; } // 数量
        public string DishFlavor { get; set; } // 口味


    }
}
