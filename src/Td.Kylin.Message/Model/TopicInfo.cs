namespace Td.Kylin.Message.Model
{
    /// <summary>
    /// 帖子信息
    /// </summary>
    internal class TopicInfo
    {
        /// <summary>
        /// 帖子ID
        /// </summary>
        public long TopicID { get; set; }

        /// <summary>
        /// 帖子所属圈子（区域圈子）分类ID
        /// </summary>
        public long ForumID { get; set; }

        /// <summary>
        /// 帖子标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 发帖用户ID
        /// </summary>
        public  long UserID { get; set; }
    }
}
