using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities.admin
{
    public class AddDishDto
    {
        // 菜品
        public long Id { get; set; }
        public long CategoryId { get; set; }   // 菜品分类id
        public string Name { get; set; }
        public string Price { get; set; }
        public int Status { get; set; } // 0 停售 1 起售    
        public string Image { get; set; }    // 图片   
        public string Description { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public long CreateUser { get; set; }
        public long UpdateUser { get; set; }

        public List<DishFlavor> Flavors { get; set; }
    }
}
