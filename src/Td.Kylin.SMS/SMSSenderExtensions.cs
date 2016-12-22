using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Td.Kylin.EnumLibrary;
using Td.Kylin.SMS.Config;
using Td.Kylin.SMS.Provider;

namespace Td.Kylin.SMS
{
    public class Middleware
    {
        private readonly RequestDelegate _next;
        private readonly MiddlewareOptions _options;

        public Middleware(RequestDelegate next, MiddlewareOptions options)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _next = next;
            _options = options;
        }

        public Task Invoke(HttpContext httpContext)
        {
            MiddlewareConfig.Options = _options;
            return _next(httpContext);
        }
    }


    public static class MiddlewareExtensions
    {
        /// <summary>
        /// 使用SMS Sender组件
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"><seealso cref="MiddlewareOptions"/>
        /// <returns></returns>
        public static IApplicationBuilder UseSMSSenderMiddleware(this IApplicationBuilder builder, MiddlewareOptions options)
        {
            return builder.Use(next => new Middleware(next, options).Invoke);
        }

        /// <summary>
        /// 使用SMS Sender组件
        /// </summary>
        /// <param name="providerType"><seealso cref="SmsProviderType"/></param>
        /// <param name="smsConfig"><seealso cref="SmsConfig"/>配置信息，根据providerType参数值配置对应的信息</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlType">数据库类型</param>
        /// <returns></returns>
        public static IApplicationBuilder UseSMSSenderMiddleware(this IApplicationBuilder builder, SmsProviderType providerType, SmsConfig smsConfig, string connectionString, SqlProviderType sqlType)
        {
            var option = new MiddlewareOptions();

            switch (providerType)
            {
                case SmsProviderType.YunPian:
                    option.SendProvider = new YunPianProvider(smsConfig as YuanPianConfig);
                    break;
            }

            option.SqlConnectionString = connectionString;

            option.SqlType = sqlType;

            return UseSMSSenderMiddleware(builder, option);
        }

        /// <summary>
        /// 使用SMS Sender组件
        /// </summary>
        /// <param name="providerType"><seealso cref="SmsProviderType"/></param>
        /// <param name="smsConfig"><seealso cref="SmsConfig"/>配置信息，根据providerType参数值配置对应的信息</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="sqlType">数据库类型</param>
        public static void Factory( SmsProviderType providerType, SmsConfig smsConfig, string connectionString, SqlProviderType sqlType)
        {
            var option = new MiddlewareOptions();

            switch (providerType)
            {
                case SmsProviderType.YunPian:
                    option.SendProvider = new YunPianProvider(smsConfig as YuanPianConfig);
                    break;
            }

            option.SqlConnectionString = connectionString;

            option.SqlType = sqlType;

            MiddlewareConfig.Options = option;
        }

    }

    public class MiddlewareOptions
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        internal string SqlConnectionString;

        /// <summary>
        /// 数据库类型，<see cref="SqlProviderType"/>枚举
        /// </summary>
        internal SqlProviderType SqlType;

        /// <summary>
        /// 短信发送服务
        /// </summary>
        internal ISendProvider SendProvider;
    }

    internal class MiddlewareConfig
    {
        internal static MiddlewareOptions Options { get; set; }
    }
}
