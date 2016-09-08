namespace Td.Kylin.SMS.ApiResult
{
    /// <summary>
    /// 短信发送结果抽象类
    /// </summary>
    public abstract class SmsSendResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess
        {
            get
            {
                return GetStatus();
            }
        }

        /// <summary>
        /// 消息说明
        /// </summary>
        public string Remark
        {
            get { return GetRemark(); }
        }

        /// <summary>
        /// 获取成功状态
        /// </summary>
        /// <returns></returns>
        protected abstract bool GetStatus();

        /// <summary>
        /// 获取消息说明
        /// </summary>
        /// <returns></returns>
        protected abstract string GetRemark();
    }
}
