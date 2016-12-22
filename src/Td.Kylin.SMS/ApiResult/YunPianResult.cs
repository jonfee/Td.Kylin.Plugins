using System;

namespace Td.Kylin.SMS.ApiResult
{
    /// <summary>
    /// 云片短信发送返回结果
    /// </summary>
    class YunPianResult: SmsSendResult
    {
        /// <summary>
        /// Code 错误码
        /// <remarks>code = 0:	正确返回</remarks>
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 具体错误描述或解决方法
        /// </summary>
        public string Detail { get; set; }

        protected override bool GetStatus()
        {
            return Code == 0;
        }

        protected override string GetRemark()
        {
            return Msg;
        }
    }
}
