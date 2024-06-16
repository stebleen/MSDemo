using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Entities
{
    public class PaymentInfoDto
    {
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; set; }

        /// <summary>
        /// 统一下单接口返回的 prepay_id 参数值
        /// </summary>
        public string PackageStr { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string PaySign { get; set; }

        /// <summary>
        /// 签名算法
        /// </summary>
        public string SignType { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string TimeStamp { get; set; }
    }
}
