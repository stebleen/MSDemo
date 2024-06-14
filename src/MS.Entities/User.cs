using MS.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class User : IEntity
    {
        public long Id { get; set; }
        public DateTime create_time { get; set; }
        public string openid { get; set; }  // 微信用户唯一标识
        public string name { get; set; }
        public string phone { get; set; }
        public string sex { get; set; }
        public string id_number { get; set; }   // 身份证号
        public string avatar { get; set; }  // 头像
    }
}
