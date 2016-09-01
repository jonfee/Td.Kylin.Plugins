using System;
using System.Collections.Generic;
using Td.Kylin.Message.Services;

namespace Td.Kylin.Message.SysMessage
{
    /// <summary>
    /// 社区活动提醒消息发送器
    /// </summary>
    public class CircleEventRemindMessageSender : SysMessageSender
    {
        #region 属性

        /// <summary>
        /// 帖子ID
        /// </summary>
        private long _topicID;

        /// <summary>
        /// 发帖用户ID
        /// </summary>
        private long _userID;

        #endregion

        /// <summary>
        /// 初始化消息发送器实例
        /// </summary>
        /// <param name="topicID">活动帖ID</param>
        /// <param name="eventTitle">活动帖标题</param>
        /// <param name="eventBeginTime">活动开始时间</param>
        public CircleEventRemindMessageSender(long topicID, string eventTitle, DateTime eventBeginTime) : base(EnumLibrary.MessageTemplateOption.CircleEventBefore)
        {
            _topicID = topicID;

            var topic = new CircleService().GetTopicInfo(topicID);

            if (topic == null) throw new InvalidOperationException("帖子信息不存在，无法继续发送消息");

            _userID = topic.UserID;

            base.ContentFactory(new { TopicTitle = topic.Title, EventTime = eventBeginTime.ToString("yyyy/MM/dd HH:mm:ss")});
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
