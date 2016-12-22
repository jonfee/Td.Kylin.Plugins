using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using Td.Kylin.SMS.ApiResult;

namespace Td.Kylin.SMS.Provider
{
    /// <summary>
    /// 短信发送接口
    /// </summary>
     interface ISendProvider
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mobile">手机号，多个手机号用逗号分隔</param>
        /// <param name="message">短信内容</param>
        /// <param name="uid">业务ID</param>
        /// <returns></returns>
        Task<SmsSendResult> SendSmsAsync(string mobile, string message, string uid);

        /// <summary>
        /// 多手机号发送
        /// </summary>
        /// <param name="mobiles">手机号群</param>
        /// <param name="message">短信内容</param>
        /// <param name="uid">业务ID</param>
        /// <returns></returns>
        Task<SmsSendResult> SendSmsAsync(IEnumerable<string> mobiles, string message, string uid);
    }
}
