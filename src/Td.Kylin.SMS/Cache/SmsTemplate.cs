using Td.Kylin.EnumLibrary;

namespace Td.Kylin.SMS.Cache
{
    /// <summary>
    /// 短信模板
    /// </summary>
     class SmsTemplate
    {
        /// <summary>
        /// 模板业务项
        /// </summary>
        public SmsTemplateOption Option { get; set; }

        /// <summary>
        /// 模板内容
        /// </summary>
        public string Template { get; set; }
    }
}
