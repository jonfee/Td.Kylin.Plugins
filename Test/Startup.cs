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

            string sqlConn = "data source=139.129.194.132;initial catalog=Kylin_Test;user id=kylintest;password=kylintest++;MultipleActiveResultSets=True;";

            MessageSenderExtensions.Factory(
                connectionString: sqlConn,
                sqlType: Td.Kylin.EnumLibrary.SqlProviderType.SqlServer,
               options: new DataCacheServerOptions
               {
                   KeepAlive = true,
                   CacheItems = null,
                   RedisConnectionString = "139.129.194.132:6399,abortConnect=false,password=kylinjonfee++",
                   InitIfNull = false,
                   SqlType = Td.Kylin.EnumLibrary.SqlProviderType.SqlServer,
                   SqlConnection = sqlConn,
                   Level2CacheSeconds = 600
               });
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
