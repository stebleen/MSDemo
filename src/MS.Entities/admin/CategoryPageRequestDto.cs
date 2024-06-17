using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class CategoryPageRequestDto
    {
        public string Name { get; set; }
        public string Page { get; set; }
        public string PageSize { get; set; }
        public string Type { get; set; }
    }
}
