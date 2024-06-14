using MS.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class DishFlavor : IEntity
    {
        // 菜品口味关系表
        public long id { get; set; }
        public long dish_id { get; set; }
        public string name { get; set; }    // 口味名称
        public string value { get; set; }   // 口味数据list
    }
}
