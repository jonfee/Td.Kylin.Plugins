using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Core;
using Td.Kylin.Message.Services;

namespace Td.Kylin.Message.Sender
{
    /// <summary>
    /// 取消版主后消息发送器
    /// </summary>
    public class CancelModeratorByForumMessageSender : SysMessageSender
    {
        #region 属性

        /// <summary>
        /// 圈子ID
        /// </summary>
        private readonly long _forumID;

        /// <summary>
        /// 发帖用户ID
        /// </summary>
        private readonly long[] _userIDs;

        #endregion

        /// <summary>
        /// 初始化消息发送器
        /// </summary>
        /// <param name="forumID">圈子ID</param>
        /// <param name="userID">被取消版主的用户ID</param>
        public CancelModeratorByForumMessageSender(long forumID, long userID)
            : this(forumID, new[] { userID })
        {
        }

        /// <summary>
        /// 初始化消息发送器
        /// </summary>
        /// <param name="forumID">圈子ID</param>
        /// <param name="userIDs">被取消版主的用户ID集合</param>
        public CancelModeratorByForumMessageSender(long forumID, long[] userIDs) : base(MessageTemplateOption.CancelModerator)
        {
            _forumID = forumID;

            _userIDs = userIDs;

            var forumName = CacheData.GetAreaForumName(forumID);

            base.ContentFactory(new { ForumName = forumName});
        }

        /// <summary>
        /// 发送系统消息给用户
        /// </summary>
        /// <returns></returns>
        public override bool Send()
        {
            return new MessageService().AddUserMessage(_userIDs, Option, _forumID.ToString(), Title, Content, "");
        }
    }
}
