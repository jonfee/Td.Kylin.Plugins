using System;
using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Services;

namespace Td.Kylin.Message.Sender
{
    /// <summary>
    /// 商家确定服务订单后消息发送器
    /// </summary>
    public class MerchantConfirmServiceOrderMessageSender : SysMessageSender
    {
        #region 属性

        /// <summary>
        /// 用户ID
        /// </summary>
        private readonly long _userID;

        /// <summary>
        /// 订单ID
        /// </summary>
        private readonly long _orderID;

        #endregion

        /// <summary>
        /// 初始化消息发送器
        /// </summary>
        /// <param name="orderId">订单ID</param>
        public MerchantConfirmServiceOrderMessageSender(int orderId) : base(MessageTemplateOption.MerchantSureServiceOrder)
        {
            _orderID = orderId;

            var order = new MerchantOrderService().GetGoodsOrderInfo(orderId);

            if (order == null) throw new InvalidOperationException("订单信息不存在，无法继续发送消息");

            _userID = order.UserID;

            var merchant = new MerchantService().GetMerchantInfo(order.MerchantID);

            if (merchant == null) throw new InvalidOperationException("商家信息不存在，无法继续发送消息");

            base.ContentFactory(new { OrderCode = order.OrderCode, MerchantName = merchant.Name, MerchantPhone = merchant.Phone });
        }

        /// <summary>
        /// 发送系统消息给用户
        /// </summary>
        /// <returns></returns>
        public override bool Send()
        {
            return new MessageService().AddUserMessage(_userID, Option, _orderID.ToString(), Title, Content, "");
        }
    }
}
