using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel.Internal.Infrastructure;

namespace Td.Kylin.Push.Model
{

    /// <summary>
    /// 用户线上支付-请求用户端支付推送内容。
    /// 工作端选择线上支付,推送给用户端
    /// </summary>
    public class RequestPaymentPushContent : PublicPushContent
    {

        /// <summary>
        /// 商品费用。
        /// </summary>
        public decimal GoodsAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 服务费用。
        /// </summary>
        public decimal ServiceCharge
        {
            get;
            set;
        }

        /// <summary>
        /// 实际总额。
        /// </summary>
        public decimal ActualAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 订单状态。
        /// </summary>
        public short OrderStatus
        {
            get;
            set;
        }
    }
}
