using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities.admin
{
    public class SetmealDishDto
    {
        public long Id { get; set; }
        public long SetmealId { get; set; }    // 套餐id
        public long DishId { get; set; }   // 菜品id
        public string Name { get; set; }    // 菜品名称 （冗余字段）
        public string Price { get; set; }  // 菜品单价（冗余字段）
        public int Copies { get; set; } // 菜品份数
    }
}
