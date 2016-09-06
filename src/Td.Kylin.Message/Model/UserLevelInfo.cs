namespace Td.Kylin.Message.Model
{
    /// <summary>
    /// 用户等级信息
    /// </summary>
    internal class UserLevelInfo
    {
        /// <summary>
        /// 经验值
        /// </summary>
        public int Empirical { get; set; }

        /// <summary>
        /// 当前排名
        /// </summary>
        public  int TopNumber { get; set; }
    }
}
