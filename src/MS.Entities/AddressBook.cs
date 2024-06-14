using MS.Entities.Core;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MS.Entities
{
    public class AddressBook : IEntity
    {
        // 用户地址关系表
        public long Id { get; set; }
        public long UserId { get; set; }
        public long AddressId { get; set; }
        public string Consignee { get; set; }   // 收货人
        public string Sex { get; set; }
        public string Phone { get; set; }
        public string Domitory { get; set; }    // 宿舍号
        public int IsDefault { get; set; }    // 默认 0 否 1是

    }
}
