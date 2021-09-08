using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace MAD.DataWarehouse.SignOnSite.Api
{
    public class SignOnSiteApiClient
    {
        private readonly HttpClient httpClient;

        public SignOnSiteApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<PaginatedApiResponse<Site>> GetSites(int? limit = null, int? offset = null)
        {
            var endpoint = "/api/public/sites";
            var query = new Dictionary<string, object>
            {
                { nameof(limit), limit},
                { nameof(offset), offset}
            }.CreateQueryString();

            var responseJson = await this.httpClient.GetStringAsync($"{endpoint}?{query}");
            var response = JsonConvert.DeserializeObject<PaginatedApiResponse<Site>>(responseJson);

            return response;
        }

        public async Task<PaginatedApiResponse<SiteAttendance>> GetSiteAttendances(int siteId, int? limit = null, int? offset = null, DateTime? filter_start_time = null, DateTime? filter_end_time = null, string order_direction = null)
        {
            var endpoint = $"/api/public/sites/{siteId}/attendances";
            var query = new Dictionary<string, object>
            {
                { nameof(limit), limit},
                { nameof(offset), offset},
                { nameof(filter_start_time), filter_start_time},
                { nameof(filter_end_time), filter_end_time},
                { nameof(order_direction), order_direction},
            }.CreateQueryString();

            var responseJson = await this.httpClient.GetStringAsync($"{endpoint}?{query}");
            var response = JsonConvert.DeserializeObject<PaginatedApiResponse<SiteAttendance>>(responseJson);

            return response;
        }

       
    }
}
