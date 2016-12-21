namespace Td.Kylin.SMS.Cache
{
    /// <summary>
    /// 区域通知配置
    /// </summary>
    public class AreaNotify
    {
        /// <summary>
        /// 区域ID
        /// </summary>
        public int AreaId { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 业务通知类型（枚举：OperatorBusinessNoticeType，如：发货通知|库存预警等）
        /// </summary>
        public int NoticeType { get; set; }

        /// <summary>
        /// 业务通知途径（枚举：OperatorBusinessNoticeWay,多个通知途径用枚举值累加方式，如：手机短信1+邮箱2=3）
        /// </summary>
        public int NoticeWay { get; set; }
    }
}
