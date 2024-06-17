using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities.admin
{
    public class OrderStatisticsVO
    {
        public int Confirmed { get; set; }
        public int DeliveryInProgress { get; set; }
        public int ToBeConfirmed { get; set; }
    }
}
