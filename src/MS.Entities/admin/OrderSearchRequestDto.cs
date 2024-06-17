using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities.admin
{
    public class OrderSearchRequestDto
    {
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public string Number { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
    }
}
