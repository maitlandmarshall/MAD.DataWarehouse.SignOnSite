using Hangfire;
using MAD.DataWarehouse.SignOnSite.Api;
using MAD.DataWarehouse.SignOnSite.Data;
using MAD.DataWarehouse.SignOnSite.Jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MIFCore.Hangfire;
using MIFCore.Settings;
using System;

namespace MAD.DataWarehouse.SignOnSite
{
    internal class Startup
    {
        public void ConfigureServices(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddIntegrationSettings<AppConfig>();

            serviceDescriptors
                .AddHttpClient<SignOnSiteWebApiClient>((svc, httpClient) =>
                {
                    var appConfig = svc.GetRequiredService<AppConfig>();
                    httpClient.BaseAddress = new Uri("https://app.signonsite.com.au");
                });

            serviceDescriptors.AddDbContext<SignOnSiteDbContext>((svc, opt) => opt.UseSqlServer(svc.GetRequiredService<AppConfig>().ConnectionString));

            serviceDescriptors.AddScoped<SiteWebApiConsumer>();
            serviceDescriptors.AddScoped<SiteBriefingsWebApiConsumer>();
            serviceDescriptors.AddScoped<SiteAttendeesWebApiConsumer>();
        }

        public void PostConfigure(SignOnSiteDbContext dbContext, IRecurringJobManager recurringJobFactory)
        {
            dbContext.Database.Migrate();
            recurringJobFactory.CreateRecurringJob<SiteWebApiConsumer>("GetSites", y => y.GetUserSites(), Cron.Daily());
        }
    }
}