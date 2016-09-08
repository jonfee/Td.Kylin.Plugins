using System.Collections.Generic;
using System.Linq;
using Td.Kylin.DataCache;
using Td.Kylin.EnumLibrary;

namespace Td.Kylin.SMS.Core
{
    /// <summary>
    /// 缓存数据类
    /// </summary>
    internal sealed class CacheData
    {
        /// <summary>
        /// 获取指定消息业务的配置信息
        /// </summary>
        /// <param name="option">消息业务配置项<see cref="SmsTemplateOption"/>枚举</param>
        /// <returns></returns>
        internal static string GetTemplate(SmsTemplateOption option)
        {
            if (CacheCollection.SystemGolbalConfigCache != null)
            {
                var cacheValue = CacheCollection.SystemGolbalConfigCache.Get((int)GlobalConfigType.SMS, (int)option);

                if (cacheValue != null)
                {
                    return cacheValue.Value;
                }
            }

            return "";
        }
    }
}
