namespace Td.Kylin.SMS.Cache
{
    /// <summary>
    /// 区域短信资产
    /// </summary>
     class AreaAssets
    {
        /// <summary>
        /// 区域ID
        /// </summary>
        public int AreaId { get; set; }

        /// <summary>
        /// 运营商ID
        /// </summary>
        public long OperatorId { get; set; }

        /// <summary>
        /// 短信剩余数量
        /// </summary>
        public int Balance { get; set; }

        /// <summary>
        /// 仅记录当前请求数量(不含本次请求）
        /// </summary>
        public int Freeze { get; set;}
    }
}
