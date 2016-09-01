using System;
using Td.Kylin.DataCache;
using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Core;

namespace Td.Kylin.Message
{
    public sealed class MessageSenderExtensions
    {
        /// <summary>
        /// 消息发送器生成
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="options"><seealso cref="DataCacheServerOptions"/>缓存服务配置，如无需用到缓存时可为null</param>
        public static void Factory(string connectionString, SqlProviderType sqlType, DataCacheServerOptions options = null)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            Configs.SqlConnectionString = connectionString;

            Configs.SqlType = sqlType;

            if (CacheCollection.Count < 1)
            {
                options.CacheItems = new CacheItemType[]
                {
                    CacheItemType.SystemGolbalConfig,
                    CacheItemType.AreaForum,
                    CacheItemType.UserLevelConfig
                };

                DataCacheExtensions.UseDataCache(options);
            }
        }
    }
}
