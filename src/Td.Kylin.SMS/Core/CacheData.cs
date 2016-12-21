using System.Linq;
using Td.Kylin.EnumLibrary;
using Td.Kylin.SMS.Cache;

namespace Td.Kylin.SMS.Core
{
    /// <summary>
    /// 缓存数据类
    /// </summary>
    internal sealed class CacheData
    {
        /// <summary>
        /// 获取短信模板
        /// </summary>
        /// <param name="option">短信业务配置项<see cref="SmsTemplateOption"/>枚举</param>
        /// <returns></returns>
        internal static string GetTemplate(SmsTemplateOption option)
        {
            var item = SmsTemplateCache.Instance.Value.Where(p => p.Option == option).FirstOrDefault();

            return item?.Template;
        }

        /// <summary>
        /// 获取指定区域通知类型的手机号
        /// </summary>
        /// <param name="areaId">区域ID</param>
        /// <param name="noticeType">通知类型</param>
        /// <returns></returns>
        internal static string[] GetMobiles(int areaId, OperatorBusinessNoticeType noticeType)
        {
            return AreaNotifyCache.Instance.Value
                .Where(p => p.AreaId == areaId && p.NoticeType == (int)noticeType && p.NoticeWay == (int)OperatorBusinessNoticeWay.SMS)
                .Select(p => p.Mobile).ToArray();
        }
    }
}
