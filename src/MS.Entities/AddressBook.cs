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
        public long id { get; set; }
        public long user_id { get; set; }
        public long address_id { get; set; }
        public string consignee { get; set; }   // 收货人
        public string sex { get; set; }
        public string phone { get; set; }
        public string domitory { get; set; }    // 宿舍号
        public int is_default { get; set; }    // 默认 0 否 1是

    }
}
