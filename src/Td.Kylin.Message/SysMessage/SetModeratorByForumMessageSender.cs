using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Core;
using Td.Kylin.Message.Services;

namespace Td.Kylin.Message.SysMessage
{
    /// <summary>
    /// 设置圈子版主后消息发送器
    /// </summary>
    public class SetModeratorByForumMessageSender : SysMessageSender
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
        /// <param name="userID">被设为版主的用户ID</param>
        /// <param name="serverPhone">客服电话</param>
        public SetModeratorByForumMessageSender(long forumID, long userID, string serverPhone)
            : this(forumID, new[] { userID }, serverPhone)
        {
        }

        /// <summary>
        /// 初始化消息发送器
        /// </summary>
        /// <param name="forumID">圈子ID</param>
        /// <param name="userIDs">被设为版主的用户ID集合</param>
        /// <param name="serverPhone">客服电话</param>
        public SetModeratorByForumMessageSender(long forumID, long[] userIDs, string serverPhone) : base(MessageTemplateOption.SetModerator)
        {
            _forumID = forumID;

            _userIDs = userIDs;

            var forumName = CacheData.GetAreaForumName(forumID);

            base.ContentFactory(new { ForumName = forumName, ServerPhone = serverPhone });
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
