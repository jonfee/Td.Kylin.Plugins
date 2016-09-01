using System;
using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Services;

namespace Td.Kylin.Message.SysMessage
{
    /// <summary>
    /// 福利审核通过后消息发送器
    /// </summary>
    public class WelfareAuditSuccessMessageSender : SysMessageSender
    {
        #region 属性

        /// <summary>
        /// 商户ID
        /// </summary>
        private readonly long _merchantID;

        /// <summary>
        /// 福利ID
        /// </summary>
        private readonly long _welfareID;

        #endregion


        /// <summary>
        /// 初始化消息发送器
        /// </summary>
        /// <param name="welfareID">福利ID</param>
        /// <param name="serverPhone">客服电话</param>
        public WelfareAuditSuccessMessageSender(long welfareID, string serverPhone)
            : base(MessageTemplateOption.WelfareAuditSuccess)
        {
            _welfareID = welfareID;

            var welfare = new WelfareService().GetWelfareInfo(welfareID);

            if (welfare == null) throw new InvalidOperationException("福利信息不存在，无法继续发送消息");

            var auditTimeString = (welfare.AuditTime ?? DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss");

            base.ContentFactory(new { WelfareName = welfare.WelfareName, DateTime = auditTimeString, ServerPhone = serverPhone });
        }

        /// <summary>
        /// 发送系统消息给商家
        /// </summary>
        /// <returns></returns>
        public override bool Send()
        {
            return new MessageService().AddMerchantMessage(_merchantID, Option, _welfareID.ToString(), Title, Content, "");
        }
    }
}
