using System.Linq;
using Td.Kylin.SMS.Data;
using Td.Kylin.SMS.Model;

namespace Td.Kylin.SMS.Services
{
    /// <summary>
    /// 用户数据服务
    /// </summary>
    public class UserService
    {
        /// <summary>
        /// 获取用户手机号
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserMobile(long userId)
        {
            using (var db = new DataContext())
            {
                return db.User_Account.Where(p => p.UserID == userId).Select(p => p.Mobile).FirstOrDefault();
            }
        }

        /// <summary>
        /// 获取用户账号及名称
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserInfo GetUseInfo(long userId)
        {
            using (var db = new DataContext())
            {
                return (from u in db.User_Account
                        where u.UserID == userId
                        select new UserInfo
                        {
                            Name = u.Username,
                            Mobile = u.Mobile
                        }).SingleOrDefault();
            }
        }
    }
}
