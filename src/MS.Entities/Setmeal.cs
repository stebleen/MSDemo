using MS.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class Setmeal : IEntity
    {
        // 套餐
        public long id { get; set; }
        public long category_id { get; set; }   // 菜品分类id
        public string name { get; set; }    // 套餐名称
        public decimal price { get; set; }
        public int staue { get; set; }  // 售卖状态 0:停售 1:起售
        public string description { get; set; }
        public string image { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
        public long create_user { get; set; }
        public long update_user { get; set; }

    }
}
