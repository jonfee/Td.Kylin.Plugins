using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Services;

namespace Td.Kylin.Message.Sender
{
    /// <summary>
    /// 实名认证成功后消息发送器
    /// </summary>
    public class RealNameCertificateSuccessfulMessageSender : BaseSender
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
        public RealNameCertificateSuccessfulMessageSender(long userId): base(MessageTemplateOption.RealNameCertificateSuccessful)
        {
            _userId = userId;
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
