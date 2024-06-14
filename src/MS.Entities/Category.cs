using MS.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class Category : IEntity
    {
        public long id { get; set; }
        public int sort { get; set; }   // 顺序
        public int status { get; set; }     // 分类状态 0:禁用，1:启用
        public string name { get; set; }   // 分类名称
        public int type { get; set; }  // 类型:   1 菜品分类 2 套餐分类
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
        public long create_user { get; set; }
        public long update_user { get; set; }
    }
}
