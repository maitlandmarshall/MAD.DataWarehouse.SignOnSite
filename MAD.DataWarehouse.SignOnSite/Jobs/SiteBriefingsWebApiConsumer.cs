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
    public class SiteBriefingsWebApiConsumer
    {
        private readonly SignOnSiteWebApiClient signOnSiteWebApiClient;
        private readonly AppConfig appConfig;
        private readonly SignOnSiteDbContext dbContext;
        private readonly IBackgroundJobClient backgroundJobClient;

        public SiteBriefingsWebApiConsumer(SignOnSiteWebApiClient signOnSiteWebApiClient, AppConfig appConfig, SignOnSiteDbContext dbContext, IBackgroundJobClient backgroundJobClient)
        {
            this.signOnSiteWebApiClient = signOnSiteWebApiClient;
            this.appConfig = appConfig;
            this.dbContext = dbContext;
            this.backgroundJobClient = backgroundJobClient;
        }

        public async Task GetSiteBriefings(int siteId, int offset = 0)
        {
            await this.signOnSiteWebApiClient.Login(this.appConfig.Email, this.appConfig.Password);

            var result = await this.signOnSiteWebApiClient.GetSiteBriefings(siteId, AppConfig.ApiPageSize, offset);
            var briefings = result.Data.ToList();

            foreach (var a in briefings)
            {
                a.SiteId = siteId;
            }

            await this.dbContext.BulkInsertOrUpdateAsync(briefings, new BulkConfig
            {
                EnableShadowProperties = true
            });

            foreach (var a in briefings)
            {
                if (a.CurrentStatus == "future")
                    continue;

                this.backgroundJobClient.Enqueue<SiteBriefingsWebApiConsumer>(f => f.GetSiteBriefingLogs(a.Id));
            }

            if (result.CurrentPage == result.LastPage)
                return;

            this.backgroundJobClient.Enqueue<SiteBriefingsWebApiConsumer>(f => f.GetSiteBriefings(siteId, offset + AppConfig.ApiPageSize));
        }

        public async Task GetSiteBriefingLogs(int briefingId)
        {
            await this.signOnSiteWebApiClient.Login(this.appConfig.Email, this.appConfig.Password);

            var result = await this.signOnSiteWebApiClient.GetBriefingLogs(briefingId);
            var logs = result.ActiveDays
                .SelectMany(y => y.Users)
                .Where(y => y.EarliestAcknowledgedAt.HasValue)
                .ToList();

            foreach (var l in logs)
            {
                l.BriefingId = briefingId;
            }

            await this.dbContext.BulkInsertOrUpdateAsync(logs, new BulkConfig
            {
                EnableShadowProperties = true
            });
        }
    }
}
