using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Services;

namespace Td.Kylin.Message.Sender
{
    /// <summary>
    /// 邀请用户注册成功消息发送器
    /// </summary>
    public class InviteUserRegistSuccessfulMessageSender : BaseSender
    {
        #region 属性

        /// <summary>
        /// 注册用户ID
        /// </summary>
        private readonly long _registUserId;

        /// <summary>
        /// 邀请者ID
        /// </summary>
        private readonly long _inviterId;

        /// <summary>
        /// 邀请者身份
        /// </summary>
        private readonly IdentityType _inviterType;

        #endregion

        /// <summary>
        /// 初始化消息发送器
        /// </summary>
        /// <param name="registUserID">注册用户ID</param>
        /// <param name="inviterId">邀请者ID</param>
        /// <param name="inviterType">邀请者身份</param>
        public InviteUserRegistSuccessfulMessageSender(long registUserID, long inviterId, IdentityType inviterType) : base(MessageTemplateOption.InviteUserRegistrationSuccessful)
        {
            _registUserId = registUserID;

            _inviterId = inviterId;

            _inviterType = inviterType;

            base.ContentFactory(new { });
        }

        /// <summary>
        /// 发送系统消息给用户
        /// </summary>
        /// <returns></returns>
        public override bool Send()
        {
            bool success = false;

            switch (_inviterType)
            {
                case IdentityType.User:
                    success = new MessageService().AddUserMessage(_inviterId, Option, _registUserId.ToString(), Title, Content, "");
                    break;
                case IdentityType.Merchant:
                    success = new MessageService().AddMerchantMessage(_inviterId, Option, _registUserId.ToString(), Title, Content, "");
                    break;
            }

            return success;
        }
    }
}
