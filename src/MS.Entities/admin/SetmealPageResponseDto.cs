using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class SetmealPageResponseDto
    {
        public int Total { get; set; }
        public IEnumerable<SetmealDto> Records { get; set; }
    }

    public class SetmealDto
    {
        public long Id { get; set; }
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string UpdateTime { get; set; }
        public string CategoryName { get; set; } 
    }
}
