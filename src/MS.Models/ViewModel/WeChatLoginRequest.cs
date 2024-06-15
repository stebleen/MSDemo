using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Models.ViewModel
{
    public class WeChatLoginRequest
    {
        // 微信登录时客户端获取的临时登录凭证
        public string Code { get; set; }
    }
}
