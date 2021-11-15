using MAD.DataWarehouse.SignOnSite.Api;
using MAD.DataWarehouse.SignOnSite.Data;
using MAD.Integration.Common.EFCore;
using MAD.Integration.Common.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.SignOnSite.Jobs
{
    public class SiteAttendeesWebApiConsumer
    {
        private readonly SignOnSiteDbContext dbContext;
        private readonly SignOnSiteWebApiClient apiClient;
        private readonly AppConfig appConfig;

        public SiteAttendeesWebApiConsumer(SignOnSiteDbContext dbContext, SignOnSiteWebApiClient apiClient, AppConfig appConfig)
        {
            this.dbContext = dbContext;
            this.apiClient = apiClient;
            this.appConfig = appConfig;
        }

        [TrackLastSuccess]
        public async Task GetSiteAttendees(int siteId)
        {
            await this.apiClient.Login(this.appConfig.Email, this.appConfig.Password);

            var utcNow = DateTime.UtcNow;
            var maxDate = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, 23, 59, 59, 999, DateTimeKind.Utc);
            var lastSuccess = BackgroundJobContext.Current.BackgroundJob.GetLastSuccess();

            DateTime minDate;

            if (lastSuccess.HasValue)
            {
                // Trail the last success by a couple days for timezone issues
                minDate = lastSuccess.Value.AddDays(-3);

                // Ensure the date has 00:00:00 for the time component
                minDate = new DateTime(minDate.Year, minDate.Month, minDate.Day);
            }
            else
            {
                minDate = new DateTime(2019, 01, 01);
            }

            var response = await this.apiClient.GetSiteAttendees(siteId, minDate, maxDate);

            foreach (var a in response.Attendees)
            {
                this.dbContext.Entry(a).Property("SiteId").CurrentValue = siteId;

                this.dbContext.Upsert(a, entity =>
                {
                    switch (entity)
                    {
                        case SiteAttendeeAttendance _:
                            this.dbContext.Entry(entity).Property("SiteAttendeeId").CurrentValue = a.Id;
                            this.dbContext.Entry(entity).Property("SiteId").CurrentValue = siteId;
                            break;
                    }
                });
            }

            await this.dbContext.SaveChangesAsync();
        }
    }
}
