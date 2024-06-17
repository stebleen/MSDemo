using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities.admin
{
    public class SetmealVO
    {
        // 根据id查询套餐详情
        public long CategoryId { get; set; }
        public string CategoryName { get; set; } // 需要与Category表联查获取
        public string Description { get; set; }
        public long Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<SetmealDishVo> SetmealDishes { get; set; }
        public int Status { get; set; }
        public string UpdateTime { get; set; } // 格式化为字符串
    }

    public class SetmealDishVo
    {
      
        public int Copies { get; set; }
        public long DishId { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public long SetmealId { get; set; }
    }
}
