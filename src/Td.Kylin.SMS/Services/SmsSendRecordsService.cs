using System.Collections.Generic;
using System.Linq;
using Td.Kylin.Entity;
using Td.Kylin.SMS.Data;

namespace Td.Kylin.SMS.Services
{
    /// <summary>
    /// 短信发送记录数据服务
    /// </summary>
     class SmsSendRecordsService
    {
        /// <summary>
        /// 添加短信发送记录
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public bool AddRecord(SmsSendRecords record)
        {
            return AddRecord(new[] {record});
        }

        /// <summary>
        /// 添加短信发送记录
        /// </summary>
        /// <param name="records"></param>
        /// <returns></returns>
        public bool AddRecord(IEnumerable<SmsSendRecords>  records)
        {
            using (var db = new DataContext())
            {
                if (!records.Any()) return false;

                db.SmsSendRecords.AddRange(records);

                return db.SaveChanges() > 0;
            }
        }
    }
}
