using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Td.Kylin.EnumLibrary;
using Td.Kylin.SMS.Core;

namespace Td.Kylin.SMS.Sender
{
    /// <summary>
    /// 短信发送抽象类
    /// </summary>
    public abstract class BaseSender
    {
        protected BaseSender(SmsTemplateOption option)
        {
            Option = option;

            Template = CacheData.GetTemplate(option);//"【天道新创】验证码为#Code#（切勿告知他人）";

            Content = Template ?? string.Empty;
        }

        #region ///////属性////////////////

        /// <summary>
        /// 消息模板
        /// </summary>
        public string Template { get; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public SmsTemplateOption Option { get; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; private set; }

        #endregion

        /// <summary>
        /// 消息内容生成
        /// </summary>
        /// <param name="parameterObject">匿名对象（如：new {ID=12312,Name="我是名称"}）
        /// <para>对象中的属性名称对应消息模板中的占位符</para>
        /// <para>对象中的属性值对应替换消息模板中的占位符</para>
        /// </param>
        protected virtual void ContentFactory(object parameterObject)
        {
            if (parameterObject != null)
            {
                try
                {
                    var properties = parameterObject.GetType().GetProperties();

                    foreach (var propery in properties)
                    {
                        string splitTag = string.Format(@"#{0}#", propery.Name);

                        var perperyValue = propery.GetValue(parameterObject) == null ? "" : propery.GetValue(parameterObject).ToString();

                        Content = Content.Replace(splitTag, perperyValue);
                    }
                }
                catch
                {
                    Content = Template;
                }
            }
        }

        /// <summary>
        /// 消息发送
        /// </summary>
        /// <returns></returns>
        public abstract Task<bool> SendAsync();


    }
}
