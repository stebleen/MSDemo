using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class Employee
    {
        public long id { get; set; }
        public string name { get; set; }    // 姓名
        public string username { get; set; }    // 用户名
        public string password { get; set; }
        public string phone { get; set; }
        public string sex { get; set; }
        public string id_number { get; set; }   // 身份证号
        public int status { get; set; } // 状态 0:禁用，1:启用
        public long address_id { get; set; }    // 所负责配送的楼宇地址
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
        public long create_user { get; set; }
        public long update_user { get; set; }

    }
}
