using GenericHostDemo.Common.QuartzExtension;
using GenericHostDemo.Jobs;
using GenericHostDemo.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using NLog.Extensions.Hosting;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Threading.Tasks;

namespace GenericHostDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            try
            {
                var builder = new HostBuilder()
                //Host config
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.AddEnvironmentVariables(prefix: "ASPNETCORE_");
                    if (args != null)
                    {
                        configHost.AddCommandLine(args);
                    }
                })
                //App config
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.AddJsonFile("appsettings.json", optional: true);
                    configApp.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
                    configApp.AddEnvironmentVariables();
                    if (args != null)
                    {
                        configApp.AddCommandLine(args);
                    }
                })
                //Services
                .ConfigureServices((hostContext, services) =>
                {
                    //EF SQL Server
                    var connectionString = hostContext.Configuration["sqlserverconnection"];
                    services.AddDbContext<HostDemoContext>(o => o.UseSqlServer(connectionString), ServiceLifetime.Singleton);

                    //Rabbit MQ
                    string rabbitMqConnection = hostContext.Configuration["rabbitmqconnection"];
                    services.RegisterEasyNetQ(rabbitMqConnection);

                    //Quartz
                    services.AddSingleton<IJobFactory, JobFactory>();
                    services.AddSingleton(provider =>
                    {
                        var option = new QuartzOption(hostContext.Configuration);
                        var sf = new StdSchedulerFactory(option.ToProperties());
                        var scheduler = sf.GetScheduler().Result;
                        scheduler.JobFactory = provider.GetService<IJobFactory>();
                        return scheduler;
                    });
                    services.AddHostedService<QuartzService>();

                    //Hosted Services
                    services.AddHostedService<TimedHostedService>();
                    services.AddHostedService<RabbitMQDemoHostedService>();

                    //Jobs
                    services.AddSingleton<TestJob>();
                })
                //Log
                .UseNLog();

                await builder.RunConsoleAsync();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }
        }
    }
}
