using MS.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class ShoppingCart : IEntity
    {
        public long id { get; set; }
        public string name { get; set; }    // 商品名称
        public string image { get; set; }   // 图片
        public long user_id { get; set; }
        public long dish_id { get; set; }   // 菜品id
        public long setmeal_id { get; set; }    // 套餐id
        public string dish_flavor { get; set; } // 口味
        public int number { get; set; } // 数量
        public decimal amount { get; set; } // 金额
        public DateTime create_time { get; set; }
    }
}
