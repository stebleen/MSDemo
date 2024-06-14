using MS.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class Address : IEntity
    {
        public long id { get; set; }
        public string campus_code { get; set; }  
        public string campus_name { get; set; }
        public string building_code { get; set; }
        public string building_name { get; set; }

    }
}
