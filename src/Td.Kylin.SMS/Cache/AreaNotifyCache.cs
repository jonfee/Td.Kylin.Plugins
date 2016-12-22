using System.Collections.Generic;
using Td.Kylin.SMS.Services;

namespace Td.Kylin.SMS.Cache
{
    /// <summary>
    /// 区域通知配置缓存
    /// </summary>
     class AreaNotifyCache
    {
        readonly static object locker = new object();

        static AreaNotifyCache _instance;

        public static AreaNotifyCache Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new AreaNotifyCache();
                        }
                    }
                }

                return _instance;
            }
        }

        private IEnumerable<AreaNotify> _value;
        public IEnumerable<AreaNotify> Value
        {
            get { return _value ?? new List<AreaNotify>(); }
            set { this._value = value; }
        }

        private AreaNotifyCache()
        {
            _value = new AreaNotifyService().GetAreaNotifyConfig();
        }
    }
}
