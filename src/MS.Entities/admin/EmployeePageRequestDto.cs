using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities.admin
{
    public class EmployeePageRequestDto
    {
        public string Name { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
