﻿using System;
using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Services;

namespace Td.Kylin.Message.SysMessage
{
    /// <summary>
    /// 福利开奖结果消息发送器
    /// </summary>
    public class WelfareLotteryResultMessageSender : SysMessageSender
    {
        #region 属性

        /// <summary>
        /// 参与用户ID集合
        /// </summary>
        private readonly long[] _partUserIds;

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
        public WelfareLotteryResultMessageSender(long welfareID, string serverPhone)
            : base(MessageTemplateOption.WelfareFinish)
        {
            _welfareID = welfareID;

            var service = new WelfareService();

            var welfareName = service.GetWelfareName(welfareID);

            if (string.IsNullOrWhiteSpace(welfareName)) throw new InvalidOperationException("福利信息不存在，无法继续发送消息");

            _partUserIds = service.GetWelfarePartUsers(welfareID);

            base.ContentFactory(new { WelfareName = welfareName, ServerPhone = serverPhone });
        }

        /// <summary>
        /// 发送系统消息给参与用户
        /// </summary>
        /// <returns></returns>
        public override bool Send()
        {
            if (_partUserIds != null && _partUserIds.Length > 0)
            {
                return new MessageService().AddUserMessage(_partUserIds, Option, _welfareID.ToString(), Title, Content, "");
            }

            return true;
        }
    }
}
