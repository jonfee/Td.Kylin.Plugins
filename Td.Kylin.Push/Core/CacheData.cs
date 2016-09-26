using System.Linq;
using Td.Kylin.DataCache;
using Td.Kylin.EnumLibrary;

namespace Td.Kylin.Push.Core
{
    /// <summary>
    /// 缓存数据类
    /// </summary>
    internal sealed class CacheData
    {
        ///// <summary>
        ///// 获取指定消息业务的配置信息
        ///// </summary>
        ///// <param name="option">消息业务配置项<see cref="MessageTemplateOption"/>枚举</param>
        ///// <returns></returns>
        //internal static MessageTemplateInfo GetTemplate(MessageTemplateOption option)
        //{
        //    if (CacheCollection.SystemGolbalConfigCache != null)
        //    {
        //        var cacheValue = CacheCollection.SystemGolbalConfigCache.Get((int)GlobalConfigType.Message, (int)option);

        //        if (cacheValue != null)
        //        {
        //            return new MessageTemplateInfo
        //            {
        //                MessageType = cacheValue.ResourceKey,
        //                Template = cacheValue.Value,
        //                Title = cacheValue.Name
        //            };
        //        }
        //    }

        //    return null;
        //}

        /// <summary>
        /// 获取区域圈子名称
        /// </summary>
        /// <param name="areaForumID"></param>
        /// <returns></returns>
        internal static string GetAreaForumName(long areaForumID)
        {
            if (CacheCollection.AreaForumCache != null)
            {
                var cacheValue = CacheCollection.AreaForumCache.Get(areaForumID);
                return cacheValue != null ? cacheValue.AliasName : string.Empty;
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取区域名称
        /// </summary>
        /// <param name="areaID"></param>
        /// <returns></returns>
        internal static string GetAreaName(int areaID)
        {
            if (CacheCollection.SystemAreaCache != null)
            {
                var cacheValue = CacheCollection.SystemAreaCache.Get(areaID);
                return cacheValue != null ? cacheValue.AreaName : string.Empty;
            }

            return string.Empty;
        }

        /// <summary>
        /// 根据经验值获取等级名称
        /// </summary>
        /// <param name="empirical"></param>
        /// <returns></returns>
        internal static string GetUserLevelName(int empirical)
        {
            if (CacheCollection.UserLevelConfigCache != null)
            {
                var cacheValue = CacheCollection.UserLevelConfigCache.Value().OrderBy(p=>p.Min).FirstOrDefault(p=>p.Min>=empirical);

                return cacheValue?.Name;
            }

            return string.Empty;
        }
    }
}
