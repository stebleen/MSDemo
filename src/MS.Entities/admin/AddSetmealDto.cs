using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities.admin
{
    public class AddSetmealDto
    {
        public long Id { get; set; }
        public long CategoryId { get; set; }   // 菜品分类id
        public string Name { get; set; }    // 套餐名称
        public string Price { get; set; }
        public int Status { get; set; }  // 售卖状态 0:停售 1:起售
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public long CreateUser { get; set; }
        public long UpdateUser { get; set; }

        public List<SetmealDish> setmealDishes { get; set; }
    }
}
