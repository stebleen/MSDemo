using MS.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class SetmealDish : IEntity
    {
        // 套餐菜品关系
        public long id { get; set; }
        public long setmeal_id { get; set; }    // 套餐id
        public long dish_id { get; set; }   // 菜品id
        public string name { get; set; }    // 菜品名称 （冗余字段）
        public decimal price { get; set; }  // 菜品单价（冗余字段）
        public int copies { get; set; } // 菜品份数
    }
}
