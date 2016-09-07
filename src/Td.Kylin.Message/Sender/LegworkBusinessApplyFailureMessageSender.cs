using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.EnumLibrary;

namespace Td.Kylin.Message.Sender
{
    /// <summary>
    /// 跑腿业务开通审核失败消息发送器
    /// </summary>
    public class LegworkBusinessApplyFailureMessageSender : BaseSender
    {
        public LegworkBusinessApplyFailureMessageSender() : base(MessageTemplateOption.WorkerLegworkBusinessApplySuccessful)
        {
        }

        public override bool Send()
        {
            throw new NotImplementedException();
        }
    }
}
