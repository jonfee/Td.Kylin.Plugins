namespace Td.Kylin.Message
{
    /// <summary>
    /// 消息模板信息
    /// </summary>
    internal class MessageTemplateInfo
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public int MessageType { get; set; }

        /// <summary>
        /// 消息名称/标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息内容模板
        /// </summary>
        public string Template { get; set; }
    }
}
