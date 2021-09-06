using Hangfire;
using MAD.DataWarehouse.SignOnSite.Api;
using MAD.DataWarehouse.SignOnSite.Data;
using MAD.DataWarehouse.SignOnSite.Jobs;
using MAD.Integration.Common.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.SignOnSite
{
    internal class Startup
    {
        public void ConfigureServices(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddIntegrationSettings<AppConfig>();
            serviceDescriptors.AddHttpClient<SignOnSiteApiClient>();

            serviceDescriptors.AddDbContext<SignOnSiteDbContext>((svc, opt) => opt.UseSqlServer(svc.GetRequiredService<AppConfig>().ConnectionString));

            serviceDescriptors.AddScoped<SiteApiConsumer>();
            serviceDescriptors.AddScoped<SiteAttendanceApiConsumer>();
        }

        public async Task Configure()
        {

        }
    }
}