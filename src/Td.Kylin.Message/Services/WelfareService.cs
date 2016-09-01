using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.Message.Data;
using Td.Kylin.Message.Model;

namespace Td.Kylin.Message.Services
{
    /// <summary>
    /// 福利数据服务
    /// </summary>
    internal class WelfareService
    {
        /// <summary>
        /// 获取福利名称
        /// </summary>
        /// <param name="welfareID"></param>
        /// <returns></returns>
        public string GetWelfareName(long welfareID)
        {
            using (var db = new DataContext())
            {
                return (from w in db.Merchant_Welfare
                        where w.WelfareID == welfareID
                        select w.Name).SingleOrDefault();
            }
        }

        /// <summary>
        /// 获取福利信息
        /// </summary>
        /// <param name="welfareID"></param>
        /// <returns></returns>
        public WelfareInfo GetWelfareInfo(long welfareID)
        {
            using (var db = new DataContext())
            {
                return (from w in db.Merchant_Welfare
                        where w.WelfareID == welfareID
                        select new WelfareInfo
                        {
                            MerchantID = w.MerchantID,
                            WelfareName = w.Name,
                            WelfareType = w.WelfareType,
                            AuditTime = w.AuditTime,
                            AuditRemark = w.AuditRemark
                        }).SingleOrDefault();
            }
        }

        /// <summary>
        /// 根据福利消费码获取用户福利信息
        /// </summary>
        /// <param name="consumerCode"></param>
        /// <returns></returns>
        public UserWelfareInfo GetUserWelfareInfo(long consumerCode)
        {
            using (var db = new DataContext())
            {
                return (from w in db.User_Welfare
                        where w.ConsumerCode == consumerCode
                        select new UserWelfareInfo
                        {
                            UserID = w.UserID,
                            WelfareName = w.Name,
                            ExpiryEndTime = w.ExpiryEndTime,
                            MerchantID = w.MerchantID,
                            MerchantName = w.MerchantName
                        }).SingleOrDefault();
            }
        }

        /// <summary>
        /// 获取福利使用信息
        /// </summary>
        /// <param name="consumerCode"></param>
        /// <returns></returns>
        public WelfareUsedInfo GetWelfareUsedInfo(long consumerCode)
        {
            using (var db = new DataContext())
            {
                return (from w in db.User_Welfare
                        where w.ConsumerCode == consumerCode
                        select new WelfareUsedInfo
                        {
                            UserID = w.UserID,
                            WelfareName = w.Name,
                            ExpiryEndTime = w.ExpiryEndTime,
                            MerchantID = w.MerchantID,
                            MerchantName = w.MerchantName,
                            UseTime = w.UseTime,
                            IsUsed = w.IsUsed
                        }).SingleOrDefault();
            }
        }

        /// <summary>
        /// 获取福利的中奖用户ID集合
        /// </summary>
        /// <param name="welfareID"></param>
        /// <returns></returns>
        public long[] GetWelfareWinners(long welfareID)
        {
            using (var db = new DataContext())
            {
                return (from w in db.User_Welfare
                        where w.WelfareID == welfareID
                        select w.UserID
                ).ToArray();
            }
        }

        /// <summary>
        /// 获取福利参与人员ID集合
        /// </summary>
        /// <param name="welfareID"></param>
        /// <returns></returns>
        public long[] GetWelfarePartUsers(long welfareID)
        {
            using (var db = new DataContext())
            {
                return (from w in db.Welfare_PartUser
                        where w.WelfareID == welfareID
                        select w.UserID
                ).ToArray();
            }
        }
    }
}
