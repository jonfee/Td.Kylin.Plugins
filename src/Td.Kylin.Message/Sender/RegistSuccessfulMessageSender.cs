using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Services;

namespace Td.Kylin.Message.Sender
{
    /// <summary>
    /// 注册成功消息发送器
    /// </summary>
    public class RegistSuccessfulMessageSender : BaseSender
    {
        #region 属性

        /// <summary>
        /// 注册者ID
        /// </summary>
        private readonly long _registratorId;

        /// <summary>
        /// 注册者身份
        /// </summary>
        private readonly IdentityType _registratorType;

        #endregion

        /// <summary>
        /// 初始化消息发送器
        /// </summary>
        /// <param name="registratorId">注册者ID</param>
        /// <param name="registratorType"><seealso cref="IdentityType"/>注册者身份类型</param>
        public RegistSuccessfulMessageSender(long registratorId, IdentityType registratorType) : base(MessageTemplateOption.RegistrationSuccessful)
        {
            _registratorId = registratorId;

            _registratorType = registratorType;
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
                    success = new MessageService().AddUserMessage(_registratorId, Option, _registratorId.ToString(), Title, Content, "");
                    break;
                case IdentityType.Merchant:
                    success = new MessageService().AddMerchantMessage(_registratorId, Option, _registratorId.ToString(), Title, Content, "");
                    break;
                case IdentityType.Worker:
                    success = new MessageService().AddWorkerMessage(_registratorId, Option, _registratorId.ToString(), Title, Content, "");
                    break;
            }

            return success;
        }
    }
}
