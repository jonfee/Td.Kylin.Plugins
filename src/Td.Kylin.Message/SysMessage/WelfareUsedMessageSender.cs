using System;
using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Services;

namespace Td.Kylin.Message.SysMessage
{
    /// <summary>
    /// 福利使用后消息发送器
    /// </summary>
    public class WelfareUsedMessageSender : SysMessageSender
    {
        #region 属性

        /// <summary>
        /// 用户ID
        /// </summary>
        private readonly long _userID;

        /// <summary>
        /// 福利消费码
        /// </summary>
        private readonly long _consumerCode;

        #endregion

        /// <summary>
        /// 初始化消息发送器
        /// </summary>
        /// <param name="consumerCode">福利消费码</param>
        public WelfareUsedMessageSender(long consumerCode)
            : base(MessageTemplateOption.WelfareUsed)
        {
            _consumerCode = consumerCode;

            var usedInfo = new WelfareService().GetWelfareUsedInfo(consumerCode);

            if (usedInfo == null) throw new InvalidOperationException("福利信息不存在，无法继续发送消息");

            _userID = usedInfo.UserID;

            var useTime = (usedInfo.UseTime ?? DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss");

            var merchant = new MerchantService().GetMerchantInfo(usedInfo.MerchantID);

            if (merchant == null) throw new InvalidOperationException("商家信息不存在，无法继续发送消息");

            base.ContentFactory(new { WelfareName = usedInfo.WelfareName, DateTime = useTime, MerchantName = merchant.Name, MerchantPhone = merchant.Phone });
        }

        /// <summary>
        /// 发送系统消息给商家
        /// </summary>
        /// <returns></returns>
        public override bool Send()
        {
            return new MessageService().AddUserMessage(_userID, Option, _consumerCode.ToString(), Title, Content, "");
        }
    }
}
