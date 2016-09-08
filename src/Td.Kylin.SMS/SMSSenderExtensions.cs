using System;
using Td.Kylin.DataCache;
using Td.Kylin.EnumLibrary;
using Td.Kylin.SMS.Config;
using Td.Kylin.SMS.Core;
using Td.Kylin.SMS.Provider;

namespace Td.Kylin.SMS
{
    /// <summary>
    /// 短信发送器扩展
    /// </summary>
    public sealed class SMSSenderExtensions
    {
        /// <summary>
        /// 系统消息发送器构建
        /// </summary>
        /// <param name="providerType"><seealso cref="SmsProviderType"/></param>
        /// <param name="smsConfig"><seealso cref="SmsConfig"/>配置信息，根据providerType参数值配置对应的信息</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlType">数据库类型</param>
        /// <param name="redisOptions">Redis缓存服务器信息</param>
        /// <param name="keepAlive">是否保持缓存服务器长连接</param>
        /// <param name="cacheItems">需要缓存的数据类型</param>
        /// <param name="level2CacheSeconds">二级缓存时间（单位：秒）</param>
        public static void Factory(SmsProviderType providerType, SmsConfig smsConfig, string connectionString, SqlProviderType sqlType, string redisOptions, bool keepAlive = true, CacheItemType[] cacheItems = null, int level2CacheSeconds = 30)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            switch (providerType)
            {
                case SmsProviderType.YunPian:
                    ConfigRoot.SendProvider = new YunPianProvider(smsConfig as YuanPianConfig);
                    break;
            }

            ConfigRoot.SqlConnectionString = connectionString;

            ConfigRoot.SqlType = sqlType;

            if (CacheCollection.Count < 1)
            {
                if (cacheItems == null || cacheItems.Length < 1)
                {
                    cacheItems = new[]
                    {
                        CacheItemType.SystemGolbalConfig
                    };
                }

                DataCacheExtensions.UseDataCache(redisOptions, keepAlive, cacheItems, level2CacheSeconds);
            }
        }
    }
}
