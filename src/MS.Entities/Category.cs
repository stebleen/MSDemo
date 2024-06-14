using MS.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class Category : IEntity
    {
        public long Id { get; set; }
        public int Sort { get; set; }   // 顺序
        public int Status { get; set; }     // 分类状态 0:禁用，1:启用
        public string Name { get; set; }   // 分类名称
        public int Type { get; set; }  // 类型:   1 菜品分类 2 套餐分类
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public long CreateUser { get; set; }
        public long UpdateUser { get; set; }
    }
}
