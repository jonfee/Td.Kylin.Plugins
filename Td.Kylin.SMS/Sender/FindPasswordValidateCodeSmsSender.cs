using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Td.Kylin.Entity;
using Td.Kylin.EnumLibrary;
using Td.Kylin.SMS.Core;
using Td.Kylin.SMS.Services;

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
        public FindPasswordValidateCodeSmsSender(string mobile, string code) : base(SmsTemplateOption.FindPasswordValidateCode)
        {
            _mobile = mobile;

            base.ContentFactory(new { Code = code });
        }

        public override async Task<bool> SendAsync()
        {
            var result = await ConfigRoot.SendProvider.SendSmsAsync(_mobile, Content, _mobile);

            bool success = result.IsSuccess;

            SmsSendRecords record = new SmsSendRecords
            {
                IsSuccess = success,
                Message = Content,
                Mobile = _mobile,
                Remark = result.Remark,
                SenderId = 0,
                SenderType = (int)IdentityType.Platform,
                SmsType = (int)SmsTemplateOption.FindPasswordValidateCode,
                SendID = IDProvider.NewId(),
                SendTime = DateTime.Now
            };

            new SmsSendRecordsService().AddRecord(record);

            return success;
        }
    }
}
