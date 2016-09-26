using System;
using System.Collections.Generic;

namespace Td.Kylin.Push.Model
{
    /// <summary>
    /// 工作端-订单报价推送内容。
    /// </summary>
    public class OrderOfferPushContent :PublicPushContent
    {
       

        /// <summary>
        /// 服务费用。
        /// </summary>
        public decimal Charge
        {
            get;
            set;
        }

        /// <summary>
        /// 服务员ID。
        /// </summary>
        public long WorkerID
        {
            get;
            set;
        }

        /// <summary>
        /// 服务员名称。
        /// </summary>
        public string WorkerName
        {
            get;
            set;
        }

        /// <summary>
        /// 服务员距离取货地或收获地的距离。
        /// </summary>
        public double Distance
        {
            get;
            set;
        }
        
    }
}
