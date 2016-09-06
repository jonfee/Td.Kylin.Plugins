using System.Collections.Generic;
using Td.Kylin.EnumLibrary;
using Td.Kylin.Message.Core;
using System;
using System.Reflection;

namespace Td.Kylin.Message.Sender
{
    /// <summary>
    /// 系统消息发送抽象类
    /// </summary>
    public abstract class SysMessageSender
    {
        /// <summary>
        /// 消息发送器初始化
        /// </summary>
        /// <param name="option">消息业务类型，<see cref="MessageTemplateOption"/>枚举</param>
        protected SysMessageSender(MessageTemplateOption option)
        {
            Option = option;
            
            var config = CacheData.GetTemplate(option);

            if (config == null) throw new InvalidOperationException("未找到当前业务对应的消息模板配置项");
            
            Title = config.Title;

            Template = config.Template;

            Content = Template ?? string.Empty;
        }

        #region ///////属性////////////////
        /// <summary>
        /// 消息模板
        /// </summary>
        public string Template { get;}

        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageTemplateOption Option { get;}

        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get;}

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; private set; }

        #endregion

        /// <summary>
        /// 消息内容生成
        /// </summary>
        /// <param name="paramsValues">消息模板中占位符与对应的参数值</param>
        protected virtual void ContentFactory(Dictionary<string, string> paramsValues)
        {
            if (paramsValues != null && paramsValues.Count > 0)
            {
                try
                {
                    foreach (var kv in paramsValues)
                    {
                        string splitTag = string.Format(@"#{0}#", kv.Key);
                        Content = Content.Replace(splitTag, kv.Value);
                    }
                }
                catch
                {
                    Content = Template;
                }
            }
        }

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

                        var perperyValue = propery.GetValue(parameterObject) as string;

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
        public abstract bool Send();
    }
}
