using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Services;

namespace Td.Kylin.Message.Sender
{
    /// <summary>
    /// 用户注册成功消息发送器
    /// </summary>
    public class RegistSuccessfulMessageSender : BaseSender
    {
        #region 属性

        /// <summary>
        /// 注册者ID
        /// </summary>
        private readonly long _registratorID;

        /// <summary>
        /// 注册者身份
        /// </summary>
        private readonly IdentityType _registratorType;

        #endregion

        /// <summary>
        /// 初始化消息发送器
        /// </summary>
        /// <param name="registratorID">注册者ID</param>
        /// <param name="registratorType"><seealso cref="IdentityType"/>注册者身份类型</param>
        public RegistSuccessfulMessageSender(long registratorID, IdentityType registratorType) : base(MessageTemplateOption.RegistrationSuccessful)
        {
            _registratorID = registratorID;

            _registratorType = registratorType;

            base.ContentFactory(new { });
        }

        /// <summary>
        /// 发送系统消息给用户
        /// </summary>
        /// <returns></returns>
        public override bool Send()
        {
            bool success = false;

            switch (_registratorType)
            {
                case IdentityType.User:
                    success = new MessageService().AddUserMessage(_registratorID, Option, _registratorID.ToString(), Title, Content, "");
                    break;
                case IdentityType.Merchant:
                    success = new MessageService().AddMerchantMessage(_registratorID, Option, _registratorID.ToString(), Title, Content, "");
                    break;
            }

            return success;
        }
    }
}
