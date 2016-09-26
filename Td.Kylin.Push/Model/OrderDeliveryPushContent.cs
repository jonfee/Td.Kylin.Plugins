using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Td.Kylin.Push.Model
{
    /// <summary>
    ///工作端-订单送达推送内容。
    /// </summary>
    public class OrderDeliveryPushContent :PublicPushContent
    {
       
        /// <summary>
        /// 订单状态。
        /// </summary>
        public short OrderStatus
        {
            get;
            set;
        }
        
    }
}
