using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Services;

namespace Td.Kylin.Message.Sender
{
    /// <summary>
    /// 实名认证成功后消息发送器
    /// </summary>
    public class UserRealNameCertificationSuccessMessageSender : SysMessageSender
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
        /// <param name="serverPhone">客服电话</param>
        public UserRealNameCertificationSuccessMessageSender(long userID, string serverPhone)
            : base(MessageTemplateOption.UserCertificationSuccess)
        {
            _userID = userID;

            base.ContentFactory(new { ServerPhone = serverPhone });
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
