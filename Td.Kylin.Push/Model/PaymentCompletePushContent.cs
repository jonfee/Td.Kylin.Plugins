using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Td.Kylin.Push.Model
{
    /// <summary>
    /// 用户支付-用户支付完成推送内容。
    /// 用户端线上支付成功,推送给工作端
    /// </summary>
    public class PaymentCompletePushContent:PublicPushContent
    {
       

        /// <summary>
        /// 下单人昵称。
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// 支付金额。
        /// </summary>
        public decimal Amount
        {
            get;
            set;
        }
      
    }
}
