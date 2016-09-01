using System.Linq;
using Td.Kylin.Message.Data;
using Td.Kylin.Message.Model;

namespace Td.Kylin.Message.Services
{
    /// <summary>
    /// 商家订单数据服务
    /// </summary>
    internal class MerchantOrderService
    {
        /// <summary>
        /// 获取商家订单信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public MerchantOrderInfo GetGoodsOrderInfo(long orderID)
        {
            using (var db = new DataContext())
            {
                return (from o in db.Merchant_Order
                        where o.OrderID == orderID
                        select new MerchantOrderInfo
                        {
                            OrderCode = o.OrderCode,
                            MerchantID = o.MerchantID,
                            UserID = o.UserID,
                            OrderType = o.OrderType
                        }).SingleOrDefault();
            }
        }
    }
}
