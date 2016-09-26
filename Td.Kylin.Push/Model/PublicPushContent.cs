using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Td.Kylin.Push.Model
{
    public class PublicPushContent
    {
        public string PushCode { get; set; }
        public long OrderID { get; set; }
        public  DateTime CreateTime { get; set; }
        public string OrderCode { get; set; }

    }
}
