using System.Collections.Generic;
using Td.Kylin.SMS.Services;

namespace Td.Kylin.SMS.Cache
{
    /// <summary>
    /// 短信模板缓存
    /// </summary>
    public class SmsTemplateCache
    {
        readonly static object locker = new object();

        static SmsTemplateCache _instance;

        public static SmsTemplateCache Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new SmsTemplateCache();
                        }
                    }
                }

                return _instance;
            }
        }

        private IEnumerable<SmsTemplate> _value;
        public IEnumerable<SmsTemplate> Value
        {
            get { return _value ?? new List<SmsTemplate>(); }
            set { this._value = value; }
        }

        private SmsTemplateCache()
        {
            _value = new GlobalResourceService().GetSmsTemplates();
        }
    }
}
