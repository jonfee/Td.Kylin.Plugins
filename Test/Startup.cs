using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Td.Kylin.Message;
using Td.Kylin.DataCache;
using Td.Kylin.SMS;
using Td.Kylin.SMS.Config;

namespace Test
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            //data source=139.129.194.132;initial catalog=Kylin_Test;user id=kylintest;password=kylintest++;MultipleActiveResultSets=True;
            string sqlConn = "data source=192.168.1.200;initial catalog=KylinTest;user id=sa;password=sql100200;MultipleActiveResultSets=True;";

            string redisConn = "139.129.194.132:6399,abortConnect=false,password=kylinjonfee++";

            MessageSenderExtensions.Factory(
                connectionString: sqlConn,
                sqlType: Td.Kylin.EnumLibrary.SqlProviderType.SqlServer,
                redisOptions: redisConn,
                cacheItems: null,
                keepAlive: true,
                level2CacheSeconds: 600);

            SMSSenderExtensions.Factory(SmsProviderType.YunPian, new YuanPianConfig
            {
                ApiKey= "665acd4512ee858910f0f06bcf264621",
                ApiUrl= "http://yunpian.com/v1/sms/send.json"
            }, sqlConn, Td.Kylin.EnumLibrary.SqlProviderType.SqlServer, redisConn, true, null, 600);
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseMvc();
        }
    }
}
