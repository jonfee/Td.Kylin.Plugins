namespace Td.Kylin.SMS.Config
{
    /// <summary>
    /// SMS Config
    /// </summary>
    public abstract class SmsConfig
    {
        /// <summary>
        /// 短信服务提供商
        /// </summary>
        public SmsProviderType ProviderType { get; set; }
    }
}
