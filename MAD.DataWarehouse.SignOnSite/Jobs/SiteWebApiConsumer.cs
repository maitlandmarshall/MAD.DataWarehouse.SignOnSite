using EFCore.BulkExtensions;
using Hangfire;
using MAD.DataWarehouse.SignOnSite.Api;
using MAD.DataWarehouse.SignOnSite.Data;
using MAD.Extensions.EFCore;
using System.Linq;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.SignOnSite.Jobs
{
    public class SiteWebApiConsumer
    {
        private readonly SignOnSiteWebApiClient signOnSiteWebApiClient;
        private readonly SignOnSiteDbContext signOnSiteDbContext;
        private readonly IBackgroundJobClient backgroundJobClient;
        private readonly AppConfig appConfig;

        public SiteWebApiConsumer(SignOnSiteWebApiClient signOnSiteWebApiClient, SignOnSiteDbContext signOnSiteDbContext, IBackgroundJobClient backgroundJobClient, AppConfig appConfig)
        {
            this.signOnSiteWebApiClient = signOnSiteWebApiClient;
            this.signOnSiteDbContext = signOnSiteDbContext;
            this.backgroundJobClient = backgroundJobClient;
            this.appConfig = appConfig;
        }

        public async Task GetUserSites()
        {
            await this.signOnSiteWebApiClient.Login(this.appConfig.Email, this.appConfig.Password);
            var userSites = await this.signOnSiteWebApiClient.GetUserSites(this.appConfig.UserId);
            var sites = userSites.Sites.Select(y => new Site
            {
                Id = y.Id,
                InternalReference = y.JobRef,
                IsActive = y.IsActive,
                Name = y.Name
            }).ToList();

            await this.signOnSiteDbContext.BulkInsertOrUpdateAsync(sites);

            foreach (var s in sites)
            {
                this.backgroundJobClient.Enqueue<SiteWebApiConsumer>(f => f.GetSiteUsers(s.Id));
                this.backgroundJobClient.Enqueue<SiteBriefingsWebApiConsumer>(f => f.GetSiteBriefings(s.Id, 0));
                this.backgroundJobClient.Enqueue<SiteAttendeesWebApiConsumer>(f => f.GetSiteAttendees(s.Id));
            }
        }

        public async Task GetSiteUsers(int siteId)
        {
            await this.signOnSiteWebApiClient.Login(this.appConfig.Email, this.appConfig.Password);
            var siteUsersResponse = await this.signOnSiteWebApiClient.GetSiteUsers(siteId);
            var siteUsers = siteUsersResponse.Users.ToList();

            foreach (var x in siteUsers)
            {
                x.SiteId = siteId;
                this.signOnSiteDbContext.Upsert(x);
            }

            await this.signOnSiteDbContext.SaveChangesAsync();
        }
    }
}
