using Hangfire;
using MAD.DataWarehouse.SignOnSite.Data;
using MAD.DataWarehouse.SignOnSite.Jobs;
using MAD.Integration.Common;
using MAD.Integration.Common.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MAD.DataWarehouse.SignOnSite.Tests")]
namespace MAD.DataWarehouse.SignOnSite
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            MigrateAndStartRecurringJobs(host.Services);
            host.Run();
        }

        private static void MigrateAndStartRecurringJobs(IServiceProvider services)
        {
            var dbContext = services.GetRequiredService<SignOnSiteDbContext>();
            var recurringJobManager = services.GetRequiredService<IRecurringJobManager>();

            dbContext.Database.Migrate();

            JobFactory.CreateRecurringJob<SiteApiConsumer>("GetSites", y => y.GetSites(0), Cron.Daily());
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            IntegrationHost.CreateDefaultBuilder(args)
                .UseAspNetCore()
                .UseAppInsights()
                .UseStartup<Startup>();
    }
}
