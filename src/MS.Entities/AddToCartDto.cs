using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class AddToCartDto
    {
        public string DishFlavor { get; set; }
        public long? DishId { get; set; } // 将此属性标记为可空，表示参数是可选的
        public long? SetmealId { get; set; } // 同上
    }
}
