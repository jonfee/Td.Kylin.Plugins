using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Core;
using Td.Kylin.Message.Services;

namespace Td.Kylin.Message.Sender
{
    /// <summary>
    /// 设置圈子版主后消息发送器
    /// </summary>
    public class SetModeratorByForumMessageSender : BaseSender
    {
        #region 属性

        /// <summary>
        /// 圈子ID
        /// </summary>
        private readonly long _forumId;

        /// <summary>
        /// 版主ID
        /// </summary>
        private readonly long[] _userIDs;

        #endregion

        /// <summary>
        /// 初始化消息发送器
        /// </summary>
        /// <param name="forumId">圈子ID</param>
        /// <param name="userId">被设为版主的用户ID</param>
        public SetModeratorByForumMessageSender(long forumId, long userId): this(forumId, new[] { userId })
        {
        }

        /// <summary>
        /// 初始化消息发送器
        /// </summary>
        /// <param name="forumId">圈子ID</param>
        /// <param name="userIDs">被设为版主的用户ID集合</param>
        public SetModeratorByForumMessageSender(long forumId, long[] userIDs) : base(MessageTemplateOption.BecomeModerator)
        {
            _forumId = forumId;

            _userIDs = userIDs;

            var forumName = CacheData.GetAreaForumName(forumId);

            base.ContentFactory(new { ForumName = forumName});
        }

        /// <summary>
        /// 发送系统消息给用户
        /// </summary>
        /// <returns></returns>
        public override bool Send()
        {
            return new MessageService().AddUserMessage(_userIDs, Option, _forumId.ToString(), Title, Content, "");
        }
    }
}
