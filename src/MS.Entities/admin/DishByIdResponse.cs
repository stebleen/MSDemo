using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities.admin
{
    public class DishByIdResponse
    {
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public List<DishFlavor> Flavors { get; set; }
        public long Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
        public string UpdateTime { get; set; }
    }

}
