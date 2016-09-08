using Td.Kylin.EnumLibrary;
using Td.Kylin.SMS.Config;
using Td.Kylin.SMS.Provider;

namespace Td.Kylin.SMS.Core
{
    /// <summary>
    /// 配置类
    /// </summary>
    internal sealed class ConfigRoot
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        internal static string SqlConnectionString;

        /// <summary>
        /// 数据库类型，<see cref="SqlProviderType"/>枚举
        /// </summary>
        internal static SqlProviderType SqlType;

        /// <summary>
        /// 短信发送服务
        /// </summary>
        internal static ISendProvider SendProvider;
    }
}
