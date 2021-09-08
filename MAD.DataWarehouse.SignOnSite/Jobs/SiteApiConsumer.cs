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
    public class SiteApiConsumer
    {
        private readonly SignOnSiteApiClient apiClient;
        private readonly SignOnSiteDbContext signOnSiteDbContext;
        private readonly IBackgroundJobClient backgroundJobClient;

        public SiteApiConsumer(SignOnSiteApiClient apiClient, SignOnSiteDbContext signOnSiteDbContext, IBackgroundJobClient backgroundJobClient)
        {
            this.apiClient = apiClient;
            this.signOnSiteDbContext = signOnSiteDbContext;
            this.backgroundJobClient = backgroundJobClient;
        }

        public async Task GetSites(int offset = 0)
        {
            var apiResult = await this.apiClient.GetSites(limit: AppConfig.ApiPageSize, offset: 0);
            var sites = apiResult.Data.ToList();

            await this.signOnSiteDbContext.BulkInsertOrUpdateAsync(sites);

            this.EnqueueNextPage(offset, apiResult);
            this.EnqueueRelatedJobs(sites);
        }

        private void EnqueueRelatedJobs(IEnumerable<Site> sites)
        {
            foreach (var s in sites)
            {
                this.backgroundJobClient.Enqueue<SiteAttendanceApiConsumer>(f => f.GetSiteAttendances(s.Id, 0));
                this.backgroundJobClient.Enqueue<SiteBriefingsWebApiConsumer>(f => f.GetSiteBriefings(s.Id, 0));
            }
        }

        private void EnqueueNextPage(int offset, PaginatedApiResponse<Site> apiResult)
        {
            if (apiResult.LastPage == apiResult.CurrentPage)
                return;

            this.backgroundJobClient.Enqueue<SiteApiConsumer>(s => s.GetSites(offset + AppConfig.ApiPageSize));
        }
    }
}
