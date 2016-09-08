using System.Linq;
using Td.Kylin.Message.Data;
using Td.Kylin.Message.Model;

namespace Td.Kylin.Message.Services
{
    /// <summary>
    /// 用户数据服务
    /// </summary>
    internal class UserService
    {
        /// <summary>
        /// 获取用户账号及名称
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public InviteRegistatorInfo GetUseRegistatorInfo(long userId)
        {
            using (var db = new DataContext())
            {
                return (from u in db.User_Account
                        where u.UserID == userId
                        select new InviteRegistatorInfo
                        {
                            Name = u.Username,
                            Account = u.Mobile
                        }).SingleOrDefault();
            }
        }

        /// <summary>
        /// 获取用户经验值 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetUserEmpirical(long userId)
        {
            using (var db = new DataContext())
            {
                //经验值
                return (from u in db.User_Account
                        where u.UserID == userId
                        select u.Empirical).SingleOrDefault();
            }
        }

        /// <summary>
        /// 获取用户等级及排名信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public UserLevelInfo GetUserLevelInfo(long userID)
        {
            using (var db = new DataContext())
            {
                //经验值
                var empirical = (from u in db.User_Account
                                 where u.UserID == userID
                                 select u.Empirical).SingleOrDefault();

                int top = 1;

                var topNumber = (from p in (from u in db.User_Account.AsEnumerable()
                                            orderby u.Empirical descending
                                            select new
                                            {
                                                Top = top++,
                                                userID = u.UserID
                                            })
                                 where p.userID == userID
                                 select p.Top).SingleOrDefault();

                return new UserLevelInfo
                {
                    Empirical = empirical,
                    TopNumber = topNumber
                };
            }
        }
    }
}
