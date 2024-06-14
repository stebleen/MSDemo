using MS.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class Dish : IEntity
    {
        // 菜品
        public long id { get; set; }
        public long category_id { get; set; }   // 菜品分类id
        public string name { get; set; }   
        public decimal price { get; set; }  
        public int status { get; set; } // 0 停售 1 起售    
        public string image{ get; set; }    // 图片   
        public string description { get; set; }  
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
        public long create_user { get; set; }
        public long update_user { get; set; }
    }
}
