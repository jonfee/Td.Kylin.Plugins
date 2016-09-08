using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Services;

namespace Td.Kylin.Message.Sender
{
    /// <summary>
    /// 跑腿业务开通审核成功消息发送器
    /// </summary>
    public class LegworkBusinessApplySuccessfulMessageSender : BaseSender
    {
        #region 属性

        /// <summary>
        /// 用户ID
        /// </summary>
        private readonly long _userId;

        #endregion

        /// <summary>
        /// 初始化消息发送器实例
        /// </summary>
        /// <param name="userId"></param>
        public LegworkBusinessApplySuccessfulMessageSender(long userId) : base(MessageTemplateOption.WorkerLegworkBusinessApplySuccessful)
        {
            _userId = userId;
        }

        public override bool Send()
        {
            return new MessageService().AddWorkerMessage(_userId, Option, _userId.ToString(), Title, Content, "");
        }
    }
}
