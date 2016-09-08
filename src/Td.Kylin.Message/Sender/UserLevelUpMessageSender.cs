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
        private readonly long _userId;

        #endregion

        /// <summary>
        /// 初始化消息发送器
        /// </summary>
        /// <param name="userId">用户ID</param>
        public UserLevelUpMessageSender(long userId): base(MessageTemplateOption.UserLevelUp)
        {
            _userId = userId;

            var empirical = new UserService().GetUserEmpirical(userId);

            var levelName = CacheData.GetUserLevelName(empirical);

            base.ContentFactory(new { LevelName = levelName});
        }

        /// <summary>
        /// 发送系统消息给用户
        /// </summary>
        /// <returns></returns>
        public override bool Send()
        {
            //暂不发送消息，后期优化
            //return new MessageService().AddUserMessage(_userID, Option, _userID.ToString(), Title, Content, "");

            return true;
        }
    }
}
