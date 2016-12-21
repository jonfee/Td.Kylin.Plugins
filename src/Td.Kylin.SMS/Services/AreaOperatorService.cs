using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.SMS.Data;

namespace Td.Kylin.SMS.Services
{
    public class AreaOperatorService
    {
        public long GetOperatorId(int areaId)
        {
            using (var db = new DataContext())
            {
                return (from ope in db.Area_OperatorRelation
                        where ope.StartTime <= DateTime.Now && ope.EndTime > DateTime.Now && ope.AreaID == areaId
                        select ope.OperatorID).FirstOrDefault();
            }
        }
    }
}
