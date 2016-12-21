using System;
using System.Collections.Generic;
using System.Linq;
using Td.Kylin.EnumLibrary.Operator;
using Td.Kylin.SMS.Cache;
using Td.Kylin.SMS.Data;

namespace Td.Kylin.SMS.Services
{
    /// <summary>
    /// 区域通知数据服务
    /// </summary>
    public class AreaNotifyService
    {
        /// <summary>
        /// 获取区域通知配置
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AreaNotify> GetAreaNotifyConfig()
        {
            using (var db = new DataContext())
            {
                var query = from acc in db.Area_OperatorSubAccount
                            join con in db.AreaOperator_BusinessNoticeConfig on acc.SubID equals con.SubID
                            join ope in db.Area_OperatorRelation on acc.OpearatorID equals ope.OperatorID
                            where acc.AccountStatus == (int)OperatorSubAccountStatus.Normal && ope.StartTime <= DateTime.Now && ope.EndTime > DateTime.Now
                            select new AreaNotify
                            {
                                AreaId = ope.AreaID,
                                Mobile = acc.Mobile,
                                NoticeType = con.NoticeType,
                                NoticeWay = con.NoticeWay
                            };

                return query.ToList();
            }
        }
    }
}
