using System.Linq;
using Td.Kylin.Message.Data;
using Td.Kylin.Message.Model;

namespace Td.Kylin.Message.Services
{
    /// <summary>
    /// 兴趣社区相关数据服务
    /// </summary>
    internal class CircleService
    {
        /// <summary>
        /// 获取帖子信息
        /// </summary>
        /// <param name="topicID"></param>
        /// <returns></returns>
        public TopicInfo GetTopicInfo(long topicID)
        {
            using (var db = new DataContext())
            {
                return (from t in db.Circle_Topic
                        where t.TopicID == topicID
                        select new TopicInfo
                        {
                            TopicID = t.TopicID,
                            ForumID = t.ForumID,
                            Title = t.Title,
                            UserID = t.UserID,
                            ItemId=t.ItemId
                        }).SingleOrDefault();
            }
        }

        /// <summary>
        /// 获取活动报名的用户ID集合
        /// </summary>
        /// <param name="eventID"></param>
        /// <returns></returns>
        public long[] GetEventUsers(long eventID)
        {
            using (var db = new DataContext())
            {
                return (from e in db.Circle_EventUser
                    where e.EventID == eventID
                    select e.UserID).ToArray();
            }
        }
    }
}
