using System.Collections.Generic;
using System.Linq;
using Td.Kylin.EnumLibrary.Operator;
using Td.Kylin.SMS.Services;

namespace Td.Kylin.SMS.Cache
{
    /// <summary>
    /// 区域短信数量缓存
    /// </summary>
    class AreaAssetsCache
    {
        readonly static object locker = new object();

        static AreaAssetsCache _instance;

        public static AreaAssetsCache Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new AreaAssetsCache();
                        }
                    }
                }

                return _instance;
            }
        }

        private IEnumerable<AreaAssets> _value;
        private IEnumerable<AreaAssets> Value
        {
            get { return _value ?? new List<AreaAssets>(); }
            set { this._value = value; }
        }

        private AreaAssetsCache()
        {
            _value = new OperatorAssetsService().GetAreaAssets(OperatorAssetsType.Sms);
        }

        /// <summary>
        /// 获取区域运营商剩余短信资源
        /// </summary>
        /// <param name="areaId"></param>
        /// <param name="requestNumber">请求数量</param>
        /// <returns></returns>
        public AreaAssets GetSmsAssets(int areaId, int requestNumber)
        {
            lock (locker)
            {
                var item = Value.Where(p => p.AreaId == areaId).FirstOrDefault();

                if (item == null)
                {
                    item = new AreaAssets
                    {
                        AreaId = areaId,
                        OperatorId = 0,
                        Balance = 0,
                        Freeze = 0
                    };
                }
                else
                {
                    item.Freeze += requestNumber;
                }

                int allotNumber = item.Balance - item.Freeze;
                if (allotNumber >= requestNumber)
                {
                    allotNumber = requestNumber;
                }

                if (allotNumber < 0) allotNumber = 0;

                //给本次请求分配的资源
                return new AreaAssets
                {
                    AreaId = item.AreaId,
                    OperatorId = item.OperatorId,
                    Balance = allotNumber
                };
            }
        }

        /// <summary>
        /// 使用短信
        /// </summary>
        /// <param name="areaId">区域ID</param>
        /// <param name="operatorId">运营商ID</param>
        /// <param name="planSendNumber">计划发送数量</param>
        /// <param name="useNumber">实际使用数量</param>
        /// <param name="sendSuccess">是否发送成功</param>
        public void UseAssets(int areaId, long operatorId,int planSendNumber, int useNumber, bool sendSuccess)
        {
            lock (locker)
            {
                try
                {
                    //发送成功，则更新资产，释放冻结的短信数
                    if (sendSuccess)
                    {
                        new OperatorAssetsService().UseAssets(operatorId, OperatorAssetsType.Sms, useNumber);

                        Value.Where(p => p.OperatorId == operatorId).ToList().ForEach((item) =>
                        {
                            item.Balance -= useNumber;
                            item.Freeze -= planSendNumber;
                        });
                    }
                    else//发送失败，则释放冻结的短信数
                    {
                        Value.Where(p => p.OperatorId == operatorId).ToList().ForEach((item) =>
                        {
                            item.Freeze -= planSendNumber;
                        });
                    }
                }
                catch { }
            }
        }
    }
}
