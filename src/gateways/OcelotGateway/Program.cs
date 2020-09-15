using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace OcelotGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile($"configuration.json");

                    // for local test use below one, multi env json file not worked
                    //config.AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true);
                    // https://github.com/ThreeMammals/Ocelot/issues/249
                })
                .UseSerilog((_, config) =>
                {
                    config
                        .MinimumLevel.Information()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .Enrich.FromLogContext()
                        .WriteTo.File(@"Logs\log.txt", rollingInterval: RollingInterval.Day);
                });
    }
}
