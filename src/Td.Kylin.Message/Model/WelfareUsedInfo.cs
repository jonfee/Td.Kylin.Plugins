using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Td.Kylin.Message.Model
{
    /// <summary>
    /// 获取福利使用信息
    /// </summary>
    internal class WelfareUsedInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID { get; set; }

        /// <summary>
        /// 福利名称
        /// </summary>
        public string WelfareName { get; set; }

        /// <summary>
        /// 福利有效期
        /// </summary>
        public DateTime ExpiryEndTime { get; set; }

       /// <summary>
       /// 福利提供商户ID
       /// </summary>
        public long MerchantID { get; set; }

        /// <summary>
        /// 福利提供商户名称
        /// </summary>
        public string MerchantName { get; set; }

        /// <summary>
        /// 福利使用时间
        /// </summary>
        public DateTime? UseTime { get; set; }

        /// <summary>
        /// 福利是否已使用
        /// </summary>
        public bool IsUsed { get; set; }
    }
}
