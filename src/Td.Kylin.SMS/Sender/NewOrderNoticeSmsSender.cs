using System;
using System.Threading.Tasks;
using Td.Kylin.EnumLibrary;
using Td.Kylin.SMS.Core;

namespace Td.Kylin.SMS.Sender
{
    /// <summary>
    /// 直营新订单通知消息发送器
    /// </summary>
    public class NewOrderNoticeSmsSender : AreaSender
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        private readonly string _orderCode;

        /// <summary>
        /// 初始化直营新订单通知消息发送器实例
        /// </summary>
        /// <param name="areaId">区域ID</param>
        /// <param name="orderCode">订单编号</param>
        /// <param name="orderTime">下单时间</param>
        public NewOrderNoticeSmsSender(int areaId, string orderCode, DateTime orderTime) : base(areaId, SmsTemplateOption.NewOrderNotice)
        {
            _orderCode = orderCode;

            base.ContentFactory(new { OrderCode = orderCode, OrderTime = orderTime.ToString("yyyy/MM/dd HH:mm") });
        }

        public override async Task<bool> SendAsync()
        {
            //计划发送的目标手机号
            planSendMobiles = CacheData.GetMobiles(areaId, OperatorBusinessNoticeType.OrderDispatch);

            //发送短信
            await Send(_orderCode);

            return true;
        }
    }
}
