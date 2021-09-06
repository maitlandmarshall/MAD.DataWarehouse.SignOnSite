using EFCore.BulkExtensions;
using Hangfire;
using MAD.DataWarehouse.SignOnSite.Api;
using MAD.DataWarehouse.SignOnSite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.SignOnSite.Jobs
{
    public class SiteAttendanceApiConsumer
    {
        private readonly SignOnSiteApiClient signOnSiteApiClient;
        private readonly SignOnSiteDbContext dbContext;
        private readonly IBackgroundJobClient backgroundJobClient;

        public SiteAttendanceApiConsumer(SignOnSiteApiClient signOnSiteApiClient, SignOnSiteDbContext dbContext, IBackgroundJobClient backgroundJobClient)
        {
            this.signOnSiteApiClient = signOnSiteApiClient;
            this.dbContext = dbContext;
            this.backgroundJobClient = backgroundJobClient;
        }

        public async Task GetSiteAttendances(int siteId, int offset = 0)
        {
            var result = await this.signOnSiteApiClient.GetSiteAttendances(siteId, AppConfig.ApiPageSize, offset);
            var attendances = result.Data.ToList();
            
            foreach (var a in attendances)
            {
                a.SiteId = siteId;
            }

            await this.dbContext.BulkInsertOrUpdateAsync(attendances, new BulkConfig
            {
                EnableShadowProperties = true
            });

            if (result.CurrentPage == result.LastPage)
                return;

            this.backgroundJobClient.Enqueue<SiteAttendanceApiConsumer>(f => f.GetSiteAttendances(siteId, offset + AppConfig.ApiPageSize));
        }
    }
}
