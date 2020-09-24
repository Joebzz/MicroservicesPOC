using Coravel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace TestScheduledTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            host.Services.UseScheduler(scheduler =>
            {
                // Yes, it's this easy!
                scheduler
                    .Schedule<MyFirstInvocable>()
                    .DailyAt(11, 01)
                    .Zoned(TimeZoneInfo.Local);
            });
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddScheduler();
                    services.AddTransient<MyFirstInvocable>();
                });
    }
}
