using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Td.Kylin.SMS.ApiResult;
using Td.Kylin.SMS.Config;
using Td.Kylin.SMS.Core;
using Td.Kylin.SMS.Sender;

namespace Td.Kylin.SMS.Provider
{

    /// <summary>
    /// 云片短信服务
    /// </summary>
    class YunPianProvider : ISendProvider
    {
        #region 云片短信接口参数

        /// <summary>
        /// 通用接口发短信的http地址
        /// </summary>
        private string URI_SEND_SMS;

        /// <summary>
        /// API KEY
        /// </summary>
        private string Apikey;

        #endregion

        public YunPianProvider(YuanPianConfig config)
        {
            if (config == null) throw new InvalidOperationException("云片短信发送接口配置信息异常");

            Apikey = config.ApiKey;

            URI_SEND_SMS = config.ApiUrl;
        }

        public async Task<SmsSendResult> SendSmsAsync(IEnumerable<string> mobile, string message, string uid)
        {
            string mobiles = string.Join(",", mobile);

            return await SendAsync(mobiles, message, uid);
        }

        public async Task<SmsSendResult> SendSmsAsync(string mobile, string message, string uid)
        {
            return await SendAsync(mobile, message, uid);
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="mobiles"></param>
        /// <param name="message"></param>
        /// <param name="uid">业务ID</param>
        /// <returns></returns>
        private async Task<YunPianResult> SendAsync(string mobiles, string message, string uid)
        {
            YunPianResult result = null;

            if (string.IsNullOrWhiteSpace(uid)) uid = "10000";

            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("apikey", Apikey);
            parameters.Add("text", message ?? string.Empty);
            parameters.Add("mobile", mobiles);
            parameters.Add("uid", uid);

            HttpContent content = new FormUrlEncodedContent(parameters);

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(URI_SEND_SMS, content);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    try
                    {
                        result = JsonConvert.DeserializeObject<YunPianResult>(data);
                    }
                    catch { }
                }
            }

            if (result == null)
            {
                result = new YunPianResult
                {
                    Code = 1,
                    Msg = "发送失败",
                    Detail = "未知的错误"
                };
            }

            return result;
        }
    }
}
