namespace Td.Kylin.Message
{
    /// <summary>
    /// 消息收发者类型
    /// </summary>
    public enum MessageTransceiverType
    {
        /// <summary>
        /// 普通用户
        /// </summary>
        User = 1,
        /// <summary>
        /// 商户
        /// </summary>
        Merchant = 2,
        /// <summary>
        /// 工作人员
        /// </summary>
        Worker = 4,
        /// <summary>
        /// 区域运营商
        /// </summary>
        Operator = 8,
        /// <summary>
        /// 区域运营管理人员
        /// </summary>
        OperatorSubAccount = 16,
        /// <summary>
        /// 代理商
        /// </summary>
        Agent = 32,
        /// <summary>
        /// 系统平台
        /// </summary>
        System = 1024
    }
}
