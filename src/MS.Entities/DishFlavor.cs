using MS.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class DishFlavor : IEntity
    {
        // 菜品口味关系表
        public long Id { get; set; }
        public long DishId { get; set; }
        public string Name { get; set; }    // 口味名称
        public string Value { get; set; }   // 口味数据list
    }
}
