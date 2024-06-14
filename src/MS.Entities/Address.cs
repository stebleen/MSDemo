using MS.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class Address : IEntity
    {
        public long Id { get; set; }
        public string CampusCode { get; set; }  
        public string CampusName { get; set; }
        public string BuildingCode { get; set; }
        public string BuildingName { get; set; }

    }
}
