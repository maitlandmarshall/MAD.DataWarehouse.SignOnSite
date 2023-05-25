using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MAD.DataWarehouse.SignOnSite.Api
{
    public class SignOnSiteWebApiClient
    {
        private readonly HttpClient httpClient;

        public SignOnSiteWebApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task Login(string email, string password)
        {
            var formContent = new MultipartFormDataContent();
            formContent.Add(new StringContent(email), nameof(email));
            formContent.Add(new StringContent(password), nameof(password));

            await this.httpClient.PostAsync("/login", formContent);
        }

        public async Task<UserSitesApiResponse> GetUserSites(int userId)
        {
            var endpoint = $"/web/api/v1/users/{userId}/sites";
            var responseJson = await this.httpClient.GetStringAsync(endpoint);
            var response = JsonConvert.DeserializeObject<UserSitesApiResponse>(responseJson);

            return response;
        }

        public async Task<SiteUserApiResponse> GetSiteUsers(int siteId)
        {
            var endpoint = $"/web/api/v1/sites/{siteId}/users";
            var responseJson = await this.httpClient.GetStringAsync(endpoint);
            var response = JsonConvert.DeserializeObject<SiteUserApiResponse>(responseJson);

            return response;
        }

        public async Task<PaginatedApiResponse<SiteBriefing>> GetSiteBriefings(int siteId, int? limit = null, int? offset = null)
        {
            var endpoint = $"/web/api/v2/sites/{siteId}/briefings";
            var query = new Dictionary<string, object>
            {
                {nameof(limit), limit },
                {nameof(offset), offset }
            }.CreateQueryString();

            var responseJson = await this.httpClient.GetStringAsync($"{endpoint}?{query}");
            var response = JsonConvert.DeserializeObject<PaginatedApiResponse<SiteBriefing>>(responseJson);

            return response;
        }

        public async Task<BriefingLogApiResponse> GetBriefingLogs(int briefingId)
        {
            var endpoint = $"/web/api/v2/briefings/{briefingId}/logs";

            var responseJson = await this.httpClient.GetStringAsync(endpoint);
            var response = JsonConvert.DeserializeObject<BriefingLogApiResponse>(responseJson);

            return response;
        }

        public async Task<SiteAttendeesApiResponse> GetSiteAttendees(int siteId, DateTime attendanceStartUtc, DateTime attendanceEndUtc)
        {
            var dateFormat = "yyyy-MM-ddTHH:mm:ss.FFFZ";
            var endpoint = $"/web/api/v2/sites/{siteId}/attendees?filter_attendance_start_utc={attendanceStartUtc.ToString(dateFormat)}&filter_attendance_end_utc={attendanceEndUtc.ToString(dateFormat)}&unarchived_worker_notes_only=true";

            var responseJson = await this.httpClient.GetStringAsync(endpoint);
            var response = JsonConvert.DeserializeObject<SiteAttendeesApiResponse>(responseJson);

            return response;
        }

        public async Task<PaginatedApiResponse<SiteInduction>> GetSiteInductions(int siteId, int? limit = null, int? offset = null)
        {
            var endpoint = $"/web/api/v2/sites/{siteId}/inductions";
            var query = new Dictionary<string, object>
            {
                {nameof(limit), limit },
                {nameof(offset), offset }
            }.CreateQueryString();

            var responseJson = await this.httpClient.GetStringAsync($"{endpoint}?{query}");
            var response = JsonConvert.DeserializeObject<PaginatedApiResponse<SiteInduction>>(responseJson);

            return response;
        }
    }
}
