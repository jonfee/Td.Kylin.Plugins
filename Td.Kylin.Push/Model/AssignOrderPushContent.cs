using System;
using System.Collections.Generic;

namespace Td.Kylin.Push.Model
{
    /// <summary>
    /// 用户下单-指派订单推送内容。
    /// </summary>
    public class AssignOrderPushContent: PublicPushContent
    {
       
        /// <summary>
        /// 订单类型。
        /// </summary>
        public short OrderType
        {
            get;
            set;
        }
        
    }

}