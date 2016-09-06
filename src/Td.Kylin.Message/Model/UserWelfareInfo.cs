using System;

namespace Td.Kylin.Message.Model
{
    /// <summary>
    /// 用户福利信息
    /// </summary>
    internal class UserWelfareInfo
    {
        /// <summary>
        /// 福利所属用户ID
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 福利名称
        /// </summary>
        public string WelfareName { get; set; }

        /// <summary>
        /// 福利有效截止时间
        /// </summary>
        public  DateTime ExpiryEndTime { get; set; }

        /// <summary>
        /// 福利提供商家ID
        /// </summary>
        public long MerchantID { get; set; }

        /// <summary>
        /// 福利提供商家名称
        /// </summary>
        public string MerchantName { get; set; }
    }
}
