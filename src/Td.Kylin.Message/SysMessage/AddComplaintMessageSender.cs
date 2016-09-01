using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Services;

namespace Td.Kylin.Message.SysMessage
{
    /// <summary>
    /// 提交举报信息后消息发送器
    /// </summary>
    public class AddComplaintMessageSender : SysMessageSender
    {
        #region 属性

        /// <summary>
        /// 用户ID
        /// </summary>
        private readonly long _userID;

        /// <summary>
        /// 举报/投诉ID
        /// </summary>
        private readonly long _complaintID;

        #endregion

        /// <summary>
        /// 初始化消息发送器
        /// </summary>
        /// <param name="complaintID">举报/投诉的ID</param>
        /// <param name="userID">登录用户ID</param>
        public AddComplaintMessageSender(int complaintID, long userID) : base(MessageTemplateOption.ComplaintsSuccess)
        {
            _userID = userID;

            _complaintID = complaintID;
        }

        /// <summary>
        /// 发送系统消息给用户
        /// </summary>
        /// <returns></returns>
        public override bool Send()
        {
            return new MessageService().AddUserMessage(_userID, Option, _complaintID.ToString(), Title, Content, "");
        }
    }
}
