using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.EnumLibrary;
using Td.Kylin.EnumLibrary.Operator;
using Td.Kylin.SMS.Data;

namespace Td.Kylin.SMS.Services
{
    /// <summary>
    /// 区域运营商数据服务
    /// </summary>
    public class AreaOperatorService
    {
        /// <summary>
        /// 获取区域当前的运营商ID
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        public long GetOperator(int areaId)
        {
            using (var db = new DataContext())
            {
                return (from o in db.Area_OperatorRelation
                        where o.AreaID == areaId && o.StartTime <= DateTime.Now && o.EndTime >= DateTime.Now
                        select o.OperatorID).FirstOrDefault();
            }
        }

        /// <summary>
        /// 获取运营商指定通知类型及通知方式匹配的子管理员账号ID集合
        /// </summary>
        /// <param name="operatorId"></param>
        /// <param name="noticeType"></param>
        /// <param name="noticeWay"></param>
        /// <returns></returns>
        public long[] GetNoticeSubAccounts(long operatorId, OperatorBusinessNoticeType noticeType, OperatorBusinessNoticeWay noticeWay)
        {
            using (var db = new DataContext())
            {
                var way = (int)noticeWay;

                return (from c in db.AreaOperator_BusinessNoticeConfig
                        where
                        c.OpearatorID == operatorId && c.NoticeType == (int)noticeType && ((c.NoticeWay & way) == way)
                        select c.SubID).ToArray();
            }
        }

        /// <summary>
        /// 获取指定消息通知需要发送通知的手机号集合
        /// </summary>
        /// <param name="operatorId"></param>
        /// <param name="noticeType"></param>
        /// <returns></returns>
        public string[] GetNoticeMobiles(long operatorId, OperatorBusinessNoticeType noticeType)
        {
            using (var db = new DataContext())
            {
                var way = (int)OperatorBusinessNoticeWay.SMS;

                var accountIds = (from c in db.AreaOperator_BusinessNoticeConfig
                                  where c.OpearatorID == operatorId && c.NoticeType == (int)noticeType && ((c.NoticeWay & way) == way)
                                  select c.SubID).ToArray();

                return (from u in db.Area_OperatorSubAccount
                        where u.AccountStatus == (int)OperatorSubAccountStatus.Normal
                              && accountIds.Contains(u.SubID)
                        select u.Mobile).ToArray();
            }
        }
    }
}
