using MS.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class ShoppingCart : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }    // 商品名称
        public string Image { get; set; }   // 图片
        public long UserId { get; set; }
        public long DishId { get; set; }   // 菜品id
        public long SetmealId { get; set; }    // 套餐id
        public string DishFlavor { get; set; } // 口味
        public int Number { get; set; } // 数量
        public decimal Amount { get; set; } // 金额
        public DateTime CreateTime { get; set; }
    }
}
