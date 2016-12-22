using Td.AspNet.Utils;

namespace Td.Kylin.SMS.Core
{
    /// <summary>
    /// ID生成器
    /// </summary>
    sealed class IDProvider
    {
        /// <summary>
        /// 生成一个新的ID
        /// </summary>
        /// <returns></returns>
        public static long NewId()
        {
            return (long)IDCreater.NewId(98, 1);
        }
    }
}
