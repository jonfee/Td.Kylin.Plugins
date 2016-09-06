using System;
using System.Collections.Generic;
using Td.Kylin.Message.Services;

namespace Td.Kylin.Message.Sender
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
        /// 活动报名用户ID集合
        /// </summary>
        private long[] _eventUserIds;

        #endregion

        /// <summary>
        /// 初始化消息发送器实例
        /// </summary>
        /// <param name="topicID">活动帖ID</param>
        /// <param name="eventBeginTime">活动开始时间</param>
        public CircleEventRemindMessageSender(long topicID,DateTime eventBeginTime) : base(EnumLibrary.MessageTemplateOption.CircleEventBefore)
        {
            _topicID = topicID;

            var service=new CircleService();

            var topic = service.GetTopicInfo(topicID);

            if (topic == null) throw new InvalidOperationException("帖子信息不存在，无法继续发送消息");

            if(topic.ItemId==null) throw new InvalidOperationException("活动信息不存在，无法继续发送消息");

            _eventUserIds = service.GetEventUsers(topic.ItemId.Value);

            base.ContentFactory(new { TopicTitle = topic.Title, EventTime = eventBeginTime.ToString("yyyy/MM/dd HH:mm:ss")});
        }

        /// <summary>
        /// 发送系统消息给用户
        /// </summary>
        /// <returns></returns>
        public override bool Send()
        {
            if (_eventUserIds != null && _eventUserIds.Length > 0)
            {
                return new MessageService().AddUserMessage(_eventUserIds, Option, _topicID.ToString(), Title, Content,"");
            }

            return true;
        }
    }
}
