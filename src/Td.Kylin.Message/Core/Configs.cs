using Td.Kylin.EnumLibrary;

namespace Td.Kylin.Message.Core
{
    /// <summary>
    /// 配置类
    /// </summary>
    internal sealed class Configs
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        internal static string SqlConnectionString;

        /// <summary>
        /// 数据库类型，<see cref="SqlProviderType"/>枚举
        /// </summary>
        internal static SqlProviderType SqlType;
    }
}
