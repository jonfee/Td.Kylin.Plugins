using System;

namespace Td.Kylin.Message.Model
{
    /// <summary>
    /// 福利信息
    /// </summary>
    internal class WelfareInfo
    {
        /// <summary>
        /// 福利名称
        /// </summary>
        public  string WelfareName { get; set; }

        /// <summary>
        /// 福利类型
        /// </summary>
        public int WelfareType { get; set; }

        /// <summary>
        /// 商家ID
        /// </summary>
        public  long MerchantID { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? AuditTime { get; set; }

        /// <summary>
        /// 审核说明
        /// </summary>
        public  string AuditRemark { get; set; }
    }
}
