using Hangfire;
using MAD.DataWarehouse.SignOnSite.Api;
using MAD.Integration.Common.Settings;
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
        }

        public async Task Configure(IGlobalConfiguration hangfireConfig)
        {

        }
    }
}