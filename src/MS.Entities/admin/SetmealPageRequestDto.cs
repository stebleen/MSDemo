using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class SetmealPageRequestDto
    {
        public string CategoryId { get; set; }
        public string Name { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Status { get; set; }
    }
}
