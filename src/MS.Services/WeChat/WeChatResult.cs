using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Services
{
    public class WeChatResult
    {
        public string Openid { get; set; }
        public string Session_key { get; set; }

        public int Errcode { get; set; }
        public string Errmsg { get; set; }
    }
}
