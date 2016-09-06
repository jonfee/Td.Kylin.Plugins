using System;
using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Services;

namespace Td.Kylin.Message.Sender
{
    /// <summary>
    /// 福利中奖消息发送器
    /// </summary>
    public class WelfareLotteryWinnerMessageSender : SysMessageSender
    {
        #region 属性

        /// <summary>
        /// 中奖用户ID集合
        /// </summary>
        private readonly long[] _winnerUserIds;

        /// <summary>
        /// 福利ID
        /// </summary>
        private readonly long _welfareID;

        #endregion


        /// <summary>
        /// 初始化消息发送器
        /// </summary>
        /// <param name="welfareID">福利ID</param>
        public WelfareLotteryWinnerMessageSender(long welfareID) : this(welfareID, null) { }

        /// <summary>
        /// 初始化消息发送器
        /// </summary>
        /// <param name="welfareID">福利ID</param>
        /// <param name="winnerIds">福利中奖人员ID集合</param>
        public WelfareLotteryWinnerMessageSender(long welfareID, long[] winnerIds)
            : base(MessageTemplateOption.WelfareWin)
        {
            _welfareID = welfareID;

            var service = new WelfareService();

            var welfareName = service.GetWelfareName(welfareID);

            if (string.IsNullOrWhiteSpace(welfareName)) throw new InvalidOperationException("福利信息不存在，无法继续发送消息");

            if (winnerIds != null && winnerIds.Length > 0)
            {
                _winnerUserIds = winnerIds;
            }
            else
            {
                _winnerUserIds = service.GetWelfareWinners(welfareID);
            }

            base.ContentFactory(new { WelfareName = welfareName });
        }

        /// <summary>
        /// 发送系统消息给中奖用户
        /// </summary>
        /// <returns></returns>
        public override bool Send()
        {
            if (_winnerUserIds != null && _winnerUserIds.Length > 0)
            {
                return new MessageService().AddUserMessage(_winnerUserIds, Option, _welfareID.ToString(), Title, Content, "");
            }

            return true;
        }
    }
}
