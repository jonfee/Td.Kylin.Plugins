using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.ComponentModel;

namespace Td.Kylin.Push
{
    public enum PushType
    {
        /// <summary>
        /// 用户下单推送给工作端
        /// </summary>
        [Description("LegworkUserAddOrder")]
        LegworkUserAddOrder,
        /// <summary>
        /// 工作端报价，推送给用户端
        /// </summary>
        [Description("LegworkOffer")]
        LegworkOffer,
        /// <summary>
        /// 用户确认订单,推送给工作端
        /// </summary>
        [Description("LegworkUserConfirmOrder")]
        LegworkUserConfirmOrder,
        /// <summary>
        /// 工作端确认送达(取送物品)及工作端选择线下支付时(购买物品),推送给用户端
        /// </summary>
        [Description("LegworkConfirmDelivery")]
        LegworkConfirmDelivery,
        /// <summary>
        /// 工作端选择线上支付,推送给用户端
        /// </summary>
        [Description("LegworkDownPay")]
        LegworkDownPay,
        /// <summary>
        /// 用户端线上支付成功,推送给工作端
        /// </summary>
        [Description("LegworkUserTopPay")]
        LegworkUserTopPay,
        /// <summary>
        /// 提醒购买
        /// </summary>
        [Description("LegworkMessageBuy")]
        LegworkMessageBuy,
    }
}
