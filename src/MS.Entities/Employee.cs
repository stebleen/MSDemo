using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class Employee
    {
        public long Id { get; set; }
        public string Name { get; set; }    // 姓名
        public string Username { get; set; }    // 用户名
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Sex { get; set; }
        public string IdNumber { get; set; }   // 身份证号
        public int Status { get; set; } // 状态 0:禁用，1:启用
        public long AddressId { get; set; }    // 所负责配送的楼宇地址
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public long CreateUser { get; set; }
        public long UpdateUser { get; set; }

    }
}
