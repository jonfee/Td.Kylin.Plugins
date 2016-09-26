using Td.AspNet.Utils;

namespace Td.Kylin.Push.Core
{
    /// <summary>
    /// ID生成器
    /// </summary>
    internal sealed class IDProvider
    {
        /// <summary>
        /// 生成一个新的ID
        /// </summary>
        /// <returns></returns>
        public static long NewId()
        {
            return (long)IDCreater.NewId(99, 1);
        }
    }
}
