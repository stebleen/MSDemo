using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities.admin
{
    public class CategoryPageResponseDto
    {
        public int Total { get; set; }
        public IEnumerable<CategoryDto> Records { get; set; }
    }

    public class CategoryDto
    {
        public long Id { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public int Status { get; set; }
        public string CreateTime { get; set; }
        public string UpdateTime { get; set; }
        public long CreateUser { get; set; }
        public long UpdateUser { get; set; }
    }
}
