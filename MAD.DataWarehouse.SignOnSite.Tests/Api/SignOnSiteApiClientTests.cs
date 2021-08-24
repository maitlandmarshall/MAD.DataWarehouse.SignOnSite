using Microsoft.VisualStudio.TestTools.UnitTesting;
using MAD.DataWarehouse.SignOnSite.Api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace MAD.DataWarehouse.SignOnSite.Api.Tests
{
    [TestClass]
    public class SignOnSiteApiClientTests
    {
        [TestMethod]
        public async Task GetSites_Ok()
        {
            var startup = new Startup();
            var serviceCollection = new ServiceCollection();
            startup.ConfigureServices(serviceCollection);

            var services = serviceCollection.BuildServiceProvider();
            var api = services.GetRequiredService<SignOnSiteApiClient>();

            var sites = await api.GetSites(10, 0);
        }
    }
}