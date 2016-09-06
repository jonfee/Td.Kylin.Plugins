using System;
using Td.Kylin.DataCache;
using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Core;

namespace Td.Kylin.Message
{
    public sealed class MessageSenderExtensions
    {
        /// <summary>
        /// 系统消息发送器构建
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="redisOptions">Redis缓存服务器信息</param>
        /// <param name="keepAlive">是否保持缓存服务器长连接</param>
        /// <param name="cacheItems">需要缓存的数据类型</param>
        /// <param name="level2CacheSeconds">二级缓存时间（单位：秒）</param>
        public static void Factory(string connectionString, SqlProviderType sqlType, string redisOptions, bool keepAlive = true, CacheItemType[] cacheItems = null, int level2CacheSeconds = 30)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            Configs.SqlConnectionString = connectionString;

            Configs.SqlType = sqlType;

            if (CacheCollection.Count < 1)
            {
                if (cacheItems == null || cacheItems.Length < 1)
                {
                    cacheItems = new[]
                    {
                        CacheItemType.SystemGolbalConfig,
                        CacheItemType.AreaForum,
                        CacheItemType.UserLevelConfig
                    };
                }

                DataCacheExtensions.UseDataCache(redisOptions, keepAlive, cacheItems, level2CacheSeconds);
            }
        }
    }
}
