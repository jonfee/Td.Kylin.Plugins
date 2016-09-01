using System;
using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Core;
using Td.Kylin.Message.Services;

namespace Td.Kylin.Message.SysMessage
{
    /// <summary>
    /// 帖子被设置为热门后消息发送器
    /// </summary>
    public class TopicSetHotMessageSender : SysMessageSender
    {
        #region 属性

        /// <summary>
        /// 帖子ID
        /// </summary>
        private readonly long _topicID;

        /// <summary>
        /// 发帖用户ID
        /// </summary>
        private readonly long _userID;

        #endregion

        /// <summary>
        /// 初始化消息发送器
        /// </summary>
        /// <param name="topicID">被设置热门帖子的ID</param>
        /// <param name="serverPhone">客服电话</param>
        public TopicSetHotMessageSender(long topicID, string serverPhone) : base(MessageTemplateOption.SetHotByCircleTopic)
        {
            _topicID = topicID;

            var topic = new CircleService().GetTopicInfo(topicID);

            if (topic == null) throw new InvalidOperationException("帖子信息不存在，无法继续发送消息");

            _userID = topic.UserID;

            var forumName = CacheData.GetAreaForumName(topic.ForumID);

            base.ContentFactory(new { ForumName = forumName, TopicTitle = topic.Title, ServerPhone = serverPhone });
        }

        /// <summary>
        /// 发送系统消息给用户
        /// </summary>
        /// <returns></returns>
        public override bool Send()
        {
            return new MessageService().AddUserMessage(_userID, Option, _topicID.ToString(), Title, Content, "");
        }
    }
}
