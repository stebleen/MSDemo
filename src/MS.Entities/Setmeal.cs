using MS.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class Setmeal : IEntity
    {
        // 套餐
        public long Id { get; set; }
        public long CategoryId { get; set; }   // 菜品分类id
        public string Name { get; set; }    // 套餐名称
        public decimal Price { get; set; }
        public int Status { get; set; }  // 售卖状态 0:停售 1:起售
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public long CreateUser { get; set; }
        public long UpdateUser { get; set; }

    }
}
