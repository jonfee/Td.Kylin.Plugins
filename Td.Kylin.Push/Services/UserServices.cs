using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.Entity;
using Td.Kylin.Push.Data;

namespace Td.Kylin.Push.Services
{
    public class UserServices : DataContext
    {
        public static User_Account GetUserModel(long UserID)
        {
            using (var db=new DataContext())
            {

                return db.User_Account.Where(q => q.UserID == UserID).FirstOrDefault();
            }
        }

        public static User_Address GetUserAddressModel(long addressID)
        {
            using ( var db=new DataContext())
            {
                return db.User_Address.Where(q => q.AddressID == addressID).FirstOrDefault();
            }
        }

        public static Worker_Account GetWorkerAccount(long workerID)
        {
            using ( var db=new DataContext())
            {
                return db.Worker_Account.Where(q => q.WorkerID == workerID).FirstOrDefault();
            }
        }

    }
}
 