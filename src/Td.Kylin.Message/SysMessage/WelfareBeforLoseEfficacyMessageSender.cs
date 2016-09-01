using System;
using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Services;

namespace Td.Kylin.Message.SysMessage
{
    /// <summary>
    /// 福利即将过期时提醒消息发送器
    /// </summary>
    public class WelfareBeforLoseEfficacyMessageSender:SysMessageSender
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
        public WelfareBeforLoseEfficacyMessageSender(long consumerCode)
            : base(MessageTemplateOption.WelfareBeforLoseEfficacy)
        {
            _consumerCode = consumerCode;

            var welfare = new WelfareService().GetUserWelfareInfo(consumerCode);

            if (welfare == null) throw new InvalidOperationException("福利信息不存在，无法继续发送消息");

            _userID = welfare.UserID;

            base.ContentFactory(new { WelfareName = welfare.WelfareName});
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
