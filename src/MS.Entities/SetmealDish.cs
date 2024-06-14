using MS.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class SetmealDish : IEntity
    {
        // 套餐菜品关系
        public long Id { get; set; }
        public long SetmealId { get; set; }    // 套餐id
        public long DishId { get; set; }   // 菜品id
        public string Name { get; set; }    // 菜品名称 （冗余字段）
        public decimal Price { get; set; }  // 菜品单价（冗余字段）
        public int Copies { get; set; } // 菜品份数
    }
}
