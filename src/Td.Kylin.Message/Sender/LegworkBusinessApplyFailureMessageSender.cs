﻿using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Services;

namespace Td.Kylin.Message.Sender
{
    /// <summary>
    /// 跑腿业务开通审核失败消息发送器
    /// </summary>
    public class LegworkBusinessApplyFailureMessageSender : BaseSender
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
        public LegworkBusinessApplyFailureMessageSender(long userId, string reason) : base(MessageTemplateOption.WorkerLegworkBusinessApplyFailure)
        {
            _userId = userId;

            base.ContentFactory(new { Reason = reason });
        }

        public override bool Send()
        {
            return new MessageService().AddWorkerMessage(_userId, Option, _userId.ToString(), Title, Content, "");
        }
    }
}
