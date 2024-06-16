using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class OrderResponseDto
    {
        public long Id { get; set; }
        public decimal OrderAmount { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderTime { get; set; }
    }
}
