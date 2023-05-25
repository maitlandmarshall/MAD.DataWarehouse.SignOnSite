using Hangfire;
using MAD.DataWarehouse.SignOnSite.Api;
using MAD.DataWarehouse.SignOnSite.Data;
using MAD.Extensions.EFCore;
using System.Linq;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.SignOnSite.Jobs
{
    public class SiteInductionsWebApiConsumer
    {
        private readonly SignOnSiteWebApiClient signOnSiteWebApiClient;
        private readonly AppConfig appConfig;
        private readonly SignOnSiteDbContext dbContext;
        private readonly IBackgroundJobClient backgroundJobClient;

        public SiteInductionsWebApiConsumer(SignOnSiteWebApiClient signOnSiteWebApiClient, AppConfig appConfig, SignOnSiteDbContext dbContext, IBackgroundJobClient backgroundJobClient)
        {
            this.signOnSiteWebApiClient = signOnSiteWebApiClient;
            this.appConfig = appConfig;
            this.dbContext = dbContext;
            this.backgroundJobClient = backgroundJobClient;
        }

        public async Task GetSiteInductions(int siteId, int offset = 0)
        {
            await this.signOnSiteWebApiClient.Login(this.appConfig.Email, this.appConfig.Password);

            var result = await this.signOnSiteWebApiClient.GetSiteInductions(siteId, AppConfig.ApiPageSize, offset);
            var inductions = result.Data.ToList();

            foreach (var ind in inductions)
            {
                this.dbContext.Entry(ind).Property("SiteId").CurrentValue = siteId;
                this.dbContext.Upsert(ind);
            }

            await this.dbContext.SaveChangesAsync();

            if (result.CurrentPage == result.LastPage)
                return;

            this.backgroundJobClient.Enqueue<SiteBriefingsWebApiConsumer>(f => f.GetSiteBriefings(siteId, offset + AppConfig.ApiPageSize));
        }
    }
}
