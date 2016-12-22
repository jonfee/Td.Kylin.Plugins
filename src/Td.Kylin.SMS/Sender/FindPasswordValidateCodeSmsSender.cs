using System.Threading.Tasks;
using Td.Kylin.EnumLibrary;

namespace Td.Kylin.SMS.Sender
{
    /// <summary>
    /// 找回密码验证码消息发送器
    /// </summary>
    public class FindPasswordValidateCodeSmsSender : BaseSender
    {
        /// <summary>
        /// 注册手机号
        /// </summary>
        private readonly string _mobile;

        /// <summary>
        /// 初始化找回密码验证码发送器实例
        /// </summary>
        /// <param name="mobile">注册的手机号</param>
        /// <param name="code">验证码</param>
        /// <param name="minutes">验证码有效期（单位：分钟）</param>
        public FindPasswordValidateCodeSmsSender(string mobile, string code, int minutes) : base(SmsTemplateOption.FindPasswordValidateCode)
        {
            _mobile = mobile;

            base.ContentFactory(new { Code = code, Minutes = minutes });
        }

        public override async Task<bool> SendAsync()
        {
            var result = await base.SendSms(IdentityType.Platform, 0, new[] { _mobile }, _mobile);

            return result.IsSuccess;
        }
    }
}
