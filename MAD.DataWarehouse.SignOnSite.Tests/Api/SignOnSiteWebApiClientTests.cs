using Microsoft.VisualStudio.TestTools.UnitTesting;
using MAD.DataWarehouse.SignOnSite.Api;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.SignOnSite.Api.Tests
{
    [TestClass()]
    public class SignOnSiteWebApiClientTests
    {
        [TestMethod()]
        public async Task LoginTest()
        {
            var startup = new Startup();
            var serviceCollection = new ServiceCollection();
            startup.ConfigureServices(serviceCollection);

            var services = serviceCollection.BuildServiceProvider();
            var appConfig = services.GetRequiredService<AppConfig>();
            var api = services.GetRequiredService<SignOnSiteWebApiClient>();

            await api.Login(appConfig.Email, appConfig.Password);
            var briefings = await api.GetSiteBriefings(13096);
        }
    }
}