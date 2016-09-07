using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Core;
using Td.Kylin.Message.Services;

namespace Td.Kylin.Message.Sender
{
    /// <summary>
    /// 用户等级提升后消息发送器
    /// </summary>
    public class UserLevelUpMessageSender : BaseSender
    {
        #region 属性

        /// <summary>
        /// 用户ID
        /// </summary>
        private readonly long _userID;

        #endregion


        /// <summary>
        /// 初始化消息发送器
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="remark">等级说明</param>
        public UserLevelUpMessageSender(long userID, string remark)
            : base(MessageTemplateOption.UserLevelUpgrade)
        {
            _userID = userID;

            var userLevel = new UserService().GetUserLevelInfo(userID);

            var levelName = CacheData.GetUserLevelName(userLevel.Empirical);

            base.ContentFactory(new { LevelName = levelName, Number = levelName, LevelRemark = remark });
        }

        /// <summary>
        /// 发送系统消息给用户
        /// </summary>
        /// <returns></returns>
        public override bool Send()
        {
            return new MessageService().AddUserMessage(_userID, Option, _userID.ToString(), Title, Content, "");
        }
    }
}
