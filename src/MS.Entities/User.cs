using MS.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class User : IEntity
    {
        public long Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string OpenId { get; set; }  // 微信用户唯一标识
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Sex { get; set; }
        public string IdNumber { get; set; }   // 身份证号
        public string Avatar { get; set; }  // 头像
    }
}
