using System.Linq;
using Td.Kylin.Message.Data;
using Td.Kylin.Message.Model;

namespace Td.Kylin.Message.Services
{
    /// <summary>
    /// 商家数据服务
    /// </summary>
    internal class MerchantService
    {
        /// <summary>
        /// 获取商家账号及名称
        /// </summary>
        /// <param name="merchantID"></param>
        /// <returns></returns>
        public InviteRegistatorInfo GetUseRegistatorInfo(long merchantID)
        {
            using (var db = new DataContext())
            {
                return (from m in db.Merchant_Account
                        where m.MerchantID == merchantID
                        select new InviteRegistatorInfo
                        {
                            Name = m.Name,
                            Account = m.Mobile
                        }).SingleOrDefault();
            }
        }

        /// <summary>
        /// 获取商家名称
        /// </summary>
        /// <param name="merchantID"></param>
        /// <returns></returns>
        public string GetMerchantName(long merchantID)
        {
            using (var db = new DataContext())
            {
                return (from m in db.Merchant_Account
                        where m.MerchantID == merchantID
                        select m.Name).SingleOrDefault();
            }
        }

        /// <summary>
        /// 获取商家信息
        /// </summary>
        /// <param name="merchantID"></param>
        /// <returns></returns>
        public MerchantInfo GetMerchantInfo(long merchantID)
        {
            using (var db = new DataContext())
            {
                return (from m in db.Merchant_Account
                        where m.MerchantID == merchantID
                        select new MerchantInfo
                        {
                            Name = m.Name,
                            Phone = m.Phone
                        }).SingleOrDefault();
            }
        }
    }
}
