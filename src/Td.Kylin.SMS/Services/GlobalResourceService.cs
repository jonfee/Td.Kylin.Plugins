using System.Collections.Generic;
using System.Linq;
using Td.Kylin.EnumLibrary;
using Td.Kylin.SMS.Cache;
using Td.Kylin.SMS.Data;

namespace Td.Kylin.SMS.Services
{
    /// <summary>
    /// 全局资源配置数据服务
    /// </summary>
     class GlobalResourceService
    {
        /// <summary>
        /// 获取短信模板
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SmsTemplate> GetSmsTemplates()
        {
            using (var db = new DataContext())
            {
                var query = from p in db.System_GlobalResources
                            where p.ResourceType == (int)GlobalConfigType.SMS
                            select new SmsTemplate
                            {
                                Option = (SmsTemplateOption)p.ResourceKey,
                                Template = p.Value
                            };

                return query.ToList();
            }
        }
    }
}
