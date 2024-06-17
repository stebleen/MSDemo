using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities.admin
{
    public class DishByCategoryIdDto
    {
        public long Id { get; set; }
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string CreateTime { get; set; }
        public string UpdateTime { get; set; }
        public long? CreateUser { get; set; }
        public long? UpdateUser { get; set; }
    }
}
