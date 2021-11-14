using Hangfire;
using MAD.DataWarehouse.SignOnSite.Api;
using MAD.DataWarehouse.SignOnSite.Data;
using MAD.DataWarehouse.SignOnSite.Jobs;
using MAD.Integration.Common.Jobs;
using MAD.Integration.Common.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.SignOnSite
{
    internal class Startup
    {
        public void ConfigureServices(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddIntegrationSettings<AppConfig>();

            serviceDescriptors.AddHttpClient<SignOnSiteApiClient>((svc, httpClient) =>
            {
                var appConfig = svc.GetRequiredService<AppConfig>();
                httpClient.BaseAddress = new Uri("https://app.signonsite.com.au");
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", appConfig.SignOnSiteKey);
            });

            serviceDescriptors.AddHttpClient<SignOnSiteWebApiClient>((svc, httpClient) =>
            {
                var appConfig = svc.GetRequiredService<AppConfig>();
                httpClient.BaseAddress = new Uri("https://app.signonsite.com.au");
            });

            serviceDescriptors.AddDbContext<SignOnSiteDbContext>((svc, opt) => opt.UseSqlServer(svc.GetRequiredService<AppConfig>().ConnectionString));

            serviceDescriptors.AddScoped<SiteWebApiConsumer>();
            serviceDescriptors.AddScoped<SiteBriefingsWebApiConsumer>();
        }

        public void PostConfigure(SignOnSiteDbContext dbContext, IRecurringJobFactory recurringJobFactory)
        {
            dbContext.Database.Migrate();
            recurringJobFactory.CreateRecurringJob<SiteWebApiConsumer>("GetSites", y => y.GetUserSites(), Cron.Daily());
        }
    }
}