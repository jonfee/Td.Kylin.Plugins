using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Services;

namespace Td.Kylin.Message.Sender
{
    /// <summary>
    /// 用户实名认证失败后消息发送器
    /// </summary>
    public class RealNameCertificateFailureMessageSender : BaseSender
    {
        #region 属性

        /// <summary>
        /// 用户ID
        /// </summary>
        private readonly long _userID;

        #endregion


        /// <summary>
        /// 初始化消息发送器
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="reason">审核失败原因</param>
        /// <param name="serverPhone">客服电话</param>
        public RealNameCertificateFailureMessageSender(long userID, string reason, string serverPhone)
            : base(MessageTemplateOption.RealNameCertificateFailure)
        {
            _userID = userID;

            base.ContentFactory(new { Reason = reason, ServerPhone = serverPhone });
        }

        /// <summary>
        /// 发送系统消息给用户
        /// </summary>
        /// <returns></returns>
        public override bool Send()
        {
            return new MessageService().AddUserMessage(_userID, Option, _userID.ToString(), Title, Content, "");
        }
    }
}
