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
        private readonly long _userId;

        #endregion

        /// <summary>
        /// 初始化消息发送器
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="reason">审核失败原因</param>
        public RealNameCertificateFailureMessageSender(long userId, string reason): base(MessageTemplateOption.RealNameCertificateFailure)
        {
            _userId = userId;

            base.ContentFactory(new { Reason = reason});
        }

        /// <summary>
        /// 发送系统消息给用户
        /// </summary>
        /// <returns></returns>
        public override bool Send()
        {
            return new MessageService().AddWorkerMessage(_userId, Option, _userId.ToString(), Title, Content, "");
        }
    }
}
