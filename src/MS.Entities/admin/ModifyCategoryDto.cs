using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities.admin
{
    public class ModifyCategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public int Type { get; set; }
    }
}
