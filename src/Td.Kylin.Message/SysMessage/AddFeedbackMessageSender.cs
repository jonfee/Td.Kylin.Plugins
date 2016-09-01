using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Services;

namespace Td.Kylin.Message.SysMessage
{
    /// <summary>
    /// 提交反馈信息成功后消息发送器
    /// </summary>
    public class AddFeedbackMessageSender:SysMessageSender
    {
        #region 属性

        /// <summary>
        /// 用户ID
        /// </summary>
        private readonly long _userID;

        /// <summary>
        /// 反馈ID
        /// </summary>
        private readonly long _feedbackID;

        #endregion

        /// <summary>
        /// 初始化消息发送器
        /// </summary>
        /// <param name="feedbackID">反馈的ID</param>
        /// <param name="userID">用户ID</param>
        public AddFeedbackMessageSender(int feedbackID, long userID) : base(MessageTemplateOption.FeedbackSuccess)
        {
            _userID = userID;

            _feedbackID = feedbackID;
        }

        /// <summary>
        /// 发送系统消息给用户
        /// </summary>
        /// <returns></returns>
        public override bool Send()
        {
            return new MessageService().AddUserMessage(_userID, Option, _feedbackID.ToString(), Title, Content, "");
        }
    }
}
