using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Td.Kylin.Push.Model
{
	/// <summary>
	/// 用户确认订单-订单确认推送内容。
	/// </summary>
    public class OrderConfirmPushContent :PublicPushContent
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
