namespace Td.Kylin.SMS.Config
{
    /// <summary>
    /// 云片配置信息
    /// </summary>
    public class YuanPianConfig : SmsConfig
    {
        /// <summary>
        /// API KEY
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// 发送接口地址
        /// </summary>
        public string ApiUrl { get; set; }
    }
}
