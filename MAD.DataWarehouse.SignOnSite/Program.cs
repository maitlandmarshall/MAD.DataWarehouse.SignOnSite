using Microsoft.Extensions.Hosting;
using MIFCore;
using MIFCore.Http;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MAD.DataWarehouse.SignOnSite.Tests")]
namespace MAD.DataWarehouse.SignOnSite
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            IntegrationHost.CreateDefaultBuilder(args)
                .UseAspNetCore()
                .UseStartup<Startup>();
    }
}
